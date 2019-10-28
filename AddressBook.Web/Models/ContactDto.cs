using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AddressBook.Core;
using Ganss.XSS;

namespace AddressBook.Web.Models
{
    public class ContactDto
    {
        [RegularExpression("((?s)^(?!.*00000000-0000-0000-0000-000000000000).*$)|(^$)", ErrorMessage = "Id must be either empty or non-default Guid.")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Name must be non-null")]
        public string Name { get; set; }
        [Required(ErrorMessage = "All address details must be provided.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "All address details must be provided.")]
        public string StreetNr { get; set; }
        [Required(ErrorMessage = "All address details must be provided.")]
        public string City { get; set; }
        [Required(ErrorMessage = "All address details must be provided.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Date of birth must be in proper format: DD.MM.YYYY.")]
        [RegularExpression(@"^\d{1,2}.\d{1,2}\.\d{4}\.$")]
        public string DateOfBirth { get; set; }
        [RegularExpression("((?s)^(?!.*00000000-0000-0000-0000-000000000000).*$)|(^$)", ErrorMessage = "Address Book Id must be either empty or non-default Guid.")]
        public string AddressBookId { get; set; }
        public IEnumerable<string> TelephoneNumbers { get; set; } = new List<string>();

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
                AddressBookId = contact.AddressBookId.ToString(),
                TelephoneNumbers = contact.TelephoneNumbers.Select(t => t.Value)
            };
        }

        public Contact Create()
        {
            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    return new Contact(
                        Name,
                        new Address(
                            Street,
                            StreetNr,
                            City,
                            Country
                        ),
                        DateTime.Parse(DateOfBirth),
                        new Guid(AddressBookId),
                        TelephoneNumbers.Select(t => new TelephoneNumber(t)).ToList());
                }
                else
                {
                    return new Contact(
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
                        TelephoneNumbers.Select(t => new TelephoneNumber(t)).ToList());
                }
            }
            catch (ArgumentException argEx)
            {
                throw argEx;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Invalid data on input: {ex.Message}");
            }
        }

        public void Sanitize(HtmlSanitizer sanitizer)
        {
            Id = sanitizer.Sanitize(Id);
            Name = sanitizer.Sanitize(Name);
            Street = sanitizer.Sanitize(Street);
            StreetNr = sanitizer.Sanitize(StreetNr);
            City = sanitizer.Sanitize(City);
            Country = sanitizer.Sanitize(Country);
            DateOfBirth = sanitizer.Sanitize(DateOfBirth);
            AddressBookId = sanitizer.Sanitize(AddressBookId);
            TelephoneNumbers = TelephoneNumbers.Select(t => sanitizer.Sanitize(t));
        }
    }
}