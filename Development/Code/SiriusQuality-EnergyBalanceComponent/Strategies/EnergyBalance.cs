

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
	///Class EnergyBalance
    /// 
    /// </summary>
	public class EnergyBalance : IStrategySiriusQualityEnergyBalance
	{

	#region Constructor

			public EnergyBalance()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 0;
				 v1.Description = "true if maize false if wheat";
				 v1.Id = 0;
				 v1.MaxValue = 1;
				 v1.MinValue = 0;
				 v1.Name = "SwitchMaize";
				 v1.Size = 1;
				 v1.Units = "NA";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new CompositeStrategyVarInfo(_calculatenetradiation,"albedoCoefficient");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new CompositeStrategyVarInfo(_calculatenetradiation,"stefanBoltzman");
				 _parameters0_0.Add(v3);
				VarInfo v4 = new CompositeStrategyVarInfo(_calculatenetradiation,"elevation");
				 _parameters0_0.Add(v4);
				VarInfo v5 = new CompositeStrategyVarInfo(_calculatediffusionlimitedevaporation,"soilDiffusionConstant");
				 _parameters0_0.Add(v5);
				VarInfo v6 = new CompositeStrategyVarInfo(_calculatepriestlytaylor,"lambda");
				 _parameters0_0.Add(v6);
				VarInfo v7 = new CompositeStrategyVarInfo(_calculatepriestlytaylor,"psychrometricConstant");
				 _parameters0_0.Add(v7);
				VarInfo v8 = new CompositeStrategyVarInfo(_calculatepriestlytaylor,"Alpha");
				 _parameters0_0.Add(v8);
				VarInfo v9 = new CompositeStrategyVarInfo(_calculateptsoil,"Alpha");
				 _parameters0_0.Add(v9);
				VarInfo v10 = new CompositeStrategyVarInfo(_calculateptsoil,"tauAlpha");
				 _parameters0_0.Add(v10);
				VarInfo v11 = new CompositeStrategyVarInfo(_calculateconductance,"vonKarman");
				 _parameters0_0.Add(v11);
				VarInfo v12 = new CompositeStrategyVarInfo(_calculateconductance,"heightWeatherMeasurements");
				 _parameters0_0.Add(v12);
				VarInfo v13 = new CompositeStrategyVarInfo(_calculateconductance,"zm");
				 _parameters0_0.Add(v13);
				VarInfo v14 = new CompositeStrategyVarInfo(_calculateconductance,"zh");
				 _parameters0_0.Add(v14);
				VarInfo v15 = new CompositeStrategyVarInfo(_calculateconductance,"d");
				 _parameters0_0.Add(v15);
				VarInfo v16 = new CompositeStrategyVarInfo(_calculatepenman,"psychrometricConstant");
				 _parameters0_0.Add(v16);
				VarInfo v17 = new CompositeStrategyVarInfo(_calculatepenman,"rhoDensityAir");
				 _parameters0_0.Add(v17);
				VarInfo v18 = new CompositeStrategyVarInfo(_calculatepenman,"specificHeatCapacityAir");
				 _parameters0_0.Add(v18);
				VarInfo v19 = new CompositeStrategyVarInfo(_calculatepenman,"lambda");
				 _parameters0_0.Add(v19);
				VarInfo v20 = new CompositeStrategyVarInfo(_calculatepenman,"Alpha");
				 _parameters0_0.Add(v20);
				VarInfo v21 = new CompositeStrategyVarInfo(_calculatecanopytemperature,"lambda");
				 _parameters0_0.Add(v21);
				VarInfo v22 = new CompositeStrategyVarInfo(_calculatecanopytemperature,"rhoDensityAir");
				 _parameters0_0.Add(v22);
				VarInfo v23 = new CompositeStrategyVarInfo(_calculatecanopytemperature,"specificHeatCapacityAir");
				 _parameters0_0.Add(v23);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityEnergyBalance.EnergyBalanceState);
				pd1.PropertyName = "isWindVpDefined";
				pd1.PropertyType = (( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.isWindVpDefined)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.isWindVpDefined);
				_inputs0_0.Add(pd1);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				mo0_0.Outputs=_outputs0_0;
				//Associated strategies
				List<string> lAssStrat0_0 = new List<string>();
				lAssStrat0_0.Add(typeof(SiriusQualityEnergyBalance.CalculateNetRadiation).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityEnergyBalance.CalculateDiffusionLimitedEvaporation).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityEnergyBalance.CalculatePriestlyTaylor).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityEnergyBalance.CalculatePtSoil).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityEnergyBalance.CalculateConductance).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityEnergyBalance.CalculatePenman).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityEnergyBalance.CalculatePotTranspiration).FullName);
				lAssStrat0_0.Add(typeof(SiriusQuailtyEnergyBalance.CalculateSoilEvaporation).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityEnergyBalance.CalculateSoilHeatFlux).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityEnergyBalance.CalculateCropHeatFlux).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityEnergyBalance.CalculateCanopyTemperature).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityEnergyBalance.CalculateCanopyTemperatureMaize).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityEnergyBalance.Calculatevpdairleaf).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityEnergyBalance.CalculateVPDeq).FullName);
				
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

			
			public int SwitchMaize
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("SwitchMaize");
						if (vi != null && vi.CurrentValue!=null) return (int)vi.CurrentValue ;
						else throw new Exception("Parameter 'SwitchMaize' not found (or found null) in strategy 'EnergyBalance'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("SwitchMaize");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'SwitchMaize' not found in strategy 'EnergyBalance'");
				}
			}

			// Getter and setters for the value of the parameters of a composite strategy
			
			public Double albedoCoefficient
			{ 
				get {
						return _calculatenetradiation.albedoCoefficient ;
				}
				set {
						_calculatenetradiation.albedoCoefficient=value;
				}
			}
			public Double stefanBoltzman
			{ 
				get {
						return _calculatenetradiation.stefanBoltzman ;
				}
				set {
						_calculatenetradiation.stefanBoltzman=value;
				}
			}
			public Double elevation
			{ 
				get {
						return _calculatenetradiation.elevation ;
				}
				set {
						_calculatenetradiation.elevation=value;
				}
			}
			public Double soilDiffusionConstant
			{ 
				get {
						return _calculatediffusionlimitedevaporation.soilDiffusionConstant ;
				}
				set {
						_calculatediffusionlimitedevaporation.soilDiffusionConstant=value;
				}
			}
			public Double lambda
			{ 
				get {
						return _calculatepriestlytaylor.lambda ;
				}
				set {
						_calculatepriestlytaylor.lambda=value;
						_calculatepenman.lambda=value;
						_calculatecanopytemperature.lambda=value;
				}
			}
			public Double psychrometricConstant
			{ 
				get {
						return _calculatepriestlytaylor.psychrometricConstant ;
				}
				set {
						_calculatepriestlytaylor.psychrometricConstant=value;
						_calculatepenman.psychrometricConstant=value;
				}
			}
			public Double Alpha
			{ 
				get {
						return _calculatepriestlytaylor.Alpha ;
				}
				set {
						_calculatepriestlytaylor.Alpha=value;
						_calculateptsoil.Alpha=value;
						_calculatepenman.Alpha=value;
				}
			}
			public Double tauAlpha
			{ 
				get {
						return _calculateptsoil.tauAlpha ;
				}
				set {
						_calculateptsoil.tauAlpha=value;
				}
			}
			public Double vonKarman
			{ 
				get {
						return _calculateconductance.vonKarman ;
				}
				set {
						_calculateconductance.vonKarman=value;
				}
			}
			public Double heightWeatherMeasurements
			{ 
				get {
						return _calculateconductance.heightWeatherMeasurements ;
				}
				set {
						_calculateconductance.heightWeatherMeasurements=value;
				}
			}
			public Double zm
			{ 
				get {
						return _calculateconductance.zm ;
				}
				set {
						_calculateconductance.zm=value;
				}
			}
			public Double zh
			{ 
				get {
						return _calculateconductance.zh ;
				}
				set {
						_calculateconductance.zh=value;
				}
			}
			public Double d
			{ 
				get {
						return _calculateconductance.d ;
				}
				set {
						_calculateconductance.d=value;
				}
			}
			public Double rhoDensityAir
			{ 
				get {
						return _calculatepenman.rhoDensityAir ;
				}
				set {
						_calculatepenman.rhoDensityAir=value;
						_calculatecanopytemperature.rhoDensityAir=value;
				}
			}
			public Double specificHeatCapacityAir
			{ 
				get {
						return _calculatepenman.specificHeatCapacityAir ;
				}
				set {
						_calculatepenman.specificHeatCapacityAir=value;
						_calculatecanopytemperature.specificHeatCapacityAir=value;
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
				
					_calculatenetradiation.SetParametersDefaultValue();
					_calculatediffusionlimitedevaporation.SetParametersDefaultValue();
					_calculatepriestlytaylor.SetParametersDefaultValue();
					_calculateptsoil.SetParametersDefaultValue();
					_calculateconductance.SetParametersDefaultValue();
					_calculatepenman.SetParametersDefaultValue();
					_calculatepottranspiration.SetParametersDefaultValue();
					_calculatesoilevaporation.SetParametersDefaultValue();
					_calculatesoilheatflux.SetParametersDefaultValue();
					_calculatecropheatflux.SetParametersDefaultValue();
					_calculatecanopytemperature.SetParametersDefaultValue();
					_calculatevpdairleaf.SetParametersDefaultValue();
					_calculatevpdeq.SetParametersDefaultValue(); 
					_calculatecanopytemperaturemaize.SetParametersDefaultValue(); 

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
                SwitchMaizeVarInfo.Name = "SwitchMaize";
				SwitchMaizeVarInfo.Description =" true if maize false if wheat";
				SwitchMaizeVarInfo.MaxValue = 1;
				SwitchMaizeVarInfo.MinValue = 0;
				SwitchMaizeVarInfo.DefaultValue = 0;
				SwitchMaizeVarInfo.Units = "NA";
				SwitchMaizeVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Integer");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _SwitchMaizeVarInfo= new VarInfo();
				/// <summary> 
				///SwitchMaize VarInfo definition
				/// </summary>
				public static VarInfo SwitchMaizeVarInfo
				{
					get { return _SwitchMaizeVarInfo; }
				}					
			
			//Parameters static VarInfo list of the composite class
			
				/// <summary> 
				///albedoCoefficient VarInfo definition
				/// </summary>
				public static VarInfo albedoCoefficientVarInfo
				{
					get { return SiriusQualityEnergyBalance.CalculateNetRadiation.albedoCoefficientVarInfo; }
				}
				/// <summary> 
				///stefanBoltzman VarInfo definition
				/// </summary>
				public static VarInfo stefanBoltzmanVarInfo
				{
					get { return SiriusQualityEnergyBalance.CalculateNetRadiation.stefanBoltzmanVarInfo; }
				}
				/// <summary> 
				///elevation VarInfo definition
				/// </summary>
				public static VarInfo elevationVarInfo
				{
					get { return SiriusQualityEnergyBalance.CalculateNetRadiation.elevationVarInfo; }
				}
				/// <summary> 
				///soilDiffusionConstant VarInfo definition
				/// </summary>
				public static VarInfo soilDiffusionConstantVarInfo
				{
					get { return SiriusQualityEnergyBalance.CalculateDiffusionLimitedEvaporation.soilDiffusionConstantVarInfo; }
				}
				/// <summary> 
				///lambda VarInfo definition
				/// </summary>
				public static VarInfo lambdaVarInfo
				{
					get { return SiriusQualityEnergyBalance.CalculatePriestlyTaylor.lambdaVarInfo; }
				}
				/// <summary> 
				///psychrometricConstant VarInfo definition
				/// </summary>
				public static VarInfo psychrometricConstantVarInfo
				{
					get { return SiriusQualityEnergyBalance.CalculatePriestlyTaylor.psychrometricConstantVarInfo; }
				}
				/// <summary> 
				///Alpha VarInfo definition
				/// </summary>
				public static VarInfo AlphaVarInfo
				{
					get { return SiriusQualityEnergyBalance.CalculatePriestlyTaylor.AlphaVarInfo; }
				}
				/// <summary> 
				///tauAlpha VarInfo definition
				/// </summary>
				public static VarInfo tauAlphaVarInfo
				{
					get { return SiriusQualityEnergyBalance.CalculatePtSoil.tauAlphaVarInfo; }
				}
				/// <summary> 
				///vonKarman VarInfo definition
				/// </summary>
				public static VarInfo vonKarmanVarInfo
				{
					get { return SiriusQualityEnergyBalance.CalculateConductance.vonKarmanVarInfo; }
				}
				/// <summary> 
				///heightWeatherMeasurements VarInfo definition
				/// </summary>
				public static VarInfo heightWeatherMeasurementsVarInfo
				{
					get { return SiriusQualityEnergyBalance.CalculateConductance.heightWeatherMeasurementsVarInfo; }
				}
				/// <summary> 
				///zm VarInfo definition
				/// </summary>
				public static VarInfo zmVarInfo
				{
					get { return SiriusQualityEnergyBalance.CalculateConductance.zmVarInfo; }
				}
				/// <summary> 
				///zh VarInfo definition
				/// </summary>
				public static VarInfo zhVarInfo
				{
					get { return SiriusQualityEnergyBalance.CalculateConductance.zhVarInfo; }
				}
				/// <summary> 
				///d VarInfo definition
				/// </summary>
				public static VarInfo dVarInfo
				{
					get { return SiriusQualityEnergyBalance.CalculateConductance.dVarInfo; }
				}
				/// <summary> 
				///rhoDensityAir VarInfo definition
				/// </summary>
				public static VarInfo rhoDensityAirVarInfo
				{
					get { return SiriusQualityEnergyBalance.CalculatePenman.rhoDensityAirVarInfo; }
				}
				/// <summary> 
				///specificHeatCapacityAir VarInfo definition
				/// </summary>
				public static VarInfo specificHeatCapacityAirVarInfo
				{
					get { return SiriusQualityEnergyBalance.CalculatePenman.specificHeatCapacityAirVarInfo; }
				}			

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
					
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					

					
					string ret = "";
					 ret += _calculatenetradiation.TestPostConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculateNetRadiation");
					 ret += _calculatediffusionlimitedevaporation.TestPostConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculateDiffusionLimitedEvaporation");
					 ret += _calculatepriestlytaylor.TestPostConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculatePriestlyTaylor");
					 ret += _calculateptsoil.TestPostConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculatePtSoil");
					 ret += _calculateconductance.TestPostConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculateConductance");
					 ret += _calculatepenman.TestPostConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculatePenman");
					 ret += _calculatepottranspiration.TestPostConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculatePotTranspiration");
					 ret += _calculatesoilevaporation.TestPostConditions(energybalancestate, "strategy SiriusQuailtyEnergyBalance.CalculateSoilEvaporation");
					 ret += _calculatesoilheatflux.TestPostConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculateSoilHeatFlux");
					 ret += _calculatecropheatflux.TestPostConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculateCropHeatFlux");
					 ret += _calculatecanopytemperature.TestPostConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculateCanopyTemperature");
					 ret += _calculatecanopytemperaturemaize.TestPostConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculateCanopyTemperatureMaize");
					 ret += _calculatevpdairleaf.TestPostConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.Calculatevpdairleaf");
					 ret += _calculatevpdeq.TestPostConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.calculateVPDeq");
					if (ret != "") { pre.TestsOut(ret, true, "   postconditions tests of associated classes"); }

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
					
					SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.isWindVpDefined.CurrentValue=energybalancestate.isWindVpDefined;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.isWindVpDefined);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.isWindVpDefined.ValueType)){prc.AddCondition(r1);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("SwitchMaize")));

					
					string ret = "";
					 ret += _calculatenetradiation.TestPreConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculateNetRadiation");
					 ret += _calculatediffusionlimitedevaporation.TestPreConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculateDiffusionLimitedEvaporation");
					 ret += _calculatepriestlytaylor.TestPreConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculatePriestlyTaylor");
					 ret += _calculateptsoil.TestPreConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculatePtSoil");
					 ret += _calculateconductance.TestPreConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculateConductance");
					 ret += _calculatepenman.TestPreConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculatePenman");
					 ret += _calculatepottranspiration.TestPreConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculatePotTranspiration");
					 ret += _calculatesoilevaporation.TestPreConditions(energybalancestate, "strategy SiriusQuailtyEnergyBalance.CalculateSoilEvaporation");
					 ret += _calculatesoilheatflux.TestPreConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculateSoilHeatFlux");
					 ret += _calculatecropheatflux.TestPreConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculateCropHeatFlux");
					 ret += _calculatecanopytemperature.TestPreConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculateCanopyTemperature");
					 ret += _calculatecanopytemperaturemaize.TestPreConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.CalculateCanopyTemperatureMaize");
					 ret += _calculatevpdairleaf.TestPreConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.Calculatevpdairleaf");
					 ret += _calculatevpdeq.TestPreConditions(energybalancestate, "strategy SiriusQualityEnergyBalance.calculateVPDeq");
					if (ret != "") { pre.TestsOut(ret, true, "   preconditions tests of associated classes"); }

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
				
					EstimateOfAssociatedClasses(energybalancestate);

				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section1
				//Code written below will not be overwritten by a future code generation

        

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				
			#region Composite class: associations

			//Declaration of the associated strategies
			SiriusQualityEnergyBalance.CalculateNetRadiation _calculatenetradiation = new SiriusQualityEnergyBalance.CalculateNetRadiation();
			SiriusQualityEnergyBalance.CalculateDiffusionLimitedEvaporation _calculatediffusionlimitedevaporation = new SiriusQualityEnergyBalance.CalculateDiffusionLimitedEvaporation();
			SiriusQualityEnergyBalance.CalculatePriestlyTaylor _calculatepriestlytaylor = new SiriusQualityEnergyBalance.CalculatePriestlyTaylor();
			SiriusQualityEnergyBalance.CalculatePtSoil _calculateptsoil = new SiriusQualityEnergyBalance.CalculatePtSoil();
			SiriusQualityEnergyBalance.CalculateConductance _calculateconductance = new SiriusQualityEnergyBalance.CalculateConductance();
			SiriusQualityEnergyBalance.CalculatePenman _calculatepenman = new SiriusQualityEnergyBalance.CalculatePenman();
			SiriusQualityEnergyBalance.CalculatePotTranspiration _calculatepottranspiration = new SiriusQualityEnergyBalance.CalculatePotTranspiration();
			SiriusQuailtyEnergyBalance.CalculateSoilEvaporation _calculatesoilevaporation = new SiriusQuailtyEnergyBalance.CalculateSoilEvaporation();
			SiriusQualityEnergyBalance.CalculateSoilHeatFlux _calculatesoilheatflux = new SiriusQualityEnergyBalance.CalculateSoilHeatFlux();
			SiriusQualityEnergyBalance.CalculateCropHeatFlux _calculatecropheatflux = new SiriusQualityEnergyBalance.CalculateCropHeatFlux();
			SiriusQualityEnergyBalance.CalculateCanopyTemperature _calculatecanopytemperature = new SiriusQualityEnergyBalance.CalculateCanopyTemperature();
			SiriusQualityEnergyBalance.CalculateCanopyTemperatureMaize _calculatecanopytemperaturemaize = new SiriusQualityEnergyBalance.CalculateCanopyTemperatureMaize();
			SiriusQualityEnergyBalance.Calculatevpdairleaf _calculatevpdairleaf = new SiriusQualityEnergyBalance.Calculatevpdairleaf();
			SiriusQualityEnergyBalance.CalculateVPDeq _calculatevpdeq = new SiriusQualityEnergyBalance.CalculateVPDeq();

			//Call of the associated strategies
			private void EstimateOfAssociatedClasses(SiriusQualityEnergyBalance.EnergyBalanceState energybalancestate){
				_calculatenetradiation.Estimate(energybalancestate);

                energybalancestate.netRadiationEquivalentEvaporation = energybalancestate.netRadiation / lambda * 1000;

				_calculatediffusionlimitedevaporation.Estimate(energybalancestate);
				_calculatepriestlytaylor.Estimate(energybalancestate);
				_calculateptsoil.Estimate(energybalancestate);
				_calculateconductance.Estimate(energybalancestate);

                if (energybalancestate.isWindVpDefined == 1)
                {
                    _calculatepenman.Estimate(energybalancestate);
                    energybalancestate.evapoTranspiration = energybalancestate.evapoTranspirationPenman;
                }
                else
                {
                    energybalancestate.evapoTranspiration = energybalancestate.evapoTranspirationPriestlyTaylor;
                }

				
				_calculatepottranspiration.Estimate(energybalancestate);
				_calculatesoilevaporation.Estimate(energybalancestate);

                energybalancestate.potentialEvapoTranspiration = energybalancestate.potentialTranspiration + energybalancestate.soilEvaporation;

				_calculatesoilheatflux.Estimate(energybalancestate);
				_calculatecropheatflux.Estimate(energybalancestate);
				_calculatecanopytemperature.Estimate(energybalancestate);

                if (SwitchMaize ==1)
                {
                    _calculatecanopytemperaturemaize.Estimate(energybalancestate);

                    _calculatevpdairleaf.Estimate(energybalancestate);
                    _calculatevpdeq.Estimate(energybalancestate);

                }
			}

			#endregion


	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation

                /// <summary>
                /// copy constructor. We only need to copy the parameters (the strategies being stateless)
                /// </summary> 
            public EnergyBalance(EnergyBalance toCopy)
                : this()
                {
                    SwitchMaize= toCopy.SwitchMaize;
			
                    albedoCoefficient= toCopy.albedoCoefficient;

                    stefanBoltzman= toCopy.stefanBoltzman;

                    elevation =toCopy.elevation;

                    soilDiffusionConstant =toCopy.soilDiffusionConstant;

                    lambda =toCopy.lambda;
                
                    psychrometricConstant =toCopy.psychrometricConstant;

                    Alpha =toCopy.Alpha;

                    tauAlpha =toCopy.tauAlpha;

                    vonKarman=toCopy.vonKarman;

                    heightWeatherMeasurements=toCopy.heightWeatherMeasurements;

                    zm=toCopy.zm;

                    zh=toCopy.zh;

                    d=toCopy.d;

                    rhoDensityAir=toCopy.rhoDensityAir;

                    specificHeatCapacityAir=toCopy.specificHeatCapacityAir;

                }
				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}
