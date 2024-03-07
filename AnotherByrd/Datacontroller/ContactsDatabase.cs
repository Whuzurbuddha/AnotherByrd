using System.Collections.ObjectModel;
using AnotherBird;
using LiteDB;
using LiteDB.Async;

namespace AnotherByrd.Datacontroller;

public static class ContactsDatabase
{
    public static async void Insert(string? firstname,string? lastname,string? address,string? number,string? city,string? postcode,string? mobilephone,string? email)
    {
        var db = new LiteDatabaseAsync($@"{ConstPaths.MainDirectory}\contacts.db");

        var contacts = db.GetCollection<Contact>("contacts");

        var lastId = await contacts.Query()
            .Select(x => x.Id)
            .ToListAsync();

        var newId = 1;
        
        if (lastId.Count != 0)
        {
            newId = (int)(lastId[^1] + 1)!;
        }
        
        var newContact = new Contact
        {
            Id = newId,
            Firstname = firstname,
            Lastname = lastname,
            Address = address,
            Number = number,
            City = city,
            Postcode = postcode,
            Mobilephone = mobilephone,
            Email = email
        };

        try
        {
            await contacts.InsertAsync(newContact);
            db.Dispose();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static void UpdateContactData(Contact? selectedContact)
    {
        var db = new LiteDatabaseAsync($@"{ConstPaths.MainDirectory}\contacts.db");

        var contacts = db.GetCollection<Contact>("contacts");

        var contact = contacts.FindOneAsync(x => x.Id == selectedContact.Id).Result;

        var editedContact = new Contact
        {
            Id = contact.Id,
            Firstname = selectedContact.Firstname,
            Lastname = selectedContact.Lastname,
            Address = selectedContact.Address,
            Number = selectedContact.Number,
            City = selectedContact.City,
            Postcode = selectedContact.Postcode,
            Mobilephone = selectedContact.Mobilephone,
            Email = selectedContact.Email
        };
        
        try
        {
            contacts.UpdateAsync(editedContact);
            db.Dispose();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<ObservableCollection<Contact>> GetContacts()
    {
        var db = new LiteDatabaseAsync($@"{ConstPaths.MainDirectory}\contacts.db");

        var contacts = db.GetCollection<Contact>("contacts");

        var contactOverview = await contacts.Query()
            .Select(x => new Contact
            {
                Id = x.Id,
                Firstname = x.Firstname,
                Lastname = x.Lastname,
                Address = x.Address,
                Number = x.Number,
                City = x.City,
                Postcode = x.Postcode,
                Mobilephone = x.Mobilephone,
                Email = x.Email
            })
            .ToArrayAsync();

        var contactList = new ObservableCollection<Contact>(contactOverview);
        db.Dispose();
        return contactList;
    }

    public class Contact
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Address { get; set; }
        public string? Number { get; set; }
        public string? City { get; set; }
        public string? Postcode { get; set; }
        public string? Mobilephone { get; set; }
        public string? Email { get; set; }
    }
}