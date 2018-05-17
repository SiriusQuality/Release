using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;

namespace SiriusModel.InOut
{
    [Serializable]
    public partial class FileContainer : ProjectItem
    {
        #region id definition

        public static readonly string ManagementID = "Management";
        public static readonly string NonVarietyID = "Non-varietal parameters";
        public static readonly string MaizeNonVarietyID = "Non-varietal parameters for maize";
        public static readonly string RunOptionID = "Run options";
        public static readonly string SiteID = "Site";
        public static readonly string SoilID = "Soil";
        public static readonly string VarietyID = "Varietal parameters";
        public static readonly string MaizeVarietyID = "Varietal parameters for maize";
        public static readonly string RunID = "Run";

        public static readonly string ManagementProperty = "ManagementFile";
        public static readonly string NonVarietyProperty = "NonVarietyFile";
        public static readonly string RunOptionProperty = "RunOptionFile";
        public static readonly string SiteProperty = "SiteFile";
        public static readonly string SoilProperty = "SoilFile";
        public static readonly string VarietyProperty = "VarietyFile";
        public static readonly string RunProperty = "RunFile";
      


        public string PropertyFromID(string id)
        {
            return AllProperty[Array.IndexOf(AllID, id)];
        }
        
        public static readonly Type ManagementType = typeof(ManagementFile);
        public static readonly Type ParameterType = typeof(NonVarietyFile);
        public static readonly Type RunOptionType = typeof(RunOptionFile);
        public static readonly Type SiteType = typeof(SiteFile);
        public static readonly Type SoilType = typeof(SoilFile);
        public static readonly Type VarietyType = typeof(VarietyFile);
        public static readonly Type MaizeVarietyType = typeof(MaizeVarietyFile);
        public static readonly Type MaizeNonVarietyType = typeof(MaizeNonVarietyFile);
        public static readonly Type RunType = typeof(RunFile);
       

        public Type TypeFromID(string id)
        {
            return AllType[Array.IndexOf(AllID, id)];
        }

        #endregion
       
        #region file container

        [XmlIgnore]
        protected Dictionary<string, IProjectDataFile> files;

        [XmlIgnore]
        [Browsable(false)]
        public IProjectDataFile this[string id]
        {
            get { return files[id]; }
        }

        [XmlIgnore]
        public ManagementFile ManagementFile 
        { 
            get { return (ManagementFile)files[ManagementID]; }
        }

        [XmlIgnore]
        public NonVarietyFile NonVarietyFile 
        {
            get { return (NonVarietyFile)files[NonVarietyID]; }
        }

        [XmlIgnore]
        public MaizeNonVarietyFile MaizeNonVarietyFile
        {
            get { return (MaizeNonVarietyFile)files[MaizeNonVarietyID]; }
        }

        [XmlIgnore]
        public RunOptionFile RunOptionFile 
        {
            get { return (RunOptionFile)files[RunOptionID]; }
        }

        [XmlIgnore]
        public SiteFile SiteFile 
        {
            get { return (SiteFile)files[SiteID]; }
        }

        [XmlIgnore]
        public SoilFile SoilFile 
        {
            get { return (SoilFile)files[SoilID]; }
        }

        [XmlIgnore]
        public VarietyFile VarietyFile 
        { 
            get { return (VarietyFile)files[VarietyID]; }
        }

        [XmlIgnore]
        public MaizeVarietyFile MaizeVarietyFile
        {
            get { return (MaizeVarietyFile)files[MaizeVarietyID]; }
        }

        [XmlIgnore]
        public RunFile RunFile
        {
            get { return (RunFile)files[RunID]; }
        }

   
        #endregion 

        #region is modified

        private bool isModified;
        [XmlIgnore]
        public bool IsModified
        {
            get { return isModified; }
            set { if (this.SetStruct(ref isModified, ref value, "IsModified")) NotifyPropertyChanged("IsModifiedStr"); }
        }

        [XmlIgnore]
        public string IsModifiedStr
        {
            get { return IsModified ? "*" : ""; }
        }

        #endregion

        #region Relative file names
       
      
        public string ManagementFileName
        {
            get { return files[ManagementID].RelativeFileName; }
            set { files[ManagementID].AbsoluteFileName = this.GetProjectAbsoluteFileName(value); }
        }

        public string NonVarietyFileName
        {
            get { return files[NonVarietyID].RelativeFileName; }
            set { files[NonVarietyID].AbsoluteFileName = this.GetProjectAbsoluteFileName(value); }
        }
        public string MaizeNonVarietyFileName
        {
            get { return files[MaizeNonVarietyID].RelativeFileName; }
            set { files[MaizeNonVarietyID].AbsoluteFileName = this.GetProjectAbsoluteFileName(value); }
        }

