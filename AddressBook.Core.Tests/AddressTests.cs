using System;
using Xunit;

namespace AddressBook.Core.Tests
{
    public class AddressTests
    {
        [Fact]
        public void Address_AllDetailsMustBeProvided()
        {
            // Arrange, Act
            Address target = new Address("First Street", "15a", "New York", "USA");

            // Assert
            Assert.Equal("First Street", target.Street);
            Assert.Equal("15a", target.StreetNr);
            Assert.Equal("New York", target.City);
            Assert.Equal("USA", target.Country);
        }

        [Fact]
        public void Address_MissingDetails_Throws()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => new Address("", string.Empty, "New York", "USA"));
        }
    }
}