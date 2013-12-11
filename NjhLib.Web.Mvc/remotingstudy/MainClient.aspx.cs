using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace NjhLib.Web.Mvc.remotingstudy
{
    public partial class MainClient : System.Web.UI.Page
    {
        /// <summary>
        /// remoting的client端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //确立通道传送方式
            ChannelServices.RegisterChannel(new TcpClientChannel(), false);
            /////使用Activator.GetObject（）或者Activator.CreateInstance（）方法建立透明代理，控制远程对象
            PersonManager personManager = Activator.GetObject(typeof(PersonManager), "tcp://localhost:8089/PersonUri") as PersonManager;

            List<Person> personList = new List<Person>();
            personList = personManager.GetList();
            Response.Write("count:" + personList.Count());
        }
    }
}