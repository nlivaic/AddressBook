using System;
using System.Linq;
using AddressBook.Core;
using AddressBook.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Data
{
    public class AddressBookRepository : IAddressBookRepository
    {
        private readonly AddressBookDbContext _ctx;
        private const int pageSize = 3;

        public AddressBookRepository(AddressBookDbContext ctx)
        {
            _ctx = ctx;
        }

        public Core.AddressBook GetContact(Guid contactId)
        {
            return (from addressBook in _ctx.AddressBooks
                    join contact in _ctx.Contacts on addressBook.Id equals contact.AddressBookId
                    where contact.Id == contactId
                    select addressBook).Include(a => a.Contacts).ThenInclude(c => c.TelephoneNumbers)
                    .AsNoTracking()
                    .Single();
        }

        public Core.AddressBook GetContact(string name, Address address)
        {
            return (from addressBook in _ctx.AddressBooks
                    join contact in _ctx.Contacts on addressBook.Id equals contact.AddressBookId
                    where contact.Name == name && contact.Address == address
                    select addressBook).Include(a => a.Contacts).ThenInclude(c => c.TelephoneNumbers)
                    .AsNoTracking()
                    .Single();
        }

        public Core.AddressBook GetContacts(int page = 1)
        {
            return _ctx.AddressBooks
                .Include(a => a.Contacts
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize))
                .ThenInclude(c => c.TelephoneNumbers)
                .AsNoTracking()
                .Single();
        }

        public void Save(Core.AddressBook addressBook)
        {
            foreach (var contact in addressBook.Contacts)
            {
                switch (contact.Tracking)
                {
                    case TrackingState.Added:
                        _ctx.Contacts.Add(contact);
                        break;
                    case TrackingState.Modified:
                        _ctx.Contacts.Update(contact);
                        break;
                    case TrackingState.Deleted:
                        _ctx.Contacts.Remove(contact);
                        break;
                }
            }
        }
    }
}
