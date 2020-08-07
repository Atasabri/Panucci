using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Panucci.Repositories.Email
{
    public class Email : IEmail
    {
        private readonly IConfiguration configuration;

        public Email(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Send Html E-Mail To Muli-Users
        /// </summary>
        /// <param name="Subject"></param>
        /// <param name="file"></param>
        /// <param name="To"></param>
        public void Send_Mail(string Subject, IFormFile file, List<string> To)
        {
            string From = configuration["Email"];
            string Pass = configuration["Password"];
            string Host = configuration["Host"];
            int Port = int.Parse(configuration["Port"]);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(From);
            foreach (var item in To)
            {
                if (item.Contains("@"))
                {
                    mail.To.Add(item);
                }
            }
            mail.Subject = Subject;
            StreamReader read = new StreamReader(file.OpenReadStream());
            mail.Body = read.ReadToEnd();
            mail.IsBodyHtml = true;
            ///-------------------------------------------------------------------------//
            SmtpClient smtpMail = new SmtpClient();
            smtpMail.EnableSsl = false;
            smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpMail.Host = Host;
            smtpMail.Port = Port;

            smtpMail.UseDefaultCredentials = false;
            smtpMail.Credentials = new NetworkCredential(From, Pass);
            ///-------------------------------------------------------------------------//
            smtpMail.Send(mail);
        }
    }
}
