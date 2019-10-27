using System;
using System.Collections.Generic;
using AddressBook.Core;

namespace AddressBook.Web.Models
{
    public class ContactDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string StreetNr { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string DateOfBirth { get; set; }
        public string AddressBookId { get; set; }

        public static ContactDto Create(Contact contact)
        {
            return new ContactDto
            {
                Id = contact.Id.ToString(),
                Name = contact.Name,
                Street = contact.Address.Street,
                StreetNr = contact.Address.StreetNr,
                City = contact.Address.City,
                Country = contact.Address.Country,
                DateOfBirth = contact.DateOfBirth.ToShortDateString(),
                AddressBookId = contact.AddressBookId.ToString()
            };
        }

        public Contact Create() =>
            string.IsNullOrEmpty(Id) ?
                new Contact(
                    Name,
                    new Address(
                        Street,
                        StreetNr,
                        City,
                        Country
                    ),
                    DateTime.Parse(DateOfBirth),
                    new Guid(AddressBookId),
                    new List<TelephoneNumber>())
                :
                new Contact(
                    new Guid(Id),
                    Name,
                    new Address(
                        Street,
                        StreetNr,
                        City,
                        Country
                    ),
                    DateTime.Parse(DateOfBirth),
                    new Guid(AddressBookId),
                    new List<TelephoneNumber>());

    }
}