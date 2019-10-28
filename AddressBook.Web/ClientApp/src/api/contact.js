import { handleHttpError } from "./HttpError";

export const getContacts = pageNr =>
  fetch(`/api/contact?pageNr=${pageNr}`).then(data => data.json());

export const getContact = id =>
  fetch(`/api/contact/${id}`)
    .then(handleHttpError)
    .then(response => response.json());

export const createContact = contact =>
  fetch("/api/contact", {
    body: JSON.stringify(contact),
    headers: {
      "Content-Type": "application/json"
    },
    method: "POST"
  })
    .then(handleHttpError)
    .then(response => response.json());

export const updateContact = contact =>
  fetch(`/api/contact/${contact.id}`, {
    body: JSON.stringify(contact),
    headers: {
      "Content-Type": "application/json"
    },
    method: "PUT"
  })
    .then(handleHttpError)
    .then(response => {
      if (response.status !== 204) {
        return response.json();
      } else {
        return response;
      }
    });

export const deleteContact = id =>
  fetch(`/api/contact/${id}`, {
    method: "DELETE"
  }).then(handleHttpError);
