//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

/// 
/// This class was created from file C:\Users\mancealo\Documents\Sirius Quality\branches\TestMeteoOct\Code\SiriusQuality-MeteoComponent\XML\SiriusQualityMeteo_MeteoState.xml
/// The tool used was: DCC - Domain Class Coder, http://components.biomamodelling.org/, DCC
/// 
/// Loic Manceau
/// loic.manceau@inra.fr
/// INRA
/// 
/// 
/// 10/6/2017 1:00:54 PM
/// 
namespace SiriusQualityMeteo
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using CRA.ModelLayer.Core;
    using CRA.ModelLayer.ParametersManagement;
    
    
    /// <summary>MeteoState Domain class contains the accessors to values</summary>
    [Serializable()]
    public class MeteoState : ICloneable, IDomainClass
    {
        
        #region Private fields
        private double _minTair;
        
        private double _maxTair;
        
        private double _meanTair;
        
        private double _vaporPressure;
        
        private double _minShootTemperature;
        
        private double _maxShootTemperature;
        
        private double _dayLength;
        
        private double _photoperiod;
        
        private double _radTopAtm;
        
        private double _HSlope;
        
        private double _saturationVaporPressure;
        
        private double _VPDair;
        
        private double _VPDairCanopy;
        
        private double[] _hourlyAirTemperature = new double[24];
        
        private double[] _hourlyVPDAir = new double[24];
        
        private double[] _hourlyRadiation = new double[24];
        
        private int _dayOfYear;
        
        private double _solarRadiation;
        
        private int _calculateHourly;
        
        private double[] _RH = new double[24];
        
        private double _solarDeclination;
        
        private double[] _hourlySolarElevation = new double[24];
        
        private double[] _hourlyIdir = new double[24];
        
        private double[] _hourlyIdiff = new double[24];
        
        private double _dailyIdir;
        
        private double _dailyIdiff;
        
        private double _dayLengthHorizonZero;
        
        private double _radTopAtmHorizonZero;
        
        private double _dailyMeanWindSpeed;
        
        private double _dailyMaxWindSpeed;
        
        private double _dailyMinWindSpeed;

        private double[] _hourlyWindSpeed = new double[24];
        #endregion
        
        #region Private field for properties
        private ParametersIO _parametersIO;
        #endregion
        
        #region Constructor
        /// <summary>No parameters constructor</summary>
        public MeteoState()
        {
            _parametersIO = new ParametersIO(this);
        }

        /// <summary>copy constructor</summary>
        public MeteoState(MeteoState toCopy, bool copyAll)
        {
            _parametersIO = new ParametersIO(this);

            _dayLengthHorizonZero = toCopy.dayLengthHorizonZero;
            _dayLength = toCopy.dayLength;
            _photoperiod = toCopy.photoperiod;
            _radTopAtm = toCopy.radTopAtm;
            _radTopAtmHorizonZero = toCopy.radTopAtmHorizonZero;
            _HSlope = toCopy.HSlope;
            _saturationVaporPressure = toCopy.saturationVaporPressure;
            _VPDair = toCopy.VPDair;
            _VPDairCanopy = toCopy.VPDairCanopy;

            _RH = new double[24];
            _hourlyAirTemperature = new double[24];
            _hourlyVPDAir = new double[24];
            _hourlyRadiation = new double[24];
            _hourlySolarElevation = new double[24];
            _hourlyWindSpeed = new double[24];

            for (int i = 0; i < 24; i++) _hourlyRadiation[i] = toCopy.hourlyRadiation[i];
            for (int i = 0; i < 24; i++) _hourlyIdir[i] = toCopy.hourlyRadiation[i];
            for (int i = 0; i < 24; i++) _hourlyIdiff[i] = toCopy.hourlyRadiation[i];
            for (int i = 0; i < 24; i++) _hourlySolarElevation[i] = toCopy._hourlySolarElevation[i];
            for (int i = 0; i < 24; i++) _hourlyWindSpeed[i] = toCopy._hourlyWindSpeed[i];

            if (toCopy.calculateHourly == 1)
            {
                for (int i = 0; i < 24; i++)
                {
                    _RH[i] = toCopy.RH[i];
                    _hourlyAirTemperature[i] = toCopy.hourlyAirTemperature[i];
                    _hourlyVPDAir[i] = toCopy.hourlyVPDAir[i];

                }
            }

            if (copyAll)
            {
                _calculateHourly = toCopy.calculateHourly;
                _minTair = toCopy.minTair;
                _maxTair = toCopy.maxTair;
                _meanTair = toCopy.meanTair;
                _minShootTemperature = toCopy.minShootTemperature;
                _maxShootTemperature = toCopy.maxShootTemperature;
                _dayOfYear = toCopy.dayOfYear;
                _vaporPressure = toCopy.vaporPressure;
                _solarRadiation = toCopy.solarRadiation;
            }

            _dailyMeanWindSpeed = toCopy._dailyMeanWindSpeed;

            _dailyMaxWindSpeed = toCopy._dailyMaxWindSpeed;

            _dailyMinWindSpeed = toCopy._dailyMinWindSpeed;

        }

        #endregion
        
        #region Public properties
        /// <summary>minimum air temperature</summary>
        public double minTair
        {
            get
            {
                return this._minTair;
            }
            set
            {
                this._minTair = value;
            }
        }
        
        /// <summary>maximum air temperature</summary>
        public double maxTair
        {
            get
            {
                return this._maxTair;
            }
            set
            {
                this._maxTair = value;
            }
        }
        
        /// <summary>average air temperature </summary>
        public double meanTair
        {
            get
            {
                return this._meanTair;
            }
            set
            {
                this._meanTair = value;
            }
        }
        
        /// <summary>vapor pressure</summary>
        public double vaporPressure
        {
            get
            {
                return this._vaporPressure;
            }
            set
            {
                this._vaporPressure = value;
            }
        }
        
        /// <summary>minimum shoot temprature</summary>
        public double minShootTemperature
        {
            get
            {
                return this._minShootTemperature;
            }
            set
            {
                this._minShootTemperature = value;
            }
        }
        
        /// <summary>maximum shoot temperature</summary>
        public double maxShootTemperature
        {
            get
            {
                return this._maxShootTemperature;
            }
            set
            {
                this._maxShootTemperature = value;
            }
        }
        
        /// <summary>length of the day taking into account atmosphere refraction sunset/rise -6°)</summary>
        public double dayLength
        {
            get
            {
                return this._dayLength;
            }
            set
            {
                this._dayLength = value;
            }
        }
        
        /// <summary>photoperiod</summary>
        public double photoperiod
        {
            get
            {
                return this._photoperiod;
            }
            set
            {
                this._photoperiod = value;
            }
        }
        
        /// <summary>Exrtraterestrial radiations with dayLength (sunset/rise -6°)</summary>
        public double radTopAtm
        {
            get
            {
                return this._radTopAtm;
            }
            set
            {
                this._radTopAtm = value;
            }
        }
        
        /// <summary>Slope of saturated vapour pressure temperature curve at temperature T </summary>
        public double HSlope
        {
            get
            {
                return this._HSlope;
            }
            set
            {
                this._HSlope = value;
            }
        }
        
        /// <summary>saturation vapour pressure of water vapour at temperature T</summary>
        public double saturationVaporPressure
        {
            get
            {
                return this._saturationVaporPressure;
            }
            set
            {
                this._saturationVaporPressure = value;
            }
        }
        
        /// <summary>VPDair</summary>
        public double VPDair
        {
            get
            {
                return this._VPDair;
            }
            set
            {
                this._VPDair = value;
            }
        }
        
        /// <summary>VPDairCanopy</summary>
        public double VPDairCanopy
        {
            get
            {
                return this._VPDairCanopy;
            }
            set
            {
                this._VPDairCanopy = value;
            }
        }
        
        /// <summary>hourly air temperature</summary>
        public double[] hourlyAirTemperature
        {
            get
            {
                return this._hourlyAirTemperature;
            }
            set
            {
                this._hourlyAirTemperature = value;
            }
        }
        
        /// <summary>hourly VPDair</summary>
        public double[] hourlyVPDAir
        {
            get
            {
                return this._hourlyVPDAir;
            }
            set
            {
                this._hourlyVPDAir = value;
            }
        }
        
        /// <summary>hourly radiation</summary>
        public double[] hourlyRadiation
        {
            get
            {
                return this._hourlyRadiation;
            }
            set
            {
                this._hourlyRadiation = value;
            }
        }
        
        /// <summary>number of this day</summary>
        public int dayOfYear
        {
            get
            {
                return this._dayOfYear;
            }
            set
            {
                this._dayOfYear = value;
            }
        }
        
        /// <summary>daily radiation</summary>
        public double solarRadiation
        {
            get
            {
                return this._solarRadiation;
            }
            set
            {
                this._solarRadiation = value;
            }
        }
        
        /// <summary>calculate the hourly outputs only if this is true ( 1)</summary>
        public int calculateHourly
        {
            get
            {
                return this._calculateHourly;
            }
            set
            {
                this._calculateHourly = value;
            }
        }
        
        /// <summary>Hourly Relative Humidity</summary>
        public double[] RH
        {
            get
            {
                return this._RH;
            }
            set
            {
                this._RH = value;
            }
        }
        
        /// <summary>Solar Declination</summary>
        public double solarDeclination
        {
            get
            {
                return this._solarDeclination;
            }
            set
            {
                this._solarDeclination = value;
            }
        }
        
        /// <summary>Hourly sun elevation above the horizon</summary>
        public double[] hourlySolarElevation
        {
            get
            {
                return this._hourlySolarElevation;
            }
            set
            {
                this._hourlySolarElevation = value;
            }
        }
        
        /// <summary>Hourly Incident beam irradiance at the top of the canopy</summary>
        public double[] hourlyIdir
        {
            get
            {
                return this._hourlyIdir;
            }
            set
            {
                this._hourlyIdir = value;
            }
        }
        
        /// <summary>Hourly Incident diffuse irradiance at the top of the canopy</summary>
        public double[] hourlyIdiff
        {
            get
            {
                return this._hourlyIdiff;
            }
            set
            {
                this._hourlyIdiff = value;
            }
        }
        
        /// <summary>Daily Incident beam irradiance at the top of the canopy</summary>
        public double dailyIdir
        {
            get
            {
                return this._dailyIdir;
            }
            set
            {
                this._dailyIdir = value;
            }
        }
        
        /// <summary>Daily Incident diffuse irradiance at the top of the canopy</summary>
        public double dailyIdiff
        {
            get
            {
                return this._dailyIdiff;
            }
            set
            {
                this._dailyIdiff = value;
            }
        }
        
        /// <summary>Length of the Day considering sunset/sunrise when sun elevation is exactly 0°</summary>
        public double dayLengthHorizonZero
        {
            get
            {
                return this._dayLengthHorizonZero;
            }
            set
            {
                this._dayLengthHorizonZero = value;
            }
        }
        
        /// <summary>Exrtraterestrial radiations with dayLengthHorizon0 (sunset/rise -exactly 0°)</summary>
        public double radTopAtmHorizonZero
        {
            get
            {
                return this._radTopAtmHorizonZero;
            }
            set
            {
                this._radTopAtmHorizonZero = value;
            }
        }
        
        /// <summary>Mean daily Wind Speed</summary>
        public double dailyMeanWindSpeed
        {
            get
            {
                return this._dailyMeanWindSpeed;
            }
            set
            {
                this._dailyMeanWindSpeed = value;
            }
        }
        
        /// <summary>Maximum Daily Wind Speed</summary>
        public double dailyMaxWindSpeed
        {
            get
            {
                return this._dailyMaxWindSpeed;
            }
            set
            {
                this._dailyMaxWindSpeed = value;
            }
        }
        
        /// <summary>Minimum Daily Wind Speed</summary>
        public double dailyMinWindSpeed
        {
            get
            {
                return this._dailyMinWindSpeed;
            }
            set
            {
                this._dailyMinWindSpeed = value;
            }
        }
        
        /// <summary>Hourly wind speed</summary>
        public double[] hourlyWindSpeed
        {
            get
            {
                return this._hourlyWindSpeed;
            }
            set
            {
                this._hourlyWindSpeed = value;
            }
        }
        #endregion
        
        #region IDomainClass members
        /// <summary>Domain Class description</summary>
        public virtual  string Description
        {
            get
            {
                return "Domain class for the meteorology component";
            }
        }
        
        /// <summary>Domain Class URL</summary>
        public virtual  string URL
        {
            get
            {
                return "http://";
            }
        }
        
        /// <summary>Domain Class Properties</summary>
        public virtual IDictionary<string, PropertyInfo> PropertiesDescription
        {
            get
            {
                return _parametersIO.GetCachedProperties(typeof(IDomainClass));
            }
        }
        #endregion
        
        /// <summary>Clears the values of the properties of the domain class by using the default value for the type of each property (e.g '0' for numbers, 'the empty string' for strings, etc.)</summary>
        public virtual Boolean ClearValues()
        {
            _minTair = default(System.Double);
            _maxTair = default(System.Double);
            _meanTair = default(System.Double);
            _vaporPressure = default(System.Double);
            _minShootTemperature = default(System.Double);
            _maxShootTemperature = default(System.Double);
            _dayLength = default(System.Double);
            _photoperiod = default(System.Double);
            _radTopAtm = default(System.Double);
            _HSlope = default(System.Double);
            _saturationVaporPressure = default(System.Double);
            _VPDair = default(System.Double);
            _VPDairCanopy = default(System.Double);
            _hourlyAirTemperature = new double[24];
            _hourlyVPDAir = new double[24];
            _hourlyRadiation = new double[24];
            _dayOfYear = default(System.Int32);
            _solarRadiation = default(System.Double);
            _calculateHourly = default(System.Int32);
            _RH = new double[24];
            _solarDeclination = default(System.Double);
            _hourlySolarElevation = new double[24];
            _hourlyIdir = new double[24];
            _hourlyIdiff = new double[24];
            _dailyIdir = default(System.Double);
            _dailyIdiff = default(System.Double);
            _dayLengthHorizonZero = default(System.Double);
            _radTopAtmHorizonZero = default(System.Double);
            _dailyMeanWindSpeed = default(System.Double);
            _dailyMaxWindSpeed = default(System.Double);
            _dailyMinWindSpeed = default(System.Double);
            _hourlyWindSpeed = new double[24]; ;
            // Returns true if everything is ok
            return true;
        }
        
        #region Clone
        /// <summary>Implement ICloneable.Clone()</summary>
        public virtual Object Clone()
        {
            // Shallow copy by default
            IDomainClass myclass = (IDomainClass) this.MemberwiseClone();
            _parametersIO.PopulateClonedCopy(myclass);
            return myclass;
        }
        #endregion
    }
}