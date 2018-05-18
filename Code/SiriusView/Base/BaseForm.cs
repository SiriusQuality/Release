using System;
using System.Collections.Generic;
using SiriusView.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace SiriusView.Base
{
    public partial class BaseForm : DockContent
    {
        private readonly static List<BaseForm> baseForms = new List<BaseForm>();
        public static IList<BaseForm> BaseForms { get { return baseForms; } }

        private static Dictionary<string, BaseFormSetting> SavedDockStates;
        private const string RowSeparatorString = "\n";
        private static readonly char[] RowSeparator = RowSeparatorString.ToCharArray();
        private const string ColumnSeparatorString = "\t";
        private static readonly char[] ColumnSeparator = ColumnSeparatorString.ToCharArray();

        public static void LoadDockStates()
        {
            SavedDockStates = new Dictionary<string, BaseFormSetting>();
            try
            {
                var saved = Settings.Default.BaseFormDockStates;
                var savedRows = saved.Split(RowSeparator, StringSplitOptions.RemoveEmptyEntries);
                foreach (var savedRow in savedRows)
                {
                    var savedCells = savedRow.Split(ColumnSeparator, StringSplitOptions.RemoveEmptyEntries);
                    var loaded = new BaseFormSetting {DockState = ((DockState) Enum.Parse(typeof (DockState), savedCells[1])), Top = int.Parse(savedCells[2]), Left = int.Parse(savedCells[3]), Width = int.Parse(savedCells[4]), Height = int.Parse(savedCells[5])};
                    SavedDockStates.Add(savedCells[0], loaded);
                }
            }
            catch
            {
                SavedDockStates.Clear();
            }
        }

        public static void SaveDockStates()
        {
            var toSave = "";
            foreach (var saved in SavedDockStates)
            {
                toSave += saved.Key + ColumnSeparatorString
                    + saved.Value.DockState + ColumnSeparatorString
                    + saved.Value.Top + ColumnSeparatorString
                    + saved.Value.Left + ColumnSeparatorString
                    + saved.Value.Width + ColumnSeparatorString
                    + saved.Value.Height + RowSeparatorString; 
            }
            Settings.Default.BaseFormDockStates = toSave;
        }

        public BaseForm()
        {
            baseForms.Add(this);
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                
                this.UpdateDockPanel(ReferenceDockPanel());
                if (!SavedDockStates.ContainsKey(BaseFormID()))
                {
                    var newSetting = new BaseFormSetting {DockState = DockState.Document};
                    SavedDockStates.Add(BaseFormID(), newSetting);
                }
                var setting = SavedDockStates[BaseFormID()];
                if (setting.DockState == DockState.Unknown) setting.DockState = DockState.Document;


                if (setting.DockState == DockState.Float)
                {
                    Top = setting.Top;
                    Left = setting.Left;
                    Width = setting.Width;
                    Height = setting.Height;
                    this.EnsureFitsInDesktop();
                }
                this.UpdateDockState(setting.DockState);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            baseForms.Remove(this);
            var savedSetting = SavedDockStates[BaseFormID()];
            savedSetting.DockState = DockState;
            savedSetting.Top = Top;
            savedSetting.Left = Left;
            savedSetting.Width = Width;
            savedSetting.Height = Height;
            base.OnClosed(e);
        }

        public virtual DockPanel ReferenceDockPanel()
        {
            return null;
        }

        public virtual string BaseFormID()
        {
            return null;
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {

        }
    }
}
