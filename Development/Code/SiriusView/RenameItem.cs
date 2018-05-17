using System;
using System.Windows.Forms;
using SiriusModel.InOut;
using SiriusModel.InOut.Base;

namespace SiriusView
{
    public partial class RenameItem : Form
    {
        private IProjectDataFileItem item;

        public RenameItem()
        {
            InitializeComponent();
        }

        public IProjectDataFileItem Item
        {
            get { return item; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                item = value;
                labelTitle.Text = String.Format("Rename {0} to :", item.Name);
                textBox1.Text = item.Name;
            }
        }

        public string NewName { get; private set; }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            NewName = textBox1.Text;
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == item.Name)
            {
                labelError.UpdateText("Please enter a different name.");
                buttonOK.UpdateEnabled(false);
            }
            else if (String.IsNullOrEmpty(textBox1.Text))
            {
                labelError.UpdateText("Please tip a name.");
                buttonOK.UpdateEnabled(false);
            }
            else
            {
                var file = item.ProjectDataFileParent;
                if (file != null)
                {
                    if (file.Contains(textBox1.Text))
                    {
                        labelError.UpdateText("This name is already used, please tip an other.");
                        buttonOK.UpdateEnabled(false);
                    }
                    else
                    {
                        labelError.UpdateText("");
                        buttonOK.UpdateEnabled(true);
                    }
                }
            }
        }

        public void Apply()
        {
            item.Name = NewName;
        }

        public delegate void SetNormalField(RunItemModeNormal n, string value);

        public delegate void SetMultiField(MultiRunItem n, string value);

        public void Apply(Func<RunItemModeNormal, string> getNormalField, SetNormalField setNormalField,
            Func<MultiRunItem, string> getMultiField, SetMultiField setMultiField)
        {
            var oldName = item.Name;
            Apply();
            foreach (var runItem in ProjectFile.This.FileContainer.RunFile.Items)
            {
                if (getNormalField(runItem.Normal) == oldName) setNormalField(runItem.Normal, NewName);
                foreach (var multiItem in runItem.Multi.MultiRuns)
                {
                    if (getMultiField(multiItem) == oldName) setMultiField(multiItem, NewName);
                }
            }
        }
    }
}
