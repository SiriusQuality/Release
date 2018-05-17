using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiriusModel.Structure;

namespace SiriusModel.Model.CropModel.Phytomers
{
    class calculateLER: UniverseLink
    {
        public calculateLER(Universe universe): base (universe)
        {
            hRadn = new Vector<double>();
            SVP = new Vector<double>();
            RH = new Vector<double>();
            VPDair = new Vector<double>();
            TLeaf = new Vector<double>();
            VPDairLeaf = new Vector<double>();
            VPDeq = new Vector<double>(); 
            hLER = new Vector<double>();
        }

        public double calcLER(double[] hourlyTemperature, double[] deltaTTCanopyHourly, double Tmax, double Tmin, double radN, double sumTTFromSowing, double profileSwAvailRatio, double calc3cmWC)
        {
            //clear LER variables
            hRadn.Clear();
            SVP.Clear();
            RH.Clear();
            VPDair.Clear();
            TLeaf.Clear();
            VPDairLeaf.Clear();
            VPDeq.Clear();
            hLER.Clear();

            // daily calculation of elongation rate given environment and genetic factors
            // LER = (Tleaf - T0)(A + b * VPDair-leaf -c * T)
            // need hourly values

            calcHourlyWeatherVars(Latitude, base.CurrentDate.DayOfYear, radN, Tmax, Tmin, hourlyTemperature);

            // double psi;
            double psi = CalcPsi(profileSwAvailRatio);

            // -------------------------------------------------------
            // [Seb L.] :

            // calculate TLeaf + SEB L. ==> Tapex != Tair !!!
            // The same CalcTLeaf is used to get the impact of SWC in the first layer !

            // With this we define the number of leaves (tip) of today : 
            double Ntip = atip * sumTTFromSowing + Leaf_tip_emerg; /////////////// /////////////////////LEAFNUMBER

            // We calculate with a function based also on the number of leaves : 
            CalcTApex(hourlyTemperature, calc3cmWC, Ntip);

            // calculate VPDair-leaf with final TLeaf corrected :
            CalcVPDairLeaf(hourlyTemperature);

            // calculate VPDeq
            CalcVPDeq();

            // calculate hLER
            CalcHLER(deltaTTCanopyHourly, LERa, LERb, LERc, psi);

            double dailyLER = 0;
            for (int i = 0; i < 24; i++ )
            {
                dailyLER += hLER[i];
            }

            return dailyLER / 24.0;
        }

        #region hourly wheater

        private void calcHourlyWeatherVars(double latitude, int doy, double radn, double maxT, double minT, double[] hourlyTemperature)
        {
            double LatR = Math.PI /180.0 * latitude;      // convert latitude (degrees) to radians

            // disaggregate radiation
            CalcRadiation(radn, doy, LatR);

            // hourly SVP
            CalcHourlySVP(hourlyTemperature);

            // hourly relative humidity
            // if daily max and min RH is unavailable, use -1
            double maxRH = -1;
            double minRH = -1;
            CalcRH(maxT, minT, maxRH, minRH);

            // hourly air VPD
            CalcVPDair(hourlyTemperature);
	    }

        private void CalcRadiation(double ApsimRad, int doy, double LatR)
        {
            // some constants
            double RATIO = 0.75; //Hammer, Wright (Aust. J. Agric. Res., 1994, 45)

            // some calculations
            double SolarDec = CalcSolarDeclination(doy);
            double DayL = CalcDayLength(LatR, SolarDec);            // day length (radians)
            double DayLH = (2.0 / 15.0 * DayL) * (180 / Math.PI);             // day length (hours)
            double Solar = CalcSolarRadn(RATIO, DayL, LatR, SolarDec); // solar radiation

            //do the radiation calculation zeroing at dawn
            for (int i = 0; i < 24; i++) hRadn.Add(0.0);

            double DuskDawnFract = (DayLH - (int)DayLH) / 2; //the remainder part of the hour at dusk and dawn
            double DawnTime = 12 - (DayLH / 2);

            //   DawnTime = (180 - RadToDeg(acos(-1 * tan(LatR) * tan(SolarDec)))) / 360 * 24; //Wikipedia ???

            //The first partial hour
            hRadn[(int)DawnTime] += (GlobalRadiation(DuskDawnFract / DayLH, LatR, SolarDec, DayLH, Solar) * 3600 * DuskDawnFract);
            //Add the next lot
            for (int i = 0; i < (int)(DayLH - 1); i++)
            {
                hRadn[(int)DawnTime + i + 1] += (GlobalRadiation((DuskDawnFract / DayLH) + ((double)(i + 1) * 1.0 / (double)((int)DayLH)), LatR, SolarDec, DayLH, Solar) * 3600);
            }
            //Add the last one
            hRadn[(int)DawnTime + (int)DayLH + 1] += (GlobalRadiation(1, LatR, SolarDec, DayLH, Solar) * 3600 * DuskDawnFract);

            double TotalRad = 0;
            for (int i = 0; i < 24; i++) TotalRad += hRadn[i];
            for (int i = 0; i < 24; i++) hRadn[i] = hRadn[i] / TotalRad * ApsimRad;
        }

        private double CalcSolarDeclination(int doy)
        {
            return (23.45 * (Math.PI / 180)) * Math.Sin(2 * Math.PI * (284.0 + doy) / 365.0);
        }

        private double CalcDayLength(double LatR, double SolarDec)
        {
            return Math.Acos(-Math.Tan(LatR) * Math.Tan(SolarDec));
        }

        private double CalcSolarRadn(double RATIO, double DayL, double LatR, double SolarDec)
        {
            return (24.0 * 3600.0 * 1360.0 * (DayL * Math.Sin(LatR) * Math.Sin(SolarDec) +
            Math.Cos(LatR) * Math.Cos(SolarDec) * Math.Sin(DayL)) / (Math.PI * 1000000.0)) * RATIO;
        }

