using System;
using System.Windows.Forms;
using System.Linq;
using SiriusModel.InOut;
using SiriusModel.InOut.Base;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;

namespace SiriusView.File
{
    public partial class ObservationForm : ProjectDataFileForm
    {
        // Managements, sites, soils, varieties in text files
        private List<ManagementCell> TempOptimizationManagements = new List<ManagementCell>();
        private List<ManagementCell> TempOptimizationSites = new List<ManagementCell>();
        private List<ManagementCell> TempOptimizationSoils = new List<ManagementCell>();
        private List<ManagementCell> TempOptimizationVarieties = new List<ManagementCell>();

        private List<String> availableObservationList;

        public ObservationForm()
            : base(FileContainer.ObservationID)
        {
            InitializeComponent();
            availableObservationList = new List<string>();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            observationItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.ObservationFile.Items;
            observationItemsBindingSource1_CurrentChanged(this, null);

        }

        private void observationItemsBindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            var current = observationItemsBindingSource1.Current as ObservationItem;
            var enabled = current != null;
            buttonDelete.UpdateEnabled(enabled);
            splitContainer1.Panel2.UpdateEnabled(enabled);
            if (enabled) { loadDataTable(); } //load the observation of the selected observationitem
        }

        ///<summary>
        ///Load submodel DataTable and DataGridView
        ///</summary>
        ///<param name="_absoluteFileName"></param>
        ///<param name="_table"></param>
        ///<param name="_cheked"></param>
        ///<param name="_dgv"></param>
        ///<param name="_frozenRows"></param>
        ///<param name="_frozenColumns"></param>
        ///<param name="_management"></param>
        ///<param name="_removeWarnings"></param>
        ///<returns></returns>
        private DataTable loadDataTable(string _absoluteFileName, DataTable _table, bool _cheked, DataGridView _dgv, int _frozenRows, int _frozenColumns, List<string> _management, bool _removeWarnings)
        {
            _cheked = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (System.IO.File.Exists(_absoluteFileName))
                {
                    if (_removeWarnings) richTextBox1.Text = " ";
                    richTextBox1.Text += "\n" + ProjectFile.This.GetRelativeFileName(_absoluteFileName) + ":\n";

                    _table.Clear();
                    _table = Text2DataTable(_absoluteFileName);
                    DataTable2DGV(_table, _dgv);
                    frozenAndColor(_dgv, _frozenRows, _frozenColumns, Color.AliceBlue);
                    _cheked = tableCheck(_table, _management, _dgv);
                    findAvailableObservation(_table, _management);
                }
                else
                {
                    //if file doesn't exist clear the datagridview
                    _table.Clear();
                    DataTable2DGV(_table, _dgv);
                }
 

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            return _table;
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            loadDataTable();
        }

