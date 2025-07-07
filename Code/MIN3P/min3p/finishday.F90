subroutine finishday(finish)
!DEC$ ATTRIBUTES STDCALL, ALIAS:"FINISHDAY" , DLLEXPORT :: finishday
    !DEC$ ATTRIBUTES REFERENCE :: finish
 
    use parm
    use gen
    use chem
    use dens
    use phys
    use bbls
    use biol
    use writeversion
    
    logical,intent(inout) :: finish
    
        finish = min3pDayFinished
    return 
    end