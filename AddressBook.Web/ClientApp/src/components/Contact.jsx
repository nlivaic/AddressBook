import React from "react";

const Contact = ({ contact, editContact }) => {
  return (
    <div>
      <div>
        Name: {contact.name}
        <br />
        Street: {contact.street}
        <br />
        Street Nr: {contact.streetNr}
        <br />
        City: {contact.city}
        <br />
        Country: {contact.country}
        <br />
        Date Of Birth: {contact.dateOfBirth}
        <br />
        <button onClick={editContact}>Edit</button>
      </div>
    </div>
  );
};

export default Contact;
