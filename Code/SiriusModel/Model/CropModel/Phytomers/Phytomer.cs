using SiriusModel.Model.Base;
using SiriusModel.Model.Observation;

namespace SiriusModel.Model.CropModel.Phytomers
{
    ///<summary>Base class for phytomer</summary>
    public abstract class Phytomer : UniverseLink
    {

        ///<summary>Thermal time from sowing to emergence of phytomer i, °CDay</summary>
        public double TTem { get; private set; }

        ///<summary>Get the index of this phytomer (= PhytoNum -1, 0 based indexation from the lower phytomer to the upper)</summary>
        public int Index { get; private set; }

        ///<summary>Initial constructor</summary>
        ///<param name="universe">The universe of this phytomer</param>
        ///<param name="i">The index of this phytomer</param>
        public Phytomer(Universe universe, int i,double cumulTTShoot ) : base(universe)
        {
            TTem = cumulTTShoot; // pm 28 May 2013, replaced air temperature by canopy temperature
            Index = i;
        }

        ///<summary>Copy constructor</summary>
        ///<param name="universe">The universe of this phytomer</param>
        ///<param name="toCopy">The index of this phytomer</param>
        public Phytomer(Universe universe, Phytomer toCopy) : base(universe)
        {
            TTem = toCopy.TTem;
            Index = toCopy.Index;
        }

        ///<summary>Get the phytomer number (= Index + 1, 1 based indexation from the lower phytomer to the upper)</summary>
        public int PhytoNum
        {
            get { return Index + 1; }
        }

        ///<summary>Get a boolean that indicates if this phytomer is a small phytomer or not</summary>
        public bool isSmallPhytomer(double finalLeafNumber,int roundedFinalLeafNumber)
        {           
            if (finalLeafNumber > 0)
            {
                // number of phytomer is known
                if (Index < roundedFinalLeafNumber - NLL)
                {
                    // index is less than the upper limit of small leaves.
                    return true;
                }
                else
                {
                    // index is greather than the upper limit of small leaves.
                    return false;
                }
            }
            else
            {
                // number of phytomer is unknown, it is a small leaf.
                return true;
            }         
        }

        ///<summary>Returns true if this phytomer is the flag leaf</summary>
        public bool IsFlagPhytomer(double leafNumber,int roundedFinalLeafNumber)
        {
          
                if (leafNumber > 0)
                {
                    // number of phytomer is known
                    if (Index == roundedFinalLeafNumber - 1)
                    {
                        return true;
                    }
                    return false;
                }
                // number of phytomer is unknown, it is not the flag leaf.
                return false;         
        }


        ///<summary>Method to implement in phytomer in order to duplicate data</summary>
        ///<param name="universe">The universe of the returned clone</param>
        ///<returns>aSheath clone of this phytomer</returns>
        public abstract Phytomer Clone(Universe universe);
    }
}