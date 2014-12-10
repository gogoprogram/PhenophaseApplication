using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public partial class FrmImportData : Form
    {
        Helper h;
        string replaceOrignore = "INSERT IGNORE";
        string[] metadata_files = { "Pheno-DomainTime Metadata", "Pheno-Attribute Metadata", "Table-Attribute Metadata" };
        string[] realdata_files = { "Focal Plant Info", "Site Info", "SiteVisit Info", "Species Info", "Phenophase Info", "Observer Info", 
                                      "Contact Info", "Pheno-Title Info", "Plant-Death Info" };


        /*------------------------------------*/
        private static FrmImportData FMDsInst = null;
        //-------------------------------------
        // Create a public static property that returns the state of the instance
        public static FrmImportData CheckInst
        {
            get
            { 
                return FMDsInst;
            }
        }

        // Create a public static property that will create an instance of the form and return it
        public static FrmImportData CreateInst
        {
            get
            {
                if (FMDsInst == null)
                    FMDsInst = new FrmImportData();
                return FMDsInst;
            }
        }

        // Need to override the OnFormClosing event to set the Instance to null
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
                FMDsInst = null;
            }
        }

        //----------------------------------------------------------

        public FrmImportData()
        {
            InitializeComponent();
            h = new Helper("phenophaseDBConnection");
            /*Create a line separator*/
            label1.AutoSize = false;
            label1.Height = 2;
            label1.Width = 750;
            label1.BorderStyle = BorderStyle.Fixed3D;
            //-------------------------------
            cmbFileCat.DataSource = realdata_files;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string filter = "csv files (*.csv)|*.csv";
            string ctrlName = "txtFile";

            h.openDialog(this, filter, ctrlName);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (txtFile.Text.Equals(""))
            {
                MessageBox.Show("File has not been chosen!", "Selection ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbFileCat.SelectedItem == null)
            {
                MessageBox.Show("File Category has not been chosen!", "Selection ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string dataType = cmbFileCat.SelectedItem.ToString();
            string tablePrefix = dataType.Substring(0, dataType.IndexOf(" "));

            switch (tablePrefix.ToLower())
            {
                case "focal":
                    ImportPlants(txtFile.Text, "focal_plant_info");
                    break;
                case "observer":
                    ImportObserver(txtFile.Text, "observer_info");
                    break;
                case "contact":
                    ImportContact(txtFile.Text, "contact_info");
                    break;
                case "pheno-title":
                    ImportPhenoTitles(txtFile.Text, "pheno_title_info");
                    break;
                case "plant-death":
                    ImportPlantDeath(txtFile.Text, "plant_death_info");
                    break;
                case "site":
                    ImportSites(txtFile.Text, "site_info");
                    break;
                case "sitevisit":
                    ImportSiteVisit(txtFile.Text, "site_note");
                    break;
                case "species":
                    ImportSpecies(txtFile.Text, "species_info");
                    break;
                case "phenophase":
                    ImportPhenophase(txtFile.Text);
                    break;
                //----------------------------
                case "pheno-attribute":
                    ImportAttributeMeta(txtFile.Text,"pheno_metadata");
                    break;
                case "pheno-domaintime":
                    ImportDomainTime(txtFile.Text, "pheno_domain_time");
                    break;
                case "table-attribute":
                    ImportTableAttribute(txtFile.Text, "table_attribute");
                    break;
                default:
                    MessageBox.Show("You did not choose a valid File Category!", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            
        }

        private void rdbIgnore_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbIgnore.Checked == true)
                replaceOrignore = "INSERT IGNORE";
        }

        private void rdbReplace_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbReplace.Checked == true)
                replaceOrignore = "REPLACE INTO";
        }

        private void chkMeta_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMeta.Checked)
            {
                cmbFileCat.DataSource = null;
                cmbFileCat.DataSource = metadata_files;
            }
            else
            {
                cmbFileCat.DataSource = null;
                cmbFileCat.DataSource = realdata_files;
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            string[] fgtables = { "ds", "es", "pg", "su" };
            string[] ufields = { "DS_09", "BE_05", "GR_09", "CA_05" };
            string[] gfields = { "DS_209", "BE_205", "GR_209", "CA_205" };
            string[] col_names = { "PLANT_ID", "YEAR(DATE)", "MAXFO" };
            int[] col_types = { 0, 1, 1 };

            string get_qr1 = "SELECT PLANT_ID, YEAR(DATE), MAX(";
            string get_qr2 = ") FROM ";
            string get_qr3 = " WHERE (";
            string get_qr4 = ") != -99 AND (";
            string get_qr5 = ") != 0 GROUP BY PLANT_ID, YEAR(DATE)";
            string cmdText = "", plantID = "", updText = "";
            int year, value, num_update_rows = 0;

            MySqlCommand getcmd = new MySqlCommand();
            DataTable dt = null;
            ArrayList updateQueries = new ArrayList();

            for (int i = 0; i < fgtables.Length; i++)
            {
                cmdText = get_qr1 + gfields[i] + get_qr2 + fgtables[i] + "_pheno" + get_qr3 + gfields[i] + get_qr4 + gfields[i] + get_qr5;
                getcmd.CommandText = cmdText;

                /*FOR DATAGRIDVIEW*/
                string[] column_names = { "FG", "DATE", "PLANT_ID", ufields[i], gfields[i] };

                for (int k = 0; k < column_names.Length; k++)
                {
                    DataGridViewColumn column = new DataGridViewColumn();
                    DataGridViewCell cell = new DataGridViewTextBoxCell(); //Specify which type of cell in this column
                    column.CellTemplate = cell;
                    column.Name = column_names[k];
                    dGVCal.Columns.Add(column);
                }
                //-----------------------------
                dt = h.get_table_wCMD(getcmd, col_names, col_types);

                toolStripProgressBar1.Step = 1;
                toolStripProgressBar1.Minimum = 0;
                toolStripProgressBar1.Maximum = dt.Rows.Count;

                h.open_DBconnection();

                foreach (DataRow row in dt.Rows)
                {
                    plantID = Convert.ToString(row[0]);
                    year = Convert.ToInt16(row[1]);
                    value = Convert.ToInt16(row[2]);

                    if (rdExport.Checked)
                    {
                        Insert_to_GridView(fgtables[i], plantID, year, ufields[i], gfields[i], value);
                    }
                    if (rdUpdate.Checked)
                    {
                        updText = "UPDATE " + fgtables[i] + "_pheno" + " SET " + ufields[i] + " = 1 WHERE PLANT_ID = '" + plantID + "' AND "
                           + "YEAR(DATE) = " + year + " AND " + ufields[i] + " = -99" + " AND " + gfields[i] + " = " + value;

                        updateQueries.Add(updText);

                        updText = "UPDATE " + fgtables[i] + "_pheno" + " SET " + ufields[i] + " = 0 WHERE PLANT_ID = '" + plantID + "' AND "
                           + "YEAR(DATE) = " + year + " AND " + ufields[i] + " = -99" + " AND " + gfields[i] + " != " + value;

                        updateQueries.Add(updText);
                    }

                    toolStripProgressBar1.PerformStep();
                }
                toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;

                if (rdUpdate.Checked)
                {
                    num_update_rows += h.update_data(updateQueries);
                    updateQueries.Clear();
                }

                h.close_DBconnection();

                /*FOR DATAGRIDVIEW*/
                if (rdExport.Checked)
                {
                    MessageBox.Show("Exporting data for functional group: " + fgtables[i] + ". You have to enter a file name", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    h.export_grid_result(this, "dGVCal");
                }

                dGVCal.Rows.Clear();
                dGVCal.Columns.Clear();
                //-----------------------

            }
            MessageBox.Show(num_update_rows + " records have been updated", "Completed AutoCalculation", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void Insert_to_GridView(string fg, string plantid, int year, string ufield, string gfield, int gvalue)
        {
            string[] col_names = { "DATE", "PLANT_ID", "PFO" };
            int[] col_types = { 3, 0, 1 };
            DataTable tmp_table = null;
            MySqlCommand tmp_cmd = new MySqlCommand();

            string temp_sql = "SELECT DATE, PLANT_ID, " + gfield + " FROM " + fg + "_pheno" + " WHERE PLANT_ID = '" + plantid + "' AND " + "YEAR(DATE) = " + year;
            tmp_cmd.CommandText = temp_sql;

            tmp_table = h.get_table_wCMD(tmp_cmd, col_names, col_types);

            foreach (DataRow row in tmp_table.Rows)
            {
                int rowindex = dGVCal.Rows.Add();
                dGVCal.Rows[rowindex].Cells[0].Value = fg;
                dGVCal.Rows[rowindex].Cells[1].Value = ((DateTime)row[0]).ToString("MM-dd-yyyy");
                dGVCal.Rows[rowindex].Cells[2].Value = Convert.ToString(row[1]);

                int val = Convert.ToInt16(row[2]);
                if (val == gvalue)
                {
                    dGVCal.Rows[rowindex].Cells[3].Value = 1;
                }
                else
                {
                    dGVCal.Rows[rowindex].Cells[3].Value = 0;
                }
                dGVCal.Rows[rowindex].Cells[4].Value = val;
            }

        }

        /*----------------------IMPORT FUNCTIONS-------------------------*/
        /*Completed*/
        private void ImportContact(string fileAddress, string tableName)
        {
            int i = 0, rows_inserted = 0;
            string contactID = "", contactName = "", contactAddress = "", contactEmail = "", contactPhone = "", contactEmployer = "", dataProvided = "", sql = "";

            ArrayList sqlcommands = new ArrayList();

            //-----------------PROGRESS BAR setup---------------------------------
            var lines = File.ReadLines(fileAddress);
            int TotalLines = lines.Count();
            toolStripProgressBar1.Step = 1;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = TotalLines;
            //---------------------------------------------------------
            h.open_DBconnection();

            foreach (string line in File.ReadLines(@fileAddress))
            {
                i++;
                toolStripProgressBar1.PerformStep();
                if (i == 1) continue; //skip the first line

                string data = line;
                //data = line.Replace(" ", "");
                string[] values = data.Split(',');

                if (values.Length == 0 || values.Length == 1)
                {
                    continue;
                }
                if (values.Length != 7)
                {
                    MessageBox.Show("Record No: " + i + ". Contact Data file is not in correct format. " + values.Length + " values found", "File ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                //--------------------------------------------
                contactID = values[0].Trim();

                if (string.IsNullOrEmpty(values[1]))
                    contactName = "NULL";
                else
                    contactName = "'" + values[1].Trim() + "'";


                if (string.IsNullOrEmpty(values[2]))
                    contactAddress = "NULL";
                else
                    contactAddress = "'" + values[2].Trim() + "'";

                if (string.IsNullOrEmpty(values[3]))
                    contactEmail = "NULL";
                else
                    contactEmail = "'" + values[3].Trim() + "'";

                if (string.IsNullOrEmpty(values[4]))
                    contactPhone = "NULL";
                else
                    contactPhone = "'" + values[4].Trim() + "'";

                if (string.IsNullOrEmpty(values[5]))
                    contactEmployer = "NULL";
                else
                    contactEmployer = "'" + values[5].Trim() + "'";

                if (string.IsNullOrEmpty(values[6]))
                    dataProvided = "NULL";
                else
                    dataProvided = "'" + values[6].Trim() + "'";

                sqlcommands.Clear();

                sql = replaceOrignore + " " + tableName;

                sql = sql + " VALUES('" + contactID + "'," + contactName + "," + contactAddress + "," + contactEmail + "," + contactPhone +
                                 "," + contactEmployer + "," + dataProvided + ")";
                sqlcommands.Add(sql);

                //--------------------------------------------
                int success = h.insert_data(sqlcommands);
                if (success >= 1)
                    rows_inserted++;

            } //end of for each

            MessageBox.Show((i - 1) + " records have been read from the data file. " + rows_inserted + " rows have been effected in the database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;
            h.close_DBconnection();

        }
        
        /*Completed*/
        private void ImportPlantDeath(string fileAddress, string tableName)
        {
            int i = 0, rows_inserted = 0;
            string plantid = "", deathreason = "", sql = "";

            ArrayList sqlcommands = new ArrayList();

            DateTime date;

            //-----------------PROGRESS BAR setup---------------------------------
            var lines = File.ReadLines(fileAddress);
            int TotalLines = lines.Count();
            toolStripProgressBar1.Step = 1;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = TotalLines;
            //---------------------------------------------------------
            h.open_DBconnection();

            foreach (string line in File.ReadLines(@fileAddress))
            {
                i++;
                toolStripProgressBar1.PerformStep();
                if (i == 1) continue; //skip the first line

                string data = line;
                //data = line.Replace(" ", "");
                string[] values = data.Split(',');

                if (values.Length == 0 || values.Length == 1)
                {
                    continue;
                }
                if (values.Length != 3)
                {
                    MessageBox.Show("Record No: " + i + " .Plant-Death Data file is not in correct format. " + values.Length + " values found", "File ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                //--------------------------------------------
                plantid = values[0].Trim();
                date = DateTime.Parse(values[1].Trim());
                deathreason = values[2].Trim();

                sqlcommands.Clear();

                sql = replaceOrignore + " " + tableName;
                
                sql = sql + " VALUES('"+ plantid + "','" + date.ToString("yyyy-MM-dd") + "','" +deathreason + "')";

                sqlcommands.Add(sql);
                                
                //------------------------------------------------
                int success = h.insert_data(sqlcommands);
                if (success >= 1)
                    rows_inserted++;

            } //end of for each

            MessageBox.Show((i - 1) + " records have been read from the data file. " + rows_inserted + " rows have been effected in the database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;

            h.close_DBconnection();
        }

        /*Completed*/
        private void ImportAttributeMeta(string fileAddress, string tableName)
        {
            int i = 0, rows_inserted = 0, att_id;
            string att_name = "", att_type = "", att_def = "", att_null = "", att_desc = "";
            string sql= "";

            ArrayList sqlcommands = new ArrayList();
            //-----------------PROGRESS BAR setup----------------------
            var lines = File.ReadLines(fileAddress);
            int TotalLines = lines.Count();
            toolStripProgressBar1.Step = 1;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = TotalLines;
            //---------------------------------------------------------
            h.open_DBconnection();

            foreach (string line in File.ReadLines(@fileAddress))
            {
                i++;
                toolStripProgressBar1.PerformStep();

                if (i == 1) continue; //skip the first line

                string data = line;
                //data = line.Replace(" ", "");
                string[] values = data.Split(',');

                if (values.Length == 0 || values.Length == 1)
                {
                    continue;
                }
                if (values.Length != 6)
                {
                    MessageBox.Show("Attribute MetaData file is not in correct format. " + values.Length + " values found", "File ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                //--------------------------------------------
                att_id = int.Parse(values[0].Trim());
                att_name = values[1].Trim();
                att_def = values[2].Trim();
                if (att_def.Contains("'"))
                    att_def = att_def.Replace("'", @"\'");
                att_type = values[3].Trim();
                att_null = values[4].Trim();
                att_desc = values[5].Trim();
                if(att_desc.Contains("'"))
                    att_desc = att_desc.Replace("'", @"\'");
               
                //----------------------------------------------
                sql = replaceOrignore + " " + tableName;

                sql = sql + " VALUES(" + att_id + ",'" + att_name + "','" + att_def + "','" + att_type + "','" + att_null + "','" + att_desc + "')";

                sqlcommands.Clear();
                sqlcommands.Add(sql);

                int success = h.insert_data(sqlcommands);
                if (success >= 1)
                    rows_inserted++;

            } //end of for each

            MessageBox.Show((i - 1) + " records have been read from the data file. " + rows_inserted + " rows have been effected in the database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;
            h.close_DBconnection();
        }

        /*Completed*/
        private void ImportDomainTime(string fileAddress, string tableName)
        {
            int i = 0, rows_inserted = 0, att_id;
            string data_domain = "", domain_notes = "", domain_desc = "", ph_stdate = "", ph_endate = "";
            string sql = "";

            ArrayList sqlcommands = new ArrayList();
            //-----------------PROGRESS BAR setup----------------------
            var lines = File.ReadLines(fileAddress);
            int TotalLines = lines.Count();
            toolStripProgressBar1.Step = 1;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = TotalLines;
            //---------------------------------------------------------
            h.open_DBconnection();

            foreach (string line in File.ReadLines(@fileAddress))
            {
                i++;
                toolStripProgressBar1.PerformStep();

                if (i == 1) continue; //skip the first line

                string data = line;
                //data = line.Replace(" ", "");
                string[] values = data.Split(',');

                if (values.Length == 0 || values.Length == 1)
                {
                    continue;
                }
                if (values.Length != 6)
                {
                    MessageBox.Show("DomainTime file is not in correct format. " + values.Length + " values found", "File ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                //--------------------------------------------
                att_id = int.Parse(values[0].Trim());
                data_domain = values[1].Trim();
                domain_desc = values[2].Trim();
                domain_notes = values[3].Trim();
                if (domain_notes.Contains("'"))
                    domain_notes = domain_notes.Replace("'", @"\'");


                if(string.IsNullOrEmpty(values[4].Trim()))
                    ph_stdate = null;
                else
                    ph_stdate = Convert.ToDateTime(values[4].Trim()).ToString("yyyy-MM-dd");

                if (string.IsNullOrEmpty(values[5].Trim()))
                    ph_endate = null;
                else
                    ph_endate = Convert.ToDateTime(values[5].Trim()).ToString("yyyy-MM-dd");

                //----------------------------------------------
                sql = replaceOrignore + " " + tableName;

                sql = sql + " VALUES(" + att_id + ",";
                if (ph_stdate == null)
                    sql = sql + "NULL" + ",";
                else
                    sql = sql + "'"+ ph_stdate + "',";

                if (ph_endate == null)
                    sql = sql + "NULL" + ",";
                else
                    sql = sql + "'" + ph_endate + "',";

                sql = sql + "'" + data_domain + "','" + domain_desc + "','" + domain_notes + "')";

                sqlcommands.Clear();
                sqlcommands.Add(sql);

                int success = h.insert_data(sqlcommands);
                if (success >= 1)
                    rows_inserted++;

            } //end of for each

            MessageBox.Show((i - 1) + " records have been read from the data file. " + rows_inserted + " rows have been effected in the database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;
            h.close_DBconnection();
        }

        /*Completed*/
        private void ImportTableAttribute(string fileAddress, string tableName)
        {
            int i = 0, rows_inserted = 0, att_id;
            string table_name = "";
            string sql = "";

            ArrayList sqlcommands = new ArrayList();
            //-----------------PROGRESS BAR setup----------------------
            var lines = File.ReadLines(fileAddress);
            int TotalLines = lines.Count();
            toolStripProgressBar1.Step = 1;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = TotalLines;
            //---------------------------------------------------------
            h.open_DBconnection();

            foreach (string line in File.ReadLines(@fileAddress))
            {
                i++;
                toolStripProgressBar1.PerformStep();

                if (i == 1) continue; //skip the first line

                string data = line;
                //data = line.Replace(" ", "");
                string[] values = data.Split(',');

                if (values.Length != 2)
                {
                    MessageBox.Show("Table-Attribute Data file is not in correct format. " + values.Length + " values found", "File ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                //--------------------------------------------
                att_id = int.Parse(values[0].Trim());
                table_name = values[1].Trim();
                

                //----------------------------------------------
                sql = replaceOrignore + " " + tableName;

                sql = sql + " VALUES(" + att_id + ",'" + table_name + "')";

                sqlcommands.Clear();
                sqlcommands.Add(sql);

                int success = h.insert_data(sqlcommands);
                if (success >= 1)
                    rows_inserted++;

            } //end of for each

            MessageBox.Show((i - 1) + " records have been read from the data file. " + rows_inserted + " rows have been effected in the database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;
            h.close_DBconnection();
        }

        /*Completed*/
        private void ImportObserver(string fileAddress, string tableName)
        {
            int i = 0, rows_inserted = 0;
            string observerID = "", observerName = "", observerAddress = "", observerEmail = "", observerPhone = "", sql = "";

            ArrayList sqlcommands = new ArrayList();

            //-----------------PROGRESS BAR setup---------------------------------
            var lines = File.ReadLines(fileAddress);
            int TotalLines = lines.Count();
            toolStripProgressBar1.Step = 1;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = TotalLines;
            //---------------------------------------------------------
            h.open_DBconnection();

            foreach (string line in File.ReadLines(@fileAddress))
            {
                i++;
                toolStripProgressBar1.PerformStep();
                if (i == 1) continue; //skip the first line

                string data = line;
                //data = line.Replace(" ", "");
                string[] values = data.Split(',');

                if (values.Length == 0 || values.Length == 1)
                {
                    continue;
                }
                if (values.Length != 5)
                {
                    MessageBox.Show("Record No: " + i + ". Observer Data file is not in correct format. " + values.Length + " values found", "File ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                //--------------------------------------------
                observerID = values[0].Trim();

                if (string.IsNullOrEmpty(values[1]))
                    observerName = "NULL";
                else
                    observerName = "'" + values[1].Trim() + "'";


                if (string.IsNullOrEmpty(values[2]))
                    observerAddress = "NULL";
                else
                    observerAddress = "'" + values[2].Trim() + "'";

                if (string.IsNullOrEmpty(values[3]))
                    observerEmail = "NULL";
                else
                    observerEmail = "'" + values[3].Trim() + "'";

                if (string.IsNullOrEmpty(values[4]))
                    observerPhone = "NULL";
                else
                    observerPhone = "'" + values[4].Trim() + "'";

                sqlcommands.Clear();

                sql = replaceOrignore + " " + tableName;

                sql = sql + " VALUES('" + observerID + "'," + observerName + "," + observerAddress + "," + observerEmail + "," + observerPhone + ")";
                sqlcommands.Add(sql);

                //--------------------------------------------
                int success = h.insert_data(sqlcommands);
                if (success >= 1)
                    rows_inserted++;

            } //end of for each

            MessageBox.Show((i - 1) + " records have been read from the data file. " + rows_inserted + " rows have been effected in the database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;
            h.close_DBconnection();
        }

        /*Completed*/
        private void ImportSiteVisit(string fileAddress, string tableName)
        {
            int i = 0, rows_inserted = 0;
            string siteCode = "", observers = "", sitenotes = "", sql = "";

            ArrayList sqlcommands = new ArrayList();

            DateTime date;

            //-----------------PROGRESS BAR setup---------------------------------
            var lines = File.ReadLines(fileAddress);
            int TotalLines = lines.Count();
            toolStripProgressBar1.Step = 1;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = TotalLines;
            //---------------------------------------------------------
            h.open_DBconnection();

            foreach (string line in File.ReadLines(@fileAddress))
            {
                i++;
                toolStripProgressBar1.PerformStep();
                if (i == 1) continue; //skip the first line

                string data = line;
                //data = line.Replace(" ", "");
                string[] values = data.Split(',');

                if (values.Length == 0 || values.Length == 1)
                {
                    continue;
                }
                if (values.Length != 5)
                {
                    MessageBox.Show("Record No: " + i + " .SiteVisit Data file is not in correct format. " + values.Length + " values found", "File ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                //--------------------------------------------
                date = DateTime.Parse(values[0].Trim());
                //skip day of year
                siteCode = values[2].Trim();

                observers = values[3];

                sitenotes = values[4].Trim();

                sqlcommands.Clear();
                //----------------DOY------------------------------
                sql = "INSERT IGNORE date_doy(DATE, DAY_OF_YEAR) VALUES('" + date.ToString("yyyy-MM-dd") + "'," + date.DayOfYear + ")";

                sqlcommands.Add(sql);
                //----------------OBSERVER-------------------------
                string[] obs = observers.Split(';');
                foreach (string observer in obs)
                {
                    sql = "INSERT IGNORE observer_info(OBSERVER_ID) VALUES('" + observer.Trim() + "')";

                    sqlcommands.Add(sql);
                }
                //--------------SITE VISIT-------------------------
                foreach (string observer in obs)
                {
                    sql = replaceOrignore + " site_visit(DATE, SITE_CODE, OBSERVER_ID) VALUES('" + date.ToString("yyyy-MM-dd") +
                        "','" + siteCode + "','" + observer.Trim() + "')";

                    sqlcommands.Add(sql);
                }
                //-----------SITE NOTES----------------------------
                if (!sitenotes.Equals(""))
                {
                    sql = replaceOrignore + " " + tableName;

                    sql = sql + " VALUES('" + date.ToString("yyyy-MM-dd") + "','" + siteCode + "','" + sitenotes + "')";

                    sqlcommands.Add(sql);
                }
                //------------------------------------------------
                int success = h.insert_data(sqlcommands);
                if (success >= 1)
                    rows_inserted++;

            } //end of for each

            MessageBox.Show((i - 1) + " records have been read from the data file. " + rows_inserted + " rows have been effected in the database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;

            h.close_DBconnection();
        }

        /*Completed*/
        private void ImportSites(string fileAddress, string tableName)
        {
            int i = 0, utmx, utmy, rows_inserted = 0;
            string site_Code = "", siteName = "", soilType = "", ecolSite = "", sql = "";
            double latDD, longDD;

            ArrayList sqlcommands = new ArrayList();
            //-----------------PROGRESS BAR setup---------------------
            var lines = File.ReadLines(fileAddress);
            int TotalLines = lines.Count();
            toolStripProgressBar1.Step = 1;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = TotalLines;
            //---------------------------------------------------------
            h.open_DBconnection();

            foreach (string line in File.ReadLines(@fileAddress))
            {
                i++;
                toolStripProgressBar1.PerformStep();
                if (i == 1) continue; //skip the first line

                string data = line;
                //data = line.Replace(" ", "");
                string[] values = data.Split(',');

                if (values.Length == 0 || values.Length == 1)
                {
                    continue;
                }
                if (values.Length != 8)
                {
                    MessageBox.Show("Site Data file is not in correct format. " + values.Length + " values found", "File ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                //--------------------------------------------
                site_Code = values[0].Trim();
                siteName = values[1].Trim();

                if (string.IsNullOrEmpty(values[2]))
                    soilType = "NULL";
                else
                    soilType = "'" + values[2].Trim() + "'";

                if (values[3].Equals(""))
                    utmx = -99;
                else
                    utmx = int.Parse(values[3].Trim());

                if (values[4].Equals(""))
                    utmy = -99;
                else
                    utmy = int.Parse(values[4].Trim());

                if (values[5].Equals(""))
                    latDD = -99.99;
                else
                    latDD = double.Parse(values[5].Trim());

                if (values[6].Equals(""))
                    longDD = -99.99;
                else
                    longDD = double.Parse(values[6].Trim());

                if (string.IsNullOrEmpty(values[7]))
                    ecolSite = "NULL";
                else
                    ecolSite = "'" + values[7] + "'";
                //----------------------------------------------
                sql = replaceOrignore + " " + tableName;

                sql = sql + " VALUES('" + site_Code + "','" + siteName + "'," + soilType + "," + utmx + "," + utmy + "," +
                     latDD + "," + longDD + "," + ecolSite + ")";

                sqlcommands.Clear();
                sqlcommands.Add(sql);

                int success = h.insert_data(sqlcommands);
                if (success >= 1)
                    rows_inserted++;

            } //end of for each

            MessageBox.Show((i - 1) + " records have been read from the data file. " + rows_inserted + " rows have been effected in the database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;
            h.close_DBconnection();
        }

        /*Completed*/
        private void ImportSpecies(string fileAddress, string tableName)
        {
            int i = 0, rows_inserted = 0;
            string sppCode = "", funcGrp = "", funcGrpCode = "", symbol = "", growthHabit = "", subCode = "", scientific = "", common = "";
            string genus = "", family = "", growthcycle = "", photosyn= "" ,usdaUrl = "", sql = "";

            ArrayList sqlcommands = new ArrayList();
            //-----------------PROGRESS BAR setup----------------------
            var lines = File.ReadLines(fileAddress);
            int TotalLines = lines.Count();
            toolStripProgressBar1.Step = 1;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = TotalLines;
            //---------------------------------------------------------
            h.open_DBconnection();

            foreach (string line in File.ReadLines(@fileAddress))
            {
                i++;
                toolStripProgressBar1.PerformStep();

                if (i == 1) continue; //skip the first line

                string data = line;
                //data = line.Replace(" ", "");
                string[] values = data.Split(',');

                if (values.Length == 0 || values.Length == 1)
                {
                    continue;
                }
                if (values.Length != 13)
                {
                    MessageBox.Show("Species Data file is not in correct format. " + values.Length + " values found", "File ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                //--------------------------------------------
                sppCode = values[0].Trim();
                symbol = values[1].Trim();
                growthHabit = values[2].Trim();
                subCode = values[3].Trim(); //growth_form
                scientific = values[4].Trim();
                common = values[5].Trim();
                genus = values[6].Trim();
                funcGrp = values[7].Trim();
                funcGrpCode = values[8].Trim();
                family = values[9].Trim();
                growthcycle = values[10]; //duration
                photosyn = values[11].Trim();
                usdaUrl = values[12].Trim();
                //----------------------------------------------
                sql = replaceOrignore + " " + tableName;

                sql = sql + " VALUES('" + sppCode + "','" + funcGrpCode + "','" + symbol + "','" + growthHabit + "','" + subCode + "','" +
                     scientific + "','" + common + "','" + genus + "','" + funcGrp + "','" + family + "','" + growthcycle + "','" + photosyn + "','" + usdaUrl + "')";

                sqlcommands.Clear();
                sqlcommands.Add(sql);

                int success = h.insert_data(sqlcommands);
                if (success >= 1)
                    rows_inserted++;

            } //end of for each

            MessageBox.Show((i - 1) + " records have been read from the data file. " + rows_inserted + " rows have been effected in the database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;
            h.close_DBconnection();
        }
        
        /*Completed*/
        private void ImportPlants(string fileAddress, string tableName)
        {
            int i = 0, disToDrain, rows_inserted = 0;
            string plantID = "", siteCode = "", sppCode = "", funcGrp = "", notes = "", sql = "";
            float canopyHt, canopyDm1, canopyDm2;
            double canopyArea;

            ArrayList sqlcommands = new ArrayList();
            //-----------------PROGRESS BAR setup---------------------------------
            var lines = File.ReadLines(fileAddress);
            int TotalLines = lines.Count();
            toolStripProgressBar1.Step = 1;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = TotalLines;
            //---------------------------------------------------------
            h.open_DBconnection();

            foreach (string line in File.ReadLines(@fileAddress))
            {
                i++;
                toolStripProgressBar1.PerformStep();
                if (i == 1) continue; //skip the first line

                string data = line;
                //data = line.Replace(" ", "");
                string[] values = data.Split(',');

                if (values.Length == 0 || values.Length == 1)
                {
                    continue;
                }
                if (values.Length != 9)
                {
                    MessageBox.Show("Plant Data file is not in correct format. " + values.Length + " values found", "File ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                //--------------------------------------------
                siteCode = values[0].Trim();
                sppCode = values[1].Trim();
                funcGrp = values[2].Trim();
                plantID = values[3].Trim();

                if (values[4].Equals(""))
                    canopyHt = -99.99f;
                else
                    canopyHt = float.Parse(values[4]);

                if (values[5].Equals(""))
                    canopyDm1 = -99.99f;
                else
                    canopyDm1 = float.Parse(values[5]);

                if (values[6].Equals(""))
                    canopyDm2 = -99.99f;
                else
                    canopyDm2 = float.Parse(values[6]);

                if (canopyDm1 != -99.99f && canopyDm2 != -99.99f)
                {
                    float meanDm = (canopyDm1 + canopyDm2) / 2;
                    float R = meanDm / 2;
                    canopyArea = Math.PI * R * R;
                }
                else
                    canopyArea = -99.99;

                if (values[7].Equals(""))
                    disToDrain = -99;
                else
                    disToDrain = int.Parse(values[7]);

                if (string.IsNullOrEmpty(values[8].Trim()))
                    notes = "NULL";
                else
                    notes = "'" + values[8].Trim() + "'";
                //----------------------------------------------
                sql = replaceOrignore + " " + tableName;

                sql = sql + " VALUES('" + plantID + "','" + siteCode + "','" + sppCode + "','" + funcGrp + "'," +
                    canopyHt + "," + canopyDm1 + "," + canopyDm2 + "," + canopyArea + "," + disToDrain + "," + notes + ")";

                sqlcommands.Clear();
                sqlcommands.Add(sql);

                int success = h.insert_data(sqlcommands);
                if (success >= 1)
                    rows_inserted++;

            } //end of for each

            MessageBox.Show((i - 1) + " records have been read from the data file. " + rows_inserted + " rows have been effected in the database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;

            h.close_DBconnection();
        }

        /*Completed*/
        private void ImportPhenoTitles(string fileAddress, string tableName)
        {
            int i = 0, rows_inserted = 0, titleID;
            string titleName = "", titleDesc = "", titleCategory = "", titleDisplay= "", funcGrp = "", sql = "";
            bool isActive;

            ArrayList sqlcommands = new ArrayList();
            //-----------------PROGRESS BAR setup---------------------
            var lines = File.ReadLines(fileAddress);
            int TotalLines = lines.Count();
            toolStripProgressBar1.Step = 1;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = TotalLines;
            //---------------------------------------------------------
            h.open_DBconnection();

            foreach (string line in File.ReadLines(@fileAddress))
            {
                i++;
                toolStripProgressBar1.PerformStep();
                if (i == 1) continue; //skip the first line

                string data = line;
                //data = line.Replace(" ", "");
                string[] values = data.Split(',');

                if (values.Length == 0 || values.Length == 1)
                {
                    continue;
                }
                if (values.Length != 7)
                {
                    MessageBox.Show("Events Data file is not in correct format. " + values.Length + " values found", "File ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                //--------------------------------------------
                titleID = int.Parse(values[0].Trim());
                titleName = values[1].Trim();
                titleDisplay = values[2].Trim();
                titleDesc = values[3].Trim();
                funcGrp = values[4].Trim();
                titleCategory = values[5].Trim();
                isActive = Convert.ToBoolean(values[6]);

                sqlcommands.Clear();
                //----------------------------------------------
                sql = replaceOrignore + " " + tableName;

                sql = sql + " VALUES(" + titleID + ",'" + funcGrp + "','" + titleName + "','" + titleCategory + "','" + titleDisplay + "','" + titleDesc + "',";

                sql += (isActive) ? 1 : 0;

                sql += ")";
                sqlcommands.Add(sql);

                int success = h.insert_data(sqlcommands);
                if (success >= 1)
                    rows_inserted++;

            } //end of for each

            MessageBox.Show((i - 1) + " records have been read from the data file. " + rows_inserted + " rows have been effected in the database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;

            h.close_DBconnection();
        }

        /*Completed*/
        private void ImportPhenophase(string fileAddress)
        {
            int i = 0, j, startIndex, rows_inserted = 0;
            string plantID = "", funcGrp = "", notes = "", sql = "", cond = "", funcGrpTable = "", photos = "";
            bool photo_flag, note_flag;

            DateTime date;
            ArrayList col_values = new ArrayList();
            ArrayList columns = new ArrayList();
            ArrayList plant_funcgrp = new ArrayList();
            plant_funcgrp.Add("PLANT_ID");
            plant_funcgrp.Add("FUNC_GRP_CODE");

            ArrayList gr_columns = h.get_data("information_schema.columns", "column_name", " WHERE table_name='pg_pheno'");
            ArrayList ds_columns = h.get_data("information_schema.columns", "column_name", " WHERE table_name='ds_pheno'");
            ArrayList be_columns = h.get_data("information_schema.columns", "column_name", " WHERE table_name='es_pheno'");
            ArrayList ca_columns = h.get_data("information_schema.columns", "column_name", " WHERE table_name='su_pheno'");

            DataTable dt = h.get_table("focal_plant_info", plant_funcgrp, "");
            Dictionary<string, string> plant_dictionary = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                plant_dictionary.Add(r["PLANT_ID"].ToString(), r["FUNC_GRP_CODE"].ToString());
            }

            ArrayList sqlcommands = new ArrayList();
            //-----------------PROGRESS BAR setup---------------------------------
            var lines = File.ReadLines(fileAddress);
            int TotalLines = lines.Count();
            toolStripProgressBar1.Step = 1;
            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = TotalLines;
            //---------------------------------------------------------
            h.open_DBconnection();

            foreach (string line in File.ReadLines(@fileAddress))
            {
                i++;
                toolStripProgressBar1.PerformStep();

                if (i == 1) continue; //skip the first line

                string data = line;
                //data = line.Replace(" ", "");
                string[] values = data.Split(',');

                if (values.Length == 1)
                {
                    continue;
                }
                if (values.Length != 57)
                {
                    MessageBox.Show("Record No: " + i + ". Phenophase Data file is not in correct format. " + values.Length + " values found", "File ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                //--------------------------------------------
                plantID = values[0].Trim();
                date = DateTime.Parse(values[1].Trim());

                cond = " WHERE PLANT_ID = '" + plantID + "'";

                try
                {
                    funcGrp = plant_dictionary[plantID];
                }
                catch (KeyNotFoundException)
                {
                    MessageBox.Show("PlantID " + plantID + " does not exist in date " + date.ToString("yyyy-MM-dd"), "Incorrect Value ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                columns.Clear();
                col_values.Clear();

                if (funcGrp.Contains("PG"))
                {
                    funcGrpTable = "pg_pheno";
                    startIndex = 2;

                    foreach (string col in gr_columns)
                        columns.Add(col);

                    for (j = 0; j < 13; j++)
                    {
                        if (values[startIndex + j].Equals("") && j != 3)
                            col_values.Add("-99");  
                        else if (values[startIndex + j].Equals("") && j == 3)  //case GR_03 being null'
                        {
                            int val_GR202 = int.Parse(values[startIndex + 2]);
                            if (val_GR202 == 0)
                                col_values.Add("1");
                            else
                                col_values.Add("0");
                        }
                        else
                            col_values.Add(values[startIndex + j]);
                    }

                    col_values.Add("-99"); //value of GR_09          
                    
                    //GR204---->5  ;    GR205------->7

                    if (!values[startIndex + 5].Equals("") && !values[startIndex + 7].Equals(""))
                    {
                        float val_GR204 = float.Parse(values[startIndex + 5]);
                        float val_GR205 = float.Parse(values[startIndex + 7]);
                        float perFO;

                        if (val_GR204 != 0.0f && val_GR205 != 0.0f)
                            perFO = (val_GR205 / val_GR204) * 100;
                        else
                            perFO = 0.0f;

                        int p_FO = Convert.ToInt32(perFO);  //value of GR_209
                        col_values.Add(p_FO.ToString());
                    }
                    /*else if (values[startIndex + 5].Equals("") && !values[startIndex + 7].Equals(""))
                    {
                        col_values.Add("-99");
                    }
                    else if (!values[startIndex + 5].Equals("") && values[startIndex + 7].Equals(""))
                    {
                        float perFO = 0.0f;
                        col_values.Add(perFO.ToString());
                    }*/
                    else
                        col_values.Add("-99");
                }
                else if (funcGrp.Contains("DS"))
                {
                    funcGrpTable = "ds_pheno";
                    startIndex = 15;

                    foreach (string col in ds_columns)
                        columns.Add(col);

                    //DS207---->8  ;    DS208------->10
                    for (j = 0; j < 19; j++)
                    {
                        //Add the new column and value
                        if (j == 12) //in case of DS_10
                        {
                            if (!values[startIndex + 8].Equals("") && !values[startIndex + 10].Equals(""))
                            {
                                float val_DS207 = float.Parse(values[startIndex + 8]);
                                float val_DS208 = float.Parse(values[startIndex + 10]);
                                float perFO;

                                if (val_DS207 != 0.0f && val_DS208 != 0.0f)
                                    perFO = (val_DS208 / (val_DS207 + val_DS208)) * 100;
                                else
                                    perFO = 0.0f;

                                int p_FO = Convert.ToInt32(perFO);  //value of DS_209
                                col_values.Add(p_FO.ToString());
                            }
                            /*else if (!values[startIndex + 8].Equals("") && values[startIndex + 10].Equals(""))
                            {
                                col_values.Add("-99");
                            }
                            else if (values[startIndex + 8].Equals("") && !values[startIndex + 10].Equals(""))
                            {
                                float perFO = 0.0f;
                                col_values.Add(perFO.ToString());
                            }*/
                            else
                                col_values.Add("-99");
                        }
                        //---------------------------------------
                        if (values[startIndex + j].Equals("") && j != 6)
                            col_values.Add("-99");//col_values.Add("null");
                        else if (values[startIndex + j].Equals("") && j == 6) //case DS_06 being null'
                        {
                            if (values[startIndex + 2].Equals(""))
                                col_values.Add("-99");
                            else
                            {
                                int val_DS202 = int.Parse(values[startIndex + 2]);
                                if (val_DS202 == 0)
                                    col_values.Add("1");
                                else
                                    col_values.Add("0");
                            }
                        }
                        else
                            col_values.Add(values[startIndex + j]);
                    }
                }
                else if (funcGrp.Contains("ES"))
                {
                    funcGrpTable = "es_pheno";
                    startIndex = 34;

                    foreach (string col in be_columns)
                        columns.Add(col);

                    for (j = 0; j < 11; j++)
                    {
                        if (j == 7) //in case of BE_06
                        {
                            if (!values[startIndex + 3].Equals("") && !values[startIndex + 5].Equals(""))
                            {
                                float val_BE203 = float.Parse(values[startIndex + 3]);
                                float val_BE204 = float.Parse(values[startIndex + 5]);
                                float perFO;

                                if (val_BE203 != 0.0f && val_BE204 != 0.0f)
                                    perFO = (val_BE204 / (val_BE203 + val_BE204)) * 100;
                                else
                                    perFO = 0.0f;

                                int p_FO = Convert.ToInt32(perFO);  //value of BE_205
                                col_values.Add(p_FO.ToString());
                            }
                            /*else if (!values[startIndex + 3].Equals("") && values[startIndex + 5].Equals(""))
                            {
                                col_values.Add("-99");
                            }
                            else if (values[startIndex + 3].Equals("") && !values[startIndex + 5].Equals(""))
                            {
                                float perFO = 0.0f;
                                col_values.Add(perFO.ToString());
                            }*/
                            else
                                col_values.Add("-99");
                        }

                        if (values[startIndex + j].Equals(""))
                            col_values.Add("-99");
                        else
                            col_values.Add(values[startIndex + j]);
                    }
                }
                else if (funcGrp.Contains("SU"))
                {
                    funcGrpTable = "su_pheno";
                    startIndex = 45;

                    foreach (string col in ca_columns)
                        columns.Add(col);

                    for (j = 0; j < 8; j++)
                    {
                        if (values[startIndex + j].Equals(""))
                            col_values.Add("-99");//col_values.Add("null");
                        else
                            col_values.Add(values[startIndex + j]);
                    }
                    col_values.Add("-99");//col_values.Add("null"); //value of CA_05

                    if (!values[startIndex + 1].Equals("") && !values[startIndex + 3].Equals(""))
                    {
                        float val_CA201 = float.Parse(values[startIndex + 1]);
                        float val_CA202 = float.Parse(values[startIndex + 3]);
                        float perFO;

                        if (val_CA201 != 0.0f && val_CA202 != 0.0f)
                            perFO = (val_CA202 / (val_CA201 + val_CA202)) * 100;
                        else
                            perFO = 0.0f;

                        int p_FO = Convert.ToInt32(perFO);  //value of CA_205
                        col_values.Add(p_FO.ToString());
                    }
                    else
                        col_values.Add("-99");//col_values.Add("null");
                }

                note_flag = Convert.ToBoolean(values[53]);
                notes = values[54].Trim();
                
                photo_flag = Convert.ToBoolean(values[55]);
                photos = values[56].Trim();
               
                sqlcommands.Clear();
                //----------------DOY------------------------------
                sql = "INSERT IGNORE date_doy(DATE, DAY_OF_YEAR) VALUES('" + date.ToString("yyyy-MM-dd") + "'," + date.DayOfYear + ")";

                sqlcommands.Add(sql);
                //-------------------PLANT DATA--------------------------
                sql = replaceOrignore + " " + funcGrpTable + "(";

                foreach (string col in columns)
                {
                    sql = sql + col + ",";
                }
                sql = sql.Remove(sql.LastIndexOf(','), 1);
                sql += ")";

                sql = sql + " VALUES('" + plantID + "','" + date.ToString("yyyy-MM-dd") + "',";

                foreach (string val in col_values)
                {
                    sql = sql + val + ",";
                }

                sql += (note_flag) ? 1 : 0;
                sql += ",";
                sql += (photo_flag) ? 1 : 0;

                sql += ")";

                sqlcommands.Add(sql);

                if (notes.Equals(""))
                    note_flag = false;

                if (photos.Equals(""))
                    photo_flag = false;
                //------------------PHOTO---------------------------
                if (photo_flag)
                {
                    string[] photo_names = photos.Split(';');

                    for (int k = 0; k < photo_names.Length; k++)
                    {
                        sql = replaceOrignore + " photo_info(PLANT_ID, DATE, PHOTO_NAME) VALUES('" + plantID + "','" + date.ToString("yyyy-MM-dd")
                          + "','" + photo_names[k].Trim() + "')";
                        sqlcommands.Add(sql);
                    }
                }
                //-------------------NOTES---------------------------
                if (note_flag)
                {
                    if (notes.Contains("'"))
                    {
                        //MessageBox.Show("Date: " + date.ToString("yyyy-MM-dd") + " Plant:" + plantID + " Notes: " + notes); 
                        notes = notes.Replace("'", @"\'");
                    }
                    notes = notes.ToLower();

                    sql = replaceOrignore + " plant_note(PLANT_ID, DATE, PLANT_NOTES) VALUES('" + plantID + "','" + date.ToString("yyyy-MM-dd")
                          + "','" + notes + "')";
                    sqlcommands.Add(sql);
                }
                //--------------------------------------------------
                int success = h.insert_data(sqlcommands);
                if (success >= 1)
                    rows_inserted++;
                else if (success < 0)
                    break;

            } //end of for each

            MessageBox.Show((i - 1) + " records have been read from the data file. " + rows_inserted + " rows have been effected in the database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;

            h.close_DBconnection();
        }

        //----------------------------------------------------------------
        

    }
}
