

 //Author:pierre martre pierre.martre@inra.fr
 //Institution:INRA
 //Author of revision: 
 //Date first release:4/12/2017
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
//To make this project compile please add the reference to assembly: System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089;

namespace SiriusQualityMeteo.Strategies
{

	/// <summary>
	///Class calculateHourlyVPDAir
    /// 
    /// </summary>
	public class calculateHourlyVPDAir : IStrategySiriusQualityMeteo
	{

	#region Constructor

			public calculateHourlyVPDAir()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd1.PropertyName = "hourlyAirTemperature";
				pd1.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.hourlyAirTemperature)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.hourlyAirTemperature);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd2.PropertyName = "maxTair";
				pd2.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.maxTair)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.maxTair);
				_inputs0_0.Add(pd2);
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd3.PropertyName = "minTair";
				pd3.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.minTair)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.minTair);
				_inputs0_0.Add(pd3);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd4.PropertyName = "hourlyVPDAir";
				pd4.PropertyType =  (( SiriusQualityMeteo.MeteoStateVarInfo.hourlyVPDAir)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =(  SiriusQualityMeteo.MeteoStateVarInfo.hourlyVPDAir);
				_outputs0_0.Add(pd4);
				PropertyDescription pd5 = new PropertyDescription();
				pd5.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd5.PropertyName = "RH";
				pd5.PropertyType =  (( SiriusQualityMeteo.MeteoStateVarInfo.RH)).ValueType.TypeForCurrentValue;
				pd5.PropertyVarInfo =(  SiriusQualityMeteo.MeteoStateVarInfo.RH);
				_outputs0_0.Add(pd5);
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
				get { return "VPD"; }
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
				_pd.Add("Creator", "pierre.martre@inra.fr");
				_pd.Add("Date", "4/12/2017");
				_pd.Add("Publisher", "INRA");
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
                
       
			}

			//Parameters static VarInfo list 
								
			
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
					
					SiriusQualityMeteo.MeteoStateVarInfo.hourlyVPDAir.CurrentValue=meteostate.hourlyVPDAir;
					SiriusQualityMeteo.MeteoStateVarInfo.RH.CurrentValue=meteostate.RH;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.hourlyVPDAir);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.hourlyVPDAir.ValueType)){prc.AddCondition(r4);}
					RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.RH);
					if(r5.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.RH.ValueType)){prc.AddCondition(r5);}

					

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
					
					SiriusQualityMeteo.MeteoStateVarInfo.hourlyAirTemperature.CurrentValue=meteostate.hourlyAirTemperature;
					SiriusQualityMeteo.MeteoStateVarInfo.maxTair.CurrentValue=meteostate.maxTair;
					SiriusQualityMeteo.MeteoStateVarInfo.minTair.CurrentValue=meteostate.minTair;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.hourlyAirTemperature);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.hourlyAirTemperature.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.maxTair);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.maxTair.ValueType)){prc.AddCondition(r2);}
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.minTair);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.minTair.ValueType)){prc.AddCondition(r3);}

					

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

                calcHourlyWeatherVars(meteostate);

                CalcVPDair(meteostate);
        

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation

            private void calcHourlyWeatherVars(SiriusQualityMeteo.MeteoState meteostate)
                {
                    CalcHourlySVP(meteostate.hourlyAirTemperature);

                    // hourly relative humidity
                    // if daily max and min RH is unavailable, use -1
                    double maxRH = -1;
                    double minRH = -1;
                    CalcRH(meteostate, maxRH, minRH);
                }

                private void CalcHourlySVP(double[] hourlytemperature)
                {
                    // calculates SVP at the air temperature
                    for (int i = 0; i < 24; i++)
                        SVP[i] =(6.108 * Math.Exp(17.27 * hourlytemperature[i] / (237.3 + hourlytemperature[i])));
                }

                private void CalcRH(SiriusQualityMeteo.MeteoState meteostate, double RHMax, double RHMin)
                {
                    // calculate relative humidity
                    double wP;
                    if (RHMax < 0.0 || RHMin < 0.0)
                        wP = CalcSVP(meteostate.minTair) * 0.90;         // svp at Tmin
                    else wP = (CalcSVP(meteostate.minTair) * (RHMax/100.0) + CalcSVP(meteostate.maxTair) / (RHMin/100.0) )/ 2.0;
                    for (int i = 0; i < 24; i++)
                    {
                        meteostate.RH[i] =100*(wP /SVP[i]);
                    }

                }
                private double CalcSVP(double TAir)
                {
                    // calculates SVP at the air temperature
                    return 6.108 * Math.Exp(17.27 * TAir / (237.3 + TAir));
                }

                private void CalcVPDair(SiriusQualityMeteo.MeteoState meteostate)
                {
                    for (int i = 0; i < 24; i++)
                        meteostate.hourlyVPDAir[i] = 6.108 * (Math.Exp((17.27 * meteostate.hourlyAirTemperature[i]) / (meteostate.hourlyAirTemperature[i] + 237.3)) - meteostate.RH[i] / 100
                        * Math.Exp((17.27 * meteostate.hourlyAirTemperature[i]) / (meteostate.hourlyAirTemperature[i] + 237.3)));
                }

                double[] SVP =new double[24];
				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}
