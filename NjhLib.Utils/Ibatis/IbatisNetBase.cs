using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using IBatisNet.DataMapper.SessionStore;
using IBatisNet.DataMapper.Exceptions;
using IBatisNet.Common;
using IBatisNet.DataMapper.Configuration.Statements;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Scope;
using IBatisNet.Common.Pagination;

namespace NjhLib.Utils.Ibatis
{
    public class IbatisNetBase
    {
        public static ISqlMapper SqlMap { get; set; }
        private static readonly object syncObj = new object();

        static IbatisNetBase()
        {
            if (SqlMap == null)
            {
                lock (syncObj)
                {
                    if (SqlMap == null)
                    {
                        SqlMap = SqlMapperManager.GetMapper("ibatisnet/dbIbatisNet.config");
                    }
                }
            }
        }

        /**/
        /// <summary>
        /// 得到参数化后的SQL
        /// </summary>
        public static string QueryForSql(string tag, object paramObject)
        {
            IStatement statement = SqlMap.GetMappedStatement(tag).Statement;
            IMappedStatement mapStatement = SqlMap.GetMappedStatement(tag);
            ISqlMapSession session = new SqlMapSession(SqlMap);
            RequestScope request = statement.Sql.GetRequestScope(mapStatement, paramObject, session);
            return request.PreparedStatement.PreparedSql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="paramObject"></param>
        /// <returns></returns>
        protected static IDbCommand GetDbCommand(string tag, object paramObject)
        {
            IMappedStatement statement = SqlMap.GetMappedStatement(tag);
            if (!SqlMap.IsSessionStarted)
            {
                SqlMap.OpenConnection();
            }
        
            RequestScope request = statement.Statement.Sql.GetRequestScope(statement, paramObject, SqlMap.LocalSession);
            statement.PreparedCommand.Create(request, SqlMap.LocalSession, statement.Statement, paramObject);

            IDbCommand command = SqlMap.LocalSession.CreateCommand(CommandType.Text);
            command.CommandText = request.IDbCommand.CommandText;
            foreach (IDataParameter pa in request.IDbCommand.Parameters)
            {
                IDbDataParameter para = SqlMap.LocalSession.CreateDataParameter();
                para.ParameterName = pa.ParameterName;
                para.Value = pa.Value;
                command.Parameters.Add(para);
            }
            return command;
        }

        /// <summary>
        /// 通用的以DataTable的方式得到Select的结果(xml文件中参数要使用$标记的占位参数)
        /// </summary>
        /// <param name="tag">语句ID</param>
        /// <param name="paramObject">语句所需要的参数</param>
        /// <returns>得到的DataTable</returns>
        public static DataTable QueryForDataTable(string tag, object paramObject)
        {
            IDbCommand command = GetDbCommand(tag, paramObject);
            IDataAdapter adapter = SqlMap.LocalSession.CreateDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        /// <summary>
        /// 用于分页控件使用
        /// </summary>
        /// <param name="tag">语句ID</param>
        /// <param name="paramObject">语句所需要的参数</param>
        /// <param name="PageSize">每页显示数目</param>
        /// <param name="curPage">当前页</param>
        /// <param name="recCount">记录总数</param>
        /// <returns>得到的DataTable</returns>
        public static DataTable QueryForDataTable(string tag, object paramObject, int PageSize, int curPage, out int recCount)
        {
            IDataReader dr = null;
            bool isSessionLocal = false;
            string sql = QueryForSql(tag, paramObject);
            string strCount = "select count(*) " + sql.Substring(sql.ToLower().IndexOf("from"));

            IDalSession session = SqlMap.LocalSession;
            DataTable dt = new DataTable();
            if (session == null)
            {
                session = new SqlMapSession(SqlMap);
                session.OpenConnection();
                isSessionLocal = true;
            }
            try
            {
                IDbCommand cmdCount = GetDbCommand(tag, paramObject);
                cmdCount.Connection = session.Connection;
                cmdCount.CommandText = strCount;
                object count = cmdCount.ExecuteScalar();
                recCount = Convert.ToInt32(count);

                IDbCommand cmd = GetDbCommand(tag, paramObject);
                cmd.Connection = session.Connection;
                dr = cmd.ExecuteReader();

                dt = QueryForPaging(dr, PageSize, curPage);
            }
            finally
            {
                if (isSessionLocal)
                {
                    session.CloseConnection();
                }
            }
            return dt;
        }

        /// <summary>
        /// 取回合适数量的数据
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="PageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>
        protected static DataTable QueryForPaging(IDataReader dataReader, int PageSize, int curPage)
        {
            DataTable dt;
            dt = new DataTable();
            int colCount = dataReader.FieldCount;
            for (int i = 0; i < colCount; i++)
            {
                dt.Columns.Add(new DataColumn(dataReader.GetName(i), dataReader.GetFieldType(i)));
            }

            // 读取数据。将DataReader中的数据读取到DataTable中

            object[] vald = new object[colCount];
            int iCount = 0; // 临时记录变量
            while (dataReader.Read())
            {
                // 当前记录在当前页记录范围内

                if (iCount >= PageSize * (curPage - 1) && iCount < PageSize * curPage)
                {
                    for (int i = 0; i < colCount; i++)
                        vald[i] = dataReader.GetValue(i);

                    dt.Rows.Add(vald);
                }
                else if (iCount > PageSize * curPage)
                {
                    break;
                }
                iCount++; // 临时记录变量递增
            }

            if (!dataReader.IsClosed)
            {
                dataReader.Close();
                dataReader.Dispose();
            }
            return dt;
        }

        #region  others's collection
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        protected bool ExecuteExists(string statementName, object parameterObject)
        {
            try
            {
                object obj = SqlMap.QueryForObject(statementName, parameterObject);
                int cmdresult;
                if ((Object.Equals(obj, null)) || (obj == null))
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
            catch (Exception e)
            {
                throw (e);
            }
        }



        /// <summary>
        /// 执行添加
        /// </summary>
        /// <param name="statementName">操作名</param>
        /// <param name="parameterObject">参数</param>
        protected object ExecuteInsert(string statementName, object parameterObject)
        {
            try
            {
                return SqlMap.Insert(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataMapperException("Error executing query '" + statementName + "' for insert.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// 执行添加，返回自动增长列
        /// </summary>
        /// <param name="statementName">操作名</param>
        /// <param name="parameterObject">参数</param>
        /// <returns>返回自动增长列</returns>
        protected int ExecuteInsertForInt(string statementName, object parameterObject)
        {
            try
            {
                object obj = SqlMap.Insert(statementName, parameterObject);
                if (obj != null)
                {
                    return Convert.ToInt32(obj);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                throw new DataMapperException("Error executing query '" + statementName + "' for insert.  Cause: " + e.Message, e);
            }
        }


        /// <summary>
        /// 执行修改
        /// </summary>
        /// <param name="statementName">操作名</param>
        /// <param name="parameterObject">参数</param>
        /// <returns>返回影响行数</returns>
        protected int ExecuteUpdate(string statementName, object parameterObject)
        {
            try
            {
                return SqlMap.Update(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataMapperException("Error executing query '" + statementName + "' for update.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// 执行删除
        /// </summary>
        /// <param name="statementName">操作名</param>
        /// <param name="parameterObject">参数</param>
        /// <returns>返回影响行数</returns>
        protected int ExecuteDelete(string statementName, object parameterObject)
        {
            try
            {
                return SqlMap.Delete(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataMapperException("Error executing query '" + statementName + "' for delete.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// 得到列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="statementName">操作名称，对应xml中的Statement的id</param>
        /// <param name="parameterObject">参数</param>
        /// <returns></returns>
        protected IList<T> QueryForList<T>(string statementName, object parameterObject)
        {
            try
            {
                return SqlMap.QueryForList<T>(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataMapperException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// 得到指定数量的记录数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statementName"></param>
        /// <param name="parameterObject">参数</param>
        /// <param name="skipResults">跳过的记录数</param>
        /// <param name="maxResults">最大返回的记录数</param>
        /// <returns></returns>
        protected IList<T> QueryForList<T>(string statementName, object parameterObject, int skipResults, int maxResults)
        {
            try
            {
                return SqlMap.QueryForList<T>(statementName, parameterObject, skipResults, maxResults);
            }
            catch (Exception e)
            {

                throw new DataMapperException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// 得到分页的列表
        /// </summary>
        /// <param name="statementName">操作名称</param>
        /// <param name="parameterObject">参数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        [Obsolete("过期方法，不建议使用")]
        protected IPaginatedList ExecuteQueryForPaginatedList(string statementName, object parameterObject, int pageSize)
        {
            try
            {
                return SqlMap.QueryForPaginatedList(statementName, parameterObject, pageSize);
            }
            catch (Exception e)
            {
                throw new DataMapperException("Error executing query '" + statementName + "' for paginated list.  Cause: " + e.Message, e);
            }

        }



        /// <summary>
        /// 查询得到对象的一个实例
        /// </summary>
        /// <typeparam name="T">对象type</typeparam>
        /// <param name="statementName">操作名</param>
        /// <param name="parameterObject">参数</param>
        /// <returns></returns>
        protected T QueryForObject<T>(string statementName, object parameterObject)
        {
            try
            {
                return SqlMap.QueryForObject<T>(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataMapperException("Error executing query '" + statementName + "' for object.  Cause: " + e.Message, e);
            }
        }
        #endregion


    }
}
