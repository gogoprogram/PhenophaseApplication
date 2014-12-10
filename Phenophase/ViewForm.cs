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

namespace WindowsFormsApplication1
{
    public partial class FrmViewData : Form
    {
        Helper h;
        ArrayList viewcolumns = null;

        /*------------------------------------*/
        private static FrmViewData sInst = null;
        //-------------------------------------
        public FrmViewData()
        {
            InitializeComponent();
            h = new Helper("phenophaseDBConnection");
        }

        // Create a public static property that returns the state of the instance
        public static FrmViewData CheckInst
        {
            get
            {
                return sInst;
            }
        }

        // Create a public static property that will create an instance of the form and return it
        public static FrmViewData CreateInst
        {
            get
            {
                if (sInst == null)
                    sInst = new FrmViewData();
                return sInst;
            }
        }

        // Need to override the OnFormClosing event to set the Instance to null
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
                sInst = null;
            }
        }

        //----------------------------------------------------------
        private void FrmView_Load(object sender, EventArgs e)
        {
            init_criteria();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string columns = "", Sql = "", viewName = "", sites = "", plants = "";
            ArrayList sitems = new ArrayList();

            DateTime start_date = dtpStart.Value.Date;
            DateTime end_date = dtpEnd.Value.Date;

            if (cmbVFG.SelectedItem == null)
            {
                MessageBox.Show("You must select a Functional Group", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (lstSite.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select a site", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (lstPlant.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select a plant", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            chkLstColumns.SetItemCheckState(0, CheckState.Checked);

            foreach (object itemChecked in chkLstColumns.CheckedItems)
            {
                columns = columns + itemChecked.ToString() + ",";
            }
            columns = columns.Remove(columns.LastIndexOf(','), 1);

            viewName = get_view_name();

            sitems = h.Get_MultiSelected_Items(this, "lstSite");
            sites = get_IN_query(sitems);

            sitems.Clear();

            foreach (Object selecteditem in lstPlant.SelectedItems)
            {
                sitems.Add(selecteditem.ToString());
            }
            plants = get_IN_query(sitems);

            Sql = "SELECT " + columns + " FROM " + viewName + " WHERE DATE BETWEEN '" + start_date.ToString("yyyy-MM-dd") + "' AND '" + end_date.ToString("yyyy-MM-dd") + "'" +
                   " AND Site IN" + sites + " AND PlantID IN" + plants;
            FillGridView(Sql);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            h.export_grid_result(this, "dataGridView1");
        }

        private void btnClearData_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }

        //-----------------------------------------------------------
        private void FillGridView(string selectCommand)
        {
            MySqlDataAdapter dataAdapter = null;
            try
            {
                // Create a new data adapter based on the specified query.
                dataAdapter = new MySqlDataAdapter(selectCommand, h.get_ConnString());

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand. These are used to update the database.
                MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the DataSource.
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);

                if (table.Rows.Count <= 0)
                    MessageBox.Show("No matching data has been found.", "No Match", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    dataGridView1.DataSource = table;
                    setColumnTooltips();
                }
                // Resize the DataGridView columns to fit the newly loaded content.
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
            }
            catch (MySqlException e)
            {
                MessageBox.Show("DateGridView Error: " + e.ToString());
            }
        }

        private void setColumnTooltips()
        {
            ArrayList event_funcgrp = new ArrayList();
            event_funcgrp.Add("TITLE_NAME");
            event_funcgrp.Add("TITLE_DISPLAY_NAME");

            string FG_Code = cmbVFG.SelectedValue.ToString();
            string cond = " WHERE FUNC_GRP_CODE = '" + FG_Code + "'";

            DataTable dt = h.get_table("pheno_title_info", event_funcgrp, cond);

            Dictionary<string, string> event_dict = new Dictionary<string, string>();
            foreach (DataRow r in dt.Rows)
            {
                event_dict.Add(r["TITLE_NAME"].ToString(), r["TITLE_DISPLAY_NAME"].ToString());
            }

            foreach (DataGridViewColumn dgvC in dataGridView1.Columns)
            {
                if (event_dict.ContainsKey(dgvC.HeaderText))
                {
                    dgvC.ToolTipText = event_dict[dgvC.HeaderText];
                    dgvC.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }

        private void init_criteria()
        {
            h.setDates(this, "dtpStart", "dtpEnd");

            viewcolumns = new ArrayList();
            //viewcolumns.Clear();
            add_viewcolumns();

            string[] par_names = new string[1];
            string[] par_values = new string[1];

            h.fillwSP_combo_or_list(this, "lstSite", false, "sp_get_sitecodes", par_names, par_values, "SITE_NAME", "SITE_CODE");
            h.fillwSP_combo_or_list(this, "cmbVFG", true, "sp_get_funcgrpcodes", par_names, par_values, "FUNC_GRP", "FUNC_GRP_CODE");

            //ArrayList plantIDs = h.get_data("focal_plant_info", "PLANT_ID", "");
            //h.populate_control(this, "cmbEPlantID", plantIDs);
        }

        private void add_viewcolumns()
        {
            //string cond = "";
            ArrayList vcols = new ArrayList();

            vcols.Add("Date");
            vcols.Add("JulianDay");
            vcols.Add("Site");
            vcols.Add("PlantID");
            vcols.Add("SpeciesCode");
            vcols.Add("FuncGrpCode");
            vcols.Add("ObserverID");
            vcols.Add("SiteNotes");
            vcols.Add("PlantNotes");
            vcols.Add("PhotoName");
            copy_to_viewcolumns(vcols);

            //cond = " WHERE table_name = 'site_visit' AND column_name NOT LIKE '%_CODE'";
            //vcols = get_data("information_schema.columns", "column_name", cond);
            //copy_to_viewcolumns(vcols);
            //vcols.Clear();

            //cond = " WHERE table_name = 'focal_plant_info' AND (column_name LIKE '%_CODE' OR column_name LIKE '%_ID')";
            //vcols = get_data("information_schema.columns", "column_name", cond);
            //copy_to_viewcolumns(vcols);

            //cond = " WHERE table_name = 'site_note' AND column_name LIKE '%_NOTES'";
            //vcols = get_data("information_schema.columns", "column_name", cond);
            //copy_to_viewcolumns(vcols);

            //cond = " WHERE table_name = 'plant_note' AND column_name LIKE '%_NOTES'";
            //vcols = get_data("information_schema.columns", "column_name", cond);
            //copy_to_viewcolumns(vcols);

        }

        private void copy_to_viewcolumns(ArrayList clist)
        {
            foreach (string col in clist)
            {
                viewcolumns.Add(col);
            }
        }

        private void cmbVFG_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sc = "", FG_Code = "";
            ArrayList sitems = null;
            if (lstSite.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select a site", "Input ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            sitems = h.Get_MultiSelected_Items(this, "lstSite");
            sc = get_IN_query(sitems);

            FG_Code = cmbVFG.SelectedValue.ToString();

            string cond = " WHERE SITE_CODE IN " + sc + " AND FUNC_GRP_CODE = '" + FG_Code + "'";

            ArrayList plantIDs = h.get_data("focal_plant_info", "PLANT_ID", cond);
            h.populate_control(this, "lstPlant", plantIDs);

            fill_chkListBox(FG_Code);
        }

        private void fill_chkListBox(string fgcode)
        {
            string cond = "", colcode = "";
            fgcode = fgcode.ToLower();

            chkLstColumns.Items.Clear();

            foreach (string c in viewcolumns)
                chkLstColumns.Items.Add(c, false);

            chkLstColumns.SetItemChecked(0, true); //set Date as checked
            chkLstColumns.SetItemChecked(3, true); //set PlantID as checked


            colcode = h.table_to_field(fgcode);
            if (colcode.Equals(""))
                return;
            else
                cond = " WHERE table_name = '" + fgcode + "_pheno' AND column_name LIKE '" + colcode + "_%'";

            ArrayList fgcolumns = h.get_data("information_schema.columns", "column_name", cond);

            foreach (string c in fgcolumns)
                chkLstColumns.Items.Add(c, true);
        }

        private string get_view_name()
        {
            string view_name = "";
            string fgCode = cmbVFG.SelectedValue.ToString();

            switch (fgCode)
            {
                case "PG":
                    view_name = "vw_pg";
                    break;
                case "DS":
                    view_name = "vw_ds";
                    break;
                case "ES":
                    view_name = "vw_es";
                    break;
                case "SU":
                    view_name = "vw_su";
                    break;
            }

            return view_name;
        }

        private string get_IN_query(ArrayList selecteditems)
        {
            string items = "(";
            foreach (string item in selecteditems)
            {
                items = items + "'" + item + "'" + ",";
            }
            items = items.Remove(items.LastIndexOf(','), 1);
            items += ")";

            return items;
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            dtpEnd.Value = dtpStart.Value.Date;
        }

        //-----------------------------------------------------------
        

        
    }
}
