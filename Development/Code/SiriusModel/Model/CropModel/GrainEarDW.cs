using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusModel.Model.CropModel
{
    public class GrainEarDW: UniverseLink
    {
        #region Fields
        ///<summary>Ear Dry weight, gDM/m2</summary>
        public double EarDW { get; protected set; }

        #endregion


        #region Constructors

        public GrainEarDW(Universe universe) : base(universe)
        {

            EarDW = 0;
        }

        public GrainEarDW(Universe universe, GrainEarDW toCopy)
            : base(universe)
        {
            EarDW = toCopy.EarDW;
        }

        #endregion

        ///<summary>Update the ear dry weight</summary>
        ///<param name="producedDM">Daily biomass produced by the crop</param>
        public void UpdateEarDW(double producedDM, double DEBF)
        {
            EarDW += FracBEAR * producedDM * DEBF;
        }

    }
}
