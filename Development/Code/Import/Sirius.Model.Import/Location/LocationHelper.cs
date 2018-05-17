using Ami.Framework.Tool;
using Sirius.Model.Interface;

namespace Sirius.Model.Helper
{
  public static class LocationHelper
  {
    public const double Epsilon = 0.01;

    public static int DoCompareTo(this ILocation location, ILocation other)
    {
      if (other == null)
        return int.MaxValue;
      if (object.ReferenceEquals((object) location, (object) other))
        return 0;
      if (!Calc.Equals(location.Altitude, other.Altitude, 0.01))
        return location.Altitude.CompareTo(other.Altitude);
      if (!Calc.Equals(location.Latitude, other.Latitude, 0.01))
        return location.Latitude.CompareTo(other.Latitude);
      if (!Calc.Equals(location.Longitude, other.Longitude, 0.01))
        return location.Longitude.CompareTo(other.Longitude);
      return 0;
    }

    public static bool DoEquals(this ILocation location, ILocation other)
    {
      if (other == null)
        return false;
      if (object.ReferenceEquals((object) location, (object) other))
        return true;
      if (Calc.Equals(location.Altitude, other.Altitude, 0.01) && Calc.Equals(location.Latitude, other.Latitude, 0.01))
        return Calc.Equals(location.Longitude, other.Longitude, 0.01);
      return false;
    }

    public static bool DoEquals(this ILocation location, object other)
    {
      if (other == null)
        return false;
      ILocation other1 = other as ILocation;
      if (other1 != null)
        return location.Equals(other1);
      return false;
    }

    public static int DoGetHashCode(this ILocation location)
    {
      return location.Altitude.GetHashCode() + location.Latitude.GetHashCode() + location.Longitude.GetHashCode();
    }

    public static double SimplifyLongitude(double longitude)
    {
      if (Calc.IsBetween(longitude, -180.0, 180.0, 0.01) || !Calc.IsBetween(longitude, 180.0, 360.0, 0.01))
        return longitude;
      return longitude - 360.0;
    }

    public static void Validate(this ILocation location)
    {
      Assert.AssertIsPositiveOrZero(location.Altitude, "Altitude must be positive or zero.");
      Assert.AssertIsBetween(location.Latitude, -90.0, 90.0, 0.01, "Latitude must be between -90 and 90.");
      Assert.AssertIsBetween(location.Longitude, -180.0, 180.0, 0.01, "Longitude must be between -180 and 180.");
    }
  }
}
