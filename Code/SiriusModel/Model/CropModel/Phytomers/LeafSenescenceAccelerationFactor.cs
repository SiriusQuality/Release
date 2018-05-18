using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusModel.Model.CropModel.Phytomers
{
    public class LeafSenescenceAccelerationFactor : UniverseLink
    {
        public LeafSenescenceAccelerationFactor(Universe universe)
            : base(universe)
        {
            SAF = 1;
        }

        /// <summary>
        /// Senescence acceleration factor.
        /// </summary>
        public double SAF { get; private set; }

        public LeafSenescenceAccelerationFactor(Universe universe, LeafSenescenceAccelerationFactor toCopy)
            : base(universe)
        {
            SAF = toCopy.SAF;
        }

        public void CalculSenescenceAccelerationFactor(double temperature, double temperatureThreshold, double slopeSenescenceAcceleration)
        {
            if (temperature < temperatureThreshold)
            {
                SAF = 1;
            }
            else
            {
                SAF = 1 + Math.Max(0, temperature - temperatureThreshold) * slopeSenescenceAcceleration;
            }
        }

    }
}
