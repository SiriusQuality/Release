using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SiriusModel.InOut;
using SiriusModel.Model.CropModel;

namespace SiriusAutoRun
{
    /// <summary>
    /// Automate sirius runs.
    /// </summary>
    static class Program
    {
        #region Constants

        #region Paths

        /// <summary>
        /// Path to the data directory for normal runs.
        /// The relative path is correct if your run this executable in:
        /// "sema_sirius2009\Code\SiriusAutoRun\bin\*".
        /// </summary>
        private const string DataDirectory = @"..\..\..\Data";

        /// <summary>
        /// Path to the data directory used for the refactor test.
        /// </summary>
        private const string DataRefactorRefDirectory = @"..\..\..\DataRefactorRef";

        /// <summary>
        /// Path to the data directory used when we generate reference runs (run used as a reference in v1.3 templates).
        /// </summary>
        private const string DataMultiRefDirectory = DataDirectory;

        /// <summary>
        /// Path to the projects.
        /// </summary>
        private static readonly string[] ProjectPaths = new[]
                                                                       {
                                                                           DataDirectory + @"\v1.5-Project\v1.5-Project.sqpro",
                                                                           DataDirectory + @"\INRA-BBSRC 16cv project\INRA-BBSRC-16cv.sqpro"
                                                                       };

        #endregion

        #region Filters

        /// <summary>
        /// Filter the project to not load. 
        /// To skip a project, just uncomment a line like "//ProjectPaths[0]," for example.
        /// </summary>
        private static readonly IEnumerable<string> ToNotLoad = new string[]
                                                                   {
                                                                       ProjectPaths[0],
                                                                       //ProjectPaths[1],
                                                                   };

        /// <summary>
        /// Filter the run to not run.
        /// To skip a run in the project file, just add the name of the run, like "ALL SITES" in 
        /// INRA-BBSRC-16cv.sqpro for example.
        /// To unskip, just comment the line.
        /// </summary>
        private static readonly IEnumerable<string> ToNotRun = new string[]
                                                                   {
                                                                       "ALL SITES",
                                                                   };

        /// <summary>
        /// Extension to test in the refactor test.
        /// </summary>
        private static readonly IEnumerable<string> ExtensionToTest = new string[]
                                                                   {
                                                                       ".sqsro",
                                                                       ".sqbro"
                                                                   };

        /// <summary>
        /// Filter runs used in the Pheno run.
        /// If a multi run item name contains one of the following string, it will be run.
        /// Otherwise, the pheno run will ignore it.
        /// </summary>
        private static readonly IEnumerable<string> PhenoRunSelection = new string[]
                                                                            { // Select all HN
                                                                                "-HN01-",
                                                                                "-HN02-",
                                                                                "-HN03-",
                                                                                "-HN04-",
                                                                                "-HN05-",
                                                                                "-HN06-",
                                                                                "-HN07-",
                                                                                "-HN08-",
                                                                                "-HN09-",
                                                                                "-HN10-",
                                                                                "-HN11-",
                                                                                "-HN12-",
                                                                                "-HN13-",
                                                                                "-HN14-",
                                                                                "-HN15-",
                                                                                "-HN16-",
                                                                            };

        /// <summary>
        /// To select only the runs which runs a given variety, add the variety name here.
        /// If this array contains no variety names, this program will run all run (not filtered by ToNotRun).
        /// If this array contains one or more variety name, this program will run only the runs which
        /// uses the varieties defined here.
        /// To run all varieties, uncomment all items in this array.
        /// This array can be empty but not null.
        /// </summary>
        private static readonly string[] VarietyToRun = new string[] 
                {
                    "01-ALC"
                };

        #endregion

        #region Observations

