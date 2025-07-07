using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
namespace Import_Export_SQ_ICASA.Cultivations
{
    class Site:Cultivation
    {

        public Site(ImportDBM dbm)
            : base(dbm)
        { }

        protected override String buildAll(bool isQuiet)
        {
            JArray arr = new JArray();

            String[] s = dbm.getWSTNames().ToArray();
            int progress0 = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int progress = ((i * 20) / (s.Length));
                if (progress > progress0)
                {
                    progress0 = progress;
                    if (!isQuiet) Console.WriteLine(((i * 20) / (s.Length)) +20 + "%");
                }
                if (!s[i].Equals("")) arr.Add(JObject.Parse((build(s[i]))));
            }
            JObject ia = new JObject(), sf = new JObject(), result = new JObject();
            ia["SITE_NAME"] = arr;
            sf["ItemsArray"] = ia;
            sf["@xmlns:xsd"] = "http://www.w3.org/2001/XMLSchema";
            sf["@xmlns:xsi"] = "http://www.w3.org/2001/XMLSchema-instance";
            result["SiteFile"] = sf;
            return result.ToString();
        }
        protected override String build(String name)
        {
            JObject source = dbm.searchObjectByName("wst_id", name);
            JObject j = Export.searchObjectKeyValue("wst_id", (String)source["wst_id"], JObject.Parse(dbm.json)["weathers"].ToString());
            
            JObject result = new JObject();
            String windvalue = "";


            result["@name"] = name;
            windvalue = Export.searchValue("wind", j["dailyWeather"].ToString());

            if (windvalue.StartsWith(";") || windvalue=="")  //provisional rule (12.02.2020)
                {
                result["@format"] = "YearJdayMinMaxRainRad"; 
            }
            else
            {
                result["@format"] = "YearJdayMinMaxRainRadWindVp";
            }


            if (!Directory.Exists(dbm.ProjectDIR + System.IO.Path.DirectorySeparatorChar + dbm.WeatherDIR))
            {
                try { Directory.CreateDirectory(dbm.ProjectDIR + System.IO.Path.DirectorySeparatorChar + dbm.WeatherDIR); }
                catch (Exception e)
                {
                    Directory.CreateDirectory(dbm.WeatherDIR);
                }

            }

            JObject weather = new JObject();
            weather["@file"] =  dbm.WeatherDIR + System.IO.Path.DirectorySeparatorChar + name + ".sr";
            JObject weatherFiles = new JObject();



            if (j["wst_lat"].ToString() != "")
            {
     
                result["FL_LAT"] =j["wst_lat"].ToString();
                
            }
            if (j["wst_long"].ToString() != "")
            {
     
                result["FL_LONG"] = j["wst_long"].ToString();
            }
            if (j["wst_elev"].ToString() != "")
            {
                
                result["FLELE"] = j["wst_elev"].ToString();
            }

            String str = "";
            foreach (JObject s in ((JArray)j["dailyWeather"]))
            {
                //writes the content of sr file only if tmax and tmin contain values different from -999, otherwise create empty file
                if (Double.Parse((String)s["tmax"]) != -999 & Double.Parse((String)s["tmin"]) != -999)  
                {
                    DateTime ds = new DateTime(Int32.Parse(((String)s["w_date"]).Substring(0, 4)), Int32.Parse(((String)s["w_date"]).Substring(4, 2)), Int32.Parse(((String)s["w_date"]).Substring(6, 2)));
                    Double meanT = (Double.Parse((String)s["tmax"]) + Double.Parse((String)s["tmin"])) / 2;

                    Double rh = 0;
                    if (!windvalue.StartsWith(";")) //provisional rule (12.02.2020)
                    {
                        if (s.ContainsKey("rhavd"))
                        {
                            rh = Double.Parse((String)s["rhavd"]);
                        }
                        else if (s.ContainsKey("rhum")) //relative humidity CIMMYT dataset
                        {
                            rh = Double.Parse((String)s["rhum"]);
                        }
                        else
                        {
                            Double tmin = Double.Parse((String)s["tmin"]);
                            Double tmax = Double.Parse((String)s["tmax"]);
                            rh = 100 * (Math.Exp((17.625 * tmin) / (243.04 + tmin)) / Math.Exp((17.625 * meanT) / (243.04 + meanT)));
                        }
                    }


                    Double vp = 0;
                    Double svp = 0;
                    if (s.ContainsKey("vprsd"))  //saturated vapor pressure [kPa]
                    {
                        svp = Double.Parse((String)s["vprsd"]) * 10;         //saturated vapor pressure [hPa]
                        vp = svp * (rh / 100);    //actual vapor pressure [hPa]
                    }
                    else if (s.ContainsKey("vprs"))  //saturated vapor pressure [kPa] CIMMYT
                    {
                        svp = Double.Parse((String)s["vprs"]) * 10;         //saturated vapor pressure [hPa]
                        vp = svp * (rh / 100);    //actual vapor pressure [hPa]
                    }
                    else
                    {
                        //Actual vapor pressure calculated from relative humidity
                        vp = ((rh / 100) * 0.6108 * 10 * Math.Exp((17.27 * meanT) / (meanT + 237.3)));
                    }
                    vp = Math.Round(vp, 2);

                    Double srad = Double.Parse((String)s["srad"]);
                    srad = Math.Round(srad, 2);

                    if (result["@format"].ToString() == "YearJdayMinMaxRainRadWindVp")
                    {
                        string vstmin = TruncString(s, "tmin", 5);
                        string vstmax = TruncString(s, "tmax", 5);
                        string vsrain = TruncString(s, "rain", 5);
                        string vwind = TruncString(s, "wind", 10);


                        str += ((String)s["w_date"]).Substring(0, 4) + "\t" + ds.DayOfYear.ToString() + "\t" + vstmin/*s["tmin"]*/ + "\t" + /*s["tmax"]*/vstmax + "\t" + /*s["rain"]*/vsrain + "\t" + srad.ToString() + "\t" + /*s["wind"]*/vwind + "\t" + vp.ToString() + "\n";
                    }
                    else
                    {
                        string vstmin = TruncString(s, "tmin", 5);
                        string vstmax = TruncString(s, "tmax", 5);
                        string vsrain = TruncString(s, "rain", 5);

                        str += ((String)s["w_date"]).Substring(0, 4) + "\t" + ds.DayOfYear.ToString() + "\t" + vstmin/*s["tmin"]*/ + "\t" + /*s["tmax"]*/vstmax + "\t" + /*s["rain"]*/vsrain + "\t" + srad.ToString() + "\n";
                    }
                }
                
            }
            //Meteo files written
            try { File.WriteAllText(dbm.ProjectDIR + System.IO.Path.DirectorySeparatorChar + dbm.WeatherDIR + System.IO.Path.DirectorySeparatorChar + name + ".sr", str); }
            catch (Exception e)
            {
                File.WriteAllText(dbm.WeatherDIR + System.IO.Path.DirectorySeparatorChar + name + ".sr", str);
            }
            weatherFiles["WeatherFile"] = weather;

            result["WeatherFiles"] = weatherFiles;
            return result.ToString();
        }


        private string TruncString(JObject sa,string n,int p)
        {
            string s=sa[n].ToString();
            s = s.Substring(0, Math.Min(p, s.Length));
            return s;
        }

        public override void run(bool isQuiet)
        //    public override void run(String str, bool isQuiet)
        {
            try
            {
                string name = dbm.SiteFilename;
                File.WriteAllText(dbm.ProjectDIR + "\\" + name + ".sqsit", (JSONtoXML(SQ_ICASA_Convertor.ICASAtoSQ(buildAll(isQuiet)))).Replace("_x0020_Date", "Date").Replace(" Date", "Date").Replace("False", "false"));
            }
            catch(Exception e)
            {
                throw new Exception("The translation of Json with the C# code has failed for Site: Check your weather data and metadata in your Excel template "+e);
            }
        }
    }
}
