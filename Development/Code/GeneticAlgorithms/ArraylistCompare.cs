using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithms
{
    ///<summary>
    ///Sort the arraylist contents.
    /// </summary>
    public class myCompare : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {

            double X = ((Genome)x).CurrentFitness;
            double Y = ((Genome)y).CurrentFitness;

            //Console.WriteLine("**************************");
            //Console.WriteLine(X);
            //Console.WriteLine(Y);
            //Console.WriteLine("**************************");

            if (double.IsNaN(X))
            {
                X = 0;
            }

            if (double.IsNaN(Y))
            {
                Y = 0;
            }

            //Console.WriteLine("-----------------------------");
            //Console.WriteLine(X);
            //Console.WriteLine(Y);
            //Console.WriteLine("-----------------------------");

            int Sign = Math.Sign(Y - X);
            return Sign;
            //Sort from maximum to minimum.
        }
    }
}
