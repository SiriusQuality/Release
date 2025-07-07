public class STEMP_EPIC_Component
{
    
        public STEMP_EPIC_Component() { }
    

    //Declaration of the associated strategies
    public STEMP_EPIC _STEMP_EPIC = new STEMP_EPIC();

    public double[] BD
    {
        get
        {
             return _STEMP_EPIC.BD; 
        }
        set
        {
            _STEMP_EPIC.BD = value;
        }
    }
    public double[] DUL
    {
        get
        {
             return _STEMP_EPIC.DUL; 
        }
        set
        {
            _STEMP_EPIC.DUL = value;
        }
    }
    public double[] DS
    {
        get
        {
             return _STEMP_EPIC.DS; 
        }
        set
        {
            _STEMP_EPIC.DS = value;
        }
    }
    public double[] DLAYR
    {
        get
        {
             return _STEMP_EPIC.DLAYR; 
        }
        set
        {
            _STEMP_EPIC.DLAYR = value;
        }
    }
    public double[] LL
    {
        get
        {
             return _STEMP_EPIC.LL; 
        }
        set
        {
            _STEMP_EPIC.LL = value;
        }
    }
    public double[] SW
    {
        get
        {
             return _STEMP_EPIC.SW; 
        }
        set
        {
            _STEMP_EPIC.SW = value;
        }
    }
    public int NLAYR
    {
        get
        {
             return _STEMP_EPIC.NLAYR; 
        }
        set
        {
            _STEMP_EPIC.NLAYR = value;
        }
    }
    public int NL
    {
        get
        {
             return _STEMP_EPIC.NL; 
        }
        set
        {
            _STEMP_EPIC.NL = value;
        }
    }
    public string ISWWAT
    {
        get
        {
             return _STEMP_EPIC.ISWWAT; 
        }
        set
        {
            _STEMP_EPIC.ISWWAT = value;
        }
    }

    public void  CalculateModel(STEMP_EPIC_State s, STEMP_EPIC_State s1, STEMP_EPIC_Rate r, STEMP_EPIC_Auxiliary a, STEMP_EPIC_Exogenous ex)
    {
        _STEMP_EPIC.CalculateModel(s,s1, r, a, ex);
    }
    
    public STEMP_EPIC_Component(STEMP_EPIC_Component toCopy): this() // copy constructor 
    {

        
            for (int i = 0; i < 100; i++)
            { BD = toCopy.BD; }
    
        
            for (int i = 0; i < 100; i++)
            { DUL = toCopy.DUL; }
    
        
            for (int i = 0; i < 100; i++)
            { DS = toCopy.DS; }
    
        
            for (int i = 0; i < 100; i++)
            { DLAYR = toCopy.DLAYR; }
    
        
            for (int i = 0; i < 100; i++)
            { LL = toCopy.LL; }
    
        
            for (int i = 0; i < 100; i++)
            { SW = toCopy.SW; }
    
        NLAYR = toCopy.NLAYR;
        NL = toCopy.NL;
        ISWWAT = toCopy.ISWWAT;
    }
}