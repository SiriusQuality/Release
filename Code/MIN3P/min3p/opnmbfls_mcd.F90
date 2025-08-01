!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/opnmbfls_mcd.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c ----------------------------------------------------------------------
!c subroutine opnmbfls_mcd
!c -------------------
!c
!c open mass balance files 
!c
!c written by:      Pejman Rasouli - March 5, 2012
!c
!c last modified:   -
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
!c gen.f:    integer*4:
!c           ----------
!c           icnv               = unit number, data conversion        + -
!c           ifls               = unit number, file information       + -
!c           imcd               = unit number, mass balance -         * *
!c                                             reactive transport
!c           imcd_first         = pointer - first unit number for     * +
!c                                mass balance - reactive transport
!c           imcd_last          = pointer - last unit number for      * +
!c                                mass balance - reactive transport
!c           imvs               = unit number, mass balance -         * *
!c                                             variably saturated
!c                                             flow
!c           imvs_first         = pointer - first unit number for     * +
!c                                mass balance - variably saturated
!c                                               flow
!c           imvs_last          = pointer - last unit number for      * +
!c                                mass balance - variably saturated
!c                                               flow
!c           l_prfx             = length of prefix of I/O files       + -
!c           nmb                = number of mass balance files for    + -
!c                                selected species
!c
!c           logical:
!c           --------
!c           mass_balance_rt    = .true.  -> compute mass balance     + -
!c                                           (reactive tramsport)
!c           mass_balance_vs    = .true.  -> compute mass balance     + -
!c                                           (variably saturated 
!c                                            flow)
!c
!c           character:
!c           ----------
!c           namemb(nmb)        = names of selected species           + -
!c           prefix             = prefix name for all I/O files       + -
!c           time_unit          = time unit for output -> 'years'     + -
!c                                                        'days'
!c
!c chem.f:   integer*4:
!c           ----------
!c           iaic(nsites)       = pointer array for compressed        + -
!c                                storage of surface site data
!c           nanc               = number of sorbed species            + -
!c           naq                = number of intra-aqueous kinetic     + -
!c                                reactions
!c           nc                 = number of components including h2o  + -
!c           nm                 = number of minerals                  + -
!c           ng                 = number of gases                     + -
!c           nsb                = number of sorbed species            + -
!c           nsb_ion            = number of sorbed species            + -
!c                                (ion-exchange)
!c           nsb_surf           = number of sorbed species            + -
!c                                (surface-complex)
!c           nsites             = number of surface sites             + -
!c
!c           character:
!c           ----------
!c           isotherm_type(nc)  = definition of sorption isotherm     + -    
!c                                'none' = no sorption
!c                                'linear' = linear adsorption
!c                                'freundlich' = Freundlich isotherm
!c                                'langmuir' = Langmuir isotherm
!c           nameanc(nc)        = names of sorbed species             + -
!c                                (non-competitive sorption)
!c           nameaq(naq)        = names of intra-aqueous kinetic      + -
!c                                reactions
!c           namec(nc)          = component names                     + -
!c           nameg(ng)          = names of gases                      + -
!c           namem(nm)          = mineral names                       + -
!c           namesb(nsb)        = names of sorbed species             + -
!c           namesb_ion(nsb_ion)= names of sorbed species             + -
!c                                (ion-exchange)
!c           namesb_surf(nsb_surf)= names of sorbed species           + -
!c                                  (surface-complex)
!c
!c           logical:
!c           --------
!c           noncompetitive_sorption = logical array for activation   + -  
!c                                     of noncompetitive sorption
!c                                     reactions
!c
!c local:    integer*4:
!c           ----------
!c           ianc               = counter (non-competitive sorption
!c                                         reactions)
!c           iaq                = counter (intra-aqueous kinetic 
!c                                         reactions)
!c           ic                 = counter (components)
!c           ig                 = counter (gases)
!c           im                 = counter (minerals)
!c           imb                = counter (selected species)
!c           isb                = counter (sorbed species)
!c           isites             = counter (surface sites)
!c           l_sufx             = length of file suffix
!c
!c           character:
!c           ----------
!c           suffix             = file suffix
!c
!c external: -
!c ----------------------------------------------------------------------
 
      subroutine opnmbfls_mcd
 
      use parm
      use gen
      use chem
      use file_unit, only : lun_get, lun_set
      use module_binary_mpiio, only : binary_file_open,               &
                                       tecplot_binary_write_header,    &
                                       tecplot_binary_write_variable,  &
                                       tecplot_binary_write_zoneinfo,  &
                                       tecplot_binary_write_section
      
      implicit none
