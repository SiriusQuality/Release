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
    class Management : Cultivation
    {


        //CO2 data overs years (1 value per year)
        private Dictionary<String, String> co2 = loadco2();
        //Function that returns a dictionnary of CO2 values per year.
        private static Dictionary<String, String> loadco2()
        {
            Dictionary<String, String> co = new Dictionary<string, string>();
            co["1950"] = "311.3";
            co["1951"] = "311.8";
            co["1952"] = "312.2";
            co["1953"] = "312.6";
            co["1954"] = "313.2";
            co["1955"] = "313.7";
            co["1956"] = "314.3";
            co["1957"] = "314.8";
            co["1958"] = "315.34";
            co["1959"] = "316.18";
            co["1960"] = "317.07";
            co["1961"] = "317.73";
            co["1962"] = "318.43";
            co["1963"] = "319.08";
            co["1964"] = "319.65";
            co["1965"] = "320.23";
            co["1966"] = "321.59";
            co["1967"] = "322.31";
            co["1968"] = "323.04";
            co["1969"] = "324.23";
            co["1970"] = "325.54";
            co["1971"] = "326.42";
            co["1972"] = "327.45";
            co["1973"] = "329.43";
            co["1974"] = "330.21";
            co["1975"] = "331.36";
            co["1976"] = "331.92";
            co["1977"] = "333.73";
            co["1978"] = "335.42";
            co["1979"] = "337.10";
            co["1980"] = "338.99";
            co["1981"] = "340.36";
            co["1982"] = "341.57";
            co["1983"] = "342.53";
            co["1984"] = "344.24";
            co["1985"] = "345.72";
            co["1986"] = "347.15";
            co["1987"] = "348.93";
            co["1988"] = "351.47";
            co["1989"] = "353.15";
            co["1990"] = "354.29";
            co["1991"] = "355.68";
            co["1992"] = "356.42";
            co["1993"] = "357.13";
            co["1994"] = "358.61";
            co["1995"] = "360.67";
            co["1996"] = "362.58";
            co["1997"] = "363.48";
            co["1998"] = "366.27";
            co["1999"] = "368.38";
            co["2000"] = "370.0";
            co["2001"] = "371.7";
            co["2002"] = "373.4";
            co["2003"] = "375.1";
            co["2004"] = "376.8";
            co["2005"] = "378.4";
            co["2006"] = "380.1";
            co["2007"] = "381.7";
            co["2008"] = "383.4";
            co["2009"] = "385.0";
            co["2010"] = "386.6";
            co["2011"] = "388.2";
            co["2012"] = "389.9";
            co["2013"] = "391.5";
            co["2014"] = "393.1";
            co["2015"] = "394.6";
            co["2016"] = "396.2";
            co["2017"] = "397.8";
            co["2018"] = "399.4";
            co["2019"] = "400.9";
            co["2020"] = "402.5";
            return co;
        }

        IDictionary<string, string> fertilizer_list = new Dictionary<string, string>()
                  {
                    {"FE001","Ammonium_Nitrate"},
                    {"FE002","Ammonium_Sulfate"},
                    {"FE003","Ammonium_Nitrate"},
                    {"FE004","Anhydrous_Ammonia"},
                    {"FE005","Urea"},
                    {"FE006","Ammonium_Phosphate"},
                    {"FE007","Ammonium_Phosphate"},
                    {"FE008","Calcium_Nitrate"},
                    {"FE010","Urea_Ammonium_Nitrate"}
                   };

        IDictionary<string, string> AddedOrganicMatter_list = new Dictionary<string, string>()
                  {
                    {"SBT","Sugarbeet"},
                    {"MAZ","Grain_maize"},
                    {"SBN","Soybean"},
                    {"PEA","Proteaginous_pea"},
                    {"CNL","Rapeseed"},
                    {"RYE","Wheat__Rye"},
                    {"RAD","Radish__Oil_seed"},
                    {"TFS","Ryegrass"},
                    {"WHB","Cereal_Straw"},
                    {"WHD","Cereal_Straw"},
                    {"WHT","Cereal_Straw"}

                   };

        IDictionary<string, string> AddedOrganicMatter_type = new Dictionary<string, string>()
                  {
                    {"Cereal_Straw","Mature_Crops"},
                    {"Sugarbeet", "Mature_Crops"},  //only for test
                    {"Grain_maize", "Mature_Crops"},
                    {"Soybean", "Mature_Crops"},
                    {"Proteaginous_pea", "Mature_Crops"},
                    {"Rapeseed", "Mature_Crops"},
                    {"Wheat__Rye", "Catch_Crops"},
                    {"Radish__Oil_seed", "Catch_Crops"},
                    {"Ryegrass", "Catch_Crops"}

                   };

        public Management(ImportDBM dbm)
            : base(dbm)
        { }
        public override void run(bool isQuiet)
        {
            try
            {
                string name = dbm.ManagementFilename;
                File.WriteAllText(dbm.ProjectDIR + "\\" + name + ".sqman", (JSONtoXML(SQ_ICASA_Convertor.ICASAtoSQ(buildAll(isQuiet)))).Replace("_x0020_Date", "Date").Replace(" Date", "Date").Replace("False", "false"));
            }
            catch(Exception e)
            {
                throw new Exception("The translation of Json with the C# code has failed for Management: Check it in your Excel template " + e);
            }
}
        protected override String buildAll(bool isQuiet)
        {
            JArray arr = new JArray();
            String[] s;
            List<String> ManArr = new List<string>();
            bool duplicate_man = false;

            int progress0 = 0;

                s = dbm.getAllNames().ToArray();


            String exID = dbm.getExperimentIdentifier();


            for (int i = 0; i < s.Length; i++)
            {
                JObject obj = (JObject)dbm.searchObjectByName(exID, s[i]);
                int progress = ((i * 15) / (s.Length)) + 65;
                if (progress > progress0)
                {
                    progress0 = progress;
                    if (!isQuiet) Console.WriteLine((((i * 15) / (s.Length)) + 65) + "%");
                }

                //check if trt_name is already in the list
                if (exID == "id")
                {
                    duplicate_man = false;

                    String Trt_name = (String)obj["trt_name"];
                    String Ex_name = (String)obj["exname"];

                    for (int j = 0; j < ManArr.Count; j++)
                    {
                        String Combined_name = Trt_name + Ex_name;
                        if (Combined_name == ManArr[j] && Combined_name != "" ) { duplicate_man = true; }

                    }
                    if (ManArr.Count == 0 || duplicate_man == false) { ManArr.Add(Trt_name + Ex_name); }

                }
                else if (exID == "plot_id")
                {
                    duplicate_man = false;

                    String Trt_name = (String)obj["trt_name"];

                    for (int j = 0; j < ManArr.Count; j++)
                    {
                        if (Trt_name == ManArr[j] && Trt_name != "") { duplicate_man = true; }


                    }
                    if (ManArr.Count == 0 || duplicate_man == false) { ManArr.Add(Trt_name); }

                }


                if (!s[i].Equals("") && duplicate_man == false) arr.Add(JObject.Parse((build(s[i]))));
            }
            JObject ia = new JObject(), sf = new JObject(), result = new JObject();
            ia["ManagementItem"] = arr;
            sf["ItemsArray"] = ia;
            sf["@xmlns:xsd"] = "http://www.w3.org/2001/XMLSchema";
            sf["@xmlns:xsi"] = "http://www.w3.org/2001/XMLSchema-instance";
            result["ManagementFile"] = sf;
            return result.ToString();
        }




    protected override String build(String name)
        {
            String manIDname;
            String exIDname;


            if (dbm.getExperimentIdentifier() == "plot_id")
            {
                exIDname = "plot_id";
                manIDname = "trt_name";  //standard in PHIS, treat_id identifies SQ managements
            }
            else if (dbm.getExperimentIdentifier() == "id")
            {
                exIDname = "id";
                manIDname = "trt_name";  //old standard in PHIS, trt_name identifies SQ managements
            }
            else
            {
                manIDname = dbm.getExperimentIdentifier(); //not sure about standard in other AgMIP json
                exIDname = manIDname; //not sure about standard in other AgMIP json
            }

            JObject obj = (JObject)dbm.searchObjectByName(exIDname, name);
            JObject result = new JObject();
            JObject man = (JObject)dbm.searchObjectByName(exIDname, name)["management"];
            JObject init_cond = (JObject)dbm.searchObjectByName(exIDname, name)["initial_conditions"];
            String s_id;
            JArray ini_cond_layer = null;
            String FCid = dbm.getFieldCapacityIdentifier();


            if (Export.searchValue("soil_id", JObject.Parse(dbm.json)["soils"].ToString()).Split(';').Length > 1)
            { s_id = "soil_id"; }
            else
            { s_id = "sid"; }
            String soilname = Export.searchValue(s_id, obj.ToString());
            JObject soil = Export.searchObjectKeyValue(s_id, (String)obj[s_id], JObject.Parse(dbm.json)["soils"].ToString());
            JArray sl = (JArray)soil["soilLayer"];

            if (init_cond != null)
            { ini_cond_layer = (JArray)init_cond["soilLayer"]; }



            double maxdepthini = 0;
            if (soil.ContainsKey("sldp"))
            {
                maxdepthini = double.Parse((String)soil["sldp"]);
            }
            else if(sl != null)
            {

                 maxdepthini = double.Parse(sl[(sl.Count) - 1]["sllb"].ToString());
            }





            result["@name"] = ((String)obj[manIDname]);
            result["Comments"] = null;
            result["IRSTG"] = null;


            String[] names ={"CRID","EXNAME",
                "PDATE","PLPOP","CO2BT","CO2A","ACO2",
                "SWDf","IsWDinPerc","ICIN","ICPCR", "ICRAG","AOMName", "OMAdded",
                "TopNi","MidNi","NitrateFrac", 
                "StartBefSow","NbDays2SowWin"
                };
            String[] management = new String[names.Length];
            String[] results = new String[names.Length];
            String pdate = "";

            String experimentName = obj["exname"].ToString();
            results[1] = experimentName;
            JObject planting = Export.searchObjectKeyValue("event", "planting", obj.ToString());

            if (planting.ContainsKey("pdate"))
            {
                pdate = "pdate";
            }
            else
            {
                pdate = "date";
            }
            DateTime d = new DateTime(Int32.Parse(((String)planting[pdate]).Substring(0, 4)), Int32.Parse(((String)planting[pdate]).Substring(4, 2)), Int32.Parse(((String)planting[pdate]).Substring(6, 2)));
            results[2] = (String)d.ToString("yyyy-MM-ddTHH:mm:ss");
            JObject firstLayerInitCond = null;
            if (ini_cond_layer != null)
            {
                firstLayerInitCond = (JObject)ini_cond_layer[0];
            }

            string isMAIZE = "";

            for (int i = 0; i < results.Length; i++)
            {
                if (results[i] == null)
                {
                    switch (i)
                    {
                        //"CRID" = Crop ID
                        case 0:


                            if (planting.ContainsKey("crid") || (obj.ContainsKey("crid")))
                            {
                                if (Export.searchFirstValue("crid", obj.ToString()) == "WHB" || Export.searchFirstValue("crid", obj.ToString()) == "WHD")
                                {
                                    results[i] = "Wheat";
                                }
                                else if (Export.searchFirstValue("crid", obj.ToString()) == "MAZ")
                                {
                                    results[i] = "Maize";
                                    isMAIZE = "Maize";
                                }
                                else { results[i] = ""; }
                            }
                            break;

                        //"PLPOP" =seed density
                        case 3:
                            if (planting.ContainsKey("plpop"))
                            {
                                results[i] = (String)planting["plpop"];

                            }
                            break;

                        //"ACO2"
                        case 6:
                            if (co2.ContainsKey((d.Year) + ""))
                            {
                                results[i] = co2[(d.Year) + ""];
                            }
                            break;
                        //    "SWDf" = soilH2Odeficit
                        case 7:
                            double soilH2Odeficit = 0;
                           if (firstLayerInitCond != null && !init_cond.ContainsKey("icswpc") && !init_cond.ContainsKey("icsw"))

                                {
                                if (firstLayerInitCond.ContainsKey("ich2o") && maxdepthini > 0)
                                {
                                    double thickness = 0;
                                    int j = 0;
                                    double sum_soilH2O = 0;
                                    int ic_layer_max = ini_cond_layer.Count;
                                    double depthmax =0;
                                    double fc = 0;


                                    for (int ii = 0; ii < ic_layer_max; ii++)
                                    {

                                        fc = covert_sl1_to_sl2(sl, ini_cond_layer, "fc", ii, ref j, ref thickness);

                                        depthmax = depthmax + thickness;
                                    
                                        if(depthmax<maxdepthini)
                                        {
                                            sum_soilH2O += (double.Parse((String)ini_cond_layer[ii]["ich2o"]) / fc * thickness / maxdepthini);
                                        }
                                        else
                                        {
                                            thickness = thickness - (depthmax - maxdepthini);
                                            sum_soilH2O += (double.Parse((String)ini_cond_layer[ii]["ich2o"]) / fc * thickness / maxdepthini);
                                            break;
                                        }

                                        if (ii == (ic_layer_max - 1) && depthmax< maxdepthini)
                                        {
                                            fc = extract_variable((JObject)sl[j], "fc");
                                            sum_soilH2O += (double.Parse((String)ini_cond_layer[ii]["ich2o"]) / fc * (maxdepthini - depthmax) / maxdepthini);
                                        }


                                    }
                                    if (sum_soilH2O > 1) sum_soilH2O = 1;
                                     soilH2Odeficit = 100-sum_soilH2O * 100;
                                    results[i] = "" + Math.Round(soilH2Odeficit, 2);
                                    results[i+1] = "true"; //"IsWDinPerc"
                                }
                            }
                            else if (firstLayerInitCond != null && init_cond.ContainsKey("icswpc"))
                            {
                                soilH2Odeficit = 100-double.Parse((String)init_cond["icswpc"]);
                                results[i] = "" + soilH2Odeficit;
                                results[i+1] = "true"; //"IsWDinPerc"
                            }
                            else if (firstLayerInitCond != null && init_cond.ContainsKey("icsw"))
                            {
                                int sl_layer_max = sl.Count;
                                double sl_water_max = 0;
                                double icsw = double.Parse((String)init_cond["icsw"]);
                                for (int jj = 0; jj < sl_layer_max; jj++)
                                {
                                    JObject soilLayer = (JObject)sl[jj];
                                    double fc = extract_variable((JObject)sl[jj], "fc");
                                    double sllt= double.Parse((String)soilLayer["sllt"]);
                                    double sllb= double.Parse((String)soilLayer["sllb"]);
                                    sl_water_max += fc / 10 * (sllb - sllt);
                                    
                                }
                                soilH2Odeficit = 100 - (icsw / sl_water_max * 100);
                                results[i] = "" + soilH2Odeficit;
                                results[i+1] = "true";
                            }


                                break;

                        //"ICIN" = TotalNi
                        case 9:
                            double totNi = 0;
                            if (firstLayerInitCond != null && !init_cond.ContainsKey("icin"))
                            {
                                if (firstLayerInitCond.ContainsKey("icno3") && firstLayerInitCond.ContainsKey("icnh4"))
                                {
                                    double thickness = 0;
                                    double bd = 0;
                                    //double depth_layer_top = 0;
                                    int j = 0;
                                    for (int ii = 0; ii < ini_cond_layer.Count; ii++)
                                    {
                                        bd = covert_sl1_to_sl2(sl, ini_cond_layer, "bd", ii, ref j, ref thickness);
                                        totNi += (double.Parse((String)ini_cond_layer[ii]["icno3"]) + double.Parse((String)ini_cond_layer[ii]["icnh4"])) * 10 * bd * thickness / 100;
                                    }
                                    results[i] = "" + Math.Round(totNi, 2);
                                }
                                else if (firstLayerInitCond.ContainsKey("icno3m") && firstLayerInitCond.ContainsKey("icnh4m"))
                                {
                                    for (int ii = 0; ii < ini_cond_layer.Count; ii++)
                                    {
                                        totNi += (double.Parse((String)ini_cond_layer[ii]["icno3m"]) + double.Parse((String)ini_cond_layer[ii]["icnh4m"]));
                                    }
                                    results[i] = "" + Math.Round(totNi, 2);
                                }
                                else if (firstLayerInitCond.ContainsKey("icn_tot"))
                                {
                                    for (int ii = 0; ii < ini_cond_layer.Count; ii++)
                                    {
                                        totNi += (double.Parse((String)ini_cond_layer[ii]["icn_tot"]));
                                    }
                                    results[i] = "" + Math.Round(totNi, 2);
                                }

                            }

                            else if (firstLayerInitCond != null && init_cond.ContainsKey("icin"))
                            {
                                totNi = double.Parse((String)init_cond["icin"]);
                                results[i] = "" + totNi;
                            }
                            break;
                        //"ICPCR" = Previous crop; "ICRAG" = residue above ground_wt (DM);"AOMName";"OMAdded"
                        case 10:
                            if (firstLayerInitCond != null && (String)init_cond["icpcr"] != null && (String)init_cond["icrag"] != null)
                            {
                                if (AddedOrganicMatter_list.ContainsKey((String)init_cond["icpcr"]))
                                {
                                    results[i] = AddedOrganicMatter_list[(String)init_cond["icpcr"]];
                                    results[i+1] = (String)init_cond["icrag"];
                                    results[i+2] = AddedOrganicMatter_type[(String)results[i]];
                                    results[i+3] = "true";
                                }
                                
                            }

                            break;
                       

                        //"TopNi"  &  "MidNi"
                        case 14:
                            if (firstLayerInitCond != null)
                            {
                                if ((firstLayerInitCond.ContainsKey("icno3") && firstLayerInitCond.ContainsKey("icnh4"))|| (firstLayerInitCond.ContainsKey("icno3m") && firstLayerInitCond.ContainsKey("icnh4m")) || firstLayerInitCond.ContainsKey("icn_tot"))
                                {
                                    //double maxdepthinicm = maxdepthini / 100;
                                    if (maxdepthini > 0)
                                    {
                                        double top33Ni = 0;
                                        double mid33Ni = 0;
                                        double bottom33Ni = 0;

                                        double totNiprofil = 0;
                                        int jj = 0;
                                        double N_inorgm = 0;

                                        double firstthirddepth = maxdepthini / 3;
                                        double midthirddepth = maxdepthini * 2 / 3;


                                        JObject layerObject1 =
                                            new JObject(
                                            new JProperty("sllb", firstthirddepth.ToString()));

                                        JObject layerObject2 =
                                            new JObject(
                                            new JProperty("sllb", midthirddepth.ToString()));

                                        JObject layerObject3 =
                                            new JObject(
                                            new JProperty("sllb", maxdepthini.ToString()));


                                        JArray splitted_soil = new JArray();
                                        splitted_soil.Add(layerObject1);
                                        splitted_soil.Add(layerObject2);
                                        splitted_soil.Add(layerObject3);


                                        for (int ii = 0; ii < splitted_soil.Count; ii++)
                                        {
                                            if (firstLayerInitCond.ContainsKey("icno3") && firstLayerInitCond.ContainsKey("icnh4"))
                                            {
                                                N_inorgm = covert_N_inorg_sl1_to_sl2(ini_cond_layer, splitted_soil, sl, "N_inorg", ii, ref jj);
                                            }
                                            else if (firstLayerInitCond.ContainsKey("icno3m") && firstLayerInitCond.ContainsKey("icnh4m") && maxdepthini > 0)
                                            {
                                                N_inorgm = covert_N_inorg_sl1_to_sl2(ini_cond_layer, splitted_soil, sl, "N_inorgm", ii, ref jj);
                                            }
                                            else if (firstLayerInitCond.ContainsKey("icn_tot")  && maxdepthini > 0)
                                            {
                                                N_inorgm = covert_N_inorg_sl1_to_sl2(ini_cond_layer, splitted_soil, sl, "N_inorg_tot", ii, ref jj);
                                            }
                                           

                                            if (ii == 0)
                                            {
                                                top33Ni = N_inorgm;
                                            }
                                            else if (ii == 1)
                                            {
                                                mid33Ni = N_inorgm;
                                            }
                                            else if (ii == 2)
                                            {
                                                bottom33Ni = N_inorgm;
                                            }
                                            totNiprofil += N_inorgm;


                                        }
                                        double top33Niperc = top33Ni / totNiprofil * 100;
                                        results[i] = "" + Math.Round(top33Niperc, 2);

                                        double mid33Niperc = mid33Ni / totNiprofil * 100;
                                        results[i + 1] = "" + Math.Round(mid33Niperc, 2);
                                    }
                                }
 
                            }
                            break;


                        //"NitrateFrac""
                        case 16:
                            if (firstLayerInitCond != null)
                            {
                                if (firstLayerInitCond.ContainsKey("icno3") && firstLayerInitCond.ContainsKey("icnh4"))
                                {
                                    totNi = 0;
                                    double totNO3 = 0;
                                    for (int ii = 0; ii < ini_cond_layer.Count; ii++)
                                    {
                                        totNO3 += (double.Parse((String)ini_cond_layer[ii]["icno3"]));
                                        totNi += (double.Parse((String)ini_cond_layer[ii]["icno3"]) + double.Parse((String)ini_cond_layer[ii]["icnh4"]));
                                    }
                                    results[i] = "" + (totNO3 / totNi);
                                }
                                else if (firstLayerInitCond.ContainsKey("icno3m") && firstLayerInitCond.ContainsKey("icnh4m"))
                                {
                                    totNi = 0;
                                    double totNO3 = 0;
                                    for (int ii = 0; ii < ini_cond_layer.Count; ii++)
                                    {
                                        totNO3 += (double.Parse((String)ini_cond_layer[ii]["icno3m"]));
                                        totNi += (double.Parse((String)ini_cond_layer[ii]["icno3m"]) + double.Parse((String)ini_cond_layer[ii]["icnh4m"]));
                                    }
                                    results[i] = "" + Math.Round((totNO3 / totNi), 2);
                                }
                            }
                            break;

                        //"StartBefSow" and "NbDays2SowWin"
                        case 17:
                            if (obj.ContainsKey("sdat"))
                            {
                                results[i] = "true";
                                DateTime sdate = new DateTime(Int32.Parse(((String)obj["sdat"]).Substring(0, 4)), Int32.Parse(((String)obj["sdat"]).Substring(4, 2)), Int32.Parse(((String)obj["sdat"]).Substring(6, 2)));
                                double ts = (d.ToOADate()- sdate.ToOADate());
                                results[i+1] = ts.ToString();

                            }
                            break;

                    }
                }
            }


            for (int i = 0; i < names.Length; i++)
            {
                if (results[i] != null)
                {
                    result[names[i]] = results[i];

                }


            }


            JArray da = new JArray();
            JArray zck = new JArray();
            JArray events = (JArray)(man["events"]);
            for (int i = 0; i < events.Count; i++)
            {
                JObject o = new JObject();
                JObject eventItem = (JObject)events[i];
                JObject irrigation = Export.searchObjectKeyValue("event", "irrigation", eventItem.ToString());
                JObject fertilizer = Export.searchObjectKeyValue("event", "fertilizer", eventItem.ToString());
                String idate = "";
                String fedate = "";


                if (irrigation != null && irrigation.ContainsKey("idate")) { idate = "idate"; }
                else { idate = "date"; }

                if (fertilizer != null && fertilizer.ContainsKey("fedate")) { fedate = "fedate"; }
                else { fedate = "date"; }

                if (((String)eventItem["event"]).Equals("fertilizer"))
                {
                    if (eventItem.ContainsKey(fedate)) {

                        DateTime dat = new DateTime(Int32.Parse(((String)eventItem[fedate]).Substring(0, 4)), Int32.Parse(((String)eventItem[fedate]).Substring(4, 2)), Int32.Parse(((String)eventItem[fedate]).Substring(6, 2)));
                        o["Date"] = dat.ToString("yyyy-MM-ddTHH:mm:ss");

                        o["FEAMN"] = eventItem["feamn"];
                        o["IRVAL"] = "0";
                        if (fertilizer_list.ContainsKey((eventItem["fecd"]) + ""))
                        { o["FertilizerName"] = fertilizer_list[(eventItem["fecd"]) + ""]; }
                        else
                        { o["FertilizerName"] = "Ammonium_Nitrate"; }
                        da.Add(o);
                    }
                }
                else if (((String)eventItem["event"]).Equals("irrigation"))
                {
                    if (eventItem.ContainsKey(idate))
                    {

                        DateTime dat = new DateTime(Int32.Parse(((String)eventItem[idate]).Substring(0, 4)), Int32.Parse(((String)eventItem[idate]).Substring(4, 2)), Int32.Parse(((String)eventItem[idate]).Substring(6, 2)));
                        o["Date"] = dat.ToString("yyyy-MM-ddTHH:mm:ss");


                        double irval = Double.Parse((String)eventItem["irval"]);
                        o["IRVAL"] = Math.Round(irval, 2);

                        da.Add(o);
                    }
                }

            }

            for (int i = 0; i < events.Count; i++)
            {
                JObject o = new JObject();
                JObject eventItem = (JObject)events[i];
                JObject irrigation = Export.searchObjectKeyValue("event", "irrigation", eventItem.ToString());
                JObject fertilizer = Export.searchObjectKeyValue("event", "fertilizer", eventItem.ToString());
                String idate = "";
                String fedate = "";


                if (irrigation != null && irrigation.ContainsKey("idate")) { idate = "date"; }
                else if (irrigation != null && irrigation.ContainsKey("izadoks") && !irrigation.ContainsKey("date")) { idate = "izadoks"; }
                else { idate = "date"; }

                if (fertilizer != null && fertilizer.ContainsKey("fedate")) { fedate = "date"; }
                else if (fertilizer != null && fertilizer.ContainsKey("fezadoks") && !fertilizer.ContainsKey("date")) { fedate = "fezadoks"; }
                else { fedate = "date"; }

                if (((String)eventItem["event"]).Equals("fertilizer"))
                {
                    if (fedate != "date")
                    {
                        //DateTime dat = new DateTime(Int32.Parse(((String)eventItem[fedate]).Substring(0, 4)), Int32.Parse(((String)eventItem[fedate]).Substring(4, 2)), Int32.Parse(((String)eventItem[fedate]).Substring(6, 2)));
                        //o["Date"] = dat.ToString("yyyy-MM-ddTHH:mm:ss");

                        if (fedate == "fezadoks")
                        {
                            string zc = (String)eventItem[fedate];
                            int zc_d = Int32.Parse(zc);
                            if (isMAIZE != "Maize") o["GrowthStage"] = NtoZC(zc_d);
                            else o["GrowthStage"] = NtoBBCH(zc_d);

                            o["FEAMN"] = eventItem["feamn"];
                            o["IRVAL"] = "0";
                            if (fertilizer_list.ContainsKey((eventItem["fecd"]) + ""))
                            { o["FertilizerName"] = fertilizer_list[(eventItem["fecd"]) + ""]; }
                            else
                            { o["FertilizerName"] = "Ammonium_Nitrate"; }
                            zck.Add(o);

                        }
                    }
                }
                else if (((String)eventItem["event"]).Equals("irrigation"))
                {

                    if (idate != "date")
                    {
                        //DateTime dat = new DateTime(Int32.Parse(((String)eventItem[idate]).Substring(0, 4)), Int32.Parse(((String)eventItem[idate]).Substring(4, 2)), Int32.Parse(((String)eventItem[idate]).Substring(6, 2)));
                        //o["Date"] = dat.ToString("yyyy-MM-ddTHH:mm:ss");
                        if (idate == "izadoks")
                        {
                            string zc = (String)eventItem[idate];
                            int zc_d = Int32.Parse(zc);
                            if (isMAIZE != "Maize") o["GrowthStage"] = NtoZC(zc_d);
                            else o["GrowthStage"] = NtoBBCH(zc_d);

                            double irval = Double.Parse((String)eventItem["irval"]);
                            o["IRVAL"] = Math.Round(irval, 2);

                            zck.Add(o);
                        }

                    }
                }

            }



            JObject p = new JObject();

                p["FEIDATE"] = da;
                result["DateApplications"] = p;

            JObject p0 = new JObject();

            if (isMAIZE!="Maize") {

                p0["IRSTG"] = zck;
                result["GrowthStageApplications"] = p0;


            }
            else
            {
                 p0["IRSTGM"]=zck;
                result["GrowthStageApplicationsMaize"] = p0;
            }

            return result.ToString();


        }

        private String NtoZC(int zc)
        {


            if (zc <= 0.0) return "ZC_00_Sowing";
            else if (zc <= 3.0) return "EndVernalisation";
            else if(zc <= 10.0) return "ZC_10_Emergence";
            else if(zc <= 21.0) return "ZC_21_MainShootPlus1Tiller";
            else if (zc <= 22.0) return "ZC_22_MainShootPlus2Tiller";
            else if (zc <= 23.0) return "ZC_23_MainShootPlus3Tiller";
            else if (zc <= 30.0) return "ZC_30_PseudoStemErection";
            else if (zc <= 31.0) return "ZC_31_1stNodeDetectable";
            else if (zc <= 32.0) return "ZC_32_2ndNodeDetectable";
            else if (zc <= 37.0) return "ZC_37_FlagLeafJustVisible";
            else if (zc <= 39.0) return "ZC_39_FlagLeafLiguleJustVisible";
            else if (zc <= 61.0) return "Heading";
            else if (zc <= 65.0) return "ZC_65_Anthesis";
            else if (zc <= 75.0) return "ZC_75_EndCellDivision";
            else if (zc <= 85.0) return "ZC_85_MidGrainFilling";
            else if (zc <= 91.0) return "ZC_91_EndGrainFilling";
            else if (zc <= 92.0) return "ZC_92_Maturity";
            else return "ZC_92_Maturity";
        }

        private String NtoBBCH(int zc)
        {


            if (zc <= 0.0) return "BBCH_00_Sowing";
            else if (zc <= 10.0) return "BBCH_10_Emergence";
            else if (zc <= 30.0) return "BBCH_30_PseudoStemElongation";
            else if (zc <= 31.0) return "BBCH_31_1stNodeDetectable";
            else if (zc <= 32.0) return "BBCH_32_2ndNodeDetectable";
            else if (zc <= 53.0) return "BBCH_53_PanicleVisible";
            else if (zc <= 63.0) return "BBCH_63_Anthesis";
            else if (zc <= 75.0) return "BBCH_75_EndCellDivision";
            //else if (zc <= 83.0) return "BBCH_83_MidGrainFilling";
            //else if (zc <= 85.0) return "BBCH_85_EndGrainFilling";
            else if (zc <= 87.0) return "BBCH_87_Maturity";
            else return "BBCH_87_Maturity";
        }

        //From a json (data array in timeSeries object which is in the observed object of a site) 
        // it returns the mean value of all plpad (plant density) of the observations.
        private String meanPLPAD(String array)
        {

            JArray tab = JArray.Parse(array);
            double val = 0;
            int counter = 0;
            for (int i = 0; i < tab.Count; i++)
            {
                JObject o = (JObject)tab[i];
                if (o.ContainsKey("plpad"))
                {
                    ++counter;
                    val += double.Parse((String)o["plpad"]);

                }
            }
            return val > 0 ? "" + (val / counter) : null;
        }

        /// <summary>
        /// This function compares 2 soil layers (eg. "soil" soillayer [s_layer1] and "initial condition" soillayer [s_layer2]) 
        /// and recalculates/interpolates a parameter (id) defined in soillayer s_layer1 for soillayer s_layer2
        /// thickness = thickness of initial condition layer
        /// ii = index of s_layer 2 (initial conditon)
        /// j = index of s_layer 1 (soil_layer)
        /// </summary>
        /// 
        private double covert_sl1_to_sl2(JArray s_layer1, JArray s_layer2, String id, int ii, ref int j, ref double thickness)
        {

            double depth_bottom_sl2 = 0;
            double depth_top_sl2 = 0;
            double depth_bottom_sl1 = 0;
            double depth_bottom_sl1_2 = 0;
            double p_1 = 0;
            double p_2 = 0;
            double p_3 = 0;
            double p = 0;
            String sl1_depth_ID = getSLBIdentifier((JObject)s_layer1[0]);
            String sl2_depth_ID = getSLBIdentifier((JObject)s_layer2[0]);


            int sl_layer_max = s_layer1.Count;

            depth_bottom_sl2 = double.Parse(s_layer2[ii][sl2_depth_ID].ToString());
            thickness = depth_bottom_sl2;
            if (ii > 0)
            {

                depth_top_sl2 = double.Parse(s_layer2[ii - 1][sl2_depth_ID].ToString());
                thickness = depth_bottom_sl2 - depth_top_sl2;
            }


             depth_bottom_sl1 = double.Parse(s_layer1[j][sl1_depth_ID].ToString());
            if ((j + 1) < sl_layer_max)
            {

                depth_bottom_sl1_2 = double.Parse(s_layer1[j + 1][sl1_depth_ID].ToString());
            }
            p_1 = extract_variable((JObject)s_layer1[j], id);

            if (depth_bottom_sl2 <= depth_bottom_sl1)
            {
                p = p_1;
            }
            else if (depth_bottom_sl2 > depth_bottom_sl1)
            {
                if ((j + 1) < sl_layer_max)
                { j += 1; }
                p_2 = extract_variable((JObject)s_layer1[j], id);

                if (depth_bottom_sl1_2 == 0 || depth_bottom_sl2 <= depth_bottom_sl1_2)
                {
                    p = p_1 * (depth_bottom_sl1 - depth_top_sl2) / thickness + p_2 * (depth_bottom_sl2 - depth_bottom_sl1) / thickness;
                }
                else if (depth_bottom_sl1_2 != 0 && depth_bottom_sl2 > depth_bottom_sl1_2)
                {
                    j += 1;
                    if (j == sl_layer_max)
                    {
                        p = p_1 * (depth_bottom_sl1 - depth_top_sl2) / thickness + p_2 * (depth_bottom_sl2 - depth_bottom_sl1) / thickness;
                    }
                    else
                    {
                        p = p_1 * (depth_bottom_sl1 - depth_top_sl2) / thickness + p_2 * (depth_bottom_sl1_2 - depth_bottom_sl1) / thickness;
                    }

                    for (int k = j; k < (sl_layer_max); k++)
                    {
                        p_3 = extract_variable((JObject)s_layer1[k], id);

                        double depth_top_sl_3= double.Parse(s_layer1[k - 1][sl1_depth_ID].ToString());
                        double depth_bottom_sl_3 = double.Parse(s_layer1[k][sl1_depth_ID].ToString());

                    if (depth_bottom_sl2 <= depth_bottom_sl_3)
                        {
                            p += p_3 * (depth_bottom_sl2 - depth_top_sl_3) / thickness;
                        }
                        else //(depth_bottom_ic > depth_bottom_sl_3)
                        {
                            p += p_3 * (depth_bottom_sl_3 - depth_top_sl_3) / thickness;
                            j += 1;
                        }
                    }
                }
            }

            return p;

        }
       

        /// <summary>
        /// This function compares 2 soil layers (eg. initial condition [s_layer1] and splitted soillayer [s_layer2]) 
        /// and recalculates/interpolates N_inorg defined in soillayer s_layer1 for soillayer s_layer2
        /// To do that it uses the Bulk Density, recalculated from soillayer s_layer3
        /// id can be N_inorgm [kgN/ha] or N_inorg [ppm = mgN/kg soil]
        /// </summary>
        /// 
        private double covert_N_inorg_sl1_to_sl2(JArray s_layer1, JArray s_layer2, JArray s_layer3, String id, int ii, ref int j)
        {
            //String id = "N_inorg";
            double thickness_ic = 0;
            double thickness_ss = 0;
            double depth_bottom_sl2 = 0;
            double depth_top_sl2 = 0;
            double depth_top_sl1 = 0;
            double depth_bottom_sl1 = 0;
            double p_1 = 0;
            double p_2 = 0;
            double p = 0;
            double bd = 0;
            int h = 0;
            String sl1_depth_ID = getSLBIdentifier((JObject)s_layer1[0]);
            String sl2_depth_ID = getSLBIdentifier((JObject)s_layer2[0]);


            int sl_layer_max = s_layer1.Count;

            if (j == sl_layer_max) { return p = 0; }
            

            depth_bottom_sl2 = double.Parse(s_layer2[ii][sl2_depth_ID].ToString());
            thickness_ss = depth_bottom_sl2;
            depth_bottom_sl1 = double.Parse(s_layer1[j][sl1_depth_ID].ToString());
            thickness_ic = depth_bottom_sl1;
            if (ii > 0)
            {
                depth_top_sl2 = double.Parse(s_layer2[ii - 1][sl2_depth_ID].ToString());
                thickness_ss = depth_bottom_sl2 - depth_top_sl2;
            }
            if (j > 0)
            {
                depth_top_sl1 = double.Parse(s_layer1[j - 1][sl1_depth_ID].ToString());
                thickness_ic = depth_bottom_sl1 - depth_top_sl1;
            }
            
            if (id == "N_inorg")
            {
                bd = covert_sl1_to_sl2(s_layer3, s_layer1, "bd", j, ref h, ref thickness_ic);
                p_1 = extract_variable((JObject)s_layer1[j], id);
                p_1 = p_1 * 10 * bd * thickness_ic / 100;  //conversion ppm -> kg/ha
            }
            else { p_1 = extract_variable((JObject)s_layer1[j], id); }
            


            if (depth_bottom_sl1 < depth_bottom_sl2)
            {
                if (depth_top_sl1 >= depth_top_sl2)
                {
                    p = p_1 * (depth_bottom_sl1 - depth_top_sl1) / thickness_ic;
                }
                else
                {
                    p = p_1 * (depth_bottom_sl1 - depth_top_sl2) / thickness_ic;
                }

                if ((j + 1) < sl_layer_max)
                {
                    j += 1;
                } else
                {
                    j += 1;
                    return p;
                } 

                for (int k = j; k < (sl_layer_max); k++)
                {
                    if (id == "N_inorg")
                    {
                        bd = covert_sl1_to_sl2(s_layer3, s_layer1, "bd", j, ref h, ref thickness_ic);
                        p_2 = extract_variable((JObject)s_layer1[k], id);
                        p_2 = p_2 * 10 * bd * thickness_ic / 100;
                    }
                    else { p_2 = extract_variable((JObject)s_layer1[k], id); }

                    double depth_top_sl1_2 = double.Parse(s_layer1[k - 1][sl1_depth_ID].ToString());
                    double depth_bottom_sl1_2 = double.Parse(s_layer1[k][sl1_depth_ID].ToString());
                    double thickness_ic2 = depth_bottom_sl1_2 - depth_top_sl1_2;
                    if (depth_bottom_sl1_2 <= depth_bottom_sl2)
                    {
                        p += p_2 * (depth_bottom_sl1_2 - depth_top_sl1_2) / thickness_ic2;
                        j += 1;
                    }
                    else if (depth_bottom_sl1_2 > depth_bottom_sl2 && depth_top_sl1_2 < depth_bottom_sl2)
                    {
                        p += p_2 * (depth_bottom_sl2 - depth_top_sl1_2) / thickness_ic2;
                        

                    }

                }
            }

            else if (depth_bottom_sl1 >= depth_bottom_sl2 && depth_top_sl1 < depth_top_sl2)
            {
                p = p_1 * thickness_ss / thickness_ic;

            }

            else if (depth_bottom_sl1 >= depth_bottom_sl2 && depth_top_sl1 >= depth_top_sl2)
            {
                p = p_1 * (depth_bottom_sl2 - depth_top_sl1) / thickness_ic;
            }

            return p;

        }
    }
}
