using System;
using SiriusModel.InOut.Base;
using System.Xml.Serialization;
using System.Collections.Generic;


namespace SiriusModel.InOut
{
    ///<summary>
    ///RunOption values.
    ///</summary>
    [Serializable]
    public class RunOptionItem : ProjectDataFileItemNoChild
    {
        [XmlElement(ElementName = "OutputPattern")]
        private string outputPattern;
        public string OutputPattern
        {
            get { return outputPattern; }
            set { this.SetObject(ref outputPattern, ref value, "OutputPattern"); }
        }
        
        private bool useObservedGrainNumber;
        public bool UseObservedGrainNumber
        {
            get { return useObservedGrainNumber; }
            set { this.SetStruct(ref useObservedGrainNumber, ref value, "UseObservedGrainNumber"); }
        }

        ///<Behnam>

        private bool ignoreGrainMaturation;
        public bool IgnoreGrainMaturation
        {
            get { return ignoreGrainMaturation; }
            set { this.SetStruct(ref ignoreGrainMaturation, ref value, "IgnoreGrainMaturation"); }
        }
        
        private bool useairtemperatureforsenescence;
        public bool UseAirTemperatureForSenescence
        {
            get { return useairtemperatureforsenescence; }
            set { this.SetStruct(ref useairtemperatureforsenescence, ref value, "UseAirTemperatureForSenescence"); }
        }

        private bool iscutongrainfillnotuse;
        public bool IsCutOnGrainFillNotUse
        {
            get { return iscutongrainfillnotuse; }
            set { this.SetStruct(ref iscutongrainfillnotuse, ref value, "IsCutOnGrainFillNotUse"); }
        }


        /* private bool useActualBase;
        public bool UseActualBase
        {
            get { return useActualBase; }
            set { this.SetStruct(ref useActualBase, ref value, "IgnoreGrainMaturation"); }
        } */

        private bool unlimitedWater;
        public bool UnlimitedWater
        {
            get { return unlimitedWater; }
            set { this.SetStruct(ref unlimitedWater, ref value, "UnlimitedWater"); }
        }

        private bool unlimitedNitrogen;
        public bool UnlimitedNitrogen
        {
            get { return unlimitedNitrogen; }
            set { this.SetStruct(ref unlimitedNitrogen, ref value, "UnlimitedNitrogen"); }
        }

        private bool unlimitedTemperature;
        public bool UnlimitedTemperature
        {
            get { return unlimitedTemperature; }
            set { this.SetStruct(ref unlimitedTemperature, ref value, "UnlimitedTemperature"); }
        }

        private double maxTempThreshold;
        public double MaxTempThreshold
        {
            get { return maxTempThreshold; }
            set { this.SetStruct(ref maxTempThreshold, ref value, "MaxTempThreshold"); }
        }

        private double wCompensationLevel;
        public double WCompensationLevel
        {
            get { return wCompensationLevel; }
            set { 
                this.SetStruct(ref wCompensationLevel, ref value, "WCompensationLevel");
                this.Assert(wCompensationLevel, d => d >= 0, "Level of water deficit compensation", ">=0", null);
            }
        }

        private double nCompensationLevel;
        public double NCompensationLevel
        {
            get { return nCompensationLevel; }
            set { 
                this.SetStruct(ref nCompensationLevel, ref value, "NCompensationLevel");
                this.Assert(nCompensationLevel, d => d >= 0, "Level of N deficit compensation", ">=0", null);
            }
        }

        private bool doInteractions;
        public bool DoInteractions
        {
            get { return doInteractions; }
            set { this.SetStruct(ref doInteractions, ref value, "DoInteractions"); }
        }

        private bool interactionsW;
        public bool InteractionsW
        {
            get { return interactionsW; }
            set { this.SetStruct(ref interactionsW, ref value, "InteractionsW"); }
        }

        private bool interactionsN;
        public bool InteractionsN
        {
            get { return interactionsN; }
            set { this.SetStruct(ref interactionsN, ref value, "InteractionsN"); }
        }

        private bool interactionsT;
        public bool InteractionsT
        {
            get { return interactionsT; }
            set { this.SetStruct(ref interactionsT, ref value, "InteractionsT"); }
        }

        private bool interactionsWN;
        public bool InteractionsWN
        {
            get { return interactionsWN; }
            set { this.SetStruct(ref interactionsWN, ref value, "InteractionsWN"); }
        }

        private bool interactionsWT;
        public bool InteractionsWT
        {
            get { return interactionsWT; }
            set { this.SetStruct(ref interactionsWT, ref value, "InteractionsWT"); }
        }

        private bool interactionsNT;
        public bool InteractionsNT
        {
            get { return interactionsNT; }
            set { this.SetStruct(ref interactionsNT, ref value, "InteractionsNT"); }
        }

        private bool interactionsWNT;
        public bool InteractionsWNT
        {
            get { return interactionsWNT; }
            set { this.SetStruct(ref interactionsWNT, ref value, "InteractionsWNT"); }
        }

        // Summary Outputs
        private bool management;
        public bool Management
        {
            get { return management; }
            set { this.SetStruct(ref management, ref value, "Management"); }
        }

        private bool nonVarietalParameters;
        public bool NonVarietalParameters
        {
            get { return nonVarietalParameters; }
            set { this.SetStruct(ref nonVarietalParameters, ref value, "NonVarietalParameters"); }
        }

        private bool varietalParameters;
        public bool VarietalParameters
        {
            get { return varietalParameters; }
            set { this.SetStruct(ref varietalParameters, ref value, "VarietalParameters"); }
        }
        private bool runOptions;
        public bool RunOptions
        {
            get { return runOptions; }
            set { this.SetStruct(ref runOptions, ref value, "RunOptions"); }
        }

        private bool site;
        public bool Site
        {
            get { return site; }
            set { this.SetStruct(ref site, ref value, "Site"); }
        }

        private bool soil;
        public bool Soil
        {
            get { return soil; }
            set { this.SetStruct(ref soil, ref value, "Soil"); }
        }
        private bool sowingYear;
        public bool SowingYear
        {
            get { return sowingYear; }
            set { this.SetStruct(ref sowingYear, ref value, "SowingYear"); }
        }

        private bool sowingWindow;
        public bool SowingWindow
        {
            get { return sowingWindow; }
            set { this.SetStruct(ref sowingWindow, ref value, "SowingWindow"); }
        }

        private bool sowingDateOut;
        public bool SowingDateOut
        {
            get { return sowingDateOut; }
            set { this.SetStruct(ref sowingDateOut, ref value, "SowingDateOut"); }
        }

        private bool sowingDateOutDOY;
        public bool SowingDateOutDOY
        {
            get { return sowingDateOutDOY; }
            set { this.SetStruct(ref sowingDateOutDOY, ref value, "SowingDateOutDOY"); }
        }


        private bool emergenceDate;
        public bool EmergenceDate
        {
            get { return emergenceDate; }
            set { this.SetStruct(ref emergenceDate, ref value, "EmergenceDate"); }
        }

        private bool emergenceDateDOY;
        public bool EmergenceDateDOY
        {
            get { return emergenceDateDOY; }
            set { this.SetStruct(ref emergenceDateDOY, ref value, "EmergenceDateDOY"); }
        }


        private bool emergenceDay;
        public bool EmergenceDay
        {
            get { return emergenceDay; }
            set { this.SetStruct(ref emergenceDay, ref value, "EmergenceDay"); }
        }

        private bool endVernalizationDate;
        public bool EndVernalizationDate
        {
            get { return endVernalizationDate; }
            set { this.SetStruct(ref endVernalizationDate, ref value, "EndVernalizationDate"); }
        }
        
        private bool fistNodeDetectable;
        public bool FirstNodeDetectable
        {
            get { return fistNodeDetectable; }
            set { this.SetStruct(ref fistNodeDetectable, ref value, "FirstNodeDetectable"); }
        }

        private bool beginningOfStemExtension;
        public bool BeginningOfStemExtension
        {
            get { return beginningOfStemExtension; }
            set { this.SetStruct(ref beginningOfStemExtension, ref value, "BeginningOfStemExtension"); }
        }

        private bool terminalSpikeletDate;
        public bool TerminalSpikeletDate
        {
            get { return terminalSpikeletDate; }
            set { this.SetStruct(ref terminalSpikeletDate, ref value, "TerminalSpikeletDate"); }
        }
        
        private bool flagLeafLiguleJustVisibleDate;
        public bool FlagLeafLiguleJustVisibleDate
        {
            get { return flagLeafLiguleJustVisibleDate; }
            set { this.SetStruct(ref flagLeafLiguleJustVisibleDate, ref value, "FlagLeafLiguleJustVisibleDate"); }
        }

        private bool headingDate;
        public bool HeadingDate
        {
            get { return headingDate; }
            set { this.SetStruct(ref headingDate, ref value, "HeadingDate"); }
        }

        private bool anthesisDate;
        public bool AnthesisDate
        {
            get { return anthesisDate; }
            set { this.SetStruct(ref anthesisDate, ref value, "AnthesisDate"); }
        }

        private bool anthesisDateDOY;
        public bool AnthesisDateDOY
        {
            get { return anthesisDateDOY; }
            set { this.SetStruct(ref anthesisDateDOY, ref value, "AnthesisDateDOY"); }
        }

