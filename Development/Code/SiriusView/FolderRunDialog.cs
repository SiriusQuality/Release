using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SiriusModel.InOut;
using SiriusModel.InOut.Base;
using SiriusModel.Model;
using SiriusModel.Model.CropModel;
using SiriusView.Properties;

namespace SiriusView
{
    public partial class FolderRunDialog : Form
    {
        private readonly List<string> lastFolderRuns;

        public FolderRunDialog()
        {
            InitializeComponent();
            var lastFolderRunList = Settings.Default.LastFolderRuns;
            lastFolderRuns = new List<string>();
            foreach (var lastFolderRun in lastFolderRunList.Split(new []{'?'}, StringSplitOptions.RemoveEmptyEntries))
            {
                lastFolderRuns.Add(lastFolderRun);
                comboBox1.Items.Add(lastFolderRun);
            }
            if (lastFolderRuns.Count > 0) comboBox1.SelectedItem = lastFolderRuns[0];
            backgroundWorker1.DoWork += RunJob;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            beginInvoke = i => BeginInvoke(i);
            invoke = i => Invoke(i);
            exit = false;
            DefaultStatus();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!Abort())
            {
                e.Cancel = true;
                base.OnClosed(e);
                return;
            }
            OnQuit();
            exit = true;
            beginInvoke = i => { };
            invoke = i => { };
            base.OnClosed(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (exit) return;
            OnQuit();
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (exit) return;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!lastFolderRuns.Contains(folderBrowserDialog1.SelectedPath))
                {
                    comboBox1.Items.Insert(0, folderBrowserDialog1.SelectedPath);
                    lastFolderRuns.Insert(0, folderBrowserDialog1.SelectedPath);
                }
                else
                {
                    lastFolderRuns.Remove(folderBrowserDialog1.SelectedPath);
                    lastFolderRuns.Insert(0, folderBrowserDialog1.SelectedPath);
                    comboBox1.Items.Remove(folderBrowserDialog1.SelectedPath);
                    comboBox1.Items.Insert(0, folderBrowserDialog1.SelectedPath);
                }
                comboBox1.SelectedItem = folderBrowserDialog1.SelectedPath;
            }
        }

        private void OnQuit()
        {
            Settings.Default.LastFolderRuns = lastFolderRuns.Take(10).Aggregate("", (s, f) => s + '?' + f);
        }

        private void StartClick(object sender, EventArgs e)
        {
            if (exit) return;
            isRunning = true;
            hasCancelled = false;
            button3.Enabled = true;
            button2.Enabled = false;
            status = null;
            permanentStatus = null;
            result = null;
            errors.Clear();
            progressBar1.Value = 0;
            label2.Text = null;
            label3.Text = null;
            tableLayoutPanel1.Enabled = false;
            progressBar1.Visible = true;
            FolderPath = (string)comboBox1.SelectedItem;
            backgroundWorker1.RunWorkerAsync();
        }

        private static string FolderPath;
        private string outputFilePath;
        private string status;
        private string permanentStatus;
        private string result;
        private List<Error> errors = new List<Error>();

        private string fileToLoad;
        private Type fileTypeToLoad;
        private IProjectItem loadedItem;

        private bool isRunning;
        private bool hasCancelled;
        private bool exit;

        private Action<InvokeDelegate> beginInvoke;
        private Action<InvokeDelegate> invoke;

        void RunJob(object sender, DoWorkEventArgs e)
        {
            DirectoryInfo folderInfo = null;
            try
            {
                if (exit) return;

                folderInfo = new DirectoryInfo(FolderPath);

                saveFileDialog1.Title = @"Select 'Folder run' summary file location";
                saveFileDialog1.AddExtension = true;

                outputFilePath = null;
                InvokeDelegate selectOutputPath = () =>
                {
                    saveFileDialog1.FileName = FolderPath + "\\" + folderInfo.Name + ".sqfro";
                    if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                    {
                        outputFilePath = null;
                    }
                    else outputFilePath = saveFileDialog1.FileName;
                };
                Invoke(selectOutputPath);

                if (outputFilePath == null) return;


                var updateDelegate = new InvokeDelegate(UpdateStatus);
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                Action<bool, Func<string>, Func<int?>> updateStatus = (f, s, p) =>
                {
                    if (f || stopWatch.ElapsedMilliseconds > 750)
                    {
                        stopWatch.Reset();
                        stopWatch.Start();
                        status = (permanentStatus ?? "") + (s != null ? s() : null);
                        beginInvoke(updateDelegate);
                        var percent = p != null ? p() : null;
                        if (percent.HasValue) backgroundWorker1.ReportProgress(percent.Value);
                        else backgroundWorker1.ReportProgress(0);
                    }
                };
                var loadDelegate = new InvokeDelegate(() =>
                {
                    var file = (IProjectItem)Serialization.DeserializeXml(fileTypeToLoad, fileToLoad);
                    file.ClearWarnings();
                    loadedItem = file;
                });
                Func<string, Type, IProjectItem> loadFile = (f, t) =>
                {
                    fileToLoad = f;
                    fileTypeToLoad = t;
                    invoke(loadDelegate);
                    return loadedItem;
                };

                permanentStatus = "Loading folder files...\n\n";
                updateStatus(true, () => null, () => 0);
                var files = folderInfo.GetFiles("*", SearchOption.AllDirectories);
                var fileIndex = 0;
                var fileCount = files.Length;

                var managements = new List<Item<ManagementItem>>();
                var parameters = new List<Item<CropParameterItem>>();
                var sites = new List<Item<SiteItem>>();
                var soils = new List<Item<SoilItem>>();
                var varieties = new List<Item<CropParameterItem>>();

                foreach (var fileInfo in files)
                {
                    var info = fileInfo;
                    var index = fileIndex;
                    if (backgroundWorker1.CancellationPending)
                    {
                        hasCancelled = true;
                        break;
                    }
                    var extension = fileInfo.Extension.ToLower();
                    switch (extension)
                    {
                        case ".sqman":
                            updateStatus(false, () => "Loading management file: '" + info.FullName + "'...", () => 100 * index / fileCount);
                            var managementFile = (ManagementFile)loadFile(fileInfo.FullName, typeof(ManagementFile));
                            managements.AddRange(managementFile.Items.Select(i => new Item<ManagementItem> { SourceFile = info.FullName, Value = i }));
                            break;
                        case ".sqpar":
                            updateStatus(false, () => "Loading non-varietal parameter file: '" + info.FullName + "'...", () => 100 * index / fileCount);
                            var nonVarietyFile = (NonVarietyFile)loadFile(fileInfo.FullName, typeof(NonVarietyFile));
                            parameters.AddRange(nonVarietyFile.Items.Select(i => new Item<CropParameterItem> { SourceFile = info.FullName, Value = i }));
                            break;
                        case ".sqsit":
                            updateStatus(false, () => "Loading site file: '" + info.FullName + "'...", () => 100 * index / fileCount);
                            var siteFile = (SiteFile)loadFile(fileInfo.FullName, typeof(SiteFile));
                            sites.AddRange(siteFile.Items.Select(i =>
                            {
                                foreach (var file in i.WeatherFiles)
                                {
                                    file.File = Path.Combine(info.DirectoryName, file.File);
                                }
                                return new Item<SiteItem> { SourceFile = info.FullName, Value = i };
                            }));
                            break;
                        case ".sqsoi":
                            updateStatus(false, () => "Loading soil file: '" + info.FullName + "'...", () => 100 * index / fileCount);
                            var soilFile = (SoilFile)loadFile(fileInfo.FullName, typeof(SoilFile));
                            soils.AddRange(soilFile.Items.Select(i => new Item<SoilItem> { SourceFile = info.FullName, Value = i }));
                            break;
                        case ".sqvar":
                            updateStatus(false, () => "Loading variety file: '" + info.FullName + "'...", () => 100 * index / fileCount);
                            var varietyFile = (VarietyFile)loadFile(fileInfo.FullName, typeof(VarietyFile));
                            varieties.AddRange(varietyFile.Items.Select(i => new Item<CropParameterItem> { SourceFile = info.FullName, Value = i }));
                            break;
                        default:
                            updateStatus(false, () => "Skipping file: '" + info.FullName + "'...", () => 100 * index / fileCount);
                            break;
                    }
                    ++fileIndex;
                }

                if (backgroundWorker1.CancellationPending)
                {
                    hasCancelled = true;
                    return;
                }

                var totalNumberOfCombinations = ((long)managements.Count) * ((long)parameters.Count) * ((long)sites.Count) * ((long)soils.Count) * ((long)varieties.Count);

                permanentStatus = string.Format("Found: {0} managements, {1} parameters, {2} sites, {3} soils, {4} varieties...\n{5} runs.\n\n",
                    managements.Count, parameters.Count, sites.Count, soils.Count, varieties.Count, totalNumberOfCombinations);
                updateStatus(true, () => "", null);
                Thread.Sleep(totalNumberOfCombinations == 0 ? 3000 : 2000);
                if (totalNumberOfCombinations < 0) throw new OverflowException("Number of combination is greater than " + long.MaxValue + ". Can't proceed.");
                if (totalNumberOfCombinations == 0)
                {
                    result = "0 / 0 runs processed.";
                    return;
                }

                var run = new Run();
                var runOptions = new RunOptionItem("FolderRun") { 
                    UseObservedGrainNumber = checkBoxGrainNumber.Checked,
                    UnlimitedNitrogen = checkBoxLimitNitrogen.Checked,
                    UnlimitedWater = checkBoxLimitWater.Checked,
                };
                var combinationIndex = 0L;
                using (var outputFile = new StreamWriter(outputFilePath))
                {
                    WriteVersionDate(outputFile);
                    outputFile.WriteLine();
                    outputFile.WriteLine();
                    WriteColumnHeaders(outputFile);

                    foreach (var combination in CartesianProduct(totalNumberOfCombinations, managements, parameters, sites, soils, varieties))
                    {
                        var c = combination;
                        var index = combinationIndex;
                        updateStatus(false, () => string.Format("Processing #{5}: {0}, {1}, {2}, {3}, {4}...",
                            c.Management.Value.Name, c.Parameter.Value.Name, c.Site.Value.Name, c.Soil.Value.Name, c.Variety.Value.Name, index),
                            () => (int)(100 * (index / totalNumberOfCombinations)));

                        try
                        {
                            run.Start(c.Variety.Value, c.Soil.Value, c.Site.Value, c.Management.Value, c.Parameter.Value, runOptions);
                            WriteColumnValues(outputFile, combination, run);
                        }
                        catch (Exception runningException)
                        {
                            errors.Add(new Error { Exception = runningException, Combination = combination });
                        }

                        ++combinationIndex;

                        if (backgroundWorker1.CancellationPending)
                        {
                            hasCancelled = true;
                            break;
                        }
                    }
                }
                result = combinationIndex + " / " + totalNumberOfCombinations + " runs processed.";

            }
            catch (Exception exception)
            {
                if (folderInfo == null) exception = new Exception(@"Invalid folder: '" + FolderPath + @"'.");
                errors.Insert(0, new Error { Exception = exception });
            }
            finally
            {
                if (!exit) beginInvoke(JobEnd);
            }
        }

        private delegate void InvokeDelegate();

        void JobEnd()
        {
            if (exit) return;
            tableLayoutPanel1.Enabled = true;
            progressBar1.Visible = false;
            
            DefaultStatus();

            if (errors.Count > 0)
            {
                var exceptionMessageBuilder = new StringBuilder();

                foreach (var error in errors)
                {
                    exceptionMessageBuilder.AppendLine("Exception: " + error.Exception.Message + "in: ");
                    if (!error.IsGlobal)
                    {
                        foreach (var item in error.Combination.Items)
                        {
                            exceptionMessageBuilder.AppendLine(item.TypeName + ": File: '" + item.SourceFile + "' Item:'" + item.Value.Name);
                        }
                    }
                    else
                    {
                        exceptionMessageBuilder.AppendLine("At global level.");
                    }
                    exceptionMessageBuilder.AppendLine(error.Exception.StackTrace);
                    exceptionMessageBuilder.AppendLine();
                    exceptionMessageBuilder.AppendLine();
                }
                var exceptionMessage = exceptionMessageBuilder.ToString();
                using (var errorDialog = new ErrorDialog("Folder run error", new InvalidOperationException(exceptionMessage)))
                {
                    errorDialog.ShowDialog(Owner);
                }
                try
                {
                    System.IO.File.WriteAllText(outputFilePath + ".errors.txt", exceptionMessage);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"Unable to write error file. Check that the selected output path is a valid path.", @"Error when writing error file.");
                } 
            }
            result = hasCancelled ? "Operation has been cancelled. " + result : result;
            result = errors.Count > 0 ? "Some error occured. " + result : result;
            errors.Clear();
            label3.Text = result;
            isRunning = false;
            button3.Enabled = false;
            button2.Enabled = true;
        }

        void UpdateStatus()
        {
            if (exit) return;
            label2.Text = status;
        }

        void DefaultStatus()
        {
            label2.Text = @"Select a folder containing the inputs files you want to run in a full-factorial design. The folder should contain at least one management (*.sqman), non-varietal parameter (*.sqpar) , site (*.sqsit), soil (*.sqsoi), and varietal parameter (*.sqvar) file.";
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (exit) return;
            progressBar1.Value = e.ProgressPercentage;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (exit) return;
            button4.Enabled = comboBox1.SelectedItem != null;
        }

        private struct Combination
        {
            public Item<ManagementItem> Management;
            public Item<CropParameterItem> Parameter;
            public Item<SiteItem> Site;
            public Item<SoilItem> Soil;
            public Item<CropParameterItem> Variety;

            public IEnumerable<Item<IProjectDataFileItem>> Items
            {
                get
                {
                    yield return new Item<IProjectDataFileItem> {SourceFile = Management.SourceFile, Value = Management.Value};
                    yield return new Item<IProjectDataFileItem> {SourceFile = Parameter.SourceFile, Value = Parameter.Value};
                    yield return new Item<IProjectDataFileItem> {SourceFile = Site.SourceFile, Value = Site.Value};
                    yield return new Item<IProjectDataFileItem> {SourceFile = Soil.SourceFile, Value = Soil.Value};
                    yield return new Item<IProjectDataFileItem> {SourceFile = Variety.SourceFile, Value = Variety.Value};
                }
            }
        }

        private struct Item<T> where T : IProjectDataFileItem
        {
            public T Value;
            public string SourceFile;
            public string TypeName
            {
                get { return Value.GetType().Name.Replace("Item", ""); }
            }
            public string ID
            {
                get { return SourceFile.Replace(FolderPath, "") + " @ " + Value.Name; }
            }
        }

        private struct Error
        {
            public Exception Exception;
            public Combination Combination;
            public bool IsGlobal
            {
                get { return Combination.Management.Value == null; }
            }
        }

        private static IEnumerable<Combination> CartesianProduct(long totalNumberOfCombinations,
            List<Item<ManagementItem>> managements, List<Item<CropParameterItem>> parameters,
            List<Item<SiteItem>> sites, List<Item<SoilItem>> soils, List<Item<CropParameterItem>> varieties)
        {
            var items = new Combination();
            for (var c = 0L; c < totalNumberOfCombinations; ++c)
            {
                long remainder;
                var position = c;

                position = Math.DivRem(position, managements.Count, out remainder);
                var managementItem = managements[(int) remainder];
                items.Management = managementItem;

                position = Math.DivRem(position, parameters.Count, out remainder);
                items.Parameter = parameters[(int)remainder];

                position = Math.DivRem(position, sites.Count, out remainder);
                items.Site = sites[(int)remainder];

                position = Math.DivRem(position, soils.Count, out remainder);
                items.Soil = soils[(int)remainder];

                position = Math.DivRem(position, varieties.Count, out remainder);
                items.Variety = varieties[(int)remainder];

                yield return items;
            }
        }

        private void DoAbort(object sender, EventArgs e)
        {
            Abort();
        }

        private bool Abort()
        {
            if (exit) return true;
            if (isRunning)
            {
                if (MessageBox.Show(@"Abort run?", @"Are you sure to abort this folder run?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    backgroundWorker1.CancelAsync();
                }
                else return false;
            }
            return true;
        }

        private static void WriteVersionDate(StreamWriter outputFile)
        {
            outputFile.Write("Sirius-v1.5 FolderRun outputFile");
            outputFile.Write('\t');
            outputFile.Write(DateTime.Now);
            outputFile.Write('\t');
            outputFile.Write("Build:");
            outputFile.Write('\t');
            outputFile.Write(ProjectFile.Build);
            outputFile.Write('\t');
            outputFile.Write("Folder:");
            outputFile.Write('\t');
            outputFile.Write(FolderPath);
        }

        private static void WriteColumnHeaders(StreamWriter outputFile)
        {
            outputFile.Write("Management");
            outputFile.Write('\t');
            outputFile.Write("Non-varietal parameters");
            outputFile.Write('\t');
            outputFile.Write("Site");
            outputFile.Write('\t');
            outputFile.Write("Soil");
            outputFile.Write('\t');
            outputFile.Write("Variety");
            outputFile.Write('\t');
            outputFile.Write("Sowing date");
            foreach (var header in OutputFileExtractorV15.GrowthStageHeaders1())
            {
                outputFile.Write('\t');
                outputFile.Write(header);
            }
            foreach (var header in OutputFileExtractorV15.SummaryHeaders1())
            {
                outputFile.Write('\t');
                outputFile.Write(header);
            }
            outputFile.WriteLine();

            outputFile.Write((object)null);
            outputFile.Write('\t');
            outputFile.Write((object)null);
            outputFile.Write('\t');
            outputFile.Write((object)null);
            outputFile.Write('\t');
            outputFile.Write((object)null);
            outputFile.Write('\t');
            outputFile.Write((object)null);
            outputFile.Write('\t');
            outputFile.Write("yyyy-mm-dd");
            foreach (var header in OutputFileExtractorV15.GrowthStageHeaders2())
            {
                outputFile.Write('\t');
                outputFile.Write(header);
            }
            foreach (var header in OutputFileExtractorV15.SummaryHeaders2())
            {
                outputFile.Write('\t');
                outputFile.Write(header);
            }
        }

        private static void WriteColumnValues(StreamWriter outputFile, Combination combination, Run run)
        {
            outputFile.WriteLine();
            outputFile.Write(combination.Management.ID);
            outputFile.Write('\t');
            outputFile.Write(combination.Parameter.ID);
            outputFile.Write('\t');
            outputFile.Write(combination.Site.ID);
            outputFile.Write('\t');
            outputFile.Write(combination.Soil.ID);
            outputFile.Write('\t');
            outputFile.Write(combination.Variety.ID);
            outputFile.Write('\t');
            outputFile.Write(combination.Management.Value.SowingDate.ToString("u").Split()[0]);
            foreach (var value in OutputFileExtractorV15.GrowthStageValues(run))
            {
                outputFile.Write('\t');
                outputFile.Write(value);
            }
            foreach (var value in OutputFileExtractorV15.SummaryValues(run))
            {
                outputFile.Write('\t');
                outputFile.Write(value);
            }
        }
    }
}
/*

                var duplicatedManagements = from i in managements where managements.Count(item => !ReferenceEquals(i.Value, item.Value) && i.Value.Name == item.Value.Name) > 0 select i;
                var duplicatedParameters = from i in parameters where parameters.Count(item => !ReferenceEquals(i.Value, item.Value) && i.Value.Name == item.Value.Name) > 0 select i;
                var duplicatedSites = from i in sites where sites.Count(item => !ReferenceEquals(i.Value, item.Value) && i.Value.Name == item.Value.Name) > 0 select i;
                var duplicatedSoils = from i in soils where soils.Count(item => !ReferenceEquals(i.Value, item.Value) && i.Value.Name == item.Value.Name) > 0 select i;
                var duplicatedVarieties = from i in varieties where varieties.Count(item => !ReferenceEquals(i.Value, item.Value) && i.Value.Name == item.Value.Name) > 0 select i;

                var hasDuplicate = false;
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Duplicated managements:");
                foreach (var d in duplicatedManagements)
                {
                    stringBuilder.AppendLine(string.Format("{0}\\{1}", d.SourceFile, d.Value.Name));
                    hasDuplicate = true;
                }
                stringBuilder.AppendLine("Duplicated parameters:");
                foreach (var d in duplicatedParameters)
                {
                    stringBuilder.AppendLine(string.Format("{0}\\{1}", d.SourceFile, d.Value.Name));
                    hasDuplicate = true;
                }
                stringBuilder.AppendLine("Duplicated sites:");
                foreach (var d in duplicatedSites)
                {
                    stringBuilder.AppendLine(string.Format("{0}\\{1}", d.SourceFile, d.Value.Name));
                    hasDuplicate = true;
                }
                stringBuilder.AppendLine("Duplicated soils:");
                foreach (var d in duplicatedSoils)
                {
                    stringBuilder.AppendLine(string.Format("{0}\\{1}", d.SourceFile, d.Value.Name));
                    hasDuplicate = true;
                }
                stringBuilder.AppendLine("Duplicated varieties:");
                foreach (var d in duplicatedVarieties)
                {
                    stringBuilder.AppendLine(string.Format("{0}\\{1}", d.SourceFile, d.Value.Name));
                    hasDuplicate = true;
                }
                if (hasDuplicate)
                {
                    throw new InvalidOperationException("Some files share items with the same name.\n\n" + stringBuilder);
                }*/