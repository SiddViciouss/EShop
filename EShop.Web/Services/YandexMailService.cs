using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EShop.Web.Services
{
    public class YandexMailService: IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            SmtpClient client = new SmtpClient("smtp.yandex.ru")
            {
                EnableSsl = true,
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("admin@42studio.org", "Iamadmin#")
            };

            MailMessage mailMessage = new MailMessage("admin@42studio.org", email)
            {
                Body = message,
                Subject = subject
            };
            await client.SendMailAsync(mailMessage);
        }
    }
}
