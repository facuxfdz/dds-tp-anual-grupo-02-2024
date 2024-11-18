import {Form, FormFieldType, IFormField} from "@components/Forms/Form";
import React from "react";

const fields: IFormField[] = [
    {
        id: "monto",
        label: "Monto de la donación",
        type: FormFieldType.NUMBER,
        width: 12,
        value: "",
        placeholder: "Ingrese un monto",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese un monto",
        options: []
    },
    {
        id: "frecuencia",
        label: "Frecuencia de la donación",
        type: FormFieldType.NUMBER,
        width: 12,
        value: "",
        placeholder: "Ingrese una frecuencia",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese una frecuencia",
        options: []
    }
];

export const DonacionMonetariaForm = () => {
    return (
        <Form fields={fields}/>
    );
}