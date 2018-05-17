using System;
using System.ComponentModel;
using SiriusModel.InOut;

namespace SiriusView 
{
    [Serializable()]
    public class FileContainerBindingSource : BaseBindingSource
    { 
        public FileContainerBindingSource(IContainer container)
            : base(container)
        {
            if (DesignMode)
            {
                DataSource = typeof(FileContainer);
            }
            else
            {
                DataSource = ProjectFile.This.FileContainerArray;
            }
        }
    }
}
