using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace GlobalSA_Morris
{
    class ReadTextFile
    {
        public string[,] ReadText(string TextFilePath, int NumberOfRandoms, int NumberOfVariables)
        {
            TextReader randomTextFile = new StreamReader(TextFilePath);

            int NumberOfLines = NumberOfRandoms;

            string[,] ListLines = new string[NumberOfLines, NumberOfVariables];

            string[] ArrayTemp;

            char[] delimiterChars = { '	' };

            for (int i = 0; i < NumberOfLines; i++)
            {
                ArrayTemp = randomTextFile.ReadLine().Split(delimiterChars);

                for (int j = 0; j < NumberOfVariables; j++)
                {
                    ListLines[i, j] = ArrayTemp[j];
                }
            }

            randomTextFile.Close();

            return ListLines;
        }
    }
}
