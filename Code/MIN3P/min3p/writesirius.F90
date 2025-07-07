! Calculate daily values of global variables used by Sirius
! Written by Frédéric Gérard, June 2021
        
      subroutine writesirius(availablewater)
      !DEC$ ATTRIBUTES STDCALL, ALIAS:"WRITESIRIUS", DLLEXPORT :: writesirius
        !DEC$ ATTRIBUTES REFERENCE :: availablewater
    
      use parm
      use gen
      use phys
      use biol
         
      Implicit none 
            
      integer :: ivol,izn
      real*8 ::  qroot_evap_tot, qroot_transp_tot ,maxavailablewater, maxexcesswater, maxavailablewater_roots, maxexcesswater_roots
      real*8, intent(inout) ::  availablewater(nngl)
      real*8 :: unavailablewater(nngl),excesswater(nngl), &
          &                     availablewater_roots(nngl),unavailablewater_roots(nngl),excesswater_roots(nngl)
       
      real*8 :: rootwat, evapo,lower_comp_water_roots,              &
     &          upper_comp_water_roots,lower_comp_water,upper_comp_water
      real*8, parameter :: r0 = 0.0d0
      
      real*8 :: comp_availablewater(nngl),comp_availablewater_roots(nngl)
            
      external rootwat, evapo
      
! initialization of local variables (unless arrays, as done later)             
        qroot_evap_tot=r0
	    qroot_transp_tot=r0
        maxavailablewater=r0
        maxexcesswater=r0
        lower_comp_water=r0
        upper_comp_water=r0
        maxavailablewater_roots=r0
        maxexcesswater_roots=r0
        lower_comp_water_roots=r0
        upper_comp_water_roots=r0
        
! Main loop over control volumes        
        do ivol = 1,nngl
            izn = mpropvs(ivol)
! calculate actual evaporation (m3/s)
		 if (BINev(ivol)) then
			 qroot_evap_tot=qroot_evap_tot+cvol(ivol)*evapo(sanew,ivol)/sec_per_days
         endif
! calculate actual transpiration (m3/s) and reservoirs in rooted control volumes (mm)
		 if (BINT(ivol)) then          
			qroot_transp_tot=qroot_transp_tot+cvol(ivol)   &      !transpiration
    		 &                         *rootwat(sanew,ivol)/sec_per_days
! *******************************************           
! water reservoirs in ROOT control volumes
! *******************************************
! assign zone
           izn = mpropvs(ivol)
! sanew < satwlim => mini unavailable water (portion between 0 and sanew of the rectangle)
!*****************************************************************************************
           if (sanew(ivol).lt.satwlim(izn)) then
! available water = 0
              availablewater_roots(ivol)=r0
! unavailable water is mini 
              unavailablewater_roots(ivol)=sanew(ivol)*pornew(ivol)! in m3water/m3bulk
              unavailablewater_roots(ivol)=unavailablewater_roots(ivol)*cvol(ivol)!conversion in m3 water
              unavailablewater_roots(ivol)=1000*unavailablewater_roots(ivol)/toparea  !conversion in mm water
! Exces water = 0
              excesswater_roots(ivol)=r0
! satwlim =< sanew < satwopt => available water increase (portion of the lower reservoir : triangle)
!***************************************************************************************************
           elseif ((sanew(ivol).ge.satwlim(izn)).and.(sanew(ivol).lt.satwopt(izn))) then
! available water increases linearly with sanew (triangle)
              lower_comp_water_roots = 0.5*(satwopt(izn)-satwlim(izn))*pornew(ivol)! full reservoir (triangle, with h=1 => s = base/2, base between
                                                                                   ! wilting point and optimal value)
              availablewater_roots(ivol) = lower_comp_water_roots*(1-(satwopt(izn)-sanew(ivol))/(satwopt(izn)-satwlim(izn)))! actual reservoir as a fraction of the base
!                                                                                                                             of the triangle, according to sanew
              availablewater_roots(ivol)=availablewater_roots(ivol)*cvol(ivol)!m3 water
              availablewater_roots(ivol)=1000*availablewater_roots(ivol)/toparea  !mm water
