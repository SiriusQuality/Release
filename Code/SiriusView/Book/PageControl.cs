using System;
using System.Windows.Forms;
using SiriusModel.InOut.OutputWriter;

namespace SiriusView.Controls
{
    public partial class PageControl : UserControl
    {
        private PageData page;
        private int rowCount;
        private int columnCount;

        public PageControl()
        {
            InitializeComponent();
            Page = null;
        }

        public PageData Page
        {
            get { return page; }
            set 
            { 
                page = value;
                if (page == null)
                {
                    rowCount = 1;
                    columnCount = 1;
                }
                else
                {
                    rowCount = page.Count;
                    columnCount = 1;
                    foreach (var row in page)
                    {
                        columnCount = Math.Max(columnCount, row.Count);
                    }
                }

                dataGridView1.SuspendLayout();
                dataGridView1.UpdateSize(rowCount, columnCount);
                dataGridView1.Refresh();
                dataGridView1.ResumeLayout();
            }
        }

        private void dataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex < rowCount && (page != null && e.RowIndex < page.Count))
            {
                var row = page[e.RowIndex];
                if (row != null && e.ColumnIndex < row.Count)
                {
                    e.Value = row[e.ColumnIndex];
                }
                else e.Value = null;
            }
            else e.Value = null;
        }
    }
}
