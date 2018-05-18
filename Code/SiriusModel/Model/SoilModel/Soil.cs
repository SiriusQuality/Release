using System;
using System.Linq.Expressions;
using System.Windows.Forms.VisualStyles;
using SiriusModel.Model.Base;
using SiriusModel.Model.SoilModel.EnergyBalance;
using SiriusModel.Structure;
using SiriusModel.Model.Observation;
using System.Collections.Generic;

namespace SiriusModel.Model.SoilModel
{
    ///<summary>The class Soil</summary>
    public class Soil : UniverseLink
    {
       
        #region Constants

        ///<summary>Active zone under root maximal depth</summary>
        public const double ExtendedRootZone = 0.05; // (Set at 5 cm to allow the roots to access at least 1 soil Layer)

        ///<summary>The zone where the evaporation modify the soil (set at 20 cm)</summary>
        private const double EvaporationZone = 0.20;


        //PHYSIC CONSTANTS

        ///<summary>
        ///Latent heat of vaporization of water at 20°C         -  
        ///UNITS: MJ kg-1
        ///</summary>
        public const double lambda = 2.454;

        ///<summary>
        ///Heat capacity of air at constant pressure
        ///UNITS: MJ kg -1 °C-1
        ///</summary>
        public const double Cp = 0.00101;

        ///<summary>
        ///Air density
        ///UNITS kg m-3
        ///</summary>
        public const double rho = 1.225;

        ///<summary>
        ///Psychrometric constant (at 20°C and 1013 hPa)
        ///UNITS hPa °C-1
        ///</summary>
        public const double psy = 0.66;   

        ///<summary>
        ///Albedo or canopy reflection coefficient
        ///UNITS Dimensionless
        ///</summary>
        const double r = 0.23;

        ///<summary>
        ///Soil diffusion constant 
        ///UNITS mm d-1/2
        ///Ref. Eq. [8] in Jamieson et al., (1995) Agricultural and Forest Meteorology 76, 41-58.
        ///</summary>
        const double C = 4.2;   // soil diffusion constant 

        ///<summary>
        ///Temperature at the triple point of water 
        ///UNITS K
        ///</summary>
        const double TemperatureWaterTriplePoint = 273.16;

        ///<summary>
        ///von Karman’s constant
        ///UNITS Dimensionless
        ///</summary>
        const double vonKarman = 0.41;

        ///<summary>
        ///Height at which air temperature and wind speed are measured
        ///UNITS m
        ///</summary>

        ///<summary>
        ///momentum roughness length 
        ///UNITS m
        ///</summary>
        const double zm = 0.13;

        ///<summary>
        ///Zero-plan displacement
        ///UNITS m
        ///</summary>
        const double d = 0.63;

        ///<summary>
        ///Stefan-Boltzman constant
        ///UNITS MJ K-4 m-2 day-1
        ///</summary>
        double StefanBoltzman = 4.903 * Math.Pow(10, -9); 
       
        #endregion

        public EnergyBalanceBiomaWrapper energyBalanceBiomaWrapper_;
        private HourlySoilTemp hourlySoilTemp_;

        #region Fields

        private Vector<double> pastDailyIrrigation;

        ///<Behnam (2015.10.27)>
        ///<summary>To store amount of water required to satisfy water balance under unlimited conditions</summary>
        public double VirtualWReq;
        ///</Behnam>

        ///<Behnam (2015.10.28)>
        public bool isUnlimitedWater;
        public bool isUnlimitedNitrogen;
        public bool isUnlimitedTemperature;
        ///</Behnam>

        ///<summary>Actual amount of water on the leaves</summary>
        public double ActualWaterOnLeaves { get; set; }
        ///<summary>
        ///The stress factor of the universe.
        ///</summary>
        public StressFactor StressFactor_ { get; protected set; }

        ///<summary>Layers of this Soil</summary>
        public Vector<Layer> Layers { get; private set; }

        ///<summary>Layers of this soil on wich the root can operate</summary>
        public VectorSegment<Layer, Layer> RootLayers { get; set; }

        ///<summary>Soil moisture</summary>
        private double Q { get; set; }

        ///<summary>Soil porosity, staturation soil moisture</summary>
        private double QS { get; set; }

        ///<summary>Soil moisture content at field capacity</summary>
        private double QFC { get; set; }

        ///<summary>Soil moisture content at wilting point</summary>
        private double QWP { get; set; }

        ///<summary>Current organic content of the soil</summary>
        private double CurrentNo { get; set; }

        ///<summary>Cumulative quantity of inorganic N from fertilisation</summary>
        public double CumulativeNfertilisation { get; set; }

        ///<summary>Current quantity of inorganic N undisolved (from fertilisation)</summary>
        public double UndisolvedFertilisation { get; set; }

        ///<summary>Current irrigation to add to this soil</summary>
        private double UndisolvedIrrigation { get; set; }

        ///<summary>Potential transpiration</summary>
        public double PotTransp { get; private set; }

        ///<summary>Actual transpiration</summary>
        public double ActTransp { get; private set; }

        ///<summary>Potential evapotranspiration</summary>
        public double PotET { get; private set; }

        ///<summary>Actual evapotranspiration</summary>
        public double ActET { get; private set; }

        ///<summary>Evaporation of the water in the soil</summary>
        public double DailyEvaporation { get; set; }

        ///<summary>The quantity of Ni consumed by plant, removed the next day</summary>
        public double NtoRemove { get; set; }

        ///<summary>The quantity of Ni available for root uptake</summary>
        public double NavForRoot { get; set; }

        ///<summary>Min soil temperature</summary>
        public double SoilMinTemperature { get; set; }

        ///<summary>Max soil temperature</summary>
        public double SoilMaxTemperature { get; set; }

        ///<summary>Mean temperature of the soil</summary>
        public double SoilMeanTemperature { get { return (SoilMaxTemperature + SoilMinTemperature) / 2.0; } }

        ///<sumary> Soil hourly temperature</sumary>
        public double[] HourlySoilTemperature { get; private set; }

        ///<summary>Value of the method slosl at the beginning of the day</summary>
        private double SloslResult { get; set; }

        ///<summary>The value of the methods ptSoil at the beginning of the day</summary>
        private double PtSoilResult { get; set; }

        ///<summary>Next min temperature of the canopy</summary>
        public double MinimumCanopyTemperature { get; set; }

        ///<summary>Next max temperature of the canopy</summary>
        public double MaximumCanopyTemperature { get; set; }

        ///<summary>Next mean temperature of the canopy</summary>
        public double MeanCanopyTemperature { get; set; }

        ///<summary>Hourly temperature of the canopy</summary>
        public double[] HourlyCanopyTemperature { get { return energyBalanceBiomaWrapper_.HourlyCanopyTemperature_; } }

        ///<summary>Mean air temperature</summary>
        public double MeanAirTemperature { get; set; }

        ///<summary>Daily irrigation of the soil</summary>
        private double DailyIrrigation { get; set; }

        ///<summary>Available Ni for roots at the beginning of the day step</summary>
        public double DailyAvNforRoots { get; private set; }

        ///<summary>Unavailable Ni for roots at the beginning of the day step</summary>
        public double DailyUnavNforRoots { get; private set; }

        ///<summary>Irrigation (rain and mamagement) added to the soil from the beginning of the simulation</summary>
        public double IncomeWater { get; private set; }

        ///<summary>Potential evapotranspiration from the beginning of the simulation</summary>
        public double AccumulatedPotEvapoTranspiration { get; private set; }

        ///<summary>Evapotranspiration from the beginning of the simulation</summary>
        public double AccumulatedActEvapoTranspiration { get; private set; }

        ///<summary>Evaporation from the beginning of the simulation</summary>
        public double AccumulatedActSoilEvaporation { get; private set; }

        ///<summary>Potential transpiration from the beginning of the simulation</summary>
        public double AccumulatedPotTranspiration { get; private set; }

        ///<summary>Transpiration from the beginning of the simulation</summary>
        public double AccumulatedActTranspiration { get; private set; }

        ///<summary>Last Layer of this Soil</summary>
        public DeepLayer SoilDeepLayer_;

        ///<summary>Get or set N lost  by the soil</summary>
        public double LostNitrogen { get { return SoilDeepLayer_.LostNitrogen; } }

