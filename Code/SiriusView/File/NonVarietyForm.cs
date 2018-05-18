using System;
using System.Windows.Forms;
using System.Linq;
using SiriusModel.InOut;
using SiriusModel.InOut.Base;

namespace SiriusView.File
{
    public partial class NonVarietyForm : ProjectDataFileForm
    {
        public NonVarietyForm()
            : base(FileContainer.NonVarietyID, FileContainer.MaizeNonVarietyID)
        {
            InitializeComponent();

            dataGridViewUpdate(nonVarietyItemsBindingSource1.Current != null);
            dataGridViewUpdate2(nonVarietyItemsBindingSource2.Current != null);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            nonVarietyItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.NonVarietyFile.Items;
            nonVarietyItemsBindingSource1_CurrentChanged(this, null);
            nonVarietyItemsBindingSource2.DataSource = ProjectFile.This.FileContainer.MaizeNonVarietyFile.Items;
            nonVarietyItemsBindingSource2_CurrentChanged(this, null);
        }
        private void nonVarietyItemsBindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            var enabled = nonVarietyItemsBindingSource1.Current != null;
            buttonDelete.UpdateEnabled(enabled);
            buttonDuplicate.UpdateEnabled(enabled);
            splitContainer1.Panel2.UpdateEnabled(enabled);

            dataGridViewUpdate(enabled);
        }
        private void nonVarietyItemsBindingSource2_CurrentChanged(object sender, EventArgs e)
        {
            var enabled = nonVarietyItemsBindingSource2.Current != null;
            buttonDeleteMaize.UpdateEnabled(enabled);
            buttonDuplicateMaize.UpdateEnabled(enabled);
            splitContainer5.Panel2.UpdateEnabled(enabled);

            dataGridViewUpdate2(enabled);
        }
        private void linkLabelName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var item = nonVarietyItemsBindingSource1.Current as CropParameterItem;
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
            var item = nonVarietyItemsBindingSource2.Current as CropParameterItem;
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
        private void dataGridViewUpdate(bool b)
        {
            if (b)
            {
                currentVariety = nonVarietyItemsBindingSource1.Current as CropParameterItem;
                var dataArray = from row in currentVariety.ParamValue orderby currentVariety.ParamModel[row.Key] select new { Model = currentVariety.ParamModel[row.Key], Name = row.Key, Value = row.Value, Unit = currentVariety.ParamUnit[row.Key], Definition = currentVariety.ParamDef[row.Key] };
                dataGridView1.DataSource = dataArray.ToArray();
            }
        }

        private void dataGridViewUpdate2(bool b)
        {
            if (b)
            {
                currentVariety = nonVarietyItemsBindingSource2.Current as CropParameterItem;
                var dataArray = from row in currentVariety.ParamValue orderby currentVariety.ParamModel[row.Key] select new { Model = currentVariety.ParamModel[row.Key], Name = row.Key, Value = row.Value, Unit = currentVariety.ParamUnit[row.Key], Definition = currentVariety.ParamDef[row.Key] };
                dataGridView2.DataSource = dataArray.ToArray();
            }
        }

