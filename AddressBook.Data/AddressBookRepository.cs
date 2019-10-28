using System;
using System.Linq;
using AddressBook.Core;
using AddressBook.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        public async Task<Core.AddressBook> GetContactAsync(Guid contactId)
        {
            var contacts = await _ctx
                .Contacts
                .Include(c => c.TelephoneNumbers)
                .AsNoTracking()
                .Where(c => c.Id == contactId)
                .ToListAsync();
            Core.AddressBook addressBook1 = await _ctx.AddressBooks
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            var addressBook = new Core.AddressBook(
               (await _ctx.AddressBooks
                    .AsNoTracking()
                    .SingleOrDefaultAsync()).Id,
                contacts);
            return addressBook;
        }

        public async Task<Core.AddressBook> GetContactAsync(string name, Address address)
        {
            var contacts = await _ctx
                .Contacts
                .Include(c => c.TelephoneNumbers)
                .AsNoTracking()
                .Where(c => c.Name == name && c.Address == address)
                .ToListAsync();
            var addressBook = new Core.AddressBook(
               (await _ctx.AddressBooks
                    .AsNoTracking()
                    .SingleOrDefaultAsync()).Id,
                contacts);
            return addressBook;
        }

        public async Task<Core.AddressBook> GetContactsAsync(int page = 1)
        {
            var contacts = await _ctx
                .Contacts
                .OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(c => c.TelephoneNumbers)
                .AsNoTracking()
                .ToListAsync();
            var addressBook = new Core.AddressBook(
                (await _ctx.AddressBooks
                    .AsNoTracking()
                    .SingleOrDefaultAsync()).Id,
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
                foreach (var telephone in contact.TelephoneNumbers)
                {
                    switch (telephone.Tracking)
                    {
                        case TrackingState.Added:
                            _ctx.TelephoneNumbers.Add(telephone);
                            break;
                        case TrackingState.Deleted:
                            _ctx.TelephoneNumbers.Remove(telephone);
                            break;
                    }
                }
            }
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Core.AddressBook>> GetAddressBooksAsync() => await _ctx.AddressBooks.ToListAsync();
    }
}