        private bool anthesisDay;
        public bool AnthesisDay
        {
            get { return anthesisDay; }
            set { this.SetStruct(ref anthesisDay, ref value, "AnthesisDay"); }
        }

        private bool endOfCellDivision;
        public bool EndOfCellDivision
        {
            get { return endOfCellDivision; }
            set { this.SetStruct(ref endOfCellDivision, ref value, "EndOfCellDivision"); }
        }
        private bool endOfGrainFilling;
        public bool EndOfGrainFilling
        {
            get { return endOfGrainFilling; }
            set { this.SetStruct(ref endOfGrainFilling, ref value, "EndOfGrainFilling"); }
        }

        private bool maturityDate;
        public bool MaturityDate
        {
            get { return maturityDate; }
            set { this.SetStruct(ref maturityDate, ref value, "MaturityDate"); }
        }

        private bool maturityDateDOY;
        public bool MaturityDateDOY
        {
            get { return maturityDateDOY; }
            set { this.SetStruct(ref maturityDateDOY, ref value, "MaturityDateDOY"); }
        }

        private bool maturityDay;
        public bool MaturityDay
        {
            get { return maturityDay; }
            set { this.SetStruct(ref maturityDay, ref value, "MaturityDay"); }
        }

        private bool meanTempAnthesis;
        public bool MeanTempAnthesis
        {
            get { return meanTempAnthesis; }
            set { this.SetStruct(ref meanTempAnthesis, ref value, "MeanTempAnthesis"); }
        }

        private bool meanTempAnth2Maturity;
        public bool MeanTempAnth2Maturity
        {
            get { return meanTempAnth2Maturity; }
            set { this.SetStruct(ref meanTempAnth2Maturity, ref value, "meanTempAnth2Maturity"); }
        }

        private bool meanMaxCanopyTempMaturity;
        public bool MeanMaxCanopyTempMaturity
        {
            get { return meanMaxCanopyTempMaturity; }
            set { this.SetStruct(ref meanMaxCanopyTempMaturity, ref value, "MeanMaxCanopyTempMaturity"); }
        }

        private bool meanMaxAirTempMaturity;
        public bool MeanMaxAirTempMaturity
        {
            get { return meanMaxAirTempMaturity; }
            set { this.SetStruct(ref meanMaxAirTempMaturity, ref value, "MeanMaxAirTempMaturity"); }
        }

        private bool meanTempMaturity;
        public bool MeanTempMaturity
        {
            get { return meanTempMaturity; }
            set { this.SetStruct(ref meanTempMaturity, ref value, "MeanTempMaturity"); }
        }

        private bool physTempAnthesis;
        public bool PhysTempAnthesis
        {
            get { return physTempAnthesis; }
            set { this.SetStruct(ref physTempAnthesis, ref value, "PhysTempAnthesis"); }
        }

        private bool physTempAnth2Maturity;
        public bool PhysTempAnth2Maturity
        {
            get { return physTempAnth2Maturity; }
            set { this.SetStruct(ref physTempAnth2Maturity, ref value, "PhysTempAnth2Maturity"); }
        }

        private bool physTempMaturity;
        public bool PhysTempMaturity
        {
            get { return physTempMaturity; }
            set { this.SetStruct(ref physTempMaturity, ref value, "PhysTempMaturity"); }
        }

        private bool finalLeafNumberOption;
        public bool FinalLeafNumberOption
        {
            get { return finalLeafNumberOption; }
            set { this.SetStruct(ref finalLeafNumberOption, ref value, "FinalLeafNumberOption"); }
        }
        private bool lAIatAnthesis;
        public bool LAIatAnthesis
        {
            get { return lAIatAnthesis; }
            set { this.SetStruct(ref lAIatAnthesis, ref value, "LAIatAnthesis"); }
        }

        private bool gAIatAnthesis;
        public bool GAIatAnthesis
        {
            get { return gAIatAnthesis; }
            set { this.SetStruct(ref gAIatAnthesis, ref value, "GAIatAnthesis"); }
        }

        private bool cropDryMatAnthesis;
        public bool CropDryMatAnthesis
        {
            get { return cropDryMatAnthesis; }
            set { this.SetStruct(ref cropDryMatAnthesis, ref value, "CropDryMatAnthesis"); }
        }
        private bool cropDryMatMatururity;
        public bool CropDryMatMatururity
        {
            get { return cropDryMatMatururity; }
            set { this.SetStruct(ref cropDryMatMatururity, ref value, "CropDryMatMatururity"); }
        }

        private bool grainDryMatMatururity;
        public bool GrainDryMatMatururity
        {
            get { return grainDryMatMatururity; }
            set { this.SetStruct(ref grainDryMatMatururity, ref value, "GrainDryMatMatururity"); }
        }

        private bool nNIatAnthesis;
        public bool NNIatAnthesis
        {
            get { return nNIatAnthesis; }
            set { this.SetStruct(ref nNIatAnthesis, ref value, "NNIatAnthesis"); }
        }

        private bool cropNatAnthesis;
        public bool CropNatAnthesis
        {
            get { return cropNatAnthesis; }
            set { this.SetStruct(ref cropNatAnthesis, ref value, "CropNatAnthesis"); }
        }
        private bool cropNatMaturity;
        public bool CropNatMaturity
        {
            get { return cropNatMaturity; }
            set { this.SetStruct(ref cropNatMaturity, ref value, "CropNatMaturity"); }
        }

        private bool grainNatMaturity;
        public bool GrainNatMaturity
        {
            get { return grainNatMaturity; }
            set { this.SetStruct(ref grainNatMaturity, ref value, "GrainNatMaturity"); }
        }

        private bool postAnthesisCropNUptake;
        public bool PostAnthesisCropNUptake
        {
            get { return postAnthesisCropNUptake; }
            set { this.SetStruct(ref postAnthesisCropNUptake, ref value, "PostAnthesisCropNUptake"); }
        }
        private bool singleGrainDMatMaturity;
        public bool SingleGrainDMatMaturity
        {
            get { return singleGrainDMatMaturity; }
            set { this.SetStruct(ref singleGrainDMatMaturity, ref value, "SingleGrainDMatMaturity"); }
        }

        private bool singleGrainNatMaturity;
        public bool SingleGrainNatMaturity
        {
            get { return singleGrainNatMaturity; }
            set { this.SetStruct(ref singleGrainNatMaturity, ref value, "SingleGrainNatMaturity"); }
        }

        private bool grainProteinAtMaturity;
        public bool GrainProteinAtMaturity
        {
            get { return grainProteinAtMaturity; }
            set { this.SetStruct(ref grainProteinAtMaturity, ref value, "GrainProteinAtMaturity"); }
        }
        private bool grainNumberOption;
        public bool GrainNumberOption
        {
            get { return grainNumberOption; }
            set { this.SetStruct(ref grainNumberOption, ref value, "GrainNumberOption"); }
        }

        private bool starchAtMaturity;
        public bool StarchAtMaturity
        {
            get { return starchAtMaturity; }
            set { this.SetStruct(ref starchAtMaturity, ref value, "StarchAtMaturity"); }
        }

        private bool albuminsAtMaturity;
        public bool AlbuminsAtMaturity
        {
            get { return albuminsAtMaturity; }
            set { this.SetStruct(ref albuminsAtMaturity, ref value, "AlbuminsAtMaturity"); }
        }
        private bool amphiphilsAtMaturity;
        public bool AmphiphilsAtMaturity
        {
            get { return amphiphilsAtMaturity; }
            set { this.SetStruct(ref amphiphilsAtMaturity, ref value, "AmphiphilsAtMaturity"); }
        }

        private bool gliadinsAtMaturity;
        public bool GliadinsAtMaturity
        {
            get { return gliadinsAtMaturity; }
            set { this.SetStruct(ref gliadinsAtMaturity, ref value, "GliadinsAtMaturity"); }
        }

        private bool gluteninsAtMaturity;
        public bool GluteninsAtMaturity
        {
            get { return gluteninsAtMaturity; }
            set { this.SetStruct(ref gluteninsAtMaturity, ref value, "GluteninsAtMaturity"); }
        }
        private bool gliadinsPAtMaturity;
        public bool GliadinsPAtMaturity
        {
            get { return gliadinsPAtMaturity; }
            set { this.SetStruct(ref gliadinsPAtMaturity, ref value, "GliadinsPAtMaturity"); }
        }

        private bool gluteinsPAtMaturity;
        public bool GluteinsPAtMaturity
        {
            get { return gluteinsPAtMaturity; }
            set { this.SetStruct(ref gluteinsPAtMaturity, ref value, "GluteinsPAtMaturity"); }
        }

        private bool gliadinsToGluteinsOption;
        public bool GliadinsToGluteinsOption
        {
            get { return gliadinsToGluteinsOption; }
            set { this.SetStruct(ref gliadinsToGluteinsOption, ref value, "GliadinsToGluteins"); }
        }
        private bool harvestIndex;
        public bool HarvestIndex
        {
            get { return harvestIndex; }
            set { this.SetStruct(ref harvestIndex, ref value, "HarvestIndex"); }
        }

        private bool nHarvestIndex;
        public bool NHarvestIndex
        {
            get { return nHarvestIndex; }
            set { this.SetStruct(ref nHarvestIndex, ref value, "NHarvestIndex"); }
        }

