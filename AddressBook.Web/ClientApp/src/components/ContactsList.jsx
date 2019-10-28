import React, { Component } from "react";
import { withRouter, Link } from "react-router-dom";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { actionCreators } from "../reducers/Contacts";
import { getAllContacts, getContactsIsLoading } from "../reducers";

class ContactsList extends Component {
  componentDidMount() {
    const { pageNr, requestContacts } = this.props;
    requestContacts(pageNr === undefined ? 1 : pageNr);
  }

  render() {
    const { pageNr, isLoading, contactsList } = this.props;
    if (isLoading) return <p>Loading...</p>;
    return (
      <div>
        {contactsList.map(c => (
          <Link key={c.id} to={`/Contact/${c.id}`}>
            {c.name}
          </Link>
        ))}
      </div>
    );
  }
}

const mapStateToProps = (state, ownProps) => {
  return {
    pageNr: ownProps.match.params.pageNr,
    contactsList: getAllContacts(state),
    isLoading: getContactsIsLoading(state)
  };
};

export default withRouter(
  connect(
    mapStateToProps,
    dispatch => bindActionCreators(actionCreators, dispatch)
  )(ContactsList)
);
