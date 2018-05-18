using System;
using System.ComponentModel;
using System.Linq;

namespace SiriusModel.InOut.Base
{
    public interface IChildKeyGenerator<TChildType, TChildKey>
        where TChildKey : IComparable
        where TChildType : IChild<TChildKey>, new()
    {
        bool Sorted { get; }
        bool Selectable { get; }
        bool NullSelectable { get; }
        string KeyPropertyName { get; }
        Func<TChildType, TChildKey> KeySelector { get; }
        Func<TChildType, TChildKey, TChildKey> KeySetter { get; }
        void Sort(BaseBindingList<TChildType> bindingList, BaseBindingList<TChildType> selectable);
        void CreateNullSelectable(BaseBindingList<TChildType> selectable);
        TChildType Find(BaseBindingList<TChildType> bindingList, TChildKey key);
        void UpdateSelectable(BaseBindingList<TChildType> bindingList, BaseBindingList<TChildType> selectable);
        TChildType[] Write(BaseBindingList<TChildType> bindingList);
        void Read(BaseBindingList<TChildType> bindingList, BaseBindingList<TChildType> selectable, TChildType[] array, object parent);
        void CheckKeys(BaseBindingList<TChildType> bindingList);
    }

    [Serializable]
    public abstract class ChildKeyGeneratorSorted<TChildType, TChildKey> : IChildKeyGenerator<TChildType, TChildKey>
        where TChildKey : IComparable
        where TChildType : IChild<TChildKey>, new()
    {
        #region IChildKeyGenerator<TItemType,string> Members

        public abstract bool Sorted { get;  }

        public abstract bool Selectable { get; }

        public abstract bool NullSelectable { get; }

        public abstract string KeyPropertyName { get; }

        public abstract Func<TChildType, TChildKey> KeySelector { get; }
        public abstract Func<TChildType, TChildKey, TChildKey> KeySetter { get; }

        public void Sort(BaseBindingList<TChildType> bindingList, BaseBindingList<TChildType> selectable)
        {
            if (bindingList.Count <= 1) return;
            var sortedItems = bindingList.OrderBy(KeySelector).ToArray();
            var nbItem = bindingList.Count;

            for (var i = 0; i < nbItem; ++i)
            {
                var oldIndex = bindingList.IndexOf(sortedItems[i]);
                if (oldIndex == i) continue;

                var temp = bindingList[i];
                bindingList[i] = sortedItems[i];
                bindingList[oldIndex] = temp;
                bindingList.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemMoved, i, oldIndex));
            }
            if (Selectable) UpdateSelectable(bindingList, selectable);
        }

        public abstract void CreateNullSelectable(BaseBindingList<TChildType> selectable);

        public TChildType Find(BaseBindingList<TChildType> bindingList, TChildKey key)
        {
            return bindingList.FirstOrDefault(child => KeySelector.Invoke(child).CompareTo(key) == 0);
        }

        public void UpdateSelectable(BaseBindingList<TChildType> bindingList, BaseBindingList<TChildType> selectable)
        {
            var nbOldSelectable = selectable.Count;
            var nbNewSelectable = bindingList.Count + ((NullSelectable) ? 1 : 0);
            for (var i = 1; i < nbNewSelectable; ++i)
            {
                if (i < nbOldSelectable)
                {
                    selectable[i] = bindingList[i - ((NullSelectable) ? 1 : 0)];
                }
                else
                {
                    selectable.Add(bindingList[i - ((NullSelectable) ? 1 : 0)]);
                }
            }
            for (var i = nbNewSelectable; i < nbOldSelectable; ++i)
            {
                selectable.RemoveAt(nbNewSelectable);
            }
            selectable.ResetBindings();
        }

        public TChildType[] Write(BaseBindingList<TChildType> bindingList)
        {
            var array = new TChildType[bindingList.Count];
            bindingList.CopyTo(array, 0);
            return array;
        }

        public void Read(BaseBindingList<TChildType> bindingList, BaseBindingList<TChildType> selectable, TChildType[] array, object parent)
        {
            if (Sorted) array = array.OrderBy(KeySelector).ToArray();
            var lastRaiseListChangedEvents = bindingList.RaiseListChangedEvents;
            bindingList.RaiseListChangedEvents = false;
            selectable.RaiseListChangedEvents = false;
            bindingList.Clear();
            foreach (var item in array)
            {
                item.Parent = parent;
                bindingList.Add(item);
            }
            if (Selectable) UpdateSelectable(bindingList, selectable);
            bindingList.RaiseListChangedEvents = lastRaiseListChangedEvents;
            selectable.RaiseListChangedEvents = lastRaiseListChangedEvents;
        }

        public void CheckKeys(BaseBindingList<TChildType> bindingList)
        {
            foreach (var child in bindingList)
            {
                var child1 = child;
                child.Assert(KeySelector(child),
                             arg => bindingList.Count(aChild => KeySelector(child1).CompareTo(KeySelector(aChild)) == 0) == 1, KeyPropertyName, "\"{0}\" must be defined only once", new object[] { KeySelector(child) });
            }
        }

        #endregion
    }
}
