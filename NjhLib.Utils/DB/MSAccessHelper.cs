using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Configuration;

namespace NjhLib.Utils.DB
{
    public class MSAccessHelper
    {
        protected OleDbConnection conn = null;
        //  protected OleDbCommand comm = new OleDbCommand();
        public string dbpath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["DbAccessPath"]);

        public MSAccessHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public MSAccessHelper(string path)
        {
            this.dbpath = path;
        }

        /// <summary>
        /// 打开数据库
        /// </summary>
        private OleDbConnection openConnection()
        {

            conn = new OleDbConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.ConnectionString = @"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + dbpath;
                //microsoft.ace.oledb.12.0
                //conn.ConnectionString = @"Provider=microsoft.ace.oledb.12.0;Data Source=" + dbpath;
                // comm.Connection = conn;
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                { throw new Exception(e.Message); }
            }
            return conn;

        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        private void closeConnection(OleDbConnection conn)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public DataTable Paging(int pno, int pagesize, string fields, string table,
            string where, string orderby,
            out int recordcount, out int pagecount)
        {
            if (string.IsNullOrEmpty(fields)) fields = " * ";
            recordcount = pagecount = 0;
            DataTable dt = new DataTable();

            //计算总记录数
            string sql = "select count(*) as c from " + table + "  " + where + "  " + " ";

            string _recordcount = GetValue(sql);
            //  HttpContext.Current.Response.Write(sql);
            //HttpContext.Current.Response.End();
            int.TryParse(_recordcount, out recordcount);
            //计算页数
            int _tmp = recordcount % pagesize;
            int pages = _tmp == 0 ? recordcount / pagesize : recordcount / pagesize + 1;
            pagecount = pages;
            string execSql = "";
            if (pno == 1)
            {
                execSql = " select top " + pagesize + " " + fields + " from  " + table + " " + where + "  " + orderby + " desc";
            }
            else
            {
                /*
                 * select * from 
                 * (select top 4 * from ( select top 8* from novel order by id desc) as t1 
                 * order by id asc )as t2 
                 * order by id desc
                 * */
                int index = (pno) * pagesize;
                execSql = " select top " + index + "  " + fields + " from  " + table + " " + where + "  " + orderby + " desc";
                execSql = "select top " + pagesize + " * from  (" + execSql + ") as t1 " + orderby + " asc ";
                execSql = " select  * from (" + execSql + ")as t2 " + orderby + " desc";
            }
            //  HttpContext.Current.Response.Write(execSql);
            // HttpContext.Current.Response.End();

            dt = DataTable(execSql);
            return dt;
        }

