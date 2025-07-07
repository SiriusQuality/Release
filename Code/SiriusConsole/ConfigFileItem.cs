using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Soap;


namespace SiriusConsole
{
    #region Class for Xml groups

    public class GeneralConfigItems
    {
        public bool ByPassTranslation { get; set; }
        public bool ByPassSimulation { get; set; }
        public bool isSensAnalysis { get; set; }

        public GeneralConfigItems()
        {
            ByPassTranslation = true;
            ByPassSimulation = true;
            isSensAnalysis = false;
        }

    }

    public class TranslatorItems
    {
        public string InputACEFile { get; set; }
        
        public string SearchKeyStringExpName { get; set; }
        public bool isTranslatorQuiet { get; set; }

        public string DefaultRefFolder { get; set; }
        public string OptionalProjectFilesPath { get; set; }
        public string OptionalWeatherFilesPath { get; set; }
        public string OptionalOutputFilesPath { get; set; }

        public string OptionalProjectFileName { get; set; }
        public string OptionalSiteFileName { get; set; }
        public string OptionalSoilFileName { get; set; }
        public string OptionalManagementFileName { get; set; }
        public string OptionalRunFileName { get; set; }

        public string OptionalVarietalPathAndName { get; set; }
        public string OptionalNonVarietalPathAndName { get; set; }
        public string OptionalRunOptionPathAndName { get; set; }

        public TranslatorItems()
        {
            InputACEFile = "";
            
            SearchKeyStringExpName = "";
            isTranslatorQuiet = false;

            DefaultRefFolder = "";
            OptionalWeatherFilesPath = "";
            OptionalOutputFilesPath = "";

            OptionalProjectFileName = "";
            OptionalSiteFileName = "";
            OptionalSoilFileName = "";
            OptionalManagementFileName = "";
            OptionalRunFileName = "";

            OptionalVarietalPathAndName = "";
            OptionalNonVarietalPathAndName = "";
            OptionalRunOptionPathAndName = "";

        }

    }

    public class SimulationItems
    {
        public string ProjectPathAndFile { get; set; }
        public string OutputPath { get; set; }
        public string iToRun { get; set; }
        public bool SaveDailyOutput { get; set; }
        public bool areSelectDatesReadInObs { get; set; }
        public string pathOfObservations { get; set; }
        public List<string> obsFileToRead { get; set; }
        public List<DateTime> OutputSelectedDates { get; set; }
        public List<string> RunsToDisplay { get; set; }
        public List<string> RunsToSkip { get; set; }
        public bool ShowProgressSimu { get; set; }

        public OverrideItems ParOverride { get; set; }
        public SensitivityItems SensitivityAnalysis { get; set; }


        public SimulationItems()
        {
            ProjectPathAndFile = "";
            OutputPath = "";
            iToRun = "";
            SaveDailyOutput = true;
            areSelectDatesReadInObs = false;
            pathOfObservations = "";
            obsFileToRead = null;
            OutputSelectedDates = new List<DateTime>();
            RunsToDisplay = new List<String>();
            RunsToSkip = new List<String>();
            ShowProgressSimu = true;
            

            ParOverride = null;
            SensitivityAnalysis = null;
        }

    }

    public class OverrideItems
    {
        public List<String> SkipOverrideForRuns { get; set; }
        public List<VarPar> ListVarietalPars { get; set; }
        public List<NonVarPar> ListNonVarietalPars { get; set; }
        //public List<GlobNonVarPar> LisGlobtNonVarietalPars { get; set; }
        public List<SoilParVal> ListSoilParVals { get; set; }
        public List<SoilLayersVal> ListSoilLayersVal { get; set; }
        public List<SiteParVal> ListSitesParVals { get; set; }
        public List<ManParVal> ListManParVals { get; set; }
        public List<ManagementByDate> ListManByDate { get; set; }
        public List<ManagementByGrowthStage> ListManByGrowthStage { get; set; }

        public OverrideItems()
        {
            SkipOverrideForRuns = new List<String>();
            List<VarPar> ListVarietalPars = new List<VarPar>();
            ListNonVarietalPars = new List<NonVarPar>();
            //LisGlobtNonVarietalPars = new List<GlobNonVarPar>();
            ListSoilParVals = new List<SoilParVal>();
            ListSoilLayersVal = new List<SoilLayersVal>();
            ListSitesParVals = new List<SiteParVal>();
            ListManParVals = new List<ManParVal>();
            ListManByDate = new List<ManagementByDate>();
            ListManByGrowthStage = new List<ManagementByGrowthStage>();

        }

    }


    public class SensitivityItems
    {
        public string sensAnalysisMode { get; set; } //Default: all combination of parameter, OneByOne: 1 parameter by 1 parameter, Vector: input
        public List<SensivityAnalysis> ListSensAnalysis { get; set; }
        public List<SensivityAnalysisVector> ListSensAnalysisVector { get; set; }

        public SensitivityItems()
        {
            sensAnalysisMode = "";
            ListSensAnalysis = new List<SensivityAnalysis>();
            ListSensAnalysisVector = new List<SensivityAnalysisVector>();

        }

    }



    #endregion

    [Serializable]
    [XmlRootAttribute("ConfigFileItem", IsNullable = false)]
    public class ConfigFileItem
    {
        [XmlElement]
        public GeneralConfigItems General_Config_Items { get; set; }
        public TranslatorItems ACE_SQ_Translation { get; set; }
        public SimulationItems SimuItems { get; set; }

        #region Utilities

        #region Plant

