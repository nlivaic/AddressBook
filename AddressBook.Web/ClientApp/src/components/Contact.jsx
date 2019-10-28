import React from "react";
import { Link } from "react-router-dom";

const Contact = ({ contact, editContact }) => {
  return (
    <div>
      <Link to={`/Contact/${contact.id}`}>{contact.name}</Link>
      {<button onClick={editContact}>Edit</button>}
    </div>
  );
};

export default Contact;
