using System.Linq;
using SiriusModel.InOut.Base;
using System;

namespace SiriusModel.InOut
{
    public class WarningList : BaseBindingList<WarningItem>
    {
        #region singleton design

        private static readonly WarningList Instance = new WarningList();
        public static WarningList This { get { return Instance; } }

        #endregion 
        
        private WarningList()
        {
            AllowEdit = false;
            AllowNew = false;
        }

        public void Clear(IProjectItem item)
        {
            var itemToRemove = this.FirstOrDefault(wi => ReferenceEquals(wi.ProjectItem, item));
            while (itemToRemove != null)
            {
                Remove(itemToRemove);
                itemToRemove = this.FirstOrDefault(wi => ReferenceEquals(wi.ProjectItem, item));
            }
        }

        public void NotifyPropertyChanged(IProjectItem item, string info)
        {
            if (info == "Name" || info == "Parent")
            {
                foreach (var wi in this)
                {
                    if (ReferenceEquals(wi.ProjectItem, item))
                    {
                        wi.NotifyPropertyChanged("FileID");
                        wi.NotifyPropertyChanged("ItemName");
                    }
                }
            }
        }

        public void CheckStatus(IProjectItem item, string propertyName, string format, object[] arguments, bool warningIfFalse)
        {
            if (warningIfFalse)
            {
                var itemToRemove = this.FirstOrDefault(wi => ReferenceEquals(wi.ProjectItem, item) && wi.PropertyName == propertyName && wi.Format == format);
                if (itemToRemove != null) Remove(itemToRemove);
            }
            else
            {
                var itemToAdd = this.FirstOrDefault(wi => ReferenceEquals(wi.ProjectItem, item) && wi.PropertyName == propertyName && wi.Format == format);
                if (itemToAdd == null)
                {
                    itemToAdd = new WarningItem(item, propertyName, format);
                    itemToAdd.Arguments = arguments;
                    Add(itemToAdd);
                }
                else itemToAdd.Arguments = arguments;
            }
        }
    }
}
