using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NjhLib.Utils
{
    public class CQueue
    {

        private CList m_List;
        public CQueue()
        //构造函数    
        {
            m_List = new CList();
            //这里使用到前面编写的List  
        }                                                              //入队  
        public void EnQueue(int DataValue)
        {
            //功能：加入队列，这里使用List 类的Append方法      
            m_List.Append(DataValue);
            //尾部添加数据，数据个数加1    
        }
        //出队  
        public int DeQueue()
        {
            //功能：出队      
            //返回值： 2147483647 表示为空队列无返回     
            int QueValue; if (!IsNull())
            //不为空的队列     
            {
                m_List.MoveFrist();
                //移动到队列的头         
                QueValue = m_List.GetCurrentValue();
                //取得当前的值            
                m_List.Delete();
                //删除出队的数据          
                return QueValue;
            } return 2147483647;
        }       //判断队列是否为空    
        public bool IsNull()
        {
            return m_List.IsNull();
            //判断是否为空的队列    
        }
        //清空队列      
        public void Clear()
        {
            m_List.Clear();
            //清空链表     
        }       //取得队列的数据个数    
        public int QueueCount
        {
            get
            {
                return m_List.ListCount;
                //取得队列的个数    
            }
        }






















    }
}
