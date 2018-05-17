using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SiriusModel.InOut;
using SiriusOptimization.Base;
using SiriusView.File;

namespace SiriusView
{
    public partial class WorkerControl : StatusStrip
    {
        private const string ReadyText = "Ready";
        private readonly ToolStripProgressBar progress;
        private volatile bool _shouldStop;
        private readonly ToolStripLabel state;
        private readonly ToolStripLabel timeSpent;
        private readonly BackgroundWorker worker;
        public BackgroundWorker Worker
        {
            get { return worker; }
        }

        private readonly Queue<WorkArgument> works;
        private DateTime chronoStart;

        public ToolStripLabel State
        {
            get { return state; }
            private set { State = value; }
        }

        public WorkerControl()
        {
            InitializeComponent();
            state = new ToolStripLabel();
            timeSpent = new ToolStripLabel("done (0s 00ms)") {Alignment = ToolStripItemAlignment.Right};
            progress = new ToolStripProgressBar {Alignment = ToolStripItemAlignment.Right, Maximum = 100, Minimum = 0, Step = 1};
            progress.UpdateVisible(false);
            state.UpdateText(ReadyText);

            works = new Queue<WorkArgument>();
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            
            worker.DoWork += WorkerDoWork;
            worker.ProgressChanged += WorkerProgressChanged;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
            AddToolStripItems();
            _shouldStop = false;
        }

        private void AddToolStripItems()
        {
            Items.Add(state);
            Items.Add(progress);
            Items.Add(timeSpent);
        }

