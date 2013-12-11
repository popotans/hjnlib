using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NjhLib.Utils
{
    /// <summary>
    /// 链表类
    /// </summary>
    public class CList
    {
        
        public CList()//构造函数    
        {
            ListCountValue = 0; //初始化  
            Head = null; Tail = null;
        }
        private ListNode Head; //头指针   
        private ListNode Tail; //尾指针   
        private ListNode Current; //当前指针 
        private int ListCountValue; //链表数据的个数

        public void Append(int DataValue)
        {
            ListNode NewNode = new ListNode(DataValue);
            if (IsNull())//如果头指针为空     
            {
                Head = NewNode;
                Tail = NewNode;
            }
            else
            {
                Tail.Next = NewNode;
                NewNode.Previous = Tail;
                Tail = NewNode;
            } Current = NewNode;
            ListCountValue += 1; //链表数据个数加1 
        }


        public void Delete()
        {
            if (!IsNull())//若为空链表    
            {
                if (IsBof())//若删除头        
                {
                    Head = Current.Next;
                    Current = Head; ListCountValue -= 1;
                    return;
                } if (IsEof())//若删除尾   
                {
                    Tail = Current.Previous; Current = Tail;
                    ListCountValue -= 1; return;
                }
                Current.Previous.Next = Current.Next; //若删除中间数据     
                Current = Current.Previous; ListCountValue -= 1;
                return;
            }
        }
        //向后移动一个数据  
        public void MoveNext() { if (!IsEof()) Current = Current.Next; }   //向前移动一个数据
        public void MovePrevious() { if (!IsBof()) Current = Current.Previous; }   //移动到第一个数据 

        public void MoveFrist() { Current = Head; }   //移动到最后一个数据  
        public void MoveLast() { Current = Tail; }   //判断是否为空链表  
        public bool IsNull() { if (ListCountValue == 0)           return true; return false; }   //判断是否为到达尾部  
        public bool IsEof() { if (Current == Tail)           return true; return false; }   //判断是否为到达头部 
        public bool IsBof() { if (Current == Head)           return true; return false; }   //获取节点值   
        public int GetCurrentValue() { return Current.Value; }   //取得链表的数据个数   
        public int ListCount { get { return ListCountValue; } }   //清空链表 
        public void Clear()
        {
            MoveFrist(); while (!IsNull())
            {
                Delete();//若不为空链表，从尾部删除    
            }
        }

        public void Insert(int DataValue)
        {
            ListNode NewNode = new ListNode(DataValue);
            if (IsNull())
            {
                Append(DataValue); //为空表，则添加         
                return;
            } if (IsBof())
            {           //为头部插入      
                NewNode.Next = Head; Head.Previous = NewNode; Head = NewNode;
                Current = Head; ListCountValue += 1; return;
            }       //中间插入  
            NewNode.Next = Current; NewNode.Previous = Current.Previous; Current.Previous.Next = NewNode;
            Current.Previous = NewNode; Current = NewNode;
            ListCountValue += 1;
        }
        public void InsertAscending(int InsertValue)
        {       //参数：InsertValue 插入的数据    
            if (IsNull())//为空链表      
            {
                Append(InsertValue); //添加   
                return;
            } MoveFrist();//移动到头部   
            if ((InsertValue < GetCurrentValue()))
            {
                Insert(InsertValue); //满足条件，则插入，退出    
                return;
            } while (true)
            {
                if (InsertValue < GetCurrentValue())
                {
                    Insert(InsertValue); //满足条件，则插入，退出      
                    break;
                }
                if (IsEof())
                {
                    Append(InsertValue); //尾部添加      
                    break;
                } MoveNext();//移动到下一个指针    
            }
        }

        public void InsertUnAscending(int InsertValue)
        {       //参数：InsertValue 插入的数据    
            if (IsNull())//为空链表      
            {
                Append(InsertValue); //添加           
                return;
            } MoveFrist();//移动到头部      
            if (InsertValue > GetCurrentValue())
            {
                Insert(InsertValue); //满足条件，则插入，退出    
                return;
            } while (true)
            {
                if (InsertValue > GetCurrentValue())
                {
                    Insert(InsertValue); //满足条件，则插入，退出      
                    break;
                }
                if (IsEof())
                {
                    Append(InsertValue); //尾部添加       
                    break;
                } MoveNext();//移动到下一个指针      
            }
        }

    }

    /// <summary>
    /// 节点类
    /// </summary>
    public class ListNode
    {
        public ListNode(int NewValue)
        {
            this.Value = NewValue;
        }
        //前一个 
        public ListNode Previous { get; set; }
        //后一个 
        public ListNode Next { get; set; }

        public int Value { get; set; }
    }

    public class DoubleClst
    {
        //public int Number { get; set; }
        //public string ClistName { get; set; }

        //public int ListCountValue { get; set; }
        //public ListNode Head;//头指针    
        //public ListNode Tail;//尾指针  
        //public ListNode Current { get; set; }
        //public DoubleClst(int num, string Name, int count)//构造函数  
        //{
        //    ListCountValue = 0; //初始化  
        //    Head = null;
        //    Tail = null;
        //}

        //public void Append(Object DataValue)
        //{
        //    ListNode NewNode = new ListNode(DataValue);
        //    if (IsNull())    //如果头指针为空  
        //    {
        //        Head = NewNode; Tail = NewNode;
        //    }
        //    else
        //    {
        //        Tail.Next = NewNode;
        //        NewNode.Previous = Tail; Tail = NewNode;
        //    } Current = NewNode; ListCountValue += 1; //链表数据个数加1
        //}



        //public void Delete()
        //{
        //    if (!IsNull())        //若为空链表   
        //    {
        //        if (IsBof())    //若删除头部   
        //        {
        //            Head = Current.Next;
        //            Current = Head; ListCountValue -= 1;
        //            return;
        //        } if (IsEof())     //若删除尾部 
        //        {
        //            Tail = Current.Previous; Tail.Next = null;
        //            Current = Tail; ListCountValue -= 1;
        //            return;
        //        } Current.Previous.Next = Current.Next;
        //        Current = Current.Previous; ListCountValue -= 1;
        //        return;
        //    }
        //}  //向后移动一个数据 
        //public void MoveNext()
        //{
        //    if (!IsEof()) Current = Current.Next;
        //}  //向前移动一个数据 
        //public void MovePrevious() { if (!IsBof()) Current = Current.Previous; }  //移动到第一个数据 
        //public void MoveFrist() { Current = Head; }  //移动到最后一个数据 
        //public void MoveLast() { Current = Tail; }  //判断是否为空链表 
        //public bool IsNull()
        //{
        //    if (ListCountValue == 0) return true;
        //    else return false;
        //}  //判断是否到达尾部 
        //public bool IsEof()
        //{
        //    if (Current == Tail)
        //        return true;
        //    else return false;
        //}  //判断是否到达头部 
        //public bool IsBof()
        //{
        //    if (Current == Head) return true;
        //    else return false;
        //}  //获取值  
        //public Object GetCurrentValue()
        //{
        //    return Current.Value;
        //}  //取得链表的数据个数  
        //public int ListCount
        //{
        //    get
        //    {
        //        return ListCountValue;
        //    }
        //}
        //public void Clear()
        //{
        //    MoveFrist(); while (!IsNull())
        //    {
        //        Delete();                                       //若不为空链表，从尾部删除
        //    }
        //}                                                      //在当前位置前插入数据 
        //public void Insert(Object DataValue)
        //{
        //    ListNode NewNode = new ListNode(DataValue);
        //    if (IsNull())
        //    {
        //        Append(DataValue);                              //为空表，则添加 
        //        return;
        //    } if (IsBof())
        //    {                                                      //为头部插入  
        //        NewNode.Next = Head; Head.Previous = NewNode; Head = NewNode;
        //        Current = Head;
        //        ListCountValue += 1; return;
        //    } NewNode.Next = Current;
        //    //中间插入     
        //    NewNode.Previous = Current.Previous;
        //    Current.Previous.Next = NewNode;
        //    Current.Previous = NewNode;
        //    Current = NewNode;
        //    ListCountValue += 1;
        //}

        //public void InsertAscending(Object InsertValue)
        //{
        //    //参数：InsertValue 插入的数据    
        //    if (IsNull())                                           //为空链表 
        //    {
        //        Append(InsertValue);                                //添加  
        //        return;
        //    } MoveFrist();                                        //移动到头部     
        //    if ((InsertValue.Number < GetCurrentValue().Number))
        //    {
        //        Insert(InsertValue);
        //        //满足条件，则插入，退出        
        //        return;
        //    } while (true)
        //    {
        //        if (InsertValue.Number < GetCurrentValue().Number)
        //        {
        //            Insert(InsertValue);
        //            //满足条件，则插入，退出       
        //            break;
        //        } if (IsEof())
        //        {
        //            Append(InsertValue);
        //            //尾部添加           
        //            break;
        //        }
        //        MoveNext();
        //        //移动到下一个指针  
        //    }
        //}
        //public void InsertUnAscending(Object InsertValue)
        //{
        //    //参数：InsertValue 插入的数据    
        //    if (IsNull())                                           //为空链表
        //    {
        //        Append(InsertValue);                                //添加
        //        return;
        //    } MoveFrist();                                        //移动到头部 
        //    if (InsertValue.Number > GetCurrentValue().Number)
        //    {
        //        Insert(InsertValue);
        //        //满足条件，则插入，退出   
        //        return;
        //    } while (true)
        //    {
        //        if (InsertValue.Number > GetCurrentValue().Number)
        //        {
        //            Insert(InsertValue);
        //            //满足条件，则插入，退出          
        //            break;
        //        } if (IsEof())
        //        {
        //            Append(InsertValue);
        //            //尾部添加             
        //            break;
        //        }
        //        MoveNext();
        //        //移动到下一个指针    
        //    }
        //}




















    }

    public class ListNode2//类 
    {
        public ListNode2(Object bugs)//构造函数  
        { goods = bugs; }
        public ListNode Previous;//前一个  
        public ListNode Next;//后一个    

        public Object goods;//值    

    }


}