        private bool rainIrrigationAnthesis;
        public bool RainIrrigationAnthesis
        {
            get { return rainIrrigationAnthesis; }
            set { this.SetStruct(ref rainIrrigationAnthesis, ref value, "RainIrrigationAnthesis"); }
        }

        private bool rainIrrigationMaturity;
        public bool RainIrrigationMaturity
        {
            get { return rainIrrigationMaturity; }
            set { this.SetStruct(ref rainIrrigationMaturity, ref value, "RainIrrigationMaturity"); }
        }

        private bool rainIrrigationAnth2Maturity;
        public bool RainIrrigationAnth2Maturity
        {
            get { return rainIrrigationAnth2Maturity; }
            set { this.SetStruct(ref rainIrrigationAnth2Maturity, ref value, "RainIrrigationAnth2Maturity"); }
        }

        private bool cumPotETAnthesis;
        public bool CumPotETAnthesis
        {
            get { return cumPotETAnthesis; }
            set { this.SetStruct(ref cumPotETAnthesis, ref value, "CumPotETAnthesis"); }
        }

        private bool cumPotETMaturity;
        public bool CumPotETMaturity
        {
            get { return cumPotETMaturity; }
            set { this.SetStruct(ref cumPotETMaturity, ref value, "CumPotETMaturity"); }
        }

        private bool cumPotETAnth2Maturity;
        public bool CumPotETAnth2Maturity
        {
            get { return cumPotETAnth2Maturity; }
            set { this.SetStruct(ref cumPotETAnth2Maturity, ref value, "CumPotETAnth2Maturity"); }
        }

        private bool cumActETAnthesis;
        public bool CumActETAnthesis
        {
            get { return cumActETAnthesis; }
            set { this.SetStruct(ref cumActETAnthesis, ref value, "CumActETAnthesis"); }
        }

        private bool cumActETMaturity;
        public bool CumActETMaturity
        {
            get { return cumActETMaturity; }
            set { this.SetStruct(ref cumActETMaturity, ref value, "CumActETMaturity"); }
        }

        private bool cumActETAnth2Maturity;
        public bool CumActETAnth2Maturity
        {
            get { return cumActETAnth2Maturity; }
            set { this.SetStruct(ref cumActETAnth2Maturity, ref value, "CumActETAnth2Maturity"); }
        }

        private bool cumActTrAnthesis;
        public bool CumActTrAnthesis
        {
            get { return cumActTrAnthesis; }
            set { this.SetStruct(ref cumActTrAnthesis, ref value, "CumActTrAnthesis"); }
        }

        private bool cumActTrAnth2Maturity;
        public bool CumActTrAnth2Maturity
        {
            get { return cumActTrAnth2Maturity; }
            set { this.SetStruct(ref cumActTrAnth2Maturity, ref value, "CumActTrAnth2Maturity"); }
        }

        private bool cumActTrMaturity;
        public bool CumActTrMaturity
        {
            get { return cumActTrMaturity; }
            set { this.SetStruct(ref cumActTrMaturity, ref value, "CumActTrMaturity"); }
        }

        private bool cumEvaporation;
        public bool CumEvaporation
        {
            get { return cumEvaporation; }
            set { this.SetStruct(ref cumEvaporation, ref value, "CumEvaporation"); }
        }

        private bool cO2AtEmergence;
        public bool CO2AtEmergence
        {
            get { return cO2AtEmergence; }
            set { this.SetStruct(ref cO2AtEmergence, ref value, "CO2AtEmergence"); }
        }

        private bool cO2AtMaturity;
        public bool CO2AtMaturity
        {
            get { return cO2AtMaturity; }
            set { this.SetStruct(ref cO2AtMaturity, ref value, "CO2AtMaturity"); }
        }

        private bool totalNApplied;
        public bool CumNApplied
        {
            get { return totalNApplied; }
            set { this.SetStruct(ref totalNApplied, ref value, "CumNApplied"); }
        }

        private bool totalAvaiSoilN;
        public bool TotalAvaiSoilN
        {
            get { return totalAvaiSoilN; }
            set { this.SetStruct(ref totalAvaiSoilN, ref value, "TotalAvaiSoilN"); }
        }

        private bool nLeaching;
        public bool NLeaching
        {
            get { return nLeaching; }
            set { this.SetStruct(ref nLeaching, ref value, "NLeaching"); }
        }
        private bool drainage;
        public bool Drainage
        {
            get { return drainage; }
            set { this.SetStruct(ref drainage, ref value, "Drainage"); }
        }

        private bool cumNMineralization;
        public bool CumNMineralization
        {
            get { return cumNMineralization; }
            set { this.SetStruct(ref cumNMineralization, ref value, "CumNMineralization"); }
        }

        private bool cumNDenitrification;
        public bool CumNDenitrification
        {
            get { return cumNDenitrification; }
            set { this.SetStruct(ref cumNDenitrification, ref value, "CumNDenitrification"); }
        }

        private bool fPAWatAnthesis;
        public bool FPAWatAnthesis
        {
            get { return fPAWatAnthesis; }
            set { this.SetStruct(ref fPAWatAnthesis, ref value, "FPAWatAnthesis"); }
        }

        private bool fPAWatMaturity;
        public bool FPAWatMaturity
        {
            get { return fPAWatMaturity; }
            set { this.SetStruct(ref fPAWatMaturity, ref value, "FPAWatMaturity"); }
        }

        private bool availableWaterinSoilatMaturity;
        public bool AvailableWaterinSoilatMaturity
        {
            get { return availableWaterinSoilatMaturity; }
            set { this.SetStruct(ref availableWaterinSoilatMaturity, ref value, "AvailableWaterinSoilatMaturity"); }
        }

        private bool availableMineralNinSoilAtMaturity;
        public bool AvailableMineralNinSoilAtMaturity
        {
            get { return availableMineralNinSoilAtMaturity; }
            set { this.SetStruct(ref availableMineralNinSoilAtMaturity, ref value, "AvailableMineralNinSoilAtMaturity"); }
        }

        private bool totalSoilMineralNatMaturity;
        public bool TotalSoilMineralNatMaturity
        {
            get { return totalSoilMineralNatMaturity; }
            set { this.SetStruct(ref totalSoilMineralNatMaturity, ref value, "TotalSoilMineralNatMaturity"); }
        }
        private bool availableWaterinRootZoneMaturity;
        public bool AvailableWaterinRootZoneMaturity
        {
            get { return availableWaterinRootZoneMaturity; }
            set { this.SetStruct(ref availableWaterinRootZoneMaturity, ref value, "AvailableWaterinRootZoneMaturity"); }
        }

        private bool availableMineralNinRootZoneMaturity;
        public bool AvailableMineralNinRootZoneMaturity
        {
            get { return availableMineralNinRootZoneMaturity; }
            set { this.SetStruct(ref availableMineralNinRootZoneMaturity, ref value, "AvailableMineralNinRootZoneMaturity"); }
        }

        private bool totalMineralNinRootZoneMaturity;
        public bool TotalMineralNinRootZoneMaturity
        {
            get { return totalMineralNinRootZoneMaturity; }
            set { this.SetStruct(ref totalMineralNinRootZoneMaturity, ref value, "TotalMineralNinRootZoneMaturity"); }
        }

        private bool nUseEfficiency;
        public bool NUseEfficiency
        {
            get { return nUseEfficiency; }
            set { this.SetStruct(ref nUseEfficiency, ref value, "NUseEfficiency"); }
        }

        private bool nUtilisationEfficiency;
        public bool NUtilisationEfficiency
        {
            get { return nUtilisationEfficiency; }
            set { this.SetStruct(ref nUtilisationEfficiency, ref value, "NUtilisationEfficiency"); }
        }

        private bool nUptakeEfficiency;
        public bool NUptakeEfficiency
        {
            get { return nUptakeEfficiency; }
            set { this.SetStruct(ref nUptakeEfficiency, ref value, "NUptakeEfficiency"); }
        }

        private bool waterUseEfficiency;
        public bool WaterUseEfficiency
        {
            get { return waterUseEfficiency; }
            set { this.SetStruct(ref waterUseEfficiency, ref value, "WaterUseEfficiency"); }
        }

        private bool accumPAREmerge2EndGrainFill;
        public bool AccumPAREmerge2EndGrainFill
        {
            get { return accumPAREmerge2EndGrainFill; }
            set { this.SetStruct(ref accumPAREmerge2EndGrainFill, ref value, "AccumPAREmerge2EndGrainFill"); }
        }

       // Daily Outputs: Top Items

        private bool versionDateItems;
        public bool VersionDateItems
        {
            get { return versionDateItems; }
            set { this.SetStruct(ref versionDateItems, ref value, "VersionDateItems"); }
        }

        private bool filesItems;
        public bool FilesItems
        {
            get { return filesItems; }
            set { this.SetStruct(ref filesItems, ref value, "FilesItems"); }
        }

        private bool dateItems;
        public bool DateItems
        {
            get { return dateItems; }
            set { this.SetStruct(ref dateItems, ref value, "DateItems"); }
        }

        private bool cropItems;
        public bool CropItems
        {
            get { return cropItems; }
            set { this.SetStruct(ref cropItems, ref value, "CropItems"); }
        }

        private bool managementItems;
        public bool ManagementItems
        {
            get { return managementItems; }
            set { this.SetStruct(ref managementItems, ref value, "ManagementItems"); }
        }

