!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/infcrt_g.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c --------------------------------------------------------------------------
!c subroutine infcrt_g
!c -------------------
!c
!c compute influence coefficients for diffusive flux terms 
!c for rectangular, cartesian finite volume discretization
!c (reactive transport)
!c
!c written by:      Uli Mayer - September 21, 96
!c
!c last modified:   -
!c last modified:   Sergi Molins - May 2, 2006 
!c                  modified to assign values to variables 
!c                  used in Dusty Gas Model & Stefan Maxwell subroutines
!c                  Sergi Molins - June 12, 2006
!c                  tort = 0 when gas saturation = 0
!c                  Sergi Molins - Feb 18, 2007
!c                  added correction for tortuosity when oil present
!c
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c                                                                        I O
!c     input:
!c
!c        d(1-3, ibk) - dimension of cells in x,y,z direction
!c        diffu - phase molecular diffusion (m2/day)
!c        ibk - block i
!c        id - connected block j
!c        idbg - output for debugging information
!c        ilog - unit number, logbook
!c        half_cells -  =.true.  -> half cells on boundary
!c        nmax - max cells for dim purposes (ncomp.com)
!c        nphas - max number of phases (ncomp.com)
!c        njamxc - max dim ja array (ncomp.com)
!c        nx, ny, nz - number of cells in x,y,z dir
!c        pornew(ibk) - porosity
!c        sgnew(nmax) - gaseous phase saturation
!c        ia(), ja() - ysmp pointers
!c        isymm(ii) - symmetry pointer for cell ibk
!c           tortuosity_corr    = .true.  -> Millington-Quirk
!c                                           tortuosity correction
!c                                           for diffusion
!c                                           coefficients
!c        gas_tortuosity - model of gas tortuosity 
!c                              =  'same as aqueous'
!c                                 'millington'
!c                                 'no correction'
!c                                 'moldrup repacked'
!c
!c     output:
!c
!c        cinfrt_dg() - area / dx * diffusivity 
!c                      influence coefficients (diffusive flux terms)
!c        deltaij     - distance between i and j [m]
!c        tauij       - tortusosity correction for gas diffusion 
!c        gporij      - gas filled porosity at interface i-j
!c        gsatij      - gas saturation at interface i-j
!c
!c passed:   -
!c
!c common:   -
!c
!c local:
!c
!c external: diffcoff  = compute effective diffusion coefficient
!c --------------------------------------------------------------------------
      subroutine infcrt_g (nx, ny, nz, ia, ja, isymm,                 &
                         cinfrt_dg, d, diffu, pornew, sgnew,          &
                         idbg, ilog, njamxc, nmax, tortuosity_corr,   &
                         half_cells,assigned_tau,tau,type_tortuosity, &
                         marchies,cinfrad,radial_coord,tau_fac,       &
                         gsatij,gporij,deltaij,tauij,tau_man,         &
                         gas_tortuosity,sonew,oil_saturation)
#ifdef OPENMP
      use omp_lib 
      use gen, only : rank, b_enable_output,                          &
                      numofthreads_global,                            &
                      numofloops_thred_global,                        &
                      numofloops_thred_infcrt_g_1,                    &
                      numofloops_thred_infcrt_g_2
#else
      use gen, only : rank, b_enable_output
#endif
#ifdef PETSC
      use petsc_mpi_common, only : petsc_mpi_finalize
#endif

      implicit none
      
      integer :: nmax, njamxc

      external diffcoff_g

      integer nx, ny, nz,                                             &
             ia(nmax+1), ja(njamxc), isymm(njamxc), idbg, ilog

      logical half_cells, tortuosity_corr, assigned_tau,radial_coord, &
              oil_saturation

      real*8  diffu, pornew(nmax), sgnew(nmax),                       &
              d(3,nmax), cinfrt_dg(njamxc),tau(nmax),cinfrad(njamxc), &
              tau_fac(nmax), marchies(nmax), gsatij(njamxc),          &
              gporij(njamxc), deltaij(njamxc),                        &
              tauij(njamxc), tau_man(nmax), sonew(nmax),              &
              so_av, tauav, tort

      real*8 diffcoff_g,tauloc
      
#ifdef OPENMP      
      integer :: nvols
#endif
      
      character(len=*) :: type_tortuosity, gas_tortuosity

!c     local variables

      real*8 r0, r1, r2, rverysmall
      parameter (r0 = 0.0d0, r1 = 1.0d0, r2 = 2.0d0,                  &
                 rverysmall = 1.0d-30)

      real*8 aread(3,12),areax(3,12),dist(3,12),                      &
             gpor, porav, satav, tend(3), diffav, diff_eff, marchieav

      integer ibk, iz, iy, ix, ii, id, iisav,                         &
             idim, npair(3), iface(3,12), ndim,                       &
             fvpair(3,12,2), ipair, idim2, idim3 
      
#ifdef OPENMP      
      nvols = nx * ny * nz
#endif

!c  compute influence coefficients for diffusive flux terms
!c  in gas phase

