using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SynthControlEditor
{
    public partial class frmDisplay : Form
    {      
        public frmDisplay()
        {
            InitializeComponent();
        }

        public void UpdateDisplay(Preset preset, Page page, int parameterNumber)
        {
            txtDisplayMain.Text = " 1|" + preset.name + Environment.NewLine + " 1|" + page.name + Environment.NewLine;
            txtDisplayMain.Text += page.parameters[parameterNumber].nameLong + Environment.NewLine;
            txtDisplayMain.Text += page.parameters[parameterNumber].max.ToString().PadLeft(5);


            txtDisplay1.Text = page.headers[0] + Environment.NewLine;
            for (int i = 0; i < 4; i++)
                txtDisplay1.Text += page.parameters[i].nameShort.PadRight(4) + " ";
            txtDisplay1.Text += Environment.NewLine;
            txtDisplay1.Text += page.headers[2] + Environment.NewLine;
            for (int i = 8; i < 12; i++)
                txtDisplay1.Text += page.parameters[i].nameShort.PadRight(4) + " ";
            txtDisplay2.Text = page.headers[1] + Environment.NewLine;
            for (int i = 4; i < 8; i++)
                txtDisplay2.Text += page.parameters[i].nameShort.PadRight(4) + " ";
            txtDisplay2.Text += Environment.NewLine;
            txtDisplay2.Text += page.headers[3] + Environment.NewLine;
            for (int i = 12; i < 16; i++)
                txtDisplay2.Text += page.parameters[i].nameShort.PadRight(4) + " ";
        }
    }
}
