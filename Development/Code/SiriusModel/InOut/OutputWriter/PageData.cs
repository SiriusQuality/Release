namespace SiriusModel.InOut.OutputWriter
{
    ///<summary>
    ///An optimized object[][].
    ///</summary>
    public class PageData : RawData<LineData>
    {
        #region Constructors

        ///<summary>
        ///Create a new page data.
        ///</summary>
        public PageData()
            : base(100)
        {
            
        }

        #endregion

        #region Properties

        public string Title { get; set; }

        ///<summary>
        ///Access to the given item in this array.
        ///</summary>
        ///<param name="rowIndex"></param>
        ///<param name="columnIndex"></param>
        ///<returns></returns>
        public object this [int rowIndex, int columnIndex]
        {
            get
            {
                Check.IsPositiveOrZero(rowIndex);
                Check.IsPositiveOrZero(columnIndex);
                var line = this[rowIndex];
                return line != null ? line[columnIndex] : null;
            }
            set
            {
                Check.IsPositiveOrZero(rowIndex);
                Check.IsPositiveOrZero(columnIndex);
                var line = this[rowIndex];
                if (line == null)
                {
                    this[rowIndex] = line = new LineData();
                }
                line[columnIndex] = value;
            }
        }

        ///<summary>
        ///Get the actual number of column used at the given row index.
        ///</summary>
        ///<param name="rowIndex">The row index.</param>
        ///<returns>The number of column at given index.</returns>
        public int ColumnCount(int rowIndex)
        {
            var line = this[rowIndex];
            return line != null ? line.Count : 0;
        }

        #endregion

        #region Format

        ///<summary>
        ///Create a new line.
        ///</summary>
        ///<returns>This object.</returns>
        public LineData NewLine()
        {
            var newLine = new LineData();
            this[Count] = newLine;
            return newLine;
        }

        ///<summary>
        ///Create a new line.
        ///</summary>
        ///<param name="nbLine">Number of line to create.</param>
        ///<returns>This object.</returns>
        public LineData NewLine(int nbLine)
        {
            Check.IsPositive(nbLine);
            for (var i = 0; i < nbLine - 1; ++i)
            {
                this[Count] = new LineData();
            }
            var newLine = new LineData();
            this[Count] = newLine;
            return newLine;
        }

        ///<summary>
        ///Set the minimal width.
        ///</summary>
        ///<param name="minWidth">Min width of this page.</param>
        ///<returns>This object.</returns>
        public PageData SetMinWidth(int minWidth)
        {
            for (var i = 0; i < Count; ++i)
            {
                this[i, minWidth - 1] = this[i, minWidth - 1];
            }
            return this;
        }

        ///<summary>
        ///Set the height.
        ///</summary>
        ///<param name="height">Height of the page.</param>
        ///<returns>This object.</returns>
        public PageData SetHeight(int height)
        {
            var rowIndex = height - 1;
            this[rowIndex] = new LineData();
            return this;
        }

        #endregion

        public new PageData Clear()
        {
            var count = Count;
            for (var i = 0; i < count; ++i)
            {
                var line = this[i];
                if (line != null) line.Clear();
            }
            base.Clear();
            return this;
        }
    }
}