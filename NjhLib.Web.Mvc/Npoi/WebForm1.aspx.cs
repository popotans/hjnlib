using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NjhLib.Utils.Npoi;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.Util;
using NPOI.SS.Util;
namespace NjhLib.Web.Mvc.Npoi
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Click += new EventHandler(Button1_Click);
            Button2.Click += new EventHandler(Button2_Click);
            Button3.Click += new EventHandler(Button3_Click);

        }

        void Button3_Click(object sender, EventArgs e)
        {
            NpoiLib npl = new NpoiLib();
            List<Row> list = npl.ReadExcel(Server.MapPath("text.xls"), 0);
            string[] data = new string[] { "AS", "CE CP", "SC IC", "Others" };
            foreach (Row r in list)
            {             
                npl.WriteDropDownList2(r.Sheet, 0, 0, 2, 3, data);
            }


            npl.SaveExcel(Server.MapPath("text2.xls"));
            Response.Write("<script>alert('CreatSuc!');</script>");
        }

        void Button2_Click(object sender, EventArgs e)
        {

            NpoiLib npl = new NpoiLib();
            List<Row> list = npl.ReadExcel(Server.MapPath("text.xls"), 0);
            Response.Write("<table><tr><td>111</td><td>222</td><td>333</td></tr>");
            int i = 0;
            string[] explicitListValues = new string[] { "AS", "CE CP", "SC IC", "Others" };
            foreach (Row r in list)
            {
                if (r != null)
                {
                    Cell c = r.GetCell(0);
                    NPOI.SS.Util.CellRangeAddressList ranglist = new NPOI.SS.Util.CellRangeAddressList();
                    ranglist.AddCellRangeAddress(new CellRangeAddress(0, 10, 0, 3));

                    DVConstraint dvconstraint = DVConstraint.CreateExplicitListConstraint(explicitListValues);
                    HSSFDataValidation dataValidation = new HSSFDataValidation(ranglist, dvconstraint);
                    ((HSSFSheet)c.Sheet).AddValidationData(dataValidation);

                    string v1 = npl.ReadRowData(r, 0);
                    string v2 = npl.ReadRowData(r, 1);
                    string v3 = npl.ReadRowData(r, 2);
                    Response.Write("<tr><td>" + v1 + "</td><td>" + v2 + "</td><td>" + v3 + "</td></tr>");
                }
            }
            Response.Write("</table>");

        }

        void Button1_Click(object sender, EventArgs e)
        {
            excel();
        }

        void excel()
        {
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("c1", typeof(string));
            DataColumn dc2 = new DataColumn("c2", typeof(string));
            DataColumn dc3 = new DataColumn("c3", typeof(string));
            DataColumn dc4 = new DataColumn("c4", typeof(string));
            DataColumn dc5 = new DataColumn("c5", typeof(string));
            DataColumn dc6 = new DataColumn("c6", typeof(string));

            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);

            for (int i = 0; i < 20; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = new Random((int)DateTime.Now.Ticks).Next(10, 20).ToString();
                dr[1] = new Random((int)DateTime.Now.Ticks - 222).Next(1000, 2000).ToString();
                dr[2] = new Random((int)DateTime.Now.Ticks - 333).Next(1550, 20000).ToString();
                dr[3] = new Random((int)DateTime.Now.Ticks - 444).Next(19990, 299900).ToString();
                dr[4] = new Random((int)DateTime.Now.Ticks - 555).Next(1, 200).ToString();
                dr[5] = new Random((int)DateTime.Now.Ticks - 666).Next(23, 45).ToString();



                dt.Rows.Add(dr);
            }


            //create table;
            NpoiLib npl = new NpoiLib();
            Sheet sheet = npl.CreateSheet("sheet11");

            string[] explicitListValues = new string[] { "AS", "CE CP", "SC IC", "Others" };
            int i0 = 0;
            foreach (DataRow dr in dt.Rows)
            {
                Row row = npl.CreateRow(sheet, i0);
                i0++;
                npl.WriteCell(row, i0, 0, dr[0].ToString());
                npl.WriteCell(row, i0, 1, dr[1].ToString());
                npl.WriteCell(row, i0, 2, dr[2].ToString());
                npl.WriteCell(row, i0, 3, dr[3].ToString());
                npl.WriteCell(row, i0, 4, dr[4].ToString());
                npl.WriteCell(row, i0, 5, dr[5].ToString());

                //NPOI.SS.Util.CellRangeAddressList ranglist = new NPOI.SS.Util.CellRangeAddressList();
                //ranglist.AddCellRangeAddress(new CellRangeAddress(0, 10, 0, 3));

                //DVConstraint dvconstraint = DVConstraint.CreateExplicitListConstraint(explicitListValues);
                //HSSFDataValidation dataValidation = new HSSFDataValidation(ranglist, dvconstraint);
                //((HSSFSheet)sheet).AddValidationData(dataValidation);
            }

            npl.SaveExcel(Server.MapPath("text.xls"));
            Response.Write("<script>alert(\"CreateSuc\");</script>");
        }
    }
}