        ///<summary>N fertilization and organic N mineralisation</summary>
        public double IncomeNitrogen { get; set; }

        ///<summary>Accumulated N mineralisation (gN/m2)</summary>
        public double AccumulatedNitrogenMineralisation { get; set; }

        ///<summary>Accumulated N denitrification (gN/m2)</summary>
        public double AccumulatedNitrogenDenitrification { get; set; }

        ///<summary>
        ///Daily soil evaporation. Used to calculate Tcanopy
        ///</summary>
        public double SoilEvaporation { get; set; }

        ///<Behnam>
        ///<summary>Height of wind measurements (m)</summary>
        public double heightMeasurements { get; set; }
        public double TRSF { get; set; }
        public double ETSF { get; set; }
        ///<Behnam>

        public double SMSF { get { return StressFactor_.SMSF; } }
        public double FPAW { get { return StressFactor_.FPAW; } }
        public double DBF { get { return StressFactor_.DBF; } }
        public double DEBF  { get { return StressFactor_.DEBF;}}
        public double DTF { get { return StressFactor_.DTF;}}
        public double DGF { get { return StressFactor_.DGF;}}

        public double[] VPDeq { get { return energyBalanceBiomaWrapper_.VPDeq; } }
        public double[] VPDairLeaf { get { return energyBalanceBiomaWrapper_.HourlyVPDAirLeaf; } }

        ///<summary>
        ///Available water content in mm in the first 3 cm of the soil
        ///</summary>
        public double WC_3cm { get { return Layers[0].AvWater * Run.GwaterToMMwater *0.03/Layer.Thickness; } } //probably not exact as water is not evently available in the first 5 cm

        #endregion

        #region Constructors

        ///<summary>Create a new soil</summary>
        ///<param name="universe">Universe of the model</param>
        public Soil(Universe universe)
            : base(universe)
        {
            SoilDeepLayer_ = new DeepLayer(universe);
            Layers = new Vector<Layer>();
            RootLayers = new VectorSegment<Layer, Layer>(Layers, 0, 0);
            StressFactor_ = new StressFactor(universe);
            energyBalanceBiomaWrapper_ = new EnergyBalanceBiomaWrapper(universe);
            Q = 0;
            QS = 0;
            QFC = 0;
            QWP = 0;
            CurrentNo = 0;
            UndisolvedFertilisation = 0;
            CumulativeNfertilisation = 0;
            UndisolvedIrrigation = 0;
            PotTransp = 0;
            ActTransp = 0;
            DailyEvaporation = 0;
            NtoRemove = 0;
            NavForRoot = 0;
            SoilMinTemperature = 0;
            SoilMaxTemperature = 0;
            HourlySoilTemperature = new double[24];
            SloslResult = 0;
            PtSoilResult = 0;
            MinimumCanopyTemperature = 0;
            MaximumCanopyTemperature = 0;
            MeanCanopyTemperature = 0;
            MeanAirTemperature = 0;
            DailyIrrigation = 0;
            DailyAvNforRoots = DailyUnavNforRoots = 0;
            IncomeWater = 0;
            AccumulatedActEvapoTranspiration = 0;
            AccumulatedPotEvapoTranspiration = 0;
            AccumulatedActSoilEvaporation = 0;
            AccumulatedPotTranspiration = 0;
            AccumulatedActTranspiration = 0;
            IncomeNitrogen = 0;
            AccumulatedNitrogenMineralisation = 0;
            AccumulatedNitrogenDenitrification = 0;
            SoilEvaporation = 0;
            ActualWaterOnLeaves = 0;

            pastDailyIrrigation = new Vector<double>();

            ///<Behnam>
            VirtualWReq = 0;
            heightMeasurements = MeasurementHeight;
            TRSF = 0;
            ETSF = 0;
            ///</Behnam>
            ///
            if (SwitchMaize)
            {
                hourlySoilTemp_ = new HourlySoilTemp();
            }
        }

        ///<summary>Create a new soil by copy</summary>
        ///<param name="universe">Universe of the model</param>
        ///<param name="toCopy">Soil to copy</param>
        ///<param name="copyAll">false copy only the outputs</param>
        public Soil(Universe universe, Soil toCopy, bool copyAll)
            : base(universe)
        {
            var nbLayer = toCopy.Layers.Count;
            Layers = new Vector<Layer>(nbLayer);
            for (var i = 0; i < nbLayer; ++i)
            {
                Layers[i] = new Layer(toCopy.Layers[i]);
            }

            RootLayers = new VectorSegment<Layer, Layer>(Layers, toCopy.RootLayers);
            SoilDeepLayer_ = (toCopy.SoilDeepLayer_ != null) ? new DeepLayer(universe, toCopy.SoilDeepLayer_) : null;
            Q = toCopy.Q;
            QS = toCopy.QS;
            QFC = toCopy.QFC;
            QWP = toCopy.QWP;
            CurrentNo = toCopy.CurrentNo;
            UndisolvedFertilisation = toCopy.UndisolvedFertilisation;
            CumulativeNfertilisation = toCopy.CumulativeNfertilisation; 
            UndisolvedIrrigation = toCopy.UndisolvedIrrigation;
            PotTransp = toCopy.PotTransp;
            ActTransp = toCopy.ActTransp;
            DailyEvaporation = toCopy.DailyEvaporation;
            NtoRemove = toCopy.NtoRemove;
            NavForRoot = toCopy.NavForRoot;
            SoilMinTemperature = toCopy.SoilMinTemperature;
            SoilMaxTemperature = toCopy.SoilMaxTemperature;
            SloslResult = toCopy.SloslResult;
            PtSoilResult = toCopy.PtSoilResult;
            MinimumCanopyTemperature = toCopy.MinimumCanopyTemperature;
            MaximumCanopyTemperature = toCopy.MaximumCanopyTemperature;
            MeanCanopyTemperature = toCopy.MeanCanopyTemperature;
            HourlySoilTemperature = new double[24];
            for (int i = 0; i < 24; i++ )
            {
                HourlySoilTemperature[i] = toCopy.HourlySoilTemperature[i];
            }
            MeanAirTemperature = toCopy.MeanAirTemperature;
            DailyIrrigation = toCopy.DailyIrrigation;
            DailyAvNforRoots = toCopy.DailyAvNforRoots;
            DailyUnavNforRoots = toCopy.DailyUnavNforRoots;
            IncomeWater = toCopy.IncomeWater;
            AccumulatedActEvapoTranspiration = toCopy.AccumulatedActEvapoTranspiration;
            AccumulatedPotEvapoTranspiration = toCopy.AccumulatedPotEvapoTranspiration;
            AccumulatedActSoilEvaporation = toCopy.AccumulatedActSoilEvaporation;
            AccumulatedPotTranspiration = toCopy.AccumulatedPotTranspiration;
            AccumulatedActTranspiration = toCopy.AccumulatedActTranspiration;
            IncomeNitrogen = toCopy.IncomeNitrogen;
            AccumulatedNitrogenDenitrification = toCopy.AccumulatedNitrogenDenitrification;
            AccumulatedNitrogenMineralisation = toCopy.AccumulatedNitrogenMineralisation;
            SoilEvaporation = toCopy.SoilEvaporation;
            StressFactor_ = (toCopy.StressFactor_ != null) ? new StressFactor(universe, toCopy.StressFactor_) : null;
            ActualWaterOnLeaves = toCopy.ActualWaterOnLeaves;
            VirtualWReq = toCopy.VirtualWReq;
            TRSF = toCopy.TRSF;
            ETSF = toCopy.ETSF;

            energyBalanceBiomaWrapper_ = (toCopy.energyBalanceBiomaWrapper_ != null) ? new EnergyBalanceBiomaWrapper(universe, toCopy.energyBalanceBiomaWrapper_, copyAll) : null;
            if (copyAll)
            {            
                pastDailyIrrigation = new Vector<double>(toCopy.pastDailyIrrigation.Count);
                for (var i = 0; i < toCopy.pastDailyIrrigation.Count; ++i)
                {
                    pastDailyIrrigation[i] = toCopy.pastDailyIrrigation[i];
                }

                isUnlimitedWater = toCopy.isUnlimitedWater;
                isUnlimitedNitrogen = toCopy.isUnlimitedNitrogen;
                isUnlimitedTemperature = toCopy.isUnlimitedTemperature;
                heightMeasurements = toCopy.heightMeasurements;

                hourlySoilTemp_ = toCopy.hourlySoilTemp_;

            }
        }

