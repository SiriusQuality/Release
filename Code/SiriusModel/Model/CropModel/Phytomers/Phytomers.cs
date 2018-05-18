using System;
using System.Collections.Generic;
using ResponseFunctions;
using SiriusModel.Model.Base;
using SiriusModel.Model.Observation;
using SiriusModel.Structure;
using System.Linq;

namespace SiriusModel.Model.CropModel.Phytomers
{
    ///<summary>The container of all phytomers</summary>
    public class Phytomers : UniverseLink
    {
        #region fields

        //to store the input of grain.grow
        public Vector<bool> leafLayerState;
        public Vector<Vector<double>> specificN;
        public Vector<Vector<double>> areaIndex;
        public Vector<Vector<double>> phytoNStruct;
        public Vector<Vector<double>> phytoNLabile;
        public Vector<Vector<double>> phytoDMLabile;

        ///<Behnam (2016.01.20)>
        ///<summary>Total PAR intercepted by canopy</summary>
        public double TotalPAR { get; private set; }
        ///<summary>Slope of biomass accumulation vs total PAR intercepted</summary>
        public double TotalLUE { get; private set; }
        ///</Behnam>

        public EarPeduncle EarPeduncle { get; private set; }

        ///<summary>All phytomers</summary>
        private Vector<Phytomer> All { get; set; }

        ///<summary>All leaves</summary>
        public VectorSelection<Phytomer, LeafLayer> AllLeaves { get; private set; }

        ///<summary>Growing and mature leaves</summary>
        private VectorSelection<Phytomer, LeafLayer> GrowingOrMatureLeaves { get; set; }

        ///<summary>Growing leaves</summary>
        private VectorSelection<Phytomer, LeafLayer> GrowingLeaves { get; set; }

        ///<summary>Mature leaves</summary>
        private VectorSelection<Phytomer, LeafLayer> MatureLeaves { get; set; }

        ///<summary>Senescing leaves</summary>
        private VectorSelection<Phytomer, LeafLayer> SenescingLeaves { get; set; }

        ///<summary>Dead leaves</summary>
        private VectorSelection<Phytomer, LeafLayer> DeadLeaves { get; set; }

        private double Kn { get { return AlphaKn * Kl * Math.Pow(GAI, -BetaKn); } }

        ///<summary>Quantity of DM produced this day</summary>
        public double DeltaShootDM { get; private set; }

        public bool HasNewLeafAppeared { get;private set; }

        /// <summary>Sum the length of each internode, cm</summary>
        public double SumInternodesLength
        {
            get
            {
                double result = 0;
                foreach (var ll in AllLeaves)
                {
                    result += ll.GetInterNode().Length;
                }
                result += (EarPeduncle != null) ? EarPeduncle.getInternodeLength() : 0;
                return result;
            }
        }

        public bool getNewLeafIsSmallPhytomer()
        {
            if (AllLeaves.Count > 0)
            {
                return AllLeaves[AllLeaves.Count - 1].IsSmallPhytomer;
            }
            else
            {
                return false;
            }
        }
        public int getNewLeafLastPhytoNum()
        {
            if (AllLeaves.Count > 0)
            {
                return AllLeaves[AllLeaves.Count - 1].PhytoNum;
            }
            else
            {
                return -1;
            }

        }
        public int getNewLeafindex()
        {
            if (AllLeaves.Count > 0)
            {
                return AllLeaves[AllLeaves.Count - 1].Index;
            }
            else
            {
                return -1;
            }

        }

        public double DEF { get { return switchLAI_.DEF; } }

        public double DSF { get { return switchLAI_.DSF; } }

        public double LER { get { return switchLAI_.LER; } }

        public double getWaterLimitedPotDeltaAI(int leafLayerIndex)
        {
            return switchLAI_.getWaterLimitedPotDeltaAI(leafLayerIndex);
        }
        
        
        public double getLeafLength(int i) 
        {
            if (i < AllLeaves.Count)
            {
                MaizeLeaf ml = AllLeaves[i].maizeLeaf;
                if (ml != null) { return ml.length; }
                else { return -1; }
            }
            else
            {
                return -1;
            }
            
        }

        public double getLeafWidth(int i)
        {
            if (i < AllLeaves.Count)
            {
                MaizeLeaf ml = AllLeaves[i].maizeLeaf;
                if (ml != null) { return ml.width; }
                else { return -1; }
            }
            else
            {
                return -1;
            }

        }

        public double getPotentialIncDeltaArea()
        {
                return switchLAI_.getPotentialIncDeltaArea();
        }

        private SwitchLAI switchLAI_;

        private CalculateInternodesGrowth calculateInternodeGrowth;

        private CalculateLayerLUE calculateLayerLUE_;

        #endregion

        ///<summary>Initial constructor</summary>
        ///<param name="universe">The universe of this phytomers.</param>
        public Phytomers(Universe universe) : base(universe)
        {
            All = new Vector<Phytomer>();
            AllLeaves = new VectorSelection<Phytomer, LeafLayer>(All);
            GrowingLeaves = new VectorSelection<Phytomer, LeafLayer>(All);
            MatureLeaves = new VectorSelection<Phytomer, LeafLayer>(All);
            GrowingOrMatureLeaves = new VectorSelection<Phytomer, LeafLayer>(All);
            SenescingLeaves = new VectorSelection<Phytomer, LeafLayer>(All);
            DeadLeaves = new VectorSelection<Phytomer, LeafLayer>(All);
            EarPeduncle = null;
            DeltaShootDM = 0;


            leafLayerState = new Vector<bool>();
            specificN = new Vector<Vector<double>>();
            areaIndex = new Vector<Vector<double>>();
            phytoNStruct = new Vector<Vector<double>>();
            phytoNLabile = new Vector<Vector<double>>();
            phytoDMLabile = new Vector<Vector<double>>();
            switchLAI_ = new SwitchLAI(universe);
            calculateInternodeGrowth = new CalculateInternodesGrowth(universe);
            calculateLayerLUE_ = new CalculateLayerLUE(universe);

            TotalPAR = 0;
            TotalLUE = 0;
        }

