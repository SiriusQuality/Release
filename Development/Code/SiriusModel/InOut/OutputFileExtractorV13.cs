using System;
using SiriusModel.InOut.OutputWriter;
using SiriusModel.Model;
using SiriusModel.Model.Phenology;
using SiriusModel.Model.CropModel;
using SiriusModel.Model.Observation;
using SiriusModel.Model.SoilModel;
using SiriusQualityPhenology;

namespace SiriusModel.InOut
{
    internal class OutputFileExtractorV13 : OutputFileExtractor
    {
        internal override PageData NormalRun(Run runOld)
        {
            var page = new PageData();

            page.NewLine().Add("Sirius-v1.5 outputFile");
            page.NewLine();
            page.NewLine().AddDateTime(DateTime.Now).AddNull().Add("DateTime, Time of simulation").AddNull().Add("Run Options");
            page.NewLine();

            PrintSummary(page, runOld, "?");
            page.NewLine();
            page.NewLine();
            PrintDaily(page, runOld);

            PrintStress(page, runOld);

            page.NewLine();
            return page;
        }

        internal override PageData MultiRunHeader()
        {
            var line = new LineData();
            line.Add(FileContainer.ManagementID)
                .Add(FileContainer.NonVarietyID)
                .Add(FileContainer.RunOptionID)
                .Add(FileContainer.SiteID)
                .Add(FileContainer.SoilID)
                .Add(FileContainer.VarietyID)
                .Add("Sowing")
                .Add("Emergence")
                .Add("Anthesis")
                .Add("End cell division")
                .Add("End grain fill")
                .Add("Maturity")
                .Add("Final leaf number")
                .Add("Anthesis GAI")
                .Add("Crop DM at anthesis, MgDM/ha")
                .Add("Total yield, MgDM/ha")
                .Add("Grain yield, MgDM/ha")
                .Add("CropN at anthesis, kgN/ha")
                .Add("CropN at maturity, kgN/ha")
                .Add("GrainN, kgN/ha")
                .Add("DM harvest index")
                .Add("N harvest index")
                .Add("AvSoilN, kgN/ha")
                .Add("N leaching, kgN/ha")
                .Add("NUE, kgDM/kgN")
                .Add("GrainDM, mgDM/grain")
                .Add("GrainN, mgN/grain")
                .Add("GPC, %")
                .Add("Grain number, grain/m2")
                .Add("AlbGlo, mgN/grain")
                .Add("Amp, mgN/grain")
                .Add("Gli, mgN/grain")
                .Add("Glu, mgN/grain")
                .Add("%Gli, % total N")
                .Add("%Glu, % total N")
                .Add("Gli/Glu");
            var page = new PageData();

            page.Add(line);
            return page;
        }

