!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 3 $
!> $Author: dsu $
!> $Date: 2012-12-10 03:46:07 +0100 (Mon, 10 Dec 2012) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/int_h_ovendry.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c----------------------------------------------------------------------
!c subroutine int_h_ovendry
!c Compute the integral for oven-dryness model
!c Fayer and Simmons (1985) 
!c----------------------------------------------------------------------
real*8 function int_h_ovendry(swr,alpha,n,m,beta,pressure,pressurec,pressure0,pressurem,aentry,w0) 

implicit none 

real*8, intent(in)    :: swr                ! Residual liquid saturation 

real*8, intent(in)    :: alpha              ! Van Genuchten parameter 

real*8, intent(in)    :: pressure 

real*8, intent(in)    :: pressure0 

real*8, intent(in)    :: w0

real*8, intent(in)    :: pressurem 

real*8, intent(in)    :: pressurec

real*8, intent(in)    :: aentry 

real*8, intent(in)    :: beta

real*8, intent(in)    :: n                 ! Van Genuchten parameter 

real*8, intent(in)    :: m                 ! Van Genuchten parameter 
!c-------------------------------------------------------------------------------------------
!c Local variables  
!c-------------------------------------------------------------------------------------------
real*8                :: w, &
                         wc, &
                         wm, &
                         w0_loc, &
                         gamma, &
                         int1_w, &
                         int2_w, &
                         int3_w, &
                         int3b_w, &
                         int3b_w0, &
                         int3a_w, &
                         alphapress0, &
                         alphapressc, &
                         alphapressm, &
                         betapress0, &
                         betapressm, &
                         w_ovendry, &
                         g_ovendry, &
                         int3b_ovendry, &
                         int3a_ovendry, &
                         m1 
       

real*8, parameter    :: r1 = 1.0d0, &
                        rsmall = 1.0d-10 

alphapress0 = dabs(alpha*pressure0)

alphapressc = dabs(alpha*pressurec)

alphapressm = dabs(alpha*pressurem)

betapress0 = dabs(beta*pressure0)

betapressm = dabs(beta*pressurem)

gamma = dlog(dabs(alpha/beta))/dlog(betapressm)

w = w_ovendry(pressure,aentry,alpha,n)

wc = w_ovendry(pressurec,aentry,alpha,n)

wm = w_ovendry(pressurem,aentry,alpha,n)

w0_loc = w0
if (w0_loc<wm) then
  w0_loc=wm
end if 
!c-------------------------------------------------------------------------------------
!c Compute the different integrals 
!c-------------------------------------------------------------------------------------
int3b_w = int3b_ovendry(w,wm,m)
int3b_w0 = int3b_ovendry(w0_loc,wm,m)
int3a_w = int3a_ovendry(w,w0_loc,m)

m1 = m - r1
int1_w = (r1 - wm)**m - (r1 - w)**m  
int2_w = (r1/alpha)* ((r1/pressure)-(r1/pressurem)) + (r1-wm)**m1 - (r1-w)**m1 
!c-------------------------------------------------------------------------------------
!c-------------------------------------------------------------------------------------
!c-------------------------------------------------------------------------------------
if (w>w0_loc) then
  int3_w = int3a_w + int3b_w0
else            
  int3_w = int3b_w
end if         
!c-------------------------------------------------------------------------------------
!c-------------------------------------------------------------------------------------
!c-------------------------------------------------------------------------------------
int_h_ovendry = int1_w * alpha *(r1-gamma*swr - swr) + (alpha*swr/dlog(betapressm)) *  (int2_w + int3_w/n) 
!c-------------------------------------------------------------------------------------        
!c-------------------------------------------------------------------------------------
!c-------------------------------------------------------------------------------------        
return
end function 