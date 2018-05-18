using System.ComponentModel;
using System.Collections.Generic;
using SiriusModel.InOut;
using SiriusModel.InOut.Base;

namespace SiriusView
{
    public class ProjectDataFileBindingSource : BaseBindingSource
    {
        public ProjectDataFileBindingSource(IContainer container)
            : base(container)
        {
            DataSource = typeof(IProjectDataFile);
        }

        public void SetID(string inputFileID)
        {
            var tempArray = new IProjectDataFile[1];
            tempArray[0] = ProjectFile.This.FileContainer[inputFileID];
            DataSource = tempArray;
        }
    }
}
