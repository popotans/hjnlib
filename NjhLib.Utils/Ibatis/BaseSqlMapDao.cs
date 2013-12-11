using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using IBatisNet.Common.Exceptions;
using IBatisNet.Common.Pagination;
using IBatisNet.DataAccess;
using IBatisNet.DataAccess.DaoSessionHandlers;
using IBatisNet.DataAccess.Interfaces;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Scope;
using IBatisNet.DataMapper.Configuration;


namespace NjhLib.Utils.Ibatis
{
    public class BaseSqlMapDao : IDao
    {
        private ISqlMapper sqlMap;
        private ISqlMapSession mapSession;
        protected const int PAGE_SIZE = 4;

        public BaseSqlMapDao()
        {
            this.sqlMap = this.GetLocalSqlMap();
        }

        /// <summary>
        /// Looks up the parent DaoManager, gets the local transaction
        /// (which should be a SqlMapDaoTransaction) and returns the
        /// SqlMap associated with this DAO.
        /// </summary>
        /// <returns>The SqlMap instance for this DAO.</returns>
        protected ISqlMapper GetLocalSqlMap()
        {
            DomSqlMapBuilder builder = new DomSqlMapBuilder();
            return builder.Configure("sqlmap.config");

        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// Executes the query for a generic object list.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        protected IList<T> ExecuteQueryForList<T>(string statementName, object parameterObject)
        {
            //  ISqlMapper sqlMap = GetLocalSqlMap();

            try
            {
                return sqlMap.QueryForList<T>(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }
        /// <summary>
        /// get paged list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        protected IList<T> ExecuteQueryForPagedList<T>(string statementName, object parameterObject, int skipResults, int MaxResults)
        {
            //  ISqlMapper sqlMap = GetLocalSqlMap();
            try
            {
                return sqlMap.QueryForList<T>(statementName, parameterObject, skipResults, MaxResults);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }


        protected IList<T> ExecuteQueryForList<T>(string statementName, object parameterObject, int skipResults, int maxResults)
        {
            // ISqlMapper sqlMap = GetLocalSqlMap();
            try
            {
                return sqlMap.QueryForList<T>(statementName, parameterObject, skipResults, maxResults);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        protected IList ExecuteQueryForList(string statementName, object parameterObject)
        {
            // ISqlMapper sqlMap = GetLocalSqlMap();
            try
            {
                return sqlMap.QueryForList(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        protected IList ExecuteQueryForList(string statementName, object parameterObject, int skipResults, int maxResults)
        {
            // ISqlMapper sqlMap = GetLocalSqlMap();
            try
            {
                return sqlMap.QueryForList(statementName, parameterObject, skipResults, maxResults);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        protected IPaginatedList ExecuteQueryForPaginatedList(string statementName, object parameterObject, int pageSize)
        {
            // ISqlMapper sqlMap = GetLocalSqlMap();
            try
            {
                return sqlMap.QueryForPaginatedList(statementName, parameterObject, pageSize);

            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for paginated list.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// Executes the query for a generic object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statementName">Name of the statement.</param>
        /// <param name="parameterObject">The parameter object.</param>
        /// <returns></returns>
        protected T ExecuteQueryForObject<T>(string statementName, object parameterObject)
        {
            // ISqlMapper sqlMap = GetLocalSqlMap();

            try
            {
                return sqlMap.QueryForObject<T>(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for object.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        protected object ExecuteQueryForObject(string statementName, object parameterObject)
        {
            // ISqlMapper sqlMap = GetLocalSqlMap();

            try
            {
                return sqlMap.QueryForObject(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for object.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        protected int ExecuteUpdate(string statementName, object parameterObject)
        {
            //ISqlMapper sqlMap = GetLocalSqlMap();

            try
            {
                return sqlMap.Update(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for update.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// delete 
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        protected int ExecuteDelete(string statementName, object parameterObject)
        {
            // ISqlMapper sqlMap = GetLocalSqlMap();

            try
            {
                return sqlMap.Delete(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for delete.  Cause: " + e.Message, e);
            }
        }

        /// <summary>
        /// Simple convenience method to wrap the SqlMap method of the same name.
        /// Wraps the exception with a IBatisNetException to isolate the SqlMap framework.
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="parameterObject"></param>
        /// <returns></returns>
        protected object ExecuteInsert(string statementName, object parameterObject)
        {
            // ISqlMapper sqlMap = GetLocalSqlMap();

            try
            {
                return sqlMap.Insert(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for insert.  Cause: " + e.Message, e);
            }
        }

        protected string GetSql(string statementName, object paramObject)
        {
            ISqlMapper mapper = this.sqlMap;// GetLocalSqlMap();
            IMappedStatement statement = mapper.GetMappedStatement(statementName);
            if (!mapper.IsSessionStarted)
            {
                mapper.OpenConnection();
            }
            RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, paramObject, mapper.LocalSession);

            return scope.PreparedStatement.PreparedSql;
        }

        /// <summary>
        /// Query for a DataSet.
        /// </summary>
        /// <param name="statementName">Name of the statement.</param>
        /// <param name="paramObject">The parameter object.</param>
        /// <returns>DataSet.</returns>
        protected DataSet QueryForDataSet(string statementName, object paramObject)
        {
            try
            {
                DataSet ds = new DataSet();
                ISqlMapper mapper = GetLocalSqlMap();
                IMappedStatement statement = mapper.GetMappedStatement(statementName);
                if (!mapper.IsSessionStarted)
                {
                    mapper.OpenConnection();
                }
                RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, paramObject, mapper.LocalSession);
                statement.PreparedCommand.Create(scope, mapper.LocalSession, statement.Statement, paramObject);

                //mapper.LocalSession.CreateDataAdapter((scope.IDbCommand).Fill(ds));
                IDbCommand dc = mapper.LocalSession.CreateCommand(scope.IDbCommand.CommandType);
                //IDbCommand dc = mapper.LocalSession.CreateCommand(CommandType.Text);
                dc.CommandText = scope.IDbCommand.CommandText;
                foreach (IDbDataParameter para in scope.IDbCommand.Parameters)
                {
                    dc.Parameters.Add(para);
                }
                //dc.Parameters = scope.IDbCommand.Parameters;
                IDbDataAdapter dda = mapper.LocalSession.CreateDataAdapter(dc);
                dda.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        #region  Trans
        public void BeginTrans()
        {
            this.mapSession = this.sqlMap.BeginTransaction();
        }
        public void CommitTrans()
        {
            this.mapSession.CommitTransaction();
        }
        public void RollBackTrans()
        {
            this.mapSession.RollBackTransaction();
        }
        public void CloseTrans()
        {
            this.mapSession.CloseConnection();
        }

        #endregion
    }
}
