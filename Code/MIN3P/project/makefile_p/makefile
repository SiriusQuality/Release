# Makefile for MIN3P_THCm MPI Version

# Additional flags that may be required by the compiler ...
# FFLAGS = -em  # When using Cray Fortran compiler
# e.g., FFLAGS = -O3
# FFLAGS = -O2
FFLAGS = 

#FPPFLAGS for GCC Compiler
#FPPFLAGS = -DLINUX -DDEBUG -DDEBUG_X64 -DSTANDARD_FORTRAN -DPETSC -DMPI -DPETSC_V3_5_X -DPETSC_V3_6_X -DPETSC_V3_7_X -DOUTPUT_DOUBLE -DPETSC_HAVE_MUMPS -DPETSC_HAVE_SUPERLU
FPPFLAGS = -DLINUX -DRELEASE -DRELEASE_X64 -DSTANDARD_FORTRAN -DPETSC -DMPI -DPETSC_V3_5_X -DPETSC_V3_6_X -DPETSC_V3_7_X -DOUTPUT_DOUBLE -DPETSC_HAVE_MUMPS -DPETSC_HAVE_SUPERLU
#FPPFLAGS = -DLINUX -DRELEASE -DRELEASE_X64 -DSTANDARD_FORTRAN -DPETSC -DMPI -DPETSC_V3_5_X -DOUTPUT_DOUBLE  -DPETSC_HAVE_MUMPS -DPETSC_HAVE_SUPERLU


#Flag for WestGrid Orcinus System
#Load PETSc module before make
#module load /global/system/Modules/modulefiles/intel-2011/petsc
#FPPFLAGS = -DLINUX -DRELEASE -DRELEASE_X64 -DPETSC -DMPI -DPETSC_V3_5_X -DPETSC_V3_6_X\
 -DOUTPUT_DOUBLE -DPETSC_HAVE_MUMPS -DPETSC_HAVE_SUPERLU

#Flag for WestGrid Jasper System
#Load PETSc module before make
#module load application/petsc/3.5.1                                         
#FPPFLAGS = -DLINUX -DRELEASE -DRELEASE_X64 -DPETSC -DMPI -DPETSC_V3_5_X -DPETSC_V3_6_X -DOUTPUT_DOUBLE -DPETSC_HAVE_SUPERLU

#Flag for WestGrid Bugaboo System
#PETSc is default loaded
#FPPFLAGS = -DLINUX -DRELEASE -DRELEASE_X64 -DSTANDARD_FORTRAN -DPETSC -DMPI

#Flag for IBM Blue Gene/Q System
#FPPFLAGS = -WF,-DLINUX -WF,-DRELEASE -WF,-DRELEASE_X64 -WF,-DSTANDARD_FORTRAN -WF,-DPETSC -WF,-DMPI -WF,-DPETSC_V3_5_X

#FPPFLAGS = -DPARDISO

# Additional libraries that may be required by the linker ...
# DLIB = -lmetis  # When METIS/ParMETIS are built or installed separately from PETSc
# DLIB = C:/cygwin/packages/metis-5.0.2/build/windows/libmetis/Release/libmetis.lib

#PETSc variables for V3.5.3 and previous version
#include ${PETSC_DIR}/conf/variables
#include ${PETSC_DIR}/conf/rules

#PETSc variables for development version, version V3.6.0 and later
include ${PETSC_DIR}/lib/petsc/conf/variables
include ${PETSC_DIR}/lib/petsc/conf/rules

#FC = ifort
#FC = gfortran
SRC =./../../

