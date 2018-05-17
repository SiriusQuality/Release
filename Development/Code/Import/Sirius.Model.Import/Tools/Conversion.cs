using Sirius.Model.Interface;
using Sirius.Model.Physic;
using System;

namespace Sirius.Model.Helper
{
  public static class Conversion
  {
    private const double m2ToHectare = 0.0001;
    private const double hectareTom2 = 10000.0;
    private const double kgTog = 1000.0;
    private const double tTog = 1000000.0;
    private const double gToTon = 1E-06;
    private const double gToKg = 0.001;

    public static double DegreeToRadian(this double degree)
    {
      return degree * (Math.PI / 180.0);
    }

    public static double CelsiusToKelvin(this double celsius)
    {
      return celsius + 273.15;
    }

    public static double KilometerPerDayToMeterPerSecond(this double kmPerDay)
    {
      return kmPerDay * (5.0 / 432.0);
    }

    public static double SunHoursToRadiations(this double sunHours, DateTime date, ILocation position)
    {
      int dayOfYear = Astronomy.DayOfYear(date);
      double dayAngle = Astronomy.DayAngle(date);
      double latitude = Astronomy.BoundedLatitude(position);
      double solarDeclination = Astronomy.SolarDeclination(dayOfYear);
      double daylightTimeFactor = Astronomy.DaylightTimeFactor(latitude, solarDeclination);
      double solarDistanceCorrection = Astronomy.SolarDistanceCorrection(dayAngle);
      double num = Astronomy.DayLength(daylightTimeFactor);
      return Math.Max(0.1, Astronomy.ExtraTerrestrialRadiation(latitude, solarDistanceCorrection, solarDeclination, daylightTimeFactor) * (0.1 + 0.24 * sunHours / num + (0.78 - 0.44 * sunHours / num) * sunHours / num));
    }

    public static double MilibarToKiloPascal(this double milibar)
    {
      return milibar * 0.1;
    }

    public static double cmSquareTomSquare(this double cmSquare)
    {
      return 0.0001 * cmSquare;
    }

    public static double cmTom(this double cm)
    {
      return 0.01 * cm;
    }

    public static double GramPerSquareMeterToTonesPerHectar(this double value)
    {
      return value * 1E-06 / 0.0001;
    }

    public static double GramPerSquareMeterToKgPerHectar(this double value)
    {
      return value * 0.001 / 0.0001;
    }

    public static double KilogramPerHectarToGramPerSquareMeter(this double value)
    {
      return value * 1000.0 / 10000.0;
    }

    public static double TonPerHectarToGramPerSquareMeter(this double value)
    {
      return value * 1000000.0 / 10000.0;
    }

    public static double Domain(double value, double minDomainFrom, double maxDomainFrom, double minDomainTo, double maxDomainTo)
    {
      return minDomainTo + (value - minDomainFrom) * (maxDomainTo - minDomainTo) / (maxDomainFrom - minDomainFrom);
    }

    public struct DomainConverter
    {
      private double minDomainFrom;
      private double minDomainTo;
      private double scale;

      public DomainConverter(double minDomainFrom, double maxDomainFrom, double minDomainTo, double maxDomainTo)
      {
        this.minDomainTo = minDomainTo;
        this.minDomainFrom = minDomainFrom;
        this.scale = (maxDomainTo - minDomainTo) / (maxDomainFrom - minDomainFrom);
      }

      public double Convert(double toConvert)
      {
        return this.minDomainTo + (toConvert - this.minDomainFrom) * this.scale;
      }
    }
  }
}