        /// <summary>
        /// Observations used to calibrate leaf area index.
        /// </summary>
        private static readonly Dictionary<string, double> LeafAreaIndexAtAnthesis = new Dictionary<string, double>
                                                                                         {
                                                                                             {"UNOT08-HN01-ALC", 4.5},
                                                                                             {"UNOT08-HN02-BEA", 4.8},
                                                                                             {"UNOT08-HN03-CON", 4.1},
                                                                                             {"UNOT08-HN04-PAR", 4.1},
                                                                                             {"UNOT08-HN05-RIA", 4.0},
                                                                                             {"UNOT08-HN06-ROB", 4.9},
                                                                                             {"UNOT08-HN07-SAV", 3.8},
                                                                                             {"UNOT08-HN08-SOI", 3.3},
                                                                                             {"UNOT08-HN09-CF91", 4.5},
                                                                                             {"UNOT08-HN11-CF99", 3.3},
                                                                                             {"UNOT08-HN12-PER", 3.4},
                                                                                             {"UNOT08-HN13-QUE", 4.5},
                                                                                             {"UNOT08-HN14-REC", 2.8},
                                                                                             {"UNOT08-HN15-REN", 2.4},
                                                                                             {"UNOT08-HN16-TOI", 3.5}
                                                                                         };

        #endregion

        #region Random

        /// <summary>
        /// The random seed.
        /// </summary>
        const int RandomSeed = 31;

        /// <summary>
        /// The random number generator.
        /// The first line will create a new random generator with a time dependent seed.
        /// The second line will create a new random generator with a given seed.
        /// To repeat the same number generation, use the second line.
        /// To have a new random generation, use the first line.
        /// </summary>
        //static readonly Random Random = new Random();
        static Random Random = new Random(RandomSeed);

        /// <summary>
        /// Returns a random number between <paramref name="minValue"/> and <paramref name="maxValue"/>.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        /// <param name="minValue">The minimal value of the random number.</param>
        /// <param name="maxValue">The maximal value of the random number.</param>
        /// <returns>A random number between <paramref name="minValue"/> and <paramref name="maxValue"/></returns>
        static double NextDouble(this Random random, double minValue, double maxValue)
        {
            // random.NextDouble produce uniform distribution of values between 0 and 1.
            return minValue + random.NextDouble()*(maxValue - minValue);
        }

        #endregion

        #endregion

