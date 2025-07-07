subroutine MinToSQCalculLeakage(nvx, nvy, nvz, iavs, javs, cinfvs, dimcv, xg,&
     &                    yg, zg, uvsnew, hhead, relperm, idbg, ilog,  &
     &                    ivel, upstream, fully_saturated, njavs, nn,  &
     &                    nn_loc, half_cells,cinfrad,radial_coord,     &
     &                    offset)


      use gen, only : rank, b_enable_output, node_idx_lg2l,            &
                      b_output_binary,realbuffer,b_output_mpiio_single,&
                      b_output_multizone,                              &
                      b_mpiarray_ivel_init,mpiarray_filetype_ivel,     &
                      mpiarray_sizes_gbl_ivel,mpiarray_sizes_sub_ivel, &
                      mpiarray_starts_sub_ivel,lost_water
#ifdef PETSC
      use petsc_mpi_common, only : petsc_mpi_finalize
#endif

      use module_binary_mpiio, only : binary_write_data,               &
                                      binary_subarray_initialize

      implicit none
#ifdef PETSC_V3_6_X
#include <petsc/finclude/petscsys.h>
#elif PETSC
#include <finclude/petscsys.h>
#endif
      
      external cliqdisp, fluxfs, fluxvs

!c  passed variables

      integer nvx, nvy, nvz, njavs, nn, nn_loc, iavs(nn+1),            &
              javs(njavs), idbg, ilog, ivel   
                                                                       
      logical half_cells, fully_saturated, upstream, radial_coord
                                                                       
      real*8  uvsnew(nn), hhead(nn), relperm(nn), dimcv(3,nn),        &
     &        cinfvs(njavs), xg(nn), yg(nn), zg(nn),cinfrad(njavs) 

      real*8 fluxfs, fluxvs
      
#ifdef MPI
      integer(kind=MPI_OFFSET_KIND) :: offset, offset_temp
#else
      integer*8 :: offset, offset_temp
#endif

!c  local variables

      real*8 eps, r0, rhalf
      parameter (eps = 1.0d-300, r0 = 0.0d0, rhalf = 0.5d0)

      real*8 aread(3,12),areax(3,12),vel(3), dist(3,12), areai, dflux, &
             xg_out, yg_out, zg_out

      integer ivol, ivz, ivy, ivx, i1, jvol, i1sav, idim, npair(3),   & 
     &        cvpair(3,12,2), ipair, idim2, idim3, ivol2, ivol_l

      character*1 iups      
      
      if (b_output_binary) then 
        allocate(realbuffer(nn_loc*6))
        realbuffer = 0.0d0
      end if

!c  compute average interfacial velocities

      ivol2 = 0
      ivol_l = 0

      do ivz = 1, nvz          !increments in z-direction
        do ivy = 1, nvy        !increments in y-direction
          do ivx = 1, nvx      !increments in x-direction

            ivol2 = ivol2 + 1  !counter - control volumes
            
            !skip ghost nodes
#ifdef PETSC
            if(node_idx_lg2l(ivol2) < 0) then
                cycle
            end if
#endif
         
!c  find node pairs for interfacial velocities
            call cliqdisp (nvx, nvy, nvz, ivx, ivy, ivz,              &
     &                     cvpair, npair,aread,areax,dist,dimcv,      &
     &                     half_cells,                                &
     &                     iavs,javs,njavs,                           &
     &                     nn,idbg,cinfrad,radial_coord)
                                                                      
!c  check connections
                                                                       
            if ((nvx .gt. 1 .and. npair(1) .eq. 0) .or.               &
     &          (nvy .gt. 1 .and. npair(2) .eq. 0) .or.               &
     &          (nvz .gt. 1 .and. npair(3) .eq. 0)) goto 400
            
            ivol_l = ivol_l + 1

!c  loop over the dimensions x,y,z

            do idim = 1, 3

              idim2 = idim + 1
              idim3 = idim + 2
              if (idim2 .gt. 3) idim2 = idim2 - 3
              if (idim3 .gt. 3) idim3 = idim3 - 3

!c  zero average interfacial velocity

              vel(idim) = r0

!c  loop over the number of control volume pairs in the dimension

              do ipair = 1, npair(idim)

                ivol = cvpair(idim, ipair, 1)
                jvol = cvpair(idim, ipair, 2)

!c  calculate average interfacial area between nodes
!cprovi------------------------------------------------
!cprovi Assign the interfacial area
!cprovi------------------------------------------------                
                areai = areax(idim, ipair)

                do i1 = iavs(ivol), iavs(ivol+1)-1
                  if (javs(i1) .eq. jvol) then
                    i1sav = i1
                    go to 500
                  endif
                end do
#ifdef PETSC
                call petsc_mpi_finalize
#endif
                stop
500             continue

!c  find flux between control volume pair

                if (.not.fully_saturated) then
                  if (upstream) then
                    iups = 'i'                            !h_i >= h_j
                    if (hhead(jvol).gt.hhead(ivol)) then  !h_j > h_i
                      iups = 'j'
                    end if
                  end if

                  dflux = - fluxvs(upstream,hhead(ivol),              &
     &                             hhead(jvol),relperm(ivol),         &
     &                             relperm(jvol),iups,                &
     &                             cinfvs(i1sav))
                                                                       
                elseif (fully_saturated) then 
                                                                       
                  dflux = - fluxfs(uvsnew(ivol),uvsnew(jvol),         &
     &                             cinfvs(i1sav))
                end if


!c  velocity is the flux (m^3/day) divided
!c  by the interfacial area between the two control volumes

                vel(idim) = vel(idim) - dflux / areai

              end do

!c  find the average interfacial velocity in the x,y,z directions

              vel(idim) = vel(idim)/(float( npair(idim) ) + eps)
              
              if (ivx.eq.1 .and. ivy.eq.1 .and. ivz.eq.2 .and. idim.eq.3) then
                lost_water = r0
                !lost_water = -0.008
                lost_water = vel(3)
              endif
            end do
            
400         continue

          end do
        end do
      end do

      return
      end

