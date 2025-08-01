!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/drategas.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c ----------------------------------------------------------------------
!c subroutine drategas
!c -------------------
!c
!c compute derivatives of degassing rates
!c
!c written by:      Uli Mayer - March 1, 00
!c
!c last modified:   -
!c
!c definition of variables:
!c
!c I --> on input   * arbitrary  - initialized  + entries expected
!c O --> on output  * arbitrary  - unaltered    + altered
!c                                                                    I O
!c common:
!
!c passed:   real*8:
!c           -------
!c           tkel               = nodal temperatures in Kelvin        + - 
!c           g(ng)              = gas concentrations                  + -
!c                                - new time level [moles/l air]
!c           hhead              = hydraulic head                      + -
!c           zg                 = spatial coordinates in z-direction  + -
!c           drtinc             = increment for numerical             + -
!c                                differentiation
!c 
!c chem.f:   real*8:
!c           -------
!c           degas_rate         = rate constant for degassing         + -
!c                                [mol L-1 h2o s-1]
!c           g_acc              = gravitational acceleration vector   + -
!c                                [m s^-2]
!c           ginc(ng,nthreads)  = gas concentrations dependent on     + -
!c                                incremented concentrations of
!c                                components as species in solution
!c                                [moles/l air]
!c           pa_atm             = conversion factor [Pa atm ^-1]      + -
!c           pres_atm           = atmospheric pressure [atm]          + -
!c           rateg(ng,nthreads) = degassing rates                     * +
!c           rho_w              = density of water [kg m^-3]          + -
!c           rgasatm               = ideal gas constant                  + -
!c                                [liter atm/(deg K mole)]
!c
!c           integer*4:
!c           ----------
!c           ng                 = number of gases                     + -
!c
!c local:    real*8:
!c           -------
!c           pres_tot           = total gas pressure [atm]
!c           pres_tot_inc       = total gas pressure [atm]
!c                                - incremented
!c           pres_conf          = confining pressure [atm]
!c           r0                 = constant
!c           r1                 = constant
!c           rate               = reaction rate [mol L^-1 h2o s^-1]
!c           rate_inc           = reaction rate [mol L^-1 h2o s^-1]
!c                                - incremented
!c
!c           integer*4:
!c           ----------
!c           ig                 = counter (gases)
!c
!c external: -
!c ----------------------------------------------------------------------
  
      subroutine drategas(g,tkel,hhead,zg,drtinc)
 
      use parm
      use chem
      use bbls
      use gen, only : rank, ilog, b_enable_output
#ifdef OPENMP
      use omp_lib 
#endif 
#ifdef PETSC
      use petsc_mpi_common, only : petsc_mpi_finalize
#endif 
      implicit none
      
      real*8 :: g, tkel, hhead, zg, drtinc
      
      integer :: tid, info_debug, ig
      
      real*8 :: pres_tot, pres_tot_inc, pres_conf, rate, rate_inc

      real*8, parameter :: r0 = 0.0d0, r1 = 1.0d0

      dimension g(*)
      
#ifdef OPENMP
      tid = omp_get_thread_num() + 1
#else
      tid = 1
#endif

!c  initialize array for degassing rates

      do ig = 1,ng
        rateg(ig,tid) = r0
      end do

!c  compute total gas pressure

      pres_tot = r0
      pres_tot_inc = r0
      do ig = 1,ng
        pres_tot = pres_tot + rgasatm*tkel*g(ig)
        pres_tot_inc = pres_tot_inc + rgasatm*tkel*ginc(ig,tid)
      end do

!c  compute confining pressure

      pres_conf = pres_atm    &
               + (rho_w * g_acc * (hhead - zg))/pa_atm

!c  compute total rate of degassing, if sum of partial pressures 
!c  exceeds confining pressure
      if (.not.gas_bubbles) then
        if (pres_tot.gt.pres_conf) then

           rate = -degas_rate * (r1-pres_tot/pres_conf)
           rate_inc = -degas_rate * (r1-pres_tot_inc/pres_conf)

!c  compute rates for specific gases depending on mole fraction in 
!c  escaping gas mixture

          do ig = 1,ng
            rateg(ig,tid) = (rgasatm*tkel*ginc(ig,tid)/pres_tot_inc *  &
                        rate_inc - rgasatm*tkel*g(ig)/pres_tot * rate) &
                        / drtinc
          end do
        end if
      end if

!cdbg ---- activate this section for purposes of debugging -----
#ifdef DEBUG
      info_debug = 0
      if (info_debug.gt.0 .and. b_enable_output .and. rank == 0) then
        if (.not.gas_bubbles) then  
          if (pres_tot.gt.pres_conf) then
            write(*,'(a)') 'returning from drategas ...'
              do ig = 1,ng
                write(*,"(a, 1x, e15.4)") trim(nameg(ig)), rateg(ig,tid)
              end do
            write(*,*) '----------------------------------------------'
          end if
        end if
      end if

      if (info_debug.gt.1) then
#ifdef PETSC
        call petsc_mpi_finalize
#endif
        close(ilog)
        stop
      end if
#endif

      return
      end
