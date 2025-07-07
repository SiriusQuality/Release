! Calculate daily values of global variables used by Sirius
! Written by Frédéric Gérard, June-October 2021
        
      subroutine MinToSQN(available_no3_bis,unavailable_no3_bis,excess_no3_bis,available_no3_roots_bis,unavailable_no3_roots_bis,excess_no3_roots_bis,  &
     &          available_nh4_bis,unavailable_nh4_bis,excess_nh4_bis,available_nh4_roots_bis,unavailable_nh4_roots_bis,excess_nh4_roots_bis)
      !DEC$ ATTRIBUTES STDCALL, ALIAS:"MINTOSQN", DLLEXPORT :: MinToSQN
        !DEC$ ATTRIBUTES REFERENCE :: available_no3_bis
        !DEC$ ATTRIBUTES REFERENCE :: unavailable_no3_bis
        !DEC$ ATTRIBUTES REFERENCE :: excess_no3_bis
        !DEC$ ATTRIBUTES REFERENCE :: available_no3_roots_bis
        !DEC$ ATTRIBUTES REFERENCE :: unavailable_no3_roots_bis
        !DEC$ ATTRIBUTES REFERENCE :: excess_no3_roots_bis
        !DEC$ ATTRIBUTES REFERENCE :: available_nh4_bis
        !DEC$ ATTRIBUTES REFERENCE :: unavailable_nh4_bis
        !DEC$ ATTRIBUTES REFERENCE :: excess_nh4_bis
        !DEC$ ATTRIBUTES REFERENCE :: available_nh4_roots_bis
        !DEC$ ATTRIBUTES REFERENCE :: unavailable_nh4_roots_bis
        !DEC$ ATTRIBUTES REFERENCE :: excess_nh4_roots_bis
    
      use parm
      use gen
      use phys
      use biol
      use chem!FG sept 2021
         
      Implicit none 
            
      integer :: ivol,izn
      integer :: ic,n_no3,n_nh4,im,ireac!FG sept 2021
      real*8 :: reduct_factor_water_reservoirs,extra_availablewater
      real*8 :: depth_ferti,amount_ferti,ferti_no3,ferti_nh4
      real*8, parameter :: r0 = 0.0d0
      
      real*8 :: availablewater(nngl),unavailablewater(nngl),excesswater(nngl),                    &
     &          availablewater_roots(nngl),unavailablewater_roots(nngl),excesswater_roots(nngl),  &
     &          maxavailablewater(nngl),maxexcesswater(nngl),maxavailablewater_roots(nngl),       &
     &          maxexcesswater_roots(nngl)
      
      real*8 :: available_no3_roots(nngl),unavailable_no3_roots(nngl),excess_no3_roots(nngl),     &!FG Sept 2021
     &          available_nh4_roots(nngl),unavailable_nh4_roots(nngl),excess_nh4_roots(nngl),       &
     &          available_no3(nngl),unavailable_no3(nngl),excess_no3(nngl),                       &
     &          available_nh4(nngl),unavailable_nh4(nngl),excess_nh4(nngl) 
      
        real*8, intent(inout) :: available_no3_roots_bis(nngl),unavailable_no3_roots_bis(nngl),excess_no3_roots_bis(nngl),     &!FG Sept 2021
     &          available_nh4_roots_bis(nngl),unavailable_nh4_roots_bis(nngl),excess_nh4_roots_bis(nngl),       &
     &          available_no3_bis(nngl),unavailable_no3_bis(nngl),excess_no3_bis(nngl),                       &
     &          available_nh4_bis(nngl),unavailable_nh4_bis(nngl),excess_nh4_bis(nngl) 
      
      !real*8,intent(inout) :: no3Uptake
      
        
! If reactive transport, get and store component number (ic) corresponding to NO3- and NH4+ !FG sept 2021
        
            if (reactive_transport) then
                do ic = 1, nc-1
                    if (namec(ic) .eq. 'no3-1') then
                        n_no3=ic
                    elseif (namec(ic) .eq. 'nh4+1') then
                        n_nh4=ic 
                    end if
                end do
                
!FG sept 2021 - Calculate N-uptake rate (g/m2/days) and N-uptake (g/m2)
!               beware, no3 uptake should be the FIRST declared mineral with this coding
                
            !  if (nm.gt.0) then
                  
              !   sum_ratemin(1)=(sum_ratemin(1)*toparea**-1)*14.0067! x PM of N to get grammes
              !   no3Uptake = sum_ratemin(1)
              !   write(*,*) 'la valeur de no3uptake = ',sum_ratemin(1)
              !   
              !   sum_rmm(1)=(sum_rmm(1)*toparea**-1)*14.0067! x PM of N to get grammes
                 
             ! endif
              
                
            
            endif!reactive transport
            
