using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Access
    {
        string AcesConString;
        OleDbConnection AcesConnection;

        public Access()
        {
        }

        public Access(string dataSource)
        {
            AcesConString = string.Format("Provider={0}; Data Source={1};", "Microsoft.ACE.OLEDB.12.0", dataSource);
            AcesConnection = null;
        }
        /*Tests connection to Access Database*/
        public bool TestAccessConn()
        {
            if (OpenAccessConn())
            {
                CloseAccessConn();
                return true;
            }
            else
                return false;
        }

        /*Open connection to Access Database*/
        private bool OpenAccessConn()
        {
            try
            {
                if (AcesConnection == null)
                    AcesConnection = new OleDbConnection(this.AcesConString);

                if (AcesConnection.State == ConnectionState.Closed)
                    AcesConnection.Open();
                return true;
            }
            catch (NullReferenceException exp)
            {
                MessageBox.Show("Unable to instantiate OleDbConnection. "+ exp.Message);
                return false;
            }
            catch (OleDbException exp)
            {
                MessageBox.Show(exp.StackTrace, "MS Access Connection ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        /*Close connection to Access Database*/
        private void CloseAccessConn()
        {
            try
            {
                if (AcesConnection != null && AcesConnection.State == ConnectionState.Open)
                    AcesConnection.Close();
            }
            catch (OleDbException exp)
            {
                MessageBox.Show(exp.StackTrace, "MS Access Connection ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            AcesConnection = null;
        }
        
        /*Insert record to Access Database*/
        public int InsertAcesRecord(string tableName, string[] columnNames, int[] columnTypes, Object[] columnValues)
        {
            string query = "";
            int status = 0;

            //prepare the query string
            query = "INSERT INTO " + tableName + "("; 
            
            for(int j = 0; j < columnNames.Length; j++)
            {
                query = query + "["+ columnNames[j] + "]" + ",";
            }
            query = query.Remove(query.LastIndexOf(','), 1);
            query += ") VALUES(";

            for(int j = 0; j < columnNames.Length; j++)
            {
                query = query + "@"+ columnNames[j].ToUpper() + ",";
            }
            query = query.Remove(query.LastIndexOf(','), 1);
            query += ")";

           
            if(!OpenAccessConn())
                return 0; //connection problem
            try
            {
                //setup the OLEDB command object
                OleDbCommand OleCmd = new OleDbCommand();
                OleCmd.Connection = this.AcesConnection;
                OleCmd.CommandType = CommandType.Text;
                OleCmd.CommandText = query;
                OleCmd.Parameters.Clear();

                //assigning the parameters for OLEDB command object
                for (int i = 0; i < columnTypes.Length; i++)
                {
                    switch (columnTypes[i])
                    {
                        case 0:     //  string or text type
                            OleCmd.Parameters.Add("@" + columnNames[i].ToUpper(), OleDbType.VarWChar).Value = Convert.ToString(columnValues[i]);
                            break;
                        case 1:     //  numeric or integer type
                            OleCmd.Parameters.Add("@" + columnNames[i].ToUpper(), OleDbType.Numeric).Value = Convert.ToInt32(columnValues[i]);
                            break;
                        case 2:
                            OleCmd.Parameters.Add("@" + columnNames[i].ToUpper(), OleDbType.Single).Value = Convert.ToSingle(columnValues[i]);
                            break;
                        case 3:     //  date type
                            OleCmd.Parameters.Add("@" + columnNames[i].ToUpper(), OleDbType.Date).Value = Convert.ToDateTime(columnValues[i]).Date;
                            break;
                        case 4:     //  yes/no type
                            OleCmd.Parameters.Add("@" + columnNames[i].ToUpper(), OleDbType.Boolean).Value = Convert.ToBoolean(columnValues[i]);
                            break;

                    }
                }

                status = OleCmd.ExecuteNonQuery();

            }
            catch (OleDbException exp)
            {
                MessageBox.Show(exp.Message, "MS Access ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseAccessConn();
            }
            return status;
        }

        /*Update record of Access Database*/
        public int UpdateAcesRecord(string tableName, string[] columnNames, Object[] columnValues, string[] conditionColumns, Object[] conditionValues)
        {
            string query = "";
            int status = 0;

            //prepare the query string
            query = "UPDATE " + tableName + " SET ";

            for (int j = 0; j < columnNames.Length; j++)
            {
                query = query + "[" + columnNames[j] + "]" + " = ?,";
            }
            query = query.Remove(query.LastIndexOf(','), 1);

            query = query + " WHERE ";
            for (int j = 0; j < conditionColumns.Length; j++)
            {
                query = query + conditionColumns[j] + " = ? AND ";
            }
            query = query.Remove(query.Length-4, 4); //removing the extra AND

            if (!OpenAccessConn())
                return 0; //connection problem
            try
            {
                //setup the OLEDB command object
                OleDbCommand OleCmd = new OleDbCommand();
                OleCmd.Connection = this.AcesConnection;
                OleCmd.CommandType = CommandType.Text;
                OleCmd.CommandText = query;
                OleCmd.Parameters.Clear();

                //assigning the parameters for OLEDB command object
                for (int i = 0; i < columnValues.Length; i++)
                {
                    OleCmd.Parameters.AddWithValue(columnNames[i], columnValues[i]);
                }
                
                for (int i = 0; i < conditionColumns.Length; i++)
                {
                    OleCmd.Parameters.AddWithValue(conditionColumns[i], conditionValues[i]);
                }

                status = OleCmd.ExecuteNonQuery();

            }
            catch (OleDbException exp)
            {
                MessageBox.Show(exp.StackTrace, "MS Access ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseAccessConn();
            }
            return status;
        }

        /*Check if a record exits in Access Database*/
        public bool isAcesRecordExists(string tableName, string[] conditionColumns, Object[] conditionValues)
        {
            string query = "";
            bool exits = false;

            //prepare the query string
            query = "SELECT * FROM " + tableName + " WHERE ";

            for (int j = 0; j < conditionColumns.Length; j++)
            {
                query = query + conditionColumns[j] + " = ? AND ";
            }
            query = query.Remove(query.Length - 4, 4); //removing the extra AND

            if (!OpenAccessConn())
                return exits; //connection problem
            try
            {
                //setup the OLEDB command object
                OleDbCommand OleCmd = new OleDbCommand();
                OleCmd.Connection = this.AcesConnection;
                OleCmd.CommandType = CommandType.Text;
                OleCmd.CommandText = query;
                OleCmd.Parameters.Clear();

                for (int i = 0; i < conditionColumns.Length; i++)
                {
                    OleCmd.Parameters.AddWithValue(conditionColumns[i], conditionValues[i]);
                }

                OleDbDataReader reader = OleCmd.ExecuteReader();

                if (reader.HasRows)
                    exits = true;
            }
            catch (OleDbException exp)
            {
                MessageBox.Show(exp.StackTrace, "MS Access ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseAccessConn();
            }
            return exits;
        }

        /*Get records from Access Database*/
        public DataTable GetAcesRecord(string tableName, string[] columnNames, string[] conditionColumns, Object[] conditionValues)
        {
            string query = "";
            DataTable dt = new DataTable();

            //prepare the query string
            query = "SELECT ";
            for (int j = 0; j < columnNames.Length; j++)
            {
                query = query + columnNames[j] + ",";
                dt.Columns.Add(columnNames[j], typeof(string));
            }
            query = query.Remove(query.LastIndexOf(','), 1);

            query += " FROM " + tableName + " WHERE ";

            for (int j = 0; j < conditionColumns.Length; j++)
            {
                query = query + conditionColumns[j] + " = ? AND ";
            }
            query = query.Remove(query.Length - 4, 4); //removing the extra AND

            if (!OpenAccessConn())
                return dt; //connection problem
            try
            {
                //setup the OLEDB command object
                OleDbCommand OleCmd = new OleDbCommand();
                OleCmd.Connection = this.AcesConnection;
                OleCmd.CommandType = CommandType.Text;
                OleCmd.CommandText = query;
                OleCmd.Parameters.Clear();

                for (int i = 0; i < conditionColumns.Length; i++)
                {
                    OleCmd.Parameters.AddWithValue(conditionColumns[i], conditionValues[i]);
                }

                OleDbDataReader reader = OleCmd.ExecuteReader();

                if (reader.Read())
                {
                    string[] values = new string[columnNames.Length];
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        if (reader.IsDBNull(i))
                            values[i] = "null";
                        else
                            values[i] = reader.GetString(i);
                    }
                    dt.Rows.Add(values);
                }
            }
            catch (OleDbException exp)
            {
                MessageBox.Show(exp.StackTrace, "MS Access ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseAccessConn();
            }
            return dt;
        }
    }
}
