using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.IO;
using SiriusModel.InOut;
using SiriusModel.InOut.OutputWriter;
using SiriusModel.Model.CropModel;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;


namespace GeneticAlgorithms
{

    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            #region 1. Starting information

            #region 1.1 Working directory of the model.
           // Directory on He's desktop computer
           // string dataDirectory = @"C:\Postdoc Research\INRA\SiriusQuality1.5\sema_sirius2009\Data\";
           // Directory on Strato's computer
           // string dataDirectory = @"Z:\sema_sirius2009\Data\";
            string dataDirectory = @"C:\Documents and Settings\ygualtieri\Bureau\Data(back)\";

            // To run the program on several cores, change the folder number [(1), (2), etc.]
            string projectFolder = @"INRA-BBSRC 16cv project (1)\";
            string projectFile = @"INRA-BBSRC-16cv (GeneticAlgorithms).sqpro";
            
            string workDirectory = dataDirectory + projectFolder;
            string projectDirectory = dataDirectory + projectFolder + projectFile;

            string genericAlgorithmsDirectory = @"Genetic Algorithms\";

            string resultsDirectory = @"Calibration Results\";
            #endregion

            #region 1.2 Starting and ending numbers of calibration rounds

            // The two lines below are for cross-validation RMSE computation (after Wallach et al., 2001, Agron. J)
            //string[,] phenologyAllExceptions = { { "2007", "CF", "HN" }, { "2008", "CF", "HN" }, { "2007", "JIC", "HN" }, { "2008", "JIC", "HN" }, { "2007", "MONS", "HN" }, { "2008", "MONS", "HN" }, { "2007", "UNOT", "HN" }, { "2008", "UNOT", "HN" } };
            //int testEndingRoundNumber = phenologyAllExceptions.GetLength(0);
            
            // Number of independent rounds of parameter estimation
            int testStartingRoundNumber = 1;
            // Comment this line for cross-validation RMSEP computation 
            int testEndingRoundNumber = 3 /*10*/;
            //Multiple rounds of calibrations will be conducted and then select the best ones among these calibration results.
            #endregion

            #endregion

