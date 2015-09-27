using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Sanford.Multimedia.Midi;
using AuSharp;

namespace SynthControlEditor
{
    public partial class frmDisplay : Form
    {
        private Knob knob1, knob2, knob3, knob4, knob5, knob6, knob7, knob8, knob9, knob10, knob11, knob12, knob13, knob14, knob15, knob16;
        private List<Knob> lKnobs;
        string mainDisplay = "";

        Preset oPreset = null;
        Page oPage = null;

        private OutputDevice midiOut = null;

        public frmDisplay()
        {
            InitializeComponent();

            this.Location = SynthControlEditor.Properties.Settings.Default.FormDisplayPosition;

            knob1 = new Knob();
            knob2 = new Knob();
            knob3 = new Knob();
            knob4 = new Knob();
            knob5 = new Knob();
            knob6 = new Knob();
            knob7 = new Knob();
            knob8 = new Knob();
            knob9 = new Knob();
            knob10 = new Knob();
            knob11 = new Knob();
            knob12 = new Knob();
            knob13 = new Knob();
            knob14 = new Knob();
            knob15 = new Knob();
            knob16 = new Knob();
            lKnobs = new List<Knob>();
            lKnobs.Add(knob1);
            lKnobs.Add(knob2);
            lKnobs.Add(knob3);
            lKnobs.Add(knob4);
            lKnobs.Add(knob5);
            lKnobs.Add(knob6);
            lKnobs.Add(knob7);
            lKnobs.Add(knob8);
            lKnobs.Add(knob9);
            lKnobs.Add(knob10);
            lKnobs.Add(knob11);
            lKnobs.Add(knob12);
            lKnobs.Add(knob13);
            lKnobs.Add(knob14);
            lKnobs.Add(knob15);
            lKnobs.Add(knob16);
           
            for (int i = 0; i < lKnobs.Count; i++)
            {
                lKnobs[i].BackColor = System.Drawing.Color.White;
                lKnobs[i].KnobColor = System.Drawing.Color.Black;
                lKnobs[i].KnobRadius = 16;
                if(i<8)
                    lKnobs[i].Location = new System.Drawing.Point(262+i*60, 118);
                else
                    lKnobs[i].Location = new System.Drawing.Point(262 + (i-8) * 60, 182);
                lKnobs[i].MarkerColor = System.Drawing.Color.Black;
                lKnobs[i].Name = "Parameter";
                lKnobs[i].Size = new System.Drawing.Size(49, 57);
                lKnobs[i].TabIndex = i+1;
                lKnobs[i].Text = (i+1).ToString();
                lKnobs[i].TextKnobRelation = AuSharp.TextKnobRelation.KnobAboveText;
                lKnobs[i].TickColor = System.Drawing.Color.Black;
                lKnobs[i].Minimum = 0;
                lKnobs[i].Maximum = 255;
                lKnobs[i].ValueChanged += frmDisplay_ValueChanged;
                this.Controls.Add(lKnobs[i]);
            }

        }

