!
!FG sept 2021 to calculate and write at each time step domain-scale mineral rates (mol/domain/day)
!             and reacted mineral mass (mol/domain), in *.dmr and *.drm
!
    
    subroutine output_min
    
      use gen
      use chem
      use file_unit, only : lun_get

! local variables
      integer :: im,ivol

! open files and write headings  
      
      if (i_first) then
          
       i_first=.false.
       
       idmr=lun_get()
       idmm=lun_get()
     
       open(idmr,file=prefix(:l_prfx)//'.dmr',status='unknown',form='formatted')
       open(idmm,file=prefix(:l_prfx)//'.dmm',status='unknown',form='formatted')
      
       write(idmr,'(1000a)') 'time',(',',namem(im),',',im=1,nm)
       
       write(idmm,'(1000a)') 'time',(',',namem(im),',',im=1,nm)
       
      endif
! calculate and write to calculate domain-scale mineral rates (mol/domain/day)
! cumulated domain-scale reacted mineral mass (mol/domain)
      
        do im=1,nm
            
          sum_ratemin=0.0d0!reinitialized as not cumulated over time
          sum_rmm=0.0d0!reinitialized as not cumulated over time
          
         do ivol=1,nngl
             
          ratemdp(im,ivol)=ratemdp(im,ivol)*cvol(ivol)!convert ratemdp from mol/lbulk/day into mol/day
          sum_ratemin(im)=sum_ratemin(im)+ratemdp(im,ivol)!mol/domain/day
          
         enddo
          
          sum_rmm(im)=sum_ratemin(im)*delt_io!reacted masses in mol/domain
          
!          sum_rmm_cumul(im)=sum_rmm_cumul(im)+sum_rmm(im)!cumulated value over time
          
        enddo
       
      write(idmr,'(50e15.7)') time_io,(sum_ratemin(im),im=1,nm)
      write(idmm,'(50e15.7)') time_io,(sum_rmm(im),im=1,nm)!,(sum_rmm_cumul(im),im=1,nm)
       
     return
     end