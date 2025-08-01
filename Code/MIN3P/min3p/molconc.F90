!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/molconc.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c ----------------------------------------------------------------------
!c subroutine molconc
!c ------------------
!c
!c compute average molar concentration of organic mixture
!c
!c written by:      Uli Mayer - January 17, 00
!c
!c last modified:   
!c
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c                                                                    I O
!c passed:   real*8:
!c           -------
!c           phim(nm)           = volume fractions of minerals        + -
!c
!c
!c common:
!c chem.f:   real*8:
!c           -------
!c           conc_mol_avg(nthreads)
!c                              = average molar concentration of      + -
!c                                organic mixture 
!c           densm(nm)          = density of free phase product       + -
!c           gfwm(nm)           = gram formula weight of free phase   + -
!c                                product
!c
!c           integer*4:
!c           ----------
!c           nm                 = number of minerals specified        + -
!c
!c           character:
!c           ----------
!c           reaction_type(nm)  = 'reversible'                        + -
!c                                'dissolution_to_equilibrium'
!c                                'precipitation_to_equilibrium'
!c                                'dissolution_far_from_equilibrium'
!c                                'precipitation_far_from_equilibrium'
!c                                'monod'
!c                                'raoult'
!c
!c
!c local:    real*8:
!c           -------
!c           r0                = constant
!c
!c           integer*4:
!c           ----------
!c           im                = counter
!c
!c external: -
!c ----------------------------------------------------------------------
  
      subroutine molconc(phim)
 
      use parm
      use chem
#ifdef OPENMP
      use omp_lib 
#endif 
 
      implicit none
      
      real*8 :: phim
      
      integer :: tid, im
      
      real*8 :: conc_mol_add

      dimension phim(*)

      real*8, parameter :: r0 = 0.0d0, r2 = 2.0d0, huge = 1.0d300   
      
#ifdef OPENMP
      tid = omp_get_thread_num() + 1
#else
      tid = 1
#endif
          
!c  compute average molar concentration for organic mixture

      conc_mol_avg(tid) = r0
      conc_mol_add = huge
   
      do im = 1,nm
   
        if (reaction_type(im).eq.'raoult') then
          conc_mol_avg(tid) = conc_mol_avg(tid)                       &
     &                 + phim(im)*densm(im)/gfwm(im)
        conc_mol_add = dmin1(conc_mol_add,eqm(im,tid))
        end if
   
      end do

!cff1d      conc_mol_avg(tid) = conc_mol_avg(tid) + 1.0d-7            !2D and 3D
      conc_mol_avg(tid) = conc_mol_avg(tid) + r2*conc_mol_add    !1D 
               
      return
      
      end