        ///<summary>
        ///Load all DataTables and DataGridViews
        ///</summary>
        private void loadDataTable()
        {
            var current = observationItemsBindingSource1.Current as ObservationItem;
            // if the observation file is not loaded
            if (observationItemsBindingSource1.Current == null)
            {
                MessageBox.Show("Please load an observation file (*.sqobs) or add an observation item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //this.Close();
            }
            else
            {
                availableObservationList.Clear();

                current.CanopyObservationTable = loadDataTable(
                    ProjectFile.This.GetAbsoluteFileName(current.CanopyObservationFile),
                    current.CanopyObservationTable,
                    current.CanopyTableChecked,
                    canopyDataGridView,
                    3, 6,
                    allManagements,
                    true);
                current.PhytomerObservationTable = loadDataTable(
                    ProjectFile.This.GetAbsoluteFileName(current.PhytomerObservationFile),
                    current.PhytomerObservationTable,
                    current.PhytomerTableChecked,
                    phytomerDataGridView,
                    4, 6,
                    allManagements,
                    false);
                current.PhenologyObservationTable = loadDataTable(
                    ProjectFile.This.GetAbsoluteFileName(current.PhenologyObservationFile),
                    current.PhenologyObservationTable,
                    current.PhenologyTableChecked,
                    phenologyDataGridView,
                    3, 5,
                    phenologyManagements,
                    false);
                current.SoilObservationTable = loadDataTable(
                    ProjectFile.This.GetAbsoluteFileName(current.SoilObservationFile),
                    current.SoilObservationTable,
                    current.SoilTableChecked,
                    soilDataGridView,
                    3, 5,
                    soilManagements,
                    false);
                current.HaunIndexObservationTable = loadDataTable(
                    ProjectFile.This.GetAbsoluteFileName(current.HaunIndexObservationFile),
                    current.HaunIndexObservationTable,
                    current.HaunIndexTableChecked,
                    haunIndexDataGridView,
                    3, 5,
                    allManagements,
                    false);

                current.ObservationList = availableObservationList.Distinct().ToList();
            }
        }

        #region All observations
        private List<string> allManagements = new List<string>
        {    
            "Management",
            "Site",
            "Soil",
            "Sowing date",
            "Variety",
            "Date",
            "Sampling year",
            "DOY"
        };
        private List<string> phenologyManagements = new List<string>
        {    
            "Management",
            "Site",
            "Soil",
            "Sowing date",
            "Variety",
        };
        private List<string> soilManagements = new List<string>
        {    
            "Management",
            "Site",
            "Soil",
            "Sowing date"
        };

        private List<string> allObservations = new List<string>
        {
            "Leaf area index",
			"Green area index",
			"Crop dry mass",
			"Leaf dry mass",
			"Leaf nitrogen",
			"Laminae dry mass",
			"Laminae nitrogen",
			"Specific leaf nitrogen",
			"Specific leaf dry mass",
			"Stem dry mass",
			"Stem nitrogen",
			"True stem dry mass",
			"True stem nitrogen",
			"Stem length",
			"Grain dry mass",
			"Crop nitrogen",
			"Grain nitrogen",
			"Single grain dry mass",
            "Maturity single grain dry mass",
			"Single grain nitrogen",
			"Starch per grain",
			"Albumins-globulins per grain",
			"Amphiphils per grain",
			"Gliadins per grain",
			"Glutenins per grain",
			"Soil mineral N",
			"Soil water",
            "ZC10_Emergence",
			"ZC65_Anthesis",
			"ZC92_Maturity",
            "ZC55_Heading",
            "ZC91_End of grain filling",
			"Final leaf number",
			"Grain number",
            "Maturity shoot number",
			"Maturity grain yield",
			"Grain protein concentration",
            "Anthesis leaf area index",	
            "Anthesis green area index",	
            "Anthesis crop dry mass",	
            "Maturity crop dry mass",	
            "Anthesis leaf dry mass",
            "Maturity leaf dry mass",
            "Anthesis leaf nitrogen",
            "Maturity leaf nitrogen",	
            "Anthesis laminae dry mass",	
            "Maturity laminae dry mass",	
            "Anthesis laminae nitrogen",	
            "Maturity laminae nitrogen",	
            "Anthesis specific leaf nitrogen",	
            "Anthesis specific leaf dry mass",
            "Anthesis stem dry mass",
            "Maturity stem dry mass",
            "Anthesis stem nitrogen",
            "Maturity stem nitrogen",
            "Anthesis true stem dry mass",	
            "Maturity true stem dry mass",	
            "Anthesis true stem nitrogen",	
            "Maturity true stem nitrogen",	
            "Anthesis stem length",	
            "Anthesis ear dry mass",	
            "Anthesis crop nitrogen",	
            "Maturity crop nitrogen",	
            "Maturity grain nitrogen",
            "Maturity single grain nitrogen",	
            "Maturity starch per grain",
            "Maturity grain starch concentration",	
            "Maturity albumins-globulins per grain",	
            "Maturity amphiphils per grain",
            "Maturity gliadins per grain",
            "Maturity glutenins per grain",
            "Post-anthesis crop n uptake (kgn/ha)",
            "% gliadins at maturity (% of total grain n)",
            "% gluteins at maturity (% of total grain n)",
            "Gliadins-to-gluteins ratio (dimensionless)",
            "DM harvest index",
            "N harvest index",
            "Cumulative available soil nitrogen",
            "N leaching (kgn/ha)",
            "Cumulative water drainage (mm)",
            "N use efficiency (kgdm/kgn)",
            "N utilisation efficiency (kgdm/kgn)",
            "N uptake efficiency (kgN/kgn)",
            "Water use efficiency (kgdm/ha/mm)",
            "Cumulative N mineralisation (kgN/ha) in soil profil",
            "Cumulative N denitrification in soil profil (kgn/ha)",
            "Available mineral soil N at maturity in soil profil (kgn/ha)",
            "Total mineral soil N at maturity in soil profil (kgn/ha)",
            "Available water at maturity in soil profil (mm)",
            "Emerged leaf number"
        };
        #endregion

        #region buttons
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            observationItemsBindingSource1.Add(new ObservationItem("new observation 2"));
            observationItemsBindingSource1.EndEdit();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (observationItemsBindingSource1.Current != null)
            {
                observationItemsBindingSource1.RemoveCurrent();
            }
            else buttonDelete.UpdateEnabled(false);
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            observationItemsBindingSource1.Clear();
        }
        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            //if (observationItemsBindingSource1.Current != null)
            //{
            //    var clone = (ObservationItem)observationItemsBindingSource1.Current.Clone();
            //    clone.Name += "_dup";
            //    observationItemsBindingSource1.Add(clone);
            //    observationItemsBindingSource1.EndEdit();
            //}
            //else buttonDuplicate.UpdateEnabled(false);
        }
        private void buttonSort_Click(object sender, EventArgs e)
        {
            ProjectFile.This.FileContainer.ObservationFile.Sort();
        }
        #endregion

        #region linkLabel
        private void linkLabelname_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var current = observationItemsBindingSource1.Current as ObservationItem;
            if (current == null) return;
            using (var renameDialog = new RenameItem())
            {
                renameDialog.Item = current;
                if (renameDialog.ShowDialog(this) == DialogResult.OK)
                {
                    renameDialog.Apply();
                }
            }
        }
        private void canopyLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Stream myStream;
            var current = observationItemsBindingSource1.Current as ObservationItem;
            string absoluteFileName = ProjectFile.This.GetAbsoluteFileName(current.CanopyObservationFile);

            openFileDialog1.UpdateFileName("Canopy", current.CanopyObservationFile, current.CanopyObservationExtension);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        myStream.Close();
                    }
                    current.CanopyObservationFile = ProjectFile.This.GetRelativeFileName(Path.GetFullPath(openFileDialog1.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }


            current.CanopyObservationTable = loadDataTable(
            ProjectFile.This.GetAbsoluteFileName(current.CanopyObservationFile),
            current.CanopyObservationTable,
            current.CanopyTableChecked,
            canopyDataGridView,
            3, 6,
            allManagements,
            true);

        }
        private void phytomerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Stream myStream;
            var current = observationItemsBindingSource1.Current as ObservationItem;
            string absoluteFileName = ProjectFile.This.GetAbsoluteFileName(current.PhytomerObservationFile);

            openFileDialog1.UpdateFileName("Phytomer", current.PhytomerObservationFile, current.PhytomerObservationExtension);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        myStream.Close();
                    }
                    current.PhytomerObservationFile = ProjectFile.This.GetRelativeFileName(Path.GetFullPath(openFileDialog1.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            current.PhytomerObservationTable = loadDataTable(
                ProjectFile.This.GetAbsoluteFileName(current.PhytomerObservationFile),
                current.PhytomerObservationTable,
                current.PhytomerTableChecked,
                phytomerDataGridView,
                4, 6,
                allManagements,
                true);
        }
        private void phenologyLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Stream myStream;
            var current = observationItemsBindingSource1.Current as ObservationItem;
            string absoluteFileName = ProjectFile.This.GetAbsoluteFileName(current.PhenologyObservationFile);

            openFileDialog1.UpdateFileName("Phenology", current.PhenologyObservationFile, current.PhenologyObservationExtension);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        myStream.Close();
                    }
                    current.PhenologyObservationFile = ProjectFile.This.GetRelativeFileName(Path.GetFullPath(openFileDialog1.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            current.PhenologyObservationTable = loadDataTable(
                ProjectFile.This.GetAbsoluteFileName(current.PhenologyObservationFile),
                current.PhenologyObservationTable,
                current.PhenologyTableChecked,
                phenologyDataGridView,
                3, 5,
                phenologyManagements,
                true);
        }
        private void soilLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Stream myStream;
            var current = observationItemsBindingSource1.Current as ObservationItem;
            string absoluteFileName = ProjectFile.This.GetAbsoluteFileName(current.SoilObservationFile);

            openFileDialog1.UpdateFileName("Soil", current.SoilObservationFile, current.SoilObservationExtension);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        myStream.Close();
                    }
                    current.SoilObservationFile = ProjectFile.This.GetRelativeFileName(Path.GetFullPath(openFileDialog1.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            current.SoilObservationTable = loadDataTable(
                ProjectFile.This.GetAbsoluteFileName(current.SoilObservationFile),
                current.SoilObservationTable,
                current.SoilTableChecked,
                soilDataGridView,
                3, 5,
                soilManagements,
                true);
        }
        private void haunIndexLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Stream myStream;
            var current = observationItemsBindingSource1.Current as ObservationItem;
            string absoluteFileName = ProjectFile.This.GetAbsoluteFileName(current.HaunIndexObservationFile);

            openFileDialog1.UpdateFileName("Haun Index", current.HaunIndexObservationFile, current.HaunIndexObservationExtension);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        myStream.Close();
                    }
                    current.HaunIndexObservationFile = ProjectFile.This.GetRelativeFileName(Path.GetFullPath(openFileDialog1.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            current.HaunIndexObservationTable = loadDataTable(
                ProjectFile.This.GetAbsoluteFileName(current.HaunIndexObservationFile),
                current.HaunIndexObservationTable,
                current.HaunIndexTableChecked,
                haunIndexDataGridView,
                3, 5,
                allManagements,
                true);
        }
        #endregion

        #region read text file
        ///<summary>
        ///Teste si la chaine de caractère commence par un(e) caractère/chaine de caractère spécial(e)
        ///</summary>
        ///<param name="text"></param>
        ///<returns></returns>
        private bool isComment(string text)
        {
            Regex myRegex = new Regex(@"^#");
            Regex myRegex1 = new Regex(@"^[lL][aA][sS][tT]");
            Regex myRegex2 = new Regex(@"^[cC][oO][mM][mM][eE][nN][tT]");
            if (myRegex.IsMatch(text) || myRegex1.IsMatch(text) || myRegex2.IsMatch(text))
                return true;
            return false;
        }

        ///<summary>
        ///Convert a text file into a DataTable
        ///</summary>
        ///<param name="txtFilePath"> </param>
        ///<returns> a DataTable containing observations </returns>
        private DataTable Text2DataTable(string txtFilePath)
        {
            string[] currentItems = null;
            int i;
            DataTable table = new DataTable();

            try
            {
                StreamReader sr = new StreamReader(txtFilePath);

                currentItems = readLine(sr);
                // Crée les colonnes du tableau
                for (i = 0; i < currentItems.Length; i++)
                    table.Columns.Add(i.ToString());

                // Lit jusqu'à la fin du fichier
                while (currentItems != null)
                {
                    if (!isComment(currentItems[0]))
                        table.Rows.Add(currentItems);
                    currentItems = readLine(sr);
                }

                // fermeture du StreamReader
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Warning: " + ex.Message);
            }

            return table;
        }

        ///<summary>
        ///convertit une ligne de données (tab delimited) en un tableau unidimensionnel
        ///</summary>
        ///<param name="sr"> StreamReader </param>
        ///<returns> string[] </returns>
        private string[] readLine(StreamReader sr)
        {
            string txtLine = null;
            string[] tabLine = null;

            // Lecture de la ligne du fichier texte
            txtLine = sr.ReadLine();

            // troncature de la ligne
            if (txtLine != null)
            {
                tabLine = txtLine.Split(new char[] { '\t' });
            }

            return tabLine;
        }
        #endregion

        #region verification of tables
        ///<summary>
        ///Check if the cells of the DataTable are rights
        ///</summary>
        ///<param name="_dt">The DataTable associated with the observation file</param>
        ///<param name="managementList">The name of all managements of Sirius</param>
        ///<param name="_dgv">The DataTable associated with the observation file</param>
        ///<returns></returns>
        private bool tableCheck(DataTable _dt, List<string> managementList, DataGridView _dgv)
        {
            var current = observationItemsBindingSource1.Current as ObservationItem;
            List<string> rowList = new List<string>();
            int rowIndex = 0;
            bool Checked = true;

            // if Management is in the text file
            while (rowIndex < _dt.Rows.Count - 1 && _dt.Rows[rowIndex][0].ToString() != managementList[0])
                rowIndex++;
            if (rowIndex == _dt.Rows.Count)
            {
                richTextBox1.Text += "Management(case sensitive) column was not found (case sensitive)\n";
                return false;
            }
            else
            {
                // if all management elements are in the text file
                for (int column = 0; column < managementList.Count; column++)
                    rowList.Add(_dt.Rows[rowIndex][column].ToString());
                if (!CompareLists(managementList, rowList))
                {
                    colorDataGridViewRow(_dgv, rowIndex, Color.IndianRed);
                    richTextBox1.Text += "A Management element is required: (case sensitive)";
                    foreach (string item in managementList)
                        richTextBox1.Text += "\"" + item + "\" ";
                    richTextBox1.Text += "\n";
                    return false;
                }
                else
                {
                    #region Columns: if all writed parameters exist in the current simulation
                    for (int column = 0; column < _dt.Columns.Count; column++)
                        if (!(Checked = elementExist(rowIndex, column, _dt, managementList, allObservations)))
                        {
                            colorDataGridViewRow(_dgv, rowIndex, Color.Orange);
                            colorDataGridViewCell(_dgv, rowIndex, column, Color.Yellow);
                        }
                    #endregion

                    #region Rows: if all writed managements/sites/soils/varieties exist in the current simulation
                    TempOptimizationManagements = createListForOptimization("Management", rowIndex, rowList, _dt);
                    foreach (ManagementCell _cell in TempOptimizationManagements)
                        if (!current.OptimizationManagements.Contains(_cell.CellName))
                            current.OptimizationManagements.Add(_cell.CellName);
                    Checked = columnCheck("Management", _dt, _dgv, Color.Yellow);

                    TempOptimizationSites = createListForOptimization("Site", rowIndex, rowList, _dt);
                    foreach (ManagementCell _cell in TempOptimizationSites)
                        if (!current.OptimizationSites.Contains(_cell.CellName))
                            current.OptimizationSites.Add(_cell.CellName);
                    Checked = columnCheck("Site", _dt, _dgv, Color.Yellow);

                    TempOptimizationSoils = createListForOptimization("Soil", rowIndex, rowList, _dt);
                    foreach (ManagementCell _cell in TempOptimizationSoils)
                        if (!current.OptimizationSoils.Contains(_cell.CellName))
                            current.OptimizationSoils.Add(_cell.CellName);
                    Checked = columnCheck("Soil", _dt, _dgv, Color.Yellow);

                    TempOptimizationVarieties = createListForOptimization("Variety", rowIndex, rowList, _dt);
                    if (TempOptimizationVarieties != null)
                    {
                        foreach (ManagementCell _cell in TempOptimizationVarieties)
                            if (!current.OptimizationVarieties.Contains(_cell.CellName))
                                current.OptimizationVarieties.Add(_cell.CellName);
                        Checked = columnCheck("Variety", _dt, _dgv, Color.Yellow);
                    }
                    #endregion
                }
            }
            return Checked;
        }

        ///<summary>
        ///add the available observations found in the obs file if they are in allObservations
        ///</summary>
        ///<param name="_dt">The DataTable associated with the observation file</param>
        ///<param name="managementList">The name of all managements of Sirius</param>
        ///<param name="_dgv">The DataTable associated with the observation file</param>
        ///<returns></returns>
        private void findAvailableObservation(DataTable _dt, List<string> managementList)
        {
            var current = observationItemsBindingSource1.Current as ObservationItem;
            List<string> rowList = new List<string>();
            int rowIndex = 0;

            // if Management is in the text file
            while (rowIndex < _dt.Rows.Count - 1 && _dt.Rows[rowIndex][0].ToString() != managementList[0])
                rowIndex++;
            if (rowIndex == _dt.Rows.Count)
            {
                //richTextBox1.Text += "Management columns was not found\n";
            }
            else
            {
                for (int column = 0; column < _dt.Columns.Count; column++)
                {
                    string item = _dt.Rows[rowIndex][column].ToString();
                    if (allObservations.Contains(item))
                    {
                        availableObservationList.Add(item);
                    }                    
                }

            }
        }

        ///<summary>
        ///Create the list containing all the "managements" of the current simulation
        ///</summary>
        ///<param name="management">Management, sites, soils, variety</param>
        ///<param name="_headRow">The index of the row that contains the parameter names</param>
        ///<param name="_headRowList">The row that contains the parameter names</param>
        ///<param name="_dt">The DataTable associated with the observation file</param>
        ///<returns></returns>
        private List<ManagementCell> createListForOptimization(string management, int _headRow, List<string> _headRowList, DataTable _dt)
        {
            List<ManagementCell> _list = new List<ManagementCell>();
            int column = _headRowList.IndexOf(management);
            if (column == -1)
                return null;
            bool cellNameExists = false;

            for (int row = _headRow + 2; row < _dt.Rows.Count; row++)
            {
                string cellName = _dt.Rows[row][column].ToString();
                ManagementCell cell = new ManagementCell(cellName, row, column);
                foreach (ManagementCell _cell in _list)
                    if (_cell.CellName == cell.CellName)
                        cellNameExists = true;
                if (!cellNameExists)
                    _list.Add(cell);
                cellNameExists = false;
            }
            return _list;
        }

        ///<summary>
        ///Check if the element exists and highlight the row of the DataGridView otherwise.
        ///</summary>
        ///<param name="_headRow">The index of the row that contains the parameter names</param>
        ///<param name="_headRowList">The row that contains the parameter names</param>
        ///<param name="_dt">The DataTable associated with the observation file</param>
        ///<param name="_optimizationManagementList">List containing all the "managements" Project</param>
        ///<param name="_projectManagementList">List containing all the "managements" of the current simulation</param>
        ///<param name="_dgv">The DataTable associated with the observation file</param>
        ///<param name="color"></param>
        ///<returns></returns>
        private bool columnCheck(string management, DataTable _dt, DataGridView _dgv, Color color)
        {
            bool Checked = true;
            var current = observationItemsBindingSource1.Current as ObservationItem;
            List<ManagementCell> _optimizationManagementList = new List<ManagementCell>();
            List<string> _projectManagementList = new List<string>();

            switch (management)
            {
                case "Management":
                    {
                        _optimizationManagementList = TempOptimizationManagements;
                        foreach (ManagementItem managementItem in ProjectFile.This.FileContainer.ManagementFile.Items)
                            _projectManagementList.Add(managementItem.Name);
                        break;
                    }
                case "Variety":
                    {
                        _optimizationManagementList = TempOptimizationVarieties;
                        foreach (CropParameterItem varietyItem in ProjectFile.This.FileContainer.VarietyFile.Items)
                            _projectManagementList.Add(varietyItem.Name);
                        break;
                    }
                case "Site":
                    {
                        _optimizationManagementList = TempOptimizationSites;
                        foreach (SiteItem siteItem in ProjectFile.This.FileContainer.SiteFile.Items)
                            _projectManagementList.Add(siteItem.Name);
                        break;
                    }
                case "Soil":
                    {
                        _optimizationManagementList = TempOptimizationSoils;
                        foreach (SoilItem soilItem in ProjectFile.This.FileContainer.SoilFile.Items)
                            _projectManagementList.Add(soilItem.Name);
                        break;
                    }
                default: return false;
            }

            foreach (var cell in _optimizationManagementList)
                if (!elementExist(cell.Row, cell.Column, _dt, _projectManagementList))
                {
                    colorDataGridViewCell(_dgv, cell.Row, cell.Column, color);
                    Checked = false;
                }
            return Checked;
        }

        ///<summary>
        ///returns true if the selected element in the DataTable exists in the List
        ///</summary>
        ///<param name="_column">the checked column</param>
        ///<param name="_row">the checked row</param>
        ///<param name="_dt">The DataTable associated with the observation file</param>
        ///<param name="_projectManagementList">List containing all the "managements" of the current simulation</param>
        ///<returns></returns>
        private bool elementExist(int _row, int _column, DataTable _dt, List<string> _projectManagementList)
        {
            bool Checked = true;
            if (!_projectManagementList.Contains(_dt.Rows[_row][_column].ToString()))
            {
                richTextBox1.Text += "\"" + _dt.Rows[_row][_column].ToString() + "\" is not in the current simulation\n";
                Checked = false;
            }

            return Checked;
        }
        ///<summary>
        ///returns true if the selected element in the DataTable exists in Lists
        ///</summary>
        ///<param name="_column">the checked column</param>
        ///<param name="_row">the checked row</param>
        ///<param name="_dt">The DataTable associated with the observation file</param>
        ///<param name="_list1">List containing all the "managements" of the current simulation</param>
        ///<param name="_list2">List containing all the names of observation variables</param>
        ///<returns></returns>
        private bool elementExist(int _row, int _column, DataTable _dt, List<string> _list1, List<string> _list2)
        {
            bool Checked = true;
            string item = _dt.Rows[_row][_column].ToString();
            if (!_list1.Contains(item) && !_list2.Contains(item) && item != "")
            {
                richTextBox1.Text += "\"" + _dt.Rows[_row][_column].ToString() + "\" is not in Sirius model\n";
                Checked = false;
            }

            return Checked;
        }


        ///<summary>
        ///Compares two lists
        ///</summary>
        ///<typeparam name="T"></typeparam>
        ///<param name="list1"></param>
        ///<param name="list2"></param>
        ///<returns></returns>
        private bool CompareLists<T>(List<T> list1, List<T> list2)
        {
            List<T> list1bis = new List<T>();
            List<T> list2bis = new List<T>();
            list1bis.AddRange(list1);
            list2bis.AddRange(list2);
            //Check if the two arraysLists have the same length
            if (list1bis.Count != list2bis.Count)
                return false;
            list1bis.Sort();
            list2bis.Sort();
            //Iterate through each element and compare if it is equal to the corresponding element in the other
            for (int i = 0; i < list1bis.Count; i++)
            {
                if (!list1bis[i].Equals(list2bis[i]))
                    return false;
            }
            return true;
        }
        #endregion

        #region DataGridView
        ///<summary>
        ///Bind a DataTable to a DataGridView
        ///</summary>
        ///<param name="_dt">DataTable of observations</param>
        ///<param name="dataGridView"></param>
        private void DataTable2DGV(DataTable _dt, DataGridView dataGridView)
        {
            try
            {
                dataGridView.DataSource = _dt;
                dataGridView.ReadOnly = true;
                dataGridView.AllowUserToAddRows = false;
                dataGridView.AllowUserToDeleteRows = false;
                dataGridView.AllowUserToOrderColumns = false;
                dataGridView.AllowUserToResizeColumns = true;
                dataGridView.BorderStyle = BorderStyle.None;
                dataGridView.ColumnHeadersVisible = false;

                foreach (DataGridViewColumn dc in dataGridView.Columns)
                {
                    if (dc.ValueType == typeof(System.Double))
                    {
                        DataGridViewCellStyle styl = new DataGridViewCellStyle();
                        styl.Alignment = DataGridViewContentAlignment.MiddleRight;
                        styl.Format = "N2";
                        dc.DefaultCellStyle = styl;
                    }
                    else if ((dc.ValueType == typeof(System.Int16)) || (dc.ValueType == typeof(System.Int32)))
                    {
                        DataGridViewCellStyle styl = new DataGridViewCellStyle();
                        styl.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dc.DefaultCellStyle = styl;
                    }
                    dc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            finally
            {
            }
        }

        private void frozenAndColor(DataGridView dataGridView, int maxRows, int maxColumns, Color color)
        {
            DataGridViewRow dr = new DataGridViewRow();
            DataGridViewColumn dc = new DataGridViewColumn();

            for (int row = 0; row < maxRows; row++)
            {
                for (int column = 0; column < dataGridView.Columns.Count; column++)
                {
                    dataGridView.Rows[row].Cells[column].Style.BackColor = color;
                }
                dr = dataGridView.Rows[row];
                dr.Frozen = true;
            }

            for (int row = 0; row < dataGridView.Rows.Count; row++)
            {
                for (int column = 0; column < maxColumns; column++)
                {
                    dataGridView.Rows[row].Cells[column].Style.BackColor = color;
                    dc = dataGridView.Columns[column];
                    dc.Frozen = true;
                }
            }
        }

        private void colorDataGridViewRow(DataGridView dataGridView, int index, Color color)
        {
            for (int column = 0; column < dataGridView.Columns.Count; column++)
                dataGridView.Rows[index].Cells[column].Style.BackColor = color;
        }

        private void colorDataGridViewColumn(DataGridView dataGridView, int index, Color color)
        {
            for (int row = 0; row < dataGridView.Rows.Count; row++)
                dataGridView.Rows[row].Cells[index].Style.BackColor = color;
        }

        private void colorDataGridViewCell(DataGridView dataGridView, int row, int column, Color color)
        {
            dataGridView.Rows[row].Cells[column].Style.BackColor = color;
        }
        #endregion

        private void computeDateButton_Click(object sender, EventArgs e)
        {
            var current = observationItemsBindingSource1.Current as ObservationItem;
            string temp;

            for (int i = 3; i < current.PhenologyObservationTable.Rows.Count; i++)
            {
                // sowing date recuperation
                DateTime sowing_date = Convert.ToDateTime(Convert.ToString(current.PhenologyObservationTable.Rows[i][3]));

                // ZC10_Emergence computing
                if ((temp = Convert.ToString(current.PhenologyObservationTable.Rows[i][5])) != "-999")
                {
                    DateTime ZC10_date = Convert.ToDateTime(temp);
                    current.PhenologyObservationTable.Rows[i][6] = (ZC10_date - sowing_date).Days;
                }
                else
                    current.PhenologyObservationTable.Rows[i][6] = -999;

                // ZC65_Anthesis computing
                if ((temp = Convert.ToString(current.PhenologyObservationTable.Rows[i][7])) != "-999")
                {
                    DateTime ZC65_date = Convert.ToDateTime(temp);
                    current.PhenologyObservationTable.Rows[i][8] = (ZC65_date - sowing_date).Days;
                }
                else
                    current.PhenologyObservationTable.Rows[i][8] = -999;

                // ZC92_Maturity computing
                if ((temp = Convert.ToString(current.PhenologyObservationTable.Rows[i][9])) != "-999")
                {
                    DateTime ZC92_date = Convert.ToDateTime(temp);
                    current.PhenologyObservationTable.Rows[i][10] = (ZC92_date - sowing_date).Days;
                }
                else
                    current.PhenologyObservationTable.Rows[i][10] = -999;

                // ZC55_Heading computing
                if ((temp = Convert.ToString(current.PhenologyObservationTable.Rows[i][11])) != "-999")
                {
                    DateTime ZC55_date = Convert.ToDateTime(temp);
                    current.PhenologyObservationTable.Rows[i][12] = (ZC55_date - sowing_date).Days;
                }
                else
                    current.PhenologyObservationTable.Rows[i][12] = -999;

                                // ZC91_End_Of_Grain_Filling computing
                if ((temp = Convert.ToString(current.PhenologyObservationTable.Rows[i][13])) != "-999")
                {
                    DateTime ZC91_date = Convert.ToDateTime(temp);
                    current.PhenologyObservationTable.Rows[i][14] = (ZC91_date - sowing_date).Days;
                }
                else
                    current.PhenologyObservationTable.Rows[i][14] = -999;

            }


            }

        }

       
    }
