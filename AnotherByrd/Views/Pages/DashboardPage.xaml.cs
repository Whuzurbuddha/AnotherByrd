using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using AnotherBird;
using AnotherBird.ViewModels.Pages;
using AnotherBird.Views.Windows;
using AnotherByrd.Datacontroller;
using AnotherByrd.Models;
using AnotherByrd.ViewModels.Pages;
using Wpf.Ui.Controls;
using TreeViewItem = Wpf.Ui.Controls.TreeViewItem;

namespace AnotherByrd.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }
        private SendMailView? _newMailWindow;
        private GetMailViewModel? _getMailViewModel;

        //private AddressBook? _addressBook;
        private NewAccount? _newAccount;
        //private Calendar? _calendar;


        public DashboardPage(DashboardViewModel viewModel)
        {
            InitializeComponent();
            if (!CheckIfAccountExits()) return;
            _getMailViewModel = new GetMailViewModel();
            DataContext = _getMailViewModel;
            Loaded += GetContent;
        }

        private async void GetContent(object sender, RoutedEventArgs gc)
        {
            await LoadMailOverview();
            //await DownloadMails();
        }

        private void OpenNewMailWindow(object sender, RoutedEventArgs e)
        {
            /*_newMailWindow = new SendMailView();
            _newMailWindow.Show();*/
        }

        private void OpenAnswerMailWindow(object sender, RoutedEventArgs e)
        {
            /*_answerMailView = new SendMailView();
            _answerMailView.SetAnswerText();
            _answerMailView.Show();*/
        }

        private void OpenAddressBook(object sender, RoutedEventArgs e)
        {
            /*_addressBook = new AddressBook();
            _addressBook.Show();*/
        }

        private void CreateNewAccount(object sender, RoutedEventArgs e)
        {
            _newAccount = new NewAccount();
            _newAccount.Show();
        }

        private void OpenCalendar(object sender, RoutedEventArgs e)
        {
            /*_calendar = new Calendar();
            _calendar.Show();*/
        }

        private void SetMailSender(object sender, RoutedEventArgs e)
        {
            if (sender is not TreeViewItem { IsExpanded: true } provider) return;
            if (provider.DataContext is ReadMailCache.UserContent mailAccount)
            {
                (ReceivedMailOverview.DataContext as MailContentViewModel)?.SetSelectedMailProvider(mailAccount.MailAddress,
                    mailAccount.Smtp, mailAccount.EncryptedPwd);
            }
        }

        private void ChooseMailbox(object sender, RoutedEventArgs e)
        {
            if (sender is not DockPanel inbox) return;
            if (inbox.DataContext is ReadMailCache.UserContent userContent)
            {
                (DataContext as GetMailViewModel)?.SetMailBoxSelection(userContent.Mailbox);
            }
        }

        private async Task DownloadMails()
        {
            await (DataContext as GetMailViewModel)?.GetMailsFromServer()!;
        }

        private async Task LoadMailOverview()
        {
            await (ProviderOverview.DataContext as GetMailViewModel)?.GetUserAccounts()!;
        }

        public void RefreshProviderOverview()
        {
            var currentContent = ProviderOverview.DataContext;
            ProviderOverview.Items.Remove(currentContent);
            ProviderOverview.Items.Refresh();
        }

        private bool CheckIfAccountExits()
        {
            if (!File.Exists($@"{ConstPaths.MainDirectory!}\mailaccounts"))
            {
                Directory.CreateDirectory($@"{ConstPaths.MainDirectory!}\mailaccounts");
            }

            var accounts = Directory.GetDirectories($@"{ConstPaths.MainDirectory!}\mailaccounts");
            if (accounts.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void SetSender(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("SELECTED MAIL");
            if (sender is not DataGridRow { IsSelected: true } dataGridRow) return;
            if (dataGridRow.DataContext is ReadMailCache.MailItem mailItem)
            {
                (DataContext as GetMailViewModel)?.SetSelectedMailText(mailItem);
                Console.WriteLine(mailItem.MessageText);
                if (mailItem.HasAttachment == true)
                {
                    AttachmentList.Visibility = Visibility.Visible;
                    AttachmentList.Items.Refresh();
                }
                else
                {
                    AttachmentList.Visibility = Visibility.Collapsed;
                    AttachmentList.Items.Refresh();
                }
            }
        }

        private FileView? _fileView;
        private void SetFile(object sender, RoutedEventArgs e)
        {
            if (sender is not ComboBoxItem { IsSelected: true } comboBoxItem) return;
            if (comboBoxItem.DataContext is ReadMailCache.AttachmentListitem boxItem)
            {
                _fileView = new FileView();
                //_fileView.Show();
                if (boxItem.AttachmentFilePath == null) return;
                _fileView.OpenFile($"file:///{boxItem.AttachmentFilePath}");
            }
        }

        public void SetMailbox(ObservableCollection<ReadMailCache.MailItem>? mailBox)
        {
            (DataContext as GetMailViewModel)?.SetMailBoxSelection(mailBox);
            ReceivedMailOverview.Items.Refresh();
        }
    }
}
