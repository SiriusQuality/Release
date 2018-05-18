using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using SiriusModel.InOut;
using SiriusModel.InOut.Base;
using System.Diagnostics;

namespace SiriusView.File
{
    public partial class RunForm : ProjectDataFileForm
    {
        private readonly List<TabPage> hidenPages;
        private readonly List<TabPage> toHidePages;
        private readonly List<TabPage> toShowPages;

        private int lastNormalSelectedTabIndex;
        private int lastMultiSelectedTabIndex;
        private int lastSensitivitySelectedTabIndex;

        public RunForm()
            : base(FileContainer.RunID)
        {
            hidenPages = new List<TabPage>();
            toHidePages = new List<TabPage>();
            toShowPages = new List<TabPage>();

            lastNormalSelectedTabIndex = 0;
            lastMultiSelectedTabIndex = 0;
            lastSensitivitySelectedTabIndex = 0;

            InitializeComponent();
        }

        public override string BaseFormID()
        {
            return "Run";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            runItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.RunFile.Items;
            managementItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.ManagementFile.ItemsSelectable;
            parameterItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.NonVarietyFile.ItemsSelectable;
            runOptionItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.RunOptionFile.ItemsSelectable;
            siteItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.SiteFile.ItemsSelectable;
            soilItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.SoilFile.ItemsSelectable;
            varietyItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.VarietyFile.ItemsSelectable;
            experimentNameBindingSourceRun.DataSource = ProjectFile.This.FileContainer.ManagementFile.ItemsSelectable.FilterPossibleStringValues("ExperimentName");
                    
            parameterIDTextBoxColumn7.Items.AddRange((object[])SensitivityRunItem.GetParameterIDs());
            foreach (var modeValue in Enum.GetValues(typeof(SensitivityMode)))
            {
                modeDataGridViewTextBoxColumn.Items.Add(modeValue);
            }
            runItemsBindingSource1.ResetBindings(false);
            radioButtonNormal.Checked = true;
            runItemsBindingSource1_CurrentChanged(this, null);



        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        private void radioButtonMode_CheckedChanged(object sender, EventArgs e)
        {
            tabControlMode.Selecting -= tabControlMode_Selecting;
            tabControlMode.SuspendLayout();
            var isNormal = radioButtonNormal.Checked == true;
            var isMulti = radioButtonMulti.Checked == true;
            var isSensitivity = radioButtonSensitivity.Checked == true;
            toHidePages.Clear(); toShowPages.Clear();
            foreach (TabPage tabPage in tabControlMode.TabPages)
            {
                CheckVisible(isNormal, isMulti, isSensitivity, tabPage);
                if ((string) tabPage.Tag == "All")
                {
                    //to make sure that the graph tab stays at the end 
                    tabControlMode.TabPages.Remove(tabPage);
                    hidenPages.Add(tabPage);
                }
            }
            foreach (var tabPage in hidenPages)
            {
                CheckVisible(isNormal, isMulti, isSensitivity, tabPage);
                if ((string) tabPage.Tag == "All")
                {
                    //to make sure that the graph tab stays at the end 
                    toShowPages.Add(tabPage);
                }
            }
            foreach (var tabPage in toHidePages)
            {
                hidenPages.Add(tabPage);
                tabControlMode.TabPages.Remove(tabPage);
            }
            foreach (var tabPage in toShowPages)
            {
                hidenPages.Remove(tabPage);
                tabControlMode.TabPages.Add(tabPage);
            }
            if (isNormal) tabControlMode.SelectedIndex = lastNormalSelectedTabIndex;
            if (isMulti) tabControlMode.SelectedIndex = lastMultiSelectedTabIndex;
            if (isSensitivity) tabControlMode.SelectedIndex = lastSensitivitySelectedTabIndex;
            tabControlMode.ResumeLayout();
            tabControlMode.Selecting += tabControlMode_Selecting;
            
        }

        private void CheckVisible(bool isNormal, bool isMulti, bool isSensitivity, TabPage tabPage)
        {
            var tag = (string)tabPage.Tag;
            switch (tag)
            {
                case "Normal":
                    tabPage.CheckVisible(isNormal, tabControlMode, toHidePages, toShowPages);
                    break;
                case "Multi":
                    tabPage.CheckVisible(isMulti, tabControlMode, toHidePages, toShowPages);
                    break;
                case "Sensitivity":
                    tabPage.CheckVisible(isSensitivity, tabControlMode, toHidePages, toShowPages);
                    break;
                case "All":
                    break;
                default:
                    throw new Exception("Unknown mode : " + tag);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            runItemsBindingSource1.Add(new RunItem("new simple run"));
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (runItemsBindingSource1.Current != null)
            {
                runItemsBindingSource1.RemoveCurrent();
            }
            else buttonDelete.UpdateEnabled(false);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            runItemsBindingSource1.Clear();
        }

        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            var clone = (RunItem)runItemsBindingSource1.Current.Clone();
            clone.Name += "_dup";
            runItemsBindingSource1.Add(clone);
            runItemsBindingSource1.EndEdit();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            //creer les full liste de varietaux et non varietaux
            Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch

            var currentRun = runItemsBindingSource1.Current as RunItem;

            if (currentRun != null)
            {
                if (radioButtonNormal.Checked)
                {
                    MainForm.This.WorkerControl.Run(RunMode.NormalRun, currentRun.Name);
                }
                else if (radioButtonMulti.Checked)
                {
                    MainForm.This.WorkerControl.Run(RunMode.MultiRun, currentRun.Name);
                }
                else if (radioButtonSensitivity.Checked)
                {
                    MainForm.This.WorkerControl.Run(RunMode.SensitivityRun, currentRun.Name);
                }
            }
            stopwatch.Stop();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            OutputFileExtractorCus.summaryHeading = true;

            var currentRun = runItemsBindingSource1.Current as RunItem;
            if (currentRun != null)
            {
                IRunItemModeContainer rimc;
                RunMode runMode;

                if (radioButtonNormal.Checked)
                {
                    rimc = currentRun.Normal;
                    runMode = RunMode.NormalRunAndExport;
                }
                else if (radioButtonMulti.Checked)
                {
                    rimc = currentRun.Multi;
                    runMode = RunMode.MultiRunAndExport;
                }
                else if (radioButtonSensitivity.Checked)
                {
                    rimc = currentRun.Sensitivity;
                    runMode = RunMode.SensitivityRunAndExport;
                }
                else throw new NotImplementedException();

                if (rimc != null)
                {
                    if (!Directory.Exists(rimc.AbsoluteOutputDirectory))
                    {
                        folderBrowserDialog1.Description = "Select " + rimc.ModeID + " " + currentRun.Name + " output directory";
                        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                        {
                            rimc.AbsoluteOutputDirectory = folderBrowserDialog1.SelectedPath;
                            MainForm.This.WorkerControl.Run(runMode, currentRun.Name);
                        }
                    }
                    else
                    {
                        MainForm.This.WorkerControl.Run(runMode, currentRun.Name);
                    }
                }
            }
        }

        private void runItemsBindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            var enabled = runItemsBindingSource1.Current != null;
            buttonDelete.UpdateEnabled(enabled);
            buttonRun.UpdateEnabled(enabled);
            buttonExport.UpdateEnabled(enabled);
            buttonDuplicate.UpdateEnabled(enabled);
            splitContainer1.Panel2.UpdateEnabled(enabled);
            if (enabled)
            {         
                if (radioButtonNormal.Checked)//single run only
                {

                    //set the combo boxes state
                    RunItem currentNormalRun = (RunItem)runItemsBindingSource1.Current;
                    string savedManagement = currentNormalRun.Normal.ManagementItem;
                    string savedParameter = currentNormalRun.Normal.ParameterItem;
                    string savedVariety = currentNormalRun.Normal.VarietyItem;

                    if ((string)experimentNameComboBoxRunSingle.SelectedItem == (string)currentNormalRun.Normal.ExperimentItem)
                    {
                        //in case the selected item is already the right one we need to explicitely call SelectindexChanged
                        experimentNameComboBoxRunSingle_SelectedIndexChanged(experimentNameComboBoxRunSingle, null);
                    }
                    else
                    {
                        experimentNameComboBoxRunSingle.SelectedItem = currentNormalRun.Normal.ExperimentItem;
                    }

                    updateCurrentManagement(savedManagement);
                    comboBox1.SelectedItem = managementItemsBindingSource1.Current;

                    updateCurrentParameter(savedParameter);
                    comboBox2.SelectedItem = parameterItemsBindingSource1.Current;

                    updateCurrentVariety(savedVariety);
                    comboBox6.SelectedItem = varietyItemsBindingSource1.Current;
                }
            }
        }

