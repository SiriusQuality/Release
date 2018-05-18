
///<Behnam>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Globalization;
using SiriusModel.InOut.OutputWriter;
using SiriusModel.Model;
using SiriusModel.Model.Phenology;
using SiriusModel.Model.CropModel;
using SiriusModel.Model.Observation;
using SiriusModel.Model.SoilModel;
using SiriusModel.InOut;
using SiriusModel.InOut.Base;
using SiriusQualityPhenology;

namespace SiriusModel.InOut
{
    public class OutputFileExtractorCus : OutputFileExtractor
    {
        #region normal

        public static bool summaryHeading;

        internal override PageData NormalRun(Run runOld)
        {
            var page = new PageData();

            if (runOld.RunOptionDef.VersionDateItems) PrintVersionDate(page);
            else page.NewLine().Add("");

            if (runOld.RunOptionDef.FilesItems) PrintFiles(page);
            else
            {
                page.NewLine().Add("");
                page.NewLine().Add("");
                page.NewLine().Add("");
                page.NewLine().Add("");

            }

            if (runOld.RunOptionDef.DateItems) PrintNormalRunSummaryDate(page, runOld);
            if (runOld.RunOptionDef.CropItems) PrintNormalRunSummaryCrop(page, runOld);

            page.SetMinWidth(4);
            PrintItems(page, runOld);
            FillGaps(page, runOld);

            if (page.Count>0) page.NewLine();
            var headerHeight = page.Count;

            PrintDailyCrop(page, runOld);
            page.NewLine();
            return page;           
        }

        #region ITEMS

