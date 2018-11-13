using System;
using System.Collections.Generic;
using SiriusModel.Model.Base.Link;
using SiriusModel.Model.CropModel;
using SiriusModel.Model.Observation;
using SiriusModel.Model.SoilModel;
using SiriusModel.Model.Meteo.Meteorology;
using SiriusModel.InOut;
using SiriusModel.Model.Phenology;
using SiriusModel.Model.Meteo;
using SiriusModel.Model.SoilModel.EnergyBalance;
using SiriusModel.Model.ThermalTime;
//



namespace SiriusModel.Model
{
    /// <summary>
    /// The class Universe is the main compartment of the simulation.
    /// </summary>
    public class Universe : RunLink
    {

        #region fields
        /// <summary>
        /// calculateDailyThermalTime strategy
        /// </summary>
        public ThermalTimeBiomaWrapper thermalTimeWrapper_;
        /// <summary>
        /// Bioma Meteorology composite strategy
        /// </summary>
        public MeteoBiomaWrapper meteorologyWrapper_;

        public ShootTemperature ShootTemperature_;

       public int PrevAppliedStagesIrr = 1;
       public int PrevAppliedStagesN = 1;

        public bool IsSowDateEstimate;
        public bool isUnlimitedWater;
        public bool isUnlimitedNitrogen;
        public bool isUnlimitedTemperature;
        public bool isInteractionsWNT;

        /// Amount of water added with each application of N, mm.
        const double WaterAppWithNApp = 5;

        /// Change in N fertilisation based on a pre-defined annual trend.
        public double NFertChange = 1;

        /// Number of days after sowing.
        public int GrowthDay;

        /// Cumulative air temperature from sowing.
        public double CumAirTempFromSowing;

        /// Cumulative maximum canopy temperature from sowing.
        public double CumMaxCanopyTempFromSowing;

        /// Cumulative maximum air temperature from sowing.
        public double CumMaxAirTempFromSowing;

        //true for Maize and false for Wheat 
        public bool switchMaize;

        #endregion
        #region properties  
		        ///<summary>
        ///Get the current date of this Universe.
        ///</summary>
        public DateTime CurrentDate { get; set; }
        


        ///<summary>
        ///The crop of the universe.
        ///</summary>
        public Crop Crop_ {  get;  set; }

        ///<summary>
        ///The soil of the universe.
        ///</summary>
        public Soil Soil_ { get;  set; }

        ///<summary>
        ///The weather controler of this run.
        ///</summary>
        public Weather Weather_ { get; set; }

        public csMTG.Gramene mtg_ { get; set; }
        

        #endregion 

        #region output

        public double NuseEfficiency { get { return Crop_.GrainTotalDM / (Soil_.CalculateTotalN() + Soil_.LostNitrogen + Crop_.CropTotalN); } }
        public double NuptakeEfficiency {get {return Crop_.CropTotalN/ (Soil_.CalculateTotalN() + Soil_.LostNitrogen + Crop_.CropTotalN); } }
        public double WaterUseEfficiency { get { return 10 * Crop_.GrainTotalDM / (Soil_.AccumulatedActEvapoTranspiration / Run.MMwaterToGwater); } }
        public double NAdded = 0;

        #endregion
        ///<summary>
        ///Default constructor.
        ///</summary>
        ///<param name="runOld">The run of this universe.</param>
	    public Universe(Run runOld) : base(runOld)
	    {
            Weather_ = null;
		    Crop_ = null;
		    Soil_ = null;
            thermalTimeWrapper_ = null;
            meteorologyWrapper_ = null;
			ShootTemperature_ = null;

            mtg_ = new csMTG.Gramene();
            mtg_.CreateBasicWheat(0);

	    }

