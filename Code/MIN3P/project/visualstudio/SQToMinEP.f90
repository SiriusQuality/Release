 subroutine SQToMinEP(ep)
!DEC$ ATTRIBUTES STDCALL, ALIAS:"SQTOMINEP" , DLLEXPORT :: SQToMinEP
 
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
        write(*,*) 'ep de Min3p : ',ep_sirius
    return 
end
