using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ForsaWebAPI.Helper
{
    public class EmailHelper
    {
        private string _emailFrom = null;

        public string EmailFrom
        {
            get
            {
                if (_emailFrom == null)
                {
                    _emailFrom = ConfigurationManager.AppSettings["EmailFrom"].ToString();
                }
                return _emailFrom;
            }
            set
            {
                _emailFrom = value;
            }
        }

        private string _password = null;
        public string EmailFromPassword
        {
            get
            {
                if (_password == null)
                {
                    _password = ConfigurationManager.AppSettings["Password"].ToString();
                }
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        private string _smtpClient = null;
        public string SMTPClient
        {
            get
            {
                if (_smtpClient == null)
                {
                    _smtpClient = ConfigurationManager.AppSettings["SMTPCLIENT"].ToString();
                }
                return _smtpClient;
            }
            set
            {
                _smtpClient = value;
            }
        }

        private int _smtpPort = 0;
        public int SMTPPort
        {
            get
            {
                if (_smtpPort == 0)
                {
                    _smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString());
                }
                return _smtpPort;
            }
            set
            {
                _smtpPort = value;
            }
        }


        // Note: To send email you need to add actual email id and credential so that it will work as expected  
        public static readonly string EMAIL_SENDER = "xyz.abc@outlook.com"; // change it to actual sender email id or get it from UI input  
        public static readonly string EMAIL_CREDENTIALS = "*******"; // Provide credentials   
        public static readonly string SMTP_CLIENT = "smtp-mail.outlook.com"; // as we are using outlook so we have provided smtp-mail.outlook.com   
        public static readonly string EMAIL_BODY = "Reset your Password <a href='http://{0}.safetychain.com/api/Account/forgotPassword?{1}'>Here.</a>";
        private string senderAddress;
        private string clientAddress;
        private string netPassword;
        public EmailHelper(string sender, string Password, string client)
        {
            senderAddress = sender;
            netPassword = Password;
            clientAddress = client;
        }
        public EmailHelper()
        {

        }
        public bool SendEMail(string recipient, string subject, string message)
        {
            bool isMessageSent = false;
            //Intialise Parameters  
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(SMTPClient);
            client.Port = SMTPPort;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(EmailFrom.Trim(), EmailFromPassword.Trim());
            client.EnableSsl = true;
            client.Credentials = credentials;
            try
            {
                var mail = new System.Net.Mail.MailMessage(EmailFrom.Trim(), recipient.Trim());
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                //System.Net.Mail.Attachment attachment;  
                //attachment = new Attachment(@"C:\Users\XXX\XXX\XXX.jpg");  
                //mail.Attachments.Add(attachment);  
                client.Send(mail);
                isMessageSent = true;
            }
            catch (Exception ex)
            {
                HelperClass.WriteMessage(ex);
                isMessageSent = false;
            }
            return isMessageSent;
        }

    }
}