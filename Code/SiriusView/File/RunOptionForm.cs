///<Behnam>
using System;
using System.Windows.Forms;
using SiriusModel.InOut;
using SiriusModel.InOut.Base;

namespace SiriusView.File
{
    public partial class RunOptionForm : ProjectDataFileForm
    {
        public RunOptionForm()
            : base(FileContainer.RunOptionID)
        {
            InitializeComponent();
            CreateCheckBoxes();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            runOptionItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.RunOptionFile.Items;
            runOptionItemsBindingSource1_CurrentChanged(this, null);

            radioDaily_CheckedChanged(this, null);
            radioWholeSeason_CheckedChanged(this, null);

            // checkBoxWNT.Enabled = true;
            // checkBoxWNT.Checked = true;
            // checkBoxWNT.Enabled = false;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            runOptionItemsBindingSource1.Add(new RunOptionItem("new run option"));
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (runOptionItemsBindingSource1.Current != null)
            {
                runOptionItemsBindingSource1.RemoveCurrent();
            }
            else buttonDelete.UpdateEnabled(false);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            runOptionItemsBindingSource1.Clear();
        }

        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            var clone = (RunOptionItem)runOptionItemsBindingSource1.Current.Clone();
            clone.Name += "_dup";
            runOptionItemsBindingSource1.Add(clone);
            runOptionItemsBindingSource1.EndEdit();
        }

