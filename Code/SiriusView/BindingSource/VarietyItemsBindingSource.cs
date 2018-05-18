using System;
using System.ComponentModel;
using SiriusModel.InOut;

namespace SiriusView
{
    [Serializable()]
    public class VarietyItemsBindingSource : BaseBindingSource
    {
        public VarietyItemsBindingSource(IContainer container)
            : base(container)
        {
            AllowNew = true;
            AddingNew += new AddingNewEventHandler(VarietyItemsBindingSource_AddingNew);
            if (DesignMode)
            {
                DataSource = typeof(CropParameterItem);
            }
            else
            {
                DataSource = ProjectFile.This.FileContainer.VarietyFile.Items;
            }
        }

        void VarietyItemsBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = new CropParameterItem("new variety");
        }
    }
}
