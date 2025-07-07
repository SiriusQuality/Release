using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Import_Export_SQ_ICASA;
using System.IO;

namespace Import_Export_SQ_ICASA
{
    class ExportToPHIS
    {
        static int soilLayerSize;
        static int dateApplicationSize;
        static Dictionary<String, String> manToString = new Dictionary<String, String>();
        static Dictionary<String, String> sitToString = new Dictionary<String, String>();
        static Dictionary<String, String> soiToString = new Dictionary<String, String>();
        static Dictionary<String, String> sowingDates = new Dictionary<String, String>();
        static String paramMan;
        static String paramSit;
        static String paramSoi;
        public static String ToStringManagement(String json)
        {
            JObject obj = JObject.Parse(json);
            String result = "";
            foreach (KeyValuePair<String, JToken> o in obj)
            {
                if (o.Value is JObject)
                {
                    int e = 0;
                    if (o.Value.ToString().Length > 0)
                    {

                        JToken arr = ((JObject)o.Value)["FEIDATE"];
                        if (arr is JArray)
                        {
                            JArray ar = (JArray)arr;
                            for (int i = 0; i < ar.Count; i++, e++)
                            {
                                JObject sl = (JObject)ar[i];
                                foreach (KeyValuePair<String, JToken> c in sl)
                                {
                                    result += c.Value.ToString() + ";";
                                }

                            }
                        }
                        else
                        {
                            JObject ar = (JObject)arr;
                            foreach (KeyValuePair<String, JToken> c in ar)
                            {
                                result += c.Value.ToString() + ";";
                            }
                            e++;

                        }
                    }
                    if (e < dateApplicationSize)
                    {
                        for (int i = e; i < dateApplicationSize; i++)
                        {
                            result += ";;;;;;";
                        }
                    }
                }
                else
                {
                    result += o.Value.ToString() + ";";
                }
            }
            return result;
        }
        public static String ToStringSoil(String json)
        {
            JObject obj = JObject.Parse(json);
            String result = "";
            foreach (KeyValuePair<String, JToken> o in obj)
            {
                if (o.Value is JObject)
                {


                    JArray ar = (JArray)((JObject)o.Value)["SoilLayer"];
                    int e = 0;
                    for (int i = 0; i < ar.Count; i++, e++)
                    {
                        JObject sl = JObject.Parse(Export.removeAllParameters("SLCLY", ((JObject)ar[i]).ToString()));
                        sl = JObject.Parse(Export.removeAllParameters("Kql", sl.ToString()));

                        foreach (KeyValuePair<String, JToken> c in sl)
                        {
                            result += c.Value.ToString() + ";";
                        }

                    }
                    if (e < soilLayerSize)
                    {
                        for (int i = e; i < soilLayerSize; i++)
                        {
                            result += ";;;;";
                        }
                    }
                }
                else
                {
                    result += o.Value.ToString() + ";";
                }
            }
            return result;
        }
        public static String ToStringSite(String json)
        {
            JObject obj = JObject.Parse(json);
            String result = "";
            String geo = "POLYGON(( ";
            foreach (KeyValuePair<String, JToken> o in obj)
            {
                if (o.Key.Equals("FL_LAT") || o.Key.Equals("FL_LONG"))
                {
                    geo += o.Value.ToString() + " ";
                }
                else
                {
                    result += o.Value.ToString() + ";";
                }

            }

            return result + geo +"));" ;
        }
        public static void soilSize(String json)
        {
            JArray arr = JArray.Parse(json);
            int n = 0;
            foreach (JObject o in arr)
            {
                JArray oarr = (JArray)(((JObject)o["LayersArray"])["SoilLayer"]);
                n = n < oarr.Count ? oarr.Count : n;
            }
            soilLayerSize = n;
        }
        public static void dateSize(String json)
        {
            JArray arr = JArray.Parse(json);
            int n = 0;
            foreach (JObject o in arr)
            {
                JToken dateApp = o["DateApplications"];

                if (dateApp.ToString().Length > 0)
                {
                    JObject date = (JObject)dateApp;
                    JToken oarr = date["DateApplication"];

                    int count = 1;
                    if (oarr is JArray)
                    {
                        count = ((JArray)oarr).Count;
                    }
                    n = n < count ? count : n;
                }

            }
            dateApplicationSize = n;

        }
        public static void buildParamMan(String json)
        {
            JObject obj = JObject.Parse(json);
            String param = "";
            foreach (KeyValuePair<String, JToken> token in obj)
            {
                if (token.Key.Equals("DateApplications"))
                {
                    for (int i = 0; i < dateApplicationSize; i++)
                    {
                        param += "FEAMN-" + i + ";IRVAL-" + i + ";Date-" + i + ";";
                    }
                }
                else
                {
                    param += token.Key + ";";
                }
            }
            paramMan = param;
        }
        public static void buildParamSoi(String json)
        {
            JObject obj = JObject.Parse(json);
            String param = "";
            foreach (KeyValuePair<String, JToken> token in obj)
            {
                if (token.Key.Equals("LayersArray"))
                {
                    for (int i = 0; i < soilLayerSize; ++i)
                    {
                        param += "SLSAT-" + i + ";SLFC1-" + i + ";SLWP-" + i + ";SABL-" + i + ";";

                    }
                }
                else
                {
                    param += token.Key + ";";
                }
            }
            paramSoi = param;
        }
        public static void buildParamSit(String json)
        {
            JObject obj = JObject.Parse(json);
            String param = "";
            foreach (KeyValuePair<String, JToken> token in obj)
            {
                if (!token.Key.Equals("FL_LAT")&&!token.Key.Equals("FL_LONG"))
                {
                    param += token.Key + ";";


                }
            }

            paramSit = param+"Geometry;";
        }

