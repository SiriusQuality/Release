using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusModel.Model.SoilModel
{
    public class SoilTemperature
    {

        ///<summary>
        ///Soil minimum temperature
        ///OUTPUT UNITS: °C
        ///</summary>
        ///<param name="weatherMaxTemp"></param>
        ///<param name="weatherMeanTemp"></param>
        ///<param name="weatherMinTemp"></param>
        ///<param name="soilHeatFlux"></param>
        ///<param name="lambda"></param>
        ///<param name="deepTemperature"></param>
        ///<returns></returns>
        public double SoilMinimumTemperature(double weatherMaxTemp, double weatherMeanTemp, double weatherMinTemp, double soilHeatFlux, double lambda, double deepTemperature)
        {
            return Math.Min(SoilTempA(weatherMaxTemp, weatherMeanTemp, soilHeatFlux, lambda),
                            SoilTempB(weatherMinTemp, deepTemperature));
        }

        ///<summary>
        ///Soil maximum temperature
        ///OUTPUT UNITS: °C
        ///</summary>
        ///<param name="weatherMaxTemp"></param>
        ///<param name="weatherMeanTemp"></param>
        ///<param name="weatherMinTemp"></param>
        ///<param name="soilHeatFlux"></param>
        ///<param name="lambda"></param>
        ///<param name="deepTemperature"></param>
        ///<returns></returns>
        public double SoilMaximumTemperature(double weatherMaxTemp, double weatherMeanTemp, double weatherMinTemp, double soilHeatFlux, double lambda, double deepTemperature)
        {
            return Math.Max(SoilTempA(weatherMaxTemp, weatherMeanTemp, soilHeatFlux, lambda),
                            SoilTempB(weatherMinTemp, deepTemperature));
        }


        ///<summary>
        ///Soil temperature A
        ///</summary>
        ///<param name="weatherMaxTemp"></param>
        ///<param name="weatherMeanTemp"></param>
        ///<param name="soilHeatFlux"></param>
        ///<param name="lambda"></param>
        ///<returns></returns>
        private double SoilTempA(double weatherMaxTemp, double weatherMeanTemp, double soilHeatFlux, double lambda)
        {
            double TempAdjustment = (weatherMeanTemp < 8.0) ? -0.5 * weatherMeanTemp + 4.0 : 0;
            double SoilAvailableEnergy = soilHeatFlux*lambda/1000;

            return weatherMaxTemp + 11.2 * (1.0 - Math.Exp(-0.07 * (SoilAvailableEnergy - 5.5))) + TempAdjustment; 
        }

        ///<summary>
        ///Soil temperature B
        ///</summary>
        ///<param name="weatherMinTemp"></param>
        ///<param name="deepTemperature"></param>
        ///<returns></returns>
        private double SoilTempB(double weatherMinTemp, double deepTemperature)
        {
            return (weatherMinTemp + deepTemperature) / 2.0;    
        }
    }
}
