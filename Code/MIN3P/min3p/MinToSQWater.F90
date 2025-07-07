! Calculate daily values of global variables used by Sirius
! Written by Frédéric Gérard, June-July 2021
        
      subroutine MinToSQWater(availablewater_bis,unavailablewater_bis,excesswater_bis,availablewater_roots_bis,unavailablewater_roots_bis,excesswater_roots_bis,  &
     &          maxavailablewater_bis,maxexcesswater_bis,maxavailablewater_roots_bis,maxexcesswater_roots_bis)
      !DEC$ ATTRIBUTES STDCALL, ALIAS:"MINTOSQWATER", DLLEXPORT :: MinToSQWater
        !DEC$ ATTRIBUTES REFERENCE :: availablewater_bis
        !DEC$ ATTRIBUTES REFERENCE :: unavailablewater_bis
        !DEC$ ATTRIBUTES REFERENCE :: excesswater_bis
        !DEC$ ATTRIBUTES REFERENCE :: availablewater_roots_bis
        !DEC$ ATTRIBUTES REFERENCE :: unavailablewater_roots_bis
        !DEC$ ATTRIBUTES REFERENCE :: excesswater_roots_bis
        !DEC$ ATTRIBUTES REFERENCE :: maxavailablewater_bis
        !DEC$ ATTRIBUTES REFERENCE :: maxexcesswater_bis
        !DEC$ ATTRIBUTES REFERENCE :: maxavailablewater_roots_bis
        !DEC$ ATTRIBUTES REFERENCE :: maxexcesswater_roots_bis
    
    use parm
    use gen
    use phys
    use biol
    use chem!FG sept 2021
         
      Implicit none 
            
      integer :: ivol,izn
      real*8 :: rootwat, evapo, reduct_factor_water_reservoirs,extra_availablewater
      real*8, parameter :: r0 = 0.0d0
      
      real*8 :: availablewater(nngl),unavailablewater(nngl),excesswater(nngl),                    &
     &          availablewater_roots(nngl),unavailablewater_roots(nngl),excesswater_roots(nngl),                 &
     &          maxavailablewater(nngl),maxexcesswater(nngl),maxavailablewater_roots(nngl), maxexcesswater_roots(nngl) !FG August 2021
      
      real*8, intent(inout) :: availablewater_bis(nngl),unavailablewater_bis(nngl),excesswater_bis(nngl),                    &
     &          availablewater_roots_bis(nngl),unavailablewater_roots_bis(nngl),excesswater_roots_bis(nngl),                 &
     &          maxavailablewater_bis(nngl),maxexcesswater_bis(nngl),maxavailablewater_roots_bis(nngl), maxexcesswater_roots_bis(nngl)
      
      external rootwat, evapo
      
! initialization of local variables (unless arrays, as done later)
        reduct_factor_water_reservoirs=r0
        extra_availablewater=r0
! Main loop over control volumes        
        do ivol = 1,nngl
! *******************************************  
            izn = mpropvs(ivol)
! set default for max available and excess win rootless controle volume!FG August 2021
            maxavailablewater_roots(ivol)=r0
            maxexcesswater_roots(ivol)=r0
! water reservoirs in ROOT control volumes
! *******************************************
            if (BINT(ivol)) then 
! max available and excess water!FG August 2021
           maxavailablewater_roots(ivol)=0.5*(satwopt(izn)-satwlim(izn))+(satwfield(izn)-satwopt(izn))+(1-satwfield(izn))
           maxavailablewater_roots(ivol)=maxavailablewater_roots(ivol)*pornew(ivol)*cvol(ivol)*1000/toparea!from m3water/m3porosity to mm water
           maxexcesswater_roots(ivol)= 0.5*(1-satwfield(izn))*pornew(ivol)*cvol(ivol)*1000/toparea!mm water
! sanew < satwlim => Domain 1 : only unavailable water (portion between 0 and sanew)
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
              
! satwlim =< sanew < satwopt => available water appears, domain 2 (linear increase with S)
!***************************************************************************************************
           elseif ((sanew(ivol).ge.satwlim(izn)).and.(sanew(ivol).lt.satwopt(izn))) then
