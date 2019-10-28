import { routerMiddleware } from "connected-react-router";
import { createBrowserHistory } from "history";
import { applyMiddleware, compose, createStore } from "redux";
import logger from "redux-logger";
import thunk from "redux-thunk";
import reducers from "../reducers";

export const history = createBrowserHistory();

export default () => {
  const middleware = [];
  middleware.push(routerMiddleware(history));
  middleware.push(thunk);
  middleware.push(logger);
  const store = createStore(
    reducers(history),
    compose(applyMiddleware(...middleware))
  );
  return store;
};
