using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiriusModel.InOut.Base;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;

namespace SiriusModel.InOut
{
    ///<summary>
    ///Observation Item
    ///</summary>
    [Serializable]
    public class ObservationItem : ProjectDataFileItemNoChild, IProjectItem
    {
        [XmlIgnore]
        private List<string> observationList;
        [XmlIgnore]
        public List<string> ObservationList
        {
            get { return observationList; }
            set { this.SetObject(ref observationList, ref value, "ObservationList"); }
        }

        private Dictionary<string, DataTable> observationDictionary;

        [XmlIgnore]
        public Dictionary<string, DataTable> ObservationDictionary
        {
            get { return observationDictionary; }
            set { this.SetObject(ref observationDictionary, ref value, "ObservationDictionary"); }
        }

        #region Managements, Sites, soils, Varieties of the current optimization (in the text file)
        private List<string> optimizationManagements = new List<string>();
        private List<string> optimizationSites = new List<string>();
        private List<string> optimizationSoils = new List<string>();
        private List<string> optimizationVarieties = new List<string>();

        [XmlIgnore]
        public List<string> OptimizationManagements
        {
            get { return optimizationManagements; }
            set { optimizationManagements = value; }
        }
        [XmlIgnore]
        public List<string> OptimizationSites
        {
            get { return optimizationSites; }
            set { optimizationSites = value; }
        }
        [XmlIgnore]
        public List<string> OptimizationSoils
        {
            get { return optimizationSoils; }
            set { optimizationSoils = value; }
        }
        [XmlIgnore]
        public List<string> OptimizationVarieties
        {
            get { return optimizationVarieties; }
            set { optimizationVarieties = value; }
        }
        #endregion

        #region Observations files
        private string canopyObservationFile;
        private string phytomerObservationFile;
        private string soilObservationFile;
        private string phenologyObservationFile;
        private string haunIndexObservationFile;

        public string CanopyObservationFile
        {
            get { return canopyObservationFile; }
            set { this.SetObject(ref canopyObservationFile, ref value, "CanopyObservationFile"); }
        }
        public string PhytomerObservationFile
        {
            get { return phytomerObservationFile; }
            set { this.SetObject(ref phytomerObservationFile, ref value, "PhytomerObservationFile"); }
        }
        public string SoilObservationFile
        {
            get { return soilObservationFile; }
            set { this.SetObject(ref soilObservationFile, ref value, "SoilObservationFile"); }
        }
        public string PhenologyObservationFile
        {
            get { return phenologyObservationFile; }
            set { this.SetObject(ref phenologyObservationFile, ref value, "PhenologyObservationFile"); }
        }
        public string HaunIndexObservationFile
        {
            get { return haunIndexObservationFile; }
            set { this.SetObject(ref haunIndexObservationFile, ref value, "HaunIndexObservationFile"); }
        }
        #endregion

        #region Observations files extensions
        private string canopyObservationExtension = ".sqcan";
        private string phytomerObservationExtension = ".sqphy";
        private string soilObservationExtension = ".sqos";
        private string phenologyObservationExtension = ".sqmat";
        private string haunIndexObservationExtension = ".sqoln";

        [XmlIgnore]
        public string CanopyObservationExtension
        {
            get { return canopyObservationExtension; }
            set { this.SetObject(ref canopyObservationExtension, ref value, "CanopyObservationExtension"); }
        }
        [XmlIgnore]
        public string PhytomerObservationExtension
        {
            get { return phytomerObservationExtension; }
            set { this.SetObject(ref phytomerObservationExtension, ref value, "PhytomerObservationExtension"); }
        }
        [XmlIgnore]
        public string SoilObservationExtension
        {
            get { return soilObservationExtension; }
            set { this.SetObject(ref soilObservationExtension, ref value, "SoilObservationExtension"); }
        }
        [XmlIgnore]
        public string PhenologyObservationExtension
        {
            get { return phenologyObservationExtension; }
            set { this.SetObject(ref phenologyObservationExtension, ref value, "PhenologyObservationExtension"); }
        }
        [XmlIgnore]
        public string HaunIndexObservationExtension
        {
            get { return haunIndexObservationExtension; }
            set { this.SetObject(ref haunIndexObservationExtension, ref value, "HaunIndexObservationExtension"); }
        }
        #endregion

