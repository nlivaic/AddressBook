using System;
using System.Threading.Tasks;

namespace AddressBook.Core
{
    public interface IAddressBookRepository
    {
        AddressBook GetContact(Guid id);
        AddressBook GetContact(string name, Address address);
        AddressBook GetContacts(int page = 1);
        Task SaveAsync(AddressBook addressBook);
    }
}