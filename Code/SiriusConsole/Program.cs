using System;
using System.IO;
using SiriusModel;
using SiriusModel.InOut;

namespace SiriusConsole
{
    class Program
    {
        private const string OutputSingleRunOption = "/OutputSingleRun";
        private const string ShowProgressOption = "/ShowProgress";
        private const string NoOutputSingleRunOption = "/NoOutputSingleRun";
        private const string NoShowProgressOption = "/NoShowProgress";

        

        private static bool OutputSingleRun = false;
        private static bool ShowProgress = false;
        private static string ProjectFilePath;
        private static string varietyToOverride;
        private static string parameterName;
        private static double parameterValue;

        static void Main(string[] args)
        {
            
            try
            {
                /*string[] argtest = new string[6];
                argtest[0] ="C:/Users/mouginot/Desktop/projet pour pierre/1-Project/Project.sqpro";
                argtest[1] = "/OutputSingleRun";
                argtest[2] = "/ShowProgress";
                argtest[3] = "Bacanora88";
                argtest[4] = "AreaPL";
                argtest[5] = "31";*/
                GetArguments(args);
                
                LoadProjectFile();
                RunProjectFile(varietyToOverride , parameterName , parameterValue);
               
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("SiriusConsole \"path to project file\" /(No)ShowProgress  /(No)OutputSingleRun [varietyToOverride] [parameterName] [parameterValue]");
            }
        }

        private static void GetArguments(string[] args)
        {
            if (args.Length < 1) throw new ArgumentException("SiriusConsole must have at least one argument: path to the project file to procceed.");
            var file = args[0];
            if (!File.Exists(file)) throw new ArgumentException("SiriusConsole first argument must be a valid path to a Sirius project file.");
            ProjectFilePath = file;
            if (args.Length > 7) throw new ArgumentException("Too much arguments provided.");
            if (args.Length >= 2)
            {
                switch (args[1])
                {
                    case OutputSingleRunOption:
                        OutputSingleRun = true;
                        break;
                    case NoOutputSingleRunOption:
                        OutputSingleRun = false;
                        break;
                    default:throw new ArgumentException("The option :" + args[1] + " is not valid.");
                }
            }
            if (args.Length >= 3)
            {
                switch (args[2])
                {
                    case ShowProgressOption:
                        ShowProgress = true;
                        break;
                    case NoShowProgressOption:
                        ShowProgress = false;
                        break;
                    default: throw new ArgumentException("The option :" + args[2] + " is not valid.");
                }
            }
            if (args.Length >= 4)
            {
                if (args.Length < 6) { throw new ArgumentException("missing arguments to override parameters"); }
                varietyToOverride = args[3];
                parameterName = args[4];
                parameterValue = Convert.ToDouble(args[5]);
            }
        }

        private static void LoadProjectFile()
        {
            try
            {
                ProjectFile.Load(ProjectFilePath);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Couldn't load the project file.", e);
            }
        }

        private static void RunProjectFile(string varietyToOverride = null, string parameterName = null, double parameterValue =0)
        {
            ProjectHelper.ForEachMultiRun("Running " + ProjectFilePath, true, OutputSingleRun, null, null, null, null, ShowProgress, varietyToOverride,parameterName,parameterValue);
        }
    }
}
