using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Xml.Serialization;
using GeoPosition = Sirius.Model.Input.Location;
using SiriusModel.InOut.Base;

namespace SiriusModel.InOut
{
    ///<summary>
    ///List all possible weather file format.
    ///</summary>
    public enum WeatherFileFormat
    {
        ///<summary>
        ///Possible format for wheather file : year day min max rain rad.
        ///</summary>
        YearJdayMinMaxRainRad,

        ///<summary>
        ///Possible format for weather file : year day min max rain rad wind wp.
        ///</summary>
        YearJdayMinMaxRainRadWindVp,

        ///<summary>
        ///Possible format for weather file : year day HoulryTemp rain rad wind wp.
        ///</summary>
        YearJdayHourlyTempRainRadWindVp
    }

    [Serializable, XmlInclude(typeof(WeatherFileFormat))]
    public class WeatherFile : Child<string>, IWithPath
    {
        private string relativeFileName;

        #region file relative, absolute

        ///<summary>
        ///aSheath weather file path.
        ///</summary>
        [XmlAttribute(AttributeName = "file")]
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string File
        {
            get { return relativeFileName; }
            set
            {
                var referenceFile = GetReferenceAbsoluteFileName();
                var newRelativeFileName = FileHelper.GetRelative(referenceFile, value);

                if (this.SetObject(ref relativeFileName, ref newRelativeFileName, "File"))
                {
                    NotifyPropertyChanged("AbsoluteFile");
                }
                this.Assert(FileHelper.GetAbsolute(GetReferenceAbsoluteFileName(), relativeFileName),
                            System.IO.File.Exists, "File", "valid file system", null);
            }
        }

        [XmlIgnore]
        public string AbsoluteFile
        {
            get
            {
                var referenceFile = GetReferenceAbsoluteFileName();
                return FileHelper.GetAbsolute(referenceFile, relativeFileName);
            }
        }

        public void UpdatePath(string oldAbsolute, string newAbsolute)
        {
            var fileOldAbsolute = FileHelper.GetAbsolute(oldAbsolute, relativeFileName);
            File = FileHelper.GetRelative(newAbsolute, fileOldAbsolute);
        }

        private static string GetReferenceAbsoluteFileName()
        {
            var siteFile = (ProjectFile.This != null) ? ProjectFile.This.FileContainer.SiteFile : null;
            var referenceFile = (siteFile != null) ? siteFile.AbsoluteFileName : null;
            return referenceFile;
        }

        #endregion

        public WeatherFile()
        {
            File = "";
        }

        public WeatherFile(string file)
        {
            File = file;
        }

        private SiteItem SiteItemParent
        {
            get { return Parent as SiteItem; }
        }

        public override bool NotifyPropertyChanged(string propertyName)
        {
            if (SiteItemParent != null && SiteItemParent.ProjectDataFileParent != null) SiteItemParent.ProjectDataFileParent.IsModified = true;
            return base.NotifyPropertyChanged(propertyName);
        }

        public override void CheckWarnings()
        {
            File = File;
        }

        public override string WarningFileID
        {
            get
            {
                return (SiteItemParent != null && SiteItemParent.ProjectDataFileParent != null) ? SiteItemParent.ProjectDataFileParent.ID : "?";
            }
        }

        public override string WarningItemName
        {
            get
            {
                return (SiteItemParent != null) ? SiteItemParent.Name + " " + File : "?";
            }
        }
    }

    [Serializable]
    public class WeatherFileGenerator : ChildKeyGeneratorSorted<WeatherFile, string>
    {
        public override bool Selectable
        {
            get { return false; }
        }
        public override bool Sorted
        {
            get { return true; }
        }

        public override bool NullSelectable
        {
            get { return false; }
        }

        public override string KeyPropertyName
        {
            get { return "File"; }
        }

        public override Func<WeatherFile, string> KeySelector
        {
            get { return weatherFile => weatherFile.File; }
        }

        public override Func<WeatherFile, string, string> KeySetter
        {
            get { return delegate(WeatherFile weatherFile, string file) { weatherFile.File = file; return file; }; }
        }

