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
    class SQMAT : Observation
    {

        protected override List<String> listParam()
        {
            List<String> sqmat = new List<String>();
            //sqmat.Add("gstzd");
            sqmat.Add("phenology"); //anthesis date
            sqmat.Add("yield"); //Total grain dry mass at maturity
            //Add more cases if there is more attributes to manage
            return sqmat;
        }

        protected override String[] buildParam()
        {
            List<String> param = new List<string>();
            String[] s = { "Management", "Site", "Soil", "Sowing date", "Variety" };
            Array.ForEach(s, param.Add);
            JObject obj = dbm.importJson();

            foreach (String str in paramList)
            {
                switch (str)
                {
                    //case "gstzd":
                    //    if (Export.searchValue("gstzd", dbm.json) != null)
                    //    {
                    //        param.Add("ZC65_Anthesis");
                    //        param.Add("ZC92_Maturity");
                    //        param.Add("PALPAD");
                    //        param.Add("Maturity grain yield");
                    //        param.Add("Maturity crop dry mass");
                    //        param.Add("Maturity crop nitrogen");
                    //        param.Add("gstzd");
                    //    }
                        //break;
                    case "phenology": //phenology
                        if (Export.searchValue("adat", dbm.json) != null)
                        { param.Add("adat"); }
                        if (Export.searchValue("mdat", dbm.json) != null)
                        { param.Add("mdat"); }
                        if (Export.searchValue("pldae", dbm.json) != null)
                        { param.Add("pldae"); }
                        
                        break;
                    case "yield": //yield components
                        if (Export.searchValue("gwam", dbm.json) != null || Export.searchValue("hwam", dbm.json) != null)
                        { param.Add("gwam");
                          param.Add("cwam");}
                        if (Export.searchValue("gwgm", dbm.json) != null)
                        { param.Add("gwgm"); }
                        if (Export.searchValue("hnoam", dbm.json) != null)
                        { param.Add("hnoam"); }
                        if (Export.searchValue("laix", dbm.json) != null)
                        { param.Add("laix"); }
                        if (Export.searchValue("gprcm", dbm.json) != null)
                        { param.Add("gprcm"); }
                        break;
                        //Add more cases if there is more attributes to manage

                }
            }
            return param.ToArray();
        }

        public SQMAT(ImportDBM dbm)
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

                if (!isQuiet) Console.WriteLine((((i * 10) / (s.Length)) + 90) + "%");


                if (!s[i].Equals(""))
                {
                    foreach (JObject obj in JArray.Parse(build(s[i], param)))
                    {
                        arr.Add(obj);
                    }
                }
            }
            if (param.Contains("gstzd"))
            {
                String[] d = new String[param.Length - 1];
                for (int i = 0, e = 0; i < param.Length; ++i)
                {
                    if (param[i].Equals("gstzd"))
                    {
                        e = -1;
                    }
                    else
                    {
                        d[i + e] = param[i];
                    }
                }
                param = d;
            }
            String[] units = base.buildUnits(param);
            result = ObsjsonToCSV(JArray.Parse(SQ_ICASA_Convertor.ICASAtoSQ(arr.ToString())), '\t');
            String[] construct = result.Split('\n');
            construct[0] = units[1] + "\n" + construct[0] + "\n" + units[0];
            return String.Join("\n", construct);
        }

        protected override String build(String name, String[] attr)
        {
            ////JObject n = dbm.searchObjectByName("exname", name);
            String manIDname = dbm.getManagementIdentifier();
            JObject obj = (JObject)dbm.searchObjectByName(manIDname, name);
            JObject obs = (JObject)dbm.searchObjectByName(manIDname, name)["observed"];
            JObject summary = (JObject)dbm.searchObjectByName(manIDname, name)["observed"]["summary"];
            if(summary != null) { obs = summary; }

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
            { pdate = "date";  }
            //foreach (JObject data in (JArray)(((JObject)n["observed"])["timeSeries"])["data"])
            // {
            JObject line = new JObject();
                int i = 0;

                foreach (String s in attr)
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
                        //case "ZC65_Anthesis":
                        case "adat":
                        JArray j = new JArray();
                            JObject a = new JObject();
                            JObject b = new JObject();
                            //if (data.ContainsKey("gstzd") && Double.Parse((String)data["gstzd"]) == 65.0)
                            if (obs.ContainsKey("adat"))
                            {
                                DateTime datess = new DateTime(Int32.Parse(((String)obs["adat"]).Substring(0, 4)), Int32.Parse(((String)obs["adat"]).Substring(4, 2)), Int32.Parse(((String)obs["adat"]).Substring(6, 2)));
                                //Parse de la date de Observation
                                a[s] = datess.ToString("yyyy-MM-dd");

                                i++;
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

                        //case "ZC92_Maturity":
                         case "mdat":
                        JArray j1 = new JArray();
                            JObject c = new JObject();
                            JObject b1 = new JObject();
                        //if (data.ContainsKey("gstzd") && Double.Parse((String)data["gstzd"]) >= 91.0)
                        if (obs.ContainsKey("mdat") )
                        {
                                DateTime datesss = new DateTime(Int32.Parse(((String)obs["mdat"]).Substring(0, 4)), Int32.Parse(((String)obs["mdat"]).Substring(4, 2)), Int32.Parse(((String)obs["mdat"]).Substring(6, 2)));
                                //Parse de la date de Observation
                                c[s] = datesss.ToString("yyyy-MM-dd");
                                i++;
                            }
                            else
                            {
                                c[s] = "-999";
                            }
                            b1[s] = "-999";
                            j1.Add(c);
                            j1.Add(b1);
                            line[s] = j1;
                            break;
                    case "pldae":
                        JArray j2 = new JArray();
                        JObject a2 = new JObject();
                        JObject b2 = new JObject();
                       // if (data.ContainsKey("gstzd") && data.ContainsKey("plpad") && Double.Parse((String)data["gstzd"]) >= 91.0)
                            if (obs.ContainsKey("pldae") )
                            {
                            DateTime datessss = new DateTime(Int32.Parse(((String)obs["pldae"]).Substring(0, 4)), Int32.Parse(((String)obs["pldae"]).Substring(4, 2)), Int32.Parse(((String)obs["pldae"]).Substring(6, 2)));
                            a2[s] = datessss.ToString("yyyy-MM-dd");
                            i++;

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

                    case "gwam": //grain DM
                        JArray j3 = new JArray();
                        JObject a3 = new JObject();
                        JObject b3 = new JObject();
                        //if (data.ContainsKey("gstzd") && data.ContainsKey("hwam") && Double.Parse((String)data["gstzd"]) >= 91.0)
                        if (obs.ContainsKey("gwam"))
                        {

                            a3[s] = Math.Round(Double.Parse((String)obs["gwam"]));
                            i++;

                        }
                        else if (obs.ContainsKey("hwam"))
                        {

                            a3[s] = Math.Round(Double.Parse((String)obs["hwam"]));
                            i++;

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

                    case "cwam": //crop DM
                        JArray j4 = new JArray();
                        JObject a4 = new JObject();
                        JObject b4 = new JObject();
                        //if (data.ContainsKey("gstzd") && data.ContainsKey("egwa") && data.ContainsKey("shwad") && Double.Parse((String)data["gstzd"]) >= 91.0)
                        if (obs.ContainsKey("cwam"))
                        {
                            a4[s] = (Double.Parse((String)obs["cwam"]));
                            i++;

                        }
                        else if (obs.ContainsKey("egwa") && obs.ContainsKey("shwad"))
                        {
                            a4[s] = ((Double.Parse((String)obs["egwa"])) + (Double.Parse((String)obs["shwad"]))) * 10;
                            i++;

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

                    case "hnoam":  //grain number
                        JArray j5 = new JArray();
                        JObject a5 = new JObject();
                        JObject b5 = new JObject();
                        //if (data.ContainsKey("gstzd") && data.ContainsKey("hwam") && Double.Parse((String)data["gstzd"]) >= 91.0)
                        if (obs.ContainsKey("hnoam"))
                        {

                            a5[s] = Math.Round(Double.Parse((String)obs["hnoam"]));
                            i++;

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

                    case "gwgm":  //grain number
                        JArray j6 = new JArray();
                        JObject a6 = new JObject();
                        JObject b6 = new JObject();
                        //if (data.ContainsKey("gstzd") && data.ContainsKey("hwam") && Double.Parse((String)data["gstzd"]) >= 91.0)
                        if (obs.ContainsKey("gwgm"))
                        {

                            a6[s] = Math.Round(Double.Parse((String)obs["gwgm"]));
                            i++;

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

                    case "gprcm":  //grain number
                        JArray j7 = new JArray();
                        JObject a7 = new JObject();
                        JObject b7 = new JObject();
                        //if (data.ContainsKey("gstzd") && data.ContainsKey("hwam") && Double.Parse((String)data["gstzd"]) >= 91.0)
                        if (obs.ContainsKey("gprcm"))
                        {

                            a7[s] = Math.Round(Double.Parse((String)obs["gprcm"]));
                            i++;

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

                    case "laix":  //grain number
                        JArray j8 = new JArray();
                        JObject a8 = new JObject();
                        JObject b8 = new JObject();
                        //if (data.ContainsKey("gstzd") && data.ContainsKey("hwam") && Double.Parse((String)data["gstzd"]) >= 91.0)
                        if (obs.ContainsKey("laix"))
                        {

                            a8[s] = Math.Round(Double.Parse((String)obs["laix"]));
                            i++;

                        }
                        else
                        {
                            a8[s] = "-999";
                        }
                        b8[s] = "-999";
                        j8.Add(a8);
                        j8.Add(b8);
                        line[s] = j8;
                        break;

                        //case "PALPAD":
                        //        JArray j2 = new JArray();
                        //        JObject a2 = new JObject();
                        //        JObject b2 = new JObject();
                        //        if (data.ContainsKey("gstzd") && data.ContainsKey("plpad") && Double.Parse((String)data["gstzd"]) >= 91.0)
                        //        {

                        //            a2[s] = Math.Round(Double.Parse((String)data["plpad"]));
                        //            i++;

                        //        }
                        //        else
                        //        {
                        //            a2[s] = "-999";
                        //        }
                        //        b2[s] = "-999";
                        //        j2.Add(a2);
                        //        j2.Add(b2);
                        //        line[s] = j2;
                        //        break;
                        //case "Maturity grain yield":
                        //        JArray j3 = new JArray();
                        //        JObject a3 = new JObject();
                        //        JObject b3 = new JObject();
                        //        if (data.ContainsKey("gstzd") && data.ContainsKey("hwam") && Double.Parse((String)data["gstzd"]) >= 91.0)
                        //        {

                        //            a3[s] = Math.Round(Double.Parse((String)data["hwam"]));
                        //            i++;

                        //        }
                        //        else
                        //        {
                        //            a3[s] = "-999";
                        //        }
                        //        b3[s] = "-999";
                        //        j3.Add(a3);
                        //        j3.Add(b3);
                        //        line[s] = j3;
                        //        break;
                        //    case "Maturity crop dry mass":


                        //        JArray j4 = new JArray();
                        //        JObject a4 = new JObject();
                        //        JObject b4 = new JObject();
                        //        if (data.ContainsKey("gstzd") && data.ContainsKey("egwa") && data.ContainsKey("shwad") && Double.Parse((String)data["gstzd"]) >= 91.0)
                        //        {
                        //            a4[s] = ((Double.Parse((String)data["egwa"])) + (Double.Parse((String)data["shwad"]))) * 10;
                        //            i++;

                        //        }
                        //        else
                        //        {
                        //            a4[s] = "-999";
                        //        }
                        //        b4[s] = "-999";
                        //        j4.Add(a4);
                        //        j4.Add(b4);
                        //        line[s] = j4;
                        //        break;
                        //    case "Maturity crop nitrogen":
                        //        if (data.ContainsKey("gstzd") && Double.Parse((String)data["gstzd"]) >= 91.0 && data.ContainsKey("egwa") && data.ContainsKey("gn%d") && data.ContainsKey("shwad") && data.ContainsKey("vn%d"))
                        //        {
                        //            line[s] = ((Double.Parse((String)data["egwa"]) * (Double.Parse((String)data["egwa"]))) + ((Double.Parse((String)data["shwad"]) * (Double.Parse((String)data["vn%d"]))))) / 10.0;

                        //        }
                        //        break;
                        //    case "gstzd":
                        //        if (data.ContainsKey("gstzd"))
                        //        {
                        //            line[s] = data["gstzd"];
                        //        }
                        //        break;

                        //Add more cases if there is more attributes to manage

                }

            }

            //if (((line.ContainsKey("gstzd") && line.Count > 9) || line.Count >= 8) && i > 0)
            if ((line.Count >= 8) && i > 0)
                {

                result.Add(line);
            }
            //}

            //if (Export.searchObjectKeyValue("gstzd", "92", result.ToString()) != null && Export.searchObjectKeyValue("gstzd", "91", result.ToString()) != null)
            //{
            //    JArray x = new JArray();
            //    for (int i = 0; i < result.Count; ++i)
            //    {
            //        if (Export.searchValue("gstzd", result[i].ToString()) != null)
            //        {
            //            if (!((String)result[i]["gstzd"]).Equals("91"))
            //            {

            //                x.Add(result[i]);
            //            }
            //        }
            //        else
            //        {
            //            x.Add(result[i]);
            //        }
            //    }
            //    result = x;
            //}
            //result = JArray.Parse(Export.removeAllParameters("gstzd", result.ToString()));
            return result.ToString();
        }

        public override void run(bool isQuiet)
        {
            Directory.CreateDirectory(dbm.ObservationsDIR);
           
            File.WriteAllText(dbm.ObservationsDIR + "\\" + "ObsSQMAT.sqmat", buildAll(isQuiet));
        }

    }
}
