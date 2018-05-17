using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SiriusModel.InOut;
using SiriusView.Base;
using SiriusView.File;
using SiriusView.Properties;
using WeifenLuo.WinFormsUI.Docking;
using System.Diagnostics;


namespace SiriusView
{
    public partial class MainForm : Form
    {
        #region singleton design

        private static MainForm instance = null;

        public static MainForm This
        {
            get { return instance; }
        }

        #endregion

        private ToolStripButton warningButton;
        private WarningForm warningForm;
        bool trial;
        //private WarningForm warningForm;

        public MainForm(bool IsTrial =false)
        {
            if (instance != null) throw new Exception("MainForm must be a singleton");
            instance = this;
            InitializeComponent();
            if (IsTrial) this.Text = "SiriusQuality (TrialMode)";
            trial = IsTrial;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            BaseForm.LoadDockStates();

            warningForm = new WarningForm();
            warningForm.UpdateVisible(false);

            ///<Behnam>
            // Adding a tool bar button for each type of inputs.
            foreach (var id in new FileContainer().AllID)
            {
                if (id != FileContainer.MaizeVarietyID && id != FileContainer.MaizeNonVarietyID)
                {
                    if (id == FileContainer.ManagementID) toolStripInput.Items.Add(new ToolStripSeparator());
                    if (id == FileContainer.NonVarietyID) toolStripInput.Items.Add(new ToolStripSeparator());
                    if (id == FileContainer.RunID) toolStripInput.Items.Add(new ToolStripSeparator());

                    toolStripInput.Items.Add(new ToolStripButton(id, null, OnToolStripInputIDClick) { Tag = id });

                    if (id == FileContainer.RunOptionID)
                    {
                        // Adding the folder run button mode.
                        toolStripInput.Items.Add(new ToolStripButton("Full-factorial run", null, FolderRun));
                        toolStripInput.Items.Add(new ToolStripSeparator());
                    }
                }
            }
            ///</Behnam>

            warningButton = new ToolStripButton("Warning (0)", null, OnToolStripWarningClick) { Alignment = ToolStripItemAlignment.Right };
            toolStripInput.Items.Add(warningButton);
            toolStripInput.Items.Add(new ToolStripSeparator { Alignment = ToolStripItemAlignment.Right });

            projectFileBindingSource1.DataSource = ProjectFile.ThisArray;
            if (Settings.Default.LastProjectFile != "?\\?")
            {
                try
                {
                    workerControl1.LoadProjectFile(Settings.Default.LastProjectFile);
                }
                catch (Exception ex)
                {
                    ex.Show("Loading startup project");
                }
            }

            ///<Behnam>
            // var formatToolStripComboBox = new ToolStripComboBox("Output version") { Alignment = ToolStripItemAlignment.Right ,AutoSize = false, Width = 55 };
            // var formatComboBox = (ComboBox)formatToolStripComboBox.Control;
            // formatComboBox.Items.Add(OutputVersion.V13);
            // formatComboBox.Items.Add(OutputVersion.V15);
            // formatComboBox.Items.Add(OutputVersion.Cus);
            // toolStripInput.Items.Add(formatToolStripComboBox);
            // toolStripInput.Items.Add(new ToolStripLabel("Output version:") { Alignment = ToolStripItemAlignment.Right });
            // toolStripInput.Items.Add(new ToolStripSeparator { Alignment = ToolStripItemAlignment.Right });
            // formatComboBox.DataBindings.Add(new Binding("SelectedItem", projectFileBindingSource1, "OutputVersion", true, DataSourceUpdateMode.OnPropertyChanged));
            // formatComboBox.SelectedIndexChanged += formatComboBox_SelectedIndexChanged;
            ///</Behnam>

            Icon = Icon.FromHandle(Resources.siriusQualityLogo2.GetHicon());
            warningForm.Show();

            switch (WindowState)
            {
                case FormWindowState.Minimized:
                    WindowState = FormWindowState.Normal;
                    break;
            }
            this.EnsureFitsInDesktop();
            if (trial) this.Text = " SiriusQuality ( Trial Mode)";
        }

        // void formatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        // {
           // ProjectFile.This.OutputVersion = (OutputVersion)((ComboBox)sender).SelectedItem;
        // }

        private bool CanExit()
        {
            if (MustClose) return true;
            var canClose = true;

            var saveFile = false;
            var userContinue = false;
            ProjectFile.This.AskUserSave(out userContinue, out saveFile);
            if (userContinue == false) canClose = false;
            else if (saveFile)
            {
                if (!Save()) canClose = false;
            }

            return canClose;
        }

