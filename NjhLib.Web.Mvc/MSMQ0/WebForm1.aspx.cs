using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Messaging;
namespace NjhLib.Web.Mvc.MSMQ0
{
    /*在.NET产品中，提供了一个MSMQ类库"System.Messaging.dll"。它提供了两个类分别对消息对象和消息队列对象进行操作
     * 队列类型及其相应的路径格式：
Public:  [MachineName]\[QueueName]
Private:  [MachineName]\Private$\[QueueName]
Journal:  [MachineName]\[QueueName]\Journal$
Machine journal:  [MachineName]\Journal$
Machine dead-letter:  [MachineName]\DeadLetter$
Machine transactional dead-letter:  [MachineName]\XactDeadLetter$
The first portion of the path indicates a computer or domain name or uses a period (.) to indicate the current computer.
     * string path = "FORMATNAME:DIRECT=TCP:10.10.43.169\\Private$\\ptest1";
     */
    public partial class WebForm1 : System.Web.UI.Page
    {
        string path = ".\\Private$\\MSMQDemo";
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public void Send(object obj)
        {
            // Open queue
            System.Messaging.MessageQueue queue = new System.Messaging.MessageQueue(path);
            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path);
            }
            // Create message
            System.Messaging.Message message = new System.Messaging.Message();
            message.Body = obj;
            message.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { obj.GetType() });
            queue.Send(message);
        }

        public object Receive(Type type)
        {
            // Open queue
            if (!MessageQueue.Exists(path)) { throw new Exception("queue does't exist!"); }
            System.Messaging.MessageQueue queue2 = new System.Messaging.MessageQueue(path);
            // Receive message, 同步的Receive方法阻塞当前执行线程，直到一个message可以得到
            System.Messaging.Message message2 = queue2.Receive();
            message2.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { type });
            object obj = message2.Body;
            return obj;
        }
        public void ReceiveAsync()
        {
            if (!MessageQueue.Exists(path)) { throw new Exception("queue does't exist!"); }
            System.Messaging.MessageQueue queue2 = new System.Messaging.MessageQueue(path);
            queue2.ReceiveCompleted += new ReceiveCompletedEventHandler(queue2_ReceiveCompleted);
            queue2.BeginReceive();

        }

        void queue2_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            System.Messaging.MessageQueue queue = (System.Messaging.MessageQueue)sender;

            System.Messaging.Message msg = queue.EndReceive(e.AsyncResult);
            msg.Formatter = new XmlMessageFormatter(new Type[] { e.AsyncResult.GetType() });
            object obj = msg.Body;
            Response.Write(obj);
        }



        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            Student s = new Student { Age = 25, Name = "niejunhua" + DateTime.Now };
            //Send("123" + DateTime.Now);
            Send(s);
        }

        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            //object obj = Receive(typeof(Student));
            //Response.Write(obj);
            ReceiveAsync();
        }

    }
    [Serializable]
    public class Student
    {
        public Student()
        {
        }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return "student:name" + Name + ",age:" + Age;
        }
    }

}