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
    ///Optimization file. Contains instance of Optimization.
    ///</summary>
    [Serializable, XmlInclude(typeof(OptimizationItem))]
    public class OptimizationFile : ProjectDataFile<OptimizationItem>
    {
        public override string ID
        {
            get { return FileContainer.OptimizationID; }
        }

        public override string FileExtension
        {
            get { return ".sqopz"; }
        }
    }
}