        ///<summary>Add a Layer to this Soil. This reduce the new Layer in several layers of 5 cm depth</summary>
        ///<param name="inSSAT">SSAT of the new Layer</param>
        ///<param name="inSDUL">SDUL of the new Layer</param>
        ///<param name="inSLL">SLL of the new Layer</param>
        ///<param name="layerThickness">Thickness of the Layer</param>
        private void AddLayer(double inClay, double inKql, double inSSAT, double inSDUL, double inSLL, double layerThickness)
        {
            int nbLayer = (int)(0.5 + layerThickness / Layer.Thickness);
            for (int layerIndex = 0; layerIndex < nbLayer; ++layerIndex) // correction 22 Aug 2011, previously loop with double type so loop end condition was imprecise and led to a few extra layers.
            {
                Layers.Add(new Layer(inClay, inKql, inSSAT, inSDUL, inSLL));
            }
        }

        #endregion

        #region Init/InitDayStep

        ///<summary>Init the soil for a new simulation</summary>
        public void Init(double minTemp, double maxTemp, double meanTemp,bool isFirstInit=true)
        {
            //if (isFirstInit)
            //{

                energyBalanceBiomaWrapper_.Init();

                var soilProfile = Run.SoilDef;

                double lastLayerDepth = 0;

                if (!isFirstInit)
                {
                    Layers = new Vector<Layer>();
                    RootLayers = new VectorSegment<Layer, Layer>(Layers, 0, 0);
                }
                    foreach (var layer in soilProfile.Layers)
                    {
                        var layerThickness = layer.Depth - lastLayerDepth;
                        AddLayer(layer.Clay, layer.Kql, layer.SSAT, layer.SDUL, layer.SLL, layerThickness);
                        lastLayerDepth = layer.Depth; // correction 22 Aug 2011, previously wrong update of lastLayerDepth.
                    }
                
                ///<Behnam (2015.11.27)>
                ///<Comment>Reduced from 40cm to 20cm</Comment>
                if (Layers.Count < 4)
                    throw new Exception("You must define at least 20 cm of soil profile. The soil "
                                        + soilProfile.Name + " defines " + Layers.Count * Layer.Thickness + " cm");
                ///</Behnam>

                RootLayers.Start = 0;
                RootLayers.Stop = 1;

                ///<Behnam>

                Q = 0;
                QS = Layers[0].SSAT;
                QFC = 0;
                QWP = 0;
                CurrentNo = No;
                UndisolvedFertilisation = 0;
                CumulativeNfertilisation = 0;
                UndisolvedIrrigation = 0;
                PotTransp = 0;
                ActTransp = 0;
                DailyEvaporation = 0;
                NtoRemove = 0;
                NavForRoot = 0;
                SloslResult = 0;
                PtSoilResult = 0;
                MinimumCanopyTemperature = 0;
                MaximumCanopyTemperature = 0;
                MeanCanopyTemperature = 0;
                MeanAirTemperature = 0;
                DailyIrrigation = 0;
                DailyAvNforRoots = DailyUnavNforRoots = 0;
                IncomeWater = 0;
                AccumulatedPotEvapoTranspiration = 0;
                AccumulatedActEvapoTranspiration = 0;
                AccumulatedActSoilEvaporation = 0;
                AccumulatedPotTranspiration = 0;
                AccumulatedActTranspiration = 0;
                IncomeNitrogen = 0;
                AccumulatedNitrogenMineralisation = 0;
                AccumulatedNitrogenDenitrification = 0;
                SoilEvaporation = 0;
                VirtualWReq = 0;
                heightMeasurements = MeasurementHeight;
                TRSF = 0;
                ETSF = 0;
                ///</Behnam>

                pastDailyIrrigation = new Vector<double>();

                IncomeNitrogen = TotalNi;
                SetNiAllocation(TotalNi, TopNi, MidNi);

                ///<Behnam (2015.10.16)>
                ///<Comment>
                ///A new argument was added to determine if the initial water 
                ///deficit is indicated in absolute amount or percentage
                ///</Comment>
                bool isPercentage = this.Universe_.Run.ManagementDef.IsWDinPerc;
                SetDeficit(Deficit, isPercentage);
                ///</Behnam>
            //}
            SoilDeepLayer_.Init(meanTemp);

            SoilMinTemperature = minTemp;
            SoilMaxTemperature = maxTemp;
            MinimumCanopyTemperature = SoilMinTemperature;
            MaximumCanopyTemperature = SoilMaxTemperature;
            MeanCanopyTemperature = (MinimumCanopyTemperature + MaximumCanopyTemperature) / 2;

            if (double.IsNaN(SoilMinTemperature) || double.IsNaN(SoilMaxTemperature))
            {
                throw new Exception("Soil temperature becomes NaN (division by 0).");
            }
            ActualWaterOnLeaves = 0;
        }

        #region Layers

        ///<summary>
        ///Set the initial inorganic nitrogen allocation.
        ///initial inorganic Ni allocation, TNi, kg/ha
        ///will  be allocated in MaxD = getTotalSoilDepth() soil; TopNi in
        ///[0,.33MaxD] MidNi in [0.33MaxD,0.66MaxD], and the rest in [.66MaxD,MaxD]
        ///50% goes to nInAvailableWater_ and 50% goes to nInUnavailableWater_.
        ///</summary>
        ///<param name="totalNi">The total amount of inorganic nitrogen to set in this Soil.</param>
        ///<param name="topNiPercent">The percent of totalNi to put in the top of the Soil 
        ///(first 1/3 of the Soil depth).</param>
        ///<param name="midNiPercent">The percent of totalNi to put in the middle of the Soil 
        ///(second 1/3 of the Soil depth).</param>
        private void SetNiAllocation(double totalNi, double topNiPercent, double midNiPercent)
        {
            // initial inorganic Ni allocation, TNi, g/m^2
            // will  be allocated in MaxD soil; TopNi in [0,.33MaxD]
            // MidNi in [0.33MaxD,0.66MaxD], and the rest in [.66MaxD,MaxD]
            // 50% goes to Naw and 50% goes to Nuw

            topNiPercent /= 100.0;
            midNiPercent /= 100.0;

            var maxDepth = SoilDepth; // the total depth of the soil

            var maxTopDepth = 1.0 * maxDepth / 3.0; // 0 -> 1/3 depth is the top
            var maxMidDepth = 2.0 * maxDepth / 3.0; // 1/3 -> 2/3 depth is the mid
            var currentDepth = Layer.Thickness / 2.0; // the current depth of the algorithm.

            var nbTopLayer = maxTopDepth / Layer.Thickness;
            var nbMidLayer = (maxMidDepth - maxTopDepth) / Layer.Thickness;
            var nbBotLayer = (maxDepth - maxMidDepth) / Layer.Thickness;

            // percent of Ni in bottom layers
            var botNiPercent = 1.0 - topNiPercent - midNiPercent;

            foreach (var layer in Layers)
            {
                if (currentDepth < maxTopDepth) // do top Layer
                {
                    layer.AvN = layer.UnavN = 0.5 * totalNi * topNiPercent / nbTopLayer;
                }
                else if (currentDepth < maxMidDepth) // do mid Layer
                {
                    layer.AvN = layer.UnavN = 0.5 * totalNi * midNiPercent / nbMidLayer;
                }
                else // do the rest
                {
                    layer.AvN = layer.UnavN = 0.5 * totalNi * botNiPercent / nbBotLayer;
                }
                currentDepth += Layer.Thickness;

                layer.ExN = 0;
            }
        }

