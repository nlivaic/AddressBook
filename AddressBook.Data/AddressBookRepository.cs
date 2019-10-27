using System;
using System.Linq;
using AddressBook.Core;
using AddressBook.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
            var contacts = _ctx
                .Contacts
                .Include(c => c.TelephoneNumbers)
                .AsNoTracking()
                .Where(c => c.Id == contactId)
                .ToList();
            var addressBook = new Core.AddressBook(
                _ctx.AddressBooks
                    .AsNoTracking()
                    .SingleOrDefault().Id,
                contacts);
            return addressBook;
        }

        public Core.AddressBook GetContact(string name, Address address)
        {
            var contacts = _ctx
                .Contacts
                .Include(c => c.TelephoneNumbers)
                .AsNoTracking()
                .Where(c => c.Name == name && c.Address == address)
                .ToList();
            var addressBook = new Core.AddressBook(
                _ctx.AddressBooks
                    .AsNoTracking()
                    .SingleOrDefault().Id,
                contacts);
            return addressBook;
        }

        public Core.AddressBook GetContacts(int page = 1)
        {
            var contacts = _ctx
                .Contacts
                .OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(c => c.TelephoneNumbers)
                .AsNoTracking()
                .ToList();
            var addressBook = new Core.AddressBook(
                _ctx.AddressBooks
                    .AsNoTracking()
                    .SingleOrDefault().Id,
                contacts);
            return addressBook;
        }

        public async Task SaveAsync(Core.AddressBook addressBook)
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
            await _ctx.SaveChangesAsync();
        }
    }
}
