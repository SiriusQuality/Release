using System;
using System.Windows.Forms;
using System.Linq;
using SiriusModel.InOut;
using SiriusModel.InOut.Base;
using SiriusModel.InOut.OutputWriter;

namespace SiriusView.File
{
    public partial class OptimizationForm : ProjectDataFileForm
    {
        public OptimizationItem currentOpti;//////////////////////
        public OptimizationForm()
            : base(FileContainer.OptimizationID)
        {
            InitializeComponent();
            this.resetParam();
        }

        protected override void OnLoad(EventArgs e)
        {
            
            base.OnLoad(e);
            optimizationItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.OptimizationFile.Items;
            optimizationItemsBindingSource1_CurrentChanged(this, null);

            varietyItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.VarietyFile.Items;
            nonVarietyItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.NonVarietyFile.Items;
            observationItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.ObservationFile.Items;
            runItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.RunFile.Items;

            if (varietyItemsBindingSource1.Current == null
                || nonVarietyItemsBindingSource1.Current == null
                || observationItemsBindingSource1.Current == null
                || runItemsBindingSource1.Current == null)
            {
                // MessageBox.Show("Please load an optimization file (*.sqopz) or add an optimization item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //  this.Close();
            }
            else
            {
                // Ne fonctionne pas. Prise en compte du run sélectionné
                var current = optimizationItemsBindingSource1.Current as OptimizationItem;

                //createDistributionDataGridView();
                //createObservationDGV();
                // disable save path
                linkLabel3.Text = "?";
                linkLabel4.Text = "?";
                linkLabel5.Text = "?";
                linkLabel3.Enabled = false;
                button1.Enabled = false;
                //Load observations
                var currentOpti = optimizationItemsBindingSource1.Current as OptimizationItem;
                var currentObs = observationItemsBindingSource1.Current as ObservationItem;

                this.observationSelector.Parameters.Selected.Clear();
                this.observationSelector.Parameters.NotSelected.Clear();
                currentOpti.Observations.Clear();
                
                this.observationSelector.Parameters.Selected.ListChanged -= ObsSelected_ListChanged;
                this.observationSelector.Parameters.NotSelected.ListChanged -= ObsNotSelected_ListChanged;
                if (currentObs != null && currentOpti != null)
                {
                    if (currentObs.ObservationList == null) { MessageBox.Show("the observations are not loaded yet", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    else
                    {
                        foreach (var i in currentObs.ObservationList)
                        {
                            if (currentOpti.Observations.Contains(new OptiObservation(i)))
                            {
                                //add the observation to the interface 
                                observationSelector.Parameters.Selected.Add(i);
                                //add the selected observation to the optimization item
                                currentOpti.ObservationItem = currentObs;
                            }
                            else
                            {
                                //add the observation to the interface 
                                observationSelector.Parameters.NotSelected.Add(i);
                            }
                        }
                    }
                }
                this.observationSelector.Parameters.Selected.ListChanged += ObsSelected_ListChanged;
                this.observationSelector.Parameters.NotSelected.ListChanged += ObsNotSelected_ListChanged;
            }
        }

        #region BindingSource events

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            optimizationItemsBindingSource1.Add(new OptimizationItem("new"));
            optimizationItemsBindingSource1.EndEdit();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (optimizationItemsBindingSource1.Current != null)
            {
                optimizationItemsBindingSource1.RemoveCurrent();
            }
            else buttonDelete.UpdateEnabled(false);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            optimizationItemsBindingSource1.Clear();
        }

        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            if (optimizationItemsBindingSource1.Current != null)
            {
                var clone = (OptimizationItem)optimizationItemsBindingSource1.Current.Clone();
                clone.Name += "_dup";
                optimizationItemsBindingSource1.Add(clone);
                optimizationItemsBindingSource1.EndEdit();
            }
            else buttonDuplicate.UpdateEnabled(false);
        }

        private void optimizationItemsBindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            var enabled = optimizationItemsBindingSource1.Current != null;
            buttonDelete.UpdateEnabled(enabled);
            buttonDuplicate.UpdateEnabled(enabled);
            splitContainer1.Panel2.UpdateEnabled(enabled);

