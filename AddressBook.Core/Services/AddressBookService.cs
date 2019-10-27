using System;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Core.Services
{
    public interface IAddressBookService
    {
        Task<Guid> AddContactAsync(Contact contact);
        Task UpdateContactAsync(Contact contact);
        Task RemoveContactAsync(Guid id);
        AddressBook GetAddressBook(int page = 0, int pageSize = 5);
        AddressBook GetAddressBookForContact(Guid contactId);

    }

    public class AddressBookService : IAddressBookService
    {
        private readonly IAddressBookRepository _addressBookRepository;
        public AddressBookService(IAddressBookRepository addressBookRepository)
        {
            _addressBookRepository = addressBookRepository;
        }

        public AddressBook GetAddressBook(int page = 0, int pageSize = 5) => _addressBookRepository.GetContacts(page);

        public AddressBook GetAddressBookForContact(Guid contactId) => _addressBookRepository.GetContact(contactId);

        public async Task<Guid> AddContactAsync(Contact contact)
        {
            // First we will fetch address book along with the contact we are trying to add.
            // This is just in case the contact already exists.
            var addressBook = _addressBookRepository.GetContact(contact.Name, contact.Address);
            addressBook.RegisterNewContact(contact);
            await _addressBookRepository.SaveAsync(addressBook);
            return contact.Id;
        }

        public async Task RemoveContactAsync(Guid id)
        {
            var addressBook = _addressBookRepository.GetContact(id);
            var contact = addressBook.Contacts.SingleOrDefault(c => c.Id == id);
            if (contact == null)
                throw new ArgumentException($"Contact {id} not found.");
            addressBook.UnregisterContact(contact);
            await _addressBookRepository.SaveAsync(addressBook);
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            var addressBook = _addressBookRepository.GetContact(contact.Id);
            var contactToUpdate = addressBook.Contacts.SingleOrDefault(c => c.Id == contact.Id);
            contactToUpdate.UpdateContact(contact.Name, contact.Address, contact.DateOfBirth);
            await _addressBookRepository.SaveAsync(addressBook);
        }
    }
}