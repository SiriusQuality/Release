using System;
using System.IO;
using SiriusModel;
using SiriusModel.InOut;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;

namespace SiriusConsole
{
    class Program
    {
        private const string OutputSingleRunOption = "/OutputSingleRun";
        private const string ShowProgressOption = "/ShowProgress";
        private const string NoOutputSingleRunOption = "/NoOutputSingleRun";
        private const string NoShowProgressOption = "/NoShowProgress";



        private static bool OutputDailyFiles = false;
        private static bool ShowProgressSimu = false;
        private static string ProjectFilePath;
        private static string OutputPath;
        private static string iToRun;
        private static List<string> SkipOverrideForRuns;
        private static List<String> DisplayRuns;
        private static List<String> SkipRuns;
        private static Dictionary<string, Dictionary<string, double>> VarietalPars;
        private static Dictionary<string, Dictionary<string, double>> NonVarietalPars;
       /*private static Dictionary<string, Dictionary<string, double>> GlobNonVarietalPars;*/
        private static Dictionary<string, Dictionary<string, dynamic>> SoilParVals;
        private static Dictionary<string, Dictionary<int, Dictionary<string, double>>> SoilLayers;
        private static Dictionary<string, Dictionary<string, dynamic>> SiteParVals;
        private static Dictionary<string, Dictionary<string, dynamic>> ManParVals;
        private static Dictionary<string, Dictionary<DateTime, dynamic[]>> ManByDate;
        private static Dictionary<string, Dictionary<string, dynamic[]>> ManByGS;
        private static string InputACEFile;
        //private static string OutputPathTranslator;
        private static string ProjectFolderPath;
        private static bool ByPassTranslation;
        private static bool ByPassSimulation;
        private static bool isTranslatorQuiet;
        private static string SearchKeyWordExpName;
        //private static string parameterName;
        //private static double parameterValue;
        private static Dictionary<string, Dictionary<string, dynamic[]>> SensAnlysis;
        private static bool isSensAnalysis;
        private static List<DateTime> OutputSelectedDates;
        private static string sensMode;
        private static string OptionalWeatherFilesPath;
        private static string OptionalOutputFilesPath;//
        private static string OptionalProjectFilesPath;//

        private static string OptionalProjectFileName;//
        private static string OptionalSiteFileName;//
        private static string OptionalSoilFileName;//
        private static string OptionalManagementFileName;//
        private static string OptionalRunFileName;//
        private static string OptionalVarietalFileName;//
        private static string OptionalNonVarietalFileName;//
        private static string OptionalRunOtpionFileName;//

        private static List<Tuple<string, DateTime>> ObsDateTime;



        static void Main(string[] args)
        {

            try
            {

                #region Old
                /*string[] argtest = new string[6];
                argtest[0] ="C:/Users/mouginot/Desktop/projet pour pierre/1-Project/Project.sqpro";
                argtest[1] = "/OutputSingleRun";
                argtest[2] = "/ShowProgress";
                argtest[3] = "Bacanora88";
                argtest[4] = "AreaPL";
                argtest[5] = "31";*/
                #endregion


                //ConfigFileItem conffi = new ConfigFileItem(true);
                //WriteXML(conffi);

                //string[] argtest = new string[1];
                //argtest[0] = "C:/Users/mancealo/Documents/DesktopBU/Shouyang/SQ_Projects/1-Project/Config_Override_Stand_Alone.sqconf";
                //argtest[0] = "C:/home/SQ_sim/barleyit_test/8-Config/Test_BarleyIT_simu.sqconf";
                //argtest[0] = "C:/home/SQ_sim/barleyit_test/8-Config/Test_BarleyIT_simu.sqconf";

                //argtest[0] = "C:/home/SQ_sim/barleyit_test/8-Config/Test_BarleyIT_simu.sqconf";


               //string[] argtestcl = new string[7];
                //string[] argtestcl = new string[21];
                //string[] argtestcl = new string[7];
                //string[] argtestcl = new string[32];
                //string[] argtestcl = new string[1];
                //argtestcl[0] = "-tran";
                /*
                argtestcl[0] = "-transimoverride";
                argtestcl[1] = "C:/Users/mancealo/Documents/DesktopBU/Modules Rami/Data/projets SQ_FACE/1-Project/INRA_SOERE2013.json";
                argtestcl[2] = "h";
                argtestcl[3] = "false";
                argtestcl[4] = "C:/Users/mancealo/";
                argtestcl[5] = "true";
                argtestcl[6] = "C:/Users/mancealo/Documents/DesktopBU/Modules Rami/Data/projets SQ_FACE/1-Project/ProjectDev2TestConsole.sqpro";
                argtestcl[7] = "+Dates";
                argtestcl[8] = "1994-04-25";
                argtestcl[9] = "1994-03-08";
                argtestcl[10] = "1993-12-20";
                argtestcl[11] = "Dates+";
                argtestcl[12] = "--Var";
                argtestcl[13] = "[Yecora Rojo]";
                argtestcl[14] = "AreaPL";
                argtestcl[15] = "50.0";
                argtestcl[16] = "AreaSL";
                argtestcl[17] = "5.0";
                argtestcl[18] = "[Batten]";
                argtestcl[19] = "Degfm";
                argtestcl[20] = "300";
                argtestcl[21] = "--Man";
                argtestcl[22] = "[RS05]";
                argtestcl[23] = "SowingDate";
                argtestcl[24] = "1992-01-05";
                argtestcl[25] = "TargetFertileShootNumber";
                argtestcl[26] = "600";
                argtestcl[27] = "IsTotalNitrogen";
                argtestcl[28] = "true";
                argtestcl[29] = "[R11]";
                argtestcl[30] = "CO2";
                argtestcl[31] = "700";*/

                //argtestcl[0] = "C:/SQ_Release_Test/1-Project/Config_Simulation.sqconf";
                //argtestcl[1] = "-simoverride";
                //argtestcl[2] = "true";
                //argtestcl[3] = "true";
                //argtestcl[4] = "C:/SQ_Release_Test/1-Project/Project.sqpro";
               //argtestcl[5] = "--Run";
                ////argtestcl[21] = "Calibration";
                //argtestcl[6] = "MaricopaFACE";
                //argtestcl[5] = "--ManByGS";
                //argtestcl[6] = "[901-Dry-High_N-Ambient_CO2]";
                //argtestcl[7] = "{ZC_10_Emergence}";
                //argtestcl[8] = "Ammonium_Nitrate";
                //argtestcl[9] = "55";
                //argtestcl[10] = "0";
                //argtestcl[11] = "{ZC_65_Anthesis}";
                //argtestcl[12] = "Urea";
                //argtestcl[13] = "15";
                //argtestcl[14] = "0";
                //argtestcl[17] = "[902-Wet-High_N-Ambient_CO2]";
                //argtestcl[18] = "{ZC_00_Sowing}";
                //argtestcl[19] = "Calcium_Nitrate";
                //argtestcl[20] = "0";
                //argtestcl[21] = "66";
                //argtestcl[15] = "--Site";
                //argtestcl[16] = "[MARA-92-93]";
                //argtestcl[17] = "MinSowingDate";
                //argtestcl[18] = "1992-11-01";
                //argtestcl[19] = "--Man";
                //argtestcl[20] = "[901-Dry-High_N-Ambient_CO2]";
                //argtestcl[21] = "SoilWaterDeficit";
                //argtestcl[22] = "170";
                //argtestcl[10] = "90.0";
                //argtestcl[11] = "VAI";
                //argtestcl[12] = "0.08";
                //argtestcl[0] = "C:/SQ_Release_Test/1-Project/Config_Sensitivity_List.sqconf";
                //argtestcl[1] = "-transimsens";
                //argtestcl[2] = "C:/Users/mancealo/Documents/DesktopBU/Modules Rami/Data/projets SQ_FACE/1-Project/INRA_SOERE2013.json";
                //argtestcl[3] = "";
                //argtestcl[4] = "false";
                //argtestcl[5] = "C:/Users/mancealo/";
                //argtestcl[6] = "true";
                //argtestcl[7] = "C:/SQ_Release_Test/1-Project/Project.sqpro";
                /*argtestcl[7] = "Vector";
                argtestcl[8] = "+Dates";
                argtestcl[9] = "1994-04-25";
                argtestcl[10] = "1994-03-08";
                argtestcl[11] = "1993-12-20";
                argtestcl[12] = "Dates+";
                argtestcl[13] = "Management.CO2";
                argtestcl[14] = "200.0";
                argtestcl[15] = "400";
                argtestcl[16] = "600";
                argtestcl[17] = "Crop.Degfm";
                argtestcl[18] = "205.0";
                argtestcl[19] = "210";*/

                /*argtestcl[7] = "Default";
                argtestcl[8] = "+Dates";
                argtestcl[9] = "1994-04-25";
                argtestcl[10] = "1994-03-08";
                argtestcl[11] = "1993-12-20";
                argtestcl[12] = "Dates+";
                argtestcl[13] = "Management.CO2";
                argtestcl[14] = "[RegularMinMax]";
                argtestcl[15] = "5";
                argtestcl[16] = "500";
                argtestcl[17] = "200";
                argtestcl[18] = "Crop.Dcd";
                argtestcl[19] = "[RegularPercent]";
                argtestcl[20] = "5";
                argtestcl[21] = "10";
                argtestcl[22] = "-10";*/
                /*
                argtestcl[0] = "-simsens";
                argtestcl[1] = "true";
                argtestcl[2] = "true";
                argtestcl[3] = "C:/Users/mancealo/Documents/DesktopBU/Modules Rami/Data/projets SQ_FACE/1-Project/ProjectDev2TestConsole.sqpro";
                argtestcl[4] = "VectorOneByOne";
                argtestcl[5] = "Management.CO2";
                argtestcl[6] = "200";
                argtestcl[7] = "300";
                argtestcl[8] = "240";
                argtestcl[9] = "Crop.Dcd";
                argtestcl[10] = "190";
                argtestcl[11] = "200";*/

                //argtestcl[0] = "-sim";
                //argtestcl[1] = "true";
                //argtestcl[2] = "true";
                //argtestcl[3] = "C:/Users/mancealo/Documents/DesktopBU/Modules Rami/Data/projets SQ_FACE/1-Project/ProjectDev2TestConsole.sqpro";

                //argtestcl[0] = "C:/SQ_Release_Test/1-Project/Config_Simulation.sqconf";
                //argtestcl[1] = "-transimoverride";
                //argtestcl[2] = "true";
                //argtestcl[3] = "true";
                //argtestcl[4] = "C:/SQ_Release_Test/1-Project/Project.sqpro";
                //argtestcl[5] = "--Var";
                //argtestcl[6] = "[Yecora Rojo]";
                //argtestcl[7] = "AreaPL";
                //argtestcl[8] = "30";




                if (args.Length == 1) GetArgumentsConfig(args, true);
                else GetArguments(args, false);

                //if (argtest.Length == 1) GetArgumentsConfig(argtest, true);
                //else GetArguments(argtest, false);

                //if (argtestcl.Length == 1) GetArgumentsConfig(argtestcl, true);
                //else GetArguments(argtestcl, false);


                //InputACEFile = args[1];
                //SearchKeyWordExpName = args[2];
                //isTranslatorQuiet = bool.Parse(args[3]);
                //ProjectFolderPath = args[4];

                if (!ByPassTranslation)
                {
                    //Import_Export_SQ_ICASA.Translator tr = new Import_Export_SQ_ICASA.Translator(InputJSonFile, OutputPathTranslator, SearchKeyWordExpName, isTranslatorQuiet);
                    //Import_Export_SQ_ICASA.Translator tr = new Import_Export_SQ_ICASA.Translator(InputACEFile, ProjectFolderPath, SearchKeyWordExpName, isTranslatorQuiet,
                        Import_Export_SQ_ICASA.Translator tr = new Import_Export_SQ_ICASA.Translator(InputACEFile, ProjectFolderPath,isTranslatorQuiet,
                                                                                                 OptionalWeatherFilesPath, OptionalOutputFilesPath, OptionalProjectFilesPath,
                                                                                                 OptionalProjectFileName, OptionalSiteFileName, OptionalSoilFileName, OptionalManagementFileName,
                                                                                                 OptionalRunFileName, OptionalVarietalFileName, OptionalNonVarietalFileName, OptionalRunOtpionFileName, true);
                    tr.Run(false);
                }

                //RunProjectFile(varietyToOverride , parameterName , parameterValue);

                if (!ByPassSimulation)
                {

                    LoadProjectFile();

                    if (!isSensAnalysis)
                    {
                        RunProjectFile(SkipOverrideForRuns, VarietalPars, NonVarietalPars, /*GlobNonVarietalPars,*/ SoilParVals, SoilLayers, SiteParVals, ManParVals, ManByDate, ManByGS, OutputSelectedDates, ObsDateTime);
                    }
                    else
                    {
                        RunSensivityAnalysis(SkipOverrideForRuns, SensAnlysis, OutputSelectedDates, ObsDateTime);
                    }
                }

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                Console.WriteLine();
                Console.WriteLine();
                //Console.WriteLine("SiriusConsole \"path to project file\" /(No)ShowProgress  /(No)OutputSingleRun [varietyToOverride] [parameterName] [parameterValue]");
                Console.WriteLine("SiriusConsole \"path to configuration file\"");
            }
}

