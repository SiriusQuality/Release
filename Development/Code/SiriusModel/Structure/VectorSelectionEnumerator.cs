using System.Collections;
using System.Collections.Generic;

namespace SiriusModel.Structure
{
    public class VectorSelectionEnumerator<T, ST> : IEnumerator<ST> where ST : T
    {
        private readonly VectorSelection<T, ST> vectorSelection;
        private int index;

        public VectorSelectionEnumerator(VectorSelection<T, ST> vectorSelection)
        {
            index = -1;
            this.vectorSelection = vectorSelection;
        }

        object IEnumerator.Current
        {
            get { return vectorSelection[index]; }
        }

        public ST Current
        {
            get { return vectorSelection[index]; }
        }

        public bool MoveNext()
        {
            if (vectorSelection.Count == 0) return false;
            else
            {
                if (index < vectorSelection.Count - 1)
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