        public static void PrintNormalRunSummaryDate(PageData page, Run runOld)
        {
            if (runOld.RunOptionDef.VersionDateItems | runOld.RunOptionDef.FilesItems) page.NewLine();
            page.NewLine().Add("Date").Add("DOY").Add("Growth stage");
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_00_Sowing);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_10_Emergence);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.EndVernalisation);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_30_PseudoStemErection);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.FloralInitiation); /// Behnam (2016.02.12): Pierre wanted to change have FloralInitiation instead of ZC_39
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.Heading); 
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.BeginningStemExtension);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_39_FlagLeafLiguleJustVisible);
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

                // if (lastUniverse.Crop_.calendar[moment].HasValue && moment != GrowthStage.ZC_00_Sowing &&
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
                // modifications stop here
            }
            else page.NewLine().AddDateDOY(null).Add(Phase.growthStageAsString(moment));
        }

        private void FillGaps(PageData page, Run runOld)
        {
            var Rows1 = 0;
            if (runOld.RunOptionDef.ManagementItems) Rows1 += 3;
            if (runOld.RunOptionDef.ParameterItems) Rows1 += 3;
            if (runOld.RunOptionDef.RunOptionItems) Rows1 += 3;
            if (runOld.RunOptionDef.SiteItems) Rows1 += 3;
            if (runOld.RunOptionDef.SoilItems) Rows1 += 3;
            if (runOld.RunOptionDef.VarietyItems) Rows1 += 3;
            if (Rows1 > 0) Rows1 += 1;

            var Rows2 = page.Count();
            if (Rows1 - Rows2 - 1 > 0) page.NewLine(Rows1 - Rows2 - 1);
        }

        public static void PrintNormalRunSummaryCrop(PageData page, Run runOld)
        {
            var anthesisUniverse = GetUniverse(runOld, GrowthStage.ZC_65_Anthesis);
            var maturityUniverse = GetUniverse(runOld, GrowthStage.ZC_92_Maturity);

            // if (runOld.RunOptionDef.VersionDateItems || runOld.RunOptionDef.FilesItems ||
                // runOld.RunOptionDef.DateItems) page.NewLine();

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

            var tilleringProfileLength = anthesisUniverse.Crop_.tilleringProfile.Count;
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
            //Debug
            var Rows = 3;
            //var Rows = 1;
            if (runOld.RunOptionDef.ManagementItems) 
            {
                PrintManagementItem(page, runOld.ManagementDef, Rows); Rows += 3; 
            }
            if (runOld.RunOptionDef.ParameterItems)
            {
                PrintParameterItem(page, runOld.ParameterDef, Rows); Rows += 3;
            }
            if (runOld.RunOptionDef.RunOptionItems)
            {
                PrintRunOptionItem(page, runOld.RunOptionDef, Rows); Rows += 3;
            }
            if (runOld.RunOptionDef.SiteItems)
            {
                PrintSiteItem(page, runOld.SiteDef, Rows); Rows += 3;
            }
            if (runOld.RunOptionDef.SoilItems)
            {
                PrintSoilItem(page, runOld.SoilDef, Rows); Rows += 3;
            }
            if (runOld.RunOptionDef.VarietyItems) PrintVarietyItem(page, runOld.VarietyDef, Rows);
        }

        public static void PrintManagementItem(PageData page, ManagementItem item, int Rows)
        {
            var nbDate = (item != null) ? item.DateApplications.Count : 0;
            var nbGs = (item != null) ? item.GrowthStageApplications.Count : 0;

            ///<Behnam (2015.09.23)>
            ///<Comment>Adding outputs related to sowing date estimation procedures</Comment>
            ///

            
            page[Rows]
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
                page[Rows]
                    .Add("Date")
                    .Add((item.IsTotalNitrogen) ? "N fertilisation (%)" : "N fertilisation (gN/ha)")
                    .Add("Irrigation (mm)");
            }
            for (var i = 0; i < nbGs; ++i)
            {
                page[Rows]
                    .Add("Growth stage")
                    .Add((item.IsTotalNitrogen) ? "N fertilisation (%)" : "N fertilisation (gN/ha)")
                    .Add("Irrigation (mm)");
            }
            if (item != null)
            {
                page[Rows + 1]
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
                page[Rows + 1]
                    .Add("?");
            }
            for (var i = 0; i < nbDate; ++i)
            {
                page[Rows + 1]
                    .Add(item.DateApplications[i].Date.ToString("u").Split()[0])
                    .Add(item.DateApplications[i].Nitrogen / ((item.IsTotalNitrogen) ? 10 : 1), 2, 10)
                    .Add(item.DateApplications[i].WaterMM);
            }
            for (var i = 0; i < nbGs; ++i)
            {
                page[Rows + 1]
                    .Add(Phase.growthStageAsString(item.GrowthStageApplications[i].GrowthStage))
                    .Add(item.GrowthStageApplications[i].Nitrogen / ((item.IsTotalNitrogen) ? 10 : 1), 2, 10)
                    .Add(item.GrowthStageApplications[i].WaterMM);
            }
        }

        public static void PrintParameterItem(PageData page, CropParameterItem item, int Rows)
        {
            page[Rows].Add(FileContainer.NonVarietyID);

            if (item != null)
            {
                page[Rows + 1].Add(item.Name);

                // Write parameters
                foreach (KeyValuePair<string, double> pair in item.ParamValue)
                {
                    page[Rows].Add(pair.Key);
                    page[Rows + 1].Add(pair.Value);
                }
            }
            else
                page[Rows + 1].Add("?");
        }

        public static void PrintRunOptionItem(PageData page, RunOptionItem item, int Rows)
        {
            page[Rows]
                .Add(FileContainer.RunOptionID)
                .Add("Use observed grain number")
                .Add("Use air Temperature")
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
                page[Rows + 1]
                    ///<Behnam>
                   .Add(item.Name)
                   .Add(item.UseObservedGrainNumber)
                   .Add(item.UseAirTemperatureForSenescence)
                   .Add(item.IsCutOnGrainFillNotUse)
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
                page[Rows + 1].Add("?");
            }
        }

        public static void PrintSiteItem(PageData page, SiteItem item, int Rows)
        {
            var nbWF = (item != null) ? item.WeatherFiles.Count : 0;

            page[Rows]
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
                page[Rows]
                    .Add("Weather file");
            }
            if (item != null)
            {
                page[Rows + 1]
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
                page[Rows + 1]
                    .Add("?");
            }
            for (var i = 0; i < nbWF; ++i)
            {
                page[Rows + 1]
                    .Add(item.WeatherFiles[i].File);
            }
        }

        public static void PrintSoilItem(PageData page, SoilItem item, int Rows)
        {
            var nbSl = (item != null) ? item.Layers.Count : 0;

            page[Rows]
                .Add(FileContainer.SoilID)
                .Add("N Mineralisation rate constant (1/day)")
                .Add("Organic N (gN/m²)")
                .Add("Denitrification  rate constant (1/day)")
                .Add("Percolation coefficient (dimensionless)")
                .Add("Minimum inorganic N (kgN/ha)")
                .Add("Total available water content (mm)");

            for (var i = 0; i < nbSl; ++i)
            {
                page[Rows]
                    .Add("Depth (m)")
                    .Add("Saturation (%)")
                    .Add("Field capacity (%)")
                    .Add("Permenent wilting point (%)");
            }

            if (item != null)
            {
                page[Rows + 1]
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
                page[Rows + 1]
                       .Add("?");
            }
            for (var i = 0; i < nbSl; ++i)
            {
                page[Rows + 1]
                    .Add(item.Layers[i].Depth)
                    .Add(item.Layers[i].SSAT)
                    .Add(item.Layers[i].SDUL)
                    .Add(item.Layers[i].SLL);
            }
        }

        public static void PrintVarietyItem(PageData page, CropParameterItem item, int Rows)
        {
            page[Rows].Add(FileContainer.VarietyID);

            if (item != null)
            {
                page[Rows + 1].Add(item.Name);

                foreach (KeyValuePair<string, double> pair in item.ParamValue)
                {
                    page[Rows].Add(pair.Key);
                    page[Rows + 1].Add(pair.Value);
                }
            }
            else
                page[Rows + 1].Add("?");
        }
        #endregion ITEMS

        public static void PrintDailyCrop(PageData page, Run runOld)
        {

            ///<Behnam (2016.01.12)>
            ///<Comment>Now, it is possible to define the outputs in RunOptionItem class as listDaily and listSummary
            ///and use them as sorted lists to create check boxes in the Run Option form and also to rearrange the 
            ///daily and summary outputs. To rearrange, we only need to change listDaily and listSummary.
            ///The Namr referes to the name of the properties of RunOptionItem class.<Comment> 

            var line = new LineData();
            var x = runOld.RunOptionDef.OutputPattern;
            var finalLeafNum = GetFinalLeafNumber(runOld);
            var List = RunOptionItem.ListDaily;
            var anthesisUniverse = GetUniverse(runOld, GrowthStage.ZC_65_Anthesis);

            for (var i = 0; i < List.Count; ++i)
            {
                var Name = List.Keys.ElementAt(i);
                var Def = List[Name];
                var Flag = typeof(RunOptionItem).GetProperty(Name).GetValue(runOld.RunOptionDef);

                if ((bool)Flag)
                {
                    if (Name == "VersionDateItems" || Name == "FilesItems" || Name == "DateItems" ||
                        Name == "CropItems" || Name == "ManagementItems" || Name == "ParameterItems" ||
                        Name == "RunOptionItems" || Name == "SiteItems" || Name == "SoilItems" || Name == "VarietyItems") { }

                    else if (Name == "DailyCropdrymatter" || Name == "DailyCropdeltadrymatter")
                    {
                        line.Add(Def);
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsW)
                            line.Add(Def + " under unlimited W");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsN)
                            line.Add(Def + " under unlimited N");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsT)
                            line.Add(Def + " under unlimited T");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsWN)
                            line.Add(Def + " under unlimited W and N");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsWT)
                            line.Add(Def + " under unlimited W and T");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsNT)
                            line.Add(Def + " under unlimited N and T");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsWNT)
                            line.Add(Def + " under unlimited W, N and T");
                    }

                    else if (Name == "DailyCropdrymatterSF")
                    {
                        /* if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsW)
                            line.Add("Dry matter SF (W) (dimensionless)");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsN)
                            line.Add("Dry matter SF (N) (dimensionless)");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsT)
                            line.Add("Dry matter SF (T) (dimensionless)");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsWN)
                            line.Add("Dry matter SF (WN) (dimensionless)");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsWT)
                            line.Add("Dry matter SF (WT) (dimensionless)");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsNT)
                            line.Add("Dry matter SF (NT) (dimensionless)");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsWNT)
                            line.Add("Dry matter SF (WNT) (dimensionless)"); */

                        /// Behnam (2016.06.29): Replaced old formula which had potential conditions as the denominator.
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsW)
                            line.Add("Dry matter SF (W) (dimensionless)");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsN)
                            line.Add("Dry matter SF (N) (dimensionless)");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsT)
                            line.Add("Dry matter SF (T) (dimensionless)");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsWN)
                            line.Add("Dry matter SF (WN) (dimensionless)");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsWT)
                            line.Add("Dry matter SF (WT) (dimensionless)");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsNT)
                            line.Add("Dry matter SF (NT) (dimensionless)");
                        if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsWNT)
                            line.Add("Dry matter SF (WNT) (dimensionless)");
                    }

                    else if (Name == "DailyLaminaesurfacearea")
                    {
                        for (var j = 0; j < finalLeafNum; ++j)
                        {
                            line.Add("Laminae surface layer " + (j+1).ToString() + " (m²/m²)");
                        }
                    }
                    else if (Name == "DailyLaminaeTotalN")
                    {
                        for (var j = 0; j < finalLeafNum; ++j)
                        {
                            line.Add("Laminae Total N " + (j + 1).ToString() + " (kgN/ha)");
                        }
                    }
                    else
                    {
                        line.Add(Def);
                    }
                }
            }
            page.Add(line);

            var nbDay = runOld.SavedUniverses.Count;
            var sowDate = runOld.SavedUniverses[nbDay - 1].Crop_.getDateOfStage(GrowthStage.ZC_00_Sowing);
            var sowDay = (int)(sowDate.Value - runOld.SavedUniverses[0].CurrentDate).TotalDays;

            for (var day = 1; day < nbDay; ++day)
            {
                var universe = runOld.SavedUniverses[day];

                if (day >= sowDay)
                {
                    var line2 = new LineData();

                    for (var i = 0; i < List.Count; ++i)
                    {
                        var Name = List.Keys.ElementAt(i);
                        var Def = List[Name];
                        var Flag = typeof(RunOptionItem).GetProperty(Name).GetValue(runOld.RunOptionDef);

                        if ((bool)Flag)
                        {
                            if (Name == "VersionDateItems" || Name == "FilesItems" || Name == "DateItems" ||
                                Name == "CropItems" || Name == "ManagementItems" || Name == "ParameterItems" ||
                                Name == "RunOptionItems" || Name == "SiteItems" || Name == "SoilItems" || Name == "VarietyItems") { }

                            else if (Name == "DailyGrowthDay") line2.Add(universe.GrowthDay);
                            else if (Name == "DailyDate") line2.AddDate(universe.CurrentDate);
                            else if (Name == "DailyDOY") line2.Add(universe.CurrentDate.DayOfYear);
                            else if (Name == "DailyDayLength") line2.Add(universe.meteorologyWrapper_.DayLength, 2);
                            else if (Name == "DailyCO2Conc") line2.Add(universe.Crop_.DailyCO2, 2);
                            else if (Name == "DailyThermaltimeaftersowingAir") line2.Add(universe.thermalTimeWrapper_.getCumulTT(Delta.Air), 2);
                            else if (Name == "DailyThermaltimeaftersowingShoot") line2.Add(universe.thermalTimeWrapper_.getCumulTT(Delta.Shoot), 2);
                            else if (Name == "DailyPhysThermaltimeaftersowing") line2.Add(universe.thermalTimeWrapper_.getCumulTT(Delta.Physiology), 2);
                            else if (Name == "DailyAirtemperature") line2.Add(universe.Soil_.MeanAirTemperature, 2);
                            else if (Name == "DailySoiltemperature") line2.Add(universe.Soil_.SoilMeanTemperature, 2);
                            else if (Name == "DailyCanopytemperature") line2.Add(universe.Soil_.MeanCanopyTemperature, 2);
                            else if (Name == "DailyRootingdepth") line2.Add(universe.Crop_.RootLength, 2);
                            else if (Name == "DailyCumulativerainirrigation") line2.Add(universe.Soil_.IncomeWater, 2, Run.GwaterToMMwater);
                            else if (Name == "DailyVPDAirOut") line2.Add(universe.meteorologyWrapper_.VPDair, 2);
                            else if (Name == "DailyVPDAirCanopyOut") line2.Add(universe.meteorologyWrapper_.VPDairCanopy, 2);
                            else if (Name == "DailyCumulativePET") line2.Add(universe.Soil_.AccumulatedPotEvapoTranspiration, 2, Run.GwaterToMMwater);
                            else if (Name == "DailyCumulativeET") line2.Add(universe.Soil_.AccumulatedActEvapoTranspiration, 2, Run.GwaterToMMwater);
                            else if (Name == "DailyCumulativePT") line2.Add(universe.Soil_.AccumulatedPotTranspiration, 2, Run.GwaterToMMwater);
                            else if (Name == "DailyCumulativeT") line2.Add(universe.Soil_.AccumulatedActTranspiration, 2, Run.GwaterToMMwater);
                            else if (Name == "DailyCumulativewaterdrainage") line2.Add(universe.Soil_.SoilDeepLayer_.LostWater, 2, Run.GwaterToMMwater);
                            else if (Name == "DailyAvailablewaterinsoilprofil") line2.Add(universe.Soil_.CalculateAvailableExcessW(), 2, Run.GwaterToMMwater);
                            else if (Name == "DailyWaterdeficitinsoilprofil") line2.Add(universe.Soil_.CalculateWaterDeficit(), 2, Run.GwaterToMMwater);
                            else if (Name == "DailyAvailablewaterinrootzone") line2.Add(universe.Soil_.CalculateRootZoneAvailableWater(), 2, Run.GwaterToMMwater);
                            else if (Name == "DailyWaterdeficitinrootzone") line2.Add(universe.Soil_.CalculateRootZoneWaterDeficit(), 2, Run.GwaterToMMwater);
                            else if (Name == "DailyVirtualWReq") line2.Add(universe.Soil_.VirtualWReq, 2, Run.GwaterToMMwater);
                            else if (Name == "DailyTRSF") line2.Add(universe.Soil_.TRSF, 2);
                            else if (Name == "DailyETSF") line2.Add(universe.Soil_.ETSF, 2);
                            else if (Name == "DailySMSF") line2.Add(universe.Soil_.SMSF, 2);
                            else if (Name == "DailyFPAW") line2.Add(universe.Soil_.FPAW, 2);
                            else if (Name == "DailyDrymassdroughtfactor") line2.Add(universe.Soil_.DBF, 2);
                            else if (Name == "DailyEargrowthdroughtfactor") line2.Add(universe.Soil_.DEBF, 2);
                            else if (Name == "DailyLeafexpansiondroughtfactor") line2.Add(universe.Crop_.DEF, 2);
                            else if (Name == "DailySenescencedroughtfactor") line2.Add(universe.Crop_.DSF, 2);
                            else if (Name == "DailyTranspirationdroughtfactor") line2.Add(universe.Soil_.DTF, 2);
                            else if (Name == "DailyCumulativeNfertilisation") line2.Add(universe.Soil_.CumulativeNfertilisation, 2, 10);
                            else if (Name == "DailyCumulativeNleaching") line2.Add(universe.Soil_.SoilDeepLayer_.LostNitrogen, 3, 10);
                            else if (Name == "DailyCumulativeNmineralisation") line2.Add(universe.Soil_.AccumulatedNitrogenMineralisation, 3, 10);
                            else if (Name == "DailyCumulativedenitrification") line2.Add(universe.Soil_.AccumulatedNitrogenDenitrification, 3, 10);
                            else if (Name == "DailyAvailablemineralNinsoilprofil") line2.Add(universe.Soil_.CalculateAvailableExcessN(), 2, 10);
                            else if (Name == "DailyTotalmineralNinsoilprofil") line2.Add(universe.Soil_.CalculateTotalN(), 2, 10);
                            else if (Name == "DailyAvailablemineralNinrootzone") line2.Add(universe.Soil_.DailyAvNforRoots, 2, 10);
                            else if (Name == "DailyTotalmineralNinrootzone") line2.Add((universe.Soil_.DailyAvNforRoots + universe.Soil_.DailyUnavNforRoots), 2, 10);
                            else if (Name == "DailyNitrogennutritionindex") line2.Add(universe.Crop_.NNI, 3);
                            else if (Name == "DailyVernaProgress") line2.Add(universe.Crop_.VernaProgress, 3);
                            // else if (Name == "DailyGrowthStage") line2.Add(universe.Crop_.Phase.Phase1);
                            else if (Name == "DailyGrowthStage") line2.Add(universe.Crop_.getLastGrowthStageSet());
                            else if (Name == "DailyEmergedleafnumber") line2.Add(universe.Crop_.LeafNumber, 2);
                            else if (Name == "DailyShootnumber") line2.Add(universe.Crop_.CanopyShootNumber, 0);
                            else if (Name == "DailyGreenareaindex") line2.Add(universe.Crop_.GAI, 2);
                            else if (Name == "DailyLeafareaindex") line2.Add(universe.Crop_.LaminaeAI, 2);
                            else if (Name == "DailyStemlength") line2.Add(universe.Crop_.SumInternodesLength, 4, Run.CMToM);
                            else if (Name == "DailyVirtualNReq") line2.Add(universe.Crop_.VirtualNReq, 3, 10);
                            else if (Name == "DailyCropnitrogen") line2.Add(universe.Crop_.OutputTotalN, 3, 10);
                            else if (Name == "DailyGraindrymatter") line2.Add(universe.Crop_.GrainTotalDM, 2, 10);
                            else if (Name == "DailyGrainnitrogen") line2.Add(universe.Crop_.GrainTotalN, 3, 10);
                            else if (Name == "DailyLeafdrymatter") line2.Add((universe.Crop_.OutputLaminaeDM + universe.Crop_.OutputSheathDM), 2, 10);
                            else if (Name == "DailyLeafnitrogen") line2.Add((universe.Crop_.OutputLaminaeN + universe.Crop_.OutputSheathN), 3, 10);
                            else if (Name == "DailyLaminaedrymatter") line2.Add(universe.Crop_.OutputLaminaeDM, 2, 10);
                            else if (Name == "DailyLaminaenitrogen") line2.Add(universe.Crop_.OutputLaminaeN, 3, 10);
                            else if (Name == "DailyStemdrymatter") line2.Add((universe.Crop_.OutputSheathDM + universe.Crop_.StemTotalDM), 2, 10);
                            else if (Name == "DailyStemnitrogen") line2.Add((universe.Crop_.OutputSheathN + universe.Crop_.StemTotalN), 3, 10);
                            else if (Name == "DailyExposedsheathdrymatter") line2.Add(universe.Crop_.OutputSheathDM, 2, 10);
                            else if (Name == "DailyExposedsheathnitrogen") line2.Add(universe.Crop_.OutputSheathN, 3, 10);
                            else if (Name == "DailySpecificleafnitrogen") line2.Add((universe.Crop_.LaminaeSLN), 4);
                            else if (Name == "DailySpecificleafdrymass") line2.Add((universe.Crop_.LaminaeSLW), 2);
                            else if (Name == "DailySinglegraindrymass") line2.Add(universe.Crop_.TotalDMperGrain, 4);
                            else if (Name == "DailyStarchpergrain") line2.Add(universe.Crop_.Starch, 4);
                            else if (Name == "DailySinglegrainnitrogen") line2.Add(universe.Crop_.TotalNperGrain, 4);
                            else if (Name == "DailyAlbuminsGlobulinspergrain") line2.Add(universe.Crop_.NalbGlo, 4);
                            else if (Name == "DailyAmphiphilspergrain") line2.Add(universe.Crop_.Namp, 4);
                            else if (Name == "DailyGliadinspergrain") line2.Add(universe.Crop_.Ngli, 4);
                            else if (Name == "DailyGluteninspergrain") line2.Add(universe.Crop_.Nglu, 4);
                            else if (Name == "DailyPostanthesisnitrogenuptake") line2.Add(universe.Crop_.PostAnthesisNUptake, 1, 10);
                            else if (Name == "DailyMinimumShootTemperature") line2.Add(universe.ShootTemperature_.MinShootTemperature, 2);
                            else if (Name == "DailyMeanShootTemperature") line2.Add(universe.ShootTemperature_.MeanShootTemperature, 2);
                            else if (Name == "DailyMaximumShootTemperature") line2.Add(universe.ShootTemperature_.MaxShootTemperature, 2);
                            else if (Name == "DailyTranspirationT") line2.Add(universe.Soil_.ActTransp, 2, Run.GwaterToMMwater);
                            else if (Name == "DailyEvaporationE") line2.Add(universe.Soil_.DailyEvaporation, 2, Run.GwaterToMMwater);
                            else if (Name == "DailyCropdrymatter")
                            {
                                line2.Add(universe.Crop_.OutputTotalDM, 2, 10);
                                if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsW)
                                    line2.Add(universe.Crop_.OutputTotalDM_W, 2, 10);
                                if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsN)
                                    line2.Add(universe.Crop_.OutputTotalDM_N, 2, 10);
                                if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsT)
                                    line2.Add(universe.Crop_.OutputTotalDM_T, 2, 10);
                                if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsWN)
                                    line2.Add(universe.Crop_.OutputTotalDM_WN, 2, 10);
                                if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsWT)
                                    line2.Add(universe.Crop_.OutputTotalDM_WT, 2, 10);
                                if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsNT)
                                    line2.Add(universe.Crop_.OutputTotalDM_NT, 2, 10);
                                if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsWNT)
                                    line2.Add(universe.Crop_.OutputTotalDM_WNT, 2, 10);
                            }
                            else if (Name == "DailyCropdeltadrymatter")
                            {
                                line2.Add(universe.Crop_.OutputTotalDM - universe.Crop_.OutputTotalDMOld, 2, 10);
                                if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsW)
                                    line2.Add(universe.Crop_.OutputTotalDM_W - universe.Crop_.OutputTotalDMOld, 2, 10);
                                if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsN)
                                    line2.Add(universe.Crop_.OutputTotalDM_N - universe.Crop_.OutputTotalDMOld, 2, 10);
                                if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsT)
                                    line2.Add(universe.Crop_.OutputTotalDM_T - universe.Crop_.OutputTotalDMOld, 2, 10);
                                if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsWN)
                                    line2.Add(universe.Crop_.OutputTotalDM_WN - universe.Crop_.OutputTotalDMOld, 2, 10);
                                if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsWT)
                                    line2.Add(universe.Crop_.OutputTotalDM_WT - universe.Crop_.OutputTotalDMOld, 2, 10);
                                if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsNT)
                                    line2.Add(universe.Crop_.OutputTotalDM_NT - universe.Crop_.OutputTotalDMOld, 2, 10);
                                if (runOld.RunOptionDef.DoInteractions && runOld.RunOptionDef.InteractionsWNT)
                                    line2.Add(universe.Crop_.OutputTotalDM_WNT - universe.Crop_.OutputTotalDMOld, 2, 10);
                            }
                            else if (Name == "DailyCropdrymatterSF")
                            {
                                if (runOld.RunOptionDef.DoInteractions)
                                {
                                    var DeltaDMa = universe.Crop_.OutputTotalDM - universe.Crop_.OutputTotalDMOld;
                                    var DeltaDMw = universe.Crop_.OutputTotalDM_W - universe.Crop_.OutputTotalDMOld;
                                    var DeltaDMn = universe.Crop_.OutputTotalDM_N - universe.Crop_.OutputTotalDMOld;
                                    var DeltaDMt = universe.Crop_.OutputTotalDM_T - universe.Crop_.OutputTotalDMOld;
                                    var DeltaDMwn = universe.Crop_.OutputTotalDM_WN - universe.Crop_.OutputTotalDMOld;
                                    var DeltaDMwt = universe.Crop_.OutputTotalDM_WT - universe.Crop_.OutputTotalDMOld;
                                    var DeltaDMnt = universe.Crop_.OutputTotalDM_NT - universe.Crop_.OutputTotalDMOld;
                                    var DeltaDMwnt = universe.Crop_.OutputTotalDM_WNT - universe.Crop_.OutputTotalDMOld;

                                    /// Behnam (2016.07.13): The runOld.RunOptionDef.UseActualBase was previously used. Pierre wanted it to be deleted.
                                    /// At one stage, we decided to use Potential conditions as the base run. Now it is not working anymore.
                                    
                                    if (true) 
                                    {
                                        if (universe.Crop_.switchAnthesis || (!universe.Crop_.switchAnthesis && universe.Crop_.LaminaeAI > 0.1 * anthesisUniverse.Crop_.LaminaeAI))
                                        {
                                            //if (runOld.RunOptionDef.InteractionsW) line2.Add(DeltaDMwnt <= 0 || DeltaDMwnt <= DeltaDMnt ? 1 : Math.Min(1, Math.Max(0, DeltaDMnt) / Math.Max(0, DeltaDMwnt)), 3);
                                            //if (runOld.RunOptionDef.InteractionsN) line2.Add(DeltaDMwnt <= 0 || DeltaDMwnt <= DeltaDMwt ? 1 : Math.Min(1, Math.Max(0, DeltaDMwt) / Math.Max(0, DeltaDMwnt)), 3);
                                            //if (runOld.RunOptionDef.InteractionsT) line2.Add(DeltaDMwnt <= 0 || DeltaDMwnt <= DeltaDMwn ? 1 : Math.Min(1, Math.Max(0, DeltaDMwn) / Math.Max(0, DeltaDMwnt)), 3);
                                            //if (runOld.RunOptionDef.InteractionsWN) line2.Add(DeltaDMwnt <= 0 || DeltaDMwnt <= DeltaDMt ? 1 : Math.Min(1, Math.Max(0, DeltaDMt) / Math.Max(0, DeltaDMwnt)), 3);
                                            //if (runOld.RunOptionDef.InteractionsWT) line2.Add(DeltaDMwnt <= 0 || DeltaDMwnt <= DeltaDMn ? 1 : Math.Min(1, Math.Max(0, DeltaDMn) / Math.Max(0, DeltaDMwnt)), 3);
                                            //if (runOld.RunOptionDef.InteractionsNT) line2.Add(DeltaDMwnt <= 0 || DeltaDMwnt <= DeltaDMw ? 1 : Math.Min(1, Math.Max(0, DeltaDMw) / Math.Max(0, DeltaDMwnt)), 3);
                                            //if (runOld.RunOptionDef.InteractionsWNT) line2.Add(DeltaDMwnt <= 0 || DeltaDMwnt <= DeltaDMa ? 1 : Math.Min(1, Math.Max(0, DeltaDMa) / Math.Max(0, DeltaDMwnt)), 3);

                                            /// Behnam (2016.06.29): Replaced old formula which had potential conditions as the denominator.
                                            if (runOld.RunOptionDef.InteractionsW) line2.Add(DeltaDMw <= 0 || DeltaDMw <= DeltaDMa ? 1 : Math.Min(1, Math.Max(0, DeltaDMa) / Math.Max(0, DeltaDMw)), 3);
                                            if (runOld.RunOptionDef.InteractionsN) line2.Add(DeltaDMn <= 0 || DeltaDMn <= DeltaDMa ? 1 : Math.Min(1, Math.Max(0, DeltaDMa) / Math.Max(0, DeltaDMn)), 3);
                                            if (runOld.RunOptionDef.InteractionsT) line2.Add(DeltaDMt <= 0 || DeltaDMt <= DeltaDMa ? 1 : Math.Min(1, Math.Max(0, DeltaDMa) / Math.Max(0, DeltaDMt)), 3);
                                            if (runOld.RunOptionDef.InteractionsWN) line2.Add(DeltaDMwn <= 0 || DeltaDMwn <= DeltaDMa ? 1 : Math.Min(1, Math.Max(0, DeltaDMa) / Math.Max(0, DeltaDMwn)), 3);
                                            if (runOld.RunOptionDef.InteractionsWT) line2.Add(DeltaDMwt <= 0 || DeltaDMwt <= DeltaDMa ? 1 : Math.Min(1, Math.Max(0, DeltaDMa) / Math.Max(0, DeltaDMwt)), 3);
                                            if (runOld.RunOptionDef.InteractionsNT) line2.Add(DeltaDMnt <= 0 || DeltaDMnt <= DeltaDMa ? 1 : Math.Min(1, Math.Max(0, DeltaDMa) / Math.Max(0, DeltaDMnt)), 3);
                                            if (runOld.RunOptionDef.InteractionsWNT) line2.Add(DeltaDMwnt <= 0 || DeltaDMwnt <= DeltaDMa ? 1 : Math.Min(1, Math.Max(0, DeltaDMa) / Math.Max(0, DeltaDMwnt)), 3);
                                        }
                                        else
                                        {
                                            //if (runOld.RunOptionDef.InteractionsW) line2.Add("");
                                            //if (runOld.RunOptionDef.InteractionsN) line2.Add("");
                                            //if (runOld.RunOptionDef.InteractionsT) line2.Add("");
                                            //if (runOld.RunOptionDef.InteractionsWN) line2.Add("");
                                            //if (runOld.RunOptionDef.InteractionsWT) line2.Add("");
                                            //if (runOld.RunOptionDef.InteractionsNT) line2.Add("");
                                            //if (runOld.RunOptionDef.InteractionsWNT) line2.Add("");

                                            /// Behnam (2016.06.29): Replaced old formula which had potential conditions as the denominator.
                                            if (runOld.RunOptionDef.InteractionsW) line2.Add("");
                                            if (runOld.RunOptionDef.InteractionsN) line2.Add("");
                                            if (runOld.RunOptionDef.InteractionsT) line2.Add("");
                                            if (runOld.RunOptionDef.InteractionsWN) line2.Add("");
                                            if (runOld.RunOptionDef.InteractionsWT) line2.Add("");
                                            if (runOld.RunOptionDef.InteractionsNT) line2.Add("");
                                            if (runOld.RunOptionDef.InteractionsWNT) line2.Add("");
                                        }
                                    }
                                    // At one stage, we decided to use Potential conditions as the base run. Now it is not working anymore.
                                    else  
                                    {
                                        if (runOld.RunOptionDef.InteractionsW) line2.Add(DeltaDMwnt == 0 ? 1 : (DeltaDMa - DeltaDMw) / DeltaDMa, 3);
                                        if (runOld.RunOptionDef.InteractionsN) line2.Add(DeltaDMwnt == 0 ? 1 : (DeltaDMa - DeltaDMn) / DeltaDMa, 3);
                                        if (runOld.RunOptionDef.InteractionsT) line2.Add(DeltaDMwnt == 0 ? 1 : (DeltaDMa - DeltaDMt) / DeltaDMa, 3);
                                        if (runOld.RunOptionDef.InteractionsWN) line2.Add(DeltaDMwnt == 0 ? 1 : (DeltaDMa - DeltaDMwn) / DeltaDMa, 3);
                                        if (runOld.RunOptionDef.InteractionsWT) line2.Add(DeltaDMwnt == 0 ? 1 : (DeltaDMa - DeltaDMwt) / DeltaDMa, 3);
                                        if (runOld.RunOptionDef.InteractionsNT) line2.Add(DeltaDMwnt == 0 ? 1 : (DeltaDMa - DeltaDMnt) / DeltaDMa, 3);
                                        if (runOld.RunOptionDef.InteractionsWNT) line2.Add(DeltaDMwnt == 0 ? 1 : (DeltaDMa - DeltaDMwnt) / DeltaDMa, 3);
                                    }
                                }
                            }

                            else if (Name == "DailyLaminaesurfacearea")
                            {
                                for (var j = 0; j < finalLeafNum; ++j)
                                {
                                    line2.Add(universe.Crop_.getLeafLaminaAreaIndexForLeafLayer(j), 4);
                                }
                            }

                            else if (Name == "DailyLaminaeTotalN")
                            {
                                for (var j = 0; j < finalLeafNum; ++j)
                                {
                                    line2.Add(universe.Crop_.getLeafLaminaTotalNForLeafLayer(j), 4,10);
                                }
                            }
                            else
                            {
                                throw new Exception(Name + ": (" + Def + ") is not part of the customized daily outputs.");
                            }
                        }
                    }
                    //line2.Add(universe.Crop_.TotalPAR, 2);
                    //line2.Add(universe.Crop_.TotalLUE, 2);
                    page.Add(line2);
                }
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
            return page;
        }
        
        internal override LineData MultiRunLine(Run runOld)
            // Prints the summary variables line by line;
        {
            ///<Behnam (2016.01.12)>
            ///<Comment>Now, it is possible to define the outputs in RunOptionItem class as listDaily and listSummary
            ///and use them as sorted lists to create check boxes in the Run Option form and also to rearrange the 
            ///daily and summary outputs. To rearrange, we only need to change listDaily and listSummary.
            ///The Namr referes to the name of the properties of RunOptionItem class.<Comment> 

            var line = new LineData();
            var List = RunOptionItem.ListSummary;

            if (!Run.SecondLine)
            {
                for (var i = 0; i < List.Count; ++i)
                {
                    var Name = List.Keys.ElementAt(i);
                    var Def = List[Name];
                    var Flag = typeof(RunOptionItem).GetProperty(Name).GetValue(runOld.RunOptionDef);
                    if ((bool)Flag)
                    {
                        if (Name == "SowingWindow")
                        {
                            line.Add("Start of final sowing window");
                            line.Add("End of final sowing window");
                        }
                        else line.Add(Def);
                    }
                }
            }

            if (Run.SecondLine)
            {
                var emergenceUniverse = GetUniverse(runOld, GrowthStage.ZC_10_Emergence);
                var anthesisUniverse = GetUniverse(runOld, GrowthStage.ZC_65_Anthesis);
                var maturityUniverse = GetUniverse(runOld, GrowthStage.ZC_92_Maturity);
                var endofgrainfillingUniverse = GetUniverse(runOld, GrowthStage.ZC_91_EndGrainFilling);

                for (var i = 0; i < List.Count; ++i)
                {
                    var Name = List.Keys.ElementAt(i);
                    var Def = List[Name];
                    var Flag = typeof(RunOptionItem).GetProperty(Name).GetValue(runOld.RunOptionDef);

                    if ((bool)Flag) AddSummaryItems(Name, Def, runOld, line, emergenceUniverse, anthesisUniverse, endofgrainfillingUniverse, maturityUniverse);
                }
            }

            Run.SecondLine = true;
            return line; 
        }

        private void AddSummaryItems(string Name, string Def, Run runOld, LineData line,
            Universe emergenceUniverse, Universe anthesisUniverse, Universe endofgrainfillingUniverse, Universe maturityUniverse)
        {
            if (Name == "Management") if (runOld.ManagementDef != null) line.Add(runOld.ManagementDef.Name); else line.Add("?");
            else if (Name == "NonVarietalParameters") if (runOld.ParameterDef != null) line.Add(runOld.ParameterDef.Name); else line.Add("?");
            else if (Name == "VarietalParameters") if (runOld.VarietyDef != null) line.Add(runOld.VarietyDef.Name); else line.Add("?");
            else if (Name == "RunOptions") if (runOld.RunOptionDef != null) line.Add(runOld.RunOptionDef.Name); else line.Add("?");
            else if (Name == "Site") if (runOld.SiteDef != null) line.Add(runOld.SiteDef.Name); else line.Add("?");
            else if (Name == "Soil") if (runOld.SoilDef != null) line.Add(runOld.SoilDef.Name); else line.Add("?");
            else if (Name == "SowingYear")
            {
                ///<Behnam (2015.10.22)>
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
            }
            else if (Name == "SowingWindow")
            {
                line.Add(runOld.ManagementDef != null ? runOld.SiteDef.FinalMinSowingDate.ToString("u").Split()[0] : "?");
                line.Add(runOld.ManagementDef != null ? runOld.SiteDef.FinalMaxSowingDate.ToString("u").Split()[0] : "?");
            }
            else if (Name == "SowingDateOut") PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_00_Sowing);
            else if (Name == "SowingDateOutDOY") PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_00_Sowing,true);
            else if (Name == "EmergenceDate") PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_10_Emergence);
            else if (Name == "EmergenceDateDOY") PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_10_Emergence,true);
            else if (Name == "EmergenceDay") if (maturityUniverse != null) line.Add(emergenceUniverse.GrowthDay); else line.Add("?");
            else if (Name == "EndVernalizationDate") PrintMultiRunSummaryDate(line, runOld, GrowthStage.EndVernalisation);
            else if (Name == "FirstNodeDetectable") PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_31_1stNodeDetectable);
            else if (Name == "BeginningOfStemExtension") PrintMultiRunSummaryDate(line, runOld, GrowthStage.BeginningStemExtension);
            else if (Name == "TerminalSpikeletDate") PrintMultiRunSummaryDate(line, runOld, GrowthStage.TerminalSpikelet);
            else if (Name == "FlagLeafLiguleJustVisibleDate") PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_39_FlagLeafLiguleJustVisible);
            else if (Name == "HeadingDate") PrintMultiRunSummaryDate(line, runOld, GrowthStage.Heading);
            else if (Name == "AnthesisDate") PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_65_Anthesis);
            else if (Name == "AnthesisDateDOY") PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_65_Anthesis,true);
            else if (Name == "AnthesisDay") if (maturityUniverse != null) line.Add(anthesisUniverse.GrowthDay); else line.Add("?");
            else if (Name == "EndOfCellDivision") PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_75_EndCellDivision);
            else if (Name == "EndOfGrainFilling") PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_91_EndGrainFilling);
            else if (Name == "MaturityDate") PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_92_Maturity);
            else if (Name == "MaturityDateDOY") PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_92_Maturity,true);
            else if (Name == "MaturityDay") if (maturityUniverse != null) line.Add(maturityUniverse.GrowthDay); else line.Add("?");
            else if (Name == "MeanTempAnthesis") if (maturityUniverse != null) line.Add(anthesisUniverse.CumAirTempFromSowing / anthesisUniverse.GrowthDay, 2); else line.Add("?");
            else if (Name == "MeanTempAnth2Maturity") if (maturityUniverse != null) line.Add((maturityUniverse.CumAirTempFromSowing - anthesisUniverse.CumAirTempFromSowing) / ((maturityUniverse.GrowthDay - anthesisUniverse.GrowthDay)), 2); else line.Add("?");
            else if (Name == "MeanTempMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.CumAirTempFromSowing / maturityUniverse.GrowthDay, 2); else line.Add("?");
            else if (Name == "MeanMaxCanopyTempMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.CumMaxCanopyTempFromSowing / maturityUniverse.GrowthDay, 2); else line.Add("?");
            else if (Name == "PhysTempAnthesis") if (maturityUniverse != null) line.Add(anthesisUniverse.thermalTimeWrapper_.getCumulTT(Delta.Physiology), 2); else line.Add("?");
            else if (Name == "MeanMaxAirTempMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.CumMaxAirTempFromSowing / maturityUniverse.GrowthDay, 2); else line.Add("?");
            else if (Name == "PhysTempAnth2Maturity") if (maturityUniverse != null) line.Add((maturityUniverse.thermalTimeWrapper_.getCumulTT(Delta.Physiology) - anthesisUniverse.thermalTimeWrapper_.getCumulTT(Delta.Physiology)), 2); else line.Add("?");
            else if (Name == "PhysTempMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.thermalTimeWrapper_.getCumulTT(Delta.Physiology), 2); else line.Add("?");
            else if (Name == "FinalLeafNumberOption") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.LeafNumber, 2); else line.Add("?");
            else if (Name == "LAIatAnthesis") if (anthesisUniverse != null) line.Add(anthesisUniverse.Crop_.LaminaeAI, 2); else line.Add("?");
            else if (Name == "GAIatAnthesis") if (anthesisUniverse != null) line.Add(anthesisUniverse.Crop_.GAI, 2); else line.Add("?");
            else if (Name == "CropDryMatAnthesis") if (anthesisUniverse != null) line.Add(anthesisUniverse.Crop_.OutputTotalDM, 0, 10); else line.Add("?");
            else if (Name == "CropDryMatMatururity") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.OutputTotalDM, 0, 10); else line.Add("?");
            else if (Name == "GrainDryMatMatururity") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.GrainTotalDM, 0, 10); else line.Add("?");
            else if (Name == "NNIatAnthesis") if (anthesisUniverse != null) line.Add(anthesisUniverse.Crop_.NNI, 2); else line.Add("?");
            else if (Name == "CropNatAnthesis") if (anthesisUniverse != null) line.Add(anthesisUniverse.Crop_.OutputTotalN, 0, 10); else line.Add("?");
            else if (Name == "CropNatMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.OutputTotalN, 0, 10); else line.Add("?");
            else if (Name == "GrainNatMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.GrainTotalN, 0, 10); else line.Add("?");
            else if (Name == "PostAnthesisCropNUptake") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.PostAnthesisNUptake, 0, 10); else line.Add("?");
            else if (Name == "SingleGrainDMatMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.TotalDMperGrain, 2); else line.Add("?");
            else if (Name == "SingleGrainNatMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.TotalNperGrain, 2); else line.Add("?");
            else if (Name == "GrainProteinAtMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.ProteinConcentration, 2); else line.Add("?");
            else if (Name == "GrainNumberOption") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.GrainNumber, 0); else line.Add("?");
            else if (Name == "StarchAtMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.PercentStarch, 4); else line.Add("?");
            else if (Name == "AlbuminsAtMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Starch, 2); else line.Add("?");
            else if (Name == "AmphiphilsAtMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Namp, 2); else line.Add("?");
            else if (Name == "GliadinsAtMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Ngli, 2); else line.Add("?");
            else if (Name == "GluteninsAtMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Nglu, 2); else line.Add("?");
            else if (Name == "GliadinsPAtMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.PercentGli, 3); else line.Add("?");
            else if (Name == "GluteinsPAtMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.PercentGlu, 3); else line.Add("?");
            else if (Name == "GliadinsToGluteinsOption") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.GliadinsToGluteins, 2); else line.Add("?");
            else if (Name == "HarvestIndex") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.HarvestIndexDM, 2); else line.Add("?");
            else if (Name == "NHarvestIndex") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.HarvestIndexN, 2); else line.Add("?");
            else if (Name == "RainIrrigationAnthesis") if (anthesisUniverse != null) line.Add(anthesisUniverse.Soil_.IncomeWater, 1, Run.GwaterToMMwater); else line.Add("?");
            else if (Name == "RainIrrigationAnth2Maturity") if (maturityUniverse != null) line.Add((maturityUniverse.Soil_.IncomeWater - anthesisUniverse.Soil_.IncomeWater), 1, Run.GwaterToMMwater); else line.Add("?");
            else if (Name == "RainIrrigationMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.IncomeWater, 1, Run.GwaterToMMwater); else line.Add("?");
            else if (Name == "CumPotETAnthesis") if (maturityUniverse != null) line.Add(anthesisUniverse.Soil_.AccumulatedPotEvapoTranspiration, 2, Run.GwaterToMMwater); else line.Add("?");
            else if (Name == "CumPotETAnth2Maturity") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.AccumulatedPotEvapoTranspiration - anthesisUniverse.Soil_.AccumulatedPotEvapoTranspiration, 2, Run.GwaterToMMwater); else line.Add("?");
            else if (Name == "CumPotETMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.AccumulatedPotEvapoTranspiration, 2, Run.GwaterToMMwater); else line.Add("?");
            else if (Name == "CumActETAnthesis") if (maturityUniverse != null) line.Add(anthesisUniverse.Soil_.AccumulatedActEvapoTranspiration, 2, Run.GwaterToMMwater); else line.Add("?");
            else if (Name == "CumActETAnth2Maturity") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.AccumulatedActEvapoTranspiration - anthesisUniverse.Soil_.AccumulatedActEvapoTranspiration, 2, Run.GwaterToMMwater); else line.Add("?");
            else if (Name == "CumActETMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.AccumulatedActEvapoTranspiration, 2, Run.GwaterToMMwater); else line.Add("?");
            else if (Name == "CumActTrAnthesis") if (anthesisUniverse != null) line.Add(anthesisUniverse.Soil_.AccumulatedActTranspiration, 2, Run.GwaterToMMwater); else line.Add("?");
            else if (Name == "CumActTrAnth2Maturity") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.AccumulatedActTranspiration - anthesisUniverse.Soil_.AccumulatedActTranspiration, 2, Run.GwaterToMMwater); else line.Add("?");
            else if (Name == "CumActTrMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.AccumulatedActTranspiration, 2, Run.GwaterToMMwater); else line.Add("?");
            else if (Name == "CumEvaporation") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.AccumulatedActSoilEvaporation, 2, Run.GwaterToMMwater); else line.Add("?");
            else if (Name == "TotalAvaiSoilN") if (maturityUniverse != null) line.Add((maturityUniverse.Soil_.CalculateTotalN() + maturityUniverse.Soil_.SoilDeepLayer_.LostNitrogen + maturityUniverse.Crop_.CropTotalN), 1, 10); else line.Add("?");
            else if (Name == "NLeaching") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.SoilDeepLayer_.LostNitrogen, 2, 10); else line.Add("?");
            else if (Name == "Drainage") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.SoilDeepLayer_.LostWater, 1, Run.GwaterToMMwater); else line.Add("?");
            else if (Name == "CumNMineralization") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.AccumulatedNitrogenMineralisation, 2, 10); else line.Add("?");
            else if (Name == "CumNDenitrification") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.AccumulatedNitrogenDenitrification, 2, 10); else line.Add("?");
            else if (Name == "FPAWatAnthesis") if (maturityUniverse != null) line.Add(anthesisUniverse.Soil_.FPAW, 2); else line.Add("?");
            else if (Name == "FPAWatMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.FPAW, 2); else line.Add("?");
            else if (Name == "AvailableWaterinSoilatMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.CalculateAvailableExcessW(), 2, Run.GwaterToMMwater); else line.Add("?");
            else if (Name == "AvailableMineralNinSoilAtMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.CalculateAvailableExcessN(), 2, 10); else line.Add("?");
            else if (Name == "TotalSoilMineralNatMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.CalculateTotalN(), 2, 10); else line.Add("?");
            else if (Name == "AvailableWaterinRootZoneMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.CalculateRootZoneAvailableExcessWater(), 2, Run.GwaterToMMwater); else line.Add("?");
            else if (Name == "AvailableMineralNinRootZoneMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.CalculateRootZoneAvailableExcessN(), 2, 10); else line.Add("?");
            else if (Name == "TotalMineralNinRootZoneMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.CalculateRootZoneTotalN(), 2, 10); else line.Add("?");
            else if (Name == "NUseEfficiency") if (maturityUniverse != null) line.Add(maturityUniverse.NuseEfficiency, 2); else line.Add("?");
            else if (Name == "NUtilisationEfficiency") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.NutilisationEfficiency, 2); else line.Add("?");
            else if (Name == "NUptakeEfficiency") if (maturityUniverse != null) line.Add(maturityUniverse.NuptakeEfficiency, 2); else line.Add("?");
            else if (Name == "WaterUseEfficiency") if (maturityUniverse != null) line.Add(maturityUniverse.WaterUseEfficiency, 2); else line.Add("?");
            else if (Name == "CumNApplied") if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.CumulativeNfertilisation, 2, 10); else line.Add("?");
            else if (Name == "CO2AtEmergence") if (emergenceUniverse != null) line.Add(emergenceUniverse.Crop_.DailyCO2, 2); else line.Add("?");
            else if (Name == "CO2AtMaturity") if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.DailyCO2, 2); else line.Add("?");
            else if (Name == "AccumPAREmerge2EndGrainFill") if (maturityUniverse != null) line.Add(endofgrainfillingUniverse.Crop_.AccumPAR - emergenceUniverse.Crop_.AccumPAR, 2); else line.Add("?");
            else
            {
                throw new Exception(Name + ": (" + Def + ") is not part of the customized summary outputs.");
            }
        }

        public static void PrintMultiRunSummaryDate(LineData line, Run runOld, GrowthStage moment,bool isDOY=false)
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

                //loic, add posibility to write the date in YYYYDDD format

                // if (lastUniverse.Crop_.Calendar[moment].HasValue && moment != GrowthStage.ZC_00_Sowing && 
                // moment != GrowthStage.ZC_39_FlagLeafLiguleJustVisible)
                // {
                // momentDate = lastUniverse.Crop_.Calendar[moment].Value.AddDays(-1);
                // }

                ///</Behnam>

                if (!isDOY) line.Add(momentDate.HasValue ? momentDate.Value.ToString("yyyy-MM-dd") : "?");
                else {

                        var Date=momentDate.Value;
                        var Year = Date.Year;

                        line.Add(momentDate.HasValue ? string.Format("{0}{1}", Year, Date.DayOfYear) : "?");

                }
            }
            else line.Add("?");
        }

        public static IEnumerable<GrowthStage> GrowthStageToOutput()
        {
            yield return GrowthStage.ZC_00_Sowing;
            yield return GrowthStage.EndVernalisation; 
            yield return GrowthStage.BeginningStemExtension;
            yield return GrowthStage.Heading;
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

            // Print summary file main header
            PrintVersionDate(page);
            PrintFiles(page);
            page.NewLine();
            return page;
        }

        internal override LineData SensitivityRunLine(Run runOld, string[] deltaHeader, double[] deltaValue)
        {

            ///<Behnam (2016.01.12)>
            ///<Comment>Now, it is possible to define the outputs in RunOptionItem class as listDaily and listSummary
            ///and use them as sorted lists to create check boxes in the Run Option form and also to rearrange the 
            ///daily and summary outputs. To rearrange, we only need to change listDaily and listSummary<Comment> 

            var line = new LineData();
            var List = RunOptionItem.ListSummary;

            if (!Run.SecondLine)
            {
                line.Add(deltaHeader);
                for (var i = 0; i < List.Count; ++i)
                {
                    var Name = List.Keys.ElementAt(i);
                    var Def = List[Name];
                    var Flag = typeof(RunOptionItem).GetProperty(Name).GetValue(runOld.RunOptionDef);
                    if ((bool)Flag)
                    {
                        if (Name == "SowingWindow")
                        {
                            line.Add("Start of final sowing window");
                            line.Add("End of final sowing window");
                        }
                        else line.Add(Def);
                    }
                }
            }

            if (Run.SecondLine)
            {
                var emergenceUniverse = GetUniverse(runOld, GrowthStage.ZC_10_Emergence);
                var anthesisUniverse = GetUniverse(runOld, GrowthStage.ZC_65_Anthesis);
                var maturityUniverse = GetUniverse(runOld, GrowthStage.ZC_92_Maturity);
                var endofgrainfillingUniverse = GetUniverse(runOld, GrowthStage.ZC_91_EndGrainFilling);
                
                for (var m = 0; m < deltaValue.Length; ++m) deltaValue[m] = Math.Round(deltaValue[m], 4);
                line.Add(deltaValue);

                for (var i = 0; i < List.Count; ++i)
                {
                    var Name = List.Keys.ElementAt(i);
                    var Def = List[Name];
                    var Flag = typeof(RunOptionItem).GetProperty(Name).GetValue(runOld.RunOptionDef);

                    if ((bool)Flag) AddSummaryItems(Name, Def, runOld, line, emergenceUniverse, anthesisUniverse, endofgrainfillingUniverse, maturityUniverse);
                }
            }

            Run.SecondLine = true;
            return line; 
        }

        #endregion

    }
}
///</Behnam>
