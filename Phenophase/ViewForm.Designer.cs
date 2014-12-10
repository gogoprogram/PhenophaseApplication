namespace WindowsFormsApplication1
{
    partial class FrmViewData
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmViewData));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstSite = new System.Windows.Forms.ListBox();
            this.lstPlant = new System.Windows.Forms.ListBox();
            this.btnClearData = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.lblVColumns = new System.Windows.Forms.Label();
            this.chkLstColumns = new System.Windows.Forms.CheckedListBox();
            this.lblVPlant = new System.Windows.Forms.Label();
            this.cmbVFG = new System.Windows.Forms.ComboBox();
            this.lblVFG = new System.Windows.Forms.Label();
            this.lblVSite = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblEDate = new System.Windows.Forms.Label();
            this.lblSDate = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox3.Controls.Add(this.lstSite);
            this.groupBox3.Controls.Add(this.lstPlant);
            this.groupBox3.Controls.Add(this.btnClearData);
            this.groupBox3.Controls.Add(this.btnExport);
            this.groupBox3.Controls.Add(this.btnView);
            this.groupBox3.Controls.Add(this.lblVColumns);
            this.groupBox3.Controls.Add(this.chkLstColumns);
            this.groupBox3.Controls.Add(this.lblVPlant);
            this.groupBox3.Controls.Add(this.cmbVFG);
            this.groupBox3.Controls.Add(this.lblVFG);
            this.groupBox3.Controls.Add(this.lblVSite);
            this.groupBox3.Controls.Add(this.dtpEnd);
            this.groupBox3.Controls.Add(this.dtpStart);
            this.groupBox3.Controls.Add(this.lblEDate);
            this.groupBox3.Controls.Add(this.lblSDate);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(906, 218);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Criteria";
            // 
            // lstSite
            // 
            this.lstSite.FormattingEnabled = true;
            this.lstSite.ItemHeight = 16;
            this.lstSite.Location = new System.Drawing.Point(116, 95);
            this.lstSite.Name = "lstSite";
            this.lstSite.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstSite.Size = new System.Drawing.Size(131, 116);
            this.lstSite.TabIndex = 13;
            // 
            // lstPlant
            // 
            this.lstPlant.FormattingEnabled = true;
            this.lstPlant.ItemHeight = 16;
            this.lstPlant.Location = new System.Drawing.Point(296, 77);
            this.lstPlant.Name = "lstPlant";
            this.lstPlant.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstPlant.Size = new System.Drawing.Size(181, 132);
            this.lstPlant.TabIndex = 12;
            // 
            // btnClearData
            // 
            this.btnClearData.Location = new System.Drawing.Point(760, 95);
            this.btnClearData.Name = "btnClearData";
            this.btnClearData.Size = new System.Drawing.Size(100, 35);
            this.btnClearData.TabIndex = 11;
            this.btnClearData.Text = "Clear Data";
            this.btnClearData.UseVisualStyleBackColor = true;
            this.btnClearData.Click += new System.EventHandler(this.btnClearData_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(760, 167);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 35);
            this.btnExport.TabIndex = 10;
            this.btnExport.Text = "Export Data";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(760, 26);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(100, 35);
            this.btnView.TabIndex = 2;
            this.btnView.Text = "Show Data";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // lblVColumns
            // 
            this.lblVColumns.AutoSize = true;
            this.lblVColumns.Location = new System.Drawing.Point(511, 26);
            this.lblVColumns.Name = "lblVColumns";
            this.lblVColumns.Size = new System.Drawing.Size(156, 16);
            this.lblVColumns.TabIndex = 9;
            this.lblVColumns.Text = "Select View Columns:";
            // 
            // chkLstColumns
            // 
            this.chkLstColumns.FormattingEnabled = true;
            this.chkLstColumns.Location = new System.Drawing.Point(514, 51);
            this.chkLstColumns.Name = "chkLstColumns";
            this.chkLstColumns.Size = new System.Drawing.Size(204, 157);
            this.chkLstColumns.TabIndex = 8;
            // 
            // lblVPlant
            // 
            this.lblVPlant.AutoSize = true;
            this.lblVPlant.Location = new System.Drawing.Point(293, 56);
            this.lblVPlant.Name = "lblVPlant";
            this.lblVPlant.Size = new System.Drawing.Size(66, 16);
            this.lblVPlant.TabIndex = 7;
            this.lblVPlant.Text = "Plant ID:";
            // 
            // cmbVFG
            // 
            this.cmbVFG.FormattingEnabled = true;
            this.cmbVFG.Location = new System.Drawing.Point(335, 21);
            this.cmbVFG.Name = "cmbVFG";
            this.cmbVFG.Size = new System.Drawing.Size(142, 24);
            this.cmbVFG.TabIndex = 5;
            this.cmbVFG.SelectedIndexChanged += new System.EventHandler(this.cmbVFG_SelectedIndexChanged);
            // 
            // lblVFG
            // 
            this.lblVFG.AutoSize = true;
            this.lblVFG.Location = new System.Drawing.Point(293, 26);
            this.lblVFG.Name = "lblVFG";
            this.lblVFG.Size = new System.Drawing.Size(32, 16);
            this.lblVFG.TabIndex = 4;
            this.lblVFG.Text = "FG:";
            // 
            // lblVSite
            // 
            this.lblVSite.AutoSize = true;
            this.lblVSite.Location = new System.Drawing.Point(16, 95);
            this.lblVSite.Name = "lblVSite";
            this.lblVSite.Size = new System.Drawing.Size(39, 16);
            this.lblVSite.TabIndex = 4;
            this.lblVSite.Text = "Site:";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(116, 56);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(131, 22);
            this.dtpEnd.TabIndex = 2;
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(116, 21);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(131, 22);
            this.dtpStart.TabIndex = 2;
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            // 
            // lblEDate
            // 
            this.lblEDate.AutoSize = true;
            this.lblEDate.Location = new System.Drawing.Point(16, 56);
            this.lblEDate.Name = "lblEDate";
            this.lblEDate.Size = new System.Drawing.Size(76, 16);
            this.lblEDate.TabIndex = 1;
            this.lblEDate.Text = "End Date:";
            // 
            // lblSDate
            // 
            this.lblSDate.AutoSize = true;
            this.lblSDate.Location = new System.Drawing.Point(16, 24);
            this.lblSDate.Name = "lblSDate";
            this.lblSDate.Size = new System.Drawing.Size(85, 16);
            this.lblSDate.TabIndex = 0;
            this.lblSDate.Text = "Start Date: ";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 246);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(905, 365);
            this.dataGridView1.TabIndex = 2;
            // 
            // FrmViewData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(930, 623);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmViewData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Data";
            this.Load += new System.EventHandler(this.FrmView_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstSite;
        private System.Windows.Forms.ListBox lstPlant;
        private System.Windows.Forms.Button btnClearData;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label lblVColumns;
        private System.Windows.Forms.CheckedListBox chkLstColumns;
        private System.Windows.Forms.Label lblVPlant;
        private System.Windows.Forms.ComboBox cmbVFG;
        private System.Windows.Forms.Label lblVFG;
        private System.Windows.Forms.Label lblVSite;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblEDate;
        private System.Windows.Forms.Label lblSDate;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}