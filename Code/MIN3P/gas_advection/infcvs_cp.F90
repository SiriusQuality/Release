!c ----------------------------------------------------------------------
!c subroutine infcvs_cp
!c --------------------
!c
!c compute influence coefficients for variably saturated flow
!c in terms of conductivies (input ones are in terms of permeabilities)
!c
!c influence coeff (aqueous phase) = (A/L)*k*(dens*g/visc) 
!c influence coeff (gaseous phase) = (A/L)*k 
!c
!c note: density and viscosity are assumed constant. This can be modified.
!c
!c written by:      Sergi Molins - January 20,2003
!c
!c last modified:   
!c
!c
!c modified by: Danyang Su - March 31, 2014
!c
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c                                                                    I O
!c passed:   -
!c
!c gen.f     integer*4: 
!c           ----------
!c           njavs              = number of global connections        + -
!c
!c           real*8:
!c           -------
!c           gacc               = gravitational acceleration [m s^-2] + -
!c           cinfvs_g(njavs)    = influence coeff gas phase           * +
!c           cinfvs_a(njavs)    = influence coeff aqueous ph          * +
!c           cinfvs(njavs)      = influence coeff (permeabilities)    + -
!
!c phys.f:   real*8:
!c           -------
!c           dens_h2o           = density of water [kg m^-3]
!c           visc_h2o           = dynamic viscosity (water)
!c                                [Pa s] = [kg m^-1 s^-1]
!c local:    integer*4:
!c           ----------
!c           i1                 = counter
!c
!c ----------------------------------------------------------------------
 
      subroutine infcvs_cp

      use gen, only: njavs, cinfvs_g, cinfvs, gacc,                   &
                     numofloops_thred_global, numofthreads_global
      use phys, only : dens_h2o, visc_h2o

      implicit none

      integer*4 :: i1
      
      real*8 :: rcvt
      
      rcvt = visc_h2o/dens_h2o/gacc

!c     loop over connections
#ifdef OPENMP
    !$omp parallel                                                    &
    !$omp if (njavs > numofloops_thred_global)                        &
    !$omp num_threads(numofthreads_global)                            &
    !$omp default(shared)                                             &
    !$omp private (i1)    
    !$omp do schedule(static)
#endif
      do i1 = 1,njavs

!c      gas phase flow
!c      note: In the gas advection model, intrinsic permeability k[m^2]
!c      is used in the model (Molins and Mayer, WRR, 2007)
        cinfvs_g(i1) = cinfvs(i1) * rcvt
        
        !check this part with dusty model

      end do
#ifdef OPENMP
    !$omp end do
    !$omp end parallel
#endif

      return
      
      end
