using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace EduSiteHQ.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // مجرد محاكاة - لا يرسل أي شيء فعلياً
            Console.WriteLine($"[Mock Email Sent] To: {email}, Subject: {subject}");
            return Task.CompletedTask;
        }
    }
}
