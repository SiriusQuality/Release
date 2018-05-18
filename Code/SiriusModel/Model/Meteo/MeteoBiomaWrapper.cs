using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiriusQualityMeteo;
using CRA.Clima.AirT.Interfaces;
using CRA.Clima.AirT.Strategies;


namespace SiriusModel.Model.Meteo
{
    public class MeteoBiomaWrapper : UniverseLink
    {

        #region output

        public double DayLength { get { return meteoState.dayLength; } }
        public double Photoperiod { get { return meteoState.photoperiod; } }
        public double RadTopAtm { get { return meteoState.radTopAtm; } }
        ///<summary>
        ///Slope of saturated vapour pressure temperature curve at temperature T (hPa °C-1)
        ///</summary>
        public double HSlope { get { return meteoState.HSlope; } }

        ///<summary>
        ///saturatation vapour pressure of water vapour at temperature T
        ///UNIT: hPa
        ///</summary>
        public double SaturationVaporPressure { get { return meteoState.saturationVaporPressure; } }
        public double VPDair { get { return meteoState.VPDair; } }//hPa
        public double VPDairCanopy { get { return meteoState.VPDairCanopy; } }//hPa

        public double DailyMaxWindSpeed { get { return meteoState.dailyMaxWindSpeed; } } //m/d
        public double DailyMinWindSpeed { get { return meteoState.dailyMinWindSpeed; } } //m/d

        /// <summary>
        /// hourly outputs for the Maize model
        /// </summary>
        //Debug
        //public double[] HourlyAirTemperatureOld { get { return meteoState.hourlyAirTemperature; } }
        //public double[] HourlyAirTemperature { get { return airTState.AirTemperatureHourly; } }
        public double[] HourlyAirTemperature { get { return meteoState.hourlyAirTemperature; } }
        //Debug
        public double[] RH { get { return meteoState.RH; } }
        public double[] HourlyVPDAir { get { return meteoState.hourlyVPDAir; } }//hPa !!!!!! Maize kPa !!!!!!!
        public double[] HourlyRadiation { get { return meteoState.hourlyRadiation; } }
        public double[] HourlySolarElevation { get { return meteoState.hourlySolarElevation; } }//radians
        public double[] HourlyDirIrradiance { get { return meteoState.hourlyIdir; }}//MJ.h-1.m-2
        public double[] HourlyDiffIrradiance { get { return meteoState.hourlyIdiff; }}//MJ.h-1.m-2
        public double[] HourlyWindSpeed { get { return meteoState.hourlyWindSpeed; } } //m/d
        
        #endregion


        public MeteoBiomaWrapper(Universe universe)
            : base(universe)
        {
            meteoComponent = new SiriusQualityMeteo.Strategies.Meteorology();
            meteoState = new SiriusQualityMeteo.MeteoState();

           // airTComponent = new CRA.Clima.AirT.Strategies.HATGoudriaanVanLaar();
           // airTState =new CRA.Clima.AirT.Interfaces.AirTData();

            loadParameters();
        }

        public MeteoBiomaWrapper(Universe universe, MeteoBiomaWrapper toCopy, bool copyAll)
            : base(universe)
        {
            meteoState = (toCopy.meteoState != null) ? new SiriusQualityMeteo.MeteoState(toCopy.meteoState, copyAll) : null;

            //airTComponent = (toCopy.airTComponent != null) ? new CRA.Clima.AirT.Strategies.HATGoudriaanVanLaar() : null;
            //airTState = (toCopy.airTState != null) ? new CRA.Clima.AirT.Interfaces.AirTData() : null;


            //airTState.AirTemperatureMax = toCopy.airTState.AirTemperatureMax;
            //airTState.AirTemperatureMin = toCopy.airTState.AirTemperatureMin;
            //airTState.DayLength = toCopy.airTState.DayLength;
            //airTState.HourSunrise = toCopy.airTState.HourSunrise;

            //airTComponent.GoudriaanVanLaarDelayOfAirTemperatureMax = toCopy.airTComponent.GoudriaanVanLaarDelayOfAirTemperatureMax;
            //airTComponent.GoudriaanVanLaarNocturnalTimeCoefficient = toCopy.airTComponent.GoudriaanVanLaarDelayOfAirTemperatureMax;
            //airTComponent.GoudriaanVanLaarAirTemperatureMaxDayBefore = toCopy.airTComponent.GoudriaanVanLaarAirTemperatureMaxDayBefore;
            //airTComponent.GoudriaanVanLaarAirTemperatureMinDayAfter = toCopy.airTComponent.GoudriaanVanLaarAirTemperatureMinDayAfter;

            if (copyAll)
            {
                meteoComponent = (toCopy.meteoComponent != null) ? new SiriusQualityMeteo.Strategies.Meteorology(toCopy.meteoComponent) : null;
            }
        }

        public void EstimateMeteo(double meanTair, double minTair,double nextMinTair, double maxTair, double previousMaxTair, double minTshoot, double maxTshoot, double vp, double solarRadiation, double winspeed)
        {
            meteoState.calculateHourly = SwitchMaize ? 1 : 0; ;
            meteoState.minTair = minTair;
            meteoState.maxTair = maxTair;
            meteoState.meanTair = meanTair;
            meteoState.minShootTemperature = minTshoot;
            meteoState.maxShootTemperature = maxTshoot;
            meteoState.dayOfYear = CurrentDate.DayOfYear;
            meteoState.vaporPressure = vp;
            meteoState.solarRadiation = solarRadiation;
            meteoState.dailyMeanWindSpeed = winspeed;//m/d

            meteoComponent.Estimate(meteoState);

            //airTState.AirTemperatureMax = maxTair;
            //airTState.AirTemperatureMin = minTair;
            //airTState.DayLength = meteoState.dayLength;
            //for (int ihour = 0; ihour < 24; ihour++) { if (meteoState.hourlySolarElevation[ihour] > 0.0) { airTState.HourSunrise = ihour; break; } }

            //airTComponent.GoudriaanVanLaarAirTemperatureMaxDayBefore = previousMaxTair;
            //airTComponent.GoudriaanVanLaarAirTemperatureMinDayAfter=nextMinTair;

            ///airTComponent.ResetOutputs(airTState);
            //airTComponent.Estimate(airTState);

        }

        private void loadParameters()
        {
            meteoComponent.latitude = Latitude;
            //Israele (Givaat-Brener)
            meteoComponent.hourOfBlowingBeginingT1 = 1;
            meteoComponent.hourOfBlowingStopT3 = 2;
            meteoComponent.nightTimeWindFactor = 0.0025;

            //California (San Joaquin Valley)
            //meteoComponent.hourOfBlowingBeginingT1 = 1;
            //meteoComponent.hourOfBlowingStopT3 = 0;
            //meteoComponent.nightTimeWindFactor = 0.0080;

            //The Netherlands (De Kooy)
            //meteoComponent.hourOfBlowingBeginingT1 = 1;
            //meteoComponent.hourOfBlowingStopT3 = 2;
            //meteoComponent.nightTimeWindFactor = 0.0060;

            //airTComponent.GoudriaanVanLaarDelayOfAirTemperatureMax=1.5;
            //airTComponent.GoudriaanVanLaarNocturnalTimeCoefficient=4.0;
        }

        private SiriusQualityMeteo.Strategies.Meteorology meteoComponent;
        private SiriusQualityMeteo.MeteoState meteoState;
        //private CRA.Clima.AirT.Strategies.HATGoudriaanVanLaar airTComponent;
        //private CRA.Clima.AirT.Interfaces.AirTData airTState;

    }

}
