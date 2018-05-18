using System;
using System.Collections.Generic;
using System.Linq;

namespace SiriusModel.InOut
{
    ///<summary>
    ///Project helper methods.
    ///</summary>
    public static class ProjectHelper
    {
        ///<summary>
        ///Do all run as multi run.
        ///</summary>
        ///<param name="title">Title of the operation.</param>
        ///<param name="serialize">Serialize output.</param>
        ///<param name="createDailyOutput">Create daily output or not.</param>
        ///<param name="dailyOutputSuffix">Sufix to add to the daily output file.</param>
        ///<param name="outputDirectory">Output directory.</param>
        public static void ForEachMultiRun(string title, bool serialize, bool createDailyOutput, string dailyOutputSuffix, string outputDirectory, IEnumerable<string> runToSkip, IEnumerable<string> varietySelected, bool showProgress, string varietyToOverride = null, string parameterName = null, double parameterValue =0)
        {
            ForEachMultiRun(title, serialize, createDailyOutput, dailyOutputSuffix, outputDirectory, (rimm, i, s, v, pn, pv) =>
            {
                rimm.StepRun(s, i, v, pn, pv);
                return false;
            }, runToSkip, varietySelected, showProgress, varietyToOverride, parameterName, parameterValue);


        }

        ///<summary>
        ///Do all run as multi run.
        ///</summary>
        ///<param name="title">Title of the operation.</param>
        ///<param name="serialize">Serialize output.</param>
        ///<param name="createDailyOutput">Create daily output or not.</param>
        ///<param name="dailyOutputSuffix">Suffix to add to the daily output file.</param>
        ///<param name="outputDirectory">Output directory override or null to let as it is in the run file.</param>
        ///<param name="doRunItemModeMulti">The action to perform on a given multi run item.</param>
        ///<param name="runToSkip">The name of the run to skip.</param>
        ///<param name="varietySelected">The variety selected.</param>
        public static void ForEachMultiRun(string title, bool serialize, bool createDailyOutput, string dailyOutputSuffix, string outputDirectory, Func<RunItemModeMulti, int, bool, string,string,double, bool> doRunItemModeMulti, IEnumerable<string> runToSkip, IEnumerable<string> varietySelected, bool showProgress, string varietyToOverride = null, string parameterName = null, double parameterValue =0)
        {
            if (showProgress) Console.WriteLine(title);
            var varietySelectedCount = varietySelected != null ? varietySelected.Count() : 0;
            foreach (var runItem in ProjectFile.This.FileContainer.RunFile.Items) // Iterates over all run items.
            {
                if (showProgress) Console.Write("{0,20}", runItem.Name);
                if (runToSkip != null && runToSkip.Contains(runItem.Name)) // Skip run if its name appears in runToSkip
                {
                    if (showProgress) Console.WriteLine(" skipped");
                    continue;
                }

                var multiRun = runItem.Multi; // Get the multi run defintion of this run item.


                // Save multi run definition.
                var originalExportNormalRun = multiRun.ExportNormalRuns;
                var orginalDailyOutputPattern = multiRun.DailyOutputPattern;
                var originalOutputDirectory = multiRun.OutputDirectory;
                try
                {
                    // Modify multi run definition.
                    multiRun.ExportNormalRuns = createDailyOutput;
                    multiRun.DailyOutputPattern += dailyOutputSuffix;
                    if (!string.IsNullOrEmpty(outputDirectory))
                    {
                        multiRun.OutputDirectory = outputDirectory;
                    }

                    // Run all multi run items
                    var nbRun = multiRun.InitRun(serialize);
                    if (showProgress) Console.Write(" [");
                    for (var i = 0; i < nbRun; ++i)
                    {
                        var varietyName = multiRun.MultiRuns[i].VarietyItemSelected;
                        if (varietySelectedCount > 0 && !varietySelected.Contains(varietyName)) 
                            continue; // Skip variety if its name doesn't appear in varietySelected.

                        var skipped = doRunItemModeMulti(multiRun, i, serialize, varietyToOverride, parameterName, parameterValue);
                        if (!skipped && showProgress) Console.Write(".");
                    }
                    multiRun.EndRun(serialize);

                    var book = multiRun.Book; // Clean memory.
                    if (book != null) book.Clear();
                }
                finally
                {
                    // Restore multi run defintion
                    multiRun.ExportNormalRuns = originalExportNormalRun;
                    multiRun.DailyOutputPattern = orginalDailyOutputPattern;
                    multiRun.OutputDirectory = originalOutputDirectory;
                }

                // Clean memory.
                multiRun.Book.Clear();
                multiRun.Book = null;

                // run done.
                if (showProgress) Console.WriteLine("]");
            }
            if (showProgress) Console.WriteLine();
        }
    }
}