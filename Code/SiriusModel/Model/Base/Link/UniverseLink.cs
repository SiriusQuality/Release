using System;
using System.ComponentModel;
using SiriusModel.InOut;
using System.Collections.Generic;

namespace SiriusModel.Model
{

    ///<summary>Enumerate the available delta sensor fields</summary>
    public enum Delta
    {
        ///<summary>Delta on shoot temperature (based on the soil temperature until leaf # reach MaxLeafSoil and then on canopy temperature)</summary>
        Shoot,

        ///<summary>Delta on air temperature (based on the weather files)</summary>
        Air,

        ///<summary>Delta on soil temperature</summary>
        Soil,

        ///<summary>Delta on leaf senescence temperature</summary> // #Andrea 26/11/2015
        LeafSenescence,

        /// <summary>Delta on N and DM remobilization</summary> // #Andrea 01/12/2015
        Remobilization,

        /// Behnam (2016.01.20): It stores Days Above 20
        /// <summary>Delta on physiological temperature</summary>
        Physiology,

        /// <summary>Delta used for the phenology of the maize </summary>
        PhenoMaize
    }
    ///The class UniverseElement is the base class for model's elements.
    public class UniverseLink : IDisposable
    {
        #region Calc

        public static readonly double EPS = 0.00000001;
        public static void RoundZero(ref double toRound)
        {
            if (toRound > -EPS && toRound < EPS) toRound = 0;
        }
        #endregion

        #region Constructors

        ///<summary>
        ///Create a new UniverseLink which belongs to the given Universe.
        ///</summary>
        ///<param name="aUniverse">The universe reference, must be not null.</param>
        ///<exception cref="ArgumentNullException">if <paramref name="aUniverse"/> is null.</exception>
        public UniverseLink(Universe aUniverse)
        {
            Check.IsNotNull(aUniverse);
            Universe_ = aUniverse;
            CalculateFixphyll();// not optimal to put it here
        }

        #endregion

        #region Properties

        ///<summary>
        ///Get the Universe reference. This is never null.
        ///</summary>
        public Universe Universe_ { get;  set; }

        ///<summary>
        ///Get the Element reference of Universe_. This is never null.
        ///</summary>
        public Run Run
        {
            get { return Universe_.Run; }
        }


        ///<summary>
        ///Get Universe_.CurrentDate.
        ///</summary>
        public DateTime CurrentDate
        {
            get { return Universe_.CurrentDate; }
        }

        public DateTime PreviousDate
        {
            get { return CurrentDate.AddDays(-1); }
        }

        #endregion

        #region Parameters

        protected bool UseObservedGrainNumber { get { return Run.RunOptionDef.UseObservedGrainNumber; } }
        protected bool IgnoreGrainMaturation { get { return Run.RunOptionDef.IgnoreGrainMaturation; } }
        protected bool UseAirTemperatureForSenescence { get { return Run.RunOptionDef.UseAirTemperatureForSenescence; } }
        protected bool IsCutOnGrainFillNotUse { get { return Run.RunOptionDef.IsCutOnGrainFillNotUse; } }
        protected bool UnlimitedWater { get { return Run.RunOptionDef.UnlimitedWater; } }
        protected bool UnlimitedNitrogen { get { return Run.RunOptionDef.UnlimitedNitrogen; } }
        protected bool UnlimitedTemperature { get { return Run.RunOptionDef.UnlimitedTemperature; } }
        protected double MaxTempThreshold { get { return Run.RunOptionDef.MaxTempThreshold; } }
        protected double WCompensationLevel { get { return Run.RunOptionDef.WCompensationLevel; } }
        protected double NCompensationLevel { get { return Run.RunOptionDef.NCompensationLevel; } }
        protected bool DoInteractions { get { return Run.RunOptionDef.DoInteractions; } }
        protected bool InteractionsW { get { return Run.RunOptionDef.InteractionsW; } }
        protected bool InteractionsN { get { return Run.RunOptionDef.InteractionsN; } }
        protected bool InteractionsT { get { return Run.RunOptionDef.InteractionsT; } }
        protected bool InteractionsWN { get { return Run.RunOptionDef.InteractionsWN; } }
        protected bool InteractionsWT { get { return Run.RunOptionDef.InteractionsWT; } }
        protected bool InteractionsNT { get { return Run.RunOptionDef.InteractionsNT; } }
        protected bool InteractionsWNT { get { return Run.RunOptionDef.InteractionsWNT; } }
        protected string OutputPattern { get { return Run.RunOptionDef.OutputPattern; } }

