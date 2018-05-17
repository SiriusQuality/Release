

 //Author:pierre martre 
 //Institution:pierre.martre@inra.fr
 //Author of revision: 
 //Date first release:9/7/2017
 //Date of revision:

using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using CRA.ModelLayer.MetadataTypes;
using CRA.ModelLayer.Core;
using CRA.ModelLayer.Strategy;
using System.Reflection;
using VarInfo=CRA.ModelLayer.Core.VarInfo;
using Preconditions=CRA.ModelLayer.Core.Preconditions;


using SiriusQualityMeteo;


//To make this project compile please add the reference to assembly: SiriusQuality-MeteoComponent, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: CRA.ModelLayer, Version=1.0.5212.29139, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: CRA.AgroManagement2014, Version=0.8.0.0, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089;

namespace SiriusQualityMeteo.Strategies
{

	/// <summary>
	///Class CalculateHourlyAirTemperature
    /// 
    /// </summary>
	public class CalculateHourlyAirTemperature : IStrategySiriusQualityMeteo
	{

	#region Constructor

			public CalculateHourlyAirTemperature()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 0;
				 v1.Description = "latitude";
				 v1.Id = 0;
				 v1.MaxValue = 90;
				 v1.MinValue = -90;
				 v1.Name = "latitude";
				 v1.Size = 1;
				 v1.Units = "°";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v1);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd1.PropertyName = "maxTair";
				pd1.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.maxTair)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.maxTair);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd2.PropertyName = "minTair";
				pd2.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.minTair)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.minTair);
				_inputs0_0.Add(pd2);
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd3.PropertyName = "dayOfYear";
				pd3.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.dayOfYear)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.dayOfYear);
				_inputs0_0.Add(pd3);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd4.PropertyName = "hourlyAirTemperature";
				pd4.PropertyType =  (( SiriusQualityMeteo.MeteoStateVarInfo.hourlyAirTemperature)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =(  SiriusQualityMeteo.MeteoStateVarInfo.hourlyAirTemperature);
				_outputs0_0.Add(pd4);
				mo0_0.Outputs=_outputs0_0;
				//Associated strategies
				List<string> lAssStrat0_0 = new List<string>();
				mo0_0.AssociatedStrategies = lAssStrat0_0;
				//Adding the modeling options to the modeling options manager
				_modellingOptionsManager = new ModellingOptionsManager(mo0_0);
			
				SetStaticParametersVarInfoDefinitions();
				SetPublisherData();
					
			}

	#endregion

	#region Implementation of IAnnotatable

			/// <summary>
			/// Description of the model
			/// </summary>
			public string Description
			{
				get { return ""; }
			}
			
			/// <summary>
			/// URL to access the description of the model
			/// </summary>
			public string URL
			{
				get { return "http://biomamodelling.org"; }
			}
		

	#endregion
	
	#region Implementation of IStrategy

			/// <summary>
			/// Domain of the model.
			/// </summary>
			public string Domain
			{
				get {  return "Weather"; }
			}

			/// <summary>
			/// Type of the model.
			/// </summary>
			public string ModelType
			{
				get { return ""; }
			}

			/// <summary>
			/// Declare if the strategy is a ContextStrategy, that is, it contains logic to select a strategy at run time. 
			/// </summary>
			public bool IsContext
			{
					get { return  false; }
			}

			/// <summary>
			/// Timestep to be used with this strategy
			/// </summary>
			public IList<int> TimeStep
			{
				get
				{
					IList<int> ts = new List<int>();
					
					return ts;
				}
			}
	
	
	#region Publisher Data

			private PublisherData _pd;
			private  void SetPublisherData()
			{
					// Set publishers' data
					
				_pd = new CRA.ModelLayer.MetadataTypes.PublisherData();
				_pd.Add("Creator", "");
				_pd.Add("Date", "9/7/2017");
				_pd.Add("Publisher", "pierre.martre@inra.fr");
			}

			public PublisherData PublisherData
			{
				get { return _pd; }
			}

	#endregion

	#region ModellingOptionsManager

			private ModellingOptionsManager _modellingOptionsManager;
			
			public ModellingOptionsManager ModellingOptionsManager
			{
				get { return _modellingOptionsManager; }            
			}

	#endregion

			/// <summary>
			/// Return the types of the domain classes used by the strategy
			/// </summary>
			/// <returns></returns>
			public IEnumerable<Type> GetStrategyDomainClassesTypes()
			{
				return new List<Type>() {  typeof(SiriusQualityMeteo.MeteoState) };
			}

	#endregion

    #region Instances of the parameters
			
			// Getter and setters for the value of the parameters of the strategy. The actual parameters are stored into the ModelingOptionsManager of the strategy.

			
			public Double latitude
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("latitude");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'latitude' not found (or found null) in strategy 'CalculateHourlyAirTemperature'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("latitude");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'latitude' not found in strategy 'CalculateHourlyAirTemperature'");
				}
			}

			// Getter and setters for the value of the parameters of a composite strategy
			

	#endregion		

	
	#region Parameters initialization method
			
            /// <summary>
            /// Set parameter(s) current values to the default value
            /// </summary>
            public void SetParametersDefaultValue()
            {
				_modellingOptionsManager.SetParametersDefaultValue();
				 

					//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section5
					//Code written below will not be overwritten by a future code generation

					//Custom initialization of the parameter. E.g. initialization of the array dimensions of array parameters

					//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
					//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section5 
            }

	#endregion		

	#region Static parameters VarInfo definition

			// Define the properties of the static VarInfo of the parameters
			private static void SetStaticParametersVarInfoDefinitions()
			{                                
                latitudeVarInfo.Name = "latitude";
				latitudeVarInfo.Description =" latitude";
				latitudeVarInfo.MaxValue = 90;
				latitudeVarInfo.MinValue = -90;
				latitudeVarInfo.DefaultValue = 0;
				latitudeVarInfo.Units = "°";
				latitudeVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _latitudeVarInfo= new VarInfo();
				/// <summary> 
				///latitude VarInfo definition
				/// </summary>
				public static VarInfo latitudeVarInfo
				{
					get { return _latitudeVarInfo; }
				}					
			
			//Parameters static VarInfo list of the composite class
						

	#endregion
	
	#region pre/post conditions management		

		    /// <summary>
			/// Test to verify the postconditions
			/// </summary>
			public string TestPostConditions(SiriusQualityMeteo.MeteoState meteostate, string callID)
			{
				try
				{
					//Set current values of the outputs to the static VarInfo representing the output properties of the domain classes				
					
					SiriusQualityMeteo.MeteoStateVarInfo.hourlyAirTemperature.CurrentValue=meteostate.hourlyAirTemperature;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.hourlyAirTemperature);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.hourlyAirTemperature.ValueType)){prc.AddCondition(r4);}

					

					//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section4
					//Code written below will not be overwritten by a future code generation

        

					//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
					//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section4 

					//Get the evaluation of postconditions
					string postConditionsResult =pre.VerifyPostconditions(prc, callID);
					//if we have errors, send it to the configured output 
					if(!string.IsNullOrEmpty(postConditionsResult)) { pre.TestsOut(postConditionsResult, true, "PostConditions errors in component SiriusQualityMeteo.Strategies, strategy " + this.GetType().Name ); }
					return postConditionsResult;
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1001,	"Strategy: " + this.GetType().Name + " - Unhandled exception running post-conditions");

					string msg = "Component SiriusQualityMeteo.Strategies, " + this.GetType().Name + ": Unhandled exception running post-condition test. ";
					throw new Exception(msg, exception);
				}
			}

			/// <summary>
			/// Test to verify the preconditions
			/// </summary>
			public string TestPreConditions(SiriusQualityMeteo.MeteoState meteostate, string callID)
			{
				try
				{
					//Set current values of the inputs to the static VarInfo representing the input properties of the domain classes				
					
					SiriusQualityMeteo.MeteoStateVarInfo.maxTair.CurrentValue=meteostate.maxTair;
					SiriusQualityMeteo.MeteoStateVarInfo.minTair.CurrentValue=meteostate.minTair;
					SiriusQualityMeteo.MeteoStateVarInfo.dayOfYear.CurrentValue=meteostate.dayOfYear;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.maxTair);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.maxTair.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.minTair);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.minTair.ValueType)){prc.AddCondition(r2);}
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.dayOfYear);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.dayOfYear.ValueType)){prc.AddCondition(r3);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("latitude")));

					

					//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section3
					//Code written below will not be overwritten by a future code generation

        

					//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
					//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section3 
								
					//Get the evaluation of preconditions;					
					string preConditionsResult =pre.VerifyPreconditions(prc, callID);
					//if we have errors, send it to the configured output 
					if(!string.IsNullOrEmpty(preConditionsResult)) { pre.TestsOut(preConditionsResult, true, "PreConditions errors in component SiriusQualityMeteo.Strategies, strategy " + this.GetType().Name ); }
					return preConditionsResult;
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//	TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1002,"Strategy: " + this.GetType().Name + " - Unhandled exception running pre-conditions");

					string msg = "Component SiriusQualityMeteo.Strategies, " + this.GetType().Name + ": Unhandled exception running pre-condition test. ";
					throw new Exception(msg, exception);
				}
			}

		
	#endregion
		


	#region Model

		 	/// <summary>
			/// Run the strategy to calculate the outputs. In case of error during the execution, the preconditions tests are executed.
			/// </summary>
			public void Estimate(SiriusQualityMeteo.MeteoState meteostate)
			{
				try
				{
					CalculateModel(meteostate);

					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Verbose, 1005,"Strategy: " + this.GetType().Name + " - Model executed");
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1003,		"Strategy: " + this.GetType().Name + " - Unhandled exception running model");

					string msg = "Error in component SiriusQualityMeteo.Strategies, strategy: " + this.GetType().Name + ": Unhandled exception running model. "+exception.GetType().FullName+" - "+exception.Message;				
					throw new Exception(msg, exception);
				}
			}

		

			private void CalculateModel(SiriusQualityMeteo.MeteoState meteostate)
			{				
				

				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section1
				//Code written below will not be overwritten by a future code generation

                meteostate.hourlyAirTemperature = getHourlyTemperature(meteostate.maxTair, meteostate.minTair, meteostate.dayOfYear, latitude, 1.5, 4, 1);
                //meteostate.hourlyAirTemperature = getHourlyTemperature(meteostate.maxTair, meteostate.minTair, meteostate.dayOfYear, meteostate.dayLength, 1.5, 4, 1);
        

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation
        
            private double[] getHourlyTemperature(double TMax, double TMin, int julianDate, double latitudeDeg, double maxLag, double nightCoef, double minLag)
            {
                double[] result = new double[24];

                double latitudeRad = (latitudeDeg) * Math.PI / 180;
                //calculate day length and night length
                double adelt = 0.4014 * Math.Sin(6.28 * (julianDate - 77) / 365);
                double tem1 = 1 - Math.Pow((-Math.Tan(latitudeRad) * adelt), 2);
                tem1 = Math.Sqrt(tem1);
                double tem2 = (-Math.Tan(latitudeRad) * Math.Tan(adelt));
                double ahou = Math.Atan2(tem1, tem2);
                double ady = (ahou / 3.14) * 24;
                double ani = (24 - ady);
                double bb = 12 - ady / 2 + minLag;
                double be = 12 + ady / 2;

                for (int i = 1; i <= 24; i++)
                {
                    if (i >= bb && i <= be)
                    {
                        result[i - 1] = (TMax - TMin) * Math.Sin(3.14 * (i - bb) / (ady + 2 * maxLag)) + TMin;
                    }
                    else
                    {
                        double bbd;
                        if (i > be) { bbd = i - be; }
                        else //i<bb
                        {
                            bbd = 24 - be + i;
                        }
                        double ddy = ady - minLag;
                        double tsn = (TMax - TMin) * Math.Sin(3.14 * ddy / (ady + 2 * maxLag)) + TMin;
                        result[i - 1] = TMin + (tsn - TMin) * Math.Exp(-nightCoef * bbd / ani);
                    }
                }
                return result;
            }
         

        
        
         //       private double[] getHourlyTemperature(double TMax, double TMin, int julianDate,double ady, double maxLag, double nightCoef, double minLag)
         //       {
         //           double[] result = new double[24];

         //           //double latitudeRad = (latitudeDeg) * Math.PI / 180;
         //           //calculate day length and night length
         //           //double adelt = 0.4014 * Math.Sin(6.28 * (julianDate - 77) / 365);
         //           //double tem1 = 1 - Math.Pow((-Math.Tan(latitudeRad) * adelt), 2);
         //           //tem1 = Math.Sqrt(tem1);
         //           //double tem2 = (-Math.Tan(latitudeRad) * Math.Tan(adelt));
         //           //double ahou = Math.Atan2(tem1, tem2);
         //           //double ady = (ahou / 3.14) * 24;
         //           double ahou = Math.PI*(ady / 24.0);
         //           double ani = (24 - ady);
         //           double bb = 12 - ady / 2 + minLag;
         //           double be = 12 + ady / 2;

         //           for (int i = 1; i <= 24; i++)
         //           {
         //               if (i >= bb && i <= be)
         //               {
         //                   result[i - 1] = (TMax - TMin) * Math.Sin(Math.PI * (i - bb) / (ady + 2 * maxLag)) + TMin;
         //               }
         //               else
         //               {
         //                   double bbd;
         //                   if (i > be) { bbd = i - be; }
         //                   else //i<bb
         //                   {
         //                       bbd = 24 - be + i;
         //                   }
         //                   double ddy = ady - minLag;
         //                   double tsn = (TMax - TMin) * Math.Sin(Math.PI * ddy / (ady + 2 * maxLag)) + TMin;
         //                   result[i - 1] = TMin + (tsn - TMin) * Math.Exp(-nightCoef * bbd / ani);
         //               }
         //           }
         //           return result;
         //       }
         //* /
         
				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}
