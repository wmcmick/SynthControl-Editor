using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SynthControlEditor
{
    public class Translator
    {
        public List<string> descriptions;
        public int maxLength;
        public int number;
        public string name;

        public Translator(int iMaxLength, int iNumber, string sName)
        {
            number = iNumber;
            name = sName;
            maxLength = iMaxLength;
            descriptions = new List<string>();
        }
        
        public bool Load(string sFileName)
        {
            BinaryReader reader = new BinaryReader(File.Open(sFileName, FileMode.Open));

            name = ReadLine(reader);
            while (reader.PeekChar() > 0)
            {
                descriptions.Add(ReadLine(reader));
            }

            reader.Close();

            return true;
        }

        public static string ReadLine(BinaryReader r)
        {
            string sTmp = "";
            char c;
            while (r.PeekChar()>0 && (c = r.ReadChar()) != '\n')
                sTmp += c;
            return sTmp.Trim();
        }

        public bool Save(string sFolder)
        {
            BinaryWriter writer = new BinaryWriter(File.Open( Path.Combine(sFolder, "t"+number.ToString()+".trl"), FileMode.Create));
            
            writer.Write(Encoding.ASCII.GetBytes(name));
            writer.Write(Encoding.ASCII.GetBytes("\n"));
            
            for (int i = 0; i < descriptions.Count; i++)
            {
                string sTmp = descriptions[i].PadRight(13);
                writer.Write(Encoding.ASCII.GetBytes(sTmp));
                if (i < descriptions.Count - 1)
                    writer.Write(Encoding.ASCII.GetBytes("\n"));
            }
            writer.Close();
            
            return true;
        }

        public static int GetMaxLength(int iTranslator)
        {
            iTranslator--;
            //int[] lengths = new int[] { 100, 100, 100, 70, 70, 70, 40, 40, 40, 20, 20, 20, 10, 10, 10, 10, 10, 10 };
            int[] lengths = new int[] { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
            if (iTranslator >= 0 && iTranslator <= 15)
                return lengths[iTranslator];
            else
                return -1;
        }

    }
}
