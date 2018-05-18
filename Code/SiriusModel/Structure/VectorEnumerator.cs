using System.Collections;
using System.Collections.Generic;

namespace SiriusModel.Structure
{
    public class VectorEnumerator<T> : IEnumerator<T>
    {
        private readonly Vector<T> vector;
        private int index;

        public VectorEnumerator(Vector<T> vector)
        {
            index = -1;
            this.vector = vector;
        }

        object IEnumerator.Current
        {
            get { return vector[index]; }
        }

        public T Current
        {
            get { return vector[index]; }
        }

        public bool MoveNext()
        {
            if (vector.Count == 0) return false;
            else
            {
                if (index < vector.Count - 1)
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
