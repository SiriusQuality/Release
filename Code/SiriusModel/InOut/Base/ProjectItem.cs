using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace SiriusModel.InOut.Base
{
    [Serializable]
    public abstract class ProjectItem : IProjectItem
    {
        #region IProjectItem Members

        public virtual bool NotifyPropertyChanged(string propertyName)
        {
            if (!ProjectFile.IsBindingSuspended && PropertyChanged != null)
            {
                var activeForm = Form.ActiveForm;
                if (activeForm == null) { return false; }
                else
                {
                    var eventArgs = new PropertyChangedEventArgs(propertyName);

                    if (activeForm.InvokeRequired)
                    {
                        try
                        {
                            PropertyChanged.Invoke(this, eventArgs);
                        }
                        catch (Exception ee)
                        {
                            Console.WriteLine(ee.Message);
                            throw ee;

                        }
                    }
                    else
                    {
                        try
                        {
                            InvokeDelegate d = () => { try { PropertyChanged.Invoke(this, eventArgs); } catch (Exception e) { Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); } };
                            activeForm.Invoke(d);
                        }
                        catch (InvalidOperationException B)
                        {
                            System.Console.WriteLine(B.Message);
                            throw B;
                        }
                    }
                    WarningList.This.NotifyPropertyChanged(this, propertyName);
                    return true;
                }
            }
            return false;
        }


        private delegate void InvokeDelegate();

        public virtual string WarningFileID { get { return "?"; } }

        public virtual string WarningItemName { get { return "?"; } }

        public virtual void ClearWarnings()
        {
            WarningList.This.Clear(this);
        }

        public abstract void CheckWarnings();

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
