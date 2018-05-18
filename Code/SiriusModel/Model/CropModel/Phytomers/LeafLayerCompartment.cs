using System;
using SiriusModel.Model.Base;
using SiriusModel.Model.Observation;

namespace SiriusModel.Model.CropModel.Phytomers
{
    ///<summary>Base class for Lamina and Sheath compartments</summary>
    public abstract class LeafLayerCompartment : UniverseLink
    {
        #region fields 

        ///<summary>The Ni pool of this leaf Layer compartment</summary>
        public Pool N { get; private set; }

        ///<summary>The DM pool of this leaf Layer compartment</summary>
        public Pool DM { get; private set; }

        private double areaIndex;

        public double PotentialSpecificN { get; set; }

        public double DailyLabileSenescenceN { get; private set; }

        #endregion 

        ///Constructor.
        ///\param [in] sr the SingleRun instance this LeafLayerCompartment belongs to.
        ///\param [in] i the index of this LeafLayerCompartment.
        protected LeafLayerCompartment(Universe universe)
            : base(universe)
        {
            DailyLabileSenescenceN = 0;
            areaIndex = 0;
            PotentialSpecificN = 0;
            N = new Pool();
            DM = new Pool();
        }

        ///Constructor.
        ///\param [in] sr the SingleRun instance this LeafLayerCompartment belongs to.
        ///\param [in] toCopy instance to init from.
        protected LeafLayerCompartment(Universe universe, LeafLayerCompartment toCopy)
            : base(universe)
        {
            N = (toCopy.N != null) ? new Pool(toCopy.N) : null;
            DM = (toCopy.DM != null) ? new Pool(toCopy.DM) : null;
            DailyLabileSenescenceN = toCopy.DailyLabileSenescenceN;
            areaIndex = toCopy.areaIndex;
            PotentialSpecificN = toCopy.PotentialSpecificN;
        }

        //public void Grow(double deltaAreaIndex, double stressDM)
        public void Grow(double areaIndex, double deltaAreaIndex, double stressDM)
        {
            /// Behnam (2016.02.16): Dry matter demand is now calculated based on total leaf area.
            //var deltaDM = stressDM * deltaAreaIndex * PotentialSpecificWeight;
            var currGreenDM = DM.Struct + DM.Labile;
            var deltaDM = Math.Max(0, stressDM * (areaIndex * PotentialSpecificWeight - currGreenDM));
            var deltaStructDM = deltaDM * (1 - FracBGR());
            DM.Struct += deltaStructDM;
            DM.Labile += deltaDM - deltaStructDM;

            var deltaStructN = deltaStructDM * StrucLeafN;
            N.Struct += deltaStructN;
            N.Labile += deltaAreaIndex * SLNcri - deltaStructN;
        }

        public void Senescence(double ratioToKill, ref double remobN, ref double remobDM)
        {
            if (ratioToKill < 1.0)
            {
                // ?
                var remobLlcDM = ratioToKill * DM.Labile;
                var deadLlcDM = ratioToKill * DM.Struct;
    	
                var remobLlcN = ratioToKill * N.Labile;
                var deadLlcN = ratioToKill * N.Struct;
    	
                DailyLabileSenescenceN += remobLlcN;
    	
                remobN += remobLlcN;
                remobDM += remobLlcDM;
    	
                DM.Struct -= deadLlcDM;
                DM.Dead += deadLlcDM;
                DM.Labile -= remobLlcDM;
  	
                N.Struct -= deadLlcN;
                N.Dead += deadLlcN;
                N.Labile -= remobLlcN;
            }
            else
            {
                DailyLabileSenescenceN += N.Labile;
    	
                remobN += N.Labile;
                remobDM += DM.Labile;
    	
                DM.Dead += DM.Struct;
                DM.Labile = 0;
                DM.Struct = 0;
    	
                N.Dead += N.Struct;
                N.Labile = 0;
                N.Struct = 0;
            }
        }

        public void Degradation(double TTSinceSen)
        {

            double DMlost = Math.Min(DM.Dead * (TTSinceSen / (PhyllDurationDMlost * P)), DM.Dead);

            DM.Lost += DMlost;
            DM.Dead -= DMlost;
        
        }

        public abstract double PotentialSpecificWeight
        {
            get;
        }
        public abstract double FracBGR();

        public double AreaIndex
        {
            get { return areaIndex; }
            set
            {
                areaIndex = value;
                RoundZero(ref areaIndex);
                Check.IsNumber(areaIndex);
                Check.IsPositiveOrZero(areaIndex);
            }
        }

        public double SpecificWeight
        {
            get { return (areaIndex > 0) ? DM.Green/areaIndex : 0; }
        }

        public double SpecificN
        {
            get { return (areaIndex > 0) ? N.Green/areaIndex : 0; }
        }

        public void UseSenescencePool(double toUse)
        {
            N.Dead += toUse;
        }

