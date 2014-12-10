using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Collections;
using System.IO;

namespace WindowsFormsApplication1
{
    class Helper
    {
        string connString;
        MySqlConnection connection;

        public Helper()
        {
        }

        public Helper(string conStrName)
        {
            connString = GetConnectionStringByName(conStrName);
            connection = null;
        }

        public string GetConnectionStringByName(string conStrName)
        {
            string connStr = "";
            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[conStrName];

            // If found, return the connection string. 
            if (settings != null)
                connStr = settings.ConnectionString;
            
            return connStr;
        }

        public string get_ConnString()
        {
            return this.connString;
        }

        public MySqlConnection get_DBconnection()
        {
            return this.connection;
        }

        public void open_DBconnection()
        {
            this.connection = new MySqlConnection(this.connString);
            if (this.connection.State == ConnectionState.Closed)
                this.connection.Open();
        }

        public void close_DBconnection()
        {
            if (this.connection != null && this.connection.State == ConnectionState.Open)
                this.connection.Close();
            this.connection = null;
        }

        public Control get_control(Form fr, string control_name)
        {
            Control ctrl = null;
            var CTRL = fr.Controls.Find(control_name, true);
            if ((CTRL != null) && (CTRL.Length != 0))
            {
                ctrl = CTRL[0];
            }
            return ctrl;
        }

