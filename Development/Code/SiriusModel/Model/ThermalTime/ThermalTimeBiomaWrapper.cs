using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiriusQualityThermalTime;

namespace SiriusModel.Model.ThermalTime
{
    public class ThermalTimeBiomaWrapper: UniverseLink
    {
        #region output

        ///<summary>Get delta thermal times of the day</summary>
        public double[] DeltaTT { get { return thermalTimeState.deltaTT; } }

        ///<summary>Get cumul thermal time from the beginning of the simulation to the current day</summary>
        public double[] CumulTT { get { return thermalTimeState.cumulTT; } }

        ///<summary>Get particular delta thermal time of the day</summary>
        ///<param name="deltaField">The delta thermal time to get</param>
        ///<returns>The delta thermal time.</returns>
        public double getDeltaTT(Delta localisation)
        {
            return thermalTimeState.deltaTT[(int)localisation];
        }
        ///<summary>Get particular thermal time since the beginning of the simulation for this day</summary>
        ///<param name="cumulField">The thermal time to get.</param>
        ///<returns>The thermal time.</returns>
        public double getCumulTT(Delta localisation)
        {
            return thermalTimeState.cumulTT[(int)localisation];
        }
        #endregion

        public ThermalTimeBiomaWrapper(Universe universe) : base(universe)
        {
            thermalTimeComponent = new SiriusQualityThermalTime.Strategies.CalculateDailyThermalTime();
            thermalTimeState = new SiriusQualityThermalTime.ThermalTimeState();
            loadParameters();
        }

        public ThermalTimeBiomaWrapper(Universe universe, ThermalTimeBiomaWrapper toCopy, bool copyAll)
            : base(universe)
        {
            thermalTimeState = (toCopy.thermalTimeState != null) ? new SiriusQualityThermalTime.ThermalTimeState(toCopy.thermalTimeState, copyAll) : null;

           if (copyAll)
           {
                thermalTimeComponent = (toCopy.thermalTimeComponent != null) ? new SiriusQualityThermalTime.Strategies.CalculateDailyThermalTime(toCopy.thermalTimeComponent) : null;
           }
        }

        public void EstimateDailyThermalTime(double airTmin, double airTmax, double soilTmin, double soilTmax, double shootTmin, double shootTmax, double[] shootHourlyTemp, double[] hourlyAirTemperature, double currentPhaseValue)
        {
            thermalTimeState.hourlyAirTemperature = hourlyAirTemperature;
            thermalTimeState.hourlyShootTemperature = shootHourlyTemp;
            thermalTimeState.minTair = airTmin;
            thermalTimeState.maxTair = airTmax;
            thermalTimeState.minTshoot = shootTmin;
            thermalTimeState.maxTshoot = shootTmax;
            thermalTimeState.minTsoil = soilTmin;
            thermalTimeState.maxTsoil = soilTmax;
            thermalTimeState.phaseValue = currentPhaseValue;

            thermalTimeComponent.Estimate(thermalTimeState);

        }

        public void Init()
        {
            thermalTimeComponent.Init(thermalTimeState);
        }

        private void loadParameters()
        {

            thermalTimeComponent.switchMaize = SwitchMaize ? 1 : 0;
            thermalTimeComponent.SenAccT = SenAccT;
            thermalTimeComponent.SenAccSlope = SenAccSlope;
            thermalTimeComponent.PreAnthesisTmin = PreAnthesisTmin;
            thermalTimeComponent.PreAnthesisTmax = PreAnthesisTmax;
            thermalTimeComponent.PreAnthesisTopt = PreAnthesisTopt;
            thermalTimeComponent.PreAnthesisShape = PreAnthesisShape;
            thermalTimeComponent.PostAnthesisTmin = PostAnthesisTmin;
            thermalTimeComponent.PostAnthesisTmax = PostAnthesisTmax;
            thermalTimeComponent.PostAnthesisTopt = PostAnthesisTopt;
            thermalTimeComponent.PostAnthesisShape = PostAnthesisShape;
            thermalTimeComponent.LUETmin = LUETmin;
            thermalTimeComponent.LUETmax = LUETmax;
            thermalTimeComponent.LUETopt = LUETopt;
            thermalTimeComponent.LUETshape = LUETshape;
            thermalTimeComponent.UseAirTemperatureForSenescence = UseAirTemperatureForSenescence ? 1 : 0;

        }

        private SiriusQualityThermalTime.Strategies.CalculateDailyThermalTime thermalTimeComponent;
        private SiriusQualityThermalTime.ThermalTimeState thermalTimeState;

    }
}
