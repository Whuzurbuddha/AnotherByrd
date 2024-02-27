using System.ComponentModel;

namespace AnotherBird.ViewModels.Windows;

public class InputViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? _userName;
    private string? _userMail;
    private string? _password;
    private string? _smtp;
    private string? _imap;

    public string? UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserName)));
        }
    }
    public string? UserMail
    {
        get => _userMail;
        set
        {
            _userMail = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserMail)));
        }
    }

    public string? Password
    {
        get => _password;
        set
        {
            _password = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
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

    public string? Imap
    {
        get => _imap;
        set
        {
            _imap = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Imap)));
        }
    }
}