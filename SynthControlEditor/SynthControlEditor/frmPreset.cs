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
    public partial class frmPreset : Form
    {
        private Preset preset;
        string rootPath;

        private List<TextBox> lHeaders;
        
        private List<TextBox> lSysex;
        private List<TextBox> lSysex2;
        private List<TextBox> lSysex3;
        private List<TextBox> lSysex4;
        private List<List<TextBox>> lSysexLayers;
        private List<Label> lSysexLayerLabels;

        private List<TextBox> lParameterHeaders;
        private List<Label> lParameterLabels;

        private List<NumericUpDown> lNumbers;
        private List<Label> lNumberLabels;

        //private Page page;
        private ListViewItemPage lviPageEdited;

        private bool readingParameter;
        private int parameterNumber;

        private frmDisplay displayForm;
        
        public frmPreset(Preset pPreset, string sRootPath) : base() 
        {
            preset = pPreset;
            rootPath = sRootPath;
            
            // true when reading parameter values to gui, so text changed events will not do anything at that point
            readingParameter = true;

            InitializeComponent();

            this.Text += preset.name;
            lblPresetName.Text = preset.name;

            LoadPages();

            //ListViewItemPage lvi = new ListViewItemPage();
            //lvi.Text = "Testpage";
            //page = lvi.page;
            //lstPages.Items.Add(lvi);

            // The active parameter
            parameterNumber = 0;

            lHeaders = new List<TextBox>();
            lHeaders.Add(txtTopLeft);
            lHeaders.Add(txtTopRight);
            lHeaders.Add(txtBottomLeft);
            lHeaders.Add(txtBottomRight);
            
            // Create a list of sysex text boxes
            lSysex = new List<TextBox>();
            lSysex.Add(txtSysex1);
            lSysex.Add(txtSysex2);
            lSysex.Add(txtSysex3);
            lSysex.Add(txtSysex4);
            lSysex.Add(txtSysex5);
            lSysex.Add(txtSysex6);
            lSysex.Add(txtSysex7);
            lSysex.Add(txtSysex8);
            lSysex.Add(txtSysex9);
            lSysex.Add(txtSysex10);
            lSysex.Add(txtSysex11);
            lSysex.Add(txtSysex12);
            lSysex.Add(txtSysex13);
            lSysex.Add(txtSysex14);
            lSysex.Add(txtSysex15);
            lSysex.Add(txtSysex16);
            lSysex.Add(txtSysex17);

            lSysexLayerLabels = new List<Label>();
            lSysexLayerLabels.Add(lblSysexLayer2);
            lSysexLayerLabels.Add(lblSysexLayer3);
            lSysexLayerLabels.Add(lblSysexLayer4);

            lSysex2 = new List<TextBox>();
            lSysex2.Add(txtSysex2_1);
            lSysex2.Add(txtSysex2_2);
            lSysex2.Add(txtSysex2_3);

            lSysex3 = new List<TextBox>();
            lSysex3.Add(txtSysex3_1);
            lSysex3.Add(txtSysex3_2);
            lSysex3.Add(txtSysex3_3);

            lSysex4 = new List<TextBox>();
            lSysex4.Add(txtSysex4_1);
            lSysex4.Add(txtSysex4_2);
            lSysex4.Add(txtSysex4_3);

            lSysexLayers = new List<List<TextBox>>();
            lSysexLayers.Add(lSysex2);
            lSysexLayers.Add(lSysex3);
            lSysexLayers.Add(lSysex4);

            lNumbers = new List<NumericUpDown>();
            lNumbers.Add(numNumber1);
            lNumbers.Add(numNumber2);
            lNumbers.Add(numNumber3);
            lNumbers.Add(numNumber4);

            lNumberLabels = new List<Label>();
            lNumberLabels.Add(lblNumber1);
            lNumberLabels.Add(lblNumber2);
            lNumberLabels.Add(lblNumber3);
            lNumberLabels.Add(lblNumber4);

            lParameterHeaders = new List<TextBox>();
            lParameterHeaders.Add(txtHeader1);
            lParameterHeaders.Add(txtHeader2);
            lParameterHeaders.Add(txtHeader3);
            lParameterHeaders.Add(txtHeader4);
            lParameterHeaders.Add(txtHeader5);
            lParameterHeaders.Add(txtHeader6);
            lParameterHeaders.Add(txtHeader7);
            lParameterHeaders.Add(txtHeader8);
            lParameterHeaders.Add(txtHeader9);
            lParameterHeaders.Add(txtHeader10);
            lParameterHeaders.Add(txtHeader11);
            lParameterHeaders.Add(txtHeader12);
            lParameterHeaders.Add(txtHeader13);
            lParameterHeaders.Add(txtHeader14);
            lParameterHeaders.Add(txtHeader15);
            lParameterHeaders.Add(txtHeader16);

            lParameterLabels = new List<Label>();
            lParameterLabels.Add(lblParameter1);
            lParameterLabels.Add(lblParameter2);
            lParameterLabels.Add(lblParameter3);
            lParameterLabels.Add(lblParameter4);
            lParameterLabels.Add(lblParameter5);
            lParameterLabels.Add(lblParameter6);
            lParameterLabels.Add(lblParameter7);
            lParameterLabels.Add(lblParameter8);
            lParameterLabels.Add(lblParameter9);
            lParameterLabels.Add(lblParameter10);
            lParameterLabels.Add(lblParameter11);
            lParameterLabels.Add(lblParameter12);
            lParameterLabels.Add(lblParameter13);
            lParameterLabels.Add(lblParameter14);
            lParameterLabels.Add(lblParameter15);
            lParameterLabels.Add(lblParameter16);
            lParameterLabels[0].BackColor = Color.DarkRed;

            UpdateColors();

            numLsbPos.BackColor = Color.LightGreen;
            numMsbPos.BackColor = Color.LightCyan;
            numChannel.BackColor = Color.LavenderBlush;
            numLength.BackColor = Color.LightCoral;
            cmbChecksum.BackColor = Color.LightSalmon;
            numParameterPos.BackColor = Color.LightGoldenrodYellow;
            numBytes.BackColor = Color.LightGoldenrodYellow;

            UpdateLayers();

            // Select first item in comboboxes
            cmbType.SelectedIndex = 0;
            cmbChecksum.SelectedIndex = 0;
            cmbTranslators.SelectedIndex = 0;

            lviPageEdited = null;
        }

        public void UpdateRanges()
        {
            
            int checksum = cmbChecksum.SelectedIndex > 0 ? 1 : 0;

            if (checksum == 1)
                numLength.Minimum = 5;
            else
                numLength.Minimum = 4;

            int length = Convert.ToInt16(numLength.Value);

            numChannel.Maximum = length - 1 - checksum;
            numLsbPos.Maximum = length - 1 - checksum;
            numMsbPos.Maximum = length - 1 - checksum;
            numParameterPos.Maximum = length - 2 - checksum;
            
        }

        public void UpdateColors()
        {
            // Clear all colors
            for (int i = 1; i < lSysex.Count; i++)
            {
                lSysex[i].BackColor = SystemColors.Window;
                if (lSysex[i].Text == "F7")
                    lSysex[i].Text = "00";
                if (i >= (Convert.ToInt16(numLength.Value)-1))
                    lSysex[i].Enabled = false;
                else
                    lSysex[i].Enabled = true;
            }

            if (Convert.ToInt16(numChannel.Value) > 2)
            {
                lSysex[Convert.ToInt16(numChannel.Value) - 1].BackColor = Color.LavenderBlush;
                lblChannel.Text = "";
                numChannelType.Enabled = true;
                lblChannelType.Enabled = true;
            }
            else
            {
                lblChannel.Text = "Disabled";
                numChannelType.Enabled = false;
                lblChannelType.Enabled = false;
            }

            int iTmp = Convert.ToInt16(numParameterPos.Value) - 1;
            for(int i=iTmp;i<iTmp+Convert.ToInt16(numBytes.Value);i++)
                lSysex[i].BackColor = Color.LightGoldenrodYellow;

            lSysex[Convert.ToInt16(numLsbPos.Value) - 1].BackColor = Color.LightGreen;
            
            if(Convert.ToInt16(numMsbPos.Value) > 2) {
                lSysex[Convert.ToInt16(numMsbPos.Value) - 1].BackColor = Color.LightCyan;
                lblMsb.Text = "";
            }
            else
                lblMsb.Text = "Disabled";
            
            if (cmbChecksum.SelectedIndex > 0)
                lSysex[Convert.ToInt16(numLength.Value) - 2].BackColor = Color.LightSalmon;
            lSysex[Convert.ToInt16(numLength.Value) - 1].BackColor = Color.LightCoral;
            lSysex[Convert.ToInt16(numLength.Value) - 1].Text = "F7";

        }

        private void UpdateLayers()
        {
            int layers = Convert.ToInt16(numLayers.Value);
            bool enable = layers == 1 ? false : true;
            Color col = enable ? Color.LightGoldenrodYellow : SystemColors.Window;

            chkLayersChannels.Enabled = enable;
            //numParameterPos.Enabled = enable;
            //numBytes.Enabled = enable;

            foreach (List<TextBox> ltb in lSysexLayers)
                foreach (TextBox tb in ltb)
                {
                    tb.BackColor = col;
                    tb.Enabled = enable;
                }

            foreach (Label lbl in lSysexLayerLabels)
                lbl.Enabled = enable;
            
            for (int i = 1; i < 4; i++)
            {
                if (layers - 1 < i)
                    lNumbers[i].Enabled = false;
                else
                    lNumbers[i].Enabled = true;
            }

            for (int i = 0; i < 3; i++)
            {
                if (layers - 2 < i)
                {
                    foreach (TextBox tbox in lSysexLayers[i])
                    {
                        tbox.Enabled = false;
                        tbox.BackColor = SystemColors.Window;
                    }
                    lSysexLayerLabels[i].Enabled = false;
                }
                else
                {
                    for (int j = Convert.ToInt16(numBytes.Value); j <= 2; j++)
                    {
                        lSysexLayers[i][j].Enabled = false;
                        lSysexLayers[i][j].BackColor = SystemColors.Window;
                    }
                }
            }

            if (cmbType.SelectedIndex == (int)Parameter.Types.SYSEX)
            {
                grpSysex.Enabled = true;
                foreach (Label lbl in lNumberLabels)
                    lbl.Enabled = false;
                foreach (NumericUpDown num in lNumbers)
                    num.Enabled = false;
                chkLayersChannels.Enabled = false;
            }
            else
            {
                grpSysex.Enabled = false;
                numNumber1.Enabled = true;
                lblNumber1.Enabled = true;

            }
        }

        private void sysex_ValueChanged(object sender, EventArgs e)
        {
            if (!readingParameter)
            {
                UpdateRanges();
                UpdateColors();
                SysexChanged();
            }
        }

        private void numLayers_ValueChanged(object sender, EventArgs e)
        {
            if (!readingParameter)
            {
                UpdateLayers();
                ParameterValuesChanged();
            }
        }

        private void numBytes_ValueChanged(object sender, EventArgs e)
        {
            if (!readingParameter)
            {
                UpdateLayers();
                UpdateRanges();
                UpdateColors();
                SysexChanged();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            byte test = Parameter.MSB7bit(780);
            MessageBox.Show(test.ToString());
            test = Parameter.LSB7bit(780);
            MessageBox.Show(test.ToString());
        }

        private void lblParameter_MouseEnter(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.ForeColor = Color.White;
        }

        private void lblParameter_MouseLeave(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.ForeColor = Color.GreenYellow;
        }

        private void lblParameter_MouseDown(object sender, MouseEventArgs e)
        {
            Label lbl = (Label)sender;

            foreach (Label label in lParameterLabels)
                label.BackColor = Color.Black;

            lbl.BackColor = Color.DarkRed;

            readingParameter = true;
            // Read parameter
            parameterNumber = Convert.ToInt16(lbl.Text) - 1;
            ReadParameter(parameterNumber);
            readingParameter = false;
            
            if(displayForm != null && lviPageEdited != null)
                displayForm.UpdateDisplay(preset, lviPageEdited.page, parameterNumber);
        }

        private void lblParameter_MouseUp(object sender, MouseEventArgs e)
        {
            Label lbl = (Label)sender;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            /*ComboBox cmb = (ComboBox)sender;
            if (cmb.SelectedIndex == (int)Parameter.Types.SYSEX)
            {
                grpSysex.Enabled = true;
            }
            else
            {
                grpSysex.Enabled = false;
            }*/

            if (!readingParameter)
            {
                UpdateLayers();
                UpdateColors();
                ParameterValuesChanged();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dlgSave.ShowDialog() == DialogResult.OK)
                lviPageEdited.page.Save(dlgSave.FileName);
        }

        private void txtSysex_TextChanged(object sender, EventArgs e)
        {
            if (!readingParameter)
            {
                SysexChanged();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            readingParameter = false;
        }

        private void ParameterValuesChanged()
        {
            for (int i = 0; i < 16; i++)
                lviPageEdited.page.parameters[i].nameShort = lParameterHeaders[i].Text;
            lviPageEdited.page.parameters[parameterNumber].nameLong = txtFullName.Text;
            lviPageEdited.page.parameters[parameterNumber].type = (byte)cmbType.SelectedIndex;
            lviPageEdited.page.parameters[parameterNumber].min = Convert.ToUInt16(numMin.Value);
            lviPageEdited.page.parameters[parameterNumber].max = Convert.ToUInt16(numMax.Value);
            lviPageEdited.page.parameters[parameterNumber].displayOffset = Convert.ToInt16(numDisplayOffset.Value);
            lviPageEdited.page.parameters[parameterNumber].translator = (byte)cmbTranslators.SelectedIndex;
            lviPageEdited.page.parameters[parameterNumber].translatorOffset = Convert.ToByte(numTranslatorOffset.Value);
            lviPageEdited.page.parameters[parameterNumber].translatorUseLastItemForExceeding = chkUseHighestExceeding.Checked;
            lviPageEdited.page.parameters[parameterNumber].layers = Convert.ToByte(numLayers.Value);
            lviPageEdited.page.parameters[parameterNumber].number_l1 = Convert.ToUInt16(numNumber1.Value);
            lviPageEdited.page.parameters[parameterNumber].number_l2 = Convert.ToUInt16(numNumber2.Value);
            lviPageEdited.page.parameters[parameterNumber].number_l3 = Convert.ToUInt16(numNumber3.Value);
            lviPageEdited.page.parameters[parameterNumber].number_l4 = Convert.ToUInt16(numNumber4.Value);
            lviPageEdited.page.parameters[parameterNumber].separateChannels = chkLayersChannels.Checked;
        }

        private void SysexChanged()
        {
            // Main sysex mesage
            string message = "";
            foreach (TextBox tb in lSysex)
                message += tb.Text.PadRight(2, '0');
            lviPageEdited.page.parameters[parameterNumber].sysex.SetMessage(message, 1);
            // Sysex layer mesages
            for (int i = 0; i < lSysexLayers.Count; i++)
            {
                message = "";
                foreach (TextBox tb in lSysexLayers[i])
                    message += tb.Text.PadRight(2, '0');
                lviPageEdited.page.parameters[parameterNumber].sysex.SetMessage(message, (byte)(i + 2));
            }
            lviPageEdited.page.parameters[parameterNumber].sysex.length = Convert.ToByte(numLength.Value);
            lviPageEdited.page.parameters[parameterNumber].sysex.channelPosition = Convert.ToByte(numChannel.Value);
            lviPageEdited.page.parameters[parameterNumber].sysex.channelType = Convert.ToByte(numChannelType.Value);
            lviPageEdited.page.parameters[parameterNumber].sysex.parameterPosition = Convert.ToByte(numParameterPos.Value);
            lviPageEdited.page.parameters[parameterNumber].sysex.parameterBytes = Convert.ToByte(numBytes.Value);
            lviPageEdited.page.parameters[parameterNumber].sysex.valueLsbPosition = Convert.ToByte(numLsbPos.Value);
            lviPageEdited.page.parameters[parameterNumber].sysex.valueMsbPosition = Convert.ToByte(numMsbPos.Value);
            lviPageEdited.page.parameters[parameterNumber].sysex.checksum = (byte)cmbChecksum.SelectedIndex;
        }

        private void ReadParameter(int param)
        {
            lParameterHeaders[parameterNumber].Text = lviPageEdited.page.parameters[parameterNumber].nameShort;
            txtFullName.Text = lviPageEdited.page.parameters[parameterNumber].nameLong;
            cmbType.SelectedIndex = lviPageEdited.page.parameters[parameterNumber].type;
            numMin.Value = lviPageEdited.page.parameters[parameterNumber].min;
            numMax.Value = lviPageEdited.page.parameters[parameterNumber].max;
            numDisplayOffset.Value = lviPageEdited.page.parameters[parameterNumber].displayOffset;
            numLayers.Value = lviPageEdited.page.parameters[parameterNumber].layers;
            numNumber1.Value = lviPageEdited.page.parameters[parameterNumber].number_l1;
            numNumber2.Value = lviPageEdited.page.parameters[parameterNumber].number_l2;
            numNumber3.Value = lviPageEdited.page.parameters[parameterNumber].number_l3;
            numNumber4.Value = lviPageEdited.page.parameters[parameterNumber].number_l4;
            chkLayersChannels.Checked = lviPageEdited.page.parameters[parameterNumber].separateChannels;
            cmbTranslators.SelectedIndex = lviPageEdited.page.parameters[parameterNumber].translator;
            numTranslatorOffset.Value = lviPageEdited.page.parameters[parameterNumber].translatorOffset;
            // Sysex
            numLength.Value = lviPageEdited.page.parameters[parameterNumber].sysex.length;
            UpdateRanges();
            numChannel.Value = lviPageEdited.page.parameters[parameterNumber].sysex.channelPosition;
            numChannelType.Value = lviPageEdited.page.parameters[parameterNumber].sysex.channelType;
            numParameterPos.Value = lviPageEdited.page.parameters[parameterNumber].sysex.parameterPosition;
            numBytes.Value = lviPageEdited.page.parameters[parameterNumber].sysex.parameterBytes;
            numLsbPos.Value = lviPageEdited.page.parameters[parameterNumber].sysex.valueLsbPosition;
            numMsbPos.Value = lviPageEdited.page.parameters[parameterNumber].sysex.valueMsbPosition;
            cmbChecksum.SelectedIndex = lviPageEdited.page.parameters[parameterNumber].sysex.checksum;
            for (int i = 0; i < 17; i++)
                lSysex[i].Text = lviPageEdited.page.parameters[parameterNumber].sysex.message[i];
            for (int i = 0; i < 3; i++)
                lSysex2[i].Text = lviPageEdited.page.parameters[parameterNumber].sysex.message_l2[i];
            for (int i = 0; i < 3; i++)
                lSysex3[i].Text = lviPageEdited.page.parameters[parameterNumber].sysex.message_l3[i];
            for (int i = 0; i < 3; i++)
                lSysex4[i].Text = lviPageEdited.page.parameters[parameterNumber].sysex.message_l4[i];

            // Update GUI
            UpdateLayers();
            UpdateRanges();
            UpdateColors();

        }

        private void txtParameter_TextChanged(object sender, EventArgs e)
        {
            if (!readingParameter)
                ParameterValuesChanged();
            if (displayForm != null && lviPageEdited != null)
                displayForm.UpdateDisplay(preset, lviPageEdited.page, parameterNumber);
        }

        private void txtHeader_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
                lviPageEdited.page.headers[i] = lHeaders[i].Text;
            if(displayForm != null && lviPageEdited != null)
                displayForm.UpdateDisplay(preset, lviPageEdited.page, parameterNumber);
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            SavePages();
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnNewPage_Click(object sender, EventArgs e)
        {
            frmPageName frmNewPage = new frmPageName("");
            if (frmNewPage.ShowDialog() == DialogResult.OK)
            {
                if (frmNewPage.name.Length > 0)
                {
                    ListViewItemPage lvi = new ListViewItemPage();
                    lvi.Text = frmNewPage.name;
                    lvi.page.name = frmNewPage.name;
                    lstPages.Items.Add(lvi);
                    lstPages.Items[lstPages.Items.Count - 1].Selected = true;
                    EditPage();
                }
                else
                    MessageBox.Show("Page name cannot be empty!");
            }
        }

        private void lstPages_DoubleClick(object sender, EventArgs e)
        {
            EditPage();
        }

        private void EditPage()
        {
            if (lstPages.SelectedItems.Count > 0)
            {
                lviPageEdited = (ListViewItemPage)lstPages.SelectedItems[0];
                txtPageName.Text = lstPages.SelectedItems[0].Text;
                pnlDisplay.Enabled = true;
                grpMainSettings.Enabled = true;
                grpTranslator.Enabled = true;
            }
            else
                MessageBox.Show("No page selected!", "SynthControl Editor");
        }

        private void btnEditPage_Click(object sender, EventArgs e)
        {
            EditPage();
        }

        private void btnMovePageUp_Click(object sender, EventArgs e)
        {
            if (lstPages.SelectedItems.Count > 0)
            {
                int selectedIndex = lstPages.SelectedIndices[0];
                if (selectedIndex > 0)
                {
                    ListViewItem lvi = lstPages.Items[selectedIndex];
                    lstPages.Items.RemoveAt(selectedIndex);
                    lstPages.Items.Insert(selectedIndex - 1, lvi);
                }
            }
            else
                MessageBox.Show("No page selected!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnMovePageDown_Click(object sender, EventArgs e)
        {
            if (lstPages.SelectedItems.Count > 0)
            {
                int selectedIndex = lstPages.SelectedIndices[0];
                if (selectedIndex < lstPages.Items.Count - 1)
                {
                    ListViewItem lvi = lstPages.Items[selectedIndex];
                    lstPages.Items.RemoveAt(selectedIndex);
                    lstPages.Items.Insert(selectedIndex + 1, lvi);
                }
            }
            else
                MessageBox.Show("No page selected!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnRemovePage_Click(object sender, EventArgs e)
        {
            if (lstPages.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Are you sure you wish to remove the page named \"" + lstPages.SelectedItems[0].Text + "\"?", "SynthControl Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    lstPages.Items.RemoveAt(lstPages.SelectedIndices[0]);
                }
            }
            else
                MessageBox.Show("No preset selected!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnEditTranslator_Click(object sender, EventArgs e)
        {

        }

        private void txtPageName_TextChanged(object sender, EventArgs e)
        {
            if (!readingParameter)
            {
                lviPageEdited.page.name = txtPageName.Text;
                lviPageEdited.Text = txtPageName.Text;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (displayForm == null)
                displayForm = new frmDisplay();
            
            if (!displayForm.Visible)
                displayForm.Show();
            //else
            //    displayForm.Hide();
        }

        private void frmPreset_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (displayForm != null)
            {
                displayForm.Hide();
                displayForm.Dispose();
            }
        }

        private void LoadPages()
        {
            if (File.Exists(Path.Combine(Path.Combine(rootPath, preset.folderName), "pages.lst")))
            {
                BinaryReader reader = new BinaryReader(File.Open( Path.Combine(Path.Combine(rootPath, preset.folderName), "pages.lst"), FileMode.Open));
                while (reader.PeekChar() > 0)
                {
                    string sTmp = Encoding.ASCII.GetString(reader.ReadBytes(17));
                    ListViewItemPage lvi = new ListViewItemPage();
                    lvi.Text = sTmp;
                    lstPages.Items.Add(lvi);
                    
                    // Read new line character
                    if (reader.PeekChar() > 0)
                        reader.ReadByte();
                }
            }
        }

        private void SavePages()
        {
            // Delete all pages
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(rootPath, preset.folderName));
            FileInfo[] files = dir.GetFiles("*.pag").Where(p => p.Extension == ".pag").ToArray();
            foreach (FileInfo file in files)
                try
                {
                    file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
                catch { }
            
            // Save pages.lst and pages
            BinaryWriter writer = new BinaryWriter(File.Open( Path.Combine(Path.Combine(rootPath, preset.folderName), "pages.lst"), FileMode.Create));
            for(int i=0;i<lstPages.Items.Count;i++)
            {
                ListViewItemPage lvi = (ListViewItemPage)lstPages.Items[i];
                string sTmp = lvi.Text.PadRight(17);
                writer.Write(Encoding.ASCII.GetBytes(sTmp));
                if(i<lstPages.Items.Count-1)
                    writer.Write(Encoding.ASCII.GetBytes("\n"));

                string pageFile = (i+1).ToString();
                lvi.page.Save(Path.Combine(Path.Combine(rootPath, preset.folderName),(i + 1).ToString() + ".pag"));
            }
            writer.Close();
        }
    }
}
