using System;
using System.ComponentModel;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;
using SiriusModel.InOut.OutputWriter;
using SiriusModel.Model;

namespace SiriusModel.InOut
{
    public static class OutputPatternVarDef 
    {
        public const string VarVVer= "$(vVer_)";
        public const string VarManagementItem = "$(ManagementName)";
        public const string VarParameterItem = "$(NonVarietalParametersName)";
        public const string VarRunItem = "$(RunName)";
        public const string VarRunOptionItem = "$(RunOptionName)";
        public const string VarSiteItem = "$(SiteName)";
        public const string VarSoilItem = "$(SoilName)";
        public const string VarVarietyItem = "$(VarietyName)";
        public const string VarExperimentItem = "$(ExperimentName)";


        private const string ValVVer = "v1.5_";

        public static string OutputPatternValue(string pattern, string miName, string piName, string riName, string roiName, string siiName, string soiName, string viName, string exName)
        {
            var value = pattern.Replace(VarVVer, ValVVer);
            value = value.Replace(VarManagementItem, (!String.IsNullOrEmpty(miName)) ? miName : "(Management?)");
            value = value.Replace(VarParameterItem, (!String.IsNullOrEmpty(piName)) ? piName : "(Non-varietal parameters?)");
            value = value.Replace(VarRunItem, (!String.IsNullOrEmpty(riName)) ? riName : "(Element?)");
            value = value.Replace(VarRunOptionItem, (!String.IsNullOrEmpty(roiName)) ? roiName : "(Element option?)");
            value = value.Replace(VarSiteItem, (!String.IsNullOrEmpty(siiName)) ? siiName : "(Site?)");
            value = value.Replace(VarSoilItem, (!String.IsNullOrEmpty(soiName)) ? soiName : "(Soil?)");
            value = value.Replace(VarVarietyItem, (!String.IsNullOrEmpty(viName)) ? viName : "(Variety?)");
            value = value.Replace(VarExperimentItem, (!String.IsNullOrEmpty(exName)) ? exName : "(Experiment?)");
            return value;
        }
    }

    public static class RunCore
    {
        public static readonly Run RunInstance = new Run();

        internal static PageData Run(string runItemName, bool extract, bool isNormalRun, string miName, string piName, string roiName, string siiName, string soiName, string viName)
        {
            ManagementItem mi = null;
            CropParameterItem pi = null;
            RunOptionItem roi = null;
            SiteItem sii = null;
            SoilItem soi = null;
            CropParameterItem vi = null;
            GetItems(runItemName, miName, piName, roiName, siiName, soiName, viName,
                 ref mi, ref pi, ref roi, ref sii, ref soi, ref vi);

            return Run(runItemName, extract, isNormalRun, mi, pi, roi, sii, soi, vi);
        }

        public static void GetItems(string runItemName,
            string miName, string piName, string roiName, string siiName, string soiName, string viName,
            ref ManagementItem mi, ref CropParameterItem pi, ref RunOptionItem roi, ref SiteItem sii, ref SoilItem soi, ref CropParameterItem vi)
        {
            mi = ProjectFile.This.FileContainer.ManagementFile[miName];
            pi = ProjectFile.This.FileContainer.NonVarietyFile[piName];
            if (pi == null) { pi = ProjectFile.This.FileContainer.MaizeNonVarietyFile[piName]; }           
            roi = ProjectFile.This.FileContainer.RunOptionFile[roiName];
            sii = ProjectFile.This.FileContainer.SiteFile[siiName];
            soi = ProjectFile.This.FileContainer.SoilFile[soiName];
            vi = ProjectFile.This.FileContainer.VarietyFile[viName];
            if (vi == null) { vi = ProjectFile.This.FileContainer.MaizeVarietyFile[viName]; }

            if (mi == null) throw new RunException(runItemName, "Management not found: " + miName);
            if (pi == null) throw new RunException(runItemName, "Parameter not found: " + piName);
            if (roi == null) throw new RunException(runItemName, "Element option not found: " + roiName);
            if (sii == null) throw new RunException(runItemName, "Site not found: " + siiName);
            if (soi == null) throw new RunException(runItemName, "Soil not found: " + soiName);
            if (vi == null) throw new RunException(runItemName, "Variety not found: " + viName);
        }

