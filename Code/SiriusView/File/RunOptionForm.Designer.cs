using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiriusModel.InOut.Base;
using SiriusModel;
using SiriusModel.InOut;

namespace SiriusView.File
{
    partial class RunOptionForm
    {
        public static string whichPattern;
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
            System.Windows.Forms.Label nameLabel;
            this.runOptionItemsBindingSource1 = new SiriusView.RunOptionItemsBindingSource(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonDuplicate = new System.Windows.Forms.Button();
            this.buttonSort = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxGrainMaturation = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.checkBoxAirTemperature = new System.Windows.Forms.CheckBox();
            this.checkBoxCutOnGrain = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.radioV15 = new System.Windows.Forms.RadioButton();
            this.radioV13 = new System.Windows.Forms.RadioButton();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.radioVMaize = new System.Windows.Forms.RadioButton();
            this.radioCus = new System.Windows.Forms.RadioButton();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNCompen = new System.Windows.Forms.TextBox();
            this.textBoxWCompen = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxNormT = new System.Windows.Forms.CheckBox();
            this.checkBoxNormN = new System.Windows.Forms.CheckBox();
            this.checkBoxNormW = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxWNT = new System.Windows.Forms.CheckBox();
            this.checkBoxNT = new System.Windows.Forms.CheckBox();
            this.checkBoxWT = new System.Windows.Forms.CheckBox();
            this.checkBoxWN = new System.Windows.Forms.CheckBox();
            this.checkBoxT = new System.Windows.Forms.CheckBox();
            this.checkBoxN = new System.Windows.Forms.CheckBox();
            this.checkBoxW = new System.Windows.Forms.CheckBox();
            this.textBoxMaxTemp = new System.Windows.Forms.TextBox();
            this.label147 = new System.Windows.Forms.Label();
            this.radioWholeSeason = new System.Windows.Forms.RadioButton();
            this.radioDaily = new System.Windows.Forms.RadioButton();
            this.linkLabelName = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSummary = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tabDaily = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.clearAllbutton = new System.Windows.Forms.Button();
            this.selectAllbutton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            nameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.runOptionItemsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabSummary.SuspendLayout();
            this.tabDaily.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            nameLabel.Location = new System.Drawing.Point(3, 0);
            nameLabel.MinimumSize = new System.Drawing.Size(0, 20);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(96, 20);
            nameLabel.TabIndex = 10;
            nameLabel.Text = "Run options name:";
            nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // runOptionItemsBindingSource1
            // 
            this.runOptionItemsBindingSource1.DataSource = typeof(SiriusModel.InOut.RunOptionItem);
            this.runOptionItemsBindingSource1.CurrentChanged += new System.EventHandler(this.runOptionItemsBindingSource1_CurrentChanged);
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
            this.splitContainer1.Panel2.AutoScrollMargin = new System.Drawing.Size(2, 2);
            this.splitContainer1.Panel2.AutoScrollMinSize = new System.Drawing.Size(2, 2);
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Panel2.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Size = new System.Drawing.Size(998, 713);
            this.splitContainer1.SplitterDistance = 166;
            this.splitContainer1.TabIndex = 2;
            // 
            // listBox1
            // 
            this.listBox1.DataSource = this.runOptionItemsBindingSource1;
            this.listBox1.DisplayMember = "Name";
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(166, 655);
            this.listBox1.TabIndex = 4;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.buttonAdd);
            this.flowLayoutPanel1.Controls.Add(this.buttonDelete);
            this.flowLayoutPanel1.Controls.Add(this.buttonRemove);
            this.flowLayoutPanel1.Controls.Add(this.buttonDuplicate);
            this.flowLayoutPanel1.Controls.Add(this.buttonSort);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 655);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(166, 58);
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
            // buttonRemove
            // 
            this.buttonRemove.AutoSize = true;
            this.buttonRemove.Location = new System.Drawing.Point(99, 3);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(61, 23);
            this.buttonRemove.TabIndex = 2;
            this.buttonRemove.Text = "Delete all";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonClear_Click);
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
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel10, 0, 1);
            this.tableLayoutPanel2.Controls.Add(nameLabel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.linkLabelName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 2, 6);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel8, 0, 6);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(824, 709);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.AutoSize = true;
            this.tableLayoutPanel10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel10.ColumnCount = 4;
            this.tableLayoutPanel2.SetColumnSpan(this.tableLayoutPanel10, 4);
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel10.Controls.Add(this.groupBox5, 2, 1);
            this.tableLayoutPanel10.Controls.Add(this.groupBox2, 2, 0);
            this.tableLayoutPanel10.Controls.Add(this.textBox3, 4, 0);
            this.tableLayoutPanel10.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel2.SetRowSpan(this.tableLayoutPanel10, 2);
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(818, 243);
            this.tableLayoutPanel10.TabIndex = 21;
            // 
            // groupBox5
            // 
            this.groupBox5.AutoSize = true;
            this.groupBox5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox5.Controls.Add(this.tableLayoutPanel12);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(505, 87);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(271, 112);
            this.groupBox5.TabIndex = 41;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Options";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.AutoSize = true;
            this.tableLayoutPanel12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel12.ColumnCount = 3;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel12.Controls.Add(this.checkBoxGrainMaturation, 0, 2);
            this.tableLayoutPanel12.Controls.Add(this.checkBox3, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.textBox4, 4, 0);
            this.tableLayoutPanel12.Controls.Add(this.checkBoxAirTemperature, 0, 3);
            this.tableLayoutPanel12.Controls.Add(this.checkBoxCutOnGrain, 0, 4);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 4;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(265, 93);
            this.tableLayoutPanel12.TabIndex = 21;
            // 
            // checkBoxGrainMaturation
            // 
            this.checkBoxGrainMaturation.AutoSize = true;
            this.checkBoxGrainMaturation.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.runOptionItemsBindingSource1, "IgnoreGrainMaturation", true));
            this.checkBoxGrainMaturation.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxGrainMaturation.Location = new System.Drawing.Point(3, 26);
            this.checkBoxGrainMaturation.Name = "checkBoxGrainMaturation";
            this.checkBoxGrainMaturation.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBoxGrainMaturation.Size = new System.Drawing.Size(194, 17);
            this.checkBoxGrainMaturation.TabIndex = 42;
            this.checkBoxGrainMaturation.Text = "Ignore grain maturation period";
            this.checkBoxGrainMaturation.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.runOptionItemsBindingSource1, "UseObservedGrainNumber", true));
            this.checkBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBox3.Location = new System.Drawing.Point(3, 3);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBox3.Size = new System.Drawing.Size(194, 17);
            this.checkBox3.TabIndex = 41;
            this.checkBox3.Text = "Use observed grain number";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runOptionItemsBindingSource1, "OutputPattern", true));
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox4.Location = new System.Drawing.Point(203, 3);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(1, 3);
            this.textBox4.TabIndex = 20;
            this.textBox4.WordWrap = false;
            // 
            // checkBoxAirTemperature
            // 
            this.checkBoxAirTemperature.AutoSize = true;
            this.checkBoxAirTemperature.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.runOptionItemsBindingSource1, "UseAirTemperatureForSenescence", true));
            this.checkBoxAirTemperature.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxAirTemperature.Location = new System.Drawing.Point(3, 49);
            this.checkBoxAirTemperature.Name = "checkBoxAirTemperature";
            this.checkBoxAirTemperature.Size = new System.Drawing.Size(194, 17);
            this.checkBoxAirTemperature.TabIndex = 43;
            this.checkBoxAirTemperature.Text = "Use air temperature for senescence";
            this.checkBoxAirTemperature.UseVisualStyleBackColor = true;
            this.checkBoxAirTemperature.CheckedChanged += new System.EventHandler(this.checkBoxAirTemperature_CheckedChanged);
            // 
            // checkBoxCutOnGrain
            // 
            this.checkBoxCutOnGrain.AutoSize = true;
            this.checkBoxCutOnGrain.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.runOptionItemsBindingSource1, "IsCutOnGrainFillNotUse", true));
            this.checkBoxCutOnGrain.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxCutOnGrain.Location = new System.Drawing.Point(3, 72);
            this.checkBoxCutOnGrain.Name = "checkBoxCutOnGrain";
            this.checkBoxCutOnGrain.Size = new System.Drawing.Size(194, 17);
            this.checkBoxCutOnGrain.TabIndex = 44;
            this.checkBoxCutOnGrain.Text = "Disable limitation on grain filling";
            this.checkBoxCutOnGrain.UseVisualStyleBackColor = true;
            this.checkBoxCutOnGrain.CheckedChanged += new System.EventHandler(this.checkBoxAirTemperature_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.tableLayoutPanel6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(505, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(271, 64);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output format";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 212F));
            this.tableLayoutPanel6.Controls.Add(this.radioV15, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.radioV13, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.textBox2, 4, 0);
            this.tableLayoutPanel6.Controls.Add(this.radioVMaize, 2, 2);
            this.tableLayoutPanel6.Controls.Add(this.radioCus, 2, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(265, 45);
            this.tableLayoutPanel6.TabIndex = 21;
            // 
            // radioV15
            // 
            this.radioV15.AutoSize = true;
            this.radioV15.Location = new System.Drawing.Point(3, 26);
            this.radioV15.Name = "radioV15";
            this.radioV15.Size = new System.Drawing.Size(47, 16);
            this.radioV15.TabIndex = 18;
            this.radioV15.TabStop = true;
            this.radioV15.Text = "V1.5";
            this.radioV15.UseVisualStyleBackColor = true;
            this.radioV15.CheckedChanged += new System.EventHandler(this.radioV15_CheckedChanged);
            // 
            // radioV13
            // 
            this.radioV13.AutoSize = true;
            this.radioV13.Location = new System.Drawing.Point(3, 4);
            this.radioV13.Name = "radioV13";
            this.radioV13.Size = new System.Drawing.Size(47, 16);
            this.radioV13.TabIndex = 17;
            this.radioV13.TabStop = true;
            this.radioV13.Text = "V1.3";
            this.radioV13.UseVisualStyleBackColor = true;
            this.radioV13.CheckedChanged += new System.EventHandler(this.radioV13_CheckedChanged);
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runOptionItemsBindingSource1, "OutputPattern", true));
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox2.Location = new System.Drawing.Point(56, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(1, 3);
            this.textBox2.TabIndex = 20;
            this.textBox2.WordWrap = false;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // radioVMaize
            // 
            this.radioVMaize.AutoSize = true;
            this.radioVMaize.Location = new System.Drawing.Point(56, 26);
            this.radioVMaize.Name = "radioVMaize";
            this.radioVMaize.Size = new System.Drawing.Size(53, 16);
            this.radioVMaize.TabIndex = 18;
            this.radioVMaize.TabStop = true;
            this.radioVMaize.Text = "Maize";
            this.radioVMaize.UseVisualStyleBackColor = true;
            this.radioVMaize.CheckedChanged += new System.EventHandler(this.radioVMaize_CheckedChanged);
            // 
            // radioCus
            // 
            this.radioCus.AutoSize = true;
            this.radioCus.Location = new System.Drawing.Point(56, 4);
            this.radioCus.Name = "radioCus";
            this.radioCus.Size = new System.Drawing.Size(79, 16);
            this.radioCus.TabIndex = 19;
            this.radioCus.TabStop = true;
            this.radioCus.Text = "Customized";
            this.radioCus.UseVisualStyleBackColor = true;
            this.radioCus.CheckedChanged += new System.EventHandler(this.radioCus_CheckedChanged);
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runOptionItemsBindingSource1, "OutputPattern", true));
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox3.Location = new System.Drawing.Point(782, 3);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(1, 3);
            this.textBox3.TabIndex = 20;
            this.textBox3.WordWrap = false;
            // 
            // groupBox4
            // 
            this.groupBox4.AutoSize = true;
            this.groupBox4.Controls.Add(this.tableLayoutPanel11);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 10);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBox4.Name = "groupBox4";
            this.tableLayoutPanel10.SetRowSpan(this.groupBox4, 2);
            this.groupBox4.Size = new System.Drawing.Size(496, 230);
            this.groupBox4.TabIndex = 32;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Unlimited resource runs";
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.AutoSize = true;
            this.tableLayoutPanel11.ColumnCount = 3;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel11.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel11.Controls.Add(this.textBoxNCompen, 1, 3);
            this.tableLayoutPanel11.Controls.Add(this.textBoxWCompen, 1, 2);
            this.tableLayoutPanel11.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel11.Controls.Add(this.groupBox1, 0, 5);
            this.tableLayoutPanel11.Controls.Add(this.groupBox3, 2, 0);
            this.tableLayoutPanel11.Controls.Add(this.textBoxMaxTemp, 1, 4);
            this.tableLayoutPanel11.Controls.Add(this.label147, 0, 4);
            this.tableLayoutPanel11.Controls.Add(this.radioWholeSeason, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.radioDaily, 0, 1);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel11.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 6;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(490, 211);
            this.tableLayoutPanel11.TabIndex = 36;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 20);
            this.label3.TabIndex = 51;
            this.label3.Text = "Level of water deficit compensation (%):";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxNCompen
            // 
            this.textBoxNCompen.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runOptionItemsBindingSource1, "NCompensationLevel", true));
            this.textBoxNCompen.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxNCompen.Location = new System.Drawing.Point(206, 69);
            this.textBoxNCompen.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.textBoxNCompen.Name = "textBoxNCompen";
            this.textBoxNCompen.Size = new System.Drawing.Size(39, 20);
            this.textBoxNCompen.TabIndex = 50;
            this.textBoxNCompen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxWCompen
            // 
            this.textBoxWCompen.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runOptionItemsBindingSource1, "WCompensationLevel", true));
            this.textBoxWCompen.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxWCompen.Location = new System.Drawing.Point(206, 46);
            this.textBoxWCompen.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.textBoxWCompen.Name = "textBoxWCompen";
            this.textBoxWCompen.Size = new System.Drawing.Size(39, 20);
            this.textBoxWCompen.TabIndex = 49;
            this.textBoxWCompen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 69);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 20);
            this.label2.TabIndex = 48;
            this.label2.Text = "Level of N deficit compensation (%):";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.tableLayoutPanel11.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.tableLayoutPanel7);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 118);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 90);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Whole season";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.AutoSize = true;
            this.tableLayoutPanel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Controls.Add(this.checkBoxNormT, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.checkBoxNormN, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.checkBoxNormW, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 3;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.Size = new System.Drawing.Size(233, 69);
            this.tableLayoutPanel7.TabIndex = 23;
            // 
            // checkBoxNormT
            // 
            this.checkBoxNormT.AutoSize = true;
            this.checkBoxNormT.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.runOptionItemsBindingSource1, "UnlimitedTemperature", true));
            this.checkBoxNormT.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxNormT.Location = new System.Drawing.Point(3, 49);
            this.checkBoxNormT.Name = "checkBoxNormT";
            this.checkBoxNormT.Size = new System.Drawing.Size(227, 17);
            this.checkBoxNormT.TabIndex = 20;
            this.checkBoxNormT.Text = "Unlimited temperature";
            this.checkBoxNormT.UseVisualStyleBackColor = true;
            this.checkBoxNormT.CheckedChanged += new System.EventHandler(this.checkBoxNormT_CheckedChanged);
            // 
            // checkBoxNormN
            // 
            this.checkBoxNormN.AutoSize = true;
            this.checkBoxNormN.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.runOptionItemsBindingSource1, "UnlimitedNitrogen", true));
            this.checkBoxNormN.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxNormN.Location = new System.Drawing.Point(3, 26);
            this.checkBoxNormN.Name = "checkBoxNormN";
            this.checkBoxNormN.Size = new System.Drawing.Size(227, 17);
            this.checkBoxNormN.TabIndex = 18;
            this.checkBoxNormN.Text = "Unlimited nitrogen";
            this.checkBoxNormN.UseVisualStyleBackColor = true;
            this.checkBoxNormN.CheckedChanged += new System.EventHandler(this.checkBoxNormN_CheckedChanged);
            // 
            // checkBoxNormW
            // 
            this.checkBoxNormW.AutoSize = true;
            this.checkBoxNormW.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.runOptionItemsBindingSource1, "UnlimitedWater", true));
            this.checkBoxNormW.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxNormW.Location = new System.Drawing.Point(3, 3);
            this.checkBoxNormW.Name = "checkBoxNormW";
            this.checkBoxNormW.Size = new System.Drawing.Size(227, 17);
            this.checkBoxNormW.TabIndex = 17;
            this.checkBoxNormW.Text = "Unlimited water";
            this.checkBoxNormW.UseVisualStyleBackColor = true;
            this.checkBoxNormW.CheckedChanged += new System.EventHandler(this.checkBoxNormW_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.tableLayoutPanel9);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(255, 3);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.groupBox3.Name = "groupBox3";
            this.tableLayoutPanel11.SetRowSpan(this.groupBox3, 6);
            this.groupBox3.Size = new System.Drawing.Size(232, 205);
            this.groupBox3.TabIndex = 45;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Daily";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.AutoSize = true;
            this.tableLayoutPanel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.Controls.Add(this.checkBoxWNT, 0, 6);
            this.tableLayoutPanel9.Controls.Add(this.checkBoxNT, 0, 5);
            this.tableLayoutPanel9.Controls.Add(this.checkBoxWT, 0, 4);
            this.tableLayoutPanel9.Controls.Add(this.checkBoxWN, 0, 3);
            this.tableLayoutPanel9.Controls.Add(this.checkBoxT, 0, 2);
            this.tableLayoutPanel9.Controls.Add(this.checkBoxN, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.checkBoxW, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.tableLayoutPanel9.RowCount = 7;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.Size = new System.Drawing.Size(226, 186);
            this.tableLayoutPanel9.TabIndex = 23;
            // 
            // checkBoxWNT
            // 
            this.checkBoxWNT.AutoSize = true;
            this.checkBoxWNT.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.runOptionItemsBindingSource1, "InteractionsWNT", true));
            this.checkBoxWNT.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxWNT.Location = new System.Drawing.Point(3, 141);
            this.checkBoxWNT.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.checkBoxWNT.Name = "checkBoxWNT";
            this.checkBoxWNT.Size = new System.Drawing.Size(220, 17);
            this.checkBoxWNT.TabIndex = 30;
            this.checkBoxWNT.Text = "Unlimted water, nitrogen and temperature";
            this.checkBoxWNT.UseVisualStyleBackColor = true;
            this.checkBoxWNT.CheckedChanged += new System.EventHandler(this.checkBoxWNT_CheckedChanged);
            // 
            // checkBoxNT
            // 
            this.checkBoxNT.AutoSize = true;
            this.checkBoxNT.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.runOptionItemsBindingSource1, "InteractionsNT", true));
            this.checkBoxNT.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxNT.Location = new System.Drawing.Point(3, 118);
            this.checkBoxNT.Name = "checkBoxNT";
            this.checkBoxNT.Size = new System.Drawing.Size(220, 17);
            this.checkBoxNT.TabIndex = 28;
            this.checkBoxNT.Text = "Unlimted nitrogen and temperature";
            this.checkBoxNT.UseVisualStyleBackColor = true;
            this.checkBoxNT.CheckedChanged += new System.EventHandler(this.checkBoxNT_CheckedChanged);
            // 
            // checkBoxWT
            // 
            this.checkBoxWT.AutoSize = true;
            this.checkBoxWT.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.runOptionItemsBindingSource1, "InteractionsWT", true));
            this.checkBoxWT.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxWT.Location = new System.Drawing.Point(3, 95);
            this.checkBoxWT.Name = "checkBoxWT";
            this.checkBoxWT.Size = new System.Drawing.Size(220, 17);
            this.checkBoxWT.TabIndex = 26;
            this.checkBoxWT.Text = "Unlimted water and temperature";
            this.checkBoxWT.UseVisualStyleBackColor = true;
            this.checkBoxWT.CheckedChanged += new System.EventHandler(this.checkBoxWT_CheckedChanged);
            // 
            // checkBoxWN
            // 
            this.checkBoxWN.AutoSize = true;
            this.checkBoxWN.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.runOptionItemsBindingSource1, "InteractionsWN", true));
            this.checkBoxWN.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxWN.Location = new System.Drawing.Point(3, 72);
            this.checkBoxWN.Name = "checkBoxWN";
            this.checkBoxWN.Size = new System.Drawing.Size(220, 17);
            this.checkBoxWN.TabIndex = 24;
            this.checkBoxWN.Text = "Unlimted water and nitrogen";
            this.checkBoxWN.UseVisualStyleBackColor = true;
            this.checkBoxWN.CheckedChanged += new System.EventHandler(this.checkBoxWN_CheckedChanged);
            // 
            // checkBoxT
            // 
            this.checkBoxT.AutoSize = true;
            this.checkBoxT.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.runOptionItemsBindingSource1, "InteractionsT", true));
            this.checkBoxT.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxT.Location = new System.Drawing.Point(3, 49);
            this.checkBoxT.Name = "checkBoxT";
            this.checkBoxT.Size = new System.Drawing.Size(220, 17);
            this.checkBoxT.TabIndex = 18;
            this.checkBoxT.Text = "Unlimted temperature";
            this.checkBoxT.UseVisualStyleBackColor = true;
            this.checkBoxT.CheckedChanged += new System.EventHandler(this.checkBoxT_CheckedChanged);
            // 
            // checkBoxN
            // 
            this.checkBoxN.AutoSize = true;
            this.checkBoxN.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.runOptionItemsBindingSource1, "InteractionsN", true));
            this.checkBoxN.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxN.Location = new System.Drawing.Point(3, 26);
            this.checkBoxN.Name = "checkBoxN";
            this.checkBoxN.Size = new System.Drawing.Size(220, 17);
            this.checkBoxN.TabIndex = 17;
            this.checkBoxN.Text = "Unlimted nitrogen";
            this.checkBoxN.UseVisualStyleBackColor = true;
            this.checkBoxN.CheckedChanged += new System.EventHandler(this.checkBoxN_CheckedChanged);
            // 
            // checkBoxW
            // 
            this.checkBoxW.AutoSize = true;
            this.checkBoxW.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.runOptionItemsBindingSource1, "InteractionsW", true));
            this.checkBoxW.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxW.Location = new System.Drawing.Point(3, 3);
            this.checkBoxW.Name = "checkBoxW";
            this.checkBoxW.Size = new System.Drawing.Size(220, 17);
            this.checkBoxW.TabIndex = 14;
            this.checkBoxW.Text = "Unlimted water";
            this.checkBoxW.UseVisualStyleBackColor = true;
            this.checkBoxW.CheckedChanged += new System.EventHandler(this.checkBoxW_CheckedChanged);
            // 
            // textBoxMaxTemp
            // 
            this.textBoxMaxTemp.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runOptionItemsBindingSource1, "MaxTempThreshold", true));
            this.textBoxMaxTemp.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxMaxTemp.Location = new System.Drawing.Point(206, 92);
            this.textBoxMaxTemp.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.textBoxMaxTemp.Name = "textBoxMaxTemp";
            this.textBoxMaxTemp.Size = new System.Drawing.Size(39, 20);
            this.textBoxMaxTemp.TabIndex = 44;
            this.textBoxMaxTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label147
            // 
            this.label147.AutoSize = true;
            this.label147.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label147.Location = new System.Drawing.Point(3, 92);
            this.label147.Margin = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(203, 20);
            this.label147.TabIndex = 43;
            this.label147.Text = "Maximum daily temperature threshold (°C):";
            this.label147.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioWholeSeason
            // 
            this.radioWholeSeason.AutoSize = true;
            this.radioWholeSeason.Location = new System.Drawing.Point(3, 3);
            this.radioWholeSeason.Name = "radioWholeSeason";
            this.radioWholeSeason.Size = new System.Drawing.Size(93, 17);
            this.radioWholeSeason.TabIndex = 41;
            this.radioWholeSeason.TabStop = true;
            this.radioWholeSeason.Text = "Whole season";
            this.radioWholeSeason.UseVisualStyleBackColor = true;
            this.radioWholeSeason.CheckedChanged += new System.EventHandler(this.radioWholeSeason_CheckedChanged);
            // 
            // radioDaily
            // 
            this.radioDaily.AutoSize = true;
            this.radioDaily.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.runOptionItemsBindingSource1, "DoInteractions", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.radioDaily.Location = new System.Drawing.Point(3, 26);
            this.radioDaily.Name = "radioDaily";
            this.radioDaily.Size = new System.Drawing.Size(48, 17);
            this.radioDaily.TabIndex = 42;
            this.radioDaily.TabStop = true;
            this.radioDaily.Text = "Daily";
            this.radioDaily.UseVisualStyleBackColor = true;
            this.radioDaily.CheckedChanged += new System.EventHandler(this.radioDaily_CheckedChanged);
            // 
            // linkLabelName
            // 
            this.linkLabelName.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.linkLabelName, 2);
            this.linkLabelName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runOptionItemsBindingSource1, "Name", true));
            this.linkLabelName.Dock = System.Windows.Forms.DockStyle.Left;
            this.linkLabelName.Location = new System.Drawing.Point(105, 0);
            this.linkLabelName.Name = "linkLabelName";
            this.linkLabelName.Size = new System.Drawing.Size(55, 20);
            this.linkLabelName.TabIndex = 17;
            this.linkLabelName.TabStop = true;
            this.linkLabelName.Text = "linkLabel1";
            this.linkLabelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabelName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelName_LinkClicked);
            // 
            // panel1
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.splitContainer2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 327);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 379);
            this.panel1.TabIndex = 20;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Left;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel5);
            this.splitContainer2.Size = new System.Drawing.Size(745, 379);
            this.splitContainer2.SplitterDistance = 190;
            this.splitContainer2.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSummary);
            this.tabControl1.Controls.Add(this.tabDaily);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Enabled = false;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(745, 190);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 3;
            // 
            // tabSummary
            // 
            this.tabSummary.AutoScroll = true;
            this.tabSummary.Controls.Add(this.tableLayoutPanel3);
            this.tabSummary.Location = new System.Drawing.Point(4, 22);
            this.tabSummary.Margin = new System.Windows.Forms.Padding(0);
            this.tabSummary.Name = "tabSummary";
            this.tabSummary.Size = new System.Drawing.Size(737, 164);
            this.tabSummary.TabIndex = 1;
            this.tabSummary.Text = "Summary Outputs";
            this.tabSummary.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoScroll = true;
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel3.Size = new System.Drawing.Size(737, 164);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // tabDaily
            // 
            this.tabDaily.Controls.Add(this.tableLayoutPanel4);
            this.tabDaily.Location = new System.Drawing.Point(4, 22);
            this.tabDaily.Margin = new System.Windows.Forms.Padding(0);
            this.tabDaily.Name = "tabDaily";
            this.tabDaily.Size = new System.Drawing.Size(737, 164);
            this.tabDaily.TabIndex = 0;
            this.tabDaily.Text = "Daily Outputs";
            this.tabDaily.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoScroll = true;
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.Padding = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel4.Size = new System.Drawing.Size(737, 164);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(745, 185);
            this.tableLayoutPanel5.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 185);
            this.label4.TabIndex = 26;
            this.label4.Text = "Comments:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.runOptionItemsBindingSource1, "Comments", true));
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(65, 0);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(680, 185);
            this.textBox1.TabIndex = 21;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.AutoSize = true;
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel2.SetColumnSpan(this.tableLayoutPanel8, 2);
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.clearAllbutton, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.selectAllbutton, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 292);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(162, 29);
            this.tableLayoutPanel8.TabIndex = 25;
            // 
            // clearAllbutton
            // 
            this.clearAllbutton.Dock = System.Windows.Forms.DockStyle.Left;
            this.clearAllbutton.Location = new System.Drawing.Point(84, 3);
            this.clearAllbutton.Name = "clearAllbutton";
            this.clearAllbutton.Size = new System.Drawing.Size(75, 23);
            this.clearAllbutton.TabIndex = 25;
            this.clearAllbutton.Text = "Clear All";
            this.clearAllbutton.UseVisualStyleBackColor = true;
            this.clearAllbutton.Click += new System.EventHandler(this.clearAllbutton_Click);
            // 
            // selectAllbutton
            // 
            this.selectAllbutton.Dock = System.Windows.Forms.DockStyle.Right;
            this.selectAllbutton.Location = new System.Drawing.Point(3, 3);
            this.selectAllbutton.Name = "selectAllbutton";
            this.selectAllbutton.Size = new System.Drawing.Size(75, 23);
            this.selectAllbutton.TabIndex = 24;
            this.selectAllbutton.Text = "Select All";
            this.selectAllbutton.UseVisualStyleBackColor = true;
            this.selectAllbutton.Click += new System.EventHandler(this.selectAllbutton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 20);
            this.label1.TabIndex = 47;
            this.label1.Text = "Level of water deficit compensation (%):";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RunOptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 742);
            this.Controls.Add(this.splitContainer1);
            this.Name = "RunOptionForm";
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.runOptionItemsBindingSource1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabSummary.ResumeLayout(false);
            this.tabSummary.PerformLayout();
            this.tabDaily.ResumeLayout(false);
            this.tabDaily.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void CreateCheckBoxes()
        {
            ///<Behnam (2016.01.12)>
            ///<Comment>Now, it is possible to define the outputs in RunOptionItem class as listDaily and listSummary
            ///and use them as sorted lists to create check boxes in the Run Option form and also to rearrange the 
            ///daily and summary outputs. To rearrange, we only need to change listDaily and listSummary.
            ///The Namr referes to the name of the properties of RunOptionItem class.<Comment> 

            this.tableLayoutPanel3.RowCount = (int)Math.Ceiling((double)RunOptionItem.ListSummary.Count() / 2);
            for (var i = 0; i < tableLayoutPanel3.RowCount; ++i)
            {
                this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            }
            
            for (var i = 0; i < RunOptionItem.ListSummary.Count; ++i)
            {
                var Name = RunOptionItem.ListSummary.Keys.ElementAt(i);
                var Def = RunOptionItem.ListSummary[Name];
                double row = i / 2;
                int Row = (int)Math.Floor(row);
                int Col = i % 2;

                this.CheckBox1 = new System.Windows.Forms.CheckBox();
                this.CheckBox1.AutoSize = true;
                this.CheckBox1.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.runOptionItemsBindingSource1, Name, true));
                this.CheckBox1.Dock = System.Windows.Forms.DockStyle.Top;
                this.CheckBox1.Name = Name + "CheckBox";
                this.CheckBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
                this.CheckBox1.Size = new System.Drawing.Size(406, 17);
                this.CheckBox1.Text = Def;
                this.CheckBox1.UseVisualStyleBackColor = true;
                this.tableLayoutPanel3.Controls.Add(this.CheckBox1, Col, Row);
            }

            this.tableLayoutPanel4.RowCount = (int)Math.Ceiling((double)RunOptionItem.ListDaily.Count() / 2);
            for (var i = 0; i < tableLayoutPanel4.RowCount; ++i)
            {
                this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            } 
            
            for (var i = 0; i < RunOptionItem.ListDaily.Count; ++i)
            {
                var Name = RunOptionItem.ListDaily.Keys.ElementAt(i);
                var Def = RunOptionItem.ListDaily[Name];
                double row = i / 2;
                int Row = (int)Math.Floor(row);
                int Col = i % 2;

                this.CheckBox1 = new System.Windows.Forms.CheckBox();
                this.CheckBox1.AutoSize = true;
                this.CheckBox1.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.runOptionItemsBindingSource1, Name, true));
                this.CheckBox1.Dock = System.Windows.Forms.DockStyle.Top;
                this.CheckBox1.Name = Name + "CheckBox";
                this.CheckBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
                this.CheckBox1.Size = new System.Drawing.Size(406, 17);
                this.CheckBox1.Text = Def;
                this.CheckBox1.UseVisualStyleBackColor = true;
                this.tableLayoutPanel4.Controls.Add(this.CheckBox1, Col, Row);
            }
            ///</Behnam>
        }
        #endregion

        private RunOptionItemsBindingSource runOptionItemsBindingSource1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonDuplicate;
        private System.Windows.Forms.Button buttonSort;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Button clearAllbutton;
        private System.Windows.Forms.Button selectAllbutton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.RadioButton radioCus;
        private System.Windows.Forms.RadioButton radioV15;
        private System.Windows.Forms.RadioButton radioVMaize;
        private System.Windows.Forms.RadioButton radioV13;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.TextBox textBoxMaxTemp;
        private System.Windows.Forms.Label label147;
        private System.Windows.Forms.RadioButton radioWholeSeason;
        private System.Windows.Forms.RadioButton radioDaily;
        private System.Windows.Forms.LinkLabel linkLabelName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSummary;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TabPage tabDaily;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.CheckBox checkBoxNormT;
        private System.Windows.Forms.CheckBox checkBoxNormN;
        private System.Windows.Forms.CheckBox checkBoxNormW;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.CheckBox checkBoxWNT;
        private System.Windows.Forms.CheckBox checkBoxNT;
        private System.Windows.Forms.CheckBox checkBoxWT;
        private System.Windows.Forms.CheckBox checkBoxWN;
        private System.Windows.Forms.CheckBox checkBoxT;
        private System.Windows.Forms.CheckBox checkBoxN;
        private System.Windows.Forms.CheckBox checkBoxW;
        private System.Windows.Forms.TextBox textBoxNCompen;
        private System.Windows.Forms.TextBox textBoxWCompen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.CheckBox checkBoxGrainMaturation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxCutOnGrain;
        private System.Windows.Forms.CheckBox checkBoxAirTemperature;

        public System.Windows.Forms.CheckBox CheckBox1 { get; set; }
    }
}