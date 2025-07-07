using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Diagnostics;
namespace Import_Export_SQ_ICASA.Cultivations
{
    class Soil:Cultivation
    {

        public Soil(ImportDBM dbm)
            : base(dbm)
        { }

        protected override String buildAll( bool isQuiet)
        {
            JArray arr = new JArray();
            String[] s = dbm.getSoilNames().ToArray();
            String[] sidArr = new String[s.Length];
            String s_id;
            int progress0 = 0;

            if (Export.searchValue("soil_id", JObject.Parse(dbm.json)["soils"].ToString()).Split(';').Length > 1)
            { s_id = "soil_id"; }
            else
            { s_id = "sid"; }  


            for (int i = 0; i < s.Length; i++)
            {
                JObject obj = null;
                if (!s[i].Equals("")) obj = dbm.searchObjectByName(s_id,s[i]);
                JObject soilObject = null;
                int progress = ((i * 15) / (s.Length))+40;
                if (obj != null)
                {
                    soilObject = Export.searchObjectKeyValue(s_id, (String)obj[s_id], JObject.Parse(dbm.json)["soils"].ToString());
                    String sid = (String)soilObject[s_id];

                    if (i == 0)
                    {
                        sidArr[i] = sid;
                        if (progress > progress0)
                        {
                            progress0 = progress;
                            if (!isQuiet) Console.WriteLine((((i * 15) / (s.Length)) + 40) + "%");
                        }
                        if (!s[i].Equals("")) arr.Add(JObject.Parse((build(s[i]))));
                    }
                    else
                    {
                        bool duplicate = false;
                        for (int j = 0; j < i; j++)
                        {
                            if (sid == sidArr[j] && sid != "") { duplicate = true; }
                        }
                        if (duplicate == false)
                        {
                            sidArr[i] = sid;
                            if (progress > progress0)
                            {
                                progress0 = progress;
                                if (!isQuiet) Console.WriteLine((((i * 15) / (s.Length)) + 50) + "%");
                            }
                            if (!s[i].Equals("")) arr.Add(JObject.Parse((build(s[i]))));
                        }
                    }
                }
                                 
            }

            JObject ia = new JObject(), sf = new JObject(), result = new JObject();
            ia["SoilItem"] = arr;
            sf["ItemsArray"] = ia;
            sf["@xmlns:xsd"] = "http://www.w3.org/2001/XMLSchema";
            sf["@xmlns:xsi"] = "http://www.w3.org/2001/XMLSchema-instance";
            result["SoilFile"] = sf;
            return result.ToString();
        }
        private string ConvertTexture(string t)
        {

            if (t == "HYPRES") return "HYPRES(European Soil Map)";
            else if (t == "USDA") return "USDA";
            else return "";
        }

        private string ConvertTexture(string c,string t)
        {

            if (c == "") return "";
            else if (c == "USDA") {
                if (t == "CLOSA") return "Loamy Sand";
                else if (t == "CSA") return "Sand";
                else if (t == "CSI") return "Silt";
                else if (t == "CSALO") return "Sandy Loam";
                else if (t == "CL") return "Clay";
                else if (t == "CLLO") return "Clay Loam";
                else if (t == "FLO") return "Loam";
                else if (t == "FLOSA") return "Loamy Sand";
                else if (t == "FSA") return "Sand";
                else if (t == "FSALO") return "Sandy Loam";
                else if (t == "SICLL") return "Silty Clay Loam";
                else if (t == "LOSA") return "Loamy Sand";
                else if (t == "SA") return "Sand";
                else if (t == "SACL") return "Sandy Clay";
                else if (t == "SACLL") return "Sandy Clay Loam";
                else if (t == "SI") return "Silt";
                else if (t == "SICL") return "Silty Clay";
                else if (t == "SILO") return "Silt Loam";
                else if (t == "SALO") return "Sandy Loam";
                else if (t == "VFLOS") return "Loamy Sand";
                else if (t == "LO") return "Loam";
                else if (t == "VFSA") return "Sand";
                else if (t == "VFSAL") return "Sandy Loam";
                else return "";


            }
            else if (c == "HYPRES")
            {
                if (t == "CLOSA") return "Coarse";
                else if (t == "CSA") return "Coarse";
                else if (t == "CSI") return "Coarse";
                else if (t == "CSALO") return "Coarse";
                else if (t == "FLO") return "Fine";
                else if (t == "FLOSA") return "Fine";
                else if (t == "FSA") return "Fine";
                else if (t == "FSALO") return "Fine";
                else if (t == "VFLOS") return "Very fine";
                else if (t == "VFSA") return "Very fine";
                else if (t == "VFSAL") return "Very fine";
                else return "";
            }
            else return "";
        }