        ///<summary>Copy constructor</summary>
        ///<param name="universe">The universe of this phytomers</param>
        ///<param name="toCopy">The phytomers to copy</param>
        ///<param name="copyAll">true for copying every thing (parallel runs), false to just copy/save the results of the simulation </param>
        public Phytomers(Universe universe, Phytomers toCopy, bool copyAll) : base(universe)
        {
            All = new Vector<Phytomer>(toCopy.All.Count);
            for (var i = 0; i < All.Count; ++i)
            {
                All[i] = toCopy.All[i].Clone(universe);
            }

            AllLeaves = new VectorSelection<Phytomer, LeafLayer>(All, toCopy.AllLeaves);
            GrowingLeaves = new VectorSelection<Phytomer, LeafLayer>(All, toCopy.GrowingLeaves);
            MatureLeaves = new VectorSelection<Phytomer, LeafLayer>(All, toCopy.MatureLeaves);
            GrowingOrMatureLeaves = new VectorSelection<Phytomer, LeafLayer>(All, toCopy.GrowingOrMatureLeaves);
            SenescingLeaves = new VectorSelection<Phytomer, LeafLayer>(All, toCopy.SenescingLeaves);
            DeadLeaves = new VectorSelection<Phytomer, LeafLayer>(All, toCopy.DeadLeaves);
            EarPeduncle = (toCopy.EarPeduncle != null) ? new EarPeduncle(universe, toCopy.EarPeduncle) : null;
            switchLAI_ = (toCopy.switchLAI_ != null) ? new SwitchLAI(universe, toCopy.switchLAI_,copyAll) : null;
            DeltaShootDM = toCopy.DeltaShootDM;
            HasNewLeafAppeared = toCopy.HasNewLeafAppeared;
            calculateLayerLUE_ = (toCopy.calculateLayerLUE_ != null) ? new CalculateLayerLUE(universe, toCopy.calculateLayerLUE_, copyAll) : null;

            if (copyAll)
            {
                var nbLayer = toCopy.leafLayerState.Count;
                leafLayerState = new Vector<bool>();
                specificN = new Vector<Vector<double>>();
                areaIndex = new Vector<Vector<double>>();
                phytoNStruct = new Vector<Vector<double>>();
                phytoNLabile = new Vector<Vector<double>>();
                phytoDMLabile = new Vector<Vector<double>>();
                DeltaShootDM = toCopy.DeltaShootDM;
                TotalPAR = toCopy.TotalPAR;
                TotalLUE = toCopy.TotalLUE;
        
                for (var i = 0; i < nbLayer; ++i)
                {
                    leafLayerState.Add(toCopy.leafLayerState[i]);
                    specificN.Add(new Vector<double>(2));
                    specificN[i][0] = toCopy.AllLeaves[i].GetLeafLamina().SpecificN;
                    specificN[i][1] = toCopy.AllLeaves[i].GetExposedSheath().SpecificN;
                    areaIndex.Add(new Vector<double>(2));
                    areaIndex[i][0] = toCopy.AllLeaves[i].GetLeafLamina().AreaIndex;
                    areaIndex[i][1] = toCopy.AllLeaves[i].GetExposedSheath().AreaIndex;
                    phytoNStruct.Add(new Vector<double>(2));
                    phytoNStruct[i][0] = toCopy.AllLeaves[i].GetLeafLamina().N.Struct;
                    phytoNStruct[i][1] = toCopy.AllLeaves[i].GetExposedSheath().N.Struct;
                    phytoNLabile.Add(new Vector<double>(2));
                    phytoNLabile[i][0] = toCopy.AllLeaves[i].GetLeafLamina().N.Labile;
                    phytoNLabile[i][1] = toCopy.AllLeaves[i].GetExposedSheath().N.Labile;
                    phytoDMLabile.Add(new Vector<double>(2));
                    phytoDMLabile[i][0] = toCopy.AllLeaves[i].GetLeafLamina().DM.Labile;
                    phytoDMLabile[i][1] = toCopy.AllLeaves[i].GetExposedSheath().DM.Labile;   
                }
                calculateInternodeGrowth = (toCopy.calculateInternodeGrowth != null) ? new CalculateInternodesGrowth(universe, toCopy.calculateInternodeGrowth) : null;
                
            }
        }

        ///<summary>Init this phytomers for a new simulation</summary>
        public void Init()
        {
            DeltaShootDM = 0;
        }

        // #Andrea 01/12/2015 - renamed deltaTTShoot to deltaTTRemobilization
        ///<summary>Init this phytomers for a new day step</summary>
        public void InitDayStep(double deltaTTRemobilization)
        {
            foreach (var iter in AllLeaves)
            {
                iter.InitDayStep(deltaTTRemobilization);// #Andrea 01/12/2015
            }
        }
        ///<summary>Create a new leaf Layer until actual number of leaf Layer = specified number. In pratice there should only be one leaf created</summary>
        ///<param name="newNumber">New number of leaf Layer</param>
        private void CreateLeafLayer(double newLeafNumber, double leafNumber, double cumulTTShoot, double deltaTTShoot, int roundedFinalNumber, double finalLeafNumber, double phyllochron, double deltaTTPhenoMaize, double cumulTTPhenoMaizeAtEmergence)
        {
            HasNewLeafAppeared = true;
            var newNbLayer = (int)Math.Ceiling(newLeafNumber);
            var curNbLayer = (int)Math.Ceiling(leafNumber);

            for (var i = curNbLayer; i < newNbLayer; ++i)// there should be only one new leaf at max
            {
                var newLeafLayer = new LeafLayer(Universe_, i, cumulTTShoot, roundedFinalNumber,finalLeafNumber,phyllochron,deltaTTShoot, deltaTTPhenoMaize, cumulTTPhenoMaizeAtEmergence);
                All.Add(newLeafLayer);
                AllLeaves.Add(i);
                GrowingLeaves.Add(i);
                GrowingOrMatureLeaves.Add(i);
            }
        }

