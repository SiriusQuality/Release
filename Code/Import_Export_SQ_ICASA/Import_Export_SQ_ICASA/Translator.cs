using System;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Import_Export_SQ_ICASA
{
    public class Translator
    {
        string Filename { get; set; }

        string InputFile { get; set; }

        string ProjectFolderPath { get; set; }

        string WeatherFolderPathGUI { get; set; }

        string OutputFolderPathGUI { get; set; }

        string ProjectFileGUI { get; set; }

        string SiteFileGUI { get; set; }

        string SoilFileGUI { get; set; }

        string ManagementFileGUI { get; set; }

        string RunFileGUI { get; set; }

        string VarietalFileGUI { get; set; }

        string NonVarietalFileGUI { get; set; }

        string RunOptionFileGUI { get; set; }

        //TR - 05/22
        string OutputFormatFileGUI { get; set; }

        string EnvirotypingFileGUI { get; set; }

        string ModelConfigurationFileGUI { get; set; }

        string GraphFileGUI { get; set; }

        string ObservationFileGUI { get; set; }

        //string SearchKeyWordExpName { get; set; }

        bool isQuiet {get;set;}

        string OptionalProjectFilesPath { get; set; }
        string OptionalWeatherFilesPath { get; set; }
        string OptionalOutputFilesPath { get; set; }

        string ProjectFolderPathGUI { get; set; }

        string OptionalProjectFileName { get; set; }
        string OptionalSiteFileName { get; set; }
        string OptionalSoilFileName { get; set; }
        string OptionalManagementFileName { get; set; }
        string OptionalRunFileName { get; set; }
        string OptionalVarietalFileName { get; set; }
        string OptionalNonVarietalFileName { get; set; }
        string OptionalRunOptionFileName { get; set; }
        string ExcelFile { get; set; }

        bool isConsole { get; set; }


        //public Translator()
        //{
        //    InputFile = "./INRA.json";
        //    //SearchKeyWordExpName = "T";
        //}



        public Translator(string inputfile, string projectpath,  bool isquiet, string weapath, string outpath, string optpropath
                          , string pname, string sname, string soname, string mname, string rname, string vname, string nvname,string runopt, bool isconsole, string exFile = "")
        {
            ExcelFile = exFile;
            Filename = Path.GetFileNameWithoutExtension(inputfile);
            InputFile = inputfile;
            //SearchKeyWordExpName = skwexpname;
            isQuiet = isquiet;
            ProjectFolderPath = projectpath;
            OptionalProjectFilesPath = optpropath;
            OptionalWeatherFilesPath = weapath;
            OptionalOutputFilesPath = outpath;

            OptionalProjectFileName = pname;
            OptionalSiteFileName = sname;
            OptionalSoilFileName = soname;
            OptionalManagementFileName = mname;
            OptionalRunFileName = rname;
            OptionalVarietalFileName = vname;
            OptionalNonVarietalFileName = nvname;
            OptionalRunOptionFileName = runopt;
            isConsole = isconsole;
        }

        public Translator(string inputfile, string projectpath, string weatherpath, string outputpath, 
            string projectname, string sitefilename, string soilfilename, string managementfilename, 
            string runfilename, string varietalfilename, string nonvarietalfilename, /*string runoptionfilename,*/
            string observationfilename, bool isquiet, bool isconsole, string exFile = "")
            //string skwexpname, bool isquiet, bool isconsole)//, bool createProject)
        {
            ExcelFile = exFile;
            Filename = Path.GetFileNameWithoutExtension(inputfile);
            InputFile = inputfile;
            //SearchKeyWordExpName = skwexpname;
            isQuiet = isquiet;
            ProjectFolderPathGUI = projectpath;
            WeatherFolderPathGUI = weatherpath;
            OutputFolderPathGUI = outputpath;
            ProjectFileGUI = projectname;
            SiteFileGUI = sitefilename;
            SoilFileGUI = soilfilename;
            ManagementFileGUI = managementfilename;
            RunFileGUI = runfilename;
            VarietalFileGUI = varietalfilename;
            NonVarietalFileGUI = nonvarietalfilename;
            ObservationFileGUI = observationfilename;
            //RunOptionFileGUI = runoptionfilename;
            isConsole = isconsole;
            // CreateProject = createProject;
        }

        public void Run(bool isObs){
            
            #region Select Paths and Names

            string ProjectFileRun = "";

            if ((OptionalProjectFilesPath == null && ProjectFolderPath == null && ProjectFolderPathGUI!=null)||
                (OptionalProjectFilesPath == "" && ProjectFolderPath == "" && ProjectFolderPathGUI != "")) ProjectFileRun = ProjectFolderPathGUI;
            else if ((OptionalProjectFilesPath == null && ProjectFolderPath != null && ProjectFolderPathGUI == null)||
                (OptionalProjectFilesPath == "" && ProjectFolderPath != "" && ProjectFolderPathGUI == "")) ProjectFileRun = ProjectFolderPath + "/1-Project";
            else if ((OptionalProjectFilesPath != null && ProjectFolderPathGUI == null)||
                (OptionalProjectFilesPath != "" && ProjectFolderPathGUI == "")) ProjectFileRun = OptionalProjectFilesPath;
            else throw new Exception("Please select a project folder");

            string OutputFolderPath = "";

            if (OptionalOutputFilesPath != null && OptionalOutputFilesPath != "") OutputFolderPath = OptionalOutputFilesPath;
            else if (OutputFolderPathGUI != null && OutputFolderPathGUI != "") OutputFolderPath = OutputFolderPathGUI;
            //else OutputFolderPath = ProjectFileRun + "\\..\\3-Output";
            //else if(!isConsole) OutputFolderPath = System.IO.Directory.GetParent(System.IO.Directory.GetParent(ProjectFileRun).ToString()).ToString() + System.IO.Path.DirectorySeparatorChar + "3-Output";
            // else OutputFolderPath = System.IO.Directory.GetParent(ProjectFileRun).ToString() + System.IO.Path.DirectorySeparatorChar + "3-Output";
            else if (!isConsole) OutputFolderPath = ".." + System.IO.Path.DirectorySeparatorChar + "3-Output";
            else OutputFolderPath =  ".."+ System.IO.Path.DirectorySeparatorChar + "3-Output";

            //create Observationfolder
            string ObsFolderPath = "";
            ObsFolderPath = System.IO.Directory.GetParent(System.IO.Directory.GetParent(ProjectFileRun).ToString()).ToString() + System.IO.Path.DirectorySeparatorChar + "6-Observations";

            string VarietalFile = ""; string NonVarietalFile = ""; string RunOptionFile = "";

            if (VarietalFileGUI != null && VarietalFileGUI != "") VarietalFile = VarietalFileGUI;
            else if (OptionalVarietalFileName != null && OptionalVarietalFileName != "") VarietalFile = OptionalVarietalFileName;
            else VarietalFile = null;

            if (NonVarietalFileGUI != null && NonVarietalFileGUI != "") NonVarietalFile = NonVarietalFileGUI;
            else if (OptionalNonVarietalFileName != null && OptionalNonVarietalFileName != "") NonVarietalFile = OptionalNonVarietalFileName;
            else NonVarietalFile = null;

            //if (RunOptionFileGUI != null && RunOptionFileGUI != "") RunOptionFile = RunOptionFileGUI;
            //else if (OptionalRunOptionFileName != null && OptionalRunOptionFileName != "") RunOptionFile = OptionalRunOptionFileName;
            //else RunOptionFile = null;

            //string RunOptionFile = "";
            //if (OptionalNonVarietalFileName != null && OptionalNonVarietalFileName != "") RunOptionFile = OptionalRunOtpionFileName;
            //else RunOptionFile = null;

            string WeatherFolderPath = "";

            if (OptionalWeatherFilesPath != null && OptionalWeatherFilesPath != "") WeatherFolderPath = OptionalWeatherFilesPath;
            else if (WeatherFolderPathGUI != null && WeatherFolderPathGUI != "") WeatherFolderPath = WeatherFolderPathGUI;
            //else WeatherFolderPath = ProjectFileRun + "\\..\\2-WeatherData";
            else if (!isConsole) WeatherFolderPath =  ".."+ System.IO.Path.DirectorySeparatorChar + "2-WeatherData";
            else WeatherFolderPath =                 System.IO.Directory.GetParent(ProjectFileRun).ToString() + System.IO.Path.DirectorySeparatorChar + "2-WeatherData";
            //else if (!isConsole) WeatherFolderPath = System.IO.Directory.GetParent(System.IO.Directory.GetParent(ProjectFileRun).ToString()).ToString() + System.IO.Path.DirectorySeparatorChar + "2-WeatherData";
            //else WeatherFolderPath = System.IO.Directory.GetParent(ProjectFileRun).ToString() + System.IO.Path.DirectorySeparatorChar + "2-WeatherData";

            string ProjectFile = ""; string SiteFile = "";string SoilFile = ""; string ManagementFile = ""; string RunFile = "";
            //TR - 05/22
            string OutputFormatFile = ""; string EnvirotypingFile = ""; string ModelConfigurationFile = ""; string ObservationFile = "";

            if (ProjectFileGUI != null && ProjectFileGUI != "") ProjectFile = ProjectFileGUI;
            else if (OptionalProjectFileName != null && OptionalProjectFileName != "") ProjectFile = OptionalProjectFileName;
            else ProjectFile = Filename + "_Project";

            if (SiteFileGUI != null && SiteFileGUI != "") SiteFile = SiteFileGUI;
            else if (OptionalSiteFileName != null && OptionalSiteFileName != "") SiteFile = OptionalSiteFileName;
            else SiteFile = Filename + "_Site"; 

            if (SoilFileGUI != null && SoilFileGUI != "") SoilFile = SoilFileGUI;
            else if (OptionalSoilFileName != null && OptionalSoilFileName != "") SoilFile = OptionalSoilFileName;
            else SoilFile = Filename + "_Soil";

            if (ManagementFileGUI != null && ManagementFileGUI != "") ManagementFile = ManagementFileGUI;
            else if (OptionalManagementFileName != null && OptionalManagementFileName != "") ManagementFile = OptionalManagementFileName;
            else ManagementFile = Filename + "_Management"; 

            if (RunFileGUI != null && RunFileGUI != "") RunFile = RunFileGUI;
            else if (OptionalRunFileName != null && OptionalRunFileName != "") RunFile = OptionalRunFileName;
            else RunFile = Filename + "_Run";

            /*************************************************************
             * **********************************************************/
            //TR - 05/22
            if (OutputFormatFileGUI != null && OutputFormatFileGUI != "") OutputFormatFile = OutputFormatFileGUI;
            else OutputFormatFile = Filename + "_OutputFormat";

            if (EnvirotypingFileGUI != null && EnvirotypingFileGUI != "") EnvirotypingFile = EnvirotypingFileGUI;
            else EnvirotypingFile = Filename + "_Envirotyping";

            if (ModelConfigurationFileGUI != null && ModelConfigurationFileGUI != "") ModelConfigurationFile = ModelConfigurationFileGUI;
            else ModelConfigurationFile = Filename + "_ModelConfiguration";

            if (ObservationFileGUI != null && ObservationFileGUI != "") ObservationFile = ObservationFileGUI;
            else ObservationFile = Filename + "_Observation"; ;
            /*************************************************************
             * **********************************************************/


            #endregion

            //TR - 06/22
            ImportDBM idbm = new ImportDBM(File.ReadAllText(InputFile), ProjectFileRun, WeatherFolderPath,
                OutputFolderPath, ObsFolderPath, ProjectFile, SiteFile,
                SoilFile, ManagementFile, RunFile, VarietalFile, NonVarietalFile, RunOptionFile, OutputFormatFile, EnvirotypingFile, ModelConfigurationFile, ObservationFile);//, CreateProject);

            idbm.importDBM(isQuiet,ExcelFile,isObs);
           // idbm.importDBM(SearchKeyWordExpName, isQuiet);
            if (!isQuiet) Console.WriteLine("End of Translation");
           
        }

    }
}
