using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Sanford.Multimedia.Midi;
using AuSharp;

namespace SynthControlEditor
{
    public partial class frmPreset : Form
    {
        public const int DESCRIPTOR_OFFSET = 4;

        public const int TYPE_NONE = 0;
        public const int TYPE_CC7BIT = 1;
        public const int TYPE_CC14BIT = 2;
        public const int TYPE_RPN7BIT = 3;
        public const int TYPE_RPN14BIT = 4;
        public const int TYPE_NRPN7BIT = 5;
        public const int TYPE_NRPN14BIT = 6;
        public const int TYPE_SYSEX = 7;
        public const int TYPE_PB = 8;
        public const int TYPE_AT = 9;
        
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
        public frmDescriptor descriptorForm;

        private bool presetChanged;

        private Parameter clipboardParameter;

        private bool enableDescriptorIndexChanged = true;

        //private Knob knob = null;
        
        public frmPreset(Preset pPreset, string sRootPath) : base() 
        {
            presetChanged = false;
            
            preset = pPreset;
            rootPath = sRootPath;
            
            // true when reading parameter values to gui, so text changed events will not do anything at that point
            readingParameter = true;

            InitializeComponent();

            this.Location = SynthControlEditor.Properties.Settings.Default.FormPresetPosition;

            this.Text += preset.name;
            lblPresetName.Text = preset.name;

            LoadPages();
            LoadTranslators();

            UpdateDescriptors();

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
            lSysex2.Add(txtSysex2_4);

            lSysex3 = new List<TextBox>();
            lSysex3.Add(txtSysex3_1);
            lSysex3.Add(txtSysex3_2);
            lSysex3.Add(txtSysex3_3);
            lSysex3.Add(txtSysex3_4);

            lSysex4 = new List<TextBox>();
            lSysex4.Add(txtSysex4_1);
            lSysex4.Add(txtSysex4_2);
            lSysex4.Add(txtSysex4_3);
            lSysex4.Add(txtSysex4_4);

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
            cmbDescriptors.SelectedIndex = 0;

            lviPageEdited = null;
        }

        public void UpdateDescriptors()
        {
            enableDescriptorIndexChanged = false;
            int i;
            int selected = cmbDescriptors.SelectedIndex;

            cmbDescriptors.Items.Add("None");
            cmbDescriptors.Items.Add("ASCII");
            cmbDescriptors.Items.Add("Notes");
            cmbDescriptors.Items.Add("Notes (quarters)");
            for (i = cmbDescriptors.Items.Count - 1; i > 3; i--) {
                cmbDescriptors.Items.RemoveAt(i);
            }
            for (i = 0; i < preset.descriptors.Count; i++)
            {
                cmbDescriptors.Items.Add(preset.descriptors[i].name);
            }
            cmbDescriptors.Items.Add("*Add new");

            if (cmbDescriptors.Items.Count > selected)
                cmbDescriptors.SelectedIndex = selected;

            enableDescriptorIndexChanged = true;
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
                    for (int j = Convert.ToInt16(numBytes.Value); j < 4; j++)
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
                presetChanged = true;
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
                presetChanged = true;
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
            ChangeParameter((Label)sender);
            //if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //    menuStripParameter.Show();
        }

