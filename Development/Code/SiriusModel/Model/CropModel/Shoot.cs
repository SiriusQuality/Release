    using System;
using SiriusModel.Model.Base;
using SiriusModel.Model.Observation;
using SiriusModel.Structure;
using SiriusModel.Model.Phenology;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SiriusModel.Model.CropModel
{
    ///<summary>Shoots of the wheat</summary>
    public class Shoot : UniverseLink
    {
        #region Properties

        ///<Behnam (2016.01.13)>
        public bool isUnlimitedWater;
        public bool isUnlimitedNitrogen;
        public bool isUnlimitedTemperature;

        // Critical N concentration in crop.
        public double NCrit;
        ///</Behnam>

        ///<Behnam (2016.01.20)>
        public double TotalPAR { get { return phytomers.TotalPAR; } }
        public double TotalLUE { get { return phytomers.TotalLUE; } }
        ///</Behnam>
        
        ///<Behnam (2016.06.30)>
        public double DailyCO2;
        ///</Behnam>
        //Debug
        public string TagGrainCN { get; set; }
        public string TagGrainCNWarnOut { get; set; }
        public double OutUptakeRatioN { get; set; }
        public double OutUptakeRatioDM { get; set; }
        public double nSupplyperGrain { get; set; }
        public double cSupplyperGrain { get; set; }
        public double OriginalNSupply { get; set; }
        public double  OutCsupply { get; set; }
        public double CSupplyWORemob { get; set; }
        public double NCRatioWRemob { get; set; }
        public double NCRatioWORemob { get; set; }
        //public double NSupplyAfterRemob { get; set; }
        //public double UEN1 { get; set; }
        //public double UEN2 { get; set; }
        //public double UEN3 { get; set; }
        //Debug
        //Debug

        public double Ncp { get { return grain.Ncp; } }

        public double TotalDMperGrain { get { return grain.TotalDMperGrain; } }

        public double TotalNperGrain { get { return grain.TotalNperGrain; } }

        public double ProteinConcentration { get { return grain.ProteinConcentration; } }

        public double GrainNumber { get { return grain.GrainNumber; } }

        public double PercentStarch { get { return grain.PercentStarch; } }

        public double NalbGlo { get { return grain.NalbGlo; } }

        public double Namp { get { return grain.Namp; } }

        public double Ngli { get { return grain.Ngli; } }

        public double Nglu { get { return grain.Nglu; } }

        public double LER { get { return phytomers.LER; } }

        public double PercentGli { get { return grain.PercentGli; } }

        public double PercentGlu { get { return grain.PercentGlu; } }

        public double GliadinsToGluteins { get { return grain.GliadinsToGluteins; } }

        public double getHarvestIndexDM(double CropTotalDM) { return grain.HarvestIndexDM(CropTotalDM); }

        public double getHarvestIndexN(double CropTotalN) {  return grain.HarvestIndexN(CropTotalN); }

        public double GrainTotalDM { get { return grain.TotalDM; } }

        public double GrainTotalN { get { return grain.GrainTotalN; } }

        public double GrainGreenN { get { return grain.GreenN; } }

        public double getNutilisationEfficiency(double CropTotalN) { return grain.NutilisationEfficiency(CropTotalN); }

        public double Starch { get { return grain.Starch; } }

        public double NStorage { get { return grain.Nstorage; } }

        public double GrainStructDM { get { return grain.StructDM; } }

        public double GrainLabileDM { get { return grain.LabileDM; } }

        public double GrainGreenDM { get { return grain.GreenDM; } }

        public double GrainDeadDM { get { return grain.DeadDM; } }

        public double GrainStructN { get { return grain.StructN; } }

        public double GrainLabileN { get { return grain.LabileN; } }

        public double GrainDeadN { get { return grain.DeadN; } }

        public double GrainCumulTT { get { return grain.CumulTT; } }

        public double DEF { get { return phytomers.DEF; } }

        public double DSF { get { return phytomers.DSF; } }

        public double LaminaeAI { get { return phytomers.LaminaeAI; } }

        public double GAI { get { return phytomers.GAI; } }

        public double OutputSheathDM { get { return phytomers.OutputSheathDM; } }

        public double OutputSheathN { get { return phytomers.OutputSheathN; } }

        public double PhytomersTotalDM { get { return phytomers.OutputTotalDM; } }

        public double PhytomersTotalN { get { return phytomers.OutputTotalN; } }

        public double OutputLaminaeDM { get { return phytomers.OutputLaminaeDM; } }

        public double OutputLaminaeN { get { return phytomers.OutputLaminaeN; } }

        public double LaminaeSLN { get { return phytomers.LaminaeSLN; } }

        public double LaminaeSLW { get { return phytomers.LaminaeSLW; } }

        public double SumInternodesLength { get { return phytomers.SumInternodesLength; } }

        public double Tau { get { return phytomers.Tau; } }

        public double PotentialWaterOnLeaves { get { return phytomers.PotentialWaterOnLeaves; } }

        public int CreatedLeavesNumber { get { return phytomers.AllLeaves.Count; } }

        public double DeltaShootDM { get { return phytomers.DeltaShootDM; } }

        public double PhytomersAliveDM { get { return phytomers.AliveDM; } }

        public double CountWSC { get { return stem.CountWSC(); } }

        public double getStemNLabile { get { return stem.N.Labile; } }

        public void setStemNLabile (double newValue) {stem.N.Labile =newValue;}

        public double getStemNStruct { get { return stem.N.Struct; } }

        public void setStemNStruct(double newValue) { stem.N.Struct = newValue; }

        public double getStemDMStruct { get { return stem.DM.Struct;  } }

        public void setStemDMStruct(double newValue) { stem.DM.Struct = newValue; }

        public double getStemStructAnthDM { get { return stem.structAnthDM; } }

        public double getStemStructAnthN { get { return stem.structAnthN; } }

        public double StemDMGreen { get { return stem.DM.Green; } }

        public double getInterNodeLengthForLeafLayer(int leafLayerIndex)
        {
            if (phytomers.GetLeafLayer(leafLayerIndex) != null)
            {
                return phytomers.GetLeafLayer(leafLayerIndex).GetInterNode().Length;
            }
            else { return 0.0; }
        }

        public double getLeafLaminaAreaIndexForLeafLayer(int leafLayerIndex)
        {
            if (phytomers.GetLeafLayer(leafLayerIndex) != null) 
            { 
                return phytomers.GetLeafLayer(leafLayerIndex).GetLeafLamina().AreaIndex;
            }
            else { return 0.0; }
        }

        public double getLeafLaminaTotalDMForLeafLayer(int leafLayerIndex) 
        { 
            if (phytomers.GetLeafLayer(leafLayerIndex) != null)
            {
                return phytomers.GetLeafLayer(leafLayerIndex).GetLeafLamina().DM.Total;
            }
            else { return 0.0; }
        }

        public double getLeafLaminaTotalNForLeafLayer(int leafLayerIndex) 
        { 
            if (phytomers.GetLeafLayer(leafLayerIndex) != null) 
            { 
                return phytomers.GetLeafLayer(leafLayerIndex).GetLeafLamina().N.Total;
            }
            else { return 0.0; }
        }

        public double getLeafLaminaSpecificNForLeafLayer(int leafLayerIndex) 
        { 
            if (phytomers.GetLeafLayer(leafLayerIndex) != null) 
            {
                return phytomers.GetLeafLayer(leafLayerIndex).GetLeafLamina().SpecificN;
            }
            else { return 0.0; }
        }

        public double getLeafLaminaSpecificWeightForLeafLayer(int leafLayerIndex) 
        { 
            if (phytomers.GetLeafLayer(leafLayerIndex) != null) 
            { 
                return phytomers.GetLeafLayer(leafLayerIndex).GetLeafLamina().SpecificWeight;
            }
            else { return 0.0; }
        }

        public double getLeafLaminaTTsinceSenForLeafLayer(int leafLayerIndex)
        {
            if (phytomers.GetLeafLayer(leafLayerIndex) != null)
            {
                return phytomers.GetLeafLayer(leafLayerIndex).GetLeafLamina().TTSinceSen;
            }
            else { return 0.0; }
        }

        public double getLeafLaminaDeadDMForLeafLayer(int leafLayerIndex)
        {
            if (phytomers.GetLeafLayer(leafLayerIndex) != null)
            {
                return phytomers.GetLeafLayer(leafLayerIndex).GetLeafLamina().DM.Dead;
            }
            else { return 0.0; }
        }

        public double getLeafLaminaLostDMForLeafLayer(int leafLayerIndex)
        {
            if (phytomers.GetLeafLayer(leafLayerIndex) != null)
            {
                return phytomers.GetLeafLayer(leafLayerIndex).GetLeafLamina().DM.Lost;
            }
            else { return 0.0; }
        }

        public double getExposedSheathAreaIndexForLeafLayer(int leafLayerIndex)
        {
            if (phytomers.GetLeafLayer(leafLayerIndex) != null) 
            {
                return phytomers.GetLeafLayer(leafLayerIndex).GetExposedSheath().AreaIndex;
            }
            else { return 0.0; }
        }

        public double getExposedSheathTotalDMForLeafLayer(int leafLayerIndex)
        {
           if (phytomers.GetLeafLayer(leafLayerIndex) != null)
           {  
               return phytomers.GetLeafLayer(leafLayerIndex).GetExposedSheath().DM.Total;
           }
           else { return 0.0; }
        }

        public double getExposedSheathTotalNForLeafLayer(int leafLayerIndex)
        {
            if (phytomers.GetLeafLayer(leafLayerIndex) != null) 
            { 
                return phytomers.GetLeafLayer(leafLayerIndex).GetExposedSheath().N.Total;
            }
            else { return 0.0; }
        }

        public double getExposedSheathSpecificNForLeafLayer(int leafLayerIndex)
        {
            if (phytomers.GetLeafLayer(leafLayerIndex) != null)
            {
                return phytomers.GetLeafLayer(leafLayerIndex).GetExposedSheath().SpecificN;
            }
            else { return 0.0; }
        }

        public double getExposedSheathSpecificWeightForLeafLayer(int leafLayerIndex)
        {
            if (phytomers.GetLeafLayer(leafLayerIndex) != null)
            { 
                return phytomers.GetLeafLayer(leafLayerIndex).GetExposedSheath().SpecificWeight;
            }
            else { return 0.0; }
        }
        

        public double getExposedSheathTTsinceSenForLeafLayer(int leafLayerIndex)
        {
            if (phytomers.GetLeafLayer(leafLayerIndex) != null)
            {
                return phytomers.GetLeafLayer(leafLayerIndex).GetExposedSheath().TTSinceSen;
            }
            else { return 0.0; }
        }

        public double getExposedSheathDeadDMForLeafLayer(int leafLayerIndex)
        {
            if (phytomers.GetLeafLayer(leafLayerIndex) != null)
            {
                return phytomers.GetLeafLayer(leafLayerIndex).GetExposedSheath().DM.Dead;
            }
            else { return 0.0; }
        }

        public double getExposedSheathLostDMForLeafLayer(int leafLayerIndex)
        {
            if (phytomers.GetLeafLayer(leafLayerIndex) != null)
            {
                return phytomers.GetLeafLayer(leafLayerIndex).GetExposedSheath().DM.Lost;
            }
            else { return 0.0; }
        }

        public double getEarPeduncleInterNodeLength()
        {
            if (phytomers.EarPeduncle != null) { return phytomers.EarPeduncle.getInternodeLength(); }
            else {return 0.0;}
        }

        public double getPotentialLaminaTillerAreaIndexForLeafLayer(int leafLayerIndex, int tillerIndex, int roundedFinalLeafNumber, double leafNumber, List<double> tilleringProfile) 
        {
            if (phytomers.GetLeafLayer(leafLayerIndex) != null)
            {
                return phytomers.GetLeafLayer(leafLayerIndex).PotentialLaminaTillerAreaIndex( tillerIndex, roundedFinalLeafNumber,leafNumber,tilleringProfile);
            }
            else { return 0.0; }
        }

        public double getPotentialLaminaAreaIndexForLeafLayer(int leafLayerIndex, int roundedFinalLeafNumber, double leafNumber,List<double> leafTillerNumberArray,  List<double> tilleringProfile)
        {
            if (phytomers.GetLeafLayer(leafLayerIndex) != null)
            {
                return phytomers.GetLeafLayer(leafLayerIndex).PotentialLaminaAreaIndex(roundedFinalLeafNumber, leafNumber,leafTillerNumberArray,tilleringProfile);
            }
            else { return 0.0; }
        }

        public double getPotentialSheathTillerAreaIndexForLeafLayer(int leafLayerIndex, int tillerIndex, int roundedFinalLeafNumber, List<double> tilleringProfile)
        {
            if (phytomers.GetLeafLayer(leafLayerIndex) != null)
            {
                return phytomers.GetLeafLayer(leafLayerIndex).PotentialSheathTillerAreaIndex(tillerIndex, roundedFinalLeafNumber,tilleringProfile);
            }
            else { return 0.0; }
        }

        public double getPotentialSheathAreaIndexForLeafLayer(int leafLayerIndex, int roundedFinalLeafNumber, double leafNumber,List<double> leafTillerNumberArray, List<double> tilleringProfile)
        {
            if (phytomers.GetLeafLayer(leafLayerIndex) != null)
            {
                return phytomers.GetLeafLayer(leafLayerIndex).PotentialSheathAreaIndex(roundedFinalLeafNumber, leafNumber, leafTillerNumberArray,tilleringProfile);
            }
            else { return 0.0; }
        }
        public double getWaterLimitedPotDeltaAI(int leafLayerIndex)
        {
            return phytomers.getWaterLimitedPotDeltaAI(leafLayerIndex);
        }

        public double getLeafLength(int i) { return phytomers.getLeafLength(i); }

        public double getLeafWidth(int i) { return phytomers.getLeafWidth(i); }

        public double getPotentialIncDeltaArea()
        {
            return phytomers.getPotentialIncDeltaArea();
        }

        #endregion

        private Stem stem;
        private Phytomers.Phytomers phytomers;
        private SenescencePool senescencePool;
        private Grain grain;
   
        #region Constructors

        ///<summary>Create a new shoots</summary>
        ///<param name="universe">Universe of the model</param>
        public Shoot(Universe universe) : base(universe)
        {
            grain = new Grain(universe);
            stem = new Stem(universe);
            phytomers = new Phytomers.Phytomers(universe);
            senescencePool = new SenescencePool(universe);           
        }

        ///<summary>Create a new shoots by coopy</summary>
        ///<param name="universe">Universe of the model</param>
        ///<param name="toCopy">Shoots to copy</param>
        ///<param name="copyAll">true for copying every thing (parallel runs), false to just copy/save the results of the simulation </param>
        public Shoot(Universe universe, Shoot toCopy, bool copyAll)
            : base(universe)
        {
            grain = (toCopy.grain != null) ? new Grain(universe, toCopy.grain) : null;
            stem = (toCopy.stem != null) ? new Stem(universe, toCopy.stem, copyAll) : null;
            phytomers = (toCopy.phytomers != null) ? new Phytomers.Phytomers(universe, toCopy.phytomers, copyAll) : null;
            senescencePool = (toCopy.senescencePool != null) ? new SenescencePool(universe, toCopy.senescencePool) : null;
            NCrit = toCopy.NCrit;
            //Debug
            TagGrainCN = toCopy.TagGrainCN;
            TagGrainCNWarnOut = toCopy.TagGrainCN;
            nSupplyperGrain = toCopy.nSupplyperGrain;
            cSupplyperGrain = toCopy.cSupplyperGrain;
            OutUptakeRatioN = toCopy.OutUptakeRatioN;
            OutUptakeRatioDM = toCopy.OutUptakeRatioDM;
            OriginalNSupply = toCopy.OriginalNSupply;
            OutCsupply = toCopy.OutCsupply;
            CSupplyWORemob = toCopy.CSupplyWORemob;
            NCRatioWRemob = toCopy.NCRatioWRemob;
            NCRatioWORemob = toCopy.NCRatioWORemob;
            //NSupplyAfterRemob= toCopy.NSupplyAfterRemob;
            //UEN1 = toCopy.UEN1;
            //UEN2 = toCopy.UEN2;
            //UEN3 = toCopy.UEN3;
            //Debug
            if (copyAll)
            {
                isUnlimitedWater = toCopy.isUnlimitedWater;
                isUnlimitedNitrogen = toCopy.isUnlimitedNitrogen;
                isUnlimitedTemperature = toCopy.isUnlimitedTemperature;
            }        
        }

        #endregion

        #region Init/InitDayStep/InitAtAnthesis

        ///<summary>Init the shoots at the beginning of the simulation</summary>
        public void Init()
        {
            grain.Init();
            stem.Init();
            phytomers.Init();
            senescencePool.Init();
            TagGrainCN = "No C or N limitation for grain";
            TagGrainCNWarnOut = "";
        }

        // #Andrea 01/12/2015 - renamed deltaTTShoot and deltaTTAir to deltaTTRemobilization
        ///<summary>Init the shoots at the beginning of the day step</summary>
        public void InitDayStep(double deltaTTRemobilization)
        {
            stem.InitDayStep(deltaTTRemobilization);// #Andrea 02/12/2015
            phytomers.InitDayStep(deltaTTRemobilization);// #Andrea 01/12/2015
        }

        ///<summary>Init the shoots at anthesis</summary>
        public void InitAtAnthesis(double EarDW)
        {
            stem.InitAtAnthesis();

            grain.InitAtAnthesis(EarDW);

        }

        #endregion

        #region Grow
        ///<Behnam (2015.10.27)>
        ///<Comment>Adding VirtualNReq to the arguments</Comment>

        // #Andrea 26/11/2015 - added double deltaTTSenescence, double canopyTMin, double canopyTmax
        //private double beforeUpdateLeafNumber = 0;
        ///<summary>Grow the shoots</summary>
        ///<param name="soilForLabileN">soil available Ni for labiles pools</param>
         public void Grow(double radTopAtm, double[] cumulTT, double deltaTTShoot, double deltaTTSenescence, double canopyTMin, double canopyTmax,double rad, double PAR,
             int roundedFinalNumber,
             double finalLeafNumber, double DBF, double leafNumber, bool isBeforeEndGrainCellDivision,
             double availableNfromSoilBefore, out double availableNfromSoilAfter, double beforeUpdateLeafNumber,
             double phyllochron, double FPAW, ref double VirtualNReq, double VPDairCanopy, double LER, bool passedZC_75_EndCellDivision, List<double> tilleringProfile, List<double> leafTillerNumberArray,
             double deltaTTPhenoMaize, double cumulTTPhenoMaizeAtEmergence, double cumulTTPhenoMaize,double dailyCO2, double[] deltaTTCanopyHourly,double[] VPDeq,
             out double excessDM)
        {
            double incDeltaArea;
            double excessDeltaLeafDM = 0;
            excessDM=0;
            double excessDeltaStemDM = 0;
            double remobN = 0;
            double remobDM = 0;

            double availableNfromSoil= availableNfromSoilBefore;
			
            phytomers.GrowInterNodes(cumulTT, deltaTTShoot);

    	
            // reload the stem
            double remobDM4 = 0;
            double NLabileInc = 0;
            if (Math.Max(0.0, stem.DM.Green * StrucStemN - stem.N.Green) > 0)
            {
                NLabileInc += senescencePool.UptakeSenescenceN(Math.Max(0.0, stem.DM.Green * StrucStemN - stem.N.Green));
            }

            if ((Math.Max(0.0, stem.DM.Green * StrucStemN - stem.N.Green) - NLabileInc) > 0)
            {
                NLabileInc += phytomers.UptakeDailyLabileN(Math.Max(0.0, stem.DM.Green * StrucStemN - stem.N.Green) - NLabileInc, ref remobDM4);
            }

            stem.FillStem(NLabileInc,remobDM4);
	
            // calculate delta shoot DM
            phytomers.CalculateDeltaShootDM(radTopAtm, deltaTTShoot, canopyTMin, canopyTmax, rad, PAR, DBF,dailyCO2);

            phytomers.growLeafLayer(beforeUpdateLeafNumber, cumulTT[(int)Delta.Shoot], deltaTTShoot, leafNumber, roundedFinalNumber, finalLeafNumber, phyllochron, deltaTTPhenoMaize, cumulTTPhenoMaizeAtEmergence);

            phytomers.calculateLAI(roundedFinalNumber, finalLeafNumber, leafNumber, FPAW, cumulTT[(int)Delta.Shoot], deltaTTShoot, deltaTTSenescence, VPDairCanopy, LER, tilleringProfile, leafTillerNumberArray, cumulTTPhenoMaize, deltaTTPhenoMaize, deltaTTCanopyHourly,VPDeq);
            
            // calculate Ni and DM remobilised by senescence
            phytomers.Senescence(ref remobN, ref remobDM);
    	
            // add remob Ni of senescence to the SenescencePool
            senescencePool.IncSenescenceN(remobN);
    	
            // update leaf Layer areas
            phytomers.UpdateAreas(out incDeltaArea, cumulTT[(int)Delta.Shoot], CountNStem(false) + CountNPhytomers(false) + senescencePool.CountSenescenceN() + availableNfromSoil);

            if (isBeforeEndGrainCellDivision)
            {
                // update leaf Layer Ni and DM content
                phytomers.Grow(incDeltaArea, remobDM, out excessDeltaLeafDM, cumulTT[(int)Delta.Shoot]);
    	
                // leaves uptake Ni from labile pools
                if (incDeltaArea > 0)
                {
                    
                    double need5 = incDeltaArea * SLNcri;
                    double ueN5 = 0;
                    if (need5 > 0)
                    {
                        ueN5 += senescencePool.UptakeSenescenceN(need5);
                    }

                    if ((need5 - ueN5) > 0)
                    {
                        if (stem != null)
                        {
                            ueN5 += stem.UptakeDailyLabileN(need5 - ueN5);
                        }
                    }

                    ueN5 += UptakeNfromSoil(need5 - ueN5, ref availableNfromSoil,ref VirtualNReq);

                    
                    if ((need5 - ueN5) > 0)
                    {
                        if (phytomers != null)
                        {
                            double remobDM5 = 0;
                            phytomers.UptakeDailyLabileN(need5 - ueN5, ref remobDM5);
                            phytomers.SatisfyDemandForGrowthN(incDeltaArea, cumulTT[(int)Delta.Shoot], remobDM5);
                        }
                    }
                }

                // (1) stem grows, it takes excessDeltaStemDM until its N cocentration equals its structural value.
                stem.Grow(excessDeltaLeafDM, passedZC_75_EndCellDivision, CountNStem(false) + CountNPhytomers(false) + senescencePool.CountSenescenceN() + availableNfromSoil, out excessDeltaStemDM);
                excessDM = excessDeltaStemDM;

                // stem uptakes Ni from labile pools
                double remobDM3 = 0;
                double need3 = (excessDeltaLeafDM - excessDeltaStemDM) * StrucStemN;
                double ueN3 = 0;

                if (need3 > 0)
                {
                    ueN3 += senescencePool.UptakeSenescenceN(need3);
                }

                ueN3 += UptakeNfromSoil(need3 - ueN3, ref availableNfromSoil, ref VirtualNReq);


                if ((need3 - ueN3) > 0)
                {
                    if (stem != null)
                    {
                        ueN3 += stem.UptakeDailyLabileN(need3 - ueN3);
                    }
                }

                if ((need3 - ueN3) > 0)
                {
                    if (phytomers != null)
                    {
                        phytomers.UptakeDailyLabileN(need3 - ueN3, ref remobDM3);
                    }
                }

                stem.SatisfyDemandForGrowthN(excessDeltaLeafDM, excessDeltaStemDM, remobDM3);               
            }
  /*          else
            {
                //after EndGrainCellDivision send all the Dm remobilized by senescence to grain
                excessDM = remobDM;
            }
*/
            // calculate leaf Layer demand for labile Ni
            double laminaeDemandLabileN =  phytomers.CalculateLaminaeDemandLabileN();
            double sheathDemandLabileN = phytomers.CalculateSheathDemandLabileN();

            Check.IsNumber(laminaeDemandLabileN);
            Check.IsNumber(sheathDemandLabileN);
    	
            // Now Senescence pool don't take excess N gradient.
            if (laminaeDemandLabileN < 0)
            {
                // add remob Ni of gradient optimization to the SenescencePool
                senescencePool.IncSenescenceN(-laminaeDemandLabileN);
            }
    	
            if (sheathDemandLabileN < 0)
            {
                // add remob Ni of gradient optimization to the SenescencePool
                senescencePool.IncSenescenceN(-sheathDemandLabileN);
            }
    	
            // calculate stem demand for labile Ni
            double stemDemandLabileN =stem.CalculateDemandLabileN();
    	
            var demandLabileN = stemDemandLabileN;
    	
            if (laminaeDemandLabileN > 0)
                demandLabileN += laminaeDemandLabileN;
            if (sheathDemandLabileN > 0)
                demandLabileN += sheathDemandLabileN;

            // soil available Ni for labiles pools
            var soilForLabileN = availableNfromSoil;

            // soil available Ni for laminae labile pool
            var soilForLaminaeLabileN = laminaeDemandLabileN >= 0 
                ? Math.Min(laminaeDemandLabileN, (demandLabileN > 0) 
                ? soilForLabileN * (laminaeDemandLabileN / demandLabileN) : soilForLabileN) : 0;
    	
            // soil available Ni for sheath labile pool
            var soilForSheathLabileN = sheathDemandLabileN >= 0 ? 
                Math.Min(sheathDemandLabileN, (demandLabileN > 0) ? 
                soilForLabileN * (sheathDemandLabileN / demandLabileN) : soilForLabileN) : 0;
    	
            // soil available Ni for leaf labile pool
            var soilForStemLabileN = Math.Min(stemDemandLabileN, (demandLabileN > 0) ? soilForLabileN * (stemDemandLabileN / demandLabileN) : soilForLabileN);

            Check.IsNumber(soilForLabileN);
            Check.IsNumber(soilForLaminaeLabileN);
            Check.IsNumber(soilForSheathLabileN);
            Check.IsNumber(soilForStemLabileN);

            // update vertical Ni distribution if labile demand exists (otherwise potential SLN is reached)
            phytomers.UpdateLaminaeVerticalNdistribution(soilForLaminaeLabileN, laminaeDemandLabileN);

            if (soilForLaminaeLabileN > 0)
            {
                double need6 = soilForLaminaeLabileN;
                double ueN6 = 0;
                if (need6 > 0)
                {
                    ueN6 += senescencePool.UptakeSenescenceN(need6);
                }
                UptakeNfromSoil(need6 - ueN6, ref availableNfromSoil, ref VirtualNReq);
            }

            // update vertical Ni distribution if labile demand exists (otherwise potential SSN is reached)
            phytomers.UpdateSheathVerticalNdistribution(soilForSheathLabileN, sheathDemandLabileN);

            if (soilForSheathLabileN > 0)
            {
                double need7 = soilForSheathLabileN;
                double ueN7 = 0;
                if (need7 > 0)
                {
                    ueN7 += senescencePool.UptakeSenescenceN(need7);
                }
                UptakeNfromSoil(need7 - ueN7, ref availableNfromSoil, ref VirtualNReq);
            }
    	
            // stem uptakes Ni for labile demand
            double need2 = soilForStemLabileN;
            double ueN2 = 0;
            if (need2 > 0)
            {
                ueN2 += senescencePool.UptakeSenescenceN(need2);
            }

            UptakeNfromSoil(need2 - ueN2, ref availableNfromSoil, ref VirtualNReq);
            stem.SatisfyDemandForLabileN(soilForStemLabileN);

            //outputs
            availableNfromSoilAfter = availableNfromSoil;

            phytomers.Degradation(deltaTTShoot);
        }

        public void useSenescencePool()
        {
            double ueN8 = 0;
            if (Math.Max(0.0, stem.DM.Green * MaxStemN - stem.N.Green) > 0)
            {
                ueN8 += senescencePool.UptakeSenescenceN(Math.Max(0.0, stem.DM.Green * MaxStemN - stem.N.Green));
            }
            stem.UseSenescencePool(ueN8);

            double ueN9 = 0;
            if (senescencePool.CountSenescenceN() > 0)
            {
                ueN9 += senescencePool.UptakeSenescenceN(senescencePool.CountSenescenceN());
            }

            stem.UseSenescencePool( phytomers.UseSenescencePool(ueN9));
        }

        public void GrowGrain(bool isBeforeEndGrainCellDivision, double deltaTTShoot,double deltaTTRemobilization, double DGF, double excessDM)
        {

            // The pool of remobilisable dry matter is defined at the end of endosperm cell division ( see pheno() )
            double remobDM = 0;

            grain.CumulTT += deltaTTShoot * DGF; // Annotation by #Andrea 01/12/2015. I did not modifie this calculation. We decided to leave phenological development
            // with deltaTTshoot. Only remobilization follow deltaTTRemobilization

            if (grain.CumulTT <= Dcd)
            {
                #region Before TTcd

                // pm 6/04/03 consider a constant ratio of C/Ni structure.
                // pm 14/03/06, modified to first calculate Nstructure, then Cstructure while maintaining a constant C/Ni ratio if Cstructure is insufficient to meet the daily demand for structural Ni

                var cStrucDemand = (grain.Cstructure == 0) ? 0.02664 / AlphaNC : Kcd * grain.Cstructure * deltaTTRemobilization * DGF;

                var nStrucDemand = cStrucDemand * AlphaNC;

                var cSupply = CountWSC;
                var nSupply = CountNStem(true) + CountNPhytomers(true) + senescencePool.CountSenescenceN();

                double variable1;//find a better name
                double variable2;

                if (nSupply > cSupply * AlphaNC)
                {
                    if (cStrucDemand < grain.ToGrainDM(cSupply))
                    {
                        variable1 = grain.FromGrainN(nStrucDemand);
                        variable2 = grain.FromGrainDM(cStrucDemand);
                    }
                    else
                    {
                        variable1 = cSupply * AlphaNC;
                        variable2 = cSupply;
                    }
                }
                else
                {
                    if (nStrucDemand < grain.ToGrainN(nSupply))
                    {
                        variable1 = grain.FromGrainN(nStrucDemand);
                        variable2 = grain.FromGrainDM(cStrucDemand);
                    }
                    else
                    {
                        variable1 = nSupply;
                        variable2 = nSupply / AlphaNC;
                    }
                }

                double ueN = 0;
                if (variable1 > 0)
                {
                    ueN += senescencePool.UptakeSenescenceN(variable1);
                }

                if ((variable1 - ueN) > 0)
                {
                    ueN += stem.UptakeLabileN(variable1 - ueN);
                }

                if ((variable1 - ueN) > 0)
                {
                    ueN += phytomers.UptakeLabileN(variable1 - ueN, ref remobDM);
                }

                //grain.grow
                grain.Nstructure += grain.ToGrainN(ueN);

                grain.Cstructure += grain.ToGrainDM(remobDM);

                if (remobDM < variable2)
                {
                    stem.UptakeWSC(variable2 - remobDM);
                    grain.Cstructure += (grain.ToGrainDM(variable2) - grain.ToGrainDM(remobDM));
                }

                //Compute Cstructure and Nstructure at the end of the cell division stage
                grain.CstructureEndCD = grain.Cstructure;
                grain.NstructureEndCD = grain.Nstructure;

                #endregion
            }
            else //TTANTHESIS > TT_CD
            {
                #region After TTcd
                //  *****   Accumulation of structural and storage DM and Ni during cell expansion *****

                // During the cell expansion phase structural DM and Ni accumulate at a rate determined during the cell division phase
                // pm 22/10/05, Assume that the end of grain filling is determined only be resource availability,but not by a constant thermal time. Thus the line below has to be commented out.
                // pm 17/04/03, DMsupply and Nsupply rescaled from the end of cell division to the end of grain filling.
                //              Define as a supply -not a demand- because it doesn't depend on grain characteristics.
                //              Daily supply of DM set as a constant fraction of PoolDM, plus all new biomass accumulated today.

                double cStrucDemand = grain.CstructureEndCD / Dcd;
                double nStrucDemand = grain.NstructureEndCD / Dcd;

                double uptakeRatioDM = 1;
                double uptakeRatioN = 1;
                //Debug
                if (grain.CumulTT < Dgf)
                {
                    uptakeRatioDM = Math.Max(0.0, Math.Min(1.0, (deltaTTRemobilization * DGF) / (Dgf - grain.CumulTT))); // #Andrea 01/12/2015 non linear temperature effect
                    uptakeRatioN = Math.Max(0.0, Math.Min(1.0, (deltaTTRemobilization * DGF) / (Dgf - grain.CumulTT))); // #Andrea 01/12/2015 non linear temperature effect
                }
                //Debug
                //Debug

                OutUptakeRatioN = uptakeRatioN;
                OutUptakeRatioDM = uptakeRatioDM;

                
                //Method I
                /* We want to limit the deltaNStorage/deltaStarch ratio by maxGrainNC or minGrainNC. If the ratio is > maxGrainNC (< minGrainNC) we need to reduce the flux of Nitrogen (Carbon).
                 * As it is very difficult to put back the nitrogen we took from the stem and phytomers, we need to limit the initial available supply.
                 *We have implemented a recursive method. We first evaluate (without uptaking or filling) the N or C flux available for storage after structure filling. If flux available for storage
                 *don't fulfilled the conditions drive by min/maxGrainNC we subtract some C or N on initial flux and so on until the min/maxGrain conditions are fulffiled.
                 *At the end, we actually fill the grain (structure and storage) and uptake C and N from the Crop. During this final step, we use the value of inital flux calculated in the convergence loop.
                */

                //Method II
                /*
                 * In Method I the effect of min/maxGrainNC is not straightforward. The value of the parameter is not directly apply to the amount of stored C and N.
                 * By applying the constraint both on structure and storage filling we hop that the value of min/maxGrainCN will be directly apply to starch and protein amount
                 * (Pierre's Idea). The method used is more or less the same than in Method I, but this time the condition driven by min/maxGrainCN is applied on inital flux.
                 */

                //Method II is better, in particular for maxGrainNC cut. Method I is commented and not used.

                /*

                

                //initialazing convergence
                //calculate Csupply without remob 
                double cSupplyWithoutRemob = CountWSC * uptakeRatioDM;

                //calculate Nsupply
                double nSupply = (CountNStem(true) + CountNPhytomers(true) + senescencePool.CountSenescenceN()) * uptakeRatioN;
                
                //N and C (for incrementation) will be modified in the convergence loop until equilibrium
                double nSupplyForinc = nSupply;
                double cSupplyWORemobForinc = cSupplyWithoutRemob;

                //Debug
                OriginalNSupply = nSupply;
                NCRatioWORemob = nSupply / cSupplyWithoutRemob;
                //Debug
                
                
               //Evaluating nSupply and Csupply after N Uptake (whithout actually uptaking)
               double ghostRemobDM = remobDM; //Fake variable which mimic actual remobDM
               nSupply = EvaluateNUptake(nSupply, ref ghostRemobDM);

               double newBiomass;
               if (isBeforeEndGrainCellDivision) newBiomass = 0;
               else newBiomass = phytomers.DeltaShootDM + excessDM;

               //we calculate the full cSupply (with remob)
               double cSupply = cSupplyWithoutRemob + newBiomass + ghostRemobDM;
               ghostRemobDM = remobDM;
                
              //we evaluate the grain structure filling
              if (grain.CumulTT <= Der) EvaluateStructNCUptake(nStrucDemand, cStrucDemand, AlphaNC, ref nSupply, ref cSupply);

              double nSupplyUseForStoreFill = nSupply;
              double cSupplyUseForStoreFill = cSupply;

              nSupply = nSupplyForinc;
              cSupply = cSupplyWORemobForinc;

              //Back-up the original values before entering the convergence loop
              //If the number of iteration exceed thresholdIt this values are applied
              double nSupplyBackUp = nSupplyForinc;
              double cSupplyBackUp = cSupplyWORemobForinc;
              string TagGrainCNBackUp = "A convergence problem occured at least 1 time, cut on C/N supply wasn't applied";

              //Iteration counter for convegence
              int countIterConvC = 0;
              int countIterConvN = 0;

               int thresholdItC = 10000;
               int thresholdItN = 10000;

              //int thresholdItC = 1000;
              //int thresholdItN = 2000;

              //Convergence loop (repeating initialization steps until conditions drive by min/maxGrainNC are fulfilled)
              if (!IsCutOnGrainFillNotUse)
              {
                  if (cSupply > EPS && nSupply > EPS)
                  {
                      while (Math.Round(nSupplyUseForStoreFill / cSupplyUseForStoreFill, 3) > maxGrainNC || Math.Round(nSupplyUseForStoreFill / cSupplyUseForStoreFill, 3) < minGrainNC)
                      {

                        if (countIterConvC > thresholdItC)
                          {
                              nSupplyForinc = nSupplyBackUp;
                              cSupplyWORemobForinc = cSupplyBackUp;
                              TagGrainCNWarnOut = TagGrainCNBackUp;
                              TagGrainCN = TagGrainCNBackUp;

                              break;

                          }

                          if (countIterConvN > thresholdItN)
                          {
                              nSupplyForinc = nSupplyBackUp;
                              cSupplyWORemobForinc = cSupplyBackUp;
                              TagGrainCNWarnOut = TagGrainCNBackUp;
                              TagGrainCN = TagGrainCNBackUp;


                              break;

                          }

                          nSupply = EvaluateNUptake(nSupply, ref ghostRemobDM);
                          cSupply = cSupply + newBiomass + ghostRemobDM;
                          ghostRemobDM = remobDM;


                          //structural growth
                          if (grain.CumulTT <= Der) EvaluateStructNCUptake(nStrucDemand, cStrucDemand, AlphaNC, ref nSupply, ref cSupply);

                          nSupplyUseForStoreFill = nSupply;
                          cSupplyUseForStoreFill = cSupply;

                          //if N supply for storage is to high we reduce the initial N Supply
                          if (Math.Round(nSupplyUseForStoreFill / cSupplyUseForStoreFill, 3) > maxGrainNC && cSupply > EPS)
                          {

                              nSupplyForinc = nSupplyForinc - 0.001;
                              TagGrainCN = "N has been limited for grain";
                              countIterConvN++;

                          }
                          //if C supply for storage is to high we reduce the initial C Supply
                          else if (Math.Round(nSupplyUseForStoreFill / cSupplyUseForStoreFill, 3) < minGrainNC && nSupply > EPS)
                          {

                              cSupplyWORemobForinc = cSupplyWORemobForinc - 0.001;
                              TagGrainCN = "C has been limited for grain";
                              countIterConvC++;

                          }
                          else break;



                          nSupply = nSupplyForinc;
                          cSupply = cSupplyWORemobForinc;



                      }
                  }
              }
 
                  //Doing again the initialisation step but actually filling the grain and uptaking N and C
                  //The initial supply is that calculated in the convergence loop

                  //uptaking N and evaluating remobilistation
                  nSupply = NUptake(nSupplyForinc, ref remobDM);
                  cSupply = cSupplyWORemobForinc + newBiomass + remobDM;

                  //Uptaking C
                  stem.UptakeWSC(cSupplyWORemobForinc);
                
                  //Filling structure
                  if (grain.CumulTT <= Der) FillStructNC(nStrucDemand, cStrucDemand, AlphaNC, ref nSupply, ref cSupply);

                  //Filling Storage
                  grain.Nstorage += grain.ToGrainN(nSupply);
                  grain.Starch += grain.ToGrainDM(cSupply);


                  //Debug
                  nSupplyperGrain = grain.ToGrainN(nSupply);
                  cSupplyperGrain = grain.ToGrainN(cSupply);
                  //Debug
      */          


                //initialazing convergence
                //calculate Csupply without remob 
                double cSupplyWithoutRemob = CountWSC * uptakeRatioDM;

                //calculate Nsupply
                double nSupply = (CountNStem(true) + CountNPhytomers(true) + senescencePool.CountSenescenceN()) * uptakeRatioN;

                //N and C (for incrementation) will be modified in the convergence loop until equilibrium
                double nSupplyForinc = nSupply;
                double cSupplyWORemobForinc = cSupplyWithoutRemob;

                //Debug
                OriginalNSupply = nSupply;
                NCRatioWORemob = nSupply / cSupplyWithoutRemob;
                //Debug


                //Evaluating nSupply and Csupply after N Uptake (whithout actually uptaking)
                double ghostRemobDM = remobDM; //Fake variable which mimic actual remobDM
                nSupply = EvaluateNUptake(nSupply, ref ghostRemobDM);

                double newBiomass;
                if (isBeforeEndGrainCellDivision) newBiomass = 0;
                else newBiomass = phytomers.DeltaShootDM + excessDM;

                //we calculate the full cSupply (with remob)
                double cSupply = cSupplyWithoutRemob + newBiomass + ghostRemobDM;
                ghostRemobDM = remobDM;

                //use only for method II
                double cSupplyWithRemob = cSupply;

                //we evaluate the grain structure filling
                if (grain.CumulTT <= Der) EvaluateStructNCUptake(nStrucDemand, cStrucDemand, AlphaNC, ref nSupply, ref cSupply);

                //only used for method I
                double nSupplyUseForStoreFill = nSupply;
                double cSupplyUseForStoreFill = cSupply;

                nSupply = nSupplyForinc;
                cSupply = cSupplyWORemobForinc;


                //Back-up the original values before entering the convergence loop
                //If the number of iteration exceed thresholdIt this values are applied
                double nSupplyBackUp = nSupplyForinc;
                double cSupplyBackUp = cSupplyWORemobForinc;
                string TagGrainCNBackUp = "A convergence problem occured at least 1 time, cut on C/N supply was not/fully applied";

                //Iteration counter for convegence
                int countIterConvC = 0;
                int countIterConvN = 0;

                int thresholdItC = 500000;
                int thresholdItN = 500000;

                //int thresholdItC = 1000;
                //int thresholdItN = 2000;

                //Variables to back-up values before supply becomes <0
                //If supply <0 these values are used
                double nSupplyBackUpInf0 = nSupplyForinc;
                double cSupplyBackUpInf0 = cSupplyWORemobForinc;


                //Convergence loop (repeating initialization steps until conditions drive by min/maxGrainNC are fulfilled)
                if (!IsCutOnGrainFillNotUse)
                {

                    //Method II

                    if (cSupplyWithRemob > EPS && nSupply > EPS)
                    {
                        while (Math.Round(nSupply / cSupplyWithRemob, 3) > maxGrainNC || Math.Round(nSupply / cSupplyWithRemob, 3) < minGrainNC)
                        {



                            if (countIterConvC > thresholdItC)
                            {
                                nSupplyForinc = nSupplyBackUp;
                                cSupplyWORemobForinc = cSupplyBackUp;
                                TagGrainCNWarnOut = TagGrainCNBackUp;
                                TagGrainCN = TagGrainCNBackUp;

                                break;

                            }

                            if (countIterConvN > thresholdItN)
                            {
                                nSupplyForinc = nSupplyBackUp;
                                cSupplyWORemobForinc = cSupplyBackUp;
                                TagGrainCNWarnOut = TagGrainCNBackUp;
                                TagGrainCN = TagGrainCNBackUp;


                                break;

                            }


                            
                            nSupplyBackUpInf0 = nSupplyForinc;
                            cSupplyBackUpInf0 = cSupplyWORemobForinc;

                            //we evaluate the full cSupply (with remob)
                            nSupply = EvaluateNUptake(nSupply, ref ghostRemobDM);
                            cSupply = cSupplyWORemobForinc + newBiomass + ghostRemobDM;
                            ghostRemobDM = remobDM;

                            cSupplyWithRemob = cSupply;

                            //if initial N supply is to high we reduce it
                            if (Math.Round(nSupply / cSupply, 3) > maxGrainNC && cSupply > EPS)
                            {

                                nSupplyForinc = nSupplyForinc - 0.001;
                                TagGrainCN = "N has been limited for grain";
                                countIterConvN++;

                                if (nSupplyForinc <0.0)
                                {
                                    nSupplyForinc = nSupplyBackUpInf0;
                                    cSupplyWORemobForinc = cSupplyBackUpInf0;
                                    TagGrainCNWarnOut = TagGrainCNBackUp;
                                    TagGrainCN = TagGrainCNBackUp;


                                    break;

                                }

                            }
                            //if initial C supply is to high we reduce it
                            else if (Math.Round(nSupply / cSupply, 3) < minGrainNC && nSupply > EPS)
                            {

                                cSupplyWORemobForinc = cSupplyWORemobForinc - 0.001;
                                TagGrainCN = "C has been limited for grain";
                                countIterConvC++;

                                if (cSupplyWORemobForinc < 0.0)
                                {
                                    nSupplyForinc = nSupplyBackUpInf0;
                                    cSupplyWORemobForinc = cSupplyBackUpInf0;
                                    TagGrainCNWarnOut = TagGrainCNBackUp;
                                    TagGrainCN = TagGrainCNBackUp;


                                    break;

                                }

                            }
                            else break;


                            nSupply = nSupplyForinc;

                        }

                    }
                }

                //Doing again the initialisation step but actually filling the grain and uptaking N and C
                //The initial supply is that calculated in the convergence loop

                //uptaking N and evaluating remobilistation
                nSupply = NUptake(nSupplyForinc, ref remobDM);
                cSupply = cSupplyWORemobForinc + newBiomass + remobDM;

                //Uptaking C
                stem.UptakeWSC(cSupplyWORemobForinc);

                //Filling structure
                if (grain.CumulTT <= Der) FillStructNC(nStrucDemand, cStrucDemand, AlphaNC, ref nSupply, ref cSupply);

                //Filling Storage
                grain.Nstorage += grain.ToGrainN(nSupply);
                grain.Starch += grain.ToGrainDM(cSupply);


                //Debug
                nSupplyperGrain = grain.ToGrainN(nSupply);
                cSupplyperGrain = grain.ToGrainN(cSupply);
                //Debug
                
                #endregion
            }

            grain.NalbGlo = AlphaAlbGlo * Math.Pow(grain.Nstructure, BetaAlbGlo);
            grain.Namp = grain.Nstructure - grain.NalbGlo;

            if (grain.Nstorage > 0)
            {
                grain.Nglu = AlphaGlu * Math.Pow(grain.Nstorage, BetaGlu);

                if (grain.Nstorage - grain.Nglu > 0)
                {
                    grain.Ngli = grain.Nstorage - grain.Nglu;
                }
            }
        }
        public bool getIsLatestLeafInternodeLengthPotPositive() { return phytomers.getIsLatestLeafInternodeLengthPotPositive(); }

        #endregion

        #region N

        public double StemTotalN { get { return stem.N.Total; } }

        ///<summary>Get the total N of the shoots</summary>
        ///<returns>Total N of the shoots</returns>
        public double TotalN
        {
            get { return GreenN + DeadN; }
        }

        ///<summary>Get the green N of the shoots</summary>
        ///<returns>Green N of the shoots</returns>
        public double GreenN
        {
            get { return StructN + LabileN; }
        }

        ///<summary>Get the struct N of the shoots</summary>
        ///<returns>Struct N of the shoots</returns>
        public double StructN
        {
            get { return stem.N.Struct + phytomers.StructN; }
        }

        ///<summary>Get the labile N of the shoots</summary>
        ///<returns>Labile N of the shoots</returns>
        public double LabileN
        {
            get { return stem.N.Labile + phytomers.LabileN; }
        }

        ///<summary>Get the dead N of the shoots</summary>
        ///<returns>Dead N of the shoots</returns>
        public double DeadN
        {
            get { return stem.N.Dead + phytomers.DeadN; }
        }


        ///<summary>Evaluate N and C used for structural grain filling</summary>
        ///<returns></returns>
        private void EvaluateStructNCUptake(double nStrucDemand, double cStrucDemand, double AlphaNC, ref double nSupply, ref double cSupply)
        {
               if (nSupply > cSupply * AlphaNC)
               {
                   if (cStrucDemand < grain.ToGrainDM(cSupply))
                   {  
                      cSupply -= grain.FromGrainDM(cStrucDemand);
                      nSupply -= grain.FromGrainN(nStrucDemand);
                   }
                   else
                   {
                      nSupply -= cSupply * AlphaNC;
                      cSupply = 0;
                   }
               }
               else
                 {
                     if (nStrucDemand < grain.ToGrainN(nSupply))
                      {
                          nSupply -= grain.FromGrainN(nStrucDemand);
                          cSupply -= grain.FromGrainN(cStrucDemand);
                      }
                      else
                        {
                           cSupply -= (nSupply / AlphaNC);
                           nSupply = 0;
                        }
                  }
        }

        ///<summary>Fill the grain with stuctural N and C</summary>
        private void FillStructNC(double nStrucDemand, double cStrucDemand, double AlphaNC, ref double nSupply, ref double cSupply)
        {
               if (nSupply > cSupply * AlphaNC)
               {
                   if (cStrucDemand < grain.ToGrainDM(cSupply))
                   {
                      grain.Cstructure += cStrucDemand;
                      cSupply -= grain.FromGrainDM(cStrucDemand);
                      grain.Nstructure += nStrucDemand;
                      nSupply -= grain.FromGrainN(nStrucDemand);
                   }
                   else
                   {
                      grain.Cstructure += grain.ToGrainDM(cSupply);
                      grain.Nstructure += grain.ToGrainN(cSupply * AlphaNC);
                      nSupply -= cSupply * AlphaNC;
                      cSupply = 0;
                   }
               }
               else
                 {
                     if (nStrucDemand < grain.ToGrainN(nSupply))
                      {
                          grain.Nstructure += nStrucDemand;
                          nSupply -= grain.FromGrainN(nStrucDemand);
                          grain.Cstructure += cStrucDemand;
                          cSupply -= grain.FromGrainN(cStrucDemand);
                      }
                      else
                        {
                           grain.Nstructure += grain.ToGrainN(nSupply);
                           grain.Cstructure += grain.ToGrainDM((nSupply / AlphaNC));
                           cSupply -= (nSupply / AlphaNC);
                           nSupply = 0;
                        }
                  }

        }

        ///<summary>Evaluate N Uptake before structure and storage grain filling (after cell division). C remob is also evaluated</summary>
        ///<returns>The N supply which should be uptaken</returns>
        private double EvaluateNUptake(double nSupply,ref double remobDM)
        {

            double ueN = 0;
            if (nSupply > 0)
            {
                ueN += senescencePool.EvaluateUptakeSenescenceN(nSupply);
            }

            if ((nSupply - ueN) > 0)
            {
                ueN += stem.EvaluateUptakeLabileN(nSupply - ueN);
            }

            if ((nSupply - ueN) > 0)
            {
                ueN += phytomers.EvaluateUptakeLabileN(nSupply - ueN, ref remobDM);
            }
            return ueN;

        }

        ///<summary>Uptake the amount of N use for structure and storage grain filling (after cell division)</summary>
        ///<returns>The N supply which was uptaken, it has to be exactly the amount used for grain filling</returns>
        private double NUptake(double nSupply, ref double remobDM)
        {

            double ueN = 0;
            if (nSupply > 0)
            {
                ueN += senescencePool.UptakeSenescenceN(nSupply);
            }

            if ((nSupply - ueN) > 0)
            {
                ueN += stem.UptakeLabileN(nSupply - ueN);
            }

            if ((nSupply - ueN) > 0)
            {
                ueN += phytomers.UptakeLabileN(nSupply - ueN, ref remobDM);
            }
            return ueN;

        }


        ///<summary>Calculate the Uptake of Ni from soil</summary>
        ///<param name="nToUptake">Quantity of Ni to remove from soil</param>
        ///<returns>Quantity of Ni removed from soil</returns>
        public double UptakeNfromSoil(double nToUptake, ref double availableNfromSoil, ref double VirtualNReq)
        {
            if (nToUptake > 0)
            {
                ///<Behnam (2016.01.13)>
                ///<Comment>To preserve N balance under limited and unlimited conditions
                ///nAppLevel can be used to define a specific level of compensation for N deficiency</Comment>
                ///After recent changes, the model does not uptake N from soil under unlimited condition
                ///to avoid any possible loss related to N deficiency.

                double nAppLevel = NCompensationLevel / 100;
                double foundN;

                if (isUnlimitedNitrogen)
                {
                    /// Behnam (2016.06.29): A minor change to how to handle N limitation.
                    if (nToUptake <= availableNfromSoil)
                    {
                        foundN = nToUptake;
                        availableNfromSoil = availableNfromSoil - nToUptake;
                    }
                    else
                    {
                        foundN = availableNfromSoil + (nAppLevel * (nToUptake - availableNfromSoil));
                        VirtualNReq += nAppLevel * (nToUptake - availableNfromSoil);
                        availableNfromSoil = 0;
                    }
                }
                else
                {
                    if (nToUptake <= availableNfromSoil)
                    {
                        foundN = nToUptake;
                        availableNfromSoil = availableNfromSoil - nToUptake;
                    }
                    else
                    {
                        foundN = availableNfromSoil;
                        availableNfromSoil = 0;
                    }
                }
                return foundN;
            }
            ///<Behnam>
            else
            {
                return 0;
            }
        }

        public double CountNStem(bool destinationIsGrain)
        {
            double count = 0;
            if (stem != null)
            {
                count = !destinationIsGrain ? stem.CountDailyLabileN() : stem.CountLabileN();
            }

            Check.IsNumber(count);
            return count;
        }
        public double CountNPhytomers(bool destinationIsGrain)
        {
            double count = 0;
            if (phytomers != null)
            {
                count = !destinationIsGrain ? phytomers.CountDailyLabileN() : phytomers.CountLabileN();
            }
            Check.IsNumber(count);
            return count;
        }
        public double getPhytomersLabileN() { return phytomers.CountLabileN(); }


        #endregion

        #region DM

        public double StemTotalDM { get { return stem.DM.Total; } }

        ///<summary>Get the total DM of the shoots</summary>
        ///<returns>Total DM of the shoots</returns>
        public double TotalDM
        {
            get { return GreenDM + DeadDM; }
        }

        ///<summary>Get green DM of the shoots</summary>
        ///<returns></returns>
        public double GreenDM
        {
            get { return StructDM + LabileDM; }
        }

        ///<summary>Get the struct DM of the shoots</summary>
        ///<returns>Struct DM of the shoots</returns>
        public double StructDM
        {
            get { return stem.DM.Struct + phytomers.StructDM; }
        }

        ///<summary>Get the labile DM of the shoots</summary>
        ///<returns>Labile DM of the shoots</returns>
        public double LabileDM
        {
            get { return stem.DM.Labile + phytomers.LabileDM; }
        }

        ///<summary>Get the dead DM of the shoots</summary>
        ///<returns>Dead DM of the shoots</returns>
        public double DeadDM
        {
            get { return stem.DM.Dead + phytomers.DeadDM; }
        }

        ///<summary>Get the lost DM of the shoots</summary>
        ///<returns>lost DM of the shoots by degradation of phytomers</returns>
        public double LostDM
        {
            get { return phytomers.LostDM; }
        }


        #endregion

        public override void Dispose()
        {
            base.Dispose();
            if (phytomers != null)
            {
                phytomers.Dispose();
                phytomers = null;
            }
            if (stem != null)
            {
                stem.Dispose();
                stem = null;
            }
        }

        #region NNI

        ///<summary>Calculate the current NNI of the crop</summary>
        ///<returns>NNI</returns>
        public double CalculateNNI()
        {
            const double minCropDM = 155; // Justes et al (1994);

            //var cropDM = Math.Max(Shoot_.GreenDM, MIN_CROP_DM);
            var cropDM = GreenDM;
            var cropN  = GreenN;

            ///<Behnam (2016.01.13)>
            ///<Comment>Justes et al (1994); Modified in response to a Pierre's email on 2016.01.13</Comment>
            if (cropDM <= minCropDM)
            {
                NCrit = 4.4; // Mg/ha OR %DM
            }
            else
            {
                NCrit = AlphaNNI * Math.Pow(cropDM / 100.0, -BetaNNI); // Mg/ha OR %DM
            }
            ///</Behnam>

            var ncrop = 100.0 * cropN / cropDM; // Mg/ha --> (gN/m2) / (gDM/m2 / (1000000 Mg/g)) / 10000 ha/m2 OR %DM
            var nniVal = (NCrit > 0 && ncrop > 0 && cropDM > 0) ? ncrop / NCrit : 1;
            return nniVal;
        }

        #endregion
    }

}