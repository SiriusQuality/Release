using System;
using System.Collections.Generic;
public class STEMP_State
{
    private static int NL;
    private double[] _DSMID = new double[NL];
    private double[] _ST = new double[NL];
    private double _CUMDPT;
    private double[] _TMA = new double[5];
    private double _SRFTEMP;
    private double _ATOT;
    private double _HDAY;
    private double _TDL;

    public STEMP_State() { }


    public STEMP_State(STEMP_State toCopy, bool copyAll) // copy constructor 
    {
        if (copyAll)
        {

            DSMID = new double[NL];
            for (int i = 0; i < NL; i++)
            { DSMID[i] = toCopy.DSMID[i]; }

            ST = new double[NL];
            for (int i = 0; i < NL; i++)
            { ST[i] = toCopy.ST[i]; }

            CUMDPT = toCopy.CUMDPT;
            TDL = toCopy.TDL;
            TMA = new double[NL];
            for (int i = 0; i < NL; i++)
            { TMA[i] = toCopy.TMA[i]; }

            SRFTEMP = toCopy.SRFTEMP;
            HDAY = toCopy.HDAY;
            ATOT = toCopy.ATOT;
        }
    }
    public double[] DSMID
    {
        get { return this._DSMID; }
        set { this._DSMID = value; }
    }
    public double HDAY
    {
        get { return this._HDAY; }
        set { this._HDAY = value; }
    }
    public double[] ST
    {
        get { return this._ST; }
        set { this._ST = value; }
    }
    public double TDL
    {
        get { return this._TDL; }
        set { this._TDL = value; }
    }
    public double CUMDPT
    {
        get { return this._CUMDPT; }
        set { this._CUMDPT = value; }
    }
    public double[] TMA
    {
        get { return this._TMA; }
        set { this._TMA = value; }
    }
    public double SRFTEMP
    {
        get { return this._SRFTEMP; }
        set { this._SRFTEMP = value; }
    }
    public double ATOT
    {
        get { return this._ATOT; }
        set { this._ATOT = value; }
    }
}