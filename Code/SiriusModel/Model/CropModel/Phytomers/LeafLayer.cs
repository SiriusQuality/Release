using System;
using SiriusModel.Model.Observation;
using SiriusQualityPhenology.Strategies;
using System.Collections.Generic;

namespace SiriusModel.Model.CropModel.Phytomers
{
    public class LeafLayer : Phytomer   // Storage
    {

        #region fields

        public bool IsSmallPhytomer { get; private set; }

        /// <summary>Phyllochron of this Layer</summary>
        public double LayerPhyllochron { get; private set; }

        ///<summary>The growing thermal time of the internode</summary>
        public double InterNodeTTgro { get { return interNode.TTgro(LayerPhyllochron); } }

        ///<summary>Period of constant area of leaf i, °CDay</summary>
        public double TTmat { get; protected set; }

        ///<summary>Period of senescence of leaf i, °CDay</summary>
        public double TTsen { get; private set; }


        /// <summary>Increment of area index for current day, dimensionless</summary>
        public double DeltaAI { get; set; }

        /// Behnam (2016.02.12)
        /// <summary>Change of DM for current day</summary>
        public double DeltaDM { get; set; }

        ///<summary>True if this leaves is dying prematurely because of Ni uptake from mature leaves</summary>
        public bool IsPrematurelyDying { get; protected set; }

        ///<summary>Largest area actually achived by leaf Layer i, dimensionless</summary>
        public double MaxAI { get; private set; }

        ///<summary>Current development state of this leaves Layer</summary>
        public LeafState State { get; private set; }

        ///<summary>Exposed sheath component of this Layer</summary>
        private ExposedSheath exposedSheath;

        ///<summary>Leaf lamina component of this Layer</summary>
        private LeafLaminae leafLamina;

        ///<summary>Internode component of this Layer</summary>
        private InterNode interNode;

        public MaizeLeaf maizeLeaf;

        #endregion

        /// <summary>Initial constructor</summary>
        /// <param name="universe">The universe of this LeafLayer.</param>
        /// <param name="i">The index of this leaf Layer (0 based index, starts from the bottom of the plant).</param>
        public LeafLayer(Universe universe, int i, double cumulTTShoot, int roundedFinalNumber, double finalLeafNumber, double phyllochron, double deltaTTShoot, double deltaTTPhenoMaize, double cumulTTPhenoMaizeAtEmergence)
            : base(universe, i, cumulTTShoot)
        {
            LayerPhyllochron = phyllochron;

            IsSmallPhytomer = isSmallPhytomer(finalLeafNumber, roundedFinalNumber);

            if (!IsSmallPhytomer)
            {
                TTmat = PlagLL * LayerPhyllochron;
                TTsen = PsenLL * LayerPhyllochron;
            }
            else
            {
                TTmat = PlagSL * LayerPhyllochron;
                TTsen = PsenSL * LayerPhyllochron;
            }

            
            DeltaAI = 0;
            DeltaDM = 0;
            IsPrematurelyDying = false;
            MaxAI = 0;
            State = LeafState.Growing;
            exposedSheath = new ExposedSheath(universe);
            leafLamina = new LeafLaminae(universe);
            interNode = new InterNode(universe, roundedFinalNumber, IsSmallPhytomer, Index, PhytoNum);

            if (SwitchMaize) { maizeLeaf = new MaizeLeaf(universe, i, cumulTTPhenoMaizeAtEmergence, finalLeafNumber, deltaTTPhenoMaize); }
        }

        ///<summary>Copy constructor</summary>
        ///<param name="universe">The universe of this leaf Layer.</param>
        ///<param name="toCopy">The leaf Layer to copy</param>
        public LeafLayer(Universe universe, LeafLayer toCopy)
            : base(universe, toCopy)
        {
            LayerPhyllochron = toCopy.LayerPhyllochron;
            TTmat = toCopy.TTmat;
            TTsen = toCopy.TTsen;
            DeltaAI = toCopy.DeltaAI;
            IsPrematurelyDying = toCopy.IsPrematurelyDying;
            MaxAI = toCopy.MaxAI;
            State = toCopy.State;
            IsSmallPhytomer = toCopy.IsSmallPhytomer;
            exposedSheath = (toCopy.exposedSheath != null) ? new ExposedSheath(universe, toCopy.exposedSheath) : null;
            leafLamina = (toCopy.leafLamina != null) ? new LeafLaminae(universe, toCopy.leafLamina) : null;
            interNode = (toCopy.interNode != null) ? new InterNode(universe, toCopy.interNode) : null;
            if (SwitchMaize) { maizeLeaf = (toCopy.maizeLeaf != null) ? new MaizeLeaf(universe, toCopy.maizeLeaf) : null; }
        }

