import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from "react-redux"
import './index.css';
import App from './App';
import mystore from './redux/mystore';

ReactDOM.render(
  <React.StrictMode>
    <Provider store={mystore}>
      <App />
    </Provider>
  </React.StrictMode>,
  document.getElementById('root')
);
