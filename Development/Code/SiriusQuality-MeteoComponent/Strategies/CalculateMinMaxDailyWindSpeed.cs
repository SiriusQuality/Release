

 //Author:Loic Manceau loic.manceau@inra.fr
 //Institution:INRA
 //Author of revision: 
 //Date first release:
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
	///Class CalculateMinMaxDailyWindSpeed
    /// Calculate Maximum and Minimum daily wind speed from mean daily wind speed following Ephrath, J.E., J. Goudriaan, and A. Marani. 1996. Modelling diurnal patterns of air temperatures, radiation, wind speed and relative humidity by equations for daily characteristics. Agr. Sys., 51:377-393.
    /// </summary>
	public class CalculateMinMaxDailyWindSpeed : IStrategySiriusQualityMeteo
	{

	#region Constructor

			public CalculateMinMaxDailyWindSpeed()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 1;
				 v1.Description = "Shift in hour from sunrise time when wind begins to blow";
				 v1.Id = 0;
				 v1.MaxValue = 24;
				 v1.MinValue = 1;
				 v1.Name = "hourOfBlowingBeginingT1";
				 v1.Size = 1;
				 v1.Units = "hour of the day";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new VarInfo();
				 v2.DefaultValue = 2;
				 v2.Description = "Shift in hour from sunset when wind stop blowing";
				 v2.Id = 0;
				 v2.MaxValue = 24;
				 v2.MinValue = 1;
				 v2.Name = "hourOfBlowingStopT3";
				 v2.Size = 1;
				 v2.Units = "hour of the day";
				 v2.URL = "";
				 v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new VarInfo();
				 v3.DefaultValue = 0.0025;
				 v3.Description = "Fraction of daily mean wind speed for the night time (minimum) wind speed calculation";
				 v3.Id = 0;
				 v3.MaxValue = 10;
				 v3.MinValue = 0;
				 v3.Name = "nightTimeWindFactor";
				 v3.Size = 1;
				 v3.Units = "dimensionless";
				 v3.URL = "";
				 v3.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v3.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v3);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd1.PropertyName = "dailyMeanWindSpeed";
				pd1.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.dailyMeanWindSpeed)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.dailyMeanWindSpeed);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd2.PropertyName = "hourlySolarElevation";
				pd2.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.hourlySolarElevation)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.hourlySolarElevation);
				_inputs0_0.Add(pd2);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd3.PropertyName = "dailyMaxWindSpeed";
				pd3.PropertyType =  (( SiriusQualityMeteo.MeteoStateVarInfo.dailyMaxWindSpeed)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =(  SiriusQualityMeteo.MeteoStateVarInfo.dailyMaxWindSpeed);
				_outputs0_0.Add(pd3);
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd4.PropertyName = "dailyMinWindSpeed";
				pd4.PropertyType =  (( SiriusQualityMeteo.MeteoStateVarInfo.dailyMinWindSpeed)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =(  SiriusQualityMeteo.MeteoStateVarInfo.dailyMinWindSpeed);
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
				get { return "Calculate Maximum and Minimum daily wind speed from mean daily wind speed following Ephrath, J.E., J. Goudriaan, and A. Marani. 1996. Modelling diurnal patterns of air temperatures, radiation, wind speed and relative humidity by equations for daily characteristics. Agr. Sys., 51:377-393."; }
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
				_pd.Add("Creator", "loic.manceau@inra.fr");
				_pd.Add("Date", "");
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

			
			public Double hourOfBlowingBeginingT1
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("hourOfBlowingBeginingT1");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'hourOfBlowingBeginingT1' not found (or found null) in strategy 'CalculateMinMaxDailyWindSpeed'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("hourOfBlowingBeginingT1");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'hourOfBlowingBeginingT1' not found in strategy 'CalculateMinMaxDailyWindSpeed'");
				}
			}
			public Double hourOfBlowingStopT3
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("hourOfBlowingStopT3");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'hourOfBlowingStopT3' not found (or found null) in strategy 'CalculateMinMaxDailyWindSpeed'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("hourOfBlowingStopT3");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'hourOfBlowingStopT3' not found in strategy 'CalculateMinMaxDailyWindSpeed'");
				}
			}
			public Double nightTimeWindFactor
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("nightTimeWindFactor");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'nightTimeWindFactor' not found (or found null) in strategy 'CalculateMinMaxDailyWindSpeed'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("nightTimeWindFactor");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'nightTimeWindFactor' not found in strategy 'CalculateMinMaxDailyWindSpeed'");
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
                hourOfBlowingBeginingT1VarInfo.Name = "hourOfBlowingBeginingT1";
				hourOfBlowingBeginingT1VarInfo.Description =" Shift in hour from sunrise time when wind begins to blow";
				hourOfBlowingBeginingT1VarInfo.MaxValue = 24;
				hourOfBlowingBeginingT1VarInfo.MinValue = 1;
				hourOfBlowingBeginingT1VarInfo.DefaultValue = 1;
				hourOfBlowingBeginingT1VarInfo.Units = "hour of the day";
				hourOfBlowingBeginingT1VarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				hourOfBlowingStopT3VarInfo.Name = "hourOfBlowingStopT3";
				hourOfBlowingStopT3VarInfo.Description =" Shift in hour from sunset when wind stop blowing";
				hourOfBlowingStopT3VarInfo.MaxValue = 24;
				hourOfBlowingStopT3VarInfo.MinValue = 1;
				hourOfBlowingStopT3VarInfo.DefaultValue = 2;
				hourOfBlowingStopT3VarInfo.Units = "hour of the day";
				hourOfBlowingStopT3VarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				nightTimeWindFactorVarInfo.Name = "nightTimeWindFactor";
				nightTimeWindFactorVarInfo.Description =" Fraction of daily mean wind speed for the night time (minimum) wind speed calculation";
				nightTimeWindFactorVarInfo.MaxValue = 10;
				nightTimeWindFactorVarInfo.MinValue = 0;
				nightTimeWindFactorVarInfo.DefaultValue = 0.0025;
				nightTimeWindFactorVarInfo.Units = "dimensionless";
				nightTimeWindFactorVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _hourOfBlowingBeginingT1VarInfo= new VarInfo();
				/// <summary> 
				///hourOfBlowingBeginingT1 VarInfo definition
				/// </summary>
				public static VarInfo hourOfBlowingBeginingT1VarInfo
				{
					get { return _hourOfBlowingBeginingT1VarInfo; }
				}
				private static VarInfo _hourOfBlowingStopT3VarInfo= new VarInfo();
				/// <summary> 
				///hourOfBlowingStopT3 VarInfo definition
				/// </summary>
				public static VarInfo hourOfBlowingStopT3VarInfo
				{
					get { return _hourOfBlowingStopT3VarInfo; }
				}
				private static VarInfo _nightTimeWindFactorVarInfo= new VarInfo();
				/// <summary> 
				///nightTimeWindFactor VarInfo definition
				/// </summary>
				public static VarInfo nightTimeWindFactorVarInfo
				{
					get { return _nightTimeWindFactorVarInfo; }
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
					
					SiriusQualityMeteo.MeteoStateVarInfo.dailyMaxWindSpeed.CurrentValue=meteostate.dailyMaxWindSpeed;
					SiriusQualityMeteo.MeteoStateVarInfo.dailyMinWindSpeed.CurrentValue=meteostate.dailyMinWindSpeed;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.dailyMaxWindSpeed);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.dailyMaxWindSpeed.ValueType)){prc.AddCondition(r3);}
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.dailyMinWindSpeed);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.dailyMinWindSpeed.ValueType)){prc.AddCondition(r4);}

					

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
					
					SiriusQualityMeteo.MeteoStateVarInfo.dailyMeanWindSpeed.CurrentValue=meteostate.dailyMeanWindSpeed;
					SiriusQualityMeteo.MeteoStateVarInfo.hourlySolarElevation.CurrentValue=meteostate.hourlySolarElevation;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.dailyMeanWindSpeed);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.dailyMeanWindSpeed.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.hourlySolarElevation);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.hourlySolarElevation.ValueType)){prc.AddCondition(r2);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("hourOfBlowingBeginingT1")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("hourOfBlowingStopT3")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("nightTimeWindFactor")));

					

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
                //Ephrath, J.E., J. Goudriaan, and A. Marani. 1996. Modelling diurnal patterns of air temperatures, radiation, wind speed and relative humidity by equations for daily characteristics. Agr. Sys., 51:377-393.
        
                //Calculate hour of sunrise
                int hsunrise = 0;
                for (int ihour = 0; ihour < 24; ihour++) if (meteostate.hourlySolarElevation[ihour] > 0.0) { hsunrise = ihour+1;  break; }

                //Calculate hour of sunrise
                int hsunset = 0;
                for (int ihour = 11; ihour < 24; ihour++) if (meteostate.hourlySolarElevation[ihour] == 0.0) { hsunset = ihour+1; break; }

                double T1 = hsunrise + hourOfBlowingBeginingT1;
                double T3 = hsunset + hourOfBlowingStopT3;

                //Calculate daily minimum wind speed
                meteostate.dailyMinWindSpeed = (meteostate.dailyMeanWindSpeed /(24*3600))* nightTimeWindFactor; //m s-1

                //Calculate daily maximum wind temperature
                double SFsum = 4 * (T3 - T1);

                meteostate.dailyMaxWindSpeed = 24 * 2 * Math.PI * (((meteostate.dailyMeanWindSpeed / (24 * 3600)) - meteostate.dailyMinWindSpeed) / SFsum); //m s-1

                //Coversion from m s-1 to m d-1

                meteostate.dailyMinWindSpeed = meteostate.dailyMinWindSpeed * (24 * 3600);
                meteostate.dailyMaxWindSpeed = meteostate.dailyMaxWindSpeed * (24 * 3600);


				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}
