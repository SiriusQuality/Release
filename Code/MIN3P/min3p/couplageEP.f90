 subroutine couplage(ep)
!DEC$ ATTRIBUTES STDCALL, ALIAS:"COUPLAGEEP" , DLLEXPORT :: couplageEP
 
    use parm
    use gen
    use chem
    use dens
    use phys
    use bbls
    use biol
    use writeversion
      
    real (type_r8) :: ep
        
        ep_sirius = ep
        pe_soil = ep
        write(*,*) 'ep de Min3p : 'ep_sirius
    return 
end
