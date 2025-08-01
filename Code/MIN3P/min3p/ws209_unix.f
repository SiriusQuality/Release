c!======================================================================!
c!                                                                      !
c!                              WatSolv                                 !
c!                                                                      !
c!                           Version 2.09                               !
c!                                                                      !
c!                  Iterative Sparse Matrix Solver                      !
c!                                                                      !
c!                    Copyright (c) 1995 - 1996                         !
c!                                                                      !
c!         J.E. VanderKwaak    (kwaak@galerkin.uwaterloo.ca)            !
c!         P.A. Forsyth        (paforsyth@yoho.uwaterloo.ca)            !
c!         K.T.B. MacQuarrie   (kerry@cgrnserc.uwaterloo.ca)            !
c!         E.A. Sudicky        (sudicky@cgrnserc.uwaterloo.ca)          !
c!                                                                      !
c!             Waterloo Centre for Groundwater Research                 !
c!                      University of Waterloo                          !
c!                Waterloo, Ontario, Canada, N2L 3G1                    !
c!                                                                      !
c!----------------------------------------------------------------------!
c!                                                                      !
c!     * Bi-Conjugate Gradient Stabilized (Bi-CGSTAB) Acceleration      !
c!                                                                      !
c!     * modified ia,ja data structure.                                 !
c!                                                                      !
c!     * Natural or Reverse Cuthill-McKee ordering of unknowns          !
c!                                                                      !
c!     * Right preconditioning by variable level incomplete             !
c!        lower/upper factorization                                     !
c!                                                                      !
c!     * all calculations performed in double precision                 !
c!                                                                      !
c!======================================================================!
c!                                                                      !
c!             COPYRIGHT NOTICE AND USAGE LIMITATIONS                   !
c!                                                                      !
c! ALL RIGHTS ARE RESERVED; THE WATSOLV SUBROUTINES AND USER'S GUIDE    !
c! ARE COPYRIGHT. THE DOCUMENTATION AND SOURCE CODE, OR ANY PART        !
c! THEREOF, MAY NOT BE REPRODUCED, DUPLICATED, TRANSLATED, OR           !
c! DISTRIBUTED IN ANY WAY WITHOUT THE EXPRESS WRITTEN PERMISSION OF     !
c! THE COPYRIGHT HOLDERS. PAPERS OR REPORTS PRODUCED USING WATSOLV      !
c! SHOULD EXPLICITLY ACKNOWLEDGE ITS SOURCE. THE WATSOLV PACKAGE, OR    !
c! ANY OF ITS COMPONENT SUBROUTINES, MUST BE SPECIFICALLY LICENSED FOR  !
c! INCLUSION IN SOFTWARE DISTRIBUTED IN ANY MANNER AND/OR SOLD          !
c! COMMERCIALLY. THIS VERSION IS FOR ACADEMIC, NON-PROFIT RESEARCH      !
c! ONLY.                                                                !
c!                                                                      !
c!----------------------------------------------------------------------!
c!                          DISCLAIMER                                  !
c!                                                                      !
c! Although great care has been taken in preparing the WatSolv          !
c! subroutines and documentation, the author(s) cannot be held          !
c! responsible for any errors or omissions. As such, this code is       !
c! offered `as is'. The author(s) makes no warranty of any kind,        !
c! express or implied. The author(s) shall not be liable for any        !
c! damages arising from a failure of this program to operate in the     !
c! manner desired by the user. The author(s) shall not be liable for    !
c! any damage to data or property which may be caused directly or       !
c! indirectly by use of this program. In no event will the author(s)    !
c! be liable for any damages, including, but not limited to, lost       !
c! profits, lost savings or other incidental or consequential damages   !
c! arising out of the use, or inability to use, this program. Use,      !
c! attempted use, and/or istallation of this program shall constitute   !
c! implied acceptance of the above conditions. Authorized users         !
c! encountering problems with the code, or requiring specific           !
c! implementations not supported by this version, are encouraged to     !
c! contact the author(s) for possible assistance.                       !
c!                                                                      !
c!======================================================================!
c! Last Modified: March 3, 1996 (JVK)

      SUBROUTINE WS209 (lfile,nn,nitmax,numit,idetail,ia,ja,iaf,iafd,    &
     &          jaf,lorder,a,af,x,b,res,work,restol,deltol,nja,njaf,     &
     &          over_flow,rnorm,rmupdate)

c! Driver routine for iterative solver.
c!-----------------------------------------------------------------------

      IMPLICIT NONE

c! Passed variables

      INTEGER   lfile         ! output unit number
      INTEGER   nn            ! number of unknowns
      INTEGER   nja           ! number of global connections
      INTEGER   njaf          ! num connections in af()
      INTEGER   nitmax        ! max num of iterations
      INTEGER   numit         ! number of solver iterations     *
      INTEGER   idetail       ! solver information level
      INTEGER   ia(nn + 1)    ! row start pointers for a()
      INTEGER   ja(nja)       ! global connection list for a()
      INTEGER   iaf(nn)       ! row start pointers for af()
      INTEGER   iafd(nn)      ! diagonal pointer for af()
      INTEGER   jaf(njaf)     ! global connection list for af()
      INTEGER   lorder(nn)    ! forward ordering of unknowns

      REAL*8    a(nja)        ! coefficient matrix
      REAL*8    af(njaf)      ! incomplete LU
      REAL*8    x(nn)         ! approximate solution            *
      REAL*8    b(nn)         ! forcing vector
      REAL*8    res(nn)       ! residual                        *
      REAL*8    work(8*nn)    ! cgstab workspace (length=8*nn)  *
      REAL*8    restol        ! solver residual tolerance
      REAL*8    deltol        ! solver update tolerance (i.e. 0.1*picard)
      REAL*8    rnorm         ! residual 2-norm
      REAL*8    rmupdate      ! maximum solution update

c!                                         modified on return = *
c!-----------------------------------------------------------------------
c! Local variables


      LOGICAL   converged     ! convergence flag

culi
      LOGICAL   over_flow     ! overflow flag

      INTEGER   i             ! loop index
      INTEGER   lr0           ! pointers
      INTEGER   lpv
      INTEGER   lvb
      INTEGER   lavb
      INTEGER   ltemp
      INTEGER   lsv
      INTEGER   lzv
      INTEGER   ltv

      REAL*8    r0
culi
      real*8    huge          ! upper bound - determination of overflow
      INTEGER   i0,i1
      PARAMETER (i0 = 0,i1 = 1,r0 = 0.0D0, huge = 1.d300)

      EXTERNAL  MatrixVectorMultiply,BiCGSTAB
c!-----------------------------------------------------------------------
c!    print *,'enter watsolv'

culi
      over_flow = .false.

      IF (idetail.GT.i1) THEN
        WRITE (lfile,9)
        WRITE (lfile,10)
        WRITE (lfile,11)
      ENDIF

      numit = i0

c!------
c! Initial solution guess not equal to zero:
c!
c!    CALL MatrixVectorMultiply (nn,nja,x,res,a,ia,ja)
c!    rnorm = r0
c!    DO i = i1,nn
c!      res(i) = b(i) - res(i)
c!      rnorm = rnorm + res(i)**2
c!    ENDDO
c!------
c! Initial solution guess equal to zero:

      rnorm = r0
      DO i = i1,nn
        res(i) = b(i)
        rnorm = rnorm + res(i)**2
      ENDDO
c!------
      rnorm = SQRT(rnorm)

culi
      if (rnorm.gt.huge) then            !overflow occurred
        over_flow = .true.               !reduce timestep or stop execution
        return                           !in calling routine

cjoel IF (rnorm.GT.r0) THEN
culi
      elseif (rnorm.gt.r0) then          !solve system of equations
c!    IF (rnorm.GT.restol) THEN

c! Solve system of equations              start of:

        lpv  = i1                        ! pvec(nn)
        lvb  = lpv  + nn                 ! vbar(nn)
        lavb = lvb  + nn                 ! avbar(nn)
        lsv  = lavb + nn                 ! svec(nn)
        lzv  = lsv  + nn                 ! zvec(nn)
        ltv  = lzv  + nn                 ! tvec(nn)
        lr0  = ltv  + nn                 ! res0(nn)

c! could use row() for temporay vector here - check out later

        ltemp = lr0 + nn                 ! temp(nn)

        converged = .FALSE.

        CALL BiCGSTAB (lfile,nn,idetail,nitmax,numit,ia,ja,iaf,iafd,     &
     &          jaf,converged,a,af,x,res,restol,rnorm,rmupdate,deltol,   &
     &          work(lr0),work(lpv),work(lvb),work(lavb),work(lsv),      &
     &          work(lzv),work(ltv),work(ltemp),lorder,nja,njaf)

cintellinux  *** relapce isnan by huge = 1.0d300 ***
        if (isnan(rnorm)) then           !rnorm = NaN
          over_flow = .true.             !reduce timestep or stop execution
          return                         !in calling routine
        end if
     
      ELSEIF (idetail.GT.i1) THEN        !rnorm.le.r0 -> converged
                   
        WRITE (lfile,14)
        converged = .TRUE.
      ENDIF

      IF (idetail.GT.i1) THEN
        WRITE (lfile,*)
      ENDIF

 9    FORMAT (/1x,'Solver Iteration Convergence Summary:')
 10   FORMAT (1x,'solver       maximum      maximum   ')
 11   FORMAT (1x,'iteration    update       residual  ')
 14   FORMAT (/1x,'Maximum error from previous solution is less',        &
     &        /1x,'than the specified convergence tolerance.')
      RETURN
      END

c!=======================================================================
c! Last Modified: March 3, 1996 (JVK)

      SUBROUTINE BiCGSTAB (lfile,nn,idetail,nitmax,numit,ia,ja,iaf,iafd, &
     &          jaf,converged,a,af,x,res,restol,rnorm,rmupdate,deltol,   &
     &          res0,pvec,vbar,avbar,svec,zvec,tvec,temp,lorder,nja,     &
     &          njaf)

c! Bi-Conjugate-Gradient Stabilized Accelerated Iterative Solver
c!-----------------------------------------------------------------------

      IMPLICIT NONE

c! Passed variables

      INTEGER   lfile         ! output file unit number
      INTEGER   nn            ! number of cells/nodes
      INTEGER   nja           ! number of global connections
      INTEGER   njaf          ! num connections in af()
      INTEGER   idetail       ! output detail level
      INTEGER   nitmax        ! maximum iterations
      INTEGER   numit         ! number of iterations
      INTEGER   ia(nn + 1)    ! row start pointers for a()
      INTEGER   ja(nja)       ! global connection list for a()
      INTEGER   iaf(nn + 1)   ! row start pointers for af()
      INTEGER   iafd(nn)      ! diagonal pointer for af()
      INTEGER   jaf(njaf)     ! global connection list for af()
      INTEGER   lorder(nn)    ! ordering vector for unknowns

      REAL*8    a(nja)        ! coefficient matrix
      REAL*8    af(njaf)      ! factored coefficient matrix
      REAL*8    x(nn)         ! solution vector
      REAL*8    res(nn)       ! residual vector
      REAL*8    restol        ! solver residual tolerance
      REAL*8    deltol        ! solver update tolerance
      REAL*8    rnorm         ! maximum 2-norm
      REAL*8    rmupdate      ! maximum solution update
      REAL*8    res0(nn)      ! initial residual vector
      REAL*8    pvec(nn)      ! intermediate vectors
      REAL*8    vbar(nn)
      REAL*8    avbar(nn)
      REAL*8    svec(nn)
      REAL*8    zvec(nn)
      REAL*8    tvec(nn)
      REAL*8    temp(nn)      ! temporary work vector (ordering)

      LOGICAL   converged     ! convergence flag
c!-----------------------------------------------------------------------
c! Local variables

      REAL*8    alpha         ! solution change coefficient
      REAL*8    rholst        ! last rho value
      REAL*8    omega         ! last omega value
      REAL*8    beta
      REAL*8    rho           ! current beta value
      REAL*8    update        ! solution update
      REAL*8    rescurr       ! current residual
      REAL*8    tiny          ! smallest possible magnitude
      REAL*8    DotProduct

      INTEGER   i

      INTEGER   i1
      REAL*8    r0,r1
      PARAMETER (i1 = 1,r0 = 0.0D0,r1 = 1.0D0,tiny = 1.0D-300)

      INTRINSIC ABS,SIGN,MAX
      EXTERNAL  DotProduct,LowerUpperSolve,MatrixVectorMultiply
c!-----------------------------------------------------------------------
c!    print *,'enter cgstab'

c! Initialize variables and vectors

      alpha  = r1
      rholst = r1
      omega  = r1
      DO i = i1,nn
        pvec(i)  = r0
        avbar(i) = r0
        res0(i)  = res(i)
      ENDDO

c!----
c! BEGIN ITERATION LOOP (up to nitmax)

      DO WHILE (.NOT.converged.AND.numit.LT.nitmax)

        numit = numit + i1

c!------------------------------
c! rho    = (res0, res)
c! beta   = (rho/rholst)*(alpha/omega)
c! rholst = rho
c! pvec   = res + beta*(pvec - omega*Avbar)

        rho  = DotProduct(nn,res0,res)
        beta = rho/(rholst + SIGN(tiny,rholst))
        beta = beta*(alpha/(omega + SIGN(tiny,omega)))
        rholst = rho
        DO i = i1,nn
          pvec(i) = res(i) + beta*(pvec(i) - omega*avbar(i))
        ENDDO

c!--------
c! solve (LU) vbar = pvec
c! Avbar = A*vbar
c! alpha = rholst/(res0,Avbar)

        CALL LowerUpperSolve (nn,njaf,vbar,pvec,af,temp,iaf,iafd,jaf,    &
     &          lorder)
        CALL MatrixVectorMultiply (nn,nja,vbar,avbar,a,ia,ja)

        alpha = DotProduct(nn,res0,avbar)
        alpha = rho/(alpha + SIGN(tiny,alpha))

c!--------
c! svec  = res - alpha*Avbar
c! solve  (LU) zvec = svec
c! Azvec = A*zvec = tvec
c! omega = (A zvec,svec)/(A zvec,A zvec)

        DO i = i1,nn
          svec(i) = res(i) - alpha*avbar(i)
        ENDDO

        CALL LowerUpperSolve (nn,njaf,zvec,svec,af,temp,iaf,iafd,        &
     &        jaf,lorder)
        CALL MatrixVectorMultiply (nn,nja,zvec,tvec,a,ia,ja)

        omega = DotProduct(nn,tvec,tvec)
        omega = DotProduct(nn,tvec,svec)/(omega + SIGN(tiny,omega))

c!--------
c! x = x + alpha*vbar + omega*zvec
c! res = svec - omega*tvec, where svec = res - alpha*Avbar

        rnorm    = r0         ! residual 2-norm
        rmupdate = r0         ! maximum update

        DO i = i1,nn          ! update approximate solution

          update  = alpha*vbar(i) + omega*zvec(i)
          x(i)    = x(i) + update
          rescurr = svec(i) - omega*tvec(i)
          res(i)  = rescurr

c! Increment residual value.

          rnorm = rnorm + rescurr**2

c! Save maximum update value.

          rmupdate = MAX(ABS(update),rmupdate)

        ENDDO

c! Complete residual calculation.

culi 
        if (.not.isnan(rnorm)) then
	    rnorm = SQRT(rnorm)
	  else
	    return
        end if

cjoel   rnorm = SQRT(rnorm)

c! Stop iterating if specified residual reduction or solution update
c!  tolerances are met.

        converged = (.NOT.(rnorm.GT.restol).or.
     &               .NOT.(rmupdate.GT.deltol))


        IF (idetail.GT.i1) THEN
          WRITE (lfile,2) numit,rmupdate,rnorm
        ENDIF
      ENDDO                           ! iteration loop

 2    FORMAT (1x,i5,4x,2(2x,1pd11.4))
      RETURN
      END

c!======================================================================!
c!                                                                      !
c!        Matrix-Vector Manipulation Routines (ia,ja structure)         !
c!                                                                      !
c!======================================================================!
c! Last Modified: March 3, 1996 (JVK)

      SUBROUTINE LowerUpperSolve (nn,njaf,x,b,af,temp,iaf,iafd,jaf,      &
     &          lorder)

c! Lower triangular matrix inversion by forward substitution and upper
c!  triangular matrix inversion by backward substitution.
c! Lower and upper triangular matrices are in af, and right-hand-side
c!  vector is in b at start. Solution vector is in x upon exit.
c! Diagonal entries are stored as their inverses.
c!-----------------------------------------------------------------------

      IMPLICIT NONE

c! Passed variables

      INTEGER   nn            ! number of cells/nodes
      INTEGER   njaf          ! num connections in af()
      INTEGER   iaf(nn + 1)   ! row start pointers for af()
      INTEGER   iafd(nn)      ! diagonal pointer for af()
      INTEGER   jaf(njaf)     ! global connection list for af()
      INTEGER   lorder(nn)    ! ordering vector for unknowns

      REAL*8    x(nn)         ! solution vector
      REAL*8    b(nn)         ! forcing vector
      REAL*8    af(njaf)      ! incomplete factorization of a()
      REAL*8    temp(nn)      ! temporary work vector (ordering)
c!-----------------------------------------------------------------------
c! Local variables

      REAL*8    sum
      INTEGER   i,j,inode,istart,istop,idiag,li

      INTEGER   i1
      PARAMETER (i1 = 1)
c!-----------------------------------------------------------------------
c!    print *,'enter lower-upper solve'

c! un-order the forcing vector

      DO i = i1,nn
        li = lorder(i)
        temp(i) = b(li)
      ENDDO

c! Forward solve:  Lz = b (L has unit diagonal)

      DO i = i1,nn
        istart = iaf(i)
        istop  = iafd(i) - i1
        sum    = temp(i)
        DO j = istart,istop
          inode = jaf(j)                      ! connection
          sum = sum - af(j)*temp(inode)
        ENDDO
        temp(i) = sum
      ENDDO

c! Backward solve: Ux = z (U does not have unit diag)

      DO i = nn,i1,-i1
        idiag = iafd(i)
        istop = iaf(i + i1) - i1
        sum   = temp(i)
        DO j = (idiag + i1),istop
          inode = jaf(j)                       ! connection
          sum   = sum - af(j)*temp(inode)
        ENDDO
        temp(i) = sum*af(idiag)                ! diagonal entry
      ENDDO

c! re-order the solution vector

      DO i = i1,nn
        li = lorder(i)
        x(li) = temp(i)
      ENDDO

      RETURN
      END

c!=======================================================================
c! Last Modified: February 2, 1996 (JVK)

      SUBROUTINE MatrixVectorMultiply (nn,nja,x,b,a,ia,ja)

c! Multiply matrix a by vector x to obtain b (Ax = b)
c! Assumes diagonal is in first postion and has been scaled to unity.
c!-----------------------------------------------------------------------

      IMPLICIT NONE

c! Passed variables

      INTEGER   nn            ! number of cells/nodes
      INTEGER   nja           ! number of global connections
      INTEGER   ia(nn + 1)    ! row start pointers for a()
      INTEGER   ja(nja)       ! global connection list for a()

      REAL*8    x(nn)         ! solution vector
      REAL*8    b(nn)         ! forcing vector
      REAL*8    a(nja)        ! coefficient matrix
c!-----------------------------------------------------------------------
c! Local variables

      REAL*8    sum
      INTEGER   i,j,inode,istart,istop

      INTEGER   i1
      PARAMETER (i1 = 1)
c!-----------------------------------------------------------------------
c!    print *,'enter matrix-vector multiply'

      DO i = i1,nn
        istart = ia(i)              ! start of row/location of diagonal
        sum    = x(ja(istart))      ! multiplied by unity = x()

        istart = istart + i1        ! skip diagonal (unity)
        istop  = ia(i + i1) - i1    ! end of row

        DO j = istart,istop
          inode = ja(j)
          sum = sum + a(j)*x(inode)
        ENDDO
        b(i) = sum
      ENDDO

      RETURN
      END

c!=======================================================================
c! Last Modified: March 3, 1996 (JVK)

      REAL*8 FUNCTION DotProduct(n,dx,dy)

c! Form the dot product of two vectors.
c!-----------------------------------------------------------------------

      IMPLICIT NONE

c! Passed variables

      INTEGER      n
      REAL*8       dx(n),dy(n)
c!-----------------------------------------------------------------------
c! Local variables

      REAL*8    temp
      INTEGER   i

      REAL*8    r0
      INTEGER   i1
      PARAMETER (i1 = 1,r0 = 0.0D0)
c!-----------------------------------------------------------------------
c!    print *,'enter dot product'

      temp = r0
      DO i = i1,n
        temp = temp + dx(i)*dy(i)
      ENDDO

      DotProduct = temp

      RETURN
      END

c!=======================================================================
c! Last Modified: March 3, 1996 (JVK)

      INTEGER FUNCTION idamax1(n,dx)

c! Find the index of element having max. absolute value.
c!-----------------------------------------------------------------------
c! Passed variables

      INTEGER      n
      REAL*8       dx(n)
c!-----------------------------------------------------------------------
c! Local variables

      REAL*8    big
      INTEGER   i

      INTEGER   i1,i2
      PARAMETER (i1 = 1,i2 = 2)

      INTRINSIC ABS
c!-----------------------------------------------------------------------
c!    print *,'enter idamax'

      idamax1 = i1
      big = ABS(dx(i1))
      DO i = i2,n
        IF (ABS(dx(i)).GT.big) THEN
          idamax1 = i
          big = ABS(dx(i))
        ENDIF
      ENDDO
      RETURN
      END

c!======================================================================!
c!                                                                      !
c!                  Preconditioning Routines                            !
c!                                                                      !
c!======================================================================!
c! Last Modified: March 3, 1996 (JVK)

      SUBROUTINE IncompleteFactorization (nn,nja,njaf,b,a,af,row,ia,     &
     &          ja,iaf,iafd,jaf,list,lorder,invord)

c! Scale rows by 1/diagonal entry - transforms [a] to unit diagonal.
c! Assumes diagonal is in first position of ja() for node i.
c! Incomplete lower-upper decomposition of matrix a into af.
c! This is general code, organized for arbitrary fill level.
c! Diagonal dominance is assumed: no pivoting performed.
c!-----------------------------------------------------------------------

      IMPLICIT NONE

c! Passed variables

      INTEGER   nn            ! number of cells/nodes
      INTEGER   nja           ! number of global connections
      INTEGER   njaf          ! num connections in af()
      INTEGER   ia(nn + 1)    ! row start pointers for a()
      INTEGER   ja(nja)       ! global connection list for a()
      INTEGER   iaf(nn + 1)   ! row start pointers for af()
      INTEGER   iafd(nn)      ! diagonal pointer for af()
      INTEGER   jaf(njaf)     ! global connection list for af()
      INTEGER   list(nn)      ! pointer vector (temporary)
      INTEGER   lorder(nn)    ! forward ordering of unknowns
      INTEGER   invord(nn)    ! backward ordering of unknowns

      REAL*8    b(nn)         ! residual/forcing vec (fv() in wts1)
      REAL*8    a(nja)        ! coefficient matrix
      REAL*8    af(njaf)      ! incomplete ILU
      REAL*8    row(nn)       ! holder vector (temporary)
c!-----------------------------------------------------------------------
c! Local variables

      REAL*8    rmult

      INTEGER   irow
      INTEGER   i,j,k
      INTEGER   induce
      INTEGER   ifill
      INTEGER   istart
      INTEGER   iend
      INTEGER   jend
      INTEGER   kstart
      INTEGER   kend
      INTEGER   jdiag
      INTEGER   iold

      REAL*8    r0,r1
      INTEGER   i1,i0
      PARAMETER (i0 = 0,i1 = 1,r0 = 0.0D0,r1 = 1.0D0)
c!-----------------------------------------------------------------------
c!    print *,'enter incomplete factorization'

      DO irow = i1,nn

c! Scale equation rows by 1/diagonal entry
c!  - transforms [a] to unit diagonal.

        istart = ia(irow)            ! start of row connections

        rmult = r1/a(istart)         ! row multiplier

        a(istart) = r1               ! set diagonal to unity

        istart = istart + i1         ! skip diagonal (ordered first)
        iend   = ia(irow + i1) - i1  ! end of row connections

        DO j = istart,iend           ! scale LHS of equation
          a(j) = a(j)*rmult
        ENDDO

        b(irow) = b(irow)*rmult      ! scale RHS of equation

c! Initialize pointer and holder vectors

        list(irow) = i0
        row(irow)  = r0

      ENDDO

c! Factor

      DO irow = i1,nn

        iold = lorder(irow)

        istart = ia(iold)
        iend   = ia(iold + i1) - i1

        DO i = istart, iend                  ! row entries in a()
          j = invord(ja(i))
          row(j) = a(i)                      ! scatter row entries
        ENDDO

        istart = iaf(irow)
        iend   = iaf(irow + i1) - i1

        DO i = istart, iend                  ! load markers
          j = jaf(i)
          list(j) = irow                     ! mark fill locations
        ENDDO

        jend = iafd(irow) - i1               ! diag - 1

        DO j = istart, jend                  ! row entry loop

          induce = jaf(j)                    ! inducing row
          jdiag  = iafd(induce)              ! diagonal pointer
          rmult  = row(induce)*af(jdiag)     ! inducing term
          row(induce) = rmult                ! lower fill entry

          kstart = jdiag + i1                ! row start
          kend   = iaf(induce + i1) - i1     ! row end

          DO k = kstart,kend
            ifill = jaf(k)                   ! fill column location

c! include if acceptable

            IF (.NOT.(list(ifill).LT.irow))                              &
     &        row(ifill) = row(ifill) - rmult*af(k)
          ENDDO

        ENDDO

        DO i = istart,iend                   ! row entries in af()
          j      = jaf(i)                    ! location in row
          af(i)  = row(j)                    ! gather row entries
          row(j) = r0                        ! zero row entry
        ENDDO

        i = iafd(irow)                       ! diag location

        af(i) = r1/af(i)                     ! invert diagonal

      ENDDO

      RETURN
      END

c!=======================================================================
c! Last Modified: March 3, 1996 (JVK)

      SUBROUTINE SymbolicFactorization (lfile,nn,nja,njaf,mnjaf,level,   &
     &          list,ia,ja,row,levptr,iaf,iafd,jaf,lorder,invord)

c! Incomplete symbolic lower/upper factorization - brute force factor.
c! Assumes ia(i) points to diagonal.
c!-----------------------------------------------------------------------

      IMPLICIT NONE

c! Passed variables

      INTEGER   lfile         ! output file unit number
      INTEGER   nn            ! number of cells or nodes
      INTEGER   nja           ! number of connections
      INTEGER   njaf          ! number of factored connections
      INTEGER   mnjaf         ! max. number of factored connections
      INTEGER   level         ! incomplete factorization level
      INTEGER   list(nn)      ! temporary pointer linked list
      INTEGER   ia(nn + 1)    ! row start pointers for a()
      INTEGER   ja(nja)       ! global connection list for a()
      INTEGER   row(nn)       ! holder vector (temporary)
      INTEGER   levptr(mnjaf) ! global level storage
      INTEGER   iaf(nn + 1)   ! row start pointers for af()
      INTEGER   iafd(nn)      ! diagonal pointer for af()
      INTEGER   jaf(mnjaf)    ! global connection list for af()
      INTEGER   lorder(nn)    ! forward ordering of unknowns
      INTEGER   invord(nn)    ! backward ordering of unknowns
c!-----------------------------------------------------------------------
c! Local variables

      integer   mmmm

      INTEGER   levtemp
      INTEGER   ir
      INTEGER   i,j,ii
      INTEGER   first
      INTEGER   next
      INTEGER   nnp1
      INTEGER   oldlst
      INTEGER   nxtlst
      INTEGER   irow
      INTEGER   jcol
      INTEGER   njafp1
      INTEGER   iend
      INTEGER   istart
      INTEGER   istop
      INTEGER   iold
      INTEGER   num

      INTEGER   i0,i1,maxlev
      PARAMETER (i0 = 0,i1 = 1, maxlev = 9999)

      INTRINSIC MIN
      EXTERNAL  Check,BubbleSort
c!-----------------------------------------------------------------------
c!    print *,'enter symbolic factorization'
c!provi------------------------------------------------------------------
c!provi mmmm variable is initialized 
c!provi------------------------------------------------------------------
      mmmm=0
c!provi------------------------------------------------------------------
      nnp1 = nn + i1                        ! end of list marker

      DO i = i1,nn
        row(i)  = maxlev
        list(i)  = i0
        iafd(i)  = i0                       ! diagonal pointern
        iaf(i)   = i0
      ENDDO

      iaf(nnp1) = i0
      iaf(i1)   = i1                        ! start of first row

      njafp1 = i1
      mmmm = max(njafp1,mmmm)
      CALL Check (njafp1,mnjaf,lfile)       ! check dimensions

      DO ir = i1,nn                         ! loop through rows

c! Start scatter

        iend = njaf                         ! temp list pointer
        iold = lorder(ir)

        istart = ia(iold)
        istop  = ia(iold + i1) - i1

        num = i0
        DO ii = istart,istop                   ! load row ir of L/U
          iend = iend + i1                     ! pointer into jaf()
          mmmm = max(njaf+iend,mmmm)
          CALL Check (njaf + iend,mnjaf,lfile) ! check dimensions
          j = invord(ja(ii))
          jaf(iend) = j
          row(j)    = i0                  ! initial level is zero
          num = num + i1
        ENDDO

c! sort entries

        IF (num.GT.i1) CALL BubbleSort (jaf(njafp1),num)

        first = jaf(njafp1)                 ! build linked list

        DO ii = njafp1,iend - i1
          list(jaf(ii)) = jaf(ii + i1)
        ENDDO

        list(jaf(iend)) = nnp1              ! end of list flag (nn + 1)

c! End scatter, Start merge

        next = first                        ! first entry in linked list

        DO WHILE (next.LT.ir)

          oldlst = next                     ! save current
          nxtlst = list(next)               ! next column
          irow   = next                     ! current col/row

          istart = iafd(irow) + i1          ! diag + 1
          istop  = iaf(irow + i1) - i1      ! end of connection list

          DO ii = istart,istop              ! scan row in U

            jcol = jaf(ii)                  ! new entry

            DO WHILE (jcol.GT.nxtlst)       ! scan linked list to find
              oldlst = nxtlst               !  correct position
              nxtlst = list(oldlst)         !  of current entry
            ENDDO

            IF (jcol.LT.nxtlst) THEN        ! entry doesn't exist

              levtemp = levptr(ii) + row(next) + i1 ! determine fill
              levtemp = MIN(row(jcol),levtemp)      !  level

              IF (.NOT.(levtemp.GT.level)) THEN ! entry <= max level
                list(oldlst) = jcol         ! add entry to list()
                list(jcol)   = nxtlst       !  if new level smaller
                oldlst       = jcol         !  than current level
                row(oldlst) = levtemp
              ENDIF

            ELSE                            ! entry already exists

              oldlst  = nxtlst
              nxtlst  = list(oldlst)

            ENDIF
          ENDDO

          next = list(next)                 ! next column in linked list

        ENDDO

c! End merge, Start gather

        next  = first

        DO WHILE (next.LT.nnp1)             ! linked list loop (gather)
          njaf         = njaf + i1
          mmmm = max(njaf,mmmm)
          CALL Check (njaf,mnjaf,lfile)     ! check dimensions
          jaf(njaf)    = next               ! column
          levptr(njaf) = row(next)          ! save level
          row(next)   = maxlev              ! reset level
          IF (next.EQ.ir) iafd(ir) = njaf   ! diagonal entry pointer
          next = list(next)                 ! next entry in list
        ENDDO
        njafp1 = njaf + i1
        mmmm = max(njafp1,mmmm)
        CALL Check (njafp1,mnjaf,lfile)     ! check dimensions
        iaf(ir + i1) = njafp1               ! start of next row

        IF (.NOT.(iafd(ir).GT.i0)) THEN     ! verify diagonal
          WRITE (lfile,*)' no diag in L/U'
          STOP
        ENDIF

      ENDDO

      write(lfile,'(a,i10)')
     &     'memory requirement during factorization:  ',mmmm

      RETURN
      END

c!======================================================================!
c!                                                                      !
c!                       Ordering Subroutines                           !
c!                                                                      !
c!           Generate:   lorder(new_order) = old_order                  !
c!                       invord(old_order) = new_order                  !
c!                                                                      !
c!======================================================================!
c! Last Modified: March 3, 1996 (JVK)

      SUBROUTINE NaturalOrdering (nn,lorder,invord)

c! Natural ordering of unknowns.
c!-----------------------------------------------------------------------

      IMPLICIT NONE

c! Passed variables

      INTEGER   nn
      INTEGER   lorder(nn)    ! forward ordering of unknowns
      INTEGER   invord(nn)    ! backward ordering of unknowns
c!-----------------------------------------------------------------------
c! Local variables

      INTEGER   i

      INTEGER   i1
      PARAMETER (i1 = 1)
c!-----------------------------------------------------------------------
c!    print *,'enter natural ordering'

      DO i = i1,nn
        lorder(i) = i
        invord(i) = i
      ENDDO

      RETURN
      END

c!=======================================================================
c! Last Modified: March 3, 1996 (JVK)

      SUBROUTINE RCMOrdering (nn,nja,ia,ja,lorder,invord,mask,xls)

c! Finds the reverse Cuthill-McKee ordering for a general graph.
c! For each connected component in the graph, RCMOrdering
c! obtains the ordering by calling the subroutine ReverseCuthillMcKee.
c!
c!           (Adapted from the Yale Sparse Matrix Package)
c!-----------------------------------------------------------------------

      IMPLICIT NONE

c! Passed variables

      INTEGER   nn            ! number of equations
      INTEGER   nja           ! number of global connections
      INTEGER   ia(*)!nn + 1)    ! row start pointers for a()
      INTEGER   ja(*)!nja)       ! global connection list for a()
      INTEGER   lorder(*)!nn)    ! array containing rcm ordering
      INTEGER   invord(*)!nn)    ! backward ordering of unknowns
      INTEGER   xls(*)!nn)       ! index vector for level structure

      LOGICAL   mask(*)!nn)      ! section subgraph (.TRUE. = active)
c!-----------------------------------------------------------------------
c! Local variables

      INTEGER   ccsize
      INTEGER   i
      INTEGER   nlvl
      INTEGER   num
      INTEGER   rnode

      INTEGER   i0,i1
      PARAMETER (i0 = 0,i1 = 1)

      EXTERNAL  FindPeripheralNode,ReverseCuthillMcKee
c!-----------------------------------------------------------------------
c!    print *,'enter rcm ordering'

      DO i = i1,nn
        mask(i)   = .TRUE.
        lorder(i) = i0
        invord(i) = i0
      ENDDO

      num = i1
      i   = i0

      DO WHILE (.NOT.(num.GT.nn))

        i = i + i1

c! for each masked connected component ...

        IF (mask(i)) THEN

          rnode = i

c! First find a pseudo-peripheral node rnode. Note that the level
c! structure found by FindPeripheralNode is stored starting at
c! lorder(num). Then ReverseCuthillMcKee is called to order the
c! component using rnode as the starting node.

          CALL FindPeripheralNode (nn,nja,rnode,ia,ja,mask,nlvl,xls,     &
     &      lorder(num))

          CALL ReverseCuthillMcKee (nn,nja,rnode,ia,ja,mask,lorder(num), &
     &      ccsize,xls)

          num = num + ccsize
        ENDIF
      ENDDO

      DO i = i1,nn
        invord(lorder(i)) = i                     ! inverse ordering
      ENDDO

      RETURN
      END

c!=======================================================================
c! Last Modified: March 3, 1996 (JVK)

      SUBROUTINE FindPeripheralNode (nn,nja,rnode,ia,ja,mask,nlvl,xls,   &
     &          ls)

c! Implements a modified version of the scheme by gibbs, poole, and
c! stockmeyer to find pseudoperipheral nodes.  It determines such a
c! node for the section subgraph specified by mask and rnode.
c!-----------------------------------------------------------------------

      IMPLICIT NONE

c! Passed variables

      INTEGER   nn            ! number of equations
      INTEGER   nja           ! number of global connections
      INTEGER   rnode         ! node at which level structure is rooted
      INTEGER   ia(*)!nn + 1)    ! row start pointers for a()
      INTEGER   ja(*)!nja)       ! global connection list for a()
      INTEGER   nlvl          ! num levels in level structure
      INTEGER   xls(*)!nn)       ! rooted level structure
      INTEGER   ls(*)         ! rooted level structure

      LOGICAL   mask(*)!nn)      ! section subgraph (.TRUE. = active)
c!-----------------------------------------------------------------------
c! Local variables

      INTEGER   ccsize
      INTEGER   j
      INTEGER   jstrt
      INTEGER   k
      INTEGER   kstop
      INTEGER   kstrt
      INTEGER   mindeg
      INTEGER   nabor
      INTEGER   ndeg
      INTEGER   node
      INTEGER   nunlvl

      LOGICAL   done

      INTEGER   i0,i1
      PARAMETER (i0 = 0,i1 = 1)

      EXTERNAL  RootedLevelStructure
c!-----------------------------------------------------------------------
c!    print *,'enter find peripheral node'

c! determine the level structure rooted at root.

      CALL RootedLevelStructure (nn,nja,rnode,ia,ja,mask,nlvl,xls,ls)

      ccsize = xls(nlvl + i1) - i1

c! pick a node with minimum degree from the last level.

      done = (nlvl.EQ.i1.or.nlvl.EQ.ccsize)

      DO WHILE (.NOT.done)

        jstrt  = xls(nlvl)
        mindeg = ccsize
        rnode   = ls(jstrt)

        IF (ccsize.ne.jstrt) THEN
          DO j = jstrt,ccsize
            node = ls(j)
            ndeg = i0
            kstrt = ia(node)
            kstop = ia(node + i1) - i1
            DO k = kstrt, kstop
              nabor = ja(k)
              IF (mask(nabor)) ndeg = ndeg + i1
            ENDDO
            IF (ndeg.LT.mindeg) THEN
              rnode   = node
              mindeg = ndeg
            ENDIF
          ENDDO
        ENDIF

c! and generate its rooted level structure.

        CALL RootedLevelStructure (nn,nja,rnode,ia,ja,mask,nunlvl,xls,   &
     &         ls)

        IF (.NOT.(nunlvl.GT.nlvl)) THEN
          done = .TRUE.
        ELSE
          nlvl = nunlvl
          done = .NOT.(nlvl.LT.ccsize)
        ENDIF
      ENDDO

      RETURN
      END

c!=======================================================================
c! Last Modified: March 3, 1996 (JVK)

      SUBROUTINE RootedLevelStructure (nn,nja,rnode,ia,ja,mask,nlvl,xls, &
     &           ls)

c! Generate the level structure rooted at the input node called root.
c! Only those nodes for which mask is .TRUE. will be considered.
c!-----------------------------------------------------------------------

      IMPLICIT NONE

c! Passed variables

      INTEGER   nn            ! number of equations
      INTEGER   nja           ! number of global connections
      INTEGER   rnode         ! node at which level structure is rooted
      INTEGER   ia(*)!nn + 1)    ! row start pointers for a()
      INTEGER   ja(*)!nja)       ! global connection list for a()
      INTEGER   nlvl          ! num levels in level structure
      INTEGER   xls(*)!nn)       ! rooted level structure
      INTEGER   ls(*)         ! rooted level structure

      LOGICAL   mask(*)!nn)      ! section subgraph (.TRUE. = active)
c!-----------------------------------------------------------------------
c! Local variables

      INTEGER   i,j
      INTEGER   jstop
      INTEGER   jstrt
      INTEGER   lbegin
      INTEGER   ccsize
      INTEGER   lvlend
      INTEGER   lvsize
      INTEGER   nbr
      INTEGER   node

      LOGICAL   done

      INTEGER   i0,i1
      PARAMETER (i0 = 0,i1 = 1)
c!-----------------------------------------------------------------------
c!    print *,'enter rooted level structure'

c! initialization ...

      mask(rnode) = .FALSE.
      ls(1)      = rnode
      nlvl       = i0
      lvlend     = i0
      ccsize     = i1

c! lbegin is the pointer to the beginning of the current
c! level, and lvlend points to the END of this level.

      done = .FALSE.
      DO WHILE (.NOT.done)

        lbegin = lvlend + i1
        lvlend = ccsize
        nlvl   = nlvl + i1
        xls(nlvl) = lbegin

c! generate the next level by finding all the masked
c! neighbors of nodes in the current level.

         DO i = lbegin, lvlend
           node = ls(i)
           jstrt = ia(node)
           jstop = ia(node + i1) - i1
           IF (.NOT.(jstop.LT.jstrt)) THEN
             DO j = jstrt, jstop
               nbr = ja(j)
               IF (mask(nbr)) THEN
                 mask(nbr)  = .FALSE.
                 ccsize     = ccsize + i1
                 ls(ccsize) = nbr
               ENDIF
             ENDDO
           ENDIF
         ENDDO

c! compute the current level width.
c! if it is nonzero, generate the next level.

        lvsize = ccsize - lvlend
        done = .NOT.(lvsize.GT.i0)
      ENDDO

c! reset mask for the nodes in the level structure.

      xls(nlvl + i1) = lvlend + i1
      DO i = i1, ccsize
        node = ls(i)
        mask(node) = .TRUE.
      ENDDO

      RETURN
      END

c!=======================================================================
c! Last Modified: March 3, 1996 (JVK)

      SUBROUTINE ReverseCuthillMcKee (nn,nja,rnode,ia,ja,mask,lorder,    &
     &            ccsize,deg)

c! Number a connected component specified by mask and rnode, using the rcm
c!  algorithm. The numbering is to be started at the node rnode.
c!-----------------------------------------------------------------------

      IMPLICIT NONE

c! Passed variables

      INTEGER   nn            ! number of equations
      INTEGER   nja           ! number of global connections
      INTEGER   rnode         ! input node that specifies the component
      INTEGER   ia(nn + 1)    ! row start pointers for a()
      INTEGER   ja(nja)       ! global connection list for a()
      INTEGER   lorder(*)     ! rcm ordering
      INTEGER   ccsize        ! size of the connected component
      INTEGER   deg(nn)       ! degrees of the nodes in the component

      LOGICAL   mask(nn)      ! section subgraph (.TRUE. = active)
c!-----------------------------------------------------------------------
c! Local variables

      INTEGER   fnbr
      INTEGER   i,j
      INTEGER   jstop
      INTEGER   jstrt
      INTEGER   k,l
      INTEGER   lbegin
      INTEGER   lnbr
      INTEGER   iord
      INTEGER   lvlend
      INTEGER   nbr
      INTEGER   node

      LOGICAL   done_lev
      LOGICAL   done_k
      LOGICAL   done_deg

      INTEGER   i0,i1,i2
      PARAMETER (i0 = 0,i1 = 1,i2 = 2)

      EXTERNAL  Degree
c!-----------------------------------------------------------------------
c!    print *,'enter reverse cuthill mckee'

c! find the degrees of the nodes in the component specified by
c!  mask and rnode.

      CALL Degree (nn,nja,rnode,ia,ja,mask,deg,ccsize,lorder)

      mask(rnode) = .FALSE.
      IF (.NOT.(ccsize.GT.i1)) RETURN
      lvlend = i0
      lnbr   = i1

c! lbegin and lvlend point to the beginning and
c! the end of the current level respectively.

      done_lev = .FALSE.

      DO WHILE (.NOT.done_lev)

        lbegin = lvlend + i1
        lvlend = lnbr
        DO i = lbegin, lvlend

c! for each node in current level ...

           node = lorder(i)
           jstrt = ia(node)
           jstop = ia(node + i1) - i1

c! find the unnumbered neighbors of node. fnbr and lnbr point to the
c! first and last unnumbered neighbors respectively of the current
c! node in lorder.

           fnbr = lnbr + i1
           DO j = jstrt, jstop
             nbr = ja(j)
             IF (mask(nbr)) THEN
               mask(nbr)  = .FALSE.
               lnbr = lnbr + i1
               lorder(lnbr) = nbr
             ENDIF
           ENDDO

          IF (fnbr.LT.lnbr) THEN

c! sort the neighbors of node in increasing
c! order by degree. linear insertion is used.

             k = fnbr
             done_k = .FALSE.
             DO WHILE (.NOT.done_k)
               l = k
               k = k + i1
               nbr = lorder(k)
               done_deg = .FALSE.
               DO WHILE (l.GT.fnbr.AND..NOT.done_deg)
                 iord = lorder(l)
                 done_deg = .NOT.(deg(iord).GT.deg(nbr))
                 IF (.NOT.done_deg) THEN
                   lorder(l + i1) = iord
                   l = l - i1
                 ENDIF
               ENDDO
               lorder(l + i1) = nbr
               done_k = .NOT.(k.LT.lnbr)
             ENDDO
           ENDIF
        ENDDO
        done_lev = .NOT.(lnbr.GT.lvlend)
      ENDDO

c! we now have the cuthill mckee ordering. reverse it below ...

      k = ccsize/i2
      l = ccsize

      DO i = i1, k
        iord = lorder(l)
        lorder(l) = lorder(i)
        lorder(i) = iord
        l = l - i1
      ENDDO

      RETURN
      END

c!=======================================================================
c! Last Modified: March 3, 1996 (JVK)

      SUBROUTINE Degree (nn,nja,rnode,ia,ja,mask,deg,ccsize,ls)

c! Compute the degrees of the nodes in the connected component specified
c!  by mask and rnode. Nodes for which mask is .FALSE. are ignored.
c!-----------------------------------------------------------------------

      IMPLICIT NONE

c! Passed variables

      INTEGER   nn            ! number of equations
      INTEGER   nja           ! number of global connections
      INTEGER   rnode         ! input node that specifies the component
      INTEGER   ia(nn + 1)    ! row start pointers for a()
      INTEGER   ja(nja)       ! global connection list for a()
      INTEGER   deg(nn)       ! degrees of the nodes in the component
      INTEGER   ccsize        ! size of the component (mask and rnode)
      INTEGER   ls(*)         ! working vector

      LOGICAL   mask(nn)      ! section subgraph (.TRUE. = active)
c!-----------------------------------------------------------------------
c! Local variables

      INTEGER   i
      INTEGER   ideg
      INTEGER   j
      INTEGER   jstop
      INTEGER   jstrt
      INTEGER   lbegin
      INTEGER   lvlend
      INTEGER   lvsize
      INTEGER   nbr
      INTEGER   node

      LOGICAL   done

      INTEGER   i0,i1
      PARAMETER (i0 = 0,i1 = 1)
c!-----------------------------------------------------------------------
c!    print *,' enter degree'

c! initialization ...
c! the array ia is used as a temporary marker to
c! indicate which nodes have been considered so far.

      ls(i1) = rnode
      ia(rnode) = -ia(rnode)
      lvlend = i0
      ccsize = i1

c! lbegin is the pointer to the beginning of the current
c! level, and lvlend points to the END of this level.

      done = .FALSE.

      DO WHILE (.NOT.done)

        lbegin = lvlend + i1
        lvlend = ccsize

c! find the degrees of nodes in the current level,
c! and at the same time, generate the next level.

        DO i = lbegin, lvlend

          node  = ls(i)
          jstrt = -ia(node)
          jstop = iabs(ia(node + i1)) - i1
          ideg  = i0

          IF (.NOT.(jstop.LT.jstrt)) THEN
            DO j = jstrt, jstop
              nbr = ja(j)
              IF (mask(nbr)) THEN
                ideg = ideg + i1
                IF (.NOT.(ia(nbr).LT.i0)) THEN
                  ia(nbr) = -ia(nbr)
                  ccsize  = ccsize + i1
                  ls(ccsize) = nbr
                ENDIF
              ENDIF
            ENDDO
          ENDIF

          deg(node) = ideg

        ENDDO

c! compute the current level width.
c! if it is nonzero , generate another level.

        lvsize = ccsize - lvlend
        done = .NOT.(lvsize.GT.i0)
      ENDDO

c! reset ia to its correct sign and return.

      DO i = i1, ccsize
        node = ls(i)
        ia(node) = -ia(node)
      ENDDO

      RETURN
      END

c!======================================================================!
c!                                                                      !
c!                   Misc. Utility Routines                             !
c!                                                                      !
c!======================================================================!
c! Last Modified: March 3, 1996 (JVK)

      SUBROUTINE BubbleSort (list,n)

c! Sort entries in list into increasing numerical order.
c!-----------------------------------------------------------------------

      IMPLICIT NONE

c! Passed variables

      INTEGER   n             ! number of entries in list()
      INTEGER   list(n)       ! index list
c!-----------------------------------------------------------------------
c! Local variables

      INTEGER   i,itemp,ip1   ! loop/sort variables
      INTEGER   pairs         ! number of pairs to compare

      LOGICAL   done          ! no interchange on last pass

      INTEGER   i1            ! constant
      PARAMETER (i1 = 1)
c!-----------------------------------------------------------------------
c!    print *,'enter bubble sort'

      pairs = n - i1
      done  = .FALSE.

      DO WHILE (.NOT. done)             ! continue until no interchanges
        done = .TRUE.
        DO i = i1,pairs
          ip1 = i + i1
          IF (list(i).GT.list(ip1)) THEN    ! interchange
            itemp     = list(i)
            list(i)   = list(ip1)
            list(ip1) = itemp
            done      = .FALSE.
          ENDIF
        ENDDO
        pairs = pairs - i1
      ENDDO

      RETURN
      END

c!=======================================================================
c! Last Modified: March 3, 1996 (JVK)

      SUBROUTINE BinaryFind (nn,nja,ia,ja,jband,nj,ni)

c! Find band for node nj in node ni adjacency list.
c! If not adjacent, return 0 as the "band"
c! Assumes entries are sorted in increasing order (binary search) with
c! the exception of the diagonal, which is stored in the first position.
c! See comments for more information.
c!-----------------------------------------------------------------------

      IMPLICIT NONE

c! Passed variables

      INTEGER   nn            ! number of nodes
      INTEGER   nja           ! num connections in a()
      INTEGER   ia(nn + 1)    ! pointer for row indexes in ja
      INTEGER   ja(nja)       ! list of connections with node irow
      INTEGER   jband         ! points to jcol(i) band in irow
      INTEGER   nj            ! global node number (columns)
      INTEGER   ni            ! global node number (row)
c!-----------------------------------------------------------------------
c! Local variables

      INTEGER   istart        ! begining search position in ja()
      INTEGER   istop         ! ending search position in ja()
      INTEGER   jmid          ! list bisection pointer
      INTEGER   jnode         ! global node number

      LOGICAL   done          ! search completion flag

      INTEGER   i0,i1,i2      ! constants
      PARAMETER (i0 = 0,i1 = 1,i2 = 2)
c!-----------------------------------------------------------------------
c!    print *,'enter binary find'

      jband  = i0

      istart = ia(ni) + i1    ! skip diagonal (in first position)
c!    istart = ia(ni)         ! diagonal not in first position

c! Uncomment the following if the calling routine does not catch
c!  the diagonal position. Note that the position of the diagonal
c!  entry is implicit if stored as the first entry: a(ia(ni)).
c!
c!    IF (nj.EQ.ni) THEN      ! check for diagonal
c!      jband = istart        ! WTS1 never searches for diagonal
c!      RETURN                ! as the position is already known.
c!    ENDIF

      istop  = ia(ni + i1) - i1

      done = .FALSE.

      DO WHILE (.NOT.done)

        done  = (istart.GT.istop)     ! end of list

        IF (.NOT.done) THEN

          jmid = (istart + istop)/i2  ! bisect list

          jnode = ja(jmid)
          done  = (jnode.EQ.nj)       ! found entry

          IF (done) THEN              ! found nodal connection
            jband = jmid
          ELSE
            IF (jnode.GT.nj) THEN     ! bisect current search branch
              istop  = jmid - i1      ! search remaining lower half
            ELSE
              istart = jmid + i1      ! search remaining upper half
            ENDIF
          ENDIF

        ENDIF

      ENDDO

      IF (.NOT.(jband.GT.i0)) THEN
        PRINT *, 'WARNING: Connection not found in adjacency list'
        PRINT *, '         Possible error in data structure'
        STOP
      ENDIF

      RETURN
      END

c!=======================================================================
c! Last Modified: March 3, 1996 (JVK)

      SUBROUTINE Check (number,maximum,lfile)

c! Check current workspace requirments
c!-----------------------------------------------------------------------

      IMPLICIT NONE

c! Passed variables

      INTEGER   number        ! current value
      INTEGER   maximum       ! maximum value
      INTEGER   lfile         ! output file unit number
c!-----------------------------------------------------------------------
c!    print *,'enter check'

      IF (number.GT.maximum) THEN
        WRITE (lfile,1)
        WRITE (lfile,1)
c!c!    print *, 'number  ',number
c!c!    print *, 'maximum ',maximum
        PAUSE
        STOP
      ENDIF

 1    FORMAT(//1X,'The work space allocated is insufficient',            &
     &   ' for this simulation.',//1X,                                   &
     &   'Please check input data or correct program dimensions',        &
     &   ' and rerun.',//5x,                                             &
     &   ' <<<<<<<  S I M U L A T I O N   H A L T E D  >>>>>>>'//)
      RETURN
      END
