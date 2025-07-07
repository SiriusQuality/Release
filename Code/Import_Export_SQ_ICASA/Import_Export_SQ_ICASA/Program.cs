using System;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
namespace Import_Export_SQ_ICASA
{
    class Program
    {
        //DEMO
        static void Main(string[] args)
        {
            ImportDBM idbm = new ImportDBM(File.ReadAllText("./INRA.json"));
            idbm.importDBM("T");
            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}
