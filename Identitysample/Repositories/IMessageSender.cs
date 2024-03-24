using System.Threading.Tasks;

namespace Identitysample.Repositories
{
    public interface IMessageSender
    {
        public Task SendEmailAsync(string email, string subject, string message, bool IsMessageHtml = false);
    }
}
