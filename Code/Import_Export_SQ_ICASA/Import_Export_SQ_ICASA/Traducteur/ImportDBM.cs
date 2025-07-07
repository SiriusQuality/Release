using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;
using Import_Export_SQ_ICASA.Cultivations;
using System.Xml;
using Import_Export_SQ_ICASA.Traducteur.Cultivations;
using OfficeOpenXml;

namespace Import_Export_SQ_ICASA
{
    class ImportDBM
    {
        //Inorder to have a better comprehention of the code, 
        //I recommend to have an eye on diagramms while your investigate the code.

        //If the values are not found in the json it is replaced by its default value in SiriusQuality

        /// <summary>
        /// Observation objects
        /// </summary>
        List<Observation> obs;
        /// <summary>
        /// Cultivation objects
        /// </summary>
        List<Cultivation> clt;

        //OBJECT ATTRIBUTES

        /// <summary>
        /// Imported json
        /// </summary>
        public String json;

        /// <summary>
        /// Project folder
        /// </summary>
        //public String DIR = "./";
        public String ProjectDIR { get; set; }

        /// <summary>
        /// Weather folder
        /// </summary>
        public String WeatherDIR { get; set; }

        /// <summary>
        /// Output folder for runs
        /// </summary>
        public String OutputDIR { get; set; }

        /// <summary>
        /// Observation folder 
        /// </summary>
        public String ObservationsDIR { get; set; }

        /// <summary>
        /// Filename of the sqsit file
        /// </summary>
        public String ProjectFilename { get; set; }

        /// <summary>
        /// Filename of the sqsit file
        /// </summary>
        public String SiteFilename { get; set; }

        /// <summary>
        /// Filename of the sqsoi file
        /// </summary>
        public String SoilFilename { get; set; }

        /// <summary>
        /// Filename of the sqman file
        /// </summary>
        public String ManagementFilename { get; set; }

        /// <summary>
        /// Filename of the sqrun file
        /// </summary>
        public String RunFilename { get; set; }

        /// <summary>
        /// Filename of the sqvar file
        /// </summary>
        public String VarietalFilename { get; set; }

        /// <summary>
        /// Filename of the sqpar file
        /// </summary>
        public String NonVarietalFilename { get; set; }

        /// <summary>
        /// Filename of the sqopt file
        /// </summary>
        public String RunOptionFilename { get; set; }

        //TR - 05/22
        /// <summary>
        /// Filename of the sqopt file
        /// </summary>
        public String OutputFormatFilename { get; set; }

        /// <summary>
        /// Filename of the sqenv file
        /// </summary>
        public String EnvirotypingFileName { get; set; }

        /// <summary>
        /// Filename of the sqmcf file
        /// </summary>
        public String ModelConfigurationFileName { get; set; }

        /// <summary>
        /// Filename of the sqobs file
        /// </summary>
        public String ObservationFileName { get; set; }

        /// <summary>
        /// Choose if project file (.sqpro) is created (by default is true for GUI translation)
        /// </summary>
        //bool CreateProject { get; set; }

        // USER FUNCTIONS

        //TR - 05/22
        /// <summary>
        ///     Initialize all object observation and cultivation objects and register the json,
        ///     which avoids to give the json to each initialized objects for treatment.
        /// </summary>
        /// <param name="json"></param>
        public ImportDBM(String json, String projectdir, String weatherdir, String outputdir, String observationsdir, String project, String site, String soil,
            String management, String run, String varietal, String nonvarietal, String runopt, String outputformat, String enviro, String modconfig, String observation)//, bool createProject)
        {

            ProjectDIR = projectdir;
            WeatherDIR = weatherdir;
            OutputDIR = outputdir;
            ObservationsDIR = observationsdir;
            ProjectFilename = project;
            SiteFilename = site;
            SoilFilename = soil;
            ManagementFilename = management;
            RunFilename = run;
            VarietalFilename = varietal;
            NonVarietalFilename = nonvarietal;
            RunOptionFilename = runopt;
            //OutputFormatFilename = outputformat;
            //EnvirotypingFileName = enviro;
            //ModelConfigurationFileName = modconfig;
            //ObservationFileName = observation;
            //CreateProject = createProject;

            //if (CreateProject) this.CreateProjectFile();
            //this.CreateProjectFile();

            this.json = json;
            clt = new List<Cultivation>();
            clt.Add(new Run(this));
            clt.Add(new Site(this));
            clt.Add(new Soil(this));
            clt.Add(new Management(this));
            //clt.Add(new Observ(this));
            //clt.Add(new Observation(this));
            obs = new List<Observation>();
            //obs.Add(new SQOCAN(this));
            //obs.Add(new SQMAT(this));

        }

