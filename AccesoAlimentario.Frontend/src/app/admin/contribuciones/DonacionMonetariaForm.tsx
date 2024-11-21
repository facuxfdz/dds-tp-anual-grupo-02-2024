"use client";
import {Form, FormFieldType, IFormField} from "@components/Forms/Form";
import React from "react";

const fields: IFormField[] = [
    {
        id: "fechaContribucion",
        label: "Fecha de la donación",
        type: FormFieldType.DATE,
        width: 12,
        value: "",
        placeholder: "Ingrese una fecha",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese una fecha",
        options: []
    },
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
        id: "frecuenciaDias",
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