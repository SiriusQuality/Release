!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/surfcomp.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c ----------------------------------------------------------------------
!c subroutine surfcomp
!c -------------------
!c
!c compute surface composition based on equilibrated solution
!c composition
!c
!c written by:      Uli Mayer - March 18, 2000
!c
!c last modified:   - 
!c
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c                                                                    I O
!c passed:   real*8:
!c           -------
!c           cnew(nc)           = concentrations of free species      + +
!c                                - new time level [moles/l water]
!c           sw                 = water saturation                    + -
!c           por                = porosity                            + -
!c
!c           integer*4:
!c           ----------
!c           ilog               = unit number - logbook               + -
!c
!c common:   
!c chem.f:   real*8:
!c           -------
!c           alc(nc-1,nc-1,nthreads) 
!c                              = Jacobian matrix                * +
!c           blc(nc-1,nthreads) = rhs-vector                          * +
!c           cec(nthreads)      = cation exchange capacity (meq/100g) + -
!c           csb(nsb)           = concentrations of sorbed species    * +
!c                                - new time level
!c           csb_ion(nsb_ion,nthreads) 
!c                              = concentrations of sorbed species    * +
!c                                - new time level (ion-exchange)
!c           csb_surf(nsb_surf,nthreads) 
!c                              = concentrations of sorbed species    * +
!c                                - new time level (surface-complex)
!c           eqsb(nsb)          = equilibrium constants for           + -
!c                                sorbed species
!c           eqsb_ion(nsb_ion,nthreads)  
!c                              = equilibrium constants for           + -
!c                                sorbed species (ion-exchange)
!c           eqsb_surf(nsb_surf,nthreads)
!c                              = equilibrium constants for           + -
!c                                sorbed species (surface-complex)
!c           gammac(nc)         = activity coefficients of components * *
!c                                as species in solution
!c           totcn(n,nthreads)  = total component concentrations      + +
!c                                - new time level [moles/l water]  
!c           xnusb(nsb*nc)      = stoichiometric coefficient matrix   + -
!c                                for formation of sorbed species
!c                                from components
!c           xnusb_ion(nsb_ion*nc)
!c                              = stoichiometric coefficient matrix + -
!c                                for formation of sorbed species
!c                                from components (ion-exchange)
!c           xnusb_surf(nsb_surf*nc)
!c                              = stoichiometric coefficient matrix + -
!c                                for formation of sorbed species
!c                                from components (surface-complex)
!c
!c           integer*4:
!c           ----------
!c           iaic(nsites)       = pointer array for compressed        + -
!c                                storage of surface site data
!c           iasb(nsb+1)        = row pointer array to                + -
!c                                stoichiometric coefficients of
!c                                components in sorbed species
!c           iasb_ion(nsb_ion+1)= row pointer array to                + -
!c                                stoichiometric coefficients of
!c                                components in sorbed species
!c                                (ion-exchange)
!c           iasb_surf(nsb_surf+1)= row pointer array to              + -
!c                                stoichiometric coefficients of
!c                                components in sorbed species
!c                                (surface-complex)
!c           iter_lc(nthreads)  = iteration counter                   * +
!c                                (local chemistry)
!c           ittot_lc(nthreads) = total number of iterations          + +
!c                                (local chemistry)
!c           jasb(nsb*nc)       = column pointer array to             + -
!c                                stoichiometric coefficients of
!c                                components in sorbed species
!c           jasb_ion(nsb_ion*nc)= column pointer array to            + -
!c                                stoichiometric coefficients of
!c                                components in sorbed species
!c                                (ion-exchange)
!c           jasb_surf(nsb_surf*nc)= column pointer array to          + -
!c                                stoichiometric coefficients of
!c                                components in sorbed species
!c                                (surface-complex)
!c           maxit_lc(nthreads) = max. number of iterations           + -
!c                                (local chemistry)
!c           nc                 = number of components including h2o  + -
!c           nopu               = number of primary unknowns          + -
!c           nsb                = number of sorbed species            + -
!c           nsb_ion            = number of sorbed species            + -
!c                                (ion-exchange)
!c           nsb_surf           = number of sorbed species            + -
!c                                (surface-complex)
!c           nsites             = number of surface sites             + -
!c
!c           character:
!c           ----------
!c           sorption_group     = 'ion-exchange'                      + -
!c                                'surface-complexation'
!c                                'undefined'
!c           sorption_type      = 'gaines-thomas'                     + -
!c                                'gapon'
!c                                'surface-complex'
!c                                'constant-capacitance'
!c
!c local:    integer*4:
!c           ----------
!c           ic                 = counter (components)
!c           isb                = counter (sorbed species)
!c           isites             = counter (surface sites)
!c
!c           logical:
!c           --------
!c           not_converged      = .true.  -> continue Newtion 
!c                                           iteration
!c
!c           character:
!c           ----------
!c           zone_name          = name of zone
!c
!c external: jacsurf   = construct jacobian matrix and rhs-vector
!c                       (composition of surface sites)
!c           simq      = solves system of n linear equations
!c                       by Gaussian elimination
!c           sorbspc   = compute concentration of sorbed species
!c           updtsurf  = update solution vector
!c                       (composition of surface sites)
!c ----------------------------------------------------------------------
 
      subroutine surfcomp(cnew,gammac,sw,por,ilog)
 
      use parm
      use chem
      use gen, only : rank, b_enable_output