        internal static PageData Run(string runItemName, bool extract, bool isNormalRun,
            ManagementItem mi,
            CropParameterItem pi,
            RunOptionItem roi,
            SiteItem sii,
            SoilItem soi,
            CropParameterItem vi)
        {
            RunInstance.Start(vi, soi, sii, mi, pi, roi);

            if (extract) return OutputFile.ExtractNormalRun(RunInstance);
            return null;
        }



        ///<Behnam>
        ///<Comment>
        ///The code was modified to add a boolean to show if it is the first year of simulation.
        ///In the first year, the output file is created, while in the following years, the outputs
        ///are appended to the previously created file.
        ///</Comment>
        internal static void Save(PageData data, string absoluteOutputPath, bool isFirstYear = true)
        {
            Serialization.SerializeText(data, absoluteOutputPath, isFirstYear);
        }
        ///</Behnam>
    }

    public interface IRunItemModeContainer : IWithPath, IProjectItem
    {
        [XmlIgnore]
        Book Book { get; set; }

        string OutputDirectory { get; set; }

        string OutputPattern { get; set; }

        [XmlIgnore]
        string OutputPath { get; } 

        [XmlIgnore]
        string AbsoluteOutputPath { get; }

        [XmlIgnore]
        string AbsoluteOutputDirectory { get; set; }

        [XmlIgnore]
        string OutputFileName { get; }

        [XmlIgnore]
        string OutputFileNameWithoutExtension {
            get;
        }

        [XmlIgnore]
        string OutputExtention { get; }

        int InitRun(bool serialize);
        void StepRun(bool serialize, int i, string variety = null, string parameterName = null, double parameterValue = 0);
        void EndRun(bool serialize);

        [XmlIgnore]
        RunItem RunItem { get; set; }

        string ModeID
        {
            get;
        }
    }

    [Serializable]
    public abstract class RunItemModeContainerNoChild : ProjectItem, IRunItemModeContainer
    {
        protected RunItemModeContainerNoChild()
        {
            outputDirectory = "?";
        }

        #region IRunItemModeContainer Members

        [NonSerialized]
        private Book book = new Book();

        [XmlIgnore]
        public Book Book
        {
            get { return book; }
            set 
            {
                book = value;
                NotifyPropertyChanged("Book"); 
            }
        }

        private string outputDirectory;
        private string outputPattern;

        public string OutputDirectory
        {
            get { return outputDirectory; }
            set
            {
                if (this.SetObject(ref outputDirectory, ref value, "OutputDirectory"))
                {
                    if (RunItem != null) RunItem.NotifyPropertyChanged(ModeID + "OutputDirectory");
                }
            }
        }

        public string OutputPattern
        {
            get { return outputPattern; }
            set
            {
                if (this.SetObject(ref outputPattern, ref value, "OutputPattern"))
                {
                    if (RunItem != null) RunItem.NotifyPropertyChanged(ModeID + "OutputFileName");
                }
            }
        }

        [XmlIgnore]
        public string OutputPath
        {
            get { return OutputDirectory + "\\" + OutputFileName; }
        }

        [XmlIgnore]
        public string AbsoluteOutputPath
        {
            get
            {
                var referenceAbsoluteFile = GetReferenceAbsoluteFileName();
                return FileHelper.GetAbsolute(referenceAbsoluteFile, OutputPath);
            }
        }

        [XmlIgnore]
        public string AbsoluteOutputDirectory
        {
            get
            {
                var referenceAbsoluteFile = GetReferenceAbsoluteFileName();
                return FileHelper.GetAbsolute(referenceAbsoluteFile, OutputDirectory);
            }
            set
            {
                var referenceAbsoluteFile = GetReferenceAbsoluteFileName();
                OutputDirectory = FileHelper.GetRelative(referenceAbsoluteFile, value);
            }
        }

