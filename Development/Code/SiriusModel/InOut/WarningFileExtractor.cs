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
    class WarningFileExtractor
    {

        //Single Run
        public PageData ExtractWarningsNormalRun(Run run)
        {
            
            var page = new PageData();

            PrintVersionDateWarnings(page);
            PrintFilesNormalRunWarnings(page);
            if(run != null) PrintNormalRunWarningDate(page, run);
            if(run != null) PrintNormalRunWarnings(page, run);

            page.NewLine();
            return page;
        }

        private void PrintFilesNormalRunWarnings(PageData page)
        {
            page.NewLine().Add("Project").Add(ProjectFile.This.Path);
            page.NewLine().Add(FileContainer.ManagementID).Add(ProjectFile.This.FileContainer[FileContainer.ManagementID].RelativeFileName);
            page.NewLine().Add(FileContainer.NonVarietyID).Add(ProjectFile.This.FileContainer[FileContainer.NonVarietyID].RelativeFileName);
            page.NewLine().Add(FileContainer.RunOptionID).Add(ProjectFile.This.FileContainer[FileContainer.RunOptionID].RelativeFileName);
            page.NewLine().Add(FileContainer.SiteID).Add(ProjectFile.This.FileContainer[FileContainer.SiteID].RelativeFileName);
            page.NewLine().Add(FileContainer.SoilID).Add(ProjectFile.This.FileContainer[FileContainer.SoilID].RelativeFileName);
            page.NewLine().Add(FileContainer.VarietyID).Add(ProjectFile.This.FileContainer[FileContainer.VarietyID].RelativeFileName);
        }

        private void PrintVersionDateWarnings(PageData page)
        {
            page.NewLine()
                .Add("SiriusQuality2.5 output file")
                .AddDateTime(DateTime.Now)
                .AddNull()
                .Add("build:" + ProjectFile.Build);
        }


        private Universe GetLastUniverse(Run runOld)
        {
            var lastUniverse = (runOld.SavedUniverses.Count > 0) ? runOld.SavedUniverses[runOld.SavedUniverses.Count - 1] : null;
            return lastUniverse;
        }

        private void PrintNormalRunWarningDate(PageData page, Run runOld)
        {
            page.NewLine();
            page.NewLine().Add("Date").Add("DOY").Add("Growth stage");
            PrintNormalRunDate(page, runOld, GrowthStage.ZC_00_Sowing);
            PrintNormalRunDate(page, runOld, GrowthStage.ZC_92_Maturity);
        }

        private void PrintNormalRunDate(PageData page, Run runOld, GrowthStage moment)
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


                ///</Behnam>

                if (momentDate != null)
                {
                    page.NewLine().AddDateDOY(momentDate).Add(Phase.growthStageAsString(moment));
                }
                else page.NewLine().AddDateDOY(null).Add(Phase.growthStageAsString(moment));
            }
            else page.NewLine().AddDateDOY(null).Add(Phase.growthStageAsString(moment));
        }


        private void PrintNormalRunWarnings(PageData page, Run runOld)
        {

            string Tag=CutGrainWarningExtractor(runOld);

            if (Tag != "") page.NewLine().Add("Warning on grain filling cut").Add(Tag);
            else page.NewLine().Add("No Warnings");

            //Tag=PhenologyWarningExtractor(runOld);

            //if (Tag != "") page.NewLine().Add("Warning on phenology phase transitions").Add(Tag);
            //else page.NewLine().Add("No Warnings");
            
        }

        private string CutGrainWarningExtractor(Run runOld)
        {

            var nbDay = runOld.SavedUniverses.Count;
            string Tag = "No Warnings";

            for (var i = 1; i < nbDay; ++i)
            {
                var universe = runOld.SavedUniverses[i];

                if (universe.Crop_.TagGrainCNWarnOut == "A convergence problem occured at least 1 time, cut on C/N supply was not/fully applied")
                    Tag = "A convergence problem occured at least 1 time, cut on C/N supply was not/fully applied";

            }
            return Tag;
        }

       /* private string PhenologyWarningExtractor(Run runOld)
        {

            var nbDay = runOld.SavedUniverses.Count;
            string Tag = "No Warnings";

            for (var i = 1; i < nbDay; ++i)
            {
                var universe = runOld.SavedUniverses[i];

                if (universe.Crop_.TagPhenoWarnOut == "Suspicious phenology phase transition occured")
                    Tag = "A suspiscious phenology phase transition occured at least 1 time";

            }
            return Tag;
        }*/


        //Batch Run

//Debug
     /*   public PageData ExtractWarningsMultiRun(Run run)
        {

            var page = new PageData();


            page.NewLine();


            page.NewLine();
            return page;
        }*/



        public PageData PrintMultiRunHeader()
        {

            var page = new PageData();

            // Print summary file main header
            PrintVersionDateWarnings(page);

            page.NewLine();


            page.NewLine()
                .Add(FileContainer.ManagementID)
                .Add(FileContainer.NonVarietyID)
                .Add(FileContainer.RunOptionID)
                .Add(FileContainer.SiteID)
                .Add(FileContainer.SoilID)
                .Add(FileContainer.VarietyID)

                .AddNull()


                .Add(Phase.growthStageAsString(GrowthStage.ZC_00_Sowing))
                .Add(Phase.growthStageAsString(GrowthStage.ZC_92_Maturity))

                .AddNull()

                .Add("Warning type")
                .Add("Warning message");

                //.AddNull()

                //.Add("Warning type")
                //.Add("Warning message");

          

            page.NewLine()

                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()
                .AddNull()

                .Add("yyyy-mm-dd")
                .Add("yyyy-mm-dd")
                
                .AddNull()
                .AddNull()
                .AddNull();

            return page;

        }


        private void PrintMultiRunSummaryDate(LineData line, Run runOld, GrowthStage moment)
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

        public LineData MultiRunLine(Run runOld)
        {
            var line = new LineData();

            if (runOld.ManagementDef != null) line.Add(runOld.ManagementDef.Name); else line.Add("?");
            if (runOld.ParameterDef != null) line.Add(runOld.ParameterDef.Name); else line.Add("?");
            if (runOld.RunOptionDef != null) line.Add(runOld.RunOptionDef.Name); else line.Add("?");
            if (runOld.SiteDef != null) line.Add(runOld.SiteDef.Name); else line.Add("?");
            if (runOld.SoilDef != null) line.Add(runOld.SoilDef.Name); else line.Add("?");
            if (runOld.VarietyDef != null) line.Add(runOld.VarietyDef.Name); else line.Add("?");

            line.AddNull();


            PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_00_Sowing);
            PrintMultiRunSummaryDate(line, runOld, GrowthStage.ZC_92_Maturity);

            line.AddNull();

            PrintMultiRunWarning(line, runOld);

            string Tag = CutGrainWarningExtractor(runOld);

            if (Tag == "No Warnings") line = null;

            return line;
        }

        private void PrintMultiRunWarning(LineData line, Run runOld)
        {

            string Tag = CutGrainWarningExtractor(runOld);

            if (Tag != "") line.Add("Warning on grain filling cut").Add(Tag);
            else line.Add("No Warnings").Add("No Warnings");

            //Tag = PhenologyWarningExtractor(runOld);

            //if (Tag != "") line.AddNull().Add("Warning on phenology phase transitions").Add(Tag);
            //else line.AddNull().Add("No Warnings");

        }


    }
}
