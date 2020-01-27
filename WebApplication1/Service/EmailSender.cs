using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApplication1.Service
{
    public class EmailSender:IEmailSender
    {
        private string host;
        private int port;
        private bool enableSSL;
        private string userName;
        private string password;
        
        public EmailSender(string host, int port, bool enableSSL, string userName, string password)
        {
            this.host = host;
            this.port = port;
            this.enableSSL = enableSSL;
            this.userName = userName;
            this.password = password;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient(host, port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enableSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            return client.SendMailAsync(
                new MailMessage(userName, email, subject, message) { IsBodyHtml = true });
        }
    }
}
