!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 221 $
!> $Author: dsu $
!> $Date: 2014-08-05 23:29:49 +0200 (Tue, 05 Aug 2014) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/phase_class/tools/F01brx.F90 $
!---------------------------------------------------------------------
!********************************************************************!

      SUBROUTINE F01BRX(N,ICN,LICN,IP,LENR,IPERM,NUMNZ,IW)
!C     MARK 7 RELEASE. NAG COPYRIGHT 1978
!C     MARK 11.5(F77) REVISED. (SEPT 1985.)
!C     DERIVED FROM HARWELL LIBRARY ROUTINE MC21A
!C
!C     INTERFACE FOR F01BRW
!C
      IMPLICIT NONE
!C     .. Scalar Arguments ..
      INTEGER           LICN, N, NUMNZ
!C     .. Array Arguments ..
      INTEGER           ICN(LICN), IP(N), IPERM(N), IW(N,5), LENR(N)
!C     .. Local Scalars ..
      INTEGER           NP1
!C     .. External Subroutines ..
      EXTERNAL          F01BRW
!C     .. Executable Statements ..
      NP1 = N + 1
      CALL F01BRW(N,ICN,LICN,IP,LENR,IPERM,NUMNZ,IW(1,1),IW(1,2), &
                 IW(1,3),IW(1,4),IW(1,5),NP1)
      RETURN
      END
