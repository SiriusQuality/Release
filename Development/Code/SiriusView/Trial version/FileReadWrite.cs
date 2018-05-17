using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace SiriusView.LockerTrialVersion
{
    class FileReadWrite
    {
        // Key for TripleDES encryption
        public static byte[] key = { 21, 10, 64, 10, 100, 40, 200, 4,
                    21, 54, 65, 236, 5, 62, 1, 54,
                    54, 6, 8, 9, 65, 4, 65, 9};

        private static byte[] iv = { 0, 0, 0, 0, 0, 0, 0, 0 };

        public static string ReadFile(string FilePath)
        {
            FileInfo fi = new FileInfo(FilePath);
            if (fi.Exists == false)
                return string.Empty;

            FileStream fin = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            TripleDES tdes = new TripleDESCryptoServiceProvider();
            CryptoStream cs = new CryptoStream(fin, tdes.CreateDecryptor(key, iv), CryptoStreamMode.Read);

            StringBuilder SB = new StringBuilder();
            int ch;
            for (int i = 0; i < fin.Length; i++)
            {
                ch = cs.ReadByte();
                if (ch == 0)
                    break;
                SB.Append(Convert.ToChar(ch));
            }

            cs.Close();
            fin.Close();
            return SB.ToString();
        }

        public static void WriteFile(string FilePath, string Data)
        {
            FileStream fout = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write);
            TripleDES tdes = new TripleDESCryptoServiceProvider();
            CryptoStream cs = new CryptoStream(fout, tdes.CreateEncryptor(key, iv), CryptoStreamMode.Write);

            byte[] d = Encoding.ASCII.GetBytes(Data);
            cs.Write(d, 0, d.Length);
            cs.WriteByte(0);

            cs.Close();
            fout.Close();
        }
    }
    static class Cryptage
    {
        static public string InverseByBase(string st, int MoveBase)
        {
            StringBuilder SB = new StringBuilder();
            int c;
            for (int i = 0; i < st.Length; i += MoveBase)
            {
                if (i + MoveBase > st.Length - 1)
                    c = st.Length - i;
                else
                    c = MoveBase;
                SB.Append(InverseString(st.Substring(i, c)));
            }
            return SB.ToString();
        }

        static public string InverseString(string st)
        {
            StringBuilder SB = new StringBuilder();
            for (int i = st.Length - 1; i >= 0; i--)
            {
                SB.Append(st[i]);
            }
            return SB.ToString();
        }

        static public string ConvertToLetterDigit(string st)
        {
            StringBuilder SB = new StringBuilder();
            foreach (char ch in st)
            {
                if (char.IsLetterOrDigit(ch) == false)
                    SB.Append(Convert.ToInt16(ch).ToString());
                else
                    SB.Append(ch);
            }
            return SB.ToString();
        }

        ///<summary>
        ///moving all characters in string insert then into new index
        ///</summary>
        ///<param name="st">string to moving characters</param>
        ///<returns>moved characters string</returns>
        static public string Boring(string st)
        {
            int NewPlace;
            char ch;
            for (int i = 0; i < st.Length; i++)
            {
                NewPlace = i * Convert.ToUInt16(st[i]);
                NewPlace = NewPlace % st.Length;
                ch = st[i];
                st = st.Remove(i, 1);
                st = st.Insert(NewPlace, ch.ToString());
            }
            return st;
        }

        static public string CreatePassword(string computerID, string Identifier)
        {
            if (Identifier.Length != 3)
                throw new ArgumentException("Identifier must be 3 character length ");

            int[] IDnumb = new int[3];
            IDnumb[0] = Convert.ToInt32(Identifier[0].ToString(), 10);
            IDnumb[1] = Convert.ToInt32(Identifier[1].ToString(), 10);
            IDnumb[2] = Convert.ToInt32(Identifier[2].ToString(), 10);
            computerID = Boring(computerID);
            computerID = InverseByBase(computerID, IDnumb[0]);
            computerID = InverseByBase(computerID, IDnumb[1]);
            computerID = InverseByBase(computerID, IDnumb[2]);

            StringBuilder SB = new StringBuilder();
            foreach (char ch in computerID)
            {
                SB.Append(ChangeChar(ch, IDnumb));
            }
            return SB.ToString();
        }

        static private char ChangeChar(char ch, int[] EnCode)
        {
            ch = char.ToUpper(ch);
            if (ch >= 'A' && ch <= 'H')
                return Convert.ToChar(Convert.ToInt16(ch) + 2 * EnCode[0]);
            else if (ch >= 'I' && ch <= 'P')
                return Convert.ToChar(Convert.ToInt16(ch) - EnCode[2]);
            else if (ch >= 'Q' && ch <= 'Z')
                return Convert.ToChar(Convert.ToInt16(ch) - EnCode[1]);
            else if (ch >= '0' && ch <= '4')
                return Convert.ToChar(Convert.ToInt16(ch) + 5);
            else if (ch >= '5' && ch <= '9')
                return Convert.ToChar(Convert.ToInt16(ch) - 5);
            else
                return '0';
        }
    }

}
