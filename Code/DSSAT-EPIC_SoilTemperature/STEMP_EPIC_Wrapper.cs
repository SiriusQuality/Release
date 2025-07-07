using System;
using System.Collections.Generic;
using System.Linq;
public class STEMP_EPIC_Wrapper
{
    public STEMP_EPIC_State s;
    public STEMP_EPIC_State s1;
    public STEMP_EPIC_Rate r;
    public STEMP_EPIC_Auxiliary a;
    public STEMP_EPIC_Exogenous ex;
    public STEMP_EPIC_Component stemp_epic_Component;

    public STEMP_EPIC_Wrapper()
    {
        s = new STEMP_EPIC_State();
        r = new STEMP_EPIC_Rate();
        a = new STEMP_EPIC_Auxiliary();
        ex = new STEMP_EPIC_Exogenous();
        stemp_epic_Component = new STEMP_EPIC_Component();
        loadParameters();
    }

        double[] BD =  new double [100];
    double[] DUL =  new double [100];
    double[] DS =  new double [100];
    double[] DLAYR =  new double [100];
    double[] LL =  new double [100];
    double[] SW =  new double [100];
    int NLAYR;
    int NL;
    string ISWWAT;

    public int NDays{ get { return s.NDays;}} 
     
    public int[] WetDay{ get { return s.WetDay;}} 
     
    public double TDL{ get { return s.TDL;}} 
     
    public double X2_PREV{ get { return s.X2_PREV;}} 
     
    public double[] DSMID{ get { return s.DSMID;}} 
     
    public double[] TMA{ get { return s.TMA;}} 
     
    public double SRFTEMP{ get { return s.SRFTEMP;}} 
     
    public double[] ST{ get { return s.ST;}} 
     
    public double CUMDPT{ get { return s.CUMDPT;}} 
     

    public STEMP_EPIC_Wrapper(STEMP_EPIC_Wrapper toCopy, bool copyAll) : this()
    {
        s = (toCopy.s != null) ? new STEMP_EPIC_State(toCopy.s, copyAll) : null;
        r = (toCopy.r != null) ? new STEMP_EPIC_Rate(toCopy.r, copyAll) : null;
        a = (toCopy.a != null) ? new STEMP_EPIC_Auxiliary(toCopy.a, copyAll) : null;
        ex = (toCopy.ex != null) ? new STEMP_EPIC_Exogenous(toCopy.ex, copyAll) : null;
        if (copyAll)
        {
            stemp_epic_Component = (toCopy.stemp_epic_Component != null) ? new STEMP_EPIC_Component(toCopy.stemp_epic_Component) : null;
        }
    }

    public void Init(){
        //stemp_epic_Component.Init(s, r, a);
        loadParameters();
    }

    private void loadParameters()
    {
        stemp_epic_Component.BD = BD;
        stemp_epic_Component.DUL = DUL;
        stemp_epic_Component.DS = DS;
        stemp_epic_Component.DLAYR = DLAYR;
        stemp_epic_Component.LL = LL;
        stemp_epic_Component.SW = SW;
        stemp_epic_Component.NLAYR = NLAYR;
        stemp_epic_Component.NL = NL;
        stemp_epic_Component.ISWWAT = ISWWAT;
    }

    public void EstimateSTEMP_EPIC_(double RAIN, double DEPIR, double TMIN, double BIOMAS, double TAMP, double MULCHMASS, double TMAX, double TAV, double SNOW, double TAVG)
    {
        ex.RAIN = RAIN;
        ex.DEPIR = DEPIR;
        ex.TMIN = TMIN;
        ex.BIOMAS = BIOMAS;
        ex.TAMP = TAMP;
        ex.MULCHMASS = MULCHMASS;
        ex.TMAX = TMAX;
        ex.TAV = TAV;
        ex.SNOW = SNOW;
        ex.TAVG = TAVG;
        stemp_epic_Component.CalculateModel(s,s1, r, a, ex);
    }

}