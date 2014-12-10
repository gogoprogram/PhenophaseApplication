using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

namespace WindowsFormsApplication1
{
    public partial class FrmEditData : Form
    {
        Helper h;
        MySqlDataAdapter da = null;

        /*------------------------------------*/
        private static FrmEditData FEDsInst = null;
        //-------------------------------------
        // Create a public static property that returns the state of the instance
        public static FrmEditData CheckInst
        {
            get
            {
                return FEDsInst;
            }
        }

        // Create a public static property that will create an instance of the form and return it
        public static FrmEditData CreateInst
        {
            get
            {
                if (FEDsInst == null)
                    FEDsInst = new FrmEditData();
                return FEDsInst;
            }
        }

        // Need to override the OnFormClosing event to set the Instance to null
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
                FEDsInst = null;
            }
        }

        //----------------------------------------------------------

        public FrmEditData()
        {
            InitializeComponent();
            h = new Helper("phenophaseDBConnection");
        }

        private void FrmEdit_Load(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder conStrB = new MySqlConnectionStringBuilder(h.get_ConnString());
            string database = conStrB.Database;
            string cond =  " WHERE table_schema = '" + database + "' AND table_name NOT LIKE 'vw_%' AND table_name != 'temp_table'";
            ArrayList tables = h.get_data("INFORMATION_SCHEMA.TABLES", "table_name", cond);
            h.populate_control(this, "cmbEditTable", tables);
        }

        private void cmbEditTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            string table = "";

            table = cmbEditTable.SelectedItem.ToString();

            if (table.Contains("_pheno") || table.Contains("plant_note") || table.Contains("photo_info"))
            {
                lblEPlantID.Visible = true;
                cmbEPLantID.Visible = true;
                lblEEDate.Visible = true;
                dtpEStart.Visible = true;
                lblESDate.Visible = true;
                dtpEEnd.Visible = true;
                Load_PlantIDs();
            }
            else if (table.Contains("site_note") || table.Contains("site_visit"))
            {
                lblEEDate.Visible = true;
                dtpEStart.Visible = true;
                lblESDate.Visible = true;
                dtpEEnd.Visible = true;
            }
            else
            {
                lblEPlantID.Visible = false;
                cmbEPLantID.Visible = false;
                lblEEDate.Visible = false;
                dtpEStart.Visible = false;
                lblESDate.Visible = false;
                dtpEEnd.Visible = false;
            }

            cmbEPLantID.SelectedIndex = -1;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dGVEdit.DataSource = null;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dGVEdit.DataSource == null)
            {
                MessageBox.Show("You must click show data button first", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable changes = ((DataTable)dGVEdit.DataSource).GetChanges();
            try
            {
                if (changes != null)
                {
                    MySqlCommandBuilder cmdb = new MySqlCommandBuilder(da);

                    da.UpdateCommand = cmdb.GetUpdateCommand();
                    //da.DeleteCommand = cmdb.GetDeleteCommand();
                    //da.InsertCommand = cmdb.GetInsertCommand();
                    da.Update(changes);
                    MessageBox.Show("Table data has been modified", "Edit Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dGVEdit.DataSource = null;
                }
                else
                    MessageBox.Show("No modification has been made to the table data", "Edit Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (MySqlException exp)
            {
                MessageBox.Show("DateGridView Error: " + exp.ToString(), "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            string cond = "";
            DateTime start_date, end_date;

            if (cmbEditTable.SelectedItem != null)
            {
                string table = cmbEditTable.SelectedItem.ToString();
                start_date = dtpEStart.Value.Date;
                end_date = dtpEEnd.Value.Date;

                if (table.Contains("_pheno") || table.Contains("plant_note") || table.Contains("photo_info"))
                {
                    cond = " WHERE DATE BETWEEN '" + start_date.ToString("yyyy-MM-dd") + "' AND '" + end_date.ToString("yyyy-MM-dd") + "'";
                    if (cmbEPLantID.SelectedItem != null)
                        cond = cond + " AND PLANT_ID = '" + cmbEPLantID.SelectedItem.ToString() + "'";
                }
                else if (table.Contains("site_note") || table.Contains("site_visit"))
                {
                    cond = " WHERE DATE BETWEEN '" + start_date.ToString("yyyy-MM-dd") + "' AND '" + end_date.ToString("yyyy-MM-dd") + "'";
                }
                dGVEdit.DataSource = null;
                Bind(table, cond);
            }
            else
                MessageBox.Show("You have to choose a table", "Selection ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dtpEStart_ValueChanged(object sender, EventArgs e)
        {
            dtpEEnd.Value = dtpEStart.Value.Date;
        }
        //------------------------------------------------------------------------

        private void Bind(string table_name, string condition)
        {
            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(h.get_ConnString());
                conn.Open();

                string sql = "SELECT * FROM " + table_name + condition;

                da = new MySqlDataAdapter(sql, conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                    dGVEdit.DataSource = dt;
                else
                    MessageBox.Show("No matching data has been found.", "No Match", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Resize the DataGridView columns to fit the newly loaded content.
                
                dGVEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
                DataGridViewCellStyle style = dGVEdit.ColumnHeadersDefaultCellStyle;
                style.Font = new Font(dGVEdit.Font, FontStyle.Bold);

            }
            catch (MySqlException exp)
            {
                MessageBox.Show("MySQL Error: " + exp.ToString(), "Database ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void Load_PlantIDs()
        {
            ArrayList plantIDs = h.get_data("focal_plant_info", "PLANT_ID", "");
            h.populate_control(this, "cmbEPLantID", plantIDs);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            h.export_grid_result(this, "dGVEdit");
        }

       
    }
}
