using System;
using SiriusModel.Model.Base;
using SiriusModel.Model.Observation;
using SiriusModel.Model.Phenology;
using System.Collections.Generic;
using SiriusQualityPhenology;

namespace SiriusModel.Model.CropModel
{
    ///<summary>The crop of the universe</summary>
    public class Crop : UniverseLink
    {
        #region Fields 

        public bool switchAnthesis;
        private bool switchEndCD;

        ///<Behnam (2015.10.28)>
        public bool isUnlimitedWater;
        public bool isUnlimitedNitrogen;
        public bool isUnlimitedTemperature;
        ///</Behnam>

        /// Change in CO2 concentration based on a pre-defined annual trend.
        ///<Behnam (2016.06.30)>
        public double DailyCO2;
        public double CO2Change = 1;
        ///</Behnam>

        ///<Behnam (2016.01.20)>
        public double TotalPAR { get { return shoot.TotalPAR; } }
        public double TotalLUE { get { return shoot.TotalLUE; } }
        public double AccumPAR {get; private set;}
        ///</Behnam>

        ///<summary>NNI at the end of cell division</summary>
        private double nniEndCD;

        ///<summary>To store amount of N required to satisfy N balance under unlimited conditions</summary>
        public double VirtualNReq;

        ///<summary>current NNI of the crop calculated at the beginning of the day step (after anthesis it always returns nniAnth</summary>
        public double NNI { get; set; }

        ///<summary>Root compartment</summary>
        private Root root;

        ///<summary>Shoot compartment</summary>
        private Shoot shoot;

        private GrainEarDW grainEarDW_;
        ///<summary>Get post anthesis N uptake</summary>
        public double PostAnthesisNUptake { get; private set; }

        ///<summary>Vernalization progress</summary>
        public double VernaProgress { get { return phenology_.VernaProg; } }

        public double LeafNumber { get { return phenology_.LeafNumber; } }

        public double Ntip { get { return phenology_.Ntip; } }

        public int CreatedLeavesNumber { get { return shoot.CreatedLeavesNumber; } }

        public double FinalLeafNumber { get { return phenology_.FinalLeafNumber; } }

        public int RoundedFinalLeafNumber { get { return phenology_.RoundedFinalLeafNumber; } }

        public double LaminaeAI { get { return shoot.LaminaeAI; } }

        public double GAI { get { return shoot.GAI; } }

        public double Ncp { get { return shoot.Ncp; } }

        public double TotalDMperGrain { get { return shoot.TotalDMperGrain; } }

        public double TotalNperGrain { get { return shoot.TotalNperGrain; } }

        public double ProteinConcentration { get { return shoot.ProteinConcentration; } }

        public double GrainNumber { get { return shoot.GrainNumber; } }

        public double PercentStarch { get { return shoot.PercentStarch; } }

        public double NalbGlo { get { return shoot.NalbGlo; } }

        public double Namp { get { return shoot.Namp; } }

        public double Ngli { get { return shoot.Ngli; } }

        public double Nglu { get { return shoot.Nglu; } }

        public double LER { get { return shoot.LER; } }

        public double PercentGli { get { return shoot.PercentGli; } }

        public double PercentGlu { get { return shoot.PercentGlu; } }

        public double GliadinsToGluteins { get { return shoot.GliadinsToGluteins; } }

        public double HarvestIndexDM { get { return shoot.getHarvestIndexDM(CropTotalDM); } }

        public double HarvestIndexN { get { return shoot.getHarvestIndexN(CropTotalN); } }

        public double GrainTotalDM { get { return shoot.GrainTotalDM; } }

        public double NutilisationEfficiency { get { return shoot.getNutilisationEfficiency(CropTotalN); } }

        public double Starch { get { return shoot.Starch; } }

        public double NStorage { get { return shoot.NStorage; } }

        public double GrainStructDM { get { return shoot.GrainStructDM; } }

        public double GrainLabileDM { get { return shoot.GrainLabileDM; } }

        public double GrainGreenDM { get { return shoot.GrainGreenDM; } }

        public double GrainDeadDM { get { return shoot.GrainDeadDM; } }

        public double RootLength { get { return root.Length; } }

