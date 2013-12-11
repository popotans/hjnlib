using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NjhLib.Utils
{
    public class CStack
    {


        private CList m_List;                                   //调用链表类    
        public CStack() { m_List = new CList(); }
        //构造函数                                                         //压入堆栈  
        public void Push(int PushValue)
        {
            m_List.Append(PushValue);
            //参数： int PushValue 压入堆栈的数据     
        }      //弹出堆栈数据，如果为空，则取得 2147483647 为 int 的最大值      
        public int Pop()
        {
            int PopValue;
            //功能：弹出堆栈数据             
            if (!IsNullStack())
            //不为空堆栈              
            {
                MoveTop();
                //移动到顶部                 
                PopValue = GetCurrentValue();
                //取得弹出的数据              
                Delete();                                    //删除   
                return PopValue;
            } return 2147483647;
            //空的时候为 int 类型的最大值      
        }       //判断是否为空的堆栈     
        public bool IsNullStack()
        {
            if (m_List.IsNull()) return true;
            return false;
        }       //堆栈的个数    
        public int StackListCount
        {
            get
            {
                return m_List.ListCount;
            }
        }       //移动到堆栈的底部     
        public void MoveBottom()
        {
            m_List.MoveFrist();
        }       //移动到堆栈的顶部  
        public void MoveTop()
        {
            m_List.MoveLast();
        }       //向上移动      
        public void MoveUp()
        {
            m_List.MoveNext();
        }       //向下移动     
        public void MoveDown() { m_List.MovePrevious(); }
        //取得当前的值    
        public int GetCurrentValue()
        {
            return m_List.GetCurrentValue();
        }      //删除取得当前的结点    
        public void Delete() { m_List.Delete(); }      //清空堆栈     
        public void Clear() { m_List.Clear(); }











    }
}
