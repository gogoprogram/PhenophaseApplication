using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class FrmInsertData : Form
    {
        string FG_table = "";
        ArrayList columns = null;
        Helper h;
        Access acs;
        Dictionary<string, string> event_desc_dictionary = null;

        /*------------------------------------*/
        private static FrmInsertData FIDsInst = null;
        //-------------------------------------
        // Create a public static property that returns the state of the instance
        public static FrmInsertData CheckInst
        {
            get
            {
                return FIDsInst;
            }
        }

        // Create a public static property that will create an instance of the form and return it
        public static FrmInsertData CreateInst
        {
            get
            {
                if (FIDsInst == null)
                    FIDsInst = new FrmInsertData();
                return FIDsInst;
            }
        }

        // Need to override the OnFormClosing event to set the Instance to null
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
                FIDsInst = null;
            }
        }

        //----------------------------------------------------------

        public FrmInsertData()
        {
            InitializeComponent();
            h = new Helper("phenophaseDBConnection");
           
            init_txtFocus_events();
        }

        private void FrmData_Load(object sender, EventArgs e)
        {
            columns = new ArrayList();
            event_desc_dictionary = new Dictionary<string, string>();

            string[] par_names = new string[1];
            string[] par_values = new string[1];

            h.fillwSP_combo_or_list(this, "cmbSite", true, "sp_get_sitecodes", par_names, par_values, "SITE_NAME", "SITE_CODE");
            h.fillwSP_combo_or_list(this, "cmbFG", true, "sp_get_funcgrpcodes", par_names, par_values, "FUNC_GRP", "FUNC_GRP_CODE");
            
            ArrayList observers = h.get_data("observer_info", "OBSERVER_ID", "");
            h.populate_control(this, "chkLstObserver", observers);

        }

        private void init_txtFocus_events()
        {
            int i;
            Control c = null;
            string controlID = "";

            for (i = 1; i <= 12; i++)
            {
                controlID = (i <= 9) ? "0" + i.ToString() : i.ToString();
                c = h.get_control(this, "txt" + controlID);
                if (c != null)
                    c.GotFocus += new System.EventHandler(this.txtGotFocus);
            }
            for (i = 1; i <= 14; i++)
            {
                if (i == 12) continue;
                controlID = (i <= 9) ? "20" + i.ToString() : "2" + i.ToString();
                c = h.get_control(this, "txt" + controlID);
                if (c != null)
                    c.GotFocus += new System.EventHandler(this.txtGotFocus);
            }
            
        }
        
        private void txtGotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb != null)
            {
                string txtID = tb.Name;
                string colID = txtID.Substring(3);

                if (!FG_table.Equals(""))
                {
                    string col = FG_table.Substring(0, 2);
                    col = h.table_to_field(col);
                    col = col + "_" + colID;
                    toolStripStatusLabel1.Text = event_desc_dictionary[col];
                }
            }
        }
        
        /*Checks whether site visit data exits in database or not*/
        private bool is_site_visited(string plantId)
        {
            bool visited = true;
            string cond = "";
            
            DateTime date = dateTimePicker1.Value.Date;

            ArrayList sites =  h.get_data("focal_plant_info", "SITE_CODE", " WHERE PLANT_ID = '" + plantId + "'");
            
            foreach(string site in sites)
            {
                cond = " WHERE DATE = '" + date.ToString("yyyy-MM-dd") + "' AND SITE_CODE = '" + site + "'";
                ArrayList sitevisits = h.get_data("site_visit", "OBSERVER_ID", cond);

                if (sitevisits.Count == 0)
                {
                    visited = false;
                    MessageBox.Show("Site: " + site + " has not been visited on " + date.ToString("MM-dd-yyyy") + ". Please insert data related to site visit first.", "SiteVisit Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
            }

            return visited;
        }
        
        /*Gets SQL to insert plant data*/
        private string get_plant_sql()
        {
            string plantID = "", colID = "", col = "", colVal = "";
            DateTime date;
            string sql = "INSERT INTO " + FG_table + "(";
            
            foreach (string c in columns)
            {
                sql = sql + c + ",";
            }
            sql = sql.Remove(sql.LastIndexOf(','), 1);
            sql += ")";

            if (cmbPlantID.SelectedItem != null)
                plantID = cmbPlantID.SelectedItem.ToString();
            
            date = dateTimePicker1.Value.Date;

            sql = sql + " VALUES('" + plantID + "','" + date.ToString("yyyy-MM-dd") + "',";
            
            for(int i = 2; i < columns.Count - 2; i++)
            {
                col = columns[i].ToString();
                colID = col.Substring(col.IndexOf("_")+1);
                Control ctrl = h.get_control(this, "txt" + colID);
                colVal = ((TextBox)ctrl).Text;

                if (colVal.Equals(""))
                    colVal = "-99";
               
                sql = sql + colVal + ",";
            }

            if (txtNotes.Text.Equals(""))
                sql += "0,";
            else
                sql += "1,";

            if(chkPhoto.Checked)
                sql += "1" + ")";
            else
                sql += "0" + ")";

            return sql;
        }
        
        /*Gets SQL to insert plant note data*/
        private string get_plant_note_sql()
        {
            string sql = "", plantid = "", plantnote = "";
            DateTime date = dateTimePicker1.Value.Date;

            plantid = cmbPlantID.SelectedItem.ToString();
            plantnote = txtNotes.Text;
            
            if(plantnote.Contains("'"))
                plantnote = plantnote.Replace("'", @"\'");
            if (plantnote.Contains(","))
                plantnote = plantnote.Replace(",", ";");

            sql = "INSERT INTO plant_note(DATE, PLANT_ID, PLANT_NOTES) VALUES('" + date.ToString("yyyy-MM-dd")
                   + "','" + plantid + "','" + plantnote + "')";

            return sql;
        }

        /*Gets SQL to insert day of year data*/
        private string get_doy_sql()
        {
            string sql = "";
            int doy;
            
            DateTime date = dateTimePicker1.Value.Date;
            doy = date.DayOfYear;

            sql = "INSERT IGNORE date_doy(DATE, DAY_OF_YEAR) VALUES('" + date.ToString("yyyy-MM-dd") + "'," + doy + ")";

            return sql;
        }

        /*Gets SQL to insert site visit data*/
        private string get_site_visit_sql(string observer)
        {
            string sql = "", sitecode = "";
            DateTime date = dateTimePicker1.Value.Date;

            sitecode = cmbSite.SelectedValue.ToString();

            sql = "INSERT INTO site_visit(DATE, SITE_CODE, OBSERVER_ID) VALUES('" + date.ToString("yyyy-MM-dd")
                   + "','" + sitecode + "','" + observer + "')";

            return sql;
        }
        
        /*Gets SQL to insert site note data*/
        private string get_site_note_sql()
        {
            string sql = "", sitecode = "", sitenote = "";
            DateTime date = dateTimePicker1.Value.Date;

            sitecode = cmbSite.SelectedValue.ToString();
            sitenote = txtSiteNote.Text.Trim();
            if (sitenote.Contains("'"))
                sitenote = sitenote.Replace("'", @"\'");
            if (sitenote.Contains(","))
                sitenote = sitenote.Replace(",", ";");

            sql = "INSERT INTO site_note(DATE, SITE_CODE, SITE_NOTES) VALUES('" + date.ToString("yyyy-MM-dd") 
                   +"','"+ sitecode + "','" + sitenote +"')";

            return sql;
        }

        /*Gets SQL to insert photo data*/
        private string get_photo_sql()
        {
            string sql = "", plantID = "", photoName = "";
            
            DateTime date = dateTimePicker1.Value.Date;
            plantID = cmbPlantID.SelectedItem.ToString();
            photoName = txtPhoto.Text;
            photoName = photoName.Substring(photoName.LastIndexOf('\\') + 1);

            sql = "INSERT INTO photo_info(PLANT_ID, DATE, PHOTO_NAME) VALUES('" + plantID + "','" + date.ToString("yyyy-MM-dd")
                   + "','" + photoName + "')";
            
            return sql;

        }

        private void change_visibility(int type)
        {
            int numControls = 0;
            string controlID = "";
            
            if (type == 1)
                numControls = 12;
            else
                numControls = 14;
            
            for (int i = 1; i <= numControls; i++)
            {
                if(type == 1)
                    controlID = (i <= 9) ? "0" + i.ToString() : i.ToString();
                else
                {
                    if (i == 12) continue;
                    controlID = (i <= 9) ? "20" + i.ToString() : "2"+i.ToString();
                }
                Control lblctrl = h.get_control(this, "lbl" + controlID);
                Control txtctrl = h.get_control(this, "txt" + controlID);

                ((TextBox)txtctrl).Visible = false;
                ((Label)lblctrl).Visible = false;
            }
        }

        private void refresh_plants()
        {
            change_visibility(1);
            change_visibility(2);
            
            bool is_active = true;
            string cond = "";
            
            string FG_Code = cmbFG.SelectedValue.ToString();
            FG_table = FG_Code.ToLower() + "_pheno";
            
            if(chkAll.Checked)
                is_active = false;
            else
                is_active = true;  
            int IsActive = (is_active) ? 1 : 0;

            ArrayList event_funcgrp = new ArrayList();
            event_funcgrp.Add("TITLE_NAME");
            event_funcgrp.Add("TITLE_DISPLAY_NAME");
            event_funcgrp.Add("TITLE_DESCRIPTION");

            if(is_active)
                cond = " WHERE FUNC_GRP_CODE = '" + FG_Code + "'" + " AND IS_ACTIVE = " + IsActive;
            else
                cond = " WHERE FUNC_GRP_CODE = '" + FG_Code + "'";

            DataTable dt = h.get_table("pheno_title_info", event_funcgrp, cond);

            event_desc_dictionary.Clear();
            Dictionary<string, string> event_dictionary = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                event_dictionary.Add(r["TITLE_NAME"].ToString(), r["TITLE_DISPLAY_NAME"].ToString());
                event_desc_dictionary.Add(r["TITLE_NAME"].ToString(), r["TITLE_DESCRIPTION"].ToString());
            }

            columns.Clear();
            columns.Add("PLANT_ID");
            columns.Add("DATE");

            foreach (KeyValuePair<string, string> entry in event_dictionary)
            {
                string col = entry.Key;
                string colID = col.Substring(3);
                
                //string colName = col.Substring(0, 2);
                //colName = colName + "_" + colID;
                string colName = col;
                columns.Add(colName);
                
                Control lblctrl = h.get_control(this, "lbl" + colID);
                Control txtctrl = h.get_control(this, "txt" + colID);

                ((Label)lblctrl).Text = entry.Value;

                ((TextBox)txtctrl).Visible = true;
                ((Label)lblctrl).Visible = true;
            }
            columns.Add("NOTES_FLAG");
            columns.Add("PHOTO_FLAG");
            
            string sitecode = cmbSite.SelectedValue.ToString();

            cond = " WHERE SITE_CODE = '" + sitecode + "' AND FUNC_GRP_CODE = '" + FG_Code + "'";
           
            ArrayList plantIDs = h.get_data("focal_plant_info", "PLANT_ID", cond);
            h.populate_control(this, "cmbPlantID", plantIDs);
        }

        private bool is_valid_input(object sender)
        {
            bool valid = false;

            if (((TextBox)sender).Visible == false)
                return true;  //hidden fields are considered as valid 
            try
            {
                int numberEntered = int.Parse(((TextBox)sender).Text);
                if (numberEntered < 0 || numberEntered > 2)
                {
                    ((TextBox)sender).BackColor = Color.Red;
                    MessageBox.Show("You have to enter a number between 0 and 2", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                    valid = true;
            }
            catch (FormatException)
            {
                ((TextBox)sender).BackColor = Color.Red;
                MessageBox.Show("You have to enter a number between 0 and 2", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return valid;
        }

        private bool is_valid_percentage(object sender)
        {
            bool valid = false;

            if (((TextBox)sender).Visible == false)
                return true;  //hidden fields are considered as valid 
     
            try
            {
                int numberEntered = int.Parse(((TextBox)sender).Text);
                if (numberEntered < 0 || numberEntered > 6)
                {
                    ((TextBox)sender).BackColor = Color.Red;
                    MessageBox.Show("You have to enter a number between 0 and 6", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                    valid = true;
            }
            catch (FormatException)
            {
                ((TextBox)sender).BackColor = Color.Red;
                MessageBox.Show("You have to enter a number between 0 and 6", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return valid;
        }

        private bool is_empty_or_number()
        {
            string col = "", colID = "", colVal = "";
            bool isInvalid = false;

            for (int i = 2; i < columns.Count - 2; i++)
            {
                col = columns[i].ToString();
                colID = col.Substring(col.IndexOf("_") + 1);
                Control tctrl = h.get_control(this, "txt" + colID);
                Control lctrl = h.get_control(this, "lbl" + colID);
                colVal = ((TextBox)tctrl).Text;

                if (string.IsNullOrEmpty(colVal) && ((TextBox)tctrl).Visible == true)
                {
                    isInvalid = true;
                    MessageBox.Show("You must put value for " + ((Label)lctrl).Text.ToUpper(), "Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                try
                {
                    int numberEntered = int.Parse(colVal);
                }
                catch (FormatException)
                {
                    //((TextBox)tctrl).BackColor = Color.Red;
                    isInvalid = true;
                    MessageBox.Show("You must enter a number", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    break;
                }
            }
            return isInvalid;
        }

        private bool checkAccessFile()
        {
            bool isSelected = true;

            if (string.IsNullOrEmpty(txtFile.Text))
            {
                isSelected = false;
                MessageBox.Show("You must select the MS Access File", "Selection ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                acs = new Access(txtFile.Text);
                if (!acs.TestAccessConn())
                {
                    isSelected = false;
                    MessageBox.Show("You must select a VALID MS Access File", "Selection ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            return isSelected;
        }

        private bool insert_SiteVisitToAccess()
        {
            string[] stvcolumns = { "date", "doy", "sitecode", "observers", "sitenotes" };
            int[] types = { 3, 1, 0, 0, 0 };
            int success;
            //---------assigning Object values-------------
            string SiteCode = cmbSite.SelectedValue.ToString();
            
            string Observers = "";
            foreach (object itemChecked in chkLstObserver.CheckedItems)
            {
                Observers = Observers + itemChecked.ToString() + ";";
            }
            Observers = Observers.Remove(Observers.LastIndexOf(';'), 1);

            string SiteNotes = "";
            SiteNotes = txtSiteNote.Text;

            if (SiteNotes.Contains(","))
                SiteNotes = SiteNotes.Replace(",", ";");
            if (SiteNotes.Contains("'"))
                SiteNotes = SiteNotes.Replace("'", "''");

            DateTime obdate = dateTimePicker1.Value.Date;
            int doy = obdate.DayOfYear;

            Object[] values = { obdate, doy, SiteCode, Observers, SiteNotes };
            //---------------------------------------------------
            string[] condColumns = { "date", "sitecode" };
            Object[] condValues = { obdate, SiteCode };

            if (acs.isAcesRecordExists("SiteVisitTable", condColumns, condValues))
            {
                success = 0;
                MessageBox.Show("SiteVisit data already exits in Access Database", "Insert Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                success = acs.InsertAcesRecord("SiteVisitTable", stvcolumns, types, values);
            }
            return (success == 1)? true: false;
        }

        private bool insert_PlantDataToAccess()
        {
            string col = "", colID = "", colmn = "", plantID = "", plantnote = "";
            DateTime obsdate;
            int numAcsCols = 0, notesFlag, photoFlag, success;

            /*-----Exceptional columns not in Access but in MySQL-----*/
            string[] exp_cols = { "GR_09", "GR_209", "DS_209", "BE_205", "CA_05", "CA_205" };
            //---------------------------------------------------------
            ArrayList AcsColumns = new ArrayList();
            AcsColumns.AddRange(columns); //copy the MySQL column names

            for (int i = 0; i < AcsColumns.Count; i++)
            {
                colmn = AcsColumns[i].ToString();
                for (int j = 0; j < exp_cols.Length; j++)
                {
                    if (colmn.Equals(exp_cols[j]))
                    {
                        AcsColumns.Remove(colmn);
                    }
                }
            }
            
            numAcsCols = AcsColumns.Count+2;
            //-----------------------------------------
            if (cmbPlantID.SelectedItem != null)
                plantID = cmbPlantID.SelectedItem.ToString();
            
            obsdate = dateTimePicker1.Value.Date;

            string[] condColumns = { "plantid", "date" };
            Object[] condValues = { plantID, obsdate };

            if (acs.isAcesRecordExists("Phenophase", condColumns, condValues))
            {
                MessageBox.Show("Plant data already exits in Access Database", "Insert Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string[] acs_columns = new string[numAcsCols];
            int[] types = new int[numAcsCols];
            Object[] acs_values = new Object[numAcsCols];

            
            int k = 0;
            acs_columns[k] = "plantid";     acs_values[k] = plantID;   types[k++] = 0;  //text type;  
            acs_columns[k] = "date";        acs_values[k] = obsdate;   types[k++] = 3;  //date type

            for (int i = 2; i < AcsColumns.Count - 2; i++)
            {
                colmn = AcsColumns[i].ToString();
               
                //------------------------------
                col = colmn.Substring(0, 2);
                colID = colmn.Substring(colmn.IndexOf("_") + 1);
                //---------Column----------------
                acs_columns[k] = col + colID;
                //---------Value-----------------
                Control ctrl = h.get_control(this, "txt" + colID);
                string colVal = ((TextBox)ctrl).Text;

                if (colVal.Equals(""))
                    colVal = "-99";

                acs_values[k] = colVal;
                //-----------Type-------------
                types[k++] = 1;  //numeric type
            }

            if (txtNotes.Text.Equals(""))
                notesFlag = 0;
            else
            {
                notesFlag = 1;
                plantnote = txtNotes.Text;
                if (plantnote.Contains(","))
                    plantnote = plantnote.Replace(",", ";");
                if (plantnote.Contains("'"))
                    plantnote = plantnote.Replace("'", "''");
            }
            if (chkPhoto.Checked)
                photoFlag = 1;
            else
                photoFlag = 0;

            acs_columns[k] = "notesflag";     acs_values[k] = notesFlag;      types[k++] = 4;
            acs_columns[k] = "notes";         acs_values[k] = plantnote;      types[k++] = 0;
            acs_columns[k] = "photoflag";     acs_values[k] = photoFlag;      types[k++] = 4;
            acs_columns[k] = "Photoname";     acs_values[k] = "";             types[k++] = 0;

            success = acs.InsertAcesRecord("Phenophase", acs_columns, types, acs_values);
            
            return (success == 1)? true: false;

        }

        private bool insert_PhotoDataToAccess()
        {
            int success;
            string plantID = "", photoName = "", phtInfo = "";
            
            plantID = cmbPlantID.SelectedItem.ToString();
            photoName = txtPhoto.Text;
            photoName = photoName.Substring(photoName.LastIndexOf('\\') + 1);
           
            DateTime obdate = dateTimePicker1.Value.Date;
           
            string[] columnNames = { "photoflag","Photoname" };
            //---------------------------------------------------
            string[] condColumns = { "plantid", "date" };
            Object[] condValues = { plantID, obdate };

            if (acs.isAcesRecordExists("Phenophase", condColumns, condValues))
            {
                DataTable phtRecord = acs.GetAcesRecord("Phenophase", columnNames, condColumns, condValues);
                phtInfo = phtRecord.Rows[0]["Photoname"].ToString();
                phtInfo = phtInfo + ";" + photoName;
                Object[] columnValues = { 1, phtInfo };
                success = acs.UpdateAcesRecord("Phenophase", columnNames, columnValues, condColumns, condValues);
            }
            else
            {
                success = 0;
                MessageBox.Show("Plant data does not exit in Access Database", "Insert Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return (success == 1) ? true : false;
        }

        private bool validate_textboxes(int type)
        {
            int numControls = 0;
            string controlID = "";
            bool valid = false;

            if (type == 1)
                numControls = 13;
            else
                numControls = 14;

            for (int i = 1; i <= numControls; i++)
            {
                if ((type == 2 && (i != 2)) || (type == 2 && (i != 14)))
                    continue;
                else if (type == 1 && i <= 12)
                    controlID = (i <= 9) ? "0" + i.ToString() : i.ToString();
                else if (type == 1 && i > 12)
                    controlID = "2" + i.ToString();
                else if (type == 2 && i < 14)
                    controlID = "20" + i.ToString();
                else
                    controlID = "2" + i.ToString();

                Control txtctrl = h.get_control(this, "txt" + controlID);

                if(type == 1)
                    valid = is_valid_input(txtctrl);
                else
                {
                    string fg = cmbFG.SelectedValue.ToString().ToLower();

                    if ((fg.Contains("ds") || fg.Contains("pg")) && i == 2)
                        valid = is_valid_percentage(txtctrl);
                    else if((fg.Contains("ds") && i == 14))
                        valid = is_valid_percentage(txtctrl);
                }

                if (!valid)
                {
                    break;
                }

                ((TextBox)txtctrl).BackColor = System.Drawing.SystemColors.Window;
            }

            return valid;
        }

        private void clear_textboxes(int type)
        {
            int numControls = 0;
            string controlID = "";

            if (type == 1)
                numControls = 12;
            else
                numControls = 14;

            for (int i = 1; i <= numControls; i++)
            {
                if (type == 1)
                    controlID = (i <= 9) ? "0" + i.ToString() : i.ToString();
                else
                {
                    if (i == 12) continue;
                    controlID = (i <= 9) ? "20" + i.ToString() : "2" + i.ToString();
                }

                Control txtctrl = h.get_control(this, "txt" + controlID);
                ((TextBox)txtctrl).Text = "";
            }
        }
 
        //----------------------------------------------------------------------

        /*Opens FileDialog to Select Access File*/
        private void btnFileBrowse_Click(object sender, EventArgs e)
        {
            string filter = "Access 2007-2010 (*.accdb)|*accdb|Access 2000-2003 (*.mdb)|*.mdb";
            string ctrlName = "txtFile";

            h.openDialog(this, filter, ctrlName);
        }

        /*Inserts Site Visit Data into Databases*/
        private void btnSiteInsert_Click(object sender, EventArgs e)
        {
            string sql = "";

            if (cmbSite.SelectedItem == null)
            {
                MessageBox.Show("You must select a site", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (chkLstObserver.CheckedItems.Count == 0 && !chkSiteNote.Checked)
            {
                MessageBox.Show("You must select an observer", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (chkAccess.Checked)
            {
                if (!checkAccessFile())
                    return;
            }

            /*-------------INSERT INTO ACCESS----------------*/
            if (chkAccess.Checked && !chkSiteNote.Checked)
            {
                if (insert_SiteVisitToAccess())
                    MessageBox.Show("SiteVisit data inserted into Access Database successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    MessageBox.Show("No SiteVisit data has been inserted into Access Database", "Insert Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            //-----------------------------------------------
            ArrayList sqlCommands = new ArrayList();

            sql = get_doy_sql();
            sqlCommands.Add(sql);

            if (!chkSiteNote.Checked)
            {
                string tbl = "site_visit", clm = "*",
                    cond = " WHERE DATE = '" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "' AND SITE_CODE = '" + cmbSite.SelectedValue.ToString() + "' AND " +
                    "OBSERVER_ID = ";
                foreach (object itemChecked in chkLstObserver.CheckedItems)
                {
                    string observer = itemChecked.ToString();

                    if (!h.check_data_exists(tbl, clm, cond + "'" + observer + "'"))
                    {
                        sql = get_site_visit_sql(observer);
                        sqlCommands.Add(sql);
                    }
                    else
                    {
                        MessageBox.Show("SiteVisit data already exits in MySQL Database", "Insert Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            if (!txtSiteNote.Text.Equals(""))
            {
                sql = get_site_note_sql();
                sqlCommands.Add(sql);
            }
            else if (txtSiteNote.Text.Equals("") && chkSiteNote.Checked)
            {
                MessageBox.Show("You must write some site note", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            h.open_DBconnection();

            int rows_effected = h.insert_data(sqlCommands);

            if (rows_effected > 0)
            {
                MessageBox.Show("Site data inserted into MySQL database successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSiteNote.Text = "";
            }
            else
                MessageBox.Show("No Site data has been inserted into MySQL database", "Insert Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            h.close_DBconnection();

            if (chkSiteNote.Checked)
                chkSiteNote.CheckState = CheckState.Unchecked;

        }

        /*Inserts Plant specific Data into Databases*/
        private void btnPlantInsert_Click(object sender, EventArgs e)
        {
            string sql = "";

            if (cmbFG.SelectedItem == null)
            {
                MessageBox.Show("You must select a Functional Group", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (cmbPlantID.SelectedItem == null)
            {
                MessageBox.Show("You must select a Plant ID", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            bool visited = is_site_visited(cmbPlantID.SelectedItem.ToString());

            if (!visited)
            {
                MessageBox.Show("You must insert site visit info. No Plant data has been inserted", "Insert Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //To insert plant data, the input fields do not require validation
            if (!chkPlantNote.Checked)
            {
                if (is_empty_or_number())
                {
                    MessageBox.Show("No Plant data has been inserted", "Insert Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!validate_textboxes(1) && !validate_textboxes(2))
                {
                    MessageBox.Show("No Plant data has been inserted", "Insert Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (chkAccess.Checked)
            {
                if (!checkAccessFile())
                    return;
            }
            
            /*-------------INSERT INTO ACCESS----------------*/
            if (chkAccess.Checked && !chkPlantNote.Checked)
            {
                if (insert_PlantDataToAccess())
                    MessageBox.Show("Plant data inserted into Access Database successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    MessageBox.Show("No Plant data has been inserted into Access Database", "Insert Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            //-----------------------------------------------

            ArrayList sqlCommands = new ArrayList();

            if (!chkPlantNote.Checked)
            {
                sql = get_plant_sql();
                sqlCommands.Add(sql);
            }

            if (!txtNotes.Text.Equals(""))
            {
                sql = get_plant_note_sql();
                sqlCommands.Add(sql);
            }
            else if (txtNotes.Text.Equals("") && chkPlantNote.Checked)
            {
                MessageBox.Show("You must write some plant note", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            h.open_DBconnection();

            int rows_effected = h.insert_data(sqlCommands);

            if (rows_effected > 0)
            {
                MessageBox.Show("Plant data inserted into MySQL Database successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear_textboxes(1);
                clear_textboxes(2);
                txtNotes.Text = "";
            }
            else
                MessageBox.Show("No Plant data has been inserted into MySQL Database", "Insert Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            h.close_DBconnection();

            if (chkPlantNote.Checked)
                chkPlantNote.CheckState = CheckState.Unchecked;
        }

        /*Inserts Photo specific Data into Databases*/
        private void btnPhoto_Click(object sender, EventArgs e)
        {
            ArrayList sqlCommands = new ArrayList();
            string sql = "";

            if (cmbPlantID.SelectedItem == null)
            {
                MessageBox.Show("You must select a Plant ID", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (txtPhoto.Text.Equals("") || txtPhoto.Text.Equals("photoname"))
            {
                MessageBox.Show("You must select a photoname", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (chkAccess.Checked)
            {
                if (!checkAccessFile())
                    return;
            }
            /*-------------INSERT INTO ACCESS----------------*/
            if (chkAccess.Checked)
            {
                if (insert_PhotoDataToAccess())
                    MessageBox.Show("Photo data inserted into Access Database successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    MessageBox.Show("No Photo data has been inserted into Access Database", "Insert Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            string tbl = "photo_info", clm = "*",
                    cond = " WHERE DATE = '" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "' AND PLANT_ID = '" + cmbPlantID.SelectedItem.ToString() +
                    "' AND " + "PHOTO_NAME = '" + txtPhoto.Text.Substring(txtPhoto.Text.LastIndexOf('\\') + 1) + "'";

            if (!h.check_data_exists(tbl, clm, cond))
            {
                sql = get_photo_sql();
                sqlCommands.Add(sql);
            }
            else
            {
                MessageBox.Show("Photo data already exits in MySQL Database", "Insert Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            h.open_DBconnection();

            int rows_effected = h.insert_data(sqlCommands);

            if (rows_effected > 0)
                MessageBox.Show("Photo data inserted into MySQL Database successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No Photo data has been inserted into MySQL Database", "Insert Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            h.close_DBconnection();
        }

        private void txtPhoto_Click(object sender, EventArgs e)
        {
            string filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            string ctrlName = "txtPhoto";

            h.openDialog(this, filter, ctrlName);
        }

        private void chkPhoto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPhoto.Checked)
            {
                txtPhoto.Visible = true;
                btnPhoto.Visible = true;
            }
            else
            {
                txtPhoto.Visible = false;
                btnPhoto.Visible = false;
            }
        }

        private void cmbSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbPlantID.Text = "";
            cmbPlantID.Items.Clear();
            cmbPlantID.SelectedIndex = -1;
        }

        private void cmbFG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSite.SelectedItem == null)
            {
                MessageBox.Show("You must select a site", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            refresh_plants();
        }

        private void chkAccess_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAccess.Checked)
            {
                txtFile.Enabled = true;
                btnFileBrowse.Enabled = true;
            }
            else
            {
                txtFile.Enabled = false;
                btnFileBrowse.Enabled = false;
            }
        }

        
    } //end of class
}
