using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using AnotherByrd.Datacontroller;

namespace AnotherBird.Models;

public class GetMailViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? _selectedMailText;
    private string? _selectedMailSender;
    private ObservableCollection<ReadMailCache.AttachmentListitem>? _selectedMailAttachmentList;
    private ObservableCollection<ReadMailCache.MailItem>? _selectedMailbox;
    private ObservableCollection<ReadMailCache.UserContent>? _userAccounts;

    public string? SelectedMailText
    {
        get => _selectedMailText;
        set
        {
            _selectedMailText = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMailText)));
        }
    }

    public string? SelectedMailSender
    {
        get => _selectedMailSender;
        set
        {
            _selectedMailSender = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMailSender)));
        }
    }

    public ObservableCollection<ReadMailCache.AttachmentListitem>? SelectedMailAttachmentList
    {
        get => _selectedMailAttachmentList;
        set
        {
            _selectedMailAttachmentList = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMailAttachmentList)));
        }
    }

    public ObservableCollection<ReadMailCache.MailItem>? SelectedMailBox
    {
        get => _selectedMailbox;
        set
        {
            _selectedMailbox = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMailBox)));
        }
    }

    public ObservableCollection<ReadMailCache.UserContent>? UserAccounts
    {
        get => _userAccounts;
        set
        {
            _userAccounts = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserAccounts)));
            Console.WriteLine($"RECEIVED MAIL CONTENT FOR: {value}");
        }
    }

    public async Task GetMailsFromServer()
    {
        await ReadMailAccountJson.GetUserContent();
    }

    public async Task GetUserAccounts()
    {
        _userAccounts = await ReadMailCache.GetLoadedMails();
        if (_userAccounts == null) return;
        UserAccounts = _userAccounts;
    }

    public void SetSelectedMailText(string? mailText, string? mailSender,
        ObservableCollection<ReadMailCache.AttachmentListitem>? attachmentList)
    {
        SelectedMailText = mailText;
        SelectedMailSender = mailSender;
        SelectedMailAttachmentList = attachmentList;
    }

    public void SetMailBoxSelection(ObservableCollection<ReadMailCache.MailItem>? mailBox)
    {
        if (mailBox == null) return;
        SelectedMailBox = mailBox;
    }
}