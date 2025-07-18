!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 221 $
!> $Author: dsu $
!> $Date: 2014-08-05 23:29:49 +0200 (Tue, 05 Aug 2014) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/chrgcorr.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c -----------------------------------------------------------------------
!c subroutine chrgcorr
!c -------------------
!c correct total aqueous component concentration for component specified
!c to satisfy charge balance
!c
!c written by:      Uli Mayer - February 24, 98
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
!c chem.f:   real*8:
!c           -------
!c           chargec(nc)        = charge of free species              + -
!c           totcn(n,nthreads)  = total aqueous component             + -
!c                                concentrations
!c                                - new time level [moles/l water]
!c           totco(n,nthreads)  = total aqueous component             + +
!c                                concentrations
!c                                - old time level [moles/l water]
!c
!c           integer*4:
!c           ----------
!c           nc                 = number of components including h2o  + -
!c
!c           character:
!c           ----------
!c           ctype(nc-1)        = 'charge' = correct total aqueous    + -
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
!c
!c local:    real*8:
!c           -------
!c           delta_totc         = correction for total aqueous
!c                                component concentration
!c           r0                 = constant
!c           rhalf              = constant
!c           r2                 = constant
!c           zbal               = charge balance
!c           zpos               = sum of positive charge
!c           zneg               = sum of negative charge
!c   
!c           integer*4:
!c           ----------
!c           ic                 = counter (components)
!c
!c external: -  
!c ----------------------------------------------------------------------
  
      subroutine chrgcorr
 
      use parm
      use chem
#ifdef OPENMP
      use omp_lib 
#endif 
 
      implicit none
      
      integer :: ic
      
      real*8 :: zpos, zneg, zbal, delta_totc

      real*8, parameter :: r0 = 0.0d0, rhalf = 0.5d0, r2 = 2.0d0
      
      integer :: tid
      
#ifdef OPENMP
      tid = omp_get_thread_num() + 1
#else
      tid = 1
#endif
 
      zpos = 0.0d0
      zneg = 0.0d0
      
  
!c  compute absolute charge balance error (unspeciated)

      do ic=1,nc-1
        if (chargec(ic).gt.r0) then
          if (ctype(ic).ne.'fixed') then
            zpos = zpos + chargec(ic) * totco(ic,tid)
          elseif (ctype(ic).eq.'fixed') then
            zpos = zpos + chargec(ic) * totcn(ic,tid)
          end if
        elseif (chargec(ic).lt.r0) then
          if (ctype(ic).ne.'fixed') then
            zneg = zneg - chargec(ic) * totco(ic,tid)
          elseif (ctype(ic).eq.'fixed') then
            zneg = zneg - chargec(ic) * totcn(ic,tid)
          end if
        end if
      end do

      zbal = zpos - zneg

!c  correct total aqueous component concentration for selected
!c  component to satisfy charge balance

      do ic = 1,nc-1

        if (ctype(ic).eq.'charge') then

!c  determine concentration correction

          delta_totc = zbal/chargec(ic)

!c  apply correction and constrain magnitude of correction

          if (delta_totc .lt. rhalf*totco(ic,tid).and.                &
             delta_totc .gt. -totco(ic,tid)) then
            totco(ic,tid) = totco(ic,tid) - delta_totc
          elseif (delta_totc .gt. rhalf*totco(ic,tid)) then
            totco(ic,tid) = rhalf * totco(ic,tid)
          elseif (delta_totc .lt. -totco(ic,tid)) then
            totco(ic,tid) = r2 * totco(ic,tid)
          end if

        end if
      end do

      return
      end
