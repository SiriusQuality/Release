using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SiriusModel.InOut.Base
{
    public interface IProjectDataFileItem : IChild<string>, IComparable<IProjectDataFileItem>, IEquatable<IProjectDataFileItem>
    {
        [XmlAttribute("name")]
        string Name { get; set; }

        [XmlElement(ElementName = "Comments")]
        string Comments { get; set; }

        IProjectDataFile ProjectDataFileParent { get; }

        void UpdatePath(string oldAbsolute, string newAbsolute);
    }

    [Serializable]
    public abstract class ProjectDataFileItemNoChild : Child<string>, IProjectDataFileItem
    {
        protected ProjectDataFileItemNoChild()
            : this("")
        {
        }

        protected ProjectDataFileItemNoChild(string name)
        {
            this.name = name;
            comments = "";
        }

        private string name;
        private string comments;

        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { this.SetObject(ref name, ref value, "Name"); }
        }

        [XmlElement(ElementName="Comments")]
        public string Comments
        {
            get { return comments; }
            set
            {
                var deserialized = CommentsHelper.OnDeserialize(value);
                this.SetObject(ref comments, ref deserialized, "Comments");
            }
        }

        public abstract void UpdatePath(string oldAbsolute, string newAbsolute);

        #region IComparable Members

        public int CompareTo(IProjectDataFileItem objPDFI)
        {
            if (objPDFI != null)
            {
                return !ReferenceEquals(this, objPDFI) ? Name.CompareTo(objPDFI.Name) : 0;
            }
            return Name.CompareTo(null);
        }

        #endregion

        public override bool NotifyPropertyChanged(string propertyName)
        {
            if (propertyName != "NormalBook" && propertyName != "MultiBook" && propertyName != "SensitivityBook")
            {
                var file = ProjectDataFileParent;
                if (file != null) file.IsModified = true;
            }
            return base.NotifyPropertyChanged(propertyName);
        }

        public IProjectDataFile ProjectDataFileParent
        {
            get { return Parent as IProjectDataFile; }
        }

        public override string WarningFileID
        {
            get
            {
                return (ProjectDataFileParent != null) ? ProjectDataFileParent.ID : "?";
            }
        }

        public override string WarningItemName
        {
            get
            {
                return Name;
            }
        }

        public bool Equals(IProjectDataFileItem other)
        {
            return (this.Name == other.Name);
        }
    }

    [Serializable]
    public abstract class ProjectDataFileItem1Child<TChildType, TChildKey, TChildKeyGenerator>
        : ChildParent1<string, TChildType, TChildKey, TChildKeyGenerator>, IProjectDataFileItem
        where TChildKey : IComparable
        where TChildType : IChild<TChildKey>, new()
        where TChildKeyGenerator : IChildKeyGenerator<TChildType, TChildKey>, new()
    {
        protected ProjectDataFileItem1Child()
            : this("")
        {
        }

        protected ProjectDataFileItem1Child(string name)
        {
            this.name = name;
            comments = "";
            BindingItems1.ListChanged += BindingItems1ListChanged;
        }

        void BindingItems1ListChanged(object sender, ListChangedEventArgs e)
        {
            if (ProjectDataFileParent != null) ProjectDataFileParent.IsModified = true;
        }

        private string name;
        private string comments;

        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { this.SetObject(ref name, ref value, "Name"); }
        }

        [XmlElement(ElementName = "Comments")]
        public string Comments
        {
            get { return comments; }
            set
            {
                var deserialized = CommentsHelper.OnDeserialize(value);
                this.SetObject(ref comments, ref deserialized, "Comments");
            }
        }
        
        public abstract void UpdatePath(string oldAbsolute, string newAbsolute);

        public int CompareTo(IProjectDataFileItem objPDFI)
        {
            if (objPDFI != null)
            {
                if (!ReferenceEquals(this, objPDFI))
                {
                    return Name.CompareTo(objPDFI.Name);
                }
                return 0;
            }
            return Name.CompareTo(null);
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var objPDFI = obj as IProjectDataFileItem;
            if (objPDFI != null)
            {
                if (!ReferenceEquals(this, objPDFI))
                {
                    return Name.CompareTo(objPDFI.Name);
                }
                return 0;
            }
            return Name.CompareTo(null);
        }

        #endregion

        public override bool NotifyPropertyChanged(string propertyName)
        {
            if (ProjectDataFileParent != null) ProjectDataFileParent.IsModified = true;
            return base.NotifyPropertyChanged(propertyName);
        }

        public IProjectDataFile ProjectDataFileParent
        {
            get { return Parent as IProjectDataFile; }
        }

        public override string WarningFileID
        {
            get
            {
                return (ProjectDataFileParent != null) ? ProjectDataFileParent.ID : "?";
            }
        }

        public override string WarningItemName
        {
            get
            {
                return Name;
            }
        }

        public bool Equals(IProjectDataFileItem other)
        {
            return (this.Name == other.Name);
        }
    }

    [Serializable]
    public abstract class ProjectDataFileItem2Child<TChildType1, TChildKey1, TChildKeyGenerator1, TChildType2, TChildKey2, TChildKeyGenerator2>
        : ChildParent2<string, TChildType1, TChildKey1, TChildKeyGenerator1, TChildType2, TChildKey2, TChildKeyGenerator2>, IProjectDataFileItem
        where TChildKey1 : IComparable
        where TChildType1 : IChild<TChildKey1>, new()
        where TChildKeyGenerator1 : IChildKeyGenerator<TChildType1, TChildKey1>, new()
        where TChildKey2 : IComparable
        where TChildType2 : IChild<TChildKey2>, new()
        where TChildKeyGenerator2 : IChildKeyGenerator<TChildType2, TChildKey2>, new()
    {
        protected ProjectDataFileItem2Child()
            : this("")
        {
        }

        protected ProjectDataFileItem2Child(string name)
        {
            this.name = name;
            comments = "";
            BindingItems1.ListChanged += BindingItems1ListChanged;
            BindingItems2.ListChanged += BindingItems2ListChanged;
        }

        void BindingItems1ListChanged(object sender, ListChangedEventArgs e)
        {
            if (ProjectDataFileParent != null) ProjectDataFileParent.IsModified = true;
        }

        void BindingItems2ListChanged(object sender, ListChangedEventArgs e)
        {
            if (ProjectDataFileParent != null) ProjectDataFileParent.IsModified = true;
        }

        private string name;
        private string comments;
        private string experiment;

        [XmlAttribute("experiment")]
        public string Experiment
        {
            get { return experiment; }
            set { this.SetObject(ref experiment, ref value, "Experiment"); }
        }

        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { this.SetObject(ref name, ref value, "Name"); }
        }

        [XmlElement(ElementName = "Comments")]
        public string Comments
        {
            get { return comments; }
            set
            {
                var deserialized = CommentsHelper.OnDeserialize(value);
                this.SetObject(ref comments, ref deserialized, "Comments");
            }
        }

        public abstract void UpdatePath(string oldAbsolute, string newAbsolute);

        public int CompareTo(IProjectDataFileItem objPDFI)
        {
            if (objPDFI != null)
            {
                return !ReferenceEquals(this, objPDFI) ? Name.CompareTo(objPDFI.Name) : 0;
            }
            return Name.CompareTo(null);
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var objPDFI = obj as IProjectDataFileItem;
            if (objPDFI != null)
            {
                if (!ReferenceEquals(this, objPDFI))
                {
                    return Name.CompareTo(objPDFI.Name);
                }
                return 0;
            }
            return Name.CompareTo(null);
        }

        #endregion

        public override bool NotifyPropertyChanged(string propertyName)
        {
            if (ProjectDataFileParent != null) ProjectDataFileParent.IsModified = true;
            return base.NotifyPropertyChanged(propertyName);
        }

        public IProjectDataFile ProjectDataFileParent
        {
            get { return Parent as IProjectDataFile; }
        }

        public override string WarningFileID
        {
            get
            {
                return (ProjectDataFileParent != null) ? ProjectDataFileParent.ID : "?";
            }
        }

        public override string WarningItemName
        {
            get
            {
                return Name;
            }
        }

        public bool Equals(IProjectDataFileItem other)
        {
            return (this.Name == other.Name);
        }
    }

    [Serializable]
    public class ProjectDataFileItemKeyGenerator<TItemType> 
        : ChildKeyGeneratorSorted<TItemType, string>
        where TItemType : IProjectDataFileItem, new()
    {
        public override bool Selectable
        {
            get { return true; }
        }
        public override bool Sorted
        {
            get { return true; }
        }

        public override bool NullSelectable
        {
            get { return true; }
        }

        public override string KeyPropertyName
        {
            get { return "Name"; }
        }

        public override Func<TItemType, string> KeySelector
        {
            get { return item => item.Name; }
        }

        public override Func<TItemType, string, string> KeySetter
        {
            get { return delegate(TItemType item, string name) { item.Name = name; return name; }; }
        }

        public override void CreateNullSelectable(BaseBindingList<TItemType> selectable)
        {
            var newItem = new TItemType {Name = ""};
            newItem.ClearWarnings();
            selectable.Add(newItem);
        }
    }
}
