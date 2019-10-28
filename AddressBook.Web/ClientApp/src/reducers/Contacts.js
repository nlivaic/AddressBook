import { combineReducers } from "redux";
import * as api from "../api/contact";

const requestContactsType = "REQUEST_CONTACTS";
const receiveContactsType = "RECEIVE_CONTACTS";

export const actionCreators = {
  requestContacts: pageNr => dispatch => {
    dispatch({ type: requestContactsType });
    api
      .getContacts(pageNr)
      .then(data => dispatch({ response: data, type: receiveContactsType }));
  }
};

const byId = (state = {}, action) => {
  switch (action.type) {
    case receiveContactsType:
      return state;
    default:
      return state;
  }
};

const contacts = (state = [], action) => {
  switch (action.type) {
    case receiveContactsType:
      return [...action.response];
    default:
      return state;
  }
};

const isLoading = (state = false, action) => {
  switch (action.type) {
    case receiveContactsType:
      return false;
    case requestContactsType:
      return true;
    default:
      return state;
  }
};

export default combineReducers({ contacts, byId, isLoading });

// Selectors
export const getAllContacts = state => state.contacts;

export const getIsLoading = state => state.isLoading;