        internal override LineData MultiRunLine(Run runOld)
        {
            var line = new LineData();

            line.Add(runOld.ManagementDef.Name)
                .Add(runOld.ParameterDef.Name)
                .Add(runOld.RunOptionDef.Name)
                .Add(runOld.SiteDef.Name)
                .Add(runOld.SoilDef.Name)
                .Add(runOld.VarietyDef.Name);

            var lastUniverse = GetLastUniverse(runOld);
            var anthesisUniverse = GetUniverse(runOld, GrowthStage.ZC_65_Anthesis);
            var maturityUniverse = GetUniverse(runOld, GrowthStage.ZC_92_Maturity);

            if (lastUniverse != null)
            {
                DateTime? sowingDate = lastUniverse.Crop_.getDateOfStage(GrowthStage.ZC_00_Sowing);
                DateTime? emergenceDate = lastUniverse.Crop_.getDateOfStage(GrowthStage.ZC_10_Emergence);
                DateTime? anthesisDate = lastUniverse.Crop_.getDateOfStage(GrowthStage.ZC_65_Anthesis);
                DateTime? endCellDivisionDate = lastUniverse.Crop_.getDateOfStage(GrowthStage.ZC_75_EndCellDivision);
                DateTime? endGrainFillDate = lastUniverse.Crop_.getDateOfStage(GrowthStage.ZC_91_EndGrainFilling);
                DateTime? maturityDate = lastUniverse.Crop_.getDateOfStage(GrowthStage.ZC_92_Maturity);
                line
                    .Add((sowingDate.HasValue) ? sowingDate.Value.ToString("yyyy-MM-dd") : "?")
                    .Add((emergenceDate.HasValue) ? emergenceDate.Value.ToString("yyyy-MM-dd") : "?")
                    .Add((anthesisDate.HasValue) ? anthesisDate.Value.ToString("yyyy-MM-dd") : "?")
                    .Add((endCellDivisionDate.HasValue) ? endCellDivisionDate.Value.ToString("yyyy-MM-dd") : "?")
                    .Add((endGrainFillDate.HasValue) ? endGrainFillDate.Value.ToString("yyyy-MM-dd") : "?")
                    .Add((maturityDate.HasValue) ? maturityDate.Value.ToString("yyyy-MM-dd") : "?")
                    .Add(lastUniverse.Crop_.LeafNumber, 2)
                    .Add(anthesisUniverse.Crop_.GAI, 2)
                    .Add(anthesisUniverse.Crop_.OutputTotalDM, 0, 10)
                    .Add(maturityUniverse.Crop_.OutputTotalDM, 0, 10)
                    .Add(maturityUniverse.Crop_.GrainTotalDM, 0, 10)
                    .Add(anthesisUniverse.Crop_.OutputTotalN, 0, 10)
                    .Add(maturityUniverse.Crop_.OutputTotalN, 0, 10)
                    .Add(maturityUniverse.Crop_.GrainTotalN, 0, 10)
                    .Add(maturityUniverse.Crop_.HarvestIndexDM, 2)
                    .Add(maturityUniverse.Crop_.HarvestIndexN, 2)
                    .Add(maturityUniverse.Soil_.CalculateAvailableExcessN(), 2, 10) // ???
                    .Add(maturityUniverse.Soil_.SoilDeepLayer_.LostNitrogen, 2, 10)
                    .Add(maturityUniverse.NuseEfficiency, 2)
                    .Add(maturityUniverse.Crop_.TotalDMperGrain, 2)
                    .Add(maturityUniverse.Crop_.TotalNperGrain, 2)
                    .Add(maturityUniverse.Crop_.ProteinConcentration, 2)
                    .Add(maturityUniverse.Crop_.GrainNumber, 0)
                    .Add(maturityUniverse.Crop_.NalbGlo, 2)
                    .Add(maturityUniverse.Crop_.Namp, 2)
                    .Add(maturityUniverse.Crop_.Ngli, 2)
                    .Add(maturityUniverse.Crop_.Nglu, 2)
                    .Add(maturityUniverse.Crop_.PercentGli, 3)
                    .Add(maturityUniverse.Crop_.PercentGlu, 3)
                    .Add(maturityUniverse.Crop_.GliadinsToGluteins, 2);
            }
            return line;
        }

        internal override PageData SensitivityRunHeader(string[] deltaHeader)
        {
            throw new NotImplementedException("v1.3 output files doesn't support sensitivty results.");
        }

        ///<Behnam>
        internal override LineData SensitivityRunLine(Run runOld, string[] deltaHeader, double[] deltaValue)
        ///</Behnam>
        {
            throw new NotImplementedException("v1.3 output files doesn't support sensitivty results.");
        }


        private static void PrintSummary(PageData page, Run runOld, string outputFile)
        {
            page.NewLine().Add("").Add("").Add("").Add("").Add("phenology").Add("Pheno V99");
            page.NewLine().Add(outputFile).Add("").Add("Output File").Add("").Add("Vernalization").Add("Vern V99");
            page.NewLine().Add(runOld.SiteDef.Name).Add("").Add("Site File").Add("").Add("Final Leaf Number").Add("FLN V99");
            page.NewLine().Add(runOld.VarietyDef.Name).Add("").Add("Variety").Add("").Add("GAI").Add("Leaf_layers V1");
            page.NewLine().Add(runOld.SoilDef.Name).Add("").Add("Soil").Add("").Add("Leaf Number").Add("LN V99");
            page.NewLine().Add("").Add("").Add("").Add("").Add("Roots").Add("Root V99");
            PrintSummaryDate(page, runOld);
            PrintSummaryCrop(page, runOld);

        }

