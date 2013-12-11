using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NjhLib.Utils.Ibatis
{
    public class TestUserDao : BaseSqlMapDao
    {
        BaseSqlMapDao dao = new BaseSqlMapDao();
        public TestUserDao()
        {

        }

        public void InsertUser(Object obj)
        {
            dao.BeginTrans();
            dao.CommitTrans();

            dao.CloseTrans();



        }
    }
}