! calculate reduction factor alpha (alpha = S/(Sopt - SPFP) - SPFP/(Sopt - SPFP))
              reduct_factor_water_reservoirs = (sanew(ivol)/(satwopt(izn)-satwlim(izn))) - (satwlim(izn)/(satwopt(izn)-satwlim(izn)))
! available water
              availablewater_roots(ivol)=reduct_factor_water_reservoirs*(sanew(ivol)-satwlim(izn))*0.5*pornew(ivol)!surface triangle, m3water/m3bulk
              availablewater_roots(ivol)=availablewater_roots(ivol)*cvol(ivol)!m3 water
              availablewater_roots(ivol)=1000*availablewater_roots(ivol)/toparea  !mm water
! unavailable water (between 0 and satwlim + reduction factor alpha) 
              unavailablewater_roots(ivol)=(sanew(ivol) - reduct_factor_water_reservoirs*(sanew(ivol)-satwlim(izn))*0.5)*pornew(ivol)! in m3water/m3bulk (S - ?* x (S-SPFP)x0.5) 
              unavailablewater_roots(ivol)=unavailablewater_roots(ivol)*cvol(ivol)!conversion in m3 water
              unavailablewater_roots(ivol)=1000*unavailablewater_roots(ivol)/toparea  !conversion in mm water
! Exces water = 0
              excesswater_roots(ivol)=r0
              
! satwopt =< sanew =< satwfield => domain 3 (constant with S)
!***************************************************************************************************
           elseif ((sanew(ivol).ge.satwopt(izn)).and.(sanew(ivol).le.satwfield(izn))) then
! max lower reservoir (alpha = 1)
              availablewater_roots(ivol)=0.5*(satwopt(izn)-satwlim(izn))*pornew(ivol)
! add complement available water : portion of rectangle
              availablewater_roots(ivol)=availablewater_roots(ivol)+(sanew(ivol)-satwopt(izn))*pornew(ivol) ! in m3water/m3bulk
! conversions
              availablewater_roots(ivol)=availablewater_roots(ivol)*cvol(ivol)!conversion in m3 water
              availablewater_roots(ivol)=1000*availablewater_roots(ivol)/toparea  !conversion in mm water
! unavailable water is max (rectangle + triangle) 
              unavailablewater_roots(ivol)=(satwlim(izn)+(satwopt(izn)-satwlim(izn))*0.5)*pornew(ivol)! in m3water/m3bulk
              unavailablewater_roots(ivol)=unavailablewater_roots(ivol)*cvol(ivol)!conversion in m3 water
              unavailablewater_roots(ivol)=1000*unavailablewater_roots(ivol)/toparea  !conversion in mm water
! Excess water is 0              
              excesswater_roots(ivol)=r0
              
