using System;
using System.ComponentModel;
using SiriusModel.InOut;

namespace SiriusView
{
    [Serializable()]
    public class NonVarietyItemsBindingSource : BaseBindingSource
    {
        public NonVarietyItemsBindingSource(IContainer container)
            : base(container)
        {
            AddingNew += NonVarietyItemsBindingSource_AddingNew;
            if (DesignMode)
            {
                DataSource = typeof(CropParameterItem);
            }
            else
            {
                DataSource = ProjectFile.This.FileContainer.NonVarietyFile.Items;
            }
            AllowNew = true;
        }

        void NonVarietyItemsBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = new CropParameterItem("new parameter");
        }
    }
}
