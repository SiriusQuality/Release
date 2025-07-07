 subroutine SQToMinFertilization(ferti)
!DEC$ ATTRIBUTES STDCALL, ALIAS:"SQTOMINFERTILIZATION" , DLLEXPORT :: SQToMinFertilization
 
    use parm
    use gen
    use chem
    use dens
    use phys
    use bbls
    use biol
    use writeversion
      
    real (type_r8) :: ferti
    integer :: ic,n_no3,n_nh4
    integer :: ivol,izn
    real*8 :: depth_ferti,amount_ferti,ferti_no3,ferti_nh4
    
    if (reactive_transport) then
        do ic = 1, nc-1
            if (namec(ic) .eq. 'no3-1') then
                n_no3=ic
            elseif (namec(ic) .eq. 'nh4+1') then
                n_nh4=ic 
            end if
        end do
    end if
        
    ! depth_ferti = ceiling((depth_wanted / zmax(nzz)) * nngl --- a utiliser quand on aura depth_wanted(in meters) de SQ
    depth_ferti = nngl  !num of control volume
    
    
    
    amount_ferti = (ferti * toparea) / 14.0067             ! ferti(gNO3NH4/m²) * toparea(m²) / 62.0049 molar mass of NO3 (gNO3/mol) and 18.0385 molar mass of NH4 (gNH4/mol)
                                                                            ! amount_ferti (mol) => to get the mol by value of ammonoNitrate from SQ by toparea (m²)
    ferti_water = (pornew(depth_ferti) * sanew(depth_ferti) * cvol(depth_ferti)) * 1000  ! liter => porosity(unitless) * saturation(unitless) * cvol(m3bulk) * 1000(m3 to liter)
    ferti_no3 = amount_ferti / ferti_water                                ! mol/l
    ferti_nh4 = amount_ferti / ferti_water                                ! mol/l
    
    totcold(n_no3,depth_ferti) = totcold(n_no3,depth_ferti) + ferti_no3
    totcold(n_nh4,depth_ferti) = totcold(n_nh4,depth_ferti) + ferti_nh4
    return 
end