        protected override String build(String name)
        {






            String s_id;

            if (Export.searchValue("soil_id", JObject.Parse(dbm.json)["soils"].ToString()).Split(';').Length > 1)
            { s_id = "soil_id"; }
            else
            { s_id = "sid"; }

            JObject obj = dbm.searchObjectByName(s_id, name);
            String[] soil = new String[7];
            String BDid = dbm.getBulkDensityIdentifier();
            String pHid = dbm.getpHIdentifier();
            String[] icasanames = { "SLNI", BDid.ToUpper(), pHid.ToUpper(), "SLOC", "SLCF" };
            JObject result = new JObject();
            JObject soilObject = Export.searchObjectKeyValue(s_id, (String)obj[s_id], JObject.Parse(dbm.json)["soils"].ToString());
            JArray soilLayers = (JArray)(Export.searchObjectKeyValue(s_id, (String)obj[s_id], ((JArray)((dbm.importJson())["soils"])).ToString())["soilLayer"]);
            JObject firstLayer = (JObject)soilLayers[0];
            double Corg = 0;
            double Norg = 0;
            double CaCO3 = 0;
            string sltx = "";
            string sl_sys = "";

            if (soilObject.ContainsKey("sl_system"))
            {
                sl_sys = (String)soilObject["sl_system"];
                result["TextureClassificationSystem"] = ConvertTexture(sl_sys.ToUpper());
                if (soilObject.ContainsKey("sltx"))
                {
                    sltx = (String)soilObject["sltx"];
                    result["TextureClass"] = ConvertTexture(sl_sys.ToUpper(), sltx.ToUpper());
                }
                else throw new Exception("You defined a soil classification, but you don't define a soil texture: Check SLTX in Soil_metadata");
            }


            for (int i = 0; i < icasanames.Length; i++)
            {
                soil[i] = Export.searchValue(icasanames[i].ToLower(), soilObject.ToString());
                if (soil[i].Length != 0)
                {
                    soil[i] = soil[i].Substring(0, soil[i].Length - 1);
                    result[icasanames[i]] = soil[i];
                }
                else
                {
                    soil[i] = null;
                }
            }

            //SLNI = soil_organic_nitrogen_conc
            if (firstLayer.ContainsKey("slni") || firstLayer.ContainsKey("sloc") || firstLayer.ContainsKey("slom"))
            // if (soil[0] != null)  // should not be tested, since the new version of the translator does not write the key if there is no value
            {
                double p = Norg40(soilLayers);
                result["SLNI"] = "" + Math.Round(p, 2);
                result["IsOrgNCalc"] = false;
                Norg = p / 0.4 / 1e6 / meanBD(soilLayers) * 100;  //covert in f[N]/100g
            }

            else
            {
                result["IsOrgNCalc"] = true;
            }


            //Bulk density
            if (soil[1] != null || firstLayer.ContainsKey("sloc") || firstLayer.ContainsKey("slom")) //bulk density
            {
                double p = meanBD(soilLayers);
                if (p != -999.0)
                {
                    result["SABDM"] = "" + Math.Round(p, 2);
                    result.Remove("SLBDM");
                }
            }

            //Clay content
            //if (soil[2] != null) 
            //{
            //    double p = -999.0;
            //    //double p = mean40cm(((JArray)(Export.searchObjectKeyValue(s_id, (String)obj[s_id], ((JArray)((dbm.importJson())["soils"])).ToString())["soilLayer"])).ToString(), "slcly");
            //    if (double.Parse((String)firstLayer["sllb"]) < 40)
            //    {
            //        p = mean40cm(soilLayers, "slcly");
            //    }
            //    else
            //    {
            //        p = double.Parse((String)firstLayer["slcly"]);
            //    }

            //    if (p != -999.0)
            //    {
            //        result["IsKqCalc"] = true;
            //        result["SLCLY"] = "" + Math.Round(p,2);
            //    }
            //}

            //pH
            if (soil[2] != null) //layer ph
            {
                double p = -999.0;

                if (double.Parse((String)firstLayer["sllb"]) < 40)
                {
                    p = mean40cm(soilLayers, pHid);
                }
                else
                {
                    p = double.Parse((String)firstLayer[pHid]);
                }

                if (p != -999.0)
                {
                    result["SLPHW"] = "" + Math.Round(p, 2);
                }
            }

            //Organic Carbon
            if (soil[3] != null)
            {
                double p = -999.0;

                if (double.Parse((String)firstLayer["sllb"]) < 40)
                {
                    p = mean40cm(soilLayers, "sloc");
                }
                else
                {
                    p = double.Parse((String)firstLayer["sloc"]);
                }

                if (p != -999.0)
                {

                    result["SLOC"] = "" + Math.Round(p, 2);
                    Corg = p;
                }

            }

            //C:N
            if (Norg != 0 && Corg != 0)
            {

                result["CtoN"] = Math.Round(Corg / Norg, 2);
            }
            else
            {
                result["CtoN"] = 10;
            }


            if (soil[4] == null) //coarse fraction
            {
                if (firstLayer.ContainsKey("slsnd") && firstLayer.ContainsKey("slcly") && firstLayer.ContainsKey("slsil"))
                {

                    double p = calculateGravel(soilLayers);
                    if (p != -999.0)
                    {
                        result["SLCF"] = "" + Math.Round(p, 2);
                    }
                }
            }
            else if (soil[4] != null)
            {
                double p = -999.0;

                if (double.Parse((String)firstLayer["sllb"]) < 40)
                {
                    p = mean40cm(soilLayers, "slcf");
                }
                else
                {
                    p = double.Parse((String)firstLayer["slcf"]);
                }
                if (p != -999.0)
                {
                    result["SLCF"] = "" + Math.Round(p, 2);
                }
            }


            result["Comments"] = null;

            //result["@name"] = "SOIL_" + (String)obj[s_id];
            result["@name"] = (String)obj[s_id];
            JArray soils = (JArray)((dbm.importJson())["soils"]);
            String sid = (String)obj[s_id];
            JArray sl = (JArray)(Export.searchObjectKeyValue(s_id, sid, soils.ToString())["soilLayer"]);
            JArray layer = new JArray();
            String FCid = dbm.getFieldCapacityIdentifier();
            String PWPid = dbm.getPWPIdentifier();
            String[] names = { "SLLB", "SLSAT", "SLFC1", "SLWP", "SLCLY" };
            for (int i = 0; i < sl.Count; i++)
            {
                JObject soil_layer = new JObject();
                String[] values = new String[5];
                for (int e = 0; e < 2; e++)
                {
                    values[e] = Export.searchValue(names[e].ToLower(), sl[i].ToString());
                    if (values[e].Length != 0)
                    {
                        values[e] = values[e].Substring(0, values[e].Length - 1);
                        double value = Double.Parse(values[e]);
                        soil_layer[names[e]] = Math.Round(value, 4);
                    }
                    else
                    {
                        values[e] = null;
                    }


                }

                bool isBld = false;

                //if saturation not available in json, calculate from bulk density
                if (values[1] == null && (firstLayer.ContainsKey("slbdm") || firstLayer.ContainsKey("sabdm") || firstLayer.ContainsKey("sloc") || firstLayer.ContainsKey("slom")))
                {
                    JObject soilLayer = (JObject)soilLayers[i];
                    //double BD = double.Parse((String)sl[i]["slbdm"]);
                    double BD = calculateBD(soilLayer);

                    values[1] = "" + Math.Round(((1 - (BD / 2.650))), 4);
                    soil_layer[names[1]] = values[1];
                    isBld = true;
                }


                values[2] = Export.searchValue(FCid, sl[i].ToString());
                values[2] = values[2].Substring(0, values[2].Length - 1);
                double slfc1 = Double.Parse(values[2]);
                soil_layer["SLFC1"] = Math.Round(slfc1, 4);

                values[3] = Export.searchValue(PWPid, sl[i].ToString());
                values[3] = values[3].Substring(0, values[3].Length - 1);
                double slwp = Double.Parse(values[3]);
                soil_layer["SLWP"] = Math.Round(slwp, 4);

                values[4] = Export.searchValue(names[4].ToLower(), sl[i].ToString());
                values[4] = values[4].Substring(0, values[4].Length - 1);
                double slclay = Double.Parse(values[4]);
                soil_layer["SLCLY"] = Math.Round(slclay, 4);

                //if ((values[2] == null || values[2] == "" || values[3] == null || values[3] == ""))
                //{
                //    result["PropEachLayers"] = true;
                //    result["SwrcFromMoisturePoints"] = false;
                //    result["MoisturePointsFromSwrc"] = true;
                //    result["Swrc"] = "van Genuchten";

                //    if (sltx == "" && firstLayer.ContainsKey("vg_alpha") && firstLayer.ContainsKey("n_alpha") && values[2] != null && values[2] != "" && isBld && firstLayer.ContainsKey("slres"))
                //    {
                //        result["PropSoilTexture"] = false;



                //        string vg_alpha = Export.searchValue("VG_ALPHA".ToLower(), sl[i].ToString());
                //        vg_alpha = vg_alpha.Substring(0, vg_alpha.Length - 1);
                //        double vga = Double.Parse(vg_alpha);
                //        soil_layer["Alpha"] = Math.Round(vga, 4);


                //        string vg_n = Export.searchValue("VG_N".ToLower(), sl[i].ToString());
                //        vg_n = vg_n.Substring(0, vg_n.Length - 1);
                //        double vgn = Double.Parse(vg_n);
                //        soil_layer["N"] = Math.Round(vgn, 4);

                //        string slres = Export.searchValue("slres".ToLower(), sl[i].ToString());
                //        slres = slres.Substring(0, slres.Length - 1);
                //        double slresd = Double.Parse(slres);
                //        soil_layer["Residual"] = Math.Round(slresd, 4);


                //    }
                //    else if (sltx != "" && isBld && sl_sys.ToUpper()=="HYPRES")
                //    {
                //        result["PropSoilTexture"] = true;

                //     soil_layer["Texture"] = ConvertTexture(sl_sys.ToUpper(), sltx.ToUpper());


                //    }
                //    else throw new Exception("Your soil meta data and/or your soil layers are not defined properly in your Excel template: " +
                //        "                     make sure you difined SDUL, SLL and SSAT for each layers, Or van genutchen para meter ans SSAT for each layer, " +
                //        "                       Or soil texture class,soil textur and bulk density");
                //}




                //if (firstLayer.ContainsKey("caco3"))
                {
                    string v = Export.searchValue("caco3", sl[i].ToString());
                    if (v != "")
                    {
                        v = v.Substring(0, Math.Min(5, v.Length - 1));
                        CaCO3 += Double.Parse(v);
                    }
                }
                layer.Add(soil_layer);
            }
            JObject T = new JObject();
            T["SoilLayer"] = layer;
            result["LayersArray"] = T;
            result["Caco3"] = CaCO3 / sl.Count;
            return result.ToString();

        }

