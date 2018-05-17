using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SiriusModel.InOut;
using SiriusModel.InOut.OutputWriter;

namespace GlobalSA_Morris
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Global sensitivity analysis is starting, please wait...");//The procedure of GSA is starting.

            #region Working directory of the model.
            //int numberOfRandomLines = 860;
            //int numberOfParameters = 85; //For 85 parameters.

            int numberOfRandomLines = 770;
            int numberOfParameters = 76;//For 76 parameters.

            string projectDirectory = @"C:\Postdoc Research\INRA\SiriusQuality1.5\Data\v1.5-Project\";
            string projectFileName = @"v1.5-SensitivityAnalysis.sqpro";//The name of the run file of sensitivity analysis.
            string projectPath = projectDirectory + projectFileName;
            //Console.WriteLine(projectPath);

            string GSADirectory = @"C:\Postdoc Research\INRA\SiriusQuality1.5\Data\INRA-BBSRC 16cv project\Sensitivity Analysis\";//GSA=global sensitivity analysis

            string inputDirectory = @"Morris\";
            //string RandomInputFileName = @"All Parameter-Morris-860.txt";
            string RandomInputFileName = @"76 Parameter-Morris.txt"; //It was changed to 76 parameters now.
            string RandomInputFilePath = GSADirectory + inputDirectory + RandomInputFileName;
            //Console.WriteLine(RandomInputFileName);

            string outputDirectory = @"Morris\";
            string outputFileName = "ModelOutput_Morris_";
            string[] treatmentNames = new string[] { "AV-HighN", "AV-LowN", "CF-HighN", "CF-LowN", "RR-HighN", "RR-LowN" };
            string outputFileExtention = ".txt";

            string[] wholeOutputFileNames = new string[treatmentNames.Length];

            for (int i = 0; i < wholeOutputFileNames.Length; i++)
            {
                string wholeOutputFileName = GSADirectory + outputDirectory + outputFileName + treatmentNames[i] + outputFileExtention;//Assign the path of the text file to save the temporary model outputs concerned.
                File.WriteAllText(wholeOutputFileName, "");//Clear the temp ouput file that contains model outputs concerned.
                wholeOutputFileNames[i] = wholeOutputFileName;
                //Console.WriteLine(wholeOutputFileNames[i]);
            }
            #endregion

            ReadTextFile readTextFile = new ReadTextFile();
            string[,] randoms = readTextFile.ReadText(RandomInputFilePath, numberOfRandomLines, numberOfParameters);//Read the input file of random parameter sets.

            int usedRandomLines = randoms.GetLength(0);//The integer to show how many model runs will be conducted in current simulation.
            //int usedRandomLines = 200;

            for (int i = 0; i < usedRandomLines; i++)
            {
                #region 1. Load random parameter set.
                List<double> inputParameterValues = new List<double>();

                for (int j = 0; j < randoms.GetLength(1); j++)
                {
                    double temParameterValue = Convert.ToDouble(randoms[i, j]);
                    inputParameterValues.Add(temParameterValue);
                }
                #endregion

                #region 2. Run the model.
                AutomaticModelRun.ModelRun(inputParameterValues, GSADirectory, projectPath, wholeOutputFileNames);
                inputParameterValues.Clear();//Run the project.
                #endregion

                Console.WriteLine("Parameter set {0} is finished, please wait...", i + 1);
            }

            Console.WriteLine("Global Sensitivity Analysis is finished!");
            //Console.ReadLine();
        }
    }
}
