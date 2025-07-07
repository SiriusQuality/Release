 subroutine SQToMinNP(avNo3)
!DEC$ ATTRIBUTES STDCALL, ALIAS:"SQTOMINNP" , DLLEXPORT :: SQToMinNP    
 
    use parm
    use gen
    use chem
    use dens
    use phys
    use bbls
    use biol
    use writeversion
      
    integer :: ivol,ic,n_no3,n_nh4
    real (type_r8) :: avNo3(nngl)
    real*8, parameter :: r0 = 0.0d0
    real*8 :: available_no3(nngl), available_nh4(nngl)
      
    
    if (reactive_transport) then
        do ic = 1, nc-1
            if (namec(ic) .eq. 'no3-1') then
                n_no3=ic
             elseif (namec(ic) .eq. 'nh4+1') then
                n_nh4=ic 
            end if
        end do
    end if
    
    ! Main loop over control volumes        
    do ivol = 1,nngl
         
         available_no3(ivol) = r0
         available_no3(ivol) = avNo3(ivol)*toparea                                              ! in gN-NO3
         available_no3(ivol) = available_no3(ivol) / 14.0067                                    ! in mol
         available_no3(ivol) = available_no3(ivol) / (cvol(ivol) * pornew(ivol) * sanew(ivol))   ! in mol/m3  
         
         write(1500,*) ivol, totcnew(n_no3,ivol)
         totcnew(n_no3,ivol) = totcnew(n_no3,ivol) - (available_no3(ivol) / 1000)                                      ! in mol/l
         write(1500,*) ivol, totcnew(n_no3,ivol)
         
         available_nh4(ivol) = r0
         available_nh4(ivol) = avNo3(ivol)*toparea                                              ! in gN-NH4
         available_nh4(ivol) = available_nh4(ivol) / 14.0067                                    ! in mol
         available_nh4(ivol) = available_nh4(ivol) / (cvol(ivol) * pornew(ivol) * sanew(ivol))   ! in mol/m3  
         totcnew(n_nh4,ivol) = totcnew(n_nh4,ivol) - (available_nh4(ivol) / 1000)                                       ! in mol/l
         
         
         
    end do
           
    return 
end

