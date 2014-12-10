using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Helper testh = new Helper();
            
            //get the phenophase database connString
            string phConstring = testh.GetConnectionStringByName("phenophaseDBConnection");
            if (!DBConnectionStatus(phConstring))
                MessageBox.Show("Could not connect to the phenophase database. Please check the connection string.", "DATABASE Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            //get the climate database connString
            string clConstring = testh.GetConnectionStringByName("phenologyDBConnection");
            if (!DBConnectionStatus(clConstring))
                MessageBox.Show("Could not connect to the climate database. Please check the connection string.", "DATABASE Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private static bool DBConnectionStatus(string connString)
        {
            try
            {
                using (MySqlConnection Conn = new MySqlConnection(connString))
                {
                    Conn.Open();
                    return (Conn.State == ConnectionState.Open);
                }
            }
            catch (MySqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        private void insertDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If FrmInsertData is not already open
            if (FrmInsertData.CheckInst == null)
            {
                FrmInsertData.CreateInst.Show();  // This creates and displays Form2
            }
            else
            {
                // These two lines make sure the state is normal (not min or max) and give it focus.
                FrmInsertData.CreateInst.WindowState = FormWindowState.Normal;
                FrmInsertData.CheckInst.Focus();
            }
        }

        private void viewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If FrmViewData is not already open
            if (FrmViewData.CheckInst == null)
            {
                FrmViewData.CreateInst.Show();  // This creates and displays Form2
            }
            else
            {
                // These two lines make sure the state is normal (not min or max) and give it focus.
                FrmViewData.CreateInst.WindowState = FormWindowState.Normal;
                FrmViewData.CheckInst.Focus();
            }
           
        }

        private void editDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If FrmEditData is not already open
            if (FrmEditData.CheckInst == null)
            {
                FrmEditData.CreateInst.Show();  // This creates and displays Form2
            }
            else
            {
                // These two lines make sure the state is normal (not min or max) and give it focus.
                FrmEditData.CreateInst.WindowState = FormWindowState.Normal;
                FrmEditData.CheckInst.Focus();
            }
        }

        private void importDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If FrmImportData is not already open
            if (FrmImportData.CheckInst == null)
            {
                FrmImportData.CreateInst.Show();  // This creates and displays Form2
            }
            else
            {
                // These two lines make sure the state is normal (not min or max) and give it focus.
                FrmImportData.CreateInst.WindowState = FormWindowState.Normal;
                FrmImportData.CheckInst.Focus();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // The user wants to exit the application. Close everything down.
            Application.Exit();
        }

        private void queriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If FrmQuery is not already open
            if (FrmQuery.CheckInst == null)
            {
                FrmQuery.CreateInst.Show();  // This creates and displays Form2
            }
            else
            {
                // These two lines make sure the state is normal (not min or max) and give it focus.
                FrmQuery.CreateInst.WindowState = FormWindowState.Normal;
                FrmQuery.CheckInst.Focus();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog();
        }

        
    }
}
