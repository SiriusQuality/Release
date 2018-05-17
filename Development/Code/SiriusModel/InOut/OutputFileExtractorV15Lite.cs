using SiriusModel.InOut.OutputWriter;
using SiriusModel.Model;
using SiriusModel.Model.CropModel;
using SiriusModel.Model.Observation;
using SiriusModel.Model.SoilModel;

namespace SiriusModel.InOut
{
    internal class OutputFileExtractorV15Lite : OutputFileExtractor
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

            PrintDailyCrop(page, runOld);
            page.SetHeight(300 + headerHeight);

            PrintDailyWaterSoil(page, runOld);
            page.SetHeight(600 + headerHeight);

            PrintDailyNitrogenSoil(page, runOld);
            page.SetHeight(900 + headerHeight);

            PrintDailyLaminae(page, runOld);
            page.SetHeight(1200 + headerHeight);

            PrintDailySheath(page, runOld);
            page.SetHeight(1500 + headerHeight);

            PrintDailyInternodes(page, runOld);
            page.SetHeight(1800 + headerHeight);

            PrintDailyTillerLaminaeAreaIndex(page, runOld);
            page.SetHeight(2100 + headerHeight);//Add one more output of leaf area of different tillers. Modified by J.He, 06-19-2009.

            PrintDailyTillerSheathAreaIndex(page, runOld);
            page.SetHeight(2400 + headerHeight);//Add one more output of sheath area of different tillers. Modified by J.He, 06-19-2009.

