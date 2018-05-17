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

namespace GeneticAlgorithms
{
    /// <summary>
    /// Summary description for ListGenome.
    /// </summary>
    public class ListGenome : Genome
    {
        ArrayList TheArray = new ArrayList();
        public static Random TheSeed = new Random((int)DateTime.Now.Ticks * 1000);
        int TheMin;
        int TheMax;

        ArrayList predictionArray = new ArrayList();
        //Declare ab arrry list to save the model predictions.

        public override int CompareTo(object a)
        {
            ListGenome Gene1 = this;
            ListGenome Gene2 = (ListGenome)a;
            return Math.Sign(Gene2.CurrentFitness - Gene1.CurrentFitness);
        }

        public override void SetCrossoverPoint(int crossoverPoint)
        {
            CrossoverPoint = crossoverPoint;
        }

        public ListGenome()
        {

        }

        public ListGenome(object min, object max, double[] kParameterMin, double[] kParameterMax, double[] kParameterStep) //Main function of initial population generation.
        {
            Length = kParameterMin.Length;
            //TheMin = (int)min;
            //TheMax = (int)max;

            int[] kParameterNumber = new int[kParameterStep.Length];

            for (int j = 0; j < kParameterStep.Length; j++)
            {
                kParameterNumber[j] = (int)((kParameterMax[j] - kParameterMin[j]) / kParameterStep[j] + 0.5);//Round the number.
            }

            for (int i = 0; i < Length; i++)
            {
                int tempRandom = (int)GenerateGeneValue(0, kParameterNumber[i]);
                double nextValue = Math.Round((double)(kParameterMin[i] + tempRandom * kParameterStep[i]), 8);
                TheArray.Add(nextValue);
            }
            //For the initial population generation,it is not necessary to consider the minimum step. The initial step is used only.
        }

        public ListGenome(object min, object max, double[] kParameterValue, double[] kParameterMin, double[] kParameterMax, double[] kParameterStep, double kParameterChangeProbability)
        {
            Length = kParameterMin.Length;
            //TheMin = (int)min;
            //TheMax = (int)max;

            int[] kParameterNumber = new int[kParameterStep.Length];

            for (int j = 0; j < kParameterStep.Length; j++)
            {
                kParameterNumber[j] = (int)((kParameterMax[j] - kParameterMin[j]) / kParameterStep[j] + 0.5);//Round the number.
            }

            for (int i = 0; i < Length; i++)
            {
                if ((double)ListGenome.TheSeed.Next(100) / (double)100 > kParameterChangeProbability)
                {
                    double tempCurrentParameterValue = (double)(kParameterMin[i] + (int)GenerateGeneValue(0, kParameterNumber[i]) * kParameterStep[i]);
                    double nextValue = Math.Round(tempCurrentParameterValue, 8);
                    TheArray.Add(nextValue);
                }
                else
                {
                    double tempCurrentParameterValue = kParameterValue[i];
                    double nextValue = Math.Round(tempCurrentParameterValue, 8);
                    TheArray.Add(nextValue);
                }
                //The probability of parameter to be fixed at its initial value is determined by the previous;y defined probability.
            }
        }

        public override void Initialize()
        {

        }

        public override bool CanDie(float fitness)
        {
            if (CurrentFitness < fitness)
            {
                return true;
            }

            return false;
        }

        public override bool CanReproduce(float fitness)
        {
             if (CurrentFitness >= fitness)
            {
                return true;
            }

            return false;
        }

        public override object GenerateGeneValue(object min, object max)
        {
            return TheSeed.Next((int)min, (int)max);
        }

