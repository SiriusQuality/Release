using System;

namespace Sirius.Model.Interface
{
  public interface ILocation : IComparable<ILocation>, IEquatable<ILocation>
  {
    string Name { get; set; }

    double Latitude { get; set; }

    double Longitude { get; set; }

    double Altitude { get; set; }

    string ShortName { get; set; }
  }
}
