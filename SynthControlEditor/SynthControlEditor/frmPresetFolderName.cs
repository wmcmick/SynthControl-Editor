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
    public partial class frmPresetFolderName : Form
    {
        public string name;
        public string nameOld;
        public string folderName;
        public string folderNameOld;
        //public DialogResult dialogResult;

        public frmPresetFolderName(string sName, string sFolderName)
        {
            InitializeComponent();

            //dialogResult = DialogResult.Cancel;
            this.DialogResult = DialogResult.OK;
            name = sName;
            folderName = sFolderName;
            //if (name.Length > 0)
            //{
                nameOld = name;
                txtName.Text = name;
            //}
            //if (folderName.Length > 0)
            //{
                folderNameOld = folderName;
                txtFolderName.Text = folderName;
            //}
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Ok();
        }

        private void Ok()
        {
            /*if (!Directory.Exists(Path.Combine(txtFolder.Text, frmNewPreset.folderName)))
            {
                if (frmNewPreset.folderName.Length > 0)
                {
                    if (frmNewPreset.folderName.Length > 0)
                    {
                        Directory.CreateDirectory(Path.Combine(txtFolder.Text, frmNewPreset.folderName));
                        ListViewItem item = new ListViewItem(new string[] { frmNewPreset.name, frmNewPreset.folderName });
                        lstPresets.Items.Add(item);
                    }
                    else
                        MessageBox.Show("Name can not be empty!");
                }
                else
                    MessageBox.Show("Folder name can not be empty!");
            }
            else
            {
                MessageBox.Show("Folder already exists!");
            }*/

            name = txtName.Text;
            folderName = txtFolderName.Text;
            //dialogResult = DialogResult.OK;
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