        public static void prepareEnvironnement(String man, String sit, String soi, String csv,String sqmat,String sqcan,String sqos,String sqphy)
        {
            SQPHYtoString(sqphy);

            SQOStoString(sqos);

            SQMATtoString(sqmat);

            SQCANtoString(sqcan);

            JArray management = (JArray)((JObject)(JObject.Parse(Export.XMLtoJSON(File.ReadAllText(man)))["ManagementFile"])["ItemsArray"])["ManagementItem"];
            JArray site = (JArray)((JObject)((JObject)JObject.Parse(Export.XMLtoJSON(File.ReadAllText(sit)))["SiteFile"])["ItemsArray"])["SiteItem"];
            JArray soil = (JArray)((JObject)((JObject)JObject.Parse(Export.XMLtoJSON(File.ReadAllText(soi)))["SoilFile"])["ItemsArray"])["SoilItem"];
            soilSize(soil.ToString());
            dateSize(management.ToString());
            soil = JArray.Parse(Export.removeAllParameters("Comments", soil.ToString()));
            soil = JArray.Parse(Export.removeAllParameters("IsKqCalc", soil.ToString()));
            soil = JArray.Parse(Export.removeAllParameters("IsKqlUsed", soil.ToString()));
            soil = JArray.Parse(Export.removeAllParameters("IsOrgNCalc", soil.ToString()));
            soil = JArray.Parse(Export.removeAllParameters("Carbon", soil.ToString()));
            soil = JArray.Parse(Export.removeAllParameters("Bd", soil.ToString()));
            soil = JArray.Parse(Export.removeAllParameters("Gravel", soil.ToString()));
            soil = JArray.Parse(Export.removeAllParameters("Kq", soil.ToString()));
            soil = JArray.Parse(Export.removeAllParameters("MinNir", soil.ToString()));
            soil = JArray.Parse(Export.removeAllParameters("Ndp", soil.ToString()));
            soil = JArray.Parse(SQ_ICASA_Convertor.SQtoICASA(soil.ToString()));
            management = JArray.Parse(Export.removeAllParameters("Comments", management.ToString()));
            management = JArray.Parse(SQ_ICASA_Convertor.SQtoICASA(management.ToString()));

            site = JArray.Parse(Export.removeAllParameters("Comments", site.ToString()));
            site = JArray.Parse(Export.removeAllParameters("WeatherFiles", site.ToString()));
            site = JArray.Parse(Export.removeAllParameters("@format", site.ToString()));
            site = JArray.Parse(SQ_ICASA_Convertor.SQtoICASA(site.ToString()));
            Console.WriteLine("25%");
            foreach (JObject obj in management)
            {
                String name = (String)obj["@name"];
                String o = Export.removeAllParameters("@name", obj.ToString());
                manToString.Add(name, ToStringManagement(o));
            }
            foreach (JObject obj in soil)
            {
                String name = (String)obj["@name"];
                String o = Export.removeAllParameters("@name", obj.ToString());
                soiToString.Add(name, ToStringSoil(o));
            }
            foreach (JObject obj in site)
            {
                String name = (String)obj["@name"];
                String o = Export.removeAllParameters("@name", obj.ToString());
                sitToString.Add(name, ToStringSite(o));
            }
            Console.WriteLine("75%");

            String val = "";

            site = JArray.Parse(Export.removeAllParameters("@name", site.ToString()));
            management = JArray.Parse(Export.removeAllParameters("@name", management.ToString()));
            soil = JArray.Parse(Export.removeAllParameters("@name", soil.ToString()));
            buildParamMan(management[0].ToString());
            buildParamSoi(soil[0].ToString());
            buildParamSit(site[0].ToString());

            String[] cs = objectCSV(csv);

            String param = cs[0] + paramSit + paramSoi + paramMan;
            param = param.Substring(0, param.Length - 1) + "\n";
            for (int i = 1; i < cs.Length; i++)
            {
                String[] p = cs[i].Split(';');
                String v = cs[i] + sitToString[p[2]] + soiToString[p[3]] + manToString[p[4]];
                val += v.Substring(0, v.Length - 1) + "\n";
            }
            File.WriteAllText("./test.csv", param + val);
            Console.WriteLine("100%");

        }

