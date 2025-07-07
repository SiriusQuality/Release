using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Xml;
using System.Xml.Linq;


namespace Import_Export_SQ_ICASA
{
    class Export
    {
        /*Most of the functions are here to export SiriusQuality data
         * The functions usefull for the import are:
         *  String ObsjsonToCSV(JArray content,char seperator)
         *  String JSONtoXML(String json)
        */


        //The function first write the attributes
        //Then it writes the data
        public static String ObsjsonToCSV(JArray content, char seperator)
        {
            String attributes = "";

            foreach (KeyValuePair<String, JToken> s in (JObject)content[0])
            {
                attributes += s.Key + seperator;

                if (s.Value is JArray)
                {
                    attributes += s.Key + seperator;
                }

            }

            attributes = attributes.Substring(0, attributes.Length - 1) + "\n";
            foreach (JObject obj in content)
            {
                foreach (KeyValuePair<String, JToken> s in obj)
                {
                    if (s.Value is JArray)
                    {
                        foreach (JObject o in (JArray)s.Value)
                        {
                            attributes += (String)((JObject)o)[s.Key] + seperator;

                        }
                    }
                    else
                    {
                        attributes += (String)s.Value + seperator;
                    }
                }
                attributes = attributes.Substring(0, attributes.Length - 1) + "\n";
            }
            return attributes;

        }
        public static String weatherToJSON(System.Collections.Generic.IEnumerable<string> n)
        {
            JObject finalJson = new JObject();
            foreach (String s in n)
            {

                JObject obj = new JObject();
                String[] p = File.ReadAllText(s).Split('\n');
                for (int i = 0; i < p.Length - 1; ++i)
                {
                    obj["" + i] = p[i];
                }
                finalJson[s] = obj;
            }
            return finalJson.ToString();
        }
        public static String textToJson(String address)
        {



            //Get all lines
            String[] a = File.ReadAllLines(address).Where(e => !e.StartsWith("#")).ToArray();
            //Get all titles
            String[] titles = a[1].Split('	');
            //Starting to filter all usefull units in the new array of size count 
            String[] units = a[2].Split('	').Where(e => e.Length > 0 && !e.Equals("yyyy-mm-dd")).ToArray();
            //Starts to get the object part of text (average/sd)
            int n = 0;
            for (int i = 0; i < titles.Length && n == 0; i++)
            {
                if (titles[i].Equals(titles[i + 1]))
                {
                    n = i;
                }
            }
            //Declaration of json object
            JObject finalJson = new JObject();

            for (int e = 5; e < a.Length; e++)
            {
                //Split values
                String[] values = a[e].Split('	');
                JObject o = new JObject();

                for (int i = 0; i < titles.Length; i++)
                {
                    o[titles[i]] = values[i];

                    if (i >= n)
                    {
                        ++i;
                    }


                }
                finalJson["" + (e - 5)] = o;
            }
            return finalJson.ToString();
        }
        public static String XMLtoJSON(String xml)
        {
            //Charge l'objet XML grâce au contenu XML
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            //Conversion
            return JObject.Parse(JsonConvert.SerializeXmlNode(doc)).ToString();
        }

        public static String searchValue(String attribut, String json)
        {
            String values = "";

            if (json.StartsWith("["))
            {
                JArray arr = JArray.Parse(json);
                for (int i = 0; i < arr.Count; ++i)
                {
                    values += searchValue(attribut, arr[i].ToString());

                }
            }
            else
            {
                JObject obj = JObject.Parse(json);
                foreach (KeyValuePair<String, JToken> i in obj)
                {
                    if (i.Value is JObject)
                    {
                        values += searchValue(attribut, ((JObject)i.Value).ToString());
                    }
                    else if (i.Value is JArray)
                    {
                        values += searchValue(attribut, ((JArray)i.Value).ToString());
                    }
                    else
                    {
                        values += i.Key.Equals(attribut) ? ((String)i.Value) + ";" : "";
                    }

                }
            }
            return values;
        }


