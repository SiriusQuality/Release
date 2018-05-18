using System;
using System.ComponentModel;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;
using SiriusModel.InOut.OutputWriter;

namespace SiriusModel.InOut
{
    [XmlInclude(typeof(RunItemModeNormal)), 
    XmlInclude(typeof(RunItemModeMulti)),
    XmlInclude(typeof(RunItemModeSensitivity))]
    [Serializable]
    public class RunItem : ProjectDataFileItemNoChild, IProjectItem, IWithPath
    {
        #region normal

        private RunItemModeNormal normal;

        [Browsable(false)]
        public RunItemModeNormal Normal
        {
            get { return normal; }
            set
            {
                normal = value;
                normal.RunItem = this;
                NotifyPropertyChanged("Normal");
            }
        }

        [XmlIgnore]
        public string NormalOutputDirectory
        {
            get { return normal.OutputDirectory; }
        }

        [XmlIgnore]
        public string NormalOutputFileName
        {
            get { return normal.OutputFileName; }
        }

        [XmlIgnore]
        public Book NormalBook
        {
            get { return normal.Book; }
            set
            {
                normal.Book = value;
                NotifyPropertyChanged("NormalBook");
            }
        }

        [XmlIgnore]
        public string NormalManagementItem
        {
            get { return normal.ManagementItem; }
            set { this.SetObject(normal, "ManagementItem", ref value, "NormalManagementItem"); }
        }

        [XmlIgnore]
        public string NormalParameterItem
        {
            get { return normal.ParameterItem; }
            set { this.SetObject(normal, "ParameterItem", ref value, "NormalParameterItem"); }
        }

        [XmlIgnore]
        public string NormalRunOptionItem
        {
            get { return normal.RunOptionItem; }
            set { this.SetObject(normal, "RunOptionItem", ref value, "NormalRunOptionItem"); }
        }

        [XmlIgnore]
        public string NormalSiteItem
        {
            get { return normal.SiteItem; }
            set { this.SetObject(normal, "SiteItem", ref value, "NormalSiteItem"); }
        }

        [XmlIgnore]
        public string NormalSoilItem
        {
            get { return normal.SoilItem; }
            set { this.SetObject(normal, "SoilItem", ref value, "NormalSoilItem"); }
        }

        [XmlIgnore]
        public string NormalVarietyItem
        {
            get { return normal.VarietyItem; }
            set { this.SetObject(normal, "VarietyItem", ref value, "NormalVarietyItem"); }
        }

        [XmlIgnore]
        public string NormalExperimentItem
        {
            get { return normal.ExperimentItem; }
            set { this.SetObject(normal, "ExperimentItem", ref value, "NormalExperimentItem"); }
        }

        #endregion 

        #region multi

        private RunItemModeMulti multi;

        [Browsable(false)]
        public RunItemModeMulti Multi
        {
            get { return multi; }
            set
            {
                multi = value;
                multi.RunItem = this;
                NotifyPropertyChanged("Multi");
            }
        }

        [XmlIgnore]
        public string MultiOutputDirectory
        {
            get { return multi.OutputDirectory; }
        }

        [XmlIgnore]
        public string MultiOutputFileName
        {
            get { return multi.OutputFileName; }
        }

        [XmlIgnore]
        public Book MultiBook
        {
            get { return multi.Book; }
            set
            {
                multi.Book = value;
                NotifyPropertyChanged("MultiBook");
            }
        }

        [XmlIgnore]
        public BindingList<MultiRunItem> MultiRuns
        {
            get { return multi.MultiRuns; }
        }

        [XmlIgnore]
        public bool MultiExportNormalRuns
        {
            get { return multi.ExportNormalRuns; }
            set { this.SetStruct(multi, "ExportNormalRuns", ref value, "MultiExportNormalRuns"); }
        }

        [XmlIgnore]
        public string MultiDailyOutputPattern
        {
            get { return multi.DailyOutputPattern; }
        }

        [XmlIgnore]
        public bool MultiMultiYear
        {
            get { return multi.MultiYear; }
            set { this.SetStruct(multi, "MultiYear", ref value, "MultiMultiYear"); }
        }

        [XmlIgnore]
        public int MultiFirstYear
        {
            get { return multi.FirstYear; }
            set { this.SetStruct(multi, "FirstYear", ref value, "MultiFirstYear"); }
        }

        [XmlIgnore]
        public int MultiLastYear
        {
            get { return multi.LastYear; }
            set { this.SetStruct(multi, "LastYear", ref value, "MultiLastYear"); }
        }

        #endregion

        #region sensitivity

        private RunItemModeSensitivity sensitivity;

        [Browsable(false)]
        public RunItemModeSensitivity Sensitivity
        {
            get { return sensitivity; }
            set
            {
                sensitivity = value;
                sensitivity.RunItem = this;
                NotifyPropertyChanged("Sensitivity");
            }
        }

        [XmlIgnore]
        public string SensitivityOutputDirectory
        {
            get { return sensitivity.OutputDirectory; }
        }

        [XmlIgnore]
        public string SensitivityOutputFileName
        {
            get { return sensitivity.OutputFileName; }
        }

        [XmlIgnore]
        public BindingList<SensitivityRunItem> SensitivityRuns
        {
            get { return sensitivity.SensitivityRuns; }
        }

        [XmlIgnore]
        public bool SensitivityExportNormalRuns
        {
            get { return sensitivity.ExportNormalRuns; }
            set { this.SetStruct(sensitivity, "ExportNormalRuns", ref value, "SensitivityExportNormalRuns"); }
        }

        [XmlIgnore]
        public bool SensitivityOneByOne
        {
            get { return sensitivity.OneByOne; }
            set { this.SetStruct(sensitivity, "OneByOne", ref value, "SensitivityOneByOne"); }
        }

        [XmlIgnore]
        public string SensitivityDailyOutputPattern
        {
            get { return sensitivity.DailyOutputPattern; }
        }

        [XmlIgnore]
        public Book SensitivityBook
        {
            get { return sensitivity.Book; }
            set
            {
                sensitivity.Book = value;
                NotifyPropertyChanged("SensitivityBook");
            }
        }

        #endregion 

        #region IProjectDataFileItemWithPath

        public override void UpdatePath(string oldAbsolute, string newAbsolute)
        {
            normal.UpdatePath(oldAbsolute, newAbsolute);
            multi.UpdatePath(oldAbsolute, newAbsolute);
            sensitivity.UpdatePath(oldAbsolute, newAbsolute);
        }

        #endregion

        #region constructor

        public RunItem()
            : this("")
        {
        }

        public RunItem(string name)
            : base(name)
        {
            normal = new RunItemModeNormal {RunItem = this};
            multi = new RunItemModeMulti {RunItem = this};
            sensitivity = new RunItemModeSensitivity {RunItem = this};
        }

        #endregion 

        public override void ClearWarnings()
        {
            base.ClearWarnings();
            normal.ClearWarnings();
            multi.ClearWarnings();
            sensitivity.ClearWarnings();
        }

        public override void CheckWarnings()
        {
            base.ClearWarnings();
            normal.CheckWarnings();
            multi.CheckWarnings();
            sensitivity.CheckWarnings();
        }
    }
}
