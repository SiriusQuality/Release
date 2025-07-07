public class STEMP_Component
{

    public STEMP_Component() { }


    //Declaration of the associated strategies
    public STEMP _STEMP = new STEMP();

    public double[] DLAYR
    {
        get
        {
            return _STEMP.DLAYR;
        }
        set
        {
            _STEMP.DLAYR = value;
        }
    }
    public double[] SW
    {
        get
        {
            return _STEMP.SW;
        }
        set
        {
            _STEMP.SW = value;
        }
    }
    public int NLAYR
    {
        get
        {
            return _STEMP.NLAYR;
        }
        set
        {
            _STEMP.NLAYR = value;
        }
    }
    public double[] LL
    {
        get
        {
            return _STEMP.LL;
        }
        set
        {
            _STEMP.LL = value;
        }
    }
    public string ISWWAT
    {
        get
        {
            return _STEMP.ISWWAT;
        }
        set
        {
            _STEMP.ISWWAT = value;
        }
    }
    public double[] DUL
    {
        get
        {
            return _STEMP.DUL;
        }
        set
        {
            _STEMP.DUL = value;
        }
    }
    public double[] BD
    {
        get
        {
            return _STEMP.BD;
        }
        set
        {
            _STEMP.BD = value;
        }
    }
    public double[] DS
    {
        get
        {
            return _STEMP.DS;
        }
        set
        {
            _STEMP.DS = value;
        }
    }
    public int NL
    {
        get
        {
            return _STEMP.NL;
        }
        set
        {
            _STEMP.NL = value;
        }
    }
    public double XLAT
    {
        get
        {
            return _STEMP.XLAT;
        }
        set
        {
            _STEMP.XLAT = value;
        }
    }
    public double MSALB
    {
        get
        {
            return _STEMP.MSALB;
        }
        set
        {
            _STEMP.MSALB = value;
        }
    }

    public void CalculateModel(STEMP_State s, STEMP_State s1, STEMP_Rate r, STEMP_Auxiliary a, STEMP_Exogenous ex)
    {
        _STEMP.CalculateModel(s, s1, r, a, ex);
    }

    public STEMP_Component(STEMP_Component toCopy) : this() // copy constructor 
    {


        for (int i = 0; i < 100; i++)
        { DLAYR = toCopy.DLAYR; }


        for (int i = 0; i < 100; i++)
        { SW = toCopy.SW; }

        NLAYR = toCopy.NLAYR;

        for (int i = 0; i < 100; i++)
        { LL = toCopy.LL; }

        ISWWAT = toCopy.ISWWAT;

        for (int i = 0; i < 100; i++)
        { DUL = toCopy.DUL; }


        for (int i = 0; i < 100; i++)
        { BD = toCopy.BD; }


        for (int i = 0; i < 100; i++)
        { DS = toCopy.DS; }

        NL = toCopy.NL;
        XLAT = toCopy.XLAT;
        MSALB = toCopy.MSALB;
    }
}