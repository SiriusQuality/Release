! Calculate daily values of global variables used by Sirius
! Written by Frédéric Gérard, June 2021
        
      subroutine MinToSQER(qroot_evap_tot)
      !DEC$ ATTRIBUTES STDCALL, ALIAS:"MINTOSQER", DLLEXPORT :: MinToSQER
        !DEC$ ATTRIBUTES REFERENCE :: qroot_evap_tot
    
      use parm
      use gen
      use phys
      use biol
         
      Implicit none 
            
      integer :: ivol,izn
      real*8, intent(inout) ::  qroot_evap_tot
       
      real*8 :: rootwat,evapo
      real*8, parameter :: r0 = 0.0d0
            
      external rootwat, evapo
      
! initialization of local variables (unless arrays, as done later)             
        qroot_evap_tot=r0
        
! Main loop over control volumes        
        do ivol = 1,nngl
! calculate actual evaporation (m3/s)
		 if (BINev(ivol)) then
			 qroot_evap_tot=qroot_evap_tot+cvol(ivol)*evapo(sanew,ivol)
        endif
        enddo
        write(*,*) 'Min3p : ER envoyé : ',ep_sirius, qroot_evap_tot
    return
    end