using SiriusModel.Model.Base;
using SiriusModel.Model.Observation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusModel.Model.CropModel.Phytomers
{
    class SwitchLAI :UniverseLink
    {

        private SiriusQualityMaizeLAI.MaizeLAIState maizeLaiState_;
        private SiriusQualityMaizeLAI.Strategies.MaizeLAI maizeLAI_;

        private SiriusQualityWheatLAI.WheatLAIState wheatLaistate_;
        private SiriusQualityWheatLAI.Strategies.WheatLAI wheatLAI_;

        #region outputs

        public List<LeafState> getLeafStateList()
        {
            if (!SwitchMaize) { return wheatLaistate_.leafStateList; }
            else { return maizeLaiState_.leafStateList; }

        }
        public List<int> getIsPrematurelyDyingList()
        {
            if (!SwitchMaize) { return wheatLaistate_.isPrematurelyDying; }
            else { return maizeLaiState_.isPrematurelyDying; }

        }
        public double getIncDeltaAreaLimitSF()
        {
            if (!SwitchMaize) { return wheatLaistate_.incDeltaAreaLimitSF; }
            else { return maizeLaiState_.incDeltaAreaLimitSF; }

        }
        public double getPotentialIncDeltaArea()
        {
            if (!SwitchMaize) { return wheatLaistate_.potentialIncDeltaArea; }
            else { return maizeLaiState_.potentialIncDeltaArea; }

        }

        public double DEF { get {
            if (!SwitchMaize) 
            { 
            return wheatLaistate_.DEF;
            }
            else {return maizeLaiState_.DEF;}
        } }

        public double DSF { get {

            if (!SwitchMaize)
            {
                return wheatLaistate_.DSF;
            }
            else
            {
                return maizeLaiState_.DSF;
            }
        }}

        public double LER
        {
            get
            {

                if (!SwitchMaize)
                {
                    return 0;
                }
                else
                {
                    return maizeLaiState_.LER;
                }
            }
        }

        public double getWaterLimitedPotDeltaAI(int i)
        {
            if (!SwitchMaize)
            {
                if (i < wheatLaistate_.WaterLimitedPotDeltaAIList.Count)
                {
                    return wheatLaistate_.WaterLimitedPotDeltaAIList[i];
                }
                else
                {
                    return -1;
                }

            }
            else
            {
                if (i < maizeLaiState_.WaterLimitedPotDeltaAIList.Count)
                {
                    return maizeLaiState_.WaterLimitedPotDeltaAIList[i];
                }
                else
                {
                    return -1;
                }
            }
        }

        #endregion

        public SwitchLAI(Universe universe)
            : base(universe)
        {
            if (!SwitchMaize)
            {
                wheatLAI_ = new SiriusQualityWheatLAI.Strategies.WheatLAI();
                wheatLaistate_ = new SiriusQualityWheatLAI.WheatLAIState();
                loadParametersWheat();
            }
            else
            {
                maizeLAI_ = new SiriusQualityMaizeLAI.Strategies.MaizeLAI();
                maizeLaiState_ = new SiriusQualityMaizeLAI.MaizeLAIState();
                loadParametersMaize();
            }

        }

        public SwitchLAI(Universe universe, SwitchLAI toCopy, bool copyAll)
            : base(universe)
        {
            if (!SwitchMaize)
            {
                wheatLaistate_ = (toCopy.wheatLaistate_ != null) ? new SiriusQualityWheatLAI.WheatLAIState(toCopy.wheatLaistate_, copyAll) : null;

                wheatLaistate_.MaximumPotentialLaminaeAI = (toCopy.wheatLaistate_.MaximumPotentialLaminaeAI != null) ? new System.Collections.Generic.List<double>(toCopy.wheatLaistate_.MaximumPotentialLaminaeAI) : null;
                wheatLaistate_.MaximumPotentialSheathAI = (toCopy.wheatLaistate_.MaximumPotentialSheathAI != null) ? new System.Collections.Generic.List<double>(toCopy.wheatLaistate_.MaximumPotentialSheathAI) : null;
                wheatLaistate_.WaterLimitedPotDeltaAIList = (toCopy.wheatLaistate_.WaterLimitedPotDeltaAIList != null) ? new System.Collections.Generic.List<double>(toCopy.wheatLaistate_.WaterLimitedPotDeltaAIList) : null;
                wheatLaistate_.TT = (toCopy.wheatLaistate_.TT != null) ? new System.Collections.Generic.List<double>(toCopy.wheatLaistate_.TT) : null;
                wheatLaistate_.TTgroSheathList = (toCopy.wheatLaistate_.TTgroSheathList != null) ? new System.Collections.Generic.List<double>(toCopy.wheatLaistate_.TTgroSheathList) : null;
                wheatLaistate_.leafStateList = (toCopy.wheatLaistate_.leafStateList != null) ? new System.Collections.Generic.List<LeafState>(toCopy.wheatLaistate_.leafStateList) : null;
                wheatLaistate_.isPrematurelyDying = (toCopy.wheatLaistate_.isPrematurelyDying != null) ? new System.Collections.Generic.List<int>(toCopy.wheatLaistate_.isPrematurelyDying) : null;

                if (copyAll)
                {
                    wheatLAI_ = (toCopy.wheatLAI_ != null) ? new SiriusQualityWheatLAI.Strategies.WheatLAI(toCopy.wheatLAI_) : null;

                }
            }
            else
            {
                maizeLaiState_ = (toCopy.maizeLaiState_ != null) ? new SiriusQualityMaizeLAI.MaizeLAIState(toCopy.maizeLaiState_, copyAll) : null;

                if (copyAll)
                {
                    maizeLAI_ = (toCopy.maizeLAI_ != null) ? new SiriusQualityMaizeLAI.Strategies.MaizeLAI(toCopy.maizeLAI_) : null;
                }
            }
        }


        public void Estimate(bool newLeafHasAppeared, int roundedFinalNumber, double finalLeafNumber, double leafNumber,
            bool newLeafIsSmallPhytomer, int newLeafLastPhytoNum, int newLeafindex,
            double FPAW, bool isPotentialLAI, double cumulTTShoot, double deltaTTShoot, double deltaTTSenescence, List<LeafLayer> All, double VPDairCanopy, double LER,
             List<double> tilleringProfile, List<double> leafTillerNumberArray,  double cumulTTPhenoMaize, double deltaTTPhenoMaize, double[] deltaTTCanopyHourly, double[] VPDeq)
        {
            if (!SwitchMaize)
            {

                wheatLaistate_.newLeafHasAppeared = newLeafHasAppeared ? 1 : 0;
                wheatLaistate_.roundedFinalLeafNumber = roundedFinalNumber;
                wheatLaistate_.finalLeafNumber = finalLeafNumber;
                wheatLaistate_.leafNumber = leafNumber;
                wheatLaistate_.FPAW = FPAW;
                wheatLaistate_.isPotentialLAI = isPotentialLAI ?1:0;
                wheatLaistate_.cumulTTShoot = cumulTTShoot;
                wheatLaistate_.deltaTTShoot = deltaTTShoot;
                wheatLaistate_.deltaTTSenescence = deltaTTSenescence;
                wheatLaistate_.VPDairCanopy = VPDairCanopy;
                wheatLaistate_.tilleringProfile = tilleringProfile;
                wheatLaistate_.leafTillerNumberArray = leafTillerNumberArray;
                wheatLaistate_.isSmallPhytomer = newLeafIsSmallPhytomer ? 1:0;
                wheatLaistate_.phytonum = newLeafLastPhytoNum;
                wheatLaistate_.index = newLeafindex;

                
                wheatLAI_.Estimate(wheatLaistate_ ,All);

            }

            else
            {
                maizeLaiState_.newLeafHasAppeared = newLeafHasAppeared ? 1 : 0;
                maizeLaiState_.roundedFinalLeafNumber = roundedFinalNumber;
                maizeLaiState_.finalLeafNumber = finalLeafNumber;
                maizeLaiState_.leafNumber = leafNumber;
                maizeLaiState_.FPAW = FPAW;
                maizeLaiState_.isPotentialLAI = isPotentialLAI ? 1 : 0;
                maizeLaiState_.cumulTTPHenoMaize = cumulTTPhenoMaize;
                maizeLaiState_.deltaTTPhenoMaize = deltaTTPhenoMaize;
                maizeLaiState_.VPDairCanopy = VPDairCanopy;
                maizeLaiState_.tilleringProfile = tilleringProfile;
                maizeLaiState_.leafTillerNumberArray = leafTillerNumberArray;
                maizeLaiState_.isSmallPhytomer = newLeafIsSmallPhytomer ? 1 : 0;
                maizeLaiState_.phytonum = newLeafLastPhytoNum;
                maizeLaiState_.index = newLeafindex;
                maizeLaiState_.deltaTTCanopyHourly = deltaTTCanopyHourly;
                maizeLaiState_.VPDeq = VPDeq;


                maizeLAI_.Estimate(maizeLaiState_, All);


            }
        }

        private void loadParametersWheat()
        {
            wheatLAI_.AreaPL =AreaPL;
            wheatLAI_.AreaSL = AreaSL;
            wheatLAI_.AreaSS = AreaSS;
            wheatLAI_.LowerFPAWexp = LowerFPAWexp;
            wheatLAI_.LowerFPAWsen = LowerFPAWsen;
            wheatLAI_.LowerVPD = LowerVPD;
            wheatLAI_.MaxDSF = MaxDSF;
            wheatLAI_.NLL = NLL;
            wheatLAI_.PexpL = PexpL;
            wheatLAI_.RatioFLPL = RatioFLPL;
            wheatLAI_.SLNmin = SLNmin;
            wheatLAI_.UpperFPAWexp = UpperFPAWexp;
            wheatLAI_.UpperFPAWsen = UpperFPAWsen;
            wheatLAI_.UpperVPD = UpperVPD;

        }

        private void loadParametersMaize()
        {
            maizeLAI_.AreaPL = AreaPL;
            maizeLAI_.AreaSL = AreaSL;
            maizeLAI_.AreaSS = AreaSS;
            maizeLAI_.LowerFPAWexp = LowerFPAWexp;
            maizeLAI_.LowerFPAWsen = LowerFPAWsen;
            maizeLAI_.LowerVPD = LowerVPD;
            maizeLAI_.MaxDSF = MaxDSF;
            maizeLAI_.NLL = NLL;
            maizeLAI_.RatioFLPL = RatioFLPL;
            maizeLAI_.UpperFPAWexp = UpperFPAWexp;
            maizeLAI_.UpperFPAWsen = UpperFPAWsen;
            maizeLAI_.UpperVPD = UpperVPD;
            maizeLAI_.LERa = LERa;
            maizeLAI_.LERb = LERb;
            maizeLAI_.LERc = LERc;
            maizeLAI_.Nfinal = Nfinal;
            maizeLAI_.plantDensity = plantDensity;
        }

    }
}
