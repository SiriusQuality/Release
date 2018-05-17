using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.IO;
using SiriusModel.InOut;
using SiriusModel.InOut.OutputWriter;
using SiriusModel.Model.CropModel;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace GeneticAlgorithms
{
    /// <summary>
    /// Summary description for Population.
    /// </summary>
    public class Population
    {
        #region varialbes

        public string cultivarID; //The variable to tell the model which round of calibration is conducting.
        public string calibrationRoundID; //The variable to tell the model which round of calibration is conducting.

        public int kCrossover;
        public int kTotalNumberofGeneration;
        public int kInitialPopulation;
        public int kPopulationLimit;
        public int kMin;
        public int kMax;
        public string[] kParameterNames;
        public double[] kParameterMin;
        public double[] kParameterMax;
        public double[] kParameterStep;
        public double[] kParameterStepLimit; // Change 1: Add the lower limit or minimum value of parameter step.
        public int[] kParameterSiteIndex;
        //Variables that control the generation of random parameter sets (or genomes).

        public int kLocalPopulation;
        public double kLocalParameterRatio;
        public int kLocalParameterRatioPartition = 5;
        //Variables that control the local exploition.

        public int amplificationFactor;
        //The factor that amplify the fitness values. If there are many observations involved, the combined fitness value will be very small and can not be significant for evaluation.

        public double CV;
        //The coefficient of variation used in fitness calculation.

        const float kMutationFrequency = 0.90f;
        const float kDeathFitness = 0.0000f;
        const float kReproductionFitness = 0.0000f;
        //Variables that control the properties of revolution process.

        const double kParameterChangeProbability = 0.5;
        //The probability for a parameter to change in the local search.

        public string dataDirectory;
        public string workDirectory;
        public string projectDirectory;
        public string genericAlgorithmsDirectory;
        public string wholeOptimalFileName;
        //The file path for the optimal parameter set.

        public ArrayList observationDateArray;
        public ArrayList observationArray;
        public ArrayList stdArray;
        public ArrayList observationSeperator;
        //The arraylist to save the number of sampling, observation dates, and observation values.

        ArrayList Genomes = new ArrayList();
        ArrayList GenomeReproducers = new ArrayList();
        ArrayList GenomeResults = new ArrayList();
        ArrayList GenomeFamily = new ArrayList();
        ArrayList optimalParameterValueArray = new ArrayList();

        public int CurrentPopulation;

        int Generation = 1;
        bool Best2 = true;

        double maxFitness = 0;
        int maxFitnessIndex = 0;//Two variables to save the optimal parameter set in each generation.

        public double fitnessShresholdForLoacalSearch;//The threshold to start local search.
        //double fitnessShresholdForGenomeAdding = 0;//The threshold to add the genome that has the maximum fitness value.
        int populationIncrementRatioLimit = 5;//The number of population increment limit, i.e. only a maximum of 5 times more genomes can be generated if the fitness is stuck for many generations.
        Genome tempGenome0 = null;//The temporary genome to save the genome that has the maximum fitness value.
        ArrayList parameterValueArray0 = new ArrayList();//The temporary array to save the parameter values of the genome the has the maximum fitness value.

        int numberOfRangeSteps = 50; //The decrease ratio of range  with respect to generation number is 1/numberOfRangeSteps.
        public int stuckGenerationNumber; //Make the stuck generation nubmer as 0 after one population or one cultivar has been calibrated.
        int initialStuckGenerationNumberLimit = 5;
        int stuckGenerationNumberLimitIncrement = 5;//The variables to save the number of stuck generations and the corresponding fitness values.
        int stuckGenerationNumberLimit;
        double rangeShrinkingFactor = 1.0;
        double stuckFitnessValue = 0;
        double initialRangeRatioIncrement = 0.05;//For the first time in stuck (i.e. the fitness value has stopped for 5 generation let's say), the parameter range will be increased by 0.05 or 5%. 
        double deltaRangeRatioIncrement = 0.05;//But next time in stuck (i.e. the fitness value has stopped for 10 generation let's say), the increment of parameter range will be increased by 0.05 or 5%, or the parameter range was increased by 10% and so on.
        double rangeRatioIncrement;
        double rangeRatioLimit = 0.10;

        myCompare myCompareMethod = new myCompare();
        //The method the sort the genomes.
        #endregion

        public void PopulationGeneration()
        {
            for (int i = 0; i < kInitialPopulation; i++)
            {
                ListGenome aGenome = new ListGenome(kMin, kMax, kParameterMin, kParameterMax, kParameterStep);

                int kLength = kParameterMin.Length;
                int crossoverRandomSeed = ListGenome.TheSeed.Next(kLength);
                kCrossover = crossoverRandomSeed;
                //kCrossover or crossover point was a random number among the length of the genome.

                aGenome.SetCrossoverPoint(kCrossover);
                aGenome.CalculateFitness(calibrationRoundID, dataDirectory, workDirectory, projectDirectory, genericAlgorithmsDirectory, observationArray, stdArray, kParameterSiteIndex, amplificationFactor, CV, observationDateArray, observationSeperator);
                aGenome.OutputString(observationArray);
                Genomes.Add(aGenome);
            }

            Genomes.Sort(myCompareMethod);

            int kPopulationLimitOfInitialPopulation = 20 * kPopulationLimit;//The population limit of first generation is 10 times of the limits of other generations.

            // kill all the genes above the population limit
            if (Genomes.Count > kPopulationLimitOfInitialPopulation)
            {
                Genomes.RemoveRange(kPopulationLimitOfInitialPopulation, Genomes.Count - kPopulationLimitOfInitialPopulation);
            }

            CurrentPopulation = Genomes.Count;
        }

        public void LocalPopulationGeneration( )
        {
            int parameterNumber = optimalParameterValueArray.Count;

            double bestFitness = 0;
            int numberOfGenomes = 0;

            double[] kLocalParameterValue = new double[parameterNumber];
            double[] kLocalParameterMin = new double[parameterNumber];
            double[] kLocalParameterMax = new double[parameterNumber];
            double[] kLocalParameterStep = new double[parameterNumber];
            double[] kLocalParameterStepLimit = new double[parameterNumber];

            for (int i = 0; i < optimalParameterValueArray.Count; i++)
            {
                double tempLocalParameterValue = Convert.ToDouble(optimalParameterValueArray[i]);
                kLocalParameterValue[i] = tempLocalParameterValue;

                double tempInitialParameterMin = kParameterMin[i];
                double tempInitialParameterMax = kParameterMax[i];

                kLocalParameterMin[i] = Math.Max((tempLocalParameterValue * (1 - kLocalParameterRatio)), tempInitialParameterMin);
                kLocalParameterMax[i] = Math.Min((tempLocalParameterValue * (1 + kLocalParameterRatio)), tempInitialParameterMax); //Restric the local seaching range of the parameters with climbing hill methods. Modified by He, 14/10/2010.
                kLocalParameterStep[i] = kParameterStep[i] * (kLocalParameterRatio / kLocalParameterRatioPartition); //Define the local searching spaces.
            }

            for (int j = 0; j < kLocalPopulation; j++)
            {
                ListGenome aGenome = new ListGenome(kMin, kMax, kLocalParameterValue, kLocalParameterMin, kLocalParameterMax, kLocalParameterStep, kParameterChangeProbability);
                aGenome.CalculateFitness(calibrationRoundID, dataDirectory, workDirectory, projectDirectory, genericAlgorithmsDirectory, observationArray, stdArray, kParameterSiteIndex, amplificationFactor, CV, observationDateArray, observationSeperator);
                aGenome.OutputString(observationArray);

                if(j==0)
                {
                  Genomes.Add(aGenome);
                }
                //Add the first genome.

                numberOfGenomes=Genomes.Count;

                double tempFitness = aGenome.CurrentFitness;

                if (tempFitness > bestFitness)
                {
                    Genomes.RemoveAt((numberOfGenomes - 1));//Remove the last element of Genomes.
                    Genomes.Add(aGenome);
                    bestFitness = tempFitness;
                }

            }

        }

        private void Mutate(
            Genome aGene,
            double[] kParameterMin,
            double[] kParameterMax,
            double[] kParameterStep,
            double[] kParameterStepLimit,
            int Generation,
            ArrayList optimalParameterValueArray,
            double rangeRatio)
        {
            if (ListGenome.TheSeed.Next(100) < (int)(kMutationFrequency * 100.0))
            {
                aGene.Mutate(
                    kParameterMin,
                    kParameterMax,
                    kParameterStep,
                    kParameterStepLimit,
                    Generation,
                    optimalParameterValueArray,
                    rangeRatio);
            }
        }

        private void setParmeterValues(Genome aGene, ArrayList parameterVaueList)
        {
            aGene.setParameterValues(parameterVaueList);
        }

        private ArrayList getParameterValues(Genome aGene)
        {
            ArrayList initialParameterValues = new ArrayList();
            initialParameterValues.Clear();
            initialParameterValues = aGene.getParameterValues();

            return initialParameterValues;
        }

        public void NextGeneration(double rangeRatio, out double newRangeRatio)
        {

            #region Count the number of generations where the result is stuck at some local optimum.
            if (Generation == 1)
            {
                stuckFitnessValue = maxFitness;
                stuckGenerationNumberLimit = initialStuckGenerationNumberLimit;
                rangeRatioIncrement = initialRangeRatioIncrement;
            }
            else
            {
                if (Math.Round(stuckFitnessValue, 6) != Math.Round(maxFitness, 6))//Reduced the precision of fiteness value comparison. He, 16/07/2010.
                {
                    stuckFitnessValue = maxFitness;
                    stuckGenerationNumber = 0;
                    stuckGenerationNumberLimit = initialStuckGenerationNumberLimit;
                    rangeRatioIncrement = initialRangeRatioIncrement;
                }
                else
                {
                    stuckGenerationNumber++;
                }

            }
            #endregion

            #region Calculate the current range ratio of the parameters.
            double deltaRangeRatio = Convert.ToDouble((double)(1) / (double)numberOfRangeSteps);
            deltaRangeRatio = deltaRangeRatio * rangeShrinkingFactor;
            rangeRatio = Math.Max((rangeRatio - deltaRangeRatio), rangeRatioLimit);//Don't let the range less than 10% of the initial range.

            if (stuckGenerationNumber == stuckGenerationNumberLimit)
            {
                rangeRatio = rangeRatio + rangeRatioIncrement;
                stuckGenerationNumber = 0;
                stuckGenerationNumberLimit = stuckGenerationNumberLimit + stuckGenerationNumberLimitIncrement;
                rangeRatioIncrement = rangeRatioIncrement + deltaRangeRatioIncrement;
            }

            newRangeRatio = rangeRatio;

            Console.WriteLine("------------------------");
            Console.WriteLine(stuckGenerationNumber);
            Console.WriteLine(stuckGenerationNumberLimit);
            Console.WriteLine(rangeRatio);
            Console.WriteLine("------------------------");
            #endregion

            // increment the generation;
            Generation++;

            // check who can die
            for (int i = 0; i < Genomes.Count; i++)
            {
                if (((Genome)Genomes[i]).CanDie(kDeathFitness))
                {
                    Genomes.RemoveAt(i);
                    i--;
                }
            }

            // determine who can reproduce
            GenomeReproducers.Clear();
            GenomeResults.Clear();

            for (int i = 0; i < Genomes.Count; i++)
            {
                if (((Genome)Genomes[i]).CanReproduce(kReproductionFitness))
                {
                    GenomeReproducers.Add(Genomes[i]);
                }
            }

            ArrayList tempGenomes = (ArrayList)GenomeReproducers.Clone();
            tempGenomes.Sort(myCompareMethod);
            tempGenome0 = (Genome)tempGenomes[0];
            parameterValueArray0.Clear();
            parameterValueArray0 = getParameterValues(tempGenome0); //Save the genome that has the highest fitness value in this generation,which is the first one after sorting.

            int crossoverIncrementRatio = Math.Min((stuckGenerationNumberLimit / initialStuckGenerationNumberLimit), (populationIncrementRatioLimit + 1));

            for (int i = 0; i < crossoverIncrementRatio; i++)
            {
                DoCrossover(GenomeReproducers);
            }

            Genomes = (ArrayList)GenomeResults.Clone();

            Console.WriteLine("================================================================");
            Console.WriteLine("The number of genomes after crossover is " + Genomes.Count);
            Console.WriteLine("================================================================");

            // mutate a part of genes, which is determined by the frequency, in the new population, except the last two genes which are the best two from laste generation.
            for (int i = 0; i < (Genomes.Count); i++)
            {
                Mutate(
                    (Genome)Genomes[i],
                    kParameterMin,
                    kParameterMax,
                    kParameterStep,
                    kParameterStepLimit,
                    Generation,
                    optimalParameterValueArray,
                    rangeRatio);
            }

            //Generate local genomes, i.e. local exploitation of best solution.
            if (maxFitness > fitnessShresholdForLoacalSearch)  //Only start local search when the maxFitnees is greater than the threshold value. 
            {
                LocalPopulationGeneration();
            }

            Genomes.Add(tempGenome0);
            setParmeterValues((Genome)Genomes[Genomes.Count - 1], parameterValueArray0);
            //Add the best genome from previous generations...

            //calculate fitness of all the genes
            for (int i = 0; i < (Genomes.Count); i++)
            {
                double CurrentFitness = ((Genome)Genomes[i]).CalculateFitness(calibrationRoundID, dataDirectory, workDirectory, projectDirectory, genericAlgorithmsDirectory, observationArray, stdArray, kParameterSiteIndex, amplificationFactor, CV, observationDateArray, observationSeperator);
            }

            Genomes.Sort(myCompareMethod);

            // kill all the genes above the population limit
            if (Genomes.Count > kPopulationLimit)
                Genomes.RemoveRange(kPopulationLimit, Genomes.Count - kPopulationLimit);

            CurrentPopulation = Genomes.Count;
            //Console.WriteLine(CurrentPopulation);

        }

        public void CalculateFitnessForAll(ArrayList genes)
        {
            foreach (ListGenome lg in genes)
            {
                double CurrentFitness = lg.CalculateFitness(calibrationRoundID, dataDirectory, workDirectory, projectDirectory, genericAlgorithmsDirectory, observationArray, stdArray, kParameterSiteIndex, amplificationFactor, CV, observationDateArray, observationSeperator);
                //Console.WriteLine(CurrentFitness);
            }
        }

        public void DoCrossover(ArrayList genes)
        {
            ArrayList GeneMoms = new ArrayList();
            GeneMoms.Clear();
            ArrayList GeneDads = new ArrayList();
            GeneDads.Clear();

            //Console.WriteLine(genes.Count);

            for (int i = 0; i < genes.Count; i++)
            {
                // randomly pick the moms and dad's
                if (ListGenome.TheSeed.Next(100) % 2 > 0)
                {
                    GeneMoms.Add(genes[i]);
                }
                else
                {
                    GeneDads.Add(genes[i]);
                }
            }

            //  now make them equal
            if (GeneMoms.Count > GeneDads.Count)
            {
                while (GeneMoms.Count > GeneDads.Count)
                {
                    GeneDads.Add(GeneMoms[GeneMoms.Count - 1]);
                    GeneMoms.RemoveAt(GeneMoms.Count - 1);
                }

                if (GeneDads.Count > GeneMoms.Count)
                {
                    GeneDads.RemoveAt(GeneDads.Count - 1); // make sure they are equal
                }

            }
            else
            {
                while (GeneDads.Count > GeneMoms.Count)
                {
                    GeneMoms.Add(GeneDads[GeneDads.Count - 1]);
                    GeneDads.RemoveAt(GeneDads.Count - 1);
                }

                if (GeneMoms.Count > GeneDads.Count)
                {
                    GeneMoms.RemoveAt(GeneMoms.Count - 1); // make sure they are equal
                }
            }

            // now cross them over and add them according to fitness
            for (int i = 0; i < GeneDads.Count; i++)
            {
                // pick best 2 from parent and children
                ListGenome babyGene1 = (ListGenome)((ListGenome)GeneDads[i]).Crossover((ListGenome)GeneMoms[i]);
                ListGenome babyGene2 = (ListGenome)((ListGenome)GeneMoms[i]).Crossover((ListGenome)GeneDads[i]);

                GenomeFamily.Clear();
                GenomeFamily.Add(GeneDads[i]);
                GenomeFamily.Add(GeneMoms[i]);
                GenomeFamily.Add(babyGene1);
                GenomeFamily.Add(babyGene2);
                CalculateFitnessForAll(GenomeFamily);
                GenomeFamily.Sort(myCompareMethod);

                if (Best2 == true)
                {
                    // if Best2 is true, add top fitness genes
                    GenomeResults.Add(GenomeFamily[0]);
                    GenomeResults.Add(GenomeFamily[1]);

                }
                else
                {
                    GenomeResults.Add(babyGene1);
                    GenomeResults.Add(babyGene2);
                }
            }

        }

        public void WriteNextGeneration(out ArrayList optimalParameterSet)
        {

            for (int i = 0; i < CurrentPopulation; i++)
            {

                if (((Genome)Genomes[i]).CurrentFitness > maxFitness)
                {
                    maxFitness = ((Genome)Genomes[i]).CurrentFitness;
                    maxFitnessIndex = i;
                }

            }

            optimalParameterValueArray.Clear();
            optimalParameterValueArray = getParameterValues((Genome)Genomes[maxFitnessIndex]);
            //Save the parameter values of the optimal genome in each generation.

            optimalParameterSet = (ArrayList)optimalParameterValueArray.Clone();

            StreamWriter SW = new StreamWriter(wholeOptimalFileName, true);
            SW.WriteLine(((Genome)Genomes[maxFitnessIndex]).OutputString(observationArray));
            SW.Close();

            Console.WriteLine("");
            Console.WriteLine("The best parameter set is NO. " + (maxFitnessIndex + 1));
            Console.WriteLine(((Genome)Genomes[maxFitnessIndex]).OutputString(observationArray));
            Console.WriteLine("The generation {0} of Cultivar {1} and Round {2} is finished, continue...\n", Generation, cultivarID, calibrationRoundID);
            //Console.ReadLine();

            if (kTotalNumberofGeneration == Generation)
            {
                optimalParameterValueArray.Clear();
                Genomes.Clear();
                Generation = 1; //Clear the Genomes when one submodel is finished.
            }
            
        }
    }
}
