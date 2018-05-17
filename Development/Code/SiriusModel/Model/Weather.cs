using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SiriusModel.InOut.Base;

using WeatherFormat = Sirius.Model.Input.WeatherFormat;
using WeatherInput = Sirius.Model.Input.WeatherInput;
using WeatherFormatID = Sirius.Model.Constant.WeatherFormatID;
using Cache = Sirius.Model.Weather.Cache;
using SiriusModel.InOut;
using SiriusModel.Model.Base;

namespace SiriusModel.Model
{
    public class Weather : UniverseLink
    {
        #region Constants

        ///The number of first days needed by DeepLayer (for calculating the initial temperature).
        public const int NumberOfFirstDays = 10;

        private int lsd_W;
        private int esd_W;
        private int lsd_S;
        private int esd_S;
        private int lsd_Sir;
        private int esd_Sir;

        public bool isUnlimitedTemperature;

        #endregion
        #region Fields

        private static readonly WeatherFormat NoWindVp;
        private static readonly WeatherFormat WindVp;
        private static readonly WeatherFormat WindVpHourly;

        private Cache cache;
        private WeatherFormat format;

        #endregion

        #region Constructors

        static Weather()
        {
            NoWindVp = new WeatherFormat 
            { 
                WeatherFormatID.Year,
                WeatherFormatID.DayOfYear,
                WeatherFormatID.TempMin,
                WeatherFormatID.TempMax,
                WeatherFormatID.Rain,
                WeatherFormatID.Radiation
            };
            WindVp = new WeatherFormat
            {
                WeatherFormatID.Year,
                WeatherFormatID.DayOfYear,
                WeatherFormatID.TempMin,
                WeatherFormatID.TempMax,
                WeatherFormatID.Rain,
                WeatherFormatID.Radiation,
                WeatherFormatID.Wind,
                WeatherFormatID.VapourPresure
            };
            WindVpHourly = new WeatherFormat
            {
                WeatherFormatID.Year,
                WeatherFormatID.DayOfYear,
                WeatherFormatID.TempH0,
                WeatherFormatID.TempH1,
                WeatherFormatID.TempH2,
                WeatherFormatID.TempH3,
                WeatherFormatID.TempH4,
                WeatherFormatID.TempH5,
                WeatherFormatID.TempH6,
                WeatherFormatID.TempH7,
                WeatherFormatID.TempH8,
                WeatherFormatID.TempH9,
                WeatherFormatID.TempH10,
                WeatherFormatID.TempH11,
                WeatherFormatID.TempH12,
                WeatherFormatID.TempH13,
                WeatherFormatID.TempH14,
                WeatherFormatID.TempH15,
                WeatherFormatID.TempH16,
                WeatherFormatID.TempH17,
                WeatherFormatID.TempH18,
                WeatherFormatID.TempH19,
                WeatherFormatID.TempH20,
                WeatherFormatID.TempH21,
                WeatherFormatID.TempH22,
                WeatherFormatID.TempH23,
                WeatherFormatID.Rain,
                WeatherFormatID.Radiation,
                WeatherFormatID.Wind,
                WeatherFormatID.VapourPresure
            };
        }

        ///<summary>
        ///Create a new Weather. It must be linked to a Element.
        ///</summary>
        public Weather(Universe universe): base(universe)
        {
        }

        #endregion

        #region methods
        
