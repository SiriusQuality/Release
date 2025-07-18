!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 453 $
!> $Author: dsu $
!> $Date: 2017-02-21 19:54:05 +0100 (Tue, 21 Feb 2017) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/mem_vs.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!c -----------------------------------------------------------------------
!c subroutine mem_vs
!c -----------------
!c
!c allocate memory for flow simulation
!c
!c written by:      Uli Mayer - Januray 6, 2000
!c                  Sergi Molins - May 15, 2006
!c                  allocated sonew(nn)
!c                  Anna Harrison - Jan 24, 2014
!c                  allocated qh2o for hydrated carb water sequestn
!c
!c last modified:   -
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
!c           hhead(nngl)          = hydraulic head                      * +
!c           perm_fac(nngl)       = scaling factor for permeability     * +
!c                                as a function of porosity changes
!c           tau_fac(nngl)        = updated tortuosity as the function
!c                                of changed porosity
!c                                tau = tau_0 * por^alpha / por_0^alpha
!c                                tauupdate_fac = por^alpha / por_0^alpha
!c           pornew(nngl)         = porosity                            * +
!c           por_init(nngl)       = initial porosity                    * +
!c           sgnew(nngl)          = gaseous phase saturation            * +
!c                                (new time level)
!c           sgold(nngl)          = gaseous phase saturation            * +
!c                                (old time level)
!c           sainc(nngl)          = aqueous phase saturation            * +
!c                                (incremented)
!c           sanew(nngl)          = aqueous phase saturation            * +
!c                                (new time level)
!c           snnew(nngl)          = NAPL phase saturation               * +
!c                                (new time level)
!c           saold(nngl)          = aqueous phase saturation            * +
!c                                (time level N)
!c           saold2(nngl)          = aqueous phase saturation            * +
!c                                (time level N-1)
!c           relperm(nngl)        = relative permeability               * +
!c           relpinc(nngl)        = relative permeability (incremented) * +
!c           uvsinc(nngl)         = solution vector (incremented)       * +
!c           uvsnew(nngl)         = solution vector (new time level)    * +
!c           uvsold(nngl)         = solution vector (old time level)    * +
!c           vsflux(ncon-1)     = interfacial fluxes                  * +
!c           bvs(nngl)            = rhs vector                          * +
!c           resvs(nngl)          = residual                            * +
!c           uvs(nngl)            = update towards solution-vector      * +
!c           relpermg(nn)       = relative gas permeability           * +
!c
!c
!c           integer*4:
!c           ----------
!c           ilog               = unit number, logbook file           + -
!c           iavs(nngl+1)         = row pointer array for 1d-scalar     * +
!c                                matrix
!c           iafvs(nngl+1)        = row pointer array for afvs          * +
!c           iafdvs(nngl)         = diagonal pointer array for afvs     * +
!c           lordervs(nngl)       = array containing ordering           * +
!c           invordvs(nngl)       = array containing inverse ordering   * +
!c           mpropvs(nngl)        = pointer array for allocation of     * +
!c                                material properties
!c
!c           character:
!c           ----------
!c           iups(ncon-1)       = upstream pointer                    * +
!c
!c local:    integer*4:
!c           ----------
!c           ierr               = 0 -> memory allocation successful
!c
!c external: checkerr  = check for error during memory allocation 
!c
!c---------------------------------------------------------------------- 
!c WARNING
!c---------------------------------------------------------------------- 
!c All allocated variables are initialized to some value. This depends  
!c of its type:
!c 
!c real*8      => 0.0d0
!c integer     => 0 
!c character   => ' '    
!c logical     => .false. 
!c 
!c Sergio Andres Bea Jofre (2009)
!c
!c ----------------------------------------------------------------------
  
      subroutine mem_vs
 
      use parm
      use gen
      use bbls
#ifdef OPENMP
      use omp_lib 
#endif

      implicit none
      
      integer :: ierr

      external checkerr
 
