using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using AnotherBird;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using static AnotherByrd.Datacontroller.AttachmentCache;

namespace AnotherByrd.Datacontroller
{
    public class EmailController
    {
        private static bool? ValidAttachment(MimeEntity attachment)
        {
            var fileName = attachment.ContentType.Name;
            var lastDot = fileName?.LastIndexOf('.');
            var fileType = lastDot < 0 ? "" : fileName?.Substring((int)(lastDot+1)!).ToLower();
            if (!string.IsNullOrEmpty(fileName))
            {
                string[] allowedTypes =
                {
                    "png", "jpg", "jpeg", "bmp", "svg", "tif", "pdf", "word", "doc", "docx", "xlsx",
                    "xlsm", "xlsx", "xls", "txt", "odt", "md", "mp3", "mid", "wav", "wave", "ogg", "flac", "avi",
                    "flv",
                    "mov", "mp4",
                };
                if (allowedTypes.Contains(fileType))
                {
                    return true;
                }
            }
            return false;
        }
        
        private ObservableCollection<AttachmentListitem>? _attachmentList; 
        public async Task ReceivingMailAsync(string  accountName, string? imap, string? userMail, string? password)
        {
            using var client = new ImapClient();

            try
            {
                await client.ConnectAsync(imap, 993, true);
                await client.AuthenticateAsync(userMail, password);
                var inbox = client.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadOnly);
                for (var i = 0; i < inbox.Count; i++)               //single message content =>
                {
                    var messageBody = await inbox.GetMessageAsync(i);
                    var messageId = messageBody.MessageId;

                    if (messageId == null) return;
                   
                    var modifiedMessageId = messageId switch
                    {
                        _ when messageId.Contains($"$") => messageId.Replace("$", ""),
                        _ when messageId.Contains($" ") => messageId.Replace(" ", ""),
                        _ => messageId
                    };
                    if (!string.IsNullOrEmpty(modifiedMessageId))
                    {
                        messageId = modifiedMessageId;
                    }
                    
                    if (!Directory.Exists($@"{ConstPaths.MailAccounts!}\{accountName}\Temp\{messageId}"))
                    { 
                        var dateTime = messageBody.Date;
                        var date = dateTime.ToString().Split(" ")[0];
                        var convertedMail = messageBody.From.ToString();
                        if (convertedMail!.Contains('<'))
                        {
                            convertedMail = messageBody.From?.ToString()!.Split("<")[1].Split(">")[0];
                        }
                        var message = new MailItem
                        {
                            Date = date,
                            MessageId = messageId,
                            MessageSubject = messageBody.Subject,
                            MessageSender = convertedMail,
                            MessageText = messageBody.TextBody,
                        };
                        _attachmentList = new ObservableCollection<AttachmentListitem>();
                            
                        var attachments = messageBody.Attachments.ToArray();
                        if (attachments.Length != 0)
                        {
                            var newAttachmentList = new List<MimeEntity>();
                            var nonevalidAttachmentsList = new List<MimeEntity>();
                                
                            foreach (var attachment in attachments)
                            {
                                var validAttachment = ValidAttachment(attachment);
                                if (validAttachment == true)
                                {
                                    newAttachmentList.Add(attachment);
                                }
                                else
                                {
                                    nonevalidAttachmentsList.Add(attachment);
                                }
                            }
                            message.HasAttachment = true;

                            if (nonevalidAttachmentsList.Count > 0)
                            {
                                foreach (var nonevalid in nonevalidAttachmentsList)
                                {
                                    if (nonevalid.ContentType.Name != "smime.p7s")
                                    {
                                        var attachmentPathListitem = new AttachmentListitem
                                        {
                                            IsLoaded = false
                                        };
                                        
                                        if (!string.IsNullOrEmpty(nonevalid.ContentType.Name))
                                        {
                                            attachmentPathListitem.AttachmentFileName =
                                                nonevalid.ContentType.Name.Replace(" ", "");
                                        }
                                        else
                                        {
                                            attachmentPathListitem.AttachmentFileName = "unknown filename";
                                        }
                                        _attachmentList.Add(attachmentPathListitem);
                                    }
                                }
                            }
                                
                            if (newAttachmentList.Count > 0)
                            {
                                var subDirectory = await NewAttachmentCache(accountName, messageId, newAttachmentList, messageBody.BodyParts)!;
                                message.AttachmentPath = subDirectory;

                                foreach (var cleanedAttachment in newAttachmentList)
                                {
                                    string fileName;
                                    if (!string.IsNullOrEmpty(cleanedAttachment.ContentType.Name))
                                    {
                                        fileName = cleanedAttachment.ContentType.Name;
                                    }
                                    else
                                    {
                                        fileName = "unknown filename";
                                    }
                                    var attachmentPathListitem = new AttachmentListitem
                                    {
                                        AttachmentFileName = fileName,
                                        AttachmentFileType = cleanedAttachment.ContentType.MediaSubtype,
                                        AttachmentFilePath = $"{subDirectory}{cleanedAttachment.ContentType.Name.Replace(" ", "")}",
                                        IsLoaded = true
                                    };
                                    _attachmentList.Add(attachmentPathListitem);
                                }
                            }
                            message.AttachmentList = _attachmentList;
                        }
                        MailCache.WriteMailCache(message, accountName);
                    };
                
                }
                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        
        public class MailItem
        {
            public string? Date { get; init; }
            public string? MessageId { get; init; }
            public string? MessageSubject { get; init; }
            public string? MessageSender { get; init; }
            public string? MessageText { get; init; }
            public ObservableCollection<AttachmentListitem>? AttachmentList { get; set; }
            public bool? HasAttachment { get; set; }
            public string? AttachmentPath { get; set; }
        }

        public class AttachmentListitem
        {
            public string? AttachmentFileName { get; set; }
            public string? AttachmentFileType { get; set; }
            public string? AttachmentFilePath { get; init; }
            public bool? IsLoaded { get; set; }
        }
    }
}