using System;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;

namespace SiriusModel.InOut
{
    ///<summary>
    ///Non Variety file. Contains all non-varietal parameter instance.
    ///</summary>
    [Serializable, XmlInclude(typeof(CropParameterItem))]
    public class NonVarietyFile : ProjectDataFile<CropParameterItem>
    {
        public override string ID
        {
            get { return FileContainer.NonVarietyID; }
        }

        public override string FileExtension
        {
            get { return ".sqpar"; }
        }
    }
}
