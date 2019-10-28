import React from "react";
import { Link } from "react-router-dom";

const Paging = ({ currentPage, hasItems }) => {
  return (
    <div>
      {currentPage > 1 && (
        <Link to={`/Contacts/${parseInt(currentPage) - 1}`}>Prev</Link>
      )}
      {hasItems === true && (
        <Link to={`/Contacts/${parseInt(currentPage) + 1}`}>Next</Link>
      )}
    </div>
  );
};

export default Paging;
