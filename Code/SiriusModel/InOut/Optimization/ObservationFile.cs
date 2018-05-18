using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiriusModel.InOut.Base;
using System.Xml.Serialization;

namespace SiriusModel.InOut
{
    ///<summary>
    ///Observation file. Contains instance of Observation.
    ///</summary>
    [Serializable, XmlInclude(typeof(ObservationItem))]
    public class ObservationFile : ProjectDataFile<ObservationItem>
    {
        public override string ID
        {
            get { return FileContainer.ObservationID; }
        }

        public override string FileExtension
        {
            get { return ".sqobs"; }
        }
    }
}