        public override void run( bool isQuiet)
        {
            try
            {
                string name = dbm.SoilFilename;
                File.WriteAllText(dbm.ProjectDIR + "\\" + name + ".sqsoi", (JSONtoXML(SQ_ICASA_Convertor.ICASAtoSQ(buildAll(isQuiet)))).Replace("_x0020_Date", "Date").Replace(" Date", "Date").Replace("False", "false"));
                //}
            }
            catch(Exception e)
            {
                throw new Exception("The translation of Json with the C# code has failed for Soil: Check your soil data and soil layer data in your Excel template "+e);
            }
        }



        private double Norg40(JArray soilLayers)
        {
            
            double val = 0;
            double depth_sum = 0;
            double depth = 0;
            for (int i = 0; i < soilLayers.Count; i++)
            { 
                JObject soilLayer = (JObject)soilLayers[i];
                depth = double.Parse((String)soilLayer["sllb"]) - double.Parse((String)soilLayer["sllt"]) ;

                if (i == 0 && double.Parse((String)soilLayer["sllb"]) >= 40 )
                {
                    val = calculateNI(soilLayer) * calculateBD(soilLayer) ;
                    depth_sum = 40;
                }

                if (double.Parse((String)soilLayer["sllb"]) < 40 )
                { 
                    val += calculateNI(soilLayer)  * calculateBD(soilLayer) * depth / 40;
                    depth_sum += depth;
                    
                }
                else if (double.Parse((String)soilLayer["sllb"]) >= 40 && depth_sum<40)
                {
                    val += calculateNI(soilLayer)  * calculateBD(soilLayer) * (40-depth_sum)/40;
                    depth_sum = 40;
                }
                

            }
            val = val * 0.4 * 1E+6 /100 ;
            return val > 0 ? val : -999.0;
        }

