"use client";
import {Form, FormFieldType, IFormField} from "@components/Forms/Form";
import React from "react";

const fields: IFormField[] = [
    {
        id: "motivo",
        label: "Motivo",
        type: FormFieldType.TEXT,
        width: 12,
        value: "",
        placeholder: "Ingrese un motivo",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese un motivo",
        options: []
    },
    {
        id: "heladeraOrigen",
        label: "Heladera Origen",
        type: FormFieldType.SELECT,
        width: 12,
        value: "",
        placeholder: "Seleccione una opción",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor seleccione una opción",
        options: ["Heladera 1", "Heladera 2", "Heladera 3"]
    },
    {
        id: "helaeraDestino",
        label: "Heladera Destino",
        type: FormFieldType.SELECT,
        width: 12,
        value: "",
        placeholder: "Seleccione una opción",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor seleccione una opción",
        options: ["Heladera 1", "Heladera 2", "Heladera 3"]
    },
    {
        id: "cantidad",
        label: "Cantidad de viandas",
        type: FormFieldType.NUMBER,
        width: 12,
        value: "",
        placeholder: "Ingrese una cantidad",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese una cantidad",
        options: []
    }
];

export const DistribucionViandasForm = () => {
    return (
        <Form fields={fields}/>
    );
}