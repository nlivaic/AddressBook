using System;
using System.Collections.Generic;

namespace AddressBook.Core.Tests
{
    public class AddressBookBuilder
    {
        private Guid _targetId;
        private List<Contact> _contacts;

        public AddressBookBuilder()
        {
            _contacts = new List<Contact>();
            _targetId = Guid.NewGuid();
        }
        public AddressBookBuilder AddContact1()
        {
            _contacts.Add(BuildContact1(_targetId));
            return this;
        }

        public AddressBookBuilder AddContact2()
        {
            _contacts.Add(BuildContact2(_targetId));
            return this;
        }

        public AddressBookBuilder AddContact3()
        {
            _contacts.Add(BuildContact3(_targetId));
            return this;
        }

        public static Contact BuildContact1(Guid addressBookId)
        {
            Address address = new Address("First Street", "15a", "New York", "USA");
            return new Contact(
                new Guid("fff7e724-e7a2-4f80-96d5-7ab6670e6c06"),
                "John Doe",
                address,
                new DateTime(1984, 1, 1),
                addressBookId,
                new List<TelephoneNumber>());
        }

        public static Contact BuildContact2(Guid addressBookId)
        {
            Address address = new Address("First Street", "30a", "New York", "USA");
            return new Contact(
                new Guid("dd884098-0f06-43a0-92c2-7301cf68451a"),
                "Jane Doe",
                address,
                new DateTime(1977, 1, 1),
                addressBookId,
                new List<TelephoneNumber>());
        }

        public static Contact BuildContact3(Guid addressBookId)
        {
            Address address = new Address("Tenth Street", "45", "New York", "USA");
            return new Contact(
                new Guid("a3dd62ed-e9f2-4fc1-aa55-161fbeee3061"),
                "Bill Doe",
                address,
                new DateTime(1981, 1, 1),
                addressBookId,
                new List<TelephoneNumber>());
        }

        public static Contact BuildContact4(Guid addressBookId)
        {
            Address address = new Address("Tenth Street", "100", "New York", "USA");
            return new Contact(
                new Guid("4ee13300-ebe2-4c30-9e8e-d903150bdc32"),
                "Jill Doe",
                address,
                new DateTime(1982, 1, 1),
                addressBookId,
                new List<TelephoneNumber>());
        }

        public AddressBook Build() => new AddressBook(_targetId, _contacts);
    }
}