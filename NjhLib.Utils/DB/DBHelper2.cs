using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;
namespace NjhLib.Utils.DB
{
    public class DBHelper2
    { // Fields
        private DbConnection _connection;
        private static string _dbConnectionString = ConfigurationManager.AppSettings["DbHelperConnectionString"];
        private static string _dbProviderName = ConfigurationManager.AppSettings["DbHelperProvider"];

        // Methods
        public DBHelper2()
        {
            this._connection = CreateConnection(_dbConnectionString);
        }

        public DBHelper2(string connectionString)
        {
            this._connection = CreateConnection(connectionString);
        }

        public static DbConnection CreateConnection()
        {
            DbConnection connection = DbProviderFactories.GetFactory(_dbProviderName).CreateConnection();
            connection.ConnectionString = _dbConnectionString;
            return connection;
        }

        public static DbConnection CreateConnection(string connectionString)
        {
            DbConnection connection = DbProviderFactories.GetFactory(_dbProviderName).CreateConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }

        public DataSet ExecuteDataSet(DbCommand cmd, Trans t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbDataAdapter adapter = DbProviderFactories.GetFactory(_dbProviderName).CreateDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            this._connection.Close();
            return dataSet;
        }

        public DataTable ExecuteDataTable(DbCommand cmd)
        {
            DbDataAdapter adapter = DbProviderFactories.GetFactory(_dbProviderName).CreateDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            this._connection.Close();
            return dataTable;
        }

        public DataTable ExecuteDataTable(DbCommand cmd, Trans t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbDataAdapter adapter = DbProviderFactories.GetFactory(_dbProviderName).CreateDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            this._connection.Close();
            return dataTable;
        }

        public DbDataReader ExecuteReader(DbCommand cmd)
        {
            cmd.Connection.Open();
            DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            this._connection.Close();
            return reader;
        }

        public DbDataReader ExecuteReader(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            this._connection.Close();
            return reader;
        }

        public DbDataReader ExecuteReader(string sqlText, params DbParameter[] cmdParms)
        {
            DbDataReader reader2;
            using (DbConnection connection = this._connection)
            {
                DbCommand sqlStringCommond = this.GetSqlStringCommond(sqlText);
                try
                {
                    PrepareCommand(sqlStringCommond, connection, null, sqlText, cmdParms);
                    DbDataReader reader = sqlStringCommond.ExecuteReader();
                    sqlStringCommond.Parameters.Clear();
                    reader2 = reader;
                }
                catch (DataException exception)
                {
                    throw new Exception(exception.Message);
                }
            }
            return reader2;
        }

        public object ExecuteScalar(DbCommand cmd)
        {
            cmd.Connection.Open();
            object obj2 = cmd.ExecuteScalar();
            cmd.Connection.Close();
            this._connection.Close();
            return obj2;
        }

        public object ExecuteScalar(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            object obj2 = cmd.ExecuteScalar();
            this._connection.Close();
            return obj2;
        }

        public int ExecuteSql(DbCommand cmd)
        {
            cmd.Connection.Open();
            int num = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            this._connection.Close();
            return num;
        }

        public int ExecuteSql(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            int num = cmd.ExecuteNonQuery();
            this._connection.Close();
            return num;
        }

        public int ExecuteSql(string sqlText, params DbParameter[] cmdParms)
        {
            using (DbConnection connection = this._connection)
            {
                DbCommand sqlStringCommond = this.GetSqlStringCommond(sqlText);
                try
                {
                    PrepareCommand(sqlStringCommond, connection, null, sqlText, cmdParms);
                    int num = sqlStringCommond.ExecuteNonQuery();
                    sqlStringCommond.Parameters.Clear();
                    return num;
                }
                catch (DataException exception)
                {
                    throw new Exception(exception.Message);
                }
                finally
                {
                    if (sqlStringCommond != null)
                    {
                        sqlStringCommond.Dispose();
                    }
                }
            }
        }

        public DbCommand GetSqlStringCommond(string sqlQuery)
        {
            DbCommand command = this._connection.CreateCommand();
            command.CommandText = sqlQuery;
            command.CommandType = CommandType.Text;
            this._connection.Close();
            return command;
        }

        public DbCommand GetStoredProcCommond(string storedProcedure)
        {
            DbCommand command = this._connection.CreateCommand();
            command.CommandText = storedProcedure;
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = CommandType.Text;
            if (cmdParms != null)
            {
                foreach (DbParameter parameter in cmdParms)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        public DataSet Query(DbCommand cmd)
        {
            DbDataAdapter adapter = DbProviderFactories.GetFactory(_dbProviderName).CreateDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            this._connection.Close();
            return dataSet;
        }

        public DataSet Query(string sqlText, params DbParameter[] cmdParms)
        {
            using (DbConnection connection = this._connection)
            {
                DbCommand sqlStringCommond = this.GetSqlStringCommond(sqlText);
                PrepareCommand(sqlStringCommond, connection, null, sqlText, cmdParms);
                DbDataAdapter adapter = DbProviderFactories.GetFactory(_dbProviderName).CreateDataAdapter();
                adapter.SelectCommand = sqlStringCommond;
                DataSet dataSet = new DataSet();
                try
                {
                    adapter.Fill(dataSet);
                    sqlStringCommond.Parameters.Clear();
                }
                catch (DataException exception)
                {
                    throw new Exception(exception.Message);
                }
                return dataSet;
            }
        }
    }
}