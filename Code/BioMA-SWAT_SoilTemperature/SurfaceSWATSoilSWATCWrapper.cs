using System;
using System.Collections.Generic;
using System.Linq;
public class SurfaceSWATSoilSWATCWrapper
{
    public SurfaceSWATSoilSWATCState s;
    public SurfaceSWATSoilSWATCState s1;
    public SurfaceSWATSoilSWATCRate r;
    public SurfaceSWATSoilSWATCAuxiliary a;
    public SurfaceSWATSoilSWATCExogenous ex;
    public SurfaceSWATSoilSWATCComponent surfaceswatsoilswatcComponent;

    public SurfaceSWATSoilSWATCWrapper()
    {
        s = new SurfaceSWATSoilSWATCState();
        r = new SurfaceSWATSoilSWATCRate();
        a = new SurfaceSWATSoilSWATCAuxiliary();
        ex = new SurfaceSWATSoilSWATCExogenous();
        surfaceswatsoilswatcComponent = new SurfaceSWATSoilSWATCComponent();
        loadParameters();
    }

        double LagCoefficient;

    public double SurfaceSoilTemperature{ get { return s.SurfaceSoilTemperature;}} 
     
    public double[] SoilTemperatureByLayers{ get { return s.SoilTemperatureByLayers;}} 
     

    public SurfaceSWATSoilSWATCWrapper(SurfaceSWATSoilSWATCWrapper toCopy, bool copyAll) : this()
    {
        s = (toCopy.s != null) ? new SurfaceSWATSoilSWATCState(toCopy.s, copyAll) : null;
        r = (toCopy.r != null) ? new SurfaceSWATSoilSWATCRate(toCopy.r, copyAll) : null;
        a = (toCopy.a != null) ? new SurfaceSWATSoilSWATCAuxiliary(toCopy.a, copyAll) : null;
        ex = (toCopy.ex != null) ? new SurfaceSWATSoilSWATCExogenous(toCopy.ex, copyAll) : null;
        if (copyAll)
        {
            surfaceswatsoilswatcComponent = (toCopy.surfaceswatsoilswatcComponent != null) ? new SurfaceSWATSoilSWATCComponent(toCopy.surfaceswatsoilswatcComponent) : null;
        }
    }

    public void Init(){
        //surfaceswatsoilswatcComponent.Init(s, r, a);
        loadParameters();
    }

    private void loadParameters()
    {
        surfaceswatsoilswatcComponent.LagCoefficient = LagCoefficient;
    }

    public void EstimateSurfaceSWATSoilSWATC(double AirTemperatureMinimum, double GlobalSolarRadiation, double WaterEquivalentOfSnowPack, double AirTemperatureMaximum, double Albedo, double AirTemperatureAnnualAverage)
    {
        ex.AirTemperatureMinimum = AirTemperatureMinimum;
        ex.GlobalSolarRadiation = GlobalSolarRadiation;
        ex.WaterEquivalentOfSnowPack = WaterEquivalentOfSnowPack;
        ex.AirTemperatureMaximum = AirTemperatureMaximum;
        ex.Albedo = Albedo;
        ex.AirTemperatureAnnualAverage = AirTemperatureAnnualAverage;
        surfaceswatsoilswatcComponent.CalculateModel(s,s1, r, a, ex);
    }

}