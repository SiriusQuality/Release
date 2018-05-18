using System;
using System.Collections.Generic;
using System.Linq;
using SiriusModel.InOut;
using SiriusModel.InOut.OutputWriter;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SiriusModel.Model
{
    ///<summary>
    ///The run class contains the definition of a single run simulation.
    ///</summary>
    [Serializable]
    public class Run
    {
        #region Constants

        public static readonly double MMwaterToGwater = 1000.0;
        public static readonly double GwaterToMMwater = 0.001;
        public static readonly double MToMM = 1000.0;
        public static readonly double MMToM = 0.001;
        public static readonly double MToCM = 100.0;
        public static readonly double CMToM = 0.01;

        ///<Behnam>
        internal static bool isFirstYear = false;
        internal static bool SecondLine = false;

        public DateTime SowingDate;
        private DateTime MinSowDate;
        private DateTime MaxSowDate;

        private bool IsSowDateEstimate = false;
        // private bool UseActualBase = false;
        private bool DoInteractions = false;
        private bool InteractionsW = false;
        private bool InteractionsN = false;
        private bool InteractionsT = false;
        private bool InteractionsWN = false;
        private bool InteractionsWT = false;
        private bool InteractionsNT = false;
        private bool InteractionsWNT = false;

        private int SowingWindowType = 0;
        private bool flagSkipDays = true;
        private bool flagFreezing = true;
        private bool flagSoilMois = true;
        private bool flagSoilTemp = true;
        private bool flagCumPcp = true;
        private bool flagWorkabil = true;
        private bool flagMaxDate = true;
        private bool flagReSow = true;
        private int checkLay = 2;
        ///</Behnam>

        #endregion

        #region Fields

        ///<summary>
        ///Dictionary (paramKey, paramValue). All parameters are record here 
        ///</summary>
        private Dictionary<string, double> parameters;

        ///<summary>
        ///The genotypeDef value used for this run.
        ///</summary>
        private CropParameterItem varietyDef;

        ///<summary>
        ///The soilDef value used for this run.
        ///</summary>
        private SoilItem soilDef;

        ///<summary>
        ///The siteDef value used for this run.
        ///</summary>
        private SiteItem siteDef;

        ///<summary>
        ///The managementDef used for this run.
        ///</summary>
        private ManagementItem managementDef;

        ///<summary>
        ///The parameter set used for this run.
        ///</summary>
        private CropParameterItem parametersDef;

        ///<summary>
        ///The run options set used for this run.
        ///</summary>
        private RunOptionItem runOptionsDef;

        ///<Behnam (2015.10.28)>
        ///<Comment>Adding parallel universes for analyzing stress indices</Comment>
        ///<summary>
        ///The current Universe of the simulation. This Universe is saved every day step.
        ///</summary>
        private Universe currentUniverse;
        ///</Behnam>

        ///<summary>
        ///The collection of copy of currentUniverse made every day step.
        ///</summary>
        private RawData<Universe> savedUniverses;


        ///<summary>
        ///The weather of this run.
        ///</summary>
        //private Weather weather;

        #endregion

        #region Parameters

        ///<summary>
        ///Get the parameters Dictionary for this run
        ///</summary>
        public Dictionary<string, double> Parameters
        {
            get
            {
                return parameters;
            }
        }

        ///<summary>
        ///Get the genotypeDef definition for this run.
        ///</summary>
        public CropParameterItem VarietyDef
        {
            get { return varietyDef; }
        }

        ///<summary>
        ///Get the soilDef definition for this run.
        ///</summary>
        public SoilItem SoilDef
        {
            get { return soilDef; }
        }

        ///<summary>
        ///Get the siteDef defintion for this run.
        ///</summary>
        public SiteItem SiteDef
        {
            get { return siteDef; }
        }

        ///<summary>
        ///Get the managementDef defintion for this run.
        ///</summary>
        public ManagementItem ManagementDef
        {
            get { return managementDef; }
        }

        ///<summary>
        ///Get the parameter definition for this run.
        ///</summary>
        public CropParameterItem ParameterDef
        {
            get { return parametersDef; }
        }

        ///<summary>
        ///Get the run options defintion for this run.
        ///</summary>
        public RunOptionItem RunOptionDef
        {
            get { return runOptionsDef; }
        }



        public RawData<Universe> SavedUniverses
        {
            get { return savedUniverses; }
        }

        #endregion

        #region Properties

        ///<summary>
        ///Get the current Universe, i.e the universe created at the beginning of the simulation.
        ///</summary>
        public Universe CurrentUniverse
        {
            get { return currentUniverse; }
        }


        ///<summary>
        ///The weather controler of this run.
        ///</summary>
        /* public Weather Weather
         {
             get { return weather; }
         }*/

        // public Phenology.Phenology Ph { set; get; }

        #endregion

        ///<summary>
        ///Clone the current <c>Universe</c> and store it in the array <c>universes</c>.
        ///</summary>
        private void SaveCurrentUniverse()
        {
            var clone = new Universe(this, currentUniverse,false);
            savedUniverses.Add(clone);
            if (!IsSowDateEstimate) currentUniverse.GrowthDay += 1;
        }

        ///<summary>
        ///Get the Universe copy at a given date.
        ///</summary>
        ///<param name="theDate">The date of the Universe to get.</param>
        ///<returns>The Universe found or null.</returns>
        public Universe GetUniverse(DateTime theDate)
        {
            Universe result = null;
            if (theDate != CurrentUniverse.CurrentDate)
            {
                if (savedUniverses != null
                    && savedUniverses.Count > 0
                    && savedUniverses[0].CurrentDate <= theDate
                    && savedUniverses[savedUniverses.Count - 1].CurrentDate >= theDate)
                {
                    result = savedUniverses[(int)(theDate - savedUniverses[0].CurrentDate).TotalDays];
                }
            }
            else return currentUniverse;
            return result;
        }

        ///<summary>
        ///Update the run. This does the loop calculation over the year and calculate universe state.
        ///</summary>
        ///<param name="variety">The genotypeDef used in the run.</param>
        ///<param name="soil">The soilDef used in the run.</param>
        ///<param name="site">The siteDef used in the run.</param>
        ///<param name="management">The managementDef used in the run.</param>
        ///<param name="Inparameters">The parameter set used in the run.</param>
        ///<param name="runOptions">The run option set used in the run.</param>


        public void Start(
            CropParameterItem variety,
            SoilItem soil,
            SiteItem site,
            ManagementItem management,
            CropParameterItem Inparameters,
            RunOptionItem runOptions)
        {

            
            this.DoStop();

            // Initialisation
            //Ph=new Phenology.Phenology(new Universe(this));
            createParameters(variety, Inparameters);
            this.varietyDef = variety;
            this.soilDef = soil;
            this.siteDef = site;
            this.managementDef = management;
            this.parametersDef = Inparameters;
            this.runOptionsDef = runOptions;

            if (variety != null
                && management != null
                && Inparameters != null
                && runOptions != null
                && site != null
                && soil != null)
            {

            }

            else throw new InvalidOperationException("Some input are null.");


            savedUniverses = new RawData<Universe>(400);
            //weather = new Weather(this);

            currentUniverse = new Universe(this);

            ///<Behnam>
            ///<Comment>Estimating sowing date or using a fixed sowing date</Comment>

            IsSowDateEstimate = management.IsSowDateEstimate;
            SowingWindowType = site.SowingWindowType;
            management.FinalSowingDate = management.SowingDate;
            currentUniverse.Init(site, management);
            currentUniverse.Weather_.EstimateSowingWindows(site, management);
            currentUniverse.IsSowDateEstimate = IsSowDateEstimate;

            double SkipDays = Math.Max(1, management.SkipDays);
            SowingDate = management.SowingDate;
            // UseActualBase = runOptionsDef.UseActualBase;
            DoInteractions = runOptionsDef.DoInteractions;
            InteractionsW = runOptionsDef.InteractionsW;
            InteractionsN = runOptionsDef.InteractionsN;
            InteractionsT = runOptionsDef.InteractionsT;
            InteractionsWN = runOptionsDef.InteractionsWN;
            InteractionsWT = runOptionsDef.InteractionsWT;
            InteractionsNT = runOptionsDef.InteractionsNT;
            InteractionsWNT = runOptionsDef.InteractionsWNT;

            if (IsSowDateEstimate)
            {
                var year1 = SowingDate.Year;

                if (SowingWindowType == 0) // Fixed sowing window
                {
                    site.FinalMinSowingDate = currentUniverse.Weather_.ESD_Fixed(year1, site);
                    site.FinalMaxSowingDate = currentUniverse.Weather_.LSD_Fixed(year1, site);
                }
                else if (SowingWindowType == 1) // JRC winter sowing window
                {
                    site.FinalMinSowingDate = currentUniverse.Weather_.ESD_Winter(year1);
                    site.FinalMaxSowingDate = currentUniverse.Weather_.LSD_Winter(year1);
                }
                else if (SowingWindowType == 2) // JRC spring sowing window
                {
                    site.FinalMinSowingDate = currentUniverse.Weather_.ESD_Spring(year1, site);
                    site.FinalMaxSowingDate = currentUniverse.Weather_.LSD_Spring(year1, site);
                }
                else if (SowingWindowType == 3) // SiriusQuality method (based on nominal sowing date)
                {
                    site.FinalMinSowingDate = currentUniverse.Weather_.ESD_Sirius(year1, site);
                    site.FinalMaxSowingDate = currentUniverse.Weather_.LSD_Sirius(year1, site);
                }

                flagReSow = true;
                MinSowDate = site.FinalMinSowingDate.AddDays(-SkipDays);
                MaxSowDate = site.FinalMaxSowingDate;
                SowingDate = MinSowDate;
                management.FinalSowingDate = MinSowDate;
            }
            else
            {
                flagReSow = false;
                management.FinalSowingDate = SowingDate;
                site.FinalMinSowingDate = site.MinSowingDate;
                site.FinalMaxSowingDate = site.MaxSowingDate;
            }

            currentUniverse.Init(site, management);
            SaveCurrentUniverse();
            currentUniverse.CurrentDate = currentUniverse.CurrentDate.AddDays(1);
            //currentUniverse.Crop_.Sow(currentUniverse.calculateDailyThermalTime_.CumulTT, currentUniverse.CurrentDate);

            int numdSkipDays = 0;
            int numdFreezing = 0;
            int numdSoilTemp = 0;
            int numdSoilMois = 0;
            double numdPcp = 0;

            ///<Behnam (2015.11.27)>
            ///<Comment>To enable using soil depth without any limitations</Comment>
            checkLay = Convert.ToInt16(Math.Ceiling(management.CheckDepth / 5)); // Soil layer to be ckecked
            checkLay = Math.Min(checkLay, currentUniverse.Soil_.Layers.Count) - 1;
            ///</Behnam>

            double CheckDaysTemp = management.CheckDaysTemp;
            double CheckDaysPcp = management.CheckDaysPcp;

            double TAveThr = management.TAveThr;
            double TMinThr = management.TMinThr;
            double SoilMoistThr = management.SoilMoistThr;
            double CumPcpThr = management.CumPcpThr;
            double SoilWorkabThr = management.SoilWorkabThr;

            ///<Relaxation variables>
            double origTAveThr = management.TAveThr;
            double origTMinThr = management.TMinThr;
            double origSoilMoistThr = management.SoilMoistThr;
            double origCumPcpThr = management.CumPcpThr;
            double origSoilWorkabThr = management.SoilWorkabThr;

            double poTAveThr = 1.0;
            double poTMinThr = 1.0;
            double poSoilMoistThr = 1.0;
            double poCumPcpThr = 1.0;
            double poSoilWorkabThr = 1.0;
            double length = (int)(site.FinalMaxSowingDate - site.FinalMinSowingDate).TotalDays;

            double TAveThrRelax = 0;
            double TMinThrRelax = -4;
            double SoilMoistThrRelax = 0;
            double CumPcpThrRelax = 3;
            double SoilWorkabThrRelax = 1.5;
            ///</Relaxation variables>

            ///<Relaxation curves>
            poTAveThr = DecreasePower(TAveThr, TAveThrRelax, length);
            poTMinThr = DecreasePower(TMinThr, TMinThrRelax, length);
            poSoilMoistThr = DecreasePower(SoilMoistThr, SoilMoistThrRelax, length);
            poCumPcpThr = DecreasePower(CumPcpThr, CumPcpThrRelax, length);
            poSoilWorkabThr = IncreasePower(SoilWorkabThr, SoilWorkabThrRelax, length);
            ///</Relaxation curves>

            bool oldLimitedWater = runOptionsDef.UnlimitedWater;
            bool oldLimitedNitrogen = runOptionsDef.UnlimitedNitrogen;
            bool oldLimitedTemperature = runOptionsDef.UnlimitedTemperature;

            bool initSoilAfterSowing = false;
            double minTempInitSoilAfterSowing=0.0;
            double maxTempInitSoilAfterSowing=0.0;
            double meanTempInitSoilAfterSowing = 0.0;

            while (!currentUniverse.Crop_.IsEnd)
            {
                if (!IsSowDateEstimate && !flagReSow)
                ///<Comment>Non-stressed conditions are only applied after sowing</Comment>
                {
                    if (DoInteractions)
                    {
                        ///<Parallel runs>
                        double OutputTotalDM_W = 0;
                        double OutputTotalDM_N = 0;
                        double OutputTotalDM_T = 0;
                        double OutputTotalDM_WN = 0;
                        double OutputTotalDM_WT = 0;
                        double OutputTotalDM_NT = 0;
                        double OutputTotalDM_WNT = 0;

                        /// Behnam (2016.07.13): The runOld.RunOptionDef.UseActualBase was previously used. Pierre wanted it to be deleted.
                        /// At one stage, we decided to use Potential conditions as the base run. Now it is not working anymore.

                        if (true) 
                        {
                            ///<Use actual conditions as the base run>
                            if (InteractionsW) OutputTotalDM_W = RunUnlimited(currentUniverse, true, false, false);
                            if (InteractionsN) OutputTotalDM_N = RunUnlimited(currentUniverse, false, true, false);
                            if (InteractionsT) OutputTotalDM_T = RunUnlimited(currentUniverse, false, false, true);
                            if (InteractionsWN) OutputTotalDM_WN = RunUnlimited(currentUniverse, true, true, false);
                            if (InteractionsWT) OutputTotalDM_WT = RunUnlimited(currentUniverse, true, false, true);
                            if (InteractionsNT) OutputTotalDM_NT = RunUnlimited(currentUniverse, false, true, true);
                            if (InteractionsWNT) OutputTotalDM_WNT = RunUnlimited(currentUniverse, true, true, true);
                            
                            currentUniverse.isUnlimitedWater = false;
                            currentUniverse.isUnlimitedNitrogen = false;
                            currentUniverse.isUnlimitedTemperature = false;

                            currentUniverse.Crop_.areRootsToBeGrown = true;

                            if (initSoilAfterSowing)
                            {
                                
                                currentUniverse.Soil_.Init(minTempInitSoilAfterSowing, maxTempInitSoilAfterSowing, meanTempInitSoilAfterSowing, false);
                                currentUniverse.ShootTemperature_.MinShootTemperature = 0;
                                currentUniverse.ShootTemperature_.MaxShootTemperature = 0;
                                currentUniverse.ShootTemperature_.MeanShootTemperature = 0;
                                currentUniverse.CumAirTempFromSowing= 0.0;
                                currentUniverse.CumMaxCanopyTempFromSowing= 0.0;
                                currentUniverse.CumMaxAirTempFromSowing= 0.0;
                                currentUniverse.PrevAppliedStagesN = 1;
                                currentUniverse.PrevAppliedStagesIrr = 1;
                                //currentUniverse.NFertChange = 1;
                                currentUniverse.CalcChangeinNFertilisation();


                                initSoilAfterSowing = false;
                            }
                                currentUniverse.RunDayStep();
                        }
                        else
                        {
                            ///<Use non-stressed conditions as the base run>
                            if (InteractionsW) OutputTotalDM_W = RunUnlimited(currentUniverse, false, true, true);
                            if (InteractionsN) OutputTotalDM_N = RunUnlimited(currentUniverse, true, false, true);
                            if (InteractionsT) OutputTotalDM_T = RunUnlimited(currentUniverse, true, true, false);
                            if (InteractionsWN) OutputTotalDM_WN = RunUnlimited(currentUniverse, false, false, true);
                            if (InteractionsWT) OutputTotalDM_WT = RunUnlimited(currentUniverse, false, true, false);
                            if (InteractionsNT) OutputTotalDM_NT = RunUnlimited(currentUniverse, true, false, false);
                            if (InteractionsWNT) OutputTotalDM_WNT = RunUnlimited(currentUniverse, false, false, false);

                            currentUniverse.isUnlimitedWater = true;
                            currentUniverse.isUnlimitedNitrogen = true;
                            currentUniverse.isUnlimitedTemperature = true;
                            currentUniverse.Crop_.areRootsToBeGrown = true;
                            if (initSoilAfterSowing)
                            {
                                
                                currentUniverse.Soil_.Init(minTempInitSoilAfterSowing, maxTempInitSoilAfterSowing, meanTempInitSoilAfterSowing, false);
                                currentUniverse.ShootTemperature_.MinShootTemperature = 0;
                                currentUniverse.ShootTemperature_.MaxShootTemperature = 0;
                                currentUniverse.ShootTemperature_.MeanShootTemperature = 0;
                                currentUniverse.CumAirTempFromSowing = 0.0;
                                currentUniverse.CumMaxCanopyTempFromSowing = 0.0;
                                currentUniverse.CumMaxAirTempFromSowing = 0.0;
                                currentUniverse.PrevAppliedStagesN = 1;
                                currentUniverse.PrevAppliedStagesIrr = 1;
                                //currentUniverse.NFertChange = 1;
                                currentUniverse.CalcChangeinNFertilisation();


                            }
                            currentUniverse.RunDayStep();
                        }


                        currentUniverse.Crop_.OutputTotalDM_W = OutputTotalDM_W;
                        currentUniverse.Crop_.OutputTotalDM_N = OutputTotalDM_N;
                        currentUniverse.Crop_.OutputTotalDM_T = OutputTotalDM_T;
                        currentUniverse.Crop_.OutputTotalDM_WN = OutputTotalDM_WN;
                        currentUniverse.Crop_.OutputTotalDM_WT = OutputTotalDM_WT;
                        currentUniverse.Crop_.OutputTotalDM_NT = OutputTotalDM_NT;
                        currentUniverse.Crop_.OutputTotalDM_WNT = OutputTotalDM_WNT;
                        ///</Parallel runs>
                    }
                    else
                    {
                        ///<Normal run>
                        currentUniverse.isUnlimitedWater = oldLimitedWater;
                        currentUniverse.isUnlimitedNitrogen = oldLimitedNitrogen;
                        currentUniverse.isUnlimitedTemperature = oldLimitedTemperature;
                        currentUniverse.Crop_.areRootsToBeGrown = true;
                        if (initSoilAfterSowing)
                        {

                            currentUniverse.Soil_.Init(minTempInitSoilAfterSowing, maxTempInitSoilAfterSowing, meanTempInitSoilAfterSowing, false);
                            currentUniverse.ShootTemperature_.MinShootTemperature = 0;
                            currentUniverse.ShootTemperature_.MaxShootTemperature = 0;
                            currentUniverse.ShootTemperature_.MeanShootTemperature = 0;
                            currentUniverse.CumAirTempFromSowing = 0.0;
                            currentUniverse.CumMaxCanopyTempFromSowing = 0.0;
                            currentUniverse.CumMaxAirTempFromSowing = 0.0;
                            currentUniverse.PrevAppliedStagesN = 1;
                            currentUniverse.PrevAppliedStagesIrr = 1;
                            //currentUniverse.NFertChange = 1;
                            currentUniverse.CalcChangeinNFertilisation();



                            initSoilAfterSowing = false;
                        }
                        currentUniverse.RunDayStep();
                        ///</Normal run>
                    }
                }
                else

                {
                    ///<Comment>Non-stressed conditions are only applied after sowing</Comment>
                    currentUniverse.isUnlimitedWater = false;
                    currentUniverse.isUnlimitedNitrogen = false;
                    currentUniverse.isUnlimitedTemperature = false;
                    currentUniverse.Crop_.areRootsToBeGrown = false;
                    currentUniverse.RunDayStep();                   
                }


                if (IsSowDateEstimate)
                {

                    // Checking sowing conditions:

                    ///<Skip days>
                    numdSkipDays += 1;
                    flagSkipDays = (numdSkipDays <= SkipDays);
                    ///</Skip days>

                    flagMaxDate = true;
                    flagSoilMois = true;
                    flagCumPcp = true;
                    flagSoilTemp = true;
                    flagFreezing = true;
                    flagWorkabil = true;

                    if (!flagSkipDays)
                    {

                        if (management.DoRelax)
                        {
                            ///<Relaxation>
                            ///<Comment>The criteria are relaxed here using four exponential curves, if DoRelax=TRUE</Comment>
                            var n = numdSkipDays - SkipDays - 1;
                            TAveThr = ExpoDecrease(TAveThr, TAveThrRelax, origTAveThr, n, poTAveThr);
                            TMinThr = ExpoDecrease(TMinThr, TMinThrRelax, origTMinThr, n, poTMinThr);
                            SoilMoistThr = ExpoDecrease(SoilMoistThr, SoilMoistThrRelax, origSoilMoistThr, n, poSoilMoistThr);
                            CumPcpThr = ExpoDecrease(CumPcpThr, CumPcpThrRelax, origCumPcpThr, n, poCumPcpThr);
                            SoilWorkabThr = ExpoIncrease(SoilWorkabThr, SoilWorkabThrRelax, origSoilWorkabThr, n, poSoilWorkabThr);
                            ///</Relaxation>
                        }

                        ///<Average daily air temperature temperature>
                        numdSoilTemp = 0;
                        for (var i = 1; i <= CheckDaysTemp; ++i)
                        {
                            if (currentUniverse.Weather_.MeanTemp(currentUniverse.CurrentDate.AddDays(i)) >= TAveThr)
                            {
                                numdSoilTemp += 1;
                            }
                        }
                        flagSoilTemp = (numdSoilTemp < CheckDaysTemp);
                        ///</Average daily air temperature temperature>

                        ///<Minimum daily air temperature (frost risk)>
                        numdFreezing = 0;
                        for (var i = 1; i <= CheckDaysTemp; ++i)
                        {
                            if (currentUniverse.Weather_.MinTemp(currentUniverse.CurrentDate.AddDays(i)) >= TMinThr)
                            {
                                numdFreezing += 1;
                            }
                        }
                        flagFreezing = (numdFreezing < CheckDaysTemp);
                        ///</Minimum daily air temperature (frost risk)>

                        ///<Cumulative precipitation>
                        numdPcp = 0;
                        for (var i = 1; i <= CheckDaysPcp; ++i)
                        {
                            numdPcp += currentUniverse.Weather_.Rain(currentUniverse.CurrentDate.AddDays(i)) / MMwaterToGwater;
                        }
                        flagCumPcp = (numdPcp < CumPcpThr);
                        ///</Cumulative precipitation>

                        ///<Soil moisture>
                        if (currentUniverse.Soil_.Layers[checkLay].AvWater < SoilMoistThr * currentUniverse.Soil_.Layers[checkLay].MaxAvWater)
                            numdSoilMois = 0;
                        if (currentUniverse.Soil_.Layers[checkLay].AvWater >= SoilMoistThr * currentUniverse.Soil_.Layers[checkLay].MaxAvWater)
                            numdSoilMois += 1;
                        flagSoilMois = (numdSoilMois == 0);
                        ///</Soil moisture>

                        ///<Soil compaction (workability)>
                        flagWorkabil = ((currentUniverse.Soil_.Layers[checkLay].AvWater +
                            currentUniverse.Soil_.Layers[checkLay].ExWater) / currentUniverse.Soil_.Layers[checkLay].FcWater
                            > SoilWorkabThr);
                        ///</Soil compaction (workability)>
                    }

                    ///<Sowing window>
                    flagMaxDate = (currentUniverse.CurrentDate < MaxSowDate.AddDays(-1));
                    ///</Sowing window>

                    flagReSow = flagMaxDate & (flagSkipDays || flagSoilMois || flagCumPcp
                        || flagSoilTemp || flagFreezing || flagWorkabil);
                }

                if (IsSowDateEstimate) // If sowing date search is ON, at least one day before sowing must be simulated.
                {
                    SowingDate = currentUniverse.CurrentDate.AddDays(1);
                    currentUniverse.Crop_.Dispose();
                    currentUniverse.Crop_ = new CropModel.Crop(currentUniverse);

                    //Debug
                    
                    //currentUniverse.CumMaxCanopyTempFromSowing = 0.0;
                    //??currentUniverse.Crop_.RootLength = 0.0;
                    //??currentUniverse.Crop_.Tau = 1.0;
                    //??currentUniverse.Crop_.SumInternodesLength = 0.0;
                    //??currentUniverse.Crop_.PotentialWaterOnLeaves = 0.0;
                    //??currentUniverse.Crop_.RootLength = 0.0;
                    //currentUniverse.Crop_.LeafNumber = 0.0;
                    //currentUniverse.Crop_.Ntip = 0.0;
                    /*currentUniverse.ShootTemperature_.MinShootTemperature=0.0;
                    currentUniverse.ShootTemperature_.MaxShootTemperature=0.0;
                    //currentUniverse.Soil_.Init(currentUniverse.Weather_.MinTemp(currentUniverse.CurrentDate/*.AddDays(1)*//*)/*, currentUniverse.Weather_.MaxTemp(currentUniverse.CurrentDate/*.AddDays(1)*//*), (currentUniverse.Weather_.MinTemp(currentUniverse.CurrentDate/*.AddDays(1)*//*) + currentUniverse.Weather_.MaxTemp(currentUniverse.CurrentDate/*.AddDays(1)*//*))/2, false);*/
                    //currentUniverse.Soil_.energyBalanceBiomaWrapper_.Init(false);
                    //Debug /**/
                    //currentUniverse.Soil_.MaximumCanopyTemperature = currentUniverse.Weather_.MaxTemp(currentUniverse.CurrentDate/*.AddDays(1)*/);//??
                    //currentUniverse.Soil_.MinimumCanopyTemperature = currentUniverse.Weather_.MinTemp(currentUniverse.CurrentDate/*.AddDays(1)*/);//??
                    //currentUniverse.Soil_.SoilMinTemperature = currentUniverse.Weather_.MinTemp(currentUniverse.CurrentDate/*.AddDays(1)*/);//??
                    //currentUniverse.Soil_.SoilMaxTemperature = currentUniverse.Weather_.MaxTemp(currentUniverse.CurrentDate/*.AddDays(1)*/);//??;
                    //currentUniverse.ShootTemperature_.MinShootTemperature = 0;
                    //currentUniverse.ShootTemperature_.MaxShootTemperature = 0;
                    //currentUniverse.ShootTemperature_.MeanShootTemperature = 0;
                    //currentUniverse.Crop_.Universe_.thermalTimeWrapper_.Init();
                    initSoilAfterSowing = true;
                    minTempInitSoilAfterSowing = currentUniverse.Weather_.MinTemp(currentUniverse.CurrentDate/*.AddDays(1)*/);
                    maxTempInitSoilAfterSowing = currentUniverse.Weather_.MaxTemp(currentUniverse.CurrentDate/*.AddDays(1)*/);
                   // meanTempInitSoilAfterSowing = (currentUniverse.Weather_.MinTemp(currentUniverse.CurrentDate/*.AddDays(1)*/) + currentUniverse.Weather_.MaxTemp(currentUniverse.CurrentDate/*.AddDays(1)*/)) / 2;
                    
                    
                    //Debug /**/
                    currentUniverse.Crop_.Universe_.thermalTimeWrapper_.Init();
                    
                    currentUniverse.Crop_.Init(currentUniverse.thermalTimeWrapper_.CumulTT, currentUniverse.CurrentDate.AddDays(1));
                    currentUniverse.CurrentDate = currentUniverse.CurrentDate.AddDays(1);
                    
                    meanTempInitSoilAfterSowing = currentUniverse.Weather_.GetMeanTemperatureBeforeSowing(SowingDate);
                }



                if (IsSowDateEstimate & flagReSow) // At least one of the sowing criteria is not met.
                {
                    IsSowDateEstimate = true; // Sowing date search continues;
                    SaveCurrentUniverse();
                }
                else if (IsSowDateEstimate & !flagReSow) // All of the sowing criteria are met.
                {
                    IsSowDateEstimate = false; // Sowing date search ends;
                    currentUniverse.IsSowDateEstimate = false;
                    management.FinalSowingDate = SowingDate;
                    currentUniverse.CalcChangeinNFertilisation();
                    SaveCurrentUniverse();
                }
                else if (!IsSowDateEstimate) // Sowing date search is OFF or all of the sowing criteria are met.
                {
                    SaveCurrentUniverse();
                    currentUniverse.CurrentDate = currentUniverse.CurrentDate.AddDays(1);
                }
            }
        }

        public void DoStop()
        {
            varietyDef = null;
            soilDef = null;
            siteDef = null;
            managementDef = null;
            parametersDef = null;
            runOptionsDef = null;
            var nbRun = savedUniverses != null ? savedUniverses.Count : 0;
            for (var i = 0; i < nbRun; ++i)
            {
                var universe = savedUniverses[i];
                if (universe != null)
                {
                    universe.Dispose();
                    savedUniverses[i] = null;
                }
            }
            savedUniverses = null;

            currentUniverse = null;
        }

        ///<summary>
        ///Initialisation of the parameters Dictionary
        ///</summary>
        private void createParameters(CropParameterItem variety, CropParameterItem parameter)
        {
            parameters = new Dictionary<string, double>();

            parameters = variety.ParamValue.Union(parameter.ParamValue).ToDictionary(k => k.Key, v => v.Value); // union of varietal and non-varietal dictionnary
        }

        ///<Behnam (2015.10.28)>
        ///<Comment>Functions to be used for relaxation of sowing date criteria</Comment>
        private double DecreasePower(double value, double threshold, double length)
        {
            if (value > threshold) return Math.Log(value - threshold + 1) / length;
            else return 1;
        }
        private double IncreasePower(double value, double threshold, double length)
        {
            if (value < threshold) return Math.Log(threshold - value + 1) / length;
            else return 1;
        }
        private double ExpoDecrease(double value, double threshold, double baseval, double n, double power)
        {
            if (value > threshold) return baseval + 1 - Math.Exp(n * power);
            else return value;
        }
        private double ExpoIncrease(double value, double threshold, double baseval, double n, double power)
        {
            if (value < threshold) return baseval - 1 + Math.Exp(n * power);
            else return value;
        }

        ///<Comment>Runs the simulation under specified stresses and returns total DM</Comment>
        public double RunUnlimited(Universe currentUniverse, bool isUnlimitedWater,
            bool isUnlimitedNitrogen, bool isUnlimitedTemperature)
        {
            
            Universe universeCopy = new Universe(this, currentUniverse, true);
            universeCopy.isUnlimitedWater = isUnlimitedWater;
            universeCopy.isUnlimitedNitrogen = isUnlimitedNitrogen;
            universeCopy.isUnlimitedTemperature = isUnlimitedTemperature;
            universeCopy.Crop_.areRootsToBeGrown = true;
            universeCopy.RunDayStep();
            return universeCopy.Crop_.OutputTotalDM;
        }
        ///</Behnam>
    }
}
  