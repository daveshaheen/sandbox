import * as React from 'react';

import './inputBox.css';

export interface IInputBox {
    maxLength: number;
    name: string;
    placeholder: string;
    type: string;
    value: string;
    valueChanged?: (value: string) => void;
}

export default class InputBox extends React.Component<IInputBox, object> {
    public render() {
        const {
            maxLength = 300,
            name,
            placeholder,
            type,
            value
        } = this.props;

        if (value && value.length > maxLength) {
            throw new Error({ name } + ' must be less than ' + { maxLength } + ' characters.');
        }

        return (
            <div className="inputBox">
                <input type={type} placeholder={placeholder} name={name} />
            </div>
        );
    }
}
