using System.ComponentModel;
using SiriusModel.InOut;

namespace SiriusView
{
    public class WarningItemBindingSource : BaseBindingSource
    {
        public WarningItemBindingSource(IContainer container)
            : base(container)
        {
            if (DesignMode)
            {
                DataSource = typeof(WarningItem);
            }
            else
            {
                DataSource = WarningList.This;
            }
        }
    }
}