#ifdef PETSC_V3_6_X
#include <petsc/finclude/petscsys.h>
#elif PETSC
#include <finclude/petscsys.h>
#endif
      
      integer :: i, ic, imb, l_sufx

      character*2 suffix
      character*2048 :: strbuffer      
      character*72, allocatable :: tec_variables(:)
      integer :: nvarsimcd, ilun, nlun
      
      if (rank == 0 .and. b_enable_output .and. b_output_binary) then
        allocate(tec_variables(nc+nmb+15))
        tec_variables = ''
      end if

!c     write(*,'(/a)') 'enter routine opnmbfls_mcd ...'
 
 
!c  mass balance - reactive transport - Multicomponent Diffusion Option

      if (flux_out) then

        !imcd_first = 203
        imcd_first = lun_get()
        nlun = n+2
        do ilun = 1, nlun
          call lun_set(imcd_first+ilun)
        end do

        write(ifls,'(//72a/a/72a)')    &
     &       ('*',i=1,72),        &
     &    'mass balance - reactive transport- Multicomponent Diffusion',    &
     &       ('*',i=1,72)

!c  total system mass for components

        imcd = imcd_first
        if (rank == 0 .and. b_enable_output .and. b_output_binary) then
          allocate(imcd_mpi(imcd_first:imcd_first+n+2))
          imcd_mpi = 0
          allocate(offset_imcd(imcd_first:imcd_first+n+2))
          offset_imcd = 0
          allocate(offset_imcd_ijk(imcd_first:imcd_first+n+2))
          offset_imcd_ijk = 0
        end if
        
        if (rank == 0 .and. b_enable_output) then
          if (b_output_binary) then
#ifndef MPI
            if (imcd_mpi(imcd) < 10) then
              imcd_mpi(imcd) = lun_get()
            end if
#endif
            call binary_file_open(PETSC_COMM_SELF,             &
                         imcd_mpi(imcd), prefix(:l_prfx)//'_o.masmcd', &
                         .true.)
          else
            open(imcd,file=prefix(:l_prfx)//'_o.masmcd',               &
                      status='unknown',form='formatted')
          end if
          
!c  version information
          if (b_writeversion_tecplot .and. .not. b_output_binary) then
            call writeversion2file(imcd, "#")
          end if            
          
          if (b_output_binary) then
            nvarsimcd = nc  
            tec_variables(1) = "time"
            do ic = 1,nc-1
              tec_variables(ic+1) = trim(namec(ic))
            end do
            strbuffer = "system mass - aqueous phase"
            
            offset_imcd(imcd) = 0  
            call tecplot_binary_write_header(PETSC_COMM_SELF,          &
                         imcd_mpi(imcd), "#!TDV102",'dataset '//       &
                         prefix(:l_prfx),offset_imcd(imcd),.true.,     &
                         .true.)                                       
                                                                       
            call tecplot_binary_write_variable(PETSC_COMM_SELF,        &
                         imcd_mpi(imcd), nvarsimcd,                    &
                         tec_variables(1:nvarsimcd),                   &
                         offset_imcd(imcd),.true.,.true.)                 
                                                                       
            call tecplot_binary_write_zoneinfo(PETSC_COMM_SELF,        &
                         imcd_mpi(imcd),trim(strbuffer),               &
                         offset_imcd(imcd), 1, 1, 1, .true.,.true.,    &
                         b_output_multizone)                           
            offset_imcd_ijk(imcd) = offset_imcd(imcd) - 5*4            
                                                                       
            call tecplot_binary_write_section(PETSC_COMM_SELF,         &
                         imcd_mpi(imcd),nvarsimcd,0,offset_imcd(imcd), &
                         .true.,.true.,b_output_multizone) 
          else
            write(imcd,'(3a)') 'title = "dataset ',prefix(:l_prfx),'"' 
            
            strbuffer = 'variables = "time"'
            do ic = 1,nc-1
              strbuffer = trim(strbuffer)//', "'//trim(namec(ic))//'"'
            end do
            
            write(imcd,'(a)') trim(strbuffer)            
            write(imcd,'(2a)')                                         &
                  'zone t = "system mass - aqueous phase", f=point'
          end if
          
          write(ifls,'(/a/72a/)') 'system mass - aqueous phase',       &
     &                            ('-',i=1,72)
          write(ifls,'(a/)') prefix(:l_prfx)//'_o.masmcd'
          
          write(ifls,'(2a)')  'column   entry                           ',&
     &                        'unit'
          write(ifls,'(2a)')  '1        time                            ',&
     &                         time_unit
          do ic = 1,nc-1
            if (ic.lt.9) then
              write(ifls,'(i1,8x,a30,2x,a)') ic+1,namec(ic),'moles'
            else
              write(ifls,'(i2,7x,a30,2x,a)') ic+1,namec(ic),'moles'
            end if
          end do
        end if        