        [XmlIgnore]
        public string OutputFileName
        {
            get { return OutputFileNameWithoutExtension + OutputExtention; }
        }

        [XmlIgnore]
        public abstract string OutputFileNameWithoutExtension
        {
            get;
        }

        [XmlIgnore]
        public abstract string OutputExtention
        {
            get;
        }

        private static string GetReferenceAbsoluteFileName()
        {
            var simpleRunFile = (ProjectFile.This != null) ? ProjectFile.This.FileContainer.RunFile : null;
            var referenceFile = (simpleRunFile != null) ? simpleRunFile.AbsoluteFileName : null;
            return referenceFile;
        }

        public abstract int InitRun(bool serialize);
        public abstract void StepRun(bool serialize, int i, string variety = null, string parameterName = null, double parameterValue = 0);
        public abstract void EndRun(bool serialize);

        private RunItem runItem;

        [XmlIgnore]
        public RunItem RunItem
        {
            get { return runItem; }
            set
            {
                if (this.SetObject(ref runItem, ref value, "RunItem"))
                {
                    if (RunItem != null)
                    {
                        RunItem.NotifyPropertyChanged(ModeID + "OutputDirectory");
                        RunItem.NotifyPropertyChanged(ModeID + "OutputFileName");
                    }
                }
            }
        }

        public abstract string ModeID
        {
            get;
        }

        #endregion

        #region IProjectDataFileItemWithPath Members

        public virtual void UpdatePath(string oldAbsolute, string newAbsolute)
        {
            var directoryOldAbsolute = FileHelper.GetAbsolute(oldAbsolute, OutputDirectory);
            var directoryNewAbsolute = FileHelper.GetRelative(newAbsolute, directoryOldAbsolute);
            OutputDirectory = directoryNewAbsolute;
        }

        #endregion

        public override bool NotifyPropertyChanged(string propertyName)
        {
            if (RunItem != null && RunItem.ProjectDataFileParent != null && propertyName != "Book") RunItem.ProjectDataFileParent.IsModified = true;
            return base.NotifyPropertyChanged(propertyName);
        }
    }

    [Serializable]
    public class RunItemModeContainer1ChildKeyGenerator<TChildType, TChildKey> : ChildKeyGeneratorSorted<TChildType, TChildKey>
        where TChildKey : IComparable
        where TChildType : IChild<TChildKey>, new()
    {
        public override bool Selectable
        {
            get { return false; }
        }

        public override bool  Sorted
        {
            get { return false; }
        }

        public override bool  NullSelectable
        {
            get { return false; }
        }

        public override string  KeyPropertyName
        {
	        get { throw new NotImplementedException(); }
        }

        public override Func<TChildType,TChildKey>  KeySelector
        {
	        get { throw new NotImplementedException(); }
        }

        public override Func<TChildType, TChildKey, TChildKey> KeySetter
        {
	        get { throw new NotImplementedException(); }
        }

        public override void CreateNullSelectable(BaseBindingList<TChildType> selectable)
        {
 	        throw new NotImplementedException();
        }
    }