        // Summary Outputs
        protected bool Management { get { return Run.RunOptionDef.Management; } }
        protected bool NonVarietalParameters { get { return Run.RunOptionDef.NonVarietalParameters; } }
        protected bool VarietalParameters { get { return Run.RunOptionDef.VarietalParameters; } }
        protected bool RunOptions { get { return Run.RunOptionDef.RunOptions; } }
        protected bool Site { get { return Run.RunOptionDef.Site; } }
        protected bool Soil { get { return Run.RunOptionDef.Soil; } }
        protected bool SowingWindow { get { return Run.RunOptionDef.SowingWindow; } }
        protected bool SowingYear { get { return Run.RunOptionDef.SowingYear; } }
        protected bool SowingDateOut { get { return Run.RunOptionDef.SowingDateOut; } }
        protected bool SowingDateOutDOY { get { return Run.RunOptionDef.SowingDateOutDOY; } }
        protected bool EmergenceDate { get { return Run.RunOptionDef.EmergenceDate; } }
        protected bool EmergenceDateDOY { get { return Run.RunOptionDef.EmergenceDateDOY; } }
        protected bool EmergenceDay { get { return Run.RunOptionDef.EmergenceDay; } }
        protected bool FirstNodeDetectable { get { return Run.RunOptionDef.FirstNodeDetectable; } }
        protected bool BeginningOfStemExtension { get { return Run.RunOptionDef.BeginningOfStemExtension; } }
        protected bool TerminalSpikeletDate { get { return Run.RunOptionDef.TerminalSpikeletDate; } }
        protected bool FlagLeafLiguleJustVisibleDate { get { return Run.RunOptionDef.FlagLeafLiguleJustVisibleDate; } }
        protected bool HeadingDate { get { return Run.RunOptionDef.HeadingDate; } }
        protected bool AnthesisDate { get { return Run.RunOptionDef.AnthesisDate; } }
        protected bool AnthesisDateDOY { get { return Run.RunOptionDef.AnthesisDateDOY; } }
        protected bool AnthesisDay { get { return Run.RunOptionDef.AnthesisDay; } }
        protected bool EndOfCellDivision { get { return Run.RunOptionDef.EndOfCellDivision; } }
        protected bool EndOfGrainFilling { get { return Run.RunOptionDef.EndOfGrainFilling; } }
        protected bool MaturityDate { get { return Run.RunOptionDef.MaturityDate; } }
        protected bool MaturityDateDOY { get { return Run.RunOptionDef.MaturityDateDOY; } }
        protected bool MaturityDay{ get { return Run.RunOptionDef.MaturityDay; } }
        protected bool MeanTempAnthesis { get { return Run.RunOptionDef.MeanTempAnthesis; } }
        protected bool MeanTempAnth2Maturity { get { return Run.RunOptionDef.MeanTempAnth2Maturity; } }
        protected bool MeanTempMaturity { get { return Run.RunOptionDef.MeanTempMaturity; } }
        protected bool MeanMaxCanopyTempMaturity { get { return Run.RunOptionDef.MeanTempMaturity; } }
        protected bool MeanMaxAirTempMaturity { get { return Run.RunOptionDef.MeanTempMaturity; } }
        protected bool PhysTempAnthesis { get { return Run.RunOptionDef.PhysTempAnthesis; } }
        protected bool PhysTempAnth2Maturity { get { return Run.RunOptionDef.PhysTempAnth2Maturity; } }
        protected bool PhysTempMaturity { get { return Run.RunOptionDef.PhysTempMaturity; } }
        protected bool FinalLeafNumberOption { get { return Run.RunOptionDef.FinalLeafNumberOption; } }
        protected bool LAIatAnthesis { get { return Run.RunOptionDef.LAIatAnthesis; } }
        protected bool GAIatAnthesis { get { return Run.RunOptionDef.GAIatAnthesis; } }
        protected bool CropDryMatAnthesis { get { return Run.RunOptionDef.CropDryMatAnthesis; } }
        protected bool CropDryMatMatururity { get { return Run.RunOptionDef.CropDryMatMatururity; } }
        protected bool GrainDryMatMatururity { get { return Run.RunOptionDef.GrainDryMatMatururity; } }
        protected bool CropNatAnthesis { get { return Run.RunOptionDef.CropNatAnthesis; } }
        protected bool CropNatMaturity { get { return Run.RunOptionDef.CropNatMaturity; } }
        protected bool GrainNatMaturity { get { return Run.RunOptionDef.GrainNatMaturity; } }
        protected bool PostAnthesisCropNUptake { get { return Run.RunOptionDef.PostAnthesisCropNUptake; } }
        protected bool SingleGrainDMatMaturity { get { return Run.RunOptionDef.SingleGrainDMatMaturity; } }
        protected bool SingleGrainNatMaturity { get { return Run.RunOptionDef.SingleGrainNatMaturity; } }
        protected bool GrainProteinAtMaturity { get { return Run.RunOptionDef.GrainProteinAtMaturity; } }
        protected bool GrainNumberOption { get { return Run.RunOptionDef.GrainNumberOption; } }
        protected bool StarchAtMaturity { get { return Run.RunOptionDef.StarchAtMaturity; } }
        protected bool AlbuminsAtMaturity { get { return Run.RunOptionDef.AlbuminsAtMaturity; } }
        protected bool AmphiphilsAtMaturity { get { return Run.RunOptionDef.AmphiphilsAtMaturity; } }
        protected bool GliadinsAtMaturity { get { return Run.RunOptionDef.GliadinsAtMaturity; } }
        protected bool GluteninsAtMaturity { get { return Run.RunOptionDef.GluteninsAtMaturity; } }
        protected bool GliadinsPAtMaturity { get { return Run.RunOptionDef.GliadinsPAtMaturity; } }
        protected bool GluteinsPAtMaturity { get { return Run.RunOptionDef.GluteinsPAtMaturity; } }
        protected bool GliadinsToGluteinsOption { get { return Run.RunOptionDef.GliadinsToGluteinsOption; } }
        protected bool HarvestIndex { get { return Run.RunOptionDef.HarvestIndex; } }
        protected bool NHarvestIndex { get { return Run.RunOptionDef.NHarvestIndex; } }
        protected bool RainIrrigationAnthesis { get { return Run.RunOptionDef.RainIrrigationAnthesis; } }
        protected bool RainIrrigationAnth2Maturity { get { return Run.RunOptionDef.RainIrrigationAnth2Maturity; } }
        protected bool RainIrrigationMaturity { get { return Run.RunOptionDef.RainIrrigationMaturity; } }
        protected bool CumPotETAnthesis { get { return Run.RunOptionDef.CumPotETAnthesis; } }
        protected bool CumPotETAnth2Maturity { get { return Run.RunOptionDef.CumPotETAnth2Maturity; } }
        protected bool CumPotETMaturity { get { return Run.RunOptionDef.CumPotETMaturity; } }
        protected bool CumActETAnthesis { get { return Run.RunOptionDef.CumActETAnthesis; } }
        protected bool CumActETAnth2Maturity { get { return Run.RunOptionDef.CumActETAnth2Maturity; } }
        protected bool CumActETMaturity { get { return Run.RunOptionDef.CumActETMaturity; } }
        protected bool CumActTrAnthesis { get { return Run.RunOptionDef.CumActTrAnthesis; } }
        protected bool CumActTrAnth2Maturity { get { return Run.RunOptionDef.CumActTrAnth2Maturity; } }
        protected bool CumActTrMaturity { get { return Run.RunOptionDef.CumActTrMaturity; } }
        protected bool CumEvaporation { get { return Run.RunOptionDef.CumEvaporation; } }
        protected bool CumNApplied { get { return Run.RunOptionDef.CumNApplied; } }
        protected bool TotalAvaiSoilN { get { return Run.RunOptionDef.TotalAvaiSoilN; } }
        protected bool NLeaching { get { return Run.RunOptionDef.NLeaching; } }
        protected bool Drainage { get { return Run.RunOptionDef.Drainage; } }
        protected bool CumNMineralization { get { return Run.RunOptionDef.CumNMineralization; } }
        protected bool CumNDenitrification { get { return Run.RunOptionDef.CumNDenitrification; } }
        protected bool FPAWatAnthesis { get { return Run.RunOptionDef.FPAWatAnthesis; } }
        protected bool FPAWatMaturity { get { return Run.RunOptionDef.FPAWatMaturity; } }
        protected bool AvailableWaterinSoilatMaturity { get { return Run.RunOptionDef.AvailableWaterinSoilatMaturity; } }
        protected bool AvailableMineralNinSoilAtMaturity { get { return Run.RunOptionDef.AvailableMineralNinSoilAtMaturity; } }
        protected bool TotalSoilMineralNatMaturity { get { return Run.RunOptionDef.TotalSoilMineralNatMaturity; } }
        protected bool AvailableWaterinRootZoneMaturity { get { return Run.RunOptionDef.AvailableWaterinRootZoneMaturity; } }
        protected bool AvailableMineralNinRootZoneMaturity { get { return Run.RunOptionDef.AvailableMineralNinRootZoneMaturity; } }
        protected bool TotalMineralNinRootZoneMaturity { get { return Run.RunOptionDef.TotalMineralNinRootZoneMaturity; } }
        protected bool NNIatAnthesis { get { return Run.RunOptionDef.NNIatAnthesis; } }
        protected bool NUseEfficiency { get { return Run.RunOptionDef.NUseEfficiency; } }
        protected bool NUtilisationEfficiency { get { return Run.RunOptionDef.NUtilisationEfficiency; } }
        protected bool NUptakeEfficiency { get { return Run.RunOptionDef.NUptakeEfficiency; } }
        protected bool WaterUseEfficiency { get { return Run.RunOptionDef.WaterUseEfficiency; } }
        protected bool AccumPAREmerge2EndGrainFill { get { return Run.RunOptionDef.AccumPAREmerge2EndGrainFill; } }

