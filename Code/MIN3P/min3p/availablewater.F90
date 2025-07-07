! Calculate daily values of global variables used by Sirius
! Written by Frédéric Gérard, June 2021
 
      real*8 function availablewater(satwnew,ivol)
 
      use parm
      use phys
      use gen
	  use biol

      real*8 satwnew(*)

!c  setup pointer to the material properties of the current volume     

	  izn = mpropvs(ivol)

!c calculate available water in control volume, in m (1D only as toparea used to convert m3 of water to m of water.
!                                                                dx(ivol) x dy(ivol) should be used for generalization
! HERE all water above satwlim is included. Set to 0 elsewhere
          availablewater=0.0d0
          if (satwnew(ivol).ge.satwlim(izn)) then
            availablewater=(satwnew(ivol)-satwlim(izn))*pornew(ivol) ! in m3water/m3bulk
!conversion in m3 water
            availablewater=availablewater*cvol(ivol)
!conversion in mm water
            availablewater=1000*availablewater/toparea  
          endif

!        write(*,*) 'ivol=', ivol
!        write(*,*) 'satwnew(ivol)=', satwnew(ivol)
!        write(*,*) 'satwlim(izn)=', satwlim(izn)
!        write(*,*) 'availablewater (mm)', availablewater

      return

	end
