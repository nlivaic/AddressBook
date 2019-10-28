import React, { Component } from "react";

class ContactEdit extends Component {
  state = {
    name: this.props.contact.name,
    street: this.props.contact.street,
    streetNr: this.props.contact.streetNr,
    city: this.props.contact.city,
    country: this.props.contact.country,
    dateOfBirth: this.props.contact.dateOfBirth,
    addressBookId: this.props.contact.addressBookId,
    telephoneNumbers: this.props.contact.telephoneNumbers,
    newTelephoneNumber: ""
  };

  componentDidUpdate(prevProps) {
    if (this.props.contact.name !== prevProps.contact.name) {
      this.setState({ name: this.props.contact.name });
    }
    if (this.props.contact.street !== prevProps.contact.street) {
      this.setState({ street: this.props.contact.street });
    }
    if (this.props.contact.streetNr !== prevProps.contact.streetNr) {
      this.setState({ streetNr: this.props.contact.streetNr });
    }
    if (this.props.contact.city !== prevProps.contact.city) {
      this.setState({ city: this.props.contact.city });
    }
    if (this.props.contact.country !== prevProps.contact.country) {
      this.setState({ country: this.props.contact.country });
    }
    if (this.props.contact.dateOfBirth !== prevProps.contact.dateOfBirth) {
      this.setState({ dateOfBirth: this.props.contact.dateOfBirth });
    }
    if (
      !this.arraysEqual(
        this.props.contact.telephoneNumbers,
        prevProps.contact.telephoneNumbers
      )
    ) {
      this.setState({ telephoneNumbers: this.props.contact.telephoneNumbers });
    }
  }

  appendTelephoneNumber() {
    this.setState(prevState => ({
      telephoneNumbers: prevState.telephoneNumbers.concat([
        prevState.newTelephoneNumber
      ]),
      newTelephoneNumber: ""
    }));
  }

  removeTelephoneNumber(telNr) {
    this.setState(prevState => {
      return {
        telephoneNumbers: prevState.telephoneNumbers.filter(t => t != telNr.t)
      };
    });
  }

  arraysEqual(arr1, arr2) {
    if (arr1.length !== arr2.length) return false;
    for (var i = arr1.length; i--; ) {
      if (arr1[i] !== arr2[i]) return false;
    }

    return true;
  }

  render() {
    const {
      cancelContactChanges,
      id,
      isSaving,
      saveContact,
      showCancel
    } = this.props;
    return (
      <div>
        Name:
        <input
          onChange={e => this.setState({ name: e.target.value })}
          type="text"
          value={this.state.name}
        />
        <br />
        Street:
        <input
          onChange={e => this.setState({ street: e.target.value })}
          type="text"
          value={this.state.street}
        />
        <br />
        Street Nr:
        <input
          onChange={e => this.setState({ streetNr: e.target.value })}
          type="text"
          value={this.state.streetNr}
        />
        <br />
        City:
        <input
          onChange={e => this.setState({ city: e.target.value })}
          type="text"
          value={this.state.city}
        />
        <br />
        Country:
        <input
          onChange={e => this.setState({ country: e.target.value })}
          type="text"
          value={this.state.country}
        />
        <br />
        Date Of Birth:
        <input
          onChange={e => this.setState({ dateOfBirth: e.target.value })}
          type="text"
          value={this.state.dateOfBirth}
        />
        <br />
        Telephone Numbers:
        <div id="divTelephoneNumbers">
          {this.state.telephoneNumbers.map(t => (
            <div style={{ marginLeft: "50px" }} key={Math.random()}>
              <span>{t}</span>
              <button onClick={() => this.removeTelephoneNumber({ t })}>
                Remove
              </button>
            </div>
          ))}
          <input
            onChange={e =>
              this.setState({ newTelephoneNumber: e.target.value })
            }
            type="text"
            value={this.state.newTelephoneNumber}
          />
          <button onClick={() => this.appendTelephoneNumber()}>
            Add phone number
          </button>
        </div>
        <br />
        {isSaving && "Saving..."}
        {!isSaving && (
          <button
            onClick={() => {
              saveContact(
                new ContactRequest(
                  id,
                  this.state.name,
                  this.state.street,
                  this.state.streetNr,
                  this.state.city,
                  this.state.country,
                  this.state.dateOfBirth,
                  this.state.addressBookId,
                  this.state.telephoneNumbers
                )
              );
            }}
          >
            Save
          </button>
        )}
        {showCancel && <button onClick={cancelContactChanges}>Cancel</button>}
      </div>
    );
  }
}

class ContactRequest {
  constructor(
    id,
    name,
    street,
    streetNr,
    city,
    country,
    dateOfBirth,
    addressBookId,
    telephoneNumbers
  ) {
    this.id = id;
    this.name = name;
    this.street = street;
    this.streetNr = streetNr;
    this.city = city;
    this.country = country;
    this.dateOfBirth = dateOfBirth;
    this.addressBookId = addressBookId;
    this.telephoneNumbers = telephoneNumbers;
  }
}

export default ContactEdit;