        public double DEF { get { return shoot.DEF; } }

        public double DSF { get { return shoot.DSF; } }

        public double OutputSheathDM { get { return shoot.OutputSheathDM; } }

        public double OutputSheathN { get { return shoot.OutputSheathN; } }

        public double OutputLaminaeDM { get { return shoot.OutputLaminaeDM; } }

        public double OutputLaminaeN { get { return shoot.OutputLaminaeN; } }

        public double LaminaeSLN { get { return shoot.LaminaeSLN; } }

        public double LaminaeSLW { get { return shoot.LaminaeSLW; } }

        public double SumInternodesLength { get { return shoot.SumInternodesLength; }}

        public double Tau { get { return shoot.Tau; } }

        public double PotentialWaterOnLeaves { get { return shoot.PotentialWaterOnLeaves; } }

        public double getInterNodeLengthForLeafLayer(int leafLayerIndex) { return shoot.getInterNodeLengthForLeafLayer(leafLayerIndex); }

        public double getLeafLaminaAreaIndexForLeafLayer(int leafLayerIndex) { return shoot.getLeafLaminaAreaIndexForLeafLayer(leafLayerIndex); }

        public double getLeafLaminaTotalDMForLeafLayer(int leafLayerIndex) { return shoot.getLeafLaminaTotalDMForLeafLayer(leafLayerIndex); }

        public double getLeafLaminaTotalNForLeafLayer(int leafLayerIndex) { return shoot.getLeafLaminaTotalNForLeafLayer(leafLayerIndex); }

        public double getLeafLaminaSpecificNForLeafLayer(int leafLayerIndex) { return shoot.getLeafLaminaSpecificNForLeafLayer(leafLayerIndex); }

        public double getLeafLaminaSpecificWeightForLeafLayer(int leafLayerIndex) { return shoot.getLeafLaminaSpecificWeightForLeafLayer(leafLayerIndex); }

        public double getLeafLaminaTTsinceSenForLeafLayer(int leafLayerIndex) { return shoot.getLeafLaminaTTsinceSenForLeafLayer(leafLayerIndex); }

        public double getLeafLaminaDeadDMForLeafLayer(int leafLayerIndex) { return shoot.getLeafLaminaDeadDMForLeafLayer(leafLayerIndex); }

        public double getLeafLaminaLostDMForLeafLayer(int leafLayerIndex) { return shoot.getLeafLaminaLostDMForLeafLayer(leafLayerIndex); }

        public double getExposedSheathAreaIndexForLeafLayer(int leafLayerIndex) { return shoot.getExposedSheathAreaIndexForLeafLayer(leafLayerIndex); }

        public double getExposedSheathTotalDMForLeafLayer(int leafLayerIndex) { return shoot.getExposedSheathTotalDMForLeafLayer(leafLayerIndex); }

        public double getExposedSheathTotalNForLeafLayer(int leafLayerIndex) { return shoot.getExposedSheathTotalNForLeafLayer(leafLayerIndex); }

        public double getExposedSheathSpecificNForLeafLayer(int leafLayerIndex) { return shoot.getExposedSheathSpecificNForLeafLayer(leafLayerIndex); }

        public double getExposedSheathSpecificWeightForLeafLayer(int leafLayerIndex) { return shoot.getExposedSheathSpecificWeightForLeafLayer(leafLayerIndex); }

        public double getExposedSheathTTsinceSenForLeafLayer(int leafLayerIndex) { return shoot.getExposedSheathTTsinceSenForLeafLayer(leafLayerIndex); }

        public double getExposedSheathDeadDMForLeafLayer(int leafLayerIndex) { return shoot.getExposedSheathDeadDMForLeafLayer(leafLayerIndex); }

        public double getExposedSheathLostDMForLeafLayer(int leafLayerIndex) { return shoot.getExposedSheathLostDMForLeafLayer(leafLayerIndex); }

        public double getEarPeduncleInterNodeLength() { return shoot.getEarPeduncleInterNodeLength(); }

        public double getPotentialLaminaTillerAreaIndexForLeafLayer(int leafLayerIndex, int tillerIndex) { return shoot.getPotentialLaminaTillerAreaIndexForLeafLayer(leafLayerIndex, tillerIndex, RoundedFinalLeafNumber, LeafNumber, phenology_.tilleringProfile); }

