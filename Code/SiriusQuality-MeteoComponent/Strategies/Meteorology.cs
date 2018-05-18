

 //Author: 
 //Institution:
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
//To make this project compile please add the reference to assembly: System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089;

namespace SiriusQualityMeteo.Strategies
{

	/// <summary>
	///Class Meteorology
    /// 
    /// </summary>
	public class Meteorology : IStrategySiriusQualityMeteo
	{

	#region Constructor

			public Meteorology()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new CompositeStrategyVarInfo(_calculatehourlyairtemperature,"latitude");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new CompositeStrategyVarInfo(_calculatehourlyradiation,"latitude");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new CompositeStrategyVarInfo(_calculateminmaxdailywindspeed,"hourOfBlowingBeginingT1");
				 _parameters0_0.Add(v3);
				VarInfo v4 = new CompositeStrategyVarInfo(_calculateminmaxdailywindspeed,"hourOfBlowingStopT3");
				 _parameters0_0.Add(v4);
				VarInfo v5 = new CompositeStrategyVarInfo(_calculateminmaxdailywindspeed,"nightTimeWindFactor");
				 _parameters0_0.Add(v5);
				VarInfo v6 = new CompositeStrategyVarInfo(_calculatephotoperiod,"latitude");
				 _parameters0_0.Add(v6);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				mo0_0.Outputs=_outputs0_0;
				//Associated strategies
				List<string> lAssStrat0_0 = new List<string>();
				lAssStrat0_0.Add(typeof(SiriusQualityMeteo.Strategies.CalculateDailyVPD).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityMeteo.Strategies.CalculateHourlyAirTemperature).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityMeteo.Strategies.CalculateHourlyDirDifRadiations).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityMeteo.Strategies.CalculateHourlyRadiation).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityMeteo.Strategies.calculateHourlyVPDAir).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityMeteo.Strategies.CalculateHourlyWindSpeed).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityMeteo.Strategies.CalculateMinMaxDailyWindSpeed).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityMeteo.Strategies.CalculatePhotoperiod).FullName);
				mo0_0.AssociatedStrategies = lAssStrat0_0;
				//Adding the modeling options to the modeling options manager

				//Creating the modeling options manager of the strategy
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
				_pd.Add("Date", "");
				_pd.Add("Publisher", "");
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
			
			public Double latitude
			{ 
				get {
						return _calculatehourlyairtemperature.latitude ;
				}
				set {
						_calculatehourlyairtemperature.latitude=value;
						_calculatehourlyradiation.latitude=value;
						_calculatephotoperiod.latitude=value;
				}
			}
			public Double hourOfBlowingBeginingT1
			{ 
				get {
						return _calculateminmaxdailywindspeed.hourOfBlowingBeginingT1 ;
				}
				set {
						_calculateminmaxdailywindspeed.hourOfBlowingBeginingT1=value;
				}
			}
			public Double hourOfBlowingStopT3
			{ 
				get {
						return _calculateminmaxdailywindspeed.hourOfBlowingStopT3 ;
				}
				set {
						_calculateminmaxdailywindspeed.hourOfBlowingStopT3=value;
				}
			}
			public Double nightTimeWindFactor
			{ 
				get {
						return _calculateminmaxdailywindspeed.nightTimeWindFactor ;
				}
				set {
						_calculateminmaxdailywindspeed.nightTimeWindFactor=value;
				}
			}

	#endregion		

	
	#region Parameters initialization method
			
            /// <summary>
            /// Set parameter(s) current values to the default value
            /// </summary>
            public void SetParametersDefaultValue()
            {
				_modellingOptionsManager.SetParametersDefaultValue();
				
					_calculatedailyvpd.SetParametersDefaultValue();
					_calculatehourlyairtemperature.SetParametersDefaultValue();
					_calculatehourlydirdifradiations.SetParametersDefaultValue();
					_calculatehourlyradiation.SetParametersDefaultValue();
					_calculatehourlyvpdair.SetParametersDefaultValue();
					_calculatehourlywindspeed.SetParametersDefaultValue();
					_calculateminmaxdailywindspeed.SetParametersDefaultValue();
					_calculatephotoperiod.SetParametersDefaultValue(); 

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
			
				/// <summary> 
				///latitude VarInfo definition
				/// </summary>
				public static VarInfo latitudeVarInfo
				{
					get { return SiriusQualityMeteo.Strategies.CalculateHourlyAirTemperature.latitudeVarInfo; }
				}
				/// <summary> 
				///hourOfBlowingBeginingT1 VarInfo definition
				/// </summary>
				public static VarInfo hourOfBlowingBeginingT1VarInfo
				{
					get { return SiriusQualityMeteo.Strategies.CalculateMinMaxDailyWindSpeed.hourOfBlowingBeginingT1VarInfo; }
				}
				/// <summary> 
				///hourOfBlowingStopT3 VarInfo definition
				/// </summary>
				public static VarInfo hourOfBlowingStopT3VarInfo
				{
					get { return SiriusQualityMeteo.Strategies.CalculateMinMaxDailyWindSpeed.hourOfBlowingStopT3VarInfo; }
				}
				/// <summary> 
				///nightTimeWindFactor VarInfo definition
				/// </summary>
				public static VarInfo nightTimeWindFactorVarInfo
				{
					get { return SiriusQualityMeteo.Strategies.CalculateMinMaxDailyWindSpeed.nightTimeWindFactorVarInfo; }
				}			

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
					
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					

					
					string ret = "";
					 ret += _calculatedailyvpd.TestPostConditions(meteostate, "strategy SiriusQualityMeteo.Strategies.CalculateDailyVPD");
					 ret += _calculatehourlyairtemperature.TestPostConditions(meteostate, "strategy SiriusQualityMeteo.Strategies.CalculateHourlyAirTemperature");
					 ret += _calculatehourlydirdifradiations.TestPostConditions(meteostate, "strategy SiriusQualityMeteo.Strategies.CalculateHourlyDirDifRadiations");
					 ret += _calculatehourlyradiation.TestPostConditions(meteostate, "strategy SiriusQualityMeteo.Strategies.CalculateHourlyRadiation");
					 ret += _calculatehourlyvpdair.TestPostConditions(meteostate, "strategy SiriusQualityMeteo.Strategies.calculateHourlyVPDAir");
					 ret += _calculatehourlywindspeed.TestPostConditions(meteostate, "strategy SiriusQualityMeteo.Strategies.CalculateHourlyWindSpeed");
					 ret += _calculateminmaxdailywindspeed.TestPostConditions(meteostate, "strategy SiriusQualityMeteo.Strategies.CalculateMinMaxDailyWindSpeed");
					 ret += _calculatephotoperiod.TestPostConditions(meteostate, "strategy SiriusQualityMeteo.Strategies.CalculatePhotoperiod");
					if (ret != "") { pre.TestsOut(ret, true, "   postconditions tests of associated classes"); }

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
					

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					

					
					string ret = "";
					 ret += _calculatedailyvpd.TestPreConditions(meteostate, "strategy SiriusQualityMeteo.Strategies.CalculateDailyVPD");
					 ret += _calculatehourlyairtemperature.TestPreConditions(meteostate, "strategy SiriusQualityMeteo.Strategies.CalculateHourlyAirTemperature");
					 ret += _calculatehourlydirdifradiations.TestPreConditions(meteostate, "strategy SiriusQualityMeteo.Strategies.CalculateHourlyDirDifRadiations");
					 ret += _calculatehourlyradiation.TestPreConditions(meteostate, "strategy SiriusQualityMeteo.Strategies.CalculateHourlyRadiation");
					 ret += _calculatehourlyvpdair.TestPreConditions(meteostate, "strategy SiriusQualityMeteo.Strategies.calculateHourlyVPDAir");
					 ret += _calculatehourlywindspeed.TestPreConditions(meteostate, "strategy SiriusQualityMeteo.Strategies.CalculateHourlyWindSpeed");
					 ret += _calculateminmaxdailywindspeed.TestPreConditions(meteostate, "strategy SiriusQualityMeteo.Strategies.CalculateMinMaxDailyWindSpeed");
					 ret += _calculatephotoperiod.TestPreConditions(meteostate, "strategy SiriusQualityMeteo.Strategies.CalculatePhotoperiod");
					if (ret != "") { pre.TestsOut(ret, true, "   preconditions tests of associated classes"); }

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
				
					EstimateOfAssociatedClasses(meteostate);

				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section1
				//Code written below will not be overwritten by a future code generation

        

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				
			#region Composite class: associations

			//Declaration of the associated strategies
			SiriusQualityMeteo.Strategies.CalculateDailyVPD _calculatedailyvpd = new SiriusQualityMeteo.Strategies.CalculateDailyVPD();
			SiriusQualityMeteo.Strategies.CalculateHourlyAirTemperature _calculatehourlyairtemperature = new SiriusQualityMeteo.Strategies.CalculateHourlyAirTemperature();
			SiriusQualityMeteo.Strategies.CalculateHourlyDirDifRadiations _calculatehourlydirdifradiations = new SiriusQualityMeteo.Strategies.CalculateHourlyDirDifRadiations();
			SiriusQualityMeteo.Strategies.CalculateHourlyRadiation _calculatehourlyradiation = new SiriusQualityMeteo.Strategies.CalculateHourlyRadiation();
			SiriusQualityMeteo.Strategies.calculateHourlyVPDAir _calculatehourlyvpdair = new SiriusQualityMeteo.Strategies.calculateHourlyVPDAir();
			SiriusQualityMeteo.Strategies.CalculateHourlyWindSpeed _calculatehourlywindspeed = new SiriusQualityMeteo.Strategies.CalculateHourlyWindSpeed();
			SiriusQualityMeteo.Strategies.CalculateMinMaxDailyWindSpeed _calculateminmaxdailywindspeed = new SiriusQualityMeteo.Strategies.CalculateMinMaxDailyWindSpeed();
			SiriusQualityMeteo.Strategies.CalculatePhotoperiod _calculatephotoperiod = new SiriusQualityMeteo.Strategies.CalculatePhotoperiod();

			//Call of the associated strategies
			private void EstimateOfAssociatedClasses(SiriusQualityMeteo.MeteoState meteostate){
				_calculatedailyvpd.Estimate(meteostate);
				_calculateminmaxdailywindspeed.Estimate(meteostate);
				_calculatephotoperiod.Estimate(meteostate);


                  if (meteostate.calculateHourly == 1)
                   {
                   _calculatehourlyairtemperature.Estimate(meteostate);
                   _calculatehourlyradiation.Estimate(meteostate);
                   _calculatehourlydirdifradiations.Estimate(meteostate);
                   _calculatehourlyvpdair.Estimate(meteostate);
                   _calculatehourlywindspeed.Estimate(meteostate);
                   }
            }

			#endregion


	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation
            public Meteorology(Meteorology toCopy)
                : this()
            {
                //we only need to copy the parameters (the strategies being stateless)
                latitude = toCopy.latitude;
                hourOfBlowingBeginingT1 = toCopy.hourOfBlowingBeginingT1;
                hourOfBlowingStopT3 = toCopy.hourOfBlowingStopT3;
                nightTimeWindFactor = toCopy.nightTimeWindFactor;

            }
				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}