        private DataSet GetDataFromDBtoDS(string sp_name, string[] param_names, string[] param_values)
        {
            DataSet ds = new DataSet();
            bool param_included = false;

            string commandtext = "CALL " + sp_name; 
            try
            {
                using (MySqlConnection sqlCon = new MySqlConnection(this.connString))
                {
                    using (MySqlDataAdapter SqlDa = new MySqlDataAdapter(sp_name, sqlCon))
                    {
                        commandtext += "(";
                        for(int i = 0; i < param_names.Length; i++)
                        {
                            
                            if (!string.IsNullOrEmpty(param_names[i]))
                            {
                                commandtext += "@" + param_names[i] + ",";
                                SqlDa.SelectCommand.Parameters.AddWithValue("@" + param_names[i], param_values[i]);
                                param_included = true;
                            }
                        }
                        if(param_included)
                            commandtext = commandtext.Remove(commandtext.LastIndexOf(','), 1);
                        commandtext += ");";
                        SqlDa.SelectCommand.CommandText = commandtext;
                        SqlDa.Fill(ds);
                    }
                }

            }
            catch (MySqlException exp)
            {
                MessageBox.Show("MySQL Error: " + exp.ToString(), "Database ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ds;
        }

        public void fillwSP_combo_or_list(Form f, string comboName, bool isCombo, string stdProc, string[] pr_name, string[] pr_value, string disMember, string valMember)
        {
            DataSet dsDataFromDB = null;
            dsDataFromDB = GetDataFromDBtoDS(stdProc, pr_name, pr_value);
            Control ctrl = get_control(f, comboName);

            if (isCombo)
            {
                ((ComboBox)ctrl).DataSource = null;
                ((ComboBox)ctrl).Items.Clear();
                ((ComboBox)ctrl).DataSource = dsDataFromDB.Tables[0];
                ((ComboBox)ctrl).DisplayMember = disMember;
                ((ComboBox)ctrl).ValueMember = valMember;
            }
            else
            {
                ((ListBox)ctrl).DataSource = null;
                ((ListBox)ctrl).Items.Clear();
                ((ListBox)ctrl).DataSource = dsDataFromDB.Tables[0];
                ((ListBox)ctrl).DisplayMember = disMember;
                ((ListBox)ctrl).ValueMember = valMember;
            }
        }

        private void fillwQR_combo_or_list(Form f, string ctrlName, bool isCombo, string tableName, ArrayList columnList, string condition, string disMember, string valMember)
        {
            DataTable dt = null;
            dt = get_table(tableName, columnList, condition);
            Control ctrl = get_control(f, ctrlName);

            if (isCombo)
            {
                ((ComboBox)ctrl).Items.Clear();
                ((ComboBox)ctrl).DataSource = dt;
                ((ComboBox)ctrl).DisplayMember = disMember;
                ((ComboBox)ctrl).ValueMember = valMember;
            }
            else
            {
                ((ListBox)ctrl).Items.Clear();
                ((ListBox)ctrl).DataSource = dt;
                ((ListBox)ctrl).DisplayMember = disMember;
                ((ListBox)ctrl).ValueMember = valMember;
            }
        }

        public void populate_control(Form f, string control_name, ArrayList valueList)
        {
            Control ctrl = get_control(f, control_name);

            if (control_name.StartsWith("cmb"))
                ((ComboBox)ctrl).Items.Clear();
            else if (control_name.StartsWith("lst"))
                ((ListBox)ctrl).Items.Clear();
            else
                ((CheckedListBox)ctrl).Items.Clear();

            foreach (string v in valueList)
            {
                if (control_name.StartsWith("cmb"))
                    ((ComboBox)ctrl).Items.Add(v);
                else if (control_name.StartsWith("lst"))
                    ((ListBox)ctrl).Items.Add(v);
                else
                    ((CheckedListBox)ctrl).Items.Add(v);
            }
        }

        public ArrayList get_data(string table_name, string column_name, string condition)
        {
            MySqlCommand cmd = null;
            MySqlDataReader dr = null;

            string value = "";
            ArrayList values = new ArrayList();
            
            try
            {
                open_DBconnection();

                string sql = "SELECT DISTINCT " + column_name + " FROM " + table_name + condition;

                cmd = new MySqlCommand(sql);
                cmd.Connection = get_DBconnection();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        value = dr.GetString(0);
                        values.Add(value);
                    }
                }
            }
            catch (MySqlException exp)
            {
                MessageBox.Show("MySQL Error: " + exp.ToString(), "Database ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                close_DBconnection();
            }
            return values;
        }

        public DataTable get_table(string table_name, ArrayList column_list, string condition)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataReader dr = null;

            DataTable dt = new DataTable();

            try
            {
                conn = new MySqlConnection(connString);
                conn.Open();

                string sql = "SELECT ";

                foreach (string col in column_list)
                {
                    sql = sql + col + ",";
                    dt.Columns.Add(col, typeof(string));
                }
                sql = sql.Remove(sql.LastIndexOf(','), 1);

                sql = sql + " FROM " + table_name + condition;

                cmd = new MySqlCommand(sql, conn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string[] values = new string[column_list.Count];
                    for (int i = 0; i < column_list.Count; i++)
                    {
                        if (dr.IsDBNull(i))
                            values[i] = "null";
                        else
                            values[i] = dr.GetString(i);

                    }
                    dt.Rows.Add(values);
                }
            }
            catch (MySqlException exp)
            {
                MessageBox.Show("MySQL Error: " + exp.ToString(), "Database ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                close_DBconnection();
            }

            return dt;
        }
       
        public DataTable get_table_wCMD(MySqlCommand cmd, string[] column_names, int[] column_types)
        {
            MySqlDataReader dr = null;
            string column = "";
            DataTable dt = new DataTable();

            try
            {
                open_DBconnection();
                cmd.Connection = get_DBconnection();

                for(int i = 0; i < column_names.Length; i++)
                {
                    column = column_names[i];
                    Type type = null;
                    switch (column_types[i])
                    {
                        case 0:
                            type = typeof(String);
                            break;
                        case 1:
                            type = typeof(Int32);
                            break;
                        case 2:
                            type = typeof(Double);
                            break;
                        case 3:
                            type = typeof(DateTime);
                            break;
                    }

                    dt.Columns.Add(column, type);
                }
                
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Object[] values = new Object[column_names.Length];
                    for (int i = 0; i < column_names.Length; i++)
                    {
                        switch (column_types[i])
                        {
                            case 0:
                                if (dr.IsDBNull(i))
                                    values[i] = "null";
                                else
                                    values[i] = dr.GetString(i);
                                break;
                            case 1:
                                if (dr.IsDBNull(i))
                                    values[i] = -99;
                                else
                                    values[i] = dr.GetInt32(i);
                                break;
                            case 2:
                                values[i] = dr.GetDouble(i);
                                break;
                            case 3:
                                values[i] = dr.GetDateTime(i);
                                break;
                        }
                        
                    }
                    dt.Rows.Add(values);
                }
            }
            catch (MySqlException exp)
            {
                MessageBox.Show("MySQL Error: " + exp.ToString(), "Database ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                close_DBconnection();
            }

            return dt;
        }

        public bool check_data_exists(string table_name, string column_name, string condition)
        {
            MySqlCommand cmd = null;
            MySqlDataReader dr = null;
            
            bool exists = false;

            try
            {
                open_DBconnection();

                string sql = "SELECT " + column_name + " FROM " + table_name + condition;

                cmd = new MySqlCommand(sql);
                cmd.Connection = get_DBconnection();

                dr = cmd.ExecuteReader();

                if(dr.HasRows)
                {
                    exists = true;
                }
            }
            catch (MySqlException exp)
            {
                MessageBox.Show("MySQL Error: " + exp.ToString(), "Database ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                close_DBconnection();
            }
            return exists;
        }

        public ArrayList Get_MultiSelected_Items(Form f, string control_name)
        {
            Control ctrl = get_control(f, control_name);

            ArrayList selectedvalues = new ArrayList();
            int itemcount;
            itemcount = ((ListBox)ctrl).SelectedItems.Count;

            for (int i = 0; i < itemcount; i++)
            {
                DataRowView drv = ((ListBox)ctrl).SelectedItems[i] as DataRowView;
                selectedvalues.Add(drv[0].ToString());
            }

            return selectedvalues;
        }

        public void openDialog(Form fr, string filter, string ctrlName)
        {
            string directoryForDialog = Directory.GetCurrentDirectory();

            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = directoryForDialog;
            openFileDialog1.Filter = filter;
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            Control ctrl = get_control(fr, ctrlName);

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            directoryForDialog = openFileDialog1.FileName;
                            ((TextBox)ctrl).Text = openFileDialog1.FileName;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not read file from disk. Original error: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void saveDialog(Form fr, string ctrlName)
        {
            // Create a new save file dialog
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();


            Control ctrl = get_control(fr, ctrlName);
            // Sets the current file name filter string, which determines 
            // the choices that appear in the "Save as file type" or 
            // "Files of type" box in the dialog box.
            saveFileDialog1.Filter = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg|EMF (*.emf)|*.emf|PNG (*.png)|*.png|SVG (*.svg)|*.svg|GIF (*.gif)|*.gif|TIFF (*.tif)|*.tif";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            // Set image file format
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.Windows.Forms.DataVisualization.Charting.ChartImageFormat format = System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Bmp;

                if (saveFileDialog1.FileName.EndsWith("bmp"))
                {
                    format = System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Bmp;
                }
                else if (saveFileDialog1.FileName.EndsWith("jpg"))
                {
                    format = System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Jpeg;
                }
                else if (saveFileDialog1.FileName.EndsWith("emf"))
                {
                    format = System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Emf;
                }
                else if (saveFileDialog1.FileName.EndsWith("gif"))
                {
                    format = System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Gif;
                }
                else if (saveFileDialog1.FileName.EndsWith("png"))
                {
                    format = System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png;
                }
                else if (saveFileDialog1.FileName.EndsWith("tif"))
                {
                    format = System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Tiff;
                }
               
                // Save image
                ((System.Windows.Forms.DataVisualization.Charting.Chart)ctrl).SaveImage(saveFileDialog1.FileName, format);
            }

        }

        public int insert_data(ArrayList SqlCommands)
        {
            MySqlCommand cmd = null;
            MySqlTransaction trans = null;

            int rows_effected = 0;
            string sql = "";

            if (connection != null && connection.State == ConnectionState.Closed)
            {
                MessageBox.Show("Connection to database is not open", "Connection ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            try
            {
                trans = connection.BeginTransaction();

                foreach (string commandString in SqlCommands)
                {
                    sql = commandString;
                    cmd = new MySqlCommand(commandString, connection, trans);
                    rows_effected += cmd.ExecuteNonQuery();
                }

                trans.Commit();
            }
            catch (MySqlException exp)
            {
                if (trans != null)
                    trans.Rollback();
                //MessageBox.Show("SQL: " + sql + "\n.MySQL Error No: " + exp.Number + get_exception_message(sql, exp.Number) + " Error:" + exp.ToString(), "Database ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(get_exception_message(sql, exp.Number) + "\n\nError Detail: " + exp.Message, "Database ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return rows_effected;
        }

        public int update_data(ArrayList SqlCommands)
        {
            MySqlCommand cmd = null;
            MySqlTransaction trans = null;

            int rows_effected = 0;
            string sql = "";

            if (connection != null && connection.State == ConnectionState.Closed)
            {
                MessageBox.Show("Connection to database is not open", "Connection ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            try
            {
                trans = connection.BeginTransaction();

                foreach (string commandString in SqlCommands)
                {
                    sql = commandString;
                    cmd = new MySqlCommand(commandString, connection, trans);
                    rows_effected += cmd.ExecuteNonQuery();
                }

                trans.Commit();
            }
            catch (MySqlException exp)
            {
                if (trans != null)
                    trans.Rollback();
                MessageBox.Show("SQL: " + sql + ".MySQL Error No: " + exp.Number + get_exception_message(sql, exp.Number) + " Error:" + exp.ToString(), "Database ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show(get_exception_message(sql, exp.Number) + "\n\nError Detail: " + exp.Message, "Database ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return rows_effected;
        }

        public string get_exception_message(string sql, int exceptionNo)
        {
            string msg = "";
            string table = find_tableName_exception(sql);

            switch (exceptionNo)
            {
                case 1451:
                    msg = "Update or Delete error on parent table" + table.Trim().ToUpper();
                    break;
                case 1452:
                    msg = "Foreign Key Constraint in " + table.Trim().ToUpper();
                    break;
                case 1062:
                    msg = "Duplicate record identified in " + table.Trim().ToUpper();
                    break;
            }
            return msg;
        }

        public string find_tableName_exception(string sql)
        {
            string tableName = "";
            string prefix = "INTO";
            int startIndex = sql.IndexOf(prefix) + prefix.Length;
            int endIndex = sql.IndexOf("(");

            tableName = sql.Substring(startIndex, endIndex - startIndex);

            return tableName;
        }

        public void export_grid_result(Form f, string control_name)
        {
            StreamWriter sw = null;
            Stream myStream = null;

            Control ctrl = get_control(f, control_name);

            if (((DataGridView)ctrl).RowCount == 0)
            {
                MessageBox.Show("Nothing to Export", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Export Excel File To";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                
                string str = "";
                try
                {
                    if ((myStream = saveFileDialog.OpenFile()) != null)
                    {
                       sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
                       for (int i = 0; i < ((DataGridView)ctrl).ColumnCount; i++)
                       {
                            if (i > 0)
                            {
                                str += "\t";
                            }
                            str += ((DataGridView)ctrl).Columns[i].HeaderText;
                        }

                        sw.WriteLine(str);

                        for (int j = 0; j < ((DataGridView)ctrl).Rows.Count; j++)
                        {
                            string tempStr = "";
                            for (int k = 0; k < ((DataGridView)ctrl).Columns.Count; k++)
                            {
                                if (k > 0)
                                {
                                    tempStr += "\t";
                                }
                                if (((DataGridView)ctrl).Rows[j].Cells[k].Value != null)
                                    tempStr += ((DataGridView)ctrl).Rows[j].Cells[k].Value.ToString();
                            }
                                sw.WriteLine(tempStr);
                        }
                        MessageBox.Show("Data has been exported successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString(), "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (sw != null)
                        sw.Close();
                    if (myStream != null)
                        myStream.Close();
                }
            }
        } //end of export

        public void setDates(Form fr, string sdateCtrl, string edateCtrl)
        {
            ArrayList maxmin_date = new ArrayList();
            maxmin_date.Add("MIN(DATE)");
            maxmin_date.Add("MAX(DATE)");
            DataTable dt = get_table("date_doy", maxmin_date, "");

            DateTime st_date, en_date;
            Control sdCtrl = get_control(fr, sdateCtrl);
            Control edCtrl = get_control(fr, edateCtrl);

            if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                st_date = Convert.ToDateTime(dr[0].ToString());
                en_date = Convert.ToDateTime(dr[1].ToString());
                ((DateTimePicker)sdCtrl).MinDate = st_date;
                ((DateTimePicker)sdCtrl).MaxDate = en_date;
                ((DateTimePicker)edCtrl).MinDate = st_date;
                ((DateTimePicker)edCtrl).MaxDate = en_date;
                
            }
        }

        public string table_to_field(string key)
        {
            string value = "";

            Dictionary<string, string> columnD = new Dictionary<string, string>();
            columnD.Add("pg", "GR");
            columnD.Add("es", "BE");
            columnD.Add("ds", "DS");
            columnD.Add("su", "CA");
            try
            {
                value = columnD[key];
            }
            catch (KeyNotFoundException)
            {
                return value;
            }
            return value;
        }
    }
}
