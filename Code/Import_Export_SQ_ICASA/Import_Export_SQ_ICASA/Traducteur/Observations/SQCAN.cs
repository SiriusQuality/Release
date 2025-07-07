using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
namespace Import_Export_SQ_ICASA.Observations
{
    class SQCAN : Observation
    {

        protected override List<String> listParam()
        {

            List<String> sqcan = new List<String>();
            //sqcan.Add("shwad");
            //sqcan.Add("vn%d");
            sqcan.Add("daily");
            //Add more cases if there is more attributes to manage
            return sqcan;
        }
        protected override String[] buildParam()
        {
            List<String> param = new List<String>();
            String[] s = { "Management", "Site", "Soil", "Sowing date", "Variety", "Date", "Sampling year", "DOY" };
            Array.ForEach(s, param.Add);

            foreach (String str in paramList)
            {
                switch (str)
                {
                    //case "shwad":
                    //    if (Export.searchValue("shwad", dbm.json) != null)
                    //    {
                    //        param.Add("shwad");

                    //    }
                    //    break;
                    //case "vn%d":
                    //    if (Export.searchValue("shwad", dbm.json) != null && Export.searchValue(str, dbm.json) != null)
                    //    {
                    //        param.Add("Crop nitrogen");
                    //    }
                    //    break;
                    case "daily":
                        if (Export.searchValue("laid", dbm.json) != null)
                        { param.Add("laid");}
                        if (Export.searchValue("gwad", dbm.json) != null)  // grain DM
                        { param.Add("gwad"); }
                        if (Export.searchValue("cwad", dbm.json) != null)  // tops DM
                        { param.Add("cwad"); }
                        if (Export.searchValue("gnoad", dbm.json) != null) // grain number
                        { param.Add("gnoad"); }
                        if (Export.searchValue("slnd", dbm.json) != null)  // specific leaf lamina N mass
                        { param.Add("slnd"); }
                        if (Export.searchValue("lwad", dbm.json) != null)  // tot leaf lamina DM
                        { param.Add("lwad"); }
                        if (Export.searchValue("lgnad", dbm.json) != null) // leaf lamina N mass
                        { param.Add("lgnad"); }
                        if (Export.searchValue("gnad", dbm.json) != null)  // grain N mass
                        { param.Add("gnad"); }
                        break;

                        //Add more cases if there is more attributes to manage

                }
            }
            return param.ToArray();
        }

        public SQCAN(ImportDBM dbm)
            : base(dbm)
        {
            paramList = listParam();
        }

        protected override String buildAll(bool isQuiet)
        {
            JArray arr = new JArray();
            //String[] s = dbm.getAllNames().Where(item => item.Contains("T5")).ToArray();
            String[] s = dbm.getAllNames().ToArray();
            String result = "";
            String[] param = buildParam();
            for (int i = 0; i < s.Length; i++)
            {
                if (!isQuiet) Console.WriteLine((((i * 10) / (s.Length)) + 80) + "%");
                if (!s[i].Equals(""))
                {
                    foreach (JObject obj in JArray.Parse(build(s[i], param)))
                    {
                        arr.Add(obj);
                    }
                }

            }
            String[] units = buildUnits(param);
            result = ObsjsonToCSV(JArray.Parse(SQ_ICASA_Convertor.ICASAtoSQ(arr.ToString())), '\t');
            String[] construct = result.Split('\n');
            construct[0] = units[1] + "\n" + construct[0] + "\n" + units[0];
            return String.Join("\n", construct);
        }

