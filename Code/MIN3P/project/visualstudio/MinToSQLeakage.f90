subroutine MinToSQLeakage(leakagewater,leakageN)
    !DEC$ ATTRIBUTES STDCALL, ALIAS:"MINTOSQLEAKAGE", DLLEXPORT :: MinToSQLeakage
    !DEC$ ATTRIBUTES REFERENCE :: leakagewater
    !DEC$ ATTRIBUTES REFERENCE :: leakageN
    
    use parm
    use gen
    use phys
    use biol
    use chem
    
    Implicit none 
    
    integer :: ic,n_no3,n_nh4
    real*8, parameter :: r0 = 0.0d0
    real*8, intent(inout) :: leakagewater,leakageN
           
           
    if (reactive_transport) then
        do ic = 1, nc-1
            if (namec(ic) .eq. 'no3-1') then
                n_no3=ic
            elseif (namec(ic) .eq. 'nh4+1') then
                n_nh4=ic 
            end if
        end do
    end if
       
    !calculate the amount of water lost which is equal to the velocity of the last layer
    call velocity (nvxgl, nvygl, nvzgl, iavs, javs,cinfvs_a,dimcv, &
     &                 xg, yg, zg, uvsnew, hhead, relperm, idbg, ilog, &
     &                 ivel, upstream, fully_saturated, njavs, nngl,   &
     &                 nn, half_cells,cinfrad,radial_coord)
           
    if  (lost_water.lt.r0) then                                                                         !Mujica 2022
        
        leakageN = lost_water    !in m/day
        leakageN = leakageN * toparea * 1000 !in liter (or in dm3/day)
        leakageN = (leakageN * totcold(n_nh4,nngl)) + (leakageN * totcold(n_no3,nngl)) !in mol/day
        leakageN = (leakageN * 14.0067) / toparea    !in gN/m²/day
           
        leakagewater = lost_water !in m/day
        leakagewater = leakagewater * 1000000.0  !in gWater/day
        leakagewater = leakagewater / toparea    !in gWater/m²/day
        
    else
        leakageN = -1.0d-25    !in m/day                                                                !Mujica 2022
        leakageN = leakageN * toparea * 1000 !in liter (or in dm3/day)                                  !Mujica 2022
        leakageN = (leakageN * totcold(n_nh4,nngl)) + (leakageN * totcold(n_no3,nngl)) !in mol/day      !Mujica 2022
        leakageN = (leakageN * 14.0067) / toparea    !in gN/m²/day                                      !Mujica 2022
           
        leakagewater = -1.0d-25 !in m/day                                                               !Mujica 2022
        leakagewater = leakagewater * 1000000.0  !in gWater/day                                         !Mujica 2022
        leakagewater = leakagewater / toparea    !in gWater/m²/day                                      !Mujica 2022
        lost_water=-1.0d-25
    endif
       
           
    write(3001,*) lost_water,leakagewater,leakageN

           
    return
end