        ///<summary>Init this leaf Layer for a new simulation</summary>
        public void Init()
        {
            exposedSheath.Init();
            leafLamina.Init();
        }

        // #Andrea 01/12/2015 renamed deltaTTShoot to deltaTTRemobilization
        ///<summary>Init this leaf Layer for a new day step</summary>
        public void InitDayStep(double deltaTTRemobilization)
        {
            exposedSheath.InitDayStep(deltaTTRemobilization, State, IsPrematurelyDying);// #Andrea 01/12/2015
            leafLamina.InitDayStep(deltaTTRemobilization, State, IsPrematurelyDying);// #Andrea 01/12/2015
            //interNode.InitDayStep();
        }

        ///<summary>Copy this leaf Layer to a new leaf Layer</summary>
        ///<param name="universe">The universe of the new leaf Layer</param>
        ///<returns>aSheath copy of this leaf Layer</returns>
        public override Phytomer Clone(Universe universe)
        {
            return new LeafLayer(universe, this);
        }

        #region properties

        public double TTgroLamina
        {
            get { return LayerPhyllochron * PexpL; }
        }

        #endregion

        public void IncAreaIndex(double stressGrowth, double cumulTTShoot, double WaterLimitedPotDeltaAI)
        {
            Check.IsNumber(stressGrowth);
            DeltaAI = WaterLimitedPotDeltaAI * stressGrowth;
            if ((cumulTTShoot - TTem) < LayerPhyllochron * PexpL || SwitchMaize)//IsLeafLaminaeGrowing. In case of Maize only laminae grows
            {
                leafLamina.AreaIndex += DeltaAI;
            }
            else
            {
                exposedSheath.AreaIndex += DeltaAI;
            }

            MaxAI = Math.Max(MaxAI, GAI);
        }
        public void DecAreaIndex(double WaterLimitedPotDeltaAI)
        {
            DeltaAI = WaterLimitedPotDeltaAI;
            var gai = GAI;
            if (gai > 0)
            {
                var leafLaminaeDec = WaterLimitedPotDeltaAI * leafLamina.AreaIndex / gai;
                var exposedSheathDec = WaterLimitedPotDeltaAI * exposedSheath.AreaIndex / gai;

                leafLamina.AreaIndex -= -leafLaminaeDec;
                exposedSheath.AreaIndex -= -exposedSheathDec;
            }
        }

        public void Grow(double stressDM, double cumulTTShoot)
        {
            if ((cumulTTShoot - TTem) < LayerPhyllochron * PexpL || SwitchMaize) //IsLeafLaminaeGrowing. In case of Maize only laminae grows
            {
                /// Behnam (2016.02.16): After reverting to the old setting, now DM demand is 
                /// calculated based on total leaf area, not only delta leaf area.

                //leafLamina.Grow(DeltaAI, stressDM);
                leafLamina.Grow(leafLamina.AreaIndex, DeltaAI, stressDM);
            }
            else
            {
                //exposedSheath.Grow(DeltaAI, stressDM);
                exposedSheath.Grow(exposedSheath.AreaIndex, DeltaAI, stressDM);
            }
        }

        public void Senescence(ref double remobN, ref double remobDM, double waterLimitedPotDeltaAI)
        {
            if (GAI > 0)
            {
                var ratio = Math.Min(1.0, (-waterLimitedPotDeltaAI) / GAI);

                leafLamina.Senescence(ratio, ref remobN, ref remobDM);
                exposedSheath.Senescence(ratio, ref remobN, ref remobDM);
            }
            else
            {
                leafLamina.Senescence(1.0, ref remobN, ref remobDM);
                exposedSheath.Senescence(1.0, ref remobN, ref remobDM);

            }
        }

