using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Identitysample.Repositories
{
    public class MessageSender : IMessageSender
    {
        public Task SendEmailAsync(string email, string subject, string message, bool IsMessageHtml = false)
        {
            using (var client = new SmtpClient())
            {
                var credentials = new NetworkCredential()
                {
                    UserName = "rwp.aftersale",
                    Password = "ovss lxlz jkuv yqhn"
                };
                client.Credentials = credentials;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;

                using var emailMessage = new MailMessage()
                {
                    To = { new MailAddress(email) },
                    From =  new MailAddress("rwp.aftersale@gmail.com") ,
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = IsMessageHtml
                };
                client.Send(emailMessage);
            }
            return Task.CompletedTask;
        }
    }
}
