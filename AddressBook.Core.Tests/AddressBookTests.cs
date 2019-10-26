using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AddressBook.Core.Tests
{
    public class AddressBookTests
    {
        [Fact]
        public void Entity_NonInitializedContacts_Throw()
        {
            // Arrange, act, assert
            Assert.Throws<ArgumentException>(() => new AddressBook(null));
        }

        [Fact]
        public void Entity_CanAddNewUniqueContact()
        {
            // Arrange
            AddressBook target = new AddressBook(new List<Contact>());
            Guid addressBookId = Guid.NewGuid();
            Address address1 = new Address("First Street", "15a", "New York", "USA");
            Address address2 = new Address("First Street", "15a", "New York", "USA");
            Contact contact1 = new Contact("John Doe", address1, new DateTime(1984, 1, 1), target.Id, new List<TelephoneNumber>());
            Contact contact2 = new Contact("Jane Doe", address1, new DateTime(1984, 1, 1), target.Id, new List<TelephoneNumber>());

            // Act
            target.RegisterNewContact(contact1);
            target.RegisterNewContact(contact2);
            var result = target.Contacts.ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("John Doe", result[0].Name);
            Assert.Equal("Jane Doe", result[1].Name);
        }

        [Fact]
        public void Entity_CannotAddNonUniqueContact_Throws()
        {
            // Arrange
            AddressBook target = new AddressBook(new List<Contact>());

            Address address1 = new Address("First Street", "15a", "New York", "USA");
            Address address2 = new Address("First Street", "15a", "New York", "USA");
            Contact contact1 = new Contact("John Doe", address1, new DateTime(1984, 1, 1), target.Id, new List<TelephoneNumber>());
            Contact contact2 = new Contact("John Doe", address1, new DateTime(1984, 1, 1), target.Id, new List<TelephoneNumber>());

            // Act
            target.RegisterNewContact(contact1);

            // Assert
            Assert.Throws<ArgumentException>(() => target.RegisterNewContact(contact2));
        }
    }
}
