using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace NjhLib.Utils.DB
{
    public class SQLHelper
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public static string ConnectionString
        {
            get { return SQLHelper.connectionString; }
        }

        public static int ExecuteNonQuery(string cmdText, CommandType cmdType, params SqlParameter[] parameters)
        {
            SqlCommand cmd = GetCommand(cmdText, cmdType, parameters);
            int result = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return result;
        }

        public static SqlDataReader ExecuteReader(string cmdText, CommandType cmdType, params SqlParameter[] parameters)
        {
            SqlCommand cmd = GetCommand(cmdText, cmdType, parameters);
            SqlDataReader result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return result;
        }

        public static object ExecuteScalar(string cmdText, CommandType cmdType, params SqlParameter[] parameters)
        {
            SqlCommand cmd = GetCommand(cmdText, cmdType, parameters);
            object result = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return result;
        }


        private static SqlCommand GetCommand(string cmdText, CommandType cmdType, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.Connection = new SqlConnection(connectionString);
            if (parameters != null)
            {
                foreach (SqlParameter p in parameters)
                { cmd.Parameters.Add(p); }
            }
            cmd.Connection.Open();
            return cmd;
        }

    }
}
