using System;
using SiriusModel.Model.Base;
using SiriusModel.Model.CropModel.Phytomers;
using SiriusModel.Structure;

namespace SiriusModel.Model.CropModel
{
    ///<summary>The class senescence pool daily Ni and DM released by leaf senescence</summary>
    public class SenescencePool : UniverseLink
    {
        // This pool is the first one to be explored when there is a need for Ni and DM.
        // At the end of the day, if this pool is not null, it roll back Ni and DM not consumed to the senescing leaves.

        #region fields

        ///Amount of senescence Ni
        private double n;

        #endregion

        ///<summary>Initial constructor</summary>
        ///<param name="universe">Universe of this senescence pool</param>
        public SenescencePool(Universe universe) : base(universe)
        {
            n = 0;
        }

        ///<summary>Copy constructor</summary>
        ///<param name="universe">Universe of this senescence pool</param>
        ///<param name="toCopy">Senescence pool to copy</param>
        public SenescencePool(Universe universe, SenescencePool toCopy) : base(universe)
        {
            n = toCopy.n;
        }

        ///<summary>Init this senescence pool for a new simulation</summary>
        public void Init()
        {
            n = 0;
        }

        public double EvaluateUptakeSenescenceN(double needN)
        {
            double result;
            if (needN <= n)
            {
                result = needN;
            }
            else
            {
                result = n;
            }
            return result;
        }
        
        
        public double UptakeSenescenceN(double needN)
        {
            double result;
            if (needN <= n)
            {
                n -= needN;
                result = needN;
            }
            else
            {
                result = n;
                n = 0;
            }
            return result;
        }

        //C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
        //ORIGINAL LINE: double countSenescenceN() const
        public double CountSenescenceN()
        {
            return n;
        }

        public void IncSenescenceN(double toAdd)
        {
            n += toAdd;
        }

    }
}