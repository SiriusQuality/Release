!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/tranbcrt_read.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c ----------------------------------------------------------------------
!c subroutine tranbcrt  
!c ------------------- 
!c
!c read boundary conditions (reactive transport)
!c
!c written by:      Danyang Su    - February 18, 2015
!c
!c
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c                                                                    I O
!c passed:   logical:
!c           -------
!c           bflag_read         = read boundary from file             * - 
!c           bflag_update       = update boundary condition           * -
!c           -------
!c           time_prev          = time of previous boundary update    * -
!c           time_next          = time of next boundary update        * -
!c           time_now           = time of current step                * -
!c           time_weight        = spatial weight in linear            * -
!c                                interpolation
!c common:
!c parm.inc: -
!c
!c gen.f:    real*8:
!c           -------
!c           bcondrt_a(nc,nbrt) = concentrations in boundary control  * +
!c                                volumes (aqueous phase)
!c                                first type b.c. -> free species 
!c                                                   concentrations
!c                                third type b.c. -> total aqueous
!c                                                   component
!c                                                   concentrations
!c           bcondrt_g(nc,nbrt) = concentrations in boundary control  * +
!c                                volumes (gaseous phase)
!c                                third type b.c. -> total gaseous
!c                                                   component
!c                                                   concentrations
!c           cnew(nc,nn)        = concentrations of free species      + -
!c                                - new time level [moles/l water]
!c           gbrt(ng,nbrt)      = gas concentrations in boundary      * +
!c                                control volumes
!c           time_io            = current solution time (I/O units)   + -
!c
!c           integer*4:
!c           ----------
!c           iabrt(nbzrt+1)     = pointer array - boundary conditions * +
!c                                (reactive transport)
!c           icnv               = unit number, data conversion and    + -
!c                                             temporary storage
!c           idat               = unit number, run specific input     + -
!c                                             file
!c           idbg               = unit number, debugging information  + -
!c           igen               = unit number, generic output file    + -
!c           ilog               = unit number, log book               + -
!c           itmp               = unit number, temporary storage      + -
!c           itsrc              = pointer to current target read      + -
!c                                time for boundary conditions        
!c                                (reactive transport)
!c           jabrt(nbrt)        = pointer array - boundary conditions * +
!c                                (reactive transport)
!c           l_prfx             = length of prefix of I/O files       + -
!c           l_zone_name        = length of zone name                 * +
!c           nbzrt              = number of boundary zones
!c                                (reactive transport)
!c
!c           logical:
!c           --------
!c           redox_equil_rt     = .true.  -> equilibrium redox        + -
!c                                           reactions
!c           tec_header         = .true.  -> write header for tecplot + -
!c                                           postprocessing to output
!c                                           files
!c                                .false. -> skip headers
!c
!c           character:
!c           ----------
!c           btypert(nn)        = type of boundary control volumes    * +
!c                                'first'  = Dirichlet
!c                                           (specified
!c                                            concentration)
!c                                'second' = Neumann
!c                                           (free advective mass
!c                                            outflux for aqueous
!c                                            phase)
!c                                'third'  = Cauchy
!c                                           (specified advective
!c                                            mass influx for
!c                                            aqueous phase)
!c                                'mixed'  = mixed
!c                                           (specified advective
!c                                            mass influx and
!c                                            free diffusive mass
!c                                            influx for aqueous
!c                                            phase and free
!c                                            diffusive mass influx
!c                                            for gaseous phase
!c           prefix             = prefix name for all I/O files       + -
!c           section_header     = section header                      * +
!c           time_unit          = time unit for output -> 'years'     + -
!c                                                        'days'
!c                                                        'hours'
!c                                                        'seconds'
!c           zone_name          = name of zone                        * +
!c           
!c
!c chem.f:   real*8:
!c           -------
!c           actv(nc)           = activities of free species          * +
!c                                - new time level
!c           ccnew(nc)          = concentrations of free species      + +
!c                                - new time level [moles/l water]
!c           ccold(nc)          = concentrations of free species      + +
!c                                - old time level [moles/l water]
!c           cgc(ng)            = gas concentrations                  * +
!c                                - new time level [moles/l air]
!c           cxc(nx)            = concentrations of secondary         * +
!c                                aqueous species
!c                                - new time level [moles/l water]
!c           delt_lc(nthreads)  = time step for local chemistry       * +
!c                                computations
!c           gamma_l(nc+nx)     = activity coefficients for aqueous   * *
!c                                species
!c           gfwc(nc)           = gram formula weight of components   + -
!c           phguess            = guess for pH                        * +
!c           tempk              = temperature [deg K]                 + -
!c           totcn(n,nthreads)  = total aqueous component             * +
!c                                concentrations
!c                                - new time level [moles/l water]
!c           totco(n,nthreads)  = total aqueous component             * +
!c                                concentrations
!c                                - old time level [moles/l water]
!c           totgn(n,nthreads)  = total gaseous component             * +
!c                                concentrations
!c                                - new time level [moles/l air]
!c
!c           integer*4:
!c           ----------
!c           l_namec(nc)        = length of component names           + -
!c           nbio               = number of biomass components        + -
!c           nc                 = number of components including h2o  + -
!c           ncorder(nc)        = ordering array for components       + -
!c                                ncorder(old order) = new order
!c           ng                 = number of gases                     + -
!c           nr                 = number of redox couples             + -
!c
!c           logical:
!c           --------
!c           reactive_minerals  = .true.  -> consider mineral         * +
!c                                           dissolution-
!c                                           precipitation reactions
!c           redox_equil_lc     = .true.  -> equilibrium reactions    + -
!c                                           for redox couples
!c
!c           character:
!c           ----------
!c           component_type(nc) = 'aqueous' = aqueous component       * +
!c                                'surface' = surface site
!c                                'biomass' = biomass
!c           ctype(nc-1)        = 'charge' = correct total aqueous    * +
!c                                           component concentration
!c                                           for specified component 
!c                                           to satisfy charge balance
!c                                'fixed'  = compute total aqueous
!c                                           component concentrations
!c                                           based on fixed activities
!c                                           of components as species
!c                                           in solution
!c                                'free'   = compute concentrations
!c                                           of components as species
!c                                           in solution based on 
!c                                           specified total aqueous
!c                                           component concentrations
!c                                'ph'    =  pH specified for 'h+1'
!c           input_units        = 'mol/l'                             + -
!c                                'mmol/l'
!c                                'mg/l'
!c                                'g/l'
!c           namec(nc)          = component names                     + -
!c
!c local:    real*8:
!c           -------
!c           porc               = porosity for local chemistry
!c                                calculations
!c           r1                 = constant
!c           r1000              = constant
!c           sgc                = gaseous phase saturation for local
!c                                chemistry calculations
!c           sac                = aqueous phase saturation for local
!c                                chemistry saturations
!c
!c           integer*4:
!c           ----------
!c           ibrt               = counter (boundary control volumes)
!c           ibrt_start         = start of current boundary zone
!c           ibrt_stop          = end of current boundary zone
!c           ibz                = counter (boundary zones)
!c           ic                 = counter (components)
!c           icount             = counter
!c           ig                 = counter (gases)
!c           itemp              = pointer
!c           ivol               = counter (control volumes)
!c           l_string           = length of text string
!c
!c           logical:
!c           --------
!c           found              = logical variable to exit search
!c           found_subsection   = .true.  -> subsection header was
!c                                           found in input file
!c
!c           character:
!c           ----------
!c           subsection         = name of subsection in input file
!c
!c external: comptotc  = compress concentration vector, if number
!c                       of unknowns is reduced due to redox
!c                       equilibrium reactions
!c           findstrg  = find text string in file
!c           findzone  = find zone in input section
!c           gcreact   = geochemical reactions for batch system
!c           guess     = guess for concentrations of
!c                       free species
!c           icbcrt    = assign initial or boundary condition
!c                       to global system (reactive transport)
!c           minmaxwd  = determine minimum total aqueous component
!c                       concentrations and maximum secondary aqueous
!c                       species concentration in solution domain
!c           outputlc  = write results of local chemistry
!c                       computations to generic output file
!c           readtime  = read section in input file for 
!c                       updating boundary conditions
!c           readzone  = read zone in section of input file and 
!c                       write to temporary file
!c           rtrvpprm  = retrieve physical parameters
!c           setsize   = define number of primary unknowns
!c           totconc   = compute total aqueous component
!c                       concentrations based on concentrations
!c                       of free species and secondary aqueous
!c                       species
!c           totconcg  = compute total gaseous component
!c                       concentrations based on concentrations
!c                       of gases
!c ----------------------------------------------------------------------
 
      subroutine tranbcrt_read
 
      use parm
      use gen
      use chem
#ifdef PETSC
      use petsc_mpi_common, only : petsc_mpi_finalize
#endif
      implicit none      
     
      integer :: i, iaq, its, ibz, ic, ierr, itemp, l_string
      
      logical found, found_subsection
      character*72 subsection

      external findstrg, findzone,readtime, readzone

      real*8, parameter :: r1 = 1.0d0, r1000 = 1.0d+3 
      
!c  allocate transient boundary variables
      allocate (transient_time_series(ntsrc+1), stat = ierr)
      transient_time_series=0.0d0
      call checkerr(ierr,'transient_time_series',ilog)

      allocate (totco_series(nc-1,nbzrt,ntsrc+1), stat = ierr)
      totco_series=0.0d0
      call checkerr(ierr,'totco_series',ilog)
      
      allocate (scalfac_aq_series(naq,nbzrt,ntsrc+1), stat = ierr)
      scalfac_aq_series=0.0d0
      call checkerr(ierr,'scalfac_aq_series',ilog)

      allocate (phguess_series(nbzrt,ntsrc+1), stat = ierr)
      phguess_series=0.0d0
      call checkerr(ierr,'phguess_series',ilog)
      
      allocate (b_update_zone_series(nbzrt,ntsrc+1), stat = ierr)
      b_update_zone_series=.false.
      call checkerr(ierr,'b_update_zone_series',ilog)
      
      allocate (ctype_bzrt_series(nc-1,nbzrt,ntsrc+1), stat = ierr)
      ctype_bzrt_series=' ' 
      call checkerr(ierr,'ctype_bzrt_series',ilog)
      
!c  assign initial boundary values
      transient_time_series(1) = time
      totco_series(1:nc-1,1:nbzrt,1)=totco_init(1:nc-1,1:nbzrt)
      scalfac_aq_series(1:naq,1:nbzrt,1)=scalfac_aq_init(1:naq,1:nbzrt)
      phguess_series(1:nbzrt,1)=phguess_init(1:nbzrt)
      ctype_bzrt_series(1:nc-1,1:nbzrt,1)=ctype_bzrt_init(1:nc-1,1:nbzrt)
      
!c  read transient boundary conditions

      do its = 2, ntsrc+1
          
        transient_time_series(its) = tsrc(its-1)  

!c  search for data for current target read time in input file
!c  and write data to temporary file
        call readtime(idat,itmp,ilog,its-1,found)

!c  read source chemistry data for current target read time

        do ibz = 1,nbzrt                      !loop over number of zones
          
!c  find current zone in input file and write to temporary file
          subsection = 'number and name of zone'        
          call findzone(subsection,itmp,found_subsection,ibz,zone_name)
          
          if (found_subsection) then
                
            b_update_zone_series(ibz,its) = .true.
            if(its == 2) then
              b_update_zone_series(ibz,its-1) = .true.
            end if
              
            call readzone(itmp,icnv,ilog,zone_name,found_subsection)
          
!c  define length of zone name

            l_zone_name = index(zone_name,'  ')-1
            if (l_zone_name.lt.0.or.l_zone_name.gt.72) then
              l_zone_name = 72
            end if

!c  write current update information to generic output file
            if (b_enable_output .and. b_enable_output_gen) then
              write(igen,'(/72a)') ('-',i=1,72)
              write(igen,'(a,1pe10.3,1x,a)')                           &
                    'read updating boundary conditions, T = ',         &
                     tsrc(its-1)/time_factor, time_unit                                    
              write(igen,'(72a)')('-',i=1,72)
            end if
                                                                      
!c  write header for boundary zone to generic output file
            if (b_enable_output .and. b_enable_output_gen) then                                                             
              write(igen,'(/a,i1,a,1x,a)') 'zone ',ibz,':',            &
                                           zone_name(:l_zone_name)
              write(igen,'(72a)')('-',i=1,72)
            end if

!c  concentration input for current zone           
            subsection = 'concentration input'
            
            call findstrg(subsection,icnv,found_subsection)
            
            if (found_subsection) then
            
              do ic=1,nc-1
              
                itemp = ncorder(ic)                     !internal order
                
                if (component_type(itemp).eq.'aqueous'.or.             &
                    component_type(itemp).eq.'biomass') then                  
              
                  read(icnv,*,err=999,end=999)                         &  
                       totco_series(itemp,ibz,its),                    &
                       ctype_bzrt_series(itemp,ibz,its)
                  
                  ctype(itemp) = ctype_bzrt_series(itemp,ibz,its)
                  
!c  -> convert input units to internal units [moles/l]

                  if (ctype(itemp).ne.'ph'.and.                        &
                      ctype(itemp).ne.'eh'.and.                        &
                      ctype(itemp).ne.'pe'.and.                        &
                      ctype(itemp).ne.'po2'.and.                       &
                      ctype(itemp).ne.'pn2'.and.                       &
                      ctype(itemp).ne.'pch4'.and.                      &
                      ctype(itemp).ne.'pco2x'.and.                     &
                      ctype(itemp).ne.'par'.and.                       &
                      ctype(itemp).ne.'pco2') then                       
                    if (input_units.eq.'mmol/l') then                    
                      totco_series(itemp,ibz,its) =                    &
                            totco_series(itemp,ibz,its) / r1000            
                    elseif (input_units.eq.'mg/l') then                  
                      totco_series(itemp,ibz,its) =                    &
                            totco_series(itemp,ibz,its) / r1000 /      &
                            gfwc(itemp)
                    elseif (input_units.eq.'g/l') then
                      totco_series(itemp,ibz,its) =                    &
                            totco_series(itemp,ibz,its) / gfwc(itemp)
                    end if
                  end if

                end if              !component_type(itemp)
                
              end do                !loop over components
           
            else
            
              l_string = index(subsection,'  ')-1
              
              if (rank == 0) then
                write(ilog,*) 'SIMULATION TERMINATED'
                write(ilog,*) 'error reading input file'
                write(ilog,*) 'section "',section_header(:l_string),'"'
                write(ilog,*) 'zone "', zone_name(:l_zone_name),'"'
                
                if (l_string.eq.-1.or.l_string.gt.72) then
                   l_string=72
                end if
                write(ilog,*) 'subsection "',subsection(:l_string),    &
                           '" missing'
                close(ilog)
              end if
#ifdef PETSC    
              call petsc_mpi_finalize
#endif        
              stop
            
            end if
          end if  
          
          if (b_update_zone_series(ibz,its)) then  

!c  define guess for pH

            subsection = 'guess for ph'
            found_subsection = .false.            
              
            call findstrg(subsection,icnv,found_subsection)
            
            if (found_subsection) then
              read(icnv,*,err=999,end=999) phguess_series(ibz,its)
            else
              phguess_series(ibz,its) = 7.0d0
            end if
          
          
!c_bubbles - spatial dependent intra-aqueous scaling factors

            subsection = 'scaling for intra-aqueous kinetic reactions'
            found_subsection = .false.
                
            call findstrg(subsection,icnv,found_subsection)
           
            if (found_subsection) then
            
              do iaq = 1,naq
                read(icnv,*,err=999,end=999) scalfac_aq_series(iaq,ibz,its)
              end do
              
            else
            
              do iaq = 1,naq
                scalfac_aq_series(iaq,ibz,its) = r1
              end do
           
            end if
        
          end if                                     !update boundary for zone

        end do                                       !number of zones
      
      end do


      goto 1000

999   continue
!c  redefine length of section header

      if (rank == 0) then
        l_string = index(section_header,'  ')-1
        if (l_string.eq.-1.or.l_string.gt.72) then
           l_string=72
        end if
        
        write(ilog,*) 'SIMULATION TERMINATED'
        write(ilog,*) 'error reading input file'
        write(ilog,*) 'section "',section_header(:l_string),'"'
        write(ilog,*) 'zone "', zone_name(:l_zone_name),'"'
        close(ilog)
      end if
#ifdef PETSC
      call petsc_mpi_finalize
#endif
      stop

1000  return
      end