!c  zero the influence coefficient for diffusive flux term
#ifdef OPENMP
    !$omp parallel                                                    &
    !$omp if (nvols > numofloops_thred_infcrt_g_1)                    &
    !$omp num_threads(numofthreads_global)                            &
    !$omp default(shared)                                             &
    !$omp private (ibk, ii)                  
    !$omp do schedule(static)
#endif
      do ibk = 1, nmax
        do ii = ia(ibk),ia(ibk+1)-1
          cinfrt_dg(ii) = r0
        end do
      end do
#ifdef OPENMP
    !$omp end do
    !$omp end parallel
#endif 

!c  loop over control volumes
#ifdef OPENMP
    !$omp parallel                                                    &
    !$omp if (nvols > numofloops_thred_infcrt_g_2)                    &
    !$omp num_threads(numofthreads_global)                            &
    !$omp default(shared)                                             &
    !$omp private (ibk, id, idim, idim2, idim3, iface, ii, iisav,     &
    !$omp ipair, ix, iy, iz, ndim, npair, areax, aread, diff_eff,     &
    !$omp diffav, dist, fvpair, marchieav, porav, satav, tauloc,      &
    !$omp tend, gpor, tauav, so_av, tort)                             
    !$omp do schedule(static)
#endif
      do iz = 1, nz            !increments in z-direction
         do iy = 1, ny         !increments in y-direction
            do ix = 1, nx      !increments in x-direction

!c  find node pairs for elemental velocities as well
!c  as influence coefficient for node pairs within dipersion element
!
               call cliqdisp (nx, ny, nz, ix, iy, iz,                 &
                          fvpair,npair,aread,areax,dist,d,half_cells, &
                          ia,ja,njamxc,nmax,idbg,cinfrad,             &
                          radial_coord)

!c  check if fully connected "pseudo dispersion element"
!c  was found for dimensionality of problem

               if ((nx .gt. 1 .and. npair(1) .eq. 0) .or.             &
                  (ny .gt. 1 .and. npair(2) .eq. 0) .or.              &
                  (nz .gt. 1 .and. npair(3) .eq. 0)) cycle

!c              loop over the dimensions x,y,z

               do idim = 1, 3

                  idim2 = idim + 1
                  idim3 = idim + 2
                  if (idim2 .gt. 3) idim2 = idim2 - 3
                  if (idim3 .gt. 3) idim3 = idim3 - 3

!c  loop over the number of node pairs in the dimension

                  do ipair = 1, npair(idim)

                     ibk = fvpair(idim, ipair, 1)
                     id = fvpair(idim, ipair, 2)

                     do ii = ia(ibk), ia(ibk+1)-1
                        if (ja(ii) .eq. id) then
                           iisav = ii
                           go to 431
                        endif
                     end do
                     
                     if (rank == 0 .and. b_enable_output) then
                       write(ilog,*) ' error-cannot find id in list-infcrt_g'
                       write(ilog,*) ' ibk, id ', ibk, id
                       close(ilog)
                     end if
#ifdef PETSC
                     call petsc_mpi_finalize
#endif
                     stop
431                  continue
                     iface(idim, ipair) = iisav

                  end do
               end do


!c  average porosity of the element
!c  note: each node is included in "ndim" number of node
!c        pairs, therefore the average must be divided
!c        by ndim as well as the number of nodes in the
!c        element

               porav = r0
               do idim = 1, 3
                  do ipair = 1, npair(idim)
                     ibk = fvpair(idim, ipair, 1)
                     id = fvpair(idim, ipair, 2)
                     porav = porav                    &
                          + dmin1( r1, pornew(ibk) )  &
                          + dmin1( r1, pornew(id) )
                  end do
               end do 

               ndim = 0
               if (nx .gt. 1) ndim = ndim + 1
               if (ny .gt. 1) ndim = ndim + 1
               if (nz .gt. 1) ndim = ndim + 1
               porav = porav / float(ndim) / r2**ndim
               
!c  average tortuosity of the element
!c  note: each node is included in "ndim" number of node
!c        pairs, therefore the average must be divided
!c        by ndim as well as the number of nodes in the
!c        element
           if (assigned_tau) then
               tauloc = r0
               do idim = 1, 3
                  do ipair = 1, npair(idim)
                     ibk = fvpair(idim, ipair, 1)
                     id = fvpair(idim, ipair, 2)
                     !tauloc = tauloc                             &
                     !     + dmin1( r1, tau(ibk) * tau_fac(ibk) ) &
                     !     + dmin1( r1, tau(id) * tau_fac(id) )
                     tauloc = tauloc                              &
                           + tau(ibk) * tau_fac(ibk)              &
                           + tau(id) * tau_fac(id)
                  end do
               end do 

               ndim = 0
               if (nx .gt. 1) ndim = ndim + 1
               if (ny .gt. 1) ndim = ndim + 1
               if (nz .gt. 1) ndim = ndim + 1
               tauloc = tauloc / float(ndim) / r2**ndim
           end if
