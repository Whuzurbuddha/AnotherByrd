using System.ComponentModel;
using AnotherByrd.Datacontroller;

namespace AnotherBird.Views.Windows
{
    public partial class NewAccount :  INotifyPropertyChanged
    {
        public NewAccount() 
        {
            InitializeComponent();
            DataContext = this;
        }
        private async void SaveNewAccount(object sender, RoutedEventArgs e)
        {
            var saveAccountResult = await SaveAccountData.SaveMailAccount(AccountName, UserMail, Password, Smtp, Imap);
            if (!saveAccountResult) return;
            Close();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private string? _accountName;
        private string? _userMail;
        private string? _password;
        private string? _smtp;
        private string? _imap;
        public string? AccountName
        {
            get => _accountName;
            set
            {
                _accountName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AccountName)));
            }
        }
        public string? UserMail{
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

        private void CloseWindow(object sender, RoutedEventArgs routedEventArgs)
        {
            Close();
        }
    }
}