namespace WindowsFormsApplication1
{
    partial class FrmImportData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImportData));
            this.btnImport = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.lblDataFile = new System.Windows.Forms.Label();
            this.pnlImport = new System.Windows.Forms.Panel();
            this.rdbReplace = new System.Windows.Forms.RadioButton();
            this.rdbIgnore = new System.Windows.Forms.RadioButton();
            this.lblImport = new System.Windows.Forms.Label();
            this.cmbFileCat = new System.Windows.Forms.ComboBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.chkMeta = new System.Windows.Forms.CheckBox();
            this.lblOptions = new System.Windows.Forms.Label();
            this.dGVCal = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdExport = new System.Windows.Forms.RadioButton();
            this.rdUpdate = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFull = new System.Windows.Forms.Label();
            this.pnlImport.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVCal)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Location = new System.Drawing.Point(200, 267);
            this.btnImport.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(81, 30);
            this.btnImport.TabIndex = 16;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(664, 85);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(79, 26);
            this.btnBrowse.TabIndex = 15;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFile
            // 
            this.txtFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFile.Location = new System.Drawing.Point(201, 87);
            this.txtFile.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(453, 22);
            this.txtFile.TabIndex = 14;
            // 
            // lblDataFile
            // 
            this.lblDataFile.AutoSize = true;
            this.lblDataFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataFile.Location = new System.Drawing.Point(10, 90);
            this.lblDataFile.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblDataFile.Name = "lblDataFile";
            this.lblDataFile.Size = new System.Drawing.Size(90, 16);
            this.lblDataFile.TabIndex = 13;
            this.lblDataFile.Text = "Select File :";
            // 
            // pnlImport
            // 
            this.pnlImport.Controls.Add(this.rdbReplace);
            this.pnlImport.Controls.Add(this.rdbIgnore);
            this.pnlImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlImport.Location = new System.Drawing.Point(200, 199);
            this.pnlImport.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.pnlImport.Name = "pnlImport";
            this.pnlImport.Size = new System.Drawing.Size(365, 47);
            this.pnlImport.TabIndex = 12;
            // 
            // rdbReplace
            // 
            this.rdbReplace.AutoSize = true;
            this.rdbReplace.Location = new System.Drawing.Point(185, 11);
            this.rdbReplace.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdbReplace.Name = "rdbReplace";
            this.rdbReplace.Size = new System.Drawing.Size(163, 20);
            this.rdbReplace.TabIndex = 1;
            this.rdbReplace.Text = "Replace Duplicates";
            this.rdbReplace.UseVisualStyleBackColor = true;
            this.rdbReplace.CheckedChanged += new System.EventHandler(this.rdbReplace_CheckedChanged);
            // 
            // rdbIgnore
            // 
            this.rdbIgnore.AutoSize = true;
            this.rdbIgnore.Checked = true;
            this.rdbIgnore.Location = new System.Drawing.Point(7, 11);
            this.rdbIgnore.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rdbIgnore.Name = "rdbIgnore";
            this.rdbIgnore.Size = new System.Drawing.Size(148, 20);
            this.rdbIgnore.TabIndex = 0;
            this.rdbIgnore.TabStop = true;
            this.rdbIgnore.Text = "Ignore Duplicates";
            this.rdbIgnore.UseVisualStyleBackColor = true;
            this.rdbIgnore.CheckedChanged += new System.EventHandler(this.rdbIgnore_CheckedChanged);
            // 
            // lblImport
            // 
            this.lblImport.AutoSize = true;
            this.lblImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImport.Location = new System.Drawing.Point(10, 212);
            this.lblImport.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblImport.Name = "lblImport";
            this.lblImport.Size = new System.Drawing.Size(151, 16);
            this.lblImport.TabIndex = 11;
            this.lblImport.Text = "Select Import Type : ";
            // 
            // cmbFileCat
            // 
            this.cmbFileCat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFileCat.FormattingEnabled = true;
            this.cmbFileCat.Location = new System.Drawing.Point(201, 144);
            this.cmbFileCat.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cmbFileCat.Name = "cmbFileCat";
            this.cmbFileCat.Size = new System.Drawing.Size(182, 24);
            this.cmbFileCat.TabIndex = 10;
            this.cmbFileCat.Text = "File Category";
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTable.Location = new System.Drawing.Point(10, 147);
            this.lblTable.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(161, 16);
            this.lblTable.TabIndex = 9;
            this.lblTable.Text = "Select File Category : ";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 484);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(789, 22);
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Status";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalculate.Location = new System.Drawing.Point(198, 426);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(83, 30);
            this.btnCalculate.TabIndex = 18;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // chkMeta
            // 
            this.chkMeta.AutoSize = true;
            this.chkMeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMeta.Location = new System.Drawing.Point(201, 34);
            this.chkMeta.Name = "chkMeta";
            this.chkMeta.Size = new System.Drawing.Size(141, 20);
            this.chkMeta.TabIndex = 19;
            this.chkMeta.Text = "Import MetaData";
            this.chkMeta.UseVisualStyleBackColor = true;
            this.chkMeta.CheckedChanged += new System.EventHandler(this.chkMeta_CheckedChanged);
            // 
            // lblOptions
            // 
            this.lblOptions.AutoSize = true;
            this.lblOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOptions.Location = new System.Drawing.Point(10, 34);
            this.lblOptions.Name = "lblOptions";
            this.lblOptions.Size = new System.Drawing.Size(164, 16);
            this.lblOptions.TabIndex = 21;
            this.lblOptions.Text = "Select Import Options: ";
            // 
            // dGVCal
            // 
            this.dGVCal.AllowUserToAddRows = false;
            this.dGVCal.AllowUserToDeleteRows = false;
            this.dGVCal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVCal.Location = new System.Drawing.Point(550, 340);
            this.dGVCal.Name = "dGVCal";
            this.dGVCal.ReadOnly = true;
            this.dGVCal.Size = new System.Drawing.Size(227, 85);
            this.dGVCal.TabIndex = 22;
            this.dGVCal.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rdUpdate);
            this.panel1.Controls.Add(this.rdExport);
            this.panel1.Location = new System.Drawing.Point(198, 340);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 66);
            this.panel1.TabIndex = 23;
            // 
            // rdExport
            // 
            this.rdExport.AutoSize = true;
            this.rdExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdExport.Location = new System.Drawing.Point(16, 10);
            this.rdExport.Name = "rdExport";
            this.rdExport.Size = new System.Drawing.Size(269, 20);
            this.rdExport.TabIndex = 0;
            this.rdExport.TabStop = true;
            this.rdExport.Text = "Calculate Full Flowering and Export";
            this.rdExport.UseVisualStyleBackColor = true;
            // 
            // rdUpdate
            // 
            this.rdUpdate.AutoSize = true;
            this.rdUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdUpdate.Location = new System.Drawing.Point(16, 36);
            this.rdUpdate.Name = "rdUpdate";
            this.rdUpdate.Size = new System.Drawing.Size(301, 20);
            this.rdUpdate.TabIndex = 1;
            this.rdUpdate.TabStop = true;
            this.rdUpdate.Text = "Calculate Full Flowering and Update DB";
            this.rdUpdate.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 16);
            this.label1.TabIndex = 24;
            this.label1.Text = "SeperatorLine";
            // 
            // lblFull
            // 
            this.lblFull.AutoSize = true;
            this.lblFull.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFull.Location = new System.Drawing.Point(12, 363);
            this.lblFull.Name = "lblFull";
            this.lblFull.Size = new System.Drawing.Size(159, 16);
            this.lblFull.TabIndex = 25;
            this.lblFull.Text = "Full Flowering options";
            // 
            // FrmImportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 506);
            this.Controls.Add(this.lblFull);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dGVCal);
            this.Controls.Add(this.lblOptions);
            this.Controls.Add(this.chkMeta);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.lblDataFile);
            this.Controls.Add(this.pnlImport);
            this.Controls.Add(this.lblImport);
            this.Controls.Add(this.cmbFileCat);
            this.Controls.Add(this.lblTable);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "FrmImportData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Data";
            this.pnlImport.ResumeLayout(false);
            this.pnlImport.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVCal)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Label lblDataFile;
        private System.Windows.Forms.Panel pnlImport;
        private System.Windows.Forms.RadioButton rdbReplace;
        private System.Windows.Forms.RadioButton rdbIgnore;
        private System.Windows.Forms.Label lblImport;
        private System.Windows.Forms.ComboBox cmbFileCat;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.CheckBox chkMeta;
        private System.Windows.Forms.Label lblOptions;
        private System.Windows.Forms.DataGridView dGVCal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdUpdate;
        private System.Windows.Forms.RadioButton rdExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFull;
    }
}