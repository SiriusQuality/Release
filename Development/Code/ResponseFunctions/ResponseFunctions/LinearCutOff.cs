using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseFunctions
{
    public class LinearCutOff
    {
        #region Implementation of IAnnotatable

        /// <summary>
        /// Description of the model
        /// </summary>
        public string Description
        {
            get
            {
                return "Linear response function with cut-off";
            }
        }

        /// <summary>
        /// URL to access the description of the model
        /// </summary>
        public string URL
        {
            get { return "http://biomamodelling.org"; }
        }
        #endregion

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minimumValue">Independent variable minimum value below which the response function result is zero</param>
        /// <param name="cutOffValue">Independent variable value above which the response function is equal to 1</param>
        /// <param name="slope">Slope of the linear response function</param>
        /// <param name="independentVariable">Independent Variable</param>
        /// <returns></returns>
        public double Calculate(double minimumValue, double cutOffValue, double slope, double independentVariable)
        {
            double output;

            if (independentVariable >= cutOffValue)
            {
                output = 1;
            }
            else
            {
                output = Math.Max(0, independentVariable - minimumValue) * slope;
 
            }
            
            return output;
        }


    }
}