        ///<summary>Update all leaf state and register leaves in the appropriate vector segment (GrowingLeaves, MatureLeaves, SenescingLeaves, DeadLeaves)</summary>
        public void UpdateStates(List<LeafState> newStates,List<int> IsPrematurelyDying)
        {
            for (int i = 0; i < AllLeaves.Count; i++)
            {
                //update leaf layer state and vector selections
                var lastState = AllLeaves[i].State;
                AllLeaves[i].setState(newStates[i]);
                AllLeaves[i].setPrematurelyDying(IsPrematurelyDying[i]);
                if (lastState != AllLeaves[i].State)
                {
                    switch (lastState)
                    {
                        case LeafState.Growing: GrowingLeaves.Remove(i); GrowingOrMatureLeaves.Remove(i); break;
                        case LeafState.Mature: MatureLeaves.Remove(i); GrowingOrMatureLeaves.Remove(i); break;
                        case LeafState.Senescing: SenescingLeaves.Remove(i); break;
                        case LeafState.Dead: throw new Exception("aSheath dead leaf must stay dead.");
                    }
                    switch (AllLeaves[i].State)
                    {
                        case LeafState.Growing: throw new Exception("aSheath leaf can't go to the growing state after is has gone to an other state");
                        case LeafState.Mature: MatureLeaves.Add(i); GrowingOrMatureLeaves.Add(i);  break;
                        case LeafState.Senescing: SenescingLeaves.Add(i); break;
                        case LeafState.Dead: DeadLeaves.Add(i); break;
                    }
                }
            }
        }

        public void UpdateInternodes(List<double> LeafLayerInternodeLengthGrowthList, double EarPeduncleInternodeLengthGrowth)
        {
            foreach (var ll in AllLeaves)
            {
                if (ll.Index > 0)
                {
                    ll.GetInterNode().Length += LeafLayerInternodeLengthGrowthList[ll.Index];
                }            
			}
            if (EarPeduncle != null)
            {
                var previousll = AllLeaves[EarPeduncle.Index - 1];
                EarPeduncle.InterNode_.Length += EarPeduncleInternodeLengthGrowth;
            }
        }

        public void resetDeltaAI()
        {
            for (int i = 0; i < AllLeaves.Count; i++)
            {
                AllLeaves[i].DeltaAI = 0;
            }
        }

        public void GrowInterNodes(double[] cumulTT, double deltaTTShoot)
        {
            
            calculateInternodeGrowth.estimate(convertVectorSelectionToList(AllLeaves),EarPeduncle,cumulTT,deltaTTShoot);

            //update the plant structure
            UpdateInternodes(calculateInternodeGrowth.getLeafLayerInternodeLengthGrowthList(), calculateInternodeGrowth.EarPeduncleInternodeLengthGrowth);

        }

        public bool getIsLatestLeafInternodeLengthPotPositive() 
        {
            if (AllLeaves.Count < 1) { return false; }
            else { return AllLeaves[AllLeaves.Count - 1].GetInterNode().LengthPot >0; }
        }

        /// <summary>Get the leaf laminae on wich the Ni optimization should be applied</summary>
        /// <returns>An array containing the Leaf Layers containing all leaf laminae that should optimize their Ni</returns>
        public LeafLayer[] GetNopLaminaeLL()
        {
            #region new implementation 
            
            /*LeafLaminae[] op = new LeafLaminae[0];
            foreach (LeafLayer leafLayer in GrowingLeaves)
            {
                if (leafLayer.isLaminaeOptimizableN())
                {
                    Array.Resize(ref op, op.Length + 1);
                    op[op.Length - 1] = leafLayer.GetLeafLamina();
                }
            }
            foreach (LeafLayer leafLayer in MatureLeaves)
            {
                if (leafLayer.isLaminaeOptimizableN())
                {
                    Array.Resize(ref op, op.Length + 1);
                    op[op.Length - 1] = leafLayer.GetLeafLamina();
                }
            }

            Array.Sort(op, LeafLaminae.CompareLeafLaminaeByIndex);
            */
            #endregion

            #region old implementation 
            var op = new LeafLayer[0];
            foreach (var leafLayer in AllLeaves)
            {
                if (leafLayer.IsLaminaeOptimizableN)
                {
                    Array.Resize(ref op, op.Length + 1);
                    op[op.Length - 1] = leafLayer;
                }
            }

            Array.Sort(op, LeafLayer.CompareLeafLayerByIndex);
            #endregion
            return op;
        }

        ///<summary>Get the exposed sheaths on wich the Ni optimization should be applied</summary>
        ///<returns>An array containing the Leaf Layers containing all exposed sheaths that should optimize their Ni.</returns>
        public LeafLayer[] GetNopSheathLL()
        {
            #region new implementation
            /*
            ExposedSheath[] op = new ExposedSheath[0];
            foreach (LeafLayer leafLayer in GrowingLeaves)
            {
                if (leafLayer.isSheathOptimizableN())
                {
                    Array.Resize(ref op, op.Length + 1);
                    op[op.Length - 1] = leafLayer.GetExposedSheath();
                }
            }
            foreach (LeafLayer leafLayer in MatureLeaves)
            {
                if (leafLayer.isSheathOptimizableN())
                {
                    Array.Resize(ref op, op.Length + 1);
                    op[op.Length - 1] = leafLayer.GetExposedSheath();
                }
            }
            
            Array.Sort(op, ExposedSheath.CompareExposedSheathByIndex);
            */
            #endregion

            #region old implementation

            var op = new LeafLayer[0];
            foreach (var leafLayer in AllLeaves)
            {
                if (leafLayer.IsSheathOptimizableN)
                {
                    Array.Resize(ref op, op.Length + 1);
                    op[op.Length - 1] = leafLayer;
                }
            }

            Array.Sort(op, LeafLayer.CompareLeafLayerByIndex);

            #endregion 

            return op;
        }


