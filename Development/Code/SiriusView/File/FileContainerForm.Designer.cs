namespace SiriusView.File
{
    partial class FileContainerForm
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
            this.relativeFileNameLabel3 = new System.Windows.Forms.Label();
            this.pathLinkLabel = new System.Windows.Forms.LinkLabel();
            this.projectFileBindingSource1 = new SiriusView.ProjectFileBindingSource(this.components);
            this.directoryLabel = new System.Windows.Forms.Label();
            this.directoryLabel1 = new System.Windows.Forms.Label();
            this.relativeFileNameLabel5 = new System.Windows.Forms.Label();
            this.relativeFileNameLinkLabel5 = new System.Windows.Forms.LinkLabel();
            this.fileContainerBindingSource1 = new SiriusView.FileContainerBindingSource(this.components);
            this.relativeFileNameLabel4 = new System.Windows.Forms.Label();
            this.relativeFileNameLinkLabel4 = new System.Windows.Forms.LinkLabel();
            this.relativeFileNameLinkLabel3 = new System.Windows.Forms.LinkLabel();
            this.relativeFileNameLabel2 = new System.Windows.Forms.Label();
            this.relativeFileNameLinkLabel2 = new System.Windows.Forms.LinkLabel();
            this.relativeFileNameLabel = new System.Windows.Forms.Label();
            this.relativeFileNameLabel1 = new System.Windows.Forms.Label();
            this.relativeFileNameLinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.relativeFileNameLinkLabel = new System.Windows.Forms.LinkLabel();
            this.pathLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.labelModified = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonNewProject = new System.Windows.Forms.Button();
            this.buttonSaveProject = new System.Windows.Forms.Button();
            this.buttonSaveProjectAs = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonRunAllSimple = new System.Windows.Forms.Button();
            this.buttonRunAllBatch = new System.Windows.Forms.Button();
            this.buttonRunAllSensitivity = new System.Windows.Forms.Button();
            this.buttonExportAllSimple = new System.Windows.Forms.Button();
            this.buttonExportAllBatch = new System.Windows.Forms.Button();
            this.buttonExportAllSensitivity = new System.Windows.Forms.Button();
            this.buttonLoadAll = new System.Windows.Forms.Button();
            this.buttonSaveAll = new System.Windows.Forms.Button();
            this.relativeFileNameLabel6 = new System.Windows.Forms.Label();
            this.relativeFileNameLinkLabel6 = new System.Windows.Forms.LinkLabel();
            this.relativeFileNameLabel7 = new System.Windows.Forms.Label();
            this.relativeFileNameLinkLabel7 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.projectFileBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileContainerBindingSource1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // relativeFileNameLabel3
            // 
            this.relativeFileNameLabel3.AutoSize = true;
            this.relativeFileNameLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relativeFileNameLabel3.Location = new System.Drawing.Point(3, 142);
            this.relativeFileNameLabel3.Margin = new System.Windows.Forms.Padding(3);
            this.relativeFileNameLabel3.MinimumSize = new System.Drawing.Size(0, 26);
            this.relativeFileNameLabel3.Name = "relativeFileNameLabel3";
            this.relativeFileNameLabel3.Size = new System.Drawing.Size(88, 26);
            this.relativeFileNameLabel3.TabIndex = 8;
            this.relativeFileNameLabel3.Text = "Run option file:";
            this.relativeFileNameLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pathLinkLabel
            // 
            this.pathLinkLabel.AutoSize = true;
            this.pathLinkLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectFileBindingSource1, "FileName", true));
            this.pathLinkLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pathLinkLabel.Location = new System.Drawing.Point(97, 3);
            this.pathLinkLabel.Margin = new System.Windows.Forms.Padding(3);
            this.pathLinkLabel.Name = "pathLinkLabel";
            this.pathLinkLabel.Size = new System.Drawing.Size(55, 29);
            this.pathLinkLabel.TabIndex = 1;
            this.pathLinkLabel.TabStop = true;
            this.pathLinkLabel.Text = "linkLabel1";
            this.pathLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pathLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.projectFileLinkLabel_LinkClicked);
            // 
            // projectFileBindingSource1
            // 
            this.projectFileBindingSource1.DataSource = typeof(SiriusModel.InOut.ProjectFile);
            // 
            // directoryLabel
            // 
            this.directoryLabel.AutoSize = true;
            this.directoryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.directoryLabel.Location = new System.Drawing.Point(3, 38);
            this.directoryLabel.Margin = new System.Windows.Forms.Padding(3);
            this.directoryLabel.MinimumSize = new System.Drawing.Size(0, 26);
            this.directoryLabel.Name = "directoryLabel";
            this.directoryLabel.Size = new System.Drawing.Size(88, 26);
            this.directoryLabel.TabIndex = 14;
            this.directoryLabel.Text = "Main directory:";
            this.directoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // directoryLabel1
            // 
            this.directoryLabel1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.directoryLabel1, 3);
            this.directoryLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectFileBindingSource1, "Directory", true));
            this.directoryLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.directoryLabel1.Location = new System.Drawing.Point(97, 38);
            this.directoryLabel1.Margin = new System.Windows.Forms.Padding(3);
            this.directoryLabel1.Name = "directoryLabel1";
            this.directoryLabel1.Size = new System.Drawing.Size(938, 26);
            this.directoryLabel1.TabIndex = 15;
            this.directoryLabel1.Text = "label1";
            this.directoryLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // relativeFileNameLabel5
            // 
            this.relativeFileNameLabel5.AutoSize = true;
            this.relativeFileNameLabel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relativeFileNameLabel5.Location = new System.Drawing.Point(3, 78);
            this.relativeFileNameLabel5.Margin = new System.Windows.Forms.Padding(3);
            this.relativeFileNameLabel5.MinimumSize = new System.Drawing.Size(0, 26);
            this.relativeFileNameLabel5.Name = "relativeFileNameLabel5";
            this.relativeFileNameLabel5.Size = new System.Drawing.Size(88, 26);
            this.relativeFileNameLabel5.TabIndex = 12;
            this.relativeFileNameLabel5.Text = "Management file:";
            this.relativeFileNameLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // relativeFileNameLinkLabel5
            // 
            this.relativeFileNameLinkLabel5.AutoSize = true;
            this.relativeFileNameLinkLabel5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fileContainerBindingSource1, "ManagementFileName", true));
            this.relativeFileNameLinkLabel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relativeFileNameLinkLabel5.Location = new System.Drawing.Point(97, 78);
            this.relativeFileNameLinkLabel5.Margin = new System.Windows.Forms.Padding(3);
            this.relativeFileNameLinkLabel5.Name = "relativeFileNameLinkLabel5";
            this.relativeFileNameLinkLabel5.Size = new System.Drawing.Size(55, 26);
            this.relativeFileNameLinkLabel5.TabIndex = 13;
            this.relativeFileNameLinkLabel5.TabStop = true;
            this.relativeFileNameLinkLabel5.Tag = "Management";
            this.relativeFileNameLinkLabel5.Text = "linklabel1";
            this.relativeFileNameLinkLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.relativeFileNameLinkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.inputFileLinkLabel_LinkClicked);
            // 
            // fileContainerBindingSource1
            // 
            this.fileContainerBindingSource1.DataSource = typeof(SiriusModel.InOut.FileContainer);
            this.fileContainerBindingSource1.CurrentItemChanged += new System.EventHandler(this.fileContainerBindingSource1_CurrentItemChanged);
            // 
            // relativeFileNameLabel4
            // 
            this.relativeFileNameLabel4.AutoSize = true;
            this.relativeFileNameLabel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relativeFileNameLabel4.Location = new System.Drawing.Point(3, 110);
            this.relativeFileNameLabel4.Margin = new System.Windows.Forms.Padding(3);
            this.relativeFileNameLabel4.MinimumSize = new System.Drawing.Size(0, 26);
            this.relativeFileNameLabel4.Name = "relativeFileNameLabel4";
            this.relativeFileNameLabel4.Size = new System.Drawing.Size(88, 26);
            this.relativeFileNameLabel4.TabIndex = 10;
            this.relativeFileNameLabel4.Text = "Non-Variety file:";
            this.relativeFileNameLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // relativeFileNameLinkLabel4
            // 
            this.relativeFileNameLinkLabel4.AutoSize = true;
            this.relativeFileNameLinkLabel4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fileContainerBindingSource1, "NonVarietyFileName", true));
            this.relativeFileNameLinkLabel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relativeFileNameLinkLabel4.Location = new System.Drawing.Point(97, 110);
            this.relativeFileNameLinkLabel4.Margin = new System.Windows.Forms.Padding(3);
            this.relativeFileNameLinkLabel4.Name = "relativeFileNameLinkLabel4";
            this.relativeFileNameLinkLabel4.Size = new System.Drawing.Size(55, 26);
            this.relativeFileNameLinkLabel4.TabIndex = 11;
            this.relativeFileNameLinkLabel4.TabStop = true;
            this.relativeFileNameLinkLabel4.Tag = "Non-varietal parameters";
            this.relativeFileNameLinkLabel4.Text = "linklabel1";
            this.relativeFileNameLinkLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.relativeFileNameLinkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.inputFileLinkLabel_LinkClicked);
            // 
            // relativeFileNameLinkLabel3
            // 
            this.relativeFileNameLinkLabel3.AutoSize = true;
            this.relativeFileNameLinkLabel3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fileContainerBindingSource1, "RunOptionFileName", true));
            this.relativeFileNameLinkLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relativeFileNameLinkLabel3.Location = new System.Drawing.Point(97, 142);
            this.relativeFileNameLinkLabel3.Margin = new System.Windows.Forms.Padding(3);
            this.relativeFileNameLinkLabel3.Name = "relativeFileNameLinkLabel3";
            this.relativeFileNameLinkLabel3.Size = new System.Drawing.Size(55, 26);
            this.relativeFileNameLinkLabel3.TabIndex = 9;
            this.relativeFileNameLinkLabel3.TabStop = true;
            this.relativeFileNameLinkLabel3.Tag = "Run options";
            this.relativeFileNameLinkLabel3.Text = "linklabel1";
            this.relativeFileNameLinkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.relativeFileNameLinkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.inputFileLinkLabel_LinkClicked);
            // 
            // relativeFileNameLabel2
            // 
            this.relativeFileNameLabel2.AutoSize = true;
            this.relativeFileNameLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relativeFileNameLabel2.Location = new System.Drawing.Point(3, 174);
            this.relativeFileNameLabel2.Margin = new System.Windows.Forms.Padding(3);
            this.relativeFileNameLabel2.MinimumSize = new System.Drawing.Size(0, 26);
            this.relativeFileNameLabel2.Name = "relativeFileNameLabel2";
            this.relativeFileNameLabel2.Size = new System.Drawing.Size(88, 26);
            this.relativeFileNameLabel2.TabIndex = 6;
            this.relativeFileNameLabel2.Text = "Site file:";
            this.relativeFileNameLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // relativeFileNameLinkLabel2
            // 
            this.relativeFileNameLinkLabel2.AutoSize = true;
            this.relativeFileNameLinkLabel2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fileContainerBindingSource1, "SiteFileName", true));
            this.relativeFileNameLinkLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relativeFileNameLinkLabel2.Location = new System.Drawing.Point(97, 174);
            this.relativeFileNameLinkLabel2.Margin = new System.Windows.Forms.Padding(3);
            this.relativeFileNameLinkLabel2.Name = "relativeFileNameLinkLabel2";
            this.relativeFileNameLinkLabel2.Size = new System.Drawing.Size(55, 26);
            this.relativeFileNameLinkLabel2.TabIndex = 7;
            this.relativeFileNameLinkLabel2.TabStop = true;
            this.relativeFileNameLinkLabel2.Tag = "Site";
            this.relativeFileNameLinkLabel2.Text = "linklabel1";
            this.relativeFileNameLinkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.relativeFileNameLinkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.inputFileLinkLabel_LinkClicked);
            // 
            // relativeFileNameLabel
            // 
            this.relativeFileNameLabel.AutoSize = true;
            this.relativeFileNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relativeFileNameLabel.Location = new System.Drawing.Point(3, 238);
            this.relativeFileNameLabel.Margin = new System.Windows.Forms.Padding(3);
            this.relativeFileNameLabel.MinimumSize = new System.Drawing.Size(0, 26);
            this.relativeFileNameLabel.Name = "relativeFileNameLabel";
            this.relativeFileNameLabel.Size = new System.Drawing.Size(88, 26);
            this.relativeFileNameLabel.TabIndex = 2;
            this.relativeFileNameLabel.Text = "Variety file:";
            this.relativeFileNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // relativeFileNameLabel1
            // 
            this.relativeFileNameLabel1.AutoSize = true;
            this.relativeFileNameLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relativeFileNameLabel1.Location = new System.Drawing.Point(3, 206);
            this.relativeFileNameLabel1.Margin = new System.Windows.Forms.Padding(3);
            this.relativeFileNameLabel1.MinimumSize = new System.Drawing.Size(0, 26);
            this.relativeFileNameLabel1.Name = "relativeFileNameLabel1";
            this.relativeFileNameLabel1.Size = new System.Drawing.Size(88, 26);
            this.relativeFileNameLabel1.TabIndex = 4;
            this.relativeFileNameLabel1.Text = "Soil file:";
            this.relativeFileNameLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // relativeFileNameLinkLabel1
            // 
            this.relativeFileNameLinkLabel1.AutoSize = true;
            this.relativeFileNameLinkLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fileContainerBindingSource1, "SoilFileName", true));
            this.relativeFileNameLinkLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relativeFileNameLinkLabel1.Location = new System.Drawing.Point(97, 206);
            this.relativeFileNameLinkLabel1.Margin = new System.Windows.Forms.Padding(3);
            this.relativeFileNameLinkLabel1.Name = "relativeFileNameLinkLabel1";
            this.relativeFileNameLinkLabel1.Size = new System.Drawing.Size(55, 26);
            this.relativeFileNameLinkLabel1.TabIndex = 5;
            this.relativeFileNameLinkLabel1.TabStop = true;
            this.relativeFileNameLinkLabel1.Tag = "Soil";
            this.relativeFileNameLinkLabel1.Text = "linklabel1";
            this.relativeFileNameLinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.relativeFileNameLinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.inputFileLinkLabel_LinkClicked);
            // 
            // relativeFileNameLinkLabel
            // 
            this.relativeFileNameLinkLabel.AutoSize = true;
            this.relativeFileNameLinkLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fileContainerBindingSource1, "VarietyFileName", true));
            this.relativeFileNameLinkLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relativeFileNameLinkLabel.Location = new System.Drawing.Point(97, 238);
            this.relativeFileNameLinkLabel.Margin = new System.Windows.Forms.Padding(3);
            this.relativeFileNameLinkLabel.Name = "relativeFileNameLinkLabel";
            this.relativeFileNameLinkLabel.Size = new System.Drawing.Size(55, 26);
            this.relativeFileNameLinkLabel.TabIndex = 3;
            this.relativeFileNameLinkLabel.TabStop = true;
            this.relativeFileNameLinkLabel.Tag = "Varietal parameters";
            this.relativeFileNameLinkLabel.Text = "linklabel1";
            this.relativeFileNameLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.relativeFileNameLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.inputFileLinkLabel_LinkClicked);
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pathLabel.Location = new System.Drawing.Point(3, 3);
            this.pathLabel.Margin = new System.Windows.Forms.Padding(3);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(88, 29);
            this.pathLabel.TabIndex = 0;
            this.pathLabel.Text = "Project file:";
            this.pathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.linkLabel2, 4, 9);
            this.tableLayoutPanel1.Controls.Add(this.labelModified, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.pathLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLinkLabel, 2, 9);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLinkLabel1, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLabel1, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLabel, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLinkLabel2, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLabel2, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLinkLabel3, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLinkLabel4, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLabel4, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLinkLabel5, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLabel5, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.directoryLabel1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.directoryLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pathLinkLabel, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLabel3, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 4, 13);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 11);
            this.tableLayoutPanel1.Controls.Add(this.linkLabel1, 2, 10);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 4, 10);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLabel6, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLinkLabel6, 2, 11);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLabel7, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLinkLabel7, 2, 13);
            this.tableLayoutPanel1.Controls.Add(this.label1, 3, 9);
            this.tableLayoutPanel1.Controls.Add(this.linkLabel3, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 13;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1038, 423);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fileContainerBindingSource1, "MaizeVarietyFileName", true));
            this.linkLabel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.linkLabel2.Location = new System.Drawing.Point(276, 235);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(51, 32);
            this.linkLabel2.TabIndex = 18;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Tag = "Varietal parameters for maize";
            this.linkLabel2.Text = "linklabel2";
            this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel2.UseWaitCursor = true;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.inputFileLinkLabel_LinkClicked);
            // 
            // labelModified
            // 
            this.labelModified.AutoSize = true;
            this.labelModified.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelModified.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelModified.ForeColor = System.Drawing.Color.Blue;
            this.labelModified.Location = new System.Drawing.Point(158, 0);
            this.labelModified.Name = "labelModified";
            this.labelModified.Size = new System.Drawing.Size(112, 35);
            this.labelModified.TabIndex = 23;
            this.labelModified.Text = "*";
            this.labelModified.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.buttonNewProject);
            this.flowLayoutPanel2.Controls.Add(this.buttonSaveProject);
            this.flowLayoutPanel2.Controls.Add(this.buttonSaveProjectAs);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(276, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(759, 29);
            this.flowLayoutPanel2.TabIndex = 17;
            // 
            // buttonNewProject
            // 
            this.buttonNewProject.Location = new System.Drawing.Point(3, 3);
            this.buttonNewProject.Name = "buttonNewProject";
            this.buttonNewProject.Size = new System.Drawing.Size(75, 23);
            this.buttonNewProject.TabIndex = 19;
            this.buttonNewProject.Text = "New project";
            this.buttonNewProject.UseVisualStyleBackColor = true;
            this.buttonNewProject.Click += new System.EventHandler(this.buttonNewProject_Click);
            // 
            // buttonSaveProject
            // 
            this.buttonSaveProject.AutoSize = true;
            this.buttonSaveProject.Location = new System.Drawing.Point(84, 3);
            this.buttonSaveProject.Name = "buttonSaveProject";
            this.buttonSaveProject.Size = new System.Drawing.Size(77, 23);
            this.buttonSaveProject.TabIndex = 17;
            this.buttonSaveProject.Text = "Save project";
            this.buttonSaveProject.UseVisualStyleBackColor = true;
            this.buttonSaveProject.Click += new System.EventHandler(this.buttonSaveProject_Click);
            // 
            // buttonSaveProjectAs
            // 
            this.buttonSaveProjectAs.AutoSize = true;
            this.buttonSaveProjectAs.Location = new System.Drawing.Point(167, 3);
            this.buttonSaveProjectAs.Name = "buttonSaveProjectAs";
            this.buttonSaveProjectAs.Size = new System.Drawing.Size(91, 23);
            this.buttonSaveProjectAs.TabIndex = 18;
            this.buttonSaveProjectAs.Text = "Save project as";
            this.buttonSaveProjectAs.UseVisualStyleBackColor = true;
            this.buttonSaveProjectAs.Click += new System.EventHandler(this.buttonSaveProjectAs_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(276, 374);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel1.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 270);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.MinimumSize = new System.Drawing.Size(0, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 66);
            this.label4.TabIndex = 27;
            this.label4.Text = "run file";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(158, 342);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.MinimumSize = new System.Drawing.Size(0, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 26);
            this.label5.TabIndex = 31;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fileContainerBindingSource1, "RunFileName", true));
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel1.Location = new System.Drawing.Point(97, 270);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(3);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(55, 66);
            this.linkLabel1.TabIndex = 34;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Tag = "Run";
            this.linkLabel1.Text = "linklabel1";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.inputFileLinkLabel_LinkClicked);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel4);
            this.flowLayoutPanel3.Controls.Add(this.buttonLoadAll);
            this.flowLayoutPanel3.Controls.Add(this.buttonSaveAll);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(276, 270);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(671, 66);
            this.flowLayoutPanel3.TabIndex = 35;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.Controls.Add(this.buttonRunAllSimple);
            this.flowLayoutPanel4.Controls.Add(this.buttonRunAllBatch);
            this.flowLayoutPanel4.Controls.Add(this.buttonRunAllSensitivity);
            this.flowLayoutPanel4.Controls.Add(this.buttonExportAllSimple);
            this.flowLayoutPanel4.Controls.Add(this.buttonExportAllBatch);
            this.flowLayoutPanel4.Controls.Add(this.buttonExportAllSensitivity);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(586, 29);
            this.flowLayoutPanel4.TabIndex = 25;
            // 
            // buttonRunAllSimple
            // 
            this.buttonRunAllSimple.AutoSize = true;
            this.buttonRunAllSimple.Location = new System.Drawing.Point(3, 3);
            this.buttonRunAllSimple.Name = "buttonRunAllSimple";
            this.buttonRunAllSimple.Size = new System.Drawing.Size(82, 23);
            this.buttonRunAllSimple.TabIndex = 0;
            this.buttonRunAllSimple.Text = "Run all simple";
            this.buttonRunAllSimple.UseVisualStyleBackColor = true;
            // 
            // buttonRunAllBatch
            // 
            this.buttonRunAllBatch.AutoSize = true;
            this.buttonRunAllBatch.Location = new System.Drawing.Point(91, 3);
            this.buttonRunAllBatch.Name = "buttonRunAllBatch";
            this.buttonRunAllBatch.Size = new System.Drawing.Size(80, 23);
            this.buttonRunAllBatch.TabIndex = 1;
            this.buttonRunAllBatch.Text = "Run all batch";
            this.buttonRunAllBatch.UseVisualStyleBackColor = true;
            // 
            // buttonRunAllSensitivity
            // 
            this.buttonRunAllSensitivity.AutoSize = true;
            this.buttonRunAllSensitivity.Location = new System.Drawing.Point(177, 3);
            this.buttonRunAllSensitivity.Name = "buttonRunAllSensitivity";
            this.buttonRunAllSensitivity.Size = new System.Drawing.Size(98, 23);
            this.buttonRunAllSensitivity.TabIndex = 2;
            this.buttonRunAllSensitivity.Text = "Run all sensitivity";
            this.buttonRunAllSensitivity.UseVisualStyleBackColor = true;
            // 
            // buttonExportAllSimple
            // 
            this.buttonExportAllSimple.AutoSize = true;
            this.buttonExportAllSimple.Location = new System.Drawing.Point(281, 3);
            this.buttonExportAllSimple.Name = "buttonExportAllSimple";
            this.buttonExportAllSimple.Size = new System.Drawing.Size(92, 23);
            this.buttonExportAllSimple.TabIndex = 3;
            this.buttonExportAllSimple.Text = "Export all simple";
            this.buttonExportAllSimple.UseVisualStyleBackColor = true;
            // 
            // buttonExportAllBatch
            // 
            this.buttonExportAllBatch.AutoSize = true;
            this.buttonExportAllBatch.Location = new System.Drawing.Point(379, 3);
            this.buttonExportAllBatch.Name = "buttonExportAllBatch";
            this.buttonExportAllBatch.Size = new System.Drawing.Size(90, 23);
            this.buttonExportAllBatch.TabIndex = 4;
            this.buttonExportAllBatch.Text = "Export all batch";
            this.buttonExportAllBatch.UseVisualStyleBackColor = true;
            // 
            // buttonExportAllSensitivity
            // 
            this.buttonExportAllSensitivity.AutoSize = true;
            this.buttonExportAllSensitivity.Location = new System.Drawing.Point(475, 3);
            this.buttonExportAllSensitivity.Name = "buttonExportAllSensitivity";
            this.buttonExportAllSensitivity.Size = new System.Drawing.Size(108, 23);
            this.buttonExportAllSensitivity.TabIndex = 5;
            this.buttonExportAllSensitivity.Text = "Export all sensitivity";
            this.buttonExportAllSensitivity.UseVisualStyleBackColor = true;
            // 
            // buttonLoadAll
            // 
            this.buttonLoadAll.AutoSize = true;
            this.buttonLoadAll.Location = new System.Drawing.Point(3, 38);
            this.buttonLoadAll.Name = "buttonLoadAll";
            this.buttonLoadAll.Size = new System.Drawing.Size(101, 23);
            this.buttonLoadAll.TabIndex = 1;
            this.buttonLoadAll.Text = "Load all input files";
            this.buttonLoadAll.UseVisualStyleBackColor = true;
            this.buttonLoadAll.Click += new System.EventHandler(this.buttonLoadAll_Click);
            // 
            // buttonSaveAll
            // 
            this.buttonSaveAll.AutoSize = true;
            this.buttonSaveAll.Location = new System.Drawing.Point(110, 38);
            this.buttonSaveAll.Name = "buttonSaveAll";
            this.buttonSaveAll.Size = new System.Drawing.Size(102, 23);
            this.buttonSaveAll.TabIndex = 0;
            this.buttonSaveAll.Text = "Save all input files";
            this.buttonSaveAll.UseVisualStyleBackColor = true;
            this.buttonSaveAll.Click += new System.EventHandler(this.buttonSaveAll_Click);
            // 
            // relativeFileNameLabel6
            // 
            this.relativeFileNameLabel6.AutoSize = true;
            this.relativeFileNameLabel6.Location = new System.Drawing.Point(3, 339);
            this.relativeFileNameLabel6.Name = "relativeFileNameLabel6";
            this.relativeFileNameLabel6.Size = new System.Drawing.Size(83, 13);
            this.relativeFileNameLabel6.TabIndex = 36;
            this.relativeFileNameLabel6.Text = "Observation file:";
            // 
            // relativeFileNameLinkLabel6
            // 
            this.relativeFileNameLinkLabel6.AutoSize = true;
            this.relativeFileNameLinkLabel6.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fileContainerBindingSource1, "ObservationFileName", true));
            this.relativeFileNameLinkLabel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relativeFileNameLinkLabel6.Location = new System.Drawing.Point(97, 339);
            this.relativeFileNameLinkLabel6.Name = "relativeFileNameLinkLabel6";
            this.relativeFileNameLinkLabel6.Size = new System.Drawing.Size(55, 32);
            this.relativeFileNameLinkLabel6.TabIndex = 37;
            this.relativeFileNameLinkLabel6.TabStop = true;
            this.relativeFileNameLinkLabel6.Tag = "Observation";
            this.relativeFileNameLinkLabel6.Text = "linkLabel1";
            this.relativeFileNameLinkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.inputFileLinkLabel_LinkClicked);
            // 
            // relativeFileNameLabel7
            // 
            this.relativeFileNameLabel7.AutoSize = true;
            this.relativeFileNameLabel7.Location = new System.Drawing.Point(3, 371);
            this.relativeFileNameLabel7.Name = "relativeFileNameLabel7";
            this.relativeFileNameLabel7.Size = new System.Drawing.Size(83, 13);
            this.relativeFileNameLabel7.TabIndex = 38;
            this.relativeFileNameLabel7.Text = "Optimization file:";
            // 
            // relativeFileNameLinkLabel7
            // 
            this.relativeFileNameLinkLabel7.AutoSize = true;
            this.relativeFileNameLinkLabel7.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fileContainerBindingSource1, "OptimizationFileName", true));
            this.relativeFileNameLinkLabel7.Location = new System.Drawing.Point(97, 371);
            this.relativeFileNameLinkLabel7.Name = "relativeFileNameLinkLabel7";
            this.relativeFileNameLinkLabel7.Size = new System.Drawing.Size(55, 13);
            this.relativeFileNameLinkLabel7.TabIndex = 39;
            this.relativeFileNameLinkLabel7.TabStop = true;
            this.relativeFileNameLinkLabel7.Text = "linkLabel1";
            this.relativeFileNameLinkLabel7.Tag = "Optimization";
            this.relativeFileNameLinkLabel7.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.inputFileLinkLabel_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(158, 238);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.MinimumSize = new System.Drawing.Size(0, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 26);
            this.label1.TabIndex = 40;
            this.label1.Text = "Maize Variety file:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fileContainerBindingSource1, "MaizeNonVarietyFileName", true));
            this.linkLabel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.linkLabel3.Location = new System.Drawing.Point(276, 107);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(55, 32);
            this.linkLabel3.TabIndex = 42;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Tag = "Non-varietal parameters for maize";
            this.linkLabel3.Text = "linkLabel3";
            this.linkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.inputFileLinkLabel_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(158, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 32);
            this.label3.TabIndex = 41;
            this.label3.Text = "Maize Non-Variety file:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(1038, 547);
            this.splitContainer1.SplitterDistance = 423;
            this.splitContainer1.TabIndex = 17;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.fileContainerBindingSource1, "Comments", true));
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(59, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(979, 120);
            this.textBox1.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Comments:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FileContainerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 547);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FileContainerForm";
            this.Text = "FileContainerForm";
            ((System.ComponentModel.ISupportInitialize)(this.projectFileBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileContainerBindingSource1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label relativeFileNameLabel3;
        private System.Windows.Forms.LinkLabel pathLinkLabel;
        private System.Windows.Forms.Label directoryLabel;
        private System.Windows.Forms.Label directoryLabel1;
        private System.Windows.Forms.Label relativeFileNameLabel5;
        private System.Windows.Forms.LinkLabel relativeFileNameLinkLabel5;
        private System.Windows.Forms.Label relativeFileNameLabel4;
        private System.Windows.Forms.LinkLabel relativeFileNameLinkLabel4;
        private System.Windows.Forms.LinkLabel relativeFileNameLinkLabel3;
        private System.Windows.Forms.Label relativeFileNameLabel2;
        private System.Windows.Forms.LinkLabel relativeFileNameLinkLabel2;
        private System.Windows.Forms.Label relativeFileNameLabel;
        private System.Windows.Forms.Label relativeFileNameLabel1;
        private System.Windows.Forms.LinkLabel relativeFileNameLinkLabel1;
        private System.Windows.Forms.LinkLabel relativeFileNameLinkLabel;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonSaveAll;
        private System.Windows.Forms.Button buttonLoadAll;
        private System.Windows.Forms.Button buttonSaveProject;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button buttonSaveProjectAs;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private ProjectFileBindingSource projectFileBindingSource1;
        private FileContainerBindingSource fileContainerBindingSource1;
        private System.Windows.Forms.Button buttonNewProject;
        private System.Windows.Forms.Label labelModified;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Button buttonRunAllSimple;
        private System.Windows.Forms.Button buttonRunAllBatch;
        private System.Windows.Forms.Button buttonRunAllSensitivity;
        private System.Windows.Forms.Button buttonExportAllSimple;
        private System.Windows.Forms.Button buttonExportAllBatch;
        private System.Windows.Forms.Button buttonExportAllSensitivity;
        private System.Windows.Forms.Label relativeFileNameLabel6;
        private System.Windows.Forms.LinkLabel relativeFileNameLinkLabel6;
        private System.Windows.Forms.Label relativeFileNameLabel7;
        private System.Windows.Forms.LinkLabel relativeFileNameLinkLabel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Label label3;

    }
}