        private void changeValueButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ParamValueBox.Text == "" || ParamNameBox.Text == "")
                    throw new NullReferenceException();

                currentVariety = nonVarietyItemsBindingSource1.Current as CropParameterItem;
                if (currentVariety.ParamValue.ContainsKey(ParamNameBox.Text))
                    currentVariety.ParamValue[ParamNameBox.Text] = Convert.ToDouble(ParamValueBox.Text);
                else
                    MessageBox.Show("This parameter is not a non-variety parameter.\n Please check the parameter name.", "Error with the parameter name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Name or Value info are null.", "Error in setting parameter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FormatException)
            {
                try
                {
                    var tabChar = ParamValueBox.Text.Split(new Char[] { '.' });
                    string newChar = tabChar[0] + "," + tabChar[1];
                    currentVariety.ParamValue[ParamNameBox.Text] = Convert.ToDouble(newChar);
                }
                catch (Exception)
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

                currentVariety = nonVarietyItemsBindingSource2.Current as CropParameterItem;
                if (currentVariety.ParamValue.ContainsKey(ParamNameBoxMaize.Text))
                    currentVariety.ParamValue[ParamNameBoxMaize.Text] = Convert.ToDouble(ParamValueBoxMaize.Text);
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
                    var tabChar = ParamValueBoxMaize.Text.Split(new Char[] { '.' });
                    string newChar = tabChar[0] + "," + tabChar[1];
                    currentVariety.ParamValue[ParamNameBoxMaize.Text] = Convert.ToDouble(newChar);
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
            var line = e.RowIndex;
            try
            {
                ParamNameBox.Text = dataGridView1[1, line].Value.ToString();
                ParamValueBox.Text = dataGridView1[2, line].Value.ToString();
            }
            catch(Exception)
            {

            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var line = e.RowIndex;
                ParamNameBoxMaize.Text = dataGridView2[1, line].Value.ToString();
                ParamValueBoxMaize.Text = dataGridView2[2, line].Value.ToString();
            }
            catch (Exception)
            {

            }

        }
        ///<summary>
        ///add non-variety item
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        private void buttonAdd_Click_1(object sender, EventArgs e)
        {
            nonVarietyItemsBindingSource1.Add(new CropParameterItem("new parameter"));
            nonVarietyItemsBindingSource1.EndEdit();
            
        }
        ///<summary>
        ///delete
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        private void buttonDelete_Click_1(object sender, EventArgs e)
        {

            if (nonVarietyItemsBindingSource1.Current != null)
            {
                nonVarietyItemsBindingSource1.RemoveCurrent();
            }
            else buttonDelete.UpdateEnabled(false);
        }

        private void buttonClear_Click_1(object sender, EventArgs e)
        {
            nonVarietyItemsBindingSource1.Clear();
        }

        private void buttonDuplicate_Click_1(object sender, EventArgs e)
        {
            if (nonVarietyItemsBindingSource1.Current != null)
            {
                var clone = (CropParameterItem)nonVarietyItemsBindingSource1.Current.Clone();
                clone.Name += "_dup";
                nonVarietyItemsBindingSource1.Add(clone);
                nonVarietyItemsBindingSource1.EndEdit();
            }
            else buttonDuplicate.UpdateEnabled(false);

        }

        private void buttonSort_Click_1(object sender, EventArgs e)
        {
            ProjectFile.This.FileContainer.NonVarietyFile.Sort();
        }


        private void buttonAddMaize_Click_1(object sender, EventArgs e)
        {
            nonVarietyItemsBindingSource2.Add(new CropParameterItem("new parameter"));
            nonVarietyItemsBindingSource2.EndEdit();

        }

        private void buttonDeleteMaize_Click_1(object sender, EventArgs e)
        {

            if (nonVarietyItemsBindingSource2.Current != null)
            {
                nonVarietyItemsBindingSource2.RemoveCurrent();
            }
            else buttonDeleteMaize.UpdateEnabled(false);
        }

        private void buttonClearMaize_Click_1(object sender, EventArgs e)
        {
            nonVarietyItemsBindingSource2.Clear();
        }

        private void buttonDuplicateMaize_Click_1(object sender, EventArgs e)
        {
            if (nonVarietyItemsBindingSource2.Current != null)
            {
                var clone = (CropParameterItem)nonVarietyItemsBindingSource2.Current.Clone();
                clone.Name += "_dup";
                nonVarietyItemsBindingSource2.Add(clone);
                nonVarietyItemsBindingSource2.EndEdit();
            }
            else buttonDuplicate.UpdateEnabled(false);

        }

        private void buttonSortMaize_Click_1(object sender, EventArgs e)
        {
            ProjectFile.This.FileContainer.MaizeNonVarietyFile.Sort();
        }
    }
}
