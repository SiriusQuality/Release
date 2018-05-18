using System;
using System.IO;
using SiriusModel.InOut;
using System.Windows.Forms;
using SiriusView.File;
using System.Data;
using System.Xml;

namespace SiriusView
{
    public partial class txt2XML : Form
    {
        public txt2XML()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Stream myStream;

            openFileDialog1.InitialDirectory = ProjectFile.This.Directory;
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        // Insert code to read the stream here.
                        myStream.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            try
            {
                linkLabel1.Text = Path.GetFullPath(openFileDialog1.FileName);
            }
            catch (Exception ex)
            {
                richTextBox1.Text = "Warning: " + ex.Message;
            }
        }

        #region txt2type
        private void txt2sqman(String inputFile, String txtFile)
        {
            int numberOfColumn = 13;
            int i, j = 0;
            int errorLine = 0;

            // Création du fichier XML
            XmlTextWriter myXmlTextWriter = new XmlTextWriter(inputFile, System.Text.Encoding.UTF8);
            myXmlTextWriter.Formatting = Formatting.Indented;

            // Prologue XML
            myXmlTextWriter.WriteStartDocument();

            // Namespace XML
            myXmlTextWriter.WriteStartElement("ManagementFile");
            myXmlTextWriter.WriteAttributeString("xmlns:xsi", @"http://www.w3.org/2001/XMLSchema-instance");
            myXmlTextWriter.WriteAttributeString("xmlns:xsd", @"http://www.w3.org/2001/XMLSchema");
            myXmlTextWriter.WriteStartElement("ItemsArray");
            string Line = null;
            string currentName;
            string[] currentItems = null;

            try
            {
                StreamReader sr = new StreamReader(txtFile);

                // Lecture de la première ligne du fichier texte
                // Troncature de la ligne (Stock les éléments dans un tableau)
                Line = sr.ReadLine();
                Line = sr.ReadLine();
                currentItems = Line.Split(new char[] { '\t' });
                currentName = currentItems[0];
                numberOfColumn = currentItems.Length;
                richTextBox1.Text = "colonnes : " + numberOfColumn + "\n";
                for (int k = 0; k < numberOfColumn; ++k)
                {
                    richTextBox1.Text += currentItems[k] + " ";
                }

                // Continue à lire jusqu'à la fin du fichier
                while (currentName != "null")
                {
                    i = 0;
                    ++errorLine;
                    if (currentItems.Length != numberOfColumn)
                    {
                        errorDisplay(errorLine, numberOfColumn);
                    }

                    // Ecriture des éléments dans le fichier XML
                    // début de "ManagementItem"
                    myXmlTextWriter.WriteStartElement("ManagementItem");
                    myXmlTextWriter.WriteAttributeString("name", currentItems[i]);
                    myXmlTextWriter.WriteElementString("Comments", currentItems[++i]);
                    string Sdate = (currentItems[++i].Split(new char[] { ' ' }))[0];
                    string[] DDMMYYYY = Sdate.Split(new char[] { '/' });
                    if (DDMMYYYY.Length > 1)
                    {
                        if (DDMMYYYY[0].Length < 2) DDMMYYYY[0] = "0" + DDMMYYYY[0];
                        if (DDMMYYYY[1].Length < 2) DDMMYYYY[1] = "0" + DDMMYYYY[1];
                        Sdate = DDMMYYYY[2] + "-" + DDMMYYYY[0] + "-" + DDMMYYYY[1] + "T00:00:00";
                    }
                    myXmlTextWriter.WriteElementString("SowingDate", Sdate);
                    myXmlTextWriter.WriteElementString("SowingDensity", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("CO2", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("SoilWaterDeficit", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("TotalNi", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("TopNi", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("MidNi", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("ObservedGrainNumber", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("TargetFertileShootNumber", currentItems[++i]);
                    // début de "DateApplications"

                    myXmlTextWriter.WriteStartElement("DateApplications");
                    string ExisteDate = currentItems[i + 2];
                    if (ExisteDate != "")
                    {
                        while (currentItems[0].Equals(currentName) && currentItems[0] != "null" && ExisteDate != "")
                        {
                            j = i;
                            myXmlTextWriter.WriteStartElement("DateApplication");
                            myXmlTextWriter.WriteElementString("Nitrogen", currentItems[++j]);
                            myXmlTextWriter.WriteElementString("WaterMM", currentItems[++j]);
                            string date = currentItems[++j];
                            date = (date.Split(new char[] { ' ' }))[0];
                            DDMMYYYY = date.Split(new char[] { '/' });
                            if (DDMMYYYY.Length > 1)
                            {
                                if (DDMMYYYY[0].Length < 2) DDMMYYYY[0] = "0" + DDMMYYYY[0];
                                if (DDMMYYYY[1].Length < 2) DDMMYYYY[1] = "0" + DDMMYYYY[1];
                                date = DDMMYYYY[2] + "-" + DDMMYYYY[0] + "-" + DDMMYYYY[1] + "T00:00:00";
                            }

                            myXmlTextWriter.WriteElementString("Date", date);
                            // end of DateApplication
                            myXmlTextWriter.WriteEndElement();
                            currentItems = sortLine(sr, numberOfColumn);
                            ExisteDate = currentItems[i + 2];
                        }
                        i = j;
                        myXmlTextWriter.WriteEndElement();
                        myXmlTextWriter.WriteStartElement("GrowthStageApplications");
                        string ExisteGrowth = currentItems[i + 2];
                        if (ExisteGrowth != "")
                        {
                            while (currentItems[0].Equals(currentName) && currentItems[0] != "null" && ExisteGrowth != "")
                            {
                                j = i;
                                myXmlTextWriter.WriteStartElement("GrowthStageApplication");
                                myXmlTextWriter.WriteElementString("Nitrogen", currentItems[++j]);
                                myXmlTextWriter.WriteElementString("WaterMM", currentItems[++j]);
                                myXmlTextWriter.WriteElementString("GrowthStage", currentItems[++j]);
                                // end of GrowthStageApplication
                                myXmlTextWriter.WriteEndElement();
                                currentItems = sortLine(sr, numberOfColumn);
                                ExisteGrowth = currentItems[i + 2];
                            }
                            i = j;
                        }

                        else
                        {
                            currentItems = sortLine(sr, numberOfColumn);
                        }
                        myXmlTextWriter.WriteEndElement();
                        currentName = currentItems[0];
                    }

                    else
                    {
                        i = i + 3;
                        myXmlTextWriter.WriteEndElement();
                        myXmlTextWriter.WriteStartElement("GrowthStageApplications");
                        string ExisteGrowth = currentItems[i + 1];

                        if (ExisteGrowth != "")
                        {
                            while (currentItems[0].Equals(currentName) && currentItems[0] != "null" && ExisteGrowth != "")
                            {

                                j = i;
                                myXmlTextWriter.WriteStartElement("GrowthStageApplication");
                                myXmlTextWriter.WriteElementString("Nitrogen", currentItems[++j]);
                                myXmlTextWriter.WriteElementString("WaterMM", currentItems[++j]);
                                myXmlTextWriter.WriteElementString("GrowthStage", currentItems[++j]);
                                // end of GrowthStageApplication
                                myXmlTextWriter.WriteEndElement();
                                currentItems = sortLine(sr, numberOfColumn);
                                ExisteGrowth = currentItems[i + 2];
                            }
                            i = j;
                        }
                        else
                        {
                            currentItems = sortLine(sr, numberOfColumn);
                        }
                        myXmlTextWriter.WriteEndElement();
                        currentName = currentItems[0];
                    }


                    // fin de "ManagementItem"
                    myXmlTextWriter.WriteEndElement();

                }

                // fermeture du StreamReader
                sr.Close();
            }
            catch (Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }

            // fin de ItemsArray
            myXmlTextWriter.WriteEndElement();
            // fin de MangementFile
            myXmlTextWriter.WriteEndElement();

            myXmlTextWriter.Close();
        }
        private void txt2sqvar(String inputFile, String txtFile)
        {
            int numberOfColumn = 11;
            int i;
            int errorLine = 0;
            string Line = null;
            string currentName;
            string[] currentItems = null;

            // Création du fichier XML
            XmlTextWriter myXmlTextWriter = new XmlTextWriter(inputFile, System.Text.Encoding.UTF8);
            myXmlTextWriter.Formatting = Formatting.Indented;

            // Prologue XML
            myXmlTextWriter.WriteStartDocument();

            // Namespace XML
            myXmlTextWriter.WriteStartElement("VarietyFile");
            myXmlTextWriter.WriteAttributeString("xmlns:xsi", @"http://www.w3.org/2001/XMLSchema-instance");
            myXmlTextWriter.WriteAttributeString("xmlns:xsd", @"http://www.w3.org/2001/XMLSchema");
            myXmlTextWriter.WriteStartElement("ItemsArray");

            try
            {
                StreamReader sr = new StreamReader(txtFile);

                // Lecture de la première ligne du fichier texte
                // Troncature de la ligne (Stock les éléments dans un tableau)
                Line = sr.ReadLine();
                Line = sr.ReadLine();
                currentItems = Line.Split(new char[] { '\t' });
                currentName = currentItems[0];
                numberOfColumn = currentItems.Length;
                richTextBox1.Text = "colonnes : " + numberOfColumn + "\n";
                for (int k = 0; k < numberOfColumn; ++k)
                {
                    richTextBox1.Text += currentItems[k] + " ";
                }

                // Continue à lire jusqu'à la fin du fichier
                while (currentName != "null")
                {
                    i = 0;
                    ++errorLine;
                    if (currentItems.Length != numberOfColumn)
                    {
                        richTextBox1.Text = "Warning: @line " + errorLine + " - table should contain 21 members separated by tabs"; ;
                    }

                    // Ecriture des éléments dans le fichier XML
                    // début de "VarietyItem"
                    myXmlTextWriter.WriteStartElement("VarietyItem");
                    myXmlTextWriter.WriteAttributeString("name", currentItems[i]);
                    myXmlTextWriter.WriteElementString("Comments", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("aSheat", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("AreaSLL", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("AreaPLL", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("AreaSLL", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("RatioFLPL", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("EarGR", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("LUE", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("GrowthHabit", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("kl", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("L_IN1", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("L_EP", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("NLL", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("Phyllochron", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("PNslope", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("PNini", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("Rp", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("SDws", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("SDsa_nh", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("SDsa_sh", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("SLDL", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("TTaegf", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("TTegfm", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("TTsoem", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("VAI", currentItems[++i]);
                    //    myXmlTextWriter.WriteElementString("VBEE", currentItems[++i]);
                    // fermeture VarietyItem
                    myXmlTextWriter.WriteEndElement();

                    // lecture et troncature de la ligne suivante 
                    currentItems = sortLine(sr, numberOfColumn);

                    // mis à jour du nom
                    currentName = currentItems[0];
                }

                // fermeture du StreamReader
                sr.Close();
            }
            catch (Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }

            // fermeture ItemsArray
            myXmlTextWriter.WriteEndElement();
            // fermeture VarietyFile
            myXmlTextWriter.WriteEndElement();

            myXmlTextWriter.Close();

            richTextBox1.Text = "Caution: Created File is at V1.5 Format";
        }
        private void txt2sqpar(String inputFile, String txtFile)
        {
            int numberOfColumn = 66;
            int i;
            int errorLine = 0;
            string Line = null;
            // Création du fichier XML
            XmlTextWriter myXmlTextWriter = new XmlTextWriter(inputFile, System.Text.Encoding.UTF8);
            myXmlTextWriter.Formatting = Formatting.Indented;

            // Prologue XML
            myXmlTextWriter.WriteStartDocument();

            // Namespace XML
            myXmlTextWriter.WriteStartElement("ParameterFile");
            myXmlTextWriter.WriteAttributeString("xmlns:xsi", @"http://www.w3.org/2001/XMLSchema-instance");
            myXmlTextWriter.WriteAttributeString("xmlns:xsd", @"http://www.w3.org/2001/XMLSchema");

            myXmlTextWriter.WriteStartElement("ItemsArray");

            try
            {

                StreamReader sr = new StreamReader(txtFile);
                Line = sr.ReadLine();
                //   Line = sr.ReadLine(); // si le ficheier .txt commence par une ligne des intitulés on passe une ligne.
                // Lecture de la première ligne du fichier texte
                // Troncature de la ligne (Stock les éléments dans un tableau)
                string[] currentItems = Line.Split(new char[] { '\t' });
                String currentName = currentItems[0];

                // Continue à lire jusqu'à la fin du fichier
                while (currentName != "null")
                {
                    i = 0;
                    ++errorLine;
                    if (currentItems.Length != numberOfColumn)
                    {
                        richTextBox1.Text = "Warning: @line " + errorLine + " - table should contain " + numberOfColumn + " members separated by tabs"; ;
                    }

                    // Ecriture des éléments dans le fichier XML
                    myXmlTextWriter.WriteStartElement("ParameterItem");
                    myXmlTextWriter.WriteAttributeString("name", currentItems[i]);
                    myXmlTextWriter.WriteElementString("Comments", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("AlphaAlbGlo", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("AlphaGlu", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("AlphaKn", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("AlphaNc", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("AlphaNNI", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("AlphaSSN", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("AMNLFNO", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("AMXLFNO", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("BetaAlbGlo", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("BetaGlu", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("BetaKn", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("BetaNNI", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("BetaRWU", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("BetaSSN", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("Co2FacRue", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("DMmaxNuptake", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("MaxDL", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("FracBEAR", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("FracLaminaeBGR", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("FracSheathBGR", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("FracStemWSC", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("IntermTvern", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("LowerFPAWbiomass", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("LowerFPAWexpansion", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("LowerFPAWsenescence", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("LowerFPAWtranspiration", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("LowerVPD", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("LLOSS", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("MaxAnForP", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("MaxDSF", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("MaxLeafSoil", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("MaxLPhyll", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("MaxLeafRRNU", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("MaxRWU", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("MaxStemRRNU", currentItems[++i]); ;
                    myXmlTextWriter.WriteElementString("MaxStemN", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("MaxTvern", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("MinLPhyll", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("MinTvern", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("PhyllDecr", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("PhyllFLLAnth", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("PhyllIncr", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("PhyllGroLamina", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("PhyllGroInterNode", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("PhyllMBLL", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("PhyllMSLL", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("PhyllSBLL", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("PhyllSSLL", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("RGRStruc", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("RVER", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("SLNcri", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("SLNmax", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("SLNmin", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("SLWp", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("SlopeFR", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("SSWp", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("StdCo2", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("StrucLeafN", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("StrucStemN", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("TauSLN", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("LUETopt", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("LUETmax", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("LUETmin", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("LUETshape", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("TTcd", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("TTer", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("UpperFPAWbiomass", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("UpperFPAWexpansion", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("UpperFPAWsenescence", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("UpperFPAWtranspiration", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("UpperVPD", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("VBEE", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("MinDL", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("tbase", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("Alpha", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("tauAlpha", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("SenAccT", currentItems[++i]);// #Andrea 23/11/2015
                    myXmlTextWriter.WriteElementString("SenAccSlope", currentItems[++i]);// #Andrea 23/11/2015
                    myXmlTextWriter.WriteElementString("PreAnthesisTmin", currentItems[++i]);// #Andrea 23/11/2015
                    myXmlTextWriter.WriteElementString("PreAnthesisTopt", currentItems[++i]);// #Andrea 23/11/2015
                    myXmlTextWriter.WriteElementString("PreAnthesisTmax", currentItems[++i]);// #Andrea 23/11/2015
                    myXmlTextWriter.WriteElementString("PreAnthesisShape", currentItems[++i]);// #Andrea 23/11/2015
                    myXmlTextWriter.WriteElementString("PostAnthesisTmin", currentItems[++i]);// #Andrea 23/11/2015
                    myXmlTextWriter.WriteElementString("PostAnthesisTopt", currentItems[++i]);// #Andrea 23/11/2015
                    myXmlTextWriter.WriteElementString("PostAnthesisTmax", currentItems[++i]);// #Andrea 23/11/2015
                    myXmlTextWriter.WriteElementString("PostAnthesisShape", currentItems[++i]);// #Andrea 23/11/2015

                    // lecture et troncature de la ligne suivante 
                    currentItems = sortLine(sr, numberOfColumn);

                    // mis à jour du nom
                    currentName = currentItems[0];

                    // fermeture ParameterItem
                    myXmlTextWriter.WriteEndElement();
                }


                // fermeture du StreamReader
                sr.Close();
            }
            catch (Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }

            // fermeture ItemsArray
            myXmlTextWriter.WriteEndElement();
            // fermeture ParameterFile
            myXmlTextWriter.WriteEndElement();

            myXmlTextWriter.Close();
            richTextBox1.Text = "Caution: Created File is at V1.5 Format";
        }
        private void txt2sqsit(String inputFile, String txtFile)
        {
            int numberOfColumn;
            int i, j = 0;
            int errorLine = 0;

            // Création du fichier XML
            XmlTextWriter myXmlTextWriter = new XmlTextWriter(inputFile, System.Text.Encoding.UTF8);
            myXmlTextWriter.Formatting = Formatting.Indented;

            // Prologue XML
            myXmlTextWriter.WriteStartDocument();

            // Namespace XML
            myXmlTextWriter.WriteStartElement("SiteFile");
            myXmlTextWriter.WriteAttributeString("xmlns:xsi", @"http://www.w3.org/2001/XMLSchema-instance");
            myXmlTextWriter.WriteAttributeString("xmlns:xsd", @"http://www.w3.org/2001/XMLSchema");
            myXmlTextWriter.WriteStartElement("ItemsArray");

            string Line = null;
            string currentName;
            string[] currentItems = null;
            try
            {
                StreamReader sr = new StreamReader(txtFile);
                // Lecture de la première ligne du fichier texte
                // Troncature de la ligne (Stock les éléments dans un tableau)
                Line = sr.ReadLine();
                Line = sr.ReadLine(); // pour passer la première ligne 
                currentItems = Line.Split(new char[] { '\t' });
                currentName = currentItems[0];
                numberOfColumn = currentItems.Length;
                richTextBox1.Text = "colonnes : " + numberOfColumn + "\n";
                for (int k = 0; k < numberOfColumn; ++k)
                {
                    richTextBox1.Text += currentItems[k] + " ";
                }

                // Continue à lire jusqu'à la fin du fichier
                while (currentName != "null")
                {
                    // currentItems = Line.Split(new char[] { '\t' });
                    //  currentName = currentItems[0];
                    i = 0;
                    ++errorLine;
                    if (currentItems.Length != numberOfColumn)
                    {
                        errorDisplay(errorLine, numberOfColumn);
                    }

                    // Ecriture des éléments dans le fichier XML
                    myXmlTextWriter.WriteStartElement("SiteItem");
                    myXmlTextWriter.WriteAttributeString("name", currentItems[i]);
                    myXmlTextWriter.WriteAttributeString("format", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("Comments", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("Latitude", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("Longitude", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("Elevation", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("MeasurementHeight", currentItems[++i]);
                    myXmlTextWriter.WriteStartElement("WeatherFiles");
                    while (currentItems[0].Equals(currentName) && currentItems[0] != "null")
                    {
                        j = i;
                        myXmlTextWriter.WriteStartElement("WeatherFile");
                        myXmlTextWriter.WriteAttributeString("file", currentItems[++i]);
                        // fin de "WeatherFile"
                        myXmlTextWriter.WriteEndElement();

                        // lecture et troncature de la ligne suivante 
                        currentItems = sortLine(sr, numberOfColumn);
                    }
                    i = j;
                    currentName = currentItems[0];

                    // fin de "WeatherFiles"
                    myXmlTextWriter.WriteEndElement();
                    // fermeture de "SiteItem"
                    myXmlTextWriter.WriteEndElement();
                }

                // fermeture du StreamReader
                sr.Close();
            }
            catch (Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }

            // fermeture ItemsArray
            myXmlTextWriter.WriteEndElement();
            // fermeture SiteFile
            myXmlTextWriter.WriteEndElement();

            myXmlTextWriter.Close();
        }
        private void txt2sqsoi(String inputFile, String txtFile)
        {
            int numberOfColumn = 11;
            int i, j = 0;
            int errorLine = 0;

            // Création du fichier XML
            XmlTextWriter myXmlTextWriter = new XmlTextWriter(inputFile, System.Text.Encoding.UTF8);
            myXmlTextWriter.Formatting = Formatting.Indented;

            // Prologue XML
            myXmlTextWriter.WriteStartDocument();

            // Namespace XML
            myXmlTextWriter.WriteStartElement("SoilFile");
            myXmlTextWriter.WriteAttributeString("xmlns:xsi", @"http://www.w3.org/2001/XMLSchema-instance");
            myXmlTextWriter.WriteAttributeString("xmlns:xsd", @"http://www.w3.org/2001/XMLSchema");

            myXmlTextWriter.WriteStartElement("ItemsArray");

            try
            {
                StreamReader sr = new StreamReader(txtFile);

                // Lecture de la première ligne du fichier texte
                // Troncature de la ligne (Stock les éléments dans un tableau)

                String Line = sr.ReadLine();
                Line = sr.ReadLine(); // pour passer la première ligne *
                string[] currentItems = Line.Split(new char[] { '\t' });
                String currentName = currentItems[0];

                // Continue à lire jusqu'à la fin du fichier
                while (currentName != "null")
                {
                    i = 0;
                    ++errorLine;
                    if (currentItems.Length != numberOfColumn)
                    {
                        richTextBox1.Text = "Warning: @line " + errorLine + " - table should contain 11 members separated by tabs"; ;
                    }

                    // Ecriture des éléments dans le fichier XML
                    myXmlTextWriter.WriteStartElement("SoilItem");
                    myXmlTextWriter.WriteAttributeString("name", currentItems[i]);
                    myXmlTextWriter.WriteElementString("Comments", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("Kq", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("Ko", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("No", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("MinNir", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("Ndp", currentItems[++i]);

                    myXmlTextWriter.WriteStartElement("LayersArray");
                    while (currentItems[0].Equals(currentName) && currentItems[0] != "null")
                    {
                        j = i;
                        myXmlTextWriter.WriteStartElement("SoilLayer");
                        myXmlTextWriter.WriteElementString("Clay", currentItems[++j]);
                        myXmlTextWriter.WriteElementString("Kql", currentItems[++j]);
                        myXmlTextWriter.WriteElementString("SSAT", currentItems[++j]);
                        myXmlTextWriter.WriteElementString("SDUL", currentItems[++j]);
                        myXmlTextWriter.WriteElementString("SLL", currentItems[++j]);
                        myXmlTextWriter.WriteElementString("Depth", currentItems[++j]);
                        // fin de "SoilLayer"
                        myXmlTextWriter.WriteEndElement();

                        // lecture et troncature de la ligne suivante 
                        currentItems = sortLine(sr, numberOfColumn);
                    }
                    i = j;
                    currentName = currentItems[0];

                    // fin de "LayersArray"
                    myXmlTextWriter.WriteEndElement();
                    // fermeture de "SoilItem"
                    myXmlTextWriter.WriteEndElement();
                }

                // fermeture du StreamReader
                sr.Close();
            }
            catch (Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }

            // fermeture ItemsArray
            myXmlTextWriter.WriteEndElement();
            // fermeture SoilFile
            myXmlTextWriter.WriteEndElement();

            myXmlTextWriter.Close();
        }
        private void txt2sqrun(String inputFile, String txtFile)
        {
            int numberOfColumn = 11;
            int i, j = 0;
            int errorLine = 0;

            // Création du fichier XML
            XmlTextWriter myXmlTextWriter = new XmlTextWriter(inputFile, System.Text.Encoding.UTF8);
            myXmlTextWriter.Formatting = Formatting.Indented;

            // Prologue XML
            myXmlTextWriter.WriteStartDocument();

            // Namespace XML
            myXmlTextWriter.WriteStartElement("RunFile");
            myXmlTextWriter.WriteAttributeString("xmlns:xsi", @"http://www.w3.org/2001/XMLSchema-instance");
            myXmlTextWriter.WriteAttributeString("xmlns:xsd", @"http://www.w3.org/2001/XMLSchema");
            myXmlTextWriter.WriteStartElement("ItemsArray");

            try
            {
                StreamReader sr = new StreamReader(txtFile);

                string Line = sr.ReadLine();
                Line = sr.ReadLine(); // si le ficheier .txt commence par une ligne des intitulés on passe une ligne.
                // Lecture de la première ligne du fichier texte
                // Troncature de la ligne (Stock les éléments dans un tableau)
                string[] currentItems = Line.Split(new char[] { '\t' });
                String currentName = currentItems[0];

                // Continue à lire jusqu'à la fin du fichier
                while (currentName != "null")
                {
                    i = 0;
                    ++errorLine;
                    if (currentItems.Length != numberOfColumn)
                    {
                        richTextBox1.Text = "Warning: @line " + errorLine + " - table should contain 11 members separated by tabs"; ;
                    }

                    // Ecriture des éléments dans le fichier XML
                    // début de "RunItem"
                    myXmlTextWriter.WriteStartElement("RunItem");
                    myXmlTextWriter.WriteAttributeString("name", currentItems[i]);
                    myXmlTextWriter.WriteElementString("Comments", currentItems[++i]);

                    // début de "Normal"
                    myXmlTextWriter.WriteStartElement("Normal");
                    myXmlTextWriter.WriteElementString("OutputDirectory", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("OutputVersion", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("ManagementItem", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("ParameterItem", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("RunOptionItem", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("SiteItem", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("SoilItem", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("VarietyItem", currentItems[++i]);
                    // fin de "Normal"
                    myXmlTextWriter.WriteEndElement();

                    // début de "Multi"
                    myXmlTextWriter.WriteStartElement("Multi");
                    myXmlTextWriter.WriteElementString("OutputDirectory", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("OutputVersion", currentItems[++i]);
                    myXmlTextWriter.WriteStartElement("MultiRunsArray");
                    while (currentItems[0].Equals(currentName) && currentItems[0] != "null")
                    {
                        j = i;
                        // début de "MultiRunItem"
                        myXmlTextWriter.WriteStartElement("MultiRunItem");
                        myXmlTextWriter.WriteElementString("ManagementItem", currentItems[++j]);
                        myXmlTextWriter.WriteElementString("RunOptionItem", currentItems[++j]);
                        myXmlTextWriter.WriteElementString("SiteItem", currentItems[++j]);
                        myXmlTextWriter.WriteElementString("SoilItem", currentItems[++j]);
                        myXmlTextWriter.WriteElementString("VarietyItem", currentItems[++j]);
                        myXmlTextWriter.WriteElementString("ParameterItem", currentItems[++j]);
                        // fin de "MultiRunItem"
                        myXmlTextWriter.WriteEndElement();

                        // lecture et troncature de la ligne suivante 
                        currentItems = sortLine(sr, numberOfColumn);
                    }
                    i = j;
                    currentName = currentItems[0];

                    // fin de "MultiRunsArray"
                    myXmlTextWriter.WriteEndElement();
                    myXmlTextWriter.WriteElementString("ExportNormalRuns", (currentItems[++i]).ToLower());
                    myXmlTextWriter.WriteElementString("DailyOutputPattern", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("MultiYear", currentItems[++i].ToLower());
                    myXmlTextWriter.WriteElementString("FirstYear", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("LastYear", currentItems[++i]);
                    // fin de "Multi"
                    myXmlTextWriter.WriteEndElement();

                    // début de "Sensitivity"
                    myXmlTextWriter.WriteStartElement("Sensitivity");
                    myXmlTextWriter.WriteElementString("OutputDirectory", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("OutputVersion", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("SensitivityRunsArray", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("ExportNormalRuns", currentItems[++i].ToLower());
                    myXmlTextWriter.WriteElementString("DailyOutputPattern", currentItems[++i]);
                    myXmlTextWriter.WriteElementString("OneByOne", currentItems[++i].ToLower());
                    // fin de "Sensitivity"
                    myXmlTextWriter.WriteEndElement();

                    // fin de "RunItem"
                    myXmlTextWriter.WriteEndElement();
                }

                // fermeture du StreamReader
                sr.Close();
            }
            catch (Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }

            // fin de ItemsArray
            myXmlTextWriter.WriteEndElement();
            // fin de RunFile
            myXmlTextWriter.WriteEndElement();

            myXmlTextWriter.Close();
        }

        private string[] sortLine(StreamReader sr, int numberOfColumn)
        {
            string Line = null, nextLine = null;
            string[] sortedLine = null;

            // test sur le nombre d'elements
            while (sortedLine == null || sortedLine.Length < numberOfColumn)
            {
                // Lecture des lignes du fichier texte
                nextLine = sr.ReadLine();
                Line += nextLine;
                sortedLine = Line.Split(new char[] { '\t' });
                if (nextLine == null)
                {
                    sortedLine[0] = "null";
                    break;
                }
            }
            return sortedLine;
        }
        private void errorDisplay(int lineNumber, int numberOfColumn)
        {
            richTextBox1.Text = "Warning: @line " + lineNumber + " - table should contain " + numberOfColumn + " members separated by tabs";
        }
        #endregion

        #region buttons
        private void butCreate_Click(object sender, EventArgs e)
        {
            String inputFile = comboBox1.Text + "\\" + textBox1.Text + textBox2.Text;
            String textFile = (string)linkLabel1.Text;

            if (radioButMan.Checked)
            {
                txt2sqman(inputFile, textFile);
            }
            else if (radioButVar.Checked)
            {
                txt2sqvar(inputFile, textFile);
            }
            else if (radioButPar.Checked)
            {
                txt2sqpar(inputFile, textFile);
            }
            else if (radioButSit.Checked)
            {
                txt2sqsit(inputFile, textFile);
            }
            else if (radioButSoi.Checked)
            {
                txt2sqsoi(inputFile, textFile);
            }
            else if (radioButRun.Checked)
            {
                txt2sqrun(inputFile, textFile);
            }
            else
            {
                richTextBox1.Text = "Error: any type of input file selected!";
                MessageBox.Show("Error: any type of input file selected!");
            }

            richTextBox1.Text += "\n" + comboBox1.Text + "\\" + textBox1.Text + textBox2.Text + " created!";
        }
        private void butOpenFolder_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                openFileDialog1.InitialDirectory = ProjectFile.This.Directory;
                openFileDialog1.FileName = null;
                comboBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        private void butCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region radioButtons
        private void radioButMan_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Text = (string)radioButMan.Tag;
        }
        private void radioButPar_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Text = (string)radioButPar.Tag;
        }
        private void radioButVar_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Text = (string)radioButVar.Tag;
        }
        private void radioButRun_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Text = (string)radioButRun.Tag;
        }
        private void radioButSit_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Text = (string)radioButSit.Tag;
        }
        private void radioButSoi_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Text = (string)radioButSoi.Tag;
        }

        #endregion

    }
}
