using System;
using System.Xml.Serialization;

namespace SiriusModel.InOut.Base
{
    public interface IChild : IProjectItem
    {
        object Parent { get; set; }
    }

    public interface IChild<TChildKey> : IChild
        where TChildKey : IComparable
    {
    }

    [Serializable]
    public abstract class Child<TChildKey> : ProjectItem, IChild<TChildKey>
        where TChildKey : IComparable
    {
        [NonSerialized]
        private object parent;

        [XmlIgnore]
        public object Parent
        {
            get { return parent; }
            set { this.SetObject(ref parent, ref value, "Parent"); }
        }
    }
}
