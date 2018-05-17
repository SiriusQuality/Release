using Sirius.Model.Helper;
using Sirius.Model.Interface;
using System;

namespace Sirius.Model.Physic
{
  public static class Astronomy
  {
    private const double MaxLatitude = 1.15191730631626;
    private const double NumberOfDayInYear = 365.0;
    private const double NumberOfHourInDay = 24.0;

    public static int DayOfYear(DateTime date)
    {
      return Math.Min(date.DayOfYear, 365);
    }

    public static double DayAngle(DateTime date)
    {
      return (double) (Astronomy.DayOfYear(date) * 2) * Math.PI / 365.0;
    }

    private static double SolarDeclination(DateTime date)
    {
      return Astronomy.SolarDeclination(Astronomy.DayOfYear(date));
    }

    internal static double SolarDeclination(int dayOfYear)
    {
      return 0.4093 * Math.Sin(2.0 * Math.PI * (284.0 + (double) dayOfYear) / 365.0);
    }

    public static double SolarElevation(ILocation position, DateTime date, double hour)
    {
      return Astronomy.SolarElevation(Astronomy.BoundedLatitude(position), hour, Astronomy.SolarDeclination(Astronomy.DayOfYear(date)));
    }

    internal static double SolarElevation(double latitude, double hour, double solarDeclination)
    {
      return Math.Acos(Math.Sin(latitude) * Math.Sin(solarDeclination) + Math.Cos(latitude) * Math.Cos(solarDeclination) * Math.Cos(Math.PI / 12.0 * (hour - 12.0)));
    }

    public static double SolarDistanceCorrection(DateTime date)
    {
      return Astronomy.SolarDistanceCorrection(Astronomy.DayAngle(date));
    }

    internal static double SolarDistanceCorrection(double dayAngle)
    {
      return 1.0 + 0.03344 * Math.Cos(dayAngle - 0.048869);
    }

    internal static double BoundedLatitude(ILocation location)
    {
      double latitude = location.Latitude;
      if (latitude <= 0.0)
        return Math.Max(-11.0 * Math.PI / 30.0, Conversion.DegreeToRadian(latitude));
      return Math.Min(11.0 * Math.PI / 30.0, Conversion.DegreeToRadian(latitude));
    }

    public static double DaylightTimeFactor(ILocation position, DateTime date)
    {
      return Astronomy.DaylightTimeFactor(Astronomy.BoundedLatitude(position), Astronomy.SolarDeclination(date));
    }

    internal static double DaylightTimeFactor(double latitude, double solarDeclination)
    {
      return Math.Acos(-Math.Tan(latitude) * Math.Tan(solarDeclination));
    }

    public static double DayLength(ILocation position, DateTime date)
    {
      return Astronomy.DayLength(Astronomy.DaylightTimeFactor(position, date));
    }

    internal static double DayLength(double daylightTimeFactor)
    {
      return 24.0 * daylightTimeFactor / Math.PI;
    }

    public static double DayLengthVisible(ILocation location, DateTime date)
    {
      double num1 = location.Latitude / 57.296;
      double[] numArray1 = new double[3];
      double[] numArray2 = new double[3];
      double[] numArray3 = new double[3];
      double num2 = (double) date.DayOfYear;
      numArray3[1] = num2 == 365.0 ? 1.0 : num2;
      numArray3[2] = numArray3[1] + 1.0 > 365.0 ? 1.0 : numArray3[1] + 1.0;
      numArray3[0] = numArray3[1] - 1.0 < 0.0 ? 365.0 : numArray3[1] - 1.0;
      for (int index = 0; index < 3; ++index)
      {
        double num3 = Math.Asin(0.3978 * Math.Sin(2.0 * Math.PI * (numArray3[index] - 80.0) / 365.0 + 0.0335 * (Math.Sin(2.0 * Math.PI * numArray3[index] / 365.0) - Math.Sin(32.0 * Math.PI / 73.0))));
        double num4 = Math.Acos(-0.01454 / (Math.Cos(num1) * Math.Cos(num3)) - Math.Tan(num1) * Math.Tan(num3));
        numArray2[index] = 24.0 * num4 / Math.PI;
        double num5 = Math.Acos(-0.10453 / (Math.Cos(num1) * Math.Cos(num3)) - Math.Tan(num1) * Math.Tan(num3));
        numArray1[index] = Math.Min(24.0 * num5 / Math.PI, 24.0);
      }
      return numArray2[1];
    }

    public static double LongestDayLength(ILocation location)
    {
      DateTime date = location.Latitude >= 0.0 ? new DateTime(1999, 6, 21) : new DateTime(1999, 12, 21);
      return Astronomy.DayLength(location, date);
    }

    public static double ShortestDayLength(ILocation location)
    {
      DateTime date = location.Latitude < 0.0 ? new DateTime(1999, 6, 21) : new DateTime(1999, 12, 21);
      return Astronomy.DayLength(location, date);
    }

    public static double RelativeSunshineFraction(ILocation position, DateTime date)
    {
      return Astronomy.RelativeSunshineFraction(Astronomy.DaylightTimeFactor(position, date));
    }

    public static double RelativeSunshineFraction(double daylightTimeFactor)
    {
      return daylightTimeFactor / Math.PI;
    }

    public static double ExtraTerrestrialRadiation(ILocation position, DateTime date)
    {
      double latitude = Astronomy.BoundedLatitude(position);
      double dayAngle = Astronomy.DayAngle(date);
      int dayOfYear = Astronomy.DayOfYear(date);
      double solarDistanceCorrection = Astronomy.SolarDistanceCorrection(dayAngle);
      double solarDeclination = Astronomy.SolarDeclination(dayOfYear);
      double daylightTimeFactor = Astronomy.DaylightTimeFactor(latitude, solarDeclination);
      return Astronomy.ExtraTerrestrialRadiation(latitude, solarDistanceCorrection, solarDeclination, daylightTimeFactor);
    }

    internal static double ExtraTerrestrialRadiation(double latitude, double solarDistanceCorrection, double solarDeclination, double daylightTimeFactor)
    {
      return 23.4960442486565 * solarDistanceCorrection * daylightTimeFactor * (Math.Sin(latitude) * Math.Sin(solarDeclination) + Math.Cos(latitude) * Math.Cos(solarDeclination));
    }
  }
}
