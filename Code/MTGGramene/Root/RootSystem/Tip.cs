using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csMTG.Utilities;

namespace csMTG.RootSystem
{
    static class Tip
    {

        /****************************************************************************/
        /****************************************************************************/
        static public double[] TipDir(double elong, List<SoilHorizon> soil, int P_DirTropismTend, double P_TropismIntensity, double soilDepth,
            double[] dirInit,double diametre,double[] coord, double[] dirCroiss,int seed)
        {

            double[] dirInt1 = new double[3];
            double[] dirInt2 = new double[3];
            double[] newDir = new double[3];

            dirInt1 = TipMecaDeflection(elong, soil, soilDepth, dirCroiss, diametre, coord,seed);
            dirInt2 = TipGeoDeflection(dirInt1, elong, P_DirTropismTend, P_TropismIntensity, dirInit, diametre);
            newDir = SurfTipDeflection(dirInt2, coord,seed);

            dirCroiss[0] = newDir[0];
            dirCroiss[1] = newDir[1];
            dirCroiss[2] = newDir[2];

            return dirCroiss;


        } 
          /****************************************************************************/
          /****************************************************************************/
        static public double[] TipMecaDeflection(double elong, List<SoilHorizon> soil, double soilDepth, double[] growDir,double diam, double[] coord,int seed)
        {
            const double teta = 15.0; /* Angle around G in degrees */

            double[] vRand = new double[3];
            double[] vRandN = new double[3];
            double[] dirInt = new double[3];
            double depth, stress;

            depth = coord[2];

            int iLayer = 1;
            if (depth > 0 ) iLayer = Math.Max(1,soil.Count - (int)(soilDepth / depth));
            
            //iLayer = (int)Math.Floor(profondeur / 50.0);
            //if (iLayer >= sol.Count) iLayer = sol.Count - 1;
            //if (iLayer < 0) iLayer = 0;
           // //Console.WriteLine("iLayer "+iLayer);
            stress = soil[iLayer-1].iCMeca;
            if (soil[iLayer-1].oCMeca == 1)  /* Vertical anisotrope constrain */
            {
                /* Random determination of the vector within an angle Teta around G */
                do
                {
                    vRand[0] = (2.0 * RootUtilities.dRandUnif() - 1.0) * Math.Sin(Math.PI * teta / 180.0);
                    vRand[1] = (2.0 * RootUtilities.dRandUnif() - 1.0) * Math.Sin(Math.PI * teta / 180.0);
                    do { vRand[2] = RootUtilities.dRandUnif(); } while (vRand[2] > Math.Cos(Math.PI * teta / 180.0));
                    vRandN = RootUtilities.norm(vRand);
                } while (vRandN[2] > Math.Cos(Math.PI * teta / 180.0));
                dirInt[0] = growDir[0] + (elong * vRandN[0] * stress);
                dirInt[1] = growDir[1] + (elong * vRandN[1] * stress);
                dirInt[2] = growDir[2] + (elong * vRandN[2] * stress);
            }
            else    /* Isotrope constrain [oCMecaSol(Profondeur)==0] */
            {
                vRand[0] = 2.0 * RootUtilities.dRandUnif() - 1.0;
                vRand[1] = 2.0 * RootUtilities.dRandUnif() - 1.0;
                vRand[2] = 2.0 * RootUtilities.dRandUnif() - 1.0;
                vRandN = RootUtilities.norm(vRand);
                if (RootUtilities.prodScal(vRandN, growDir) < 0.0)
                {
                    vRandN[0] = -vRandN[0];
                    vRandN[1] = -vRandN[1];
                    vRandN[2] = -vRandN[2];
                }
                dirInt[0] = growDir[0] + (elong * vRandN[0] * stress);
                dirInt[1] = growDir[1] + (elong * vRandN[1] * stress);
                dirInt[2] = growDir[2] + (elong * vRandN[2] * stress);
            }
            return RootUtilities.norm(dirInt);

        } 
          /****************************************************************************/
          /****************************************************************************/
        static public double[] TipGeoDeflection(double[] dirAfterMeca, double elong, int P_DirTropismTrend, double P_TropismIntensity,double[] dirInit,double diam)
        /* Version with plagiotropism */
        {
            double[] dirInt = new double[3];
            double[] vGeoInt = new double[3];
            double[] vGeo = new double[3];

            switch (P_DirTropismTrend)
            {
                case -1:
                    vGeo[0] = 0.0;                  /* Negative Gravitropism */
                    vGeo[1] = 0.0;
                    vGeo[2] = -1.0;
                    break;
                case 0:
                    vGeoInt[0] = dirInit[0]; /* Plagiotropism */
                    vGeoInt[1] = dirInit[1];
                    vGeoInt[2] = 0.0;
                    vGeo = RootUtilities.norm(vGeoInt);
                    break;
                case 1:
                    vGeo[0] = 0.0;                  /* Positive Gravitropism */
                    vGeo[1] = 0.0;
                    vGeo[2] = 1.0;
                    break;
                case 2:
                    vGeoInt[0] = dirInit[0]; /* Exotropism */
                    vGeoInt[1] = dirInit[1];
                    vGeoInt[2] = dirInit[2];
                    vGeo = RootUtilities.norm(vGeoInt);
                    break;
                default:
                    vGeo[0] = 0.0;                 /* Positive Gravitropism */
                    vGeo[1] = 0.0;
                    vGeo[2] = 1.0;
                    break;
            }

            dirInt[0] = dirAfterMeca[0] + (vGeo[0] * P_TropismIntensity * elong * diam);
            dirInt[1] = dirAfterMeca[1] + (vGeo[1] * P_TropismIntensity * elong * diam);
            dirInt[2] = dirAfterMeca[2] + (vGeo[2] * P_TropismIntensity * elong * diam);

            return RootUtilities.norm(dirInt);
        } 
          /****************************************************************************/
          /****************************************************************************/
        static public double[] SurfTipDeflection(double[] dirAfterGeo, double[] coord,int seed)
        {
            double profLim = 50.0 * RootUtilities.dRandUnif();
            double[] dirInt = new double[3];
            dirInt[0] = dirAfterGeo[0];
            dirInt[1] = dirAfterGeo[1];
            dirInt[2] = dirAfterGeo[2];

            if ((dirInt[2] < 0.0) && ((coord[2]) < profLim)) dirInt[2] = dirInt[2] / 10.0;
            return RootUtilities.norm(dirInt);
        } 