        /// <summary>
        /// This function calls all run fuctions and write the sended data by those 
        /// functions on the right SiriusQuality folder format.
        /// </summary>
        public void importDBM(bool isQuiet, string Filename,bool conObs=false)
        //    public void importDBM(String str, bool isQuiet)
        {
            if (!Directory.Exists(this.ProjectDIR +"\\"+ "..\\" + "6-Observations"))
            {
                Directory.CreateDirectory(this.ProjectDIR + "\\" + "..\\" + "6-Observations");
            }
            string[] paths =null;
            if(conObs) paths = ConvertObservationToText(Filename, this.ProjectDIR + "\\" + "..\\" + "6-Observations\\");
            //sdueri
            this.CreateProjectFile();
            //sdueri
            clt.ForEach(e => e.run(isQuiet));
            Observ ob = new Observ(this);
            clt.Add(ob);
            if (conObs) ob.run(isQuiet, paths);

            //clt.ForEach(e => e.run(str, isQuiet));
            //obs.ForEach(e=>e.run(isQuiet));
        }

        public static string[] ConvertObservationToText(string excelFilePath, string outputDirectory)
        {
            string[] paths = new string[5] {"?", "?", "?", "?", "?" };
            if (!Directory.Exists(outputDirectory)) Directory.CreateDirectory(outputDirectory);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            // Load Excel file using EPPlus
            using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                // Loop through each sheet in the workbook
                foreach (var sheet in package.Workbook.Worksheets)
                {
                    if (sheet.Name.StartsWith("obs", StringComparison.InvariantCultureIgnoreCase) && sheet.Name.Contains("mean"))
                    {
                        // Create new text file with the same name as the sheet
                        string outputPath = "";
                        if (sheet.Name.Contains("summary"))
                        {
                            outputPath = Path.Combine(outputDirectory, $"{sheet.Name}.sqosum");
                            paths[0] = outputPath;
                        }
                        else if (sheet.Name.Contains("daily")) 
                        {
                            outputPath = Path.Combine(outputDirectory, $"{sheet.Name}.sqocan");
                            paths[1] = outputPath;
                        }
                        else if (sheet.Name.Contains("canopy")) 
                        {
                            outputPath = Path.Combine(outputDirectory, $"{sheet.Name}.sqocl");
                            paths[2] = outputPath;
                        }
                        else if (sheet.Name.Contains("soil")) 
                        {
                            outputPath = Path.Combine(outputDirectory, $"{sheet.Name}.sqosl");
                            paths[3] = outputPath;
                        }
                        else if (sheet.Name.Contains("root")) 
                        {
                            outputPath = Path.Combine(outputDirectory, $"{sheet.Name}.sqorl");
                            paths[4] = outputPath;
                        }
                        else continue;

                        List<int> columnDate = new List<int>();
                        using (var writer = new StreamWriter(outputPath))
                        {
                            // Loop through each row in the sheet
                            for (int row = 1; row <= sheet.Dimension.End.Row; row++)
                            {
                                if (sheet.Cells[row, 1].Value == null) break;
                                // Loop through each column in the sheet
                                for (int col = 1; col <= sheet.Dimension.End.Column; col++)
                                {
                                    var cellValue = sheet.Cells[row, col].Value;

                                    // Write cell value to text file
                                    if (cellValue == null) writer.Write("na");
                                    else if (cellValue.ToString() == "date")
                                    {
                                        writer.Write("yyyy-mm-dd");
                                        columnDate.Add(col);
                                    }
                                    else if (columnDate.Contains(col) &&
                                        cellValue is double numericValue && DateTime.FromOADate(numericValue) is DateTime dateValue) writer.Write(dateValue.ToString("yyyy-MM-dd"));
                                    else writer.Write(cellValue);

                                    // Separate columns with a tab character
                                    if (col < sheet.Dimension.End.Column)
                                    {
                                        writer.Write('\t');
                                    }
                                }
                                // Move to next line
                                writer.WriteLine();
                            }
                        }
                    }
                }
            }
            return paths;
        }