        public Dictionary<string, Dictionary<string, double>> ConvertToDict(List<VarPar> ObjToConvert)
        {
            Dictionary<string, Dictionary<string, double>> result = new Dictionary<string, Dictionary<string, double>>();

            if (ObjToConvert != null)
            {

                foreach (VarPar ielem in ObjToConvert)
                {

                    result.Add(ielem.Name, new Dictionary<string, double>());

                    foreach (PairParNameValue ipair in ielem.ListParNameVal)
                    {
                        result[ielem.Name].Add(ipair.Name, ipair.Value);

                    }
                }

            }
            return result;

        }

        public Dictionary<string, Dictionary<string, double>> ConvertToDict(List<NonVarPar> ObjToConvert)
        {
            Dictionary<string, Dictionary<string, double>> result = new Dictionary<string, Dictionary<string, double>>();

            if (ObjToConvert != null)
            {

                foreach (NonVarPar ielem in ObjToConvert)
                {

                    result.Add(ielem.Name, new Dictionary<string, double>());

                    foreach (PairParNameValue ipair in ielem.ListParNameVal)
                    {
                        result[ielem.Name].Add(ipair.Name, ipair.Value);

                    }
                }

            }
            return result;

        }

    /*    public Dictionary<string, Dictionary<string, double>> ConvertToDict(List<GlobNonVarPar> ObjToConvert)
        {
            Dictionary<string, Dictionary<string, double>> result = new Dictionary<string, Dictionary<string, double>>();

            if (ObjToConvert != null)
            {

                foreach (GlobNonVarPar ielem in ObjToConvert)
                {

                    result.Add(ielem.Name, new Dictionary<string, double>());

                    foreach (PairParNameValue ipair in ielem.ListGlobParNameVal)
                    {
                        result[ielem.Name].Add(ipair.Name, ipair.Value);

                    }
                }

            }
            return result;

        }
    */
        #endregion

        #region Soil

        public Dictionary<string, Dictionary<string, dynamic>> ConvertToDict(List<SoilParVal> ObjToConvert)
        {
            Dictionary<string, Dictionary<string, dynamic>> result = new Dictionary<string, Dictionary<string, dynamic>>();

            if (ObjToConvert != null)
            {

                foreach (SoilParVal ielem in ObjToConvert)
                {

                    result.Add(ielem.Name, new Dictionary<string, dynamic>());

                    foreach (PairParNameValue ipair in ielem.ListParNameVal)
                    {
                        result[ielem.Name].Add(ipair.Name, ipair.Value);

                    }

                    foreach (PairParNameBool ipair in ielem.ListParNameBool)
                    {
                        result[ielem.Name].Add(ipair.Name, ipair.Flag);

                    }
                }

            }

            return result;

        }
        /*
        public Dictionary<string, Dictionary<string, bool>> ConvertToDict(List<SoilParBool> ObjToConvert)
        {
            Dictionary<string, Dictionary<string, bool>> result = new Dictionary<string, Dictionary<string, bool>>();

            if (ObjToConvert != null)
            {

                foreach (SoilParBool ielem in ObjToConvert)
                {


                    result.Add(ielem.Name, new Dictionary<string, bool>());

                    foreach (PairParNameBool ipair in ielem.ListParNameBool)
                    {
                        result[ielem.Name].Add(ipair.Name, ipair.Flag);

                    }

                }
            }
            return result;

        }
         * */

        public Dictionary<string, Dictionary<int, Dictionary<string, double>>> ConvertToDict(List<SoilLayersVal> ObjToConvert)
        {
            Dictionary<string, Dictionary<int, Dictionary<string, double>>> result = new Dictionary<string, Dictionary<int, Dictionary<string, double>>>();

            if (ObjToConvert != null)
            {

                foreach (SoilLayersVal ielem in ObjToConvert)
                {
                    result.Add(ielem.Name, new Dictionary<int, Dictionary<string, double>>());

                    foreach (TripletParNameValueiLayer invil in ielem.ListParNameValiLayer)
                    {
                        result[ielem.Name].Add(invil.iLayer, new Dictionary<string, double>());

                        foreach (PairParNameValue ipair in invil.ListParNameVal)
                        {
                            result[ielem.Name][invil.iLayer].Add(ipair.Name, ipair.Value);

                        }
                    }
                }
            }
            return result;

        }

        #endregion

        #region Site

        public Dictionary<string, Dictionary<string, dynamic>> ConvertToDict(List<SiteParVal> ObjToConvert)
        {
            Dictionary<string, Dictionary<string, dynamic>> result = new Dictionary<string, Dictionary<string, dynamic>>();

            if (ObjToConvert != null)
            {

                foreach (SiteParVal ielem in ObjToConvert)
                {

                    result.Add(ielem.Name, new Dictionary<string, dynamic>());

                    foreach (PairParNameValue ipair in ielem.ListParNameVal)
                    {
                        result[ielem.Name].Add(ipair.Name, ipair.Value);

                    }

                    foreach (PairParNameDate ipair in ielem.ListParNameDate)
                    {
                        result[ielem.Name].Add(ipair.Name, ipair.Value);

                    }
                }

            }

            return result;

        }

        /*
        public Dictionary<string, Dictionary<string, DateTime>> ConvertToDict(List<SiteSowDate> ObjToConvert)
        {
            Dictionary<string, Dictionary<string, DateTime>> result = new Dictionary<string, Dictionary<string, DateTime>>();

            if (ObjToConvert != null)
            {

                foreach (SiteSowDate ielem in ObjToConvert)
                {

                    result.Add(ielem.Name, new Dictionary<string, DateTime>());

                    foreach (PairParNameDate ipair in ielem.ListParNameDate)
                    {
                        result[ielem.Name].Add(ipair.Name, ipair.Value);

                    }
                }

            }

            return result;

        }
         */

