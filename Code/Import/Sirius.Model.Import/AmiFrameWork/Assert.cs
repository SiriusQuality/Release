using System;
using System.Runtime.Serialization;

namespace Ami.Framework.Tool
{
  public static class Assert
  {
    public static void AssertIsNotNull<T>(this T a, string message) where T : class
    {
      if ((object) a == null)
        throw new Assert.AssertException(message);
    }

   public static void AssertIsNotNullBlock<T>(this T[] a, string message) where T : class
    {
            if ((object)a == null)
                throw new Assert.AssertException(message);
    }

        public static void AssertIsPositive(this double a, string message)
    {
      if (a <= 0.0)
        throw new Assert.AssertException(message);
    }

    public static void AssertIsPositiveOrZero(this double a, string message)
    {
      if (a < 0.0)
        throw new Assert.AssertException(message);
    }

    public static void AssertIsBetween(this double value, double min, double max, double e, string message)
    {
        if (!Calc.IsBetween(value, min, max, e))
            throw new Assert.AssertException(message);
    }

    [Serializable]
    public class AssertException : Exception
    {
      internal AssertException(string message)
        : base(message)
      {
      }

      protected AssertException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
  }
}