        public double getPotentialLaminaAreaIndexForLeafLayer(int leafLayerIndex) { return shoot.getPotentialLaminaAreaIndexForLeafLayer(leafLayerIndex, RoundedFinalLeafNumber, LeafNumber, phenology_.leafTillerNumberArray, phenology_.tilleringProfile); }

        public double getPotentialSheathTillerAreaIndexForLeafLayer(int leafLayerIndex, int tillerIndex) { return shoot.getPotentialSheathTillerAreaIndexForLeafLayer(leafLayerIndex, tillerIndex, RoundedFinalLeafNumber, phenology_.tilleringProfile); }

        public double getPotentialSheathAreaIndexForLeafLayer(int leafLayerIndex) { return shoot.getPotentialSheathAreaIndexForLeafLayer(leafLayerIndex, RoundedFinalLeafNumber, LeafNumber, phenology_.leafTillerNumberArray, phenology_.tilleringProfile); }

        public double getWaterLimitedPotDeltaAI(int leafLayerIndex) { return shoot.getWaterLimitedPotDeltaAI(leafLayerIndex); }

        public double getLeafLength(int i) { return shoot.getLeafLength(i); }

        public double getLeafWidth(int i) { return shoot.getLeafWidth(i); }

        public List<double> tilleringProfile { get { return phenology_.tilleringProfile; } }

        public int TillerNumber { get { return phenology_.TillerNumber; } }

        public double CanopyShootNumber { get { return phenology_.CanopyShootNumber; } }

        public double getPotentialIncDeltaArea()
        {
            return shoot.getPotentialIncDeltaArea();
        }

        public double getPhaseValue() { return phenology_.getPhaseValue(); }

        public DateTime? getDateOfStage(GrowthStage moment) { return phenology_.calendar[moment]; }

        public GrowthStage getLastGrowthStageSet() { return phenology_.calendar.LastGrowthStageSet; }
        //Debug
        public string TagGrainCN { get {return shoot.TagGrainCN; } }
        public string TagGrainCNWarnOut { get { return shoot.TagGrainCNWarnOut; } }
        public double OutUptakeRatioN { get { return shoot.OutUptakeRatioN; } }
        public double OutUptakeRatioDM { get { return shoot.OutUptakeRatioN; } }
        public double NSupplyperGrain { get { return shoot.nSupplyperGrain; } }
        public double CSupplyperGrain { get { return shoot.cSupplyperGrain; } }
        public double OriginalNSupply { get { return shoot.OriginalNSupply; } }
        public double OutCsupply { get { return shoot.OutCsupply; } }
        public double CSupplyWORemob { get { return shoot.CSupplyWORemob; } }
        public double NCRatioWRemob { get { return shoot.NCRatioWRemob; } }
        public double NCRatioWORemob { get { return shoot.NCRatioWORemob; } }
        public double Phyllochron { get { return phenology_.Phyllochron; } }
       // public double NSupplyAfterRemob { get { return shoot.NSupplyAfterRemob; } }
        //public double UEN1 { get { return shoot.UEN1; } }
        //public double UEN2 { get { return shoot.UEN2; } }
        //public double UEN3 { get { return shoot.UEN3; } }
        //Debug
        public double GrainCumulTT { get { return shoot.GrainCumulTT; } }

        ///<Behnam (2015.10.28)>
        public double OutputTotalDM_WNT;
        public double OutputTotalDM_W;
        public double OutputTotalDM_N;
        public double OutputTotalDM_T;
        public double OutputTotalDM_WN;
        public double OutputTotalDM_WT;
        public double OutputTotalDM_NT;
        ///</Modification: Behnam (2015.10.28)>

        private PhenologyBiomaWrapper phenology_ { set; get; }

        public bool areRootsToBeGrown = true;

       /* public string TagPhenoWarnOut
        {
            get {return phenology_.TagPhenoWarnOut; }

        }*/

        #endregion

        #region Constructors