        /// <summary>
        /// Start this program.
        /// </summary>
        static void Main()
        {
            var executePath = Environment.CurrentDirectory; // The executable path.

            var exit = false; // while you want to redo this.
            while (!exit)
            {
                var startDate = DateTime.Now; // Reccord running time.

                const bool DoMulti = false; // Run all multi run.
                const bool DoMultiRef = false; // Generate multi run reference.
                const bool DoRefactorRef = false; // Generate the refactor test reference.
                const bool DoRefactorTest = false; // Do the refactor test.
                const bool DoCalibAreaIndex = false; // Do the area index calibration.
                const bool DoPhenoRun = true; // Do the pheno run.
                
                #region For each project

                // Enter a for each project loop if needed.
                if (DoMulti || DoMultiRef || DoRefactorRef || DoCalibAreaIndex || DoPhenoRun) // it is not a good idea to do all at one time
                {
                    foreach (var projectPath in ProjectPaths) // Iterate throught all projects.
                    {
                        try
                        {
                            if (ToNotLoad.Contains(projectPath)) continue; // Skip this project if it is marked as ToNotLoad.

                            var absoluteProjectPath = FileHelper.GetAbsolute(executePath, projectPath);
                            Console.Write("Loading: " + absoluteProjectPath);
                            ProjectFile.Load(absoluteProjectPath); // Loading the project.
                            Console.WriteLine(".");
                            Console.WriteLine();

                            #region Simple multi runs

                            if (DoMulti) ProjectHelper.ForEachMultiRun("Mutli run", true, true, "", DataDirectory, ToNotRun, null, true);
                            if (DoMultiRef) ProjectHelper.ForEachMultiRun("Multi run ref", true, true, "-ref", DataMultiRefDirectory, ToNotRun, null, true);
                            if (DoRefactorRef) ProjectHelper.ForEachMultiRun("Refactor run ref", true, true, "", DataRefactorRefDirectory, ToNotRun, null, true);

                            #endregion

                            #region Calib area index

                            if (DoCalibAreaIndex)
                            {
                                const double minArea = 15;
                                const double maxArea = 45;
                                ProjectHelper.ForEachMultiRun("Calib area index", false, false, "", DataDirectory, (rimm, i, s) =>
                                                                                                                       {
                                                                                                                           var multiRunItem = rimm.MultiRuns[i]; // Get mutli run item defintion.
                                                                                                                           var runName = multiRunItem.DailyOutputFileName.Replace(".sqsro", "");

                                                                                                                           if (!LeafAreaIndexAtAnthesis.ContainsKey(runName)) return true; // No observation, return.

                                                                                                                           var obs = LeafAreaIndexAtAnthesis[runName];

                                                                                                                           var variety = ProjectFile.This.FileContainer.VarietyFile[multiRunItem.VarietyItemSelected];
                                                                                                                           var initialValue = variety.AreaPLL; // Save inital AreaPLL value.

                                                                                                                           var bestAreaIndex = double.PositiveInfinity;
                                                                                                                           var bestParameter = double.NaN;

                                                                                                                           try
                                                                                                                           {
                                                                                                                               for (var area = minArea; area <= maxArea; ++area) // Run current multi run item for all area.
                                                                                                                               {
                                                                                                                                   rimm.StepRun(s, i);
                                                                                                                                   var runObject = RunCore.RunInstance;
                                                                                                                                   var universes = runObject.SavedUniverses;
                                                                                                                                   var lastUniverse = universes[universes.Count - 1];
                                                                                                                                   var anthesisDate = lastUniverse.Calendar_[GrowthStage.ZC_65_Anthesis];
                                                                                                                                   if (anthesisDate == null) continue;
                                                                                                                                   var anthesisUniverse = runObject.GetUniverse(anthesisDate.Value);
                                                                                                                                   var laiAnthesis = anthesisUniverse.Crop_.Phytomers_.LaminaeAI;
                                                                                                                                   if (Math.Abs(obs - laiAnthesis) > Math.Abs(obs - bestAreaIndex)) continue;
                                                                                                                                   bestAreaIndex = laiAnthesis; // A better value has been found.
                                                                                                                                   bestParameter = area;
                                                                                                                               }

                                                                                                                               if (!double.IsPositiveInfinity(bestAreaIndex)) // Print best value.
                                                                                                                               {
                                                                                                                                   Console.Write(variety.Name + " Area PLL : " + bestParameter + " Error: " + Math.Abs(obs - bestAreaIndex).ToString("0.000"));
                                                                                                                               }
                                                                                                                               else // No value found (all run in Error).
                                                                                                                               {
                                                                                                                                   Console.Write("No min found");
                                                                                                                               }
                                                                                                                               Console.WriteLine(" ]");
                                                                                                                               Console.Write("{0,20}", "");
                                                                                                                           }
                                                                                                                           finally
                                                                                                                           {
                                                                                                                               variety.AreaPLL = initialValue; // Restore intial AreaPLL value.
                                                                                                                           }
                                                                                                                           return false; // Run has not been skipped.
                                                                                                                       }, ToNotRun, VarietyToRun, true);
                            }

                            #endregion

                            #region Pheno run

                            if (DoPhenoRun)
                            {
                                const int numberOfPerturbation = 1000; // Number of perturbation for each multi run item.

                                // The pheno run f2akes a sensitivity run of 5 parameters.
                                var paramNames = new[]      {"Phyllochron", "PhyllFLLAnth", "SLDL",        "VAI",      "VBEE"};     // Parameters names which are perturbed.
                                var paramMinValues = new[]  { 70.0,         1.0,            0.20,          0.0002,     0.0040};     // Minimum of the parameters.
                                var paramMaxValues = new[]  { 140.0,        4.0,            1.0,           0.0030,     0.0150};     // Maximum of the parameters.
                                var paramSteps     = new[]  { 1.0,          0.05,           0.01,          0.00002,    0.0001};     // Step between each values in the space.
                                var paramValues = new[]     {double.NaN,    double.NaN,     double.NaN,    double.NaN, double.NaN}; // Parameters values which are perturbed.

                                var paramStepCounts = new[] { 1+(int)((paramMaxValues[0]-paramMinValues[0])/paramSteps[0]), // Step count for each parameters.
                                                              1+(int)((paramMaxValues[1]-paramMinValues[1])/paramSteps[1]), // This is the space length of each parameter.
                                                              1+(int)((paramMaxValues[2]-paramMinValues[2])/paramSteps[2]),
                                                              1+(int)((paramMaxValues[3]-paramMinValues[3])/paramSteps[3]),
                                                              1+(int)((paramMaxValues[4]-paramMinValues[4])/paramSteps[4])};

                                
                                const int indexPhyllochron = 0; // Index of the Phyllochron parameter in the array above.
                                const int indexPhyllFLLAnth = 1; // Index of the PhyllFLLAnth parameter in the array above.
                                const int indexSLDL = 2; // Index of the SLDL parameter in the array above.
                                const int indexVAI = 3; // Index of the VAI parameter in the array above.
                                const int indexVBEE = 4; // Index of the VBEE parameter in the array above.
                                const int numberOfParameter = 5; // Number of parameter to pertube.

                                var initialOutputVersion = ProjectFile.This.FileContainer.OutputVersion; // Save initial output version.

                                try
                                {
                                    ProjectFile.This.FileContainer.OutputVersion = OutputVersion.V15; // Ensure Output version 1.5, as it is the only one supporting sensitivity output.

                                    ProjectHelper.ForEachMultiRun("Pheno run", false, false, "", DataDirectory, (rimm, i, s) =>
                                                                                                                    {
                                                                                                                        var multiRunItem = rimm.MultiRuns[i]; // Get mutli run item defintion.
                                                                                                                        var runFileName = multiRunItem.DailyOutputFileName.Replace(".sqsro", ".sqsrs"); // Get summary output file name.
                                                                                                                        var runName = runFileName.Replace(".sqsro", "");

                                                                                                                        if (PhenoRunSelection.FirstOrDefault(runName.Contains) == null) return true; // Multi run item is not selected for the pheno run, return.
                                                                                                                        runFileName = runFileName.Replace("01-ALC", ""); // Remove vareity name.

                                                                                                                        var variety = ProjectFile.This.FileContainer.VarietyFile[multiRunItem.VarietyItemSelected];

                                                                                                                        // Save initial values.
                                                                                                                        var initialPhyllochron = variety.Phyllochron;
                                                                                                                        var initialPhyllFllAnth = variety.PhyllFLLAnth;
                                                                                                                        var initialSLDL = variety.SLDL;
                                                                                                                        var initialVAI = variety.VAI;
                                                                                                                        var initialVBEE = variety.VBEE;

                                                                                                                        try
                                                                                                                        {
                                                                                                                            // Create a new output file.
                                                                                                                            var page = OutputFile.ExtractSensitivityRunHeader(paramNames); // Generate header of the sensitivity file.
                                                                                                                            // var page = new PageData(); // Use this line to create the page if you don't want the header.

                                                                                                                            Random = new Random(RandomSeed); // Uncomment this line to always generate the same pertubations for each varieties on each site.
                                                                                                                            for (var permut = 0; permut < numberOfPerturbation; ++permut) // Perturbe and run loop.
                                                                                                                            {
                                                                                                                                for (var param = 0; param < numberOfParameter; ++param) // Generate a new vector.
                                                                                                                                {
                                                                                                                                    // Random.Next(paramStepCounts[param]) == random number in [0, paramStepCounts[param] - 1].
                                                                                                                                    paramValues[param] = paramMinValues[param] + paramSteps[param]*Random.Next(paramStepCounts[param]);
                                                                                                                                }
                                                                                                                                // Assign variety parameters.
                                                                                                                                variety.Phyllochron = paramValues[indexPhyllochron];
                                                                                                                                variety.PhyllFLLAnth = paramValues[indexPhyllFLLAnth];
                                                                                                                                variety.SLDL = paramValues[indexSLDL];
                                                                                                                                variety.VAI = paramValues[indexVAI];
                                                                                                                                variety.VBEE = paramValues[indexVBEE];

                                                                                                                                rimm.StepRun(s, i); // Run the model with the vector generated above.

                                                                                                                                page.Add(OutputFile.ExtractSensitivityRunLine(RunCore.RunInstance, paramValues)); // Save the summary of the run above.
                                                                                                                            }

                                                                                                                            Serialization.SerializeText(page, rimm.AbsoluteOutputDirectory + "\\" + runFileName); // Save the summary file.

                                                                                                                        }
                                                                                                                        finally
                                                                                                                        {
                                                                                                                            // Restore initial values.
                                                                                                                            variety.Phyllochron = initialPhyllochron;
                                                                                                                            variety.PhyllFLLAnth = initialPhyllFllAnth;
                                                                                                                            variety.SLDL = initialSLDL;
                                                                                                                            variety.VAI = initialVAI;
                                                                                                                            variety.VBEE = initialVBEE;
                                                                                                                        }
                                                                                                                        return false; // Run has not been skipped.
                                                                                                                    }, ToNotRun, VarietyToRun, true);
                                }
                                finally
                                {
                                    ProjectFile.This.FileContainer.OutputVersion = initialOutputVersion; // Restore initial output version.
                                }
                            }
                            
                            #endregion
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e.Message);
                        }
                        ProjectFile.New();
                    }
                }

                #endregion                

                #region Refactor test

                if (DoRefactorTest)
                {
                    Console.WriteLine("Refactor test");
                    var stack = new Stack<DirectoryInfo>();
                    stack.Push(new DirectoryInfo(DataRefactorRefDirectory));
                    do
                    {
                        var onTop = stack.Pop();
                        var error = 0;
                        Console.Write("{0,50}", onTop.FullName.Replace(DataRefactorRefDirectory, ""));
                        Console.Write(" [");
                        foreach (var fileRef in onTop.GetFiles())
                        {
                            if (!ExtensionToTest.Contains(fileRef.Extension)) continue;
                            try
                            {
                                var fileNew = new FileInfo(DataDirectory + fileRef.FullName.Replace(DataRefactorRefDirectory, ""));
                                using (var streamRef = fileRef.OpenRead())
                                {
                                    using (var streamNew = fileNew.OpenRead())
                                    {
                                        var differences = 0L;
                                        var count = 0L;

                                        var readRef = 0;
                                        var readNew = 0;
                                        do
                                        {

                                        }
                                        while ((readRef = streamRef.ReadByte()) != -1 && readRef != '\n');
                                        do
                                        {

                                        }
                                        while ((readNew = streamNew.ReadByte()) != -1 && readNew != '\n');


                                        while ((readRef = streamRef.ReadByte()) != -1
                                            && (readNew = streamNew.ReadByte()) != -1)
                                        {
                                            ++count;
                                            if (readRef != readNew) ++differences;
                                        }

                                        if (count == 0) Console.Write(" ");
                                        else if (differences > 0) Console.Write("*");
                                        else Console.Write(".");
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                ++error;
                                Console.Write("!");
                            }
                        }
                        Console.WriteLine("]");

                        foreach (var subDirectory in onTop.GetDirectories())
                        {
                            stack.Push(subDirectory);
                        }
                    }
                    while (stack.Count != 0);
                }

                #endregion

                var stopDate = DateTime.Now; // Reccord running time.
                Console.WriteLine();
                Console.WriteLine("done (" + (stopDate - startDate).TotalSeconds.ToString("0.00") + " s), redo ?");
                exit = Console.ReadKey().Key != ConsoleKey.Y; // We redo if user press Y.

                /*
                 * Create output file example.
                 * 
                 * var p = new PageData(); // Create a new output file.
                p.NewLine().Add("Parameter1").Add("Parameter2").Add("Value1").Add("Value2"); // Print a header for this file.
                                                                                             // Here 2 parameters and 2 values.
                foreach (var run in allRun) // The run loop, replace by what run you want to do, like for the PhenoRun.
                {
                    run.Run(); // Make your run.
                    p.NewLine().Add(run.Parameter1).Add(run.Parameter2).Add(run.Value1).Add(run.Value2); // Write one output line, with 
                                                                                                         // 2 parameter values, 2 output values.
                }
                Serialization.SerializeText(p, "MyFilePath.out"); // Save the output file.
                p.Clear(); // Clean memory.
                p = null;*/
            }
        }
    }
}