! unavailable water is maximum (between 0 and satwlim)
              unavailablewater_roots(ivol)=satwlim(izn)*pornew(ivol)! in m3water/m3bulk
              unavailablewater_roots(ivol)=unavailablewater_roots(ivol)*cvol(ivol)!conversion in m3 water
              unavailablewater_roots(ivol)=1000*unavailablewater_roots(ivol)/toparea  !conversion in mm water
! Exces water = 0
              excesswater_roots(ivol)=r0
! satwopt =< sanew =< satwfield => available water further increases (lower reservoir + portion of rectangle)
!***************************************************************************************************
           elseif ((sanew(ivol).ge.satwopt(izn)).and.(sanew(ivol).le.satwfield(izn))) then
! max lower reservoir 
              availablewater_roots(ivol) = 0.5*(satwopt(izn)-satwlim(izn))*pornew(ivol)! full reservoir (triangle, with h=1 => s = base/2, base between
                                                                                       ! wilting point and optimal value)
! add complement available water : portion of rectangle
              availablewater_roots(ivol)=availablewater_roots(ivol)+(sanew(ivol)-satwopt(izn))*pornew(ivol) ! in m3water/m3bulk
! conversion
              availablewater_roots(ivol)=availablewater_roots(ivol)*cvol(ivol)!conversion in m3 water
              availablewater_roots(ivol)=1000*availablewater_roots(ivol)/toparea  !conversion in mm water
! unavailable water is maximum (between 0 and satwlim)
              unavailablewater_roots(ivol)=satwlim(izn)*pornew(ivol)! in m3water/m3bulk
              unavailablewater_roots(ivol)=unavailablewater_roots(ivol)*cvol(ivol)!conversion in m3 water
              unavailablewater_roots(ivol)=1000*unavailablewater_roots(ivol)/toparea  !conversion in mm water
! Excess water is 0              
              excesswater_roots(ivol)=r0
