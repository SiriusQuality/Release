using System;
using System.Windows.Forms;
using SiriusModel.InOut;

namespace SiriusView
{
    public partial class OutputPatternForm : Form
    {
        public OutputPatternForm()
        {
            InitializeComponent();
        }

        public string Pattern
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public bool IsNormalEdit
        {
            set
            {
                buttonDeltaSensitivity_.Enabled = !value;
                buttonMultiYearSowingYear.Enabled = !value;
            }
        }

        public bool IsMultiSummaryEdit
        {
            set
            {
                buttonDeltaSensitivity_.Enabled = !value;
                buttonMultiYearSowingYear.Enabled = !value;
            }
        }

        public bool IsMultiDailyEdit
        {
            set
            {
                buttonMultiYearSowingYear.Enabled = value;
                buttonDeltaSensitivity_.Enabled = !value;
            }
        }

        public bool IsSensitivitySummaryEdit
        {
            set
            {
                buttonDeltaSensitivity_.Enabled = !value;
                buttonMultiYearSowingYear.Enabled = !value;
            }
        }

        public bool IsSensitivityDailyEdit
        {
            set
            {
                buttonDeltaSensitivity_.Enabled = value;
                buttonMultiYearSowingYear.Enabled = value;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button_vVer__Click(object sender, EventArgs e)
        {
            textBox1.Paste(OutputPatternVarDef.VarVVer);
        }

        private void button_ManagementItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste(OutputPatternVarDef.VarManagementItem);
        }

        private void buttonParameterItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste(OutputPatternVarDef.VarParameterItem);
        }

        private void buttonRunItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste(OutputPatternVarDef.VarRunItem);
        }

        private void buttonRunOptionItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste(OutputPatternVarDef.VarRunOptionItem);
        }

        private void buttonSiteItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste(OutputPatternVarDef.VarSiteItem);
        }

        private void buttonSoilItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste(OutputPatternVarDef.VarSoilItem);
        }

        private void buttonVarietyItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste(OutputPatternVarDef.VarVarietyItem);
        }

        private void buttonMultiYearSowingYear_Click(object sender, EventArgs e)
        {
            textBox1.Paste(RunItemModeMulti.VarMultiYearSowingYear);
        }

        private void buttonDeltaSensitivity__Click(object sender, EventArgs e)
        {
            textBox1.Paste(RunItemModeSensitivity.VarDeltaSensitivity);
        }

        private void buttonExperiment_Click(object sender, EventArgs e)
        {
            textBox1.Paste(OutputPatternVarDef.VarExperimentItem);
        }
    }
}
