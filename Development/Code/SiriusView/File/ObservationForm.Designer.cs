using System.Collections.Generic;
namespace SiriusView.File
{
    partial class ObservationForm
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
            this.observationItemsBindingSource1 = new SiriusView.ObservationItemsBindingSource(this.components);
            this.canopyOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonDuplicate = new System.Windows.Forms.Button();
            this.buttonSort = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.canopyDataGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.canopyLinkLabel = new System.Windows.Forms.LinkLabel();
            this.canopyFileLabel = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.phytomerDataGridView = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.phytomerLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.phenologyDataGridView = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.phenologyLinkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.soilDataGridView = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.soilLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.HaunIndexTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.haunIndexDataGridView = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.haunIndexLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabelname = new System.Windows.Forms.LinkLabel();
            this.computeDateButton = new System.Windows.Forms.Button();
            this.managementItemsBindingSource1 = new SiriusView.ManagementItemsBindingSource(this.components);
            this.varietyItemsBindingSource1 = new SiriusView.VarietyItemsBindingSource(this.components);
            this.soilItemsBindingSource1 = new SiriusView.SoilItemsBindingSource(this.components);
            this.siteItemsBindingSource1 = new SiriusView.SiteItemsBindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.observationItemsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canopyDataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.phytomerDataGridView)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.phenologyDataGridView)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.soilDataGridView)).BeginInit();
            this.panel4.SuspendLayout();
            this.HaunIndexTabPage.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.haunIndexDataGridView)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.managementItemsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.varietyItemsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.soilItemsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.siteItemsBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // observationItemsBindingSource1
            // 
            this.observationItemsBindingSource1.AllowNew = true;
            this.observationItemsBindingSource1.DataSource = typeof(SiriusModel.InOut.ObservationItem);
            this.observationItemsBindingSource1.CurrentChanged += new System.EventHandler(this.observationItemsBindingSource1_CurrentChanged);
            // 
            // canopyOpenFileDialog
            // 
            this.canopyOpenFileDialog.FileName = "canopyOpenFileDialog";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 29);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBox1);
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Panel2.Enabled = false;
            this.splitContainer1.Size = new System.Drawing.Size(787, 446);
            this.splitContainer1.SplitterDistance = 161;
            this.splitContainer1.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.DataSource = this.observationItemsBindingSource1;
            this.listBox1.DisplayMember = "Name";
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(161, 359);
            this.listBox1.TabIndex = 3;
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
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 359);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(161, 87);
            this.flowLayoutPanel1.TabIndex = 4;
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
            this.buttonClear.Location = new System.Drawing.Point(3, 32);
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
            this.buttonDuplicate.Location = new System.Drawing.Point(70, 32);
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
            this.buttonSort.Location = new System.Drawing.Point(3, 61);
            this.buttonSort.Name = "buttonSort";
            this.buttonSort.Size = new System.Drawing.Size(36, 23);
            this.buttonSort.TabIndex = 4;
            this.buttonSort.Text = "Sort";
            this.buttonSort.UseVisualStyleBackColor = true;
            this.buttonSort.Click += new System.EventHandler(this.buttonSort_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.splitContainer2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.loadButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.linkLabelname, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.computeDateButton, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(622, 446);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.splitContainer2, 4);
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 32);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer2.Size = new System.Drawing.Size(616, 411);
            this.splitContainer2.SplitterDistance = 333;
            this.splitContainer2.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.HaunIndexTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(616, 333);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(608, 307);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Canopy";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.canopyDataGridView, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(602, 301);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // canopyDataGridView
            // 
            this.canopyDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.canopyDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.canopyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.canopyDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canopyDataGridView.Location = new System.Drawing.Point(3, 28);
            this.canopyDataGridView.Name = "canopyDataGridView";
            this.canopyDataGridView.ReadOnly = true;
            this.canopyDataGridView.RowHeadersVisible = false;
            this.canopyDataGridView.Size = new System.Drawing.Size(596, 270);
            this.canopyDataGridView.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.canopyLinkLabel);
            this.panel1.Controls.Add(this.canopyFileLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(596, 19);
            this.panel1.TabIndex = 0;
            // 
            // canopyLinkLabel
            // 
            this.canopyLinkLabel.AutoSize = true;
            this.canopyLinkLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.observationItemsBindingSource1, "canopyObservationFile", true));
            this.canopyLinkLabel.Location = new System.Drawing.Point(41, 0);
            this.canopyLinkLabel.Name = "canopyLinkLabel";
            this.canopyLinkLabel.Size = new System.Drawing.Size(49, 13);
            this.canopyLinkLabel.TabIndex = 1;
            this.canopyLinkLabel.TabStop = true;
            this.canopyLinkLabel.Text = "linkLabel";
            this.canopyLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.canopyLinkLabel_LinkClicked);
            // 
            // canopyFileLabel
            // 
            this.canopyFileLabel.AutoSize = true;
            this.canopyFileLabel.Location = new System.Drawing.Point(3, 0);
            this.canopyFileLabel.Name = "canopyFileLabel";
            this.canopyFileLabel.Size = new System.Drawing.Size(29, 13);
            this.canopyFileLabel.TabIndex = 0;
            this.canopyFileLabel.Text = "File :";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(608, 307);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Phytomer";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.phytomerDataGridView, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(602, 301);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // phytomerDataGridView
            // 
            this.phytomerDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.phytomerDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.phytomerDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.phytomerDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.phytomerDataGridView.Location = new System.Drawing.Point(3, 28);
            this.phytomerDataGridView.Name = "phytomerDataGridView";
            this.phytomerDataGridView.ReadOnly = true;
            this.phytomerDataGridView.RowHeadersVisible = false;
            this.phytomerDataGridView.Size = new System.Drawing.Size(596, 270);
            this.phytomerDataGridView.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.phytomerLinkLabel);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(596, 19);
            this.panel2.TabIndex = 0;
            // 
            // phytomerLinkLabel
            // 
            this.phytomerLinkLabel.AutoSize = true;
            this.phytomerLinkLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.observationItemsBindingSource1, "phytomerObservationFile", true));
            this.phytomerLinkLabel.Location = new System.Drawing.Point(41, 0);
            this.phytomerLinkLabel.Name = "phytomerLinkLabel";
            this.phytomerLinkLabel.Size = new System.Drawing.Size(55, 13);
            this.phytomerLinkLabel.TabIndex = 1;
            this.phytomerLinkLabel.TabStop = true;
            this.phytomerLinkLabel.Text = "linkLabel1";
            this.phytomerLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.phytomerLinkLabel_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File :";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(608, 307);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Phenology and Yield components";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.phenologyDataGridView, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(602, 301);
            this.tableLayoutPanel5.TabIndex = 3;
            // 
            // phenologyDataGridView
            // 
            this.phenologyDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.phenologyDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.phenologyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.phenologyDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.phenologyDataGridView.Location = new System.Drawing.Point(3, 28);
            this.phenologyDataGridView.Name = "phenologyDataGridView";
            this.phenologyDataGridView.ReadOnly = true;
            this.phenologyDataGridView.RowHeadersVisible = false;
            this.phenologyDataGridView.Size = new System.Drawing.Size(596, 270);
            this.phenologyDataGridView.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.phenologyLinkLabel2);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(596, 19);
            this.panel3.TabIndex = 0;
            // 
            // phenologyLinkLabel2
            // 
            this.phenologyLinkLabel2.AutoSize = true;
            this.phenologyLinkLabel2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.observationItemsBindingSource1, "phenologyObservationFile", true));
            this.phenologyLinkLabel2.Location = new System.Drawing.Point(41, 0);
            this.phenologyLinkLabel2.Name = "phenologyLinkLabel2";
            this.phenologyLinkLabel2.Size = new System.Drawing.Size(55, 13);
            this.phenologyLinkLabel2.TabIndex = 1;
            this.phenologyLinkLabel2.TabStop = true;
            this.phenologyLinkLabel2.Text = "linkLabel2";
            this.phenologyLinkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.phenologyLinkLabel_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "File :";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tableLayoutPanel6);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(608, 307);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Soil";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.soilDataGridView, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(602, 301);
            this.tableLayoutPanel6.TabIndex = 3;
            // 
            // soilDataGridView
            // 
            this.soilDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.soilDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.soilDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.soilDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.soilDataGridView.Location = new System.Drawing.Point(3, 28);
            this.soilDataGridView.Name = "soilDataGridView";
            this.soilDataGridView.ReadOnly = true;
            this.soilDataGridView.RowHeadersVisible = false;
            this.soilDataGridView.Size = new System.Drawing.Size(596, 270);
            this.soilDataGridView.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.soilLinkLabel);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(596, 19);
            this.panel4.TabIndex = 0;
            // 
            // soilLinkLabel
            // 
            this.soilLinkLabel.AutoSize = true;
            this.soilLinkLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.observationItemsBindingSource1, "soilObservationFile", true));
            this.soilLinkLabel.Location = new System.Drawing.Point(41, 0);
            this.soilLinkLabel.Name = "soilLinkLabel";
            this.soilLinkLabel.Size = new System.Drawing.Size(55, 13);
            this.soilLinkLabel.TabIndex = 1;
            this.soilLinkLabel.TabStop = true;
            this.soilLinkLabel.Text = "linkLabel3";
            this.soilLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.soilLinkLabel_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "File :";
            // 
            // HaunIndexTabPage
            // 
            this.HaunIndexTabPage.Controls.Add(this.tableLayoutPanel7);
            this.HaunIndexTabPage.Location = new System.Drawing.Point(4, 22);
            this.HaunIndexTabPage.Name = "HaunIndexTabPage";
            this.HaunIndexTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.HaunIndexTabPage.Size = new System.Drawing.Size(608, 307);
            this.HaunIndexTabPage.TabIndex = 4;
            this.HaunIndexTabPage.Text = "Haun Index";
            this.HaunIndexTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Controls.Add(this.haunIndexDataGridView, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(602, 301);
            this.tableLayoutPanel7.TabIndex = 4;
            // 
            // haunIndexDataGridView
            // 
            this.haunIndexDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.haunIndexDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.haunIndexDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.haunIndexDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.haunIndexDataGridView.Location = new System.Drawing.Point(3, 28);
            this.haunIndexDataGridView.Name = "haunIndexDataGridView";
            this.haunIndexDataGridView.ReadOnly = true;
            this.haunIndexDataGridView.RowHeadersVisible = false;
            this.haunIndexDataGridView.Size = new System.Drawing.Size(596, 270);
            this.haunIndexDataGridView.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.haunIndexLinkLabel);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(596, 19);
            this.panel5.TabIndex = 0;
            // 
            // haunIndexLinkLabel
            // 
            this.haunIndexLinkLabel.AutoSize = true;
            this.haunIndexLinkLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.observationItemsBindingSource1, "HaunIndexObservationFile", true));
            this.haunIndexLinkLabel.Location = new System.Drawing.Point(41, 0);
            this.haunIndexLinkLabel.Name = "haunIndexLinkLabel";
            this.haunIndexLinkLabel.Size = new System.Drawing.Size(55, 13);
            this.haunIndexLinkLabel.TabIndex = 1;
            this.haunIndexLinkLabel.TabStop = true;
            this.haunIndexLinkLabel.Text = "linkLabel4";
            this.haunIndexLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.haunIndexLinkLabel_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "File :";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(616, 74);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(3, 3);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(103, 23);
            this.loadButton.TabIndex = 5;
            this.loadButton.Text = "Load observations";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(112, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 29);
            this.label4.TabIndex = 3;
            this.label4.Text = "Observation name:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkLabelname
            // 
            this.linkLabelname.AutoSize = true;
            this.linkLabelname.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.observationItemsBindingSource1, "Name", true));
            this.linkLabelname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabelname.Location = new System.Drawing.Point(214, 0);
            this.linkLabelname.Name = "linkLabelname";
            this.linkLabelname.Size = new System.Drawing.Size(314, 29);
            this.linkLabelname.TabIndex = 4;
            this.linkLabelname.TabStop = true;
            this.linkLabelname.Text = "linkLabel1";
            this.linkLabelname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabelname.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelname_LinkClicked);
            // 
            // computeDateButton
            // 
            this.computeDateButton.AutoSize = true;
            this.computeDateButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.computeDateButton.Location = new System.Drawing.Point(534, 3);
            this.computeDateButton.Name = "computeDateButton";
            this.computeDateButton.Size = new System.Drawing.Size(85, 23);
            this.computeDateButton.TabIndex = 6;
            this.computeDateButton.Text = "Compute Date";
            this.computeDateButton.UseVisualStyleBackColor = true;
            this.computeDateButton.Click += new System.EventHandler(this.computeDateButton_Click);
            // 
            // managementItemsBindingSource1
            // 
            this.managementItemsBindingSource1.DataSource = typeof(SiriusModel.InOut.ManagementItem);
            // 
            // varietyItemsBindingSource1
            // 
            this.varietyItemsBindingSource1.AllowNew = true;
            this.varietyItemsBindingSource1.DataSource = typeof(SiriusModel.InOut.CropParameterItem);
            // 
            // soilItemsBindingSource1
            // 
            this.soilItemsBindingSource1.DataSource = typeof(SiriusModel.InOut.SoilItem);
            // 
            // siteItemsBindingSource1
            // 
            this.siteItemsBindingSource1.DataSource = typeof(SiriusModel.InOut.SiteItem);
            // 
            // ObservationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 475);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ObservationForm";
            this.TabText = "ObservationForm";
            this.Text = "ObservationForm";
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.observationItemsBindingSource1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canopyDataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.phytomerDataGridView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.phenologyDataGridView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.soilDataGridView)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.HaunIndexTabPage.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.haunIndexDataGridView)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.managementItemsBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.varietyItemsBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.soilItemsBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.siteItemsBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog canopyOpenFileDialog;
        private ObservationItemsBindingSource observationItemsBindingSource1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonDuplicate;
        private System.Windows.Forms.Button buttonSort;
        private ManagementItemsBindingSource managementItemsBindingSource1;
        private VarietyItemsBindingSource varietyItemsBindingSource1;
        private SoilItemsBindingSource soilItemsBindingSource1;
        private SiteItemsBindingSource siteItemsBindingSource1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.DataGridView canopyDataGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel canopyLinkLabel;
        private System.Windows.Forms.Label canopyFileLabel;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DataGridView phytomerDataGridView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel phytomerLinkLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.DataGridView phenologyDataGridView;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.LinkLabel phenologyLinkLabel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.DataGridView soilDataGridView;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.LinkLabel soilLinkLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.LinkLabel linkLabelname;
        private System.Windows.Forms.Button computeDateButton;
        private System.Windows.Forms.TabPage HaunIndexTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.DataGridView haunIndexDataGridView;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.LinkLabel haunIndexLinkLabel;
        private System.Windows.Forms.Label label5;

    }
}