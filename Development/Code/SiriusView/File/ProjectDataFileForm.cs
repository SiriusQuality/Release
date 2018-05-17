using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using SiriusModel.InOut;
using SiriusView.Base;

namespace SiriusView.File
{
    public partial class ProjectDataFileForm : BaseCentralDockedForm
    {
        private readonly string inputFileID;
        private readonly string inputFileIDMaize;

        public ProjectDataFileForm()
        {
            InitializeComponent();
            this.UpdateText(inputFileID);
        }
        
        public ProjectDataFileForm(string inputFileID, string inputFileIDMaize= "")
        {
            this.inputFileID = inputFileID;
            this.inputFileIDMaize = inputFileIDMaize;

            InitializeComponent();

            if (inputFileIDMaize == "")
            {
                relativeFileNameLinkLabelMaize.Hide();
                label1.Hide();
            }
            this.UpdateText(inputFileID + " ");
        }

        public override string BaseFormID()
        {
            return inputFileID;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!String.IsNullOrEmpty(inputFileID))
            {
                projectDataFileBindingSource1.SetID(inputFileID);
            }
            if (!String.IsNullOrEmpty(inputFileIDMaize))
            {
                projectDataFileBindingSource2.SetID(inputFileIDMaize);
            }
            this.UpdateText(inputFileID);

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (e.Cancel) return;
            if (!CanClose()) e.Cancel = true;
        }

        public bool CanClose()
        {
            if (MainForm.This.MustClose) return true;
            var canClose = true;

            var saveFile = false;
            var userContinue = true;
            ProjectFile.This.FileContainer[inputFileID].AskUserSave(out userContinue, out saveFile);
            if (userContinue == false) canClose = false;
            else
            {
                if (saveFile)
                {
                    if (!Save()) canClose = false;
                }
                else
                {
                    //chose to not save so reload the old file 
                    MainForm.This.WorkerControl.LoadProjectDataFile(inputFileID, ProjectFile.This.FileContainer[inputFileID].AbsoluteFileName);
                    const int maxWait = 10000;
                    var wait = 0;
                    while (wait <= maxWait && ProjectFile.This.FileContainer[inputFileID].IsModified)
                    {
                        try
                        {
                            wait += 10;
                            Thread.Sleep(10);
                        }
                        catch (ThreadInterruptedException)
                        {
                        }
                    }
                }
            }
            //save the maize file for tab which have two files (varietal and non varietal parameters
            var canCloseMaize = true;
            if (!String.IsNullOrEmpty(inputFileIDMaize))
            {
                var saveFileMaize = false;
                var userContinueMaize = false;
                ProjectFile.This.FileContainer[inputFileIDMaize].AskUserSave(out userContinueMaize, out saveFileMaize);
                if (userContinueMaize == false) canCloseMaize = false;
                else
                {
                    if (saveFileMaize)
                    {
                        if (!Save()) canClose = false;
                    }
                    else
                    {
                        //chose to not save so reload the old file
                        MainForm.This.WorkerControl.LoadProjectDataFile(inputFileIDMaize, ProjectFile.This.FileContainer[inputFileIDMaize].AbsoluteFileName);
                        const int maxWait = 10000;
                        var wait = 0;
                        while (wait <= maxWait && ProjectFile.This.FileContainer[inputFileIDMaize].IsModified)
                        {
                            try
                            {
                                wait += 10;
                                Thread.Sleep(10);
                            }
                            catch (ThreadInterruptedException)
                            {
                            }
                        }
                    }
                }
            }
            return canClose && canCloseMaize;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            ProjectDataFileFormFactory.This.Close(this);
        }

        public string InputFileID
        {
            get { return inputFileID; }
        }

        private void relativeFileNameLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var saveFile = false;
            var userContinue = false;
            ProjectFile.This.FileContainer[inputFileID].AskUserSave(out userContinue, out saveFile);
            if (saveFile == true) userContinue = Save();

            if (userContinue)
            {
                openFileDialog1.UpdateFileName(ProjectFile.This.FileContainer[inputFileID]);
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    MainForm.This.WorkerControl.LoadProjectDataFile(inputFileID, openFileDialog1.FileName);
                }
            }
        }

        private void relativeFileNameLinkLabelMaize_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bool saveFileMaize = false;
            bool userContinueMaize = false;
            ProjectFile.This.FileContainer[inputFileIDMaize].AskUserSave(out userContinueMaize, out saveFileMaize);
            if (saveFileMaize == true) userContinueMaize = Save();

            if (userContinueMaize)
            {
                openFileDialog1.UpdateFileName(ProjectFile.This.FileContainer[inputFileIDMaize]);
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    MainForm.This.WorkerControl.LoadProjectDataFile(inputFileIDMaize, openFileDialog1.FileName);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
            if (!String.IsNullOrEmpty(inputFileIDMaize)) { SaveMaize(); }
        }

        private bool Save()
        {
            if (System.IO.File.Exists(ProjectFile.This.FileContainer[inputFileID].AbsoluteFileName))
            {
                MainForm.This.WorkerControl.SaveProjectDataFile(inputFileID, ProjectFile.This.FileContainer[inputFileID].AbsoluteFileName);
      
                return true;
            }
            return SaveAs();
        }

        private bool SaveMaize()
        {
            if (System.IO.File.Exists(ProjectFile.This.FileContainer[inputFileIDMaize].AbsoluteFileName))
            {
                MainForm.This.WorkerControl.SaveProjectDataFile(inputFileIDMaize, ProjectFile.This.FileContainer[inputFileIDMaize].AbsoluteFileName);

                return true;
            }
            return SaveAsMaize();
        }
        private void buttonSaveAs_Click(object sender, EventArgs e)
        {
            SaveAs();
            if (!String.IsNullOrEmpty(inputFileIDMaize)) { SaveAsMaize(); }
        }

        private bool SaveAs()
        {
            saveFileDialog1.UpdateFileName(ProjectFile.This.FileContainer[inputFileID]);
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MainForm.This.WorkerControl.SaveProjectDataFile(inputFileID, saveFileDialog1.FileName);
                return true;
            }
            return false;
        }
        private bool SaveAsMaize()
        {
            saveFileDialog1.UpdateFileName(ProjectFile.This.FileContainer[inputFileIDMaize]);
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MainForm.This.WorkerControl.SaveProjectDataFile(inputFileIDMaize, saveFileDialog1.FileName);
                return true;
            }
            return false;
        }
        private void buttonNew_Click(object sender, EventArgs e)
        {
            MainForm.This.WorkerControl.NewProjectDataFile(inputFileID);
        }

        private void projectDataFileBindingSource1_CurrentItemChanged(object sender, EventArgs e)
        {
            var lastModified = labelModified.Visible;
            var nextModified = ProjectFile.This.FileContainer[inputFileID].IsModified;
            if (lastModified != nextModified)
            {
                labelModified.UpdateVisible(nextModified);
                this.UpdateText(inputFileID + " " + ProjectFile.This.FileContainer[inputFileID].IsModifiedStr);
            }
        }

        private void projectDataFileBindingSource2_CurrentItemChanged(object sender, EventArgs e)
        {
            var lastModified = labelModified.Visible;
            var nextModified = ProjectFile.This.FileContainer[inputFileIDMaize].IsModified;
            if (lastModified != nextModified)
            {
                labelModified.UpdateVisible(nextModified);
                this.UpdateText(inputFileIDMaize + " " + ProjectFile.This.FileContainer[inputFileIDMaize].IsModifiedStr);
            }
        }
    }
}