        #endregion

        #region Managment

        public Dictionary<string, Dictionary<string, dynamic>> ConvertToDict(List<ManParVal> ObjToConvert)
        {
            Dictionary<string, Dictionary<string, dynamic>> result = new Dictionary<string, Dictionary<string, dynamic>>();

            if (ObjToConvert != null)
            {

                foreach (ManParVal ielem in ObjToConvert)
                {

                    result.Add(ielem.Name, new Dictionary<string, dynamic>());

                    foreach (PairParNameValue ipair in ielem.ListParNameVal)
                    {
                        result[ielem.Name].Add(ipair.Name, ipair.Value);

                    }

                    foreach (PairParNameBool ipair in ielem.ListParNameBool)
                    {
                        result[ielem.Name].Add(ipair.Name, ipair.Flag);

                    }

                    foreach (PairParNameString ipair in ielem.ListParNameString)
                    {
                        result[ielem.Name].Add(ipair.Name, ipair.Str);

                    }

                    foreach (PairParNameDate ipair in ielem.ListParNameDate)
                    {
                        result[ielem.Name].Add(ipair.Name, ipair.Value);

                    }

                }

            }

            return result;

        }


        public Dictionary<string, Dictionary<DateTime, dynamic[]>> ConvertToDict(List<ManagementByDate> ObjToConvert)
        {
            Dictionary<string, Dictionary<DateTime, dynamic[]>> result = new Dictionary<string, Dictionary<DateTime, dynamic[]>>();

            foreach (ManagementByDate mbd in ObjToConvert)
            {

                result.Add(mbd.Name, new Dictionary<DateTime, dynamic[]>());

                foreach (ManagementByDateItem mbdi in mbd.items.OrderBy(x=>x.Date))
                {

                    dynamic[] tab = new dynamic[3];
                    tab[0] = mbdi.FetilizerName;
                    tab[1] = mbdi.Nitrogen;
                    tab[2] = mbdi.Water;

                    result[mbd.Name].Add(mbdi.Date, tab);

                }


            }
                return result;
        }

        public Dictionary<string, Dictionary<string, dynamic[]>> ConvertToDict(List<ManagementByGrowthStage> ObjToConvert)
        {

            Dictionary<string, Dictionary<string, dynamic[]>> result = new Dictionary<string, Dictionary<string, dynamic[]>>();

            foreach (ManagementByGrowthStage mbd in ObjToConvert)
            {

                result.Add(mbd.Name, new Dictionary<string, dynamic[]>());

                foreach (ManagementByGrowthStageItem mbdi in mbd.items.OrderBy(x => x.GrowthStage))
                {

                    dynamic[] tab = new dynamic[3];
                    tab[0] = mbdi.FetilizerName;
                    tab[1] = mbdi.Nitrogen;
                    tab[2] = mbdi.Water;

                    result[mbd.Name].Add(mbdi.GrowthStage, tab);

                }


            }

            return result;
        }

        #endregion

        #region Sensitivity Analysis

        public Dictionary<string,Dictionary<string,  dynamic[]>> ConvertToDict(List<SensivityAnalysis> ObjToConvert)
        {

            Dictionary<string, Dictionary<string, dynamic[]>> result = new Dictionary<string,Dictionary<string,  dynamic[]>>();

            result.Add(SimuItems.SensitivityAnalysis.sensAnalysisMode, new Dictionary<string, dynamic[]>());


                foreach (SensivityAnalysis sa in ObjToConvert.OrderBy(x => x.ParName))
                {


                    dynamic[] tab = new dynamic[4];
                    tab[0] = sa.SensivityMode;
                    tab[1] = sa.NbSteps;
                    tab[2] = sa.Max;
                    tab[3] = sa.Min;


                    result[SimuItems.SensitivityAnalysis.sensAnalysisMode].Add(sa.ParName, tab);


                }

            return result;
        }


        public Dictionary<string, Dictionary<string, dynamic[]>> ConvertToDict(List<SensivityAnalysisVector> ObjToConvert)
        {

            Dictionary<string, Dictionary<string, dynamic[]>> result = new Dictionary<string, Dictionary<string, dynamic[]>>();

            result.Add(SimuItems.SensitivityAnalysis.sensAnalysisMode, new Dictionary<string, dynamic[]>());


            foreach (SensivityAnalysisVector sav in ObjToConvert.OrderBy(x => x.ParName))
            {

                int countvec = sav.vector.Length;
                dynamic[] tab = new dynamic[countvec];

                for (int i = 0; i < countvec; i++) tab[i] = sav.vector[i];


                result[SimuItems.SensitivityAnalysis.sensAnalysisMode].Add(sav.ParName, tab);


            }

            return result;
        }


        #endregion


        #endregion

        #region Constructors

