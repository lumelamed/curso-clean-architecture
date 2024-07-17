using CleanArchitecture.Application.Abstractions.Email;

namespace CleanArchitecture.Infrastructure.Email
{
    internal sealed class EmailService : IEmailService
    {
        public Task SendAsync(string recipient, string subject, string body)
        {
            // reemplazar con el envio de emails
            return Task.CompletedTask;
        }
    }
}