            if (enabled)
            {
                var currentItem = this.optimizationItemsBindingSource1.Current as OptimizationItem;
                this.algoComboBox.SelectedItem = currentItem.ChoosedAlgo;
                this.fitnessFunctionComboBox.SelectedItem = currentItem.ChoosedObjFct;
                this.optiRadioButton.Checked = currentItem.OptiOrMaxi;
                this.singleRunRadioButton.Checked = currentItem.SingleOrBatch;
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            ProjectFile.This.FileContainer.OptimizationFile.Sort();
        }

        private void linkLabelName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var item = optimizationItemsBindingSource1.Current as OptimizationItem;
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

        #endregion

        #region algorithm tab

        private void algoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentItem = this.optimizationItemsBindingSource1.Current as OptimizationItem;
            currentItem.ChoosedAlgo = algoComboBox.Text;
            this.resetParam();

            switch (algoComboBox.Text)
            {
                case "CMA-ES":
                    // Parameter
                    param1label.Text = "Nb of round";
                    param1label.Visible = true;
                    param1textBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.optimizationItemsBindingSource1, "CMAESNbOfRound", true));
                    param1textBox.Enabled = true; param1textBox.Visible = true;
                    param2label.Text = "Nb of generation";
                    param2label.Visible = true;
                    param2textBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.optimizationItemsBindingSource1, "CMAESNbOfGeneration", true));
                    param2textBox.Enabled = true; param2textBox.Visible = true;
                    param3label.Text = "Population size";
                    param3label.Visible = true;
                    param3textBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.optimizationItemsBindingSource1, "CMAES_u", true));
                    param3textBox.Enabled = true; param3textBox.Visible = true;
                    param4label.Text = "Stop fitness";
                    param4label.Visible = true;
                    param4textBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.optimizationItemsBindingSource1, "CMAESStopFitness", true));
                    param4textBox.Enabled = true; param4textBox.Visible = true;
                    break;
                case "NelderMeadSimplex":
                    param1label.Visible = true;
                    param1label.Text = "Nb of round";
                    param1textBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.optimizationItemsBindingSource1, "SimplexNbOfRound", true));
                    param1textBox.Enabled = true; param1textBox.Visible = true;
                    param2label.Text = "Nb of generation";
                    param2label.Visible = true;
                    param2textBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.optimizationItemsBindingSource1, "SimplexNbOfGeneration", true));
                    param2textBox.Enabled = true; param2textBox.Visible = true;
                    param3label.Text = "Stop fitness";
                    param3label.Visible = true;
                    param3textBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.optimizationItemsBindingSource1, "SimplexStopFitness", true));
                    param3textBox.Enabled = true; param3textBox.Visible = true;
                    break;
            }
        }

        private void resetParam()
        {
            param1label.Visible = false; param1textBox.Enabled = false; param1textBox.Visible = false; param1textBox.DataBindings.Clear();
            param2label.Visible = false; param2textBox.Enabled = false; param2textBox.Visible = false; param2textBox.DataBindings.Clear();
            param3label.Visible = false; param3textBox.Enabled = false; param3textBox.Visible = false; param3textBox.DataBindings.Clear();
            param4label.Visible = false; param4textBox.Enabled = false; param4textBox.Visible = false; param4textBox.DataBindings.Clear();
            param5label.Visible = false; param5textBox.Enabled = false; param5textBox.Visible = false; param5textBox.DataBindings.Clear();
            param6label.Visible = false; param6textBox.Enabled = false; param6textBox.Visible = false; param6textBox.DataBindings.Clear();
            param7label.Visible = false; param7textBox.Enabled = false; param7textBox.Visible = false; param7textBox.DataBindings.Clear();
            param8label.Visible = false; param8textBox.Enabled = false; param8textBox.Visible = false; param8textBox.DataBindings.Clear();
        }

        private void optiRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var currentItem = this.optimizationItemsBindingSource1.Current as OptimizationItem;
            currentItem.OptiOrMaxi = optiRadioButton.Checked;
            maxiRadioButton.Checked = !optiRadioButton.Checked;
        }

        #endregion

        #region selection tab

        private void subModelListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentItem = this.optimizationItemsBindingSource1.Current as OptimizationItem;

            varietalParameterSelector.Parameters.Selected.Clear();
            varietalParameterSelector.Parameters.NotSelected.Clear();
            nonVarietalParameterSelector.Parameters.Selected.Clear();
            nonVarietalParameterSelector.Parameters.NotSelected.Clear();
            this.varietalParameterSelector.Parameters.Selected.ListChanged -= Selected_ListChanged;
            this.varietalParameterSelector.Parameters.NotSelected.ListChanged -= NotSelected_ListChanged;
            this.nonVarietalParameterSelector.Parameters.Selected.ListChanged -= Selected_ListChanged;
            this.nonVarietalParameterSelector.Parameters.NotSelected.ListChanged -= NotSelected_ListChanged;

            if ((string)subModelListBox.SelectedItem == "Soil & Management")
            {
                varietalParameterSelector.GrouName = "Soil parameters";
                nonVarietalParameterSelector.GrouName = "Management parameters";

                foreach (var i in SoilParameter)
                {
                    if (currentItem.OptimizedParameter.Contains(new OptiParameter(i)))
                        varietalParameterSelector.Parameters.Selected.Add(i);
                    else
                        varietalParameterSelector.Parameters.NotSelected.Add(i);
                }

                foreach (var i in ManagementParameter)
                {
                    if (currentItem.OptimizedParameter.Contains(new OptiParameter(i)))
                        nonVarietalParameterSelector.Parameters.Selected.Add(i);
                    else
                        nonVarietalParameterSelector.Parameters.NotSelected.Add(i);
                }
            }
            else
            {
                varietalParameterSelector.GrouName = "Varietal parameters";
                nonVarietalParameterSelector.GrouName = "Non-Varietal parameters";

                var current = varietyItemsBindingSource1[0] as SiriusModel.InOut.CropParameterItem;
                foreach (var i in current.ParamValue)
                {
                    if (!current.ParamModel.ContainsKey(i.Key)) { throw new Exception("a parameter ("+i.Key+" in your varietal file is not defined in Sirius Quality please remove it."); }
                    if (current.ParamModel[i.Key] == (string)subModelListBox.SelectedItem)
                    {
                        if (currentItem.OptimizedParameter.Contains(new OptiParameter(i.Key)))
                            varietalParameterSelector.Parameters.Selected.Add(i.Key);
                        else
                            varietalParameterSelector.Parameters.NotSelected.Add(i.Key);
                    }
                }

                current = nonVarietyItemsBindingSource1[0] as SiriusModel.InOut.CropParameterItem;
                foreach (var i in current.ParamValue)
                {
                    if (!current.ParamModel.ContainsKey(i.Key)) { throw new Exception("a parameter (" + i.Key + " in your non varietal file is not defined in Sirius Quality please remove it."); }
                    if (current.ParamModel[i.Key] == (string)subModelListBox.SelectedItem)
                    {
                        if (currentItem.OptimizedParameter.Contains(new OptiParameter(i.Key)))
                            nonVarietalParameterSelector.Parameters.Selected.Add(i.Key);
                        else
                            nonVarietalParameterSelector.Parameters.NotSelected.Add(i.Key);
                    }
                }
            }

            this.varietalParameterSelector.Parameters.Selected.ListChanged += Selected_ListChanged;
            this.varietalParameterSelector.Parameters.NotSelected.ListChanged += NotSelected_ListChanged;
            this.nonVarietalParameterSelector.Parameters.Selected.ListChanged += Selected_ListChanged;
            this.nonVarietalParameterSelector.Parameters.NotSelected.ListChanged += NotSelected_ListChanged;
        }

        void Selected_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            var senderList = sender as System.ComponentModel.BindingList<string>;
            var currentItem = this.optimizationItemsBindingSource1.Current as OptimizationItem;

            if (e.ListChangedType.ToString() == "ItemAdded")
                currentItem.OptimizedParameter.Add(new OptiParameter(senderList[e.NewIndex]));

            //test to solve the random crash in the datagridviews in parameter tab
            this.paramDistribDataGridView.DataSource = null;
            this.paramDistribDataGridView.DataSource = this.optimizedParameterBindingSource;
        }

        void NotSelected_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            var senderList = sender as System.ComponentModel.BindingList<string>;
            var currentItem = this.optimizationItemsBindingSource1.Current as OptimizationItem;

            if (e.ListChangedType.ToString() == "ItemAdded")
                currentItem.OptimizedParameter.Remove(new OptiParameter(senderList[e.NewIndex]));

            //test to solve the random crash in the datagridviews in parameter tab
            this.paramDistribDataGridView.DataSource = null;
            this.paramDistribDataGridView.DataSource = this.optimizedParameterBindingSource;
        }

        private void observationCombaBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentOpti = optimizationItemsBindingSource1.Current as OptimizationItem;
            //var currentObs = observationItemsBindingSource1.Current as ObservationItem;
            ComboBox comboBox = (ComboBox)sender;
            var currentObs = (ObservationItem)comboBox.SelectedItem; 
            this.observationSelector.Parameters.Selected.Clear();
            this.observationSelector.Parameters.NotSelected.Clear();
            currentOpti.Observations.Clear();

            this.observationSelector.Parameters.Selected.ListChanged -= ObsSelected_ListChanged;
            this.observationSelector.Parameters.NotSelected.ListChanged -= ObsNotSelected_ListChanged;

            if (currentObs != null)
            {
                if (currentObs.ObservationList == null) { MessageBox.Show("the observations are not loaded yet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else
                {
                    foreach (var i in currentObs.ObservationList)
                    {
                        observationSelector.Parameters.NotSelected.Add(i);
                    }
                }
            }

            this.observationSelector.Parameters.Selected.ListChanged += ObsSelected_ListChanged;
            this.observationSelector.Parameters.NotSelected.ListChanged += ObsNotSelected_ListChanged;
        }


        void ObsSelected_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            var senderList = sender as System.ComponentModel.BindingList<string>;
            var currentItem = this.optimizationItemsBindingSource1.Current as OptimizationItem;
            var currentObservation = this.observationItemsBindingSource1.Current as ObservationItem;

            if (e.ListChangedType.ToString() == "ItemAdded")
            {
                currentItem.Observations.Add(new OptiObservation(senderList[e.NewIndex]));
                currentItem.ObservationItem = currentObservation;
            }
            //test to solve the random crash in the datagridviews in parameter tab
            this.observationDataGridView.DataSource = null;
            this.observationDataGridView.DataSource = this.observationsBindingSource;
        }

        void ObsNotSelected_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            var senderList = sender as System.ComponentModel.BindingList<string>;
            var currentItem = this.optimizationItemsBindingSource1.Current as OptimizationItem;

            if (e.ListChangedType.ToString() == "ItemAdded")
            {
                currentItem.Observations.Remove(new OptiObservation(senderList[e.NewIndex]));
            }
            //test to solve the random crash in the datagridviews in parameter tab
            this.observationDataGridView.DataSource = null;
            this.observationDataGridView.DataSource = this.observationsBindingSource;
        }

        #endregion

        #region run tab

        private void iterativeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            iterativeListBox.Enabled = iterativeCheckBox.Checked;
            iterativeListBox.Visible = iterativeCheckBox.Checked;
        }

        private void singleRunRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var currentItem = this.optimizationItemsBindingSource1.Current as OptimizationItem;
            currentItem.SingleOrBatch = singleRunRadioButton.Checked;
            BatchRunRadioButton.Checked = !singleRunRadioButton.Checked;
        }

        #endregion
        /*
        private void startOptiButton_Click(object sender, EventArgs e)
        {
           // var currentOpti = optimizationItemsBindingSource1.Current as OptimizationItem;
            currentOpti = (OptimizationItem)optimizationItemsBindingSource1.Current;///////////////
            currentOpti.ResultBook.Clear();

            MainForm.This.WorkerControl.StartOpti(OptiMode.NormalOpti, currentOpti.Name);
            this.startOptiButton.Enabled = false;
            this.StopOptiButton.Enabled = true;
        }*/

        /*
        private void StopOptiButton_Click(object sender, EventArgs e)
        {
            MainForm.This.WorkerControl.stopButton();

            this.startOptiButton.Enabled = true;
            this.StopOptiButton.Enabled = false;
        }*/

        private void fitnessFunctionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentItem = this.optimizationItemsBindingSource1.Current as OptimizationItem;
            currentItem.ChoosedObjFct = fitnessFunctionComboBox.Text;
        }
      
        ///<summary>
        ///Button to save the results of the optimization
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //Book b = currentOpti.ResultBook;
            
                
                Book b = new Book();
                b = currentOpti.ResultBook;
                /*LineData l = new LineData();
                l.Add(12, 2);
                PageData p = new PageData();
                p.Add(l);
                b.Add(p);*/

                //System.IO.StreamWriter file = new System.IO.StreamWriter("test.txt");


                Serialization.SerializeXml(b, linkLabel5.Text + "\\" + linkLabel4.Text + ".sqopr");
            
            
        }
        ///<summary>
        ///linLabel to choose the path for saving the optimization results
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            /*SaveFileDialog dlg = new SaveFileDialog();

            dlg.DefaultExt = "sqopt";
            dlg.AddExtension = true;
            dlg.ShowDialog();
            linkLabel4.Text = dlg.FileName;*/
           /* var item = optimizationItemsBindingSource1.Current as OptimizationItem;
            if (item == null) return;
            using (var renameDialog = new RenameItem())
            {
                renameDialog.Item = item;
                if (renameDialog.ShowDialog(this) == DialogResult.OK)
                {
                    
                    renameDialog.Apply();
                    linkLabel4.Text = renameDialog.NewName;
                }
            }*/
            var currentRun = (RunItem)runItemsBindingSource1.Current;
            var opnf = new OutputPatternForm
            {
                Pattern = currentRun.Normal.OutputPattern,
                Text = "Edit single run daily output file pattern (" + currentRun.Name + ")",
                IsNormalEdit = true
            };
            if (opnf.ShowDialog() == DialogResult.OK)
            {
                currentRun.Normal.OutputPattern = opnf.Pattern;
                //linkLabel4.Text = opnf.Pattern;
            }
            if (linkLabel4.Text != "?" && linkLabel5.Text != "?")
            {
                button1.Enabled = true;
            }
        }
      

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Text Files (.sqopr)|*.sqopr|All Files (*.*)|*.*";
           
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                linkLabel3.Text = openFileDialog1.FileName;
                Book b = new Book();
                b = Serialization.DeserializeToObj<Book>(linkLabel3.Text);
                bookControl1.Book = b;
                label5.Text = "Loaded Results";
            }
        }
      
       /*
        
        ///<summary>
        ///Load the optimization results
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Book b = new Book();
            b = Serialization.DeserializeToObj<Book>(linkLabel3.Text);
            bookControl1.Book = b;
        }*/

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var currentOptimization = optimizationItemsBindingSource1.Current as OptimizationItem;
            folderBrowserDialog1.Description = "Select "+currentOptimization.Name + " output directory";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                linkLabel5.Text = folderBrowserDialog1.SelectedPath;
                
            }
            if (linkLabel4.Text != "?" && linkLabel5.Text != "?")
            {
                button1.Enabled = true;
            }
        }
        ///<summary>
        ///Start the optimization
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            currentOpti = (OptimizationItem)optimizationItemsBindingSource1.Current;
            currentOpti.ResultBook.Clear();

            MainForm.This.WorkerControl.StartOpti(OptiMode.NormalOpti, currentOpti.Name);
            this.button3.Enabled = false;
            this.button4.Enabled = true;
            label5.Text = "Results";

        }

        ///<summary>
        ///Stop the optmization
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            MainForm.This.WorkerControl.stopButton();

            this.button3.Enabled = true;
            this.button4.Enabled = false;

        }

       

        private void IsPcpCheclNcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                linkLabel3.Enabled = true;
                
            }
            else
            {
                linkLabel3.Enabled = false;
               

            }

        }
        private void paramDistribTab_Enter(object sender, System.EventArgs e)
        {
            //we need to reset the binding because for some reason the datagridviews are not updated when we remove an element in the previous tab even though the binding list is
            //this seems to not always work (random crahses) . For test purposes the reset was moved in the functions which modifiy the bindingsources
         /*   this.observationDataGridView.DataSource = null;
            this.observationDataGridView.DataSource = this.observationsBindingSource;
            this.paramDistribDataGridView.DataSource = null;
            this.paramDistribDataGridView.DataSource = this.optimizedParameterBindingSource;*/

        }
    }
}
