using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SiriusModel.InOut.Base
{
    [Serializable]
    public class Parent1Child<TChildType, TChildKey, TChildKeyGenerator> : ProjectItem
        where TChildKey : IComparable
        where TChildType : IChild<TChildKey>, new()
        where TChildKeyGenerator : IChildKeyGenerator<TChildType, TChildKey>, new()
    {
        protected readonly TChildKeyGenerator generator1;

        #region binding items
        private readonly BaseBindingList<TChildType> bindingItems1;

        [XmlIgnore]
        protected BaseBindingList<TChildType> BindingItems1 { get { return bindingItems1; } }
        #endregion

        #region selectable items
        private readonly BaseBindingList<TChildType> selectableItems1;

        [XmlIgnore]
        protected BaseBindingList<TChildType> SelectableItems1 { get { return selectableItems1; } }
        #endregion

        public Parent1Child()
        {
            generator1 = new TChildKeyGenerator();

            bindingItems1 = new BaseBindingList<TChildType>();
            selectableItems1 = new BaseBindingList<TChildType>();

            bindingItems1.ListChanged += ParentListItem1Changed;
            if (generator1.NullSelectable) generator1.CreateNullSelectable(selectableItems1);
        }

        public override void ClearWarnings()
        {
            base.ClearWarnings();
            foreach (var child in bindingItems1)
            {
                child.ClearWarnings();
            }
        }

        public override void CheckWarnings()
        {
            foreach (var child in bindingItems1)
            {
                child.CheckWarnings();
            }
            if (generator1.Sorted) generator1.CheckKeys(bindingItems1);
        }

        public void Sort()
        {
            generator1.Sort(BindingItems1, SelectableItems1);
        }

        #region this binding list tools

        private void ParentListItem1Changed(object sender, ListChangedEventArgs e)
        {
            if (generator1.Sorted
                && e.ListChangedType == ListChangedType.ItemChanged
                && e.PropertyDescriptor != null && e.PropertyDescriptor.Name == generator1.KeyPropertyName)
            {
                generator1.CheckKeys(bindingItems1);
            }
            if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                if (generator1.Selectable) generator1.UpdateSelectable(bindingItems1, selectableItems1);
                if (generator1.Sorted) generator1.CheckKeys(bindingItems1);
            }
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                bindingItems1[e.NewIndex].Parent = this;
                if (generator1.Selectable) generator1.UpdateSelectable(bindingItems1, selectableItems1);
                if (generator1.Sorted) generator1.CheckKeys(bindingItems1);
            }
        }

        [XmlIgnore]
        public TChildType this[TChildKey key]
        {
            get { return generator1.Find(bindingItems1, key); }
        }

        [Browsable(false), XmlIgnore]
        protected TChildType[] BindingItemsArray1
        {
            get { return generator1.Write(bindingItems1); }
            set
            {
                generator1.Read(bindingItems1, selectableItems1, value, this);
                if (generator1.Sorted) generator1.CheckKeys(bindingItems1);
            }
        }

        public void SuspendBinding()
        {
            bindingItems1.RaiseListChangedEvents = false;
            selectableItems1.RaiseListChangedEvents = false;
        }

        public void ResumeBinding()
        {
            bindingItems1.RaiseListChangedEvents = true;
            selectableItems1.RaiseListChangedEvents = true;
            bindingItems1.ResetBindings();
            selectableItems1.ResetBindings();
        }
        #endregion
    }

    [Serializable]
    public class Parent2Child<TChildType1, TChildKey1, TChildKeyGenerator1, TChildType2, TChildKey2, TChildKeyGenerator2> : ProjectItem
        where TChildKey1 : IComparable
        where TChildType1 : IChild<TChildKey1>, new()
        where TChildKeyGenerator1 : IChildKeyGenerator<TChildType1, TChildKey1>, new()
        where TChildKey2 : IComparable
        where TChildType2 : IChild<TChildKey2>, new()
        where TChildKeyGenerator2 : IChildKeyGenerator<TChildType2, TChildKey2>, new()
    {
        protected readonly TChildKeyGenerator1 generator1;
        protected readonly TChildKeyGenerator2 generator2;

        #region binding items
        private readonly BaseBindingList<TChildType1> bindingItems1;

        [XmlIgnore]
        public BaseBindingList<TChildType1> BindingItems1 { get { return bindingItems1; } }

        private readonly BaseBindingList<TChildType2> bindingItems2;

        [XmlIgnore]
        public BaseBindingList<TChildType2> BindingItems2 { get { return bindingItems2; } }
        #endregion

        #region selectable items
        private readonly BaseBindingList<TChildType1> selectableItems1;

        [XmlIgnore]
        public BaseBindingList<TChildType1> SelectableItems1 { get { return selectableItems1; } }

        private readonly BaseBindingList<TChildType2> selectableItems2;

        [XmlIgnore]
        public BaseBindingList<TChildType2> SelectableItems2 { get { return selectableItems2; } }
        #endregion

        public Parent2Child()
        {
            generator1 = new TChildKeyGenerator1();
            generator2 = new TChildKeyGenerator2();

            bindingItems1 = new BaseBindingList<TChildType1>();
            selectableItems1 = new BaseBindingList<TChildType1>();

            bindingItems2 = new BaseBindingList<TChildType2>();
            selectableItems2 = new BaseBindingList<TChildType2>();

            bindingItems1.ListChanged += ParentListItem1Changed;
            if (generator1.NullSelectable) generator1.CreateNullSelectable(selectableItems1);

            bindingItems2.ListChanged += ParentListItem2Changed;
            if (generator2.NullSelectable) generator2.CreateNullSelectable(selectableItems2);
        }

        public override void ClearWarnings()
        {
            base.ClearWarnings();
            foreach (var child in bindingItems1)
            {
                child.ClearWarnings();
            }

            foreach (var child in bindingItems2)
            {
                child.ClearWarnings();
            }
        }

        public override void CheckWarnings()
        {
            base.ClearWarnings();
            foreach (var child in bindingItems1)
            {
                child.CheckWarnings();
            }
            if (generator1.Sorted) generator1.CheckKeys(bindingItems1);

            foreach (var child in bindingItems2)
            {
                child.CheckWarnings();
            }
            if (generator2.Sorted) generator2.CheckKeys(bindingItems2);
        }

        #region this binding list tools

        private void ParentListItem1Changed(object sender, ListChangedEventArgs e)
        {
            if (generator1.Sorted
                && e.ListChangedType == ListChangedType.ItemChanged
                && e.PropertyDescriptor != null && e.PropertyDescriptor.Name == generator1.KeyPropertyName)
            {
                generator1.CheckKeys(bindingItems1);
            }
            if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                if (generator1.Selectable) generator1.UpdateSelectable(bindingItems1, selectableItems1);
                if (generator1.Sorted) generator1.CheckKeys(bindingItems1);
            }
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                bindingItems1[e.NewIndex].Parent = this;
                if (generator1.Selectable) generator1.UpdateSelectable(bindingItems1, selectableItems1);
                if (generator1.Sorted) generator1.CheckKeys(bindingItems1);
            }
        }

        private void ParentListItem2Changed(object sender, ListChangedEventArgs e)
        {
            if (generator2.Sorted
                && e.ListChangedType == ListChangedType.ItemChanged
                && e.PropertyDescriptor != null && e.PropertyDescriptor.Name == generator2.KeyPropertyName)
            {
                generator2.CheckKeys(bindingItems2);
            }
            if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                if (generator2.Selectable) generator2.UpdateSelectable(bindingItems2, selectableItems2);
                if (generator2.Sorted) generator2.CheckKeys(bindingItems2);
            }
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                bindingItems2[e.NewIndex].Parent = this;
                if (generator2.Selectable) generator2.UpdateSelectable(bindingItems2, selectableItems2);
                if (generator2.Sorted) generator2.CheckKeys(bindingItems2);
            }
        }

        [XmlIgnore]
        public TChildType1 this[TChildKey1 key]
        {
            get { return generator1.Find(bindingItems1, key); }
        }

        [XmlIgnore]
        public TChildType2 this[TChildKey2 key]
        {
            get { return generator2.Find(bindingItems2, key); }
        }

        [Browsable(false), XmlIgnore]
        public TChildType1[] BindingItemsArray1
        {
            get { return generator1.Write(bindingItems1); }
            set
            {
                generator1.Read(bindingItems1, selectableItems1, value, this);
                if (generator1.Sorted) generator1.CheckKeys(bindingItems1);
            }
        }

        [Browsable(false), XmlIgnore]
        public TChildType2[] BindingItemsArray2
        {
            get { return generator2.Write(bindingItems2); }
            set
            {
                generator2.Read(bindingItems2, selectableItems2, value, this);
                if (generator2.Sorted) generator2.CheckKeys(bindingItems2);
            }
        }

        public void SuspendBinding()
        {
            bindingItems1.RaiseListChangedEvents = false;
            selectableItems1.RaiseListChangedEvents = false;
            bindingItems2.RaiseListChangedEvents = false;
            selectableItems2.RaiseListChangedEvents = false;
        }

        public void ResumeBinding()
        {
            bindingItems1.RaiseListChangedEvents = true;
            selectableItems1.RaiseListChangedEvents = true;
            bindingItems2.RaiseListChangedEvents = true;
            selectableItems2.RaiseListChangedEvents = true;
            bindingItems1.ResetBindings();
            selectableItems1.ResetBindings();
            bindingItems2.ResetBindings();
            selectableItems2.ResetBindings();
        }
        #endregion

        public void Sort1()
        {
            generator1.Sort(BindingItems1, SelectableItems1);
        }

        public void Sort2()
        {
            generator2.Sort(BindingItems2, SelectableItems2);
        }
    }

    [Serializable]
    public class Parent7Child< TChildType1,TChildType2, TChildType3,TChildType4,TChildType5,TChildType6,TChildType7> : ProjectItem
        where TChildType1 : IChild, new()
        where TChildType2 : IChild, new()
        where TChildType3 : IChild, new()
        where TChildType4 : IChild, new()
        where TChildType5 : IChild, new()
        where TChildType6 : IChild, new()
        where TChildType7 : IChild, new()
    {
        #region binding items
        private readonly BaseBindingList<TChildType1> bindingItems1;

        [XmlIgnore]
        public BaseBindingList<TChildType1> BindingItems1 { get { return bindingItems1; } }

        private readonly BaseBindingList<TChildType2> bindingItems2;

        [XmlIgnore]
        public BaseBindingList<TChildType2> BindingItems2 { get { return bindingItems2; } }

        private readonly BaseBindingList<TChildType3> bindingItems3;

        [XmlIgnore]
        public BaseBindingList<TChildType3> BindingItems3 { get { return bindingItems3; } }

        private readonly BaseBindingList<TChildType4> bindingItems4;

        [XmlIgnore]
        public BaseBindingList<TChildType4> BindingItems4 { get { return bindingItems4; } }

        private readonly BaseBindingList<TChildType5> bindingItems5;

        [XmlIgnore]
        public BaseBindingList<TChildType5> BindingItems5 { get { return bindingItems5; } }

        private readonly BaseBindingList<TChildType6> bindingItems6;

        [XmlIgnore]
        public BaseBindingList<TChildType6> BindingItems6 { get { return bindingItems6; } }

        private readonly BaseBindingList<TChildType7> bindingItems7;

        [XmlIgnore]
        public BaseBindingList<TChildType7> BindingItems7 { get { return bindingItems7; } }
        #endregion

        #region selectable items
        private readonly BaseBindingList<TChildType1> selectableItems1;

        [XmlIgnore]
        public BaseBindingList<TChildType1> SelectableItems1 { get { return selectableItems1; } }

        private readonly BaseBindingList<TChildType2> selectableItems2;

        [XmlIgnore]
        public BaseBindingList<TChildType2> SelectableItems2 { get { return selectableItems2; } }

        private readonly BaseBindingList<TChildType3> selectableItems3;

        [XmlIgnore]
        public BaseBindingList<TChildType3> SelectableItems3 { get { return selectableItems3; } }

        private readonly BaseBindingList<TChildType4> selectableItems4;

        [XmlIgnore]
        public BaseBindingList<TChildType4> SelectableItems4 { get { return selectableItems4; } }

        private readonly BaseBindingList<TChildType5> selectableItems5;

        [XmlIgnore]
        public BaseBindingList<TChildType5> SelectableItems5 { get { return selectableItems5; } }

        private readonly BaseBindingList<TChildType6> selectableItems6;

        [XmlIgnore]
        public BaseBindingList<TChildType6> SelectableItems6 { get { return selectableItems6; } }

        private readonly BaseBindingList<TChildType7> selectableItems7;

        [XmlIgnore]
        public BaseBindingList<TChildType7> SelectableItems7 { get { return selectableItems7; } }

        #endregion

        public Parent7Child()
        {
            bindingItems1 = new BaseBindingList<TChildType1>();
            selectableItems1 = new BaseBindingList<TChildType1>();

            bindingItems2 = new BaseBindingList<TChildType2>();
            selectableItems2 = new BaseBindingList<TChildType2>();

            bindingItems3 = new BaseBindingList<TChildType3>();
            selectableItems3 = new BaseBindingList<TChildType3>();

            bindingItems4 = new BaseBindingList<TChildType4>();
            selectableItems4 = new BaseBindingList<TChildType4>();

            bindingItems5 = new BaseBindingList<TChildType5>();
            selectableItems5 = new BaseBindingList<TChildType5>();

            bindingItems6 = new BaseBindingList<TChildType6>();
            selectableItems6 = new BaseBindingList<TChildType6>();

            bindingItems7 = new BaseBindingList<TChildType7>();
            selectableItems7 = new BaseBindingList<TChildType7>();
        }

        public override void ClearWarnings()
        {
            base.ClearWarnings();
            foreach (var child in bindingItems1)
            {
                child.ClearWarnings();
            }

            foreach (var child in bindingItems2)
            {
                child.ClearWarnings();
            }

            foreach (var child in bindingItems3)
            {
                child.ClearWarnings();
            }

            foreach (var child in bindingItems4)
            {
                child.ClearWarnings();
            }

            foreach (var child in bindingItems5)
            {
                child.ClearWarnings();
            }

            foreach (var child in bindingItems6)
            {
                child.ClearWarnings();
            }

            foreach (var child in bindingItems7)
            {
                child.ClearWarnings();
            }

        }

        public override void CheckWarnings()
        {
            base.ClearWarnings();
            foreach (var child in bindingItems1)
            {
                child.CheckWarnings();
            }

            foreach (var child in bindingItems2)
            {
                child.CheckWarnings();
            }

            foreach (var child in bindingItems3)
            {
                child.CheckWarnings();
            }

            foreach (var child in bindingItems4)
            {
                child.CheckWarnings();
            }

            foreach (var child in bindingItems5)
            {
                child.CheckWarnings();
            }

            foreach (var child in bindingItems6)
            {
                child.CheckWarnings();
            }

            foreach (var child in bindingItems7)
            {
                child.CheckWarnings();
            }

        }

        #region this binding list tools

        public void SuspendBinding()
        {
            bindingItems1.RaiseListChangedEvents = false;
            selectableItems1.RaiseListChangedEvents = false;
            bindingItems2.RaiseListChangedEvents = false;
            selectableItems2.RaiseListChangedEvents = false;
            bindingItems3.RaiseListChangedEvents = false;
            selectableItems3.RaiseListChangedEvents = false;
            bindingItems4.RaiseListChangedEvents = false;
            selectableItems4.RaiseListChangedEvents = false;
            bindingItems5.RaiseListChangedEvents = false;
            selectableItems5.RaiseListChangedEvents = false;
            bindingItems6.RaiseListChangedEvents = false;
            selectableItems6.RaiseListChangedEvents = false;
            bindingItems7.RaiseListChangedEvents = false;
            selectableItems7.RaiseListChangedEvents = false;
        }

        public void ResumeBinding()
        {
            bindingItems1.RaiseListChangedEvents = true;
            selectableItems1.RaiseListChangedEvents = true;
            bindingItems2.RaiseListChangedEvents = true;
            selectableItems2.RaiseListChangedEvents = true;
            bindingItems3.RaiseListChangedEvents = true;
            selectableItems3.RaiseListChangedEvents = true;
            bindingItems4.RaiseListChangedEvents = true;
            selectableItems4.RaiseListChangedEvents = true;
            bindingItems5.RaiseListChangedEvents = true;
            selectableItems5.RaiseListChangedEvents = true;
            bindingItems6.RaiseListChangedEvents = true;
            selectableItems6.RaiseListChangedEvents = true;
            bindingItems7.RaiseListChangedEvents = true;
            selectableItems7.RaiseListChangedEvents = true;
            bindingItems1.ResetBindings();
            selectableItems1.ResetBindings();
            bindingItems2.ResetBindings();
            selectableItems2.ResetBindings();
            bindingItems3.ResetBindings();
            selectableItems3.ResetBindings();
            bindingItems4.ResetBindings();
            selectableItems4.ResetBindings();
            bindingItems5.ResetBindings();
            selectableItems5.ResetBindings();
            bindingItems6.ResetBindings();
            selectableItems6.ResetBindings();
            bindingItems7.ResetBindings();
            selectableItems7.ResetBindings();
        }

        #endregion
    }
}