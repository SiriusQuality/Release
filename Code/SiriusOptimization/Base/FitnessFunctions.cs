// Classes define in this file :
// FitnessFunctions
// RunInfo
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiriusModel.InOut;
using SiriusModel.InOut.Base;
using SiriusModel.Model.Phenology;
using SiriusModel.Model;
using SiriusModel.Model.CropModel;
using SiriusModel;
using SiriusQualityPhenology;

namespace SiriusOptimization.Base
{
    public class FitnessFunctions
    {
        #region Attributes

        public Dictionary<string, Dictionary<string, double>> ParamValueDictio { get; set; }  //submodelName (varietyName), parameterName, parameterValue
        public int paramValueDictioSize;//number of parameters
        public Dictionary<string, string> ParamSubModel { get; set; }//parameterName, submodelType
        public Dictionary<RunInfo, List<KeyValuePair<OptiObservation, Tuple<DateTime, double>>>> ObservationsValue { get; set; }

        public Run run;

        public List<RunInfo> RunInfos { get; set; }
        
             
        public HashSet<ManagementItem> Managements { get; set; }
        public HashSet<RunOptionItem> RunOptions { get; set; }
        public HashSet<SiteItem> Sites { get; set; }
        public HashSet<SoilItem> Soils { get; set; }
        public HashSet<CropParameterItem> Varieties { get; set; }
        public HashSet<CropParameterItem> NonVarieties { get; set; }

        public FitnessMode mode;

        private bool multiMultiYear;
        private int multiFirstYear;
        private int multiLastYear;
        private List<OptiObservation> observList_ { get; set; }
        private ObservationItem observItem_ { get; set; }


        private bool optimizeManagementParameter; //true if there is a management parameter in the one we try to optimize
        private bool optimizeSoilParameter;
        //Debug
        //private bool optimizeVarietalParameter;
        //private bool optimizeNonVarietalParameter;
        public bool optimizeVarietalParameter;
        public bool optimizeNonVarietalParameter;
        //Debug
        //save the number of time we added a difference (observed - simulated ) for each kind of observation. Used for calculating the rmse ... . 
        //This takes in account the number of run where the observation appears as well as the number of years
        private Dictionary<string, Dictionary<string, int>> fitnessModeValueSize;

        #endregion
        
        #region Constructors

        public FitnessFunctions()
        {
            this.mode = FitnessMode.MRE; // ToDo à modifier depuis l'interface utilisateur
            this.ParamValueDictio = new Dictionary<string, Dictionary<string, double>>();
            paramValueDictioSize = 0;
            this.ParamSubModel = new Dictionary<string, string>();
            this.ObservationsValue = new Dictionary<RunInfo, List<KeyValuePair<OptiObservation, Tuple<DateTime, double>>>>();

            optimizeManagementParameter = false;
            optimizeSoilParameter = false;
            optimizeVarietalParameter = false;
            optimizeNonVarietalParameter = false;

            // Soil Parameters
            this.ParamSubModel.Add("Kq", "Soil");
            this.ParamSubModel.Add("Ko", "Soil");
            this.ParamSubModel.Add("No", "Soil");
            this.ParamSubModel.Add("MinNir", "Soil");
            this.ParamSubModel.Add("Ndp", "Soil");
            // Management Parameters
            this.ParamSubModel.Add("SowingDate", "Management");
            this.ParamSubModel.Add("TotalNi", "Management");
            this.ParamSubModel.Add("CO2", "Management");
            this.ParamSubModel.Add("SowingDensity", "Management");
            this.ParamSubModel.Add("TopNi", "Management");
            this.ParamSubModel.Add("MidNi", "Management");
            this.ParamSubModel.Add("SoilWaterDeficit", "Management");
            this.ParamSubModel.Add("TargetFertileShootNumber", "Management");

            run = new Run();

            this.RunInfos = new List<RunInfo>();

            this.Managements = new HashSet<ManagementItem>();
            this.RunOptions = new HashSet<RunOptionItem>();
            this.Sites = new HashSet<SiteItem>();
            this.Soils = new HashSet<SoilItem>();
            this.Varieties = new HashSet<CropParameterItem>();
            this.NonVarieties = new HashSet<CropParameterItem>();   
   
            multiMultiYear= false;
            multiFirstYear= 0;
            multiLastYear =0;
        }