        private void chekedvalued(object sender, EventArgs e)
        {
            textBox10.Enabled = checkBox2.Checked;
        }
        private void outputDirectory_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var mode = (string)(((LinkLabel)sender).Tag);
            var currentRun = runItemsBindingSource1.Current as RunItem;
            if (currentRun == null) return;

            IRunItemModeContainer rimc;
            switch (mode)
            {
                case "Normal":
                    rimc = currentRun.Normal;
                    break;
                case "Multi":
                    rimc = currentRun.Multi;
                    break;
                case "Sensitivity":
                    rimc = currentRun.Sensitivity;
                    break;
                default:
                    throw new NotImplementedException();
            }

            folderBrowserDialog1.Description = "Select " + rimc.ModeID + " " + currentRun.Name + " output directory";
            if (Directory.Exists(rimc.AbsoluteOutputDirectory))
            {
                folderBrowserDialog1.SelectedPath = rimc.AbsoluteOutputDirectory;
            }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                rimc.AbsoluteOutputDirectory = folderBrowserDialog1.SelectedPath;
            }
        }

        private void dataGridView1_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var currentRun = (RunItem) runItemsBindingSource1.Current;
            var opnf = new OutputPatternForm
                           {
                               Pattern = currentRun.Normal.OutputPattern, 
                               Text = "Edit single run daily output file pattern (" + currentRun.Name + ")", 
                               IsNormalEdit = true
                           };
            if (opnf.ShowDialog() == DialogResult.OK)
            {
                currentRun.Normal.OutputPattern = opnf.Pattern;
            }
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var currentRun = (RunItem)runItemsBindingSource1.Current;
            var opnf = new OutputPatternForm();
            opnf.Pattern = currentRun.Multi.OutputPattern;
            opnf.Text = "Edit multi run summary output file pattern (" + currentRun.Name + ")";
            opnf.IsMultiSummaryEdit = true;
            if (opnf.ShowDialog() == DialogResult.OK)
            {
                currentRun.Multi.OutputPattern = opnf.Pattern;
            }
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var currentRun = (RunItem)runItemsBindingSource1.Current;
            var opnf = new OutputPatternForm
                           {
                               Pattern = currentRun.Multi.DailyOutputPattern, 
                               Text = "Edit multi run daily output file pattern (" + currentRun.Name + ")", 
                               IsMultiDailyEdit = true
                           };
            if (opnf.ShowDialog() == DialogResult.OK)
            {
                currentRun.Multi.DailyOutputPattern = opnf.Pattern;
            }
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var currentRun = (RunItem)runItemsBindingSource1.Current;
            var opnf = new OutputPatternForm
                           {
                               Pattern = currentRun.Sensitivity.OutputPattern, 
                               Text = "Edit sensitivity run summary output file pattern (" + currentRun.Name + ")", 
                               IsSensitivitySummaryEdit = true
                           };
            if (opnf.ShowDialog() == DialogResult.OK)
            {
                currentRun.Sensitivity.OutputPattern = opnf.Pattern;
            }
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var currentRun = (RunItem)runItemsBindingSource1.Current;
            var opnf = new OutputPatternForm
                           {
                               Pattern = currentRun.Sensitivity.DailyOutputPattern, 
                               Text = "Edit sensitivity run daily output file pattern (" + currentRun.Name + ")",
                               IsSensitivityDailyEdit = true
                           };
            if (opnf.ShowDialog() == DialogResult.OK)
            {
                currentRun.Sensitivity.DailyOutputPattern = opnf.Pattern;
            }
        }

        private void tabControlMode_Selecting(object sender, TabControlCancelEventArgs e)
        {
            var isNormal = radioButtonNormal.Checked;
            var isMulti = radioButtonMulti.Checked;
            var isSensitivity = radioButtonSensitivity.Checked;
            if (isNormal) lastNormalSelectedTabIndex = Math.Max(0, tabControlMode.SelectedIndex);
            else if (isMulti) lastMultiSelectedTabIndex = Math.Max(0, tabControlMode.SelectedIndex);
            else if (isSensitivity) lastSensitivitySelectedTabIndex = Math.Max(0, tabControlMode.SelectedIndex);
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            ProjectFile.This.FileContainer.RunFile.Sort();
        }

        private void linkLabel1Name1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var item = runItemsBindingSource1.Current as RunItem;
            if (item == null) return;
            using (var renameDialog = new RenameItem())
            {
                renameDialog.Item = item;
                if (renameDialog.ShowDialog(this) == DialogResult.OK)
                {
                    renameDialog.Apply();
                }
            }
        }

        //select the managementitem with the corresponding name in the bindinglist
        private void updateCurrentManagement(string ManagementName)
        {
            bool notfound = true;
            int i = 0;
            while (notfound && i < managementItemsBindingSource1.Count)
            {
                var item = managementItemsBindingSource1[i] as ManagementItem;
                if (item.Name == ManagementName)
                {
                    notfound = false;
                }
                else
                {
                    i++;
                }
            }
            if (!notfound) { managementItemsBindingSource1.Position = i; }
        }
        //select the Parameteritem with the corresponding name in the bindinglist
        private void updateCurrentParameter(string ParameterName)
        {
            bool notfound = true;
            int i = 0;
            while (notfound && i < parameterItemsBindingSource1.Count)
            {
                var item = parameterItemsBindingSource1[i] as CropParameterItem;
                if (item.Name == ParameterName)
                {
                    notfound = false;
                }
                else
                {
                    i++;
                }
            }
            if (!notfound) { parameterItemsBindingSource1.Position = i; }
        }
        //select the Varietyitem with the corresponding name in the bindinglist
        private void updateCurrentVariety(string VarietyName)
        {
            bool notfound = true;
            int i = 0;
            while (notfound && i < varietyItemsBindingSource1.Count)
            {
                var item = varietyItemsBindingSource1[i] as CropParameterItem;
                if (item.Name == VarietyName)
                {
                    notfound = false;
                }
                else
                {
                    i++;
                }
            }
            if (!notfound) { varietyItemsBindingSource1.Position = i; }
        }
        private void updateComboBox1(string experienceName)
        {
            this.comboBox1.Items.Clear();
            for (int i = 0; i < ProjectFile.This.FileContainer.ManagementFile.Items.Count; i++)
            {
                if (ProjectFile.This.FileContainer.ManagementFile.Items[i].ExperimentName == experienceName)
                    this.comboBox1.Items.Add(ProjectFile.This.FileContainer.ManagementFile.Items[i]);
            }
            if (comboBox1.Items.Count > 0) 
            { 
                comboBox1.SelectedItem = (ManagementItem)(comboBox1.Items[0]);
            }//there is always at least one element except for old files
        }

        private void experimentNameComboBoxRunSingle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string experienceName = (string)comboBox.SelectedItem;
            try
            {
                updateComboBox1(experienceName);

            }
            catch
            {

            }
        }

        private void updateComboBox19(string experienceName)
        {
            this.comboBox19.Items.Clear();
            for (int i = 0; i < ProjectFile.This.FileContainer.ManagementFile.Items.Count; i++)
            {
                if (ProjectFile.This.FileContainer.ManagementFile.Items[i].ExperimentName == experienceName)
                    this.comboBox19.Items.Add(ProjectFile.This.FileContainer.ManagementFile.Items[i]);
            }
        }

        private void experimentNameComboBoxRunMulti_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string experienceName = (string)comboBox.SelectedItem;
            try
            {
                updateComboBox19(experienceName);

            }
            catch
            {

            }
        }

        private void comboBox19_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if ((ManagementItem)comboBox.SelectedItem != null && experimentNameComboBoxRunMulti.SelectedItem != null)
            {
                System.String selectedManagement = (System.String)((ManagementItem)comboBox.SelectedItem).Name;
                System.String selectedExperiment = (System.String)(experimentNameComboBoxRunMulti.SelectedItem);
                try
                {
                    dataGridView1.CurrentRow.Cells[1].Value = selectedManagement;
                    dataGridView1.CurrentRow.Cells[0].Value = selectedExperiment;
                }
                catch
                {

                }

                System.String selectedSpecies = (System.String)((ManagementItem)comboBox.SelectedItem).Species;
                if (selectedSpecies == "" || selectedSpecies == "Wheat")
                {
                    parameterItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.NonVarietyFile.ItemsSelectable;
                    varietyItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.VarietyFile.ItemsSelectable;
                }
                else
                {
                    parameterItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.MaizeNonVarietyFile.ItemsSelectable;
                    varietyItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.MaizeVarietyFile.ItemsSelectable;
                }

                if (comboBox20.Items.Count > 0)
                {
                    comboBox20.SelectedItem = (CropParameterItem)(comboBox20.Items[0]);
                }
                if (comboBox21.Items.Count > 0)
                {
                    comboBox21.SelectedItem = (CropParameterItem)(comboBox21.Items[0]);
                }
            }
        }

        private void comboBox20_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if ((CropParameterItem)comboBox.SelectedItem != null)
            {
                System.String selectedNonVar = (System.String)((CropParameterItem)comboBox.SelectedItem).Name;
                try
                {
                    dataGridView1.CurrentRow.Cells[2].Value = selectedNonVar;
                }
                catch
                {

                }

            }
        }
        private void comboBox21_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if ((CropParameterItem)comboBox.SelectedItem != null)
            {
                System.String selectedVarietal = (System.String)((CropParameterItem)comboBox.SelectedItem).Name;
                try
                {
                    dataGridView1.CurrentRow.Cells[6].Value = selectedVarietal;
                }
                catch
                {

                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            var item= (ManagementItem)comboBox.SelectedItem;
            if (item != null)
            {
                updateCurrentManagement((string)item.Name);//there is always at least one element 

                System.String selectedSpecies = (System.String)item.Species;
                if (selectedSpecies == "" || selectedSpecies == "Wheat")
                {
                    parameterItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.NonVarietyFile.ItemsSelectable;
                    varietyItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.VarietyFile.ItemsSelectable;
                }
                else
                {
                    parameterItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.MaizeNonVarietyFile.ItemsSelectable;
                    varietyItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.MaizeVarietyFile.ItemsSelectable;
                }

                if (comboBox2.Items.Count > 0)
                {
                    comboBox2.SelectedItem = (CropParameterItem)(comboBox2.Items[0]);
                }
                if (comboBox6.Items.Count > 0)
                {
                    comboBox6.SelectedItem = (CropParameterItem)(comboBox6.Items[0]);
                }

                var currentRun = (RunItem)runItemsBindingSource1.Current;
                currentRun.Normal.ManagementItem = (System.String)((ManagementItem)managementItemsBindingSource1.Current).Name;
                currentRun.Normal.ExperimentItem = (System.String)((ManagementItem)managementItemsBindingSource1.Current).ExperimentName;

            }
        }

        private void dataGridView1_RowHeaderClicked(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == (dataGridView1.Rows.Count-1))//if you clicked on the last row header
            {
            }
        }

        private void createGraph()
        {
            //clean the previous graph
            this.chart1.Series.Clear();

            var currentRunItem = (RunItem)this.runItemsBindingSource1.Current;

            if (radioButtonNormal.Checked)
            {
                
                SiriusModel.InOut.OutputWriter.PageData allNormalRunData = currentRunItem.NormalBook[1];// .sqsro

                if (allNormalRunData != null)
                {
                    //find the array with all the data
                    int j = 0;
                    int found = 0;
                    while (j < allNormalRunData.Count && found < 2)
                    {
                        if ((string) allNormalRunData[j][0] == "Date")
                        {
                            found++;
                        }
                        j++;
                    }
                    if (found == 2)
                    {
                        this.chart1.Series.Add((string)allNormalRunData[j - 1][2] + " " + (string)allNormalRunData[j][2] + " " + (string)allNormalRunData[j + 1][2]);
                        this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        for (int i = j + 2; (i < allNormalRunData.Count && allNormalRunData[i] != null); ++i)
                        {
                            var line = allNormalRunData[i];
                            this.chart1.Series[0].Points.AddXY(line[0], line[2]);//mean air temp
                        }

                        this.chart1.Series.Add((string)allNormalRunData[j - 1][4] + " " + (string)allNormalRunData[j][4] + " " + (string)allNormalRunData[j + 1][4]);
                        this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        for (int i = j + 2; (i < allNormalRunData.Count && allNormalRunData[i] != null); ++i)
                        {
                            var line = allNormalRunData[i];
                            this.chart1.Series[1].Points.AddXY(line[0], line[4]);//mean canopy temp
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The ouput file is empty, please run a simulation", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }       
            else if (radioButtonMulti.Checked)
            {
                for (int runNumber =1; runNumber < currentRunItem.MultiBook.Count; runNumber++)
                {
                    
                    SiriusModel.InOut.OutputWriter.PageData allNormalRunData = currentRunItem.MultiBook[runNumber];// we avoid MultiBook[0] which is the .sqbrs 
                    if (allNormalRunData != null)
                    {
                        //find the array with all the data
                        int j = 0;
                        int found = 0;
                        while (j < allNormalRunData.Count && found < 2)
                        {
                            if (allNormalRunData[j][0] == "Date")
                            {
                                found++;
                            }
                            j++;
                        }
                        if (found == 2)
                        {
                            this.chart1.Series.Add((allNormalRunData.Title.Split(new Char[] { '.' }))[0] + " " + (string)allNormalRunData[j - 1][33] + " " + (string)allNormalRunData[j][33] + " " + (string)allNormalRunData[j + 1][33]);
                            this.chart1.Series[runNumber-1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                            for (int i = j + 2; (i < allNormalRunData.Count && allNormalRunData[i] != null); ++i)
                            {
                                var line = allNormalRunData[i];
                                this.chart1.Series[runNumber-1].Points.AddXY(line[0], line[33]);//crop dry matter
                            }
                        }
                    }
                    else
                    {
                        runNumber = currentRunItem.MultiBook.Count;  //stop the loop
                        MessageBox.Show("one of the ouput file is empty, please run a multi simulation", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }           
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createGraph();
        }


    }
}
