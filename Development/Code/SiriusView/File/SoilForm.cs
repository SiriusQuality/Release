///<Behnam>
using System;
using System.Windows.Forms;
using SiriusModel.InOut;
using SiriusModel.InOut.Base;
using System.Drawing;

namespace SiriusView.File
{
    public partial class SoilForm : ProjectDataFileForm
    {
        public SoilForm()
            : base(FileContainer.SoilID)
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            soilItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.SoilFile.Items;
            soilItemsBindingSource1_CurrentChanged(this, null);
            isOrgNCalccheckBox_CheckedChanged(this, null);

            clayTextBox.Visible = !isKqlUsedCheckBox.Checked;
            kqTextBox.Visible = !isKqlUsedCheckBox.Checked;
            kqLabel.Visible = !isKqlUsedCheckBox.Checked;
            clayLabel.Visible = !isKqlUsedCheckBox.Checked;
            clayLabel.Enabled = isKqCalcCheckBox.Checked;
            clayTextBox.Enabled = isKqCalcCheckBox.Checked;
            kqLabel.Enabled = !isKqCalcCheckBox.Checked;
            kqTextBox.Enabled = !isKqCalcCheckBox.Checked;

            if (isKqlUsedCheckBox.Checked)
            {
                dataGridViewTextBoxColumn5.ReadOnly = !isKqCalcCheckBox.Checked;
                dataGridViewTextBoxColumn6.ReadOnly = isKqCalcCheckBox.Checked;
                if (isKqCalcCheckBox.Checked)
                {
                    dataGridViewTextBoxColumn5.Visible = true;
                    dataGridViewTextBoxColumn6.Visible = true;
                    dataGridViewTextBoxColumn6.DefaultCellStyle.BackColor = Color.LightGray;    
                }
                else
                {
                    dataGridViewTextBoxColumn5.Visible = false;
                    dataGridViewTextBoxColumn6.Visible = true;
                    dataGridViewTextBoxColumn6.DefaultCellStyle.BackColor = Color.White;  
                }
            }
            else
            {
                dataGridViewTextBoxColumn5.Visible = false;
                dataGridViewTextBoxColumn6.Visible = false;
                dataGridViewTextBoxColumn5.ReadOnly = true;
                dataGridViewTextBoxColumn6.ReadOnly = true;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            soilItemsBindingSource1.Add(new SoilItem("new soil"));
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            soilItemsBindingSource1.RemoveCurrent();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            soilItemsBindingSource1.Clear();
        }

        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            var clone = (SoilItem)soilItemsBindingSource1.Current.Clone();
            clone.Name += "_dup";
            soilItemsBindingSource1.Add(clone);
            soilItemsBindingSource1.EndEdit();
        }

