using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;
using System.Collections;
using System.Reflection;

namespace GeneticAlgorithms
{
    public class XMLEdit
    {
        public XMLEdit()
        {
            //
            // TODO: Add construct logic.
            //
        }

        public void XMLSave(string FilePath)
        {
            XmlDocument xmlNew = new XmlDocument();
            xmlNew.Load(FilePath);
            xmlNew.Save(FilePath);
        }

        public void CopyAndPasteNode(string SourceFilePath, string DestinationFilePath, string SourceRoot, string DestinationRoot, int currentTreatmentNumber, int ExistingNodeNumber)
        {
            // Open the reader with the source XML file
            XmlTextReader myReader = new XmlTextReader(SourceFilePath);

            XmlDocument mySourceDoc = new XmlDocument();// Load the source of the XML file into an XmlDocument

            mySourceDoc.Load(myReader);// Load the source XML file into the first document

            myReader.Close();// Close the reader


            // Open the reader with the destination XML file

            myReader = new XmlTextReader(DestinationFilePath);

            XmlDocument myDestDoc = new XmlDocument();  // Load the source of the XML file into an XmlDocument

            myDestDoc.Load(myReader); // Load the destination XML file into the first document

            myReader.Close(); // Close the reader


            // Store the root node of the destination document into an XmlNode
            // The 1 in ChildNodes[1] is the index of the node to be copied (where 0 is the first node)

            XmlNode rootDest = myDestDoc.SelectSingleNode(DestinationRoot);

            XmlNode nodeOrig = mySourceDoc.SelectSingleNode(SourceRoot);            // Store the node to be copied into an XmlNode

            for (int i = 0; i < (currentTreatmentNumber - ExistingNodeNumber); i++)
            {

                XmlNode nodeDest = myDestDoc.ImportNode(nodeOrig, true);            // Store the copy of the original node into an XmlNode

                rootDest.AppendChild(nodeDest);            // Append the node being copied to the root of the destination document


                // Open the writer
                XmlTextWriter myWriter = new XmlTextWriter(DestinationFilePath, Encoding.UTF8); // Indented for easy reading

                myWriter.Formatting = Formatting.Indented;            // Write the file

                myDestDoc.WriteTo(myWriter);

                myWriter.Close();            // Close the writer
            }
        }

        public void PrintAllNodes(string FilePath, string nodeName)
        {
            XmlDocument xmlNew = new XmlDocument();
            xmlNew.Load(FilePath);

            XmlNodeList xn1 = xmlNew.SelectNodes(nodeName);

            foreach (XmlNode xnf in xn1)
            {
                XmlElement xe = (XmlElement)xnf;

                XmlNodeList xnf1 = xe.ChildNodes;

                foreach (XmlNode xn2 in xnf1)
                {
                    Console.WriteLine(xn2.InnerText);
                }
            }
        }

        public int CountNodeNumber(string FilePath, string Root)
        {
            int NumberOfNodes;

            XmlDocument xmlNew = new XmlDocument();
            xmlNew.Load(FilePath);

            XmlNodeList nl1 = xmlNew.SelectSingleNode(Root).ChildNodes;
            NumberOfNodes = nl1.Count;

            return NumberOfNodes;

            //xmlNew.Save(FilePath);
        }

