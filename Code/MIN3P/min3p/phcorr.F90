!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/phcorr.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c ----------------------------------------------------------------------
!c subroutine phcorr
!c -----------------
!c calculate fixed free species activities and concentrations
!c which require specified pH
!c
!c written by:      Uli Mayer - October 18, 95
!c
!c last modified:   Uli Mayer - March 15, 00
!c
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c                                                                    I O
!c passed:   real*8:
!c           -------
!c           cnew(nc)           = concentrations of free species      * +    
!c                                - new time level [moles/l water]  
!c           cold(nc)           = concentrations of free species      * +    
!c                                - old time level [moles/l water]  
!c           tempkel            = temperature [K]                     + -
!c
!c           integer*4:
!c           ----------
!c           ilog               = unit number - logbook               + -
!c
!c common:
!c chem.f:   real*8:
!c           -------
!c           actv(nc)           = activity of free species            - +
!c                                - new time level
!c           totco(n,nthreads)  = total aqueous component             + -
!c                                concentrations
!c                                - old time level [moles/l water]
!c           ph_fixed           = value for fixed pH                  + -
!c
!c           integer*4
!c           ---------
!c           nc                 = number of components including h2o  + -
!c
!c           character:
!c           ----------
!c           ctype(nc-1)        = 'charge' = correct total aqueous    + +
!c                                           component concentration
!c                                           for specified component 
!c                                           to satisfy charge balance
!c                                'fixed'  = compute total aqueous
!c                                           component concentrations
!c                                           based on fixed activities
!c                                           of components as species
!c                                           in solution
!c                                'free'   = compute concentrations
!c                                           of components as species
!c                                           in solution based on 
!c                                           specified total aqueous
!c                                           component concentrations
!c                                'ph'    =  pH specified for 'h+1'
!c
!c           logical:
!c           --------
!c           specified_ph       = ph value is specified               + -
!c
!c local:    real*8:
!c           -------
!c           r2                 = constant
!c           r4                 = constant
!c           r10                = constant
!c
!c           integer*4:
!c           ----------
!c           ic                 = counter (components)               
!c
!c external: -        
!c ----------------------------------------------------------------------
  
      subroutine phcorr(cnew,cold,tempkel,ilog)
 
      use parm
      use chem
      use gen, only : idbg, rank, b_enable_output
#ifdef OPENMP
      use omp_lib 
#endif
#ifdef PETSC
      use petsc_mpi_common, only : petsc_mpi_finalize
#endif 
      implicit none
      
      real*8 :: cnew, cold, tempkel
      integer :: ilog
      
      dimension cnew(*),cold(*)

      real*8, parameter :: r2 = 2.0d0, r4 = 4.0d0, r10 = 10.0d0
      
      integer :: tid, ic
      
#ifdef OPENMP
      tid = omp_get_thread_num() + 1
#else
      tid = 1
#endif
 
!c  calculate fixed free species activities and concentrations
!c  which require specified pH

      do ic = 1,nc-1
        if (ctype(ic).eq.'eh'.and.namec(ic).eq.'o2(aq)') then
          if (specified_ph) then
            totco(ic,tid) = totco(ic,tid)/ehfac/tempkel
            actv(ic) = r10**(-86.0180) * r10**(r4*totco(ic,tid))      &
     &               * r10**(r4*ph_fixed)                              
            cnew(ic) = actv(ic)                                        
            cold(ic) = actv(ic)                                        
            ctype(ic) = 'fixed'                                        
          else
            if (rank == 0) then  
              write(ilog,*) 'error in input file'                        
              write(ilog,*) 'specified Eh requires specified pH'
              close(ilog)
            end if
#ifdef PETSC
            call petsc_mpi_finalize
#endif
            stop                                                       
          end if                                                       
        elseif (ctype(ic).eq.'pe'.and.namec(ic).eq.'o2(aq)') then      
          if (specified_ph) then                                       
            actv(ic) = r10**(-86.0180) * r10**(r4*totco(ic,tid))      &
     &               * r10**(r4*ph_fixed)                              
            cnew(ic) = actv(ic)                                        
            cold(ic) = actv(ic)                                        
            ctype(ic) = 'fixed'                                        
          else
            if (rank == 0) then  
              write(ilog,*) 'error in input file'                        
              write(ilog,*) 'specified pe requires specified pH'  
              close(ilog)
            end if
#ifdef PETSC
            call petsc_mpi_finalize
#endif
            stop                                                       
          end if                                                       
        elseif (ctype(ic).eq.'pco2'.and.namec(ic).eq.'co3-2') then     
          if (specified_ph) then                                       
            actv(ic) = r10**(-18.16d0) * totco(ic,tid)                &
     &               * r10**(r2*ph_fixed)        
            cnew(ic) = actv(ic)
            cold(ic) = actv(ic)
            ctype(ic) = 'fixed'
          else
            if (rank == 0) then  
              write(ilog,*) 'error in input file'
              write(ilog,*) 'specified pco2 requires specified pH'
              close(ilog)
            end if
#ifdef PETSC
            call petsc_mpi_finalize
#endif
            stop
          end if
        elseif (ctype(ic).eq.'pco2'.and.namec(ic).eq.'hco3-') then     
          if (specified_ph) then                                       
            actv(ic) = r10**(-7.830d0)*totco(ic,tid)*r10**(r2*ph_fixed)        
            cnew(ic) = actv(ic)
            cold(ic) = actv(ic)
            ctype(ic) = 'fixed'
          else
            if (rank == 0) then  
              write(ilog,*) 'error in input file'
              write(ilog,*) 'specified pco2 requires specified pH'
              close(ilog)
            end if
#ifdef PETSC
            call petsc_mpi_finalize
#endif
            stop
          end if
        end if
      end do

      return
      end
