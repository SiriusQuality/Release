

 //Author:pierre.martre pierre.martre@inra.fr
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


using SiriusQualityWheatLAI;
using SiriusModel.Model.CropModel.Phytomers;


//To make this project compile please add the reference to assembly: SiriusQuality-Model, Version=2.0.6192.23234, Culture=neutral, PublicKeyToken=null
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

namespace SiriusQualityWheatLAI.Strategies
{

	/// <summary>
	///Class WaterLimitedLeafExpansion
    /// 
    /// </summary>
	public class WaterLimitedLeafExpansion : IStrategySiriusQualityWheatLAI
	{

	#region Constructor

			public WaterLimitedLeafExpansion()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 0;
				 v1.Description = "Phyllochronic duration of leaf lamina expansion";
				 v1.Id = 0;
				 v1.MaxValue = 0;
				 v1.MinValue = 0;
				 v1.Name = "PexpL";
				 v1.Size = 1;
				 v1.Units = "dimensionless";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new VarInfo();
				 v2.DefaultValue = 0;
				 v2.Description = "Specific leaf N at which LUE is null";
				 v2.Id = 0;
				 v2.MaxValue = 0;
				 v2.MinValue = 0;
				 v2.Name = "SLNmin";
				 v2.Size = 1;
				 v2.Units = "g(N)/m²(leaf)";
				 v2.URL = "";
				 v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v2);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd1.PropertyName = "cumulTTShoot";
				pd1.PropertyType = (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.cumulTTShoot)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityWheatLAI.WheatLAIStateVarInfo.cumulTTShoot);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd2.PropertyName = "deltaTTShoot";
				pd2.PropertyType = (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.deltaTTShoot)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityWheatLAI.WheatLAIStateVarInfo.deltaTTShoot);
				_inputs0_0.Add(pd2);
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd3.PropertyName = "deltaTTSenescence";
				pd3.PropertyType = (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.deltaTTSenescence)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =( SiriusQualityWheatLAI.WheatLAIStateVarInfo.deltaTTSenescence);
				_inputs0_0.Add(pd3);
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd4.PropertyName = "DSF";
				pd4.PropertyType = (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.DSF)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =( SiriusQualityWheatLAI.WheatLAIStateVarInfo.DSF);
				_inputs0_0.Add(pd4);
				PropertyDescription pd5 = new PropertyDescription();
				pd5.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd5.PropertyName = "DEF";
				pd5.PropertyType = (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.DEF)).ValueType.TypeForCurrentValue;
				pd5.PropertyVarInfo =( SiriusQualityWheatLAI.WheatLAIStateVarInfo.DEF);
				_inputs0_0.Add(pd5);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd6 = new PropertyDescription();
				pd6.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd6.PropertyName = "incDeltaAreaLimitSF";
				pd6.PropertyType =  (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.incDeltaAreaLimitSF)).ValueType.TypeForCurrentValue;
				pd6.PropertyVarInfo =(  SiriusQualityWheatLAI.WheatLAIStateVarInfo.incDeltaAreaLimitSF);
				_outputs0_0.Add(pd6);
				PropertyDescription pd7 = new PropertyDescription();
				pd7.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd7.PropertyName = "WaterLimitedPotDeltaAIList";
				pd7.PropertyType =  (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.WaterLimitedPotDeltaAIList)).ValueType.TypeForCurrentValue;
				pd7.PropertyVarInfo =(  SiriusQualityWheatLAI.WheatLAIStateVarInfo.WaterLimitedPotDeltaAIList);
				_outputs0_0.Add(pd7);
				PropertyDescription pd8 = new PropertyDescription();
				pd8.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd8.PropertyName = "potentialIncDeltaArea";
				pd8.PropertyType =  (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.potentialIncDeltaArea)).ValueType.TypeForCurrentValue;
				pd8.PropertyVarInfo =(  SiriusQualityWheatLAI.WheatLAIStateVarInfo.potentialIncDeltaArea);
				_outputs0_0.Add(pd8);
				PropertyDescription pd9 = new PropertyDescription();
				/*pd9.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd9.PropertyName = "leafLayerState";
                pd9.PropertyType = ((SiriusQualityWheatLAI.WheatLAIStateVarInfo.leafStateList)).ValueType.TypeForCurrentValue;
                pd9.PropertyVarInfo = (SiriusQualityWheatLAI.WheatLAIStateVarInfo.leafStateList);
				_outputs0_0.Add(pd9);
				PropertyDescription pd10 = new PropertyDescription();
				pd10.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd10.PropertyName = "isPrematurelyDying";
				pd10.PropertyType =  (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.isPrematurelyDying)).ValueType.TypeForCurrentValue;
				pd10.PropertyVarInfo =(  SiriusQualityWheatLAI.WheatLAIStateVarInfo.isPrematurelyDying);
				_outputs0_0.Add(pd10);
				mo0_0.Outputs=_outputs0_0;
				//Associated strategies
				List<string> lAssStrat0_0 = new List<string>();
				mo0_0.AssociatedStrategies = lAssStrat0_0;
				//Adding the modeling options to the modeling options manager*/
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
				return new List<Type>() {  typeof(SiriusQualityWheatLAI.WheatLAIState) };
			}

	#endregion

    #region Instances of the parameters
			
			// Getter and setters for the value of the parameters of the strategy. The actual parameters are stored into the ModelingOptionsManager of the strategy.

			
			public Double PexpL
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("PexpL");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'PexpL' not found (or found null) in strategy 'WaterLimitedLeafExpansion'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("PexpL");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'PexpL' not found in strategy 'WaterLimitedLeafExpansion'");
				}
			}
			public Double SLNmin
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("SLNmin");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'SLNmin' not found (or found null) in strategy 'WaterLimitedLeafExpansion'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("SLNmin");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'SLNmin' not found in strategy 'WaterLimitedLeafExpansion'");
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
                PexpLVarInfo.Name = "PexpL";
				PexpLVarInfo.Description =" Phyllochronic duration of leaf lamina expansion";
				PexpLVarInfo.MaxValue = 0;
				PexpLVarInfo.MinValue = 0;
				PexpLVarInfo.DefaultValue = 0;
				PexpLVarInfo.Units = "dimensionless";
				PexpLVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				SLNminVarInfo.Name = "SLNmin";
				SLNminVarInfo.Description =" Specific leaf N at which LUE is null";
				SLNminVarInfo.MaxValue = 0;
				SLNminVarInfo.MinValue = 0;
				SLNminVarInfo.DefaultValue = 0;
				SLNminVarInfo.Units = "g(N)/m²(leaf)";
				SLNminVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _PexpLVarInfo= new VarInfo();
				/// <summary> 
				///PexpL VarInfo definition
				/// </summary>
				public static VarInfo PexpLVarInfo
				{
					get { return _PexpLVarInfo; }
				}
				private static VarInfo _SLNminVarInfo= new VarInfo();
				/// <summary> 
				///SLNmin VarInfo definition
				/// </summary>
				public static VarInfo SLNminVarInfo
				{
					get { return _SLNminVarInfo; }
				}					
			
			//Parameters static VarInfo list of the composite class
						

	#endregion
	
	#region pre/post conditions management		

		    /// <summary>
			/// Test to verify the postconditions
			/// </summary>
			public string TestPostConditions(SiriusQualityWheatLAI.WheatLAIState wheatlaistate, string callID)
			{
				try
				{
					//Set current values of the outputs to the static VarInfo representing the output properties of the domain classes				
					
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.incDeltaAreaLimitSF.CurrentValue=wheatlaistate.incDeltaAreaLimitSF;
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.WaterLimitedPotDeltaAIList.CurrentValue=wheatlaistate.WaterLimitedPotDeltaAIList;
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.potentialIncDeltaArea.CurrentValue=wheatlaistate.potentialIncDeltaArea;
                    //SiriusQualityWheatLAI.WheatLAIStateVarInfo.leafStateList.CurrentValue = wheatlaistate.leafStateList;
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.isPrematurelyDying.CurrentValue=wheatlaistate.isPrematurelyDying;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r6 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.incDeltaAreaLimitSF);
					if(r6.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.incDeltaAreaLimitSF.ValueType)){prc.AddCondition(r6);}
					RangeBasedCondition r7 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.WaterLimitedPotDeltaAIList);
					if(r7.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.WaterLimitedPotDeltaAIList.ValueType)){prc.AddCondition(r7);}
					RangeBasedCondition r8 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.potentialIncDeltaArea);
					if(r8.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.potentialIncDeltaArea.ValueType)){prc.AddCondition(r8);}
                   /* RangeBasedCondition r9 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.leafStateList);
                    if (r9.ApplicableVarInfoValueTypes.Contains(SiriusQualityWheatLAI.WheatLAIStateVarInfo.leafStateList.ValueType)) { prc.AddCondition(r9); }
					RangeBasedCondition r10 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.isPrematurelyDying);
					if(r10.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.isPrematurelyDying.ValueType)){prc.AddCondition(r10);}*/

					

					//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section4
					//Code written below will not be overwritten by a future code generation

        

					//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
					//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section4 

					//Get the evaluation of postconditions
					string postConditionsResult =pre.VerifyPostconditions(prc, callID);
					//if we have errors, send it to the configured output 
					if(!string.IsNullOrEmpty(postConditionsResult)) { pre.TestsOut(postConditionsResult, true, "PostConditions errors in component SiriusQualityWheatLAI.Strategies, strategy " + this.GetType().Name ); }
					return postConditionsResult;
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1001,	"Strategy: " + this.GetType().Name + " - Unhandled exception running post-conditions");

					string msg = "Component SiriusQualityWheatLAI.Strategies, " + this.GetType().Name + ": Unhandled exception running post-condition test. ";
					throw new Exception(msg, exception);
				}
			}

			/// <summary>
			/// Test to verify the preconditions
			/// </summary>
			public string TestPreConditions(SiriusQualityWheatLAI.WheatLAIState wheatlaistate, string callID)
			{
				try
				{
					//Set current values of the inputs to the static VarInfo representing the input properties of the domain classes				
					
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.cumulTTShoot.CurrentValue=wheatlaistate.cumulTTShoot;
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.deltaTTShoot.CurrentValue=wheatlaistate.deltaTTShoot;
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.deltaTTSenescence.CurrentValue=wheatlaistate.deltaTTSenescence;
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.DSF.CurrentValue=wheatlaistate.DSF;
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.DEF.CurrentValue=wheatlaistate.DEF;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.cumulTTShoot);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.cumulTTShoot.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.deltaTTShoot);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.deltaTTShoot.ValueType)){prc.AddCondition(r2);}
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.deltaTTSenescence);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.deltaTTSenescence.ValueType)){prc.AddCondition(r3);}
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.DSF);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.DSF.ValueType)){prc.AddCondition(r4);}
					RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.DEF);
					if(r5.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.DEF.ValueType)){prc.AddCondition(r5);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("PexpL")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("SLNmin")));

					

					//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section3
					//Code written below will not be overwritten by a future code generation

        

					//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
					//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section3 
								
					//Get the evaluation of preconditions;					
					string preConditionsResult =pre.VerifyPreconditions(prc, callID);
					//if we have errors, send it to the configured output 
					if(!string.IsNullOrEmpty(preConditionsResult)) { pre.TestsOut(preConditionsResult, true, "PreConditions errors in component SiriusQualityWheatLAI.Strategies, strategy " + this.GetType().Name ); }
					return preConditionsResult;
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//	TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1002,"Strategy: " + this.GetType().Name + " - Unhandled exception running pre-conditions");

					string msg = "Component SiriusQualityWheatLAI.Strategies, " + this.GetType().Name + ": Unhandled exception running pre-condition test. ";
					throw new Exception(msg, exception);
				}
			}

		
	#endregion
		


	#region Model

            public void Estimate(SiriusQualityWheatLAI.WheatLAIState wheatlaistate) { }
		 	/// <summary>
			/// Run the strategy to calculate the outputs. In case of error during the execution, the preconditions tests are executed.
			/// </summary>
            public void Estimate(SiriusQualityWheatLAI.WheatLAIState wheatlaistate, List<LeafLayer> All)
			{
				try
				{
					CalculateModel(wheatlaistate,All);

					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Verbose, 1005,"Strategy: " + this.GetType().Name + " - Model executed");
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1003,		"Strategy: " + this.GetType().Name + " - Unhandled exception running model");

					string msg = "Error in component SiriusQualityWheatLAI.Strategies, strategy: " + this.GetType().Name + ": Unhandled exception running model. "+exception.GetType().FullName+" - "+exception.Message;				
					throw new Exception(msg, exception);
				}
			}



            private void CalculateModel(SiriusQualityWheatLAI.WheatLAIState wheatlaistate, List<LeafLayer> All)
			{				
				

				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section1
				//Code written below will not be overwritten by a future code generation
                //update the size of the lists
                for (int i = 0; i < All.Count; i++)
                {
                    //if a new leaf has appeared
                    if (i >= wheatlaistate.WaterLimitedPotDeltaAIList.Count)
                    {
                        wheatlaistate.WaterLimitedPotDeltaAIList.Add(0);
                        wheatlaistate.leafStateList.Add(All[i].State);//State==growing
                        wheatlaistate.isPrematurelyDying.Add(0);
                        wheatlaistate.TTgroSheathList.Add(All[i].TTgroLamina * wheatlaistate.MaximumPotentialSheathAI[i] / wheatlaistate.MaximumPotentialLaminaeAI[i]);
                        wheatlaistate.TT.Add(wheatlaistate.cumulTTShoot - wheatlaistate.deltaTTShoot);   // pm 29 May 2013, replaced air temperature by canopy temperature
                    }
                }

                CalculateNewStates(wheatlaistate, All);

                // calculate potential delta area
                CalculatePotIncDeltaAI(wheatlaistate, All);
        

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation
            // #Andrea 26/11/2015 - included double deltaTTSenescence
            /// <summary>Calculate potential delta area index of the day (either growth or senescence)</summary>
            /// <param name="potentialIncDeltaArea">Potential delta area index increment of the day</param>
            private void CalculatePotIncDeltaAI(SiriusQualityWheatLAI.WheatLAIState wheatlaistate,
                List<LeafLayer> All)
            {

                wheatlaistate.potentialIncDeltaArea = 0;
                for (int i = 0; i < All.Count; i++)
                {
                    CalculateWaterLimitedPotentialDeltaArea(wheatlaistate,i, All);
                    var potDelta = wheatlaistate.WaterLimitedPotDeltaAIList[i];
                    if (potDelta > 0) wheatlaistate.potentialIncDeltaArea += potDelta;
                }

                wheatlaistate.incDeltaAreaLimitSF = wheatlaistate.potentialIncDeltaArea * wheatlaistate.DEF;

            }

            // #Andrea 26/11/2015 - Added deltaTTSenescence
            private void CalculateWaterLimitedPotentialDeltaArea(SiriusQualityWheatLAI.WheatLAIState wheatlaistate, int i,
                List<LeafLayer> All)
            {
                wheatlaistate.WaterLimitedPotDeltaAIList[i] = 0;

                if (wheatlaistate.leafStateList[i] >= LeafState.Senescing)
                //if ((IsPrematurelyDying && ! IsDeadBeforeEnd) || State >= LeafState.Senescing)
                {
                     wheatlaistate.WaterLimitedPotDeltaAIList[i] = Math.Max(-All[i].MaxAI * (wheatlaistate.deltaTTSenescence * wheatlaistate.DSF) / All[i].TTsen, -(All[i].getLaminaAreaIndex() + All[i].getSheathAreaIndex()));
                }
                else if (wheatlaistate.leafStateList[i] == LeafState.Growing && wheatlaistate.isPrematurelyDying[i]==0)
                {
                    if ((wheatlaistate.cumulTTShoot - All[i].TTem) < All[i].LayerPhyllochron * PexpL) //IsLeafLaminaeGrowing
                    {
                        wheatlaistate.WaterLimitedPotDeltaAIList[i] = Math.Min(wheatlaistate.MaximumPotentialLaminaeAI[i] * wheatlaistate.deltaTTShoot / All[i].TTgroLamina, Math.Max(0, wheatlaistate.MaximumPotentialLaminaeAI[i] - All[i].getLaminaAreaIndex()));
                    }
                    else if (wheatlaistate.TTgroSheathList[i] > 0)
                    {
                        wheatlaistate.WaterLimitedPotDeltaAIList[i] = Math.Min(wheatlaistate.MaximumPotentialSheathAI[i] * wheatlaistate.deltaTTShoot / wheatlaistate.TTgroSheathList[i], Math.Max(0, wheatlaistate.MaximumPotentialSheathAI[i] - All[i].getSheathAreaIndex()));
                    }
                }
            }

            // #Andrea 26/11/2015
            /// <summary>Update all leaf state </summary>
            private void CalculateNewStates(SiriusQualityWheatLAI.WheatLAIState wheatlaistate, List<LeafLayer> All)
            {
                for (int i = 0; i < All.Count; i++)
                {
                    var ll = All[i];
                    double TTgro = All[i].TTgroLamina + wheatlaistate.TTgroSheathList[i];
                    CalculateNewLeafLayerState(wheatlaistate, i, ll.GAI, ll.TTem, TTgro, ll.TTmat, ll.TTsen, ll.GetLeafLamina().SpecificN);//#Andrea 25/11/2015
                }
            }

            // #Andrea 26/11/2015
            private void CalculateNewLeafLayerState(SiriusQualityWheatLAI.WheatLAIState wheatlaistate, int leafIndex, double GAI, double TTem, double TTgro, double TTmat, double TTsen, double leafLaminaSpecificN)
            {
                switch (wheatlaistate.leafStateList[leafIndex])
                {
                    case LeafState.Mature:
                    case LeafState.Senescing:

                        //TT[leafIndex] += deltaTTShoot * DSF; break; //#Andrea 26/11/2015
                        wheatlaistate.TT[leafIndex] += wheatlaistate.deltaTTSenescence * wheatlaistate.DSF; break; // #Andrea 26/11/2015
                    default: wheatlaistate.TT[leafIndex] += wheatlaistate.deltaTTShoot; break;
                }
                if (GAI <= 0 && wheatlaistate.leafStateList[leafIndex] >= LeafState.Mature && wheatlaistate.leafStateList[leafIndex] != LeafState.Dead)
                {
                    wheatlaistate.leafStateList[leafIndex] = LeafState.Dead;
                }
                else if (GAI > 0)
                {
                    if (wheatlaistate.isPrematurelyDying[leafIndex]==0)
                    {
                        if (wheatlaistate.TT[leafIndex] <= TTem + TTgro)
                        {
                            wheatlaistate.leafStateList[leafIndex] = LeafState.Growing;
                        }
                        else if (wheatlaistate.TT[leafIndex] < TTem + TTgro + TTmat)
                        {
                            wheatlaistate.leafStateList[leafIndex] = LeafState.Mature;
                        }
                        else if (wheatlaistate.TT[leafIndex] < TTem + TTgro + TTmat + TTsen || GAI > 0)
                        {
                            wheatlaistate.leafStateList[leafIndex] = LeafState.Senescing;
                        }
                        else
                        {
                            wheatlaistate.leafStateList[leafIndex] = LeafState.Dead;
                        }
                        if (leafLaminaSpecificN <= SLNmin)
                        {
                            wheatlaistate.isPrematurelyDying[leafIndex] = 1;
                            wheatlaistate.leafStateList[leafIndex] = LeafState.Senescing;
                        }
                    }
                    else
                    {
                        wheatlaistate.leafStateList[leafIndex] = LeafState.Senescing;
                    }
                }
            }

           // private List<double> TTgroSheathList = new List<double>();

            /// <summary>list of Thermal times since emergence of this leaf Layer</summary>
           // private List<double> TT = new List<double>();

           /* public WaterLimitedLeafExpansion(WaterLimitedLeafExpansion toCopy)
            {
                List<double> TT = new List<double>(toCopy.TT);
                List<double> TTgroSheathList = new List<double>(toCopy.TTgroSheathList);

            }*/

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}
