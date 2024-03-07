using System.ComponentModel;
using AnotherByrd.Datacontroller;

namespace AnotherByrd.ViewModels.Windows;

public class ContactViewmodel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

        private string? _firstname;
        private string? _lastname;
        private string? _address;
        private string? _number;
        private string? _city;
        private string? _postcode;
        private string? _mobilephone;
        private string? _email;

        public string? Firstname
        {
            get => _firstname;
            set{
                _firstname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Firstname)));
            }
        }
        public string? Lastname
        {
            get => _lastname;
            set
            {
                _lastname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Lastname)));
            }
        }
        public string? Address
        {
            get => _address;
            set
            {
                _address = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Address)));
            }
        }
        public string? Number
        {
            get => _number;
            set
            {
                _number = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Number)));
            }
        }
        public string? City
        {
            get => _city;
            set
            {
                _city = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(City)));
            }
        }
        public string? Postcode
        {
            get => _postcode;
            set
            {
                _postcode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Postcode)));
            }
        }
        public string? Mobilephone
        {
            get => _mobilephone;
            set
            {
                _mobilephone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Mobilephone)));
            }
        }
        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
            }
        }
        
        public void SentToDatabase()
        {
            ContactsDatabase.Insert(Firstname, Lastname, Address, Number, City, Postcode, Mobilephone, Email);
        }
}