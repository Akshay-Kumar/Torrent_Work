using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SourceCodeDAL
{
    public interface Action
    {   
        /*Methods*/
        DataSet ExecuteDataSet(out SqlCommand cmd, string procedureName, params SqlParameter[] parameters);
        DataTable ExecuteDataTable(out SqlCommand cmd, string procedureName, params SqlParameter[] parameters);
        int ExecuteUpdate(out SqlCommand cmd, string procedureName, params SqlParameter[] parameters);
        DataTable ExecuteFunction(out SqlCommand cmd, string functionName, params SqlParameter[] parameters);
    }
}