# Note: Order of the following code files is the same with that in visual fortran project buildlog
SOURCES = $(SRC)dgm/msub.o\
	$(SRC)min3p/ionstr.o\
	$(SRC)min3p/fluxv.o\
	$(SRC)dgm/dgeslm.o\
	$(SRC)min3p/zero_i4.o\
	$(SRC)min3p/secspec.o\
	$(SRC)min3p/diff_vapour.o\
	$(SRC)min3p/crc.o\
	$(SRC)gas_advection/wgprop.o\
	$(SRC)min3p/storheatevap.o\
	$(SRC)gas_advection/fluxvg.o\
	$(SRC)min3p/findkey.o\
	$(SRC)dgm/diffcoff_g.o\
	$(SRC)solver/cond_number/cond_num_cal.o\
	$(SRC)min3p/vhoff.o\
	$(SRC)min3p/icnvrt.o\
	$(SRC)min3p/fluxheat.o\
	$(SRC)dgm/dgefam.o\
	$(SRC)min3p/satindex.o\
	$(SRC)min3p/int3a_ovendry.o\
	$(SRC)min3p/alkcalc.o\
	$(SRC)min3p/storheat.o\
	$(SRC)min3p/rsort.o\
	$(SRC)min3p/gs_ovendry.o\
	$(SRC)min3p/concsort.o\
	$(SRC)min3p/zoutput.o\
	$(SRC)gas_advection/relpfsat_g.o\
	$(SRC)min3p/int_h_ovendry.o\
	$(SRC)min3p/fluxfs.o\
	$(SRC)parallel_io/dumpstring.o\
	$(SRC)min3p/xh_ovendry.o\
	$(SRC)min3p/satfpres_app.o\
	$(SRC)min3p/findzone.o\
	$(SRC)min3p/dhconst.o\
	$(SRC)min3p/cos_function.o\
	$(SRC)min3p/acoff.o\
	$(SRC)min3p/soilprdd.o\
	$(SRC)gas_advection/giups_brt.o\
	$(SRC)min3p/ddtds_1.o\
	$(SRC)min3p/bunsen.o\
	$(SRC)min3p/relpfsat.o\
	$(SRC)min3p/porosity_flow.o\
	$(SRC)min3p/int3b_ovendry.o\
	$(SRC)min3p/fluxdd.o\
	$(SRC)min3p/satfpres.o\
	$(SRC)min3p/g_ovendry.o\
	$(SRC)min3p/findstrg.o\
	$(SRC)gas_advection/dfluxvg.o\
	$(SRC)min3p/w_ovendry.o\
	$(SRC)min3p/storddfs.o\
	$(SRC)min3p/rhsvs.o\
	$(SRC)min3p/bulkconc.o\
	$(SRC)min3p/zero_r8.o\
	$(SRC)min3p/relperm_ovendry.o\
	$(SRC)dgm/mmul.o\
	$(SRC)phase_class/tools/lubksb.o\
	$(SRC)min3p/fluxd.o\
	$(SRC)min3p/darcy_energybal.o\
	$(SRC)solver/cond_number/cond_num_mc75d.o\
	$(SRC)min3p/stordd.o\
	$(SRC)min3p/simq.o\
	$(SRC)min3p/rhsrt.o\
	$(SRC)min3p/boundary_func.o\
	$(SRC)dgm/madd.o\
	$(SRC)gas_advection/gasm.o\
	$(SRC)min3p/dtotcong.o\
	$(SRC)min3p/diffcoff.o\
	$(SRC)gas_advection/wgpropd.o\
	$(SRC)min3p/totsorb.o\
	$(SRC)min3p/h_ovendry.o\
	$(SRC)min3p/fluxvs.o\
	$(SRC)solver/cond_number/cond_num_ddeps.o\
	$(SRC)version/Version.o\
	$(SRC)phase_class/constants/m_constants_cheproo.o\
	$(SRC)phase_class/tools/xmlreader/m_charset.o\
	$(SRC)solver/solver_snes_common.o\
	$(SRC)solver/petsc_mpi_common.o\
	$(SRC)min3p/parm.o\
	$(SRC)phase_class/tools/xmlreader/m_debug.o\
	$(SRC)solver/solver_snes_interface.o\
	$(SRC)min3p/multidiff.o\
	$(SRC)min3p/bbls.o\
	$(SRC)solver/solver_snes_function.o\
	$(SRC)dgm/dgml.o\
	$(SRC)icesheet/m_ice_sheet.o\
	$(SRC)phase_class/tools/xmlreader/m_buffer.o\
	$(SRC)energy_balance/m_heat_transport.o\
	$(SRC)min3p/dual.o\
	$(SRC)phase_class/tools/xmlreader/m_dictionary.o\
	$(SRC)phase_class/tools/xmlreader/m_io.o\
	$(SRC)version/writeversion.o\
	$(SRC)phase_class/tools/xmlreader/m_reader.o\
	$(SRC)min3p/dens.o\
	$(SRC)min3p/gen.o\
	$(SRC)min3p/phys.o\
	$(SRC)phase_class/tools/xmlreader/m_converters.o\
	$(SRC)solver/solver_ddmethod.o\
	$(SRC)phase_class/tools/xmlreader/m_entities.o\
	$(SRC)min3p/file_utility.o\
	$(SRC)phase_class/tools/xmlreader/m_elstack.o\
	$(SRC)min3p/file_unit.o\
	$(SRC)solver/solver_runtime.o\
	$(SRC)parallel_io/m_binary_mpiio.o\
	$(SRC)solver/solver_results.o\
	$(SRC)phase_class/tools/m_general_tools_cheproo.o\
	$(SRC)solver/matrix_utility.o\
	$(SRC)phase_class/tools/xmlreader/m_fsm.o\
	$(SRC)phase_class/tools/xmlreader/m_xml_error.o\
	$(SRC)solver/solver_pardiso.o\
	$(SRC)phase_class/tools/xmlreader/m_xml_parser.o\
	$(SRC)phase_class/tools/xmlreader/flib_sax.o\
	$(SRC)phase_class/tools/xmlreader/m_handlers.o\
	$(SRC)phase_class/tools/xpath/m_path.o\
	$(SRC)phase_class/tools/xpath/flib_xpath.o\
	$(SRC)phase_class/species/m_species.o\
	$(SRC)phase_class/phase/m_parentphase.o\
	$(SRC)phase_class/phase/m_parentaqueousphase.o\
	$(SRC)phase_class/phase/m_aqueousphasepitzer.o\
	$(SRC)phase_class/phase/m_aqueousphaseideal.o\
	$(SRC)phase_class/phase/m_aqueousphasedavis.o\
	$(SRC)phase_class/phase/m_aqueousphasebdot.o\
	$(SRC)phase_class/phase/m_aqueousphase.o\
	$(SRC)phase_class/phase/m_phase.o\
	$(SRC)min3p/chem.o\
	$(SRC)solver/solver.o\
	$(SRC)min3p/visconew.o\
	$(SRC)min3p/velocity.o\
	$(SRC)min3p/tsteprt.o\
	$(SRC)dgm/totmin_w.o\
	$(SRC)min3p/totint.o\
	$(SRC)min3p/stedflow.o\
	$(SRC)min3p/setsize.o\
	$(SRC)min3p/restart_w.o\
	$(SRC)min3p/readint.o\
	$(SRC)min3p/rate_gasmin.o\
	$(SRC)min3p/opnmbfls_mcd.o\
	$(SRC)min3p/mem_vs.o\
	$(SRC)min3p/mem_dd.o\
	$(SRC)min3p/initppdd.o\
	$(SRC)min3p/initbcvs.o\
	$(SRC)min3p/icrtlczn.o\
	$(SRC)min3p/gcreact.o\
	$(SRC)min3p/energy_bal.o\
	$(SRC)min3p/distmp.o\
	$(SRC)min3p/ddbdflux_energybal.o\
	$(SRC)min3p/clstpfls.o\
	$(SRC)min3p/bdryflux.o\
	$(SRC)min3p/updtsurf.o\
	$(SRC)min3p/tranbcrt_read.o\
	$(SRC)min3p/tortuosity_vap.o\
	$(SRC)min3p/readtime.o\
	$(SRC)min3p/phpe.o\
	$(SRC)min3p/mem_nmx.o\
	$(SRC)dgm/lennard_jones.o\
	$(SRC)min3p/jacsurf.o\
	$(SRC)min3p/initicdd.o\
	$(SRC)min3p/infevap.o\
	$(SRC)min3p/iajart.o\
	$(SRC)gas_advection/gasd_m.o\
	$(SRC)min3p/dtotconc.o\
	$(SRC)min3p/checksat.o\
	$(SRC)min3p/aratemin.o\
	$(SRC)min3p/updatert.o\
	$(SRC)min3p/totredx.o\
	$(SRC)min3p/rstatgs.o\
	$(SRC)min3p/readminx_new.o\
	$(SRC)min3p/ratemin.o\
	$(SRC)min3p/outputlc.o\
	$(SRC)min3p/mem_naq.o\
	$(SRC)min3p/jacddfs.o\
	$(SRC)min3p/initprob.o\
	$(SRC)min3p/initcpgs.o\
	$(SRC)min3p/infcrtdd.o\
	$(SRC)min3p/guess.o\
	$(SRC)min3p/drateint.o\
	$(SRC)min3p/ddvsflow.o\
	$(SRC)min3p/checkerr.o\
	$(SRC)min3p/vel_lin.o\
	$(SRC)min3p/tsteplc.o\
	$(SRC)dgm/totint_w.o\
	$(SRC)min3p/totdyvisc.o\
	$(SRC)min3p/stddmass.o\
	$(SRC)min3p/restart_r.o\
	$(SRC)min3p/readgses.o\
	$(SRC)min3p/raoult.o\
	$(SRC)min3p/opnmbfls.o\
	$(SRC)dgm/ms_fluxdg_s.o\
	$(SRC)min3p/mem_rt.o\
	$(SRC)min3p/mem_bbls.o\
	$(SRC)min3p/intpolt.o\
	$(SRC)min3p/pitzer/initpitzer_pc.o\
	$(SRC)min3p/initbcrt.o\
	$(SRC)min3p/gcconst.o\
	$(SRC)min3p/elecmigration.o\
	$(SRC)min3p/diffcoff_mcd.o\
	$(SRC)min3p/ddbdflux.o\
	$(SRC)min3p/clsgfls.o\
	$(SRC)min3p/xyzcoord.o\
	$(SRC)min3p/updtsoil.o\
	$(SRC)min3p/tranbcrt.o\
	$(SRC)min3p/timeloop.o\
	$(SRC)min3p/readtemp.o\
	$(SRC)min3p/phcorr.o\
	$(SRC)min3p/msysvs.o\
	$(SRC)min3p/mem_nm.o\
	$(SRC)min3p/jacrt.o\
	$(SRC)min3p/initgeom.o\
	$(SRC)gas_advection/infcvs_cp.o\
	$(SRC)min3p/iajabl.o\
	$(SRC)dgm/gasdiff2_s.o\
	$(SRC)min3p/gasconc.o\
	$(SRC)min3p/driver_pc.o\
	$(SRC)min3p/diff_grad.o\
	$(SRC)min3p/cputime_pc.o\
	$(SRC)min3p/welcome_pc.o\
	$(SRC)min3p/velovapour.o\
	$(SRC)min3p/updatelc.o\
	$(SRC)min3p/readminx.o\
	$(SRC)min3p/rateint_new.o\
	$(SRC)min3p/outputdd.o\
	$(SRC)min3p/mem_mat.o\
	$(SRC)min3p/jacbvs_energybal.o\
	$(SRC)min3p/initppvs.o\
	$(SRC)min3p/initcpgb.o\
	$(SRC)min3p/infcrt_mcd.o\
	$(SRC)min3p/fluxv_vl.o\
	$(SRC)min3p/drategdd.o\
	$(SRC)min3p/ddtds_energybal.o\
	$(SRC)min3p/cbalance.o\
	$(SRC)min3p/vapor_prop.o\
	$(SRC)min3p/trddflow.o\
	$(SRC)min3p/totconfac.o\
	$(SRC)min3p/sptldisc.o\
	$(SRC)min3p/readcomp.o\
	$(SRC)dgm/read_tau.o\
	$(SRC)min3p/radiation.o\
	$(SRC)min3p/opngfls.o\
	$(SRC)dgm/ms_fluxdg.o\
	$(SRC)min3p/mem_nx.o\
	$(SRC)min3p/mbalvs.o\
	$(SRC)min3p/initopgs.o\
	$(SRC)min3p/initbcenergybal.o\
	$(SRC)min3p/icbcrt.o\
	$(SRC)gas_advection/gasv.o\
	$(SRC)min3p/datstr_n.o\
	$(SRC)min3p/pitzer/closepitzer.o\
	$(SRC)min3p/batreac.o\
	$(SRC)min3p/updtbcvs.o\
	$(SRC)min3p/tprfvs.o\
	$(SRC)min3p/tcorr.o\
	$(SRC)min3p/readsspc.o\
	$(SRC)min3p/msysrt.o\
	$(SRC)min3p/mem_njavs.o\
	$(SRC)min3p/jaclc.o\
	$(SRC)min3p/initweed.o\
	$(SRC)min3p/initcsys.o\
	$(SRC)min3p/infcvs.o\
	$(SRC)min3p/i2upfind_heat.o\
	$(SRC)dgm/gasdiff2.o\
	$(SRC)min3p/gasbub.o\
	$(SRC)min3p/draterdx.o\
	$(SRC)min3p/weed.o\
	$(SRC)min3p/velodd.o\
	$(SRC)min3p/updatedd_energybalance.o\
	$(SRC)min3p/storfs.o\
	$(SRC)min3p/rootwat.o\
	$(SRC)min3p/readmin_new.o\
	$(SRC)min3p/rateint.o\
	$(SRC)min3p/opntemp.o\
	$(SRC)min3p/molconc.o\
	$(SRC)min3p/mem_hmcd.o\
	$(SRC)min3p/jacbvs.o\
	$(SRC)min3p/initpprt.o\
	$(SRC)min3p/initcpevaporation.o\
	$(SRC)min3p/infcrt_g.o\
	$(SRC)min3p/drategas.o\
	$(SRC)dgm/dgm_fluxdg_s.o\
	$(SRC)min3p/compute_sensible.o\
	$(SRC)min3p/zero_r8_parallel.o\
	$(SRC)min3p/updtsvmp.o\
	$(SRC)min3p/tranunit.o\
	$(SRC)min3p/totconcg.o\
	$(SRC)min3p/sortcomp.o\
	$(SRC)min3p/readbloc.o\
	$(SRC)dgm/rateh2o.o\
	$(SRC)min3p/opnenergybal.o\
	$(SRC)dgm/ms_dfluxdg.o\
	$(SRC)min3p/mem_ntmp.o\
	$(SRC)min3p/mbalrt.o\
	$(SRC)min3p/initicvs.o\
	$(SRC)min3p/initbcdd.o\
	$(SRC)min3p/iajavs_energybal.o\
	$(SRC)gas_advection/gasp_m.o\
	$(SRC)min3p/datstr_1.o\
	$(SRC)min3p/cliqdisp.o\
	$(SRC)min3p/atotconc.o\
	$(SRC)solver/ws209.o\
	$(SRC)min3p/updtbcdd.o\
	$(SRC)min3p/tprfrtlc.o\
	$(SRC)min3p/surfcomp.o\
	$(SRC)min3p/readsorb.o\
	$(SRC)min3p/reactran.o\
	$(SRC)min3p/msysdd.o\
	$(SRC)min3p/mem_njart.o\
	$(SRC)min3p/jacfs.o\
	$(SRC)min3p/inittsgs.o\
	$(SRC)min3p/initcpvs.o\
	$(SRC)min3p/i2upfind.o\
	$(SRC)dgm/gasdiff_s.o\
	$(SRC)min3p/dratemin_salinity.o\
	$(SRC)min3p/dratemin_new.o\
	$(SRC)min3p/constnts.o\
	$(SRC)gas_advection/velocity_g.o\
	$(SRC)min3p/updatedd.o\
	$(SRC)min3p/soilparm.o\
	$(SRC)min3p/readmin.o\
	$(SRC)min3p/rategasd.o\
	$(SRC)min3p/opnplfls.o\
	$(SRC)min3p/modrate.o\
	$(SRC)min3p/mem_heat.o\
	$(SRC)min3p/jacbrt.o\
	$(SRC)min3p/initpppm.o\
	$(SRC)min3p/initcpenergybal.o\
	$(SRC)min3p/infcrt_a.o\
	$(SRC)gas_advection/giups.o\
	$(SRC)min3p/evap.o\
	$(SRC)min3p/drate_gasmin.o\
	$(SRC)dgm/dgm_fluxdg.o\
	$(SRC)min3p/ddtds.o\
	$(SRC)min3p/comptotc.o\
	$(SRC)min3p/updtsvgp.o\
	$(SRC)min3p/trans_evap_info.o\
	$(SRC)min3p/totconc.o\
	$(SRC)min3p/sorbspc.o\
	$(SRC)min3p/seepfdd.o\
	$(SRC)min3p/readbcvs.o\
	$(SRC)min3p/pitzer/pitzer.o\
	$(SRC)min3p/opndbfls_pc.o\
	$(SRC)min3p/mem_nsb.o\
	$(SRC)min3p/mbaldd.o\
	$(SRC)min3p/initicrt.o\
	$(SRC)min3p/infheat_d.o\
	$(SRC)min3p/iajavs_dp.o\
	$(SRC)gas_advection/gasp.o\
	$(SRC)min3p/dtotdyvisc.o\
	$(SRC)min3p/chrgcorr.o\
	$(SRC)min3p/assnpair.o\
	$(SRC)min3p/updtbcatm.o\
	$(SRC)min3p/tprfdd.o\
	$(SRC)min3p/surf_tens_prop.o\
	$(SRC)min3p/rtrvpprm.o\
	$(SRC)min3p/readredx_new.o\
	$(SRC)min3p/rateredx.o\
	$(SRC)min3p/outputvs.o\
	$(SRC)min3p/mem_ng.o\
	$(SRC)min3p/jacddvs.o\
	$(SRC)min3p/inittemp.o\
	$(SRC)min3p/initcprt.o\
	$(SRC)min3p/hydrostatic_pitzerdens.o\
	$(SRC)dgm/gasdiff.o\
	$(SRC)min3p/fsflow.o\
	$(SRC)min3p/findpath.o\
	$(SRC)min3p/dratemin.o\
	$(SRC)min3p/vsflow.o\
	$(SRC)min3p/velocity_a.o\
	$(SRC)min3p/tstepfs.o\
	$(SRC)min3p/tstepvs.o\
	$(SRC)dgm/totredx_w.o\
	$(SRC)min3p/totmin.o\
	$(SRC)min3p/readint_new.o\
	$(SRC)min3p/rategas.o\
	$(SRC)min3p/opnpgfls.o\
	$(SRC)min3p/minmaxwd.o\
	$(SRC)min3p/mem_evap.o\
	$(SRC)min3p/jacbevap.o\
	$(SRC)min3p/initppenergybal.o\
	$(SRC)min3p/initcpdd.o\
	$(SRC)min3p/indextec.o\
	$(SRC)min3p/energysys.o\
	$(SRC)min3p/draoult.o\
	$(SRC)dgm/dgm_dfluxdg.o\
	$(SRC)min3p/ddfsflow.o\
	$(SRC)icesheet/comp_bc_ice_sheet.o\
	$(SRC)min3p/zero_i4_parallel.o\
	$(SRC)min3p/updtsvap.o\
	$(SRC)min3p/tranflow.o\
	$(SRC)min3p/totcona.o\
	$(SRC)min3p/solvsg.o\
	$(SRC)min3p/seepface.o\
	$(SRC)min3p/readzone.o\
	$(SRC)min3p/reactran_2.o\
	$(SRC)min3p/nexttime.o\
	$(SRC)min3p/mem_nr.o\
	$(SRC)min3p/mbal_mcd.o\
	$(SRC)min3p/jacvs.o\
	$(SRC)min3p/initicenergybal.o\
	$(SRC)min3p/infheat_c.o\
	$(SRC)min3p/iajavs.o\
	$(SRC)min3p/flux_calc.o\
	$(SRC)min3p/cvolume.o\
	$(SRC)min3p/assngaspc.o\
	$(SRC)min3p/updatevs.o\
	$(SRC)min3p/storvs.o\
	$(SRC)min3p/rstatlc.o\
	$(SRC)min3p/readredx.o\
	$(SRC)min3p/ratemin_new.o\
	$(SRC)min3p/outputrt.o\
	$(SRC)min3p/mem_nc.o\
	$(SRC)min3p/jacddfs_energybal.o\
	$(SRC)min3p/initsatw.o\
	$(SRC)min3p/initcplc.o\
	$(SRC)min3p/findname.o\
	$(SRC)dgm/drt_hubb.o\
	$(SRC)min3p/drateint_new.o\
	$(SRC)min3p/delcmigfunc.o



