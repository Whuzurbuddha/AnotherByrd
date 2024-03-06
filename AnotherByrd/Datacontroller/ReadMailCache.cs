using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AnotherBird;
using LiteDB.Async;
using static AnotherByrd.Datacontroller.EmailController;

namespace AnotherByrd.Datacontroller;

public abstract class ReadMailCache
{
    private static ObservableCollection<UserContent>? _userAccounts;
    private static ObservableCollection<MailItem>? _mailBox;
    
    
    public static async Task<ObservableCollection<UserContent>?> GetLoadedMails()
    {
        Console.WriteLine("START LOADING LOCAL MAILS");
        _userAccounts = new ObservableCollection<UserContent>();
        var mainDirectory = ConstPaths.MainDirectory;
        var mailAccounts = Directory.GetDirectories($@"{mainDirectory}\mailaccounts");

        if (mailAccounts.Length == 0) return null;

        foreach (var account in mailAccounts)
        {
            var lastDot = account.LastIndexOf(@$"\", StringComparison.Ordinal);
            var accountName = lastDot < 0 ? "" : account.Substring((lastDot+1)!).ToLower();
            _mailBox = new ObservableCollection<MailItem>(); 
            
            using var addressReader = new StreamReader(@$"{ConstPaths.MailAccounts}\{accountName}\Account.json");
            var addressContent = await addressReader.ReadToEndAsync();
            var readJson = JsonSerializer.Deserialize<ReadMailAccount>(addressContent);
            var mailAddress = readJson?.UserMail;
            var smtp = readJson?.Smtp;
            var imap = readJson?.Imap;
            var encryptedPwd = readJson?.Passwd;
            
            var database = @$"{ConstPaths.MailAccounts}\{accountName}\{accountName}.db";
            
            var db = new LiteDatabaseAsync(database);

            var provider = db.GetCollection<MailItem>("Messages");

            var mails = await provider.Query()
                .Select(x => new MailItem
                {
                    Date = x.Date,
                    MessageId = x.MessageId,
                    MessageSubject = x.MessageSubject,
                    MessageText = x.MessageText,
                    MessageSender = x.MessageSender,
                })
                .ToArrayAsync();

            var mailList = new ObservableCollection<MailItem>(mails);
            
            var userContent = new UserContent
            {
                AccountName = accountName,
                MailAddress = mailAddress,
                Smtp = smtp,
                EncryptedPwd = encryptedPwd,
                Mailbox = mailList
            };
            _userAccounts.Add(userContent);
            db.Dispose();
        }
        return _userAccounts;
    }
    public class UserContent
    {
        public string? AccountName { get; init; }
        public string? MailAddress { get; init; }
        public string? Smtp { get; init;}
        public string? EncryptedPwd{ get; init;}
        public ObservableCollection<MailItem>? Mailbox { get; set; }
    }
    public class MailItem
    {
        public string? Date { get; set; }
        public string? MessageId { get; init; }
        public string? MessageSubject { get; init; }
        public string? MessageSender { get; init; }
        public string? MessageText { get; init; }
        public ObservableCollection<AttachmentListitem>? AttachmentList { get; set; }
        public bool? HasAttachment { get; set; }
        public string? AttachmentPath { get; set; }
    }

    public abstract class AttachmentListitem
    {
        public string? AttachmentFileName { get; set; }
        public string? AttachmentFileType { get; set; }
        public string? AttachmentFilePath { get; init; }
        public bool? IsLoaded { get; set; }
    }
}