        private double GlobalRadiation(double oTime, double latitude, double SolarDec, double DayLH, double Solar)
        {
            double sunAngle = 0.0;
            double ITot = 0.0;
            double IDiff = 0.0;
            double IDir = 0.0;
            GlobalRadiation(oTime, latitude, SolarDec, DayLH, Solar, ref sunAngle, ref ITot, ref IDiff, ref IDir);
            return IDir;
        }

        private void GlobalRadiation(double oTime, double latitude, double SolarDec, double DayLH, double Solar,
                                ref double sunAngle, ref double ITot, ref double IDiff, ref double IDir)
        {
            sunAngle = Math.Asin(Math.Sin(latitude) * Math.Sin(SolarDec) +
            Math.Cos(latitude) * Math.Cos(SolarDec) * Math.Cos((Math.PI / 12.0) * DayLH * (oTime - 0.5))); //global variable
            ITot = Solar * (1.0 + Math.Sin(2.0 * Math.PI * oTime + 1.5 * Math.PI)) / (DayLH * 60.0 * 60.0); //global variable
            IDiff = 0.17 * 1360.0 * Math.Sin(sunAngle) / 1000000.0; //global variable
            if (IDiff > ITot)
            {
                IDiff = ITot;
            }
            IDir = ITot - IDiff; //global variable
        }

        private void CalcHourlySVP(double[] hourlytemperature)
        {
            // calculates SVP at the air temperature
            for (int i = 0; i < 24; i++)
                SVP.Add(6.1078 * Math.Exp(17.269 * hourlytemperature[i] / (237.3 + hourlytemperature[i])) * 0.1);
        }

        private void CalcRH(double tMax, double tMin, double RHMax, double RHMin)
        {
            // calculate relative humidity
            double wP;
            if (RHMax < 0.0 || RHMin < 0.0)
                wP = CalcSVP(tMin) / 100 * 1000 * 90;         // svp at Tmin
            else wP = (CalcSVP(tMin) / 100 * 1000 * RHMax + CalcSVP(tMax) / 100 * 1000 * RHMin) / 2.0;
            for (int i = 0; i < 24; i++)
            {
                RH.Add(wP / (10 * SVP[i]));
            }

        }
        private double CalcSVP(double TAir)
        {
            // calculates SVP at the air temperature
            return 6.1078 * Math.Exp(17.269 * TAir / (237.3 + TAir)) * 0.1;
        }

        private void CalcVPDair(double[] hourlyTemperature)
        {
            for (int i = 0; i < 24; i++)
                VPDair.Add(0.6106 * (Math.Exp((17.27 * hourlyTemperature[i]) / (hourlyTemperature[i] + 237.3)) - RH[i] / 100
                * Math.Exp((17.27 * hourlyTemperature[i]) / (hourlyTemperature[i] + 237.3))));
        }

        private Vector<double> hRadn;
        private Vector<double> SVP;
        private Vector<double> RH;
        private Vector<double> VPDair;

        #endregion

        private double CalcPsi(double ftsw)
        {
            if (ftsw < 0.001) return -1.5;
            return Math.Min(-0.1, -0.0578 + 0.246 * Math.Log(ftsw));
        }

        // New function to Calculate Tapex for leaf before applying stress (S/D ratio effect on Tleaf) :
        private void CalcTApex(double[] hourlyTemperature, double Wc_3cm, double Ntip)
        {
            // declare new parameters:
            double a, b, c, d, e;
            // Set the parameters value depending on the stage of the crop (up till leaf 8): 
            if (Ntip >= 8)
            { a = 6.49; b = 0.77; c = 0.01; d = -0.48; e = -0.22; }
            else
            { a = 1.18; b = 1; c = 0.01; d = -1.65; e = 0; }
            // Now loop over the hours to define Tapex !
            for (int i = 0; i < 24; i++)
            {
                double Tcalc = a + b * hourlyTemperature[i] + c * VPDair[i] + d * hRadn[i] + e * Wc_3cm;
                TLeaf.Add(Tcalc);
            }
        }

        private void CalcVPDairLeaf(double[] hourlyTemperature)
        {
            for (int i = 0; i < 24; i++)
            {
                double vpd = 0.6106 * (Math.Exp((17.27 * TLeaf[i]) / (TLeaf[i] + 237.3)) - RH[i]
                / 100 * Math.Exp((17.27 * hourlyTemperature[i]) / (hourlyTemperature[i] + 237.3)));
                VPDairLeaf.Add(vpd);
            }
        }

        private void CalcVPDeq()
        {
            for (int i = 0; i < 24; i++)
            {
                double PAR = (hRadn[i] * 2.02 / 3600 * 1e6);
                VPDeq.Add(VPDairLeaf[i] * Math.Max(0.0, Math.Min(PAR / 500.0, 1.0)));
            }
        }

        private Vector<double> TLeaf;
        private Vector<double> VPDairLeaf;
        private Vector<double> VPDeq;


        //------------------------------------------------------------------------------------------------
        //-----------  calculate Hourly Leaf Extension Rate
        //------------------------------------------------------------------------------------------------
        protected void CalcHLER(double[] deltaTTCanopyHourly, double a, double b, double c, double psi)
        {
            for (int i = 0; i < 24; i++)
                hLER[i] = (Math.Max(0.0, deltaTTCanopyHourly[i] * (a + b * VPDairLeaf[i] + c * psi)));// replace TLeaf[i] with canopytemperature[i] t0 is the Tbase t-t0 should be replaced by thermal time
        }

        private Vector<double> hLER;

    }
}
