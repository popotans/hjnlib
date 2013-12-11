﻿using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Collections;
using System.Data.Common;
using System.Data.SQLite;
namespace NjhLib.Utils.DB
{
    /// <summary> 
    /// 说明：这是一个针对System.Data.SQLite的数据库常规操作封装的通用类。 
    /// 作者：zhoufoxcn(周公） 
    /// 日期：2010-04-01 
    /// Blog:http://zhoufoxcn.blog.51cto.com or http://blog.csdn.net/zhoufoxcn 
    /// Version:0.1 
    /// </summary> 
    public class SQLiteDBHelper
    {
        private string connectionString = string.Empty;
        /// <summary> 
        /// 构造函数 
        /// </summary> 
        /// <param name="dbPath">SQLite数据库文件路径</param> 
        public SQLiteDBHelper(string dbPath)
        {
            this.connectionString = "Data Source=" + dbPath;
            if (!System.IO.File.Exists(dbPath))
            {
                CreateDB(dbPath);
            }
        }
        /// <summary> 
        /// 创建SQLite数据库文件 
        /// </summary> 
        /// <param name="dbPath">要创建的SQLite数据库文件路径</param> 
        public static void CreateDB(string dbPath)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + dbPath))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "CREATE TABLE Demo(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE)";
                    command.ExecuteNonQuery();
                    command.CommandText = "DROP TABLE Demo";
                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary> 
        /// 对SQLite数据库执行增删改操作，返回受影响的行数。 
        /// </summary> 
        /// <param name="sql">要执行的增删改的SQL语句</param> 
        /// <param name="parameters">执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public int ExecuteNonQuery(string sql, SQLiteParameter[] parameters)
        {
            int affectedRows = 0;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = sql;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        affectedRows = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            return affectedRows;
        }

        public DbTransaction OpenDbTransaction()
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();
            return transaction;
        }
        public void CommitDbTransaction(DbTransaction trans)
        {
            if (trans != null)
            {
                // 提交事务自动关闭链接？
                trans.Commit();
            }

        }

        public int ExecuteNonQueryWithTrans(string sql, SQLiteParameter[] parameters, DbTransaction trans)
        {
            int affectedRows = 0;
            // using (SQLiteConnection connection = new SQLiteConnection(connectionString))

            {
                SQLiteConnection connection = trans.Connection as SQLiteConnection;
                // connection.Open();
                //  using (DbTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = sql;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        affectedRows = command.ExecuteNonQuery();
                    }
                    // transaction.Commit();
                }
            }
            return affectedRows;
        }

        /// <summary> 
        /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public SQLiteDataReader ExecuteReader(string sql, SQLiteParameter[] parameters)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary> 
        /// 执行一个查询语句，返回一个包含查询结果的DataTable 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public DataTable ExecuteDataTable(string sql, SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return data;
                }
            }
        }
        /// <summary> 
        /// 执行一个查询语句，返回查询结果的第一行第一列 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public Object ExecuteScalar(string sql, SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return data;
                }
            }
        }
        /// <summary> 
        /// 查询数据库中的所有数据类型信息 
        /// </summary> 
        /// <returns></returns> 
        public DataTable GetSchema()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                DataTable data = connection.GetSchema("TABLES");
                connection.Close();
                //foreach (DataColumn column in data.Columns) 
                //{ 
                // Console.WriteLine(column.ColumnName); 
                //} 
                return data;
            }
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strfields"></param>
        /// <param name="strTableWhere"></param>
        /// <param name="strorderby"></param>
        /// <param name="pno"></param>
        /// <param name="pagesize"></param>
        /// <param name="pagecount"></param>
        /// <param name="recordcount"></param>
        /// <returns></returns>
        public DataTable SqlLitePaging(string strfields, string strTableWhere, string strorderby, int pno, int pagesize, out int pagecount, out int recordcount)
        {
            pagecount = recordcount = 0;
            if (pno < 1) pno = 1;

            DataTable dt = new DataTable();
            string sqlCount = "select count(1) " + strTableWhere;
            recordcount = int.Parse((ExecuteScalar(sqlCount, null) as DataTable).Rows[0][0].ToString());

            double f = (double)recordcount / (double)pagesize;
            pagecount = (int)Math.Ceiling(f);

            if (pno > pagecount) pno = pagecount;

            int idxFrom = (pno - 1) * pagesize;
            string sqlPage = "select " + strfields + strTableWhere + strorderby + " limit " + idxFrom + ", " + pagesize + "";
            dt = ExecuteDataTable(sqlPage, null);
            return dt;

        }
    }
}

/***
 * 
 * using System; 
using System.Collections.Generic; 
using System.Text; 
using System.Data; 
using System.Data.Common; 
using System.Data.SQLite; 
using SQLiteQueryBrowser; 
namespace SQLiteDemo 
{ 
class Program 
{ 
static void Main(string[] args) 
{ 
//CreateTable(); 
//InsertData(); 
ShowData(); 
Console.ReadLine(); 
} 
public static void CreateTable() 
{ 
string dbPath = "D:\\Demo.db3"; 
//如果不存在改数据库文件，则创建该数据库文件 
if (!System.IO.File.Exists(dbPath)) 
{ 
SQLiteDBHelper.CreateDB("D:\\Demo.db3"); 
} 
SQLiteDBHelper db = new SQLiteDBHelper("D:\\Demo.db3"); 
string sql = "CREATE TABLE Test3(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,Name char(3),TypeName varchar(50),addDate datetime,UpdateTime Date,Time time,Comments blob)"; 
db.ExecuteNonQuery(sql, null); 
} 
public static void InsertData() 
{ 
string sql = "INSERT INTO Test3(Name,TypeName,addDate,UpdateTime,Time,Comments)values(@Name,@TypeName,@addDate,@UpdateTime,@Time,@Comments)"; 
SQLiteDBHelper db = new SQLiteDBHelper("D:\\Demo.db3"); 
for (char c = 'A'; c <= 'Z'; c++) 
{ 
for (int i = 0; i < 100; i++) 
{ 
SQLiteParameter[] parameters = new SQLiteParameter[]{ 
new SQLiteParameter("@Name",c+i.ToString()), 
new SQLiteParameter("@TypeName",c.ToString()), 
new SQLiteParameter("@addDate",DateTime.Now), 
new SQLiteParameter("@UpdateTime",DateTime.Now.Date), 
new SQLiteParameter("@Time",DateTime.Now.ToShortTimeString()), 
new SQLiteParameter("@Comments","Just a Test"+i) 
}; 
db.ExecuteNonQuery(sql, parameters); 
} 
} 
} 
public static void ShowData() 
{ 
//查询从50条起的20条记录 
string sql = "select * from test3 order by id desc limit 50 offset 20"; 
SQLiteDBHelper db = new SQLiteDBHelper("D:\\Demo.db3"); 
using (SQLiteDataReader reader = db.ExecuteReader(sql, null)) 
{ 
while (reader.Read()) 
{ 
Console.WriteLine("ID:{0},TypeName{1}", reader.GetInt64(0), reader.GetString(1)); 
} 
} 
} 
} 
} 


详细出处参考：http://www.jb51.net/article/26381.htm
 * ****/