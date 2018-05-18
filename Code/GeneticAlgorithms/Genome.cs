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
    /// Summary description for Genome.
    /// </summary>
    public abstract class Genome : IComparable
    {
        public long Length;
        public int CrossoverPoint;
        public int MutationIndex;
        public float CurrentFitness = 0.0f;

        abstract public void Initialize();
        abstract public void Mutate(
            double[] kParameterMin,
            double[] kParameterMax,
            double[] kParameterStep,
            double[] kParameterStepLimit,
            int Generation,
            ArrayList optimalParameterValueArray,
            double rangeRatio);
        abstract public Genome Crossover(Genome g);
        abstract public ArrayList getParameterValues();
        abstract public void setParameterValues(ArrayList parameterValueList);
        abstract public object GenerateGeneValue(object min, object max);
        abstract public void SetCrossoverPoint(int crossoverPoint);
        abstract public float CalculateFitness(string calibrationRoundID, string dataDirectory, string workDirectory, string projectDirectory, string genericAlgorithmsDirectory,
            ArrayList observationArray, ArrayList stdArray, int[] kParameterSiteIndex, int amplificationFactor, double CV, ArrayList observationDateArray, ArrayList observationSeperator);
        abstract public bool CanReproduce(float fitness);
        abstract public bool CanDie(float fitness);
        abstract public void CopyGeneInfo(Genome g);
        abstract public int CompareTo(object a);
        abstract public string OutputString(ArrayList observationArray);
    }
}
