using System;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Core.Services
{
    public interface IAddressBookService
    {
        Task<Guid> GetAddressBookIdAsync();
        Task<Guid> AddContactAsync(Contact contact);
        Task UpdateContactAsync(Contact contact);
        Task RemoveContactAsync(Guid id);
        Task<AddressBook> GetAddressBookAsync(int page = 0, int pageSize = 5);
        Task<AddressBook> GetAddressBookForContactAsync(Guid contactId);

    }

    public class AddressBookService : IAddressBookService
    {
        private readonly IAddressBookRepository _addressBookRepository;
        public AddressBookService(IAddressBookRepository addressBookRepository)
        {
            _addressBookRepository = addressBookRepository;
        }

        public async Task<Guid> GetAddressBookIdAsync() => (await _addressBookRepository.GetAddressBooksAsync()).First().Id;

        public async Task<AddressBook> GetAddressBookAsync(int page = 0, int pageSize = 5) => await _addressBookRepository.GetContactsAsync(page);

        public async Task<AddressBook> GetAddressBookForContactAsync(Guid contactId) => await _addressBookRepository.GetContactAsync(contactId);

        public async Task<Guid> AddContactAsync(Contact contact)
        {
            // First we will fetch address book along with the contact we are trying to add.
            // This is just in case the contact already exists.
            var addressBook = await _addressBookRepository.GetContactAsync(contact.Name, contact.Address);
            addressBook.RegisterNewContact(contact);
            await _addressBookRepository.SaveAsync(addressBook);
            return contact.Id;
        }

        public async Task RemoveContactAsync(Guid id)
        {
            var addressBook = await _addressBookRepository.GetContactAsync(id);
            var contact = addressBook.Contacts.SingleOrDefault(c => c.Id == id);
            if (contact == null)
                throw new ArgumentException($"Contact {id} not found.");
            addressBook.UnregisterContact(contact);
            await _addressBookRepository.SaveAsync(addressBook);
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            var addressBook = await _addressBookRepository.GetContactAsync(contact.Id);
            var contactToUpdate = addressBook.Contacts.SingleOrDefault(c => c.Id == contact.Id);
            contactToUpdate.UpdateContact(contact.Name, contact.Address, contact.DateOfBirth);
            await _addressBookRepository.SaveAsync(addressBook);
        }
    }
}