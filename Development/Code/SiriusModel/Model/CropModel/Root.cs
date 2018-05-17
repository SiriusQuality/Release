using System;
using SiriusModel.Model.Base;
using SiriusModel.Model.Phenology;
using SiriusQualityPhenology;

namespace SiriusModel.Model.CropModel
{
    ///The class Root.
    public class Root : UniverseLink
    {
        #region Fields

        ///<Behnam (2015.10.28)>
        public bool isUnlimitedWater;
        public bool isUnlimitedNitrogen;
        public bool isUnlimitedTemperature;
        ///</Behnam>

        public double availableNfromSoil;

        ///<summary>Length of this root</summary>
        public double Length { get; private set; }

        #endregion

        #region Constructors

        ///<summary>Create a new root</summary>
        ///<param name="universe">Current universe</param>
        public Root(Universe universe)
            : base(universe)
        {
            Length = 0;
            availableNfromSoil = 0;
        }

        ///<summary>Create a new root by copy</summary>
        ///<param name="universe">The current universe</param>
        ///<param name="toCopy">The root to copy</param>
        public Root(Universe universe, Root toCopy, bool copyAll)
            : base(universe)
        {
            Length = toCopy.Length;
            availableNfromSoil = toCopy.availableNfromSoil;
            if (copyAll)
            {
                isUnlimitedWater = toCopy.isUnlimitedWater;
                isUnlimitedNitrogen = toCopy.isUnlimitedNitrogen;
                isUnlimitedTemperature = toCopy.isUnlimitedTemperature;
            }
        }

        #endregion

        #region Init/InitDayStep

        ///<summary>Init the root for a new simulation</summary>
        public void Init()
        {
            Length = 0;
        }
        ///<summary>Init the root for a new day step</summary>
        public void InitDayStep(double cumulTemperature, double navForRoot, double phaseValue, double AliveDM,double green,Calendar calendar)
        {
            #region new soil av implementation
            
            ///<Behnam (2015.10.27)>
            ///<Comment>Now, N balance is satisfield under unlimited conditions</Comment>

            double cropMaxDeltaN;
            if (phaseValue < 4) // SowingToAnthesis
            {
                cropMaxDeltaN = MaxNuptake * (1 - Math.Exp(-5 * (AliveDM + green) / DMmaxNuptake));
                if (cropMaxDeltaN <= 0) cropMaxDeltaN = MaxNuptake / 10; // because first day DM == 0 // Eq. 35, Manual: CNFluxes
            }
            else
            {
                //cropMaxDeltaN = MaxNuptake * Math.Max(0.0, 1 - CropTool.From(Delta.Air, GrowthStage.ZC_65_Anthesis, calendar, cumulTTAir) / Dgf);
                //#Andrea 14/01/2016 - modified from Delta.Air temperature to Delta.Remobilization
                //cropMaxDeltaN = MaxNuptake * Math.Max(0.0, 1 - CropTool.From(Delta.Air, GrowthStage.ZC_65_Anthesis, calendar, cumulTemperature) / Dgf);
                cropMaxDeltaN = MaxNuptake * Math.Max(0.0, 1 - calendar.cumulTTFrom((int)Delta.Remobilization, GrowthStage.ZC_65_Anthesis, cumulTemperature) / Dgf);
            
            }
            if (isUnlimitedNitrogen)
            {
                availableNfromSoil = cropMaxDeltaN;
            }
            else
            {
                availableNfromSoil = Math.Min(cropMaxDeltaN, navForRoot);
                Check.IsLessOrEqual(availableNfromSoil, navForRoot);
            }
            ///</Behnam>
            
            Check.IsNumber(availableNfromSoil);
            Check.IsPositiveOrZero(availableNfromSoil);
            
            #endregion
        }

        #endregion

        #region Grow

        ///<summary>Grow the Root</summary>
        //public void Grow(double soilDepth, double cumulTTAir)
        public void Grow(double soilDepth, double cumulTTSoil)
        {
            // Daily increment of thermal time based on air temperature, degree-day above 0°C.
            // Length = Math.Min(soilDepth, RVER * Universe_.calculateDailyThermalTime_.getCumulTT(Delta.Air));
            //Length = Math.Min(soilDepth, RVER * cumulTTAir);
            //#Andrea 14/01/2016 - cumulTTSoil instead of cumulTTAir
            Length = Math.Min(soilDepth, RVER * cumulTTSoil);
        }

        #endregion
    }
}