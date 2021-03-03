using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class Smtp
    {
        //private string mail_host = "smtp..com";  
        //private string mail_user = "@qq.com";
        //private string mail_pass = "";
        //private string sender = "1791746286@qq.com";
        //private string receivers = "xincheng213618@gmail.com";
        public void SMTPPUT()
        {
            //SmtpClient mailClient = new SmtpClient(mail_host);
            //mailClient.Credentials = new NetworkCredential(mail_user, mail_pass);
            
            //MailMessage message = new MailMessage("1791746286@qq.com");
            //// message.Bcc.Add(new MailAddress("tst@qq.com")); //可以添加多个收件人
            //message.Body = "Hello Word!";//邮件内容
            //message.Subject = "this is a test";//邮件主题
            //Attachment att = new Attachment(@"C:\Users\TOPO\Desktop\123.png");

            //message.Attachments.Add(att);//添加附件
            //mailClient.Send(message);
        }
    }
}
