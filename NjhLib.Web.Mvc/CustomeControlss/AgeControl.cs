using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
namespace NjhLib.Web.Mvc.CustomeControlss
{
    public class AgeCollector : System.Web.UI.WebControls.WebControl
    {
        /// <summary>
        /// 定义属性Prompt
        /// </summary>
        [Bindable(true),
        Category("Appearance"),
        DefaultValue("Please enter your date of birth:"),
        Description("Text to prompt user with."),
        Localizable(true)]
        public virtual String Prompt
        {
            get { string s = (string)ViewState["Prompt"]; return (s == null) ? String.Empty : s; }
            set { ViewState["Prompt"] = value; }
        }

        /// <summary>
        /// 定义属性Datetime
        /// </summary>
        [Bindable(true), Category("Appearance"),
        DefaultValue(""),
        Description("Date of Birth Input area")]
        public virtual DateTime DateOfBirth
        {
            get { object o = ViewState["DateOfBirth"]; return (o == null) ? DateTime.Now : (DateTime)o; }
            set { ViewState["DateOfBirth"] = value; }
        }

        /// <summary>
        /// CreateChildControls 方法
        /// </summary>
        protected override void CreateChildControls()
        {
            //Create and load the label 
            Label lab1 = new Label();
            lab1.Text = Prompt;
            lab1.ForeColor = this.ForeColor;
            this.Controls.Add(lab1);
            //Add a line break between the label and text box  
            Literal lit = new Literal(); lit.Text = ""; this.Controls.Add(lit);
            //Add the Textbox   
            TextBox tb = new TextBox();
            tb.ID = "tb1"; tb.Text = DateOfBirth.ToString(); this.Controls.Add(tb);
            //call the parent method   
            base.CreateChildControls();
        }



    }
}