            return page;
        }

        public static void PrintNormalRunSummaryDate(PageData page, Run runOld)
        {
            page.NewLine();
            page.NewLine().Add("Date").Add("DOY");
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_00_Sowing);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_10_Emergence);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.BeginningStemExtension);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_65_Anthesis);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_75_EndCellDivision);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_91_EndGrainFilling);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_92_Maturity);
            PrintNormalRunSummaryDate(page, runOld, GrowthStage.ZC_39_FlagLeafLiguleJustVisible);// Add the output of Flag Leaf Ligule Just visible. Modified by J.He, 06-19-2009.
            //page.NewLine();
        }

        public static void PrintNormalRunSummaryDate(PageData page, Run runOld, GrowthStage moment)
        {
            var lastUniverse = GetLastUniverse(runOld);
            if (lastUniverse != null)
            {
                var momentDate = lastUniverse.Calendar_[moment];
                if (momentDate.HasValue)
                {
                    page.NewLine().AddDateDOY(momentDate).Add(moment.Title());
                }
                else page.NewLine().AddDateDOY(null).Add(moment.Title());
            }
            else page.NewLine().AddDateDOY(null).Add(moment.Title());
        }

        public static void PrintNormalRunSummaryCrop(PageData page, Run runOld)
        {
            var anthesisUniverse = GetUniverse(runOld, GrowthStage.ZC_65_Anthesis);
            var maturityUniverse = GetUniverse(runOld, GrowthStage.ZC_92_Maturity);

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Shoot_.Phytomers_.Number, 2).Add("Final leaf number");
            else page.NewLine().Add("?.??").Add("Final leaf number");

            if (anthesisUniverse != null) page.NewLine().Add(anthesisUniverse.Crop_.Shoot_.Phytomers_.LaminaeAI, 2).Add("LAI at anthesis (m?m?");
            else page.NewLine().Add("?.??").Add("LAI at anthesis (m?m?");

            if (anthesisUniverse != null) page.NewLine().Add(anthesisUniverse.Crop_.Shoot_.Phytomers_.GAI, 2).Add("GAI at anthesis (m?m?");
            else page.NewLine().Add("?.??").Add("GAI at anthesis (m?m?");

            if (anthesisUniverse != null) page.NewLine().Add(anthesisUniverse.Crop_.OutputTotalDM, 0, 10).Add("Crop DM at anthesis (kgDM/ha)");
            else page.NewLine().Add("?.??").Add("Crop DM at anthesis (kgDM/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.OutputTotalDM, 0, 10).Add("Crop DM at maturity (kgDM/ha)");
            else page.NewLine().Add("?").Add("Crop DM at maturity (kgDM/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.TotalDM, 0, 10).Add("Grain DM at maturity (kgDM/ha)");
            else page.NewLine().Add("?").Add("Grain DM at maturity (kgDM/ha)");

            if (anthesisUniverse != null) page.NewLine().Add(anthesisUniverse.Crop_.OutputTotalN, 0, 10).Add("Crop N at anthesis (kgN/ha)");
            else page.NewLine().Add("?").Add("Crop N at anthesis (kgN/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.OutputTotalN, 0, 10).Add("Crop N at maturity (kgN/ha)");
            else page.NewLine().Add("?").Add("Crop N at maturity (kgN/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.TotalN, 0, 10).Add("Grain N at maturity (kgN/ha)");
            else page.NewLine().Add("?").Add("Grain N at maturity (kgN/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.PostAnthesisNUptake, 0, 10).Add("Post-anthesis crop N uptake (kgN/ha)");
            else page.NewLine().Add("?").Add("Post-anthesis crop N uptake (kgN/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.TotalDMperGrain, 2).Add("Single grain DM at maturity (mgDM/grain)");
            else page.NewLine().Add("?").Add("Single grain DM at maturity (mgDM/grain)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.TotalNperGrain, 2).Add("Single grain N at maturity (mgN/grain)");
            else page.NewLine().Add("?").Add("Single grain N at maturity (mgN/grain)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.ProteinConcentration, 4).Add("Grain protein concentration at maturity (% of grain DM)");
            else page.NewLine().Add("?.??").Add("Grain protein concentration at maturity (% of grain DM)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.Number, 0).Add("Grain number (grain/m?");
            else page.NewLine().Add("?").Add("Grain number (grain/m?");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.PercentStarch, 4).Add("% Starch at maturity (% of total grain DM)");
            else page.NewLine().Add("?").Add("% Starch at maturity (% of total grain DM)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.NalbGlo, 2).Add("Albumins-globulins at maturity (mgN/grain)");
            else page.NewLine().Add("?.??").Add("Albumins-globulins at maturity (mgN/grain)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.Namp, 2).Add("Amphiphils at maturity (mgN/grain)");
            else page.NewLine().Add("?.??").Add("Amphiphils at maturity (mgN/grain)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.Ngli, 2).Add("Gliadins at maturity (mgN/grain)");
            else page.NewLine().Add("?.??").Add("Gliadins at maturity (mgN/grain)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.Nglu, 2).Add("Glutenins at maturity (mgN/grain)");
            else page.NewLine().Add("?.??").Add("Glutenins at maturity, mgN/grain)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.PercentGli, 3).Add("% Gliadins at maturity (% of total grain N)");
            else page.NewLine().Add("?.??").Add("% Gliadins at maturity (% of total grain N)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.PercentGlu, 3).Add("% Gluteins at maturity (% of total grain N)");
            else page.NewLine().Add("?.??").Add("% Gluteins at maturity (% of total grain N)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.GliadinsToGluteins, 2).Add("Gliadins-to-gluteins ratio (dimensionless)");
            else page.NewLine().Add("?.??").Add("Gliadins-to-gluteins ratio (dimensionless)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.HarvestIndexDM, 2).Add("DM harvest index (dimensionless)");
            else page.NewLine().Add("?.??").Add("DM harvest index (dimensionless)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.HarvestIndexN, 2).Add("N harvest index (dimensionless)");
            else page.NewLine().Add("?.??").Add("N harvest index (dimensionless)");

            if (maturityUniverse != null) page.NewLine().Add((maturityUniverse.Soil_.IncomeWater * Run.GwaterToMMwater), 1).Add("rainfall + irrigation (mm)");
            else page.NewLine().Add("?.??").Add("rainfall + irrigation (mm)");

            if (maturityUniverse != null) page.NewLine().Add((maturityUniverse.Soil_.CalculateTotalN() + maturityUniverse.Soil_.SoilDeepLayer_.LostNitrogen + maturityUniverse.Crop_.TotalN), 0, 10).Add("Total available soil N (kgN/ha)");
            else page.NewLine().Add("?.??").Add("Total available soil N (kgN/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Soil_.SoilDeepLayer_.LostNitrogen, 2, 10).Add("N leaching (kgN/ha)");
            else page.NewLine().Add("?.??").Add("N leaching (kgN/ha)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Soil_.SoilDeepLayer_.LostWater, 1, Run.GwaterToMMwater).Add("Water drainage (mm)");
            else page.NewLine().Add("?.??").Add("Water drainage (mm)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.NuseEfficiency, 2).Add("N use efficiency (kgDM/kgN)");
            else page.NewLine().Add("?.??").Add("N use efficiency (kgDM/kgN)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.NutilisationEfficiency, 2).Add("N utilisation efficiency (kgDM/kgN)");
            else page.NewLine().Add("?.??").Add("N utilisation efficiency (kgDM/kgN)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.NuptakeEfficiency, 2).Add("N upake efficiency (kgN/kgN)");
            else page.NewLine().Add("?.??").Add("N upake efficiency (kgN/kgN)");

            if (maturityUniverse != null) page.NewLine().Add(maturityUniverse.Crop_.Grain_.WaterUseEfficiency, 2).Add("Water use efficiency (kgDM/ha/mm)");
            else page.NewLine().Add("?.??").Add("Water use efficiency (kgDM/ha/mm)");

            page.NewLine(5);

            var titleTillering = new LineData().Add("Main stem density at anthesis (shoot/m?");

            var tilleringProfileLength = runOld.TilleringProfile.Length;
            for (var i = 1; i < tilleringProfileLength; ++i)
            {
                if (i == 1) titleTillering = titleTillering.Add("First order tiller density at anthesis (shoot/m?");
                else if (i == 2) titleTillering = titleTillering.Add("Second order tiller density at anthesis (shoot/m?");
                else if (i == 3) titleTillering = titleTillering.Add("Third order tiller density at anthesis (shoot/m?");
                else titleTillering = titleTillering.Add(i + "th order tiller density at anthesis (shoot/m?");
            }
            page.Add(titleTillering);

            var valueTillering = new LineData();
            if (tilleringProfileLength > 0)
            {
                for (var i = 0; i < tilleringProfileLength; ++i)
                {
                    valueTillering = valueTillering.Add(runOld.TilleringProfile[i]);
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

            page[3]
                .Add(FileContainer.ManagementID)
                .Add("Sowing date")
                .Add("Total inorganic N (kgN/ha)")
                .Add("% inorganic N in top 33%")
                .Add("% inorganic N in middle 33%")
                .Add("Grain number (grain/m?")
                .Add("Sowing density (seed/m?")
                .Add("Soil water deficit at sowing (mm)")
                .Add("CO2 (ppm)")
                .Add("Target fertile shoot number (shoot/m?");
            for (var i = 0; i < nbDate; ++i)
            {
                page[3]
                    .Add("Date")
                    .Add("N fertilisation (kgN/ha)")
                    .Add("Irrigation (mm)");
            }
            for (var i = 0; i < nbGs; ++i)
            {
                page[3]
                    .Add("Growth stage")
                    .Add("N fertilisation (kgN/ha)")
                    .Add("Irrigation (mm)");
            }
            if (item != null)
            {
                page[4]
                    .Add(item.Name)
                    .Add(item.SowingDate.ToString("dd/MM/yyyy"))
                    .Add(item.TotalNi, 2, 10)
                    .Add(item.TopNi)
                    .Add(item.MidNi)
                    .Add(item.ObservedGrainNumber)
                    .Add(item.SowingDensity)
                    .Add(item.SoilWaterDeficit)
                    .Add(item.CO2)
                    .Add(item.TargetFertileShootNumber);
            }
            else
            {
                page[4]
                    .Add("?");
            }
            for (var i = 0; i < nbDate; ++i)
            {
                page[4]
                    // ReSharper disable PossibleNullReferenceException
                    .Add(item.DateApplications[i].Date.ToString("dd/MM/yyyy"))
                    // ReSharper restore PossibleNullReferenceException
                    .Add(item.DateApplications[i].Nitrogen, 2, 10)
                    .Add(item.DateApplications[i].WaterMM);
            }
            for (var i = 0; i < nbGs; ++i)
            {
                page[4]
                    // ReSharper disable PossibleNullReferenceException
                    .Add(item.GrowthStageApplications[i].GrowthStage.Title())
                    // ReSharper restore PossibleNullReferenceException
                    .Add(item.GrowthStageApplications[i].Nitrogen, 2, 10)
                    .Add(item.GrowthStageApplications[i].WaterMM);
            }
        }

        public static void PrintParameterItem(PageData page, ParameterItem item)
        {
            page[6]
                .Add(FileContainer.ParameterID)
                .Add("AlphaAlbGlo")
                .Add("AlphaGlu")
                .Add("AlphaKn")
                .Add("AlphaNc")
                .Add("AlphaNNI")
                .Add("BetaAlbGlo")
                .Add("BetaGlu")
                .Add("BetaNNI")
                .Add("BetaRWU")
                .Add("Co2FacRue")
                .Add("DMmaxNuptake")
                .Add("FracAnth")
                .Add("FracBEAR")
                .Add("FracLaminaeBGR")
                .Add("IntermTvern")
                .Add("LowerFTSWbiomass")
                .Add("LowerFTSWexpansion")
                .Add("LowerFTSWsenescence")
                .Add("LowerFTSWtranspiration")
                .Add("LLOSS")
                .Add("MaxAnForP")
                .Add("MaxDSF")
                .Add("MaxLeafSoil")
                .Add("MaxLPhyll")
                .Add("MaxLeafRRNU")
                .Add("MaxStemN")
                .Add("MaxStemRRNU")
                .Add("MaxTvern")
                .Add("MinLPhyll")
                .Add("MinTvern")
                .Add("PhyllDecr")
                .Add("PhyllGroInterNode")
                .Add("PhyllGroLamina")
                .Add("PhyllIncr")
                .Add("PhyllMBLL")
                .Add("PhyllMSLL")
                .Add("PhyllSBLL")
                .Add("PhyllSSLL")
                .Add("RGRStruc")
                .Add("RVER")
                .Add("SLNcri")
                .Add("SLNmax0")
                .Add("SLNmin")
                .Add("SLWp")
                .Add("SlopeFR")
                .Add("SSWp")
                .Add("StdCo2")
                .Add("StrucLeafN")
                .Add("StrucStemN")
                .Add("TauSLN")
                .Add("Topt")
                .Add("TTcd")
                .Add("TTer")
                .Add("UpperFTSWbiomass")
                .Add("UpperFTSWexpansion")
                .Add("UpperFTSWsenescence")
                .Add("UpperFTSWtranspiration");

            if (item != null)
            {
                page[7]
                    .Add(item.Name)
                    .Add(item.AlphaAlbGlo)
                    .Add(item.AlphaGlu)
                    .Add(item.AlphaKn)
                    .Add(item.AlphaNc)
                    .Add(item.AlphaNNI)
                    .Add(item.BetaAlbGlo)
                    .Add(item.BetaGlu)
                    .Add(item.BetaNNI)
                    .Add(item.BetaRWU)
                    .Add(item.Co2FacRue)
                    .Add(item.DMmaxNuptake)
                    .Add(item.FracAnth)
                    .Add(item.FracBEAR)
                    .Add(item.FracLaminaeBGR)
                    .Add(item.IntermTvern)
                    .Add(item.LowerFTSWbiomass)
                    .Add(item.LowerFTSWexpansion)
                    .Add(item.LowerFTSWsenescence)
                    .Add(item.LowerFTSWtranspiration)
                    .Add(item.LLOSS)
                    .Add(item.MaxAnForP)
                    .Add(item.MaxDSF)
                    .Add(item.MaxLeafSoil)
                    .Add(item.MaxLPhyll)
                    .Add(item.MaxLeafRRNU)
                    .Add(item.MaxStemN)
                    .Add(item.MaxStemRRNU)
                    .Add(item.MaxTvern)
                    .Add(item.MinLPhyll)
                    .Add(item.MinTvern)
                    .Add(item.PhyllDecr)
                    .Add(item.PhyllGroInterNode)
                    .Add(item.PhyllGroLamina)
                    .Add(item.PhyllIncr)
                    .Add(item.PhyllMBLL)
                    .Add(item.PhyllMSLL)
                    .Add(item.PhyllSBLL)
                    .Add(item.PhyllSSLL)
                    .Add(item.RGRStruc)
                    .Add(item.RVER)
                    .Add(item.SLNcri)
                    .Add(item.SLNmax0)
                    .Add(item.SLNmin)
                    .Add(item.SLWp)
                    .Add(item.SlopeFR)
                    .Add(item.SSWp)
                    .Add(item.StdCo2)
                    .Add(item.StrucLeafN)
                    .Add(item.StrucStemN)
                    .Add(item.TauSLN)
                    .Add(item.Topt)
                    .Add(item.TTcd)
                    .Add(item.TTer)
                    .Add(item.UpperFTSWbiomass)
                    .Add(item.UpperFTSWexpansion)
                    .Add(item.UpperFTSWsenescence)
                    .Add(item.UpperFTSWtranspiration);
            }
            else
            {
                page[7].Add("?");
            }
        }

        public static void PrintRunOptionItem(PageData page, RunOptionItem item)
        {
            page[9]
                .Add(FileContainer.RunOptionID)
                .Add("Calculate grain number")
                .Add("Limited nitrogen")
                .Add("Limited water");
            if (item != null)
            {
                page[10]
                    .Add(item.Name)
                    .Add(item.CalculateGrainNumber)
                    .Add(item.LimitedNitrogen)
                    .Add("-?");
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
                .Add(FileContainer.SiteID)
                .Add("Elevation (m)")
                .Add("Latitude (?")
                .Add("Longitude (?")
                .Add("Format");

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
                    .Add(item.Format);
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
                .Add("N Mineralisation constant (1/day)")
                .Add("Organic N (kgN/ha)")
                .Add("Denitrification pulse (dimensionless)")
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

        public static void PrintVarietyItem(PageData page, VarietyItem item)
        {
            page[18]
                .Add(FileContainer.VarietyID)
                .Add("aSheath")
                .Add("AreaSLL")
                .Add("AreaPLL")
                .Add("AreaSSL")
                .Add("RatioFLPL")
                .Add("AMNLFNO")
                .Add("AMXLFNO")
                .Add("EarGR")
                .Add("GrowthHabit")
                .Add("Kl")
                .Add("LUE Diffuse")
                .Add("L_IN1")
                .Add("L_EP")
                .Add("NLL")
                .Add("PhyllFLLAnth")
                .Add("Phyllochron")
                .Add("SLDL")
                .Add("TTaegf")
                .Add("TTegfm")
                .Add("TTsoem")
                .Add("VAI")
                .Add("VBEE");

            if (item != null)
            {
                page[19]
                    .Add(item.Name)
                    .Add(item.aSheath)
                    .Add(item.AreaSLL)
                    .Add(item.AreaPLL)
                    .Add(item.AreaSSL)
                    .Add(item.RatioFLPL)
                    .Add(item.AMNLFNO)
                    .Add(item.AMXLFNO)
                    .Add(item.EarGR)
                    .Add(item.GrowthHabit)
                    .Add(item.Kl)
                    .Add(item.LueDiffuse)
                    .Add(item.L_IN1)
                    .Add(item.L_EP)
                    .Add(item.NLL)
                    .Add(item.PhyllFLLAnth)
                    .Add(item.Phyllochron)
                    .Add(item.SLDL)
                    .Add(item.TTaegf)
                    .Add(item.TTegfm)
                    .Add(item.TTsoem)
                    .Add(item.VAI)
                    .Add(item.VBEE);
            }
            else
            {
                page[19]
                    .Add("?");
            }
        }

        public static void PrintDailyCrop(PageData page, Run runOld)
        {
            page.NewLine()
                .Add("Date")
                .Add("DOY")
                .Add("Thermal time")
                .Add("Accumulated rain + irrigation")
                .Add("Accumulated ET")
                .Add("Rooting depth")
                .Add("Available water in root zone")
                .Add("Water deficit in root zone")
                .Add("FTSW")
                .Add("Biomass DF")
                .Add("Ear growth DF")
                .Add("Leaf expension DF")
                .Add("Senescence DF")
                .Add("Transpiraion DF")
                .Add("Accumulated N leaching")
                .Add("N nutrition index")
                .Add("Available N in root zone")
                .Add("Total N in root zone")
                .Add("Number of leaf")
                .Add("Shoot number")
                .Add("GAI")
                .Add("LAI")
                .Add("Stem length")
                .Add("Crop DM")
                .Add("Crop N")
                .Add("Grain DM")
                .Add("Grain N")
                .Add("Leaf DM")
                .Add("Leaf N")
                .Add("Laminae DM")
                .Add("Laminae N")
                .Add("Stem DM")
                .Add("Stem N")
                .Add("Exposed Sheath DM")
                .Add("Exposed Sheath N")
                .Add("SLN")
                .Add("SLW")
                .Add("DM per grain")
                .Add("Starch")
                .Add("N per grain")
                .Add("Albumins")
                .Add("Amphiphils")
                .Add("Gliadins")
                .Add("Glutenins")
                .Add("Post-anthesis crop n uptake")
                .Add("Soil Temperature")
                .Add("Canopy Temperature");

            page.NewLine()
                .Add("dd/mm/yyyy")
                .Add("")
                .Add("°Cd")
                .Add("mm")
                .Add("mm")
                .Add("m")
                .Add("mm")
                .Add("mm")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("kgN/ha")
                .Add("dimensionless")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("leaf/mainstem")
                .Add("shoot/m")
                .Add("m2/m2")
                .Add("m2/m2")
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
                .Add("gN/m2")
                .Add("gDM/m2")
                .Add("mgDM/grain")
                .Add("mg/grain")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("kgN/ha")
                .Add("°C")
                .Add("°C");

            var nbDay = runOld.SavedUniverses.Count;
            for (var i = 0; i < nbDay; ++i)
            {
                var universe = runOld.SavedUniverses[i];

                page.NewLine()
                    .AddDateDOY(universe.CurrentDate)
                    .Add(universe.Sensor_[Cumul.Air], 2)
                    .Add(universe.Soil_.IncomeWater, 2, Run.GwaterToMMwater)
                    .Add(universe.Soil_.AccumulatedEvapoTranspiration, 2, Run.GwaterToMMwater)
                    .Add(universe.Crop_.Root_.Length, 2)
                    .Add(universe.Soil_.CalculateRootZoneAvailableWater(), 2, Run.GwaterToMMwater)
                    .Add(universe.Soil_.CalculateRootZoneWaterDeficit(), 2, Run.GwaterToMMwater)
                    .Add(universe.StressFactor_.FTSW, 2)
                    .Add(universe.StressFactor_.DBF, 2)
                    .Add(universe.StressFactor_.DEBF, 2)
                    .Add(universe.StressFactor_.DEF, 2)
                    .Add(universe.StressFactor_.DSF, 2)
                    .Add(universe.StressFactor_.DTF, 2)
                    .Add(universe.Soil_.SoilDeepLayer_.LostNitrogen, 2, 10)
                    .Add(universe.Crop_.NNI, 3)
                    .Add(universe.Soil_.DailyAvNforRoots, 2, 10)
                    .Add((universe.Soil_.DailyAvNforRoots + universe.Soil_.DailyUnavNforRoots), 2, 10)
                    .Add(universe.Crop_.Phytomers_.Number, 2)
                    .Add(universe.Crop_.Phytomers_.ShootNumber, 0)
                    .Add(universe.Crop_.Phytomers_.GAI, 2)
                    .Add(universe.Crop_.Phytomers_.LaminaeAI, 2)
                    .Add(universe.Crop_.Phytomers_.Length, 4, Run.CMToM)
                    .Add(universe.Crop_.OutputTotalDM, 2, 10)
                    .Add(universe.Crop_.OutputTotalN, 2, 10)
                    .Add(universe.Crop_.Grain_.TotalDM, 2, 10)
                    .Add(universe.Crop_.Grain_.TotalN, 2, 10)
                    //leaf
                    //.Add(universe.Crop_.Phytomers_.TotalDM, 2, 10)
                    //.Add(universe.Crop_.Phytomers_.TotalN, 2, 10)
                    .Add((universe.Crop_.Phytomers_.OutputLaminaeDM + universe.Crop_.Phytomers_.OutputSheathDM), 2, 10)
                    .Add((universe.Crop_.Phytomers_.OutputLaminaeN + universe.Crop_.Phytomers_.OutputSheathN), 2, 10)
                    //laminae
                    .Add(universe.Crop_.Phytomers_.OutputLaminaeDM, 2, 10)
                    .Add(universe.Crop_.Phytomers_.OutputLaminaeN, 2, 10)
                    //stem
                    .Add((universe.Crop_.Phytomers_.OutputSheathDM + universe.Crop_.Stem_.DM.Total), 2, 10)
                    .Add((universe.Crop_.Phytomers_.OutputSheathN + universe.Crop_.Stem_.N.Total), 2, 10)
                    //sheath
                    .Add(universe.Crop_.Phytomers_.OutputSheathDM, 2, 10)
                    .Add(universe.Crop_.Phytomers_.OutputSheathN, 2, 10)
                    .Add(universe.Crop_.Phytomers_.SLN, 2)
                    .Add(universe.Crop_.Phytomers_.SLW(), 2)
                    .Add(universe.Crop_.Grain_.TotalDMperGrain, 4)
                    .Add(universe.Crop_.Grain_.Starch, 4)
                    .Add(universe.Crop_.Grain_.TotalNperGrain, 4)
                    .Add(universe.Crop_.Grain_.NalbGlo, 4)
                    .Add(universe.Crop_.Grain_.Namp, 4)
                    .Add(universe.Crop_.Grain_.Ngli, 4)
                    .Add(universe.Crop_.Grain_.Nglu, 4)
                    .Add(universe.Crop_.PostAnthesisNUptake, 0, 10)
                    .Add(universe.Soil_.MeanTemperature, 2)
                    .Add(universe.Crop_.Shoot_.MeanTemperature, 2);
            }
        }

        public static void PrintDailyWaterSoil(PageData page, Run runOld)
        {
            var titleLine = new LineData()
                .Add("Date").Add("Soil available water, mm").SetWidth(51)
                .Add("Date").Add("Soil excess water, mm").SetWidth(102)
                .Add("Date").Add("Soil unvailable water, mm").SetWidth(153);
            page.Add(titleLine);

            var nbLine = new LineData();
            var nbSoilLayer = GetNbSoilLayer(runOld);
            for (var i = 1; i < 4; ++i)
            {
                nbLine.Add("dd/mm/yyyy");
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
                var dateValue = universe.CurrentDate.ToString("dd/MM/yyyy");


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
                .Add("Date").Add("Soil available N (kgN/ha)").SetWidth(51)
                .Add("Date").Add("Soil excess N (kgN/ha)").SetWidth(102)
                .Add("Date").Add("Soil unvailable N (kgN/ha)").SetWidth(153);
            page.Add(titleLine);

            var nbLine = new LineData();
            var nbSoilLayer = GetNbSoilLayer(runOld);
            for (var i = 1; i < 4; ++i)
            {
                nbLine.Add("dd/mm/yyyy");
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
                var dateValue = universe.CurrentDate.ToString("dd/MM/yyyy");


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
                .Add("Date").Add("Laminae surface area per layer (m?m?").SetWidth(21)
                .Add("Date").Add("Laminae DM per layer (kgDM/ha)").SetWidth(42)
                .Add("Date").Add("Laminae N per layer (kgN/ha)").SetWidth(63)
                .Add("Date").Add("SLN per layer (gN/m?").SetWidth(84)
                .Add("Date").Add("SLW per layer (gDM/m?").SetWidth(105);

            page.NewLine();

            var finalLeafNum = GetFinalLeafNumber(runOld);
            for (var i = 0; i < 5; ++i)
            {
                page[page.Count - 1].Add("dd/mm/yyyy");
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
                var dateValue = universe.CurrentDate.ToString("dd/MM/yyy");
                page.NewLine().Add(dateValue);

                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);
                    if (ll != null)
                    {
                        page[page.Count - 1].Add(ll.GetLeafLamina().AreaIndex, 4);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(21);

                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);
                    if (ll != null)
                    {
                        page[page.Count - 1].Add(ll.GetLeafLamina().DM.Total, 4, 10);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(42);

                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);
                    if (ll != null)
                    {
                        page[page.Count - 1].Add(ll.GetLeafLamina().N.Total, 4, 10);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(63);

                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);
                    if (ll != null)
                    {
                        page[page.Count - 1].Add(ll.GetLeafLamina().SpecificN, 2);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(84);

                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);
                    if (ll != null)
                    {
                        page[page.Count - 1].Add(ll.GetLeafLamina().SpecificWeight, 2);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(105);
            }

        }

        public static void PrintDailySheath(PageData page, Run runOld)
        {
            page.NewLine()
                .Add("Date").Add("Sheath surface area per layer (m?m?").SetWidth(21)
                .Add("Date").Add("Sheath DM per layer (kgDM/ha)").SetWidth(42)
                .Add("Date").Add("Sheath N per layer (kgN/ha)").SetWidth(63)
                .Add("Date").Add("SSN per layer (gN/m?").SetWidth(84)
                .Add("Date").Add("SSW per layer (gDM/m?").SetWidth(105);

            page.NewLine();

            var finalLeafNum = GetFinalLeafNumber(runOld);
            for (var i = 0; i < 5; ++i)
            {
                page[page.Count - 1].Add("dd/mm/yyyy");
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
                var dateValue = universe.CurrentDate.ToString("dd/MM/yyyy");
                page.NewLine().Add(dateValue);

                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);
                    if (ll != null)
                    {
                        page[page.Count - 1].Add(ll.GetExposedSheath().AreaIndex, 4);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(21);

                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);
                    if (ll != null)
                    {
                        page[page.Count - 1].Add(ll.GetExposedSheath().DM.Total, 4, 10);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(42);

                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);
                    if (ll != null)
                    {
                        //pm 09/10/09
                        page[page.Count - 1].Add(ll.GetExposedSheath().N.Total, 4, 10);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(63);

                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);
                    if (ll != null)
                    {
                        page[page.Count - 1].Add(ll.GetExposedSheath().SpecificN, 2);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(84);

                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);
                    if (ll != null)
                    {
                        page[page.Count - 1].Add(ll.GetExposedSheath().SpecificWeight, 2);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
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
            page[page.Count - 1].Add("dd/mm/yyyy");
            for (var j = finalLeafNum; j >= 0; --j)
            {
                page[page.Count - 1].Add(j + 1);
            }
            page[page.Count - 1].SetWidth(22);

            var nbDay = runOld.SavedUniverses.Count;
            for (var i = 0; i < nbDay; ++i)
            {
                var universe = runOld.SavedUniverses[i];
                var dateValue = universe.CurrentDate.ToString("dd/MM/yyyy");
                page.NewLine().Add(dateValue);

                if (universe.Crop_.Phytomers_.EarPeduncle != null)
                {
                    page[page.Count - 1].Add(universe.Crop_.Phytomers_.EarPeduncle.InterNode.Length, 4);
                }
                else page[page.Count - 1].Add(0.0, 4);

                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);
                    if (ll != null)
                    {
                        page[page.Count - 1].Add(ll.GetInterNode().Length, 4);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(22);
            }
        }

        //Create a new method to print out the daily leaf area index for each tiller. Modified by J.He, 6-19-2009.
        public static void PrintDailyTillerLaminaeAreaIndex(PageData page, Run runOld)
        {
            page.NewLine()
                .Add("Date").Add("Main Stem Laminae surface area per layer (m?m?").SetWidth(21)
                .Add("Date").Add("Tiller 1 Laminae surface area per layer (m?m?").SetWidth(42)
                .Add("Date").Add("Tiller 2 Laminae surface area per layer (m?m?").SetWidth(63)
                .Add("Date").Add("Total Laminae surface area per layer (m?m?").SetWidth(84);

            page.NewLine();

            var finalLeafNum = GetFinalLeafNumber(runOld);

            for (var i = 0; i < 3; ++i)
            {
                page[page.Count - 1].Add("dd/mm/yyyy");
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
                var dateValue = universe.CurrentDate.ToString("dd/MM/yyyy");
                #region Print Daily Laminea Area Index of main stem.
                page.NewLine().Add(dateValue);

                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);

                    if (ll != null)
                    {
                        page[page.Count - 1].Add(ll.PotentialLaminaTillerAreaIndex(0), 4);

                        //page[page.Count - 1].Add(ll.GetLeafLamina().AreaIndex, 4);

                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }

                page[page.Count - 1].SetWidth(21);
                #endregion

                #region Print Daily Laminea Area Index of Tiller 1.
                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);

                    // RunInstance.TillerNumber > 1 to avoid crash when their is no tiller. Modified by P.Strato, 6-29-2009.
                    if (ll != null && runOld.TillerNumber > 1)
                    {
                        page[page.Count - 1].Add(ll.PotentialLaminaTillerAreaIndex(1), 4);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(42);
                #endregion

                #region Print Daily Laminea Area Index of Tiller 2.
                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);

                    // RunInstance.TillerNumber > 2 to avoid crash when their is only one tiller. Modified by P.Strato, 6-29-2009.
                    if (ll != null && runOld.TillerNumber > 2)
                    {
                        page[page.Count - 1].Add(ll.PotentialLaminaTillerAreaIndex(2), 4);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(63);
                #endregion

                #region Print Daily Laminea Area Index of total tillers.
                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);

                    // RunInstance.TillerNumber > 3 to avoid crash when their is only two tiller. Modified by P.Strato, 6-29-2009.
                    if (ll != null && runOld.TillerNumber > 3)
                    {
                        page[page.Count - 1].Add(ll.PotentialLaminaAreaIndex(), 4);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(84);
                #endregion

            }

        }  //Modified by J.He, 6-19-2009.

        //Create a new method to print out the daily sheath area index for each tiller. Modified by J.He, 6-19-2009.
        public static void PrintDailyTillerSheathAreaIndex(PageData page, Run runOld)
        {
            page.NewLine()
                .Add("Date").Add("Main Stem Sheath surface area per layer (m?m?").SetWidth(21)
                .Add("Date").Add("Tiller 1 Sheath surface area per layer (m?m?").SetWidth(42)
                .Add("Date").Add("Tiller 2 Sheath surface area per layer (m?m?").SetWidth(63)
                .Add("Date").Add("Total Sheath surface area per layer (m?m?").SetWidth(84);

            page.NewLine();

            var finalLeafNum = GetFinalLeafNumber(runOld);

            for (var i = 0; i < 3; ++i)
            {
                page[page.Count - 1].Add("dd/mm/yyyy");
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

                var dateValue = universe.CurrentDate.ToString("dd/MM/yyyy");
                #region Print Daily Sheath Area Index of main stem.
                page.NewLine().Add(dateValue);

                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);

                    if (ll != null)
                    {
                        page[page.Count - 1].Add(ll.PotentialSheathTillerAreaIndex(0), 4);

                        //page[page.Count - 1].Add(ll.GetLeafLamina().AreaIndex, 4);

                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }

                page[page.Count - 1].SetWidth(21);
                #endregion

                #region Print Daily Sheath Area Index of Tiller 1.
                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);

                    // RunInstance.TillerNumber > 1 to avoid crash when their is no tiller. Modified by P.Strato, 6-29-2009.
                    if (ll != null && runOld.TillerNumber > 1)
                    {
                        page[page.Count - 1].Add(ll.PotentialSheathTillerAreaIndex(1), 4);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(42);
                #endregion

                #region Print Daily Sheath Area Index of Tiller 2.
                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);

                    // RunInstance.TillerNumber > 2 to avoid crash when their is only one tiller. Modified by P.Strato, 6-29-2009.
                    if (ll != null && runOld.TillerNumber > 2)
                    {
                        page[page.Count - 1].Add(ll.PotentialSheathTillerAreaIndex(2), 4);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(63);
                #endregion

                #region Print Daily Sheath Area Index of total tillers.
                page[page.Count - 1].Add(dateValue);
                for (var j = finalLeafNum - 1; j >= 0; --j)
                {
                    var ll = universe.Crop_.Phytomers_.GetLeafLayer(j);

                    // RunInstance.TillerNumber > 3 to avoid crash when their is only two tiller. Modified by P.Strato, 6-29-2009.
                    if (ll != null && runOld.TillerNumber > 3)
                    {
                        page[page.Count - 1].Add(ll.PotentialSheathAreaIndex(), 4);
                    }
                    else page[page.Count - 1].Add(0.0, 4);
                }
                page[page.Count - 1].SetWidth(84);
                #endregion

            }

        }  //Modified by J.He, 6-19-2009.

        #endregion

        #region multi

        internal override PageData MultiRunHeader()
        {
            var page = new PageData();

            PrintVersionDate(page);
            PrintFiles(page);

            page.NewLine();

            PrintMultiRunHeader(page);

            return page;
        }

        public static void PrintMultiRunHeader(PageData page)
        {
            page.NewLine()
                .Add(FileContainer.ManagementID)
                .Add(FileContainer.ParameterID)
                .Add(FileContainer.RunOptionID)
                .Add(FileContainer.SiteID)
                .Add(FileContainer.SoilID)
                .Add(FileContainer.VarietyID)

                .AddNull()

                .Add("Sowing year")
                .Add(GrowthStage.ZC_00_Sowing.Title())
                .Add(GrowthStage.ZC_10_Emergence.Title())
                .Add(GrowthStage.BeginningStemExtension.Title())
                .Add(GrowthStage.ZC_65_Anthesis.Title())
                .Add(GrowthStage.ZC_75_EndCellDivision.Title())
                .Add(GrowthStage.ZC_91_EndGrainFilling.Title())
                .Add(GrowthStage.ZC_92_Maturity.Title())

                .AddNull()


                .Add("Final leaf number")
                .Add("LAI at anthesis")
                .Add("GAI at anthesis")
                .Add("Crop DM at anthesis")
                .Add("Crop DM at maturity")
                .Add("Grain DM at maturity")
                .Add("Crop N at anthesis")
                .Add("Crop N at maturity")
                .Add("Grain N at maturity")
                .Add("Post-anthesis crop N uptake")
                .Add("Single grain DM at maturity")
                .Add("Single grain N at maturity")
                .Add("Grain protein concentration at maturity")
                .Add("Grain number")
                .Add("% Starch at maturity")
                .Add("Albumins-globulins at maturity")
                .Add("Amphiphils at maturity")
                .Add("Gliadins at maturity")
                .Add("Glutenins at maturity")
                .Add("% Gliadins at maturity")
                .Add("% Gluteins at maturity")
                .Add("Gliadins-to-gluteins ratio")
                .Add("DM harvest index")
                .Add("N harvest index")
                .Add("rainfall + irrigation")
                .Add("Total available soil N")
                .Add("N leaching")
                .Add("Water drainage")
                .Add("N use efficiency")
                .Add("N utilisation efficiency")
                .Add("N upake efficiency")
                .Add("Water use efficiency");

            page.NewLine()

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

                .Add("leaf")
                .Add("m2/m2")
                .Add("m2/m2")
                .Add("kgDM/ha")
                .Add("kgDM/ha")
                .Add("kgDM/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("mgDM/grain")
                .Add("mgN/grain")
                .Add("% of grain DM")
                .Add("grain/m2")
                .Add("% of total grain DM")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("% of total grain N")
                .Add("% of total grain N")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("mm")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("mm")
                .Add("kgDM/kgN")
                .Add("kgDM/kgN")
                .Add("kgN/kgN")
                .Add("kgDM/ha/mm");
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
            line.Add(runOld.ManagementDef != null ? runOld.ManagementDef.SowingDate.Year.ToString() : "?");
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
                var momentDate = lastUniverse.Calendar_[moment];
                line.Add(momentDate.HasValue ? momentDate.Value.DayOfYear.ToString() : "?");
            }
            else line.Add("?");
        }

        public static void PrintMultiRunSummaryCrop(LineData line, Run runOld)
        {
            var anthesisUniverse = GetUniverse(runOld, GrowthStage.ZC_65_Anthesis);
            var maturityUniverse = GetUniverse(runOld, GrowthStage.ZC_92_Maturity);

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Shoot_.Phytomers_.Number, 2);
            else line.Add("?");

            if (anthesisUniverse != null) line.Add(anthesisUniverse.Crop_.Shoot_.Phytomers_.LaminaeAI, 2);
            else line.Add("?");

            if (anthesisUniverse != null) line.Add(anthesisUniverse.Crop_.Shoot_.Phytomers_.GAI, 2);
            else line.Add("?");

            if (anthesisUniverse != null) line.Add(anthesisUniverse.Crop_.OutputTotalDM, 0, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.OutputTotalDM, 0, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.TotalDM, 0, 10);
            else line.Add("?");

            if (anthesisUniverse != null) line.Add(anthesisUniverse.Crop_.OutputTotalN, 0, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.OutputTotalN, 0, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.TotalN, 0, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.PostAnthesisNUptake, 0, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.TotalDMperGrain, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.TotalNperGrain, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.ProteinConcentration, 4);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.Number, 0);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.Starch, 4);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.PercentStarch, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.Namp, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.Ngli, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.Nglu, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.PercentGli, 3);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.PercentGlu, 3);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.GliadinsToGluteins, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.HarvestIndexDM, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.HarvestIndexN, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.IncomeWater, 1, Run.GwaterToMMwater);
            else line.Add("?");

            if (maturityUniverse != null) line.Add((maturityUniverse.Soil_.CalculateTotalN() + maturityUniverse.Soil_.SoilDeepLayer_.LostNitrogen + maturityUniverse.Crop_.TotalN), 0, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.SoilDeepLayer_.LostNitrogen, 2, 10);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Soil_.SoilDeepLayer_.LostWater, 1, Run.GwaterToMMwater);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.NuseEfficiency, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.NutilisationEfficiency, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.NuptakeEfficiency, 2);
            else line.Add("?");

            if (maturityUniverse != null) line.Add(maturityUniverse.Crop_.Grain_.WaterUseEfficiency, 2);
            else line.Add("?");
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
                .Add(deltaHeader)
                .Add(FileContainer.ManagementID)
                .Add(FileContainer.ParameterID)
                .Add(FileContainer.RunOptionID)
                .Add(FileContainer.SiteID)
                .Add(FileContainer.SoilID)
                .Add(FileContainer.VarietyID)

                .AddNull()

                .Add("Sowing year")
                .Add(GrowthStage.ZC_00_Sowing.Title())
                .Add(GrowthStage.ZC_10_Emergence.Title())
                .Add(GrowthStage.BeginningStemExtension.Title())
                .Add(GrowthStage.ZC_65_Anthesis.Title())
                .Add(GrowthStage.ZC_75_EndCellDivision.Title())
                .Add(GrowthStage.ZC_91_EndGrainFilling.Title())
                .Add(GrowthStage.ZC_92_Maturity.Title())

                .AddNull()


                .Add("Final leaf number")
                .Add("LAI at anthesis")
                .Add("GAI at anthesis")
                .Add("Crop DM at anthesis")
                .Add("Crop DM at maturity")
                .Add("Grain DM at maturity")
                .Add("Crop N at anthesis")
                .Add("Crop N at maturity")
                .Add("Grain N at maturity")
                .Add("Post-anthesis crop N uptake")
                .Add("Single grain DM at maturity")
                .Add("Single grain N at maturity")
                .Add("Grain protein concentration at maturity")
                .Add("Grain number")
                .Add("% Starch at maturity")
                .Add("Albumins-globulins at maturity")
                .Add("Amphiphils at maturity")
                .Add("Gliadins at maturity")
                .Add("Glutenins at maturity")
                .Add("% Gliadins at maturity")
                .Add("% Gluteins at maturity")
                .Add("Gliadins-to-gluteins ratio")
                .Add("DM harvest index")
                .Add("N harvest index")
                .Add("rainfall + irrigation")
                .Add("Total available soil N")
                .Add("N leaching")
                .Add("Water drainage")
                .Add("N use efficiency")
                .Add("N utilisation efficiency")
                .Add("N upake efficiency")
                .Add("Water use efficiency");

            page.NewLine()
                .AddNull(deltaHeader.Length)
                .AddNull(7)

                .Add("yyyy")
                .Add("DOY")
                .Add("DOY")
                .Add("DOY")
                .Add("DOY")
                .Add("DOY")
                .Add("DOY")
                .Add("DOY")

                .AddNull()

                .Add("leaf")
                .Add("m2/m2")
                .Add("m2/m2")
                .Add("kgDM/ha")
                .Add("kgDM/ha")
                .Add("kgDM/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("mgDM/grain")
                .Add("mgN/grain")
                .Add("% of grain DM")
                .Add("grain/m2")
                .Add("% of total grain DM")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("mgN/grain")
                .Add("% of total grain N")
                .Add("% of total grain N")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("dimensionless")
                .Add("mm")
                .Add("kgN/ha")
                .Add("kgN/ha")
                .Add("mm")
                .Add("kgDM/kgN")
                .Add("kgDM/kgN")
                .Add("kgN/kgN")
                .Add("kgDM/ha/mm");
        }

        internal override LineData SensitivityRunLine(Run runOld, double[] deltaValue)
        {
            var line = new LineData();

            line.Add(deltaValue)
                .Add(runOld.ManagementDef != null ? runOld.ManagementDef.Name : "?")
                .Add(runOld.ParameterDef != null ? runOld.ParameterDef.Name : "?")
                .Add(runOld.RunOptionDef != null ? runOld.RunOptionDef.Name : "?")
                .Add(runOld.SiteDef != null ? runOld.SiteDef.Name : "?")
                .Add(runOld.SoilDef != null ? runOld.SoilDef.Name : "?")
                .Add(runOld.VarietyDef != null ? runOld.VarietyDef.Name : "?")
                .AddNull()
                .Add(runOld.ManagementDef != null ? runOld.ManagementDef.SowingDate.Year.ToString() : "?");

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