        public ConfigFileItem()
        {

        }
        public ConfigFileItem(bool init)
        {
            General_Config_Items = new GeneralConfigItems();
            ACE_SQ_Translation = new TranslatorItems();
            SimuItems = new SimulationItems();
            SimuItems.ParOverride = new OverrideItems();
            SimuItems.SensitivityAnalysis = new SensitivityItems();

            if (init)
            {

                #region Global

                SimuItems.ProjectPathAndFile = "C:/SQ_Release/1-Project/Project.sqpro";
                SimuItems.OutputPath = "C:/SQ_Release/3-Output/";
                //ACE_SQ_Translation.InputACEFile = "C:/Users/mancealo/Documents/DesktopBU/Modules Rami/Data/projets SQ_FACE/1-Project/Hohenfinow.json";
                //ACE_SQ_Translation.DefaultRefFolder = "C:/Users/mancealo/Documents/DesktopBU/Modules Rami/Data/projets SQ_FACE/";
                //ACE_SQ_Translation.SearchKeyStringExpName = "h";

                //ACE_SQ_Translation.OptionalProjectFilesPath = "C:/Users/ mancealo/ Documents/DesktopBU/Modules Rami/Data/TestPro";
                //ACE_SQ_Translation.OptionalWeatherFilesPath = "C:/Users/ mancealo/ Documents/DesktopBU/Modules Rami/Data/TestWea";
                //ACE_SQ_Translation.OptionalOutputFilesPath = "C:/Users/ mancealo/ Documents/DesktopBU/Modules Rami/Data/TestOut";

                //ACE_SQ_Translation.OptionalProjectFileName = "OptionalProjectFileName.pro";
                //ACE_SQ_Translation.OptionalSiteFileName = "OptionalSiteFileName.si";
                //ACE_SQ_Translation.OptionalSoilFileName = "OptionalSoilFileName.so";
                //ACE_SQ_Translation.OptionalManagementFileName = "OptionalManagementFileName.man";
                //ACE_SQ_Translation.OptionalRunFileName = "OptionalRunFileName.run";


                SimuItems.SaveDailyOutput = true;
                SimuItems.ShowProgressSimu = true;
                /*
                SimuItems.ParOverride.SkipOverrideForRuns = new List<string>();
                //SimuItems.SkipOverrideForRuns.Add("MaricopaFACE");
                SimuItems.ParOverride.SkipOverrideForRuns.Add("Lincoln_Rainshelter");

                SimuItems.SkipRuns = new List<string>();
                SimuItems.SkipRuns.Add("Lincoln91");

                SimuItems.OutputSelectedDates = new List<DateTime>();
                SimuItems.OutputSelectedDates.Add(new DateTime(1996, 12, 25));
                SimuItems.OutputSelectedDates.Add(new DateTime(1997, 02, 05));
                SimuItems.OutputSelectedDates.Add(new DateTime(1997, 02, 05));
                SimuItems.OutputSelectedDates.Add(new DateTime(1997, 01, 05));
                */

                General_Config_Items.ByPassTranslation = false;
                General_Config_Items.ByPassSimulation = false;
                General_Config_Items.isSensAnalysis = false;

                /*
                ACE_SQ_Translation.isTranslatorQuiet = false;

                #endregion

                #region Plant Parameters

                List<string> nparl = new List<string>();
                nparl.Add("AreaPL");
                nparl.Add("AreaSL");

                List<double> vall = new List<double>();
                vall.Add(27.378);
                vall.Add(15.0);

                SimuItems.ParOverride.ListVarietalPars = new List<VarPar>();
                //SimuItems.ParOverride.ListVarietalPars.Add(new VarPar("Yecora Rojo", nparl, vall));
                //SimuItems.ParOverride.ListVarietalPars.Add(new VarPar("Batten", nparl, vall));

                List<string> nparlnv = new List<string>();
                nparlnv.Add("PhyllDurationDMlost");

                List<double> vallnv = new List<double>();
                vallnv.Add(2.0);

                SimuItems.ParOverride.ListNonVarietalPars = new List<NonVarPar>();
                //SimuItems.ParOverride.ListNonVarietalPars.Add(new NonVarPar("Spring_Wheat", nparlnv, vallnv));

                #endregion

                #region Soil Parameters

                List<string> soilparl = new List<string>();
                soilparl.Add("Bd");
                soilparl.Add("Gravel");

                List<double> soilvall = new List<double>();
                soilvall.Add(2.0);
                soilvall.Add(2.0);

                List<string> soilparlbool = new List<string>();
                soilparlbool.Add("IsKqCalc");
                soilparlbool.Add("IsOrgNCalc");

                List<bool> soilbool = new List<bool>();
                soilbool.Add(true);
                soilbool.Add(true);

                SimuItems.ParOverride.ListSoilParVals = new List<SoilParVal>();

                //SimuItems.ParOverride.ListSoilParVals.Add(new SoilParVal("Lincoln91-92", soilparl, soilvall, soilparlbool, soilbool));




                List<PairParNameValue> lppnv1 = new List<PairParNameValue>();
                lppnv1.Add(new PairParNameValue("SSAT", 61.0));
                lppnv1.Add(new PairParNameValue("SDUL", 13.0));

                List<PairParNameValue> lppnv2 = new List<PairParNameValue>();
                lppnv2.Add(new PairParNameValue("Kql", 1.0));

                List<TripletParNameValueiLayer> lppnvil = new List<TripletParNameValueiLayer>();
                lppnvil.Add(new TripletParNameValueiLayer(0, lppnv1));
                lppnvil.Add(new TripletParNameValueiLayer(1, lppnv2));

                SimuItems.ParOverride.ListSoilLayersVal = new List<SoilLayersVal>();
                //SimuItems.ParOverride.ListSoilLayersVal.Add(new SoilLayersVal("Lincoln91-92", lppnvil));

                #endregion

                #region Site Parameters

                List<string> siteparl = new List<string>();
                siteparl.Add("Latitude");

                List<double> sitevall = new List<double>();
                sitevall.Add(-60);

                List<string> sitepardwl = new List<string>();
                sitepardwl.Add("MinSowingDate");
                sitepardwl.Add("MaxSowingDate");

                List<DateTime> sitevaldwl = new List<DateTime>();
                sitevaldwl.Add(new DateTime(1991, 12, 01));
                sitevaldwl.Add(new DateTime(1992, 02, 01));

                SimuItems.ParOverride.ListSitesParVals = new List<SiteParVal>();

                //SimuItems.ParOverride.ListSitesParVals.Add(new SiteParVal("LincolnRS91-92", siteparl, sitevall, sitepardwl, sitevaldwl));

                //List<string> sitepardwl = new List<string>();
                //sitepardwl.Add("MinSowingDate");
                //sitepardwl.Add("MaxSowingDate");

                //List<DateTime> sitevaldwl = new List<DateTime>();
                //sitevaldwl.Add(new DateTime(1991, 12, 01));
                //sitevaldwl.Add(new DateTime(1992, 02, 01));

                //ListSiteSowDates = new List<SiteSowDate>();

                //ListSiteSowDates.Add(new SiteSowDate("LincolnRS91-92", sitepardwl, sitevaldwl));

                #endregion

                #region Management Parameters

                //List<string> manparl = new List<string>();
                //manparl.Add("TargetFertileShootNumber");

                //List<double> manvall = new List<double>();
                //manvall.Add(500);

                //List<string> manparlbool = new List<string>();
                //manparlbool.Add("IsTotalNitrogen");

                //List<bool> manbool = new List<bool>();
                //manbool.Add(true);

                List<string> manparstrl = new List<string>();
                //manparstrl.Add("AOMName");

                List<string> manparstrvall = new List<string>();
                //manparstrvall.Add("Urea_Ammonium_Nitrate");

                //List<string> manpardwl = new List<string>();
                //manpardwl.Add("SowingDate");

                //List<DateTime> manvaldwl = new List<DateTime>();
                //manvaldwl.Add(new DateTime(1991, 07, 01));

                List<string> manparl = new List<string>();
                manparl.Add("NbDays2SowWin");

                List<double> manvall = new List<double>();
                manvall.Add(20);

                List<string> manparlbool = new List<string>();
                manparlbool.Add("StartBefSow");

                List<bool> manbool = new List<bool>();
                manbool.Add(false);

                List<string> manpardwl = new List<string>();
                //manpardwl.Add("SimStartDate");

                List<DateTime> manvaldwl = new List<DateTime>();
                //manvaldwl.Add(new DateTime(2005, 12, 10));

                SimuItems.ParOverride.ListManParVals = new List<ManParVal>();

                //SimuItems.ParOverride.ListManParVals.Add(new ManParVal("RS05", manparl, manvall, manparlbool, manbool, manparstrl, manparstrvall, manpardwl, manvaldwl));

                SimuItems.ParOverride.ListManByDate = new List<ManagementByDate>();

                List<DateTime> mbddl0 = new List<DateTime>();
                mbddl0.Add(new DateTime(1991, 08, 04));
                mbddl0.Add(new DateTime(1991, 10, 04));

                List<string> mbdsl0 = new List<string>();
                mbdsl0.Add("None");
                mbdsl0.Add("Urea");

                List<double> anl0 = new List<double>();
                anl0.Add(0.0);
                anl0.Add(10.0);

                List<double> awl0 = new List<double>();
                awl0.Add(5.0);
                awl0.Add(0.0);


                //SimuItems.ParOverride.ListManByDate.Add(new ManagementByDate("RS05", mbddl0, mbdsl0, anl0, awl0));

                List<DateTime> mbddl1 = new List<DateTime>();
                mbddl1.Add(new DateTime(1991, 11, 04));

                List<string> mbdsl1 = new List<string>();
                mbdsl1.Add("None");

                List<double> anl1 = new List<double>();
                anl1.Add(-0.0);

                List<double> awl1 = new List<double>();
                awl1.Add(30.0);

                //SimuItems.ParOverride.ListManByDate.Add(new ManagementByDate("RS11", mbddl1, mbdsl1, anl1, awl1));


                SimuItems.ParOverride.ListManByGrowthStage = new List<ManagementByGrowthStage>();

                List<string> mgsl0 = new List<string>();
                mgsl0.Add("ZC_00_Sowing");
                mgsl0.Add("ZC_32_2ndNodeDetectable");

                List<string> mgsnl0 = new List<string>();
                mgsnl0.Add("None");
                mgsnl0.Add("Urea");

                List<double> angsl0 = new List<double>();
                angsl0.Add(0.0);
                angsl0.Add(10.0);

                List<double> awgsl0 = new List<double>();
                awgsl0.Add(5.0);
                awgsl0.Add(0.0);


                //SimuItems.ParOverride.ListManByGrowthStage.Add(new ManagementByGrowthStage("RS05", mgsl0, mgsnl0, angsl0, awgsl0));


                #endregion

                #region Sensitivity Analysis

                //sensAnalysisMode = "Default";
                //isSensAnalysis = true;
                //ListSensAnalysis = new List<SensivityAnalysis>();
                //ListSensAnalysis.Add(new SensivityAnalysis("Management.CO2", "RegularMinMax", 5, 500, 200));
                //ListSensAnalysis.Add(new SensivityAnalysis("Crop.Dcd", "RegularPercent", 5, -10, 10));

                SimuItems.SensitivityAnalysis.sensAnalysisMode = "Vector";

                SimuItems.SensitivityAnalysis.ListSensAnalysisVector = new List<SensivityAnalysisVector>();
                double[] tab = new double[3];
                tab[0] = 300;
                tab[1] = 200;
                tab[2] = 500;
                SimuItems.SensitivityAnalysis.ListSensAnalysisVector.Add(new SensivityAnalysisVector("Management.CO2", tab));
                */

                #endregion

            }
        }

