using Logger;
using System;
using System.Data;
using System.Data.SqlClient;
namespace SourceCodeDAL
{
    public class DataAccessAction : Action
    {
        public static SqlDataAdapter dataAdapter = null;
        public static DataSet dataSet = null;
        public static DataTable dataTable = null;
        public static SqlConnection connection = null;
        public DataAccessAction()
        {
            try
            {
                DataAccessAction.connection = DBConnection.Instance();
            }
            catch (SqlException dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
        }
        public DataSet ExecuteDataSet(out SqlCommand cmd, string procedureName, params SqlParameter[] parameters)
        {
            cmd = new SqlCommand();
            try
            {
                this.Initialize(cmd, procedureName, CommandType.StoredProcedure, parameters);
                DataAccessAction.dataSet = new DataSet();
                DataAccessAction.dataAdapter = new SqlDataAdapter(cmd);
                DataAccessAction.dataAdapter.Fill(DataAccessAction.dataSet, "LocalTable");
            }
            catch (SqlException dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return DataAccessAction.dataSet;
        }
        public DataTable ExecuteDataTable(out SqlCommand cmd, string procedureName, params SqlParameter[] parameters)
        {
            cmd = new SqlCommand();
            try
            {
                this.Initialize(cmd, procedureName, CommandType.StoredProcedure, parameters);
                DataAccessAction.dataTable = new DataTable();
                DataAccessAction.dataAdapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder cb = new SqlCommandBuilder(DataAccessAction.dataAdapter);
                DataAccessAction.dataAdapter.Fill(DataAccessAction.dataTable);
            }
            catch (SqlException dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return DataAccessAction.dataTable;
        }
        public int ExecuteUpdate(out SqlCommand cmd, string procedureName, params SqlParameter[] parameters)
        {
            int result = 0;
            cmd = new SqlCommand();
            try
            {
                this.Initialize(cmd, procedureName, CommandType.StoredProcedure, parameters);
                DBConnection.OpenSqlConnection();
                result = cmd.ExecuteNonQuery();
                DBConnection.CloseSqlConnection();
            }
            catch (SqlException dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return result;
        }
        public DataTable ExecuteFunction(out SqlCommand cmd, string functionName, params SqlParameter[] parameters)
        {
            cmd = new SqlCommand();
            try
            {
                this.Initialize(cmd, "SELECT " + functionName, CommandType.Text, parameters);
                DataAccessAction.dataTable = new DataTable();
                DataAccessAction.dataAdapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder cb = new SqlCommandBuilder(DataAccessAction.dataAdapter);
                DataAccessAction.dataAdapter.Fill(DataAccessAction.dataTable);
            }
            catch (SqlException dalException)
            {
                ErrorLog.ErrorRoutine(dalException);
            }
            return DataAccessAction.dataTable;
        }
        public void Initialize(SqlCommand cmd, string procedureName, CommandType commandType, params SqlParameter[] parameters)
        {
            if (parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    SqlParameter param = parameters[i];
                    cmd.Parameters.Add(param);
                }
            }
            cmd.Connection = DataAccessAction.connection;
            cmd.CommandType = commandType;
            cmd.CommandText = procedureName;
        }
    }
}
