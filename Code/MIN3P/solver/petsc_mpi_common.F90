!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/solver/petsc_mpi_common.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!> module: solver_ksp_common
!>
!> written by: Danyang Su
!>
!> module description:
!>
!> Module of linear functions 
!>
!> Note: This module is not high efficient at present. 
!> See http://www.mcs.anl.gov/petsc/ for detail


module petsc_mpi_common

    implicit none
    
    contains
    
    !> Initialize petsc 
    subroutine petsc_mpi_initialize(rank,nprcs)    
    
        implicit none        
#ifdef PETSC_V3_6_X
#include <petsc/finclude/petscsys.h>
#elif PETSC
#include <finclude/petscsys.h>
#endif

        integer*4 :: rank
        integer*4 :: nprcs
        
#ifdef PETSC
        PetscErrorCode :: ierr
#endif

#ifdef PETSC
        call PetscInitialize(Petsc_Null_Character,ierr)  
        CHKERRQ(ierr)
        call MPI_Comm_rank(Petsc_Comm_World,rank,ierr)
        CHKERRQ(ierr)
        call MPI_Comm_size(Petsc_Comm_World,nprcs,ierr)
        CHKERRQ(ierr)
#ifdef DEBUG
        write(*,'(2(a,1x,i4,1x),a)') "rank",rank,"of",nprcs,"processors"
#endif
#endif
        
    end subroutine petsc_mpi_initialize
    
    !> Release solver space and end parallel region
    subroutine petsc_mpi_finalize
    
        implicit none
#ifdef PETSC_V3_6_X
#include <petsc/finclude/petscsys.h>
#elif PETSC
#include <finclude/petscsys.h>
#endif
        
#ifdef PETSC
        PetscErrorCode :: ierr
        call PetscFinalize(ierr)
        CHKERRQ(ierr)
#endif
        
    end subroutine petsc_mpi_finalize
    
    !> MPI Barrier
    subroutine petsc_mpi_barrier
        
        implicit none
#ifdef PETSC_V3_6_X
#include <petsc/finclude/petscsys.h>
#elif PETSC
#include <finclude/petscsys.h>
#endif
        
#ifdef PETSC
        integer :: ierr
        call MPI_Barrier(Petsc_Comm_World, ierr)
        CHKERRQ(ierr)
#endif
    
    end subroutine petsc_mpi_barrier
    
end module petsc_mpi_common
