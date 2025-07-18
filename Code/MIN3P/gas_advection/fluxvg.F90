!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/gas_advection/fluxvg.F90 $
!---------------------------------------------------------------------
!********************************************************************!
    
!c ----------------------------------------------------------------------
!c real*8 function fluxvg
!c ------------------------
!c
!c compute advective mass flux of ideal gases
!c
!c written by:  Sergi Molins - May 2,2006  
!c
!c modified by: Danyang Su - March 26, 2014
!c
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c                                                                    I O
!c passed:   real*8:
!c           -------
!c           gpi                = gas pressure at control volume i    + -
!c           gpj                = gas pressure at control volume j    + -
!c           zgi                = elevation at control volume i       + -
!c           zgj                = elevation at control volume j       + -
!c           totg_ij            = gaseous species concentrations      + -
!c                                for components at interface i-j     + -
!c           relp               = relative permeability               + -
!c           dens               = gas density at interface i-j        + -
!c           visc               = gas viscosity at interface i-j      + -
!c           cinfvs_gx          = influence coefficients for          + -
!c                                gas advection              
!c           gacc               = gravity m s^-2                      + -
!c           fluxvg             = advective flux                      * +
!c
!c           integer*4:
!c           ----------
!c           i                  = pointer to control volume i         + -
!c           j                  = pointer to control volume j         + -
!c           ic                 = pointer to current component        + -
!c
!c
!c           logical:
!c           --------
!c           gas_gravity        = enable gas gravity term             + -
!c                                for advection
!c gen.f:    real*8:
!c           -------
!c           gacc               = gravity m s^-2                      + -
!c
!c common:   -
!c
!c local:    real*8:
!c           -------
!c           grad             = gradient
!c
!c external: -  
!c ----------------------------------------------------------------------
 
      real*8 function fluxvg(gpi       ,gpj       , &
     &                       zgi       ,zgj       , &
     &                       totg_ij   ,relp      , &
     &                       dens      ,visc      , &
     &                       cinfvs_gx,             &
     &                       gas_gravity, gacc)
      
      
      implicit none

!c  passed
      real*8       gpi       ,gpj       ,           &
     &             zgi       ,zgj       ,           &
     &             totg_ij   ,relp      ,           &
     &             dens      ,visc      ,           &
     &             cinfvs_gx            ,           &
     &             gacc
      
      logical :: gas_gravity

!c  local 
      real*8       grad    
      
!c     compute gradient
      grad = - (gpj - gpi) !sign
      
      if (gas_gravity) then

        grad = grad - dens * gacc * (zgj - zgi)   

      endif

!c     compute specific discharge
      fluxvg = cinfvs_gx * relp * grad / visc * totg_ij

      return
      end