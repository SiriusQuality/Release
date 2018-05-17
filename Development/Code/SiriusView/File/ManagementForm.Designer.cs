namespace SiriusView.File
{
    partial class ManagementForm
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
            System.Windows.Forms.Label stemDensityLabel1;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label sowingDateLabel1;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label13;
            System.Windows.Forms.Label label14;
            System.Windows.Forms.Label label15;
            System.Windows.Forms.Label nbcheckdaylabel;
            System.Windows.Forms.Label cumpcplabel;
            System.Windows.Forms.Label label28;
            System.Windows.Forms.Label label29;
            System.Windows.Forms.Label totalNiLabel1;
            System.Windows.Forms.Label topNiLabel1;
            System.Windows.Forms.Label midNiLabel1;
            System.Windows.Forms.Label label31;
            System.Windows.Forms.Label label32;
            System.Windows.Forms.Label cO2Label1;
            System.Windows.Forms.Label TargetFSNlabel;
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dateApplicationsDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.momentApplicationsDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.managementItemsBindingSource1 = new SiriusView.ManagementItemsBindingSource(this.components);
            this.experimentNameBindingSource = new SiriusView.BaseBindingSource(this.components);
            this.dateApplicationsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.growthStageApplicationsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonDuplicate = new System.Windows.Forms.Button();
            this.buttonSort = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.experimentNameComboBox = new System.Windows.Forms.ComboBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.nameLabel1 = new System.Windows.Forms.Label();
            this.experimentNameTextBox1 = new System.Windows.Forms.TextBox();
            this.experimentNameLabel1 = new System.Windows.Forms.Label();
            this.linkLabelName = new System.Windows.Forms.LinkLabel();
            this.specieslabel = new System.Windows.Forms.Label();
            this.speciescomboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.sowingDateDateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.stemDensityTextBox1 = new System.Windows.Forms.TextBox();
            this.isSowEstCheckBox = new System.Windows.Forms.CheckBox();
            this.textBoxIsRelax = new System.Windows.Forms.CheckBox();
            this.textBoxCheckDepth = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxSoilMoistThr = new System.Windows.Forms.TextBox();
            this.textBoxSoilWorkabThr = new System.Windows.Forms.TextBox();
            this.textBoxSkipDays = new System.Windows.Forms.TextBox();
            this.textBoxCheckDaysPcp = new System.Windows.Forms.TextBox();
            this.textBoxCumPcp = new System.Windows.Forms.TextBox();
            this.textBoxCheckDaysTemp = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBoxSoilFreezThr = new System.Windows.Forms.TextBox();
            this.textBoxSoilTempThr = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.midNiTextBox1 = new System.Windows.Forms.TextBox();
            this.topNiTextBox1 = new System.Windows.Forms.TextBox();
            this.totalNiTextBox1 = new System.Windows.Forms.TextBox();
            this.isWDinPercCheckBox = new System.Windows.Forms.CheckBox();
            this.deficitLabel1 = new System.Windows.Forms.Label();
            this.deficitTextBox1 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxCO2TrendSlope = new System.Windows.Forms.TextBox();
            this.CO2TrendSlopelabel = new System.Windows.Forms.Label();
            this.textBoxCO2TrendBase = new System.Windows.Forms.TextBox();
            this.CO2trendbaselabel = new System.Windows.Forms.Label();
            this.checkBoxCO2Trend = new System.Windows.Forms.CheckBox();
            this.cO2TextBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxNTrendSlope = new System.Windows.Forms.TextBox();
            this.AnnualChangeLabel = new System.Windows.Forms.Label();
            this.textBoxNTrendBase = new System.Windows.Forms.TextBox();
            this.TrendBaseYearlabel = new System.Windows.Forms.Label();
            this.textBoxNNIMult = new System.Windows.Forms.TextBox();
            this.NNImultlabel = new System.Windows.Forms.Label();
            this.textBoxNNIThr = new System.Windows.Forms.TextBox();
            this.NNithrlabel = new System.Windows.Forms.Label();
            this.checkBoxIsNNIUsed = new System.Windows.Forms.CheckBox();
            this.checkBoxNTrend = new System.Windows.Forms.CheckBox();
            this.totalNFertTextBox = new System.Windows.Forms.TextBox();
            this.totalfertNlabel = new System.Windows.Forms.Label();
            this.isTotNitrogenCheckBox = new System.Windows.Forms.CheckBox();
            this.IsPcpCheclNcheckBox = new System.Windows.Forms.CheckBox();
            this.ChecledDaysNtextBox = new System.Windows.Forms.TextBox();
            this.CumPcpThrNtextBox = new System.Windows.Forms.TextBox();
            this.maxpostponlabel = new System.Windows.Forms.Label();
            this.MaxPostponeNtextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.totalNfertlabelvalue = new System.Windows.Forms.Label();
            this.totalNfertlabel = new System.Windows.Forms.Label();
            this.totalirrigationlabelvalue = new System.Windows.Forms.Label();
            this.totalirrigationlabel = new System.Windows.Forms.Label();
            this.TargetFSNtextBox = new System.Windows.Forms.TextBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nitrogenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WaterMM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonSortDate = new System.Windows.Forms.Button();
            this.buttonAddDate = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.GrowthStageComboBox = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.nitrogenDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonSortGrowthStage = new System.Windows.Forms.Button();
            this.buttonAddGrowthStage = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            stemDensityLabel1 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            sowingDateLabel1 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            nbcheckdaylabel = new System.Windows.Forms.Label();
            cumpcplabel = new System.Windows.Forms.Label();
            label28 = new System.Windows.Forms.Label();
            label29 = new System.Windows.Forms.Label();
            totalNiLabel1 = new System.Windows.Forms.Label();
            topNiLabel1 = new System.Windows.Forms.Label();
            midNiLabel1 = new System.Windows.Forms.Label();
            label31 = new System.Windows.Forms.Label();
            label32 = new System.Windows.Forms.Label();
            cO2Label1 = new System.Windows.Forms.Label();
            TargetFSNlabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateApplicationsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.momentApplicationsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.managementItemsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.experimentNameBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateApplicationsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.growthStageApplicationsBindingSource)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // stemDensityLabel1
            // 
            stemDensityLabel1.AutoSize = true;
            stemDensityLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            stemDensityLabel1.Location = new System.Drawing.Point(298, 0);
            stemDensityLabel1.Name = "stemDensityLabel1";
            stemDensityLabel1.Size = new System.Drawing.Size(215, 26);
            stemDensityLabel1.TabIndex = 16;
            stemDensityLabel1.Text = "Sowing density (seed/m²):";
            stemDensityLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Dock = System.Windows.Forms.DockStyle.Fill;
            label11.Location = new System.Drawing.Point(605, 75);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(252, 26);
            label11.TabIndex = 39;
            label11.Text = "Cumulative precipitation threshold (mm):";
            label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Dock = System.Windows.Forms.DockStyle.Fill;
            label10.Location = new System.Drawing.Point(3, 101);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(203, 26);
            label10.TabIndex = 37;
            label10.Text = "Number of checked days for temperature:";
            label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // sowingDateLabel1
            // 
            sowingDateLabel1.AutoSize = true;
            sowingDateLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            sowingDateLabel1.Location = new System.Drawing.Point(3, 0);
            sowingDateLabel1.Name = "sowingDateLabel1";
            sowingDateLabel1.Size = new System.Drawing.Size(203, 26);
            sowingDateLabel1.TabIndex = 34;
            sowingDateLabel1.Text = "(Nominal) Sowing date:";
            sowingDateLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = System.Windows.Forms.DockStyle.Fill;
            label7.Location = new System.Drawing.Point(3, 49);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(203, 26);
            label7.TabIndex = 31;
            label7.Text = "Soil depth to be checked (cm):";
            label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Dock = System.Windows.Forms.DockStyle.Fill;
            label9.Location = new System.Drawing.Point(3, 75);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(203, 26);
            label9.TabIndex = 8;
            label9.Text = "Initial simulation days before start (days):";
            label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Dock = System.Windows.Forms.DockStyle.Fill;
            label13.Location = new System.Drawing.Point(605, 49);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(252, 26);
            label13.TabIndex = 4;
            label13.Text = "Upper soil moisture threshold (frac. of field capacity):";
            label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Dock = System.Windows.Forms.DockStyle.Fill;
            label14.Location = new System.Drawing.Point(298, 75);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(215, 26);
            label14.TabIndex = 6;
            label14.Text = "Number of checked days for precipitation:";
            label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Dock = System.Windows.Forms.DockStyle.Fill;
            label15.Location = new System.Drawing.Point(605, 101);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(252, 26);
            label15.TabIndex = 2;
            label15.Text = "Average daily air temperature threshold (°C):";
            label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nbcheckdaylabel
            // 
            nbcheckdaylabel.AutoSize = true;
            nbcheckdaylabel.Dock = System.Windows.Forms.DockStyle.Fill;
            nbcheckdaylabel.Location = new System.Drawing.Point(208, 0);
            nbcheckdaylabel.Name = "nbcheckdaylabel";
            nbcheckdaylabel.Size = new System.Drawing.Size(129, 26);
            nbcheckdaylabel.TabIndex = 31;
            nbcheckdaylabel.Text = "Number of checked days:";
            nbcheckdaylabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cumpcplabel
            // 
            cumpcplabel.AutoSize = true;
            cumpcplabel.Dock = System.Windows.Forms.DockStyle.Fill;
            cumpcplabel.Location = new System.Drawing.Point(723, 0);
            cumpcplabel.Name = "cumpcplabel";
            cumpcplabel.Size = new System.Drawing.Size(193, 26);
            cumpcplabel.TabIndex = 4;
            cumpcplabel.Text = "Cumulative precipitation threshold (mm):";
            cumpcplabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Dock = System.Windows.Forms.DockStyle.Fill;
            label28.Location = new System.Drawing.Point(3, 23);
            label28.Name = "label28";
            label28.Size = new System.Drawing.Size(129, 26);
            label28.TabIndex = 31;
            label28.Text = "Number of checked days:";
            label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Dock = System.Windows.Forms.DockStyle.Fill;
            label29.Location = new System.Drawing.Point(718, 23);
            label29.Name = "label29";
            label29.Size = new System.Drawing.Size(193, 26);
            label29.TabIndex = 4;
            label29.Text = "Cumulative precipitation threshold (mm):";
            label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // totalNiLabel1
            // 
            totalNiLabel1.AutoSize = true;
            totalNiLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            totalNiLabel1.Location = new System.Drawing.Point(3, 26);
            totalNiLabel1.Name = "totalNiLabel1";
            totalNiLabel1.Size = new System.Drawing.Size(166, 26);
            totalNiLabel1.TabIndex = 33;
            totalNiLabel1.Text = "            Total inorganic N (gN/m²):";
            totalNiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // topNiLabel1
            // 
            topNiLabel1.AutoSize = true;
            topNiLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            topNiLabel1.Location = new System.Drawing.Point(261, 26);
            topNiLabel1.Name = "topNiLabel1";
            topNiLabel1.Size = new System.Drawing.Size(160, 26);
            topNiLabel1.TabIndex = 38;
            topNiLabel1.Text = "% inorganic N in top 33%:";
            topNiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // midNiLabel1
            // 
            midNiLabel1.AutoSize = true;
            midNiLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            midNiLabel1.Location = new System.Drawing.Point(513, 26);
            midNiLabel1.Name = "midNiLabel1";
            midNiLabel1.Size = new System.Drawing.Size(142, 26);
            midNiLabel1.TabIndex = 40;
            midNiLabel1.Text = "% inorganic N in middle 33%:";
            midNiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Dock = System.Windows.Forms.DockStyle.Fill;
            label31.Location = new System.Drawing.Point(3, 23);
            label31.Name = "label31";
            label31.Size = new System.Drawing.Size(129, 26);
            label31.TabIndex = 31;
            label31.Text = "Number of checked days:";
            label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Dock = System.Windows.Forms.DockStyle.Fill;
            label32.Location = new System.Drawing.Point(718, 23);
            label32.Name = "label32";
            label32.Size = new System.Drawing.Size(193, 26);
            label32.TabIndex = 4;
            label32.Text = "Cumulative precipitation threshold (mm):";
            label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cO2Label1
            // 
            cO2Label1.AutoSize = true;
            cO2Label1.Dock = System.Windows.Forms.DockStyle.Fill;
            cO2Label1.Location = new System.Drawing.Point(3, 0);
            cO2Label1.Name = "cO2Label1";
            cO2Label1.Size = new System.Drawing.Size(103, 26);
            cO2Label1.TabIndex = 4;
            cO2Label1.Text = "Baseline CO2 (ppm):";
            cO2Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TargetFSNlabel
            // 
            TargetFSNlabel.AutoSize = true;
            TargetFSNlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            TargetFSNlabel.Location = new System.Drawing.Point(3, 0);
            TargetFSNlabel.Name = "TargetFSNlabel";
            TargetFSNlabel.Size = new System.Drawing.Size(136, 26);
            TargetFSNlabel.TabIndex = 54;
            TargetFSNlabel.Text = "Target fertile shoot number:";
            TargetFSNlabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 107);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.AutoScroll = true;
            this.splitContainer2.Panel1.Controls.Add(this.dateApplicationsDataGridView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(this.momentApplicationsDataGridView);
            this.splitContainer2.Size = new System.Drawing.Size(567, 329);
            this.splitContainer2.SplitterDistance = 270;
            this.splitContainer2.TabIndex = 21;
            // 
            // dateApplicationsDataGridView
            // 
            this.dateApplicationsDataGridView.AutoGenerateColumns = false;
            this.dateApplicationsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dateApplicationsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dateApplicationsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateApplicationsDataGridView.Location = new System.Drawing.Point(0, 0);
            this.dateApplicationsDataGridView.Name = "dateApplicationsDataGridView";
            this.dateApplicationsDataGridView.Size = new System.Drawing.Size(270, 329);
            this.dateApplicationsDataGridView.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Date";
            this.dataGridViewTextBoxColumn1.HeaderText = "Date";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Nitrogen";
            this.dataGridViewTextBoxColumn2.HeaderText = "Nitrogen";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Water";
            this.dataGridViewTextBoxColumn3.HeaderText = "Water";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // momentApplicationsDataGridView
            // 
            this.momentApplicationsDataGridView.AutoGenerateColumns = false;
            this.momentApplicationsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.momentApplicationsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.momentApplicationsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.momentApplicationsDataGridView.Location = new System.Drawing.Point(0, 0);
            this.momentApplicationsDataGridView.Name = "momentApplicationsDataGridView";
            this.momentApplicationsDataGridView.Size = new System.Drawing.Size(293, 329);
            this.momentApplicationsDataGridView.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "GrowthStage";
            this.dataGridViewTextBoxColumn4.HeaderText = "Growth stage";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Nitrogen";
            this.dataGridViewTextBoxColumn5.HeaderText = "Nitrogen";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Water";
            this.dataGridViewTextBoxColumn6.HeaderText = "Water";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // managementItemsBindingSource1
            // 
            this.managementItemsBindingSource1.DataSource = typeof(SiriusModel.InOut.ManagementItem);
            this.managementItemsBindingSource1.CurrentChanged += new System.EventHandler(this.managementItemsBindingSource1_CurrentChanged);
            // 
            // dateApplicationsBindingSource
            // 
            this.dateApplicationsBindingSource.DataMember = "DateApplications";
            this.dateApplicationsBindingSource.DataSource = this.managementItemsBindingSource1;
            // 
            // growthStageApplicationsBindingSource
            // 
            this.growthStageApplicationsBindingSource.DataMember = "GrowthStageApplications";
            this.growthStageApplicationsBindingSource.DataSource = this.managementItemsBindingSource1;
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
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 655);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(229, 58);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // buttonAdd
            // 
            this.buttonAdd.AutoSize = true;
            this.buttonAdd.Location = new System.Drawing.Point(3, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(36, 23);
            this.buttonAdd.TabIndex = 1;
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
            this.buttonDelete.TabIndex = 2;
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
            this.buttonClear.TabIndex = 0;
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
            this.buttonSort.TabIndex = 4;
            this.buttonSort.Text = "Sort";
            this.buttonSort.UseVisualStyleBackColor = true;
            this.buttonSort.Click += new System.EventHandler(this.buttonSort_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer5);
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel7);
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer1.Size = new System.Drawing.Size(1362, 713);
            this.splitContainer1.SplitterDistance = 229;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.experimentNameComboBox);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.AutoScroll = true;
            this.splitContainer5.Panel2.Controls.Add(this.listBox1);
            this.splitContainer5.Size = new System.Drawing.Size(229, 655);
            this.splitContainer5.SplitterDistance = 25;
            this.splitContainer5.TabIndex = 3;
            // 
            // experimentNameComboBox
            // 
            this.experimentNameComboBox.DataSource = this.experimentNameBindingSource;
            this.experimentNameComboBox.DisplayMember = "Name";
            this.experimentNameComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.experimentNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.experimentNameComboBox.FormattingEnabled = true;
            this.experimentNameComboBox.Location = new System.Drawing.Point(0, 0);
            this.experimentNameComboBox.Name = "experimentNameComboBox";
            this.experimentNameComboBox.Size = new System.Drawing.Size(229, 21);
            this.experimentNameComboBox.TabIndex = 0;
            this.experimentNameComboBox.SelectedIndexChanged += new System.EventHandler(this.experimentNameComboBox_SelectedIndexChanged);
            // 
            // listBox1
            // 
            this.listBox1.DisplayMember = "Name";
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(229, 626);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.nameLabel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.experimentNameTextBox1, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.experimentNameLabel1, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.linkLabelName, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.specieslabel, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.speciescomboBox, 5, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.MinimumSize = new System.Drawing.Size(50, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1112, 30);
            this.tableLayoutPanel3.TabIndex = 37;
            // 
            // nameLabel1
            // 
            this.nameLabel1.AutoSize = true;
            this.nameLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameLabel1.Location = new System.Drawing.Point(3, 0);
            this.nameLabel1.Name = "nameLabel1";
            this.nameLabel1.Size = new System.Drawing.Size(101, 27);
            this.nameLabel1.TabIndex = 10;
            this.nameLabel1.Text = "Management name:";
            this.nameLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // experimentNameTextBox1
            // 
            this.experimentNameTextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "ExperimentName", true));
            this.experimentNameTextBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.experimentNameTextBox1.Location = new System.Drawing.Point(268, 3);
            this.experimentNameTextBox1.MinimumSize = new System.Drawing.Size(50, 4);
            this.experimentNameTextBox1.Name = "experimentNameTextBox1";
            this.experimentNameTextBox1.Size = new System.Drawing.Size(180, 20);
            this.experimentNameTextBox1.TabIndex = 21;
            // 
            // experimentNameLabel1
            // 
            this.experimentNameLabel1.AutoSize = true;
            this.experimentNameLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.experimentNameLabel1.Location = new System.Drawing.Point(171, 0);
            this.experimentNameLabel1.Name = "experimentNameLabel1";
            this.experimentNameLabel1.Size = new System.Drawing.Size(91, 27);
            this.experimentNameLabel1.TabIndex = 20;
            this.experimentNameLabel1.Text = "Experiment name:";
            this.experimentNameLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkLabelName
            // 
            this.linkLabelName.AutoSize = true;
            this.linkLabelName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "Name", true));
            this.linkLabelName.Dock = System.Windows.Forms.DockStyle.Left;
            this.linkLabelName.Location = new System.Drawing.Point(110, 0);
            this.linkLabelName.Name = "linkLabelName";
            this.linkLabelName.Size = new System.Drawing.Size(55, 27);
            this.linkLabelName.TabIndex = 28;
            this.linkLabelName.TabStop = true;
            this.linkLabelName.Text = "linkLabel1";
            this.linkLabelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabelName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelName_LinkClicked);
            // 
            // specieslabel
            // 
            this.specieslabel.AutoSize = true;
            this.specieslabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.specieslabel.Location = new System.Drawing.Point(454, 0);
            this.specieslabel.Name = "specieslabel";
            this.specieslabel.Size = new System.Drawing.Size(48, 27);
            this.specieslabel.TabIndex = 3;
            this.specieslabel.Text = "Species:";
            this.specieslabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // speciescomboBox
            // 
            this.speciescomboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "Species", true));
            this.speciescomboBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.speciescomboBox.FormattingEnabled = true;
            this.speciescomboBox.Items.AddRange(new object[] {
            "",
            "Wheat",
            "Maize"});
            this.speciescomboBox.Location = new System.Drawing.Point(508, 3);
            this.speciescomboBox.Name = "speciescomboBox";
            this.speciescomboBox.Size = new System.Drawing.Size(111, 21);
            this.speciescomboBox.TabIndex = 48;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.tableLayoutPanel4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.groupBox2.Size = new System.Drawing.Size(1112, 159);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sowing";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 7;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(sowingDateLabel1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.sowingDateDateTimePicker1, 1, 0);
            this.tableLayoutPanel4.Controls.Add(stemDensityLabel1, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.stemDensityTextBox1, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.isSowEstCheckBox, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.textBoxIsRelax, 2, 1);
            this.tableLayoutPanel4.Controls.Add(label7, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.textBoxCheckDepth, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.label16, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.textBoxSoilMoistThr, 3, 2);
            this.tableLayoutPanel4.Controls.Add(label13, 4, 2);
            this.tableLayoutPanel4.Controls.Add(this.textBoxSoilWorkabThr, 5, 2);
            this.tableLayoutPanel4.Controls.Add(label9, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.textBoxSkipDays, 1, 3);
            this.tableLayoutPanel4.Controls.Add(label14, 2, 3);
            this.tableLayoutPanel4.Controls.Add(this.textBoxCheckDaysPcp, 3, 3);
            this.tableLayoutPanel4.Controls.Add(label11, 4, 3);
            this.tableLayoutPanel4.Controls.Add(this.textBoxCumPcp, 5, 3);
            this.tableLayoutPanel4.Controls.Add(label10, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.textBoxCheckDaysTemp, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.label17, 2, 4);
            this.tableLayoutPanel4.Controls.Add(this.textBoxSoilFreezThr, 3, 4);
            this.tableLayoutPanel4.Controls.Add(label15, 4, 4);
            this.tableLayoutPanel4.Controls.Add(this.textBoxSoilTempThr, 5, 4);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.MinimumSize = new System.Drawing.Size(50, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 6;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1106, 137);
            this.tableLayoutPanel4.TabIndex = 30;
            // 
            // sowingDateDateTimePicker1
            // 
            this.sowingDateDateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.sowingDateDateTimePicker1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.managementItemsBindingSource1, "SowingDate", true));
            this.sowingDateDateTimePicker1.Dock = System.Windows.Forms.DockStyle.Left;
            this.sowingDateDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.sowingDateDateTimePicker1.Location = new System.Drawing.Point(212, 3);
            this.sowingDateDateTimePicker1.MinimumSize = new System.Drawing.Size(50, 4);
            this.sowingDateDateTimePicker1.Name = "sowingDateDateTimePicker1";
            this.sowingDateDateTimePicker1.Size = new System.Drawing.Size(80, 20);
            this.sowingDateDateTimePicker1.TabIndex = 35;
            // 
            // stemDensityTextBox1
            // 
            this.stemDensityTextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "SowingDensity", true));
            this.stemDensityTextBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.stemDensityTextBox1.Location = new System.Drawing.Point(519, 3);
            this.stemDensityTextBox1.MinimumSize = new System.Drawing.Size(50, 4);
            this.stemDensityTextBox1.Name = "stemDensityTextBox1";
            this.stemDensityTextBox1.Size = new System.Drawing.Size(80, 20);
            this.stemDensityTextBox1.TabIndex = 17;
            // 
            // isSowEstCheckBox
            // 
            this.isSowEstCheckBox.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.isSowEstCheckBox, 2);
            this.isSowEstCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.managementItemsBindingSource1, "IsSowDateEstimate", true));
            this.isSowEstCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.isSowEstCheckBox.Location = new System.Drawing.Point(3, 29);
            this.isSowEstCheckBox.Name = "isSowEstCheckBox";
            this.isSowEstCheckBox.Size = new System.Drawing.Size(126, 17);
            this.isSowEstCheckBox.TabIndex = 33;
            this.isSowEstCheckBox.Text = "Estimate sowing date";
            this.isSowEstCheckBox.UseVisualStyleBackColor = true;
            this.isSowEstCheckBox.CheckedChanged += new System.EventHandler(this.isSowEstCheckBox_CheckedChanged);
            // 
            // textBoxIsRelax
            // 
            this.textBoxIsRelax.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.textBoxIsRelax, 2);
            this.textBoxIsRelax.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.managementItemsBindingSource1, "DoRelax", true));
            this.textBoxIsRelax.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBoxIsRelax.Location = new System.Drawing.Point(298, 29);
            this.textBoxIsRelax.Name = "textBoxIsRelax";
            this.textBoxIsRelax.Size = new System.Drawing.Size(149, 17);
            this.textBoxIsRelax.TabIndex = 36;
            this.textBoxIsRelax.Text = "Apply relaxation on criteria";
            this.textBoxIsRelax.UseVisualStyleBackColor = true;
            // 
            // textBoxCheckDepth
            // 
            this.textBoxCheckDepth.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "CheckDepth", true));
            this.textBoxCheckDepth.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBoxCheckDepth.Location = new System.Drawing.Point(212, 52);
            this.textBoxCheckDepth.MaxLength = 100;
            this.textBoxCheckDepth.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBoxCheckDepth.Name = "textBoxCheckDepth";
            this.textBoxCheckDepth.Size = new System.Drawing.Size(80, 20);
            this.textBoxCheckDepth.TabIndex = 32;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(298, 49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(215, 26);
            this.label16.TabIndex = 22;
            this.label16.Text = "Lower soil moisture threshold (frac. of AWC):";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxSoilMoistThr
            // 
            this.textBoxSoilMoistThr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "SoilMoistThr", true));
            this.textBoxSoilMoistThr.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBoxSoilMoistThr.Location = new System.Drawing.Point(519, 52);
            this.textBoxSoilMoistThr.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBoxSoilMoistThr.Name = "textBoxSoilMoistThr";
            this.textBoxSoilMoistThr.Size = new System.Drawing.Size(80, 20);
            this.textBoxSoilMoistThr.TabIndex = 23;
            // 
            // textBoxSoilWorkabThr
            // 
            this.textBoxSoilWorkabThr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "SoilWorkabThr", true));
            this.textBoxSoilWorkabThr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSoilWorkabThr.Location = new System.Drawing.Point(863, 52);
            this.textBoxSoilWorkabThr.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBoxSoilWorkabThr.Name = "textBoxSoilWorkabThr";
            this.textBoxSoilWorkabThr.Size = new System.Drawing.Size(80, 20);
            this.textBoxSoilWorkabThr.TabIndex = 5;
            // 
            // textBoxSkipDays
            // 
            this.textBoxSkipDays.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "SkipDays", true));
            this.textBoxSkipDays.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBoxSkipDays.Location = new System.Drawing.Point(212, 78);
            this.textBoxSkipDays.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBoxSkipDays.Name = "textBoxSkipDays";
            this.textBoxSkipDays.Size = new System.Drawing.Size(80, 20);
            this.textBoxSkipDays.TabIndex = 9;
            // 
            // textBoxCheckDaysPcp
            // 
            this.textBoxCheckDaysPcp.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "CheckDaysPcp", true));
            this.textBoxCheckDaysPcp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCheckDaysPcp.Location = new System.Drawing.Point(519, 78);
            this.textBoxCheckDaysPcp.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBoxCheckDaysPcp.Name = "textBoxCheckDaysPcp";
            this.textBoxCheckDaysPcp.Size = new System.Drawing.Size(80, 20);
            this.textBoxCheckDaysPcp.TabIndex = 7;
            // 
            // textBoxCumPcp
            // 
            this.textBoxCumPcp.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "CumPcpThr", true));
            this.textBoxCumPcp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCumPcp.Location = new System.Drawing.Point(863, 78);
            this.textBoxCumPcp.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBoxCumPcp.Name = "textBoxCumPcp";
            this.textBoxCumPcp.Size = new System.Drawing.Size(80, 20);
            this.textBoxCumPcp.TabIndex = 40;
            // 
            // textBoxCheckDaysTemp
            // 
            this.textBoxCheckDaysTemp.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "CheckDaysTemp", true));
            this.textBoxCheckDaysTemp.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBoxCheckDaysTemp.Location = new System.Drawing.Point(212, 104);
            this.textBoxCheckDaysTemp.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBoxCheckDaysTemp.Name = "textBoxCheckDaysTemp";
            this.textBoxCheckDaysTemp.Size = new System.Drawing.Size(80, 20);
            this.textBoxCheckDaysTemp.TabIndex = 38;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(298, 101);
            this.label17.MinimumSize = new System.Drawing.Size(0, 26);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(215, 26);
            this.label17.TabIndex = 24;
            this.label17.Text = "Minimum daily air temperature threshold (°C):";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxSoilFreezThr
            // 
            this.textBoxSoilFreezThr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "TMinThr", true));
            this.textBoxSoilFreezThr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSoilFreezThr.Location = new System.Drawing.Point(519, 104);
            this.textBoxSoilFreezThr.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBoxSoilFreezThr.Name = "textBoxSoilFreezThr";
            this.textBoxSoilFreezThr.Size = new System.Drawing.Size(80, 20);
            this.textBoxSoilFreezThr.TabIndex = 30;
            // 
            // textBoxSoilTempThr
            // 
            this.textBoxSoilTempThr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "TAveThr", true));
            this.textBoxSoilTempThr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSoilTempThr.Location = new System.Drawing.Point(863, 104);
            this.textBoxSoilTempThr.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBoxSoilTempThr.Name = "textBoxSoilTempThr";
            this.textBoxSoilTempThr.Size = new System.Drawing.Size(80, 20);
            this.textBoxSoilTempThr.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.tableLayoutPanel2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 189);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.groupBox3.Size = new System.Drawing.Size(1112, 84);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Initiation conditions";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.midNiTextBox1, 5, 1);
            this.tableLayoutPanel2.Controls.Add(midNiLabel1, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.topNiTextBox1, 3, 1);
            this.tableLayoutPanel2.Controls.Add(topNiLabel1, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.totalNiTextBox1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(totalNiLabel1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.isWDinPercCheckBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.deficitLabel1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.deficitTextBox1, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.MinimumSize = new System.Drawing.Size(50, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1106, 62);
            this.tableLayoutPanel2.TabIndex = 30;
            // 
            // midNiTextBox1
            // 
            this.midNiTextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "MidNi", true));
            this.midNiTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.midNiTextBox1.Location = new System.Drawing.Point(661, 29);
            this.midNiTextBox1.MinimumSize = new System.Drawing.Size(50, 4);
            this.midNiTextBox1.Name = "midNiTextBox1";
            this.midNiTextBox1.Size = new System.Drawing.Size(80, 20);
            this.midNiTextBox1.TabIndex = 41;
            // 
            // topNiTextBox1
            // 
            this.topNiTextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "TopNi", true));
            this.topNiTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topNiTextBox1.Location = new System.Drawing.Point(427, 29);
            this.topNiTextBox1.MinimumSize = new System.Drawing.Size(50, 4);
            this.topNiTextBox1.Name = "topNiTextBox1";
            this.topNiTextBox1.Size = new System.Drawing.Size(80, 20);
            this.topNiTextBox1.TabIndex = 39;
            // 
            // totalNiTextBox1
            // 
            this.totalNiTextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "TotalNi", true));
            this.totalNiTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalNiTextBox1.Location = new System.Drawing.Point(175, 29);
            this.totalNiTextBox1.MinimumSize = new System.Drawing.Size(50, 4);
            this.totalNiTextBox1.Name = "totalNiTextBox1";
            this.totalNiTextBox1.Size = new System.Drawing.Size(80, 20);
            this.totalNiTextBox1.TabIndex = 34;
            // 
            // isWDinPercCheckBox
            // 
            this.isWDinPercCheckBox.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.isWDinPercCheckBox, 2);
            this.isWDinPercCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.managementItemsBindingSource1, "IsWDinPerc", true));
            this.isWDinPercCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.isWDinPercCheckBox.Location = new System.Drawing.Point(3, 3);
            this.isWDinPercCheckBox.Name = "isWDinPercCheckBox";
            this.isWDinPercCheckBox.Size = new System.Drawing.Size(252, 20);
            this.isWDinPercCheckBox.TabIndex = 30;
            this.isWDinPercCheckBox.Text = "Soil water deficit in percentage";
            this.isWDinPercCheckBox.UseVisualStyleBackColor = true;
            this.isWDinPercCheckBox.CheckedChanged += new System.EventHandler(this.isWDinPercCheckBox_CheckedChanged);
            // 
            // deficitLabel1
            // 
            this.deficitLabel1.AutoSize = true;
            this.deficitLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deficitLabel1.Location = new System.Drawing.Point(261, 0);
            this.deficitLabel1.Name = "deficitLabel1";
            this.deficitLabel1.Size = new System.Drawing.Size(160, 26);
            this.deficitLabel1.TabIndex = 31;
            this.deficitLabel1.Text = "Soil water deficit at sowing (mm):";
            this.deficitLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // deficitTextBox1
            // 
            this.deficitTextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "SoilWaterDeficit", true));
            this.deficitTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deficitTextBox1.Location = new System.Drawing.Point(427, 3);
            this.deficitTextBox1.MinimumSize = new System.Drawing.Size(50, 4);
            this.deficitTextBox1.Name = "deficitTextBox1";
            this.deficitTextBox1.Size = new System.Drawing.Size(80, 20);
            this.deficitTextBox1.TabIndex = 32;
            // 
            // groupBox4
            // 
            this.groupBox4.AutoSize = true;
            this.groupBox4.Controls.Add(this.tableLayoutPanel6);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Location = new System.Drawing.Point(0, 273);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.groupBox4.Size = new System.Drawing.Size(1112, 84);
            this.groupBox4.TabIndex = 38;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Atmospheric CO2 concentration";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 7;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.textBoxCO2TrendSlope, 5, 1);
            this.tableLayoutPanel6.Controls.Add(this.CO2TrendSlopelabel, 4, 1);
            this.tableLayoutPanel6.Controls.Add(this.textBoxCO2TrendBase, 3, 1);
            this.tableLayoutPanel6.Controls.Add(this.CO2trendbaselabel, 2, 1);
            this.tableLayoutPanel6.Controls.Add(this.checkBoxCO2Trend, 0, 1);
            this.tableLayoutPanel6.Controls.Add(cO2Label1, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.cO2TextBox1, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel6.MinimumSize = new System.Drawing.Size(50, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1106, 62);
            this.tableLayoutPanel6.TabIndex = 30;
            // 
            // textBoxCO2TrendSlope
            // 
            this.textBoxCO2TrendSlope.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "CO2TrendSlope", true));
            this.textBoxCO2TrendSlope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCO2TrendSlope.Location = new System.Drawing.Point(507, 29);
            this.textBoxCO2TrendSlope.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBoxCO2TrendSlope.Name = "textBoxCO2TrendSlope";
            this.textBoxCO2TrendSlope.Size = new System.Drawing.Size(80, 20);
            this.textBoxCO2TrendSlope.TabIndex = 66;
            // 
            // CO2TrendSlopelabel
            // 
            this.CO2TrendSlopelabel.AutoSize = true;
            this.CO2TrendSlopelabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CO2TrendSlopelabel.Location = new System.Drawing.Point(377, 26);
            this.CO2TrendSlopelabel.Name = "CO2TrendSlopelabel";
            this.CO2TrendSlopelabel.Size = new System.Drawing.Size(124, 26);
            this.CO2TrendSlopelabel.TabIndex = 65;
            this.CO2TrendSlopelabel.Text = "Annual change (%/year):";
            this.CO2TrendSlopelabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCO2TrendBase
            // 
            this.textBoxCO2TrendBase.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "CO2TrendBaseYear", true));
            this.textBoxCO2TrendBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCO2TrendBase.Location = new System.Drawing.Point(291, 29);
            this.textBoxCO2TrendBase.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBoxCO2TrendBase.Name = "textBoxCO2TrendBase";
            this.textBoxCO2TrendBase.Size = new System.Drawing.Size(80, 20);
            this.textBoxCO2TrendBase.TabIndex = 64;
            // 
            // CO2trendbaselabel
            // 
            this.CO2trendbaselabel.AutoSize = true;
            this.CO2trendbaselabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CO2trendbaselabel.Location = new System.Drawing.Point(198, 26);
            this.CO2trendbaselabel.Name = "CO2trendbaselabel";
            this.CO2trendbaselabel.Size = new System.Drawing.Size(87, 26);
            this.CO2trendbaselabel.TabIndex = 63;
            this.CO2trendbaselabel.Text = "Trend base year:";
            this.CO2trendbaselabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBoxCO2Trend
            // 
            this.tableLayoutPanel6.SetColumnSpan(this.checkBoxCO2Trend, 2);
            this.checkBoxCO2Trend.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.managementItemsBindingSource1, "IsCO2TrendApplied", true));
            this.checkBoxCO2Trend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxCO2Trend.Location = new System.Drawing.Point(3, 29);
            this.checkBoxCO2Trend.Name = "checkBoxCO2Trend";
            this.checkBoxCO2Trend.Size = new System.Drawing.Size(189, 20);
            this.checkBoxCO2Trend.TabIndex = 50;
            this.checkBoxCO2Trend.Text = "Apply annual trend on CO2 concentration";
            this.checkBoxCO2Trend.UseVisualStyleBackColor = true;
            this.checkBoxCO2Trend.CheckedChanged += new System.EventHandler(this.checkBoxCO2Trend_CheckedChanged);
            // 
            // cO2TextBox1
            // 
            this.cO2TextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "CO2", true));
            this.cO2TextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cO2TextBox1.Location = new System.Drawing.Point(112, 3);
            this.cO2TextBox1.MinimumSize = new System.Drawing.Size(50, 4);
            this.cO2TextBox1.Name = "cO2TextBox1";
            this.cO2TextBox1.Size = new System.Drawing.Size(80, 20);
            this.cO2TextBox1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.tableLayoutPanel5);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 357);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.groupBox1.Size = new System.Drawing.Size(1112, 136);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "N fertilizer application";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 8;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.textBoxNTrendSlope, 6, 3);
            this.tableLayoutPanel5.Controls.Add(this.AnnualChangeLabel, 5, 3);
            this.tableLayoutPanel5.Controls.Add(this.textBoxNTrendBase, 4, 3);
            this.tableLayoutPanel5.Controls.Add(this.TrendBaseYearlabel, 3, 3);
            this.tableLayoutPanel5.Controls.Add(this.textBoxNNIMult, 6, 2);
            this.tableLayoutPanel5.Controls.Add(this.NNImultlabel, 5, 2);
            this.tableLayoutPanel5.Controls.Add(this.textBoxNNIThr, 4, 2);
            this.tableLayoutPanel5.Controls.Add(this.NNithrlabel, 3, 2);
            this.tableLayoutPanel5.Controls.Add(this.checkBoxIsNNIUsed, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.checkBoxNTrend, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.totalNFertTextBox, 4, 1);
            this.tableLayoutPanel5.Controls.Add(this.totalfertNlabel, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.isTotNitrogenCheckBox, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.IsPcpCheclNcheckBox, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.ChecledDaysNtextBox, 2, 0);
            this.tableLayoutPanel5.Controls.Add(nbcheckdaylabel, 1, 0);
            this.tableLayoutPanel5.Controls.Add(cumpcplabel, 5, 0);
            this.tableLayoutPanel5.Controls.Add(this.CumPcpThrNtextBox, 6, 0);
            this.tableLayoutPanel5.Controls.Add(this.maxpostponlabel, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.MaxPostponeNtextBox, 4, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel5.MinimumSize = new System.Drawing.Size(50, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 5;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1106, 114);
            this.tableLayoutPanel5.TabIndex = 30;
            // 
            // textBoxNTrendSlope
            // 
            this.textBoxNTrendSlope.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "NTrendSlope", true));
            this.textBoxNTrendSlope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNTrendSlope.Location = new System.Drawing.Point(922, 81);
            this.textBoxNTrendSlope.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBoxNTrendSlope.Name = "textBoxNTrendSlope";
            this.textBoxNTrendSlope.Size = new System.Drawing.Size(80, 20);
            this.textBoxNTrendSlope.TabIndex = 54;
            // 
            // AnnualChangeLabel
            // 
            this.AnnualChangeLabel.AutoSize = true;
            this.AnnualChangeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnnualChangeLabel.Location = new System.Drawing.Point(723, 78);
            this.AnnualChangeLabel.Name = "AnnualChangeLabel";
            this.AnnualChangeLabel.Size = new System.Drawing.Size(193, 26);
            this.AnnualChangeLabel.TabIndex = 53;
            this.AnnualChangeLabel.Text = "Annual change (%/year):";
            this.AnnualChangeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxNTrendBase
            // 
            this.textBoxNTrendBase.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "NTrendBaseYear", true));
            this.textBoxNTrendBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNTrendBase.Location = new System.Drawing.Point(637, 81);
            this.textBoxNTrendBase.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBoxNTrendBase.Name = "textBoxNTrendBase";
            this.textBoxNTrendBase.Size = new System.Drawing.Size(80, 20);
            this.textBoxNTrendBase.TabIndex = 52;
            // 
            // TrendBaseYearlabel
            // 
            this.TrendBaseYearlabel.AutoSize = true;
            this.TrendBaseYearlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrendBaseYearlabel.Location = new System.Drawing.Point(429, 78);
            this.TrendBaseYearlabel.Name = "TrendBaseYearlabel";
            this.TrendBaseYearlabel.Size = new System.Drawing.Size(202, 26);
            this.TrendBaseYearlabel.TabIndex = 51;
            this.TrendBaseYearlabel.Text = "Trend base year:";
            this.TrendBaseYearlabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxNNIMult
            // 
            this.textBoxNNIMult.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "NNIMultiplier", true));
            this.textBoxNNIMult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNNIMult.Location = new System.Drawing.Point(922, 55);
            this.textBoxNNIMult.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBoxNNIMult.Name = "textBoxNNIMult";
            this.textBoxNNIMult.Size = new System.Drawing.Size(80, 20);
            this.textBoxNNIMult.TabIndex = 62;
            // 
            // NNImultlabel
            // 
            this.NNImultlabel.AutoSize = true;
            this.NNImultlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NNImultlabel.Location = new System.Drawing.Point(723, 52);
            this.NNImultlabel.Name = "NNImultlabel";
            this.NNImultlabel.Size = new System.Drawing.Size(193, 26);
            this.NNImultlabel.TabIndex = 61;
            this.NNImultlabel.Text = "NNI multiplier:";
            this.NNImultlabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxNNIThr
            // 
            this.textBoxNNIThr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "NNIThreshold", true));
            this.textBoxNNIThr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNNIThr.Location = new System.Drawing.Point(637, 55);
            this.textBoxNNIThr.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBoxNNIThr.Name = "textBoxNNIThr";
            this.textBoxNNIThr.Size = new System.Drawing.Size(80, 20);
            this.textBoxNNIThr.TabIndex = 60;
            // 
            // NNithrlabel
            // 
            this.NNithrlabel.AutoSize = true;
            this.NNithrlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NNithrlabel.Location = new System.Drawing.Point(429, 52);
            this.NNithrlabel.Name = "NNithrlabel";
            this.NNithrlabel.Size = new System.Drawing.Size(202, 26);
            this.NNithrlabel.TabIndex = 59;
            this.NNithrlabel.Text = "NNI threshold to trigger N fertilisation:";
            this.NNithrlabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBoxIsNNIUsed
            // 
            this.checkBoxIsNNIUsed.AutoSize = true;
            this.checkBoxIsNNIUsed.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.managementItemsBindingSource1, "IsNNIUsed", true));
            this.checkBoxIsNNIUsed.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBoxIsNNIUsed.Location = new System.Drawing.Point(3, 55);
            this.checkBoxIsNNIUsed.Name = "checkBoxIsNNIUsed";
            this.checkBoxIsNNIUsed.Size = new System.Drawing.Size(145, 20);
            this.checkBoxIsNNIUsed.TabIndex = 58;
            this.checkBoxIsNNIUsed.Text = "Use NNI for N fertilisation";
            this.checkBoxIsNNIUsed.UseVisualStyleBackColor = true;
            this.checkBoxIsNNIUsed.CheckedChanged += new System.EventHandler(this.checkBoxIsNNIUsed_CheckedChanged);
            // 
            // checkBoxNTrend
            // 
            this.checkBoxNTrend.AutoSize = true;
            this.checkBoxNTrend.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.managementItemsBindingSource1, "IsNTrendApplied", true));
            this.checkBoxNTrend.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBoxNTrend.Location = new System.Drawing.Point(3, 81);
            this.checkBoxNTrend.Name = "checkBoxNTrend";
            this.checkBoxNTrend.Size = new System.Drawing.Size(192, 20);
            this.checkBoxNTrend.TabIndex = 57;
            this.checkBoxNTrend.Text = "Apply annual trend on N fertilisation";
            this.checkBoxNTrend.UseVisualStyleBackColor = true;
            this.checkBoxNTrend.CheckedChanged += new System.EventHandler(this.checkBoxNTrend_CheckedChanged);
            // 
            // totalNFertTextBox
            // 
            this.totalNFertTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "TotalNApplication", true));
            this.totalNFertTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalNFertTextBox.Location = new System.Drawing.Point(637, 29);
            this.totalNFertTextBox.MinimumSize = new System.Drawing.Size(50, 4);
            this.totalNFertTextBox.Name = "totalNFertTextBox";
            this.totalNFertTextBox.Size = new System.Drawing.Size(80, 20);
            this.totalNFertTextBox.TabIndex = 56;
            // 
            // totalfertNlabel
            // 
            this.totalfertNlabel.AutoSize = true;
            this.totalfertNlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalfertNlabel.Location = new System.Drawing.Point(429, 26);
            this.totalfertNlabel.Name = "totalfertNlabel";
            this.totalfertNlabel.Size = new System.Drawing.Size(202, 26);
            this.totalfertNlabel.TabIndex = 55;
            this.totalfertNlabel.Text = "Total N fertilisation (gN/m²):";
            this.totalfertNlabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // isTotNitrogenCheckBox
            // 
            this.isTotNitrogenCheckBox.AutoSize = true;
            this.isTotNitrogenCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.managementItemsBindingSource1, "IsTotalNitrogen", true));
            this.isTotNitrogenCheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.isTotNitrogenCheckBox.Location = new System.Drawing.Point(3, 29);
            this.isTotNitrogenCheckBox.Name = "isTotNitrogenCheckBox";
            this.isTotNitrogenCheckBox.Size = new System.Drawing.Size(199, 20);
            this.isTotNitrogenCheckBox.TabIndex = 54;
            this.isTotNitrogenCheckBox.Text = "Apply N fertiliser as % of total amount";
            this.isTotNitrogenCheckBox.UseVisualStyleBackColor = true;
            this.isTotNitrogenCheckBox.CheckedChanged += new System.EventHandler(this.isTotNitrogenCheckBox_CheckedChanged);
            // 
            // IsPcpCheclNcheckBox
            // 
            this.IsPcpCheclNcheckBox.AutoSize = true;
            this.IsPcpCheclNcheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.managementItemsBindingSource1, "IsCheckPcpN", true));
            this.IsPcpCheclNcheckBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.IsPcpCheclNcheckBox.Location = new System.Drawing.Point(3, 3);
            this.IsPcpCheclNcheckBox.Name = "IsPcpCheclNcheckBox";
            this.IsPcpCheclNcheckBox.Size = new System.Drawing.Size(132, 20);
            this.IsPcpCheclNcheckBox.TabIndex = 33;
            this.IsPcpCheclNcheckBox.Text = "Check for precipitation";
            this.IsPcpCheclNcheckBox.UseVisualStyleBackColor = true;
            this.IsPcpCheclNcheckBox.CheckedChanged += new System.EventHandler(this.IsPcpCheclNcheckBox_CheckedChanged);
            // 
            // ChecledDaysNtextBox
            // 
            this.ChecledDaysNtextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "CheckDaysPcpN", true));
            this.ChecledDaysNtextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.ChecledDaysNtextBox.Location = new System.Drawing.Point(343, 3);
            this.ChecledDaysNtextBox.MinimumSize = new System.Drawing.Size(50, 4);
            this.ChecledDaysNtextBox.Name = "ChecledDaysNtextBox";
            this.ChecledDaysNtextBox.Size = new System.Drawing.Size(80, 20);
            this.ChecledDaysNtextBox.TabIndex = 32;
            // 
            // CumPcpThrNtextBox
            // 
            this.CumPcpThrNtextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "CumPcpThrN", true));
            this.CumPcpThrNtextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CumPcpThrNtextBox.Location = new System.Drawing.Point(922, 3);
            this.CumPcpThrNtextBox.MinimumSize = new System.Drawing.Size(50, 4);
            this.CumPcpThrNtextBox.Name = "CumPcpThrNtextBox";
            this.CumPcpThrNtextBox.Size = new System.Drawing.Size(80, 20);
            this.CumPcpThrNtextBox.TabIndex = 5;
            // 
            // maxpostponlabel
            // 
            this.maxpostponlabel.AutoSize = true;
            this.maxpostponlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maxpostponlabel.Location = new System.Drawing.Point(429, 0);
            this.maxpostponlabel.Name = "maxpostponlabel";
            this.maxpostponlabel.Size = new System.Drawing.Size(202, 26);
            this.maxpostponlabel.TabIndex = 22;
            this.maxpostponlabel.Text = "Maximum allowable postponement (days):";
            this.maxpostponlabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MaxPostponeNtextBox
            // 
            this.MaxPostponeNtextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "MaxPostponeN", true));
            this.MaxPostponeNtextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MaxPostponeNtextBox.Location = new System.Drawing.Point(637, 3);
            this.MaxPostponeNtextBox.MinimumSize = new System.Drawing.Size(50, 4);
            this.MaxPostponeNtextBox.Name = "MaxPostponeNtextBox";
            this.MaxPostponeNtextBox.Size = new System.Drawing.Size(80, 20);
            this.MaxPostponeNtextBox.TabIndex = 23;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.AutoSize = true;
            this.tableLayoutPanel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel7.ColumnCount = 5;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.Controls.Add(TargetFSNlabel, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.totalNfertlabelvalue, 3, 1);
            this.tableLayoutPanel7.Controls.Add(this.totalNfertlabel, 2, 1);
            this.tableLayoutPanel7.Controls.Add(this.totalirrigationlabelvalue, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.totalirrigationlabel, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.TargetFSNtextBox, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 493);
            this.tableLayoutPanel7.MinimumSize = new System.Drawing.Size(50, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1112, 58);
            this.tableLayoutPanel7.TabIndex = 39;
            // 
            // totalNfertlabelvalue
            // 
            this.totalNfertlabelvalue.AutoSize = true;
            this.totalNfertlabelvalue.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "TotalNfertilisation", true));
            this.totalNfertlabelvalue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalNfertlabelvalue.Location = new System.Drawing.Point(373, 26);
            this.totalNfertlabelvalue.Name = "totalNfertlabelvalue";
            this.totalNfertlabelvalue.Size = new System.Drawing.Size(41, 26);
            this.totalNfertlabelvalue.TabIndex = 37;
            this.totalNfertlabelvalue.Text = "label18";
            this.totalNfertlabelvalue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // totalNfertlabel
            // 
            this.totalNfertlabel.AutoSize = true;
            this.totalNfertlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalNfertlabel.Location = new System.Drawing.Point(231, 26);
            this.totalNfertlabel.MinimumSize = new System.Drawing.Size(0, 26);
            this.totalNfertlabel.Name = "totalNfertlabel";
            this.totalNfertlabel.Size = new System.Drawing.Size(136, 26);
            this.totalNfertlabel.TabIndex = 36;
            this.totalNfertlabel.Text = "Total N fertilisation (gN/m²):";
            this.totalNfertlabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // totalirrigationlabelvalue
            // 
            this.totalirrigationlabelvalue.AutoSize = true;
            this.totalirrigationlabelvalue.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "TotalIrrigation", true));
            this.totalirrigationlabelvalue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalirrigationlabelvalue.Location = new System.Drawing.Point(145, 26);
            this.totalirrigationlabelvalue.Name = "totalirrigationlabelvalue";
            this.totalirrigationlabelvalue.Size = new System.Drawing.Size(80, 26);
            this.totalirrigationlabelvalue.TabIndex = 35;
            this.totalirrigationlabelvalue.Text = "label20";
            this.totalirrigationlabelvalue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // totalirrigationlabel
            // 
            this.totalirrigationlabel.AutoSize = true;
            this.totalirrigationlabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalirrigationlabel.Location = new System.Drawing.Point(3, 26);
            this.totalirrigationlabel.Name = "totalirrigationlabel";
            this.totalirrigationlabel.Size = new System.Drawing.Size(136, 26);
            this.totalirrigationlabel.TabIndex = 34;
            this.totalirrigationlabel.Text = "Total irrigation (mm):";
            this.totalirrigationlabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TargetFSNtextBox
            // 
            this.TargetFSNtextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "TargetFertileShootNumber", true));
            this.TargetFSNtextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TargetFSNtextBox.Location = new System.Drawing.Point(145, 3);
            this.TargetFSNtextBox.MinimumSize = new System.Drawing.Size(50, 4);
            this.TargetFSNtextBox.Name = "TargetFSNtextBox";
            this.TargetFSNtextBox.Size = new System.Drawing.Size(80, 20);
            this.TargetFSNtextBox.TabIndex = 23;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer4.Location = new System.Drawing.Point(0, 551);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.AutoScroll = true;
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.AutoScroll = true;
            this.splitContainer4.Panel2.Controls.Add(this.textBox1);
            this.splitContainer4.Panel2.Controls.Add(this.label6);
            this.splitContainer4.Size = new System.Drawing.Size(1112, 425);
            this.splitContainer4.SplitterDistance = 222;
            this.splitContainer4.TabIndex = 24;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.AutoScroll = true;
            this.splitContainer3.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer3.Panel1.Controls.Add(this.flowLayoutPanel2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.AutoScroll = true;
            this.splitContainer3.Panel2.Controls.Add(this.dataGridView2);
            this.splitContainer3.Panel2.Controls.Add(this.flowLayoutPanel3);
            this.splitContainer3.Size = new System.Drawing.Size(1112, 222);
            this.splitContainer3.SplitterDistance = 511;
            this.splitContainer3.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateDataGridViewTextBoxColumn,
            this.nitrogenDataGridViewTextBoxColumn,
            this.WaterMM});
            this.dataGridView1.DataSource = this.dateApplicationsBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(511, 192);
            this.dataGridView1.TabIndex = 0;
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            // 
            // nitrogenDataGridViewTextBoxColumn
            // 
            this.nitrogenDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.nitrogenDataGridViewTextBoxColumn.DataPropertyName = "Nitrogen";
            this.nitrogenDataGridViewTextBoxColumn.HeaderText = "N fertilisation (gN/m²)";
            this.nitrogenDataGridViewTextBoxColumn.Name = "nitrogenDataGridViewTextBoxColumn";
            this.nitrogenDataGridViewTextBoxColumn.Width = 120;
            // 
            // WaterMM
            // 
            this.WaterMM.DataPropertyName = "WaterMM";
            this.WaterMM.HeaderText = "Irrigation (mm)";
            this.WaterMM.Name = "WaterMM";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.buttonSortDate);
            this.flowLayoutPanel2.Controls.Add(this.buttonAddDate);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 192);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(511, 30);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // buttonSortDate
            // 
            this.buttonSortDate.AutoSize = true;
            this.buttonSortDate.Location = new System.Drawing.Point(443, 3);
            this.buttonSortDate.Name = "buttonSortDate";
            this.buttonSortDate.Size = new System.Drawing.Size(65, 24);
            this.buttonSortDate.TabIndex = 1;
            this.buttonSortDate.Text = "Sort dates";
            this.buttonSortDate.UseVisualStyleBackColor = true;
            this.buttonSortDate.Click += new System.EventHandler(this.buttonSortDate_Click);
            // 
            // buttonAddDate
            // 
            this.buttonAddDate.AutoSize = true;
            this.buttonAddDate.Location = new System.Drawing.Point(372, 3);
            this.buttonAddDate.Name = "buttonAddDate";
            this.buttonAddDate.Size = new System.Drawing.Size(65, 24);
            this.buttonAddDate.TabIndex = 0;
            this.buttonAddDate.Text = "Add date";
            this.buttonAddDate.UseVisualStyleBackColor = true;
            this.buttonAddDate.Click += new System.EventHandler(this.buttonAddDate_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GrowthStageComboBox,
            this.nitrogenDataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn7});
            this.dataGridView2.DataSource = this.growthStageApplicationsBindingSource;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(597, 193);
            this.dataGridView2.TabIndex = 0;
            // 
            // GrowthStageComboBox
            // 
            this.GrowthStageComboBox.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GrowthStageComboBox.DataPropertyName = "GrowthStage";
            this.GrowthStageComboBox.HeaderText = "Growth stage";
            this.GrowthStageComboBox.Name = "GrowthStageComboBox";
            // 
            // nitrogenDataGridViewTextBoxColumn1
            // 
            this.nitrogenDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.nitrogenDataGridViewTextBoxColumn1.DataPropertyName = "Nitrogen";
            this.nitrogenDataGridViewTextBoxColumn1.HeaderText = "N fertilisation (gN/m²)";
            this.nitrogenDataGridViewTextBoxColumn1.Name = "nitrogenDataGridViewTextBoxColumn1";
            this.nitrogenDataGridViewTextBoxColumn1.Width = 120;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "WaterMM";
            this.dataGridViewTextBoxColumn7.HeaderText = "Irrigation (mm)";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.Controls.Add(this.buttonSortGrowthStage);
            this.flowLayoutPanel3.Controls.Add(this.buttonAddGrowthStage);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 193);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(597, 29);
            this.flowLayoutPanel3.TabIndex = 1;
            // 
            // buttonSortGrowthStage
            // 
            this.buttonSortGrowthStage.AutoSize = true;
            this.buttonSortGrowthStage.Location = new System.Drawing.Point(489, 3);
            this.buttonSortGrowthStage.Name = "buttonSortGrowthStage";
            this.buttonSortGrowthStage.Size = new System.Drawing.Size(105, 23);
            this.buttonSortGrowthStage.TabIndex = 1;
            this.buttonSortGrowthStage.Text = "Sort growth stages";
            this.buttonSortGrowthStage.UseVisualStyleBackColor = true;
            this.buttonSortGrowthStage.Click += new System.EventHandler(this.buttonSortGrowthStage_Click);
            // 
            // buttonAddGrowthStage
            // 
            this.buttonAddGrowthStage.AutoSize = true;
            this.buttonAddGrowthStage.Location = new System.Drawing.Point(383, 3);
            this.buttonAddGrowthStage.Name = "buttonAddGrowthStage";
            this.buttonAddGrowthStage.Size = new System.Drawing.Size(100, 23);
            this.buttonAddGrowthStage.TabIndex = 0;
            this.buttonAddGrowthStage.Text = "Add growth stage";
            this.buttonAddGrowthStage.UseVisualStyleBackColor = true;
            this.buttonAddGrowthStage.Click += new System.EventHandler(this.buttonAddGrowthStage_Click);
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "Comments", true));
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(62, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(1050, 199);
            this.textBox1.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 199);
            this.label6.TabIndex = 25;
            this.label6.Text = "Comments:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.managementItemsBindingSource1, "IsCheckPcpN", true));
            this.checkBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox2.Location = new System.Drawing.Point(3, 3);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(132, 17);
            this.checkBox2.TabIndex = 33;
            this.checkBox2.Text = "Check for precipitation";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.managementItemsBindingSource1, "IsCheckPcpN", true));
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox1.Location = new System.Drawing.Point(3, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(132, 17);
            this.checkBox1.TabIndex = 33;
            this.checkBox1.Text = "Check for precipitation";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(375, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 26);
            this.label1.TabIndex = 22;
            this.label1.Text = "Target fertile shoot number (shoot/m²):";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "CheckDaysPcpN", true));
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox3.Location = new System.Drawing.Point(138, 26);
            this.textBox3.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(180, 20);
            this.textBox3.TabIndex = 32;
            // 
            // textBox4
            // 
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "CumPcpThrN", true));
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox4.Location = new System.Drawing.Point(917, 26);
            this.textBox4.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(186, 20);
            this.textBox4.TabIndex = 5;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label30.Location = new System.Drawing.Point(324, 23);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(202, 26);
            this.label30.TabIndex = 22;
            this.label30.Text = "Maximum allowable postponement (days):";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox5
            // 
            this.textBox5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "MaxPostponeN", true));
            this.textBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox5.Location = new System.Drawing.Point(532, 26);
            this.textBox5.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(180, 20);
            this.textBox5.TabIndex = 23;
            // 
            // textBox6
            // 
            this.textBox6.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "CheckDaysPcpN", true));
            this.textBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox6.Location = new System.Drawing.Point(138, 26);
            this.textBox6.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(180, 20);
            this.textBox6.TabIndex = 32;
            // 
            // textBox7
            // 
            this.textBox7.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "CumPcpThrN", true));
            this.textBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox7.Location = new System.Drawing.Point(917, 26);
            this.textBox7.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(186, 20);
            this.textBox7.TabIndex = 5;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label33.Location = new System.Drawing.Point(324, 23);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(202, 26);
            this.label33.TabIndex = 22;
            this.label33.Text = "Maximum allowable postponement (days):";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox8
            // 
            this.textBox8.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.managementItemsBindingSource1, "MaxPostponeN", true));
            this.textBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox8.Location = new System.Drawing.Point(532, 26);
            this.textBox8.MinimumSize = new System.Drawing.Size(50, 4);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(180, 20);
            this.textBox8.TabIndex = 23;
            // 
            // ManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 742);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ManagementForm";
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateApplicationsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.momentApplicationsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.managementItemsBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.experimentNameBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateApplicationsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.growthStageApplicationsBindingSource)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dateApplicationsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridView momentApplicationsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private ManagementItemsBindingSource managementItemsBindingSource1;
        private BaseBindingSource experimentNameBindingSource;
        private System.Windows.Forms.BindingSource dateApplicationsBindingSource;
        private System.Windows.Forms.BindingSource growthStageApplicationsBindingSource;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonDuplicate;
        private System.Windows.Forms.Button buttonSort;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nitrogenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn WaterMM;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button buttonSortDate;
        private System.Windows.Forms.Button buttonAddDate;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn nitrogenDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button buttonSortGrowthStage;
        private System.Windows.Forms.Button buttonAddGrowthStage;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox stemDensityTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TextBox textBoxCumPcp;
        private System.Windows.Forms.TextBox textBoxCheckDaysTemp;
        private System.Windows.Forms.DateTimePicker sowingDateDateTimePicker1;
        private System.Windows.Forms.TextBox textBoxCheckDepth;
        private System.Windows.Forms.TextBox textBoxSoilFreezThr;
        private System.Windows.Forms.TextBox textBoxSkipDays;
        private System.Windows.Forms.TextBox textBoxSoilWorkabThr;
        private System.Windows.Forms.TextBox textBoxCheckDaysPcp;
        private System.Windows.Forms.TextBox textBoxSoilTempThr;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxSoilMoistThr;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.ComboBox experimentNameComboBox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.DataGridViewComboBoxColumn GrowthStageComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.CheckBox IsPcpCheclNcheckBox;
        private System.Windows.Forms.TextBox ChecledDaysNtextBox;
        private System.Windows.Forms.TextBox CumPcpThrNtextBox;
        private System.Windows.Forms.Label maxpostponlabel;
        private System.Windows.Forms.TextBox MaxPostponeNtextBox;
        private System.Windows.Forms.CheckBox isSowEstCheckBox;
        private System.Windows.Forms.CheckBox textBoxIsRelax;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.CheckBox isWDinPercCheckBox;
        private System.Windows.Forms.Label deficitLabel1;
        private System.Windows.Forms.TextBox deficitTextBox1;
        private System.Windows.Forms.TextBox totalNiTextBox1;
        private System.Windows.Forms.TextBox topNiTextBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label nameLabel1;
        private System.Windows.Forms.TextBox experimentNameTextBox1;
        private System.Windows.Forms.Label experimentNameLabel1;
        private System.Windows.Forms.LinkLabel linkLabelName;
        private System.Windows.Forms.TextBox midNiTextBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBoxNTrendSlope;
        private System.Windows.Forms.Label AnnualChangeLabel;
        private System.Windows.Forms.TextBox textBoxNTrendBase;
        private System.Windows.Forms.Label TrendBaseYearlabel;
        private System.Windows.Forms.CheckBox checkBoxCO2Trend;
        private System.Windows.Forms.TextBox cO2TextBox1;
        private System.Windows.Forms.TextBox textBoxCO2TrendSlope;
        private System.Windows.Forms.Label CO2TrendSlopelabel;
        private System.Windows.Forms.TextBox textBoxCO2TrendBase;
        private System.Windows.Forms.Label CO2trendbaselabel;
        private System.Windows.Forms.TextBox textBoxNNIMult;
        private System.Windows.Forms.Label NNImultlabel;
        private System.Windows.Forms.TextBox textBoxNNIThr;
        private System.Windows.Forms.Label NNithrlabel;
        private System.Windows.Forms.CheckBox checkBoxIsNNIUsed;
        private System.Windows.Forms.CheckBox checkBoxNTrend;
        private System.Windows.Forms.TextBox totalNFertTextBox;
        private System.Windows.Forms.Label totalfertNlabel;
        private System.Windows.Forms.CheckBox isTotNitrogenCheckBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label totalNfertlabelvalue;
        private System.Windows.Forms.Label totalNfertlabel;
        private System.Windows.Forms.Label totalirrigationlabelvalue;
        private System.Windows.Forms.Label totalirrigationlabel;
        private System.Windows.Forms.TextBox TargetFSNtextBox;
        private System.Windows.Forms.Label specieslabel;
        private System.Windows.Forms.ComboBox speciescomboBox;

    }
}