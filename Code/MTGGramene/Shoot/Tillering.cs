using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csMTG
{
    public static class Tillering
    {

        static private Dictionary<string, double> tillersTTAge = new Dictionary<string, double>();
        static private Dictionary<string, double> tillersHS = new Dictionary<string, double>();
        static private List<string> KirbyList = new List<string>();

        static internal Dictionary<string, double> CalcTillerPosition(double HSMainAxis,double dTT, double Phyll)
        {
                                                                                                                 
            Dictionary<string, List<double>> TillersResultsToOrder = new Dictionary<string, List<double>>();                                                         


            int NLeafMainAxis = (int)Math.Max(1, Math.Ceiling(HSMainAxis));
            BuildTillerFrame(NLeafMainAxis);

            CalculateTillersTTAge(/*TillersResults.Keys.ToList()*/KirbyList, dTT);
            CalcHSTillers(/*TillersResults.Keys.ToList() */ KirbyList, Phyll);

            return tillersHS;
            //Key T_(tiller rank)_(position wrt main axis)_(position wrt parent axis)
            //Value: number of leaves created on parent axis or main axis in case of T000
        }
        

        static private /*Func<int>*/ void BuildTillerFrame(int NLeafMainAxis)
        {

            #region Debug
            //int age = 1;
            //int age2 = 1;
            //int age3 = 1;
            //int posMS = 1;
            //int rk2 = 1;
            //int rk = 1;
            //int posMS2 = 1;
            //int posMS3 = 1;

            //if (!KirbyList.ContainsKey("T|0|0|0")) KirbyList.Add("T|0|0|0", 0);

            //for (int ims = 4; ims <= NLeafMainAxis; ims++)
            //{

            //    string label = "";
            //    label = "T|" + rk + "|" + posMS + "|0";
            //    posMS++;
            //    if (!KirbyList.ContainsKey(label)) KirbyList.Add(label, age);

            //    if (KirbyList[label] >= 3)
            //    {

            //        rk2 = rk + 1;
            //        string label0 = "T|" + rk2 + "|" + posMS2 + "|" + age2;

            //        if (!KirbyList.ContainsKey(label0)) KirbyList.Add(label0, age2);

            //        if (KirbyList[label0] >= 2)
            //        {
            //            posMS3++;

            //            string label1 = "T|" + rk2 + "|" + posMS3 + "|" + age3;


            //            if (!KirbyList.ContainsKey(label1)) KirbyList.Add(label1, age3);

            //            age3++;
            //        }

            //        age2++;
            //    }

            //   age++;
            //}
            #endregion

            

            #region Write Tillers
            List<string> l = new List<string>();

                l.Add("T|0|0|0");

            if ( NLeafMainAxis >= 4)
            {
                l.Add("T|1|1|0");
            }
            if (NLeafMainAxis >= 5)
            {
                l.Add("T|1|2|0");
            }
            if (NLeafMainAxis >= 6)
            {
                l.Add("T|1|3|0"); l.Add("T|2|1|1");
            }
            if (NLeafMainAxis >= 7)
            {
                l.Add("T|1|4|0"); l.Add("T|2|1|2"); l.Add("T|2|2|1");
            }
            if (NLeafMainAxis >= 8)
            {
                l.Add("T|1|5|0"); l.Add("T|2|1|3"); l.Add("T|2|2|2"); l.Add("T|2|3|1"); l.Add("T|3|1|1");
            }
            if (NLeafMainAxis >= 9)
            {
                l.Add("T|1|6|0"); l.Add("T|2|1|4"); l.Add("T|3|1|2"); l.Add("T|3|2|1"); l.Add("T|2|2|3"); l.Add("T|2|3|2"); l.Add("T|2|4|1"); l.Add("T|4|1|1");
            }
            if (NLeafMainAxis >= 10)
            {
                l.Add("T|1|7|0"); l.Add("T|2|5|1"); l.Add("T|2|1|5"); l.Add("T|3|1|3"); l.Add("T|3|2|2"); l.Add("T|3|3|1"); l.Add("T|2|2|4"); l.Add("T|2|3|3"); l.Add("T|2|4|2"); l.Add("T6|1|1|"); l.Add("T|4|1|2"); l.Add("T|4|2|1"); l.Add("T|5|1|1");
            }
            if (NLeafMainAxis >= 11)
            {
                l.Add("T|2|1|6"); l.Add("T|3|1|4"); l.Add("T|4|1|3"); l.Add("T|5|1|2"); l.Add("T|6|1|2"); l.Add("T|7|1|1"); l.Add("T|8|1|1"); l.Add("T|9|1|1"); l.Add("T|2|2|5"); l.Add("T|3|2|3"); l.Add("T|4|2|2"); l.Add("T|5|2|1"); l.Add("T|6|2|1");
                l.Add("T|2|3|4"); l.Add("T|3|3|2"); l.Add("T|4|3|1"); l.Add("T|2|4|3"); l.Add("T|3|4|1"); l.Add("T|2|5|2"); l.Add("T|2|6|1"); l.Add("T|1|8|0");
            }
            if (NLeafMainAxis >= 12)
            {
                l.Add("T|2|1|7"); l.Add("T|3|1|5"); l.Add("T|4|1|4"); l.Add("T|5|1|3"); l.Add("T|6|1|3"); l.Add("T|7|1|2"); l.Add("T|8|1|2"); l.Add("T|9|1|2"); l.Add("T|10|1|1"); l.Add("T|11|1|1"); l.Add("T|12|1|1"); l.Add("T|13|1|1"); l.Add("T|14|1|1");
                l.Add("T|2|2|6"); l.Add("T|3|2|4"); l.Add("T|4|2|3"); l.Add("T|5|2|2"); l.Add("T|6|2|2"); l.Add("T|7|2|1"); l.Add("T|8|2|1"); l.Add("T|9|2|1"); l.Add("T|2|3|5"); l.Add("T|3|3|3"); l.Add("T|4|3|2"); l.Add("T|5|3|1"); l.Add("T|6|3|1");
                l.Add("T|2|4|4"); l.Add("T|3|4|2"); l.Add("T|4|4|1"); l.Add("T|2|5|3"); l.Add("T|3|5|1"); l.Add("T|2|6|2"); l.Add("T|2|7|1"); l.Add("T|1|9|0");
            }
            if (NLeafMainAxis > 12) throw new Exception("Tillers are not calculated for leaf " + NLeafMainAxis);
            #endregion

            KirbyList = new List<string>(l);
            
        }

        static private int Fibonacci(int N)
        {
            int a = 0;
            int b = 1;
            for (int i = 0; i < N; i++)
            {
                int temp = a;
                a = b;
                b = temp + b;
            }
            return a;
        }


        static private void CalculateTillersTTAge(List<string> KirbiList, double dtt)
        {
            foreach (string sk in KirbiList)
            {
                if (!tillersTTAge.ContainsKey(sk)) tillersTTAge.Add(sk, 0.0);
                else tillersTTAge[sk] += dtt;
            }
        }


        static private void CalcHSTillers(List<string> KirbyList,double Phyll)
        {
            foreach(string ik in KirbyList)
            {
                if (!tillersHS.ContainsKey(ik)) tillersHS.Add(ik,0.0);
                tillersHS[ik]=tillersTTAge[ik]/Phyll;
            }
        }
    }
}