        ///<summary>
        ///Set the deficit of the layers.
        ///Previously the deficit was allocated constantly over each Layer
        ///Have now weighted the deficit distrubution. RFZ Aug 2001
        ///</summary>
        ///<param name="inDeficit">The new deficit of the Soil.</param>
        private void SetDeficit(double inDeficit, bool isPercentage)
        {
            // Previously the deficit was allocated constantly over each Layer
            // Have now weighted the deficit distrubution. RFZ Aug 2001

            // first : translate inDeficit to a percent of available water

            var totalAw = GetTotalMaxAvailableWater();
            if (totalAw <= 0) throw new Exception("Soil initialization : Total AWC is negative or null.");
            if (!isPercentage & inDeficit * 1000.0 > totalAw) throw new Exception("Soil initialization : Soil deficit is greather than Total AWC");
            if (isPercentage & inDeficit > 100) throw new Exception("Soil initialization : Soil deficit is greather than 100%");

            ///<Behnam (2015.10.16)>
            ///<Comment>
            ///A new argument was added to determine if the initial water 
            ///deficit value is indicated in absolute amount or percentage
            ///</Comment>
           
            double percentAvailable = 0;
            if (isPercentage)
            {
                percentAvailable = 1 - inDeficit / 100;
            }
            else
            {
                percentAvailable = 1 - (inDeficit * Run.MMwaterToGwater / totalAw);
            }
            ///</Behnam>
            
            if (percentAvailable < 1e-5) percentAvailable = 0.0;

            foreach (var layer in Layers)
            {
                layer.AvWater = layer.MaxAvWater * percentAvailable;
            }
        }

        #endregion

        ///<summary>Init the soil for a new day step</summary>
        public void InitDayStep(double rootLength)
        {
            DailyIrrigation = 0;
            RootLayers.Stop = Math.Min(Layers.Count, (int)((rootLength + ExtendedRootZone) / Layer.Thickness));
        }

        #endregion

        #region RunDayStep

        ///<summary>Element a day step (calls the virtual method doRunDayStep)</summary>
        public void RunDayStep(double radTopAtm, bool isWindAndVpDefined, double vp, double rad, 
            double minTemp, double maxTemp, double meanTemp,double[] hourlyTemp, double wind, double meanWeekTemp, 
            double HSlope, double VPDair, double Tau, double SumInternodesLength,
            double potentialWaterOnLeaves, double rootLength, double leafNumber, double[] hourlyVPDair,double[] hourlysolarRadiation, double Ntip, double[] RH)
        {
            StressFactor_.isUnlimitedWater = isUnlimitedWater;
            
            double deficitOnTopLayers = CalculateAccumulatedDeficit(EvaporationZone);

            energyBalanceBiomaWrapper_.EstimateEnergyBalance(deficitOnTopLayers, minTemp, maxTemp, hourlyTemp, rad, radTopAtm, vp, Tau, HSlope, SumInternodesLength, wind, VPDair, isWindAndVpDefined, hourlyVPDair, hourlysolarRadiation, WC_3cm, Ntip, RH);


            MinimumCanopyTemperature = energyBalanceBiomaWrapper_.MinCanopyTemperature_;
            MaximumCanopyTemperature = energyBalanceBiomaWrapper_.MaxCanopyTemperature_;
            MeanCanopyTemperature = (MinimumCanopyTemperature + MaximumCanopyTemperature) / 2;

            // Energy Balance
            // Soil temperature
            // OUTPUT UNIT: °C
            SoilTemperature st = new SoilTemperature();
           
            ///<Behnam (2016.01.08)>
            SoilMinTemperature = st.SoilMinimumTemperature(maxTemp,
                                                            meanTemp,
                                                            minTemp,
                                                            energyBalanceBiomaWrapper_.SoilHeatFlux_,
                                                            lambda,
                                                            SoilDeepLayer_.Temperature);
            SoilMaxTemperature = st.SoilMaximumTemperature(maxTemp,
                                                            meanTemp,
                                                            minTemp,
                                                            energyBalanceBiomaWrapper_.SoilHeatFlux_,
                                                            lambda,
                                                            SoilDeepLayer_.Temperature);

            ///</Behnam>

            if (SwitchMaize)
            {

                HourlySoilTemperature = hourlySoilTemp_.getHourlySoilSurfaceTemperature(SoilMaxTemperature, SoilMinTemperature, CurrentDate.DayOfYear, Latitude);
            }

            Check.IsNumber(SoilMinTemperature);
            Check.IsNumber(SoilMaxTemperature);
            SoilDeepLayer_.UpdateTemperature(this);

            ///<Behnam (2015.11.27)>
            PotTransp = energyBalanceBiomaWrapper_.PotentialTranspiration_;
            ActTransp = PotTransp;
            // if (!UnlimitedWater) FillWaterProfile();
            ///</Behnam>
           
            SoilMoisture();

            NitrogenPulses(meanWeekTemp);
            
            Percolation(potentialWaterOnLeaves);

            Equilibrium();

            
            Evaporate(energyBalanceBiomaWrapper_.SoilEvaporation_);


            
            WaterUptakeByRoots(energyBalanceBiomaWrapper_.PotentialTranspiration_, rootLength);

          
            // Energy Balance
            // Canopy temperature
            // OUTPUT UNITS: °C
            /* CanopyTemperature ct = new CanopyTemperature();
            MinimumCanopyTemperature = ct.CanopyTemp(minTemp, CropHeatFlux, conductance, lambda, rho, Cp);
            MaximumCanopyTemperature = ct.CanopyTemp(maxTemp, CropHeatFlux, conductance, lambda, rho, Cp);*/

            //NavForRoot = DailyAvNforRoots = 0;
            NavForRoot = DailyAvNforRoots = CalculateAvailableNForRoots();
            DailyUnavNforRoots = CalculateUnavailableNForRoots();

            ActET = DailyEvaporation + ActTransp;
            PotET = energyBalanceBiomaWrapper_.PotentialEvapoTranspiration_;

            AccumulatedActEvapoTranspiration += ActET;
            AccumulatedPotEvapoTranspiration += PotET;
            AccumulatedActSoilEvaporation    += DailyEvaporation;
            AccumulatedActTranspiration      += ActTransp;
            AccumulatedPotTranspiration += energyBalanceBiomaWrapper_.PotentialTranspiration_;

            TRSF = StressFactor_.SF(PotTransp, ActTransp, 1); /// Behnam (2016.02.10): Pierre wanted to change the powet to 1;
            ETSF = StressFactor_.SF(PotET, ActET, 1); /// Behnam (2016.02.10): Pierre wanted to change the powet to 1;
            StressFactor_.TranspSF = TRSF;
            StressFactor_.InitDayStep(CalculateRootZoneMaxAvailableWater(), CalculateRootZoneAvailableWater());

            Check.IsNumber(SoilMinTemperature);
            Check.IsNumber(SoilMaxTemperature);
        }