        ///<summary>Initial constructor</summary>
        ///<param name="universe">The universe of this crop</param>
        public Crop(Universe universe) : base(universe)
        {
            root = new Root(universe);
            shoot = new Shoot(universe);
            phenology_ = new PhenologyBiomaWrapper(universe);
            grainEarDW_ = new GrainEarDW(universe);
            nniEndCD = NNI = -1;
            switchAnthesis = false;
            switchEndCD = false;
            PostAnthesisNUptake = 0;
            VirtualNReq = 0;
            AccumPAR = 0;
        }

        ///<summary>Copy constructor</summary>
        ///<param name="universe">The universe of this crop</param>
        ///<param name="toCopy">The crop to copy</param>
        ///<param name="copyAll">false copy only the outputs</param>
        public Crop(Universe universe, Crop toCopy, bool copyAll)
            : base(universe)
        {
            nniEndCD = toCopy.nniEndCD;
            NNI = toCopy.NNI;
            PostAnthesisNUptake = toCopy.PostAnthesisNUptake;
            root = (toCopy.root != null) ? new Root(universe, toCopy.root,copyAll) : null;
            shoot = (toCopy.shoot != null) ? new Shoot(universe, toCopy.shoot, true) : null;
            grainEarDW_ = (toCopy.grainEarDW_ != null) ? new GrainEarDW(universe, toCopy.grainEarDW_) : null;
            

            switchAnthesis = toCopy.switchAnthesis;
            switchEndCD = toCopy.switchEndCD;

            /// Behnam (2016.06.30)
            DailyCO2 = toCopy.DailyCO2;
            CO2Change = toCopy.CO2Change;

            ///<Behnam (2015.10.27)>
            OutputTotalDM_WNT = toCopy.OutputTotalDM_WNT;
            OutputTotalDM_W = toCopy.OutputTotalDM_W;
            OutputTotalDM_N = toCopy.OutputTotalDM_N;
            OutputTotalDM_T = toCopy.OutputTotalDM_T;
            OutputTotalDM_WN = toCopy.OutputTotalDM_WN;
            OutputTotalDM_WT = toCopy.OutputTotalDM_WT;
            OutputTotalDM_NT = toCopy.OutputTotalDM_NT;
            OutputTotalDMOld = toCopy.OutputTotalDMOld;

            phenology_ = (toCopy.phenology_ != null) ? new PhenologyBiomaWrapper(universe, toCopy.phenology_, copyAll) : null;
            AccumPAR = toCopy.AccumPAR;

            ///<Comment>To store amount of N required to satisfy N balance under unlimited conditions</Comment>
            VirtualNReq = toCopy.VirtualNReq;
            if (copyAll)
            {

                isUnlimitedWater = toCopy.isUnlimitedWater;
                isUnlimitedNitrogen = toCopy.isUnlimitedNitrogen;
                isUnlimitedTemperature = toCopy.isUnlimitedTemperature;
            }
        }
        #endregion

        #region Init/InitDayStep/Sow

        ///<summary>Init the crop for a new simulation</summary>
        public void Init(double[] cumulTTatSowing, DateTime sowingDate)
        {

            phenology_.Init(cumulTTatSowing, sowingDate);

            root.Init();
            shoot.Init();

            nniEndCD = -1;
            NNI = shoot.CalculateNNI();
            switchAnthesis = true;
            switchEndCD = true;

            CalcChangeinCO2Conc();
        }

        // #Andrea 01/12/2015 - renamed deltaTTShoot to deltaTTRemobilization and reference to deltaTTAir removed. removed reference to cumulTTAir as it was not used
        ///<summary>Init the crop for a new day step</summary>
        public  void InitDayStep(double deltaTTRemobilization)
        {
            CalcChangeinCO2Conc();
            OutputTotalDMOld = OutputTotalDM;
            NNI = getPhaseValue() < 4.5 ? shoot.CalculateNNI() : nniEndCD; /// Behnam (2016.02.11): According to Pierre, NNI can be estimated by the end of cell division.
            shoot.InitDayStep(deltaTTRemobilization); // #Andrea 01/12/2015 removed reference to deltaTTAir. Now there is only reference to deltaTTRemobilization
        }
		
