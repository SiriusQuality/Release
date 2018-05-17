using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using SiriusModel.InOut;
using SiriusModel.InOut.Base;
using SiriusModel.InOut.OutputWriter;

namespace SiriusOptimization.Base
{
    public class OptimizationProblem
    {
        public OptimizationItem OptimizationParameters { get; set; }
        public ObservationItem ObservationsValue { get; set; }

        ///<summary>
        ///Optimization algorithm 
        ///</summary>
        public IAlgorithm algorithm;
        public FitnessFunctions fitnessFunctions;

        #region Parameters choosed in OptimizationView

        public OptimizationMode Opt_Mode
        { 
            get
            {
                var output = OptimizationMode.BatchRun;

                if (OptimizationParameters.SingleOrBatch)
                    output = OptimizationMode.SingleRun;

                return output;
            }
        }

        public RunItem Opt_RunItem { get { return OptimizationParameters.RunItem; } }

        ///<summary>
        ///List of the selected parameters
        ///</summary>
        public List<OptiParameter> Opt_Parameters { get { return OptimizationParameters.OptimizedParameter; } }

        ///<summary>
        ///List of the selected observations
        ///</summary>
        public List<OptiObservation> Opt_Observations { get { return OptimizationParameters.Observations; } }

        //List of the different submodel used in the run(s)
        private List<string> varietyNames;
        private List<string> managementNames;
        private List<string> soilNames;
        private List<string> allSubModelNames;
        private Dictionary<string, string> varietiesNonVariety;

        #endregion

        #region Constructors

        public OptimizationProblem()
        {
            
        }

        public OptimizationProblem(OptimizationItem inOptimizationItem)
        {
            varietiesNonVariety = new Dictionary<string, string>();
            OptimizationParameters = inOptimizationItem;
            ObservationsValue = OptimizationParameters.ObservationItem;
            fitnessFunctions = new FitnessFunctions(Opt_RunItem, (Opt_Mode == OptimizationMode.SingleRun), this.Opt_Parameters, this.Opt_Observations, ObservationsValue);

            string mode= "";
            switch (OptimizationParameters.SelectedObjFct)
            {
                case 0: fitnessFunctions.mode = FitnessMode.RMSE; mode = " RMSE"; break;
                case 1: fitnessFunctions.mode = FitnessMode.MRE; mode = " MRE"; break;
                case 2: fitnessFunctions.mode = FitnessMode.RMSRE; mode = " RMSRE"; break;
                default: throw new NotImplementedException();
            }

            // Selection of the algorithm
            switch (OptimizationParameters.SelectedAlglo)
            {
                case 0: algorithm = new CMAES.CMAESMain(this); break;
                case 1: algorithm = new NelderMeadSimplex.NelderMeadSimplex(this); break;
                default: throw new NotImplementedException();
            }

            // Mise en forme Book résultat
            this.OptimizationParameters.ResultBook.Clear();
            PageData resultPage = new PageData();
            resultPage.NewLine().Add("Algorithm : ").Add(algorithm.Name()).Add("Based on observations : ");
            foreach (string obs in Opt_Observations.Select(ii => ii.Name).ToList())
            {
                resultPage[0].Add(obs);
            }

            resultPage.NewLine().AddNull();
            resultPage.NewLine().Add("Variety").Add("Round").Add("Generation").Add("Fitness");

            foreach (OptiParameter param in Opt_Parameters)
                resultPage[2].Add(param.Name);

            //create the list of variety names and their non variety (sub species)
            varietyNames = new List<string>();
            if ((Opt_Mode == OptimizationMode.SingleRun))
            { 
                varietyNames.Add(Opt_RunItem.Normal.VarietyItem);
                varietiesNonVariety.Add(Opt_RunItem.Normal.VarietyItem, Opt_RunItem.Normal.ParameterItem);
            }
            else
            {
                foreach (var item in Opt_RunItem.MultiRuns)
                {
                    if (!varietyNames.Contains(item.VarietyItem)) 
                    {
                        varietyNames.Add(item.VarietyItem);
                        varietiesNonVariety.Add(item.VarietyItem, item.ParameterItem);
                    }
                }
            }


            //create the list of management names
            managementNames = new List<string>();
            if ((Opt_Mode == OptimizationMode.SingleRun))
            {
                managementNames.Add(Opt_RunItem.Normal.ManagementItem);
            }
            else
            {
                foreach (var item in Opt_RunItem.MultiRuns)
                {
                    if (!managementNames.Contains(item.ManagementItem)) { managementNames.Add(item.ManagementItem); }
                }
            }
            //create the list of soil names
            soilNames = new List<string>();
            if ((Opt_Mode == OptimizationMode.SingleRun))
            {
                soilNames.Add(Opt_RunItem.Normal.SoilItem);
            }
            else
            {
                foreach (var item in Opt_RunItem.MultiRuns)
                {
                    if (!soilNames.Contains(item.SoilItem)) { soilNames.Add(item.SoilItem); }
                }
            }
            //Debug
            //allSubModelNames = varietyNames.Concat(managementNames).Concat(soilNames).ToList();
            if (fitnessFunctions.optimizeNonVarietalParameter == true && fitnessFunctions.optimizeVarietalParameter == false)
            {
                allSubModelNames = varietiesNonVariety.Values.Concat(managementNames).Concat(soilNames).ToList();
            } 
            else allSubModelNames = varietyNames.Concat(managementNames).Concat(soilNames).ToList();
            //Debug

            foreach (string obs in Opt_Observations.Select(ii => ii.Name).ToList())
            {
                    resultPage[2].Add(obs + mode);
                    resultPage[2].Add(obs + " Absolute Max Error");
            }
            this.OptimizationParameters.ResultBook.Add(resultPage);

            lineNb = 0;
            roundNb = 0;
        }

