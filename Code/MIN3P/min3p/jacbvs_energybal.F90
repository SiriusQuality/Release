!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/jacbvs_energybal.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c ----------------------------------------------------------------------
!c subroutine jacbvs
!c -----------------
!c
!c incorporate dirichlet and neumann type boundary condition in 
!c jacobian matrix and rhs vector for variably saturated flow 
!c
!c written by:      Uli Mayer - May 6, 96
!c
!c last modified:   Tom Henderson - March 20, 2003
!c                  added point source
!c
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c                                                                    I O
!c passed:   -
!c
!c common:   
!c gen.f:    real*8:
!c           -------
!c           avs(njavs)         = Jacobian matrix                     + +
!c           bcondvs(nbvs)      = boundary condition                  + -
!c                                (pressure head or flux) or
!c                                identification of seepage face
!c                                boundary type
!c           bvs(nn)            = rhs vector                          + +
!c           
!c           integer*4:
!c           ----------
!c           iavs(nn+1)         = row pointer array for avs           + -
!c           iabvs(nbvs)        = pointer to boundary control volumes + -
!c                                for variably saturated flow
!c           nn                 = total number of control volumes     + -
!c           nbvs               = number of specified boundary        + -
!c                                control volumes
!c                                (variably saturated flow)
!c           njavs              = number of global connections        + -
!c
!c           character:
!c           ----------
!c           btypevs(nbvs)      = boundary type array                 + -
!c                                (variably saturated flow)
!c                                'first'   = Dirichlet
!c                                'second'  = Neumann
!c                                'seepage' = seepage face
!c dens.f:   real*8:
!c           -------
!c           ssdens(nn)         = density of point source fluid       + -
!c
!c           logical:
!c           --------
!c           density_dependence = .true.  -> simulate density 
!c                                           dependent flow
!c           flow_verification  = .true.  -> verify pressure formulation
!c                                           for constant density 
!c                                           test problem
!c
!c local:    real*8:
!c           -------
!c           r0                 = constant
!c           r1                 = constant
!c
!c           integer*4:
!c           ----------
!c           ivol               = counter (control volumes)
!c           ibvs               = counter (boundary control volumes)
!c           istart             = pointer (start of row) 
!c           iend               = pointer (end of row)
!c           idiag              = pointer (diagonal entry)
!c           i1                 = counter (row entries)
!c
!c           logical
!c           -------
!c           isdebug            = debugging information level
!c
!c external: -
!c ----------------------------------------------------------------------
 
      subroutine jacbvs_energybal 
 
      use parm
      use gen
      use dens
      use phys 
      use chem
      
#ifdef OPENMP
      use omp_lib 
#endif 

      implicit none
      
      character(len=100) :: typebc
      logical            :: isdebug
     
      integer :: i1, ibheat, ibvs, ivol, istart, iend, idiag 
      real*8 :: waterflux, densloc, densinc_ivol, dheatflux, heatflux, &
                ddbdflux_energybal, tempinc_ivol, temploc, dtemp_ivol, &
                rhonew, visconew, viscoinc_ivol, heatflux_temp 
      real*8, parameter :: r0 = 0.0d0, r1 = 1.0d0
      
      external :: ddbdflux_energybal, rhonew, visconew
      
      !!For the shared-memory parallel version, the variables defined in the module
      !!are shared variables by different threads. So as to avoid race condition, 
      !!these variable should be passed by dummy arguments. Danyang Su, 2013-05.
      !interface
      !
      !  !>interface of jacbevap
      !  subroutine jacbevap(ivol,typeequation)
      !    integer, intent(in)           :: ivol 
      !    character(len=*), intent(in)  :: typeequation    
      !  end subroutine jacbevap
      !
      !end interface
  
!c  debug toggle
 
      isdebug = .false. 

!c  loop over boundary control volumes
#ifdef OPENMP
    !$omp parallel                                                    &
    !$omp if (i_matrix_assembly_type_flow == 1)                       &
    !$omp num_threads(numofthreads_matrix_flow)                       &
    !$omp default(shared)                                             &
    !$omp private (i1, ibheat, ibvs, idiag, iend, istart, ivol,       &
    !$omp densinc_ivol, densloc, dheatflux, dtemp_ivol,               &
    !$omp heatflux, heatflux_temp, tempinc_ivol, temploc,             &
    !$omp typebc, viscoinc_ivol, waterflux, tothflux_atm, totwflux_atm)
