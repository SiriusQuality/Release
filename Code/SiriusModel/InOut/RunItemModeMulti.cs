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
    public class MultiRunItem : Child<int>
    {
        #region override normal items definition

        private string managementItem;
        private string parameterItem;
        private string runOptionItem;
        private string siteItem;
        private string soilItem;
        private string varietyItem;
        private string experimentItem;

        public string ManagementItem 
        { 
            get { return managementItem; }
            set { this.SetObject(ref managementItem, ref value, "ManagementItem"); }
        }

        public string ParameterItem 
        {
            get { return parameterItem; }
            set { this.SetObject(ref parameterItem, ref value, "ParameterItem"); }
        }

        public string RunOptionItem 
        {
            get { return runOptionItem; }
            set { this.SetObject(ref runOptionItem, ref value, "RunOptionItem"); }
        }
        public string SiteItem 
        {
            get { return siteItem; }
            set { this.SetObject(ref siteItem, ref value, "SiteItem"); }
        }
        public string SoilItem 
        {
            get { return soilItem; }
            set { this.SetObject(ref soilItem, ref value, "SoilItem"); }
        }

        public string VarietyItem 
        {
            get { return varietyItem; }
            set { this.SetObject(ref varietyItem, ref value, "VarietyItem"); }
        }

        public string ExperimentItem
        {
            get { return experimentItem; }
            set { this.SetObject(ref experimentItem, ref value, "ExperimentItem"); }
        }

        

        [XmlIgnore, Browsable(false)]
        public string ManagementItemSelected { get { return (!String.IsNullOrEmpty(ManagementItem)) ? ManagementItem : RunItemModeMultiParent.RunItem.Normal.ManagementItem; } }

        [XmlIgnore, Browsable(false)]
        public string ParameterItemSelected { get { return (!String.IsNullOrEmpty(ParameterItem)) ? ParameterItem : RunItemModeMultiParent.RunItem.Normal.ParameterItem; } }

        [XmlIgnore, Browsable(false)]
        public string RunOptionItemSelected { get { return (!String.IsNullOrEmpty(RunOptionItem)) ? RunOptionItem : RunItemModeMultiParent.RunItem.Normal.RunOptionItem; } }

        [XmlIgnore, Browsable(false)]
        public string SiteItemSelected { get { return (!String.IsNullOrEmpty(SiteItem)) ? SiteItem : RunItemModeMultiParent.RunItem.Normal.SiteItem; } }

        [XmlIgnore, Browsable(false)]
        public string SoilItemSelected { get { return (!String.IsNullOrEmpty(SoilItem)) ? SoilItem : RunItemModeMultiParent.RunItem.Normal.SoilItem; } }

        [XmlIgnore, Browsable(false)]
        public string VarietyItemSelected { get { return (!String.IsNullOrEmpty(VarietyItem)) ? VarietyItem : RunItemModeMultiParent.RunItem.Normal.VarietyItem; } }

        [XmlIgnore, Browsable(false)]
        public string ExperimentSelected { get { return (!String.IsNullOrEmpty(ExperimentItem)) ? ExperimentItem : RunItemModeMultiParent.RunItem.Normal.ExperimentItem; } }

        #endregion

        private RunItemModeMulti RunItemModeMultiParent
        {
            get { return Parent.Cast<RunItemModeMulti>(); }
        }

        [XmlIgnore]
        public string DailyOutputFileName
        {
            get 
            {
                var outputFileName = OutputPatternVarDef.OutputPatternValue(RunItemModeMultiParent.DailyOutputPattern, ManagementItemSelected, ParameterItemSelected, RunItemModeMultiParent.RunItem.Name, RunOptionItemSelected, SiteItemSelected, SoilItemSelected, VarietyItemSelected, ExperimentSelected);
                outputFileName = outputFileName.Replace(RunItemModeMulti.VarMultiYearSowingYear, (RunItemModeMultiParent.MultiYear) ? "yyyy" : "");
                return outputFileName + RunItemModeMultiParent.RunItem.Normal.OutputExtention;
            }
        }


        public MultiRunItem()
        {
            ManagementItem = "";
            ParameterItem = "";
            RunOptionItem = "";
            SiteItem = "";
            SoilItem = "";
            VarietyItem = "";
        }

        #region IProjectItem Members

        public override bool NotifyPropertyChanged(String info)
        {
            if (RunItemModeMultiParent != null && RunItemModeMultiParent.RunItem != null && RunItemModeMultiParent.RunItem.ProjectDataFileParent != null) RunItemModeMultiParent.RunItem.ProjectDataFileParent.IsModified = true;
            if (base.NotifyPropertyChanged(info))
            {
                if (info != "DailyOutputFileName")
                {
                    NotifyPropertyChanged("DailyOutputFileName");
                }
                return true;
            }
            return false;
        }

        public override void CheckWarnings()
        {
            
        }

        #endregion
    }

    [Serializable]
    public class RunItemModeMulti :  RunItemModeContainer1Child<MultiRunItem, int>
    {
        public static readonly string VarMultiYearSowingYear = "$(MultiYear?SowingYear)";
        private bool exportNormalRuns;
        private string dailyOutputPattern;
        private bool multiYear;
        private int firstYear;
        private int lastYear;
        string oldDailyOutputFileName = null;

        public RunItemModeMulti()
        {
            Book = new Book();
            DailyOutputPattern = OutputPatternVarDef.VarVVer + OutputPatternVarDef.VarManagementItem + "_" + VarMultiYearSowingYear;
            OutputPattern = OutputPatternVarDef.VarVVer + OutputPatternVarDef.VarRunItem;
            exportNormalRuns = false;
        }

        #region multi mode parameters

        [XmlIgnore]
        public BaseBindingList<MultiRunItem> MultiRuns 
        {
            get { return BindingItems1; }
        }

        [Browsable(false)]
        public MultiRunItem[] MultiRunsArray
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

        public bool MultiYear
        {
            get { return multiYear; }
            set 
            {
                if (this.SetStruct(ref multiYear, ref value, "MultiYear"))
                {
                    foreach (var runItem in MultiRuns)
                    {
                        runItem.NotifyPropertyChanged("DailyOutputFileName");
                    }
                }
            }
        }

        public int FirstYear
        {
            get { return firstYear; }
            set { this.SetStruct(ref firstYear, ref value, "FirstYear"); }
        }

        public int LastYear
        {
            get { return lastYear; }
            set { this.SetStruct(ref lastYear, ref value, "LastYear"); }
        }
        
        #endregion 

        #region iterate definition

        public override int InitRun(bool serialize)
        {
            if (multiYear && firstYear > lastYear) throw new RunException(RunItem.Name, "First year (" + firstYear + ") must be <= to last year (" + lastYear + ")");

            // Book.Clear();
            // var outputFile = OutputFile.ExtractMultiRunHeader();
            // outputFile.Title = OutputFileName;
            // Book.Add(outputFile);
            return Math.Max(1, MultiRuns.Count);
        }
        PageData warningFile = new PageData();
        public override void StepRun(bool serialize, int i, string variety = null, string parameterName = null, double parameterValue = 0)
        {
            #region get items

            string miName, piName, riName, roiName, siiName, soiName, viName, exName;

            if (MultiRuns.Count > 0)
            {
                var item = MultiRuns[i];
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

            #endregion 

            #region override some parameters

            if (variety != null)
            {
                if (variety == viName)
                {
                        vi.ParamValue[parameterName] = parameterValue;
                }
            }

            #endregion
            
            if (!multiYear)
            {
                switch (roi.OutputPattern)
                {
                    case "V13": ProjectFile.OutputVersion = OutputVersion.V13; break;
                    case "V15": ProjectFile.OutputVersion = OutputVersion.V15; break;
                    case "Cus": ProjectFile.OutputVersion = OutputVersion.Cus; break;
                    case "Maize": ProjectFile.OutputVersion = OutputVersion.Maize; break;
                }

                ///<Behnam>
                ///<Comment>
                ///Modifications related to estimating sowing window and forcing the model
                ///to put daily outputs into one file if the names of the daily output files
                ///are identical for two successive years.
                ///</Comment>                

                var yy = sii.MaxSowingDate.Year - sii.MinSowingDate.Year;
                mi.SowingDate = new DateTime(mi.SowingDate.Year, mi.SowingDate.Month, mi.SowingDate.Day);
                sii.MinSowingDate = new DateTime(mi.SowingDate.Year, sii.MinSowingDate.Month, sii.MinSowingDate.Day);
                sii.MaxSowingDate = new DateTime(mi.SowingDate.Year + yy, sii.MaxSowingDate.Month, sii.MaxSowingDate.Day);
                
                Run.isFirstYear = true;
                ///</Behnam>
                //Debug
                var excelPage = RunCore.Run(riName, exportNormalRuns, false, mi, pi, roi, sii, soi, vi);

                ///<Behnam>
                if (i == 0)
                {
                    Book.Clear();
                    var outputFile = OutputFile.ExtractMultiRunHeader();
                    outputFile.Title = OutputFileName;
                    Book.Add(outputFile);

                }


                if (exportNormalRuns)
                {
                    var dailyOutputFileName = OutputPatternVarDef.OutputPatternValue(dailyOutputPattern, miName, piName, riName, roiName, siiName, soiName, viName, exName) + RunItem.Normal.OutputExtention;
                    dailyOutputFileName = dailyOutputFileName.Replace(VarMultiYearSowingYear, "");
                    excelPage.Title = dailyOutputFileName;
                    Book.Add(excelPage);
                    if (serialize) RunCore.Save(excelPage, AbsoluteOutputDirectory + "\\" + dailyOutputFileName);


                    if (i == 0)
                    {
                        warningFile = OutputFile.ExtractWarningsHeader();
                        warningFile.Title = "warnings";
                    }

                    LineData line = OutputFile.ExtractWarningsMultiRun(RunCore.RunInstance);
                    if (line != null) warningFile.Add(line);

                }

                if (i == 0 && ProjectFile.OutputVersion == OutputVersion.Cus)
                {
                    Run.SecondLine = false;
                    Book[0].Add(OutputFile.ExtractMultiRunLine(RunCore.RunInstance));
                }
                ///</Behnam>
                Book[0].Add(OutputFile.ExtractMultiRunLine(RunCore.RunInstance));

                /// Behnam (2016.06.01): Added to write the summaty file after each run is finished.
                /// Other way, if one simulations is not completed, the summary file is not pronted at all.
                if (serialize) RunCore.Save(Book[0], AbsoluteOutputPath);

            }
            else 
            {
                

                mi = mi.Clone();

                ///<Behnam>
                switch (roi.OutputPattern)
                {
                    case "V13": ProjectFile.OutputVersion = OutputVersion.V13; break;
                    case "V15": ProjectFile.OutputVersion = OutputVersion.V15; break;
                    case "Cus": ProjectFile.OutputVersion = OutputVersion.Cus; break;
                    case "Maize": ProjectFile.OutputVersion = OutputVersion.Maize; break;
                }

                for (var j = firstYear; j <= lastYear; ++j)
                {
                    foreach (var dateApp in mi.DateApplications)
                    {
                        dateApp.Date = new DateTime(j + (dateApp.Date.Year - mi.SowingDate.Year), dateApp.Date.Month, dateApp.Date.Day);
                    }
                    
                    var yy = sii.MaxSowingDate.Year - sii.MinSowingDate.Year;
                    mi.SowingDate = new DateTime(j, mi.SowingDate.Month, mi.SowingDate.Day);
                    sii.MinSowingDate = new DateTime(j, sii.MinSowingDate.Month, sii.MinSowingDate.Day);
                    sii.MaxSowingDate = new DateTime(j + yy, sii.MaxSowingDate.Month, sii.MaxSowingDate.Day);

                    Run.isFirstYear = (j == firstYear);
                    
                    var excelPage = RunCore.Run(riName, exportNormalRuns, false, mi, pi, roi, sii, soi, vi);

                    if (i == 0 && j == firstYear)
                    {
                        Book.Clear();
                        var outputFile = OutputFile.ExtractMultiRunHeader();
                        outputFile.Title = OutputFileName;
                        Book.Add(outputFile);

                    }

                    if (exportNormalRuns)
                    {
                        var dailyOutputFileName = OutputPatternVarDef.OutputPatternValue(dailyOutputPattern, miName, piName, riName, roiName, siiName, soiName, viName, exName) + RunItem.Normal.OutputExtention;
                        dailyOutputFileName = dailyOutputFileName.Replace(VarMultiYearSowingYear, sii.MinSowingDate.Year.ToString());
                        excelPage.Title = dailyOutputFileName;
                        Book.Add(excelPage);

                        if (j == firstYear || (j != firstYear && oldDailyOutputFileName != dailyOutputFileName)) 
                        {
                            if (serialize) RunCore.Save(excelPage, AbsoluteOutputDirectory + "\\" + dailyOutputFileName, true);
                        }
                        else 
                        {
                            if (serialize) RunCore.Save(excelPage, AbsoluteOutputDirectory + "\\" + dailyOutputFileName, false);
                        }
                        oldDailyOutputFileName = dailyOutputFileName;
                    }

                    if (i == 0 && j == firstYear && ProjectFile.OutputVersion == OutputVersion.Cus)
                    {
                        Run.SecondLine = false;
                        Book[0].Add(OutputFile.ExtractMultiRunLine(RunCore.RunInstance));
                    }
                    Book[0].Add(OutputFile.ExtractMultiRunLine(RunCore.RunInstance));

                    /// Behnam (2016.06.01): Added to write the summaty file after each run is finished.
                    /// Other way, if one simulations is not completed, the summary file is not pronted at all.
                    if (serialize) RunCore.Save(Book[0], AbsoluteOutputPath);
                    ///</Behnam>

                    //Warnings
                    if (i == 0 && j == firstYear)
                    {
                        warningFile = OutputFile.ExtractWarningsHeader();
                        warningFile.Title = "warnings";
                    }

                        LineData line = OutputFile.ExtractWarningsMultiRun(RunCore.RunInstance);
                        if (line!=null) warningFile.Add(line);

                    

                }

            }
            if (i == (MultiRuns.Count - 1)) Book.Add(warningFile);
        }


     

        public override void EndRun(bool serialize)
        {
            /// Behnam (2016.06.01): Commented to write the summaty file after each run is finished.
            // if (serialize) RunCore.Save(Book[0], AbsoluteOutputPath);
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
            get { return ".sqbrs"; }
        }

        #endregion 

        #region mode definition

        public override string ModeID
        {
            get { return "Multi"; }
        }

        #endregion 
    }
}