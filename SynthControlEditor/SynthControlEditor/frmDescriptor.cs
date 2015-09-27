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
    public partial class frmDescriptor : Form
    {
        public bool changesMade = false;
        public Translator translator = null;
        public Descriptor descriptor;
        public frmPreset parentForm;
        
        public frmDescriptor(frmPreset parent, string sFolder, Descriptor descr)
        {
            InitializeComponent();
            parentForm = parent;
            descriptor = descr;
            txtName.Text = descriptor.name;
            txtLines.Text = descriptor.GetLines();
            this.Location = SynthControlEditor.Properties.Settings.Default.FormTranslatorsPosition;
        }

        private void txtLines_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtLines.Text.Length == 0) return;

            if (e.KeyChar == '\r') {
                e.Handled = false;
                return;
            }

            if (e.KeyChar == '\b')
            {
                e.Handled = false;
                return;
            }

            int index = txtLines.GetLineFromCharIndex(txtLines.SelectionStart);
            string temp = txtLines.Lines[index];
            if (temp.Length < 13)
            { // character limit
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public void EditDescriptor(Descriptor descr)
        {
            Save(); // Save old one first
            descriptor = descr;
            txtName.Text = descriptor.name;
            txtLines.Text = descriptor.GetLines();
        }

        private void frmDescriptor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
            
            SynthControlEditor.Properties.Settings.Default.FormTranslatorsPosition = this.Location;
            
            this.Hide();
            e.Cancel = true;
            //parentForm.descriptorForm = null;
        }

        private void txtLines_Leave(object sender, EventArgs e)
        {
            Save();
        }

        public void Save()
        {
            descriptor.name = txtName.Text.Trim();
            descriptor.SetLines(txtLines.Lines);
            BeginInvoke(new Action(() => parentForm.UpdateDescriptors()));
            //parentForm.UpdateDescriptors();
        }

        private void frmDescriptor_Deactivate(object sender, EventArgs e)
        {
            Save();
        }

    }
}