! Main loop over control volumes        
    do ivol = 1,nngl
! assign material properties zones
         izn = mpropvs(ivol)
         if (BINT(ivol)) then !it is false of no root in the control volume
! max available water and max excess water in mm!FG August 2021
           maxavailablewater_roots(ivol)=0.5*(satwopt(izn)-satwlim(izn))+(satwfield(izn)-satwopt(izn))+(1-satwfield(izn))
           maxavailablewater_roots(ivol)=maxavailablewater_roots(ivol)*pornew(ivol)*cvol(ivol)*1000/toparea!from m3water/m3porosity to mm water
           maxexcesswater_roots(ivol)= 0.5*(1-satwfield(izn))*pornew(ivol)*cvol(ivol)*1000/toparea!mm water
           
! *******************************************           
! water reservoirs in ROOT control volumes
! *******************************************
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
        
!FG sept 2021   -------------------------------------------------------------        
! *******************************************           
! Nitrogen reservoirs in ROOT control volumes
! *******************************************
! nitrates       
             available_no3_roots(ivol)= availablewater_roots(ivol)*toparea              ! liter (water pool in mm x toparea (m2) => divided by 1000 to get m3 and multiplied by same factor to get dm3)
             available_no3_roots(ivol)=totcnew(n_no3,ivol)*available_no3_roots(ivol)    ! conversion in mol
             available_no3_roots(ivol)=available_no3_roots(ivol)*14.0067                ! conversion in g of N as NO3 (N-NO3)
             available_no3_roots(ivol)=available_no3_roots(ivol)*toparea**-1            ! per units of surface area of soil (gN-NO3/m2)
             
             unavailable_no3_roots(ivol)= unavailablewater_roots(ivol)*toparea          ! liter (water pool in mm x toparea (m2) => divided by 1000 to get m3 and multiplied by same factor to get dm3)
             unavailable_no3_roots(ivol)=totcnew(n_no3,ivol)*unavailable_no3_roots(ivol)! conversion in mol
             unavailable_no3_roots(ivol)=unavailable_no3_roots(ivol)*14.0067            ! conversion in g of N as NO3 (N-NO3)
             unavailable_no3_roots(ivol)=unavailable_no3_roots(ivol)*toparea**-1        ! per units of surface area of soil (gN-NO3/m2)
             
             excess_no3_roots(ivol)= excesswater_roots(ivol)*toparea                    ! liter (water pool in mm x toparea (m2) => divided by 1000 to get m3 and multiplied by same factor to get dm3)
             excess_no3_roots(ivol)=totcnew(n_no3,ivol)*excess_no3_roots(ivol)          ! conversion in mol
             excess_no3_roots(ivol)=excess_no3_roots(ivol)*14.0067                      ! conversion in g of N as NO3 (N-NO3)
             excess_no3_roots(ivol)=excess_no3_roots(ivol)*toparea**-1                  ! per units of surface area of soil (gN-NO3/m2)
             
! ammonium
             available_nh4_roots(ivol)= availablewater_roots(ivol)*toparea              ! liter (water pool in mm x toparea (m2) => divided by 1000 to get m3 and multiplied by same factor to get dm3)
             available_nh4_roots(ivol)=totcnew(n_nh4,ivol)*available_nh4_roots(ivol)    ! conversion in mol
             available_nh4_roots(ivol)=available_nh4_roots(ivol)*14.0067                ! conversion in g of N as NH4 (N-NH4)
             available_nh4_roots(ivol)=available_nh4_roots(ivol)*toparea**-1            ! per units of surface area of soil (gN-NH4/m2)
             
             unavailable_nh4_roots(ivol)= unavailablewater_roots(ivol)*toparea          ! liter (water pool in mm x toparea (m2) => divided by 1000 to get m3 and multiplied by same factor to get dm3)
             unavailable_nh4_roots(ivol)=totcnew(n_nh4,ivol)*unavailable_nh4_roots(ivol)! conversion in mol
             unavailable_nh4_roots(ivol)=unavailable_nh4_roots(ivol)*14.0067            ! conversion in g of N as NH4 (N-NH4)
             unavailable_nh4_roots(ivol)=unavailable_nh4_roots(ivol)*toparea**-1        ! per units of surface area of soil (gN-NH4/m2)
             
             excess_nh4_roots(ivol)= excesswater_roots(ivol)*toparea                    ! liter (water pool in mm x toparea (m2) => divided by 1000 to get m3 and multiplied by same factor to get dm3)
             excess_nh4_roots(ivol)=totcnew(n_nh4,ivol)*excess_nh4_roots(ivol)          ! conversion in mol
             excess_nh4_roots(ivol)=excess_nh4_roots(ivol)*14.0067                      ! conversion in g of N as NH4 (N-NH4)
             excess_nh4_roots(ivol)=excess_nh4_roots(ivol)*toparea**-1                  ! per units of surface area of soil (gN-NH4/m2)
             
         else! no root (BINT(ivol) = false) => all compartments set to 0
             
              availablewater_roots(ivol)=r0
              unavailablewater_roots(ivol)=r0
              excesswater_roots(ivol)=r0
              
              available_no3_roots(ivol)=r0 ! FG sept 2021 
              unavailable_no3_roots(ivol)=r0
              excess_no3_roots(ivol)=r0
              
              available_nh4_roots(ivol)=r0 ! FG sept 2021 
              unavailable_nh4_roots(ivol)=r0
              excess_nh4_roots(ivol)=r0
              
         endif! root or no root
         