            public static String searchFirstValue(String attribut, String json)
        {
            String value = "";

            if (json.StartsWith("["))
            {
                JArray arr = JArray.Parse(json);
                for (int i = 0; i < arr.Count; ++i)
                {
                    value = searchFirstValue(attribut, arr[i].ToString());
                    if (value != "") return value;
                }
            }
            else
            {
                JObject obj = JObject.Parse(json);
                foreach (KeyValuePair<String, JToken> i in obj)
                {
                    if (i.Value is JObject)
                    {
                        value = searchFirstValue(attribut, ((JObject)i.Value).ToString());
                        if (value != "") return value;
                    }
                    else if (i.Value is JArray)
                    {
                        value = searchFirstValue(attribut, ((JArray)i.Value).ToString());
                        if (value != "") return value;
                    }
                    else
                    {
                        value = i.Key.Equals(attribut) ? ((String)i.Value) : "";
                        if (value != "") return value;
                    }

                }
            }
            return value;
        }

        

        public static String removeAllParameters(String attribut, String json)
        {
            JObject result = null;

            if (json.StartsWith("["))
            {
                JArray arr = JArray.Parse(json);
                for (int i = 0; i < arr.Count && result == null; ++i)
                {
                    arr[i] = JObject.Parse(removeAllParameters(attribut, arr[i].ToString()));

                }
                return arr.ToString();
            }
            else
            {
                JObject obj = JObject.Parse(json);
                if (obj.ContainsKey(attribut))
                {
                    obj.Remove(attribut);
                }
                foreach (KeyValuePair<String, JToken> i in obj)
                {
                    if (i.Value is JObject)
                    {
                        obj[i.Key] = JObject.Parse(removeAllParameters(attribut, ((JObject)i.Value).ToString()));
                    }
                    else if (i.Value is JArray)
                    {
                        obj[i.Key] = JArray.Parse(removeAllParameters(attribut, ((JArray)i.Value).ToString()));
                    }

                }
                return obj.ToString();
            }
        }
        public static JObject searchObjectKeyValue(String attribut, String value, String json)
        {
            JObject result = null;

            if (json.StartsWith("["))
            {
                JArray arr = JArray.Parse(json);
                for (int i = 0; i < arr.Count && result == null; ++i)
                {
                    result = searchObjectKeyValue(attribut, value, arr[i].ToString());

                }
                return result;
            }
            else
            {
                JObject obj = JObject.Parse(json);
                foreach (KeyValuePair<String, JToken> i in obj)
                {
                    if (i.Value is JObject)
                    {
                        result = searchObjectKeyValue(attribut, value, ((JObject)i.Value).ToString());
                    }
                    else if (i.Value is JArray)
                    {
                        result = searchObjectKeyValue(attribut, value, ((JArray)i.Value).ToString());
                    }
                    else if (i.Key.ToLower().Equals(attribut) && ((String)i.Value).Equals(value))
                    {
                        result = obj;
                    }

                    if (result != null)
                    {
                        return result;
                    }

                }
            }
            return null;
        }
        //The json is first converted in XmlDocument. 
        //Then it's parsed into XDocument and we get our XML with the ToString() function
        public static String JSONtoXML(String json)
        {
            return "<?xml version=\"1.0\"?>\n" + XDocument.Parse(JsonConvert.DeserializeXmlNode(json).InnerXml).ToString();
        }
        public static String JSONtoCSV(String json)
        {
            JObject obj = JObject.Parse(json);
            String attr = "";
            String val = "";
            foreach (KeyValuePair<String, JToken> s in obj)
            {
                attr += s.Key + ";";
                val += ((String)s.Value) + ";";
            }
            attr = attr.Substring(0, attr.Length - 1);
            val = val.Substring(0, val.Length - 1);

            return attr + "\n" + val;
        }
        public static String JSONtoCSVNoAttributes(String json)
        {
            JObject obj = JObject.Parse(json);
            String val = "";
            foreach (KeyValuePair<String, JToken> s in obj)
            {
                val += ((String)s.Value) + ";";
            }
            val = val.Substring(0, val.Length - 1);

            return val;
        }
        public static String JSONtoCSVNoValue(String json)
        {
            JObject obj = JObject.Parse(json);
            String attr = "";
            foreach (KeyValuePair<String, JToken> s in obj)
            {
                attr += s.Key + ";";
            }
            attr = attr.Substring(0, attr.Length - 1);

            return attr;
        }




