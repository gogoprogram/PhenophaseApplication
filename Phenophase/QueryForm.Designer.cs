namespace WindowsFormsApplication1
{
    partial class FrmQuery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQuery));
            this.grpCriteria = new System.Windows.Forms.GroupBox();
            this.lblClimate = new System.Windows.Forms.Label();
            this.chkClimate = new System.Windows.Forms.CheckBox();
            this.lstSite = new System.Windows.Forms.ListBox();
            this.btnQuery3 = new System.Windows.Forms.Button();
            this.lstYear = new System.Windows.Forms.ListBox();
            this.btnQuery2 = new System.Windows.Forms.Button();
            this.btnQuery1 = new System.Windows.Forms.Button();
            this.lblYear = new System.Windows.Forms.Label();
            this.cmbPhTitle = new System.Windows.Forms.ComboBox();
            this.lblPhTitle = new System.Windows.Forms.Label();
            this.lblSite = new System.Windows.Forms.Label();
            this.lstSp = new System.Windows.Forms.ListBox();
            this.lblSp = new System.Windows.Forms.Label();
            this.cmbFg = new System.Windows.Forms.ComboBox();
            this.lblFg = new System.Windows.Forms.Label();
            this.dGVResult = new System.Windows.Forms.DataGridView();
            this.btnChart = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.pnlClimate = new System.Windows.Forms.Panel();
            this.lblClimateMsg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbVar2 = new System.Windows.Forms.ComboBox();
            this.cmbVar1 = new System.Windows.Forms.ComboBox();
            this.lblVar1 = new System.Windows.Forms.Label();
            this.grpCriteria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVResult)).BeginInit();
            this.pnlClimate.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCriteria
            // 
            this.grpCriteria.Controls.Add(this.lblClimate);
            this.grpCriteria.Controls.Add(this.chkClimate);
            this.grpCriteria.Controls.Add(this.lstSite);
            this.grpCriteria.Controls.Add(this.btnQuery3);
            this.grpCriteria.Controls.Add(this.lstYear);
            this.grpCriteria.Controls.Add(this.btnQuery2);
            this.grpCriteria.Controls.Add(this.btnQuery1);
            this.grpCriteria.Controls.Add(this.lblYear);
            this.grpCriteria.Controls.Add(this.cmbPhTitle);
            this.grpCriteria.Controls.Add(this.lblPhTitle);
            this.grpCriteria.Controls.Add(this.lblSite);
            this.grpCriteria.Controls.Add(this.lstSp);
            this.grpCriteria.Controls.Add(this.lblSp);
            this.grpCriteria.Controls.Add(this.cmbFg);
            this.grpCriteria.Controls.Add(this.lblFg);
            this.grpCriteria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCriteria.Location = new System.Drawing.Point(13, 13);
            this.grpCriteria.Name = "grpCriteria";
            this.grpCriteria.Size = new System.Drawing.Size(937, 221);
            this.grpCriteria.TabIndex = 0;
            this.grpCriteria.TabStop = false;
            this.grpCriteria.Text = "Search Criteria";
            // 
            // lblClimate
            // 
            this.lblClimate.Font = new System.Drawing.Font("Lucida Sans", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClimate.ForeColor = System.Drawing.Color.Maroon;
            this.lblClimate.Location = new System.Drawing.Point(709, 93);
            this.lblClimate.Name = "lblClimate";
            this.lblClimate.Size = new System.Drawing.Size(210, 76);
            this.lblClimate.TabIndex = 14;
            this.lblClimate.Text = "To compare with climate data, select the ClimateData checkbox after running a que" +
    "ry";
            // 
            // chkClimate
            // 
            this.chkClimate.AutoSize = true;
            this.chkClimate.Location = new System.Drawing.Point(712, 177);
            this.chkClimate.Name = "chkClimate";
            this.chkClimate.Size = new System.Drawing.Size(116, 20);
            this.chkClimate.TabIndex = 13;
            this.chkClimate.Text = "Climate Data";
            this.chkClimate.UseVisualStyleBackColor = true;
            this.chkClimate.CheckedChanged += new System.EventHandler(this.chkClimate_CheckedChanged);
            // 
            // lstSite
            // 
            this.lstSite.FormattingEnabled = true;
            this.lstSite.ItemHeight = 16;
            this.lstSite.Location = new System.Drawing.Point(160, 93);
            this.lstSite.Name = "lstSite";
            this.lstSite.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSite.Size = new System.Drawing.Size(110, 116);
            this.lstSite.TabIndex = 1;
            this.lstSite.SelectedIndexChanged += new System.EventHandler(this.lstSite_SelectedIndexChanged);
            // 
            // btnQuery3
            // 
            this.btnQuery3.Location = new System.Drawing.Point(545, 157);
            this.btnQuery3.Name = "btnQuery3";
            this.btnQuery3.Size = new System.Drawing.Size(125, 40);
            this.btnQuery3.TabIndex = 7;
            this.btnQuery3.Text = "Run Abundance Query";
            this.btnQuery3.UseVisualStyleBackColor = true;
            this.btnQuery3.Click += new System.EventHandler(this.btnQuery3_Click);
            // 
            // lstYear
            // 
            this.lstYear.FormattingEnabled = true;
            this.lstYear.ItemHeight = 16;
            this.lstYear.Location = new System.Drawing.Point(429, 19);
            this.lstYear.Name = "lstYear";
            this.lstYear.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstYear.Size = new System.Drawing.Size(91, 84);
            this.lstYear.TabIndex = 3;
            // 
            // btnQuery2
            // 
            this.btnQuery2.Location = new System.Drawing.Point(545, 93);
            this.btnQuery2.Name = "btnQuery2";
            this.btnQuery2.Size = new System.Drawing.Size(125, 40);
            this.btnQuery2.TabIndex = 6;
            this.btnQuery2.Text = "Run Status Query";
            this.btnQuery2.UseVisualStyleBackColor = true;
            this.btnQuery2.Click += new System.EventHandler(this.btnQuery2_Click);
            // 
            // btnQuery1
            // 
            this.btnQuery1.Location = new System.Drawing.Point(545, 22);
            this.btnQuery1.Name = "btnQuery1";
            this.btnQuery1.Size = new System.Drawing.Size(125, 40);
            this.btnQuery1.TabIndex = 5;
            this.btnQuery1.Text = "Run Event Query";
            this.btnQuery1.UseVisualStyleBackColor = true;
            this.btnQuery1.Click += new System.EventHandler(this.btnQuery1_Click);
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(340, 21);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(49, 16);
            this.lblYear.TabIndex = 11;
            this.lblYear.Text = "Year :";
            // 
            // cmbPhTitle
            // 
            this.cmbPhTitle.FormattingEnabled = true;
            this.cmbPhTitle.Location = new System.Drawing.Point(160, 55);
            this.cmbPhTitle.Name = "cmbPhTitle";
            this.cmbPhTitle.Size = new System.Drawing.Size(149, 24);
            this.cmbPhTitle.TabIndex = 2;
            // 
            // lblPhTitle
            // 
            this.lblPhTitle.AutoSize = true;
            this.lblPhTitle.Location = new System.Drawing.Point(7, 58);
            this.lblPhTitle.Name = "lblPhTitle";
            this.lblPhTitle.Size = new System.Drawing.Size(138, 16);
            this.lblPhTitle.TabIndex = 10;
            this.lblPhTitle.Text = "Phenophase Title :";
            // 
            // lblSite
            // 
            this.lblSite.AutoSize = true;
            this.lblSite.Location = new System.Drawing.Point(7, 99);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(43, 16);
            this.lblSite.TabIndex = 9;
            this.lblSite.Text = "Site :";
            // 
            // lstSp
            // 
            this.lstSp.FormattingEnabled = true;
            this.lstSp.ItemHeight = 16;
            this.lstSp.Location = new System.Drawing.Point(429, 125);
            this.lstSp.Name = "lstSp";
            this.lstSp.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSp.Size = new System.Drawing.Size(91, 84);
            this.lstSp.TabIndex = 4;
            // 
            // lblSp
            // 
            this.lblSp.AutoSize = true;
            this.lblSp.Location = new System.Drawing.Point(340, 125);
            this.lblSp.Name = "lblSp";
            this.lblSp.Size = new System.Drawing.Size(73, 16);
            this.lblSp.TabIndex = 12;
            this.lblSp.Text = "Species :";
            // 
            // cmbFg
            // 
            this.cmbFg.FormattingEnabled = true;
            this.cmbFg.Location = new System.Drawing.Point(160, 19);
            this.cmbFg.Name = "cmbFg";
            this.cmbFg.Size = new System.Drawing.Size(149, 24);
            this.cmbFg.TabIndex = 0;
            this.cmbFg.SelectedIndexChanged += new System.EventHandler(this.cmbFg_SelectedIndexChanged);
            // 
            // lblFg
            // 
            this.lblFg.AutoSize = true;
            this.lblFg.Location = new System.Drawing.Point(7, 22);
            this.lblFg.Name = "lblFg";
            this.lblFg.Size = new System.Drawing.Size(133, 16);
            this.lblFg.TabIndex = 8;
            this.lblFg.Text = "Functional Group :";
            // 
            // dGVResult
            // 
            this.dGVResult.AllowUserToAddRows = false;
            this.dGVResult.AllowUserToDeleteRows = false;
            this.dGVResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGVResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dGVResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dGVResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.dGVResult.Location = new System.Drawing.Point(13, 240);
            this.dGVResult.Name = "dGVResult";
            this.dGVResult.ReadOnly = true;
            this.dGVResult.Size = new System.Drawing.Size(684, 425);
            this.dGVResult.TabIndex = 1;
            this.dGVResult.DataSourceChanged += new System.EventHandler(this.dGVResult_DataSourceChanged);
            // 
            // btnChart
            // 
            this.btnChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChart.Location = new System.Drawing.Point(12, 671);
            this.btnChart.Name = "btnChart";
            this.btnChart.Size = new System.Drawing.Size(110, 30);
            this.btnChart.TabIndex = 2;
            this.btnChart.Text = "Draw Chart";
            this.btnChart.UseVisualStyleBackColor = true;
            this.btnChart.Click += new System.EventHandler(this.btnChart_Click);
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(159, 671);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(110, 30);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export Result";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // pnlClimate
            // 
            this.pnlClimate.Controls.Add(this.lblClimateMsg);
            this.pnlClimate.Controls.Add(this.label1);
            this.pnlClimate.Controls.Add(this.cmbVar2);
            this.pnlClimate.Controls.Add(this.cmbVar1);
            this.pnlClimate.Controls.Add(this.lblVar1);
            this.pnlClimate.Enabled = false;
            this.pnlClimate.Location = new System.Drawing.Point(715, 240);
            this.pnlClimate.Name = "pnlClimate";
            this.pnlClimate.Size = new System.Drawing.Size(235, 180);
            this.pnlClimate.TabIndex = 4;
            this.pnlClimate.Visible = false;
            // 
            // lblClimateMsg
            // 
            this.lblClimateMsg.Font = new System.Drawing.Font("Lucida Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClimateMsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblClimateMsg.Location = new System.Drawing.Point(14, 10);
            this.lblClimateMsg.Name = "lblClimateMsg";
            this.lblClimateMsg.Size = new System.Drawing.Size(176, 37);
            this.lblClimateMsg.TabIndex = 3;
            this.lblClimateMsg.Text = "Choose ClimateData Variables";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Variable Right Y-axis:";
            // 
            // cmbVar2
            // 
            this.cmbVar2.FormattingEnabled = true;
            this.cmbVar2.Location = new System.Drawing.Point(15, 141);
            this.cmbVar2.Name = "cmbVar2";
            this.cmbVar2.Size = new System.Drawing.Size(202, 21);
            this.cmbVar2.TabIndex = 1;
            // 
            // cmbVar1
            // 
            this.cmbVar1.FormattingEnabled = true;
            this.cmbVar1.Location = new System.Drawing.Point(15, 76);
            this.cmbVar1.Name = "cmbVar1";
            this.cmbVar1.Size = new System.Drawing.Size(202, 21);
            this.cmbVar1.TabIndex = 1;
            // 
            // lblVar1
            // 
            this.lblVar1.AutoSize = true;
            this.lblVar1.Font = new System.Drawing.Font("Lucida Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVar1.Location = new System.Drawing.Point(12, 56);
            this.lblVar1.Name = "lblVar1";
            this.lblVar1.Size = new System.Drawing.Size(162, 17);
            this.lblVar1.TabIndex = 0;
            this.lblVar1.Text = "Variable Left Y-axis:";
            // 
            // FrmQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 713);
            this.Controls.Add(this.pnlClimate);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnChart);
            this.Controls.Add(this.dGVResult);
            this.Controls.Add(this.grpCriteria);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Query Form";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.grpCriteria.ResumeLayout(false);
            this.grpCriteria.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVResult)).EndInit();
            this.pnlClimate.ResumeLayout(false);
            this.pnlClimate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCriteria;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.ComboBox cmbPhTitle;
        private System.Windows.Forms.Label lblPhTitle;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.ListBox lstSp;
        private System.Windows.Forms.Label lblSp;
        private System.Windows.Forms.ComboBox cmbFg;
        private System.Windows.Forms.Label lblFg;
        private System.Windows.Forms.Button btnQuery1;
        private System.Windows.Forms.DataGridView dGVResult;
        private System.Windows.Forms.Button btnQuery2;
        private System.Windows.Forms.ListBox lstYear;
        private System.Windows.Forms.Button btnQuery3;
        private System.Windows.Forms.ListBox lstSite;
        private System.Windows.Forms.Button btnChart;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblClimate;
        private System.Windows.Forms.CheckBox chkClimate;
        private System.Windows.Forms.Panel pnlClimate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbVar2;
        private System.Windows.Forms.ComboBox cmbVar1;
        private System.Windows.Forms.Label lblVar1;
        private System.Windows.Forms.Label lblClimateMsg;
    }
}