using System;
using Xunit;

namespace AddressBook.Core.Tests
{
    public class TelephoneNumberTests
    {
        [Fact]
        public void TelephoneNumber_MustBeInProperFormatShorter()
        {
            // Arrange, Act
            TelephoneNumber target = new TelephoneNumber("091123456");

            // Assert
            Assert.Equal("091123456", target.Value);
        }

        [Fact]
        public void TelephoneNumber_MustBeInProperFormatLonger()
        {
            // Arrange, Act
            TelephoneNumber target = new TelephoneNumber("0911234567");

            // Assert
            Assert.Equal("0911234567", target.Value);
        }

        [Fact]
        public void TelephoneNumber_ImproperFormat_Throws()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => new TelephoneNumber("09112345"));
        }
    }
}