#endif

#ifdef SCHEDULE_DYNAMIC
    !$omp do schedule(dynamic)
#elif SCHEDULE_STATIC
    !$omp do schedule(static)
#elif SCHEDULE_GUIDED
    !$omp do schedule(guided) 
#else
    !$omp do schedule(auto)
#endif  
      do ibvs = 1,nbvs
        
        !if(.not.bvalid_iabvs(ibvs)) then
        !    cycle
        !end if
        
        ivol = iabvs(ibvs)
 
!c  modify for second type flow boundary condition and
!c  point source boundary
!c  calculate mass flux
 
        if ((btypevs(ibvs).eq.'second') .or.                          &
     &       (btypevs(ibvs).eq.'point') .or.                          &
     &     (btypevs(ibvs).eq.'evaporation'.and.evaporation)) then

          if ((density_dependence).and.(.not.flow_verification)) then
             waterflux = bcondvs(ibvs)
             if (isboussinesq) then
                if (waterflux>r0) then
                     densloc=ssdens(ivol)/density(ivol)
                else
                     densloc=r1
                end if
                                
                bglob(ivol) = bglob(ivol) + densloc*waterflux
             else   
                if (massflux_second) then  
                 bglob(ivol) = bglob(ivol) + waterflux 
                else
                   if (waterflux>r0) then
                     densloc=ssdens(ivol)
                   else
                     densloc=density(ivol)
                   end if  
                   bglob(ivol) = bglob(ivol) + waterflux * densloc 
                end if     
             end if   
        else 
             bglob(ivol) = bglob(ivol) + bcondvs(ibvs) 
          end if

          
!c  modify for first type boundary and zero pressure seepage face
!c  boundary conditions
 
        elseif ((btypevs(ibvs).eq.'first').or.                        &
     &          (btypevs(ibvs).eq.'seepage'.and.                      &
     &           bcondvs(ibvs).lt.r0)) then

          istart = iaglob(ivol)          !pointer - start of row
          iend = iaglob(ivol+1)-1        !pointer - end of row
          idiag = iaglob(ivol)           !pointer - diagonal

          do i1=istart,iend            !modify matrix and rhs
            aglob(i1) = r0
          end do          
      
          aglob(idiag) = r1
 
          bglob(ivol) = r0 
          
          elseif (btypevs(ibvs).eq.'atmospheric'.and.evaporation) then
  
          call jacbevap(ivol,'flow') 
             
        end if                        !(btypevs(ibvs).eq......)

      end do                          !loop - boundary control volumes 
      
#ifdef OPENMP
    !$omp end do
#endif
    
      
!cprovi--------------------------------------------------------------------------------------
!cprovi Boundary conditions for heat equation 
!cprovi--------------------------------------------------------------------------------------      
#ifdef SCHEDULE_DYNAMIC
    !$omp do schedule(dynamic)
#elif SCHEDULE_STATIC
    !$omp do schedule(static)
#elif SCHEDULE_GUIDED
    !$omp do schedule(guided) 
#else
    !$omp do schedule(auto)
#endif
      do ibheat = 1,nbheat
          
        !if(.not.bvalid_iabheat(ibheat)) then
        !    cycle
        !end if
        
        ivol = iabheat(ibheat)
 
