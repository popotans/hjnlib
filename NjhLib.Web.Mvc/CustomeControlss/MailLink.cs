using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.ComponentModel;
using System.Web.UI;
namespace NjhLib.Web.Mvc.CustomeControlss
{
    public class MailLink : System.Web.UI.WebControls.WebControl
    {
        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("The e-mail address.")]
        public virtual string Email
        {
            get
            {
                string s = ViewState["email"] as string;
                return string.IsNullOrEmpty(s) ? string.Empty : s;
            }
            set
            {
                ViewState["email"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue(""), Description("The text to display on the link."), Localizable(true),
            System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerDefaultProperty)
        ]
        public virtual string Text
        {
            get { string s = (string)ViewState["Text"]; return (s == null) ? String.Empty : s; }
            set
            {
                ViewState["Text"] = value;
            }
        }

        protected override System.Web.UI.HtmlTextWriterTag TagKey { get { return HtmlTextWriterTag.A; } }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "mailto:" + Email);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
            if (string.IsNullOrEmpty(Text)) Text = Email;
            writer.WriteEncodedText(Text);
        }
    }
}