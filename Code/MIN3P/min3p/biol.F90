!c		    Celine Blitz Frayret (CBF), from May 9, 2017 - According to the MIN3P-ArchiSimple,
!c          and Frédéric Gérard (FG) - dec 2020 - to prepare coupling with SiriusQuality (root and shoot)
    module biol

    use parm
    
    implicit none!FG dec 2020
	
	integer :: cmws
	real (type_r8) :: rewm, p1, rew0 !FG dec 2020 p1 and rew0 moved from phys.F90 and defined as scalars
	logical :: vegetation_growth
	logical :: passive_uptake
	logical :: rootlengthdens_field
	logical :: update_root_rld 
	logical :: inside_rld
	logical :: coupled_archi_rld
    logical :: coupled_sirius
	logical :: rootdensitynill
    real (type_r8) :: rootdens!FG sept 2021
	real (type_r8), allocatable :: rootlengthdens(:)
	real (type_r8), allocatable :: rld(:)
    real (type_r8) :: time_rld!update time rld
	real (type_r8) :: canopy_evap_factor
	real (type_r8), allocatable :: uptakefactor(:)
	real (type_r8), allocatable :: puf(:)
!           h1lim(nzn)         = water pressure at wilting point
!           h1field(nzn)       = water pressure at field capacity
!           h1opt(nzn)         = optimal water pressure (Feddes)
!           satwlim(nzn)       = water saturation at wilting point
!           satwfield(nzn)     = water saturation at field capacity
!           satwopt(nzn)       = optimal water saturation (Feddes correction)
    real (type_r8), allocatable :: satwlim(:) !FG dec 2020 - moved from phys.f90, as it was
    real (type_r8), allocatable :: satwfield(:) !FG dec 2020 - moved from phys.f90, as it was
    real (type_r8), allocatable :: satwopt(:) !FG dec 2020 - moved from phys.f90, as it was    
    real (type_r8), allocatable :: h1lim(:)!FG dec 2020
    real (type_r8), allocatable :: h1field(:)!FG dec 2020
    real (type_r8), allocatable :: h1opt(:)!FG dec 2020
  
	end module biol