        ///<summary>
        ///Read the weather file and compute weekTemp, threeDayRain and weekRain.
        ///</summary>
        public void Init(SiteItem site, ManagementItem management)
        {
            isUnlimitedTemperature = false;

            var weatherFiles = site.WeatherFiles;

            if (site.Format == WeatherFileFormat.YearJdayMinMaxRainRad)
            {
                format=  NoWindVp;
            }
            else
            {
                if (site.Format == WeatherFileFormat.YearJdayMinMaxRainRadWindVp)
                {
                    format=  WindVp;
                }
                else
                {
                    format = WindVpHourly;
                }
            }

            var input = new WeatherInput{Files = new List<string>(), Format = format, MaxConsecutiveMissingValuesAllowed = 5};
            foreach (var file in weatherFiles) input.Files.Add(file.AbsoluteFile);

            cache = Cache.Get(site.GeoPosition, input);
            if (!cache.Start.HasValue) throw new InvalidOperationException("Weather has no starting date.");
            if (cache.Start.Value > management.SowingDate.AddDays(-(NumberOfFirstDays + 1)))
            {
                throw new InvalidOperationException("Sowing date - number of first days needed by DeepLayer "  + management.SowingDate.ToString("u").Split()[0] +" - (" + NumberOfFirstDays + ")"+ " is before the first daily weather data " + cache.Start.Value.ToString("u").Split()[0] + ".");
            }
            if (cache.Start.Value.AddDays(cache.Count) <= management.SowingDate)
            {
                throw new InvalidOperationException("Sowing date " + management.SowingDate.ToString("u").Split()[0] + " is after the last daily weather data " + cache.Start.Value.AddDays(cache.Count).ToString("u").Split()[0] + ".");
            }

            ///<Behnam>
            if (management.IsSowDateEstimate & site.SowingWindowType != 1 & site.SowingWindowType != 3 &
                site.MinSowingDate > site.MaxSowingDate)
            {
                throw new InvalidOperationException("Minimum sowing date " +
                    site.MinSowingDate.ToString("u").Split()[0] +
                    " is after maximum sowing date " + site.MaxSowingDate.ToString("u").Split()[0] + ".");
            }
            if (management.IsSowDateEstimate & site.SowingWindowType != 1 & site.SowingWindowType != 3 &
                site.MinSowingDate.AddDays(-management.SkipDays - NumberOfFirstDays - 1) < cache.Start.Value)
            {
                throw new InvalidOperationException("First date of simulation " +
                    site.MinSowingDate.AddDays(-management.SkipDays - NumberOfFirstDays - 1).ToString("u").Split()[0] +
                    " is before the first daily weather data " + cache.Start.Value.ToString("u").Split()[0] + ".");
            }
            if (management.IsSowDateEstimate & site.SowingWindowType != 1 & site.SowingWindowType != 3 &
                site.MaxSowingDate >= cache.Start.Value.AddDays(cache.Count))
            {
                throw new InvalidOperationException("Maximum sowing date " + site.MaxSowingDate.ToString("u").Split()[0] +
                    " is after the last daily weather data " +
                    cache.Start.Value.AddDays(cache.Count).ToString("u").Split()[0] + ".");
            }
        }

        public void EstimateSowingWindows(SiteItem site, ManagementItem management)
        {
            if (site.SowingWindowType == 1) // JRC winter sowing window
            {
                EstimateSowingWindow_JRC_Winter(cache, site);
                site.ESD_W = ESD_Winter(cache.Start.Value.Year);
                site.LSD_W = LSD_Winter(cache.Start.Value.Year);
            }
            if (site.SowingWindowType == 2) // JRC spring sowing window
            {
                EstimateSowingWindow_JRC_Spring(cache, site);
                site.ESD_S = ESD_Spring(cache.Start.Value.Year, site);
                site.LSD_S = LSD_Spring(cache.Start.Value.Year, site);

            }
            if (site.SowingWindowType == 3) // SiriusQuality method (based on nominal sowing date)
            {
                EstimateSowingWindow_Sirius(cache, site, management);
                site.ESD_Sir = ESD_Sirius(cache.Start.Value.Year, site);
                site.LSD_Sir = LSD_Sirius(cache.Start.Value.Year, site);
            }
        }


        public DateTime LSD_Fixed(int year, SiteItem site)
        {
            var yy = site.MaxSowingDate.Year - site.MinSowingDate.Year;
            DateTime datex = new DateTime(year + yy, site.MaxSowingDate.Month, site.MaxSowingDate.Day);
            return datex;
        }

        public DateTime ESD_Fixed(int year, SiteItem site)
        {
            DateTime datex = new DateTime(year, site.MinSowingDate.Month, site.MinSowingDate.Day);
            return datex;
        }


