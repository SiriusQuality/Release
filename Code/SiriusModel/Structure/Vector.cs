using System;
using System.Collections;
using System.Collections.Generic;

namespace SiriusModel.Structure
{
    ///<summary>
    ///This class contains an array of object. 
    ///</summary>
    ///<typeparam name="T">The type of object to contain.</typeparam>
    public class Vector<T> : IEnumerable<T>, IDisposable
    {
        ///<summary>
        ///Items of the vector.
        ///</summary>
        T[] items;

        ///<summary>
        ///Create a new vector.
        ///</summary>
        public Vector()
        {
            items = new T[0];
        }

        ///<summary>
        ///Create a new vector with a specified initial length.
        ///</summary>
        ///<param name="count">The specified length of this vector.</param>
        public Vector(int count)
        {
            items = new T[count];
        }

        ///<summary>
        ///Access to an item of the vector.
        ///</summary>
        ///<param name="index">The index of the item to acces.</param>
        ///<returns>The item at the specified index.</returns>
        public T this[int index]
        {
            get { return items[index]; }
            set { items[index] = value; }
        }

        ///<summary>
        ///Add an item at the end of the vector.
        ///</summary>
        ///<param name="toPush">The item to push.</param>
        public void Add(T toPush)
        {
            Array.Resize(ref items, items.Length + 1);
            items[items.Length - 1] = toPush;
        }

        ///<summary>
        ///Count the number of item in the vector.
        ///</summary>
        public int Count
        {
            get { return items.Length; }
        }

        ///<summary>
        ///Erase all item in the vector.
        ///</summary>
        public void Clear()
        {
            items = new T[0];
        }

        ///<summary>
        ///create a new vector of size "size" et fill it with items with the value "value".
        public void Assign(int size, T value)
        {
            items = new T[size];
            for (int i = 0; i < size; i++)
            {
                items[i] = value;
            }
        }
        ///</summary>
       
        ///<summary>
        ///Enumerate items of this vector.
        ///</summary>
        ///<returns>An enumerator on the items of this vector.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new VectorEnumerator<T>(this);
        }

        ///<summary>
        ///Enumerate items of this vector.
        ///</summary>
        ///<returns>An enumeratr on the items of this vector.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        /// <summary>
        /// Clone this vector. This do a shadow copy.
        /// </summary>
        /// <returns>aSheath shadow copy of this vector.</returns>
        public Vector<T> Clone()
        {
            var count = Count;
            var clone = new Vector<T>(count);
            for (var i = 0; i < count; ++i)
            {
                clone.items[i] = this[i];
            }
            return clone;
        }

         public void Dispose()
        {
            items = null;
        }
    }
}
