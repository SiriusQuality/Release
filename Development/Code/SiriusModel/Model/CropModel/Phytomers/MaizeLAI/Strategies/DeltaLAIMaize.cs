

 //Author:pierre martre pierre.martre@inra.fr
 //Institution:Inra
 //Author of revision: 
 //Date first release:15/12/2016
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


using SiriusQualityMaizeLAI;
using SiriusModel.Model.CropModel.Phytomers;


//To make this project compile please add the reference to assembly: SiriusQuality-Model, Version=2.0.6193.21125, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: CRA.ModelLayer, Version=1.0.5212.29139, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: SiriusQuality-PhenologyComponent, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: csMTG, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: SiriusQuality-MeteoComponent, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: SiriusQuality-EnergyBalanceComponent, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: SiriusQuality-ThermalTime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: Sirius.Model, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: ResponseFunctions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: CRA.AgroManagement2014, Version=0.8.0.0, Culture=neutral, PublicKeyToken=null;

namespace SiriusQualityMaizeLAI.Strategies
{

	/// <summary>
	///Class DeltaLAIMaize
    /// 
    /// </summary>
	public class DeltaLAIMaize : IStrategySiriusQualityMaizeLAI
	{

	#region Constructor

			public DeltaLAIMaize()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 16;
				 v1.Description = "final leaf number ";
				 v1.Id = 0;
				 v1.MaxValue = 20;
				 v1.MinValue = 10;
				 v1.Name = "Nfinal";
				 v1.Size = 1;
				 v1.Units = "leaf";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new VarInfo();
				 v2.DefaultValue = 7.5;
				 v2.Description = " ";
				 v2.Id = 0;
				 v2.MaxValue = 10;
				 v2.MinValue = 0;
				 v2.Name = "plantDensity";
				 v2.Size = 1;
				 v2.Units = " ";
				 v2.URL = "";
				 v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v2);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityMaizeLAI.MaizeLAIState);
				pd1.PropertyName = "DEF";
				pd1.PropertyType = (( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.DEF)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.DEF);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityMaizeLAI.MaizeLAIState);
				pd2.PropertyName = "cumulTTPHenoMaize";
				pd2.PropertyType = (( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.cumulTTPHenoMaize)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.cumulTTPHenoMaize);
				_inputs0_0.Add(pd2);
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityMaizeLAI.MaizeLAIState);
				pd3.PropertyName = "LER";
				pd3.PropertyType = (( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.LER)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.LER);
				_inputs0_0.Add(pd3);
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityMaizeLAI.MaizeLAIState);
				pd4.PropertyName = "deltaTTPhenoMaize";
				pd4.PropertyType = (( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.deltaTTPhenoMaize)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.deltaTTPhenoMaize);
				_inputs0_0.Add(pd4);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd5 = new PropertyDescription();
				pd5.DomainClassType = typeof(SiriusQualityMaizeLAI.MaizeLAIState);
				pd5.PropertyName = "leafLayerState";
                pd5.PropertyType = ((SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.leafStateList)).ValueType.TypeForCurrentValue;
                pd5.PropertyVarInfo = (SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.leafStateList);
				_outputs0_0.Add(pd5);
				PropertyDescription pd6 = new PropertyDescription();
				pd6.DomainClassType = typeof(SiriusQualityMaizeLAI.MaizeLAIState);
				pd6.PropertyName = "isPrematurelyDying";
				pd6.PropertyType =  (( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.isPrematurelyDying)).ValueType.TypeForCurrentValue;
				pd6.PropertyVarInfo =(  SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.isPrematurelyDying);
				_outputs0_0.Add(pd6);
				PropertyDescription pd7 = new PropertyDescription();
				pd7.DomainClassType = typeof(SiriusQualityMaizeLAI.MaizeLAIState);
				pd7.PropertyName = "WaterLimitedPotDeltaAIList";
				pd7.PropertyType =  (( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.WaterLimitedPotDeltaAIList)).ValueType.TypeForCurrentValue;
				pd7.PropertyVarInfo =(  SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.WaterLimitedPotDeltaAIList);
				_outputs0_0.Add(pd7);
				PropertyDescription pd8 = new PropertyDescription();
				pd8.DomainClassType = typeof(SiriusQualityMaizeLAI.MaizeLAIState);
				pd8.PropertyName = "incDeltaAreaLimitSF";
				pd8.PropertyType =  (( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.incDeltaAreaLimitSF)).ValueType.TypeForCurrentValue;
				pd8.PropertyVarInfo =(  SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.incDeltaAreaLimitSF);
				_outputs0_0.Add(pd8);
				PropertyDescription pd9 = new PropertyDescription();
				pd9.DomainClassType = typeof(SiriusQualityMaizeLAI.MaizeLAIState);
				pd9.PropertyName = "potentialIncDeltaArea";
				pd9.PropertyType =  (( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.potentialIncDeltaArea)).ValueType.TypeForCurrentValue;
				pd9.PropertyVarInfo =(  SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.potentialIncDeltaArea);
				_outputs0_0.Add(pd9);
				PropertyDescription pd10 = new PropertyDescription();
				pd10.DomainClassType = typeof(SiriusQualityMaizeLAI.MaizeLAIState);
				pd10.PropertyName = "WaterLimitedPotExposedDeltaAIList";
				pd10.PropertyType =  (( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.WaterLimitedPotExposedDeltaAIList)).ValueType.TypeForCurrentValue;
				pd10.PropertyVarInfo =(  SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.WaterLimitedPotExposedDeltaAIList);
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
				get {  return "Crop"; }
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
				_pd.Add("Creator", "pierre.martre@inra.fr");
				_pd.Add("Date", "15/12/2016");
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
				return new List<Type>() {  typeof(SiriusQualityMaizeLAI.MaizeLAIState) };
			}

	#endregion

    #region Instances of the parameters
			
			// Getter and setters for the value of the parameters of the strategy. The actual parameters are stored into the ModelingOptionsManager of the strategy.

			
			public Double Nfinal
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("Nfinal");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'Nfinal' not found (or found null) in strategy 'DeltaLAIMaize'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("Nfinal");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'Nfinal' not found in strategy 'DeltaLAIMaize'");
				}
			}
			public Double plantDensity
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("plantDensity");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'plantDensity' not found (or found null) in strategy 'DeltaLAIMaize'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("plantDensity");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'plantDensity' not found in strategy 'DeltaLAIMaize'");
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
                NfinalVarInfo.Name = "Nfinal";
				NfinalVarInfo.Description =" final leaf number ";
				NfinalVarInfo.MaxValue = 20;
				NfinalVarInfo.MinValue = 10;
				NfinalVarInfo.DefaultValue = 16;
				NfinalVarInfo.Units = "leaf";
				NfinalVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				plantDensityVarInfo.Name = "plantDensity";
				plantDensityVarInfo.Description ="  ";
				plantDensityVarInfo.MaxValue = 10;
				plantDensityVarInfo.MinValue = 0;
				plantDensityVarInfo.DefaultValue = 7.5;
				plantDensityVarInfo.Units = " ";
				plantDensityVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _NfinalVarInfo= new VarInfo();
				/// <summary> 
				///Nfinal VarInfo definition
				/// </summary>
				public static VarInfo NfinalVarInfo
				{
					get { return _NfinalVarInfo; }
				}
				private static VarInfo _plantDensityVarInfo= new VarInfo();
				/// <summary> 
				///plantDensity VarInfo definition
				/// </summary>
				public static VarInfo plantDensityVarInfo
				{
					get { return _plantDensityVarInfo; }
				}					
			
			//Parameters static VarInfo list of the composite class
						

	#endregion
	
	#region pre/post conditions management		

		    /// <summary>
			/// Test to verify the postconditions
			/// </summary>
			public string TestPostConditions(SiriusQualityMaizeLAI.MaizeLAIState maizelaistate, string callID)
			{
				try
				{
					//Set current values of the outputs to the static VarInfo representing the output properties of the domain classes				

                    SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.leafStateList.CurrentValue = maizelaistate.leafStateList;
					SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.isPrematurelyDying.CurrentValue=maizelaistate.isPrematurelyDying;
					SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.WaterLimitedPotDeltaAIList.CurrentValue=maizelaistate.WaterLimitedPotDeltaAIList;
					SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.incDeltaAreaLimitSF.CurrentValue=maizelaistate.incDeltaAreaLimitSF;
					SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.potentialIncDeltaArea.CurrentValue=maizelaistate.potentialIncDeltaArea;
					SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.WaterLimitedPotExposedDeltaAIList.CurrentValue=maizelaistate.WaterLimitedPotExposedDeltaAIList;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();


                    RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.leafStateList);
                    if (r5.ApplicableVarInfoValueTypes.Contains(SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.leafStateList.ValueType)) { prc.AddCondition(r5); }
					RangeBasedCondition r6 = new RangeBasedCondition(SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.isPrematurelyDying);
					if(r6.ApplicableVarInfoValueTypes.Contains( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.isPrematurelyDying.ValueType)){prc.AddCondition(r6);}
					RangeBasedCondition r7 = new RangeBasedCondition(SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.WaterLimitedPotDeltaAIList);
					if(r7.ApplicableVarInfoValueTypes.Contains( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.WaterLimitedPotDeltaAIList.ValueType)){prc.AddCondition(r7);}
					RangeBasedCondition r8 = new RangeBasedCondition(SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.incDeltaAreaLimitSF);
					if(r8.ApplicableVarInfoValueTypes.Contains( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.incDeltaAreaLimitSF.ValueType)){prc.AddCondition(r8);}
					RangeBasedCondition r9 = new RangeBasedCondition(SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.potentialIncDeltaArea);
					if(r9.ApplicableVarInfoValueTypes.Contains( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.potentialIncDeltaArea.ValueType)){prc.AddCondition(r9);}
					RangeBasedCondition r10 = new RangeBasedCondition(SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.WaterLimitedPotExposedDeltaAIList);
					if(r10.ApplicableVarInfoValueTypes.Contains( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.WaterLimitedPotExposedDeltaAIList.ValueType)){prc.AddCondition(r10);}

					

					//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section4
					//Code written below will not be overwritten by a future code generation

        

					//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
					//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section4 

					//Get the evaluation of postconditions
					string postConditionsResult =pre.VerifyPostconditions(prc, callID);
					//if we have errors, send it to the configured output 
					if(!string.IsNullOrEmpty(postConditionsResult)) { pre.TestsOut(postConditionsResult, true, "PostConditions errors in component SiriusQualityMaizeLAI.Strategies, strategy " + this.GetType().Name ); }
					return postConditionsResult;
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1001,	"Strategy: " + this.GetType().Name + " - Unhandled exception running post-conditions");

					string msg = "Component SiriusQualityMaizeLAI.Strategies, " + this.GetType().Name + ": Unhandled exception running post-condition test. ";
					throw new Exception(msg, exception);
				}
			}

			/// <summary>
			/// Test to verify the preconditions
			/// </summary>
			public string TestPreConditions(SiriusQualityMaizeLAI.MaizeLAIState maizelaistate, string callID)
			{
				try
				{
					//Set current values of the inputs to the static VarInfo representing the input properties of the domain classes				
					
					SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.DEF.CurrentValue=maizelaistate.DEF;
					SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.cumulTTPHenoMaize.CurrentValue=maizelaistate.cumulTTPHenoMaize;
					SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.LER.CurrentValue=maizelaistate.LER;
					SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.deltaTTPhenoMaize.CurrentValue=maizelaistate.deltaTTPhenoMaize;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.DEF);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.DEF.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.cumulTTPHenoMaize);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.cumulTTPHenoMaize.ValueType)){prc.AddCondition(r2);}
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.LER);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.LER.ValueType)){prc.AddCondition(r3);}
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.deltaTTPhenoMaize);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.deltaTTPhenoMaize.ValueType)){prc.AddCondition(r4);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("Nfinal")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("plantDensity")));

					

					//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section3
					//Code written below will not be overwritten by a future code generation

        

					//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
					//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section3 
								
					//Get the evaluation of preconditions;					
					string preConditionsResult =pre.VerifyPreconditions(prc, callID);
					//if we have errors, send it to the configured output 
					if(!string.IsNullOrEmpty(preConditionsResult)) { pre.TestsOut(preConditionsResult, true, "PreConditions errors in component SiriusQualityMaizeLAI.Strategies, strategy " + this.GetType().Name ); }
					return preConditionsResult;
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//	TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1002,"Strategy: " + this.GetType().Name + " - Unhandled exception running pre-conditions");

					string msg = "Component SiriusQualityMaizeLAI.Strategies, " + this.GetType().Name + ": Unhandled exception running pre-condition test. ";
					throw new Exception(msg, exception);
				}
			}

		
	#endregion
		


	#region Model

            public void Estimate(SiriusQualityMaizeLAI.MaizeLAIState maizelaistate)
            { }
		 	/// <summary>
			/// Run the strategy to calculate the outputs. In case of error during the execution, the preconditions tests are executed.
			/// </summary>
            public void Estimate(SiriusQualityMaizeLAI.MaizeLAIState maizelaistate, List<LeafLayer> AllLeaves)
			{
				try
				{
					CalculateModel(maizelaistate, AllLeaves );

					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Verbose, 1005,"Strategy: " + this.GetType().Name + " - Model executed");
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1003,		"Strategy: " + this.GetType().Name + " - Unhandled exception running model");

					string msg = "Error in component SiriusQualityMaizeLAI.Strategies, strategy: " + this.GetType().Name + ": Unhandled exception running model. "+exception.GetType().FullName+" - "+exception.Message;				
					throw new Exception(msg, exception);
				}
			}



            private void CalculateModel(SiriusQualityMaizeLAI.MaizeLAIState maizelaistate, List<LeafLayer> AllLeaves)
			{				
				

				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section1
				//Code written below will not be overwritten by a future code generation
                //update the size of the lists
                for (int i = 0; i < AllLeaves.Count; i++)
                {
                    //if a new leaf has appeared, increase the size of the arrays
                    if (i >= maizelaistate.WaterLimitedPotDeltaAIList.Count)
                    {
                        maizelaistate.WaterLimitedPotDeltaAIList.Add(0);
                        maizelaistate.WaterLimitedPotExposedDeltaAIList.Add(0);
                        maizelaistate.leafStateList.Add(AllLeaves[i].State);//State==growing
                        maizelaistate.isPrematurelyDying.Add(0);
                    }
                }

                CalculateNewLeafLayerStates(maizelaistate.cumulTTPHenoMaize, AllLeaves, maizelaistate);

                maizelaistate.potentialIncDeltaArea = calculateDeltaLAIpot(maizelaistate.cumulTTPHenoMaize, maizelaistate.deltaTTPhenoMaize, maizelaistate.LER, AllLeaves, maizelaistate);

                maizelaistate.incDeltaAreaLimitSF = maizelaistate.potentialIncDeltaArea * maizelaistate.DEF;
        

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation
                private void CalculateNewLeafLayerStates(double sumTTFromSowing, List<LeafLayer> All,SiriusQualityMaizeLAI.MaizeLAIState maizelaistate )
                {
                    for (int i = 0; i < All.Count; i++)
                    {
                        switch (maizelaistate.leafStateList[i])
                        {
                            case LeafState.Growing:
                                // if (sumTTFromSowing > All[i].maizeLeaf.fullyExpTT) { LeafStateList[i] = LeafState.Mature; } break;
                                if (sumTTFromSowing > All[i].maizeLeaf.liguleTT)
                                {
                                    maizelaistate.leafStateList[i] = LeafState.Mature;
                                } break; // take in account the growth of exposedFraction
                            case LeafState.Mature:
                                break;
                        }
                    }
                }

                private double calculateDeltaLAIpot(double sumTTFromSowing, double deltaTT, double LER, List<LeafLayer> AllLeaves, SiriusQualityMaizeLAI.MaizeLAIState maizelaistate)
                {
                    // some variables
                    double ttElapsed = sumTTFromSowing - deltaTT;// ttElapsed doesn't include the tt for today
                    double dltTT = deltaTT;
                    dltLERlai = 0;
                    exposedDltLERlai = 0;

                    if (AllLeaves.Count < Nfinal || ttElapsed <= AllLeaves[(int)(Nfinal - 1)].maizeLeaf.fullyExpTT)
                    {

                        //we don't calculate a deltaLAI but the whole new area
                        for (int leafNo = 0; leafNo < AllLeaves.Count; leafNo++)
                        {
                            double previousLAI = AllLeaves[leafNo].GAI;

                            double previousExposedLAI = AllLeaves[leafNo].maizeLeaf.area * AllLeaves[leafNo].maizeLeaf.fracPopn * plantDensity / 1000000;

                            if (ttElapsed + dltTT > AllLeaves[leafNo].maizeLeaf.startExpTT &&
                               ttElapsed < AllLeaves[leafNo].maizeLeaf.fullyExpTT) //in growing period
                            {

                                // some mucking around to take part days into account
                                double growthTT = Math.Min(dltTT, ttElapsed + dltTT - AllLeaves[leafNo].maizeLeaf.startExpTT);
                                growthTT = Math.Min(growthTT, AllLeaves[leafNo].maizeLeaf.fullyExpTT - ttElapsed);

                                double ttLER;
                                ttLER = divide(LER * AllLeaves[leafNo].maizeLeaf.LERCoef, dltTT);      // expansion rate in mm/oCd   (LER in mm/day)

                                double dltLength = growthTT * ttLER;      // mm
                                AllLeaves[leafNo].maizeLeaf.length += dltLength;

                                AllLeaves[leafNo].maizeLeaf.width = AllLeaves[leafNo].maizeLeaf.potentialWidth;
                                AllLeaves[leafNo].maizeLeaf.CalcArea();
                            }

                            maizelaistate.WaterLimitedPotDeltaAIList[leafNo] = Math.Max(0, (AllLeaves[leafNo].maizeLeaf.area * AllLeaves[leafNo].maizeLeaf.fracPopn * plantDensity / 1000000) - previousLAI);//m²/m²
                            dltLERlai += maizelaistate.WaterLimitedPotDeltaAIList[leafNo];


                            // calculate exposed(green) leaf
                            // if before tip appearance then 0
                            // else calculate the fraction we are between tip and ligule
                            // and use that as a fraction of the current expanded area
                            double exposedFraction = divide((ttElapsed + dltTT - AllLeaves[leafNo].maizeLeaf.tipTT),
                               (AllLeaves[leafNo].maizeLeaf.liguleTT - AllLeaves[leafNo].maizeLeaf.tipTT));
                            exposedFraction = Math.Max(Math.Min(exposedFraction, 1), 0);

                            AllLeaves[leafNo].maizeLeaf.exposedArea = exposedFraction * AllLeaves[leafNo].maizeLeaf.area;//mm²
                            maizelaistate.WaterLimitedPotExposedDeltaAIList[leafNo] = Math.Max(0, (AllLeaves[leafNo].maizeLeaf.exposedArea * AllLeaves[leafNo].maizeLeaf.fracPopn * plantDensity / 1000000) - previousExposedLAI);//m²/m²
                            exposedDltLERlai += maizelaistate.WaterLimitedPotExposedDeltaAIList[leafNo];
                        }

                        exposedDltLERlai = Math.Max(exposedDltLERlai, 0.0);
                        dltLERlai = Math.Max(dltLERlai, 0.0);

                        return dltLERlai;
                    }
                    else
                    {
                        for (int leafNo = 0; leafNo < AllLeaves.Count; leafNo++)
                        {
                            maizelaistate.WaterLimitedPotDeltaAIList[leafNo] = 0;
                            maizelaistate.WaterLimitedPotExposedDeltaAIList[leafNo] = 0;
                        }

                        dltLERlai = 0;
                        exposedDltLERlai = 0;
                        return dltLERlai;
                    }
                }

                private double divide(double dividend, double divisor, double default_value = 0)
                {
                    //Constant Values
                    double LARGEST = 1.0e30;    //largest acceptable no. for quotient
                    double SMALLEST = 1.0e-30;  //smallest acceptable no. for quotient
                    double nought = 0.0;

                    //Local Varialbes
                    double quotient;

                    //Implementation
                    if (isEqual(dividend, 0.0))      //multiplying by 0
                    {
                        quotient = 0.0;
                    }
                    else if (isEqual(divisor, 0.0))  //dividing by 0
                    {
                        quotient = default_value;
                    }
                    else if (Math.Abs(divisor) < 1.0)            //possible overflow
                    {
                        if (Math.Abs(dividend) > Math.Abs(LARGEST * divisor)) //overflow
                        {
                            quotient = default_value;
                        }
                        else
                        {
                            quotient = dividend / divisor;          //ok
                        }
                    }
                    else if (Math.Abs(divisor) > 1.0)             //possible underflow
                    {
                        if (Math.Abs(dividend) < Math.Abs(SMALLEST * divisor))    //underflow
                        {
                            quotient = nought;
                        }
                        else
                        {
                            quotient = dividend / divisor;                //ok
                        }
                    }
                    else
                    {
                        quotient = dividend / divisor;                   //ok
                    }
                    return quotient;
                }
                private bool isEqual(double A, double B) { return (Math.Abs(A - B) < 1.0E-6); }

                private double dltLERlai;
                private double exposedDltLERlai;
				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}
