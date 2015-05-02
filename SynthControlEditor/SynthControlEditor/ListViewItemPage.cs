using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SynthControlEditor
{
    class ListViewItemPage : ListViewItem
    {
        public Page page;

        public ListViewItemPage() : base()
        {
            page = new Page();
        }
    }
}
