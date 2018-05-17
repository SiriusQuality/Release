using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SiriusModel.Structure
{
    ///<summary>
    ///Define a selection on a Vector. The selection can be not continuous.
    ///</summary>
    ///<typeparam name="T">The type of the item in the vector.</typeparam>
    ///<typeparam name="ST">The type of the item in the selection (item of the vector are 
    ///casted to this type).</typeparam>
    public class VectorSelection<T, ST> : IEnumerable<ST>, ICloneable, IDisposable where ST : T
    {
        ///<summary>
        ///The reference to the vector.
        ///</summary>
        private Vector<T> vector;

        ///<summary>
        ///The indexs of the item in this selection.
        ///</summary>
        private int[] indexs;

        ///<summary>
        ///Create a new selection.
        ///</summary>
        ///<param name="vector">The vector of this selection.</param>
        public VectorSelection(Vector<T> vector)
        {
            this.vector = vector;
            indexs = new int[0];
        }

        ///<summary>
        ///Create a new selection by copy.
        ///</summary>
        ///<param name="vector">The vector of this selection.</param>
        ///<param name="toCopy">The selection to copy.</param>
        public VectorSelection(Vector<T> vector, VectorSelection<T, ST> toCopy)
        {
            this.vector = vector;
            indexs = (int[])toCopy.indexs.Clone();
        }

        public object Clone()
        {
            VectorSelection<T, ST> newVectorSelection = (VectorSelection<T, ST>)this.MemberwiseClone();
            return newVectorSelection;
        }

        ///<summary>
        ///Enumerate items of this vector selection.
        ///</summary>
        ///<returns>An enumerator on the items of this vector selection.</returns>
        public IEnumerator<ST> GetEnumerator()
        {
            return new VectorSelectionEnumerator<T, ST>(this);
        }

        ///<summary>
        ///Enumerate items of this vector selection.
        ///</summary>
        ///<returns>An enumeratr on the items of this vector selection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new VectorSelectionEnumerator<T, ST>(this);
        }

        ///<summary>
        ///Clear this selection.
        ///</summary>
        public void Clear()
        {
            Array.Resize(ref indexs, 0);
        }

        ///<summary>
        ///Add an item to this selection.
        ///</summary>
        ///<param name="index">The index of the item in the vector to add.</param>
        public void Add(int index)
        {
            Array.Resize(ref indexs, indexs.Length + 1);
            indexs[indexs.Length - 1] = index;
            Array.Sort(indexs);
        }

        ///<summary>
        ///Remove an item to this selection.
        ///</summary>
        ///<param name="index">The index of the item in the vector to remove.</param>
        public void Remove(int index)
        {
            var indexItem = Array.IndexOf(indexs, index);
            var nbIndex = indexs.Length;
            for (var i = indexItem; i < nbIndex - 1; ++i)
            {
                indexs[i] = indexs[i + 1];
            }
            Array.Resize(ref indexs, nbIndex - 1);
        }

        ///<summary>
        ///Check if this selection contains a specified item.
        ///</summary>
        ///<param name="index">The index of the item in the vector.</param>
        ///<returns>True if this selection contains this item, otherwise false.</returns>
        public bool Contains(int index)
        {
            return indexs.Contains(index);
        }

        ///<summary>
        ///Access to an item of this selection.
        ///</summary>
        ///<param name="index">The index of the item to access.</param>
        ///<returns>The item at indexs[index] in the associated vector.</returns>
        public ST this[int index]
        {
            get { return (ST)vector[indexs[index]]; }
            set { vector[indexs[index]] = (T)value; }
        }

        ///<summary>
        ///Count the number of items in this selection.
        ///</summary>
        public int Count
        {
            get { return indexs.Length; }
        }

        ///<summary>
        ///Get or set the vector of this selection.
        ///</summary>
        public Vector<T> Vector
        {
            get { return vector; }
            set { vector = value; }
        }

        public void Dispose()
        {
            vector = null;
        }
    }
}
