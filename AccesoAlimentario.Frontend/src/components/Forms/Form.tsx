"use client";
import React from "react";
import Grid from "@mui/material/Grid2";
import {
    CheckboxButtonGroup, Controller, MultiSelectElement,
    RadioButtonGroup,
    SelectElement,
    TextFieldElement,
} from "react-hook-form-mui";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {DatePickerElement, TimePickerElement} from "react-hook-form-mui/date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {MuiFileInput} from 'mui-file-input';
import {InputAdornment} from "@mui/material";

export const Form = ({
                         fields,
                     }: {
    fields: IFormField[];
}) => {
    return (
        <Grid container spacing={3} alignItems="center">
            {fields
                .map((field: IFormField) => (
                    GetFieldComponent({field})
                ))}
        </Grid>
    );
};

export type FormFieldValue = {
    [key: string]: string;
};

export interface IFormField {
    id: string;
    label: string;
    type: FormFieldType;
    width: 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12;
    value: string;
    placeholder: string;
    isRequired: boolean;
    regex: string;
    errorMessage: string;
    options: string[];
    icon?: string;
}

export enum FormFieldType {
    TEXT = 0,
    CHECKBOX = 1,
    RADIO = 2,
    SELECT = 3,
    MULTISELECT = 4,
    NUMBER = 5,
    DATE = 6,
    TIME = 7,
    EMAIL = 8,
    IMAGE = 9,
    FILE = 10,
    TEL = 11
}

const TextInputField = (field: IFormField) => {
    return (
        <Grid size={field.width} key={field.id}>
            <TextFieldElement
                name={field.id}
                label={field.label}
                placeholder={field.placeholder}
                value={field.value}
                required={field.isRequired}
                fullWidth
                rules={
                    {
                        pattern: {
                            value: new RegExp(field.regex),
                            message: field.errorMessage
                        },
                        required: field.isRequired ? field.errorMessage : undefined
                    }
                }
                slotProps={{
                    input: {
                        startAdornment: field.icon ? (
                            <InputAdornment position="start">
                                <i className={field.icon}/>
                            </InputAdornment>
                        ) : null
                    }
                }}
            />
        </Grid>
    );
}

const DateInputField = (field: IFormField) => {
    return (
        <Grid size={field.width} key={field.id}>
            <LocalizationProvider dateAdapter={AdapterDateFns}>
                <DatePickerElement
                    label={field.label}
                    name={field.id}
                    required={field.isRequired}
                    rules={
                        {
                            pattern: {
                                value: new RegExp(field.regex),
                                message: field.errorMessage
                            },
                            required: field.isRequired ? field.errorMessage : undefined
                        }
                    }
                    sx={{width: '100%'}}
                />
            </LocalizationProvider>
        </Grid>
    );
}

const CheckBoxInputField = (field: IFormField) => {
    return (
        <Grid size={field.width} key={field.id}>
            <CheckboxButtonGroup
                name={field.id}
                label={field.label}
                options={field.options.map((option: string) => ({label: option, id: option}))}
                required={field.isRequired}
                row
                rules={{
                    required: field.isRequired ? field.errorMessage : undefined
                }}
            />
        </Grid>
    );
}

const RadioButtonInputField = (field: IFormField) => {
    return (
        <Grid size={field.width} key={field.id}>
            <RadioButtonGroup
                name={field.id}
                label={field.label}
                options={field.options.map((option: string) => ({label: option, id: option}))}
                required={field.isRequired}
                row
                rules={{
                    required: field.isRequired ? field.errorMessage : undefined
                }}
            />
        </Grid>
    );
}

const SelectInputField = (field: IFormField) => {
    return (
        <Grid size={field.width} key={field.id}>
            <SelectElement
                name={field.id}
                label={field.label}
                options={field.options.map((option: string) => ({label: option, id: option}))}
                required={field.isRequired}
                fullWidth
                rules={
                    {
                        pattern: {
                            value: new RegExp(field.regex),
                            message: field.errorMessage
                        },
                        required: field.isRequired ? field.errorMessage : undefined
                    }
                }
                slotProps={{
                    input: {
                        startAdornment: field.icon ? (
                            <InputAdornment position="start">
                                <i className={field.icon}/>
                            </InputAdornment>
                        ) : null
                    }
                }}
            />
        </Grid>
    );
}

const MultiSelectInputField = (field: IFormField) => {
    return (
        <Grid size={field.width} key={field.id}>
            <MultiSelectElement
                name={field.id}
                label={field.label}
                options={field.options.map((option: string) => ({label: option, id: option}))}
                required={field.isRequired}
                fullWidth
                rules={
                    {
                        pattern: {
                            value: new RegExp(field.regex),
                            message: field.errorMessage
                        },
                        required: field.isRequired ? field.errorMessage : undefined
                    }
                }
            />
        </Grid>
    );
}

