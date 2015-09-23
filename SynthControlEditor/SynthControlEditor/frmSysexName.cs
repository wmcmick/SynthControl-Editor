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
    public partial class frmSysexName : Form
    {
        public string name;
        public string nameOld;

        public frmSysexName(string sName)
        {
            InitializeComponent();

            //if (sName.Length > 0)
            //    btnOk.Text = "Save";

            this.DialogResult = DialogResult.OK;
            name = sName;
            nameOld = name;
            txtName.Text = name;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Ok();
        }

        private void Ok()
        {
            name = txtName.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            //    Ok();
        }
    }
}
