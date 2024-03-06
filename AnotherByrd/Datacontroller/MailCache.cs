using System.IO;
using AnotherBird;
using LiteDB;
using LiteDB.Async;
using static AnotherByrd.Datacontroller.EmailController;

namespace AnotherByrd.Datacontroller;

public static class MailCache
{
    private static System.IO.DirectoryInfo? CreateDirectory (string path)
    {
        Directory.CreateDirectory(path);
        return null;
    }
    public static async void WriteMailCache(MailItem? message, string? accountName)
    {
        Console.WriteLine("START WRITING MAIL JSON");
        
        var messageId = message?.MessageId;
        var tempDirectory = $@"{ConstPaths.MailAccounts!}\{accountName}\";
        if (!Directory.Exists(tempDirectory)) CreateDirectory(tempDirectory);
       
        var messages = $@"{tempDirectory}\{accountName}.db";
        
        var db = new LiteDatabaseAsync(messages);

        var newMessage = new Message()
        {
            Date = message?.Date,
            MessageSubject = message?.MessageSubject,
            MessageSender = message?.MessageSender,
            MessageText = message?.MessageText,
        };
        
        var content = db.GetCollection<Message>("Messages");
        var existingMail = await content.FindOneAsync(x => x.MessageId == messageId);
        
        var attachmentTable = db.GetCollection<AttachmentListitem>("Attachments");
        
        if (message?.AttachmentList != null)
        {
            newMessage.HasAttachment = true;
            foreach (var attachment in message?.AttachmentList!)
            {
                var newAttachment = new AttachmentListitem
                {
                    AttachmentFileName = attachment.AttachmentFileName,
                    AttachmentFileType = attachment.AttachmentFilePath,
                    AttachmentFilePath = attachment.AttachmentFilePath
                };
                newMessage.MessageAttachmentKey?.Add(newAttachment.Id);
                await attachmentTable.InsertAsync(newAttachment);
            }
        }
        else
        {
            newMessage.HasAttachment = false;
        }
        if (existingMail != null && string.IsNullOrEmpty(existingMail.ToString()))
        {
            await content.InsertAsync(newMessage);
        }
    }
    private class Message
    {
        public string? Date { get; init; }
        public string? MessageId { get; init; }
        public string? MessageSubject { get; init; }
        public string? MessageSender { get; init; }
        public string? MessageText { get; init; }
        public bool? HasAttachment { get; set; }
        public List<int>? MessageAttachmentKey { get; set; }
    }

    private class AttachmentListitem
    {
        public int Id { get; set; }
        public string? AttachmentFileName { get; set; }
        public string? AttachmentFileType { get; set; }
        public string? AttachmentFilePath { get; init; }
        public bool? IsLoaded { get; set; }
    }
}