        public double UptakeDailyLabileN(double needN, ref double remobDM)
        {
            double foundN = 0;
            if (needN > 0 && CountDailyLabileN > 0)
            {
                if (SpecificN >= SLNcri)
                {
                    var ratio = Math.Min(1.0, needN / CountDailyLabileN);
    	
                    foundN = N.DailyLabile * ratio;
                    N.UptakeDailyLabile(foundN);
                }
                else
                {
                    if (AreaIndex > 0 && (SpecificN - (N.Struct / AreaIndex)) > 0)
                    {
                        needN = Math.Min(needN, CountDailyLabileN);
    	
                        var deltaArea = ((needN * LLOSS) / (SpecificN - (N.Struct / AreaIndex)));
                        var ratioDelete = deltaArea / AreaIndex;
    	
                        N.Dead += N.Struct * ratioDelete;
                        N.Struct -= N.Struct * ratioDelete;
    	
                        DM.Dead += DM.Struct * ratioDelete;
                        DM.Struct -= DM.Struct * ratioDelete;
                        remobDM += DM.Labile * ratioDelete;
                        DM.Labile -= DM.Labile * ratioDelete;
    	
                        foundN = needN;
                        AreaIndex -= deltaArea;
                        N.UptakeDailyLabile(needN);
                    }
                }
            }
            return foundN;
        }

        public double EvaluateUptakeLabileN(double needN, ref double remobDM)
        {
            double foundN = 0;

            if (needN > 0 && CountLabileN > 0)
            {
                if (SpecificN >= SLNcri)
                {
                    double ratio = Math.Min(1.0, needN / CountLabileN);

                    foundN = N.Labile * ratio;
                }
                else
                {
                    if (AreaIndex > 0 && (SpecificN - (N.Struct / AreaIndex)) > 0)
                    {
                        needN = Math.Min(needN, CountLabileN);

                        double deltaArea = ((needN * LLOSS) / (SpecificN - (N.Struct / AreaIndex)));
                        double ratioDelete = deltaArea / AreaIndex;

                        remobDM += DM.Labile * ratioDelete;

                        foundN = needN;

                    }

                }
            }

            return foundN;
        }

        public double UptakeLabileN(double needN, ref double remobDM)
        {
            double foundN = 0;

            if (needN > 0 && CountLabileN > 0)
            {
                if (SpecificN >= SLNcri)
                {
                    double ratio = Math.Min(1.0, needN / CountLabileN);

                    foundN = N.Labile * ratio;
                    N.Labile -= foundN;
                }
                else
                {
                    if (AreaIndex > 0 && (SpecificN - (N.Struct / AreaIndex)) > 0)
                    {
                        needN = Math.Min(needN, CountLabileN);

                        var deltaArea = ((needN * LLOSS) / (SpecificN - (N.Struct / AreaIndex)));
                        var ratioDelete = deltaArea / AreaIndex;

                        N.Dead += N.Struct * ratioDelete;
                        N.Struct -= N.Struct * ratioDelete;

                        DM.Dead += DM.Struct * ratioDelete;
                        DM.Struct -= DM.Struct * ratioDelete;
                        remobDM += DM.Labile * ratioDelete;
                        DM.Labile -= DM.Labile * ratioDelete;

                        foundN = needN;
                        AreaIndex -= deltaArea;
                        N.Labile -= needN;
                    }

                }
            }

            return foundN;
        }
        public double CountLabileN
        {
            get { return N.Labile; }
        }

        public double CountDailyLabileN
        {
            get { return N.DailyLabile; }
        }

        // #Andrea 01/12/2015 - renamed deltaTTShoot to deltaTTRemobilization
        ///Virtual method to init the Element at the beginning of a day step.
        public void InitDayStep(double deltaTTRemobilization, LeafState LLState, bool isPrematurelyDying)
        {
            N.InitDayStep();
            DM.InitDayStep();

            if ((LLState == LeafState.Mature
                    || LLState == LeafState.Senescing)
                && !isPrematurelyDying)
            {
                //N.InitDayStepDailyLabile(N.Labile * MaxLeafRRND * deltaTTShoot);  // pm 29 May 2013, replaced air temperature by canopy temperature - modified by #Andrea 01/12/2015
                N.InitDayStepDailyLabile(N.Labile * MaxLeafRRND * deltaTTRemobilization); // #Andrea 01/12/2015
            }
            else
            {
                N.InitDayStepDailyLabile(0);
            }
    	
            DailyLabileSenescenceN = 0;
        }

        ///Virtual method for init the Element.
        public void Init()
        {
            DailyLabileSenescenceN = 0;
    	
            areaIndex = 0;
            N.Init();
            DM.Init();
    	
            PotentialSpecificN = 0;
        }

        public void setPotentialSpecificN(double potentialSpecificN) { PotentialSpecificN = potentialSpecificN; }

        public void setNLabile(double NLabile) { N.Labile = NLabile; }

        public override void Dispose()
        {
            base.Dispose();
            N = null;
            DM = null;
        }
    }
}