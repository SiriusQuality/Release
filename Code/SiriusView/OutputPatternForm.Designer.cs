namespace SiriusView
{
    partial class OutputPatternForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.button_vVer_ = new System.Windows.Forms.Button();
            this.button_ManagementItem = new System.Windows.Forms.Button();
            this.buttonParameterItem = new System.Windows.Forms.Button();
            this.buttonRunItem = new System.Windows.Forms.Button();
            this.buttonRunOptionItem = new System.Windows.Forms.Button();
            this.buttonSiteItem = new System.Windows.Forms.Button();
            this.buttonSoilItem = new System.Windows.Forms.Button();
            this.buttonVarietyItem = new System.Windows.Forms.Button();
            this.buttonMultiYearSowingYear = new System.Windows.Forms.Button();
            this.buttonExperiment = new System.Windows.Forms.Button();
            this.buttonDeltaSensitivity_ = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.buttonCancel);
            this.flowLayoutPanel1.Controls.Add(this.buttonOk);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 96);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(814, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // buttonCancel
            // 
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.Location = new System.Drawing.Point(761, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(50, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.AutoSize = true;
            this.buttonOk.Location = new System.Drawing.Point(724, 3);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(31, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.button_vVer_);
            this.flowLayoutPanel2.Controls.Add(this.button_ManagementItem);
            this.flowLayoutPanel2.Controls.Add(this.buttonParameterItem);
            this.flowLayoutPanel2.Controls.Add(this.buttonRunItem);
            this.flowLayoutPanel2.Controls.Add(this.buttonRunOptionItem);
            this.flowLayoutPanel2.Controls.Add(this.buttonSiteItem);
            this.flowLayoutPanel2.Controls.Add(this.buttonSoilItem);
            this.flowLayoutPanel2.Controls.Add(this.buttonVarietyItem);
            this.flowLayoutPanel2.Controls.Add(this.buttonMultiYearSowingYear);
            this.flowLayoutPanel2.Controls.Add(this.buttonExperiment);
            this.flowLayoutPanel2.Controls.Add(this.buttonDeltaSensitivity_);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 20);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(814, 58);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // button_vVer_
            // 
            this.button_vVer_.AutoSize = true;
            this.button_vVer_.Location = new System.Drawing.Point(3, 3);
            this.button_vVer_.Name = "button_vVer_";
            this.button_vVer_.Size = new System.Drawing.Size(57, 23);
            this.button_vVer_.TabIndex = 0;
            this.button_vVer_.Text = "$(vVer_)";
            this.button_vVer_.UseVisualStyleBackColor = true;
            this.button_vVer_.Click += new System.EventHandler(this.button_vVer__Click);
            // 
            // button_ManagementItem
            // 
            this.button_ManagementItem.AutoSize = true;
            this.button_ManagementItem.Location = new System.Drawing.Point(66, 3);
            this.button_ManagementItem.Name = "button_ManagementItem";
            this.button_ManagementItem.Size = new System.Drawing.Size(119, 23);
            this.button_ManagementItem.TabIndex = 1;
            this.button_ManagementItem.Text = "$(ManagementName)";
            this.button_ManagementItem.UseVisualStyleBackColor = true;
            this.button_ManagementItem.Click += new System.EventHandler(this.button_ManagementItem_Click);
            // 
            // buttonParameterItem
            // 
            this.buttonParameterItem.AutoSize = true;
            this.buttonParameterItem.Location = new System.Drawing.Point(191, 3);
            this.buttonParameterItem.Name = "buttonParameterItem";
            this.buttonParameterItem.Size = new System.Drawing.Size(165, 23);
            this.buttonParameterItem.TabIndex = 2;
            this.buttonParameterItem.Text = "$(NonVarietalParametersName)";
            this.buttonParameterItem.UseVisualStyleBackColor = true;
            this.buttonParameterItem.Click += new System.EventHandler(this.buttonParameterItem_Click);
            // 
            // buttonRunItem
            // 
            this.buttonRunItem.AutoSize = true;
            this.buttonRunItem.Location = new System.Drawing.Point(362, 3);
            this.buttonRunItem.Name = "buttonRunItem";
            this.buttonRunItem.Size = new System.Drawing.Size(77, 23);
            this.buttonRunItem.TabIndex = 3;
            this.buttonRunItem.Text = "$(RunName)";
            this.buttonRunItem.UseVisualStyleBackColor = true;
            this.buttonRunItem.Click += new System.EventHandler(this.buttonRunItem_Click);
            // 
            // buttonRunOptionItem
            // 
            this.buttonRunOptionItem.AutoSize = true;
            this.buttonRunOptionItem.Location = new System.Drawing.Point(445, 3);
            this.buttonRunOptionItem.Name = "buttonRunOptionItem";
            this.buttonRunOptionItem.Size = new System.Drawing.Size(108, 23);
            this.buttonRunOptionItem.TabIndex = 4;
            this.buttonRunOptionItem.Text = "$(RunOptionName)";
            this.buttonRunOptionItem.UseVisualStyleBackColor = true;
            this.buttonRunOptionItem.Click += new System.EventHandler(this.buttonRunOptionItem_Click);
            // 
            // buttonSiteItem
            // 
            this.buttonSiteItem.AutoSize = true;
            this.buttonSiteItem.Location = new System.Drawing.Point(559, 3);
            this.buttonSiteItem.Name = "buttonSiteItem";
            this.buttonSiteItem.Size = new System.Drawing.Size(75, 23);
            this.buttonSiteItem.TabIndex = 5;
            this.buttonSiteItem.Text = "$(SiteName)";
            this.buttonSiteItem.UseVisualStyleBackColor = true;
            this.buttonSiteItem.Click += new System.EventHandler(this.buttonSiteItem_Click);
            // 
            // buttonSoilItem
            // 
            this.buttonSoilItem.AutoSize = true;
            this.buttonSoilItem.Location = new System.Drawing.Point(640, 3);
            this.buttonSoilItem.Name = "buttonSoilItem";
            this.buttonSoilItem.Size = new System.Drawing.Size(74, 23);
            this.buttonSoilItem.TabIndex = 6;
            this.buttonSoilItem.Text = "$(SoilName)";
            this.buttonSoilItem.UseVisualStyleBackColor = true;
            this.buttonSoilItem.Click += new System.EventHandler(this.buttonSoilItem_Click);
            // 
            // buttonVarietyItem
            // 
            this.buttonVarietyItem.AutoSize = true;
            this.flowLayoutPanel2.SetFlowBreak(this.buttonVarietyItem, true);
            this.buttonVarietyItem.Location = new System.Drawing.Point(720, 3);
            this.buttonVarietyItem.Name = "buttonVarietyItem";
            this.buttonVarietyItem.Size = new System.Drawing.Size(89, 23);
            this.buttonVarietyItem.TabIndex = 7;
            this.buttonVarietyItem.Text = "$(VarietyName)";
            this.buttonVarietyItem.UseVisualStyleBackColor = true;
            this.buttonVarietyItem.Click += new System.EventHandler(this.buttonVarietyItem_Click);
            // 
            // buttonMultiYearSowingYear
            // 
            this.buttonMultiYearSowingYear.AutoSize = true;
            this.buttonMultiYearSowingYear.Location = new System.Drawing.Point(3, 32);
            this.buttonMultiYearSowingYear.Name = "buttonMultiYearSowingYear";
            this.buttonMultiYearSowingYear.Size = new System.Drawing.Size(136, 23);
            this.buttonMultiYearSowingYear.TabIndex = 8;
            this.buttonMultiYearSowingYear.Text = "$(MultiYear?SowingYear)";
            this.buttonMultiYearSowingYear.UseVisualStyleBackColor = true;
            this.buttonMultiYearSowingYear.Click += new System.EventHandler(this.buttonMultiYearSowingYear_Click);
            // 
            // buttonExperiment
            // 
            this.buttonExperiment.AutoSize = true;
            this.buttonExperiment.Location = new System.Drawing.Point(3, 32);
            this.buttonExperiment.Name = "buttonExperiment";
            this.buttonExperiment.Size = new System.Drawing.Size(89, 23);
            this.buttonExperiment.TabIndex = 9;
            this.buttonExperiment.Text = "$(Experiment)";
            this.buttonExperiment.UseVisualStyleBackColor = true;
            this.buttonExperiment.Click += new System.EventHandler(this.buttonExperiment_Click);
            // 
            // buttonDeltaSensitivity_
            // 
            this.buttonDeltaSensitivity_.AutoSize = true;
            this.buttonDeltaSensitivity_.Location = new System.Drawing.Point(145, 32);
            this.buttonDeltaSensitivity_.Name = "buttonDeltaSensitivity_";
            this.buttonDeltaSensitivity_.Size = new System.Drawing.Size(107, 23);
            this.buttonDeltaSensitivity_.TabIndex = 10;
            this.buttonDeltaSensitivity_.Text = "$(DeltaSensitivity_)";
            this.buttonDeltaSensitivity_.UseVisualStyleBackColor = true;
            this.buttonDeltaSensitivity_.Click += new System.EventHandler(this.buttonDeltaSensitivity__Click);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(814, 20);
            this.textBox1.TabIndex = 2;
            // 
            // OutputPatternForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(814, 125);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.textBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OutputPatternForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button button_vVer_;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_ManagementItem;
        private System.Windows.Forms.Button buttonParameterItem;
        private System.Windows.Forms.Button buttonRunItem;
        private System.Windows.Forms.Button buttonRunOptionItem;
        private System.Windows.Forms.Button buttonSiteItem;
        private System.Windows.Forms.Button buttonSoilItem;
        private System.Windows.Forms.Button buttonVarietyItem;
        private System.Windows.Forms.Button buttonMultiYearSowingYear;
        private System.Windows.Forms.Button buttonExperiment;
        private System.Windows.Forms.Button buttonDeltaSensitivity_;
    }
}