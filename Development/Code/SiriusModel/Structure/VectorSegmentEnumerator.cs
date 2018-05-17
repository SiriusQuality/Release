using System.Collections;
using System.Collections.Generic;

namespace SiriusModel.Structure
{
    public class VectorSegmentEnumerator<T, ST> : IEnumerator<ST> where ST : T
    {
        private readonly VectorSegment<T, ST> vectorSegment;
        private int index;

        public VectorSegmentEnumerator(VectorSegment<T, ST> vectorSegment)
        {
            index = -1;
            this.vectorSegment = vectorSegment;
        }

        object IEnumerator.Current
        {
            get { return vectorSegment[index]; }
        }

        public ST Current
        {
            get { return vectorSegment[index]; }
        }

        public bool MoveNext()
        {
            if (vectorSegment.Count == 0) return false;
            else
            {
                if (index < vectorSegment.Count - 1)
                {
                    ++index;
                    return true;
                }
                else return false;
            }
        }

        public void Reset()
        {
            index = -1;
        }

        public void Dispose()
        {

        }
    }
}
