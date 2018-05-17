namespace SiriusView.File
{
    partial class ProjectDataFileForm
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
            System.Windows.Forms.Label relativeFileNameLabel;
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonNew = new System.Windows.Forms.Button();
            this.buttonSaveAs = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.relativeFileNameLinkLabel = new System.Windows.Forms.LinkLabel();
            this.projectDataFileBindingSource1 = new SiriusView.ProjectDataFileBindingSource(this.components);
            this.relativeFileNameLinkLabelMaize = new System.Windows.Forms.LinkLabel();
            this.projectDataFileBindingSource2 = new SiriusView.ProjectDataFileBindingSource(this.components);
            this.labelModified = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            relativeFileNameLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataFileBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataFileBindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // relativeFileNameLabel
            // 
            relativeFileNameLabel.AutoSize = true;
            relativeFileNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            relativeFileNameLabel.Location = new System.Drawing.Point(3, 3);
            relativeFileNameLabel.Margin = new System.Windows.Forms.Padding(3);
            relativeFileNameLabel.Name = "relativeFileNameLabel";
            relativeFileNameLabel.Size = new System.Drawing.Size(26, 23);
            relativeFileNameLabel.TabIndex = 0;
            relativeFileNameLabel.Text = "File:";
            relativeFileNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.buttonNew, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonSaveAs, 6, 0);
            this.tableLayoutPanel1.Controls.Add(relativeFileNameLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonSave, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLinkLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.relativeFileNameLinkLabelMaize, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelModified, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(517, 29);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonNew
            // 
            this.buttonNew.AutoSize = true;
            this.buttonNew.Location = new System.Drawing.Point(475, 3);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(39, 23);
            this.buttonNew.TabIndex = 2;
            this.buttonNew.Text = "New";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // buttonSaveAs
            // 
            this.buttonSaveAs.AutoSize = true;
            this.buttonSaveAs.Location = new System.Drawing.Point(413, 3);
            this.buttonSaveAs.Name = "buttonSaveAs";
            this.buttonSaveAs.Size = new System.Drawing.Size(56, 23);
            this.buttonSaveAs.TabIndex = 2;
            this.buttonSaveAs.Text = "Save as";
            this.buttonSaveAs.UseVisualStyleBackColor = true;
            this.buttonSaveAs.Click += new System.EventHandler(this.buttonSaveAs_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.AutoSize = true;
            this.buttonSave.Location = new System.Drawing.Point(365, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(42, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // relativeFileNameLinkLabel
            // 
            this.relativeFileNameLinkLabel.AutoSize = true;
            this.relativeFileNameLinkLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectDataFileBindingSource1, "RelativeFileName", true));
            this.relativeFileNameLinkLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relativeFileNameLinkLabel.Location = new System.Drawing.Point(35, 3);
            this.relativeFileNameLinkLabel.Margin = new System.Windows.Forms.Padding(3);
            this.relativeFileNameLinkLabel.Name = "relativeFileNameLinkLabel";
            this.relativeFileNameLinkLabel.Size = new System.Drawing.Size(81, 23);
            this.relativeFileNameLinkLabel.TabIndex = 1;
            this.relativeFileNameLinkLabel.TabStop = true;
            this.relativeFileNameLinkLabel.Text = "linkLabelWheat";
            this.relativeFileNameLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.relativeFileNameLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.relativeFileNameLinkLabel_LinkClicked);
            // 
            // projectDataFileBindingSource1
            // 
            this.projectDataFileBindingSource1.DataSource = typeof(SiriusModel.InOut.Base.IProjectDataFile);
            this.projectDataFileBindingSource1.CurrentItemChanged += new System.EventHandler(this.projectDataFileBindingSource1_CurrentItemChanged);
            // 
            // relativeFileNameLinkLabelMaize
            // 
            this.relativeFileNameLinkLabelMaize.AutoSize = true;
            this.relativeFileNameLinkLabelMaize.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.projectDataFileBindingSource2, "RelativeFileName", true));
            this.relativeFileNameLinkLabelMaize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relativeFileNameLinkLabelMaize.Location = new System.Drawing.Point(182, 3);
            this.relativeFileNameLinkLabelMaize.Margin = new System.Windows.Forms.Padding(3);
            this.relativeFileNameLinkLabelMaize.Name = "relativeFileNameLinkLabelMaize";
            this.relativeFileNameLinkLabelMaize.Size = new System.Drawing.Size(77, 23);
            this.relativeFileNameLinkLabelMaize.TabIndex = 1;
            this.relativeFileNameLinkLabelMaize.TabStop = true;
            this.relativeFileNameLinkLabelMaize.Text = "linkLabelMaize";
            this.relativeFileNameLinkLabelMaize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.relativeFileNameLinkLabelMaize.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.relativeFileNameLinkLabelMaize_LinkClicked);
            // 
            // projectDataFileBindingSource2
            // 
            this.projectDataFileBindingSource2.DataSource = typeof(SiriusModel.InOut.Base.IProjectDataFile);
            this.projectDataFileBindingSource2.CurrentItemChanged += new System.EventHandler(this.projectDataFileBindingSource2_CurrentItemChanged);
            // 
            // labelModified
            // 
            this.labelModified.AutoSize = true;
            this.labelModified.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelModified.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelModified.ForeColor = System.Drawing.Color.Blue;
            this.labelModified.Location = new System.Drawing.Point(265, 0);
            this.labelModified.Name = "labelModified";
            this.labelModified.Size = new System.Drawing.Size(94, 29);
            this.labelModified.TabIndex = 3;
            this.labelModified.Text = "*";
            this.labelModified.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(122, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 29);
            this.label1.TabIndex = 4;
            this.label1.Text = "Maize file:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ProjectDataFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(517, 404);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProjectDataFileForm";
            this.TabText = "InputForm";
            this.Text = "InputForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataFileBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataFileBindingSource2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.LinkLabel relativeFileNameLinkLabel;
        private System.Windows.Forms.LinkLabel relativeFileNameLinkLabelMaize;
        private System.Windows.Forms.Button buttonSaveAs;
        private System.Windows.Forms.Button buttonSave;
        protected System.Windows.Forms.OpenFileDialog openFileDialog1;
        protected System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private ProjectDataFileBindingSource projectDataFileBindingSource1;
        private ProjectDataFileBindingSource projectDataFileBindingSource2;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Label labelModified;
        private System.Windows.Forms.Label label1;

    }
}