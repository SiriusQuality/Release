using System;
using System.Diagnostics;

namespace SiriusModel
{
    ///<summary>
    ///Check method used in debug mode.
    ///</summary>
    public static class Check
    {
        #region GetMethodName

        ///<summary>
        ///Get the method name to make exception clearer.
        ///</summary>
        ///<returns></returns>
        private static string GetMethodName()
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(2).GetMethod();
            return method.ReflectedType.Name + ", " + method.Name;
        }

        #endregion

        #region IsNumber

        ///<summary>
        ///Check a given value is number, i.e not NaN or +/-Infinity.
        ///</summary>
        ///<param name="value">The value to check.</param>
        [Conditional("DEBUG")]
        public static void IsNumber(double value)
        {
           // Console.WriteLine("valeur" + value);
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new ArgumentOutOfRangeException("value", GetMethodName() + ": is not a number." );
            }
        }

        #endregion

        #region IsPositive

        ///<summary>
        ///Check a number is positive.
        ///</summary>
        ///<param name="value">The value to check.</param>
        [Conditional("DEBUG")]
        public static void IsPositive(double value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException("value", GetMethodName() + ": is not positive.");
            }
        }

        ///<summary>
        ///Check a number is positive.
        ///</summary>
        ///<param name="value">The value to check.</param>
        [Conditional("DEBUG")]
        public static void IsPositive(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException("value", GetMethodName() + ": is not positive.");
            }
        }

        #endregion

        #region IsPositiveOrZero

        ///<summary>
        ///Check a number is positive or zero.
        ///</summary>
        ///<param name="value">The value to check.</param>
        [Conditional("DEBUG")]
        public static void IsPositiveOrZero(double value)
        {
            if (value < 0)
            {
                Console.WriteLine("value" + value);
                throw new ArgumentOutOfRangeException("value", GetMethodName() + ": is not positive or zero.");
            }
        }

        ///<summary>
        ///Check a number is positive or zero.
        ///</summary>
        ///<param name="value">The value to check.</param>
        [Conditional("DEBUG")]
        public static void IsPositiveOrZero(int value)
        {
            if (value < 0)
            {                
                throw new ArgumentOutOfRangeException("value", GetMethodName() + ": is not positive or zero.");
            }
        }

        #endregion

        #region IsLessOrEqual

        ///<summary>
        ///Check a number is less than an other maximal value.
        ///</summary>
        ///<param name="value">The value to check.</param>
        ///<param name="max">The maximal value.</param>
        [Conditional("DEBUG")]
        public static void IsLessOrEqual(double value, double max)
        {
            if (value > max + 1e-4)
            {
                throw new ArgumentOutOfRangeException("value", GetMethodName() + ": is greater than " + max + ".");
            }
        }

        ///<summary>
        ///Check a number is less than an other maximal value.
        ///</summary>
        ///<param name="value">The value to check.</param>
        ///<param name="max">The maximal value.</param>
        ///<param name="e">Epsilon precision.</param>
        [Conditional("DEBUG")]
        public static void IsLessOrEqual(double value, double max, double e)
        {
            if (value > max + e)
            {
                throw new ArgumentOutOfRangeException("value", GetMethodName() + ": is greater than " + max + ".");
            }
        }

        #endregion

        #region IsZero

        ///<summary>
        ///Check the given value is zero.
        ///</summary>
        ///<param name="value">The value to check.</param>
        [Conditional("DEBUG")]
        public static void IsZero(double value)
        {
            if (value != 0)
            {
                throw new ArgumentOutOfRangeException("value", GetMethodName() + ": is not zero.");
            }
        }

        #endregion

        #region IsEqual

        ///<summary>
        ///Check two values are equal with a given precision.
        ///</summary>
        ///<param name="a">The first value to check.</param>
        ///<param name="b">The second value to check.</param>
        ///<param name="e">Epsilon precision.</param>
        [Conditional("DEBUG")]
        public static void IsEqual(double a, double b, double e)
        {
            if (Math.Abs(a - b) > e)
            {
                throw new ArgumentOutOfRangeException("a", GetMethodName() + ": is not equal to: " + b + ".");
            }
        }

        #endregion

        #region IsNotNull

        ///<summary>
        ///Check a given reference is not null.
        ///</summary>
        ///<param name="toTest">The reference to test.</param>
        public static void IsNotNull(object toTest)
        {
            if (toTest == null)
            {
                throw new ArgumentNullException("toTest", GetMethodName() + ": is null.");
            }
        }

        #endregion
    }
}