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
        public SmtpClient client { get; set; }
        private MailAddress fromEmail { get; set; }

        public EmailService(string host, int port, NetworkCredential credential, bool ssl)
        {
            fromEmail = new MailAddress(credential.UserName);
            client = new SmtpClient(host)
            {
                Port = port,
                Credentials = credential,
                EnableSsl = ssl,
            };
        }


        public async Task<bool> Send(string toEmail, string subject, string message)
        {
            MailMessage mailMessage = new MailMessage(fromEmail, new MailAddress(toEmail))
            {
                Subject = subject,
                Body = message
            };

            await client.SendMailAsync(mailMessage);

            return true;
        }
    }
}

