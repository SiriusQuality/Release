using System;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;

namespace SiriusModel.InOut
{
    ///<summary>
    ///Non Variety file. Contains all non-varietal parameter instance.
    ///</summary>
    [Serializable, XmlInclude(typeof(CropParameterItem))]
    public class MaizeNonVarietyFile : ProjectDataFile<CropParameterItem>
    {
        public override string ID
        {
            get { return FileContainer.MaizeNonVarietyID; }
        }

        public override string FileExtension
        {
            get { return ".sqparm"; }
        }
    }
}
