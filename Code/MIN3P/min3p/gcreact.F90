!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/gcreact.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c ----------------------------------------------------------------------
!c subroutine gcreact 
!c ------------------
!c
!c geochemical reactions for batch system
!c
!c equilibration of aqueous species
!c kinetically controlled dissolution-precipitation of minerals
!c equilibration of aqueous species with specified minerals
!c
!c written by:      Uli Mayer - October, 95
!c
!c last modified:   December 4, 96
!c                  November 17, 2004 - quick fix: remove aqueous equilibration statement
!c                                      line 321 (clogs *.log file for nodal aqueous IC)
!c                  June 1, 2006 - pass integer 0 to updtsvmp.f = no nodal update of
!c                                       surface area
!c
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c                                                                    I O
!c passed:   real*8:
!c           -------
!c           cnew(nc)           = concentrations of free species      + +
!c                                - new time level [moles/l water]
!c           cold(nc)           = concentrations of free species      + +
!c                                - old time level [moles/l water]
!c           cmcnew(nm,nthreads)= concentration of minerals           + +
!c                                - new time level [moles/l bulk]
!c           cx(nx)             = concentrations of secondary aqueous * +
!c                                species [moles/l water]
!c           gnew(ng)           = gas concentrations                  * +
!c                                - new time level [moles / l air]
!c           sw                 = water saturation                    + -
!c           sa                 = air saturation                      + -
!c           por                = porosity                            + -
!c
!c           integer*4:
!c           ----------
!c           idbg               = unit number - debugging information + -
!c           igen               = unit number - general output file   + -
!c           ilog               = unit number - logbook               + -
!c           l_prfx             = length of prefix of I/O files       + -
!c           l_zone_name        = length of zone name                 + -
!c
!c           logical:
!c           --------
!c           tec_header         = .true.  -> write header for tecplot + -
!c                                           postprocessing to output
!c                                           files
!c                                .false. -> skip headers
!c
!c           character:
!c           ----------
!c           prefix             = prefix name for all I/O files       + -
!c           zone_name          = name of zone                        + -
!c
!c common:   
!c chem.f:   real*8:
!c           -------
!c           alc(nc-1,nc-1,nthreads) 
!c                              = Jacobian matrix                * +
!c           blc(nc-1,nthreads) = rhs-vector                          * +
!c           areac(nm)          = reactivity term                     + +
!c           eqm(nm,nthreads)   = equilibrium constants for minerals  + -
!c           cec(nthreads)      = cation exchange capacity [meq/100g] + -
!c           delt_lc(nthreads)  = time step for local chemistry       + +
!c                                computations
!c           distcoff_lc(nc)    = sorption distribution coefficient   + -
!c                                [L H_2O/L bulk] 
!c                                - local chemistry
!c           gammac(nc)         = activity coefficients of components * *
!c                                as species in solution
!c           gammax(nx)         = activity coefficients of aqueous    * *
!c                                complexes
!c           ph_fixed           = fixed value for pH                  + -
!c           phic(nm,nthreads)  = volume fractions of minerals        + +
!c           phicold(nm,nthreads)
!c                              = volume fractions of minerals        + +
!c                                - old time level
!c           ratedp(nm,nthreads)= absolute dissolution-precipitation  * +
!c                                rates of minerals
!c                                [moles/(l bulk*day)]
!c           sion1(nthreads)    = ionic strength of solution          * +
!c           sionmax            = max. ionic strength in solution     + -
!c           tempk              = temperature [deg K]                 + -
!c           tfinal_lc          = final solution time                 + -
!c                                (local chemistry)
!c           time_factor_lc     = conversion factor from I/O time     + -
!c                                units to internal time units
!c           totan(nc,nthreads) = total sorbed component              * +
!c                                concentrations
!c                                non-competitive sorption
!c                                - new time level [moles/l bulk)
!c           totcn(n,nthreads)  = total aqueous component             + +
!c                                concentrations
!c                                - new time level [moles/l water]  
!c           totco(n,nthreads)  = total aqueous component             + +
!c                                concentrations
!c                                - old time level [moles/l water]
!c           totgn(nc-1,nthreads)
!c                              = total gaseous component             * +
!c                                concentrations
!c                                - new time level [moles/l air]
!c           xnum(nm*nc)        = stoichiometric coefficients of      + -
!c                                components in mineral
!c
!c           integer*4:
!c           ----------
!c           iam(nm+1)          = row pointer array to                + -
!c                                stoichiometric coefficients of
!c                                components in mineral
!c           ilbb               = unit number, concentrations of      + -
!c                                             sorbed species
!c                                             - transient data
!c                                               (local system)
!c           ilbc               = unit number, free species and       + -
!c                                             secondary aqueous
!c                                             species concentrations
!c                                             - transient data
!c                                               (local system)
!c           ilbd               = unit number, reaction rates for     + -
!c                                             dissolution-
!c                                             precipitation
!c                                             reactions
!c                                             - transient data
!c                                               (local system)
!c           ilbg               = unit number, partial gas pressures  + -
!c                                             - transient data
!c                                               (local system)
!c           ilbgr              = unit number, degassing rates        + -
!c                                             - transient data
!c                                               (local system)
!c           ilbm               = unit number, master variables       + -
!c                                             - transient data
!c                                               (local system)
!c           ilbi               = unit number, rates of intra-aqueous + -
!c                                             kinetic reactions
!c                                             - transient data
!c                                               (local system)
!c           ilbs               = unit number, saturation indices     + -
!c                                             - transient data
!c                                               (local system)
!c           ilbt               = unit number, total aqueous          + -
!c                                             component
!c                                             concentrations
!c                                             - transient data
!c                                               (local system)
!c           ilbv               = unit number, mineral volume         + -
!c                                             fractions
!c                                             - transient data
!c                                               (local system)
!c           ilbx               = unit number, saturation indices     + -
!c                                             excluded minerals
!c                                             - transient data
!c                                               (local system)
!c           idetail_lc(nthreads)
!c                              = information level                   + -
!c           iph_steps          = counter (ph-sweep calculations)     + -
!c           iter_lc(nthreads)  = iteration counter                   * +
!c                                (local chemistry)
!c           ittot_lc(nthreads) = total number of iterations          * +
!c                                (local chmeistry)
!c           jam(nm*nc)         = column pointer array to             + -
!c                                stoichiometric coefficients of
!c                                free species in mineral
!c           maxit_lc(nthreads) = max. number of iterations           + -
!c                                (local chemistry)
!c           nc                 = number of components including h2o  + -
!c           nopu               = number of primary unknowns          + -
!c           ng                 = number of gases                     + -
!c           nm                 = number of minerals                  + -
!c           nr                 = number of redox couples             + -
!c           ntstp_lc(nthreads) = time step counter                   * +
!c                                (local chemistry)
!c
!c           logical:
!c           --------
!c           far_from_equil(nm) = .true.  -> far from equilibrium     + -
!c           implicit_surface   = .true.  -> equilibrate surface      + -
!c                                           sites with fixed 
!c                                           solution composition
!c           implicit_surface_ion  = .true.  -> equilibrate surface   + -
!c                                           sites with fixed 
!c                                           solution composition
!c                                           of ion-exchange
!c           implicit_surface_surf = .true.  -> equilibrate surface   + -
!c                                           sites with fixed 
!c                                           solution composition
!c                                           of surface-complex
!c           finite_minerals    = .true.  -> finite minerals          + -
!c           lb_output          = .true.  -> output of transient data + -
!c                                           (local chemistry)
!c           noncompetitive_sorption = logical array for activation   + -  
!c                                     of noncompetitive sorption
!c                                     reactions
!c           ph_sweep           = .true.  -> sweep over specified     + -
!c                                           pH-range
!c           reactive_minerals  = .true.  -> consider mineral         + -
!c                                           dissolution-
!c                                           precipitation reactions
!c           redox_equil        = .true.  -> equilibrium reactions    + -
!c                                           for redox couples
!c           tstart_to_tfinal   = .true.  -> stop kinetic batch       + -
!c                                           simulation when tfinal
!c                                           is reached
!c           update_activity(nthreads)
!c                              = 'no_update' -> unity activity       + -
!c                                 coefficients
!c                                'time_lagged' -> update activity
!c                                 coefficients after each time step
!c                                'double_update' -> double update
!c                                 of activity coefficients during
!c                                 Newton iterations
!c           update_activity_lc = 'no_update' -> unity activity       + -
!c                                 coefficients
!c                                'time_lagged' -> update activity
!c                                 coefficients after each time step
!c                                'double_update' -> double update
!c                                 of activity coefficients during
!c                                 Newton iterations
!c                                 (local chemistry)
!c
!c           character*12:
!c           -------------
!c           time_unit_lc       = time unit for output -> 'years'     + -
!c                                                        'days'
!c                                                        'hours'
!c                                                        'seconds'
!c
!c local:    real*8:
!c           -------
!c           delt_io            = time step (I/O units)
!c           r0                 = constant
!c           time               = current solution time              
!c           time_io            = current solution time (I/O units)
!c
!c           integer*4:
!c           ----------
!c           ic                 = counter (components)
!c           ic2                = counter (components)
!c           ig                 = counter (gases)    
!c           im                 = counter (minerals)
!c           ir                 = counter (redox couples)
!c
!c           logical:
!c           --------
!c           done               = .true.  -> terminate time loop
!c           all_saturated      = .true.  -> all minerals to be
!c                                           equilibrated reached
!c                                           saturation
!c           false_logical      = .false.
!c
!c
!c external: chrgcorr  = correct total aqueous component 
!c                       concentration for selected component 
!c                       to satisfy charge balance
!c           comptotc  = compress concentration vector, if number
!c                       of unknowns is reduced due to redox 
!c                       equilibrium reactions
!c           jaclc     = construct jacobian matrix 
!c                       (local chemistry)
!c           simq      = solves system of n linear equations
!c                       by Gaussian elimination
!c           updatelc  = update solution vector
!c                       (local chemistry)
!c           updtsvap  = update secondary variables in aqueous
!c                       phase
!c           satindex  = compute saturation index
!c           surfcomp  = compute surface composition based on 
!c                       equilibrated solution composition
!c           updtsvgp  = update secondary variables in gaseous
!c                       phase
!c           updtsvmp  = update secondary variables in mineral 
!c                       phase
!c           totcona   = compute total sorbed component concenrations 
!c                       due to non-competitive sorption reactions
!c           tprfrtlc  = write transient data to output file for
!c                       postprocessing
!c           tsteplc   = estimate magnitude of next time step
!c                       (local chemistry)
!c ----------------------------------------------------------------------
 
      subroutine gcreact(cnew,cold,cx,gammac,gammax,gnew,sw,sa,por,     &
                        igen,ilog,idbg,tec_header,prefix,l_prfx,        &
                        zone_name,l_zone_name)
     
 
      use parm
      use chem
      use gen, only : rank, b_enable_output
      use multidiff, only : hmulti_diff
