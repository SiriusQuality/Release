using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusModel.Model.CropModel.Phytomers
{
    public class MaizeLeaf: UniverseLink
    {
        public MaizeLeaf(Universe universe, int index, double cumulTTPhenoMaizeAtEmergence, double finalLeafNumber, double deltaTTPhenoMaize)
            : base(universe)
        {


            initTT = calculateInitTT(index, cumulTTPhenoMaizeAtEmergence - deltaTTPhenoMaize); //<=> initTT = calculateInitTT(index, cumulTTShootAtEmergence - deltaTTShoot);
            // New LER Model (Seb L. / Tardieu F. et Parent B. - 2014 and onwards )
            // ____________________________________________________________________

            // we are centered on emergence !
            // For all cases we need to add tt_to_emerg to center on sowing !! 
            // As the module is only called if we are at emerg, it has to be calculable (^^)
            // Important to understand that leafnum is +1 than vector allocation 
            // First allocation C++ is 0 ! First leaf is 1 !

            // For leaf tips appearance :
            // --------------------------

            double leafnum = index + 1;

            if (leafnum <= Leaf_tip_emerg)
            {
                // Add tt_to_emerg (TTem) :
                tipTT = Dse;
            }
            else
            {
                // Add tt_to_emerg (TTem) :
                tipTT = ((leafnum) - Leaf_tip_emerg) / atip + Dse;
            }


            // For beginning of linear elongation :
            // ------------------------------------

            double abl = k_bl * atip;
            double tt_lim_lip = ((Nlim - Leaf_tip_emerg) / atip) + Dse;
            double bbl = Nlim - (abl * tt_lim_lip);

            double tt_bl = ((leafnum) - bbl) / abl;
            if (tt_bl > tipTT)
            {
                startExpTT = tipTT;
            }
            else
            {
                startExpTT = tt_bl;
            }

            // For ligulation :
            // ----------------

            double tt_ll1 = ttll1;
            double a_ll2 = k_ll * a_ll1;
            double b_ll1 = 1 - (a_ll1 * tt_ll1);
            double N_limll = alpha_tr * Nfinal;
            double b_ll2 = (((N_limll - b_ll1) * (a_ll1 - a_ll2)) / a_ll1) + b_ll1;


            //double leafnum = i + 1;
            if (leafnum <= N_limll)
            {
                // Add tt_to_emerg (TTem):
                liguleTT = ((leafnum - b_ll1) / a_ll1) + Dse;
            }
            else
            {
                // Add tt_to_emerg (TTem):
                liguleTT = ((leafnum - b_ll2) / a_ll2) + Dse;
            }

            // For end of linear exp :
            // -----------------------

            fullyExpTT = calculateFullyExpTT(index, b_ll1, b_ll2, a_ll2);

            //////
            double SIGMA = Sigma * Nfinal;
            double ALPHA = AlphaLER * Nfinal;
            double DOWN = (2 * Math.Pow(SIGMA, 2));
            double A6 = 1 / Math.Exp((-Math.Pow(6 - ALPHA, 2)) / DOWN);
            double UP = (-Math.Pow((index + 1) - ALPHA, 2));

            LERCoef = A6 * Math.Exp(UP / DOWN);

            //////

            if (index < Math.Floor(finalLeafNumber))
            {
                fracPopn = 1.0;
            }
            else
            {
                fracPopn = finalLeafNumber - Math.Floor(finalLeafNumber);
            }

            potentialWidth = calculatePotentialWidths((int)finalLeafNumber, index);
        }

        private double calculateInitTT(int index, double ttElapsed)
        {
            // For leaf initiation :
            if (index < leafNoInitEmerg)
            {
                if (index < Math.Floor(leafNoInitEmerg))
                {
                    return  ttElapsed;
                }
                else
                {
                    return  ttElapsed + (1 - (leafNoInitEmerg - Math.Floor(leafNoInitEmerg))) * 1 / LIR;
                }
            }
            else
            {
                return (calculateInitTT(index - 1, ttElapsed) + 1 / LIR);
            }
        }

        private double calculateFullyExpTT(int index,double b_ll1, double b_ll2, double a_ll2)
        {
            int leafnum =index +1;
            double N_limll = alpha_tr * Nfinal ;

            double lag = Lagmax * leafnum;
            if (leafnum <= N_limll)
            {
                // Add tt_to_emerg (TTem) :
                return ((leafnum - b_ll1) / a_ll1 - lag) + Dse;
            }
            else if (leafnum <= Nfinal - 1)
            {
                // Add tt_to_emerg (TTem) :
                return ((leafnum - b_ll2) / a_ll2 - lag) + Dse;
            }
            else
            {
                // DON'T ADD tt_to_emerg !
                return (calculateFullyExpTT(index - 1, b_ll1, b_ll2, a_ll2));
            }
        }

       private double calculatePotentialWidths(int finalLeafNumber, int index)
       {

           // in this context, the small leaf number parameter is a % of finalLeafNumber --> Small leaf number = finalLeafNumber *SLNparam
           int SLnumber = (int)Math.Ceiling(SLNparam * finalLeafNumber);

           // get slope of leaf size between smallest and largest
           double deltaY = BLsize - SLsize;
           double deltaX = (finalLeafNumber - BLrankFLN - 1) - SLnumber;
           double slope = divide(deltaY, deltaX);

           if (index>=0 && index<SLnumber)
           {
               return SLsize;
           }
           else
           {
               if (index >=SLnumber && index < (finalLeafNumber - BLrankFLN - 1))
               {
                   return SLsize + slope * (index + 1 - SLnumber);
               }
               else
               {
                   if (index >= (finalLeafNumber - BLrankFLN - 1) && index < (finalLeafNumber - BLrankFLN + 1))
                   {
                       return BLsize;
                   }
                   else
                   {
                       if (index >= (finalLeafNumber - BLrankFLN + 1) && index < finalLeafNumber)
                       {
                           return BLsize - slope * (index + 1 - (finalLeafNumber - BLrankFLN + 1));
                       }
                   }
               }
               return -1;
           }
       }
       private double divide(double dividend, double divisor, double default_value = 0)
           //===========================================================================

    /*Definition
    *   Returns (dividend / divisor) if the division can be done
    *   without overflow or underflow.  If divisor is zero or
    *   overflow would have occurred, a specified default is returned.
    *   If underflow would have occurred, zero is returned.
    *Assumptions
    *   largest/smallest real number is 1.0e+/-30
    *Parameters
    *   dividend:     dividend
    *   divisor:      divisor
    *   defaultValue: default value to return if overflow
    *Calls
    *   reals_are_equal
    */
        {
        //Constant Values
        double LARGEST = 1.0e30;    //largest acceptable no. for quotient
        double SMALLEST = 1.0e-30;  //smallest acceptable no. for quotient
        double nought = 0.0;

        //Local Varialbes
        double quotient;

        //Implementation
        if (isEqual(dividend, 0.0))      //multiplying by 0
        {
            quotient = 0.0;
        }
        else if (isEqual(divisor, 0.0))  //dividing by 0
        {
            quotient = default_value;
        }
        else if (Math.Abs(divisor) < 1.0)            //possible overflow
        {
            if (Math.Abs(dividend) > Math.Abs(LARGEST * divisor)) //overflow
            {
                quotient = default_value;
            }
            else
            {
                quotient = dividend / divisor;          //ok
            }
        }
        else if (Math.Abs(divisor) > 1.0)             //possible underflow
        {
            if (Math.Abs(dividend) < Math.Abs(SMALLEST * divisor))    //underflow
            {
                quotient = nought;
            }
            else
            {
                quotient = dividend / divisor;                //ok
            }
        }
        else
        {
            quotient = dividend / divisor;                   //ok
        }
        return quotient;
        }
        private bool isEqual(double A, double B) { return (Math.Abs(A - B) < 1.0E-6); }

        public double CalcArea()
        {
            area = length * width * 0.75;
            return area;
        }

        public MaizeLeaf(Universe universe, MaizeLeaf toCopy) :base(universe)
        {
        initTT= toCopy.initTT;
        tipTT = toCopy.tipTT;
        startExpTT = toCopy.startExpTT;
        liguleTT= toCopy.liguleTT;
        fullyExpTT= toCopy.fullyExpTT;

        LERCoef= toCopy.LERCoef;
        length= toCopy.length;
        potentialWidth= toCopy.potentialWidth;
        width= toCopy.width;

        fracPopn= toCopy.fracPopn;
        }

        public double initTT;                       // TT of initiation of each leaf
        public double startExpTT;                   // TT at start of expansion (initTT + 100)
        public double tipTT;                        // TT of leaf tip appearance
        public double fullyExpTT;                   // TT at initiation, when fully expanded (liguleTT - 50)
        public double liguleTT;                     // TT at ligule appeareance

        public double LERCoef;
        public double length; //mm ?
        public double potentialWidth;
        public double width; //mm ?

        public double fracPopn;
        public double area;                         // calculated area (l x w x 0.75)
        public double exposedArea;                  // area of the leaf that is exposed to light

    }
}
