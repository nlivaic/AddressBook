import { goBack } from "connected-react-router";
import React, { Component } from "react";
import { connect } from "react-redux";
import { withRouter } from "react-router-dom";
import { bindActionCreators } from "redux";
import {
  getContact,
  getContactError,
  getContactIsDeleting,
  getContactIsEditing,
  getContactIsLoading,
  getContactIsNotFound,
  getIsRequestingSaveContact
} from "../reducers";
import { actionCreators as contactActionCreators } from "../reducers/Contact";
import Contact from "./Contact";
import ContactEdit from "./ContactEdit";
import Error from "./Error";
import NotFound from "./NotFound";

class ContactData extends Component {
  componentDidMount() {
    const { id, requestContact } = this.props;
    requestContact(id);
  }

  componentWillUnmount() {
    const { resetContact } = this.props;
    resetContact();
  }

  render() {
    const {
      contact,
      cancelContactEditing,
      deleteContact,
      editContact,
      error,
      goBack,
      id,
      isDeleting,
      isEditing,
      isLoading,
      isNotFound,
      isSaving,
      updateContact,
      readContact
    } = this.props;
    if (isLoading) {
      return <p>Loading...</p>;
    }
    if (isNotFound) {
      return <NotFound />;
    }
    if (error.isError) {
      return <Error text={error.message} />;
    }
    return (
      <div>
        {!isEditing && <Contact contact={contact} editContact={editContact} />}
        {isEditing && (
          <ContactEdit
            contact={contact}
            cancelContactChanges={cancelContactEditing}
            id={id}
            isSaving={isSaving}
            saveContact={updateContact}
            showCancel={true}
          />
        )}
        {!isDeleting && (
          <button onClick={() => deleteContact(id)}>Delete</button>
        )}
        {isDeleting && <span>Deleting...</span>}
        <br />
        <button
          onClick={() => {
            readContact();
            goBack();
          }}
        >
          Back
        </button>
      </div>
    );
  }
}

const mapStateToProps = (state, ownProps) => {
  return {
    contact: getContact(state),
    error: getContactError(state),
    id: ownProps.match.params.id,
    isDeleting: getContactIsDeleting(state),
    isEditing: getContactIsEditing(state),
    isLoading: getContactIsLoading(state),
    isNotFound: getContactIsNotFound(state),
    isSaving: getIsRequestingSaveContact(state)
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
  )(ContactData)
);