        void frmDisplay_ValueChanged(object sender, EventArgs e)
        {
            Knob knob = (Knob)sender;
            int index = Convert.ToInt32(knob.Text) - 1;
            int val = 0;
            int valShow = 0;
            string desc = "";

            if (oPage.parameters[index].type != (byte)Parameter.Types.NONE)
            {
                val = map(knob.Value, knob.Minimum, knob.Maximum, oPage.parameters[index].min, oPage.parameters[index].max);
                valShow = val + oPage.parameters[index].displayOffset;
                if (oPage.parameters[index].translator == (byte)Descriptor.Types.NONE)
                    desc = "";
                else if (oPage.parameters[index].translator == (byte)Descriptor.Types.ASCII)
                    desc = ": " + Descriptor.GetAsciiChar(val, oPage.parameters[index].translatorOffset);
                else if (oPage.parameters[index].translator == (byte)Descriptor.Types.SEMITONES)
                    desc = ": " + Descriptor.GetSemitone(val, oPage.parameters[index].translatorOffset);
                else if (oPage.parameters[index].translator == (byte)Descriptor.Types.QUARTERTONES)
                    desc = ": " + Descriptor.GetQuartertone(val, oPage.parameters[index].translatorOffset);
                
                txtDisplayMain.Text = mainDisplay + oPage.parameters[index].nameLong + Environment.NewLine + valShow.ToString().PadLeft(5) + desc;

                if (midiOut != null)
                {

                    ChannelMessageBuilder builder = new ChannelMessageBuilder();
                    builder.MidiChannel = 0;

                    if (oPage.parameters[index].type == (byte)Parameter.Types.PB)
                    {
                        builder.Command = ChannelCommand.PitchWheel;
                        builder.Data1 = Parameter.LSB7bit((ushort)val);
                        builder.Data2 = Parameter.MSB7bit((ushort)val);
                        builder.Build();
                        midiOut.Send(builder.Result);
                    }
                    else if (oPage.parameters[index].type == (byte)Parameter.Types.AT)
                    {
                        builder.Command = ChannelCommand.ChannelPressure;
                        builder.Data1 = val;
                        builder.Build();
                        midiOut.Send(builder.Result);
                    }
                    else if (oPage.parameters[index].type == (byte)Parameter.Types.CC7)
                    {
                        builder.Command = ChannelCommand.Controller;
                        builder.Data1 = oPage.parameters[index].number_l1;
                        builder.Data2 = val;
                        builder.Build();
                        midiOut.Send(builder.Result);
                    }
                    else if (oPage.parameters[index].type == (byte)Parameter.Types.CC14)
                    {
                        builder.Command = ChannelCommand.Controller;
                        builder.Data1 = oPage.parameters[index].number_l1;
                        builder.Data2 = Parameter.MSB7bit((ushort)val);
                        builder.Build();
                        midiOut.Send(builder.Result);

                        builder.Command = ChannelCommand.Controller;
                        builder.Data1 = oPage.parameters[index].number_l1+32;
                        builder.Data2 = Parameter.LSB7bit((ushort)val);
                        builder.Build();
                        midiOut.Send(builder.Result);
                    }
                    else if (oPage.parameters[index].type == (byte)Parameter.Types.RPN7 || 
                        oPage.parameters[index].type == (byte)Parameter.Types.NRPN7 || 
                        oPage.parameters[index].type == (byte)Parameter.Types.RPN14 || 
                        oPage.parameters[index].type == (byte)Parameter.Types.NRPN14)
                    {
                        int cc1 = 0;
                        int cc2 = 0;
                        if (oPage.parameters[index].type == (byte)Parameter.Types.RPN7 || oPage.parameters[index].type == (byte)Parameter.Types.RPN14)
                        {
                            cc1 = 101;
                            cc2 = 100;
                        }
                        else
                        {
                            cc1 = 99;
                            cc2 = 98;
                        }
                        builder.Command = ChannelCommand.Controller;
                        builder.Data1 = cc1;
                        builder.Data2 = Parameter.MSB7bit(oPage.parameters[index].number_l1);
                        builder.Build();
                        midiOut.Send(builder.Result);

                        builder.Command = ChannelCommand.Controller;
                        builder.Data1 = cc2;
                        builder.Data2 = Parameter.LSB7bit(oPage.parameters[index].number_l1);
                        builder.Build();
                        midiOut.Send(builder.Result);

                        if (oPage.parameters[index].type == (byte)Parameter.Types.RPN14 || oPage.parameters[index].type == (byte)Parameter.Types.NRPN14)
                        {
                            builder.Command = ChannelCommand.Controller;
                            builder.Data1 = 6;
                            builder.Data2 =  Parameter.MSB7bit((ushort)val);
                            builder.Build();
                            midiOut.Send(builder.Result);

                            builder.Command = ChannelCommand.Controller;
                            builder.Data1 = 38;
                            builder.Data2 = Parameter.LSB7bit((ushort)val);
                            builder.Build();
                            midiOut.Send(builder.Result);
                        }
                        else
                        {
                            builder.Command = ChannelCommand.Controller;
                            builder.Data1 = 6;
                            builder.Data2 = val;
                            builder.Build();
                            midiOut.Send(builder.Result);
                        }

                    }
                    else if (oPage.parameters[index].type == (byte)Parameter.Types.SYSEX)
                    {
                        //byte[] sysexMessage = new byte[] {0xF0, 0x41, 0x10, 0x42, 0x12, 0x40, 0x00, 0x7F, 0x00, 0x41, 0xF7};
                        byte[] sysexMessage = oPage.parameters[index].sysex.GetMessage((ushort)val, 6, 1);

                        SysExMessage sysex = new SysExMessage(sysexMessage);
                        midiOut.Send(sysex);

                    }
                   
                }
            }
        }

        int map(int s, int a1, int a2, int b1, int b2)
        {
            return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        }


        public void UpdateDisplay(Preset preset, Page page, int parameterNumber)
        {
            oPreset = preset;
            oPage = page;
            
            mainDisplay = " 1|" + preset.name + Environment.NewLine + " 1|" + page.name + Environment.NewLine;
            //mainDisplay += page.parameters[parameterNumber].nameLong + Environment.NewLine;
            txtDisplayMain.Text = mainDisplay;

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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetMidiDevices();
        }

        private void cmbMidiDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            SynthControlEditor.Properties.Settings.Default.MidiDevice = cmbMidiDevices.SelectedItem.ToString();
            try
            {
                if (midiOut != null)
                    midiOut.Close();
                midiOut = new OutputDevice(cmbMidiDevices.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmDisplay_Load(object sender, EventArgs e)
        {
            GetMidiDevices();
            chkOnTop.Checked = SynthControlEditor.Properties.Settings.Default.PreviewOnTop;
        }

        private void GetMidiDevices()
        {
            cmbMidiDevices.Items.Clear();
            for (int i = 0; i < OutputDevice.DeviceCount; i++)
            {
                MidiOutCaps caps = OutputDevice.GetDeviceCapabilities(i);
                cmbMidiDevices.Items.Add(caps.name);
                if (caps.name == SynthControlEditor.Properties.Settings.Default.MidiDevice)
                {
                    cmbMidiDevices.SelectedIndex = i;
                }
            }
        }

        private void frmDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (midiOut != null)
                    midiOut.Close();
            }
            catch (Exception) { }

            SynthControlEditor.Properties.Settings.Default.FormDisplayPosition = this.Location;
        }

        private void chkOnTop_CheckedChanged(object sender, EventArgs e)
        {
            SynthControlEditor.Properties.Settings.Default.PreviewOnTop = chkOnTop.Checked;
            this.TopMost = chkOnTop.Checked;
        }

        private void pianoControl_PianoKeyDown(object sender, Sanford.Multimedia.Midi.UI.PianoKeyEventArgs e)
        {
            midiOut.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, e.NoteID, 127));
        }

        private void pianoControl_PianoKeyUp(object sender, Sanford.Multimedia.Midi.UI.PianoKeyEventArgs e)
        {
            midiOut.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, e.NoteID, 0));
        }

        private void frmDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            //pianoControl.PressPianoKey(e.KeyCode);
        }

        private void frmDisplay_KeyUp(object sender, KeyEventArgs e)
        {
            //pianoControl.ReleasePianoKey(e.KeyCode);
        }

    }
}