        private void ChangeParameter(Label lbl)
        {
            for (int i = 0; i < lParameterLabels.Count; i++)
                lParameterLabels[i].BackColor = Color.Black;

            lbl.BackColor = Color.DarkRed;

            readingParameter = true;
            // Read parameter
            parameterNumber = Convert.ToInt16(lbl.Text) - 1;
            ReadParameter(parameterNumber);
            readingParameter = false;

            if (displayForm != null && lviPageEdited != null)
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
                UpdateNumRanges();
                UpdateLayers();
                UpdateColors();
                ParameterValuesChanged();
                presetChanged = true;
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
                presetChanged = true;
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
            lviPageEdited.page.parameters[parameterNumber].stepSize = Convert.ToUInt16(numStepSize.Value);
            lviPageEdited.page.parameters[parameterNumber].translator = (byte)cmbDescriptors.SelectedIndex;
            lviPageEdited.page.parameters[parameterNumber].translatorOffset = Convert.ToByte(numDescriptorOffset.Value);
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

        private void ReadHeaders()
        {
            for (int i = 0; i < 4; i++)
                lHeaders[i].Text = lviPageEdited.page.headers[i];
            for (int i = 0; i < 16; i++)
                lParameterHeaders[i].Text = lviPageEdited.page.parameters[i].nameShort;
        }

        private void ReadParameter(int param)
        {
            txtFullName.Text = lviPageEdited.page.parameters[parameterNumber].nameLong;
            cmbType.SelectedIndex = lviPageEdited.page.parameters[parameterNumber].type;

            UpdateNumRanges();

            numMin.Value = lviPageEdited.page.parameters[parameterNumber].min;
            numMax.Value = lviPageEdited.page.parameters[parameterNumber].max;
            numDisplayOffset.Value = lviPageEdited.page.parameters[parameterNumber].displayOffset;
            numStepSize.Value = lviPageEdited.page.parameters[parameterNumber].stepSize;
            numLayers.Value = lviPageEdited.page.parameters[parameterNumber].layers;
            numNumber1.Value = lviPageEdited.page.parameters[parameterNumber].number_l1;
            numNumber2.Value = lviPageEdited.page.parameters[parameterNumber].number_l2;
            numNumber3.Value = lviPageEdited.page.parameters[parameterNumber].number_l3;
            numNumber4.Value = lviPageEdited.page.parameters[parameterNumber].number_l4;
            chkLayersChannels.Checked = lviPageEdited.page.parameters[parameterNumber].separateChannels;
            cmbDescriptors.SelectedIndex = lviPageEdited.page.parameters[parameterNumber].translator;
            numDescriptorOffset.Value = lviPageEdited.page.parameters[parameterNumber].translatorOffset;
            chkUseHighestExceeding.Checked = lviPageEdited.page.parameters[parameterNumber].translatorUseLastItemForExceeding;
            // Sysex
            numLength.Value = lviPageEdited.page.parameters[parameterNumber].sysex.length;
            cmbChecksum.SelectedIndex = lviPageEdited.page.parameters[parameterNumber].sysex.checksum;
            UpdateRanges();
            numChannel.Value = lviPageEdited.page.parameters[parameterNumber].sysex.channelPosition;
            numChannelType.Value = lviPageEdited.page.parameters[parameterNumber].sysex.channelType;
            numParameterPos.Value = lviPageEdited.page.parameters[parameterNumber].sysex.parameterPosition;
            numBytes.Value = lviPageEdited.page.parameters[parameterNumber].sysex.parameterBytes;
            numLsbPos.Value = lviPageEdited.page.parameters[parameterNumber].sysex.valueLsbPosition;
            numMsbPos.Value = lviPageEdited.page.parameters[parameterNumber].sysex.valueMsbPosition;
            for (int i = 0; i < 17; i++)
                lSysex[i].Text = lviPageEdited.page.parameters[parameterNumber].sysex.message[i];
            for (int i = 0; i < 4; i++)
                lSysex2[i].Text = lviPageEdited.page.parameters[parameterNumber].sysex.message_l2[i];
            for (int i = 0; i < 4; i++)
                lSysex3[i].Text = lviPageEdited.page.parameters[parameterNumber].sysex.message_l3[i];
            for (int i = 0; i < 4; i++)
                lSysex4[i].Text = lviPageEdited.page.parameters[parameterNumber].sysex.message_l4[i];

            // Update GUI
            UpdateLayers();
            UpdateRanges();
            UpdateColors();

        }

        private void UpdateNumRanges()
        {
            if (cmbType.SelectedIndex == TYPE_CC14BIT || cmbType.SelectedIndex == TYPE_NRPN7BIT || cmbType.SelectedIndex == TYPE_RPN7BIT || cmbType.SelectedIndex == TYPE_NRPN14BIT || cmbType.SelectedIndex == TYPE_RPN14BIT || cmbType.SelectedIndex == TYPE_SYSEX || cmbType.SelectedIndex == TYPE_PB)
            {
                numMin.Maximum =
                    numMax.Maximum =
                    numStepSize.Maximum =
                    numDisplayOffset.Maximum =
                    numNumber1.Maximum =
                    numNumber2.Maximum =
                    numNumber3.Maximum =
                    numNumber4.Maximum = 16383;
                numDisplayOffset.Minimum = -16383;
                if (cmbType.SelectedIndex == TYPE_CC14BIT)
                {
                    numNumber1.Maximum =
                        numNumber2.Maximum =
                        numNumber3.Maximum =
                        numNumber4.Maximum = 31;
                }
                else if (cmbType.SelectedIndex == TYPE_PB)
                {
                    numNumber1.Maximum =
                        numNumber2.Maximum =
                        numNumber3.Maximum =
                        numNumber4.Maximum = 0;
                }
                else if (cmbType.SelectedIndex == TYPE_NRPN7BIT || cmbType.SelectedIndex == TYPE_RPN7BIT)
                {
                    numMin.Maximum =
                        numMax.Maximum =
                        numStepSize.Maximum =
                        numDisplayOffset.Maximum = 127;
                    numDisplayOffset.Minimum = -127;
                }
            }
            else
            {
                numMin.Maximum =
                    numMax.Maximum =
                    numStepSize.Maximum =
                    numDisplayOffset.Maximum =
                    numNumber1.Maximum =
                    numNumber2.Maximum =
                    numNumber3.Maximum =
                    numNumber4.Maximum = 127;
                numDisplayOffset.Minimum = -127;
                if (cmbType.SelectedIndex == TYPE_AT)
                {
                    numNumber1.Maximum =
                        numNumber2.Maximum =
                        numNumber3.Maximum =
                        numNumber4.Maximum = 0;
                }
            }
        }

        private void txtParameter_TextChanged(object sender, EventArgs e)
        {
            parameterTextChanged();
        }

        private void parameterTextChanged()
        {
            if (!readingParameter)
            {
                ParameterValuesChanged();
                presetChanged = true;
            }
            if (displayForm != null && lviPageEdited != null)
                displayForm.UpdateDisplay(preset, lviPageEdited.page, parameterNumber);
        }

        private void txtHeader_TextChanged(object sender, EventArgs e)
        {
            if (!readingParameter)
            {
                for (int i = 0; i < 4; i++)
                    lviPageEdited.page.headers[i] = lHeaders[i].Text;
                presetChanged = true;
            }
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
            bool save = false;
            DialogResult dlgResult = DialogResult.Yes;
            
            if (presetChanged) {
                dlgResult = MessageBox.Show("There are unsaved changes to the preset, save before closing?", "SynthControl Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                    save = true;
            }

            if (save)
                SavePages();

            if (dlgResult != DialogResult.Cancel)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
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
                    presetChanged = true;
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
                readingParameter = true;
                lviPageEdited = (ListViewItemPage)lstPages.SelectedItems[0];
                txtPageName.Text = lstPages.SelectedItems[0].Text;
                parameterNumber = 0;
                ReadHeaders();
                ReadParameter(parameterNumber);
                HighlightParameterButton(parameterNumber);
                pnlDisplay.Enabled = true;
                grpMainSettings.Enabled = true;
                grpDescriptors.Enabled = true;
                readingParameter = false;
            }
            else
                MessageBox.Show("No page selected!", "SynthControl Editor");
        }

        private void HighlightParameterButton(int parameterButton)
        {
            foreach (Label label in lParameterLabels)
                label.BackColor = Color.Black;
            lParameterLabels[parameterButton].BackColor = Color.DarkRed;
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
                presetChanged = true;
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
                presetChanged = true;
            }
            else
                MessageBox.Show("No page selected!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnRemovePage_Click(object sender, EventArgs e)
        {
            if (lstPages.SelectedItems.Count > 0)
            {
                if ((ListViewItemPage)lstPages.SelectedItems[0] == lviPageEdited)
                {
                    MessageBox.Show("Cannot remove the page currently being edited!","SynthControl Editor",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are you sure you wish to remove the page named \"" + lstPages.SelectedItems[0].Text + "\"?", "SynthControl Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        lstPages.Items.RemoveAt(lstPages.SelectedIndices[0]);
                        presetChanged = true;
                    }
                }

            }
            else
                MessageBox.Show("No preset selected!", "SynthControl Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void txtPageName_TextChanged(object sender, EventArgs e)
        {
            if (!readingParameter)
            {
                lviPageEdited.page.name = txtPageName.Text;
                lviPageEdited.Text = txtPageName.Text;
                if (displayForm != null && lviPageEdited != null)
                    displayForm.UpdateDisplay(preset, lviPageEdited.page, parameterNumber);
                presetChanged = true;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (displayForm == null)
                displayForm = new frmDisplay();

            if (!displayForm.Visible)
            {
                displayForm.Show();
                btnPreview.Text = "Hide Preview";
            }
            else
            {
                displayForm.Hide();
                btnPreview.Text = "Show Preview";
            }

            if (displayForm != null && lviPageEdited != null)
                displayForm.UpdateDisplay(preset, lviPageEdited.page, parameterNumber);
        }

        private void frmPreset_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (displayForm != null)
            {
                displayForm.Close();
                displayForm.Dispose();
            }

            if (descriptorForm != null)
            {
                descriptorForm.Close();
                descriptorForm.Dispose();
            }

            SynthControlEditor.Properties.Settings.Default.FormPresetPosition = this.Location;
        }

        private void LoadPages()
        {
            string sFolder = Path.Combine(rootPath, preset.folderName);
            
            if (File.Exists(Path.Combine(sFolder, "pages.lst")))
            {
                BinaryReader reader = new BinaryReader(File.Open( Path.Combine(Path.Combine(rootPath, preset.folderName), "pages.lst"), FileMode.Open));
                int i = 1;
                while (reader.PeekChar() > 0)
                {
                    string sTmp = Encoding.ASCII.GetString(reader.ReadBytes(17)).Trim();

                    if (File.Exists(Path.Combine(sFolder, "p" + i.ToString() + ".pag")))
                    {
                        ListViewItemPage lvi = new ListViewItemPage();
                        lvi.Text = sTmp;
                        lvi.page.Load(Path.Combine(sFolder, "p" + i.ToString() + ".pag"));
                        lvi.page.name = sTmp;
                        lstPages.Items.Add(lvi);
                    }
                    
                    // Read new line character
                    if (reader.PeekChar() > 0)
                        reader.ReadByte();

                    i++;
                }
                reader.Close();
            }
        }

        public void LoadTranslators()
        {
            /*string sFolder = Path.Combine(rootPath, preset.folderName);

            if (File.Exists(Path.Combine(sFolder, "trans.lst")))
            {
                BinaryReader reader = new BinaryReader(File.Open(Path.Combine(Path.Combine(rootPath, preset.folderName), "trans.lst"), FileMode.Open));
                //int i = 1;
                while (reader.PeekChar() > 0)
                {
                    byte index = reader.ReadByte();
                    string sFilename = "t" + index.ToString() + ".trl";
                    if (File.Exists(Path.Combine(sFolder, sFilename)))
                    {
                        BinaryReader reader2 = new BinaryReader(File.Open(Path.Combine(Path.Combine(rootPath, preset.folderName), sFilename), FileMode.Open));
                        string sTranslatorName = Translator.ReadLine(reader2);
                        reader2.Close();
                        cmbDescriptors.Items[TRANSLATOR_OFFSET + index] = sTranslatorName;
                    }
                }
                reader.Close();
            }*/
        }

        private void SavePages()
        {
            // Variables
            string sTmp = "";
            
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
                sTmp = lvi.Text.PadRight(17);
                writer.Write(Encoding.ASCII.GetBytes(sTmp));
                if(i<lstPages.Items.Count-1)
                    writer.Write(Encoding.ASCII.GetBytes("\n"));

                string pageFile = (i+1).ToString();
                lvi.page.Save(Path.Combine(Path.Combine(rootPath, preset.folderName), "p" + (i + 1).ToString() + ".pag"));
            }
            writer.Close();

            // Save trans.lst
            writer = new BinaryWriter(File.Open(Path.Combine(Path.Combine(rootPath, preset.folderName), "trans.lst"), FileMode.Create));
            files = dir.GetFiles("*.trl").Where(p => p.Extension == ".trl").ToArray();
            for(int i=0;i<files.Length;i++)
            {
                //MessageBox.Show(files[i].Name.Remove(files[i].Name.IndexOf('.')).Substring(1));
                byte translatorNumber = Convert.ToByte(files[i].Name.Remove(files[i].Name.IndexOf('.')).Substring(1));
                writer.Write(translatorNumber);
            }
            writer.Close();

            // Reset presetChanged flag
            presetChanged = false;
        }

        private void txtHeader_Enter(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;


            for (int i = 0; i < lParameterHeaders.Count; i++)
            {
                if (txtBox == lParameterHeaders[i])
                {
                    ChangeParameter(lParameterLabels[i]);
                    break;
                }
            }
        }

        private void toolStripMenuItemCopyParameter_Click(object sender, EventArgs e)
        {
            clipboardParameter = (Parameter)lviPageEdited.page.parameters[parameterNumber].Clone();
        }

        private void toolStripMenuItemPasteParameter_Click(object sender, EventArgs e)
        {
            readingParameter = true;
            string sLongName = lviPageEdited.page.parameters[parameterNumber].nameLong;
            string sShortName = lviPageEdited.page.parameters[parameterNumber].nameShort;
            lviPageEdited.page.parameters[parameterNumber] = (Parameter)clipboardParameter.Clone();
            if (toolStripMenuItemPasteParameter == (ToolStripMenuItem)sender)
            {
                lviPageEdited.page.parameters[parameterNumber].nameLong = sLongName;
                lviPageEdited.page.parameters[parameterNumber].nameShort = sShortName;      
            }
            ReadHeaders();
            ReadParameter(parameterNumber);
            if (displayForm != null && lviPageEdited != null)
                displayForm.UpdateDisplay(preset, lviPageEdited.page, parameterNumber);
            readingParameter = false;
            presetChanged = true;

        }

        private void btnDuplicatePage_Click(object sender, EventArgs e)
        {
            if (lstPages.SelectedIndices.Count > 0)
            {
                frmPageName frmNewPage = new frmPageName("");
                if (frmNewPage.ShowDialog() == DialogResult.OK)
                {
                    if (frmNewPage.name.Length > 0)
                    {
                        ListViewItemPage lvi = new ListViewItemPage();
                        ListViewItemPage lviSelected = (ListViewItemPage)lstPages.SelectedItems[0];
                        //lvi.page = (Page)lviSelected.page.Clone();
                        Page pag = (Page)(lviSelected.page.Clone());
                        lvi.page = pag;
                        lvi.Text = frmNewPage.name;
                        lvi.page.name = frmNewPage.name;
                        lstPages.Items.Add(lvi);
                        presetChanged = true;
                    }
                    else
                        MessageBox.Show("Page name cannot be empty!");
                }               
            }
        }

        int map(int s, int a1, int a2, int b1, int b2)
        {
            return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        }

        private void control_MouseEnter(object sender, EventArgs e)
        {
            string hint = tooltip.GetToolTip((Control)sender);
            txtHelp.Text = hint;
        }

        private void control_MouseLeave(object sender, EventArgs e)
        {
            txtHelp.Text = "";
        }

        private void cmbDescriptors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!enableDescriptorIndexChanged) return;

            if (cmbDescriptors.SelectedIndex >= DESCRIPTOR_OFFSET)
            {
                Descriptor descr = null;

                if (cmbDescriptors.SelectedIndex == cmbDescriptors.Items.Count - 1)
                {
                    string descriptorName = "Descriptor " + (cmbDescriptors.SelectedIndex - DESCRIPTOR_OFFSET + 1).ToString();

                    BeginInvoke(new Action(() => cmbDescriptors.Items[cmbDescriptors.SelectedIndex] = descriptorName));

                    cmbDescriptors.Items.Add("*Add new");

                    descr = new Descriptor();
                    descr.name = descriptorName;
                    this.preset.descriptors.Add(descr);
                }
                else
                {
                    descr = this.preset.descriptors[cmbDescriptors.SelectedIndex - DESCRIPTOR_OFFSET];
                }

                if (descriptorForm == null)
                    descriptorForm = new frmDescriptor(this, Path.Combine(rootPath, preset.folderName), descr);
                else
                    descriptorForm.EditDescriptor(descr);
                descriptorForm.Show();
            }

            parameterTextChanged();
        }

    }
}
