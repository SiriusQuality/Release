!c ----------------------------------------------------------------------
!c subroutine updtrootdensity
!c -------------------
!c
!c update root density field
!c
!c		    Celine Blitz Frayret (CBF) and Frédéric Gérard (FG) - from May 9, 2017 - According to the MIN3P-ArchiSimple
!c      
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c 
!c                                                                    I O
!c passed:   -
!c
!c common:   
!c gen.f:    real*8:
!c           -------
!c           
!c           time_io            = current solution time (I/O units)   + -
!c           time_soi           = next read time for etp specific    * +
!c                                parameters
!c           
!c
!c           integer*4:
!c           ----------
!c           ilog               = unit number - logbok file           + -
!c           isoi               = unit number - soil specific         + - 
!c                                parameters
!c
!c biol.f:   real*8:
!c           -------
!c           time_rld           = update time for root density
!c 
!c xyzcoord.f real*8:
!c            --------
!c           delx(nvx)          = spatial increment in x-direction    * +
!c           dely(nvy)          = spatial increment in y-direction    * +
!c           delz(nvz)          = spatial increment in z-direction    * +
!c
!c      
!c local:    double precision:
!c           ----------------
!c           tiny               = small increment
!            
!            integer:
!            -------
!c           ivol               = counter for control volumes
!c
!c external: -
!c ---------------------------------------------------------------------
 
      subroutine updtrootdensity
       
 
!      use, intrinsic :: ISO_C_BINDING
      use parm
      use gen
      use phys
      use biol
      
      
    use chem
    use dens
    use bbls
    use writeversion
         
      Implicit none 
      
      
      integer::ivol, ii, i, n_no3, n_nh4,ic !TR
      real :: dump_var,tpmin,epmin
      character*40::dump_var2
      logical::first = .false.
      

	  double precision::tiny,sumrld !norme f98 pour real*8 en f77, nota: sumrld used to check if no root

      parameter (tiny = 1.d-10)
      character (len=90) :: file_name
      
!-------------
        write(*,*) inside_rld, ivol
      if(inside_rld) then
          
                if (time_io.gt.time_rld-tiny) then!update root each day  
             
                        do ivol=1,1250 ! less growth moitie inf du profil de 50x50 dans transpevap.dat
                
                            rld(ivol) = rld(ivol)+0.01
                        
                        enddo
                        
                        do ivol=1251,nn ! more growth moitie sup du profil de 50x50 dans transpevap.dat
                
                            rld(ivol) = rld(ivol)+0.02
                        
                        enddo
                    
                        write(*,*) 'RSD internal update'           
                    
                        time_rld=time_rld+1.0d0 ! increment time
                        
!                        pause

                endif
       
      endif       
      
        
!! update by coupling with root model archisimple!FG implementation should be redone in this version 

     if(coupled_archi_rld) then
         
                if (time_io.gt.time_rld-tiny) then!update root each day  
             
                        do ivol=1,nn/2 ! less growth moitie inf du profil de 50x50 dans transpevap.dat
                
                            rld(ivol) = rld(ivol)+0.01
                        
                        enddo
                        
                        do ivol=(nn/2)+1,nn ! more growth moitie sup du profil de 50x50 dans transpevap.dat
                
                            rld(ivol) = rld(ivol)+0.02
                        
                        enddo
                    
                        !write(*,*) 'RSD "external" updated-temp'
                    
                        time_rld=time_rld+1.0d0 ! increment time

                endif!update time check
          
     endif ! CB internal or coupled_archi_rld 
     
     
     ! update by coupling with root model archisimple of Sirius
     if(coupled_sirius) then
         
                if (time_io.gt.time_rld-tiny) then!update root each day
                     
                        do ivol=1,nn
                            rld(ivol) = rld_sirius(nn-ivol+1)
                        enddo              
                        
                        !write(*,*) 'RSD "external" updated-temp' 
                    
                        time_rld=time_rld+1.0d0 ! increment time

                endif!update time check
          
     endif ! CB internal or coupled_archi_rld 

!FG July 2017 Write new rsd in tecplot readable files. Should work for 1D,2D and 3D simulations and may contain additional variables
                
                        ii=time_io

                        if(time_io.lt.10)then
                          write (file_name, '("Rootdens", I1,".txt")' ) ii
                        else
                          write (file_name, '("Rootdens", I2,".txt")' ) ii
                        endif
                        if(time_io.ge.100)then
                          write (file_name, '("Rootdens", I3,".txt")' ) ii
                        endif
                        
                       file_name = 'RootDens/'//prefix(:l_prfx)//'_'//file_name
                       
                       open(1112,file=file_name,status='unknown',&
     &                 form='formatted')
                
                       write(1112,*) 'variables = "x", "y", "z", "Root Length (cm m<sup>-2</sup>)","NO3","NH4"' !add other variables in this file please
                       
                 !FG to make tecplot readable files
                        tpmin = pet - pe_soil - canopy_int * canopy_evap_factor
                        epmin = pet * sec_per_days
                        
    !mujica2022 stoped to write the description line because tecplot was causing problems with this:
                        
   !                    write(1112,'(a,1pe10.3,1x,a,3(a,i5),4(a,e15.7))') 'zone t = "RSD, T = ',&
  !   &                      time_io,time_unit(:l_time_unit),'", i =',nvx,', j =',nvz,', k =',nvy,',  f=point, TPSirius =', tp_sirius,', EPSirius =', ep_sirius,', TPmin3p =', tpmin,', EPmin3p =', epmin
                       
                       if (reactive_transport) then
                        do ic = 1, nc-1
                            if (namec(ic) .eq. 'no3-1') then
                                n_no3=ic
                            elseif (namec(ic) .eq. 'nh4+1') then
                                n_nh4=ic 
                            end if
                        end do
                       end if
                       
                       do ivol=1,nn
                 
                         !write(1112,'(6e15.7)') xg(ivol),yg(ivol),(((zg(ivol)-zmax(nzz)))*-1),rld(ivol),totcnew(n_no3,ivol),totcnew(n_nh4,ivol) !Mujica 2022
                         
                         !Write(*,*) rld (ivol), cvol(ivol), (rld(ivol))
                       enddo
                
                       close(1112)
                       
!FG jan 2016 : update binary matrix for transpiration

     call binmattransp ! redesigned for n root systems

! FG feb 2016 : check if new sum rld = 0
      sumrld=0.0d0
        do ivol=1,nn ! loop over control volumes
          sumrld=sumrld+rld(ivol)
        enddo
 
! FG Feb 2015 check if no root developed yet
    if (sumrld.eq.0) then !operator set to true is no root, and set to false if root (it first appears in initplant.f
                          !if initially no root, here it is after update)
        rootdensitynill=.true.
    else
        rootdensitynill=.false.
    endif
	
    return

    end
