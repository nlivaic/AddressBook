using System;
using AddressBook.SharedKernel;

namespace AddressBook.Core
{
    public class Address : BaseValueObject<Address>
    {
        public string Street { get; private set; }
        public string StreetNr { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }

        private Address() { }

        public Address(string street, string streetNr, string city, string country)
        {
            if (string.IsNullOrEmpty(street) || string.IsNullOrEmpty(streetNr) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(country))
            {
                throw new ArgumentException("All address fields must be provided.");
            }
            Street = street;
            StreetNr = streetNr;
            City = city;
            Country = country;
        }

        public override bool Equals(Address other)
            => Street == other.Street && StreetNr == other.StreetNr && City == other.City && Country == other.Country;

        public override string ToString()
        {
            return $"{Street} {StreetNr}, {City}, {Country}";
        }
    }
}