using System;

namespace AddressBook.Core
{
    public interface IAddressBookRepository
    {
        AddressBook GetContact(Guid id);
        AddressBook GetContact(string name, Address address);
        AddressBook GetContacts(int page = 0);
        void Save(AddressBook addressBook);
    }
}