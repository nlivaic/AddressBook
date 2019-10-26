using System;
using System.Linq;

namespace AddressBook.Core.Services
{
    public interface IAddressBookService
    {
        void AddContact(Contact contact);
        void UpdateContact(Contact contact);
        void RemoveContact(Guid id);
        AddressBook GetAddressBook(int page = 0, int pageSize = 5);
    }

    public class AddressBookService : IAddressBookService
    {
        private readonly IAddressBookRepository _addressBookRepository;
        public AddressBookService(IAddressBookRepository addressBookRepository)
        {
            _addressBookRepository = addressBookRepository;
        }

        public void AddContact(Contact contact)
        {
            // First we will fetch address book along with the contact we are trying to add.
            // This is just in case the contact already exists.
            var addressBook = _addressBookRepository.GetContact(contact.Name, contact.Address);
            addressBook.RegisterNewContact(contact);
            _addressBookRepository.Save(addressBook);
        }

        public AddressBook GetAddressBook(int page = 0, int pageSize = 5) => _addressBookRepository.GetContacts(page);

        public void RemoveContact(Guid id)
        {
            var addressBook = _addressBookRepository.GetContact(id);
            var contact = addressBook.Contacts.SingleOrDefault(c => c.Id == id);
            addressBook.UnregisterContact(contact);
            _addressBookRepository.Save(addressBook);
        }

        public void UpdateContact(Contact contact)
        {
            var addressBook = _addressBookRepository.GetContact(contact.Id);
            var contactToUpdate = addressBook.Contacts.SingleOrDefault(c => c.Id == contact.Id);
            contactToUpdate.UpdateContact(contact.Name, contact.Address, contact.DateOfBirth);
            _addressBookRepository.Save(addressBook);
        }
    }
}