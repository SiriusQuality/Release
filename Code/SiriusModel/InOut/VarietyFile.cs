using System;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;

namespace SiriusModel.InOut
{
    ///<summary>
    ///Genotype file. Contains all instance of Genotype.
    ///</summary>
    [Serializable, XmlInclude(typeof(CropParameterItem))]
    public class VarietyFile : ProjectDataFile<CropParameterItem>
    {
        public override string ID
        {
            get { return FileContainer.VarietyID; }
        }

        public override string FileExtension
        {
            get { return ".sqvar"; }
        }
    }
}
