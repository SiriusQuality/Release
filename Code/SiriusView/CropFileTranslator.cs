using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using SiriusModel.InOut;
using SiriusView.File;

namespace SiriusView
{
    public partial class CropFileTranslator : Form
    {
        public CropFileTranslator()
        {
            InitializeComponent();
        }

        private Dictionary<string, string> OldNewParamName = new Dictionary<string, string>()
        {
            // Dry mass allocation
                {"FracBEAR", "FracBEAR"},
                {"FracLaminaeBGR", "FracLaminaeBGR"},
                {"FracSheathBGR", "FracSheathBGR"},
                {"FracStemWSC", "FracStemWSC"},
                {"SLWp", "SLWp"},
                {"SSWp", "SSWp"},
                {"PhyllDurationDMlost", "PhyllDurationDMlost"},
                // Evapotranspiration
                {"Alpha", "Alpha"},
                {"tauAlpha", "tauAlpha"},
                // Grain
                {"AlphaAlbGlo", "AlphaAlbGlo"},
                {"AlphaGlu", "AlphaGlu"},
                {"AlphaNc", "AlphaNC"},
                {"BetaAlbGlo", "BetaAlbGlo"},
                {"BetaGlu", "BetaGlu"},
                {"TTcd", "Dcd"},
                {"TTegfm", "Degfm"},
                {"TTer", "Der"},
                {"TTaegf", "Dgf"},
                {"EarGR", "EarGR"},
                {"RGRStruc", "Kcd"},
                // Leaf layer expansion
                {"AreaPLL", "AreaPL"},
                {"AreaSLL", "AreaSL"},
                {"AreaSSL", "AreaSS"},
                {"L_EP", "L_EP"},
                {"L_IN1", "L_IN1"},
                {"NLL", "NLL"},
                {"PhyllGroInterNode", "PexpIN"},
                {"PhyllGroLamina", "PexpL"},
                {"PhyllMBLL", "PlagLL"},
                {"PhyllMSLL", "PlagSL"},
                {"PhyllSBLL", "PsenLL"},
                {"PhyllSSLL", "PsenSL"},
                {"RatioFLPL", "RatioFLPL"},
                // Light use efficiency
                {"Co2FacRue", "FacCO2"},
                {"Kl", "Kl"},
                {"LueDiffuse", "LUE"},
                {"SlopeFR", "SlopeFR"},
                {"StdCo2", "StdCO2"},
                {"TauSLN", "TauSLN"},
                {"Tmax", "LUETmax"},
                {"Topt", "LUETopt"},
                {"LUETmin", "LUETmin"},
                {"LUETshape", "LUETshape"},
                // N allocation
                {"AlphaKn", "AlphaKn"},
                {"AlphaNNI", "AlphaNNI"},
                {"AlphaSSN", "AlphaSSN"},
                {"BetaKn", "BetaKn"},
                {"BetaNNI", "BetaNNI"},
                {"BetaSSN", "BetaSSN"},
                {"LLOSS", "LLOSS"},
                {"MaxLeafRRNU", "MaxLeafRRND"},
                {"MaxStemN", "MaxStemN"},
                {"MaxStemRRNU", "MaxStemRRND"},
                {"SLNcri", "SLNcri"},
                {"SLNmax0", "SLNmax0"},
                {"SLNmin", "SLNmin"},
                {"StrucLeafN", "StrucLeafN"},
                {"StrucStemN", "StrucStemN"},
                // Phenology
                {"AMNLFNO", "AMNLFNO"},
                {"AMXLFNO", "AMXLFNO"},
                {"TTsoem", "Dse"},
                {"GrowthHabit", "IsVernalizable"}, // Spring/Winter to 0/1
                {"IntermTvern", "IntTvern"},
                {"MaxLPhyll", "Ldecr"},
                {"MinLPhyll", "Lincr"},
                {"MaxDL", "MaxDL"},
                {"MaxLeafSoil", "MaxLeafSoil"},
                {"MaxTvern", "MaxTvern"},
                {"MinDL", "MinDL"},
                {"MinTvern", "MinTvern"},
                {"Phyllochron", "P"},
                {"PNslope", "PNslope"},
                {"PNini", "PNini"},
                {"PhyllDecr", "Pdecr"},
                {"PhyllFLLAnth", "PFLLAnth"},
                {"PhyllIncr", "Pincr"},
                {"Rp", "Rp"},
                {"SDws", "SDws"},
                {"SDsa_nh", "SDsa_nh"},
                {"SDsa_nh", "SDsa_nh"},
                {"SLDL", "SLDL"},
                {"tbase", "Tbase"},
                {"VAI", "VAI"},
                {"VBEE", "VBEE"},
                {"SenAccT", "SenAccT"}, // #Andrea 27/11/2015
                {"SenAccSlope", "SenAccSlope"}, // #Andrea 27/11/2015
                {"PreAnthesisTmin", "PreAnthesisTmin"},// #Andrea 27/11/2015
                {"PreAnthesisTopt", "PreAnthesisTopt"},// #Andrea 27/11/2015
                {"PreAnthesisTmax", "PreAnthesisTmax"},// #Andrea 27/11/2015
                {"PreAnthesisShape", "PreAnthesisShape"},// #Andrea 27/11/2015
                {"PostAnthesisTmin", "PostAnthesisTmin"},// #Andrea 27/11/2015
                {"PostAnthesisTopt", "PostAnthesisTopt"},// #Andrea 27/11/2015
                {"PostAnthesisTmax", "PostAnthesisTmax"},// #Andrea 27/11/2015
                {"PostAnthesisShape", "PostAnthesisShape"},// #Andrea 27/11/2015
                // Root growth and N uptake
                {"BetaRWU", "BetaRWU"},
                {"DMmaxNuptake", "DMmaxNuptake"},
                {"MaxAnForP", "MaxNuptake"},
                {"MaxRWU", "MaxRWU"},
                {"RVER", "RVER"},
                // Soil drought factor
                {"LowerFTSWexpansion", "LowerFTSWexp"},
                {"LowerFTSWtranspiration", "LowerFTSWgs"},
                {"LowerFTSWbiomass", "LowerFTSWlue"},
                {"LowerFTSWsenescence", "LowerFTSWsen"},
                {"LowerVPD", "LowerVPD"},
                {"MaxDSF", "MaxDSF"},
                {"UpperFTSWexpansion", "UpperFTSWexp"},
                {"UpperFTSWtranspiration", "UpperFTSWgs"},
                {"UpperFTSWbiomass", "UpperFTSWlue"},
                {"UpperFTSWsenescence", "UpperFTSWsen"},
                {"UpperVPD", "UpperVPD"}
        };

