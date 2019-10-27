using System;
using AddressBook.Core;

namespace AddressBook.Data
{
    public class AddressBookRepository : IAddressBookRepository
    {
        private readonly AddressBookDbContext _ctx;

        public AddressBookRepository(AddressBookDbContext ctx)
        {
            _ctx = ctx;
        }

        public Core.AddressBook GetContact(Guid id)
        {
            throw new NotImplementedException();
        }

        public Core.AddressBook GetContact(string name, Address address)
        {
            throw new NotImplementedException();
        }

        public Core.AddressBook GetContacts(int page = 0)
        {
            throw new NotImplementedException();
        }

        public void RemoveContact(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save(Core.AddressBook addressBook)
        {
            throw new NotImplementedException();
        }
    }
}
