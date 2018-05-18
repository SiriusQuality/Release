using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;

namespace SiriusModel.InOut
{
    public partial class FileContainer
    {
        #region id definition

        public static readonly string OptimizationID = "Optimization";
        public static readonly string ObservationID = "Observation";

        public static readonly string OptimizationProperty = "OptimizationFile";
        public static readonly string ObservationProperty = "ObservationFile";

        public static readonly Type OptimizationType = typeof(OptimizationFile);
        public static readonly Type ObservationType = typeof(ObservationFile);

        #endregion

        #region file container

        [XmlIgnore]
        public OptimizationFile OptimizationFile
        {
            get { return (OptimizationFile)files[OptimizationID]; }
        }

        [XmlIgnore]
        public ObservationFile ObservationFile
        {
            get { return (ObservationFile)files[ObservationID]; }
        }

        #endregion

        #region Relative file names

        public string OptimizationFileName
        {
            get { return files[OptimizationID].RelativeFileName; }
            set { files[OptimizationID].AbsoluteFileName = this.GetProjectAbsoluteFileName(value); }
        }

        public string ObservationFileName
        {
            get { return files[ObservationID].RelativeFileName; }
            set { files[ObservationID].AbsoluteFileName = this.GetProjectAbsoluteFileName(value); }
        }

        #endregion

        ///<Behnam>
        public readonly string[] AllID = new[] { 
            ManagementID, SiteID, SoilID, NonVarietyID,MaizeNonVarietyID, VarietyID, MaizeVarietyID,
            RunID, RunOptionID, OptimizationID, ObservationID};

        public readonly string[] AllProperty = new[] { 
            ManagementProperty, SiteProperty, SoilProperty, NonVarietyProperty,NonVarietyProperty, VarietyProperty, VarietyProperty,
            RunProperty, RunOptionProperty, OptimizationProperty, ObservationProperty};

        public readonly Type[] AllType = new[] { 
            ManagementType, SiteType, SoilType, ParameterType,MaizeNonVarietyType, VarietyType, MaizeVarietyType,
            RunType, RunOptionType, OptimizationType, ObservationType};

        partial void Init()
        {
            files = new Dictionary<string, IProjectDataFile>();

            files.Add(ManagementID, new ManagementFile());
            files.Add(SiteID, new SiteFile());
            files.Add(SoilID, new SoilFile());
            files.Add(NonVarietyID, new NonVarietyFile());
            files.Add(MaizeNonVarietyID, new MaizeNonVarietyFile());
            files.Add(VarietyID, new VarietyFile());
            files.Add(MaizeVarietyID, new MaizeVarietyFile());
            files.Add(RunID, new RunFile());
            files.Add(RunOptionID, new RunOptionFile());
            files.Add(OptimizationID, new OptimizationFile());
            files.Add(ObservationID, new ObservationFile());

            Comments = "";
            // outputVersion = OutputVersion.V15;
            IsModified = false;
        }

        partial void partialLoadAll()
        {
            Load(VarietyID, VarietyFile.AbsoluteFileName);
            Load(MaizeVarietyID, MaizeVarietyFile.AbsoluteFileName);
            Load(SoilID, SoilFile.AbsoluteFileName);
            Load(SiteID, SiteFile.AbsoluteFileName);
            Load(ManagementID, ManagementFile.AbsoluteFileName);
            Load(RunOptionID, RunOptionFile.AbsoluteFileName);
            Load(NonVarietyID, NonVarietyFile.AbsoluteFileName);
            Load(MaizeNonVarietyID, MaizeNonVarietyFile.AbsoluteFileName);
            Load(RunID, RunFile.AbsoluteFileName);

            Load(OptimizationID, OptimizationFile.AbsoluteFileName);
            Load(ObservationID, ObservationFile.AbsoluteFileName);
        }

        partial void partialSaveAll()
        {
            Save(SoilID, SoilFile.AbsoluteFileName);
            Save(SiteID, SiteFile.AbsoluteFileName);
            Save(ManagementID, ManagementFile.AbsoluteFileName);
            Save(NonVarietyID, NonVarietyFile.AbsoluteFileName);
            Save(MaizeNonVarietyID, MaizeNonVarietyFile.AbsoluteFileName);
            Save(VarietyID, VarietyFile.AbsoluteFileName);
            Save(MaizeVarietyID, MaizeVarietyFile.AbsoluteFileName);
            Save(RunID, RunFile.AbsoluteFileName);
            Save(RunOptionID, RunOptionFile.AbsoluteFileName);

            Save(OptimizationID, OptimizationFile.AbsoluteFileName);
            Save(ObservationID, ObservationFile.AbsoluteFileName);
        }
        ///</Behnam>
    }
}