!c  total system mass for selected species

        if (nmb.gt.0) then

          imcd = imcd + 1
          
          !Missing results in this file. DSU, 2014-10-09
          if (rank == 0 .and. b_enable_output) then
            if (b_output_binary) then
#ifndef MPI
              if (imcd_mpi(imcd) < 10) then
                imcd_mpi(imcd) = lun_get()
              end if
#endif
              call binary_file_open(PETSC_COMM_SELF,           &
                          imcd_mpi(imcd), prefix(:l_prfx)//'_o.mssmcd',&
                          .true.)
            else  
              open(imcd,file=prefix(:l_prfx)//'_o.mssmcd',             &
                        status='unknown',form='formatted')
            end if
          
!c  version information
            if (b_writeversion_tecplot .and. .not. b_output_binary) then
              call writeversion2file(imcd, "#")
            end if            
            
            if (b_output_binary) then
              nvarsimcd = nmb+1  
              tec_variables(1) = "time"
              do imb = 1,nmb
                tec_variables(imb+1) = trim(namemb(imb))
              end do
              strbuffer = "system mass - selected species"
              
              offset_imcd(imcd) = 0  
              call tecplot_binary_write_header(PETSC_COMM_SELF,        &
                           imcd_mpi(imcd), "#!TDV102",'dataset '//     &
                           prefix(:l_prfx),offset_imcd(imcd),.true.,   &
                           .true.)  
              
              call tecplot_binary_write_variable(PETSC_COMM_SELF,      &
                           imcd_mpi(imcd), nvarsimcd,                  &
                           tec_variables(1:nvarsimcd),                 &
                           offset_imcd(imcd),.true.,.true.)               
              
              call tecplot_binary_write_zoneinfo(PETSC_COMM_SELF,      &
                           imcd_mpi(imcd),trim(strbuffer),             &
                           offset_imcd(imcd), 1, 1, 1, .true.,.true.,  &
                           b_output_multizone)
              offset_imcd_ijk(imcd) = offset_imcd(imcd) - 5*4
              
              call tecplot_binary_write_section(PETSC_COMM_SELF,       &
                           imcd_mpi(imcd),nvarsimcd,0,                 &
                           offset_imcd(imcd),.true.,.true.,            &
                           b_output_multizone) 
            else
              write(imcd,'(3a)') 'title = "dataset ',prefix(:l_prfx),'"' 
              
              strbuffer = 'variables = "time"'
              do imb = 1,nmb
                strbuffer = trim(strbuffer)//', "'//trim(namemb(imb))//'"'
              end do
              
              write(imcd,'(a)') trim(strbuffer)            
              write(imcd,'(2a)')                                       &
                    'zone t = "system mass - selected species", f=point'
            end if
            
            write(ifls,'(/a/72a/)') 'system mass - selected species',  &
     &                              ('-',i=1,72)
            write(ifls,'(a/)') prefix(:l_prfx)//'_o.mssmcd'
            
            write(ifls,'(2a)')'column   entry                           ',    &
     &                        'unit'
            write(ifls,'(2a)')'1        time                            ',    &
     &                         time_unit
            do imb = 1,nmb
              if (imb.lt.9) then
                write(ifls,'(i1,8x,a30,2x,a)') imb+1,namemb(imb),'moles'
              else
                write(ifls,'(i2,7x,a30,2x,a)') imb+1,namemb(imb),'moles'
              end if
            end do          
          end if

        end if

