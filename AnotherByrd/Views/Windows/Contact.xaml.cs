using AnotherByrd.Datacontroller;
using AnotherByrd.Models;
using AnotherByrd.ViewModels.Windows;

namespace AnotherByrd.Views.Windows;

public partial class Contact : Window
{
    private ContactViewmodel? _contactViewmodel;
    private ContactsModel? _contactsModel;
    public Contact()
    {
        InitializeComponent();
        _contactViewmodel = new ContactViewmodel();
        DataContext = _contactViewmodel;
        Topmost = true;
    }

    public void LoadContact(ContactsDatabase.Contact selectedContact)
    {
        ContactOverview.ItemsSource = new[] { selectedContact };
        ContactOverview.Items.Refresh();
    }

    private void EditContact(object sender, RoutedEventArgs routedEventArgs)
    {
        _contactsModel = new ContactsModel();
        var contact = ContactOverview.Items.CurrentItem as ContactsDatabase.Contact;
        _contactsModel?.SentUpdatedData(contact);
        Close();
    }
    private void CloseWindow(object sender, RoutedEventArgs routedEventArgs)
    {
        Close();
    }
}