using System.Windows.Forms;
namespace SiriusView.File
{
    partial class RunForm
    {
        ///<summary>
        ///Required designer variable.
        ///</summary>
        private System.ComponentModel.IContainer components = null;

        ///<summary>
        ///Clean up any resources being used.
        ///</summary>
        ///<param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        ///<summary>
        ///Required method for Designer support - do not modify
        ///the contents of this method with the code editor.
        ///</summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.runItemsBindingSource1 = new SiriusView.RunItemsBindingSource(this.components);
            this.experimentNameBindingSourceRun = new SiriusView.BaseBindingSource(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonDuplicate = new System.Windows.Forms.Button();
            this.buttonSort = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControlMode = new System.Windows.Forms.TabControl();
            this.tabPageNormalSettings = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label61 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.varietyItemsBindingSource1 = new SiriusView.VarietyItemsBindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.experimentNameComboBoxRunSingle = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.runOptionItemsBindingSource1 = new SiriusView.RunOptionItemsBindingSource(this.components);
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.siteItemsBindingSource1 = new SiriusView.SiteItemsBindingSource(this.components);
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.soilItemsBindingSource1 = new SiriusView.SoilItemsBindingSource(this.components);
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label43 = new System.Windows.Forms.Label();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.parameterItemsBindingSource1 = new SiriusView.NonVarietyItemsBindingSource(this.components);
            this.linkLabel1Name1 = new System.Windows.Forms.LinkLabel();
            this.tabPageNormalText = new System.Windows.Forms.TabPage();
            this.pagesControl1 = new SiriusView.Controls.BookControl();
            this.tabPageGraph = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPageMultiSettings = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ManagementName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DailyOutputFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.multiRunsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label62 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.label44 = new System.Windows.Forms.Label();
            this.linkLabel7 = new System.Windows.Forms.LinkLabel();
            this.label39 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label52 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label53 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.linkLabel9 = new System.Windows.Forms.LinkLabel();
            this.linkLabelName2 = new System.Windows.Forms.LinkLabel();
            this.comboBox19 = new System.Windows.Forms.ComboBox();
            this.experimentNameComboBoxRunMulti = new System.Windows.Forms.ComboBox();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.comboBox20 = new System.Windows.Forms.ComboBox();
            this.comboBox21 = new System.Windows.Forms.ComboBox();
            this.tabPageMultiText = new System.Windows.Forms.TabPage();
            this.bookControl1 = new SiriusView.Controls.BookControl();
            this.tabPageSensitivitySettings = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.parameterIDTextBoxColumn7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.modeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.NbStep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Min = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Max = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sensitivityRunsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.label49 = new System.Windows.Forms.Label();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.linkLabel10 = new System.Windows.Forms.LinkLabel();
            this.label46 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.label48 = new System.Windows.Forms.Label();
            this.linkLabel8 = new System.Windows.Forms.LinkLabel();
            this.linkLabeName3 = new System.Windows.Forms.LinkLabel();
            this.tabPageSensitivityText = new System.Windows.Forms.TabPage();
            this.bookControl2 = new SiriusView.Controls.BookControl();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonNormal = new System.Windows.Forms.RadioButton();
            this.radioButtonMulti = new System.Windows.Forms.RadioButton();
            this.radioButtonSensitivity = new System.Windows.Forms.RadioButton();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label51 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.managementItemsBindingSource1 = new SiriusView.ManagementItemsBindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBox8 = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox9 = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.comboBox10 = new System.Windows.Forms.ComboBox();
            this.comboBox11 = new System.Windows.Forms.ComboBox();
            this.comboBox12 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.comboBox13 = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.comboBox14 = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.comboBox15 = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.comboBox16 = new System.Windows.Forms.ComboBox();
            this.comboBox17 = new System.Windows.Forms.ComboBox();
            this.comboBox18 = new System.Windows.Forms.ComboBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.runItemsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.experimentNameBindingSourceRun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControlMode.SuspendLayout();
            this.tabPageNormalSettings.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.varietyItemsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.runOptionItemsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.siteItemsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.soilItemsBindingSource1)).BeginInit();
            this.flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.parameterItemsBindingSource1)).BeginInit();
            this.tabPageNormalText.SuspendLayout();
            this.tabPageGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabPageMultiSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.multiRunsBindingSource)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tabPageMultiText.SuspendLayout();
            this.tabPageSensitivitySettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensitivityRunsBindingSource)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.tabPageSensitivityText.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.managementItemsBindingSource1)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.DataSource = this.runItemsBindingSource1;
            this.listBox1.DisplayMember = "Name";
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(169, 562);
            this.listBox1.TabIndex = 1;
            // 
            // runItemsBindingSource1
            // 
            this.runItemsBindingSource1.DataSource = typeof(SiriusModel.InOut.RunItem);
            this.runItemsBindingSource1.CurrentChanged += new System.EventHandler(this.runItemsBindingSource1_CurrentChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 29);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.listBox1);
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1054, 620);
            this.splitContainer1.SplitterDistance = 169;
            this.splitContainer1.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.buttonAdd);
            this.flowLayoutPanel1.Controls.Add(this.buttonDelete);
            this.flowLayoutPanel1.Controls.Add(this.buttonClear);
            this.flowLayoutPanel1.Controls.Add(this.buttonDuplicate);
            this.flowLayoutPanel1.Controls.Add(this.buttonSort);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 562);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(169, 58);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // buttonAdd
            // 
            this.buttonAdd.AutoSize = true;
            this.buttonAdd.Location = new System.Drawing.Point(3, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(36, 23);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.AutoSize = true;
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Location = new System.Drawing.Point(45, 3);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(48, 23);
            this.buttonDelete.TabIndex = 1;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.AutoSize = true;
            this.buttonClear.Location = new System.Drawing.Point(99, 3);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(61, 23);
            this.buttonClear.TabIndex = 2;
            this.buttonClear.Text = "Delete all";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonDuplicate
            // 
            this.buttonDuplicate.AutoSize = true;
            this.buttonDuplicate.Enabled = false;
            this.buttonDuplicate.Location = new System.Drawing.Point(3, 32);
            this.buttonDuplicate.Name = "buttonDuplicate";
            this.buttonDuplicate.Size = new System.Drawing.Size(62, 23);
            this.buttonDuplicate.TabIndex = 3;
            this.buttonDuplicate.Text = "Duplicate";
            this.buttonDuplicate.UseVisualStyleBackColor = true;
            this.buttonDuplicate.Click += new System.EventHandler(this.buttonDuplicate_Click);
            // 
            // buttonSort
            // 
            this.buttonSort.AutoSize = true;
            this.buttonSort.Location = new System.Drawing.Point(71, 32);
            this.buttonSort.Name = "buttonSort";
            this.buttonSort.Size = new System.Drawing.Size(36, 23);
            this.buttonSort.TabIndex = 5;
            this.buttonSort.Text = "Sort";
            this.buttonSort.UseVisualStyleBackColor = true;
            this.buttonSort.Click += new System.EventHandler(this.buttonSort_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.AutoScroll = true;
            this.splitContainer2.Panel1.Controls.Add(this.tabControlMode);
            this.splitContainer2.Panel1.Controls.Add(this.flowLayoutPanel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel7);
            this.splitContainer2.Size = new System.Drawing.Size(881, 620);
            this.splitContainer2.SplitterDistance = 539;
            this.splitContainer2.TabIndex = 2;
            // 
            // tabControlMode
            // 
            this.tabControlMode.Controls.Add(this.tabPageNormalSettings);
            this.tabControlMode.Controls.Add(this.tabPageNormalText);
            this.tabControlMode.Controls.Add(this.tabPageGraph);
            this.tabControlMode.Controls.Add(this.tabPageMultiSettings);
            this.tabControlMode.Controls.Add(this.tabPageMultiText);
            this.tabControlMode.Controls.Add(this.tabPageSensitivitySettings);
            this.tabControlMode.Controls.Add(this.tabPageSensitivityText);
            this.tabControlMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMode.Location = new System.Drawing.Point(0, 29);
            this.tabControlMode.Name = "tabControlMode";
            this.tabControlMode.SelectedIndex = 0;
            this.tabControlMode.Size = new System.Drawing.Size(881, 510);
            this.tabControlMode.TabIndex = 2;
            this.tabControlMode.Tag = "All";
            this.tabControlMode.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControlMode_Selecting);
            // 
            // tabPageNormalSettings
            // 
            this.tabPageNormalSettings.AutoScroll = true;
            this.tabPageNormalSettings.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageNormalSettings.Controls.Add(this.tableLayoutPanel2);
            this.tabPageNormalSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageNormalSettings.Name = "tabPageNormalSettings";
            this.tabPageNormalSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNormalSettings.Size = new System.Drawing.Size(873, 484);
            this.tabPageNormalSettings.TabIndex = 1;
            this.tabPageNormalSettings.Tag = "Normal";
            this.tabPageNormalSettings.Text = "Settings";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoScroll = true;
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.label61, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.comboBox6, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.label58, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.experimentNameComboBoxRunSingle, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBox3, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.comboBox4, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.comboBox5, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel4, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.comboBox1, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.comboBox2, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.linkLabel1Name1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 12;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(867, 221);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Dock = System.Windows.Forms.DockStyle.Right;
            this.label61.Location = new System.Drawing.Point(25, 194);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(100, 27);
            this.label61.TabIndex = 3;
            this.label61.Text = "Varietal parameters:";
            this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 13);
            this.label7.MinimumSize = new System.Drawing.Size(0, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 26);
            this.label7.TabIndex = 3;
            this.label7.Text = "Daily output file:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 27);
            this.label5.TabIndex = 8;
            this.label5.Text = "Site:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox6
            // 
            this.comboBox6.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.runItemsBindingSource1, "NormalVarietyItem", true));
            this.comboBox6.DataSource = this.varietyItemsBindingSource1;
            this.comboBox6.DisplayMember = "Name";
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Location = new System.Drawing.Point(131, 197);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(606, 21);
            this.comboBox6.TabIndex = 12;
            this.comboBox6.ValueMember = "Name";
            // 
            // varietyItemsBindingSource1
            // 
            this.varietyItemsBindingSource1.AllowNew = true;
            this.varietyItemsBindingSource1.DataSource = typeof(SiriusModel.InOut.CropParameterItem);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 27);
            this.label4.TabIndex = 6;
            this.label4.Text = "Run option:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label58.Location = new System.Drawing.Point(3, 39);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(122, 20);
            this.label58.TabIndex = 22;
            this.label58.Text = "Experiment";
            this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // experimentNameComboBoxRunSingle
            // 
            this.experimentNameComboBoxRunSingle.DataSource = this.experimentNameBindingSourceRun;
            this.experimentNameComboBoxRunSingle.DisplayMember = "Name";
            this.experimentNameComboBoxRunSingle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.experimentNameComboBoxRunSingle.FormattingEnabled = true;
            this.experimentNameComboBoxRunSingle.Location = new System.Drawing.Point(131, 42);
            this.experimentNameComboBoxRunSingle.Name = "experimentNameComboBoxRunSingle";
            this.experimentNameComboBoxRunSingle.Size = new System.Drawing.Size(200, 21);
            this.experimentNameComboBoxRunSingle.TabIndex = 23;
            this.experimentNameComboBoxRunSingle.SelectedIndexChanged += new System.EventHandler(this.experimentNameComboBoxRunSingle_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 27);
            this.label6.TabIndex = 10;
            this.label6.Text = "Soil:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Run name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox3
            // 
            this.comboBox3.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.runItemsBindingSource1, "NormalRunOptionItem", true));
            this.comboBox3.DataSource = this.runOptionItemsBindingSource1;
            this.comboBox3.DisplayMember = "Name";
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(131, 116);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(606, 21);
            this.comboBox3.TabIndex = 7;
            this.comboBox3.ValueMember = "Name";
            // 
            // runOptionItemsBindingSource1
            // 
            this.runOptionItemsBindingSource1.DataSource = typeof(SiriusModel.InOut.RunOptionItem);
            // 
            // comboBox4
            // 
            this.comboBox4.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.runItemsBindingSource1, "NormalSiteItem", true));
            this.comboBox4.DataSource = this.siteItemsBindingSource1;
            this.comboBox4.DisplayMember = "Name";
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(131, 143);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(606, 21);
            this.comboBox4.TabIndex = 9;
            this.comboBox4.ValueMember = "Name";
            // 
            // siteItemsBindingSource1
            // 
            this.siteItemsBindingSource1.DataSource = typeof(SiriusModel.InOut.SiteItem);
            // 
            // comboBox5
            // 
            this.comboBox5.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.runItemsBindingSource1, "NormalSoilItem", true));
            this.comboBox5.DataSource = this.soilItemsBindingSource1;
            this.comboBox5.DisplayMember = "Name";
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new System.Drawing.Point(131, 170);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(606, 21);
            this.comboBox5.TabIndex = 16;
            this.comboBox5.ValueMember = "Name";
            // 
            // soilItemsBindingSource1
            // 
            this.soilItemsBindingSource1.DataSource = typeof(SiriusModel.InOut.SoilItem);
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.Controls.Add(this.linkLabel1);
            this.flowLayoutPanel4.Controls.Add(this.label43);
            this.flowLayoutPanel4.Controls.Add(this.linkLabel6);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(128, 13);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(739, 26);
            this.flowLayoutPanel4.TabIndex = 20;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "NormalOutputDirectory", true));
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel1.Location = new System.Drawing.Point(0, 0);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabel1.MinimumSize = new System.Drawing.Size(0, 26);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(55, 26);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Tag = "Normal";
            this.linkLabel1.Text = "linkLabel1";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.outputDirectory_Click);
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label43.Location = new System.Drawing.Point(55, 0);
            this.label43.Margin = new System.Windows.Forms.Padding(0);
            this.label43.MinimumSize = new System.Drawing.Size(0, 26);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(12, 26);
            this.label43.TabIndex = 18;
            this.label43.Text = "\\";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkLabel6
            // 
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "NormalOutputFileName", true));
            this.linkLabel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel6.LinkColor = System.Drawing.Color.Green;
            this.linkLabel6.Location = new System.Drawing.Point(70, 0);
            this.linkLabel6.MinimumSize = new System.Drawing.Size(0, 26);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(55, 26);
            this.linkLabel6.TabIndex = 19;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "linkLabel6";
            this.linkLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel6_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "Management:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox1
            // 
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(131, 62);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(606, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.ValueMember = "Name";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 27);
            this.label8.TabIndex = 4;
            this.label8.Text = "Non-varietal parameters:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox2
            // 
            this.comboBox2.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.runItemsBindingSource1, "NormalParameterItem", true));
            this.comboBox2.DataSource = this.parameterItemsBindingSource1;
            this.comboBox2.DisplayMember = "Name";
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(131, 89);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(606, 21);
            this.comboBox2.TabIndex = 5;
            this.comboBox2.ValueMember = "Name";
            // 
            // parameterItemsBindingSource1
            // 
            this.parameterItemsBindingSource1.AllowNew = true;
            this.parameterItemsBindingSource1.DataSource = typeof(SiriusModel.InOut.CropParameterItem);
            // 
            // linkLabel1Name1
            // 
            this.linkLabel1Name1.AutoSize = true;
            this.linkLabel1Name1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "Name", true));
            this.linkLabel1Name1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel1Name1.Location = new System.Drawing.Point(131, 0);
            this.linkLabel1Name1.Name = "linkLabel1Name1";
            this.linkLabel1Name1.Size = new System.Drawing.Size(733, 13);
            this.linkLabel1Name1.TabIndex = 21;
            this.linkLabel1Name1.TabStop = true;
            this.linkLabel1Name1.Text = "linkLabel11";
            this.linkLabel1Name1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel1Name1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1Name1_LinkClicked);
            // 
            // tabPageNormalText
            // 
            this.tabPageNormalText.AutoScroll = true;
            this.tabPageNormalText.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageNormalText.Controls.Add(this.pagesControl1);
            this.tabPageNormalText.Location = new System.Drawing.Point(4, 22);
            this.tabPageNormalText.Name = "tabPageNormalText";
            this.tabPageNormalText.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNormalText.Size = new System.Drawing.Size(873, 484);
            this.tabPageNormalText.TabIndex = 2;
            this.tabPageNormalText.Tag = "Normal";
            this.tabPageNormalText.Text = "Text output";
            // 
            // pagesControl1
            // 
            this.pagesControl1.AutoScroll = true;
            this.pagesControl1.BackColor = System.Drawing.SystemColors.Window;
            this.pagesControl1.Book = null;
            this.pagesControl1.DataBindings.Add(new System.Windows.Forms.Binding("Book", this.runItemsBindingSource1, "NormalBook", true));
            this.pagesControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagesControl1.Location = new System.Drawing.Point(3, 3);
            this.pagesControl1.Name = "pagesControl1";
            this.pagesControl1.Size = new System.Drawing.Size(867, 478);
            this.pagesControl1.TabIndex = 0;
            // 
            // tabPageGraph
            // 
            this.tabPageGraph.AutoScroll = true;
            this.tabPageGraph.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageGraph.Controls.Add(this.button1);
            this.tabPageGraph.Controls.Add(this.chart1);
            this.tabPageGraph.Location = new System.Drawing.Point(4, 22);
            this.tabPageGraph.Name = "tabPageGraph";
            this.tabPageGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGraph.Size = new System.Drawing.Size(873, 484);
            this.tabPageGraph.TabIndex = 7;
            this.tabPageGraph.Tag = "All";
            this.tabPageGraph.Text = "Graphs";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(2, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "loadGraph";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chart1
            // 
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX2.LineColor = System.Drawing.Color.DarkRed;
            chartArea1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Top;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(3, 3);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(867, 478);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // tabPageMultiSettings
            // 
            this.tabPageMultiSettings.AutoScroll = true;
            this.tabPageMultiSettings.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageMultiSettings.Controls.Add(this.dataGridView1);
            this.tabPageMultiSettings.Controls.Add(this.tableLayoutPanel5);
            this.tabPageMultiSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageMultiSettings.Name = "tabPageMultiSettings";
            this.tabPageMultiSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMultiSettings.Size = new System.Drawing.Size(873, 484);
            this.tabPageMultiSettings.TabIndex = 3;
            this.tabPageMultiSettings.Tag = "Multi";
            this.tabPageMultiSettings.Text = "Settings";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.ManagementName,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.DailyOutputFileName});
            this.dataGridView1.DataSource = this.multiRunsBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 266);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(867, 215);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError_1);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderClicked);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ExperimentItem";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn1.HeaderText = "Experiment";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 84;
            // 
            // ManagementName
            // 
            this.ManagementName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ManagementName.DataPropertyName = "ManagementItem";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            this.ManagementName.DefaultCellStyle = dataGridViewCellStyle2;
            this.ManagementName.HeaderText = "Management";
            this.ManagementName.Name = "ManagementName";
            this.ManagementName.ReadOnly = true;
            this.ManagementName.Width = 94;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ParameterItem";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn2.HeaderText = "Non-varietal parameters";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.Width = 132;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "RunOptionItem";
            this.dataGridViewTextBoxColumn3.DataSource = this.runOptionItemsBindingSource1;
            this.dataGridViewTextBoxColumn3.DisplayMember = "Name";
            this.dataGridViewTextBoxColumn3.DisplayStyleForCurrentCellOnly = true;
            this.dataGridViewTextBoxColumn3.HeaderText = "Run option";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn3.ValueMember = "Name";
            this.dataGridViewTextBoxColumn3.Width = 78;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "SiteItem";
            this.dataGridViewTextBoxColumn4.DataSource = this.siteItemsBindingSource1;
            this.dataGridViewTextBoxColumn4.DisplayMember = "Name";
            this.dataGridViewTextBoxColumn4.DisplayStyleForCurrentCellOnly = true;
            this.dataGridViewTextBoxColumn4.HeaderText = "Site";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn4.ValueMember = "Name";
            this.dataGridViewTextBoxColumn4.Width = 50;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "SoilItem";
            this.dataGridViewTextBoxColumn5.DataSource = this.soilItemsBindingSource1;
            this.dataGridViewTextBoxColumn5.DisplayMember = "Name";
            this.dataGridViewTextBoxColumn5.DisplayStyleForCurrentCellOnly = true;
            this.dataGridViewTextBoxColumn5.HeaderText = "Soil";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn5.ValueMember = "Name";
            this.dataGridViewTextBoxColumn5.Width = 49;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "VarietyItem";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGray;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn6.HeaderText = "Variety";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn6.Width = 64;
            // 
            // DailyOutputFileName
            // 
            this.DailyOutputFileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DailyOutputFileName.DataPropertyName = "DailyOutputFileName";
            this.DailyOutputFileName.HeaderText = "Daily output file";
            this.DailyOutputFileName.Name = "DailyOutputFileName";
            this.DailyOutputFileName.ReadOnly = true;
            // 
            // multiRunsBindingSource
            // 
            this.multiRunsBindingSource.DataMember = "MultiRuns";
            this.multiRunsBindingSource.DataSource = this.runItemsBindingSource1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoScroll = true;
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.ColumnCount = 6;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.label62, 0, 10);
            this.tableLayoutPanel5.Controls.Add(this.label31, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label30, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.label33, 1, 6);
            this.tableLayoutPanel5.Controls.Add(this.label32, 2, 6);
            this.tableLayoutPanel5.Controls.Add(this.label34, 3, 6);
            this.tableLayoutPanel5.Controls.Add(this.label28, 4, 6);
            this.tableLayoutPanel5.Controls.Add(this.label35, 5, 6);
            this.tableLayoutPanel5.Controls.Add(this.label26, 0, 7);
            this.tableLayoutPanel5.Controls.Add(this.label36, 1, 7);
            this.tableLayoutPanel5.Controls.Add(this.label29, 2, 7);
            this.tableLayoutPanel5.Controls.Add(this.label37, 3, 7);
            this.tableLayoutPanel5.Controls.Add(this.label27, 4, 7);
            this.tableLayoutPanel5.Controls.Add(this.label38, 5, 7);
            this.tableLayoutPanel5.Controls.Add(this.label25, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel5, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.label39, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.checkBox1, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.label52, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.checkBox2, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.label53, 2, 4);
            this.tableLayoutPanel5.Controls.Add(this.textBox10, 3, 4);
            this.tableLayoutPanel5.Controls.Add(this.label54, 4, 4);
            this.tableLayoutPanel5.Controls.Add(this.textBox11, 5, 4);
            this.tableLayoutPanel5.Controls.Add(this.label41, 2, 3);
            this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel3, 3, 3);
            this.tableLayoutPanel5.Controls.Add(this.linkLabelName2, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.comboBox19, 3, 9);
            this.tableLayoutPanel5.Controls.Add(this.experimentNameComboBoxRunMulti, 1, 9);
            this.tableLayoutPanel5.Controls.Add(this.label59, 0, 9);
            this.tableLayoutPanel5.Controls.Add(this.label60, 2, 9);
            this.tableLayoutPanel5.Controls.Add(this.label63, 2, 10);
            this.tableLayoutPanel5.Controls.Add(this.comboBox20, 1, 10);
            this.tableLayoutPanel5.Controls.Add(this.comboBox21, 3, 10);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 12;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(867, 263);
            this.tableLayoutPanel5.TabIndex = 3;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label62.Location = new System.Drawing.Point(3, 223);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(97, 20);
            this.label62.TabIndex = 4;
            this.label62.Text = "Non-varietal:";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label31.Location = new System.Drawing.Point(3, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(97, 13);
            this.label31.TabIndex = 0;
            this.label31.Text = "Run name:";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label30.Location = new System.Drawing.Point(3, 131);
            this.label30.MinimumSize = new System.Drawing.Size(0, 26);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(97, 26);
            this.label30.TabIndex = 2;
            this.label30.Text = "Management:";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "NormalManagementItem", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label33.Location = new System.Drawing.Point(106, 134);
            this.label33.Margin = new System.Windows.Forms.Padding(3);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(200, 20);
            this.label33.TabIndex = 11;
            this.label33.Text = "label33";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Location = new System.Drawing.Point(312, 131);
            this.label32.MinimumSize = new System.Drawing.Size(0, 26);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(125, 26);
            this.label32.TabIndex = 4;
            this.label32.Text = "Non-varietal parameters:";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "NormalParameterItem", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label34.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label34.Location = new System.Drawing.Point(443, 134);
            this.label34.Margin = new System.Windows.Forms.Padding(3);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(132, 20);
            this.label34.TabIndex = 12;
            this.label34.Text = "label34";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label28.Location = new System.Drawing.Point(581, 131);
            this.label28.MinimumSize = new System.Drawing.Size(0, 26);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(125, 26);
            this.label28.TabIndex = 6;
            this.label28.Text = "Run option:";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "NormalRunOptionItem", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label35.Location = new System.Drawing.Point(712, 134);
            this.label35.Margin = new System.Windows.Forms.Padding(3);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(205, 20);
            this.label35.TabIndex = 13;
            this.label35.Text = "label35";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label26.Location = new System.Drawing.Point(3, 157);
            this.label26.MinimumSize = new System.Drawing.Size(0, 26);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(97, 26);
            this.label26.TabIndex = 8;
            this.label26.Text = "Site:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "NormalSiteItem", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label36.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label36.Location = new System.Drawing.Point(106, 160);
            this.label36.Margin = new System.Windows.Forms.Padding(3);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(200, 20);
            this.label36.TabIndex = 14;
            this.label36.Text = "label36";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Location = new System.Drawing.Point(312, 157);
            this.label29.MinimumSize = new System.Drawing.Size(0, 26);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(125, 26);
            this.label29.TabIndex = 10;
            this.label29.Text = "Soil:";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "NormalSoilItem", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label37.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label37.Location = new System.Drawing.Point(443, 160);
            this.label37.Margin = new System.Windows.Forms.Padding(3);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(132, 20);
            this.label37.TabIndex = 15;
            this.label37.Text = "label37";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label27.Location = new System.Drawing.Point(581, 157);
            this.label27.MinimumSize = new System.Drawing.Size(0, 26);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(125, 26);
            this.label27.TabIndex = 2;
            this.label27.Text = "Variety:";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "NormalVarietyItem", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label38.Location = new System.Drawing.Point(712, 160);
            this.label38.Margin = new System.Windows.Forms.Padding(3);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(205, 20);
            this.label38.TabIndex = 16;
            this.label38.Text = "label38";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label25.Location = new System.Drawing.Point(3, 13);
            this.label25.MinimumSize = new System.Drawing.Size(0, 26);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(97, 26);
            this.label25.TabIndex = 3;
            this.label25.Text = "Batch output file:";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.SetColumnSpan(this.flowLayoutPanel5, 5);
            this.flowLayoutPanel5.Controls.Add(this.linkLabel4);
            this.flowLayoutPanel5.Controls.Add(this.label44);
            this.flowLayoutPanel5.Controls.Add(this.linkLabel7);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(103, 13);
            this.flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(817, 26);
            this.flowLayoutPanel5.TabIndex = 30;
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "MultiOutputDirectory", true));
            this.linkLabel4.Location = new System.Drawing.Point(0, 0);
            this.linkLabel4.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabel4.MinimumSize = new System.Drawing.Size(0, 26);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(55, 26);
            this.linkLabel4.TabIndex = 3;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Tag = "Multi";
            this.linkLabel4.Text = "linkLabel4";
            this.linkLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.outputDirectory_Click);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(55, 0);
            this.label44.Margin = new System.Windows.Forms.Padding(0);
            this.label44.MinimumSize = new System.Drawing.Size(0, 26);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(12, 26);
            this.label44.TabIndex = 19;
            this.label44.Text = "\\";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkLabel7
            // 
            this.linkLabel7.AutoSize = true;
            this.linkLabel7.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "MultiOutputFileName", true));
            this.linkLabel7.LinkColor = System.Drawing.Color.Green;
            this.linkLabel7.Location = new System.Drawing.Point(70, 0);
            this.linkLabel7.MinimumSize = new System.Drawing.Size(0, 26);
            this.linkLabel7.Name = "linkLabel7";
            this.linkLabel7.Size = new System.Drawing.Size(55, 26);
            this.linkLabel7.TabIndex = 20;
            this.linkLabel7.TabStop = true;
            this.linkLabel7.Text = "linkLabel7";
            this.linkLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel7.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel7_LinkClicked);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label39.Location = new System.Drawing.Point(3, 59);
            this.label39.MinimumSize = new System.Drawing.Size(0, 26);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(97, 26);
            this.label39.TabIndex = 17;
            this.label39.Text = "Export daily output:";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.runItemsBindingSource1, "MultiExportNormalRuns", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox1.Location = new System.Drawing.Point(106, 62);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(200, 20);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label52.Location = new System.Drawing.Point(3, 85);
            this.label52.MinimumSize = new System.Drawing.Size(0, 26);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(97, 26);
            this.label52.TabIndex = 23;
            this.label52.Text = "Multi year:";
            this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.runItemsBindingSource1, "MultiMultiYear", true));
            this.checkBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox2.Location = new System.Drawing.Point(106, 88);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(200, 20);
            this.checkBox2.TabIndex = 24;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.chekedvalued);
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.runItemsBindingSource1, "MultiMultiYear", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label53.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label53.Location = new System.Drawing.Point(312, 85);
            this.label53.MinimumSize = new System.Drawing.Size(125, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(125, 26);
            this.label53.TabIndex = 25;
            this.label53.Text = "First sowing year:";
            this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox10
            // 
            this.textBox10.AccessibleName = "textB";
            this.textBox10.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "MultiFirstYear", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox10.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.runItemsBindingSource1, "MultiMultiYear", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox10.Location = new System.Drawing.Point(443, 88);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(132, 20);
            this.textBox10.TabIndex = 27;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.runItemsBindingSource1, "MultiMultiYear", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label54.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label54.Location = new System.Drawing.Point(581, 85);
            this.label54.MinimumSize = new System.Drawing.Size(125, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(125, 26);
            this.label54.TabIndex = 26;
            this.label54.Text = "Last sowing year:";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox11
            // 
            this.textBox11.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "MultiLastYear", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox11.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.runItemsBindingSource1, "MultiMultiYear", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox11.Location = new System.Drawing.Point(712, 88);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(205, 20);
            this.textBox11.TabIndex = 28;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.runItemsBindingSource1, "MultiExportNormalRuns", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label41.Location = new System.Drawing.Point(312, 59);
            this.label41.MinimumSize = new System.Drawing.Size(0, 26);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(125, 26);
            this.label41.TabIndex = 31;
            this.label41.Text = "Daily output pattern:";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel5.SetColumnSpan(this.flowLayoutPanel3, 3);
            this.flowLayoutPanel3.Controls.Add(this.linkLabel9);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(440, 59);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(480, 26);
            this.flowLayoutPanel3.TabIndex = 32;
            // 
            // linkLabel9
            // 
            this.linkLabel9.AutoSize = true;
            this.linkLabel9.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.runItemsBindingSource1, "MultiExportNormalRuns", true));
            this.linkLabel9.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "MultiDailyOutputPattern", true));
            this.linkLabel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel9.LinkColor = System.Drawing.Color.Green;
            this.linkLabel9.Location = new System.Drawing.Point(3, 0);
            this.linkLabel9.MinimumSize = new System.Drawing.Size(0, 26);
            this.linkLabel9.Name = "linkLabel9";
            this.linkLabel9.Size = new System.Drawing.Size(55, 26);
            this.linkLabel9.TabIndex = 2;
            this.linkLabel9.TabStop = true;
            this.linkLabel9.Text = "linkLabel9";
            this.linkLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel9.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel9_LinkClicked);
            // 
            // linkLabelName2
            // 
            this.linkLabelName2.AutoSize = true;
            this.tableLayoutPanel5.SetColumnSpan(this.linkLabelName2, 5);
            this.linkLabelName2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "Name", true));
            this.linkLabelName2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabelName2.Location = new System.Drawing.Point(106, 0);
            this.linkLabelName2.Name = "linkLabelName2";
            this.linkLabelName2.Size = new System.Drawing.Size(811, 13);
            this.linkLabelName2.TabIndex = 33;
            this.linkLabelName2.TabStop = true;
            this.linkLabelName2.Text = "linkLabel12";
            this.linkLabelName2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabelName2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1Name1_LinkClicked);
            // 
            // comboBox19
            // 
            this.comboBox19.DisplayMember = "Name";
            this.comboBox19.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox19.FormattingEnabled = true;
            this.comboBox19.Location = new System.Drawing.Point(443, 206);
            this.comboBox19.Name = "comboBox19";
            this.comboBox19.Size = new System.Drawing.Size(132, 21);
            this.comboBox19.TabIndex = 34;
            this.comboBox19.SelectedIndexChanged += new System.EventHandler(this.comboBox19_SelectedIndexChanged);
            // 
            // experimentNameComboBoxRunMulti
            // 
            this.experimentNameComboBoxRunMulti.DataSource = this.experimentNameBindingSourceRun;
            this.experimentNameComboBoxRunMulti.DisplayMember = "Name";
            this.experimentNameComboBoxRunMulti.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.experimentNameComboBoxRunMulti.FormattingEnabled = true;
            this.experimentNameComboBoxRunMulti.Location = new System.Drawing.Point(106, 206);
            this.experimentNameComboBoxRunMulti.Name = "experimentNameComboBoxRunMulti";
            this.experimentNameComboBoxRunMulti.Size = new System.Drawing.Size(200, 21);
            this.experimentNameComboBoxRunMulti.TabIndex = 21;
            this.experimentNameComboBoxRunMulti.SelectedIndexChanged += new System.EventHandler(this.experimentNameComboBoxRunMulti_SelectedIndexChanged);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label59.Location = new System.Drawing.Point(3, 203);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(97, 20);
            this.label59.TabIndex = 0;
            this.label59.Text = "Experiment:";
            this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label60.Location = new System.Drawing.Point(312, 203);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(125, 20);
            this.label60.TabIndex = 0;
            this.label60.Text = "Management:";
            this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label63.Location = new System.Drawing.Point(312, 223);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(125, 20);
            this.label63.TabIndex = 35;
            this.label63.Text = "Variety:";
            this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox20
            // 
            this.comboBox20.DataSource = this.parameterItemsBindingSource1;
            this.comboBox20.DisplayMember = "name";
            this.comboBox20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox20.FormattingEnabled = true;
            this.comboBox20.Location = new System.Drawing.Point(106, 226);
            this.comboBox20.Name = "comboBox20";
            this.comboBox20.Size = new System.Drawing.Size(200, 21);
            this.comboBox20.TabIndex = 36;
            this.comboBox20.SelectionChangeCommitted += new System.EventHandler(this.comboBox20_SelectedIndexChanged);
            // 
            // comboBox21
            // 
            this.comboBox21.DataSource = this.varietyItemsBindingSource1;
            this.comboBox21.DisplayMember = "name";
            this.comboBox21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox21.FormattingEnabled = true;
            this.comboBox21.Location = new System.Drawing.Point(443, 226);
            this.comboBox21.Name = "comboBox21";
            this.comboBox21.Size = new System.Drawing.Size(132, 21);
            this.comboBox21.TabIndex = 37;
            this.comboBox21.SelectionChangeCommitted += new System.EventHandler(this.comboBox21_SelectedIndexChanged);
            // 
            // tabPageMultiText
            // 
            this.tabPageMultiText.AutoScroll = true;
            this.tabPageMultiText.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageMultiText.Controls.Add(this.bookControl1);
            this.tabPageMultiText.Location = new System.Drawing.Point(4, 22);
            this.tabPageMultiText.Name = "tabPageMultiText";
            this.tabPageMultiText.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMultiText.Size = new System.Drawing.Size(873, 484);
            this.tabPageMultiText.TabIndex = 4;
            this.tabPageMultiText.Tag = "Multi";
            this.tabPageMultiText.Text = "Text output";
            // 
            // bookControl1
            // 
            this.bookControl1.AutoScroll = true;
            this.bookControl1.BackColor = System.Drawing.SystemColors.Window;
            this.bookControl1.Book = null;
            this.bookControl1.DataBindings.Add(new System.Windows.Forms.Binding("Book", this.runItemsBindingSource1, "MultiBook", true));
            this.bookControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bookControl1.Location = new System.Drawing.Point(3, 3);
            this.bookControl1.Name = "bookControl1";
            this.bookControl1.Size = new System.Drawing.Size(867, 478);
            this.bookControl1.TabIndex = 0;
            // 
            // tabPageSensitivitySettings
            // 
            this.tabPageSensitivitySettings.AutoScroll = true;
            this.tabPageSensitivitySettings.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageSensitivitySettings.Controls.Add(this.dataGridView2);
            this.tabPageSensitivitySettings.Controls.Add(this.tableLayoutPanel6);
            this.tabPageSensitivitySettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSensitivitySettings.Name = "tabPageSensitivitySettings";
            this.tabPageSensitivitySettings.Size = new System.Drawing.Size(873, 484);
            this.tabPageSensitivitySettings.TabIndex = 5;
            this.tabPageSensitivitySettings.Tag = "Sensitivity";
            this.tabPageSensitivitySettings.Text = "Settings";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.parameterIDTextBoxColumn7,
            this.modeDataGridViewTextBoxColumn,
            this.NbStep,
            this.Min,
            this.Max});
            this.dataGridView2.DataSource = this.sensitivityRunsBindingSource;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 164);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(873, 320);
            this.dataGridView2.TabIndex = 5;
            // 
            // parameterIDTextBoxColumn7
            // 
            this.parameterIDTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.parameterIDTextBoxColumn7.DataPropertyName = "ParameterID";
            this.parameterIDTextBoxColumn7.DisplayStyleForCurrentCellOnly = true;
            this.parameterIDTextBoxColumn7.HeaderText = "Parameter name";
            this.parameterIDTextBoxColumn7.MinimumWidth = 200;
            this.parameterIDTextBoxColumn7.Name = "parameterIDTextBoxColumn7";
            this.parameterIDTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.parameterIDTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.parameterIDTextBoxColumn7.Width = 200;
            // 
            // modeDataGridViewTextBoxColumn
            // 
            this.modeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.modeDataGridViewTextBoxColumn.DataPropertyName = "Mode";
            this.modeDataGridViewTextBoxColumn.DisplayStyleForCurrentCellOnly = true;
            this.modeDataGridViewTextBoxColumn.HeaderText = "Mode";
            this.modeDataGridViewTextBoxColumn.MinimumWidth = 120;
            this.modeDataGridViewTextBoxColumn.Name = "modeDataGridViewTextBoxColumn";
            this.modeDataGridViewTextBoxColumn.Width = 120;
            // 
            // NbStep
            // 
            this.NbStep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.NbStep.DataPropertyName = "NbStep";
            this.NbStep.HeaderText = "# step";
            this.NbStep.Name = "NbStep";
            this.NbStep.Width = 58;
            // 
            // Min
            // 
            this.Min.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Min.DataPropertyName = "Min";
            this.Min.HeaderText = "Minimum";
            this.Min.Name = "Min";
            this.Min.Width = 73;
            // 
            // Max
            // 
            this.Max.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Max.DataPropertyName = "Max";
            this.Max.HeaderText = "Maximum";
            this.Max.Name = "Max";
            this.Max.Width = 76;
            // 
            // sensitivityRunsBindingSource
            // 
            this.sensitivityRunsBindingSource.DataMember = "SensitivityRuns";
            this.sensitivityRunsBindingSource.DataSource = this.runItemsBindingSource1;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoScroll = true;
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.ColumnCount = 6;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.checkBox5, 3, 3);
            this.tableLayoutPanel6.Controls.Add(this.label49, 2, 3);
            this.tableLayoutPanel6.Controls.Add(this.checkBox4, 1, 5);
            this.tableLayoutPanel6.Controls.Add(this.label56, 3, 5);
            this.tableLayoutPanel6.Controls.Add(this.label57, 2, 5);
            this.tableLayoutPanel6.Controls.Add(this.label50, 5, 5);
            this.tableLayoutPanel6.Controls.Add(this.label55, 4, 5);
            this.tableLayoutPanel6.Controls.Add(this.label47, 0, 5);
            this.tableLayoutPanel6.Controls.Add(this.linkLabel10, 5, 3);
            this.tableLayoutPanel6.Controls.Add(this.label46, 4, 3);
            this.tableLayoutPanel6.Controls.Add(this.checkBox3, 1, 3);
            this.tableLayoutPanel6.Controls.Add(this.label45, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.label42, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label40, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.flowLayoutPanel6, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.linkLabeName3, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 7;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(873, 164);
            this.tableLayoutPanel6.TabIndex = 4;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.runItemsBindingSource1, "SensitivityOneByOne", true));
            this.checkBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox5.Location = new System.Drawing.Point(230, 75);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(35, 20);
            this.checkBox5.TabIndex = 31;
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label49.Location = new System.Drawing.Point(136, 72);
            this.label49.MinimumSize = new System.Drawing.Size(0, 26);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(88, 26);
            this.label49.TabIndex = 30;
            this.label49.Text = "One by one:";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.runItemsBindingSource1, "MultiMultiYear", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox4.Enabled = false;
            this.checkBox4.Location = new System.Drawing.Point(115, 121);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(15, 20);
            this.checkBox4.TabIndex = 28;
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "MultiFirstYear", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label56.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.runItemsBindingSource1, "MultiMultiYear", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label56.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label56.Location = new System.Drawing.Point(230, 118);
            this.label56.MinimumSize = new System.Drawing.Size(0, 26);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(35, 26);
            this.label56.TabIndex = 27;
            this.label56.Text = "label1";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.runItemsBindingSource1, "MultiMultiYear", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label57.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label57.Location = new System.Drawing.Point(136, 118);
            this.label57.MinimumSize = new System.Drawing.Size(0, 26);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(88, 26);
            this.label57.TabIndex = 26;
            this.label57.Text = "First sowing year:";
            this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "MultiLastYear", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label50.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.runItemsBindingSource1, "MultiMultiYear", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label50.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label50.Location = new System.Drawing.Point(379, 118);
            this.label50.MinimumSize = new System.Drawing.Size(0, 26);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(491, 26);
            this.label50.TabIndex = 25;
            this.label50.Text = "label1";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.runItemsBindingSource1, "MultiMultiYear", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label55.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label55.Location = new System.Drawing.Point(271, 118);
            this.label55.MinimumSize = new System.Drawing.Size(0, 26);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(102, 26);
            this.label55.TabIndex = 24;
            this.label55.Text = "Last sowing year:";
            this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label47.Location = new System.Drawing.Point(3, 118);
            this.label47.MinimumSize = new System.Drawing.Size(0, 26);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(106, 26);
            this.label47.TabIndex = 22;
            this.label47.Text = "Multi year:";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkLabel10
            // 
            this.linkLabel10.AutoSize = true;
            this.linkLabel10.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "SensitivityDailyOutputPattern", true));
            this.linkLabel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel10.LinkColor = System.Drawing.Color.Green;
            this.linkLabel10.Location = new System.Drawing.Point(379, 72);
            this.linkLabel10.MinimumSize = new System.Drawing.Size(0, 26);
            this.linkLabel10.Name = "linkLabel10";
            this.linkLabel10.Size = new System.Drawing.Size(491, 26);
            this.linkLabel10.TabIndex = 21;
            this.linkLabel10.TabStop = true;
            this.linkLabel10.Text = "linkLabel10";
            this.linkLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel10.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel10_LinkClicked);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label46.Location = new System.Drawing.Point(271, 72);
            this.label46.MinimumSize = new System.Drawing.Size(0, 26);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(102, 26);
            this.label46.TabIndex = 20;
            this.label46.Text = "Daily output pattern:";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.runItemsBindingSource1, "SensitivityExportNormalRuns", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox3.Location = new System.Drawing.Point(115, 75);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(15, 20);
            this.checkBox3.TabIndex = 19;
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label45.Location = new System.Drawing.Point(3, 72);
            this.label45.MinimumSize = new System.Drawing.Size(0, 26);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(106, 26);
            this.label45.TabIndex = 10;
            this.label45.Text = "Export daily output:";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label42.Location = new System.Drawing.Point(3, 0);
            this.label42.MinimumSize = new System.Drawing.Size(0, 26);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(106, 26);
            this.label42.TabIndex = 9;
            this.label42.Text = "Run name:";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label40.Location = new System.Drawing.Point(3, 26);
            this.label40.MinimumSize = new System.Drawing.Size(0, 26);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(106, 26);
            this.label40.TabIndex = 3;
            this.label40.Text = "Sensitivity output file:";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.SetColumnSpan(this.flowLayoutPanel6, 5);
            this.flowLayoutPanel6.Controls.Add(this.linkLabel5);
            this.flowLayoutPanel6.Controls.Add(this.label48);
            this.flowLayoutPanel6.Controls.Add(this.linkLabel8);
            this.flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel6.Location = new System.Drawing.Point(112, 26);
            this.flowLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(761, 26);
            this.flowLayoutPanel6.TabIndex = 8;
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "SensitivityOutputDirectory", true));
            this.linkLabel5.Location = new System.Drawing.Point(0, 0);
            this.linkLabel5.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabel5.MinimumSize = new System.Drawing.Size(0, 26);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(55, 26);
            this.linkLabel5.TabIndex = 3;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Tag = "Sensitivity";
            this.linkLabel5.Text = "linkLabel5";
            this.linkLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.outputDirectory_Click);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(55, 0);
            this.label48.Margin = new System.Windows.Forms.Padding(0);
            this.label48.MinimumSize = new System.Drawing.Size(0, 26);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(12, 26);
            this.label48.TabIndex = 4;
            this.label48.Text = "\\";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkLabel8
            // 
            this.linkLabel8.AutoSize = true;
            this.linkLabel8.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "SensitivityOutputFileName", true));
            this.linkLabel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel8.LinkColor = System.Drawing.Color.Green;
            this.linkLabel8.Location = new System.Drawing.Point(70, 0);
            this.linkLabel8.MinimumSize = new System.Drawing.Size(0, 26);
            this.linkLabel8.Name = "linkLabel8";
            this.linkLabel8.Size = new System.Drawing.Size(55, 26);
            this.linkLabel8.TabIndex = 5;
            this.linkLabel8.TabStop = true;
            this.linkLabel8.Text = "linkLabel8";
            this.linkLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel8.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel8_LinkClicked);
            // 
            // linkLabeName3
            // 
            this.linkLabeName3.AutoSize = true;
            this.tableLayoutPanel6.SetColumnSpan(this.linkLabeName3, 5);
            this.linkLabeName3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "Name", true));
            this.linkLabeName3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabeName3.Location = new System.Drawing.Point(115, 0);
            this.linkLabeName3.Name = "linkLabeName3";
            this.linkLabeName3.Size = new System.Drawing.Size(755, 26);
            this.linkLabeName3.TabIndex = 29;
            this.linkLabeName3.TabStop = true;
            this.linkLabeName3.Text = "linkLabel13";
            this.linkLabeName3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabeName3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1Name1_LinkClicked);
            // 
            // tabPageSensitivityText
            // 
            this.tabPageSensitivityText.AutoScroll = true;
            this.tabPageSensitivityText.BackColor = System.Drawing.SystemColors.Window;
            this.tabPageSensitivityText.Controls.Add(this.bookControl2);
            this.tabPageSensitivityText.Location = new System.Drawing.Point(4, 22);
            this.tabPageSensitivityText.Name = "tabPageSensitivityText";
            this.tabPageSensitivityText.Size = new System.Drawing.Size(873, 484);
            this.tabPageSensitivityText.TabIndex = 6;
            this.tabPageSensitivityText.Tag = "Sensitivity";
            this.tabPageSensitivityText.Text = "Text output";
            // 
            // bookControl2
            // 
            this.bookControl2.AutoScroll = true;
            this.bookControl2.BackColor = System.Drawing.SystemColors.Window;
            this.bookControl2.Book = null;
            this.bookControl2.DataBindings.Add(new System.Windows.Forms.Binding("Book", this.runItemsBindingSource1, "SensitivityBook", true));
            this.bookControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bookControl2.Location = new System.Drawing.Point(0, 0);
            this.bookControl2.Name = "bookControl2";
            this.bookControl2.Size = new System.Drawing.Size(873, 484);
            this.bookControl2.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.radioButtonNormal);
            this.flowLayoutPanel2.Controls.Add(this.radioButtonMulti);
            this.flowLayoutPanel2.Controls.Add(this.radioButtonSensitivity);
            this.flowLayoutPanel2.Controls.Add(this.buttonRun);
            this.flowLayoutPanel2.Controls.Add(this.buttonExport);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(881, 29);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // radioButtonNormal
            // 
            this.radioButtonNormal.AutoSize = true;
            this.radioButtonNormal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonNormal.Location = new System.Drawing.Point(3, 3);
            this.radioButtonNormal.Name = "radioButtonNormal";
            this.radioButtonNormal.Size = new System.Drawing.Size(72, 23);
            this.radioButtonNormal.TabIndex = 2;
            this.radioButtonNormal.Tag = "Normal";
            this.radioButtonNormal.Text = "Single run";
            this.radioButtonNormal.UseVisualStyleBackColor = true;
            this.radioButtonNormal.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // radioButtonMulti
            // 
            this.radioButtonMulti.AutoSize = true;
            this.radioButtonMulti.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonMulti.Location = new System.Drawing.Point(81, 3);
            this.radioButtonMulti.Name = "radioButtonMulti";
            this.radioButtonMulti.Size = new System.Drawing.Size(71, 23);
            this.radioButtonMulti.TabIndex = 4;
            this.radioButtonMulti.Tag = "Multi";
            this.radioButtonMulti.Text = "Batch run";
            this.radioButtonMulti.UseVisualStyleBackColor = true;
            this.radioButtonMulti.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // radioButtonSensitivity
            // 
            this.radioButtonSensitivity.AutoSize = true;
            this.radioButtonSensitivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButtonSensitivity.Location = new System.Drawing.Point(158, 3);
            this.radioButtonSensitivity.Name = "radioButtonSensitivity";
            this.radioButtonSensitivity.Size = new System.Drawing.Size(112, 23);
            this.radioButtonSensitivity.TabIndex = 3;
            this.radioButtonSensitivity.Tag = "Sensitivity";
            this.radioButtonSensitivity.Text = "Sensitivity analysis";
            this.radioButtonSensitivity.UseVisualStyleBackColor = true;
            this.radioButtonSensitivity.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // buttonRun
            // 
            this.buttonRun.AutoSize = true;
            this.buttonRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRun.Enabled = false;
            this.buttonRun.Location = new System.Drawing.Point(276, 3);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(37, 23);
            this.buttonRun.TabIndex = 1;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.AutoSize = true;
            this.buttonExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonExport.Enabled = false;
            this.buttonExport.Location = new System.Drawing.Point(319, 3);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(47, 23);
            this.buttonExport.TabIndex = 2;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.Controls.Add(this.label51, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.textBox9, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(881, 77);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label51.Location = new System.Drawing.Point(3, 0);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(59, 77);
            this.label51.TabIndex = 0;
            this.label51.Text = "Comments:";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox9
            // 
            this.textBox9.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runItemsBindingSource1, "Comments", true));
            this.textBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox9.Location = new System.Drawing.Point(68, 3);
            this.textBox9.Multiline = true;
            this.textBox9.Name = "textBox9";
            this.textBox9.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox9.Size = new System.Drawing.Size(810, 71);
            this.textBox9.TabIndex = 1;
            // 
            // managementItemsBindingSource1
            // 
            this.managementItemsBindingSource1.DataSource = typeof(SiriusModel.InOut.ManagementItem);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 27);
            this.label1.TabIndex = 2;
            this.label1.Text = "Variety";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.linkLabel2, 1, 8);
            this.tableLayoutPanel3.Controls.Add(this.label9, 0, 8);
            this.tableLayoutPanel3.Controls.Add(this.label10, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.comboBox7, 1, 6);
            this.tableLayoutPanel3.Controls.Add(this.label11, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.label12, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label13, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.comboBox8, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.label14, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label15, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBox2, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.comboBox9, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label16, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.comboBox10, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.comboBox11, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.comboBox12, 1, 4);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 9;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(641, 216);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel2.Location = new System.Drawing.Point(89, 203);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(549, 13);
            this.linkLabel2.TabIndex = 3;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "linkLabel2";
            this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 203);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Output file:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 107);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 27);
            this.label10.TabIndex = 8;
            this.label10.Text = "Site:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox7
            // 
            this.comboBox7.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox7.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox7.DataSource = this.varietyItemsBindingSource1;
            this.comboBox7.DisplayMember = "Name";
            this.comboBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Location = new System.Drawing.Point(89, 164);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(549, 21);
            this.comboBox7.TabIndex = 12;
            this.comboBox7.ValueMember = "Name";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(3, 161);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 27);
            this.label11.TabIndex = 2;
            this.label11.Text = "Variety";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(3, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 27);
            this.label12.TabIndex = 6;
            this.label12.Text = "Element option:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(3, 134);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 27);
            this.label13.TabIndex = 10;
            this.label13.Text = "Soil:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox8
            // 
            this.comboBox8.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox8.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox8.DataSource = this.soilItemsBindingSource1;
            this.comboBox8.DisplayMember = "Name";
            this.comboBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox8.FormattingEnabled = true;
            this.comboBox8.Location = new System.Drawing.Point(89, 137);
            this.comboBox8.Name = "comboBox8";
            this.comboBox8.Size = new System.Drawing.Size(549, 21);
            this.comboBox8.TabIndex = 11;
            this.comboBox8.ValueMember = "Name";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(3, 26);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 27);
            this.label14.TabIndex = 2;
            this.label14.Text = "Management:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(3, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 26);
            this.label15.TabIndex = 0;
            this.label15.Text = "Name:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(89, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(549, 20);
            this.textBox2.TabIndex = 1;
            // 
            // comboBox9
            // 
            this.comboBox9.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox9.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox9.DataSource = this.managementItemsBindingSource1;
            this.comboBox9.DisplayMember = "Name";
            this.comboBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox9.FormattingEnabled = true;
            this.comboBox9.Location = new System.Drawing.Point(89, 29);
            this.comboBox9.Name = "comboBox9";
            this.comboBox9.Size = new System.Drawing.Size(549, 21);
            this.comboBox9.TabIndex = 3;
            this.comboBox9.ValueMember = "Name";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(3, 53);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(80, 27);
            this.label16.TabIndex = 4;
            this.label16.Text = "Parameter:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox10
            // 
            this.comboBox10.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox10.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox10.DataSource = this.parameterItemsBindingSource1;
            this.comboBox10.DisplayMember = "Name";
            this.comboBox10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox10.FormattingEnabled = true;
            this.comboBox10.Location = new System.Drawing.Point(89, 56);
            this.comboBox10.Name = "comboBox10";
            this.comboBox10.Size = new System.Drawing.Size(549, 21);
            this.comboBox10.TabIndex = 5;
            this.comboBox10.ValueMember = "Name";
            // 
            // comboBox11
            // 
            this.comboBox11.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox11.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox11.DataSource = this.runOptionItemsBindingSource1;
            this.comboBox11.DisplayMember = "Name";
            this.comboBox11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox11.FormattingEnabled = true;
            this.comboBox11.Location = new System.Drawing.Point(89, 83);
            this.comboBox11.Name = "comboBox11";
            this.comboBox11.Size = new System.Drawing.Size(549, 21);
            this.comboBox11.TabIndex = 7;
            this.comboBox11.ValueMember = "Name";
            // 
            // comboBox12
            // 
            this.comboBox12.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox12.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox12.DataSource = this.siteItemsBindingSource1;
            this.comboBox12.DisplayMember = "Name";
            this.comboBox12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox12.FormattingEnabled = true;
            this.comboBox12.Location = new System.Drawing.Point(89, 110);
            this.comboBox12.Name = "comboBox12";
            this.comboBox12.Size = new System.Drawing.Size(549, 21);
            this.comboBox12.TabIndex = 9;
            this.comboBox12.ValueMember = "Name";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.linkLabel3, 1, 8);
            this.tableLayoutPanel4.Controls.Add(this.label17, 0, 8);
            this.tableLayoutPanel4.Controls.Add(this.label18, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.comboBox13, 1, 6);
            this.tableLayoutPanel4.Controls.Add(this.label19, 0, 6);
            this.tableLayoutPanel4.Controls.Add(this.label20, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.label21, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.comboBox14, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.label22, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label23, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.textBox3, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.comboBox15, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label24, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.comboBox16, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.comboBox17, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.comboBox18, 1, 4);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 9;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(641, 216);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel3.Location = new System.Drawing.Point(89, 203);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(549, 13);
            this.linkLabel3.TabIndex = 3;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "linkLabel3";
            this.linkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(3, 203);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "Output file:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Location = new System.Drawing.Point(3, 107);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 27);
            this.label18.TabIndex = 8;
            this.label18.Text = "Site:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox13
            // 
            this.comboBox13.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox13.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox13.DataSource = this.varietyItemsBindingSource1;
            this.comboBox13.DisplayMember = "Name";
            this.comboBox13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox13.FormattingEnabled = true;
            this.comboBox13.Location = new System.Drawing.Point(89, 164);
            this.comboBox13.Name = "comboBox13";
            this.comboBox13.Size = new System.Drawing.Size(549, 21);
            this.comboBox13.TabIndex = 12;
            this.comboBox13.ValueMember = "Name";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Location = new System.Drawing.Point(3, 161);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 27);
            this.label19.TabIndex = 2;
            this.label19.Text = "Variety";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Location = new System.Drawing.Point(3, 80);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(80, 27);
            this.label20.TabIndex = 6;
            this.label20.Text = "Element option:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.Location = new System.Drawing.Point(3, 134);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 27);
            this.label21.TabIndex = 10;
            this.label21.Text = "Soil:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox14
            // 
            this.comboBox14.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox14.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox14.DataSource = this.soilItemsBindingSource1;
            this.comboBox14.DisplayMember = "Name";
            this.comboBox14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox14.FormattingEnabled = true;
            this.comboBox14.Location = new System.Drawing.Point(89, 137);
            this.comboBox14.Name = "comboBox14";
            this.comboBox14.Size = new System.Drawing.Size(549, 21);
            this.comboBox14.TabIndex = 11;
            this.comboBox14.ValueMember = "Name";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.Location = new System.Drawing.Point(3, 26);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(80, 27);
            this.label22.TabIndex = 2;
            this.label22.Text = "Management:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Location = new System.Drawing.Point(3, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(80, 26);
            this.label23.TabIndex = 0;
            this.label23.Text = "Name:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox3
            // 
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox3.Location = new System.Drawing.Point(89, 3);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(549, 20);
            this.textBox3.TabIndex = 1;
            // 
            // comboBox15
            // 
            this.comboBox15.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox15.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox15.DataSource = this.managementItemsBindingSource1;
            this.comboBox15.DisplayMember = "Name";
            this.comboBox15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox15.FormattingEnabled = true;
            this.comboBox15.Location = new System.Drawing.Point(89, 29);
            this.comboBox15.Name = "comboBox15";
            this.comboBox15.Size = new System.Drawing.Size(549, 21);
            this.comboBox15.TabIndex = 3;
            this.comboBox15.ValueMember = "Name";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label24.Location = new System.Drawing.Point(3, 53);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(80, 27);
            this.label24.TabIndex = 4;
            this.label24.Text = "Parameter:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox16
            // 
            this.comboBox16.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox16.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox16.DataSource = this.parameterItemsBindingSource1;
            this.comboBox16.DisplayMember = "Name";
            this.comboBox16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox16.FormattingEnabled = true;
            this.comboBox16.Location = new System.Drawing.Point(89, 56);
            this.comboBox16.Name = "comboBox16";
            this.comboBox16.Size = new System.Drawing.Size(549, 21);
            this.comboBox16.TabIndex = 5;
            this.comboBox16.ValueMember = "Name";
            // 
            // comboBox17
            // 
            this.comboBox17.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox17.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox17.DataSource = this.runOptionItemsBindingSource1;
            this.comboBox17.DisplayMember = "Name";
            this.comboBox17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox17.FormattingEnabled = true;
            this.comboBox17.Location = new System.Drawing.Point(89, 83);
            this.comboBox17.Name = "comboBox17";
            this.comboBox17.Size = new System.Drawing.Size(549, 21);
            this.comboBox17.TabIndex = 7;
            this.comboBox17.ValueMember = "Name";
            // 
            // comboBox18
            // 
            this.comboBox18.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox18.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox18.DataSource = this.siteItemsBindingSource1;
            this.comboBox18.DisplayMember = "Name";
            this.comboBox18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox18.FormattingEnabled = true;
            this.comboBox18.Location = new System.Drawing.Point(89, 110);
            this.comboBox18.Name = "comboBox18";
            this.comboBox18.Size = new System.Drawing.Size(549, 21);
            this.comboBox18.TabIndex = 9;
            this.comboBox18.ValueMember = "Name";
            // 
            // RunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 649);
            this.Controls.Add(this.splitContainer1);
            this.Name = "RunForm";
            this.TabText = "SimpleRun";
            this.Text = "Element";
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.runItemsBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.experimentNameBindingSourceRun)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControlMode.ResumeLayout(false);
            this.tabPageNormalSettings.ResumeLayout(false);
            this.tabPageNormalSettings.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.varietyItemsBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.runOptionItemsBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.siteItemsBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.soilItemsBindingSource1)).EndInit();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.parameterItemsBindingSource1)).EndInit();
            this.tabPageNormalText.ResumeLayout(false);
            this.tabPageGraph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabPageMultiSettings.ResumeLayout(false);
            this.tabPageMultiSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.multiRunsBindingSource)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.tabPageMultiText.ResumeLayout(false);
            this.tabPageSensitivitySettings.ResumeLayout(false);
            this.tabPageSensitivitySettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensitivityRunsBindingSource)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            this.tabPageSensitivityText.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.managementItemsBindingSource1)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonClear;
        private ManagementItemsBindingSource managementItemsBindingSource1;
        private NonVarietyItemsBindingSource parameterItemsBindingSource1;
        private RunOptionItemsBindingSource runOptionItemsBindingSource1;
        private SiteItemsBindingSource siteItemsBindingSource1;
        private SoilItemsBindingSource soilItemsBindingSource1;
        private VarietyItemsBindingSource varietyItemsBindingSource1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBox8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox comboBox9;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comboBox10;
        private System.Windows.Forms.ComboBox comboBox11;
        private System.Windows.Forms.ComboBox comboBox12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox comboBox13;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox comboBox14;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox comboBox15;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox comboBox16;
        private System.Windows.Forms.ComboBox comboBox17;
        private System.Windows.Forms.ComboBox comboBox18;
        private RunItemsBindingSource runItemsBindingSource1;
        private System.Windows.Forms.BindingSource multiRunsBindingSource;
        private System.Windows.Forms.BindingSource sensitivityRunsBindingSource;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.RadioButton radioButtonNormal;
        private System.Windows.Forms.RadioButton radioButtonMulti;
        private System.Windows.Forms.RadioButton radioButtonSensitivity;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Button buttonDuplicate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSort;
        private BaseBindingSource experimentNameBindingSourceRun;
        private TabControl tabControlMode;
        private TabPage tabPageNormalSettings;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label61;
        private Label label7;
        private Label label5;
        private ComboBox comboBox6;
        private Label label4;
        private Label label58;
        private ComboBox experimentNameComboBoxRunSingle;
        private Label label6;
        private Label label3;
        private ComboBox comboBox3;
        private ComboBox comboBox4;
        private ComboBox comboBox5;
        private FlowLayoutPanel flowLayoutPanel4;
        private LinkLabel linkLabel1;
        private Label label43;
        private LinkLabel linkLabel6;
        private Label label2;
        private ComboBox comboBox1;
        private Label label8;
        private ComboBox comboBox2;
        private LinkLabel linkLabel1Name1;
        private TabPage tabPageNormalText;
        private Controls.BookControl pagesControl1;
        private TabPage tabPageGraph;
        private Button button1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private TabPage tabPageMultiSettings;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn ManagementName;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewComboBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewComboBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewComboBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn DailyOutputFileName;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label62;
        private Label label31;
        private Label label30;
        private Label label33;
        private Label label32;
        private Label label34;
        private Label label28;
        private Label label35;
        private Label label26;
        private Label label36;
        private Label label29;
        private Label label37;
        private Label label27;
        private Label label38;
        private Label label25;
        private FlowLayoutPanel flowLayoutPanel5;
        private LinkLabel linkLabel4;
        private Label label44;
        private LinkLabel linkLabel7;
        private Label label39;
        private CheckBox checkBox1;
        private Label label52;
        private CheckBox checkBox2;
        private Label label53;
        private TextBox textBox10;
        private Label label54;
        private TextBox textBox11;
        private Label label41;
        private FlowLayoutPanel flowLayoutPanel3;
        private LinkLabel linkLabel9;
        private LinkLabel linkLabelName2;
        private ComboBox comboBox19;
        private ComboBox experimentNameComboBoxRunMulti;
        private Label label59;
        private Label label60;
        private Label label63;
        private ComboBox comboBox20;
        private ComboBox comboBox21;
        private TabPage tabPageMultiText;
        private Controls.BookControl bookControl1;
        private TabPage tabPageSensitivitySettings;
        private DataGridView dataGridView2;
        private DataGridViewComboBoxColumn parameterIDTextBoxColumn7;
        private DataGridViewComboBoxColumn modeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn NbStep;
        private DataGridViewTextBoxColumn Min;
        private DataGridViewTextBoxColumn Max;
        private TableLayoutPanel tableLayoutPanel6;
        private CheckBox checkBox5;
        private Label label49;
        private CheckBox checkBox4;
        private Label label56;
        private Label label57;
        private Label label50;
        private Label label55;
        private Label label47;
        private LinkLabel linkLabel10;
        private Label label46;
        private CheckBox checkBox3;
        private Label label45;
        private Label label42;
        private Label label40;
        private FlowLayoutPanel flowLayoutPanel6;
        private LinkLabel linkLabel5;
        private Label label48;
        private LinkLabel linkLabel8;
        private LinkLabel linkLabeName3;
        private TabPage tabPageSensitivityText;
        private Controls.BookControl bookControl2;
    }
}