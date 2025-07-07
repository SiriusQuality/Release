 !c ----------------------------------------------------------------------
 !c subroutine binmattransp
 !c -------------------
 !c assign binary values to matrix localizing control volume subjected to
 !c transpiration (rld based)
 !c written by:      Fred Gérard - May, 08
 !c definition of variables:
 !c common:   
 !c gen.f:    logical:
 !c           -------- 
 !c           BINT(nn)       = binary matrix of track control volume
 !c                            subjected to transpiration
 !c           integer:
 !c           --------
 !c           mpropvs(nn)    = pointer array for allocation of 
 !c                            material properties
 !c biol.f:   real*8:
 !c           ------- 
 !c           rld(nn)        = root length density
 !c ----------------------------------------------------------------------
      subroutine binmattransp
 
      use gen
	use biol
      
	parameter (r0 = 0.0d0)

 !c build binary matrix for transpiration
	do i=1,nn
	    
!c		izn=mpropvs(i)!FG useless here
		BINT(i)=.false.

		if (rld(i).gt.r0) then
			BINT(i)= .true.
		else
			BINT(i)= .false.
		endif
	
	enddo

	return

	end