#ifdef OPENMP
      use omp_lib 
#endif 
#ifdef PETSC
      use petsc_mpi_common, only : petsc_mpi_finalize
#endif
 
      implicit none
      
      real*8 :: cnew,cold,cx,gammac,gammax,gnew,sw,sa,por
      
      integer :: igen,ilog,idbg,l_prfx,l_zone_name
      
      integer :: tid, i, ic, im, istart, istop, istart2, istop2,       &
                 ireac, ilbis, i1, i2, i3, ii, ic2, icount, icur, next
      
      real*8 :: time, time_io, satindex, delt_io, gammatemp

      external comptotc, simq, satindex, totconc, totcona, tprfrtlc    

      logical :: tec_header
      logical :: done
      logical :: all_saturated
      logical :: not_converged
      logical :: false_logical

      character*72 :: prefix
      character*72 :: zone_name
      character*72 :: update_activity_save

      dimension cnew(*),cold(*),cx(*),gammac(*),gammax(*),gnew(*)

      real*8, parameter :: r0 = 0.0d0, r1 = 1.0d0
      integer, parameter :: i0 = 0
      
      !For the shared-memory parallel version, the variables defined in the module
      !are shared variables by different threads. So as to avoid race condition, 
      !these variable should be passed by dummy arguments. Danyang Su, 2013-05.
      interface      
        !>interface of updtsvap
        subroutine updtsvap (c,cx,gammac,gammax,strion)
          use parm, only : type_r8
          real(type_r8), dimension(*) :: c
          real(type_r8), dimension(*) :: cx
          real(type_r8), dimension(*) :: gammac
          real(type_r8), dimension(*) :: gammax
          real(type_r8) :: strion
        end subroutine updtsvap
                             
        !>interface of jaclc
        subroutine jaclc(cnew,cx,gammac,gammax,sw,sa,por)
          use parm, only : type_i4, type_r8
          real (type_r8), dimension(*) :: cnew
          real (type_r8), dimension(*) :: cx
          real (type_r8), dimension(*) :: gammac
          real (type_r8), dimension(*) :: gammax
          real (type_r8) :: sw
          real (type_r8) :: sa
          real (type_r8) :: por
        end subroutine jaclc
                         
        !>interface of updatelc                 
        subroutine updatelc(c,ulc,ilog,not_converged,zone_name)         
          use parm, only : type_i4, type_r8
          real(type_r8), dimension(*) :: c
          real(type_r8), dimension(*) :: ulc
          integer(type_i4) :: ilog
          logical :: not_converged
          character*72 :: zone_name
        end subroutine

        !>interface of chrgcorr
        subroutine chrgcorr   
        end subroutine
        
        !>interface of surfcomp
        subroutine surfcomp(cnew,gammac,sw,por,ilog)
          use parm, only : type_i4, type_r8
          real (type_r8), dimension(*) :: cnew
          real (type_r8), dimension(*) :: gammac
          real (type_r8) :: sw
          real (type_r8) :: por
          integer(type_i4) :: ilog          
        end subroutine
        
        !>interface of tsteplc
        subroutine tsteplc(cnew,cold,ulc)
          use parm, only : type_r8
          real(type_r8), dimension(*) :: cnew
          real(type_r8), dimension(*) :: cold
          real(type_r8), dimension(*) :: ulc
        end subroutine
        
        !>interface of updtsvmp
        subroutine updtsvmp(cmnewm,cmoldm,phim,aream,ratem,deltsv,ivol)
          use parm, only: type_i4, type_r8        
          real(type_r8), dimension(*) :: cmnewm
          real(type_r8), dimension(*) :: cmoldm
          real(type_r8), dimension(*) :: phim 
          real(type_r8), dimension(*) :: aream
          real(type_r8), dimension(*) :: ratem
          real(type_r8) :: deltsv
          integer(type_i4) :: ivol  
        end subroutine
        
        !>interface of updtsvgp
        subroutine updtsvgp (c,gammac,g,totcg,tempkel)
          use parm, only: type_i4, type_r8
          real(type_r8), dimension(*) :: c
          real(type_r8), dimension(*) :: gammac
          real(type_r8), dimension(*) :: g
          real(type_r8), dimension(*) :: totcg
          real(type_r8) :: tempkel
        end subroutine
        
      end interface
      
