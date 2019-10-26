using System;
using System.Collections.Generic;
using Xunit;
using AddressBook.Core;

namespace AddressBook.SharedKernel.Tests
{
    public class EntityTests
    {
        [Fact]
        public void Entity_CanTestEqualityOnId_Equal()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            AddressBook addressBook1 = new AddressBook(id, new List<Contact>());
            AddressBook addressBook2 = new AddressBook(id, new List<Contact>());

            // Act
            // bool result = addressBook1.Equals(addressBook2);
            bool result = addressBook1 == addressBook2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Entity_CanTestEquals_Equal()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            AddressBook addressBook1 = new AddressBook(id, new List<Contact>());
            AddressBook addressBook2 = new AddressBook(id, new List<Contact>());

            // Act
            bool result = addressBook1.Equals(addressBook2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Entity_CanTestEqualityOnId_NotEqual()
        {
            // Arrange
            AddressBook addressBook1 = new AddressBook(new List<Contact>());
            AddressBook addressBook2 = new AddressBook(new List<Contact>());

            // Act
            bool result = addressBook1 == addressBook2;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Contacts_CanTestCustomEquality_Equal()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Guid addressBookId = Guid.NewGuid();
            Address address1 = new Address("First Street", "15a", "New York", "USA");
            Address address2 = new Address("First Street", "15a", "New York", "USA");
            Contact contact1 = new Contact(id, "John Doe", address1, new DateTime(1984, 1, 1), addressBookId, new List<TelephoneNumber>());
            Contact contact2 = new Contact(id, "John Doe", address1, new DateTime(1984, 1, 1), addressBookId, new List<TelephoneNumber>());

            // Act
            bool result = contact1 == contact2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Contacts_CanTestCustomEquality_NotEqual()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Guid addressBookId = Guid.NewGuid();
            Address address1 = new Address("First Street", "15a", "New York", "USA");
            Address address2 = new Address("First Street", "15a", "New York", "USA");
            Contact contact1 = new Contact(id, "John Doe", address1, new DateTime(1984, 1, 1), addressBookId, new List<TelephoneNumber>());
            Contact contact2 = new Contact(id, "Jane Doe", address1, new DateTime(1984, 1, 1), addressBookId, new List<TelephoneNumber>());

            // Act
            bool result = contact1 == contact2;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Entity_IdCannotBeNullOrDefault_Throws()
        {
            // Arrange
            Guid id = default(Guid);
            Address address = new Address("First Street", "15a", "New York", "USA");

            // Assert
            Assert.Throws<ArgumentException>(() => new Contact(id, "John Doe", address, new DateTime(1984, 1, 1), Guid.NewGuid(), new List<TelephoneNumber>()));
        }
    }
}
