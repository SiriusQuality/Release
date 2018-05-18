using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace SiriusModel.InOut.Base
{
    [Serializable]
    public abstract class ProjectDataFile<TItemType> 
        : Parent1Child<TItemType, string, ProjectDataFileItemKeyGenerator<TItemType>>, IProjectDataFile
        where TItemType : IProjectDataFileItem, new()
    {
        private string absoluteFileName;

        protected ProjectDataFile()
        {
            absoluteFileName = "?";
            IsModified = false;
            BindingItems1.ListChanged += BindingItems1ListChanged;
        }

        protected void BindingItems1ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType != ListChangedType.Reset)
            {
                var propertyDescriptor = e.PropertyDescriptor;
                var propertyName = propertyDescriptor != null ? propertyDescriptor.Name : null;
                if (propertyName != "NormalBook" && propertyName != "MultiBook" && propertyName != "SensitivityBook")
                {
                    IsModified = true;
                }
            }
        }

        [XmlIgnore]
        public BaseBindingList<TItemType> Items { get { return BindingItems1; } }

        [XmlIgnore]
        public BaseBindingList<TItemType> ItemsSelectable { get { return SelectableItems1; } }

        [Browsable(false)]
        public TItemType[] ItemsArray
        {
            get { return BindingItemsArray1; }
            set { BindingItemsArray1 = value; }
        }

        #region IProjectDataFile

        [XmlIgnore]
        public virtual string AbsoluteFileName
        {
            get { return absoluteFileName; }
            set
            {
                var oldAbsolute = AbsoluteFileName;
                var oldRelativeFileName = RelativeFileName  ; 
                if (this.SetObject(ref absoluteFileName, ref value, "AbsoluteFileName"))
                {
                    var newRelativeFileName = RelativeFileName;
                    this.SetObject(ref oldRelativeFileName, ref newRelativeFileName, "RelativeFileName");
                    ProjectFile.This.FileContainer.IsModified = true;
                }

                var newAbsolute = AbsoluteFileName;
                UpdateItems(oldAbsolute, newAbsolute);
            }
        }

        [XmlIgnore]
        public virtual string RelativeFileName
        {
            get { return this.GetProjectRelativeFileName(absoluteFileName); }
            set
            {
                var oldAbsolute = AbsoluteFileName;
                if (value != RelativeFileName)
                {
                    AbsoluteFileName = this.GetProjectAbsoluteFileName(value);
                    NotifyPropertyChanged("RelativeFileName");
                    ProjectFile.This.FileContainer.IsModified = true;
                }
                var newAbsolute = AbsoluteFileName;
                UpdateItems(oldAbsolute, newAbsolute);
            }
        }

        public abstract string ID
        {
            get;
        }

        public abstract string FileExtension
        {
            get;
        }

        public void CopyFrom(IProjectDataFile toCopy)
        {
            BindingItemsArray1 = ((ProjectDataFile<TItemType>)toCopy).BindingItemsArray1;
        }

        public bool Contains(string itemName)
        {
            var found = BindingItems1.FirstOrDefault(i => i.Name == itemName);
            return found != null;
        }

        public void CopyFileNameFrom(IProjectDataFile toCopy)
        {
            AbsoluteFileName = toCopy.AbsoluteFileName;
        }

        private void UpdateItems(string oldAbsolute, string newAbsolute)
        {
            if (oldAbsolute == newAbsolute) return;
            foreach (var item in Items)
            {
                item.UpdatePath(oldAbsolute, newAbsolute);
            }
        }

        #endregion

        private bool isModified;

        [XmlIgnore, Browsable(false)]
        public bool IsModified
        {
            get { return isModified; }
            set
            {
                if (this.SetStruct(ref isModified, ref value, "IsModified"))
                {
                    NotifyPropertyChanged("IsModifiedStr");
                }
            }
        }

        [XmlIgnore]
        public string IsModifiedStr
        {
            get { return (isModified) ? "*" : ""; }
        }

        public override bool NotifyPropertyChanged(string propertyName)
        {
            if (propertyName != "IsModified" && propertyName != "IsModifiedStr")
            {
                IsModified = true;
            }
            return base.NotifyPropertyChanged(propertyName);
        }

        public override string WarningFileID
        {
            get
            {
                return ID;
            }
        }

        public override string WarningItemName
        {
            get
            {
                return "";
            }
        }
    }
}
