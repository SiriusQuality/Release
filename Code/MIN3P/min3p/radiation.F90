!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 221 $
!> $Author: dsu $
!> $Date: 2014-08-05 23:29:49 +0200 (Tue, 05 Aug 2014) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/radiation.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!cprovi-----------------------------------------------------------------------------------------
!cprovi Compute the solar radiation based on Saaltink et al. (2005)
!cprovi-----------------------------------------------------------------------------------------
real*8 function radiation(saw_ivol,temp_ivol) 
 
use parm
use gen
use dens
use phys 
use chem
 
implicit none
     
real*8, intent(in)            :: saw_ivol

real*8, intent(in)            :: temp_ivol 

real*8, parameter :: r0 = 0.0d0, r1 = 1.0d0, &
                     r2=2.0d0, rsmall=1.0d-1, & 
                     r4=4.0d0, rhalf=0.5d0

real*8, parameter     :: pi=3.14159265359d0

real*8, parameter     :: r365=365.241d0

real*8, parameter     :: c1=1.00011d0

real*8, parameter     :: c2=0.03422d0

real*8, parameter     :: c3=0.00128d0

real*8, parameter     :: c4=0.000179d0

real*8, parameter     :: c5=0.000077d0 

real*8, parameter     :: rkelvin=273.15d0 

real*8 :: dsun, ds, time_norm, term1, term2, rs, ra, rg, tempk_atm,    &
          tempk_ivol, em, al, ratm, rsoil

!cprovi-----------------------------------------------------------------------------------------
!cprovi Inizialization of variables 
!cprovi-----------------------------------------------------------------------------------------
radiation=r0
!cprovi-----------------------------------------------------------------------------------------
!cprovi Compute the dayly potential solar radiation
!cprovi (see RETRASO manual)
!cprovi-----------------------------------------------------------------------------------------
!cprovi Coompute declination of the sun 
!cprovi-----------------------------------------------------------------------------------------
dsun = -cmaxdsun*dsin(r2*pi*(time_io-tautumn_atm)/r365) 
!cprovi-----------------------------------------------------------------------------------------
!cprovi Coompute declination of the sun 
!cprovi-----------------------------------------------------------------------------------------
!cprovi Compute the solar daily radiation 
!cprovi Cloud index (= 1 for a clear sky, = 0 for a completely clouded sky)
!cprovi-----------------------------------------------------------------------------------------
ds=(r1/pi)*dacos(-dtan(lat_atm)*dtan(dsun))
!cprovi-----------------------------------------------------------------------------------------
!cprovi Compute normalized time 
!cprovi-----------------------------------------------------------------------------------------
time_norm=floor(time_io)
time_norm=time_io-time_norm
!cprovi-----------------------------------------------------------------------------------------
!cprovi Compute daily solar radiation  
!cprovi-----------------------------------------------------------------------------------------
if (comp_daily_rad_atm) then
  !cprovi-----------------------------------------------------------------------------------------
  !cprovi If compute daily variation in radiation  
  !cprovi-----------------------------------------------------------------------------------------
  if(time_norm>=(tnoon_atm-rhalf*ds).and.time_norm<=(tnoon_atm+rhalf*ds)) then
   term1=r2*pi*(time_io-tjan_atm)/r365
   term2=r4*pi*(time_io-tjan_atm)/r365
   rs=c1+c2*dcos(term1)+c3*dsin(term1)+c4*dcos(term2)+c5*dsin(term1)
   term1=(r1/pi)*dcos(lat_atm)*dcos(dsun)*dsin(pi*ds)+ds*dsin(lat_atm)*dsin(dsun)
   ra=cgsun*rs*term1
   rg=ra*(0.29d0*dcos(lat_atm)+0.52d0*icloud_atm)
   rg=(pi*rg/(r2*ds)) * dsin((time_norm-tnoon_atm+rhalf*ds)*pi/ds)    
  else
   rg=r0 
  end if
else
   term1=r2*pi*(time_io-tjan_atm)/r365
   term2=r4*pi*(time_io-tjan_atm)/r365
   rs=c1+c2*dcos(term1)+c3*dsin(term1)+c4*dcos(term2)+c5*dsin(term1)
   term1=(r1/pi)*dcos(lat_atm)*dcos(dsun)*dsin(pi*ds)+ds*dsin(lat_atm)*dsin(dsun)
   ra=cgsun*rs*term1
   rg=ra*(0.29d0*dcos(lat_atm)+0.52d0*icloud_atm)
   rg=(pi*rg/(r2*ds)) 
end if
!cprovi-----------------------------------------------------------------------------------------
!cprovi Temperature in kelvins
!cprovi----------------------------------------------------------------------------------------- 
tempk_atm=temp_atm+rkelvin
tempk_ivol=temp_ivol+rkelvin
em=0.9d0+0.05d0*saw_ivol
al=adry_atm+(adry_atm-awet_atm)*((saw_ivol**r2)-r2*saw_ivol)
!cprovi-----------------------------------------------------------------------------------------
!cprovi Compute the long wave atmospheric radiation 
!cprovi-----------------------------------------------------------------------------------------
ratm=em*cboltzman*(tempk_atm**r4)*(0.605d0+0.048d0*dsqrt(1360.0d0*densv_atm)) 
!cprovi-----------------------------------------------------------------------------------------
!cprovi Compute the soil radiation 
!cprovi-----------------------------------------------------------------------------------------
rsoil=em*cboltzman*(tempk_ivol**r4)
!cprovi-----------------------------------------------------------------------------------------
!cprovi Compute total radiation
!cprovi-----------------------------------------------------------------------------------------
radiation = facrad_atm *((r1-al)*rg + ratm - rsoil)

return
end function  