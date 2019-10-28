using AddressBook.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AddressBook.Data
{
    public class AddressBookDbContext : DbContext
    {
        public DbSet<Core.AddressBook> AddressBooks { get; set; }
        public DbSet<Core.Contact> Contacts { get; set; }
        public DbSet<Core.TelephoneNumber> TelephoneNumbers { get; set; }

        public AddressBookDbContext(DbContextOptions<AddressBookDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().OwnsOne(p => p.Address);
            modelBuilder.Entity<Contact>().Ignore(c => c.Tracking);
            modelBuilder.Entity<TelephoneNumber>().Ignore(t => t.Tracking);
        }
    }
}