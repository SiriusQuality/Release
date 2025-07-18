!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/readcomp.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c -----------------------------------------------------------------------
!c subroutine readcomp
!c -------------------
!c
!c read database for components and assign to permanent storage 
!c
!c written by:      Uli Mayer - April 8, 96
!c
!c last modified:   Uli Mayer - November 12, 96
!c
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c                                                                    I O
!c passed:   integer*4:
!c           ----------
!c           idbg               = unit number, debugging file         + -
!c           ilog               = unit number, log book               + -
!c           icdbs              = unit number, database for           + -
!c                                             components
!c
!c common:
!c chem.f:   real*8:
!c           -------
!c           alkfacc(nc)        = alkalinity factors for components   * +
!c           chargec(nc)        = charge of free species              * +
!c           dhac(nc)           = debye-huckel a for free species     * +
!c           dhbc(nc)           = debye-huckel b for free species     * +
!c           gfwc(nc)           = gram formula weight of free         * +
!c                                species
!c
!c           integer*4:
!c           ----------
!c           nc                 = number of components including h2o  + -
!c
!c           logical:
!c           --------
!c           compute_alkalinity = .true  -> calculate alkalinity      - +
!c
!c           character:
!c           ----------
!c           namec(nc)          = component names                     + -
!c
!c local:    real*8:
!c           -------
!c           alkfac             = alkalinity factor (temporary)
!c           charge             = charge of current component 
!c                                (temporary)
!c           dha                = debye huckel a (temporary)
!c           dhb                = debye huckel b (temporary)
!c           gfw                = gram formula weight (temporary)
!c
!c           integer*4:
!c           ----------
!c           ic                 = counter (components)
!c
!c           logical:
!c           --------
!c           done               = logical variable to stop search 
!c
!c           character:
!c           ----------
!c           name               = name of current component 
!c
!c external: -
!c ----------------------------------------------------------------------

      subroutine readcomp(icdbs,ilog,idbg)
 
      use parm
      use chem
      use multidiff
      use gen, only : rank, b_enable_output
#ifdef PETSC
      use petsc_mpi_common, only : petsc_mpi_finalize
#endif 
 
      implicit none
      
      integer :: icdbs,ilog,idbg
      
      integer :: i, ic, info_debug
      
      real*8 :: charge, dha, dhb, gfw, alkfac, sec_per_days

      character*256 :: strbuffer
      character*72 name
      logical done
      real*8 null
 
 
!c  loop over components specified for simulation
 
      do ic = 1,nc
 
!c  loop over components in database,
!c  search for match and assign data to permanent storage
!c  exit when done or when end of file is reached
 
        done = .false.
 
        do while (.not.done)
 
!c  read component name and component specific data 
 
        if (.not.multi_diff) then 
          if (compute_alkalinity) then
            read(icdbs,110,err=999) name,charge,dha,dhb,gfw,alkfac
          else
            read(icdbs,100,err=999) name,charge,dha,dhb,gfw
          end if !  compute_alkalinity
        end if !  .not.multi_diff 

! prc Reading diffusion coefficients from the "comp.dbs" database for each component

        if (multi_diff) then
            if (compute_alkalinity) then
              read(icdbs,130,err=999) name,charge,dha,dhb,gfw,alkfac,diffcoff1
            else      
              read(icdbs,120,err=999) name,charge,dha,dhb,gfw,null,diffcoff1
            end if !    compute_alkalinity
        end if !  multi_diff
 
 
!c  look for match, as long end of file is not reached or 
!c  match is found
 
!c  component is found --> assign permanent storage
 
          if (name.eq.namec(ic)) then
 
            done = .true.
 
            chargec(ic) = charge
            dhac(ic) = dha
            dhbc(ic) = dhb
            gfwc(ic) = gfw
            if (compute_alkalinity) then
              alkfacc(ic) = alkfac
            end if
! prc ----------------------------------------------------------------------------
! prc Initialization of the vector of diffusion coeff. of primary species 
! prc (components) and conversion of time units for computation in days
! prc ----------------------------------------------------------------------------
 
            sec_per_days = 8.64d4
            
            if (multi_diff) then
                mdiff_ic(ic)= diffcoff1 * sec_per_days                
            end if ! multi_diff
            
! prc ----------------------------------------------------------------------------
! prc ----------------------------------------------------------------------------           
 
!c  end of file is reached --> exit 
 
          elseif (name.eq.'end') then
 
            if (rank == 0) then  
              write(ilog,'(72a)') ('-',i=1,72)
              write(ilog,'(a,a72,a)')'component ',namec(ic),           & 
     &                            ' not in database'
              write(ilog,'(72a)') ('-',i=1,72)
            end if
#ifdef PETSC
            call petsc_mpi_finalize
#endif
            stop
 
          end if
        end do                 !end - loop over component database
 
!c  rewind component database to search for next component, doing this
!c  allows the specification of components in an arbitray order
 
        rewind(icdbs)
 
      end do                   !end - loop over specified components
 
 100  format(a12,f4.1,4x,f5.2,f5.2,8x,f11.5)
 110  format(a12,f4.1,4x,f5.2,f5.2,8x,f11.5,f7.2)

! prc Defining new formats for "comp.dbs",  last column for diffusion coefficients

 120  format(a12,f4.1,4x,f5.2,f5.2,8x,f11.5,f7.2,f10.4)
 130  format(a12,f4.1,4x,f5.2,f5.2,8x,f11.5,f7.2,f10.4)
 

!cdbg ---- activate this section for purposes of debugging -----
#ifdef DEBUG
      info_debug = 0

      if (info_debug.eq.1) then
        do ic=1,nc
          write(idbg,'(a,i3,2x,a)') 'ic    = ',ic,trim(namec(ic))
          write(idbg,'(a,e10.3)') 'chargec(ic) = ',chargec(ic)
          write(idbg,'(a,f10.3)') 'dhac(ic)    = ',dhac(ic)
          write(idbg,'(a,f10.3)') 'dhbc(ic)    = ',dhbc(ic)
          write(idbg,'(a,f10.3)') 'gfwc(ic)    = ',gfwc(ic)
          write(idbg,'(a,e10.3)') 'mdiff_ic(ic)    = ',mdiff_ic(ic)
          if (compute_alkalinity) then
            write(idbg,'(a,f10.3)') 'alkfacc(ic) = ',alkfacc(ic)
          end if
          write(idbg,*)
          write(idbg,*) '----------------------------------------------'
          write(idbg,*)
        end do
#ifdef PETSC
        call petsc_mpi_finalize
#endif
        stop
      end if
#endif
!cdbg
      return

999   continue
      backspace(icdbs)
      read(icdbs,'(a)') strbuffer
      if (rank == 0) then
        write(ilog,*) 'SIMULATION TERMINATED'
        write(ilog,'(2a)') 'error reading in component database: ',trim(strbuffer)
        write(*,*) 'SIMULATION TERMINATED'
        write(*,'(2a)') 'error reading in component database: ',trim(strbuffer)
        close(ilog)
      end if
#ifdef PETSC
      call petsc_mpi_finalize
#endif
      stop

      end
  
