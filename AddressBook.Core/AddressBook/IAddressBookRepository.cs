using System;
using System.Threading.Tasks;

namespace AddressBook.Core
{
    public interface IAddressBookRepository
    {
        Task<AddressBook> GetContactAsync(Guid id);
        Task<AddressBook> GetContactAsync(string name, Address address);
        Task<AddressBook> GetContactsAsync(int page = 1);
        Task SaveAsync(AddressBook addressBook);
    }
}