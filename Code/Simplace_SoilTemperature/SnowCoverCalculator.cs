using System;
using System.Collections.Generic;
using System.Linq;
public class SnowCoverCalculator
{
    public void Init(SoilTemperatureState s, SoilTemperatureState s1, SoilTemperatureRate r, SoilTemperatureAuxiliary a, SoilTemperatureExogenous ex)
    {
        double iTempMax = ex.iAirTemperatureMax;
        double iTempMin = ex.iAirTemperatureMin;
        double iRadiation = ex.iRadiation;
        double iRAIN;
        double iCropResidues;
        double iPotentialSoilEvaporation;
        double iLeafAreaIndex;
        double[] iSoilTempArray;
        double Albedo;
        double SnowWaterContent = 0.0;
        double SoilSurfaceTemperature = 0.0;
        int AgeOfSnow = 0;
        Albedo = 0.00d;
        double TMEAN;
        double TAMPL;
        double DST;
        //Ajouter la condition que si Albedo est initialis�e, alors pas de calcul � faire
        if (s.Albedo == 0 || s.Albedo == null) Albedo = 0.02260d * Math.Log(cCarbonContent, 10) + 0.15020d;
        else Albedo = s.Albedo;
        //Albedo = 0.02260d * Math.Log(cCarbonContent, 10) + 0.15020d;
        TMEAN = 0.50d * (iTempMax + iTempMin);
        TAMPL = 0.50d * (iTempMax - iTempMin);
        DST = TMEAN + (TAMPL * (iRadiation * (1 - Albedo) - 14) / 20);
        SoilSurfaceTemperature = DST;
        s.Albedo = Albedo;
        s.SnowWaterContent = SnowWaterContent;
        s.SoilSurfaceTemperature = SoilSurfaceTemperature;
        s.AgeOfSnow = AgeOfSnow;
    }
    private double _cCarbonContent;
    public double cCarbonContent
    {
        get { return this._cCarbonContent; }
        set { this._cCarbonContent = value; }
    }
    public SnowCoverCalculator() { }