        // take organic nitrogen in soil either directly from JObject (SLNI) of calculate through pedotransfer function from SLOC (or SLOM)
        private double calculateNI(JObject soillayer)
        {
            double NIvalue = 0;
            if (soillayer.ContainsKey("slni"))
            {
                NIvalue = double.Parse((String)soillayer["slni"]);
            }
            else if (soillayer.ContainsKey("sloc"))
            {
                NIvalue = double.Parse((String)soillayer["sloc"]) / 10;
            }
            else if (soillayer.ContainsKey("slom"))
            {
                NIvalue = calculateOC(soillayer) /10;
            }
            
            return NIvalue;
        }


        //From a json (SoilLayer array) it returns the mean value of slbdm for a depth lower than 40
        private double meanBD(JArray soilLayers)
        {  
            double val = 0;
            double depth_sum = 0;
            double depth = 0;

           // int counter = 0;

            for (int i = 0; i < soilLayers.Count; i++)
            {
                JObject soilLayer = (JObject)soilLayers[i];
                depth = double.Parse((String)soilLayer["sllb"]) - double.Parse((String)soilLayer["sllt"]) ;

                if (double.Parse((String)soilLayer["sllb"]) <= 40)
                {
                    //++counter;
                    val += calculateBD(soilLayer) * depth / 40;
                    depth_sum += depth;
                }
                else if (double.Parse((String)soilLayer["sllb"]) > 40 && i==0)
                {
                    val += calculateBD(soilLayer);
                    depth_sum = 40;
                    // counter = 1;
                }
                else if (double.Parse((String)soilLayer["sllb"]) > 40 && depth_sum<40)
                {
                    val += calculateBD(soilLayer)*(40-depth_sum)/40;
                    depth_sum = 40;
                    
                    // counter = 1;
                }
            }
            return val > 0 ? val  : -999.0;
        }

        