        #endregion
    }

    #region Classes

    #region Plant Parameters

    public class VarPar : IEquatable<VarPar>, IComparable<VarPar>
    {
        public string Name { get; set; } // Name of the parameter
        public List<PairParNameValue> ListParNameVal { get; set; } // Name of the parameter


        public VarPar(string name, List<string> par, List<double> val)
        {
            Name = name;
            ListParNameVal = new List<PairParNameValue>();
            int i=0;

            foreach (string ipar in par)
            {
                ListParNameVal.Add(new PairParNameValue(ipar, val[i]));
                i++;
            }

        }

        public VarPar()
        {
        }

        public bool Equals(VarPar other)
        {
            return (other.Name == this.Name);
        }

        public int CompareTo(VarPar other)
        {
            return this.Name.CompareTo(other.Name);
        }




    }

    public class NonVarPar : IEquatable<NonVarPar>, IComparable<NonVarPar>
    {
        public string Name { get; set; } // Name of the parameter
        public List<PairParNameValue> ListParNameVal { get; set; } // Name of the parameter


        public NonVarPar(string name, List<string> par, List<double> val)
        {
            Name = name;
            ListParNameVal = new List<PairParNameValue>();
            int i=0;

            foreach (string ipar in par)
            {
                ListParNameVal.Add(new PairParNameValue(ipar, val[i]));
                i++;
            }

        }

