namespace SynthControlEditor
{
    partial class frmDisplay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDisplay));
            this.txtDisplay1 = new System.Windows.Forms.TextBox();
            this.txtDisplay2 = new System.Windows.Forms.TextBox();
            this.txtDisplayMain = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbMidiDevices = new System.Windows.Forms.ComboBox();
            this.chkOnTop = new System.Windows.Forms.CheckBox();
            this.pianoControl = new Sanford.Multimedia.Midi.UI.PianoControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDisplay1
            // 
            this.txtDisplay1.BackColor = System.Drawing.Color.DarkGreen;
            this.txtDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplay1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplay1.ForeColor = System.Drawing.Color.GreenYellow;
            this.txtDisplay1.Location = new System.Drawing.Point(257, 10);
            this.txtDisplay1.MaxLength = 20;
            this.txtDisplay1.Multiline = true;
            this.txtDisplay1.Name = "txtDisplay1";
            this.txtDisplay1.ReadOnly = true;
            this.txtDisplay1.Size = new System.Drawing.Size(210, 76);
            this.txtDisplay1.TabIndex = 168;
            this.txtDisplay1.TabStop = false;
            // 
            // txtDisplay2
            // 
            this.txtDisplay2.BackColor = System.Drawing.Color.DarkGreen;
            this.txtDisplay2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplay2.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplay2.ForeColor = System.Drawing.Color.GreenYellow;
            this.txtDisplay2.Location = new System.Drawing.Point(501, 10);
            this.txtDisplay2.MaxLength = 20;
            this.txtDisplay2.Multiline = true;
            this.txtDisplay2.Name = "txtDisplay2";
            this.txtDisplay2.ReadOnly = true;
            this.txtDisplay2.Size = new System.Drawing.Size(210, 76);
            this.txtDisplay2.TabIndex = 169;
            this.txtDisplay2.TabStop = false;
            // 
            // txtDisplayMain
            // 
            this.txtDisplayMain.BackColor = System.Drawing.Color.DarkGreen;
            this.txtDisplayMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplayMain.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplayMain.ForeColor = System.Drawing.Color.GreenYellow;
            this.txtDisplayMain.Location = new System.Drawing.Point(11, 10);
            this.txtDisplayMain.MaxLength = 20;
            this.txtDisplayMain.Multiline = true;
            this.txtDisplayMain.Name = "txtDisplayMain";
            this.txtDisplayMain.ReadOnly = true;
            this.txtDisplayMain.Size = new System.Drawing.Size(210, 76);
            this.txtDisplayMain.TabIndex = 170;
            this.txtDisplayMain.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.txtDisplay2);
            this.panel1.Controls.Add(this.txtDisplayMain);
            this.panel1.Controls.Add(this.txtDisplay1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(721, 101);
            this.panel1.TabIndex = 171;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(34, 119);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 69);
            this.pictureBox1.TabIndex = 172;
            this.pictureBox1.TabStop = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(181, 215);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(52, 23);
            this.btnRefresh.TabIndex = 175;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 174;
            this.label3.Text = "Midi output device";
            // 
            // cmbMidiDevices
            // 
            this.cmbMidiDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMidiDevices.FormattingEnabled = true;
            this.cmbMidiDevices.Location = new System.Drawing.Point(23, 216);
            this.cmbMidiDevices.Name = "cmbMidiDevices";
            this.cmbMidiDevices.Size = new System.Drawing.Size(152, 21);
            this.cmbMidiDevices.TabIndex = 173;
            this.cmbMidiDevices.SelectedIndexChanged += new System.EventHandler(this.cmbMidiDevices_SelectedIndexChanged);
            // 
            // chkOnTop
            // 
            this.chkOnTop.AutoSize = true;
            this.chkOnTop.Location = new System.Drawing.Point(127, 154);
            this.chkOnTop.Name = "chkOnTop";
            this.chkOnTop.Size = new System.Drawing.Size(92, 17);
            this.chkOnTop.TabIndex = 176;
            this.chkOnTop.Text = "Always on top";
            this.chkOnTop.UseVisualStyleBackColor = true;
            this.chkOnTop.CheckedChanged += new System.EventHandler(this.chkOnTop_CheckedChanged);
            // 
            // pianoControl
            // 
            this.pianoControl.BackColor = System.Drawing.SystemColors.Control;
            this.pianoControl.HighNoteID = 109;
            this.pianoControl.Location = new System.Drawing.Point(34, 253);
            this.pianoControl.LowNoteID = 21;
            this.pianoControl.Name = "pianoControl";
            this.pianoControl.NoteOnColor = System.Drawing.Color.SkyBlue;
            this.pianoControl.Size = new System.Drawing.Size(678, 54);
            this.pianoControl.TabIndex = 177;
            this.pianoControl.Text = "pianoControl";
            this.pianoControl.PianoKeyDown += new System.EventHandler<Sanford.Multimedia.Midi.UI.PianoKeyEventArgs>(this.pianoControl_PianoKeyDown);
            this.pianoControl.PianoKeyUp += new System.EventHandler<Sanford.Multimedia.Midi.UI.PianoKeyEventArgs>(this.pianoControl_PianoKeyUp);
            // 
            // frmDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(759, 346);
            this.ControlBox = false;
            this.Controls.Add(this.pianoControl);
            this.Controls.Add(this.chkOnTop);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbMidiDevices);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmDisplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SynthControl Editor - Page Display";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDisplay_FormClosing);
            this.Load += new System.EventHandler(this.frmDisplay_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDisplay_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmDisplay_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDisplay1;
        private System.Windows.Forms.TextBox txtDisplay2;
        private System.Windows.Forms.TextBox txtDisplayMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbMidiDevices;
        private System.Windows.Forms.CheckBox chkOnTop;
        private Sanford.Multimedia.Midi.UI.PianoControl pianoControl;
    }
}