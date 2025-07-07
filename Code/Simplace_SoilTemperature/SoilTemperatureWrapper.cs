using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplace
{

    public class SoilTemperatureWrapper
    {
        public SoilTemperatureState s;
        public SoilTemperatureState s1;
        public SoilTemperatureRate r;
        public SoilTemperatureAuxiliary a;
        public SoilTemperatureExogenous ex;
        public SoilTemperatureComponent soiltemperatureComponent;

        public SoilTemperatureWrapper()
        {
            s = new SoilTemperatureState();
            r = new SoilTemperatureRate();
            a = new SoilTemperatureAuxiliary();
            ex = new SoilTemperatureExogenous();
            soiltemperatureComponent = new SoilTemperatureComponent();
            loadParameters();
        }

        double cCarbonContent;
        double[] cSoilLayerDepth = new double[100];
        double cFirstDayMeanTemp;
        double cAverageGroundTemperature;
        double cAverageBulkDensity;
        double cDampingDepth;

        public double SnowWaterContent { get { return s.SnowWaterContent; } }

        public double SoilSurfaceTemperature { get { return s.SoilSurfaceTemperature; } }

        public int AgeOfSnow { get { return s.AgeOfSnow; } }

        public double[] rSoilTempArrayRate { get { return s.rSoilTempArrayRate; } }

        public double[] SoilTempArray { get { return s.SoilTempArray; } }

        public double SnowIsolationIndex { get { return a.SnowIsolationIndex; } }


        public SoilTemperatureWrapper(SoilTemperatureWrapper toCopy, bool copyAll) : this()
        {
            s = (toCopy.s != null) ? new SoilTemperatureState(toCopy.s, copyAll) : null;
            r = (toCopy.r != null) ? new SoilTemperatureRate(toCopy.r, copyAll) : null;
            a = (toCopy.a != null) ? new SoilTemperatureAuxiliary(toCopy.a, copyAll) : null;
            ex = (toCopy.ex != null) ? new SoilTemperatureExogenous(toCopy.ex, copyAll) : null;
            if (copyAll)
            {
                soiltemperatureComponent = (toCopy.soiltemperatureComponent != null) ? new SoilTemperatureComponent(toCopy.soiltemperatureComponent) : null;
            }
        }

        public void Init()
        {
            //soiltemperatureComponent.Init(s, r, a);
            loadParameters();
        }

        private void loadParameters()
        {
            soiltemperatureComponent.cCarbonContent = cCarbonContent;
            soiltemperatureComponent.cSoilLayerDepth = cSoilLayerDepth;
            soiltemperatureComponent.cFirstDayMeanTemp = cFirstDayMeanTemp;
            soiltemperatureComponent.cAverageGroundTemperature = cAverageGroundTemperature;
            soiltemperatureComponent.cAVT = cAverageGroundTemperature;
            soiltemperatureComponent.cAverageBulkDensity = cAverageBulkDensity;
            soiltemperatureComponent.cABD = cAverageBulkDensity;
            soiltemperatureComponent.cDampingDepth = cDampingDepth;
        }

        public void EstimateSoilTemperature(double iAirTemperatureMax, double iTempMax, double iAirTemperatureMin, double iTempMin, double iGlobalSolarRadiation, double iRadiation, double iRAIN, double iCropResidues, double iPotentialSoilEvaporation, double iLeafAreaIndex, double[] iSoilTempArray, double iSoilWaterContent, double iSoilSurfaceTemperature)
        {
            ex.iAirTemperatureMax = iAirTemperatureMax;
            ex.iTempMax = iTempMax;
            ex.iAirTemperatureMin = iAirTemperatureMin;
            ex.iTempMin = iTempMin;
            ex.iGlobalSolarRadiation = iGlobalSolarRadiation;
            ex.iRadiation = iRadiation;
            ex.iRAIN = iRAIN;
            ex.iCropResidues = iCropResidues;
            ex.iPotentialSoilEvaporation = iPotentialSoilEvaporation;
            ex.iLeafAreaIndex = iLeafAreaIndex;
            ex.iSoilTempArray = iSoilTempArray;
            ex.iSoilWaterContent = iSoilWaterContent;
            ex.iSoilSurfaceTemperature = iSoilSurfaceTemperature;
            soiltemperatureComponent.CalculateModel(s, s1, r, a, ex);
        }
    }
}