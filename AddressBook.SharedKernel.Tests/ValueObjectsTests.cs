using System;
using Xunit;
using AddressBook.Core;

namespace AddressBook.Core.Tests
{
    public class ValueObjectsTests
    {
        [Fact]
        public void ValueObjects_CanTestEquality_Equal()
        {
            // Arrange
            Address address1 = new Address("First Street", "15a", "New York", "USA");
            Address address2 = new Address("First Street", "15a", "New York", "USA");

            // Act
            bool result = address1 == address2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValueObjects_CanTestEquality_NotEqual()
        {
            // Arrange
            Address address1 = new Address("First Street", "15a", "New York", "USA");
            Address address2 = new Address("First Street", "15b", "New York", "USA");

            // Act
            bool result = address1 == address2;

            // Assert
            Assert.False(result);
        }
    }
}
