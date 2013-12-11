using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NjhLib.Web.Mvc.ibatisnet
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        dao.studentDao dao = new dao.studentDao();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            IList<Student> list = dao.GetAllStudent();
            foreach (Student s in list)
            {
                Response.Write(s.Name + "<br>");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Student s = new Student { Name = "nijunhua2", Birth = new DateTime(1998, 2, 1), Age = 15 };
            int rs = dao.InsertS(s);
            Response.Write(" 刚插入的学生的id是：" + rs);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Student s = new Student
            {
                Age = 26,
                Id = 3,
                Name = "jjjjjj"
            };
            int ts = dao.Update(s);
            Response.Write(" 刚更新了：" + ts);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int id = 2;
            int ts = dao.Del(id);
            Response.Write(" 刚删除了：" + ts);
        }
    }
}