        private void runOptionItemsBindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            var enabled = runOptionItemsBindingSource1.Current != null;
            buttonDelete.UpdateEnabled(enabled);
            buttonDuplicate.UpdateEnabled(enabled);
            splitContainer1.Panel1.UpdateEnabled(enabled);
            splitContainer1.Panel2.UpdateEnabled(enabled);
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            ProjectFile.This.FileContainer.RunOptionFile.Sort();
        }

        private void linkLabelName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var item = runOptionItemsBindingSource1.Current as RunOptionItem;
            if (item == null) return;
            using (var renameDialog = new RenameItem())
            {
                renameDialog.Item = item;
                if (renameDialog.ShowDialog(this) == DialogResult.OK)
                {
                    renameDialog.Apply(n => n.RunOptionItem, (n, s) => n.RunOptionItem = s,
                        m => m.RunOptionItem, (m, s) => m.RunOptionItem = s);
                }
            }
        }
        
        private void radioCus_CheckedChanged(object sender, EventArgs e)
        {
            // ProjectFile.This.FileContainer.OutputVersion = OutputVersion.Cus;
            textBox2.Text = "Cus"; selectAllbutton.Enabled = true; clearAllbutton.Enabled = true;
            tabControl1.Enabled = true;
            textBox2.Show(); textBox2.Focus(); textBox2.Hide();
        }

        private void radioV13_CheckedChanged(object sender, EventArgs e)
        {
            // ProjectFile.This.FileContainer.OutputVersion = OutputVersion.V13;
            textBox2.Text = "V13"; selectAllbutton.Enabled = false; clearAllbutton.Enabled = false;
            tabControl1.Enabled = false;
            textBox2.Show(); textBox2.Focus(); textBox2.Hide();
        }

        private void radioV15_CheckedChanged(object sender, EventArgs e)
        {
            // ProjectFile.This.FileContainer.OutputVersion = OutputVersion.V15;
            textBox2.Text = "V15"; selectAllbutton.Enabled = false; clearAllbutton.Enabled = false;
            tabControl1.Enabled = false;
            textBox2.Show(); textBox2.Focus(); textBox2.Hide();
        }

        private void radioVMaize_CheckedChanged(object sender, EventArgs e)
        {
            // ProjectFile.This.FileContainer.OutputVersion = OutputVersion.V15;
            textBox2.Text = "Maize"; selectAllbutton.Enabled = false; clearAllbutton.Enabled = false;
            tabControl1.Enabled = false;
            textBox2.Show(); textBox2.Focus(); textBox2.Hide();
        }

        private void textBox2_TextChanged(object sender, System.EventArgs e)
        {
            switch (textBox2.Text)
            {
                case "V13": radioV15.Checked = false; radioCus.Checked = false; radioVMaize.Checked = false; radioV13.Checked = true; break;
                case "V15": radioV13.Checked = false; radioCus.Checked = false; radioVMaize.Checked = false; radioV15.Checked = true; break;
                case "Cus": radioV13.Checked = false; radioV15.Checked = false; radioVMaize.Checked = false; radioCus.Checked = true; break;
                case "Maize": radioV13.Checked = false; radioV15.Checked = false; radioCus.Checked = false; radioVMaize.Checked = true; break;
            }
            textBox2.Show(); textBox2.Focus(); textBox2.Hide();
        }

        private void Selection(int selectedTab, bool isSelect)
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            if (selectedTab == 0) tableLayoutPanel = tableLayoutPanel3;
            if (selectedTab == 1) tableLayoutPanel = tableLayoutPanel4;
            var x = tableLayoutPanel.AutoScrollPosition; tabControl1.Enabled = false;

            foreach (Control ctrl in tableLayoutPanel.Controls)
            {
                if (ctrl is CheckBox)
                {
                    CheckBox box = (CheckBox)ctrl; box.Select();
                    if ( isSelect) box.CheckState = CheckState.Checked;
                    if (!isSelect) box.CheckState = CheckState.Unchecked;
                }
            }
            selectAllbutton.Select(); tableLayoutPanel.AutoScrollPosition = x; tabControl1.Enabled = true;
        }

        private void selectAllbutton_Click(object sender, EventArgs e)
        {
            Selection(tabControl1.SelectedIndex, true);
        }

        private void clearAllbutton_Click(object sender, EventArgs e)
        {
            Selection(tabControl1.SelectedIndex, false);
        }

        private void radioWholeSeason_CheckedChanged(object sender, EventArgs e)
        {
            radioDaily.Checked = !radioWholeSeason.Checked;
            checkBoxNormW.Enabled = radioWholeSeason.Checked;
            checkBoxNormN.Enabled = radioWholeSeason.Checked;
            checkBoxNormT.Enabled = radioWholeSeason.Checked;
            checkBoxW.Enabled = !radioWholeSeason.Checked;
            checkBoxN.Enabled = !radioWholeSeason.Checked;
            checkBoxT.Enabled = !radioWholeSeason.Checked;
            checkBoxWNT.Enabled = !radioWholeSeason.Checked;
            checkBoxWN.Enabled = !radioWholeSeason.Checked;
            checkBoxWT.Enabled = !radioWholeSeason.Checked;
            checkBoxNT.Enabled = !radioWholeSeason.Checked;

            // checkBoxWNT.Enabled = true;
            // checkBoxWNT.Checked = true;
            // checkBoxWNT.Enabled = false;
            
            EnableBoxes();
        }

        private void radioDaily_CheckedChanged(object sender, EventArgs e)
        {
            //EnableBoxes();
            //checkBoxActualCond.Enabled = radioDaily.Checked;
            //radioWholeSeason_CheckedChanged(this, null);
        }

        private void checkBoxNormW_CheckedChanged(object sender, EventArgs e)
        {
            EnableBoxes();
        }

        private void checkBoxNormN_CheckedChanged(object sender, EventArgs e)
        {
            EnableBoxes();
        }

        private void checkBoxNormT_CheckedChanged(object sender, EventArgs e)
        {
            EnableBoxes();
        }

        private void checkBoxW_CheckedChanged(object sender, EventArgs e)
        {
            EnableBoxes();
        }

        private void checkBoxT_CheckedChanged(object sender, EventArgs e)
        {
            EnableBoxes();
        }

        private void checkBoxN_CheckedChanged(object sender, EventArgs e)
        {
            EnableBoxes();
        }

        private void checkBoxWT_CheckedChanged(object sender, EventArgs e)
        {
            EnableBoxes();
        }

        private void checkBoxNT_CheckedChanged(object sender, EventArgs e)
        {
            EnableBoxes();
        }

        private void checkBoxWNT_CheckedChanged(object sender, EventArgs e)
        {
            EnableBoxes();
        }

        private void checkBoxWN_CheckedChanged(object sender, EventArgs e)
        {
            EnableBoxes();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void EnableBoxes()
        {
            textBoxMaxTemp.Enabled = (checkBoxNormT.Checked && radioWholeSeason.Checked) || ((checkBoxT.Checked || checkBoxWT.Checked || checkBoxNT.Checked 
                || checkBoxWNT.Checked) && radioDaily.Checked);
            textBoxWCompen.Enabled = (checkBoxNormW.Checked && radioWholeSeason.Checked) || ((checkBoxW.Checked || checkBoxWT.Checked || checkBoxWN.Checked
                 || checkBoxWNT.Checked) && radioDaily.Checked);
            textBoxNCompen.Enabled = (checkBoxNormN.Checked && radioWholeSeason.Checked) || ((checkBoxN.Checked || checkBoxWN.Checked || checkBoxNT.Checked
                 || checkBoxWNT.Checked) && radioDaily.Checked);
        }

        private void checkBoxAirTemperature_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }
    }

}
///</Behnam>