		///<summary>Init the crop for a new day step</summary>
        //public void InitDayStepRoot(double cumulTTAir, double deltaTTAir, double deltaTTShoot, double navForRoot)
        //#Andrea 13/01/2016 - Removed double deltaTTAir, double deltaTTShoot as they were not used
        public void InitDayStepRoot(double cumulTemperature, double navForRoot)
        {
            root.InitDayStep(cumulTemperature, navForRoot, getPhaseValue(), shoot.PhytomersAliveDM, shoot.StemDMGreen, phenology_.calendar);
        }

        #endregion    

        #region Haun stage, flag leaf and zadocks
        
        
        ///<summary>Check if the crop has reached the stage "flag leaf ligule just visible"</summary>
        ///<param name="stg"> the flag leaf</param>
        ///<returns>return true if the crop has reached the stage "flag leaf ligule just visible", false if not</returns>
        public bool HasReachedFlagLeaf(int stg)
        {

           // var trgt = Phytomers_.FinalLeafNumber - (double)stg;
            var trgt = RoundedFinalLeafNumber - (double)stg;
            return (LeafNumber >= trgt && trgt > 0);
        }

        ///<summary>Check if the crop has reached a specified Haun stage</summary>
        ///<param name="stg">Haun stage to check for</param>
        ///<returns>return true if the crop has reached the specified Haun stage, false if not</returns>
       /* private bool HasReachedHaun(double stg)
        {
            var ret = false;
            //pm, 17/04/09: the line below based the Zadocks stages on final leaf number and not on current leaf number, have added the next line 
            // if (Phytomers_.FinalLeafNumber >= stg)
            if (LeafNumber >= stg)
                ret = true;

            return ret;
        }*/

      

        #endregion

        #region Grow

