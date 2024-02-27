using System.ComponentModel;
using System.Threading.Tasks;
using AnotherByrd.Datacontroller;

namespace AnotherBird.ViewModels.Pages;

public abstract class MailContentViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? _mailSender;
    private string? _smtp;
    private string? _encryptedPwd;
    private string? _recipient;
    private string? _regarding;
    private string? _mailText;
    private static string? _filePath;

    public string? MailSender
    {
        get => _mailSender;
        set
        {
            _mailSender = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MailSender)));
        }
    }
    public string? Smtp
    {
        get => _smtp;
        set
        {
            _smtp = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Smtp)));
        }
    }
    
    public string? EncryptedPwd
    {
        get => _encryptedPwd;
        set
        {
            _encryptedPwd = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EncryptedPwd)));
        }
    }
    
    public string? Recipient
    {
        get => _recipient;
        set
        {
            _recipient = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Recipient)));
        }
    }

    public string? Regarding
    {
        get => _regarding;
        set
        {
            _regarding = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Regarding)));
        }
    }

    public string? MailText
    {
        get => _mailText;
        set
        {
            _mailText = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MailText)));
        }
    }

    public string? FilePath
    {
        get => _filePath;
        set
        {
            _filePath = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilePath)));
        }
    }

    private bool _mailStatus;
    private const string? RecipientMissing = "Empfängeradresse fehlt";
    private const string? SendingSuccess = "Mail erfolgreich versendet";
    private const string? SendingFailed = "Versandt fehlgeschlagen";

    public void GetFilePath()
    {
        FilePath = LoadFile.OpenDirectory();
    }
    public async Task<string?> SendMail()
    {
        if (Recipient == string.Empty) return RecipientMissing;
        _mailStatus = true;  await EmailSender.SendingMail(MailSender, Smtp, EncryptedPwd, Recipient, Regarding, MailText, FilePath);
        return _mailStatus ? SendingSuccess : SendingFailed;
    }

    public void SetSelectedMailProvider(string? mailAddress, string? smtp, string? encryptedPwd)
    {
        MailSender = mailAddress;
        Smtp = smtp;
        EncryptedPwd = encryptedPwd;
    }
}