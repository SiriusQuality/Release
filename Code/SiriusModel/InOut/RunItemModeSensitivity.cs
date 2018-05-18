using System;
using System.ComponentModel;
using System.Reflection;
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
    public enum SensitivityMode
    {
        RegularMinMax,
        RegularPercent
    }

    [Serializable]
    public class SensitivityRunItem : Child<int>
    {
        #region Parent cast

        private RunItemModeSensitivity RunItemModeSensitivityParent { get { return Parent as RunItemModeSensitivity; } }

        private RunItem ParentRunItem
        {
            get
            {
                var runItemModeSensitivity = RunItemModeSensitivityParent;
                return runItemModeSensitivity != null ? runItemModeSensitivity.RunItem : null;
            }
        }
        #endregion

        #region list all double parameter ids

        public static string[] GetParameterIDs()
        {
            var managementProperties = GetParameterIDs(FileContainer.ManagementID, typeof(ManagementItem));
            var parameterProperties = GetParameterIDsCrop("Crop");
            ///<Behbnam (2015.12.08)>
            ///<Comment>RunID must have been replaced by RunOptionID</Comment>
            var runOptionProperties = GetParameterIDs(FileContainer.RunOptionID, typeof(RunOptionItem));
            ///</Behnam>
            var siteProperties = GetParameterIDs(FileContainer.SiteID, typeof(SiteItem));
            var soilProperties = GetParameterIDs(FileContainer.SoilID, typeof(SoilItem));
            //var varietyProperties = GetParameterIDs(FileContainer.VarietyID, typeof(CropParameterItem));

            var allProperties = new string[0];
            allProperties = allProperties.Merge(
                new[] { "" },
                managementProperties,
                parameterProperties,
                runOptionProperties,
                siteProperties,
                soilProperties
                //,varietyProperties
                );

            return allProperties;
        }

        private static string[] GetParameterIDs(string fileId, Type fileItemType)
        {
            var propertyInfos = fileItemType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var properties = new string[0];
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite && propertyInfo.PropertyType == typeof(double)
                    && propertyInfo.GetIndexParameters().Length == 0)
                {
                    Array.Resize(ref properties, properties.Length + 1);
                    properties[properties.Length - 1] = fileId + "." + propertyInfo.Name;
                }
            }
            return properties;
        }

        private static string[] GetParameterIDsCrop(string fileId)
        {
            string[] res = new string[0];
            foreach (string parameterName in CropParameterItem.paramNameList)
            {
                {
                    Array.Resize(ref res, res.Length + 1);
                    res[res.Length - 1] = fileId + "." + parameterName;
                }
            }
            return res;
        }
        #endregion

        #region define a delta step

        private string parameterID;
        public string ParameterID
        {
            get { return parameterID; }
            set
            {
                if (this.SetObject(ref parameterID, ref value, "ParameterID"))
                {
                    ClearWarnings();
                    CheckWarnings();
                    propertyInfo = null;
                }
            }
        }

        private SensitivityMode mode;
        public SensitivityMode Mode
        {
            get { return mode; }
            set
            {
                this.SetStruct(ref mode, ref value, "Mode");
                AssertModeMinMaxNbStep();
            }
        }

        private PropertyInfo propertyInfo;

        private double min;
        public double Min
        {
            get { return min; }
            set
            {
                this.SetStruct(ref min, ref value, "Min");
                AssertModeMinMaxNbStep();
            }
        }

        private double max;
        public double Max
        {
            get { return max; }
            set
            {
                this.SetStruct(ref max, ref value, "Max");
                AssertModeMinMaxNbStep();
            }
        }

        private int nbStep;
        public int NbStep
        {
            get { return nbStep; }
            set
            {
                this.SetStruct(ref nbStep, ref value, "NbStep");
                AssertModeMinMaxNbStep();
            }
        }

        private void AssertModeMinMaxNbStep()
        {
            this.Assert(nbStep, d => d > 0, "NbStep", "NbStep must be > 0.", null);
            if (mode == SensitivityMode.RegularMinMax)
            {
                this.Assert(min, d => true, "Min", "Min is the absolute decrease of the value in percent (must be > 0).", null);
                this.Assert(max, d => true, "Max", "Max is the absolute increase of the value in percent (must be > 0).", null);
            }
            else
            {
                this.Assert(min, d => d > 0, "Min", "Min is the absolute decrease of the value in percent (must be > 0).", null);
                this.Assert(max, d => d > 0, "Max", "Max is the absolute increase of the value in percent (must be > 0).", null);
            }
        }

        #endregion

        #region run temp values

        internal int Step { get; set; }
        internal double InitialValue { get; set; }

        #endregion

        public SensitivityRunItem()
        {
            ParameterID = "";
            Mode = SensitivityMode.RegularPercent;
            Min = 10;
            Max = 10;
            NbStep = 1;
            Step = 0;
        }

        private string GetFileID()
        {
            var indexDot = parameterID.LastIndexOf('.');
            if (indexDot < 1) throw new RunException(ParentRunItem.Name, "Parameter not found : " + parameterID);
            return parameterID.Substring(0, indexDot);
        }

        private object GetItem(ManagementItem mi, RunOptionItem roi, SiteItem sii, SoilItem soi)
        {
            var fileID = GetFileID();
            if (fileID == FileContainer.ManagementID)
            {
                return mi;
            }
            if (fileID == FileContainer.RunOptionID)
            {
                return roi;
            }
            if (fileID == FileContainer.SiteID)
            {
                return sii;
            }
            if (fileID == FileContainer.SoilID)
            {
                return soi;
            }
            throw new RunException(ParentRunItem.Name, "Unknow file ID : " + fileID);
        }

        private string GetPropertyName()
        {
            var indexDot = parameterID.LastIndexOf('.');
            if (indexDot < 1 || indexDot == parameterID.Length) throw new RunException(ParentRunItem.Name, "Parameter not found : " + parameterID);
            return parameterID.Substring(indexDot + 1);
        }

        public double GetValue(ManagementItem mi, CropParameterItem pi, RunOptionItem roi, SiteItem sii, SoilItem soi, CropParameterItem vi)
        {
            double result;
            var fileID = GetFileID();
            var propertyName = GetPropertyName();
            

            if (fileID == "Crop")
            {
                //look in non varietal file
                if (pi.paramValue.ContainsKey(propertyName)) { result = pi.paramValue[propertyName];}
                else
                {
                    //look in varietal file
                    if (vi.paramValue.ContainsKey(propertyName)) { result = vi.paramValue[propertyName];}
                    else     
                    {
                        throw new Exception("Parameter " + propertyName + " was not found in the varietal or non varietal files");
                    }
                }
            }
            else
            {
                var item = GetItem(mi, roi, sii, soi);
                if (propertyInfo == null) propertyInfo = item.GetType().GetProperty(propertyName);
                result = (double)propertyInfo.GetValue(item, null);
            }

            return result ;
        }

        public void SetValue(ManagementItem mi, CropParameterItem pi, RunOptionItem roi, SiteItem sii, SoilItem soi, CropParameterItem vi, double value)
        {
            var fileID =GetFileID();
            var propertyName = GetPropertyName();

            if (fileID == "Crop")
            {
                //look in non varietal file
                if (pi.paramValue.ContainsKey(propertyName)) { pi.paramValue[propertyName]=value; }
                else
                {
                    //look in varietal file
                    if (vi.paramValue.ContainsKey(propertyName)) { vi.paramValue[propertyName] = value; }
                    else
                    {
                        throw new Exception("Parameter " + propertyName + " was not found in the varietal or non varietal files");
                    }
                }
            }
            else
            {
                var item = GetItem(mi, roi, sii, soi);
                if (propertyInfo == null) propertyInfo = item.GetType().GetProperty(propertyName);
                propertyInfo.SetValue(item, value, null);
            }
        }

        public void SetStep(int step, ManagementItem mi, CropParameterItem pi, RunOptionItem roi, SiteItem sii, SoilItem soi, CropParameterItem vi)
        {
            Step = step;
            switch (Mode)
            {
                case SensitivityMode.RegularMinMax:
                    SetValue(mi, pi, roi, sii, soi, vi, Min + Step * (Max - Min) / ((double)NbStep - 1));
                    break;
                case SensitivityMode.RegularPercent:
                    var minValue = Min * InitialValue / 100.0;
                    var maxValue = Max * InitialValue / 100.0;
                    SetValue(mi, pi, roi, sii, soi, vi, minValue + Step * (maxValue - minValue) / ((double)NbStep - 1));
                    break;
            }
        }

        public string GetHeader()
        {
            return GetPropertyName();
        }

        #region IProjectItem Members

        public override bool NotifyPropertyChanged(String info)
        {
            if (RunItemModeSensitivityParent != null && RunItemModeSensitivityParent.RunItem != null && RunItemModeSensitivityParent.RunItem.ProjectDataFileParent != null) RunItemModeSensitivityParent.RunItem.ProjectDataFileParent.IsModified = true;
            return base.NotifyPropertyChanged(info);
        }

        public override void CheckWarnings()
        {
            NbStep = NbStep;
        }

        #endregion

        public override string WarningFileID
        {
            get
            {
                return (ParentRunItem != null && ParentRunItem.ProjectDataFileParent != null) ? ParentRunItem.ProjectDataFileParent.ID : "?";
            }
        }

        public override string WarningItemName
        {
            get
            {
                return (ParentRunItem != null) ? ParentRunItem.Name + " Sensitivity step " + parameterID : "?";
            }
        }
    }

    [Serializable]
    public class RunItemModeSensitivity : RunItemModeContainer1Child<SensitivityRunItem, int>
    {
        public const string VarDeltaSensitivity = "$(DeltaSensitivity_)";

        private bool exportNormalRuns;
        private bool oneByOne;
        private string dailyOutputPattern;
        private string oldDailyOutputFileName = null;

        public RunItemModeSensitivity()
        {
            OutputPattern = OutputPatternVarDef.VarVVer + OutputPatternVarDef.VarRunItem;
            dailyOutputPattern = OutputPatternVarDef.VarVVer + VarDeltaSensitivity + OutputPatternVarDef.VarManagementItem + "_" + RunItemModeMulti.VarMultiYearSowingYear;
            exportNormalRuns = false;
            oneByOne = true;
        }

        [XmlIgnore]
        public BindingList<SensitivityRunItem> SensitivityRuns
        {
            get { return BindingItems1; }
        }

        [Browsable(false)]
        public SensitivityRunItem[] SensitivityRunsArray
        {
            get { return BindingItemsArray1; }
            set { BindingItemsArray1 = value; }
        }

        public bool ExportNormalRuns
        {
            get { return exportNormalRuns; }
            set { this.SetStruct(ref exportNormalRuns, ref value, "ExportNormalRuns"); }
        }

        public string DailyOutputPattern
        {
            get { return dailyOutputPattern; }
            set
            {
                if (this.SetObject(ref dailyOutputPattern, ref value, "DailyOutputPattern"))
                {
                    if (RunItem != null) RunItem.NotifyPropertyChanged(ModeID + "DailyOutputPattern");
                }
            }
        }

        public bool OneByOne
        {
            get { return oneByOne; }
            set
            {
                if (this.SetStruct(ref oneByOne, ref value, "OneByOne"))
                {

                }
            }
        }

        #region iterate definition

        int nbSensitivityStep;

        public override int InitRun(bool serialize)
        {
         
            ///<Behnam 92015.12.08)>
            #region get items
            string miName, piName, riName, roiName, siiName, soiName, viName,exName;
            BindingList<MultiRunItem> multiRuns = RunItem.Multi.MultiRuns;
            var multiYear = RunItem.Multi.MultiYear;

            if (multiRuns.Count > 0)
            {
                var item = multiRuns[0];
                miName = item.ManagementItemSelected;
                piName = item.ParameterItemSelected;
                riName = RunItem.Name;
                roiName = item.RunOptionItemSelected;
                siiName = item.SiteItemSelected;
                soiName = item.SoilItemSelected;
                viName = item.VarietyItemSelected;
                exName = item.ExperimentSelected;
            }
            else
            {
                miName = RunItem.Normal.ManagementItem;
                piName = RunItem.Normal.ParameterItem;
                riName = RunItem.Name;
                roiName = RunItem.Normal.RunOptionItem;
                siiName = RunItem.Normal.SiteItem;
                soiName = RunItem.Normal.SoilItem;
                viName = RunItem.Normal.VarietyItem;
                exName = RunItem.Normal.ExperimentItem;
            }

            ManagementItem mi = null;
            CropParameterItem pi = null;
            RunOptionItem roi = null;
            SiteItem sii = null;
            SoilItem soi = null;
            CropParameterItem vi = null;
            RunCore.GetItems(riName, miName, piName, roiName, siiName, soiName, viName, ref mi, ref pi, ref roi, ref sii, ref soi, ref vi);

            mi = mi.Clone();
            pi = pi.Clone();
            roi = roi.Clone();
            sii = sii.Clone();
            soi = soi.Clone();
            vi = vi.Clone();

            switch (roi.OutputPattern)
            {
                case "V13": ProjectFile.OutputVersion = OutputVersion.V13; break;
                case "V15": ProjectFile.OutputVersion = OutputVersion.V15; break;
                case "Cus": ProjectFile.OutputVersion = OutputVersion.Cus; break;
                case "Maize": ProjectFile.OutputVersion = OutputVersion.Maize; break;
            }
            #endregion
            ///</Behnam>

            if (RunItem.Multi.MultiYear && RunItem.Multi.FirstYear > RunItem.Multi.LastYear) throw new RunException(RunItem.Name, "First year (" + RunItem.Multi.FirstYear + ") must be <= to last year (" + RunItem.Multi.LastYear + ")");

            var deltaHeader = new string[SensitivityRuns.Count];
            var index = 0;

            if (oneByOne)
            {
                nbSensitivityStep = 1;
                SensitivityRuns.Iterate(
                    delegate(SensitivityRunItem item)
                    {
                        nbSensitivityStep += item.NbStep;
                        deltaHeader[index++] = item.GetHeader();
                    });
            }
            else
            {
                nbSensitivityStep = 1;
                SensitivityRuns.Iterate(
                    delegate(SensitivityRunItem item)
                    {
                        nbSensitivityStep *= item.NbStep;
                        deltaHeader[index++] = item.GetHeader();
                    });
            }

            if (Book != null) Book.Clear();
            else Book = new Book();
            var page = OutputFile.ExtractSensitivityRunHeader(deltaHeader);
            page.Title = OutputFileName;
            Book.Add(page);
            return Math.Max(1, RunItem.Multi.MultiRuns.Count);
        }

        public override void StepRun(bool serialize, int i, string variety = null, string parameterName = null, double parameterValue = 0)
        {
            BindingList<MultiRunItem> multiRuns = RunItem.Multi.MultiRuns;
            var multiYear = RunItem.Multi.MultiYear;

            #region get items

            string miName, piName, riName, roiName, siiName, soiName, viName ,exName;

            if (multiRuns.Count > 0)
            {
                var item = multiRuns[i];
                miName = item.ManagementItemSelected;
                piName = item.ParameterItemSelected;
                riName = RunItem.Name;
                roiName = item.RunOptionItemSelected;
                siiName = item.SiteItemSelected;
                soiName = item.SoilItemSelected;
                viName = item.VarietyItemSelected;
                exName = item.ExperimentSelected;
            }
            else
            {
                miName = RunItem.Normal.ManagementItem;
                piName = RunItem.Normal.ParameterItem;
                riName = RunItem.Name;
                roiName = RunItem.Normal.RunOptionItem;
                siiName = RunItem.Normal.SiteItem;
                soiName = RunItem.Normal.SoilItem;
                viName = RunItem.Normal.VarietyItem;
                exName = RunItem.Normal.ExperimentItem;
            }

            ManagementItem mi = null;
            CropParameterItem pi = null;
            RunOptionItem roi = null;
            SiteItem sii = null;
            SoilItem soi = null;
            CropParameterItem vi = null;
            RunCore.GetItems(riName, miName, piName, roiName, siiName, soiName, viName, ref mi, ref pi, ref roi, ref sii, ref soi, ref vi);

            mi = mi.Clone();
            pi = pi.Clone();
            roi = roi.Clone();
            sii = sii.Clone();
            soi = soi.Clone();
            vi = vi.Clone();

            #endregion

            ///<Behnam>
            var deltaHeader = new string[SensitivityRuns.Count];
            var index1 = 0;

            if (oneByOne)
            {
                nbSensitivityStep = 1;
                SensitivityRuns.Iterate(
                    delegate(SensitivityRunItem item)
                    {
                        nbSensitivityStep += item.NbStep;
                        deltaHeader[index1++] = item.GetHeader();
                    });
            }
            else
            {
                nbSensitivityStep = 1;
                SensitivityRuns.Iterate(
                    delegate(SensitivityRunItem item)
                    {
                        nbSensitivityStep *= item.NbStep;
                        deltaHeader[index1++] = item.GetHeader();
                    });
            }
            ///</Behnam>

            var deltaValue = new double[SensitivityRuns.Count];
            int[] index = { 0 };

            if (!multiYear)
            {
                ///<Behnam>
                switch (roi.OutputPattern)
                {
                    case "V13": ProjectFile.OutputVersion = OutputVersion.V13; break;
                    case "V15": ProjectFile.OutputVersion = OutputVersion.V15; break;
                    case "Cus": ProjectFile.OutputVersion = OutputVersion.Cus; break;
                    case "Maize": ProjectFile.OutputVersion = OutputVersion.Maize; break;
                }
                ///</Behnam>

                var yy = sii.MaxSowingDate.Year - sii.MinSowingDate.Year;
                mi.SowingDate = new DateTime(mi.SowingDate.Year, mi.SowingDate.Month, mi.SowingDate.Day);
                sii.MinSowingDate = new DateTime(mi.SowingDate.Year, sii.MinSowingDate.Month, sii.MinSowingDate.Day);
                sii.MaxSowingDate = new DateTime(mi.SowingDate.Year + yy, sii.MaxSowingDate.Month, sii.MaxSowingDate.Day);

                Run.isFirstYear = true;
                ///</Behnam>

                var end = false;
                InitStepSensitivity(mi, pi, roi, sii, soi, vi, deltaValue, index);

                var stepIndex = 0;
                while (!end)
                {
                    var excelPage = RunCore.Run(riName, exportNormalRuns, false, mi, pi, roi, sii, soi, vi);
                    var deltaSensitivity = "";

                    index[0] = 0;
                    SensitivityRuns.Iterate(
                        item =>
                        {
                            deltaValue[index[0]] = item.GetValue(mi, pi, roi, sii, soi, vi);
                            deltaSensitivity += item.GetHeader() + "_" + Math.Round(deltaValue[index[0]], 4) + "_";
                            ++index[0];
                        });

                    ///<Behnam>
                    if (i == 0 && stepIndex == 0)
                    {
                        Book.Clear();
                        var outputFile = OutputFile.ExtractSensitivityRunHeader(deltaHeader);
                        outputFile.Title = OutputFileName;
                        Book.Add(outputFile);
                    }

                    if (exportNormalRuns)
                    {
                        var dailyOutputFileName = OutputPatternVarDef.OutputPatternValue(dailyOutputPattern, miName, piName, riName, roiName, siiName, soiName, viName, exName) + RunItem.Normal.OutputExtention;
                        dailyOutputFileName = dailyOutputFileName.Replace(VarDeltaSensitivity, deltaSensitivity);
                        dailyOutputFileName = dailyOutputFileName.Replace(RunItemModeMulti.VarMultiYearSowingYear, "");
                        excelPage.Title = dailyOutputFileName;
                        Book.Add(excelPage);
                        if (serialize) RunCore.Save(excelPage, AbsoluteOutputDirectory + "\\" + dailyOutputFileName);
                    }

                    if (i == 0 && ProjectFile.OutputVersion == OutputVersion.Cus && stepIndex == 0)
                    {
                        Run.SecondLine = false;
                        Book[0].Add(OutputFile.ExtractSensitivityRunLine(RunCore.RunInstance, deltaHeader, deltaValue));
                    }
                    Book[0].Add(OutputFile.ExtractSensitivityRunLine(RunCore.RunInstance, deltaHeader, deltaValue));
                    ///</Behnam>

                    end = Increment(stepIndex, mi, pi, roi, sii, soi, vi);
                    ++stepIndex;
                }
            }
            else
            {
                ///<Behnam>
                switch (roi.OutputPattern)
                {
                    case "V13": ProjectFile.OutputVersion = OutputVersion.V13; break;
                    case "V15": ProjectFile.OutputVersion = OutputVersion.V15; break;
                    case "Cus": ProjectFile.OutputVersion = OutputVersion.Cus; break;
                    case "Maize": ProjectFile.OutputVersion = OutputVersion.Maize; break;
                }
                ///</Behnam>

                var firstYear = RunItem.Multi.FirstYear;
                var lastYear = RunItem.Multi.LastYear;

                for (var j = firstYear; j <= lastYear; ++j)
                {
                    foreach (var dateApp in mi.DateApplications)
                    {
                        dateApp.Date = new DateTime(j + (dateApp.Date.Year - mi.SowingDate.Year), dateApp.Date.Month, dateApp.Date.Day);
                    }

                    ///<Behnam>
                    var yy = sii.MaxSowingDate.Year - sii.MinSowingDate.Year;
                    mi.SowingDate = new DateTime(j, mi.SowingDate.Month, mi.SowingDate.Day);
                    sii.MinSowingDate = new DateTime(j, sii.MinSowingDate.Month, sii.MinSowingDate.Day);
                    sii.MaxSowingDate = new DateTime(j + yy, sii.MaxSowingDate.Month, sii.MaxSowingDate.Day);

                    Run.isFirstYear = (j == firstYear);
                    ///</Behnam>

                    var end = false;
                    InitStepSensitivity(mi, pi, roi, sii, soi, vi, deltaValue, index);

                    var stepIndex = 0;

                    while (!end)
                    {
                        var excelPage = RunCore.Run(riName, exportNormalRuns, false, mi, pi, roi, sii, soi, vi);
                        var deltaSensitivity = "";

                        index[0] = 0;
                        var index0 = index[0];
                        SensitivityRuns.Iterate(
                            delegate(SensitivityRunItem item)
                            {
                                deltaValue[index0] = item.GetValue(mi, pi, roi, sii, soi, vi);
                                deltaSensitivity += item.GetHeader() + "_" + Math.Round(deltaValue[index[0]], 4) + "_";
                                ++index0;

                            });

                        ///<Behnam (2016.11.12)>
                        if (i == 0 && j == firstYear && stepIndex == 0)
                        {
                            Book.Clear();
                            var outputFile = OutputFile.ExtractSensitivityRunHeader(deltaHeader);
                            outputFile.Title = OutputFileName;
                            Book.Add(outputFile);
                        }

                        if (exportNormalRuns)
                        {
                            var dailyOutputFileName = OutputPatternVarDef.OutputPatternValue(dailyOutputPattern, miName, piName, riName, roiName, siiName, soiName, viName, exName) + RunItem.Normal.OutputExtention;
                            dailyOutputFileName = dailyOutputFileName.Replace(VarDeltaSensitivity, deltaSensitivity);
                            dailyOutputFileName = dailyOutputFileName.Replace(RunItemModeMulti.VarMultiYearSowingYear, mi.FinalSowingDate.Year.ToString());
                            excelPage.Title = dailyOutputFileName;
                            Book.Add(excelPage);

                            if ((j == firstYear || (j != firstYear && oldDailyOutputFileName != dailyOutputFileName)) && stepIndex == 0)
                            {
                                if (serialize) RunCore.Save(excelPage, AbsoluteOutputDirectory + "\\" + dailyOutputFileName, true);
                            }
                            else
                            {
                                if (serialize) RunCore.Save(excelPage, AbsoluteOutputDirectory + "\\" + dailyOutputFileName, false);
                            }
                            oldDailyOutputFileName = dailyOutputFileName;
                        }

                        if (i == 0 && j == firstYear && ProjectFile.OutputVersion == OutputVersion.Cus && stepIndex == 0)
                        {
                            Run.SecondLine = false;
                            Book[0].Add(OutputFile.ExtractSensitivityRunLine(RunCore.RunInstance, deltaHeader, deltaValue));
                        }
                        Book[0].Add(OutputFile.ExtractSensitivityRunLine(RunCore.RunInstance, deltaHeader, deltaValue));
                        ///</Behnam>

                        end = Increment(stepIndex, mi, pi, roi, sii, soi, vi);
                        ++stepIndex;
                    }
                }
            }
        }

        private void InitStepSensitivity(ManagementItem mi, CropParameterItem pi, RunOptionItem roi, SiteItem sii, SoilItem soi, 
            CropParameterItem vi, double[] deltaValue, int[] index)
        {
            if (oneByOne)
            {
                foreach (var sri in SensitivityRuns)
                {
                    var initialValue = sri.GetValue(mi, pi, roi, sii, soi, vi);
                    sri.InitialValue = initialValue;
                    sri.Step = 0;
                    sri.SetValue(mi, pi, roi, sii, soi, vi, initialValue);
                    deltaValue[index[0]] = initialValue;
                }
            }
            else
            {
                foreach (var sri in SensitivityRuns)
                {
                    var initialValue = sri.GetValue(mi, pi, roi, sii, soi, vi);
                    sri.InitialValue = initialValue;
                    sri.SetStep(0, mi, pi, roi, sii, soi, vi);
                    deltaValue[index[0]] = sri.InitialValue;
                }
            }
        }

        private bool Increment(int stepIndex, ManagementItem mi, CropParameterItem pi, RunOptionItem roi, SiteItem sii, SoilItem soi, CropParameterItem vi)
        {
            if (oneByOne)
            {
                var sensitivityRunCount = SensitivityRuns.Count;
                var found = false;
                var i = 0;
                while (!found && i < sensitivityRunCount)
                {
                    var sensitivityRun = SensitivityRuns[i];
                    var sensitivityRunNbStep = sensitivityRun.NbStep;
                    if (stepIndex < sensitivityRunNbStep)
                    {
                        sensitivityRun.SetStep(stepIndex, mi, pi, roi, sii, soi, vi);
                        found = true;
                    }
                    else
                    {
                        if (stepIndex == sensitivityRunNbStep)
                        {
                            sensitivityRun.SetValue(mi, pi, roi, sii, soi, vi, sensitivityRun.InitialValue);
                        }
                        stepIndex -= sensitivityRunNbStep;
                    }
                    ++i;
                }
                return !found;
            }
            else
            {
                var end = true;
                var i = SensitivityRuns.Count - 1;

                while (i >= 0 && end)
                {
                    var sensitivityRun = SensitivityRuns[i];
                    var sensitivityRunStep = sensitivityRun.Step;
                    if (sensitivityRunStep == sensitivityRun.NbStep - 1)
                    {
                        sensitivityRun.SetStep(0, mi, pi, roi, sii, soi, vi);
                    }
                    else
                    {
                        sensitivityRun.SetStep(sensitivityRunStep + 1, mi, pi, roi, sii, soi, vi);
                        end = false;
                    }
                    --i;
                }
                return end;
            }
        }

        public override void EndRun(bool serialize)
        {
            Book = Book;
            if (serialize) RunCore.Save(Book[0], AbsoluteOutputPath);
            RunCore.RunInstance.DoStop();
        }



        #endregion

        #region output path

        public override string OutputFileNameWithoutExtension
        {
            get
            {
                var outputName = OutputPatternVarDef.OutputPatternValue(OutputPattern, RunItem.Normal.ManagementItem, RunItem.Normal.ParameterItem, RunItem.Name, RunItem.Normal.RunOptionItem, RunItem.Normal.SiteItem, RunItem.Normal.SoilItem, RunItem.Normal.VarietyItem, RunItem.Normal.ExperimentItem);
                return outputName;
            }
        }

        public override string OutputExtention
        {
            get { return ".sqsrs"; }
        }

        #endregion

        #region mode definition

        public override string ModeID
        {
            get { return "Sensitivity"; }
        }

        #endregion
    }
}
