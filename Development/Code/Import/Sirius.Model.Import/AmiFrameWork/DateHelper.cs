using System;

namespace Ami.Framework.Tool
{
  public static class DateHelper
  {
    public static readonly DateTime NaD = new DateTime();
    private static readonly int[] MonthOfDoyLeap = new int[335];
    private static readonly int[] DayOfDoyLeap = new int[335];
    private static readonly int[] MonthOfDoyNotLeap = new int[334];
    private static readonly int[] DayOfDoyNotLeap = new int[334];

    static DateHelper()
    {
      DateTime dateTime1 = new DateTime(2000, 2, 1);
      for (int index = 0; index < 335; ++index)
      {
        DateHelper.MonthOfDoyLeap[index] = dateTime1.Month;
        DateHelper.DayOfDoyLeap[index] = dateTime1.Day;
        dateTime1 = dateTime1.AddDays(1.0);
      }
      DateTime dateTime2 = new DateTime(2001, 2, 1);
      for (int index = 0; index < 334; ++index)
      {
        DateHelper.MonthOfDoyNotLeap[index] = dateTime2.Month;
        DateHelper.DayOfDoyNotLeap[index] = dateTime2.Day;
        dateTime2 = dateTime2.AddDays(1.0);
      }
    }

    public static DateTime Date(int year, int dayOfYear)
    {
      if (dayOfYear <= 0)
        throw new ArgumentOutOfRangeException("dayOfYear", "Day of year must be a positive number.");
      if (dayOfYear > 366)
        throw new ArgumentOutOfRangeException("dayOfYear", "Day of year must be less than 366.");
      if (dayOfYear >= 1 && dayOfYear <= 31)
        return new DateTime(year, 1, dayOfYear);
      dayOfYear -= 32;
      if (DateTime.IsLeapYear(year))
        return new DateTime(year, DateHelper.MonthOfDoyLeap[dayOfYear], DateHelper.DayOfDoyLeap[dayOfYear]);
      if (dayOfYear == 366)
        throw new ArgumentOutOfRangeException("dayOfYear", "Day of year must be less than 365 as the year " + (object) year + " is not a leap year.");
      return new DateTime(year, DateHelper.MonthOfDoyNotLeap[dayOfYear], DateHelper.DayOfDoyNotLeap[dayOfYear]);
    }

    public static DateTime Date(int year, int month, int dayOfMonth)
    {
      return new DateTime(year, month, dayOfMonth);
    }

    public static string DateToString(this DateTime dateTime)
    {
      return dateTime.Day.ToString("00") + "/" + dateTime.Month.ToString("00") + "/" + dateTime.Year.ToString("0000");
    }

    public static DateTime StringToDate(string date)
    {
      return new DateTime(int.Parse(date.Substring(6, 4)), int.Parse(date.Substring(3, 2)), int.Parse(date.Substring(0, 2)));
    }

    public static bool IsBetween(DateTime value, DateTime min, DateTime max)
    {
      if (value >= min)
        return value <= max;
      return false;
    }

    public static bool DateEquals(this DateTime date, DateTime other)
    {
      if (date.Year == other.Year && date.Month == other.Month)
        return date.Day == other.Day;
      return false;
    }

    public static bool IsNaD(this DateTime dateTime)
    {
      return dateTime == DateHelper.NaD;
    }
  }
}
