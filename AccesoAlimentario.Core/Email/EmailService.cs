using AccesoAlimentario.Core.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace AccesoAlimentario.Core.Email
{

    public class EmailService
    {
        private readonly SmtpConfiguration _smtpConfig;

        public EmailService()
        {
            _smtpConfig = AppSettings.Instance.SmtpConfig;
        }

        public async Task<bool> SendAsync(string from, string to, string subject, string body, IEnumerable<MailAttachment>? attachments = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from, _smtpConfig.Username));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;
            var builder = new BodyBuilder { HtmlBody = body };

            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    builder.Attachments.Add(attachment.FileName, new MemoryStream(attachment.Content));
                }
            }

            message.Body = builder.ToMessageBody();

            try
            {
                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpConfig.Host, _smtpConfig.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpConfig.Username, _smtpConfig.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return true;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Failed to send email", e);
            }
        }
    }

    public class MailAttachment
    {
        public string FileName { get; set; } = null!;
        public byte[] Content { get; set; } = null!;
    }
}
