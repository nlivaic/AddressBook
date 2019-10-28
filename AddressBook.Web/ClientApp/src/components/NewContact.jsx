import { push } from "connected-react-router";
import React from "react";
import { connect } from "react-redux";

const NewContact = ({ push }) => {
  return <button onClick={push}>New Contact</button>;
};

const mapDispatchToProps = dispatch => {
  return {
    push: () => {
      dispatch(push("/NewContact"));
    }
  };
};

export default connect(
  null,
  mapDispatchToProps
)(NewContact);
