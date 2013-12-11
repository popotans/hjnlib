using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NjhLib.Utils
{
    public class CSort
    {


    }
    //选择排序 
    public class SelectionSorter
    {
        private int min;
        public void Sort(int[] list)
        {
            for (int i = 0; i < list.Length - 1; i++)
            //遍历数组中的数据，不包含最后一个        
            {
                min = i;
                //读取当前数据           
                for (int j = i + 1; j < list.Length; j++)
                //遍历当前数据以后的数据           
                {
                    if (list[j] < list[min])
                        //如果当前是最小值                 
                        min = j;
                    //将最小值放在相应位置          
                }
                int t = list[min];
                list[min] = list[i];
                //交换数据        
                list[i] = t;
            }
        }
    }

    //插入排序 
    public class InsertionSorter
    {
        public void Sort(int[] list)
        {
            for (int i = 1; i < list.Length; i++)
            //遍历当前数组，不包含第一个和最后一个    
            {
                int t = list[i];
                //获取当前值        
                int j = i;
                //记录当前值的标记          
                while ((j > 0) && (list[j - 1] > t))
                //插入法            
                {
                    list[j] = list[j - 1];
                    //交换顺序                
                    --j;
                } list[j] = t;
            }
        }
    }

    //希尔排序
    public class ShellSorter
    {
        public void Sort(int[] list)
        {
            int inc;
            for (inc = 1; inc <= list.Length / 9; inc = 3 * inc + 1) ;
            //遍历当前数组        
            for (; inc > 0; inc /= 3)
            //遍历当前值          
            {
                for (int i = inc + 1; i <= list.Length; i += inc)
                {
                    int t = list[i - 1];
                    //获取值               
                    int j = i;
                    while ((j > inc) && (list[j - inc - 1] > t))
                    //希尔排序法             
                    {
                        list[j - 1] = list[j - inc - 1];
                        //交换数据                      
                        j -= inc;
                    } list[j - 1] = t;
                }
            }
        }
    }
}
