public interface IEmailSender
{
    Task SendEmailAsync(string? recipientEmail, string subject, string message, int? id);
}