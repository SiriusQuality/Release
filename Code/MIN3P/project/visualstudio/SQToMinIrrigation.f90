 subroutine SQToMinIrrigation(irrig)
!DEC$ ATTRIBUTES STDCALL, ALIAS:"SQTOMINIRRIGATION" , DLLEXPORT :: SQToMinIrrigation
 
    use parm
    use gen
    use chem
    use dens
    use phys
    use bbls
    use biol
    use writeversion
      
    real (type_r8) :: irrig
        
        irrig_sirius = irrig
        write(*,*) 'irrig de Min3p : ',irrig_sirius
    return 
end
