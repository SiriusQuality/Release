using System;

namespace Ami.Framework.Tool
{
  public static class Calc
  {
    public static readonly double[] GaussianAbscissa5PointBetween0To1 = new double[5]
    {
      0.046910077030668,
      0.230765344947158,
      0.5,
      0.769234655052842,
      0.953089922969332
    };
    public static readonly double[] GaussianAbscissa3PointBetween0To1 = new double[3]
    {
      0.112701665379258,
      0.5,
      0.887298334620742
    };
    public static readonly double[] GaussianWeight5PointBetween0To1 = new double[5]
    {
      0.118463442528095,
      0.239314335249683,
      64.0 / 225.0,
      0.239314335249683,
      0.118463442528095
    };
    public static readonly double[] GaussianWeight3PointBetween0To1 = new double[3]
    {
      5.0 / 18.0,
      4.0 / 9.0,
      5.0 / 18.0
    };
    private static readonly double[] RespectDigitEpsilon = new double[(int) byte.MaxValue];

    static Calc()
    {
      for (byte index = 0; (int) index < (int) byte.MaxValue; ++index)
        Calc.RespectDigitEpsilon[(int) index] = Math.Pow(10.0, (double) -((int) index + 1));
    }

    public static bool Equals(this double a, double b, double e)
    {
      return Math.Abs(a - b) < e;
    }

    public static bool LessOrEquals(this double a, double b, double e)
    {
      if (a >= b)
        return Calc.Equals(a, b, e);
      return true;
    }

    public static bool Less(this double a, double b, double e)
    {
      if (a < b)
        return !Calc.Equals(a, b, e);
      return false;
    }

    public static bool GreaterOrEquals(this double a, double b, double e)
    {
      if (a <= b)
        return Calc.Equals(a, b, e);
      return true;
    }

    public static bool Greater(this double a, double b, double e)
    {
      if (a > b)
        return !Calc.Equals(a, b, e);
      return false;
    }

    public static double RoundZero(this double a, double e)
    {
      if (!Calc.Equals(a, 0.0, e))
        return a;
      return 0.0;
    }

    public static bool IsBetween(this double value, double min, double max)
    {
      if (value >= min)
        return value <= max;
      return false;
    }

    public static bool IsBetween(this int value, int min, int max)
    {
      if (value >= min)
        return value <= max;
      return false;
    }

    public static bool IsBetween(this double value, double min, double max, double e)
    {
      if (value >= min - e)
        return value <= max + e;
      return false;
    }

    public static bool RespectDigit(this double value, byte digit)
    {
      return Calc.Equals(Math.Round(value, (int) digit), value, Calc.RespectDigitEpsilon[(int) digit]);
    }

    public static double Mean(double a, double b)
    {
      return (a + b) * 0.5;
    }
     
    public static double MeanHourly(double[] hourlyTemperature)
    {
        double sum = Calc.Sum(hourlyTemperature, 0, 24);
        return sum/24;
    }

    public static double Mean(double[] values, int start, int stop)
    {
      double num = Calc.Sum(values, start, stop);
      if (num != 0.0)
        return num / (double) (stop - start);
      return 0.0;
    }

    public static double Mean(short[] values, int start, int stop)
    {
      double num = Calc.Sum(values, start, stop);
      if (num != 0.0)
        return num / (double) (stop - start);
      return 0.0;
    }

    public static double Mean(double[] values1, double[] values2, int start, int stop)
    {
      double num = Calc.Sum(values1, values2, start, stop);
      if (num != 0.0)
        return num / (double) (2 * (stop - start));
      return 0.0;
    }

    public static double Mean(short[] values1, short[] values2, int start, int stop)
    {
      double num = Calc.Sum(values1, values2, start, stop);
      if (num != 0.0)
        return num / (double) (2 * (stop - start));
      return 0.0;
    }

    public static double Sum(double[] values, int start, int stop)
    {
      if (start > stop)
        throw new ArgumentException("Start index must be <= stop index.", "start");
      if (start == stop)
        return 0.0;
      double num = 0.0;
      for (int index = start; index < stop; ++index)
        num += values[index];
      return num;
    }

