!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/multidiff.F90 $
!---------------------------------------------------------------------
!********************************************************************!

    
module multidiff

    use parm
    
    implicit none

! prc -------------------------------------------------------------------
! defining the vectors for diffusion coefficients for primary and 
! secondary spices
! prc -------------------------------------------------------------------
    logical :: multi_diff = .false.
    logical :: hmulti_diff = .false.
    logical :: poisson           
    logical :: electric_field
    logical :: electric_field_file
    logical :: microporosity
      
    real (type_r8), allocatable :: mdiff_ic(:)         ! diffusion coefficient of free aqueous species
    real (type_r8), allocatable :: mdiff_ix(:)        ! diffusion coefficient of secondary aqueous complexes
      
    real (type_r8) :: diffcoff1
    real (type_r8) :: diffcoff2
      
    real (type_r8), allocatable :: cinfrt_mcd(:)       ! influence coeff for Multi-diff
                                                       ! in the aqueous phase
    real (type_r8), allocatable :: deltaij(:)          ! distance between i-j
    real (type_r8), allocatable :: tauij(:)            ! aqueous tortuosity at i-j
    real (type_r8), allocatable :: porij(:)            ! aqueous  porosity at i-j
    real (type_r8), allocatable :: satij(:)            ! aqueous saturation at i-j
    real (type_r8), allocatable :: area_d(:)           ! interfacial area between i-j
    real (type_r8), allocatable :: dtotviscnew(:,:)    ! d (concentration * diffusion coef.) in current cvolume
    real (type_r8), allocatable :: delecmigrationnew(:,:)
    
    real (type_r8), allocatable :: dtotviscnewMP(:)    ! d (concentration * diffusion coef.) in current cvolume
    real (type_r8), allocatable :: delecmigrationnewMP(:)
!    real (type_r8), allocatable :: elecmig(:)
!    real (type_r8), allocatable :: elecmig_pert(:)     
         
    real (type_r8), allocatable :: totviscnew(:,:)      ! concentration * diffusion coef. in each cvolume   
    real (type_r8), allocatable :: totviscnewMP(:,:)
   
    real (type_r8), allocatable :: totviscnewjvol(:)    ! this is used only in parallel version, private variable for each thread. DSU
    
    real (type_r8), allocatable :: mdiff_ic_cvol(:,:)
    real (type_r8), allocatable :: mdiff_ix_cvol(:,:)
    
    real (type_r8), allocatable :: electromignew(:,:)
    real (type_r8), allocatable :: electromignew_inc(:,:)
    
    real (type_r8), allocatable :: electromignewMP(:,:)
    real (type_r8), allocatable :: electromignew_incMP(:,:)
    
    real (type_r8), allocatable :: totcnewMP(:,:)
    real (type_r8), allocatable :: totcoldMP(:,:)

    real (type_r8), allocatable :: driftnew(:,:)
    real (type_r8), allocatable :: ddriftnew(:)
    
    real (type_r8), allocatable :: zc(:) 
    real (type_r8), allocatable :: zcabs(:)
    
!    real (type_r8), allocatable :: f_epor(:) 
    
!    real (type_r8), allocatable :: SumChargeMicropore(:)

    real (type_r8), allocatable :: DonnanPotentialTmp(:)  
      
! prc --------------------------------------------------------------------
! prc --------------------------------------------------------------------
! prc --------------------------------------------------------------------
 
 
 end module multidiff
