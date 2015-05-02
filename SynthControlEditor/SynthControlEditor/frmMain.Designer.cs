namespace SynthControlEditor
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lstPresets = new System.Windows.Forms.ListView();
            this.clmnPresetName = new System.Windows.Forms.ColumnHeader();
            this.clmnPresetFolder = new System.Windows.Forms.ColumnHeader();
            this.clmnPresetVisible = new System.Windows.Forms.ColumnHeader();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnChooseFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnNewPreset = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAddExisting = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnEditPreset = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnQuit = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lstPresets
            // 
            this.lstPresets.BackColor = System.Drawing.SystemColors.Window;
            this.lstPresets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmnPresetName,
            this.clmnPresetFolder,
            this.clmnPresetVisible});
            this.lstPresets.FullRowSelect = true;
            this.lstPresets.HideSelection = false;
            this.lstPresets.Location = new System.Drawing.Point(12, 40);
            this.lstPresets.MultiSelect = false;
            this.lstPresets.Name = "lstPresets";
            this.lstPresets.Size = new System.Drawing.Size(317, 462);
            this.lstPresets.TabIndex = 0;
            this.toolTip.SetToolTip(this.lstPresets, "The currently available controller presets");
            this.lstPresets.UseCompatibleStateImageBehavior = false;
            this.lstPresets.View = System.Windows.Forms.View.Details;
            this.lstPresets.DoubleClick += new System.EventHandler(this.lstPresets_DoubleClick);
            // 
            // clmnPresetName
            // 
            this.clmnPresetName.Text = "Name";
            this.clmnPresetName.Width = 144;
            // 
            // clmnPresetFolder
            // 
            this.clmnPresetFolder.Text = "Folder";
            this.clmnPresetFolder.Width = 90;
            // 
            // clmnPresetVisible
            // 
            this.clmnPresetVisible.Text = "???";
            // 
            // txtFolder
            // 
            this.txtFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFolder.Location = new System.Drawing.Point(111, 12);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(309, 20);
            this.txtFolder.TabIndex = 1;
            this.toolTip.SetToolTip(this.txtFolder, "The current path to the SD-card / folder");
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.Location = new System.Drawing.Point(12, 10);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(93, 23);
            this.btnChooseFolder.TabIndex = 2;
            this.btnChooseFolder.Text = "Choose &folder...";
            this.toolTip.SetToolTip(this.btnChooseFolder, "Choose the path to the SD-card");
            this.btnChooseFolder.UseVisualStyleBackColor = true;
            this.btnChooseFolder.Click += new System.EventHandler(this.btnChooseFolder_Click);
            // 
            // btnNewPreset
            // 
            this.btnNewPreset.Location = new System.Drawing.Point(335, 40);
            this.btnNewPreset.Name = "btnNewPreset";
            this.btnNewPreset.Size = new System.Drawing.Size(85, 23);
            this.btnNewPreset.TabIndex = 3;
            this.btnNewPreset.Text = "&New";
            this.toolTip.SetToolTip(this.btnNewPreset, "Create a new controller preset");
            this.btnNewPreset.UseVisualStyleBackColor = true;
            this.btnNewPreset.Click += new System.EventHandler(this.btnAddPreset_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Location = new System.Drawing.Point(335, 153);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(85, 23);
            this.btnMoveUp.TabIndex = 4;
            this.btnMoveUp.Text = "Move &up";
            this.toolTip.SetToolTip(this.btnMoveUp, "Move the selected preset up");
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Location = new System.Drawing.Point(335, 182);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(85, 23);
            this.btnMoveDown.TabIndex = 5;
            this.btnMoveDown.Text = "Move &down";
            this.toolTip.SetToolTip(this.btnMoveDown, "Move the selected preset down");
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(335, 330);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(85, 23);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "&Remove";
            this.toolTip.SetToolTip(this.btnRemove, "Remove the selected preset (careful)");
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAddExisting
            // 
            this.btnAddExisting.Location = new System.Drawing.Point(335, 235);
            this.btnAddExisting.Name = "btnAddExisting";
            this.btnAddExisting.Size = new System.Drawing.Size(85, 23);
            this.btnAddExisting.TabIndex = 7;
            this.btnAddExisting.Text = "&Import...";
            this.toolTip.SetToolTip(this.btnAddExisting, "Import a preset (zip)");
            this.btnAddExisting.UseVisualStyleBackColor = true;
            this.btnAddExisting.Click += new System.EventHandler(this.btnAddExisting_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(335, 264);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(85, 23);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "E&xport...";
            this.toolTip.SetToolTip(this.btnExport, "Export a preset (zip)");
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnEditPreset
            // 
            this.btnEditPreset.Location = new System.Drawing.Point(335, 69);
            this.btnEditPreset.Name = "btnEditPreset";
            this.btnEditPreset.Size = new System.Drawing.Size(85, 23);
            this.btnEditPreset.TabIndex = 9;
            this.btnEditPreset.Text = "&Edit";
            this.toolTip.SetToolTip(this.btnEditPreset, "Edit the selected preset");
            this.btnEditPreset.UseVisualStyleBackColor = true;
            this.btnEditPreset.Click += new System.EventHandler(this.btnEditPreset_Click);
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(335, 98);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(85, 23);
            this.btnRename.TabIndex = 10;
            this.btnRename.Text = "Rena&me";
            this.toolTip.SetToolTip(this.btnRename, "Rename the selected preset");
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "SynthControl presets|*.zip";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "SynthControl presets|*.zip";
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(335, 514);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(85, 23);
            this.btnQuit.TabIndex = 11;
            this.btnQuit.Text = "&Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(430, 549);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnEditPreset);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnAddExisting);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnNewPreset);
            this.Controls.Add(this.btnChooseFolder);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.lstPresets);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SynthControl Editor v0.1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstPresets;
        private System.Windows.Forms.ColumnHeader clmnPresetName;
        private System.Windows.Forms.ColumnHeader clmnPresetVisible;
        private System.Windows.Forms.ColumnHeader clmnPresetFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnChooseFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnNewPreset;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAddExisting;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnEditPreset;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.ToolTip toolTip;
    }
}