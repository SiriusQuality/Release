using System.Linq;

namespace SiriusModel.InOut.OutputWriter
{
    public class Book : RawData<PageData>
    {
        public PageData this[string pageTitle]
        {
            get
            {
                return this.FirstOrDefault(delegate(PageData page) { return page.Title == pageTitle; });
            }
        }

        public new Book Clear()
        {
            var count = Count;
            for (var i = 0; i < count; ++i)
            {
                var page = this[i];
                if (page != null) page.Clear();
            }
            base.Clear();
            return this;
        }
    }
}