          /****************************************************************************/
          /****************************************************************************/
        static public /*List<dynamic>*/double[] MovePointe(double elong,double[] growDir, double[] coord, double distPrimInit, out double distPrimInitout)
        { /* Meristem displacement due to axial growth */

            //List<dynamic> l = new List<dynamic>();
            double[] co = new double[3];
            distPrimInitout = distPrimInit;

            /* Sa position est modifiée */
            co[0] = coord[0] + (elong * growDir[0]);
            co[1] = coord[1] + (elong * growDir[1]);
            co[2] = coord[2] + (elong * growDir[2]);

            /* Son attribut distPrimInit est modifié */
            distPrimInitout += elong;

            //l.Add(co);
            //l.Add(distPrimInit);
            ////Console.WriteLine(elong);

            return co;

        } /* Fonction deplacePointe */
          /****************************************************************************/
          /****************************************************************************/

        static internal double CalcTipElongation(List<SoilHorizon> soil,double dltTT, double P_diamMin, double P_slopeSpeedDiam, double soilDepth,bool mature,bool stop,bool senile,double diam, double[] coord,bool isPot=false)
        /* Calcul of the potential élongation due to soil constrain */
        {
            if ((mature) && (!stop) && (!senile) && (diam > P_diamMin))
                if(!isPot) return (diam) * P_slopeSpeedDiam * SolGrowth(soil, coord[2], soilDepth)*dltTT;
                else return (diam) * P_slopeSpeedDiam * dltTT;
            else return 0.0;

        } 
          /****************************************************************************/
          /****************************************************************************/

        static private double SolGrowth(List<SoilHorizon> soil, double depth, double SoilDepth)
        /* Growth coefficient due to the soil at a given depth */
        {
            //int hor = 0;
            //hor = (int)Math.Floor(profondeur / 50.0);
            //if (hor >= sol.Count) hor = sol.Count - 1;
            //if (hor < 0) hor = 0;
            int hor = 1;

            if (depth > 0) hor = soil.Count - (int)(SoilDepth / depth);
            if (hor < 1) hor = 1;

            return (soil[hor-1].growth);
        }
    }
}
