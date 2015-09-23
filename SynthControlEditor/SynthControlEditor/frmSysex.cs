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
    public partial class frmSysex : Form
    {
        public bool changesMade;
        private bool changed;
        private string folder;

        public frmSysex(string sFolder)
        {
            folder = sFolder;
            
            InitializeComponent();

            this.Location = SynthControlEditor.Properties.Settings.Default.FormSysexPosition;

            GetSysexFiles();

            /*translator = new Translator(Translator.GetMaxLength(iTranslatorNumber), iTranslatorNumber, sTranslatorName);
            string sFilename = "t" + iTranslatorNumber.ToString() + ".trl";
            if (File.Exists(Path.Combine(sFolder, sFilename)))
            {
                translator.Load(Path.Combine(sFolder, sFilename));
                LoadDescriptions();
            }*/
            changed = false;
            changesMade = false;
        }

        private void GetSysexFiles()
        {
            string sysexPath = Path.Combine(folder,"sysex");
            if (Directory.Exists(sysexPath))
            {
                string[] files = Directory.GetFiles(sysexPath);
                for (int i = 0; i < files.Length; i++)
                {
                    ListViewItem lvi = new ListViewItem(i.ToString());
                    ListViewItem.ListViewSubItem lviSub = new ListViewItem.ListViewSubItem();
                    FileInfo fInfo = new FileInfo(files[i]);
                    lviSub.Text = fInfo.Name;
                    lvi.SubItems.Add(lviSub);
                    lstSysex.Items.Add(lvi);
                    changed = true;
                }
            }
        }

        private void LoadDescriptions()
        {
            /*for (int i = 0; i < translator.descriptions.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                ListViewItem.ListViewSubItem lviSub = new ListViewItem.ListViewSubItem();
                lviSub.Text = translator.descriptions[i];
                lvi.Text = i.ToString();
                lvi.SubItems.Add(lviSub);
                lstSysex.Items.Add(lvi);
            }*/
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lstSysex.Items.Count < 50)
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fInfo = new FileInfo(openFileDialog.FileName);
                    if (fInfo.Name.Length < 20)
                    {
                        if(!Directory.Exists(Path.Combine(folder, "sysex")))
                            Directory.CreateDirectory(Path.Combine(folder, "sysex"));
                        File.Copy(fInfo.FullName, Path.Combine( Path.Combine(folder, "sysex"), fInfo.Name));

                        string sName = "";
                        if (fInfo.Name.IndexOf('.') > 0)
                            sName = fInfo.Name.Substring(0, fInfo.Name.LastIndexOf('.'));
                        else
                            sName = fInfo.Name;
                        //frmSysexName frmSysex = new frmSysexName(sName);
                        //if (frmSysex.ShowDialog() == DialogResult.OK)
                        //{
                            ListViewItem lvi = new ListViewItem(lstSysex.Items.Count.ToString());
                            ListViewItem.ListViewSubItem lviSub = new ListViewItem.ListViewSubItem();
                            lviSub.Text = fInfo.Name;
                            lvi.SubItems.Add(lviSub);
                            lstSysex.Items.Add(lvi);
                            changed = true;
                        //}
                    }
                    else
                    {
                        MessageBox.Show("Filename of sysex file too long (max 50)", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                /*frmTranslatorDescription frm = new frmTranslatorDescription("");
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    ListViewItem lvi = new ListViewItem();
                    ListViewItem.ListViewSubItem lviSub = new ListViewItem.ListViewSubItem();
                    lviSub.Text = frm.name;

                    lvi.Text = lstSysex.Items.Count.ToString();

                    lvi.SubItems.Add(lviSub);
                    //lvi.page.name = frm.name;
                    lstSysex.Items.Add(lvi);
                    translatorChanged = true;
                    //UpdateValues();
                }*/
            }
            else
                MessageBox.Show("Maximum number of sysex files reached!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //EditDescription();
        }

        /*private void EditDescription()
        {
            if (lstSysex.SelectedItems.Count > 0)
            {
                frmSysexName frm = new frmSysexName(lstSysex.SelectedItems[0].SubItems[1].Text);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstSysex.SelectedItems[0].SubItems[1].Text = frm.name;
                    changed = true;
                }
            }
        }*/

        private void UpdateValues()
        {
            for (int i = 0; i < lstSysex.Items.Count; i++)
            {
                lstSysex.Items[i].Text = i.ToString();
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (lstSysex.SelectedItems.Count > 0)
            {
                int selectedIndex = lstSysex.SelectedIndices[0];
                if (selectedIndex > 0)
                {
                    ListViewItem lvi = lstSysex.Items[selectedIndex];
                    lstSysex.Items.RemoveAt(selectedIndex);
                    lstSysex.Items.Insert(selectedIndex - 1, lvi);
                }
                changed = true;
                UpdateValues();
            }
            else
                MessageBox.Show("No line selected!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void lstTranslator_DoubleClick(object sender, EventArgs e)
        {
            //EditDescription();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (lstSysex.SelectedItems.Count > 0)
            {
                int selectedIndex = lstSysex.SelectedIndices[0];
                if (selectedIndex < lstSysex.Items.Count - 1)
                {
                    ListViewItem lvi = lstSysex.Items[selectedIndex];
                    lstSysex.Items.RemoveAt(selectedIndex);
                    lstSysex.Items.Insert(selectedIndex + 1, lvi);
                }
                changed = true;
                UpdateValues();
            }
            else
                MessageBox.Show("No preset selected!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstSysex.SelectedItems.Count > 0)
            {
                if (File.Exists(Path.Combine(Path.Combine(folder, "sysex"), lstSysex.SelectedItems[0].SubItems[2].Text)))
                    File.Delete(Path.Combine(Path.Combine(folder, "sysex"), lstSysex.SelectedItems[0].SubItems[2].Text));
                lstSysex.Items.RemoveAt(lstSysex.SelectedIndices[0]);
                changed = true;
            }
            else
                MessageBox.Show("No line selected!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            changed = false;
        }

        private void frmTranslators_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changed)
                UpdateSysexList();

            SynthControlEditor.Properties.Settings.Default.FormSysexPosition = this.Location;
            /*bool save = false;
            if (changed)
                if (MessageBox.Show("There are unsaved changes to the translator, save before closing?", "SynthControl Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    save = true;

            if (save)
                SaveTranslator();*/
        }

        private void UpdateSysexList()
        {
            BinaryWriter writer = new BinaryWriter(File.Open(Path.Combine(folder, "sysex.lst"), FileMode.Create));
            for (int i = 0; i < lstSysex.Items.Count; i++)
            {
                // Filename
                string sTmp = lstSysex.Items[i].SubItems[1].Text;
                if (sTmp.Length < 20)
                    sTmp += '\0'; // Add a null character to filename if length of filename is shorter than 20
                byte[] bytes = Encoding.ASCII.GetBytes(sTmp);
                byte[] padding = new byte[20 - bytes.Length];
                writer.Write(bytes);
                writer.Write(padding);
                if (i < lstSysex.Items.Count - 1)
                    writer.Write(Encoding.ASCII.GetBytes("\n"));
            }
            writer.Close();
        }
    }
}
