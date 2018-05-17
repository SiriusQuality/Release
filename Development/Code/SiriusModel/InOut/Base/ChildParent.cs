using System;
using System.Xml.Serialization;

namespace SiriusModel.InOut.Base
{
    [Serializable]
    public class ChildParent1<TChildKey, TParentChildType, TParentChildKey, TParentChildKeyGenerator> 
        : Parent1Child<TParentChildType, TParentChildKey, TParentChildKeyGenerator>, IChild<TChildKey>
        where TChildKey : IComparable
        where TParentChildKey : IComparable
        where TParentChildType : IChild<TParentChildKey>, new()
        where TParentChildKeyGenerator : IChildKeyGenerator<TParentChildType, TParentChildKey>, new()
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

    [Serializable]
    public class ChildParent2<TChildKey, 
        TParentChildType1, TParentChildKey1, TParentChildKeyGenerator1, 
        TParentChildType2, TParentChildKey2, TParentChildKeyGenerator2>
        : Parent2Child<
        TParentChildType1, TParentChildKey1, TParentChildKeyGenerator1,
        TParentChildType2, TParentChildKey2, TParentChildKeyGenerator2>,
        IChild<TChildKey>
        where TChildKey : IComparable
        where TParentChildKey1 : IComparable
        where TParentChildType1 : IChild<TParentChildKey1>, new()
        where TParentChildKeyGenerator1 : IChildKeyGenerator<TParentChildType1, TParentChildKey1>, new()
        where TParentChildKey2 : IComparable
        where TParentChildType2 : IChild<TParentChildKey2>, new()
        where TParentChildKeyGenerator2 : IChildKeyGenerator<TParentChildType2, TParentChildKey2>, new()
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
