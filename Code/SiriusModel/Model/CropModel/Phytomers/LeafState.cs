namespace SiriusModel.Model.CropModel.Phytomers
{
    ///<summary>This enumerates particular development state of leaves. This is equivalent to DEV_* in SiriusQuality</summary>
    public enum LeafState
    {
        ///<summary>The leaf is growing</summary>
        Growing,

        ///<summary>The leaf is mature</summary>
        Mature,

        ///<summary>The leaf is senescing</summary>
        Senescing, 

        ///<summary>The leaf is dead</summary>
        Dead

    }
}