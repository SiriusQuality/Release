using System;
using System.ComponentModel;
using SiriusModel.InOut;

namespace SiriusView
{
    [Serializable()]
    public class ManagementItemsBindingSource : BaseBindingSource
    {
        public ManagementItemsBindingSource(IContainer container)
            : base(container)
        {
            if (DesignMode)
            {
                DataSource = typeof(ManagementItem);
            }
            else
            {
                DataSource = ProjectFile.This.FileContainer.ManagementFile.Items;
            }
        }
    }
}