!c  contributions to mass balance - aqueous phase

        if (rank == 0 .and. b_enable_output) then
          write(ifls,'(/a/72a/)')'mass balance - aqueous phase',       &
                                 ('-',i=1,72)
          write(ifls,'(2a)')'file name                   ','component'
        end if
        
        if (rank == 0 .and. b_enable_output .and. b_output_binary) then
          nvarsimcd = 15
          tec_variables(1:nvarsimcd) = [character(len=72) ::           &
                  "time ["//"time_unit(:l_time_unit)]",                &
                  "influx diffusion [mol/d]",                          &
                  "influx migration [mol/d]",                          & 
                  "influx [mol/d]",                                    &
                  "outflux diffusion [mol/d]",                         &
                  "outflux migration [mol/d]",                         &
                  "outflux [mol/d]",                                   &
                  "change in storage [mol/d]",                         &
                  "total influx diffusion [mol/elapsed time]",         &
                  "total influx migration [mol/elapsed time]",         &
                  "total influx [mol/elapsed time]",                   &
                  "total outflux diffusion [mol/elapsed time]",        &
                  "total outflux migration [mol/elapsed time]",        &
                  "total outflux [mol/elapsed time]",                  &
                  "total change in storage [mol/elapsed time]"] 
        end if

        do ic = 1,n

          imcd = imcd+1
          
          if (rank == 0 .and. b_enable_output) then

            !rewind(icnv)           !Deprecated, use internal convert instead. DSU
            if(ic.lt.10) then
              !write(icnv,'(i1)') ic
              !rewind(icnv)
              !read(icnv,'(a2)') suffix
              write(suffix,'(i1)') ic
              l_sufx = 1
            elseif (ic.ge.10) then
              !write(icnv,'(i2)') ic
              !rewind(icnv)
              !read(icnv,'(a2)') suffix
              write(suffix,'(i2)') ic
              l_sufx = 2
            end if

!c  open file
            if (b_output_binary) then
#ifndef MPI
              if (imcd_mpi(imcd) < 10) then
                imcd_mpi(imcd) = lun_get()
              end if