        ///<summary>Percolation of water between soil layers</summary>
        private void Percolation( double potentialWaterOnLeaves)
        {

            // Percolation and leaching based on T. Addiscott leaching model.

            var actualWaterOnLeaves = ActualWaterOnLeaves;

            // canopy interception of water
            if (potentialWaterOnLeaves - actualWaterOnLeaves < UndisolvedIrrigation)
            {
                UndisolvedIrrigation -= potentialWaterOnLeaves - actualWaterOnLeaves;
                ActualWaterOnLeaves = potentialWaterOnLeaves;
            }
            else
            {
                ActualWaterOnLeaves = actualWaterOnLeaves + UndisolvedIrrigation;
                UndisolvedIrrigation = 0;
            }

            ///<Behnam>
            // Percolate water through the layers
            const double minAvWater = 10;

            var nbLayer = Layers.Count;
            for (var i = 0; i < nbLayer - 1; ++i)
            {
                var layer = Layers[i];
                var nextLayer = Layers[i + 1];

                if (UndisolvedIrrigation >= minAvWater)
                {
                    // Try to fill the AW bucket with water first
                    if (layer.MaxAvWater - layer.AvWater < UndisolvedIrrigation)
                    {
                        UndisolvedIrrigation -= layer.MaxAvWater - layer.AvWater;

                        layer.AvWater = layer.MaxAvWater;
                        // Fill exw bucket assuming that there is a limit of EXW and percolate the rest of the WP water to the next Layer moving nitrogen
                        if (layer.MaxExWater - layer.ExWater < UndisolvedIrrigation)
                        {
                            // Fill EXW bucket moving nitrogen down
                            UndisolvedIrrigation -= layer.MaxExWater - layer.ExWater;

                            layer.ExWater = layer.MaxExWater;

                            // Calculate proportion of passing water for Ni
                            var x = UndisolvedIrrigation / (UndisolvedIrrigation + layer.MaxExWater);

                            nextLayer.ExN += x * layer.ExN;
                            layer.ExN -= x * layer.ExN;
                        }
                        else
                        {
                            // Fill exw bucket
                            layer.ExWater += UndisolvedIrrigation;
                            UndisolvedIrrigation = 0;
                        }
                    }
                    else
                    {
                        // Fill AW bucket
                        layer.AvWater += UndisolvedIrrigation;
                        UndisolvedIrrigation = 0;
                    }
                }

                if (!IsKqlUsed) layer.Kql = Kq;

                UndisolvedIrrigation += layer.Kql * layer.ExWater;
                layer.ExWater = (1.0 - layer.Kql) * layer.ExWater;

                nextLayer.ExN += layer.Kql * layer.ExN;
                layer.ExN = (1.0 - layer.Kql) * layer.ExN;

            }
            ///</Behnam>

            var lastLayer = Layers[nbLayer - 1];
            // Try to fill the AW bucket with water first (last Layer)
            if (lastLayer.MaxAvWater - lastLayer.AvWater < UndisolvedIrrigation)
            {
                UndisolvedIrrigation -= lastLayer.MaxAvWater - lastLayer.AvWater;

                lastLayer.AvWater = lastLayer.MaxAvWater;
                // Fill exw bucket assuming that there is a limit of EXW and percolate the rest of the WP water to the deep soil moving nitrogen
                if (lastLayer.MaxExWater - lastLayer.ExWater < UndisolvedIrrigation)
                {
                    // Fill EXW bucket moving nitrogen down
                    UndisolvedIrrigation -= lastLayer.MaxExWater - lastLayer.ExWater;

                    lastLayer.ExWater = lastLayer.MaxExWater;

                    // Calculate proportion of passing water for Ni
                    var x = UndisolvedIrrigation / (UndisolvedIrrigation + lastLayer.MaxExWater);

                    SoilDeepLayer_.LostNitrogen += x * (lastLayer).ExN;
                    lastLayer.ExN -= x * (lastLayer).ExN;
                }
                else
                {
                    // Fill exw bucket
                    lastLayer.ExWater += UndisolvedIrrigation;
                    UndisolvedIrrigation = 0;
                }
            }
            else
            {
                // Fill AW bucket
                lastLayer.AvWater += UndisolvedIrrigation;
                UndisolvedIrrigation = 0;
            }

            ///<Behnam>
            if (!IsKqlUsed) lastLayer.Kql = Kq;

            SoilDeepLayer_.LostWater += lastLayer.Kql * lastLayer.ExWater;
            lastLayer.ExWater = (1.0 - lastLayer.Kql) * lastLayer.ExWater;

            SoilDeepLayer_.LostNitrogen += lastLayer.Kql * lastLayer.ExN;
            lastLayer.ExN = (1.0 - lastLayer.Kql) * lastLayer.ExN;
            ///</Behnam>

            SoilDeepLayer_.LostWater += UndisolvedIrrigation;
            UndisolvedIrrigation = 0;

            /// Behnam (2016.05.14): Filling back soil moisture to WCompensationLevel% of MaxAvWater.
            if (isUnlimitedWater) FillWaterProfile(WCompensationLevel / 100);
        }

        /// Behnam (2016.05.14): Refill soil profile up to UpperFPAWexp.
        ///</summary>
        private void FillWaterProfile(double MaxThreshold)
        {
            foreach (var layer in RootLayers)
            {
                var NeededWater = Math.Max(0, layer.MaxAvWater * MaxThreshold - layer.AvWater);
                VirtualWReq += NeededWater;
                layer.AvWater += NeededWater;
            }
        }
        ///</Behnam>

        ///<summary>Equilibrium concentration of N in AW, UW and EXW bucket</summary>
        private void Equilibrium()
        {
            // Relative rate = 40 % per day between AW and UW bucket and then
            // Relative rate = 70 % per day between AW and EXW bucket

            foreach (var layer in Layers)
            {
                /* //Debug
                double unavW = layer.UnavWater;
                double avW = layer.AvWater;
                double exW = layer.ExWater;

                UniverseLink.RoundZero(ref unavW);
                UniverseLink.RoundZero(ref avW);
                UniverseLink.RoundZero(ref exW);
                */
                
                if (layer.AvWater > 0)
                //if (avW > 0)
                {
                    double dN;
                    //if (unavW > 0.0)
                    if (layer.UnavWater > 0.0)
                    {
                        dN = -layer.UnavN + (layer.AvN + layer.UnavN) * layer.UnavWater / (layer.AvWater + layer.UnavWater);

                        layer.UnavN += 0.4 * dN;
                        layer.AvN -= 0.4 * dN;
                    }
                    //Debug
                    //if (exW > 0.0)
                    if (layer.ExWater > 0.0)
                    {
                        dN = -layer.AvN + layer.AvWater * (layer.AvN + layer.ExN) / (layer.AvWater + layer.ExWater);

                        layer.AvN += 0.7 * dN;
                        layer.ExN -= 0.7 * dN;
                    }
                 
                }

                 
               
            }
        }

        ///<summary>Set the evaporation</summary>
        ///<param name="evaporation">Value of evaporation</param>
        private void Evaporate(double evaporation)
        {
            // Soil evaporated water is extracted from the first 20 cm of the soil from EXW bucket and then from AW bucket.

            const double minEvapo = 1.0;
            const double maxDepth = 0.20;

            DailyEvaporation = 0;
            if (evaporation > minEvapo)
            {
                var evapoLayers
                    = new VectorSegment<Layer, Layer>(Layers, 0, Math.Min(Layers.Count, (int)(maxDepth / Layer.Thickness)));

                foreach (var layer in evapoLayers)
                {
                    if (evaporation <= 0) break;
                    if (layer.ExWater >= evaporation)
                    {
                        layer.ExWater -= evaporation;
                        DailyEvaporation += evaporation;
                        evaporation = 0;
                        break;
                    }
                    else
                    {
                        DailyEvaporation += layer.ExWater;
                        evaporation -= layer.ExWater;
                        layer.ExWater = 0;
                    }
                    if (evaporation > 0)
                    {
                        if (layer.AvWater >= evaporation)
                        {
                            layer.AvWater -= evaporation;
                            DailyEvaporation += evaporation;
                            evaporation = 0;
                            break;
                        }
                        else
                        {
                            DailyEvaporation += layer.AvWater;
                            evaporation -= layer.AvWater;
                            layer.AvWater = 0;
                        }
                    }
                }

            }
        }

        ///<summary>Calculate soil moisture. Update q_, qs_, qfc_, qwp_ </summary>
        private void SoilMoisture()
        {
            // 0.1 = 100% * (mm) / (m = 1000mm)  1000.0 = convert from gH2O per m^2 to mm

            Q = (0.1 * (Layers[0].ExWater + Layers[0].AvWater + Layers[0].UnavWater) / Layer.Thickness) / 1000.0;

            // is constant !
            QFC = (0.1 * (Layers[0].MaxAvWater + Layers[0].UnavWater) / Layer.Thickness) / 1000.0;

            // is constant !
            QWP = (0.1 * (Layers[0].UnavWater) / Layer.Thickness) / 1000.0;
        }

