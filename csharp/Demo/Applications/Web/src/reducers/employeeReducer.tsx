import { InputAction } from '../actions';
import { EMPLOYEE_FIRSTNAME_CHANGED } from '../constants';
import { IStateStore } from '../types';

const INITIAL_STATE: IStateStore = {
    employee: {
        email: '',
        firstName: '',
        lastName: ''
    }
};

export default function employeeReducer(state: IStateStore = INITIAL_STATE, action: InputAction): IStateStore {
    switch (action.type) {
        case EMPLOYEE_FIRSTNAME_CHANGED:
            return { ...state };
        default:
            return state;
    }
}