#endif
              call binary_file_open(PETSC_COMM_SELF,           &
                           imcd_mpi(imcd), prefix(:l_prfx)//'_'//      &
                           suffix(:l_sufx)//'.mcd',.true.)
            else 
              open(imcd,file=prefix(:l_prfx)//'_'//                    &
                             suffix(:l_sufx)//'.mcd',status='unknown', &
                        form='formatted')
            end if
          
!c  version information
            if (b_writeversion_tecplot .and. .not. b_output_binary) then
                call writeversion2file(imcd, "#")
            end if
            
            if (b_output_binary) then
              write(strbuffer,'(3a)') "mass balance for component ",   &
                    namec(ic)(:l_namec(ic))," - reactive transport"              
              
              offset_imcd(imcd) = 0  
              call tecplot_binary_write_header(PETSC_COMM_SELF,        &
                           imcd_mpi(imcd), "#!TDV102",'dataset '//     &
                           prefix(:l_prfx),offset_imcd(imcd),.true.,   &
                           .true.)  
              
              call tecplot_binary_write_variable(PETSC_COMM_SELF,      &
                           imcd_mpi(imcd), nvarsimcd,                  &
                           tec_variables(1:nvarsimcd),                 &
                           offset_imcd(imcd),.true.,.true.)               
              
              call tecplot_binary_write_zoneinfo(PETSC_COMM_SELF,      &
                           imcd_mpi(imcd),trim(strbuffer),             &
                           offset_imcd(imcd), 1, 1, 1, .true.,.true.,  &
                           b_output_multizone)
              offset_imcd_ijk(imcd) = offset_imcd(imcd) - 5*4
              
              call tecplot_binary_write_section(PETSC_COMM_SELF,       &
                           imcd_mpi(imcd),nvarsimcd,0,                 &
                           offset_imcd(imcd),.true.,.true.,            &
                           b_output_multizone) 
            else
              write(imcd,'(3a)') 'title = "dataset ',prefix(:l_prfx),'"'
              
              write(imcd,'(31a)') 'variables = "time [',               &
                     time_unit(:l_time_unit),']", ',                   &
                    '"influx diffusion [mol/d]", ',                    &
                    '"influx migration [mol/d]", ',                    & 
                    '"influx [mol/d]", ',                              &
                    '"outflux diffusion [mol/d]", ',                   &
                    '"outflux migration [mol/d]", ',                   &
                    '"outflux [mol/d]", ',                             &
                    '"change in storage [mol/d]", ',                   &
                    '"total influx diffusion [mol/elapsed time]", ',   &
                    '"total influx migration [mol/elapsed time]", ',   &
                    '"total influx [mol/elapsed time]", ',             &
                    '"total outflux diffusion [mol/elapsed time]", ',  &
                    '"total outflux migration [mol/elapsed time]", ',  &
                    '"total outflux [mol/elapsed time]", ',            &
                    '"total change in storage [mol/elapsed time]"'
              
              write(imcd,'(4a)')                                       &
                    'zone t = "mass balance for component ',           &
                    namec(ic)(:l_namec(ic)),' - ',                     &
                    ' reactive transport", f=point'
            end if

!c  write data to file information file

            if (l_prfx+l_sufx.eq.2) then
              write(ifls,'(a,21x,a)') prefix(:l_prfx)//'_'//           &
                                     suffix(:l_sufx)//'.mcd',          &
                                     namec(ic)                         
            elseif (l_prfx+l_sufx.eq.3) then                           
              write(ifls,'(a,20x,a)') prefix(:l_prfx)//'_'//           &
                                     suffix(:l_sufx)//'.mcd',          &
                                     namec(ic)                         
            elseif (l_prfx+l_sufx.eq.4) then                           
              write(ifls,'(a,19x,a)') prefix(:l_prfx)//'_'//           &
                                     suffix(:l_sufx)//'.mcd',          &
                                     namec(ic)                         
            elseif (l_prfx+l_sufx.eq.5) then                           
              write(ifls,'(a,18x,a)') prefix(:l_prfx)//'_'//           &
                                     suffix(:l_sufx)//'.mcd',          &
                                     namec(ic)                         
            elseif (l_prfx+l_sufx.eq.6) then                           
              write(ifls,'(a,17x,a)') prefix(:l_prfx)//'_'//           &
                                     suffix(:l_sufx)//'.mcd',          &
                                     namec(ic)                         
            elseif (l_prfx+l_sufx.eq.7) then                           
              write(ifls,'(a,16x,a)') prefix(:l_prfx)//'_'//           &
                                     suffix(:l_sufx)//'.mcd',          &
                                     namec(ic)                         
            elseif (l_prfx+l_sufx.eq.8) then                           
              write(ifls,'(a,15x,a)') prefix(:l_prfx)//'_'//           &
                                     suffix(:l_sufx)//'.mcd',          &
                                     namec(ic)                         
            elseif (l_prfx+l_sufx.eq.9) then                           
              write(ifls,'(a,14x,a)') prefix(:l_prfx)//'_'//           &
                                     suffix(:l_sufx)//'.mcd',          &
                                     namec(ic)                         
            elseif (l_prfx+l_sufx.eq.10) then                          
              write(ifls,'(a,13x,a)') prefix(:l_prfx)//'_'//           &
                                     suffix(:l_sufx)//'.mcd',          &
                                     namec(ic)                         
            elseif (l_prfx+l_sufx.eq.11) then                          
              write(ifls,'(a,12x,a)') prefix(:l_prfx)//'_'//           &
                                     suffix(:l_sufx)//'.mcd',          &
                                     namec(ic)                         
            elseif (l_prfx+l_sufx.eq.12) then                          
              write(ifls,'(a,11x,a)') prefix(:l_prfx)//'_'//           &
                                     suffix(:l_sufx)//'.mcd',          &
                                     namec(ic)                         
            elseif (l_prfx+l_sufx.eq.13) then                          
              write(ifls,'(a,10x,a)') prefix(:l_prfx)//'_'//           &
                                     suffix(:l_sufx)//'.mcd',          &
                                     namec(ic)                         
            elseif (l_prfx+l_sufx.eq.14) then                          
              write(ifls,'(a,9x,a)') prefix(:l_prfx)//'_'//            &
                                     suffix(:l_sufx)//'.mcd',          &
                                     namec(ic)                         
            elseif (l_prfx+l_sufx.eq.15) then                          
              write(ifls,'(a,8x,a)') prefix(:l_prfx)//'_'//            &
                                     suffix(:l_sufx)//'.mcd',          &
                                     namec(ic)               
            end if
          end if
        end do


!c  pointer to last mass balance file for reactive transport

        imcd_last = imcd

      end if

      return
      end
