using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csMTG.RootSystem
{
    public class SoilHorizon
    {
        #region properties

        public double growth { get; set; }  /* Growth oefficient between 0 et 1 */
        public double ramif { get; set; }   /* Mutiplying coefficient for the distance inter-ramif  */
        public double iCMeca { get; set; }  /* Intensity of the mechanical stress */
        public double oCMeca { get; set; }    /* Orientation of the mechanical stress (O iso, ou 1 vert) */
        public double thickness { get; set; }

        #endregion

        #region Constructors

        public SoilHorizon(double cr, double ra, double imec, double omec, double thi)
        {
            growth = cr;
            ramif = ra;
            iCMeca = imec;
            oCMeca = omec;
            thickness = thi;
        }

        public SoilHorizon(SoilHorizon toCopy)
        {
            growth = toCopy.growth;
            ramif = toCopy.ramif;
            iCMeca = toCopy.iCMeca;
            oCMeca = toCopy.oCMeca;
        }

        #endregion


    }
}
