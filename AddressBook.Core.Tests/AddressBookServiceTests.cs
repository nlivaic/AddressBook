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
        public void Service_CanAddUniqueContact()
        {
            // Arrange
            AddressBook addressBook = new AddressBookBuilder()
               .Build();
            Contact contact = AddressBookBuilder.BuildContact4(addressBook.Id);
            IAddressBookRepository repo = Substitute.For<IAddressBookRepository>();
            repo.GetContact(contact.Name, contact.Address).Returns(addressBook);
            AddressBookService target = new AddressBookService(repo);

            // Act
            target.AddContact(contact);

            // Assert - repo called.
            repo.Received().Save(addressBook);
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
            Assert.Throws<ArgumentException>(() => target.AddContact(contact));
        }

        [Fact]
        public void Service_CanRemoveContact()
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
            target.RemoveContact(contactToRemove.Id);

            // Assert - repo called.
            repo.Received().Save(addressBook);
            Assert.Equal(TrackingState.Deleted, addressBook.Contacts.First().Tracking);
        }

        [Fact]
        public void Service_CanUpdateContact()
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
            target.UpdateContact(contactWithUpdateDetails);

            // Assert - repo called.
            repo.Received().Save(addressBook);
            Assert.Equal(TrackingState.Modified, addressBook.Contacts.First().Tracking);
        }
    }
}