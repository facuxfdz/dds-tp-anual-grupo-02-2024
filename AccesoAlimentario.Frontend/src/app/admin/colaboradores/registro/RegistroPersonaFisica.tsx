"use client";
import {Form, FormFieldType, IFormField} from "@components/Forms/Form";
import React from "react";

const fields: IFormField[] = [
    {
        id: "nombre",
        label: "Nombre",
        type: FormFieldType.TEXT,
        width: 12,
        value: "",
        placeholder: "Ingrese su nombre",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese su nombre",
        options: []
    },
    {
        id: "apellido",
        label: "Apellido",
        type: FormFieldType.TEXT,
        width: 12,
        value: "",
        placeholder: "Ingrese su apellido",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese su apellido",
        options: []
    },
    {
        id: "tipoDocumento",
        label: "Tipo de Documento",
        type: FormFieldType.SELECT,
        width: 6,
        value: "DNI",
        placeholder: "Seleccione una opción",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor seleccione una opción",
        options: ["DNI", "LE", "LC", "CUIT", "CUIL"]
    },
    {
        id: "numeroDocumento",
        label: "Número de Documento",
        type: FormFieldType.NUMBER,
        width: 6,
        value: "",
        placeholder: "Ingrese su número de documento",
        isRequired: true,
        regex: "\\d{8,11}",
        errorMessage: "Por favor ingrese su número de documento",
        options: []
    },
    {
        id: "calle",
        label: "Calle",
        type: FormFieldType.TEXT,
        width: 6,
        value: "",
        placeholder: "Ingrese su calle",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese su calle",
        options: []
    },
    {
        id: "numero",
        label: "Número",
        type: FormFieldType.NUMBER,
        width: 6,
        value: "",
        placeholder: "Ingrese su número",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese su número",
        options: []
    },
    {
        id: "localidad",
        label: "Localidad",
        type: FormFieldType.TEXT,
        width: 4,
        value: "",
        placeholder: "Ingrese su localidad",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese su localidad",
        options: []
    },
    {
        id: "piso",
        label: "Piso",
        type: FormFieldType.NUMBER,
        width: 2,
        value: "",
        placeholder: "Ingrese su piso",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese su piso",
        options: []
    },
    {
        id: "departamento",
        label: "Departamento",
        type: FormFieldType.TEXT,
        width: 4,
        value: "",
        placeholder: "Ingrese su departamento",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese su departamento",
        options: []
    },
    {
        id: "codigoPostal",
        label: "Código Postal",
        type: FormFieldType.NUMBER,
        width: 2,
        value: "",
        placeholder: "Ingrese su código postal",
        isRequired: true,
        regex: "\\d{4,8}",
        errorMessage: "Por favor ingrese su código postal",
        options: []
    },
    {
        id: "sexo",
        label: "Sexo",
        type: FormFieldType.SELECT,
        width: 12,
        value: "",
        placeholder: "Seleccione una opción",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor seleccione una opción",
        options: ["Masculino", "Femenino", "Otro"]
    },
]

export const RegistroPersonaFisica = () => {
    return (
        <Form fields={fields}/>
    );
}