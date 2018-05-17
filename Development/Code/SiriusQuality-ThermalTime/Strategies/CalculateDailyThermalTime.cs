

 //Author:pierre martre pierre.martre@inra.fr
 //Institution:INRA
 //Author of revision: 
 //Date first release:12/16/2016
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


using SiriusQualityThermalTime;
using ResponseFunctions;


//To make this project compile please add the reference to assembly: SiriusQuality-ThermalTime, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: CRA.ModelLayer, Version=1.0.5212.29139, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: CRA.AgroManagement2014, Version=0.8.0.0, Culture=neutral, PublicKeyToken=null
//To make this project compile please add the reference to assembly: System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//To make this project compile please add the reference to assembly: ResponseFunctions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;

namespace SiriusQualityThermalTime.Strategies
{

	/// <summary>
	///Class CalculateDailyThermalTime
    /// 
    /// </summary>
	public class CalculateDailyThermalTime : IStrategySiriusQualityThermalTime
	{

	#region Constructor

			public CalculateDailyThermalTime()
			{
				
				ModellingOptions mo0_0 = new ModellingOptions();
				//Parameters
				List<VarInfo> _parameters0_0 = new List<VarInfo>();
				VarInfo v1 = new VarInfo();
				 v1.DefaultValue = 10;
				 v1.Description = "parameter for the non linear Wang-Engel model";
				 v1.Id = 0;
				 v1.MaxValue = 40;
				 v1.MinValue = -30;
				 v1.Name = "PostAnthesisTmin";
				 v1.Size = 1;
				 v1.Units = "°C";
				 v1.URL = "";
				 v1.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v1);
				VarInfo v2 = new VarInfo();
				 v2.DefaultValue = 15;
				 v2.Description = "parameter for the non linear Wang-Engel model";
				 v2.Id = 0;
				 v2.MaxValue = 40;
				 v2.MinValue = -20;
				 v2.Name = "PostAnthesisTopt";
				 v2.Size = 1;
				 v2.Units = "°C";
				 v2.URL = "";
				 v2.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v2);
				VarInfo v3 = new VarInfo();
				 v3.DefaultValue = 20;
				 v3.Description = "parameter for the non linear Wang-Engel model";
				 v3.Id = 0;
				 v3.MaxValue = 50;
				 v3.MinValue = -20;
				 v3.Name = "PostAnthesisTmax";
				 v3.Size = 1;
				 v3.Units = "°C";
				 v3.URL = "";
				 v3.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v3.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v3);
				VarInfo v4 = new VarInfo();
				 v4.DefaultValue = 1;
				 v4.Description = "parameter for the non linear Wang-Engel model";
				 v4.Id = 0;
				 v4.MaxValue = 10;
				 v4.MinValue = 0;
				 v4.Name = "PostAnthesisShape";
				 v4.Size = 1;
				 v4.Units = "NA";
				 v4.URL = "";
				 v4.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v4.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v4);
				VarInfo v5 = new VarInfo();
				 v5.DefaultValue = 10;
				 v5.Description = "parameter for the non linear Wang-Engel model";
				 v5.Id = 0;
				 v5.MaxValue = 40;
				 v5.MinValue = -30;
				 v5.Name = "PreAnthesisTmin";
				 v5.Size = 1;
				 v5.Units = "°C";
				 v5.URL = "";
				 v5.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v5.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v5);
				VarInfo v6 = new VarInfo();
				 v6.DefaultValue = 15;
				 v6.Description = "parameter for the non linear Wang-Engel model";
				 v6.Id = 0;
				 v6.MaxValue = 40;
				 v6.MinValue = -20;
				 v6.Name = "PreAnthesisTopt";
				 v6.Size = 1;
				 v6.Units = "°C";
				 v6.URL = "";
				 v6.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v6.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v6);
				VarInfo v7 = new VarInfo();
				 v7.DefaultValue = 20;
				 v7.Description = "parameter for the non linear Wang-Engel model";
				 v7.Id = 0;
				 v7.MaxValue = 50;
				 v7.MinValue = -20;
				 v7.Name = "PreAnthesisTmax";
				 v7.Size = 1;
				 v7.Units = "°C";
				 v7.URL = "";
				 v7.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v7.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v7);
				VarInfo v8 = new VarInfo();
				 v8.DefaultValue = 1;
				 v8.Description = "parameter for the non linear Wang-Engel model";
				 v8.Id = 0;
				 v8.MaxValue = 10;
				 v8.MinValue = 0;
				 v8.Name = "PreAnthesisShape";
				 v8.Size = 1;
				 v8.Units = "NA";
				 v8.URL = "";
				 v8.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v8.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v8);
				VarInfo v9 = new VarInfo();
				 v9.DefaultValue = 10;
				 v9.Description = "parameter for the non linear Wang-Engel model for remobilisation";
				 v9.Id = 0;
				 v9.MaxValue = 40;
				 v9.MinValue = -30;
				 v9.Name = "LUETmin";
				 v9.Size = 1;
				 v9.Units = "°C";
				 v9.URL = "";
				 v9.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v9.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v9);
				VarInfo v10 = new VarInfo();
				 v10.DefaultValue = 15;
				 v10.Description = "parameter for the non linear Wang-Engel model for remobilisation";
				 v10.Id = 0;
				 v10.MaxValue = 40;
				 v10.MinValue = -20;
				 v10.Name = "LUETmax";
				 v10.Size = 1;
				 v10.Units = "°C";
				 v10.URL = "";
				 v10.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v10.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v10);
				VarInfo v11 = new VarInfo();
				 v11.DefaultValue = 20;
				 v11.Description = "parameter for the non linear Wang-Engel model for remobilisation";
				 v11.Id = 0;
				 v11.MaxValue = 50;
				 v11.MinValue = -20;
				 v11.Name = "LUETopt";
				 v11.Size = 1;
				 v11.Units = "°C";
				 v11.URL = "";
				 v11.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v11.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v11);
				VarInfo v12 = new VarInfo();
				 v12.DefaultValue = 1;
				 v12.Description = "parameter for the non linear Wang-Engel model for remobilisation";
				 v12.Id = 0;
				 v12.MaxValue = 10;
				 v12.MinValue = 0;
				 v12.Name = "LUETshape";
				 v12.Size = 1;
				 v12.Units = "NA";
				 v12.URL = "";
				 v12.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v12.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v12);
				VarInfo v13 = new VarInfo();
				 v13.DefaultValue = 20;
				 v13.Description = "for leaf senescence factor";
				 v13.Id = 0;
				 v13.MaxValue = 40;
				 v13.MinValue = -20;
				 v13.Name = "SenAccT";
				 v13.Size = 1;
				 v13.Units = "°C";
				 v13.URL = "";
				 v13.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v13.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v13);
				VarInfo v14 = new VarInfo();
				 v14.DefaultValue = 1;
				 v14.Description = "for leaf senescence factor";
				 v14.Id = 0;
				 v14.MaxValue = 10;
				 v14.MinValue = 0;
				 v14.Name = "SenAccSlope";
				 v14.Size = 1;
				 v14.Units = "NA";
				 v14.URL = "";
				 v14.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v14.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
				 _parameters0_0.Add(v14);
				VarInfo v15 = new VarInfo();
				 v15.DefaultValue = 0;
				 v15.Description = "true for Maize and false for Wheat ";
				 v15.Id = 0;
				 v15.MaxValue = 1;
				 v15.MinValue = 0;
				 v15.Name = "switchMaize";
				 v15.Size = 1;
				 v15.Units = "NA";
				 v15.URL = "";
				 v15.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v15.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");
				 _parameters0_0.Add(v15);
				VarInfo v16 = new VarInfo();
				 v16.DefaultValue = 0;
				 v16.Description = "true : use Air temperature, false : use Canopy temperature for leaf senescence";
				 v16.Id = 0;
				 v16.MaxValue = 1;
				 v16.MinValue = 0;
				 v16.Name = "UseAirTemperatureForSenescence";
				 v16.Size = 1;
				 v16.Units = "NA";
				 v16.URL = "";
				 v16.VarType = CRA.ModelLayer.Core.VarInfo.Type.STATE;
				 v16.ValueType = VarInfoValueTypes.GetInstanceForName("Integer");
				 _parameters0_0.Add(v16);
				mo0_0.Parameters=_parameters0_0;
				//Inputs
				List<PropertyDescription> _inputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd1 = new PropertyDescription();
				pd1.DomainClassType = typeof(SiriusQualityThermalTime.ThermalTimeState);
				pd1.PropertyName = "phaseValue";
				pd1.PropertyType = (( SiriusQualityThermalTime.ThermalTimeStateVarInfo.phaseValue)).ValueType.TypeForCurrentValue;
				pd1.PropertyVarInfo =( SiriusQualityThermalTime.ThermalTimeStateVarInfo.phaseValue);
				_inputs0_0.Add(pd1);
				PropertyDescription pd2 = new PropertyDescription();
				pd2.DomainClassType = typeof(SiriusQualityThermalTime.ThermalTimeState);
				pd2.PropertyName = "minTsoil";
				pd2.PropertyType = (( SiriusQualityThermalTime.ThermalTimeStateVarInfo.minTsoil)).ValueType.TypeForCurrentValue;
				pd2.PropertyVarInfo =( SiriusQualityThermalTime.ThermalTimeStateVarInfo.minTsoil);
				_inputs0_0.Add(pd2);
				PropertyDescription pd3 = new PropertyDescription();
				pd3.DomainClassType = typeof(SiriusQualityThermalTime.ThermalTimeState);
				pd3.PropertyName = "minTshoot";
				pd3.PropertyType = (( SiriusQualityThermalTime.ThermalTimeStateVarInfo.minTshoot)).ValueType.TypeForCurrentValue;
				pd3.PropertyVarInfo =( SiriusQualityThermalTime.ThermalTimeStateVarInfo.minTshoot);
				_inputs0_0.Add(pd3);
				PropertyDescription pd4 = new PropertyDescription();
				pd4.DomainClassType = typeof(SiriusQualityThermalTime.ThermalTimeState);
				pd4.PropertyName = "minTair";
				pd4.PropertyType = (( SiriusQualityThermalTime.ThermalTimeStateVarInfo.minTair)).ValueType.TypeForCurrentValue;
				pd4.PropertyVarInfo =( SiriusQualityThermalTime.ThermalTimeStateVarInfo.minTair);
				_inputs0_0.Add(pd4);
				PropertyDescription pd5 = new PropertyDescription();
				pd5.DomainClassType = typeof(SiriusQualityThermalTime.ThermalTimeState);
				pd5.PropertyName = "maxTsoil";
				pd5.PropertyType = (( SiriusQualityThermalTime.ThermalTimeStateVarInfo.maxTsoil)).ValueType.TypeForCurrentValue;
				pd5.PropertyVarInfo =( SiriusQualityThermalTime.ThermalTimeStateVarInfo.maxTsoil);
				_inputs0_0.Add(pd5);
				PropertyDescription pd6 = new PropertyDescription();
				pd6.DomainClassType = typeof(SiriusQualityThermalTime.ThermalTimeState);
				pd6.PropertyName = "maxTshoot";
				pd6.PropertyType = (( SiriusQualityThermalTime.ThermalTimeStateVarInfo.maxTshoot)).ValueType.TypeForCurrentValue;
				pd6.PropertyVarInfo =( SiriusQualityThermalTime.ThermalTimeStateVarInfo.maxTshoot);
				_inputs0_0.Add(pd6);
				PropertyDescription pd7 = new PropertyDescription();
				pd7.DomainClassType = typeof(SiriusQualityThermalTime.ThermalTimeState);
				pd7.PropertyName = "maxTair";
				pd7.PropertyType = (( SiriusQualityThermalTime.ThermalTimeStateVarInfo.maxTair)).ValueType.TypeForCurrentValue;
				pd7.PropertyVarInfo =( SiriusQualityThermalTime.ThermalTimeStateVarInfo.maxTair);
				_inputs0_0.Add(pd7);
				PropertyDescription pd8 = new PropertyDescription();
				pd8.DomainClassType = typeof(SiriusQualityThermalTime.ThermalTimeState);
				pd8.PropertyName = "hourlyAirTemperature";
				pd8.PropertyType = (( SiriusQualityThermalTime.ThermalTimeStateVarInfo.hourlyAirTemperature)).ValueType.TypeForCurrentValue;
				pd8.PropertyVarInfo =( SiriusQualityThermalTime.ThermalTimeStateVarInfo.hourlyAirTemperature);
				_inputs0_0.Add(pd8);
				PropertyDescription pd9 = new PropertyDescription();
				pd9.DomainClassType = typeof(SiriusQualityThermalTime.ThermalTimeState);
				pd9.PropertyName = "hourlyShootTemperature";
				pd9.PropertyType = (( SiriusQualityThermalTime.ThermalTimeStateVarInfo.hourlyShootTemperature)).ValueType.TypeForCurrentValue;
				pd9.PropertyVarInfo =( SiriusQualityThermalTime.ThermalTimeStateVarInfo.hourlyShootTemperature);
				_inputs0_0.Add(pd9);
				mo0_0.Inputs=_inputs0_0;
				//Outputs
				List<PropertyDescription> _outputs0_0 = new List<PropertyDescription>();
				PropertyDescription pd10 = new PropertyDescription();
				pd10.DomainClassType = typeof(SiriusQualityThermalTime.ThermalTimeState);
				pd10.PropertyName = "cumulTT";
				pd10.PropertyType =  (( SiriusQualityThermalTime.ThermalTimeStateVarInfo.cumulTT)).ValueType.TypeForCurrentValue;
				pd10.PropertyVarInfo =(  SiriusQualityThermalTime.ThermalTimeStateVarInfo.cumulTT);
				_outputs0_0.Add(pd10);
				PropertyDescription pd11 = new PropertyDescription();
				pd11.DomainClassType = typeof(SiriusQualityThermalTime.ThermalTimeState);
				pd11.PropertyName = "deltaTT";
				pd11.PropertyType =  (( SiriusQualityThermalTime.ThermalTimeStateVarInfo.deltaTT)).ValueType.TypeForCurrentValue;
				pd11.PropertyVarInfo =(  SiriusQualityThermalTime.ThermalTimeStateVarInfo.deltaTT);
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
				get {  return ""; }
			}

			/// <summary>
			/// Type of the model.
			/// </summary>
			public string ModelType
			{
				get { return "thermal time"; }
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
				_pd.Add("Date", "12/16/2016");
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
				return new List<Type>() {  typeof(SiriusQualityThermalTime.ThermalTimeState) };
			}

	#endregion

    #region Instances of the parameters
			
			// Getter and setters for the value of the parameters of the strategy. The actual parameters are stored into the ModelingOptionsManager of the strategy.

			
			public Double PostAnthesisTmin
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("PostAnthesisTmin");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'PostAnthesisTmin' not found (or found null) in strategy 'CalculateDailyThermalTime'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("PostAnthesisTmin");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'PostAnthesisTmin' not found in strategy 'CalculateDailyThermalTime'");
				}
			}
			public Double PostAnthesisTopt
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("PostAnthesisTopt");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'PostAnthesisTopt' not found (or found null) in strategy 'CalculateDailyThermalTime'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("PostAnthesisTopt");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'PostAnthesisTopt' not found in strategy 'CalculateDailyThermalTime'");
				}
			}
			public Double PostAnthesisTmax
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("PostAnthesisTmax");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'PostAnthesisTmax' not found (or found null) in strategy 'CalculateDailyThermalTime'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("PostAnthesisTmax");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'PostAnthesisTmax' not found in strategy 'CalculateDailyThermalTime'");
				}
			}
			public Double PostAnthesisShape
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("PostAnthesisShape");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'PostAnthesisShape' not found (or found null) in strategy 'CalculateDailyThermalTime'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("PostAnthesisShape");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'PostAnthesisShape' not found in strategy 'CalculateDailyThermalTime'");
				}
			}
			public Double PreAnthesisTmin
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("PreAnthesisTmin");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'PreAnthesisTmin' not found (or found null) in strategy 'CalculateDailyThermalTime'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("PreAnthesisTmin");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'PreAnthesisTmin' not found in strategy 'CalculateDailyThermalTime'");
				}
			}
			public Double PreAnthesisTopt
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("PreAnthesisTopt");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'PreAnthesisTopt' not found (or found null) in strategy 'CalculateDailyThermalTime'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("PreAnthesisTopt");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'PreAnthesisTopt' not found in strategy 'CalculateDailyThermalTime'");
				}
			}
			public Double PreAnthesisTmax
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("PreAnthesisTmax");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'PreAnthesisTmax' not found (or found null) in strategy 'CalculateDailyThermalTime'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("PreAnthesisTmax");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'PreAnthesisTmax' not found in strategy 'CalculateDailyThermalTime'");
				}
			}
			public Double PreAnthesisShape
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("PreAnthesisShape");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'PreAnthesisShape' not found (or found null) in strategy 'CalculateDailyThermalTime'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("PreAnthesisShape");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'PreAnthesisShape' not found in strategy 'CalculateDailyThermalTime'");
				}
			}
			public Double LUETmin
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("LUETmin");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'LUETmin' not found (or found null) in strategy 'CalculateDailyThermalTime'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("LUETmin");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'LUETmin' not found in strategy 'CalculateDailyThermalTime'");
				}
			}
			public Double LUETmax
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("LUETmax");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'LUETmax' not found (or found null) in strategy 'CalculateDailyThermalTime'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("LUETmax");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'LUETmax' not found in strategy 'CalculateDailyThermalTime'");
				}
			}
			public Double LUETopt
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("LUETopt");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'LUETopt' not found (or found null) in strategy 'CalculateDailyThermalTime'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("LUETopt");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'LUETopt' not found in strategy 'CalculateDailyThermalTime'");
				}
			}
			public Double LUETshape
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("LUETshape");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'LUETshape' not found (or found null) in strategy 'CalculateDailyThermalTime'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("LUETshape");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'LUETshape' not found in strategy 'CalculateDailyThermalTime'");
				}
			}
			public Double SenAccT
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("SenAccT");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'SenAccT' not found (or found null) in strategy 'CalculateDailyThermalTime'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("SenAccT");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'SenAccT' not found in strategy 'CalculateDailyThermalTime'");
				}
			}
			public Double SenAccSlope
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("SenAccSlope");
						if (vi != null && vi.CurrentValue!=null) return (Double)vi.CurrentValue ;
						else throw new Exception("Parameter 'SenAccSlope' not found (or found null) in strategy 'CalculateDailyThermalTime'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("SenAccSlope");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'SenAccSlope' not found in strategy 'CalculateDailyThermalTime'");
				}
			}
			public Int32 switchMaize
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("switchMaize");
						if (vi != null && vi.CurrentValue!=null) return (Int32)vi.CurrentValue ;
						else throw new Exception("Parameter 'switchMaize' not found (or found null) in strategy 'CalculateDailyThermalTime'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("switchMaize");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'switchMaize' not found in strategy 'CalculateDailyThermalTime'");
				}
			}
			public Int32 UseAirTemperatureForSenescence
			{ 
				get {
						VarInfo vi= _modellingOptionsManager.GetParameterByName("UseAirTemperatureForSenescence");
						if (vi != null && vi.CurrentValue!=null) return (Int32)vi.CurrentValue ;
						else throw new Exception("Parameter 'UseAirTemperatureForSenescence' not found (or found null) in strategy 'CalculateDailyThermalTime'");
				 } set {
							VarInfo vi = _modellingOptionsManager.GetParameterByName("UseAirTemperatureForSenescence");
							if (vi != null)  vi.CurrentValue=value;
						else throw new Exception("Parameter 'UseAirTemperatureForSenescence' not found in strategy 'CalculateDailyThermalTime'");
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
                PostAnthesisTminVarInfo.Name = "PostAnthesisTmin";
				PostAnthesisTminVarInfo.Description =" parameter for the non linear Wang-Engel model";
				PostAnthesisTminVarInfo.MaxValue = 40;
				PostAnthesisTminVarInfo.MinValue = -30;
				PostAnthesisTminVarInfo.DefaultValue = 10;
				PostAnthesisTminVarInfo.Units = "°C";
				PostAnthesisTminVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				PostAnthesisToptVarInfo.Name = "PostAnthesisTopt";
				PostAnthesisToptVarInfo.Description =" parameter for the non linear Wang-Engel model";
				PostAnthesisToptVarInfo.MaxValue = 40;
				PostAnthesisToptVarInfo.MinValue = -20;
				PostAnthesisToptVarInfo.DefaultValue = 15;
				PostAnthesisToptVarInfo.Units = "°C";
				PostAnthesisToptVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				PostAnthesisTmaxVarInfo.Name = "PostAnthesisTmax";
				PostAnthesisTmaxVarInfo.Description =" parameter for the non linear Wang-Engel model";
				PostAnthesisTmaxVarInfo.MaxValue = 50;
				PostAnthesisTmaxVarInfo.MinValue = -20;
				PostAnthesisTmaxVarInfo.DefaultValue = 20;
				PostAnthesisTmaxVarInfo.Units = "°C";
				PostAnthesisTmaxVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				PostAnthesisShapeVarInfo.Name = "PostAnthesisShape";
				PostAnthesisShapeVarInfo.Description =" parameter for the non linear Wang-Engel model";
				PostAnthesisShapeVarInfo.MaxValue = 10;
				PostAnthesisShapeVarInfo.MinValue = 0;
				PostAnthesisShapeVarInfo.DefaultValue = 1;
				PostAnthesisShapeVarInfo.Units = "NA";
				PostAnthesisShapeVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				PreAnthesisTminVarInfo.Name = "PreAnthesisTmin";
				PreAnthesisTminVarInfo.Description =" parameter for the non linear Wang-Engel model";
				PreAnthesisTminVarInfo.MaxValue = 40;
				PreAnthesisTminVarInfo.MinValue = -30;
				PreAnthesisTminVarInfo.DefaultValue = 10;
				PreAnthesisTminVarInfo.Units = "°C";
				PreAnthesisTminVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				PreAnthesisToptVarInfo.Name = "PreAnthesisTopt";
				PreAnthesisToptVarInfo.Description =" parameter for the non linear Wang-Engel model";
				PreAnthesisToptVarInfo.MaxValue = 40;
				PreAnthesisToptVarInfo.MinValue = -20;
				PreAnthesisToptVarInfo.DefaultValue = 15;
				PreAnthesisToptVarInfo.Units = "°C";
				PreAnthesisToptVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				PreAnthesisTmaxVarInfo.Name = "PreAnthesisTmax";
				PreAnthesisTmaxVarInfo.Description =" parameter for the non linear Wang-Engel model";
				PreAnthesisTmaxVarInfo.MaxValue = 50;
				PreAnthesisTmaxVarInfo.MinValue = -20;
				PreAnthesisTmaxVarInfo.DefaultValue = 20;
				PreAnthesisTmaxVarInfo.Units = "°C";
				PreAnthesisTmaxVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				PreAnthesisShapeVarInfo.Name = "PreAnthesisShape";
				PreAnthesisShapeVarInfo.Description =" parameter for the non linear Wang-Engel model";
				PreAnthesisShapeVarInfo.MaxValue = 10;
				PreAnthesisShapeVarInfo.MinValue = 0;
				PreAnthesisShapeVarInfo.DefaultValue = 1;
				PreAnthesisShapeVarInfo.Units = "NA";
				PreAnthesisShapeVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				LUETminVarInfo.Name = "LUETmin";
				LUETminVarInfo.Description =" parameter for the non linear Wang-Engel model for remobilisation";
				LUETminVarInfo.MaxValue = 40;
				LUETminVarInfo.MinValue = -30;
				LUETminVarInfo.DefaultValue = 10;
				LUETminVarInfo.Units = "°C";
				LUETminVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				LUETmaxVarInfo.Name = "LUETmax";
				LUETmaxVarInfo.Description =" parameter for the non linear Wang-Engel model for remobilisation";
				LUETmaxVarInfo.MaxValue = 40;
				LUETmaxVarInfo.MinValue = -20;
				LUETmaxVarInfo.DefaultValue = 15;
				LUETmaxVarInfo.Units = "°C";
				LUETmaxVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				LUEToptVarInfo.Name = "LUETopt";
				LUEToptVarInfo.Description =" parameter for the non linear Wang-Engel model for remobilisation";
				LUEToptVarInfo.MaxValue = 50;
				LUEToptVarInfo.MinValue = -20;
				LUEToptVarInfo.DefaultValue = 20;
				LUEToptVarInfo.Units = "°C";
				LUEToptVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				LUETshapeVarInfo.Name = "LUETshape";
				LUETshapeVarInfo.Description =" parameter for the non linear Wang-Engel model for remobilisation";
				LUETshapeVarInfo.MaxValue = 10;
				LUETshapeVarInfo.MinValue = 0;
				LUETshapeVarInfo.DefaultValue = 1;
				LUETshapeVarInfo.Units = "NA";
				LUETshapeVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				SenAccTVarInfo.Name = "SenAccT";
				SenAccTVarInfo.Description =" for leaf senescence factor";
				SenAccTVarInfo.MaxValue = 40;
				SenAccTVarInfo.MinValue = -20;
				SenAccTVarInfo.DefaultValue = 20;
				SenAccTVarInfo.Units = "°C";
				SenAccTVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				SenAccSlopeVarInfo.Name = "SenAccSlope";
				SenAccSlopeVarInfo.Description =" for leaf senescence factor";
				SenAccSlopeVarInfo.MaxValue = 10;
				SenAccSlopeVarInfo.MinValue = 0;
				SenAccSlopeVarInfo.DefaultValue = 1;
				SenAccSlopeVarInfo.Units = "NA";
				SenAccSlopeVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Double");

				switchMaizeVarInfo.Name = "switchMaize";
				switchMaizeVarInfo.Description =" true for Maize and false for Wheat ";
				switchMaizeVarInfo.MaxValue = 1;
				switchMaizeVarInfo.MinValue = 0;
				switchMaizeVarInfo.DefaultValue = 0;
				switchMaizeVarInfo.Units = "NA";
				switchMaizeVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Integer");

				UseAirTemperatureForSenescenceVarInfo.Name = "UseAirTemperatureForSenescence";
				UseAirTemperatureForSenescenceVarInfo.Description =" true : use Air temperature, false : use Canopy temperature for leaf senescence";
				UseAirTemperatureForSenescenceVarInfo.MaxValue = 1;
				UseAirTemperatureForSenescenceVarInfo.MinValue = 0;
				UseAirTemperatureForSenescenceVarInfo.DefaultValue = 0;
				UseAirTemperatureForSenescenceVarInfo.Units = "NA";
				UseAirTemperatureForSenescenceVarInfo.ValueType = CRA.ModelLayer.Core.VarInfoValueTypes.GetInstanceForName("Integer");

				
       
			}

			//Parameters static VarInfo list 
			
				private static VarInfo _PostAnthesisTminVarInfo= new VarInfo();
				/// <summary> 
				///PostAnthesisTmin VarInfo definition
				/// </summary>
				public static VarInfo PostAnthesisTminVarInfo
				{
					get { return _PostAnthesisTminVarInfo; }
				}
				private static VarInfo _PostAnthesisToptVarInfo= new VarInfo();
				/// <summary> 
				///PostAnthesisTopt VarInfo definition
				/// </summary>
				public static VarInfo PostAnthesisToptVarInfo
				{
					get { return _PostAnthesisToptVarInfo; }
				}
				private static VarInfo _PostAnthesisTmaxVarInfo= new VarInfo();
				/// <summary> 
				///PostAnthesisTmax VarInfo definition
				/// </summary>
				public static VarInfo PostAnthesisTmaxVarInfo
				{
					get { return _PostAnthesisTmaxVarInfo; }
				}
				private static VarInfo _PostAnthesisShapeVarInfo= new VarInfo();
				/// <summary> 
				///PostAnthesisShape VarInfo definition
				/// </summary>
				public static VarInfo PostAnthesisShapeVarInfo
				{
					get { return _PostAnthesisShapeVarInfo; }
				}
				private static VarInfo _PreAnthesisTminVarInfo= new VarInfo();
				/// <summary> 
				///PreAnthesisTmin VarInfo definition
				/// </summary>
				public static VarInfo PreAnthesisTminVarInfo
				{
					get { return _PreAnthesisTminVarInfo; }
				}
				private static VarInfo _PreAnthesisToptVarInfo= new VarInfo();
				/// <summary> 
				///PreAnthesisTopt VarInfo definition
				/// </summary>
				public static VarInfo PreAnthesisToptVarInfo
				{
					get { return _PreAnthesisToptVarInfo; }
				}
				private static VarInfo _PreAnthesisTmaxVarInfo= new VarInfo();
				/// <summary> 
				///PreAnthesisTmax VarInfo definition
				/// </summary>
				public static VarInfo PreAnthesisTmaxVarInfo
				{
					get { return _PreAnthesisTmaxVarInfo; }
				}
				private static VarInfo _PreAnthesisShapeVarInfo= new VarInfo();
				/// <summary> 
				///PreAnthesisShape VarInfo definition
				/// </summary>
				public static VarInfo PreAnthesisShapeVarInfo
				{
					get { return _PreAnthesisShapeVarInfo; }
				}
				private static VarInfo _LUETminVarInfo= new VarInfo();
				/// <summary> 
				///LUETmin VarInfo definition
				/// </summary>
				public static VarInfo LUETminVarInfo
				{
					get { return _LUETminVarInfo; }
				}
				private static VarInfo _LUETmaxVarInfo= new VarInfo();
				/// <summary> 
				///LUETmax VarInfo definition
				/// </summary>
				public static VarInfo LUETmaxVarInfo
				{
					get { return _LUETmaxVarInfo; }
				}
				private static VarInfo _LUEToptVarInfo= new VarInfo();
				/// <summary> 
				///LUETopt VarInfo definition
				/// </summary>
				public static VarInfo LUEToptVarInfo
				{
					get { return _LUEToptVarInfo; }
				}
				private static VarInfo _LUETshapeVarInfo= new VarInfo();
				/// <summary> 
				///LUETshape VarInfo definition
				/// </summary>
				public static VarInfo LUETshapeVarInfo
				{
					get { return _LUETshapeVarInfo; }
				}
				private static VarInfo _SenAccTVarInfo= new VarInfo();
				/// <summary> 
				///SenAccT VarInfo definition
				/// </summary>
				public static VarInfo SenAccTVarInfo
				{
					get { return _SenAccTVarInfo; }
				}
				private static VarInfo _SenAccSlopeVarInfo= new VarInfo();
				/// <summary> 
				///SenAccSlope VarInfo definition
				/// </summary>
				public static VarInfo SenAccSlopeVarInfo
				{
					get { return _SenAccSlopeVarInfo; }
				}
				private static VarInfo _switchMaizeVarInfo= new VarInfo();
				/// <summary> 
				///switchMaize VarInfo definition
				/// </summary>
				public static VarInfo switchMaizeVarInfo
				{
					get { return _switchMaizeVarInfo; }
				}
				private static VarInfo _UseAirTemperatureForSenescenceVarInfo= new VarInfo();
				/// <summary> 
				///UseAirTemperatureForSenescence VarInfo definition
				/// </summary>
				public static VarInfo UseAirTemperatureForSenescenceVarInfo
				{
					get { return _UseAirTemperatureForSenescenceVarInfo; }
				}					
			
			//Parameters static VarInfo list of the composite class
						

	#endregion
	
	#region pre/post conditions management		

		    /// <summary>
			/// Test to verify the postconditions
			/// </summary>
			public string TestPostConditions(SiriusQualityThermalTime.ThermalTimeState thermaltimestate, string callID)
			{
				try
				{
					//Set current values of the outputs to the static VarInfo representing the output properties of the domain classes				
					
					SiriusQualityThermalTime.ThermalTimeStateVarInfo.cumulTT.CurrentValue=thermaltimestate.cumulTT;
					SiriusQualityThermalTime.ThermalTimeStateVarInfo.deltaTT.CurrentValue=thermaltimestate.deltaTT;
					
					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();            
					
					
					RangeBasedCondition r10 = new RangeBasedCondition(SiriusQualityThermalTime.ThermalTimeStateVarInfo.cumulTT);
					if(r10.ApplicableVarInfoValueTypes.Contains( SiriusQualityThermalTime.ThermalTimeStateVarInfo.cumulTT.ValueType)){prc.AddCondition(r10);}
					RangeBasedCondition r11 = new RangeBasedCondition(SiriusQualityThermalTime.ThermalTimeStateVarInfo.deltaTT);
					if(r11.ApplicableVarInfoValueTypes.Contains( SiriusQualityThermalTime.ThermalTimeStateVarInfo.deltaTT.ValueType)){prc.AddCondition(r11);}

					

					//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section4
					//Code written below will not be overwritten by a future code generation

        

					//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
					//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section4 

					//Get the evaluation of postconditions
					string postConditionsResult =pre.VerifyPostconditions(prc, callID);
					//if we have errors, send it to the configured output 
					if(!string.IsNullOrEmpty(postConditionsResult)) { pre.TestsOut(postConditionsResult, true, "PostConditions errors in component SiriusQualityThermalTime.Strategies, strategy " + this.GetType().Name ); }
					return postConditionsResult;
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1001,	"Strategy: " + this.GetType().Name + " - Unhandled exception running post-conditions");

					string msg = "Component SiriusQualityThermalTime.Strategies, " + this.GetType().Name + ": Unhandled exception running post-condition test. ";
					throw new Exception(msg, exception);
				}
			}

			/// <summary>
			/// Test to verify the preconditions
			/// </summary>
			public string TestPreConditions(SiriusQualityThermalTime.ThermalTimeState thermaltimestate, string callID)
			{
				try
				{
					//Set current values of the inputs to the static VarInfo representing the input properties of the domain classes				
					
					SiriusQualityThermalTime.ThermalTimeStateVarInfo.phaseValue.CurrentValue=thermaltimestate.phaseValue;
					SiriusQualityThermalTime.ThermalTimeStateVarInfo.minTsoil.CurrentValue=thermaltimestate.minTsoil;
					SiriusQualityThermalTime.ThermalTimeStateVarInfo.minTshoot.CurrentValue=thermaltimestate.minTshoot;
					SiriusQualityThermalTime.ThermalTimeStateVarInfo.minTair.CurrentValue=thermaltimestate.minTair;
					SiriusQualityThermalTime.ThermalTimeStateVarInfo.maxTsoil.CurrentValue=thermaltimestate.maxTsoil;
					SiriusQualityThermalTime.ThermalTimeStateVarInfo.maxTshoot.CurrentValue=thermaltimestate.maxTshoot;
					SiriusQualityThermalTime.ThermalTimeStateVarInfo.maxTair.CurrentValue=thermaltimestate.maxTair;
					SiriusQualityThermalTime.ThermalTimeStateVarInfo.hourlyAirTemperature.CurrentValue=thermaltimestate.hourlyAirTemperature;
					SiriusQualityThermalTime.ThermalTimeStateVarInfo.hourlyShootTemperature.CurrentValue=thermaltimestate.hourlyShootTemperature;

					//Create the collection of the conditions to test
					ConditionsCollection prc = new ConditionsCollection();
					Preconditions pre = new Preconditions();
            
					
					RangeBasedCondition r1 = new RangeBasedCondition(SiriusQualityThermalTime.ThermalTimeStateVarInfo.phaseValue);
					if(r1.ApplicableVarInfoValueTypes.Contains( SiriusQualityThermalTime.ThermalTimeStateVarInfo.phaseValue.ValueType)){prc.AddCondition(r1);}
					RangeBasedCondition r2 = new RangeBasedCondition(SiriusQualityThermalTime.ThermalTimeStateVarInfo.minTsoil);
					if(r2.ApplicableVarInfoValueTypes.Contains( SiriusQualityThermalTime.ThermalTimeStateVarInfo.minTsoil.ValueType)){prc.AddCondition(r2);}
					RangeBasedCondition r3 = new RangeBasedCondition(SiriusQualityThermalTime.ThermalTimeStateVarInfo.minTshoot);
					if(r3.ApplicableVarInfoValueTypes.Contains( SiriusQualityThermalTime.ThermalTimeStateVarInfo.minTshoot.ValueType)){prc.AddCondition(r3);}
					RangeBasedCondition r4 = new RangeBasedCondition(SiriusQualityThermalTime.ThermalTimeStateVarInfo.minTair);
					if(r4.ApplicableVarInfoValueTypes.Contains( SiriusQualityThermalTime.ThermalTimeStateVarInfo.minTair.ValueType)){prc.AddCondition(r4);}
					RangeBasedCondition r5 = new RangeBasedCondition(SiriusQualityThermalTime.ThermalTimeStateVarInfo.maxTsoil);
					if(r5.ApplicableVarInfoValueTypes.Contains( SiriusQualityThermalTime.ThermalTimeStateVarInfo.maxTsoil.ValueType)){prc.AddCondition(r5);}
					RangeBasedCondition r6 = new RangeBasedCondition(SiriusQualityThermalTime.ThermalTimeStateVarInfo.maxTshoot);
					if(r6.ApplicableVarInfoValueTypes.Contains( SiriusQualityThermalTime.ThermalTimeStateVarInfo.maxTshoot.ValueType)){prc.AddCondition(r6);}
					RangeBasedCondition r7 = new RangeBasedCondition(SiriusQualityThermalTime.ThermalTimeStateVarInfo.maxTair);
					if(r7.ApplicableVarInfoValueTypes.Contains( SiriusQualityThermalTime.ThermalTimeStateVarInfo.maxTair.ValueType)){prc.AddCondition(r7);}
					RangeBasedCondition r8 = new RangeBasedCondition(SiriusQualityThermalTime.ThermalTimeStateVarInfo.hourlyAirTemperature);
					if(r8.ApplicableVarInfoValueTypes.Contains( SiriusQualityThermalTime.ThermalTimeStateVarInfo.hourlyAirTemperature.ValueType)){prc.AddCondition(r8);}
					RangeBasedCondition r9 = new RangeBasedCondition(SiriusQualityThermalTime.ThermalTimeStateVarInfo.hourlyShootTemperature);
					if(r9.ApplicableVarInfoValueTypes.Contains( SiriusQualityThermalTime.ThermalTimeStateVarInfo.hourlyShootTemperature.ValueType)){prc.AddCondition(r9);}
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("PostAnthesisTmin")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("PostAnthesisTopt")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("PostAnthesisTmax")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("PostAnthesisShape")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("PreAnthesisTmin")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("PreAnthesisTopt")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("PreAnthesisTmax")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("PreAnthesisShape")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("LUETmin")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("LUETmax")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("LUETopt")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("LUETshape")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("SenAccT")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("SenAccSlope")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("switchMaize")));
					prc.AddCondition(new RangeBasedCondition(_modellingOptionsManager.GetParameterByName("UseAirTemperatureForSenescence")));

					

					//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section3
					//Code written below will not be overwritten by a future code generation

        

					//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
					//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section3 
								
					//Get the evaluation of preconditions;					
					string preConditionsResult =pre.VerifyPreconditions(prc, callID);
					//if we have errors, send it to the configured output 
					if(!string.IsNullOrEmpty(preConditionsResult)) { pre.TestsOut(preConditionsResult, true, "PreConditions errors in component SiriusQualityThermalTime.Strategies, strategy " + this.GetType().Name ); }
					return preConditionsResult;
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//	TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1002,"Strategy: " + this.GetType().Name + " - Unhandled exception running pre-conditions");

					string msg = "Component SiriusQualityThermalTime.Strategies, " + this.GetType().Name + ": Unhandled exception running pre-condition test. ";
					throw new Exception(msg, exception);
				}
			}

		
	#endregion
		


	#region Model

		 	/// <summary>
			/// Run the strategy to calculate the outputs. In case of error during the execution, the preconditions tests are executed.
			/// </summary>
			public void Estimate(SiriusQualityThermalTime.ThermalTimeState thermaltimestate)
			{
				try
				{
					CalculateModel(thermaltimestate);

					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Verbose, 1005,"Strategy: " + this.GetType().Name + " - Model executed");
				}
				catch (Exception exception)
				{
					//Uncomment the next line to use the trace
					//TraceStrategies.TraceEvent(System.Diagnostics.TraceEventType.Error, 1003,		"Strategy: " + this.GetType().Name + " - Unhandled exception running model");

					string msg = "Error in component SiriusQualityThermalTime.Strategies, strategy: " + this.GetType().Name + ": Unhandled exception running model. "+exception.GetType().FullName+" - "+exception.Message;				
					throw new Exception(msg, exception);
				}
			}

		

			private void CalculateModel(SiriusQualityThermalTime.ThermalTimeState thermaltimestate)
			{				
				

				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section1
				//Code written below will not be overwritten by a future code generation

                thermaltimestate.deltaTT[(int)SiriusQualityThermalTime.ThermalTimeState.Delta.Air] = GetLinearThermalTimeIncrement(0, thermaltimestate.minTair, thermaltimestate.maxTair); // #Andrea 26/11/2015
                thermaltimestate.deltaTT[(int)SiriusQualityThermalTime.ThermalTimeState.Delta.Soil] = GetLinearThermalTimeIncrement(0, thermaltimestate.minTsoil, thermaltimestate.maxTsoil);// #Andrea 26/11/2015

                // #Andrea 12/01/2016 - There is no more the condition related to the number of leaves. Now that condition is just in the class ShootTemperature.cs and temperature passed from Universe are already "modified"   
                thermaltimestate.deltaTT[(int)SiriusQualityThermalTime.ThermalTimeState.Delta.Shoot] = GetNonLinearThermalTimeIncrement(thermaltimestate.minTshoot, thermaltimestate.maxTshoot, thermaltimestate.hourlyShootTemperature, thermaltimestate.phaseValue);
                

                thermaltimestate.deltaTT[(int)SiriusQualityThermalTime.ThermalTimeState.Delta.PhenoMaize] = GetNonLinearThermalTimeIncrement(thermaltimestate.minTair, thermaltimestate.maxTair, thermaltimestate.hourlyAirTemperature, thermaltimestate.phaseValue);


                // Behnam (2016.02.11)
                thermaltimestate.deltaTT[(int)SiriusQualityThermalTime.ThermalTimeState.Delta.Physiology] = GetNonLinearPhysThermalTimeIncrement(thermaltimestate.minTshoot, thermaltimestate.maxTshoot, thermaltimestate.phaseValue);

                //Loïc 10/26/2016 possibility to work with air temperature for process sensitive to heat stress (leaf scenescence acceleration)
                if (UseAirTemperatureForSenescence == 1) thermaltimestate.deltaTT[(int)SiriusQualityThermalTime.ThermalTimeState.Delta.LeafSenescence] = GetLeafSenescenceThermalTimeIncrement(0, thermaltimestate.minTair, thermaltimestate.maxTair, SenAccT, SenAccSlope);
                // #Andrea 12/01/2016 - There is no more the condition related to the number of leaves. Now that condition is just in the class ShootTemperature.cs and temperature passed from Universe are already "modified"
                else thermaltimestate.deltaTT[(int)SiriusQualityThermalTime.ThermalTimeState.Delta.LeafSenescence] = GetLeafSenescenceThermalTimeIncrement(0, thermaltimestate.minTshoot, thermaltimestate.maxTshoot, SenAccT, SenAccSlope);

                // #Andrea 12/01/2016 - There is no more the condition related to the number of leaves. Now that condition is just in the class ShootTemperature.cs and temperature passed from Universe are already "modified"
                // Canopy fields are calculated from shoot temperature only when leaf # reach MaxLeafSoil.
                thermaltimestate.deltaTT[(int)SiriusQualityThermalTime.ThermalTimeState.Delta.Remobilization] = GetRemobNonLinearThermalTimeIncrement(thermaltimestate.minTshoot, thermaltimestate.maxTshoot);

                ///Cumul daily delta thermal times.
                for (var i = 0; i < Enum.GetNames(typeof(SiriusQualityThermalTime.ThermalTimeState.Delta)).Length; ++i)
                {
                    thermaltimestate.cumulTT[i] += thermaltimestate.deltaTT[i];
                }

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section1 
			}

				

	#endregion


				//GENERATED CODE END - PLACE YOUR CUSTOM CODE BELOW - Section2
				//Code written below will not be overwritten by a future code generation
                /// <summary>Calculate thermal time increment based on non-linear approach</summary>
                /// <param name="canopyTmin">Canopy minimum temperature</param>
                /// <param name="canopyTmax">Canopy maximum temperature</param>
                /// <param name="currentPhase">Current phenological stage</param>
                /// <returns></returns>
                private double GetNonLinearThermalTimeIncrement(double canopyTmin, double canopyTmax, double[] shootHourlyTemp, double currentPhaseValue)
                {

                    double NonLinearThermalTimeIncrement = 0;
                    WangEngel nonlinearWangEngel = new WangEngel();

                    double TresponseMin;
                    double TresponseOpt;
                    double TresponseMax;
                    double TresponseShape;

                    if (currentPhaseValue >= 4)
                    {
                        TresponseMin = PostAnthesisTmin;
                        TresponseOpt = PostAnthesisTopt;
                        TresponseMax = PostAnthesisTmax;
                        TresponseShape = PostAnthesisShape;
                    }
                    else
                    {
                        TresponseMin = PreAnthesisTmin;
                        TresponseOpt = PreAnthesisTopt;
                        TresponseMax = PreAnthesisTmax;
                        TresponseShape = PreAnthesisShape;
                    }
                    
                    if (switchMaize ==1) //test to fit the maize response with a WangEngel curve
                    {
                        if (shootHourlyTemp == null) //no hourly temperatures
                        {
                            // Subroutine modified from ARCWHEAT1, Proportion that each 3-h period during the day contributes to the thermal time for that day.
                            // The numbers in array comes to 1/2*(1+cos 90/8 (2*r-1)) where r corresponds to the array index of the item here.
                            ///<summary>daily integral of thermal time calculated as the sum of cosinusoidal variation between maximum and minimum temperatures
                            ///</summary>
                            double[] Tmfac = { 0.98, 0.91, 0.8, 0.6, 0.37, 0.22, 0.1, 0.02 };
                            for (var i = 0; i < Tmfac.Count(); ++i)
                            {
                                var htemp = canopyTmin + (canopyTmax - canopyTmin) * Tmfac[i];

                                if (htemp > 0 & htemp < 44)
                                {
                                    NonLinearThermalTimeIncrement += maizetempResponse(htemp);
                                }
                            }
                            return NonLinearThermalTimeIncrement / Tmfac.Count();
                        }
                        else
                        {
                            for (var i = 0; i < 24; ++i)
                            {
                                var htemp = shootHourlyTemp[i];

                                if (htemp > 0 & htemp < 44)
                                {
                                    NonLinearThermalTimeIncrement += maizetempResponse(htemp);
                                }
                                else
                                {
                                    NonLinearThermalTimeIncrement += 0;
                                }
                            }
                            return NonLinearThermalTimeIncrement / 24;
                        }
                    }
                    else
                    {

                        if (shootHourlyTemp == null) //no hourly temperatures
                        {
                            // Subroutine modified from ARCWHEAT1, Proportion that each 3-h period during the day contributes to the thermal time for that day.
                            // The numbers in array comes to 1/2*(1+cos 90/8 (2*r-1)) where r corresponds to the array index of the item here.
                            ///<summary>daily integral of thermal time calculated as the sum of cosinusoidal variation between maximum and minimum temperatures
                            ///</summary>
                            double[] Tmfac = { 0.98, 0.91, 0.8, 0.6, 0.37, 0.22, 0.1, 0.02 };
                            for (var i = 0; i < Tmfac.Count(); ++i)
                            {
                                var htemp = canopyTmin + (canopyTmax - canopyTmin) * Tmfac[i];

                                if (htemp > TresponseMin & htemp < TresponseMax)
                                {
                                    NonLinearThermalTimeIncrement += TresponseOpt * nonlinearWangEngel.Calculate(TresponseMin, TresponseOpt, TresponseMax, TresponseShape, htemp);
                                }
                            }
                            return NonLinearThermalTimeIncrement / Tmfac.Count();
                        }
                        else
                        {
                            for (var i = 0; i < 24; ++i)
                            {
                                var htemp = shootHourlyTemp[i];

                                if (htemp > TresponseMin & htemp < TresponseMax)
                                {
                                    NonLinearThermalTimeIncrement += TresponseOpt * nonlinearWangEngel.Calculate(TresponseMin, TresponseOpt, TresponseMax, TresponseShape, htemp);
                                }
                                else
                                {
                                    NonLinearThermalTimeIncrement += 0;
                                }
                            }
                            return NonLinearThermalTimeIncrement / 24;
                        }
                    }

                }

                /// <summary>response to the temperature for the maize model</summary>
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

                /// <summary>Calculate physiological thermal time increment based on non-linear approach</summary>
                /// <param name="canopyTmin">Canopy minimum temperature</param>
                /// <param name="canopyTmax">Canopy maximum temperature</param>
                /// <param name="currentPhase">Current phenological stage</param>
                /// <returns></returns>
                private double GetNonLinearPhysThermalTimeIncrement(double canopyTmin, double canopyTmax, double currentPhaseValue)
                {
                    double[] Tmfac = { 0.98, 0.91, 0.8, 0.6, 0.37, 0.22, 0.1, 0.02 };
                    double NonLinearPhysThermalTimeIncrement = 0;
                    WangEngel nonlinearWangEngel = new WangEngel();

                    double TresponseMin;
                    double TresponseOpt;
                    double TresponseMax;
                    double TresponseShape;

                    if (currentPhaseValue >= 4)
                    {
                        TresponseMin = PostAnthesisTmin;
                        TresponseOpt = PostAnthesisTopt;
                        TresponseMax = PostAnthesisTmax;
                        TresponseShape = PostAnthesisShape;
                    }
                    else
                    {
                        TresponseMin = PreAnthesisTmin;
                        TresponseOpt = PreAnthesisTopt;
                        TresponseMax = PreAnthesisTmax;
                        TresponseShape = PreAnthesisShape;
                    }

                    for (var i = 0; i < Tmfac.Count(); ++i)
                    {
                        var htemp = canopyTmin + (canopyTmax - canopyTmin) * Tmfac[i];

                        if (htemp > TresponseMin & htemp < TresponseMax)
                        {
                            var ThermTimeAct = nonlinearWangEngel.Calculate(TresponseMin, TresponseOpt, TresponseMax, TresponseShape, htemp);
                            var ThermTime20 = nonlinearWangEngel.Calculate(TresponseMin, TresponseOpt, TresponseMax, TresponseShape, 20);
                            NonLinearPhysThermalTimeIncrement += ThermTimeAct / ThermTime20;
                        }
                    }
                    return NonLinearPhysThermalTimeIncrement / Tmfac.Count();
                }

                /// <summary>Calculate thermal time increment for DM and N remobilization based on non-linear approach</summary>
                /// <param name="canopyTmin">Canopy minimum temperature</param>
                /// <param name="canopyTmax">Canopy maximum temperature</param>
                /// <returns></returns>
                private double GetRemobNonLinearThermalTimeIncrement(double canopyTmin, double canopyTmax)
                {
                    // Subroutine modified from ARCWHEAT1, Proportion that each 3-h period during the day contributes to the thermal time for that day.
                    // The numbers in array comes to 1/2*(1+cos 90/8 (2*r-1)) where r corresponds to the array index of the item here.
                    ///<summary>daily integral of thermal time calculated as the sum of cosinusoidal variation between maximum and minimum temperatures
                    ///</summary>
                    double[] Tmfac = { 0.98, 0.91, 0.8, 0.6, 0.37, 0.22, 0.1, 0.02 };
                    double NonLinearThermalTimeIncrement = 0;
                    WangEngel nonlinearWangEngel = new WangEngel();

                    for (var i = 0; i < Tmfac.Count(); ++i)
                    {
                        var htemp = canopyTmin + (canopyTmax - canopyTmin) * Tmfac[i];

                        if (htemp > LUETmin & htemp < LUETmax)
                        {
                            NonLinearThermalTimeIncrement += LUETopt * nonlinearWangEngel.Calculate(LUETmin, LUETopt, LUETmax, LUETshape, htemp);
                        }
                    }
                    return NonLinearThermalTimeIncrement / Tmfac.Count();
                }

                /// <summary>
                /// Calculate soil thermal time increment based on tbase temperature
                /// </summary>
                /// <param name="tbase">Base temperature</param>
                /// <param name="tmin">Min temperature</param>
                /// <param name="tmax">Max temperature</param>
                /// <returns></returns>
                private double GetLinearThermalTimeIncrement(double tbase, double tmin, double tmax)
                {
                    // Subroutine modified from ARCWHEAT1, Proportion that each 3-h period during the day contributes to the thermal time for that day.
                    // The numbers in array comes to 1/2*(1+cos 90/8 (2*r-1)) where r corresponds to the array index of the item here.
                    ///<summary>daily integral of thermal time calculated as the sum of cosinusoidal variation between maximum and minimum temperatures
                    ///</summary>
                    double[] Tmfac = { 0.98, 0.91, 0.8, 0.6, 0.37, 0.22, 0.1, 0.02 };
                    double ThermalTimeIncrement = 0;
                    for (var i = 0; i < Tmfac.Count(); ++i)
                    {
                        var htemp = tmin + (tmax - tmin) * Tmfac[i];
                        if (htemp > tbase)
                        {
                            ThermalTimeIncrement += htemp - tbase;
                        }
                    }
                    return ThermalTimeIncrement / Tmfac.Count();
                }

                ///<summary>Calculate thermal time increment base on tbase temperature</summary>
                ///<param name="tbase">Base temperature</param>
                ///<param name="tmin">Min temperature</param>
                ///<param name="tmax">Max temperature</param>
                ///<returns>LeafSenescenceThermalTimeIncrement/8</returns>
                private double GetLeafSenescenceThermalTimeIncrement(double tbase, double canopyTmin, double canopyTmax, double SenTempThreshold, double SenSlope)
                {
                    double[] Tmfac = { 0.98, 0.91, 0.8, 0.6, 0.37, 0.22, 0.1, 0.02 };
                    double LeafSenescenceThermalTimeIncrement = 0;
                    double leafSenescenceFactor;

                    for (var i = 0; i < Tmfac.Count(); ++i)
                    {
                        var htemp = canopyTmin + (canopyTmax - canopyTmin) * Tmfac[i];

                        leafSenescenceFactor = 1 + Math.Max(0, htemp - SenTempThreshold) * SenSlope;
                        if (htemp > tbase)
                        {
                            LeafSenescenceThermalTimeIncrement += ((htemp * leafSenescenceFactor - tbase));
                        }
                    }
                    return LeafSenescenceThermalTimeIncrement / Tmfac.Count();
                }

                
                /// <summary>
                /// copy constructor. We only need to copy the parameters (the strategies being stateless)
                /// </summary> 
                public CalculateDailyThermalTime(CalculateDailyThermalTime toCopy)
                    : this()
                    {
                        switchMaize= toCopy.switchMaize;
                        SenAccT = toCopy.SenAccT;
                        SenAccSlope = toCopy.SenAccSlope;
                        PreAnthesisTmin = toCopy.PreAnthesisTmin;
                        PreAnthesisTmax = toCopy.PreAnthesisTmax;
                        PreAnthesisTopt = toCopy.PreAnthesisTopt;
                        PreAnthesisShape = toCopy.PreAnthesisShape;
                        PostAnthesisTmin = toCopy.PostAnthesisTmin;
                        PostAnthesisTmax = toCopy.PostAnthesisTmax;
                        PostAnthesisTopt = toCopy.PostAnthesisTopt;
                        PostAnthesisShape = toCopy.PostAnthesisShape;
                        LUETmin = toCopy.LUETmin;
                        LUETmax = toCopy.LUETmax;
                        LUETopt = toCopy.LUETopt;
                        LUETshape = toCopy.LUETshape;
                        UseAirTemperatureForSenescence = toCopy.UseAirTemperatureForSenescence;

                    }

                public void Init(SiriusQualityThermalTime.ThermalTimeState thermaltimestate)
                {


                    for (var i = 0; i < Enum.GetNames(typeof(SiriusQualityThermalTime.ThermalTimeState.Delta)).Length; ++i)
                    {
                        thermaltimestate.cumulTT[i] =0.0;
                        thermaltimestate.deltaTT[i] = 0.0;
                    }

                }

				//End of custom code. Do not place your custom code below. It will be overwritten by a future code generation.
				//PLACE YOUR CUSTOM CODE ABOVE - GENERATED CODE START - Section2 
	}
}
