using System;
using System.Collections;
using System.Collections.Generic;

namespace SiriusModel.Structure
{
    /// <summary>
    /// Define a segment on a vector. The segment has to be continuous.
    /// </summary>
    /// <typeparam name="T">The type of the item in the vector.</typeparam>
    /// <typeparam name="ST">The type of the item in the segment (item of the vector are 
    /// casted to this type).</typeparam>
    public class VectorSegment<T, ST> : IEnumerable<ST>, IDisposable where ST : T
    {
        /// <summary>
        /// The reference to the vector.
        /// </summary>
        private Vector<T> vector;

        /// <summary>
        /// The start index of the segment.
        /// </summary>
        private int start;

        /// <summary>
        /// The stop index of the segment.
        /// </summary>
        private int stop;

        /// <summary>
        /// Create a new segment.
        /// </summary>
        /// <param name="vector">The vector of this segment.</param>
        /// <param name="start">The start index of this segment.</param>
        /// <param name="stop">The stop index of this segment.</param>
        public VectorSegment(Vector<T> vector, int start, int stop)
        {
            this.vector = vector;
            this.start = start;
            this.stop = stop;
        }

        /// <summary>
        /// Create a new segment.
        /// </summary>
        /// <param name="vector">The vector of this segment.</param>
        public VectorSegment(Vector<T> vector)
            : this(vector, -1, -1)
        {
        }

        /// <summary>
        /// Create a new segment by copy.
        /// </summary>
        /// <param name="vector">The vector of this segment.</param>
        /// <param name="toCopy">The vector segment to copy.</param>
        public VectorSegment(Vector<T> vector, VectorSegment<T, ST> toCopy)
            : this(vector, toCopy.start, toCopy.stop)
        {
        }

        /// <summary>
        /// Enumerate items of this vector segment.
        /// </summary>
        /// <returns>An enumerator on the items of this vector segment.</returns>
        public IEnumerator<ST> GetEnumerator()
        {
            return new VectorSegmentEnumerator<T, ST>(this);
        }

        /// <summary>
        /// Enumerate items of this vector segment.
        /// </summary>
        /// <returns>An enumeratr on the items of this vector segment.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new VectorSegmentEnumerator<T, ST>(this);
        }

        /// <summary>
        /// Clear this selection.
        /// </summary>
        public void Clear()
        {
            start = stop = -1;
        }

        /// <summary>
        /// Get or set the start index of this segment.
        /// </summary>
        public int Start
        {
            get { return start; }
            set
            {
                start = value;
                if (start < 0 && start != -1) throw new ArgumentException("start index must be > 0 or = -1");
            }
        }

        /// <summary>
        /// Get or set the stop index of this segment.
        /// </summary>
        public int Stop
        {
            get { return stop; }
            set
            {
                stop = value;
                if (stop < 0 && stop != -1) throw new ArgumentException("stop index must be > 0 or = -1");
            }
        }

        /// <summary>
        /// Check if this segment contains a specified item.
        /// </summary>
        /// <param name="index">The index of the item in the vector.</param>
        /// <returns>True if this segment contains this item, otherwise false.</returns>
        public bool Contains(int index)
        {
            return (start != -1 && stop != -1) ? start <= index && index < stop : false;
        }

        /// <summary>
        /// Access to an item of this segment.
        /// </summary>
        /// <param name="index">The index of the item to access.</param>
        /// <returns>The item at index + Start in the associated vector.</returns>
        public ST this[int index]
        {
            get { return (ST)vector[start + index]; }
            set { vector[start + index] = (T)value; }
        }

        /// <summary>
        /// Count the number of items in this segment.
        /// </summary>
        public int Count
        {
            get { return (start != -1 && stop != -1) ? stop - start : 0; }
        }

        /// <summary>
        /// Get or set the vector of this segment.
        /// </summary>
        public Vector<T> Vector
        {
            get { return vector; }
            set { vector = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            vector = null;
        }

        #endregion
    }
}
