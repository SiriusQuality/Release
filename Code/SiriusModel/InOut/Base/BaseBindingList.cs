using System.ComponentModel;
using System.Reflection;

namespace SiriusModel.InOut.Base
{
    public class BaseBindingList<TChildType> : BindingList<TChildType>
    {
        public BaseBindingList()
        {
            RaiseListChangedEvents = true;
            AllowEdit = true;
            AllowNew = true;
            AllowRemove = true;
        }

        public new void OnListChanged(ListChangedEventArgs e)
        {
            base.OnListChanged(e);
        }

        protected override void RemoveItem(int index)
        {
            var projectItem = this[index] as IProjectItem;
            var warningItem = projectItem as WarningItem;
            if (projectItem != null && warningItem == null)
            {
                WarningList.This.Clear(projectItem);
                var child = projectItem as IChild;
                if (child != null)
                {
                    child.Parent = null;
                }
            }
            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            foreach (var item in this)
            {
                var projectItem = item as IProjectItem;
                var warningItem = projectItem as WarningItem;
                if (projectItem == null || warningItem != null) continue;

                WarningList.This.Clear(projectItem);
                var child = projectItem as IChild;
                if (child != null)
                {
                    child.Parent = null;
                }
            }
            base.ClearItems();
        }

        public BindingList<string> FilterPossibleStringValues(string filteredProperty)
        {
            BindingList<string> results = new BindingList<string>();

            if (filteredProperty != null)
            {
            PropertyDescriptor propDesc = TypeDescriptor.GetProperties(typeof(TChildType))[filteredProperty];

                if (propDesc != null)
                {
                    // Get the property info for the specified property.

                    PropertyInfo propInfo = typeof(TChildType).GetProperty(propDesc.Name);

                    TChildType item;

                    // Loop through the items to find all the possibles values of the property
                    for (int i = 0; i < Count; ++i)
                    {
                        item = (TChildType)Items[i];

                        if (!results.Contains((string)propInfo.GetValue(item, null)))
                        {
                            results.Add((string)propInfo.GetValue(item, null));
                        }
                    }

                 }
            }
            return results;
        }    }
}