        public override void Mutate(
            double[] kParameterMin,
            double[] kParameterMax,
            double[] kParameterStep,
            double[] kParameterStepLimit,
            int Generation,
            ArrayList optimalParameterValueArray,
            double rangeRatio)
        {
            int numberOfMutatedGene = TheSeed.Next((int)Length); 
            //Randomly determine how many genes to mutate.

            ArrayList geneIndexArray = new ArrayList();
            geneIndexArray.Clear();

            int[] mutationIndexArray = new int[numberOfMutatedGene];

            for (int i = 0; i < (int)Length; i++)
            {
                geneIndexArray.Add(i);
            }

            for (int j = 0; j < numberOfMutatedGene; j++)
            {
                int tempIndex = TheSeed.Next(geneIndexArray.Count);
                int tempValue = (int)geneIndexArray[tempIndex];
                mutationIndexArray[j] = tempValue;
                geneIndexArray.RemoveAt(tempIndex);
            }
            //Randomly generate unrepeatable gene index for mutation.

            for (int i = 0; i < numberOfMutatedGene; i++)
            {
                MutationIndex = mutationIndexArray[i];

                double tempValue = (double)optimalParameterValueArray[MutationIndex];

                double rangeValue = (kParameterMax[MutationIndex] - kParameterMin[MutationIndex]) * rangeRatio;
                //The range of the parameter is shrinking as the generation increases.

                double deltaUpper = (tempValue + rangeValue / 2) - kParameterMax[MutationIndex]; //The delta value between newly calculated upper limit value and the initial upper limit value.
                double deltaLower = kParameterMin[MutationIndex] - (tempValue - rangeValue / 2); //The delta value between newly calculated lower limit value and the initial lower limit value.
                
                //Change 3: Move the truncation to the other side. If the newly calculated  minimum is lower than the initial minimum, or newly calculated maximum is greater than the initial maximum.
                double tempParameterMax = 0;
                double tempParameterMin = 0;

                //if (deltaUpper >= 0 & deltaLower <=0)
                //{
                //    tempParameterMax = kParameterMax[MutationIndex];
                //    tempParameterMin = Math.Max(((tempValue - rangeValue / 2) - deltaUpper), kParameterMin[MutationIndex]);
                //}
                //else if (deltaUpper >= 0 & deltaLower >= 0)
                //{
                //    tempParameterMax = kParameterMax[MutationIndex];
                //    tempParameterMin = kParameterMin[MutationIndex];
                //}
                //else if(deltaUpper <= 0 & deltaLower <= 0)
                //{
                //    tempParameterMax = (tempValue + rangeValue / 2);
                //    tempParameterMin = (tempValue - rangeValue / 2);
                //}
                //else if (deltaUpper <= 0 & deltaLower >= 0)
                //{
                //    tempParameterMax = Math.Min(((tempValue + rangeValue / 2) + deltaLower), kParameterMax[MutationIndex]);
                //    tempParameterMin = kParameterMin[MutationIndex];
                //}

                tempParameterMax = Math.Min((tempValue + rangeValue / 2), kParameterMax[MutationIndex]);
                tempParameterMin = Math.Max((tempValue - rangeValue / 2), kParameterMin[MutationIndex]);
                //Change 2:Restrict the searching space between the initial minimum and maximum values. by He, 13/10/2010.

                double tempParameterStepShrinked = kParameterStep[MutationIndex] * Math.Min(1, rangeRatio);//Modified by He. In this way, the step size was varied according to the range ratio or the generation number. The ratio cannot be greater than the initial one.   
                double tempParameterStepLimit = kParameterStepLimit[MutationIndex];//Obtain the step limit or minimum step values read from the Excel file.
                double tempParameterStep = Math.Max(tempParameterStepShrinked, tempParameterStepLimit); //Change 1: The step value can not be lower than the minimum value.

                int tempParameterSectionNumber = (int)((tempParameterMax - tempParameterMin) / tempParameterStep); //Discretize the space of the parameter.
                int randomValue = (int)GenerateGeneValue(1, Math.Max(1, tempParameterSectionNumber));

                double tempCurrentParameterValue = tempParameterMin + randomValue * tempParameterStep;
                TheArray[MutationIndex] = Math.Round(tempCurrentParameterValue, 8);
            }
            //Mutate the selected genes.
        }