!c  calculate average diffusion coefficients for the "pseudo
!c  dispersion element"

               diffav = r0

               do idim = 1, 3                      !loop over dimensions
                  do ipair = 1, npair(idim)        !node pairs

                     ibk = fvpair(idim, ipair, 1)
                     id = fvpair(idim, ipair, 2)

                     diffav = diffav + r2 * diffu

                  end do                           !node pairs
               end do                              !dimensions 

               diffav = diffav / float(ndim) / r2**ndim

!c  calculate the diffusion tensor for the "pseudo diffusion element"
!c  average porosity of the element

               satav = r0
               tauav = r0
               so_av = r0
               do idim = 1, 3
                  do ipair = 1, npair(idim)
                     ibk = fvpair(idim, ipair, 1)
                     id = fvpair(idim, ipair, 2)

                     satav = satav                      &
                           + dmin1( r1, sgnew(ibk) )    &
                           + dmin1( r1, sgnew(id) )
                     
	                 tauav = tauav                      &
                           + dmin1( r1, tau_man(ibk) )  &
                           + dmin1( r1, tau_man(id) )
                                                       
	                 so_av = so_av                      &
                           + dmin1( r1, sonew(ibk) )    &
                           + dmin1( r1, sonew(id) )

                  end do
               end do

               satav = satav / float(ndim) / r2**ndim
               tauav = tauav / float(ndim) / r2**ndim
	           so_av = so_av / float(ndim) / r2**ndim
               
               if (.not.assigned_tau) then
                 tauloc = tauav  
               end if
               
!c  calculate the average marchie factor

               marchieav = r0
               do idim = 1, 3
                  do ipair = 1, npair(idim)
                     ibk = fvpair(idim, ipair, 1)
                     id = fvpair(idim, ipair, 2)
                     marchieav = marchieav + marchies(ibk) + marchies(id)
                  end do
               end do
               marchieav = marchieav / float(ndim) / r2**ndim               

!c  calculate effective diffusion coefficient

               diff_eff = diffcoff_g(diffav,satav,porav,tortuosity_corr, &
                                  assigned_tau,tauloc,type_tortuosity, &
                                  marchieav,gas_tortuosity,so_av)
!c  that high value of the effective diffusion coefficient can be obtained
!c  for the Millington's method in the chamber simulation where the assigned
!c  tortuosity is no longer used. The condition is based on the fact that the
!c  porosity in the chamber is greater than 1. So the zone for the high
!c  diffusion coefficient can be linked to the chamber. Jiao Zhao, 2015.
               !if (pornew(ibk) > 1.0d0) then
               !  diff_eff = diff_eff * 500
               !end if

               tend(1) = diff_eff

               tend(2) = diff_eff

               tend(3) = diff_eff
               
!c  calculate gas filled porosity
               gpor =  satav * porav
               
			   if (gpor.lt.rverysmall) then 
                 tort = r0
               else
                 tort = diff_eff / diffav / gpor  ! tortuosity         
	           endif

!c  build total influence coefficient from all elemental contributions
#ifdef OPENMP
    !$omp critical
#endif 
               do idim = 1, 3                 !loop over dimensions
                  do ipair = 1, npair(idim)   !node pairs

                     iisav = iface(idim, ipair)
                     
                     cinfrt_dg(iisav) = cinfrt_dg(iisav)      &
                                     + tend(idim)             &
                                     * aread(idim, ipair)

                     cinfrt_dg(isymm(iisav)) = cinfrt_dg(isymm(iisav)) &
                                            + tend(idim)               &
                                            * aread(idim, ipair)

                     gsatij(iisav)           = satav
                   
                     gsatij(isymm(iisav))    = satav
                     
                     gporij(iisav)           = gpor
                     
                     gporij(isymm(iisav))    = gpor
                     
                     deltaij(iisav)          = dist(idim, ipair)

                     deltaij(isymm(iisav))   = dist(idim, ipair)
                     
                     tauij(iisav)            = tort
                     tauij(isymm(iisav))     = tort                     
                     
                  end do                      !node pairs
               end do                         !loop over dimensions
               
#ifdef OPENMP
    !$omp end critical
#endif 
               
            end do   
         end do      
      end do
#ifdef OPENMP
    !$omp end do
    !$omp end parallel
#endif

!cdbg
!c     do ibk = 1,nmax
!c       do ii = ia(ibk),ia(ibk+1)-1
!c         write(idbg,*) 'cinfrt_dg(',ii,') = ',cinfrt_dg(ii)
!c       end do
!c     end do
!c	   do ibk = 1,nmax
!c       do ii = ia(ibk),ia(ibk+1)-1
!c         write(idbg,*) 'gporij(',ii,') = ',gporij(ii)
!c       end do
!c     end do
!      
!c	   do ibk = 1,nmax
!c       do ii = ia(ibk),ia(ibk+1)-1
!c         write(idbg,*) 'deltaij(',ii,') = ',deltaij(ii)
!c       end do
!c     end do
!#ifdef PETSC
!      call petsc_mpi_finalize
!#endif
!c     stop
!cdbg

      return
      end

