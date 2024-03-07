using AnotherByrd.ViewModels.Windows;

namespace AnotherByrd.Views.Windows;

public partial class NewContact : Window
{
    private readonly ContactViewmodel? _newContactViewmodel;
    public NewContact()
    {
        _newContactViewmodel = new ContactViewmodel();
        DataContext = _newContactViewmodel;
        InitializeComponent();
    }

    private void SaveNew(object sender, RoutedEventArgs routedEventArgs)
    {
        (DataContext as ContactViewmodel)?.SentToDatabase();
        Close();
    }

    private void CloseWindow(object sender, RoutedEventArgs cw)
    {
        Close();
    }
}