using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace AddressBook.Core.Tests
{
    public class ContactTests
    {
        [Fact]
        public void Contact_AllDetailsMustBeProvided()
        {
            // Arrange
            Address address = new Address("First Street", "15a", "New York", "USA");
            DateTime dateOfBirth = new DateTime(1984, 1, 1);

            // Act
            Contact target = new Contact("John Doe", address, dateOfBirth, Guid.NewGuid(), new List<TelephoneNumber>());

            // Assert
            Assert.Equal("John Doe", target.Name);
            Assert.Equal(address, target.Address);
            Assert.Equal(dateOfBirth, target.DateOfBirth);
        }

        [Fact]
        public void Contact_MissingDetails_Throws()
        {
            // Arrange
            Address address = new Address("First Street", "15a", "New York", "USA");

            // Act, Assert
            Assert.Throws<ArgumentException>(() => new Contact("John Doe", address, new DateTime(1984, 1, 1), Guid.NewGuid(), null));
        }

        [Fact]
        public void Contact_CanAddNewTelephoneNumber()
        {
            // Arrange
            Address address = new Address("First Street", "15a", "New York", "USA");
            Contact target = new Contact("John Doe", address, new DateTime(1984, 1, 1), Guid.NewGuid(), new List<TelephoneNumber>());

            // Act
            target.Assign(new TelephoneNumber(/*target.Id, */"091123456"));
            target.Assign(new TelephoneNumber(/*target.Id, */"091123457"));
            target.Assign(new TelephoneNumber(/*target.Id, */"091123458"));
            var result = target.TelephoneNumbers.ToList();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal("091123456", result[0].Value);
            Assert.Equal("091123457", result[1].Value);
            Assert.Equal("091123458", result[2].Value);
        }

        [Fact]
        public void Contact_CannotAddDuplicateTelephoneNumber_Throws()
        {
            // Arrange
            Address address = new Address("First Street", "15a", "New York", "USA");
            Contact target = new Contact("John Doe", address, new DateTime(1984, 1, 1), Guid.NewGuid(), new List<TelephoneNumber>());

            // Act
            target.Assign(new TelephoneNumber(/*target.Id, */"091123456"));

            // Assert
            Assert.Throws<ArgumentException>(() => target.Assign(new TelephoneNumber(/*target.Id, */"091123456")));
        }

        [Fact]
        public void Contacts_CanUpdateContactWithValidInformation()
        {
            // Arrange
            Address address = new Address("First Street", "15a", "New York", "USA");
            Contact target = new Contact(
                "John Doe",
                address,
                new DateTime(1984, 1, 1),
                Guid.NewGuid(),
                new List<TelephoneNumber> { new TelephoneNumber("091123456"), new TelephoneNumber("091123457") });
            Guid originalTargetId = target.Id;
            Address newAddress = new Address("Second Street", "30b", "Los Angeles", "USA");
            DateTime newDateOfBirth = new DateTime(1948, 1, 1);

            // Act
            target.UpdateContact("Joe Schmoe", newAddress, new DateTime(1948, 1, 1), new List<TelephoneNumber> { new TelephoneNumber("091123458") });

            // Assert
            Assert.Equal(originalTargetId, target.Id);
            Assert.Equal("Joe Schmoe", target.Name);
            Assert.Equal(newAddress, target.Address);
            Assert.Equal(newDateOfBirth, target.DateOfBirth);
            Assert.Equal(2, target.TelephoneNumbers.Where(t => t.Tracking == SharedKernel.TrackingState.Deleted).Count());
            Assert.Single(target.TelephoneNumbers.Where(t => t.Tracking == SharedKernel.TrackingState.Added));
            Assert.Equal("091123458", target.TelephoneNumbers.Single(t => t.Tracking == SharedKernel.TrackingState.Added).Value);
        }

        [Fact]
        public void Contacts_UpdateContactWithInvalidInformation_Throws()
        {
            // Arrange
            Address address = new Address("First Street", "15a", "New York", "USA");
            Contact target = new Contact("John Doe", address, new DateTime(1984, 1, 1), Guid.NewGuid(), new List<TelephoneNumber>());
            Address newAddress = new Address("Second Street", "30b", "Los Angeles", "USA");
            DateTime newDateOfBirth = new DateTime(1948, 1, 1);

            // Act
            Assert.Throws<ArgumentException>(() => target.UpdateContact(string.Empty, newAddress, new DateTime(1948, 1, 1), new List<TelephoneNumber>()));
        }
    }
}
