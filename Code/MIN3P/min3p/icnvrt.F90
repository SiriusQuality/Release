!*****Revision Informations Automatically Generated by VisualSVN*****!
!---------------------------------------------------------------------
!> $ID:$
!> $Revision: 221 $
!> $Author: dsu $
!> $Date: 2014-08-05 23:29:49 +0200 (Tue, 05 Aug 2014) $
!> $URL: https://biot.eos.ubc.ca/svn/min3p_thcm/branches/fgerard_new/src/min3p/icnvrt.F90 $
!---------------------------------------------------------------------
!********************************************************************!

!C
!C       FUNCTION: ICNVRT
!CF
!CF        This subroutine does an integer-to-character conversion
!CF        or a characater-to-integer conversion depending on the
!CF        integer WAY:
!CF                If WAY = 0 then an integer-to-character conversion
!CF                is done. If WAY .NE. 0 then a character-to-integer
!CF                conversion is done.
!CF
!C       USAGE:
!CU
!CU        CALL ICNVRT(WAY,NUM,STRING)
!CU             where WAY, NUM, STRING, and LENGTH are defined below.
!CU
!CU        Example: CALL ICNVRT(0,1000,STRING,LENGTH)
!CU                 on return STRING = '1000' and
!CU                 LENGTH = 4.
!CU         
!C       INPUTS:
!CI
!CI        WAY - INTEGER; Determines which way the conversion goes:
!CI              if WAY = 0 then an integer-to-character conversion
!CI                         is performed;
!CI              if WAY.NE.0 then a character-to-integer conversion
!CI                         is performed.
!CI
!CI         NUM - INTEGER; an input only if WAY = 0. NUM is the integer
!CI               number to be converted to a character expression.
!CI
!CI         STRING - CHARACTER; an input only if WAY .NE. 0. STRING
!CI                is the character expression to be converted to an
!CI                integer value. It contain no decimal points or 
!CI                non-numeric characters other than possibly a
!CI                sign. If STRING contains  a '+' sign, it will be
!CI                stripped of it on return.
!CI
!C       OUTPUTS:
!CO
!CO         NUM - INTEGER; contains the INTEGER representation of 
!CO                STRING.
!CO
!CO         STRING - CHARACTER; contains the CHARACTER representation of
!CO                  NUM.
!CO
!CO         LENGTH - INTEGER; The length of STRING to the first blank.
!CO                  The significant part of STRING can be accessed with
!CO                  the declaration STRING(1:LENGTH).
!CO
!CO         IERR - INTEGER variable giving return condition:
!CO                IERR = 0 for normal return;
!CO                IERR = 1 if NUM cannot be converted to STRING because
!CO                       STRING is too short or STRING cannot be
!CO                       converted to NUM because STRING is too long.
!CO                IERR = 2 if STRING contained a non-numeric character
!CO                       other than a leading sign or something went
!CO                       wrong with an integer-to-character conversion.
!CO
!C       ALGORITHM:
!CA
!CA         Nothing noteworthy, except that this subroutine will work
!CA          for strange character sets where the character '1' doesn't
!CA          follow '0', etc.
!CA
!C       MACHINE DEPENDENCIES:
!CM
!CM          The parameter MAXINT (below) should be set to the
!CM          number of digits that an INTEGER data type can have
!CM          not including leading signs. For VAX FORTRAN V4.4-177
!CM          MAXINT = 10.
!CM
!CM          NOTE: Under VAX FORTRAN V4.4-177, the
!CM          error condition IERR = 1 will never occur for an
!CM          integer-to-character conversion if STRING
!CM          is allocated at least 11 bytes (CHARACTER*11).
!CM
!C       HISTORY:
!CH
!CH      written by:             bobby bodenheimer
!CH      date:                   september 1986
!CH      current version:        1.0
!CH      modifications:          none
!CH
!C       ROUTINES CALLED:
!CC
!CC          NONE.
!CC
!C----------------------------------------------------------------------
!C       written for:    The CASCADE Project
!C                       Oak Ridge National Laboratory
!C                       U.S. Department of Energy
!C                       contract number DE-AC05-840R21400
!C                       subcontract number 37B-7685 S13
!C                       organization:  The University of Tennessee
!C----------------------------------------------------------------------
!C       THIS SOFTWARE IS IN THE PUBLIC DOMAIN
!C       NO RESTRICTIONS ON ITS USE ARE IMPLIED
!C----------------------------------------------------------------------
!C

SUBROUTINE ICNVRT(WAY,NUM,STRING,LENGTH,IERR)

      IMPLICIT NONE

!C Global Variables.
!C
      INTEGER       WAY
      INTEGER       LENGTH
      INTEGER       NUM
      INTEGER       IERR
      CHARACTER*(*) STRING