        ///<summary>Apply senescence on senescing layers</summary>
        ///<param name="remobN">Quantity of Ni released by senescence</param>
        ///<param name="remobDM">Quantity of DM released by senescence</param>
        public void Senescence(ref double remobN, ref double remobDM)
        {
            foreach (var leafLayer in SenescingLeaves)
            {
                leafLayer.Senescence(ref remobN, ref remobDM, switchLAI_.getWaterLimitedPotDeltaAI(leafLayer.Index));
            }
        }

        ///<summary>Apply degradation on senesced layers</summary>
        ///<param name="DeltaTTShoot">Shoot thermal time increment of the day</param>
        public void Degradation(double DeltaTTShoot)
        {
            foreach (var leafLayer in SenescingLeaves)
            {
                leafLayer.Degradation(DeltaTTShoot);
            }

            foreach (var leafLayer in DeadLeaves)
            {
                leafLayer.Degradation(DeltaTTShoot);
            }

        }


        ///<summary>Real delta area index of the day</summary>
        ///<param name="potentialIncDeltaArea">Potential area index created this day</param>
        ///<param name="incDeltaArea">Area index created this day</param>
        public void UpdateAreas( out double incDeltaArea, double cumulTTShoot, double availN )
        {

            if (switchLAI_.getPotentialIncDeltaArea() > 0)
            {
                /// Behnam (2016.02.25): when getIncDeltaAreaLimitSF==0, incDeltaArea had become Inf.
                if (switchLAI_.getIncDeltaAreaLimitSF() == 0)
                {
                    incDeltaArea = 0;
                }
                else
                {
                    incDeltaArea = switchLAI_.getIncDeltaAreaLimitSF() * Math.Min(1.0, availN / (switchLAI_.getIncDeltaAreaLimitSF() * SLNcri));
                    Check.IsNumber(incDeltaArea);
                }

                var stressGrowth = incDeltaArea / switchLAI_.getPotentialIncDeltaArea();

                foreach (var leafLayer in AllLeaves)
                {
                    if (switchLAI_.getWaterLimitedPotDeltaAI(leafLayer.Index) > 0) // leaf layer is growing
                    {
                        leafLayer.IncAreaIndex(stressGrowth, cumulTTShoot, switchLAI_.getWaterLimitedPotDeltaAI(leafLayer.Index));
                    }
                }
            }
            else incDeltaArea = 0;

            foreach (var leafLayer in SenescingLeaves)
            {
                leafLayer.DecAreaIndex(switchLAI_.getWaterLimitedPotDeltaAI(leafLayer.Index));
            }
        }

        // #Andrea 01/12/2015 - added double canopyTMin, double canopyTmax,
        ///<summary>Calculate the quantity of DM produced by green layers</summary>
        ///<param name="deltaShootDM">Quantity of DM produced this day</param>
        public void CalculateDeltaShootDM(double radTopAtm, double deltaTTShoot, double canopyTMin, double canopyTmax, double rad, double PAR, double DBF, double dailyCO2)
        {    	
            DeltaShootDM = 0;
            var cumulGai = 0.0;
            TotalPAR = 0;
            TotalLUE = 0;
            for (var i = AllLeaves.Count - 1; i >= 0; --i)
            {
                LeafLayer leafLayer = AllLeaves[i];
                LeafLaminae lamina = leafLayer.GetLeafLamina();
                double laminaAi = lamina.AreaIndex;
                double sheathAi = leafLayer.GetExposedSheath().AreaIndex;

                //layerParInt
                double layerPar = GetPAR(PAR, cumulGai);// pm 14 April 2016: consider that sheath intercept PAR and contribute to biomass production.              
                double layerParInt = GetPARint(layerPar, laminaAi);

                //LayerLue
                double layerS = GetSi(rad, radTopAtm, layerPar,PAR);
                double layerLue = calculateLayerLUE_.GetLue(lamina.SpecificN, GetQ(layerS), canopyTMin, canopyTmax, DBF,dailyCO2);

                leafLayer.DeltaDM =layerParInt * layerLue;
                DeltaShootDM += leafLayer.DeltaDM ;
                cumulGai += laminaAi + sheathAi;

                /// Behnam (2016.02.10): Shoot total PAR.
                TotalPAR += layerParInt;
            }

            /// Behnam (2016.02.10): Shoot (global) LUE.
            TotalLUE = (TotalPAR == 0) ? 0 : DeltaShootDM / TotalPAR;
        }

        ///<summary>Grow the growable layers</summary>
        ///<param name="potentialIncDeltaArea">Potential area index increment of the day</param>
        ///<param name="incDeltaArea">Area index increment of the day</param>
        ///<param name="remobDM"></param>
        ///<param name="excessDeltaLeafDM">DM not used by the growing leaves</param>
        public void Grow( double incDeltaArea, double remobDM, out double excessDeltaLeafDM, double cumulTTShoot)
        {
            // no Layer DM accumulation if their is no increment of area index.
            if (switchLAI_.getPotentialIncDeltaArea() > EPS && incDeltaArea > EPS)
            {
                double potentialDeltaLeafDM = 0;
                foreach (var leafLayer in AllLeaves)
                {
                    potentialDeltaLeafDM += leafLayer.getPotentialDeltaDM(cumulTTShoot, switchLAI_.getWaterLimitedPotDeltaAI(leafLayer.Index));
                }

                var deltaLeafDM = Math.Min((DeltaShootDM + remobDM), potentialDeltaLeafDM);
                excessDeltaLeafDM = (DeltaShootDM + remobDM) - deltaLeafDM;

                var stressDM = deltaLeafDM / potentialDeltaLeafDM;

                Check.IsPositiveOrZero(stressDM);
                Check.IsLessOrEqual(stressDM, 1);

                foreach (var leafLayer in GrowingLeaves)
                {
                    leafLayer.Grow(stressDM, cumulTTShoot);
                }

            }
            else
            {
                excessDeltaLeafDM = (DeltaShootDM + remobDM);
            }
        }