        public override void CreateNullSelectable(BaseBindingList<WeatherFile> selectable)
        {
            throw new NotImplementedException();
        }
    }

    ///<summary>
    ///Site values.
    ///</summary>
    [Serializable, XmlInclude(typeof(WeatherFile)), XmlInclude(typeof(WeatherFileFormat))]
    public class SiteItem : ProjectDataFileItem1Child<WeatherFile, string, WeatherFileGenerator>, IWithPath
    {
        private WeatherFileFormat format;

        ///<summary>
        ///The geoposition of this site.
        ///</summary>
        private readonly GeoPosition geoPosition;
        private DateTime minSowingDate;
        private DateTime maxSowingDate;
        private DateTime finalMinSowingDate;
        private DateTime finalMaxSowingDate;

        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set
            {
                this.SetStruct(ref latitude, ref value, "Latitude");
                this.Assert(latitude, l => l >= -90 && l <= 90, "Latitude", "Latitude must be between -90 and 90.", null);
                if (latitude >= -90 && latitude <= 90) geoPosition.Latitude = (float)latitude;
            }
        }

        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set
            {
                this.SetStruct(ref longitude, ref value, "Longitude");
                this.Assert(longitude, l => l >= -180 && l <= 360, "Latitude", "Latitude must be between -180 and 360.", null);
                if (longitude >= -90 && longitude <= 90) geoPosition.Longitude = (float)longitude;
            }
        }

        private double elevation;
        public double Elevation
        {
            get { return elevation; }
            set
            {
                this.SetStruct(ref elevation, ref value, "Elevation");
                this.Assert(elevation, e => e >= 0 && e <= 5000, "Elevation", "Elevation must be between 0 and 5000.", null);
                if (elevation >= 0 && elevation <= 5000) geoPosition.Altitude = (float) elevation;
            }
        }

        private double measurementHeight;
        public double MeasurementHeight
        {
            get { return measurementHeight; }
            set
            {
                this.SetStruct(ref measurementHeight, ref value, "MeasurementHeight");
                this.Assert(measurementHeight, e => e >= 0 && e <= 50, "MeasurementHeight", "Measurement height must be less than 50.", null);
            }
        }

        ///<summary>
        ///Get or set the type of sowing window (fixed, JRC winder and spring, and SiriusQuality method).
        ///</summary>
        private int sowingWindowType;
        public int SowingWindowType
        {
            get { return sowingWindowType; }
            set
            {
                this.SetStruct(ref sowingWindowType, ref value, "SowingWindowType");
                this.Assert(sowingWindowType, e => e >= 0 && e <= 3, "SowingWindowType", "SowingWindowType must be less than 3.", null);
            }
        }

        ///<summary>
        ///Get or set the start of sowing date window.
        ///</summary>
        public DateTime MinSowingDate
        {
            get { return minSowingDate; }
            set { this.SetStruct(ref minSowingDate, ref value, "MinSowingDate"); CheckWarnings(); }
        }

        ///<summary>
        ///Get or set the start of finally selected sowing date window.
        ///</summary>
        [XmlIgnore]
        public DateTime FinalMinSowingDate
        {
            get { return finalMinSowingDate; }
            set { this.SetStruct(ref finalMinSowingDate, ref value, "FinalMinSowingDate"); }
        }

        ///<summary>
        ///Get or set the end of sowing date window.
        ///</summary>
        public DateTime MaxSowingDate
        {
            get { return maxSowingDate; }
            set { this.SetStruct(ref maxSowingDate, ref value, "MaxSowingDate"); CheckWarnings(); }
        }

        ///<summary>
        ///Get or set the end of finally selected sowing date window.
        ///</summary>
        [XmlIgnore]
        public DateTime FinalMaxSowingDate
        {
            get { return finalMaxSowingDate; }
            set { this.SetStruct(ref finalMaxSowingDate, ref value, "FinalMaxSowingDate"); }
        }

        ///<summary>
        ///Get or set the minimum length of sowing window.
        ///</summary>
        private int minSowWinLength;
        public int MinSowWinLength
        {
            get { return minSowWinLength; }
            set
            {
                this.SetStruct(ref minSowWinLength, ref value, "MinLength");
                this.Assert(minSowWinLength, d => d >= 0, "MinLength", ">=0", null);
            }
        }

        ///<summary>
        ///Get or set the initial length of sowing window for the SIRIUS method of estimation.
        ///</summary>
        private int initSowWindow;
        public int InitSowWindow
        {
            get { return initSowWindow; }
            set
            {
                this.SetStruct(ref initSowWindow, ref value, "InitSowWindow");
                this.Assert(initSowWindow, d => d >= 7, "InitSowWindow", ">=7", null);
            }
        }

        ///<summary>
        ///Get or set the number of days that should be checked for temperature criterion (JRC Spring).
        ///</summary>
        private int checkDaysTemp;
        public int CheckDaysTemp
        {
            get { return checkDaysTemp; }
            set
            {
                this.SetStruct(ref checkDaysTemp, ref value, "CheckDaysTemp");
                this.Assert(checkDaysTemp, d => d >= 0, "CheckDaysTemp", ">=0", null);
            }
        }

        ///<summary>
        ///Get or set the number of days that should be checked for precipitation criterion (JRC Spring).
        ///</summary>
        private int checkDaysPcp;
        public int CheckDaysPcp
        {
            get { return checkDaysPcp; }
            set
            {
                this.SetStruct(ref checkDaysPcp, ref value, "CheckDaysPcp");
                this.Assert(checkDaysPcp, d => d >= 0, "CheckDaysPcp", ">=0", null);
            }
        }

        ///<summary>
        ///Get or set the minimum temperature sum to determine the LSD (JRC Winter).
        ///</summary>
        private double tempSum;
        public double TempSum
        {
            get { return tempSum; }
            set
            {
                this.SetStruct(ref tempSum, ref value, "TempSum");
                this.Assert(tempSum, d => d >= 0, "TempSum", ">=0", null);
            }
        }

        ///<summary>
        ///Get or set the threshold for mean average temperature over CheckDaysTemp days (JRC Spring).
        ///</summary>
        private double tempThr;
        public double TempThr
        {
            get { return tempThr; }
            set
            {
                this.SetStruct(ref tempThr, ref value, "TempThr");
                this.Assert(tempThr, d => d >= 0, "TempThr", ">=0", null);
            }
        }

        ///<summary>
        ///Get or set the threshold for total precipitation over CheckDaysPcp days (JRC Spring).
        ///</summary>
        private double pcpSum;
        public double PcpSum
        {
            get { return pcpSum; }
            set
            {
                this.SetStruct(ref pcpSum, ref value, "PcpSum");
                this.Assert(pcpSum, d => d >= 0, "PcpSum", ">=0", null);
            }
        }

        private DateTime esd_S;
        [XmlIgnore]
        public DateTime ESD_S
        {
            get { return esd_S; }
            set { this.SetStruct(ref esd_S, ref value, "ESD_S"); }
        }

        private DateTime lsd_S;
        [XmlIgnore]
        public DateTime LSD_S
        {
            get { return lsd_S; }
            set { this.SetStruct(ref lsd_S, ref value, "LSD_S"); }
        }

        private DateTime esd_W;
        [XmlIgnore]
        public DateTime ESD_W
        {
            get { return esd_W; }
            set { this.SetStruct(ref esd_W, ref value, "ESD_W"); }
        }

        private DateTime lsd_W;
        [XmlIgnore]
        public DateTime LSD_W
        {
            get { return lsd_W; }
            set {  this.SetStruct(ref lsd_W, ref value, "LSD_W"); }
        }

        private DateTime esd_Sir;
        [XmlIgnore]
        public DateTime ESD_Sir
        {
            get { return esd_Sir; }
            set { this.SetStruct(ref esd_Sir, ref value, "ESD_Sir"); }
        }

        private DateTime lsd_Sir;
        [XmlIgnore]
        public DateTime LSD_Sir
        {
            get { return lsd_Sir; }
            set { this.SetStruct(ref lsd_Sir, ref value, "LSD_Sir"); }
        }

        [XmlIgnore]
        public GeoPosition GeoPosition
        {
            get { return geoPosition; }
        }

        ///<summary>
        ///Weather file format.
        ///</summary>
        [XmlAttribute(AttributeName = "format"), Bindable(false)]
        public WeatherFileFormat Format
        {
            get { return format; }
            set { this.SetStruct(ref format, ref value, "Format"); }
        }

        public const string YearJdayMinMaxRainRadName = "Year (YYYY)   DOY (DDD)   Tmin (°C)   Tmax (°C)   Rain (mm/d)   Radiation (MJ/m²)";
        public const string YearJdayMinMaxRainRadWindVpName = "Year (YYYY)   DOY (DDD)   Tmin (°C)   Tmax (°C)   Rain (mm/d)   Radiation (MJ/m²) Wind (km/d)  Vapour presure (hPa)";
        public const string YearJdayHourlyTempRainRadWindVpName = "Year (YYYY)   DOY (DDD)   TH0 (°C) ... TH23 (°C)   Rain (mm/d)   Radiation (MJ/m²) Wind (km/d)  Vapour presure (hPa)";
        [XmlIgnore]
        public string FormatStr
        {
            get
            {
                if (format == WeatherFileFormat.YearJdayMinMaxRainRad)
                {
                    return YearJdayMinMaxRainRadName;
                }
                else
                {
                    if (format == WeatherFileFormat.YearJdayMinMaxRainRadWindVp)
                    {
                        return YearJdayMinMaxRainRadWindVpName;
                    }
                    else
                    {
                        return YearJdayHourlyTempRainRadWindVpName;
                    }
                }                     
            }
            set
            {
                if (value == YearJdayMinMaxRainRadName)
              //  if (value == "Year DOY TempMin TempMax Rain Radiation")
                {
                    Format = WeatherFileFormat.YearJdayMinMaxRainRad;
                }
                else if (value == YearJdayMinMaxRainRadWindVpName)
                // else if (value == "Year DOY TempMin TempMax Rain Radiation Wind VapourPresure")
                {
                    Format = WeatherFileFormat.YearJdayMinMaxRainRadWindVp;
                }
                else if (value == YearJdayHourlyTempRainRadWindVpName)
                {
                    Format = WeatherFileFormat.YearJdayHourlyTempRainRadWindVp;
                }
                else throw new ArgumentException("value must be a valid weather file format.", "value");
            }
        }

        ///<summary>
        ///List of weather files associated to a siteDef.
        ///</summary>
        public BaseBindingList<WeatherFile> WeatherFiles { get { return BindingItems1; } }

        ///<summary>
        ///Create a new Site.
        ///</summary>
        public SiteItem(string name)
            : base(name)
        {
            geoPosition = new GeoPosition();
            Latitude = 45.8;
            Longitude = 3.1;
            Elevation = 321;
            MeasurementHeight = 2;
            SowingWindowType = 3;
            MinSowingDate = new DateTime(2005, 11, 01);
            MaxSowingDate = new DateTime(2005, 12, 31);
            FinalMinSowingDate = MinSowingDate;
            FinalMaxSowingDate = MaxSowingDate;
            InitSowWindow = 42;
            MinSowWinLength = 20;
            checkDaysPcp = 3;
            checkDaysTemp = 10;
            TempThr = 10;
            TempSum = 500;
            PcpSum = 3;
            ESD_S = new DateTime(9999, 1, 1);
            LSD_S = new DateTime(9999, 12, 31);
            ESD_W = new DateTime(9999, 1, 1);
            LSD_W = new DateTime(9999, 12, 31);
            ESD_Sir = new DateTime(9999, 1, 1);
            LSD_Sir = new DateTime(9999, 12, 31);
            format = WeatherFileFormat.YearJdayMinMaxRainRad;
        }

        public SiteItem()
            : this("")
        {
        }

        public override void UpdatePath(string oldAbsolute, string newAbsolute)
        {
            foreach (var weatherFile in WeatherFiles)
            {
                weatherFile.UpdatePath(oldAbsolute, newAbsolute);
            }
        }
    }
}
