using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csMTG.Utilities;

namespace csMTG.RootSystem
{
    static class RootAxe
    {



        #region Seminal Root


        /****************************************************************************/
        /****************************************************************************/
        static internal int CalcNewAdvNumber(double cumulTT, double P_EmissionAdvSpeed, double P_EmissionAdvAge, int P_MaxNumberAdv)
        {
            /* Calcul of the new number of adventice root */

            int newAdvNumber;

            newAdvNumber = (int)(P_EmissionAdvSpeed * (cumulTT - P_EmissionAdvAge));

            if (newAdvNumber > P_MaxNumberAdv) newAdvNumber = P_MaxNumberAdv;

            return newAdvNumber;

        }  

        /****************************************************************************/
        /****************************************************************************/

        static internal int CalcNewSeminalNumber(double cumilTT, double P_EmissionSemSpeed, int P_MaxNumberSem)
        {
            /* Calcul the new number of seminal roots */

            int newSemNumber;

            newSemNumber = (int)(P_EmissionSemSpeed * cumilTT);

            if (newSemNumber >= P_MaxNumberSem) newSemNumber = P_MaxNumberSem;

            return newSemNumber;

        }  


        /****************************************************************************/
        /****************************************************************************/
        static internal double DistRamifTip(List<SoilHorizon> soil, double P_distRamif, double SoilDepth, double[] coord)
        { /* lovcal value of the inter-ramification distance of the tip */

            return (P_distRamif * ramifSoil(soil, coord[2], SoilDepth));

        } 
          /****************************************************************************/
          /****************************************************************************/
        static internal double RandomDiamDaugtherTip(double P_propDiamRamif, double P_diamMin, double P_coeffVarDiamRamif, double diametre)
        {   /* determine randomely the diameter of a ramification meristem according to that of the father
       for the sequential ramification */
            //double par = (P_propDiamRamif-(P_diamMin/ diametre))/(1-(P_diamMin/diametre));
            //double av = (diametre * par) + (P_diamMin * (1.0 - par));
            double av = (diametre * P_propDiamRamif)+ P_diamMin*(1- P_propDiamRamif);
            double sd = av * P_coeffVarDiamRamif;
            double diamPFille = 100.0;  // initialisation Ã  une forte valeur pour boucle de tirage
            while (diamPFille > (0.95 * diametre)) diamPFille = RootUtilities.RandGaussien(av, sd);

            ////Console.WriteLine(RootUtilities.tireGaussien(moy, et,seed));

            return diamPFille;

        } 

        /****************************************************************************/
        /****************************************************************************/
        static internal double ramifSoil(List<SoilHorizon> soil, double depth, double SoilDepth)
        /* Calcul the ramification coefficient of the soil at the given depth */
        {
            int hor = 1;

            if (depth > 0) hor = soil.Count - (int)(SoilDepth / depth);
            if (hor < 1) hor = 1;
            return (soil[hor - 1].ramif);

            //int hor;

            //hor = (int)Math.Floor(profondeur / 50.0);
            //if (hor >= sol.Count) hor = sol.Count - 1;
            //if (hor < 0) hor = 0;

            //return (sol[hor].ramif);
        } 

        /****************************************************************************/
        /****************************************************************************/
        
        static internal double[] RamifOrigin(double[] coord, double[] growDir, double primInitDist)
        {
            double[] originDaugther = new double[3];

            /* Calcul the position of the origine of a ramification */
            originDaugther[0] = coord[0] - (primInitDist * growDir[0]);
            originDaugther[1] = coord[1] - (primInitDist * growDir[1]);
            originDaugther[2] = coord[2] - (primInitDist * growDir[2]);
            return originDaugther;

        } 
          /****************************************************************************/
          /****************************************************************************/
        static internal double[] RamifDirection(double P_angLat,double[] growDir,int seed)
        {
            double[] DaugtherDir = new double[3];

            /* Calcul the direction of a son axis created from ramification */
            double[] vAxeRot = new double[3];
            double[] rotDirGrow = new double[3];
            double norVProjHor, angRot;

            /* Calcul of the norm of the  projection direction on the horiszontal plan */
            norVProjHor = Math.Sqrt((growDir[0] * growDir[0]) + (growDir[1] * growDir[1]));
            if (norVProjHor < RootUtilities.epsilon)
            {
                vAxeRot[0] = 1.0; /* Vertical initial vector */
                vAxeRot[1] = 0.0;
                vAxeRot[2] = 0.0; /* Vecteur (1,0,0) chosen as rotation axis */
            }
            else
            {
                vAxeRot[0] = growDir[1] / norVProjHor;
                vAxeRot[1] = -growDir[0] / norVProjHor;
                vAxeRot[2] = 0.0;
            }
            /* growDir is rotated around vAxeRot with an insertion angle */
            angRot = P_angLat;
            rotDirGrow = RootUtilities.rotVect(angRot, vAxeRot, growDir);

            /* rotDirCroiss is rotated around the growDir with a radial angle */
            ////Console.WriteLine(angRot);
            angRot = RootUtilities.randAngRad(seed);
            return RootUtilities.rotVect(angRot, growDir, rotDirGrow);
        } 
          /****************************************************************************/
          /****************************************************************************/

        #endregion

    }
}
