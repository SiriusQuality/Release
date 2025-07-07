! Calculate daily values of global variables used by Sirius
! Written by Frédéric Gérard, June 2021
        
      subroutine MinToSQTR(qroot_transp_tot)
      !DEC$ ATTRIBUTES STDCALL, ALIAS:"MINTOSQTR", DLLEXPORT :: MinToSQTR
        !DEC$ ATTRIBUTES REFERENCE :: qroot_transp_tot
    
      use parm
      use gen
      use phys
      use biol
         
      Implicit none 
            
      integer :: ivol,izn
      real*8, intent(inout) ::  qroot_transp_tot
       
      real*8 :: rootwat, evapo
      real*8, parameter :: r0 = 0.0d0
      
            
      external rootwat, evapo
      
! initialization of local variables (unless arrays, as done later)
	    qroot_transp_tot=r0
        
! Main loop over control volumes        
        do ivol = 1,nngl
! calculate actual transpiration (m3/s) and reservoirs in rooted control volumes (mm)
		 if (BINT(ivol)) then          
			qroot_transp_tot=qroot_transp_tot+cvol(ivol)   &      !transpiration
    		 &                         *rootwat(sanew,ivol)
         endif
        enddo
        write(*,*) 'Min3p : TR envoyé : ', tp_sirius, qroot_transp_tot
    return
    end