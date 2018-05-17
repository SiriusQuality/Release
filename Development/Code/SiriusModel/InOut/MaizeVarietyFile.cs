using System;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;

namespace SiriusModel.InOut
{
    ///<summary>
    ///Genotype file. Contains all instance of Genotype for maize.
    ///</summary>
    [Serializable, XmlInclude(typeof(CropParameterItem))]
    public class MaizeVarietyFile : ProjectDataFile<CropParameterItem>
    {
        public override string ID
        {
            get { return FileContainer.MaizeVarietyID; }
        }

        public override string FileExtension
        {
            get { return ".sqvarm"; }
        }
    }
}
