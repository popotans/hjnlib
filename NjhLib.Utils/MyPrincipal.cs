using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Threading;

namespace NjhLib.Utils
{
    public class MyPrincipal
    {
        WindowsPrincipal wp = (WindowsPrincipal)Thread.CurrentPrincipal;
        /// <summary>
        /// 域用户名
        /// </summary>
        public string UserName
        {
            get
            {
                string[] wpArray = wp.Identity.Name.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                return wpArray.Length > 0 ? wpArray[1] : "";
            }
        }
        /// <summary>
        /// 域
        /// </summary>
        public string UserDomain
        {
            get
            {
                string[] wpArray = wp.Identity.Name.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                return wpArray.Length > 0 ? wpArray[0] : "";
            }
        }
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(UserDomain))
                return string.Format("域用户名：{0}，当前域：{1}", UserName, UserDomain);
            else
                return "未知";
        }

    }
}
