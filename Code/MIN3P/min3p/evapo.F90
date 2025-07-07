!c -----------------------------------------------------------------------
!c real*8 function evapo
!c -----------------------
!c compute water evaporation for current control volume 
!c
!c written by:      Fred Gérard - Oct. 05, 05
!c
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c                                                                    I O
!c passed:   real*8:
!c           -------
!c           satwnew(nn)        = aqueous phase saturation            + -
!c                                - new time level
!c
!c           integer*4:
!c           ----------
!c           ivol               = pointer to current volume
!c
!c          
!c common:   
!c
!c gen.f     real*8
!c           ------
!c           cvol(nn)           = nodal volumes
!c           pe_soil            = potential soil evaporation
!c           delx(nvx)          = spatial increment in x-direction         
!c           dely(nvy)          = spatial increment in y-direction         
!c           delz(nvz)          = spatial increment in z-direction 
!c
!c           integer*4
!c           ------
!c           nn                 = total number of control volumes 
!c           mpropvs(nn)        = pointer array to volume properties
!c
!c phys.f:   real*8:
!c           ------- 
!c           satwfield(nzn)     = water saturation at field capacity  + -
!c           satwdry(nzn)       = air-dry water saturation
!c           pvol(nzn)          = volume of property zone (used for evaporation)
!c
!c           logical:
!c           --------
!c           pure_evaporation   = .true.  -> physical evaporation only
!c
!c
!c local:    vsum               = total volume
!c           evap               = evaporation flux
!c           izn                = pointer to material property zone
!c
!c external: -
!c ----------------------------------------------------------------------
 
      real*8 function evapo(satwnew,ivol)
 
      use parm
      use phys
      use gen
	  use dual
	  use biol

	implicit real*8 (a-h,o-z)

      parameter (r0 = 0.0d0, r1 = 1.0d0, r2 = 2.0d0, r4 = 4.0d0)
      parameter (r1p5 = 1.5d0)

      real*8 satwnew(*)
      real*8 slosl,ptsoil
	  integer iiii,j,i


!c
!c!FG april 2013: initialization as required regarding how evap is used 
!c                in the function (see third case below).
!c                Note that this fix also correct time step differences (and so on)
!c                compared to compaq
!c
      evapo = r0


!c     
!c  setup pointer to the material properties of the current volume
!c    
	izn = mpropvs(ivol)

!FG Oct 09 : quick fix to detect if pure evaporation considered in the simulation

      pure_evaporation=.false. ! initialize pure_evaporation logical

	if (.not.root_uptake) then      
		   pure_evaporation=.true.
      endif

!c
!c  check for zone where soil evaporation should operate and calculate the total flux (in s-1)
!c


	if (h1dry(izn).gt.r0) then
        
        
        
      
!c flux eventually corrected for the effect of water saturation
!c        
!c         if (cmws.eq.3) then
!c
!c	     evapo = toparea * pe_soil/pvol(izn)
!c
!c         else
        
       ! if(coupled_sirius) then
          !  write(*,*) 'test'
          !  ptsoil
         !   evapo = min(slosl,ptsoil)
            
       ! endif

        if (satwnew(ivol).gt.satwfield(izn)) then   ! h1dry > 0 and satwnew > satwfield     
    	        	evapo = toparea * pe_soil/pvol(izn)

        elseif (satwnew(ivol).lt.satwdry(izn)) then	!  h1dry > 0 and satwnew < satwdry 
	        	   evapo=r0
	   
        else					!  h1dry > 0 and satwdry <= satwnew <= satwfield

	        	evapo = ((satwnew(ivol)- satwdry(izn))/	&
    &	          	 (satwfield(izn)-satwdry(izn)))
   
   


   	        	evapo = evapo* toparea * pe_soil/pvol(izn)


         	endif


	endif ! if h1dry>0


	return
      
	end 