!C
!C Local Variables
!C
      INTEGER       I
      INTEGER       MAXINT
      INTEGER       MNUM
      INTEGER       M
      LOGICAL       NEG
!C
      PARAMETER(MAXINT=10)
!C
      NEG = .FALSE.
      IERR = 0
!C
!C  Integer-to-character conversion.
!C
      IF (WAY.EQ.0) THEN
         STRING = ' '
         IF (NUM.LT.0) THEN
            NEG = .TRUE.
            MNUM = -NUM
            LENGTH = INT(LOG10(REAL(MNUM))) + 1
         ELSE IF (NUM.EQ.0) THEN
            MNUM = NUM
            LENGTH = 1
         ELSE
            MNUM = NUM
            LENGTH = INT(LOG10(REAL(MNUM))) + 1
         END IF
         IF (LENGTH.GT.LEN(STRING)) THEN
            IERR = 1
            RETURN
         END IF
         DO 10, I=LENGTH,1,-1
            M=INT(REAL(MNUM)/10**(I-1))
            IF (M.EQ.0) THEN
               STRING(LENGTH-I+1:LENGTH-I+1) = '0'
            ELSE IF (M.EQ.1) THEN
               STRING(LENGTH-I+1:LENGTH-I+1) = '1'
            ELSE IF (M.EQ.2) THEN
               STRING(LENGTH-I+1:LENGTH-I+1) = '2'
            ELSE IF (M.EQ.3) THEN
               STRING(LENGTH-I+1:LENGTH-I+1) = '3'
            ELSE IF (M.EQ.4) THEN
               STRING(LENGTH-I+1:LENGTH-I+1) = '4'
            ELSE IF (M.EQ.5) THEN
               STRING(LENGTH-I+1:LENGTH-I+1) = '5'
            ELSE IF (M.EQ.6) THEN
               STRING(LENGTH-I+1:LENGTH-I+1) = '6'
            ELSE IF (M.EQ.7) THEN
               STRING(LENGTH-I+1:LENGTH-I+1) = '7'
            ELSE IF (M.EQ.8) THEN
               STRING(LENGTH-I+1:LENGTH-I+1) = '8'
            ELSE IF (M.EQ.9) THEN
               STRING(LENGTH-I+1:LENGTH-I+1) = '9'
            ELSE
               IERR = 2
               RETURN
            END IF
            MNUM = MNUM - M*10**(I-1)
10       CONTINUE
         IF (NEG) THEN
            STRING = '-'//STRING
            LENGTH = LENGTH + 1
         END IF
!C
!C  Character-to-integer conversion.
!C
      ELSE
         IF (STRING(1:1).EQ.'-') THEN
            NEG = .TRUE.
            STRING = STRING(2:LEN(STRING))
         END IF
         IF (STRING(1:1).EQ.'+') STRING = STRING(2:LEN(STRING))
         NUM = 0
         LENGTH = INDEX(STRING,' ') - 1
         IF (LENGTH.GT.MAXINT) THEN
            IERR = 1
            RETURN
         END IF
         DO 20, I=LENGTH,1,-1
            IF (STRING(LENGTH-I+1:LENGTH-I+1).EQ.'0') THEN
               M = 0
            ELSE IF (STRING(LENGTH-I+1:LENGTH-I+1).EQ.'1') THEN
               M = 1
            ELSE IF (STRING(LENGTH-I+1:LENGTH-I+1).EQ.'2') THEN
               M = 2
            ELSE IF (STRING(LENGTH-I+1:LENGTH-I+1).EQ.'3') THEN
               M = 3
            ELSE IF (STRING(LENGTH-I+1:LENGTH-I+1).EQ.'4') THEN
               M = 4
            ELSE IF (STRING(LENGTH-I+1:LENGTH-I+1).EQ.'5') THEN
               M = 5
            ELSE IF (STRING(LENGTH-I+1:LENGTH-I+1).EQ.'6') THEN
               M = 6
            ELSE IF (STRING(LENGTH-I+1:LENGTH-I+1).EQ.'7') THEN
               M = 7
            ELSE IF (STRING(LENGTH-I+1:LENGTH-I+1).EQ.'8') THEN
               M = 8
            ELSE IF (STRING(LENGTH-I+1:LENGTH-I+1).EQ.'9') THEN
               M = 9
            ELSE
               IERR = 2
               RETURN
            END IF
            NUM = NUM + INT(10**(I-1))*M
20       CONTINUE
         IF (NEG) THEN
            NUM = -NUM
            STRING = '-'//STRING
            LENGTH = LENGTH + 1
         END IF
      END IF
!C
!C  Last lines of ICNVRT
!C
      RETURN
      END
         
            
