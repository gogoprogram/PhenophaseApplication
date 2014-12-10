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
using System.Configuration;

namespace WindowsFormsApplication1
{
    public partial class FrmQuery : Form
    {
        Helper h;

        /*------------------------------------*/
        private static FrmQuery FQsInst = null;
        //-------------------------------------
        // Create a public static property that returns the state of the instance
        public static FrmQuery CheckInst
        {
            get
            {
                return FQsInst;
            }
        }

        // Create a public static property that will create an instance of the form and return it
        public static FrmQuery CreateInst
        {
            get
            {
                if (FQsInst == null)
                    FQsInst = new FrmQuery();
                return FQsInst;
            }
        }

        // Need to override the OnFormClosing event to set the Instance to null
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
                FQsInst = null;
            }
        }

        //----------------------------------------------------------

        public FrmQuery()
        {
            InitializeComponent();
            h = null;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            h = new Helper("phenophaseDBConnection");
            
            string[] par_names = new string[1];
            string[] par_values = new string[1];

            h.fillwSP_combo_or_list(this, "lstSite", false, "sp_get_sitecodes", par_names, par_values, "SITE_NAME", "SITE_CODE");
            h.fillwSP_combo_or_list(this, "cmbFg", true, "sp_get_funcgrpcodes", par_names, par_values, "FUNC_GRP", "FUNC_GRP_CODE");
        }

        private bool has_selected_criteria()
        {
            bool isSelected = true;

            if (cmbFg.SelectedItem == null)
            {
                MessageBox.Show("You must select a Functional Group", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                isSelected = false;
                return isSelected;
            }

            if (cmbPhTitle.SelectedItem == null)
            {
                MessageBox.Show("You must select a Phenophase Title", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                isSelected = false;
                return isSelected;
            }

            if (lstSite.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select at least one site", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                isSelected = false;
                return isSelected;
            }

            if (lstYear.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select at least one year", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                isSelected = false;
                return isSelected;
            }

            if (lstSp.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select at least one species", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                isSelected = false;
                return isSelected;
            }

            return isSelected;
        }

        private void btnQuery1_Click(object sender, EventArgs e)
        {
            string proc_name = "sp_get_eventresult";

            string phenoTitle = "", FG_Code = "", fgTable = "", siteCodes = "", sppCodes = "", years = "";
            
            if (!has_selected_criteria())
                return;

            FG_Code = cmbFg.SelectedValue.ToString();
            fgTable = FG_Code.ToLower() + "_pheno";

            ArrayList stList = h.Get_MultiSelected_Items(this, "lstSite");
            foreach (string st in stList)
                siteCodes = siteCodes + "'" + st + "',";
            siteCodes = siteCodes.Remove(siteCodes.LastIndexOf(','), 1);

            ArrayList spList = h.Get_MultiSelected_Items(this, "lstSp"); 
            foreach(string sp in spList)
                sppCodes = sppCodes + "'" + sp + "',";
            sppCodes = sppCodes.Remove(sppCodes.LastIndexOf(','), 1);

            ArrayList yrList = h.Get_MultiSelected_Items(this, "lstYear");
            foreach (string y in yrList)
                years = years + y + ",";
            years = years.Remove(years.LastIndexOf(','), 1);

            phenoTitle = cmbPhTitle.SelectedValue.ToString();
            //phenoTitle = phenoTitle.Substring(0, 2) + "_" + phenoTitle.Substring(2, 2);

            MySqlCommand cmd = new MySqlCommand(proc_name);
            cmd.CommandText = "CALL " + proc_name + "(@fg_tbl, @spc_code_lst, @st_code, @ph_title, @syear);";

            cmd.Parameters.AddWithValue("@fg_tbl", fgTable);
            cmd.Parameters.AddWithValue("@spc_code_lst", sppCodes);
            cmd.Parameters.AddWithValue("@st_code", siteCodes);
            cmd.Parameters.AddWithValue("@ph_title", phenoTitle);
            cmd.Parameters.AddWithValue("@syear", years);

            string[] column_names = {"PLANT_ID", "DATE(=0)", "DATE(=1)", "PHENO_TITLE1", "PHENO_TITLE2", "DAYS"};
            column_names[3] = cmbPhTitle.Text + "(=0)";
            column_names[4] = cmbPhTitle.Text + "(=1)";

            int[] column_types = {0, 0, 0, 1, 1, 1};

            dGVResult.DataSource = null;

            DataTable resultData = h.get_table_wCMD(cmd, column_names, column_types);

            if (resultData.Rows.Count <= 0)
                MessageBox.Show("No matching data has been found.", "No Match", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                dGVResult.DataSource = resultData;

            clearComboboxes();
        }
        
        private void cmbFg_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fg_Code = cmbFg.SelectedValue.ToString();
            string fg_Table = fg_Code.ToLower() + "_pheno";

            ArrayList par_nameList = new ArrayList();
            ArrayList par_valueList = new ArrayList();
            par_nameList.Add("fgcode");
            par_valueList.Add(fg_Code);

            string[] par_names = (string[])par_nameList.ToArray(typeof(string));
            string[] par_values = (string[])par_valueList.ToArray(typeof(string));

            if (fg_Code.Length <= 2)
            {
                h.fillwSP_combo_or_list(this, "cmbPhTitle", true, "sp_get_eventcodes", par_names, par_values, "TITLE_DISPLAY_NAME", "TITLE_NAME");
                
                par_nameList.Clear();
                par_nameList.Add("fgTbl");
                par_valueList.Clear();
                par_valueList.Add(fg_Table);

                par_names = (string[])par_nameList.ToArray(typeof(string));
                par_values = (string[])par_valueList.ToArray(typeof(string));

                h.fillwSP_combo_or_list(this, "lstYear", false, "sp_get_years", par_names, par_values, "YEAR", "YEAR");
                
            }
        }

        private void lstSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFg.SelectedValue == null)
                return;

            string siteCodes = "";
            ArrayList stList = h.Get_MultiSelected_Items(this, "lstSite");
            foreach (string st in stList)
                siteCodes = siteCodes + "'" + st + "',";
            siteCodes = siteCodes.Remove(siteCodes.LastIndexOf(','), 1);

            string fg_Code = cmbFg.SelectedValue.ToString();
            
            ArrayList par_nameList = new ArrayList();
            ArrayList par_valueList = new ArrayList();
            par_nameList.Add("fgcode");
            par_nameList.Add("stcode");
            par_valueList.Add(fg_Code);
            par_valueList.Add(siteCodes);

            string[] par_names = (string[])par_nameList.ToArray(typeof(string));
            string[] par_values = (string[])par_valueList.ToArray(typeof(string));

            if (fg_Code.Length <= 2)
            {
                h.fillwSP_combo_or_list(this, "lstSp", false, "sp_get_speciescodes", par_names, par_values, "SPP_CODE", "SPP_CODE");
            }

        }
        
        private void btnQuery2_Click(object sender, EventArgs e)
        {
            string proc_name = "sp_get_statusresult";

            string phenoTitle = "", FG_Code = "", fgTable = "", siteCodes = "", sppCodes = "", years = "", phenoAbundance = "";
            
            if (!has_selected_criteria())
                return;

            FG_Code = cmbFg.SelectedValue.ToString();
            fgTable = FG_Code.ToLower() + "_pheno";
            
            ArrayList stList = h.Get_MultiSelected_Items(this, "lstSite");
            foreach (string st in stList)
                siteCodes = siteCodes + "'" + st + "',";
            siteCodes = siteCodes.Remove(siteCodes.LastIndexOf(','), 1);

            ArrayList spList = h.Get_MultiSelected_Items(this, "lstSp");
            foreach (string sp in spList)
                sppCodes = sppCodes + "'" + sp + "',";
            sppCodes = sppCodes.Remove(sppCodes.LastIndexOf(','), 1);

            ArrayList yrList = h.Get_MultiSelected_Items(this, "lstYear");
            foreach (string y in yrList)
                years = years + y + ",";
            years = years.Remove(years.LastIndexOf(','), 1);

            phenoTitle = cmbPhTitle.SelectedValue.ToString();
            //phenoTitle = phenoTitle.Substring(0, 2) + "_" + phenoTitle.Substring(2, 2);
            phenoAbundance = get_phenoAbundance(phenoTitle);

            MySqlCommand cmd = new MySqlCommand(proc_name);
            cmd.CommandText = "CALL " + proc_name + "(@fg_tbl, @spc_code_lst, @st_code, @ph_title, @syear);";

            cmd.Parameters.AddWithValue("@fg_tbl", fgTable);
            cmd.Parameters.AddWithValue("@spc_code_lst", sppCodes);
            cmd.Parameters.AddWithValue("@st_code", siteCodes);
            cmd.Parameters.AddWithValue("@ph_title", phenoTitle);
            cmd.Parameters.AddWithValue("@syear", years);

            string[] column_names = { "PLANT_ID", "START_DATE", "END_DATE", "DURATION"};
           
            int[] column_types = { 0, 0, 0, 1 };

            dGVResult.DataSource = null;

            DataTable resultData = h.get_table_wCMD(cmd, column_names, column_types);

            if (resultData.Rows.Count <= 0)
                MessageBox.Show("No matching data has been found.", "No Match", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                dGVResult.DataSource = resultData;

            clearComboboxes();
        }

        private void btnQuery3_Click(object sender, EventArgs e)
        {
            string proc_name = "sp_get_abundanceresult";

            string phenoTitle = "", phenoAbundance = "", FG_Code = "", fgTable = "", siteCodes = "", sppCodes = "", years = "";

            if (!has_selected_criteria())
                return;

            phenoTitle = cmbPhTitle.SelectedValue.ToString();
            //-------------------------------------------------
            phenoAbundance = get_phenoAbundance(phenoTitle);
            if (string.IsNullOrEmpty(phenoAbundance))
            {
                MessageBox.Show("No abundance data exists for this phenophase title.\nSelect appropriate phenophase title", "Wrong PhenophaseTitle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //-----------------------------------
            FG_Code = cmbFg.SelectedValue.ToString();
            fgTable = FG_Code.ToLower() + "_pheno";

            ArrayList stList = h.Get_MultiSelected_Items(this, "lstSite");
            foreach (string st in stList)
                siteCodes = siteCodes + "'" + st + "',";
            siteCodes = siteCodes.Remove(siteCodes.LastIndexOf(','), 1);

            ArrayList spList = h.Get_MultiSelected_Items(this, "lstSp");
            foreach (string sp in spList)
                sppCodes = sppCodes + "'" + sp + "',";
            sppCodes = sppCodes.Remove(sppCodes.LastIndexOf(','), 1);

            ArrayList yrList = h.Get_MultiSelected_Items(this, "lstYear");
            foreach (string y in yrList)
                years = years + y + ",";
            years = years.Remove(years.LastIndexOf(','), 1);

            MySqlCommand cmd = new MySqlCommand(proc_name);
            cmd.CommandText = "CALL " + proc_name + "(@fg_tbl, @spc_code_lst, @st_code, @ph_title, @ph_abnd, @syear);";

            cmd.Parameters.AddWithValue("@fg_tbl", fgTable);
            cmd.Parameters.AddWithValue("@spc_code_lst", sppCodes);
            cmd.Parameters.AddWithValue("@st_code", siteCodes);
            cmd.Parameters.AddWithValue("@ph_title", phenoTitle);
            cmd.Parameters.AddWithValue("@ph_abnd", phenoAbundance);
            cmd.Parameters.AddWithValue("@syear", years);

            string[] column_names = { "PLANT_ID", "OBSERVATION_DATE", "ABUNDANCE" };

            int[] column_types = { 0, 0, 1 };

            dGVResult.DataSource = null;

            DataTable resultData = h.get_table_wCMD(cmd, column_names, column_types);

            if (resultData.Rows.Count <= 0)
                MessageBox.Show("No matching data has been found.", "No Match", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                dGVResult.DataSource = resultData;
            
            
            clearComboboxes();
        }

        private string get_phenoAbundance(string phenoTitle)
        {
            string phenoTitleId = "", phenoAbundance = "";
            string[] excluded_events = { "GR_01", "GR_03", "GR_08", "DS_01", "DS_06", "DS_12", "BE_01", "BE_02" };
            
            foreach (string ev in excluded_events)
            {
                if (phenoTitle.Equals(ev))
                {
                    //MessageBox.Show("No abundance data exists for this phenophase title.\nSelect appropriate phenophase title", "Wrong PhenophaseTitle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";
                }
            }
            //--------------------------------------------------
            phenoTitleId = phenoTitle.Substring(3, 2);
            phenoAbundance = phenoTitle.Substring(0, 2) + "_2" + phenoTitleId;
            //---------------------------------
            //exception only for DS
            if (phenoTitle.Equals("DS_213"))
                phenoAbundance = "DS_214";
            //---------------------------------

            return phenoAbundance;
        }

        private ArrayList get_selected_PlantId()
        {
            string plantId = "";
            ArrayList plantids = new ArrayList();

            int selectedRowCount = dGVResult.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                foreach (DataGridViewRow row in dGVResult.SelectedRows)
                {
                    plantId = row.Cells[0].Value.ToString();

                    if (plantids.Count != 0 && !plantids.Contains(plantId))
                    {
                        MessageBox.Show("Please select rows of the same plant", "Multiple Plants Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return plantids;
                    }
                    else
                    {
                        plantids.Add(plantId);
                        return plantids;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select at least one row from query result", "Row Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                chkClimate.Checked = false;
            }
            return plantids;
        }

        private bool checkVariables()
        {
            bool isSelected = true;

            if (cmbVar1.SelectedItem == null)
            {
                MessageBox.Show("You must select variable for Left Y-axis", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                isSelected = false;
            }
            else if (cmbVar2.SelectedItem == null)
            {
                MessageBox.Show("You must select variable for Right Y-axis", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                isSelected = false;
            }

            return isSelected;
        }

        private void clearComboboxes()
        {
            cmbVar1.Text = "";
            cmbVar1.Items.Clear();
            cmbVar1.SelectedIndex = -1;

            cmbVar2.Text = "";
            cmbVar2.Items.Clear();
            cmbVar2.SelectedIndex = -1;
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            string phenoTitle = "", phenoAbundance = "", FG_Code = "", fgTable = "", plantId = "", years = "", var_left = "", var_right = "";
            int axisInterval;
            ArrayList events;
            DateTime[] events_arr;

            ArrayList plantids = get_selected_PlantId();
            if (plantids.Count == 0)
                return;
            
            ArrayList yrList = h.Get_MultiSelected_Items(this, "lstYear");
            foreach (string y in yrList)
                years = years + y + ",";
            years = years.Remove(years.LastIndexOf(','), 1);
            
            phenoTitle = cmbPhTitle.SelectedValue.ToString();
            phenoAbundance = get_phenoAbundance(phenoTitle);

            FG_Code = cmbFg.SelectedValue.ToString();
            fgTable = FG_Code.ToLower() + "_pheno";
            plantId = plantids[0].ToString();

            events = get_eventDates(plantId, years);
            events_arr = events.ToArray(typeof(DateTime)) as DateTime[];

            if (yrList.Count > 1)
                axisInterval = 90;
            else
                axisInterval = 30;
            //------------------------------------------------
            if (chkClimate.Checked == false)
            {
                FrmGraph graphForm = new FrmGraph(axisInterval);
                graphForm.FillChart(yrList, plantId, events_arr, fgTable, phenoTitle, phenoAbundance);
                graphForm.Show();
            }
            else
            {
                if (!checkVariables())
                    return;

                var_left = cmbVar1.SelectedItem.ToString();
                var_right = cmbVar2.SelectedItem.ToString();
             
                Form6 climate_graph = new Form6(axisInterval);
                climate_graph.FillChart(yrList, plantId, events_arr, fgTable, phenoTitle, phenoAbundance, var_left, var_right);
                climate_graph.Show();
            }
            
        }

        private ArrayList get_eventDates(string plant_id, string years)
        {
            ArrayList events = new ArrayList();
            
            MySqlCommand cmd = new MySqlCommand("sp_geteventdates");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@plant_id", plant_id);
            cmd.Parameters.AddWithValue("@syears", years);

            string[] column_names = { "PLANT_ID", "EVENT_DATE"};
            int[] column_types = { 0, 0 };

            DataTable resultTable = h.get_table_wCMD(cmd, column_names, column_types);

            if (resultTable.Rows.Count > 0)
            {
                foreach (DataRow dr in resultTable.Rows)
                {
                    events.Add(Convert.ToDateTime(dr[1].ToString()));
                }
            }

            return events;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            h.export_grid_result(this, "dGVResult");
        }

        private void chkClimate_CheckedChanged(object sender, EventArgs e)
        {
            if (dGVResult.DataSource == null && chkClimate.Checked == false)
            {
                MessageBox.Show("Please run a query and select a plant from the result", "Plant Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //chkClimate.Checked = false;
                return;
            }
            
            if (chkClimate.Checked == true)
            {
                pnlClimate.Visible = true;
                pnlClimate.Enabled = true;
                clearComboboxes();
                Load_Climate_Variables();
            }
            else
            {
                pnlClimate.Visible = false;
                pnlClimate.Enabled = false;
                clearComboboxes();
            }
        }

        private void Load_Climate_Variables()
        {
            Helper h2 = new Helper("phenologyDBConnection");
            MySqlConnectionStringBuilder conStrB = new MySqlConnectionStringBuilder(h2.get_ConnString());
            string database = conStrB.Database;

            ArrayList plantids = get_selected_PlantId();
            if (plantids.Count == 0)
                return;

            string plantid = plantids[0].ToString();
            
            
            string siteid = plantid.Substring(0, 2).ToLower();
            string table_name = siteid + "_std_clim";

            ArrayList vars = h2.get_data("information_schema.columns", "column_name", " WHERE table_schema = '" + database.ToLower() + "' AND table_name = '" +
                                        table_name + "'" + " AND column_name NOT LIKE '%_data' AND column_name NOT IN ('DATE', 'SITE_CODE')");
            h2.populate_control(this, "cmbVar1", vars);
            h2.populate_control(this, "cmbVar2", vars);
        }

        private void dGVResult_DataSourceChanged(object sender, EventArgs e)
        {
            if (chkClimate.Checked == true && dGVResult.DataSource != null)
            {
                chkClimate.Checked = false;
                clearComboboxes();
            }
        }

    }
}
