using System;
using System.ComponentModel;
using SiriusModel.InOut;

namespace SiriusView
{
    [Serializable()]
    public class SoilItemsBindingSource : BaseBindingSource
    {
        public SoilItemsBindingSource(IContainer container)
            : base(container)
        {
            if (DesignMode)
            {
                DataSource = typeof(SoilItem);
            }
            else
            {
                DataSource = ProjectFile.This.FileContainer.SoilFile.Items;
            }
        }
    }
}