        public static String[] objectCSV(String csv)
        {
            JArray obj = JArray.Parse(Export.csvToJson(File.ReadAllText(csv)));
            JArray distinct = new JArray();
            String paramDataset = "ScientificObjectURI;Date;Zadoks;CodeCV;NbG/M2;TGW;DMG/M2;AGDM/M2;GPC HH";
            String values = "";
            for (int i = 0; i < obj.Count-1; i++)
            {
                JObject ob = new JObject();
                JObject e = (JObject)obj[i];

                ob["Year"] = e["Year"];
                ob["Site"] = e["Site"];
                ob["Treatment"] = e["Treatment"];
                ob["Cultivar"] = e["Cultivar"];
                ob["Block"] = e["Block"];
                String site = "";
                if (((String)ob["Site"]).Equals("NOTT"))
                {
                    site = "INRA-BBSRC_UNOT";

                }
                else
                {
                    site = "INRA-BBSRC_" + ((String)ob["Site"]).ToUpper();

                }
                String sSuffix = "";
                if (((String)ob["Site"]).Equals("CF"))
                {
                    if (((String)ob["Year"]).Equals("2007"))
                    {
                        sSuffix = " (MG6B)";

                    }
                    else
                    {
                        sSuffix = " (CR4)";

                    }
                }
                else if (((String)ob["Site"]).Equals("Mons"))
                {
                    if (((String)ob["Year"]).Equals("2007"))
                    {
                        sSuffix = " (C4)";

                    }
                    else
                    {
                        sSuffix = " (C2)";

                    }
                }

                String soil = site + ((String)ob["Year"]).Substring(2, 2);
                String treatment = "";

                if (((String)ob["Treatment"]).Equals("N+"))
                {
                    treatment += soil + "-HN";
                }
                else
                {
                    treatment += soil + "-LN";

                }

                soil += sSuffix;

                ob["Site"] = site;
                ob["Treatment"] = treatment;
                ob["Year"] = soil;
                values += site+soil+treatment+";" + e["Sampling Date"] + ";" + e["Zadoks"] + ";" + e["CodeCV"] + ";" + (e["NbG/M2"] ?? "") + ";" + (e["TGW"] ?? "") + ";" + (e["DMG/M2"] ?? "") + ";" + (e["AGDM/M2"] ?? "") + ";" + (e["GPC HH"] ?? "")+"\n";
                
                if (!distinct.Contains(ob))
                {
                    distinct.Add(ob);
                }
            }
            File.WriteAllText("./dataset1.csv", paramDataset + "\n" + values);
            String[] lines = new String[distinct.Count + 1];
            lines[0] = "ExperimentURI;Alias;Site;Soil;ExperimentModalities;Species;Repetition;Variety;PDATE;";

            for (int i = 0; i < distinct.Count; i++)
            {

                JObject e = (JObject)distinct[i];

                String cultivar = e["Cultivar"].ToString().Substring(0, 3).ToUpper();
                if (e["Cultivar"].ToString().Contains('9'))
                {
                    cultivar = e["Cultivar"].ToString().Substring(0, 4).ToUpper();
                }
                String result = "http://www.phenome-fppn.fr/agmip/AGP1-1;" + e["Site"] + e["Year"] + e["Treatment"] + ";" + e["Site"] + ";" + e["Year"] + ";" + e["Treatment"] + ";" + "http://www.phenome-fppn.fr/id/species/" + e["Cultivar"] + ";" + e["Block"] + ";;" + sowingDates[e["Treatment"].ToString() + '¤' + e["Site"] + '¤' + e["Year"] + '¤' + cultivar]+";";

                lines[i + 1] = result;

            }
            return lines;
        }

