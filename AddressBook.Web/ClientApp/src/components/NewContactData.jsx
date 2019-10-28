import { goBack } from "connected-react-router";
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
      goBack,
      contact,
      cancelContactEditing,
      createContact,
      resetContact,
      isSaving,
      error
    } = this.props;
    if (error.isError) {
      return (
        <div>
          <Error text={error.message} />
          <button
            onClick={() => {
              resetContact();
              goBack();
            }}
          >
            Back
          </button>
        </div>
      );
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

const mapDispatchToProps = dispatch => {
  let actions = bindActionCreators(contactActionCreators, dispatch);
  actions["goBack"] = () => dispatch(goBack());
  return actions;
};

export default withRouter(
  connect(
    mapStateToProps,
    mapDispatchToProps
  )(NewContactData)
);
