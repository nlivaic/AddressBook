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
        Telephone Numbers:
        {contact.telephoneNumbers.map(t => (
          <div style={{ marginLeft: "50px" }} key={t}>
            {t}
          </div>
        ))}
        <br />
        <button onClick={editContact}>Edit</button>
      </div>
    </div>
  );
};

export default Contact;
