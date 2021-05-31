﻿using System;
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

        //public EmailService(SmtpClient client, string fromEmail)
        //{
        //    Client = client;
        //    FromEmail = new MailAddress(fromEmail);
        //}


        public async Task<bool> Send(string toEmail, string subject, string message)
        {
            MailMessage mailMessage = new MailMessage(FromEmail, new MailAddress(toEmail))
            {
                Subject = subject,
                Body = message
            };

            await Client.SendMailAsync(mailMessage);

            return true;
        }
    }
}