        ///<summary>
        ///Calculate nitrogen pulses.
        ///This method removes the quantity of disolved nitrogen for this day.
        //
        //
        ////Amount Nf of fertilizer has been dissolved
        //  SS.AddNitrogen(-Nf);
        //
        ////Denitrification pulses
        //  Ndp = SP.alfa*Nf;
        //        ODS("Ndp : ", Ndp);
        //
        ////Inorganic Ni from fertilizers
        //  Ni = Nf-Ndp;
        //        ODS("Ni : ", Ni);
        //
        ///</summary>
        private void NitrogenPulses(double meanWeekTemp)
        {
            const double minUpperSoilMoisture = 0.8;
            const double inorganicFertiliserRate = 0.8;
            const int maxNumberOfLayerToPulse = 8;

            var qr = (Q - QWP) / (QFC - QWP);

            // criterion 1, upper soil Layer is moist
            var cr1 = qr > minUpperSoilMoisture;

            var r3D =  GetAccumulatedIrrigation(3);
            var r7D = GetAccumulatedIrrigation(7);

            // criterion 2, heavy rain during last three days
            var cr2 = r3D / 1000.0 > 7.0 * (1.9 - qr);

            // cirterion 3, rain during last seven days
            var cr3 = r7D / 1000.0 > 12.0 * (1.9 - qr);

            double nf; // manager fertilisation

            if (cr1 || cr2 || cr3)
            {
                nf = inorganicFertiliserRate * UndisolvedFertilisation;
            }
            else
            {
                nf = 0.05 * UndisolvedFertilisation;
            }

            // update the managerAdd quantity which need to be disolved
            UndisolvedFertilisation -= nf;

            var ndp = Ndp * nf; // denitrificaiton pulses
            // inorganic Ni from fertilizers
            var ni = nf - ndp; // change in soil inorganic nitrogen
            // update the amount of nitrogen in the top Layer
            Check.IsPositiveOrZero(ni);

            AccumulatedNitrogenDenitrification += ndp;

            IncomeNitrogen += ni;
            Layers[0].AvN += ni;

            // Assume that mineralization of organic N (No) takes place in the top 40-cm of the soil and No is equally distributed between each Layer. 
            // Mineralized N (Nm) is distributed between AW and UW bucket in proportion with the water content of the bucket.
            // Mineralization is a function of temperature (temperature function ft) and soil moisture (socking function Fq).

            var nm = Ko * Ft(meanWeekTemp) * Fq() * CurrentNo; // mineralisation

            AccumulatedNitrogenMineralisation += nm;
            CurrentNo -= nm;
            IncomeNitrogen += nm;
            Check.IsPositiveOrZero(CurrentNo);

            ///<Behnam (2015.11.27)>
            ///<Comment>To enable using soil depth without any limitations</Comment>
            var numberOfLayerToPulse = Math.Min(maxNumberOfLayerToPulse,Layers.Count);
            ///</Behnam>
            
            var pulsedLayers
                = new VectorSegment<Layer, Layer>(Layers, 0, numberOfLayerToPulse);
            foreach (var layer in pulsedLayers)
            {
                double x;
                if (layer.UnavWater == 0)
                {
                    x = 0;
                }
                else
                {
                    x = (nm / numberOfLayerToPulse) * layer.UnavWater / (layer.AvWater + layer.UnavWater);
                }
                Check.IsPositiveOrZero(x);
                layer.UnavN += x;

                Check.IsLessOrEqual(x, nm / numberOfLayerToPulse, EPS);
                layer.AvN += Math.Max(0, nm / numberOfLayerToPulse - x);
            }
        }

        ///<summary>Uptake water by the root</summary>
        ///<param name="waterNeeded">Water needed by the root</param>
        private void WaterUptakeByRoots(double waterNeeded, double rootLength)
        {
            var actualWaterNeeded = waterNeeded;

            #region calculate excess water in root zone

            double excessWaterInRootZone = 0; // excess of water in root zone
            foreach (var layer in RootLayers)
            {
                excessWaterInRootZone += layer.ExWater;
            }

            #endregion

            #region calculate acutal water on leaves

            var actualWaterOnLeaves = ActualWaterOnLeaves;

            if (actualWaterOnLeaves >= waterNeeded)
            {
                ActualWaterOnLeaves = actualWaterOnLeaves - waterNeeded;
                waterNeeded = 0;
            }
            else
            {
                ActualWaterOnLeaves = 0;
                waterNeeded -= actualWaterOnLeaves;
            }

            #endregion

            // Plant transpired  water (WP)  is extracted from the root zone first from EXW bucket and then from AW

            // Extract water from EXW bucket (Actually should extract from both EXW and AW using the Kl function!!!)

            if (waterNeeded > 0)
            {
                if (excessWaterInRootZone > waterNeeded)
                {
                    foreach (var layer in RootLayers)
                    {
                        layer.ExWater -= layer.ExWater * waterNeeded / excessWaterInRootZone;
                    }
                    waterNeeded = 0;
                }
                else
                {
                    foreach (var layer in RootLayers)
                    {
                        layer.ExWater = 0;
                        
                    }
                    waterNeeded -= excessWaterInRootZone;

                    // Extract available water from AW bucket restricted by F() function
                    double currentDepth = 0;
                    StressFactor_.InitDayStep(CalculateRootZoneMaxAvailableWater(), CalculateRootZoneAvailableWater());

                    var dtf = StressFactor_.DTF;

                    foreach (var layer in RootLayers)
                    {
                        currentDepth += Layer.Thickness;

                        var k = RelativeWaterUptake(currentDepth - (Layer.Thickness / 2), rootLength);
                        var x = k * StressFactor_.DTF * layer.AvWater;

                        if (waterNeeded > x)
                        {
                            layer.AvWater -= x;
                            waterNeeded -= x;
                        }
                        else
                        {
                            layer.AvWater -= waterNeeded;
                            waterNeeded = 0;
                            break;
                        }
                    }
                }
            }

            // Calculate actual transpiration
            ///<Behnam (2015.10.27)>
            ///<Comment>To preserve water balance under unlimited water conditions</Comment>
            ///wAppLevel can be used to define a specific level of compensation for N deficiency</Comment>
            double wAppLevel = WCompensationLevel / 100;

            /// Behnam (2016.05.14): Now water is added to the soil instead of directly adding it to the crop.
            //if (isUnlimitedWater)
            if (false)
            {
                VirtualWReq += wAppLevel * waterNeeded;
                ActTransp = actualWaterNeeded - (1 - wAppLevel) * waterNeeded;
            }
            else
            {
                ActTransp = actualWaterNeeded - waterNeeded;
                //VirtualWReq = 0;
            }
            ///</Behnam>
        }

        ///Pool Ni mineralisation, f(T) function of the soil model.
        private double Ft(double meanWeekTemp)
        {
            var t = GetTemperature(meanWeekTemp);
            var x = Math.Exp(0.57 - 0.024 * t + 0.0020 * t * t) - Math.Exp(0.57 - 0.042 * t - 0.0051 * t * t);
            return Math.Max(0.0, x);
        }

        ///Pool Ni mineralisation, f(teta) function of the soil moisture.
        private double Fq()
        {
            if (Q <= QFC)
            {
                return Q / QFC;
            }
            return Math.Max(0.0, 1.0 - (Q - QFC) / (QS - QFC));
        }

        #endregion

        #region FinishDayStep

        ///<summary>Finish a day step</summary>
        public void FinishDayStep()
        {
            if(!isUnlimitedNitrogen) ApplyRemoveN();

            pastDailyIrrigation.Add(DailyIrrigation);
        }

        #endregion

        #region CalculateAvailable

        ///<summary>Calculate the quantity of Ni available for root uptake</summary>
        ///<returns></returns>
        private double CalculateAvailableNForRoots()
        {
            double avN = 0;

            foreach (var iter in RootLayers)
            {
                avN += iter.AvN + iter.ExN;
            }
            Check.IsPositiveOrZero(avN);
            return avN;
        }

        ///<summary>Get the amount of unavailable inorganic N for each layers upper than rootDepth</summary>
        ///<returns>Unavailable inorganic Ni</returns>
        private double CalculateUnavailableNForRoots()
        {
            double unavN = 0;
            foreach (var layer in RootLayers)
            {
                unavN += layer.UnavN;
            }
            Check.IsPositiveOrZero(unavN);
            return unavN;
        }

        ///<summary>Get the amout of available and excess water for each layers upper than rootDeph</summary>
        ///<returns>Available and excess water in root zone</returns>
        public double CalculateRootZoneAvailableExcessWater()
        {
            double avWexW = 0;

            foreach (var layer in RootLayers)
            {
                avWexW += layer.AvWater + layer.ExWater;
            }

            return avWexW;
        }

        ///<summary>Get the amout of available and excess N for each layers upper than rootDeph</summary>
        ///<returns>Available and excess nitrogen in root zone</returns>
        public double CalculateRootZoneAvailableExcessN()
        {
            double avWexW = 0;

            foreach (var layer in RootLayers)
            {
                avWexW += layer.AvN + layer.ExN;
            }

            return avWexW;
        }

        public double CalculateRootZoneMaxAvailableWater()
        {
            double totalAvW = 0;
            foreach (var layer in RootLayers)
            {
                totalAvW += layer.MaxAvWater;
            }
            return totalAvW;
        }