!c  allocate memory for reactive transport simulation

!c  main variables - variably saturated flow
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp parallel private(ierr)
    !$omp sections
#endif
#endif

#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (uvsnew(nngl), stat = ierr)
      uvsnew=0.0d0
      call checkerr(ierr,'uvsnew',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (uvsold(nngl), stat = ierr)
      uvsold=0.0d0
      call checkerr(ierr,'uvsold',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (uvsinc(nngl), stat = ierr)
      uvsinc=0.0d0
      call checkerr(ierr,'uvsinc',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (hhead(nngl), stat = ierr)
      hhead=0.0d0
      call checkerr(ierr,'hhead',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (saold(nngl), stat = ierr)
      saold=0.0d0
      call checkerr(ierr,'saold',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (sanew(nngl), stat = ierr)
      sanew=0.0d0
      call checkerr(ierr,'sanew',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (stor(nngl), stat = ierr)
      stor=0.0d0
      call checkerr(ierr,'stor',ilog)
      
!cprovi---------------------------------------------
!cprovi---------------------------------------------
!cprovi--------------------------------------------- 
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      if (compute_ice_sheet_loading) then   
         allocate (skempton(nngl), stat = ierr)
         skempton=1.0d0
         call checkerr(ierr,'skempton',ilog)
      end if 
!cprovi---------------------------------------------
!cprovi---------------------------------------------
!cprovi--------------------------------------------- 
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (sgold(nngl), stat = ierr)
      sgold=0.0d0
      call checkerr(ierr,'sgold',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (sgnew(nngl), stat = ierr)
      sgnew=0.0d0
      call checkerr(ierr,'sgnew',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (sainc(nngl), stat = ierr)
      sainc=0.0d0
      call checkerr(ierr,'sainc',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (sonew(nngl), stat = ierr)
      sonew=0.0d0
      call checkerr(ierr,'sonew',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif
      allocate (tauivol(nngl), stat = ierr)
      tauivol=0.0d0
      call checkerr(ierr,'tauivol',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif
      allocate (qwater(nngl), stat = ierr)
      qwater=0.0d0
      call checkerr(ierr,'qwater',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif
      allocate (relperm(nngl), stat = ierr)
      relperm=0.0d0
      call checkerr(ierr,'relperm',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (relpermg(nngl), stat = ierr)
      relpermg=0.0d0
      call checkerr(ierr,'relpermg',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (relpinc(nngl), stat = ierr)
      relpinc=0.0d0
      call checkerr(ierr,'relpinc',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (relpincg(nngl), stat = ierr)
      relpincg=0.0d0
      call checkerr(ierr,'relpincg',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (perm_fac(nngl), stat = ierr)
      perm_fac=0.0d0
      call checkerr(ierr,'perm_fac',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (tau_fac(nngl), stat = ierr)
      tau_fac = 1.0d0
      call checkerr(ierr,'tau_fac',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (marchies(nngl), stat = ierr)
      marchies = 0.0d0
      call checkerr(ierr,'marchies',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (pornew(nngl), stat = ierr)
      pornew=0.0d0
      call checkerr(ierr,'pornew',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (por_stress(nngl), stat = ierr)
      por_stress=0.0d0
      call checkerr(ierr,'por_stress',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (por_stress_old(nngl), stat = ierr)
      por_stress_old=0.0d0
      call checkerr(ierr,'por_stress_old',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif
      allocate (porold(nngl), stat = ierr)
      porold=0.0d0
      call checkerr(ierr,'porold',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (por_init(nngl), stat = ierr)
      por_init=0.0d0
      call checkerr(ierr,'por_init',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (qh2o(nngl), stat = ierr)
      qh2o = 0.0d0
      call checkerr(ierr,'qh2o',ilog)
      
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (mpropvs(nngl), stat = ierr)
      mpropvs=0
      call checkerr(ierr,'mpropvs',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (snnew(nngl), stat = ierr)
      snnew=0.0d0
      call checkerr(ierr,'snnew',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (saold2(nngl), stat = ierr)
      saold2=0.0d0
      call checkerr(ierr,'saold2',ilog)
 
!c  newton iteration - variably saturated flow
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (vsflux(ncon-1), stat = ierr)
      vsflux=0.0d0
      call checkerr(ierr,'vsflux',ilog)
 
      allocate (iups(ncon-1), stat = ierr)
      iups=' ' 
      call checkerr(ierr,'iups',ilog)

!c  data structure and solver - variably saturated flow
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (bvs(nngl), stat = ierr)
      bvs=0.0d0
      call checkerr(ierr,'bvs',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (uvs(nngl), stat = ierr)
      uvs=0.0d0
      call checkerr(ierr,'uvs',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (resvs(nngl), stat = ierr)
      resvs=0.0d0
      call checkerr(ierr,'resvs',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (iavs(nngl+1), stat = ierr)
      iavs=0
      call checkerr(ierr,'iavs',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (row_idx_l2pg_vs(nngl+1), stat = ierr)
      row_idx_l2pg_vs=0
      call checkerr(ierr,'row_idx_l2pg_vs',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (iafvs(nngl+1), stat = ierr)
      iafvs=0
      call checkerr(ierr,'iafvs',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (iafdvs(nngl), stat = ierr)
      iafdvs=0 
      call checkerr(ierr,'iafdvs',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (lordervs(nngl), stat = ierr)
      lordervs=0 
      call checkerr(ierr,'lordervs',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (invordvs(nngl), stat = ierr)
      invordvs=0 
      call checkerr(ierr,'invordvs',ilog)
      
!c_bubbles variables for bubble problem
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (unsaturated(nngl), stat = ierr)
      unsaturated = .false.
      call checkerr(ierr,'unsaturated',ilog)

#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (solvegb(nngl), stat = ierr)
      solvegb = .false.
      call checkerr(ierr,'solvegb',ilog)
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (sg_temp(nngl), stat = ierr)
      sg_temp=0.0d0
      call checkerr(ierr,'sg_temp',ilog)    
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      allocate (sgt_old(nngl), stat = ierr)
      sgt_old=0.0d0
      call checkerr(ierr,'sgt_old',ilog)
      
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
    allocate (sa_min_old(nngl), stat = ierr)
    sa_min_old=0.0d0
    call checkerr(ierr,'sa_min_old',ilog)
    
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
    allocate (sa_min(nngl), stat = ierr)
    sa_min=0.0d0
    call checkerr(ierr,'sa_min',ilog)
    
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp section
#endif
#endif 
      if (trap_bubbles) then
          
        allocate (sa_app(nngl), stat = ierr)
        sa_app=0.0d0
        call checkerr(ierr,'sa_app',ilog)
        
        allocate (sa_eff(nngl), stat = ierr)
        sa_eff=0.0d0
        call checkerr(ierr,'sa_eff',ilog)
        
        allocate (sg_eff(nngl), stat = ierr)
        sg_eff=0.0d0
        call checkerr(ierr,'sg_eff',ilog)
        
        allocate (sgt(nngl), stat = ierr)
        sgt=0.0d0
        call checkerr(ierr,'sgt',ilog)
        
        allocate (drainage(nngl), stat = ierr)
        drainage = .false.
        call checkerr(ierr,'drainage',ilog)

        allocate (main_drain(nngl), stat = ierr)
        main_drain = .false.
        call checkerr(ierr,'main_drain',ilog)

        allocate (big_bubble(nngl), stat = ierr)
        big_bubble = .false.
        call checkerr(ierr,'big_bubble',ilog)

        allocate (big_bub_old(nngl), stat = ierr)
        big_bub_old = .false.
        call checkerr(ierr,'big_bub_old',ilog)
    end if
      
#ifdef OPENMP
#ifdef PARALLEL_SECTION
    !$omp end sections
    !$omp end parallel
#endif
#endif 
      return
      end
