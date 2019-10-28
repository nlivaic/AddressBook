import { connectRouter } from "connected-react-router";
import { combineReducers } from "redux";
import contact, * as fromContact from "./Contact";
import contacts, * as fromContacts from "./Contacts";

export default history =>
  combineReducers({
    contact,
    contacts,
    router: connectRouter(history)
  });

// Top-level selectors - Contact
export const getAllContacts = state =>
  fromContacts.getAllContacts(state.contacts);

export const getContactsIsLoading = state =>
  fromContacts.getIsLoading(state.contacts);

// Top-level selectors - Contact
export const getContact = state => fromContact.getContact(state.contact);

export const getContactIsLoading = state =>
  fromContact.getIsLoading(state.contact);

export const getContactIsNotFound = state =>
  fromContact.getIsNotFound(state.contact);

export const getContactError = state => fromContact.getError(state.contact);

export const getContactIsEditing = state =>
  fromContact.getIsEditing(state.contact);

export const getIsRequestingSaveContact = state =>
  fromContact.getIsRequestingSaveContact(state.contact);

export const getContactIsDeleting = state =>
  fromContact.getIsDeleting(state.contact);