        private static void PrintSummaryDate(PageData page, Run runOld)
        {
            var lastUniverse = GetLastUniverse(runOld);

            if (lastUniverse != null)
            {
                page.NewLine().AddDateDOY(lastUniverse.Crop_.getDateOfStage(GrowthStage.ZC_00_Sowing)).Add("Sowing").Add("").Add("Crop N Uptake").Add("CNUT V99");
                page.NewLine().AddDateDOY(lastUniverse.Crop_.getDateOfStage(GrowthStage.ZC_10_Emergence)).Add("Emergence").Add("").Add("Dry Matter").Add("DryM V99");
                page.NewLine().AddDateDOY(lastUniverse.Crop_.getDateOfStage(GrowthStage.ZC_65_Anthesis)).Add("Anthesis").Add("").Add("Grain").Add("GrainQuality V1");
                page.NewLine().AddDateDOY(lastUniverse.Crop_.getDateOfStage(GrowthStage.ZC_75_EndCellDivision)).Add("End cell division");
                page.NewLine().AddDateDOY(lastUniverse.Crop_.getDateOfStage(GrowthStage.ZC_91_EndGrainFilling)).Add("End Grain Fill").Add("").Add("Calculate yield and protein percentage at 14% moisture content").Add("False");
                page.NewLine().AddDateDOY(lastUniverse.Crop_.getDateOfStage(GrowthStage.ZC_92_Maturity)).Add("Maturity").Add("").Add("Grain Number").Add("Use calculated value (within model)");
                page.NewLine().AddDateDOY(lastUniverse.Crop_.getDateOfStage(GrowthStage.FloralInitiation)).Add("Floral initiation");
            }
            else
            {
                page.NewLine().AddDateDOY(null).Add("Sowing").Add("").Add("Crop N Uptake").Add("CNUT V99");
                page.NewLine().AddDateDOY(null).Add("Emergence").Add("").Add("Dry Matter").Add("DryM V99");
                page.NewLine().AddDateDOY(null).Add("Anthesis").Add("").Add("Grain").Add("GrainQuality V1");
                page.NewLine().AddDateDOY(null).Add("End cell division");
                page.NewLine().AddDateDOY(null).Add("End Grain Fill").Add("").Add("Calculate yield and protein percentage at 14% moisture content").Add("False");
                page.NewLine().AddDateDOY(null).Add("Maturity").Add("").Add("Grain Number").Add("Use calculated value (within model)");
                page.NewLine().AddDateDOY(null).Add("Floral initiation");
            }
        }

