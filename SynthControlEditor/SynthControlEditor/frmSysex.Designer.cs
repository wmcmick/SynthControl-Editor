namespace SynthControlEditor
{
    partial class frmSysex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSysex));
            this.lstSysex = new System.Windows.Forms.ListView();
            this.clmnIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // lstSysex
            // 
            this.lstSysex.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmnIndex,
            this.clmnFilename});
            this.lstSysex.FullRowSelect = true;
            this.lstSysex.HideSelection = false;
            this.lstSysex.Location = new System.Drawing.Point(12, 12);
            this.lstSysex.MultiSelect = false;
            this.lstSysex.Name = "lstSysex";
            this.lstSysex.Size = new System.Drawing.Size(263, 516);
            this.lstSysex.TabIndex = 0;
            this.lstSysex.UseCompatibleStateImageBehavior = false;
            this.lstSysex.View = System.Windows.Forms.View.Details;
            this.lstSysex.DoubleClick += new System.EventHandler(this.lstTranslator_DoubleClick);
            // 
            // clmnIndex
            // 
            this.clmnIndex.Text = "Index";
            this.clmnIndex.Width = 41;
            // 
            // clmnFilename
            // 
            this.clmnFilename.Text = "Filename";
            this.clmnFilename.Width = 194;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(281, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "&Add sysex";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(281, 505);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(281, 71);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "&Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Sysex files|*.syx|All files|*.*";
            // 
            // frmSysex
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 538);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstSysex);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSysex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SynthControl Editor - Sysex";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTranslators_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstSysex;
        private System.Windows.Forms.ColumnHeader clmnIndex;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ColumnHeader clmnFilename;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}