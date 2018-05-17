using System;

namespace SiriusModel.Model.CropModel.Phytomers

{
    ///<summary>The ear peduncle of the crop</summary>
    public class EarPeduncle : Phytomer
    {
        ///<summary>The last internode</summary>
        public InterNode InterNode_ { get; protected set; }

        ///<summary>Initial constructor</summary>
        ///<param name="universe">The universe of this ear peduncle.</param>
        ///<param name="i">The index of this phytomer (= FinalLeafNumber + 1).</param>
        public EarPeduncle(Universe universe, int i, double cumulTTShoot, int roundedFinalNumber,double finalLeafNumber)
            : base(universe, i, cumulTTShoot)
        {
            InterNode_ = new InterNode(universe, roundedFinalNumber, isSmallPhytomer(finalLeafNumber, roundedFinalNumber), Index, PhytoNum);
        }

        ///<summary>Copy constructor</summary>
        ///<param name="universe">The universe of this ear peduncle.</param>
        ///<param name="toCopy">The ear peduncle to copy.</param>
        public EarPeduncle(Universe universe, EarPeduncle toCopy)
            : base(universe, toCopy)
        {
            InterNode_ = new InterNode(universe, toCopy.InterNode_);
        }

        ///<summary>Clone this ear peduncle</summary>
        ///<param name="universe"></param>
        ///<returns></returns>
        public override Phytomer Clone(Universe universe)
        {
            return new EarPeduncle(universe, this);
        }


        ///<summary>The growing thermal time of the internode</summary>
        public double InterNodeTTgro { get { return InterNode_.TTgro(/*LayerPhyllochron*/ 0); } }

        public double getInternodeLength() { return InterNode_.Length; }

        public double getInternodeLengthPot() { return InterNode_.LengthPot; }

    }
}