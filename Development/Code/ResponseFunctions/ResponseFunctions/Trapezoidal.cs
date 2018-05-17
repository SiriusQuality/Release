using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

/// 
/// Andrea Maiorano
/// andrea.maiorano@supagro.inra.fr
/// maiorano.andrea@gmail.com
/// UMR LEPSE, INRA-SupAGRO, Montpellier
/// www6.montpellier.inra.fr/lepse/
/// 
namespace ResponseFunctions
{
    public class Trapezoidal
    {

        #region Implementation of IAnnotatable

        /// <summary>
        /// Description of the model
        /// </summary>
        public string Description
        {
            get { return "Trapezoidal response function. Response function including an increasing phase from a minimum value (below which the response is equal to zero) " +
                         "to an optimum-minimum value, a plateau from an optimum-minimum to an optimum-maximum value where the output is equal to 1, a decreasing phase from the" +
                         " optimum-maximum value to a maximum value above which the esponse is equal to zero. Please note that the function can also be used in the case of" +
                         " rectangular trapezoidal responses. In this case the minimum value (or the maximum) coincides with the optimum-minimum (or the optimum-maximum) value.";
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
        /// Trapezoidal response function
        /// </summary>
        /// <param name="minimumValue">Independent variable minimum value below which the response function result is zero</param>
        /// <param name="optimumValueStart">Independent variable minimum value of the plateu region where the result of the response function is equal to 1</param>
        /// <param name="optimumValueEnd">Independent variable maximum value of the plateu region where the result of the response function is equal to 1</param>
        /// <param name="maximumValue">Independent variable maximum value above which the response function result is zero</param>
        /// <param name="independentVariable"></param>
        /// <returns></returns>
        public double Calculate(double minimumValue, double optimumValueStart, double optimumValueEnd, double maximumValue, double independentVariable)
        {
            double output = new double();

            if (independentVariable > minimumValue && independentVariable < maximumValue)
            {
                if (optimumValueStart == minimumValue)
                {
                    if (independentVariable <= optimumValueEnd)
                    {
                        output = 1;
                    }
                    else if (independentVariable > optimumValueEnd && independentVariable < maximumValue)
                    {
                        output = (-1 / (maximumValue - optimumValueEnd)) * (independentVariable - optimumValueEnd) + 1;
                    }

                }
                else if (optimumValueEnd == maximumValue)
                {
                    if (independentVariable <= optimumValueStart)
                    {
                        output = (1 / (optimumValueStart - minimumValue)) * (independentVariable - minimumValue);
                    }
                    else if (independentVariable > optimumValueStart)
                    {
                        output = 1;
                    }
                }
                else
                {
                    if (independentVariable > minimumValue && independentVariable < optimumValueStart)
                    {
                        output = (1 / (optimumValueStart - minimumValue)) * (independentVariable - minimumValue);
                    }
                    else if (independentVariable >= optimumValueStart && independentVariable <= optimumValueEnd)
                    {
                        output = 1;
                    }
                    else if (independentVariable >= optimumValueEnd && independentVariable < maximumValue)
                    {
                        output = (-1 / (maximumValue - optimumValueEnd)) * (independentVariable - optimumValueEnd) + 1;
                    }
                }
            }
            else
            {
                output = 0;
            }


            return output;
        }

        


    }
}