        public NonVarPar()
        {
        }

        public bool Equals(NonVarPar other)
        {
            return (other.Name == this.Name);
        }

        public int CompareTo(NonVarPar other)
        {
            return this.Name.CompareTo(other.Name);
        }


    }

 /*   public class GlobNonVarPar : IEquatable<GlobNonVarPar>, IComparable<GlobNonVarPar>
    {
        public string Name { get; set; } // Name of the parameter
        public List<PairParNameValue> ListGlobParNameVal { get; set; } // Name of the parameter


        public GlobNonVarPar(string name, List<string> par, List<double> val)
        {
            Name = name;
            ListGlobParNameVal = new List<PairParNameValue>();
            int i = 0;

            foreach (string ipar in par)
            {
                ListGlobParNameVal.Add(new PairParNameValue(ipar, val[i]));
                i++;
            }

        }

        public GlobNonVarPar()
        {
        }

        public bool Equals(GlobNonVarPar other)
        {
            return (other.Name == this.Name);
        }

        public int CompareTo(GlobNonVarPar other)
        {
            return this.Name.CompareTo(other.Name);
        }


    }
 */


    #endregion

    #region Soil

    public class SoilParVal : IEquatable<SoilParVal>, IComparable<SoilParVal>
    {
        public string Name { get; set; } // Name of the parameter
        public List<PairParNameValue> ListParNameVal { get; set; } // Name of the parameter
        public List<PairParNameBool> ListParNameBool { get; set; } // Name of the parameter


        public SoilParVal(string name, List<string> parval, List<double> val, List<string> parbool, List<bool> flag)
        {
            Name = name;
            ListParNameVal = new List<PairParNameValue>();
            ListParNameBool = new List<PairParNameBool>();
            int i = 0;

            foreach (string ipar in parval)
            {
                ListParNameVal.Add(new PairParNameValue(ipar, val[i]));
                i++;
            }
            i = 0;
            foreach (string ipar in parbool)
            {
                ListParNameBool.Add(new PairParNameBool(ipar, flag[i]));
                i++;
            }

        }

        public SoilParVal()
        {
        }

        public bool Equals(SoilParVal other)
        {
            return (other.Name == this.Name);
        }

        public int CompareTo(SoilParVal other)
        {
            return this.Name.CompareTo(other.Name);
        }


    }
    /*
    public class SoilParBool : IEquatable<SoilParBool>, IComparable<SoilParBool>
    {
        public string Name { get; set; } // Name of the parameter
        public List<PairParNameBool> ListParNameBool { get; set; } // Name of the parameter


        public SoilParBool(string name, List<string> par, List<bool> flag)
        {
            Name = name;
            ListParNameBool = new List<PairParNameBool>();
            int i = 0;

            foreach (string ipar in par)
            {
                ListParNameBool.Add(new PairParNameBool(ipar, flag[i]));
                i++;
            }

        }

        public SoilParBool()
        {
        }

        public bool Equals(SoilParBool other)
        {
            return (other.Name == this.Name);
        }

        public int CompareTo(SoilParBool other)
        {
            return this.Name.CompareTo(other.Name);
        }


    }
     */

    public class SoilLayersVal : IEquatable<SoilLayersVal>, IComparable<SoilLayersVal>
    {
        public string Name { get; set; } // Name of the parameter
        public List<TripletParNameValueiLayer> ListParNameValiLayer { get; set; } // Name of the parameter


        public SoilLayersVal(string name, List<TripletParNameValueiLayer> parnamevalilayer)
        {
            Name = name;
            ListParNameValiLayer = parnamevalilayer;

        }

        public SoilLayersVal()
        {
        }

        public bool Equals(SoilLayersVal other)
        {
            return (other.Name == this.Name);
        }

        public int CompareTo(SoilLayersVal other)
        {
            return this.Name.CompareTo(other.Name);
        }


    }

    #endregion

    #region Site

    public class SiteParVal : IEquatable<SiteParVal>, IComparable<SiteParVal>
    {
        public string Name { get; set; } // Name of the parameter
        public List<PairParNameValue> ListParNameVal { get; set; } // Name of the parameter
        public List<PairParNameDate> ListParNameDate { get; set; } // Name of the parameter


