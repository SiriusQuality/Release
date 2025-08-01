!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/updatevs.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c ----------------------------------------------------------------------
!c subroutine updatevs
!c -------------------
!c
!c update pressure head and check for convergence
!c (variably saturated flow)
!c compute underrelaxation factor for variably saturated flow
!c update solution vector and secondary variables - check for convergence
!c if relaxation factor is less than one, updates are scaled and the
!c convergence check is based on the scaled updates
!c
!c written by:      Uli Mayer - July 3, 96
!c
!c last modified:   Uli Mayer - November 28, 96 
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
!c           hhead(nn)          = hydraulic head                      * +
!c           relfacold          = underrelaxation factor              + +
!c                                (old time level)
!c           srelfac_vs         = user specified underrelaxation      + -
!c                                factor
!c           uvs(nn)            = update towards solution-vector      + +
!c           uvsnew(nn)         = solution vector (new time level)    + +
!c           uvslim             = user specified upper limit for      + -
!c                                magnitude of solution update        + -
!c           uvsmaxold          = maximum solution update             + +
!c                                (old time level)
!c           zg(nn)             = spatial coordinates in z-direction  + -
!c
!c           integer*4:
!c           ----------
!c           ilog               = unit number, log file               + -
!c           nn                 = total number of control volumes     + -
!c           iter_vs            = iteration counter                   + -
!c                                (variably saturated flow)
!c           itsolv             = actual number of solver iterations
!c           idetail_vs         = solver information level            + -
!c
!c           logical:
!c           --------
!c           comp_relax         = .true.  -> compute underelaxation   + -
!c                                           factor
!c           not_converged      = .true.  -> continue Newton          + +
!c                                           iteration
!c           under_relax        = .true.  -> underrelaxation          + -
!c
!c local:    real*8:
!c           -------
!c           r0                 = constant
!c           r1                 = constant
!c           r3                 = constant
!c           rhalf              = constant
!c           relfac             = underrelaxation factor
!c           sfac               = intermediate value for 
!c                                computation of relaxation factor
!c           ufac               = intermediate dampening factor
!c           uvsmax             = maximum solution update
!c           uvsabs             = absolute value of solution update
!c
!c           integer*4:
!c           ----------
!c           ivol               = counter (control volumes)
!c           maxvol             = control volume with maximum 
!c                                solution update
!c           nexvol             = number of control volumes 
!c                                exceeding update tolerance
!c
!c external: -
!c ----------------------------------------------------------------------

      subroutine updatevs 

      use parm
      use gen
#ifdef OPENMP
      use omp_lib 
#endif 

      implicit none
#ifdef PETSC_V3_6_X
#include <petsc/finclude/petscsys.h>
#elif PETSC
#include <finclude/petscsys.h>
#endif
      
      real*8 :: uvsmax, relfac, sfac, ufac, uvsabs
      integer :: nexvol, maxvol, ivol, i1
      
#ifdef PETSC
      real*8 :: uvsmax_gbl,uvsmax_gbl2
      DOUBLE PRECISION :: mpireduce_in(2), mpireduce_out(2)
      integer*4 :: nexvol_gbl, mpireduce_irank
      PetscErrorCode :: ierrcode
#endif

      real*8, parameter :: rhalf = 0.5d0,r0 = 0.0d0,r1 = 1.0d0,r3 = 3.0d0
      
      integer :: tid

      relfac = r0

      if (under_relax) then          !underrelaxation

        if (comp_relax) then         !compute underelaxation factor

!c  find maximum update to calculate relaxation factor
!c  according to Cooley's method (1983)

          uvsmax    = r0
          
#ifdef OPENMP
          maxval_omp = uvsmax
#endif

#ifdef OPENMP
    !$omp parallel                                                    &
    !$omp if (nngl > numofloops_thred_updatevs_1)                     &
    !$omp num_threads(numofthreads_global)                            &
    !$omp default(shared)                                             &
    !$omp private (ivol, tid)                                              
    !$omp do schedule(static)
#endif   
          do ivol = 1,nngl 
!#ifdef PETSC
!            if(node_idx_lg2l(ivol) < 0) then
!                cycle
!            end if
!#endif
              
#ifdef OPENMP
            tid = omp_get_thread_num()+1  
            if (dabs(uvs(ivol)).gt.dabs(maxval_omp(tid))) then
                maxval_omp(tid) = uvs(ivol)
            end if
#else          
            if (dabs(uvs(ivol)).gt.dabs(uvsmax)) then
                uvsmax = uvs(ivol)
            end if
#endif        
          end do
#ifdef OPENMP
    !$omp end do
    !$omp end parallel
#endif

#ifdef OPENMP
          i1 = maxloc(abs(maxval_omp), 1)
          uvsmax = maxval_omp(i1)       
#endif

#ifdef PETSC
          call MPI_Allreduce(uvsmax, uvsmax_gbl,1,MPI_REAL8,MPI_MAX,   &
                             Petsc_Comm_World,ierrcode)
          CHKERRQ(ierrcode)
          call MPI_Allreduce(uvsmax, uvsmax_gbl2,1,MPI_REAL8,MPI_MIN,  &
                             Petsc_Comm_World,ierrcode)
          CHKERRQ(ierrcode)
          if(abs(uvsmax_gbl) > abs(uvsmax_gbl2)) then
              uvsmax = uvsmax_gbl
          else
              uvsmax = uvsmax_gbl2
          end if
#endif