        private static void PrintSummaryCrop(PageData page, Run runOld)
        {
            var lastUniverse = GetLastUniverse(runOld);
            if (lastUniverse != null)
            {
                
                Universe anthesisUniverse; 
                Universe maturityUniverse;
                if (lastUniverse.Crop_.getDateOfStage(GrowthStage.ZC_65_Anthesis) != null) {anthesisUniverse= runOld.GetUniverse(lastUniverse.Crop_.getDateOfStage(GrowthStage.ZC_65_Anthesis).Value);}
                else {anthesisUniverse =null;}

                if (lastUniverse.Crop_.getDateOfStage(GrowthStage.ZC_92_Maturity) != null) {maturityUniverse= runOld.GetUniverse(lastUniverse.Crop_.getDateOfStage(GrowthStage.ZC_92_Maturity).Value);}
                else {maturityUniverse =null;}

                var lastLeafNumber = lastUniverse.Crop_.LeafNumber;
                var anthesisCropDM = (anthesisUniverse != null) ? anthesisUniverse.Crop_.OutputTotalDM * 10 : double.MaxValue;
                var maturityCropDM = (maturityUniverse != null) ? maturityUniverse.Crop_.OutputTotalDM * 10 : double.MaxValue;
                var maturityGrainDM = (maturityUniverse != null) ? maturityUniverse.Crop_.GrainTotalDM * 10 : double.MaxValue;
                var anthesisCropN = (anthesisUniverse != null) ? anthesisUniverse.Crop_.OutputTotalN * 10 : double.MaxValue;
                var maturityCropN = (maturityUniverse != null) ? maturityUniverse.Crop_.OutputTotalN * 10 : double.MaxValue;

                var maturityGrainDMperGrain = (maturityUniverse != null) ? (maturityUniverse.Crop_.GrainTotalDM / maturityUniverse.Crop_.GrainNumber * 1000.0) : double.MaxValue;
                var maturityGrainNperDM = (maturityUniverse != null) ? maturityUniverse.Crop_.Ncp : double.MaxValue;
                var maturityGrainNumber = (maturityUniverse != null) ? maturityUniverse.Crop_.GrainNumber : double.MaxValue;

                var accIrrigation = (maturityUniverse != null) ? maturityUniverse.Soil_.IncomeWater : double.MaxValue;

                var nalbGlo = (maturityUniverse != null) ? maturityUniverse.Crop_.NalbGlo : double.MaxValue;
                var namp = (maturityUniverse != null) ? maturityUniverse.Crop_.Namp : double.MaxValue;
                var ngli = (maturityUniverse != null) ? maturityUniverse.Crop_.Ngli : double.MaxValue;
                var nglu = (maturityUniverse != null) ? maturityUniverse.Crop_.Nglu : double.MaxValue;

                page.NewLine().Add(lastLeafNumber, 2).AddNull().Add("Final Leaf Number").AddNull().Add("GAI to LAI ratio:").AddNull().Add("1.34");
                page.NewLine().AddNull().AddNull().AddNull().Add("Initialisation at anthesis date :").Add("False");
                page.NewLine().Add(anthesisCropDM, 0).AddNull().Add("Crop biomass at anthesis, kg/ha");
                page.NewLine().Add(maturityCropDM, 0).AddNull().Add("Crop biomass at maturity, kg/ha");
                page.NewLine().Add(maturityGrainDM, 0).AddNull().Add("Grain biomass at maturity, kg/ha");
                page.NewLine().Add(anthesisCropN, 0).AddNull().Add("Crop N at anthesis, kg/ha");
                page.NewLine().Add(maturityCropN, 0).AddNull().Add("Crop N at maturity, kg/ha");
                page.NewLine();
                page.NewLine().Add(maturityGrainDMperGrain, 2).AddNull().Add("Grain biomass at maturity, mg");
                page.NewLine().Add(maturityGrainNperDM, 2).AddNull().Add("Grain N at maturity, %");
                page.NewLine().Add(maturityGrainNumber, 0).AddNull().Add("Grain number, grain/m2");
                page.NewLine();
                page.NewLine().Add((accIrrigation / 1000.0), 2).AddNull().Add("Irrigation added, mm");
                page.NewLine();
                page.NewLine().Add(nalbGlo, 2).AddNull().Add("Albumins-globulins at maturity, mg/grain");
                page.NewLine().Add(namp, 2).AddNull().Add("Amphiphils at maturity, mg/grain");
                page.NewLine().Add(ngli, 2).AddNull().Add("Gliadins at maturity, mg/grain");
                page.NewLine().Add(nglu, 2).AddNull().Add("Glutenins at maturity, mg/grain");

            }
            else
            {
                page.NewLine().Add("?").AddNull().Add("Final Leaf Number").AddNull().Add("GAI to LAI ratio:").AddNull().Add("1.34");
                page.NewLine().AddNull().AddNull().AddNull().Add("Initialisation at anthesis date :").Add("False");
                page.NewLine().Add("?").AddNull().Add("Crop biomass at anthesis, kg/ha");
                page.NewLine().Add("?").AddNull().Add("Crop biomass at maturity, kg/ha");
                page.NewLine().Add("?").AddNull().Add("Grain biomass at maturity, kg/ha");
                page.NewLine().Add("?").AddNull().Add("Crop N at anthesis, kg/ha");
                page.NewLine().Add("?").AddNull().Add("Crop N at maturity, kg/ha");
                page.NewLine();
                page.NewLine().Add("?").AddNull().Add("Grain biomass at maturity, mg");
                page.NewLine().Add("?").AddNull().Add("Grain N at maturity, %");
                page.NewLine().Add("?").AddNull().Add("Grain number, grain/m2");
                page.NewLine();
                page.NewLine().Add("?").AddNull().Add("Irrigation added, mm");
                page.NewLine();
                page.NewLine().Add("?").AddNull().Add("Albumins-globulins at maturity, mg/grain");
                page.NewLine().Add("?").AddNull().Add("Amphiphils at maturity, mg/grain");
                page.NewLine().Add("?").AddNull().Add("Gliadins at maturity, mg/grain");
                page.NewLine().Add("?").AddNull().Add("Glutenins at maturity, mg/grain");
            }
        }

        private static void PrintDaily(PageData page, Run runOld)
        {
            PrintDailyHeader1(page, runOld);
            PrintDailyRows1(page, runOld);

            var nbRun = runOld.SavedUniverses.Count;

            for (var i = 0; i < 368 - nbRun; ++i)
            {
                page.NewLine();
            }

            PrintDailyHeader2(page, runOld);
            PrintDailyRows2(page, runOld);
        }

        private static void PrintDailyHeader1(PageData page, Run runOld)
        {
            page.Add(PrintDailyHeaderCrop11().Add(PrintDailyHeaderLeaf11(runOld)));
            page.Add(PrintDailyHeaderCrop12().Add(PrintDailyHeaderLeaf12(runOld)));
        }

