!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/msysvs.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c ----------------------------------------------------------------------
!c subroutine msysvs
!c -----------------
!c
!c total system mass (variably saturated flow)
!c
!c written by:      Uli Mayer - July 16, 96
!c
!c last modified:   Tom Henderson - September 24, 2002
!c                  Sergi Molins - May 2, 2006
!c                  added skip, nskip variables
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
!c           cvol(nn)           = nodal volumes                       + -
!c           pornew(nn)         = porosity                            + -
!c           sanew(nn)          = aqueous phase saturation            + -
!c                                - new time level
!c           relperm(nn)        = relative permeability               + -
!c           uvsnew(nn)         = solution vector (new time level)    + -
!c           time_io            = current solution time (I/O units)   + -
!c           totvsmass          = total system mass                   * +
!c
!c           integer*4:
!c           ----------
!c           mpropvs(nn)        = pointer array for allocation of     + -
!c                                material properties
!c           mtime              = curent time step                    + -
!c           nn                 = total number of control volumes     + -
!c           imvs               = unit number, mass balance -         * +
!c                                             variably saturated
!c                                             flow
!c           imvs_first         = pointer - first unit number for     + -
!c                                mass balance - variably saturated
!c                                               flow
!c
!c           skip               = number of skipped timesteps in logf + -
!c           nskip              = counter of skipped timesteps        + -
!c
!c           logical:
!c           --------
!c           fully_saturated    = .true.  -> saturated conditions     + -
!c           variably_saturated = .true.  -> .not.fully_saturated,    + -
!c                                        -> variably saturated
!c                                           conditions
!c
!c local:    real*8:
!c           -------
!c           phead_vol          = local pressure head for fully
!c                                saturated conditions
!c           r0                 = constant
!c           r1                 = constant
!c           totv_a             = total volume (aqueous phase)
!c           totv_g             = total gas (gaseous phase)
!c           vsmass             = system mass (local)
!c           dens_h2o           = water density (kg/m^3) 
!c
!c           integer*4:
!c           ----------
!c           ivol               = counter (control volumes)
!c
!c external: storvs   = storage function for variably saturated 
!c                      flow
!c ----------------------------------------------------------------------

      subroutine msysvs

      use parm
      use gen
      use writeversion
#ifdef OPENMP
      use omp_lib 
#endif 

      use module_binary_mpiio, only : binary_write_data
      
      implicit none
#ifdef PETSC_V3_6_X
#include <petsc/finclude/petscsys.h>
#elif PETSC
#include <finclude/petscsys.h>
#endif    

      integer :: ivol
      real*8 :: totv_a, totv_g, vsmass, storvs, phead_vol

#ifdef PETSC
      real*8 :: totvsmass_gbl, totv_a_gbl, totv_g_gbl
      PetscErrorCode :: ierrcode
#endif

      external storvs

      real*8, parameter :: r0 = 0.0d0, r1 = 1.0d0, dens_h2o = 1.0d+3
      
      integer :: nvarsimvs

      vsmass= r0

!c  compute total system mass
!c  variably saturated conditions:
!c  replace uvsold and saold by 0 -> can use storage function 

      totvsmass = r0

!c  compute total volumes for aqueous and gaseous phase
      totv_a = r0
      totv_g = r0
      
#ifdef OPENMP
    !$omp parallel                                                    &
    !$omp if (nngl > numofloops_thred_msysvs_1)                       &
    !$omp num_threads(numofthreads_global)                            &
    !$omp default(shared)                                             &
    !$omp private(ivol, phead_vol, vsmass)                            &
    !$omp reduction(+:totvsmass, totv_a, totv_g)
    !$omp do schedule(static)
#endif 
      do ivol = 1,nngl   
#ifdef PETSC
        if(node_idx_lg2l(ivol) < 0) then
            cycle
        end if
#endif
          
        if (variably_saturated) then
          vsmass = cvol(ivol) * dens_h2o                              &
               * storvs(uvsnew(ivol),r0,sanew(ivol),r0,mpropvs(ivol), &
                 stor(ivol))
        elseif (fully_saturated) then

!c  fully saturated conditions
!c  replace uvsold and saold by 0 and pass pressure head 
!c  instead of hydraulic head
!c  -> can use storage function for variably saturated flow 

          phead_vol = uvsnew(ivol)-zg(ivol)    !need pressure head here
          vsmass = cvol(ivol) * dens_h2o                              &
               * storvs(phead_vol,r0,sanew(ivol),r0,mpropvs(ivol),    &
                stor(ivol))
        end if
        totvsmass = totvsmass + vsmass
        
        !Put totv_a and totv_g here to reduce overhead by OpenMP
        totv_a = totv_a + sanew(ivol)*pornew(ivol)*cvol(ivol)
        totv_g = totv_g + (r1-sanew(ivol))*pornew(ivol)*cvol(ivol)
        
      end do
#ifdef OPENMP
    !$omp end do
    !$omp end parallel
#endif

#ifdef PETSC
      call MPI_Allreduce(totvsmass, totvsmass_gbl,1,MPI_REAL8,MPI_SUM, &
                         Petsc_Comm_World,ierrcode)
      CHKERRQ(ierrcode)
      totvsmass = totvsmass_gbl
      call MPI_Allreduce(totv_a, totv_a_gbl,1,MPI_REAL8,MPI_SUM,       &
                         Petsc_Comm_World,ierrcode)
      CHKERRQ(ierrcode)
      totv_a = totv_a_gbl
      call MPI_Allreduce(totv_g, totv_g_gbl,1,MPI_REAL8,MPI_SUM,       &
                         Petsc_Comm_World,ierrcode)
      CHKERRQ(ierrcode)
      totv_g = totv_g_gbl      
#endif

 
!c  write total contributions to file   

      imvs = imvs_first

      if(rank == 0 .and. b_enable_output .and.                         &
         .not.((skip_time.gt.0).and.(nskip_time.lt.skip_time))) then
        if (b_output_binary) then
          nvarsimvs = 4
          realbuffer_gb(1:nvarsimvs) = (/time_io,totvsmass,totv_a,  &
                                         totv_g/)
          call binary_write_data(imvs_mpi(imvs), 1,         &
                       (/mtime/),offset_imvs_ijk(imvs),.true.)      
          call binary_write_data(imvs_mpi(imvs), nvarsimvs, &
                       realbuffer_gb,offset_imvs(imvs),.true.) 

          offset_imvs(imvs) = offset_imvs(imvs) + nvarsimvs*nfloatbit

        else
          write(imvs,'(4(1pe12.4))') time_io,totvsmass,totv_a,totv_g
        end if
      end if

      return
      end 
