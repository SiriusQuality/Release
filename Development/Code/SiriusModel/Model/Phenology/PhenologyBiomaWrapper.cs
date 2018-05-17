using SiriusQualityPhenology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusModel.Model.Phenology
{
    class PhenologyBiomaWrapper: UniverseLink
    {

        #region output

        public Calendar previousCalendar { get { return previousphenologyState.Calendar; }}
        public Calendar calendar { get { return phenologyState.Calendar; } }
        public double BeforeUpdateLeafNumber { get { return previousphenologyState.LeafNumber; } }
        public double LeafNumber 
        { get { return phenologyState.LeafNumber; } }
       // { get { return mtg_.GetLeafNumber(); } }
        public double Ntip { get { return phenologyState.Ntip; } }
        public double FinalLeafNumber { get { return phenologyState.FinalLeafNumber; } }
        public double getPhaseValue() { return phenologyState.phase_.phaseValue; }
        public int RoundedFinalLeafNumber { get { return (int)(phenologyState.FinalLeafNumber+0.5); } }
        public GrowthStage ZadokStage { get { return phenologyState.currentZadokStage; } }
        public double Phyllochron { get { return phenologyState.Phyllochron; } }
        public double VernaProg { get { return phenologyState.Vernaprog; } }
        public List<double> tilleringProfile { get { return phenologyState.tilleringProfile; } }
        public List<double> leafTillerNumberArray { get { return phenologyState.leafTillerNumberArray; } }
        public int TillerNumber { get { return phenologyState.TillerNumber; } }
        public double CanopyShootNumber { get { return phenologyState.CanopyShootNumber; } }
        public double cumulTTPhenoMaizeAtEmergence { get { return phenologyState.cumulTTPhenoMaizeAtEmergence; } }

       /* public string TagPhenoWarnOut
        {
            get { return phenologyState.TagPhenoWarnOut;}
            set { phenologyState.TagPhenoWarnOut=value; }
        }*/	

        #endregion

        public PhenologyBiomaWrapper(Universe universe) : base(universe)
        {
            phenologyComponent = new SiriusQualityPhenology.Strategies.Phenology();
            phenologyState = new SiriusQualityPhenology.PhenologyState();
            previousphenologyState = new SiriusQualityPhenology.PhenologyState();
            loadParameters();
            if (SwitchMaize) { phenologyState.FinalLeafNumber = Nfinal; }
        }

        public PhenologyBiomaWrapper(Universe universe, PhenologyBiomaWrapper toCopy, bool copyAll)
            : base(universe)
        {
            phenologyState = (toCopy.phenologyState != null) ? new SiriusQualityPhenology.PhenologyState(toCopy.phenologyState, copyAll) : null;
            previousphenologyState = (toCopy.previousphenologyState != null) ? new SiriusQualityPhenology.PhenologyState(toCopy.previousphenologyState, copyAll) : null;
            if (copyAll)
            {
                phenologyComponent = (toCopy.phenologyComponent != null) ? new SiriusQualityPhenology.Strategies.Phenology(toCopy.phenologyComponent) : null;
            }
        }

        public void Init(double[] cumulTTatSowing, DateTime sowingDate)
        { 
            List<double> cumulTTatSowingList=new List<double>();
            for (int i = 0; i < cumulTTatSowing.Length; i++) cumulTTatSowingList.Add(cumulTTatSowing[i]);

            phenologyComponent.Init(cumulTTatSowingList, sowingDate, phenologyState);
            loadParameters();
        }

        public void EstimatePhenology(double dayLength, double deltaTTShoot, double[] cumulTT, double grainCumulTT, double GAI, bool isLatestLeafInternodeLengthPotPositive)
        {

            LoadPreviousStates();

            phenologyState.DayLength = dayLength;
            phenologyState.DeltaTT = deltaTTShoot;
            phenologyState.cumulTT = new List<double>();
            for (int i = 0; i < cumulTT.Length; i++) phenologyState.cumulTT.Add(cumulTT[i]);
            phenologyState.GrainCumulTT = grainCumulTT;
            phenologyState.GAI = GAI;
            phenologyState.IsLatestLeafInternodeLengthPotPositive = (isLatestLeafInternodeLengthPotPositive) ? 1 : 0;
            phenologyState.currentdate = CurrentDate;

            phenologyComponent.Estimate(phenologyState, previousphenologyState,null);

           // mtg_.SetLeafNumber(phenologyState.LeafNumber);
            
            
        }


        private void LoadPreviousStates()
        {



            previousphenologyState.Calendar = phenologyState.Calendar;

            previousphenologyState.LeafNumber = phenologyState.LeafNumber;

            previousphenologyState.phase_ = phenologyState.phase_;

            previousphenologyState.Vernaprog = phenologyState.Vernaprog;

            previousphenologyState.MinFinalNumber = phenologyState.MinFinalNumber;

            previousphenologyState.tilleringProfile = new List<double>();
            for (int i = 0; i < phenologyState.tilleringProfile.Count; i++) previousphenologyState.tilleringProfile.Add(phenologyState.tilleringProfile[i]);

            previousphenologyState.leafTillerNumberArray = new List<double>();
            for (int i = 0; i < phenologyState.leafTillerNumberArray.Count; i++) previousphenologyState.leafTillerNumberArray.Add(phenologyState.leafTillerNumberArray[i]);

            previousphenologyState.CanopyShootNumber = phenologyState.CanopyShootNumber;
        


        }


        private void loadParameters()
        {
            phenologyComponent.AMNFLNO = AMNLFNO;
            phenologyComponent.AMXLFNO = AMXLFNO;
            phenologyComponent.Dcd = Dcd;
            phenologyComponent.Degfm = Degfm;
            phenologyComponent.Der = Der;
            phenologyComponent.Dgf = Dgf;
            phenologyComponent.Dse = Dse;
            phenologyComponent.FixPhyll = FixPhyll;
            phenologyComponent.IgnoreGrainMaturation = (IgnoreGrainMaturation)?1:0;
            phenologyComponent.IntTvern = IntTvern;
            phenologyComponent.IsVernalizable = (IsVernalizable)?1:0;
            phenologyComponent.Ldecr = Ldecr;
            phenologyComponent.Lincr = Lincr;
            phenologyComponent.MaxDL = MaxDL;
            phenologyComponent.MaxTvern = MaxTvern;
            phenologyComponent.MinDL = MinDL;
            phenologyComponent.MinTvern = MinTvern;
            phenologyComponent.Pdecr = Pdecr;
            phenologyComponent.PFLLAnth = PFLLAnth;
            phenologyComponent.PHEADANTH = PHEADANTH;
            phenologyComponent.Pincr = Pincr;
            phenologyComponent.PNini = PNini;
            phenologyComponent.SLDL = SLDL;
            phenologyComponent.VAI = VAI;
            phenologyComponent.VBEE = VBEE;
            phenologyComponent.SowingDensity = SowingDensity;
            phenologyComponent.TargetFertileShoot = TargetFertileShoot;
            phenologyComponent.slopeTSFLN = slopeTSFLN;
            phenologyComponent.intTSFLN = intTSFLN;
            phenologyComponent.SwitchMaize = (SwitchMaize)?1:0;

            if (SwitchMaize)
            {
                phenologyComponent.atip = atip;
                phenologyComponent.Leaf_tip_emerg = Leaf_tip_emerg;
                phenologyComponent.k_bl = k_bl;
                phenologyComponent.Nlim = Nlim;
            }
        }

        private SiriusQualityPhenology.Strategies.Phenology phenologyComponent;
        private SiriusQualityPhenology.PhenologyState phenologyState;
        private SiriusQualityPhenology.PhenologyState previousphenologyState;// not exactly the full copy  of phenologyState at the previous timestep. It is merely used to save values
    }

}