        // Daily Outputs: Top Items
        protected bool VersionDateItems { get { return Run.RunOptionDef.VersionDateItems; } }
        protected bool FilesItems { get { return Run.RunOptionDef.FilesItems; } }
        protected bool DateItems { get { return Run.RunOptionDef.DateItems; } }
        protected bool CropItems { get { return Run.RunOptionDef.CropItems; } }
        protected bool ManagementItems { get { return Run.RunOptionDef.ManagementItems; } }
        protected bool ParameterItems { get { return Run.RunOptionDef.ParameterItems; } }
        protected bool RunOptionItems { get { return Run.RunOptionDef.RunOptionItems; } }
        protected bool SiteItems { get { return Run.RunOptionDef.SiteItems; } }
        protected bool SoilItems { get { return Run.RunOptionDef.SoilItems; } }
        protected bool VarietyItems { get { return Run.RunOptionDef.VarietyItems; } }
        // Daily Outputs 
        protected bool DailyGrowthDay { get { return Run.RunOptionDef.DailyGrowthDay; } }
        protected bool DailyDate { get { return Run.RunOptionDef.DailyDate; } }
        protected bool DailyDOY { get { return Run.RunOptionDef.DailyDOY; } }
        protected bool DailyDayLength { get { return Run.RunOptionDef.DailyDayLength; } }
        protected bool DailyCO2Conc { get { return Run.RunOptionDef.DailyCO2Conc; } }
        protected bool DailyThermaltimeaftersowingAir { get { return Run.RunOptionDef.DailyThermaltimeaftersowingAir; } }
        protected bool DailyThermaltimeaftersowingShoot { get { return Run.RunOptionDef.DailyThermaltimeaftersowingShoot; } }
        protected bool DailyPhysThermaltimeaftersowing { get { return Run.RunOptionDef.DailyPhysThermaltimeaftersowing; } }
        protected bool DailyAirtemperature { get { return Run.RunOptionDef.DailyAirtemperature; } }
        protected bool DailySoiltemperature { get { return Run.RunOptionDef.DailySoiltemperature; } }
        protected bool DailyCanopytemperature { get { return Run.RunOptionDef.DailyCanopytemperature; } }
        protected bool DailyRootingdepth { get { return Run.RunOptionDef.DailyRootingdepth; } }
        protected bool DailyCumulativerainirrigation { get { return Run.RunOptionDef.DailyCumulativerainirrigation; } }
        protected bool DailyVPDAirOut { get { return Run.RunOptionDef.DailyVPDAirOut; } }
        protected bool DailyVPDAirCanopyOut { get { return Run.RunOptionDef.DailyVPDAirCanopyOut; } }
        protected bool DailyCumulativePET { get { return Run.RunOptionDef.DailyCumulativePET; } }
        protected bool DailyCumulativeET { get { return Run.RunOptionDef.DailyCumulativeET; } }
        protected bool DailyCumulativePT { get { return Run.RunOptionDef.DailyCumulativePT; } }
        protected bool DailyCumulativeT { get { return Run.RunOptionDef.DailyCumulativeT; } }
        protected bool DailyCumulativewaterdrainage { get { return Run.RunOptionDef.DailyCumulativewaterdrainage; } }
        protected bool DailyAvailablewaterinsoilprofil { get { return Run.RunOptionDef.DailyAvailablewaterinsoilprofil; } }
        protected bool DailyWaterdeficitinsoilprofil { get { return Run.RunOptionDef.DailyWaterdeficitinsoilprofil; } }
        protected bool DailyAvailablewaterinrootzone { get { return Run.RunOptionDef.DailyAvailablewaterinrootzone; } }
        protected bool DailyWaterdeficitinrootzone { get { return Run.RunOptionDef.DailyWaterdeficitinrootzone; } }
        protected bool DailyVirtualWReq { get { return Run.RunOptionDef.DailyVirtualWReq; } }
        protected bool DailyTRSF { get { return Run.RunOptionDef.DailyTRSF; } }
        protected bool DailyETSF { get { return Run.RunOptionDef.DailyETSF; } }
        protected bool DailySMSF { get { return Run.RunOptionDef.DailySMSF; } }
        protected bool DailyFPAW { get { return Run.RunOptionDef.DailyFPAW; } }
        protected bool DailyDrymassdroughtfactor { get { return Run.RunOptionDef.DailyDrymassdroughtfactor; } }
        protected bool DailyEargrowthdroughtfactor { get { return Run.RunOptionDef.DailyEargrowthdroughtfactor; } }
        protected bool DailyLeafexpansiondroughtfactor { get { return Run.RunOptionDef.DailyLeafexpansiondroughtfactor; } }
        protected bool DailySenescencedroughtfactor { get { return Run.RunOptionDef.DailySenescencedroughtfactor; } }
        protected bool DailyTranspirationdroughtfactor { get { return Run.RunOptionDef.DailyTranspirationdroughtfactor; } }
        protected bool DailyCumulativeNfertilisation { get { return Run.RunOptionDef.DailyCumulativeNfertilisation; } }
        protected bool DailyCumulativeNleaching { get { return Run.RunOptionDef.DailyCumulativeNleaching; } }
        protected bool DailyCumulativeNmineralisation { get { return Run.RunOptionDef.DailyCumulativeNmineralisation; } }
        protected bool DailyCumulativedenitrification { get { return Run.RunOptionDef.DailyCumulativedenitrification; } }
        protected bool DailyAvailablemineralNinsoilprofil { get { return Run.RunOptionDef.DailyAvailablemineralNinsoilprofil; } }
        protected bool DailyTotalmineralNinsoilprofil { get { return Run.RunOptionDef.DailyTotalmineralNinsoilprofil; } }
        protected bool DailyAvailablemineralNinrootzone { get { return Run.RunOptionDef.DailyAvailablemineralNinrootzone; } }
        protected bool DailyTotalmineralNinrootzone { get { return Run.RunOptionDef.DailyTotalmineralNinrootzone; } }
        protected bool DailyNitrogennutritionindex { get { return Run.RunOptionDef.DailyNitrogennutritionindex; } }
        protected bool DailyVernaProgress { get { return Run.RunOptionDef.DailyVernaProgress; } }
        protected bool DailyGrowthStage { get { return Run.RunOptionDef.DailyGrowthStage; } }
        protected bool DailyEmergedleafnumber { get { return Run.RunOptionDef.DailyEmergedleafnumber; } }
        protected bool DailyShootnumber { get { return Run.RunOptionDef.DailyShootnumber; } }
        protected bool DailyGreenareaindex { get { return Run.RunOptionDef.DailyGreenareaindex; } }
        protected bool DailyLeafareaindex { get { return Run.RunOptionDef.DailyLeafareaindex; } }
        protected bool DailyStemlength { get { return Run.RunOptionDef.DailyStemlength; } }
        protected bool DailyCropdrymatter { get { return Run.RunOptionDef.DailyCropdrymatter; } }
        protected bool DailyCropdeltadrymatter { get { return Run.RunOptionDef.DailyCropdeltadrymatter; } }
        protected bool DailyVirtualNReq { get { return Run.RunOptionDef.DailyVirtualNReq; } }
        protected bool DailyCropnitrogen { get { return Run.RunOptionDef.DailyCropnitrogen; } }
        protected bool DailyGraindrymatter { get { return Run.RunOptionDef.DailyGraindrymatter; } }
        protected bool DailyGrainnitrogen { get { return Run.RunOptionDef.DailyGrainnitrogen; } }
        protected bool DailyLeafdrymatter { get { return Run.RunOptionDef.DailyLeafdrymatter; } }
        protected bool DailyLeafnitrogen { get { return Run.RunOptionDef.DailyLeafnitrogen; } }
        protected bool DailyLaminaedrymatter { get { return Run.RunOptionDef.DailyLaminaedrymatter; } }
        protected bool DailyLaminaenitrogen { get { return Run.RunOptionDef.DailyLaminaenitrogen; } }
        protected bool DailyStemdrymatter { get { return Run.RunOptionDef.DailyStemdrymatter; } }
        protected bool DailyStemnitrogen { get { return Run.RunOptionDef.DailyStemnitrogen; } }
        protected bool DailyExposedsheathdrymatter { get { return Run.RunOptionDef.DailyExposedsheathdrymatter; } }
        protected bool DailyExposedsheathnitrogen { get { return Run.RunOptionDef.DailyExposedsheathnitrogen; } }
        protected bool DailySpecificleafnitrogen { get { return Run.RunOptionDef.DailySpecificleafnitrogen; } }
        protected bool DailySpecificleafdrymass { get { return Run.RunOptionDef.DailySpecificleafdrymass; } }
        protected bool DailySinglegraindrymass { get { return Run.RunOptionDef.DailySinglegraindrymass; } }
        protected bool DailyStarchpergrain { get { return Run.RunOptionDef.DailyStarchpergrain; } }
        protected bool DailySinglegrainnitrogen { get { return Run.RunOptionDef.DailySinglegrainnitrogen; } }
        protected bool DailyAlbuminsGlobulinspergrain { get { return Run.RunOptionDef.DailyAlbuminsGlobulinspergrain; } }
        protected bool DailyAmphiphilspergrain { get { return Run.RunOptionDef.DailyAmphiphilspergrain; } }
        protected bool DailyGliadinspergrain { get { return Run.RunOptionDef.DailyGliadinspergrain; } }
        protected bool DailyGluteninspergrain { get { return Run.RunOptionDef.DailyGluteninspergrain; } }
        protected bool DailyPostanthesisnitrogenuptake { get { return Run.RunOptionDef.DailyPostanthesisnitrogenuptake; } }
        protected bool DailyMinimumShootTemperature { get { return Run.RunOptionDef.DailyMinimumShootTemperature; } }
        protected bool DailyMeanShootTemperature { get { return Run.RunOptionDef.DailyMeanShootTemperature; } }
        protected bool DailyMaximumShootTemperature { get { return Run.RunOptionDef.DailyMaximumShootTemperature; } }
        protected bool DailyTranspirationT { get { return Run.RunOptionDef.DailyTranspirationT; } }
        protected bool DailyEvaporationE { get { return Run.RunOptionDef.DailyEvaporationE; } }
        protected bool DailyLaminaesurfacearea { get { return Run.RunOptionDef.DailyLaminaesurfacearea; } }
        protected bool DailyLaminaeTotalN { get { return Run.RunOptionDef.DailyLaminaeTotalN; } }
        // Behnam: End

