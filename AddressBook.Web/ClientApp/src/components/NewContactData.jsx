import React, { Component } from "react";
import { connect } from "react-redux";
import { withRouter } from "react-router-dom";
import { bindActionCreators } from "redux";
import { getContact, getIsRequestingSaveContact } from "../reducers";
import { actionCreators as contactActionCreators } from "../reducers/Contact";
import ContactEdit from "./ContactEdit";

class NewContactData extends Component {
  render() {
    const {
      contact,
      cancelContactEditing,
      createContact,
      isSaving
    } = this.props;
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
    isSaving: getIsRequestingSaveContact(state)
  };
};

export default withRouter(
  connect(
    mapStateToProps,
    dispatch => bindActionCreators(contactActionCreators, dispatch)
  )(NewContactData)
);