        public void EstimateSowingWindow_Sirius(Cache cache, SiteItem site, ManagementItem management)
        {
            DateTime date1;
            DateTime date2;
            DateTime datex;
            int daynx;
            int ndays;
            double tav10d;
            double pcp3d;
            int HalfSowWindow = (int)(InitSowWindow / 2);
            List<int> esd = new List<int>();
            List<int> lsd = new List<int>();

            var TempThr = site.TempThr;
            var TempSum = site.TempSum;
            var PcpSum = site.PcpSum;
            var CheckDaysPcp = site.CheckDaysPcp;
            var CheckDaysTemp = site.CheckDaysTemp;

            var year1 = cache.Start.Value.Year;
            var year2 = cache.End.Value.Year;
            var yy = management.SowingDate.AddDays(HalfSowWindow).Year - management.SowingDate.AddDays(-HalfSowWindow).Year;

            date1 = new DateTime(year1, management.SowingDate.Month, management.SowingDate.Day);
            esd_Sir = date1.DayOfYear;
            lsd_Sir = date1.DayOfYear;
            esd.Add(esd_Sir);
            lsd.Add(lsd_Sir);

            for (var year = year1; year <= year2; ++year)
            {
                date1 = new DateTime(year, management.SowingDate.Month, management.SowingDate.Day);
                date2 = date1.AddDays(HalfSowWindow);
                ndays = (int)(date2 - date1).TotalDays;

                if (date2 <= cache.End.Value && date1 >= cache.Start.Value)
                {
                    for (var dayn = 1; dayn <= ndays; ++dayn)
                    {
                        datex = date1.AddDays(dayn - 1);
                        daynx = datex.DayOfYear;
                        tav10d = cache.TemperatureMean(datex.AddDays(-1), CheckDaysTemp);

                        if (tav10d >= TempThr)
                        {
                            pcp3d = cache.Rain(datex.AddDays(-1), CheckDaysPcp);

                            if (pcp3d >= PcpSum)
                            {
                                var xx = datex.Year - year;
                                lsd.Add(xx * 365 + daynx);
                                break;
                            }
                        }
                    }
                }

                date2 = new DateTime(year, management.SowingDate.Month, management.SowingDate.Day);
                date1 = date2.AddDays(-HalfSowWindow);
                if (date1 < cache.Start.Value)
                {
                    date1 = cache.Start.Value;
                }
                ndays = (int)(date2 - date1).TotalDays;

                if (date2 <= cache.End.Value)
                {
                    for (var dayn = 1; dayn <= ndays; ++dayn)
                    {
                        datex = date2.AddDays(-dayn + 1);
                        daynx = datex.DayOfYear;
                        tav10d = cache.TemperatureMean(datex.AddDays(-1), CheckDaysTemp);

                        if (tav10d >= TempThr)
                        {
                            pcp3d = cache.Rain(datex.AddDays(-1), CheckDaysPcp);

                            if (pcp3d >= PcpSum)
                            {
                                var xx = datex.Year - year;
                                esd.Add(xx * 365 + daynx);
                                break;
                            }
                        }
                    }
                }
            }
            esd_Sir = (int)Percentile(esd, 0.25);
            lsd_Sir = (int)Percentile(lsd, 0.75);
        }

        public DateTime LSD_Sirius(int year, SiteItem site)
        {
            var xx = 0;
            if (lsd_Sir - esd_Sir < site.MinSowWinLength) xx = (int)(site.MinSowWinLength - lsd_Sir + esd_Sir + 0.5) / 2;
            DateTime date1 = new DateTime(year, 1, 1).AddDays(lsd_Sir - 1 + xx);
            return date1;
        }

        public DateTime ESD_Sirius(int year, SiteItem site)
        {
            var xx = 0;
            if (lsd_Sir - esd_Sir < site.MinSowWinLength) xx = (int)(site.MinSowWinLength - lsd_Sir + esd_Sir + 0.5) / 2;
            DateTime date1 = new DateTime(year, 1, 1).AddDays(esd_Sir - 1 - xx);
            return date1;
        }


