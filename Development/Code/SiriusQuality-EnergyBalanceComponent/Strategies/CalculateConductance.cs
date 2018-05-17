

 //Author:pierre martre pierre.martre@supagro.inra.fr
 //Institution:Inra
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


using SiriusQualityEnergyBalance;


//To make this project compile please add the reference to assembly: SiriusQuality-EnergyBalanceComponent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: CRA.ModelLayer, Version=1.0.5212.29139, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: CRA.AgroManagement2014, Version=0.8.0.0, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089;

namespace SiriusQualityEnergyBalance
{

	/// <summary>
	///Class CalculateConductance
    /// 
    /// </summary>
	public class CalculateConductance : IStrategySiriusQualityEnergyBalance
	{

	#region Constructor

			public CalculateConductance()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 0.42;
				 v1.Description = "von Karman constant";
				 v1.Id = 0;
				 v1.MaxValue = 1;
				 v1.MinValue = 0;
				 v1.Name = "vonKarman";
				 v1.Size = 1;
				 v1.Units = "?";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new VarInfo();
				 v2.DefaultValue = 2;
				 v2.Description = "reference height of wind and humidity measurements";
				 v2.Id = 0;
				 v2.MaxValue = 10;
				 v2.MinValue = 0;
				 v2.Name = "heightWeatherMeasurements";
				 v2.Size = 1;
				 v2.Units = "m";
				 v2.URL = "";
				 v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new VarInfo();
				 v3.DefaultValue = 0.13;
				 v3.Description = "roughness length governing momentum transfer, FAO";
				 v3.Id = 0;
				 v3.MaxValue = 1;
				 v3.MinValue = 0;
				 v3.Name = "zm";
				 v3.Size = 1;
				 v3.Units = "?";
				 v3.URL = "";
				 v3.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v3.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v3);
				VarInfo v4 = new VarInfo();
				 v4.DefaultValue = 0.013;
				 v4.Description = "roughness length governing transfer of heat and vapour, FAO";
				 v4.Id = 0;
				 v4.MaxValue = 1;
				 v4.MinValue = 0;
				 v4.Name = "zh";
				 v4.Size = 1;
				 v4.Units = "?";
				 v4.URL = "";
				 v4.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v4.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v4);
				VarInfo v5 = new VarInfo();
				 v5.DefaultValue = 0.67;
				 v5.Description = "corresponding to 2/3. This is multiplied to the crop heigth for calculating the zero plane displacement height, FAO";
				 v5.Id = 0;
				 v5.MaxValue = 1;
				 v5.MinValue = 0;
				 v5.Name = "d";
				 v5.Size = 1;
				 v5.Units = "?";
				 v5.URL = "";
				 v5.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v5.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v5);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityEnergyBalance.EnergyBalanceState);
				pd1.PropertyName = "plantHeight";
				pd1.PropertyType = (( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.plantHeight)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.plantHeight);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityEnergyBalance.EnergyBalanceState);
				pd2.PropertyName = "wind";
				pd2.PropertyType = (( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.wind)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.wind);
				_inputs0_0.Add(pd2);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityEnergyBalance.EnergyBalanceState);
				pd3.PropertyName = "conductance";
				pd3.PropertyType =  (( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.conductance)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =(  SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.conductance);
				_outputs0_0.Add(pd3);
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
				get {  return "EnergyBalance"; }
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
				_pd.Add("Creator", "pierre.martre@supagro.inra.fr");
				_pd.Add("Date", "");
				_pd.Add("Publisher", "Inra");
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
				return new List<Type>() {  typeof(SiriusQualityEnergyBalance.EnergyBalanceState) };
			}

	#endregion

    #region Instances of the parameters
			
			// Getter and setters for the value of the parameters of the strategy. The actual parameters are stored into the ModelingOptionsManager of the strategy.

			
			public Double vonKarman
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("vonKarman");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'vonKarman' not found (or found null) in strategy 'CalculateConductance'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("vonKarman");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'vonKarman' not found in strategy 'CalculateConductance'");
				}
			}
			public Double heightWeatherMeasurements
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("heightWeatherMeasurements");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'heightWeatherMeasurements' not found (or found null) in strategy 'CalculateConductance'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("heightWeatherMeasurements");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'heightWeatherMeasurements' not found in strategy 'CalculateConductance'");
				}
			}
			public Double zm
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("zm");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'zm' not found (or found null) in strategy 'CalculateConductance'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("zm");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'zm' not found in strategy 'CalculateConductance'");
				}
			}
			public Double zh
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("zh");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'zh' not found (or found null) in strategy 'CalculateConductance'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("zh");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'zh' not found in strategy 'CalculateConductance'");
				}
			}
			public Double d
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("d");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'd' not found (or found null) in strategy 'CalculateConductance'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("d");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'd' not found in strategy 'CalculateConductance'");
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
                vonKarmanVarInfo.Name = "vonKarman";
				vonKarmanVarInfo.Description =" von Karman constant";
				vonKarmanVarInfo.MaxValue = 1;
				vonKarmanVarInfo.MinValue = 0;
				vonKarmanVarInfo.DefaultValue = 0.42;
				vonKarmanVarInfo.Units = "?";
				vonKarmanVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				heightWeatherMeasurementsVarInfo.Name = "heightWeatherMeasurements";
				heightWeatherMeasurementsVarInfo.Description =" reference height of wind and humidity measurements";
				heightWeatherMeasurementsVarInfo.MaxValue = 10;
				heightWeatherMeasurementsVarInfo.MinValue = 0;
				heightWeatherMeasurementsVarInfo.DefaultValue = 2;
				heightWeatherMeasurementsVarInfo.Units = "m";
				heightWeatherMeasurementsVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				zmVarInfo.Name = "zm";
				zmVarInfo.Description =" roughness length governing momentum transfer, FAO";
				zmVarInfo.MaxValue = 1;
				zmVarInfo.MinValue = 0;
				zmVarInfo.DefaultValue = 0.13;
				zmVarInfo.Units = "?";
				zmVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				zhVarInfo.Name = "zh";
				zhVarInfo.Description =" roughness length governing transfer of heat and vapour, FAO";
				zhVarInfo.MaxValue = 1;
				zhVarInfo.MinValue = 0;
				zhVarInfo.DefaultValue = 0.013;
				zhVarInfo.Units = "?";
				zhVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				dVarInfo.Name = "d";
				dVarInfo.Description =" corresponding to 2/3. This is multiplied to the crop heigth for calculating the zero plane displacement height, FAO";
				dVarInfo.MaxValue = 1;
				dVarInfo.MinValue = 0;
				dVarInfo.DefaultValue = 0.67;
				dVarInfo.Units = "?";
				dVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _vonKarmanVarInfo= new VarInfo();
				/// <summary> 
				///vonKarman VarInfo definition
				/// </summary>
				public static VarInfo vonKarmanVarInfo
				{
					get { return _vonKarmanVarInfo; }
				}
				private static VarInfo _heightWeatherMeasurementsVarInfo= new VarInfo();
				/// <summary> 
				///heightWeatherMeasurements VarInfo definition
				/// </summary>
				public static VarInfo heightWeatherMeasurementsVarInfo
				{
					get { return _heightWeatherMeasurementsVarInfo; }
				}
				private static VarInfo _zmVarInfo= new VarInfo();
				/// <summary> 
				///zm VarInfo definition
				/// </summary>
				public static VarInfo zmVarInfo
				{
					get { return _zmVarInfo; }
				}
				private static VarInfo _zhVarInfo= new VarInfo();
				/// <summary> 
				///zh VarInfo definition
				/// </summary>
				public static VarInfo zhVarInfo
				{
					get { return _zhVarInfo; }
				}
				private static VarInfo _dVarInfo= new VarInfo();
				/// <summary> 
				///d VarInfo definition
				/// </summary>
				public static VarInfo dVarInfo
				{
					get { return _dVarInfo; }
				}					
			
			//Parameters static VarInfo list of the composite class
						

	#endregion
	
	#region pre/post conditions management		

		    /// <summary>
			/// Test to verify the postconditions
			/// </summary>
			public string TestPostConditions(SiriusQualityEnergyBalance.EnergyBalanceState energybalancestate, string callID)
			{
				try
				{
					//Set current values of the outputs to the static VarInfo representing the output properties of the domain classes				
					
					SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.conductance.CurrentValue=energybalancestate.conductance;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.conductance);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.conductance.ValueType)){prc.AddCondition(r3);}

					

					//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section4
					//Code written below will not be overwritten by a future code generation

        

					//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
					//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section4 

					//Get the evaluation of postconditions
					string postConditionsResult =pre.VerifyPostconditions(prc, callID);
					//if we have errors, send it to the configured output 
					if(!string.IsNullOrEmpty(postConditionsResult)) { pre.TestsOut(postConditionsResult, true, "PostConditions errors in component SiriusQualityEnergyBalance, strategy " + this.GetType().Name ); }
					return postConditionsResult;
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1001,	"Strategy: " + this.GetType().Name + " - Unhandled exception running post-conditions");

					string msg = "Component SiriusQualityEnergyBalance, " + this.GetType().Name + ": Unhandled exception running post-condition test. ";
					throw new Exception(msg, exception);
				}
			}

			/// <summary>
			/// Test to verify the preconditions
			/// </summary>
			public string TestPreConditions(SiriusQualityEnergyBalance.EnergyBalanceState energybalancestate, string callID)
			{
				try
				{
					//Set current values of the inputs to the static VarInfo representing the input properties of the domain classes				
					
					SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.plantHeight.CurrentValue=energybalancestate.plantHeight;
					SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.wind.CurrentValue=energybalancestate.wind;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.plantHeight);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.plantHeight.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.wind);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.wind.ValueType)){prc.AddCondition(r2);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("vonKarman")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("heightWeatherMeasurements")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("zm")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("zh")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("d")));

					

					//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section3
					//Code written below will not be overwritten by a future code generation

        

					//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
					//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section3 
								
					//Get the evaluation of preconditions;					
					string preConditionsResult =pre.VerifyPreconditions(prc, callID);
					//if we have errors, send it to the configured output 
					if(!string.IsNullOrEmpty(preConditionsResult)) { pre.TestsOut(preConditionsResult, true, "PreConditions errors in component SiriusQualityEnergyBalance, strategy " + this.GetType().Name ); }
					return preConditionsResult;
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//	TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1002,"Strategy: " + this.GetType().Name + " - Unhandled exception running pre-conditions");

					string msg = "Component SiriusQualityEnergyBalance, " + this.GetType().Name + ": Unhandled exception running pre-condition test. ";
					throw new Exception(msg, exception);
				}
			}

		
	#endregion
		


	#region Model

		 	/// <summary>
			/// Run the strategy to calculate the outputs. In case of error during the execution, the preconditions tests are executed.
			/// </summary>
			public void Estimate(SiriusQualityEnergyBalance.EnergyBalanceState energybalancestate)
			{
				try
				{
					CalculateModel(energybalancestate);

					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Verbose, 1005,"Strategy: " + this.GetType().Name + " - Model executed");
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1003,		"Strategy: " + this.GetType().Name + " - Unhandled exception running model");

					string msg = "Error in component SiriusQualityEnergyBalance, strategy: " + this.GetType().Name + ": Unhandled exception running model. "+exception.GetType().FullName+" - "+exception.Message;				
					throw new Exception(msg, exception);
				}
			}

			private void CalculateModel(SiriusQualityEnergyBalance.EnergyBalanceState energybalancestate)
			{				
				

				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section1
				//Code written below will not be overwritten by a future code generation
                double h = Math.Max(10, energybalancestate.plantHeight) / 100;   // crop height (m) - assume a plant height of 10 cm before stem extension, / 100 to convert cm to m

                energybalancestate.conductance = (energybalancestate.wind * Math.Pow(vonKarman, 2)) / (Math.Log((heightWeatherMeasurements - d * h) / (zm * h)) * Math.Log((heightWeatherMeasurements - d * h) / (zh * h)));

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
