namespace SynthControlEditor
{
    partial class frmSettings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numSysexMessagePause = new System.Windows.Forms.NumericUpDown();
            this.numSysexBufferPause = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numSysexBufferLength = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numSysexPause = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSysexMessagePause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSysexBufferPause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSysexBufferLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSysexPause)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Preset short name";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(63, 179);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Create";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(144, 179);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.DarkGreen;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.ForeColor = System.Drawing.Color.GreenYellow;
            this.txtName.Location = new System.Drawing.Point(148, 13);
            this.txtName.MaxLength = 4;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(61, 20);
            this.txtName.TabIndex = 1;
            this.toolTip.SetToolTip(this.txtName, "The short name of the preset (used for universal favorites)");
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numSysexPause);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numSysexMessagePause);
            this.groupBox1.Controls.Add(this.numSysexBufferPause);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numSysexBufferLength);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 134);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SYSEX";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Pause after \'F7\' (ms)";
            // 
            // numSysexMessagePause
            // 
            this.numSysexMessagePause.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSysexMessagePause.Location = new System.Drawing.Point(125, 101);
            this.numSysexMessagePause.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numSysexMessagePause.Name = "numSysexMessagePause";
            this.numSysexMessagePause.Size = new System.Drawing.Size(72, 20);
            this.numSysexMessagePause.TabIndex = 15;
            this.numSysexMessagePause.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numSysexBufferPause
            // 
            this.numSysexBufferPause.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSysexBufferPause.Location = new System.Drawing.Point(125, 75);
            this.numSysexBufferPause.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numSysexBufferPause.Name = "numSysexBufferPause";
            this.numSysexBufferPause.Size = new System.Drawing.Size(72, 20);
            this.numSysexBufferPause.TabIndex = 14;
            this.numSysexBufferPause.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Pause after buffer (ms)";
            // 
            // numSysexBufferLength
            // 
            this.numSysexBufferLength.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSysexBufferLength.Location = new System.Drawing.Point(125, 23);
            this.numSysexBufferLength.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numSysexBufferLength.Name = "numSysexBufferLength";
            this.numSysexBufferLength.Size = new System.Drawing.Size(72, 20);
            this.numSysexBufferLength.TabIndex = 12;
            this.numSysexBufferLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Buffer length";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Pause after buffer (ms)";
            // 
            // numSysexPause
            // 
            this.numSysexPause.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSysexPause.Location = new System.Drawing.Point(125, 49);
            this.numSysexPause.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numSysexPause.Name = "numSysexPause";
            this.numSysexPause.Size = new System.Drawing.Size(72, 20);
            this.numSysexPause.TabIndex = 18;
            this.numSysexPause.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmSettings
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(231, 213);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SynthControl Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSysexMessagePause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSysexBufferPause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSysexBufferLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSysexPause)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numSysexMessagePause;
        private System.Windows.Forms.NumericUpDown numSysexBufferPause;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numSysexBufferLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numSysexPause;
        private System.Windows.Forms.Label label5;
    }
}