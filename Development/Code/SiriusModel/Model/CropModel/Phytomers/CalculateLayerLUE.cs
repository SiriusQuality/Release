using ResponseFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusModel.Model.CropModel.Phytomers
{
    public class CalculateLayerLUE:UniverseLink
    {
        public CalculateLayerLUE(Universe universe):base(universe)
        {

        }

        public CalculateLayerLUE(Universe universe,CalculateLayerLUE toCopy, bool copyAll):base(universe)
        {

        }

        ///<summary>Get the LUE of a Layer </summary>
        ///<param name="leafLaminaeSpecificN"></param>
        ///<param name="q"></param>
        ///<param name="canopyTmin"></param>
        ///<param name="canopyTmax"></param>
        ///<param name="DBF"></param>
        ///<returns>Returns the LUE of a Layer</returns>
        public double GetLue(double leafLaminaeSpecificN, double q, double canopyTmin, double canopyTmax, double DBF, double dailyCO2)
        {
            return GetFR(q) * GetFt(canopyTmin, canopyTmax) * GetFCO2(dailyCO2) * GetFN(leafLaminaeSpecificN) * GetFH2O(DBF);
        }

        ///<summary>Get the CO2 factor for LUE (FCO2)</summary>
        ///<returns>Returns the CO2 factor for LUE (FT)</returns>
        public double GetFCO2(double dailyCO2)
        {
            return 1 + FacCO2 * (dailyCO2 - StdCO2) / StdCO2;
        }

        public double GetFN(double leafLaminaeSpecificN)
        {
            return (leafLaminaeSpecificN > SLNmin) ? 1 - Math.Exp(-TauSLN * (leafLaminaeSpecificN - SLNmin)) : 0;
        }

        ///<summary>Get the temperature factor for LUE (FT)</summary>
        ///<returns>returns the temperature factor for LUE (FT)</returns>
        public double GetFt(double canopyTmin, double canopyTmax)
        {
            double result = 0;
            double[] Tmfac = { 0.98, 0.91, 0.8, 0.6, 0.37, 0.22, 0.1, 0.02 };

            WangEngel nonlinearWangEngel = new WangEngel();
            for (int i = 0; i < 8; i++)
            {
                var htemp = canopyTmin + (canopyTmax - canopyTmin) * Tmfac[i];
                result += nonlinearWangEngel.Calculate(LUETmin, LUETopt, LUETmax, LUETshape, htemp);
                //result += nonlinearWangEngel.Calculate(-1, 20, 35, 0.8, htemp);
            }
            return result / 8;
        }

        ///<summary>Get FR</summary>
        ///<param name="q">Q ratio</param>
        ///<returns>Returns FR</returns>
        public double GetFR(double q)
        {
            // pm 29/09/09, calculation of FR reparametrized so LUE (for diffsue light) is defined as a 
            //              varietal parameter. Maximum value of FR (and hance of LUE) is equal to LueDiffuse

            // pm 28 Jan. 2016: reparametrized so the parameter LUE is defined for clear days (when Qratio = 0.23)
            //return SlopeFR * (q - 1) + LUEDiffuse;
            return SlopeFR * (q - 0.23) + LUE;
        }
        ///Get the FH2O.
        ///\return the FH2O.
        public double GetFH2O(double DBF)
        {
            return DBF;
        }
    }
}