        protected double Latitude { get { return Run.SiteDef.Latitude; } }

        protected double Longitude { get { return Run.SiteDef.Longitude; } }

        protected double Elevation { get { return Run.SiteDef.Elevation; } }

        protected double MeasurementHeight { get { return Run.SiteDef.MeasurementHeight; } }

        protected int SowingWindowType { get { return Run.SiteDef.SowingWindowType; } }

        protected DateTime MinSowingDate { get { return Run.SiteDef.MinSowingDate; } }

        protected DateTime MaxSowingDate { get { return Run.SiteDef.MaxSowingDate; } }

        protected DateTime FinalMinSowingDate { get { return Run.SiteDef.FinalMinSowingDate; } }

        protected DateTime FinalMaxSowingDate { get { return Run.SiteDef.FinalMaxSowingDate; } }

        protected int MinSowWinLength { get { return Run.SiteDef.MinSowWinLength; } }

        protected int InitSowWindow { get { return Run.SiteDef.InitSowWindow; } }

        protected int CheckDaysTemp { get { return Run.SiteDef.CheckDaysTemp; } }

        protected int CheckDaysPcp { get { return Run.SiteDef.CheckDaysPcp; } }

        protected double TempSum { get { return Run.SiteDef.TempSum; } }

        protected double TempThr { get { return Run.SiteDef.TempThr; } }

        protected double PcpSum { get { return Run.SiteDef.PcpSum; } }

        protected DateTime ESD_S { get { return Run.SiteDef.ESD_S; } }

        protected DateTime LSD_S { get { return Run.SiteDef.LSD_S; } }

        protected DateTime ESD_W { get { return Run.SiteDef.ESD_W; } }

        protected DateTime LSD_W { get { return Run.SiteDef.LSD_W; } }

        protected DateTime ESD_Sir { get { return Run.SiteDef.ESD_Sir; } }

        protected DateTime LSD_Sir { get { return Run.SiteDef.LSD_Sir; } }

        protected bool IsSowDateEstimate { get { return Run.ManagementDef.IsSowDateEstimate; } }

        protected bool DoRelax { get { return Run.ManagementDef.DoRelax; } }

        protected DateTime SowingDate { get { return Run.ManagementDef.SowingDate; } }

        protected DateTime FinalSowingDate { get { return Run.ManagementDef.FinalSowingDate; } }

        protected DateTime FinalSowingDateFixPhyll { get { return Run.SowingDate; } }

        protected double SkipDays { get { return Run.ManagementDef.SkipDays; } }

        protected double CheckDaysTempSow { get { return Run.ManagementDef.CheckDaysTemp; } }

        protected double CheckDaysPcpSow { get { return Run.ManagementDef.CheckDaysPcp; } }

        protected double CheckDepth { get { return Run.ManagementDef.CheckDepth; } }

        protected double CumPcpThr { get { return Run.ManagementDef.CumPcpThr; } }

        protected double SoilMoistThr { get { return Run.ManagementDef.SoilMoistThr; } }

        protected double TAveThr { get { return Run.ManagementDef.TAveThr; } }

        protected double TMinThr { get { return Run.ManagementDef.TMinThr; } }

        protected double SoilWorkabThr { get { return Run.ManagementDef.SoilWorkabThr; } }

        protected double SowingDensity { get { return Run.ManagementDef.SowingDensity; } }

        protected bool IsTotalNitrogen { get { return Run.ManagementDef.IsTotalNitrogen; } }

        protected double TotalNApplication { get { return Run.ManagementDef.TotalNApplication; } }

        protected bool IsNTrendApplied { get { return Run.ManagementDef.IsNTrendApplied; } }
        protected int NTrendBaseYear { get { return Run.ManagementDef.NTrendBaseYear; } }
        protected double NTrendSlope { get { return Run.ManagementDef.NTrendSlope; } }

        protected bool IsCO2TrendApplied { get { return Run.ManagementDef.IsCO2TrendApplied; } }
        protected int CO2TrendBaseYear { get { return Run.ManagementDef.CO2TrendBaseYear; } }
        protected double CO2TrendSlope { get { return Run.ManagementDef.CO2TrendSlope; } }

