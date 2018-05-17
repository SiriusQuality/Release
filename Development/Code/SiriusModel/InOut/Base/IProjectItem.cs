using System;
using System.ComponentModel;

namespace SiriusModel.InOut.Base
{
    public interface IProjectItem : INotifyPropertyChanged
    {
        bool NotifyPropertyChanged(string propertyName);

        string WarningFileID { get; }
        string WarningItemName { get; }
        void ClearWarnings();
        void CheckWarnings();
    }

    public static class ProjectItemHelper
    {
        public static bool SetStruct<T>(this IProjectItem notifier, ref T oldValue, ref T newValue, string propertyName)
            where T : struct
        {
            try
            {
                if (!oldValue.Equals(newValue))
                {
                    oldValue = newValue;
                    notifier.NotifyPropertyChanged(propertyName);
                    return true;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("exeption :" + ee.Message);
            }
            return false;
        }

        public static bool SetStruct<T>(this IProjectItem notifier, IProjectItem container, string propertyName, ref T newValue, string propertyNotifierName)
            where T : struct
        {
            var propertyInfo = container.GetType().GetProperty(propertyName);
            var oldValue = (T)propertyInfo.GetValue(container, null);
            propertyInfo.SetValue(container, newValue, null);
            var result = notifier.SetStruct(ref oldValue, ref newValue, propertyNotifierName);
            return result;
        }

        public static bool SetObject(this IProjectItem notifier, string propertyName)
        {
            notifier.NotifyPropertyChanged(propertyName);
            return true;
        }

        public static bool SetObject<T>(this IProjectItem notifier, ref T oldValue, ref T newValue, string propertyName)
            where T : class
        {
            if (oldValue == null && newValue == null)
            {
                return false;
            }
            if (oldValue == null || (newValue == null || !oldValue.Equals(newValue)))
            {
                oldValue = newValue;
                notifier.NotifyPropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        public static bool SetObject<T>(this IProjectItem notifier, IProjectItem container, string propertyName, ref T newValue, string propertyNotifierName)
            where T : class
        {
            var propertyInfo = container.GetType().GetProperty(propertyName);
            var oldValue = (T)propertyInfo.GetValue(container, null);
            propertyInfo.SetValue(container, newValue, null);
            var result = notifier.SetObject(ref oldValue, ref newValue, propertyNotifierName);
            return result;
        }

        public static void Assert<T>(this IProjectItem notifier, T value, Func<T, bool> warningIfFalse, string propertyName, string format, object[] arguments)
        {
            WarningList.This.CheckStatus(notifier, propertyName, format, arguments, warningIfFalse.Invoke(value));
        }
    }
}