        private double mean40cm(JArray soilLayers, String attribut)
        {
            double val = 0;
            double depth_sum = 0;
            double depth = 0;
            double previous = 0;
            //int counter = 0;
            for (int i = 0; i < soilLayers.Count; i++)
            // int i = 0;
            {
                JObject soilLayer = (JObject)soilLayers[i];
                depth = double.Parse((String)soilLayer["sllb"]) - double.Parse((String)soilLayer["sllt"]);
                if (double.Parse((String)soilLayer["sllt"]) < 40 & double.Parse((String)soilLayer["sllt"]) > 0 & soilLayer[attribut] == null)
                {
                    soilLayer[attribut] = previous * (40 - depth_sum) / 40;
                    depth_sum = 40;
                }

                else
                {

                    if (double.Parse((String)soilLayer["sllb"]) <= 40 && soilLayer.ContainsKey(attribut))
                    {
                        //++counter;
                        val += double.Parse((String)soilLayer[attribut]) * depth / 40;
                        previous = double.Parse((String)soilLayer[attribut]);
                        depth_sum += depth;
                    }
                    else if (double.Parse((String)soilLayer["sllb"]) > 40 && depth_sum < 40)
                    {
                        val += double.Parse((String)soilLayer[attribut]) * (40 - depth_sum) / 40;
                        depth_sum = 40;
                    }

                }
            }
            return val >= 0 ? val : -999.0;
        }

