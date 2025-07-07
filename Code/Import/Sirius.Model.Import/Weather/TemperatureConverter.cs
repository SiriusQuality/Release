using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRA.Clima.AirT.Strategies;
using CRA.Clima.AirT.Interfaces;

namespace Sirius.Model.Weather
{
    class TemperatureConverter
    {
        public TemperatureConverter()
        {
            data = new AirTData();
            hatstockles = new HATStockle();
            //default parameters 
            hatstockles.StöckleA = 0.44;
            hatstockles.StöckleB = -0.46;
            hatstockles.StöckleC = 0.9;
            hatstockles.StöckleD = 0.11;
            hatstockles.StöckleE = 0.9;
        }

        // min max to hourly based on Stöckle (2002) (Bioma strategie)
        public double[] getHourlyAirTemperature(double TAirMin, double TAirMax, double previousDayTAirMax,double nextDayTAirMin)
        {

            data.AirTemperatureMax = TAirMax;
            data.AirTemperatureMin = TAirMin;

            hatstockles.StöckleAirTemperatureMaxDayBefore = previousDayTAirMax;
            hatstockles.StöckleAirTemperatureMinDayAfter = nextDayTAirMin;

            hatstockles.Estimate(data);

            return data.AirTemperatureHourly;
        }

        private AirTData data;
        private HATStockle hatstockles;

        //hourly to min max
        static public double HourlyToMin(double[] hourlyTemperature)
        {
            if (hourlyTemperature != null) return
                Math.Min(hourlyTemperature[0], (Math.Min(hourlyTemperature[1], (Math.Min(hourlyTemperature[2], (Math.Min(hourlyTemperature[3],
                (Math.Min(hourlyTemperature[4], (Math.Min(hourlyTemperature[5], (Math.Min(hourlyTemperature[6], (Math.Min(hourlyTemperature[7],
                (Math.Min(hourlyTemperature[8], (Math.Min(hourlyTemperature[9], (Math.Min(hourlyTemperature[10], (Math.Min(hourlyTemperature[11],
                (Math.Min(hourlyTemperature[12], (Math.Min(hourlyTemperature[13], (Math.Min(hourlyTemperature[14], (Math.Min(hourlyTemperature[15],
                (Math.Min(hourlyTemperature[16], (Math.Min(hourlyTemperature[17], (Math.Min(hourlyTemperature[18], (Math.Min(hourlyTemperature[19],
                (Math.Min(hourlyTemperature[20], (Math.Min(hourlyTemperature[21], (Math.Min(hourlyTemperature[22], hourlyTemperature[23])))))))))))))))))))))))))))))))))))))))))))));
            else return 999;
        
        }
        static public double HourlyToMax(double[] hourlyTemperature)
        {
            return
            Math.Max(hourlyTemperature[0], (Math.Max(hourlyTemperature[1], (Math.Max(hourlyTemperature[2], (Math.Max(hourlyTemperature[3],
            (Math.Max(hourlyTemperature[4], (Math.Max(hourlyTemperature[5], (Math.Max(hourlyTemperature[6], (Math.Max(hourlyTemperature[7],
            (Math.Max(hourlyTemperature[8], (Math.Max(hourlyTemperature[9], (Math.Max(hourlyTemperature[10], (Math.Max(hourlyTemperature[11],
            (Math.Max(hourlyTemperature[12], (Math.Max(hourlyTemperature[13], (Math.Max(hourlyTemperature[14], (Math.Max(hourlyTemperature[15],
            (Math.Max(hourlyTemperature[16], (Math.Max(hourlyTemperature[17], (Math.Max(hourlyTemperature[18], (Math.Max(hourlyTemperature[19],
            (Math.Max(hourlyTemperature[20], (Math.Max(hourlyTemperature[21], (Math.Max(hourlyTemperature[22], hourlyTemperature[23])))))))))))))))))))))))))))))))))))))))))))));
        }
    }
}
