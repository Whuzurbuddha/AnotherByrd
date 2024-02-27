using System.IO;
using System.Text.Json;
using Microsoft.VisualBasic.FileIO;

namespace AnotherByrd.Datacontroller
{
    public class ReadMainAccountJson
    {
        public string? Passwd { get; set; }
        public string? UserName { get; set; }
        public string? Smtp { get; set; }
        public string? Imap { get; set; }
        
        public static string? FilePath = FindSavedData();

        public class UserContent
        {
            public string? User { get; init; }
            public string? Smtp { get; set; }
            public string? Imap { get; set; }
            public string? EncryptedPasswd { get; init; }
            public string? DecryptedPasswd { get; set; }
        }
        
        public string? GetUserPasswd()
        {
            try
            {
                if (FilePath != null)
                {
                    using var reader = new StreamReader(FilePath);
                    var jsonPasswd = reader.ReadToEnd();
                    var readJson = JsonSerializer.Deserialize<ReadMainAccountJson>(jsonPasswd);
                    var passwd = readJson?.Passwd;
                    return passwd;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Lesen der JSON-Datei: {ex.Message}");
                return null;
            }

            return null;
        }

        public static UserContent? GetUserContent()
        {
            try
            {
                if (FilePath != null)
                {
                    using var reader = new StreamReader(FilePath);
                    var jsonContent = reader.ReadToEnd();
                    var readJson = JsonSerializer.Deserialize<ReadMainAccountJson>(jsonContent);
                    var user = readJson?.UserName;
                    var encryptedPasswd = readJson?.Passwd;
                    var userContent = new UserContent
                    {
                        User = user,
                        EncryptedPasswd = encryptedPasswd
                    };
                
                    return userContent;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }

            return null;
        }

        private static string? FindSavedData()
        {
            var mainDirectory = $"{SpecialDirectories.MyDocuments}\\MailClient";
            string[] account = Directory.GetDirectories($@"{mainDirectory}\\mainaccount");
            var user = account[0];
            var filePath = $@"{user}\\Data\\SavedData.json";
            return filePath;
        }
    }
}