        public void Degradation(double DeltaTTShoot)
        {
            if (leafLamina.AreaIndex<= 0)
            {
                leafLamina.IncTTsinceSen(DeltaTTShoot);

                if (IsSmallPhytomer && leafLamina.TTSinceSen > 0)
                {

                    leafLamina.Degradation(leafLamina.TTSinceSen);

                }
            }

            if (exposedSheath.AreaIndex <= 0)
            {
                exposedSheath.IncTTsinceSen(DeltaTTShoot);

                if (IsSmallPhytomer && exposedSheath.TTSinceSen > 0)
                {
                    exposedSheath.Degradation(exposedSheath.TTSinceSen);

                }
            }

        }

        public double DailyLabileSenescenceN
        {
            get { return leafLamina.DailyLabileSenescenceN + exposedSheath.DailyLabileSenescenceN; }
        }

        public void UseSenescencePool(double toUse)
        {
            var dailyLabileSenescenceN = DailyLabileSenescenceN;
            leafLamina.UseSenescencePool(toUse * leafLamina.DailyLabileSenescenceN / dailyLabileSenescenceN);
            exposedSheath.UseSenescencePool(toUse * exposedSheath.DailyLabileSenescenceN / dailyLabileSenescenceN);
        }

        ///<summary>Get the exposed sheath component of this Layer</summary>
        ///<returns>Exposed sheath</returns>
        public ExposedSheath GetExposedSheath()
        {
            return exposedSheath;
        }

        ///<summary>Get the leaf lamina of this Layer</summary>
        ///<returns>Leaf lamina of this Layer</returns>
        public LeafLaminae GetLeafLamina()
        {
            return leafLamina;
        }

        ///<summary>Get the internode of this Layer</summary>
        ///<returns>Internode of this Layer</returns>
        public InterNode GetInterNode()
        {
            return interNode;
        }

        public double getLaminaPotentialSpecificN() { return leafLamina.PotentialSpecificN; }

        public double getSheathPotentialSpecificN() { return exposedSheath.PotentialSpecificN; }

        public double getSheathSpecificN() { return exposedSheath.SpecificN; }

        public double getSheathAreaIndex() { return exposedSheath.AreaIndex; }

        public double getLaminaAreaIndex() { return leafLamina.AreaIndex; }

        public double getSheathNGreen() { return exposedSheath.N.Green; }

        public double getLaminaNGreen() { return leafLamina.N.Green; }

        public double getLaminaSpecificN() { return leafLamina.SpecificN; }

        public double getSheathNStruct() { return exposedSheath.N.Struct; }

        public double getLaminaNStruct() { return leafLamina.N.Struct; }

        public double GAI
        {
            get { return leafLamina.AreaIndex + exposedSheath.AreaIndex; }
        }

        public double TotalDM
        {
            get { return leafLamina.DM.Total + exposedSheath.DM.Total; }
        }

        public double TotalN
        {
            get { return leafLamina.N.Total + exposedSheath.N.Total; }
        }

        public double AliveDM
        {
            get { return leafLamina.DM.Green + exposedSheath.DM.Green; }
        }

        public double AliveN
        {
            get { return leafLamina.N.Green + exposedSheath.N.Green; }
        }

        public double StructDM
        {
            get { return leafLamina.DM.Struct + exposedSheath.DM.Struct; }
        }

        public double StructN
        {
            get { return leafLamina.N.Struct + exposedSheath.N.Struct; }
        }

        public double LabileDM
        {
            get { return leafLamina.DM.Labile + exposedSheath.DM.Labile; }
        }

        public double LabileN
        {
            get { return leafLamina.N.Labile + exposedSheath.N.Labile; }
        }

        public double DailyLabileN
        {
            get { return leafLamina.N.DailyLabile + exposedSheath.N.DailyLabile; }
        }

        public double DeadDM
        {
            get { return leafLamina.DM.Dead + exposedSheath.DM.Dead; }
        }

        public double LostDM
        {
            get { return leafLamina.DM.Lost + exposedSheath.DM.Lost; }
        }

        public double DeadN
        {
            get { return leafLamina.N.Dead + exposedSheath.N.Dead; }
        }

