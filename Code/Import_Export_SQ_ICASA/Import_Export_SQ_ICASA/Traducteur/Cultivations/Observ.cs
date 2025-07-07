using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Import_Export_SQ_ICASA.Traducteur.Cultivations
{
    class Observ : Cultivation
    {
        public Observ(ImportDBM dbm)
           : base(dbm)
        { }

        public void run(bool isQuiet, string[] paths)
        {
            string name = dbm.ObservationFileName;
            File.WriteAllText(dbm.ProjectDIR + "\\" + name + ".sqobs", JSONtoXML(/*SQ_ICASA_Convertor.ICASAtoSQ(*/buildAll(isQuiet,paths)))/*)*/;
        }

        public override void run(bool isQuiet)
        {
            return;
        }

        protected string build(string name, string[] paths)
        {
            String s_id;
            JObject result = new JObject();

            result["SummaryObservationFile"] = paths[0];
            result["CanopyObservationFile"] = paths[1];
            result["CanopyLayerObservationFile"] = paths[2];
            result["SoilLayerObservationFile"] = paths[3];
            result["RootLayerObservationFile"] = paths[4];

            return result.ToString();
        }

        protected string buildAll(bool isQuiet, string[] paths)
        {
            JArray arr = new JArray();
            String s = dbm.ObservationFileName;
            arr.Add(JObject.Parse(build(s,paths)));

            JObject ia = new JObject(), sf = new JObject(), result = new JObject();
            ia["ObservationItem"] = arr;
            sf["ItemsArray"] = ia;
            sf["@xmlns:xsd"] = "http://www.w3.org/2001/XMLSchema";
            sf["@xmlns:xsi"] = "http://www.w3.org/2001/XMLSchema-instance";
            result["ObservationFile"] = sf;
            return result.ToString();
        }

        protected override string build(string name)
        {
            return "";
        }

        protected override string buildAll(bool isQuiet)
        {
            return "";
        }
    }
}
