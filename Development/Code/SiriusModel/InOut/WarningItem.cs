using SiriusModel.InOut.Base;

namespace SiriusModel.InOut
{
    public class WarningItem : Child<string>
    {
        private readonly IProjectItem item;
        public IProjectItem ProjectItem { get { return item; } }

        public string FileID 
        { 
            get { return item.WarningFileID; } 
        }

        public string ItemName 
        {
            get { return item.WarningItemName; } 
        }

        private readonly string propertyName;
        public string PropertyName { get { return propertyName; } }

        private readonly string format;
        public string Format { get { return format; } }

        private object[] arguments;
        public object[] Arguments
        {
            get { return arguments; }
            set
            {
                arguments = value;
                text = null;
                NotifyPropertyChanged("Arguments");
                NotifyPropertyChanged("Text");
            }
        }

        private string text;
        public string Text { 
            get
            {
                if (text == null)
                {
                    if (arguments != null)
                    {
                        text = string.Format(format, arguments);
                    }
                    else
                    {
                        text = format;
                    }
                }
                return text;
            }
        }

        public WarningItem(IProjectItem item, string propertyName, string format)
        {
            this.item = item;
            this.propertyName = propertyName;
            this.format = format;
        }

        public override void CheckWarnings()
        {
        }
    }
}