        private static void GetArguments(string[] args, bool isConfigAlone)
        {


            int ishiftconf = 0;
            if (File.Exists(args[0])) ishiftconf++;

            switch (args[0 + ishiftconf])
            {
                case "-tran":
                    {
                        if (args.Length != 4 + ishiftconf) throw new ArgumentException("Error: for translation 4 arguments are mandatory, .json Keyword isTranslatorQuiet Refdirectory");
                        else
                        {
                            ByPassSimulation = true;
                            ByPassTranslation = false;

                            InputACEFile = args[1 + ishiftconf];
                            //SearchKeyWordExpName = args[2 + ishiftconf];
                            isTranslatorQuiet = bool.Parse(args[2 + ishiftconf]);
                            ProjectFolderPath = args[3 + ishiftconf];
                        }

                        break;
                    }

                case "-transimoverride":
                    {
                        if (args.Length < 6 + ishiftconf) throw new ArgumentException("Error: for translation and simulation override at least 7 arguments are necessary. See documentation.");
                        else
                        {
                            DisplayRuns = null;
                            SkipRuns = null;
                            OutputSelectedDates = null;
                            SkipOverrideForRuns = null;

                            ManParVals = null;
                            SiteParVals = null;
                            SoilParVals = null;
                            NonVarietalPars = null;
                            //GlobNonVarietalPars = null;
                            VarietalPars = null;
                            SoilLayers = null;
                            ManByDate = null;
                            ManByGS = null;

                            GetArgumentsConfig(args, isConfigAlone, false);

                            ByPassSimulation = false;
                            ByPassTranslation = false;
                            isSensAnalysis = false;

                            InputACEFile = args[1 + ishiftconf];
                            //SearchKeyWordExpName = args[2 + ishiftconf];
                            isTranslatorQuiet = bool.Parse(args[2 + ishiftconf]);
                            ProjectFolderPath = args[3 + ishiftconf];


                            OutputDailyFiles = bool.Parse(args[4 + ishiftconf]);
                            ShowProgressSimu = !isTranslatorQuiet;
                            ProjectFilePath = args[5 + ishiftconf];

                            List<Dictionary<string, Dictionary<string, double>>> ListVarietalPars = new List<Dictionary<string, Dictionary<string, double>>>();
                            List<Dictionary<string, Dictionary<string, double>>> ListNonVarietalPars = new List<Dictionary<string, Dictionary<string, double>>>();
                            //List<Dictionary<string, Dictionary<string, double>>> ListGlobNonVarietalPars = new List<Dictionary<string, Dictionary<string, double>>>();
                            List<Dictionary<string, Dictionary<string, dynamic>>> ListSoilParVals = new List<Dictionary<string, Dictionary<string, dynamic>>>();
                            List<Dictionary<string, Dictionary<string, dynamic>>> ListSiteParVals = new List<Dictionary<string, Dictionary<string, dynamic>>>();
                            List<Dictionary<string, Dictionary<string, dynamic>>> ListManParVals = new List<Dictionary<string, Dictionary<string, dynamic>>>();

                            for (int i = 6 + ishiftconf; i < args.Length; i++)
                            {
                                int j = i + 1;
                                int k = j + 1;

                                if (args[i] == "+Dates")
                                {
                                    OutputSelectedDates = new List<DateTime>();
                                    int l = i + 1;
                                    while (args[l] != "Dates+") { OutputSelectedDates.Add(DateTime.Parse(args[l])); l++; }
                                }

                                if (args[i] == "--Var") ListVarietalPars.Add(FillDicCrop(j, k, args));

                                if (args[i] == "--NonVar") ListNonVarietalPars.Add(FillDicCrop(j, k, args));

                                //if (args[i] == "--Glob") ListGlobNonVarietalPars.Add(FillDicCrop(j, k, args));

                                if (args[i] == "--Soil") ListSoilParVals.Add(FillDicDyn(j, k, args));

                                if (args[i] == "--Site") ListSiteParVals.Add(FillDicDyn(j, k, args));

                                if (args[i] == "--Man") ListManParVals.Add(FillDicDyn(j, k, args));


                            }



                            Dictionary<string, Dictionary<string, double>> VarietalParsConsLine = MergeDictDouble(ListVarietalPars);

                            if (ishiftconf == 1)
                            {

                                foreach (string k in VarietalParsConsLine.Keys)
                                {
                                    if (!VarietalPars.ContainsKey(k)) VarietalPars.Add(k, VarietalParsConsLine[k]);
                                    else
                                    {
                                        foreach (string s in VarietalParsConsLine[k].Keys)
                                        {
                                            try { VarietalPars[k].Add(s, VarietalParsConsLine[k][s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }

                                    }
                                }
                            }
                            else VarietalPars = VarietalParsConsLine;


                            Dictionary<string, Dictionary<string, double>> NonVarietalParsConsLine = MergeDictDouble(ListNonVarietalPars);

                            if (ishiftconf == 1)
                            {

                                foreach (string k in NonVarietalParsConsLine.Keys)
                                {
                                    if (!NonVarietalPars.ContainsKey(k)) NonVarietalPars.Add(k, NonVarietalParsConsLine[k]);
                                    else
                                    {
                                        foreach (string s in NonVarietalParsConsLine[k].Keys)
                                        {
                                            try { NonVarietalPars[k].Add(s, NonVarietalParsConsLine[k][s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }

                                    }
                                }
                            }
                            else NonVarietalPars = NonVarietalParsConsLine;


                           /* Dictionary<string, Dictionary<string, double>> GlobNonVarietalParsConsLine = MergeDictDouble(ListGlobNonVarietalPars);

                            if (ishiftconf == 1)
                            {

                                foreach (string k in GlobNonVarietalParsConsLine.Keys)
                                {
                                    if (!GlobNonVarietalPars.ContainsKey(k)) GlobNonVarietalPars.Add(k, GlobNonVarietalParsConsLine[k]);
                                    else
                                    {
                                        foreach (string s in GlobNonVarietalParsConsLine[k].Keys)
                                        {
                                            try { GlobNonVarietalPars[k].Add(s, GlobNonVarietalParsConsLine[k][s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }

                                    }
                                }
                            }
                            else GlobNonVarietalPars = GlobNonVarietalParsConsLine;
                           */

                            Dictionary<string, Dictionary<string, dynamic>> SoilParValsConsLine = MergeDictDyn(ListSoilParVals);

                            if (ishiftconf == 1)
                            {

                                foreach (string k in SoilParValsConsLine.Keys)
                                {
                                    if (!SoilParVals.ContainsKey(k)) SoilParVals.Add(k, SoilParValsConsLine[k]);
                                    else
                                    {
                                        foreach (string s in SoilParValsConsLine[k].Keys)
                                        {
                                            try { SoilParVals[k].Add(s, SoilParValsConsLine[k][s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }

                                    }
                                }
                            }
                            else SoilParVals = SoilParValsConsLine;


                            Dictionary<string, Dictionary<string, dynamic>> SiteParValsConsLine = MergeDictDyn(ListSiteParVals);

                            if (ishiftconf == 1)
                            {

                                foreach (string k in SiteParValsConsLine.Keys)
                                {
                                    if (!SiteParVals.ContainsKey(k)) SiteParVals.Add(k, SiteParValsConsLine[k]);
                                    else
                                    {
                                        foreach (string s in SiteParValsConsLine[k].Keys)
                                        {
                                            try { SiteParVals[k].Add(s, SiteParValsConsLine[k][s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }

                                    }
                                }
                            }
                            else SiteParVals = SiteParValsConsLine;


                            Dictionary<string, Dictionary<string, dynamic>> ManParValsConsLine = MergeDictDyn(ListManParVals);

                            if (ishiftconf == 1)
                            {

                                foreach (string k in ManParValsConsLine.Keys)
                                {
                                    if (!ManParVals.ContainsKey(k)) ManParVals.Add(k, ManParValsConsLine[k]);
                                    else
                                    {
                                        foreach (string s in ManParValsConsLine[k].Keys)
                                        {
                                            try { ManParVals[k].Add(s, ManParValsConsLine[k][s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }

                                    }
                                }
                            }
                            else ManParVals = ManParValsConsLine;
                        }
                        break;
                    }
                case "-transimsens":
                    {
                        if (args.Length < 7 + ishiftconf) throw new ArgumentException("Error: for translation and sensitivity simulation at least 8 arguments are necessary. See documentation.");
                        else
                        {
                            DisplayRuns = null;
                            SkipRuns = null;
                            OutputSelectedDates = null;
                            SkipOverrideForRuns = null;

                            GetArgumentsConfig(args, isConfigAlone, true);

                            ByPassSimulation = false;
                            ByPassTranslation = false;
                            isSensAnalysis = true;

                            InputACEFile = args[1 + ishiftconf];
                            //SearchKeyWordExpName = args[2 + ishiftconf];
                            isTranslatorQuiet = bool.Parse(args[2 + ishiftconf]);
                            ProjectFolderPath = args[3 + ishiftconf];


                            OutputDailyFiles = bool.Parse(args[4 + ishiftconf]);
                            ShowProgressSimu = !isTranslatorQuiet;
                            ProjectFilePath = args[5 + ishiftconf];


                            if (ishiftconf == 0) sensMode = args[6 + ishiftconf];

                            for (int i = 7 + ishiftconf; i < args.Length; i++)
                            {
                                if (args[i] == "+Dates")
                                {
                                    OutputSelectedDates = new List<DateTime>();
                                    int l = i + 1;
                                    while (args[l] != "Dates+") { OutputSelectedDates.Add(DateTime.Parse(args[l])); l++; }
                                }
                            }

                            if (sensMode != "Vector")
                            {
                                foreach (string k in SensAnlysis.Keys)
                                {
                                    if (!SensAnlysis.ContainsKey(k)) SensAnlysis.Add(sensMode, FillSens(8 + ishiftconf, args));
                                    else
                                    {
                                        foreach (string s in FillSens(8 + ishiftconf, args).Keys)
                                        {
                                            try { SensAnlysis[k].Add(s, FillSens(8 + ishiftconf, args)[s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }
                                    }
                                }

                            }
                            else {

                                foreach (string k in SensAnlysis.Keys)
                                {
                                    if (!SensAnlysis.ContainsKey(k)) SensAnlysis.Add(sensMode, FillSensVector(8 + ishiftconf, args));
                                    else
                                    {
                                        foreach (string s in FillSensVector(8 + ishiftconf, args).Keys)
                                        {
                                            try { SensAnlysis[k].Add(s, FillSensVector(8 + ishiftconf, args)[s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }
                                    }
                                }

                            }

                            break;
                        }
                    }

                case "-simoverride":
                    {
                        if (args.Length < 4 + ishiftconf) throw new ArgumentException("Error: for simulation override at least 7 arguments are mandatory. See documentation.");
                        else
                        {
                            DisplayRuns = null;
                            SkipRuns = null;
                            OutputSelectedDates = null;
                            SkipOverrideForRuns = null;

                            ManParVals = null;
                            SiteParVals = null;
                            SoilParVals = null;
                            NonVarietalPars = null;
                            //GlobNonVarietalPars = null;
                            VarietalPars = null;
                            SoilLayers = null;
                            ManByDate = null;
                            ManByGS = null;

                            GetArgumentsConfig(args, isConfigAlone, false);

                            ByPassSimulation = false;
                            ByPassTranslation = true;
                            isSensAnalysis = false;

                            OutputDailyFiles = bool.Parse(args[1 + ishiftconf]);
                            ShowProgressSimu = bool.Parse(args[2 + ishiftconf]);
                            ProjectFilePath = args[3 + ishiftconf];

                            List<Dictionary<string, Dictionary<string, double>>> ListVarietalPars = new List<Dictionary<string, Dictionary<string, double>>>();
                            List<Dictionary<string, Dictionary<string, double>>> ListNonVarietalPars = new List<Dictionary<string, Dictionary<string, double>>>();
                            //List<Dictionary<string, Dictionary<string, double>>> ListGlobNonVarietalPars = new List<Dictionary<string, Dictionary<string, double>>>();
                            List<Dictionary<string, Dictionary<string, dynamic>>> ListSoilParVals = new List<Dictionary<string, Dictionary<string, dynamic>>>();
                            List<Dictionary<string, Dictionary<string, dynamic>>> ListSiteParVals = new List<Dictionary<string, Dictionary<string, dynamic>>>();
                            List<Dictionary<string, Dictionary<string, dynamic>>> ListManParVals = new List<Dictionary<string, Dictionary<string, dynamic>>>();
                            List<Dictionary<string, Dictionary<string, dynamic[]>>> ListManbyGParVals = new List<Dictionary<string, Dictionary<string, dynamic[]>>>();
                            List<Dictionary<string, Dictionary<int, Dictionary<string, double>>>> ListSoilbyLParVals = new List<Dictionary<string, Dictionary<int, Dictionary<string, double>>>>();

                            for (int i = 4 + ishiftconf; i < args.Length; i++)
                            {
                                int j = i + 1;
                                int k = j + 1;

                                if (args[i] == "+Dates")
                                {
                                    OutputSelectedDates = new List<DateTime>();
                                    int l = i + 1;
                                    while (args[l] != "Dates+") { OutputSelectedDates.Add(DateTime.Parse(args[l])); l++; }
                                }

                                if (args[i] == "--Var") ListVarietalPars.Add(FillDicCrop(j, k, args));

                                if (args[i] == "--NonVar") ListNonVarietalPars.Add(FillDicCrop(j, k, args));

                                //if (args[i] == "--Glob") ListGlobNonVarietalPars.Add(FillDicCrop(j, k, args));

                                if (args[i] == "--Soil") ListSoilParVals.Add(FillDicDyn(j, k, args));

                                if (args[i] == "--SoilByL") ListSoilbyLParVals.Add(FillDicSoilL(j, k, args));

                                if (args[i] == "--Site") ListSiteParVals.Add(FillDicDyn(j, k, args));

                                if (args[i] == "--Man") ListManParVals.Add(FillDicDyn(j, k, args));

                                if (args[i] == "--ManByGS") ListManbyGParVals.Add(FillDicManGS(j, k, args));

                                if (args[i] == "--Run")
                                {
                                    DisplayRuns = new List<string>();
                                    int ir = i + 1;
                                    while (!args[ir].Contains("--")) {
                                        DisplayRuns.Add(args[ir]);
                                        try { args[ir + 1].Contains("--"); }
                                        catch(Exception e) { break; }
                                        ir++;
                                    }
                                }

                                if (args[i] == "--OutPath") OutputPath = args[i + 1];

                                if(args[i] == "--iRun") iToRun = args[i + 1];
                            }


                            Dictionary<string, Dictionary<int, Dictionary<string, double>>> ListSoilbyLConsLine = MergeDictSoliL(ListSoilbyLParVals);
                            
                            if (ishiftconf == 1)
                            {

                                foreach (string k in ListSoilbyLConsLine.Keys)
                                {
                                    if (!SoilLayers.ContainsKey(k)) SoilLayers.Add(k, ListSoilbyLConsLine[k]);
                                    else
                                    {
                                        foreach (int s in ListSoilbyLConsLine[k].Keys)
                                        {
                                            try { SoilLayers[k].Add(s, ListSoilbyLConsLine[k][s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }

                                    }
                                }
                            }
                            else SoilLayers = ListSoilbyLConsLine;


                            Dictionary<string, Dictionary<string, double>> VarietalParsConsLine = MergeDictDouble(ListVarietalPars);

                            if (ishiftconf == 1)
                            {

                                foreach (string k in VarietalParsConsLine.Keys)
                                {
                                    if (!VarietalPars.ContainsKey(k)) VarietalPars.Add(k, VarietalParsConsLine[k]);
                                    else
                                    {
                                        foreach (string s in VarietalParsConsLine[k].Keys)
                                        {
                                            try { VarietalPars[k].Add(s, VarietalParsConsLine[k][s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }

                                    }
                                }
                            }
                            else VarietalPars = VarietalParsConsLine;


                            Dictionary<string, Dictionary<string, double>> NonVarietalParsConsLine = MergeDictDouble(ListNonVarietalPars);

                            if (ishiftconf == 1)
                            {

                                foreach (string k in NonVarietalParsConsLine.Keys)
                                {
                                    if (!NonVarietalPars.ContainsKey(k)) NonVarietalPars.Add(k, NonVarietalParsConsLine[k]);
                                    else
                                    {
                                        foreach (string s in NonVarietalParsConsLine[k].Keys)
                                        {
                                            try { NonVarietalPars[k].Add(s, NonVarietalParsConsLine[k][s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }

                                    }
                                }
                            }
                            else NonVarietalPars = NonVarietalParsConsLine;


                           /* Dictionary<string, Dictionary<string, double>> GlobNonVarietalParsConsLine = MergeDictDouble(ListGlobNonVarietalPars);

                            if (ishiftconf == 1)
                            {

                                foreach (string k in GlobNonVarietalParsConsLine.Keys)
                                {
                                    if (!GlobNonVarietalPars.ContainsKey(k)) GlobNonVarietalPars.Add(k, GlobNonVarietalParsConsLine[k]);
                                    else
                                    {
                                        foreach (string s in GlobNonVarietalParsConsLine[k].Keys)
                                        {
                                            try { GlobNonVarietalPars[k].Add(s, GlobNonVarietalParsConsLine[k][s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }

                                    }
                                }
                            }
                            else GlobNonVarietalPars = GlobNonVarietalParsConsLine;
                           */



                            Dictionary<string, Dictionary<string, dynamic>> SoilParValsConsLine = MergeDictDyn(ListSoilParVals);

                            if (ishiftconf == 1)
                            {

                                foreach (string k in SoilParValsConsLine.Keys)
                                {
                                    if (!SoilParVals.ContainsKey(k)) SoilParVals.Add(k, SoilParValsConsLine[k]);
                                    else
                                    {
                                        foreach (string s in SoilParValsConsLine[k].Keys)
                                        {
                                            try { SoilParVals[k].Add(s, SoilParValsConsLine[k][s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }

                                    }
                                }
                            }
                            else SoilParVals = SoilParValsConsLine;


                            Dictionary<string, Dictionary<string, dynamic>> SiteParValsConsLine = MergeDictDyn(ListSiteParVals);

                            if (ishiftconf == 1)
                            {

                                foreach (string k in SiteParValsConsLine.Keys)
                                {
                                    if (!SiteParVals.ContainsKey(k)) SiteParVals.Add(k, SiteParValsConsLine[k]);
                                    else
                                    {
                                        foreach (string s in SiteParValsConsLine[k].Keys)
                                        {
                                            try { SiteParVals[k].Add(s, SiteParValsConsLine[k][s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }

                                    }
                                }
                            }
                            else SiteParVals = SiteParValsConsLine;


                            Dictionary<string, Dictionary<string, dynamic>> ManParValsConsLine = MergeDictDyn(ListManParVals);

                            if (ishiftconf == 1)
                            {

                                foreach (string k in ManParValsConsLine.Keys)
                                {
                                    if (!ManParVals.ContainsKey(k)) ManParVals.Add(k, ManParValsConsLine[k]);
                                    else
                                    {
                                        foreach (string s in ManParValsConsLine[k].Keys)
                                        {
                                            try { ManParVals[k].Add(s, ManParValsConsLine[k][s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }

                                    }
                                }
                            }
                            else ManParVals = ManParValsConsLine;

                            Dictionary<string, Dictionary<string, dynamic[]>> ManByGSParValsConsLine = MergeDictManByGS(ListManbyGParVals);

                            if (ishiftconf == 1)
                            {

                                foreach (string k in ManByGSParValsConsLine.Keys)
                                {
                                    if (!ManByGS.ContainsKey(k)) ManByGS.Add(k, ManByGSParValsConsLine[k]);
                                    else
                                    {
                                        foreach (string s in ManByGSParValsConsLine[k].Keys)
                                        {
                                            try { ManByGS[k].Add(s, ManByGSParValsConsLine[k][s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }

                                    }
                                }
                            }
                            else ManByGS = ManByGSParValsConsLine;
                        }
                        break;
                    }

                case "-simsens":
                    {
                        if (args.Length < 5 + ishiftconf) throw new ArgumentException("Error: for translation and sensitivity simulation at least 5 arguments are necessary. See documentation.");
                        else
                        {
                            DisplayRuns = null;
                            SkipRuns = null;
                            OutputSelectedDates = null;
                            SkipOverrideForRuns = null;

                            GetArgumentsConfig(args, isConfigAlone, true);

                            ByPassSimulation = false;
                            ByPassTranslation = true;
                            isSensAnalysis = true;


                            OutputDailyFiles = bool.Parse(args[1 + ishiftconf]);
                            ShowProgressSimu = bool.Parse(args[2 + ishiftconf]);
                            ProjectFilePath = args[3 + ishiftconf];


                            if (ishiftconf == 0) sensMode = args[4 + ishiftconf];

                            for (int i = 5 + ishiftconf; i < args.Length; i++)
                            {

                                if (args[i] == "+Dates")
                                {
                                    OutputSelectedDates = new List<DateTime>();
                                    int l = i + 1;
                                    while (args[l] != "Dates+") { OutputSelectedDates.Add(DateTime.Parse(args[l])); l++; }
                                }
                            }

                            if (sensMode != "Vector")
                            {
                                foreach (string k in SensAnlysis.Keys)
                                {
                                    if (!SensAnlysis.ContainsKey(k)) SensAnlysis.Add(sensMode, FillSens(8 + ishiftconf, args));
                                    else
                                    {
                                        foreach (string s in FillSens(8 + ishiftconf, args).Keys)
                                        {
                                            try { SensAnlysis[k].Add(s, FillSens(8 + ishiftconf, args)[s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }
                                    }
                                }

                            }
                            else
                            {

                                foreach (string k in SensAnlysis.Keys)
                                {
                                    if (!SensAnlysis.ContainsKey(k)) SensAnlysis.Add(sensMode, FillSensVector(8 + ishiftconf, args));
                                    else
                                    {
                                        foreach (string s in FillSensVector(8 + ishiftconf, args).Keys)
                                        {
                                            try { SensAnlysis[k].Add(s, FillSensVector(8 + ishiftconf, args)[s]); }
                                            catch (Exception e) { throw new ArgumentException("Error: Argument defined twice, one time in the Configuration File and one tim on the command line."); }
                                        }
                                    }
                                }

                            }

                            break;
                        }
                    }

                case "-sim":
                    {
                        DisplayRuns = null;
                        SkipRuns = null;
                        OutputSelectedDates = null;
                        SkipOverrideForRuns = null;

                        ManParVals = null;
                        SiteParVals = null;
                        SoilParVals = null;
                        NonVarietalPars = null;
                        //GlobNonVarietalPars = null;
                        VarietalPars = null;
                        SoilLayers = null;
                        ManByDate = null;
                        ManByGS = null;

                        GetArgumentsConfig(args, isConfigAlone, false);

                        ByPassSimulation = false;
                        ByPassTranslation = true;
                        isSensAnalysis = false;


                        OutputDailyFiles = bool.Parse(args[1 + ishiftconf]);
                        ShowProgressSimu = bool.Parse(args[2 + ishiftconf]);
                        ProjectFilePath = args[3 + ishiftconf];



                        for (int i = 4 + ishiftconf; i < args.Length; i++)
                        {
                            int j = i + 1;
                            int k = j + 1;

                            if (args[i] == "+Dates")
                            {
                                OutputSelectedDates = new List<DateTime>();
                                int l = i + 1;
                                while (args[l] != "Dates+") { OutputSelectedDates.Add(DateTime.Parse(args[l])); l++; }
                            }

                            if (args[i] == "--Run")
                            {
                                DisplayRuns = new List<string>();
                                int ir = i + 1;
                                while (!args[ir].Contains("--"))
                                {
                                    DisplayRuns.Add(args[ir]);
                                    try { args[ir + 1].Contains("--"); }
                                    catch (Exception e) { break; }
                                    ir++;
                                }
                            }

                            if (args[i] == "--OutPath") OutputPath = args[i + 1];

                            if (args[i] == "--iRun") iToRun = args[i + 1];

                        }

                        break;
                    }

                case "-transim":
                    {
                        DisplayRuns = null;
                        SkipRuns = null;
                        OutputSelectedDates = null;
                        SkipOverrideForRuns = null;

                        ManParVals = null;
                        SiteParVals = null;
                        SoilParVals = null;
                        NonVarietalPars = null;
                        //GlobNonVarietalPars = null;
                        VarietalPars = null;
                        SoilLayers = null;
                        ManByDate = null;
                        ManByGS = null;

                        GetArgumentsConfig(args, isConfigAlone, false);

                        ByPassSimulation = false;
                        ByPassTranslation = false;
                        isSensAnalysis = false;

                        InputACEFile = args[1 + ishiftconf];
                        //SearchKeyWordExpName = args[2 + ishiftconf];
                        isTranslatorQuiet = bool.Parse(args[2 + ishiftconf]);
                        ProjectFolderPath = args[3 + ishiftconf];

                        OutputDailyFiles = bool.Parse(args[4 + ishiftconf]);
                        ShowProgressSimu = !isTranslatorQuiet;
                        ProjectFilePath = args[5 + ishiftconf];


                        for (int i = 3 + ishiftconf; i < args.Length; i++)
                        {
                            int j = i + 1;
                            int k = j + 1;

                            if (args[i] == "+Dates")
                            {
                                OutputSelectedDates = new List<DateTime>();
                                int l = i + 1;
                                while (args[l] != "Dates+") { OutputSelectedDates.Add(DateTime.Parse(args[l])); l++; }
                            }

                        }

                        break;
                    }
            }
        }

        private static void GetArgumentsConfig(string[] args, bool isConfigAlone, bool getConfForSensAnalysis = false)
        {


            //System.Console.WriteLine(args.Length);
            if (args.Length < 1 && isConfigAlone) throw new ArgumentException("SiriusConsole must have a single argument: path to the configuration file to procceed. Or an explicit commad line");

            var fileConfig = args[0];
            //if (!File.Exists(file)) throw new ArgumentException("SiriusConsole first argument must be a valid path to a Sirius project file.");
            if (!File.Exists(fileConfig) && isConfigAlone) throw new ArgumentException("SiriusConsole argument must be a valid path to a Sirius configuration file.");
            else if (File.Exists(fileConfig))
            {

                ConfigFileItem conffii = Deserialize(fileConfig);

                if (isConfigAlone)
                {
                    ByPassSimulation = conffii.General_Config_Items.ByPassSimulation;
                    ByPassTranslation = conffii.General_Config_Items.ByPassTranslation;
                }
                else
                {
                    ByPassSimulation = false;
                    ByPassTranslation = true;
                }
                if (!ByPassTranslation)
                {
                    try
                    {
                        InputACEFile = conffii.ACE_SQ_Translation.InputACEFile;

                        //SearchKeyWordExpName = conffii.ACE_SQ_Translation.SearchKeyStringExpName;
                        isTranslatorQuiet = conffii.ACE_SQ_Translation.isTranslatorQuiet;

                        ProjectFolderPath = conffii.ACE_SQ_Translation.DefaultRefFolder;
                        OptionalProjectFilesPath = conffii.ACE_SQ_Translation.OptionalProjectFilesPath;
                        OptionalWeatherFilesPath = conffii.ACE_SQ_Translation.OptionalWeatherFilesPath;
                        OptionalOutputFilesPath = conffii.ACE_SQ_Translation.OptionalOutputFilesPath;


                        OptionalProjectFileName = conffii.ACE_SQ_Translation.OptionalProjectFileName;
                        OptionalSiteFileName = conffii.ACE_SQ_Translation.OptionalSiteFileName;
                        OptionalSoilFileName = conffii.ACE_SQ_Translation.OptionalSoilFileName;
                        OptionalManagementFileName = conffii.ACE_SQ_Translation.OptionalManagementFileName;
                        OptionalRunFileName = conffii.ACE_SQ_Translation.OptionalRunFileName;
                        OptionalVarietalFileName = conffii.ACE_SQ_Translation.OptionalVarietalPathAndName;
                        OptionalNonVarietalFileName = conffii.ACE_SQ_Translation.OptionalNonVarietalPathAndName;
                        OptionalRunOtpionFileName = conffii.ACE_SQ_Translation.OptionalRunOptionPathAndName;

                    }
                    catch (Exception e)
                    {
                        throw new Exception("You have to provide All necessary inputs to do a translation");
                    }

                }


                if (!ByPassSimulation)
                {
                    if (isConfigAlone)
                    {

                        OutputDailyFiles = conffii.SimuItems.SaveDailyOutput;
                        ShowProgressSimu = conffii.SimuItems.ShowProgressSimu;
                        ProjectFilePath = conffii.SimuItems.ProjectPathAndFile;
                        OutputPath = conffii.SimuItems.OutputPath;
                        isSensAnalysis = conffii.General_Config_Items.isSensAnalysis;
                        iToRun = conffii.SimuItems.iToRun;
                    }
                    else isSensAnalysis = getConfForSensAnalysis;

                    DisplayRuns = conffii.SimuItems.RunsToDisplay;
                    SkipRuns = conffii.SimuItems.RunsToSkip;

                    if (!conffii.SimuItems.areSelectDatesReadInObs) OutputSelectedDates = conffii.SimuItems.OutputSelectedDates;
                    else
                    {
                        OutputSelectedDates = null;
                        ObsDateTime = new List<Tuple<string, DateTime>>();

                        string[] obsFiles = Directory.GetFiles(conffii.SimuItems.pathOfObservations, "*.*", SearchOption.AllDirectories);

                        foreach (string obsdir in obsFiles)
                        {
                            char[] charSeparators = new char[] { '.' };
                            string ext = obsdir.Split(charSeparators, 2)[1];

                            if ((ext == "sqcan" && (conffii.SimuItems.obsFileToRead.Contains("sqcan") || conffii.SimuItems.obsFileToRead.Count == 0)) ||
                                (ext == "sqoln" && (conffii.SimuItems.obsFileToRead.Contains("sqoln") || conffii.SimuItems.obsFileToRead.Count == 0)))
                            {
                                string line;
                                int count = 0;

                                // Read the file and display it line by line.  
                                System.IO.StreamReader file =
                                    new System.IO.StreamReader(@obsdir);
                                while ((line = file.ReadLine()) != null)
                                {
                                    if (count < 5) { count++; continue; }
                                    char[] charSep = new char[] { '\t' };
                                    string man = line.Split(charSep, 10000)[0];
                                    DateTime date = DateTime.Parse(line.Split(charSep, 10000)[5]);

                                    ObsDateTime.Add(new Tuple<string, DateTime>(man, date));


                                    count++;
                                }

                                file.Close();
                            }
                            else if (ext == "sqmat" && (conffii.SimuItems.obsFileToRead.Contains("sqmat") || conffii.SimuItems.obsFileToRead.Count == 0)) {

                                string line;
                                int count = 0;

                                // Read the file and display it line by line.  
                                System.IO.StreamReader file =
                                    new System.IO.StreamReader(@obsdir);
                                while ((line = file.ReadLine()) != null)
                                {
                                    if (count < 5) { count++; continue; }
                                    char[] charSep = new char[] { '\t' };
                                    string man = line.Split(charSep, 10000)[0];
                                    int i = 0;
                                    int LineLength = line.Split(charSep, 10000).Length;

                                    while (i < LineLength)
                                    {
                                        if (i < 5) { i++; continue; }

                                        try { double.Parse(line.Split(charSep, 10000)[i]); i++; }
                                        catch (FormatException e)
                                        {
                                            try
                                            {

                                                DateTime date = DateTime.Parse(line.Split(charSep, 10000)[i]);
                                                ObsDateTime.Add(new Tuple<string, DateTime>(man, date));

                                                i++;
                                            }
                                            catch (FormatException e1) { i++; }
                                        }


                                    }

                                    count++;
                                }

                                file.Close();

                            }
                            else if (ext == "sqos" && (conffii.SimuItems.obsFileToRead.Contains("sqos") || conffii.SimuItems.obsFileToRead.Count == 0))
                            {

                                string line;
                                int count = 0;

                                // Read the file and display it line by line.  
                                System.IO.StreamReader file =
                                    new System.IO.StreamReader(@obsdir);
                                while ((line = file.ReadLine()) != null)
                                {
                                    if (count < 5) { count++; continue; }
                                    char[] charSep = new char[] { '\t' };
                                    string man = line.Split(charSep, 10000)[0];
                                    DateTime date = DateTime.Parse(line.Split(charSep, 10000)[4]);

                                    ObsDateTime.Add(new Tuple<string, DateTime>(man, date));


                                    count++;
                                }

                                file.Close();

                            }
                            else if (ext == "sqphy" && (conffii.SimuItems.obsFileToRead.Contains("sqphy") || conffii.SimuItems.obsFileToRead.Count == 0)) {

                                string line;
                                int count = 0;

                                // Read the file and display it line by line.  
                                System.IO.StreamReader file =
                                    new System.IO.StreamReader(@obsdir);
                                while ((line = file.ReadLine()) != null)
                                {
                                    if (count < 6) { count++; continue; }
                                    char[] charSep = new char[] { '\t' };
                                    string man = line.Split(charSep, 10000)[0];
                                    DateTime date = DateTime.Parse(line.Split(charSep, 10000)[5]);

                                    ObsDateTime.Add(new Tuple<string, DateTime>(man, date));


                                    count++;
                                }

                                file.Close();

                            }
                        }


                    }

                    if (!isSensAnalysis)
                    {
                        try
                        {
                            SkipOverrideForRuns = conffii.SimuItems.ParOverride.SkipOverrideForRuns;
                            VarietalPars = conffii.ConvertToDict(conffii.SimuItems.ParOverride.ListVarietalPars);
                            NonVarietalPars = conffii.ConvertToDict(conffii.SimuItems.ParOverride.ListNonVarietalPars);
                            //GlobNonVarietalPars = conffii.ConvertToDict(conffii.SimuItems.ParOverride.LisGlobtNonVarietalPars);
                            SoilParVals = conffii.ConvertToDict(conffii.SimuItems.ParOverride.ListSoilParVals);
                            SoilLayers = conffii.ConvertToDict(conffii.SimuItems.ParOverride.ListSoilLayersVal);
                            SiteParVals = conffii.ConvertToDict(conffii.SimuItems.ParOverride.ListSitesParVals);
                            ManParVals = conffii.ConvertToDict(conffii.SimuItems.ParOverride.ListManParVals);
                            ManByDate = conffii.ConvertToDict(conffii.SimuItems.ParOverride.ListManByDate);
                            ManByGS = conffii.ConvertToDict(conffii.SimuItems.ParOverride.ListManByGrowthStage);
                        }
                        catch (Exception e)
                        {
                            throw new Exception("For simulation without override add empty <ParOverride> element");
                        }


                    }
                    else
                    {

                        try
                        {
                            sensMode = conffii.SimuItems.SensitivityAnalysis.sensAnalysisMode;
                            if (sensMode != "Vector") SensAnlysis = conffii.ConvertToDict(conffii.SimuItems.SensitivityAnalysis.ListSensAnalysis);
                            else SensAnlysis = conffii.ConvertToDict(conffii.SimuItems.SensitivityAnalysis.ListSensAnalysisVector);
                        }
                        catch (Exception e)
                        {
                            throw new Exception("You have to provide All necessary inputs to do a sensitivity analysis");
                        }


                    }
                }
            }






            #region Old

            //ProjectFilePath = file;
            //if (args.Length > 7) throw new ArgumentException("Too much arguments provided.");
            //if (args.Length >= 2)
            //{
            //    switch (args[1])
            //    {
            //        case OutputSingleRunOption:
            //            OutputSingleRun = true;
            //            break;
            //        case NoOutputSingleRunOption:
            //            OutputSingleRun = false;
            //            break;
            //        default:throw new ArgumentException("The option :" + args[1] + " is not valid.");
            //    }
            //}
            //if (args.Length >= 3)
            //{
            //    switch (args[2])
            //    {
            //        case ShowProgressOption:
            //            ShowProgress = true;
            //            break;
            //        case NoShowProgressOption:
            //            ShowProgress = false;
            //            break;
            //        default: throw new ArgumentException("The option :" + args[2] + " is not valid.");
            //    }
            //}
            //if (args.Length >= 4)
            //{
            //    if (args.Length < 6) { throw new ArgumentException("missing arguments to override parameters"); }
            //    varietyToOverride = args[3];
            //    parameterName = args[4];
            //    parameterValue = Convert.ToDouble(args[5]);
            //}

            #endregion
        }


        private static Dictionary<string, Dictionary<int,Dictionary<string, double>>> MergeDictSoliL(List<Dictionary<string, Dictionary<int, Dictionary<string, double>>>> listDict)
        {
            Dictionary<string, Dictionary<int, Dictionary<string, double>>> result = new Dictionary<string, Dictionary<int, Dictionary<string, double>>>();

            foreach (Dictionary<string, Dictionary<int, Dictionary<string, double>>> dict in listDict)
            {
                foreach (string k in dict.Keys)
                {
                    Dictionary < int, Dictionary<string, double>> toAdd = dict[k];

                    if (!result.ContainsKey(k)) { result.Add(k, toAdd); }
                    else
                    {
                        foreach (int pn in toAdd.Keys) { try { result[k].Add(pn, toAdd[pn]); } catch { throw new Exception("You try to override twice the same parameter"); } };
                    }
                }
            }

            return result;
        }

        private static Dictionary<string, Dictionary<string, double>> MergeDictDouble(List<Dictionary<string, Dictionary<string, double>>> listDict)
        {
            Dictionary<string, Dictionary<string, double>> result = new Dictionary<string, Dictionary<string, double>>();

            foreach (Dictionary<string, Dictionary<string, double>> dict in listDict)
            {
                foreach (string k in dict.Keys)
                {
                    Dictionary<string, double> toAdd = dict[k];

                    if (!result.ContainsKey(k)) { result.Add(k, toAdd); }
                    else
                    {
                        foreach (string pn in toAdd.Keys) { try { result[k].Add(pn, toAdd[pn]); } catch { throw new Exception("You try to override twice the same parameter"); } };
                    }
                }
            }

            return result;
        }

        private static Dictionary<string, Dictionary<string, dynamic>> MergeDictDyn(List<Dictionary<string, Dictionary<string, dynamic>>> listDict)
        {
            Dictionary<string, Dictionary<string, dynamic>> result = new Dictionary<string, Dictionary<string, dynamic>>();

            foreach (Dictionary<string, Dictionary<string, dynamic>> dict in listDict)
            {
                foreach (string k in dict.Keys)
                {
                    Dictionary<string, dynamic> toAdd = dict[k];

                    if (!result.ContainsKey(k)) { result.Add(k, toAdd); }
                    else
                    {
                        foreach (string pn in toAdd.Keys) { try { result[k].Add(pn, toAdd[pn]); } catch { throw new Exception("You try to override twice the same parameter"); } };
                    }
                }
            }

            return result;
        }

        private static Dictionary<string, Dictionary<string, dynamic[]>> MergeDictManByGS(List<Dictionary<string, Dictionary<string, dynamic[]>>> listDict)
        {
            Dictionary<string, Dictionary<string, dynamic[]>> result = new Dictionary<string, Dictionary<string, dynamic[]>>();

            foreach (Dictionary<string, Dictionary<string, dynamic[]>> dict in listDict)
            {
                foreach (string k in dict.Keys)
                {
                    Dictionary<string, dynamic[]> toAdd = dict[k];

                    if (!result.ContainsKey(k)) { result.Add(k, toAdd); }
                    else
                    {
                        foreach (string pn in toAdd.Keys) { try { result[k].Add(pn, toAdd[pn]); } catch { throw new Exception("You try to override twice the same parameter"); } };
                    }
                }
            }

            return result;
        }

        private static Dictionary<string, Dictionary<string, double>> FillDicCrop(int j, int k, string[] args)
        {
            string run = "";
            Dictionary<string, Dictionary<string, double>> result = new Dictionary<string, Dictionary<string, double>>();
            while (!args[j].Contains("--") && j <= args.Length - 2)
            {
                if (args[j].Contains("["))
                {
                    k = j + 1;
                    run = args[j].Substring(1, args[j].Length - 2);
                    result.Add(run, new Dictionary<string, double>());

                    while (!args[k].Contains("[") && k + 1 <= args.Length - 1)
                    {
                        try { result[run].Add(args[k], double.Parse(args[k + 1])); k++; }
                        catch (FormatException e) { k++; }

                    }
                }
                j++;

            }

            return result;
        }


        private static Dictionary<string, Dictionary<string, dynamic>> FillDicDyn(int j, int k, string[] args)
        {
            string run = "";
            Dictionary<string, Dictionary<string, dynamic>> result = new Dictionary<string, Dictionary<string, dynamic>>();
            while (!args[j].Contains("--") && j <= args.Length - 2)
            {
                if (args[j].Contains("["))
                {
                    k = j + 1;
                    run = args[j].Substring(1, args[j].Length - 2);
                    result.Add(run, new Dictionary<string, dynamic>());
                    string skip = "";
                    while (!args[k].Contains("[") && k + 1 <= args.Length - 1)
                    {
                        try
                        {
                            result[run].Add(args[k], (double)double.Parse(args[k + 1]));
                        }
                        catch (FormatException e0)
                        {
                            try
                            {
                                result[run].Add(args[k], (bool)bool.Parse(args[k + 1]));
                            }
                            catch (FormatException e1)
                            {
                                try
                                {
                                    result[run].Add(args[k], (DateTime)DateTime.Parse(args[k + 1]));
                                }
                                catch (FormatException e2)
                                {
                                    if (args[k] != skip) result[run].Add(args[k], (string)args[k + 1]);
                                }

                            }
                        }

                        skip = args[k + 1];
                        k++;

                    }

                }
                j++;
            }
            return result;
        }

        private static Dictionary<string,Dictionary<int, Dictionary<string, double>>> FillDicSoilL(int j, int k, string[] args)
        {
            Dictionary<string, Dictionary<int, Dictionary<string, double>>> res = new Dictionary<string, Dictionary<int, Dictionary<string, double>>>();
            string run = "";

            while (!args[j].Contains("--") && j< args.Length-1)
            {
                if (args[j].Contains("["))
                {
                    k = j + 1;
                    run = args[j].Substring(1, args[j].Length - 2);
                    res.Add(run, new Dictionary<int, Dictionary<string, double>>());
                    while (!args[k].Contains("--") && !args[k].Contains("[") && k < args.Length-1)
                    {
                        int l = 0;
                        if (args[k].Contains("{"))
                        {
                            l= int.Parse(args[k].Substring(1, args[k].Length - 2));
                            res[run].Add(l, new  Dictionary<string, double>());
                            int m = k + 1;
                            while (!args[m].Contains("--") && !args[m].Contains("[") && !args[m].Contains("{"))
                            {
                                string par = args[m];
                                res[run][l].Add(par, 0.0);
                                double val= Double.Parse(args[m+1]);
                                res[run][l][par] =val;

                                m=m+2;
                                if (m >= args.Length - 1) break;
                            }
                        }
                        k++;
                    }
                }
                j++;
            }

            return res;

        }

        private static Dictionary<string, Dictionary<string, dynamic[]>> FillDicManGS(int j, int k, string[] args)
        {
            string run = "";
            k = j;
            Dictionary<string, Dictionary<string, dynamic[]>> result = new Dictionary<string, Dictionary<string, dynamic[]>>();
            while (!args[j].Contains("--") && j <= args.Length - 2)
            {


                //if (args[j].Contains("["))
                //{



                run = args[k].Substring(1, args[k].Length - 2);
                result.Add(run, new Dictionary<string, dynamic[]>());

                bool stop = false;
                int iGS = k + 1;
                int istop = iGS;
                while (!stop)
                {
                    

                    dynamic[] tabVal = new dynamic[3];
                    if (args[iGS].Contains("{") && !args[iGS].Contains("--"))
                    {
                        int itab = 0;

                        string GS = args[iGS].Substring(1, args[iGS].Length - 2);



                        while (!args[iGS+1].Contains("{") && !args[iGS+1].Contains("[") && !args[iGS+1].Contains("--"))
                        {
                            iGS++;
                            try
                            {
                                itab++;
                                tabVal[itab] = (double)double.Parse(args[iGS]);
                            }
                            catch (FormatException e0)
                            {
                                itab = 0;
                                tabVal[itab] = (string)args[iGS];

                            }

                            try { args[iGS + 1].Contains("{"); }
                            catch (Exception e1) { break; }
                        }
                        result[run].Add(GS, tabVal);
                        iGS++;
                    }

                    try { args[iGS].Contains("{"); }
                    catch (Exception e1) { break; }
                    if (args[iGS+1].Contains("[") || args[iGS+1].Contains("--") || istop> args.Length) stop = true;
                    istop++;
                }
                //k++;
                //}
                //}
                j++;
                k = k + 1;
                while (!args[k].Contains("[")) { k++; try { args[k].Contains("["); }
                    catch (Exception e1) { break; }
                    
                }
                if (args[k-1].Contains("--")) { break; }
                try { args[k].Contains("["); }
                catch (Exception e1) { break; }
            }

            return result;
        }




        private static Dictionary<string, dynamic[]> FillSensVector(int i, string[] args)
        {
            string par = "";
            Dictionary<string, dynamic[]> result = new Dictionary<string, dynamic[]>();

            while (i < args.Length)
            {
                if (args[i] == "+Dates") while (args[i] != "Dates+") { i++; continue; }
                if (args[i] == "Dates+") i++;

                    try { double.Parse(args[i]); }
                catch
                {
                    int k = i + 1;
                    int count = 0;
                    par = args[i];
                    bool isInt = true;

                    while (isInt)
                    {
                        count++;
                        if (k < args.Length)
                        {
                            try
                            {
                                double.Parse(args[k]);

                                isInt = true;

                            }
                            catch (FormatException e0) { isInt = false; }
                        }
                        else isInt = false;
                        k++;
                    }

                    k = i + 1;
                    dynamic[] tab = new dynamic[count-1];
                    isInt = true;

                    while (isInt)
                    {
                        if (k < args.Length)
                        {
                            try
                            {
                                tab[k-i-1] = (double)double.Parse(args[k]);

                                isInt = true;

                            }
                            catch (FormatException e0) { isInt = false; }
                        }
                        else isInt = false;
                        k++;

                    }
                    result.Add(par, tab);
                    i += count;
                }

            }
                    return result;
        }

        private static Dictionary<string, dynamic[]> FillSens(int i,string[] args)
        {
            string par = "";
            Dictionary<string, dynamic[]> result = new Dictionary<string, dynamic[]>();
            int count = 0;

            while (i < args.Length)
            {

                if (args[i] == "+Dates") while (args[i] != "Dates+") { i++; continue; }
                if (args[i] == "Dates+") i++;

                    if (!args[i].Contains("["))
                {

                    try { double.Parse(args[i]); }
                    catch
                    {
                        int k = i + 1;
                        bool isInt = true;

                        while (isInt)
                        {
                            count++;
                            if (k < args.Length)
                            {
                                if (args[k].Contains("[")) isInt = true;
                                else
                                {
                                    try
                                    {
                                        double.Parse(args[k]);

                                        isInt = true;

                                    }
                                    catch (FormatException e0) { isInt = false; }
                                }
                            }
                            else isInt = false;
                            k++;
                        }
                    }
                }

                if (!args[i].Contains("["))
                {

                    try { double.Parse(args[i]); }
                    catch
                    {
                        int k = i + 1;
                        par = args[i];
                        bool isInt = true;

                        dynamic[] tab = new dynamic[count - 1];
                        isInt = true;

                        while (isInt)
                        {
                            if (k < args.Length)
                            {
                                if (args[k].Contains("["))
                                {
                                    tab[k - i - 1] = (string)args[k].Substring(1, args[k].Length - 2);

                                    isInt = true;
                                }
                                else
                                {
                                    try
                                    {
                                        if(k - i - 1==1) tab[k - i - 1] = (int)int.Parse(args[k]);
                                        else tab[k - i - 1] = (double)double.Parse(args[k]);

                                        isInt = true;

                                    }
                                    catch (FormatException e0) { isInt = false; }
                                }

                            }
                            else isInt = false;
                            k++;

                        }
                        result.Add(par, tab);
                        i += count;
                        count = 0;
                    }
                }
            }

            return result;
        }

            private static void LoadProjectFile()
        {
            try
            {
                //Console.WriteLine("Project File: "+ProjectFilePath);
                ProjectFile.Load(ProjectFilePath, false);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Couldn't load the project file.", e);
            }
        }

        //private static void RunProjectFile(string varietyToOverride = null, string parameterName = null, double parameterValue =0)
        private static void RunProjectFile(List<string> SkipOverrideForRuns,
                                           Dictionary<string, Dictionary<string, double>> VarietalPars,
                                           Dictionary<string, Dictionary<string, double>> NonVarietalPars,
                                           //Dictionary<string, Dictionary<string, double>> GlobNonVarietalPars,
                                           Dictionary<string, Dictionary<string, dynamic>> SoilParVals,
                                           Dictionary<string, Dictionary<int, Dictionary<string, double>>> SoilLayers,
                                           Dictionary<string, Dictionary<string, dynamic>> SiteParVals,
                                           Dictionary<string, Dictionary<string, dynamic>> ManParVals,
                                           Dictionary<string, Dictionary<DateTime, dynamic[]>> ManByDate,
                                           Dictionary<string, Dictionary<string, dynamic[]>> ManByGS, List<DateTime> OutputSelectedDates, List<Tuple<string, DateTime>> ObsDateTime)
        {
            //ProjectHelper.ForEachMultiRun("Running " + ProjectFilePath, true, OutputSingleRun, null, null, null, null, ShowProgress, varietyToOverride,parameterName,parameterValue);
            ProjectHelper.ForEachMultiRun("Running " + ProjectFilePath, true, OutputDailyFiles, null, OutputPath, DisplayRuns, SkipRuns, null, ShowProgressSimu/*, SkipOverrideForRuns*/,
                                          VarietalPars, NonVarietalPars, SoilParVals, SoilLayers, SiteParVals,ManParVals,
                                          ManByDate, ManByGS, OutputSelectedDates, ObsDateTime, /*GlobNonVarietalPars,*/iToRun,0);

        }
        private static void RunSensivityAnalysis(List<string> SkipOverrideForRuns, Dictionary<string, Dictionary<string, dynamic[]>> SensAnlysis,List<DateTime> OutputSelectedDates, List<Tuple<string, DateTime>> ObsDateTime)
        {

            //ProjectHelper.ForEachMultiRun("Running " + ProjectFilePath, true, OutputSingleRun, null, null, null, null, ShowProgressSimu, SkipOverrideForRuns,
            //                              VarietalPars, NonVarietalPars, SoilParVals, SoilLayers, SiteParVals, ManParVals,
            //                              ManByDate, ManByGS);

            ProjectHelper.ForEachSensRun("Running " + ProjectFilePath, true, OutputDailyFiles, null, OutputPath, DisplayRuns, SkipRuns, ShowProgressSimu, null,
                                           SensAnlysis, OutputSelectedDates, ObsDateTime);
        }

        internal static void WriteXML(ConfigFileItem conffi)
        {
            string output = "C:/SQ_Release/1-Project/Config_Simulation.sqconf";

            
            XmlSerializer x = new XmlSerializer(typeof(ConfigFileItem));
            TextWriter writer = new StreamWriter(output);
            x.Serialize(writer, conffi);
            writer.Close();
            

        }

        internal static ConfigFileItem Deserialize(string file)
        {

            XmlSerializer x = new XmlSerializer(typeof(ConfigFileItem));
            StreamReader reader = new StreamReader(file);
            return (ConfigFileItem)x.Deserialize(reader);


        }


    }
}
