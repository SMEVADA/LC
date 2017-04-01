using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mail;

namespace LetsConnect.Services.Repository.RCommon
{
  public class InfoMailRepository
    {
        string FromEmail = Convert.ToString(WebConfigurationManager.AppSettings["FromEmail"]);
        string Password = Convert.ToString(WebConfigurationManager.AppSettings["Password"]);
        string ToEmail = Convert.ToString(WebConfigurationManager.AppSettings["ToEmail"]);

        public void SendEmail(string Mailsubject, string Mailbody, string MailType, string Mailto = "inquiry@baadam.com")
        {
            #region send mail to info@Baadam.com

            GMailer.GmailUsername = "info.baadam@gmail.com";
            if (!string.IsNullOrEmpty(FromEmail))
                GMailer.GmailUsername = FromEmail;



            GMailer.GmailPassword = "baadam@345$";
            if (!string.IsNullOrEmpty(Password))
                GMailer.GmailPassword = Password;


            GMailer mailer = new GMailer();

            mailer.ToEmail = "info@Baadam.com";
            if (Mailto != "test@gmail.com")
            {
                if (!string.IsNullOrEmpty(Mailto))
                    mailer.ToEmail = Mailto;
            }

            if (!string.IsNullOrEmpty(ToEmail))
                mailer.ToEmail = ToEmail;

            mailer.Subject = Mailsubject;
            mailer.Body = Mailbody;
            mailer.IsHtml = true;
            mailer.Send();
            //-------------------------------------------
            //create the mail message
            //MailMessage mail = new MailMessage();

            //var client = new SmtpClient {
            //    Credentials = new NetworkCredential("info.baadam@gmail.com", "baadam@345$"),
            //    EnableSsl = true,
            //    Port = 587,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Host = "smtp.gmail.com"
            //};

            ////set the addresses
            //mail.From = new MailAddress("info.baadam@gmail.com");
            //mail.To.Add("suhasmewada728@gmail,com");

            ////set the content
            //mail.Subject = "Test subject!";
            //mail.Body = Mailbody;
            //mail.IsBodyHtml = true;
            //client.Send(mail);
            //--------------------------------------
            //MailMessage msg = new MailMessage();
            ////Add your email address to the recipients
            //msg.To.Add("info.baadam@gmail.com");
            ////Configure the address we are sending the mail from
            //MailAddress address = new MailAddress("info.baadam@gmail.com");
            //msg.From = address;
            //msg.Subject = Mailsubject;
            //msg.Body = Mailbody;

            ////Configure an SmtpClient to send the mail.
            //SmtpClient client = new SmtpClient();
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = false;
            //client.Host = "relay-hosting.secureserver.net";
            //client.Port = 25;

            ////Setup credentials to login to our sender email address ("UserName", "Password")
            //NetworkCredential credentials = new NetworkCredential("user@mydomain.com", "Password");
            //client.UseDefaultCredentials = true;
            //client.Credentials = credentials;

            ////Send the msg
            //client.Send(msg);

            #endregion
        }

        public class GMailer
        {
            public static string GmailUsername { get; set; }
            public static string GmailPassword { get; set; }
            public static string GmailHost { get; set; }
            public static int GmailPort { get; set; }
            public static bool GmailSSL { get; set; }

            public string ToEmail { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public bool IsHtml { get; set; }

            static GMailer()
            {
                string Host = Convert.ToString(WebConfigurationManager.AppSettings["Host"]);
                int Port = Convert.ToInt32(WebConfigurationManager.AppSettings["Port"]);
                bool EnableSSL = Convert.ToBoolean(WebConfigurationManager.AppSettings["EnableSSL"]);

                GmailHost = "relay-hosting.secureserver.net";
                if (!string.IsNullOrEmpty(Host))
                    GmailHost = Host;

                GmailPort = 25; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
                if (Port != 0)
                    GmailPort = Port;

                GmailSSL = true;
                //EnableSSL = false;
                //GmailSSL = EnableSSL;

            }

            public string Send()
            {
                try
                {
                    #region local code
                    // SmtpClient smtp = new SmtpClient();
                    // smtp.Host = GmailHost;
                    //// smtp.Host = "smptout.secureserver.net";
                    // smtp.Port = GmailPort;
                    // smtp.EnableSsl = GmailSSL;
                    // smtp.Timeout = 10000;
                    // smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    // smtp.UseDefaultCredentials = false;
                    // smtp.Credentials = new NetworkCredential(GmailUsername, GmailPassword);
                    // using (var message = new MailMessage(GmailUsername, ToEmail))
                    // {
                    //     message.Subject = Subject;
                    //     message.Body = Body;
                    //     message.IsBodyHtml = IsHtml;
                    //     smtp.Send(message);
                    // }

                    #endregion

                    #region godday server code
                    const string SERVER = "relay-hosting.secureserver.net";
                    System.Web.Mail.MailMessage oMail = new System.Web.Mail.MailMessage();
                    oMail.From = "inquiry@baadam.com";
                    oMail.To = "info@baadam.com";
                    oMail.Subject = Subject;
                    oMail.BodyFormat = MailFormat.Html; // enumeration
                    oMail.Priority = System.Web.Mail.MailPriority.High; // enumeration
                    oMail.Body = Body;
                    SmtpMail.SmtpServer = SERVER;
                    SmtpMail.Send(oMail);
                    oMail = null;   // free up resources
                    #endregion

                    return "success";
                }
                catch (Exception ex)
                {
                    string Body = ex.ToString() + "<br/>" + ex.InnerException.ToString() + "<br />" + ex.Message.ToString() + "<br />" + ex.StackTrace.ToString() + "<br />" + ex.Source.ToString() + "<br />" + ex.Message.ToString();
                    return Body;
                }
            }
        }
    }
}
