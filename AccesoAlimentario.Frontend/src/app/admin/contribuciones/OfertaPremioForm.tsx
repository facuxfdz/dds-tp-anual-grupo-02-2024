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
        placeholder: "Ingrese el nombre de su premio",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese el nombre de su premio",
        options: []
    },
    {
        id: "descripcion",
        label: "Descripción",
        type: FormFieldType.TEXT,
        width: 12,
        value: "",
        placeholder: "Ingrese una descripción",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese una descripción",
        options: []
    },
    {
        id: "puntosNecesarios",
        label: "Puntos Necesarios",
        type: FormFieldType.NUMBER,
        width: 12,
        value: "",
        placeholder: "Ingrese la cantidad de puntos necesarios",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese la cantidad de puntos necesarios",
        options: []
    },
    {
        id: "imagen",
        label: "Imagen",
        type: FormFieldType.IMAGE,
        width: 12,
        value: "",
        placeholder: "Seleccione una imagen",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor seleccione una imagen",
        options: []
    },
    {
        id: "categoria",
        label: "Categoría",
        type: FormFieldType.SELECT,
        width: 12,
        value: "",
        placeholder: "Seleccione una opción",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor seleccione una opción",
        options: ["Gastronomia", "Electronica", "ArticulosHogar", "Otros"]
    }
];

export const OfertaPremioForm = () => {
    return (
        <Form fields={fields}/>
    );
}