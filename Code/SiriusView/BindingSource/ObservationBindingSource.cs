using System;
using System.ComponentModel;
using SiriusModel.InOut;

namespace SiriusView
{
    [Serializable()]
    public class ObservationItemsBindingSource : BaseBindingSource
    {
        public ObservationItemsBindingSource(IContainer container)
            : base(container)
        {
            AddingNew += ObservationItemsBindingSource_AddingNew;
            if (DesignMode)
            {
                DataSource = typeof(ObservationItem);
            }
            else
            {
                DataSource = ProjectFile.This.FileContainer.ObservationFile.Items;
            }
            AllowNew = true;
        }

        void ObservationItemsBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = new ObservationItem("new");
        }
    }
}
