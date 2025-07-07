 !c ---------------------------------------------------------------------- 
 !c subroutine binmatevap
 !c -------------------
 !c assign binary values to matrix localizing control volume subjected to 
 !c evaporation (h1dry based)
 !c written by:      Fred Gérard - May, 08
 !c definition of variables: 
 !c 
 !c common:    
 !c 
 !c gen.f:    logical: 
 !c           --------  
 !c           BINev(nn)      = binary matrix of track control volume 
 !c                            subjected to evaporation 
 !c           integer: 
 !c           -------- 
 !c           mpropvs(nn)    = pointer array for allocation of  
 !c                            material properties 
 !c phys.f:   real*8: 
 !c           -------  
 !c           h1dry(nzn)     = air-dry aqueous pressure 
 !c ---------------------------------------------------------------------- 
      subroutine binmatevap 
  
      use gen 
      use phys 
       
	!integer :: ivol
        parameter (r0 = 0.0d0) 
 	

 !c build binary matrix for evaporation
        do ivol=1,nn 

            izn=mpropvs(ivol) 

		
                BINev(ivol)=.false.
 
                if (h1dry(izn).gt.r0) then 
                        BINev(ivol)= .true. 
                else  
                        BINev(ivol)= .false. 
                endif 
                 
        enddo 

        return 
 
        end

