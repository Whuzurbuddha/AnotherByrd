using System.IO;
using AnotherBird;
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
        var tempDirectory = $@"{ConstPaths.MailAccounts!}\{accountName}\Temp\";
        if (!Directory.Exists(tempDirectory)) CreateDirectory(tempDirectory);
        
        var newMessagePath = $@"{tempDirectory}{messageId}";
        if (!Directory.Exists(newMessagePath)) CreateDirectory(newMessagePath);
        
        var db = new LiteDatabaseAsync(newMessagePath);
        
        var newMessage = $@"{newMessagePath}\{message?.MessageSender}.db";
        
        if (File.Exists(newMessage)) return;
        try
        {
            var messageTable = db.GetCollection<MailItem>(message?.MessageSender);

            if (message != null) await messageTable.InsertAsync(message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}