using System;

namespace SiriusModel.Model.SoilModel
{
    ///<summary>5 cm thick soil layer</summary>
    public class Layer
    {
        #region constants

        ///<summary>Define the thickness of each layers (5 cm)</summary>
        public const double Thickness = 0.05;

        #endregion

        #region fields

        ///<summary>Soil layer clay content</summary>
        public readonly double Clay;

        ///<Behnam>
        ///<summary>Kql</summary>
        public double Kql;
        ///</Behnam>

        ///<summary>Moisture at saturation</summary>
        public readonly double SSAT;

        ///<summary>Drained upper limit</summary>
        private readonly double sdul;

        ///<summary>Lower limit of extraction</summary>
        private readonly double sll;

        ///<summary>Maximum available water in the Layer</summary>
        public readonly double MaxAvWater;

        ///<summary>Maximum excess water in the Layer</summary>
        public readonly double MaxExWater;

        ///<summary>Unavailable water in the Layer</summary>
        public readonly double UnavWater;

        ///<summary>Water content at field capacity in the Layer</summary>
        private double fcWater;

        ///<summary>Current available water in the Layer</summary>
        private double avWater;

        ///<summary>Current excess water in the Layer</summary>
        private double exWater;

        ///<summary>Current inorganic N in the available bucket</summary>
        private double avN;

        ///<summary>Current inorganic N in the available bucket</summary>
        private double unavN;

        ///<summary>Current inorganic N in the available bucket</summary>
        private double exN;

        #endregion

        #region Constructors

        ///<summary>Initial constructor. This calculate automatically the members MaxAvWater,UnavWater, MaxExWater and initialise AvWater, ExWater, AvN, UnavN, ExN to 0.</summary>
        ///<param name="inSSAT">SSAT value of this new Layer</param>
        ///<param name="inSDUL">SDUL value of this new Layer</param>
        ///<param name="inSLL">SLL value of this new Layer</param>
        public Layer(double inClay, double inKql, double inSSAT, double inSDUL, double inSLL)
        {
            // 10 = 1000 mm  / 100%  // ps * 1000 to convert mm to gH2O / m^2
            Clay = inClay;
            Kql = inKql;
            SSAT = inSSAT;
            sdul = inSDUL;
            sll = inSLL;
            fcWater = 10000.0 * (sdul - 0) * Thickness;
            MaxAvWater = 10000.0 * (sdul - sll) * Thickness;
            AvWater = 0;
            UnavWater = 10000.0 * sll * Thickness;
            MaxExWater = 10000.0 * (SSAT - sdul) * Thickness;
            ExWater = 0;
            AvN = 0;
            UnavN = 0;
            ExN = 0;
            if (SSAT <= 0) throw new Exception("SSAT must be > 0");
            if (sdul <= 0) throw new Exception("SDUL must be > 0");
            if (sll <= 0) throw new Exception("SLL must be > 0");

            if (fcWater <= 0) throw new Exception("FcWater must be > 0");
            if (MaxAvWater <= 0) throw new Exception("MaxAvWater must be > 0");
            if (UnavWater <= 0) throw new Exception("UnavWater must be > 0");
            if (MaxExWater <= 0) throw new Exception("MaxExWater must be > 0");
        }

        ///<summary>Copy constructor</summary>
        ///<param name="toCopy">Layer to copy from</param>
        public Layer(Layer toCopy)
        {
            Clay = toCopy.Clay;
            Kql = toCopy.Kql;
            SSAT = toCopy.SSAT;
            sdul = toCopy.sdul;
            sll = toCopy.sll;
            fcWater = toCopy.FcWater;
            MaxAvWater = toCopy.MaxAvWater;
            AvWater = toCopy.AvWater;
            UnavWater = toCopy.UnavWater;
            MaxExWater = toCopy.MaxExWater;
            ExWater = toCopy.ExWater;
            AvN = toCopy.AvN;
            UnavN = toCopy.UnavN;
            ExN = toCopy.ExN;
        }

        #endregion

        #region Properties

        #region Water

        ///<Behnam>
        ///<summary>Water content at field capacity in the Layer</summary>
        public double FcWater
        {
            get { return fcWater; }
            set
            {
                fcWater = value;
                UniverseLink.RoundZero(ref fcWater);
                Check.IsNumber(fcWater);
                Check.IsPositiveOrZero(fcWater);
            }
        }
        ///</Behnam>

        ///<summary>Current available water in the Layer</summary>
        public double AvWater
        {
            get { return avWater; }
            set
            {
                avWater = value;
                UniverseLink.RoundZero(ref avWater);
                Check.IsNumber(avWater);
                Check.IsPositiveOrZero(avWater);
            }
        }

        ///<summary>Current excess water in the Layer</summary>
        public double ExWater
        {
            get { return exWater; }
            set
            {
                exWater = value;
                UniverseLink.RoundZero(ref exWater);
                Check.IsNumber(exWater);
                Check.IsPositiveOrZero(exWater);
            }
        }

        #endregion

        #region Nitrogen

        ///<summary>Current inorganic N in the available bucket</summary>
        public double AvN
        {
            get { return avN; }
            set
            {
                avN = value;
                UniverseLink.RoundZero(ref avN);
                Check.IsNumber(avN);
                Check.IsPositiveOrZero(avN);
            }
        }

        ///<summary>Current inorganic N in the available bucket</summary>
        public double UnavN
        {
            get { return unavN; }
            set
            {
                unavN = value;
                UniverseLink.RoundZero(ref unavN);
                Check.IsNumber(unavN);
                Check.IsPositiveOrZero(unavN);
            }
        }

        ///<summary>Current inorganic N in the available bucket</summary>
        public double ExN
        {
            get { return exN; }
            set
            {
                exN = value;
                UniverseLink.RoundZero(ref exN);
                Check.IsNumber(exN);
                Check.IsPositiveOrZero(exN);
            }
        }

        #endregion

        #endregion
    }
}