        private void ProcessButton_Click(object sender, EventArgs e)
        {
            string buffer;

            // recup name of the input file, and generate the output name
            var temp = inputFileLink.Text.Split('.');
            var nb = temp.LongCount();
            string output = "";

            for (int i = 0; i < nb - 1; i++)
                output += temp[i] + ".";

            output += "_V2.1." + temp[nb - 1];

            // Open intput and output file
            XmlTextReader reader = new XmlTextReader(inputFileLink.Text);
            XmlTextWriter writer = new XmlTextWriter(output, System.Text.Encoding.UTF8);

            writer.Formatting = Formatting.Indented;

            // Prologue XML
            writer.WriteStartDocument();

            // Depending if it is a varietal or a non-varietal file
            if (temp[nb - 1] == "sqpar")
            {
                reader.ReadStartElement("ParameterFile");
                writer.WriteStartElement("NonVarietyFile");
                temp[0] = "Parameter";
            }
            else
            {
                reader.ReadStartElement("VarietyFile");
                writer.WriteStartElement("VarietyFile");
                temp[0] = "Variety";
            }

            writer.WriteAttributeString("xmlns:xsi", @"http://www.w3.org/2001/XMLSchema-instance");
            writer.WriteAttributeString("xmlns:xsd", @"http://www.w3.org/2001/XMLSchema");

            reader.MoveToContent();
            reader.MoveToElement();

            reader.ReadStartElement("ItemsArray");
            writer.WriteStartElement("ItemsArray");

            reader.MoveToContent();

            try
            {
                while (reader.AttributeCount != 0)
                {
                    buffer = reader.GetAttribute(0);

                    reader.ReadStartElement(temp[0] + "Item");
                    writer.WriteStartElement("CropParameterItem");

                    writer.WriteAttributeString("name", buffer);
                    writer.WriteElementString("Comments", reader.ReadElementString("Comments"));
                    writer.WriteStartElement("ParamValue");

                    reader.MoveToContent();

                    while (reader.IsStartElement()) // 72 parameters
                    {
                        buffer = reader.Name;

                        writer.WriteStartElement("Item"); // Open "Item"
                        writer.WriteStartElement("Key");
                        writer.WriteElementString("string", OldNewParamName[buffer]);
                        writer.WriteEndElement();

                        buffer = reader.ReadElementString(buffer);

                        // GrowthHabit case
                        if (buffer == "Spring")
                            buffer = "0";
                        if (buffer == "Winter")
                            buffer = "1";

                        writer.WriteStartElement("Value");
                        writer.WriteElementString("double", buffer);
                        writer.WriteEndElement();
                        writer.WriteEndElement(); // Close "Item"

                        reader.MoveToContent();
                    }

                    writer.WriteEndElement(); // Close "ParamValue"

                    reader.ReadEndElement(); // Close "ParameterItem"
                    reader.MoveToContent();
                    writer.WriteEndElement(); // Close "CropParameterItem"
                }


                writer.WriteEndElement(); // Close "ItemsArray"
                writer.WriteEndElement(); // Close "NonVarietyFile" or "VarietyFile"

                writer.Close();
                reader.Close();

                infoTextBox.Text = "Translation succeed.\nResult record in: " + output;
            }
            catch (Exception)
            {
                infoTextBox.Text = "Error: check if the input file is in the V1.5 format";
            }
            
        }

        private void inputFileLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Stream myStream;

            openFileDialog1.InitialDirectory = ProjectFile.This.Directory;
            openFileDialog1.Filter = "Variety File (*.sqvar)|*.sqvar|Non-Variety file (*sqpar)|*.sqpar";
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
                inputFileLink.Text = Path.GetFullPath(openFileDialog1.FileName);
                infoTextBox.Text = "File " + Path.GetFileName(openFileDialog1.FileName) + " open with success";
            }
            catch (Exception ex)
            {
                infoTextBox.Text = "Warning: " + ex.Message;
            }
        }
    }
}