        ///<summary>Take N from stem, root an other phytomers to satisfy the growth of growable layers</summary>
        ///<param name="incDeltaArea">Area index increment of the day</param>
        public void SatisfyDemandForGrowthN(double incDeltaArea,double cumulTTShoot,double remobDM)
        {
            //If Ni uptake releases DM, this DM is allocated to the growing layers in proporition to their delta area index.
            if (remobDM > 0)
            {
                /// Behnam (2016.02.12): Mature and growing leaves are now considered.
                //foreach (var leafLayer in GrowingLeaves)
                for (var i = GrowingLeaves.Count - 1; i >= 0; --i)
                {
                    var leafLayer = GrowingOrMatureLeaves[i];
                    if ((cumulTTShoot - leafLayer.TTem) < leafLayer.LayerPhyllochron * PexpL || SwitchMaize)// leafLayer.IsLeafLaminaeGrowing . In case of Maize only laminae grows
                    {
                        leafLayer.GetLeafLamina().DM.Labile += remobDM * leafLayer.DeltaAI / incDeltaArea;
                    }
                    else
                    {
                        leafLayer.GetExposedSheath().DM.Labile += remobDM * leafLayer.DeltaAI / incDeltaArea;
                    }
                }
            }
        }

        public double CalculateLaminaeDemandLabileN()
        {
            var opLaminaeLL = GetNopLaminaeLL();
            double leafDemandLabileN = 0;
            var nbLamiae = opLaminaeLL.Length;
            if (nbLamiae > 0)
            {
                if (nbLamiae > 1)
                {
                    var aboveAreaIndex = 0.0;
                    for (var i = nbLamiae - 2; i >= 0; --i)
                    {
                        var leafLaminaeLL = opLaminaeLL[i];
                        leafLaminaeLL.setLaminaPotentialSpecificN ((SLNmax0 - SLNmin) * Math.Exp(-Kn * aboveAreaIndex) + SLNmin);
                        aboveAreaIndex += leafLaminaeLL.getLaminaAreaIndex();
                    }
                    opLaminaeLL[nbLamiae - 1].setLaminaPotentialSpecificN(SLNmax0);
                }
                else
                {
                    opLaminaeLL[0].setLaminaPotentialSpecificN(SLNmax0);
                }
                foreach (var leafLaminaeLL in opLaminaeLL)
                {
                    leafDemandLabileN += (leafLaminaeLL.getLaminaPotentialSpecificN() - leafLaminaeLL.getLaminaSpecificN()) * leafLaminaeLL.getLaminaAreaIndex();
                }
            }

            return leafDemandLabileN;
        }
        
        public double CalculateSheathDemandLabileN()
        {
            var opSheathLL = GetNopSheathLL();  
            double sheathDemandLabileN = 0;
            var nbSheath = opSheathLL.Length;
            if (nbSheath > 0)
            {
                for (var i = 0; i < nbSheath; ++i)
                {
                    var sheathLL = opSheathLL[i];
                    var potSSNi = AlphaSSN * Math.Pow(sheathLL.getLaminaPotentialSpecificN() - SLNmin, BetaSSN) + SLNmin;
                    Check.IsNumber(potSSNi);
                    sheathLL.setSheathPotentialSpecificN(potSSNi); 
                }
                foreach (var leafLayer in opSheathLL)
                {
                    sheathDemandLabileN += (leafLayer.getSheathPotentialSpecificN() - leafLayer.getSheathSpecificN()) * leafLayer.getSheathAreaIndex();
                }
            }

            return sheathDemandLabileN;
        }