        public void EstimateSowingWindow_JRC_Spring(Cache cache, SiteItem site)
        {
            DateTime date1;
            DateTime date2;
            DateTime datex;
            int daynx;
            int ndays;
            double tav10d;
            double pcp3d;
            esd_S = 366;
            lsd_S = 0;
            List<int> esd = new List<int>();
            List<int> lsd = new List<int>();

            var year1 = cache.Start.Value.Year;
            var year2 = cache.End.Value.Year;
            var yy = site.MaxSowingDate.Year - site.MinSowingDate.Year;

            var TempThr = site.TempThr;
            var TempSum = site.TempSum;
            var PcpSum = site.PcpSum;
            var CheckDaysPcp = site.CheckDaysPcp;
            var CheckDaysTemp = site.CheckDaysTemp;

            for (var year = year1; year <= year2; ++year)
            {
                date1 = new DateTime(year, site.MinSowingDate.Month, site.MinSowingDate.Day);
                date2 = new DateTime(year + yy, site.MaxSowingDate.Month, site.MaxSowingDate.Day);
                ndays = (int)(date2 - date1).TotalDays;

                if (date2 <= cache.End && date1 >= cache.Start.Value.AddDays(-CheckDaysTemp))
                {
                    for (var dayn = 1; dayn <= ndays; ++dayn)
                    {
                        datex = date1.AddDays(dayn - 1);
                        daynx = datex.DayOfYear;
                        tav10d = cache.TemperatureMean(datex.AddDays(-1), CheckDaysTemp);

                        if (tav10d >= TempThr)
                        {
                            pcp3d = cache.Rain(datex.AddDays(-1), CheckDaysPcp);

                            if (pcp3d >= PcpSum)
                            {
                                var xx = datex.Year - year;
                                esd.Add(xx * 365 + daynx);
                                lsd.Add(xx * 365 + daynx);
                                break;
                            }
                        }
                    }
                }
            }

            if (esd.Count == 0)
            {
                date1 = new DateTime(year1, site.MinSowingDate.Month, site.MinSowingDate.Day);
                date2 = new DateTime(year1 + yy, site.MaxSowingDate.Month, site.MaxSowingDate.Day);
                esd_S = (int)(date1.DayOfYear + date2.DayOfYear - 0.5) / 2;
            }
            else esd_S = (int)Percentile(esd, 0.25);

            if (lsd.Count == 0)
            {
                date1 = new DateTime(year1, site.MinSowingDate.Month, site.MinSowingDate.Day);
                date2 = new DateTime(year1 + yy, site.MaxSowingDate.Month, site.MaxSowingDate.Day);
                lsd_S = (int)(date1.DayOfYear + date2.DayOfYear + 0.5) / 2;
            }
            else lsd_S = (int)Percentile(lsd, 0.75);
        }

        public DateTime LSD_Spring(int year, SiteItem site)
        {
            var xx = 0;
            if (lsd_S - esd_S < site.MinSowWinLength) xx = (int)(site.MinSowWinLength - lsd_S + esd_S + 0.5) / 2;
            var yy = site.MaxSowingDate.Year - site.MinSowingDate.Year;
            DateTime date1 = new DateTime(year, 1, 1).AddDays(lsd_S - 1 + xx);
            DateTime datex = new DateTime(year + yy, site.MaxSowingDate.Month, site.MaxSowingDate.Day);
            return (date1 < datex) ? date1 : datex;
        }

        public DateTime ESD_Spring(int year, SiteItem site)
        {
            var xx = 0;
            if (lsd_S - esd_S < site.MinSowWinLength) xx = (int)(site.MinSowWinLength - lsd_S + esd_S + 0.5) / 2;
            DateTime date1 = new DateTime(year, 1, 1).AddDays(esd_S - 1 - xx);
            DateTime datex = new DateTime(year, site.MinSowingDate.Month, site.MinSowingDate.Day);
            return (date1 > datex) ? date1 : datex;
        }


        public void EstimateSowingWindow_JRC_Winter(Cache cache, SiteItem site)
        {
            DateTime date1;
            DateTime date2;
            DateTime datex;
            int dayn1;
            int dayn2;
            int daynx;
            int ndays;
            double tempsum;
            esd_W = 366;
            lsd_W = 0;

            var MinLength = site.MinSowWinLength;
            var TempThr = site.TempThr;
            var TempSum = site.TempSum;
            var PcpSum = site.PcpSum;
            var CheckDaysPcp = site.CheckDaysPcp;
            var CheckDaysTemp = site.CheckDaysTemp;

            var year1 = cache.Start.Value.Year;
            var year2 = cache.End.Value.Year;

            for (var year = year1; year <= year2; ++year)
            {
                date1 = new DateTime(year, 12, 30);
                date2 = new DateTime(year, 09, 01);
                dayn1 = date1.DayOfYear;
                dayn2 = date2.DayOfYear;
                ndays = dayn1 - dayn2 + 1;

                if (date1 <= cache.End && date2 >= cache.Start)
                {
                    tempsum = 0;

                    for (var dayn = 1; dayn <= ndays; ++dayn)
                    {
                        datex = date1.AddDays(-dayn+1);
                        daynx = datex.DayOfYear;

                        tempsum += Math.Max(0, cache.TemperatureMean(datex) - TempThr);

                        if (tempsum >= TempSum || dayn == ndays)
                        {
                            if (datex.DayOfYear > lsd_W) lsd_W = datex.DayOfYear;
                            break;
                        }
                    }
                }
            }
            esd_W = lsd_W - MinLength;
        }

