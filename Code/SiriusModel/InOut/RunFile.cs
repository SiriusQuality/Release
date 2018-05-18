using System.Xml.Serialization;
using SiriusModel.InOut.Base;

namespace SiriusModel.InOut
{
    [XmlInclude(typeof(RunItem))]
    public class RunFile : ProjectDataFile<RunItem>
    {
        public override string ID
        {
            get { return FileContainer.RunID; }
        }

        public override string FileExtension
        {
            get { return ".sqrun"; }
        }
    }
}
