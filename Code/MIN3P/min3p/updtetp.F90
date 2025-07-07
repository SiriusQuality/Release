 !c ----------------------------------------------------------------------
 !c subroutine updtetp
 !c -------------------
 !c
 !c update etp and canopy dependent variables 
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
 !c common:   
 !c gen.f:    real*8:
 !c           -------
 !c           pet                = potential evapotranspiration        * +
 !c           pe_soil            = potential soil evaporation          * +
 !c           scale_tree_growth  = scale factor to account for the influence
 !c                                of tree growth on transpiration
 !c           canopy_int         = canopy interception                 * +
 !c           sec_per_days       = conversion factor from SI input     + -
 !c                                units for physico-chemical 
 !c                                parameters internal time units      
 !c           tfinal             = final solution time                 + -
 !c           time_factor        = conversion factor from I/O time     + -
 !c                                units to internal time units
 !c           time_io            = current solution time (I/O units)   + -
 !c           time_soi           = next read time for etp specific    * +
 !c                                parameters
 !c           
 !c           integer*4:
 !c           ----------
 !c           ilog               = unit number - logbok file           + -
 !c           isoi               = unit number - soil specific         + - 
 !c                                parameters
 !c phys.f:   real*8:
 !c           -------
 !c           canopy_evap_factor = canopy evaporation factor      + -
 !c
 !c local:    real*8:
 !c           -------
 !c           tiny               = small increment
 !c           solar_ratio        = ratio between the solar energy at the forest floor
 !c                                and this above tree canopy
 !c
 !c
 !c external: -
 !c ----------------------------------------------------------------------
 
      subroutine updtetp
 
      use parm
      use gen
	  use phys
	  use biol
    
#ifdef PETSC !from updtsoil.f90 (initial trunk version)
      use petsc_mpi_common, only : petsc_mpi_finalize
#endif

      implicit real*8 (a-h,o-z)

      parameter (tiny = 1.d-10)
      
      logical::first = .false. !TR
      
      if (time_io.gt.time_soi-tiny) then

 !c  read file containing time dependent parameters for root water uptake
 !c  and physical evap
          
        if(coupled_sirius) then !TR - Ajout de l'option de couplage avec Sirius
            !tp_sirius = tp_sirius / 1000 !TR mise à l'unité : mm/d -> m/s
            !ep_sirius = ep_sirius / 1000 !TR mise à l'unité : mm/d -> m/s
            pe_soil = ep_sirius
            time_soi = time_soi + 1
        
        else
 
            read(isoi,*,err=998,end=997) time_soi,pet,canopy_int,	&
         &                               solar_ratio,scale_tree_growth


	        pe_soil = pet*solar_ratio*sec_per_days
        endif

	
	 if (.not.pure_evap) then ! if not pure evaporation calculate
	                          ! potential evapotranspiration and canopy intercept.
	  !pet = pet*sec_per_days
         pet = tp_sirius + ep_sirius
	  canopy_int = canopy_int*sec_per_days

     else ! then pure evaporation set, and potential phys. evap. = 100% ETP

	  !pe_soil = pet*sec_per_days
         pe_soil = ep_sirius

     endif
     
     return

!c  assign next read time greater than final solution time, if no more
!c  read times left and return

997     time_soi = 1.1d0*tfinal/time_factor
        return

998     continue
        if (rank == 0) then
          write(ilog,*) 'SIMULATION TERMINATED' 
          write(ilog,*) 'error reading file ', prefix(:l_prfx)//'.soi'
          close(ilog)
        end if
#ifdef PETSC !from updtsoil.f90 (initial trunk version)
        call petsc_mpi_finalize
#endif
        stop
        
      end if ! check for time

	  return

      end
