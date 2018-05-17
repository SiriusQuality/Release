

 //Author:pierre martre pierre.martre@inra.fr
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


using SiriusQualityMaizeLAI;


//To make this project compile please add the reference to assembly: SiriusQuality-Model, Version=2.0.6192.30021, Culture=neutral, PublicKeyToken=null
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
	///Class calcLER
    /// 
    /// </summary>
	public class calcLER : IStrategySiriusQualityMaizeLAI
	{

	#region Constructor

			public calcLER()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 4.25;
				 v1.Description = "potential growth of leaf 6";
				 v1.Id = 0;
				 v1.MaxValue = 10;
				 v1.MinValue = -10;
				 v1.Name = "LERa";
				 v1.Size = 1;
				 v1.Units = "mm/dd";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new VarInfo();
				 v2.DefaultValue = -1.15;
				 v2.Description = "effect of vapor pressure deficit on leaf 6 elongation";
				 v2.Id = 0;
				 v2.MaxValue = 10;
				 v2.MinValue = -10;
				 v2.Name = "LERb";
				 v2.Size = 1;
				 v2.Units = "mm/[dd.kPa]";
				 v2.URL = "";
				 v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new VarInfo();
				 v3.DefaultValue = 3.69;
				 v3.Description = "effect of soil water deficit on leaf 6 elongation";
				 v3.Id = 0;
				 v3.MaxValue = 10;
				 v3.MinValue = -10;
				 v3.Name = "LERc";
				 v3.Size = 1;
				 v3.Units = "mm/[dd.Bars]";
				 v3.URL = "";
				 v3.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v3.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v3);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityMaizeLAI.MaizeLAIState);
				pd1.PropertyName = "deltaTTCanopyHourly";
				pd1.PropertyType = (( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.deltaTTCanopyHourly)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.deltaTTCanopyHourly);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityMaizeLAI.MaizeLAIState);
				pd2.PropertyName = "FPAW";
				pd2.PropertyType = (( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.FPAW)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.FPAW);
				_inputs0_0.Add(pd2);
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityMaizeLAI.MaizeLAIState);
				pd3.PropertyName = "VPDeq";
				pd3.PropertyType = (( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.VPDeq)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.VPDeq);
				_inputs0_0.Add(pd3);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityMaizeLAI.MaizeLAIState);
				pd4.PropertyName = "LER";
				pd4.PropertyType =  (( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.LER)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =(  SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.LER);
				_outputs0_0.Add(pd4);
				PropertyDescription pd5 = new PropertyDescription();
				pd5.DomainClassType = typeof(SiriusQualityMaizeLAI.MaizeLAIState);
				pd5.PropertyName = "hLER";
				pd5.PropertyType =  (( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.hLER)).ValueType.TypeForCurrentValue;
				pd5.PropertyVarInfo =(  SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.hLER);
				_outputs0_0.Add(pd5);
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
				return new List<Type>() {  typeof(SiriusQualityMaizeLAI.MaizeLAIState) };
			}

	#endregion

    #region Instances of the parameters
			
			// Getter and setters for the value of the parameters of the strategy. The actual parameters are stored into the ModelingOptionsManager of the strategy.

			
			public Double LERa
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("LERa");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'LERa' not found (or found null) in strategy 'calcLER'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("LERa");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'LERa' not found in strategy 'calcLER'");
				}
			}
			public Double LERb
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("LERb");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'LERb' not found (or found null) in strategy 'calcLER'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("LERb");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'LERb' not found in strategy 'calcLER'");
				}
			}
			public Double LERc
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("LERc");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'LERc' not found (or found null) in strategy 'calcLER'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("LERc");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'LERc' not found in strategy 'calcLER'");
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
                LERaVarInfo.Name = "LERa";
				LERaVarInfo.Description =" potential growth of leaf 6";
				LERaVarInfo.MaxValue = 10;
				LERaVarInfo.MinValue = -10;
				LERaVarInfo.DefaultValue = 4.25;
				LERaVarInfo.Units = "mm/dd";
				LERaVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				LERbVarInfo.Name = "LERb";
				LERbVarInfo.Description =" effect of vapor pressure deficit on leaf 6 elongation";
				LERbVarInfo.MaxValue = 10;
				LERbVarInfo.MinValue = -10;
				LERbVarInfo.DefaultValue = -1.15;
				LERbVarInfo.Units = "mm/[dd.kPa]";
				LERbVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				LERcVarInfo.Name = "LERc";
				LERcVarInfo.Description =" effect of soil water deficit on leaf 6 elongation";
				LERcVarInfo.MaxValue = 10;
				LERcVarInfo.MinValue = -10;
				LERcVarInfo.DefaultValue = 3.69;
				LERcVarInfo.Units = "mm/[dd.Bars]";
				LERcVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _LERaVarInfo= new VarInfo();
				/// <summary> 
				///LERa VarInfo definition
				/// </summary>
				public static VarInfo LERaVarInfo
				{
					get { return _LERaVarInfo; }
				}
				private static VarInfo _LERbVarInfo= new VarInfo();
				/// <summary> 
				///LERb VarInfo definition
				/// </summary>
				public static VarInfo LERbVarInfo
				{
					get { return _LERbVarInfo; }
				}
				private static VarInfo _LERcVarInfo= new VarInfo();
				/// <summary> 
				///LERc VarInfo definition
				/// </summary>
				public static VarInfo LERcVarInfo
				{
					get { return _LERcVarInfo; }
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
					
					SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.LER.CurrentValue=maizelaistate.LER;
					SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.hLER.CurrentValue=maizelaistate.hLER;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.LER);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.LER.ValueType)){prc.AddCondition(r4);}
					RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.hLER);
					if(r5.ApplicableVarInfoValueTypes.Contains( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.hLER.ValueType)){prc.AddCondition(r5);}

					

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
					
					SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.deltaTTCanopyHourly.CurrentValue=maizelaistate.deltaTTCanopyHourly;
					SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.FPAW.CurrentValue=maizelaistate.FPAW;
					SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.VPDeq.CurrentValue=maizelaistate.VPDeq;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.deltaTTCanopyHourly);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.deltaTTCanopyHourly.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.FPAW);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.FPAW.ValueType)){prc.AddCondition(r2);}
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.VPDeq);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityMaizeLAI.MaizeLAIStateVarInfo.VPDeq.ValueType)){prc.AddCondition(r3);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("LERa")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("LERb")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("LERc")));

					

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

		 	/// <summary>
			/// Run the strategy to calculate the outputs. In case of error during the execution, the preconditions tests are executed.
			/// </summary>
			public void Estimate(SiriusQualityMaizeLAI.MaizeLAIState maizelaistate)
			{
				try
				{
					CalculateModel(maizelaistate);

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

		

			private void CalculateModel(SiriusQualityMaizeLAI.MaizeLAIState maizelaistate)
			{				
				

				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section1
				//Code written below will not be overwritten by a future code generation

                // calculate hLER
                CalcHLER(maizelaistate);

                double dailyLER = 0;
                for (int i = 0; i < 24; i++)
                {
                    dailyLER += maizelaistate.hLER[i];
                }

                maizelaistate.LER = dailyLER / 24.0;
        

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation
                //------------------------------------------------------------------------------------------------
                //-----------  calculate Hourly Leaf Extension Rate
                //------------------------------------------------------------------------------------------------
                private void CalcHLER(SiriusQualityMaizeLAI.MaizeLAIState maizelaistate)
                {
                    //VPDairLeaf is in kPa
                    for (int i = 0; i < 24; i++)
                        maizelaistate.hLER[i] = (Math.Max(0.0, maizetempResponse(maizelaistate.deltaTTCanopyHourly[i]) * (LERa + LERb * maizelaistate.VPDeq[i] + LERc * CalcPsi(maizelaistate.FPAW))));
                }

                private double maizetempResponse(double temperature)
                {
                    if (temperature < 0)
                    {
                        return 0;
                    }
                    else
                    {
                        if (temperature <= 18)
                        {
                            return (10.0 / 18.0 * temperature);
                        }
                        else
                        {
                            if (temperature <= 34)
                            {
                                return temperature - 8;
                            }
                            else
                            {
                                if (temperature < 44)
                                {
                                    return -2.6 * temperature + 114.4;
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                        }
                    }
                }
                private double CalcPsi(double ftsw)
                {
                    if (ftsw < 0.001) return -1.5;
                    return Math.Min(-0.1, -0.0578 + 0.246 * Math.Log(ftsw));
                }

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}
