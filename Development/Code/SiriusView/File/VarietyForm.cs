using System;
using System.Windows.Forms;
using SiriusModel.InOut;
using SiriusModel.InOut.Base;
using System.Linq;
using System.Collections;

namespace SiriusView.File
{
    public partial class VarietyForm : ProjectDataFileForm
    {
        public VarietyForm()
            : base(FileContainer.VarietyID, FileContainer.MaizeVarietyID)
        {
            InitializeComponent();

            dataGridViewUpdate(varietyItemsBindingSource1.Current != null);
            dataGridViewUpdate2(varietyItemsBindingSource1.Current != null);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            varietyItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.VarietyFile.Items;
            varietyItemsBindingSource1_CurrentChanged(this, null);
            varietyItemsBindingSource2.DataSource = ProjectFile.This.FileContainer.MaizeVarietyFile.Items;
            varietyItemsBindingSource2_CurrentChanged(this, null);
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            ProjectFile.This.FileContainer.VarietyFile.Sort();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            varietyItemsBindingSource1.Add(new CropParameterItem("new variety"));
            varietyItemsBindingSource1.EndEdit();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (varietyItemsBindingSource1.Current != null)
            {
                varietyItemsBindingSource1.RemoveCurrent();
            }
            else buttonDelete.UpdateEnabled(false);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            varietyItemsBindingSource1.Clear();
        }

        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            if (varietyItemsBindingSource1.Current != null)
            {
                var clone = (CropParameterItem)varietyItemsBindingSource1.Current.Clone();
                clone.Name += "_dup";
                varietyItemsBindingSource1.Add(clone);
                varietyItemsBindingSource1.EndEdit();
            }
            else buttonDuplicate.UpdateEnabled(false);
        }
        private void buttonSortMaize_Click(object sender, EventArgs e)
        {
            ProjectFile.This.FileContainer.MaizeVarietyFile.Sort();
        }

        private void buttonAddMaize_Click(object sender, EventArgs e)
        {
            varietyItemsBindingSource2.Add(new CropParameterItem("new variety"));
            varietyItemsBindingSource2.EndEdit();
        }

        private void buttonDeleteMaize_Click(object sender, EventArgs e)
        {
            if (varietyItemsBindingSource2.Current != null)
            {
                varietyItemsBindingSource2.RemoveCurrent();
            }
            else buttonDeleteMaize.UpdateEnabled(false);
        }

        private void buttonClearMaize_Click(object sender, EventArgs e)
        {
            varietyItemsBindingSource2.Clear();
        }

        private void buttonDuplicateMaize_Click(object sender, EventArgs e)
        {
            if (varietyItemsBindingSource2.Current != null)
            {
                var clone = (CropParameterItem)varietyItemsBindingSource2.Current.Clone();
                clone.Name += "_dup";
                varietyItemsBindingSource2.Add(clone);
                varietyItemsBindingSource2.EndEdit();
            }
            else buttonDuplicateMaize.UpdateEnabled(false);
        }

        private void varietyItemsBindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            var enabled = varietyItemsBindingSource1.Current != null;
            buttonDelete.UpdateEnabled(enabled);
            buttonDuplicate.UpdateEnabled(enabled);
            splitContainer1.Panel2.UpdateEnabled(enabled);

