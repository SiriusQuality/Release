//#define TestClone

using System.IO;
using System.Xml.Serialization;

namespace SiriusModel.InOut.Base
{
    public static class CloneHelper
    {
        public static T Cast<T>(this object toCast) { return (toCast is T) ? (T)toCast : default(T); }

        static readonly MemoryStream memoryStream;

#if TestClone
        static double timeSerialize;
        static double timeDeserialize;
        static DateTime time;
#endif 
        static CloneHelper()
        {
            memoryStream = new MemoryStream();
#if TestClone
            timeSerialize = 0;
            timeDeserialize = 0;
#endif
        }

        public static T Clone<T>(this T toClone)
        {
#if TestClone
            time = DateTime.Now;
            timeSerialize += (DateTime.Now - time).TotalMilliseconds;
            time = DateTime.Now;
            timeDeserialize += (DateTime.Now - time).TotalMilliseconds;
#endif

            // using binary formatter produce memory leak

            memoryStream.Seek(0, SeekOrigin.Begin);
            var xmlSerializer = new XmlSerializer(toClone.GetType());
            xmlSerializer.Serialize(memoryStream, toClone);

            memoryStream.Seek(0, SeekOrigin.Begin);
            var clone = Cast<T>(xmlSerializer.Deserialize(memoryStream));
            memoryStream.SetLength(0);

            return clone;
        }
#if TestClone
        public static double TimeDeserialize
        {
            get { return timeDeserialize; }
        }

        public static double TimeSerialize
        {
            get { return timeSerialize; }
        }
#endif
    }
}
