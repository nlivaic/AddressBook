using System;
using System.Collections.Generic;
using System.Linq;
using AddressBook.SharedKernel;

namespace AddressBook.Core
{
    public class AddressBook : BaseEntity<Guid, AddressBook>
    {
        private List<Contact> _contacts { get; set; }

        public IEnumerable<Contact> Contacts => _contacts.AsEnumerable();

        private AddressBook() : base()
        {
            _contacts = new List<Contact>();
        }

        public AddressBook(IEnumerable<Contact> contacts) : this(Guid.NewGuid(), contacts) { }

        public AddressBook(Guid id, IEnumerable<Contact> contacts) : base(id)
        {
            if (contacts == null) throw new ArgumentException("Contacts not set.");
            _contacts = contacts.ToList();
        }

        public void RegisterNewContact(Contact newContact)
        {
            if (_contacts.Any(c => c == newContact))
                throw new ArgumentException($"Contact {newContact.Name} at {newContact.Address} cannot be registered twice.");
            _contacts.Add(newContact);
            newContact.Tracking = TrackingState.Added;
        }

        public void UnregisterContact(Contact contact)
        {
            contact.Tracking = TrackingState.Deleted;
        }
    }
}