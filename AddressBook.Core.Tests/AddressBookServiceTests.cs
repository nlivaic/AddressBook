using System;
using System.Linq;
using AddressBook.Core.Services;
using AddressBook.SharedKernel;
using NSubstitute;
using Xunit;

namespace AddressBook.Core.Tests
{
    public class AddressBookServiceTests
    {
        [Fact]
        public async void Service_CanAddUniqueContact()
        {
            // Arrange
            AddressBook addressBook = new AddressBookBuilder()
               .Build();
            Contact contact = AddressBookBuilder.BuildContact4(addressBook.Id);
            IAddressBookRepository repo = Substitute.For<IAddressBookRepository>();
            repo.GetContact(contact.Name, contact.Address).Returns(addressBook);
            AddressBookService target = new AddressBookService(repo);

            // Act
            await target.AddContactAsync(contact);

            // Assert - repo called.
            await repo.Received().SaveAsync(addressBook);
            Assert.Contains(contact, addressBook.Contacts);
        }

        [Fact]
        public void Service_CannotAddNonUniqueContact_WillThrow()
        {
            // Arrange
            AddressBook addressBook = new AddressBookBuilder()
               .AddContact3()
               .Build();
            Contact contact = AddressBookBuilder.BuildContact3(addressBook.Id);
            IAddressBookRepository repo = Substitute.For<IAddressBookRepository>();
            repo.GetContact(contact.Name, contact.Address).Returns(addressBook);
            AddressBookService target = new AddressBookService(repo);

            // Act, Assert
            Assert.ThrowsAsync<ArgumentException>(() => target.AddContactAsync(contact));
        }

        [Fact]
        public async void Service_CanRemoveContact()
        {
            // Arrange
            AddressBook addressBook = new AddressBookBuilder()
                .AddContact1()
               .Build();
            Contact contactToRemove = AddressBookBuilder.BuildContact1(addressBook.Id);
            IAddressBookRepository repo = Substitute.For<IAddressBookRepository>();
            repo.GetContact(contactToRemove.Id).Returns(addressBook);
            AddressBookService target = new AddressBookService(repo);

            // Act
            await target.RemoveContactAsync(contactToRemove.Id);

            // Assert - repo called.
            await repo.Received().SaveAsync(addressBook);
            Assert.Equal(TrackingState.Deleted, addressBook.Contacts.First().Tracking);
        }

        [Fact]
        public async void Service_CanUpdateContact()
        {
            // Arrange
            AddressBook addressBook = new AddressBookBuilder()
                .AddContact1()
               .Build();
            Contact contactWithUpdateDetails = AddressBookBuilder.BuildContact1(addressBook.Id);
            IAddressBookRepository repo = Substitute.For<IAddressBookRepository>();
            repo.GetContact(contactWithUpdateDetails.Id).Returns(addressBook);
            AddressBookService target = new AddressBookService(repo);

            // Act
            await target.UpdateContactAsync(contactWithUpdateDetails);

            // Assert - repo called.
            await repo.Received().SaveAsync(addressBook);
            Assert.Equal(TrackingState.Modified, addressBook.Contacts.First().Tracking);
        }
    }
}