        public DateTime LSD_Winter(int year)
        {
            return new DateTime(year, 1, 1).AddDays(lsd_W - 1);
        }

        public DateTime ESD_Winter(int year)
        {
            return new DateTime(year, 1, 1).AddDays(esd_W - 1);
        }

        public double Percentile(List<int> sequence, double percentile)
        {
            sequence.Sort();
            int N = sequence.Count;
            double n = (N - 1) * percentile + 1;
            // Another method: double n = (N + 1) * excelPercentile;
            if (n == 1d) return sequence[0];
            else if (n == N) return sequence[N - 1];
            else
            {
                int k = (int)n;
                double d = n - k;
                return sequence[k - 1] + d * (sequence[k] - sequence[k - 1]);
            }
        }

        ///</Behnam>


        ///<Behnam 92015.10.29)>
        ///<Comment>
        ///Whenever isUnlimitedTemperature is TRUE, temperature values are first 
        ///checked against the threshold value, updated if needed, and then returned.
        ///</Comment>
        ///</Behnam>

        public bool IsWindAndVpDefined()
        {
            return format == WindVp;
        }

        public List<double> CheckTemperatures(DateTime theDate)
        {
            double tmax = cache.TemperatureMax(theDate);
            double tmin = cache.TemperatureMin(theDate);
            double tmean = (tmin + tmax) / 2;
            double cut = tmax - MaxTempThreshold;
            if (cut > 0)
            {
                tmin = tmin - cut;
                tmax = tmax - cut;
                tmean = (tmin + tmax) / 2;
            }
            List<double> Temps = new List<double>();
            Temps.Add(tmin); Temps.Add(tmax); Temps.Add(tmean);
            return Temps;
        }

        ///<summary>
        ///Acces to daily minTemp.
        ///</summary>
        ///<param name="theDate">The date to get infos.</param>
        ///<returns>minTemp</returns>
        public double MinTemp(DateTime theDate)
        {
            if (isUnlimitedTemperature) return CheckTemperatures(theDate)[0];
            else return cache.TemperatureMin(theDate);
        }

        ///<summary>
        ///Acces to daily maxTemp.
        ///</summary>
        ///<param name="theDate">The date to get infos.</param>
        ///<returns>maxTemp</returns>
        public double MaxTemp(DateTime theDate)
        {
            if (isUnlimitedTemperature) return CheckTemperatures(theDate)[1];
            else return cache.TemperatureMax(theDate);
        }

        ///<summary>
        ///Acces to daily hourly temperatures. UnlimitedTemperature is not implemented for hourly temp yet
        ///</summary>
        ///<param name="theDate">The date to get infos.</param>
        ///<returns>hourlyTemp</returns>
        public double[] HourlyTemp(DateTime theDate)
        {
            return cache.HourlyTemperature(theDate);
        }
        ///<summary>
        ///Acces to daily meanTemp.
        ///</summary>
        ///<param name="theDate">The date to get infos.</param>
        ///<returns>meanTemp</returns>
        public double MeanTemp(DateTime theDate)
        {
            if (isUnlimitedTemperature) return CheckTemperatures(theDate)[2];
            else return cache.TemperatureMean(theDate);
        }

        ///<summary>
        ///Acces to average mean air temperature between two specified dates.
        public double MeanTemp(DateTime theDate1, DateTime theDate2)
        {
            int dayn = (int)(theDate2 - theDate1).TotalDays;
            return MeanTemp(theDate2.AddDays(1), dayn);
        }

        ///<summary>
        ///Acces to average mean air temperature between an specified date and a number of days before.
        public double MeanTemp(DateTime theDate, int dayn)
        {
            return cache.TemperatureMean(theDate.AddDays(1), dayn);
        }

        ///<summary>
        ///Acces to daily weekTemp.
        ///</summary>
        ///<param name="theDate">The date to get infos.</param>
        ///<returns></returns>
        public double WeekTemp(DateTime theDate)
        {
            if (isUnlimitedTemperature)
            {
                double tmean = 0;
                for (var i = 0; i <= 6; ++i)
                {
                    tmean += CheckTemperatures(theDate.AddDays(-i))[2];
                }
                return tmean / 7;
            }
            else return cache.TemperatureMean(theDate, 7);
        }

