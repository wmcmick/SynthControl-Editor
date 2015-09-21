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
            this.SuspendLayout();
            // 
            // txtDisplay1
            // 
            this.txtDisplay1.BackColor = System.Drawing.Color.DarkGreen;
            this.txtDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplay1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplay1.ForeColor = System.Drawing.Color.GreenYellow;
            this.txtDisplay1.Location = new System.Drawing.Point(260, 12);
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
            this.txtDisplay2.Location = new System.Drawing.Point(502, 12);
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
            this.txtDisplayMain.Location = new System.Drawing.Point(12, 12);
            this.txtDisplayMain.MaxLength = 20;
            this.txtDisplayMain.Multiline = true;
            this.txtDisplayMain.Name = "txtDisplayMain";
            this.txtDisplayMain.ReadOnly = true;
            this.txtDisplayMain.Size = new System.Drawing.Size(210, 76);
            this.txtDisplayMain.TabIndex = 170;
            this.txtDisplayMain.TabStop = false;
            // 
            // frmDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(723, 100);
            this.ControlBox = false;
            this.Controls.Add(this.txtDisplayMain);
            this.Controls.Add(this.txtDisplay2);
            this.Controls.Add(this.txtDisplay1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDisplay";
            this.Text = "SynthControl Editor - Page Display";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDisplay1;
        private System.Windows.Forms.TextBox txtDisplay2;
        private System.Windows.Forms.TextBox txtDisplayMain;
    }
}