        public SiteParVal(string name, List<string> parval, List<double> val, List<string> pardate, List<DateTime> date)
        {
            Name = name;
            ListParNameVal = new List<PairParNameValue>();
            ListParNameDate = new List<PairParNameDate>();
            int i = 0;

            foreach (string ipar in parval)
            {
                ListParNameVal.Add(new PairParNameValue(ipar, val[i]));
                i++;
            }

            i = 0;

            

            foreach (string ipar in pardate)
            {
                ListParNameDate.Add(new PairParNameDate(ipar, date[i]));
                i++;
            }

        }

        public SiteParVal()
        {
        }

        public bool Equals(SiteParVal other)
        {
            return (other.Name == this.Name);
        }

        public int CompareTo(SiteParVal other)
        {
            return this.Name.CompareTo(other.Name);
        }


    }
    /*
    public class SiteSowDate : IEquatable<SiteSowDate>, IComparable<SiteSowDate>
    {
        public string Name { get; set; } // Name of the parameter
        public List<PairParNameDate> ListParNameDate { get; set; } // Name of the parameter


        public SiteSowDate(string name, List<string> par, List<DateTime> val)
        {
            Name = name;
            ListParNameDate = new List<PairParNameDate>();
            int i = 0;

            foreach (string ipar in par)
            {
                ListParNameDate.Add(new PairParNameDate(ipar, val[i]));
                i++;
            }

        }

        public SiteSowDate()
        {
        }

        public bool Equals(SiteSowDate other)
        {
            return (other.Name == this.Name);
        }

        public int CompareTo(SiteSowDate other)
        {
            return this.Name.CompareTo(other.Name);
        }


    }
     */

    #endregion

    #region Managment

    public class ManParVal : IEquatable<ManParVal>, IComparable<ManParVal>
    {
        public string Name { get; set; } // Name of the parameter
        public List<PairParNameValue> ListParNameVal { get; set; } // Name of the parameter
        public List<PairParNameBool> ListParNameBool { get; set; } // Name of the parameter
        public List<PairParNameString> ListParNameString { get; set; } // Name of the parameter
        public List<PairParNameDate> ListParNameDate { get; set; } // Name of the parameter


        public ManParVal(string name, List<string> parval, List<double> val, List<string> parbool, List<bool> flag,
            List<string> parstr, List<string> str, List<string> pardate, List<DateTime> date)
        {
            Name = name;
            ListParNameVal = new List<PairParNameValue>();
            ListParNameBool = new List<PairParNameBool>();
            ListParNameString = new List<PairParNameString>();
            ListParNameDate = new List<PairParNameDate>();

            int i = 0;

            foreach (string ipar in parval)
            {
                ListParNameVal.Add(new PairParNameValue(ipar, val[i]));
                i++;
            }

            i = 0;

            foreach (string ipar in parbool)
            {
                ListParNameBool.Add(new PairParNameBool(ipar, flag[i]));
                i++;
            }

            i = 0;

            foreach (string ipar in parstr)
            {
                ListParNameString.Add(new PairParNameString(ipar, str[i]));
                i++;
            }

            i = 0;

            foreach (string ipar in pardate)
            {
                ListParNameDate.Add(new PairParNameDate(ipar, date[i]));
                i++;
            }

        }

        public ManParVal()
        {
        }

        public bool Equals(ManParVal other)
        {
            return (other.Name == this.Name);
        }

        public int CompareTo(ManParVal other)
        {
            return this.Name.CompareTo(other.Name);
        }


    }

    public class ManagementByDate : IEquatable<ManagementByDate>, IComparable<ManagementByDate>
    {
        public string Name { get; set; }
        public List<ManagementByDateItem> items { get; set; }



        public ManagementByDate(string name, List<DateTime> datel, List<string> fertname, List<double> nitro, List<double> warter)
        {
            Name=name;
            items = new List<ManagementByDateItem>();

            int i = 0;

            foreach(DateTime dt in datel){


                items.Add(new ManagementByDateItem(dt, fertname[i], nitro[i], warter[i]));

                i++;
            }

            
        }

         public ManagementByDate()
        {
        }

        public bool Equals(ManagementByDate other)
        {
            return (other.Name == this.Name);
        }

        public int CompareTo(ManagementByDate other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }

    public class ManagementByGrowthStage : IEquatable<ManagementByGrowthStage>, IComparable<ManagementByGrowthStage>
    {
        public string Name { get; set; }
        public List<ManagementByGrowthStageItem> items { get; set; }



        public ManagementByGrowthStage(string name, List<string> gsl, List<string> fertname, List<double> nitro, List<double> water)
        {
            Name = name;
            items = new List<ManagementByGrowthStageItem>();

            int i = 0;

            foreach (string gst in gsl)
            {


                items.Add(new ManagementByGrowthStageItem(gst, fertname[i], nitro[i], water[i]));

                i++;
            }


        }

        public ManagementByGrowthStage()
        {
        }

        public bool Equals(ManagementByGrowthStage other)
        {
            return (other.Name == this.Name);
        }

