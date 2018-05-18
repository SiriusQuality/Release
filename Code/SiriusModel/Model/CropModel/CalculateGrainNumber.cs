using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusModel.Model.CropModel
{
    public class CalculateGrainNumber: UniverseLink
    {
        #region Fields
        ///<summary>Grain number per m^2</summary>
        public double GrainNumber { get;  set; }

        #endregion

        #region Constructors

        public CalculateGrainNumber(Universe universe) : base(universe)
        {

            GrainNumber = 0;
        }

        public CalculateGrainNumber(Universe universe, CalculateGrainNumber toCopy)
            : base(universe)
        {
            GrainNumber = toCopy.GrainNumber;
        }

        #endregion

        public void Estimate(double EarDW)
        {
            if (!UseObservedGrainNumber || ObservedGrainNumber == -1)
            {
                GrainNumber = Math.Max(1, EarDW * EarGR);
            }
            else
            {
                GrainNumber = ObservedGrainNumber;
            }
        }
    }
}