    public void CalculateModel(SoilTemperatureState s, SoilTemperatureState s1, SoilTemperatureRate r, SoilTemperatureAuxiliary a, SoilTemperatureExogenous ex)
    {
        //- Name: SnowCoverCalculator -Version: 001, -Time step: 1
        //- Description:
        //            * Title: SnowCoverCalculator model
        //            * Authors: Gunther Krauss
        //            * Reference: ('http://www.simplace.net/doc/simplace_modules/',)
        //            * Institution: INRES Pflanzenbau, Uni Bonn
        //            * ExtendedDescription: as given in the documentation
        //            * ShortDescription: None
        //- inputs:
        //            * name: cCarbonContent
        //                          ** description : Carbon content of upper soil layer
        //                          ** inputtype : parameter
        //                          ** parametercategory : constant
        //                          ** datatype : DOUBLE
        //                          ** max : 20.0
        //                          ** min : 0.0
        //                          ** default : 0.5
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/percent
        //            * name: iTempMax
        //                          ** description : Daily maximum air temperature
        //                          ** inputtype : variable
        //                          ** variablecategory : exogenous
        //                          ** datatype : DOUBLE
        //                          ** max : 50.0
        //                          ** min : -40.0
        //                          ** default : 
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
        //            * name: iTempMin
        //                          ** description : Daily minimum air temperature
        //                          ** inputtype : variable
        //                          ** variablecategory : exogenous
        //                          ** datatype : DOUBLE
        //                          ** max : 50.0
        //                          ** min : -40.0
        //                          ** default : 
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
        //            * name: iRadiation
        //                          ** description : Global Solar radiation
        //                          ** inputtype : variable
        //                          ** variablecategory : exogenous
        //                          ** datatype : DOUBLE
        //                          ** max : 2000.0
        //                          ** min : 0.0
        //                          ** default : 
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/megajoule_per_square_metre
        //            * name: iRAIN
        //                          ** description : Rain amount
        //                          ** inputtype : variable
        //                          ** variablecategory : exogenous
        //                          ** datatype : DOUBLE
        //                          ** max : 60.0
        //                          ** min : 0.0
        //                          ** default : 
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre
        //            * name: iCropResidues
        //                          ** description : Crop residues plus above ground biomass
        //                          ** inputtype : variable
        //                          ** variablecategory : exogenous
        //                          ** datatype : DOUBLE
        //                          ** max : 20000.0
        //                          ** min : 0.0
        //                          ** default : 
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/gram_per_square_metre
        //            * name: iPotentialSoilEvaporation
        //                          ** description : Potenial Evaporation
        //                          ** inputtype : variable
        //                          ** variablecategory : exogenous
        //                          ** datatype : DOUBLE
        //                          ** max : 12.0
        //                          ** min : 0.0
        //                          ** default : 
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre
        //            * name: iLeafAreaIndex
        //                          ** description : Leaf area index
        //                          ** inputtype : variable
        //                          ** variablecategory : exogenous
        //                          ** datatype : DOUBLE
        //                          ** max : 10.0
        //                          ** min : 0.0
        //                          ** default : 
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/square_metre_per_square_metre
        //            * name: iSoilTempArray
        //                          ** description : Soil Temp array of last day
        //                          ** inputtype : variable
        //                          ** variablecategory : exogenous
        //                          ** datatype : DOUBLEARRAY
        //                          ** len : 
        //                          ** max : 35.0
        //                          ** min : -15.0
        //                          ** default : 
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
        //            * name: Albedo
        //                          ** description : Albedo
        //                          ** inputtype : variable
        //                          ** variablecategory : state
        //                          ** datatype : DOUBLE
        //                          ** max : 1.0
        //                          ** min : 0.0
        //                          ** default : 
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/one
        //            * name: SnowWaterContent
        //                          ** description : Snow water content
        //                          ** inputtype : variable
        //                          ** variablecategory : state
        //                          ** datatype : DOUBLE
        //                          ** max : 1500.0
        //                          ** min : 0.0
        //                          ** default : 0.0
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre
        //            * name: SoilSurfaceTemperature
        //                          ** description : Soil surface temperature
        //                          ** inputtype : variable
        //                          ** variablecategory : state
        //                          ** datatype : DOUBLE
        //                          ** max : 70.0
        //                          ** min : -40.0
        //                          ** default : 0.0
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
        //            * name: AgeOfSnow
        //                          ** description : Age of snow
        //                          ** inputtype : variable
        //                          ** variablecategory : state
        //                          ** datatype : INT
        //                          ** max : 
        //                          ** min : 0
        //                          ** default : 0
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/one
        //- outputs:
        //            * name: SnowWaterContent
        //                          ** description : Snow water content
        //                          ** datatype : DOUBLE
        //                          ** variablecategory : state
        //                          ** max : 1500.0
        //                          ** min : 0.0
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/millimetre
        //            * name: SoilSurfaceTemperature
        //                          ** description : Soil surface temperature
        //                          ** datatype : DOUBLE
        //                          ** variablecategory : state
        //                          ** max : 70.0
        //                          ** min : -40.0
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/degree_Celsius
        //            * name: AgeOfSnow
        //                          ** description : Age of snow
        //                          ** datatype : INT
        //                          ** variablecategory : state
        //                          ** max : 
        //                          ** min : 0
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/one
        //            * name: SnowIsolationIndex
        //                          ** description : Snow isolation index
        //                          ** datatype : DOUBLE
        //                          ** variablecategory : auxiliary
        //                          ** max : 1.0
        //                          ** min : 0.0
        //                          ** unit : http://www.wurvoc.org/vocabularies/om-1.8/one
        double iTempMax = ex.iTempMax;
        double iTempMin = ex.iTempMin;
        double iRadiation = ex.iRadiation;
        double iRAIN = ex.iRAIN;
        double iCropResidues = ex.iCropResidues;
        double iPotentialSoilEvaporation = ex.iPotentialSoilEvaporation;
        double iLeafAreaIndex = ex.iLeafAreaIndex;
        double[] iSoilTempArray = ex.iSoilTempArray;
        double Albedo = s.Albedo;
        double SnowWaterContent = s.SnowWaterContent;
        double SoilSurfaceTemperature = s.SoilSurfaceTemperature;
        int AgeOfSnow = s.AgeOfSnow;
        double SnowIsolationIndex;
        double tiCropResidues;
        double tiSoilTempArray;
        double TMEAN;
        double TAMPL;
        double DST;
        double tSoilSurfaceTemperature;
        double tSnowIsolationIndex;
        double SNOWEVAPORATION;
        double SNOWMELT;
        double EAJ;
        double ageOfSnowFactor;
        double SNPKT;
        tiCropResidues = iCropResidues * 10.00d;
        tiSoilTempArray = iSoilTempArray[0];
        TMEAN = 0.50d * (iTempMax + iTempMin);
        TAMPL = 0.50d * (iTempMax - iTempMin);
        DST = TMEAN + (TAMPL * (iRadiation * (1 - Albedo) - 14) / 20);
        if (iRAIN > (double)(0) && (tiSoilTempArray < (double)(1) || (SnowWaterContent > (double)(3) || SoilSurfaceTemperature < (double)(0))))
        {
            //Do not take care of the rain
            //SnowWaterContent = SnowWaterContent + iRAIN;
            SnowWaterContent = SnowWaterContent;
        }
        tSnowIsolationIndex = 1.00d;
        if (tiCropResidues < (double)(10))
        {
            tSnowIsolationIndex = tiCropResidues / (tiCropResidues + Math.Exp(5.340d - (2.40d * tiCropResidues)));
        }
        if (SnowWaterContent < 1E-10)
        {
            tSnowIsolationIndex = tSnowIsolationIndex * 0.850d;
            tSoilSurfaceTemperature = 0.50d * (DST + ((1 - tSnowIsolationIndex) * DST) + (tSnowIsolationIndex * tiSoilTempArray));
        }
        else
        {
            tSnowIsolationIndex = Math.Max(SnowWaterContent / (SnowWaterContent + Math.Exp(0.470d - (0.620d * SnowWaterContent))), tSnowIsolationIndex);
            //New value of factor
            //tSnowIsolationIndex = Math.Max(SnowWaterContent / (SnowWaterContent + Math.Exp(2.3 - (0.22 * SnowWaterContent))), tSnowIsolationIndex);
            tSoilSurfaceTemperature = (1 - tSnowIsolationIndex) * DST + (tSnowIsolationIndex * tiSoilTempArray);
        }
        if (SnowWaterContent == (double)(0) && !(iRAIN > (double)(0) && tiSoilTempArray < (double)(1)))
        {
            SnowWaterContent = (double)(0);
        }
        else
        {
            EAJ = .50d;
            if (SnowWaterContent < (double)(5))
            {
                EAJ = Math.Exp(-Math.Max((0.40d * iLeafAreaIndex), (0.10d * (tiCropResidues + 0.10d))));
            }
            SNOWEVAPORATION = iPotentialSoilEvaporation * EAJ;
            ageOfSnowFactor = AgeOfSnow / (AgeOfSnow + Math.Exp(5.340d - (2.3950d * AgeOfSnow)));
            SNPKT = .33330d * (2 * Math.Min(tSoilSurfaceTemperature, tiSoilTempArray) + iTempMax);
            if (TMEAN > (double)(0))
            {
                SNOWMELT = Math.Max(0, Math.Sqrt(iTempMax * iRadiation) * (1.520d + (.540d * ageOfSnowFactor * SNPKT)));
            }
            else
            {
                SNOWMELT = (double)(0);
            }
            if (SNOWMELT + SNOWEVAPORATION > SnowWaterContent)
            {
                SNOWMELT = SNOWMELT / (SNOWMELT + SNOWEVAPORATION) * SnowWaterContent;
                SNOWEVAPORATION = SNOWEVAPORATION / (SNOWMELT + SNOWEVAPORATION) * SnowWaterContent;
            }
            SnowWaterContent = SnowWaterContent - (SNOWMELT + SNOWEVAPORATION);
            if (SnowWaterContent < (double)(0))
            {
                SnowWaterContent = (double)(0);
            }
            if (SnowWaterContent < (double)(5))
            {
                AgeOfSnow = 0;
            }
            else
            {
                AgeOfSnow = AgeOfSnow + 1;
            }
        }
        SnowIsolationIndex = tSnowIsolationIndex;
        SoilSurfaceTemperature = tSoilSurfaceTemperature;
        s.SnowWaterContent = SnowWaterContent;
        s.SoilSurfaceTemperature = SoilSurfaceTemperature;
        s.AgeOfSnow = AgeOfSnow;
        a.SnowIsolationIndex = SnowIsolationIndex;
    }
}