        //From a json (SoilLayer array) it returns 100 minus the mean value of slcly, 
        //slsnd, slsil  and for a depth lower than 40
        private double calculateGravel(JArray soilLayers)
        {
            double depth_sum = 0;
            double depth = 0;
            double val = 0;
            double previoussand = 0;
            double previoussilt = 0;
            double previousclay = 0;
            //int counter = 0;
            for (int i = 0; i < soilLayers.Count; i++)
            {
                JObject soilLayer = (JObject)soilLayers[i];
                depth = double.Parse((String)soilLayer["sllb"]) - double.Parse((String)soilLayer["sllt"]);
                if (double.Parse((String)soilLayer["sllt"]) < 40 & double.Parse((String)soilLayer["sllt"]) > 0 & soilLayer["slsnd"] == null)
                {
                    soilLayer["slsnd"] = previoussand;
                    soilLayer["slcly"] = previousclay;
                    soilLayer["slsil"] = previoussilt;
                    val += double.Parse((String)soilLayer["slsnd"]) * (40 - depth_sum) / 40;
                    val += double.Parse((String)soilLayer["slcly"]) * (40 - depth_sum) / 40;
                    val += double.Parse((String)soilLayer["slsil"]) * (40 - depth_sum) / 40;
                    depth_sum = 40;
                }
                if (double.Parse((String)soilLayer["sllb"]) <= 40 && depth_sum < 40)
                {
                    //++counter;
                    val += double.Parse((String)soilLayer["slsnd"]) * depth / 40;
                    val += double.Parse((String)soilLayer["slcly"]) * depth / 40;
                    val += double.Parse((String)soilLayer["slsil"]) * depth / 40;
                    depth_sum += depth;
                    previoussand = double.Parse((String)soilLayer["slsnd"]);
                    previoussilt = double.Parse((String)soilLayer["slcly"]);
                    previousclay = double.Parse((String)soilLayer["slsil"]);

                }
                else if (double.Parse((String)soilLayer["sllb"]) > 40 && depth_sum < 40)
                {
                    val += double.Parse((String)soilLayer["slsnd"]) * (40 - depth_sum) / 40;
                    val += double.Parse((String)soilLayer["slcly"]) * (40 - depth_sum) / 40;
                    val += double.Parse((String)soilLayer["slsil"]) * (40 - depth_sum) / 40;
                    depth_sum = 40;
                    //counter = 1;
                }
            }
            return val > 0 ? 100 - val : -999.0;
        }
    }
}
