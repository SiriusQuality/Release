!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 221 $
!> $Author: dsu $
!> $Date: 2014-08-05 23:29:49 +0200 (Tue, 05 Aug 2014) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/phase_class/tools/F01brs.F90 $
!---------------------------------------------------------------------
!********************************************************************!

      SUBROUTINE F01BRS(A,ICN,IPTR,N,IACTIV,ITOP,REALS,IUP)
!C     MARK 7 RELEASE. NAG COPYRIGHT 1978
!C     MARK 11.5(F77) REVISED. (SEPT 1985.)
!C     DERIVED FROM HARWELL LIBRARY ROUTINE MA30D
!C
!C     COLLECTS GARBAGE IN ARRAYS, COMPRESSING THE USEFUL
!C     INFORMATION TO THE TOP END OF THE ARRAY AND FREEING THE
!C     EARLIER LOCATIONS FOR WORKSPACE OR SUBSEQUENT STORAGE.
!C
!C     IF REALS=TRUE, F01BRS MUST BE CALLED WITH ICNCP AS IUP.
!C     IF REALS=FALSE, F01BRS MUST BE CALLED WITH IRNCP AS IUP.
!C     IACTIV IS THE FIRST POSITION IN ARRAYS A/ICN FROM WHICH THE
!C     COMPRESS STARTS.
!C     ON EXIT IACTIV EQUALS THE POSITION OF THE FIRST ELEMENT IN
!C     THE COMPRESSED PART OF A/ICN
      IMPLICIT NONE
!C     .. Scalar Arguments ..
      INTEGER           IACTIV, ITOP, IUP, N
      LOGICAL           REALS
!C     .. Array Arguments ..
      DOUBLE PRECISION  A(ITOP)
      INTEGER           ICN(ITOP), IPTR(N)
!C     .. Local Scalars ..
      INTEGER           J, JPOS, K, KL, KN
!C     .. Executable Statements ..
      IUP = IUP + 1
!C     SET THE FIRST NON-ZERO ELEMENT IN EACH ROW TO THE NEGATIVE OF
!C     THE ROW/COL NUMBER AND HOLD THIS ROW/COL INDEX IN THE ROW/COL
!C     POINTER.  THIS IS SO THAT THE BEGINNING OF EACH ROW/COL CAN
!C     BE RECOGNIZED IN THE SUBSEQUENT SCAN.
      DO 20 J = 1, N
         K = IPTR(J)
         IF (K.LT.IACTIV) GO TO 20
         IPTR(J) = ICN(K)
         ICN(K) = -J
   20 CONTINUE
      KN = ITOP + 1
      KL = ITOP - IACTIV + 1
!C     GO THROUGH ARRAYS IN REVERSE ORDER COMPRESSING TO THE BACK SO
!C     THAT THERE ARE NO ZEROS HELD IN POSITIONS IACTIV TO ITOP IN
!C     ICN.
!C     RESET FIRST ELEMENT OF EACH ROW/COL AND POINTER ARRAY IPTR.
      DO 60 K = 1, KL
         JPOS = ITOP - K + 1
         IF (ICN(JPOS).EQ.0) GO TO 60
         KN = KN - 1
         IF (REALS) A(KN) = A(JPOS)
         IF (ICN(JPOS).GE.0) GO TO 40
!C        FIRST NON-ZERO OF ROW/COL HAS BEEN LOCATED
         J = -ICN(JPOS)
         ICN(JPOS) = IPTR(J)
         IPTR(J) = KN
   40    ICN(KN) = ICN(JPOS)
   60 CONTINUE
      IACTIV = KN
      RETURN
      END