            for (int testIndex = testStartingRoundNumber; testIndex <= testEndingRoundNumber; testIndex++)
            {
                #region 2. Set up variety file and parameter files...
                XmlDocument myXMLEdit1 = new XmlDocument();
                XMLEdit myXMLEdit2 = new XMLEdit();

                #region 2.1 Set up Parameter file.
                string projectName = "INRA-BBSRC-16cv";
                string templateParameterFileName = projectName + " (Template)";
                string templateParameterFileExtension = ".sqpar";
                string wholeTemplateParameterFilePath = workDirectory + genericAlgorithmsDirectory + templateParameterFileName + templateParameterFileExtension;

                string currentParameterFileName = projectName + " (GeneticAlgorithms)" + "_" + testIndex;//Change for multiple rounds of test to see the reliability of the calibration results.
                string currentParameterFileExtension = ".sqpar";
                string wholeCurrentParameterFilePath = workDirectory + currentParameterFileName + currentParameterFileExtension;

                myXMLEdit1.Load(wholeTemplateParameterFilePath);
                myXMLEdit1.Save(wholeCurrentParameterFilePath);
                //Load the parameter file.
                #endregion

                #region 2.2 Set up Variety file.
                string templateVarietyFileName = projectName + " (Template)";
                string templateVarietyFileExtension = ".sqvar";
                string wholeTemplateVarietyFilePath = workDirectory + genericAlgorithmsDirectory + templateVarietyFileName + templateVarietyFileExtension;

                string currentVarietyFileName = projectName + " (GeneticAlgorithms)" + "_" + testIndex;//Change for multiple rounds of test to see the reliability of the calibration results.
                string currentVarietyFileExtension = ".sqvar";
                string wholeCurrentVarietyFilePath = workDirectory + currentVarietyFileName + currentVarietyFileExtension;

                myXMLEdit1.Load(wholeTemplateVarietyFilePath);
                myXMLEdit1.Save(wholeCurrentVarietyFilePath);
                //Load the variety file.
                #endregion

                #region Set up Soil file.
                //Soil parameters were not calibrated.
                //string templateSoilFileName = projectName + " (Template)";
                //string templateSoilFileExtension = ".sqsoi";
                //string wholeTemplateSoilFilePath = workDirectory + genericAlgorithmsDirectory + templateSoilFileName + templateSoilFileExtension;

                //string currentSoilFileName = projectName + " (GeneticAlgorithms)";
                //string currentSoilFileExtension = ".sqsoi";
                //string wholeCurrentSoilFilePath = workDirectory + currentSoilFileName + currentSoilFileExtension;

                //myXMLEdit1.Load(wholeTemplateSoilFilePath);
                //myXMLEdit1.Save(wholeCurrentSoilFilePath);
                ////Load the soil file.
                #endregion

                #endregion

                #region 3. Run the cultivars one by one.
                // If one wants to estimate the paramters of different cultivars on different cores, then specify here which cultivar 
                // should be estimated on the current core
                int startingCultivarID_For_Calibration = 1;
                int endingCultivarID_For_Calibration = 4;//Which c ultivars to be calibrated? e.g. 01-ALC, 02-BEA, etc.

                for (int cultivarIDs = (startingCultivarID_For_Calibration - 1); cultivarIDs < endingCultivarID_For_Calibration; cultivarIDs++)
                {
                    #region 3.1 Starting calibration...
                    Console.WriteLine("Calibration of cultivar " + (cultivarIDs + 1) + " is starting, please wait...");//The procedure of calibration is starting.
                    Population 
                        TestPopulation = new Population(); //Create an instance of population.
                    #endregion

                    #region 3.2 Simulation control parameters.

                    int totalNumberofGeneration = 6/*75*/; // Total number of generations for evolution.

                    TestPopulation.kInitialPopulation = 2000; //Number of species in the initial generation.
                    TestPopulation.kPopulationLimit = 20; //Number of residual (survivor) genomes in the following generation. It was reduced from 40 to 20 since more genomes could be generated if it is stuck for more generations.
                    TestPopulation.CurrentPopulation = TestPopulation.kInitialPopulation; //Assign values to the variables in the Population class.
                    TestPopulation.kTotalNumberofGeneration = totalNumberofGeneration;

                    TestPopulation.kLocalPopulation = TestPopulation.kPopulationLimit / 2; // Size of the population for hill climbing as a fraction of the survival genomes
                    TestPopulation.kLocalParameterRatio = 0.05; // Range of parameters for local hill climbing as fraction of the initial range (+/-5%)

                    TestPopulation.kMin = 0;
                    TestPopulation.kMax = 100;//The two parameters for random value generation.

                    double rangeRatio = 1.0;//Set the ratio of the parameter ranges.
                    double newRangeRatio;
                    #endregion

                    #region 3.3 Cultivar information (the total cultivars to be calibrated?)
                    string[] cultivarNumbers = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" };
                    string[] cultivarNames = { "01-ALC", "02-BEA", "03-CON", "04-PAR", "05-RIA", "06-ROB", "07-SAV", "08-SOI", "09-ARC", "10-CF91", "11-CF99", "12-PER", "13-QUE", "14-REC", "15-REN", "16-TOI" };
                    int cultivarID;//Run cultivar 14 if the index of cultivarNumbers is 13.

                    cultivarID = cultivarIDs;
                    string[] currentCultivarNumbers = { cultivarNumbers[cultivarID] };//Run all of the 16 cultivars.
                    string[] currentCultivarNames = { cultivarNames[cultivarID] };
                    #endregion

                    #region 3.4 Treatment information (the total treatment to be calibrated.)
                    string[] Years = { "2007", "2008" };
                    string[] Sites = { "CF", "JIC", "MONS", "UNOT" };
                    string[] nitrogenTreatments = { "HN", "LN" };

                    string[] managementItems ={"CF07-HN","CF07-LN","CF08-HN","CF08-LN","JIC07-HN","JIC07-LN","JIC08-HN","JIC08-LN",
                                          "MONS07-HN","MONS07-LN","MONS08-HN","MONS08-LN","UNOT07-HN","UNOT07-LN","UNOT08-HN","UNOT08-LN"};
                    string[] siteItems = { "CF", "JIC", "MONS", "UNOT" };
                    string[] soilItems = { "CF07 (MG6B)", "CF08 (CR4)", "JIC07", "JIC08", "MONS07 (C4)", "MONS08 (C2)", "UNOT07", "UNOT08" };
                    string currentVarietyName = cultivarNames[cultivarIDs];
                    string[] varieyItems = { currentVarietyName };//Use different cultivar type for each calibration.

                    string[][] runItems = { managementItems, siteItems, soilItems, varieyItems };
                    #endregion

                    #region 3.5 Submodel ID of Calibration (Which parameters and observations will be calibrated in the submodel?)
                    //string[] calibrationRoundIDs = { "Phenology", "LLE", "LLE1", "LLE2", "LUE", "NAllocation", "SoilDrought"};
                    //Totally 6 rounds of calibration will be conducted. LLE1 is based on leaf layer profile at anthesis, while LLE2 is based on leaf dynamic at different dates.

                    // Specify here the sub-model to calibrate
                    string[] calibrationRoundIDs = { "Phenology" };
                    int calibrationRoundNumber = calibrationRoundIDs.Length;

                    int startingRoundID_For_Calibration = 1;
                    int endingRoundID_For_Calibration = calibrationRoundIDs.Length;//Which submodels to be calibrated? e.g. Phenology, LLE, LUE, NAllocation, and SoilDrought.
                    #endregion

                    #region 3.6 Calibration information of each submodel

                    #region 3.6.0 The column number of the observation variables in the Excel file.
                    int anthesisDateID = 6;
                    int finalLeafNumberID = 7; //Anthesis date and final leaf number were saved in column 6 and 7 seperately in "End-season" spreadsheet.

                    int leafAreaIndexID = 20;//The LAI is saved in the 20th column of the "Time-series" spreadsheet,and so on.
                    int cropDMID = 18;
                    int cropNID = 27;

                    int LAI_FLNIndexID = 6;//The LAI of different layers at anthesis are saved from  "Time-series" spreadsheet,and so on.
                    int LAI_FLN_1IndexID = 7;
                    int LAI_FLN_2IndexID = 8;
                    int LAI_FLN_3IndexID = 9;
                    int LAI_FLN_4IndexID = 10;

                    //The column ID for the observation variables that are save in the Excel file.
                    #endregion

                    #region 3.6.1. Phenology calibration infromation
                    string[] phenologyCalibrationYears = { "2007", "2008" };//Year, alphabetically.
                    string[] phenologyCalibrationSites = { "CF", "JIC", "MONS", "UNOT" };//Site, alphabetically.
                    string[] phenologyCalibrationNitrogenTreatments = { "HN" };//Nitrogen treatment, alphabetically.

                    // Comment this line to compute the cross-validation RMSEP
                    string[,] phenologyException = { { } };
                    
                    // Uncomment this line to compute the cross-validation RMSEP
                    //string[,] phenologyException = { { phenologyAllExceptions[(testIndex - 1), 0], phenologyAllExceptions[(testIndex - 1), 1], phenologyAllExceptions[(testIndex - 1), 2] } };

                    int[] phenologyObservationIDs = { anthesisDateID, finalLeafNumberID };
                    //The column numbers of observation variables in the spreadsheet.

                    double phenologyFitnessShresholdForLoacalSearch = 0.7;
                    int phenologyAmplificationFactor = 1;
                    double phenologyCV = 0.1; //The CV value used in the fitness value calculation.
                    #endregion

                    #region 3.6.2 Leaf layer expansion (LLE) calibration based on both leaf layer profile at anthesis and LAI dynamic with 6 parameters.
                    string[] LLECalibrationYears = { "2007", "2008" };//Year, alphabetically.
                    string[] LLECalibrationSites = { "CF", "UNOT" };//Site, alphabetically.
                    string[] LLECalibrationNitrogenTreatments = { "HN" };//Nitrogen treatment, alphabetically.

                    //string[,] LLEException = { { } };
                    string[,] LLEException = { { "2007", "CF", "HN" } };
                    //Treatment that will not be involved.

                    int[] LLEObservationIDs_1 = { LAI_FLNIndexID, LAI_FLN_1IndexID, LAI_FLN_2IndexID, LAI_FLN_3IndexID, LAI_FLN_4IndexID }; //Column numbers for leaf profile data.
                    int[] LLEObservationIDs_2 = { leafAreaIndexID }; //Column numbers for LAI dynamic.
                    //The column numbers of observation variables in the spreadsheet. Two kinds of observations involved in this study.

                    double LLEFitnessShresholdForLoacalSearch = 0.00;
                    int LLEAmplificationFactor = 10;
                    double LLECV = 0.2; //The CV value used in the fitness value calculation.
                    #endregion

                    #region 3.6.2.1 Leaf layer expansion (LLE1) calibration based on leaf layer profile at anthesis.
                    string[] LLE1CalibrationYears = { "2007", "2008" };//Year
                    string[] LLE1CalibrationSites = { "CF", "UNOT" };//Site
                    string[] LLE1CalibrationNitrogenTreatments = { "HN" };//Nitrogen treatment.

                    string[,] LLE1Exception = { { "2007", "CF", "HN" } };
                    //Treatment that will not be involved. The exceptions are CF07-HN and UNOT08-HN.

                    int[] LLE1ObservationIDs = { LAI_FLNIndexID,  LAI_FLN_1IndexID, LAI_FLN_2IndexID, LAI_FLN_3IndexID, LAI_FLN_4IndexID};
                    //The column numbers of observation variables in the spreadsheet.For leaf layer profile, 5 top leaves were involved.

                    double LLE1FitnessShresholdForLoacalSearch = 0.00;
                    int LLE1AmplificationFactor = 10;
                    double LLE1CV = 0.1; //The CV value used in the fitness value calculation.
                    #endregion

                    #region 3.6.2.2 Leaf layer expansion (LLE2) calibration based on leaf layer profile at anthesis.
                    string[] LLE2CalibrationYears = { "2007", "2008" };//Year
                    string[] LLE2CalibrationSites = { "CF", "UNOT" };//Site
                    string[] LLE2CalibrationNitrogenTreatments = { "HN", "LN" };//Nitrogen treatment.

                    string[,] LLE2Exception = { { } };
                    //Treatment that will not be involved.

                    int[] LLE2ObservationIDs = { leafAreaIndexID };
                    //The column numbers of observation variables in the spreadsheet.

                    double LLE2FitnessShresholdForLoacalSearch = 0.00;
                    int LLE2AmplificationFactor = 10;
                    double LLE2CV = 0.3; //The CV value used in the fitness value calculation.
                    #endregion

                    #region 3.6.3. Light use efficiency (LUE) calibration infromation
                    string[] LUECalibrationYears = { "2007" };//Year
                    string[] LUECalibrationSites = { "CF", "JIC", "MONS", "UNOT" };//Site
                    //string[] LUECalibrationSites = { "UNOT" };//Site
                    string[] LUECalibrationNitrogenTreatments = { "HN", "LN" };//Nitrogen treatment.

                    //string[,] LUEException = { { "2007", "CF", "HN" }, { "2008", "UNOT", "HN" } };//Treatment that will not be involved. The exception is CF07.
                    string[,] LUEException = { { } };//Treatment that will not be involved. The exception is CF07.

                    int[] LUEObservationIDs = { cropDMID };//The column numbers of observation variables in the spreadsheet.

                    double LUEFitnessShresholdForLoacalSearch = 0.0001;
                    int LUEAmplificationFactor = 10;
                    double LUECV = 0.1; //The CV value used in the fitness value calculation.
                    #endregion

                    #region 3.6.4. NAllocation (Nitrogen Allocation) calibration infromation
                    string[] NAllocationCalibrationYears = { "2007" };//Year
                    string[] NAllocationCalibrationSites = { "CF", "JIC", "MONS", "UNOT" };//Site
                    string[] NAllocationCalibrationNitrogenTreatments = { "HN", "LN" };//Nitrogen treatment

                    //string[,] NAllocationException = { { "2007", "CF", "HN" }, { "2007", "CF", "LN" }, { "2008", "UNOT", "HN" }, { "2008", "UNOT", "LN" } };
                    string[,] NAllocationException = { {  } };//Treatment that will not be involved. No exception here.

                    int[] NAllocationObservationIDs = { cropNID };//The column numbers of observation variables in the spreadsheet.

                    double NAllocationFitnessShresholdForLoacalSearch = 0.0;
                    int NAllocationAmplificationFactor = 10; // Change this value if the fitness value is too small
                    double NAllocationCV = 0.1; //The CV value used in the fitness value calculation.
                    #endregion

                    #region 3.6.5. SoilDrought(Soil drought factor) calibration infromation
                    string[] soilDroughtCalibrationYears = { "2007" };//Year
                    string[] soilDroughtCalibrationSites = { "CF", "JIC", "MONS", "UNOT" };//Site
                    string[] soilDroughtCalibrationNitrogenTreatments = { "HN", "LN" };//Nitrogen treatment

                    string[,] soilDroughtException = { { } };//Treatment that will not be involved. No exception here.

                    int[] soilDroughtObservationIDs = { leafAreaIndexID };//The column numbers of observation variables in the spreadsheet.

                    double soilDroughtFitnessShresholdForLoacalSearch = 0.0;
                    int soilDroughtAmplificationFactor = 100;
                    double soilDroughtCV = 0.1; //The CV value used in the fitness value calculation.
                    #endregion
                    #endregion

                    #region 3.7 Calibration of submodels, such as Phenology, LLE, LUE, NAllocation, and SoilDrought...

                    for (int roundID = (startingRoundID_For_Calibration - 1); roundID < endingRoundID_For_Calibration; roundID = roundID + 1)
                    {
                        string calibrationRoundID = calibrationRoundIDs[roundID];
                        TestPopulation.calibrationRoundID = calibrationRoundID;
                        TestPopulation.cultivarID = currentCultivarNames[0];

                        #region 3.7.1. Load treatment information in each round of calibration
                        ArrayList currentYears = new ArrayList();
                        ArrayList currentSites = new ArrayList();
                        ArrayList currentNitrogenTreatments = new ArrayList();

                        ArrayList currentExceptionYears = new ArrayList();
                        ArrayList currentExceptionSites = new ArrayList();
                        ArrayList currentNitrogenExceptionTreatments = new ArrayList();

                        ArrayList currentException = new ArrayList();

                        currentYears.Clear();
                        currentSites.Clear();
                        currentNitrogenTreatments.Clear();

                        currentExceptionYears.Clear();
                        currentExceptionSites.Clear();
                        currentNitrogenExceptionTreatments.Clear();

                        currentException.Clear();

                        int exceptionNumber = 0;

                        switch (calibrationRoundID)
                        {
                            #region 1.1 Phenology calibration
                            case "Phenology":
                                for (int i = 0; i < phenologyCalibrationYears.Length; i++)
                                {
                                    currentYears.Add(phenologyCalibrationYears[i]);
                                }
                                //Assign year information

                                for (int i = 0; i < phenologyCalibrationSites.Length; i++)
                                {
                                    currentSites.Add(phenologyCalibrationSites[i]);
                                }
                                //Assign site information

                                for (int i = 0; i < phenologyCalibrationNitrogenTreatments.Length; i++)
                                {
                                    currentNitrogenTreatments.Add(phenologyCalibrationNitrogenTreatments[i]);
                                }
                                //Assign nitrogen treatment information

                                currentException.Add(phenologyException);//Assign exception treatment information.
                                break;
                            #endregion

                            #region 1.2 LLE calibration
                            case "LLE":
                                for (int i = 0; i < LLECalibrationYears.Length; i++)
                                {
                                    currentYears.Add(LLECalibrationYears[i]);
                                }
                                //Assign year information

                                for (int i = 0; i < LLECalibrationSites.Length; i++)
                                {
                                    currentSites.Add(LLECalibrationSites[i]);
                                }
                                //Assign site information

                                for (int i = 0; i < LLECalibrationNitrogenTreatments.Length; i++)
                                {
                                    currentNitrogenTreatments.Add(LLECalibrationNitrogenTreatments[i]);
                                }
                                //Assign nitrogen treatment information

                                currentException.Add(LLEException);//Assign exception treatment information.                               
                                break;
                            #endregion

                            #region 1.2.1 LLE1 calibration
                            case "LLE1":
                                for (int i = 0; i < LLE1CalibrationYears.Length; i++)
                                {
                                    currentYears.Add(LLE1CalibrationYears[i]);
                                }
                                //Assign year information

                                for (int i = 0; i < LLE1CalibrationSites.Length; i++)
                                {
                                    currentSites.Add(LLE1CalibrationSites[i]);
                                }
                                //Assign site information

                                for (int i = 0; i < LLE1CalibrationNitrogenTreatments.Length; i++)
                                {
                                    currentNitrogenTreatments.Add(LLE1CalibrationNitrogenTreatments[i]);
                                }
                                //Assign nitrogen treatment information

                                currentException.Add(LLE1Exception);//Assign exception treatment information.                               
                                break;
                            #endregion

                            #region 1.2.2 LLE2 calibration
                            case "LLE2":
                                for (int i = 0; i < LLE2CalibrationYears.Length; i++)
                                {
                                    currentYears.Add(LLE2CalibrationYears[i]);
                                }
                                //Assign year information

                                for (int i = 0; i < LLE2CalibrationSites.Length; i++)
                                {
                                    currentSites.Add(LLE2CalibrationSites[i]);
                                }
                                //Assign site information

                                for (int i = 0; i < LLE2CalibrationNitrogenTreatments.Length; i++)
                                {
                                    currentNitrogenTreatments.Add(LLE2CalibrationNitrogenTreatments[i]);
                                }
                                //Assign nitrogen treatment information

                                currentException.Add(LLE2Exception);//Assign exception treatment information.                               
                                break;
                            #endregion

                            #region 1.3 LUE calibration
                            case "LUE":
                                for (int i = 0; i < LUECalibrationYears.Length; i++)
                                {
                                    currentYears.Add(LUECalibrationYears[i]);
                                }
                                //Assign year information

                                for (int i = 0; i < LUECalibrationSites.Length; i++)
                                {
                                    currentSites.Add(LUECalibrationSites[i]);
                                }
                                //Assign site information

                                for (int i = 0; i < LUECalibrationNitrogenTreatments.Length; i++)
                                {
                                    currentNitrogenTreatments.Add(LUECalibrationNitrogenTreatments[i]);
                                }
                                //Assign nitrogen treatment information

                                currentException.Add(LUEException);//Assign exception treatment information.
                                break;
                            #endregion

                            #region 1.4 N allocation calibration
                            case "NAllocation":
                                for (int i = 0; i < NAllocationCalibrationYears.Length; i++)
                                {
                                    currentYears.Add(NAllocationCalibrationYears[i]);
                                }
                                //Assign year information

                                for (int i = 0; i < NAllocationCalibrationSites.Length; i++)
                                {
                                    currentSites.Add(NAllocationCalibrationSites[i]);
                                }
                                //Assign site information

                                for (int i = 0; i < NAllocationCalibrationNitrogenTreatments.Length; i++)
                                {
                                    currentNitrogenTreatments.Add(NAllocationCalibrationNitrogenTreatments[i]);
                                }
                                //Assign nitrogen treatment information

                                currentException.Add(NAllocationException);//Assign exception treatment information.
                                break;
                            #endregion

                            #region 1.5 Soil drought factor calibration
                            case "SoilDrought":
                                for (int i = 0; i < soilDroughtCalibrationYears.Length; i++)
                                {
                                    currentYears.Add(soilDroughtCalibrationYears[i]);
                                }
                                //Assign year information

                                for (int i = 0; i < soilDroughtCalibrationSites.Length; i++)
                                {
                                    currentSites.Add(soilDroughtCalibrationSites[i]);
                                }
                                //Assign site information

                                for (int i = 0; i < soilDroughtCalibrationNitrogenTreatments.Length; i++)
                                {
                                    currentNitrogenTreatments.Add(soilDroughtCalibrationNitrogenTreatments[i]);
                                }
                                //Assign nitrogen treatment information

                                currentException.Add(soilDroughtException);//Assign exception treatment information.
                                break;
                            #endregion
                        }
                        //The treamtment information that is used in current procedure of model calibration.
                        #endregion

                        #region 3.7.2. Set up run file and project file...

                        #region 2.1 Determine the number of total current treatments.
                        string[,] Exception = (string[,])currentException[0];
                        int exceptionNumberNotExistingInCurrentTreatments = 0;

                        if (Exception.GetLength(1) != 0)
                        {
                            for (int except = 0; except < Exception.GetLength(0); except++)
                            {
                                bool yearExsiting = false;
                                bool siteExsiting = false;
                                bool NTreatmentExsiting = false;

                                string currentExceptionYear = Exception[except, 0];
                                string currentExceptionSite = Exception[except, 1];
                                string currentNitrogenExceptionTreatment = Exception[except, 2];

                                for (int year = 0; year < currentYears.Count; year++)
                                {
                                    if (currentExceptionYear == (string)currentYears[year])
                                        yearExsiting = true;
                                }

                                for (int site = 0; site < currentSites.Count; site++)
                                {
                                    if (currentExceptionSite == (string)currentSites[site])
                                        siteExsiting = true;
                                }

                                for (int nitrogen = 0; nitrogen < currentNitrogenTreatments.Count; nitrogen++)
                                {
                                    if (currentNitrogenExceptionTreatment == (string)currentNitrogenTreatments[nitrogen])
                                        NTreatmentExsiting = true;
                                }

                                if (yearExsiting == false || siteExsiting == false || NTreatmentExsiting == false)
                                {
                                    exceptionNumberNotExistingInCurrentTreatments++;
                                }
                            }
                        }
                        //To test whether Exception is exsiting in current possible treatments. If not existed, this exception item should not be counted in the exception number.

                        if (Exception.GetLength(1) == 0) //Get the culumn number of tempException.
                        {
                            exceptionNumber = Exception.GetLength(0) - 1;//Get the exception treatment number, when there is no Exception. Actually the exception number is 0 here.
                        }
                        else
                        {
                            exceptionNumber = Exception.GetLength(0) - exceptionNumberNotExistingInCurrentTreatments;//Get the exception treatment number, when there are Exceptions.
                        }

                        int currentTreatmentNumber = currentSites.Count * currentYears.Count * currentNitrogenTreatments.Count - exceptionNumber;//Get the current treatment number.

                        XmlDocument myXMLEdit3 = new XmlDocument();
                        XMLEdit myXMLEdit4 = new XMLEdit();
                        #endregion

                        #region 2.2 Set up Run file.
                        string templateRunFileName = projectName + " (Template)";
                        string templateRunFileExtension = ".sqrun";
                        string wholeTemplateRunFilePath = workDirectory + genericAlgorithmsDirectory + templateRunFileName + templateRunFileExtension;

                        string currentRunFileName = projectName + " (GeneticAlgorithms)";
                        string currentRunFileExtension = ".sqrun";
                        string wholeCurrentRunFilePath = workDirectory + currentRunFileName + currentRunFileExtension;

                        myXMLEdit3.Load(wholeTemplateRunFilePath);
                        myXMLEdit3.Save(wholeCurrentRunFilePath);

                        string runFileRoot = "RunFile/ItemsArray";
                        string sourceRunFileRoot = "RunFile/ItemsArray/RunItem";
                        string destinationRunFileRoot = "RunFile/ItemsArray";

                        int existingRunFileNodeNumber = myXMLEdit2.CountNodeNumber(wholeCurrentRunFilePath, runFileRoot);
                        //Console.WriteLine(existingRunFileNodeNumber);

                        myXMLEdit4.CopyAndPasteNode(wholeTemplateRunFilePath, wholeCurrentRunFilePath, sourceRunFileRoot, destinationRunFileRoot, currentTreatmentNumber, existingRunFileNodeNumber);
                        myXMLEdit4.XMLSave(wholeCurrentRunFilePath);
                        //Copy and paste the node. Each node represents a treatment.

                        string destinationNormalRunFileRoot = "RunFile/ItemsArray/RunItem/Normal";
                        string destinationMultiRunFileRoot = "RunFile/ItemsArray/RunItem/Multi/MultiRunsArray/MultiRunItem";
                        string destinationSensitivityRunFileRoot = "RunFile/ItemsArray/RunItem/Sensitivity";

                        string[] destinationRoots = { destinationRunFileRoot, destinationNormalRunFileRoot, destinationMultiRunFileRoot, destinationSensitivityRunFileRoot };

                        myXMLEdit4.ChangeRunFileNodeValue(wholeCurrentRunFilePath, runFileRoot, runItems, currentYears, currentSites, currentNitrogenTreatments, currentException);
                        //Load the Run file.
                        #endregion

                        #region 2.3 Set up Project file.
                        string templateProjectFileName = projectName + " (Template)";
                        string templateProjectFileExtension = ".sqpro";
                        string wholeTemplateProjectFilePath = workDirectory + genericAlgorithmsDirectory + templateProjectFileName + templateProjectFileExtension;

                        string currentProjectFileName = projectName + " (GeneticAlgorithms)";
                        string currentProjectFileExtension = ".sqpro";
                        string wholeCurrentProjectFilePath = workDirectory + currentProjectFileName + currentProjectFileExtension;

                        myXMLEdit3.Load(wholeTemplateProjectFilePath);
                        myXMLEdit3.Save(wholeCurrentProjectFilePath);

                        string projectFileNode = "ProjectFile/Inputs";

                        string[] projectFileItems = { currentParameterFileName + currentParameterFileExtension, currentVarietyFileName + currentVarietyFileExtension, currentRunFileName + currentRunFileExtension };

                        myXMLEdit4.ChangeProjectFileNodeValue(wholeCurrentProjectFilePath, projectFileNode, projectFileItems);
                        //Load the project file.
                        #endregion

                        #endregion

                        #region 3.7.3. Load the observations.

                        #region 3.7.3.1.Observation file information
                        string observationFileName = "Observations (New Format)";
                        string observationFileExtention = ".xls";
                        string wholeObservationFileName = workDirectory + genericAlgorithmsDirectory + observationFileName + observationFileExtention;
                        //Assign the path of the Excel file that contains observations.
                        string currentObservationSheetName = "";
                        string currentObservationSheetName_1 = "";
                        string currentObservationSheetName_2 = "";

                        int totalTreatmentNumber = 0;
                        int totalTreatmentNumber_1 = 0;
                        int totalTreatmentNumber_2 = 0;
                        //Line number of the string array that contains treatment information.

                        if (calibrationRoundID == "Phenology")
                        {
                            currentObservationSheetName = "End-season";
                            totalTreatmentNumber = 256;//This number is very important, must be (Total Lines in the spreadsheet - 2);
                        }
                        else if (calibrationRoundID == "LLE")
                        {
                            currentObservationSheetName_1 = "Leaf-profile";
                            totalTreatmentNumber_1 = 256;

                            currentObservationSheetName_2 = "Time-series";
                            totalTreatmentNumber_2 = 768;
                            //For LLE calibration, two kinds of LAI observations of leaf profile and LAI dynamic were involved.
                        }
                        else if (calibrationRoundID == "LLE1")
                        {
                            currentObservationSheetName = "Leaf-profile";
                            totalTreatmentNumber = 256;//This number is very important, must be (Total Lines in the spreadsheet - 2);
                        }
                        else
                        {
                            currentObservationSheetName = "Time-series";
                            totalTreatmentNumber = 768;//This number is very important, must be (Total Lines in the spreadsheet - 2);
                        }
                        //For "Phenology" calibration, only the end-season observations will be involved, while time-series observations will be involved in other sub-model calibrations.

                        #endregion

                        #region 3.7.3.2.Observation variables (Which observation variables will be used in this round of calibration?)

                        double fitnessShresholdForLoacalSearch = 0;
                        int amplificationFactor = 1;//The factor to amplify the fitness values.
                        double CV = 0; //The coefficient of variation used for fitness value calibration.

                        ArrayList observationDates = new ArrayList();
                        observationDates.Clear();
                        //The array that contains the dates of observations.

                        ArrayList observations = new ArrayList();
                        observations.Clear();
                        //The array that contains the mean values of the observations.

                        ArrayList stds = new ArrayList();
                        stds.Clear();
                        //The array that contains the standard deviations of the observations.

                        ArrayList observationSeperator = new ArrayList();
                        observations.Clear();
                        //The array that contains the seperators to seperate the whole observation data for each treatment.

                        ArrayList observationIDs = new ArrayList();
                        observationIDs.Clear();

                        ArrayList observationIDs_1 = new ArrayList();
                        observationIDs_1.Clear();

                        ArrayList observationIDs_2 = new ArrayList();
                        observationIDs_2.Clear();
                        //The array that contains the column number of each observation variable.

                        switch (calibrationRoundID)
                        {
                            #region 3.7.3.2.1 Phenology observation
                            case "Phenology":

                                fitnessShresholdForLoacalSearch = phenologyFitnessShresholdForLoacalSearch;
                                amplificationFactor = phenologyAmplificationFactor;
                                CV = phenologyCV;

                                for (int phenologyID = 0; phenologyID < phenologyObservationIDs.Length; phenologyID++)
                                {
                                    observationIDs.Add(phenologyObservationIDs[phenologyID]);
                                }

                                break;

                            #endregion

                            #region 3.7.3.2.2 LLE1 observations
                            case "LLE":

                                fitnessShresholdForLoacalSearch = LLEFitnessShresholdForLoacalSearch;
                                amplificationFactor = LLEAmplificationFactor;
                                CV = LLECV;

                                for (int LLEID = 0; LLEID < LLEObservationIDs_1.Length; LLEID++)
                                {
                                    observationIDs_1.Add(LLEObservationIDs_1[LLEID]);
                                }

                                for (int LLEID = 0; LLEID < LLEObservationIDs_2.Length; LLEID++)
                                {
                                    observationIDs_2.Add(LLEObservationIDs_2[LLEID]);
                                }

                                //Two kinds of observations were involved.
                                break;
                            #endregion

                            #region 3.7.3.2.2.1 LLE1 observations
                            case "LLE1":

                                fitnessShresholdForLoacalSearch = LLE1FitnessShresholdForLoacalSearch;
                                amplificationFactor = LLE1AmplificationFactor;
                                CV = LLE1CV;

                                for (int LLEID = 0; LLEID < LLE1ObservationIDs.Length; LLEID++)
                                {
                                    observationIDs.Add(LLE1ObservationIDs[LLEID]);
                                }

                                break;
                            #endregion

                            #region 3.7.3.2.2.2 LLE2 observations
                            case "LLE2":

                                fitnessShresholdForLoacalSearch = LLE2FitnessShresholdForLoacalSearch;
                                amplificationFactor = LLE2AmplificationFactor;
                                CV = LLE2CV;

                                for (int LLEID = 0; LLEID < LLE2ObservationIDs.Length; LLEID++)
                                {
                                    observationIDs.Add(LLE2ObservationIDs[LLEID]);
                                }

                                break;
                            #endregion

                            #region 3.7.3.2.3 LUE observations
                            case "LUE":
                                fitnessShresholdForLoacalSearch = LUEFitnessShresholdForLoacalSearch;
                                amplificationFactor = LUEAmplificationFactor;
                                CV = LUECV;

                                for (int LUEID = 0; LUEID < LUEObservationIDs.Length; LUEID++)
                                {
                                    observationIDs.Add(LUEObservationIDs[LUEID]);
                                }
                                break;
                            #endregion

                            #region 3.7.3.2.4 NAllocation (N allocation in grains, stems, and leaves) observations
                            case "NAllocation":
                                fitnessShresholdForLoacalSearch = NAllocationFitnessShresholdForLoacalSearch;
                                amplificationFactor = NAllocationAmplificationFactor;
                                CV = NAllocationCV;

                                for (int NAllocationID = 0; NAllocationID < NAllocationObservationIDs.Length; NAllocationID++)
                                {
                                    observationIDs.Add(NAllocationObservationIDs[NAllocationID]);
                                }
                                break;
                            #endregion

                            #region 3.7.3.2.5 Soil drought (soil factor factor) observations
                            case "SoilDrought":
                                fitnessShresholdForLoacalSearch = soilDroughtFitnessShresholdForLoacalSearch;
                                amplificationFactor = soilDroughtAmplificationFactor;
                                CV = soilDroughtCV;

                                for (int soilDroughtID = 0; soilDroughtID < soilDroughtObservationIDs.Length; soilDroughtID++)
                                {
                                    observationIDs.Add(soilDroughtObservationIDs[soilDroughtID]);
                                }
                                break;
                            #endregion
                        }

                        #endregion

                        #region 3.7.3.3.Read observation values.
                        ExcelEdit myExcelEdit = new ExcelEdit();
                        object cellValue = null;
                        //The object to save the value of each cell.

                        //1.0 Open the observation file
                        myExcelEdit.Open(wholeObservationFileName);

                        //2.0 Read the observation file.
                        Console.WriteLine("Loading the observations...");

                        for (int i = 0; i < currentCultivarNumbers.Length; i++)
                        {
                            if (calibrationRoundID != "LLE")
                            {
                                #region 3.7.3.3.1 Variables used in the loading process.
                                string currentCultivarNumber = currentCultivarNumbers[i];
                                ArrayList totalTreatmentLineIDs = new ArrayList();
                                int singleTreatmentLineNumber = 0;
                                string localSite = "";
                                string localYear = "";
                                string localNitrogenTreatment = "";
                                int accumulativeLineNumber = 0;
                                #endregion

                                #region 3.7.3.3.2 Select the lines that contains the observations for the given treatments.
                                for (int j = 3; j <= totalTreatmentNumber + 1; j++) //Observation value starts from Line 3 in the Observation file.
                                {
                                    bool yearExisting = false;
                                    bool siteExisting = false;
                                    bool nitrogenTreatmentExisting = false;
                                    bool ExceptionIndicator = false;
                                    //The indicators to show whether the treatment is existing.

                                    string cultivarNumber = (string)myExcelEdit.GetCellValue(currentObservationSheetName, j, 4, cellValue); //Cultivar number is saved in Column 4.

                                    if (cultivarNumber == currentCultivarNumber)
                                    {
                                        string year = (string)myExcelEdit.GetCellValue(currentObservationSheetName, j, 1, cellValue); //Year is the 1st column.
                                        string site = (string)myExcelEdit.GetCellValue(currentObservationSheetName, j, 2, cellValue); //Site
                                        string nitrogenTreatment = (string)myExcelEdit.GetCellValue(currentObservationSheetName, j, 3, cellValue); //Nitrogen treatment.

                                        if (exceptionNumber != 0)
                                        {
                                            for (int line = 0; line < Exception.GetLength(0); line++) //Exception.GetLength(0) is the total number of exceptions.
                                            {
                                                string currentExceptionYear = Exception[line, 0];
                                                string currentExceptionSite = Exception[line, 1];
                                                string currentNitrogenExceptionTreatment = Exception[line, 2];

                                                if (currentExceptionYear == year & currentExceptionSite == site & currentNitrogenExceptionTreatment == nitrogenTreatment)
                                                {
                                                    ExceptionIndicator = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ExceptionIndicator = false;
                                        }
                                        //The indicator to show if the Exception is existing. Observations will not be loaded for exceptions.

                                        foreach (string currentYear in currentYears)
                                        {
                                            if (currentYear == year)
                                            {
                                                yearExisting = true;
                                            }
                                        }
                                        //To check which years exist.

                                        foreach (string currentSite in currentSites)
                                        {
                                            if (currentSite == site)
                                            {
                                                siteExisting = true;
                                            }
                                        }
                                        //To check which sites exist.

                                        foreach (string currentNitrogenTreatment in currentNitrogenTreatments)
                                        {
                                            if (currentNitrogenTreatment == nitrogenTreatment)
                                            {
                                                nitrogenTreatmentExisting = true;
                                            }
                                        }
                                        //To check which nitrogen treatments exist.

                                        if (yearExisting == true & siteExisting == true & nitrogenTreatmentExisting == true & ExceptionIndicator == false)
                                        {
                                            totalTreatmentLineIDs.Add(j);
                                            //Save the number of the line that contains the observations for a given treatment.The process them later.
                                        }
                                        else
                                        {
                                            continue;
                                        }

                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                #endregion

                                #region 3.7.3.3.3 Load the data for each treament in an alphabetial order.
                                int totalLineNumber = totalTreatmentLineIDs.Count;

                                foreach (string currentSite in currentSites)
                                {
                                    foreach (string currentYear in currentYears)
                                    {
                                        foreach (string currentNitrogenTreatment in currentNitrogenTreatments)
                                        {
                                            Console.WriteLine("--------------");
                                            Console.WriteLine(currentSite);
                                            Console.WriteLine(currentYear);
                                            Console.WriteLine(currentNitrogenTreatment);
                                            Console.WriteLine("--------------");

                                            if (currentObservationSheetName == "Time-series")
                                            {
                                                #region 3.7.3.3.3.1 Load the time-series data.
                                                for (int lineIDIndex = 0; lineIDIndex < totalLineNumber; lineIDIndex++)
                                                {
                                                    int tempLineID = (int)(totalTreatmentLineIDs[lineIDIndex]);

                                                    string tempSite = Convert.ToString(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, 2, cellValue));
                                                    string tempYearString = Convert.ToString(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, 1, cellValue));
                                                    string tempNTreatment = Convert.ToString(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, 3, cellValue));

                                                    int tempYear = Convert.ToInt16(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, 1, cellValue));
                                                    int tempMonth = Convert.ToInt16(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, 7, cellValue));
                                                    int tempDay = Convert.ToInt16(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, 8, cellValue));

                                                    if (tempSite == currentSite & tempYearString == currentYear & tempNTreatment == currentNitrogenTreatment)
                                                    {
                                                        DateTime tempDate = new DateTime(tempYear, tempMonth, tempDay);
                                                        observationDates.Add(tempDate);

                                                        for (int obsID = 0; obsID < observationIDs.Count; obsID++)
                                                        {
                                                            int observationID = (int)observationIDs[obsID];

                                                            double observationValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, observationID, cellValue));
                                                            observations.Add(observationValue);

                                                            double stdValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, observationID + 22, cellValue));
                                                            stds.Add(stdValue);
                                                            //The number 22 means there are 22 columns between the mean and standard deviation of the given observation variable in the time-series spreadsheet.
                                                            //Console.WriteLine(tempLineID);
                                                            //Console.WriteLine(observationValue);
                                                        }
                                                        //Read the observations one by one in each line if there are multiple observations involved in the calibration.

                                                        if (localSite != tempSite | localYear != tempYearString | localNitrogenTreatment != tempNTreatment)
                                                        {
                                                            accumulativeLineNumber = accumulativeLineNumber + singleTreatmentLineNumber;

                                                            if (accumulativeLineNumber != 0)
                                                            {
                                                                observationSeperator.Add(accumulativeLineNumber);
                                                                //When the treatment (combination of site, year, and N treatment) is changed, save the line number for this treament.
                                                            }

                                                            localSite = tempSite;
                                                            localYear = tempYearString;
                                                            localNitrogenTreatment = tempNTreatment;

                                                            singleTreatmentLineNumber = 0;
                                                        }
                                                        //Calculate the observation seoerator.

                                                        singleTreatmentLineNumber++;//When find a new line, the line number for each treatment increases by 1.

                                                    }

                                                }
                                                //Only read the time-series observations.
                                                #endregion
                                            }
                                            else if (currentObservationSheetName == "End-season")
                                            {
                                                #region 3.7.3.3.3.2 Load the end-season data.
                                                for (int lineIDIndex = 0; lineIDIndex < totalLineNumber; lineIDIndex++)
                                                {
                                                    int tempLineID = (int)(totalTreatmentLineIDs[lineIDIndex]);

                                                    string tempSite = Convert.ToString(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, 2, cellValue));
                                                    string tempYear = Convert.ToString(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, 1, cellValue));
                                                    string tempNTreatment = Convert.ToString(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, 3, cellValue));

                                                    if (tempSite == currentSite & tempYear == currentYear & tempNTreatment == currentNitrogenTreatment)
                                                    {
                                                        for (int obsID = 0; obsID < observationIDs.Count; obsID++)
                                                        {
                                                            int observationID = (int)observationIDs[obsID];

                                                            double observationValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, observationID, cellValue));
                                                            observations.Add(observationValue);

                                                            double stdValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, observationID + 4, cellValue));
                                                            //The number 22 means there are 22 columns between the mean and standard deviation of the given observation variable in the end-season spreadsheet.
                                                            stds.Add(stdValue);
                                                            //Console.WriteLine(tempLineID);
                                                            //Console.WriteLine(observationValue);
                                                        }
                                                        //Save observations alphabetically.
                                                    }
                                                }
                                                #endregion
                                            }
                                            else if (currentObservationSheetName == "Leaf-profile")
                                            {
                                                #region 3.7.3.3.3.3 Load the leaf-profile data.
                                                for (int lineIDIndex = 0; lineIDIndex < totalLineNumber; lineIDIndex++)
                                                {
                                                    int tempLineID = (int)(totalTreatmentLineIDs[lineIDIndex]);

                                                    string tempSite = Convert.ToString(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, 2, cellValue));
                                                    string tempYear = Convert.ToString(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, 1, cellValue));
                                                    string tempNTreatment = Convert.ToString(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, 3, cellValue));

                                                    if (tempSite == currentSite & tempYear == currentYear & tempNTreatment == currentNitrogenTreatment)
                                                    {
                                                        for (int obsID = 0; obsID < observationIDs.Count; obsID++)
                                                        {
                                                            int observationID = (int)observationIDs[obsID];

                                                            double observationValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, observationID, cellValue));
                                                            observations.Add(observationValue);

                                                            double stdValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentObservationSheetName, tempLineID, observationID + 5, cellValue));
                                                            //The number 22 means there are 22 columns between the mean and standard deviation of the given observation variable in the end-season spreadsheet.
                                                            stds.Add(stdValue);
                                                            //Console.WriteLine(tempLineID);
                                                            //Console.WriteLine(observationValue);
                                                        }
                                                        //Save observations alphabetically.
                                                    }
                                                }
                                                #endregion
                                            }
                                        }
                                    }
                                }
                                //Load the observation array alphabetically.

                                observationSeperator.Add(totalTreatmentLineIDs.Count);
                                totalTreatmentLineIDs.Clear();
                                #endregion
                            }
                            else if (calibrationRoundID == "LLE")
                            {
                                #region 3.7.3.3.1 Variables used in the loading process.
                                string currentCultivarNumber = currentCultivarNumbers[i];
                                ArrayList totalTreatmentLineIDs = new ArrayList();
                                ArrayList totalTreatmentLineIDs_1 = new ArrayList();
                                ArrayList totalTreatmentLineIDs_2 = new ArrayList();

                                int singleTreatmentLineNumber = 0;
                                string localSite = "";
                                string localYear = "";
                                string localNitrogenTreatment = "";
                                int accumulativeLineNumber = 0;
                                #endregion

                                #region 3.7.3.3.2.1 Select the lines that contains the observations for the given treatments (Leaf profile).
                                for (int j = 3; j <= totalTreatmentNumber_1 + 1; j++) //Observation value starts from Line 3 in the Observation file.
                                {
                                    bool yearExisting = false;
                                    bool siteExisting = false;
                                    bool nitrogenTreatmentExisting = false;
                                    bool ExceptionIndicator = false;
                                    //The indicators to show whether the treatment is existing.

                                    string cultivarNumber = (string)myExcelEdit.GetCellValue(currentObservationSheetName_1, j, 4, cellValue); //Cultivar number is saved in Column 4.

                                    if (cultivarNumber == currentCultivarNumber)
                                    {
                                        string year = (string)myExcelEdit.GetCellValue(currentObservationSheetName_1, j, 1, cellValue); //Year is the 1st column.
                                        string site = (string)myExcelEdit.GetCellValue(currentObservationSheetName_1, j, 2, cellValue); //Site
                                        string nitrogenTreatment = (string)myExcelEdit.GetCellValue(currentObservationSheetName_1, j, 3, cellValue); //Nitrogen treatment.

                                        if (exceptionNumber != 0)
                                        {
                                            for (int line = 0; line < Exception.GetLength(0); line++) //Exception.GetLength(0) is the total number of exceptions.
                                            {
                                                string currentExceptionYear = Exception[line, 0];
                                                string currentExceptionSite = Exception[line, 1];
                                                string currentNitrogenExceptionTreatment = Exception[line, 2];

                                                if (currentExceptionYear == year & currentExceptionSite == site & currentNitrogenExceptionTreatment == nitrogenTreatment)
                                                {
                                                    ExceptionIndicator = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ExceptionIndicator = false;
                                        }
                                        //The indicator to show if the Exception is existing. Observations will not be loaded for exceptions.

                                        foreach (string currentYear in currentYears)
                                        {
                                            if (currentYear == year)
                                            {
                                                yearExisting = true;
                                            }
                                        }
                                        //To check which years exist.

                                        foreach (string currentSite in currentSites)
                                        {
                                            if (currentSite == site)
                                            {
                                                siteExisting = true;
                                            }
                                        }
                                        //To check which sites exist.

                                        foreach (string currentNitrogenTreatment in currentNitrogenTreatments)
                                        {
                                            if (currentNitrogenTreatment == nitrogenTreatment)
                                            {
                                                nitrogenTreatmentExisting = true;
                                            }
                                        }
                                        //To check which nitrogen treatments exist.

                                        if (yearExisting == true & siteExisting == true & nitrogenTreatmentExisting == true & ExceptionIndicator == false)
                                        {
                                            totalTreatmentLineIDs_1.Add(j);
                                            //Save the number of the line that contains the observations for a given treatment.The process them later.
                                        }
                                        else
                                        {
                                            continue;
                                        }

                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                #endregion

                                #region 3.7.3.3.2.2 Select the lines that contains the observations for the given treatments (LAI dynaimic).
                                for (int j = 3; j <= totalTreatmentNumber_2 + 1; j++) //Observation value starts from Line 3 in the Observation file.
                                {
                                    bool yearExisting = false;
                                    bool siteExisting = false;
                                    bool nitrogenTreatmentExisting = false;
                                    bool ExceptionIndicator = false;
                                    //The indicators to show whether the treatment is existing.

                                    string cultivarNumber = (string)myExcelEdit.GetCellValue(currentObservationSheetName_2, j, 4, cellValue); //Cultivar number is saved in Column 4.

                                    if (cultivarNumber == currentCultivarNumber)
                                    {
                                        string year = (string)myExcelEdit.GetCellValue(currentObservationSheetName_2, j, 1, cellValue); //Year is the 1st column.
                                        string site = (string)myExcelEdit.GetCellValue(currentObservationSheetName_2, j, 2, cellValue); //Site
                                        string nitrogenTreatment = (string)myExcelEdit.GetCellValue(currentObservationSheetName_2, j, 3, cellValue); //Nitrogen treatment.

                                        if (exceptionNumber != 0)
                                        {
                                            for (int line = 0; line < Exception.GetLength(0); line++) //Exception.GetLength(0) is the total number of exceptions.
                                            {
                                                string currentExceptionYear = Exception[line, 0];
                                                string currentExceptionSite = Exception[line, 1];
                                                string currentNitrogenExceptionTreatment = Exception[line, 2];

                                                if (currentExceptionYear == year & currentExceptionSite == site & currentNitrogenExceptionTreatment == nitrogenTreatment)
                                                {
                                                    ExceptionIndicator = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ExceptionIndicator = false;
                                        }
                                        //The indicator to show if the Exception is existing. Observations will not be loaded for exceptions.

                                        foreach (string currentYear in currentYears)
                                        {
                                            if (currentYear == year)
                                            {
                                                yearExisting = true;
                                            }
                                        }
                                        //To check which years exist.

                                        foreach (string currentSite in currentSites)
                                        {
                                            if (currentSite == site)
                                            {
                                                siteExisting = true;
                                            }
                                        }
                                        //To check which sites exist.

                                        foreach (string currentNitrogenTreatment in currentNitrogenTreatments)
                                        {
                                            if (currentNitrogenTreatment == nitrogenTreatment)
                                            {
                                                nitrogenTreatmentExisting = true;
                                            }
                                        }
                                        //To check which nitrogen treatments exist.

                                        if (yearExisting == true & siteExisting == true & nitrogenTreatmentExisting == true & ExceptionIndicator == false)
                                        {
                                            totalTreatmentLineIDs_2.Add(j);
                                            //Save the number of the line that contains the observations for a given treatment.The process them later.
                                        }
                                        else
                                        {
                                            continue;
                                        }

                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                #endregion

                                #region 3.7.3.3.3 Load the data for each treament in an alphabetial order.
                                int totalLineNumber_1 = totalTreatmentLineIDs_1.Count;
                                int totalLineNumber_2 = totalTreatmentLineIDs_2.Count;

                                foreach (string currentSite in currentSites)
                                {
                                    foreach (string currentYear in currentYears)
                                    {
                                        foreach (string currentNitrogenTreatment in currentNitrogenTreatments)
                                        {
                                            Console.WriteLine("--------------");
                                            Console.WriteLine(currentSite);
                                            Console.WriteLine(currentYear);
                                            Console.WriteLine(currentNitrogenTreatment);
                                            Console.WriteLine("--------------");

                                            #region 3.7.3.3.3.1 Load the leaf-profile data.
                                            if (currentObservationSheetName_1 == "Leaf-profile")
                                            {
                                                
                                                for (int lineIDIndex = 0; lineIDIndex < totalLineNumber_1; lineIDIndex++)
                                                {
                                                    int tempLineID = (int)(totalTreatmentLineIDs_1[lineIDIndex]);

                                                    string tempSite = Convert.ToString(myExcelEdit.GetCellValue(currentObservationSheetName_1, tempLineID, 2, cellValue));
                                                    string tempYear = Convert.ToString(myExcelEdit.GetCellValue(currentObservationSheetName_1, tempLineID, 1, cellValue));
                                                    string tempNTreatment = Convert.ToString(myExcelEdit.GetCellValue(currentObservationSheetName_1, tempLineID, 3, cellValue));

                                                    if (tempSite == currentSite & tempYear == currentYear & tempNTreatment == currentNitrogenTreatment)
                                                    {
                                                        for (int obsID = 0; obsID < observationIDs_1.Count; obsID++)
                                                        {
                                                            int observationID = (int)observationIDs_1[obsID];

                                                            double observationValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentObservationSheetName_1, tempLineID, observationID, cellValue));
                                                            observations.Add(observationValue);

                                                            double stdValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentObservationSheetName_1, tempLineID, observationID + 5, cellValue));
                                                            //The number 22 means there are 22 columns between the mean and standard deviation of the given observation variable in the end-season spreadsheet.
                                                            stds.Add(stdValue);

                                                            //Console.WriteLine(tempLineID);
                                                            //Console.WriteLine(observationValue);
                                                            //Console.WriteLine("aaaaaaaaaaaa");
                                                        }
                                                        //Save observations alphabetically.
                                                    }
                                                }

                                            }
                                            #endregion

                                            #region 3.7.3.3.3.2 Load the time-series data.
                                            if (currentObservationSheetName_2 == "Time-series")
                                            {
                                                
                                                for (int lineIDIndex = 0; lineIDIndex < totalLineNumber_2; lineIDIndex++)
                                                {
                                                    int tempLineID = (int)(totalTreatmentLineIDs_2[lineIDIndex]);

                                                    string tempSite = Convert.ToString(myExcelEdit.GetCellValue(currentObservationSheetName_2, tempLineID, 2, cellValue));
                                                    string tempYearString = Convert.ToString(myExcelEdit.GetCellValue(currentObservationSheetName_2, tempLineID, 1, cellValue));
                                                    string tempNTreatment = Convert.ToString(myExcelEdit.GetCellValue(currentObservationSheetName_2, tempLineID, 3, cellValue));

                                                    int tempYear = Convert.ToInt16(myExcelEdit.GetCellValue(currentObservationSheetName_2, tempLineID, 1, cellValue));
                                                    int tempMonth = Convert.ToInt16(myExcelEdit.GetCellValue(currentObservationSheetName_2, tempLineID, 7, cellValue));
                                                    int tempDay = Convert.ToInt16(myExcelEdit.GetCellValue(currentObservationSheetName_2, tempLineID, 8, cellValue));

                                                    if (tempSite == currentSite & tempYearString == currentYear & tempNTreatment == currentNitrogenTreatment)
                                                    {
                                                        DateTime tempDate = new DateTime(tempYear, tempMonth, tempDay);
                                                        observationDates.Add(tempDate);

                                                        for (int obsID = 0; obsID < observationIDs_2.Count; obsID++)
                                                        {
                                                            int observationID = (int)observationIDs_2[obsID];

                                                            double observationValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentObservationSheetName_2, tempLineID, observationID, cellValue));
                                                            observations.Add(observationValue);

                                                            double stdValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentObservationSheetName_2, tempLineID, observationID + 22, cellValue));
                                                            stds.Add(stdValue);
                                                            //The number 22 means there are 22 columns between the mean and standard deviation of the given observation variable in the time-series spreadsheet.
                                                            
                                                            //Console.WriteLine(tempLineID);
                                                            //Console.WriteLine(observationValue);
                                                            //Console.WriteLine("bbbbbbbbbb");
                                                        }
                                                        //Read the observations one by one in each line if there are multiple observations involved in the calibration.

                                                        if (localSite != tempSite | localYear != tempYearString | localNitrogenTreatment != tempNTreatment)
                                                        {
                                                            accumulativeLineNumber = accumulativeLineNumber + singleTreatmentLineNumber;

                                                            if (accumulativeLineNumber != 0)
                                                            {
                                                                observationSeperator.Add(accumulativeLineNumber);
                                                                //When the treatment (combination of site, year, and N treatment) is changed, save the line number for this treament.
                                                            }

                                                            localSite = tempSite;
                                                            localYear = tempYearString;
                                                            localNitrogenTreatment = tempNTreatment;

                                                            singleTreatmentLineNumber = 0;
                                                        }
                                                        //Calculate the observation seoerator.

                                                        singleTreatmentLineNumber++;//When find a new line, the line number for each treatment increases by 1.

                                                    }

                                                }
                                                //Only read the time-series observations.
                                            }
                                                                                       
                                            #endregion
                                        }
                                    }
                                }
                                //Load the observation array alphabetically.

                                observationSeperator.Add(totalTreatmentLineIDs_2.Count);
                                totalTreatmentLineIDs.Clear();
                                #endregion
                            }
                            
                            //For LLE, two kinds of observations will be involved, so the observation loading is different.
                        }

                        TestPopulation.observationDateArray = observationDates;//Transfer the observation dates to the model run.
                        TestPopulation.observationArray = observations;//Transfer the observations to the model run.
                        TestPopulation.stdArray = stds; //Transfer the standard deviation of the observation variables to the model run.
                        TestPopulation.observationSeperator = observationSeperator;//Transfer the observation seperator to model run so that Sirius can recognize whis part of the observation will be used for the given treatment.
                        TestPopulation.fitnessShresholdForLoacalSearch = fitnessShresholdForLoacalSearch;//Transfer the shreshold fitness value for local search to the model run.
                        TestPopulation.amplificationFactor = amplificationFactor;//Transfer the amplification factor for fitness value.
                        TestPopulation.CV = CV;//Transfer the coefficient of variation for fitness value.

                        //3.0 Close the observation file
                        myExcelEdit.Close();
                        #endregion

                        #endregion

                        #region 3.7.4. Load the parameter ranges.

                        #region 3.7.4.1.Parameter range file information
                        string parameterRangeFileName = "ParameterRanges";
                        string parameterRangeFileExtention = ".xls";
                        string wholeParameterRangeFileName = workDirectory + genericAlgorithmsDirectory + parameterRangeFileName + parameterRangeFileExtention;
                        //Assign the path of the Excel file that contains parameter ranges.
                        #endregion

                        #region 3.7.4.2.Parameters used in each round of simulation

                        string currentParameterRangeSheetName = null;

                        switch (calibrationRoundID)
                        {
                            #region 3.7.4.2.1 Phenology parameter ranges
                            case "Phenology":
                                currentParameterRangeSheetName = "1. Phenology";
                                break;
                            #endregion

                            #region 3.7.4.2.2 Leaf area index parameter rangesn (6 parameters, both leaf profile and LAI dynamics)
                            case "LLE":
                                currentParameterRangeSheetName = "2. LLE";
                                break;
                            #endregion

                            #region 3.7.4.2.2.1 Leaf area index parameter ranges (3 parameters, leaf profile at anthesis)
                            case "LLE1":
                                currentParameterRangeSheetName = "2.1 LLE1";
                                break;
                            #endregion

                            #region 3.7.4.2.2.2 Leaf area index parameter ranges (3 parameters, LAI dynamics)
                            case "LLE2":
                                currentParameterRangeSheetName = "2.2 LLE2";
                                break;
                            #endregion

                            #region 3.7.4.2.3 LUE parameter ranges
                            case "LUE":
                                currentParameterRangeSheetName = "3. LUE";
                                break;
                            #endregion

                            #region 3.7.4.2.4 NAllocation (N allocation in grains, stems, and leaves) parameter ranges
                            case "NAllocation":
                                currentParameterRangeSheetName = "4. NAllocation";
                                break;
                            #endregion

                            #region 3.7.4.2.5 SoilDrought (soil drought factor) parameter ranges
                            case "SoilDrought":
                                currentParameterRangeSheetName = "5. SoilDrought";
                                break;
                            #endregion
                        }

                        #endregion

                        #region 3.7.4.3.Read and assign the parameter range values
                        //1.0 Open the observation file
                        myExcelEdit.Open(wholeParameterRangeFileName);
                        int totalParameterNumber;

                        int[] parameterSiteIndex;
                        string[] parameterNames = new string[0];//A string vector to save the parameter names used in the calibration. 05/08/2010.He
                        
                        if (calibrationRoundID != "SoilN") //The Soil N parameters was removed from the calibration procedure.
                        {
                            totalParameterNumber = (int)(Convert.ToDouble(myExcelEdit.GetCellValue(currentParameterRangeSheetName, 2, 8, cellValue)));

                            parameterNames = new string[totalParameterNumber];
                            double[] parameterMin = new double[totalParameterNumber];
                            double[] parameterMax = new double[totalParameterNumber];
                            double[] parameterStep = new double[totalParameterNumber];
                            double[] parameterStepLimit = new double[totalParameterNumber];//Vectors to save the minimum, maximum, initial step, and minimum step values of each parameter.

                            parameterSiteIndex = new int[totalParameterNumber];//Create an integer list to save the site index, for example CF07=1, CF08=2, but it is only used by Soil N calibration.

                            for (int i = 0; i <= totalParameterNumber - 1; i++)
                            {
                                string tempName = Convert.ToString(myExcelEdit.GetCellValue(currentParameterRangeSheetName, (i + 2), 2, cellValue));
                                double tempMinValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentParameterRangeSheetName, (i + 2), 3, cellValue));
                                double tempMaxValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentParameterRangeSheetName, (i + 2), 4, cellValue));
                                double tempStepsValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentParameterRangeSheetName, (i + 2), 5, cellValue));
                                double tempStepLimitValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentParameterRangeSheetName, (i + 2), 6, cellValue));

                                parameterNames[i] = tempName;
                                parameterMin[i] = tempMinValue;
                                parameterMax[i] = tempMaxValue;
                                parameterStep[i] = tempStepsValue;
                                parameterStepLimit[i] = tempStepLimitValue;
                            }

                            TestPopulation.kParameterSiteIndex = parameterSiteIndex;
                            TestPopulation.kParameterNames = parameterNames;
                            TestPopulation.kParameterMin = parameterMin;
                            TestPopulation.kParameterMax = parameterMax;
                            TestPopulation.kParameterStep = parameterStep;
                            TestPopulation.kParameterStepLimit = parameterStepLimit; // Change 1: Add the lower limit or minimum value of parameter step.
                        }
                        else
                        {
                            totalParameterNumber = (int)(Convert.ToDouble(myExcelEdit.GetCellValue(currentParameterRangeSheetName, 2, 10, cellValue)));

                            double[] parameterMin = new double[currentSites.Count * currentYears.Count];
                            double[] parameterMax = new double[currentSites.Count * currentYears.Count];
                            double[] parameterStep = new double[currentSites.Count * currentYears.Count];
                            parameterSiteIndex = new int[currentSites.Count * currentYears.Count];//Create an integer list to save the site index, for example CF07=1, CF08=2.
                            int index = 0;//An integer to act as the index of existing site index, min value, max value.

                            for (int i = 0; i <= totalParameterNumber - 1; i++)
                            {
                                bool yearExisting = false;
                                bool siteExisting = false;

                                string tempYear = Convert.ToString(myExcelEdit.GetCellValue(currentParameterRangeSheetName, (i + 2), 3, cellValue));
                                string tempSite = Convert.ToString(myExcelEdit.GetCellValue(currentParameterRangeSheetName, (i + 2), 4, cellValue));

                                foreach (string currentYear in currentYears)
                                {
                                    if (currentYear == tempYear)
                                    {
                                        yearExisting = true;
                                    }
                                }
                                //To check which years exist.

                                foreach (string currentSite in currentSites)
                                {
                                    if (currentSite == tempSite)
                                    {
                                        siteExisting = true;
                                    }
                                }
                                //To check which sites exist.

                                if (yearExisting & siteExisting)
                                {
                                    int tempSiteIndex = Convert.ToInt16(myExcelEdit.GetCellValue(currentParameterRangeSheetName, (i + 2), 1, cellValue));
                                    double tempMinValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentParameterRangeSheetName, (i + 2), 5, cellValue));
                                    double tempMaxValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentParameterRangeSheetName, (i + 2), 6, cellValue));
                                    double tempStepsValue = Convert.ToDouble(myExcelEdit.GetCellValue(currentParameterRangeSheetName, (i + 2), 7, cellValue));

                                    parameterSiteIndex[index] = tempSiteIndex;
                                    parameterMin[index] = tempMinValue;
                                    parameterMax[index] = tempMaxValue;
                                    parameterStep[index] = tempStepsValue;

                                    index++;
                                }
                                else
                                {
                                    continue;
                                }
                            }

                            TestPopulation.kParameterSiteIndex = parameterSiteIndex;
                            TestPopulation.kParameterMin = parameterMin;
                            TestPopulation.kParameterMax = parameterMax;
                            TestPopulation.kParameterStep = parameterStep;
                        }

                        //3.0 Close the observation file
                        myExcelEdit.Close();
                        #endregion

                        #endregion

                        #region 3.7.5. Create the file that saves the optimal parameter set that maximize the total fitness value.
                        string optimalFileName = "_OptimalParameterSet";
                        string optimalFileExtention = ".txt";

                        string wholeOptimalFileName;

                        if (calibrationRoundID != "SoilN")
                        {
                            //wholeOptimalFileName = workDirectory + genericAlgorithmsDirectory + resultsDirectory + (roundID + 1) + ". " + calibrationRoundID + optimalFileName + "_" + currentCultivarNames[0] + optimalFileExtention;//Assign the path of the text file to save the model outputs concerned.                       
                            wholeOptimalFileName = workDirectory + genericAlgorithmsDirectory + resultsDirectory + "Round" + "." + testIndex + "_" + currentCultivarNames[0] + optimalFileName + "_" + (roundID + 1) + ". " + calibrationRoundID + optimalFileExtention;//Change for multiple rounds of test to see the reliability of the calibration results.                       
                        }
                        else
                        {
                            wholeOptimalFileName = workDirectory + genericAlgorithmsDirectory + resultsDirectory + (roundID + 1) + ". " + calibrationRoundID + optimalFileName + "_" + currentSites[0] + "_" + currentYears[0] + optimalFileExtention;//Assign the path of the text file to save the model outputs concerned.
                        }

                        TestPopulation.dataDirectory = dataDirectory;
                        TestPopulation.workDirectory = workDirectory;
                        TestPopulation.projectDirectory = projectDirectory;
                        TestPopulation.genericAlgorithmsDirectory = genericAlgorithmsDirectory;
                        TestPopulation.wholeOptimalFileName = wholeOptimalFileName;
                        //Create a txt file to save the final optimal parameter set.

                        File.WriteAllText(wholeOptimalFileName, "");
                        //Clean up the old contents.
                        #endregion

                        #region 3.7.6. Call the main function of GA procedure.
                        ArrayList optimalParameterSet = new ArrayList();
                        optimalParameterSet.Clear();

                        TestPopulation.PopulationGeneration();
                        TestPopulation.WriteNextGeneration(out optimalParameterSet);
                        //Generate the initial population.

                        TestPopulation.stuckGenerationNumber = 0;
                        //Set the stuck generation nubmer to 0 after one population or one cultivar has been calibrated.

                        for (int i = 1; i < totalNumberofGeneration; i++)
                        {
                            optimalParameterSet.Clear();

                            TestPopulation.NextGeneration(rangeRatio, out newRangeRatio);

                            rangeRatio = newRangeRatio;

                            TestPopulation.WriteNextGeneration(out optimalParameterSet);
                        }
                        //Run the rest generations.

                        #endregion

                        #region 3.7.7. Change input files with the optimal paramter values found in the above, then start a new round of calibration...

                        //string variteyRoot = "VarietyFile/ItemsArray";
                        ////string soilRoot = "SoilFile/ItemsArray";
                        //string parameterRoot = "ParameterFile/ItemsArray";

                        //switch (calibrationRoundID)
                        //{
                        //    #region 3.7.7.1 Change input file with optimal parameter values of phenology.
                        //    case "Phenology":

                        //        myXMLEdit2.ChangeVarietyAndParameterNodeValue(varieyItems, parameterNames, wholeCurrentVarietyFilePath, wholeCurrentParameterFilePath, variteyRoot, parameterRoot, optimalParameterSet);

                        //        break;
                        //    #endregion

                        //    #region 3.7.7.2 Change input file with optimal parameter values of leaf area index(LAI).
                        //    case "LLE":

                        //        myXMLEdit2.ChangeVarietyAndParameterNodeValue(varieyItems, parameterNames, wholeCurrentVarietyFilePath, wholeCurrentParameterFilePath, variteyRoot, parameterRoot, optimalParameterSet);

                        //        break;
                        //    #endregion

                        //    #region 3.7.7.3 Change input file with optimal parameter values of light use efficiency(LUE).
                        //    case "LUE":
                        //        myXMLEdit2.ChangeVarietyAndParameterNodeValue(varieyItems, parameterNames, wholeCurrentVarietyFilePath, wholeCurrentParameterFilePath, variteyRoot, parameterRoot, optimalParameterSet);
                        //        break;
                        //    #endregion

                        //    #region 3.7.7.4 Change input file with optimal parameter values of N allocation in stems, leaves, and grains.
                        //    case "NAllocation":
                        //        myXMLEdit2.ChangeVarietyAndParameterNodeValue(varieyItems, parameterNames, wholeCurrentVarietyFilePath, wholeCurrentParameterFilePath, variteyRoot, parameterRoot, optimalParameterSet);
                        //        break;
                        //    #endregion

                        //    #region 3.7.7.5 Change input file with optimal parameter values of soil drought factor.
                        //    case "SoilDrought":
                        //        myXMLEdit2.ChangeVarietyAndParameterNodeValue(varieyItems, parameterNames, wholeCurrentVarietyFilePath, wholeCurrentParameterFilePath, variteyRoot, parameterRoot, optimalParameterSet);
                        //        break;
                        //    #endregion
                        //}

                        #endregion
                        //
                    }

                    #endregion
                }
                #endregion
            }
        }
    }
}
