using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SiriusModel.InOut;
using SiriusModel.InOut.Base;
using WeifenLuo.WinFormsUI.Docking;

namespace SiriusView
{
    public static class ControlHelper
    {
        public static void EnsureFitsInDesktop(this Control control)
        {
            var formBounds = control.Bounds;
            var screenBounds = Screen.FromControl(control).Bounds;
            formBounds.X = Math.Max(screenBounds.X, formBounds.X);
            formBounds.Y = Math.Max(screenBounds.Y, formBounds.Y);
            formBounds.Width = Math.Min(screenBounds.Width, formBounds.Width);
            formBounds.Height = Math.Min(screenBounds.Height, formBounds.Height);
            control.SetBounds(formBounds.X, formBounds.Y, formBounds.Width, formBounds.Height);
        }

        public static void UpdateText(this Control control, string text)
        {
            if (control.Text != text)
            {
                control.Text = text;
            }
        }

        public static void UpdateText(this ToolStripLabel control, string text)
        {
            if (control.Text != text)
            {
                control.Text = text;
            }
        }

        public static void UpdateText(this DockContent control, string text)
        {
            
            if (control.Text != text)
            {
                control.Text = text;
            }
            if (control.TabText != text)
            {
                control.TabText = text;
            }
        }

        public static void UpdateEnabled(this Control control, bool enabled)
        {
            if (control.Enabled != enabled)
            {
                control.Enabled = enabled;
            }
        }

        public static void UpdateVisible(this ToolStripProgressBar control, bool visible)
        {
            if (control.Visible != visible)
            {
                control.Visible = visible;
            }
        }

        public static void UpdateValue(this ToolStripProgressBar control, int value)
        {
            if (control.Value != value)
            {
                control.Value = value;
            }
        }

        public static void UpdateVisible(this Control control, bool visible)
        {
            if (control.Visible != visible)
            {
                control.Visible = visible;
            }
        }

        public static void CheckVisible(this TabPage control, bool visible, TabControl tabControl, List<TabPage> toHidePages, List<TabPage> toShowPages)
        {
            if (tabControl.TabPages.Contains(control))
            {
                if (visible == false)
                {
                    toHidePages.Add(control);
                }
            }
            else
            {
                if (visible == true)
                {
                    toShowPages.Add(control);
                }
            }
        }

        public static void UpdateFileName(this OpenFileDialog openfiledialog, string fileName)
        {
            if (openfiledialog.FileName != fileName) openfiledialog.FileName = fileName;
        }

        public static void UpdateFileName(this FileDialog fileDialog, IProjectDataFile projectFile)
        {
            var absoluteFileName = projectFile.AbsoluteFileName;
            absoluteFileName = (absoluteFileName != "?") ? absoluteFileName : "";
            fileDialog.FileName = absoluteFileName;
            fileDialog.Title = ((fileDialog is OpenFileDialog) ? "Open " : "Save ") + projectFile.ID;
            fileDialog.DefaultExt = projectFile.FileExtension;
            fileDialog.DereferenceLinks = true;
            fileDialog.Filter = projectFile.ID + " file (*" + projectFile.FileExtension + ")|*" + projectFile.FileExtension + "|" + "All files (*.*)|*.*";
        }

        public static void UpdateFileName(this FileDialog fileDialog, ProjectFile projectFile)
        {
            var absoluteFileName = projectFile.Path;
            absoluteFileName = (absoluteFileName != "?\\?") ? absoluteFileName : "";
            fileDialog.FileName = absoluteFileName;
            fileDialog.Title = (fileDialog is OpenFileDialog) ? "Open project" : "Save project";
            fileDialog.DefaultExt = ProjectFile.FileExtension;
            fileDialog.DereferenceLinks = true;
            fileDialog.Filter = "Project file (*" + ProjectFile.FileExtension + ")|*" + ProjectFile.FileExtension + "|" + "All files (*.*)|*.*";
        }

        public static void UpdateFileName(this FileDialog fileDialog, string id, string absoluteFileName, string fileExtension)
        {
            absoluteFileName = (absoluteFileName != "?") ? absoluteFileName : "";
            fileDialog.FileName = absoluteFileName;
            fileDialog.Title = ((fileDialog is OpenFileDialog) ? "Open " : "Save ") + id;
            fileDialog.DefaultExt = fileExtension;
            fileDialog.DereferenceLinks = true;
            fileDialog.Filter = id + " observation file (*" + fileExtension + ")|*" + fileExtension + "|" + "All files (*.*)|*.*";
            fileDialog.RestoreDirectory = true;
        }
        public static void Show(this Exception e, string caption)
        {
            MainForm.This.BeginInvoke(ShowDelegate, e, caption);
        }

        private delegate void ShowDelegateType(Exception e, string caption);

        private static ShowDelegateType ShowDelegate = ShowDelegateInvoke;

        private static void ShowDelegateInvoke(Exception e, string caption)
        {
            using (var errorDialog = new ErrorDialog(caption, e))
            {
                errorDialog.ShowDialog(MainForm.This);
            }
        }

        public static void UpdateDockPanel(this DockContent dockContent, DockPanel dockPanel)
        {
            if (dockContent.DockPanel != dockPanel)
            {
                dockContent.DockPanel = dockPanel;
            }
        }

        public static void UpdateDockState(this DockContent dockContent, DockState dockState)
        {
            DockPane paneState = null;
            if (dockContent.DockState != dockState)
            {
                dockContent.DockState = dockState;
            }
            foreach (var pane in dockContent.DockPanel.Panes)
            {
                if (pane.DockState == dockState)
                {
                    paneState = pane; break;
                }
            }
            if (paneState != null)
            {
                if (dockContent.Pane != paneState)
                {
                    dockContent.DockTo(paneState, DockStyle.Fill, 0);
                }
            }            
        }

        public static void UpdateSize(this DataGridView dataGridView, int rowCount, int columnCount)
        {
            if (dataGridView.RowCount != rowCount) dataGridView.RowCount = rowCount;
            if (dataGridView.ColumnCount != columnCount) dataGridView.ColumnCount = columnCount;
        }

        public static void AskUserSave(this IProjectDataFile pdf, out bool userContinue, out bool mustSave)
        {
            userContinue = false;
            mustSave = false;
            if (pdf.IsModified)
            {
                var result = MessageBox.Show("The " + pdf.ID + " file \"" + pdf.RelativeFileName + "\" has been modified. Save changes ?", "Close " + pdf.ID + " file", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes: mustSave = true; userContinue = true; break;
                    case DialogResult.Cancel: userContinue = false; break;
                    case DialogResult.No: userContinue = true; break;
                }
            }
            else
            {
                userContinue = true;
            }
        }

        public static void AskUserSave(this ProjectFile pf, out bool userContinue, out bool mustSave)
        {
            userContinue = true;
            mustSave = false;
            if (pf.FileContainer.IsModified == true)
            {
                var result = MessageBox.Show("The project file \"" + ProjectFile.This.FileName + "\" has been modified. Save changes ?", "Close project file", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes: mustSave = true; break;
                    case DialogResult.Cancel: userContinue = false; break;
                }
            }
        }
    }
}
