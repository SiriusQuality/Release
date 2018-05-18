using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using SiriusModel.InOut.OutputWriter;
using SiriusModel.Model;
using SiriusModel.Model.Phenology;
using SiriusModel.Model.CropModel;
using SiriusModel.Model.Observation;
using SiriusModel.Model.SoilModel;
using SiriusQualityPhenology;

namespace SiriusModel.InOut
{
    public class OutputFileExtractorMaize : OutputFileExtractor
    {
        #region normal

        internal override PageData NormalRun(Run runOld)
        {
            var page = new PageData();

            PrintVersionDate(page);
            PrintFiles(page);
            PrintNormalRunSummaryDate(page, runOld);
            PrintNormalRunSummaryCrop(page, runOld);

            page.SetMinWidth(4);

            PrintItems(page, runOld);

            page.NewLine();

            var headerHeight = page.Count;

            ///<Behnam (2016.01.11)
            ///<Comment>This page size causes problems with growing seasons longer than 300 days.
            ///A better solution is to use Page.Count instead if using a pre-defined page size</Comment>
            var NormalSize = 300;
            ///</Behnam>

            PrintDailyCrop(page, runOld);
            page.SetHeight(NormalSize * 1 + headerHeight);

            PrintDailyWaterSoil(page, runOld);
            page.SetHeight(NormalSize * 2 + headerHeight);

            PrintDailyNitrogenSoil(page, runOld);
            page.SetHeight(NormalSize * 3 + headerHeight);

            PrintDailyLaminae(page, runOld);
            page.SetHeight(NormalSize * 4 + headerHeight);

            PrintDailySheath(page, runOld);
            page.SetHeight(NormalSize * 5 + headerHeight);

            PrintDailyInternodes(page, runOld);
            page.SetHeight(NormalSize * 6 + headerHeight);

            PrintDailyTillerLaminaeAreaIndex(page, runOld);
            page.SetHeight(NormalSize * 7 + headerHeight);//Add one more output of leaf area of different tillers. Modified by J.He, 06-19-2009.

            PrintDailyTillerSheathAreaIndex(page, runOld);
            page.SetHeight(NormalSize * 8 + headerHeight);//Add one more output of sheath area of different tillers. Modified by J.He, 06-19-2009.

            PrintLeafLength(page, runOld);
            page.SetHeight(NormalSize * 9 + headerHeight);

            PrintLeafWidth(page, runOld);
            page.SetHeight(NormalSize * 10 + headerHeight);

            PrintDailyHourlyRad(page, runOld);
            page.SetHeight(NormalSize * 11 + headerHeight);

            page.NewLine();
            return page;
        }

