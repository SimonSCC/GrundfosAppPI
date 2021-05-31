using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services
{
    public class EmailService
    {
        public SmtpClient Client { get; set; }
        private MailAddress FromEmail { get; set; }

        public EmailService(string host, int port, NetworkCredential credential, bool ssl)
        {
            FromEmail = new MailAddress(credential.UserName);
            Client = new SmtpClient(host)
            {
                Port = port,
                Credentials = credential,
                EnableSsl = ssl,
            };
        }

        public void Send(string toEmail, string subject, string message)
        {
            MailMessage mailMessage = new MailMessage(FromEmail, new MailAddress(toEmail))
            {
                Subject = subject,
                Body = message
            };

            Client.Send(mailMessage);
        }
    }
}