#ifdef OPENMP
      tid = omp_get_thread_num() + 1
#else
      tid = 1
#endif

!c  initialize local parameters

      done = .false.
      ntstp_lc(tid) = 0
      ittot_lc(tid) = 0
      time = r0
      time_io = r0
      false_logical = .false.

      idetail_lc(tid) = 1
      maxit_lc(tid) = 2000

!c  define update technique for activity coefficients for
!c  local chemistry
!c  save the update activity type first as this should be 
!c  reverted later, DSU
      update_activity_save = update_activity(tid)
      if (update_activity_lc.eq.'no_update') then
        update_activity(tid) = 'no_update'
      else
        update_activity(tid) = 'double_update'
      endif
 
!c  equilibrium solution as initial condition 

!      THH hack Nov 17, 2004: commented out 
!      if (zone_name.ne.'temperature-correction') then
!        write(ilog,'(/a/72a/)') 'equilibration of aqueous species',
!     &                      ('-',i=1,72)
!      end if

!c  compress initial total aqueous component concentration vector 
!c  in case of redox equilibrium reactions

      if (redox_equil.and.nr.gt.0) then
        call comptotc(totco(:,tid))
      end if

!c  enter time loop, if kinetic reactions are specified
 
      do while (.not.done)

!c  start Newton iteration
 
        iter_lc(tid) = 0
        not_converged = .true.
 
        do while (not_converged) 

          if (iter_lc(tid).lt.maxit_lc(tid)) then

            iter_lc(tid) = iter_lc(tid)+1
            ittot_lc(tid) = ittot_lc(tid)+1

