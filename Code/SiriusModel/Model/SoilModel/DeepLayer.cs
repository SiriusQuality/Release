using SiriusModel.Model.Base;
using System;
using SiriusModel.Model.Observation;
using SiriusModel.Structure;

namespace SiriusModel.Model.SoilModel
{
    ///<summary>Last Layer of the soil</summary>
    public class DeepLayer : UniverseLink
    {


        #region Fields

        ///<summary>N lost by soil leaching</summary>
        private double lostNitrogen;

        ///<summary>Water lost by the soil</summary>
        private double lostWater;

        ///<summary>Get or set the temperature of the deep soil</summary>
        public double Temperature { get; private set; }

        #endregion

        #region Constructors

        ///<summary>Create a new DeepLayer</summary>
        ///<param name="universe">Universe of the model</param>
        public DeepLayer(Universe universe)
            : base(universe)
        {
            Temperature = 0;
            lostNitrogen = 0;
            lostWater = 0;
        }

        ///<summary>Create a new DeepLayer by copy</summary>
        ///<param name="universe">The universe of the model</param>
        ///<param name="toCopy">DeepLayer to copy</param>
        public DeepLayer(Universe universe, DeepLayer toCopy)
            : base(universe)
        {
            Temperature = toCopy.Temperature;
            lostNitrogen = toCopy.lostNitrogen;
            lostWater = toCopy.lostWater;
        }

        #endregion

        #region Init/InitDayStep.

        ///<summary>Init the deep soil for a new simulation</summary>
        public void Init(double meanTemperature)
        {
            Temperature = meanTemperature; 
            lostNitrogen = 0;
            lostWater = 0;
        }

        ///<summary>Init the deep soil for a new day step</summary>
/*        public override void InitDayStep()
        {
        }
        */
        #endregion

        #region Temperature

        ///<summary>Update the temperature from the soil</summary>
        ///<param name="soil">The soil</param>
        public void UpdateTemperature(Soil soil)
        {
            var minSoilTemp = soil.SoilMinTemperature;
            var maxSoilTemp = soil.SoilMaxTemperature;
            var meanTemp = (minSoilTemp + maxSoilTemp) / 2.0;
            Temperature = (9.0 * Temperature + meanTemp) / 10.0;
        }

        #endregion

        #region Lost

        ///<summary>Get or set the lost water by the soil</summary>
        public double LostWater
        {
            get { return lostWater; }
            set
            {
                Check.IsLessOrEqual(lostWater, value);
                lostWater = value;
            }
        }

        ///<summary>Get or set N lost  by the soil</summary>
        public double LostNitrogen
        {
            get { return lostNitrogen; }
            set
            {
                Check.IsLessOrEqual(lostNitrogen, value);
                lostNitrogen = value;
            }
        }

        #endregion
    }
}