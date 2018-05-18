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

namespace GeneticAlgorithms
{
    public static class AutomaticModelRun
    {

        /// <summary>
        /// Start this program.
        /// </summary>
        public static void ModelRun(
            ArrayList TheArray,
            string calibrationRoundID,
            string dataDirectory,
            string workDirectory,
            string projectDirectory,
            string wholeOutputFileName,
            int[] parameterSiteIndex,
            ArrayList observationDateArray,
            ArrayList observationSeperator
            )
        {
            #region 1. Project path     
            /// <summary>
            /// Path to the projects.
            /// </summary>
            string projectPath = projectDirectory;
            string outputDirectory = dataDirectory;
            #endregion

            #region 2. Simulation options
            var executePath = Environment.CurrentDirectory; // The executable path.
            var startDate = DateTime.Now; // Reccord running time.
            #endregion

            #region 3. Run the project.

            try
            {
                var absoluteProjectPath = FileHelper.GetAbsolute(executePath, projectPath);
                ProjectFile.Load(absoluteProjectPath); // Loading the project.
                
                var initialOutputVersion = ProjectFile.This.FileContainer.OutputVersion; // Save initial output version.

                    try
                    {
                        ProjectFile.This.FileContainer.OutputVersion = OutputVersion.V15; // Ensure Output version 1.5, as it is the only one supporting sensitivity output.

                        int runTreatmentIndicator = 0;

                        ForeEachMultiRun("Model run", false, false, "", outputDirectory, dataDirectory, (rimm, i, s) =>
                        {
                            runTreatmentIndicator++; //Increase the run treatment indicator to tell which treatment is being running.

                            var multiRunItem = rimm.MultiRuns[i]; // Get mutli run item defintion.
                            var runFileName = multiRunItem.DailyOutputFileName.Replace(".sqsro", ".sqsrs"); // Get summary output file name.
                            var runName = runFileName.Replace(".sqsro", "");

                            var variety = ProjectFile.This.FileContainer.VarietyFile[multiRunItem.VarietyItemSelected];
                            var parameter = ProjectFile.This.FileContainer.ParameterFile[multiRunItem.ParameterItemSelected];
                            var soil = ProjectFile.This.FileContainer.SoilFile[multiRunItem.SoilItemSelected];

                            #region Processing the date list, where all of the sampling dates are saved.
                            List<DateTime> tempDates = new List<DateTime>();//List to save the dates of each treatment in the run file.
                            if (observationDateArray.Count != 0)
                            {
                                int samplingStaringPoint = 0;
                                int samplingEndingPoint = 0;

                                if (runTreatmentIndicator == 1)
                                {
                                    samplingStaringPoint = 0;
                                    samplingEndingPoint = (int)observationSeperator[(runTreatmentIndicator - 1)] - 1;
                                }
                                else
                                {
                                    samplingStaringPoint = (int)observationSeperator[(runTreatmentIndicator - 2)];
                                    samplingEndingPoint = (int)observationSeperator[(runTreatmentIndicator - 1)] - 1;
                                }

                                for (int samplingID = samplingStaringPoint; samplingID <= samplingEndingPoint; samplingID++)
                                {
                                    DateTime tempDate = (DateTime)observationDateArray[samplingID];
                                    tempDates.Add(tempDate);
                                }
                            }
                            #endregion

                            //Only deal with the sampling date array for multiple observation variables, i.e. this part of code doesn't work for end-season observations.

                            switch (calibrationRoundID)
                            {
                                #region 3.1 Phenology calibration.
                                case "Phenology":

                                    int indexAMNLFNO = 0; // Index of the AMNLFNO parameter in the array above.
                                    int indexPhyllochron = 1; // Index of the Phyllochron parameter in the array above.
                                    int indexSLDL = 2; // Index of the SLDL parameter in the array above.
                                    int indexVAI = 3; // Index of the VAI parameter in the array above.
                                    //int indexVBEE = 4; // Index of the VBEE parameter in the array above.
                                    //int indexTTsoem = 5; // Index of the TTsome parameter in the array above.
                                    //int indexAMXLFNO = 6; // Index of the AMXLFNO parameter in the array above.

                                    double AMNLFNOValue = (double)TheArray[indexAMNLFNO];
                                    double PhyllochronValue = (double)TheArray[indexPhyllochron];
                                    double SLDLValue = (double)TheArray[indexSLDL];
                                    double VAIValue = (double)TheArray[indexVAI];
                                    //double VBEEValue = (double)TheArray[indexVBEE];
                                    //double TTsoemValue = (double)TheArray[indexTTsoem];
                                    //double AMXLFNOValue = (double)TheArray[indexAMXLFNO];

                                    try
                                    {
                                        parameter.AMNLFNO = AMNLFNOValue;
                                        variety.Phyllochron = PhyllochronValue;
                                        variety.SLDL = SLDLValue;
                                        variety.VAI = VAIValue;
                                        //variety.VBEE = VBEEValue;
                                        //variety.TTsoem = TTsoemValue;
                                        //variety.AMXLFNO = AMXLFNOValue;
                                        // Assign variety parameters.

                                        rimm.StepRun(s, i);
                                        // Run the model with the random values loaded above.

                                        var runObject = RunCore.RunInstance;
                                        var universes = runObject.SavedUniverses;
                                        var lastUniverse = universes[universes.Count - 1];
                                        var anthesisDate_Date = lastUniverse.Calendar_[GrowthStage.ZC_65_Anthesis];
                                        var anthesisUniverse_DOY = anthesisDate_Date.Value.DayOfYear;

                                        var maturity_Date = lastUniverse.Calendar_[GrowthStage.ZC_92_Maturity];
                                        var maturityUnivere = runObject.GetUniverse(maturity_Date.Value); //Get the universe of maturity date.
                                        var finalLeafNumber = Math.Round((maturityUnivere.Crop_.Shoot_.Phytomers_.Number), 2);//Get the final leaf number.

                                        List<double> phenologyOutputs = new List<double>();            
                                        phenologyOutputs.Add(finalLeafNumber);
                                        phenologyOutputs.Add(anthesisUniverse_DOY);

                                        Console.Write(phenologyOutputs[0]);
                                        Console.Write(", ");
                                        Console.Write(phenologyOutputs[1]);

                                        //Console.Write(", ");
                                        //Console.Write(AMNLFNOValue);
                                        //Console.Write(", ");
                                        //Console.Write(PhyllochronValue);
                                        //Console.Write(", ");
                                        //Console.Write(SLDLValue);
                                        //Console.Write(", ");
                                        //Console.Write(VBEEValue);

                                        StreamWriter SW = new StreamWriter(wholeOutputFileName, true);
                                        SW.WriteLine(phenologyOutputs[0]);
                                        SW.WriteLine(phenologyOutputs[1]);
                                        SW.Close();
                                        //Write the concerned model outputs to a text file.

                                    }
                                    finally
                                    {

                                    }

                                break;

                                #endregion

                                #region 3.2 Leaf layer expansion (LLE) calibration (both leaf profile at anthesis and LAI dynamic,6 parameters will be involved).
                                case "LLE":

                                int indexAreaPLL_1 = 0; // Index of the AreaPLL parameter in the array above.
                                int indexNLL_1 = 1; // Index of the NLL parameter in the array above.
                                //int indexRatioFLPL_1 = 1; // Index of the RatioFLPL parameter in the array above.
                                int indexPhyllMBLL_1 = 2; // Index of the PhyllMBLL parameter in the array above.
                                //int indexPhyllSBLL_1 = 4; // Index of the PhyllSBLL parameter in the array above.
                                //int indexMaxDSF_1 = 3; // Index of the MaxDSF parameter in the array above.

                                double AreaPLLValue_1 = (double)TheArray[indexAreaPLL_1];
                                double NLLValue_1 = (double)TheArray[indexNLL_1];
                                //double RatioFLPLValue_1 = (double)TheArray[indexRatioFLPL_1];
                                double PhyllMBLLValue_1 = (double)TheArray[indexPhyllMBLL_1];
                                //double PhyllSBLLValue_1 = (double)TheArray[indexPhyllSBLL_1];
                                //double MaxDSFValue_1 = (double)TheArray[indexMaxDSF_1];

                                try
                                {
                                    List<double> LLEOutputs = new List<double>();//List to save outputs of LAI.

                                    variety.AreaPLL = AreaPLLValue_1;
                                    variety.NLL = NLLValue_1;
                                    //variety.RatioFLPL = RatioFLPLValue_1;
                                    parameter.PhyllMBLL = PhyllMBLLValue_1;
                                    //parameter.PhyllSBLL = PhyllSBLLValue_1;
                                    //parameter.MaxDSF = MaxDSFValue_1;
                                    // Assign variety parameters.

                                    rimm.StepRun(s, i);

                                    #region 3.2.1.Get the objects.
                                    var runObject = RunCore.RunInstance;
                                    var universes = runObject.SavedUniverses;
                                    var lastUniverse = universes[universes.Count - 1];

                                    var anthesis_Date = lastUniverse.Calendar_[GrowthStage.ZC_65_Anthesis];
                                    var anthesisUnivere = runObject.GetUniverse(anthesis_Date.Value); //Get the universe of maturity date.
                                    DateTime maturity_Date = (lastUniverse.Calendar_[GrowthStage.ZC_92_Maturity]).Value; //Get the date of maturity.

                                    int finalLeafNum = (int)lastUniverse.Crop_.Shoot_.Phytomers_.FinalLeafNumber;//Get the final leaf number.
                                    //Console.WriteLine(finalLeafNum);
                                    #endregion

                                    #region 3.2.2.Save the outputs of leaf profile at anthesis.
                                    double currentLAI = 0;
                                    for (var j = finalLeafNum - 1; j >= finalLeafNum - 5; --j)
                                    {
                                        var ll = anthesisUnivere.Crop_.Phytomers_.GetLeafLayer(j);//Get the leaf layer number.

                                        if (ll != null)
                                        {
                                            currentLAI = (double)ll.GetLeafLamina().AreaIndex;
                                            if (currentLAI == 0)
                                            {
                                                currentLAI = -999;
                                            }
                                        }
                                        else
                                        {
                                            currentLAI = -999;
                                        }

                                        currentLAI = Math.Round(currentLAI, 4);
                                        LLEOutputs.Add(currentLAI);//Save the outputs in a list.
                                    }
                                    //Get the required model outputs, or LAI of the top five leaf layers.
                                    #endregion

                                    #region 3.2.3.Save the LAI dynamic data.
                                    List<DateTime> dates = tempDates;//List to save dates.

                                    for (int date_i = 0; date_i < dates.Count; date_i++)
                                    {

                                        if (DateTime.Compare(maturity_Date, dates[date_i]) < 0)
                                        {
                                            dates[date_i] = maturity_Date;//If the date is beyond maturity date, there is no observation.
                                        }

                                        var Univere_date = runObject.GetUniverse(dates[date_i]); //Get the universe of date.
                                        var LLE_date_LAI = Math.Round(Univere_date.Crop_.Phytomers_.LaminaeAI, 2);//Get the LAI value at different dates.

                                        if (LLE_date_LAI == 0)
                                        {
                                            LLE_date_LAI = -999;
                                        }

                                        LLEOutputs.Add(LLE_date_LAI);//Save the outputs in a list.
                                    }
                                    //Get the required model outputs.
                                    #endregion

                                    #region 3.2.4.Save the model outputs in a text file.
                                    StreamWriter SW = new StreamWriter(wholeOutputFileName, true);
                                    
                                    for (int j = 0; j < LLEOutputs.Count; j++)
                                    {
                                        if (j != (LLEOutputs.Count - 1))
                                        {
                                            Console.Write("");
                                            //Console.Write(LLEOutputs[j]);
                                            //Console.Write(", ");
                                        }
                                        else
                                        {
                                            Console.Write("");
                                            //Console.Write(LLEOutputs[j]);
                                        }

                                        SW.WriteLine(LLEOutputs[j]);
                                    }
                                    SW.Close();
                                    //Write the simulation outputs in the temporary text file.
                                    #endregion

                                }
                                finally
                                {

                                }

                                break;
                                #endregion

                                #region 3.2.1 Leaf layer expansion (LLE1) calibration (Leaf profile at anthesis, 3 parameters will be involved).
                                case "LLE1":

                                int indexAreaPLL = 0; // Index of the AreaPLL parameter in the array above.
                                int indexNLL = 1; // Index of the NLL parameter in the array above.
                                int indexRatioFLPL = 2; // Index of the RatioFLPL parameter in the array above.

                                double AreaPLLValue = (double)TheArray[indexAreaPLL];
                                double NLLValue = (double)TheArray[indexNLL];
                                double RatioFLPLValue = (double)TheArray[indexRatioFLPL];

                                try
                                {
                                    List<double> LLE1Outputs = new List<double>();//List to save outputs of LAI.

                                    variety.AreaPLL = AreaPLLValue;
                                    variety.NLL = NLLValue;
                                    variety.RatioFLPL = RatioFLPLValue;
                                    // Assign variety parameters.

                                    rimm.StepRun(s, i);

                                    var runObject = RunCore.RunInstance;
                                    var universes = runObject.SavedUniverses;
                                    var lastUniverse = universes[universes.Count - 1];

                                    var anthesis_Date = lastUniverse.Calendar_[GrowthStage.ZC_65_Anthesis];
                                    var anthesisUnivere = runObject.GetUniverse(anthesis_Date.Value); //Get the universe of maturity date.

                                    int finalLeafNum = (int)lastUniverse .Crop_.Shoot_.Phytomers_.FinalLeafNumber;//Get the final leaf number.
                                    //Console.WriteLine(finalLeafNum);

                                    double currentLAI = 0;

                                    for (var j = finalLeafNum - 1; j >= finalLeafNum - 5; --j)
                                    {
                                        var ll = anthesisUnivere.Crop_.Phytomers_.GetLeafLayer(j);//Get the leaf layer number.

                                        if (ll != null)
                                        {
                                            currentLAI = (double)ll.GetLeafLamina().AreaIndex;
                                            if (currentLAI == 0)
                                            {
                                                currentLAI = -999;
                                            }
                                        }
                                        else
                                        {
                                            currentLAI = -999;
                                        }

                                        currentLAI = Math.Round(currentLAI, 4);
                                        LLE1Outputs.Add(currentLAI);//Save the outputs in a list.
                                    }
                                    //Get the required model outputs, or LAI of the top five leaf layers.

                                    StreamWriter SW = new StreamWriter(wholeOutputFileName, true);

                                    for (int j = 0; j < LLE1Outputs.Count; j++)
                                    {
                                        if (j != (LLE1Outputs.Count - 1))
                                        {
                                            Console.Write(LLE1Outputs[j]);
                                            Console.Write(", ");
                                        }
                                        else
                                        {
                                            Console.Write(LLE1Outputs[j]);
                                        }

                                        SW.WriteLine(LLE1Outputs[j]);
                                    }
                                    SW.Close();
                                    //Write the simulation outputs in the temporary text file.

                                    //Console.Write("| ");
                                    //Console.Write(NLLValue);
                                    //Console.Write(", ");
                                    //Console.Write(NLLValue);
                                    //Console.Write(", ");
                                    //Console.Write(PhyllMBLLValue);
                                    //Console.Write(", ");
                                    //Console.Write(PhyllSBLLValue);
                                }
                                finally
                                {

                                }

                                break;
                                #endregion

                                #region 3.2.2 Leaf layer expansion (LLE2) calibration (LAI dynamic, 3 parameters will be involved).
                                case "LLE2":

                                int indexPhyllMBLL = 0; // Index of the PhyllMBLL parameter in the array above.
                                int indexPhyllSBLL = 1; // Index of the PhyllSBLL parameter in the array above.
                                int indexMaxDSF = 2; // Index of the MaxDSF parameter in the array above.

                                double PhyllMBLLValue = (double)TheArray[indexPhyllMBLL];
                                double PhyllSBLLValue = (double)TheArray[indexPhyllSBLL];
                                double MaxDSFValue = (double)TheArray[indexMaxDSF];

                                try
                                {
                                    List<double> LLE2Outputs = new List<double>();//List to save outputs of LAI.

                                    parameter.PhyllMBLL = PhyllMBLLValue;
                                    parameter.PhyllSBLL = PhyllSBLLValue;
                                    parameter.MaxDSF = MaxDSFValue;
                                    // Assign variety parameters.

                                    rimm.StepRun(s, i);

                                    var runObject = RunCore.RunInstance;
                                    var universes = runObject.SavedUniverses;
                                    var lastUniverse = universes[universes.Count - 1];
                                    DateTime maturity_Date = (lastUniverse.Calendar_[GrowthStage.ZC_92_Maturity]).Value;

                                    List<DateTime> dates = tempDates;//List to save dates.

                                    for (int date_i = 0; date_i < dates.Count; date_i++)
                                    {

                                        if (DateTime.Compare(maturity_Date, dates[date_i]) < 0)
                                        {
                                            dates[date_i] = maturity_Date;//If the date is beyond maturity date, there is no observation.
                                        }

                                        var Univere_date = runObject.GetUniverse(dates[date_i]); //Get the universe of date.
                                        var LLE_date_LAI = Math.Round(Univere_date.Crop_.Phytomers_.LaminaeAI, 2);//Get the LAI value at different dates.

                                        if (LLE_date_LAI == 0)
                                        {
                                            LLE_date_LAI = -999;
                                        }

                                        LLE2Outputs.Add(LLE_date_LAI);//Save the outputs in a list.
                                    }
                                    //Get the required model outputs.

                                    StreamWriter SW = new StreamWriter(wholeOutputFileName, true);

                                    for (int j = 0; j < LLE2Outputs.Count; j++)
                                    {
                                        if (j != (LLE2Outputs.Count - 1))
                                        {
                                            Console.Write(LLE2Outputs[j]);
                                            Console.Write(", ");
                                        }
                                        else
                                        {
                                            Console.Write(LLE2Outputs[j]);
                                        }

                                        SW.WriteLine(LLE2Outputs[j]);
                                    }
                                    SW.Close();
                                    //Write the simulation outputs in the temporary text file.

                                    //Console.Write("| ");
                                    //Console.Write(NLLValue);
                                    //Console.Write(", ");
                                    //Console.Write(NLLValue);
                                    //Console.Write(", ");
                                    //Console.Write(PhyllMBLLValue);
                                    //Console.Write(", ");
                                    //Console.Write(PhyllSBLLValue);
                                }
                                finally
                                {

                                }

                                break;
                                #endregion

                                #region 3.3 LUE calibration (6 parameters will be involved).
                                case "LUE":

                                var paramNames_LUE = new[] { "Kl", "LueDiffuse", "TauSLN", "Topt", "TTaegf", "TTcd" }; // Parameters names which are perturbed.
                                var paramValues_LUE = new[] { double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN }; // Parameters values which are perturbed.

                                int indexKl = 0; // Index of the Kl parameter in the array above.
                                int indexLueDiffuse = 1; // Index of the LueDiffuse parameter in the array above.
                                int indexTauSLN = 2; // Index of the TauSLN parameter in the array above.
                                int indexTopt = 3; // Index of the Topt parameter in the array above.
                                int indexTTaegf = 4; // Index of the TTaegf parameter in the array above.
                                int indexTTcd = 5; // Index of the TTcd parameter in the array above.

                                double KlValue = (double)TheArray[indexKl];
                                double LueDiffuseValue = (double)TheArray[indexLueDiffuse];
                                double TauSLNValue = (double)TheArray[indexTauSLN];
                                double ToptValue = (double)TheArray[indexTopt];
                                double TTaegfValue = (double)TheArray[indexTTaegf];
                                double TTcdValue = (double)TheArray[indexTTcd];

                                // Save initial values.
                                var initialKl = variety.Kl;
                                var initialLueDiffuse = variety.LueDiffuse;
                                var initialTauSLN = parameter.TauSLN;
                                var initialTopt = parameter.Topt;
                                var initialTTaegf = variety.TTaegf;
                                var initialTTcd = parameter.TTcd;

                                try
                                {
                                    // Create a new output file.
                                    var page = OutputFile.ExtractSensitivityRunHeader(paramNames_LUE); // Generate header of the sensitivity file.
                                    // var page = new PageData(); // Use this line to create the page if you don't want the header.

                                    List<double> LUEOutputs = new List<double>();//List to save outputs of LAI.

                                    variety.Kl = KlValue;
                                    variety.LueDiffuse = LueDiffuseValue;
                                    parameter.TauSLN = TauSLNValue;
                                    parameter.Topt = ToptValue;
                                    variety.TTaegf = TTaegfValue;
                                    parameter.TTcd = TTcdValue;
                                    // Assign variety parameters.

                                    paramValues_LUE[indexKl] = KlValue;
                                    paramValues_LUE[indexLueDiffuse] = LueDiffuseValue;
                                    paramValues_LUE[indexTauSLN] = TauSLNValue;
                                    paramValues_LUE[indexTopt] = ToptValue;
                                    paramValues_LUE[indexTTaegf] = TTaegfValue;
                                    paramValues_LUE[indexTTcd] = TTcdValue;
                                    // Save the random values in tha vector of paramValues and will be saved in the output file.

                                    rimm.StepRun(s, i);

                                    var runObject = RunCore.RunInstance;
                                    var universes = runObject.SavedUniverses;
                                    var lastUniverse = universes[universes.Count - 1];
                                    DateTime maturity_Date = (lastUniverse.Calendar_[GrowthStage.ZC_92_Maturity]).Value;

                                    List<DateTime> dates = tempDates;//List to save dates.

                                    for (int date_i = 0; date_i < dates.Count; date_i++)
                                    {

                                        if (DateTime.Compare(maturity_Date, dates[date_i]) < 0)
                                        {
                                            dates[date_i] = maturity_Date;//If the date is beyond maturity date, there is no observation.
                                        }

                                        var Univere_date = runObject.GetUniverse(dates[date_i]); //Get the universe of date.
                                        var LUE_date_CropDM = Math.Round(Univere_date.Crop_.OutputTotalDM * 10, 0);//Get the crop dry matter value at different dates.

                                        LUEOutputs.Add(LUE_date_CropDM);//Save the outputs in a list.
                                    }
                                    //Get the required model outputs.

                                    StreamWriter SW = new StreamWriter(wholeOutputFileName, true);

                                    for (int j = 0; j < LUEOutputs.Count; j++)
                                    {
                                        if (j != (LUEOutputs.Count - 1))
                                        {
                                            Console.Write(LUEOutputs[j]);
                                            Console.Write(", ");
                                        }
                                        else
                                        {
                                            Console.Write(LUEOutputs[j]);
                                        }

                                        SW.WriteLine(LUEOutputs[j]);
                                    }
                                    SW.Close();

                                    //Console.Write(", ");
                                    //Console.Write(KlValue);
                                    //Console.Write(", ");
                                    //Console.Write(LueDiffuseValue);
                                    //Console.Write(", ");
                                    //Console.Write(TauSLNValue);
                                    //Console.Write(", ");
                                    //Console.Write(ToptValue);
                                    //Console.Write(", ");
                                    //Console.Write(TTaegfValue);
                                    //Console.Write(", ");
                                    //Console.Write(TTcdValue);
                                }
                                finally
                                {
                                    // Restore initial values.
                                    variety.Kl = initialKl;
                                    variety.LueDiffuse = initialLueDiffuse;
                                    parameter.TauSLN = initialTauSLN;
                                    parameter.Topt = initialTopt;
                                    variety.TTaegf = initialTTaegf;
                                    parameter.TTcd = initialTTcd;
                                }

                                break;
                                #endregion

                                #region 3.4 NAllocation calibration (4 parameters will be involved).
                                case "NAllocation":

                                var paramNames_NAllocation = new[] { "MaxStemN", "SLNcri", "SLNmax0", "StrucStemN" }; // Parameters names which are perturbed.
                                var paramValues_NAllocation = new[] { double.NaN, double.NaN, double.NaN, double.NaN }; // Parameters values which are perturbed.

                                int indexMaxStemN = 0; // Index of the MaxStemN parameter in the array above.
                                int indexSLNcri = 1; // Index of the SLNcri parameter in the array above.
                                int indexSLNmax0 = 2; // Index of the SLNmax0 parameter in the array above.
                                int indexStrucStemN = 3; // Index of the StrucStemN parameter in the array above.

                                double MaxStemNValue = (double)TheArray[indexMaxStemN];
                                double SLNcriValue = (double)TheArray[indexSLNcri];
                                double SLNmax0Value = (double)TheArray[indexSLNmax0];
                                double StrucStemNValue = (double)TheArray[indexStrucStemN];

                                if (StrucStemNValue > MaxStemNValue)
                                {
                                    StrucStemNValue = 0.8 * MaxStemNValue;
                                    TheArray[indexStrucStemN] = 0.8 * MaxStemNValue;
                                }

                                if (SLNcriValue > SLNmax0Value)
                                {
                                    SLNcriValue = 0.8 * SLNmax0Value;
                                    TheArray[indexSLNcri] = 0.8 * SLNmax0Value;
                                }
                                //Change 4: The situations of "StrucStemNValue > MaxStemNValue" and "SLNcriValue > SLNmax0Value" should be avoided to prevent possible program collapse.

                                // Save initial values.
                                var initialMaxStemN = parameter.MaxStemN;
                                var initialSLNcri = parameter.SLNcri;
                                var initialSLNmax0 = parameter.SLNmax0;
                                var initialStrucStemN = parameter.StrucStemN;

                                //Console.WriteLine("*********");
                                //double Kl = variety.Kl;
                                //Console.WriteLine(Kl);
                                //Console.WriteLine("*********");

                                try
                                {
                                    // Create a new output file.
                                    var page = OutputFile.ExtractSensitivityRunHeader(paramNames_NAllocation); // Generate header of the sensitivity file.
                                    // var page = new PageData(); // Use this line to create the page if you don't want the header.

                                    List<double> NAllocationOutputs = new List<double>();//List to save outputs of Leaf N, Stem N, and Grain N.

                                    parameter.MaxStemN = MaxStemNValue;
                                    parameter.SLNcri = SLNcriValue;
                                    parameter.SLNmax0 = SLNmax0Value;
                                    parameter.StrucStemN = StrucStemNValue;
                                    // Assign variety parameters.

                                    paramValues_NAllocation[indexMaxStemN] = MaxStemNValue;
                                    paramValues_NAllocation[indexSLNcri] = SLNcriValue;
                                    paramValues_NAllocation[indexSLNmax0] = SLNmax0Value;
                                    paramValues_NAllocation[indexStrucStemN] = StrucStemNValue;
                                    // Save the random values in tha vector of paramValues and will be saved in the output file.

                                    rimm.StepRun(s, i);

                                    var runObject = RunCore.RunInstance;
                                    var universes = runObject.SavedUniverses;
                                    var lastUniverse = universes[universes.Count - 1];
                                    DateTime maturity_Date = (lastUniverse.Calendar_[GrowthStage.ZC_92_Maturity]).Value;

                                    List<DateTime> dates = tempDates;//List to save dates.

                                    #region Obtain crop N observations.
                                    for (int date_i = 0; date_i < dates.Count; date_i++)
                                    {
                                        if (DateTime.Compare(maturity_Date, dates[date_i]) < 0)
                                        {
                                            dates[date_i] = maturity_Date;//If the date is beyond maturity date, there is no observation.
                                        }

                                        var Univere_date = runObject.GetUniverse(dates[date_i]); //Get the universe of date.
                                        var NAllocation_date_CropN = Math.Round((Univere_date.Crop_.TotalN * 10), 2);//Get the crop N value at different dates.

                                        NAllocationOutputs.Add(NAllocation_date_CropN);//Save the crop N in a list.
                                    }
                                    #endregion

                                    #region Obtain leaf N observations.
                                    //for (int date_i = 0; date_i < dates.Count; date_i++)
                                    //{
                                    //    if (DateTime.Compare(maturity_Date, dates[date_i]) < 0)
                                    //    {
                                    //        dates[date_i] = maturity_Date;//If the date is beyond maturity date, there is no observation.
                                    //    }

                                    //    var Univere_date = runObject.GetUniverse(dates[date_i]); //Get the universe of date.
                                    //    var NAllocation_date_LeafN = Math.Round((Univere_date.Crop_.Phytomers_.OutputLaminaeN * 10), 2);//Get the leaf N value at different dates.

                                    //    NAllocationOutputs.Add(NAllocation_date_LeafN);//Save the leaf N in a list.
                                    //}
                                    #endregion

                                    #region Obtain stem N observations.
                                    //for (int date_i = 0; date_i < dates.Count; date_i++)
                                    //{

                                    //    if (DateTime.Compare(maturity_Date, dates[date_i]) < 0)
                                    //    {
                                    //        dates[date_i] = maturity_Date;//If the date is beyond maturity date, there is no observation.
                                    //    }

                                    //    var Univere_date = runObject.GetUniverse(dates[date_i]); //Get the universe of date.
                                    //    var NAllocation_date_StemN = Math.Round(((Univere_date.Crop_.Phytomers_.OutputSheathN + Univere_date.Crop_.Stem_.N.Total) * 10), 2);//Get the stem N (including stem and sheath) value at different dates.

                                    //    NAllocationOutputs.Add(NAllocation_date_StemN);//Save the stem N in a list.
                                    //}
                                    #endregion

                                    #region Obtain grain N observations.
                                    //for (int date_i = 0; date_i < dates.Count; date_i++)
                                    //{
                                    //    if (DateTime.Compare(maturity_Date, dates[date_i]) < 0)
                                    //    {
                                    //        dates[date_i] = maturity_Date;//If the date is beyond maturity date, there is no observation.
                                    //    }

                                    //    var Univere_date = runObject.GetUniverse(dates[date_i]); //Get the universe of date.
                                    //    var NAllocation_date_GrainN = Math.Round((Univere_date.Crop_.Grain_.TotalN * 10), 2);//Get the grain N value at different dates.

                                    //    NAllocationOutputs.Add(NAllocation_date_GrainN);//Save the grain N in a list.
                                    //}
                                    #endregion

                                    //Get the required model outputs.

                                    StreamWriter SW = new StreamWriter(wholeOutputFileName, true);

                                    for (int j = 0; j < NAllocationOutputs.Count; j++)
                                    {
                                        if (j != (NAllocationOutputs.Count - 1))
                                        {
                                            Console.Write(NAllocationOutputs[j]);
                                            Console.Write(", ");
                                        }
                                        else
                                        {
                                            Console.Write(NAllocationOutputs[j]);
                                        }

                                        SW.WriteLine(NAllocationOutputs[j]);
                                    }
                                    SW.Close();

                                    //Console.WriteLine("---------------");
                                    //Console.Write(", ");
                                    //Console.Write(MaxStemNValue);
                                    //Console.Write(", ");
                                    //Console.Write(SLNcriValue);
                                    //Console.Write(", ");
                                    //Console.Write(SLNmax0Value);
                                    //Console.Write(", ");
                                    //Console.Write(StrucStemNValue);
                                    //Console.WriteLine("---------------");
                                }
                                finally
                                {
                                    // Restore initial values.                                   
                                    parameter.MaxStemN = initialMaxStemN;
                                    parameter.SLNcri = initialSLNcri;
                                    parameter.SLNmax0 = initialSLNmax0;
                                    parameter.StrucStemN = initialStrucStemN;
                                }

                                break;
                                #endregion

                                #region 3.5 Soil drought factor calibration (2 parameters will be involved).
                                //case "SoilDrought":

                                //var paramNames_SoilDrought = new[] { "MaxRWU", "MaxDSF", "LowerFTSWsenescence", "LowerFTSWtranspiration" }; // Parameters names which are perturbed.
                                //var paramValues_SoilDrought = new[] { double.NaN, double.NaN, double.NaN, double.NaN };// Parameters values which are perturbed.

                                //int indexMaxRWU = 0; // Index of the MaxRWU parameter in the array above.
                                //int indexMaxDSF = 1; // Index of the MaxDSF parameter in the array above.
                                //int indexLowerFTSWsenescence = 2; // Index of the LowerFTSWsenescence parameter in the array above.
                                //int indexLowerFTSWtranspiration = 3; // Index of the LowerFTSWtranspiration parameter in the array above.

                                //double MaxRWUValue = (double)TheArray[indexMaxRWU];
                                //double MaxDSFValue = (double)TheArray[indexMaxDSF];
                                //double LowerFTSWsenescenceValue = (double)TheArray[indexLowerFTSWsenescence];
                                //double LowerFTSWtranspirationValue = (double)TheArray[indexLowerFTSWtranspiration];

                                //// Save initial values.
                                //var initialMaxRWU = parameter.MaxRWU;
                                //var initialMaxDSF = parameter.MaxDSF;
                                //var initialLowerFTSWsenescence = parameter.LowerFTSWsenescence;
                                //var initialLowerFTSWtranspiration = parameter.LowerFTSWtranspiration;

                                //try
                                //{
                                //    // Create a new output file.
                                //    var page = OutputFile.ExtractSensitivityRunHeader(paramNames_SoilDrought); // Generate header of the sensitivity file.
                                //    // var page = new PageData(); // Use this line to create the page if you don't want the header.

                                //    List<double> SoilDroughtOutputs = new List<double>();//List to save outputs of Crop DM and Crop N.

                                //    parameter.MaxRWU = MaxRWUValue;
                                //    parameter.MaxDSF = MaxDSFValue;
                                //    parameter.LowerFTSWsenescence = LowerFTSWsenescenceValue;
                                //    parameter.LowerFTSWtranspiration = LowerFTSWtranspirationValue;
                                //    // Assign variety parameters.

                                //    paramValues_SoilDrought[indexMaxRWU] = MaxRWUValue;
                                //    paramValues_SoilDrought[indexMaxDSF] = MaxDSFValue;
                                //    paramValues_SoilDrought[indexLowerFTSWsenescence] = LowerFTSWsenescenceValue;
                                //    paramValues_SoilDrought[indexLowerFTSWtranspiration] = LowerFTSWtranspirationValue;
                                //    // Save the random values in tha vector of paramValues and will be saved in the output file.

                                //    rimm.StepRun(s, i);

                                //    var runObject = RunCore.RunInstance;
                                //    var universes = runObject.SavedUniverses;
                                //    var lastUniverse = universes[universes.Count - 1];
                                //    DateTime maturity_Date = (lastUniverse.Calendar_[GrowthStage.ZC_92_Maturity]).Value;

                                //    List<DateTime> dates = tempDates;//List to save dates.

                                //    #region Obtain green leaf area index (GAI) observations.
                                //    for (int date_i = 0; date_i < dates.Count; date_i++)
                                //    {

                                //        if (DateTime.Compare(maturity_Date, dates[date_i]) < 0)
                                //        {
                                //            dates[date_i] = maturity_Date;//If the date is beyond maturity date, there is no observation.
                                //        }

                                //        var Univere_date = runObject.GetUniverse(dates[date_i]); //Get the universe of date.
                                //        var SoilDrought_date_LAI = Math.Round(Univere_date.Crop_.Phytomers_.LaminaeAI, 2);//Get the LAI value at different dates.

                                //        SoilDroughtOutputs.Add(SoilDrought_date_LAI);//Save the outputs in a list.
                                //    }
                                //    #endregion

                                //    #region Obtain crop DM observations.
                                //    //for (int date_i = 0; date_i < dates.Count; date_i++)
                                //    //{
                                //    //    if (DateTime.Compare(maturity_Date, dates[date_i]) < 0)
                                //    //    {
                                //    //        dates[date_i] = maturity_Date;//If the date is beyond maturity date, there is no observation.
                                //    //    }

                                //    //    var Univere_date = runObject.GetUniverse(dates[date_i]); //Get the universe of date.
                                //    //    var NAllocation_date_CropDM = Math.Round((Univere_date.Crop_.OutputTotalDM * 10), 2);//Get the crop DM value at different dates.

                                //    //    SoilDroughtOutputs.Add(NAllocation_date_CropDM);//Save the crop DM in a list.
                                //    //}
                                //    #endregion

                                //    #region Obtain crop N observations.
                                //    //for (int date_i = 0; date_i < dates.Count; date_i++)
                                //    //{
                                //    //    if (DateTime.Compare(maturity_Date, dates[date_i]) < 0)
                                //    //    {
                                //    //        dates[date_i] = maturity_Date;//If the date is beyond maturity date, there is no observation.
                                //    //    }

                                //    //    var Univere_date = runObject.GetUniverse(dates[date_i]); //Get the universe of date.
                                //    //    var NAllocation_date_CropN = Math.Round((Univere_date.Crop_.OutputTotalN * 10), 2);//Get the crop N (including stem and sheath) value at different dates.
                                //    //    SoilDroughtOutputs.Add(NAllocation_date_CropN);//Save the crop N in a list.
                                //    //}
                                //    #endregion

                                //    //Get the required model outputs.

                                //    StreamWriter SW = new StreamWriter(wholeOutputFileName, true);

                                //    for (int j = 0; j < SoilDroughtOutputs.Count; j++)
                                //    {
                                //        if (j != (SoilDroughtOutputs.Count - 1))
                                //        {
                                //            Console.Write(SoilDroughtOutputs[j]);
                                //            Console.Write(", ");
                                //        }
                                //        else
                                //        {
                                //            Console.Write(SoilDroughtOutputs[j]);
                                //        }

                                //        SW.WriteLine(SoilDroughtOutputs[j]);
                                //    }
                                //    SW.Close();

                                //    //Console.Write(", ");
                                //    //Console.Write(MaxRWUValue);
                                //    //Console.Write(", ");
                                //    //Console.Write(MaxDSFValue);
                                //    //Console.Write(", ");
                                //    //Console.Write(LowerFTSWsenescenceValue);
                                //    //Console.Write(", ");
                                //    //Console.Write(LowerFTSWtranspirationValue);
                                //}
                                //finally
                                //{
                                //    // Restore initial values.
                                //    parameter.MaxRWU = initialMaxRWU;
                                //    parameter.MaxDSF = initialMaxDSF;
                                //    parameter.LowerFTSWsenescence = initialLowerFTSWsenescence;
                                //    parameter.LowerFTSWtranspiration = initialLowerFTSWtranspiration;
                                //}

                                //break;
                                #endregion
                            }
                            return false; // Run has not been skipped.
                        }
                        );
                    }
                    finally
                    {
                        ProjectFile.This.FileContainer.OutputVersion = initialOutputVersion; // Restore initial output version.
                    }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            ProjectFile.New();

            #endregion
        }

        /// <summary>
        /// Do all run as multi run.
        /// </summary>
        /// <param name="title">Title of the operation.</param>
        /// <param name="randomParameterValues">Random parameter values loaded from a text file.</param>
        /// <param name="serialize">Serialize output.</param>
        /// <param name="createDailyOutput">Create daily output or not.</param>
        /// <param name="dailyOutputSuffix">Surfix to add to the daily output file.</param>
        /// <param name="outputDirectory">Output directory.</param>
        public static void ForeEachMultiRun(string title, bool serialize, bool createDailyOutput, string dailyOutputSuffix, string outputDirectory, string dataDirectory)
        {
            ForeEachMultiRun(title, serialize, createDailyOutput, dailyOutputSuffix, outputDirectory, dataDirectory, (rimm, i, s) =>
            {
                rimm.StepRun(s, i);
                return false;
            });
        }

        /// <summary>
        /// Do all run as multi run.
        /// </summary>
        /// <param name="title">Title of the operation.</param>
        /// <param name="randomParameterValues">Random parameter values loaded from a text file.</param>
        /// <param name="serialize">Serialize output.</param>
        /// <param name="createDailyOutput">Create daily output or not.</param>
        /// <param name="dailyOutputSuffix">Suufix to add to the daily output file.</param>
        /// <param name="outputDirectory">Output directory.</param>
        /// <param name="doRunItemModeMulti">The action to perform on a given multi run item.</param>
        public static void ForeEachMultiRun(string title, bool serialize, bool createDailyOutput, string dailyOutputSuffix, string outputDirectory, string dataDirectory,
            Func<RunItemModeMulti, int, bool, bool> doRunItemModeMulti)
        {
            //Console.WriteLine(title);
            foreach (var runItem in ProjectFile.This.FileContainer.RunFile.Items) // Iterates over all run items.
            {

                Console.Write("\n{0,20}", runItem.Name);

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
                    multiRun.OutputDirectory = originalOutputDirectory.Replace(dataDirectory, outputDirectory);

                    // Run all multi run items
                    var nbRun = multiRun.InitRun(serialize);
                    Console.Write("[");
                    for (var i = 0; i < nbRun; ++i)
                    {
                        var varietyName = multiRun.MultiRuns[i].VarietyItemSelected;
                        var skipped = doRunItemModeMulti(multiRun, i, serialize);
                        if (!skipped) Console.Write("");
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
                Console.Write("]");
            }
            Console.WriteLine();
        }

    }
}
