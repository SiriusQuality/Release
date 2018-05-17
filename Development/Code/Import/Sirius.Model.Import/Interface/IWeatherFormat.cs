using Sirius.Model.Constant;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sirius.Model.Interface
{
  public interface IWeatherFormat : IList<WeatherFormatID>, ICollection<WeatherFormatID>, IEnumerable<WeatherFormatID>, IEnumerable, IEquatable<IWeatherFormat>
  {
  }
}
