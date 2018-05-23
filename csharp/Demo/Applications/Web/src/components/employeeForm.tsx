import * as React from 'react';
import { IEmployee } from '../types';
import InputBox from './common/inputBox';

export interface IEmployeeForm {
    employee: IEmployee;
}

export default class EmployeeForm extends React.Component<IEmployeeForm, object> {
    public render() {
        const {
            employeeEmail,
            employeeFirstName,
            employeeLastName
        } = this.props.employee;

        return (
            <div className="employeeForm">
                <InputBox maxLength={300} name="employeeFirstName" placeholder="First name" type="text" value={employeeFirstName} />
                <InputBox maxLength={300} name="employeeLastName" placeholder="Last name" type="text" value={employeeLastName} />
                <InputBox maxLength={300} name="employeeEmail" placeholder="Email address" type="email" value={employeeEmail} />
            </div>
        );
    }
}
