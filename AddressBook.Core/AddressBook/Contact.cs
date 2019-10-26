using System;
using System.Collections.Generic;
using System.Linq;
using AddressBook.SharedKernel;

namespace AddressBook.Core
{
    public class Contact : BaseEntity<Guid, Contact>
    {
        private List<TelephoneNumber> _telephoneNumbers { get; set; }

        public string Name { get; private set; }
        public Address Address { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Guid AddressBookId { get; private set; }
        public IEnumerable<TelephoneNumber> TelephoneNumbers => _telephoneNumbers.AsEnumerable();

        private Contact() : base() { }

        public Contact(string name, Address address, DateTime dateOfBirth, Guid addressBookId)
            : this(Guid.NewGuid(), name, address, dateOfBirth, addressBookId, new List<TelephoneNumber>()) { }

        public Contact(string name, Address address, DateTime dateOfBirth, Guid addressBookId, List<TelephoneNumber> telephoneNumbers)
            : this(Guid.NewGuid(), name, address, dateOfBirth, addressBookId, telephoneNumbers) { }

        public Contact(Guid id, string name, Address address, DateTime dateOfBirth, Guid addressBookId, List<TelephoneNumber> telephoneNumbers) : base(id)
        {
            Validate(name, address, telephoneNumbers);
            Name = name;
            Address = address;
            DateOfBirth = dateOfBirth;
            AddressBookId = addressBookId;
            _telephoneNumbers = telephoneNumbers;
        }

        public void Assign(TelephoneNumber telephoneNumber)
        {
            if (_telephoneNumbers.Any(t => t.Value == telephoneNumber.Value))
            {
                throw new ArgumentException($"Telephone number {telephoneNumber.Value} already assigned to contact {Name}.");
            }
            _telephoneNumbers.Add(telephoneNumber);
        }

        public void Remove(TelephoneNumber telephoneNumber)
        {
            _telephoneNumbers.Remove(telephoneNumber);
        }

        public void UpdateContact(string name, Address address, DateTime dateOfBirth)
        {
            Validate(name, address, _telephoneNumbers);
            Name = name;
            Address = address;
            DateOfBirth = dateOfBirth;
        }

        private void Validate(string name, Address address, List<TelephoneNumber> telephoneNumbers)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be empty or null.");
            if (address == null) throw new ArgumentException("Address cannot be null.");
            if (telephoneNumbers == null) throw new ArgumentException("Telephone numbers collection must be initialized.");
        }

        public override bool Equals(Contact other) => Name == other.Name && Address == other.Address;
    }
}