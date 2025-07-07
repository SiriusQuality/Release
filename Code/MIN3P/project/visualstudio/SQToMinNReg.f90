 !Mujica 2022
    
    subroutine SQToMinNReg(nAfter, nBefore)
    !DEC$ ATTRIBUTES STDCALL, ALIAS:"SQTOMINNREG" , DLLEXPORT :: SQToMinNReg

    
 
    use parm
    use gen
    use chem
    use dens
    use phys
    use bbls
    use biol
    use writeversion
      
   logical :: NReg
    
   
   real*8 ::  nAfter, nAftermol, NToReincorporate, nBefore, nBeforemol, molNadded, sum_raten,totvolumen, SQuptakemol, nmin3pUncoupled,nmin3pCoupled, no3Uptakemin3ptosqcontrolmol
   integer :: timeday
              
        !totcnew(1) = totcnew(1) + ((nAfter/14.0067)*toparea)/(1000*0.495) !volumen del dominio y porosidad   
            
        if (time > 2) then
             
        do ivol=1,nngl
            !sum_raten = sum_raten+(ratemdp(1,ivol)/cvol(ivol)) !ratemdp in which unit is it? was converted into mol/lbulk/day into mol/day
            sum_raten = sum_raten + (ratemdp(1,ivol)) !
            totvolumen = totvolumen + (cvol(ivol)*pornew(ivol)*sanew(ivol)*1000) !volume of water in the domain m3
        enddo
        

        nAftermol = (((nAfter/14.0067)*toparea)/totvolumen) ![gN m-2] to [mol l-1]
             
        nBeforemol = (((nBefore/14.0067)*toparea)/totvolumen)
                                     
        SQuptakemol = (nBeforemol - nAftermol) 
        
        no3Uptakemin3ptosqcontrolmol=(((no3Uptakemin3ptosqcontrol/14.0067)*toparea)/totvolumen)
        
                                      
        do ivol=1,nngl
             
             !value=(nAfter/14.0067)*toparea/cvol(ivol)/pornew(ivol)/1000.0e0/float(nngl)
            
             ! nAftermol = (nAfter/14.0067)*toparea/cvol(ivol)/pornew(ivol)/1000.0e0
                                              
                NToReincorporate = ((ratemdp(1,ivol)/ sum_raten) * nAftermol) !proportion of precipitated(uptake) nitrogen multiplied by nitrogen to reincorporate in the soil solution => coef * mol/l = mol/l
             
                !value = value + ((ratemdp(1,ivol)/(cvol(ivol)*pornew(ivol)*sanew(ivol)*1000.0e0) )/ sum_raten) !sum of the proportions of reincorpoation in each soil layer should be = 1
                                        
              if (NToReincorporate >= 0.0 ) then !not required, nafter will never be less than 0 
             
                totcold(1,ivol) = totcold(1,ivol) + NToReincorporate                 ! y setear nregcoeff a 1 siempre  o eliminar lo de nregcoeff 
                molNadded = molNadded + NToReincorporate
              else
                    molNadded = 0.0E0
              endif
              
              
         end do
        endif 
        
        nmin3pUncoupled = ((no3_day_uptake)/totvolumen)
        
        nmin3pCoupled = nmin3pUncoupled - molNadded + 1.0E-20

        
    if (time < 0.0000000001) then
        
         write (1800,'(A210)') 'variables = " Time [days] "," N reincorporated in MIN3P [mol/l] "," Nitrogen in excess in SQ [mol/l]"," N uptake in MIN3P Uncoupled [mol/l] "," N uptake in MIN3P Coupled [mol/l] ","SQ uptake coupled [mol/l] " '
            
    else
         
         write (1800,'(ES15.3E2, ES15.3E2, ES15.3E2, ES15.3E2, ES15.3E2, ES15.3E2)') time, molNadded, nAftermol, nmin3pUncoupled , nmin3pCoupled, SQuptakemol 
            
    endif 
    
    no3_day_uptake = 0.0e0 
    return 
end