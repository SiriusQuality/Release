using System;
using System.ComponentModel;
using SiriusModel.InOut;

namespace SiriusView
{
    [Serializable()]
    public class ProjectFileBindingSource : BaseBindingSource
    {
        public ProjectFileBindingSource(IContainer container)
            : base(container)
        {
            if (DesignMode)
            {
                DataSource = typeof(ProjectFile);
            }
            else
            {
                DataSource = ProjectFile.ThisArray;
            }
        }
    }
}
