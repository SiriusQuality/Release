namespace SiriusView
{
    partial class CropFileTranslator
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
            this.ProcessButton = new System.Windows.Forms.Button();
            this.inputFileLink = new System.Windows.Forms.LinkLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.infoTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ProcessButton
            // 
            this.ProcessButton.Location = new System.Drawing.Point(277, 81);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(121, 23);
            this.ProcessButton.TabIndex = 0;
            this.ProcessButton.Text = "Process";
            this.ProcessButton.UseVisualStyleBackColor = true;
            this.ProcessButton.Click += new System.EventHandler(this.ProcessButton_Click);
            // 
            // inputFileLink
            // 
            this.inputFileLink.AutoSize = true;
            this.inputFileLink.Location = new System.Drawing.Point(12, 35);
            this.inputFileLink.Name = "inputFileLink";
            this.inputFileLink.Size = new System.Drawing.Size(43, 13);
            this.inputFileLink.TabIndex = 1;
            this.inputFileLink.TabStop = true;
            this.inputFileLink.Text = "load file";
            this.inputFileLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.inputFileLink_LinkClicked);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please select a sqvar or sqpar file to translate";
            // 
            // infoTextBox
            // 
            this.infoTextBox.Location = new System.Drawing.Point(12, 62);
            this.infoTextBox.Name = "infoTextBox";
            this.infoTextBox.Size = new System.Drawing.Size(256, 42);
            this.infoTextBox.TabIndex = 4;
            this.infoTextBox.Text = "";
            // 
            // CropFileTranslator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 116);
            this.Controls.Add(this.infoTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputFileLink);
            this.Controls.Add(this.ProcessButton);
            this.Name = "CropFileTranslator";
            this.Text = "CropFileTranslator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ProcessButton;
        private System.Windows.Forms.LinkLabel inputFileLink;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox infoTextBox;
    }
}