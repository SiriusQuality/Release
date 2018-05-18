using System;
using System.ComponentModel;
using System.Windows.Forms;
using SiriusModel.InOut;
using SiriusView.Base;

namespace SiriusView.File
{
    public partial class FileContainerForm : BaseCentralDockedForm  
    {
        #region singleton design

        private static FileContainerForm instance = null;

        public static FileContainerForm This
        {
            get
            {
                if (instance == null) instance = new FileContainerForm();
                return instance;
            }
        }

        #endregion 

        private FileContainerForm()
        {
            InitializeComponent();
            this.UpdateText("Input files ");
        }

        public override string BaseFormID()
        {
            return "Input files";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // comboBoxOutputVersion.Items.Add(OutputVersion.V13);
            // comboBoxOutputVersion.Items.Add(OutputVersion.V15);
            // comboBoxOutputVersion.Items.Add(OutputVersion.Cus);
            projectFileBindingSource1.DataSource = ProjectFile.ThisArray;
            fileContainerBindingSource1.DataSource = ProjectFile.This.FileContainerArray;
            fileContainerBindingSource1.DataSource = ProjectFile.This.FileContainerArray;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (e.Cancel) return;
            e.Cancel = !(MainForm.This.CanCloseDataFiles() && MainForm.This.CanClose());
        }

        protected override void OnClosed(EventArgs e)
        {
            instance = null;
            base.OnClosed(e);
        }


        private void projectFileLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            MainForm.This.Open();

            projectFileBindingSource1.DataSource = ProjectFile.ThisArray;
            fileContainerBindingSource1.DataSource = ProjectFile.This.FileContainerArray;

            System.Threading.Thread.Sleep(3000);
        }

        private void inputFileLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var linkLabel = sender as LinkLabel;
            var inputFileID = (string)linkLabel.Tag;
            
            if (ProjectDataFileFormFactory.This.Contains(inputFileID))
            {               
                var form = ProjectDataFileFormFactory.This[inputFileID];
                if (form.CanClose())
                {
                    openFileDialog1.UpdateFileName(ProjectFile.This.FileContainer[inputFileID]);
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        MainForm.This.WorkerControl.LoadProjectDataFile(inputFileID, openFileDialog1.FileName);
                    }
                }
            }
            else
            {
                openFileDialog1.UpdateFileName(ProjectFile.This.FileContainer[inputFileID]);
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    MainForm.This.WorkerControl.LoadProjectDataFile(inputFileID, openFileDialog1.FileName);
                }
            }
        }

        private void buttonSaveProject_Click(object sender, EventArgs e)
        {
            MainForm.This.Save();
        }

        private void buttonSaveProjectAs_Click(object sender, EventArgs e)
        {
            MainForm.This.SaveAs();
        }

        private void buttonNewProject_Click(object sender, EventArgs e)
        {
            MainForm.This.New();
        }

        private void buttonLoadAll_Click(object sender, EventArgs e)
        {
            foreach (var id in new FileContainer().AllID)
            {
                if (!System.IO.File.Exists(ProjectFile.This.FileContainer[id].AbsoluteFileName))
                {
                    openFileDialog1.UpdateFileName(ProjectFile.This.FileContainer[id]);
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        MainForm.This.WorkerControl.LoadProjectDataFile(id, openFileDialog1.FileName);
                    }
                }
                else MainForm.This.WorkerControl.LoadProjectDataFile(id, ProjectFile.This.FileContainer[id].AbsoluteFileName);
            }
        }

        private void buttonSaveAll_Click(object sender, EventArgs e)
        {
            foreach (var id in new FileContainer().AllID)
            {
                if (!System.IO.File.Exists(ProjectFile.This.FileContainer[id].AbsoluteFileName))
                {
                    saveFileDialog1.UpdateFileName(ProjectFile.This.FileContainer[id]);
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        MainForm.This.WorkerControl.SaveProjectDataFile(id, saveFileDialog1.FileName);
                    }
                }
                else MainForm.This.WorkerControl.SaveProjectDataFile(id, ProjectFile.This.FileContainer[id].AbsoluteFileName);
            }
        }


        private void buttonExportAll_Click(object sender, EventArgs e)
        {
            foreach (var id in new FileContainer().AllID)
            {
                if (!System.IO.File.Exists(ProjectFile.This.FileContainer[id].AbsoluteFileName))
                {
                    saveFileDialog1.UpdateFileName(ProjectFile.This.FileContainer[id]);
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        MainForm.This.WorkerControl.SaveProjectDataFile(id, saveFileDialog1.FileName);
                    }
                }
                else MainForm.This.WorkerControl.SaveProjectDataFile(id, ProjectFile.This.FileContainer[id].AbsoluteFileName);
            }
        }

        private void fileContainerBindingSource1_CurrentItemChanged(object sender, EventArgs e)
        {
            var lastModified = labelModified.Visible == true;
            var nextModified = ProjectFile.This.FileContainer.IsModified;
            if (lastModified != nextModified)
            {
                labelModified.UpdateVisible(nextModified);
                this.UpdateText("Input files " + ProjectFile.This.FileContainer.IsModifiedStr);
            }
        }

        private void buttonRunAllSimple_Click(object sender, EventArgs e)
        {
            var runFile = ProjectFile.This.FileContainer.RunFile;
            foreach (var runItem in runFile.Items)
            {
                MainForm.This.WorkerControl.Run(RunMode.NormalRun, runItem.Name);
            }
        }

        private void buttonRunAllBatch_Click(object sender, EventArgs e)
        {
            var runFile = ProjectFile.This.FileContainer.RunFile;
            foreach (var runItem in runFile.Items)
            {
                MainForm.This.WorkerControl.Run(RunMode.MultiRun, runItem.Name);
            }
        }

        private void buttonRunAllSensitivity_Click(object sender, EventArgs e)
        {
            var runFile = ProjectFile.This.FileContainer.RunFile;
            foreach (var runItem in runFile.Items)
            {
                MainForm.This.WorkerControl.Run(RunMode.SensitivityRun, runItem.Name);
            }
        }

        private void buttonExportAllSimple_Click(object sender, EventArgs e)
        {
            var runFile = ProjectFile.This.FileContainer.RunFile;
            foreach (var runItem in runFile.Items)
            {
                MainForm.This.WorkerControl.Run(RunMode.NormalRunAndExport, runItem.Name);
            }
        }

        private void buttonExportAllBatch_Click(object sender, EventArgs e)
        {
            var runFile = ProjectFile.This.FileContainer.RunFile;
            foreach (var runItem in runFile.Items)
            {
                MainForm.This.WorkerControl.Run(RunMode.MultiRunAndExport, runItem.Name);
            }
        }

        private void buttonExportAllSensitivity_Click(object sender, EventArgs e)
        {
            var runFile = ProjectFile.This.FileContainer.RunFile;
            foreach (var runItem in runFile.Items)
            {
                MainForm.This.WorkerControl.Run(RunMode.SensitivityRunAndExport, runItem.Name);
            }
        }       
    }
}