!c  construct Jacobian matrix and rhs-vector 

            call jaclc(cnew,cx,gammac,gammax,sw,sa,por)

!c  solve for update
  
            call simq(alc(:,:,tid),blc(:,tid),nopu,nc-1)
 
!c  update solution vector
   
            call updatelc(cnew,blc(:,tid),ilog,not_converged,zone_name)

!c  correct total aqueous component concentration for selected
!c  component to satisfy charge balance

            if (ntstp_lc(tid).eq.0) then
              call chrgcorr
            end if

          else                                 

            if (rank == 0) then  
              write(ilog,*)'-------------------------------------------'
              write(ilog,*)'   terminated in routine gcreact           '
              write(ilog,*)'   maximum number of iterations exceeded   '
              write(ilog,*)'   bye now ...                             '
              write(ilog,*)'-------------------------------------------'
            end if
#ifdef PETSC
            call petsc_mpi_finalize
#endif
            stop

          end if
        end do                                 !end - Newton loop
 
!c  update secondary variables in water phase
        call updtsvap(cnew,cx,gammac,gammax,sion1(tid))

!c  compute composition of surface sites based on equilibrated water
!c  composition

        if ((nsb_ion.gt.0.and.implicit_surface_ion).or.                &
            (nsb_surf.gt.0.and.implicit_surface_surf)) then
          call surfcomp(cnew,gammac,sw,por,ilog)
        end if