        // #Andrea 26/11/2015 - added double  double deltaTTRemobilization, deltaTTSenescence, double canopyTmin, double canopyTmax
        ///<summary>Ask to the crop compartments to modify their states depending to the current crop phase</summary>
        public double Grow(double radTopAtm,
            double[] cumulTT,
            double deltaTTShoot, 
            double deltaTTPhenoMaize,
            double deltaTTRemobilization,
            double deltaTTSenescence,
            double canopyTmin,
            double canopyTmax,
            double rad,
            double PAR, 
            double DBF, 
            double soilDepth,
            double DEBF,
            double DGF, 
            double dayLength,
            double FPAW,
            double VPDairCanopy,
            double[] deltaTTCanopyHourly,
            double[] VPDeq)
        {

            ///<Behnam (2015.10.28)>
            shoot.isUnlimitedWater = isUnlimitedWater;
            shoot.isUnlimitedNitrogen = isUnlimitedNitrogen;
            shoot.isUnlimitedTemperature = isUnlimitedTemperature;
            root.isUnlimitedWater = isUnlimitedWater;
            root.isUnlimitedNitrogen = isUnlimitedNitrogen;
            root.isUnlimitedTemperature = isUnlimitedTemperature;
            ///</Behnam>

            phenology_.EstimatePhenology(dayLength, deltaTTShoot, cumulTT, shoot.GrainCumulTT, GAI, shoot.getIsLatestLeafInternodeLengthPotPositive());

            var oldCropTotalN = CropTotalN;
            //Quantity of Ni to remove from soil
            double soilNDec = 0;

            //excess dry matter after phytomers and stem growth
            double excessDM;

            if (phenology_.getPhaseValue() >= 0 && phenology_.getPhaseValue() < 1)
            {
               if(areRootsToBeGrown) root.Grow(soilDepth,cumulTT[(int)Delta.Air]);
                // #Andrea 13/01/2016 - Root growth based on Soil temperature
                //root.Grow(soilDepth, cumulTT[(int)Delta.Soil]);            
            }
            else if (phenology_.getPhaseValue() >= 1 && phenology_.getPhaseValue() < 4)
            {

                // #Andrea 13/01/2016 - Root growth based on Soil temperature
                if (areRootsToBeGrown) root.Grow(soilDepth, cumulTT[(int)Delta.Air]);
                //root.Grow(soilDepth, cumulTT[(int)Delta.Soil]); 
                double availableNfromSoilAfter;

                shoot.Grow(radTopAtm, cumulTT, deltaTTShoot, deltaTTSenescence,canopyTmin,canopyTmax,rad, PAR, RoundedFinalLeafNumber,
                phenology_.FinalLeafNumber, DBF, LeafNumber, IsBeforeEndGrainCellDivision(cumulTT, phenology_.previousCalendar),
                root.availableNfromSoil, out availableNfromSoilAfter, phenology_.BeforeUpdateLeafNumber, phenology_.Phyllochron, FPAW, ref VirtualNReq,
                VPDairCanopy, LER, (phenology_.calendar.IsMomentRegistred(GrowthStage.ZC_75_EndCellDivision)==1)?true:false,phenology_.tilleringProfile,phenology_.leafTillerNumberArray,
                deltaTTPhenoMaize, phenology_.cumulTTPhenoMaizeAtEmergence,  cumulTT[(int)Delta.PhenoMaize],DailyCO2,deltaTTCanopyHourly,VPDeq,
                out excessDM);
                ///</Behnam>

                /// Behnam (2016.02.15): On Anaelle's request.
                AccumPAR += shoot.TotalPAR;

                soilNDec = root.availableNfromSoil - availableNfromSoilAfter;
                root.availableNfromSoil = availableNfromSoilAfter;//not technically useful to update root.availableNfromSoil because it is not used after but we do it anyway to avoid confusion              

				shoot.useSenescencePool();

                // calculate ear dry weight
                if (HasReachedFlagLeaf(1))
                {
                    grainEarDW_.UpdateEarDW(shoot.DeltaShootDM,DEBF);
                }
            }
            else if (phenology_.getPhaseValue() == 4 || phenology_.getPhaseValue() == 4.5)
            { //AnthesisToEndCellDivision,EndCellDivisionToEndGrainFill

                if (phenology_.getPhaseValue() == 4.5 && switchEndCD)
                 {
                     nniEndCD = shoot.CalculateNNI();
                     switchEndCD = false;
                 }

                if (phenology_.getPhaseValue() == 4 && switchAnthesis) //AnthesisToEndCellDivision
                {
                    shoot.InitAtAnthesis(grainEarDW_.EarDW);
                    switchAnthesis = false;
                    
                }
                double availableNfromSoilAfter;

                shoot.Grow(radTopAtm, cumulTT, deltaTTShoot, deltaTTSenescence,canopyTmin,canopyTmax,rad, PAR,
                        RoundedFinalLeafNumber, phenology_.FinalLeafNumber, DBF, LeafNumber,
                        IsBeforeEndGrainCellDivision(cumulTT, phenology_.previousCalendar),
                        root.availableNfromSoil, out availableNfromSoilAfter, phenology_.BeforeUpdateLeafNumber, phenology_.Phyllochron, FPAW, ref VirtualNReq,
                        VPDairCanopy, LER, (phenology_.calendar.IsMomentRegistred(GrowthStage.ZC_75_EndCellDivision)==1)?true:false, phenology_.tilleringProfile, phenology_.leafTillerNumberArray,
                        deltaTTPhenoMaize, phenology_.cumulTTPhenoMaizeAtEmergence, cumulTT[(int)Delta.PhenoMaize], DailyCO2,deltaTTCanopyHourly,VPDeq,
                        out excessDM);

                AccumPAR += shoot.TotalPAR;

                soilNDec = root.availableNfromSoil - availableNfromSoilAfter;
                root.availableNfromSoil = availableNfromSoilAfter;//not technically useful to update root.availableNfromSoil because it is not used after but we do it anyway to avoid confusion

                shoot.GrowGrain(IsBeforeEndGrainCellDivision(cumulTT, phenology_.previousCalendar), deltaTTShoot, deltaTTRemobilization, DGF, excessDM);

                shoot.useSenescencePool();
                PostAnthesisNUptake += CropTotalN - oldCropTotalN;
            }
            else if (phenology_.getPhaseValue() >= 5 && phenology_.getPhaseValue() < 7)//EndGrainFillToMaturity,AllOver 
            {
                PostAnthesisNUptake += CropTotalN - oldCropTotalN;
            }
            else if (phenology_.getPhaseValue() < 0)
            {
                throw new Exception("is running but the crop phase is BEFORE_SOWING");
            }
            else
            {
                throw new Exception("current phase is unknown");
            }

            return soilNDec;
        }

