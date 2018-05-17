using System;
using System.ComponentModel;
using SiriusModel.InOut;

namespace SiriusView
{
    [Serializable()]
    public class SiteItemsBindingSource : BaseBindingSource
    {
        public SiteItemsBindingSource(IContainer container)
            : base(container)
        {
            if (DesignMode)
            {
                DataSource = typeof(SiteItem);
            }
            else
            {
                DataSource = ProjectFile.This.FileContainer.SiteFile.Items;
            }
        }
    }
}
