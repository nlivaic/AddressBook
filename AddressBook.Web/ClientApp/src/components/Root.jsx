import { ConnectedRouter } from "connected-react-router";
import React from "react";
import { Provider } from "react-redux";
import { Route, Switch } from "react-router-dom";
import ContactData from "./ContactData";
import ContactsList from "./ContactsList";
import NewContact from "./NewContact";
import NewContactData from "./NewContactData";

const Root = ({ history, store }) => {
  return (
    <Provider store={store}>
      <ConnectedRouter history={history}>
        <NewContact />
        <hr />
        <Switch>
          <Route component={ContactsList} exact path="/" />
          <Route component={NewContactData} exact path="/NewContact" />
          <Route component={ContactData} exact path="/Contact/:id" />
        </Switch>
      </ConnectedRouter>
    </Provider>
  );
};

export default Root;