        protected bool IsNNIUsed { get { return Run.ManagementDef.IsNNIUsed; } }
        protected double NNIThreshold { get { return Run.ManagementDef.NNIThreshold; } }
        protected double NNIMultiplier { get { return Run.ManagementDef.NNIMultiplier; } }

        protected bool IsCheckPcpN { get { return Run.ManagementDef.IsCheckPcpN; } }

        protected int CheckDaysPcpN { get { return Run.ManagementDef.CheckDaysPcpN; } }

        protected int MaxPostponeN { get { return Run.ManagementDef.MaxPostponeN; } }

        protected double CumPcpThrN { get { return Run.ManagementDef.CumPcpThrN; } }

        protected double CO2 { get { return Run.ManagementDef.CO2; } }

        protected bool IsWDinPerc { get { return Run.ManagementDef.IsWDinPerc; } }

        protected double Deficit { get { return Run.ManagementDef.SoilWaterDeficit; } }

        protected double TotalNi { get { return Run.ManagementDef.TotalNi; } }

        protected double TopNi { get { return Run.ManagementDef.TopNi; } }

        protected double MidNi { get { return Run.ManagementDef.MidNi; } }

        protected double ObservedGrainNumber { get { return Run.ManagementDef.ObservedGrainNumber; } }

        protected bool IsKqlUsed { get { return Run.SoilDef.IsKqlUsed; } }

        protected bool IsKqCalc { get { return Run.SoilDef.IsKqCalc; } }
        
        protected bool IsOrgNCalc { get { return Run.SoilDef.IsOrgNCalc; } }

        protected double CtoN { get { return Run.SoilDef.CtoN; } }

        protected double Carbon { get { return Run.SoilDef.Carbon; } }

        protected double Bd { get { return Run.SoilDef.Bd; } }

        protected double Gravel { get { return Run.SoilDef.Gravel; } }

        protected double Kq { get { return Run.SoilDef.Kq; } }

        protected double Ko { get { return Run.SoilDef.Ko; } }

        protected double No { get { return Run.SoilDef.No; } }

        protected double MinNir { get { return Run.SoilDef.MinNir; } }

        protected double Ndp { get { return Run.SoilDef.Ndp; } }

        protected SoilLayer[] SoilLayersDescription { get { return Run.SoilDef.LayersArray; } }

        protected double Dse { get { try{ return Run.Parameters["Dse"]; }catch { throw new Exception("the parameter Dse was neither found in your varietal nor in your non varietal file"); } } }

        protected double Dgf { get { try{ return Run.Parameters["Dgf"]; }catch { throw new Exception("the parameter Dgf was neither found in your varietal nor in your non varietal file"); } } }

        protected double Degfm { get { try{ return Run.Parameters["Degfm"]; } catch { throw new Exception("the parameter Degfm was neither found in your varietal nor in your non varietal file"); } }}

        protected bool IsVernalizable { get { try{ return System.Convert.ToBoolean(Run.Parameters["IsVernalizable"]); } catch { throw new Exception("the parameter IsVernalizable was neither found in your varietal nor in your non varietal file"); } }}

        protected double P { get { try{ return Run.Parameters["P"]; } catch { throw new Exception("the parameter P was neither found in your varietal nor in your non varietal file"); } }}

        protected double PhyllDurationDMlost { get { try { return Run.Parameters["PhyllDurationDMlost"]; } catch { throw new Exception("the parameter PhyllDurationDMlost was neither found in your varietal nor in your non varietal file"); } } }
        
        protected double PNslope { get{ try { return Run.Parameters["PNslope"]; }catch { throw new Exception("the parameter PNslope was neither found in your varietal nor in your non varietal file"); } } }

        protected double PNini { get{ try { return Run.Parameters["PNini"]; } catch { throw new Exception("the parameter PNini was neither found in your varietal nor in your non varietal file"); } }}

        protected double Rp { get{ try { return Run.Parameters["Rp"]; } catch { throw new Exception("the parameter Rp was neither found in your varietal nor in your non varietal file"); } }}

        protected double SDws { get { try{ return Run.Parameters["SDws"]; } catch { throw new Exception("the parameter SDws was neither found in your varietal nor in your non varietal file"); } }}

        protected double SDsa_nh { get{ try { return Run.Parameters["SDsa_nh"]; } catch { throw new Exception("the parameter SDsa_sh was neither found in your varietal nor in your non varietal file"); } }}

        protected double SDsa_sh { get { try{ return Run.Parameters["SDsa_sh"]; }catch { throw new Exception("the parameter SDsa_sh was neither found in your varietal nor in your non varietal file"); } } }

        protected double SLDL { get { try { return Run.Parameters["SLDL"]; } catch { throw new Exception("the parameter SLDL was neither found in your varietal nor in your non varietal file"); } } }

        protected double VAI { get { try{ return Run.Parameters["VAI"]; }catch { throw new Exception("the parameter VAI was neither found in your varietal nor in your non varietal file"); } } }

        protected double Kl { get{ try { return Run.Parameters["Kl"]; }catch { throw new Exception("the parameter Kl was neither found in your varietal nor in your non varietal file"); } } }

        protected double AreaSL { get{ try { return Run.Parameters["AreaSL"]; } catch { throw new Exception("the parameter AreaSL was neither found in your varietal nor in your non varietal file"); } }}

        protected double AreaPL { get { try{ return Run.Parameters["AreaPL"]; } catch { throw new Exception("the parameter AreaPL was neither found in your varietal nor in your non varietal file"); } }}

        protected double AreaSS { get{ try { return Run.Parameters["AreaSS"]; } catch { throw new Exception("the parameter AreaSS was neither found in your varietal nor in your non varietal file"); } }}

        protected double RatioFLPL { get { try{ return Run.Parameters["RatioFLPL"]; } catch { throw new Exception("the parameter RatioFLPL was neither found in your varietal nor in your non varietal file"); } }}

        protected double L_IN1 { get { try{ return Run.Parameters["L_IN1"]; }catch { throw new Exception("the parameter L_IN1 was neither found in your varietal nor in your non varietal file"); } } }

        protected double L_EP { get{ try { return Run.Parameters["L_EP"]; } catch { throw new Exception("the parameter L_EP was neither found in your varietal nor in your non varietal file"); } }}

        protected double NLL { get { try{ return Run.Parameters["NLL"]; } catch { throw new Exception("the parameter NLL was neither found in your varietal nor in your non varietal file"); } }}

        protected double BetaRWU { get{ try { return Run.Parameters["BetaRWU"]; } catch { throw new Exception("the parameter BetaRWU was neither found in your varietal nor in your non varietal file"); } }}

        protected double AlphaAlbGlo { get{ try { return Run.Parameters["AlphaAlbGlo"]; } catch { throw new Exception("the parameter AlphaAlbGlo was neither found in your varietal nor in your non varietal file"); } }}

        protected double AlphaGlu { get { try{ return Run.Parameters["AlphaGlu"]; } catch { throw new Exception("the parameter AlphaGlu was neither found in your varietal nor in your non varietal file"); } }}

        protected double AlphaKn { get { try{ return Run.Parameters["AlphaKn"]; } catch { throw new Exception("the parameter AlphaKn was neither found in your varietal nor in your non varietal file"); } }}

        protected double AlphaNC { get { try { return Run.Parameters["AlphaNC"]; } catch { throw new Exception("the parameter AlphaNC was neither found in your varietal nor in your non varietal file"); } } }