        private bool parameterItems;
        public bool ParameterItems
        {
            get { return parameterItems; }
            set { this.SetStruct(ref parameterItems, ref value, "ParameterItems"); }
        }

        private bool runOptionItems;
        public bool RunOptionItems
        {
            get { return runOptionItems; }
            set { this.SetStruct(ref runOptionItems, ref value, "RunOptionItems"); }
        }

        private bool siteItems;
        public bool SiteItems
        {
            get { return siteItems; }
            set { this.SetStruct(ref siteItems, ref value, "SiteItems"); }
        }

        private bool soilItems;
        public bool SoilItems
        {
            get { return soilItems; }
            set { this.SetStruct(ref soilItems, ref value, "SoilItems"); }
        }

        private bool varietyItems;
        public bool VarietyItems
        {
            get { return varietyItems; }
            set { this.SetStruct(ref varietyItems, ref value, "VarietyItems"); }
        }
        // Daily Outputs 

        private bool dailyGrowthDay;
        public bool DailyGrowthDay
        {
            get { return dailyGrowthDay; }
            set { this.SetStruct(ref dailyGrowthDay, ref value, "DailyGrowthDay"); }
        }

        private bool dailyDate;
        public bool DailyDate
        {
            get { return dailyDate; }
            set { this.SetStruct(ref dailyDate, ref value, "DailyDate"); }
        }

        private bool dailyDOY;
        public bool DailyDOY
        {
            get { return dailyDOY; }

            set { this.SetStruct(ref dailyDOY, ref value, "DailyDOY"); }
        }

        private bool dailyDayLength;
        public bool DailyDayLength
        {
            get { return dailyDayLength; }
            set { this.SetStruct(ref dailyDayLength, ref value, "DailyDayLength"); }
        }

        private bool dailyCO2Conc;
        public bool DailyCO2Conc
        {
            get { return dailyCO2Conc; }
            set { this.SetStruct(ref dailyCO2Conc, ref value, "DailyCO2Conc"); }
        }

        private bool dailyThermaltimeaftersowingAir;
        public bool DailyThermaltimeaftersowingAir
        {
            get { return dailyThermaltimeaftersowingAir; }
            set { this.SetStruct(ref dailyThermaltimeaftersowingAir, ref value, "DailyThermaltimeaftersowingAir"); }
        }

        private bool dailyThermaltimeaftersowingShoot;
        public bool DailyThermaltimeaftersowingShoot
        {
            get { return dailyThermaltimeaftersowingShoot; }
            set { this.SetStruct(ref dailyThermaltimeaftersowingShoot, ref value, "DailyThermaltimeaftersowingShoot"); }
        }

        private bool dailyPhysThermaltimeaftersowing;
        public bool DailyPhysThermaltimeaftersowing
        {
            get { return dailyPhysThermaltimeaftersowing; }
            set { this.SetStruct(ref dailyPhysThermaltimeaftersowing, ref value, "DailyPhysThermaltimeaftersowing"); }
        }

        private bool dailyAirtemperature;
        public bool DailyAirtemperature
        {
            get { return dailyAirtemperature; }
            set { this.SetStruct(ref dailyAirtemperature, ref value, "DailyAirtemperature"); }
        }

        private bool dailySoiltemperature;
        public bool DailySoiltemperature
        {
            get { return dailySoiltemperature; }
            set { this.SetStruct(ref dailySoiltemperature, ref value, "DailySoiltemperature"); }
        }

        private bool dailyCanopytemperature;
        public bool DailyCanopytemperature
        {
            get { return dailyCanopytemperature; }
            set { this.SetStruct(ref dailyCanopytemperature, ref value, "DailyCanopytemperature"); }
        }

        private bool dailyRootingdepth;
        public bool DailyRootingdepth
        {
            get { return dailyRootingdepth; }
            set { this.SetStruct(ref dailyRootingdepth, ref value, "DailyRootingdepth"); }
        }

        private bool dailyCumulativerainirrigation;
        public bool DailyCumulativerainirrigation
        {
            get { return dailyCumulativerainirrigation; }
            set { this.SetStruct(ref dailyCumulativerainirrigation, ref value, "DailyCumulativerainirrigation"); }
        }

        private bool dailyVPDAirOut;
        public bool DailyVPDAirOut
        {
            get { return dailyVPDAirOut; }
            set { this.SetStruct(ref dailyVPDAirOut, ref value, "DailyVPDAirOut"); }
        }

        private bool dailyVPDAirCanopyOut;
        public bool DailyVPDAirCanopyOut
        {
            get { return dailyVPDAirCanopyOut; }
            set { this.SetStruct(ref dailyVPDAirCanopyOut, ref value, "DailyVPDAirCanopyOut"); }
        }

        private bool dailyCumulativePET;
        public bool DailyCumulativePET
        {
            get { return dailyCumulativePET; }
            set { this.SetStruct(ref dailyCumulativePET, ref value, "DailyCumulativePET"); }
        }

        private bool dailyCumulativeET;
        public bool DailyCumulativeET
        {
            get { return dailyCumulativeET; }
            set { this.SetStruct(ref dailyCumulativeET, ref value, "DailyCumulativeET"); }
        }

        private bool dailyCumulativePT;
        public bool DailyCumulativePT
        {
            get { return dailyCumulativePT; }
            set { this.SetStruct(ref dailyCumulativePT, ref value, "DailyCumulativePT"); }
        }

        private bool dailyCumulativeT;
        public bool DailyCumulativeT
        {
            get { return dailyCumulativeT; }
            set { this.SetStruct(ref dailyCumulativeT, ref value, "DailyCumulativeT"); }
        }

        private bool dailyCumulativewaterdrainage;
        public bool DailyCumulativewaterdrainage
        {
            get { return dailyCumulativewaterdrainage; }
            set { this.SetStruct(ref dailyCumulativewaterdrainage, ref value, "DailyCumulativewaterdrainage"); }
        }

        private bool dailyAvailablewaterinsoilprofil;
        public bool DailyAvailablewaterinsoilprofil
        {
            get { return dailyAvailablewaterinsoilprofil; }
            set { this.SetStruct(ref dailyAvailablewaterinsoilprofil, ref value, "DailyAvailablewaterinsoilprofil"); }
        }

        private bool dailyWaterdeficitinsoilprofil;
        public bool DailyWaterdeficitinsoilprofil
        {
            get { return dailyWaterdeficitinsoilprofil; }
            set { this.SetStruct(ref dailyWaterdeficitinsoilprofil, ref value, "DailyWaterdeficitinsoilprofil"); }
        }

        private bool dailyAvailablewaterinrootzone;
        public bool DailyAvailablewaterinrootzone
        {
            get { return dailyAvailablewaterinrootzone; }
            set { this.SetStruct(ref dailyAvailablewaterinrootzone, ref value, "DailyAvailablewaterinrootzone"); }
        }

        private bool dailyWaterdeficitinrootzone;
        public bool DailyWaterdeficitinrootzone
        {
            get { return dailyWaterdeficitinrootzone; }
            set { this.SetStruct(ref dailyWaterdeficitinrootzone, ref value, "DailyWaterdeficitinrootzone"); }
        }

        private bool dailyVirtualWReq;
        public bool DailyVirtualWReq
        {
            get { return dailyVirtualWReq; }
            set { this.SetStruct(ref dailyVirtualWReq, ref value, "DailyVirtualWReq"); }
        }

        private bool dailyTRSF;
        public bool DailyTRSF
        {
            get { return dailyTRSF; }
            set { this.SetStruct(ref dailyTRSF, ref value, "DailyTRSF"); }
        }

        private bool dailyETSF;
        public bool DailyETSF
        {
            get { return dailyETSF; }
            set { this.SetStruct(ref dailyETSF, ref value, "DailyETSF"); }
        }

        private bool dailySMSF;
        public bool DailySMSF
        {
            get { return dailySMSF; }
            set { this.SetStruct(ref dailySMSF, ref value, "DailySMSF"); }
        }

        private bool dailyFPAW;
        public bool DailyFPAW
        {
            get { return dailyFPAW; }
            set { this.SetStruct(ref dailyFPAW, ref value, "DailyFPAW"); }
        }

        private bool dailyDrymassdroughtfactor;
        public bool DailyDrymassdroughtfactor
        {
            get { return dailyDrymassdroughtfactor; }
            set { this.SetStruct(ref dailyDrymassdroughtfactor, ref value, "DailyDrymassdroughtfactor"); }
        }

        private bool dailyEargrowthdroughtfactor;
        public bool DailyEargrowthdroughtfactor
        {
            get { return dailyEargrowthdroughtfactor; }
            set { this.SetStruct(ref dailyEargrowthdroughtfactor, ref value, "DailyEargrowthdroughtfactor"); }
        }

        private bool dailyLeafexpansiondroughtfactor;
        public bool DailyLeafexpansiondroughtfactor
        {
            get { return dailyLeafexpansiondroughtfactor; }
            set { this.SetStruct(ref dailyLeafexpansiondroughtfactor, ref value, "DailyLeafexpansiondroughtfactor"); }
        }

        private bool dailySenescencedroughtfactor;
        public bool DailySenescencedroughtfactor
        {
            get { return dailySenescencedroughtfactor; }
            set { this.SetStruct(ref dailySenescencedroughtfactor, ref value, "DailySenescencedroughtfactor"); }
        }

