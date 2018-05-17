using WeifenLuo.WinFormsUI.Docking;

namespace SiriusView.Base
{
    public partial class BaseCentralDockedForm : BaseForm
    {
        public BaseCentralDockedForm()
        {
            InitializeComponent();
        }

        public override DockPanel ReferenceDockPanel()
        {
            return (MainForm.This != null) ? MainForm.This.DockPanel : null;
        }

        private void BaseCentralDockedForm_Load(object sender, System.EventArgs e)
        {

        }
    }
}
