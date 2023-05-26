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
            string senderEmail = _configuration["EmailSender:Email"]; // 發件人郵箱
            string senderPassword = _configuration["EmailSender:Password"]; // 發件人郵箱密碼

            // 設置發送郵箱的配置
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com", // Gmail SMTP服務器
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword)
            };

            // 創建郵件對象
            var mailMessage = new MailMessage(senderEmail, email)
            {
                Subject = subject,
                IsBodyHtml = true
            };

            //獲取當前請求的主機名稱和端口號
            var request = _httpContextAccessor.HttpContext.Request;
            string host = request.Host.Host;
            int port = (int)request.Host.Port;

            //構建郵件正文，包括驗證鏈接
            string verificationLink = $"https://{host}:{port}/StoreAdmin/CompleteVerification/?id={id}"; //根據傳入的id構建驗證鏈接
            string emailBody = $"<p>{message}</p>";
            emailBody += $"<p>請點擊以下鏈接進行驗證：</p>";
            emailBody += $"<p><a href=\"{verificationLink}\">驗證鏈結</a></p>";

            mailMessage.Body = emailBody;

            try
            {
                // 發送郵件
                smtpClient.Send(mailMessage);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // 處理發送郵件時的異常
                // 可以添加日誌紀錄或其他錯誤處理邏輯
                throw new ApplicationException("郵件發送失敗: " + ex.Message);
            }
        }
    }
}