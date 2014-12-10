using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }
        
        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
           

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            /*OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\phenomet_DB_phenocam_16Sep14.accdb";
            conn.Open();
            string Sitecode = "TR";
            string Observer = "MMM";
            DateTime dt = DateTime.Now.AddDays(1);
            int doy = dt.DayOfYear;
            string query = "INSERT INTO SiteVisitTable([date], [doy], [sitecode], [observers]) VALUES(@Date,@Doy,@Sitecode,@Observer)";
            OleDbCommand cmmd = new OleDbCommand();
            cmmd.Connection = conn;
            cmmd.CommandType = CommandType.Text;
            cmmd.CommandText = query;
            cmmd.Parameters.Clear();

            if (conn.State == ConnectionState.Open)
            {
                cmmd.Parameters.Add("@Date", OleDbType.Date).Value = dt.Date;
                cmmd.Parameters.Add("@Doy", OleDbType.Numeric).Value = doy;
                cmmd.Parameters.Add("@Sitecode", OleDbType.VarWChar, 20).Value = Sitecode;
                cmmd.Parameters.Add("@Observer", OleDbType.VarWChar, 20).Value = Observer;
                
                try
                {
                    cmmd.ExecuteNonQuery();
                    MessageBox.Show("DATA ADDED");
                    conn.Close();
                }
                catch (OleDbException expe)
                {
                    MessageBox.Show(expe.Message);
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("CON FAILED");
            }*/
            //string[] columns = { "date","doy", "sitecode", "observers"};
            //int[] types = { 3, 1, 0 , 0};
            //string Sitecode = "TR";
            //string Observer = "MMM";
            //DateTime dt = DateTime.Now;
            //int doy = dt.DayOfYear;
            //Object[] values = { dt, doy, Sitecode, Observer};
            //Access ace = new Access("D:\\phenomet_DB_phenocam_16Sep14.accdb");
            //int success = ace.InsertAcesRecord("SiteVisitTable", columns, types, values);
            //MessageBox.Show(success.ToString());

            //string[] columns = { "sitecode", "observers" };
            //string[] conds = { "date", "doy" };
            ////int[] types = { 3, 1, 0 , 0};
            //string Sitecode = "SC";
            //string Observer = "GMC";
            //DateTime dt = DateTime.Now.Date;
            //int doy = dt.DayOfYear+1;
            //Object[] colvalues = { Sitecode, Observer};
            //Object[] condvalues = { dt, doy };
            //Access ace = new Access("D:\\phenomet_DB_phenocam_16Sep14.accdb");
            //int success = ace.UpdateAcesRecord("SiteVisitTable", columns, colvalues, conds, condvalues);
            //MessageBox.Show(success.ToString());

            string[] conds = { "date", "doy" };
            DateTime dt = new DateTime(2014, 10, 21);
            int doy = dt.DayOfYear;
            Object[] condvalues = { dt, doy };
            
            Access ace = new Access("D:\\phenomet_DB_phenocam_16Sep14.accdb");
            bool success = ace.isAcesRecordExists("SiteVisitTable", conds, condvalues);
            MessageBox.Show(success.ToString());

        }

        
    }
}
