using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusModel.Model.SoilModel
{
    //hourly soil surface temperature based on Parton 1981
    class HourlySoilTemp
    {
        //values for soil surface
        private double a=0.5;
        private double b=1.81;
        private double c=0.49;

        public double[] getHourlySoilSurfaceTemperature(double TMax, double TMin, int julianDate,double latitudeDeg)
        {
            double[] result = new double[24];

            double latitudeRad = latitudeDeg * Math.PI / 180;
            //calculate day length and night length
            double adelt = 0.4014 * Math.Sin(6.28 * (julianDate - 77) / 365);
            double tem1 = 1 - Math.Pow((-Math.Tan(latitudeRad) * adelt), 2);
            tem1 = Math.Sqrt(tem1);
            double tem2 = (-Math.Tan(latitudeRad) * Math.Tan(adelt));
            double ahou = Math.Atan2(tem1, tem2);
            double ady = (ahou / 3.14) * 24;
            double ani = (24 - ady);
            double bb = 12 - ady / 2 + c;
            double be = 12 + ady / 2;

            for (int i= 0; i< 24 ; i++)
            {
                if (i >= bb && i <= be)
                {
                    result[i] = (TMax - TMin) * Math.Sin(3.14 * (i - bb) / (ady + 2 * a)) + TMin;
                }
                else
                {
                    double bbd;
                    if (i > be) { bbd = i - be; }
                    else //i<bb
                    {
                        bbd = 24 + be + i;
                    }
                    double ddy = ady - c;
                    double tsn = (TMax - TMin) * Math.Sin(3.14 * ddy / (ady + 2 * a)) + TMin;
                    result[i] = TMin + (tsn - TMin) * Math.Exp(-b * bbd / ani);
                }
            }
            return result;
        }

    }
}
