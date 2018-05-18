using System;
using SiriusModel.InOut.OutputWriter;
using SiriusModel.Model.Phenology;
using SiriusModel.Model.CropModel;
using SiriusModel.Model;
using SiriusModel.Model.Base;
using SiriusQualityPhenology;

namespace SiriusModel.InOut
{
    public abstract class OutputFileExtractor
    {
        #region common output methods

        protected static void PrintVersionDate(PageData page)
        {
            page.NewLine()
                .Add("SiriusQuality2.5 output file")
                .AddDateTime(DateTime.Now)
                .AddNull() 
                .Add("build:" + ProjectFile.Build);
        }

        protected static void PrintFiles(PageData page)
        {
            page.NewLine().Add("Project").Add(ProjectFile.This.Path);
            page.NewLine().Add(FileContainer.ManagementID).Add(ProjectFile.This.FileContainer[FileContainer.ManagementID].RelativeFileName);
            page.NewLine().Add(FileContainer.NonVarietyID).Add(ProjectFile.This.FileContainer[FileContainer.NonVarietyID].RelativeFileName);
            page.NewLine().Add(FileContainer.RunOptionID).Add(ProjectFile.This.FileContainer[FileContainer.RunOptionID].RelativeFileName);
            page.NewLine().Add(FileContainer.SiteID).Add(ProjectFile.This.FileContainer[FileContainer.SiteID].RelativeFileName);
            page.NewLine().Add(FileContainer.SoilID).Add(ProjectFile.This.FileContainer[FileContainer.SoilID].RelativeFileName);
            page.NewLine().Add(FileContainer.VarietyID).Add(ProjectFile.This.FileContainer[FileContainer.VarietyID].RelativeFileName);
        }

        protected static Universe GetLastUniverse(Run runOld)
        {
            var lastUniverse = (runOld.SavedUniverses.Count > 0) ? runOld.SavedUniverses[runOld.SavedUniverses.Count - 1] : null;
            return lastUniverse;
        }

        public static Universe GetUniverse(Run runOld, GrowthStage moment)
        {
            DateTime? momentDate = null;
            Universe momentUniverse = null;
            var lastUniverse = GetLastUniverse(runOld);
            if (lastUniverse != null)
            {
                momentDate = lastUniverse.Crop_.getDateOfStage(moment);
            }
            if (momentDate.HasValue)
            {
                momentUniverse = runOld.GetUniverse(momentDate.Value);
            }
            return momentUniverse;
        }

        protected static int GetFinalLeafNumber(Run runOld)
        {
            var lastUniverse = GetLastUniverse(runOld);
            return (lastUniverse != null) ? lastUniverse.Crop_.RoundedFinalLeafNumber : 0;
        }

        protected static int GetNbSoilLayer(Run runOld)
        {
            var lastUniverse = GetLastUniverse(runOld);
            return lastUniverse != null ? lastUniverse.Soil_.Layers.Count : 0;
        }

        #endregion

        internal abstract PageData NormalRun(Run runOld);
        internal abstract PageData MultiRunHeader();
        internal abstract LineData MultiRunLine(Run runOld);
        internal abstract PageData SensitivityRunHeader(string[] deltaHeader);
        ///<Behnam>
        internal abstract LineData SensitivityRunLine(Run runOld, string[] deltaHeader, double[] deltaValue);
        ///</Behnam>
    }
}
