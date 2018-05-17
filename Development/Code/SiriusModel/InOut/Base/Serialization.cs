using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using SiriusModel.InOut.OutputWriter;

namespace SiriusModel.InOut.Base
{
    public static class Serialization
    {
        public static void SerializeXml(object toSerialize, string fileName)
        {
            var xmlSerializer = new XmlSerializer(toSerialize.GetType());
            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                xmlSerializer.Serialize(fileStream, toSerialize);
                fileStream.Close();
            }
        }

        public static void SerializeText(PageData page, string fileName, bool isFirstYear = true)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            FileStream fileStream = null;
            if ( isFirstYear) fileStream = new FileStream(fileName, FileMode.Create);
            if (!isFirstYear) fileStream = new FileStream(fileName, FileMode.Append);

            using (fileStream)
            {
                using (var textWriter = new StreamWriter(fileStream, Encoding.Unicode))
                {
                    if (page == null)
                    {
                        textWriter.Close();
                        fileStream.Close();
                    }
                    var nbRow = page.Count;
                    if (nbRow == 0)
                    {
                        textWriter.Close();
                        fileStream.Close();
                    }
                    
                    for (var i = 0; i < nbRow; ++i)
                    {
                        var row = page[i];
                        if (row == null)
                        {
                            textWriter.WriteLine();
                            continue;
                        }
                        var nbColumn = row.Count;
                        if (nbColumn == 0) 
						{
							textWriter.WriteLine();
							continue;
						}
                        for (var j = 0; j < nbColumn - 1; ++j)
                        {
                            var cell = row[j];
                            textWriter.Write(cell ?? "");
                            textWriter.Write('\t');
                        }
                        var lastCell = row[nbColumn - 1];
                        textWriter.Write(lastCell ?? "");
                        textWriter.WriteLine();
                    }

                    textWriter.Close();
                    fileStream.Close();
                }
            }
        }

        public static object DeserializeXml(Type typeToDeserialize, string fileName)
        {
            object deserialized;
            var xmlSerializer = new XmlSerializer(typeToDeserialize);
            
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                deserialized = xmlSerializer.Deserialize(fileStream);
                fileStream.Close();
            }
            return deserialized;
        }


        

///<summary>
///Deserialization of a generic type
///</summary>
///<typeparam name="T"></typeparam>
///<param name="fuleFullPath"></param>
///<returns></returns>
        public static T DeserializeToObj<T>(string fuleFullPath)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var reader = new FileStream(fuleFullPath, FileMode.Open))
            {
                return (T)xmlSerializer.Deserialize(reader);
            }
        }

    }
}
