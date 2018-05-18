using Sirius.Model.Interface;
using System.Collections.Generic;

namespace Sirius.Model.Input
{
  public class WeatherInput : IWeatherInput
  {
    public string Name { get; set; }

    public IWeatherFormat Format { get; set; }

    public IList<string> Files { get; set; }

    public int MaxConsecutiveMissingValuesAllowed { get; set; }

    public override string ToString()
    {
      return string.Format("Weather {0}", (object) this.Name);
    }
  }
}
