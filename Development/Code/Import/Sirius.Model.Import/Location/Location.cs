using Sirius.Model.Helper;
using Sirius.Model.Interface;
using System;

namespace Sirius.Model.Input
{
  public class Location : ILocation, IComparable<ILocation>, IEquatable<ILocation>
  {
    private double longitude;

    public string Name { get; set; }

    public double Latitude { get; set; }

    public double Longitude
    {
      get
      {
        return this.longitude;
      }
      set
      {
        this.longitude = LocationHelper.SimplifyLongitude(value);
      }
    }

    public double Altitude { get; set; }

    public string ShortName { get; set; }

    public Location()
      : this(0.0f, 0.0f, 0.0f)
    {
    }

    public Location(float latitude, float longitude, float altitude)
    {
      this.Latitude = (double) latitude;
      this.Longitude = (double) longitude;
      this.Altitude = (double) altitude;
    }

    public int CompareTo(ILocation other)
    {
      return LocationHelper.DoCompareTo((ILocation) this, other);
    }

    public bool Equals(ILocation other)
    {
      return LocationHelper.DoEquals((ILocation) this, other);
    }

    public override bool Equals(object other)
    {
      return LocationHelper.DoEquals((ILocation) this, other);
    }

    public override int GetHashCode()
    {
      return LocationHelper.DoGetHashCode((ILocation) this);
    }

    public override string ToString()
    {
      return string.Format("Site {0} {1} [{2:##.##};{3:##.##};{4}]", (object) this.Name, (object) this.ShortName, (object) this.Latitude, (object) this.Longitude, (object) this.Altitude);
    }
  }
}
