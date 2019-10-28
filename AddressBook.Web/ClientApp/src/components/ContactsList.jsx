import React, { Component } from "react";
import { withRouter, Link } from "react-router-dom";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { actionCreators } from "../reducers/Contacts";
import { getAllContacts, getContactsIsLoading } from "../reducers";
import Paging from "./Paging";

class ContactsList extends Component {
  componentDidMount() {
    const { pageNr = 1, requestContacts } = this.props;
    requestContacts(pageNr);
  }

  componentDidUpdate(prevProps, prevState) {
    if (this.props.pageNr !== prevProps.pageNr) {
      const { pageNr = 1, requestContacts } = this.props;
      requestContacts(pageNr);
    }
  }

  render() {
    const { pageNr = 1, isLoading, contactsList } = this.props;
    if (isLoading) return <p>Loading...</p>;
    return (
      <div>
        {contactsList.map(c => (
          <span>
            <Link key={c.id} to={`/Contact/${c.id}`}>
              {c.name}
            </Link>
            <br />
          </span>
        ))}
        <br />
        <Paging
          currentPage={pageNr}
          hasItems={contactsList.length > 0}
        ></Paging>
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