!----------------------------------------------------------
        
! *******************************************           
! water reservoirs in ALL control volumes
! *******************************************
!
! max available and excess water!FG August 2021
           maxavailablewater(ivol)=0.5*(satwopt(izn)-satwlim(izn))+(satwfield(izn)-satwopt(izn))+(1-satwfield(izn))
           maxavailablewater(ivol)=maxavailablewater(ivol)*pornew(ivol)*cvol(ivol)*1000/toparea!from m3water/m3porosity to mm water
           maxexcesswater(ivol)= 0.5*(1-satwfield(izn))*pornew(ivol)*cvol(ivol)*1000/toparea!from m3water/m3porosity to mm water

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
           
!FG sept 2021   -------------------------------------------------------------        
! *******************************************           
! Nitrogen reservoirs in all control volumes
! *******************************************
! nitrates       
             available_no3(ivol)= availablewater(ivol)*toparea              ! liter (water pool in mm x toparea (m2) => divided by 1000 to get m3 and multiplied by same factor to get dm3)
             available_no3(ivol)=totcnew(n_no3,ivol)*available_no3(ivol)    ! conversion in mol
             available_no3(ivol)=available_no3(ivol)*14.0067                ! conversion in g of N as NO3 (N-NO3)
             available_no3(ivol)=available_no3(ivol)*toparea**-1            ! per units of surface area of soil (gN-NO3/m2)
             
             unavailable_no3(ivol)= unavailablewater(ivol)*toparea          ! liter (water pool in mm x toparea (m2) => divided by 1000 to get m3 and multiplied by same factor to get dm3)
             unavailable_no3(ivol)=totcnew(n_no3,ivol)*unavailable_no3(ivol)! conversion in mol
              unavailable_no3(ivol)=unavailable_no3(ivol)*14.0067
             unavailable_no3(ivol)=unavailable_no3(ivol)*toparea**-1        ! per units of surface area of soil (gN-NO3/m2)
             
             excess_no3(ivol)= excesswater(ivol)*toparea                    ! liter (water pool in mm x toparea (m2) => divided by 1000 to get m3 and multiplied by same factor to get dm3)
             excess_no3(ivol)=totcnew(n_no3,ivol)*excess_no3(ivol)          ! conversion in mol
             excess_no3(ivol)=excess_no3(ivol)*14.0067                      ! conversion in g of N as NO3 (N-NO3)
             excess_no3(ivol)=excess_no3(ivol)*toparea**-1                  ! per units of surface area of soil (gN-NO3/m2)
             
