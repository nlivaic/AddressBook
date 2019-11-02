import * as signalR from "@aspnet/signalr";
import { getContact } from "./reducers";
import { receiveContactType } from "./reducers/Contact";

const addressBookHubMiddleware = url => {
  return store => {
    let addressBookHub = new signalR.HubConnectionBuilder()
      .withUrl(url)
      .build();

    addressBookHub.on("updateContact", updatedContact => {
      var currentContactId = getContact(store.getState()).id;
      if (updatedContact.id === currentContactId) {
        store.dispatch({
          type: receiveContactType,
          response: updatedContact
        });
      }
    });

    addressBookHub
      .start()
      .then()
      .catch(err => console.error(err.toString()));

    return next => action => {
      return next(action);
    };
  };
};

export default addressBookHubMiddleware;
