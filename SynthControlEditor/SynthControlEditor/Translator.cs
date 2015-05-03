using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SynthControlEditor
{
    class Translator
    {
        public List<string> descriptions;
        public int maxLength;

        public Translator(int iMaxLength)
        {
            maxLength = iMaxLength;
            descriptions = new List<string>();
        }
        
        public bool Load(string sFileName)
        {
            return true;
        }

        public bool Save(string sFileName)
        {
            return true;
        }
    }
}