        public void UpdateLaminaeVerticalNdistribution(double availN, double laminaeDemandLabileN)
        {
            var opLaminaeLL = GetNopLaminaeLL();
            var nbLaminae = opLaminaeLL.Length;
            if (nbLaminae > 0)
            {
                double leafN = 0;
                if (laminaeDemandLabileN > 0)
                {
                    leafN += availN;
                }
                else
                {
                    ///?
                    leafN += laminaeDemandLabileN;
                }

                if (nbLaminae > 1)
                {
                    var sum = 0.0;
                    var aboveAreaIndex = 0.0;
                    for (var i = nbLaminae - 1; i >= 0 ; --i)
                    {
                        var leafLaminaLL = opLaminaeLL[i];
                        leafN += leafLaminaLL.getLaminaNGreen();
                        sum += leafLaminaLL.getLaminaAreaIndex() * Math.Exp(-Kn * aboveAreaIndex);

                        aboveAreaIndex += leafLaminaLL.getLaminaAreaIndex();
                    }

                    var slNtop = SLNmin + (leafN - SLNmin * aboveAreaIndex) / sum;

                    aboveAreaIndex = opLaminaeLL[nbLaminae - 1].getLaminaAreaIndex();
                    for (var i = nbLaminae - 2; i >= 0 ; --i)
                    {
                        var leafLaminaLL = opLaminaeLL[i];

                        var leafAreaIndex = leafLaminaLL.getLaminaAreaIndex();
                        var slNi = (slNtop - SLNmin) * Math.Exp(-Kn * aboveAreaIndex) + SLNmin;
                        leafLaminaLL.setLaminaNLabile( (slNi * leafAreaIndex) - leafLaminaLL.getLaminaNStruct());
                        aboveAreaIndex += leafAreaIndex;
                    }

                    opLaminaeLL[nbLaminae - 1].setLaminaNLabile( (slNtop * opLaminaeLL[nbLaminae - 1].getLaminaAreaIndex()) - opLaminaeLL[nbLaminae - 1].getLaminaNStruct());
                }
                else
                {
                    leafN += opLaminaeLL[0].getLaminaNGreen();
                    opLaminaeLL[0].setLaminaNLabile(leafN - opLaminaeLL[0].getLaminaNStruct());
                }
            }
        }
        public void UpdateSheathVerticalNdistribution(double availN, double sheathDemandLabileN)
        {
            var opSheathLL = GetNopSheathLL();  
            var nbSheath = opSheathLL.Length;
            if (nbSheath > 0)
            {
                double sheathN = 0;
                if (sheathDemandLabileN > 0)
                {
                    sheathN += availN;
                }
                else
                {
                    // ?
                    sheathN += sheathDemandLabileN;
                }

                if (nbSheath > 1)
                {

                    var totalSheathAreaIndex = 0.0;
                    var sum = 0.0;
                    for (var i = 0; i < nbSheath; ++i)
                    {
                        var sheathLL = opSheathLL[i];
                        totalSheathAreaIndex += sheathLL.getSheathAreaIndex();

                        sheathN += sheathLL.getSheathNGreen();

                        var specificNminusSLNmin = sheathLL.getLaminaSpecificN() - SLNmin;

                        ///<Behnam and Pierre MO (2015.12.17)>
                        if (specificNminusSLNmin < 0) specificNminusSLNmin = 0;
                        ///</Behnam>

                        Check.IsPositiveOrZero(specificNminusSLNmin);
                        sum += Math.Pow(specificNminusSLNmin, BetaSSN) * sheathLL.getSheathAreaIndex();
                    }

                    var alpha = (sheathN - SLNmin * totalSheathAreaIndex) / sum;  // Eq. 59 of the manual
                    
                    ///<Behnam (2015.12.17)>
                    ///<Comment>To keep Alpha from becoming infinite</Comment>
                    if (sum==0) alpha = 0;
                    ///</Behnam>

                    for (var i = 0; i < nbSheath; ++i)
                    {
                        var sheathLL = opSheathLL[i];

                        var specificNminusSLNmin = sheathLL.getLaminaSpecificN() - SLNmin;

                        ///<Behnam and Pierre MO (2015.12.17)>
                        if (specificNminusSLNmin < 0) specificNminusSLNmin = 0;
                        ///</Behnam>
                        
                        Check.IsPositiveOrZero(specificNminusSLNmin);
                        sheathLL.setSheathNLabile(Math.Max(0, (alpha * Math.Pow(specificNminusSLNmin, BetaSSN) + SLNmin) * sheathLL.getSheathAreaIndex() - sheathLL.getSheathNStruct())); // 24/05/2012 (strat): Check that N.Labile is not set < 0
                    }
                }
                else
                {
                    sheathN += opSheathLL[0].getSheathNGreen();
                    opSheathLL[0].setSheathNLabile( Math.Max(0, sheathN - opSheathLL[0].getSheathNStruct()));
                }
            }
        }

        ///<summary>Use Ni released by senescence this day and not use by the plant and increment dead Ni in senescing layers</summary>
        ///<returns>increment value of Stem Nlabile </returns>
        public double  UseSenescencePool(double notUsed)
        {
            var dailyLabileSenescenceN = GetDailyLabileSenescenceN();
    	
            if (dailyLabileSenescenceN > 0)
            {
                if (notUsed > 0)
                {
                    foreach (var leafLayer in AllLeaves)
                    {
                        if (leafLayer.DailyLabileSenescenceN > 0)
                        {
                            leafLayer.UseSenescencePool(notUsed * leafLayer.DailyLabileSenescenceN / dailyLabileSenescenceN);
                        }
                    }
                }
                return 0;
            }
            else
            {
                var notUsedRounded = notUsed;
                RoundZero(ref notUsedRounded);
                return notUsedRounded;
            }
        }

        public double UptakeDailyLabileN(double needN, ref double remobDM)
        {
            double foundN = 0;
            var availN = CountDailyLabileN();
    	
            if (needN > 0 && availN > 0)
            {
                var ratio = Math.Min(1.0, needN / availN);

                foreach (var leafLayer in AllLeaves)
                {
                    if (leafLayer.State != LeafState.Dead)
                    {
                        foundN += leafLayer.UptakeDailyLabileN(leafLayer.CountDailyLabileN() * ratio, ref remobDM);
                    }
                }
            }
            return foundN;
        }

        public double EvaluateUptakeLabileN(double needN, ref double remobDM)
        {
            double foundN = 0;
            var availN = CountLabileN();

            if (needN > 0 && availN > 0)
            {
                double ratio = Math.Min(1.0, needN / availN);

                foreach (var leafLayer in AllLeaves)
                {
                    if (leafLayer.State != LeafState.Dead)
                    {
                        foundN += leafLayer.EvaluateUptakeLabileN(leafLayer.CountLabileN() * ratio, ref remobDM);
                    }
                }
            }
            return foundN;
        }


        public double UptakeLabileN(double needN, ref double remobDM)
        {
            double foundN = 0;
            var availN = CountLabileN();

            if (needN > 0 && availN > 0)
            {
                double ratio = Math.Min(1.0, needN / availN);

                foreach (var leafLayer in AllLeaves)
                {
                    if (leafLayer.State != LeafState.Dead)
                    {
                        foundN += leafLayer.UptakeLabileN(leafLayer.CountLabileN() * ratio, ref remobDM);
                    }
                }
            }
            return foundN;
        }

        public double CountDailyLabileN()
        {
            double foundN = 0;

            foreach (var leafLayer in AllLeaves)
            {
                if (leafLayer.State != LeafState.Dead)
                {
                    foundN += leafLayer.CountDailyLabileN();
                }
            }
            return foundN;
        }

        public double CountLabileN()
        {
            double foundN = 0;

            foreach (var leafLayer in AllLeaves)
            {
                if (leafLayer.State != LeafState.Dead)
                {
                    foundN += leafLayer.CountLabileN();
                }
            }
            return foundN;
        }

        private double GetDailyLabileSenescenceN()
        {
            double result = 0;
            foreach (var leafLayer in SenescingLeaves)
            {
                result += leafLayer.DailyLabileSenescenceN;
            }
            return result;
        }

        public LeafLayer GetLeafLayer(int i)
        {
            LeafLayer result = null;
            if (i < AllLeaves.Count)
            {
                result = AllLeaves[i];
            }
            return result;
        }        
      
