using System;
using System.Windows.Forms;
using SiriusModel.InOut;
using SiriusModel.InOut.Base;

namespace SiriusView.File
{
    public partial class SiteForm : ProjectDataFileForm
    {
        public SiteForm()
            : base(FileContainer.SiteID)
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            weatherFormatComboBox.Items.Add(SiteItem.YearJdayMinMaxRainRadName);
            weatherFormatComboBox.Items.Add(SiteItem.YearJdayMinMaxRainRadWindVpName);
            weatherFormatComboBox.Items.Add(SiteItem.YearJdayHourlyTempRainRadWindVpName);
            siteItemsBindingSource1.DataSource = ProjectFile.This.FileContainer.SiteFile.Items;
            siteItemsBindingSource1_CurrentChanged(this, null);
            comboSowWinType_SelectedIndexChanged(this, null);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            siteItemsBindingSource1.Add(new SiteItem("new site"));
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            siteItemsBindingSource1.RemoveCurrent();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            siteItemsBindingSource1.Clear();
        }

        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            var clone = (SiteItem)siteItemsBindingSource1.Current.Clone();
            clone.Name += "_dup";
            siteItemsBindingSource1.Add(clone);
            siteItemsBindingSource1.EndEdit();
        }

        private void siteItemsBindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            var enabled = siteItemsBindingSource1.Current != null;
            buttonDelete.UpdateEnabled(enabled);
            buttonDuplicate.UpdateEnabled(enabled);
            splitContainer1.Panel2.UpdateEnabled(enabled);
        }

        private void weatherFilesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void weatherFilesDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (siteItemsBindingSource1.Position != -1)
            {
                var siteItem = (SiteItem)siteItemsBindingSource1.Current;
                if (e.ColumnIndex == 0)
                {
                    if (siteItem.WeatherFiles.Count > e.RowIndex)
                    {
                        using (var weatherOpenFileDialog = new OpenFileDialog {FileName = siteItem.WeatherFiles[e.RowIndex].AbsoluteFile, Title = ("Select " + siteItem.Name + " weather file")})
                        {
                            if (weatherOpenFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                siteItem.WeatherFiles[e.RowIndex].File = weatherOpenFileDialog.FileName;
                            }
                        }
                    }
                }
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            ProjectFile.This.FileContainer.SiteFile.Sort();
        }

        private void linkLabelName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var item = siteItemsBindingSource1.Current as SiteItem;
            if (item == null) return;
            using (var renameDialog = new RenameItem())
            {
                renameDialog.Item = item;
                if (renameDialog.ShowDialog(this) == DialogResult.OK)
                {
                    renameDialog.Apply(n => n.SiteItem, (n, s) => n.SiteItem = s,
                        m => m.SiteItem, (m, s) => m.SiteItem = s);
                }
            }
        }

        private void buttonAddWeatherFile_Click(object sender, EventArgs e)
        {
            var item = siteItemsBindingSource1.Current as SiteItem;
            if (item == null) return;
            using (var weatherOpenFileDialog = new OpenFileDialog { Title = ("Select " + item.Name + " weather file") })
            {
                if (weatherOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    item.WeatherFiles.Add(new WeatherFile(weatherOpenFileDialog.FileName));
                }
            }
        }

        private void buttonSortWeatherFile_Click(object sender, EventArgs e)
        {
            var item = siteItemsBindingSource1.Current as SiteItem;
            if (item == null) return;
            item.Sort();
        }

        private void comboSowWinType_SelectedIndexChanged(object sender, EventArgs e)
        {
            minSowingDateDateTimePicker.Enabled = maxSowingDateDateTimePicker.Enabled = 
            (comboSowWinType.SelectedIndex == 0 || comboSowWinType.SelectedIndex == 2);

            if (comboSowWinType.SelectedIndex == 0) // Fixed
            {
                textBoxInitSowWindow.Enabled = false;
                textBoxMinLength.Enabled = false;
                textBoxTempSum.Enabled = false;
                textBoxPcpSum.Enabled = false;
                textBoxTempThr.Enabled = false;
                textBoxCheckDaysTemp.Enabled = false;
                textBoxCheckDaysPcp.Enabled = false;
            }
            else if (comboSowWinType.SelectedIndex == 1) // JRC Winter
            {
                textBoxInitSowWindow.Enabled = false;
                textBoxMinLength.Enabled = true;
                textBoxTempSum.Enabled = true;
                textBoxPcpSum.Enabled = false;
                textBoxTempThr.Enabled = true;
                textBoxCheckDaysTemp.Enabled = false;
                textBoxCheckDaysPcp.Enabled = false;
            }
            else if (comboSowWinType.SelectedIndex == 2) // JRC Spring
            {
                textBoxInitSowWindow.Enabled = false;
                textBoxMinLength.Enabled = true;
                textBoxTempSum.Enabled = false;
                textBoxPcpSum.Enabled = true;
                textBoxTempThr.Enabled = true;
                textBoxCheckDaysTemp.Enabled = true;
                textBoxCheckDaysPcp.Enabled = true;
            }
            else if (comboSowWinType.SelectedIndex == 3) // SiriusQuality method
            {
                textBoxInitSowWindow.Enabled = true;
                textBoxMinLength.Enabled = true;
                textBoxTempSum.Enabled = false;
                textBoxPcpSum.Enabled = true;
                textBoxTempThr.Enabled = true;
                textBoxCheckDaysTemp.Enabled = true;
                textBoxCheckDaysPcp.Enabled = true;
            }
        }

        private void weatherFilesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (siteItemsBindingSource1.Position != -1)
            {
                var siteItem = (SiteItem)siteItemsBindingSource1.Current;
                if (e.ColumnIndex == 0)
                {
                    if (siteItem.WeatherFiles.Count > e.RowIndex)
                    {
                        using (var weatherOpenFileDialog = new OpenFileDialog { FileName = siteItem.WeatherFiles[e.RowIndex].AbsoluteFile, Title = ("Select " + siteItem.Name + " weather file") })
                        {
                            if (weatherOpenFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                siteItem.WeatherFiles[e.RowIndex].File = weatherOpenFileDialog.FileName;
                            }
                        }
                    }
                }
            }
        }

        private void linkLabelName_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var item = siteItemsBindingSource1.Current as SiteItem;
            if (item == null) return;
            using (var renameDialog = new RenameItem())
            {
                renameDialog.Item = item;
                if (renameDialog.ShowDialog(this) == DialogResult.OK)
                {
                    renameDialog.Apply(n => n.SiteItem, (n, s) => n.SiteItem = s,
                        m => m.SiteItem, (m, s) => m.SiteItem = s);
                }
            }
        }
    }
}