        public bool IsGrowing
        {
            get { return State == LeafState.Growing && !IsPrematurelyDying; }
        }

        public bool IsLaminaeOptimizableN
        {
            get { return State <= LeafState.Senescing && leafLamina.AreaIndex > 0 && !IsPrematurelyDying; }
        }

        public bool IsSheathOptimizableN
        {
            get { return State <= LeafState.Senescing && leafLamina.AreaIndex > 0 && exposedSheath.AreaIndex > 0 && !IsPrematurelyDying; }
        }

        public double getPotentialDeltaDM(double cumulTTShoot, double waterLimitedPotDeltaAI)
        {
            double result = 0.0;
            if (waterLimitedPotDeltaAI > 0)
            {
                if ((cumulTTShoot - TTem) < LayerPhyllochron * PexpL || SwitchMaize) //IsLeafLaminaeGrowing .In case of Maize only laminae grows
                {
                    result = leafLamina.PotentialSpecificWeight * DeltaAI;
                }
                else
                {
                    result = exposedSheath.PotentialSpecificWeight * DeltaAI;
                }
            }
            return result;
        }

        public double SLW
        {
            get { return (GAI > 0) ? AliveDM / GAI : 0; }
        }

        public double SLN
        {
            get { return (GAI > 0) ? AliveN / GAI : 0; }
        }

        public void setSheathPotentialSpecificN(double sheathPotentialSpecificN) { exposedSheath.setPotentialSpecificN(sheathPotentialSpecificN); }

        public void setLaminaPotentialSpecificN(double laminaPotentialSpecificN) { leafLamina.setPotentialSpecificN(laminaPotentialSpecificN); }

        public void setSheathNLabile(double sheathNLabile) { exposedSheath.setNLabile(sheathNLabile); }

        public void setLaminaNLabile(double laminaNLabile)
        {
            if (laminaNLabile < 0) laminaNLabile = 0;
            leafLamina.setNLabile(laminaNLabile);
        }

        public double UptakeDailyLabileN(double needN, ref double remobDM)
        {
            double result = 0;
            var dailyLabileN = DailyLabileN;

            if (needN > 0 && dailyLabileN > 0)
            {
                needN = Math.Min(needN, dailyLabileN);

                var ratioLaminae = leafLamina.N.DailyLabile / dailyLabileN;
                var ratioSheath = exposedSheath.N.DailyLabile / dailyLabileN;

                result = leafLamina.UptakeDailyLabileN(needN * ratioLaminae, ref remobDM) + exposedSheath.UptakeDailyLabileN(needN * ratioSheath, ref remobDM);
            }
            return result;
        }

        public double EvaluateUptakeLabileN(double needN, ref double remobDM)
        {
            double result = 0;
            double labileN = LabileN;

            if (needN > 0 && labileN > 0)
            {
                needN = Math.Min(needN, labileN);

                double ratioLaminae = leafLamina.N.Labile / labileN;
                double ratioSheath = exposedSheath.N.Labile / labileN;

                result = leafLamina.EvaluateUptakeLabileN(needN * ratioLaminae, ref remobDM) + exposedSheath.EvaluateUptakeLabileN(needN * ratioSheath, ref remobDM);
            }
            return result;
        }

        public double UptakeLabileN(double needN, ref double remobDM)
        {
            double result = 0;
            double labileN = LabileN;

            if (needN > 0 && labileN > 0)
            {
                needN = Math.Min(needN, labileN);

                double ratioLaminae = leafLamina.N.Labile / labileN;
                double ratioSheath = exposedSheath.N.Labile / labileN;

                result = leafLamina.UptakeLabileN(needN * ratioLaminae, ref remobDM) + exposedSheath.UptakeLabileN(needN * ratioSheath, ref remobDM);
            }
            return result;
        }
        public double CountLabileN()
        {
            return leafLamina.CountLabileN + exposedSheath.CountLabileN;
        }

        public double CountDailyLabileN()
        {
            return leafLamina.CountDailyLabileN + exposedSheath.CountDailyLabileN;
        }


        public static int CompareLeafLayerByIndex(LeafLayer a, LeafLayer b)
        {
            return a.Index.CompareTo(b.Index);
        }