	    ///<summary>
	    ///Copy constructor.
	    ///</summary>
	    ///<param name="runOld">The run of this universe.</param>
	    ///<param name="toCopy">The universe to copy.</param>
        ///<param name="copyAll">false copy only the outputs</param>
        public Universe(Run runOld, Universe toCopy, bool copyAll)
            : base(runOld)
        {
            switchMaize = toCopy.switchMaize;
            CurrentDate = toCopy.CurrentDate;
            Crop_ = (toCopy.Crop_ != null) ? new Crop(this, toCopy.Crop_, copyAll) : null;
            Soil_ = (toCopy.Soil_ != null) ? new Soil(this, toCopy.Soil_, copyAll) : null;
            thermalTimeWrapper_ = (toCopy.thermalTimeWrapper_ != null) ? new ThermalTimeBiomaWrapper(this, toCopy.thermalTimeWrapper_, copyAll) : null;
            meteorologyWrapper_ = (toCopy.meteorologyWrapper_ != null) ? new MeteoBiomaWrapper(this, toCopy.meteorologyWrapper_, copyAll) : null;
            ShootTemperature_ = (toCopy.ShootTemperature_ != null) ? new ShootTemperature(this, toCopy.ShootTemperature_) : null;            
            PrevAppliedStagesIrr = toCopy.PrevAppliedStagesIrr;
            PrevAppliedStagesN = toCopy.PrevAppliedStagesN;
            NFertChange = toCopy.NFertChange;
            GrowthDay = toCopy.GrowthDay;
            CumMaxCanopyTempFromSowing = toCopy.CumMaxCanopyTempFromSowing;
            CumMaxAirTempFromSowing = toCopy.CumMaxAirTempFromSowing;
            CumAirTempFromSowing = toCopy.CumAirTempFromSowing;
            mtg_ = (toCopy.mtg_ != null) ? new csMTG.Gramene(toCopy.mtg_, copyAll) : null;

            if (copyAll)
            {
                Weather_ = toCopy.Weather_;
                IsSowDateEstimate = toCopy.IsSowDateEstimate;
                isUnlimitedWater = toCopy.isUnlimitedWater;
                isUnlimitedNitrogen = toCopy.isUnlimitedNitrogen;
                isUnlimitedTemperature = toCopy.isUnlimitedTemperature;
                isInteractionsWNT = toCopy.isInteractionsWNT;
                CurrentDate = toCopy.CurrentDate;
                
            }
            NAdded = toCopy.NAdded;

        }
		        /// <summary>
        /// Init the universe for a new run.
        /// </summary>
	    public void Init( SiteItem site, ManagementItem management)
	    {
            Weather_ = new Weather(this);
		    Crop_ = new Crop(this);
            Soil_ = new Soil(this);
            thermalTimeWrapper_ = new ThermalTimeBiomaWrapper(this);
            ShootTemperature_ = new ShootTemperature(this);

            meteorologyWrapper_ = new MeteoBiomaWrapper(this);

            Weather_.Init(site, management);
            CurrentDate = management.FinalSowingDate.AddDays(-1);
            Soil_.Init(Weather_.MinTemp(CurrentDate), Weather_.MaxTemp(CurrentDate), Weather_.GetMeanTemperatureBeforeSowing());
            Crop_.Init(thermalTimeWrapper_.CumulTT, CurrentDate.AddDays(1));

            CalcChangeinNFertilisation();
            GrowthDay = 0;
            CumAirTempFromSowing = 0;
            CumMaxCanopyTempFromSowing = 0;
            CumMaxAirTempFromSowing = 0;
            if (management.Species == "Maize") 
            { switchMaize = true; }
            else { switchMaize = false; }
        }

        /// <summary>
        /// Element a day step on this universe.
        /// </summary>
        /// 
        
