using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseFunctions
{
    public class Linear
    {
        #region Implementation of IAnnotatable

        /// <summary>
        /// Description of the model
        /// </summary>
        public string Description
        {
            get
            {
                return "Linear response function";
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
        /// "Linear response function"
        /// </summary>
        /// <param name="minimumValue">Independent variable minimum value below which the response function result is zero</param>
        /// <param name="slope">Slope of the linear response function</param>
        /// <param name="independentVariable">Indipendent variable</param>
        /// <returns></returns>
        public double Calculate(double minimumValue, double slope, double independentVariable)
        {
            double output = new double();

            output = Math.Max(0, independentVariable - minimumValue)*slope;

            return output;
        }


    }
}