        public float RunSiriusModel(string calibrationRoundID, string dataDirectory, string workDirectory, string projectDirectory, string genericAlgorithmsDirectory,
            ArrayList observationArray, ArrayList stdArray, int[] kParameterSiteIndex, int amplificationFactor, double CV, ArrayList observationDateArray, ArrayList observationSeperator)
        {
            #region Input variables
            //Assign the random values generated above to each of the parameters.

            int observationNumber = observationArray.Count;
            //Get the number of observations.

            string outputFileName = "ModelOutput";
            string outputFileExtention = ".txt";
            string wholeOutputFileName = workDirectory + genericAlgorithmsDirectory + outputFileName + outputFileExtention;//Assign the path of the text file to save the model outputs concerned.
            File.WriteAllText(wholeOutputFileName, "");
            //Create a txt file to save model outputs concerned and clean up the output contents of last model run before a new run.
            #endregion

            #region Run the model
            AutomaticModelRun.ModelRun(
            TheArray,
            calibrationRoundID,
            dataDirectory,
            workDirectory,
            projectDirectory,
            wholeOutputFileName,
            kParameterSiteIndex,
            observationDateArray,
            observationSeperator
            );
            //Call the AutomaticModelRun statis class to run the model.
            #endregion

            #region Read model outputs
            List<double> modelOutputs = new List<double>();
            StreamReader SR = File.OpenText(wholeOutputFileName);
            string fileLineContent = null;

            modelOutputs.Clear();

            while ((fileLineContent = SR.ReadLine()) != null)
            {
                modelOutputs.Add(Convert.ToDouble(fileLineContent));
            }

            SR.Close();

            //Read the model outputs saved during the model run and save them in a list.
            #endregion

            #region Fitness calculation
            // fitness for the closest predictions to observations.

            predictionArray.Clear();

            for (int i = 0; i < observationNumber; i++)
            {
                predictionArray.Add(modelOutputs[i]);
            }
            //Prediction of anthesis dates and FLN of the hight nitrogen treatment at Clermont-Ferrand in 2007 and 2008 in order.
            //Declare an array to save the predictions concerned, e.g. anthesis date, final leaf number, LAI, etc.

            float[] fitnessValues = new float[observationNumber];
            float CurrentFitness = 1;

            for (int i = 0; i < observationNumber; i++)
            {
                double observationValue = Convert.ToDouble(observationArray[i]);
                double stdValue = Convert.ToDouble(stdArray[i]);
                double predictionValue = Convert.ToDouble(predictionArray[i]);
                //Console.WriteLine(amplificationFactor);

                if (observationValue != -999 & predictionValue != -999)
                {
                    //fitnessValues[i] = (float)(Math.Exp(-Math.Abs((float)Math.Pow((observationValue - predictionValue), 2) / (2 * (float)Math.Pow((stdValue* amplificationFactor), 2)))));
                    fitnessValues[i] = (float)(Math.Exp(-Math.Abs((float)Math.Pow((observationValue - predictionValue), 2) / (2 * (float)Math.Pow((observationValue * CV), 2))))) * amplificationFactor;
                    //The CV was modified to a variable for different sub-models since the fitness values are hard to calculate for many observation points.
                }
                else
                {
                    fitnessValues[i] = 1;
                }

                //Console.WriteLine("***");
                //Console.WriteLine(fitnessValues[i]);

                CurrentFitness = CurrentFitness * fitnessValues[i];
            }

            Console.WriteLine("The fitness value is {0}", Math.Round(CurrentFitness, 15));
            return CurrentFitness;
            #endregion
        }

