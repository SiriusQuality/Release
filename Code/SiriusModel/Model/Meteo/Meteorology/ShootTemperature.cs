using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusModel.Model.Meteo.Meteorology
{
    public class ShootTemperature: UniverseLink
    {
        public bool isUnlimitedTemperature;
        public double MinShootTemperature;
        public double MaxShootTemperature;
        public double MeanShootTemperature;
        public double[] ShootHourlyTemperature;

        public ShootTemperature(Universe universe) : base(universe)
        {
            isUnlimitedTemperature = UnlimitedTemperature;
            MinShootTemperature = 0;
            MaxShootTemperature = 0;
            MeanShootTemperature = 0;
            if (SwitchMaize)
            {
                ShootHourlyTemperature = new double[24];
            }
        }

        ///<summary>Copy constructor</summary>
        ///<param name="universe">The universe of this sensor</param>
        ///<param name="toCopy">The sensor to copy</param>
        public ShootTemperature(Universe universe, ShootTemperature toCopy)
              : base(universe)
        {
            isUnlimitedTemperature = toCopy.isUnlimitedTemperature;
            MinShootTemperature = toCopy.MinShootTemperature;
            MaxShootTemperature = toCopy.MaxShootTemperature;
            MeanShootTemperature = toCopy.MeanShootTemperature;
            if (SwitchMaize)
            {
                ShootHourlyTemperature = (double[])toCopy.ShootHourlyTemperature.Clone();
            }
        }

        public void Estimate(double leafNumber, double minimumCanopyTemperature, double maximumCanopyTemperature,double[] hourlyCanopyTemperature,
            double soilMinTemperature, double soilMaxTemperature,double[] hourlySoilTemperature)
        {
            ///<Behnam (2016.01.08)>
            double minShootTemp;
            double maxShootTemp;

            if (leafNumber >= MaxLeafSoil)
            {
                minShootTemp = minimumCanopyTemperature;
                maxShootTemp = maximumCanopyTemperature;
                if (SwitchMaize)
                {
                    ShootHourlyTemperature = hourlyCanopyTemperature;
                }
            }
            else
            {
                minShootTemp = soilMinTemperature;
                maxShootTemp = soilMaxTemperature;
                if (SwitchMaize)
                {
                    ShootHourlyTemperature = hourlySoilTemperature;
                }
            }



            /// Behnam (2016.05.11): Temperature is cut at LUETopt instead of MaxTempThreshold
            //  var maxTempThreshold = (currentPhase.Phase1 >= 4) ? PostAnthesisTopt : PreAnthesisTopt;
            
            MinShootTemperature = isUnlimitedTemperature ? CheckTemperatures(minShootTemp, maxShootTemp, /*LUETopt*/MaxTempThreshold)[0] : minShootTemp;
            MaxShootTemperature = isUnlimitedTemperature ? CheckTemperatures(minShootTemp, maxShootTemp, /*LUETopt*/MaxTempThreshold)[1] : maxShootTemp;
            MeanShootTemperature = (MinShootTemperature + MaxShootTemperature) / 2;
            ///</Behnam>
        }

        ///<Behnam (2016.01.08)>
        ///<Comment>Cut the canopy temperature if unlimited temperature condition is being simulated</Comment>
        /// Behnam (2016.05.11) Now only Tmax is cut and Tmin is checked to be less than or equal to Tmax.
        public List<double> CheckTemperatures(double Tmin, double Tmax, double MaxTempThreshold)
        {
            double cut = Tmax - MaxTempThreshold;
            double Tmean = (Tmin + Tmax) / 2;
            if (cut > 0)
            {
                Tmax = Tmax - cut;
                //Tmin = Math.Min(Tmin, Tmax);
                Tmin = Math.Max(0, Tmin - cut);
                //Tmin = Tmin - cut;
                Tmin = Math.Min(Tmin, Tmax);
                Tmean = (Tmin + Tmax) / 2;
            }
            List<double> Temps = new List<double>();
            Temps.Add(Tmin); Temps.Add(Tmax); Temps.Add(Tmean);
            return Temps;
        }
        ///</Behnam>
    }
}