        /// <summary>
        /// This function writes the project file .sqpro 
        /// 
        /// </summary>
        public void CreateProjectFile()
        {
            //String  filename = Path.GetFileNameWithoutExtension(json);
            String nonvarietal = (File.Exists(NonVarietalFilename)) ? Path.GetFileName(NonVarietalFilename) : "NonVarietal.sqpar";
            String varietal = (File.Exists(VarietalFilename)) ? Path.GetFileName(VarietalFilename) : "Varietal.sqvar";
            //if (ManagementFilename == null) { ManagementFilename = "tr_management"; }
            //if (SiteFilename == null) { SiteFilename = "tr_site"; }
            //if (SoilFilename == null) { SoilFilename = "tr_soil"; }
            //if (RunFilename == null) { RunFilename = "tr_run"; }
            //if (ProjectFilename == null) { ProjectFilename = "tr_project"; }
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            XmlWriter writer = XmlWriter.Create(ProjectDIR + ProjectFilename + ".sqpro", settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("ProjectFile");
            writer.WriteStartElement("Inputs");
            writer.WriteElementString("OptimizationFileName", "?");
            writer.WriteElementString("ObservationFileName", ObservationFileName + ".sqobs");
            writer.WriteElementString("ManagementFileName", ManagementFilename + ".sqman");
            writer.WriteElementString("NonVarietyFileName", nonvarietal);
            writer.WriteElementString("MaizeNonVarietyFileName", "?");
            writer.WriteElementString("RunOptionFileName", "Run_options.sqopt");
            //TR - 05/22
            writer.WriteElementString("OutputFormatFileName", OutputFormatFilename + ".sqout");
            writer.WriteElementString("EnvirotypingFileName", EnvirotypingFileName + ".sqenv");
            writer.WriteElementString("ModelConfigurationFileName", ModelConfigurationFileName + ".sqmcf");
            writer.WriteElementString("SiteFileName", SiteFilename + ".sqsit");
            writer.WriteElementString("SoilFileName", SoilFilename + ".sqsoi");
            writer.WriteElementString("VarietyFileName", varietal);
            writer.WriteElementString("MaizeVarietyFileName", "?");
            writer.WriteElementString("RunFileName", RunFilename + ".sqrun");
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
            
            
        }
        // UTILITY FUNCTIONS


        /// <summary>
        /// Allows to get the JObject from the json attribute
        /// </summary>
        /// <returns>JObject of the parsed json.</returns>
        public JObject importJson()
        {
            return JObject.Parse(json);
        }


        /// <summary>
        /// Search for an object with given a property (attribut:exname, soil_id...) and value (name)
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Experiment/Soil JObject</returns>
        //public JObject searchObjectByName(String name)
        //{
        //    return Export.searchObjectKeyValue("exname", name, json);
        //}
        public JObject searchObjectByName(String attribut, String name)
        {
            return Export.searchObjectKeyValue(attribut, name, json);
        }

        /// <summary>
        /// This function stores all the experiment name in an array.
        /// It uses searchValue function with the getExperimentIdentifier() as parameter and parse the csv returned string
        /// inorder to get a string array.
        /// </summary>
        /// <returns>Names of all experiments in a String array</returns>
        public String[] getAllNames()
        {
            String name = getExperimentIdentifier();

            //sdueri
            List<string> values = new List<string> { };
            JObject obj = JObject.Parse(json);
            JArray exp = (JArray)obj["experiments"];
            var t= exp.Children();

            foreach (JToken i in t)
            {
                
                {
                    String value = i[name].Value<String>();
                    values.Add(value);
                }
            }

            return values.ToArray();


            //   return Export.searchValue(name, json).Split(';');
            //sdueri
        }

        /// <summary>
        /// This function stores all the treatment (management) name in an array.
        /// It uses searchValue function with the getManagementIdentifier as parameter and parse the csv returned string
        /// inorder to get a string array.
        /// </summary>
        /// <returns>Names of all experiments in a String array</returns>
        public String[] getAllTreatNames()
        {
            String name = getManagementIdentifier();

            //sdueri
            List<string> values = new List<string> { };
            JObject obj = JObject.Parse(json);
            JArray exp = (JArray)obj["experiments"];
            var t = exp.Children();

            foreach (JToken i in t)
            {

                {
                    String value = i[name].Value<String>();
                    values.Add(value);
                }
            }

            return values.ToArray();


            //   return Export.searchValue(name, json).Split(';');
            //sdueri
        }
        /// <summary>
        /// This function finds the right identifier for the experiment in the json file. 
        /// The result can be either "id" (PHIS), "field_id" (old PHIS) or "trt_name" or "fl_name" (DBM)
        /// </summary>
        /// <returns>Names of all experiments in a String array</returns>
        public String getExperimentIdentifier()
        {

            if (Export.searchValue("id", json).Length > 0)
            {
                return "id";
            }
            else if (Export.searchValue("plot_id", json).Length > 0)  // identifier of the PHIS json
            {
                return "plot_id";
            }
            else if (Export.searchValue("field_id", json).Length > 0)  //old identifier of the PHIS json
            {
                return "field_id";
            }
            
            else if (Export.searchValue("fl_name", json).Length > 0)
            {
                return "fl_name";
            }
            else if (Export.searchValue("trt_name", json).Length > 0) // in this case trt_name will be the identifier of the manangement and of the experiment
            {
                return "trt_name";
            }
            else
            { throw new ArgumentException("Cannot translate: need a definition of the experiment id or field name in the json file"); }
        }

        /// <summary>
        /// This function finds the right identifier for the management in the json file. 
        /// The result can be either "field_id" (old PHIS) or "trt_name" or "fl_name" (DBM)
        /// </summary>
        /// <returns>Names of all experiments in a String array</returns>
        public String getManagementIdentifier()
        {


            if (Export.searchValue("trt_name", json).Length > 0)  //PHIS json
            {
                return "trt_name";
            }
            else if (Export.searchValue("field_id", json).Length > 0)  //old identifier of the PHIS json
            {
                return "field_id";
            }

            else if (Export.searchValue("fl_name", json).Length > 0)
            {
                return "fl_name";
            }
            else
            { throw new ArgumentException("Cannot translate: need a definition of the treatment or field name in the json file"); }
        }

        /// <summary>
        /// This function finds the right identifier for the field capacity in the json file. 
        /// The result can be either "slfc1" (DBM) or "sldul" 
        /// </summary>
        /// <returns>Names of all experiments in a String array</returns>
        public String getFieldCapacityIdentifier()
        {
            if (Export.searchValue("slfc1", json).Length > 0)
            {
                return "slfc1";
            }
            else if (Export.searchValue("sldul", json).Length > 0)
            {
                return "sldul";
            }
            else
            { throw new ArgumentException("Cannot translate: need a definition of the field capacity"); }
        }

        /// <summary>
        /// This function finds the right identifier for the permanent wilting point in the json file. 
        /// The result can be either "slwp" (DBM) or "slll" 
        /// </summary>
        /// <returns>Names of all experiments in a String array</returns>
        public String getPWPIdentifier()
        {
            if (Export.searchValue("slwp", json).Length > 0)
            {
                return "slwp";
            }
            else if (Export.searchValue("slll", json).Length > 0)
            {
                return "slll";
            }
            else
            { throw new ArgumentException("Cannot translate: need a definition of the permanent wilting point"); }
        }

        /// <summary>
        /// This function finds the right identifier for the bulk density in the json file. 
        /// The result can be either "slbdm"  or "sabdm" 
        /// </summary>
        /// <returns>Names of all experiments in a String array</returns>
        public String getBulkDensityIdentifier()
        {
            if (Export.searchValue("slbdm", json).Length > 0)
            {
                return "slbdm";
            }
            else if (Export.searchValue("sabdm", json).Length > 0)
            {
                return "sabdm";
            }
            else
            { return ""; }
        }

        /// <summary>
        /// This function finds the right identifier for the pH in the json file. 
        /// The result can be either "slhw"  or "slphw" 
        /// </summary>
        /// <returns>Names of all experiments in a String array</returns>
        public String getpHIdentifier()
        {
            if (Export.searchValue("slphw", json).Length > 0)
            {
                return "slphw";
            }
            else if (Export.searchValue("slhw", json).Length > 0)
            {
                return "slhw";
            }
            else
            { return "unavailable"; }
        }

        /// <summary>
        /// This function stores all weather station id of the weathers Jarray in an array.
        /// It uses searchValue function with "wst_id" as parameter and parse the csv returned string
        /// inorder to get a string array.
        /// </summary>
        /// <returns>Names of all site in a String array</returns>
        public String[] getWSTNames()
        {//sdueri
            List<string> values = new List<string> { };
            JObject obj = JObject.Parse(json);
            JArray weathers = (JArray)obj["weathers"];
            var t = weathers.Children();

            foreach (JToken i in t)
            {

                {
                    String value = i["wst_id"].Value<String>();
                    values.Add(value);
                }
            }

            return values.ToArray();


            //   return Export.searchValue("wst_id", JObject.Parse(json)["weathers"].ToString()).Split(';');
            //sdueri
            //
        }

        /// <summary>
        /// This function stores all soil id of the soils Jarray in an array.
        /// It uses searchValue function with "sid" as parameter and parse the csv returned string
        /// inorder to get a string array.
        /// </summary>
        /// <returns>Names of all site in a String array</returns>
        public String[] getSoilNames()
        {
            if (Export.searchValue("soil_id", JObject.Parse(json)["soils"].ToString()).Split(';').Length > 1)
            {
                return Export.searchValue("soil_id", JObject.Parse(json)["soils"].ToString()).Split(';');
            }
            else
            {
                return Export.searchValue("sid", JObject.Parse(json)["soils"].ToString()).Split(';');
            }

        }
       
    }
}