        public FitnessFunctions(RunItem runItem, bool singleRun, List<OptiParameter> ParamList, List<OptiObservation> ObservList, ObservationItem ObservItem)
            : this()
        {
            if (runItem == null )
            {
                throw new Exception("RunItem is empty: Select a run in the combobox");
            }
            this.RunInfos = RunDefinitions(runItem, singleRun).ToList();
            multiMultiYear = runItem.MultiMultiYear;
            multiFirstYear = runItem.MultiFirstYear;
            multiLastYear = runItem.MultiLastYear;

            observItem_ = ObservItem;
            observItem_.updateObservationDictionary();

            observList_ = ObservList;

            foreach (var item in this.RunInfos)
            {
                item.Management = item.Management.Clone();
                item.RunOptions = item.RunOptions.Clone();
                item.Site = item.Site.Clone();
                item.Soil = item.Soil.Clone();
                item.Variety = item.Variety.Clone();
                item.NonVariety = item.NonVariety.Clone();

                this.Managements.Add(item.Management);
                this.RunOptions.Add(item.RunOptions);
                this.Sites.Add(item.Site);
                this.Soils.Add(item.Soil);
                this.Varieties.Add(item.Variety);
                this.NonVarieties.Add(item.NonVariety);
            }

            // Update of ParamSubModel

            foreach (var item in this.Varieties.First().ParamValue.Keys)
                this.ParamSubModel.Add(item, "Variety");

            foreach (var item in this.NonVarieties.First().ParamValue.Keys)
                this.ParamSubModel.Add(item, "NonVariety");

            
            // Initialisation of ParamList
            List<string> temp = new List<string>();
            foreach (var item in ParamList)
            {    
                switch (this.ParamSubModel[item.Name])
                {
                    case "Management":
                        temp = Managements.Select(ii => ii.Name).ToList(); optimizeManagementParameter =true;
                        break;
                    case "Soil":
                        temp = Soils.Select(ii => ii.Name).ToList(); optimizeSoilParameter =true;
                        break;
                    case "NonVariety":
                        temp = NonVarieties.Select(ii => ii.Name).ToList(); optimizeNonVarietalParameter =true;
                        break;
                    case "Variety":
                        temp = Varieties.Select(ii => ii.Name).ToList(); optimizeVarietalParameter =true;
                        break;
                    default: break;
                }

                while (temp.Count != 0)
                {
                    if (!ParamValueDictio.ContainsKey(temp[0]))
                    {
                        ParamValueDictio.Add(temp[0], new Dictionary<string, double>());
                    }
                    if (!ParamValueDictio[temp[0]].ContainsKey(item.Name))// should always be true
                    {
                        ParamValueDictio[temp[0]].Add(item.Name, 0);
                        paramValueDictioSize++;
                    }
                    
                    string typeToRemove = temp[0];
                    temp.RemoveAll(elem => elem == typeToRemove);
                }
                
            }
        }

        #endregion

        #region Methods

        private IEnumerable<RunInfo> RunDefinitions(RunItem runItem, bool singleRun)
        {
            var multiRuns = runItem.MultiRuns;

            if (!singleRun && multiRuns.Count > 0)
            {
                foreach (var item in multiRuns)
                {
                    var info = new RunInfo();
                    RunCore.GetItems(runItem.Name, item.ManagementItemSelected, item.ParameterItemSelected, item.RunOptionItemSelected, item.SiteItemSelected, item.SoilItemSelected, item.VarietyItem,
                        ref info.Management, ref info.NonVariety, ref info.RunOptions, ref info.Site, ref info.Soil, ref info.Variety);
                    info.Name = item.ManagementItemSelected;

                    yield return info;
                }
            }
            else
            {
                var info = new RunInfo();
                RunCore.GetItems(runItem.Name, runItem.Normal.ManagementItem, runItem.Normal.ParameterItem, runItem.Normal.RunOptionItem, runItem.Normal.SiteItem, runItem.Normal.SoilItem, runItem.Normal.VarietyItem,
                    ref info.Management, ref info.NonVariety, ref info.RunOptions, ref info.Site, ref info.Soil, ref info.Variety);
                info.Name = runItem.Normal.ManagementItem;

                yield return info;
            }
        }

