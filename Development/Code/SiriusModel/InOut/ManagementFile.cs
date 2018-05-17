using System;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;

namespace SiriusModel.InOut
{
    ///<summary>
    ///Management file. Contains instance of Management.
    ///</summary>
    [Serializable, XmlInclude(typeof(ManagementItem))]
    public class ManagementFile : ProjectDataFile<ManagementItem>
    {
        public override string ID
        {
            get { return FileContainer.ManagementID; }
        }

        public override string FileExtension
        {
            get { return ".sqman"; }
        }
    }
}
