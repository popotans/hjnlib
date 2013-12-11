using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NjhLib.Utils.Ibatis;
using System.Data;
namespace NjhLib.Web.Mvc.ibatisnet.dao
{
    public class studentDao : NjhLib.Utils.Ibatis.IbatisNetBase
    {
        public IList<Student> GetAllStudent()
        {
            IList<Student> list = new List<Student>();
            //   list = QueryForList<Student>("selectAll", null);

            IDbCommand cmd = GetDbCommand("selectAll", null);
            cmd.Connection.Open();
            IDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Student s = new Student();
                s.Id = int.Parse(dr["id"].ToString());
                s.Age = int.Parse(dr["age"].ToString());
                s.Birth = DateTime.Parse(dr["birth"].ToString());
                s.Name = dr["name"].ToString();
                list.Add(s);
            }
            cmd.Connection.Close();
            return list;
        }

        public int InsertS(Student s)
        {
            int rs = 0;
            rs = (int)ExecuteInsert("insertA", s);
            return rs;
        }

        public int Update(Student s)
        {
            int trs = ExecuteUpdate("updateById", s);
            return trs;
        }

        public int Del(int id)
        {
            int rs = ExecuteDelete("deleteById", id);
            return rs;
        }
    }
}