using System;
using Xunit;

namespace AddressBook.Core.Tests
{
    public class TelephoneNumberTests
    {
        [Theory]
        [InlineData("091123456")]
        [InlineData("0911234567")]
        public void TelephoneNumber_MustBeInProperFormat(
            string telephoneNumber)
        {
            // Arrange, Act
            TelephoneNumber target = new TelephoneNumber(Guid.NewGuid(), telephoneNumber);

            // Assert
            Assert.Equal(telephoneNumber, target.Value);
        }

        [Fact]
        public void TelephoneNumber_ImproperFormat_Throws()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => new TelephoneNumber(Guid.NewGuid(), "09112345"));
        }
    }
}