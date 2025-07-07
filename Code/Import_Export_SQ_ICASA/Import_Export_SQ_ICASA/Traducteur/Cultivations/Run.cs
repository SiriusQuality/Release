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
    class Run : Cultivation
    {
         public Run(ImportDBM dbm)
            : base(dbm)
        { }

         protected override String buildAll( bool isQuiet)
         {
             //int progress0 = 0;
             JArray arr = new JArray();

             JObject ia = new JObject(), sf = new JObject(), result = new JObject();
             arr.Add(new JObject(buildMulti(isQuiet)));
             ia["RunItem"] = arr;
             sf["ItemsArray"] = ia;
             sf["@xmlns:xsd"] = "http://www.w3.org/2001/XMLSchema";
             sf["@xmlns:xsi"] = "http://www.w3.org/2001/XMLSchema-instance";
             result["RunFile"] = sf;
             return result.ToString();
         }


         protected override String build(String name)
         {
            String ManIDname;
            String ExIDname = dbm.getExperimentIdentifier();
            if (ExIDname == "plot_id")
            {
                ManIDname = "trt_name";  //standard in PHIS
            }
            else
            {
                ManIDname = ExIDname; //not sure about standard in other AgMIP json
            }
            
            JObject source = dbm.searchObjectByName(ExIDname, name);
           
            JObject result = new JObject();

            JObject MultiRunItem = new JObject();

            String s_id;

            if(source.ContainsKey("soil_id"))
            { s_id = "soil_id"; }
             else
             { s_id = "sid"; }

            String NonVarietalDBM = Export.searchFirstValue("crid", source.ToString());

            String NonVarietal;
             if (NonVarietalDBM == "BAR")
             { NonVarietal = "Barley"; }
             else if (NonVarietalDBM == "WHB")
             { NonVarietal = "Winter_Wheat"; }
            else if (NonVarietalDBM == "WHD")
            { NonVarietal = "Durum wheat"; }
            else
             { NonVarietal = NonVarietalDBM; }

            String VarietalDBM = Export.searchFirstValue("cul_name", source.ToString());

            String Varietal = "";
             if (VarietalDBM.Length > 0)
             {
                 if (VarietalDBM == "rubina ")
                 { Varietal = "Rubina"; }
                 else if (VarietalDBM == "grete")
                 { Varietal = "Grete"; }
                 else if (VarietalDBM.Length > 4 && VarietalDBM.Substring(0, 5) == "baron")
                 { Varietal = "Baroness"; }
                 else { Varietal = VarietalDBM; }
             }
             else
             { Varietal = "unknown"; }

            String experimentName = source["exname"].ToString();
            String IDAgMip = "";
            if (ExIDname=="id" || ExIDname == "plot_id")
            {

                IDAgMip = source[ExIDname].ToString();
            }



            MultiRunItem["ManagementItem"] = source[ManIDname].ToString();
            MultiRunItem["ParameterItem"] = NonVarietal;
             MultiRunItem["RunOptionItem"] = "Run_options";
            //////////////////////////////////////////////////TEST///////////////////////

            MultiRunItem["SiteItem"] = source["wst_id"].ToString();
            ////////////////////////////////////////////////////////////////////////////

            MultiRunItem["SoilItem"] = source[s_id].ToString();  //
            MultiRunItem["VarietyItem"] = Varietal;
            MultiRunItem["ExperimentItem"] = experimentName;
            MultiRunItem["RunIDAgMip"] =IDAgMip;

            result = MultiRunItem;
             return result.ToString();
         }

         
         public JObject buildMulti(bool isQuiet)
        //    public JObject buildMulti(String str, bool isQuiet)
        {
             int progress0 = 0;
             JObject result = new JObject();

            result["@name"] = "RUN_all";

            JArray MultiRunsItem = new JArray();
             JObject MultiRunItem = new JObject();
             //get all management names
             String[] man_name = dbm.getAllNames().ToArray();

            for (int i = 0; i < man_name.Length; i++)
                {

                 int progress = ((i * 20) / (man_name.Length )) ;
                if (progress > progress0)
                 {
                     progress0 = progress;
                     if (!isQuiet) Console.WriteLine((progress) + "%");
                 }

                 if (man_name[i] != "")
                    {

                    MultiRunItem = JObject.Parse((build(man_name[i])));
                    MultiRunsItem.Add(MultiRunItem);
                 }
                     
             }

             
             JObject Multi = new JObject();


            if (!Directory.Exists(dbm.ProjectDIR + System.IO.Path.DirectorySeparatorChar + dbm.OutputDIR))
            {

                try
                {
                    Directory.CreateDirectory(dbm.ProjectDIR + System.IO.Path.DirectorySeparatorChar + dbm.OutputDIR);
                }
                catch (Exception e)
                {
                    Directory.CreateDirectory(dbm.OutputDIR);
                }

            }

            Multi["OutputDirectory"] = dbm.OutputDIR ;
            Multi["OutputPattern"] = "Summary_output";
            Multi["ExportNormalRuns"] = "true";
            Multi["DailyOutputPattern"] = "$(VarietyName)$(ManagementName)";
            JObject T = new JObject();
             T["MultiRunItem"] = MultiRunsItem;
             Multi["MultiRunsArray"] = T;
           


            result["Multi"] = Multi;

            JObject Tsens = new JObject();
            Tsens["OutputDirectory"] = dbm.OutputDIR;

            result["Sensitivity"] = Tsens;
            return result;

        }

        public override void run(bool isQuiet)
        {

            string name = dbm.RunFilename;
            File.WriteAllText(dbm.ProjectDIR + "\\" + name + ".sqrun", (JSONtoXML(SQ_ICASA_Convertor.ICASAtoSQ(buildAll( isQuiet)))).Replace("_x0020_Date", "Date").Replace(" Date", "Date").Replace("False", "false"));

            
        }
    }
}