const NumberInputField = (field: IFormField) => {
    return (
        <Grid size={field.width} key={field.id}>
            <TextFieldElement
                name={field.id}
                label={field.label}
                placeholder={field.placeholder}
                value={field.value}
                required={field.isRequired}
                fullWidth
                type="number"
                rules={
                    {
                        pattern: {
                            value: new RegExp(field.regex),
                            message: field.errorMessage
                        },
                        required: field.isRequired ? field.errorMessage : undefined
                    }
                }
                slotProps={{
                    input: {
                        startAdornment: field.icon ? (
                            <InputAdornment position="start">
                                <i className={field.icon}/>
                            </InputAdornment>
                        ) : null
                    }
                }}
            />
        </Grid>
    );
}

const TimeInputField = (field: IFormField) => {
    return (
        <Grid size={field.width} key={field.id}>
            <LocalizationProvider dateAdapter={AdapterDateFns}>
                <TimePickerElement
                    label={field.label}
                    name={field.id}
                    required={field.isRequired}
                    rules={
                        {
                            pattern: {
                                value: new RegExp(field.regex),
                                message: field.errorMessage
                            },
                            required: field.isRequired ? field.errorMessage : undefined
                        }
                    }
                    sx={{width: '100%'}}
                />
            </LocalizationProvider>
        </Grid>
    );
}

const EmailInputField = (field: IFormField) => {
    return (
        <Grid size={field.width} key={field.id}>
            <TextFieldElement
                name={field.id}
                label={field.label}
                placeholder={field.placeholder}
                value={field.value}
                required={field.isRequired}
                fullWidth
                type="email"
                rules={
                    {
                        pattern: {
                            value: new RegExp(field.regex),
                            message: field.errorMessage
                        },
                        required: field.isRequired ? field.errorMessage : undefined
                    }
                }
                slotProps={{
                    input: {
                        startAdornment: field.icon ? (
                            <InputAdornment position="start">
                                <i className={field.icon}/>
                            </InputAdornment>
                        ) : null
                    }
                }}
            />
        </Grid>
    );
}

const ImageInputField = (field: IFormField) => {
    return (
        <Grid size={field.width} key={field.id}>
            <Controller
                name={field.id}
                rules={{required: field.errorMessage}}
                render={({field: {onChange, value}, fieldState: {error}}) => (
                    <MuiFileInput
                        label={field.label}
                        placeholder={field.placeholder}
                        value={value}
                        onChange={onChange}
                        required={field.isRequired}
                        error={!!error}
                        helperText={error ? field.errorMessage : ""}
                        fullWidth
                        clearIconButtonProps={{
                            title: "Remove",
                            children: <i className="fa-duotone fa-solid fa-times"/>
                        }}
                        InputProps={{
                            inputProps: {
                                accept: '.png, .jpeg',
                            },
                            startAdornment: field.icon ? (
                                <i className={field.icon}/>
                            ) : null,
                        }}
                    />
                )}
            />
        </Grid>
    );
}

const FileInputField = (field: IFormField) => {
    return (
        <Grid size={field.width} key={field.id}>
            <Controller
                name={field.id}
                rules={{required: field.errorMessage}}
                render={({field: {onChange, value}, fieldState: {error}}) => (
                    <MuiFileInput
                        label={field.label}
                        placeholder={field.placeholder}
                        value={value}
                        onChange={onChange}
                        required={field.isRequired}
                        error={!!error}
                        helperText={error ? field.errorMessage : ""}
                        fullWidth
                        clearIconButtonProps={{
                            title: "Remove",
                            children: <i className="fa-duotone fa-solid fa-times"/>
                        }}
                        InputProps={{
                            startAdornment: field.icon ? (
                                <i className={field.icon}/>
                            ) : null
                        }}
                    />
                )}
            />
        </Grid>
    );
}

const TelInputField = (field: IFormField) => {
    return (
        <Grid size={field.width} key={field.id}>
            <TextFieldElement
                name={field.id}
                label={field.label}
                placeholder={field.placeholder}
                value={field.value}
                required={field.isRequired}
                fullWidth
                type="tel"
                rules={
                    {
                        pattern: {
                            value: new RegExp(field.regex),
                            message: field.errorMessage
                        },
                        required: field.errorMessage
                    }
                }
            />
        </Grid>
    );
}

const GetFieldComponent = ({field}: { field: IFormField }) => {
    switch (field.type) {
        case FormFieldType.TEXT:
            return TextInputField(field);
        case FormFieldType.CHECKBOX:
            return CheckBoxInputField(field);
        case FormFieldType.RADIO:
            return RadioButtonInputField(field);
        case FormFieldType.SELECT:
            return SelectInputField(field);
        case FormFieldType.MULTISELECT:
            return MultiSelectInputField(field);
        case FormFieldType.NUMBER:
            return NumberInputField(field);
        case FormFieldType.DATE:
            return DateInputField(field);
        case FormFieldType.TIME:
            return TimeInputField(field);
        case FormFieldType.EMAIL:
            return EmailInputField(field);
        case FormFieldType.IMAGE:
            return ImageInputField(field);
        case FormFieldType.FILE:
            return FileInputField(field);
        case FormFieldType.TEL:
            return TelInputField(field);
        default:
            return TextInputField(field);
    }
}