        private bool dailyTranspirationdroughtfactor;
        public bool DailyTranspirationdroughtfactor
        {
            get { return dailyTranspirationdroughtfactor; }
            set { this.SetStruct(ref dailyTranspirationdroughtfactor, ref value, "DailyTranspirationdroughtfactor"); }
        }

        private bool dailyCumulativeNfertilisation;
        public bool DailyCumulativeNfertilisation
        {
            get { return dailyCumulativeNfertilisation; }
            set { this.SetStruct(ref dailyCumulativeNfertilisation, ref value, "DailyCumulativeNfertilisation"); }
        }

        private bool dailyCumulativeNleaching;
        public bool DailyCumulativeNleaching
        {
            get { return dailyCumulativeNleaching; }
            set { this.SetStruct(ref dailyCumulativeNleaching, ref value, "DailyCumulativeNleaching"); }
        }

        private bool dailyCumulativeNmineralisation;
        public bool DailyCumulativeNmineralisation
        {
            get { return dailyCumulativeNmineralisation; }
            set { this.SetStruct(ref dailyCumulativeNmineralisation, ref value, "DailyCumulativeNmineralisation"); }
        }

        private bool dailyCumulativedenitrification;
        public bool DailyCumulativedenitrification
        {
            get { return dailyCumulativedenitrification; }
            set { this.SetStruct(ref dailyCumulativedenitrification, ref value, "DailyCumulativedenitrification"); }
        }

        private bool dailyAvailablemineralNinsoilprofil;
        public bool DailyAvailablemineralNinsoilprofil
        {
            get { return dailyAvailablemineralNinsoilprofil; }
            set { this.SetStruct(ref dailyAvailablemineralNinsoilprofil, ref value, "DailyAvailablemineralNinsoilprofil"); }
        }

        private bool dailyTotalmineralNinsoilprofil;
        public bool DailyTotalmineralNinsoilprofil
        {
            get { return dailyTotalmineralNinsoilprofil; }
            set { this.SetStruct(ref dailyTotalmineralNinsoilprofil, ref value, "DailyTotalmineralNinsoilprofil"); }
        }

        private bool dailyAvailablemineralNinrootzone;
        public bool DailyAvailablemineralNinrootzone
        {
            get { return dailyAvailablemineralNinrootzone; }
            set { this.SetStruct(ref dailyAvailablemineralNinrootzone, ref value, "DailyAvailablemineralNinrootzone"); }
        }

        private bool dailyTotalmineralNinrootzone;
        public bool DailyTotalmineralNinrootzone
        {
            get { return dailyTotalmineralNinrootzone; }
            set { this.SetStruct(ref dailyTotalmineralNinrootzone, ref value, "DailyTotalmineralNinrootzone"); }
        }

        private bool dailyNitrogennutritionindex;
        public bool DailyNitrogennutritionindex
        {
            get { return dailyNitrogennutritionindex; }
            set { this.SetStruct(ref dailyNitrogennutritionindex, ref value, "DailyNitrogennutritionindex"); }
        }

        private bool dailyVernaProgress;
        public bool DailyVernaProgress
        {
            get { return dailyVernaProgress; }
            set { this.SetStruct(ref dailyVernaProgress, ref value, "DailyVernaProgress"); }
        }

        private bool dailyGrowthStage;
        public bool DailyGrowthStage
        {
            get { return dailyGrowthStage; }
            set { this.SetStruct(ref dailyGrowthStage, ref value, "DailyGrowthStage"); }
        }

        private bool dailyEmergedleafnumber;
        public bool DailyEmergedleafnumber
        {
            get { return dailyEmergedleafnumber; }
            set { this.SetStruct(ref dailyEmergedleafnumber, ref value, "DailyEmergedleafnumber"); }
        }

        private bool dailyShootnumber;
        public bool DailyShootnumber
        {
            get { return dailyShootnumber; }
            set { this.SetStruct(ref dailyShootnumber, ref value, "DailyShootnumber"); }
        }

        private bool dailyGreenareaindex;
        public bool DailyGreenareaindex
        {
            get { return dailyGreenareaindex; }
            set { this.SetStruct(ref dailyGreenareaindex, ref value, "DailyGreenareaindex"); }
        }

        private bool dailyLeafareaindex;
        public bool DailyLeafareaindex
        {
            get { return dailyLeafareaindex; }
            set { this.SetStruct(ref dailyLeafareaindex, ref value, "DailyLeafareaindex"); }
        }

        private bool dailyStemlength;
        public bool DailyStemlength
        {
            get { return dailyStemlength; }
            set { this.SetStruct(ref dailyStemlength, ref value, "DailyStemlength"); }
        }

        private bool dailyCropdrymatterSF;
        public bool DailyCropdrymatterSF
        {
            get { return dailyCropdrymatterSF; }
            set { this.SetStruct(ref dailyCropdrymatterSF, ref value, "DailyCropdrymatterSF"); }
        }

        private bool dailyCropdrymatter;
        public bool DailyCropdrymatter
        {
            get { return dailyCropdrymatter; }
            set { this.SetStruct(ref dailyCropdrymatter, ref value, "DailyCropdrymatter"); }
        }

        private bool dailyCropdeltadrymatter;
        public bool DailyCropdeltadrymatter
        {
            get { return dailyCropdeltadrymatter; }
            set { this.SetStruct(ref dailyCropdeltadrymatter, ref value, "DailyCropdeltadrymatter"); }
        }

        private bool dailyVirtualNReq;
        public bool DailyVirtualNReq
        {
            get { return dailyVirtualNReq; }
            set { this.SetStruct(ref dailyVirtualNReq, ref value, "DailyVirtualNReq"); }
        }

        private bool dailyCropnitrogen;
        public bool DailyCropnitrogen
        {
            get { return dailyCropnitrogen; }
            set { this.SetStruct(ref dailyCropnitrogen, ref value, "DailyCropnitrogen"); }
        }

        private bool dailyGraindrymatter;
        public bool DailyGraindrymatter
        {
            get { return dailyGraindrymatter; }
            set { this.SetStruct(ref dailyGraindrymatter, ref value, "DailyGraindrymatter"); }
        }

        private bool dailyGrainnitrogen;
        public bool DailyGrainnitrogen
        {
            get { return dailyGrainnitrogen; }
            set { this.SetStruct(ref dailyGrainnitrogen, ref value, "DailyGrainnitrogen"); }
        }

        private bool dailyLeafdrymatter;
        public bool DailyLeafdrymatter
        {
            get { return dailyLeafdrymatter; }
            set { this.SetStruct(ref dailyLeafdrymatter, ref value, "DailyLeafdrymatter"); }
        }

        private bool dailyLeafnitrogen;
        public bool DailyLeafnitrogen
        {
            get { return dailyLeafnitrogen; }
            set { this.SetStruct(ref dailyLeafnitrogen, ref value, "DailyLeafnitrogen"); }
        }

        private bool dailyLaminaedrymatter;
        public bool DailyLaminaedrymatter
        {
            get { return dailyLaminaedrymatter; }
            set { this.SetStruct(ref dailyLaminaedrymatter, ref value, "DailyLaminaedrymatter"); }
        }

        private bool dailyLaminaenitrogen;
        public bool DailyLaminaenitrogen
        {
            get { return dailyLaminaenitrogen; }
            set { this.SetStruct(ref dailyLaminaenitrogen, ref value, "DailyLaminaenitrogen"); }
        }

        private bool dailyStemdrymatter;
        public bool DailyStemdrymatter
        {
            get { return dailyStemdrymatter; }
            set { this.SetStruct(ref dailyStemdrymatter, ref value, "DailyStemdrymatter"); }
        }

        private bool dailyStemnitrogen;
        public bool DailyStemnitrogen
        {
            get { return dailyStemnitrogen; }
            set { this.SetStruct(ref dailyStemnitrogen, ref value, "DailyStemnitrogen"); }
        }

        private bool dailyExposedsheathdrymatter;
        public bool DailyExposedsheathdrymatter
        {
            get { return dailyExposedsheathdrymatter; }
            set { this.SetStruct(ref dailyExposedsheathdrymatter, ref value, "DailyExposedsheathdrymatter"); }
        }

        private bool dailyExposedsheathnitrogen;
        public bool DailyExposedsheathnitrogen
        {
            get { return dailyExposedsheathnitrogen; }
            set { this.SetStruct(ref dailyExposedsheathnitrogen, ref value, "DailyExposedsheathnitrogen"); }
        }

        private bool dailySpecificleafnitrogen;
        public bool DailySpecificleafnitrogen
        {
            get { return dailySpecificleafnitrogen; }
            set { this.SetStruct(ref dailySpecificleafnitrogen, ref value, "DailySpecificleafnitrogen"); }
        }

        private bool dailySpecificleafdrymass;
        public bool DailySpecificleafdrymass
        {
            get { return dailySpecificleafdrymass; }
            set { this.SetStruct(ref dailySpecificleafdrymass, ref value, "DailySpecificleafdrymass"); }
        }

        private bool dailySinglegraindrymass;
        public bool DailySinglegraindrymass
        {
            get { return dailySinglegraindrymass; }
            set { this.SetStruct(ref dailySinglegraindrymass, ref value, "DailySinglegraindrymass"); }
        }

        private bool dailyStarchpergrain;
        public bool DailyStarchpergrain
        {
            get { return dailyStarchpergrain; }
            set { this.SetStruct(ref dailyStarchpergrain, ref value, "DailyStarchpergrain"); }
        }