        #region Observations Tables
        private bool canopyTableChecked = false;
        private bool phytomerTableChecked = false;
        private bool soilTableChecked = false;
        private bool phenologyTableChecked = false;
        private bool haunIndexTableChecked = false;

        [XmlIgnore]
        public bool CanopyTableChecked
        {
            get { return canopyTableChecked; }
            set { this.SetStruct(ref canopyTableChecked, ref value, "CanopyTableChecked"); }
        }
        [XmlIgnore]
        public bool PhytomerTableChecked
        {
            get { return phytomerTableChecked; }
            set { this.SetStruct(ref phytomerTableChecked, ref value, "PhytomerTableChecked"); }
        }
        [XmlIgnore]
        public bool SoilTableChecked
        {
            get { return soilTableChecked; }
            set { this.SetStruct(ref soilTableChecked, ref value, "SoilTableChecked"); }
        }
        [XmlIgnore]
        public bool PhenologyTableChecked
        {
            get { return phenologyTableChecked; }
            set { this.SetStruct(ref phenologyTableChecked, ref value, "PhenologyTableChecked"); }
        }
        [XmlIgnore]
        public bool HaunIndexTableChecked
        {
            get { return haunIndexTableChecked; }
            set { this.SetStruct(ref haunIndexTableChecked, ref value, "HaunIndexTableChecked"); }
        }

        private DataSet observationsDataSet = new DataSet("Observations");
        private DataTable canopyObservationTable = new DataTable("Canopy");
        private DataTable phytomerObservationTable = new DataTable("Phytomer");
        private DataTable soilObservationTable = new DataTable("Soil");
        private DataTable phenologyObservationTable = new DataTable("Phenology");
        private DataTable haunIndexObservationTable = new DataTable("Haun Index");

        [XmlIgnore]
        public DataSet ObservationsDataSet
        {
            get { return observationsDataSet; }
            set { this.SetObject(ref observationsDataSet, ref value, "ObservationsDataSet"); }
        }
        [XmlIgnore]
        public DataTable CanopyObservationTable
        {
            get { return canopyObservationTable; }
            set { this.SetObject(ref canopyObservationTable, ref value, "CanopyObservationTable"); }
        }
        [XmlIgnore]
        public DataTable PhytomerObservationTable
        {
            get { return phytomerObservationTable; }
            set { this.SetObject(ref phytomerObservationTable, ref value, "PhytomerObservationTable"); }
        }
        [XmlIgnore]
        public DataTable SoilObservationTable
        {
            get { return soilObservationTable; }
            set { this.SetObject(ref soilObservationTable, ref value, "SoilObservationTable"); }
        }
        [XmlIgnore]
        public DataTable PhenologyObservationTable
        {
            get { return phenologyObservationTable; }
            set { this.SetObject(ref phenologyObservationTable, ref value, "PhenologyObservationTable"); }
        }
        [XmlIgnore]
        public DataTable HaunIndexObservationTable
        {
            get { return haunIndexObservationTable; }
            set { this.SetObject(ref haunIndexObservationTable, ref value, "HaunIndexObservationTable"); }
        }
        #endregion

        private string[] dateFormats; 

        public ObservationItem(string name)
            : base(name)
        {
            CanopyObservationFile = "?";
            PhytomerObservationFile = "?";
            SoilObservationFile = "?";
            PhenologyObservationFile = "?";
            HaunIndexObservationFile = "?";

            ObservationsDataSet.Tables.Add(CanopyObservationTable);
            ObservationsDataSet.Tables.Add(PhytomerObservationTable);
            ObservationsDataSet.Tables.Add(SoilObservationTable);
            ObservationsDataSet.Tables.Add(PhenologyObservationTable);
            ObservationsDataSet.Tables.Add(HaunIndexObservationTable);

                // Link between observations and data tables 
            this.ObservationDictionary = new Dictionary<string, DataTable>();

            dateFormats = new string[] { "yyyy-MM-dd","dd/MM/yyyy" };
        }

