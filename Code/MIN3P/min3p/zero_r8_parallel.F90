!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 221 $
!> $Author: dsu $
!> $Date: 2014-08-05 23:29:49 +0200 (Tue, 05 Aug 2014) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/zero_r8_parallel.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c ----------------------------------------------------------------------
!c subroutine zero_r8_parallel
!c ------------------
!c
!c clear real*8 array
!c
!c written by:      Uli Mayer - May 6, 96
!c
!c last modified:   Uli Mayer - November 27, 96
!c
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c                                                                    I O
!c passed:   real*8:
!c           -------
!c           r8(n1*n2*n3)       = array to be cleared                 + -
!c
!c           integer*4:
!c           ----------
!c           n1                 = first dimension of array            + -
!c           n2                 = second dimension of array           + -
!c           n3                 = third dimension of array            + -
!c
!c common:   -
!c
!c local:    real*8:
!c           -------
!c           r0                 = constant
!c
!c           integer*4:
!c           ----------
!c           nentries           = number of entries        
!c           i                  = counter (number of entries
!c
!c external: -
!c ----------------------------------------------------------------------
 
      subroutine zero_r8_parallel(r8,n1,n2,n3)
 
#ifdef OPENMP
      use omp_lib 
      use gen, only : numofthreads_global, numofloops_thred_global
#endif 

      implicit none 
      
      integer :: nentries, n1, n2, n3, i

      real*8 r8(*), r0

      parameter (r0 = 0.0d0)
 
      nentries = n1*n2*n3

#ifdef OPENMP
    !$omp parallel                                                    &
    !$omp if (nentries > numofloops_thred_global)                     &
    !$omp num_threads(numofthreads_global)                            &
    !$omp default(shared)                                             &
    !$omp private (i)  
    !$omp do schedule(static)
#endif
      do i=1,nentries
        r8(i) = r0
      end do
#ifdef OPENMP
    !$omp end do
    !$omp end parallel
#endif
 
      return
      end
