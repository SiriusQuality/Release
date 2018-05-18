using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SiriusModel.InOut.Base
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValueAttribute : Attribute
    {
        private readonly string name;
        private readonly string unit;
        public double OutputFactor { get; set; }

        public ValueAttribute(string name, string unit)
        {
            OutputFactor = 1;
            this.name = name;
            this.unit = unit;
        }

        public ValueAttribute(string name)
        {
            this.name = name;
            unit = null;
        }

        public string Title
        {
            get
            {
                return name + (unit != null ? " (" + unit + ")" : null);
            }
        }
    }

    public static class ValueAttributeHelper
    {
        public static object [] PrintHeader(this object o, string dataType)
        {
            object[] line = Print.CreateLine();
            line = line.Add(dataType);
            PropertyInfo[] properties = o.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                var valueAttribute = (ValueAttribute)propertyInfo.GetCustomAttributes(typeof (ValueAttribute), true).FirstOrDefault();
                if (valueAttribute != null)
                {
                    line = line.Add(valueAttribute.Title);
                }
            }
            return line;
        }

        public static object[] PrintValue(this object o, string dataName)
        {
            object[] line = Print.CreateLine();
            line = line.Add(dataName);
            PropertyInfo[] properties = o.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                var valueAttribute = (ValueAttribute)propertyInfo.GetCustomAttributes(typeof(ValueAttribute), true).FirstOrDefault();
                if (valueAttribute != null)
                {
                    line = line.Add(GetValue(propertyInfo.GetValue(o, null), valueAttribute));
                }
            }
            return line;
        }



        public static object[] PrintHeader<T>(this IList<T> l)
        {
            object[] line = Print.CreateLine();

            if (l.Count > 0)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                object[] lineT = Print.CreateLine();
                foreach (PropertyInfo propertyInfo in properties)
                {
                    var valueAttribute = (ValueAttribute)propertyInfo.GetCustomAttributes(typeof(ValueAttribute), true).FirstOrDefault();
                    if (valueAttribute != null)
                    {
                        lineT = lineT.Add(valueAttribute.Title);
                    }
                }

                for (int i = 0; i < l.Count; ++i)
                {
                    line = line.Add(lineT);
                }
            }
            return line;
        }

        public static object[] PrintValue<T>(this IList<T> l)
        {
            object[] line = Print.CreateLine();

            if (l.Count > 0)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();

                for (int i = 0; i < l.Count; ++i)
                {
                    object[] lineT = Print.CreateLine();
                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        var valueAttribute = (ValueAttribute)propertyInfo.GetCustomAttributes(typeof(ValueAttribute), true).FirstOrDefault();
                        if (valueAttribute != null)
                        {
                            lineT = lineT.Add(GetValue(propertyInfo.GetValue(l[i], null), valueAttribute));
                        }
                    }

                    line = line.Add(lineT);
                }
            }
            return line;
        }

        private static object GetValue(object o, ValueAttribute va)
        {
            if (va.OutputFactor != 1)
            {
                if (!(o is DateTime) && !(o.GetType().IsEnum)) return o;
                if (o is DateTime) return ((DateTime)o).ToString("dd/MM/yyyy");
                return o.ToString();
            }
            return ((double) o)*va.OutputFactor;
        }
    }
}