        public ObservationItem()
            : this("")
        {

        }

        void BindingItemsListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemDeleted || (e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor != null))
                NotifyPropertyChanged(e.PropertyDescriptor.Name);
        }

        public override void UpdatePath(string oldAbsolute, string newAbsolute)
        {
        }

        public override void CheckWarnings()
        {
        }

        public double getObservation(string name, string management, DateTime managementSowingDate, string variety, string soil, string site, List<Tuple<DateTime, List<double>>> getObsResult)
        {
            //this.updateObservationDictionary(); 

                // Select the table in which to look
            DataTable ObservationTable = this.ObservationDictionary[name];
            if (ObservationTable.Rows.Count == 0) { throw new Exception("the Observation table is empty, did you forget to load the observations ?"); }
            bool findRow = false;
            DataRow current = ObservationTable.Rows[0];
            int i ;
            int index = -1;
            //find the first row with the header
            bool findHeader = false;
            DataRow header = ObservationTable.Rows[0];
            int k = 0;
            while (!findHeader && k < ObservationTable.Rows.Count)
            {
                header = ObservationTable.Rows[k++];

                if (header[0].ToString() == "Management" && header[1].ToString() == "Site" && header[2].ToString() == "Soil")
                    findHeader = true;
            }
            if (!findHeader) { return -999; }



            //find the column with the data
            bool findColumn = false;
            int j = 4;
            while (!findColumn && j < ObservationTable.Columns.Count)
            {
                j++;
                findColumn = (header[j].ToString() == name);
            }

            if (!findColumn) { return -999; }
            else
            {
                index = j;
            }


            //find the rows with the data
            i = k;//start below the header
            while (i < ObservationTable.Rows.Count)
            {
                current = ObservationTable.Rows[i++];
                if (current[0].ToString() == management)
                {
                    DateTime obsFileSowingDate = new DateTime();
                    DateTime.TryParseExact((string)current[3], dateFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out obsFileSowingDate);

                    if (current[1].ToString() == site && current[2].ToString() == soil && obsFileSowingDate.Year == managementSowingDate.Year && (ObservationTable == this.SoilObservationTable || current[4].ToString() == variety))
                    {
                        findRow = true;
                        DateTime dateRes;
                        // process the index (double format or date format)
                        switch (name)
                        {
                            //recover the value and the corresponding date
                            case "Leaf area index":
                            case "Green area index":
                            case "Crop dry mass":
                            case "Leaf dry mass":
                            case "Leaf nitrogen":
                            case "Laminae dry mass":
                            case "Laminae nitrogen":
                            case "Specific leaf nitrogen":
                            case "Specific leaf dry mass":
                            case "Stem dry mass":
                            case "Stem nitrogen":
                            case "True stem dry mass":
                            case "True stem nitrogen":
                            case "Stem length":
                            case "Grain dry mass":
                            case "Crop nitrogen":
                            case "Grain nitrogen":
                            case "Single grain dry mass":
                            case "Maturity single grain dry mass":
                            case "Single grain nitrogen":
                            case "Starch per grain":
                            case "Albumins-globulins per grain":
                            case "Amphiphils per grain":
                            case "Gliadins per grain":
                            case "Glutenins per grain":
                            case "Final leaf number":
                            case "Grain number":
                            case "Grain protein concentration":                            
                            case "Post-anthesis crop n uptake (kgn/ha)":
                            case "Emerged leaf number":
                                try
                                {
                                    if (current[5].Equals("-999")) //no date 
                                    {
                                        dateRes = new DateTime();
                                    }
                                    else
                                    {
                                        DateTime.TryParseExact((string)current[5], dateFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateRes);
                                    }
                                    List<double> obs = new List<double>();
                                    obs.Add(Convert.ToDouble(current[index]));
                                    Tuple<DateTime, List<double>> result = new Tuple<DateTime, List<double>>(dateRes, obs);
                                    getObsResult.Add(result);

                                }
                                catch (Exception)
                                {
                                    if (current[5].Equals("-999")) //no date 
                                    {
                                        dateRes = new DateTime();
                                    }
                                    else
                                    {
                                        //replace . with ,
                                        string[] split = current[index].ToString().Split('.');
                                        string temp = split[0] + "," + split[1];

                                        DateTime.TryParseExact((string)current[5], dateFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateRes);
                                    }
                                    List<double> obs = new List<double>();
                                    obs.Add(Convert.ToDouble(current[index]));
                                    Tuple<DateTime, List<double>> result = new Tuple<DateTime, List<double>>(dateRes, obs);
                                    getObsResult.Add(result);
                                }
                                break;
                            //soil file obs (the soil file has a different structure (no variety and the obs depend on toplayer and bottomlayer)
                            case "Soil mineral N":
                            case "Soil water":
                                try
                                {
                                    if (current[4].Equals("-999")) //no date 
                                    {
                                        dateRes = new DateTime();
                                    }
                                    else
                                    {
                                        DateTime.TryParseExact((string)current[4], dateFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateRes);
                                    }
                                    List<double> obs = new List<double>();
                                    obs.Add(Convert.ToDouble(current[index]));
                                    obs.Add(Convert.ToDouble(current[5]));//top layer
                                    obs.Add(Convert.ToDouble(current[6]));//bottom layer
                                    Tuple<DateTime, List<double>> result = new Tuple<DateTime, List<double>>(dateRes, obs);
                                    getObsResult.Add(result);

                                }
                                catch (Exception)
                                {
                                    if (current[4].Equals("-999")) //no date 
                                    {
                                        dateRes = new DateTime();
                                    }
                                    else
                                    {
                                        //replace . with ,
                                        string[] split = current[index].ToString().Split('.');
                                        string temp = split[0] + "," + split[1];

                                        DateTime.TryParseExact((string)current[4], dateFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateRes);
                                    }
                                    List<double> obs = new List<double>();
                                    obs.Add(Convert.ToDouble(current[index]));
                                    Tuple<DateTime, List<double>> result = new Tuple<DateTime, List<double>>(dateRes, obs);
                                    getObsResult.Add(result);
                                }
                                break;
                            //only recover the value
                            case "Anthesis leaf area index":
                            case "Anthesis green area index":
                            case "Anthesis crop dry mass":
                            case "Maturity crop dry mass":
                            case "Anthesis leaf dry mass":
                            case "Maturity leaf dry mass":
                            case "Anthesis leaf nitrogen":
                            case "Maturity leaf nitrogen":
                            case "Anthesis laminae dry mass":
                            case "Maturity laminae dry mass":
                            case "Anthesis laminae nitrogen":
                            case "Maturity laminae nitrogen":
                            case "Anthesis specific leaf nitrogen":
                            case "Anthesis specific leaf dry mass":
                            case "Anthesis stem dry mass":
                            case "Maturity stem dry mass":
                            case "Anthesis stem nitrogen":
                            case "Maturity stem nitrogen":
                            case "Anthesis true stem dry mass":
                            case "Maturity true stem dry mass":
                            case "Anthesis true stem nitrogen":
                            case "Maturity true stem nitrogen":
                            case "Anthesis stem length":
                            case "Anthesis ear dry mass":
                            case "Anthesis crop nitrogen":
                            case "Maturity crop nitrogen":
                            case "Maturity grain nitrogen":
                            case "Maturity single grain nitrogen":
                            case "Maturity starch per grain":
                            case "Maturity grain starch concentration":
                            case "Maturity albumins-globulins per grain":
                            case "Maturity amphiphils per grain":
                            case "Maturity gliadins per grain":
                            case "Maturity glutenins per grain":
                            case "Maturity shoot number":
                            case "Maturity grain yield":
                            case "% gliadins at maturity (% of total grain n)":
                            case "% gluteins at maturity (% of total grain n)":
                            case "Gliadins-to-gluteins ratio (dimensionless)":
                            case "DM harvest index":
                            case "N harvest index":
                            case "Cumulative available soil nitrogen":
                            case "N leaching (kgn/ha)":
                            case "Cumulative water drainage (mm)":
                            case "N use efficiency (kgdm/kgn)":
                            case "N utilisation efficiency (kgdm/kgn)":
                            case "N uptake efficiency (kgN/kgn)":
                            case "Cumulative N mineralisation (kgN/ha) in soil profil":
                            case "Cumulative N denitrification in soil profil (kgn/ha)":
                            case "Available mineral soil N at maturity in soil profil (kgn/ha)":
                            case "Total mineral soil N at maturity in soil profil (kgn/ha)":
                            case "Available water at maturity in soil profil (mm)":

                                // Recover the values and the dates at emergence 
                                try
                                {
                                    dateRes = new DateTime();
                                    List<double> obs = new List<double>();
                                    obs.Add(Convert.ToDouble(current[index]));
                                    Tuple<DateTime, List<double>> result = new Tuple<DateTime, List<double>>(dateRes, obs);
                                    getObsResult.Add(result);
                                    return 1;//there will never be multiple observations for those variables
                                }
                                catch (Exception)
                                {

                                    dateRes = new DateTime();
                                    //replace . with ,
                                    string[] split = current[index].ToString().Split('.');
                                    string temp = split[0] + "," + split[1];
                                    List<double> obs = new List<double>();
                                    obs.Add(Convert.ToDouble(current[index]));
                                    Tuple<DateTime, List<double>> result = new Tuple<DateTime, List<double>>(dateRes, obs);
                                    getObsResult.Add(result);
                                    return 1;//there will never be multiple observations for those variables
                                }
                                break;
                            // the value is a date 
                            case "ZC10_Emergence":
                            case "ZC65_Anthesis":
                            case "ZC92_Maturity":
                            case "ZC55_Heading":
                            case "ZC91_End of grain filling":


                                if (DateTime.TryParseExact((string)current[index], dateFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateRes))
                                {
                                    List<double> obs = new List<double>();
                                    obs.Add(dateRes.ToOADate());
                                    Tuple<DateTime, List<double>> result = new Tuple<DateTime, List<double>>(dateRes, obs);
                                    getObsResult.Add(result);
                                    return 1;//there will never be multiple observations for those variables
                                }
                                else
                                {
                                    return -999;
                                }
                                break;

                            default:
                                return -999;
                                break;
                        }

                    }
                }
            }
           if (!findRow) { return -999; }
           return 1;
        }

        public void updateObservationDictionary()
        {
            this.ObservationDictionary.Clear();

            this.ObservationDictionary.Add("Leaf area index", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Green area index", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Crop dry mass", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Leaf dry mass", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Leaf nitrogen", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Laminae dry mass", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Laminae nitrogen", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Specific leaf nitrogen", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Specific leaf dry mass", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Stem dry mass", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Stem nitrogen", this.CanopyObservationTable);
			this.ObservationDictionary.Add("True stem dry mass", this.CanopyObservationTable);
			this.ObservationDictionary.Add("True stem nitrogen", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Stem length", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Grain dry mass", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Crop nitrogen", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Grain nitrogen", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Single grain dry mass", this.CanopyObservationTable);
            this.ObservationDictionary.Add("Maturity single grain dry mass", this.PhenologyObservationTable);
			this.ObservationDictionary.Add("Single grain nitrogen", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Starch per grain", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Albumins-globulins per grain", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Amphiphils per grain", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Gliadins per grain", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Glutenins per grain", this.CanopyObservationTable);
			this.ObservationDictionary.Add("Soil mineral N", this.SoilObservationTable);
			this.ObservationDictionary.Add("Soil water",this.SoilObservationTable);
            this.ObservationDictionary.Add("ZC10_Emergence", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("ZC65_Anthesis", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("ZC92_Maturity", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("ZC55_Heading", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("ZC91 _End of grain filling", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Final leaf number", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Grain number", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity shoot number", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity grain yield", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Grain protein concentration", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Anthesis leaf area index",	this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Anthesis green area index",	this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Anthesis crop dry mass",this.PhenologyObservationTable);	
            this.ObservationDictionary.Add("Maturity crop dry mass", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Anthesis leaf dry mass",this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity leaf dry mass",this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Anthesis leaf nitrogen",this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity leaf nitrogen",	this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Anthesis laminae dry mass",	this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity laminae dry mass",	this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Anthesis laminae nitrogen",	this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity laminae nitrogen",	this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Anthesis specific leaf nitrogen",	this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Anthesis specific leaf dry mass",this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Anthesis stem dry mass",this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity stem dry mass",this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Anthesis stem nitrogen",this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity stem nitrogen",this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Anthesis true stem dry mass",this.PhenologyObservationTable);	
            this.ObservationDictionary.Add("Maturity true stem dry mass",this.PhenologyObservationTable);	
            this.ObservationDictionary.Add("Anthesis true stem nitrogen",	this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity true stem nitrogen",	this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Anthesis stem length", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Anthesis ear dry mass",	this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Anthesis crop nitrogen",	this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity crop nitrogen", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity grain nitrogen", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity single grain nitrogen", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity starch per grain",this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity grain starch concentration",	this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity albumins-globulins per grain",	this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity amphiphils per grain",this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity gliadins per grain",this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Maturity glutenins per grain", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Post-anthesis crop n uptake (kgn/ha)", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("% gliadins at maturity (% of total grain n)", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("% gluteins at maturity (% of total grain n)", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Gliadins-to-gluteins ratio (dimensionless)", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("DM harvest index", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("N harvest index", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Cumulative available soil nitrogen", this.SoilObservationTable);
            this.ObservationDictionary.Add("N leaching (kgn/ha)", this.SoilObservationTable);
            this.ObservationDictionary.Add("Cumulative water drainage (mm)", this.SoilObservationTable);
            this.ObservationDictionary.Add("N use efficiency (kgdm/kgn)", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("N utilisation efficiency (kgdm/kgn)", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("N uptake efficiency (kgN/kgn)", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Water use efficiency (kgdm/ha/mm)", this.PhenologyObservationTable);
            this.ObservationDictionary.Add("Cumulative N mineralisation (kgN/ha) in soil profil", this.SoilObservationTable);
            this.ObservationDictionary.Add("Cumulative N denitrification in soil profil (kgn/ha)", this.SoilObservationTable);
            this.ObservationDictionary.Add("Available mineral soil N at maturity in soil profil (kgn/ha)", this.SoilObservationTable);
            this.ObservationDictionary.Add("Total mineral soil N at maturity in soil profil (kgn/ha)", this.SoilObservationTable);
            this.ObservationDictionary.Add("Available water at maturity in soil profil (mm)", this.SoilObservationTable);
            this.ObservationDictionary.Add("Emerged leaf number", this.haunIndexObservationTable);

        }
    }

    public class ManagementCell
    {
        private string cellName;
        private int row;
        private int column;

        public string CellName
        {
            get { return cellName; }
            set { cellName = value; }
        }
        public int Row
        {
            get { return row; }
            set { row = value; }
        }
        public int Column
        {
            get { return column; }
            set { column = value; }
        }

        public ManagementCell(string _cellName, int _row, int _column)
        {
            cellName = _cellName;
            row = _row;
            column = _column;
        }
    }
}