        public DataTable Paging(int pno, int pagesize, string sql,
           out int recordcount, out int pagecount)
        {
            recordcount = pagecount = 0;
            string sqlrecordcount = "select count(1) from (" + sql + ") as t1";
            string _recordcount = GetValue(sqlrecordcount);
            int.TryParse(_recordcount, out recordcount);
            //计算页数
            int _tmp = recordcount % pagesize;
            int pages = _tmp == 0 ? recordcount / pagesize : recordcount / pagesize + 1;
            string execSql = "";
            if (pno == 1)
            {
                execSql = " select top " + pagesize + " * from  (" + sql + ") as t1";
            }
            else
            {
                int index = recordcount - (pno - 1) * pagesize;
                execSql = " select top " + index + " * from (" + sql + ") as t1";
            }
            DataTable dt = DataTable(execSql);// new DataTable();
            return dt;
        }



        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sqlstr"></param>
        public void ExcuteSql(string sqlstr)
        {
            OleDbCommand comm = new OleDbCommand();
            OleDbConnection conn = openConnection();
            try
            {
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                comm.Connection = conn;
                comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            { closeConnection(conn); }
        }

        public void ExecuteNonQuery(string sql)
        {
            ExcuteSql(sql);
        }

        public void ExecuteNonQuery(string sql, OleDbParameter[] ops)
        {
            OleDbCommand comm = new OleDbCommand();
            OleDbConnection conn = openConnection();
            try
            {
                comm.CommandType = CommandType.Text;
                comm.CommandText = sql;
                comm.Connection = conn;
                if (ops != null)
                {
                    comm.Parameters.AddRange(ops);
                }
                comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            { closeConnection(conn); }
        }

        public OleDbParameter GetParameter(string name, string value, OleDbType type)
        {
            OleDbParameter p = new OleDbParameter(name, value);
            p.OleDbType = type;
            return p;
        }

        public IDataReader ExecuteReader(string sql, OleDbParameter[] ops)
        {
            OleDbCommand comm = new OleDbCommand();
            OleDbConnection conn = openConnection();
            OleDbDataReader dr = null;
            try
            {
                comm.Connection = conn;
                comm.CommandText = sql;
                comm.CommandType = CommandType.Text;
                if (ops != null)
                {
                    comm.Parameters.AddRange(ops);
                }

                dr = comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                try
                {
                    dr.Close();
                    closeConnection(conn);
                }
                catch { }
            }
            return dr;
        }


        public IDataReader ExecuteReader(string sql)
        {

            return DataReader(sql);
        }


        public string GetValue(string sqlstr)
        {
            string s = string.Empty;
            using (OleDbDataReader dr = DataReader(sqlstr))
            {
                if (dr.Read())
                {
                    if (dr[0] != null)
                        s = dr[0].ToString();
                }
            }
            return s;

        }

        /// <summary>
        /// 返回指定sql语句的OleDbDataReader对象，使用时请注意关闭这个对象。
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public OleDbDataReader DataReader(string sqlstr)
        {
            OleDbCommand comm = new OleDbCommand();
            OleDbConnection conn = openConnection();
            OleDbDataReader dr = null;
            try
            {
                comm.Connection = conn;
                comm.CommandText = sqlstr;
                comm.CommandType = CommandType.Text;

                dr = comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                try
                {
                    dr.Close();
                    closeConnection(conn);
                }
                catch { }
            }
            return dr;
        }
        /// <summary>
        /// 返回指定sql语句的OleDbDataReader对象,使用时请注意关闭
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="dr"></param>
        public void DataReader(string sqlstr, ref OleDbDataReader dr)
        {
            OleDbCommand comm = new OleDbCommand();
            OleDbConnection conn = openConnection();
            try
            {
                comm.Connection = conn;
                comm.CommandText = sqlstr;
                comm.CommandType = CommandType.Text;
                dr = comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                try
                {
                    if (dr != null && !dr.IsClosed)
                        dr.Close();
                }
                catch
                {
                }
                finally
                {
                    closeConnection(conn);
                }
            }
        }
        /// <summary>
        /// 返回指定sql语句的dataset
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public DataSet DataSet(string sqlstr)
        {
            OleDbCommand comm = new OleDbCommand();
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter();
            OleDbConnection conn = openConnection();
            try
            {
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                da.SelectCommand = comm;
                da.Fill(ds);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                closeConnection(conn);
            }
            return ds;
        }
        /// <summary>
        /// 返回指定sql语句的dataset
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="ds"></param>
        public void dDtaSet(string sqlstr, ref DataSet ds)
        {
            OleDbCommand comm = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter();
            OleDbConnection conn = openConnection();
            try
            {
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                da.SelectCommand = comm;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                closeConnection(conn);
            }
        }
        /// <summary>
        /// 返回指定sql语句的datatable
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public DataTable DataTable(string sqlstr)
        {
            OleDbCommand comm = new OleDbCommand();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter();
            OleDbConnection conn = openConnection();
            try
            {
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                da.SelectCommand = comm;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                closeConnection(conn);
            }
            return dt;
        }
        public DataTable ExecuteDataTable(string sql)
        {
            return DataTable(sql);
        }
        public DataTable ExecuteDataTable(string sql, OleDbParameter[] ops)
        {
            OleDbCommand comm = new OleDbCommand();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter();
            OleDbConnection conn = openConnection();
            try
            {
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = sql;
                if (ops != null)
                {
                    comm.Parameters.AddRange(ops);
                }
                da.SelectCommand = comm;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                closeConnection(conn);
            }
            return dt;
        }


        /// <summary>
        /// 返回指定sql语句的datatable
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="dt"></param>
        public void DataTable(string sqlstr, ref DataTable dt)
        {
            OleDbCommand comm = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter();
            OleDbConnection conn = openConnection();
            try
            {
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                da.SelectCommand = comm;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                closeConnection(conn);
            }
        }
        /// <summary>
        /// 返回指定sql语句的dataview
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public DataView DataView(string sqlstr)
        {
            OleDbCommand comm = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataView dv = new DataView();
            DataSet ds = new DataSet();
            OleDbConnection conn = openConnection();
            try
            {
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlstr;
                da.SelectCommand = comm;
                da.Fill(ds);
                dv = ds.Tables[0].DefaultView;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                closeConnection(conn);
            }
            return dv;
        }


        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataTable AccessPaging(string fields, string table, string strWhere, string filedOrder, int pageSize, int currentPage, out int pagecount, out int recordcount)
        {
            pagecount = recordcount = 0;
            string sql = " select count(1) from  " + table;
            if (!string.IsNullOrEmpty(strWhere)) { sql += " where  " + strWhere; }
            object obj =// ExecuteScalar(sql);
            GetValue(sql);
            if (obj != null) { recordcount = int.Parse(obj.ToString()); } else { recordcount = 0; }
            pagecount = (int)Math.Ceiling((double)recordcount / (double)pageSize);
            StringBuilder strSql = new StringBuilder();
            //select top 10 * from t  where order by id desc 
            /*
             where id< ( select min(id) from ( select top20 * from t where order by id desc) )
             * order by id desc 
             */



            if (currentPage > 0)
            {
                if (currentPage != 1)
                {
                    int topNum = pageSize * (currentPage - 1);
                    strSql.Append("select top " + pageSize + " " + fields + " from " + table + " ");
                    strSql.Append(" where Id <( select min(id) from( select top " + topNum + " Id from  " + table + "");
                    if (strWhere.Trim() != "")
                    {
                        strSql.Append(" where " + strWhere);
                    }
                    strSql.Append(" order by " + filedOrder + "))");
                    if (strWhere.Trim() != "")
                    {
                        strSql.Append(" and " + strWhere);
                    }
                    //5%1+a+s+p+x
                    strSql.Append(" order by " + filedOrder);
                }
                else if (currentPage == 1)
                {
                    strSql.Append("select top " + pageSize + " " + fields + " from  " + table);
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        strSql.Append(" where " + strWhere);
                    }
                    strSql.Append(" order by " + filedOrder);
                }
            }
            else
            {
                strSql.Append("select top " + pageSize + " " + fields + " from " + table + "");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(" order by " + filedOrder);
            }

            return ExecuteDataTable(strSql.ToString());
        }
    }
}