        public bool CanClose()
        {
            if (MustClose) return true;
            var canClose = true;

            var saveFile = false;
            var userContinue = false;
            ProjectFile.This.AskUserSave(out userContinue, out saveFile);
            if (userContinue == false) canClose = false;
            else if (saveFile)
            {
                if (!Save()) canClose = false;
            }
            else
            {
                var projectFile = ProjectFile.This.Path;
                if (System.IO.File.Exists(projectFile))
                {
                    WorkerControl.LoadProjectFile(projectFile);
                }
                else WorkerControl.NewProjectFile();
            }

            return canClose;
        }

        public bool CanCloseDataFiles()
        {
            if (MustClose) return true;
            var canClose = true;
            foreach (var form in ProjectDataFileFormFactory.This.Forms)
            {
                var projectDataFileFromID = form.InputFileID;
                form.Close();
                if (ProjectDataFileFormFactory.This.Contains(projectDataFileFromID))
                {
                    canClose = false;
                    break;
                }
            }
            return canClose;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (e.Cancel) return;
            MustClose = false;
            if (CanCloseDataFiles() && CanExit())
            {
                MustClose = true;
                FileContainerForm.This.Close();
                warningForm.Close();
                Settings.Default.MainWindowMaximized = WindowState;
                Settings.Default.MainFormLocation = Location;
                Settings.Default.LastProjectFile = ProjectFile.This.Path;
            }
            else e.Cancel = true;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            BaseForm.SaveDockStates();
            Settings.Default.Save();
        }

        public bool MustClose { get; private set; }

        protected void OnToolStripInputIDClick(object sender, EventArgs args)
        {
            var tsb = (ToolStripButton)sender;
            var inputform = ProjectDataFileFormFactory.This[(string)tsb.Tag];
            inputform.Show();
        }

        protected void OnToolStripWarningClick(object sender, EventArgs args)
        {
            warningForm.Show();
        }

        public DockPanel DockPanel
        {
            get { return dockPanel1; }
        }

        public WorkerControl WorkerControl
        {
            get { return workerControl1; }
        }

        private void toolStripButtonInputs_Click(object sender, EventArgs e)
        {
            FileContainerForm.This.Show();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        public bool Open()
        {
            try
            {
                if (CanCloseDataFiles() && CanClose())
                {


                    openFileDialog1.UpdateFileName(ProjectFile.This);
              
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
               
                        WorkerControl.LoadProjectFile(openFileDialog1.FileName);
        
                        return true;
                   }
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(" Exception!" +ee.Message);
                throw ee;
            }
            return false;
        }

        public bool New()
        {
            if (CanCloseDataFiles() && CanClose())
            {
                WorkerControl.NewProjectFile();
                return true;
            }
            return false;
        }

        public bool Save()
        {
            if (System.IO.File.Exists(ProjectFile.This.Path))
            {
                WorkerControl.SaveProjectFile(ProjectFile.This.Path);
                return true;
            }
            else
            {
                SaveAs();
                return true;
            }
        }

        public bool SaveAs()
        {
            saveFileDialog1.UpdateFileName(ProjectFile.This);
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                WorkerControl.SaveProjectFile(saveFileDialog1.FileName);
                return true;
            }
            else return false;
        }

        public void UpdateWarningButtonTitle(int count)
        {
            if (warningButton != null && warningButton.Text != warningForm.Text)
            {
                warningButton.Text = warningForm.Text;
                if (count > 0)
                {
                    warningButton.ForeColor = Color.Red;
                }
                else warningButton.ForeColor = Color.Black;
            }
        }

        private void clearWeatherBufferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SiriusModel.Model.Weather.Clear();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {     
            using (var about = new AboutSiriusQuality())
              {
                about.ShowDialog(this);
              }
        }

        private void FolderRun(object sender, EventArgs e)
        {
            using (var folderRunDialog = new FolderRunDialog())
            {
                folderRunDialog.ShowDialog(this);
            }
        }

        private void createXMLInputFileFromATxtFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var txt2xmlDialog = new txt2XML())
            {
                txt2xmlDialog.ShowDialog(this);
            }
        }

        private void generalHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        string filename = "..\\Help\\" +
                "SiriusHelpWindow.chm";
         System.IO.FileInfo fi = new System.IO.FileInfo(filename);
        if (fi.Exists)
            {
              Help.ShowHelp(this, filename, HelpNavigator.Find, "");
            }
        else
            {
             MessageBox.Show("Help" ,"Help file (.chm) was not found ",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GoToSiriusWebSite(object sender, EventArgs e)
        {

            Process p = new Process();
            p.StartInfo.FileName = "http://www1.clermont.inra.fr/siriusquality/";
            p.Start();
        }

        private void setVarietalAndNonVarietalParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var CropSelectorDialog = new CropParameterSetting())
            {
                CropSelectorDialog.ShowDialog(this);
            }
        }

        private void translateV15CropParameterXMLFileIntoV21ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var CropTranslatorDialog = new CropFileTranslator())
            {
                CropTranslatorDialog.ShowDialog(this);
            }
        }
    }
}
