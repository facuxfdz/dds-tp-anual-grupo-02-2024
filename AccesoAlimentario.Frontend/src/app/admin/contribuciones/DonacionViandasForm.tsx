"use client";
import {Form, FormFieldType, IFormField} from "@components/Forms/Form";
import React from "react";

const fields: IFormField[] = [
    {
        id: "comida",
        label: "Comida",
        type: FormFieldType.TEXT,
        width: 12,
        value: "",
        placeholder: "Ingrese el nombre de la comida",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese el nombre de la comida",
        options: []
    },
    {
        id: "calorias",
        label: "Calorias",
        type: FormFieldType.NUMBER,
        width: 12,
        value: "",
        placeholder: "Ingrese la cantidad de calorias",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese la cantidad de calorias",
        options: []
    },
    {
        id: "peso",
        label: "Peso",
        type: FormFieldType.NUMBER,
        width: 12,
        value: "",
        placeholder: "Ingrese el peso de la comida",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese el peso de la comida",
        options: []
    },
    {
        id: "fechaCaducidad",
        label: "Fecha de caducidad",
        type: FormFieldType.DATE,
        width: 12,
        value: "",
        placeholder: "Ingrese la fecha de caducidad",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese la fecha de caducidad",
        options: []
    },
    {
        id: "heladera",
        label: "Heladera",
        type: FormFieldType.SELECT,
        width: 12,
        value: "",
        placeholder: "Seleccione una opción",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor seleccione una opción",
        options: ["Heladera 1", "Heladera 2", "Heladera 3"]
    }
];

export const DonacionViandasForm = () => {
    return (
        <Form fields={fields}/>
    );
}