        protected double maxGrainNC { get { try { return Run.Parameters["maxGrainNC"]; } catch { throw new Exception("the parameter maxGrainNC was neither found in your varietal nor in your non varietal file"); } } }
        
        protected double minGrainNC { get { try { return Run.Parameters["minGrainNC"]; } catch { throw new Exception("the parameter minGrainNC was neither found in your varietal nor in your non varietal file"); } } }

        protected double AlphaNNI { get { try{ return Run.Parameters["AlphaNNI"]; } catch { throw new Exception("the parameter AlphaNNI was neither found in your varietal nor in your non varietal file"); } }}

        protected double AlphaSSN { get { try{ return Run.Parameters["AlphaSSN"]; } catch { throw new Exception("the parameter AlphaSSN was neither found in your varietal nor in your non varietal file"); } }}

        protected double BetaAlbGlo { get{ try { return Run.Parameters["BetaAlbGlo"]; }catch { throw new Exception("the parameter BetaAlbGlo was neither found in your varietal nor in your non varietal file"); } } }

        protected double BetaGlu { get { try{ return Run.Parameters["BetaGlu"]; } catch { throw new Exception("the parameter BetaGlu was neither found in your varietal nor in your non varietal file"); } }}

        protected double BetaKn { get { try{ return Run.Parameters["BetaKn"]; }catch { throw new Exception("the parameter BetaKn was neither found in your varietal nor in your non varietal file"); } } }

        protected double BetaNNI { get { try{ return Run.Parameters["BetaNNI"]; } catch { throw new Exception("the parameter BetaNNI was neither found in your varietal nor in your non varietal file"); } }}

        protected double BetaSSN { get { try{ return Run.Parameters["BetaSSN"]; } catch { throw new Exception("the parameter BetaSSN was neither found in your varietal nor in your non varietal file"); } }}

        protected double FacCO2 { get { try{ return Run.Parameters["FacCO2"]; } catch { throw new Exception("the parameter FacCO2 was neither found in your varietal nor in your non varietal file"); } }}

        protected double DMmaxNuptake { get { try{ return Run.Parameters["DMmaxNuptake"]; } catch { throw new Exception("the parameter DMmaxNuptake was neither found in your varietal nor in your non varietal file"); } }}

        protected double EarGR { get { try{ return Run.Parameters["EarGR"]; } catch { throw new Exception("the parameter EarGR was neither found in your varietal nor in your non varietal file"); } }}

        protected double MaxDL { get { try{ return Run.Parameters["MaxDL"]; }catch { throw new Exception("the parameter MaxDL was neither found in your varietal nor in your non varietal file"); } } }

        protected double Alpha { get { try{ return Run.Parameters["Alpha"]; } catch { throw new Exception("the parameter Alpha was neither found in your varietal nor in your non varietal file"); } }}

        protected double tauAlpha { get { try{ return Run.Parameters["tauAlpha"]; }catch { throw new Exception("the parameter tauAlpha was neither found in your varietal nor in your non varietal file"); } } }

        protected double MinDL { get { try{ return Run.Parameters["MinDL"]; } catch { throw new Exception("the parameter MinDL was neither found in your varietal nor in your non varietal file"); } }}

        protected double Tbase { get { try{ return Run.Parameters["Tbase"]; }catch { throw new Exception("the parameter Tbase was neither found in your varietal nor in your non varietal file"); } } }

        protected double FracBEAR { get { try{ return Run.Parameters["FracBEAR"]; } catch { throw new Exception("the parameter FracBEAR was neither found in your varietal nor in your non varietal file"); } }}

        protected double FracLaminaeBGR { get { try{ return Run.Parameters["FracLaminaeBGR"]; } catch { throw new Exception("the parameter FracLaminaeBGR was neither found in your varietal nor in your non varietal file"); } }}

        protected double FracSheathBGR { get{ try { return Run.Parameters["FracSheathBGR"]; }catch { throw new Exception("the parameter FracSheathBGR was neither found in your varietal nor in your non varietal file"); } } }

        internal double FracStemWSC { get { try{ return Run.Parameters["FracStemWSC"]; } catch { throw new Exception("the parameter FracStemWSC was neither found in your varietal nor in your non varietal file"); } }}

        protected double LUE { get { try{ return Run.Parameters["LUE"]; } catch { throw new Exception("the parameter LUE was neither found in your varietal nor in your non varietal file"); } }}

        protected double IntTvern { get { try{ return Run.Parameters["IntTvern"]; } catch { throw new Exception("the parameter IntTvern was neither found in your varietal nor in your non varietal file"); } }}

        public double LowerFPAWlue { get { try{ return Run.Parameters["LowerFPAWlue"]; }catch { throw new Exception("the parameter LowerFPAWlue was neither found in your varietal nor in your non varietal file"); } } }

        public double LowerFPAWexp { get{ try { return Run.Parameters["LowerFPAWexp"]; } catch { throw new Exception("the parameter LowerFPAWexp was neither found in your varietal nor in your non varietal file"); } }}

        public double LowerFPAWsen { get { try{ return Run.Parameters["LowerFPAWsen"]; } catch { throw new Exception("the parameter LowerFPAWsen was neither found in your varietal nor in your non varietal file"); } }}

        public double LowerFPAWgs { get { try{ return Run.Parameters["LowerFPAWgs"]; } catch { throw new Exception("the parameter LowerFPAWgs was neither found in your varietal nor in your non varietal file"); } }}

        public double LowerVPD { get { try{ return Run.Parameters["LowerVPD"]; }catch { throw new Exception("the parameter LowerVPD was neither found in your varietal nor in your non varietal file"); } } }

        protected double LLOSS { get { try{ return Run.Parameters["LLOSS"]; }catch { throw new Exception("the parameter LLOSS was neither found in your varietal nor in your non varietal file"); } } }

        protected double MaxNuptake { get { try{ return Run.Parameters["MaxNuptake"]; }catch { throw new Exception("the parameter MaxNuptake was neither found in your varietal nor in your non varietal file"); } } }

        protected double MaxLeafSoil { get { try{ return Run.Parameters["MaxLeafSoil"]; } catch { throw new Exception("the parameter MaxLeafSoil was neither found in your varietal nor in your non varietal file"); } }}

        protected double MaxLeafRRND { get { try{ return Run.Parameters["MaxLeafRRND"]; } catch { throw new Exception("the parameter MaxLeafRRND was neither found in your varietal nor in your non varietal file"); } }}

        protected double Ldecr { get { try{ return Run.Parameters["Ldecr"]; }catch { throw new Exception("the parameter Ldecr was neither found in your varietal nor in your non varietal file"); } } }

        protected double MaxRWU { get { try{ return Run.Parameters["MaxRWU"]; } catch { throw new Exception("the parameter MaxRWU was neither found in your varietal nor in your non varietal file"); } }}

        protected double MaxStemRRND { get{ try { return Run.Parameters["MaxStemRRND"]; }catch { throw new Exception("the parameter MaxStemRRND was neither found in your varietal nor in your non varietal file"); } } }

        protected double MaxDSF { get{ try { return Run.Parameters["MaxDSF"]; } catch { throw new Exception("the parameter MaxDSF was neither found in your varietal nor in your non varietal file"); } }}