        public double Compute(out Dictionary<string, Dictionary<string, double>> fitnessModeValue, out Dictionary<string, Dictionary<string, double>> absMaxErr, bool needOuput)
        {
            try {

            #region Set Parameters

                foreach (KeyValuePair<string, Dictionary<string, double>> subModel in this.ParamValueDictio)
            {
                foreach (KeyValuePair<string, double> parameter in subModel.Value)
                {
                    switch (this.ParamSubModel[parameter.Key])
                    {
                        case "Management":
                            foreach (var management in this.Managements)
                            {
                                if (subModel.Key == management.Name)
                                {
                                    switch (parameter.Key)
                                    {
                                        case "TotalNi": management.TotalNi = parameter.Value; break;
                                        case "CO2": management.CO2 = parameter.Value; break;
                                        case "SowingDensity": management.SowingDensity = parameter.Value; break;
                                        case "TopNi": management.TopNi = parameter.Value; break;
                                        case "MidNi": management.MidNi = parameter.Value; break;
                                        case "SoilWaterDeficit": management.SoilWaterDeficit = parameter.Value; break;
                                        case "TargetFertileShootNumber": management.TargetFertileShootNumber = parameter.Value; break;
                                        default: break;
                                    }
                                }
                            }
                            break;
                        case "Soil":
                            foreach (var soil in Soils)
                            {
                                if (subModel.Key == soil.Name)
                                {
                                    switch (parameter.Key)
                                    {
                                        case "Kq": soil.Kq = parameter.Value; break;
                                        case "Ko": soil.Ko = parameter.Value; break;
                                        case "No": soil.No = parameter.Value; break;
                                        case "MinNir": soil.MinNir = parameter.Value; break;
                                        case "Ndp": soil.Ndp = parameter.Value; break;
                                        default: break;
                                    }
                                }
                            }
                            break;
                        case "Variety":
                            foreach (var variety in Varieties)
                            {
                                if (subModel.Key == variety.Name)
                                    variety.ParamValue[parameter.Key] = parameter.Value;
                            }
                            break;
                        case "NonVariety":
                            foreach (var nonVariety in NonVarieties)
                            {
                                if (subModel.Key == nonVariety.Name)
                                    nonVariety.ParamValue[parameter.Key] = parameter.Value;
                            }
                            break;
                        default: break;
                    }
                }
            }

            #endregion

            #region Compute Fitness
            double fitness = 0;
            if (needOuput)
            {
                fitnessModeValue = new Dictionary<string, Dictionary<string, double>>();
                absMaxErr = new Dictionary<string, Dictionary<string, double>>();
                fitnessModeValueSize = new Dictionary<string, Dictionary<string, int>>();
            }
            else
            {
                fitnessModeValue = null;
                absMaxErr = null;
                fitnessModeValueSize = null;
            }



            foreach (var current in RunInfos)
            {
                if (!multiMultiYear) { multiLastYear = multiFirstYear = current.Management.SowingDate.Year; }
                for (int j = multiFirstYear; j <= multiLastYear; j++)
                {
                    if (multiMultiYear)
                    {
                        foreach (var dateApp in current.Management.DateApplications)
                        {
                            dateApp.Date = new DateTime(j + (dateApp.Date.Year - current.Management.SowingDate.Year), dateApp.Date.Month, dateApp.Date.Day);
                        }

                        var yy = current.Site.MaxSowingDate.Year - current.Site.MinSowingDate.Year;
                        current.Management.SowingDate = new DateTime(j, current.Management.SowingDate.Month, current.Management.SowingDate.Day);
                        current.Site.MinSowingDate = new DateTime(j, current.Site.MinSowingDate.Month, current.Site.MinSowingDate.Day);
                        current.Site.MaxSowingDate = new DateTime(j + yy, current.Site.MaxSowingDate.Month, current.Site.MaxSowingDate.Day);
                    }
                    run.Start(current.Variety, current.Soil, current.Site, current.Management, current.NonVariety, current.RunOptions);

                    //get observations
                    List<KeyValuePair<OptiObservation, Tuple<DateTime, List<double>>>> tempDico = new List<KeyValuePair<OptiObservation, Tuple<DateTime, List<double>>>>();
                    List<Tuple<DateTime, List<double>>> getObsResult = new List<Tuple<DateTime, List<double>>>();
                    foreach (var observation in observList_)
                    {
                        double tempValue = observItem_.getObservation(observation.Name, current.Management.Name, current.Management.SowingDate, current.Variety.Name, current.Soil.Name, current.Site.Name, getObsResult);

                       if (tempValue != -999)
                       {
                           for (int i = 0; i < getObsResult.Count; i++)
                           {
                               if (getObsResult[i].Item2[0] != -999)
                               {
                                   tempDico.Add(new KeyValuePair<OptiObservation, Tuple<DateTime, List<double>>>(observation, getObsResult[i]));
                               }
                           }
                       }

                        getObsResult.Clear();
                    }
                    double sumWeight = 0;
                    foreach (KeyValuePair<OptiObservation, Tuple<DateTime, List<double>>> pair in tempDico)
                    {
                        sumWeight += pair.Key.Weight;
                    }
                    //calcul fitness
                    foreach (KeyValuePair<OptiObservation, Tuple<DateTime, List<double>>> pair in tempDico)
                    {
                        double predicted = getPredictions(run, pair.Key.Name, pair.Value.Item1, pair.Value.Item2);
                        fitness += difference(predicted, pair.Value.Item2[0], pair.Key.Weight) / sumWeight;

                        if (needOuput)
                        {
                            //varietal
                            if (optimizeVarietalParameter)
                            {
                                if (!fitnessModeValue.ContainsKey(current.Variety.Name))
                                {
                                    fitnessModeValue.Add(current.Variety.Name, new Dictionary<string, double>());
                                    fitnessModeValueSize.Add(current.Variety.Name, new Dictionary<string, int>());
                                    absMaxErr.Add(current.Variety.Name, new Dictionary<string, double>());
                                }
                                if (!fitnessModeValue[current.Variety.Name].ContainsKey(pair.Key.Name))
                                {
                                    fitnessModeValue[current.Variety.Name].Add(pair.Key.Name, 0);
                                    fitnessModeValueSize[current.Variety.Name].Add(pair.Key.Name, 0);
                                    absMaxErr[current.Variety.Name].Add(pair.Key.Name, 0);
                                }
                                fitnessModeValue[current.Variety.Name][pair.Key.Name] += difference(predicted, pair.Value.Item2[0], 1);
                                fitnessModeValueSize[current.Variety.Name][pair.Key.Name] += 1;
                                absMaxErr[current.Variety.Name][pair.Key.Name] = Math.Max(Math.Abs(predicted - pair.Value.Item2[0]), absMaxErr[current.Variety.Name][pair.Key.Name]);
                            }

                            //non varietal
                            if (optimizeNonVarietalParameter)
                            {
                                if (!fitnessModeValue.ContainsKey(current.NonVariety.Name))
                                {
                                    fitnessModeValue.Add(current.NonVariety.Name, new Dictionary<string, double>());
                                    fitnessModeValueSize.Add(current.NonVariety.Name, new Dictionary<string, int>());
                                    absMaxErr.Add(current.NonVariety.Name, new Dictionary<string, double>());
                                }
                                if (!fitnessModeValue[current.NonVariety.Name].ContainsKey(pair.Key.Name))
                                {
                                    fitnessModeValue[current.NonVariety.Name].Add(pair.Key.Name, 0);
                                    fitnessModeValueSize[current.NonVariety.Name].Add(pair.Key.Name, 0);
                                    absMaxErr[current.NonVariety.Name].Add(pair.Key.Name, 0);
                                }
                                fitnessModeValue[current.NonVariety.Name][pair.Key.Name] += difference(predicted, pair.Value.Item2[0], 1);
                                fitnessModeValueSize[current.NonVariety.Name][pair.Key.Name] += 1;
                                absMaxErr[current.NonVariety.Name][pair.Key.Name] = Math.Max(Math.Abs(predicted - pair.Value.Item2[0]), absMaxErr[current.NonVariety.Name][pair.Key.Name]);
                            }

                            //management
                            if (optimizeManagementParameter)
                            {
                                if (!fitnessModeValue.ContainsKey(current.Management.Name))
                                {
                                    fitnessModeValue.Add(current.Management.Name, new Dictionary<string, double>());
                                    fitnessModeValueSize.Add(current.Management.Name, new Dictionary<string, int>());
                                    absMaxErr.Add(current.Management.Name, new Dictionary<string, double>());
                                }
                                if (!fitnessModeValue[current.Management.Name].ContainsKey(pair.Key.Name))
                                {
                                    fitnessModeValue[current.Management.Name].Add(pair.Key.Name, 0);
                                    fitnessModeValueSize[current.Management.Name].Add(pair.Key.Name, 0);
                                    absMaxErr[current.Management.Name].Add(pair.Key.Name, 0);
                                }
                                fitnessModeValue[current.Management.Name][pair.Key.Name] += difference(predicted, pair.Value.Item2[0], 1);
                                fitnessModeValueSize[current.Management.Name][pair.Key.Name] += 1;
                                absMaxErr[current.Management.Name][pair.Key.Name] = Math.Max(Math.Abs(predicted - pair.Value.Item2[0]), absMaxErr[current.Management.Name][pair.Key.Name]);
                            }

                            //soil
                            if (optimizeSoilParameter)
                            {
                                if (!fitnessModeValue.ContainsKey(current.Soil.Name))
                                {
                                    fitnessModeValue.Add(current.Soil.Name, new Dictionary<string, double>());
                                    fitnessModeValueSize.Add(current.Soil.Name, new Dictionary<string, int>());
                                    absMaxErr.Add(current.Soil.Name, new Dictionary<string, double>());
                                }
                                if (!fitnessModeValue[current.Soil.Name].ContainsKey(pair.Key.Name))
                                {
                                    fitnessModeValue[current.Soil.Name].Add(pair.Key.Name, 0);
                                    fitnessModeValueSize[current.Soil.Name].Add(pair.Key.Name, 0);
                                    absMaxErr[current.Soil.Name].Add(pair.Key.Name, 0);
                                }
                                fitnessModeValue[current.Soil.Name][pair.Key.Name] += difference(predicted, pair.Value.Item2[0], 1);
                                fitnessModeValueSize[current.Soil.Name][pair.Key.Name] += 1;
                                absMaxErr[current.Soil.Name][pair.Key.Name] = Math.Max(Math.Abs(predicted - pair.Value.Item2[0]), absMaxErr[current.Soil.Name][pair.Key.Name]);
                            }

                        }
                    }
                }
            }

        #endregion

            switch (this.mode)
            {
                case FitnessMode.RMSE:
                    if (needOuput)
                    {
                        foreach (KeyValuePair<string, Dictionary<string, double>> submodel in absMaxErr) // cannot foreach on rmse while modifing it so we use absMaxErr which has the same keys by design
                        {
                            foreach (KeyValuePair<string, double> pair in submodel.Value)
                            {
                                fitnessModeValue[submodel.Key][pair.Key] = Math.Sqrt(fitnessModeValue[submodel.Key][pair.Key] / fitnessModeValueSize[submodel.Key][pair.Key] );
                            }
                        }
                    }
                    return Math.Exp(-Math.Sqrt(fitness / this.RunInfos.Count / (1 + multiLastYear - multiFirstYear)));
                    break;
                case FitnessMode.MRE:
                    if (needOuput)
                    {
                        foreach (KeyValuePair<string, Dictionary<string, double>> submodel in absMaxErr) // cannot foreach on rmse while modifing it so we use absMaxErr which has the same keys by design
                        {
                            foreach (KeyValuePair<string, double> pair in submodel.Value)
                            {
                                fitnessModeValue[submodel.Key][pair.Key] = fitnessModeValue[submodel.Key][pair.Key] / fitnessModeValueSize[submodel.Key][pair.Key];
                            }
                        }
                    }
                    return Math.Exp(-fitness / this.RunInfos.Count / (1 + multiLastYear - multiFirstYear));
                    break;
                case FitnessMode.RMSRE:
                    if (needOuput)
                    {
                        foreach (KeyValuePair<string, Dictionary<string, double>> submodel in absMaxErr) // cannot foreach on rmse while modifing it so we use absMaxErr which has the same keys by design
                        {
                            foreach (KeyValuePair<string, double> pair in submodel.Value)
                            {
                                fitnessModeValue[submodel.Key][pair.Key] = Math.Sqrt(fitnessModeValue[submodel.Key][pair.Key] / fitnessModeValueSize[submodel.Key][pair.Key] );
                            }
                        }
                    }
                    return Math.Exp(-Math.Sqrt(fitness / this.RunInfos.Count / (1 + multiLastYear - multiFirstYear)));
                    break;
                default:
                    return -1;
            }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        ///<summary>
        ///Get predictions from the run
        ///</summary>
        ///<param name="localRun"></param>
        ///<returns></returns>
        private double getPredictions(Run localRun, string name, DateTime date, List<double> observationInfos)
        {
            Dictionary<string, double> PredictionValues = new Dictionary<string, double>();
            double value;
            var universes = localRun.SavedUniverses;
            var universe = universes[universes.Count - 1];
            // get the universes.
            var sowing_Date = universe.Crop_.getDateOfStage(GrowthStage.ZC_00_Sowing);
            var emergence_Date = universe.Crop_.getDateOfStage(GrowthStage.ZC_10_Emergence);
            var anthesis_Date = universe.Crop_.getDateOfStage(GrowthStage.ZC_65_Anthesis);
            var anthesisUniverse = localRun.GetUniverse(anthesis_Date.Value);
            var maturity_Date = universe.Crop_.getDateOfStage(GrowthStage.ZC_92_Maturity);
            var heading_Date = universe.Crop_.getDateOfStage(GrowthStage.Heading);
            var endgrainfilling_Date = universe.Crop_.getDateOfStage(GrowthStage.ZC_91_EndGrainFilling);
            var maturityUniverse = localRun.GetUniverse(maturity_Date.Value);

            //find the universe which match the date
            Universe universeZero =universes[0];
            int index =(date - universeZero.CurrentDate).Days;
            if (index < 0 || index >= universes.Count) { index = universes.Count - 1; } //if date is wrong take the end date
            Universe predictionUniverse= universes[index];

                switch (name)
                {
                    case "Leaf area index":
                        value = predictionUniverse.Crop_.LaminaeAI;
                        break;
                    case "Green area index":
                        value = predictionUniverse.Crop_.GAI;
                        break;
                    case "Crop dry mass":
                        value = predictionUniverse.Crop_.OutputTotalDM*10;//change unit to match observation and output files
                        break;
                    case "Leaf dry mass":
                        value = (predictionUniverse.Crop_.OutputLaminaeDM + predictionUniverse.Crop_.OutputSheathDM)*10;//change unit to match observation and output files
                        break;
                    case "Leaf nitrogen":
                        value = (predictionUniverse.Crop_.OutputLaminaeN + predictionUniverse.Crop_.OutputSheathN)*10;//change unit to match observation and output files;
                        break;
                    case "Laminae dry mass":
                        value = predictionUniverse.Crop_.OutputLaminaeDM*10;//change unit to match observation and output files;
                        break;
                    case "Laminae nitrogen":
                        value = predictionUniverse.Crop_.OutputLaminaeN*10;//change unit to match observation and output files;;
                        break;
                    case "Specific leaf nitrogen":
                        value = predictionUniverse.Crop_.LaminaeSLN;
                        break;
                    case "Specific leaf dry mass":
                        value = predictionUniverse.Crop_.LaminaeSLW;
                        break;
                    case "Stem dry mass":
                        value = (predictionUniverse.Crop_.OutputSheathDM + predictionUniverse.Crop_.StemTotalDM)* 10;
                        break;
                    case "Stem nitrogen":
                        value = (predictionUniverse.Crop_.OutputSheathN+ predictionUniverse.Crop_.StemTotalN)* 10;
                        break;
                    case "True stem dry mass":
                        value = 10 * predictionUniverse.Crop_.StemTotalDM;
                        break;
                    case "True stem nitrogen":
                        value = 10 * predictionUniverse.Crop_.StemTotalN;
                        break;
                    case "Stem length":
                        value = predictionUniverse.Crop_.SumInternodesLength*Run.CMToM;
                        break;
                    case "Grain dry mass":
                        value = predictionUniverse.Crop_.GrainTotalDM* 10;
                        break;
                    case "Crop nitrogen":
                        value = predictionUniverse.Crop_.OutputTotalN* 10;
                        break;
                    case "Grain nitrogen":
                        value = predictionUniverse.Crop_.GrainTotalN* 10;
                        break;
                    case "Single grain dry mass":
                        value = predictionUniverse.Crop_.TotalDMperGrain;
                        break;
                    case "Maturity single grain dry mass":
                        value = maturityUniverse.Crop_.TotalDMperGrain;
                        break;
                    case "Single grain nitrogen":
                        value = predictionUniverse.Crop_.TotalNperGrain;
                        break;
                    case "Starch per grain":
                        value = predictionUniverse.Crop_.Starch;
                        break;
                    case "Albumins-globulins per grain":
                        value = predictionUniverse.Crop_.NalbGlo;
                        break;
                    case "Amphiphils per grain":
                        value = predictionUniverse.Crop_.Namp;
                        break;
                    case "Gliadins per grain":
                        value =predictionUniverse.Crop_.Ngli;
                        break;
                    case "Glutenins per grain":
                        value =predictionUniverse.Crop_.Nglu;
                        break;
                    case "Soil mineral N":
                        value = predictionUniverse.Soil_.CalculateTotalNBetweenTwoDepth(observationInfos[1], observationInfos[2]) * 10;
                        break;
                    case "Soil water":
                        value = predictionUniverse.Soil_.CalculateAvailableExcessWBetweenTwoDepth(observationInfos[1], observationInfos[2]) * Run.GwaterToMMwater;
                        break;
                    case "ZC10_Emergence":
                        value = emergence_Date.Value.ToOADate();
                        break;
                    case "ZC65_Anthesis":
                        value = anthesis_Date.Value.ToOADate();
                        break;
                    case "ZC92_Maturity":
                        value = maturity_Date.Value.ToOADate();
                        break;
                    case "ZC55_Heading":
                        value = heading_Date.Value.ToOADate();
                        break;
                    case "ZC91_End of grain filling":
                        value = endgrainfilling_Date.Value.ToOADate();
                        break;
                    case "Final leaf number":
                        value = maturityUniverse.Crop_.LeafNumber;
                        break;
                    case "Grain number":
                        value = maturityUniverse.Crop_.GrainNumber;
                        break;
                    case "Maturity shoot number":
                        value = maturityUniverse.Crop_.CanopyShootNumber;
                        break;
                    case "Maturity grain yield":
                        value = maturityUniverse.Crop_.GrainTotalDM *10;//change unit to match observation and output files
                        break;
                    case "Grain protein concentration":
                        value = maturityUniverse.Crop_.ProteinConcentration;
                        break;
                    case "Anthesis leaf area index":
                        value =anthesisUniverse.Crop_.LaminaeAI;
                        break;
                    case "Anthesis green area index":
                        value =anthesisUniverse.Crop_.GAI;
                        break;
                    case "Anthesis crop dry mass":
                        value =anthesisUniverse.Crop_.OutputTotalDM*10;//change unit to match observation and output files;
                        break;
                    case "Maturity crop dry mass":
                        value = maturityUniverse.Crop_.OutputTotalDM*10;//change unit to match observation and output files
                        break;
                    case "Anthesis leaf dry mass":
                        value = (anthesisUniverse.Crop_.OutputLaminaeDM + anthesisUniverse.Crop_.OutputSheathDM)*10;
                        break;
                    case "Maturity leaf dry mass":
                        value = (maturityUniverse.Crop_.OutputLaminaeDM + maturityUniverse.Crop_.OutputSheathDM)*10;
                        break;
                    case "Anthesis leaf nitrogen":
                        value = (anthesisUniverse.Crop_.OutputLaminaeN + anthesisUniverse.Crop_.OutputSheathN)*10;
                        break;
                    case "Maturity leaf nitrogen":
                        value =(maturityUniverse.Crop_.OutputLaminaeN + maturityUniverse.Crop_.OutputSheathN)*10;
                        break;
                    case "Anthesis laminae dry mass":
                        value = anthesisUniverse.Crop_.OutputLaminaeDM*10;
                        break;
                    case "Maturity laminae dry mass":
                        value = maturityUniverse.Crop_.OutputLaminaeDM*10;
                        break;
                    case "Anthesis laminae nitrogen":
                        value = anthesisUniverse.Crop_.OutputLaminaeN*10;
                        break;
                    case "Maturity laminae nitrogen":
                        value = maturityUniverse.Crop_.OutputLaminaeN*10;
                        break;
                    case "Anthesis specific leaf nitrogen":
                        value =anthesisUniverse.Crop_.LaminaeSLN;
                        break;
                    case "Anthesis specific leaf dry mass":
                        value =anthesisUniverse.Crop_.LaminaeSLW;
                        break;
                    case "Anthesis stem dry mass":
                        value = (anthesisUniverse.Crop_.OutputSheathDM + anthesisUniverse.Crop_.StemTotalDM)* 10;
                        break;
                    case "Maturity stem dry mass":
                        value = (maturityUniverse.Crop_.OutputSheathDM + maturityUniverse.Crop_.StemTotalDM)* 10;
                        break;
                    case "Anthesis stem nitrogen":
                        value = (anthesisUniverse.Crop_.OutputSheathN+ anthesisUniverse.Crop_.StemTotalN)* 10;
                        break;
                    case "Maturity stem nitrogen":
                        value = (maturityUniverse.Crop_.OutputSheathN+ maturityUniverse.Crop_.StemTotalN)* 10;
                        break;
                    case "Anthesis true stem dry mass":
                        value = 10 * anthesisUniverse.Crop_.StemTotalDM;
                        break;
                    case "Maturity true stem dry mass":
                        value = 10 * maturityUniverse.Crop_.StemTotalDM;
                        break;
                    case "Anthesis true stem nitrogen":
                        value = 10 * anthesisUniverse.Crop_.StemTotalN;
                        break;
                    case "Maturity true stem nitrogen":
                        value = 10 * maturityUniverse.Crop_.StemTotalN;
                        break;
                    case "Anthesis stem length":
                        value = anthesisUniverse.Crop_.SumInternodesLength;
                        break;
                    case "Anthesis ear dry mass":
                        value = 10 * anthesisUniverse.Crop_.EarDW;
                        break;
                    case "Anthesis crop nitrogen":
                        value =anthesisUniverse.Crop_.OutputTotalN*10;//change unit to match observation and output files;
                        break;
                    case "Maturity crop nitrogen":
                        value = maturityUniverse.Crop_.OutputTotalN*10;//change unit to match observation and output files
                        break;
                    case "Maturity grain nitrogen":
                        value = maturityUniverse.Crop_.GrainTotalN*10;//change unit to match observation and output files
                        break;
                    case "Maturity single grain nitrogen":
                        value = maturityUniverse.Crop_.TotalNperGrain;
                        break;
                    case "Maturity starch per grain":
                        value = maturityUniverse.Crop_.Starch;
                        break;
                    case "Maturity grain starch concentration":
                        value = 100 * maturityUniverse.Crop_.Starch / maturityUniverse.Crop_.TotalDMperGrain;
                        break;
                    case "Maturity albumins-globulins per grain":
                        value = maturityUniverse.Crop_.NalbGlo;
                        break;
                    case "Maturity amphiphils per grain":
                        value =maturityUniverse.Crop_.Namp;
                        break;
                    case "Maturity gliadins per grain":
                        value =maturityUniverse.Crop_.Ngli;
                        break;
                    case "Maturity glutenins per grain":
                        value =maturityUniverse.Crop_.Nglu;
                        break;
                    case "Post-anthesis crop n uptake (kgn/ha)":
                        value = predictionUniverse.Crop_.PostAnthesisNUptake*10;
                        break;
                    case "% gliadins at maturity (% of total grain n)":
                        value = maturityUniverse.Crop_.PercentGli;
                        break;
                    case "% gluteins at maturity (% of total grain n)":
                        value = maturityUniverse.Crop_.PercentGlu;
                        break;
                    case "Gliadins-to-gluteins ratio (dimensionless)":
                        value =maturityUniverse.Crop_.GliadinsToGluteins;
                        break;
                    case "DM harvest index":
                        value =maturityUniverse.Crop_.HarvestIndexDM;
                        break;
                    case "N harvest index":
                        value =maturityUniverse.Crop_.HarvestIndexN;
                        break;
                    case "Cumulative available soil nitrogen":
                        value = (maturityUniverse.Soil_.CalculateTotalN() + maturityUniverse.Soil_.SoilDeepLayer_.LostNitrogen + maturityUniverse.Crop_.CropTotalN)* 10;
                        break;
                    case "N leaching (kgn/ha)":
                        value =maturityUniverse.Soil_.SoilDeepLayer_.LostNitrogen*10;
                        break;
                    case "Cumulative water drainage (mm)":
                        value =maturityUniverse.Soil_.SoilDeepLayer_.LostWater* Run.GwaterToMMwater;
                        break;
                    case "N use efficiency (kgdm/kgn)":
                        value =maturityUniverse.NuseEfficiency;
                        break;
                    case "N utilisation efficiency (kgdm/kgn)":
                        value = maturityUniverse.Crop_.NutilisationEfficiency;
                        break;
                    case "N uptake efficiency (kgN/kgn)":
                        value = maturityUniverse.NuptakeEfficiency;
                        break;
                    case "Water use efficiency (kgdm/ha/mm)":
                        value = maturityUniverse.WaterUseEfficiency;
                        break;
                    case "Cumulative N mineralisation (kgN/ha) in soil profil":
                        value = maturityUniverse.Soil_.AccumulatedNitrogenMineralisation* 10;
                        break;
                    case "Cumulative N denitrification in soil profil (kgn/ha)":
                        value =maturityUniverse.Soil_.AccumulatedNitrogenDenitrification* 10;
                        break;
                    case "Available mineral soil N at maturity in soil profil (kgn/ha)":
                        value = maturityUniverse.Soil_.CalculateAvailableExcessN()* 10;
                        break;
                    case "Total mineral soil N at maturity in soil profil (kgn/ha)":
                        value = maturityUniverse.Soil_.CalculateTotalN()* 10;
                        break;
                    case "Available water at maturity in soil profil (mm)":  
                        value =maturityUniverse.Soil_.CalculateAvailableExcessW()* Run.GwaterToMMwater;
                        break;
                    case "Emerged leaf number":
                        value = predictionUniverse.Crop_.LeafNumber;
                        break;
                    default: value = 0; break;
                }

            return value;
        }

        private double difference(double predicted,double observed, double Weight)
        {
            double diff;

            switch (this.mode)
            {
                case FitnessMode.RMSE:
                    diff = Weight * Math.Pow((predicted - observed), 2);
                    break;
                case FitnessMode.MRE:
                    diff = Weight * Math.Abs(predicted - observed) / observed;
                    break;
                case FitnessMode.RMSRE:
                    diff = Weight * Math.Pow((predicted - observed)/observed, 2);
                    break;
                default: diff = 0; break;
            }

            return diff;
        }
        #endregion
    }

    public class RunInfo
    {
        public string Name;
        public ManagementItem Management;
        public CropParameterItem NonVariety;
        public RunOptionItem RunOptions;
        public SiteItem Site;
        public SoilItem Soil;
        public CropParameterItem Variety;
    }

    public enum FitnessMode
    {
        RMSE,
        MRE,
        RMSRE
    }
}
