using System.Collections.Generic;

namespace Sirius.Model.Interface
{
  public interface IWeatherInput
  {
    IWeatherFormat Format { get; set; }

    IList<string> Files { get; set; }

    int MaxConsecutiveMissingValuesAllowed { get; set; }
  }
}
