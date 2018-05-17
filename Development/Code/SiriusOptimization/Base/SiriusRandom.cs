using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace SiriusModel.Optimization.Base
{

    public static class SiriusRandomProvider
    {
        private static int seed = Environment.TickCount;
        private static ThreadLocal<Random> randomDel = new ThreadLocal<Random>(() =>
            new Random(Interlocked.Increment(ref seed))
            );
        public static Random GetRandom()
        {
            return randomDel.Value;
        }

    }
    /// <summary>
    /// Gaussian distribution 
    /// </summary>
    public class GaussianDistribution
    {
        private double _Mean;
        private double _SD;
        public double Next;
        public double NextGaussian(double mean, double sd)
        {
            var uni1 = SiriusRandomProvider.GetRandom();
            var uni2 = SiriusRandomProvider.GetRandom();
            Next = (mean + sd * (Math.Sqrt(-2 * Math.Log(uni1.NextDouble()))) * Math.Sin(2 * Math.PI * uni2.NextDouble()));
            return Next;
        }
        public double Mean
        {
            get { return _Mean; }
            set
            {
                _Mean = value;
            }
        }
        public double SD
        {
            get { return _SD; }
            set
            {
                _SD = value;
            }
        }

        public GaussianDistribution(double mean, double sd)
        {
            Mean = mean;
            SD = sd;

            var uni1 = SiriusRandomProvider.GetRandom();
            var uni2 = SiriusRandomProvider.GetRandom();
            Next = (mean + sd * (Math.Sqrt(-2 * Math.Log(uni1.NextDouble()))) * Math.Sin(2 * Math.PI * uni2.NextDouble()));


        }

    }
}
