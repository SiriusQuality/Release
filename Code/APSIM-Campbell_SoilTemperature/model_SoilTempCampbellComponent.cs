public class Model_SoilTempCampbellComponent
{
    
    /// <summary>
    /// Constructor of the Model_SoilTempCampbellComponent component")
    /// </summary>  
    public Model_SoilTempCampbellComponent() { }
    

    //Declaration of the associated strategies
    public Campbell _Campbell = new Campbell();

    public int NLAYR
    {
        get
        {
             return _Campbell.NLAYR; 
        }
        set
        {
            _Campbell.NLAYR = value;
        }
    }
    public double[] THICK
    {
        get
        {
             return _Campbell.THICK; 
        }
        set
        {
            _Campbell.THICK = value;
        }
    }
    public double[] BD
    {
        get
        {
             return _Campbell.BD; 
        }
        set
        {
            _Campbell.BD = value;
        }
    }
    public double[] SLCARB
    {
        get
        {
             return _Campbell.SLCARB; 
        }
        set
        {
            _Campbell.SLCARB = value;
        }
    }
    public double[] CLAY
    {
        get
        {
             return _Campbell.CLAY; 
        }
        set
        {
            _Campbell.CLAY = value;
        }
    }
    public double[] SLROCK
    {
        get
        {
             return _Campbell.SLROCK; 
        }
        set
        {
            _Campbell.SLROCK = value;
        }
    }
    public double[] SLSILT
    {
        get
        {
             return _Campbell.SLSILT; 
        }
        set
        {
            _Campbell.SLSILT = value;
        }
    }
    public double[] SLSAND
    {
        get
        {
             return _Campbell.SLSAND; 
        }
        set
        {
            _Campbell.SLSAND = value;
        }
    }
    public double[] SW
    {
        get
        {
             return _Campbell.SW; 
        }
        set
        {
            _Campbell.SW = value;
        }
    }
    public double CONSTANT_TEMPdepth
    {
        get
        {
             return _Campbell.CONSTANT_TEMPdepth; 
        }
        set
        {
            _Campbell.CONSTANT_TEMPdepth = value;
        }
    }
    public double TAV
    {
        get
        {
             return _Campbell.TAV; 
        }
        set
        {
            _Campbell.TAV = value;
        }
    }
    public double TAMP
    {
        get
        {
             return _Campbell.TAMP; 
        }
        set
        {
            _Campbell.TAMP = value;
        }
    }
    public double XLAT
    {
        get
        {
             return _Campbell.XLAT; 
        }
        set
        {
            _Campbell.XLAT = value;
        }
    }
    public double SALB
    {
        get
        {
             return _Campbell.SALB; 
        }
        set
        {
            _Campbell.SALB = value;
        }
    }
    public double instrumentHeight
    {
        get
        {
             return _Campbell.instrumentHeight; 
        }
        set
        {
            _Campbell.instrumentHeight = value;
        }
    }
    public string boundaryLayerConductanceSource
    {
        get
        {
             return _Campbell.boundaryLayerConductanceSource; 
        }
        set
        {
            _Campbell.boundaryLayerConductanceSource = value;
        }
    }
    public string netRadiationSource
    {
        get
        {
             return _Campbell.netRadiationSource; 
        }
        set
        {
            _Campbell.netRadiationSource = value;
        }
    }

    public void  CalculateModel(Model_SoilTempCampbellState s, Model_SoilTempCampbellState s1, Model_SoilTempCampbellRate r, Model_SoilTempCampbellAuxiliary a, Model_SoilTempCampbellExogenous ex)
    {
        _Campbell.CalculateModel(s,s1, r, a, ex);
    }
    
    public Model_SoilTempCampbellComponent(Model_SoilTempCampbellComponent toCopy): this() // copy constructor 
    {

            NLAYR = toCopy.NLAYR;
            
            for (int i = 0; i < 100; i++)
                { THICK = toCopy.THICK; }
    
            
            for (int i = 0; i < 100; i++)
                { BD = toCopy.BD; }
    
            
            for (int i = 0; i < 100; i++)
                { SLCARB = toCopy.SLCARB; }
    
            
            for (int i = 0; i < 100; i++)
                { CLAY = toCopy.CLAY; }
    
            
            for (int i = 0; i < 100; i++)
                { SLROCK = toCopy.SLROCK; }
    
            
            for (int i = 0; i < 100; i++)
                { SLSILT = toCopy.SLSILT; }
    
            
            for (int i = 0; i < 100; i++)
                { SLSAND = toCopy.SLSAND; }
    
            
            for (int i = 0; i < 100; i++)
                { SW = toCopy.SW; }
    
            CONSTANT_TEMPdepth = toCopy.CONSTANT_TEMPdepth;
            TAV = toCopy.TAV;
            TAMP = toCopy.TAMP;
            XLAT = toCopy.XLAT;
            SALB = toCopy.SALB;
            instrumentHeight = toCopy.instrumentHeight;
            boundaryLayerConductanceSource = toCopy.boundaryLayerConductanceSource;
            netRadiationSource = toCopy.netRadiationSource;
            }
        }