using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csMTG.Utilities;

namespace csMTG.RootSystem
{
    static class Segment
    {
        static internal double SegLength(double[] posE, double[] posO)
        /* Calcul the length of a segment */
        {
            return Math.Sqrt(((posE[0] - posO[0]) * (posE[0] - posO[0])) +
                        ((posE[1] - posO[1]) * (posE[1] - posO[1])) +
                        ((posE[2] - posO[2]) * (posE[2] - posO[2])));

        }  
           /****************************************************************************/
           /****************************************************************************/
        static internal double[] AdvOrigin(double[] posO,double[] posE, int seed)
        {   /* Calcul the position of the origin of a adventice on the father segment */

            double[] daugtherOrg = new double[3];

            double rel = RootUtilities.dRandUnif();  /* define the relative position on the segment */
            daugtherOrg[0] = (rel * posO[0]) + ((1.0 - rel) * posE[0]);
            daugtherOrg[1] = (rel * posO[1]) + ((1.0 - rel) * posE[1]);
            daugtherOrg[2] = (rel * posO[2]) + ((1.0 - rel) * posE[2]);

            return daugtherOrg;

        } 
          /****************************************************************************/
          /****************************************************************************/

        static internal double TotVolSeg(double[] posE, double[] posO, double diam)
        /* Calcul the total volume of the segment */
        {
            return 0.25 * Math.PI * Math.Pow(diam, 2) * Math.Sqrt(((posE[0] - posO[0]) * (posE[0] - posO[0])) +
                                                            ((posE[1] - posO[1]) * (posE[1] - posO[1])) +
                                                            ((posE[2] - posO[2]) * (posE[2] - posO[2])));

        }


        static internal double TotSurfSeg(double[] posE, double[] posO, double diam)
        /* Calcul the total volume of the segment */
        {

            double L = Math.Sqrt(Math.Pow(posE[0] - posO[0], 2) + Math.Pow(posE[1] - posO[1], 2));
            return L * diam * Math.PI;

        }


        /****************************************************************************/
        /****************************************************************************/
        static internal double PrimVolSeg(double[] segPosE, double[] segPosO, double tipDiam)
        /* Calcul the primary volume of the segment */
        {
            return 0.25 * Math.PI * tipDiam * tipDiam *
            Math.Sqrt(((segPosE[0] - segPosO[0]) * (segPosE[0] - segPosO[0])) +
                 ((segPosE[1] - segPosO[1]) * (segPosE[1] - segPosO[1])) +
                 ((segPosE[2] - segPosO[2]) * (segPosE[2] - segPosO[2])));

        }  
    }
}