        void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!progress.IsDisposed)
            {
                progress.UpdateValue(e.ProgressPercentage);
            }

        }

        void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BaseBindingSource.ResumeAll();
            if (!Disposing)
            {
                var timeSpan = DateTime.Now - chronoStart;
                timeSpent.UpdateText("done (" + (int)timeSpan.TotalSeconds + "s " + timeSpan.Milliseconds + "ms)");
                MainForm.This.Activate();
            }
            if (works.Count == 0)
            {
                if (!Disposing && progress != null && !progress.IsDisposed) progress.UpdateVisible(false);
                if (!Disposing && state != null && !state.IsDisposed) state.UpdateText(ReadyText);
            }
            else
            {
                StartWork(works.Dequeue());
            }
        }

        public void LoadProjectDataFile(string inputFileID, string fileName)
        {
            EnqueueWork(new ProjectDataFileArgument(FileMode.Load, inputFileID, fileName));
        }

        public void SaveProjectDataFile(string inputFileID, string fileName)
        {
            EnqueueWork(new ProjectDataFileArgument(FileMode.Save, inputFileID, fileName));
        }

        public void NewProjectDataFile(string inputFileID)
        {
            EnqueueWork(new ProjectDataFileArgument(FileMode.New, inputFileID, ""));
        }

        public void LoadProjectFile(string fileName)
        {
            EnqueueWork(new ProjectFileArgument(FileMode.Load, fileName));
        }

        public void SaveProjectFile(string fileName)
        {
            EnqueueWork(new ProjectFileArgument(FileMode.Save, fileName));
        }

        public void NewProjectFile()
        {
            EnqueueWork(new ProjectFileArgument(FileMode.New, ""));
        }

        public void Run(RunMode runMode, string runName)
        {
            EnqueueWork(new RunArgument(runMode, runName));
        }

        public void StartOpti(OptiMode optiMode, string optiName)
        {
            EnqueueWork(new OptiArgument(optiMode, optiName));
        }

        public void stopButton()
        {
            worker.CancelAsync();
        }

        void EnqueueWork(WorkArgument workArgument)
        {
            if (worker.IsBusy)
            {
                works.Enqueue(workArgument);
            }
            else StartWork(workArgument);
        }
        public void RequestStop()
        {
            _shouldStop = true;
        }

        void StartWork(WorkArgument workArgument)
        {
            if (!Disposing && state != null && !state.IsDisposed) state.UpdateText(workArgument.WorkName);
            if (!Disposing && progress != null && !progress.IsDisposed) progress.UpdateVisible(true);
            BaseBindingSource.SuspendAll();
            worker.RunWorkerAsync(workArgument);
        }

        void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            if (_shouldStop) return;

            chronoStart = DateTime.Now;
            var workArgument = e.Argument as WorkArgument;
            if (workArgument == null) 
            {
                throw new Exception("Background thread receive wrong argument type");
            }
  
            try
            {
                var inputFileArg = workArgument as ProjectDataFileArgument;
                if (inputFileArg != null)
                {
                    worker.ReportProgress(10);
                    switch (inputFileArg.FileMode)
                    {
                        case FileMode.Load: ProjectFile.This.FileContainer.Load(inputFileArg.FileID, inputFileArg.FileName); break;
                        case FileMode.Save: ProjectFile.This.FileContainer.Save(inputFileArg.FileID, inputFileArg.FileName); break;
                        case FileMode.New: ProjectFile.This.FileContainer.New(inputFileArg.FileID); break;

                    }
                    worker.ReportProgress(90);
                }
                else
                {
                    var projectFileArg = workArgument as ProjectFileArgument;
                    if (projectFileArg != null)
                    {
                        worker.ReportProgress(10);
                        switch (projectFileArg.FileMode)
                        {
                            case FileMode.Load: ProjectFile.Load(projectFileArg.FileName); break;
                            case FileMode.Save: ProjectFile.Save(projectFileArg.FileName); break;
                            case FileMode.New: ProjectFile.New(); break;
                        }
                        worker.ReportProgress(90);
                    }
                    else
                    {
                        var runArgument = workArgument as RunArgument;
                        if (runArgument != null)
                        {
                            IRunItemModeContainer runContainer;
                            var runMode = runArgument.RunMode;
                            switch (runMode)
                            {
                                case RunMode.NormalRun:
                                case RunMode.NormalRunAndExport:
                                    runContainer = ProjectFile.This.FileContainer.RunFile[runArgument.RunName].Normal;
                                    break;

                                case RunMode.MultiRun:
                                case RunMode.MultiRunAndExport:
                                    runContainer = ProjectFile.This.FileContainer.RunFile[runArgument.RunName].Multi;
                                    break;

                                case RunMode.SensitivityRun:
                                case RunMode.SensitivityRunAndExport:
                                    runContainer = ProjectFile.This.FileContainer.RunFile[runArgument.RunName].Sensitivity;
                                    break;
                                default: throw new ArgumentOutOfRangeException("e", "Run mode is unknown.");
                            }
                            var serialize = false;

                            switch (runMode)
                            {
                                case RunMode.NormalRunAndExport:
                                case RunMode.MultiRunAndExport:
                                case RunMode.SensitivityRunAndExport: serialize = true; break;
                            }

                            worker.ReportProgress(0);
                            var maxStep = runContainer.InitRun(serialize);
                            var maxProgress = maxStep + 2;
                            worker.ReportProgress((int)(1 / ((double)maxProgress) * 100));
                            for (var i = 0; i < maxStep; ++i)
                            {
                                runContainer.StepRun(serialize, i);
                                worker.ReportProgress((int)(((double)i + 2) / maxProgress * 100));
                            }
                            runContainer.EndRun(serialize);
                            worker.ReportProgress(100);
                        }
                        else
                        {
                            var optiArgument = workArgument as OptiArgument;
                            if (optiArgument != null)
                            {
                                OptimizationProblem problem = new OptimizationProblem(ProjectFile.This.FileContainer.OptimizationFile[optiArgument.OptiName]);

                                worker.ReportProgress(0);   

                                for (int i = 0; i < problem.algorithm.NbOfRounds(); i++)
                                {
                                    var j = 0;
                                    var finish = false;

                                    if (!worker.CancellationPending)
                                        problem.Init();

                                    while (!finish)
                                    {
                                        j++;
                                        // finish if fitness > StopFitness or nbGeneration = MaxNbOfGeneration
                                        finish = worker.CancellationPending || problem.Step(j) || j == problem.algorithm.NbOfGeneration();
                                        
                                        // Compute the progression of the optimisation
                                        var progression = i*problem.algorithm.NbOfGeneration() + j;
                                        worker.ReportProgress(progression * 100 / (problem.algorithm.NbOfRounds() * problem.algorithm.NbOfGeneration()));
                                    }

                                    worker.ReportProgress((i+1) * 100 / problem.algorithm.NbOfRounds());

                                    Console.WriteLine("nb iter : " + j);
                                    problem.End();
                                }
    
                                worker.ReportProgress(100);
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                ex.Show("Error: " + workArgument.WorkName);
            }
        }

        #region Nested type: FileArgument

        private abstract class FileArgument : WorkArgument
        {
            protected FileArgument(FileMode fileMode, string fileName)
            {
                FileMode = fileMode;
                FileName = fileName;
            }

            public FileMode FileMode { get; private set; }
            public string FileName { get; private set; }
        }

        #endregion

        #region Nested type: FileMode

        private enum FileMode
        {
            Load,
            Save, 
            New
        }

        #endregion

        #region Nested type: ProjectDataFileArgument

        private class ProjectDataFileArgument : FileArgument
        {
            public ProjectDataFileArgument(FileMode fileMode, string fileID, string fileName)
                : base(fileMode, fileName)
            {
                FileID = fileID;
            }

            public string FileID { get; private set; }

            public override string WorkName
            {
                get { return ((FileMode == FileMode.Load) ? "Loading " : "Saving ") + FileID + ": " + FileName; }
            }
        }

        #endregion

        #region Nested type: ProjectFileArgument

        private class ProjectFileArgument : FileArgument
        {
            public ProjectFileArgument(FileMode fileMode, string fileName)
                : base(fileMode, fileName)
            {
            }

            public override string WorkName
            {
                get
                {
                    switch (FileMode)
                    {
                        case FileMode.Load: return "Loading project: " + FileName;
                        case FileMode.Save: return "Saving project: " + FileName;
                        case FileMode.New: return "New project";
                    }
                    return "?";
                }
            }
        }

        #endregion

        #region Nested type: RunArgument

        private class RunArgument : WorkArgument
        {
            private readonly string workName;

            public RunArgument(RunMode runMode, string runName)
            {
                RunMode = runMode;
                RunName = runName;
                switch (runMode)
                {
                    case RunMode.NormalRun : workName = "Normal running: "; break;
                    case RunMode.NormalRunAndExport: workName = "Normal running and exporting: "; break;
                    case RunMode.MultiRun: workName = "Multi running: "; break;
                    case RunMode.MultiRunAndExport: workName = "Multi running and exporting: "; break;
                    case RunMode.SensitivityRun: workName = "Sensitivity running: "; break;
                    case RunMode.SensitivityRunAndExport: workName = "Sensitivity running and exporting: "; break;
                }
                workName += runName;
            }

            public RunMode RunMode { get; private set; }
            public string RunName { get; private set; }

            public override string WorkName { get { return workName; } }

        }

        #endregion

        #region Nested type: OptiArgument

        private class OptiArgument : WorkArgument
        {
            private readonly string workName;
            public OptiMode OptiMode { get; private set; }
            public string OptiName { get; private set; }

            public OptiArgument(OptiMode optiMode, string optiName)
            {
                OptiMode = optiMode;
                OptiName = optiName;

                switch (optiMode)
                {
                    case OptiMode.NormalOpti: workName = "Classic optimization: "; break;
                }
                workName += optiName;
            }

            public override string WorkName { get { return workName; } }
        }

        #endregion

        #region Nested type: WorkArgument

        private abstract class WorkArgument
        {
            public abstract string WorkName { get; }
        }

        #endregion
    }

    public enum RunMode
    {
        NormalRun,
        NormalRunAndExport,
        MultiRun,
        MultiRunAndExport,
        SensitivityRun,
        SensitivityRunAndExport
    }

    public enum OptiMode
    {
        NormalOpti
    }
}
