namespace SiriusView
{
    partial class CropParameterSetting
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
            SiriusView.Parameters parameters3 = new SiriusView.Parameters();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CropParameterSetting));
            this.parameterSelector1 = new SiriusView.ParameterSelector();
            this.varietyItemsBindingSource1 = new SiriusView.VarietyItemsBindingSource(this.components);
            this.nonVarietyItemsBindingSource1 = new SiriusView.NonVarietyItemsBindingSource(this.components);
            this.applyButton = new System.Windows.Forms.Button();
            this.nonVarietalLabel = new System.Windows.Forms.Label();
            this.varietalLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.varietyItemsBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nonVarietyItemsBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // parameterSelector1
            // 
            this.parameterSelector1.GrouName = null;
            this.parameterSelector1.Location = new System.Drawing.Point(12, 12);
            this.parameterSelector1.Name = "parameterSelector1";
            parameters3.NotSelected = ((System.ComponentModel.BindingList<string>)(resources.GetObject("parameters3.NotSelected")));
            parameters3.Selected = ((System.ComponentModel.BindingList<string>)(resources.GetObject("parameters3.Selected")));
            this.parameterSelector1.Parameters = parameters3;
            this.parameterSelector1.Size = new System.Drawing.Size(422, 212);
            this.parameterSelector1.TabIndex = 0;
            // 
            // varietyItemsBindingSource1
            // 
            this.varietyItemsBindingSource1.AllowNew = true;
            this.varietyItemsBindingSource1.DataSource = typeof(SiriusModel.InOut.CropParameterItem);
            // 
            // nonVarietyItemsBindingSource1
            // 
            this.nonVarietyItemsBindingSource1.AllowNew = true;
            this.nonVarietyItemsBindingSource1.DataSource = typeof(SiriusModel.InOut.CropParameterItem);
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(154, 230);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(135, 23);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "Apply changes";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // nonVarietalLabel
            // 
            this.nonVarietalLabel.AutoSize = true;
            this.nonVarietalLabel.Location = new System.Drawing.Point(35, 12);
            this.nonVarietalLabel.Name = "nonVarietalLabel";
            this.nonVarietalLabel.Size = new System.Drawing.Size(120, 13);
            this.nonVarietalLabel.TabIndex = 2;
            this.nonVarietalLabel.Text = "Non-Varietal parameters";
            // 
            // varietalLabel
            // 
            this.varietalLabel.AutoSize = true;
            this.varietalLabel.Location = new System.Drawing.Point(298, 12);
            this.varietalLabel.Name = "varietalLabel";
            this.varietalLabel.Size = new System.Drawing.Size(97, 13);
            this.varietalLabel.TabIndex = 3;
            this.varietalLabel.Text = "Varietal parameters";
            // 
            // CropParameterSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 270);
            this.Controls.Add(this.varietalLabel);
            this.Controls.Add(this.nonVarietalLabel);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.parameterSelector1);
            this.Name = "CropParameterSetting";
            this.Text = "Crop Parameters Setting";
            ((System.ComponentModel.ISupportInitialize)(this.varietyItemsBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nonVarietyItemsBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VarietyItemsBindingSource varietyItemsBindingSource1;
        private NonVarietyItemsBindingSource nonVarietyItemsBindingSource1;
        private ParameterSelector parameterSelector1;

        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Label nonVarietalLabel;
        private System.Windows.Forms.Label varietalLabel;
    }
}