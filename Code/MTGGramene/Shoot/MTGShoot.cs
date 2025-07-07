using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csMTG
{
    public class MTGShoot:Gramene
    {
        public MTGShoot()
        {

        }

        public MTGShoot(MTGShoot toCopy)
        {

        }


        public Dictionary<string,double> CalcTillerPosition(double leafNOnMS, double dTT, double Phyll)
        {
            return Tillering.CalcTillerPosition(leafNOnMS, dTT, Phyll);
        }

    }
}
