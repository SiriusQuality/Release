using System;

namespace SiriusModel.InOut
{
    public class RunException : Exception
    {
        public RunException(string simpleRunName, string message)
            : base(simpleRunName + " : " + message)
        {

        }

        public RunException(string simpleRunName, string message, DateTime date)
            : base(simpleRunName + "[" + date.ToString("u").Split()[0] + "] : " + message)
        {

        }
    }
}
