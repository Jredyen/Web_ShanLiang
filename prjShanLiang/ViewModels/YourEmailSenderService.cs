namespace prjShanLiang.ViewModels
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;

    public class YourEmailSenderService : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public YourEmailSenderService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task SendEmailAsync(string? email, string subject, string message, int? id)
        {
            string senderEmail = _configuration["EmailSender:Email"]; // 发件人邮箱
            string senderPassword = _configuration["EmailSender:Password"]; // 发件人邮箱密码

            // 设置发送邮件的配置
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com", // Gmail SMTP服务器
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword)
            };

            // 创建邮件对象
            var mailMessage = new MailMessage(senderEmail, email)
            {
                Subject = subject,
                IsBodyHtml = true
            };

            // 获取当前请求的主机名和端口号
            var request = _httpContextAccessor.HttpContext.Request;
            string host = request.Host.Host;
            int port = (int)request.Host.Port;

            // 构建邮件正文，包括验证链接
            string verificationLink = $"https://{host}:{port}/StoreAdmin/CompleteVerification/?id={id}"; // 根据传入的 id 构建验证链接
            string emailBody = $"<p>{message}</p>";
            emailBody += $"<p>请点击以下链接进行验证：</p>";
            emailBody += $"<p><a href=\"{verificationLink}\">驗證連結</a></p>";

            mailMessage.Body = emailBody;

            try
            {
                // 发送邮件
                smtpClient.Send(mailMessage);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // 处理发送邮件时的异常
                // 可以添加日志记录或其他错误处理逻辑
                throw new ApplicationException("邮件发送失败: " + ex.Message);
            }
        }
    }
}