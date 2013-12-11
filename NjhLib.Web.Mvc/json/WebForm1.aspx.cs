using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

namespace NjhLib.Web.Mvc.json
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string JsonScores = "";
            /*
                "[";
            JsonScores += "{\"Id\":1,\"ActionName\":\"发贴\",\"Score\":10,\"IsMulti\":false,\"MultiScore\":null},";
            JsonScores += "{\"Id\":2,\"ActionName\":\"回贴\",\"Score\":20,\"IsMulti\":false,\"MultiScore\":null},";
            JsonScores += "{\"Id\":3,\"ActionName\":\"设为精华\",\"Score\":0,\"IsMulti\":true,\"MultiScore\":[1,2,3]},";
            JsonScores += "{\"Id\":4,\"ActionName\":\"删贴\",\"Score\":-1,\"IsMulti\":false,\"MultiScore\":null},";
            JsonScores += "{\"Id\":5,\"ActionName\":\"删贴\",\"Score\":0,\"IsMulti\":true,\"MultiScore\":[-1,-2,-3]}";
            JsonScores += "]";
             * */
            JsonScores = "[{\"Id\":5,\"ActionName\":\"删贴\",\"Score\":0,\"IsMulti\":true,\"MultiScore\":[1,2,3,5,6,98,9]},";
            JsonScores += "{\"Id\":6,\"ActionName\":\"发贴\",\"Score\":0,\"IsMulti\":true,\"MultiScore\":[1,2]}";
            JsonScores += "]";
            System.Web.Script.Serialization.JavaScriptSerializer jse = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> dic = jse.Deserialize<List<Dictionary<string, object>>>(JsonScores);
            // Response.Write(dic.Count);
            object obj = dic[0]["MultiScore"];

            if (obj != null)
            {
                foreach (int item in obj as System.Collections.ArrayList)
                {
                    Response.Write(item + "<br>");
                }
            }
            else
            {
                Response.Write("obj is nul" + "<br>");
            }


            //Response.Write(dic["ActionName"]);
            //foreach (int item in dic["MultiScore"] as System.Collections.ArrayList)
            //{
            //    Response.Write(item.ToString());
            //}
        }
    }

    public class Scores
    {
        public int ID { get; set; }
        public ScoreActionName ActionName { get; set; }
        public int Score { get; set; }
        public bool IsMulti { get; set; }
        public string[] MultiScore { get; set; }

        public static string GetActionNameStr(ScoreActionName san)
        {
            string str = string.Empty;
            if (san == ScoreActionName.Post) { str = "发帖"; }
            else if (san == ScoreActionName.ReplyPost) { str = "回帖"; }
            else if (san == ScoreActionName.ElitePost) { str = "设为精华"; }
            else if (san == ScoreActionName.DelPostSingle) { str = "删贴"; }
            else if (san == ScoreActionName.DelPostMulti) { str = "删贴"; }
            return str;
        }
    }

    public enum ScoreActionName
    {
        /// <summary>
        /// 发帖
        /// </summary>
        Post,
        /// <summary>
        /// 回帖
        /// </summary>
        ReplyPost,
        /// <summary>
        /// 设为精华
        /// </summary>
        ElitePost,
        /// <summary>
        /// 删贴扣除单一值
        /// </summary>
        DelPostSingle,
        /// <summary>
        /// 删贴 扣除 分数段
        /// </summary>
        DelPostMulti,

    }
}