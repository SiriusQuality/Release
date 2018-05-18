

 //Author:pierre martre pierre.martre@inra.fr
 //Institution:INRA
 //Author of revision: 
 //Date first release:14/12/2016
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
using SiriusQualityPhenology.Strategies;


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
	///Class MaximumPotentialFinalLAI
    /// 
    /// </summary>
	public class MaximumPotentialFinalLAI : IStrategySiriusQualityWheatLAI
	{

	#region Constructor

			public MaximumPotentialFinalLAI()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 0;
				 v1.Description = "Number of leaves produced after floral initiation";
				 v1.Id = 0;
				 v1.MaxValue = 0;
				 v1.MinValue = 0;
				 v1.Name = "NLL";
				 v1.Size = 1;
				 v1.Units = "leaf";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new VarInfo();
				 v2.DefaultValue = 0;
				 v2.Description = "Maximum potential surface area of the penultimate leaf lamina";
				 v2.Id = 0;
				 v2.MaxValue = 0;
				 v2.MinValue = 0;
				 v2.Name = "AreaPL";
				 v2.Size = 1;
				 v2.Units = "cm²/lamina";
				 v2.URL = "";
				 v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new VarInfo();
				 v3.DefaultValue = 0;
				 v3.Description = "Ratio of flag leaf to penultimate leaf lamina surface area";
				 v3.Id = 0;
				 v3.MaxValue = 0;
				 v3.MinValue = 0;
				 v3.Name = "RatioFLPL";
				 v3.Size = 1;
				 v3.Units = "dimensionless";
				 v3.URL = "";
				 v3.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v3.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v3);
				VarInfo v4 = new VarInfo();
				 v4.DefaultValue = 0;
				 v4.Description = "Potential surface area of the leaves produced before floral initiation";
				 v4.Id = 0;
				 v4.MaxValue = 0;
				 v4.MinValue = 0;
				 v4.Name = "AreaSL";
				 v4.Size = 1;
				 v4.Units = "cm²/lamina";
				 v4.URL = "";
				 v4.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v4.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v4);
				VarInfo v5 = new VarInfo();
				 v5.DefaultValue = 0;
				 v5.Description = "Potential surface area of the sheath of the leaves produced before floral initiation";
				 v5.Id = 0;
				 v5.MaxValue = 0;
				 v5.MinValue = 0;
				 v5.Name = "AreaSS";
				 v5.Size = 1;
				 v5.Units = "cm²/lamina";
				 v5.URL = "";
				 v5.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v5.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v5);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd1.PropertyName = "newLeafHasAppeared";
				pd1.PropertyType = (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.newLeafHasAppeared)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityWheatLAI.WheatLAIStateVarInfo.newLeafHasAppeared);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd2.PropertyName = "leafNumber";
				pd2.PropertyType = (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.leafNumber)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityWheatLAI.WheatLAIStateVarInfo.leafNumber);
				_inputs0_0.Add(pd2);
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd3.PropertyName = "roundedFinalLeafNumber";
				pd3.PropertyType = (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.roundedFinalLeafNumber)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =( SiriusQualityWheatLAI.WheatLAIStateVarInfo.roundedFinalLeafNumber);
				_inputs0_0.Add(pd3);
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd4.PropertyName = "finalLeafNumber";
				pd4.PropertyType = (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.finalLeafNumber)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =( SiriusQualityWheatLAI.WheatLAIStateVarInfo.finalLeafNumber);
				_inputs0_0.Add(pd4);
				PropertyDescription pd5 = new PropertyDescription();
				pd5.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd5.PropertyName = "leafTillerNumberArray";
				pd5.PropertyType = (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.leafTillerNumberArray)).ValueType.TypeForCurrentValue;
				pd5.PropertyVarInfo =( SiriusQualityWheatLAI.WheatLAIStateVarInfo.leafTillerNumberArray);
				_inputs0_0.Add(pd5);
				PropertyDescription pd6 = new PropertyDescription();
				pd6.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd6.PropertyName = "tilleringProfile";
				pd6.PropertyType = (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.tilleringProfile)).ValueType.TypeForCurrentValue;
				pd6.PropertyVarInfo =( SiriusQualityWheatLAI.WheatLAIStateVarInfo.tilleringProfile);
				_inputs0_0.Add(pd6);
				PropertyDescription pd7 = new PropertyDescription();
				pd7.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd7.PropertyName = "isSmallPhytomer";
				pd7.PropertyType = (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.isSmallPhytomer)).ValueType.TypeForCurrentValue;
				pd7.PropertyVarInfo =( SiriusQualityWheatLAI.WheatLAIStateVarInfo.isSmallPhytomer);
				_inputs0_0.Add(pd7);
				PropertyDescription pd8 = new PropertyDescription();
				pd8.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd8.PropertyName = "phytonum";
				pd8.PropertyType = (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.phytonum)).ValueType.TypeForCurrentValue;
				pd8.PropertyVarInfo =( SiriusQualityWheatLAI.WheatLAIStateVarInfo.phytonum);
				_inputs0_0.Add(pd8);
				PropertyDescription pd9 = new PropertyDescription();
				pd9.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd9.PropertyName = "index";
				pd9.PropertyType = (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.index)).ValueType.TypeForCurrentValue;
				pd9.PropertyVarInfo =( SiriusQualityWheatLAI.WheatLAIStateVarInfo.index);
				_inputs0_0.Add(pd9);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd10 = new PropertyDescription();
				pd10.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd10.PropertyName = "MaximumPotentialSheathAI";
				pd10.PropertyType =  (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.MaximumPotentialSheathAI)).ValueType.TypeForCurrentValue;
				pd10.PropertyVarInfo =(  SiriusQualityWheatLAI.WheatLAIStateVarInfo.MaximumPotentialSheathAI);
				_outputs0_0.Add(pd10);
				PropertyDescription pd11 = new PropertyDescription();
				pd11.DomainClassType = typeof(SiriusQualityWheatLAI.WheatLAIState);
				pd11.PropertyName = "MaximumPotentialLaminaeAI";
				pd11.PropertyType =  (( SiriusQualityWheatLAI.WheatLAIStateVarInfo.MaximumPotentialLaminaeAI)).ValueType.TypeForCurrentValue;
				pd11.PropertyVarInfo =(  SiriusQualityWheatLAI.WheatLAIStateVarInfo.MaximumPotentialLaminaeAI);
				_outputs0_0.Add(pd11);
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
				get { return "LAI"; }
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
				_pd.Add("Date", "14/12/2016");
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

			
			public Double NLL
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("NLL");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'NLL' not found (or found null) in strategy 'MaximumPotentialFinalLAI'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("NLL");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'NLL' not found in strategy 'MaximumPotentialFinalLAI'");
				}
			}
			public Double AreaPL
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("AreaPL");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'AreaPL' not found (or found null) in strategy 'MaximumPotentialFinalLAI'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("AreaPL");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'AreaPL' not found in strategy 'MaximumPotentialFinalLAI'");
				}
			}
			public Double RatioFLPL
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("RatioFLPL");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'RatioFLPL' not found (or found null) in strategy 'MaximumPotentialFinalLAI'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("RatioFLPL");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'RatioFLPL' not found in strategy 'MaximumPotentialFinalLAI'");
				}
			}
			public Double AreaSL
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("AreaSL");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'AreaSL' not found (or found null) in strategy 'MaximumPotentialFinalLAI'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("AreaSL");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'AreaSL' not found in strategy 'MaximumPotentialFinalLAI'");
				}
			}
			public Double AreaSS
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("AreaSS");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'AreaSS' not found (or found null) in strategy 'MaximumPotentialFinalLAI'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("AreaSS");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'AreaSS' not found in strategy 'MaximumPotentialFinalLAI'");
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
                NLLVarInfo.Name = "NLL";
				NLLVarInfo.Description =" Number of leaves produced after floral initiation";
				NLLVarInfo.MaxValue = 0;
				NLLVarInfo.MinValue = 0;
				NLLVarInfo.DefaultValue = 0;
				NLLVarInfo.Units = "leaf";
				NLLVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				AreaPLVarInfo.Name = "AreaPL";
				AreaPLVarInfo.Description =" Maximum potential surface area of the penultimate leaf lamina";
				AreaPLVarInfo.MaxValue = 0;
				AreaPLVarInfo.MinValue = 0;
				AreaPLVarInfo.DefaultValue = 0;
				AreaPLVarInfo.Units = "cm²/lamina";
				AreaPLVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				RatioFLPLVarInfo.Name = "RatioFLPL";
				RatioFLPLVarInfo.Description =" Ratio of flag leaf to penultimate leaf lamina surface area";
				RatioFLPLVarInfo.MaxValue = 0;
				RatioFLPLVarInfo.MinValue = 0;
				RatioFLPLVarInfo.DefaultValue = 0;
				RatioFLPLVarInfo.Units = "dimensionless";
				RatioFLPLVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				AreaSLVarInfo.Name = "AreaSL";
				AreaSLVarInfo.Description =" Potential surface area of the leaves produced before floral initiation";
				AreaSLVarInfo.MaxValue = 0;
				AreaSLVarInfo.MinValue = 0;
				AreaSLVarInfo.DefaultValue = 0;
				AreaSLVarInfo.Units = "cm²/lamina";
				AreaSLVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				AreaSSVarInfo.Name = "AreaSS";
				AreaSSVarInfo.Description =" Potential surface area of the sheath of the leaves produced before floral initiation";
				AreaSSVarInfo.MaxValue = 0;
				AreaSSVarInfo.MinValue = 0;
				AreaSSVarInfo.DefaultValue = 0;
				AreaSSVarInfo.Units = "cm²/lamina";
				AreaSSVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _NLLVarInfo= new VarInfo();
				/// <summary> 
				///NLL VarInfo definition
				/// </summary>
				public static VarInfo NLLVarInfo
				{
					get { return _NLLVarInfo; }
				}
				private static VarInfo _AreaPLVarInfo= new VarInfo();
				/// <summary> 
				///AreaPL VarInfo definition
				/// </summary>
				public static VarInfo AreaPLVarInfo
				{
					get { return _AreaPLVarInfo; }
				}
				private static VarInfo _RatioFLPLVarInfo= new VarInfo();
				/// <summary> 
				///RatioFLPL VarInfo definition
				/// </summary>
				public static VarInfo RatioFLPLVarInfo
				{
					get { return _RatioFLPLVarInfo; }
				}
				private static VarInfo _AreaSLVarInfo= new VarInfo();
				/// <summary> 
				///AreaSL VarInfo definition
				/// </summary>
				public static VarInfo AreaSLVarInfo
				{
					get { return _AreaSLVarInfo; }
				}
				private static VarInfo _AreaSSVarInfo= new VarInfo();
				/// <summary> 
				///AreaSS VarInfo definition
				/// </summary>
				public static VarInfo AreaSSVarInfo
				{
					get { return _AreaSSVarInfo; }
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
					
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.MaximumPotentialSheathAI.CurrentValue=wheatlaistate.MaximumPotentialSheathAI;
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.MaximumPotentialLaminaeAI.CurrentValue=wheatlaistate.MaximumPotentialLaminaeAI;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r10 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.MaximumPotentialSheathAI);
					if(r10.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.MaximumPotentialSheathAI.ValueType)){prc.AddCondition(r10);}
					RangeBasedCondition r11 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.MaximumPotentialLaminaeAI);
					if(r11.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.MaximumPotentialLaminaeAI.ValueType)){prc.AddCondition(r11);}

					

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
					
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.newLeafHasAppeared.CurrentValue=wheatlaistate.newLeafHasAppeared;
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.leafNumber.CurrentValue=wheatlaistate.leafNumber;
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.roundedFinalLeafNumber.CurrentValue=wheatlaistate.roundedFinalLeafNumber;
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.finalLeafNumber.CurrentValue=wheatlaistate.finalLeafNumber;
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.leafTillerNumberArray.CurrentValue=wheatlaistate.leafTillerNumberArray;
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.tilleringProfile.CurrentValue=wheatlaistate.tilleringProfile;
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.isSmallPhytomer.CurrentValue=wheatlaistate.isSmallPhytomer;
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.phytonum.CurrentValue=wheatlaistate.phytonum;
					SiriusQualityWheatLAI.WheatLAIStateVarInfo.index.CurrentValue=wheatlaistate.index;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.newLeafHasAppeared);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.newLeafHasAppeared.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.leafNumber);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.leafNumber.ValueType)){prc.AddCondition(r2);}
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.roundedFinalLeafNumber);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.roundedFinalLeafNumber.ValueType)){prc.AddCondition(r3);}
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.finalLeafNumber);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.finalLeafNumber.ValueType)){prc.AddCondition(r4);}
					RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.leafTillerNumberArray);
					if(r5.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.leafTillerNumberArray.ValueType)){prc.AddCondition(r5);}
					RangeBasedCondition r6 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.tilleringProfile);
					if(r6.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.tilleringProfile.ValueType)){prc.AddCondition(r6);}
					RangeBasedCondition r7 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.isSmallPhytomer);
					if(r7.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.isSmallPhytomer.ValueType)){prc.AddCondition(r7);}
					RangeBasedCondition r8 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.phytonum);
					if(r8.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.phytonum.ValueType)){prc.AddCondition(r8);}
					RangeBasedCondition r9 = new RangeBasedCondition(SiriusQualityWheatLAI.WheatLAIStateVarInfo.index);
					if(r9.ApplicableVarInfoValueTypes.Contains( SiriusQualityWheatLAI.WheatLAIStateVarInfo.index.ValueType)){prc.AddCondition(r9);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("NLL")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("AreaPL")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("RatioFLPL")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("AreaSL")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("AreaSS")));

					

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

		 	/// <summary>
			/// Run the strategy to calculate the outputs. In case of error during the execution, the preconditions tests are executed.
			/// </summary>
			public void Estimate(SiriusQualityWheatLAI.WheatLAIState wheatlaistate)
			{
				try
				{
					CalculateModel(wheatlaistate);

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

		

			private void CalculateModel(SiriusQualityWheatLAI.WheatLAIState wheatlaistate)
			{				
				

				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section1
				//Code written below will not be overwritten by a future code generation
                if (wheatlaistate.newLeafHasAppeared==1)
                {
                    wheatlaistate.MaximumPotentialLaminaeAI.Add(PotentialLaminaAreaIndex(wheatlaistate.roundedFinalLeafNumber, wheatlaistate.finalLeafNumber, wheatlaistate.leafNumber, wheatlaistate.isSmallPhytomer==1, wheatlaistate.phytonum, wheatlaistate.index, wheatlaistate.tilleringProfile, wheatlaistate.leafTillerNumberArray));
                    wheatlaistate.MaximumPotentialSheathAI.Add(PotentialSheathAreaIndex(wheatlaistate.roundedFinalLeafNumber, wheatlaistate.isSmallPhytomer==1, wheatlaistate.phytonum, wheatlaistate.index, wheatlaistate.tilleringProfile, wheatlaistate.leafTillerNumberArray));
                }
        

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation
            private double PotentialLaminaTillerAreaIndex(int tillerIndex, int roundedFinalNumber, double leafNumber, bool IsSmallPhytomer, int PhytoNum, int index, List<double> tilleringProfile)
            {
                double result;
                if (!IsSmallPhytomer)
                {
                    var n = roundedFinalNumber;
                    if (!IsFlagPhytomer(leafNumber, roundedFinalNumber, index))
                    {
                        result = ((n - PhytoNum - 1 + CalculateShootNumber.ShiftTiller(tillerIndex)) * ((-1) / (NLL - 1)) + 1) * AreaPL;
                    }
                    else result = RatioFLPL * ((n - (n - 1) - 1 + CalculateShootNumber.ShiftTiller(tillerIndex)) * ((-1) / (NLL - 1)) + 1) * AreaPL;
                }
                else
                {
                    result = AreaSL;
                }

                result *= tilleringProfile[tillerIndex] / 10000;  // 10000 = cm^2 -> m^2
                return result;
            }

            private double PotentialLaminaAreaIndex(int roundedFinalNumber, double finalLeafNumber, double leafNumber, bool IsSmallPhytomer, int PhytoNum, int index, List<double> tilleringProfile, List<double> leafTillerNumberArray)
            {
                double laminaeAreaIndex = 0;
                if (index >= leafTillerNumberArray.Count())
                {
                    throw new Exception("LeafTillerNumber is smaller than the number of leaves");
                }
                var leafTillerNumber = leafTillerNumberArray[index];
                for (var i = 0; i < leafTillerNumber; ++i)
                {
                    laminaeAreaIndex += PotentialLaminaTillerAreaIndex(i, roundedFinalNumber, leafNumber, IsSmallPhytomer, PhytoNum, index, tilleringProfile);
                }
                return laminaeAreaIndex;
            }

            private double PotentialSheathTillerAreaIndex(int tillerIndex, int roundedFinalLeafNumber, bool IsSmallPhytomer, int PhytoNum, List<double> tilleringProfile)
            {
                double result;
                if (!IsSmallPhytomer)
                {
                    //pm 25 July 2016: modify the model to calculate the surface area of leaf sheath between two succesive collars (light exposed)
                    //                 surface area).   
                    //  result = aSheath * Math.Pow(roundedFinalLeafNumber - PhytoNum - NLL + LeafLayer.ShiftTiller(tillerIndex), 2);
                    result = Math.Max(AreaSS, AreaPL * ((roundedFinalLeafNumber - PhytoNum + CalculateShootNumber.ShiftTiller(tillerIndex)) * (-1 / (NLL - 2)) + 1));
                }
                else if (PhytoNum > 1)
                {
                    result = 0;
                }
                else result = AreaSS;

                result *= tilleringProfile[tillerIndex] / 10000;   // 10000 = cm^2 -> m^2
                return result;
            }

            private double PotentialSheathAreaIndex(int roundedFinalLeafNumber, bool IsSmallPhytomer, int PhytoNum, int index, List<double> tilleringProfile, List<double> leafTillerNumberArray)
            {
                double sheathAreaIndex = 0;
                var leafTillerNumber = leafTillerNumberArray[index];
                for (var i = 0; i < leafTillerNumber; ++i)
                {
                    sheathAreaIndex += PotentialSheathTillerAreaIndex(i, roundedFinalLeafNumber, IsSmallPhytomer, PhytoNum, tilleringProfile);
                }
                return sheathAreaIndex;
            }

            /// <summary>Returns true if this phytomer is the flag leaf</summary>
            private bool IsFlagPhytomer(double leafNumber, int roundedFinalLeafNumber, int index)
            {
                if (leafNumber > 0)
                {
                    // number of phytomer is known
                    if (index == roundedFinalLeafNumber - 1)
                    {
                        return true;
                    }
                    return false;
                }
                // number of phytomer is unknown, it is not the flag leaf.
                return false;

            }

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}
