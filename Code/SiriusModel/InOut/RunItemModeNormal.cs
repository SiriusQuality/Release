using System;
using System.ComponentModel;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;
using SiriusModel.InOut.OutputWriter;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using SiriusModel.Model;
using SiriusModel.Model.CropModel;
using SiriusModel.Model.Observation;
using SiriusModel.Model.SoilModel;
using SiriusModel.InOut;

namespace SiriusModel.InOut
{
    [Serializable]
    [XmlInclude(typeof(RunItemModeContainerNoChild))]
    public class RunItemModeNormal : RunItemModeContainerNoChild
    {

        private string managementItem;
        private string parameterItem;
        private string runOptionItem;
        private string siteItem;
        private string soilItem;
        private string varietyItem;
        private string experimentItem;

        public RunItemModeNormal()
        {
            managementItem = "";
            parameterItem = "";
            runOptionItem = "";
            siteItem = "";
            soilItem = "";
            varietyItem = "";
            experimentItem = "";
            OutputPattern = OutputPatternVarDef.VarVVer + OutputPatternVarDef.VarManagementItem;
        }

        #region normal mode parameters

        public string ManagementItem
        {
            get { return managementItem; }
            set
            {
                if (this.SetObject(ref managementItem, ref value, "ManagementItem"))
                {
                    if (RunItem != null) RunItem.NotifyPropertyChanged(ModeID + "OutputFileName");
                }
            }
        }

        public string ParameterItem
        {
            get { return parameterItem; }
            set
            {
                if (this.SetObject(ref parameterItem, ref value, "ParameterItem"))
                {
                    if (RunItem != null) RunItem.NotifyPropertyChanged(ModeID + "OutputFileName");
                }
            }
        }

        public string RunOptionItem
        {
            get { return runOptionItem; }
            set
            {
                if (this.SetObject(ref runOptionItem, ref value, "RunOptionItem"))
                {
                    if (RunItem != null) RunItem.NotifyPropertyChanged(ModeID + "OutputFileName");
                }
            }
        }

        public string SiteItem
        {
            get { return siteItem; }
            set
            {
                if (this.SetObject(ref siteItem, ref value, "SiteItem"))
                {
                    if (RunItem != null) RunItem.NotifyPropertyChanged(ModeID + "OutputFileName");
                }
            }
        }

        public string SoilItem
        {
            get { return soilItem; }
            set
            {
                if (this.SetObject(ref soilItem, ref value, "SoilItem"))
                {
                    if (RunItem != null) RunItem.NotifyPropertyChanged(ModeID + "OutputFileName");
                }
            }
        }

        public string VarietyItem
        {
            get { return varietyItem; }
            set
            {
                if (this.SetObject(ref varietyItem, ref value, "VarietyItem"))
                {
                    if (RunItem != null) RunItem.NotifyPropertyChanged(ModeID + "OutputFileName");
                }
            }
        }

        public string ExperimentItem
        {
            get { return experimentItem; }
            set
            {
                if (this.SetObject(ref experimentItem, ref value, "ExperimentItem"))
                {
                    if (RunItem != null) RunItem.NotifyPropertyChanged(ModeID + "OutputFileName");
                }
            }
        }
        #endregion

        #region iterate definition

        public override int InitRun(bool serialize)
        {
            Book.Clear();
            return 1;
        }

        public override void StepRun(bool serialize, int i, string variety = null, string parameterName = null, double parameterValue = 0)
        {
            if (i == 0)
            {
                ///<Behnam>
                string miName, piName, riName, roiName, siiName, soiName, viName;
                miName = RunItem.Normal.ManagementItem;
                piName = RunItem.Normal.ParameterItem;
                riName = RunItem.Name;
                roiName = RunItem.Normal.RunOptionItem;
                siiName = RunItem.Normal.SiteItem;
                soiName = RunItem.Normal.SoilItem;
                viName = RunItem.Normal.VarietyItem;

                ManagementItem mi = null;
                CropParameterItem pi = null;
                RunOptionItem roi = null;
                SiteItem sii = null;
                SoilItem soi = null;
                CropParameterItem vi = null;
                RunCore.GetItems(riName, miName, piName, roiName, siiName, soiName, viName, ref mi, ref pi, ref roi, ref sii, ref soi, ref vi);

                switch (roi.OutputPattern)
                {
                    case "V13": ProjectFile.OutputVersion = OutputVersion.V13; break;
                    case "V15": ProjectFile.OutputVersion = OutputVersion.V15; break;
                    case "Cus": ProjectFile.OutputVersion = OutputVersion.Cus; break;
                    case "Maize": ProjectFile.OutputVersion = OutputVersion.Maize; break;
                }
                ///</Behnam>

                ///<Behnam (2016.01.19)>
                ///<Comment>Adding the Summary file to the outputs of a Normal run</Comment>
                var SumOutputFileName = OutputFile.ExtractMultiRunHeader();
                SumOutputFileName.Title = OutputFileName.Replace(OutputExtention, SumOutputExtention);
                Book.Add(SumOutputFileName);

                Run.isFirstYear = true;
                var excelPage = RunCore.Run(RunItem.Name, true, true, ManagementItem, ParameterItem, RunOptionItem, SiteItem, SoilItem, VarietyItem);
                excelPage.Title = OutputFileName;
                Book.Add(excelPage);

                var WarningOutputFileName = OutputFile.ExtractWarningsNormalRun(RunCore.RunInstance);
                WarningOutputFileName.Title = "Warnings";
                Book.Add(WarningOutputFileName);


                if (serialize) RunCore.Save(excelPage, AbsoluteOutputPath, true);

                if (ProjectFile.OutputVersion == OutputVersion.Cus)
                {
                    Run.SecondLine = false;
                    Book[0].Add(OutputFile.ExtractMultiRunLine(RunCore.RunInstance));
                }
                Book[0].Add(OutputFile.ExtractMultiRunLine(RunCore.RunInstance));
                ///</Behnam>
            }
            else throw new RunException(RunItem.Name, "Normal run index != 0");
        }

        public override void EndRun(bool serialize)
        {
            if (serialize && Book.Count > 0) RunCore.Save(Book[0], AbsoluteOutputPath.Replace(OutputExtention, SumOutputExtention), true);
            RunCore.RunInstance.DoStop();
        }

        #endregion

        #region output path

        public override string OutputExtention
        {
            get { return ".sqsro"; }
        }

        public string SumOutputExtention
        {
            get { return ".sqbrs"; }
        }

        public override string OutputFileNameWithoutExtension
        {
            get
            {
                var outputName = OutputPatternVarDef.OutputPatternValue(OutputPattern, ManagementItem, ParameterItem, RunItem.Name, RunOptionItem, SiteItem, SoilItem, VarietyItem, ExperimentItem);
                return outputName;
            }
        }

        #endregion

        #region mode definition

        public override string ModeID
        {
            get { return "Normal"; }
        }

        #endregion

        public override void CheckWarnings()
        {

        }
    }
}
