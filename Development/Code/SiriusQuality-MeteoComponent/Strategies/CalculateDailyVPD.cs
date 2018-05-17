

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
//To make this project compile please add the reference to assembly: System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089;

namespace SiriusQualityMeteo.Strategies
{

	/// <summary>
	///Class CalculateDailyVPD
    /// 
    /// </summary>
	public class CalculateDailyVPD : IStrategySiriusQualityMeteo
	{

	#region Constructor

			public CalculateDailyVPD()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd1.PropertyName = "vaporPressure";
				pd1.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.vaporPressure)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.vaporPressure);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd2.PropertyName = "minTair";
				pd2.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.minTair)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.minTair);
				_inputs0_0.Add(pd2);
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd3.PropertyName = "maxTair";
				pd3.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.maxTair)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.maxTair);
				_inputs0_0.Add(pd3);
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd4.PropertyName = "meanTair";
				pd4.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.meanTair)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.meanTair);
				_inputs0_0.Add(pd4);
				PropertyDescription pd5 = new PropertyDescription();
				pd5.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd5.PropertyName = "maxShootTemperature";
				pd5.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.maxShootTemperature)).ValueType.TypeForCurrentValue;
				pd5.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.maxShootTemperature);
				_inputs0_0.Add(pd5);
				PropertyDescription pd6 = new PropertyDescription();
				pd6.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd6.PropertyName = "minShootTemperature";
				pd6.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.minShootTemperature)).ValueType.TypeForCurrentValue;
				pd6.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.minShootTemperature);
				_inputs0_0.Add(pd6);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd7 = new PropertyDescription();
				pd7.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd7.PropertyName = "HSlope";
				pd7.PropertyType =  (( SiriusQualityMeteo.MeteoStateVarInfo.HSlope)).ValueType.TypeForCurrentValue;
				pd7.PropertyVarInfo =(  SiriusQualityMeteo.MeteoStateVarInfo.HSlope);
				_outputs0_0.Add(pd7);
				PropertyDescription pd8 = new PropertyDescription();
				pd8.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd8.PropertyName = "saturationVaporPressure";
				pd8.PropertyType =  (( SiriusQualityMeteo.MeteoStateVarInfo.saturationVaporPressure)).ValueType.TypeForCurrentValue;
				pd8.PropertyVarInfo =(  SiriusQualityMeteo.MeteoStateVarInfo.saturationVaporPressure);
				_outputs0_0.Add(pd8);
				PropertyDescription pd9 = new PropertyDescription();
				pd9.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd9.PropertyName = "VPDair";
				pd9.PropertyType =  (( SiriusQualityMeteo.MeteoStateVarInfo.VPDair)).ValueType.TypeForCurrentValue;
				pd9.PropertyVarInfo =(  SiriusQualityMeteo.MeteoStateVarInfo.VPDair);
				_outputs0_0.Add(pd9);
				PropertyDescription pd10 = new PropertyDescription();
				pd10.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd10.PropertyName = "VPDairCanopy";
				pd10.PropertyType =  (( SiriusQualityMeteo.MeteoStateVarInfo.VPDairCanopy)).ValueType.TypeForCurrentValue;
				pd10.PropertyVarInfo =(  SiriusQualityMeteo.MeteoStateVarInfo.VPDairCanopy);
				_outputs0_0.Add(pd10);
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
					
					SiriusQualityMeteo.MeteoStateVarInfo.HSlope.CurrentValue=meteostate.HSlope;
					SiriusQualityMeteo.MeteoStateVarInfo.saturationVaporPressure.CurrentValue=meteostate.saturationVaporPressure;
					SiriusQualityMeteo.MeteoStateVarInfo.VPDair.CurrentValue=meteostate.VPDair;
					SiriusQualityMeteo.MeteoStateVarInfo.VPDairCanopy.CurrentValue=meteostate.VPDairCanopy;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r7 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.HSlope);
					if(r7.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.HSlope.ValueType)){prc.AddCondition(r7);}
					RangeBasedCondition r8 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.saturationVaporPressure);
					if(r8.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.saturationVaporPressure.ValueType)){prc.AddCondition(r8);}
					RangeBasedCondition r9 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.VPDair);
					if(r9.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.VPDair.ValueType)){prc.AddCondition(r9);}
					RangeBasedCondition r10 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.VPDairCanopy);
					if(r10.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.VPDairCanopy.ValueType)){prc.AddCondition(r10);}

					

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
					
					SiriusQualityMeteo.MeteoStateVarInfo.vaporPressure.CurrentValue=meteostate.vaporPressure;
					SiriusQualityMeteo.MeteoStateVarInfo.minTair.CurrentValue=meteostate.minTair;
					SiriusQualityMeteo.MeteoStateVarInfo.maxTair.CurrentValue=meteostate.maxTair;
					SiriusQualityMeteo.MeteoStateVarInfo.meanTair.CurrentValue=meteostate.meanTair;
					SiriusQualityMeteo.MeteoStateVarInfo.maxShootTemperature.CurrentValue=meteostate.maxShootTemperature;
					SiriusQualityMeteo.MeteoStateVarInfo.minShootTemperature.CurrentValue=meteostate.minShootTemperature;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.vaporPressure);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.vaporPressure.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.minTair);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.minTair.ValueType)){prc.AddCondition(r2);}
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.maxTair);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.maxTair.ValueType)){prc.AddCondition(r3);}
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.meanTair);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.meanTair.ValueType)){prc.AddCondition(r4);}
					RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.maxShootTemperature);
					if(r5.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.maxShootTemperature.ValueType)){prc.AddCondition(r5);}
					RangeBasedCondition r6 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.minShootTemperature);
					if(r6.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.minShootTemperature.ValueType)){prc.AddCondition(r6);}

					

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
                EstimateHSlope(meteostate);
                CalculateVPD(meteostate);
        

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation
                ///<summary>
                ///Slope of saturated vapour pressure temperature curve at temperature T
                ///OUTPUT UNITS: hPa �C-1
                ///</summary>
                ///<param name="meanTemperature"></param>
                ///<param name="temperatureWaterTriplePoint"></param>
                ///<returns></returns>
                public void EstimateHSlope(SiriusQualityMeteo.MeteoState meteostate)
                {
                    meteostate.HSlope = 4098 * CalculateSaturationVaporPressure(meteostate.minTair, meteostate.maxTair) / (Math.Pow(meteostate.meanTair + 237.3, 2));
                }

                ///<summary>
                ///Calculate air (VPDair) and air-to-canopy (VPDairCanopy) vapour pressure deficit
                ///OUTPUT UNITS: hPa
                ///</summary>
                ///<param name="meanAirTemperature"></param>
                ///<param name="meanCanopyTemperature"></param>
                ///<param name="vp"></param>
                ///<returns></returns>
                public void CalculateVPD(SiriusQualityMeteo.MeteoState meteostate)
                {
                    meteostate.VPDair = Math.Max(0.0, CalculateSaturationVaporPressure(meteostate.minTair, meteostate.maxTair) - meteostate.vaporPressure); // returns hPa

                    meteostate.VPDairCanopy = Math.Max(0.0, CalculateSaturationVaporPressure(meteostate.minShootTemperature, meteostate.maxShootTemperature) - meteostate.vaporPressure); // returns hPa
                }

                ///<summary>
                ///Daily mean saturatation vapour pressure of water vapour
                ///OUTPUT UNITS: hPa
                ///</summary>
                ///<param name="temperature"></param>
                ///<returns></returns>
                private double CalculateSaturationVaporPressure(double Tmin, double Tmax)
                {
                    double es_min = 6.108 * Math.Exp(17.27 * Tmin / (Tmin + 237.3));
                    double es_max = 6.108 * Math.Exp(17.27 * Tmax / (Tmax + 237.3));

                    return (es_max + es_min) / 2; // returns hPa
                }
				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}
