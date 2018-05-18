using System;
using SiriusModel.Model.Base;

namespace SiriusModel.Model.Observation
{
    ///<Behnam (2015.10.27)>
    ///<Comment>UnlimitedWater boolean variable used to ro remove drought stress under unlimited water conditions</Comment>
    ///</Behnam>

    ///<summary>
    ///The class StressFactor. This class is designed to calculate daily stress factors applied to the
    ///the crop evolution.
    ///</summary>
    public class StressFactor : UniverseLink
    {

        ///<Behnam (2015.10.28)>
        public bool isUnlimitedWater;
        ///<Behnam (2016.03.18)>
        public double TranspSF;
        ///</Behnam>


        ///<summary>
        ///Fraction of plant available water.
        ///</summary>
        public double FPAW { get; private set; }
        public double SMSF { get; private set; }

        ///<summary>
        ///Initial constructor.
        ///</summary>
        ///<param name="universe">The universe of this stress factor.</param>
        public StressFactor(Universe universe) : base(universe)
        {
        }

        ///<summary>
        ///Copy constructor.
        ///</summary>
        ///<param name="universe">The universe of this stress factor.</param>
        ///<param name="toCopy">The stress factor to copy.</param>
        public StressFactor(Universe universe, StressFactor toCopy) : base(universe)
        {
            FPAW = toCopy.FPAW;
            SMSF = toCopy.SMSF;
            TranspSF = toCopy.TranspSF;
            isUnlimitedWater = toCopy.isUnlimitedWater;
        }

        ///<summary>
        ///Get the soil stress factor (SF). This stress factor is calculated from the
        ///actual transpiration an the potential transpiration.
        ///</summary>
        ///<returns>The soil stress factor.</returns>
        public double SF(double Pot, double Act, double Power)
        {
            //if (isUnlimitedWater) return 1;
            //else return Pot > 0 ? Math.Pow((Pot - Act) / Pot, Power) : 0;
            return Pot > 0 ? Math.Pow(Act / Pot, Power) : 1;
        }

        ///<summary>
        ///Drougth biomass factor.
        ///</summary>
        public double DBF
        {
            //get { return CalculateDF(LowerFPAWlue, UpperFPAWlue, 0, 1, TranspSF); }
            // Pierre 11 April 2016: TranspSF is not applied on transpiration drought factor.
            get { return CalculateDF(LowerFPAWlue, UpperFPAWlue, 0, 1, 1); }
        }

        ///<summary>
        ///Drought ear biomass factor.
        ///</summary>
        public double DEBF
        {
            get
            {
                var dbf = DBF;
                // pm 30 Jan. 2016: Based on the NZ rainshelter exp. taking the square
                //                  of DBF makes the grain number to sensitive to water deficit
                //     return dbf * dbf;
                return dbf;
            }
        }

        ///<summary>
        ///Drought grain factor. = 1
        ///</summary>
        public double DGF
        {
            get { return 1; }
        }

        ///<summary>
        ///Drougth transpiration factor.
        ///</summary>
        public double DTF
        {
            /// Behnam (2016.03.18): TranspSF is not applied on transpiration drought factor.
            get { return CalculateDF(LowerFPAWgs, UpperFPAWgs, 0, 1, 1); }
        }

        ///<summary>
        ///Init the stress factor for a new day step.
        ///</summary>
        public void InitDayStep(double rootZoneMaxAvailableWater, double rootZoneAvailableWater)
        {
            /// Behnam (2016.05.14): Now water is added to the soil instead of being added to the roots.
            //if (isUnlimitedWater)
            if (false)
            {
                TranspSF = 1;
                FPAW = 1;
                SMSF = 0;
            }
            else
            {
                var awc = rootZoneMaxAvailableWater;
                var aw = rootZoneAvailableWater;
                var md = (awc - aw) / awc;
                FPAW = 1 - md;
                SMSF = md;
            }
        }

        private double CalculateDF(double lowerFPAW, double upperFPAW, double minDF, double maxDF, double TranspSF)
        {
            if (FPAW > upperFPAW) return maxDF;
            if (FPAW < lowerFPAW) return minDF;
            return minDF + (maxDF - minDF) * (FPAW - lowerFPAW) / (upperFPAW - lowerFPAW) * TranspSF;
        }
    }
}