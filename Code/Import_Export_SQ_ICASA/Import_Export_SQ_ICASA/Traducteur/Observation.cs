using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Import_Export_SQ_ICASA
{

    /// <summary>
    /// This is an abstract class with all functions that childrens of this class (SQCAN,SQMAT,or more) 
    /// will have to implement inorder to generate observation files.
    /// 
    /// If you want to implement a new observation file, create a sub class of Observation, implement the functions then 
    /// add to the observation to the observation list in ImportDBM.
    /// </summary>
    abstract class Observation
    {

        /// <summary>
        /// Permits to use utility functions and get the json string.
        /// </summary>
        protected ImportDBM dbm;

        
        /// <summary>
        /// List of sqmat possible attributes if one attribute needs an other, it won't be listed here but will be
        /// </summary>
        protected List<String> paramList;


        //added to the attributes array in <see cref="paramList"/>.
        /// <summary>
        ///Function that returns the list of sqcan/sqmat possible attributes, 
        ///if one attribute needs an other, it won't be listed here but will be
        ///added to the attributes array in <see cref="listParam()"/>
        ///
        /// If their is parameters that are not added here that could be helpfull inorder to get data
        /// add it in the implementation function by adding the attribute in a string list.
        /// </summary>
        /// <returns>List of parameters needed to found or compute values.</returns>
        abstract protected List<String> listParam();


        /// <summary>
        /// Create the object and registers the ImportDBM object inorder to work with te json and have usefull functions.
        /// </summary>
        /// <param name="db"></param>
        public Observation(ImportDBM db)
        {
            dbm = db;
        }



        /// <summary>
        /// In the observation file, there is a line which specifies
        /// the unit and an other one that tells if the valued is an average,
        /// or an standard deviation. This function permits to build those two lines.
        /// 
        /// If the attributes haves units, you should add a case inorder to add the case.
        /// </summary>
        /// <param name="param"></param>
        /// <returns>Array of units to add after the name parameters of an observation file</returns>
        protected String[] buildUnits(String[] param)
        {

            String units = "";
            String val = "";
            foreach (String s in param)
            {
                switch (s)
                {
                    case "Sowing Date":
                        units += "yyyy-mm-dd";
                        break;
                    case "Date":
                        units += "yyyy-mm-dd";
                        break;
                    case "Sampling year":
                        units += "yyyy-mm-dd";
                        break;
                    case "DOY":
                        units += "dd";
                        break;
                    case "shwad":
                        units += "kgDM/ha\tkgDM/ha";
                        val += "average\tsd";
                        break;
                    case "Crop nitrogen":
                        units += "kgH/ha\tkgH/ha";
                        val += "average\tsd";
                        break;
                    //case "ZC92_Maturity":
                    //    units += "yyyy-mm-dd\tyyyy-mm-dd";
                    //    val += "average\tsd";
                    //    break;
                    //case "ZC65_Anthesis":
                    //    units += "yyyy-mm-dd\tyyyy-mm-dd";
                    //    val += "average\tsd";
                    //    break;
                    case "PALPAD":
                        units += "shoot/m^2\tshoot/m^2";
                        val += "average\tsd";
                        break;
                    case "gwam":
                        units += "kgDM/ha\tkgDM/ha";
                        val += "average\tsd";
                        break;
                    case "cwam":
                        units += "kgDM/ha\tkgDM/ha";
                        val += "average\tsd";
                        break;
                    case "Maturity crop nitrogen":
                        units += "KgN/ha";
                        val += "average\tsd";
                        break;
                    case "adat":
                        units += "yyyy-mm-dd\tyyyy-mm-dd";
                        val += "average\tsd";
                        break;
                    case "mdat":
                        units += "yyyy-mm-dd\tyyyy-mm-dd";
                        val += "average\tsd";
                        break;
                    case "pldae":
                        units += "yyyy-mm-dd\tyyyy-mm-dd";
                        val += "average\tsd";
                        break;
                    case "hnoam": //grain number
                        units += "number/m²\tnumber/m²";
                        val += "average\tsd";
                        break;
                    case "gwgm": //grain weight
                        units += "mgDM/grain\tmgDM/grain";
                        val += "average\tsd";
                        break;
                    case "gprcm": //grain protein
                        units += "% of grain DM\t% of grain DM";
                        val += "average\tsd";
                        break;
                    case "laix": //LAI
                        units += "m²/ m²\t m²/ m²";
                        val += "average\tsd";
                        break;
                    case "laid": //LAI
                        units += "m²/ m²\t m²/ m²";
                        val += "average\tsd";
                        break;
                    case "gnoad": //grain number
                        units += "number/m²\tnumber/m²";
                        val += "average\tsd";
                        break;
                    case "gwad": //grain DM
                        units += "kgDM/ha\tkgDM/ha";
                        val += "average\tsd";
                        break;
                    case "cwad": //crop DM
                        units += "kgDM/ha\tkgDM/ha";
                        val += "average\tsd";
                        break;
                    case "slnd": //specific leaf lamina DM
                        units += "gDM/m²\tgDM/m²";
                        val += "average\tsd";
                        break;
                    case "lwad": //tot leaf lamina DM
                        units += "kgDM/ha\tkgDM/ha";
                        val += "average\tsd";
                        break;
                    case "lgnad": //leaf lamina N
                        units += "kgN/ha\tkgN/ha";
                        val += "average\tsd";
                        break;
                    case "gnad": //grain N
                        units += "kgN/ha\tkgN/ha";
                        val += "average\tsd";
                        break;

                        //Add more cases if there is more attributes to manage

                }
                units += "\t";
                val += "\t";
            }
            String[] result = { units, val };
            return result;
        }

        /// <summary>
        /// Create the list of paramters of SQMAT/SQCAN, as there is various parameters
        /// that could be in the file, but not all are available.
        /// 
        /// If there is output attributes that can be added and are not in the array, add it in the switch statement by adding
        /// the case of the attribute helping to get the outputattribute and add the output attribute to the array.
        /// </summary>
        /// <returns>String array of available parameters to write.</returns>
        abstract protected String[] buildParam();
        
        
        /// <summary>
        ///Create a CSV with '\t' as separator. An observations file format is defined by :
        ///  1st line: Says if the column is an average, or an standard deviation, or nothing.
        ///  2nd line: Tells the attributes
        ///  3rd line: Tells the unit of the column
        ///  4th+ lines: The observation data
        /// In the function, it first builds de parameters because there could be several parameters 
        /// that we don't want to be in the file because we can not get values from the current json file.
        /// Then we get loads of observations site by site and append the observations to an observation list.
        /// After that it builds the 1st and 3rd line with the parameters.
        /// Once we have all the data we want, we start treating the data by converting the values and the name of parameters.
        /// Convert it into CSV with '\t' sperator, split it in lines, append the 1st line and 3rd line at 1st item
        /// and finaly join all elements of the array with '\n'. Then we have our result ready to be returned.
        /// </summary>
        /// <returns>Observation output to write.</returns>
        abstract protected String buildAll(bool isQuiet);


        /// <summary>
        /// Create all observations that we can found on a site and takes only the data that are needed in the attributes.
        /// As attributes is not a fixed array we can't assume that we need X values.
        /// 
        /// Inorder to add access method to an attribute, you should add a if/else if/else statement at the
        /// end of the case of the attribute in the switch statement.
        /// 
        /// If you want to add output attributes, add a case statement with its 
        /// name and add an access method inorder to get the value.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="attr"></param>
        /// <returns> Observation line with the required attributes.</returns>
        abstract protected String build(String name, String[] attr);


        /// <summary>
        /// This function launches the treatment by using <see cref="buildAll()"/> and writing the output at the right location.
        /// </summary>
        abstract public void run(bool isQuiet);



        //The function first write the attributes
        //Then it writes the data
        /// <summary>
        /// Convert an observation json to an observation csv.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="seperator"></param>
        /// <returns>Converted observation csv</returns>
        protected String ObsjsonToCSV(JArray content, char seperator)
        {
            if (!(content == null) || content.Count > 0)
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
            return "";
        }
    }
}