        private static LineData PrintDailyHeaderCrop11()
        {
            return new LineData().Add("DateTime").Add("Julian").Add("Cumulative").Add("Root zone WD").Add("Crop").Add("Grain").Add("Root")
                .Add("LAI").Add("Haun").Add("Crop").Add("Grain").Add("Available").Add("Total").Add("Root zone AW").Add("Soil stress")
                .Add("Albumins-Globulins").Add("Amphiphils").Add("Gliadins").Add("Glutenins").Add("Grain").Add("Starch").Add("Grain")
                .Add("StemDM").Add("StemNc").Add("LaminaeDM").Add("LaminaeN").Add("SLN").Add("SLW").Add("ExposedSheathDM").Add("ExposedSheathN").Add("LeafDM")
                .Add("LeafN").Add("GAI").Add("TotalTT").Add("VPDair").Add("VPDair-canopy").Add("HSlope").Add("Ear DM").Add("Stem Alone DM")
                .Add("Sheath Alone DM").Add("Shoot Dead DM").Add("Shoot Lost DM");
        }

        private static LineData PrintDailyHeaderCrop12()
        {
            return new LineData().Add("DD/MM/YYYY").Add("").Add("mm").Add("mm").Add("kgDM/ha").Add("kgDM/ha").Add("m")
                .Add("dimensionless").Add("leaf#").Add("kgN/ha").Add("kgN/ha").Add("kgN/ha").Add("kgN/ha").Add("mm")
                .Add("dimensionless").Add("mgN/grain").Add("mgN/grain").Add("mgN/grain").Add("mgN/grain")
                .Add("mgN/grain").Add("mg/grain").Add("mg/grain").Add("kg/ha").Add("kg/ha").Add("kg/ha").Add("kg/ha")
                .Add("gN/m^2").Add("gDM/m^2").Add("kg/ha").Add("kgN/ha").Add("kg/ha").Add("kgN/ha").Add("").Add("°Cdays").Add("hPa").Add("hPa").Add("hPa/°C").Add("kg/ha")
                .Add("kg/ha").Add("kg/ha").Add("kg/ha").Add("kg/ha");
        }

        private static LineData PrintDailyHeaderLeaf11(Run runOld)
        {
            const int space = 37;

            var lastUniverse = GetLastUniverse(runOld);
            var nbLeaf = (int)((lastUniverse != null) ? lastUniverse.Crop_.LeafNumber : 0);
            var spaceNotLeaf = 37 - nbLeaf + 1;

            return new LineData().Add("Leaf laminae layers area (m^2 (leaf layer)/m^2)").AddNull(space).AddNull()
                .Add("Interlayer distances (cm)").AddNull(nbLeaf).Add("Stem height (cm)").AddNull(spaceNotLeaf)
                .Add("Leaf laminae DM (kg/ha)").AddNull(space).AddNull()
                .Add("Leaf laminae N (kg/ha)").AddNull(space).AddNull()
                .Add("laminae SLN (gN/m^2)").AddNull(space).AddNull()
                .Add("laminae SLW (gDM/m^2)").AddNull(space).AddNull()
                .Add("laminae TTsinceSen (°Cd)").AddNull(space).AddNull()
                .Add("laminae Dead DM (kg/ha)").AddNull(space).AddNull()
                .Add("laminae Lost DM (kg/ha)");
        }