        public override float CalculateFitness(string calibrationRoundID, string dataDirectory, string workDirectory, string projectDirectory, string genericAlgorithmsDirectory, 
            ArrayList observationArray, ArrayList stdArray, int[] kParameterSiteIndex, int amplificationFactor, double CV, ArrayList observationDateArray, ArrayList observationSeperator)
        {
            CurrentFitness = RunSiriusModel(calibrationRoundID, dataDirectory, workDirectory, projectDirectory, genericAlgorithmsDirectory, observationArray, stdArray, kParameterSiteIndex, amplificationFactor, CV, observationDateArray, observationSeperator);
            return CurrentFitness;
        }

        public override string OutputString(ArrayList observationArray)
        {
            int observationNumber = observationArray.Count;
            //Get the number of observations.

            string strResult = "[Par:";
            for (int i = 0; i < Length; i++)
            {
                if (i != (Length - 1))
                {
                    double tempValue = Math.Round((double)TheArray[i], 8);
                    strResult = strResult + (tempValue).ToString() + " ";
                }
                else
                {
                    strResult = strResult + (Math.Round((double)TheArray[i], 8)).ToString();
                }
            }
            strResult += "]";
            //Print out parameters.

            strResult += "\n[Pred:";
            for (int j = 0; j < observationNumber; j++)
            {
                if (j != (observationNumber - 1))
                {
                    strResult = strResult + ((double)predictionArray[j]).ToString() + " ";
                }
                else
                {
                    strResult = strResult + ((double)predictionArray[j]).ToString();
                }
            }
            strResult += "]";
            //Print out predictions.

            strResult += "\n[Obs:";
            for (int k = 0; k < observationNumber; k++)
            {
                if (k != (observationNumber - 1))
                {
                    double tempValue = Math.Round(Convert.ToDouble(observationArray[k]), 4);
                    strResult = strResult + tempValue.ToString() + " ";
                }
                else
                {
                    double tempValue = Math.Round(Convert.ToDouble(observationArray[k]), 4);
                    strResult = strResult + tempValue.ToString();
                }
            }
            strResult += "]";
            //Print out observations.

            strResult += "\nFitness-->" + Math.Round(CurrentFitness, 15).ToString() + "\n";
            //Print out fitness values.

            return strResult;
        }

        public override void CopyGeneInfo(Genome dest)
        {
            ListGenome theGene = (ListGenome)dest;
            theGene.Length = Length;
            theGene.TheMin = TheMin;
            theGene.TheMax = TheMax;
        }

        public override Genome Crossover(Genome g)
        {
            ListGenome aGene1 = new ListGenome();
            ListGenome aGene2 = new ListGenome();
            g.CopyGeneInfo(aGene1);
            g.CopyGeneInfo(aGene2);

            ListGenome CrossingGene = (ListGenome)g;
            for (int i = 0; i < CrossoverPoint; i++)
            {
                aGene1.TheArray.Add(CrossingGene.TheArray[i]);
                aGene2.TheArray.Add(TheArray[i]);
            }

            for (int j = CrossoverPoint; j < Length; j++)
            {
                aGene1.TheArray.Add(TheArray[j]);
                aGene2.TheArray.Add(CrossingGene.TheArray[j]);
            }

            // 50/50 chance of returning gene1 or gene2
            ListGenome aGene = null;
            if (TheSeed.Next(2) == 1)
            {
                aGene = aGene1;
            }
            else
            {
                aGene = aGene2;
            }

            return aGene;
        }

        public override ArrayList getParameterValues()
        {
            ArrayList parameterValueArray = new ArrayList();
            parameterValueArray.Clear();

            for (int i = 0; i < Length; i++)
            {
                double tempValue = (double)(TheArray[i]);
                parameterValueArray.Add(tempValue);
            }

            return parameterValueArray;
        }

        public override void setParameterValues(ArrayList parameterVaueList)
        {

            for (int i = 0; i < Length; i++)
            {
                double tempValue = (double)(parameterVaueList[i]);
                TheArray[i] = tempValue;
            }
        }

    }
}