        protected double MaxStemN { get{ try { return Run.Parameters["MaxStemN"]; } catch { throw new Exception("the parameter MaxStemN was neither found in your varietal nor in your non varietal file"); } }}

        protected double MaxTvern { get{ try { return Run.Parameters["MaxTvern"]; } catch { throw new Exception("the parameter MaxTvern was neither found in your varietal nor in your non varietal file"); } }}

        protected double Lincr { get{ try { return Run.Parameters["Lincr"]; } catch { throw new Exception("the parameter Lincr was neither found in your varietal nor in your non varietal file"); } }}

        protected double MinTvern { get { try{ return Run.Parameters["MinTvern"]; } catch { throw new Exception("the parameter MinTvern was neither found in your varietal nor in your non varietal file"); } }}

        protected double Pdecr { get { try{ return Run.Parameters["Pdecr"]; } catch { throw new Exception("the parameter Pdecr was neither found in your varietal nor in your non varietal file"); } }}

        protected double Pincr { get{ try { return Run.Parameters["Pincr"]; }catch { throw new Exception("the parameter Pincr was neither found in your varietal nor in your non varietal file"); } } }

        internal double PexpL { get { try{ return Run.Parameters["PexpL"]; } catch { throw new Exception("the parameter PexpL was neither found in your varietal nor in your non varietal file"); } }}

        internal double PexpIN { get { try{ return Run.Parameters["PexpIN"]; } catch { throw new Exception("the parameter PexpIN was neither found in your varietal nor in your non varietal file"); } }}

        protected double PlagLL { get{ try { return Run.Parameters["PlagLL"]; }catch { throw new Exception("the parameter PlagLL was neither found in your varietal nor in your non varietal file"); } } }

        protected double PlagSL { get { try{ return Run.Parameters["PlagSL"]; }catch { throw new Exception("the parameter PlagSL was neither found in your varietal nor in your non varietal file"); } } }

        protected double PsenLL { get{ try { return Run.Parameters["PsenLL"]; } catch { throw new Exception("the parameter PsenLL was neither found in your varietal nor in your non varietal file"); } }}

        protected double PsenSL { get{ try { return Run.Parameters["PsenSL"]; } catch { throw new Exception("the parameter PsenSL was neither found in your varietal nor in your non varietal file"); } }}

        protected double Kcd { get{ try { return Run.Parameters["Kcd"]; } catch { throw new Exception("the parameter Kcd was neither found in your varietal nor in your non varietal file"); } }}

        protected double RVER { get { try{ return Run.Parameters["RVER"]; } catch { throw new Exception("the parameter RVER was neither found in your varietal nor in your non varietal file"); } }}

        protected double SLNcri { get{ try { return Run.Parameters["SLNcri"]; } catch { throw new Exception("the parameter SLNcri was neither found in your varietal nor in your non varietal file"); } }}

        protected double SLNmax0 { get { try{ return Run.Parameters["SLNmax0"]; } catch { throw new Exception("the parameter SLNmax0 was neither found in your varietal nor in your non varietal file"); } }}

        protected double SLNmin { get { try{ return Run.Parameters["SLNmin"]; }catch { throw new Exception("the parameter SLNmin was neither found in your varietal nor in your non varietal file"); } } }

        protected double SlopeFR { get{ try { return Run.Parameters["SlopeFR"]; } catch { throw new Exception("the parameter SlopeFR was neither found in your varietal nor in your non varietal file"); } }}

        protected double SLWp { get { try{ return Run.Parameters["SLWp"]; }catch { throw new Exception("the parameter SLWp was neither found in your varietal nor in your non varietal file"); } } }

        protected double SSWp { get { try{ return Run.Parameters["SSWp"]; } catch { throw new Exception("the parameter SSWp was neither found in your varietal nor in your non varietal file"); } }}

        protected double StdCO2 { get{ try { return Run.Parameters["StdCO2"]; }catch { throw new Exception("the parameter StdCO2 was neither found in your varietal nor in your non varietal file"); } } }

        protected double StrucLeafN { get{ try { return Run.Parameters["StrucLeafN"]; }catch { throw new Exception("the parameter StrcutLeafN was neither found in your varietal nor in your non varietal file"); } } }

        protected double StrucStemN { get{ try { return Run.Parameters["StrucStemN"]; }catch { throw new Exception("the parameter StructStemN was neither found in your varietal nor in your non varietal file"); } } }

        protected double TargetFertileShoot { get { return Run.ManagementDef.TargetFertileShootNumber; } }

        protected double TauSLN { get { try{ return Run.Parameters["TauSLN"]; } catch { throw new Exception("the parameter TauSLN was neither found in your varietal nor in your non varietal file"); } }}

        protected double LUETopt { get{ try { return Run.Parameters["LUETopt"]; }catch { throw new Exception("the parameter LUETopt was neither found in your varietal nor in your non varietal file"); } } }

        protected double LUETmax { get { try{ return Run.Parameters["LUETmax"]; } catch { throw new Exception("the parameter LUETmax was neither found in your varietal nor in your non varietal file"); } }}

        protected double LUETmin { get { try{ return Run.Parameters["LUETmin"]; }catch { throw new Exception("the parameter LUETmin was neither found in your varietal nor in your non varietal file"); } } }

        protected double LUETshape { get { try{ return Run.Parameters["LUETshape"]; } catch { throw new Exception("the parameter LUETshape was neither found in your varietal nor in your non varietal file"); } }}

        protected double Dcd { get { try{ return Run.Parameters["Dcd"]; } catch { throw new Exception("the parameter Dcd was neither found in your varietal nor in your non varietal file"); } }}

        protected double Der { get { try{ return Run.Parameters["Der"]; }catch { throw new Exception("the parameter Der was neither found in your varietal nor in your non varietal file"); } } }

        protected double SenAccT { get{ try { return Run.Parameters["SenAccT"]; }catch { throw new Exception("the parameter SenAccT was neither found in your varietal nor in your non varietal file"); } } } // #Andrea 23/11/2015
        protected double SenAccSlope { get{ try { return Run.Parameters["SenAccSlope"]; }catch { throw new Exception("the parameter SenAccSlope was neither found in your varietal nor in your non varietal file"); } } } // #Andrea 23/11/2015

        protected double PreAnthesisTmin { get { try { return Run.Parameters["PreAnthesisTmin"]; } catch { throw new Exception("the parameter PreAnthesisTmin was neither found in your varietal nor in your non varietal file"); } } } // #Andrea 27/11/2015
        protected double PreAnthesisTopt { get { try { return Run.Parameters["PreAnthesisTopt"]; } catch { throw new Exception("the parameter PreAnthesisTopt was neither found in your varietal nor in your non varietal file"); } } } // #Andrea 27/11/2015
        protected double PreAnthesisTmax { get { try { return Run.Parameters["PreAnthesisTmax"]; } catch { throw new Exception("the parameter PreAnthesisTmax was neither found in your varietal nor in your non varietal file"); } } } // #Andrea 27/11/2015
        protected double PreAnthesisShape { get { try { return Run.Parameters["PreAnthesisShape"]; } catch { throw new Exception("the parameter PreAnthesisShape was neither found in your varietal nor in your non varietal file"); } } } // #Andrea 27/11/2015
        protected double PostAnthesisTmin { get { try { return Run.Parameters["PostAnthesisTmin"]; } catch { throw new Exception("the parameter PostAnthesisTmin was neither found in your varietal nor in your non varietal file"); } } } // #Andrea 27/11/2015
        protected double PostAnthesisTopt { get { try { return Run.Parameters["PostAnthesisTopt"]; } catch { throw new Exception("the parameter PostAnthesisTopt was neither found in your varietal nor in your non varietal file"); } } } // #Andrea 27/11/2015
        protected double PostAnthesisTmax { get { try { return Run.Parameters["PostAnthesisTmax"]; } catch { throw new Exception("the parameter PostAnthesisTmax was neither found in your varietal nor in your non varietal file"); } } } // #Andrea 27/11/2015
        protected double PostAnthesisShape { get { try { return Run.Parameters["PostAnthesisShape"]; } catch { throw new Exception("the parameter PostAnthesisShape was neither found in your varietal nor in your non varietal file"); } } } // #Andrea 27/11/2015

