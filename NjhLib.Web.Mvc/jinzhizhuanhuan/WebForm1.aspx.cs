using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
namespace NjhLib.Web.Mvc.jinzhizhuanhuan
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string s = "我是程序猿";
            //byte[] arr = Encoding.GetEncoding("gb2312").GetBytes(s);
            //string rs = "";
            //string tmp = "";
            //foreach (byte b in arr)
            //{
            //    tmp = NjhLib.Utils.StringUtil.ConvertFromTen(b, 2);

            //    rs += (int)b;

            //}
            //Response.Write(rs);
            //go62();
            // Response.Write("<br>" + "ten<br>");
            //  go10();

            string ff = NjhLib.Utils.test.ConvertTenToSixtyTwo(51);
            Response.Write(ff);
        }

        void go62()
        {
            string _s = "6030236512.html$中华人民共和国嘻嘻$4564564.html$564654896asdasdasdas54.html";
            byte[] arr = Encoding.GetEncoding("gb2312").GetBytes(_s);
            string st = "";
            foreach (byte b in arr)
            {
                st += NjhLib.Utils.StringUtil.ConvertTenToSixtyTwo(b).PadLeft(2, '0');
            }
            Response.Write(st);
            Response.Write("<br>" + st.Length);
        }
        void go10()
        {
            string s = "0S0M0P0M0O0P0S0R0N0O0K1G1S1L1K0A3s3m312K3e3h393T2Z2S303j2Z423k433k430A0Q0R0S0Q0R0S0Q0K1G1S1L1K0A0R0S0Q0S0R0Q0U0V0S1z1R1C1z1R1C1z1R1C1z1R0R0Q0K1G1S1L1K";
            //string
            byte[] arr = Encoding.GetEncoding("gb2312").GetBytes(s);
            int length = arr.Length / 2;
            byte[] toarrar = new byte[length];
            long ten = 0;
            for (int i = 0; i < length; i++)
            {
                long j = NjhLib.Utils.StringUtil.ConvertSixtytwoToTen(s.Substring(i * 2, 2));
                toarrar[i] = (byte)j;

            }
            string rs = Encoding.GetEncoding("gb2312").GetString(toarrar);

            string ss = "010S3O2e1Z1b0s2Q3g0N1k0Y3e1F120J371m360z181v272e1U1c071s0W1D0H393J0m0p0r0w0G2M0T1t1W053v2h3B2A2Y311I2i2I230j3e2e0F3y1a0a213X2M2q1d3T3v1a3S0m3v0K1n3S2U0h0A2X1R2S";
            Response.Write("<br>" + rs);
        }
    }
}