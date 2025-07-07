 subroutine couplageRLD(rld_new)
!DEC$ ATTRIBUTES STDCALL, ALIAS:"COUPLAGERLD" , DLLEXPORT :: couplageRLD
 
    use parm
    use gen
    use chem
    use dens
    use phys
    use bbls
    use biol
    use writeversion
      
    real*8 :: rld_new(nn)
    
        do ivol=1,nn
            rld_sirius(ivol) = rld_new(ivol)
        enddo
    return 
end