        protected double intTSFLN { get { try { return Run.Parameters["intTSFLN"]; } catch { throw new Exception("the parameter intTSFLN was neither found in your varietal nor in your non varietal file"); } } }
        protected double slopeTSFLN { get { try { return Run.Parameters["slopeTSFLN"]; } catch { throw new Exception("the parameter slopeTSFLN was neither found in your varietal nor in your non varietal file"); } } }

        public double UpperFPAWlue { get { try { return Run.Parameters["UpperFPAWlue"]; } catch { throw new Exception("the parameter UpperFPAWlue was neither found in your varietal nor in your non varietal file"); } } }
        public double UpperFPAWexp { get { try { return Run.Parameters["UpperFPAWexp"]; } catch { throw new Exception("the parameter UpperFPAWexp was neither found in your varietal nor in your non varietal file"); } } }
        public double UpperFPAWsen { get { try { return Run.Parameters["UpperFPAWsen"]; } catch { throw new Exception("the parameter UpperFPAWsen was neither found in your varietal nor in your non varietal file"); } } }
        public double UpperFPAWgs { get { try { return Run.Parameters["UpperFPAWgs"]; } catch { throw new Exception("the parameter UpperFPAWgs was neither found in your varietal nor in your non varietal file"); } } }
        public double UpperVPD { get { try { return Run.Parameters["UpperVPD"]; } catch { throw new Exception("the parameter UpperVPD was neither found in your varietal nor in your non varietal file"); } } }

        protected double PFLLAnth { get { try { return Run.Parameters["PFLLAnth"]; } catch { throw new Exception("the parameter PFLLAnth was neither found in your varietal nor in your non varietal file"); } } }

        protected double PHEADANTH { get { try { return Run.Parameters["PHEADANTH"]; } catch { throw new Exception("the parameter PHEADANTH was neither found in your varietal nor in your non varietal file"); } } }

        protected double AMNLFNO { get { try { return Run.Parameters["AMNLFNO"]; } catch { throw new Exception("the parameter AMNLFNO was neither found in your varietal nor in your non varietal file"); } } }

        protected double VBEE { get { try { return Run.Parameters["VBEE"]; } catch { throw new Exception("the parameter VBEE was neither found in your varietal nor in your non varietal file"); } } }

        protected double AMXLFNO { get { try { return Run.Parameters["AMXLFNO"]; } catch { throw new Exception("the parameter AMXLFNO was neither found in your varietal nor in your non varietal file"); } } }

        protected BindingList<WeatherFile> WeatherFiles { get { return Run.SiteDef.WeatherFiles; } }

        protected double FixPhyll { get; private set; }    

        #endregion                 
       

        #region parameters energy balance
        protected double stefanBoltzman = 4.903 * Math.Pow(10, -9);
        protected const double albedoCoefficient = 0.23; // albedo coefficient
        protected const double soilDiffusionConstant = 4.2; //soil diffusion constant
        protected const double psychrometricConstant = 0.66; // psychrometric constant
        protected const double vonKarman = 0.41; // von Karman constant
        protected const double heightWeatherMeasurements = 2; // reference height of wind and humidity measurements
        protected const double zm = 0.13; // roughness length governing momentum transfer, FAO
        protected const double lambda = 2.454; // latent heat of vaporization of water
        protected const double specificHeatCapacityAir = 0.00101; // Specific heat capacity of dry air
        protected const double rhoDensityAir = 1.225; // Density of air
        protected const double d = 0.67; // corresponding to 2/3. This is multiplied to the crop heigth for calculating the zero plane displacement height, FAO
        protected const double zh = zm * 0.1; // roughness length governing transfer of heat and vapour, FAO

        # endregion

        #region Parameters Maize

        protected double leafNoInitEmerg { get { return Run.Parameters["leafNoInitEmerg"]; } }
        protected double LIR { get { return Run.Parameters["LIR"]; } }
        protected double Leaf_tip_emerg { get { return Run.Parameters["Leaf_tip_emerg"]; } }
        protected double atip { get { return Run.Parameters["atip"]; } }
        protected double ttll1 { get { return Run.Parameters["ttll1"]; } }
        protected double a_ll1 { get { return Run.Parameters["a_ll1"]; } }
        protected double Nfinal { get { return Run.Parameters["Nfinal"]; } }
        protected double Lagmax { get { return Run.Parameters["Lagmax"]; } }

        protected double Sigma { get { return Run.Parameters["Sigma"]; } }
        protected double AlphaLER { get { return Run.Parameters["AlphaLER"]; } }

        protected double LERa { get { return Run.Parameters["LERa"]; } }
        protected double LERb { get { return Run.Parameters["LERb"]; } }
        protected double LERc { get { return Run.Parameters["LERc"]; } }

        protected double SLNparam { get { return Run.Parameters["SLNparam"]; } }
        protected int BLrankFLN { get { return (int)Run.Parameters["BLrankFLN"]; } }
        protected double SLsize { get { return Run.Parameters["SLsize"]; } }
        protected double BLsize { get { return Run.Parameters["BLsize"]; } }

        // Parameters that are non-genotypic in the model :
        protected double k_bl = 1.412;
        protected double Nlim = 6.617; 
        protected double alpha_tr = 0.5; 
        protected double k_ll = 2.4;

        ////
        protected double plantDensity { get { return Run.Parameters["plantDensity"]; } }

        #endregion

        #region Maize switch
        //true for Maize and false for Wheat 
        public bool SwitchMaize
        {
            get { return Universe_.switchMaize; }
        }

        #endregion

        #region MTG

        public csMTG.Gramene mtg_
        {
            get { return Universe_.mtg_; }
        }

        #endregion

        public virtual void Dispose()
        {
            Universe_ = null;
        }

        public void CalculateFixphyll()
        {
            var SowingDay = FinalSowingDateFixPhyll.DayOfYear;
            if (Latitude < 0)
            {
                if (SowingDay > SDsa_sh)
                {
                    FixPhyll = P * (1 - Rp * Math.Min(SowingDay - SDsa_sh, SDws));
                    // FixPhyll = parameters["P"] * (1 - parameters["Rp"] * (Math.Min(SowingDay , 241) - 151));
            }
                else FixPhyll = P;
            }
            else
            {
                if (SowingDay < SDsa_nh)
                {
                    FixPhyll = P * (1 - Rp * Math.Min(SowingDay, SDws));
                }
                else FixPhyll = P;
            }
        }
    }

}

