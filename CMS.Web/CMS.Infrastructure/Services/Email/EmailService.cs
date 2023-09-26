using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly string smtpHost = "smtp.gmail.com";
        private readonly int smtpPort = 587;
        private readonly string senderEmail = "ziad2001kamal@gmail.com";
        private readonly string senderPassword = "YOUR_SENDER_PASSWORD";  // Replace with your actual password

        public async Task Send(string to, string subject, string body)
        {
            try
            {
                // create message
                var message = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                };

                message.To.Add(new MailAddress(to));

                using (var emailClient = new SmtpClient(smtpHost, smtpPort))
                {
                    emailClient.EnableSsl = true;
                    emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    emailClient.UseDefaultCredentials = false;
                    emailClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

                    await emailClient.SendMailAsync(message);
                }
            }
            catch (Exception e)
            {
                // Handle exceptions appropriately (log, rethrow, etc.)
                Console.WriteLine("An error occurred while sending the email: " + e.Message);
                throw;  // Re-throw the exception to propagate it
            }
        }
    }
}