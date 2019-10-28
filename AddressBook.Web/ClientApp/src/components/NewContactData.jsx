import React, { Component } from "react";
import { connect } from "react-redux";
import { withRouter } from "react-router-dom";
import { bindActionCreators } from "redux";
import Error from "./Error";
import {
  getContact,
  getContactError,
  getIsRequestingSaveContact
} from "../reducers";
import { actionCreators as contactActionCreators } from "../reducers/Contact";
import ContactEdit from "./ContactEdit";

class NewContactData extends Component {
  render() {
    const {
      contact,
      cancelContactEditing,
      createContact,
      isSaving,
      error
    } = this.props;
    if (error.isError) {
      return <Error text={error.message} />;
    }
    return (
      <ContactEdit
        contact={contact}
        cancelContactChanges={cancelContactEditing}
        isSaving={isSaving}
        saveContact={createContact}
      />
    );
  }
}

export const mapStateToProps = state => {
  return {
    contact: getContact(state),
    isSaving: getIsRequestingSaveContact(state),
    error: getContactError(state)
  };
};

export default withRouter(
  connect(
    mapStateToProps,
    dispatch => bindActionCreators(contactActionCreators, dispatch)
  )(NewContactData)
);
