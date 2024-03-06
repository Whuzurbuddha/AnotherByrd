using System.Collections.ObjectModel;
using System.ComponentModel;
using AnotherByrd.Datacontroller;

namespace AnotherByrd.Models;

public class GetMailViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private ReadMailCache.MailItem? _selectedMail;
    private ObservableCollection<ReadMailCache.MailItem>? _selectedMailbox;
    private ObservableCollection<ReadMailCache.UserContent>? _userAccounts;

    public ReadMailCache.MailItem? SelectedMail
    {
        get => _selectedMail;
        set
        {
            _selectedMail = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMail)));
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
        await ReadMailAccount.GetUserContent();
    }

    public async Task GetUserAccounts()
    {
        _userAccounts = await ReadMailCache.GetLoadedMails();
        if (_userAccounts == null) return;
        UserAccounts = _userAccounts;
    }

    public void SetSelectedMailText(ReadMailCache.MailItem mailItem)
    {
        SelectedMail = mailItem;
    }

    public void SetMailBoxSelection(ObservableCollection<ReadMailCache.MailItem>? mailBox)
    {
        if (mailBox == null) return;
        SelectedMailBox = mailBox;
    }
}