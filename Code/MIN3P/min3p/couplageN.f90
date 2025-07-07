 subroutine couplageN(np)
!DEC$ ATTRIBUTES STDCALL, ALIAS:"COUPLAGEN" , DLLEXPORT :: couplageN      
 
    use parm
    use gen
    use chem
    use dens
    use phys
    use bbls
    use biol
    use writeversion
      
    real (type_r8) :: np
        
        np_sirius = np
        write(*,*) 'np de Min3p : ',np_sirius
    return 
end
