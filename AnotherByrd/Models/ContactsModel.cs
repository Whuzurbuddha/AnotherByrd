using System.Collections.ObjectModel;
using System.ComponentModel;
using AnotherByrd.Datacontroller;
using static AnotherByrd.Datacontroller.ContactsDatabase;
namespace AnotherByrd.Models;

public class ContactsModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private ObservableCollection<Contact>? _contacts;
    private Contact? _selectedContact;

    public ObservableCollection<Contact>? Contacts
    {
        get => _contacts;
        set
        {
            _contacts = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Contacts)));
        }
    }

    public Contact? SelectedContact
    {
        get => _selectedContact;
        set
        {
            _selectedContact = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedContact)));
        }
    }
    public async void GetContacts()
    {
        Contacts = await ContactsDatabase.GetContacts();
    }

    public void SetSelectedContact(Contact selectedContact)
    {
        SelectedContact = selectedContact;
    }
    
    public void SentUpdatedData(Contact? contact)
    {
        UpdateContactData(contact);
    }
}