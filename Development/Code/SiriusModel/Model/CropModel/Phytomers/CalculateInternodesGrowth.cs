using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiriusModel.Structure; 

namespace SiriusModel.Model.CropModel.Phytomers
{
    class CalculateInternodesGrowth:UniverseLink
    {
        private List<double> LeafLayerInternodeLengthGrowthList;
        public double EarPeduncleInternodeLengthGrowth {get ; private set;}

        public List<double> getLeafLayerInternodeLengthGrowthList()
        {
            return LeafLayerInternodeLengthGrowthList;
        }

        public CalculateInternodesGrowth(Universe universe): base(universe)
        {
            EarPeduncleInternodeLengthGrowth=0;
            LeafLayerInternodeLengthGrowthList = new List<double>();
        }
        public CalculateInternodesGrowth(Universe universe, CalculateInternodesGrowth toCopy): base(universe)
        {
            LeafLayerInternodeLengthGrowthList = new List<double>(toCopy.LeafLayerInternodeLengthGrowthList);
            EarPeduncleInternodeLengthGrowth = toCopy.EarPeduncleInternodeLengthGrowth;
        }

        public void estimate(List<LeafLayer> All_, EarPeduncle earPeduncle_,double[] cumulTT, double deltaTTShoot)
        {
            //update size of the output list
            for (int i = 0; i < All_.Count; i++)
            {
                //if a new leaf has appeared
                if (i >= LeafLayerInternodeLengthGrowthList.Count)
                {
                    LeafLayerInternodeLengthGrowthList.Add(0);
                }
            }


            foreach (var ll in All_)
            {
                if (ll.Index > 0)
                {
                    var previousll = All_[ll.Index - 1];
                    LeafLayerInternodeLengthGrowthList[ll.Index] = LeafLayerInternodeGrowth(cumulTT, deltaTTShoot,ll.GetInterNode().LengthPot,ll.GetInterNode().Length, ll.TTem, ll.TTgroLamina, ll.InterNodeTTgro);
                }
            }
            if (earPeduncle_ != null)
            {
                var previousll = All_[earPeduncle_.Index - 1];
                EarPeduncleInternodeLengthGrowth = EarPeduncleInternodeGrowth(cumulTT, deltaTTShoot, earPeduncle_.getInternodeLengthPot(), earPeduncle_.getInternodeLength(), earPeduncle_.InterNodeTTgro, previousll.TTem, previousll.TTgroLamina, previousll.InterNodeTTgro);
            }


        }

        public double LeafLayerInternodeGrowth(double[] cumulTT, double deltaTTShoot,double lengthPot, double length, double _TTem, double _TTgroLamina, double _TTgro)
        {
            double growth=0;
            if (lengthPot > 0)
            {
                if ((cumulTT[(int)Delta.Shoot] - _TTem) > _TTgroLamina)
                {
                    if ((cumulTT[(int)Delta.Shoot] - _TTem) - _TTgroLamina < _TTgro)
                    {
                        growth = Math.Min(lengthPot * deltaTTShoot / _TTgro, lengthPot - length);  // pm 24 May 2013, replaced air temperature by canopy temperature
                    }
                    
                }

            }
            return growth;
        }
        public double EarPeduncleInternodeGrowth(double[] cumulTT, double deltaTTShoot, double lengthPot, double length,double _TTgro, double previousllTTem, double previousllTTgroLamina, double previousllTTgro)
        {
            if ((cumulTT[(int)Delta.Shoot] - previousllTTem) > previousllTTgroLamina + previousllTTgro)
            {
                return Math.Min(lengthPot * deltaTTShoot / _TTgro, lengthPot - length);  // pm 24 May 2013, replaced air temperature by canopy temperature
            }
            else
            {
                return 0;
            }
        }
    }
}
