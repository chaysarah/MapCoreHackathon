import { applyMiddleware, createStore } from "redux";
import rootReducer from "./combineReducer";
import confirmMiddleware from "./middlewares"

const store = createStore(rootReducer,
    applyMiddleware(confirmMiddleware)
    );


export default store;
