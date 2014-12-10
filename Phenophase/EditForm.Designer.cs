namespace WindowsFormsApplication1
{
    partial class FrmEditData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditData));
            this.cmbEPLantID = new System.Windows.Forms.ComboBox();
            this.lblEPlantID = new System.Windows.Forms.Label();
            this.dtpEEnd = new System.Windows.Forms.DateTimePicker();
            this.lblEEDate = new System.Windows.Forms.Label();
            this.dtpEStart = new System.Windows.Forms.DateTimePicker();
            this.lblESDate = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.cmbEditTable = new System.Windows.Forms.ComboBox();
            this.lblEditTable = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dGVEdit = new System.Windows.Forms.DataGridView();
            this.btnExport = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbEPLantID
            // 
            this.cmbEPLantID.FormattingEnabled = true;
            this.cmbEPLantID.Location = new System.Drawing.Point(113, 67);
            this.cmbEPLantID.Margin = new System.Windows.Forms.Padding(4);
            this.cmbEPLantID.Name = "cmbEPLantID";
            this.cmbEPLantID.Size = new System.Drawing.Size(139, 24);
            this.cmbEPLantID.TabIndex = 22;
            this.cmbEPLantID.Text = "PlantID";
            this.cmbEPLantID.Visible = false;
            // 
            // lblEPlantID
            // 
            this.lblEPlantID.AutoSize = true;
            this.lblEPlantID.Location = new System.Drawing.Point(8, 69);
            this.lblEPlantID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEPlantID.Name = "lblEPlantID";
            this.lblEPlantID.Size = new System.Drawing.Size(70, 16);
            this.lblEPlantID.TabIndex = 21;
            this.lblEPlantID.Text = "Plant ID :";
            this.lblEPlantID.Visible = false;
            // 
            // dtpEEnd
            // 
            this.dtpEEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEEnd.Location = new System.Drawing.Point(609, 27);
            this.dtpEEnd.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEEnd.Name = "dtpEEnd";
            this.dtpEEnd.Size = new System.Drawing.Size(96, 22);
            this.dtpEEnd.TabIndex = 20;
            this.dtpEEnd.Visible = false;
            // 
            // lblEEDate
            // 
            this.lblEEDate.AutoSize = true;
            this.lblEEDate.Location = new System.Drawing.Point(521, 28);
            this.lblEEDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEEDate.Name = "lblEEDate";
            this.lblEEDate.Size = new System.Drawing.Size(80, 16);
            this.lblEEDate.TabIndex = 19;
            this.lblEEDate.Text = "End Date :";
            this.lblEEDate.Visible = false;
            // 
            // dtpEStart
            // 
            this.dtpEStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEStart.Location = new System.Drawing.Point(397, 27);
            this.dtpEStart.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEStart.Name = "dtpEStart";
            this.dtpEStart.Size = new System.Drawing.Size(96, 22);
            this.dtpEStart.TabIndex = 18;
            this.dtpEStart.Visible = false;
            this.dtpEStart.ValueChanged += new System.EventHandler(this.dtpEStart_ValueChanged);
            // 
            // lblESDate
            // 
            this.lblESDate.AutoSize = true;
            this.lblESDate.Location = new System.Drawing.Point(304, 28);
            this.lblESDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblESDate.Name = "lblESDate";
            this.lblESDate.Size = new System.Drawing.Size(85, 16);
            this.lblESDate.TabIndex = 17;
            this.lblESDate.Text = "Start Date :";
            this.lblESDate.Visible = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(549, 62);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 31);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "Clear Data";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cmbEditTable
            // 
            this.cmbEditTable.FormattingEnabled = true;
            this.cmbEditTable.Location = new System.Drawing.Point(113, 25);
            this.cmbEditTable.Margin = new System.Windows.Forms.Padding(4);
            this.cmbEditTable.Name = "cmbEditTable";
            this.cmbEditTable.Size = new System.Drawing.Size(162, 24);
            this.cmbEditTable.TabIndex = 15;
            this.cmbEditTable.Text = "Table";
            this.cmbEditTable.SelectedIndexChanged += new System.EventHandler(this.cmbEditTable_SelectedIndexChanged);
            // 
            // lblEditTable
            // 
            this.lblEditTable.AutoSize = true;
            this.lblEditTable.Location = new System.Drawing.Point(8, 28);
            this.lblEditTable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEditTable.Name = "lblEditTable";
            this.lblEditTable.Size = new System.Drawing.Size(113, 16);
            this.lblEditTable.TabIndex = 14;
            this.lblEditTable.Text = "Select Table :  ";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(423, 62);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(105, 31);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Update Data";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(307, 62);
            this.btnShow.Margin = new System.Windows.Forms.Padding(4);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(91, 31);
            this.btnShow.TabIndex = 12;
            this.btnShow.Text = "Show Data";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.cmbEditTable);
            this.groupBox1.Controls.Add(this.cmbEPLantID);
            this.groupBox1.Controls.Add(this.btnShow);
            this.groupBox1.Controls.Add(this.lblEPlantID);
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.dtpEEnd);
            this.groupBox1.Controls.Add(this.lblEditTable);
            this.groupBox1.Controls.Add(this.lblEEDate);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.dtpEStart);
            this.groupBox1.Controls.Add(this.lblESDate);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(812, 110);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Criteria";
            // 
            // dGVEdit
            // 
            this.dGVEdit.AllowUserToDeleteRows = false;
            this.dGVEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGVEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVEdit.Location = new System.Drawing.Point(12, 128);
            this.dGVEdit.Name = "dGVEdit";
            this.dGVEdit.Size = new System.Drawing.Size(812, 419);
            this.dGVEdit.TabIndex = 24;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(672, 62);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 31);
            this.btnExport.TabIndex = 16;
            this.btnExport.Text = "Export Data";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // FrmEditData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 558);
            this.Controls.Add(this.dGVEdit);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmEditData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Data";
            this.Load += new System.EventHandler(this.FrmEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbEPLantID;
        private System.Windows.Forms.Label lblEPlantID;
        private System.Windows.Forms.DateTimePicker dtpEEnd;
        private System.Windows.Forms.Label lblEEDate;
        private System.Windows.Forms.DateTimePicker dtpEStart;
        private System.Windows.Forms.Label lblESDate;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ComboBox cmbEditTable;
        private System.Windows.Forms.Label lblEditTable;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dGVEdit;
        private System.Windows.Forms.Button btnExport;
    }
}