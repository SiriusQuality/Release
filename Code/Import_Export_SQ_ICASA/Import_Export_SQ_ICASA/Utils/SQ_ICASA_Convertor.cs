using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Import_Export_SQ_ICASA
{
    public class SQ_ICASA_Convertor
    {
        /*
         * All the convertion functions works recursively.
         * First it detects if the JSON it's an JArray (Json Array)
         * If yes it applies his functions on each element of the array.
         * If no it  knows it's a JObject (Json Object) because a 
         * Json can only have two state : Object or Array.
         * Then it loop over all attributes of the JObject
         * If the value of the attribute is an JObject or an JArray
         * It applies his functions on each element of the array.
         * 
         * Then if the current function changes the names ,it will check
         * if the attribute name is in ICASA dictionnary, it changes the name.
         * (It add the same value with an other name in the new JObject)
         * Otherwise it add the value to the new JObject.
         * 
         * If the function changes the unit:
         * It will check the value of the attribute and will not change the value if its values is "-999"
         * Otherwise it will check if the current attribute haves to be changed in a switch statement 
         * and changes it if it's value is in the switch statement.
         */


        //Relation between ICASA/SiriusQuality
        private static Dictionary<String, String> ICASA = loadICASA();

        //Load SiriusQuality , ICASA relations
        private static Dictionary<String, String> loadICASA()
        {
            Dictionary<String, String> ICASA = new Dictionary<String, String>();
            //SQCAN
            ICASA["Management"] = "TRT_NAME";
            ICASA["Site"] = "SITE_NAME";
            ICASA["Soil"] = "SOIL_NAME";
            ICASA["Sowing Date"] = "PDATE";
            ICASA["Variety"] = "CUL_ID";
            ICASA["Date"] = "DATE";
            ICASA["Sampling year"] = "YEAR";
            ICASA["DOY"] = "DAY";
            ICASA["Leaf area index"] = "LAID";
            ICASA["Maximum leaf area index"] = "LAIX";
            ICASA["Green area index"] = "GAID";
            ICASA["Crop dry mass"] = "CWAD";
            ICASA["Leaf dry mass"] = "LShWAD";
            ICASA["Total leaf lamina dry mass"] = "LWAD";
            ICASA["Leaf nitrogen"] = "LShNAD";
            ICASA["Specific leaf nitrogen"] = "SLND";
            ICASA["Specific leaf dry mass"] = "SLWD";
            ICASA["Stem dry mass"] = "SWAD";
            ICASA["Stem nitrogen"] = "SCNAD";
            ICASA["Stem length"] = "EHTD";
            ICASA["Grain dry mass"] = "GWAD";
            ICASA["Crop nitrogen"] = "CNAD";
            ICASA["Grain nitrogen"] = "GNAD";
            ICASA["Grain number"] = "GnoAD";
            ICASA["Single grain dry mass"] = "GWGD";
            ICASA["Single grain nitrogen"] = "GNGD";
            ICASA["Crop dry mass"] = "SHWAD";
            //SQMAT
            ICASA["ZC10_Emergence"] = "PLDAE";
            ICASA["ZC65_Anthesis"] = "ADAT";
            ICASA["ZC92_Maturity"] = "MDAT";
            ICASA["Final leaf number"] = "LnoSX";
            ICASA["Maturity shoot number"] = "TnoAM";
            ICASA["Grain number"] = "HnoAM";
            ICASA["Maturity single grain dry mass"] = "GWGM";
            ICASA["Maturity grain yield"] = "GWAM";
            ICASA["Grain protein concentration"] = "GPRCM";
            ICASA["Maturity crop dry mass"] = "CWAM";
            ICASA["Anthesis stem length"] = "EHTX";
            ICASA["Maturity crop nitrogen"] = "CNAM";
            ICASA["Maturity grain nitrogen"] = "GNAM";
            ICASA["Maturity single grain nitrogen"] = "GNGM";
            ICASA["DM harvest index"] = "HIAM";
            ICASA["N harvest index"] = "GWGD";
            ICASA["Single grain nitrogen"] = "NHIAM";
            ICASA["Maturity shoot number"] = "PALPAD";
            //SQOLN
            ICASA["Emerged leaf number"] = "LNUM";
            //Weather
            ICASA["Weather file"] = "WTH_DATASET";
            ICASA["Year"] = "YEAR";
            ICASA["Tmin"] = "TMIN";
            ICASA["Tmin"] = "TMAX";
            ICASA["Rain"] = "RAIN";
            ICASA["Radiation"] = "SRAD";
            ICASA["wind"] = "WIND";
            ICASA["Vapor Pressure"] = "VPRSD";
            //Site
            ICASA["SiteItem"] = "SITE_NAME";
            ICASA["Elevation"] = "FLELE";
            ICASA["Latitude"] = "FL_LAT";
            ICASA["Longitude"] = "FL_LONG";
            ICASA["MeasurementHeight"] = "WNDHT";
            //Soil
            ICASA["SoilItem"] = "SOIL_NAME";
            //ICASA["Ndp"] = "SLDN";
            //ICASA["Kq"] = "PERC";
            ICASA["Clay"] = "SLCLY";
            //ICASA["No"] = "SNTD";
            ICASA["No"] = "SLNI";
            //ICASA["Bd"] = "SLBDM";
            ICASA["PH"] = "SLPHW";
            ICASA["CtoN"] = "OMC2N";
            ICASA["Caco3"] = "Caco3";
            ICASA["Carbon"] = "SLOC";
            ICASA["Bd"] = "SABDM";
            ICASA["Gravel"] = "SLCF";
            ICASA["Depth"] = "SLLB";
            ICASA["SSAT"] = "SLSAT";
            ICASA["SDUL"] = "SLFC1";
            //ICASA["SLDUL"] = "SLFC1"//;
            ICASA["SLL"] = "SLWP";
            //ICASA["SLLL"] = "SLWP";
            //Management
            ICASA["ManagementItem"] = "TRT_NAME";
            ICASA["ExperimentName"] = "EXNAME";
            ICASA["Species"] = "CRID";
            ICASA["SowingDate"] = "PDATE";
            ICASA["SowingDensity"] = "PLPOP";
            ICASA["SoilWaterDeficit"] = "SWDf";
            ICASA["TotalNi"] = "ICIN";
            //ICASA["TopNi"] = "IN_TOP";
            //ICASA["MidNi"] = "IN_MI";
            ICASA["AOMName"] = "ICPCR";
            ICASA["averageDMaom"] = "ICRAG";
            ICASA["OMAdded"] = "OMAdded";
            ICASA["CO2"] = "ACO2";
            ICASA["CO2TrendBaseYear"] = "CO2BT";
            ICASA["CO2TrendSlope"] = "CO2A";
            //ICASA["TargetFertileShootNumber"] = "TFSno";
            ICASA["DateApplication"] = "FEIDATE";
            ICASA["Nitrogen"] = "FEAMN";
            ICASA["WaterMM"] = "IRVAL";
            ICASA["GrowthStageApplication"] = "IRSTG";
            ICASA["GrowthStageApplicationMaize"] = "IRSTGM";
            ICASA["PropEachLayers"] = "PropEachLayers";
            ICASA["SwrcFromMoisturePoints"] = "SwrcFromMoisturePoints";
            ICASA["MoisturePointsFromSwrc"] = "MoisturePointsFromSwrc";
            ICASA["PropSoilTexture"] = "PropSoilTexture";
            ICASA["Residual"] = "Residual";
            ICASA["Alpha"] = "Alpha";
            ICASA["N"] = "N";
            ICASA["Swrc"] = "Swrc";
            ICASA["Texture"] = "Texture";
            ICASA["TextureClassificationSystem"] = "TextureClassificationSystem";




            return ICASA;
        }

        //Convert the SQ units in ICASA format
        private static String ICASAunitConvert(String json)
        {
            if (json.StartsWith("["))
            {
                JArray arr = JArray.Parse(json);
                for (int i = 0; i < arr.Count; ++i)
                {
                    arr[i] = JObject.Parse(ICASAunitConvert(((JObject)arr[i]).ToString()));

                }
                return arr.ToString();
            }
            else
            {
                JObject obj = JObject.Parse(json);



                //Initialisation du json
                //Parcours tout les paramètres de obj,
                //Il regarde si il existe des String (Il y a seulement 2 types d'objet "String" et "JObject") 
                // ne valant pas -999.
                // Puis il regarde i.Key fait parti des valeurs à changer d'unités. Si oui, il la change.
                //Si il ne trouve pas de string ne valant pa "-999" il recherche recursivement dans le JObject
                //d'autres valeurs à modifier au sein du json et attache le nouveau JObject à obj[i.key].
                foreach (KeyValuePair<String, JToken> i in obj)
                {
                    if (!((i.Value is JObject) || (i.Value is JArray) || (i.Value == null)))
                    {
                        String o = (String)i.Value ?? "-999";
                        if (!o.Equals("-999"))
                        {


                            switch (i.Key)
                            {
                                case "SLND":
                                    obj[i.Key] = ("" + (10000 * Convert.ToDouble(o))).Replace(",", ".");
                                    break;
                                //case "GWGM":
                                //    obj[i.Key] = ("" + (1000 * Convert.ToDouble(o))).Replace(",", ".");
                                //    break;
                                case "VPRSD":
                                    obj[i.Key] = ("" + ( Convert.ToDouble(o) / 10)).Replace(",", ".");
                                    break;
                                case "ICIN":  
                                    obj[i.Key] = ("" + (Convert.ToDouble(o) * 10)).Replace(",", ".");
                                    break;
                                case "FEAMN":
                                    obj[i.Key] = ("" + (Convert.ToDouble(o) * 10)).Replace(",", ".");
                                    break;
                                case "SNTD":
                                    obj[i.Key] = ("" + (10 * Convert.ToDouble(o))).Replace(",", ".");
                                    break;
                                case "SLLB":
                                    obj[i.Key] = ("" + (100 * Convert.ToDouble(o))).Replace(",", ".");
                                    break;
                                case "SLSAT":
                                    obj[i.Key] = ("" + (Convert.ToDouble(o) / 100)).Replace(",", ".");
                                    break;
                                //case "SLDUL":
                                //    obj[i.Key] = ("" + (Convert.ToDouble(o) / 100)).Replace(",", ".");
                                //    break;
                                //case "SLLL":
                                //    obj[i.Key] = ("" + (Convert.ToDouble(o) / 100)).Replace(",", ".");
                                //    break;
                                case "SLFC1":
                                    obj[i.Key] = ("" + (Convert.ToDouble(o) / 100)).Replace(",", ".");
                                    break;
                                case "SLWP":
                                    obj[i.Key] = ("" + (Convert.ToDouble(o) / 100)).Replace(",", ".");
                                    break;
                            }
                        }
                    }
                    else if (i.Value is JObject)
                    {
                        //Réitère le code si jamais la valeur est un Jobject et remplace la valeur actuel par la nouvelle valeur JObject
                        obj[i.Key] = JObject.Parse(ICASAunitConvert(((JObject)i.Value).ToString()));
                    }

                    else if (i.Value is JArray)
                    {

                        obj[i.Key] = JArray.Parse(ICASAunitConvert(((JArray)i.Value).ToString()));
                    }

                }
                return obj.ToString();
            }
        }

        //Convert the names of SiriusQuality in ICASA names.
        private static String ICASANameConvert(String json)
        {
            if (json.StartsWith("["))
            {
                JArray arr = JArray.Parse(json);
                for (int i = 0; i < arr.Count; ++i)
                {
                    arr[i] = JObject.Parse(ICASANameConvert(((JObject)arr[i]).ToString()));

                }
                return arr.ToString();
            }
            else
            {
                JObject obj = JObject.Parse(json);

                //Regarde si il existe des JObject au sein de obj et si c'est le cas,
                //il traite les JObject récursivement en ratachant le nouveau JObject à obj[i.key]
                foreach (KeyValuePair<String, JToken> i in obj)
                {
                    if (i.Value is JObject)
                    {
                        obj[i.Key] = JObject.Parse(ICASANameConvert(((JObject)i.Value).ToString()));
                    }
                    else if (i.Value is JArray)
                    {
                        obj[i.Key] = JArray.Parse(ICASANameConvert(((JArray)i.Value).ToString()));
                    }

                }

                //Regarde si il existe des clés de ICASA Existent dans le JObject obj,
                //et si c'est le cas, il remplace les valeurs.
                JObject h = new JObject();
                foreach (KeyValuePair<String, JToken> k in obj)
                {
                    if (ICASA.ContainsKey(k.Key))
                    {
                        h[ICASA[k.Key].ToUpper()] = obj[k.Key];
                    }
                    else
                    {
                        h[k.Key] = obj[k.Key];

                    }
                }
                /*foreach (KeyValuePair<String, String> k in ICASA)
                {
                    if (obj.ContainsKey(k.Key))
                    {
                        var value = obj[k.Key];
                        obj.Remove(k.Key);
                        obj[k.Value] = value;
                    }
                }*/
                return h.ToString();
            }
        }


        //Convert the ICASA units in SiriusQuality format
        private static String SQunitConvert(String json)
        {
            if (json.StartsWith("["))
            {

                JArray arr = JArray.Parse(json);
                for (int i = 0; i < arr.Count; ++i)
                {
                    arr[i] = JObject.Parse(SQunitConvert(((JObject)arr[i]).ToString()));

                }
                return arr.ToString();
            }
            else
            {
                //Initialisation du json
                JObject obj = JObject.Parse(json);
                //Parcours tout les paramètres de obj,
                //Il regarde si il existe des String (Il y a seulement 2 types d'objet "String" et "JObject") 
                // ne valant pas -999.
                // Puis il regarde i.Key fait parti des valeurs à changer d'unités. Si oui, il la change.
                //Si il ne trouve pas de string ne valant pa "-999" il recherche recursivement dans le JObject
                //d'autres valeurs à modifier au sein du json et attache le nouveau JObject à obj[i.key].
                foreach (KeyValuePair<String, JToken> i in obj)
                {

                    if (!((i.Value is JObject) || (i.Value is JArray) || (i.Value == null)))
                    {
                        String o = (String)i.Value ?? "-999";
                        if (!o.Equals("-999"))
                        {



                            switch (i.Key)
                            {
                                case "Specific leaf nitrogen":
                                    obj[i.Key] = ("" + (Convert.ToDouble(o) / 10000.0)).Replace(",", ".");
                                    break;
                                //case "Maturity single grain dry mass":
                                //    obj[i.Key] = ("" + (Convert.ToDouble(o) / 1000.0)).Replace(",", ".");
                                //    break;
                                //case "Vapor Pressure":
                                //    obj[i.Key] = ("" + (10 * Convert.ToDouble(o))).Replace(",", ".");
                                //    break;
                                case "TotalNi":
                                    obj[i.Key] = ("" + (Convert.ToDouble(o) / 10)).Replace(",", ".");
                                    break;
                                case "Nitrogen":
                                    obj[i.Key] = ("" + (Convert.ToDouble(o) / 10)).Replace(",", ".");
                                    break;
                                //case "No":
                                //    obj[i.Key] = ("" + (Convert.ToDouble(o) / 10)).Replace(",", ".");
                                //    break;
                                case "Depth":
                                    obj[i.Key] = ("" + (Convert.ToDouble(o) / 100)).Replace(",", ".");
                                    break;
                                case "SSAT":
                                    obj[i.Key] = ("" + (100 * Convert.ToDouble(o))).Replace(",", ".");
                                    break;
                                case "SDUL":
                                    obj[i.Key] = ("" + (100 * Convert.ToDouble(o))).Replace(",", ".");
                                    break;
                                case "SLL":
                                    obj[i.Key] = ("" + (100 * Convert.ToDouble(o))).Replace(",", ".");
                                    break;                            //for the moment the unit is wrong in DBM
                            }
                        }
                    }
                    else if (i.Value is JObject)
                    {

                        obj[i.Key] = JObject.Parse(SQunitConvert(((JObject)i.Value).ToString()));
                    }
                    else if (i.Value is JArray)
                    {

                        obj[i.Key] = JArray.Parse(SQunitConvert(((JArray)i.Value).ToString()));
                    }

                }
                return obj.ToString();
            }
        }

        //Convert the names of ICASA in SiriusQuality names.
        private static String SQNameConvert(String json)
        {
            if (json.StartsWith("["))
            {
                JArray arr = JArray.Parse(json);
                for (int i = 0; i < arr.Count; ++i)
                {
                    arr[i] = JObject.Parse(SQNameConvert(((JObject)arr[i]).ToString()));

                }
                return arr.ToString();
            }
            else
            {
                JObject obj = JObject.Parse(json);
                foreach (KeyValuePair<String, JToken> i in obj)
                {
                    if (i.Value is JObject)
                    {
                        obj[i.Key] = JObject.Parse(SQNameConvert(((JObject)i.Value).ToString()));
                    }
                    else if (i.Value is JArray)
                    {
                        obj[i.Key] = JArray.Parse(SQNameConvert(((JArray)i.Value).ToString()));
                    }

                }
                JObject h = new JObject();
                foreach (KeyValuePair<String, JToken> k in obj)
                {
                    bool g = false;
                    foreach (KeyValuePair<String, String> i in ICASA)
                    {
                        g = g || k.Key.ToUpper().Equals(i.Value.ToUpper());

                    }
                    if (g)
                    {
                        String t = "";
                        foreach (KeyValuePair<String, String> i in ICASA)
                        {
                            t = k.Key.ToUpper().Equals(i.Value.ToUpper()) ? i.Key : t;
                        }
                        h[t] = obj[k.Key];
                    }
                    else
                    {
                        h[k.Key] = obj[k.Key];
                    }
                }/*
                foreach (KeyValuePair<String, String> k in ICASA)
                {


                    if (obj.ContainsKey(k.Value))
                    {
                        var value = obj[k.Value];
                        obj.Remove(k.Value);
                        obj[k.Key] = value;
                    }
                }*/
                return h.ToString();
            }
        }

        //Convert SQ Json in ICASA json
        //First it changes the name
        //Then it changes the units
        public static String SQtoICASA(String json)
        {
            return ICASAunitConvert(ICASANameConvert(json));
        }

        //Convert ICASA Json in SQ json
        //First it changes the name
        //Then it changes the units
        public static String ICASAtoSQ(String json)
        {
            return SQunitConvert(SQNameConvert(json));
        }
    }
}