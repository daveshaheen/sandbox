import { connect, Dispatch } from 'react-redux';
import * as actions from '../actions/';
import InputBox from '../components/common/inputBox';
import { IEmployee, IStateStore } from '../types/index';

export function mapStateToProps(employee: IEmployee): IStateStore {
    const { employeeEmail, employeeFirstName, employeeLastName } = employee;

    return {
        employee: {
            employeeEmail,
            employeeFirstName,
            employeeLastName,
        }
    };
}

export function mapDispatchToProps(dispatch: Dispatch<actions.EmployeeAction>) {
    return {
        onChange: (employee: IEmployee) => dispatch(actions.saveEmployee(employee))
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(InputBox);
