 subroutine couplage(tp)
!DEC$ ATTRIBUTES STDCALL, ALIAS:"COUPLAGETP" , DLLEXPORT :: couplageTP  
 
    use parm
    use gen
    use chem
    use dens
    use phys
    use bbls
    use biol
    use writeversion
      
    real (type_r8) :: tp
        
        tp_sirius = tp
        write(*,*) 'tp de Min3p : ',tp_sirius
    return 
end
