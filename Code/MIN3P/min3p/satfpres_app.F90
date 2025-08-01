!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 268 $
!> $Author: dsu $
!> $Date: 2015-01-09 17:00:41 -0800 (Fri, 09 Jan 2015) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/dsu_isotope/src/solver/ws209.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c ----------------------------------------------------------------------
!c real*8 function satfpres
!c ------------------------
!c  
!c compute apparent saturation from pressure using van Genuchten relationship
!c
!c taken from Joel VanderKwaak's Water Transport Simulator (WTS)
!c Version 1.0, March, 1996 
!c 
!c written by:      Joel VanderKwaak - March 1996
!c
!c last modified:   Rich Amos - Nov 17, 2004
!c
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c                                                                    I O
!c passed:   real*8:
!c           -------
!c           swr                = residual water saturation           + -
!c           aentry             = air entry pressure                  + -
!c           pressure           = nodal pressure                      + -
!c           spalpha            = soil hydraulic parameter            + -
!c           spbeta             = soil hydraulic parameter            + -
!c           spgamma            = soil hydraulic parameter            + -
!c           satfpres           = water saturation                    - +
!c
!c common:   -  
!c
!c local:    real*8:
!c           -------
!c           se                 = effective saturation
!c           dp                 = pressure head difference
!c           press              = intermediate value
!c           r1                 = constant
!c
!c external: -
!c ----------------------------------------------------------------------

      real*8 function satfpres_app(aentry,pressure,spalpha,spbeta,     &
                                   spgamma)

      implicit none
      
      real*8 :: aentry,pressure,spalpha,spbeta,spgamma
      real*8 :: press, dp 

      real*8, parameter :: r0 = 0.0d0, r1 = 1.0D0

      if (pressure.lt.r0)then
        dp = pressure-aentry
        press = spalpha*dabs(dp)
        satfpres_app  = (r1+(press)**spbeta)**(-spgamma)
      else
        satfpres_app = r1
      end if

      return
      end
