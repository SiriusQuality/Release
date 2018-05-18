using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ResponseFunctions
{
    public class WangEngel
    {

        #region Implementation of IAnnotatable

        /// <summary>
        /// Description of the model
        /// </summary>
        public string Description
        {
            get { return "Wang and Engel nonlinear response function. Wang and Engel 1998, Simulation of phenological development of wheat crops, " +
                         "Agricultural Systems, 58(1):1-24"; }
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
        /// Wang and Engel nonlinear response function
        /// </summary>
        /// <param name="minimumValue">Independent variable minimum value below which the response function result is zero</param>
        /// <param name="optimumValue">Independent variable optimum value at which the response function result is equal to 1</param>
        /// <param name="maximumValue">Independent variable maximum value above which the response function result is zero</param>
        /// <param name="shapeParameter">Shape parameter</param>
        /// <param name="independentVariable">Independent variable - input</param>
        /// <returns></returns>
        public double Calculate(double minimumValue, double optimumValue, double maximumValue, double shapeParameter,
            double independentVariable)
        {
            double output = new double();

            if (independentVariable <= minimumValue || independentVariable >= maximumValue)
            {
                output = 0;
            }
            else
            {

                double alpha = Math.Log(2) / Math.Log((maximumValue - minimumValue) / (optimumValue - minimumValue));

                output = (2 * Math.Pow(independentVariable - minimumValue, alpha) * Math.Pow(optimumValue - minimumValue, alpha) - Math.Pow(independentVariable - minimumValue, (2 * alpha)))
                    / Math.Pow(optimumValue - minimumValue, (2 * alpha));


            }


            return output;
        }

        


    }
}
