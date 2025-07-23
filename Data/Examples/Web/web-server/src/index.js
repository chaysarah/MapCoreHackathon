import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import 'typeface-roboto';
import ApplicationState from './context/ApplicationState';
import SideNotification from './components/SideNtification/SideNotification';
import store from './redux/store';
import { Provider } from "react-redux";

ReactDOM.render(
  <Provider store={store}>

    <ApplicationState>
      <App />
      <SideNotification />
    </ApplicationState>
   </Provider>

  , document.getElementById('root'));