        private bool dailySinglegrainnitrogen;
        public bool DailySinglegrainnitrogen
        {
            get { return dailySinglegrainnitrogen; }
            set { this.SetStruct(ref dailySinglegrainnitrogen, ref value, "DailySinglegrainnitrogen"); }
        }

        private bool dailyAlbuminsGlobulinspergrain;
        public bool DailyAlbuminsGlobulinspergrain
        {
            get { return dailyAlbuminsGlobulinspergrain; }
            set { this.SetStruct(ref dailyAlbuminsGlobulinspergrain, ref value, "DailyAlbuminsGlobulinspergrain"); }
        }

        private bool dailyAmphiphilspergrain;
        public bool DailyAmphiphilspergrain
        {
            get { return dailyAmphiphilspergrain; }
            set { this.SetStruct(ref dailyAmphiphilspergrain, ref value, "DailyAmphiphilspergrain"); }
        }

        private bool dailyGliadinspergrain;
        public bool DailyGliadinspergrain
        {
            get { return dailyGliadinspergrain; }
            set { this.SetStruct(ref dailyGliadinspergrain, ref value, "DailyGliadinspergrain"); }
        }

        private bool dailyGluteninspergrain;
        public bool DailyGluteninspergrain
        {
            get { return dailyGluteninspergrain; }
            set { this.SetStruct(ref dailyGluteninspergrain, ref value, "DailyGluteninspergrain"); }
        }

        private bool dailyPostanthesisnitrogenuptake;
        public bool DailyPostanthesisnitrogenuptake
        {
            get { return dailyPostanthesisnitrogenuptake; }
            set { this.SetStruct(ref dailyPostanthesisnitrogenuptake, ref value, "DailyPostanthesisnitrogenuptake"); }
        }

        private bool dailyMinimumShootTemperature;
        public bool DailyMinimumShootTemperature
        {
            get { return dailyMinimumShootTemperature; }
            set { this.SetStruct(ref dailyMinimumShootTemperature, ref value, "DailyMinimumShootTemperature"); }
        }

        private bool dailyMeanShootTemperature;
        public bool DailyMeanShootTemperature
        {
            get { return dailyMeanShootTemperature; }
            set { this.SetStruct(ref dailyMeanShootTemperature, ref value, "DailyMeanShootTemperature"); }
        }

        private bool dailyMaximumShootTemperature;
        public bool DailyMaximumShootTemperature
        {
            get { return dailyMaximumShootTemperature; }
            set { this.SetStruct(ref dailyMaximumShootTemperature, ref value, "DailyMaximumShootTemperature"); }
        }

        private bool dailyTranspirationT;
        public bool DailyTranspirationT
        {
            get { return dailyTranspirationT; }
            set { this.SetStruct(ref dailyTranspirationT, ref value, "DailyTranspirationT"); }
        }

        private bool dailyEvaporationE;
        public bool DailyEvaporationE
        {
            get { return dailyEvaporationE; }
            set { this.SetStruct(ref dailyEvaporationE, ref value, "DailyEvaporationE"); }
        }

        private bool dailyLaminaesurfacearea;
        public bool DailyLaminaesurfacearea
        {
            get { return dailyLaminaesurfacearea; }
            set { this.SetStruct(ref dailyLaminaesurfacearea, ref value, "DailyLaminaesurfacearea"); }
        }

        private bool dailyLaminaeTotalN;
        public bool DailyLaminaeTotalN
        {
            get { return dailyLaminaeTotalN; }
            set { this.SetStruct(ref dailyLaminaeTotalN, ref value, "DailyLaminaesurfacearea"); }
        }
        ///</Behnam>


        ///<summary>
        ///Create a new RunOption.
        ///</summary>
        public RunOptionItem(string name)
            : base(name)
        {
            ///<Behnam>
            OutputPattern = "V15";

            // Summary Outputs
            IgnoreGrainMaturation = false;
            UseAirTemperatureForSenescence = false;
            IsCutOnGrainFillNotUse = false;
            // UseActualBase = true;
            UseObservedGrainNumber = false;
            UnlimitedWater = false;
            UnlimitedNitrogen = false;
            UnlimitedTemperature = false;
            MaxTempThreshold = 31;
            WCompensationLevel = 100;
            NCompensationLevel = 100;
            DoInteractions = false;
            InteractionsW = false;
            InteractionsN = false;
            InteractionsT = false;
            InteractionsWN = false;
            InteractionsWT = false;
            InteractionsNT = false;
            InteractionsWNT = true;
            Management = false;
            NonVarietalParameters = false;
            VarietalParameters = false;
            RunOptions = false;
            Site = false;
            Soil = false;
			SowingWindow = false;
            SowingYear = false;
            SowingDateOut = false;
            SowingDateOutDOY = false;
            EmergenceDate = false;
            EmergenceDateDOY = false;
            EmergenceDay = false;
            EndVernalizationDate = false;
            FirstNodeDetectable = false;
            BeginningOfStemExtension = false;
            TerminalSpikeletDate = false;
            FlagLeafLiguleJustVisibleDate = false;
            HeadingDate = false;
            AnthesisDate = false;
            AnthesisDateDOY = false;
            AnthesisDay = false;
            EndOfCellDivision = false;
            EndOfGrainFilling = false;
            MaturityDate = false;
            MaturityDateDOY = false;
            MaturityDay = false;
            MeanTempAnthesis = false;
            MeanTempAnth2Maturity = false;
            MeanTempMaturity = false;
            MeanMaxCanopyTempMaturity = false;
            MeanMaxAirTempMaturity = false;
            PhysTempAnthesis = false;
            PhysTempAnth2Maturity = false;
            PhysTempMaturity = false;
            FinalLeafNumberOption = false;
            LAIatAnthesis = false;
            GAIatAnthesis = false;
            CropDryMatAnthesis = false;
            CropDryMatMatururity = false;
            GrainDryMatMatururity = false;
            NNIatAnthesis = false;
            CropNatAnthesis = false;
            CropNatMaturity = false;
            GrainNatMaturity = false;
            PostAnthesisCropNUptake = false;
            SingleGrainDMatMaturity = false;
            SingleGrainNatMaturity = false;
            GrainProteinAtMaturity = false;
            GrainNumberOption = false;
            StarchAtMaturity = false;
            AlbuminsAtMaturity = false;
            AmphiphilsAtMaturity = false;
            GliadinsAtMaturity = false;
            GluteninsAtMaturity = false;
            GliadinsPAtMaturity = false;
            GluteinsPAtMaturity = false;
            GliadinsToGluteinsOption = false;
            HarvestIndex = false;
            NHarvestIndex = false;
            RainIrrigationAnthesis = false;
            RainIrrigationMaturity = false;
            RainIrrigationAnth2Maturity = false;
            CumPotETAnthesis = false;
            CumPotETMaturity = false;
            CumPotETAnth2Maturity = false;
            CumActETAnthesis = false;
            CumActETMaturity = false;
            CumActETAnth2Maturity = false;
            CumActTrAnthesis = false;
            CumActTrAnth2Maturity = false;
            CumActTrMaturity = false;
            CumEvaporation = false;
            CO2AtEmergence = false;
            CO2AtMaturity = false;
            CumNApplied = false;
            TotalAvaiSoilN = false;
            NLeaching = false;
            Drainage = false;
            CumNMineralization = false;
            CumNDenitrification = false;
            AvailableWaterinSoilatMaturity = false;
            AvailableMineralNinSoilAtMaturity = false;
            TotalSoilMineralNatMaturity = false;
            AvailableWaterinRootZoneMaturity = false;
            AvailableMineralNinRootZoneMaturity = false;
            TotalMineralNinRootZoneMaturity = false;
            NUseEfficiency = false;
            NUtilisationEfficiency = false;
            NUptakeEfficiency = false;
            WaterUseEfficiency = false;
            AccumPAREmerge2EndGrainFill = false;

            // Daily Outputs: Top Items;
            VersionDateItems = true;
            FilesItems = true;
            DateItems = true;
            CropItems = true;
            ManagementItems = true;
            ParameterItems = true;
            RunOptionItems = true;
            SiteItems = true;
            SoilItems = true;
            VarietyItems = true;

            // Daily Outputs;
            DailyGrowthDay = false;
            DailyDate = false;
            DailyDOY = false;
            DailyDayLength = false;
            DailyCO2Conc = false;
            DailyThermaltimeaftersowingAir = false;
            DailyThermaltimeaftersowingShoot = false;
            DailyPhysThermaltimeaftersowing = false;
            DailyAirtemperature = false;
            DailySoiltemperature = false;
            DailyCanopytemperature = false;
            DailyRootingdepth = false;
            DailyCumulativerainirrigation = false;
            DailyVPDAirOut = false;
            DailyVPDAirCanopyOut = false; 
            DailyCumulativePET = false;
            DailyCumulativeET = false;
            DailyCumulativePT = false;
            DailyCumulativeT = false;
            DailyCumulativewaterdrainage = false;
            DailyAvailablewaterinsoilprofil = false;
            DailyWaterdeficitinsoilprofil = false;
            DailyAvailablewaterinrootzone = false;
            DailyWaterdeficitinrootzone = false;
            DailyVirtualWReq = false;
            DailyTRSF = false;
            DailyETSF = false;
            DailySMSF = false;
            DailyFPAW = false;
            DailyDrymassdroughtfactor = false;
            DailyEargrowthdroughtfactor = false;
            DailyLeafexpansiondroughtfactor = false;
            DailySenescencedroughtfactor = false;
            DailyTranspirationdroughtfactor = false;
            DailyCumulativeNfertilisation = false;
            DailyCumulativeNleaching = false;
            DailyCumulativeNmineralisation = false;
            DailyCumulativedenitrification = false;
            DailyAvailablemineralNinsoilprofil = false;
            DailyTotalmineralNinsoilprofil = false;
            DailyAvailablemineralNinrootzone = false;
            DailyTotalmineralNinrootzone = false;
            DailyNitrogennutritionindex = false;
            DailyVernaProgress = false;
            DailyGrowthStage = false;
            DailyEmergedleafnumber = false;
            DailyShootnumber = false;
            DailyGreenareaindex = false;
            DailyLeafareaindex = false;
            DailyStemlength = false;
            DailyCropdrymatterSF = false;
            DailyCropdrymatter = false;
            DailyCropdeltadrymatter = false;
            DailyVirtualNReq = false;
            DailyCropnitrogen = false;
            DailyGraindrymatter = false;
            DailyGrainnitrogen = false;
            DailyLeafdrymatter = false;
            DailyLeafnitrogen = false;
            DailyLaminaedrymatter = false;
            DailyLaminaenitrogen = false;
            DailyStemdrymatter = false;
            DailyStemnitrogen = false;
            DailyExposedsheathdrymatter = false;
            DailyExposedsheathnitrogen = false;
            DailySpecificleafnitrogen = false;
            DailySpecificleafdrymass = false;
            DailySinglegraindrymass = false;
            DailyStarchpergrain = false;
            DailySinglegrainnitrogen = false;
            DailyAlbuminsGlobulinspergrain = false;
            DailyAmphiphilspergrain = false;
            DailyGliadinspergrain = false;
            DailyGluteninspergrain = false;
            DailyPostanthesisnitrogenuptake = false;
            DailyMinimumShootTemperature = false;
            DailyMeanShootTemperature = false;
            DailyMaximumShootTemperature = false;
            DailyTranspirationT = false;
            DailyEvaporationE = false;
            DailyLaminaesurfacearea = false;
            DailyLaminaeTotalN = false;
            ///</Behnam>
        }

