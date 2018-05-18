using System;
using System.ComponentModel;
using SiriusModel.InOut;

namespace SiriusView
{
    [Serializable()]
    public class RunOptionItemsBindingSource : BaseBindingSource
    {
        public RunOptionItemsBindingSource(IContainer container)
            : base(container)
        {
            if (DesignMode)
            {
                DataSource = typeof(RunOptionItem);
            }
            else
            {
                DataSource = ProjectFile.This.FileContainer.RunOptionFile.Items;
            }
        }
    }
}
