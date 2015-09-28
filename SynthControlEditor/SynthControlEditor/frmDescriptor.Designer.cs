namespace SynthControlEditor
{
    partial class frmDescriptor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDescriptor));
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLines = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(52, 12);
            this.txtName.MaxLength = 0;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(137, 20);
            this.txtName.TabIndex = 8;
            this.txtName.Leave += new System.EventHandler(this.txtLines_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Name";
            // 
            // txtLines
            // 
            this.txtLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLines.Location = new System.Drawing.Point(14, 42);
            this.txtLines.Multiline = true;
            this.txtLines.Name = "txtLines";
            this.txtLines.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLines.Size = new System.Drawing.Size(175, 408);
            this.txtLines.TabIndex = 9;
            this.txtLines.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLines_KeyPress);
            this.txtLines.Leave += new System.EventHandler(this.txtLines_Leave);
            // 
            // frmDescriptor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 495);
            this.Controls.Add(this.txtLines);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmDescriptor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SynthControl Editor - Translator";
            this.Deactivate += new System.EventHandler(this.frmDescriptor_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDescriptor_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLines;
    }
}