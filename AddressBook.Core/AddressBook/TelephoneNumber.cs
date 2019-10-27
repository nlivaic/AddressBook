using System;
using System.Text.RegularExpressions;
using AddressBook.SharedKernel;

namespace AddressBook.Core
{
    public class TelephoneNumber : BaseEntity<Guid, TelephoneNumber>
    {
        public string Value { get; private set; }
        public Guid ContactId { get; private set; }
        public TrackingState Tracking { get; set; }

        private TelephoneNumber() : base() { }

        public TelephoneNumber(Guid contactId, string value) : this(Guid.NewGuid(), contactId, value) { }

        public TelephoneNumber(Guid id, Guid contactId, string value) : base(id)
        {
            if (!Regex.IsMatch(value, @"^\d{9,10}$"))
            {
                throw new ArgumentException("Telephone number must consist of 9 or 10 digits.");
            }
            ContactId = contactId;
            Value = value;
        }

        public override bool Equals(TelephoneNumber other) => Value == other.Value;
    }
}