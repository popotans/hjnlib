using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
namespace NjhLib.Utils
{
    public class EmailUtil
    {
        public static bool SendEmail(string from, string to, string subject, string body)
        {
            return SendEmail(from, "", to, subject, body, "", MailPriority.Normal);
            //   return true;
        }
        public static bool SendEmail(string from, string displayName, string to, string subject, string body)
        {
            return SendEmail(from, displayName, to, subject, body, "", MailPriority.Normal);
        }

        /// <summary>
        /// 结合配置文件改的
        /// </summary>
        /// <param name="mail"></param>
        public static bool SendEmail(string from, string displayName, string to0, string subject, string body, string encoding, MailPriority prioity)
        {
            if (string.IsNullOrEmpty(displayName))
                displayName = from;
            MailAddress _from = new MailAddress(from, displayName);

            MailAddress _to = new MailAddress(to0);
            MailMessage mail = new MailMessage(_from, _to);
            mail.Subject = subject;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.Default;
            if (!string.IsNullOrEmpty(encoding))
            {
                mail.BodyEncoding = System.Text.Encoding.GetEncoding(encoding);
            }
            mail.IsBodyHtml = true;
            mail.Priority = prioity;

            Configs.Config cfg = new Configs.Config();
            // Override To
            if (!string.IsNullOrEmpty(cfg.Email.MailTo_Override))
            {
                var tos = cfg.Email.MailTo_Override.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                mail.To.Clear();
                foreach (var to in tos)
                {
                    mail.To.Add(to);
                }
            }
            return SendEmail(mail);

        }

        public static bool SendEmail(MailMessage mail)
        {
            try
            {
                Configs.Config cfg = new Configs.Config();

                if (!cfg.Email.Enable)
                    return false;

                // Override To
                if (!string.IsNullOrEmpty(cfg.Email.MailTo_Override))
                {
                    var tos = cfg.Email.MailTo_Override.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    mail.To.Clear();
                    foreach (var to in tos)
                    {
                        mail.To.Add(to);
                    }
                }

                // Override Cc
                if (!string.IsNullOrEmpty(cfg.Email.MailTo_Override))
                {
                    var ccs = cfg.Email.MailCc_Override.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    mail.CC.Clear();
                    foreach (var cc in ccs)
                    {
                        mail.CC.Add(cc);
                    }
                }

                // Override Bcc
                if (!string.IsNullOrEmpty(cfg.Email.MailTo_Override))
                {
                    var bccs = cfg.Email.MailBcc_Override.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    mail.Bcc.Clear();
                    foreach (var bcc in bccs)
                    {
                        mail.CC.Add(bcc);
                    }
                }
                // Override Subject
                if (!string.IsNullOrEmpty(cfg.Email.MailSubject_Override))
                    mail.Subject = cfg.Email.MailSubject_Override;

                // Override Body
                if (!string.IsNullOrEmpty(cfg.Email.MailSubject_Override))
                    mail.Body = cfg.Email.MailBody_Override;

                //如果程序中显示指定了emailfrom则不用哦配置文件中的了
                string disPlayName = mail.From.DisplayName;
                if (string.IsNullOrEmpty(cfg.Email.DisplayName)) cfg.Email.DisplayName = cfg.Email.EmailFrom;
                if (string.IsNullOrEmpty(disPlayName)) disPlayName = cfg.Email.DisplayName;

                if (string.IsNullOrEmpty(mail.From.Address))
                    mail.From = new MailAddress(cfg.Email.EmailFrom, disPlayName);
                else
                {
                    mail.From = new MailAddress(mail.From.Address, disPlayName);
                }



                var smtp = new SmtpClient(cfg.Email.SMTP, cfg.Email.Port);
                smtp.EnableSsl = cfg.Email.IsSSL;
                if (cfg.Email.RequireLogin)
                    smtp.Credentials = new NetworkCredential(cfg.Email.Account, cfg.Email.Password);

                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        public static void SendMail(string _from, string _to, string _subject, string _body, Encoding encode, MailPriority prioity, string host, int port, string name, string pwd)
        {
            MailAddress from = new MailAddress(_from);
            MailAddress to = new MailAddress(_to);
            MailMessage mail = new MailMessage(from, to);
            mail.Subject = _subject;
            mail.Body = _body;
            mail.BodyEncoding = encode == null ? System.Text.Encoding.Default : encode;
            mail.IsBodyHtml = true;
            mail.Priority = prioity;

            SmtpClient sc = new SmtpClient();
            sc.Host = host;
            sc.Port = port;
            sc.UseDefaultCredentials = false;
            sc.Credentials = new System.Net.NetworkCredential(name, pwd);
            sc.DeliveryMethod = SmtpDeliveryMethod.Network;
            sc.Send(mail);
        }
    }
}
