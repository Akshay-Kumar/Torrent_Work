using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using Logger;

namespace SourceCodeDAL
{
    public class DBConnection 
    {
        private static SqlConnection connection = null;

        public static SqlConnection Connection
        {
            get { return DBConnection.connection; }
            set { DBConnection.connection = value; }
        }

        public static bool IsDatabaseOnline()
        {
            string con = GetConnectionString();
            if (!string.IsNullOrEmpty(con))
            {
                //Monitor.ProcessMonitor.Instance().Monitor("Notification : ", "Obtained connection string.", Monitor.ComConfig.Notification.Information);
            }
            bool isConnected = false;
            SqlConnection connect = null;

            try
            {
                connect = new SqlConnection(con);
                connect.Open();
                isConnected = true;
            }
            catch (Exception e)
            {
                isConnected = false;
            }
            finally
            {
                if (connect != null)
                    connect.Close();
            }
            return isConnected;
        }

        public static void OpenSqlConnection()
        {
            SqlConnection con = Instance();
            if (con.State == System.Data.ConnectionState.Closed)
            {
                //Monitor.ProcessMonitor.Instance().Monitor("Notification : ", "Opening connection.", Monitor.ComConfig.Notification.Information);
                con.Open();
            }
        }
        public static void CloseSqlConnection()
        {
            SqlConnection con = Instance();
            if (con.State == System.Data.ConnectionState.Open)
            {
                //Monitor.ProcessMonitor.Instance().Monitor("Notification : ", "Closing connection.", Monitor.ComConfig.Notification.Information);
                con.Close();
            }
        }
        static private string GetConnectionString()
        {
            return GlobalClass.SOURCE_CODE_CONNECTION_STRING;
        }
        public static SqlConnection Instance()
        {
            try
            {
                if (connection == null)
                {
                    string connectionString = GetConnectionString();
                    connection = new SqlConnection(connectionString);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            return connection;
        }
    }
}
