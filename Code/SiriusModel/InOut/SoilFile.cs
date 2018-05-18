using System;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;

namespace SiriusModel.InOut
{
    ///<summary>
    ///Soil file. Contains all instance of Soil profile.
    ///</summary>
    [Serializable, XmlInclude(typeof(SoilItem))]
    public class SoilFile : ProjectDataFile<SoilItem>
    {
        public override string ID
        {
            get { return FileContainer.SoilID; }
        }

        public override string FileExtension
        {
            get { return ".sqsoi"; }
        }
    }
}