        public void RunDayStep()
        {

            ///<Behnam (2016.01.07)>
            ///<Comment>To apply air temperature cut only on canopy temperature, instead of air temperature.
            ///Old setting will be preserved, just in case in the future it is required to work on air temperature.
            ///But the Boolean flag is now applied on canopy temperature.
            ///Weather_.isUnlimitedTemperature = isUnlimitedTemperature;
            Weather_.isUnlimitedTemperature = false;         
            ///</Behnam>
            Soil_.isUnlimitedWater = Crop_.isUnlimitedWater = isUnlimitedWater;
            Soil_.isUnlimitedNitrogen = Crop_.isUnlimitedNitrogen = isUnlimitedNitrogen;
            Soil_.isUnlimitedTemperature = Crop_.isUnlimitedTemperature = isUnlimitedTemperature;

            ///<Comment>Cumulative air temperature from sowing (to calculate mean air temperature for different periods)</Comment>
            if (GrowthDay > 0) CumAirTempFromSowing = GrowthDay * Weather_.MeanTemp(CurrentDate, GrowthDay);
            ///</Behnam>
            ///Loic, Cumulative maximum air temperature from sowing 
            if (GrowthDay > 0) CumMaxAirTempFromSowing += Weather_.MaxTemp(CurrentDate);
            ///Loic, Cumulative maximum canopy temperature from sowing
            if (GrowthDay > 0) CumMaxCanopyTempFromSowing += Soil_.MaximumCanopyTemperature;

            Soil_.InitDayStep(Crop_.RootLength);
            Soil_.MeanAirTemperature = Weather_.MeanTemp(CurrentDate);
            
            ///<Behnam (2016.01.19)>
            ///<Comment>Trying to organize the Universe class, contents were moved to two new methods
            ///With each application of N, WaterAppWithNApp mm of water is added, if needed</Comment>            
            double TotalWApplied = 0;
            Soil_.Irrigate(Weather_.Rain(CurrentDate));
            TotalWApplied = Weather_.Rain(CurrentDate);

            NAdded = 0;

            double TotalNApplied = 0;
            TotalNApplied = ApplyResources_ByDate(TotalNApplied, ref TotalWApplied);
            TotalNApplied = ApplyResources_ByGrowthStage(TotalNApplied, ref TotalWApplied);

            ///</Behnam>

            Crop_.InitDayStep(thermalTimeWrapper_.getDeltaTT(Delta.Remobilization));

            double vp;
            if (!Weather_.IsWindAndVpDefined())
            {
                // from the formulation of Murray 1967, Journal of Aplplied Meteorology 1:203-204
                // The minimum air temperature is used as a proxy for dew temperature
                vp = (0.6108 * Math.Exp((17.27 * Weather_.MinTemp(CurrentDate)) / (Weather_.MinTemp(CurrentDate) + 237.3))) * 10; // multiplied by 10 to convert kPa to hPa
            }
            else
            {
                vp = Weather_.Vp(CurrentDate);
            }


            // Energy Balance
            // Boundary layer conductance
            // OUTPUT UNIT: m d-1
            double wind;
            if (Weather_.IsWindAndVpDefined() == false)
            {
                wind = 240 * 1000;                          // Assumes wind = 240 km/day converted to m/day
            }
            else
            {
                const double minWind = 100.0;
                wind = Math.Max(Weather_.Wind(CurrentDate), minWind) * 1000;
            }
            //

            meteorologyWrapper_.EstimateMeteo(Weather_.MeanTemp(CurrentDate), Weather_.MinTemp(CurrentDate), Weather_.MinTemp(CurrentDate.AddDays(1)), Weather_.MaxTemp(CurrentDate), Weather_.MinTemp(CurrentDate.AddDays(-1)), ShootTemperature_.MinShootTemperature, ShootTemperature_.MaxShootTemperature, vp, Weather_.Rad(CurrentDate), wind);
            
            Soil_.RunDayStep(meteorologyWrapper_.RadTopAtm,
                              Weather_.IsWindAndVpDefined(),
                              vp,
                              Weather_.Rad(CurrentDate),
                              Weather_.MinTemp(CurrentDate),
                              Weather_.MaxTemp(CurrentDate),
                              Weather_.MeanTemp(CurrentDate),
                              meteorologyWrapper_.HourlyAirTemperature,
                              wind,
                              Weather_.WeekTemp(CurrentDate),
                              meteorologyWrapper_.HSlope,
                              meteorologyWrapper_.VPDair,
                              Crop_.Tau,
                              Crop_.SumInternodesLength,
                              Crop_.PotentialWaterOnLeaves,
                              Crop_.RootLength,
                              Crop_.LeafNumber,
                              meteorologyWrapper_.HourlyVPDAir,
                              meteorologyWrapper_.HourlyRadiation,
                              Crop_.Ntip,
                              meteorologyWrapper_.RH
                              );


            ShootTemperature_.isUnlimitedTemperature = isUnlimitedTemperature;
            ShootTemperature_.Estimate(Crop_.LeafNumber, Soil_.MinimumCanopyTemperature, Soil_.MaximumCanopyTemperature,Soil_.HourlyCanopyTemperature,
                Soil_.SoilMinTemperature, Soil_.SoilMaxTemperature, Soil_.HourlySoilTemperature);

            Crop_.InitDayStepRoot(thermalTimeWrapper_.getCumulTT(Delta.Remobilization), Soil_.NavForRoot);

            //Quantity of Ni to remove from soil
            double soilNinc = 0;

            ///<Behnam (2016.01.08)>
            ///<Comment>Directly using shoot temperature instead of checking leaf number
            /// and selecting between soil and canopy temperature</Comment>
            /// 
            thermalTimeWrapper_.EstimateDailyThermalTime(Weather_.MinTemp(CurrentDate), Weather_.MaxTemp(CurrentDate),
                Soil_.SoilMinTemperature, Soil_.SoilMaxTemperature, ShootTemperature_.MinShootTemperature,
                ShootTemperature_.MaxShootTemperature, ShootTemperature_.ShootHourlyTemperature, meteorologyWrapper_.HourlyAirTemperature, Crop_.getPhaseValue());
            ///</Behnam>

            soilNinc = Crop_.Grow(meteorologyWrapper_.RadTopAtm,
                thermalTimeWrapper_.CumulTT,
                thermalTimeWrapper_.getDeltaTT(Delta.Shoot),
                thermalTimeWrapper_.getDeltaTT(Delta.PhenoMaize),
                thermalTimeWrapper_.getDeltaTT(Delta.Remobilization),
                thermalTimeWrapper_.getDeltaTT(Delta.LeafSenescence),
                ShootTemperature_.MinShootTemperature,
                ShootTemperature_.MaxShootTemperature,
                Weather_.Rad(CurrentDate),
                Weather_.PAR(CurrentDate),
                Soil_.DBF,
                Soil_.SoilDepth,
                Soil_.DEBF,
                Soil_.DGF,
                meteorologyWrapper_.DayLength,
                Soil_.FPAW, meteorologyWrapper_.VPDairCanopy, Soil_.HourlyCanopyTemperature, Soil_.VPDeq);

            if (!isUnlimitedNitrogen) Soil_.RemoveN(soilNinc);

            Soil_.FinishDayStep();

        }

