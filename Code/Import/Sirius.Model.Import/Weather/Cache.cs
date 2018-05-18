using Ami.Framework.Collection;
using Ami.Framework.Tool;
using Sirius.Model.Constant;
using Sirius.Model.Helper;
using Sirius.Model.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sirius.Model.Weather
{
  public class Cache
  {

    private TemperatureConverter temperatureConverter;
    private static readonly double ScaleInRadiation = 10.0;
    private static readonly double ScaleInRain = 10.0;
    private static readonly double ScaleInTemperature = 100.0;
    private static readonly double ScaleInVapourPresure = 100.0;
    private static readonly double ScaleInWind = 1.0;
    private static readonly double ScaleOutRadiation = 0.1;
    private static readonly double ScaleOutRain = 0.1;
    private static readonly double ScaleOutTemperature = 0.01;
    private static readonly double ScaleOutVapourPresure = 0.01;
    private static readonly double ScaleOutWind = 1.0;
    private static readonly Cache<Cache.DataKey, Cache> CacheData = new Cache<Cache.DataKey, Cache>((Func<Cache.DataKey, Cache>) (key => new Cache(key.location, key.input)), (IEqualityComparer<Cache.DataKey>) new Cache.DataKeyEqualityComparer());
    private static readonly string ReadDateOperation = "read date";
    private static readonly string ReadRadiationOperation = "read radiation";
    private static readonly string ReadRainOperation = "read rain";
    private static readonly string ReadTempMaxOperation = "read temperature max";
    private static readonly string ReadTempMinOperation = "read temperature min";
    private static readonly string ReadTempHourlyOperation = "read hourly temperature";
    private static readonly string ReadVapourPresureOperation = "read vapour presure";
    private static readonly string ReadWindOperation = "read wind";
    private const int AllocSize = 36600;
    private const double Epsilon = 1E-06;
    private const int InterpolateCount = 5;
    private const double TextMissingValue = -99.0;
    private const byte DigitRadiation = 1;
    private const byte DigitRain = 1;
    private const byte DigitTemperature = 2;
    private const byte DigitVapourPresure = 2;
    private const byte DigitWind = 0;
    private const double MinRadiation = 0.1;
    private const double MinRain = 0.0;
    private const double MinSunHours = 0.0;
    private const double MinTemperature = -90.0;
    private const double MinVapourPresure = 0.0;
    private const double MinWind = 0.0;
    internal const double MaxRadiation = 50.0;
    private const double MaxRain = 400.0;
    private const double MaxSunHours = 24.0;
    private const double MaxTemperature = 70.0;
    private const double MaxVapourPresure = 60.0;
    private const double MaxWind = 3000.0;
    private readonly double[] bufferRead;
    private readonly IEnumerable<string> fileEnum;
    private readonly IWeatherFormat format;
    private readonly ILocation location;
    private readonly NumericReader numericReader;
    private int count;
    private DateTime? currentDate;
    private string currentOperation;
    private DateTime? lastDate;
    private int maxConsecutiveMissingValuesAllowed;
    private DateTime? startDate;
    private short[] radiationArray;
    private short[] rainArray;
    private short[] tempMaxArray;
    private short[] tempMinArray;
    private short[] tempH0Array;
    private short[] tempH1Array;
    private short[] tempH2Array;
    private short[] tempH3Array;
    private short[] tempH4Array;
    private short[] tempH5Array;
    private short[] tempH6Array;
    private short[] tempH7Array;
    private short[] tempH8Array;
    private short[] tempH9Array;
    private short[] tempH10Array;
    private short[] tempH11Array;
    private short[] tempH12Array;
    private short[] tempH13Array;
    private short[] tempH14Array;
    private short[] tempH15Array;
    private short[] tempH16Array;
    private short[] tempH17Array;
    private short[] tempH18Array;
    private short[] tempH19Array;
    private short[] tempH20Array;
    private short[] tempH21Array;
    private short[] tempH22Array;
    private short[] tempH23Array;
    private short[] vapourPresureArray;
    private short[] windArray;
    private byte radiationMissing;
    private byte rainMissing;
    private byte tempMaxMissing;
    private byte tempMinMissing;
    private byte vapourPresureMissing;
    private byte windMissing;
    private byte tempHourlyMissing;
    private readonly int dayColumn;
    private readonly int doyColumn;
    private readonly int monthColumn;
    private readonly int radiationColumn;
    private readonly int rainColumn;
    private readonly int sunHoursColumn;
    private readonly int tempMaxColumn;
    private readonly int tempMinColumn;
    private readonly int tempH0Column;
    private readonly int tempH1Column;
    private readonly int tempH2Column;
    private readonly int tempH3Column;
    private readonly int tempH4Column;
    private readonly int tempH5Column;
    private readonly int tempH6Column;
    private readonly int tempH7Column;
    private readonly int tempH8Column;
    private readonly int tempH9Column;
    private readonly int tempH10Column;
    private readonly int tempH11Column;
    private readonly int tempH12Column;
    private readonly int tempH13Column;
    private readonly int tempH14Column;
    private readonly int tempH15Column;
    private readonly int tempH16Column;
    private readonly int tempH17Column;
    private readonly int tempH18Column;
    private readonly int tempH19Column;
    private readonly int tempH20Column;
    private readonly int tempH21Column;
    private readonly int tempH22Column;
    private readonly int tempH23Column;

    private readonly int vapourPresureColumn;
    private readonly int windColumn;
    private readonly int yearColumn;

    public DateTime? Start
    {
      get
      {
        return this.startDate;
      }
    }

    public int Count
    {
      get
      {
        return this.count;
      }
    }

    public DateTime? End
    {
      get
      {
        if (!this.startDate.HasValue)
          return new DateTime?();
        return new DateTime?(this.startDate.Value.AddDays((double) (this.count - 1)));
      }
    }

    public bool VapourPresureDefined
    {
      get
      {
        return this.vapourPresureArray != null;
      }
    }

    public bool WindDefined
    {
      get
      {
        return this.windArray != null;
      }
    }

    private Cache(ILocation dataLocation, IWeatherInput input)
    {
      this.location = dataLocation;
      this.fileEnum = (IEnumerable<string>) input.Files;
      this.format = input.Format;
      this.maxConsecutiveMissingValuesAllowed = input.MaxConsecutiveMissingValuesAllowed;

      this.dayColumn = this.doyColumn = this.monthColumn = this.radiationColumn = this.rainColumn = this.tempMaxColumn = this.tempMinColumn = this.sunHoursColumn = this.vapourPresureColumn = this.windColumn = this.yearColumn = -1;
      this.tempMaxColumn = this.tempMinColumn = this.tempH0Column = this.tempH1Column = this.tempH2Column = this.tempH3Column = this.tempH4Column = this.tempH5Column = 
      this.tempH6Column = this.tempH7Column = this.tempH8Column = this.tempH9Column = this.tempH10Column = this.tempH11Column = this.tempH12Column = this.tempH13Column =
      this.tempH14Column = this.tempH15Column = this.tempH16Column = this.tempH17Column = this.tempH18Column = this.tempH19Column = this.tempH20Column = this.tempH21Column =
      this.tempH22Column = this.tempH23Column = -1;

      temperatureConverter =new TemperatureConverter();

      this.bufferRead = new double[0];
      int index = 0;
      foreach (WeatherFormatID weatherFormatId in (IEnumerable<WeatherFormatID>) this.format)
      {
        switch (weatherFormatId)
        {
          case WeatherFormatID.Day:
            Cache.AddDateColumn(index, ref this.bufferRead, ref this.dayColumn);
            break;
          case WeatherFormatID.DayOfYear:
            Cache.AddDateColumn(index, ref this.bufferRead, ref this.doyColumn);
            break;
          case WeatherFormatID.Month:
            Cache.AddDateColumn(index, ref this.bufferRead, ref this.monthColumn);
            break;
          case WeatherFormatID.Radiation:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.radiationArray, ref this.radiationColumn);
            break;
          case WeatherFormatID.Rain:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.rainArray, ref this.rainColumn);
            break;
          case WeatherFormatID.SunHours:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.radiationArray, ref this.sunHoursColumn);
            break;
          case WeatherFormatID.TempMax:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempMaxArray, ref this.tempMaxColumn);
            break;
          case WeatherFormatID.TempMin:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempMinArray, ref this.tempMinColumn);
            break;
          case WeatherFormatID.Unknown:
            Cache.AddEmptyColumn(ref this.bufferRead);
            break;
          case WeatherFormatID.VapourPresure:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.vapourPresureArray, ref this.vapourPresureColumn);
            break;
          case WeatherFormatID.Wind:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.windArray, ref this.windColumn);
            break;
          case WeatherFormatID.Year:
            Cache.AddDateColumn(index, ref this.bufferRead, ref this.yearColumn);
            break;
          case WeatherFormatID.TempH0:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH0Array, ref this.tempH0Column);
            break;
          case WeatherFormatID.TempH1:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH1Array, ref this.tempH1Column);
            break;
          case WeatherFormatID.TempH2:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH2Array, ref this.tempH2Column);
            break;
          case WeatherFormatID.TempH3:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH3Array, ref this.tempH3Column);
            break;
          case WeatherFormatID.TempH4:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH4Array, ref this.tempH4Column);
            break;
          case WeatherFormatID.TempH5:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH5Array, ref this.tempH5Column);
            break;
          case WeatherFormatID.TempH6:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH6Array, ref this.tempH6Column);
            break;
          case WeatherFormatID.TempH7:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH7Array, ref this.tempH7Column);
            break;
          case WeatherFormatID.TempH8:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH8Array, ref this.tempH8Column);
            break;
          case WeatherFormatID.TempH9:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH9Array, ref this.tempH9Column);
            break;
          case WeatherFormatID.TempH10:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH10Array, ref this.tempH10Column);
            break;
          case WeatherFormatID.TempH11:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH11Array, ref this.tempH11Column);
            break;
          case WeatherFormatID.TempH12:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH12Array, ref this.tempH12Column);
            break;
          case WeatherFormatID.TempH13:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH13Array, ref this.tempH13Column);
            break;
          case WeatherFormatID.TempH14:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH14Array, ref this.tempH14Column);
            break;
          case WeatherFormatID.TempH15:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH15Array, ref this.tempH15Column);
            break;
          case WeatherFormatID.TempH16:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH16Array, ref this.tempH16Column);
            break;
          case WeatherFormatID.TempH17:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH17Array, ref this.tempH17Column);
            break;
          case WeatherFormatID.TempH18:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH18Array, ref this.tempH18Column);
            break;
          case WeatherFormatID.TempH19:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH19Array, ref this.tempH19Column);
            break;
          case WeatherFormatID.TempH20:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH20Array, ref this.tempH20Column);
            break;
          case WeatherFormatID.TempH21:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH21Array, ref this.tempH21Column);
            break;
          case WeatherFormatID.TempH22:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH22Array, ref this.tempH22Column);
            break;
          case WeatherFormatID.TempH23:
            Cache.AddValueColumn(index, ref this.bufferRead, ref this.tempH23Array, ref this.tempH23Column);
            break;
          default:
            throw new InvalidOperationException("Unknow weather format ID: " + (object) weatherFormatId);
        }
        ++index;
      }
      this.numericReader = new NumericReader(this.bufferRead, new Action<NumericReader, NumericParser, bool, bool, bool>(this.OnLine), new Action<NumericReader, Exception>(this.OnException));
      this.numericReader.Read(this.fileEnum);
      this.ResizeColumn(ref this.radiationArray);
      this.ResizeColumn(ref this.rainArray);
      this.ResizeColumn(ref this.tempMaxArray);
      this.ResizeColumn(ref this.tempMinArray);
      this.ResizeColumn(ref this.vapourPresureArray);
      this.ResizeColumn(ref this.windArray);
        this.ResizeColumn(ref this.tempH0Array);
        this.ResizeColumn(ref this.tempH1Array);
        this.ResizeColumn(ref this.tempH2Array);
        this.ResizeColumn(ref this.tempH3Array);
        this.ResizeColumn(ref this.tempH4Array);
        this.ResizeColumn(ref this.tempH5Array);
        this.ResizeColumn(ref this.tempH6Array);
        this.ResizeColumn(ref this.tempH7Array);
        this.ResizeColumn(ref this.tempH8Array);
        this.ResizeColumn(ref this.tempH9Array);
        this.ResizeColumn(ref this.tempH10Array);
        this.ResizeColumn(ref this.tempH11Array);
        this.ResizeColumn(ref this.tempH12Array);
        this.ResizeColumn(ref this.tempH13Array);
        this.ResizeColumn(ref this.tempH14Array);
        this.ResizeColumn(ref this.tempH15Array);
        this.ResizeColumn(ref this.tempH16Array);
        this.ResizeColumn(ref this.tempH17Array);
        this.ResizeColumn(ref this.tempH18Array);
        this.ResizeColumn(ref this.tempH19Array);
        this.ResizeColumn(ref this.tempH20Array);
        this.ResizeColumn(ref this.tempH21Array);
        this.ResizeColumn(ref this.tempH22Array);
        this.ResizeColumn(ref this.tempH23Array);
      this.numericReader = (NumericReader) null;
    }

    private static void AddDateColumn(int index, ref double[] bufferRead, ref int columnIndex)
    {
      columnIndex = index;
      CollectionHelper.Add<double>(ref bufferRead, 0.0);
    }

    private static void AddValueColumn(int index, ref double[] bufferRead, ref short[] valueArray, ref int columnIndex)
    {
      columnIndex = index;
      CollectionHelper.Add<double>(ref bufferRead, 0.0);
      if (valueArray != null)
        return;
      valueArray = new short[36600];
    }

    private static void AddEmptyColumn(ref double[] bufferRead)
    {
      CollectionHelper.Add<double>(ref bufferRead, 0.0);
    }

    private void ResizeColumn(ref short[] array)
    {
      if (array == null)
        return;
      Array.Resize<short>(ref array, this.count);
    }

    private void OnLine(NumericReader nR, NumericParser numericParser, bool lineIsEmpty, bool tooManyValues, bool tooFewValues)
    {
      this.currentOperation = (string) null;
      if (lineIsEmpty)
        return;
      double radiation;
      double rain;
      double sunHours;
      double temperatureMax = -1 ;
      double temperatureMin = -1; ;
      double[] temperatureH = new double[24];

      double vapourPresure;
      double wind;
      int day;
      int doy;
      int month;
      int year;

      bool hourlyTemp = (this.tempMaxColumn == -1 && this.tempMinColumn == -1); 

      if (!tooFewValues)
      {
        radiation = this.GetDoubleValue(this.radiationColumn);
        rain = this.GetDoubleValue(this.rainColumn);
        sunHours = this.GetDoubleValue(this.sunHoursColumn);
        if (!hourlyTemp)
        {
            temperatureMax = this.GetDoubleValue(this.tempMaxColumn);
            temperatureMin = this.GetDoubleValue(this.tempMinColumn);
        }
        else
        {
            temperatureH[0]= this.GetDoubleValue(this.tempH0Column);
            temperatureH[1] = this.GetDoubleValue(this.tempH1Column);
            temperatureH[2] = this.GetDoubleValue(this.tempH2Column);
            temperatureH[3] = this.GetDoubleValue(this.tempH3Column);
            temperatureH[4] = this.GetDoubleValue(this.tempH4Column);
            temperatureH[5] = this.GetDoubleValue(this.tempH5Column);
            temperatureH[6] = this.GetDoubleValue(this.tempH6Column);
            temperatureH[7] = this.GetDoubleValue(this.tempH7Column);
            temperatureH[8] = this.GetDoubleValue(this.tempH8Column);
            temperatureH[9] = this.GetDoubleValue(this.tempH9Column);
            temperatureH[10] = this.GetDoubleValue(this.tempH10Column);
            temperatureH[11] = this.GetDoubleValue(this.tempH11Column);
            temperatureH[12] = this.GetDoubleValue(this.tempH12Column);
            temperatureH[13] = this.GetDoubleValue(this.tempH13Column);
            temperatureH[14] = this.GetDoubleValue(this.tempH14Column);
            temperatureH[15] = this.GetDoubleValue(this.tempH15Column);
            temperatureH[16] = this.GetDoubleValue(this.tempH16Column);
            temperatureH[17] = this.GetDoubleValue(this.tempH17Column);
            temperatureH[18] = this.GetDoubleValue(this.tempH18Column);
            temperatureH[19] = this.GetDoubleValue(this.tempH19Column);
            temperatureH[20] = this.GetDoubleValue(this.tempH20Column);
            temperatureH[21] = this.GetDoubleValue(this.tempH21Column);
            temperatureH[22] = this.GetDoubleValue(this.tempH22Column);
            temperatureH[23] = this.GetDoubleValue(this.tempH23Column);
        }
        vapourPresure = this.GetDoubleValue(this.vapourPresureColumn);
        wind = this.GetDoubleValue(this.windColumn);
        day = this.GetIntValue(this.dayColumn);
        doy = this.GetIntValue(this.doyColumn);
        month = this.GetIntValue(this.monthColumn);
        year = this.GetIntValue(this.yearColumn);
      }
      else
      {
        radiation = double.NaN;
        rain = double.NaN;
        sunHours = double.NaN;
        if (!hourlyTemp)
        {
            temperatureMax = double.NaN;
            temperatureMin = double.NaN;
        }
        else
        {
            temperatureH[0] = double.NaN;
            temperatureH[1] = double.NaN;
            temperatureH[2] = double.NaN;
            temperatureH[3] = double.NaN;
            temperatureH[4] = double.NaN;
            temperatureH[5] = double.NaN;
            temperatureH[6] = double.NaN;
            temperatureH[7] = double.NaN;
            temperatureH[8] = double.NaN;
            temperatureH[9] = double.NaN;
            temperatureH[10] = double.NaN;
            temperatureH[11] = double.NaN;
            temperatureH[12] = double.NaN;
            temperatureH[13] = double.NaN;
            temperatureH[14] = double.NaN;
            temperatureH[15] = double.NaN;
            temperatureH[16] = double.NaN;
            temperatureH[17] = double.NaN;
            temperatureH[18] = double.NaN;
            temperatureH[19] = double.NaN;
            temperatureH[20] = double.NaN;
            temperatureH[21] = double.NaN;
            temperatureH[22] = double.NaN;
            temperatureH[23] = double.NaN;
        }
        vapourPresure = double.NaN;
        wind = double.NaN;
        day = int.MinValue;
        doy = int.MinValue;
        month = int.MinValue;
        year = int.MinValue;
      }

      if (!hourlyTemp)
      {
          if (this.doyColumn != -1)
              this.ProcessLine(year, doy, radiation, rain, temperatureMax, temperatureMin, sunHours, vapourPresure, wind);
          else
              this.ProcessLine(year, month, day, radiation, rain, temperatureMax, temperatureMin, sunHours, vapourPresure, wind);
      }
      else
      {
          if (this.doyColumn != -1)
              this.ProcessLine(year, doy, radiation, rain, temperatureH, sunHours, vapourPresure, wind);
          else
              this.ProcessLine(year, month, day, radiation, rain, temperatureH, sunHours, vapourPresure, wind);
      }
    }

    private double GetDoubleValue(int indexColumn)
    {
      if (indexColumn == -1)
        return double.NaN;
      double a = this.bufferRead[indexColumn];
      if (Calc.Equals(a, -99.0, 1E-06))
        a = double.NaN;
      return a;
    }

    private int GetIntValue(int indexColumn)
    {
      double doubleValue = this.GetDoubleValue(indexColumn);
      if (!double.IsNaN(doubleValue) && Calc.RespectDigit(doubleValue, (byte) 0))
        return (int) doubleValue;
      return int.MinValue;
    }

    private void ProcessLine(int year, int doy, double radiation, double rain, double temperatureMax, double temperatureMin, double sunHours, double vapourPresure, double wind)
    {
      this.currentOperation = Cache.ReadDateOperation;
      this.currentDate = new DateTime?();
      if (year != int.MinValue && doy != int.MinValue)
        this.currentDate = new DateTime?(DateHelper.Date(year, doy));
      this.ProcessLine(radiation, rain, temperatureMax, temperatureMin, sunHours, vapourPresure, wind);
    }

    private void ProcessLine(int year, int month, int day, double radiation, double rain, double temperatureMax, double temperatureMin, double sunHours, double vapourPresure, double wind)
    {
      this.currentOperation = Cache.ReadDateOperation;
      this.currentDate = new DateTime?();
      if (year != int.MinValue && month != int.MinValue && day != int.MinValue)
        this.currentDate = new DateTime?(new DateTime(year, month, day));
      this.ProcessLine(radiation, rain, temperatureMax, temperatureMin, sunHours, vapourPresure, wind);
    }

    private void ProcessLine(int year, int doy, double radiation, double rain, double[] temperatureH, double sunHours, double vapourPresure, double wind)
    {
        this.currentOperation = Cache.ReadDateOperation;
        this.currentDate = new DateTime?();
        if (year != int.MinValue && doy != int.MinValue)
            this.currentDate = new DateTime?(DateHelper.Date(year, doy));
        this.ProcessLine(radiation, rain, temperatureH, sunHours, vapourPresure, wind);
    }

    private void ProcessLine(int year, int month, int day, double radiation, double rain, double[] temperatureH, double sunHours, double vapourPresure, double wind)
    {
        this.currentOperation = Cache.ReadDateOperation;
        this.currentDate = new DateTime?();
        if (year != int.MinValue && month != int.MinValue && day != int.MinValue)
            this.currentDate = new DateTime?(new DateTime(year, month, day));
        this.ProcessLine(radiation, rain, temperatureH, sunHours, vapourPresure, wind);
    }

    private void ProcessLine(double radiation, double rain, double temperatureMax, double temperatureMin, double sunHours, double vapourPresure, double wind)
    {
      if (this.lastDate.HasValue && !this.currentDate.HasValue)
      {
        this.currentDate = new DateTime?(this.lastDate.Value.AddDays(1.0));
        double num;
        wind = num = double.NaN;
        vapourPresure = num;
        sunHours = num;
        temperatureMin = num;
        temperatureMax = num;
        rain = num;
        radiation = num;
      }
      if (!this.currentDate.HasValue)
        throw new FileFormatException("First line of the weather file enumeration is corrupted.");
      if (this.lastDate.HasValue)
      {
        DateTime? nullable1 = this.currentDate;
        DateTime? nullable2 = this.lastDate;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          if (this.numericReader.CurrentLineNumbber < 2)
            throw new FileFormatException("Date are not ordered (current = " + DateHelper.DateToString(this.currentDate.Value) + ", previous = " + DateHelper.DateToString(this.lastDate.Value) + "). Check the end and beginning of your files.");
          throw new FileFormatException("Date are not ordered (current = " + DateHelper.DateToString(this.currentDate.Value) + ", previous = " + DateHelper.DateToString(this.lastDate.Value) + ").");
        }
        DateTime? nullable3 = this.currentDate;
        DateTime dateTime = this.lastDate.Value.AddDays(1.0);
        if ((!nullable3.HasValue ? 0 : (nullable3.GetValueOrDefault() == dateTime ? 1 : 0)) != 0)
        {
          this.ProcessValues(radiation, rain, temperatureMax, temperatureMin, sunHours, vapourPresure, wind);
        }
        else
        {
          int num = (int) (this.currentDate.Value - this.lastDate.Value).TotalDays - 1;
          for (int index = 0; index < num; ++index)
            this.ProcessValues(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN);
          this.ProcessValues(radiation, rain, temperatureMax, temperatureMin, sunHours, vapourPresure, wind);
        }
      }
      else
      {
        this.startDate = this.currentDate;
        this.ProcessValues(radiation, rain, temperatureMax, temperatureMin, sunHours, vapourPresure, wind);
      }
      this.lastDate = this.currentDate;
    }

    private void ProcessLine(double radiation, double rain, double[] temperatureH, double sunHours, double vapourPresure, double wind)
    {
        if (this.lastDate.HasValue && !this.currentDate.HasValue)
        {
            this.currentDate = new DateTime?(this.lastDate.Value.AddDays(1.0));
            wind = double.NaN;
            vapourPresure = double.NaN;
            sunHours = double.NaN;
            for (int i = 0; i < 24; i++) { temperatureH[i] = double.NaN; }
            rain = double.NaN;
            radiation = double.NaN;
        }
        if (!this.currentDate.HasValue)
            throw new FileFormatException("First line of the weather file enumeration is corrupted.");
        if (this.lastDate.HasValue)
        {
            DateTime? nullable1 = this.currentDate;
            DateTime? nullable2 = this.lastDate;
            if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            {
                if (this.numericReader.CurrentLineNumbber < 2)
                    throw new FileFormatException("Date are not ordered (current = " + DateHelper.DateToString(this.currentDate.Value) + ", previous = " + DateHelper.DateToString(this.lastDate.Value) + "). Check the end and beginning of your files.");
                throw new FileFormatException("Date are not ordered (current = " + DateHelper.DateToString(this.currentDate.Value) + ", previous = " + DateHelper.DateToString(this.lastDate.Value) + ").");
            }
            DateTime? nullable3 = this.currentDate;
            DateTime dateTime = this.lastDate.Value.AddDays(1.0);
            if ((!nullable3.HasValue ? 0 : (nullable3.GetValueOrDefault() == dateTime ? 1 : 0)) != 0)
            {
                this.ProcessValues(radiation, rain, temperatureH, sunHours, vapourPresure, wind);
            }
            else
            {
                int num = (int)(this.currentDate.Value - this.lastDate.Value).TotalDays - 1;
                for (int index = 0; index < num; ++index)
                {
                    double[] emptyHtemp = new double[24];
                    for (int i = 0; i < 24; i++) { emptyHtemp[i] = double.NaN; }
                    this.ProcessValues(double.NaN, double.NaN, emptyHtemp, double.NaN, double.NaN, double.NaN);
                }
                this.ProcessValues(radiation, rain, temperatureH, sunHours, vapourPresure, wind);
            }
        }
        else
        {
            this.startDate = this.currentDate;
            this.ProcessValues(radiation, rain, temperatureH, sunHours, vapourPresure, wind);
        }
        this.lastDate = this.currentDate;
    }

    private void ProcessValues(double radiation, double rain, double temperatureMax, double temperatureMin, double sunHours, double vapourPresure, double wind)
    {
      //interpolation for missing or erronous temperature values
      if (double.IsNaN(temperatureMax) || double.IsNaN(temperatureMin) || (temperatureMax < temperatureMin || !Calc.IsBetween(temperatureMax, -90.0, 70.0, 1E-06)) || !Calc.IsBetween(temperatureMin, -90.0, 70.0, 1E-06))
      {
        this.currentOperation = Cache.ReadTempMaxOperation;
        this.AddInterpolatedValue(ref this.tempMaxArray, ref this.tempMaxMissing);
        this.currentOperation = Cache.ReadTempMinOperation;
        this.AddInterpolatedValue(ref this.tempMinArray, ref this.tempMinMissing);
      }
      else
      {
        this.currentOperation = Cache.ReadTempMaxOperation;
        this.AddValue(temperatureMax, ref this.tempMaxArray, ref this.tempMaxMissing, Cache.ScaleInTemperature);
        this.currentOperation = Cache.ReadTempMinOperation;
        this.AddValue(temperatureMin, ref this.tempMinArray, ref this.tempMinMissing, Cache.ScaleInTemperature);
      }
      this.currentOperation = Cache.ReadRainOperation;
      if (!double.IsNaN(rain))
      {
        if (!Calc.IsBetween(rain, 0.0, 400.0, 1E-06))
          this.AddNulledValue(ref this.rainArray, ref this.rainMissing);
        else
          this.AddValue(rain, ref this.rainArray, ref this.rainMissing, Cache.ScaleInRain);
      }
      else
        this.AddNulledValue(ref this.rainArray, ref this.rainMissing);
      this.currentOperation = Cache.ReadRadiationOperation;
      if (!double.IsNaN(radiation))
      {
        if (!Calc.IsBetween(radiation, 0.1, 50.0, 1E-06))
        {
          if (!double.IsNaN(sunHours))
          {
            if (!Calc.IsBetween(sunHours, 0.0, 24.0, 1E-06))
            {
              this.AddInterpolatedValue(ref this.radiationArray, ref this.radiationMissing);
            }
            else
            {
              radiation = Conversion.SunHoursToRadiations(sunHours, this.currentDate.Value, this.location);
              if (!Calc.IsBetween(radiation, 0.1, 50.0))
                this.AddInterpolatedValue(ref this.radiationArray, ref this.radiationMissing);
              else
                this.AddValue(radiation, ref this.radiationArray, ref this.radiationMissing, Cache.ScaleInRadiation);
            }
          }
          else
            this.AddInterpolatedValue(ref this.radiationArray, ref this.radiationMissing);
        }
        else
          this.AddValue(radiation, ref this.radiationArray, ref this.radiationMissing, Cache.ScaleInRadiation);
      }
      else if (!double.IsNaN(sunHours))
      {
        if (!Calc.IsBetween(sunHours, 0.0, 24.0, 1E-06))
        {
          this.AddInterpolatedValue(ref this.radiationArray, ref this.radiationMissing);
        }
        else
        {
          radiation = Conversion.SunHoursToRadiations(sunHours, this.currentDate.Value, this.location);
          if (!Calc.IsBetween(radiation, 0.1, 50.0))
            this.AddInterpolatedValue(ref this.radiationArray, ref this.radiationMissing);
          else
            this.AddValue(radiation, ref this.radiationArray, ref this.radiationMissing, Cache.ScaleInRadiation);
        }
      }
      else
        this.AddInterpolatedValue(ref this.radiationArray, ref this.radiationMissing);
      if (this.vapourPresureColumn != -1)
      {
        this.currentOperation = Cache.ReadVapourPresureOperation;
        if (!double.IsNaN(vapourPresure))
        {
          if (!Calc.IsBetween(vapourPresure, 0.0, 60.0))
            this.AddInterpolatedValue(ref this.vapourPresureArray, ref this.vapourPresureMissing);
          else
            this.AddValue(vapourPresure, ref this.vapourPresureArray, ref this.vapourPresureMissing, Cache.ScaleInVapourPresure);
        }
        else
          this.AddInterpolatedValue(ref this.vapourPresureArray, ref this.vapourPresureMissing);
      }
      if (this.windColumn != -1)
      {
        this.currentOperation = Cache.ReadWindOperation;
        if (!double.IsNaN(vapourPresure))
        {
          if (!Calc.IsBetween(wind, 0.0, 3000.0))
            this.AddInterpolatedValue(ref this.windArray, ref this.windMissing);
          else
            this.AddValue(wind, ref this.windArray, ref this.windMissing, Cache.ScaleInWind);
        }
        else
          this.AddInterpolatedValue(ref this.windArray, ref this.windMissing);
      }
      ++this.count;
    }

    private void ProcessValues(double radiation, double rain, double[] temperatureH, double sunHours, double vapourPresure, double wind)
    {
        //no interpolation for missing or erronous hourly temperature values
        this.currentOperation = Cache.ReadTempHourlyOperation;
        this.AddValue(temperatureH[0], ref this.tempH0Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[1], ref this.tempH1Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[2], ref this.tempH2Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[3], ref this.tempH3Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[4], ref this.tempH4Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[5], ref this.tempH5Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[6], ref this.tempH6Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[7], ref this.tempH7Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[8], ref this.tempH8Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[9], ref this.tempH9Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[10], ref this.tempH10Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[11], ref this.tempH11Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[12], ref this.tempH12Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[13], ref this.tempH13Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[14], ref this.tempH14Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[15], ref this.tempH15Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[16], ref this.tempH16Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[17], ref this.tempH17Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[18], ref this.tempH18Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[19], ref this.tempH19Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[20], ref this.tempH20Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[21], ref this.tempH21Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[22], ref this.tempH22Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);
        this.AddValue(temperatureH[23], ref this.tempH23Array, ref this.tempHourlyMissing, Cache.ScaleInTemperature);

        this.currentOperation = Cache.ReadRainOperation;
        if (!double.IsNaN(rain))
        {
            if (!Calc.IsBetween(rain, 0.0, 400.0, 1E-06))
                this.AddNulledValue(ref this.rainArray, ref this.rainMissing);
            else
                this.AddValue(rain, ref this.rainArray, ref this.rainMissing, Cache.ScaleInRain);
        }
        else
            this.AddNulledValue(ref this.rainArray, ref this.rainMissing);
        this.currentOperation = Cache.ReadRadiationOperation;
        if (!double.IsNaN(radiation))
        {
            if (!Calc.IsBetween(radiation, 0.1, 50.0, 1E-06))
            {
                if (!double.IsNaN(sunHours))
                {
                    if (!Calc.IsBetween(sunHours, 0.0, 24.0, 1E-06))
                    {
                        this.AddInterpolatedValue(ref this.radiationArray, ref this.radiationMissing);
                    }
                    else
                    {
                        radiation = Conversion.SunHoursToRadiations(sunHours, this.currentDate.Value, this.location);
                        if (!Calc.IsBetween(radiation, 0.1, 50.0))
                            this.AddInterpolatedValue(ref this.radiationArray, ref this.radiationMissing);
                        else
                            this.AddValue(radiation, ref this.radiationArray, ref this.radiationMissing, Cache.ScaleInRadiation);
                    }
                }
                else
                    this.AddInterpolatedValue(ref this.radiationArray, ref this.radiationMissing);
            }
            else
                this.AddValue(radiation, ref this.radiationArray, ref this.radiationMissing, Cache.ScaleInRadiation);
        }
        else if (!double.IsNaN(sunHours))
        {
            if (!Calc.IsBetween(sunHours, 0.0, 24.0, 1E-06))
            {
                this.AddInterpolatedValue(ref this.radiationArray, ref this.radiationMissing);
            }
            else
            {
                radiation = Conversion.SunHoursToRadiations(sunHours, this.currentDate.Value, this.location);
                if (!Calc.IsBetween(radiation, 0.1, 50.0))
                    this.AddInterpolatedValue(ref this.radiationArray, ref this.radiationMissing);
                else
                    this.AddValue(radiation, ref this.radiationArray, ref this.radiationMissing, Cache.ScaleInRadiation);
            }
        }
        else
            this.AddInterpolatedValue(ref this.radiationArray, ref this.radiationMissing);
        if (this.vapourPresureColumn != -1)
        {
            this.currentOperation = Cache.ReadVapourPresureOperation;
            if (!double.IsNaN(vapourPresure))
            {
                if (!Calc.IsBetween(vapourPresure, 0.0, 60.0))
                    this.AddInterpolatedValue(ref this.vapourPresureArray, ref this.vapourPresureMissing);
                else
                    this.AddValue(vapourPresure, ref this.vapourPresureArray, ref this.vapourPresureMissing, Cache.ScaleInVapourPresure);
            }
            else
                this.AddInterpolatedValue(ref this.vapourPresureArray, ref this.vapourPresureMissing);
        }
        if (this.windColumn != -1)
        {
            this.currentOperation = Cache.ReadWindOperation;
            if (!double.IsNaN(vapourPresure))
            {
                if (!Calc.IsBetween(wind, 0.0, 3000.0))
                    this.AddInterpolatedValue(ref this.windArray, ref this.windMissing);
                else
                    this.AddValue(wind, ref this.windArray, ref this.windMissing, Cache.ScaleInWind);
            }
            else
                this.AddInterpolatedValue(ref this.windArray, ref this.windMissing);
        }
        ++this.count;
    }

    private double InterpolatedValue(short[] valueArray)
    {
      int stop = this.count;
      int val1 = this.count - 5;
      return Calc.Sum(valueArray, Math.Max(val1, 0), stop) / 5.0;
    }

    private void AddValue(double value, ref short[] valueArray, ref byte missingCount, double scaleIn)
    {
      missingCount = (byte) 0;
      CollectionHelper.Add<short>(ref valueArray, (short) Math.Round(value * scaleIn), this.count, 36600);
    }

    private void AddInterpolatedValue(ref short[] valueArray, ref byte missingCount)
    {
      this.CheckCanAddInterpolate(ref missingCount);
      CollectionHelper.Add<short>(ref valueArray, (short) this.InterpolatedValue(valueArray), this.count, 36600);
    }

    private void AddNulledValue(ref short[] valueArray, ref byte missingCount)
    {
      this.CheckCanAddInterpolate(ref missingCount);
      CollectionHelper.Add<short>(ref valueArray, (short) 0, this.count, 36600);
    }

    private void CheckCanAddInterpolate(ref byte missingCount)
    {
      if (this.count == 0)
        throw new FileFormatException("A missing value is not accepted for the first day.");
      ++missingCount;
      if ((int) missingCount < this.maxConsecutiveMissingValuesAllowed)
        return;
      if (this.numericReader.CurrentLineNumbber <= this.maxConsecutiveMissingValuesAllowed)
        throw new FileFormatException("Too much consecutive missing values(" + (object) missingCount + "). Check the end and the beginning of your files.");
      throw new Exception("Too much consecutive missing values (" + (object) missingCount + ").");
    }

    private void OnException(NumericReader nR, Exception e)
    {
      this.startDate = new DateTime?();
      this.count = 0;
      this.radiationArray = (short[]) null;
      this.rainArray = (short[]) null;
      this.tempMaxArray = (short[]) null;
      this.tempMinArray = (short[]) null;
      this.vapourPresureArray = (short[]) null;
      this.windArray = (short[]) null;
      Uri result;
      if (Uri.TryCreate(nR.CurrentFile, UriKind.RelativeOrAbsolute, out result))
     // throw new FileFormatException(result, "Read weather file exception.\r\nOperation : " + (object)this.currentOperation + ".\r\nLine " + (string)(object)nR.CurrentLineNumbber.ToString() + ", column " + (string)(object)nR.CurrentColumnNumber.ToString() + ".\r\nError \"" + e.Message + "\".\r\nFile \"" + nR.CurrentFile + "\".");
     throw new FileFormatException(result, "Read weather file exception.\r\nOperation : " + (object)this.currentOperation + ".\r\nLine " + (string)(object)nR.CurrentLineNumbber.ToString() + ", column " + (string)(object)nR.CurrentColumnNumber.ToString() + "" + "" + "\".\r\nFile \"" + nR.CurrentFile + "\".");
     // throw new FileFormatException("Read weather file exception.\r\nOperation : " + (object)this.currentOperation + ".\r\nLine " + (string)(object)nR.CurrentLineNumbber.ToString() + ", column " + (string)(object)nR.CurrentColumnNumber.ToString() + ".\r\nError \"" + e.Message + "\".\r\\nFile \"" + nR.CurrentFile + "\".");
    }

    public int IndexOf(DateTime date)
    {
      return (int) (date - this.startDate.Value).TotalDays;
    }

    public bool Contains(int index)
    {
      if (index >= 0)
        return index < this.count;
      return false;
    }

    public bool Contains(DateTime date)
    {
      return this.Contains(this.IndexOf(date));
    }

    public double Radiation(int index)
    {
      return Cache.GetValue(this.radiationArray, index, Cache.ScaleOutRadiation);
    }

    public double Radiation(DateTime date)
    {
      return this.Radiation(this.IndexOf(date));
    }

    public double Rain(int index)
    {
      return Cache.GetValue(this.rainArray, index, Cache.ScaleOutRain);
    }

    public double Rain(DateTime date)
    {
      return this.Rain(this.IndexOf(date));
    }

    public double Rain(int index, int nbDays)
    {
      int stop = index + 1;
      return Calc.Sum(this.rainArray, Math.Max(0, stop - nbDays), stop) * Cache.ScaleOutRain;
    }

    public double Rain(DateTime date, int nbDays)
    {
      return this.Rain(this.IndexOf(date), nbDays);
    }

    public double[] HourlyTemperature(int index)
    {
        if (this.tempH0Column != -1 && this.tempH1Column != -1 && this.tempH2Column != -1 && this.tempH3Column != -1 && this.tempH4Column != -1 && this.tempH5Column != -1 && this.tempH6Column != -1 &&
            this.tempH7Column != -1 && this.tempH8Column != -1 && this.tempH9Column != -1 && this.tempH10Column != -1 && this.tempH11Column != -1 && this.tempH12Column != -1 && this.tempH13Column != -1 &&
            this.tempH14Column != -1 && this.tempH15Column != -1 && this.tempH16Column != -1 && this.tempH17Column != -1 && this.tempH18Column != -1 && this.tempH19Column != -1 && this.tempH20Column != -1 &&
            this.tempH21Column != -1 && this.tempH22Column != -1 && this.tempH23Column != -1)
        {
            double[] hourlyTemp = new double[24];
            hourlyTemp[0] = Cache.GetValue(this.tempH0Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[1] = Cache.GetValue(this.tempH1Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[2] = Cache.GetValue(this.tempH2Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[3] = Cache.GetValue(this.tempH3Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[4] = Cache.GetValue(this.tempH4Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[5] = Cache.GetValue(this.tempH5Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[6] = Cache.GetValue(this.tempH6Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[7] = Cache.GetValue(this.tempH7Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[8] = Cache.GetValue(this.tempH8Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[9] = Cache.GetValue(this.tempH9Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[10] = Cache.GetValue(this.tempH10Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[11] = Cache.GetValue(this.tempH11Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[12] = Cache.GetValue(this.tempH12Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[13] = Cache.GetValue(this.tempH13Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[14] = Cache.GetValue(this.tempH14Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[15] = Cache.GetValue(this.tempH15Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[16] = Cache.GetValue(this.tempH16Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[17] = Cache.GetValue(this.tempH17Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[18] = Cache.GetValue(this.tempH18Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[19] = Cache.GetValue(this.tempH19Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[20] = Cache.GetValue(this.tempH20Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[21] = Cache.GetValue(this.tempH21Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[22] = Cache.GetValue(this.tempH22Array, index, Cache.ScaleOutTemperature);
            hourlyTemp[23] = Cache.GetValue(this.tempH23Array, index, Cache.ScaleOutTemperature);

            return hourlyTemp;
        }
        else
        {
            if (this.tempMaxColumn != -1 && this.tempMinColumn != -1)//to avopid infinite loop
            {
               int nextIndex = Math.Min (index+1, this.tempMinArray.Count() -1) ;
               int previousindex = Math.Max(index -1,0);

               return temperatureConverter.getHourlyAirTemperature(TemperatureMin(index), TemperatureMax(index), TemperatureMax(previousindex), TemperatureMin(nextIndex));
            }
            else
            {
                throw new Exception("can not find tmin and tmax or the 24 hourly temperatures");
            }
        }
    }

    public double[] HourlyTemperature(DateTime date)
    {
        return HourlyTemperature(this.IndexOf(date));
    }

    public double TemperatureMax(int index)
    {
        if (this.tempMaxColumn != -1)
        {
            return Cache.GetValue(this.tempMaxArray, index, Cache.ScaleOutTemperature);
        }
        else
        {
            return TemperatureConverter.HourlyToMax(HourlyTemperature(index));
        }
    }

    public double TemperatureMax(DateTime date)
    {
      return this.TemperatureMax(this.IndexOf(date));
    }

    public double TemperatureMin(int index)
    {
        if (this.tempMinColumn != -1)
        {
            return Cache.GetValue(this.tempMinArray, index, Cache.ScaleOutTemperature);
        }
        else
        {
            return TemperatureConverter.HourlyToMin(HourlyTemperature(index));
        }

    }

    public double TemperatureMin(DateTime date)
    {
      return this.TemperatureMin(this.IndexOf(date));
    }

    public double TemperatureMean(int index)
    {
        if (this.tempMinColumn != -1 && this.tempMaxColumn!= -1)
        {
            return Calc.Mean(TemperatureMin(index), TemperatureMax(index));
        }
        else
        {
            return Calc.MeanHourly(HourlyTemperature(index));
        }
    }

    public double TemperatureMean(DateTime date)
    {
      return this.TemperatureMean(this.IndexOf(date));
    }

    public double TemperatureMean(int index, int nbDays)
    {
      int stop = index + 1;
      if (this.tempMinColumn != -1 && this.tempMaxColumn != -1)
      {
          return Calc.Mean(this.tempMaxArray, this.tempMinArray, Math.Max(0, stop - nbDays), stop) * Cache.ScaleOutTemperature;
      }
      else
      {
          int start=  Math.Max(0, stop - nbDays);
          if (start ==stop)
          {
              return 0.0;
          }
          else
          {
              double sum = 0;
              for (int i = start; i < stop; i++)
              {
                  sum += TemperatureMean(i);
              }
              return sum / (double)(2 * (stop - start));
          }
      }
    }

    public double TemperatureMean(DateTime date, int nbDays)
    {
      return this.TemperatureMean(this.IndexOf(date), nbDays);
    }

    public double VapourPresure(int index)
    {
      return Cache.GetValue(this.vapourPresureArray, index, Cache.ScaleOutVapourPresure);
    }

    public double VapourPresure(DateTime date)
    {
      return Cache.GetValue(this.vapourPresureArray, this.IndexOf(date), Cache.ScaleOutVapourPresure);
    }

    public double Wind(int index)
    {
      return Cache.GetValue(this.windArray, index, Cache.ScaleOutWind);
    }

    public double Wind(DateTime date)
    {
      return Cache.GetValue(this.windArray, this.IndexOf(date), Cache.ScaleOutWind);
    }

    private static double GetValue(short[] valueArray, int index, double scaleOut)
    {
        if (index < valueArray.Length)
        {
            return (double)valueArray[index] * scaleOut;
        }
        else
        {
            throw new Exception("try to reach past the end of the meteo file");
        }
    }

    public static Cache Get(ILocation geoPosition, IWeatherInput input)
    {
      return Cache.CacheData[new Cache.DataKey(geoPosition, input)];
    }

    public static void Clear()
    {
      Cache.CacheData.Clear();
    }

    public static void Remove(ILocation geoPosition, IWeatherInput input)
    {
      Cache.CacheData.Remove(new Cache.DataKey(geoPosition, input));
    }

    private struct DataKey
    {
      internal readonly IWeatherInput input;
      internal readonly ILocation location;

      public DataKey(ILocation dataKeyGeoPosition, IWeatherInput dataKeyInput)
      {
        this.location = dataKeyGeoPosition;
        this.input = dataKeyInput;
      }
    }

    private class DataKeyEqualityComparer : IEqualityComparer<Cache.DataKey>
    {
      public bool Equals(Cache.DataKey x, Cache.DataKey y)
      {
        ILocation location = x.location;
        ILocation other = y.location;
        if (!CollectionHelper.AreBothNullOrNot((object) location, (object) other) || location != null && !location.Equals(other))
          return false;
        IWeatherInput weatherInput1 = x.input;
        IWeatherInput weatherInput2 = y.input;
        if (!CollectionHelper.AreBothNullOrNot((object) weatherInput1, (object) weatherInput2))
          return false;
        if (weatherInput1 == null)
          return true;
        if (weatherInput1.MaxConsecutiveMissingValuesAllowed != weatherInput2.MaxConsecutiveMissingValuesAllowed)
          return false;
        IWeatherFormat format1 = weatherInput1.Format;
        IWeatherFormat format2 = weatherInput2.Format;
        if (!CollectionHelper.AreBothNullOrNot((object) format1, (object) format2) || format1 != null && !format1.Equals(format2))
          return false;
        IList<string> files1 = weatherInput1.Files;
        IList<string> files2 = weatherInput2.Files;
        if (!CollectionHelper.AreBothNullOrNot((object) files1, (object) files2))
          return false;
        if (files1 == null)
          return true;
        int index = 0;
        foreach (string str in (IEnumerable<string>) files1)
        {
          if (str != Enumerable.ElementAtOrDefault<string>((IEnumerable<string>) files2, index))
            return false;
          ++index;
        }
        return true;
      }

      public int GetHashCode(Cache.DataKey obj)
      {
        return obj.location.GetHashCode() + obj.input.Format.GetHashCode();
      }
    }
  }
}