        public static void SQMATtoString(String url)
        {
            String[] a = File.ReadAllLines(url).Where(e => !e.StartsWith("#")&&!e.Contains("yyyy-mm-dd")).ToArray();

            String[] titre=a[2].Split('\t');
            List<String> n=new List<String>(a);
            n.Remove(a[0]);
            n.Remove(a[1]);
            n.Remove(a[2]);

            a=n.ToArray();
            JArray arr = new JArray();
            foreach(String i in a){
                JObject obj = new JObject();
                String[] data = i.Split('\t');
                obj["ScientificObjectURI"] = "INRA-BBSRC_" + data[0] + "¤" + "INRA-BBSRC_" + data[1] + "¤" + "INRA-BBSRC_" + data[2] + "¤" + data[4].Substring(3);
                obj["Date"] = data[3];
                sowingDates[obj["ScientificObjectURI"].ToString()] = data[3];
                for (int e = 5; e < data.Length; e += 2)
                {
                    obj[titre[e]] = data[e];
                }
                arr.Add(obj);
            }
            arr = JArray.Parse(SQ_ICASA_Convertor.SQtoICASA(arr.ToString()));
            String result = "";
            String param = "";
            int c = 0;
            foreach (JObject obj in arr)
            {
                foreach(KeyValuePair<String,JToken> e in obj){
                    if (c == 0)
                    {
                        param += ";" + e.Key;
                    }
                    result += e.Value.ToString() + ';';
                }
                result = result.Substring(0, result.Length - 1)+'\n';
                c++;
            }
            param = param.Substring(1)+'\n';
            File.WriteAllText("./SQMAT.csv", param + result);
        }
        public static void SQCANtoString(String url)
        {
            String[] a = File.ReadAllLines(url).Where(e => !e.StartsWith("#") && !e.Contains("yyyy-mm-dd")).ToArray();
            String[] titre = a[1].Split('\t');
            List<String> n = new List<String>(a);
            n.Remove(a[0]);
            n.Remove(a[1]);
            
            a = n.ToArray();
            JArray arr = new JArray();
            foreach (String i in a)
            {
                JObject obj = new JObject();
                String[] data = i.Split('\t');
                obj["ScientificObjectURI"] = "INRA-BBSRC_" + data[0] + "¤" + "INRA-BBSRC_" + data[1] + "¤" + "INRA-BBSRC_" + data[2] + "¤" + data[4].Substring(3);
                obj["Date"] = data[5];
                obj["Sowing Date"] = data[3];
                obj["Sampling Year"] = data[6];
                obj["DOY"] = data[7];
                for (int e = 8; e < data.Length; e += 2)
                {
                    obj[titre[e]] = data[e];
                }
                arr.Add(obj);
            }
            arr = JArray.Parse(SQ_ICASA_Convertor.SQtoICASA(arr.ToString()));
            String result = "";
            String param = "";
            int c = 0;
            foreach (JObject obj in arr)
            {
                foreach (KeyValuePair<String, JToken> e in obj)
                {
                    if (c == 0)
                    {
                        param += ";" + e.Key;
                    }
                    result += e.Value.ToString() + ';';
                }
                result = result.Substring(0, result.Length - 1) + '\n';
                c++;
            }
            param = param.Substring(1) + '\n';
            File.WriteAllText("./SQCAN.csv", param + result);
        }
        public static void SQOStoString(String url)
        {
            String[] a = File.ReadAllLines(url).Where(e => !e.StartsWith("#") && !e.Contains("yyyy-mm-dd")).ToArray();
            String[] titre = a[2].Split('\t');
            List<String> n = new List<String>(a);
            n.Remove(a[0]);
            n.Remove(a[1]);
            n.Remove(a[2]);

            a = n.ToArray();
            JArray arr = new JArray();
            foreach (String i in a)
            {
                JObject obj = new JObject();
                String[] data = i.Split('\t');
                obj["ScientificObjectURI"] = "INRA-BBSRC_" + data[0] + "¤" + "INRA-BBSRC_" + data[1] + "¤" + "INRA-BBSRC_" + data[2];
                obj["Date"] = data[3];
                obj["DOY"] = data[4];
                obj["Top layer"] = data[5];
                obj["Bottom layer"] = data[6];

                for (int e = 7; e < data.Length; e += 2)
                {
                    obj[titre[e]] = data[e];
                }
                arr.Add(obj);
            }
            arr = JArray.Parse(SQ_ICASA_Convertor.SQtoICASA(arr.ToString()));
            String result = "";
            String param = "";
            int c = 0;
            foreach (JObject obj in arr)
            {
                foreach (KeyValuePair<String, JToken> e in obj)
                {
                    if (c == 0)
                    {
                        param += ";" + e.Key;
                    }
                    result += e.Value.ToString() + ';';
                }
                result = result.Substring(0, result.Length - 1) + '\n';
                c++;
            }
            param = param.Substring(1) + '\n';
            File.WriteAllText("./SQOS.csv", param + result);
        }
        public static void SQPHYtoString(String url)
        {
            String[] a = File.ReadAllLines(url).Where(e => !e.StartsWith("#") && !e.Contains("yyyy-mm-dd")).ToArray();
            String[] titre = a[3].Split('\t');
            List<String> n = new List<String>(a);
            n.Remove(a[0]);
            n.Remove(a[1]);
            n.Remove(a[2]);
            n.Remove(a[3]);

            a = n.ToArray();

            JArray arr = new JArray();
            foreach (String i in a)
            {

                JObject obj = new JObject();
                String[] data = i.Split('\t');

                obj["ScientificObjectURI"] = "INRA-BBSRC_" + data[0] + "¤" + "INRA-BBSRC_" + data[1] + "¤" + "INRA-BBSRC_" + data[2] + "¤" + data[4].Substring(3);
                obj["Date"] = data[5];
                obj["Sowing Date"] = data[3];
                obj["Sampling Year"] = data[6];
                obj["DOY"] = data[7];


                for (int e = 8; e < data.Length; e += 2)
                {
                    obj[titre[e]+((((e-8)/2)%5)+1)] = data[e];
                }
                arr.Add(obj);
            }

            arr = JArray.Parse(SQ_ICASA_Convertor.SQtoICASA(arr.ToString()));
            String result = "";
            String param = "";
            int c = 0;

            foreach (JObject obj in arr)
            {
                foreach (KeyValuePair<String, JToken> e in obj)
                {
                    if (c == 0)
                    {
                        param += ";" + e.Key;
                    }
                    result += e.Value.ToString() + ';';
                }
                result = result.Substring(0, result.Length - 1) + '\n';
                c++;
            }

            param = param.Substring(1) + '\n';
            File.WriteAllText("./SQPHY.csv", param + result);
            
        }
        public static String WeatherToCSV(String Weather, String n)
        {
            String param = "AgronomicalObjectURI;DATE;Tmin;Tmax;Rain;Radiation;Wind;Vapour Presure\n";
            String[] list = Weather.Substring(0, Weather.Length - 1).Split('\n');
            String values = "";
            foreach (String s in list)
            {
                String val = s.Replace("	", ";");
                String[] spl = val.Split(';');
                DateTime x = new DateTime(Convert.ToInt32(spl[0], 10), 1, 1).AddDays(Convert.ToInt32(spl[1], 10) - 1);

                String date = x.ToString("yyyy-MM-dd");
                val = n+ ";" + date.Replace('/', '-') + val.Substring((spl[0] + ";" + spl[1]).Length);
                values += val + "\n";
            }
            return param + values.Substring(0, values.Length - 1);

        }

        public static String ALLWeatherToCSV(IEnumerable<String> list)
        {
            String param = "AgronomicalObjectURI;DATE;Tmin;Tmax;Rain;Radiation;Wind;Vapour Presure";

            String result = "";
            foreach (String s in list)
            {
                String str = File.ReadAllText(s);
                String csv = WeatherToCSV(str, s.Split('/')[2].Split('.')[0]);
                result += csv.Substring(csv.IndexOf("\n"));

            }
            result = param + result;
            return result.Substring(0, result.Length - 1);
        }

    }
}
