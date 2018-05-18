using System;
using System.ComponentModel;
using SiriusModel.InOut;

namespace SiriusView
{
    [Serializable()]
    public class OptimizationItemsBindingSource : BaseBindingSource
    {
        public OptimizationItemsBindingSource(IContainer container)
            : base(container)
        {
            AddingNew += OptimizationItemsBindingSource_AddingNew;
            if (DesignMode)
            {
                DataSource = typeof(OptimizationItem);
            }
            else
            {
                DataSource = ProjectFile.This.FileContainer.OptimizationFile.Items;
            }
            AllowNew = true;
        }

        void OptimizationItemsBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = new OptimizationItem("new");
        }
    }
}
