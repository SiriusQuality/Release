using System;
using System.Collections;
using System.Collections.Generic;

namespace SiriusModel.InOut.OutputWriter
{
    ///<summary>
    ///Base class for raw <see cref="RawData{T}"/>.
    ///</summary>
    public class RawData
    {
        #region Constants

        ///<summary>
        ///Default value for count and count increment.
        ///</summary>
        public const int ConstDefaultSize = 8;

        #endregion
    }

    ///<summary>
    ///A raw data is base class of <see cref="PageData"/> and <see cref="LineData"/>.
    ///</summary>
    ///<typeparam name="T">Item type in a raw data.</typeparam>
    public class RawData<T> : RawData, IEnumerable<T>
    {
        #region Fields

        ///<summary>
        ///Data in this row.
        ///</summary>
        private T[] data;

        ///<summary>
        ///Default count and count increment.
        ///</summary>
        private readonly int defaultSize;

        ///<summary>
        ///Current column count.
        ///</summary>
        private int count;

        #endregion

        #region Constructors

        ///<summary>
        ///Create a new raw data.
        ///</summary>
        public RawData()
            : this(ConstDefaultSize)
        {
            
        }

        ///<summary>
        ///Create a new raw data with specified default count (for count and count increment).
        ///</summary>
        ///<param name="count">Size of the raw <see cref="Data"/> buffer.</param>
        public RawData(int count)
        {
            Check.IsPositive(count);

            data = null;
            defaultSize = count;
        }

        #endregion

        #region Properties

        ///<summary>
        ///Get or set the value at the given index.
        ///</summary>
        ///<param name="index">The idnex of the value.</param>
        ///<returns>The value at the given index.</returns>
        public T this[int index]
        {
            get
            {
                Check.IsPositiveOrZero(index);
                return data != null ? (index < count ? data[index] : default(T)) : default(T);
            }
            set
            {
                Check.IsPositiveOrZero(index);
                var size = 0;
                if (data == null)
                {
                    size = (1 + index / defaultSize) * defaultSize;
                    data = new T[size];
                }
                if (index >= data.Length)
                {
                    size = (1 + index / defaultSize) * defaultSize;
                    Array.Resize(ref data, size);
                }
                count = Math.Max(count, index + 1);
                data[index] = value;
            }
        }

        ///<summary>
        ///Number of item in this raw.
        ///</summary>
        public int Count
        {
            get { return count; }
        }

        ///<summary>
        ///Get the pure array behind this raw data.
        ///This is the buffer.
        ///</summary>
        public T[] Data
        {
            get { return data; }
        }

        #endregion

        #region Format

        ///<summary>
        ///Clear this raw data.
        ///</summary>
        ///<returns>This raw data cleared.</returns>
        public RawData<T> Clear()
        {
            data = null;
            count = 0;
            return this;
        }

        ///<summary>
        ///Add a value at the end of this raw.
        ///</summary>
        ///<param name="value">The value to add.</param>
        ///<returns>This object.</returns>
        public RawData<T> Add(T value)
        {
            this[Count] = value;
            return this;
        }

        ///<summary>
        ///Add a null value in the next column of the this line.
        ///</summary>
        ///<returns>This object.</returns>
        public RawData<T> AddNull()
        {
            this[Count] = default(T);
            return this;
        }

        ///<summary>
        ///Add a null value in the next column of the this line.
        ///</summary>
        ///<returns>This object.</returns>
        public RawData<T> AddNull(int nbNull)
        {
            Check.IsPositive(nbNull);
            this[Count + nbNull - 1] = default(T);
            return this;
        }

        #endregion

        #region Implementation of IEnumerable<T>

        ///<summary>
        ///Enumerate all non <see cref="default(T)"/>.
        ///</summary>
        ///<returns>All non default.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < count; ++i)
            {
                var t = data[i];
                if (t != null && !t.Equals(default(T))) yield return t;
            }
        }

        ///<summary>
        ///Get object enumerator of <see cref="GetEnumerator"/>.
        ///</summary>
        ///<returns>All non default.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}