        #endregion

        #region Init, Step, End

        public void Init()
        {
            algorithm.InitAlgo();
            
            
        }

        public bool Step(int generationNb)
        {

            Dictionary<string, Dictionary<string, double>> fitnessModeValue;//rmse, rme or rmsre
            Dictionary<string, Dictionary<string, double>> absMaxErr;
            double fitness = algorithm.StepAlgo(out fitnessModeValue, out absMaxErr);
            lineVar = 0;
            //print the result of the round
            if (generationNb == 1) { roundNb++; }
            foreach (string submodel in allSubModelNames)
            {
                if (this.fitnessFunctions.ParamValueDictio.ContainsKey(submodel))
                {
                    this.OptimizationParameters.ResultBook[0].NewLine();
                    lineNb++;
                    lineVar++;
                    //Debug
                    if ((fitnessFunctions.optimizeNonVarietalParameter == true && fitnessFunctions.optimizeVarietalParameter == false) && varietiesNonVariety.ContainsValue(submodel))
                    {
                        this.OptimizationParameters.ResultBook[0][lineNb + 2].Add(varietyNames[lineVar - 1] + " " + submodel);
                    }
                    //Debug
                    else if (varietiesNonVariety.ContainsKey(submodel))
                    {
                        this.OptimizationParameters.ResultBook[0][lineNb + 2].Add(submodel+" "+varietiesNonVariety[submodel]);
                    }
                    else
                    {
                        this.OptimizationParameters.ResultBook[0][lineNb + 2].Add(submodel);
                    }
                    this.OptimizationParameters.ResultBook[0][lineNb + 2].Add(roundNb).Add(generationNb).Add(Math.Round(fitness,8));


                    foreach (OptiParameter param in Opt_Parameters)
                    {   
                            if (this.fitnessFunctions.ParamValueDictio[submodel].ContainsKey(param.Name))
                            {
                                this.OptimizationParameters.ResultBook[0][lineNb + 2].Add(Math.Round(this.fitnessFunctions.ParamValueDictio[submodel][param.Name], 4));
                            }
                            else
                            {
                                if (varietiesNonVariety.ContainsKey(submodel) && this.fitnessFunctions.ParamValueDictio[varietiesNonVariety[submodel]].ContainsKey(param.Name))
                                {
                                    this.OptimizationParameters.ResultBook[0][lineNb + 2].Add(Math.Round(this.fitnessFunctions.ParamValueDictio[varietiesNonVariety[submodel]][param.Name], 4));
                                }
                                else
                                {
                                    this.OptimizationParameters.ResultBook[0][lineNb + 2].Add("X");
                                }
                            }
                            
                    }

                    if (!fitnessModeValue.ContainsKey(submodel))
                    {
                        this.OptimizationParameters.ResultBook[0][lineNb + 2].Add("no observation found for this variety");
                    }
                    else
                    {
                        foreach (KeyValuePair<string, double> kvp in fitnessModeValue[submodel])//can be optimized
                        {
                            this.OptimizationParameters.ResultBook[0][lineNb + 2].Add(kvp.Value);
                            this.OptimizationParameters.ResultBook[0][lineNb + 2].Add(absMaxErr[submodel][kvp.Key]);
                        }
                    }
                }
            }
           
            return (fitness > algorithm.StopFitness());
        }
        public void End() 
        {
            algorithm.EndAlgo();

        }

        private int roundNb;
        private int lineNb;
        private int lineVar;

        #endregion
    }

    public interface IAlgorithm
    {
        ///<summary>
        ///Return the name of the algorithm
        ///</summary>
        ///<returns></returns>
        string Name();
        ///<summary>
        ///Initialisation of the algorithm
        ///</summary>
        void InitAlgo();
        ///<summary>
        ///Do one iteration
        ///</summary>
        ///<returns>false if any stop criterion is satisfied, true else</returns>
        double StepAlgo(out Dictionary<string, Dictionary<string, double>> fitnessModeValue, out Dictionary<string, Dictionary<string, double>> absMaxErr);
        void EndAlgo();

        double StopFitness();
        int NbOfRounds();
        int NbOfGeneration();
    }

    public enum OptimizationMode
    {
        SingleRun,
        BatchRun,
    }
}