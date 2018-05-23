export type InputAction = ITextChanged;

export interface ITextChanged {
    type: any;
    value: string;
}

export function onTextChange(type: any, value: string): ITextChanged {
    return {
        type,
        value
    };
}
