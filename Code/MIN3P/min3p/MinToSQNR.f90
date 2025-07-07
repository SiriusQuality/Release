! Calculate daily values of global variables used by Sirius
! Written by Frédéric Gérard, June 2021
        
      subroutine MinToSQNR(n_opti)
      !DEC$ ATTRIBUTES STDCALL, ALIAS:"MINTOSQNR", DLLEXPORT :: MinToSQNR
        !DEC$ ATTRIBUTES REFERENCE :: n_opti
    
      use parm
      use gen
      use phys
      use biol
         
      Implicit none 
            
      integer :: ivol,izn
      real*8, intent(inout) ::  n_opti
       
      real*8, parameter :: r0 = 0.0d0
            
      external rootwat, evapo
      
! initialization of local variables (unless arrays, as done later)             
        n_opti=r0
        n_opti = np_sirius
        write(*,*) 'Min3p : NR envoyé : ',np_sirius, n_opti
    return
    end