        public void ChangeRunFileNodeValue(string FilePath, string runFileRoot, string[][] runItems, ArrayList currentYears, ArrayList currentSites, ArrayList currentNitrogenTreatments, ArrayList currentException)
        {
            string[] managementItems=runItems[0];
            string[] siteItems=runItems[1];
            string[] soilItems=runItems[2];
            string[] varieyItems = runItems[3];

            XmlDocument NewXMLRun = new XmlDocument();
            NewXMLRun.Load(FilePath);

            XmlNodeList NodeList1 = NewXMLRun.SelectSingleNode(runFileRoot).ChildNodes;
            int numberOfNodes1 = NodeList1.Count;

            int currentNodeNumber;
            int ExceptionLine;
            int ExceptionColumn;

            string[,] Exception = (string[,])currentException[0];

            if (Exception.GetLength(1) == 0) //Get the culumn number of tempException.
            {
                ExceptionLine = Exception.GetLength(0) - 1;
                ExceptionColumn = Exception.GetLength(1);//Get the line and column number of Exceptiopn, when there is no Exception.
            }
            else
            {
                ExceptionLine = Exception.GetLength(0);
                ExceptionColumn = Exception.GetLength(1);//Get the line and column number of Exceptiopn, when there are Exceptions.
            }
            //To test whether Exception is exsiting.

            if (ExceptionColumn == 0)
            {
                #region 1. Change the run file node value when there is no exception

                currentNodeNumber = 0;

                for (int nitrogen = 0; nitrogen < currentNitrogenTreatments.Count; nitrogen++)
                {
                    for (int site = 0; site < currentSites.Count; site++)
                    {
                        for (int year = 0; year < currentYears.Count; year++)
                        {

                            string currentSite = (string)currentSites[site];
                            string currentYear = (string)currentYears[year];
                            string currentNitrogenTreatment = (string)currentNitrogenTreatments[nitrogen];

                            XmlElement xe1 = (XmlElement)NodeList1[currentNodeNumber]; //Convert XML node to XML element.
                            string runName = xe1.GetAttribute("name");
                            //Console.WriteLine(runName);

                            #region Set the treatment name
                            if (currentSite == "CF" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe1.SetAttribute("name", "CF_07_HN");
                            if (currentSite == "CF" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe1.SetAttribute("name", "CF_07_LN");
                            if (currentSite == "CF" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe1.SetAttribute("name", "CF_08_HN");
                            if (currentSite == "CF" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe1.SetAttribute("name", "CF_08_LN");

                            if (currentSite == "JIC" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe1.SetAttribute("name", "JIC_07_HN");
                            if (currentSite == "JIC" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe1.SetAttribute("name", "JIC_07_LN");
                            if (currentSite == "JIC" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe1.SetAttribute("name", "JIC_08_HN");
                            if (currentSite == "JIC" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe1.SetAttribute("name", "JIC_08_LN");

                            if (currentSite == "MONS" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe1.SetAttribute("name", "MONS_07_HN");
                            if (currentSite == "MONS" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe1.SetAttribute("name", "MONS_07_LN");
                            if (currentSite == "MONS" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe1.SetAttribute("name", "MONS_08_HN");
                            if (currentSite == "MONS" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe1.SetAttribute("name", "MONS_08_LN");

                            if (currentSite == "UNOT" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe1.SetAttribute("name", "UNOT_07_HN");
                            if (currentSite == "UNOT" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe1.SetAttribute("name", "UNOT_07_LN");
                            if (currentSite == "UNOT" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe1.SetAttribute("name", "UNOT_08_HN");
                            if (currentSite == "UNOT" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe1.SetAttribute("name", "UNOT_08_LN");
                            #endregion

                            XmlNodeList Nodelist2 = xe1.ChildNodes;//Get the child nodes of xe1.
                            int NumberOfNodes2 = Nodelist2.Count;

                            for (int j = 0; j < NumberOfNodes2; j++)
                            {
                                XmlElement xe2 = (XmlElement)Nodelist2[j]; //Convert XML node to XML element.
                                string runItemName = xe2.Name;

                                switch (runItemName)
                                {
                                    #region 1. Change comments
                                    case "Comments":

                                        break;
                                    #endregion

                                    #region 2. Change normal run items
                                    case "Normal":

                                        XmlNodeList Nodelist3_1 = xe2.ChildNodes;//Get the child nodes of xe1.
                                        int NumberOfNodes3_1 = Nodelist3_1.Count;
                                        //Console.WriteLine(NumberOfNodes31);

                                        for (int k = 0; k < NumberOfNodes3_1; k++)
                                        {
                                            XmlElement xe3 = (XmlElement)Nodelist3_1[k];
                                            string normalItemName = xe3.Name;
                                            //Console.WriteLine(normalItemName);

                                            #region 2.1. Management item
                                            if (normalItemName == "ManagementItem")
                                            {
                                                if (currentSite == "CF" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe3.InnerText = managementItems[0];
                                                if (currentSite == "CF" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe3.InnerText = managementItems[1];
                                                if (currentSite == "CF" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe3.InnerText = managementItems[2];
                                                if (currentSite == "CF" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe3.InnerText = managementItems[3];

                                                if (currentSite == "JIC" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe3.InnerText = managementItems[4];
                                                if (currentSite == "JIC" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe3.InnerText = managementItems[5];
                                                if (currentSite == "JIC" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe3.InnerText = managementItems[6];
                                                if (currentSite == "JIC" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe3.InnerText = managementItems[7];

                                                if (currentSite == "MONS" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe3.InnerText = managementItems[8];
                                                if (currentSite == "MONS" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe3.InnerText = managementItems[9];
                                                if (currentSite == "MONS" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe3.InnerText = managementItems[10];
                                                if (currentSite == "MONS" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe3.InnerText = managementItems[11];

                                                if (currentSite == "UNOT" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe3.InnerText = managementItems[12];
                                                if (currentSite == "UNOT" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe3.InnerText = managementItems[13];
                                                if (currentSite == "UNOT" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe3.InnerText = managementItems[14];
                                                if (currentSite == "UNOT" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe3.InnerText = managementItems[15];
                                            }
                                            #endregion

                                            #region 2.2. Parameter item
                                            if (normalItemName == "ParameterItem")
                                            {
                                                xe3.InnerText = varieyItems[0];
                                            }
                                            #endregion

                                            #region 2.3. Site item
                                            if (normalItemName == "SiteItem")
                                            {
                                                if (currentSite == "CF") xe3.InnerText = siteItems[0];
                                                if (currentSite == "JIC") xe3.InnerText = siteItems[1];
                                                if (currentSite == "MONS") xe3.InnerText = siteItems[2];
                                                if (currentSite == "UNOT") xe3.InnerText = siteItems[3];
                                            }
                                            #endregion

                                            #region 2.4. Soil item
                                            if (normalItemName == "SoilItem")
                                            {
                                                if (currentSite == "CF" & currentYear == "2007") xe3.InnerText = soilItems[0];
                                                if (currentSite == "CF" & currentYear == "2008") xe3.InnerText = soilItems[1];

                                                if (currentSite == "JIC" & currentYear == "2007") xe3.InnerText = soilItems[2];
                                                if (currentSite == "JIC" & currentYear == "2008") xe3.InnerText = soilItems[3];

                                                if (currentSite == "MONS" & currentYear == "2007") xe3.InnerText = soilItems[4];
                                                if (currentSite == "MONS" & currentYear == "2008") xe3.InnerText = soilItems[5];

                                                if (currentSite == "UNOT" & currentYear == "2007") xe3.InnerText = soilItems[6];
                                                if (currentSite == "UNOT" & currentYear == "2008") xe3.InnerText = soilItems[7];
                                            }
                                            #endregion

                                            #region 2.5. Variety item
                                            if (normalItemName == "VarietyItem")
                                            {
                                                xe3.InnerText = varieyItems[0];
                                            }
                                            #endregion
                                        }

                                        break;
                                    #endregion

                                    #region 3. Change multiple run items
                                    case "Multi":

                                        XmlNodeList Nodelist3_2 = xe2.ChildNodes;//Get the child nodes of xe1.
                                        int NumberOfNodes3_2 = Nodelist3_2.Count;
                                        //Console.WriteLine(NumberOfNodes3_2);

                                        for (int k = 0; k < NumberOfNodes3_2; k++)
                                        {
                                            XmlElement xe3 = (XmlElement)Nodelist3_2[k];
                                            string multiItemName = xe3.Name;
                                            //Console.WriteLine(multiItemName);

                                            if (multiItemName == "MultiRunsArray")
                                            {
                                                XmlNodeList Nodelist3_2_1 = xe3.ChildNodes;//Get the child nodes of xe1.
                                                int NumberOfNodes3_2_1 = Nodelist3_2_1.Count;
                                                //Console.WriteLine(NumberOfNodes3_2_1);

                                                for (int l = 0; l < NumberOfNodes3_2_1; l++)
                                                {
                                                    XmlElement xe4 = (XmlElement)Nodelist3_2_1[l];
                                                    string multiRunsArrayName = xe4.Name;
                                                    //Console.WriteLine(multiRunsArrayName);

                                                    if (multiRunsArrayName == "MultiRunItem")
                                                    {
                                                        XmlNodeList Nodelist3_2_1_1 = xe4.ChildNodes;//Get the child nodes of xe1.
                                                        int NumberOfNodes3_2_1_1 = Nodelist3_2_1_1.Count;
                                                        //Console.WriteLine(NumberOfNodes3_2_1_1);

                                                        for (int m = 0; m < NumberOfNodes3_2_1_1; m++)
                                                        {
                                                            XmlElement xe5 = (XmlElement)Nodelist3_2_1_1[m];
                                                            string multiRunItemName = xe5.Name;
                                                            //Console.WriteLine(multiRunItemName);

                                                            #region 3.1. Management item

                                                            if (multiRunItemName == "ManagementItem")
                                                            {
                                                                if (currentSite == "CF" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe5.InnerText = managementItems[0];
                                                                if (currentSite == "CF" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe5.InnerText = managementItems[1];
                                                                if (currentSite == "CF" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe5.InnerText = managementItems[2];
                                                                if (currentSite == "CF" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe5.InnerText = managementItems[3];

                                                                if (currentSite == "JIC" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe5.InnerText = managementItems[4];
                                                                if (currentSite == "JIC" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe5.InnerText = managementItems[5];
                                                                if (currentSite == "JIC" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe5.InnerText = managementItems[6];
                                                                if (currentSite == "JIC" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe5.InnerText = managementItems[7];

                                                                if (currentSite == "MONS" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe5.InnerText = managementItems[8];
                                                                if (currentSite == "MONS" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe5.InnerText = managementItems[9];
                                                                if (currentSite == "MONS" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe5.InnerText = managementItems[10];
                                                                if (currentSite == "MONS" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe5.InnerText = managementItems[11];

                                                                if (currentSite == "UNOT" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe5.InnerText = managementItems[12];
                                                                if (currentSite == "UNOT" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe5.InnerText = managementItems[13];
                                                                if (currentSite == "UNOT" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe5.InnerText = managementItems[14];
                                                                if (currentSite == "UNOT" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe5.InnerText = managementItems[15];
                                                            }
                                                            #endregion

                                                            #region 3.2. Parameter item
                                                            if (multiRunItemName == "ParameterItem")
                                                            {
                                                                xe5.InnerText = varieyItems[0];
                                                            }
                                                            #endregion

                                                            #region 3.3. Site item
                                                            if (multiRunItemName == "SiteItem")
                                                            {
                                                                if (currentSite == "CF") xe5.InnerText = siteItems[0];
                                                                if (currentSite == "JIC") xe5.InnerText = siteItems[1];
                                                                if (currentSite == "MONS") xe5.InnerText = siteItems[2];
                                                                if (currentSite == "UNOT") xe5.InnerText = siteItems[3];
                                                            }
                                                            #endregion

                                                            #region 3.4. Soil item
                                                            if (multiRunItemName == "SoilItem")
                                                            {
                                                                if (currentSite == "CF" & currentYear == "2007") xe5.InnerText = soilItems[0];
                                                                if (currentSite == "CF" & currentYear == "2008") xe5.InnerText = soilItems[1];

                                                                if (currentSite == "JIC" & currentYear == "2007") xe5.InnerText = soilItems[2];
                                                                if (currentSite == "JIC" & currentYear == "2008") xe5.InnerText = soilItems[3];

                                                                if (currentSite == "MONS" & currentYear == "2007") xe5.InnerText = soilItems[4];
                                                                if (currentSite == "MONS" & currentYear == "2008") xe5.InnerText = soilItems[5];

                                                                if (currentSite == "UNOT" & currentYear == "2007") xe5.InnerText = soilItems[6];
                                                                if (currentSite == "UNOT" & currentYear == "2008") xe5.InnerText = soilItems[7];
                                                            }
                                                            #endregion

                                                            #region 3.5. Variety item
                                                            if (multiRunItemName == "VarietyItem")
                                                            {
                                                                xe5.InnerText = varieyItems[0];
                                                            }
                                                            #endregion
                                                        }
                                                    }
                                                }

                                            }
                                        }

                                        //Console.WriteLine(" ");

                                        break;
                                    #endregion

                                    #region 4. Change sesitivity run items
                                    case "Sensitivity":

                                        break;
                                    #endregion
                                }
                            }

                            currentNodeNumber++;
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region 2. Change the run file node value when there is an exception

                currentNodeNumber = 0;

                for (int nitrogen = 0; nitrogen < currentNitrogenTreatments.Count; nitrogen++)
                {
                    for (int site = 0; site < currentSites.Count; site++)
                    {
                        for (int year = 0; year < currentYears.Count; year++)
                        {
                            bool ExceptionIndicator = false;

                            string currentSite = (string)currentSites[site];
                            string currentYear = (string)currentYears[year];
                            string currentNitrogenTreatment = (string)currentNitrogenTreatments[nitrogen];

                            for (int line = 0; line < ExceptionLine; line++)
                            {
                                string currentExceptionYear = Exception[line, 0];
                                string currentExceptionSite = Exception[line, 1];
                                string currentNitrogenExceptionTreatment = Exception[line, 2];

                                if (currentExceptionSite == currentSite & currentExceptionYear == currentYear & currentNitrogenExceptionTreatment == currentNitrogenTreatment)
                                {
                                    ExceptionIndicator = true;  //The indicator to show that the Exception is existing and should be removed.
                                }
                            }

                            if (ExceptionIndicator == true)
                            {
                                continue;//Get rid of the exception treatments.
                            }
                            else
                            {
                                XmlElement xe1 = (XmlElement)NodeList1[currentNodeNumber]; //Convert XML node to XML element.
                                string runName = xe1.GetAttribute("name");

                                #region Set the treatment name
                                if (currentSite == "CF" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe1.SetAttribute("name", "CF_07_HN");
                                if (currentSite == "CF" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe1.SetAttribute("name", "CF_07_LN");
                                if (currentSite == "CF" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe1.SetAttribute("name", "CF_08_HN");
                                if (currentSite == "CF" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe1.SetAttribute("name", "CF_08_LN");

                                if (currentSite == "JIC" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe1.SetAttribute("name", "JIC_07_HN");
                                if (currentSite == "JIC" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe1.SetAttribute("name", "JIC_07_LN");
                                if (currentSite == "JIC" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe1.SetAttribute("name", "JIC_08_HN");
                                if (currentSite == "JIC" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe1.SetAttribute("name", "JIC_08_LN");

                                if (currentSite == "MONS" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe1.SetAttribute("name", "MONS_07_HN");
                                if (currentSite == "MONS" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe1.SetAttribute("name", "MONS_07_LN");
                                if (currentSite == "MONS" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe1.SetAttribute("name", "MONS_08_HN");
                                if (currentSite == "MONS" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe1.SetAttribute("name", "MONS_08_LN");

                                if (currentSite == "UNOT" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe1.SetAttribute("name", "UNOT_07_HN");
                                if (currentSite == "UNOT" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe1.SetAttribute("name", "UNOT_07_LN");
                                if (currentSite == "UNOT" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe1.SetAttribute("name", "UNOT_08_HN");
                                if (currentSite == "UNOT" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe1.SetAttribute("name", "UNOT_08_LN");
                                #endregion

                                XmlNodeList Nodelist2 = xe1.ChildNodes;//Get the child nodes of xe1.
                                int NumberOfNodes2 = Nodelist2.Count;

                                for (int j = 0; j < NumberOfNodes2; j++)
                                {
                                    XmlElement xe2 = (XmlElement)Nodelist2[j]; //Convert XML node to XML element.
                                    string runItemName = xe2.Name;

                                    switch (runItemName)
                                    {
                                        #region 1. Change comments
                                        case "Comments":

                                            break;
                                        #endregion

                                        #region 2. Change normal run items
                                        case "Normal":

                                            XmlNodeList Nodelist3_1 = xe2.ChildNodes;//Get the child nodes of xe1.
                                            int NumberOfNodes3_1 = Nodelist3_1.Count;
                                            //Console.WriteLine(NumberOfNodes31);

                                            for (int k = 0; k < NumberOfNodes3_1; k++)
                                            {
                                                XmlElement xe3 = (XmlElement)Nodelist3_1[k];
                                                string normalItemName = xe3.Name;
                                                //Console.WriteLine(normalItemName);

                                                #region 2.1. Management item
                                                if (normalItemName == "ManagementItem")
                                                {
                                                    if (currentSite == "CF" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe3.InnerText = managementItems[0];
                                                    if (currentSite == "CF" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe3.InnerText = managementItems[1];
                                                    if (currentSite == "CF" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe3.InnerText = managementItems[2];
                                                    if (currentSite == "CF" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe3.InnerText = managementItems[3];

                                                    if (currentSite == "JIC" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe3.InnerText = managementItems[4];
                                                    if (currentSite == "JIC" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe3.InnerText = managementItems[5];
                                                    if (currentSite == "JIC" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe3.InnerText = managementItems[6];
                                                    if (currentSite == "JIC" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe3.InnerText = managementItems[7];

                                                    if (currentSite == "MONS" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe3.InnerText = managementItems[8];
                                                    if (currentSite == "MONS" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe3.InnerText = managementItems[9];
                                                    if (currentSite == "MONS" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe3.InnerText = managementItems[10];
                                                    if (currentSite == "MONS" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe3.InnerText = managementItems[11];

                                                    if (currentSite == "UNOT" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe3.InnerText = managementItems[12];
                                                    if (currentSite == "UNOT" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe3.InnerText = managementItems[13];
                                                    if (currentSite == "UNOT" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe3.InnerText = managementItems[14];
                                                    if (currentSite == "UNOT" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe3.InnerText = managementItems[15];
                                                }
                                                #endregion

                                                #region 2.2. Parameter item
                                                if (normalItemName == "ParameterItem")
                                                {
                                                    xe3.InnerText = varieyItems[0];
                                                }
                                                #endregion

                                                #region 2.3. Site item
                                                if (normalItemName == "SiteItem")
                                                {
                                                    if (currentSite == "CF") xe3.InnerText = siteItems[0];
                                                    if (currentSite == "JIC") xe3.InnerText = siteItems[1];
                                                    if (currentSite == "MONS") xe3.InnerText = siteItems[2];
                                                    if (currentSite == "UNOT") xe3.InnerText = siteItems[3];
                                                }
                                                #endregion

                                                #region 2.4. Soil item
                                                if (normalItemName == "SoilItem")
                                                {
                                                    if (currentSite == "CF" & currentYear == "2007") xe3.InnerText = soilItems[0];
                                                    if (currentSite == "CF" & currentYear == "2008") xe3.InnerText = soilItems[1];

                                                    if (currentSite == "JIC" & currentYear == "2007") xe3.InnerText = soilItems[2];
                                                    if (currentSite == "JIC" & currentYear == "2008") xe3.InnerText = soilItems[3];

                                                    if (currentSite == "MONS" & currentYear == "2007") xe3.InnerText = soilItems[4];
                                                    if (currentSite == "MONS" & currentYear == "2008") xe3.InnerText = soilItems[5];

                                                    if (currentSite == "UNOT" & currentYear == "2007") xe3.InnerText = soilItems[6];
                                                    if (currentSite == "UNOT" & currentYear == "2008") xe3.InnerText = soilItems[7];
                                                }
                                                #endregion

                                                #region 2.5. Variety item
                                                if (normalItemName == "VarietyItem")
                                                {
                                                    xe3.InnerText = varieyItems[0];
                                                }
                                                #endregion
                                            }

                                            break;
                                        #endregion

                                        #region 3. Change multiple run items
                                        case "Multi":

                                            XmlNodeList Nodelist3_2 = xe2.ChildNodes;//Get the child nodes of xe1.
                                            int NumberOfNodes3_2 = Nodelist3_2.Count;
                                            //Console.WriteLine(NumberOfNodes3_2);

                                            for (int k = 0; k < NumberOfNodes3_2; k++)
                                            {
                                                XmlElement xe3 = (XmlElement)Nodelist3_2[k];
                                                string multiItemName = xe3.Name;
                                                //Console.WriteLine(multiItemName);

                                                if (multiItemName == "MultiRunsArray")
                                                {
                                                    XmlNodeList Nodelist3_2_1 = xe3.ChildNodes;//Get the child nodes of xe1.
                                                    int NumberOfNodes3_2_1 = Nodelist3_2_1.Count;
                                                    //Console.WriteLine(NumberOfNodes3_2_1);

                                                    for (int l = 0; l < NumberOfNodes3_2_1; l++)
                                                    {
                                                        XmlElement xe4 = (XmlElement)Nodelist3_2_1[l];
                                                        string multiRunsArrayName = xe4.Name;
                                                        //Console.WriteLine(multiRunsArrayName);

                                                        if (multiRunsArrayName == "MultiRunItem")
                                                        {
                                                            XmlNodeList Nodelist3_2_1_1 = xe4.ChildNodes;//Get the child nodes of xe1.
                                                            int NumberOfNodes3_2_1_1 = Nodelist3_2_1_1.Count;
                                                            //Console.WriteLine(NumberOfNodes3_2_1_1);

                                                            for (int m = 0; m < NumberOfNodes3_2_1_1; m++)
                                                            {
                                                                XmlElement xe5 = (XmlElement)Nodelist3_2_1_1[m];
                                                                string multiRunItemName = xe5.Name;
                                                                //Console.WriteLine(multiRunItemName);

                                                                #region 3.1. Management item

                                                                if (multiRunItemName == "ManagementItem")
                                                                {
                                                                    if (currentSite == "CF" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe5.InnerText = managementItems[0];
                                                                    if (currentSite == "CF" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe5.InnerText = managementItems[1];
                                                                    if (currentSite == "CF" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe5.InnerText = managementItems[2];
                                                                    if (currentSite == "CF" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe5.InnerText = managementItems[3];

                                                                    if (currentSite == "JIC" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe5.InnerText = managementItems[4];
                                                                    if (currentSite == "JIC" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe5.InnerText = managementItems[5];
                                                                    if (currentSite == "JIC" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe5.InnerText = managementItems[6];
                                                                    if (currentSite == "JIC" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe5.InnerText = managementItems[7];

                                                                    if (currentSite == "MONS" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe5.InnerText = managementItems[8];
                                                                    if (currentSite == "MONS" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe5.InnerText = managementItems[9];
                                                                    if (currentSite == "MONS" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe5.InnerText = managementItems[10];
                                                                    if (currentSite == "MONS" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe5.InnerText = managementItems[11];

                                                                    if (currentSite == "UNOT" & currentYear == "2007" & currentNitrogenTreatment == "HN") xe5.InnerText = managementItems[12];
                                                                    if (currentSite == "UNOT" & currentYear == "2007" & currentNitrogenTreatment == "LN") xe5.InnerText = managementItems[13];
                                                                    if (currentSite == "UNOT" & currentYear == "2008" & currentNitrogenTreatment == "HN") xe5.InnerText = managementItems[14];
                                                                    if (currentSite == "UNOT" & currentYear == "2008" & currentNitrogenTreatment == "LN") xe5.InnerText = managementItems[15];
                                                                }
                                                                #endregion

                                                                #region 3.2. Parameter item
                                                                if (multiRunItemName == "ParameterItem")
                                                                {
                                                                    xe5.InnerText = varieyItems[0];
                                                                }
                                                                #endregion

                                                                #region 3.3. Site item
                                                                if (multiRunItemName == "SiteItem")
                                                                {
                                                                    if (currentSite == "CF") xe5.InnerText = siteItems[0];
                                                                    if (currentSite == "JIC") xe5.InnerText = siteItems[1];
                                                                    if (currentSite == "MONS") xe5.InnerText = siteItems[2];
                                                                    if (currentSite == "UNOT") xe5.InnerText = siteItems[3];
                                                                }
                                                                #endregion

                                                                #region 3.4. Soil item
                                                                if (multiRunItemName == "SoilItem")
                                                                {
                                                                    if (currentSite == "CF" & currentYear == "2007") xe5.InnerText = soilItems[0];
                                                                    if (currentSite == "CF" & currentYear == "2008") xe5.InnerText = soilItems[1];

                                                                    if (currentSite == "JIC" & currentYear == "2007") xe5.InnerText = soilItems[2];
                                                                    if (currentSite == "JIC" & currentYear == "2008") xe5.InnerText = soilItems[3];

                                                                    if (currentSite == "MONS" & currentYear == "2007") xe5.InnerText = soilItems[4];
                                                                    if (currentSite == "MONS" & currentYear == "2008") xe5.InnerText = soilItems[5];

                                                                    if (currentSite == "UNOT" & currentYear == "2007") xe5.InnerText = soilItems[6];
                                                                    if (currentSite == "UNOT" & currentYear == "2008") xe5.InnerText = soilItems[7];
                                                                }
                                                                #endregion

                                                                #region 3.5. Variety item
                                                                if (multiRunItemName == "VarietyItem")
                                                                {
                                                                    xe5.InnerText = varieyItems[0];
                                                                }
                                                                #endregion
                                                            }
                                                        }
                                                    }

                                                }
                                            }

                                            //Console.WriteLine(" ");

                                            break;
                                        #endregion

                                        #region 4. Change sesitivity run items
                                        case "Sensitivity":

                                            break;
                                        #endregion
                                    }
                                }

                                currentNodeNumber++;
                            }
                        }
                    }
                }
                #endregion
            }

            NewXMLRun.Save(FilePath);
        }

        public void ChangeProjectFileNodeValue(string FilePath, string Root, string[] projectFileItems)
        {
            XmlDocument NewCultivar = new XmlDocument();
            NewCultivar.Load(FilePath);

            XmlNodeList NodeList1 = NewCultivar.SelectSingleNode(Root).ChildNodes;
            int NumberOfNodes1 = NodeList1.Count;

            for (int i = 0; i < NumberOfNodes1; i++)
            {
                XmlElement xe1 = (XmlElement)NodeList1[i]; //Convert XML node to XML element.

                XmlNodeList Nodelist2 = xe1.ChildNodes;//Get the child nodes of xe1.

                string ProjectItemNames = xe1.Name;

                switch (ProjectItemNames)
                {

                    case "ParameterFileName":
                        xe1.InnerText = projectFileItems[0];
                        break;
                    case "VarietyFileName":
                        xe1.InnerText = projectFileItems[1];
                        break;
                    //case "SoilFileName":
                    //    xe1.InnerText = projectFileItems[2];
                    //    break;
                    //Soil parameters were not calibrated any more.
                    case "RunFileName":
                        xe1.InnerText = projectFileItems[2];
                        break;
                }
            }

            NewCultivar.Save(FilePath);
        }

        public void ChangeSoilFileNodeValue(string[] varieyItems, string[] parameterNames, string FilePath, string Root, ArrayList optimalParameterSet, int[] parameterSiteIndex)
        {
            string[] soilNames = { "CF07 (MG6B)", "CF08 (CR4)", "JIC07", "JIC08", "MONS07 (C4)", "MONS08 (C2)", "UNOT07", "UNOT08" };//The string array that contains the soil site names.

            XmlDocument NewSoil = new XmlDocument();
            NewSoil.Load(FilePath);

            XmlNodeList NodeList1 = NewSoil.SelectSingleNode(Root).ChildNodes;
            int NumberOfNodes1 = NodeList1.Count;
            
            for (int i = 0; i < NumberOfNodes1; i++)
            {
                XmlElement xe1 = (XmlElement)NodeList1[i]; //Convert XML node to XML element.

                string soilName = xe1.GetAttribute("name");

                for (int j = 0; j < parameterSiteIndex.Length; j++)
                {
                    int temIndex = parameterSiteIndex[j];
                    string currentSoilName = soilNames[temIndex - 1];

                    if (soilName == currentSoilName)
                    {
                        XmlNodeList NodeList2 = xe1.ChildNodes;//Get the child nodes of xe1.
                        int NumberOfNodes2 = NodeList2.Count;

                        for (int k = 0; k < NumberOfNodes2; k++)
                        {
                            XmlElement xe2 = (XmlElement)NodeList2[k]; //Convert XML node to XML element.

                            string varietyItemName = xe2.Name;

                            for (int l = 0; l < parameterNames.Length; l++)
                            {
                                string paramterName = parameterNames[l];
                                if (paramterName == varietyItemName)
                                {
                                    xe2.InnerText = Convert.ToString(optimalParameterSet[j]);
                                }
                            }
                        }
                    }
                }
           }

            NewSoil.Save(FilePath);
        }

        public void ChangeVarietyAndParameterNodeValue(string[] varieyItems, string[] parameterNames, string varietyFilePath, string parameterFilePath, string varietyRoot, string parameterRoot, ArrayList optimalParameterSet)
        {
            XmlDocument NewVariety = new XmlDocument();
            NewVariety.Load(varietyFilePath);
            XmlNodeList NodeList11 = NewVariety.SelectSingleNode(varietyRoot).ChildNodes;
            int NumberOfNodes11 = NodeList11.Count;

            XmlDocument NewParameter = new XmlDocument();
            NewParameter.Load(parameterFilePath);
            XmlNodeList NodeList12 = NewParameter.SelectSingleNode(parameterRoot).ChildNodes;
            int NumberOfNodes12 = NodeList12.Count;

            int parameterNumber = parameterNames.Length;

            for (int i = 0; i < parameterNumber; i++)
            {
                string currentParameterName = parameterNames[i];

                #region 1. Change varitety file.
                for (int j = 0; j < NumberOfNodes11; j++)
                {
                    XmlElement xe11 = (XmlElement)NodeList11[j]; //Convert XML node to XML element.
                    string varietyName = xe11.GetAttribute("name");

                    if (varietyName == varieyItems[0])
                    {
                        XmlNodeList NodeList21 = xe11.ChildNodes;//Get the child nodes of xe1.
                        int NumberOfNodes21 = NodeList21.Count;

                        for (int k = 0; k < NumberOfNodes21; k++)
                        {
                            XmlElement xe21 = (XmlElement)NodeList21[k]; //Convert XML node to XML element.

                            string varietyItemName = xe21.Name;

                            if (currentParameterName == varietyItemName)
                            {
                                xe21.InnerText = Convert.ToString(Math.Round((double)optimalParameterSet[i], 6));
                            }
                        }
                    }
                }
                #endregion

                #region 2. Change parameter file.
                for (int j = 0; j < NumberOfNodes12; j++)
                {
                    XmlElement xe12 = (XmlElement)NodeList12[j]; //Convert XML node to XML element.
                    string varietyName = xe12.GetAttribute("name");

                    if (varietyName == varieyItems[0])
                    {
                        XmlNodeList NodeList22 = xe12.ChildNodes;//Get the child nodes of xe1.
                        int NumberOfNodes22 = NodeList22.Count;

                        for (int k = 0; k < NumberOfNodes22; k++)
                        {
                            XmlElement xe22 = (XmlElement)NodeList22[k]; //Convert XML node to XML element.
                            string parameterItemName = xe22.Name;

                            if (currentParameterName == parameterItemName)
                            {
                                xe22.InnerText = Convert.ToString(Math.Round((double)optimalParameterSet[i], 6));
                            }
                        }
                    }
                }
                #endregion
            }

            NewVariety.Save(varietyFilePath);
            NewParameter.Save(parameterFilePath);
        }
    }
}
