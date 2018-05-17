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
using System.Xml.Serialization;
using SiriusModel.InOut.Base;

namespace GlobalSA_Morris
{
    public class AutomaticModelRun
    {
        /// <summary>
        /// Start this program.
        /// </summary>
        public static void ModelRun(
            List<double> inputParameterValues,
            string GSADirectory,
            string projectPath,
            string[] wholeOutputFileNames
            )
        {
            #region 1. Project path
            /// <summary>
            /// Path to the projects.
            /// </summary>
            string outputDirectory = GSADirectory;
            #endregion

            #region 2. Simulation options
            var executePath = Environment.CurrentDirectory; // The executable path.
            var startDate = DateTime.Now; // Reccord running time.
            #endregion

            #region 3. Run the project and save the outputs.
            try
            {
                var absoluteProjectPath = FileHelper.GetAbsolute(executePath, projectPath);
                ProjectFile.Load(absoluteProjectPath); // Loading the project.

                var initialOutputVersion = ProjectFile.This.FileContainer.OutputVersion; // Save initial output version.

                try
                {
                    ProjectFile.This.FileContainer.OutputVersion = OutputVersion.V15; // Ensure Output version 1.5, as it is the only one supporting sensitivity output.

                    ForeEachMultiRun("Model run", false, false, "", outputDirectory, (rimm, i, s) =>
                    {
                        var multiRunItem = rimm.MultiRuns[i]; // Get mutli run item defintion.
                        int multiRunNumber = rimm.MultiRuns.Count;
                        var runFileName = multiRunItem.DailyOutputFileName.Replace(".sqsro", ".sqsrs"); // Get summary output file name.
                        var runName = runFileName.Replace(".sqsro", "");

                        string managementName = multiRunItem.ManagementItem;

                        var variety = ProjectFile.This.FileContainer.VarietyFile[multiRunItem.VarietyItemSelected];
                        var parameter = ProjectFile.This.FileContainer.ParameterFile[multiRunItem.ParameterItemSelected];
                        var soil = ProjectFile.This.FileContainer.SoilFile[multiRunItem.SoilItemSelected];//Get the variety, parameter, and soil file contents.

                        rimm.MultiYear = false;//Change it to single year run, because multiple run was realized in later loop,

                        string miName;
                        var item = rimm.MultiRuns[i];
                        miName = item.ManagementItemSelected;//Get the management name for each treatment.
                        //Console.Write("{0}.", i + 1);
                        Console.Write(miName);

                        if (i != (multiRunNumber - 1))
                        {
                            Console.Write(" ");
                        }
                        else
                        {
                            Console.Write("");
                        }
                        //Format of output on the screen.

                        ManagementItem mi = ProjectFile.This.FileContainer.ManagementFile[miName]; //Get the management file.
                        var firstYear = rimm.FirstYear;
                        var lastYear = rimm.LastYear;//Starting and ending years of multiple year model runs.

                        #region Parameter index of 85 parameters
                        //int index_aSheath = 0; // Index of the Phyllochron parameter in the input parameter value list, and so on.
                        //int index_AreaSLL = 1;
                        //int index_AreaPLL = 2;
                        //int index_AreaSSL = 3;
                        //int index_RatioFLPL = 4;
                        //int index_AMNLFNO = 5;
                        //int index_AMXLFNO = 6;
                        //int index_EarGR = 7;
                        //int index_LueDiffuse = 8;
                        //int index_Kl = 9;
                        //int index_L_IN1 = 10;
                        //int index_L_EP = 11;
                        //int index_NLL = 12;
                        //int index_PhyllFLLAnth = 13;
                        //int index_Phyllochron = 14;
                        //int index_SLDL = 15;
                        //int index_TTaegf = 16;
                        //int index_TTegfm = 17;
                        //int index_TTsoem = 18;
                        //int index_VAI = 19;
                        //int index_VBEE = 20;
                        //int index_AlphaAlbGlo = 21;
                        //int index_AlphaGlu = 22;
                        //int index_AlphaKn = 23;
                        //int index_AlphaNc = 24;
                        //int index_AlphaNNI = 25;
                        //int index_AlphaSSN = 26;
                        //int index_BetaAlbGlo = 27;
                        //int index_BetaGlu = 28;
                        //int index_BetaKn = 29;
                        //int index_BetaNNI = 30;
                        //int index_BetaRWU = 31;
                        //int index_BetaSSN = 32;
                        //int index_Co2FacRue = 33;
                        //int index_DMmaxNuptake = 34;
                        //int index_FracAnth = 35;
                        //int index_FracBEAR = 36;
                        //int index_FracLaminaeBGR = 37;
                        //int index_FracSheathBGR = 38;
                        //int index_FracStemWSC = 39;
                        //int index_IntermTvern = 40;
                        //int index_LowerFTSWbiomass = 41;
                        //int index_LowerFTSWexpansion = 42;
                        //int index_LowerFTSWsenescence = 43;
                        //int index_LowerFTSWtranspiration = 44;
                        //int index_LLOSS = 45;
                        //int index_MaxAnForP = 46;
                        //int index_MaxDSF = 47;
                        //int index_MaxLeafSoil = 48;
                        //int index_MaxLPhyll = 49;
                        //int index_MaxLeafRRNU = 50;
                        //int index_MaxRWU = 51;
                        //int index_MaxStemRRNU = 52;
                        //int index_MaxStemN = 53;
                        //int index_MaxTvern = 54;
                        //int index_MinLPhyll = 55;
                        //int index_MinTvern = 56;
                        //int index_PhyllDecr = 57;
                        //int index_PhyllIncr = 58;
                        //int index_PhyllGroLamina = 59;
                        //int index_PhyllGroInterNode = 60;
                        //int index_PhyllMBLL = 61;
                        //int index_PhyllMSLL = 62;
                        //int index_PhyllSBLL = 63;
                        //int index_PhyllSSLL = 64;
                        //int index_RGRStruc = 65;
                        //int index_RVER = 66;
                        //int index_SLNcri = 67;
                        //int index_SLNmax0 = 68;
                        //int index_SLNmin = 69;
                        //int index_SLWp = 70;
                        //int index_SlopeFR = 71;
                        //int index_SSWp = 72;
                        //int index_StdCo2 = 73;
                        //int index_StrucLeafN = 74;
                        //int index_StrucStemN = 75;
                        //int index_TauSLN = 76;
                        //int index_Topt = 77;
                        //int index_Tmax = 78;
                        //int index_TTcd = 79;
                        //int index_TTer = 80;
                        //int index_UpperFTSWbiomass = 81;
                        //int index_UpperFTSWexpansion = 82;
                        //int index_UpperFTSWsenescence = 83;
                        //int index_UpperFTSWtranspiration = 84;
                        #endregion

                        #region Parameter index of 76 parameters.
                        int index_aSheath = 0; // Index of the Phyllochron parameter in the input parameter value list, and so on.
                        int index_AreaSLL = 1;
                        int index_AreaPLL = 2;
                        int index_AreaSSL = 3;
                        int index_RatioFLPL = 4;
                        int index_AMNLFNO = 5;
                        int index_AMXLFNO = 6;
                        int index_EarGR = 7;
                        int index_LueDiffuse = 8;
                        int index_Kl = 9;
                        int index_NLL = 10;
                        int index_PhyllFLLAnth = 11;
                        int index_Phyllochron = 12;
                        int index_SLDL = 13;
                        int index_TTaegf = 14;
                        int index_TTsoem = 15;
                        int index_VAI = 16;
                        int index_VBEE = 17;
                        int index_AlphaKn = 18;
                        int index_AlphaNc = 19;
                        int index_AlphaNNI = 20;
                        int index_AlphaSSN = 21;
                        int index_BetaKn = 22;
                        int index_BetaNNI = 23;
                        int index_BetaRWU = 24;
                        int index_BetaSSN = 25;
                        int index_Co2FacRue = 26;
                        int index_DMmaxNuptake = 27;
                        int index_MaxDL = 28;
                        int index_FracBEAR = 29;
                        int index_FracLaminaeBGR = 30;
                        int index_FracSheathBGR = 31;
                        int index_FracStemWSC = 32;
                        int index_IntermTvern = 33;
                        int index_LowerFTSWbiomass = 34;
                        int index_LowerFTSWexpansion = 35;
                        int index_LowerFTSWsenescence = 36;
                        int index_LowerFTSWtranspiration = 37;
                        int index_LLOSS = 38;
                        int index_MaxAnForP = 39;
                        int index_MaxDSF = 40;
                        int index_MaxLeafSoil = 41;
                        int index_MaxLPhyll = 42;
                        int index_MaxLeafRRNU = 43;
                        int index_MaxRWU = 44;
                        int index_MaxStemRRNU = 45;
                        int index_MaxStemN = 46;
                        int index_MaxTvern = 47;
                        int index_MinLPhyll = 48;
                        int index_MinTvern = 49;
                        int index_PhyllDecr = 50;
                        int index_PhyllIncr = 51;
                        int index_PhyllGroLamina = 52;
                        int index_PhyllMBLL = 53;
                        int index_PhyllMSLL = 54;
                        int index_PhyllSBLL = 55;
                        int index_PhyllSSLL = 56;
                        int index_RGRStruc = 57;
                        int index_RVER = 58;
                        int index_SLNcri = 59;
                        int index_SLNmax0 = 60;
                        int index_SLNmin = 61;
                        int index_SLWp = 62;
                        int index_SlopeFR = 63;
                        int index_SSWp = 64;
                        int index_StrucLeafN = 65;
                        int index_StrucStemN = 66;
                        int index_TauSLN = 67;
                        int index_Topt = 68;
                        int index_Tmax = 69;
                        int index_TTcd = 70;
                        int index_TTer = 71;
                        int index_UpperFTSWbiomass = 72;
                        int index_UpperFTSWexpansion = 73;
                        int index_UpperFTSWsenescence = 74;
                        int index_UpperFTSWtranspiration = 75;
                        int index_MinDL = 76;
                        int index_tbase = 77;
                        #endregion

                        #region Set parameter values with 85 parameters.
                        //double aSheath_Value = inputParameterValues[index_aSheath];//Get the value of the input parameters, and so on.
                        //double AreaSLL_Value = inputParameterValues[index_AreaSLL];
                        //double AreaPLL_Value = inputParameterValues[index_AreaPLL];
                        //double AreaSSL_Value = inputParameterValues[index_AreaSSL];
                        //double RatioFLPL_Value = inputParameterValues[index_RatioFLPL];
                        //double AMNLFNO_Value = inputParameterValues[index_AMNLFNO];
                        //double AMXLFNO_Value = inputParameterValues[index_AMXLFNO];
                        //double EarGR_Value = inputParameterValues[index_EarGR];
                        //double LueDiffuse_Value = inputParameterValues[index_LueDiffuse];
                        //double Kl_Value = inputParameterValues[index_Kl];
                        //double L_IN1_Value = inputParameterValues[index_L_IN1];
                        //double L_EP_Value = inputParameterValues[index_L_EP];
                        //double NLL_Value = inputParameterValues[index_NLL];
                        //double PhyllFLLAnth_Value = inputParameterValues[index_PhyllFLLAnth];
                        //double Phyllochron_Value = inputParameterValues[index_Phyllochron];
                        //double SLDL_Value = inputParameterValues[index_SLDL];
                        //double TTaegf_Value = inputParameterValues[index_TTaegf];
                        //double TTegfm_Value = inputParameterValues[index_TTegfm];
                        //double TTsoem_Value = inputParameterValues[index_TTsoem];
                        //double VAI_Value = inputParameterValues[index_VAI];
                        //double VBEE_Value = inputParameterValues[index_VBEE];
                        //double AlphaAlbGlo_Value = inputParameterValues[index_AlphaAlbGlo];
                        //double AlphaGlu_Value = inputParameterValues[index_AlphaGlu];
                        //double AlphaKn_Value = inputParameterValues[index_AlphaKn];
                        //double AlphaNc_Value = inputParameterValues[index_AlphaNc];
                        //double AlphaNNI_Value = inputParameterValues[index_AlphaNNI];
                        //double AlphaSSN_Value = inputParameterValues[index_AlphaSSN];
                        //double BetaAlbGlo_Value = inputParameterValues[index_BetaAlbGlo];
                        //double BetaGlu_Value = inputParameterValues[index_BetaGlu];
                        //double BetaKn_Value = inputParameterValues[index_BetaKn];
                        //double BetaNNI_Value = inputParameterValues[index_BetaNNI];
                        //double BetaRWU_Value = inputParameterValues[index_BetaRWU];
                        //double BetaSSN_Value = inputParameterValues[index_BetaSSN];
                        //double Co2FacRue_Value = inputParameterValues[index_Co2FacRue];
                        //double DMmaxNuptake_Value = inputParameterValues[index_DMmaxNuptake];
                        //double FracAnth_Value = inputParameterValues[index_FracAnth];
                        //double FracBEAR_Value = inputParameterValues[index_FracBEAR];
                        //double FracLaminaeBGR_Value = inputParameterValues[index_FracLaminaeBGR];
                        //double FracSheathBGR_Value = inputParameterValues[index_FracSheathBGR];
                        //double FracStemWSC_Value = inputParameterValues[index_FracStemWSC];
                        //double IntermTvern_Value = inputParameterValues[index_IntermTvern];
                        //double LowerFTSWbiomass_Value = inputParameterValues[index_LowerFTSWbiomass];
                        //double LowerFTSWexpansion_Value = inputParameterValues[index_LowerFTSWexpansion];
                        //double LowerFTSWsenescence_Value = inputParameterValues[index_LowerFTSWsenescence];
                        //double LowerFTSWtranspiration_Value = inputParameterValues[index_LowerFTSWtranspiration];
                        //double LLOSS_Value = inputParameterValues[index_LLOSS];
                        //double MaxAnForP_Value = inputParameterValues[index_MaxAnForP];
                        //double MaxDSF_Value = inputParameterValues[index_MaxDSF];
                        //double MaxLeafSoil_Value = inputParameterValues[index_MaxLeafSoil];
                        //double MaxLPhyll_Value = inputParameterValues[index_MaxLPhyll];
                        //double MaxLeafRRNU_Value = inputParameterValues[index_MaxLeafRRNU];
                        //double MaxRWU_Value = inputParameterValues[index_MaxRWU];
                        //double MaxStemRRNU_Value = inputParameterValues[index_MaxStemRRNU];
                        //double MaxStemN_Value = inputParameterValues[index_MaxStemN];
                        //double MaxTvern_Value = inputParameterValues[index_MaxTvern];
                        //double MinLPhyll_Value = inputParameterValues[index_MinLPhyll];
                        //double MinTvern_Value = inputParameterValues[index_MinTvern];
                        //double PhyllDecr_Value = inputParameterValues[index_PhyllDecr];
                        //double PhyllIncr_Value = inputParameterValues[index_PhyllIncr];
                        //double PhyllGroLamina_Value = inputParameterValues[index_PhyllGroLamina];
                        //double PhyllGroInterNode_Value = inputParameterValues[index_PhyllGroInterNode];
                        //double PhyllMBLL_Value = inputParameterValues[index_PhyllMBLL];
                        //double PhyllMSLL_Value = inputParameterValues[index_PhyllMSLL];
                        //double PhyllSBLL_Value = inputParameterValues[index_PhyllSBLL];
                        //double PhyllSSLL_Value = inputParameterValues[index_PhyllSSLL];
                        //double RGRStruc_Value = inputParameterValues[index_RGRStruc];
                        //double RVER_Value = inputParameterValues[index_RVER];
                        //double SLNcri_Value = inputParameterValues[index_SLNcri];
                        //double SLNmax0_Value = inputParameterValues[index_SLNmax0];
                        //double SLNmin_Value = inputParameterValues[index_SLNmin];
                        //double SLWp_Value = inputParameterValues[index_SLWp];
                        //double SlopeFR_Value = inputParameterValues[index_SlopeFR];
                        //double SSWp_Value = inputParameterValues[index_SSWp];
                        //double StdCo2_Value = inputParameterValues[index_StdCo2];
                        //double StrucLeafN_Value = inputParameterValues[index_StrucLeafN];
                        //double StrucStemN_Value = inputParameterValues[index_StrucStemN];
                        //double TauSLN_Value = inputParameterValues[index_TauSLN];
                        //double Topt_Value = inputParameterValues[index_Topt];
                        //double Tmax_Value = inputParameterValues[index_Tmax];
                        //double TTcd_Value = inputParameterValues[index_TTcd];
                        //double TTer_Value = inputParameterValues[index_TTer];
                        //double UpperFTSWbiomass_Value = inputParameterValues[index_UpperFTSWbiomass];
                        //double UpperFTSWexpansion_Value = inputParameterValues[index_UpperFTSWexpansion];
                        //double UpperFTSWsenescence_Value = inputParameterValues[index_UpperFTSWsenescence];
                        //double UpperFTSWtranspiration_Value = inputParameterValues[index_UpperFTSWtranspiration];
                        #endregion

                        #region Set parameter values with 76 parameters.
                        double aSheath_Value = inputParameterValues[index_aSheath];//Get the value of the input parameters, and so on.
                        double AreaSLL_Value = inputParameterValues[index_AreaSLL];
                        double AreaPLL_Value = inputParameterValues[index_AreaPLL];
                        double AreaSSL_Value = inputParameterValues[index_AreaSSL];
                        double RatioFLPL_Value = inputParameterValues[index_RatioFLPL];
                        double AMNLFNO_Value = inputParameterValues[index_AMNLFNO];
                        double AMXLFNO_Value = inputParameterValues[index_AMXLFNO];
                        double EarGR_Value = inputParameterValues[index_EarGR];
                        double LueDiffuse_Value = inputParameterValues[index_LueDiffuse];
                        double Kl_Value = inputParameterValues[index_Kl];
                        double NLL_Value = inputParameterValues[index_NLL];
                        double PhyllFLLAnth_Value = inputParameterValues[index_PhyllFLLAnth];
                        double Phyllochron_Value = inputParameterValues[index_Phyllochron];
                        double SLDL_Value = inputParameterValues[index_SLDL];
                        double TTaegf_Value = inputParameterValues[index_TTaegf];
                        double TTsoem_Value = inputParameterValues[index_TTsoem];
                        double VAI_Value = inputParameterValues[index_VAI];
                        double VBEE_Value = inputParameterValues[index_VBEE];
                        double AlphaKn_Value = inputParameterValues[index_AlphaKn];
                        double AlphaNc_Value = inputParameterValues[index_AlphaNc];
                        double AlphaNNI_Value = inputParameterValues[index_AlphaNNI];
                        double AlphaSSN_Value = inputParameterValues[index_AlphaSSN];
                        double BetaKn_Value = inputParameterValues[index_BetaKn];
                        double BetaNNI_Value = inputParameterValues[index_BetaNNI];
                        double BetaRWU_Value = inputParameterValues[index_BetaRWU];
                        double BetaSSN_Value = inputParameterValues[index_BetaSSN];
                        double Co2FacRue_Value = inputParameterValues[index_Co2FacRue];
                        double DMmaxNuptake_Value = inputParameterValues[index_DMmaxNuptake];
                        double MaxDL_Value = inputParameterValues[index_MaxDL];
                        double MinDL_Value = inputParameterValues[index_MinDL];
                        double tbase_value = inputParameterValues[index_tbase];
                        double FracBEAR_Value = inputParameterValues[index_FracBEAR];
                        double FracLaminaeBGR_Value = inputParameterValues[index_FracLaminaeBGR];
                        double FracSheathBGR_Value = inputParameterValues[index_FracSheathBGR];
                        double FracStemWSC_Value = inputParameterValues[index_FracStemWSC];
                        double IntermTvern_Value = inputParameterValues[index_IntermTvern];
                        double LowerFTSWbiomass_Value = inputParameterValues[index_LowerFTSWbiomass];
                        double LowerFTSWexpansion_Value = inputParameterValues[index_LowerFTSWexpansion];
                        double LowerFTSWsenescence_Value = inputParameterValues[index_LowerFTSWsenescence];
                        double LowerFTSWtranspiration_Value = inputParameterValues[index_LowerFTSWtranspiration];
                        double LLOSS_Value = inputParameterValues[index_LLOSS];
                        double MaxAnForP_Value = inputParameterValues[index_MaxAnForP];
                        double MaxDSF_Value = inputParameterValues[index_MaxDSF];
                        double MaxLeafSoil_Value = inputParameterValues[index_MaxLeafSoil];
                        double MaxLPhyll_Value = inputParameterValues[index_MaxLPhyll];
                        double MaxLeafRRNU_Value = inputParameterValues[index_MaxLeafRRNU];
                        double MaxRWU_Value = inputParameterValues[index_MaxRWU];
                        double MaxStemRRNU_Value = inputParameterValues[index_MaxStemRRNU];
                        double MaxStemN_Value = inputParameterValues[index_MaxStemN];
                        double MaxTvern_Value = inputParameterValues[index_MaxTvern];
                        double MinLPhyll_Value = inputParameterValues[index_MinLPhyll];
                        double MinTvern_Value = inputParameterValues[index_MinTvern];
                        double PhyllDecr_Value = inputParameterValues[index_PhyllDecr];
                        double PhyllIncr_Value = inputParameterValues[index_PhyllIncr];
                        double PhyllGroLamina_Value = inputParameterValues[index_PhyllGroLamina];
                        double PhyllMBLL_Value = inputParameterValues[index_PhyllMBLL];
                        double PhyllMSLL_Value = inputParameterValues[index_PhyllMSLL];
                        double PhyllSBLL_Value = inputParameterValues[index_PhyllSBLL];
                        double PhyllSSLL_Value = inputParameterValues[index_PhyllSSLL];
                        double RGRStruc_Value = inputParameterValues[index_RGRStruc];
                        double RVER_Value = inputParameterValues[index_RVER];
                        double SLNcri_Value = inputParameterValues[index_SLNcri];
                        double SLNmax0_Value = inputParameterValues[index_SLNmax0];
                        double SLNmin_Value = inputParameterValues[index_SLNmin];
                        double SLWp_Value = inputParameterValues[index_SLWp];
                        double SlopeFR_Value = inputParameterValues[index_SlopeFR];
                        double SSWp_Value = inputParameterValues[index_SSWp];
                        double StrucLeafN_Value = inputParameterValues[index_StrucLeafN];
                        double StrucStemN_Value = inputParameterValues[index_StrucStemN];
                        double TauSLN_Value = inputParameterValues[index_TauSLN];
                        double Topt_Value = inputParameterValues[index_Topt];
                        double Tmax_Value = inputParameterValues[index_Tmax];
                        double TTcd_Value = inputParameterValues[index_TTcd];
                        double TTer_Value = inputParameterValues[index_TTer];
                        double UpperFTSWbiomass_Value = inputParameterValues[index_UpperFTSWbiomass];
                        double UpperFTSWexpansion_Value = inputParameterValues[index_UpperFTSWexpansion];
                        double UpperFTSWsenescence_Value = inputParameterValues[index_UpperFTSWsenescence];
                        double UpperFTSWtranspiration_Value = inputParameterValues[index_UpperFTSWtranspiration];
                        #endregion

                        #region Save initial parameter values.
                        var initial_aSheath = variety.aSheath;//Get the initial value of the input parameters, and so on.
                        var initial_AreaSLL = variety.AreaSLL;
                        var initial_AreaPLL = variety.AreaPLL;
                        var initial_AreaSSL = variety.AreaSSL;
                        var initial_RatioFLPL = variety.RatioFLPL;
                        var initial_EarGR = variety.EarGR;
                        var initial_LueDiffuse = variety.LueDiffuse;
                        var initial_Kl = variety.Kl;
                        var initial_L_IN1 = variety.L_IN1;
                        var initial_L_EP = variety.L_EP;
                        var initial_NLL = variety.NLL;
                        var initial_Phyllochron = variety.Phyllochron;
                        var initial_SLDL = variety.SLDL;
                        var initial_TTaegf = variety.TTaegf;
                        var initial_TTegfm = variety.TTegfm;
                        var initial_TTsoem = variety.TTsoem;
                        var initial_VAI = variety.VAI;
                        var initial_AlphaAlbGlo = parameter.AlphaAlbGlo;
                        var initial_AlphaGlu = parameter.AlphaGlu;
                        var initial_AlphaKn = parameter.AlphaKn;
                        var initial_AlphaNc = parameter.AlphaNc;
                        var initial_AlphaNNI = parameter.AlphaNNI;
                        var initial_AlphaSSN = parameter.AlphaSSN;
                        var initial_BetaAlbGlo = parameter.BetaAlbGlo;
                        var initial_BetaGlu = parameter.BetaGlu;
                        var initial_BetaKn = parameter.BetaKn;
                        var initial_BetaNNI = parameter.BetaNNI;
                        var initial_BetaRWU = parameter.BetaRWU;
                        var initial_BetaSSN = parameter.BetaSSN;
                        var initial_Co2FacRue = parameter.Co2FacRue;
                        var initial_DMmaxNuptake = parameter.DMmaxNuptake;
                        var initial_MaxDL = parameter.MaxDL;
                        var initial_MinDl = parameter.MinDL;
                        var initial_tbase = parameter.tbase;
                        var initial_FracBEAR = parameter.FracBEAR;
                        var initial_FracLaminaeBGR = parameter.FracLaminaeBGR;
                        var initial_FracSheathBGR = parameter.FracSheathBGR;
                        var initial_FracStemWSC = parameter.FracStemWSC;
                        var initial_IntermTvern = parameter.IntermTvern;
                        var initial_LowerFTSWbiomass = parameter.LowerFTSWbiomass;
                        var initial_LowerFTSWexpansion = parameter.LowerFTSWexpansion;
                        var initial_LowerFTSWsenescence = parameter.LowerFTSWsenescence;
                        var initial_LowerFTSWtranspiration = parameter.LowerFTSWtranspiration;
                        var initial_LLOSS = parameter.LLOSS;
                        var initial_MaxAnForP = parameter.MaxAnForP;
                        var initial_MaxDSF = parameter.MaxDSF;
                        var initial_MaxLeafSoil = parameter.MaxLeafSoil;
                        var initial_MaxLPhyll = parameter.MaxLPhyll;
                        var initial_MaxLeafRRNU = parameter.MaxLeafRRNU;
                        var initial_MaxRWU = parameter.MaxRWU;
                        var initial_MaxStemRRNU = parameter.MaxStemRRNU;
                        var initial_MaxStemN = parameter.MaxStemN;
                        var initial_MaxTvern = parameter.MaxTvern;
                        var initial_MinLPhyll = parameter.MinLPhyll;
                        var initial_MinTvern = parameter.MinTvern;
                        var initial_PhyllDecr = parameter.PhyllDecr;
                        var initial_PhyllIncr = parameter.PhyllIncr;
                        var initial_PhyllGroLamina = parameter.PhyllGroLamina;
                        var initial_PhyllGroInterNode = parameter.PhyllGroInterNode;
                        var initial_PhyllMBLL = parameter.PhyllMBLL;
                        var initial_PhyllMSLL = parameter.PhyllMSLL;
                        var initial_PhyllSBLL = parameter.PhyllSBLL;
                        var initial_PhyllSSLL = parameter.PhyllSSLL;
                        var initial_RGRStruc = parameter.RGRStruc;
                        var initial_RVER = parameter.RVER;
                        var initial_SLNcri = parameter.SLNcri;
                        var initial_SLNmax0 = parameter.SLNmax0;
                        var initial_SLNmin = parameter.SLNmin;
                        var initial_SLWp = parameter.SLWp;
                        var initial_SlopeFR = parameter.SlopeFR;
                        var initial_SSWp = parameter.SSWp;
                        var initial_StdCo2 = parameter.StdCo2;
                        var initial_StrucLeafN = parameter.StrucLeafN;
                        var initial_StrucStemN = parameter.StrucStemN;
                        var initial_TauSLN = parameter.TauSLN;
                        var initial_Topt = parameter.Topt;
                        var initial_Tmax = parameter.Tmax;
                        var initial_TTcd = parameter.TTcd;
                        var initial_TTer = parameter.TTer;
                        var initial_UpperFTSWbiomass = parameter.UpperFTSWbiomass;
                        var initial_UpperFTSWexpansion = parameter.UpperFTSWexpansion;
                        var initial_UpperFTSWsenescence = parameter.UpperFTSWsenescence;
                        var initial_UpperFTSWtranspiration = parameter.UpperFTSWtranspiration;
                        var initial_VBEE = parameter.VBEE;
                        var initial_PhyllFLLAnth = parameter.PhyllFLLAnth;
                        var initial_AMNLFNO = parameter.AMNLFNO;
                        var initial_AMXLFNO = parameter.AMXLFNO;
                        #endregion

                        try
                        {
                            #region Change the paramtere values with the random ones, where the space it +- 50% with 85 parameters.
                            //variety.aSheath = initial_aSheath * (0.5 + aSheath_Value);
                            //variety.AreaSLL = initial_AreaSLL * (0.5 + AreaSLL_Value);
                            //variety.AreaPLL = initial_AreaPLL * (0.5 + AreaPLL_Value);
                            //variety.AreaSSL = initial_AreaSSL * (0.5 + AreaSSL_Value);
                            //variety.RatioFLPL = initial_RatioFLPL * (0.5 + RatioFLPL_Value);
                            //variety.AMNLFNO = initial_AMNLFNO * (0.5 + AMNLFNO_Value);
                            //variety.AMXLFNO = initial_AMXLFNO * (0.5 + AMXLFNO_Value);
                            //variety.EarGR = initial_EarGR * (0.5 + EarGR_Value);
                            //variety.LueDiffuse = initial_LueDiffuse * (0.5 + LueDiffuse_Value);
                            //variety.Kl = initial_Kl * (0.5 + Kl_Value);
                            //variety.L_IN1 = initial_L_IN1 * (0.5 + L_IN1_Value);
                            //variety.L_EP = initial_L_EP * (0.5 + L_EP_Value);
                            //variety.NLL = initial_NLL * (0.5 + NLL_Value);
                            //variety.PhyllFLLAnth = initial_PhyllFLLAnth * (0.5 + PhyllFLLAnth_Value);
                            //variety.Phyllochron = initial_Phyllochron * (0.5 + Phyllochron_Value);
                            //variety.SLDL = initial_SLDL * (0.5 + SLDL_Value);
                            //variety.TTaegf = initial_TTaegf * (0.5 + TTaegf_Value);
                            //variety.TTegfm = initial_TTegfm * (0.5 + TTegfm_Value);
                            //variety.TTsoem = initial_TTsoem * (0.5 + TTsoem_Value);
                            //variety.VAI = initial_VAI * (0.5 + VAI_Value);
                            //variety.VBEE = initial_VBEE * (0.5 + VBEE_Value);
                            //parameter.AlphaAlbGlo = initial_AlphaAlbGlo * (0.5 + AlphaAlbGlo_Value);
                            //parameter.AlphaGlu = initial_AlphaGlu * (0.5 + AlphaGlu_Value);
                            //parameter.AlphaKn = initial_AlphaKn * (0.5 + AlphaKn_Value);
                            //parameter.AlphaNc = initial_AlphaNc * (0.5 + AlphaNc_Value);
                            //parameter.AlphaNNI = initial_AlphaNNI * (0.5 + AlphaNNI_Value);
                            //parameter.AlphaSSN = initial_AlphaSSN * (0.5 + AlphaSSN_Value);
                            //parameter.BetaAlbGlo = initial_BetaAlbGlo * (0.5 + BetaAlbGlo_Value);
                            //parameter.BetaGlu = initial_BetaGlu * (0.5 + BetaGlu_Value);
                            //parameter.BetaKn = initial_BetaKn * (0.5 + BetaKn_Value);
                            //parameter.BetaNNI = initial_BetaNNI * (0.5 + BetaNNI_Value);
                            //parameter.BetaRWU = initial_BetaRWU * (0.5 + BetaRWU_Value);
                            //parameter.BetaSSN = initial_BetaSSN * (0.5 + BetaSSN_Value);
                            //parameter.Co2FacRue = initial_Co2FacRue * (0.5 + Co2FacRue_Value);
                            //parameter.DMmaxNuptake = initial_DMmaxNuptake * (0.5 + DMmaxNuptake_Value);
                            //parameter.FracAnth = initial_FracAnth * (0.5 + FracAnth_Value);
                            //parameter.FracBEAR = initial_FracBEAR * (0.5 + FracBEAR_Value);
                            //parameter.FracLaminaeBGR = initial_FracLaminaeBGR * (0.5 + FracLaminaeBGR_Value);
                            //parameter.FracSheathBGR = initial_FracSheathBGR * (0.5 + FracSheathBGR_Value);
                            //parameter.FracStemWSC = initial_FracStemWSC * (0.5 + FracStemWSC_Value);
                            //parameter.IntermTvern = initial_IntermTvern * (0.5 + IntermTvern_Value);
                            //parameter.LowerFTSWbiomass = initial_LowerFTSWbiomass * (0.5 + LowerFTSWbiomass_Value);
                            //parameter.LowerFTSWexpansion = initial_LowerFTSWexpansion * (0.5 + LowerFTSWexpansion_Value);
                            //parameter.LowerFTSWsenescence = initial_LowerFTSWsenescence * (0.5 + LowerFTSWsenescence_Value);
                            //parameter.LowerFTSWtranspiration = initial_LowerFTSWtranspiration * (0.5 + LowerFTSWtranspiration_Value);
                            //parameter.LLOSS = initial_LLOSS * (0.5 + LLOSS_Value);
                            //parameter.MaxAnForP = initial_MaxAnForP * (0.5 + MaxAnForP_Value);
                            //parameter.MaxDSF = initial_MaxDSF * (0.5 + MaxDSF_Value);
                            //parameter.MaxLeafSoil = initial_MaxLeafSoil * (0.5 + MaxLeafSoil_Value);
                            //parameter.MaxLPhyll = initial_MaxLPhyll * (0.5 + MaxLPhyll_Value);
                            //parameter.MaxLeafRRNU = initial_MaxLeafRRNU * (0.5 + MaxLeafRRNU_Value);
                            //parameter.MaxRWU = initial_MaxRWU * (0.5 + MaxRWU_Value);
                            //parameter.MaxStemRRNU = initial_MaxStemRRNU * (0.5 + MaxStemRRNU_Value);
                            //parameter.MaxStemN = initial_MaxStemN * (0.5 + MaxStemN_Value);
                            //parameter.MaxTvern = initial_MaxTvern * (0.5 + MaxTvern_Value);
                            //parameter.MinLPhyll = initial_MinLPhyll * (0.5 + MinLPhyll_Value);
                            //parameter.MinTvern = initial_MinTvern * (0.5 + MinTvern_Value);
                            //parameter.PhyllDecr = initial_PhyllDecr * (0.5 + PhyllDecr_Value);
                            //parameter.PhyllIncr = initial_PhyllIncr * (0.5 + PhyllIncr_Value);
                            //parameter.PhyllGroLamina = initial_PhyllGroLamina * (0.5 + PhyllGroLamina_Value);
                            //parameter.PhyllGroInterNode = initial_PhyllGroInterNode * (0.5 + PhyllGroInterNode_Value);
                            //parameter.PhyllMBLL = initial_PhyllMBLL * (0.5 + PhyllMBLL_Value);
                            //parameter.PhyllMSLL = initial_PhyllMSLL * (0.5 + PhyllMSLL_Value);
                            //parameter.PhyllSBLL = initial_PhyllSBLL * (0.5 + PhyllSBLL_Value);
                            //parameter.PhyllSSLL = initial_PhyllSSLL * (0.5 + PhyllSSLL_Value);
                            //parameter.RGRStruc = initial_RGRStruc * (0.5 + RGRStruc_Value);
                            //parameter.RVER = initial_RVER * (0.5 + RVER_Value);
                            //parameter.SLNcri = initial_SLNcri * (0.5 + SLNcri_Value);
                            //parameter.SLNmax0 = initial_SLNmax0 * (0.5 + SLNmax0_Value);
                            //parameter.SLNmin = initial_SLNmin * (0.5 + SLNmin_Value);
                            //parameter.SLWp = initial_SLWp * (0.5 + SLWp_Value);
                            //parameter.SlopeFR = initial_SlopeFR * (0.5 + SlopeFR_Value);
                            //parameter.SSWp = initial_SSWp * (0.5 + SSWp_Value);
                            //parameter.StdCo2 = initial_StdCo2 * (0.5 + StdCo2_Value);
                            //parameter.StrucLeafN = initial_StrucLeafN * (0.5 + StrucLeafN_Value);
                            //parameter.StrucStemN = initial_StrucStemN * (0.5 + StrucStemN_Value);
                            //parameter.TauSLN = initial_TauSLN * (0.5 + TauSLN_Value);
                            //parameter.Topt = initial_Topt * (0.5 + Topt_Value);
                            //parameter.Tmax = initial_Tmax * (0.8 + 0.7 * Tmax_Value); //The minimum vlaue of Tmax is 0.8*Tmax, while the maximum value is 1.5*Tmax.
                            //parameter.TTcd = initial_TTcd * (0.5 + TTcd_Value);
                            //parameter.TTer = initial_TTer * (0.5 + TTer_Value);
                            //parameter.UpperFTSWbiomass = initial_UpperFTSWbiomass * (0.5 + UpperFTSWbiomass_Value);
                            //parameter.UpperFTSWexpansion = initial_UpperFTSWexpansion * (0.5 + UpperFTSWexpansion_Value);
                            //parameter.UpperFTSWsenescence = initial_UpperFTSWsenescence * (0.5 + UpperFTSWsenescence_Value);
                            //parameter.UpperFTSWtranspiration = initial_UpperFTSWtranspiration * (0.5 + UpperFTSWtranspiration_Value);
                            #endregion

                            #region Change the paramtere values with the random ones, where the space it +- 20% with 85 parameters.
                            //variety.aSheath = initial_aSheath * (0.8 + 0.4 * aSheath_Value);
                            //variety.AreaSLL = initial_AreaSLL * (0.8 + 0.4 * AreaSLL_Value);
                            //variety.AreaPLL = initial_AreaPLL * (0.8 + 0.4 * AreaPLL_Value);
                            //variety.AreaSSL = initial_AreaSSL * (0.8 + 0.4 * AreaSSL_Value);
                            //variety.RatioFLPL = initial_RatioFLPL * (0.8 + 0.4 * RatioFLPL_Value);
                            //variety.AMNLFNO = initial_AMNLFNO * (0.8 + 0.4 * AMNLFNO_Value);
                            //variety.AMXLFNO = initial_AMXLFNO * (0.8 + 0.4 * AMXLFNO_Value);
                            //variety.EarGR = initial_EarGR * (0.8 + 0.4 * EarGR_Value);
                            //variety.LueDiffuse = initial_LueDiffuse * (0.8 + 0.4 * LueDiffuse_Value);
                            //variety.Kl = initial_Kl * (0.8 + 0.4 * Kl_Value);
                            //variety.L_IN1 = initial_L_IN1 * (0.8 + 0.4 * L_IN1_Value);
                            //variety.L_EP = initial_L_EP * (0.8 + 0.4 * L_EP_Value);
                            //variety.NLL = initial_NLL * (0.8 + 0.4 * NLL_Value);
                            //variety.PhyllFLLAnth = initial_PhyllFLLAnth * (0.8 + 0.4 * PhyllFLLAnth_Value);
                            //variety.Phyllochron = initial_Phyllochron * (0.8 + 0.4 * Phyllochron_Value);
                            //variety.SLDL = initial_SLDL * (0.8 + 0.4 * SLDL_Value);
                            //variety.TTaegf = initial_TTaegf * (0.8 + 0.4 * TTaegf_Value);
                            //variety.TTegfm = initial_TTegfm * (0.8 + 0.4 * TTegfm_Value);
                            //variety.TTsoem = initial_TTsoem * (0.8 + 0.4 * TTsoem_Value);
                            //variety.VAI = initial_VAI * (0.8 + 0.4 * VAI_Value);
                            //variety.VBEE = initial_VBEE * (0.8 + 0.4 * VBEE_Value);
                            //parameter.AlphaAlbGlo = initial_AlphaAlbGlo * (0.8 + 0.4 * AlphaAlbGlo_Value);
                            //parameter.AlphaGlu = initial_AlphaGlu * (0.8 + 0.4 * AlphaGlu_Value);
                            //parameter.AlphaKn = initial_AlphaKn * (0.8 + 0.4 * AlphaKn_Value);
                            //parameter.AlphaNc = initial_AlphaNc * (0.8 + 0.4 * AlphaNc_Value);
                            //parameter.AlphaNNI = initial_AlphaNNI * (0.8 + 0.4 * AlphaNNI_Value);
                            //parameter.AlphaSSN = initial_AlphaSSN * (0.8 + 0.4 * AlphaSSN_Value);
                            //parameter.BetaAlbGlo = initial_BetaAlbGlo * (0.8 + 0.4 * BetaAlbGlo_Value);
                            //parameter.BetaGlu = initial_BetaGlu * (0.8 + 0.4 * BetaGlu_Value);
                            //parameter.BetaKn = initial_BetaKn * (0.8 + 0.4 * BetaKn_Value);
                            //parameter.BetaNNI = initial_BetaNNI * (0.8 + 0.4 * BetaNNI_Value);
                            //parameter.BetaRWU = initial_BetaRWU * (0.8 + 0.4 * BetaRWU_Value);
                            //parameter.BetaSSN = initial_BetaSSN * (0.8 + 0.4 * BetaSSN_Value);
                            //parameter.Co2FacRue = initial_Co2FacRue * (0.8 + 0.4 * Co2FacRue_Value);
                            //parameter.DMmaxNuptake = initial_DMmaxNuptake * (0.8 + 0.4 * DMmaxNuptake_Value);
                            //parameter.FracAnth = initial_FracAnth * (0.8 + 0.4 * FracAnth_Value);
                            //parameter.FracBEAR = initial_FracBEAR * (0.8 + 0.4 * FracBEAR_Value);
                            //parameter.FracLaminaeBGR = initial_FracLaminaeBGR * (0.8 + 0.4 * FracLaminaeBGR_Value);
                            //parameter.FracSheathBGR = initial_FracSheathBGR * (0.8 + 0.4 * FracSheathBGR_Value);
                            //parameter.FracStemWSC = initial_FracStemWSC * (0.8 + 0.4 * FracStemWSC_Value);
                            //parameter.IntermTvern = initial_IntermTvern * (0.8 + 0.4 * IntermTvern_Value);
                            //parameter.LowerFTSWbiomass = initial_LowerFTSWbiomass * (0.8 + 0.4 * LowerFTSWbiomass_Value);
                            //parameter.LowerFTSWexpansion = initial_LowerFTSWexpansion * (0.8 + 0.4 * LowerFTSWexpansion_Value);
                            //parameter.LowerFTSWsenescence = initial_LowerFTSWsenescence * (0.8 + 0.4 * LowerFTSWsenescence_Value);
                            //parameter.LowerFTSWtranspiration = initial_LowerFTSWtranspiration * (0.8 + 0.4 * LowerFTSWtranspiration_Value);
                            //parameter.LLOSS = initial_LLOSS * (0.8 + 0.4 * LLOSS_Value);
                            //parameter.MaxAnForP = initial_MaxAnForP * (0.8 + 0.4 * MaxAnForP_Value);
                            //parameter.MaxDSF = initial_MaxDSF * (0.8 + 0.4 * MaxDSF_Value);
                            //parameter.MaxLeafSoil = initial_MaxLeafSoil * (0.8 + 0.4 * MaxLeafSoil_Value);
                            //parameter.MaxLPhyll = initial_MaxLPhyll * (0.8 + 0.4 * MaxLPhyll_Value);
                            //parameter.MaxLeafRRNU = initial_MaxLeafRRNU * (0.8 + 0.4 * MaxLeafRRNU_Value);
                            //parameter.MaxRWU = initial_MaxRWU * (0.8 + 0.4 * MaxRWU_Value);
                            //parameter.MaxStemRRNU = initial_MaxStemRRNU * (0.8 + 0.4 * MaxStemRRNU_Value);
                            //parameter.MaxStemN = initial_MaxStemN * (0.8 + 0.4 * MaxStemN_Value);
                            //parameter.MaxTvern = initial_MaxTvern * (0.8 + 0.4 * MaxTvern_Value);
                            //parameter.MinLPhyll = initial_MinLPhyll * (0.8 + 0.4 * MinLPhyll_Value);
                            //parameter.MinTvern = initial_MinTvern * (0.8 + 0.4 * MinTvern_Value);
                            //parameter.PhyllDecr = initial_PhyllDecr * (0.8 + 0.4 * PhyllDecr_Value);
                            //parameter.PhyllIncr = initial_PhyllIncr * (0.8 + 0.4 * PhyllIncr_Value);
                            //parameter.PhyllGroLamina = initial_PhyllGroLamina * (0.8 + 0.4 * PhyllGroLamina_Value);
                            //parameter.PhyllGroInterNode = initial_PhyllGroInterNode * (0.8 + 0.4 * PhyllGroInterNode_Value);
                            //parameter.PhyllMBLL = initial_PhyllMBLL * (0.8 + 0.4 * PhyllMBLL_Value);
                            //parameter.PhyllMSLL = initial_PhyllMSLL * (0.8 + 0.4 * PhyllMSLL_Value);
                            //parameter.PhyllSBLL = initial_PhyllSBLL * (0.8 + 0.4 * PhyllSBLL_Value);
                            //parameter.PhyllSSLL = initial_PhyllSSLL * (0.8 + 0.4 * PhyllSSLL_Value);
                            //parameter.RGRStruc = initial_RGRStruc * (0.8 + 0.4 * RGRStruc_Value);
                            //parameter.RVER = initial_RVER * (0.8 + 0.4 * RVER_Value);
                            //parameter.SLNcri = initial_SLNcri * (0.8 + 0.4 * SLNcri_Value);
                            //parameter.SLNmax0 = initial_SLNmax0 * (0.8 + 0.4 * SLNmax0_Value);
                            //parameter.SLNmin = initial_SLNmin * (0.8 + 0.4 * SLNmin_Value);
                            //parameter.SLWp = initial_SLWp * (0.8 + 0.4 * SLWp_Value);
                            //parameter.SlopeFR = initial_SlopeFR * (0.8 + 0.4 * SlopeFR_Value);
                            //parameter.SSWp = initial_SSWp * (0.8 + 0.4 * SSWp_Value);
                            //parameter.StdCo2 = initial_StdCo2 * (0.8 + 0.4 * StdCo2_Value);
                            //parameter.StrucLeafN = initial_StrucLeafN * (0.8 + 0.4 * StrucLeafN_Value);
                            //parameter.StrucStemN = initial_StrucStemN * (0.8 + 0.4 * StrucStemN_Value);
                            //parameter.TauSLN = initial_TauSLN * (0.8 + 0.4 * TauSLN_Value);
                            //parameter.Topt = initial_Topt * (0.8 + 0.4 * Topt_Value);
                            //parameter.Tmax = initial_Tmax * (0.8 + 0.4 * Tmax_Value);
                            //parameter.TTcd = initial_TTcd * (0.8 + 0.4 * TTcd_Value);
                            //parameter.TTer = initial_TTer * (0.8 + 0.4 * TTer_Value);
                            //parameter.UpperFTSWbiomass = initial_UpperFTSWbiomass * (0.8 + 0.4 * UpperFTSWbiomass_Value);
                            //parameter.UpperFTSWexpansion = initial_UpperFTSWexpansion * (0.8 + 0.4 * UpperFTSWexpansion_Value);
                            //parameter.UpperFTSWsenescence = initial_UpperFTSWsenescence * (0.8 + 0.4 * UpperFTSWsenescence_Value);
                            //parameter.UpperFTSWtranspiration = initial_UpperFTSWtranspiration * (0.8 + 0.4 * UpperFTSWtranspiration_Value);
                            #endregion

                            #region Change the paramtere values with the random ones, where the space it +- 20% with 76 parameters.
                            variety.aSheath = initial_aSheath * (0.8 + 0.4 * aSheath_Value);
                            variety.AreaSLL = initial_AreaSLL * (0.8 + 0.4 * AreaSLL_Value);
                            variety.AreaPLL = initial_AreaPLL * (0.8 + 0.4 * AreaPLL_Value);
                            variety.AreaSSL = initial_AreaSSL * (0.8 + 0.4 * AreaSSL_Value);
                            variety.RatioFLPL = initial_RatioFLPL * (0.8 + 0.4 * RatioFLPL_Value);
                            variety.EarGR = initial_EarGR * (0.8 + 0.4 * EarGR_Value);
                            variety.LueDiffuse = initial_LueDiffuse * (0.8 + 0.4 * LueDiffuse_Value);
                            variety.Kl = initial_Kl * (0.8 + 0.4 * Kl_Value);
                            variety.NLL = initial_NLL * (0.8 + 0.4 * NLL_Value);
                            variety.Phyllochron = initial_Phyllochron * (0.8 + 0.4 * Phyllochron_Value);
                            variety.SLDL = initial_SLDL * (0.8 + 0.4 * SLDL_Value);
                            variety.TTaegf = initial_TTaegf * (0.8 + 0.4 * TTaegf_Value);
                            variety.TTsoem = initial_TTsoem * (0.8 + 0.4 * TTsoem_Value);
                            variety.VAI = initial_VAI * (0.8 + 0.4 * VAI_Value);
                            parameter.AlphaKn = initial_AlphaKn * (0.8 +
                                0.4 * AlphaKn_Value);
                            parameter.AlphaNc = initial_AlphaNc * (0.8 + 0.4 * AlphaNc_Value);
                            parameter.AlphaNNI = initial_AlphaNNI * (0.8 + 0.4 * AlphaNNI_Value);
                            parameter.AlphaSSN = initial_AlphaSSN * (0.8 + 0.4 * AlphaSSN_Value);
                            parameter.BetaKn = initial_BetaKn * (0.8 + 0.4 * BetaKn_Value);
                            parameter.BetaNNI = initial_BetaNNI * (0.8 + 0.4 * BetaNNI_Value);
                            parameter.BetaRWU = initial_BetaRWU * (0.8 + 0.4 * BetaRWU_Value);
                            parameter.BetaSSN = initial_BetaSSN * (0.8 + 0.4 * BetaSSN_Value);
                            parameter.Co2FacRue = initial_Co2FacRue * (0.8 + 0.4 * Co2FacRue_Value);
                            parameter.DMmaxNuptake = initial_DMmaxNuptake * (0.8 + 0.4 * DMmaxNuptake_Value);
                            parameter.MaxDL = initial_MaxDL * (0.8 + 0.4 * MaxDL_Value);
                            parameter.FracBEAR = initial_FracBEAR * (0.8 + 0.4 * FracBEAR_Value);
                            parameter.FracLaminaeBGR = initial_FracLaminaeBGR * (0.8 + 0.4 * FracLaminaeBGR_Value);
                            parameter.FracSheathBGR = initial_FracSheathBGR * (0.8 + 0.4 * FracSheathBGR_Value);
                            parameter.FracStemWSC = initial_FracStemWSC * (0.8 + 0.4 * FracStemWSC_Value);
                            parameter.IntermTvern = initial_IntermTvern * (0.8 + 0.4 * IntermTvern_Value);
                            parameter.LowerFTSWbiomass = initial_LowerFTSWbiomass * (0.8 + 0.4 * LowerFTSWbiomass_Value);
                            parameter.LowerFTSWexpansion = initial_LowerFTSWexpansion * (0.8 + 0.4 * LowerFTSWexpansion_Value);
                            parameter.LowerFTSWsenescence = initial_LowerFTSWsenescence * (0.8 + 0.4 * LowerFTSWsenescence_Value);
                            parameter.LowerFTSWtranspiration = initial_LowerFTSWtranspiration * (0.8 + 0.4 * LowerFTSWtranspiration_Value);
                            parameter.LLOSS = initial_LLOSS * (0.8 + 0.4 * LLOSS_Value);
                            parameter.MaxAnForP = initial_MaxAnForP * (0.8 + 0.4 * MaxAnForP_Value);
                            parameter.MaxDSF = initial_MaxDSF * (0.8 + 0.4 * MaxDSF_Value);
                            parameter.MaxLeafSoil = initial_MaxLeafSoil * (0.8 + 0.4 * MaxLeafSoil_Value);
                            parameter.MaxLPhyll = initial_MaxLPhyll * (0.8 + 0.4 * MaxLPhyll_Value);
                            parameter.MaxLeafRRNU = initial_MaxLeafRRNU * (0.8 + 0.4 * MaxLeafRRNU_Value);
                            parameter.MaxRWU = initial_MaxRWU * (0.8 + 0.4 * MaxRWU_Value);
                            parameter.MaxStemRRNU = initial_MaxStemRRNU * (0.8 + 0.4 * MaxStemRRNU_Value);
                            parameter.MaxStemN = initial_MaxStemN * (0.8 + 0.4 * MaxStemN_Value);
                            parameter.MaxTvern = initial_MaxTvern * (0.8 + 0.4 * MaxTvern_Value);
                            parameter.MinLPhyll = initial_MinLPhyll * (0.8 + 0.4 * MinLPhyll_Value);
                            parameter.MinTvern = initial_MinTvern * (0.8 + 0.4 * MinTvern_Value);
                            parameter.PhyllDecr = initial_PhyllDecr * (0.8 + 0.4 * PhyllDecr_Value);
                            parameter.PhyllIncr = initial_PhyllIncr * (0.8 + 0.4 * PhyllIncr_Value);
                            parameter.PhyllGroLamina = initial_PhyllGroLamina * (0.8 + 0.4 * PhyllGroLamina_Value);
                            parameter.PhyllMBLL = initial_PhyllMBLL * (0.8 + 0.4 * PhyllMBLL_Value);
                            parameter.PhyllMSLL = initial_PhyllMSLL * (0.8 + 0.4 * PhyllMSLL_Value);
                            parameter.PhyllSBLL = initial_PhyllSBLL * (0.8 + 0.4 * PhyllSBLL_Value);
                            parameter.PhyllSSLL = initial_PhyllSSLL * (0.8 + 0.4 * PhyllSSLL_Value);
                            parameter.RGRStruc = initial_RGRStruc * (0.8 + 0.4 * RGRStruc_Value);
                            parameter.RVER = initial_RVER * (0.8 + 0.4 * RVER_Value);
                            parameter.SLNcri = initial_SLNcri * (0.8 + 0.4 * SLNcri_Value);
                            parameter.SLNmax0 = initial_SLNmax0 * (0.8 + 0.4 * SLNmax0_Value);
                            parameter.SLNmin = initial_SLNmin * (0.8 + 0.4 * SLNmin_Value);
                            parameter.SLWp = initial_SLWp * (0.8 + 0.4 * SLWp_Value);
                            parameter.SlopeFR = initial_SlopeFR * (0.8 + 0.4 * SlopeFR_Value);
                            parameter.SSWp = initial_SSWp * (0.8 + 0.4 * SSWp_Value);
                            parameter.StrucLeafN = initial_StrucLeafN * (0.8 + 0.4 * StrucLeafN_Value);
                            parameter.StrucStemN = initial_StrucStemN * (0.8 + 0.4 * StrucStemN_Value);
                            parameter.TauSLN = initial_TauSLN * (0.8 + 0.4 * TauSLN_Value);
                            parameter.Topt = initial_Topt * (0.8 + 0.4 * Topt_Value);
                            parameter.Tmax = initial_Tmax * (0.8 + 0.4 * Tmax_Value);
                            parameter.TTcd = initial_TTcd * (0.8 + 0.4 * TTcd_Value);
                            parameter.TTer = initial_TTer * (0.8 + 0.4 * TTer_Value);
                            parameter.UpperFTSWbiomass = initial_UpperFTSWbiomass * (0.8 + 0.4 * UpperFTSWbiomass_Value);
                            parameter.UpperFTSWexpansion = initial_UpperFTSWexpansion * (0.8 + 0.4 * UpperFTSWexpansion_Value);
                            parameter.UpperFTSWsenescence = initial_UpperFTSWsenescence * (0.8 + 0.4 * UpperFTSWsenescence_Value);
                            parameter.UpperFTSWtranspiration = initial_UpperFTSWtranspiration * (0.8 + 0.4 * UpperFTSWtranspiration_Value);
                            parameter.PhyllFLLAnth = initial_PhyllFLLAnth * (0.8 + 0.4 * PhyllFLLAnth_Value);
                            parameter.AMNLFNO = initial_AMNLFNO * (0.8 + 0.4 * AMNLFNO_Value);
                            parameter.AMXLFNO = initial_AMXLFNO * (0.8 + 0.4 * AMXLFNO_Value);
                            parameter.VBEE = initial_VBEE * (0.8 + 0.4 * VBEE_Value);
                            #endregion

                            #region multiple year model runs

                            for (var j = firstYear; j <= lastYear; ++j)
                            {

                                mi.SowingDate = new DateTime(j, mi.SowingDate.Month, mi.SowingDate.Day);//Change the sowing date for each year.

                                #region Run the model for each trreatment and each year.
                                rimm.StepRun(s, i);

                                var runObject = RunCore.RunInstance;
                                var universes = runObject.SavedUniverses;
                                var lastUniverse = universes[universes.Count - 1];

                                var anthesis_Date = lastUniverse.Calendar_[GrowthStage.ZC_65_Anthesis];
                                var anthesisUniverse = runObject.GetUniverse(anthesis_Date.Value); //Get the universe of maturity date.

                                var maturity_Date = lastUniverse.Calendar_[GrowthStage.ZC_92_Maturity];
                                var maturityUniverse = runObject.GetUniverse(maturity_Date.Value); //Get the universe of maturity date.


                                #region Get output values.
                                var anthesisDay = anthesis_Date.Value.DayOfYear;//1.Anthesis day after sowing.
                                var maturityDay = maturity_Date.Value.DayOfYear;//2.Maturity day after sowing.
                                var GAIAtAnthesis = Math.Round((anthesisUniverse.Crop_.Phytomers_.GAI), 2);//3.GAI at anthesis.
                                var cropDryMatterAtAnthesis = Math.Round(anthesisUniverse.Crop_.OutputTotalDM * 10, 0);//4.Crop dry matter at anthesis.
                                var cropDryMatterAtMaturity = Math.Round(maturityUniverse.Crop_.OutputTotalDM * 10, 0);//5.Crop dry matter at maturity.
                                var cropNAtAnthesis = Math.Round(anthesisUniverse.Crop_.OutputTotalN * 10, 0);//6.Crop N at anthesis.
                                var cropNAtMaturity = Math.Round(maturityUniverse.Crop_.OutputTotalN * 10, 0);//7.Crop N at maturity.
                                var grainDMAtMaturity = Math.Round((maturityUniverse.Crop_.Grain_.TotalDM * 10), 0); //8.Grain dry matter at maturity.                               
                                var grainNAtMaturity = Math.Round((maturityUniverse.Crop_.Grain_.TotalN * 10), 0); //9.Grain N at maturity..
                                var postAnthesisCropNUptake = Math.Round((maturityUniverse.Crop_.PostAnthesisNUptake * 10), 2); //10.Post-anthesis crop n uptake.

                                var singleGrainDMAtMaturity = Math.Round((maturityUniverse.Crop_.Grain_.TotalDMperGrain), 2); //11.Single grain DM at maturity.
                                var singleGrainNAtMaturity = Math.Round((maturityUniverse.Crop_.Grain_.TotalNperGrain), 4); //12.Single grain N at maturity. 
                                var grainProteinConcentrationAtMaturity = Math.Round((maturityUniverse.Crop_.Grain_.ProteinConcentration), 2); //13.Grain protein concentration at maturity. 
                                var grainNumberAtMaturity = Math.Round((maturityUniverse.Crop_.Grain_.Number), 0); //14.Grain dry matter.
                                var DMHarvestIndex = Math.Round((maturityUniverse.Crop_.Grain_.HarvestIndexDM), 1); //15.DM harvest index.
                                var NHarvestIndex = Math.Round((maturityUniverse.Crop_.Grain_.HarvestIndexN), 1); //16.N harvest index.
                                var NUseEfficiency = Math.Round((maturityUniverse.Crop_.Grain_.NuseEfficiency), 2); //17.N use efficiency.
                                var NUtilisationEfficiency = Math.Round((maturityUniverse.Crop_.Grain_.NutilisationEfficiency),2); //18.N utilisation efficiency.
                                var NUptakeEfficiency = Math.Round((maturityUniverse.Crop_.Grain_.NuptakeEfficiency), 4); //19.N upake efficiency.
                                var WaterUseEfficiency = Math.Round((maturityUniverse.Crop_.Grain_.WaterUseEfficiency), 4); //20.Water use efficiency.
                                
                                var FLNAtMaturity = Math.Round((maturityUniverse.Crop_.Shoot_.Phytomers_.Number), 2);//21.Final leaf number (FLN).
                                #endregion


                                string wholeOutputFileName = null;

                                switch (miName)
                                {
                                    case "AV-HighN":
                                        wholeOutputFileName = wholeOutputFileNames[0];
                                        break;

                                    case "AV-LowN":
                                        wholeOutputFileName = wholeOutputFileNames[1];
                                        break;

                                    case "CF-HighN":
                                        wholeOutputFileName = wholeOutputFileNames[2];
                                        break;

                                    case "CF-LowN":
                                        wholeOutputFileName = wholeOutputFileNames[3];
                                        break;

                                    case "RR-HighN":
                                        wholeOutputFileName = wholeOutputFileNames[4];
                                        break;

                                    case "RR-LowN":
                                        wholeOutputFileName = wholeOutputFileNames[5];
                                        break;
                                }

                                #region Save the outputs in text file.
                                StreamWriter SW = new StreamWriter(wholeOutputFileName, true);

                                SW.Write(anthesisDay);
                                SW.Write(" ");
                                SW.Write(maturityDay);
                                SW.Write(" ");
                                SW.Write(GAIAtAnthesis);
                                SW.Write(" ");
                                SW.Write(cropDryMatterAtAnthesis);
                                SW.Write(" ");
                                SW.Write(cropDryMatterAtMaturity);
                                SW.Write(" ");
                                SW.Write(cropNAtAnthesis);
                                SW.Write(" ");
                                SW.Write(cropNAtMaturity);
                                SW.Write(" ");
                                SW.Write(grainDMAtMaturity);
                                SW.Write(" ");
                                SW.Write(grainNAtMaturity);
                                SW.Write(" ");
                                SW.Write(postAnthesisCropNUptake);
                                SW.Write(" ");
                                SW.Write(singleGrainDMAtMaturity);
                                SW.Write(" ");
                                SW.Write(singleGrainNAtMaturity);
                                SW.Write(" ");
                                SW.Write(grainProteinConcentrationAtMaturity);
                                SW.Write(" ");
                                SW.Write(grainNumberAtMaturity);
                                SW.Write(" ");
                                SW.Write(DMHarvestIndex);
                                SW.Write(" ");
                                SW.Write(NHarvestIndex);
                                SW.Write(" ");
                                SW.Write(NUseEfficiency);
                                SW.Write(" ");
                                SW.Write(NUtilisationEfficiency);
                                SW.Write(" ");
                                SW.Write(NUptakeEfficiency);
                                SW.Write(" ");
                                SW.Write(WaterUseEfficiency);
                                SW.Write(" ");
                                SW.Write(FLNAtMaturity + "\r\n");

                                SW.Close();
                                #endregion

                                #endregion
                            }

                            #endregion
                        }
                        finally
                        {
                            #region Restore initial values.
                            variety.aSheath = initial_aSheath;//Get the initial value of the input parameters, and so on.
                            variety.AreaSLL = initial_AreaSLL;
                            variety.AreaPLL = initial_AreaPLL;
                            variety.AreaSSL = initial_AreaSSL;
                            variety.RatioFLPL = initial_RatioFLPL;
                            variety.EarGR = initial_EarGR;
                            variety.LueDiffuse = initial_LueDiffuse;
                            variety.Kl = initial_Kl;
                            variety.L_IN1 = initial_L_IN1;
                            variety.L_EP = initial_L_EP;
                            variety.NLL = initial_NLL;
                            variety.Phyllochron = initial_Phyllochron;
                            variety.SLDL = initial_SLDL;
                            variety.TTaegf = initial_TTaegf;
                            variety.TTegfm = initial_TTegfm;
                            variety.TTsoem = initial_TTsoem;
                            variety.VAI = initial_VAI;
                            parameter.AlphaAlbGlo = initial_AlphaAlbGlo;
                            parameter.AlphaGlu = initial_AlphaGlu;
                            parameter.AlphaKn = initial_AlphaKn;
                            parameter.AlphaNc = initial_AlphaNc;
                            parameter.AlphaNNI = initial_AlphaNNI;
                            parameter.AlphaSSN = initial_AlphaSSN;
                            parameter.BetaAlbGlo = initial_BetaAlbGlo;
                            parameter.BetaGlu = initial_BetaGlu;
                            parameter.BetaKn = initial_BetaKn;
                            parameter.BetaNNI = initial_BetaNNI;
                            parameter.BetaRWU = initial_BetaRWU;
                            parameter.BetaSSN = initial_BetaSSN;
                            parameter.Co2FacRue = initial_Co2FacRue;
                            parameter.DMmaxNuptake = initial_DMmaxNuptake;
                            parameter.MaxDL = initial_MaxDL;
                            parameter.FracBEAR = initial_FracBEAR;
                            parameter.FracLaminaeBGR = initial_FracLaminaeBGR;
                            parameter.FracSheathBGR = initial_FracSheathBGR;
                            parameter.FracStemWSC = initial_FracStemWSC;
                            parameter.IntermTvern = initial_IntermTvern;
                            parameter.LowerFTSWbiomass = initial_LowerFTSWbiomass;
                            parameter.LowerFTSWexpansion = initial_LowerFTSWexpansion;
                            parameter.LowerFTSWsenescence = initial_LowerFTSWsenescence;
                            parameter.LowerFTSWtranspiration = initial_LowerFTSWtranspiration;
                            parameter.LLOSS = initial_LLOSS;
                            parameter.MaxAnForP = initial_MaxAnForP;
                            parameter.MaxDSF = initial_MaxDSF;
                            parameter.MaxLeafSoil = initial_MaxLeafSoil;
                            parameter.MaxLPhyll = initial_MaxLPhyll;
                            parameter.MaxLeafRRNU = initial_MaxLeafRRNU;
                            parameter.MaxRWU = initial_MaxRWU;
                            parameter.MaxStemRRNU = initial_MaxStemRRNU;
                            parameter.MaxStemN = initial_MaxStemN;
                            parameter.MaxTvern = initial_MaxTvern;
                            parameter.MinLPhyll = initial_MinLPhyll;
                            parameter.MinTvern = initial_MinTvern;
                            parameter.PhyllDecr = initial_PhyllDecr;
                            parameter.PhyllIncr = initial_PhyllIncr;
                            parameter.PhyllGroLamina = initial_PhyllGroLamina;
                            parameter.PhyllGroInterNode = initial_PhyllGroInterNode;
                            parameter.PhyllMBLL = initial_PhyllMBLL;
                            parameter.PhyllMSLL = initial_PhyllMSLL;
                            parameter.PhyllSBLL = initial_PhyllSBLL;
                            parameter.PhyllSSLL = initial_PhyllSSLL;
                            parameter.RGRStruc = initial_RGRStruc;
                            parameter.RVER = initial_RVER;
                            parameter.SLNcri = initial_SLNcri;
                            parameter.SLNmax0 = initial_SLNmax0;
                            parameter.SLNmin = initial_SLNmin;
                            parameter.SLWp = initial_SLWp;
                            parameter.SlopeFR = initial_SlopeFR;
                            parameter.SSWp = initial_SSWp;
                            parameter.StdCo2 = initial_StdCo2;
                            parameter.StrucLeafN = initial_StrucLeafN;
                            parameter.StrucStemN = initial_StrucStemN;
                            parameter.TauSLN = initial_TauSLN;
                            parameter.Topt = initial_Topt;
                            parameter.Tmax = initial_Tmax;
                            parameter.TTcd = initial_TTcd;
                            parameter.TTer = initial_TTer;
                            parameter.UpperFTSWbiomass = initial_UpperFTSWbiomass;
                            parameter.UpperFTSWexpansion = initial_UpperFTSWexpansion;
                            parameter.UpperFTSWsenescence = initial_UpperFTSWsenescence;
                            parameter.UpperFTSWtranspiration = initial_UpperFTSWtranspiration;
                            parameter.VBEE = initial_VBEE;
                            parameter.PhyllFLLAnth = initial_PhyllFLLAnth;
                            parameter.AMNLFNO = initial_AMNLFNO;
                            parameter.AMXLFNO = initial_AMXLFNO;
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
        public static void ForeEachMultiRun(string title, bool serialize, bool createDailyOutput, string dailyOutputSuffix, string outputDirectory)
        {
            ForeEachMultiRun(title, serialize, createDailyOutput, dailyOutputSuffix, outputDirectory, (rimm, i, s) =>
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
        public static void ForeEachMultiRun(string title, bool serialize, bool createDailyOutput, string dailyOutputSuffix, string outputDirectory,
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
                    multiRun.OutputDirectory = outputDirectory;

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
