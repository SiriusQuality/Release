 subroutine couplage(tp,ep,rld_new,dayDone)
!DEC$ ATTRIBUTES STDCALL, ALIAS:"COUPLAGE" , DLLEXPORT :: couplage      
 
    use parm
    use gen
    use chem
    use dens
    use phys
    use bbls
    use biol
    use writeversion
      
    real (type_r8) :: tp, ep
    real*8 :: rld_new(nn)
    logical :: dayDone
    
        do ivol=1,nn
            rld_sirius(ivol) = rld_new(ivol)
        enddo  
        
        tp_sirius = tp
        ep_sirius = ep
        pe_soil = ep
        siriusDayDone = dayDone
        write(*,*) 'tp et ep de Min3p : ',tp_sirius,ep_sirius
    return 
end
