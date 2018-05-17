using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusOptimization.CMAES
{
    class CMAESParameters
    {
        public int N { get; set; } // Order of the sytem
        public int lambda { get; set; }
        public double mu { get; set; }

        // Variables
        public Matrix<double> C { get; set; }
        public Matrix<double> B { get; set; }
        public Matrix<double> D { get; set; }
        public double sigma { get; set; }
        public Matrix<double> X { get; set; }
        public Matrix<double> xmean { get; set; }
        public Matrix<double> weights { get; set; }

        public Matrix<double> pc { get; set; }
        public Matrix<double> ps { get; set; }
        public Matrix<double> invsqrtC { get; set; }
        public double cs;
        public double cc;
        public double c1;
        public double cmu;
        public double mueff;
        public double damps;
        public double chiN;
        public double eigeneval;
        public double counteval;

        // Constructor

        public CMAESParameters(int N, int u)
        {
            this.N = N;
            this.lambda = Convert.ToInt32(Math.Max(u, 4 + Math.Truncate(3 * Math.Log(N))));
            this.mu = lambda / 2;
            this.C = Matrix<double>.Build.DenseIdentity(N);
            this.B = Matrix<double>.Build.Dense(N, N);
            this.D = Matrix<double>.Build.Dense(N, N);
            this.sigma = 0;
            this.X = Matrix<double>.Build.Dense(this.N, this.lambda);
            this.xmean = Matrix<double>.Build.Dense(N, 1, 0);// objective variables initial point 
        }
    }
}
