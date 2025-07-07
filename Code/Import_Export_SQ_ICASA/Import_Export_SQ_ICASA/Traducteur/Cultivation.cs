using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Import_Export_SQ_ICASA
{
    /// <summary>
    /// This is an abstract class with all functions that childrens of this class (management,site,soil, or more) 
    /// will have to implement inorder to generate core files.
    /// 
    /// If you want to implement a new cultivation file, create a sub class of Cultivation, implement the functions then 
    /// add to the cultivation to the cultivation list in ImportDBM.
    /// </summary>
    abstract class Cultivation
    {
        /// <summary>
        /// Permits to use utility functions and get the json string.
        /// </summary>
        protected ImportDBM dbm;

        /// <summary>
        /// Create a JSON of all data needed inorder to create (site.sqsit/soil.sqsoi/management.sqman)
        /// which will be converted into SQ values and in XML format.
        /// The function takes all the names of experiments and calls the build function for a single site/management/soil.
        /// </summary>
        /// <returns>Output data to write.</returns>
        abstract protected String buildAll(bool isQuiet);
        //abstract protected String buildAll(String str, bool isQuiet);

        //See in the child build implementation
        /// <summary>
        /// Create each items for the <see cref="buildAll()"/> function. The item can be one site, one soil or one management.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Item with all the data to return</returns>
        abstract protected String build(String name);

        
        


        /// <summary>
        /// Creates the object and registers the ImportDBM object in order to work with the json and have useful functions.
        /// </summary>
        /// <param name="db"></param>
        protected Cultivation(ImportDBM db)
        {
            dbm = db;
        }
        /// <summary>
        /// The json is first converted in XmlDocument.
        /// Then it's parsed into XDocument and we get our XML with the ToString() function
        /// </summary>
        /// <param name="json"></param>
        /// <returns>XML string converted from the json string.</returns>
        protected String JSONtoXML(String json)
        {
            return "<?xml version=\"1.0\"?>\n" + XDocument.Parse(JsonConvert.DeserializeXmlNode(json).InnerXml).ToString();
        }

        /// <summary>
        /// This function launches the treatment by using <see cref="buildAll()"/> and writing the output at the right location.
        /// </summary>
        abstract public void run(bool isQuiet);
        //abstract public void run(String str, bool isQuiet);


        /// <summary>
        /// This function either extracts the bulk density from the json or calculates the value using pedotransfer functions 
        /// </summary>
        public double calculateBD(JObject soillayer)
        {

            String BDid = dbm.getBulkDensityIdentifier();
            double bdvalue = 0;
            if (BDid != "")
            {
                bdvalue = double.Parse((String)soillayer[BDid]);
            }
            else
            {
                if (soillayer.ContainsKey("sloc"))
                {
                    bdvalue = 1 / (0.635 + 0.059 * double.Parse((String)soillayer["sloc"]));
                }
                else if (soillayer.ContainsKey("slom"))
                {
                    double sloc = calculateOC(soillayer);
                    bdvalue = 1 / (0.635 + 0.059 * sloc);
                }
            }

            return bdvalue;
        }
        /// <summary>
        /// Pedotransfer function to calculate SLOC from SLOM
        /// </summary>
        public double calculateOC(JObject soillayer)
        {

            double ocvalue = double.Parse((String)soillayer["slom"]) / 1.72;
            return ocvalue;
        }

        /// <summary>
        /// Function to extract or calculate variable (that will be integrated across different soillayers) 
        /// </summary>
        public double extract_variable(JObject soillayer, String id)
        {
            if (id == "fc")
            {
                String FCid = dbm.getFieldCapacityIdentifier();
                //double fc = double.Parse(Export.searchFirstValue(FCid, soillayer.ToString()));
                double fc = double.Parse(soillayer[FCid].ToString());
                return fc;
            }
            else if (id == "bd")
            {
                double bd = calculateBD(soillayer);
                return bd;
            }
            else if (id == "N_inorg")
            {
                //double N_inorg = double.Parse(Export.searchFirstValue("icno3", soillayer.ToString()))+ double.Parse(Export.searchFirstValue("icnh4", soillayer.ToString()));
                double N_inorg = double.Parse(soillayer["icno3"].ToString()) + double.Parse(soillayer["icnh4"].ToString());
                return N_inorg;
            }
            else if (id == "N_inorgm")
            {
                //double N_inorgm = double.Parse(Export.searchFirstValue("icno3m", soillayer.ToString())) + double.Parse(Export.searchFirstValue("icnh4m", soillayer.ToString()));
                double N_inorgm = double.Parse(soillayer["icno3m"].ToString()) + double.Parse(soillayer["icnh4m"].ToString());
                return N_inorgm;
            }
            else if (id == "N_inorg_tot")
            {
                //double N_inorgm = double.Parse(Export.searchFirstValue("icno3m", soillayer.ToString())) + double.Parse(Export.searchFirstValue("icnh4m", soillayer.ToString()));
                double N_inorgm = double.Parse(soillayer["icn_tot"].ToString());
                return N_inorgm;
            }
            else 
            {
                return 0;
            }
        }

        /// <summary>
        /// This function finds the right identifier for the soil layer base depth. 
        /// The result can be either "icbl"  or "sbbl" 
        /// </summary>
        /// <returns>identifier for the soil layer base depth</returns>
        public String getSLBIdentifier(JObject soillayer)
        {
           //if (Export.searchFirstValue("sllb", soillayer.ToString()).Length > 0)
           if (soillayer.ContainsKey("sllb"))
             {
                return "sllb";
            }
            //else if (Export.searchFirstValue("icbl", soillayer.ToString()).Length > 0)
            else if (soillayer.ContainsKey("icbl"))
            {
                return "icbl";
            }
            else
            { throw new ArgumentException("Cannot translate: no identifier for base depth found"); }
        }
    }
}
