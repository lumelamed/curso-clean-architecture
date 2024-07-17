namespace CleanArchitecture.Infrastructure.Email
{
    using CleanArchitecture.Application.Abstractions.Email;

    internal sealed class EmailService : IEmailService
    {
        public Task SendAsync(string recipient, string subject, string body)
        {
            // reemplazar con el envio de emails
            return Task.CompletedTask;
        }
    }
}
