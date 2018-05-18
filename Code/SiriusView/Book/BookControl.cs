using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SiriusModel.InOut.OutputWriter;

namespace SiriusView.Controls
{
    public partial class BookControl : UserControl
    {
        private Book book;

        public BookControl()
        {
            InitializeComponent();
            Book = null;
        }

        [Bindable(BindableSupport.Yes, BindingDirection.OneWay)]
        public Book Book
        {
            get { return book; }
            set
            {
                book = value;
                var pageCount = (book != null) ? book.Count : 0;

                tabControl1.SuspendLayout();
                for (var i = 0; i < pageCount; ++i)
                {
                    var tabPage = (tabControl1.TabPages.Count > i) ? tabControl1.TabPages[i] : null;
                    PageControl pageControl;

                    if (tabPage == null)
                    {
                        tabPage = new TabPage();
                        pageControl = new PageControl();
                        tabPage.BackColor = SystemColors.Window;
                        pageControl.Dock = DockStyle.Fill;
                        tabPage.Controls.Add(pageControl);
                        tabControl1.TabPages.Add(tabPage);
                    }
                    else
                    {
                        pageControl = (PageControl)tabPage.Controls[0];
                    }
// ReSharper disable PossibleNullReferenceException
                    tabPage.UpdateText(book[i].Title);
// ReSharper restore PossibleNullReferenceException
                    pageControl.Page = book[i];
                }

                var pageToDeleteCount = tabControl1.TabPages.Count;
                for (var i = pageCount; i < pageToDeleteCount; ++i)
                {
                    var tabPage = tabControl1.TabPages[pageCount];
                    tabControl1.TabPages.RemoveAt(pageCount);
                    tabPage.Dispose();
                }
                tabControl1.ResumeLayout();
            }

            
        }

        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }
    }
}