        public int CompareTo(ManagementByGrowthStage other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
    

    #endregion

    #region Sensivity

    public class SensivityAnalysis : IEquatable<SensivityAnalysis>, IComparable<SensivityAnalysis>
    {

        public string ParName { get; set; }
        public string SensivityMode { get; set; }
        public int NbSteps { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }



        public SensivityAnalysis(string pn,string svtm, int nbs, double max, double min)
        {
            ParName = pn;
            SensivityMode = svtm;
            NbSteps = nbs;
            Max = max;
            Min = min;

        }

        public SensivityAnalysis()
        {

        }

        public bool Equals(SensivityAnalysis other)
        {
            return (other.SensivityMode == this.SensivityMode);
        }

        public int CompareTo(SensivityAnalysis other)
        {
            return (this.SensivityMode.CompareTo(other.SensivityMode));
        }

    }


    public class SensivityAnalysisVector : IEquatable<SensivityAnalysisVector>, IComparable<SensivityAnalysisVector>
    {

        public string ParName { get; set; }
        public double[] vector { get; set; }



    public SensivityAnalysisVector(string pn, double[] vec)
    {
            ParName = pn;
            vector = vec;


    }

    public SensivityAnalysisVector()
    {

    }

    public bool Equals(SensivityAnalysisVector other)
    {
        return (other.ParName == this.ParName);
    }

    public int CompareTo(SensivityAnalysisVector other)
    {
        return (this.ParName.CompareTo(other.ParName));
    }

}

#endregion

    #region Utilities

public class PairParNameValue : IEquatable<PairParNameValue>, IComparable<PairParNameValue>
    {
        public string Name { get; set; }
        public double Value { get; set; }


        public PairParNameValue(string name, double val)
        {
            Name = name;
            Value = val;

        }

        public PairParNameValue()
        {

        }

        public bool Equals(PairParNameValue other)
        {
            return (other.Name == this.Name && other.Value == this.Value);
        }

        public int CompareTo(PairParNameValue other)
        {
            return (this.Name.CompareTo(other.Name));
        }

    }

    public class PairParNameDate : IEquatable<PairParNameDate>, IComparable<PairParNameDate>
    {
        public string Name { get; set; }
        public DateTime Value { get; set; }


        public PairParNameDate(string name, DateTime val)
        {
            Name = name;
            Value = val;

        }

        public PairParNameDate()
        {

        }

        public bool Equals(PairParNameDate other)
        {
            return (other.Name == this.Name && other.Value == this.Value);
        }

        public int CompareTo(PairParNameDate other)
        {
            return (this.Name.CompareTo(other.Name));
        }

    }


    public class PairParNameBool : IEquatable<PairParNameBool>, IComparable<PairParNameBool>
    {
        public string Name { get; set; }
        public bool Flag { get; set; }


        public PairParNameBool(string name, bool flag)
        {
            Name = name;
            Flag = flag;

        }

        public PairParNameBool()
        {

        }

        public bool Equals(PairParNameBool other)
        {
            return (other.Name == this.Name && other.Flag == this.Flag);
        }

        public int CompareTo(PairParNameBool other)
        {
            return (this.Name.CompareTo(other.Name));
        }

    }

    public class PairParNameString : IEquatable<PairParNameString>, IComparable<PairParNameString>
    {
        public string Name { get; set; }
        public string Str { get; set; }


        public PairParNameString(string name, string str)
        {
            Name = name;
            Str = str;

        }

        public PairParNameString()
        {

        }

        public bool Equals(PairParNameString other)
        {
            return (other.Name == this.Name && other.Str == this.Str);
        }

        public int CompareTo(PairParNameString other)
        {
            return (this.Name.CompareTo(other.Name));
        }

    }


    public class TripletParNameValueiLayer : IEquatable<TripletParNameValueiLayer>, IComparable<TripletParNameValueiLayer>
    {
        public int iLayer { get; set; }
        public List<PairParNameValue> ListParNameVal { get; set; }


        public TripletParNameValueiLayer(int ilayer, List<PairParNameValue> namevalue)
        {
            ListParNameVal = new List<PairParNameValue>();
            foreach (PairParNameValue ielem in namevalue) ListParNameVal.Add(ielem);
            iLayer=ilayer;

        }

        public TripletParNameValueiLayer()
        {

        }

        public bool Equals(TripletParNameValueiLayer other)
        {
            return (other.iLayer == this.iLayer);
        }

        public int CompareTo(TripletParNameValueiLayer other)
        {
            return (this.iLayer.CompareTo(other.iLayer));
        }

    }


    public class ManagementByDateItem : IEquatable<ManagementByDateItem>, IComparable<ManagementByDateItem>
    {
        public DateTime Date { get; set; }
        public string FetilizerName { get; set; }
        public double Nitrogen { get; set; }
        public double Water { get; set; }



        public ManagementByDateItem(DateTime date, string fertname, double nitrogen, double water)
        {
            Date = date;
            FetilizerName = fertname;
            Nitrogen = nitrogen;
            Water = water;

        }

        public ManagementByDateItem()
        {

        }

        public bool Equals(ManagementByDateItem other)
        {
            return (other.Date == this.Date);
        }

        public int CompareTo(ManagementByDateItem other)
        {
            return (this.Date.CompareTo(other.Date));
        }

    }

    public class ManagementByGrowthStageItem : IEquatable<ManagementByGrowthStageItem>, IComparable<ManagementByGrowthStageItem>
    {
        public string GrowthStage { get; set; }
        public string FetilizerName { get; set; }
        public double Nitrogen { get; set; }
        public double Water { get; set; }



        public ManagementByGrowthStageItem(string gs, string fertname, double nitrogen, double water)
        {
            GrowthStage = gs;
            FetilizerName = fertname;
            Nitrogen = nitrogen;
            Water = water;

        }

        public ManagementByGrowthStageItem()
        {

        }

        public bool Equals(ManagementByGrowthStageItem other)
        {
            return (other.GrowthStage == this.GrowthStage);
        }

        public int CompareTo(ManagementByGrowthStageItem other)
        {
            return (this.GrowthStage.CompareTo(other.GrowthStage));
        }

    }




     #endregion

     #endregion

}
