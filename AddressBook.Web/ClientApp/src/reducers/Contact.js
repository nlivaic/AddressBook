import { push } from "connected-react-router";
import { combineReducers } from "redux";
import * as api from "../api/contact";

const requestContactType = "REQUEST_CONTACT";
export const receiveContactType = "RECEIVE_CONTACT";
const notFoundContactType = "NOT_FOUND_CONTACT";
const readContactType = "READ_CONTACT";
const editContactType = "EDIT_CONTACT";
const requestUpdateContactType = "REQUEST_UPDATE_CONTACT";
const receiveUpdateContactType = "RECEIVE_UPDATE_CONTACT";
const requestCreateContactType = "REQUEST_CREATE_CONTACT";
const receiveCreateContactType = "RECEIVE_CREATE_CONTACT";
const requestDeleteContactType = "REQUEST_DELETE_CONTACT";
const receiveDeleteContactType = "RECEIVE_DELETE_CONTACT";
const resetContactType = "RESET_CONTACT";
const errorType = "ERROR_CONTACT";
const initialState = {
  id: "",
  name: "",
  street: "",
  streetNr: "",
  city: "",
  country: "",
  dateOfBirth: "",
  addressBookId: "",
  telephoneNumbers: []
};
const initialErrorState = { isError: false, message: "" };

export const actionCreators = {
  cancelContactEditing: () => dispatch => {
    dispatch({ type: readContactType });
  },
  createContact: contact => dispatch => {
    dispatch({ type: requestCreateContactType });
    api
      .createContact(contact)
      .then(data => {
        dispatch({ type: receiveCreateContactType });
        return data;
      })
      .then(data => {
        dispatch(push(`/contact/${data.id}`));
      })
      .catch(error => {
        dispatch({
          response: `An error occurred trying to save contact: ${error.message}.`,
          type: errorType
        });
      });
  },
  deleteContact: id => dispatch => {
    dispatch({ type: requestDeleteContactType });
    api
      .deleteContact(id)
      .then(() => dispatch({ type: receiveDeleteContactType }))
      .then(() => dispatch(push("/")));
  },
  editContact: () => dispatch => {
    dispatch({ type: editContactType });
  },
  requestContact: id => dispatch => {
    dispatch({ type: requestContactType });
    api
      .getContact(id)
      .then(data => dispatch({ response: data, type: receiveContactType }))
      .catch(error => {
        switch (error.status) {
          case 404:
            dispatch({ type: notFoundContactType });
            break;
          default:
            dispatch({
              response: `An error occurred trying to fetch contact ${id}: ${error.message}.`,
              type: errorType
            });
            break;
        }
      });
  },
  resetContact: () => dispatch => {
    dispatch({ type: resetContactType });
  },
  updateContact: contact => dispatch => {
    dispatch({ type: requestUpdateContactType });
    api
      .updateContact(contact)
      .then(() => dispatch({ data: contact, type: receiveUpdateContactType }))
      .then(() => dispatch({ type: readContactType }))
      .catch(error => {
        dispatch({
          response: `An error occurred trying to save contact: ${error.message}.`,
          type: errorType
        });
      });
  },
  readContact: () => dispatch => {
    dispatch({ type: readContactType });
  }
};

const contact = (state = initialState, action) => {
  switch (action.type) {
    case receiveContactType:
      return action.response;
    case receiveUpdateContactType:
      return {
        ...state,
        name: action.data.name,
        street: action.data.street,
        streetNr: action.data.streetNr,
        city: action.data.city,
        country: action.data.country,
        dateOfBirth: action.data.dateOfBirth,
        telephoneNumbers: action.data.telephoneNumbers
      };
    case resetContactType:
      return initialState;
    default:
      return state;
  }
};

const isLoading = (state = false, action) => {
  switch (action.type) {
    case errorType:
    case notFoundContactType:
    case receiveContactType:
    case resetContactType:
      return false;
    case requestContactType:
      return true;
    default:
      return state;
  }
};

const isNotFound = (state = false, action) => {
  switch (action.type) {
    case notFoundContactType:
      return true;
    case receiveContactType:
      return false;
    default:
      return state;
  }
};

const isEditing = (state = false, action) => {
  switch (action.type) {
    case editContactType:
      return true;
    case readContactType:
      return false;
    default:
      return state;
  }
};

const isRequestingSaveContact = (state = false, action) => {
  switch (action.type) {
    case errorType:
    case receiveCreateContactType:
    case receiveUpdateContactType:
      return false;
    case requestCreateContactType:
    case requestUpdateContactType:
      return true;
    default:
      return state;
  }
};

const isDeleting = (state = false, action) => {
  switch (action.type) {
    case errorType:
    case receiveDeleteContactType:
      return false;
    case requestDeleteContactType:
      return true;
    default:
      return state;
  }
};

const error = (state = initialErrorState, action) => {
  switch (action.type) {
    case errorType:
      return {
        isError: true,
        message: action.response
      };
    case resetContactType:
    case receiveContactType:
      return initialErrorState;
    default:
      return state;
  }
};

export default combineReducers({
  contact,
  error,
  isDeleting,
  isEditing,
  isLoading,
  isNotFound,
  isRequestingSaveContact
});

// selectors
export const getContact = state => state.contact;

export const getIsLoading = state => state.isLoading;

export const getIsNotFound = state => state.isNotFound;

export const getError = state => state.error;

export const getIsEditing = state => state.isEditing;

export const getIsRequestingSaveContact = state =>
  state.isRequestingSaveContact;

export const getIsDeleting = state => state.isDeleting;
