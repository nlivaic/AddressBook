using System;
using System.Text.RegularExpressions;
using AddressBook.SharedKernel;

namespace AddressBook.Core
{
    public class TelephoneNumber : BaseValueObject<TelephoneNumber>
    {
        public Guid Id { get; private set; }
        public Guid ContactId { get; private set; }
        public string Value { get; private set; }
        public TrackingState Tracking { get; set; }

        private TelephoneNumber() : base() { }

        public TelephoneNumber(string value)
        {
            Id = new Guid();
            if (!Regex.IsMatch(value, @"^\d{9,10}$"))
            {
                throw new ArgumentException("Telephone number must consist of 9 or 10 digits.");
            }
            Value = value;
        }

        public override bool Equals(TelephoneNumber other) => Value == other.Value;
    }
}