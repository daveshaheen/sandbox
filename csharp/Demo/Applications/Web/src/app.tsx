import * as React from 'react';
import EmployeeForm from './components/employeeForm';

import './app.css';
import { IEmployee } from './types';

class App extends React.Component {
  public render() {
    return (
      <div className="app">
        <header className="app-header">
          <h1 className="app-title">Demo</h1>
        </header>
        <main>
          <p className="app-intro">
            Pay
          </p>
          <EmployeeForm employee={{} as IEmployee} />
        </main>
      </div>
    );
  }
}

export default App;
