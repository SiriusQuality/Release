using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiriusModel.InOut.Base;
using System.Xml.Serialization;
using System.IO;

namespace SiriusModel.InOut
{
    public partial class ProjectFile
    {

        #region new load save

        static partial void partialNew()
        {
            This.Path = "?\\?";
            This.FileContainer.New(FileContainer.ManagementID);
            This.FileContainer.New(FileContainer.NonVarietyID);
            This.FileContainer.New(FileContainer.MaizeNonVarietyID);
            This.FileContainer.New(FileContainer.RunOptionID);
            This.FileContainer.New(FileContainer.RunID);
            This.FileContainer.New(FileContainer.SiteID);
            This.FileContainer.New(FileContainer.SoilID);
            This.FileContainer.New(FileContainer.VarietyID);
            This.FileContainer.New(FileContainer.MaizeVarietyID);
            This.FileContainer.New(FileContainer.OptimizationID);
            This.FileContainer.New(FileContainer.ObservationID);

            This.FileContainer.Comments = "";
            // ProjectFile.OutputVersion = OutputVersion.V15;

            This.FileContainer.IsModified = false;
        }

        #endregion

    }
}
