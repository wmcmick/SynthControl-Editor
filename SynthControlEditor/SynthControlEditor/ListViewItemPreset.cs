using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SynthControlEditor
{
    class ListViewItemPreset : ListViewItem
    {
        public Preset preset;

        public ListViewItemPreset() : base()
        {
            preset = new Preset();
        }

        public ListViewItemPreset(string[] items)
            : base(items)
        {
            preset = new Preset();
        }
    }
}
