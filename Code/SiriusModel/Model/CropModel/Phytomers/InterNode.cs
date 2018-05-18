using System;
using SiriusModel.Model.Observation;
using SiriusModel.Model.Base;

namespace SiriusModel.Model.CropModel.Phytomers
{
    ///<summary>The class internode represent an internode of a phytomer</summary>
    public class InterNode : UniverseLink
    {
        #region fields 

        ///<summary>Current internode length, m</summary>
        public double Length { get; set; }

        ///<summary>Potential length of this internode</summary>
        public double LengthPot { get; protected set; }

        ///<summary>True if this internode is the internode of the ear peduncle</summary>
        public bool IsEarInterNode { get; protected set; }

        ///<summary>The growing thermal time of this internode</summary>
        public double TTgro (double layerPhyllochron)
        {
                if (IsEarInterNode)
                {
                    return FixPhyll * PexpIN;
                }
                else 
                {
                    return layerPhyllochron * PexpIN;
                }
        }

        #endregion 

        ///<summary>Initial constructor</summary>
        ///<param name="phytomer">The phytomer of this internode</param>
        public InterNode(Universe universe,int roundedFinalNumber,bool isSmallPhytomer,int index, int phytonum): base(universe)
           // : base(phytomer)
        {
            IsEarInterNode = false;
            Length = 0;
            if (isSmallPhytomer)
            {
                LengthPot = 0;
            }
            else if (index > roundedFinalNumber - (NLL - 1))
            {
                if (index < roundedFinalNumber)
                {
                    LengthPot = -L_IN1 * (roundedFinalNumber - phytonum) / (NLL - 1) + L_IN1;
                }
                else
                {
                    IsEarInterNode = true;
                    LengthPot = L_EP;
                }
            }
        }

        ///<summary>Copy constructor</summary>
        ///<param name="phytomer">The phytomer of this internode.</param>
        ///<param name="toCopy">The internode to copy</param>
        public InterNode(Universe universe, InterNode toCopy) : base(universe)
        {
            IsEarInterNode = toCopy.IsEarInterNode;
            Length = toCopy.Length;
            LengthPot = toCopy.LengthPot;
        }

    }
}