!c  compute total sorbed component concentrations due to 
!c  non-competitive sorption reactions based on equilibrated 
!c  water composition

        if (noncompetitive_sorption) then
          call totconc(cnew,cx,totcn(:,tid))
          call totcona(totan(:,tid),totcn(:,tid),distcoff_lc,sw,por)
          call comptotc(totcn(:,tid))
        end if

!c  saturation indices for minerals

        if (nm.gt.0) then
          do im = 1,nm
            if (.not.far_from_equil(im)) then
!c_isotope
              if (isofrac(im)) then
                satm(im,tid) = eqm(im,tid)**(-r1)
                ireac = iamd(im)            
                istart = iam(im)
                istop = iam(im+1)-1           
          
                do i1 = istart, istop ! loop through components in mineral
                  icount = 0
                  ic = jam(i1)
                  next = 0 
                  do i = 1, nifrm(im)  ! loop through isotope sets
                    istart2 = next + iamdiso(im)
                    icur = iamdiso2(im) + i - 1
                    istop2 = iamdiso(im) + jamdiso2(icur) - 1
                    next = jamdiso2(icur)
                    gammatemp = r0
                    !loop through isotope compents in set
                    do i2 = istart2, istop2
                      ii = jamdiso(i2)
                      !check to see if component is an isotope
                      if (ii.eq.ic) then 
                        icount = icount + 1
                        !if so sum the isotope activities
                        do i3 = istart2, istop2 
                            ic2 = jamdiso(i3)
                            gammatemp = gammatemp+gammac(ic2)*cnew(ic2)
                        end do 
                        satm(im,tid) = satm(im,tid) *gammatemp**xnum(i1)
                      end if
                    end do
                  end do 
                  !if not calculate the saturaton index the normal way  
                  if (icount.eq.0) then    
                    satm(im,tid) = satm(im,tid)*(gammac(ic)*cnew(ic))**xnum(i1)
                  end if
                end do   !i1 
              else
                satm(im,tid) = satindex(cnew,eqm(im,tid),gammac,xnum,  &
                               iam,jam,im)
              end if
            end if
          end do
        end if

