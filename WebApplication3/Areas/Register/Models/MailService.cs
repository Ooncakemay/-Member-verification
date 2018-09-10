using System;
using System.Collections.Generic;
using System.Configuration;
                             using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication3.Areas.Register.Models
{
    public class MailService
    {
        private string gmail_account = ConfigurationManager.AppSettings["gmail_account"];
        private string gmail_password = ConfigurationManager.AppSettings["gmail_password"];
        private string gmail_mail = ConfigurationManager.AppSettings["gmail_mail"];
        private int portNum = Convert.ToInt32(ConfigurationManager.AppSettings["mail_port_num"]);//best？

        public string GenerateEmailToken()
        {
            // generate an email verification token for the user
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[16];
                provider.GetBytes(data);
                return Convert.ToBase64String(data);
            }
            
        }
        public void SendRegisterMail(string MailBody, string toEMail) {
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = portNum;
            smtpServer.Credentials = new System.Net.NetworkCredential(gmail_account, gmail_password);
            smtpServer.EnableSsl = true;//開啟SSL連線
            MailMessage mail = new MailMessage(); //宣告信件內容物
            mail.From = new MailAddress(gmail_mail, "小魚", Encoding.UTF8);
            mail.To.Add(toEMail); //設定收信者信箱
            mail.Subject = "會員認證信"; //信件主旨
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Body = MailBody; //信件內容
            mail.BodyEncoding = Encoding.UTF8;
            mail.Priority = MailPriority.High;
            mail.IsBodyHtml = true; //信件格式為html
          
            smtpServer.Send(mail); //送出信件 
            smtpServer.Dispose();
            mail.Dispose();
        }
        public string GetRegisterBody(string content,string UserName,string vaildateUrl) {
            content.Replace("{{UserName}}", UserName);
            content.Replace("{{VailDateUril}}", vaildateUrl);

            return content;
        }


    }
}