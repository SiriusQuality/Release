using System;
using System.ComponentModel;
using SiriusModel.InOut;
using SiriusView.Base;

namespace SiriusView
{
    public partial class WarningForm : BaseCentralDockedForm
    {
        public WarningForm()
        {
            InitializeComponent();
            warningItemBindingSource1.DataSource = WarningList.This;
        }

        public override string BaseFormID()
        {
            return "Warning"; 
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            /*DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide;
            DockAreas |= WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom;*/
        }

        private void warningItemBindingSource1_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.UpdateText("Warning (" + warningItemBindingSource1.Count + ")");
            MainForm.This.UpdateWarningButtonTitle(warningItemBindingSource1.Count);
            if (e.ListChangedType == ListChangedType.ItemAdded
                || e.ListChangedType == ListChangedType.ItemDeleted
                || e.ListChangedType == ListChangedType.ItemChanged)
            {
                Activate();
            }
            else if (e.ListChangedType == ListChangedType.Reset && warningItemBindingSource1.Count > 0)
            {
                Activate();
            }
        }

        private void dataGridView1_DataError(object sender, System.Windows.Forms.DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