        public void setPrematurelyDying(int NewIsPrematurelyDying)
        {
            IsPrematurelyDying = NewIsPrematurelyDying==1;
        }

        public void setState(LeafState newState)
        {
            State = newState;
        }

        #region Tilleroutput
        //those functions should be refactored as we already use them in MaxPotentialFinalLai

        public double PotentialLaminaTillerAreaIndex(int tillerIndex, int roundedFinalNumber, double leafNumber, List<double> tilleringProfile)
        {
            double result;
            if (!IsSmallPhytomer)
            {
                var n = roundedFinalNumber;
                if (!IsFlagPhytomer(leafNumber, roundedFinalNumber))
                {
                    result = ((n - PhytoNum - 1 + CalculateShootNumber.ShiftTiller(tillerIndex)) * ((-1) / (NLL - 1)) + 1) * AreaPL;
                }
                else result = RatioFLPL * ((n - (n - 1) - 1 + CalculateShootNumber.ShiftTiller(tillerIndex)) * ((-1) / (NLL - 1)) + 1) * AreaPL;
            }
            else
            {
                result = AreaSL;
            }

            result *= tilleringProfile[tillerIndex] / 10000;  // 10000 = cm^2 -> m^2
            return result;
        }

        public double PotentialLaminaAreaIndex(int roundedFinalLeafNumber, double leafNumber, List<double> leafTillerNumberArray, List<double> tilleringProfile)
        {
            double laminaeAreaIndex = 0.0;
            /// Behnam (2016.03.09): Final leaf number was replaced with LeafNumber.
            // var leafTillerNumber = LeafTillerNumber[roundedFinalLeafNumber];
            var leafTillerNumber = leafTillerNumberArray[(int)Math.Ceiling(leafNumber) - 1];
            for (var i = 0; i < leafTillerNumber; ++i)
            {
                laminaeAreaIndex += PotentialLaminaTillerAreaIndex(i, roundedFinalLeafNumber, leafNumber, tilleringProfile);
            }
            return laminaeAreaIndex;
        }

        public double PotentialSheathTillerAreaIndex(int tillerIndex, int roundedFinalLeafNumber, List<double> tilleringProfile)
        {
            double result;
            if (!IsSmallPhytomer)
            {
                //pm 25 July 2016: modify the model to calculate the surface area of leaf sheath between two succesive collars (light exposed)
                //                 surface area).   
                //  result = aSheath * Math.Pow(roundedFinalLeafNumber - PhytoNum - NLL + LeafLayer.ShiftTiller(tillerIndex), 2);
                result = Math.Max(AreaSS, AreaPL * ((roundedFinalLeafNumber - PhytoNum + CalculateShootNumber.ShiftTiller(tillerIndex)) * (-1 / (NLL - 2)) + 1));
            }
            else if (PhytoNum > 1)
            {
                result = 0.0;
            }
            else result = AreaSS;

            result *= tilleringProfile[tillerIndex] / 10000;   // 10000 = cm^2 -> m^2
            return result;
        }

        public double PotentialSheathAreaIndex(int roundedFinalLeafNumber, double leafNumber, List<double> leafTillerNumberArray, List<double> tilleringProfile)
        {
            double sheathAreaIndex = 0.0;
            /// Behnam (2016.03.09): Final leaf number was replaced with LeafNumber. LeafNumber was added to input arguments.
            // var leafTillerNumber = LeafTillerNumber[roundedFinalLeafNumber];
            var leafTillerNumber = leafTillerNumberArray[(int)Math.Ceiling(leafNumber) - 1];
            for (var i = 0; i < leafTillerNumber; ++i)
            {
                sheathAreaIndex += PotentialSheathTillerAreaIndex(i, roundedFinalLeafNumber, tilleringProfile);
            }
            return sheathAreaIndex;
        }

        #endregion

        public override void Dispose()
        {
            base.Dispose();
            if (interNode != null)
            {
                interNode.Dispose();
                interNode = null;
            }
            if (leafLamina != null)
            {
                leafLamina.Dispose();
                leafLamina = null;
            }
            if (exposedSheath != null)
            {
                exposedSheath.Dispose();
                exposedSheath = null;
            }
        }
    }
}