! sanew > satwfield => max available water (rectangle + portion of triangle)
!***************************************************************************
           else ! ((sanew(ivol).gt.satwfield(izn))
! max lower reservoir (triangle)
              availablewater_roots(ivol) = 0.5*(satwopt(izn)-satwlim(izn))*pornew(ivol)! full reservoir (triangle, with h=1 => s = base/2, base between
                                                                                       ! wilting point and optimal value)
!          add baseline available water (triangle + rectangle)          
              availablewater_roots(ivol)=availablewater_roots(ivol)+(satwfield(izn)-satwlim(izn))*pornew(ivol) ! in m3water/m3bulk
!          complement calculated (portion of triangle)
              upper_comp_water_roots = 0.5*(1-satwfield(izn))*pornew(ivol)! full reservoir (triangle, with h=1 => s = base/2, base between saturation and satfield)
              comp_availablewater_roots(ivol) = upper_comp_water_roots*(1-(1-sanew(ivol))/(1-satwfield(izn)))! actual reservoir as a fraction of the base defined by sanew                                                                                                ! of the triangle base defined by Sanew
! actual available water (all)    
              availablewater_roots(ivol)=availablewater_roots(ivol)+comp_availablewater_roots(ivol)
              availablewater_roots(ivol)=availablewater_roots(ivol)*cvol(ivol)!m3 water
              availablewater_roots(ivol)=1000*availablewater_roots(ivol)/toparea  !mm water
! unavailable water is maximum (between 0 and satwlim)
              unavailablewater_roots(ivol)=satwlim(izn)*pornew(ivol)! in m3water/m3bulk
              unavailablewater_roots(ivol)=unavailablewater_roots(ivol)*cvol(ivol)!conversion in m3 water
              unavailablewater_roots(ivol)=1000*unavailablewater_roots(ivol)/toparea  !conversion in mm water
! Excess water not 0
              excesswater_roots(ivol)=((sanew(ivol)-satwfield(izn))*pornew(ivol))-comp_availablewater_roots(ivol)! rectangle defined between satwfield et sanew - comp_availablewater
              excesswater_roots(ivol)=excesswater_roots(ivol)*cvol(ivol)!conversion in m3 water
              excesswater_roots(ivol)=1000*excesswater_roots(ivol)/toparea  !conversion in mm water
              
           endif ! test on sanew to calculate water reservoirs
        else! no root
              availablewater_roots(ivol)=r0
              unavailablewater_roots(ivol)=r0
              excesswater_roots(ivol)=r0         
        endif!transp in ivol?
        
! *******************************************           
! water reservoirs in ALL control volumes
! *******************************************
! sanew < satwlim => mini unavailable water (portion between 0 and sanew of the rectangle)
!*****************************************************************************************
           if (sanew(ivol).lt.satwlim(izn)) then
! available water = 0
              availablewater(ivol)=r0
! unavailable water is mini 
              unavailablewater(ivol)=sanew(ivol)*pornew(ivol)! in m3water/m3bulk
              unavailablewater(ivol)=unavailablewater(ivol)*cvol(ivol)!conversion in m3 water
              unavailablewater(ivol)=1000*unavailablewater(ivol)/toparea  !conversion in mm water
! Exces water = 0
              excesswater(ivol)=r0
              
! satwlim =< sanew < satwopt => available water increase (portion of the lower reservoir : triangle)
!***************************************************************************************************
           elseif ((sanew(ivol).ge.satwlim(izn)).and.(sanew(ivol).lt.satwopt(izn))) then
! available water increases linearly with sanew (triangle)
              lower_comp_water = 0.5*(satwopt(izn)-satwlim(izn))*pornew(ivol)! full reservoir (triangle, with h=1 => s = base/2, base between
                                                                                   ! wilting point and optimal value)
              availablewater(ivol) = lower_comp_water*(1-(satwopt(izn)-sanew(ivol))/(satwopt(izn)-satwlim(izn)))! actual reservoir as a fraction of the base
!                                                                                                                             of the triangle, according to sanew
              availablewater(ivol)=availablewater(ivol)*cvol(ivol)!m3 water
              availablewater(ivol)=1000*availablewater(ivol)/toparea  !mm water
! unavailable water is maximum (between 0 and satwlim)
              unavailablewater(ivol)=satwlim(izn)*pornew(ivol)! in m3water/m3bulk
              unavailablewater(ivol)=unavailablewater(ivol)*cvol(ivol)!conversion in m3 water
              unavailablewater(ivol)=1000*unavailablewater(ivol)/toparea  !conversion in mm water
! Exces water = 0
              excesswater(ivol)=r0
              
! satwopt =< sanew =< satwfield => available water further increases (lower reservoir + portion of rectangle)
!***************************************************************************************************
           elseif ((sanew(ivol).ge.satwopt(izn)).and.(sanew(ivol).le.satwfield(izn))) then
! max lower reservoir 
              availablewater(ivol) = 0.5*(satwopt(izn)-satwlim(izn))*pornew(ivol)! full reservoir (triangle, with h=1 => s = base/2, base between
                                                                                       ! wilting point and optimal value)
! add complement available water : portion of rectangle
              availablewater(ivol)=availablewater(ivol)+(sanew(ivol)-satwopt(izn))*pornew(ivol) ! in m3water/m3bulk
! conversion
              availablewater(ivol)=availablewater(ivol)*cvol(ivol)!conversion in m3 water
              availablewater(ivol)=1000*availablewater(ivol)/toparea  !conversion in mm water
! unavailable water is maximum (between 0 and satwlim)
              unavailablewater(ivol)=satwlim(izn)*pornew(ivol)! in m3water/m3bulk
              unavailablewater(ivol)=unavailablewater(ivol)*cvol(ivol)!conversion in m3 water
              unavailablewater(ivol)=1000*unavailablewater(ivol)/toparea  !conversion in mm water
! Excess water is 0              
              excesswater(ivol)=r0
              
! sanew > satwfield => max available water (rectangle + portion of triangle)
!***************************************************************************
           else ! ((sanew(ivol).gt.satwfield(izn))
! max lower reservoir (triangle)
              availablewater(ivol) = 0.5*(satwopt(izn)-satwlim(izn))*pornew(ivol)! full reservoir (triangle, with h=1 => s = base/2, base between
                                                                                       ! wilting point and optimal value)
!            write(*,*) 'max lower res', availablewater(ivol)                                                                                     
!          add baseline available water (triangle + rectangle)          
              availablewater(ivol)=availablewater(ivol)+(satwfield(izn)-satwlim(izn))*pornew(ivol) ! in m3water/m3bulk
!            write(*,*) 'triangle + rectangle', availablewater(ivol)
!          complement calculated (portion of triangle)
              upper_comp_water = 0.5*(1-satwfield(izn))*pornew(ivol)! full reservoir (triangle, with h=1 => s = base/2, base between saturation and satfield)
              comp_availablewater(ivol) = upper_comp_water*(1-(1-sanew(ivol))/(1-satwfield(izn)))! actual reservoir as a fraction of the base defined by sanew                                                                                                ! of the triangle base defined by Sanew
! actual available water (all)    
              availablewater(ivol)=availablewater(ivol)+comp_availablewater(ivol)
              availablewater(ivol)=availablewater(ivol)*cvol(ivol)!m3 water
              availablewater(ivol)=1000*availablewater(ivol)/toparea  !mm water
! unavailable water is maximum (between 0 and satwlim)
              unavailablewater(ivol)=satwlim(izn)*pornew(ivol)! in m3water/m3bulk
              unavailablewater(ivol)=unavailablewater(ivol)*cvol(ivol)!conversion in m3 water
              unavailablewater(ivol)=1000*unavailablewater(ivol)/toparea  !conversion in mm water
! Excess water not = 0
              excesswater(ivol)=((sanew(ivol)-satwfield(izn))*pornew(ivol))-comp_availablewater(ivol)! rectangle defined between satwfield et sanew - comp_availablewater   
              excesswater(ivol)=excesswater(ivol)*cvol(ivol)!conversion in m3 water
              excesswater(ivol)=1000*excesswater(ivol)/toparea  !conversion in mm water        
           endif ! test on sanew to calculate water reservoirs
           
        end do!ivol
!
! get max values
!
        do ivol = 1,nngl
            maxavailablewater=dmax1(availablewater(ivol),r0)
            maxexcesswater=dmax1(excesswater(ivol),r0)
            maxavailablewater_roots=dmax1(availablewater_roots(ivol),r0)
            maxexcesswater_roots=dmax1(excesswater_roots(ivol),r0)
        end do !ivol2
        
  
! Voilà Teiki, tout est calculé correctement maintenant, à pas journalier. Transferer à Sirius donc
! Recapitulatif. On transfert 6 vecteurs et 6 scalaires, qui correspondent à:
!                           - availablewater,unavailablewater,excesswater dans chaque maille du profil (ivol), en mm
!                           - maxavailablewater et maxexcesswater (les valeurs max)
!                           - availablewater_roots,unavailablewater_roots,excesswater_roots dans chaque maille RACINAIRE du profil, en mm
!                           - maxavailablewater_roots et maxexcesswater_roots (les valeurs max)
!                           - qroot_evap_tot = actual evaporation (m3/s)
!                           - qroot_transp_tot = actual transpiration (m3/s)
        
!test       
   !    do ivol = 1,nngl
    !    write(*,*) 'sat',sanew(ivol),'aw',availablewater(ivol)
     !   write(*,*) 'unaw',unavailablewater(ivol),'ew',excesswater(ivol)        
     ! end do
     !   write (*,*) 'actual transp (m3/s)', qroot_transp_tot
      ! write (*,*) 'actual evapo (m3/s)', qroot_evap_tot
       ! write (*,*) 'max avail water (mm)',maxavailablewater,'max avail water ROOTS (mm)',maxavailablewater_roots  
        !write (*,*) 'max excess water (mm)',maxexcesswater,'max excess water ROOTS (mm)',maxexcesswater_roots

        
    return
    end