!c  modify for second type flow boundary condition and
!c  point source boundary
!c  calculate mass flux
        if (btypeheat(ibheat)=='free') then
          
          heatflux=ddbdflux_energybal(ivol,density(ivol),             &
     &                                viscosity(ivol),                &
     &                                typebc)
          
     
              if (heatflux<r0) then
              
                 temploc=tempnew(ivol)   
                 dtemp_ivol = dinc_heat         
                 tempinc_ivol = tempnew(ivol) + dtemp_ivol
                 idiag = iaglob(ivol+nngl)         
              
              
              
                 if (isboussinesq) then  
               
                    densloc=r1
                    dheatflux=heatflux*(tempinc_ivol-temploc)/dtemp_ivol
                  
                 else !boussinesq
              
                    densloc=density(ivol)
                                                 
                    if (ispitzerdens) then 
                           densinc_ivol =rhonew (ref_dens,            &
     &                            density_pitzer(ivol),               &
     &                            ref_dens,tempinc_ivol,tempref_dens, &
     &                            r1,drho_dt,nonlindens_heat,cdens1,  &
     &                            cdens2,cdens3,cdens4)
                    else
                           densinc_ivol = rhonew (ref_dens,           &
     &                            tds_new(ivol),                      &
     &                            ref_tds,tempinc_ivol,tempref_dens,  &
     &                            drho_dc,drho_dt,nonlindens_heat,    &
     &                            cdens1,cdens2,cdens3,cdens4)
                    end if
                 
                    if (update_viscosity_temp) then
                        viscoinc_ivol = visconew(densinc_ivol,        &
     &                               tds_new(ivol),tempinc_ivol,      &
     &                               update_viscosity,                &
     &                               update_viscosity_temp,           &
     &                               cvisco1,cvisco2,cvisco3,         &
     &                               cvisco4,cvisco5,                 &
     &                               tempref_dens,iviscomodel,        &
     &                               ref_visco,ref_tds,ref_dens)
                    else
                        viscoinc_ivol = viscosity(ivol)
                    end if 
                 
                    heatflux_temp=ddbdflux_energybal(ivol,            &
     &                            densinc_ivol,viscoinc_ivol,         &
     &                            typebc)
                 
                 
                    dheatflux = heatcapw*                             &
     &                     (heatflux_temp*densinc_ivol*tempinc_ivol-  &
     &                      heatflux*densloc*temploc)/dtemp_ivol
     
                 
                 
                  end if !boussinesq
            
              aglob(idiag)=aglob(idiag) - dheatflux
            
           else  !heatflux
          
              temploc=bcondheat(ibheat)
              if (isboussinesq) then  
                densloc=ssdens(ivol)/density(ivol)
              else
                densloc=ssdens(ivol)
              end if
             
           end if !heatflux
          
           if (massflux_second) then  
               heatflux=heatflux*heatcapw*temploc
           else
               heatflux=heatflux*densloc*heatcapw*temploc
           end if !massflux_second
           
           
           bglob(ivol+nngl) = bglob(ivol+nngl) + heatflux
           
        
          
        elseif (btypeheat(ibheat)=='fluxw+') then
          heatflux=ddbdflux_energybal(ivol,density(ivol),             & 
     &                                viscosity(ivol),                &
     &                                typebc)
          if (heatflux<r0) then
            heatflux = r0
            temploc=r0
            densloc=r0
          else
            temploc=bcondheat(ibheat)
            if (isboussinesq) then  
              densloc=ssdens(ivol)/density(ivol)
            else
              densloc=ssdens(ivol)*heatcapw
            end if
          end if
          
          if (massflux_second) then  
            heatflux=heatflux*temploc
          else
            heatflux=heatflux*densloc*temploc 
          end if
       
         bglob(ivol+nngl) = bglob(ivol+nngl) + heatflux 
           
       
            
        else if ((btypeheat(ibheat)=='second') .or.                   &
     &            (btypeheat(ibheat).eq.'point')) then
 
          bglob(ivol+nngl) = bglob(ivol+nngl) + bcondheat(ibheat) 
 
        elseif ((btypeheat(ibheat)=='first').or.                      &
     &          (btypeheat(ibheat)=='seepage'.and.                    &
     &           bcondheat(ibheat)<r0)) then

          istart = iaglob(ivol+nngl)          !pointer - start of row
          iend = iaglob(ivol+nngl+1)-1        !pointer - end of row
          idiag = iaglob(ivol+nngl)           !pointer - diagonal

          do i1=istart,iend            !modify matrix and rhs
            aglob(i1) = r0
          end do          
        
          aglob(idiag) = r1

          bglob(ivol+nngl) = r0 
        
       elseif (btypeheat(ibheat)=='atmospheric') then 
    
          call jacbevap(ivol,'heat')
            
       end if 
        
      end do                          !loop - boundary control volumes
      
#ifdef OPENMP
    !$omp end do
#endif      
      
#ifdef OPENMP
    !$omp end parallel
#endif  
        
!cprovi-------------------------------------------------------------                    
!cprovi-------------------------------------------------------------                    
!cprovi-------------------------------------------------------------   
#ifdef DEBUG
      if (isdebug) then
        write(idbg,*) '---------------------------------------------------'
        write(idbg,*) 'jabobian and residual'
        write(idbg,*) '---------------------------------------------------'
        write(idbg,*) aglob,bglob
      end if
#endif
!cprovi-------------------------------------------------------------                    
!cprovi-------------------------------------------------------------                    
!cprovi-------------------------------------------------------------

      return
      end
