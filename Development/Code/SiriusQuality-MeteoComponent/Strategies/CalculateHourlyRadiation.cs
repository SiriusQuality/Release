

 //Author:pierre martre pierre.martre@inra.fr
 //Institution:INRA
 //Author of revision: 
 //Date first release:4/12/2017
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
	///Class CalculateHourlyRadiation
    /// 
    /// </summary>
	public class CalculateHourlyRadiation : IStrategySiriusQualityMeteo
	{

	#region Constructor

			public CalculateHourlyRadiation()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 0;
				 v1.Description = "latitude";
				 v1.Id = 0;
				 v1.MaxValue = 90;
				 v1.MinValue = -90;
				 v1.Name = "latitude";
				 v1.Size = 1;
				 v1.Units = "°";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v1);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd1.PropertyName = "dayOfYear";
				pd1.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.dayOfYear)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.dayOfYear);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd2.PropertyName = "solarRadiation";
				pd2.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.solarRadiation)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.solarRadiation);
				_inputs0_0.Add(pd2);
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd3.PropertyName = "solarDeclination";
				pd3.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.solarDeclination)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.solarDeclination);
				_inputs0_0.Add(pd3);
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd4.PropertyName = "dayLengthHorizonZero";
				pd4.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.dayLengthHorizonZero)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.dayLengthHorizonZero);
				_inputs0_0.Add(pd4);
				PropertyDescription pd5 = new PropertyDescription();
				pd5.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd5.PropertyName = "radTopAtmHorizonZero";
				pd5.PropertyType = (( SiriusQualityMeteo.MeteoStateVarInfo.radTopAtmHorizonZero)).ValueType.TypeForCurrentValue;
				pd5.PropertyVarInfo =( SiriusQualityMeteo.MeteoStateVarInfo.radTopAtmHorizonZero);
				_inputs0_0.Add(pd5);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd6 = new PropertyDescription();
				pd6.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd6.PropertyName = "hourlyRadiation";
				pd6.PropertyType =  (( SiriusQualityMeteo.MeteoStateVarInfo.hourlyRadiation)).ValueType.TypeForCurrentValue;
				pd6.PropertyVarInfo =(  SiriusQualityMeteo.MeteoStateVarInfo.hourlyRadiation);
				_outputs0_0.Add(pd6);
				PropertyDescription pd7 = new PropertyDescription();
				pd7.DomainClassType = typeof(SiriusQualityMeteo.MeteoState);
				pd7.PropertyName = "hourlySolarElevation";
				pd7.PropertyType =  (( SiriusQualityMeteo.MeteoStateVarInfo.hourlySolarElevation)).ValueType.TypeForCurrentValue;
				pd7.PropertyVarInfo =(  SiriusQualityMeteo.MeteoStateVarInfo.hourlySolarElevation);
				_outputs0_0.Add(pd7);
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
				get {  return "Weather"; }
			}

			/// <summary>
			/// Type of the model.
			/// </summary>
			public string ModelType
			{
				get { return "Radiation"; }
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
				_pd.Add("Date", "4/12/2017");
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

			
			public Double latitude
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("latitude");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'latitude' not found (or found null) in strategy 'CalculateHourlyRadiation'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("latitude");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'latitude' not found in strategy 'CalculateHourlyRadiation'");
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
                latitudeVarInfo.Name = "latitude";
				latitudeVarInfo.Description =" latitude";
				latitudeVarInfo.MaxValue = 90;
				latitudeVarInfo.MinValue = -90;
				latitudeVarInfo.DefaultValue = 0;
				latitudeVarInfo.Units = "°";
				latitudeVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _latitudeVarInfo= new VarInfo();
				/// <summary> 
				///latitude VarInfo definition
				/// </summary>
				public static VarInfo latitudeVarInfo
				{
					get { return _latitudeVarInfo; }
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
					
					SiriusQualityMeteo.MeteoStateVarInfo.hourlyRadiation.CurrentValue=meteostate.hourlyRadiation;
					SiriusQualityMeteo.MeteoStateVarInfo.hourlySolarElevation.CurrentValue=meteostate.hourlySolarElevation;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r6 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.hourlyRadiation);
					if(r6.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.hourlyRadiation.ValueType)){prc.AddCondition(r6);}
					RangeBasedCondition r7 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.hourlySolarElevation);
					if(r7.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.hourlySolarElevation.ValueType)){prc.AddCondition(r7);}

					

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
					
					SiriusQualityMeteo.MeteoStateVarInfo.dayOfYear.CurrentValue=meteostate.dayOfYear;
					SiriusQualityMeteo.MeteoStateVarInfo.solarRadiation.CurrentValue=meteostate.solarRadiation;
					SiriusQualityMeteo.MeteoStateVarInfo.solarDeclination.CurrentValue=meteostate.solarDeclination;
					SiriusQualityMeteo.MeteoStateVarInfo.dayLengthHorizonZero.CurrentValue=meteostate.dayLengthHorizonZero;
					SiriusQualityMeteo.MeteoStateVarInfo.radTopAtmHorizonZero.CurrentValue=meteostate.radTopAtmHorizonZero;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.dayOfYear);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.dayOfYear.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.solarRadiation);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.solarRadiation.ValueType)){prc.AddCondition(r2);}
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.solarDeclination);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.solarDeclination.ValueType)){prc.AddCondition(r3);}
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.dayLengthHorizonZero);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.dayLengthHorizonZero.ValueType)){prc.AddCondition(r4);}
					RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualityMeteo.MeteoStateVarInfo.radTopAtmHorizonZero);
					if(r5.ApplicableVarInfoValueTypes.Contains( SiriusQualityMeteo.MeteoStateVarInfo.radTopAtmHorizonZero.ValueType)){prc.AddCondition(r5);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("latitude")));

					

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
                for (int i = 0; i < 24; i++) meteostate.hourlyRadiation[i] = 0.0;

                // some constants

                double LatR = latitude * Math.PI / 180;
                // some calculations
                //double SolarDec = CalcSolarDeclination(meteostate.dayOfYear);
                double SolarDec = meteostate.solarDeclination;
                
                //double DayL = CalcDayLength(LatR, SolarDec);            // day length (radians)
                //double DayLH = (2.0 / 15.0 * DayL) * (180 / Math.PI);             // day length (hours)
                //Debug
                double DayL = meteostate.dayLengthHorizonZero;            // day length (radians)
                double DayLH = (2.0 / 15.0 * DayL) * (180 / Math.PI);             // day length (hours)
               //double DayLH = meteostate.dayLength; // day length (hours)
               // double DayL = meteostate.dayLength * (Math.PI / 24.0); // day length (radians)
                //Debug
                //double Solar = CalcSolarRadn(RATIO, DayL, LatR, SolarDec); // solar radiation
               //Debug
                double Solar = meteostate.radTopAtmHorizonZero;// solar radiation
                //double Solar = meteostate.radTopAtm;// solar radiation
                //Debug
                //do the radiation calculation zeroing at dawn

                double DuskDawnFract = (DayLH - (int)DayLH) / 2; //the remainder part of the hour at dusk and dawn
                double DawnTime = 12 - (DayLH / 2);

                //   DawnTime = (180 - RadToDeg(acos(-1 * tan(LatR) * tan(SolarDec)))) / 360 * 24; //Wikipedia ???

                //The first partial hour
               // meteostate.hourlyRadiation[(int)DawnTime] += (GlobalRadiation(DuskDawnFract / DayLH, LatR, SolarDec, DayLH, Solar)[0] * 3600 * DuskDawnFract);
                meteostate.hourlyRadiation[(int)DawnTime] = (GlobalRadiation(DuskDawnFract / DayLH, LatR, SolarDec, DayLH, Solar)[0] * 3600 * DuskDawnFract);
                meteostate.hourlySolarElevation[(int)DawnTime]=GlobalRadiation(DuskDawnFract / DayLH, LatR, SolarDec, DayLH, Solar)[2];
                //Add the next lot
                for (int i = 0; i < (int)(DayLH - 1); i++)
                {
                   // meteostate.hourlyRadiation[(int)DawnTime + i + 1] += (GlobalRadiation((DuskDawnFract / DayLH) + ((i + 1) * 1.0 / Math.Truncate(DayLH)), LatR, SolarDec, DayLH, Solar)[0] * 3600);
                    meteostate.hourlyRadiation[(int)DawnTime + i + 1] = (GlobalRadiation((DuskDawnFract / DayLH) + ((i + 1) * 1.0 / Math.Truncate(DayLH)), LatR, SolarDec, DayLH, Solar)[0] * 3600);
                    meteostate.hourlySolarElevation[(int)DawnTime + i + 1]=GlobalRadiation((DuskDawnFract / DayLH) + ((i + 1) * 1.0 / Math.Truncate(DayLH)), LatR, SolarDec, DayLH, Solar)[2];
                }
                //Add the last one
                //meteostate.hourlyRadiation[(int)DawnTime + (int)DayLH + 1] += (GlobalRadiation(1, LatR, SolarDec, DayLH, Solar)[0] * 3600 * DuskDawnFract);
                meteostate.hourlyRadiation[(int)DawnTime + (int)DayLH + 1] = (GlobalRadiation(1, LatR, SolarDec, DayLH, Solar)[0] * 3600 * DuskDawnFract);
                meteostate.hourlySolarElevation[(int)DawnTime + (int)DayLH + 1]=GlobalRadiation(1, LatR, SolarDec, DayLH, Solar)[2];
                double TotalRad = 0;
                for (int i = 0; i < 24; i++) TotalRad += meteostate.hourlyRadiation[i];
                for (int i = 0; i < 24; i++) meteostate.hourlyRadiation[i] = meteostate.hourlyRadiation[i] / TotalRad * meteostate.solarRadiation;



				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation
           //     private double CalcSolarDeclination(int doy)
           //     {
           //         return (23.45 * (Math.PI / 180)) * Math.Sin(2 * Math.PI * (284.0 + doy) / 365.0);

           //     }
                //------------------------------------------------------------------------------------------------
             /*   private double CalcDayLength(double LatR, double SolarDec)
                {
                    return Math.Acos(-Math.Tan(LatR) * Math.Tan(SolarDec));
                }*/
                //------------------------------------------------------------------------------------------------
              //  private double CalcSolarRadn(double RATIO, double DayL, double LatR, double SolarDec) // solar radiation
              //  {
              //      return (24.0 * 3600.0 * 1360.0 * (DayL * Math.Sin(LatR) * Math.Sin(SolarDec) +
              //         Math.Cos(LatR) * Math.Cos(SolarDec) * Math.Sin(DayL)) / (Math.PI * 1000000.0)) * RATIO;
              //}
                //------------------------------------------------------------------------------------------------
                private List<double> GlobalRadiation(double oTime, double latitude, double SolarDec, double DayLH, double Solar)
                {
                    double sunAngle = 0.0;
                    double ITot = 0.0;
                    double IDiff = 0.0;
                    double IDir = 0.0;
                    sunAngle = Math.Asin(Math.Sin(latitude) * Math.Sin(SolarDec) +
                       Math.Cos(latitude) * Math.Cos(SolarDec) * Math.Cos((Math.PI / 12.0) * DayLH * (oTime - 0.5))); //global variable
                    ITot = Solar * (1.0 + Math.Sin(2.0 * Math.PI * oTime + 1.5 * Math.PI)) / (DayLH * 60.0 * 60.0); //global variable
                    IDiff = 0.17 * 1370.0 * Math.Sin(sunAngle) / 1000000.0; //global variable
                    if (IDiff > ITot)
                    {
                        IDiff = ITot;
                    }
                    IDir = ITot - IDiff; //global variable
                    List<double> list = new List<double>();
                    list.Add(IDir);
                    list.Add(IDiff);
                    list.Add(sunAngle);
                    return list;
                }
				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}
