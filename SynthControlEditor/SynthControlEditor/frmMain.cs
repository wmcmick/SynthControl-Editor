using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace SynthControlEditor
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            this.Location = SynthControlEditor.Properties.Settings.Default.FormMainPosition;

            txtFolder.Text = SynthControlEditor.Properties.Settings.Default.FolderMain;
            folderBrowserDialog.SelectedPath = txtFolder.Text;
            GetPresets();
        }

        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (Directory.Exists(folderBrowserDialog.SelectedPath))
                {
                    txtFolder.Text = folderBrowserDialog.SelectedPath;
                    GetPresets();
                }
                else
                {
                    MessageBox.Show("Folder not found!", "SynthControl Editor",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SynthControlEditor.Properties.Settings.Default.FolderMain = txtFolder.Text;
            SynthControlEditor.Properties.Settings.Default.FormMainPosition = this.Location;

            SynthControlEditor.Properties.Settings.Default.Save();
        }

        private void GetPresets()
        {         
            lstPresets.Items.Clear();

            if (Directory.Exists(txtFolder.Text))
            {
                if (!File.Exists(Path.Combine(txtFolder.Text, "presets.lst")))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(txtFolder.Text);
                    foreach (DirectoryInfo subDir in dirInfo.GetDirectories())
                    {
                        ListViewItemPreset item = new ListViewItemPreset(new string[] { subDir.Name, subDir.Name });
                        item.preset.folderName = subDir.Name;
                        item.preset.name = subDir.Name;
                        lstPresets.Items.Add(item);
                    }
                    UpdatePresets();
                }
                else
                {
                    BinaryReader binaryReader = new BinaryReader(File.Open(Path.Combine(txtFolder.Text, "presets.lst"), FileMode.Open));
                    byte [] file = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
                    binaryReader.Close();
                    string fileText = System.Text.Encoding.ASCII.GetString(file);
                    string[] lines = fileText.Split('\n');
                    for (int i = 0; i < lines.Length - 1; i += 2) // There is one extra line break at the end
                    {
                        ListViewItemPreset item = new ListViewItemPreset(new string[] { lines[i+1].Trim(), lines[i].Trim() });
                        item.preset.folderName = lines[i].Trim().Replace("\0",""); // Trim and remove null character that was saved for device
                        item.preset.name = lines[i + 1].Trim();
                        lstPresets.Items.Add(item);
                    }
                }
            }
            else
            {
                MessageBox.Show("Folder not found!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddPreset_Click(object sender, EventArgs e)
        {
            frmPresetFolderName frmNewPreset = new frmPresetFolderName("", "");
            if (frmNewPreset.ShowDialog() == DialogResult.OK)
            {
                Directory.CreateDirectory(Path.Combine(txtFolder.Text, frmNewPreset.folderName));
                ListViewItemPreset item = new ListViewItemPreset(new string[] { frmNewPreset.name, frmNewPreset.folderName });
                item.preset.name = frmNewPreset.name;
                item.preset.folderName = frmNewPreset.folderName;
                lstPresets.Items.Add(item);
            }
            UpdatePresets();
        }

        private void btnEditPreset_Click(object sender, EventArgs e)
        {
            EditPreset();
        }

        private void EditPreset()
        {
            if (lstPresets.SelectedItems.Count > 0)
            {
                ListViewItemPreset lvItem = (ListViewItemPreset)lstPresets.SelectedItems[0];
                frmPreset frmEditPreset = new frmPreset(lvItem.preset, txtFolder.Text);
                if (frmEditPreset.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Should save!", "SynthControl Editor");
                }
            }
            else
                MessageBox.Show("No preset selected!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (lstPresets.SelectedItems.Count > 0)
            {
                frmPresetFolderName frmNewPreset = new frmPresetFolderName(lstPresets.SelectedItems[0].Text, lstPresets.SelectedItems[0].SubItems[1].Text);
                if (frmNewPreset.ShowDialog() == DialogResult.OK)
                {
                    ListViewItemPreset lvi = (ListViewItemPreset)lstPresets.SelectedItems[0];
                    if (frmNewPreset.folderNameOld.ToLower() != frmNewPreset.folderName.ToLower())
                    {
                        if (!Directory.Exists(Path.Combine(txtFolder.Text, frmNewPreset.folderName)))
                        {
                            Directory.Move(Path.Combine(txtFolder.Text, frmNewPreset.folderNameOld), Path.Combine(txtFolder.Text, frmNewPreset.folderName));
                            lvi.SubItems[1].Text = frmNewPreset.folderName;
                            lvi.preset.folderName = frmNewPreset.folderName;
                        }
                        else
                            MessageBox.Show("Folder already exists, can't rename folder!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                    lvi.Text = frmNewPreset.name;
                    lvi.preset.name = frmNewPreset.name;

                    UpdatePresets();
                    //Directory.CreateDirectory(Path.Combine(txtFolder.Text, frmNewPreset.folderName));
                    //ListViewItem item = new ListViewItem(new string[] { frmNewPreset.name, frmNewPreset.folderName });
                    //lstPresets.Items.Add(item);
                }
            }
            else
                MessageBox.Show("No preset selected!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void UpdatePresets()
        {
            BinaryWriter writer = new BinaryWriter(File.Open(Path.Combine(txtFolder.Text, "presets.lst"), FileMode.Create));
            for (int i = 0; i < lstPresets.Items.Count; i++)
            {
                string sTmp = lstPresets.Items[i].SubItems[1].Text;
                if(sTmp.Length < 8)
                    sTmp += '\0'; // Add a null character to folder name if length of folder name is shorter than 8
                //sTmp.PadRight(8);
                byte[] bytes = Encoding.ASCII.GetBytes(sTmp);
                //if(bytes.Length < 8)
                byte[] padding = new byte[8-bytes.Length];
                writer.Write(bytes);
                writer.Write(padding);
                writer.Write(Encoding.ASCII.GetBytes("\n"));
                sTmp = lstPresets.Items[i].Text.PadRight(17);
                writer.Write(Encoding.ASCII.GetBytes(sTmp));
                if (i < lstPresets.Items.Count - 1)
                    writer.Write(Encoding.ASCII.GetBytes("\n"));
            }
            writer.Close();
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (lstPresets.SelectedItems.Count > 0)
            {
                int selectedIndex = lstPresets.SelectedIndices[0];
                if (selectedIndex > 0)
                {
                    ListViewItem lvi = lstPresets.Items[selectedIndex];
                    lstPresets.Items.RemoveAt(selectedIndex);
                    lstPresets.Items.Insert(selectedIndex - 1, lvi);
                }
                UpdatePresets();
            }
            else
                MessageBox.Show("No preset selected!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (lstPresets.SelectedItems.Count > 0)
            {
                int selectedIndex = lstPresets.SelectedIndices[0];
                if (selectedIndex < lstPresets.Items.Count - 1)
                {
                    ListViewItem lvi = lstPresets.Items[selectedIndex];
                    lstPresets.Items.RemoveAt(selectedIndex);
                    lstPresets.Items.Insert(selectedIndex + 1, lvi);
                }
                UpdatePresets();
            }
            else
                MessageBox.Show("No preset selected!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstPresets.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Are you sure you wish to remove the preset named \"" + lstPresets.SelectedItems[0].Text + "\"?\n\nIt will also be removed from disk.\n(It's a good practice to export a preset before removing it.)", "SynthControl Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Directory.Delete(Path.Combine(txtFolder.Text, lstPresets.SelectedItems[0].SubItems[1].Text), true);
                    lstPresets.Items.RemoveAt(lstPresets.SelectedIndices[0]);
                    UpdatePresets();
                }
            }
            else
                MessageBox.Show("No preset selected!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnAddExisting_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Import...";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    Preset preset = new Preset();
                    preset.Import(openFileDialog.FileName, txtFolder.Text);
                    ListViewItemPreset lvi = new ListViewItemPreset(new string[] { preset.name, preset.folderName });
                    lvi.preset = preset;
                    lstPresets.Items.Add(lvi);
                    MessageBox.Show("\"" + preset.name + "\" imported successfully!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdatePresets();
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (lstPresets.SelectedItems.Count > 0)
            {
                saveFileDialog.Title = "Export to...";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Preset preset = new Preset();
                    preset.folderName = lstPresets.SelectedItems[0].SubItems[1].Text;
                    preset.name = lstPresets.SelectedItems[0].Text;
                    preset.Export(saveFileDialog.FileName, txtFolder.Text);
                    MessageBox.Show("\"" + preset.name + "\" exported to \"" + saveFileDialog.FileName + "\"!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                MessageBox.Show("No preset selected!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void lstPresets_DoubleClick(object sender, EventArgs e)
        {
            EditPreset();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSysex_Click(object sender, EventArgs e)
        {
            if (lstPresets.SelectedItems.Count > 0)
            {
                frmSysex frm = new frmSysex( Path.Combine(txtFolder.Text, lstPresets.SelectedItems[0].SubItems[1].Text.Trim().Replace("\0","") )  );
                frm.ShowDialog();
            }
        }
    }
}