        private static LineData PrintDailyHeaderLeaf12(Run runOld)
        {
            var lastUniverse = GetLastUniverse(runOld);
            var finalLeafNum = (int)((lastUniverse != null) ? lastUniverse.Crop_.LeafNumber : 0);
            var space = 39 - finalLeafNum;

            var header = new LineData();
            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                header.Add(i + 1);
            }
            header.AddNull(space);
            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                header.Add(i + 1);
            }
            header.AddNull(space + 1);
            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                header.Add(i + 1);
            }
            header.AddNull(space);
            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                header.Add(i + 1);
            }
            header.AddNull(space);
            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                header.Add(i + 1);
            }
            header.AddNull(space);
            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                header.Add(i + 1);
            }
            header.AddNull(space);
            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                header.Add(i + 1);
            }
            header.AddNull(space);
            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                header.Add(i + 1);
            }
            return header;
        }

        private static void PrintDailyHeader2(PageData page, Run runOld)
        {
            page.Add(PrintDailyHeaderCrop21().Add(PrintDailyHeaderLeaf21()));
            page.Add(PrintDailyHeaderCrop22().Add(PrintDailyHeaderLeaf22(runOld)));
        }

        private static LineData PrintDailyHeaderCrop21()
        {
            return new LineData().Add("DateTime").Add("Julian").AddNull(32);
        }

        private static LineData PrintDailyHeaderCrop22()
        {
            return new LineData().Add("DD/MM/YYYY").AddNull().AddNull(32);
        }

        private static LineData PrintDailyHeaderLeaf21()
        {
            return new LineData().Add("Leaf sheath layers area (m^2 (leaf layer)/m^2)")
                .AddNull(38 + 38 + 2)
                .Add("Leaf sheath DM (kg/ha)").AddNull(38)
                .Add("Leaf sheath N (kg/ha)").AddNull(38)
                .Add("Sheath SLN (gN/m^2)").AddNull(38)
                .Add("Sheath SLW (gDM/m^2)").AddNull(38)
                .Add("Sheath TTsinceSen (°Cd)").AddNull(38)
                .Add("Sheath Dead DM (kg/ha)").AddNull(38)
                .Add("Sheath Lost DM (kg/ha)");
        }

        private static LineData PrintDailyHeaderLeaf22(Run runOld)
        {
            var lastUniverse = GetLastUniverse(runOld);
            var finalLeafNum = (int)((lastUniverse != null) ? lastUniverse.Crop_.LeafNumber : 0);
            var space = 39 - finalLeafNum;

            var header = new LineData();
            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                header.Add(i + 1);
            }
            header.AddNull(space);
            header.AddNull(40);
            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                header.Add(i + 1);
            }
            header.AddNull(space);
            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                header.Add(i + 1);
            }
            header.AddNull(space);
            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                header.Add(i + 1);
            }
            header.AddNull(space);
            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                header.Add(i+1);
            }
            header.AddNull(space);
            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                header.Add(i + 1);
            }
            header.AddNull(space);
            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                header.Add(i + 1);
            }
            return header;
        }

        private static void PrintDailyRows1(PageData page, Run runOld)
        {
            var allUniverse = runOld.SavedUniverses;
            var lastUniverse = GetLastUniverse(runOld);
            var finalLeafNumber = (int)lastUniverse.Crop_.LeafNumber;
            if (allUniverse != null && allUniverse.Count > 1)
            {
                var nbUniverse = allUniverse.Count;

                ///<Behnam>

                var sowDate = runOld.SavedUniverses[nbUniverse - 1].Crop_.getDateOfStage(GrowthStage.ZC_00_Sowing);
                var sowDay = (int)(sowDate.Value - runOld.SavedUniverses[0].CurrentDate).TotalDays;

                for (var i = 1; i < nbUniverse; ++i)
                {
                    var universe = allUniverse[i];
                    if (i >= sowDay) page.Add(PrintDailyCropRow1(universe).Add(PrintDailyLeafRow1(universe, finalLeafNumber)));

                    ///</Behnam>
                }
            }
        }

        private static LineData PrintDailyCropRow1(Universe universe)
        {
            return new LineData().AddDateDOY(universe.CurrentDate)
                .Add((universe.Soil_.AccumulatedActEvapoTranspiration / 1000.0), 2)
                .Add((universe.Soil_.CalculateAccumulatedDeficit(universe.Crop_.RootLength + Soil.ExtendedRootZone) / 1000.0), 2)
                .Add((universe.Crop_.OutputTotalDM * 10.0), 2)
                .Add((universe.Crop_.GrainTotalDM * 10.0), 2)
                .Add((universe.Crop_.RootLength), 2)
                .Add((universe.Crop_.LaminaeAI), 2)
                .Add((universe.Crop_.LeafNumber), 2)
                .Add((universe.Crop_.OutputTotalN * 10.0), 1)
                .Add((universe.Crop_.GrainTotalN * 10.0), 1)
                .Add((universe.Soil_.DailyAvNforRoots * 10.0), 1)
                .Add(((universe.Soil_.DailyAvNforRoots + universe.Soil_.DailyUnavNforRoots) * 10), 1)
                .Add((universe.Soil_.CalculateRootZoneAvailableExcessWater() / 1000.0), 1)
                .Add((universe.Soil_.TRSF), 2)
                .Add((universe.Crop_.NalbGlo), 4)
                .Add((universe.Crop_.Namp), 4)
                .Add((universe.Crop_.Ngli), 4)
                .Add((universe.Crop_.Nglu), 4)
                .Add((TotalNperGrain(universe)), 4)
                .Add((universe.Crop_.Starch), 2)
                .Add((TotalDMperGrain(universe)), 2)
                .Add(((universe.Crop_.StemTotalDM + universe.Crop_.OutputSheathDM) * 10.0), 2)
                .Add(((universe.Crop_.StemTotalN + universe.Crop_.OutputSheathN) * 10.0), 2)
                .Add((universe.Crop_.OutputLaminaeDM * 10.0), 2)
                .Add((universe.Crop_.OutputLaminaeN * 10.0), 2)
                .Add((universe.Crop_.LaminaeSLN), 2)
                .Add((universe.Crop_.LaminaeSLW), 2)
                .Add((universe.Crop_.OutputSheathDM * 10.0), 2)
                .Add((universe.Crop_.OutputSheathN * 10.0), 2)
                //.Add((universe.Crop_.Phytomers_.TotalDM * 10.0), 2)
                //.Add((universe.Crop_.Phytomers_.TotalN * 10.0), 2)
                .Add(((universe.Crop_.OutputLaminaeDM + universe.Crop_.OutputSheathDM) * 10.0), 2)
                .Add(((universe.Crop_.OutputLaminaeN + universe.Crop_.OutputSheathN) * 10.0), 2)
                .Add((universe.Crop_.GAI), 2)
                .Add((universe.thermalTimeWrapper_.getCumulTT(Delta.Air)), 1)
                .Add(universe.meteorologyWrapper_.VPDair,2)
                .Add(universe.meteorologyWrapper_.VPDairCanopy, 2)
                .Add(universe.meteorologyWrapper_.HSlope, 2)
                .Add(universe.Crop_.EarDW, 2, 10)
                .Add((universe.Crop_.StemTotalDM), 2, 10)
                .Add((universe.Crop_.OutputSheathDM), 2, 10)
                .Add((universe.Crop_.ShootDeadDM), 2, 10)
                .Add((universe.Crop_.ShootLostDM), 2, 10);
        }

        private static double TotalDMperGrain(Universe universe)
        {
            var grainNumber = universe.Crop_.GrainNumber;
            return (grainNumber > 0) ? universe.Crop_.GrainTotalDM / grainNumber * 1000.0 : 0;
        }

        private static double TotalNperGrain(Universe universe)
        {
            var grainNumber = universe.Crop_.GrainNumber;
            return (grainNumber > 0) ? universe.Crop_.GrainTotalN / universe.Crop_.GrainNumber * 1000.0 : 0;
        }

        private static LineData PrintDailyLeafRow1(Universe universe, int finalLeafNum)//OK
        {
            var line = new LineData();

            var nbLeaf = universe.Crop_.CreatedLeavesNumber;
            var space = 38 - finalLeafNum;

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                if (i < nbLeaf)
                {
                    line.Add(universe.Crop_.getLeafLaminaAreaIndexForLeafLayer(i), 6);                   
                }
                else line.Add((0.0), 6);
            }

            line.AddNull(space).AddNull();

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                if (i < nbLeaf)
                {
                    line.Add(universe.Crop_.getInterNodeLengthForLeafLayer(i), 6);
                }
                else line.Add((0.0), 6);
            }

            /*var earInterNode = (universe.Crop_.Phytomers_.EarPeduncle != null) ? universe.Crop_.Phytomers_.EarPeduncle.InterNode : null;
            if (earInterNode != null)
            {
                line.Add(earInterNode.Length, 6);
            }
            else line.Add((0.0), 6);*/
            line.Add(universe.Crop_.getEarPeduncleInterNodeLength(), 6);

            line.Add(universe.Crop_.SumInternodesLength, 6);

            line.AddNull(space);

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                if (i < nbLeaf)
                {
                    line.Add((universe.Crop_.getLeafLaminaTotalDMForLeafLayer(i) * 10.0), 6);
                }
                else line.Add((0.0), 6);
            }

            line.AddNull(space).AddNull();

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                if (i < nbLeaf)
                {
                    line.Add((universe.Crop_.getLeafLaminaTotalNForLeafLayer(i) * 10.0), 6);
                }
                else line.Add((0.0), 6);
            }

            line.AddNull(space).AddNull();

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                if (i < nbLeaf)
                {
                    line.Add(universe.Crop_.getLeafLaminaSpecificNForLeafLayer(i), 6);
                }
                else line.Add((0.0), 6);
            }

            line.AddNull(space + 1);

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                if (i < nbLeaf)
                {
                    line.Add(universe.Crop_.getLeafLaminaSpecificWeightForLeafLayer(i), 6);
                }
                else line.Add((0.0), 6);
            }
            line.AddNull(space).AddNull();

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                if (i < nbLeaf)
                {
                    line.Add(universe.Crop_.getLeafLaminaTTsinceSenForLeafLayer(i), 6);
                }
                else line.Add((0.0), 6);
            }

            line.AddNull(space + 1);

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                if (i < nbLeaf)
                {
                    line.Add(universe.Crop_.getLeafLaminaDeadDMForLeafLayer(i)*10.0, 6);
                }
                else line.Add((0.0), 6);
            }

            line.AddNull(space + 1);

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                if (i < nbLeaf)
                {
                    line.Add(universe.Crop_.getLeafLaminaLostDMForLeafLayer(i)*10.0, 6);
                }
                else line.Add((0.0), 6);
            }

            return line;
        }

        private static void PrintDailyRows2(PageData page, Run runOld)
        {
            var allUniverse = runOld.SavedUniverses;
            var lastUniverse = GetLastUniverse(runOld);
            var finalLeafNumber = lastUniverse.Crop_.CreatedLeavesNumber;
            if (allUniverse != null && allUniverse.Count > 0)
            {
                var nbUniverse = allUniverse.Count;
                for (var i = 1; i < nbUniverse; ++i)
                {
                    var universe = allUniverse[i];
                    page.Add(PrintDailyCropRow2(universe).Add(PrintDailyLeafRow2(universe, finalLeafNumber)));

                }
            }
        }

        private static LineData PrintDailyCropRow2(Universe universe)
        {
            return new LineData().AddDateDOY(universe.CurrentDate).AddNull(32);
        }

        private static LineData PrintDailyLeafRow2(Universe universe, int finalLeafNum)//OK
        {
            var line = new LineData();
            var nbLeaf = universe.Crop_.CreatedLeavesNumber;
            var space = 38 - finalLeafNum;

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                line.Add(universe.Crop_.getExposedSheathAreaIndexForLeafLayer(i), 6);
            }

            line.AddNull(41 + space);

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                line.Add((universe.Crop_.getExposedSheathTotalDMForLeafLayer(i) * 10.0), 6);
            }

            line.AddNull(space + 1);

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                line.Add((universe.Crop_.getExposedSheathTotalNForLeafLayer(i) * 10.0), 6);
            }

            line.AddNull(space + 1);

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                line.Add(universe.Crop_.getExposedSheathSpecificNForLeafLayer(i), 6);
            }

            line.AddNull(space + 1);

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                line.Add(universe.Crop_.getExposedSheathSpecificWeightForLeafLayer(i), 6);
            }

            line.AddNull(space + 1);

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                line.Add(universe.Crop_.getExposedSheathTTsinceSenForLeafLayer(i), 6);
            }

            line.AddNull(space + 1);

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                line.Add(universe.Crop_.getExposedSheathDeadDMForLeafLayer(i)*10.0, 6);
            }

            line.AddNull(space + 1);

            for (var i = finalLeafNum - 1; i >= 0; --i)
            {
                line.Add(universe.Crop_.getExposedSheathLostDMForLeafLayer(i)*10.0, 6);
            }

            return line;


        }


        private static void PrintStress(PageData page, Run runOld)
        {
            var nbRun = runOld.SavedUniverses.Count;

            for (var i = 0; i < 368 - nbRun; ++i)
            {
                page.NewLine();
            }

            page.NewLine()
                .Add("Date").Add("Julian").Add("FPAW").Add("DBF").Add("DEBF").Add("DEF").Add("DSF").Add("DTF");
            page.NewLine()
                .Add("dd/mm/yyyy").Add("").Add("dimensionless").Add("dimensionless").Add("dimensionless").Add("dimensionless").Add("dimensionless").Add("dimensionless");
            for (var i = 0; i < nbRun; ++i)
            {
                var universe = runOld.SavedUniverses[i];
                page.NewLine()
                    .AddDateDOY(universe.CurrentDate)
                    .Add(universe.Soil_.FPAW)
                    .Add(universe.Soil_.DBF)
                    .Add(universe.Soil_.DEBF)
                    .Add(universe.Crop_.DEF)
                    .Add(universe.Crop_.DSF)
                    .Add(universe.Soil_.DTF);
            }
        }
    }
}
