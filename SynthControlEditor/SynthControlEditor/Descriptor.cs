using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SynthControlEditor
{
    public class Descriptor
    {
        public string name;
        public int length;
        public int offset;
        public string lines;

        public Descriptor()
        {
            name = "";
            length = 0;
            offset = 0;
            lines = "";
        }
    }
}