    public static double Sum(short[] values, int start, int stop)
    {
      if (start > stop)
        throw new ArgumentException("Start index must be <= stop index.", "start");
      if (start == stop)
        return 0.0;
      double num = 0.0;
      for (int index = start; index < stop; ++index)
        num += (double) values[index];
      return num;
    }

    public static double Sum(double[] values1, double[] values2, int start, int stop)
    {
      if (start > stop)
        throw new ArgumentException("Start index must be <= stop index.", "start");
      if (start == stop)
        return 0.0;
      double num = 0.0;
      for (int index = start; index < stop; ++index)
        num = num + values1[index] + values2[index];
      return num;
    }

    public static double Sum(short[] values1, short[] values2, int start, int stop)
    {
        if (start > stop)
        {
            throw new ArgumentException("Start index must be <= stop index.", "start");
        }
        if (start == stop) return 0.0;
        if (stop > Math.Min(values1.Length, values2.Length)) 
        { 
            throw new ArgumentException("You try to reach values past the end of the meteo file");
        }

        double num1 = 0.0;
        for (int index = start; index < stop; ++index)
        {
        short num2 = values1[index];
        short num3 = values2[index];
        num1 = num1 + (double) num2 + (double) num3;
        }
        return num1;
    }

    public static double Snap(double v, double min, double max, double step)
    {
      v = Math.Max(min, Math.Min(v, max));
      double num1 = max - min;
      double num2 = num1 / step + 1.0 - 1.0;
      v = (v - min) / num1;
      v *= num2;
      v = Math.Round(v);
      v /= num2;
      v = min + v * num1;
      return v;
    }

    public static int Compare(double a, double b, double e)
    {
      double num = a - b;
      if (Math.Abs(num) < e)
        return 0;
      return num <= 0.0 ? -1 : 1;
    }

    public static double GaussianIntegration5PointsBetween0To1(Func<double, double> f)
    {
      return f(Calc.GaussianAbscissa5PointBetween0To1[0]) * Calc.GaussianWeight5PointBetween0To1[0] + f(Calc.GaussianAbscissa5PointBetween0To1[1]) * Calc.GaussianWeight5PointBetween0To1[1] + f(Calc.GaussianAbscissa5PointBetween0To1[2]) * Calc.GaussianWeight5PointBetween0To1[2] + f(Calc.GaussianAbscissa5PointBetween0To1[3]) * Calc.GaussianWeight5PointBetween0To1[3] + f(Calc.GaussianAbscissa5PointBetween0To1[4]) * Calc.GaussianWeight5PointBetween0To1[4];
    }

    public static double GaussianIntegration3PointsBetween0To1(Func<double, double> f)
    {
      return f(Calc.GaussianAbscissa3PointBetween0To1[0]) * Calc.GaussianWeight3PointBetween0To1[0] + f(Calc.GaussianAbscissa3PointBetween0To1[1]) * Calc.GaussianWeight3PointBetween0To1[1] + f(Calc.GaussianAbscissa3PointBetween0To1[2]) * Calc.GaussianWeight3PointBetween0To1[2];
    }

    public static double GaussianIntegration5PointsBetween0ToX(Func<double, double> f, double x)
    {
      return Calc.GaussianIntegration5PointsBetween0To1((Func<double, double>) (xx => f(xx * x))) * x;
    }

    public static double GaussianIntegration3PointsBetween0ToX(Func<double, double> f, double x)
    {
      return Calc.GaussianIntegration3PointsBetween0To1((Func<double, double>) (xx => f(xx * x))) * x;
    }

    public static void Norm(this double[] vector)
    {
      int length = vector.Length;
      double d = 0.0;
      for (int index = 0; index < length; ++index)
      {
        double num = vector[index];
        d += num * num;
      }
      double num1 = Math.Sqrt(d);
      for (int index = 0; index < length; ++index)
        vector[index] /= num1;
    }
  }
}