        protected override String build(String name, String[] param)
        {
            //JObject n = dbm.searchObjectByName("exname", name);
            String manIDname = dbm.getManagementIdentifier();
            JObject obj = (JObject)dbm.searchObjectByName(manIDname, name);
            //JObject timeseries = (JObject)dbm.searchObjectByName(manIDname, name)["observed"]["timeSeries"];
            JArray result = new JArray();

            String s_id;
            if (Export.searchValue("soil_id", JObject.Parse(dbm.json)["soils"].ToString()).Split(';').Length > 1)
            { s_id = "soil_id"; }
            else
            { s_id = "sid"; }
            String pdate = "";
            JObject planting = Export.searchObjectKeyValue("event", "planting", obj.ToString());
            if (planting.ContainsKey("pdate"))
            { pdate = "pdate"; }
            else
            { pdate = "date"; }

            foreach (JObject data in (JArray)((JObject)obj["observed"])["timeSeries"])
            {
                JObject line = new JObject();

                foreach (String s in param)
                {
                    

                    switch (s)
                    {
                        case "Management":
                            line[s] = "MAN_" + name;
                            break;
                        case "Site":
                            line[s] = "SITE_" + Export.searchFirstValue("wst_id", obj.ToString());//
                            break;
                        case "Soil":
                            line[s] = "SOIL_" + Export.searchFirstValue(s_id, obj.ToString());
                            break;
                        case "Sowing date":
                            JObject date = Export.searchObjectKeyValue("event", "planting", obj.ToString());
                            //Parse de la date de platation
                            DateTime d = new DateTime(Int32.Parse(((String)date[pdate]).Substring(0, 4)), Int32.Parse(((String)date[pdate]).Substring(4, 2)), Int32.Parse(((String)date[pdate]).Substring(6, 2)));
                            line[s] = d.ToString("yyyy-MM-dd");

                            break;
                        case "Variety":
                            JObject t = ((JObject)obj["management"]);
                            if ((String)t["cul_name"] != null)
                            {
                                line[s] = ((String)t["cul_name"]).Replace(" ", "");

                            }
                            else
                            {
                                line[s] = "-999";
                            }
                            break;
                        case "Date":
                            //Parse de la date de platation
                            DateTime da = new DateTime(Int32.Parse(((String)data["date"]).Substring(0, 4)), Int32.Parse(((String)data["date"]).Substring(4, 2)), Int32.Parse(((String)data["date"]).Substring(6, 2)));
                            line[s] = da.ToString("yyyy-MM-dd");
                            break;
                        case "Sampling year":
                            //Parse de la date de platation
                            DateTime dat = new DateTime(Int32.Parse(((String)data["date"]).Substring(0, 4)), Int32.Parse(((String)data["date"]).Substring(4, 2)), Int32.Parse(((String)data["date"]).Substring(6, 2)));
                            line[s] = dat.ToString("yyyy");
                            break;
                        case "DOY":
                            DateTime dates = new DateTime(Int32.Parse(((String)data["date"]).Substring(0, 4)), Int32.Parse(((String)data["date"]).Substring(4, 2)), Int32.Parse(((String)data["date"]).Substring(6, 2)));
                            line[s] = dates.DayOfYear;
                            break;
                        //case "shwad":
                        //    if (data.ContainsKey("shwad"))
                        //    {

                        //        JArray j = new JArray();
                        //        JObject a = new JObject();
                        //        JObject b = new JObject();
                        //        a[s] = Math.Round(Double.Parse((String)data["shwad"]));

                        //        b[s] = "-999";

                        //        j.Add(a);
                        //        j.Add(b);
                        //        line[s] = j;
                        //    }
                        //    break;
                        //case "Crop nitrogen":
                        //    if (data.ContainsKey("shwad") && data.ContainsKey("vn%d"))
                        //    {
                        //        JArray j1 = new JArray();
                        //        JObject c = new JObject();
                        //        JObject de = new JObject();
                        //        c[s] = Double.Parse((String)(data["vn%d"])) * (Double.Parse((String)data["shwad"]) / 100.0);

                        //        de[s] = "-999";
                        //        j1.Add(c);
                        //        j1.Add(de);
                        //        line[s] = j1;
                        //    }
                        //    break;
                        case "laid":
                            JArray j = new JArray();
                            JObject a = new JObject();
                            JObject b = new JObject();
                            if (data.ContainsKey("laid"))
                            {  
                                a[s] = Math.Round(Double.Parse((String)data["laid"]));
                            }
                            else
                            {
                                a[s] = "-999";
                            }
                            b[s] = "-999";
                            j.Add(a);
                            j.Add(b);
                            line[s] = j;
                            
                            break;
                        case "gwad":
                            JArray j1 = new JArray();
                            JObject a1 = new JObject();
                            JObject b1 = new JObject();
                            if (data.ContainsKey("gwad"))
                            {  
                                a1[s] = Math.Round(Double.Parse((String)data["gwad"]));
                            }
                            else
                            {
                                a1[s] = "-999";
                            }
                            b1[s] = "-999";

                                j1.Add(a1);
                                j1.Add(b1);
                                line[s] = j1;
                            
                            break;
                        case "cwad":
                            JArray j2 = new JArray();
                            JObject a2 = new JObject();
                            JObject b2 = new JObject();
                            if (data.ContainsKey("cwad"))
                            { 
                                a2[s] = Math.Round(Double.Parse((String)data["cwad"]));
                            }
                            else
                            {
                                a2[s] = "-999";
                            }
                            b2[s] = "-999";

                                j2.Add(a2);
                                j2.Add(b2);
                                line[s] = j2;
                            break;
                        case "gnoad":
                            JArray j3 = new JArray();
                            JObject a3 = new JObject();
                            JObject b3 = new JObject();
                            if (data.ContainsKey("gnoad"))
                            {
                                a3[s] = Math.Round(Double.Parse((String)data["gnoad"]));
                            }
                            else
                            {
                                a3[s] = "-999";
                            }
                            b3[s] = "-999";

                                j3.Add(a3);
                                j3.Add(b3);
                                line[s] = j3;
                            break;
                        case "slnd":
                            JArray j4 = new JArray();
                            JObject a4 = new JObject();
                            JObject b4 = new JObject();
                            if (data.ContainsKey("slnd"))
                            { 
                                a4[s] = Math.Round(Double.Parse((String)data["slnd"]));
                            }
                            else
                            {
                                a4[s] = "-999";
                            }
                            b4[s] = "-999";

                                j4.Add(a4);
                                j4.Add(b4);
                                line[s] = j4;
                            break;
                        case "lwad":
                            JArray j5 = new JArray();
                            JObject a5 = new JObject();
                            JObject b5 = new JObject();
                            if (data.ContainsKey("lwad"))
                            {
                                a5[s] = Math.Round(Double.Parse((String)data["lwad"]));
                            }
                            else
                            {
                                a5[s] = "-999";
                            }
                            b5[s] = "-999";

                                j5.Add(a5);
                                j5.Add(b5);
                                line[s] = j5;
                            break;
                        case "lgnad":
                            JArray j6 = new JArray();
                            JObject a6 = new JObject();
                            JObject b6 = new JObject();
                            if (data.ContainsKey("lgnad"))
                            {
                                a6[s] = Math.Round(Double.Parse((String)data["lgnad"]));
                            }
                            else
                            {
                                a6[s] = "-999";
                            }
                            b6[s] = "-999";

                                j6.Add(a6);
                                j6.Add(b6);
                                line[s] = j6;
                            break;
                        case "gnad":
                            JArray j7 = new JArray();
                            JObject a7 = new JObject();
                            JObject b7 = new JObject();
                            if (data.ContainsKey("gnad"))
                            {
                                a7[s] = Math.Round(Double.Parse((String)data["gnad"]));
                            }
                            else
                            {
                                a7[s] = "-999";
                            }
                            b7[s] = "-999";
                                j7.Add(a7);
                                j7.Add(b7);
                                line[s] = j7;
                            break;
                            //Add more cases if there is more attributes to manage
                    }

                }
                if (line.Count > 8)
                {
                    result.Add(line);
                }
            }
            return result.ToString();
        }

        public override void run(bool isQuiet)
        {
            //File.WriteAllText(dbm.ProjectDIR + "/./" + "7-ObservationData/ObsSQCAN.sqcan", buildAll(isQuiet));
            Directory.CreateDirectory(dbm.ObservationsDIR);

            File.WriteAllText(dbm.ObservationsDIR + "\\" + "ObsSQCAN.sqcan", buildAll(isQuiet));
        }
    }
}