        public double ApplyResources_ByDate(double TotalNApplied, ref double TotalWApplied)
        {
            double NApplied = 0;
            double NNIThreshold = Run.ManagementDef.NNIThreshold;
            double NNIMultiplier = Run.ManagementDef.NNIMultiplier;

            var dateApp = Run.ManagementDef[CurrentDate];

            ///<Behnam (2015.10.20)>
            ///<Comment>To enable using total N fertisation and application shares at each event.
            ///<Behnam (2016.01.11)>
            ///Also a trend is applied, if applicable.</Comment>

            if (dateApp != null)
            {
                //if (!isUnlimitedWater) Soil_.Irrigate(dateApp.Water);
                Soil_.Irrigate(dateApp.Water);
                TotalWApplied += dateApp.Water;

                if (!isUnlimitedNitrogen)
                {
                    if (Run.ManagementDef.IsTotalNitrogen)
                    {
                        NApplied = NFertChange * dateApp.Nitrogen * Run.ManagementDef.TotalNApplication / 100;
                    }
                    else
                    {
                        NApplied = NFertChange * dateApp.Nitrogen;
                    }

                    ///<Comment>If NNI is to be used, the amount of N application is calculated by CalcNFertilisation</Comment>
                    if (Run.ManagementDef.IsNNIUsed && !isUnlimitedNitrogen)
                    {
                        NApplied = CalcNFertilisation(NNIThreshold, NNIMultiplier, TotalNApplied, NApplied);
                    }

                    /// Behnam (2016.05.14): Adding WaterAppWithNApp mm of water for each N application, if needed.
                    if (NApplied > 0 && TotalWApplied < WaterAppWithNApp * Run.MMwaterToGwater) {
                        var WaterNeeded = WaterAppWithNApp * Run.MMwaterToGwater - TotalWApplied;
                        Soil_.Irrigate(WaterNeeded);
                        TotalWApplied += WaterNeeded;
                    }
                    NAdded = NApplied;
                    Soil_.Fertilize(NApplied);
                }
                TotalNApplied += NApplied;
            }
            return TotalNApplied;
            ///</Behnam>
        }

