namespace SiriusView
{
    partial class ParameterSelector
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

        #region Component Designer generated code

        ///<summary> 
        ///Required method for Designer support - do not modify 
        ///the contents of this method with the code editor.
        ///</summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.notSelected = new System.Windows.Forms.ListBox();
            this.selected = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.select = new System.Windows.Forms.Button();
            this.deselect = new System.Windows.Forms.Button();
            this.groupName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupName, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(422, 212);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(416, 193);
            this.panel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.notSelected, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.selected, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(416, 193);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // notSelected
            // 
            this.notSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notSelected.FormattingEnabled = true;
            this.notSelected.Location = new System.Drawing.Point(3, 3);
            this.notSelected.Name = "notSelected";
            this.notSelected.Size = new System.Drawing.Size(158, 187);
            this.notSelected.Sorted = true;
            this.notSelected.TabIndex = 0;
            this.notSelected.SelectedIndexChanged += new System.EventHandler(this.notSelected_SelectedIndexChanged);
            // 
            // selected
            // 
            this.selected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selected.FormattingEnabled = true;
            this.selected.Location = new System.Drawing.Point(254, 3);
            this.selected.Name = "selected";
            this.selected.Size = new System.Drawing.Size(159, 187);
            this.selected.Sorted = true;
            this.selected.TabIndex = 1;
            this.selected.SelectedIndexChanged += new System.EventHandler(this.selected_SelectedIndexChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.select, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.deselect, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(167, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(81, 187);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // select
            // 
            this.select.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.select.Location = new System.Drawing.Point(3, 67);
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(75, 23);
            this.select.TabIndex = 0;
            this.select.Text = ">";
            this.select.UseVisualStyleBackColor = true;
            this.select.Click += new System.EventHandler(this.select_Click);
            // 
            // deselect
            // 
            this.deselect.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.deselect.Location = new System.Drawing.Point(3, 96);
            this.deselect.Name = "deselect";
            this.deselect.Size = new System.Drawing.Size(75, 23);
            this.deselect.TabIndex = 1;
            this.deselect.Text = "<";
            this.deselect.UseVisualStyleBackColor = true;
            this.deselect.Click += new System.EventHandler(this.deselect_Click);
            // 
            // groupName
            // 
            this.groupName.AutoSize = true;
            this.groupName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupName.Location = new System.Drawing.Point(3, 0);
            this.groupName.Name = "groupName";
            this.groupName.Size = new System.Drawing.Size(416, 13);
            this.groupName.TabIndex = 1;
            this.groupName.Text = "label1";
            // 
            // ParameterSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ParameterSelector";
            this.Size = new System.Drawing.Size(422, 212);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListBox notSelected;
        private System.Windows.Forms.ListBox selected;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button select;
        private System.Windows.Forms.Button deselect;
        private System.Windows.Forms.Label groupName;
        private System.Windows.Forms.Panel panel1;
    }
}
