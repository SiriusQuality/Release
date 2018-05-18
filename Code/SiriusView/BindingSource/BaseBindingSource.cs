using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SiriusModel.InOut;

namespace SiriusView
{
    public class BaseBindingSource : BindingSource
    {
        private static readonly List<BaseBindingSource> AllBindingSources = new List<BaseBindingSource>();

        public static void SuspendAll()
        {
            ProjectFile.IsBindingSuspended = true;
            foreach (var bbs in AllBindingSources)
            {
                //bbs.SuspendBinding();
                bbs.RaiseListChangedEvents = false;
            }
            ProjectFile.This.FileContainer.ManagementFile.SuspendBinding();
            ProjectFile.This.FileContainer.NonVarietyFile.SuspendBinding();
            ProjectFile.This.FileContainer.RunOptionFile.SuspendBinding();
            ProjectFile.This.FileContainer.RunFile.SuspendBinding();
            ProjectFile.This.FileContainer.SiteFile.SuspendBinding();
            ProjectFile.This.FileContainer.SoilFile.SuspendBinding();
            ProjectFile.This.FileContainer.VarietyFile.SuspendBinding();
            ProjectFile.This.FileContainer.ObservationFile.SuspendBinding();/////////////
            ProjectFile.This.FileContainer.OptimizationFile.SuspendBinding();//////////
        }

        public static void ResumeAll()
        {
            ProjectFile.IsBindingSuspended = false;

            ProjectFile.This.FileContainer.ManagementFile.ResumeBinding();
            ProjectFile.This.FileContainer.NonVarietyFile.ResumeBinding();
            ProjectFile.This.FileContainer.RunOptionFile.ResumeBinding();
            ProjectFile.This.FileContainer.RunFile.ResumeBinding();
            ProjectFile.This.FileContainer.SiteFile.ResumeBinding();
            ProjectFile.This.FileContainer.SoilFile.ResumeBinding();
            ProjectFile.This.FileContainer.VarietyFile.ResumeBinding();
            ProjectFile.This.FileContainer.ObservationFile.ResumeBinding();//////////////
            ProjectFile.This.FileContainer.OptimizationFile.ResumeBinding();/////////////

            foreach (var bbs in AllBindingSources)
            {
                bbs.RaiseListChangedEvents = true;
                bbs.ResetBindings(false);
                //bbs.ResumeBinding();
            }
        }

        public BaseBindingSource(IContainer container)
            : base(container)
        {
            AllBindingSources.Add(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (AllBindingSources.Contains(this))
            {
                AllBindingSources.Remove(this);
            }
            base.Dispose(disposing);
        }
    }
}
