using System;
using System.Text.RegularExpressions;
using AddressBook.SharedKernel;

namespace AddressBook.Core
{
    public class TelephoneNumber : BaseEntity<Guid, TelephoneNumber>
    {
        public string Value { get; private set; }
        public TrackingState Tracking { get; set; }

        private TelephoneNumber() { }

        public TelephoneNumber(string value)
        {
            if (!Regex.IsMatch(value, @"^\d{9,10}$"))
            {
                throw new ArgumentException("Telephone number must consist of 9 or 10 digits.");
            }
            Value = value;
        }

        public override bool Equals(TelephoneNumber other) => Value == other.Value;
    }
}