        public static void PrintNormalRunSummaryDate(PageData page, Run runOld)
        {
            page.NewLine();
            page.NewLine().Add("Date").Add("DOY").Add("Growth stage");
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_00_Sowing);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_10_Emergence);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.FloralInitiation); /// Behnam (2016.02.12): Pierre wanted to change have FloralInitiation instead of ZC_39
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.BeginningStemExtension);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_65_Anthesis);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_75_EndCellDivision);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_91_EndGrainFilling);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_92_Maturity);
        }

        public static void PrintNormalRunSummaryDate(PageData page, Run runOld, GrowthStage moment)
        {
            var lastUniverse = GetLastUniverse(runOld);
            if (lastUniverse != null)
            {
                DateTime? momentDate = lastUniverse.Crop_.getDateOfStage(moment);

                ///<Behnam (2015.09.18)>
                ///<Comment>
                ///Previously it was required to subtract one day to report model state variables at specific
                ///growth stages, except ZC_00_Sowing and ZC_39_FlagLeafLiguleJustVisible.
                ///By this modification, the model stores the dates correctly and they need not to be changed
                ///when reporting or when retrieving their values.
                ///</Comment>

                // if (lastUniverse.Crop_.Calendar[moment].HasValue && moment != GrowthStage.ZC_00_Sowing &&
                // moment != GrowthStage.ZC_39_FlagLeafLiguleJustVisible)
                // {
                // momentDate = lastUniverse.Crop_.calendar[moment].Value.AddDays(-1);
                // }

                ///</Behnam>

                if (momentDate != null)
                {
                    page.NewLine().AddDateDOY(momentDate).Add(Phase.growthStageAsString(moment));
                }
                else page.NewLine().AddDateDOY(null).Add(Phase.growthStageAsString(moment));
            }
            else page.NewLine().AddDateDOY(null).Add(Phase.growthStageAsString(moment));
        }

        public static void PrintNormalRunSummaryCrop(PageData page, Run runOld)
        {
            var anthesisUniverse = GetUniverse(runOld, GrowthStage.ZC_65_Anthesis);
            var maturityUniverse = GetUniverse(runOld, GrowthStage.ZC_92_Maturity);

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.LeafNumber, 2).Add("Final leaf number");
            else page.NewLine().Add("?.??").Add("Final leaf number");

            if (anthesisUniverse != null) page.NewLine().Add(anthesisUniverse.Crop_.LaminaeAI, 2).Add("LAI at anthesis (m²/m²)");
            else page.NewLine().Add("?.??").Add("LAI at anthesis (m²/m²)");

            if (anthesisUniverse != null) page.NewLine().Add(anthesisUniverse.Crop_.GAI, 2).Add("GAI at anthesis (m²/m²)");
            else page.NewLine().Add("?.??").Add("GAI at anthesis (m²/m²)");

            if (anthesisUniverse != null) page.NewLine().Add(anthesisUniverse.Crop_.OutputTotalDM, 0, 10).Add("Crop DM at anthesis (kgDM/ha)");
            else page.NewLine().Add("?.??").Add("Crop DM at anthesis (kgDM/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.OutputTotalDM, 0, 10).Add("Crop DM at maturity (kgDM/ha)");
            else page.NewLine().Add("?").Add("Crop DM at maturity (kgDM/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.GrainTotalDM, 0, 10).Add("Grain DM at maturity (kgDM/ha)");
            else page.NewLine().Add("?").Add("Grain DM at maturity (kgDM/ha)");

            if (anthesisUniverse != null) page.NewLine().Add(anthesisUniverse.Crop_.OutputTotalN, 0, 10).Add("Crop N at anthesis (kgN/ha)");
            else page.NewLine().Add("?").Add("Crop N at anthesis (kgN/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.OutputTotalN, 0, 10).Add("Crop N at maturity (kgN/ha)");
            else page.NewLine().Add("?").Add("Crop N at maturity (kgN/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.GrainTotalN, 1, 10).Add("Grain N at maturity (kgN/ha)");
            else page.NewLine().Add("?").Add("Grain N at maturity (kgN/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.PostAnthesisNUptake, 0, 10).Add("Post-anthesis crop N uptake (kgN/ha)");
            else page.NewLine().Add("?").Add("Post-anthesis crop N uptake (kgN/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.TotalDMperGrain, 2).Add("Single grain DM at maturity (mgDM/grain)");
            else page.NewLine().Add("?").Add("Single grain DM at maturity (mgDM/grain)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.TotalNperGrain, 2).Add("Single grain N at maturity (mgN/grain)");
            else page.NewLine().Add("?").Add("Single grain N at maturity (mgN/grain)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.ProteinConcentration, 2).Add("Grain protein concentration at maturity (% of grain DM)");
            else page.NewLine().Add("?.??").Add("Grain protein concentration at maturity (% of grain DM)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.GrainNumber, 0).Add("Grain number (grain/m²)");
            else page.NewLine().Add("?").Add("Grain number (grain/m²)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.PercentStarch, 4).Add("% Starch at maturity (% of total grain DM)");
            else page.NewLine().Add("?").Add("% Starch at maturity (% of total grain DM)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.NalbGlo, 2).Add("Albumins-globulins at maturity (mgN/grain)");
            else page.NewLine().Add("?.??").Add("Albumins-globulins at maturity (mgN/grain)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Namp, 2).Add("Amphiphils at maturity (mgN/grain)");
            else page.NewLine().Add("?.??").Add("Amphiphils at maturity (mgN/grain)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Ngli, 2).Add("Gliadins at maturity (mgN/grain)");
            else page.NewLine().Add("?.??").Add("Gliadins at maturity (mgN/grain)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Nglu, 2).Add("Glutenins at maturity (mgN/grain)");
            else page.NewLine().Add("?.??").Add("Glutenins at maturity, mgN/grain)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.PercentGli, 3).Add("% Gliadins at maturity (% of total grain N)");
            else page.NewLine().Add("?.??").Add("% Gliadins at maturity (% of total grain N)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.PercentGlu, 3).Add("% Gluteins at maturity (% of total grain N)");
            else page.NewLine().Add("?.??").Add("% Gluteins at maturity (% of total grain N)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.GliadinsToGluteins, 2).Add("Gliadins-to-gluteins ratio (dimensionless)");
            else page.NewLine().Add("?.??").Add("Gliadins-to-gluteins ratio (dimensionless)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.HarvestIndexDM, 2).Add("DM harvest index (dimensionless)");
            else page.NewLine().Add("?.??").Add("DM harvest index (dimensionless)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.HarvestIndexN, 2).Add("N harvest index (dimensionless)");
            else page.NewLine().Add("?.??").Add("N harvest index (dimensionless)");

            if (maturityUniverse != null) page.NewLine().Add((maturityUniverse.Soil_.IncomeWater * Run.GwaterToMMwater), 1).Add("rainfall + irrigation (mm)");
            else page.NewLine().Add("?.??").Add("rainfall + irrigation (mm)");

            if (maturityUniverse != null) page.NewLine().Add((maturityUniverse.Soil_.CalculateTotalN() + maturityUniverse.Soil_.SoilDeepLayer_.LostNitrogen + maturityUniverse.Crop_.CropTotalN), 0, 10).Add("Total available soil N (kgN/ha)");
            else page.NewLine().Add("?.??").Add("Total available soil N (kgN/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Soil_.SoilDeepLayer_.LostNitrogen, 2, 10).Add("N leaching (kgN/ha)");
            else page.NewLine().Add("?.??").Add("N leaching (kgN/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Soil_.SoilDeepLayer_.LostWater, 1, Run.GwaterToMMwater).Add("Water drainage (mm)");
            else page.NewLine().Add("?.??").Add("Water drainage (mm)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.NuseEfficiency, 2).Add("N use efficiency (kgDM/kgN)");
            else page.NewLine().Add("?.??").Add("N use efficiency (kgDM/kgN)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.NutilisationEfficiency, 2).Add("N utilisation efficiency (kgDM/kgN)");
            else page.NewLine().Add("?.??").Add("N utilisation efficiency (kgDM/kgN)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.NuptakeEfficiency, 2).Add("N uptake efficiency (kgN/kgN)");
            else page.NewLine().Add("?.??").Add("N upake efficiency (kgN/kgN)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.WaterUseEfficiency, 2).Add("Water use efficiency (kgDM/ha/mm)");
            else page.NewLine().Add("?.??").Add("Water use efficiency (kgDM/ha/mm)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Soil_.AccumulatedActEvapoTranspiration, 2, Run.GwaterToMMwater).Add("Cumulative ET (mm)");
            else page.NewLine().Add("?.??").Add("Cumulative ET (mm)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Soil_.AccumulatedNitrogenMineralisation, 2, 10).Add("Cumulative N mineralisation (kgN/ha) in soil profil");
            else page.NewLine().Add("?.??").Add("Cumulative N mineralisation in soil profil (kgN/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Soil_.AccumulatedNitrogenDenitrification, 2, 10).Add("Cumulative N denitrification in soil profil (kgN/ha)");
            else page.NewLine().Add("?.??").Add("Cumulative N denitrification in soil profil (kgN/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Soil_.CalculateAvailableExcessN(), 2, 10).Add("Available mineral soil N at maturity in soil profil (kgN/ha)");
            else page.NewLine().Add("?.??").Add("Available mineral soil N at maturity in soil profil (kgN/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Soil_.CalculateTotalN(), 2, 10).Add("Total mineral soil N at maturity in soil profil (kgN/ha)");
            else page.NewLine().Add("?.??").Add("Total mineral soil N at maturity in soil profil (kgN/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Soil_.CalculateAvailableExcessW(), 2, Run.GwaterToMMwater).Add("Available water at maturity in soil profil (mm)");
            else page.NewLine().Add("?.??").Add("Available water at maturity  in soil profil(mm)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Soil_.AccumulatedActSoilEvaporation, 2, Run.GwaterToMMwater).Add("Cumulative Evaporation (mm)");
            else page.NewLine().Add("?.??").Add("Cumulative Evaporation (mm)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Soil_.AccumulatedActTranspiration, 2, Run.GwaterToMMwater).Add("Cumulative Transpiration (mm)");
            else page.NewLine().Add("?.??").Add("Cumulative Transpiration (mm)");

            var titleTillering = new LineData().Add("Main stem density at anthesis (shoot/m²)");

            int tilleringProfileLength = anthesisUniverse.Crop_.tilleringProfile.Count;
            for (var i = 1; i < tilleringProfileLength; ++i)
            {
                if (i == 1) titleTillering = titleTillering.Add("First order tiller density at anthesis (shoot/m²)");
                else if (i == 2) titleTillering = titleTillering.Add("Second order tiller density at anthesis (shoot/m²)");
                else if (i == 3) titleTillering = titleTillering.Add("Third order tiller density at anthesis (shoot/m²)");
                else titleTillering = titleTillering.Add(i + "th order tiller density at anthesis (shoot/m²)");
            }
            page.Add(titleTillering);

            var valueTillering = new LineData();
            if (tilleringProfileLength > 0)
            {
                for (var i = 0; i < tilleringProfileLength; ++i)
                {
                    valueTillering = valueTillering.Add(anthesisUniverse.Crop_.tilleringProfile[i]);
                }
            }
            else valueTillering = valueTillering.Add(0);

            page.Add(valueTillering);
        }

        public static void PrintItems(PageData page, Run runOld)
        {
            PrintManagementItem(page, runOld.ManagementDef);
            PrintParameterItem(page, runOld.ParameterDef);
            PrintRunOptionItem(page, runOld.RunOptionDef);
            PrintSiteItem(page, runOld.SiteDef);
            PrintSoilItem(page, runOld.SoilDef);
            PrintVarietyItem(page, runOld.VarietyDef);
        }

        public static void PrintManagementItem(PageData page, ManagementItem item)
        {
            var nbDate = (item != null) ? item.DateApplications.Count : 0;
            var nbGs = (item != null) ? item.GrowthStageApplications.Count : 0;

            ///<Behnam (2015.09.23)>
            ///<Comment>Adding outputs related to sowing date estimation procedures</Comment>

            page[3]
                .Add(FileContainer.ManagementID)
                .Add("Is sowing date estimated?")
                .Add("Is relaxation applied?")
                .Add("(Nominal) sowing date")
                .Add("Final sowing date")
                .Add("Number of skiped days")
                .Add("Number of checked days for precipitation")
                .Add("Cumulative precipitation threshold for sowing date (mm)")
                .Add("Soil depth to be checked")
                .Add("Soil moisture threshold for germination")
                .Add("Soil moisture threshold for workability")
                .Add("Number of checked days for temperature")
                .Add("Average daily air temperature threshold for germination (C)")
                .Add("Minimum daily air temperature threshold for frost (C)")
                .Add("Is N application specified in total amount?")
                .Add("Total N application (gN/m²)")
                .Add("Is annual trend applied on N fertilisation?")
                .Add("N trend base year")
                .Add("N trend slope (%/year)")
                .Add("Is annual trend applied on CO2 concentration?")
                .Add("CO2 trend base year")
                .Add("CO2 trend slope (%/year)")
                .Add("Is NNI used for N fertilisation?")
                .Add("NNI threshold to trigger N fertilisation")
                .Add("NNI multiplier")
                .Add("Is cumulative precipitation checked for N application?")
                .Add("Number of checked days for precipitation for N application")
                .Add("Maximum allowable postponement (days)")
                .Add("Cumulative precipitation threshold for N application (mm)")
                .Add("Total inorganic N (gN/ha)")
                .Add("% inorganic N in top 33%")
                .Add("% inorganic N in middle 33%")
                .Add("Grain number (grain/m²)")
                .Add("Sowing density (seed/m²)")
                .Add("Is soil water deficit at sowing is percentage")
                .Add("Soil water deficit at sowing (mm or %)")
                .Add("CO2 (ppm)")
                .Add("Target fertile shoot number (shoot/m²)");

            for (var i = 0; i < nbDate; ++i)
            {
                page[3]
                    .Add("Date")
                    .Add((item.IsTotalNitrogen) ? "N fertilisation (%)" : "N fertilisation (gN/ha)")
                    .Add("Irrigation (mm)");
            }
            for (var i = 0; i < nbGs; ++i)
            {
                page[3]
                    .Add("Growth stage")
                    .Add((item.IsTotalNitrogen) ? "N fertilisation (%)" : "N fertilisation (gN/ha)")
                    .Add("Irrigation (mm)");
            }
            if (item != null)
            {
                page[4]
                    .Add(item.Name)
                    .Add(item.IsSowDateEstimate.ToString())
                    .Add(item.DoRelax.ToString())
                    .Add(item.SowingDate.ToString("u").Split()[0])
                    .Add(item.FinalSowingDate.ToString("u").Split()[0])
                    .Add(item.SkipDays.ToString())
                    .Add(item.CheckDaysPcp.ToString())
                    .Add(item.CumPcpThr.ToString())
                    .Add(item.CheckDepth.ToString())
                    .Add(item.SoilMoistThr.ToString())
                    .Add(item.SoilWorkabThr.ToString())
                    .Add(item.CheckDaysTemp.ToString())
                    .Add(item.TAveThr.ToString())
                    .Add(item.TMinThr.ToString())
                    .Add(item.IsTotalNitrogen.ToString())
                    .Add(item.TotalNApplication.ToString())
                    .Add(item.IsNTrendApplied.ToString())
                    .Add(item.NTrendBaseYear.ToString())
                    .Add(item.NTrendSlope.ToString())
                    .Add(item.IsCO2TrendApplied.ToString())
                    .Add(item.CO2TrendBaseYear.ToString())
                    .Add(item.CO2TrendSlope.ToString())
                    .Add(item.IsNNIUsed.ToString())
                    .Add(item.NNIThreshold.ToString())
                    .Add(item.NNIMultiplier.ToString())
                    .Add(item.IsCheckPcpN.ToString())
                    .Add(item.CheckDaysPcpN.ToString())
                    .Add(item.MaxPostponeN.ToString())
                    .Add(item.CumPcpThrN.ToString())
                    .Add(item.TotalNi, 2, 10)
                    .Add(item.TopNi)
                    .Add(item.MidNi)
                    .Add(item.ObservedGrainNumber)
                    .Add(item.SowingDensity)
                    .Add(item.IsWDinPerc)
                    .Add(item.SoilWaterDeficit)
                    .Add(item.CO2)
                    .Add(item.TargetFertileShootNumber);
            }
            ///</Behnam>

            else
            {
                page[4]
                    .Add("?");
            }
            for (var i = 0; i < nbDate; ++i)
            {
                page[4]
                    .Add(item.DateApplications[i].Date.ToString("u").Split()[0])
                    .Add(item.DateApplications[i].Nitrogen / ((item.IsTotalNitrogen) ? 10 : 1), 2, 10)
                    .Add(item.DateApplications[i].WaterMM);
            }
            for (var i = 0; i < nbGs; ++i)
            {
                page[4]
                    .Add(Phase.growthStageAsString(item.GrowthStageApplications[i].GrowthStage))
                    .Add(item.GrowthStageApplications[i].Nitrogen / ((item.IsTotalNitrogen) ? 10 : 1), 2, 10)
                    .Add(item.GrowthStageApplications[i].WaterMM);
            }
        }

        public static void PrintParameterItem(PageData page, CropParameterItem item)
        {
            page[6].Add(FileContainer.NonVarietyID);

            if (item != null)
            {
                page[7].Add(item.Name);

                // Write parameters
                foreach (KeyValuePair<string, double> pair in item.ParamValue)
                {
                    page[6].Add(pair.Key);
                    page[7].Add(pair.Value);
                }
            }
            else
                page[7].Add("?");
        }

        public static void PrintRunOptionItem(PageData page, RunOptionItem item)
        {
            page[9]
                .Add(FileContainer.RunOptionID)
                .Add("Use observed grain number")
                .Add("Unlimited water")
                .Add("Unlimited nitrogen")
                .Add("Unlimited temperature")
                .Add("Level of water deficit compensation (%)")
                .Add("Level of N deficit compensation (%)")
                .Add("Maximum temperature threshold")
                .Add("Simulate interactions")
                .Add("Simulate unlimited water")
                .Add("Simulate unlimited nitrogen")
                .Add("Simulate unlimited temperature")
                .Add("Simulate unlimited water and nitrogen")
                .Add("Simulate unlimited water and temperature")
                .Add("Simulate unlimited nitrogen and temperature")
                .Add("Simulate unlimited water, nitrogen and temperature");
            if (item != null)
            {
                page[10]
                    ///<Behnam>
                   .Add(item.Name)
                   .Add(item.UseObservedGrainNumber)
                   .Add(item.UnlimitedWater)
                   .Add(item.UnlimitedNitrogen)
                   .Add(item.UnlimitedTemperature)
                   .Add(item.WCompensationLevel)
                   .Add(item.NCompensationLevel)
                   .Add(item.MaxTempThreshold)
                   .Add(item.DoInteractions)
                   .Add(item.InteractionsW)
                   .Add(item.InteractionsN)
                   .Add(item.InteractionsT)
                   .Add(item.InteractionsWN)
                   .Add(item.InteractionsWT)
                   .Add(item.InteractionsNT)
                   .Add(item.InteractionsWNT);
                ///</Behnam>
            }
            else
            {
                page[10].Add("?");
            }
        }

        public static void PrintSiteItem(PageData page, SiteItem item)
        {
            var nbWF = (item != null) ? item.WeatherFiles.Count : 0;

            page[12]
                ///<Behnam>
                .Add(FileContainer.SiteID)
                .Add("Elevation (m)")
                .Add("Latitude (°)")
                .Add("Longitude (°)")
                .Add("Height of wind measurements (m)")
                .Add("Format")
                .Add("Sowing window type")
                .Add("Initial start of sowing window")
                .Add("Initial end of sowing window")
                .Add("Initial sowing window duration (days)")
                .Add("Minimum length of sowing window (days)")
                .Add("Temperature sum threshold (C)")
                .Add("Cumulative precipitation threshold (mm)")
                .Add("Average temperature threshold (C)")
                .Add("Number of checked days for temperature")
                .Add("Number of checked days for precipitation")
                .Add("Final start of sowing window")
                .Add("Final end of sowing window");

            for (var i = 0; i < nbWF; ++i)
            {
                page[12]
                    .Add("Weather file");
            }
            if (item != null)
            {
                page[13]
                    .Add(item.Name)
                    .Add(item.Elevation)
                    .Add(item.Latitude)
                    .Add(item.Longitude)
                    .Add(item.MeasurementHeight)
                    .Add(item.Format)
                    .Add(item.SowingWindowType)
                    .Add(item.MinSowingDate.ToString("u").Split()[0])
                    .Add(item.MaxSowingDate.ToString("u").Split()[0])
                    .Add(item.InitSowWindow)
                    .Add(item.MinSowWinLength)
                    .Add(item.TempSum)
                    .Add(item.PcpSum)
                    .Add(item.TempThr)
                    .Add(item.CheckDaysTemp)
                    .Add(item.CheckDaysPcp)
                    .Add(item.FinalMinSowingDate.ToString("u").Split()[0])
                    .Add(item.FinalMaxSowingDate.ToString("u").Split()[0]);
                ///</Behnam>
            }
            else
            {
                page[13]
                    .Add("?");
            }
            for (var i = 0; i < nbWF; ++i)
            {
                page[13]
                    // ReSharper disable PossibleNullReferenceException
                    .Add(item.WeatherFiles[i].File);
                // ReSharper restore PossibleNullReferenceException
            }
        }

        public static void PrintSoilItem(PageData page, SoilItem item)
        {
            var nbSl = (item != null) ? item.Layers.Count : 0;
            page[15]
                .Add(FileContainer.SoilID)
                .Add("N Mineralisation rate constant (1/day)")
                .Add("Organic N (gN/m²)")
                .Add("Denitrification  rate constant (1/day)")
                .Add("Percolation coefficient (dimensionless)")
                .Add("Minimum inorganic N (kgN/ha)")
                .Add("Total available water content (mm)");

            for (var i = 0; i < nbSl; ++i)
            {
                page[15]
                    .Add("Depth (m)")
                    .Add("Saturation (%)")
                    .Add("Field capacity (%)")
                    .Add("Permenent wilting point (%)");
            }

            if (item != null)
            {
                page[16]
                    .Add(item.Name)
                    .Add(item.Ko)
                    .Add(item.No)
                    .Add(item.Ndp)
                    .Add(item.Kq)
                    .Add(item.MinNir)
                    .Add(item.TotalAWC);
            }
            else
            {
                page[16]
                       .Add("?");
            }
            for (var i = 0; i < nbSl; ++i)
            {
                page[16]
                    // ReSharper disable PossibleNullReferenceException
                    .Add(item.Layers[i].Depth)
                    // ReSharper restore PossibleNullReferenceException
                    .Add(item.Layers[i].SSAT)
                    .Add(item.Layers[i].SDUL)
                    .Add(item.Layers[i].SLL);
            }
        }

        public static void PrintVarietyItem(PageData page, CropParameterItem item)
        {
            page[18].Add(FileContainer.VarietyID);

            if (item != null)
            {
                page[19].Add(item.Name);

                foreach (KeyValuePair<string, double> pair in item.ParamValue)
                {
                    page[18].Add(pair.Key);
                    page[19].Add(pair.Value);
                }
            }
            else
                page[19].Add("?");
        }

        public static void PrintDailyCrop(PageData page, Run runOld)
        {
            double cumulTTMaizePheno, cumulTTShoot;

            page.NewLine()
                .Add("Date")
                .Add("Thermal time (shoot)")
				.Add("Thermal time (Maize pheno)")
                .Add("Mean air")
                .Add("Mean soil")
                .Add("Mean canopy")
                .Add("Rooting")
                .Add("Cumulative")
                .Add("Cumulative")
                .Add("Cumulative")
                .Add("Available water")
                .Add("Water deficit")
                .Add("Available water")
                .Add("Water deficit")
                .Add("FPAW")
                .Add("Dry mass")
                .Add("Ear growth")
                .Add("Leaf expansion")
                .Add("Senescence")
                .Add("Transpiration")
                .Add("Cumulative N")
                .Add("Cumulative N")
                .Add("Cumulative")
                .Add("Available mineral N")
                .Add("Total mineral N")
                .Add("Available mineral N")
                .Add("Total mineral N")
                .Add("Nitrogen")
                .Add("Growth")
                .Add("Emerged")
                .Add("Shoot")
                .Add("Green")
                .Add("Leaf")
                .Add("Stem")
                .Add("Crop")
                .Add("Crop")
                .Add("Grain")
                .Add("Grain")
                .Add("Leaf")
                .Add("Leaf")
                .Add("Laminae")
                .Add("Laminae")
                .Add("Stem")
                .Add("Stem")
                .Add("Exposed sheath")
                .Add("Exposed sheath")
                .Add("Specific leaf")
                .Add("Specific leaf")
                .Add("Single grain")
                .Add("Starch")
                .Add("Single grain")
                .Add("Albumins-Globulins")
                .Add("Amphiphils")
                .Add("Gliadins")
                .Add("Glutenins")
                .Add("Post-anthesis")
                .Add("Maximum shoot")
                .Add("Cumulative")
                .Add("Cumulative")
                .Add("Vernalisation")
                .Add("Mean shoot")
                .Add("VPDair")
                .Add("VPDair-canopy")
                .Add("LER")
                .Add("deltaLERlai")
                .Add("WC3cm")
                .Add("LERPsi")
                .Add("WaterLimitedDeltaLAI(leaf 16)");

            page.NewLine()
                .Add("")
                .Add("after sowing")
                .Add("")
                .Add("temperature")
                .Add("temperature")
                .Add("temperature")
                .Add("depth")
                .Add("rain + irrigation")
                .Add("ET")
                .Add("water drainage")
                .Add("in soil profil")
                .Add("in soil profil")
                .Add("in root zone")
                .Add("in root zone")
                .Add("")
                .Add("drought factor")
                .Add("drought factor")
                .Add("drought factor")
                .Add("drought factor")
                .Add("drought factor")
                .Add("leaching")
                .Add("mineralisation")
                .Add("denitrification")
                .Add("in soil profil")
                .Add("in soil profil")
                .Add("in root zone")
                .Add("in root zone")
                .Add("nutrition index")
                .Add("stage")
                .Add("leaf number")
                .Add("number")
                .Add("area index")
                .Add("area index")
                .Add("length")
                .Add("dry matter")
                .Add("nitrogen")
                .Add("dry matter")
                .Add("nitrogen")
                .Add("dry matter")
                .Add("nitrogen")
                .Add("dry matter")
                .Add("nitrogen")
                .Add("dry matter")
                .Add("nitrogen")
                .Add("dry matter")
                .Add("nitrogen")
                .Add("nitrogen")
                .Add("dry mass")
                .Add("dry mass")
                .Add("per grain")
                .Add("nitrogen")
                .Add("per grain")
                .Add("per grain")
                .Add("per grain")
                .Add("per grain")
                .Add("nitrogen uptake")
                .Add("temperature")
                .Add("transpiration")
                .Add("evaporation")
                .Add("progress")
                .Add("temperature")
                .Add("")
                .Add("")
                .Add("")
                .Add("")
                .Add("")
                .Add("")
                .Add("");

            page.NewLine()
                .Add("yyyy-mm-dd")
                .Add("°Cd")
                .Add("°Cd")
                .Add("°C")
                .Add("°C")
                .Add("°C")
                .Add("m")
                .Add("mm")
                .Add("mm")
                .Add("mm")
                .Add("mm")
                .Add("mm")
                .Add("mm")
                .Add("mm")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("dimensionless")
                .Add("Zadoks stage")
                .Add("leaf/mainstem")
                .Add("shoot/m²")
                .Add("m²/m²")
                .Add("m²/m²")
                .Add("m")
                .Add("kgDM/ha")
                .Add("kgN/ha")
                .Add("kgDM/ha")
                .Add("kgN/ha")
                .Add("kgDM/ha")
                .Add("kgN/ha")
                .Add("kgDM/ha")
                .Add("kgN/ha")
                .Add("kgDM/ha")
                .Add("kgN/ha")
                .Add("kgDM/ha")
                .Add("kgN/ha")
                .Add("gN/m²")
                .Add("gDM/m²")
                .Add("mgDM/grain")
                .Add("mg/grain")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("kgN/ha")
                .Add("°C")
                .Add("mm")
                .Add("mm")
                .Add("")
                .Add("°C")
                .Add("hPa")
                .Add("hPa")
                .Add("")
                .Add("")
                .Add("")
                .Add("")
                .Add("");

            var nbDay = runOld.SavedUniverses.Count;
            var sowDate = runOld.SavedUniverses[nbDay - 1].Crop_.getDateOfStage(GrowthStage.ZC_00_Sowing);
            var sowDay = (int)(sowDate.Value - runOld.SavedUniverses[0].CurrentDate).TotalDays;

            for (var i = 1; i < nbDay; ++i)
            {
                var universe = runOld.SavedUniverses[i];

                if (i >= sowDay)
                {
                    cumulTTShoot = universe.thermalTimeWrapper_.getCumulTT(Delta.Shoot);
                    cumulTTMaizePheno = universe.thermalTimeWrapper_.getCumulTT(Delta.PhenoMaize);

                    page.NewLine()
                        .Add(universe.CurrentDate.ToString("u").Split()[0])
                        .Add(cumulTTShoot, 2)
                        .Add(cumulTTMaizePheno,2)
                        .Add(universe.Soil_.MeanAirTemperature, 2)
                        .Add(universe.Soil_.SoilMeanTemperature, 2)
                        .Add(universe.Soil_.MeanCanopyTemperature, 2)
                        .Add(universe.Crop_.RootLength, 2)
                        .Add(universe.Soil_.IncomeWater, 2, Run.GwaterToMMwater)
                        .Add(universe.Soil_.AccumulatedActEvapoTranspiration, 2, Run.GwaterToMMwater)
                        .Add(universe.Soil_.SoilDeepLayer_.LostWater, 2, Run.GwaterToMMwater)
                        .Add(universe.Soil_.CalculateAvailableExcessW(), 2, Run.GwaterToMMwater)
                        .Add(universe.Soil_.CalculateWaterDeficit(), 2, Run.GwaterToMMwater)
                        .Add(universe.Soil_.CalculateRootZoneAvailableWater(), 2, Run.GwaterToMMwater)
                        .Add(universe.Soil_.CalculateRootZoneWaterDeficit(), 2, Run.GwaterToMMwater)
                        .Add(universe.Soil_.FPAW, 2)
                        .Add(universe.Soil_.DBF, 2)
                        .Add(universe.Soil_.DEBF, 2)
                        .Add(universe.Crop_.DEF, 2)
                        .Add(universe.Crop_.DSF, 2)
                        .Add(universe.Soil_.DTF, 2)
                        .Add(universe.Soil_.SoilDeepLayer_.LostNitrogen, 2, 10)
                        .Add(universe.Soil_.AccumulatedNitrogenMineralisation, 2, 10)
                        .Add(universe.Soil_.AccumulatedNitrogenDenitrification, 2, 10)
                        .Add(universe.Soil_.CalculateAvailableExcessN(), 2, 10)
                        .Add(universe.Soil_.CalculateTotalN(), 2, 10)
                        .Add(universe.Soil_.DailyAvNforRoots, 2, 10)
                        .Add((universe.Soil_.DailyAvNforRoots + universe.Soil_.DailyUnavNforRoots), 2, 10)
                        .Add(universe.Crop_.NNI, 3)
                            .Add(universe.Crop_.getLastGrowthStageSet())
                            .Add(universe.Crop_.LeafNumber, 2)
                            .Add(universe.Crop_.CanopyShootNumber, 0)
                            .Add(universe.Crop_.GAI, 8)
                            .Add(universe.Crop_.LaminaeAI, 8)
                            .Add(universe.Crop_.SumInternodesLength, 4, Run.CMToM)
                        .Add(universe.Crop_.OutputTotalDM, 2, 10)
                        .Add(universe.Crop_.OutputTotalN, 2, 10)
                            .Add(universe.Crop_.GrainTotalDM, 2, 10)
                            .Add(universe.Crop_.GrainTotalN, 2, 10)
                        //leaf
                            .Add((universe.Crop_.OutputLaminaeDM + universe.Crop_.OutputSheathDM), 2, 10)
                            .Add((universe.Crop_.OutputLaminaeN + universe.Crop_.OutputSheathN), 2, 10)
                        //laminae
                            .Add(universe.Crop_.OutputLaminaeDM, 2, 10)
                            .Add(universe.Crop_.OutputLaminaeN, 2, 10)
                        //stem
                            .Add((universe.Crop_.OutputSheathDM + universe.Crop_.StemTotalDM), 2, 10)
                            .Add((universe.Crop_.OutputSheathN + universe.Crop_.StemTotalN), 2, 10)
                        //sheath
                            .Add(universe.Crop_.OutputSheathDM, 2, 10)
                            .Add(universe.Crop_.OutputSheathN, 2, 10)
                            .Add((universe.Crop_.LaminaeSLN), 2)
                            .Add((universe.Crop_.LaminaeSLW), 2)
                            .Add(universe.Crop_.TotalDMperGrain, 4)
                            .Add(universe.Crop_.Starch, 4)
                            .Add(universe.Crop_.TotalNperGrain, 4)
                            .Add(universe.Crop_.NalbGlo, 4)
                            .Add(universe.Crop_.Namp, 4)
                            .Add(universe.Crop_.Ngli, 4)
                            .Add(universe.Crop_.Nglu, 4)
                        .Add(universe.Crop_.PostAnthesisNUptake, 0, 10)
                        .Add(universe.ShootTemperature_.MaxShootTemperature, 2)
                        /// Behnam (2016.02.23): Transpiration and evaporation are now cummulative.
                        .Add(universe.Soil_.AccumulatedActTranspiration, 2, Run.GwaterToMMwater)
                        .Add(universe.Soil_.AccumulatedActSoilEvaporation, 2, Run.GwaterToMMwater)
                        .Add(universe.Crop_.VernaProgress, 3)
                        .Add(universe.ShootTemperature_.MeanShootTemperature, 2)
                        .Add(universe.meteorologyWrapper_.VPDair, 2)
                        .Add(universe.meteorologyWrapper_.VPDairCanopy, 2)
                    .Add(universe.Crop_.LER, 3)
                    .Add(universe.Crop_.getPotentialIncDeltaArea(),8)
                    .Add(universe.Soil_.WC_3cm, 3)
                    .Add(PsiSol.CalcPsi(universe.Soil_.FPAW),4)
                    .Add(universe.Crop_.getWaterLimitedPotDeltaAI(15));
                }
            }
        }

        public static void PrintDailyWaterSoil(PageData page, Run runOld)
        {
            var titleLine = new LineData()
                .Add("Date").Add("Available soil water (mm)").SetWidth(51)
                .Add("Date").Add("Excess soil water (mm)").SetWidth(102)
                .Add("Date").Add("Unvailable soil water (mm)").SetWidth(153);
            page.Add(titleLine);

            var nbLine = new LineData();
            var nbSoilLayer = GetNbSoilLayer(runOld);
            for (var i = 1; i < 4; ++i)
            {
                nbLine.Add("yyyy-mm-dd");
                for (var j = 0; j < nbSoilLayer; ++j)
                {
                    nbLine.Add((j + 1) * Layer.Thickness);
                }
                nbLine.SetWidth(51 * i);
            }
            page.Add(nbLine);

            var nbDay = runOld.SavedUniverses.Count;
            for (var i = 0; i < nbDay; ++i)
            {
                var universe = runOld.SavedUniverses[i];
                var soil = universe.Soil_;
                var valueLine = new LineData();
                var dateValue = universe.CurrentDate.ToString("u").Split()[0];


                valueLine.Add(dateValue);
                for (var j = 0; j < nbSoilLayer; ++j)
                {
                    valueLine.Add(soil.Layers[j].AvWater, 2, Run.GwaterToMMwater);
                }
                valueLine.SetWidth(51);

                valueLine.Add(dateValue);
                for (var j = 0; j < nbSoilLayer; ++j)
                {
                    valueLine.Add(soil.Layers[j].ExWater, 2, Run.GwaterToMMwater);
                }
                valueLine.SetWidth(102);

                valueLine.Add(dateValue);
                for (var j = 0; j < nbSoilLayer; ++j)
                {
                    valueLine.Add(soil.Layers[j].UnavWater, 2, Run.GwaterToMMwater);
                }
                valueLine.SetWidth(153);

                page.Add(valueLine);
            }
        }

        public static void PrintDailyNitrogenSoil(PageData page, Run runOld)
        {
            var titleLine = new LineData()
                .Add("Date").Add("Available soil  N (kgN/ha)").SetWidth(51)
                .Add("Date").Add("Excess soil N (kgN/ha)").SetWidth(102)
                .Add("Date").Add("Unvailable soil N (kgN/ha)").SetWidth(153);
            page.Add(titleLine);

            var nbLine = new LineData();
            var nbSoilLayer = GetNbSoilLayer(runOld);
            for (var i = 1; i < 4; ++i)
            {
                nbLine.Add("yyyy-mm-dd");
                for (var j = 0; j < nbSoilLayer; ++j)
                {
                    nbLine.Add((j + 1) * Layer.Thickness);
                }
                nbLine.SetWidth(51 * i);
            }
            page.Add(nbLine);

            var nbDay = runOld.SavedUniverses.Count;
            for (var i = 0; i < nbDay; ++i)
            {
                var universe = runOld.SavedUniverses[i];
                var soil = universe.Soil_;
                var valueLine = new LineData();
                var dateValue = universe.CurrentDate.ToString("u").Split()[0];


                valueLine.Add(dateValue);
                for (var j = 0; j < nbSoilLayer; ++j)
                {
                    valueLine.Add((soil.Layers[j].AvN), 2, 10);
                }
                valueLine.SetWidth(51);

                valueLine.Add(dateValue);
                for (var j = 0; j < nbSoilLayer; ++j)
                {
                    valueLine.Add((soil.Layers[j].ExN), 2, 10);
                }
                valueLine.SetWidth(102);

                valueLine.Add(dateValue);
                for (var j = 0; j < nbSoilLayer; ++j)
                {
                    valueLine.Add((soil.Layers[j].UnavN), 2, 10);
                }
                valueLine.SetWidth(153);

                page.Add(valueLine);
            }
        }

        public static void PrintDailyLaminae(PageData page, Run runOld)
        {
            page.NewLine()
                .Add("Date").Add("Laminae surface area per layer (m²/m²)").SetWidth(21)
                .Add("Date").Add("Laminae Dry mass per layer (kgDM/ha)").SetWidth(42)
                .Add("Date").Add("Laminae nitrogen per layer (kgN/ha)").SetWidth(63)
                .Add("Date").Add("Specific leaf nitrogen per layer (gN/m²)").SetWidth(84)
                .Add("Date").Add("Specific leag nitrogen per layer (gDM/m²)").SetWidth(105);

            page.NewLine();

            var finalLeafNum = GetFinalLeafNumber(runOld);
            for (var i = 0; i < 5; ++i)
            {
                page[page.Count - 1].Add("yyyy-mm-dd");
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(j + 1);
                }
                page[page.Count - 1].SetWidth(21 * (i + 1));
            }


            var nbDay = runOld.SavedUniverses.Count;
            for (var i = 0; i < nbDay; ++i)
            {
                var universe = runOld.SavedUniverses[i];
                var dateValue = universe.CurrentDate.ToString("u").Split()[0];
                page.NewLine().Add(dateValue);

                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(universe.Crop_.getLeafLaminaAreaIndexForLeafLayer(j), 4);
                }
                page[page.Count - 1].SetWidth(21);

                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(universe.Crop_.getLeafLaminaTotalDMForLeafLayer(j), 4, 10);
                }
                page[page.Count - 1].SetWidth(42);

                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(universe.Crop_.getLeafLaminaTotalNForLeafLayer(j), 4, 10);
                }
                page[page.Count - 1].SetWidth(63);

                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(universe.Crop_.getLeafLaminaSpecificNForLeafLayer(j), 2);
                }
                page[page.Count - 1].SetWidth(84);

                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(universe.Crop_.getLeafLaminaSpecificWeightForLeafLayer(j), 2);
                }
                page[page.Count - 1].SetWidth(105);
            }

        }

        public static void PrintDailySheath(PageData page, Run runOld)
        {
            page.NewLine()
                .Add("Date").Add("Sheath surface area per layer (m²/m²)").SetWidth(21)
                .Add("Date").Add("Sheath dry mass per layer (kgDM/ha)").SetWidth(42)
                .Add("Date").Add("Sheath nitrogen per layer (kgN/ha)").SetWidth(63)
                .Add("Date").Add("Sheath specific  nitrogen per layer (gN/m²)").SetWidth(84)
                .Add("Date").Add("Sheath specific dry mass per layer (gDM/m²)").SetWidth(105);

            page.NewLine();

            var finalLeafNum = GetFinalLeafNumber(runOld);
            for (var i = 0; i < 5; ++i)
            {
                page[page.Count - 1].Add("yyyy-mm-dd");
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(j + 1);
                }
                page[page.Count - 1].SetWidth(21 * (i + 1));
            }

            var nbDay = runOld.SavedUniverses.Count;
            for (var i = 0; i < nbDay; ++i)
            {
                var universe = runOld.SavedUniverses[i];
                var dateValue = universe.CurrentDate.ToString("u").Split()[0];
                page.NewLine().Add(dateValue);

                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(universe.Crop_.getExposedSheathAreaIndexForLeafLayer(j), 4);
                }
                page[page.Count - 1].SetWidth(21);

                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(universe.Crop_.getExposedSheathTotalDMForLeafLayer(j), 4, 10);
                }
                page[page.Count - 1].SetWidth(42);

                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(universe.Crop_.getExposedSheathTotalNForLeafLayer(j), 4, 10);
                }
                page[page.Count - 1].SetWidth(63);

                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(universe.Crop_.getExposedSheathSpecificNForLeafLayer(j), 2);
                }
                page[page.Count - 1].SetWidth(84);

                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(universe.Crop_.getExposedSheathSpecificWeightForLeafLayer(j), 2);
                }
                page[page.Count - 1].SetWidth(105);
            }
        }

        public static void PrintDailyInternodes(PageData page, Run runOld)
        {
            page.NewLine().Add("Date")
                .Add("Internode length (m)").SetWidth(22);

            var finalLeafNum = GetFinalLeafNumber(runOld);

            page.NewLine();
            page[page.Count - 1].Add("yyyy-mm-dd");
            for (var j = finalLeafNum; j >= 0; --j)
            {
                page[page.Count - 1].Add(j + 1);
            }
            page[page.Count - 1].SetWidth(22);

            var nbDay = runOld.SavedUniverses.Count;
            for (var i = 0; i < nbDay; ++i)
            {
                var universe = runOld.SavedUniverses[i];
                var dateValue = universe.CurrentDate.ToString("u").Split()[0];
                page.NewLine().Add(dateValue);

                page[page.Count - 1].Add(universe.Crop_.getEarPeduncleInterNodeLength(), 4);

                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(universe.Crop_.getInterNodeLengthForLeafLayer(j), 4);
                }
                page[page.Count - 1].SetWidth(22);
            }
        }

        //Create a new method to print out the daily leaf area index for each tiller. Modified by J.He, 6-19-2009.
        public static void PrintDailyTillerLaminaeAreaIndex(PageData page, Run runOld)
        {
            page.NewLine()
                .Add("Date").Add("Main-stem laminae surface area per layer (m²/m²)").SetWidth(21)
                .Add("Date").Add("Tiller 1 laminae surface area per layer (m²/m²)").SetWidth(42)
                .Add("Date").Add("Tiller 2 laminae surface area per layer (m²/m²)").SetWidth(63)
                .Add("Date").Add("Total laminae surface area per layer (m²/m²)").SetWidth(84);

            page.NewLine();

            var finalLeafNum = GetFinalLeafNumber(runOld);

            for (var i = 0; i < 3; ++i)
            {
                page[page.Count - 1].Add("yyyy-mm-dd");
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(j + 1);
                }
                page[page.Count - 1].SetWidth(21 * (i + 1));
            }


            var nbDay = runOld.SavedUniverses.Count;
            for (var i = 0; i < nbDay; ++i)
            {
                Universe universe = runOld.SavedUniverses[i];
                string dateValue = universe.CurrentDate.ToString("u").Split()[0];
                #region Print Daily Laminea Area Index of main stem.
                page.NewLine().Add(dateValue);

                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(universe.Crop_.getPotentialLaminaTillerAreaIndexForLeafLayer(j, 0), 4);
                }

                page[page.Count - 1].SetWidth(21);
                #endregion

                #region Print Daily Laminea Area Index of Tiller 1.
                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    // RunInstance.TillerNumber > 1 to avoid crash when their is no tiller. Modified by P.Strato, 6-29-2009.
                    if (universe.Crop_.TillerNumber > 1)
                    {
                        page[page.Count - 1].Add(universe.Crop_.getPotentialLaminaTillerAreaIndexForLeafLayer(j, 1), 4);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(42);
                #endregion

                #region Print Daily Laminea Area Index of Tiller 2.
                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    // RunInstance.TillerNumber > 2 to avoid crash when their is only one tiller. Modified by P.Strato, 6-29-2009.
                    if (universe.Crop_.TillerNumber > 2)
                    {
                        page[page.Count - 1].Add(universe.Crop_.getPotentialLaminaTillerAreaIndexForLeafLayer(j, 2), 4);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(63);
                #endregion

                #region Print Daily Laminea Area Index of total tillers.
                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    // RunInstance.TillerNumber > 3 to avoid crash when their is only two tiller. Modified by P.Strato, 6-29-2009.
                    if (universe.Crop_.TillerNumber > 3)
                    {
                        page[page.Count - 1].Add(universe.Crop_.getPotentialLaminaAreaIndexForLeafLayer(j), 4);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(84);
                #endregion

            }

        }

        //Create a new method to print out the daily sheath area index for each tiller. Modified by J.He, 6-19-2009.
        public static void PrintDailyTillerSheathAreaIndex(PageData page, Run runOld)
        {
            page.NewLine()
                .Add("Date").Add("Main-stem exposed sheath surface area per layer (m²/m²)").SetWidth(21)
                .Add("Date").Add("Tiller 1 exposed sheath surface area per layer (m²/m²)").SetWidth(42)
                .Add("Date").Add("Tiller 2 exposed sheath surface area per layer (m²/m²)").SetWidth(63)
                .Add("Date").Add("Total exposed sheath surface area per layer (m²/m²)").SetWidth(84);

            page.NewLine();

            var finalLeafNum = GetFinalLeafNumber(runOld);

            for (var i = 0; i < 3; ++i)
            {
                page[page.Count - 1].Add("yyyy-mm-dd");
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(j + 1);
                }
                page[page.Count - 1].SetWidth(21 * (i + 1));
            }


            var nbDay = runOld.SavedUniverses.Count;
            for (var i = 0; i < nbDay; ++i)
            {
                Universe universe = runOld.SavedUniverses[i];

                string dateValue = universe.CurrentDate.ToString("u").Split()[0];
                #region Print Daily Sheath Area Index of main stem.
                page.NewLine().Add(dateValue);

                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    page[page.Count - 1].Add(universe.Crop_.getPotentialSheathTillerAreaIndexForLeafLayer(j, 0), 4);
                }

                page[page.Count - 1].SetWidth(21);
                #endregion

                #region Print Daily Sheath Area Index of Tiller 1.
                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    // RunInstance.TillerNumber > 1 to avoid crash when their is no tiller. Modified by P.Strato, 6-29-2009.
                    if (universe.Crop_.TillerNumber > 1)
                    {
                        page[page.Count - 1].Add(universe.Crop_.getPotentialSheathTillerAreaIndexForLeafLayer(j, 1), 4);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(42);
                #endregion

                #region Print Daily Sheath Area Index of Tiller 2.
                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    // RunInstance.TillerNumber > 2 to avoid crash when their is only one tiller. Modified by P.Strato, 6-29-2009.
                    if (universe.Crop_.TillerNumber > 2)
                    {
                        page[page.Count - 1].Add(universe.Crop_.getPotentialSheathTillerAreaIndexForLeafLayer(j, 2), 4);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(63);
                #endregion

                #region Print Daily Sheath Area Index of total tillers.
                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    // RunInstance.TillerNumber > 3 to avoid crash when their is only two tiller. Modified by P.Strato, 6-29-2009.
                    if (universe.Crop_.TillerNumber > 3)
                    {
                        page[page.Count - 1].Add(universe.Crop_.getPotentialSheathAreaIndexForLeafLayer(j), 4);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(84);
                #endregion

            }
            
        }
        public static void PrintDailyHourlyVPDair(PageData page, Run runOld)
        {
            page.NewLine()
                .Add("Date").Add("hourlyVPDairLeaf").SetWidth(21);

            page.NewLine();

            page[page.Count - 1].Add("yyyy-mm-dd");
            for (var j = 0; j <24; j++)
            {
                page[page.Count - 1].Add(j + 1);
            }

            var nbDay = runOld.SavedUniverses.Count;
            for (var i = 0; i < nbDay; ++i)
            {
                var universe = runOld.SavedUniverses[i];
                var dateValue = universe.CurrentDate.ToString("u").Split()[0];
                page.NewLine().Add(dateValue);

                for (var j = 0 ; j <24 ; j++)
                {
                    page[page.Count - 1].Add(universe.Soil_.VPDairLeaf[j], 4);
                }
                page[page.Count - 1].SetWidth(21);
            }
        }

        public static void PrintDailyHourlyCanopyTemp(PageData page, Run runOld)
        {
            page.NewLine()
                .Add("Date").Add("HourlyCanopyTemp").SetWidth(21);

            page.NewLine();

            page[page.Count - 1].Add("yyyy-mm-dd");
            for (var j = 0; j < 24; j++)
            {
                page[page.Count - 1].Add(j + 1);
            }

            var nbDay = runOld.SavedUniverses.Count;
            for (var i = 0; i < nbDay; ++i)
            {
                var universe = runOld.SavedUniverses[i];
                var dateValue = universe.CurrentDate.ToString("u").Split()[0];
                page.NewLine().Add(dateValue);

                for (var j = 0; j < 24; j++)
                {
                    page[page.Count - 1].Add(universe.Soil_.HourlyCanopyTemperature[j], 4);
                }
                page[page.Count - 1].SetWidth(21);
            }
        }

        public static void PrintDailyHourlyRad(PageData page, Run runOld)
        {
            page.NewLine()
                .Add("Date").Add("HourlyRad").SetWidth(21);

            page.NewLine();

            page[page.Count - 1].Add("yyyy-mm-dd");
            for (var j = 0; j < 24; j++)
            {
                page[page.Count - 1].Add(j + 1);
            }

            var nbDay = runOld.SavedUniverses.Count;
            for (var i = 0; i < nbDay; ++i)
            {
                var universe = runOld.SavedUniverses[i];
                var dateValue = universe.CurrentDate.ToString("u").Split()[0];
                page.NewLine().Add(dateValue);

                for (var j = 0; j < 24; j++)
                {
                    page[page.Count - 1].Add(universe.meteorologyWrapper_.HourlyRadiation[j], 4);
                }
                page[page.Count - 1].SetWidth(21);
            }
        }

        public static void PrintLeafLength(PageData page, Run runOld)
        {
            page.NewLine()
                .Add("Date").Add("Length per layer").SetWidth(21);

            page.NewLine();

            var finalLeafNum = GetFinalLeafNumber(runOld);
            for (var i = 0; i < 5; ++i)
            {
                page[page.Count - 1].Add("yyyy-mm-dd");
                for (var j = 0 ; j<finalLeafNum ; j++)
                {
                    page[page.Count - 1].Add(j + 1);
                }
                page[page.Count - 1].SetWidth(21 * (i + 1));
            }

            var nbDay = runOld.SavedUniverses.Count;
            for (var i = 0; i < nbDay; ++i)
            {
                var universe = runOld.SavedUniverses[i];
                var dateValue = universe.CurrentDate.ToString("u").Split()[0];
                page.NewLine().Add(dateValue);

                for (var j =0; j< finalLeafNum; j++)
                {
                    page[page.Count - 1].Add(universe.Crop_.getLeafLength(j)*0.1, 2);
                }
                page[page.Count - 1].SetWidth(21);
            }
        }
        public static void PrintLeafWidth(PageData page, Run runOld)
        {
            page.NewLine()
                .Add("Date").Add("Width per layer").SetWidth(21);

            page.NewLine();

            var finalLeafNum = GetFinalLeafNumber(runOld);
            for (var i = 0; i < 5; ++i)
            {
                page[page.Count - 1].Add("yyyy-mm-dd");
                for (var j = 0; j < finalLeafNum; j++)
                {
                    page[page.Count - 1].Add(j + 1);
                }
                page[page.Count - 1].SetWidth(21 * (i + 1));
            }

            var nbDay = runOld.SavedUniverses.Count;
            for (var i = 0; i < nbDay; ++i)
            {
                var universe = runOld.SavedUniverses[i];
                var dateValue = universe.CurrentDate.ToString("u").Split()[0];
                page.NewLine().Add(dateValue);

                for (var j = 0; j < finalLeafNum; j++)
                {
                    page[page.Count - 1].Add(universe.Crop_.getLeafWidth(j)*0.1, 2);
                }
                page[page.Count - 1].SetWidth(21);
            }
        }
        #endregion

        #region multi

        internal override PageData MultiRunHeader()
        {
            var page = new PageData();

            // Print summary file main header
            PrintVersionDate(page);
            PrintFiles(page);

            page.NewLine();
            PrintMultiRunHeader(page);
            return page;
        }

        public static void PrintMultiRunHeader(PageData page)
        {
            page.NewLine()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()

                .AddNull()

                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()

                .AddNull()

                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()

                .Add("Available water")
                .Add("Available mineral N")
                .Add("Total mineral N")
                .Add("Available water")
                .Add("Available mineral N")
                .Add("Total mineral N")

                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull();


            page.NewLine()
                .Add(FileContainer.ManagementID)
                .Add(FileContainer.NonVarietyID)
                .Add(FileContainer.RunOptionID)
                .Add(FileContainer.SiteID)
                .Add(FileContainer.SoilID)
                .Add(FileContainer.VarietyID)

                .AddNull()

                .Add("Sowing year")
                .Add(Phase.growthStageAsString(GrowthStage.ZC_00_Sowing))
                .Add(Phase.growthStageAsString(GrowthStage.ZC_10_Emergence))
                .Add(Phase.growthStageAsString(GrowthStage.BeginningStemExtension))
                .Add(Phase.growthStageAsString(GrowthStage.ZC_65_Anthesis))
                .Add(Phase.growthStageAsString(GrowthStage.ZC_75_EndCellDivision))
                .Add(Phase.growthStageAsString(GrowthStage.ZC_91_EndGrainFilling))
                .Add(Phase.growthStageAsString(GrowthStage.ZC_92_Maturity))

                .AddNull()

                .Add("Final")
                .Add("Leaf area index")
                .Add("Green area index")
                .Add("Crop dry mass")
                .Add("Crop dry mass")
                .Add("Grain dry mass")
                .Add("Crop nitrogen")
                .Add("Crop nitrogen")
                .Add("Grain nitrogen")
                .Add("Post-anthesis crop")
                .Add("Single grain")
                .Add("Single grain")
                .Add("Grain protein")
                .Add("Grain")
                .Add("Grain starch")
                .Add("Albumins-globulins")
                .Add("Amphiphils")
                .Add("Gliadins")
                .Add("Glutenins")
                .Add("Gliadins")
                .Add("Gluteins")
                .Add("Gliadins to")
                .Add("Dry mass ")
                .Add("Nitrogen")
                .Add("Cumulative")
                .Add("Cumulative")
                .Add("Cumulative available")
                .Add("Cumulative nitrogen")
                .Add("Cumulative water")
                .Add("Cumulative")
                .Add("Cumulative")
                .Add("in soil profil")
                .Add("in soil profil")
                .Add("in soil profil")
                .Add("in root zone")
                .Add("in root zone")
                .Add("in root zone")
                .Add("Nitrogen use")
                .Add("Nitrogen utilisation")
                .Add("Nitrogen upake")
                .Add("Water use")
                .Add("Cumulative")
                .Add("Cumulative");

            page.NewLine()

                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()

                .Add("leaf number")
                .Add("at anthesis")
                .Add("at anthesis")
                .Add("at anthesis")
                .Add("at maturity")
                .Add("at maturity")
                .Add("at anthesis")
                .Add("at maturity")
                .Add("at maturity")
                .Add("nitrogen uptake")
                .Add("dry mass")
                .Add("nitrogen")
                .Add("concentration")
                .Add("number")
                .Add("concentration")
                .Add("per grain")
                .Add("per grain")
                .Add("per grain")
                .Add("per grain")
                .Add("concentration")
                .Add("concentration")
                .Add("gluteins ratio")
                .Add("harvest index")
                .Add("harvest index")
                .Add("rain + irrigation")
                .Add("ET")
                .Add("soil nitrogen")
                .Add("leaching")
                .Add("drainage")
                .Add("mineralisation")
                .Add("denitrification")
                .Add("at maturity")
                .Add("at maturity")
                .Add("at maturity")
                .Add("at maturity")
                .Add("at maturity")
                .Add("at maturity")
                .Add("efficiency")
                .Add("efficiency")
                .Add("efficiency")
                .Add("efficiency")
                .Add("Evaporation")
                .Add("Transpiration");

            page.NewLine()

                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()

                .Add("yyyy")
                .Add("yyyy-mm-dd")
                .Add("yyyy-mm-dd")
                .Add("yyyy-mm-dd")
                .Add("yyyy-mm-dd")
                .Add("yyyy-mm-dd")
                .Add("yyyy-mm-dd")
                .Add("yyyy-mm-dd")

                .AddNull()

                .Add("leaf/mainstem")
                .Add("m²/m²")
                .Add("m²/m²")
                .Add("kgDM/ha")
                .Add("kgDM/ha")
                .Add("kgDM/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("mgDM/grain")
                .Add("mgN/grain")
                .Add("% grain dry mass")
                .Add("grain/m²")
                .Add("% grain dry mass")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("% grain nitrogen")
                .Add("% grain nitrogen")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("mm")
                .Add("mm")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("mm")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("mm")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("mm")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("kgDM/kgN")
                .Add("kgDM/kgN")
                .Add("kgN/kgN")
                .Add("kgDM/ha/mm")
                .Add("mm")
                .Add("mm");
        }

        internal override LineData MultiRunLine(Run runOld)
        {
            var line = new LineData();

            if (runOld.ManagementDef != null) line.Add(runOld.ManagementDef.Name); else line.Add("?");
            if (runOld.ParameterDef != null) line.Add(runOld.ParameterDef.Name); else line.Add("?");
            if (runOld.RunOptionDef != null) line.Add(runOld.RunOptionDef.Name); else line.Add("?");
            if (runOld.SiteDef != null) line.Add(runOld.SiteDef.Name); else line.Add("?");
            if (runOld.SoilDef != null) line.Add(runOld.SoilDef.Name); else line.Add("?");
            if (runOld.VarietyDef != null) line.Add(runOld.VarietyDef.Name); else line.Add("?");

            line.AddNull();
            ///<Behnam (2015.10.20)>
            ///<Comment>Use the year of the start of sowing window as the sowing year, if applicable</Comment>
            if (runOld.ManagementDef.IsSowDateEstimate)
            {
                line.Add(runOld.SiteDef != null ? runOld.SiteDef.FinalMinSowingDate.Year.ToString() : "?");
            }
            else
            {
                line.Add(runOld.ManagementDef != null ? runOld.ManagementDef.FinalSowingDate.Year.ToString() : "?");
            }
            ///</Behnam>

            PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_00_Sowing);
            PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_10_Emergence);
            PrintMultiRunSummaryDate(line, runOld, GrowthStage.BeginningStemExtension);
            PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_65_Anthesis);
            PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_75_EndCellDivision);
            PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_91_EndGrainFilling);
            PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_92_Maturity);

            line.AddNull();

            PrintMultiRunSummaryCrop(line, runOld);

            return line;
        }

        public static void PrintMultiRunSummaryDate(LineData line, Run runOld, GrowthStage moment)
        {
            var lastUniverse = GetLastUniverse(runOld);
            if (lastUniverse != null)
            {
                // pm 29 April 2015: need to remove 1 day to report the growth stage on the day 
                //                   they were simulated  
                var momentDate = lastUniverse.Crop_.getDateOfStage(moment);

                ///<Behnam (2015.09.18)>
                ///<Comment>
                ///Previously it was required to subtract one day to report model state variables at specific
                ///growth stages, except ZC_00_Sowing and ZC_39_FlagLeafLiguleJustVisible.
                ///By this modification, the model stores the dates correctly and they need not to be changed
                ///when reporting or when retrieving their values.
                ///</Comment>

                // if (lastUniverse.Crop_.calendar[moment].HasValue && moment != GrowthStage.ZC_00_Sowing &&
                // moment != GrowthStage.ZC_39_FlagLeafLiguleJustVisible)
                // {
                // momentDate = lastUniverse.Crop_.calendar[moment].Value.AddDays(-1);
                // }

                ///</Behnam>

                line.Add(momentDate.HasValue ? momentDate.Value.ToString("yyyy-MM-dd") : "?");
            }
            else line.Add("?");
        }

        public static void PrintMultiRunSummaryCrop(LineData line, Run runOld)
        {
            var anthesisUniverse = GetUniverse(runOld, GrowthStage.ZC_65_Anthesis);
            var maturityUniverse = GetUniverse(runOld, GrowthStage.ZC_92_Maturity);

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.LeafNumber, 2);
            else line.Add("?");

            if (anthesisUniverse != null) line.Add(anthesisUniverse.Crop_.LaminaeAI, 2);
            else line.Add("?");

            if (anthesisUniverse != null) line.Add(anthesisUniverse.Crop_.GAI, 2);
            else line.Add("?");

            if (anthesisUniverse != null) line.Add(anthesisUniverse.Crop_.OutputTotalDM, 0, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.OutputTotalDM, 0, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.GrainTotalDM, 0, 10);
            else line.Add("?");

            if (anthesisUniverse != null) line.Add(anthesisUniverse.Crop_.OutputTotalN, 0, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.OutputTotalN, 0, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.GrainTotalN, 0, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.PostAnthesisNUptake, 0, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.TotalDMperGrain, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.TotalNperGrain, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.ProteinConcentration, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.GrainNumber, 0);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Starch, 4); 
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.PercentStarch, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Namp, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Ngli, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Nglu, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.PercentGli, 3);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.PercentGlu, 3);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.GliadinsToGluteins, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.HarvestIndexDM, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.HarvestIndexN, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.IncomeWater, 1, Run.GwaterToMMwater);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.AccumulatedActEvapoTranspiration, 2, Run.GwaterToMMwater);
            else line.Add("?");

            if (maturityUniverse != null) line.Add((maturityUniverse.Soil_.CalculateTotalN() + maturityUniverse.Soil_.SoilDeepLayer_.LostNitrogen + maturityUniverse.Crop_.CropTotalN), 0, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.SoilDeepLayer_.LostNitrogen, 2, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.SoilDeepLayer_.LostWater, 1, Run.GwaterToMMwater);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.AccumulatedNitrogenMineralisation, 2, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.AccumulatedNitrogenDenitrification, 2, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.CalculateAvailableExcessW(), 2, Run.GwaterToMMwater);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.CalculateAvailableExcessN(), 2, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.CalculateTotalN(), 2, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.CalculateRootZoneAvailableExcessWater(), 2, Run.GwaterToMMwater);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.CalculateRootZoneAvailableExcessN(), 2, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.CalculateRootZoneTotalN(), 2, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.NuseEfficiency, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.NutilisationEfficiency, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.NuptakeEfficiency, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.WaterUseEfficiency, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.AccumulatedActSoilEvaporation, 2, Run.GwaterToMMwater);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.AccumulatedActTranspiration, 2, Run.GwaterToMMwater);
            else line.Add("?");
        }

        public static IEnumerable<GrowthStage> GrowthStageToOutput()
        {
            yield return GrowthStage.ZC_00_Sowing;
            yield return GrowthStage.BeginningStemExtension;
            yield return GrowthStage.ZC_65_Anthesis;
            yield return GrowthStage.ZC_75_EndCellDivision;
            yield return GrowthStage.ZC_91_EndGrainFilling;
            yield return GrowthStage.ZC_92_Maturity;
        }

        public static IEnumerable<string> GrowthStageHeaders1()
        {
            return GrowthStageToOutput().Select(g => Phase.growthStageAsString(g));
        }

        public static IEnumerable<string> GrowthStageHeaders2()
        {
            return GrowthStageToOutput().Select(g => "DOY");
        }

        public static IEnumerable<string> GrowthStageValues(Run run)
        {
            var lastUniverse = GetLastUniverse(run);
            if (lastUniverse != null)
            {
                return GrowthStageToOutput().Select(g =>
                {
                    var date = lastUniverse.Crop_.getDateOfStage(g);
                    return date.HasValue ? date.Value.DayOfYear.ToString() : "?";
                });
            }
            return GrowthStageToOutput().Select(g => "?");
        }

        public static IEnumerable<string> SummaryHeaders1()
        {
            yield return "Final leaf number";
            yield return "LAI at anthesis";
            yield return "GAI at anthesis";
            yield return "Crop DM at anthesis";
            yield return "Crop DM at maturity";
            yield return "Grain DM at maturity";
            yield return "Crop N at anthesis";
            yield return "Crop N at maturity";
            yield return "Grain N at maturity";
            yield return "Post-anthesis crop N uptake";
            yield return "Single grain DM at maturity";
            yield return "Single grain N at maturity";
            yield return "Grain protein concentration at maturity";
            yield return "Grain number";
            yield return "% Starch at maturity";
            yield return "Albumins-globulins at maturity";
            yield return "Amphiphils at maturity";
            yield return "Gliadins at maturity";
            yield return "Glutenins at maturity";
            yield return "% Gliadins at maturity";
            yield return "% Gluteins at maturity";
            yield return "Gliadins-to-gluteins ratio";
            yield return "DM harvest index";
            yield return "N harvest index";
            yield return "rainfall + irrigation";
            yield return "Total available soil N";
            yield return "N leaching";
            yield return "Water drainage";
            yield return "N use efficiency";
            yield return "N utilisation efficiency";
            yield return "N upake efficiency";
            yield return "Water use efficiency";
            yield return "Water use efficiency";
            yield return "Water use efficiency";


        }

        public static IEnumerable<string> SummaryHeaders2()
        {
            yield return "leaf";
            yield return "m²/m²";
            yield return "m²/m²";
            yield return "kgDM/ha";
            yield return "kgDM/ha";
            yield return "kgDM/ha";
            yield return "kgN/ha";
            yield return "kgN/ha";
            yield return "kgN/ha";
            yield return "kgN/ha";
            yield return "mgDM/grain";
            yield return "mgN/grain";
            yield return "% of grain DM";
            yield return "grain/m²";
            yield return "% of total grain DM";
            yield return "mgN/grain";
            yield return "Amphiphils at maturity";
            yield return "mgN/grain";
            yield return "mgN/grain";
            yield return "% of total grain N";
            yield return "% of total grain N";
            yield return "dimensionless";
            yield return "dimensionless";
            yield return "dimensionless";
            yield return "mm";
            yield return "kgN/ha";
            yield return "kgN/ha";
            yield return "mm";
            yield return "kgDM/kgN";
            yield return "kgDM/kgN";
            yield return "kgN/kgN";
            yield return "kgDM/ha/mm";
        }

        public static IEnumerable<string> SummaryValues(Run run)
        {
            var anthesisUniverse = GetUniverse(run, GrowthStage.ZC_65_Anthesis);
            var maturityUniverse = GetUniverse(run, GrowthStage.ZC_92_Maturity);

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.LeafNumber, 2);
            else yield return "?";

            if (anthesisUniverse != null) yield return FormatNumber(anthesisUniverse.Crop_.LaminaeAI, 2);
            else yield return "?";

            if (anthesisUniverse != null) yield return FormatNumber(anthesisUniverse.Crop_.GAI, 2);
            else yield return "?";

            if (anthesisUniverse != null) yield return FormatNumber(anthesisUniverse.Crop_.OutputTotalDM, 0, 10);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.OutputTotalDM, 0, 10);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.GrainTotalDM, 0, 10);
            else yield return "?";

            if (anthesisUniverse != null) yield return FormatNumber(anthesisUniverse.Crop_.OutputTotalN, 0, 10);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.OutputTotalN, 0, 10);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.GrainTotalN, 0, 10);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.PostAnthesisNUptake, 0, 10);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.TotalDMperGrain, 2);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.TotalNperGrain, 2);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.ProteinConcentration, 2);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.GrainNumber, 0);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.Starch, 4);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.PercentStarch, 2);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.Namp, 2);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.Ngli, 2);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.Nglu, 2);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.PercentGli, 3);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.PercentGlu, 3);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.GliadinsToGluteins, 2);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.HarvestIndexDM, 2);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.HarvestIndexN, 2);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Soil_.IncomeWater, 1, Run.GwaterToMMwater);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber((maturityUniverse.Soil_.CalculateTotalN() + maturityUniverse.Soil_.SoilDeepLayer_.LostNitrogen + maturityUniverse.Crop_.CropTotalN), 0, 10);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Soil_.SoilDeepLayer_.LostNitrogen, 2, 10);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Soil_.SoilDeepLayer_.LostWater, 1, Run.GwaterToMMwater);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.NuseEfficiency, 2);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.Crop_.NutilisationEfficiency, 2);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.NuptakeEfficiency, 2);
            else yield return "?";

            if (maturityUniverse != null) yield return FormatNumber(maturityUniverse.WaterUseEfficiency, 2);
            else yield return "?";
        }

        private static string FormatNumber(double value, int nbDecimal)
        {
            return value.ToString(LineData.DoubleFormat[nbDecimal]);
        }

        private static string FormatNumber(double value, int nbDecimal, double scale)
        {
            return (value * scale).ToString(LineData.DoubleFormat[nbDecimal]);
        }

        #endregion

        #region sensitivity

        internal override PageData SensitivityRunHeader(string[] deltaHeader)
        {
            var page = new PageData();

            PrintVersionDate(page);
            PrintFiles(page);

            page.NewLine();

            PrintSensitivityRunHeader(page, deltaHeader);

            return page;
        }

        private static void PrintSensitivityRunHeader(PageData page, string[] deltaHeader)
        {
            page.NewLine()
                .AddNull(deltaHeader.Length)
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()

                .AddNull()

                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()

                .AddNull()

                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()

                .Add("Available water")
                .Add("Available nitrogen")
                .Add("Available water")
                .Add("Available nitrogen")

                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull();


            page.NewLine()
                .Add(deltaHeader)
                .Add(FileContainer.ManagementID)
                .Add(FileContainer.NonVarietyID)
                .Add(FileContainer.RunOptionID)
                .Add(FileContainer.SiteID)
                .Add(FileContainer.SoilID)
                .Add(FileContainer.VarietyID)

                .AddNull()

                .Add("Sowing year")
                .Add(Phase.growthStageAsString(GrowthStage.ZC_00_Sowing))
                .Add(Phase.growthStageAsString(GrowthStage.ZC_10_Emergence))
                .Add(Phase.growthStageAsString(GrowthStage.BeginningStemExtension))
                .Add(Phase.growthStageAsString(GrowthStage.ZC_65_Anthesis))
                .Add(Phase.growthStageAsString(GrowthStage.ZC_75_EndCellDivision))
                .Add(Phase.growthStageAsString(GrowthStage.ZC_91_EndGrainFilling))
                .Add(Phase.growthStageAsString(GrowthStage.ZC_92_Maturity))

                .AddNull()

                .Add("Final")
                .Add("Leaf area index")
                .Add("Green area index")
                .Add("Crop dry mass")
                .Add("Crop dry mass")
                .Add("Grain dry mass")
                .Add("Crop nitrogen")
                .Add("Crop nitrogen")
                .Add("Grain nitrogen")
                .Add("Post-anthesis crop")
                .Add("Single grain")
                .Add("Single grain")
                .Add("Grain protein")
                .Add("Grain")
                .Add("Grain starch")
                .Add("Albumins-globulins")
                .Add("Amphiphils")
                .Add("Gliadins")
                .Add("Glutenins")
                .Add("Gliadins")
                .Add("Gluteins")
                .Add("Gliadins to")
                .Add("Dry mass ")
                .Add("Nitrogen")
                .Add("Cumulative")
                .Add("Cumulative")
                .Add("Total available")
                .Add("Soil nitrogen")
                .Add("Soil water")
                .Add("Cumulative N")
                .Add("Cumulative")
                .Add("in soil profil")
                .Add("in soil profil")
                .Add("in root zone")
                .Add("in root zone")
                .Add("Nitrogen use")
                .Add("Nitrogen utilisation")
                .Add("Nitrogen upake")
                .Add("Water use");

            page.NewLine()
                .AddNull(deltaHeader.Length)
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()

                .Add("leaf number")
                .Add("at anthesis")
                .Add("at anthesis")
                .Add("at anthesis")
                .Add("at maturity")
                .Add("at maturity")
                .Add("at anthesis")
                .Add("at maturity")
                .Add("at maturity")
                .Add("nitrogen uptake")
                .Add("dry mass")
                .Add("nitrogen")
                .Add("concentration")
                .Add("number")
                .Add("concentration")
                .Add("per grain")
                .Add("per grain")
                .Add("per grain")
                .Add("per grain")
                .Add("concentration")
                .Add("concentration")
                .Add("gluteins ratio")
                .Add("harvest index")
                .Add("harvest index")
                .Add("rain + irrigation")
                .Add("ET")
                .Add("soil nitrogen")
                .Add("leaching")
                .Add("drainage")
                .Add("mineralisation")
                .Add("denitrification")
                .Add("at maturity")
                .Add("at maturity")
                .Add("at maturity")
                .Add("at maturity")
                .Add("efficiency")
                .Add("efficiency")
                .Add("efficiency")
                .Add("efficiency");

            page.NewLine()
                .AddNull(deltaHeader.Length)
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()

                .Add("yyyy")
                .Add("DOY")
                .Add("DOY")
                .Add("DOY")
                .Add("DOY")
                .Add("DOY")
                .Add("DOY")
                .Add("DOY")

                .AddNull()

                .Add("leaf/mainstem")
                .Add("m²/m²")
                .Add("m²/m²")
                .Add("kgDM/ha")
                .Add("kgDM/ha")
                .Add("kgDM/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("mgDM/grain")
                .Add("mgN/grain")
                .Add("% grain dry mass")
                .Add("grain/m²")
                .Add("% grain dry mass")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("% grain nitrogen")
                .Add("% grain nitrogen")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("mm")
                .Add("mm")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("mm")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("mm")
                .Add("kgN/ha")
                .Add("mm")
                .Add("kgN/ha")
                .Add("kgDM/kgN")
                .Add("kgDM/kgN")
                .Add("kgN/kgN")
                .Add("kgDM/ha/mm)");
        }

        ///<Behnam>
        internal override LineData SensitivityRunLine(Run runOld, string[] deltaHeader, double[] deltaValue)
        ///</Behnam>
        {
            var line = new LineData();

            for (var m = 0; m < deltaValue.Length; ++m) deltaValue[m] = Math.Round(deltaValue[m], 4);
            line.Add(deltaValue)

                .Add(runOld.ManagementDef != null ? runOld.ManagementDef.Name : "?")
                .Add(runOld.ParameterDef != null ? runOld.ParameterDef.Name : "?")
                .Add(runOld.RunOptionDef != null ? runOld.RunOptionDef.Name : "?")
                .Add(runOld.SiteDef != null ? runOld.SiteDef.Name : "?")
                .Add(runOld.SoilDef != null ? runOld.SoilDef.Name : "?")
                .Add(runOld.VarietyDef != null ? runOld.VarietyDef.Name : "?")
                .AddNull()
                ///<Behnam>
                .Add(runOld.ManagementDef != null ? runOld.ManagementDef.FinalSowingDate.Year.ToString() : "?");
            ///</Behnam>

            PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_00_Sowing);
            PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_10_Emergence);
            PrintMultiRunSummaryDate(line, runOld, GrowthStage.BeginningStemExtension);
            PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_65_Anthesis);
            PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_75_EndCellDivision);
            PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_91_EndGrainFilling);
            PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_92_Maturity);

            line.AddNull();

            PrintMultiRunSummaryCrop(line, runOld);

            return line;
        }

        #endregion

    }
}