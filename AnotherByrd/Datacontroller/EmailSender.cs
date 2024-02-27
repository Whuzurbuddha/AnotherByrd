using System.Net;
using System.Net.Mail;

namespace AnotherByrd.Datacontroller;

public static class EmailSender
{
    public static Task<bool> SendingMail(string?  mailSender, string? smtp, string? encryptedPwd, string? recipient, string? regarding, string? mailContent, string? filePath)
    {
        var fileType = new List<string?>()
        {
            "pdf",
            "doc",
            "jpeg",
            "png"
        };
        var lastDot = filePath?.LastIndexOf('.');
        var fileEnding = lastDot < 0 ? "" : filePath?.Substring((int)(lastDot+1)!).ToLower();
        if (!fileType.Contains(fileEnding))
        {
            MessageBox.Show("Ungültiges Dateiformat!\r\n Erlaubt sind PDF, DOC, PNG, JPEG");
            return Task.FromResult(false);
        }
        var password = ContentManager.DecryptPasswd(encryptedPwd);

        if (mailSender == null || recipient == null)
            return Task.FromResult(true);

        var message = new MailMessage(mailSender, recipient, regarding, mailContent);

        var smtpClient = new SmtpClient(smtp)
        {
            Port = 587,
            Credentials = new NetworkCredential(mailSender, password),
            EnableSsl = true
        };

        if (!string.IsNullOrEmpty(filePath))
        {
            var attachment = new Attachment(filePath);
            message.Attachments.Add(attachment);
        }

        smtpClient.Send(message);
        message.Dispose();
        return Task.FromResult(true);
    }
}