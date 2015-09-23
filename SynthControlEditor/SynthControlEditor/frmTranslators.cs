using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SynthControlEditor
{
    public partial class frmTranslators : Form
    {
        public bool changesMade;
        public Translator translator;

        private bool translatorChanged;
        private string folder;

        public frmTranslators(string sFolder, int iTranslatorNumber, string sTranslatorName)
        {
            folder = sFolder;

            InitializeComponent();

            this.Location = SynthControlEditor.Properties.Settings.Default.FormTranslatorsPosition;

            translator = new Translator(Translator.GetMaxLength(iTranslatorNumber), iTranslatorNumber, sTranslatorName);
            string sFilename = "t" + iTranslatorNumber.ToString() + ".trl";
            if (File.Exists(Path.Combine(sFolder, sFilename)))
            {
                translator.Load(Path.Combine(sFolder, sFilename));
                LoadDescriptions();
            }


            txtName.Text = sTranslatorName;
            translatorChanged = false;
            changesMade = false;
        }

        private void LoadDescriptions()
        {
            for (int i = 0; i < translator.descriptions.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                ListViewItem.ListViewSubItem lviSub = new ListViewItem.ListViewSubItem();
                lviSub.Text = translator.descriptions[i];
                lvi.Text = i.ToString();
                lvi.SubItems.Add(lviSub);
                lstTranslator.Items.Add(lvi);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditDescription();
        }

        private void EditDescription()
        {
            if (lstTranslator.SelectedItems.Count > 0)
            {
                frmTranslatorDescription frm = new frmTranslatorDescription(lstTranslator.SelectedItems[0].SubItems[1].Text);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstTranslator.SelectedItems[0].SubItems[1].Text = frm.name;
                    translatorChanged = true;
                }
            }
        }

        private void UpdateValues()
        {
            for (int i = 0; i < lstTranslator.Items.Count; i++)
            {
                lstTranslator.Items[i].Text = i.ToString();
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {

        }

        private void lstTranslator_DoubleClick(object sender, EventArgs e)
        {
            EditDescription();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveTranslator();
            translatorChanged = false;
        }

        private void SaveTranslator()
        {
            translator.name = txtName.Text;
            translator.descriptions.Clear();
            for (int i = 0; i < lstTranslator.Items.Count; i++)
                translator.descriptions.Add(lstTranslator.Items[i].SubItems[1].Text);
            translator.Save(folder);
            changesMade = true;
        }

        private void frmTranslators_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool save = false;
            if (translatorChanged)
                if (MessageBox.Show("There are unsaved changes to the translator, save before closing?", "SynthControl Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    save = true;

            if (save)
                SaveTranslator();

            SynthControlEditor.Properties.Settings.Default.FormTranslatorsPosition = this.Location;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            translatorChanged = true;
        }
    }
}
