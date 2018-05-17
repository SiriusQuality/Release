

 //Author:pierre martre pierre.martre@inra.fr
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


//To make this project compile please add the reference to assembly: SiriusQuality-Model, Version=2.0.6192.23847, Culture=neutral, PublicKeyToken=null
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
	///Class WheatLAI
    /// 
    /// </summary>
	public class WheatLAI : IStrategySiriusQualityWheatLAI
	{

	#region Constructor

			public WheatLAI()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new CompositeStrategyVarInfo(_leafexpansiondroughtfactor,"LowerFPAWexp");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new CompositeStrategyVarInfo(_leafexpansiondroughtfactor,"UpperFPAWexp");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new CompositeStrategyVarInfo(_leafexpansiondroughtfactor,"MaxDSF");
				 _parameters0_0.Add(v3);
				VarInfo v4 = new CompositeStrategyVarInfo(_leafexpansiondroughtfactor,"LowerFPAWsen");
				 _parameters0_0.Add(v4);
				VarInfo v5 = new CompositeStrategyVarInfo(_leafexpansiondroughtfactor,"UpperFPAWsen");
				 _parameters0_0.Add(v5);
				VarInfo v6 = new CompositeStrategyVarInfo(_leafexpansiondroughtfactor,"UpperVPD");
				 _parameters0_0.Add(v6);
				VarInfo v7 = new CompositeStrategyVarInfo(_leafexpansiondroughtfactor,"LowerVPD");
				 _parameters0_0.Add(v7);
				VarInfo v8 = new CompositeStrategyVarInfo(_maximumpotentialfinallai,"NLL");
				 _parameters0_0.Add(v8);
				VarInfo v9 = new CompositeStrategyVarInfo(_maximumpotentialfinallai,"AreaPL");
				 _parameters0_0.Add(v9);
				VarInfo v10 = new CompositeStrategyVarInfo(_maximumpotentialfinallai,"RatioFLPL");
				 _parameters0_0.Add(v10);
				VarInfo v11 = new CompositeStrategyVarInfo(_maximumpotentialfinallai,"AreaSL");
				 _parameters0_0.Add(v11);
				VarInfo v12 = new CompositeStrategyVarInfo(_maximumpotentialfinallai,"AreaSS");
				 _parameters0_0.Add(v12);
				VarInfo v13 = new CompositeStrategyVarInfo(_waterlimitedleafexpansion,"PexpL");
				 _parameters0_0.Add(v13);
				VarInfo v14 = new CompositeStrategyVarInfo(_waterlimitedleafexpansion,"SLNmin");
				 _parameters0_0.Add(v14);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				mo0_0.Outputs=_outputs0_0;
				//Associated strategies
				List<string> lAssStrat0_0 = new List<string>();
				lAssStrat0_0.Add(typeof(SiriusQualityWheatLAI.Strategies.LeafExpansionDroughtFactor).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityWheatLAI.Strategies.MaximumPotentialFinalLAI).FullName);
				lAssStrat0_0.Add(typeof(SiriusQualityWheatLAI.Strategies.WaterLimitedLeafExpansion).FullName);
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

			

			// Getter and setters for the value of the parameters of a composite strategy
			
			public Double LowerFPAWexp
			{ 
				get {
						return _leafexpansiondroughtfactor.LowerFPAWexp ;
				}
				set {
						_leafexpansiondroughtfactor.LowerFPAWexp=value;
				}
			}
			public Double UpperFPAWexp
			{ 
				get {
						return _leafexpansiondroughtfactor.UpperFPAWexp ;
				}
				set {
						_leafexpansiondroughtfactor.UpperFPAWexp=value;
				}
			}
			public Double MaxDSF
			{ 
				get {
						return _leafexpansiondroughtfactor.MaxDSF ;
				}
				set {
						_leafexpansiondroughtfactor.MaxDSF=value;
				}
			}
			public Double LowerFPAWsen
			{ 
				get {
						return _leafexpansiondroughtfactor.LowerFPAWsen ;
				}
				set {
						_leafexpansiondroughtfactor.LowerFPAWsen=value;
				}
			}
			public Double UpperFPAWsen
			{ 
				get {
						return _leafexpansiondroughtfactor.UpperFPAWsen ;
				}
				set {
						_leafexpansiondroughtfactor.UpperFPAWsen=value;
				}
			}
			public Double UpperVPD
			{ 
				get {
						return _leafexpansiondroughtfactor.UpperVPD ;
				}
				set {
						_leafexpansiondroughtfactor.UpperVPD=value;
				}
			}
			public Double LowerVPD
			{ 
				get {
						return _leafexpansiondroughtfactor.LowerVPD ;
				}
				set {
						_leafexpansiondroughtfactor.LowerVPD=value;
				}
			}
			public Double NLL
			{ 
				get {
						return _maximumpotentialfinallai.NLL ;
				}
				set {
						_maximumpotentialfinallai.NLL=value;
				}
			}
			public Double AreaPL
			{ 
				get {
						return _maximumpotentialfinallai.AreaPL ;
				}
				set {
						_maximumpotentialfinallai.AreaPL=value;
				}
			}
			public Double RatioFLPL
			{ 
				get {
						return _maximumpotentialfinallai.RatioFLPL ;
				}
				set {
						_maximumpotentialfinallai.RatioFLPL=value;
				}
			}
			public Double AreaSL
			{ 
				get {
						return _maximumpotentialfinallai.AreaSL ;
				}
				set {
						_maximumpotentialfinallai.AreaSL=value;
				}
			}
			public Double AreaSS
			{ 
				get {
						return _maximumpotentialfinallai.AreaSS ;
				}
				set {
						_maximumpotentialfinallai.AreaSS=value;
				}
			}
			public Double PexpL
			{ 
				get {
						return _waterlimitedleafexpansion.PexpL ;
				}
				set {
						_waterlimitedleafexpansion.PexpL=value;
				}
			}
			public Double SLNmin
			{ 
				get {
						return _waterlimitedleafexpansion.SLNmin ;
				}
				set {
						_waterlimitedleafexpansion.SLNmin=value;
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
				
					_leafexpansiondroughtfactor.SetParametersDefaultValue();
					_maximumpotentialfinallai.SetParametersDefaultValue();
					_waterlimitedleafexpansion.SetParametersDefaultValue(); 

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
				///LowerFPAWexp VarInfo definition
				/// </summary>
				public static VarInfo LowerFPAWexpVarInfo
				{
					get { return SiriusQualityWheatLAI.Strategies.LeafExpansionDroughtFactor.LowerFPAWexpVarInfo; }
				}
				/// <summary> 
				///UpperFPAWexp VarInfo definition
				/// </summary>
				public static VarInfo UpperFPAWexpVarInfo
				{
					get { return SiriusQualityWheatLAI.Strategies.LeafExpansionDroughtFactor.UpperFPAWexpVarInfo; }
				}
				/// <summary> 
				///MaxDSF VarInfo definition
				/// </summary>
				public static VarInfo MaxDSFVarInfo
				{
					get { return SiriusQualityWheatLAI.Strategies.LeafExpansionDroughtFactor.MaxDSFVarInfo; }
				}
				/// <summary> 
				///LowerFPAWsen VarInfo definition
				/// </summary>
				public static VarInfo LowerFPAWsenVarInfo
				{
					get { return SiriusQualityWheatLAI.Strategies.LeafExpansionDroughtFactor.LowerFPAWsenVarInfo; }
				}
				/// <summary> 
				///UpperFPAWsen VarInfo definition
				/// </summary>
				public static VarInfo UpperFPAWsenVarInfo
				{
					get { return SiriusQualityWheatLAI.Strategies.LeafExpansionDroughtFactor.UpperFPAWsenVarInfo; }
				}
				/// <summary> 
				///UpperVPD VarInfo definition
				/// </summary>
				public static VarInfo UpperVPDVarInfo
				{
					get { return SiriusQualityWheatLAI.Strategies.LeafExpansionDroughtFactor.UpperVPDVarInfo; }
				}
				/// <summary> 
				///LowerVPD VarInfo definition
				/// </summary>
				public static VarInfo LowerVPDVarInfo
				{
					get { return SiriusQualityWheatLAI.Strategies.LeafExpansionDroughtFactor.LowerVPDVarInfo; }
				}
				/// <summary> 
				///NLL VarInfo definition
				/// </summary>
				public static VarInfo NLLVarInfo
				{
					get { return SiriusQualityWheatLAI.Strategies.MaximumPotentialFinalLAI.NLLVarInfo; }
				}
				/// <summary> 
				///AreaPL VarInfo definition
				/// </summary>
				public static VarInfo AreaPLVarInfo
				{
					get { return SiriusQualityWheatLAI.Strategies.MaximumPotentialFinalLAI.AreaPLVarInfo; }
				}
				/// <summary> 
				///RatioFLPL VarInfo definition
				/// </summary>
				public static VarInfo RatioFLPLVarInfo
				{
					get { return SiriusQualityWheatLAI.Strategies.MaximumPotentialFinalLAI.RatioFLPLVarInfo; }
				}
				/// <summary> 
				///AreaSL VarInfo definition
				/// </summary>
				public static VarInfo AreaSLVarInfo
				{
					get { return SiriusQualityWheatLAI.Strategies.MaximumPotentialFinalLAI.AreaSLVarInfo; }
				}
				/// <summary> 
				///AreaSS VarInfo definition
				/// </summary>
				public static VarInfo AreaSSVarInfo
				{
					get { return SiriusQualityWheatLAI.Strategies.MaximumPotentialFinalLAI.AreaSSVarInfo; }
				}
				/// <summary> 
				///PexpL VarInfo definition
				/// </summary>
				public static VarInfo PexpLVarInfo
				{
					get { return SiriusQualityWheatLAI.Strategies.WaterLimitedLeafExpansion.PexpLVarInfo; }
				}
				/// <summary> 
				///SLNmin VarInfo definition
				/// </summary>
				public static VarInfo SLNminVarInfo
				{
					get { return SiriusQualityWheatLAI.Strategies.WaterLimitedLeafExpansion.SLNminVarInfo; }
				}			

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
					
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					

					
					string ret = "";
					 ret += _leafexpansiondroughtfactor.TestPostConditions(wheatlaistate, "strategy SiriusQualityWheatLAI.Strategies.LeafExpansionDroughtFactor");
					 ret += _maximumpotentialfinallai.TestPostConditions(wheatlaistate, "strategy SiriusQualityWheatLAI.Strategies.MaximumPotentialFinalLAI");
					 ret += _waterlimitedleafexpansion.TestPostConditions(wheatlaistate, "strategy SiriusQualityWheatLAI.Strategies.WaterLimitedLeafExpansion");
					if (ret != "") { pre.TestsOut(ret, true, "   postconditions tests of associated classes"); }

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
					

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					

					
					string ret = "";
					 ret += _leafexpansiondroughtfactor.TestPreConditions(wheatlaistate, "strategy SiriusQualityWheatLAI.Strategies.LeafExpansionDroughtFactor");
					 ret += _maximumpotentialfinallai.TestPreConditions(wheatlaistate, "strategy SiriusQualityWheatLAI.Strategies.MaximumPotentialFinalLAI");
					 ret += _waterlimitedleafexpansion.TestPreConditions(wheatlaistate, "strategy SiriusQualityWheatLAI.Strategies.WaterLimitedLeafExpansion");
					if (ret != "") { pre.TestsOut(ret, true, "   preconditions tests of associated classes"); }

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

            public void Estimate(SiriusQualityWheatLAI.WheatLAIState wheatlaistate)
            { }
		 	/// <summary>
			/// Run the strategy to calculate the outputs. In case of error during the execution, the preconditions tests are executed.
			/// </summary>
            public void Estimate(SiriusQualityWheatLAI.WheatLAIState wheatlaistate, List<LeafLayer> All)
			{
				try
				{
					CalculateModel(wheatlaistate, All);

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
				
					EstimateOfAssociatedClasses(wheatlaistate, All);

				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section1
				//Code written below will not be overwritten by a future code generation

        

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				
			#region Composite class: associations

			//Declaration of the associated strategies
			SiriusQualityWheatLAI.Strategies.LeafExpansionDroughtFactor _leafexpansiondroughtfactor = new SiriusQualityWheatLAI.Strategies.LeafExpansionDroughtFactor();
			SiriusQualityWheatLAI.Strategies.MaximumPotentialFinalLAI _maximumpotentialfinallai = new SiriusQualityWheatLAI.Strategies.MaximumPotentialFinalLAI();
			SiriusQualityWheatLAI.Strategies.WaterLimitedLeafExpansion _waterlimitedleafexpansion = new SiriusQualityWheatLAI.Strategies.WaterLimitedLeafExpansion();

			//Call of the associated strategies
            private void EstimateOfAssociatedClasses(SiriusQualityWheatLAI.WheatLAIState wheatlaistate, List<LeafLayer> All)
            {
                _leafexpansiondroughtfactor.Estimate(wheatlaistate);
                _maximumpotentialfinallai.Estimate(wheatlaistate);						
				_waterlimitedleafexpansion.Estimate(wheatlaistate,All);
			}

			#endregion


	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation

                /// <summary>
                /// copy constructor. We only need to copy the parameters (the strategies being stateless)
                /// </summary> 
            public WheatLAI(WheatLAI toCopy)
                : this()
                {
                    AreaPL = toCopy.AreaPL;
                    AreaSL = toCopy.AreaSL;
                    AreaSS = toCopy.AreaSS;
                    LowerFPAWexp = toCopy.LowerFPAWexp;
                    LowerFPAWsen = toCopy.LowerFPAWsen;
                    LowerVPD = toCopy.LowerVPD;
                    MaxDSF = toCopy.MaxDSF;
                    NLL = toCopy.NLL;
                    RatioFLPL = toCopy.RatioFLPL;
                    UpperFPAWexp = toCopy.UpperFPAWexp;
                    UpperFPAWsen = toCopy.UpperFPAWsen;
                    UpperVPD = toCopy.UpperVPD;
                    SLNmin = toCopy.SLNmin;
                    PexpL = toCopy.PexpL;



                }
				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}