        ///<summary>
        ///Get the mean temperature during NumberOfFirstDays before sowing
        ///</summary>
        ///<param name="theDate">The end date of the period.</param>
        ///<param name="nbDays">The number of days in the perdiod</param>
        ///<returns>The mean temperature for the period</returns>

        ///<Behnam (2015.10.28)>
        ///<Comment>SowingDate was replaced by FinalSowingDate</Comment>
        public double GetMeanTemperatureBeforeSowing()
        {
            if (isUnlimitedTemperature)
            {
                double tmean = 0;
                for (var i = 0; i <= NumberOfFirstDays - 1; ++i)
                {
                    tmean += CheckTemperatures(FinalSowingDate.AddDays(-i))[2];
                }
                return tmean / NumberOfFirstDays;
            } 
            else return cache.TemperatureMean(FinalSowingDate, NumberOfFirstDays);
        }
        ///</Behnam>

        public double GetMeanTemperatureBeforeSowing(DateTime FinalSowingDate)
        {
            if (isUnlimitedTemperature)
            {
                double tmean = 0;
                for (var i = 0; i <= NumberOfFirstDays - 1; ++i)
                {
                    tmean += CheckTemperatures(FinalSowingDate.AddDays(-i))[2];
                }
                return tmean / NumberOfFirstDays;
            }
            else return cache.TemperatureMean(FinalSowingDate, NumberOfFirstDays);
        }

        ///<summary>
        ///Acces to daily rain.
        ///</summary>
        ///<param name="theDate">The date to get infos.</param>
        ///<returns>rain</returns>
        public double Rain(DateTime theDate)
        {
            return cache.Rain(theDate) * Run.MMwaterToGwater;
        }

        ///<summary>
        ///Acces to daily threeDayRain.
        ///</summary>
        ///<param name="theDate">The date to get infos.</param>
        ///<returns>threeDayRain</returns>
        public double ThreeDayRain(DateTime theDate)
        {
            return cache.Rain(theDate, 3) * Run.MMwaterToGwater;
        }

        /// <summary>
        /// <Modification: Behnam (2015.11.03)>
        public double CumRainMM(DateTime theDate, int nbdays, bool isBackWard)
        {
            double cumpcp = 0;
            if (isBackWard) cumpcp = cache.Rain(theDate, nbdays);
            else
            {
                for (var i = 0; i < nbdays; ++i)
                {
                    cumpcp += cache.Rain(theDate.AddDays(i));
                }
            }
            return cumpcp;
        }
        ///</Behnam>

        ///<summary>
        ///Acces to daily rad.
        ///</summary>
        ///<param name="theDate">The date to get infos.</param>
        ///<returns>rad</returns>
        public double Rad(DateTime theDate)
        {
            return cache.Radiation(theDate);
        }

        public double PAR(DateTime theDate)
        {
            return cache.Radiation(theDate) * 0.48;
        }

        ///<summary>
        ///Acces to daily weekRain.
        ///</summary>
        ///<param name="theDate">The date to get infos.</param>
        ///<returns>weekRain</returns>
        public double WeekRain(DateTime theDate)
        {
            return cache.Rain(theDate, 7) * Run.MMwaterToGwater;
        }

        ///<summary>
        ///Acces to daily wind.
        ///</summary>
        ///<param name="theDate">The date to get infos.</param>
        ///<returns>wind</returns>
        public double Wind(DateTime theDate)
        {
            return cache.Wind(theDate);
        }

        ///<summary>
        ///Acces to daily vp.
        ///</summary>
        ///<param name="theDate">The date to get infos.</param>
        ///<returns>vp</returns>
        public double Vp(DateTime theDate)
        {
            return cache.VapourPresure(theDate);
        }

        ///<summary>
        ///Get the mean temperature for a given period.
        ///</summary>
        ///<param name="theDate">The end date of the period.</param>
        ///<param name="nbDays">The number of days in the perdiod</param>
        ///<returns>The mean temperature for the period</returns>
        /*public double GetMeanTemperature(DateTime theDate, int nbDays)
        {
            return cache.TemperatureMean(theDate, nbDays);
        }*/

        #endregion

        public static void Clear()
        {
            Cache.Clear();
        }

        /*public override void Dispose()
        {
            cache = null;
            base.Dispose();
        }*/
    }
}