        public double ApplyResources_ByGrowthStage(double TotalNApplied, ref double TotalWApplied)
        {
            var gs = 0;
            foreach (var growthStageApp in Run.ManagementDef.GrowthStageApplications)
            {
                ///<Behnam (2015.10.22)>
                ///<Comment>
                ///Keep in mind, N and irrigation are triggered at least one day after the specified
                ///growth stage, but as they are applied at the begining of the simulation of each day,
                ///it is like they are applied one day earlier.
                ///</Comment>

                double NApplied = 0;
                double NNIThreshold = Run.ManagementDef.NNIThreshold;
                double NNIMultiplier = Run.ManagementDef.NNIMultiplier;

                gs += 1;
                var dateMoment = Run.CurrentUniverse.Crop_.getDateOfStage(growthStageApp.GrowthStage);

                ///<Comment>Don't apply irrigation if this irrigation event has already been triggered</Comment>
                if (PrevAppliedStagesIrr == gs)
                {
                    if (dateMoment.HasValue && dateMoment.Value.AddDays(1) == CurrentDate)
                    {
                        //if (!isUnlimitedWater) Soil_.Irrigate(growthStageApp.Water);
                        Soil_.Irrigate(growthStageApp.Water);
                        PrevAppliedStagesIrr += 1;
                        TotalWApplied += growthStageApp.Water;
                    }
                }

                ///<Comment>Don't apply N if this N application event has already been triggered</Comment>
                if (PrevAppliedStagesN == gs)
                {
                    if (dateMoment.HasValue)
                    {
                        bool apply = true;
                        if (Run.ManagementDef.IsCheckPcpN)
                        {
                            ///<Comment>Check for cumulative precipitation over CheckDaysPcpN coming days, if applicable</Comment>
                            double cumpcp = Weather_.CumRainMM(CurrentDate, Run.ManagementDef.CheckDaysPcpN, false);

                            ///<Comment>Apply N if precipitation criterion is met or MaxPostponeN is reached</Comment>
                            apply = (cumpcp >= Run.ManagementDef.CumPcpThrN ||
                                dateMoment.Value.AddDays(Run.ManagementDef.MaxPostponeN) <= CurrentDate);
                        }

                        if (apply)
                        {
                            if (!isUnlimitedNitrogen)
                            {
                                ///<Comment>To enable using total N fertisation and application shares at each event</Comment> 
                                if (Run.ManagementDef.IsTotalNitrogen)
                                {
                                    NApplied = NFertChange * growthStageApp.Nitrogen * Run.ManagementDef.TotalNApplication / 100;
                                }
                                else
                                {
                                    NApplied = NFertChange * growthStageApp.Nitrogen;
                                }

                                ///<Comment>If NNI is to be used, the amount of N application is calculated by CalcNFertilisation</Comment>
                                if (Run.ManagementDef.IsNNIUsed && !isUnlimitedNitrogen)
                                {
                                    NApplied = CalcNFertilisation(NNIThreshold, NNIMultiplier, TotalNApplied, NApplied);
                                }

                                /// Behnam (2016.05.14): Adding WaterAppWithNApp mm of water for each N application, if needed.
                                if (NApplied > 0 && TotalWApplied < WaterAppWithNApp * Run.MMwaterToGwater)
                                {
                                    var WaterNeeded = WaterAppWithNApp * Run.MMwaterToGwater - TotalWApplied;
                                    Soil_.Irrigate(WaterNeeded);
                                    TotalWApplied += WaterNeeded;
                                }
                                NAdded = NApplied;
                                Soil_.Fertilize(NApplied);
                            }
                            TotalNApplied += NApplied;
                            PrevAppliedStagesN += 1;
                        }
                    }
                }
            }
            return TotalNApplied;
            ///</Behnam>
        }

        ///<Behnam (2016.01.11)>
        ///<Comment>Calculates the change in N fertilisation based on a pre-defined annual trend</Comment>        
        public void CalcChangeinNFertilisation()
        {
            if (Run.ManagementDef.IsNTrendApplied)
            {
                double CurrYear = Run.ManagementDef.FinalSowingDate.Year;
                double BaseYear = Run.ManagementDef.NTrendBaseYear;
                NFertChange = 1 + (CurrYear - BaseYear) * Run.ManagementDef.NTrendSlope / 100;
            }
            else NFertChange = 1;
        }

        ///<Behnam (2016.01.13)>
        ///<Comment>Calculates N required based on NNI and the amount already applied</Comment>        
        public double CalcNFertilisation(double NNIThreshold, double NNIMultiplier, double TotalNApplied, double UserNValue)
        {
            double need = 0;
            double nni = Crop_.NNI;
            if (nni < NNIThreshold)
            {
                if (UserNValue > 0)
                {
                    need = UserNValue;
                }
                else
                {
                    double cropN = Crop_.ShootGreenN;
                    double cropDM = Crop_.ShootGreenDM;
                    double ncrop = 100.0 * cropN / cropDM;
                    double ncrit = Crop_.ShootNCrit;
                    if (ncrop < ncrit) need = (ncrit - ncrop) * cropDM / 100;
                    need = Math.Max(0, NNIMultiplier * need - TotalNApplied);
                }
            }
            return need;
        }
        ///</Behnam>

		public override void Dispose()
        {
            base.Dispose();
           
            if (Crop_ != null)
            {
                Crop_.Dispose();
                Crop_ = null;
            }
            if (Soil_ != null)
            {
                Soil_.Dispose();
                Soil_ = null;
            }
        }
    }
}


                          