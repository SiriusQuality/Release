using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SiriusModel.InOut;
using SiriusModel.InOut.Base;
using SiriusModel.Model.CropModel;
using SiriusModel;
using SiriusQualityPhenology;

namespace SiriusView.File
{
    public partial class ManagementForm : ProjectDataFileForm
    {
        public ManagementForm()
            : base(FileContainer.ManagementID)
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            managementItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.ManagementFile.Items;
            if (ProjectFile.This.FileContainer.ManagementFile.Items.Count != 0)
            {
                experimentNameBindingSource.DataSource = ProjectFile.This.FileContainer.ManagementFile.Items.FilterPossibleStringValues("ExperimentName");
                updateListBox1((string)experimentNameBindingSource[0]);
            }


            var growthStageItems = Enum.GetValues(typeof(GrowthStage));
            GrowthStageComboBox.MaxDropDownItems = 20;
            foreach (var growthStageItem in growthStageItems)
            {
                var growthStage = (GrowthStage)growthStageItem;
                if (growthStage != SiriusQualityPhenology.GrowthStage.ZC_91_EndGrainFilling
                    && growthStage != SiriusQualityPhenology.GrowthStage.ZC_92_Maturity
                    && growthStage != SiriusQualityPhenology.GrowthStage.BeginningStemExtension
                    && growthStage != SiriusQualityPhenology.GrowthStage.EndVernalisation
                    && growthStage != SiriusQualityPhenology.GrowthStage.FloralInitiation)
                {
                    GrowthStageComboBox.Items.Add(growthStageItem);
                }
            }
            managementItemsBindingSource1_CurrentChanged(this, null);
            isWDinPercCheckBox_CheckedChanged(this, null);
            isTotNitrogenCheckBox_CheckedChanged(this, null);
            isSowEstCheckBox_CheckedChanged(this, null);
            IsPcpCheclNcheckBox_CheckedChanged(this, null);
            checkBoxCO2Trend_CheckedChanged(this, null);
            checkBoxNTrend_CheckedChanged(this, null);
            checkBoxIsNNIUsed_CheckedChanged(this, null);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (experimentNameBindingSource.Count !=0)
            {
                managementItemsBindingSource1.Add(new ManagementItem("new management"+managementItemsBindingSource1.Count, (string)experimentNameBindingSource.Current));
            }
            else
            {
                managementItemsBindingSource1.Add(new ManagementItem("new management", "new experiment"));
            }
            managementItemsBindingSource1.EndEdit();
            //we need to update the experiment name list with the "no experience" experience name of the new management
            experimentNameBindingSource.DataSource = ProjectFile.This.FileContainer.ManagementFile.Items.FilterPossibleStringValues("ExperimentName");
            updateListBox1((string)experimentNameBindingSource.Current);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (managementItemsBindingSource1.Current != null)
            {
                managementItemsBindingSource1.RemoveCurrent();
                if (experimentNameBindingSource.Count != 0)
                {
                    updateListBox1((string)experimentNameBindingSource.Current);
                }
            }
            else buttonDelete.UpdateEnabled(false);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            managementItemsBindingSource1.Clear();
            listBox1.Items.Clear();
            experimentNameBindingSource.Clear();
        }

        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            if (managementItemsBindingSource1.Current != null)
            {
                var clone = (ManagementItem)managementItemsBindingSource1.Current.Clone();
                clone.Name += "_dup";
                managementItemsBindingSource1.Add(clone);
                managementItemsBindingSource1.EndEdit();
                updateListBox1((string)experimentNameBindingSource.Current);
            }
            else buttonDuplicate.UpdateEnabled(false);            
        }

        private void managementItemsBindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            var enabled = managementItemsBindingSource1.Current != null;
            if (enabled)
            {
                experimentNameBindingSource.DataSource = ProjectFile.This.FileContainer.ManagementFile.Items.FilterPossibleStringValues("ExperimentName");
                bool notfound = true;
                int i = 0;
                var item = managementItemsBindingSource1.Current as ManagementItem;
                
                while (notfound && i < experimentNameBindingSource.Count)
                {
                    var exp = experimentNameBindingSource[i] as string;
                    if (exp == item.ExperimentName)
                    {
                        notfound = false;
                    }
                    else
                    {
                        i++;
                    }
                }
                if (!notfound) { experimentNameBindingSource.Position = i; }
            }
            else //the management file was remove ( new)
            {
                managementItemsBindingSource1.Clear();
                listBox1.Items.Clear();
                experimentNameBindingSource.Clear();
            }
            
            buttonDelete.UpdateEnabled(enabled);
            buttonDuplicate.UpdateEnabled(enabled);
            splitContainer1.Panel2.UpdateEnabled(enabled);          
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            ProjectFile.This.FileContainer.ManagementFile.Sort();
            if (experimentNameBindingSource != null)
            {
            updateListBox1((string)experimentNameBindingSource.Current);
            }
        }

        private void linkLabelName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ManagementItem item = managementItemsBindingSource1.Current as ManagementItem;
            if (item == null) return;
            using (var renameDialog = new RenameItem())
            {
                renameDialog.Item = item;
                if (renameDialog.ShowDialog(this) == DialogResult.OK)
                {
                    renameDialog.Apply(n => n.ManagementItem, (n, s) => n.ManagementItem = s, 
                        m => m.ManagementItem, (m, s) => m.ManagementItem = s);
                }
            }
            //update the list after the renaming
            if (experimentNameBindingSource != null)
            {
                updateListBox1((string)experimentNameBindingSource.Current, item.Name);
            }
        }

        private void buttonAddDate_Click(object sender, EventArgs e)
        {
            var item = managementItemsBindingSource1.Current as ManagementItem;
            if (item == null) return;
            item.BindingItems1.Add(new DateApplication(DateTime.Today, 0, 0));
        }

        private void buttonSortDate_Click(object sender, EventArgs e)
        {
            var item = managementItemsBindingSource1.Current as ManagementItem;
            if (item == null) return;
            item.Sort1();
        }

        private void buttonAddGrowthStage_Click(object sender, EventArgs e)
        {
            var item = managementItemsBindingSource1.Current as ManagementItem;
            if (item == null) return;
            item.BindingItems2.Add(new GrowthStageApplication(SiriusQualityPhenology.GrowthStage.Unknown, 0, 0));
        }

        private void buttonSortGrowthStage_Click(object sender, EventArgs e)
        {
            var item = managementItemsBindingSource1.Current as ManagementItem;
            if (item == null) return;
            item.Sort2();
        }

        private void isSowEstCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // sowingDateDateTimePicker1.Enabled = !isSowEstCheckBox.Checked;
            textBoxIsRelax.Enabled = isSowEstCheckBox.Checked;
            textBoxSkipDays.Enabled = isSowEstCheckBox.Checked;
            textBoxCheckDaysPcp.Enabled = isSowEstCheckBox.Checked;
            textBoxCheckDaysTemp.Enabled = isSowEstCheckBox.Checked;
            textBoxSoilMoistThr.Enabled = isSowEstCheckBox.Checked;
            textBoxSoilWorkabThr.Enabled = isSowEstCheckBox.Checked;
            textBoxCheckDepth.Enabled = isSowEstCheckBox.Checked;
            textBoxSoilTempThr.Enabled = isSowEstCheckBox.Checked;
            textBoxSoilFreezThr.Enabled = isSowEstCheckBox.Checked;
            textBoxCumPcp.Enabled = isSowEstCheckBox.Checked;
        }

        private void isWDinPercCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ( isWDinPercCheckBox.Checked) deficitLabel1.Text = "Soil water deficit at sowing (%):";
            if (!isWDinPercCheckBox.Checked) deficitLabel1.Text = "Soil water deficit at sowing (mm):";
        }

        ///<Behnam (2015.10.20)>
        ///<Comment>
        ///Adding a boolean to Manegemtn items to indicate whether we are using
        ///total N application and application shares at each application event
        ///</Comment>
        private void isTotNitrogenCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            totalNFertTextBox.Enabled = isTotNitrogenCheckBox.Checked;
            //totalNFertTextBox.Visible = isTotNitrogenCheckBox.Checked;
            //label8.Visible = isTotNitrogenCheckBox.Checked;

            if (isTotNitrogenCheckBox.Checked)
            {
                nitrogenDataGridViewTextBoxColumn1.HeaderText = "N fertilisation (%)";
                nitrogenDataGridViewTextBoxColumn.HeaderText = "N fertilisation (%)";
                totalNfertlabel.Text = "Total N fertilisation (%):";
            } else {
                nitrogenDataGridViewTextBoxColumn1.HeaderText = "N fertilisation (gN/m²)";
                nitrogenDataGridViewTextBoxColumn.HeaderText = "N fertilisation (gN/m²)";
                totalNfertlabel.Text = "Total N fertilisation (gN/m²):";
            }
        }

        private void IsPcpCheclNcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ChecledDaysNtextBox.Enabled = IsPcpCheclNcheckBox.Checked;
            MaxPostponeNtextBox.Enabled = IsPcpCheclNcheckBox.Checked;
            CumPcpThrNtextBox.Enabled = IsPcpCheclNcheckBox.Checked;
        }

        private void checkBoxNTrend_CheckedChanged(object sender, EventArgs e)
        {
            textBoxNTrendBase.Enabled = checkBoxNTrend.Checked;
            textBoxNTrendSlope.Enabled = checkBoxNTrend.Checked;
        }

        private void checkBoxCO2Trend_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCO2TrendBase.Enabled = checkBoxCO2Trend.Checked;
            textBoxCO2TrendSlope.Enabled = checkBoxCO2Trend.Checked;
        }

        private void checkBoxIsNNIUsed_CheckedChanged(object sender, EventArgs e)
        {
            textBoxNNIThr.Enabled = checkBoxIsNNIUsed.Checked;
            textBoxNNIMult.Enabled = checkBoxIsNNIUsed.Checked;
        }

        private void experimentNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedExp = (string)comboBox.SelectedItem;
            if (selectedExp != null)
            {
                updateListBox1(selectedExp);
            }
        }

        private void updateListBox1(string experienceName, string selectedManagementName =null)
        {
            this.listBox1.Items.Clear();

            int managementNameindex = 0; //there is always at least one element 

            for (int i = 0; i < ProjectFile.This.FileContainer.ManagementFile.Items.Count; i++)
            {
                if (ProjectFile.This.FileContainer.ManagementFile.Items[i].ExperimentName == experienceName)
                    this.listBox1.Items.Add(ProjectFile.This.FileContainer.ManagementFile.Items[i].Name);

                if (selectedManagementName != null && ProjectFile.This.FileContainer.ManagementFile.Items[i].Name == selectedManagementName) { managementNameindex = i; }
            }
            updateCurrentManagement((string)listBox1.Items[managementNameindex]);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listbox1 = (ListBox)sender;
            string selectedMan = (string)listBox1.SelectedItem;
            if (selectedMan != null)
            {
                //select the corresponding item in the bindinglist
                updateCurrentManagement(selectedMan);
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
    }
}
