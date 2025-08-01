!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 296 $
!> $Author: dsu $
!> $Date: 2015-04-07 22:22:37 +0200 (Tue, 07 Apr 2015) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/dgm/msub.F90 $
!---------------------------------------------------------------------
!********************************************************************!

      SUBROUTINE MSUB (NA,NB,NC,M,N,A,B,C)
      
      IMPLICIT NONE
!C
!C     *****PARAMETERS:
      INTEGER NA,NB,NC,M,N
      DOUBLE PRECISION A(NA,N),B(NB,N),C(NC,N)
!C
!C     *****LOCAL VARIABLES:
      INTEGER I,J
!C
!C     *****SUBROUTINES CALLED:
!C     NONE
!C
!C     ------------------------------------------------------------------
!C
!C     *****PURPOSE:
!C     THIS SUBROUTINE COMPUTES THE MATRIX DIFFERENCE A-B AND STORES
!C     THE RESULT IN THE ARRAY C.  ALL MATRICES ARE M X N.  THE
!C     DIFFERENCE MAY BE OVERWRITTEN INTO A BY DESIGNATING THE
!C     ARRAY C TO BE A.
!C
!C     *****PARAMETER DESCRIPTION:
!C     ON INPUT:
!C        NA,NB,NC         ROW DIMENSIONS OF THE ARRAYS CONTAINING A,B,
!C                         AND C, RESPECTIVELY, AS DECLARED IN THE
!C                         CALLING PROGRAM DIMENSION STATEMENT;
!C
!C        M                NUMBER OF ROWS OF THE MATRICES A,B, AND C;
!C
!C        N                NUMBER OF COLUMNS OF THE MATRICES A,B, AND C;
!C
!C        A                AN M X N MATRIX;
!C
!C        B                AN M X N MATRIX.
!C
!C     ON OUTPUT:
!C
!C        C                AN M X N ARRAY CONTAINING A-B.
!C
!C     *****HISTORY:
!C     WRITTEN BY ALAN J. LAUB (ELEC. SYS. LAB., M.I.T., RM. 35-331,
!C     CAMBRIDGE, MA 02139,  PH.: (617)-253-2125), SEPTEMBER 1977.
!C     MOST RECENT VERSION: SEP. 21, 1977.
!C
!C     ------------------------------------------------------------------
!C
      DO 20 J=1,N
         DO 10 I=1,M
            C(I,J)=A(I,J)-B(I,J)
10       CONTINUE
20    CONTINUE
      RETURN
!C
!C     LAST LINE OF MSUB
!C
      END