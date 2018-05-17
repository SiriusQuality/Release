using System;

namespace SiriusModel.Model.CropModel.Phytomers
{
    public class ExposedSheath : LeafLayerCompartment
    {

        ///<summary>Period since senescence of exposedSheath, °CDay</summary>
        public double TTSinceSen { get; private set; }

        ///Constructor.
        public ExposedSheath(Universe universe)
            : base(universe)
        {
            TTSinceSen = 0.0;
        }

        ///Constructor.
        public ExposedSheath(Universe universe, ExposedSheath toCopy)
            : base(universe, toCopy)
        {

            TTSinceSen = toCopy.TTSinceSen;
        }

        public override double FracBGR()
        {
            return FracSheathBGR;
        }

        public override double PotentialSpecificWeight
        {
            get { return SSWp; }
        }

        public void IncTTsinceSen(double DeltaTTShoot)
        {
            TTSinceSen += DeltaTTShoot;
        }

        /*public static int CompareExposedSheathByIndex(ExposedSheath a, ExposedSheath b)
        {
            return a.Index.CompareTo(b.Index);
        }*/       
    }
}