!c  compute underrelaxation factor

          if (iter_vs.eq.1) then                 !first iteration

            relfac = r1

          else                                   !following iterations

            sfac = uvsmax/(uvsmaxold*relfacold)            !step 1

            if (sfac.lt.-r1) then                          !step 2
              relfac = rhalf/dabs(sfac)
            else
              relfac = (r3 + sfac)/(r3 + dabs(sfac))
            endif

          end if

!c  limit to maximum allowed update

          ufac = relfac*dabs(uvsmax)/uvslim                !step 3
          if (ufac.gt.r1) then
            relfac = relfac/ufac
          end if

!c  assign old max. update and relaxation factor for next time step

          uvsmaxold = uvsmax
          relfacold = relfac

        else                      !user specified underelaxtion factor

          relfac = srelfac_vs

        end if       !comp_relax
      end if         !under_relax

!c  check size of updates (relaxed/un-relaxed) to determine convergence

      uvsmax  = r0
      nexvol = 0

#ifdef OPENMP
      maxval_omp = uvsmax      
#endif 

#ifdef OPENMP
    !$omp parallel                                                    &
    !$omp if (nngl > numofloops_thred_updatevs_2)                     &
    !$omp num_threads(numofthreads_global)                            &
    !$omp default(shared)                                             &
    !$omp private (ivol, tid, uvsabs)                                 &
    !$omp reduction(+:nexvol)
    !$omp do schedule(static)
#endif 
      do ivol = 1,nngl  
!#ifdef PETSC
!        if(node_idx_lg2l(ivol) < 0) then
!            cycle
!        end if
!#endif
          
#ifdef OPENMP    
        tid = omp_get_thread_num()+1
#else
        tid = 1
#endif 
          
        if (under_relax) then                    !underrelaxation
          uvs(ivol) = relfac*uvs(ivol)
        end if

        uvsnew(ivol) = uvsnew(ivol)+uvs(ivol)    !update primary unknown
        hhead(ivol) = uvsnew(ivol)+zg(ivol)      !and hydraulic head
        
!#ifdef DEBUG
!        if(ivol == 14) then
!            write(idbg,'(3(a,1x,e,1x))')                               &
!                  "-->updatevs hhead(ivol)", hhead(ivol),              &
!                  "uvsnew(ivol)",uvsnew(ivol),"zg(ivol)",zg(ivol)
!        end if
!#endif

        uvsabs  = dabs(uvs(ivol))
        
#ifdef OPENMP
        if (uvsabs.gt.maxval_omp(tid)) then
          maxval_omp(tid) = uvsabs               !max solution update
          maxvol_omp(tid) = ivol                 !location of max update
        endif
#else
        if (uvsabs.gt.uvsmax) then
          uvsmax = uvsabs                        !max solution update
          maxvol = ivol                          !location of max update
        endif
#endif

        if (uvsabs.gt.tol_vs) then               !number of volumes
          nexvol = nexvol + 1                    !exceeding convergence
        end if                                   !tolerance

      end do        !loop over control volumes
#ifdef OPENMP
    !$omp end do
    !$omp end parallel
#endif

#ifdef OPENMP
      i1 = maxloc(maxval_omp, 1)
      uvsmax = maxval_omp(i1)
      maxvol = maxvol_omp(i1)
#endif

#ifdef PETSC
      mpireduce_in(1) = uvsmax      !returns the reduced value
      mpireduce_in(2) = rank        !returns the rank of process that owns it
      call MPI_Allreduce(mpireduce_in, mpireduce_out, 1,               &
                         MPI_2DOUBLE_PRECISION,MPI_MAXLOC,             &
                         Petsc_Comm_World,ierrcode)
      CHKERRQ(ierrcode)
      uvsmax = mpireduce_out(1)
      mpireduce_irank = int(mpireduce_out(2))
      
      call MPI_BCAST(maxvol, 1, MPI_INTEGER4, mpireduce_irank,         &
                     Petsc_Comm_World, ierrcode) 
      CHKERRQ(ierrcode)
      
      call MPI_Allreduce(nexvol, nexvol_gbl,1,MPI_INTEGER4,MPI_SUM,    &
                         Petsc_Comm_World,ierrcode)
      CHKERRQ(ierrcode)
      nexvol = nexvol_gbl
#endif

!c  write convergence history to screen or log file

       if (idetail_vs.gt.0 .and. rank == 0 .and. b_enable_output) then
        if (iter_vs.eq.1.or.idetail_vs.eq.2) then
          write(ilog,'(a)') ' Newton Iteration Convergence Summary:'
          write(ilog,'(2a)')' Newton       maximum      maximum    ',  &
                            ' solver' 
#ifdef PETSC          
          write(ilog,'(2a)')' iteration    update       residual   ',  &
                            ' iterations  maxvol      nexvol     rank' 
#else
          write(ilog,'(2a)')' iteration    update       residual   ',  &
                            ' iterations  maxvol      nexvol' 
#endif
        end if
#ifdef PETSC
        write(ilog,'(i6,6x,1pd11.4,2x,1pd11.4,3(i9,3x),i6)')           &
              iter_vs,uvsmax,rnorm,itsolv,maxvol,nexvol,mpireduce_irank
#else
        write(ilog,'(i6,6x,1pd11.4,2x,1pd11.4,3(i9,3x))')              &
              iter_vs,uvsmax,rnorm,itsolv,maxvol,nexvol
#endif
      end if

!c  final convergence check
      if (uvsmax.lt.tol_vs) then
        if (abs(relbalance_vs) .le. rtol_relbalance_vs .or.            &
            abs(absbalance_vs) .le. rtol_absbalance_vs) then

          not_converged = .false.

        end if
      end if

      return
      end