! ammonium
             available_nh4(ivol)= availablewater(ivol)*toparea              ! liter (water pool in mm x toparea (m2) => divided by 1000 to get m3 and multiplied by same factor to get dm3)
             available_nh4(ivol)=totcnew(n_nh4,ivol)*available_nh4(ivol)    ! conversion in mol
             available_nh4(ivol)=available_nh4(ivol)*14.0067                ! conversion in g of N as NH4 (N-NH4)
             available_nh4(ivol)=available_nh4(ivol)*toparea**-1            ! per units of surface area of soil (gN-NH4/m2)
             
             unavailable_nh4(ivol)= unavailablewater(ivol)*toparea          ! liter (water pool in mm x toparea (m2) => divided by 1000 to get m3 and multiplied by same factor to get dm3)
             unavailable_nh4(ivol)=totcnew(n_nh4,ivol)*unavailable_nh4(ivol)! conversion in mol
             unavailable_nh4(ivol)=unavailable_nh4(ivol)*14.0067            ! conversion in g of N as NH4 (N-NH4)
             unavailable_nh4(ivol)=unavailable_nh4(ivol)*toparea**-1        ! per units of surface area of soil (gN-NH4/m2)
             
             excess_nh4(ivol)= excesswater(ivol)*toparea                    ! liter (water pool in mm x toparea (m2) => divided by 1000 to get m3 and multiplied by same factor to get dm3)
             excess_nh4(ivol)=totcnew(n_nh4,ivol)*excess_nh4(ivol)          ! conversion in mol
             excess_nh4(ivol)=excess_nh4(ivol)*14.0067                      ! conversion in g of N as NH4 (N-NH4)
             excess_nh4(ivol)=excess_nh4(ivol)*toparea**-1                  ! per units of surface area of soil (gN-NH4/m2)
             
!----------------------------------------------------------
           
             available_no3_roots_bis(nngl-ivol+1) = available_no3_roots(ivol)
             unavailable_no3_roots_bis(nngl-ivol+1) = unavailable_no3_roots(ivol)
             excess_no3_roots_bis(nngl-ivol+1) = excess_no3_roots(ivol)
             available_nh4_roots_bis(nngl-ivol+1) = available_nh4_roots(ivol)
             unavailable_nh4_roots_bis(nngl-ivol+1) = unavailable_nh4_roots(ivol)
             excess_nh4_roots_bis(nngl-ivol+1) = excess_nh4_roots(ivol)
             available_no3_bis(nngl-ivol+1) = available_no3(ivol)
             unavailable_no3_bis(nngl-ivol+1) = unavailable_no3(ivol)
             excess_no3_bis(nngl-ivol+1) = excess_no3(ivol)
             available_nh4_bis(nngl-ivol+1) = available_nh4(ivol)
             unavailable_nh4_bis(nngl-ivol+1) = unavailable_nh4(ivol)
             excess_nh4_bis(nngl-ivol+1) = excess_nh4(ivol)
             
    end do!main loop (ivol)
  
    return
    
    end

! Voilà Teiki, tout devrait être calculé correctement, à un pas de temps journalier.
!
! Recapitulatif des variables à transferer à Sirius:
!
! Pour l'eau, on transfert pour l'instant 6 vecteurs et 2 scalaires:
!                           - availablewater,unavailablewater,excesswater dans chaque maille du profil (ivol), en mm
!                           - availablewater_roots,unavailablewater_roots,excesswater_roots dans chaque maille RACINAIRE du profil, en mm
!                           - qroot_evap_tot = actual evaporation (m3/s)
!                           - qroot_transp_tot = actual transpiration (m3/s)
! Pour l'azote, on transfert pour l'instant 12 vecteurs:
!                           - available_no3,unavailable_no3,excess_no3 dans chaque maille du profil (ivol), exprimés en gN-NO3/m2
!                           - available_nh4,unavailable_nh4,excess_nh4 dans chaque maille du profil (ivol), exprimés en gN-NH4/m2    
!                           - available_no3_roots,unavailable_no3_roots,excess_no3_roots dans chaque maille RACINAIRE du profil, exprimés en gN-NO3/m2
!                           - available_nh4_roots,unavailable_nh4_roots,excess_nH4_roots dans chaque maille RACINAIRE du profil, exprimés en gN-NH4/m2
    
!                                     et deux scalaires:
!                           -sum_ratemin(1) et sum_rmm(1), respectivement la vitesse de prélèvement de N-NO3 (g/m2/day) et la masse de N-NO3 prélevée (g/m2)
!   
!test       
!       do ivol = 1,nngl
!        izn = mpropvs(ivol)
!        if (ivol.eq.2) then
!          write(*,*) satwlim(izn),satwopt(izn),satwfield(izn)
!          write(*,*) 'sat',sanew(ivol)
!          write(*,*) 'aw',availablewater(ivol)
!          write(*,*) 'unaw',unavailablewater(ivol)
!          write(*,*) 'ew',excesswater(ivol)
!        endif
!       end do 
!       pause 
!        write (*,*) 'actual transp (m3/s)', qroot_transp_tot
!        write (*,*) 'actual evapo (m3/s)', qroot_evap_tot
!        pause