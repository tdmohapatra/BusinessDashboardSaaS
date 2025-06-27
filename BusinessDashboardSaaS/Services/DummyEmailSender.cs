using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Threading.Tasks;

namespace BusinessDashboardSaaS.Services
{
    public class DummyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"📧 Dummy Email Sent To: {email}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {htmlMessage}");
            Console.WriteLine("--------------------------------------------------");
            return Task.CompletedTask;
        }
    }
}
