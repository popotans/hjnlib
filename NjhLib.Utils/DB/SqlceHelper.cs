using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using System.Data.Common;

namespace NjhLib.Utils.DB
{
    public class SqlceHelper
    {
        private static string connstr { get; set; }
        public SqlceHelper(string connstr)
        {
            SqlceHelper.connstr = connstr;
        }

        public static SqlCeConnection CreateConn()
        {
            SqlCeConnection conn = new SqlCeConnection(connstr);
            conn.Open();
            return conn;
        }

        public static IDataReader ExecuteReader(string sql, params SqlCeParameter[] sps)
        {
            IDbConnection conn = CreateConn();
            SqlCeCommand cmd = new SqlCeCommand(sql);
            if (sps != null)
            {
                cmd.Parameters.AddRange(sps);
            }
            IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }

        public static void ExecuteNonQuery(string sql, params SqlCeParameter[] sps)
        {
            IDbConnection conn = CreateConn();
            SqlCeCommand cmd = new SqlCeCommand(sql);
            if (sps != null)
            {
                cmd.Parameters.AddRange(sps);
            }
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public static string ExecuteScalar(string sql, params SqlCeParameter[] sps)
        {
            IDbConnection conn = CreateConn();
            SqlCeCommand cmd = new SqlCeCommand(sql);
            if (sps != null)
            {
                cmd.Parameters.AddRange(sps);
            }
            string r = "";
            object l = cmd.ExecuteScalar();
            if (l != null) r = l.ToString();
            cmd.Connection.Close();
            return r;
        }

        public static DataTable ExecuteDatatable(string sql, params SqlCeParameter[] sps)
        {
            DataTable dt = new DataTable();
            SqlCeDataAdapter adp = new SqlCeDataAdapter();
            SqlCeConnection conn = CreateConn();
            SqlCeCommand cmd = new SqlCeCommand(sql);
            if (sps != null) cmd.Parameters.AddRange(sps);
            cmd.Connection = conn;
            adp.SelectCommand = cmd;
            adp.Fill(dt);
            cmd.Connection.Close();
            return dt;

        }

    }
}
