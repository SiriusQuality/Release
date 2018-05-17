using System;

namespace SiriusModel.Model.CropModel.Phytomers
{
    public class LeafLaminae : LeafLayerCompartment
    {

        ///<summary>Period since senescence of lamina, °CDay</summary>
        public double TTSinceSen { get; private set; }

        ///Constructor.
        ///\param [in] i the index of this LeafLaminae.
        public LeafLaminae(Universe universe) : base(universe)
        {
            TTSinceSen = 0.0;
        }

        ///Constructor.
        ///\param [in] toCopy instance to init from.
        public LeafLaminae(Universe universe, LeafLaminae toCopy) : base(universe, toCopy)
        {
            TTSinceSen = toCopy.TTSinceSen;
        }

        public override double FracBGR()
        {
            return FracLaminaeBGR;
        }

        public override double PotentialSpecificWeight
        {
            get { return SLWp; }
        }

        public void IncTTsinceSen(double DeltaTTShoot)
        {
            TTSinceSen += DeltaTTShoot;
        }

        /*public static int CompareLeafLaminaeByIndex(LeafLaminae a, LeafLaminae b)
        {
            return a.Index.CompareTo(b.Index);
        }*/
    }
}