    [Serializable]
    public abstract class RunItemModeContainer1Child<TChildType, TChildKey>
        : Parent1Child<TChildType, TChildKey, RunItemModeContainer1ChildKeyGenerator<TChildType, TChildKey>>, IRunItemModeContainer
        where TChildKey : IComparable
        where TChildType : IChild<TChildKey>, new()
    {

        protected RunItemModeContainer1Child()
        {
            outputDirectory = "?";
            BindingItems1.ListChanged += BindingItems1ListChanged;
        }

        void BindingItems1ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType != ListChangedType.Reset)
            {
                if (RunItem != null && RunItem.ProjectDataFileParent != null) RunItem.ProjectDataFileParent.IsModified = true;
            }
        }

        #region IRunItemModeContainer Members

        [NonSerialized]
        private Book book;

        [XmlIgnore]
        public Book Book
        {
            get { return book; }
            set 
            {

                book = value;
                NotifyPropertyChanged("Book"); 
            }
        }

        private string outputDirectory;
        private string outputPattern;

        public string OutputDirectory
        {
            get { return outputDirectory; }
            set
            {
                if (this.SetObject(ref outputDirectory, ref value, "OutputDirectory"))
                {
                    if (RunItem != null) RunItem.NotifyPropertyChanged(ModeID + "OutputDirectory");
                }
            }
        }

        public string OutputPattern
        {
            get { return outputPattern; }
            set
            {
                if (this.SetObject(ref outputPattern, ref value, "OutputPattern"))
                {
                    if (RunItem != null) RunItem.NotifyPropertyChanged(ModeID + "OutputFileName");
                }
            }
        }

        [XmlIgnore]
        public string OutputPath
        {
            get { return OutputDirectory + "\\" + OutputFileName; }
        }

        [XmlIgnore]
        public string AbsoluteOutputPath
        {
            get
            {
                var referenceAbsoluteFile = GetReferenceAbsoluteFileName();
                return FileHelper.GetAbsolute(referenceAbsoluteFile, OutputPath);
            }
        }

        [XmlIgnore]
        public string AbsoluteOutputDirectory
        {
            get
            {
                var referenceAbsoluteFile = GetReferenceAbsoluteFileName();
                return FileHelper.GetAbsolute(referenceAbsoluteFile, OutputDirectory);
            }
            set
            {
                var referenceAbsoluteFile = GetReferenceAbsoluteFileName();
                OutputDirectory = FileHelper.GetRelative(referenceAbsoluteFile, value);
            }
        }

        [XmlIgnore]
        public string OutputFileName
        {
            get { return OutputFileNameWithoutExtension + OutputExtention; }
        }

        [XmlIgnore]
        public abstract string OutputFileNameWithoutExtension
        {
            get;
        }

        [XmlIgnore]
        public abstract string OutputExtention
        {
            get;
        }

        private static string GetReferenceAbsoluteFileName()
        {
            var simpleRunFile = (ProjectFile.This != null) ? ProjectFile.This.FileContainer.RunFile : null;
            var referenceFile = (simpleRunFile != null) ? simpleRunFile.AbsoluteFileName : null;
            return referenceFile;
        }

        public abstract int InitRun(bool serialize);
        public abstract void StepRun(bool serialize, int i, string variety = null, string parameterName = null, double parameterValue = 0);
        public abstract void EndRun(bool serialize);

        private RunItem runItem;

        [XmlIgnore]
        public RunItem RunItem
        {
            get { return runItem; }
            set
            {
                if (this.SetObject(ref runItem, ref value, "RunItem"))
                {
                    if (RunItem != null)
                    {
                        RunItem.NotifyPropertyChanged(ModeID + "OutputDirectory");
                        RunItem.NotifyPropertyChanged(ModeID + "OutputFileName");
                    }
                }
            }
        }

        public abstract string ModeID
        {
            get;
        }

        #endregion

        #region IProjectDataFileItemWithPath Members

        public virtual void UpdatePath(string oldAbsolute, string newAbsolute)
        {
            var directoryOldAbsolute = FileHelper.GetAbsolute(oldAbsolute, OutputDirectory);
            var directoryNewAbsolute = FileHelper.GetRelative(newAbsolute, directoryOldAbsolute);
            OutputDirectory = directoryNewAbsolute;
        }

        #endregion

        public override bool NotifyPropertyChanged(string propertyName)
        {
            if (RunItem != null && RunItem.ProjectDataFileParent != null && propertyName != "Book") RunItem.ProjectDataFileParent.IsModified = true;
            return base.NotifyPropertyChanged(propertyName);
        }

        public override void ClearWarnings()
        {
            base.ClearWarnings();
            foreach (var child in BindingItems1)
            {
                child.ClearWarnings();
            }
        }
    }
}
