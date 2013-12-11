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
    public partial class Main : System.Web.UI.Page
    {
        /// <summary>
        /// 创建remoting的服务器端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            /// //建立服务传输方式，可选择TCP或者HTTP，前者更能发挥高效性
            TcpServerChannel tcp = new TcpServerChannel(8089);
            /// //注册通道
            ChannelServices.RegisterChannel(tcp, false);
            //添加可调用的远程对象，WellKonwnObjectMode可选择为SingleTon或者SingleCall
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(PersonManager), "PersonUri", WellKnownObjectMode.Singleton);

        }
    }
}