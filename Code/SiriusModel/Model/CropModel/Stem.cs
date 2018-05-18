using System;
using SiriusModel.Model.Base;
using SiriusModel.Model.Observation;

namespace SiriusModel.Model.CropModel
{
    ///<summary>
    ///Stem compartment.
    ///</summary>
    public class Stem : UniverseLink
    {
        #region Fields

        ///The struc stem DM at anthesis
        public double structAnthDM;
        public double structAnthN;

        ///<summary>
        ///Get the N pool.
        ///</summary>
        public Pool N { get; private set; }

        ///<summary>
        ///Get the DM pool.
        ///</summary>
        public Pool DM { get; private set; }

        #endregion

        #region Constructors

        ///<summary>
        ///Create a new stem.
        ///</summary>
        ///<param name="universe">The current universe.</param>
        public Stem(Universe universe)
            : base(universe)
        {
            N = null;
            DM = null;
            structAnthDM = 0;
            structAnthN = 0;
            N = new Pool();
            DM = new Pool();
        }

        ///<summary>
        ///Create a new stem by copy.
        ///</summary>
        ///<param name="universe">The current universe.</param>
        ///<param name="toCopy">The stem to copy.</param>
        ///<param name="copyAll">true for copying every thing (parallel runs), false to just copy/save the results of the simulation </param>
        public Stem(Universe universe, Stem toCopy, bool copyAll)
            : base(universe)
        {
            structAnthDM = toCopy.structAnthDM;
            structAnthN = toCopy.structAnthN;
            N = (toCopy.N != null) ? new Pool(toCopy.N) : null;
            DM = (toCopy.DM != null) ? new Pool(toCopy.DM) : null;
        }

        #endregion

        #region Init/InitDayStep/InitAtAnthesis

        public void Init()
        {
            N.Init();
            DM.Init();
            structAnthDM = 0;
            structAnthN = 0;
        }

        //#Andrea 02/12/2015 - DeltaAirTemperature replaced with deltaTTRemobilization
        public void InitDayStep(double deltaTTRemobilization)
        {
            //N.InitDayStep();
            DM.InitDayStep();

            //N.InitDayStepDailyLabile(N.Labile * Math.Min(1.0, MaxStemRRND * deltaTTAir)); //#Andrea 02/12/2015 - DeltaAirTemperature replaced with deltaTTRemobilization

            N.InitDayStepDailyLabile(N.Labile * Math.Min(1.0, MaxStemRRND * deltaTTRemobilization)); // #Andrea 02/12/2015   
        }
        
        ///<summary>
        ///Init values at anthesis.
        ///</summary>
        public void InitAtAnthesis()
        {
            structAnthDM = (1 - FracStemWSC) * DM.Total;
            structAnthN = structAnthDM * StrucStemN;

            Check.IsLessOrEqual(structAnthN, N.Total);
        }

        #endregion

        #region Grow

        public void Grow(double excessDeltaLeafDM, bool passedZC_75_EndCellDivision, double availN, out double excessDeltaStemDM)
        {
            if (!passedZC_75_EndCellDivision)
            {
                //var availN = countNRootStemPhytoSene(false);
                var maxDeltaStemDM = availN / StrucStemN;

                // double deltaStemDM = excessDeltaLeafDM; // pm 03/07/09, have commented this line and added the line
                // below to limite the translocation of DM to 
                // the stem so than its N concentration does
                // fall below its structural value
                var deltaStemDM = Math.Min(maxDeltaStemDM,excessDeltaLeafDM); 
                DM.Struct += deltaStemDM;

                // excessDeltaStemDM = 0;// pm 03/07/09, id comment above.
                excessDeltaStemDM = Math.Max(0.0, excessDeltaLeafDM - deltaStemDM);
            }
            else excessDeltaStemDM = excessDeltaLeafDM;
        }

        public void SatisfyDemandForGrowthN(double excessDeltaLeafDM,double excessDeltaStemDM, double remobDM)
        {
            N.Struct += (excessDeltaLeafDM - excessDeltaStemDM) * StrucStemN;
            DM.Struct += remobDM;
        }

        public double CalculateDemandLabileN()
        {
            return Math.Max(0.0, DM.Green * MaxStemN - N.Green);
        }

        public void SatisfyDemandForLabileN(double availN)
        {
            N.Labile += availN;
        }

        public void UseSenescencePool(double NLabileInc)
        {
            N.Labile += NLabileInc; 
        }

        public void FillStem(double NLabileInc, double remobDM)
        {
            N.Labile += NLabileInc;
            DM.Struct += remobDM;
        }


        #endregion

        #region Count/uptake N labile/daily labile, WSC

        public double UptakeDailyLabileN(double needN)
        {
            return N.UptakeDailyLabile(needN);
        }

        public double EvaluateUptakeLabileN(double needN)
        {
            double foundN;
            if (needN <= N.Labile)
            {
                foundN = needN;
            }
            else
            {
                foundN = N.Labile;
                needN -= N.Labile;

                double availN = Math.Max(0.0, N.Struct - structAnthN);

                if (needN <= availN)
                {
                    foundN += needN;
                }
                else
                {
                    foundN += availN;
                }
            }
            return foundN;
        }


        public double UptakeLabileN(double needN)
        {
            double foundN;
            if (needN <= N.Labile)
            {
                foundN = needN;
                N.Labile -= needN;
            }
            else
            {
                foundN = N.Labile;
                needN -= N.Labile;
                N.Labile = 0;

                double availN = Math.Max(0.0, N.Struct - structAnthN);

                if (needN <= availN)
                {
                    foundN += needN;
                    N.Struct -= needN;
                }
                else
                {
                    foundN += availN;
                    N.Struct = structAnthN;
                }
            }
            return foundN;
        }

        public double UptakeWSC(double needDM)
        {
            double foundDM;
            var countDM = CountWSC();

            if (needDM <= countDM)
            {
                DM.Struct -= needDM;
                foundDM = needDM;
            }
            else
            {
                foundDM = countDM;
                DM.Struct = structAnthDM;
            }
            return foundDM;
        }

        public double CountDailyLabileN()
        {
            return N.DailyLabile;
        }

        public double CountLabileN()
        {
            return N.Labile + Math.Max(0.0, N.Struct - structAnthN);
        }
        public double CountWSC()
        {
            return Math.Max(0.0, DM.Green - structAnthDM);
        }

        #endregion 
    }
}