using System;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;

namespace SiriusModel.InOut
{
    ///<summary>
    ///Site file. Contains instance of Sites.Site.
    ///</summary>
    [Serializable, XmlInclude(typeof(SiteItem))]
    public class SiteFile : ProjectDataFile<SiteItem>
    {
        public override string ID
        {
            get { return FileContainer.SiteID; }
        }

        public override string FileExtension
        {
            get { return ".sqsit"; }
        }
    }
}