        public RunOptionItem()
            : this("")
        {
        }

        public override void UpdatePath(string oldAbsolute, string newAbsolute)
        {
        }

        public override void CheckWarnings()
        {
        }

        ///<Behnam (2016.01.12)>
        ///<Comment>Now, it is possible to define the outputs in RunOptionItem class as listDaily and listSummary
        ///and use them as sorted lists to create check boxes in the Run Option form and also to rearrange the 
        ///daily and summary outputs. To rearrange, we only need to change listDaily and listSummary.
        ///The Namr referes to the name of the properties of RunOptionItem class.<Comment> 
        
        private static Dictionary<string, string> listSummary = new Dictionary<string, string>()
            {
                {"Management", "Management"},
                {"NonVarietalParameters", "Non-varietal parameters"},
                {"VarietalParameters", "Varietal parameters"},
                {"RunOptions", "Run options"},
                {"Site", "Site"},
                {"Soil", "Soil"},
                {"SowingYear", "Sowing year"},
                {"SowingWindow", "Sowing widnow"},
                {"SowingDateOut", "Sowing date (ZC00)"},
                {"SowingDateOutDOY", "Sowing date DOY (YYYYJJJ) (ZC00)"},
                {"EmergenceDate", "Emergence date (ZC10)"},
                {"EmergenceDateDOY", "Emergence date DOY (YYYYJJJ) (ZC10)"},
                {"EmergenceDay", "Emergence day after sowing (ZC10)"},
                {"EndVernalizationDate", "End of vernalization date"},
                {"FirstNodeDetectable", "Fist node detectable (ZC31)"},
                {"BeginningOfStemExtension", "Beginning of stem extension"},
                {"TerminalSpikeletDate", "Terminal Spikelet date"},
                {"FlagLeafLiguleJustVisibleDate", "Flag leaf ligule just visible (ZC39)"},
                {"HeadingDate", "Heading date"},
                {"AnthesisDate", "Anthesis date (ZC65)"},
                {"AnthesisDateDOY", "Anthesis date DOY (YYYYJJJ) (ZC65)"},
                {"AnthesisDay", "Anthesis day after sowing (ZC65)"},
                {"EndOfCellDivision", "End of cell division (ZC75)"},
                {"EndOfGrainFilling", "End of grain filling (ZC91)"},
                {"MaturityDate", "Maturity date (ZC92)"},
                {"MaturityDateDOY", "Maturity date DOY (YYYYJJJ) (ZC92)"},
                {"MaturityDay", "Maturity day after sowing (ZC92)"},
                {"FinalLeafNumberOption", "Final leaf number"},
                {"LAIatAnthesis", "LAI at anthesis (m²/m²)"},
                {"GAIatAnthesis", "GAI at anthesis (m²/m²)"},
                {"AccumPAREmerge2EndGrainFill", "Cumulative PAR between emergence and end of grain filling (MJ/m²)"},
                {"CropDryMatAnthesis", "Crop DM at anthesis (kgDM/ha)"},
                {"CropDryMatMatururity", "Crop DM at maturity (kgDM/ha)"},
                {"GrainDryMatMatururity", "Grain DM at maturity (kgDM/ha)"},
                {"NNIatAnthesis", "Nitrogen nutrition index at anthesis (dimensionless)"},
                {"CropNatAnthesis", "Crop N at anthesis (kgN/ha)"},
                {"CropNatMaturity", "Crop N at maturity (kgN/ha)"},
                {"GrainNatMaturity", "Grain N at maturity (kgN/ha)"},
                {"PostAnthesisCropNUptake", "Post-anthesis crop N uptake (kgN/ha)"},
                {"SingleGrainDMatMaturity", "Single grain DM at maturity (mgDM/grain)"},
                {"SingleGrainNatMaturity", "Single grain N at maturity (mgN/grain)"},
                {"GrainProteinAtMaturity", "Grain protein concentration at maturity (% of grain DM)"},
                {"GrainNumberOption", "Grain number (grain/m²)"},
                {"StarchAtMaturity", "% Starch at maturity (% of total grain DM)"},
                {"AlbuminsAtMaturity", "Albumins-globulins at maturity (mgN/grain)"},
                {"AmphiphilsAtMaturity", "Amphiphils at maturity (mgN/grain)"},
                {"GliadinsAtMaturity", "Gliadins at maturity (mgN/grain)"},
                {"GluteninsAtMaturity", "Glutenins at maturity (mgN/grain)"},
                {"GliadinsPAtMaturity", "% Gliadins at maturity (% of total grain N)"},
                {"GluteinsPAtMaturity", "% Gluteins at maturity (% of total grain N)"},
                {"GliadinsToGluteinsOption", "Gliadins-to-gluteins ratio (dimensionless)"},
                {"HarvestIndex", "DM harvest index (dimensionless)"},
                {"NHarvestIndex", "N harvest index (dimensionless)"},
                {"CumNApplied", "Cumulative inorganic N fertilisation (kgN/ha)"},
                {"TotalAvaiSoilN", "Total available soil N (kgN/ha)"},
                {"NLeaching", "N leaching (kgN/ha)"},
                {"Drainage", "Water drainage (mm)"},
                {"CumNMineralization", "Cumulative N mineralisation in soil profil (kgN/ha)"},
                {"CumNDenitrification", "Cumulative N denitrification in soil profil (kgN/ha)"},
                {"FPAWatAnthesis", "FPAW at anthesis (dimensionless)"},
                {"FPAWatMaturity", "FPAW at maturity (dimensionless)"},
                {"AvailableWaterinSoilatMaturity", "Available water at maturity in soil profil (mm)"},
                {"AvailableMineralNinSoilAtMaturity", "Available mineral soil N at maturity in soil profil (kgN/ha)"},
                {"TotalSoilMineralNatMaturity", "Total mineral soil N at maturity in soil profil (kgN/ha)"},
                {"AvailableWaterinRootZoneMaturity", "Available water in root zone at maturity (mm)"},
                {"AvailableMineralNinRootZoneMaturity", "Available mineral N in root zone at maturity (kgN/ha)"},
                {"TotalMineralNinRootZoneMaturity", "Total mineral N in root zone at maturity (kgN/ha)"},
                {"NUseEfficiency", "N use efficiency (kgDM/kgN)"},
                {"NUtilisationEfficiency", "N utilisation efficiency (kgDM/kgN)"},
                {"NUptakeEfficiency", "N uptake efficiency (kgN/kgN)"},
                {"WaterUseEfficiency", "Water use efficiency (kgDM/ha/mm)"},
                {"RainIrrigationAnthesis", "Incoming water up to anthesis (mm)"},
                {"RainIrrigationAnth2Maturity", "Incoming water between anthesis and maturity (mm)"},
                {"RainIrrigationMaturity", "Incoming water up to maturity (mm)"},
                {"MeanTempAnthesis", "Average air temperature up to anthesis (°C)"},
                {"MeanTempAnth2Maturity", "Average air temperature between anthesis and maturity (°C)"},
                {"MeanTempMaturity", "Average air temperature up to maturity (°C)"},
                {"MeanMaxCanopyTempMaturity", "Average max canopy temperature up to maturity (°C)"},
                {"MeanMaxAirTempMaturity", "Average max air temperature up to maturity (°C)"},
                {"PhysTempAnthesis", "Days at 20 °C up to anthesis (days)"},
                {"PhysTempAnth2Maturity", "Days at 20 °C between anthesis and maturity (days)"},
                {"PhysTempMaturity", "Days at 20 °C up to maturity (days)"},
                {"CumPotETAnthesis", "Cumulative PET up to anthesis (mm)"},
                {"CumPotETAnth2Maturity", "Cumulative PET between anthesis and maturity (mm)"},
                {"CumPotETMaturity", "Cumulative PET up to maturity (mm)"},
                {"CumActETAnthesis", "Cumulative ET up to anthesis (mm)"},
                {"CumActETAnth2Maturity", "Cumulative ET between anthesis and maturity (mm)"},
                {"CumActETMaturity", "Cumulative ET up to maturity (mm)"},
                {"CumActTrAnthesis", "Cumulative TR up to anthesis (mm)"},
                {"CumActTrAnth2Maturity", "Cumulative TR between anthesis and maturity (mm)"},
                {"CumActTrMaturity", "Cumulative TR up to maturity (mm)"},
                {"CumEvaporation", "Cumulative evaporation (mm)"},
                {"CO2AtEmergence", "CO2 concentration at emergence (ppm)"},
                {"CO2AtMaturity", "CO2 concentration at maturity (ppm)"},
            };

