using System;
using System.Collections.Generic;
using System.Linq;
public class SurfacePartonSoilSWATCWrapper
{
    public SurfacePartonSoilSWATCState s;
    public SurfacePartonSoilSWATCState s1;
    public SurfacePartonSoilSWATCRate r;
    public SurfacePartonSoilSWATCAuxiliary a;
    public SurfacePartonSoilSWATCExogenous ex;
    public SurfacePartonSoilSWATCComponent surfacepartonsoilswatcComponent;

    public SurfacePartonSoilSWATCWrapper()
    {
        s = new SurfacePartonSoilSWATCState();
        r = new SurfacePartonSoilSWATCRate();
        a = new SurfacePartonSoilSWATCAuxiliary();
        ex = new SurfacePartonSoilSWATCExogenous();
        surfacepartonsoilswatcComponent = new SurfacePartonSoilSWATCComponent();
        loadParameters();
    }

        double LagCoefficient;

    public double SurfaceSoilTemperature{ get { return s.SurfaceSoilTemperature;}} 
     
    public double[] SoilTemperatureByLayers{ get { return s.SoilTemperatureByLayers;}} 
     
    public double SurfaceTemperatureMaximum{ get { return a.SurfaceTemperatureMaximum;}} 
     
    public double SurfaceTemperatureMinimum{ get { return a.SurfaceTemperatureMinimum;}} 
     

    public SurfacePartonSoilSWATCWrapper(SurfacePartonSoilSWATCWrapper toCopy, bool copyAll) : this()
    {
        s = (toCopy.s != null) ? new SurfacePartonSoilSWATCState(toCopy.s, copyAll) : null;
        r = (toCopy.r != null) ? new SurfacePartonSoilSWATCRate(toCopy.r, copyAll) : null;
        a = (toCopy.a != null) ? new SurfacePartonSoilSWATCAuxiliary(toCopy.a, copyAll) : null;
        ex = (toCopy.ex != null) ? new SurfacePartonSoilSWATCExogenous(toCopy.ex, copyAll) : null;
        if (copyAll)
        {
            surfacepartonsoilswatcComponent = (toCopy.surfacepartonsoilswatcComponent != null) ? new SurfacePartonSoilSWATCComponent(toCopy.surfacepartonsoilswatcComponent) : null;
        }
    }

    public void Init(){
        //surfacepartonsoilswatcComponent.Init(s, r, a);
        loadParameters();
    }

    private void loadParameters()
    {
        surfacepartonsoilswatcComponent.LagCoefficient = LagCoefficient;
    }

    public void EstimateSurfacePartonSoilSWATC(double GlobalSolarRadiation, double AirTemperatureMinimum, double DayLength, double AirTemperatureMaximum, double AirTemperatureAnnualAverage)
    {
        ex.GlobalSolarRadiation = GlobalSolarRadiation;
        ex.AirTemperatureMinimum = AirTemperatureMinimum;
        ex.DayLength = DayLength;
        ex.AirTemperatureMaximum = AirTemperatureMaximum;
        ex.AirTemperatureAnnualAverage = AirTemperatureAnnualAverage;
        surfacepartonsoilswatcComponent.CalculateModel(s,s1, r, a, ex);
    }

}