!c  update gas concentrations and total gaseous component concentrations

        if (ng.gt.0) then
          call updtsvgp(cnew,gammac,gnew,totgn(:,tid),tempk)
        end if

!c  write data for ph-pc diagrams
        if(rank == 0) then  
          if (lb_output .and. ph_sweep .and. b_enable_output) then
              
              
            call tprfrtlc(totcn(:,tid),cnew,cx,gammac,gammax,          &
                     cmcnew(:,tid),gnew,                               &
                     cec(tid),distcoff_lc,areac,                       &
                     phic(:,tid),phicold(:,tid),                       &
                     sion1(tid),tempk,r0,r0,ph_fixed,                  &
                     delt_lc(tid),sw,por,ilbt,ilbc,ilbm,               &
                     ilbg,ilbgr,ilbi,ilbb,ilbs,ilbv,ilbd,ilbx,ilbis,   &
                     offset_ilbt,offset_ilbc,offset_ilbm,              &
                     offset_ilbg,offset_ilbgr,offset_ilbi,             &
                     offset_ilbb,offset_ilbs,offset_ilbv,              &
                     offset_ilbd,offset_ilbx,offset_ilbis,             &
                     offset_ilbt_ijk,offset_ilbc_ijk,offset_ilbm_ijk,  &
                     offset_ilbg_ijk,offset_ilbgr_ijk,offset_ilbi_ijk, &
                     offset_ilbb_ijk,offset_ilbs_ijk,offset_ilbv_ijk,  &
                     offset_ilbd_ijk,offset_ilbx_ijk,offset_ilbis_ijk, &
                     prefix,l_prfx,tec_header,0,iph_steps,iph_steps+1, &
                     zone_name,l_zone_name,false_logical)
          end if
        end if

!c  only equilibration of aqueous species -> return
 
        if (.not.reactive_minerals) then

          done = .true.
 
!c  kinetically controlled dissolution-precipitation reactions
 
        elseif (reactive_minerals) then

          if (tstart_to_tfinal) then

            all_saturated = .false.

          else

            all_saturated = .true.

!c  check, if equilibrium with respect to specified minerals is reached

            do im=1,nm
              if (minequil(im)) then
                if (satm(im,tid).lt.0.99d0.or.satm(im,tid).gt.1.01d0) then
                  all_saturated = .false.
                end if
              end if
            end do
      
          end if
 
!c  update concentrations of minerals, mineral volumes and 
!c  specific surface area of minerals
 
          if (finite_minerals.and.nm.gt.0.and.ntstp_lc(tid).gt.0) then
          call updtsvmp(cmcnew(:,tid),cmcold(:,tid),phic(:,tid),areac, &
                        ratedp(:,tid),delt_lc(tid),i0)
          end if

