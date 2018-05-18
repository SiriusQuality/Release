using System;
using System.ComponentModel;
using SiriusModel.InOut;

namespace SiriusView
{
    [Serializable()]
    public class RunItemsBindingSource : BaseBindingSource
    {
        public RunItemsBindingSource(IContainer container)
            : base(container)
        {
            if (DesignMode)
            {
                DataSource = typeof(RunItem);
            }
            else
            {
                DataSource = ProjectFile.This.FileContainer.RunFile.Items;
            }
        }
    }
}
