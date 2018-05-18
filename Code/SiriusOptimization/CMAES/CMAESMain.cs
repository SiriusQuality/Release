using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiriusModel.InOut;
using SiriusOptimization.Base;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra.Factorization;
using MathNet.Numerics.Random;

namespace SiriusOptimization.CMAES
{
    class CMAESMain : IAlgorithm
    {
        public static OptimizationProblem OptProblem { get; set; }

        public CMAESMain(OptimizationProblem optimizationProblem)
        {
            CMAESMain.OptProblem = optimizationProblem;
        }

        #region CMA-ES parameters

        public int u { get { return OptProblem.OptimizationParameters.CMAES_u; } }

        public CMAESParameters parameters { get; set; }

        #endregion

        #region IAlgorithm parameters

        public string Name() { return "CMA-ES"; }
        public int NbOfRounds() { return OptProblem.OptimizationParameters.CMAESNbOfRound; }
        public int NbOfGeneration() { return OptProblem.OptimizationParameters.CMAESNbOfGeneration; }
        public double StopFitness() { return OptProblem.OptimizationParameters.CMAESStopFitness; } 

        public void EndAlgo()
        {
            // Put the result in fitnessFunction
            XtoParamValue(parameters.xmean.Column(0));
        }
        #endregion

        ///<summary>
        ///Transfert the value of vector X to fitness parameters in order to compute fitness and change it from a value between 0 and 1
        ///</summary>
        ///<param name="X">Parameters for fitness compute</param>
        private bool XtoParamValue(Vector<double> X)
        {
            int i = 0;

            foreach (string subModel in OptProblem.fitnessFunctions.ParamValueDictio.Keys.ToList())
            {
                foreach (string parameterName in OptProblem.fitnessFunctions.ParamValueDictio[subModel].Keys.ToList())
                {
                    //avoid going outside of the parameters boundaries which could cause the simulation to crash
                    if (X[i] < 0) { X[i] = 0; }
                    if (X[i] > 1) { X[i] = 1; }

                    OptiParameter completeParam = OptProblem.Opt_Parameters.Find(ii => ii.Name == parameterName);

                    OptProblem.fitnessFunctions.ParamValueDictio[subModel][parameterName] = completeParam.Min + X[i] * (completeParam.Max - completeParam.Min);
                    i++;
                }
            }
            return true;
        }
        public void InitAlgo()
        {
            parameters = new CMAESParameters(OptProblem.fitnessFunctions.paramValueDictioSize, u);
                        ///init to put in init algo    
            int count = 0;
            foreach (string subModel in OptProblem.fitnessFunctions.ParamValueDictio.Keys.ToList())
            {
                foreach (string parameterName in OptProblem.fitnessFunctions.ParamValueDictio[subModel].Keys.ToList())
                {
                    parameters.xmean[count, 0] = SystemRandomSource.Default.NextDouble();//normalize parameters , random values 
                    parameters.sigma = 0.2; //coordinate wise standard deviation (step size)
                    count++;
                }
            }
            // Strategy parameter setting: Selection  
            parameters.lambda = Math.Max(u,4 + (int)Math.Floor(3 * Math.Log(parameters.N)));  // population size, offspring number
            parameters.mu = parameters.lambda / 2;               // number of parents/points for recombination
            ;
            parameters.weights = Matrix<double>.Build.Dense((int)Math.Floor(parameters.mu), 1, 0); ;
            double sumWeight=0;
            double sumWeightSquare=0;
            for (int i = 0; i < Math.Floor(parameters.mu); i++)
            {
                parameters.weights[i, 0] = Math.Log(parameters.mu + 1 / 2) - Math.Log(i + 1); // muXone array for weighted recombination
                sumWeight += parameters.weights[i, 0]; //to do use the sum method
                sumWeightSquare += Math.Pow(parameters.weights[i, 0], 2);
            }

            parameters.mu = Math.Floor(parameters.mu);

            for (int i = 0; i < parameters.mu; i++)
            {
                parameters.weights[i, 0] = parameters.weights[i, 0] / sumWeight;     // normalize recombination weights array
            }

            parameters.mueff = Math.Pow(sumWeight, 2) / sumWeightSquare; // variance-effectiveness of sum w_i x_i

            // Strategy parameter setting: Adaptation
            parameters.cc = (4 + parameters.mueff / parameters.N) / (parameters.N + 4 + 2 * parameters.mueff / parameters.N); // time constant for cumulation for C
            parameters.cs = (parameters.mueff + 2) / (parameters.N + parameters.mueff + 5);  // t-const for cumulation for sigma control
            parameters.c1 = 2 / (Math.Pow(parameters.N + 1.3, 2) + parameters.mueff);    // learning rate for rank-one update of C
            parameters.cmu = Math.Min(1 - parameters.c1, 2 * (parameters.mueff - 2 + 1 / parameters.mueff) / (Math.Pow(parameters.N + 2, 2) + parameters.mueff));  // and for rank-mu update
            parameters.damps = 1 + 2 * Math.Max(0, Math.Sqrt((parameters.mueff - 1) / (parameters.N + 1)) - 1) + parameters.cs; // damping for sigma 
                                                                // usually close to 1
            // Initialize dynamic (internal) strategy parameters and constants


            parameters.pc = Matrix<double>.Build.Dense(parameters.N, 1); //initialized at 0 by default // evolution paths for C
            parameters.ps = Matrix<double>.Build.Dense(parameters.N, 1); //initialized at 0 by default // evolution paths for sigma

            parameters.B = Matrix<double>.Build.DenseIdentity(parameters.N); // B defines the coordinate system

            parameters.D = Matrix<double>.Build.DenseIdentity(parameters.N); // diagonal D defines the scaling

            Matrix<double> DSquare = parameters.D.PointwisePower(2);

            parameters.C = parameters.B * DSquare * parameters.B.Transpose();            // covariance matrix C

            Matrix<double> Dinv = parameters.D;// diag(D.^-1)

            parameters.invsqrtC = parameters.B * Dinv * parameters.B.Transpose();    // C^-1/2 

            parameters.chiN = Math.Sqrt(parameters.N) * (1 - 1 / (4 * parameters.N) + 1 / (21 * Math.Pow(parameters.N, 2)));  // expectation of ||N(0,I)|| == norm(randn(N,1))
            parameters.eigeneval = 0;
            parameters.counteval = 0; 

        }