        public double GAI
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.GAI;
                }
                return result;
            }
        }

        
        ///<summary>Get the PAR intercepted by the Layer i</summary>
        ///<param name="globalPar"></param>
        ///<param name="cumulAboveAi"></param>
        ///<returns>Returns the PAR transmitted to the Layer i</returns>
        private double GetPAR(double globalPar, double cumulAboveAi)
        {
            var result = globalPar * Math.Exp(-Kl * cumulAboveAi);
            Check.IsNumber(result);
            return result;
        }

        // Get the PARint of the Layer i.
        ///<summary>Get the PAR interceptd by the Layer i (PARint)</summary>
        ///<param name="layerPar"></param>
        ///<param name="layerAi"></param>
        ///<returns>Returns the PAR intercepted by the Layer i (PARint)</returns>
        private double GetPARint(double layerPar, double layerAi)
        {
            var result = layerPar * (1 - Math.Exp(-Kl * layerAi));
            Check.IsNumber(result);
            return result;
        }

        ///<summary>Get the canopy transmission ratio at the top of the Layer i (SI) for LUE</summary>
        ///<param name="globalS"></param>
        ///<param name="layerPar"></param>
        ///<param name="globalPar"></param>
        ///<returns>Return the canopy transmission ratio at the top of the Layer i (Si) for LUE</returns>
        public double GetSi(double globalRad, double radTopAtm , double layerPar, double globalPar)
        {

            // Assuming that the diffusion of light within the canopy is sufficiently similar to diffusion of light in the atmosphere 
            // the canopy transmission ratio at the top of each leaf layer (S(i)) is calculated follows
            var S = globalRad / radTopAtm;  // the atmospheric transmission ratio

            return S * layerPar / globalPar;
        }

        ///<summary>Get the Q ratio of the Layer i (Qi) for LUE</summary>
        ///<param name="s"></param>
        ///<returns>\return the Q ratio of the Layer i (Qi) for LUE</returns>
        public double GetQ(double s)
        {
            double result;

            if (s < 0.07)
            {
                result = 1;
            }
            else if (0.07 <= s && s < 0.35)
            {
                result = 1 - 2.3 * Math.Pow(s - 0.07, 2);
            }
            else if (0.35 <= s && s < 0.75)
            {
                result = 1.33 - 1.46 * s;
            }
            else
            {
                result = 0.23;
            }

            return result;
        }


        #region properties

        ///<summary>Get the potential water on the leaves</summary>
        public double PotentialWaterOnLeaves
        {
            get
            {
                const double h2OPerLai = 500;
                return h2OPerLai*GAI;
            }
        }

        public double TotalDM
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.TotalDM;
                }
                return result;
            }
        }

        public double OutputTotalDM
        {
            get
            {
                var result = 0.0;
                ForEachOutputLayer(ll => result += ll.TotalDM);
                return result;
            }
        }

        public double TotalN
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.TotalN;
                }
                return result;
            }
        }

        public double OutputTotalN
        {
            get
            {
                var result = 0.0;
                ForEachOutputLayer(ll => result += ll.TotalN);
                return result;
            }
        }

        public double AliveDM
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.AliveDM;
                }
                return result;
            }
        }

        public double AliveN
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.AliveN;
                }
                return result;
            }
        }

        public double StructDM
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.StructDM;
                }
                return result;
            }
        }

        public double StructN
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.StructN;
                }
                return result;
            }
        }

        public double LabileDM
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.LabileDM;
                }
                return result;
            }
        }

        public double LabileN
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.CountLabileN();
                }
                return result;
            }
        }

        public double DeadDM
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.DeadDM;
                }
                return result;
            }
        }

        public double LostDM
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.LostDM;
                }
                return result;
            }
        }

        

        public double DeadN
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.DeadN;
                }
                return result;
            }
        }

        public double SheathDM
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.GetExposedSheath().DM.Total;
                }
                return result;
            }
        }

        public double OutputSheathDM
        {
            get
            {
                var result = 0.0;
                ForEachOutputLayer(ll => result += ll.GetExposedSheath().DM.Total);
                return result;
            }
        }

        public double SheathN
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.GetExposedSheath().N.Total;
                }
                return result;
            }
        }

        public double OutputSheathN
        {
            get
            {
                var result = 0.0;
                ForEachOutputLayer(ll => result += ll.GetExposedSheath().N.Total);
                return result;
            }
        }

        public double LaminaeDM
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.GetLeafLamina().DM.Total;
                }
                return result;
            }
        }

        public double OutputLaminaeDM
        {
            get
            {
                var result = 0.0;
                ForEachOutputLayer(ll => result += ll.GetLeafLamina().DM.Total);
                return result;
            }
        }

        public double LaminaeN
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.GetLeafLamina().N.Total;
                }
                return result;
            }
        }

        public double OutputLaminaeN
        {
            get
            {
                var result = 0.0;
                ForEachOutputLayer(ll => result += ll.GetLeafLamina().N.Total);
                return result;
            }
        }

        public double LaminaeAliveN
        {
            get
            {
                double result = 0;
                foreach (var layer in AllLeaves)
                {
                    result += layer.GetLeafLamina().N.Green;
                }
                return result;
            }
        }

        public double LaminaeSLW
        {
            get
            {
                double dm = 0;
                double gai = 0;
                foreach (var layer in AllLeaves)
                {
                    dm += layer.GetLeafLamina().DM.Green;
                    gai += layer.GetLeafLamina().AreaIndex;
                }
                return (gai != 0) ? dm / gai : 0;
            }
        }

        public double LaminaeSLN
        {
            get
            {
                double n = 0;
                double gai = 0;
                foreach (var layer in AllLeaves)
                {
                    n += layer.GetLeafLamina().N.Green;
                    gai += layer.GetLeafLamina().AreaIndex;
                }
                return (gai != 0) ? n / gai : 0;
            }
        }

        public double LaminaeAI
        {
            get
            {
                double ai = 0;
                foreach (var layer in AllLeaves)
                {
                    ai += layer.GetLeafLamina().AreaIndex;
                }
                return ai;
            }
        }

        public double Tau
        {
            get { return Math.Exp(-Kl * LaminaeAI); }
        }

        #endregion
        ///<summary>
        ///PS-30/09/2009: Function added to consider only the last 5 leaves in the calculated of leaf DM and N and to 
        ///               calculated crop DM in the output files.
        ///</summary>
        // public const int NbOutputLayer = 5;

        ///<Behnam (2015.10.27)>
        ///<Comment>
        ///Previously only last 5 leaves were considered to calculate leaf DM and N.
        ///Now, the same setting has been preserved, but a large value of NbOutputLayer
        ///is used to consider all the leaf layers.
        ///</Comment>
        public const int NbOutputLayer = 50;
        ///</Behnam>
        
        public void ForEachOutputLayer(Action<LeafLayer> toDo)
        {
            var allLeaves = AllLeaves;
            var nbLeaves = allLeaves.Count;
            for (var i = Math.Max(0, nbLeaves - (NbOutputLayer + 1)); i < nbLeaves; ++i)
            {
                toDo(allLeaves[i]);
            }
        }       

        public override void Dispose()
        {
            base.Dispose();
            if (EarPeduncle != null)
            {
                EarPeduncle.Dispose();
                EarPeduncle = null;
            }
            if (All != null)
            {
                var allCount = All.Count;
                for (var i = 0; i < allCount; ++i)
                {
                    var p = All[i];
                    if (p != null)
                    {
                        p.Dispose();
                        All[i] = null;
                    }
                }
                All.Dispose();
            }
            if (AllLeaves != null)
            {
                AllLeaves.Dispose();
                AllLeaves = null;
            }
            if (GrowingLeaves != null)
            {
                GrowingLeaves.Dispose();
                GrowingLeaves = null;
            }
            if (MatureLeaves != null)
            {
                MatureLeaves.Dispose();
                MatureLeaves = null;
            }
            if (GrowingOrMatureLeaves != null)
            {
                GrowingOrMatureLeaves.Dispose();
                GrowingOrMatureLeaves = null;
            }
            if (SenescingLeaves != null)
            {
                SenescingLeaves.Dispose();
                SenescingLeaves = null;
            }
            if (DeadLeaves != null)
            {
                DeadLeaves.Dispose();
                DeadLeaves = null;
            }
        }

        public void growLeafLayer(double previousLeafNumber, double cumulTTShoot, double deltaTTShoot, double leafNumber, int roundedFinalNumber, double finalLeafNumber, double phyllochron, double deltaTTPhenoMaize, double cumulTTPhenoMaizeAtEmergence)
        {
            HasNewLeafAppeared = false;
            if (finalLeafNumber > 0)
            {
                // final number of phytomer is known.
                if (leafNumber <= roundedFinalNumber)
                {
                    bool CreateOrNot = false;
                    if (leafNumber > 0) { CreateOrNot = true; }

                    if (((int)previousLeafNumber == (int)leafNumber) && (previousLeafNumber > 0))
                    {
                        CreateOrNot = false;
                        // a leaf Layer is growing, do nothing.
                    }

                    if (CreateOrNot)
                        CreateLeafLayer(leafNumber, previousLeafNumber, cumulTTShoot, deltaTTShoot, roundedFinalNumber, finalLeafNumber, phyllochron, deltaTTPhenoMaize, cumulTTPhenoMaizeAtEmergence);
                }
                else
                {
                    // ear phytomer is appearing or growing.
                    if (EarPeduncle == null)
                    {
                        EarPeduncle = new EarPeduncle(Universe_, (int)leafNumber, cumulTTShoot, roundedFinalNumber, finalLeafNumber);
                    }
                }
            }
            else
            {
                bool CreateOrNot = false;
                if (leafNumber > 0) { CreateOrNot = true; }
                
                if ((Math.Floor(previousLeafNumber) == Math.Floor(leafNumber)) && (previousLeafNumber > 0))
                {
                    CreateOrNot = false;
                    // a leaf Layer is growing, do nothing.
                }

                if (CreateOrNot)
                    CreateLeafLayer(leafNumber, previousLeafNumber, cumulTTShoot, deltaTTShoot, roundedFinalNumber, finalLeafNumber, phyllochron, deltaTTPhenoMaize, cumulTTPhenoMaizeAtEmergence);
            }
        }

        public void calculateLAI(int roundedFinalNumber, double finalLeafNumber, double leafNumber, double FPAW, double cumulTTShoot, double deltaTTShoot, double deltaTTSenescence, double VPDairCanopy, double LER, List<double> tilleringProfile, List<double> leafTillerNumberArray, double cumulTTPhenoMaize, double deltaTTPhenoMaize, double[] deltaTTCanopyHourly, double[] VPDeq)
        {
            switchLAI_.Estimate(HasNewLeafAppeared, roundedFinalNumber, finalLeafNumber, leafNumber,
            getNewLeafIsSmallPhytomer(), getNewLeafLastPhytoNum(), getNewLeafindex(), FPAW, false, cumulTTShoot, deltaTTShoot, deltaTTSenescence, convertVectorSelectionToList(AllLeaves), VPDairCanopy, LER, tilleringProfile, leafTillerNumberArray, cumulTTPhenoMaize, deltaTTPhenoMaize, deltaTTCanopyHourly, VPDeq);

            UpdateStates(switchLAI_.getLeafStateList(), switchLAI_.getIsPrematurelyDyingList());

            resetDeltaAI();
        }

        public List<LeafLayer> convertVectorSelectionToList(VectorSelection<Phytomer, LeafLayer> vector)
        {
            List<LeafLayer> liste = new List<LeafLayer>();
            for (int i = 0; i < vector.Count; i++)
            {                
                liste.Add(vector[i]);
            }
            return liste;
        }
    }
}