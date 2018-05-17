using Sirius.Model.Constant;
using Sirius.Model.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sirius.Model.Input
{
  public class WeatherFormat : List<WeatherFormatID>, IWeatherFormat, IList<WeatherFormatID>, ICollection<WeatherFormatID>, IEnumerable<WeatherFormatID>, IEnumerable, IEquatable<IWeatherFormat>
  {
    public bool Equals(IWeatherFormat other)
    {
      int count = this.Count;
      if (count != other.Count)
        return false;
      for (int index = 0; index < count; ++index)
      {
        if (this[index] != other[index])
          return false;
      }
      return true;
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("Format ");
      foreach (WeatherFormatID weatherFormatId in (List<WeatherFormatID>) this)
      {
        stringBuilder.Append(weatherFormatId.ToString());
        stringBuilder.Append(" ");
      }
      return stringBuilder.ToString();
    }
  }
}