min3p: $(SOURCES) chkopts
	-${FLINKER} $(FFLAGS) $(FPPFLAGS) -o min3p_thcm $(SOURCES) ${PETSC_LIB} ${DLIB}
	${RM} $(SRC)*.o $(SRC)energy_balance/*.o $(SRC)icesheet/*.o $(SRC)min3p/*.o 
	${RM} $(SRC)min3p/pitzer/*.o $(SRC)phase_class/constants/*.o 
	${RM} $(SRC)phase_class/phase/*.o $(SRC)phase_class/species/*.o 
	${RM} $(SRC)phase_class/tools/*.o $(SRC)phase_class/tools/xmlreader/*.o 
	${RM} $(SRC)phase_class/tools/xpath/*.o $(SRC)version/*.o
	${RM} $(SRC)solver/*.o $(SRC)solver/cond_number/*.o $(SRC)gas_advection/*.o 
	${RM} $(SRC)parallel_io/*.o $(SRC)dgm/*.o *.o
	${RM} $(SRC)*.mod $(SRC)energy_balance/*.mod $(SRC)icesheet/*.mod $(SRC)min3p/*.mod
	${RM} $(SRC)min3p/pitzer/*.mod $(SRC)phase_class/constants/*.mod 
	${RM} $(SRC)phase_class/phase/*.mod $(SRC)phase_class/species/*.mod 
	${RM} $(SRC)phase_class/tools/*.mod $(SRC)phase_class/tools/xmlreader/*.mod 
	${RM} $(SRC)phase_class/tools/xpath/*.mod $(SRC)version/*.mod
	${RM} $(SRC)solver/*.mod $(SRC)solver/cond_number/*.mod $(SRC)gas_advection/*.mod 
	${RM} $(SRC)parallel_io/*.mod $(SRC)dgm/*.mod *.mod