        #endregion

        #region Is ?

        public bool IsEnd
        {
            get { return (phenology_.getPhaseValue() >= 6 && phenology_.getPhaseValue() < 7); }
        }

        ///<summary>Returns true if the crop is before the end of the grain cell division</summary>
        public bool IsBeforeEndGrainCellDivision(double[] cumulTT, Calendar previousCalendar)
        {
            if (!SwitchMaize)
            {
                return ((phenology_.getPhaseValue() < 4)
                           ||
                           ((phenology_.getPhaseValue() >= 4 && phenology_.getPhaseValue() < 7) && previousCalendar.cumulTTFrom((int)Delta.Shoot, GrowthStage.ZC_65_Anthesis, cumulTT[(int)Delta.Shoot]) <= Dcd));
            }
            else 
            {
                return ((phenology_.getPhaseValue() < 4)
               ||
               ((phenology_.getPhaseValue() >= 4 && phenology_.getPhaseValue() < 7) && previousCalendar.cumulTTFrom((int)Delta.PhenoMaize, GrowthStage.ZC_65_Anthesis, cumulTT[(int)Delta.PhenoMaize]) <= Dcd));
            }
        }

        #endregion

        #region N

        public double StemTotalN { get { return shoot.StemTotalN; } }

        public double GrainTotalN
        {
            get { return shoot.GrainTotalN; }
        }
             

        public double CropTotalN
        {
            get { return shoot.GrainTotalN + shoot.TotalN; }
        }

        public double OutputTotalN
        {
            get { return shoot.GrainTotalN + shoot.StemTotalN + shoot.PhytomersTotalN; }
        }

        public double AliveN
        {
            get { return shoot.GrainGreenN + shoot.GreenN; }
        }

        public double ShootGreenN
        {
            get { return shoot.GreenN; }
        }

        public double StructN
        {
            get { return shoot.GrainStructN + shoot.StructN; }
        }

        public double LabileN
        {
            get { return shoot.GrainLabileN + shoot.LabileN; }
        }

        public double DeadN
        {
            get { return shoot.GrainDeadN + shoot.DeadN; }
        }

        #endregion

        #region DM

        public double EarDW { get { return grainEarDW_.EarDW; } }

        public double CropTotalDM
        {
            get { return GrainTotalDM + shoot.TotalDM; }
        }



        public double StemTotalDM { get { return shoot.StemTotalDM; } }

        public double OutputTotalDM
        {
            get { return GrainTotalDM + shoot.StemTotalDM + shoot.PhytomersTotalDM; }
        }

        public double OutputTotalDMOld;

        public double AliveDM
        {
            get { return GrainGreenDM + shoot.GreenDM; }
        }

        public double ShootGreenDM
        {
            get { return shoot.GreenDM; }
        }

        public double ShootNCrit
        {
            get { return shoot.NCrit; }
        }

        public double StructDM
        {
            get { return GrainStructDM + shoot.StructDM; }
        }

        public double LabileDM
        {
            get { return GrainLabileDM + shoot.LabileDM; }
        }

        public double DeadDM
        {
            get { return GrainDeadDM + shoot.DeadDM; }
        }

        public double ShootDeadDM
        {
            get { return shoot.DeadDM; }
        }

        public double ShootLostDM
        {
            get { return shoot.LostDM; }
        }


        ///<Behnam (2016.06.30)>
        ///<Comment>Calculates the change in CO2 concentration based on a pre-defined annual trend</Comment>        
        public void CalcChangeinCO2Conc()
        {
            if (Run.ManagementDef.IsCO2TrendApplied)
            {
                DateTime BaseDate = new DateTime(Run.ManagementDef.CO2TrendBaseYear, 6, 30);
                CO2Change = 1 + (CurrentDate - BaseDate).TotalDays * Run.ManagementDef.CO2TrendSlope / 100 / 365.25;
            }
            else CO2Change = 1;

            DailyCO2 = CO2 * CO2Change;
        }

        #endregion
    }
}