        public double StepAlgo(out Dictionary<string, Dictionary<string, double>> fitnessModeValue, out Dictionary<string, Dictionary<string, double>> absMaxErr)
        {
   
             Matrix<double> arx = Matrix<double>.Build.Dense(parameters.N, parameters.lambda);
             Vector<double> arfitness = Vector<double>.Build.Dense(parameters.lambda);
            // Generate and evaluate lambda offspring
            for (int k=0;k<parameters.lambda;k++)
            {
                Matrix<double> random = Matrix<double>.Build.Random(parameters.N, 1, new Normal(0, 1));

                Matrix<double> arxcolumnk = parameters.xmean + parameters.sigma * parameters.B * (parameters.D.Diagonal().ToColumnMatrix().PointwiseMultiply(random)); // m + sig * Normal(0,C)

                arx.SetColumn(k,arxcolumnk.Column(0));

                // Calcul Fitness
                XtoParamValue(arxcolumnk.Column(0));
                arfitness[k] = OptProblem.fitnessFunctions.Compute(out fitnessModeValue, out absMaxErr,false);  // objective function call
                parameters.counteval =+ 1;
            }

            Vector<double> bestIndex = Vector<double>.Build.Dense((int)Math.Floor(parameters.mu));// should be int but only double matrix are supported
            for (int i = 0; i < parameters.mu; i++)
            {

                bestIndex[i] = arfitness.MaximumIndex();               
                arfitness[(int)bestIndex[i]] = 0;//to find the next maximumIndex next time
            }

            Matrix<double> xold = parameters.xmean;

            Matrix<double> bestarx = Matrix<double>.Build.Dense(parameters.N, (int)Math.Floor(parameters.mu));
            for (int l = 0; l < parameters.mu; l++)
            {
                bestarx.SetColumn(l,arx.Column((int)bestIndex[l]));
            }
            parameters.xmean = bestarx * parameters.weights;  // recombination, new mean value
    
            // Cumulation: Update evolution paths
            parameters.ps = (1 - parameters.cs) * parameters.ps + Math.Sqrt(parameters.cs * (2 - parameters.cs) * parameters.mueff) * parameters.invsqrtC * (parameters.xmean - xold) / parameters.sigma;

            bool hsigbool = (((parameters.ps.PointwisePower(2)).RowSums()[0]) / (1 - Math.Pow((1 - parameters.cs), (2 * parameters.counteval / parameters.lambda))) / parameters.N) < (2 + 4 / (parameters.N + 1));

            int hsig = hsigbool ? 1 : 0;

            parameters.pc = (1 - parameters.cc) * parameters.pc + hsig * Math.Sqrt(parameters.cc * (2 - parameters.cc) * parameters.mueff) * (parameters.xmean - xold) / parameters.sigma; 

            // Adapt covariance matrix C
            Matrix<double> artmp = Matrix<double>.Build.Dense(parameters.N, (int)Math.Floor(parameters.mu));
            Matrix<double> repetedxold = Matrix<double>.Build.Dense(parameters.N, (int)Math.Floor(parameters.mu));

            for (int i = 0; i < parameters.mu; i++)
            {
                repetedxold.SetColumn(i,xold.Column(0));
            }

            artmp = (1 / parameters.sigma) * (bestarx - repetedxold);  // mu difference vectors

            Matrix<double> diagWeight = Matrix<double>.Build.DiagonalOfDiagonalVector(parameters.weights.Column(0));

            parameters.C = (1 - parameters.c1 - parameters.cmu) * parameters.C + parameters.c1 * (parameters.pc * parameters.pc.Transpose() + (1 - hsig) * parameters.cc * (2 - parameters.cc) * parameters.C) + parameters.cmu * artmp * diagWeight * artmp.Transpose();

            // Adapt step size sigma
            parameters.sigma = parameters.sigma * Math.Exp((parameters.cs / parameters.damps) * (parameters.ps.L2Norm() / parameters.chiN - 1)); 
    
            // Update B and D from C
            if (parameters.counteval - parameters.eigeneval > parameters.lambda / (parameters.c1 + parameters.cmu) / parameters.N / 10) // to achieve O(N^2)
            {
                parameters.eigeneval = parameters.counteval;
                parameters.C = parameters.C.UpperTriangle() + (parameters.C.StrictlyUpperTriangle()).Transpose(); // enforce symmetry

                Evd<double> eigen = parameters.C.Evd();
                parameters.B = eigen.EigenVectors;
             // eigen decomposition, B==normalized eigenvectors        
                parameters.D = (eigen.D).PointwisePower(-0.5);        // D contains standard deviations now
                parameters.invsqrtC = parameters.B * parameters.D * parameters.B.Transpose();
            }

            //could add a limited number of run
            XtoParamValue(parameters.xmean.Column(0));
            return OptProblem.fitnessFunctions.Compute(out fitnessModeValue, out absMaxErr,true);
          
        }
    }
}