        //pm 13/09/2011, add Total mineral N in root zone
        public double CalculateRootZoneTotalN()
        {
            double result = 0;
            foreach (var layer in RootLayers)
            {
                result += layer.AvN + layer.ExN + layer.UnavN;
            }
            return result;
        }

        ///<summary>Get the amout of available nitrogen in the top 90 soil cm</summary>
        ///<returns>Available water in root zone</returns>
        public double CalculateZoneAvailableNitrogenTop90()
        {
            int nLayersTop90cm = Math.Min(Layers.Count, (int)(0.9 / Layer.Thickness));

            var LayersTop90cm = new VectorSegment<Layer, Layer>(Layers, 0, nLayersTop90cm);

            double avN = 0;

            foreach (var layer in LayersTop90cm)
            {
                avN += layer.AvN;
            }

            return avN;
        }

        ///<summary>Get the amout of excess nitrogen in the top 90 soil cm</summary>
        ///<returns>Available water in root zone</returns>
        public double CalculateZoneExcessNitrogenTop90()
        {
            int nLayersTop90cm = Math.Min(Layers.Count, (int)(0.9 / Layer.Thickness));

            var LayersTop90cm = new VectorSegment<Layer, Layer>(Layers, 0, nLayersTop90cm);

            double exN = 0;

            foreach (var layer in LayersTop90cm)
            {
                exN += layer.ExN;
            }

            return exN;
        }


        ///<summary>Get the amout of available water for each layers upper than rootDeph</summary>
        ///<returns>Available water in root zone</returns>
        public double CalculateRootZoneAvailableWater()
        {
            double avW = 0;

            foreach (var layer in RootLayers)
            {
                avW += layer.AvWater;
            }

            return avW;
        }

        ///<summary>Get the amout of available water in the top 90 soil cm</summary>
        ///<returns>Available water in root zone</returns>
        public double CalculateZoneAvailableWaterTop90()
        {
           int nLayersTop90cm= Math.Min(Layers.Count, (int)(0.9/ Layer.Thickness));

           var LayersTop90cm = new VectorSegment<Layer, Layer>(Layers, 0, nLayersTop90cm);

            double avW = 0;

            foreach (var layer in LayersTop90cm)
            {
                avW += layer.AvWater;
            }

            return avW;
        }

        ///<summary>Get the amout of excess water in the top 90 soil cm</summary>
        ///<returns>Available water in root zone</returns>
        public double CalculateZoneExcessWaterTop90()
        {
            int nLayersTop90cm = Math.Min(Layers.Count, (int)(0.9 / Layer.Thickness));

            var LayersTop90cm = new VectorSegment<Layer, Layer>(Layers, 0, nLayersTop90cm);

            double exW = 0;

            foreach (var layer in LayersTop90cm)
            {
                exW += layer.ExWater;
            }

            return exW;
        }

        public double CalculateRootZoneWaterDeficit()
        {
            double defW = 0;

            foreach (var layer in RootLayers)
            {
                defW += Math.Max(0.0, layer.MaxAvWater - (layer.AvWater + layer.ExWater));
            }

            return defW;
        }

        public double CalculateWaterDeficit()
        {
            double defW = 0;

            foreach (var layer in Layers)
            {
                defW += Math.Max(0.0, layer.MaxAvWater - (layer.AvWater + layer.ExWater));
            }

            return defW;
        }

        ///<summary>
        ///Get the accumulated deficit for layers upper than the depth parameter
        ///OUTPUT UNITS: g m-2 d-1
        ///</summary>
        ///<param name="maxDepth">The maximal depth to calculate Cumulative deficit</param>
        ///<returns>Cumulative water deficit</returns>
        public double CalculateAccumulatedDeficit(double maxDepth)
        {
            double result = 0;
            var deficitLayer
                = new VectorSegment<Layer, Layer>(Layers, 0, Math.Min(Layers.Count, (int)(maxDepth / Layer.Thickness)));

            foreach (var layer in deficitLayer)
            {
                result += layer.MaxAvWater - (layer.AvWater + layer.ExWater);
            }

            return (result > 0) ? result : 0;
        }

        ///<summary>Sum MAX_AVAILABLE_WATER over each layers</summary>
        ///<returns></returns>
        private double GetTotalMaxAvailableWater()
        {
            double totalAWResult = 0;
            foreach (var layer in Layers)
            {
                totalAWResult += layer.MaxAvWater;
            }
            return totalAWResult;
        }

        ///<summary>Get the total available N in the soil</summary>
        ///<returns></returns>
        public double CalculateAvailableN()
        {
            double result = 0;
            foreach (var layer in Layers)
            {
                result += layer.AvN;
            }
            return result;
        }

        ///<summary>Get the total excess N in the soil</summary>
        ///<returns></returns>
        public double CalculateExcessN()
        {
            double result = 0;
            foreach (var layer in Layers)
            {
                result += layer.ExN;
            }
            return result;
        }

        ///<summary>Get the total available and excess N in the soil</summary>
        ///<returns></returns>
        public double CalculateAvailableExcessN()
        {
            double result = 0;
            foreach (var layer in Layers)
            {
                result += layer.AvN + layer.ExN;
            }
            return result;
        }

        ///<summary>Get the total unvailable N in the soil</summary>
        ///<returns>Total unavailable N in the soil</returns>
        public double CalculateUnavailableN()
        {
            double result = 0;
            foreach (var layer in Layers)
            {
                result += layer.UnavN;
            }
            return result;
        }

        ///<summary>Actual total N in the soil</summary>
        ///<returns>Total soil N</returns>
        public double CalculateTotalN()
        {
            double result = 0;
            foreach (var layer in Layers)
            {
                result += layer.AvN + layer.ExN + layer.UnavN;
                
            }
            return result;
        }

        ///<summary>Actual total N in the soil between two depths</summary>
        ///<returns>Total soil N</returns>
        public double CalculateTotalNBetweenTwoDepth(double topLayerDepth, double bottomLayerDepth)
        {
            double result = 0;
            //the layer are ordered from the closest of the surface to the deepest and have all the same height 
            for (int i = 0; i < Layers.Count; i++ )
            {
                //if the layer is between the boundaries 
                if (topLayerDepth<(i*Layer.Thickness) && bottomLayerDepth>((i+1)*Layer.Thickness))
                {
                    result += Layers[i].AvN + Layers[i].ExN + Layers[i].UnavN;
                }
                else
                {   //take a proportion
                    if (topLayerDepth < (i*Layer.Thickness) &&  bottomLayerDepth<((i+1)*Layer.Thickness)) 
                    {
                        double proportion = ((bottomLayerDepth - topLayerDepth) - (((i * Layer.Thickness) - topLayerDepth))) / Layer.Thickness;
                        proportion = Math.Max(0, proportion); //remove the case when proportion is negative ( bottom layer is above i*Layer.thickness)

                        result += (Layers[i].AvN + Layers[i].ExN + Layers[i].UnavN)*proportion;
                    }
                    else
                    {
                        //take a proportion
                        if (topLayerDepth > (i*Layer.Thickness) &&  bottomLayerDepth>((i+1)*Layer.Thickness))
                        {
                            double proportion = ((bottomLayerDepth - topLayerDepth) - (bottomLayerDepth - (i+1)*Layer.Thickness)) / Layer.Thickness;
                            proportion = Math.Max(0, proportion); //remove the case when proportion is negative ( top layer is below (i+1)*Layer.thickness)

                            result += (Layers[i].AvN + Layers[i].ExN + Layers[i].UnavN) * proportion;
                        }
                        else// topLayerDepth > (i*Layer.Thickness) &&  bottomLayerDepth<((i+1)*Layer.Thickness)
                        {
                            double proportion = Math.Max(0, (bottomLayerDepth - topLayerDepth));
                                result += (Layers[i].AvN + Layers[i].ExN + Layers[i].UnavN) * proportion;
                        }
                            
                    }
                }
            }

            return result;
        }

        ///<summary>Get total available Water in the soil</summary>
        ///<returns></returns>
        public double CalculateAvailableW()
        {
            double result = 0;
            foreach (var layer in Layers)
            {
                result += layer.AvWater;
            }
            return result;
        }

