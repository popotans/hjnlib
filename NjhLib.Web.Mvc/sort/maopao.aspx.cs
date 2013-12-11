using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NjhLib.Web.Mvc.sort
{
    public partial class maopao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int[] arr = new int[] { 
            1,5,9,3,6,4,7,2,10,15,12,14,19,18,23,22,25,24
            };

            Response.Write("<br/>排序前：");
            foreach (int i in arr)
            {
                Response.Write(i + ",");
            }
             sort(arr);
            Response.Write("<br/>排序后：");
            foreach (int i in arr)
            {
                Response.Write(i + ",");
            }
        }


        private void sort(int[] arr)
        {
            int tmp = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length - 1; j++)
                {
                    if (arr[i] < arr[j])
                    {
                        tmp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = tmp;
                    }
                }
            }

        }
    }
}