! sanew > satwfield => domain 4 (linear decrease with S, excess water occurs) 
!***************************************************************************
           else ! ((sanew(ivol).gt.satwfield(izn))
! Calculate max lower reservoir of available water (triangle between wilting point and optimal value, alpha = 1)
              availablewater_roots(ivol) = 0.5*(satwopt(izn)-satwlim(izn))*pornew(ivol)
! calculate baseline available water, defined between Sopt and Sfield (rectangle, alpha = 1), and addition to max lower reservoir          
              availablewater_roots(ivol)=availablewater_roots(ivol)+(satwfield(izn)-satwopt(izn))*pornew(ivol) ! in m3water/m3bulk
! calculate reduction factor alpha (alpha* = S/(Sfield cap - 1) - 1/(Sfield cap - 1))
              reduct_factor_water_reservoirs = (sanew(ivol)/(satwfield(izn)-1)) - (satwfield(izn)-1)**-1
! calculate extra available water
              extra_availablewater = (1-satwfield(izn))-reduct_factor_water_reservoirs*(1-sanew(ivol))*0.5
! total available water
              availablewater_roots(ivol)=availablewater_roots(ivol)+extra_availablewater*pornew(ivol)! in m3water/m3bulk
              availablewater_roots(ivol)=availablewater_roots(ivol)*cvol(ivol)!m3 water
              availablewater_roots(ivol)=1000*availablewater_roots(ivol)/toparea  !mm water
! unavailable water is max (rectangle + triangle) 
              unavailablewater_roots(ivol)=(satwlim(izn)+(satwopt(izn)-satwlim(izn))*0.5)*pornew(ivol)! in m3water/m3bulk
              unavailablewater_roots(ivol)=unavailablewater_roots(ivol)*cvol(ivol)!conversion in m3 water
              unavailablewater_roots(ivol)=1000*unavailablewater_roots(ivol)/toparea  !conversion in mm water
! Excess water(1- alpha*) x (S-Sfield cap) 
              excesswater_roots(ivol)=(1-reduct_factor_water_reservoirs)*(sanew(ivol)-satwfield(izn))*pornew(ivol)! in m3water/m3bulk
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
! max available and excess water!FG August 2021
           maxavailablewater(ivol)=0.5*(satwopt(izn)-satwlim(izn))+(satwfield(izn)-satwopt(izn))+(1-satwfield(izn))
           maxavailablewater(ivol)=maxavailablewater(ivol)*pornew(ivol)*cvol(ivol)*1000/toparea!from m3water/m3porosity to mm water
           maxexcesswater(ivol)= 0.5*(1-satwfield(izn))*pornew(ivol)*cvol(ivol)*1000/toparea!mm water
! sanew < satwlim => Domain 1 : only unavailable water (portion between 0 and sanew)
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
              
! satwlim =< sanew < satwopt => available water appears, domain 2 (linear increase with S)
!***************************************************************************************************
           elseif ((sanew(ivol).ge.satwlim(izn)).and.(sanew(ivol).lt.satwopt(izn))) then
! calculate reduction factor alpha (alpha = S/(Sopt - SPFP) - SPFP/(Sopt - SPFP))
              reduct_factor_water_reservoirs = (sanew(ivol)/(satwopt(izn)-satwlim(izn))) - (satwlim(izn)/(satwopt(izn)-satwlim(izn)))
! available water
              availablewater(ivol)=reduct_factor_water_reservoirs*(sanew(ivol)-satwlim(izn))*0.5*pornew(ivol)!surface triangle, m3water/m3bulk
              availablewater(ivol)=availablewater(ivol)*cvol(ivol)!m3 water
              availablewater(ivol)=1000*availablewater(ivol)/toparea  !mm water
! unavailable water (between 0 and satwlim + reduction factor alpha) 
              unavailablewater(ivol)=(sanew(ivol) - reduct_factor_water_reservoirs*(sanew(ivol)-satwlim(izn))*0.5)*pornew(ivol)! in m3water/m3bulk (S - ?* x (S-SPFP)x0.5) 
              unavailablewater(ivol)=unavailablewater(ivol)*cvol(ivol)!conversion in m3 water
              unavailablewater(ivol)=1000*unavailablewater(ivol)/toparea  !conversion in mm water
! Exces water = 0
              excesswater(ivol)=r0
              
! satwopt =< sanew =< satwfield => domain 3 (constant with S)
!***************************************************************************************************
           elseif ((sanew(ivol).ge.satwopt(izn)).and.(sanew(ivol).le.satwfield(izn))) then
! max lower reservoir (alpha = 1)
              availablewater(ivol)=0.5*(satwopt(izn)-satwlim(izn))*pornew(ivol)
! add complement available water : portion of rectangle
              availablewater(ivol)=availablewater(ivol)+(sanew(ivol)-satwopt(izn))*pornew(ivol) ! in m3water/m3bulk
! conversions
              availablewater(ivol)=availablewater(ivol)*cvol(ivol)!conversion in m3 water
              availablewater(ivol)=1000*availablewater(ivol)/toparea  !conversion in mm water
! unavailable water is max (rectangle + triangle) 
              unavailablewater(ivol)=(satwlim(izn)+(satwopt(izn)-satwlim(izn))*0.5)*pornew(ivol)! in m3water/m3bulk
              unavailablewater(ivol)=unavailablewater(ivol)*cvol(ivol)!conversion in m3 water
              unavailablewater(ivol)=1000*unavailablewater(ivol)/toparea  !conversion in mm water
! Excess water is 0              
              excesswater(ivol)=r0
              
! sanew > satwfield => domain 4 (linear decrease with S, excess water occurs) 
!***************************************************************************
           else ! ((sanew(ivol).gt.satwfield(izn))
! Calculate max lower reservoir of available water (triangle between wilting point and optimal value, alpha = 1)
              availablewater(ivol) = 0.5*(satwopt(izn)-satwlim(izn))*pornew(ivol)
! calculate baseline available water, defined between Sopt and Sfield (rectangle, alpha = 1), and addition to max lower reservoir          
              availablewater(ivol)=availablewater(ivol)+(satwfield(izn)-satwopt(izn))*pornew(ivol) ! in m3water/m3bulk
! calculate reduction factor alpha (alpha* = S/(Sfield cap - 1) - 1/(Sfield cap - 1))
              reduct_factor_water_reservoirs = (sanew(ivol)/(satwfield(izn)-1)) - (satwfield(izn)-1)**-1
! calculate extra available water
              extra_availablewater = (1-satwfield(izn))-reduct_factor_water_reservoirs*(1-sanew(ivol))*0.5
! total available water
              availablewater(ivol)=availablewater(ivol)+extra_availablewater*pornew(ivol)! in m3water/m3bulk
              availablewater(ivol)=availablewater(ivol)*cvol(ivol)!m3 water
              availablewater(ivol)=1000*availablewater(ivol)/toparea  !mm water
! unavailable water is max (rectangle + triangle) 
              unavailablewater(ivol)=(satwlim(izn)+(satwopt(izn)-satwlim(izn))*0.5)*pornew(ivol)! in m3water/m3bulk
              unavailablewater(ivol)=unavailablewater(ivol)*cvol(ivol)!conversion in m3 water
              unavailablewater(ivol)=1000*unavailablewater(ivol)/toparea  !conversion in mm water
! Excess water(1- alpha*) x (S-Sfield cap) 
              excesswater(ivol)=(1-reduct_factor_water_reservoirs)*(sanew(ivol)-satwfield(izn))*pornew(ivol)! in m3water/m3bulk
              excesswater(ivol)=excesswater(ivol)*cvol(ivol)!conversion in m3 water
              excesswater(ivol)=1000*excesswater(ivol)/toparea  !conversion in mm water        
           endif ! test on sanew to calculate water reservoirs
           
           availablewater_bis(nngl-ivol+1) = availablewater(ivol)
           unavailablewater_bis(nngl-ivol+1) = unavailablewater(ivol)
           excesswater_bis(nngl-ivol+1) = excesswater(ivol)
           availablewater_roots_bis(nngl-ivol+1) = availablewater_roots(ivol)
           unavailablewater_roots_bis(nngl-ivol+1) = unavailablewater_roots(ivol)
           excesswater_roots_bis(nngl-ivol+1) = excesswater_roots(ivol)
           maxavailablewater_bis(nngl-ivol+1) = maxavailablewater(ivol)
           maxexcesswater_bis(nngl-ivol+1) = maxexcesswater(ivol)
           maxavailablewater_roots_bis(nngl-ivol+1) = maxavailablewater_roots(ivol)
           maxexcesswater_roots_bis(nngl-ivol+1) = maxexcesswater_roots(ivol)
           
    end do!ivol
           
! Voilà Teiki, tout est calculé correctement maintenant, à pas journalier. Transferer à Sirius donc
! Recapitulatif. On transfert pour l'instant 6 vecteurs et 2 scalaires
!                           - availablewater,unavailablewater,excesswater dans chaque maille du profil (ivol), en mm
!                           - availablewater_roots,unavailablewater_roots,excesswater_roots dans chaque maille RACINAIRE du profil, en mm
!                           - qroot_evap_tot = actual evaporation (m3/s)
!                           - qroot_transp_tot = actual transpiration (m3/s)
        
!test       
     !  do ivol = 1,nngl
      !     write(*,*) 'aw',ivol, availablewater_roots(ivol)
      !     write(*,*) 'unaw',ivol, unavailablewater_roots(ivol)
      !      write(*,*) 'ex',ivol, excesswater_roots(ivol)
      !    write(*,*) 'maxex',ivol, maxexcesswater_roots(ivol)
      !     write(*,*) 'maxav',ivol, maxavailablewater_roots(ivol)
     !      enddo
!        izn = mpropvs(ivol)
!        if (ivol.eq.2) then
!          write(*,*) satwlim(izn),satwopt(izn),satwfield(izn)
!          write(*,*) 'sat',sanew(ivol)
!          write(*,*) 'aw',availablewater(ivol)
!          
!          write(*,*) 'ew',excesswater(ivol)
!        endif
!       end do

    return
    end