        ///<summary>Get the total excess Water in the soil</summary>
        ///<returns></returns>
        public double CalculateExcessW()
        {
            double result = 0;
            foreach (var layer in Layers)
            {
                result += layer.ExWater;
            }
            return result;
        }

        ///<summary>Get the total available and excess Water in the soil</summary>
        ///<returns></returns>
        public double CalculateAvailableExcessW()
        {
            double result = 0;
            foreach (var layer in Layers)
            {
                result += layer.AvWater + layer.ExWater;
            }
            return result;
        }

        ///<summary>Actual total N in the soil between two depths</summary>
        ///<returns>Total soil N</returns>
        public double CalculateAvailableExcessWBetweenTwoDepth(double topLayerDepth, double bottomLayerDepth)
        {
            double result = 0;
            //the layer are ordered from the closest of the surface to the deepest and have all the same height 
            for (int i = 0; i < Layers.Count; i++)
            {
                //if the layer is between the boundaries 
                if (topLayerDepth < (i * Layer.Thickness) && bottomLayerDepth > ((i + 1) * Layer.Thickness))
                {
                    result += Layers[i].AvWater + Layers[i].ExWater;
                }
                else
                {   //take a proportion
                    if (topLayerDepth < (i * Layer.Thickness) && bottomLayerDepth < ((i + 1) * Layer.Thickness))
                    {
                        double proportion = ((bottomLayerDepth - topLayerDepth) - (((i * Layer.Thickness) - topLayerDepth))) / Layer.Thickness;
                        proportion = Math.Max(0, proportion); //remove the case when proportion is negative ( bottom layer is above i*Layer.thickness)

                        result += (Layers[i].AvWater + Layers[i].ExWater) * proportion;
                    }
                    else
                    {
                        //take a proportion
                        if (topLayerDepth > (i * Layer.Thickness) && bottomLayerDepth > ((i + 1) * Layer.Thickness))
                        {
                            double proportion = ((bottomLayerDepth - topLayerDepth) - (bottomLayerDepth - (i + 1) * Layer.Thickness)) / Layer.Thickness;
                            proportion = Math.Max(0, proportion); //remove the case when proportion is negative ( top layer is below (i+1)*Layer.thickness)

                            result += (Layers[i].AvWater + Layers[i].ExWater) * proportion;
                        }
                        else// topLayerDepth > (i*Layer.Thickness) &&  bottomLayerDepth<((i+1)*Layer.Thickness)
                        {
                            double proportion = Math.Max(0, (bottomLayerDepth - topLayerDepth));
                            result += (Layers[i].AvWater + Layers[i].ExWater) * proportion;
                        }

                    }
                }
            }

            return result;
        }

        ///<summary>Get the total unvailable Water in the soil</summary>
        ///<returns>The total unavailable Water in the soil</returns>
        public double CalculateUnavailableW()
        {
            double result = 0;
            foreach (var layer in Layers)
            {
                result += layer.UnavWater;
            }
            return result;
        }

        ///<summary>Actual total Water in the soil</summary>
        ///<returns>Total soil Water</returns>
        public double CalculateTotalW()
        {
            double result = 0;
            foreach (var layer in Layers)
            {
                result += layer.AvWater + layer.ExWater + layer.UnavWater;
            }
            return result;
        }

        #endregion

        #region CalculateEvaporation

        ///<summary>
        ///Soil evaporation 
        ///OUTPUT UNITS: g m-2 d-1 
        /// </summary>
        ///<returns></returns>
        private double CalculateEvaporation()
        {
           ///return Math.Min(SloslResult, PtSoilResult);
            return Math.Min(energyBalanceBiomaWrapper_.DiffusionLimitedEvaporation_, energyBalanceBiomaWrapper_.EnergyLimitedEvaporation_); 
        }

        #endregion

        #region RemoveN

        ///<summary>Remove Ni from soil (increment only the amount of Ni to remove at the beginning of the next day)</summary>
        ///<param name="neededN">Quantity of Ni to remove from soil</param>
        public void RemoveN(double neededN)
        {
            NtoRemove += neededN;
            NavForRoot -= neededN;
            if (NavForRoot < 0 && NavForRoot > -EPS) { NavForRoot = 0; }
            Check.IsPositiveOrZero(NavForRoot);
        }

        ///<summary>Realy remove Ni from soil (method called once a day)</summary>
        private void ApplyRemoveN()
        {
            if (NtoRemove > 0)
            {
                double nRemoved = 0;
                foreach (var layer in RootLayers)
                {
                    if (NtoRemove <= DailyAvNforRoots)
                    {
                        var nRemovedFromAvN = Math.Min(layer.AvN, layer.AvN * NtoRemove / DailyAvNforRoots);
                        Check.IsNumber(nRemovedFromAvN);
                        Check.IsPositiveOrZero(nRemovedFromAvN);
                        layer.AvN -= nRemovedFromAvN;

                        var nRemovedFromExN = Math.Min(layer.ExN, layer.ExN * NtoRemove / DailyAvNforRoots);
                        Check.IsNumber(nRemovedFromExN);
                        Check.IsPositiveOrZero(nRemovedFromExN);
                        layer.ExN -= nRemovedFromExN;

                        nRemoved += nRemovedFromAvN + nRemovedFromExN;
                    }
                    else
                    {
                        var nRemovedFromAvN = layer.AvN;
                        Check.IsNumber(nRemovedFromAvN);
                        Check.IsPositiveOrZero(nRemovedFromAvN);
                        layer.AvN -= nRemovedFromAvN;

                        var nRemovedFromExN = layer.ExN;
                        Check.IsNumber(nRemovedFromExN);
                        Check.IsPositiveOrZero(nRemovedFromExN);
                        layer.ExN -= nRemovedFromExN;

                        nRemoved += nRemovedFromAvN + nRemovedFromExN;
                    }
                }
                Check.IsEqual(NtoRemove, nRemoved, 0.0001);
            }

            NtoRemove = 0;
        }


        #endregion

        #region Temperature


        ///<summary>Soil temperature (°C) used to calculated soil N mineralisation </summary>
        ///<returns></returns>
        private double GetTemperature(double meanWeekTemp)
        {
            return 0.66 + 0.93 * meanWeekTemp; 
        }



        #endregion

        #region Irrigate/Fertilize

        ///<summary>Irrigate the soil</summary>
        ///<param name="waterAdd">Water to add to the soil</param>
        public void Irrigate(double waterAdd)
        {
            DailyIrrigation += waterAdd;
            IncomeWater += waterAdd;
            UndisolvedIrrigation += waterAdd;
        }

        ///<summary>Fertilize the soil (call doFertilize method</summary>
        ///<param name="managerAdd">Inorganic N to add to the soil</param>
        public void Fertilize(double managerAdd)
        {
            UndisolvedFertilisation += managerAdd;
            CumulativeNfertilisation += managerAdd;
        }

        ///<summary>Get soil irrigation accumulated over n days</summary>
        ///<returns>Cumulated irrigation from the previous nbDays</returns>
        private double GetAccumulatedIrrigation(int nbDay)
        {
            var result = DailyIrrigation;
            for (var i = 1; i < nbDay; ++i)
            {
                int length= pastDailyIrrigation.Count;
                if (length - i >= 0)
                {
                    result += pastDailyIrrigation[(length - i)];
                }
            }
            return result;
        }

        #endregion

        #region Depth

        ///<summary>Get the total soil depth</summary>
        ///<returns>Soil depth</returns>
        public double SoilDepth
        {
            get { return (Layers.Count) * Layer.Thickness; }
        }

        #endregion

        #region Relative uptake

        ///<summary>Get the relative water of the root at a given depth</summary>
        ///<param name="depth">Depth at which it calculates the relative water uptake</param>
        ///<returns></returns>
        public double RelativeWaterUptake(double depth, double rootLength)
        {
            return depth <= rootLength ? MaxRWU - BetaRWU * (depth / SoilDepth) : 0;
        }

        #endregion

        public override void Dispose()
        {
            base.Dispose();
            if (Layers != null)
            {
                Layers.Dispose();
                Layers = null;
            }
            if (RootLayers != null)
            {
                RootLayers.Dispose();
                RootLayers = null;
            }
            SoilDeepLayer_ = null;
            if (StressFactor_ != null)
            {
                StressFactor_.Dispose();
                StressFactor_ = null;
            }
        }

    }
}