        private void soilItemsBindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            var enabled = soilItemsBindingSource1.Current != null;
            buttonDelete.UpdateEnabled(enabled);
            buttonDuplicate.UpdateEnabled(enabled);
            splitContainer1.Panel2.UpdateEnabled(enabled);
        }

        private void layersDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //e.Exception.Show("Soil Layer edit error");
            layersDataGridView[e.ColumnIndex, e.RowIndex].ErrorText = e.Exception.Message;
        }

        private void dataGridViewTextBoxColumn5_RaiseCellValueChanged(object sender,DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                //this is a weird and convoluted way to make sure the cells of dataGridViewTextBoxColumn6 are updated when the cells of dataGridViewTextBoxColumn5 are changed. 
                // the -42 has not meaning . We are just assigning a value to trigger some events.
                layersDataGridView[dataGridViewTextBoxColumn6.Index, e.RowIndex].Value = -42;
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            ProjectFile.This.FileContainer.SoilFile.Sort();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var item = soilItemsBindingSource1.Current as SoilItem;
            if (item == null) return;
            using (var renameDialog = new RenameItem())
            {
                renameDialog.Item = item;
                if (renameDialog.ShowDialog(this) == DialogResult.OK)
                {
                    renameDialog.Apply(n => n.SoilItem, (n, s) => n.SoilItem = s,
                        m => m.SoilItem, (m, s) => m.SoilItem = s);
                }
            }
        }

        private void buttonAddLayer_Click(object sender, EventArgs e)
        {
            var item = soilItemsBindingSource1.Current as SoilItem;
            if (item == null) return;
            item.Layers.Add(new SoilLayer());
        }

        private void buttonSortLayer_Click(object sender, EventArgs e)
        {
            var item = soilItemsBindingSource1.Current as SoilItem;
            if (item == null) return;
            item.Sort();
        }

        ///<Behnam>
        private void isOrgNCalccheckBox_CheckedChanged(object sender, EventArgs e)
        {
            carbonTextBox.Enabled = isOrgNCalcCheckBox.Checked;
            c2nTextBox.Enabled = isOrgNCalcCheckBox.Checked;
            bdTextBox.Enabled = isOrgNCalcCheckBox.Checked;
            gravelTextBox.Enabled = isOrgNCalcCheckBox.Checked;
            noTextBox.Enabled = !isOrgNCalcCheckBox.Checked;

            if (isOrgNCalcCheckBox.Checked)
            {
                double carbon = Convert.ToDouble(carbonTextBox.Text);
                double c2n = Convert.ToDouble(c2nTextBox.Text);
                double bd = Convert.ToDouble(bdTextBox.Text);
                double gravel = Convert.ToDouble(gravelTextBox.Text);
                noTextBox.Text = Convert.ToString(Math.Round((carbon/100/c2n)*(0.4*1e6*bd*(1-(gravel/100))),1));
            }
        }

        private void isKqlUsedcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            clayTextBox.Visible = !isKqlUsedCheckBox.Checked;
            kqTextBox.Visible = !isKqlUsedCheckBox.Checked;
            kqLabel.Visible = !isKqlUsedCheckBox.Checked;
            clayLabel.Visible = !isKqlUsedCheckBox.Checked;

            if (isKqlUsedCheckBox.Checked)
            {
                dataGridViewTextBoxColumn5.ReadOnly = !isKqCalcCheckBox.Checked;
                dataGridViewTextBoxColumn6.ReadOnly = isKqCalcCheckBox.Checked;
                if (isKqCalcCheckBox.Checked)
                {
                    dataGridViewTextBoxColumn5.Visible = true;
                    dataGridViewTextBoxColumn6.Visible = true;
                    dataGridViewTextBoxColumn6.DefaultCellStyle.BackColor = Color.LightGray;                  
                }
                else
                {
                    dataGridViewTextBoxColumn5.Visible = false;
                    dataGridViewTextBoxColumn6.Visible = true;
                    dataGridViewTextBoxColumn6.DefaultCellStyle.BackColor = Color.White;    
                }
            }
            else
            {
                dataGridViewTextBoxColumn5.Visible = false;
                dataGridViewTextBoxColumn6.Visible = false;
                dataGridViewTextBoxColumn5.ReadOnly = true;
                dataGridViewTextBoxColumn6.ReadOnly = true;
            }
        }

        private void isKqlCalccheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SoilItem.IsKqCalcStatic = isKqCalcCheckBox.Checked;
            clayLabel.Enabled = isKqCalcCheckBox.Checked;
            clayTextBox.Enabled = isKqCalcCheckBox.Checked;
            kqLabel.Enabled = !isKqCalcCheckBox.Checked;
            kqTextBox.Enabled = !isKqCalcCheckBox.Checked;

            if (isKqlUsedCheckBox.Checked)
            {
                dataGridViewTextBoxColumn5.ReadOnly = !isKqCalcCheckBox.Checked;
                dataGridViewTextBoxColumn6.ReadOnly = isKqCalcCheckBox.Checked;
                if (isKqCalcCheckBox.Checked)
                {
                    dataGridViewTextBoxColumn5.Visible = true;
                    dataGridViewTextBoxColumn6.Visible = true;
                    dataGridViewTextBoxColumn6.DefaultCellStyle.BackColor = Color.LightGray;     
                }
                else
                {
                    dataGridViewTextBoxColumn5.Visible = false;
                    dataGridViewTextBoxColumn6.Visible = true;
                    dataGridViewTextBoxColumn6.DefaultCellStyle.BackColor = Color.White;  
                }
            }
            else
            {
                dataGridViewTextBoxColumn5.Visible = false;
                dataGridViewTextBoxColumn6.Visible = false;
                dataGridViewTextBoxColumn5.ReadOnly = true;
                dataGridViewTextBoxColumn6.ReadOnly = true;
            }

            if (!isKqlUsedCheckBox.Checked & isKqCalcCheckBox.Checked & kqTextBox.Text!="")
            {
                double clay = Convert.ToDouble(clayTextBox.Text);
                double kq = Convert.ToDouble(kqTextBox.Text);
                if (clay <= 9.5) kq = 1;
                if (clay <= 58.3 & clay > 9.5) kq = Math.Round(1.0271 - 0.000302 * (clay * clay), 2); 
                if (clay > 58.3) kq = 0;
                kqTextBox.Text = Convert.ToString(kq);
            }
        }
    }
}
///</Behnam>
