!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/i2upfind.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c ----------------------------------------------------------------------
!c subroutine i2upfind
!c -------------------
!c
!c assign pointers to second upstream point for flux limiter
!c
!c written by:      Uli Mayer - February 10, 98
!c
!c last modified:   -
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
!c           cinfrt_va(njavs)   = influence coefficients              + -
!c                                (advection - aqueous phase)
!c
!c           integer*4:
!c           ----------
!c           i2up(nn)           = pointer array to second upstream    * +
!c                                point
!c           iavs(nn+1)         = row pointer array for avs           + -
!c           idbg               = unit number - debugging file        + -
!c           javs(njavs)        = connectivity list                   + -
!c           nn                 = total number of control volumes     + -
!c     
!c
!c local:    real*8:
!c           -------
!c           cinvrt_va_max      = max. inflow in control volume ivol
!c                                (Note: signs are negative)
!c           r0                 = constant
!c
!c           integer*4:
!c           ----------
!c           i1                 = counter (entries in ja, a arrays 
!c                                         for 1d-scalar matrix)
!c           icon               = pointer (off-diagonal connections)
!c           istart             = pointer (first off diagonal entry 
!c                                         in row for 1d-scalar 
!c                                         matrix)
!c           istop              = pointer (last off-diagonal entry 
!c                                         in row for 1d-scalar 
!c                                         matrix)
!c           ivol               = counter (control volumes)
!c           jvol               = pointer (column in 1d-scalar 
!c                                         matrix)
!c           info_debug         = 0 -> no debugging information
!c                              = 1 -> write debugging information to
!c                                     prefix_o.dbg
!c                              = 2 -> write debugging information to
!c                                     prefix_o.dbg and quit
!c
!c external: -
!c ----------------------------------------------------------------------
 
      subroutine i2upfind
 
      use parm
      use gen
#ifdef OPENMP
      use omp_lib 
#endif
#ifdef PETSC
      use petsc_mpi_common, only : petsc_mpi_finalize
#endif

      implicit none

      integer :: i1, ivol, istart, istop, jvol, info_debug
      
      real*8 :: cinfrt_va_max
      
      real*8, parameter :: r0 = 0.0d0

!c  find second upstream point i2up based on the maximum advective flux
!c  into control volume ivol
#ifdef OPENMP
    !$omp parallel                                                    &
    !$omp if (nngl > numofloops_thred_i2upfind_1)                     &
    !$omp num_threads(numofthreads_global)                            &
    !$omp default(shared)                                             &
    !$omp private(i1, istart, istop, ivol, jvol,                      &
    !$omp cinfrt_va_max)                  
    !$omp do schedule(static)
#endif
      do ivol=1,nngl             !loop over control volumes

!c  initialize pointer to second upstream point

        i2up(ivol) = 0

!c  get row pointers

        istart = iavs(ivol)+1        !start - off-diagonal connections
        istop = iavs(ivol+1)-1       !end   - off-diagonal connections

!c  compute fluxes between current control volume and adjacent
!c  control volumes and total flux into current control volume

        cinfrt_va_max = r0           !initialize max. influx

        do i1 = istart,istop         !off-diagonal connections

          jvol = javs(i1)            !column pointer

!c  define second upstream point
!c  max. influx has here a negative sign

          if (cinfrt_va(i1).lt.cinfrt_va_max) then
            cinfrt_va_max = cinfrt_va(i1)
            i2up(ivol) = jvol
          endif

        end do                   !off-diagonal connections

      end do                  !loop over control volumes
#ifdef OPENMP
    !$omp end do
    !$omp end parallel
#endif 

!cdbg activate this section for debugging
#ifdef DEBUG
      info_debug = 0
      if (info_debug.gt.0) then
        do ivol=1,nngl 
           write(idbg,'(2(a,1x,i6))') 'ivol = ',ivol,     &
                                     ' i2up = ',i2up(ivol)
        end do
      end if
      if (info_debug.gt.1) then
#ifdef PETSC
        call petsc_mpi_finalize
#endif
        stop
      end if
#endif
!cdbg
      return
      end