        public string RunOptionFileName
        {
            get { return files[RunOptionID].RelativeFileName; }
            set { files[RunOptionID].AbsoluteFileName = this.GetProjectAbsoluteFileName(value); }
        }

        public string SiteFileName
        {
            get { return files[SiteID].RelativeFileName; }
            set { files[SiteID].AbsoluteFileName = this.GetProjectAbsoluteFileName(value); }
        }

        public string SoilFileName
        {
            get { return files[SoilID].RelativeFileName; }
            set { files[SoilID].AbsoluteFileName = this.GetProjectAbsoluteFileName(value); }
        }

        public string VarietyFileName
        {
            get { return files[VarietyID].RelativeFileName; }
            set { files[VarietyID].AbsoluteFileName = this.GetProjectAbsoluteFileName(value); }
        }

        public string MaizeVarietyFileName
        {
            get { return files[MaizeVarietyID].RelativeFileName; }
            set { files[MaizeVarietyID].AbsoluteFileName = this.GetProjectAbsoluteFileName(value); }
        }

        public string RunFileName
        {
            get { return files[RunID].RelativeFileName; }
            set { files[RunID].AbsoluteFileName = this.GetProjectAbsoluteFileName(value); }
        }


        #endregion 

        #region Comments

        private string comments;
        [XmlElement(ElementName="Comments")]
        public string Comments
        {
            get { return comments; }
            set
            {
                var deserialized = CommentsHelper.OnDeserialize(value);
                this.SetObject(ref comments, ref deserialized, "Comments");
            }
        }

        #endregion 

        #region Output version

        // private OutputVersion outputVersion;
        // public OutputVersion OutputVersion
        // {
            // get { return outputVersion; }
            // set { this.SetStruct(ref outputVersion, ref value, "OutputVersion"); }
        // }

        #endregion

        public FileContainer()
        {
            Init();        
        }

        partial void Init();

        #region load

        public void LoadAll()
        {
            partialLoadAll();
        }

        partial void partialLoadAll();

        public void loading(string Check, string TrueOrFalse)
        {
            try
            {
                var typeCheck = TypeFromID(Check);
                var LoadCheck = (bool)Serialization.DeserializeXml(typeCheck, TrueOrFalse);
            }
            catch (Exception ee)
            {
                Console.WriteLine(" catch exception " + ee.Message);
            }
        }

        public void Load(string id, string fileName)
        {
            var oldProjectDataFile = this[id];
            oldProjectDataFile.ClearWarnings();

            var oldFileName = oldProjectDataFile.AbsoluteFileName;
            string newFileName;
            Exception eCatch = null;
        
            if (!String.IsNullOrEmpty(fileName) && fileName != "?")
            {
                var idType = TypeFromID(id);

                try
                {
                    var loadProjectFile = (IProjectDataFile)Serialization.DeserializeXml(idType, fileName);
                    loadProjectFile.ClearWarnings();
                    this[id].CopyFrom(loadProjectFile);
                    newFileName = fileName;
                }
                catch (Exception e)
                {
                    this[id].CopyFrom(oldProjectDataFile);
                    newFileName = oldFileName;
                    eCatch = e;
                }
            }
            else
            {
                try
                {
                    var newProjectFile = (IProjectDataFile)Activator.CreateInstance(TypeFromID(id));
                    this[id].CopyFrom(newProjectFile);
                    newFileName = fileName;
                }
                catch (Exception e)
                {
                    newFileName = oldFileName;
                    this[id].CopyFrom(oldProjectDataFile);
                    eCatch = e;
                }
            }
            this[id].AbsoluteFileName = newFileName;
            this[id].CheckWarnings();
            this[id].IsModified = false;

            if (eCatch != null)
            {
                //this[id].IsModified = true;
                throw eCatch;
            }
        }

        #endregion

        #region save

        public void SaveAll()
        {
            partialSaveAll();
        }

        partial void partialSaveAll();

        public void Save(string id, string fileName)
        {
            if (fileName != "?")
            {
                var lastFileName = this[id].AbsoluteFileName;
                var wasModified = this[id].IsModified;
                try
                {
                    this[id].AbsoluteFileName = fileName;
                    Serialization.SerializeXml(this[id], fileName);
                    this[id].IsModified = false;
                }
                catch (Exception)
                {
                    this[id].AbsoluteFileName = lastFileName;
                    this[id].IsModified = wasModified;
                    throw;
                }
            }
        }

        #endregion

        #region new

        public void New(string id)
        {
            Load(id, "?");
        }

        #endregion 

        public void newCheck(string CheckId)
        {
            loading(CheckId, "true");
        }
        public override bool NotifyPropertyChanged(string propertyName)
        {
            if (propertyName != "IsModified" && propertyName != "IsModifiedStr") IsModified = true;
            return base.NotifyPropertyChanged(propertyName);
        }

        public override void CheckWarnings()
        {
            
        }
    }

}
