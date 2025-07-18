!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 221 $
!> $Author: dsu $
!> $Date: 2014-08-05 23:29:49 +0200 (Tue, 05 Aug 2014) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/storvs.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c ----------------------------------------------------------------------
!c real*8 function storvs
!c ------------------------
!c
!c compute mass storage term for variably saturated conditions
!c
!c written by:      Uli Mayer - May 29, 96
!c
!c last modified:   Uli Mayer - November 25, 96
!c
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c                                                                    I O
!c passed:   real*8:
!c           ------- 
!c           psinew             = pressure head (new time level)      + -
!c           psiold             = pressure head (old time level)      + -
!c           satwnew            = water saturation (new time level)   + -
!c           satwold            = water saturation (old time level)   + -
!c           storvs             = mass storage term                   * +
!c
!c           integer*4:
!c           ----------
!c           izn                = pointer (material property zone)    + -
!c
!c common: 
!c phys.f:   spstor(nzn)        = specific storage coefficient        + -
!c           por                = porosity                            + -
!c           nzn                = number of zones                     + -
!c
!c local:    -
!c
!c external: -
!c ----------------------------------------------------------------------
 
      real*8 function storvs(psinew,psiold,satwnew,satwold,izn,       &
     &                       stor_ivol)
                                                                       
      use parm                                                         
      use phys
                                                                       
      implicit none
      
      real*8 :: psinew,psiold,satwnew,satwold,stor_ivol
      integer :: izn
                                                                       
      storvs = stor_ivol*satwnew*(psinew-psiold)                      &
     &       + por*(satwnew-satwold)
  
      return
      end