        public static String[] ObservationToCSV(String can, String mat, String oln, String exp)
        {
            String[] OLN = SQolnToCSV(SQ_ICASA_Convertor.SQtoICASA(textToJson(oln)), exp);
            String[] CAN = SQcanToCSV(SQ_ICASA_Convertor.SQtoICASA(textToJson(can)), exp);
            String[] MAT = SQmatToCSV(SQ_ICASA_Convertor.SQtoICASA(textToJson(mat)), exp);
            String[] result = new String[4];
            result[0] = "URI;Alias;Experiment;TRT_NAME;SITE_NAME;SOIL_NAME\n" + CAN[0] + "\n" + MAT[0] + "\n" + OLN[0];
            result[1] = CAN[1];
            result[2] = MAT[1];
            result[3] = OLN[1];


            return result;

        }

        public static String WeatherToCSV(String Weather, String n)
        {
            String name = "http://www.agmip/arizona/hsc/weather/hsc" + n;
            String param = "AgronomicalObjectURI;DATE;Tmin;Tmax;Rain;Radiation;Wind;Vapour Presure\n";
            String[] list = Weather.Substring(0, Weather.Length - 1).Split('\n');
            String values = "";
            foreach (String s in list)
            {
                String val = s.Replace("	", ";");
                String[] spl = val.Split(';');
                DateTime x = new DateTime(Convert.ToInt32(spl[0], 10), 1, 1).AddDays(Convert.ToInt32(spl[1], 10) - 1);

                String date = x.ToString("yyyy-MM-dd");
                val = name + ";" + date.Replace('/', '-') + val.Substring((spl[0] + ";" + spl[1]).Length);
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
        public static String SiteToCSV(String json, String experiment)
        {
            JArray arr = (JArray)((JObject)((JObject)JObject.Parse(json)["SiteFile"])["ItemsArray"])["FL_NAME"];
            String values = "";
            String attr = "Alias;Geometry;ExperimentURI;Type;";

            for (int i = 0; i < arr.Count; ++i)
            {

                JObject obj = (JObject)arr[i];
                String geometry = "POLYGON((" + obj["FL_LAT"] + " " + obj["FL_LONG"] + "))";
                String com = (String)obj["Comments"];

                obj.Remove("FL_LAT");
                obj.Remove("FL_LONG");
                String val = obj["@name"] + ";" + geometry + ";" + experiment + ";block;";
                obj.Remove("@name");
                obj.Remove("@format");
                obj.Remove("WeatherFiles");

                foreach (KeyValuePair<String, JToken> tupple in obj)
                {
                    val += (String)tupple.Value + ";";
                    if (!attr.Contains("\n"))
                    {
                        attr += tupple.Key + ";";
                    }
                }
                values += val.Substring(0, val.Length - 1) + "\n";
                if (!attr.Contains("\n"))
                {
                    attr = attr.Substring(0, attr.Length - 1) + "\n";
                }

            }

            values = values.Substring(0, values.Length - 1);
            return attr + values;
        }

        public static String[] SoilToCSV(String XML, String experiment)
        {
            JObject obj = (JObject)((JObject)((JObject)JObject.Parse(XMLtoJSON(XML))["SoilFile"])["ItemsArray"])["SoilItem"];
            String value2 = "";
            String value = "";
            String name = experiment + "/" + obj["@name"];
            String com = (String)obj["Comments"];

            obj["Comments"] = com.Replace("\n", "\\n").Replace("\r", "\\r");
            value += name + ";" + obj["@name"] + ";" + experiment + ";";
            obj.Remove("@name");
            foreach (KeyValuePair<String, JToken> tupple in obj)
            {
                if (tupple.Key.Equals("LayersArray"))
                {
                    JArray arr = (JArray)((JObject)obj["LayersArray"])["SoilLayer"];
                    for (int i = 0; i < arr.Count; i++)
                    {
                        value2 += name + ";" + (DateTime.Today.ToString("yyyy-MM-dd")) + ";";
                        foreach (KeyValuePair<String, JToken> t in (JObject)arr[i])
                        {
                            value2 += (String)t.Value + ";";
                        }
                        value2 = value2.Substring(0, value2.Length - 1) + "\n";
                    }
                    value2 = value2.Substring(0, value2.Length - 1) + "\n";
                }
                else
                {
                    value += (String)tupple.Value + ";";
                }

            }
            value = value.Substring(0, value.Length - 1);
            String attr = "URI;Alias;Experiment;Comments;IsKqCalc;IsKqlUsed;IsOrgNCalc;CtoN;Carbon;Bd;Gravel;Clay;Kq;Ko;No;MinNir;Ndp\n";
            String attr2 = "AgronomicalObjectURI;DATE;Clay;Kql;SSAT;SDUL;SLL;Depth\n";
            String[] result = new String[2];
            result[0] = attr + value;
            result[1] = attr2 + value2;
            return result;
        }

        public static String[] ManagementToCSV(String XML, String experiment)
        {
            JArray arr = (JArray)((JObject)((JObject)JObject.Parse(XMLtoJSON(XML))["ManagementFile"])["ItemsArray"])["ManagementItem"];

            String values = "";
            String values2 = "";
            for (int i = 0; i < arr.Count; i++)
            {
                JObject obj = (JObject)arr[i];

                String value2 = "";
                String value = "";


                String name = experiment + "/" + obj["@name"];
                String com = (String)obj["Comments"];

                obj["Comments"] = com.Replace("\n", "\\n").Replace("\r", "\\r");
                value += name + ";" + obj["@name"] + ";" + experiment + ";";
                obj.Remove("@name");

                foreach (KeyValuePair<String, JToken> tupple in obj)
                {
                    if (tupple.Key.Equals("GrowthStageApplications"))
                    {
                        JArray arr2 = (JArray)((JObject)obj["GrowthStageApplications"])["GrowthStageApplications"];
                        for (int e = 0; e < arr2.Count; e++)
                        {

                            String[] d = ((String)((JObject)(arr2[e]))["GrowthStage"]).Split(' ');
                            String b = d[0];
                            value2 += name + ";" + b  + ";";

                            ((JObject)(arr2[e])).Remove("GrowthStage");
                            foreach (KeyValuePair<String, JToken> t in (JObject)arr2[e])
                            {
                                value2 += (String)t.Value + ";";

                            }
                            value2 = value2.Substring(0, value2.Length - 1) + "\n";
                        }
                        values2 += value2;
                    }
                    else if (tupple.Key.Equals("DateApplications"))
                    {
                        JArray arr2 = (JArray)((JObject)obj["DateApplications"])["DateApplication"];
                        for (int e = 0; e < arr2.Count; e++)
                        {

                            String[] d = ((String)((JObject)(arr2[e]))["Date"]).Split(' ');
                            String[] b = d[0].Split('/');
                            value2 += name + ";" + (new DateTime(Convert.ToInt32(b[2]), Convert.ToInt32(b[0]), Convert.ToInt32(b[1]))).ToString("yyyy-MM-dd") + ";";

                            ((JObject)(arr2[e])).Remove("Date");
                            foreach (KeyValuePair<String, JToken> t in (JObject)arr2[e])
                            {
                                value2 += (String)t.Value + ";";

                            }
                            value2 = value2.Substring(0, value2.Length - 1) + "\n";
                        }
                        values2 += value2;
                    }
                    else
                    {
                        value += (String)tupple.Value + ";";
                    }

                }
                values += value.Substring(0, value.Length - 1) + '\n';
            }
            String attr = "URI;Alias;Experiment;Comments;Species;ExperimentName;IsSowDateEstimate;DoRelax;SowingDate;SkipDays;CheckDaysTemp;CheckDaysPcp;CheckDepth;CumPcpThr;SoilMoistThr;TAveThr;TMinThr;SoilWorkabThr;SowingDensity;IsTotalNitrogen;TotalNApplication;IsNTrendApplied;NTrendBaseYear;NTrendSlope;IsCO2TrendApplied;CO2TrendBaseYear;CO2TrendSlope;IsNNIUsed;NNIThreshold;NNIMultiplier;IsCheckPcpN;CheckDaysPcpN;MaxPostponeN;CumPcpThrN;CO2;IsWDinPerc;SoilWaterDeficit;TotalNi;TopNi;MidNi;ObservedGrainNumber;TargetFertileShootNumber\n";
            String attr2 = "AgronomicalObjectURI;Date;Nitrogen;WaterMM\n";
            String[] result = new String[2];
            result[0] = attr + values.Substring(0, values.Length - 1);
            result[1] = attr2 + values2.Substring(0, values2.Length - 1);
            return result;
        }

        public static String[] SQcanToCSV(String json, String experiment)
        {
            JObject obj = JObject.Parse(json);
            String attribute2 = "AgronomicalObjectURI;PDATE;CUL_ID;DATE;YEAR;DAY;LAID;GAID;CWAD;LShWAD;LShNAD;SLND;SLWD;SWAD;SCNAD;EHTD;GWAD;CNAD;GNAD;GWGD;NHIAM";

            String value1 = "";
            String value2 = "";
            foreach (KeyValuePair<String, JToken> t in obj)
            {
                value1 += experiment + "/CAN" + t.Key + ";CAN" + t.Key + ";" + experiment + ";";
                value2 += experiment + "/CAN" + t.Key + ";";

                foreach (KeyValuePair<String, JToken> c in (JObject)t.Value)
                {
                    String s = (String)c.Value;
                    if (c.Key.Equals("TRT_NAME") || c.Key.Equals("SITE_NAME") || c.Key.Equals("SOIL_NAME"))
                    {
                        value1 += experiment + "/" + s + ";";
                    }
                    else
                    {
                        value2 += s + ";";
                    }
                }
                value1 = value1.Substring(0, value1.Length - 1) + "\n";
                value2 = value2.Substring(0, value2.Length - 1) + "\n";
            }
            String[] result = new String[2];
            result[0] = value1.Substring(0, value1.Length - 1);
            result[1] = attribute2 + value2.Substring(0, value2.Length - 1);
            return result;
        }

        public static String[] SQmatToCSV(String json, String experiment)
        {
            JObject obj = JObject.Parse(json);
            String attribute2 = "AgronomicalObjectURI;PDATE;CUL_ID;PLDAE;ADAT;HDATE;LnoSX;TnoAM;HnoAM;GWGM;GWAM;GPRCM;CWAM;EHTX;CNAM;GNAM;GNGM;HIAM;GWGD";

            String value1 = "";
            String value2 = "";
            foreach (KeyValuePair<String, JToken> t in obj)
            {
                value1 += experiment + "/MAT" + t.Key + ";MAT" + t.Key + ";" + experiment + ";";
                value2 += experiment + "/MAT" + t.Key + ";";
                foreach (KeyValuePair<String, JToken> c in (JObject)t.Value)
                {
                    String s = (String)c.Value;
                    if (c.Key.Equals("TRT_NAME") || c.Key.Equals("SITE_NAME") || c.Key.Equals("SOIL_NAME"))
                    {
                        value1 += experiment + "/" + s + ";";
                    }
                    else
                    {
                        value2 += s + ";";
                    }
                }
                value1 = value1.Substring(0, value1.Length - 1) + "\n";
                value2 = value2.Substring(0, value2.Length - 1) + "\n";
            }
            String[] result = new String[2];
            result[0] = value1.Substring(0, value1.Length - 1);
            result[1] = attribute2 + value2.Substring(0, value2.Length - 1);
            return result;
        }
        public static String[] SQolnToCSV(String json, String experiment)
        {
            JObject obj = JObject.Parse(json);
            String attribute2 = "AgronomicalObjectURI;PDATE;CUL_ID;DATE;YEAR;DAY;LNUM";

            String value1 = "";
            String value2 = "";
            foreach (KeyValuePair<String, JToken> t in obj)
            {
                value1 += experiment + "/OLN" + t.Key + ";OLN" + t.Key + ";" + experiment + ";";
                value2 += experiment + "/OLN" + t.Key + ";";

                foreach (KeyValuePair<String, JToken> c in (JObject)t.Value)
                {
                    String s = (String)c.Value;
                    if (c.Key.Equals("TRT_NAME") || c.Key.Equals("SITE_NAME") || c.Key.Equals("SOIL_NAME"))
                    {
                        value1 += experiment + "/" + s + ";";
                    }
                    else
                    {
                        value2 += s + ";";
                    }
                }
                value1 = value1.Substring(0, value1.Length - 1) + "\n";
                value2 = value2.Substring(0, value2.Length - 1) + "\n";
            }
            String[] result = new String[2];
            result[0] = value1.Substring(0, value1.Length - 1);
            result[1] = attribute2 + value2.Substring(0, value2.Length - 1);
            return result;
        }

        public static String csvToJson(String csv)
        {
            String[] str = csv.Split('\n');
            JArray arr = new JArray();
            String[] attr = str[0].Split(';');
            for (int i = 1; i < str.Length; i++)
            {
                JObject obj = new JObject();
                String[] s = str[i].Split(';');
                for (int e = 0; e < s.Length; e++)
                {
                    obj[attr[e]] = s[e];
                }
                arr.Add(obj);
            }
            return arr.ToString();
        }
    }
}
