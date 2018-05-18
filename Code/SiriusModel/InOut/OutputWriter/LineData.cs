using System;

namespace SiriusModel.InOut.OutputWriter
{
    ///<summary>
    ///An optimized object[] for a single line.
    ///</summary>
    public class LineData : RawData<object>
    {
        #region Constructors

        ///<summary>
        ///Create a new page line with specified dimensions.
        ///</summary>
        public LineData()
            : base()
        {
        }

        #endregion

        #region Format

        ///<summary>
        ///Add an object.
        ///</summary>
        ///<param name="value">Object to add.</param>
        ///<returns>This object.</returns>
        public new LineData Add(object value)
        {
            base.Add(value);
            return this;
        }

        ///<summary>
        ///Add a list of value to the end of this line data.
        ///</summary>
        public LineData Add<T>(T[] values)
        {
            if (values == null) return this;
            var nbValue = values.Length;
            if (nbValue == 0) return this;
            for (var i = 0; i < nbValue; ++i)
            {
                base.Add(values[i]);
            }
            return this;
        }

        internal static readonly string[] DoubleFormat = new [] {
                                                                   "0", 
                                                                   "0.0", 
                                                                   "0.00",
                                                                   "0.000",
                                                                   "0.0000",
                                                                   "0.00000",
                                                                   "0.000000",
                                                                   "0.0000000",
                                                                   "0.00000000",
                                                                   "0.000000000",
                                                                   "0.0000000000",
                                                                   "0.00000000000"
                                                               };

        ///<summary>
        ///Add a null value at the end of this raw.
        ///</summary>
        ///<returns>This object.</returns>
        public new LineData AddNull()
        {
            base.AddNull();
            return this;
        }

        ///<summary>
        ///Add a value at the end of this raw.
        ///</summary>
        ///<param name="value">The value to add.</param>
        ///<returns>This object.</returns>
        public LineData Add(LineData lineData)
        {
            var countCopy = lineData.Count;
            for (var i = 0; i < countCopy; ++i)
            {
                Add(lineData[i]);
            }
            return this;
        }

        ///<summary>
        ///Add a null value in the next column of the this line.
        ///</summary>
        ///<returns>This object.</returns>
        public new LineData AddNull(int nbNull)
        {
            base.AddNull(nbNull);
            return this;
        }

        ///<summary>
        ///Add value with the given number of decimal.
        ///</summary>
        ///<param name="value">Value to add.</param>
        ///<param name="nbDecimal">Number of decimal to print.</param>
        ///<returns>This object.</returns>
        public LineData Add(double value, int nbDecimal)
        {
            base.Add(value.ToString(DoubleFormat[nbDecimal]));
            return this;
        }

        ///<summary>
        ///Add value with the given number of decimal scaled with the given scale value.
        ///</summary>
        ///<param name="value">Object to add.</param>
        ///<param name="nbDecimal">Number of decimal to print.</param>
        ///<param name="scale">Scale of the value.</param>
        ///<returns>This object.</returns>
        public LineData Add(double value, int nbDecimal, double scale)
        {
            base.Add((value * scale).ToString(DoubleFormat[nbDecimal]));
            return this;
        }

        ///<summary>
        ///Add date and day of year.
        ///</summary>
        ///<param name="date">Date to add.</param>
        ///<returns>This object.</returns>
        public LineData AddDateDOY(DateTime? date)
        {
            if (!date.HasValue)
            {
                Add("????/??/??");
                Add("?");
            }
            else
            {
                AddDate(date.Value);
                Add(date.Value.DayOfYear);
            }
            return this;
        }

        ///<summary>
        ///Add date.
        ///</summary>
        ///<param name="date">Date to add.</param>
        ///<returns>This object.</returns>
        public LineData AddDate(DateTime date)
        {
            Add(date.ToString("u").Split()[0]);
            return this;
        }

        ///<summary>
        ///Add date.
        ///</summary>
        ///<param name="date">Date to add.</param>
        ///<returns>This object.</returns>
        public LineData AddDate(DateTime? date)
        {
            if (date.HasValue)
            {
                AddDate(date.Value);
            }
            else Add("??/??/????");
            return this;
        }

        ///<summary>
        ///Add date and time.
        ///</summary>
        ///<param name="date">Date time to add.</param>
        ///<returns>This object.</returns>
        public LineData AddDateTime(DateTime date)
        {
            AddDate(date);
            Add(date.Hour.ToString("00") + ":" + date.Minute.ToString("00") + ":" + date.Second.ToString("00"));
            return this;
        }

        ///<summary>
        ///Set the width.
        ///</summary>
        ///<param name="width">Width of the line.</param>
        ///<returns>This object.</returns>
        public LineData SetWidth(int width)
        {
            var columnIndex = width - 1;
            this[columnIndex] = this[columnIndex];
            return this;
        }

        #endregion
    }
}