#ifdef OPENMP
      use omp_lib 
#endif 
#ifdef PETSC
      use petsc_mpi_common, only : petsc_mpi_finalize
#endif
 
      implicit none
      
      real*8 :: cnew, gammac, sw, por
      
      integer :: ilog
      
      integer :: tid, ic, isb, isites
      
      real*8 :: dummy
      
      external simq, sorbspc
      
      logical not_converged

      character*72 zone_name 

      dimension cnew(*),gammac(*)
      
      !For the shared-memory parallel version, the variables defined in the module
      !are shared variables by different threads. So as to avoid race condition, 
      !these variable should be passed by dummy arguments. Danyang Su, 2013-05.
      interface
      
        !>interface of jacsurf
        subroutine jacsurf(cnew,gammac,sw,por) 
          use parm, only : type_r8  
          real (type_r8), dimension(*) :: cnew
          real (type_r8), dimension(*) :: gammac
          real (type_r8) :: sw
          real (type_r8) :: por
        end subroutine
      
        !>interface of updtsurf
        subroutine updtsurf(c,ulc,ilog,not_converged)
          use parm, only : type_i4, type_r8 
          real(type_r8), dimension(*) :: c
          real(type_r8), dimension(*) :: ulc
          integer(type_i4) :: ilog
          logical :: not_converged     
        end subroutine      
      
      end interface
      
#ifdef OPENMP
      tid = omp_get_thread_num() + 1
#else
      tid = 1
#endif
      
!c  initialize local parameters

      zone_name = 'computing composition on surface sites'
    !  write(ilog,'(/a/72a/)') zone_name,('-',i=1,72)

!c  explicit solution for ion-exchange reactions

      if (implicit_surface_ion .and. nsb_ion.gt.0) then
          
        do isb = 1,nsb_ion
            call sorbspc_m(csb_ion(isb,tid),dummy,cec(tid),           &
                 cec_fraction(idx_nsites_ion(isb)),                   &
                 eqsb_ion(:,tid),eqsb_surf(:,tid),                    &
                 gammac,cnew,xnusb_ion,xnusb_surf,                    &
                 iasb_ion,iasb_surf,jasb_ion,jasb_surf,               &
                 nsb_ion,nsb_surf,isb,0,                              &
                 sorption_type_ion,sorption_type_surf,                &
                 sorption_group,isactcexch)
        end do
        
      end if  

!c  implicit solution for surface-complexation reactions

      if (implicit_surface_surf .and. nsb_surf.gt.0) then
          

!c  start Newton iteration
 
        iter_lc(tid) = 0
        not_converged = .true.

        do while (not_converged) 

          if (iter_lc(tid).lt.maxit_lc(tid)) then

            iter_lc(tid) = iter_lc(tid)+1
            ittot_lc(tid) = ittot_lc(tid)+1

!c  construct Jacobian matrix and rhs-vector 

            call jacsurf(cnew,gammac,sw,por)

!c  solve for update
  
            call simq(alc(:,:,tid),blc(:,tid),nopu,nc-1)
 
!c  update solution vector
   
            call updtsurf(cnew,blc(:,tid),ilog,not_converged)

          else                                 

            if (rank == 0) then  
              write(ilog,*)'-------------------------------------------'
              write(ilog,*)'   terminated in routine surfcomp          '
              write(ilog,*)'   maximum number of iterations exceeded   '
              write(ilog,*)'   bye now ...                             '
              write(ilog,*)'-------------------------------------------'
            end if
#ifdef PETSC
            call petsc_mpi_finalize
#endif
            stop

          end if

        end do               !(not_converged)

!c  assign new total component concentrations for surface sites

        do isites = 1,nsites
          ic = iaic(isites)
          totcn(ic,tid) = cnew(ic)
        end do

      end if                 !(sorption_group)

      return
      end
