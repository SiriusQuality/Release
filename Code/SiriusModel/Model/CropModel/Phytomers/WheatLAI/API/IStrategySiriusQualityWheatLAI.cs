/*
     -----------------------------------------------------------

     Code generated by CRA.ModelLayer.ACC - API Code Generator
     http://components.biomamodelling.org/components/acc/help/ 

     MyName MyLastname
     myemail@mydomain.com
     MyInstitution
     14/12/2016 12:42:34 PM

     -----------------------------------------------------------
*/

using System;
using CRA.ModelLayer.Strategy;


namespace SiriusQualityWheatLAI
{
    /// <summary>
    /// SiriusQualityWheatLAI component model interface   
    /// </summary>
    public interface IStrategySiriusQualityWheatLAI : IStrategy
    {
        /// <summary>
        /// Calculate method for models
        /// </summary>
        /// <param name=w>WheatLAIState Domain class contains the accessors to values</param>
        void Estimate
        ( WheatLAIState w);
        
        /// <summary>
        /// Test of pre conditions
        /// </summary>
        /// <param name=w>WheatLAIState Domain class contains the accessors to values</param>
        /// <param name=callID>Preconditions test context</param>
        /// <returns>List of violations</returns>
        string TestPreConditions( WheatLAIState w, string callID);
        
        /// <summary>
        /// Test of post-conditions
        /// </summary>
        /// <param name=w>WheatLAIState Domain class contains the accessors to values</param>
        /// <param name=callID>Postconditions test context</param>
        /// <returns>List of violations</returns>
        string TestPostConditions( WheatLAIState w, string callID);
    
        /// <summary>
        /// Set parameters to the default value
        /// </summary>
        void SetParametersDefaultValue();
    }
}