        public static Dictionary<string, string> ListSummary
        {
            get { return listSummary; }
        }

        private static Dictionary<string, string> listDaily = new Dictionary<string, string>()
            {
                {"VersionDateItems", "Top version/date items"},
                {"FilesItems", "Top file items"},
                {"DateItems", "Top date summary items"},
                {"CropItems", "Top crop summary items"},
                {"ManagementItems", "Top management items"},
                {"ParameterItems", "Top non-varietal items"},
                {"RunOptionItems", "Top run option items"},
                {"SiteItems", "Top site items"},
                {"SoilItems", "Top soil items"},
                {"VarietyItems", "Top varietal items"},
                {"DailyGrowthDay", "Growing day number"},
                {"DailyDate", "Date (yyyy-mm-dd)"},
                {"DailyDOY", "Day of year"},
                {"DailyGrowthStage", "Growth stage (Zadoks stage)"},
                {"DailyVernaProgress", "Vernalisation progress"},
                {"DailyDayLength", "Day length (hours)"},
                {"DailyCO2Conc", "Daily CO2 concentratiion (ppm)"},
                {"DailyThermaltimeaftersowingAir", "Thermal time after sowing (air) (°Cd)"},
                {"DailyThermaltimeaftersowingShoot", "Thermal time after sowing (shoot) (°Cd)"},
                {"DailyPhysThermaltimeaftersowing", "Days at 20 °C from sowing (days)"},
                {"DailyAirtemperature", "Mean air temperature (°C)"},
                {"DailySoiltemperature", "Mean soil temperature (°C)"},
                {"DailyCanopytemperature", "Mean canopy temperature (°C)"},
                {"DailyMinimumShootTemperature", "Minimum shoot temperature (°C)"},
                {"DailyMeanShootTemperature", "Mean shoot temperature (°C)"},
                {"DailyMaximumShootTemperature", "Maximum shoot temperature (°C)"},
                {"DailyRootingdepth", "Rooting depth (m)"},
                {"DailyCumulativerainirrigation", "Cumulative rain + irrigation (mm)"},
                {"DailyVPDAirOut", "Daily VPD air (hPa)"},
                {"DailyVPDAirCanopyOut", "Daily VPD air-canopy (hPa)"},
                {"DailyCumulativePET", "Cumulative PET (mm)"},
                {"DailyCumulativeET", "Cumulative ET (mm)"},
                {"DailyCumulativePT", "Cumulative PT (mm)"},
                {"DailyCumulativeT", "Cumulative T (mm)"},
                {"DailyCumulativewaterdrainage", "Cumulative water drainage (mm)"},
                {"DailyAvailablewaterinsoilprofil", "Available water in soil profil (mm)"},
                {"DailyWaterdeficitinsoilprofil", "Water deficit in soil profil (mm)"},
                {"DailyAvailablewaterinrootzone", "Available water in root zone (mm)"},
                {"DailyWaterdeficitinrootzone", "Water deficit in root zone (mm)"},
                {"DailyVirtualWReq", "Water required to preserve Water balance (mm)"},
                {"DailyTRSF", "Transpiration SF (dimensionless)"},
                {"DailyETSF", "Evapotranspiration SF (dimensionless)"},
                {"DailySMSF", "Soil moisture SF (dimensionless)"},
                {"DailyFPAW", "FPAW (dimensionless)"},
                {"DailyDrymassdroughtfactor", "Dry mass drought factor (dimensionless)"},
                {"DailyEargrowthdroughtfactor", "Ear growth drought factor (dimensionless)"},
                {"DailyLeafexpansiondroughtfactor", "Leaf expansion drought factor (dimensionless)"},
                {"DailySenescencedroughtfactor", "Senescence drought factor (dimensionless)"},
                {"DailyTranspirationdroughtfactor", "Transpiration drought factor (dimensionless)"},
                {"DailyCumulativeNfertilisation", "Cumulative N fertilisation (kgN/ha)"},
                {"DailyCumulativeNleaching", "Cumulative N leaching (kgN/ha)"},
                {"DailyCumulativeNmineralisation", "Cumulative N mineralisation (kgN/ha)"},
                {"DailyCumulativedenitrification", "Cumulative denitrification (kgN/ha)"},
                {"DailyAvailablemineralNinsoilprofil", "Available mineral N in soil profil (kgN/ha)"},
                {"DailyTotalmineralNinsoilprofil", "Total mineral N in soil profil (kgN/ha)"},
                {"DailyAvailablemineralNinrootzone", "Available mineral N in root zone (kgN/ha)"},
                {"DailyTotalmineralNinrootzone", "Total mineral N in root zone (kgN/ha)"},
                {"DailyNitrogennutritionindex", "Nitrogen nutrition index (dimensionless)"},
                {"DailyEmergedleafnumber", "Emerged leaf number (leaf/mainstem)"},
                {"DailyShootnumber", "Shoot number (shoot/m²)"},
                {"DailyGreenareaindex", "Green area index (m²/m²)"},
                {"DailyLeafareaindex", "Leaf area index (m²/m²)"},
                {"DailyStemlength", "Stem length (m)"},
                {"DailyCropdrymatter", "Crop dry matter (kgDM/ha)"},
                {"DailyCropdeltadrymatter", "Crop delta dry matter (kgDM/ha)"},
                {"DailyCropdrymatterSF", "Crop dry matter SF (dimensionless)"},
                {"DailyVirtualNReq", "N required to preserve N balance (kgN/ha)"},
                {"DailyCropnitrogen", "Crop nitrogen (kgN/ha)"},
                {"DailyGraindrymatter", "Grain dry matter (kgDM/ha)"},
                {"DailyGrainnitrogen", "Grain nitrogen (kgN/ha)"},
                {"DailyLeafdrymatter", "Leaf dry matter (kgDM/ha)"},
                {"DailyLeafnitrogen", "Leaf nitrogen (kgN/ha)"},
                {"DailyLaminaedrymatter", "Laminae dry matter (kgDM/ha)"},
                {"DailyLaminaenitrogen", "Laminae nitrogen (kgN/ha)"},
                {"DailyStemdrymatter", "Stem dry matter (kgDM/ha)"},
                {"DailyStemnitrogen", "Stem nitrogen (kgN/ha)"},
                {"DailyExposedsheathdrymatter", "Exposed sheath dry matter (kgDM/ha)"},
                {"DailyExposedsheathnitrogen", "Exposed sheath nitrogen (kgN/ha)"},
                {"DailySpecificleafnitrogen", "Specific leaf nitrogen (gN/m²)"},
                {"DailySpecificleafdrymass", "Specific leaf dry mass (gDM/m²)"},
                {"DailySinglegraindrymass", "Single grain dry mass (mgDM/grain)"},
                {"DailyStarchpergrain", "Starch per grain (mg/grain)"},
                {"DailySinglegrainnitrogen", "Single grain nitrogen (mgN/grain)"},
                {"DailyAlbuminsGlobulinspergrain", "Albumins-Globulins per grain (mgN/grain)"},
                {"DailyAmphiphilspergrain", "Amphiphils per grain (mgN/grain)"},
                {"DailyGliadinspergrain", "Gliadins per grain (mgN/grain)"},
                {"DailyGluteninspergrain", "Glutenins per grain (mgN/grain)"},
                {"DailyPostanthesisnitrogenuptake", "Post-anthesis nitrogen uptake (kgN/ha)"},
                {"DailyTranspirationT", "Transpiration T (mm)"},
                {"DailyEvaporationE", "Evaporation E (mm)"},
                {"DailyLaminaesurfacearea", "Laminae surface area per layer (m²/m²)"},
                {"DailyLaminaeTotalN", "Laminae Total N per layer (kgN/ha)"}

            };

        public static Dictionary<string, string> ListDaily
        {
            get { return listDaily; }
        }
    }
}