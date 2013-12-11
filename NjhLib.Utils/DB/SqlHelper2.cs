using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NjhLib.Utils.DB
{
    public class SqlHelper2
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public SqlHelper2()
        {
        }

        #region 公用方法
        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = SqlHelper2.GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        public static bool Exists(string strSql)
        {
            object obj = SqlHelper2.GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            object obj = SqlHelper2.GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region  执行简单SQL语句
        /// <summary>   
        /// 执行SQL语句，返回影响的记录数   
        /// </summary>   
        /// <param name="SQLString">SQL语句</param>   
        /// <returns>影响的记录数</returns> 
        /// <remarks>
        /// -----------------------------------------
        /// 修改了在try里 return rows从而没法 connection.Close()的bug，修改了当出错时重复 connection.Close()的bug。
        /// update by xiaowen.qiu, 2011-7-5 10:18
        /// -----------------------------------------
        /// </remarks>
        public static int ExecuteSql(string SQLString)
        {
            int rows = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        rows = cmd.ExecuteNonQuery();
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return rows;
        }

        /// <summary>   
        /// 执行SQL语句，返回影响的记录数 适用于select语句   
        /// </summary>   
        /// <param name="SQLString">SQL语句</param>   
        /// <returns>影响的记录数</returns>
        /// <remarks>
        /// -----------------------------------------
        /// 修改了在try里 return rows从而没法 connection.Close()的bug，修改了没法 connection.Close()的bug。
        /// update by xiaowen.qiu, 2011-7-5 10:30
        /// -----------------------------------------
        /// </remarks>
        public static int ExecuteSql2(string SQLString)
        {
            int rows = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        rows = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return rows;
        }

        /// <summary>   
        /// 执行多条SQL语句，实现数据库事务。   
        /// </summary>   
        /// <param name="SQLStringList">多条SQL语句</param>        
        public static void ExecuteSqlTran(IList<string> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>   
        /// 执行带一个存储过程参数的的SQL语句。   
        /// </summary>   
        /// <param name="SQLString">SQL语句</param>   
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>   
        /// <returns>影响的记录数</returns>   
        public static int ExecuteSql(string SQLString, string content)
        {
            int rows = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLString, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.VarChar);

                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    rows = cmd.ExecuteNonQuery();
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
            return rows;
        }

        /// <summary>   
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)   
        /// </summary>   
        /// <param name="strSQL">SQL语句</param>   
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>   
        /// <returns>影响的记录数</returns>   
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            int rows = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(strSQL, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@fs", SqlDbType.Binary);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    rows = cmd.ExecuteNonQuery();

                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
            return rows;
        }

        /// <summary>   
        /// 执行一条计算查询结果语句，返回查询结果（object）。   
        /// </summary>   
        /// <param name="SQLString">计算查询结果语句</param>   
        /// <returns>查询结果（object）</returns>   
        public static object GetSingle(string SQLString)
        {
            object result = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            result = null;
                        }
                        else
                        {
                            result = obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 执行SQL，返回第一行的第一个字段，当为空时，按默认类型/值返回
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static int GetSingle(string sql, int def)
        {
            var obj = GetSingle(sql);
            if (null == obj || obj is DBNull)
                return def;
            else
                return (int)obj;
        }

        /// <summary>
        /// 执行SQL，返回第一行的第一个字段，当为空时，按默认类型/值返回
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static double GetSingle(string sql, double def)
        {
            var obj = GetSingle(sql);
            if (null == obj || obj is DBNull)
                return def;
            else
                return Convert.ToDouble(obj);
        }

        /// <summary>
        /// 执行SQL，返回第一行的第一个字段，当为空时，按默认类型/值返回
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static string GetSingle(string sql, string def)
        {
            var obj = GetSingle(sql);
            if (null == obj || obj is DBNull)
                return def;
            else
                return obj.ToString();
        }

        /// <summary>
        /// 执行SQL，返回第一行的第一个字段，当为空时，按默认类型/值返回
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static DateTime GetSingle(string sql, DateTime def)
        {
            var obj = GetSingle(sql);
            if (null == obj || obj is DBNull)
                return def;
            else
                return Convert.ToDateTime(obj);
        }

        /// <summary>   
        /// 执行查询语句，返回SqlDataReader   
        /// </summary>   
        /// <param name="strSQL">查询语句</param>   
        /// <returns>SqlDataReader</returns>
        /// <remarks>
        /// update by xiaowen.qiu, 2011-06-30
        /// </remarks>
        public static SqlDataReader ExecuteReader(string strSQL)
        {
            return ExecuteReader(strSQL, CommandBehavior.CloseConnection);
        }

        /// <summary>   
        /// 执行查询语句，返回SqlDataReader   
        /// </summary>   
        /// <param name="strSQL">查询语句</param>   
        /// <returns>SqlDataReader</returns>
        /// <remarks>add by xiaowen.qiu, 2011-06-30</remarks>
        public static SqlDataReader ExecuteReader(string strSQL, CommandBehavior cmdBehavior)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader(cmdBehavior);
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                try
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                catch { }
                throw new Exception(e.Message);
            }
        }

        /// <summary>   
        /// 执行查询语句，返回DataSet   
        /// </summary>   
        /// <param name="SQLString">查询语句</param>   
        /// <returns>DataSet</returns>   
        public static DataSet Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);

                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return ds;
            }
        }

        /// <summary>   
        /// 执行查询语句，返回datatable   
        /// </summary>   
        /// <param name="SQLString">查询语句</param>   
        /// <returns>DataSet</returns>   
        public static DataTable QueryTable(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return ds.Tables[0];
            }
        }
        #endregion

        #region 执行带参数的SQL语句
        /// <summary>   
        /// 执行SQL语句，返回影响的记录数   
        /// </summary>   
        /// <param name="SQLString">SQL语句</param>   
        /// <returns>影响的记录数</returns>   
        public static int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            int rows = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return rows;
        }

        /// <summary>   
        /// 执行多条SQL语句，实现数据库事务。   
        /// </summary>   
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param> 
        /// <remarks>
        /// 修改了当执行出错时没有关闭数据库连接的bug. - xiaowen.qiu,2011-7-12
        /// 当key为sql时，可能会存在 sql 一样的情况，此时会出错。
        /// </remarks>
        public static void ExecuteSqlTran(System.Collections.Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                try
                {
                    //循环   
                    foreach (System.Collections.DictionaryEntry myDE in SQLStringList)
                    {
                        string cmdText = myDE.Key.ToString();
                        SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                        PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                        int val = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>   
        /// 执行一条计算查询结果语句，返回查询结果（object）。   
        /// </summary>   
        /// <param name="SQLString">计算查询结果语句</param>   
        /// <returns>查询结果（object）</returns>   
        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            object result = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            result = null;
                        }
                        else
                        {
                            result = obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }

        /// <summary>   
        /// 执行查询语句，返回SqlDataReader   
        /// </summary>   
        /// <param name="strSQL">查询语句</param>   
        /// <returns>SqlDataReader</returns>  
        /// <remarks>update by xiaowen.qiu, 2011-06-30 16:45</remarks>
        public static SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            return ExecuteReader(SQLString, cmdParms, CommandBehavior.CloseConnection);
        }

        /// <summary>   
        /// 执行查询语句，返回SqlDataReader   
        /// </summary>   
        /// <param name="strSQL">查询语句</param>   
        /// <returns>SqlDataReader</returns>
        /// <remarks>add by xiaowen.qiu, 2011-06-30 16:45</remarks>
        public static SqlDataReader ExecuteReader(string SQLString, SqlParameter[] cmdParms, CommandBehavior cmdBehavior)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);

                SqlDataReader myReader = cmd.ExecuteReader(cmdBehavior);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                try
                {
                    connection.Close();
                }
                catch
                {
                }
                throw new Exception(e.Message);
            }
        }
        /// <summary>   
        /// 执行查询语句，返回DataSet   
        /// </summary>   
        /// <param name="SQLString">查询语句</param>   
        /// <returns>DataSet</returns>   
        public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }

            }
            return ds;
        }

        public static DataTable QueryTable(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                    return ds.Tables[0];
                }
            }
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;   
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion

        #region 存储过程操作
        /// <summary>   
        /// 执行存储过程   
        /// </summary>   
        /// <param name="storedProcName">存储过程名</param>   
        /// <param name="parameters">存储过程参数</param>   
        /// <returns>SqlDataReader</returns>   
        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataReader returnReader;
            connection.Open();
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader();
            return returnReader;
        }

        /// <summary>   
        /// 执行存储过程   
        /// </summary>   
        /// <param name="storedProcName">存储过程名</param>   
        /// <param name="parameters">存储过程参数</param>   
        /// <param name="tableName">DataSet结果中的表名</param>   
        /// <returns>DataSet</returns>   
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        /// <summary>   
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)   
        /// </summary>   
        /// <param name="connection">数据库连接</param>   
        /// <param name="storedProcName">存储过程名</param>   
        /// <param name="parameters">存储过程参数</param>   
        /// <returns>SqlCommand</returns>   
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

        /// <summary>   
        /// 执行存储过程，返回影响的行数         
        /// </summary>   
        /// <param name="storedProcName">存储过程名</param>   
        /// <param name="parameters">存储过程参数</param>   
        /// <param name="rowsAffected">影响的行数</param>   
        /// <returns></returns>   
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int result = 0;
                try
                {
                    connection.Open();
                    SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                    rowsAffected = command.ExecuteNonQuery();
                    result = (int)command.Parameters["ReturnValue"].Value;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
                return result;
            }
        }

        /// <summary>   
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)      
        /// </summary>   
        /// <param name="storedProcName">存储过程名</param>   
        /// <param name="parameters">存储过程参数</param>   
        /// <returns>SqlCommand 对象实例</returns>   
        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion


        public static void Close()
        {

        }



        public static void ExecuteReader()
        {
            throw new NotImplementedException();
        }
    }
}