!c  optional printout of transient data
      
          if(rank == 0) then
            if (lb_output .and. b_enable_output) then
              call tprfrtlc(totcn(:,tid),cnew,cx,gammac,gammax,        &
                      cmcnew(:,tid),gnew,                              &
                      cec(tid),distcoff_lc,areac,                      &
                      phic(:,tid),phicold(:,tid),                      &
                      sion1(tid), tempk,r0,r0,time_io,                 &
                      delt_lc(tid),sw,por,ilbt,ilbc,ilbm,              &
                      ilbg,ilbgr,ilbi,ilbb,ilbs,ilbv,ilbd,ilbx,ilbis,  &
                      offset_ilbt,offset_ilbc,offset_ilbm,             &
                      offset_ilbg,offset_ilbgr,offset_ilbi,            &
                      offset_ilbb,offset_ilbs,offset_ilbv,             &
                      offset_ilbd,offset_ilbx,offset_ilbis,            &
                      offset_ilbt_ijk,offset_ilbc_ijk,offset_ilbm_ijk, &
                      offset_ilbg_ijk,offset_ilbgr_ijk,offset_ilbi_ijk,&
                      offset_ilbb_ijk,offset_ilbs_ijk,offset_ilbv_ijk, &
                      offset_ilbd_ijk,offset_ilbx_ijk,offset_ilbis_ijk,&
                      prefix,l_prfx,tec_header,0,ntstp_lc(tid),        &
                      ntstp_lc(tid)+1,zone_name,l_zone_name,           &
                      false_logical)
            end if
          end if
 
!c  final solution time is reached -> return
 
          if (time.gt.tfinal_lc.and.tstart_to_tfinal) then
            done = .true.
            all_saturated = .true.
          end if

!c  equilibrium for specified minerals is reached -> return

          if (all_saturated) then

            done = .true.
          
!c  equilibrium between solid and aqueous phase not reached yet
!c  -> prepare for next time step

          else

!c  estimate magnitude of next time step
 
            if (ntstp_lc(tid).gt.0) then           !use initial value
              call tsteplc(cnew,cold,blc(:,tid))
            end if

            ntstp_lc(tid) = ntstp_lc(tid) + 1
            time = time+delt_lc(tid)
            if (ntstp_lc(tid).eq.1 ) then
              if(rank == 0 .and. b_enable_output)  then  
                write(ilog,'(/2a/72a)') 'enter timeloop - ',    &
                     'modelling of reaction path',('-',i=1,72)
              end if
            end if

!c  convert time units to I/O units

            time_io = time/time_factor_lc
            delt_io = delt_lc(tid)/time_factor_lc

!c  write time step information to screen

            if (idetail_lc(tid).gt.0.and.      &
               zone_name.ne.'temperature-correction') then
              if(rank == 0 .and. b_enable_output)  then 
                  
              !write(ilog,'(/72a)')('-',i=1,72)
              !write(ilog,'(a,i5,2x,a,1pe10.3,1x,a,1x,a,1pe10.3,1x,a)')&
              !     'timestep:',ntstp_lc(tid),'time:',time_io,         &
              !     time_unit_lc,'delt: ',delt_io,time_unit_lc
             ! write(ilog,'(72a/)')('-',i=1,72)
              
              end if
            end if

!c  reassign free species concentrations 
 
            do ic=1,nc
              cold(ic) = cnew(ic)
            end do

!c  reassign mineral volume fractions

            do im = 1,nm
              phicold(im,tid) = phic(im,tid)
              cmcold(im,tid) = cmcnew(im,tid)
            end do

!c  redox equilibrium reactions
!c  compute total aqueous component concentrations and
!c  compress total aqueous component concentration vector

            if (redox_equil.and.nr.gt.0) then
              call totconc(cnew,cx,totcn(:,tid))
              call comptotc(totcn(:,tid))
            end if

!c  reassign total aqueous component concentrations (compressed)

            do ic = 1,nc-1
              totco(ic,tid) = totcn(ic,tid)
            end do
 
          end if                          !(all_saturated)
        end if                            !(reactive_minerals)

      end do                              !end - time loop
      
      update_activity(tid) = update_activity_save

      return
      end
