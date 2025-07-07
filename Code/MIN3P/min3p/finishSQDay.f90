subroutine finishSQDay()
!DEC$ ATTRIBUTES STDCALL, ALIAS:"FINISHSQDAY" , DLLEXPORT :: finishSQDay      
 
    use parm
    use gen
    use chem
    use dens
    use phys
    use bbls
    use biol
    use writeversion  
    
        siriusDayDone = .true.
    return 
end