            dataGridViewUpdate(enabled);
        }

        private void varietyItemsBindingSource2_CurrentChanged(object sender, EventArgs e)
        {
            var enabled = varietyItemsBindingSource2.Current != null;
            buttonDeleteMaize.UpdateEnabled(enabled);
            buttonDuplicateMaize.UpdateEnabled(enabled);
            splitContainer5.Panel2.UpdateEnabled(enabled);

            dataGridViewUpdate2(enabled);
        }

        private void dataGridViewUpdate(bool b)
        {
            if (b)
            {
                currentVariety = varietyItemsBindingSource1.Current as CropParameterItem;
                var dataArray = from row in currentVariety.ParamValue orderby currentVariety.ParamModel[row.Key] select new { Model = currentVariety.ParamModel[row.Key], Name = row.Key, Value = row.Value, Unit = currentVariety.ParamUnit[row.Key], Definition = currentVariety.ParamDef[row.Key] };
                dataGridView1.DataSource = dataArray.ToArray();      
            }
        }
        private void dataGridViewUpdate2(bool b)
        {
            if (b)
            {
                currentVariety = varietyItemsBindingSource2.Current as CropParameterItem;
                var dataArray = from row in currentVariety.ParamValue orderby currentVariety.ParamModel[row.Key] select new { Model = currentVariety.ParamModel[row.Key], Name = row.Key, Value = row.Value, Unit = currentVariety.ParamUnit[row.Key], Definition = currentVariety.ParamDef[row.Key] };
                dataGridView2.DataSource = dataArray.ToArray();
            }
        }

        private void linkLabelName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var item = varietyItemsBindingSource1.Current as CropParameterItem;
            if (item == null) return;
            using (var renameDialog = new RenameItem())
            {
                renameDialog.Item = item;
                if (renameDialog.ShowDialog(this) == DialogResult.OK)
                {
                    renameDialog.Apply(n => n.ParameterItem, (n, s) => n.ParameterItem = s,
                        m => m.ParameterItem, (m, s) => m.ParameterItem = s);
                }
            }
        }

        private void linkLabelNameMaize_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var item = varietyItemsBindingSource2.Current as CropParameterItem;
            if (item == null) return;
            using (var renameDialog = new RenameItem())
            {
                renameDialog.Item = item;
                if (renameDialog.ShowDialog(this) == DialogResult.OK)
                {
                    renameDialog.Apply(n => n.ParameterItem, (n, s) => n.ParameterItem = s,
                        m => m.ParameterItem, (m, s) => m.ParameterItem = s);
                }
            }
        }

        private void changeValueButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ParamNameBox.Text == "" || ParamValueBox.Text == "")
                    throw new NullReferenceException();

                currentVariety = varietyItemsBindingSource1.Current as CropParameterItem;
                if (currentVariety.ParamValue.ContainsKey(ParamValueBox.Text))
                    currentVariety.ParamValue[ParamValueBox.Text] = Convert.ToDouble(ParamNameBox.Text);
                else
                    MessageBox.Show("This parameter is not a variety parameter.\n Please check the parameter name.", "Error with the parameter name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Name or Value info are null.", "Error in setting parameter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FormatException)
            {
                try
                {
                    var tabChar = ParamNameBox.Text.Split(new Char[] { '.' });
                    string newChar = tabChar[0] + "," + tabChar[1];
                    currentVariety.ParamValue[ParamValueBox.Text] = Convert.ToDouble(newChar);
                }
                catch(Exception)
                {
                    MessageBox.Show("Please enter a number as value.", "Error with the parameter value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
                dataGridViewUpdate(true);
            }
         
        }

        private void changeValueButtonMaize_Click(object sender, EventArgs e)
        {
            try
            {
                if (ParamNameBoxMaize.Text == "" || ParamValueBoxMaize.Text == "")
                    throw new NullReferenceException();

                currentVariety = varietyItemsBindingSource2.Current as CropParameterItem;
                if (currentVariety.ParamValue.ContainsKey(ParamValueBoxMaize.Text))
                    currentVariety.ParamValue[ParamValueBoxMaize.Text] = Convert.ToDouble(ParamNameBoxMaize.Text);
                else
                    MessageBox.Show("This parameter is not a variety parameter.\n Please check the parameter name.", "Error with the parameter name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Name or Value info are null.", "Error in setting parameter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FormatException)
            {
                try
                {
                    var tabChar = ParamNameBoxMaize.Text.Split(new Char[] { '.' });
                    string newChar = tabChar[0] + "," + tabChar[1];
                    currentVariety.ParamValue[ParamValueBoxMaize.Text] = Convert.ToDouble(newChar);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please enter a number as value.", "Error with the parameter value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
                dataGridViewUpdate2(true);
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var line = e.RowIndex;
                ParamValueBox.Text = dataGridView1[1, line].Value.ToString();
                ParamNameBox.Text = dataGridView1[2, line].Value.ToString();
            }
            catch (ArgumentOutOfRangeException)
            {

            }
            
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var line = e.RowIndex;
                ParamValueBoxMaize.Text = dataGridView2[1, line].Value.ToString();
                ParamNameBoxMaize.Text = dataGridView2[2, line].Value.ToString();
            }
            catch (ArgumentOutOfRangeException)
            {

            }

        }
    }
}
