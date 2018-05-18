

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
//To make this project compile please add the reference to assembly: System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089;

namespace SiriusQualityEnergyBalance
{

	/// <summary>
	///Class CalculateNetRadiation
    /// 
    /// </summary>
	public class CalculateNetRadiation : IStrategySiriusQualityEnergyBalance
	{

	#region Constructor

			public CalculateNetRadiation()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 0.23;
				 v1.Description = "albedoCoefficient";
				 v1.Id = 0;
				 v1.MaxValue = 1;
				 v1.MinValue = 0;
				 v1.Name = "albedoCoefficient";
				 v1.Size = 1;
				 v1.Units = "?";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new VarInfo();
				 v2.DefaultValue = 1E-08;
				 v2.Description = "stefanBoltzman";
				 v2.Id = 0;
				 v2.MaxValue = 1;
				 v2.MinValue = 0;
				 v2.Name = "stefanBoltzman";
				 v2.Size = 1;
				 v2.Units = "?";
				 v2.URL = "";
				 v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new VarInfo();
				 v3.DefaultValue = 0;
				 v3.Description = "elevation";
				 v3.Id = 0;
				 v3.MaxValue = 10000;
				 v3.MinValue = -500;
				 v3.Name = "elevation";
				 v3.Size = 1;
				 v3.Units = "m";
				 v3.URL = "";
				 v3.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v3.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v3);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityEnergyBalance.EnergyBalanceState);
				pd1.PropertyName = "solarRadiation";
				pd1.PropertyType = (( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.solarRadiation)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.solarRadiation);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityEnergyBalance.EnergyBalanceState);
				pd2.PropertyName = "minTair";
				pd2.PropertyType = (( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.minTair)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.minTair);
				_inputs0_0.Add(pd2);
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityEnergyBalance.EnergyBalanceState);
				pd3.PropertyName = "maxTair";
				pd3.PropertyType = (( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.maxTair)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.maxTair);
				_inputs0_0.Add(pd3);
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityEnergyBalance.EnergyBalanceState);
				pd4.PropertyName = "vaporPressure";
				pd4.PropertyType = (( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.vaporPressure)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.vaporPressure);
				_inputs0_0.Add(pd4);
				PropertyDescription pd5 = new PropertyDescription();
				pd5.DomainClassType = typeof(SiriusQualityEnergyBalance.EnergyBalanceState);
				pd5.PropertyName = "extraSolarRadiation";
				pd5.PropertyType = (( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.extraSolarRadiation)).ValueType.TypeForCurrentValue;
				pd5.PropertyVarInfo =( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.extraSolarRadiation);
				_inputs0_0.Add(pd5);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd6 = new PropertyDescription();
				pd6.DomainClassType = typeof(SiriusQualityEnergyBalance.EnergyBalanceState);
				pd6.PropertyName = "netRadiation";
				pd6.PropertyType =  (( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.netRadiation)).ValueType.TypeForCurrentValue;
				pd6.PropertyVarInfo =(  SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.netRadiation);
				_outputs0_0.Add(pd6);
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

			
			public Double albedoCoefficient
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("albedoCoefficient");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'albedoCoefficient' not found (or found null) in strategy 'CalculateNetRadiation'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("albedoCoefficient");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'albedoCoefficient' not found in strategy 'CalculateNetRadiation'");
				}
			}
			public Double stefanBoltzman
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("stefanBoltzman");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'stefanBoltzman' not found (or found null) in strategy 'CalculateNetRadiation'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("stefanBoltzman");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'stefanBoltzman' not found in strategy 'CalculateNetRadiation'");
				}
			}
			public Double elevation
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("elevation");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'elevation' not found (or found null) in strategy 'CalculateNetRadiation'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("elevation");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'elevation' not found in strategy 'CalculateNetRadiation'");
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
                albedoCoefficientVarInfo.Name = "albedoCoefficient";
				albedoCoefficientVarInfo.Description =" albedoCoefficient";
				albedoCoefficientVarInfo.MaxValue = 1;
				albedoCoefficientVarInfo.MinValue = 0;
				albedoCoefficientVarInfo.DefaultValue = 0.23;
				albedoCoefficientVarInfo.Units = "?";
				albedoCoefficientVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				stefanBoltzmanVarInfo.Name = "stefanBoltzman";
				stefanBoltzmanVarInfo.Description =" stefanBoltzman";
				stefanBoltzmanVarInfo.MaxValue = 1;
				stefanBoltzmanVarInfo.MinValue = 0;
				stefanBoltzmanVarInfo.DefaultValue = 1E-08;
				stefanBoltzmanVarInfo.Units = "?";
				stefanBoltzmanVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				elevationVarInfo.Name = "elevation";
				elevationVarInfo.Description =" elevation";
				elevationVarInfo.MaxValue = 10000;
				elevationVarInfo.MinValue = -500;
				elevationVarInfo.DefaultValue = 0;
				elevationVarInfo.Units = "m";
				elevationVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _albedoCoefficientVarInfo= new VarInfo();
				/// <summary> 
				///albedoCoefficient VarInfo definition
				/// </summary>
				public static VarInfo albedoCoefficientVarInfo
				{
					get { return _albedoCoefficientVarInfo; }
				}
				private static VarInfo _stefanBoltzmanVarInfo= new VarInfo();
				/// <summary> 
				///stefanBoltzman VarInfo definition
				/// </summary>
				public static VarInfo stefanBoltzmanVarInfo
				{
					get { return _stefanBoltzmanVarInfo; }
				}
				private static VarInfo _elevationVarInfo= new VarInfo();
				/// <summary> 
				///elevation VarInfo definition
				/// </summary>
				public static VarInfo elevationVarInfo
				{
					get { return _elevationVarInfo; }
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
					
					SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.netRadiation.CurrentValue=energybalancestate.netRadiation;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r6 = new RangeBasedCondition(SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.netRadiation);
					if(r6.ApplicableVarInfoValueTypes.Contains( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.netRadiation.ValueType)){prc.AddCondition(r6);}

					

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
					
					SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.solarRadiation.CurrentValue=energybalancestate.solarRadiation;
					SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.minTair.CurrentValue=energybalancestate.minTair;
					SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.maxTair.CurrentValue=energybalancestate.maxTair;
					SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.vaporPressure.CurrentValue=energybalancestate.vaporPressure;
					SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.extraSolarRadiation.CurrentValue=energybalancestate.extraSolarRadiation;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.solarRadiation);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.solarRadiation.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.minTair);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.minTair.ValueType)){prc.AddCondition(r2);}
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.maxTair);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.maxTair.ValueType)){prc.AddCondition(r3);}
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.vaporPressure);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.vaporPressure.ValueType)){prc.AddCondition(r4);}
					RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.extraSolarRadiation);
					if(r5.ApplicableVarInfoValueTypes.Contains( SiriusQualityEnergyBalance.EnergyBalanceStateVarInfo.extraSolarRadiation.ValueType)){prc.AddCondition(r5);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("albedoCoefficient")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("stefanBoltzman")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("elevation")));

					

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
                double Nsr = NetShortwaveRadiation(energybalancestate.solarRadiation);
                double Nolr = NetOutgoingLongwaveRadiation(energybalancestate.minTair, energybalancestate.maxTair, energybalancestate.vaporPressure, energybalancestate.solarRadiation, energybalancestate.extraSolarRadiation);

                energybalancestate.netRadiation= Nsr - Nolr;
        

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation
                ///<summary>
                ///Net shortwave radiation
                ///</summary>
                ///<param name="solarRadiation"></param>
                ///<param name="albedoCoefficient"></param>
                ///<returns></returns>
                private double NetShortwaveRadiation(double solarRadiation)
                {
                    return (1 - albedoCoefficient) * solarRadiation;
                }

                ///<summary>
                ///Net outgoing longwave radiation
                ///</summary>
                ///<param name="Tmin"></param>
                ///<param name="Tmax"></param>
                ///<param name="vaporPressure"></param>
                ///<param name="solarRadiation"></param>
                ///<param name="extraSolarRadiation"></param>
                ///<param name="elevation"></param>
                ///<param name="stefanBoltzman"></param>
                ///<returns></returns>
                private double NetOutgoingLongwaveRadiation(double Tmin, double Tmax, double vaporPressure, double solarRadiation, double extraSolarRadiation)
                {

                    double clearSkySolarRadiation = (0.75 + 2 * Math.Pow(10, -5) * elevation) * extraSolarRadiation;
                    double averageT = (Math.Pow(Tmax + 273.16, 4) + Math.Pow(Tmin + 273.16, 4)) / 2;                             // �C 
                    double surfaceEmissivity = (0.34 - 0.14 * Math.Sqrt(vaporPressure / 10));                                      // Surface emissivity --> VaporPressure divided by 10 to convert hPa to kPa
                    double cloudCoverFactor = (1.35 * (solarRadiation / clearSkySolarRadiation) - 0.35);
                    return stefanBoltzman * averageT * surfaceEmissivity * cloudCoverFactor;
                }
				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}
