using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseFunctions
{
    public class Triangular
    {
        #region Implementation of IAnnotatable

        /// <summary>
        /// Description of the model
        /// </summary>
        public string Description
        {
            get
            {
                return "Triangular response function. Broken linear model composed of two linear functions one increasing from the minimum value to the optimum value" +
                       " and the second decreasing from the optimum value to the maximum value";
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
        /// Triangular response function
        /// </summary>
        /// <param name="minimumValue">Independent variable minimum value below which the response function result is zero</param>
        /// <param name="optimumValue">Independent variable optimum value at which the response function result is equal to 1</param>
        /// <param name="maximumValue">Independent variable maximum value above which the response function result is zero</param>
        /// <param name="independentVariable">Independent variable</param>
        /// <returns></returns>
        public double Calculate(double minimumValue, double optimumValue, double maximumValue, double independentVariable)
        {
            double output = new double();

            if (independentVariable <= minimumValue || independentVariable >= maximumValue)
            {
                output = 0;
            }
            else if (independentVariable < optimumValue)
            {
                output = (1 / (optimumValue - minimumValue)) * (independentVariable - minimumValue);
            }
            else if (independentVariable >= optimumValue)
            {
                output = (-1 / (maximumValue - optimumValue)) * (independentVariable - optimumValue) + 1;
            }

            
            return output;
        }


    }
}
