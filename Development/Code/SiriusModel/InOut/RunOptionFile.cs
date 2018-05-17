using System;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;

namespace SiriusModel.InOut
{
    ///<summary>
    ///RunOption file. Contains instance of RunOptions.RunOption. RunOptions specify 
    ///algorithms to use.
    ///</summary>
    [Serializable, XmlInclude(typeof(RunOptionItem))]
    public class RunOptionFile : ProjectDataFile<RunOptionItem>
    {
        public override string ID
        {
            get { return FileContainer.RunOptionID; }
        }

        public override string FileExtension
        {
            get { return ".sqopt"; }
        }
    }
}
