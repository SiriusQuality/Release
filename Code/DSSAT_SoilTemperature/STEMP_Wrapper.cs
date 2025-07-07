using System;
using System.Collections.Generic;
using System.Linq;
public class STEMP_Wrapper
{
    public STEMP_State s;
    public STEMP_State s1;
    public STEMP_Rate r;
    public STEMP_Auxiliary a;
    public STEMP_Exogenous ex;
    public STEMP_Component stemp_Component;

    public STEMP_Wrapper()
    {
        s = new STEMP_State();
        r = new STEMP_Rate();
        a = new STEMP_Auxiliary();
        ex = new STEMP_Exogenous();
        stemp_Component = new STEMP_Component();
        loadParameters();
    }


    double[] DLAYR = new double[100];
    double[] SW = new double[100];
    int NLAYR;
    double[] LL = new double[100];
    string ISWWAT;
    double[] DUL = new double[100];
    double[] BD = new double[100];
    double[] DS = new double[100];
    int NL;
    double XLAT;
    double MSALB;

    public double[] DSMID { get { return s.DSMID; } }

    public double[] ST { get { return s.ST; } }

    public double CUMDPT { get { return s.CUMDPT; } }

    public double[] TMA { get { return s.TMA; } }

    public double TDL { get { return s.TDL; } }

    public double SRFTEMP { get { return s.SRFTEMP; } }

    public double ATOT { get { return s.ATOT; } }


    public STEMP_Wrapper(STEMP_Wrapper toCopy, bool copyAll) : this()
    {
        s = (toCopy.s != null) ? new STEMP_State(toCopy.s, copyAll) : null;
        r = (toCopy.r != null) ? new STEMP_Rate(toCopy.r, copyAll) : null;
        a = (toCopy.a != null) ? new STEMP_Auxiliary(toCopy.a, copyAll) : null;
        ex = (toCopy.ex != null) ? new STEMP_Exogenous(toCopy.ex, copyAll) : null;
        if (copyAll)
        {
            stemp_Component = (toCopy.stemp_Component != null) ? new STEMP_Component(toCopy.stemp_Component) : null;
        }
    }

    public void Init()
    {
        //stemp_Component.Init(s, r, a);
        loadParameters();
    }

    private void loadParameters()
    {
        stemp_Component.DLAYR = DLAYR;
        stemp_Component.SW = SW;
        stemp_Component.NLAYR = NLAYR;
        stemp_Component.LL = LL;
        stemp_Component.ISWWAT = ISWWAT;
        stemp_Component.DUL = DUL;
        stemp_Component.BD = BD;
        stemp_Component.DS = DS;
        stemp_Component.NL = NL;
        stemp_Component.XLAT = XLAT;
        stemp_Component.MSALB = MSALB;
    }

    public void EstimateSTEMP_(double TAVG, double TMAX, double TAV, double TAMP, int DOY, double SRAD)
    {
        ex.TAVG = TAVG;
        ex.TMAX = TMAX;
        ex.TAV = TAV;
        ex.TAMP = TAMP;
        ex.DOY = DOY;
        ex.SRAD = SRAD;
        stemp_Component.CalculateModel(s, s1, r, a, ex);
    }

}