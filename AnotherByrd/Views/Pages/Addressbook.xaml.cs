using System.Windows.Controls;
using System.Windows.Input;
using AnotherByrd.Datacontroller;
using AnotherByrd.Models;
using AnotherByrd.Views.Windows;

namespace AnotherByrd.Views.Pages;

public partial class Addressbook
{
    public Addressbook()
    {
        var contactsModel = new ContactsModel();
        DataContext = contactsModel;
        InitializeComponent();
        Loaded += LoadContacts;
    }

    private void LoadContacts(object sender, RoutedEventArgs lc)
    {
        (DataContext as ContactsModel)?.GetContacts();
    }

    private NewContact? _newContact;
    private void OpenNewContact(object sender, RoutedEventArgs routedEventArgs)
    {
        _newContact = new NewContact();
        _newContact.Show();
    }

    private Contact? _contactWindow;
    private void OpenContact(object sender, MouseButtonEventArgs mouseButtonEventArgs)
    {
        _contactWindow = new Contact();
        _contactWindow.Show();
        if (sender is not StackPanel panel) return;
        if (panel.DataContext is ContactsDatabase.Contact contact)
        {
            (DataContext as ContactsModel)?.SetSelectedContact(contact);
            _contactWindow.LoadContact(contact);
        }
    }

    private void SelectContact(object sender, MouseButtonEventArgs mouseButtonEventArgs)
    {
        ContactsOverview.SelectedItem = ((FrameworkElement)sender).DataContext;
    }
    private void OpenDialog(object sender, MouseButtonEventArgs od)
    {
        if (od.RightButton == MouseButtonState.Pressed)
        {
            ContextMenu? contextMenu = ((StackPanel)sender).ContextMenu;
            contextMenu!.IsOpen = true;

            od.Handled = true;
        }
        
    }

    private void DeleteContact(object sender, RoutedEventArgs routedEventArgs)
    {
        if (sender is not MenuItem menuItem) return;
        var selectedContact = ContactsOverview.SelectedItem as ContactsDatabase.Contact;
        Console.WriteLine(selectedContact.Firstname);
    }
    private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (_contactWindow?.Visibility == Visibility.Visible && !IsMouseOverWindow(_contactWindow, e))
        {
            _contactWindow.Close();
        }
    }
    private bool IsMouseOverWindow(FrameworkElement window, MouseButtonEventArgs e)
    {
        var position = e.GetPosition(window);
        return position.X >= 0 && position.X <= window.ActualWidth && position.Y >= 0 && position.Y <= window.ActualHeight;
    }
}