!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 495 $
!> $Author: fgerard $
!> $Date: 2017-07-19 00:12:32 +0200 (mer., 19 juil. 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/mbalvs.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c ----------------------------------------------------------------------
!c subroutine mbalvs
!c -----------------
!c
!c mass balance (variably saturated flow)
!c sign convention: inflow  -> positive
!c                  outflow -> negative
!c
!c written by:      Uli Mayer - July 12, 96
!c
!c last modified:   Tom Henderson - March 24, 2003
!c                  added point source
!c                  Sergi Molins - May 2, 2006
!c                  added skip and nskip variables
!c
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c                                                                    I O
!c passed:   -
!c
!c common:   
!c gen.f:    real*8:
!c           -------
!c           bcondvs(nbvs)      = boundary condition                  + -
!c                                (pressure head or flux) or
!c                                identification of seepage face
!c                                boundary type
!c           cinfvs(njavs)      = influence coefficients              + -
!c           cvol(nn)           = nodal volumes                       + -
!c           hhead(nn)          = hydraulic head                      + -
!c           relperm(nn)        = relative permeability               + -
!c           sanew(nn)          = aqueous phase saturation            + -
!c                                - new time level
!c           saold(nn)          = aqueous phase saturation            + -
!c                                - old time level
!c           time_io            = current solution time (I/O units)   + -
!c           totvsmass          = total system mass                   + +
!c           uvsold(nn)         = solution vector (old time level)    + -
!c           uvsnew(nn)         = solution vector (new time level)    + -
!c           vsflux(ncon-1)     = interfacial fluxes                  * *
!c
!c           integer*4:
!c           ----------
!c           iabvs(nbvs)        = pointer to boundary control volumes + -
!c                                for variably saturated flow
!c           iavs(nn+1)         = row pointer array for avs           + -
!c           imvs               = unit number, mass balance -         + *
!c                                             variably saturated
!c                                             flow
!c           javs(njavs)        = connectivity list                   + -
!c           mpropvs(nn)        = pointer array for allocation of     + -
!c                                material properties
!c           mtime              = current time step                   + -
!c           nbvs               = number of specified boundary        + -
!c                                control volumes
!c                                (variably saturated flow)
!c           nn                 = total number of control volumes     + -
!c           skip               = number of skipped timesteps in logf + -
!c           nskip              = counter of skipped timesteps        + -
!c
!c           logical:
!c           --------
!c           root_uptake        = .true.  -> calculate root water     + -
!c                                           uptake
!c           steady_flow        = .true.  -> steady state flow        + -
!c           transient_flow     = .true.  -> .not.steady_flow,        + -
!c                                        -> transient flow
!c           update_permeability= .true.  -> update permeability as   + -
!c                                           a function of porosity
!c           upstream           = .true.  -> upstream weighting       + -
!c
!c           character:
!c           ----------
!c           btypevs(nbvs)      = boundary type array                 + -
!c                                (variably saturated flow)
!c                                'first'   = Dirichlet
!c                                'second'  = Neumann
!c                                'seepage' = seepage face
!c           iups(ncon-1)       = upstream pointer                    * *
!c
!c
!c local:    real*8:
!c           -------
!c           qroot              = root water uptake for current 
!c                                control volume
!c           qroot_tot_act      = total actual root water uptake
!c	     qroot_evap_tot      = CBF ADDED
!c	     qroot_transp_tot      = CBF ADDED
!c           r0                 = constant
!c           r100               = constant
!c           totinflux          = total flux into domain
!c           totoutflux         = total flux out of domain
!c           totvsflux          = total flux into control volume
!c           dens_h2o           = water density (kg/m^3)
!c
!c           integer*4:
!c           ----------
!c           i1                 = counter (row entries)
!c           ibvs               = counter (boundary control 
!c                                         volumes)
!c           icon               = pointer (connections - local)
!c           iend               = pointer (end of row)
!c           istart             = pointer (start of row)
!c           jvol               = row-column pointer
!c           ivol               = counter (control volumes)
!c
!c external: fluxvs   = flux function for variably saturated 
!c                      flow 
!c           fluxfs   = flux function for fully saturated flow 
!c           msysvs   = compute total system mass
!c                      (variably saturated flow)
!c           rootwat  = function for computing root water uptake 
!c           storvs   = storage function for variably saturated 
!c                      flow
!c ----------------------------------------------------------------------

      subroutine mbalvs

      use parm
      use gen
      use phys
     
      use writeversion
      
      use module_binary_mpiio, only : binary_write_data

      implicit none !FG july 2017 - back to implicit none!
!	implicit real*8 (a-h,o-z)	!CBF
      
#ifdef PETSC_V3_6_X
#include <petsc/finclude/petscsys.h>
#elif PETSC
#include <finclude/petscsys.h>
#endif

#ifdef PETSC
      real*8 :: totvsstor_gbl, totinflux_gbl, totoutflux_gbl,          &
            &    qroot_tot_act_gbl
      
!      real*8 :: evapo, qroot_tot_act, qroot_evap_tot, qroot_transp_tot		!CBF

      PetscErrorCode :: ierrcode
#endif

      integer :: i1, ivol, ibvs, istart, iend, icon, jvol
      real*8 :: totvsstor, qroot_tot_act, vsstor, storvs, totinflux,   &
            &    totoutflux, totvsflux, fluxvs, fluxfs, rootwat, &
! CBF qroot
            &    absbalance, relbalance, culrelbalvs,rdummy1, rdummy2
      
      real*8 :: evapo, qroot_evap_tot, qroot_transp_tot		!CBF!FG july 2017 discarded from PETSC zone above
      
      external fluxvs, fluxfs, msysvs, rootwat, storvs, evapo ! CBF ADDED evapo

      real*8, parameter :: r0 = 0.0d0, r100 = 100.0d0, rsmall = 1.0d-100
      
      integer :: nvarsimvs
      

      dens_h2o = 1.0d+3
      ! CBF totvsstor = r0
      qroot_tot_act = r0




!c  compute total system mass
!c  Parallelized, OpenMP, DSU

      call msysvs

!c  for transient conditions -> compute changes in storage 

	totvsstor = r0 !CBF - FG april 2013: should be initialized here,
        !               and not if transient flow condition is verified
        !               as totvsstor is used in output file (see below) in
        !               any case (this fixes small differences of mass balance output, compared to compaq)



      if (transient_flow) then
#ifdef OPENMP
    !$omp parallel                                                    &                                                              
    !$omp if (nngl > numofloops_thred_mbalvs_1)                       &
    !$omp num_threads(numofthreads_global)                            &
    !$omp default(shared)                                             &
    !$omp private(ivol, vsstor)                                       &
    !$omp reduction(+:totvsstor)
    !$omp do schedule(static)
#endif 
        do ivol = 1,nngl
#ifdef PETSC
          if(node_idx_lg2l(ivol) < 0) then
              cycle
          end if
#endif
            
          vsstor = cvol(ivol)                                         &
                 * storvs(uvsnew(ivol),uvsold(ivol),                  &
                          sanew(ivol),saold(ivol),                    &
                          mpropvs(ivol),stor(ivol))/delt
          totvsstor = totvsstor + vsstor 
        end do
#ifdef OPENMP
    !$omp end do
    !$omp end parallel
#endif    

#ifdef PETSC
      call MPI_Allreduce(totvsstor, totvsstor_gbl,1,MPI_REAL8,MPI_SUM, &
                         Petsc_Comm_World,ierrcode)
      CHKERRQ(ierrcode)
      totvsstor = totvsstor_gbl    
#endif

      end if

!c  compute fluxes across boundary

      totinflux = r0               !initialize total in- and outflux
      totoutflux = r0
#ifdef OPENMP
    !$omp parallel                                                    &                                                              
    !$omp if (nbvs > numofloops_thred_mbalvs_2)                       &
    !$omp num_threads(numofthreads_global)                            &
    !$omp default(shared)                                             &
    !$omp private(i1, icon, iend, istart, ivol, iups, jvol,           &
    !$omp totvsflux, vsflux)                                          &
    !$omp reduction(+:totinflux, totoutflux)
    !$omp do schedule(dynamic)
#endif  
      do ibvs = 1,nbvs             !loop over boundary control volumes

        ivol = iabvs(ibvs)         !pointer to control volume
#ifdef PETSC
        if(node_idx_lg2l(ivol) < 0) then
            cycle
        end if
#endif
        totvsflux = r0             !initialize total flux

!c  fluxes at first type control volumes or zero pressure seepage 
!c  control volumes

        if ((btypevs(ibvs).eq.'first').or.                            & !first type (Dirichlet) 
     &      ((btypevs(ibvs).eq.'seepage').and.                        & !first type (seepage)
     &       (bcondvs(ibvs).lt.r0))) then

          istart = iavs(ivol)       !pointer - start of row
          iend = iavs(ivol+1)-1     !pointer - end of row
          icon = 0                  !counter (connections)

          do i1=istart,iend         !loop over connected control volumes

            jvol = javs(i1)         !column pointer

            if (jvol.ne.ivol) then
              icon = icon+1         !counter (connections)

!c  consistent with upstream weighting

              if (upstream) then
                iups(icon) = 'i'                         !h_i >= h_j
                if (hhead(jvol).gt.hhead(ivol)) then     !h_j > h_i
                  iups(icon) = 'j'
                end if
              end if

!c  flux calculations

              if (variably_saturated) then
                vsflux(icon) = - fluxvs(upstream,hhead(ivol),         &
     &                                  hhead(jvol),relperm(ivol),    &
     &                                  relperm(jvol),iups(icon),     &
     &                                  cinfvs_a(i1))
              elseif (fully_saturated) then
                vsflux(icon) = - fluxfs(uvsnew(ivol),uvsnew(jvol),    &
     &                                  cinfvs_a(i1))
              end if
              
              totvsflux = totvsflux + vsflux(icon)

            end if                  !(ivol.eq.jvol)
          end do                    !loop over connected control volumes
   
!c  fluxes at second type control volumes

        elseif ((btypevs(ibvs).eq.'second').or.       &
     &      (btypevs(ibvs).eq.'point')) then

          totvsflux = bcondvs(ibvs)
 
        end if                      !boundary type

!c  sum up total inflow and outflow

        if (totvsflux.gt.r0) then   
          totinflux = totinflux + totvsflux
        else
          totoutflux = totoutflux - totvsflux
        end if

      end do                        !loop over boundary control volumes
#ifdef OPENMP
    !$omp end do
    !$omp end parallel
#endif  

#ifdef PETSC
      call MPI_Allreduce(totinflux,totinflux_gbl,1,MPI_REAL8,MPI_SUM,  &
                         Petsc_Comm_World,ierrcode)
      CHKERRQ(ierrcode)
      totinflux = totinflux_gbl    
      
      call MPI_Allreduce(totoutflux,totoutflux_gbl,1,MPI_REAL8,MPI_SUM,&
                         Petsc_Comm_World,ierrcode)
      CHKERRQ(ierrcode)
      totoutflux = totoutflux_gbl 
#endif

!c  compute contributions from root water uptake
     if (root_uptake) then

	 qroot_tot_act = r0	!CBF
	 qroot_evap_tot=r0	!CBF
	 qroot_transp_tot=r0	!CBF
       
#ifdef OPENMP
    !$omp parallel                                                    &                                                              
    !$omp if (nngl > numofloops_thred_mbalvs_3)                       &
    !$omp num_threads(numofthreads_global)                            &
    !$omp default(shared)                                             &
    !$omp private(ivol, qroot_evap_tot,qroot_transp_tot)              &
    !$omp reduction(+:qroot_tot_act)
    !$omp do schedule(static)
#endif
!    !$omp private(ivol, qroot)              & !FG 07-2017 private for other variable names

       do ivol = 1,nngl
#ifdef PETSC
          	if(node_idx_lg2l(ivol) < 0) then
              		cycle
          	end if
#endif
          	!qroot = cvol(ivol)*rootwat(sanew(ivol), mpropvs(ivol))	!CBF

		if (BINev(ivol)) then	!CBF
			
			qroot_evap_tot=qroot_evap_tot+cvol(ivol)*evapo(sanew,ivol)!CBF

		endif!CBF
		
		if (BINT(ivol)) then!CBF
			qroot_transp_tot=qroot_transp_tot+cvol(ivol)	&!CBF
    		 &                         *rootwat(sanew,ivol)!CBF
        	endif!CBF

          	qroot_tot_act = qroot_evap_tot+qroot_transp_tot ! CBF ADDED +qroot_transp_tot !+ qroot CBF : delete qroot to prevent from convergeance nulmerical bug

       end do
       
#ifdef OPENMP
    !$omp end do
    !$omp end parallel
#endif 

	else	!FG if no root uptake, check for phys evap (according to simple formalism)

	    qroot_tot_act = r0!CBF
	    qroot_evap_tot=r0!CBF
        
!FG 07-2017 added open MP for this new loop
#ifdef OPENMP
    !$omp parallel                                                    &                                                              
    !$omp if (nngl > numofloops_thred_mbalvs_3)                       &
    !$omp num_threads(numofthreads_global)                            &
    !$omp default(shared)                                             &
    !$omp private(ivol, qroot_evap_tot)                               &
    !$omp reduction(+:qroot_tot_act)
    !$omp do schedule(static)
#endif
!    !$omp private(ivol, qroot)& !FG private for other variable names

	do ivol = 1,nngl ! detect in which control volumes (zone-based) evaporation can occur!CBF
                     !FG nngl used instead of nn (old version)
	  
		if (BINev(ivol)) then!CBF
			
	       		qroot_evap_tot=qroot_evap_tot+cvol(ivol)*evapo(sanew,ivol)	!CBF 

		endif!CBF
		
			qroot_tot_act=qroot_evap_tot!CBF
	    
	 end do!CBF

!FG end added open MP
#ifdef OPENMP
    !$omp end do
    !$omp end parallel
#endif 

#ifdef PETSC
      call MPI_Allreduce(qroot_tot_act,qroot_tot_act_gbl,1,MPI_REAL8,  &
                         MPI_SUM,Petsc_Comm_World,ierrcode)
      CHKERRQ(ierrcode)

      qroot_tot_act = qroot_tot_act_gbl    
#endif

      end if ! root_uptake


!c  write total contributions to file   

      imvs = imvs+1
    
      if (rank == 0 .and. b_enable_output .and.                        &
          .not.((skip_time.gt.0).and.(nskip_time.lt.skip_time))) then
        if (b_output_binary) then
          nvarsimvs = 5
          realbuffer_gb(1:nvarsimvs) = (/time_io,totinflux,totoutflux, &
                                   totvsstor,qroot_tot_act/)
          call binary_write_data(imvs_mpi(imvs), 1,            &
                       (/mtime/),offset_imvs_ijk(imvs),.true.)       
          call binary_write_data(imvs_mpi(imvs), nvarsimvs,    &
                       realbuffer_gb,offset_imvs(imvs),.true.) 

          offset_imvs(imvs) = offset_imvs(imvs) + nvarsimvs*nfloatbit

        else
          write(imvs,'(5(1pe12.4))') time_io,totinflux,totoutflux,     &
                                   totvsstor,qroot_tot_act 
        end if
      end if

!c  compute absolute and relative mass balance error 

      rdummy1 = (totinflux-totoutflux-totvsstor-qroot_tot_act)
      rdummy2 = abs(totinflux) + abs(totoutflux)
      if (rdummy2 < rsmall) then
        if(rdummy1 < rsmall) then
          relbalance_vs = 0.0d0
        else
          relbalance_vs = 1.0d100
        end if
      else
        relbalance_vs = rdummy1/rdummy2
      end if

      absbalance = rdummy1 * dens_h2o * delt
      relbalance = absbalance/totvsmass * r100

      absbalance_vs = absbalance
 
!c  compute accumulative absolute and relative mass balance error 

      culabsbalvs = culabsbalvs + absbalance
      culrelbalvs = culabsbalvs/totvsmass*r100

      imvs = imvs+1
    
      if(rank == 0 .and. b_enable_output) then
        if (b_output_binary) then
          nvarsimvs = 5
          realbuffer_gb(1:nvarsimvs) = (/time_io,absbalance,relbalance,&
                                   culabsbalvs,culrelbalvs/)
          call binary_write_data(imvs_mpi(imvs), 1,            &
                       (/mtime/),offset_imvs_ijk(imvs),.true.)       
          call binary_write_data(imvs_mpi(imvs), nvarsimvs,    &
                       realbuffer_gb,offset_imvs(imvs),.true.) 

          offset_imvs(imvs) = offset_imvs(imvs) + nvarsimvs*nfloatbit

        else
          write(imvs,'(5(1pe12.4))') time_io,absbalance,relbalance,    &
                                   culabsbalvs,culrelbalvs 
        end if
      end if

      return
      end 
