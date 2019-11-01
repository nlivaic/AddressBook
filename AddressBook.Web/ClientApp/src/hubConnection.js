import * as signalR from "@aspnet/signalr";
import { getContact } from "./reducers";
import { receiveContactType } from "./reducers/Contact";

const addressBookHubMiddleware = () => {
  return store => {
    let addressBookHub = new signalR.HubConnectionBuilder()
      .withUrl("/api/addressBookHub")
      .build();

    addressBookHub.on("updateContact", updatedContact => {
      console.log("Will try to update contact.");
      var currentContactId = getContact(store.getState()).id;
      if (updatedContact.id === currentContactId) {
        store.dispatch({
          type: receiveContactType,
          response: updatedContact
        });
        console.log(updatedContact);
      }
    });

    addressBookHub
      .start()
      .then
      //   () => addressBookHub
      //   .invoke("DoSomething")
      // .catch(err => console.error(err.toString()))
      ()
      .catch(err => console.error(err.toString()));

    return next => action => {
      return next(action);
    };

    // socket.on("message", (message) => {
    //     store.dispatch({
    //         type : "SOCKET_MESSAGE_RECEIVED",
    //         payload : message
    //     });
    // });

    // return next => action => {
    //   if (action.type == "SEND_WEBSOCKET_MESSAGE") {
    //     socket.send(action.payload);
    //     return;
    //   }

    //   return next(action);
    // };
  };
};

export default addressBookHubMiddleware;

// export const setupConnection = () => {
//   debugger;
//   addressBookHub = new signalR.HubConnectionBuilder()
//     .withUrl("/api/addressBookHub")
//     .build();

//   addressBookHub.on("methodOnClient", update => {
//     console.log(update);
//   });
// };

// setupConnection();
