"use client";
import React from "react";
import {SelectElement, TextFieldElement} from "react-hook-form-mui";
import Grid from "@mui/material/Grid2";
import {TipoDocumento} from "@models/enums/tipoDocumento";
import {TipoJuridica} from "@models/enums/tipoJuridica";

export const RegistroPersonaJuridica = () => {
    return (
        <Grid container spacing={3} alignItems="center">
            <Grid size={12} key={"nombre"}>
                <TextFieldElement
                    name={"nombre"}
                    label={"Nombre"}
                    placeholder={"Ingrese su nombre"}
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor ingrese su nombre"
                        }
                    }
                />
            </Grid>
            <Grid size={12} key={"email"}>
                <TextFieldElement
                    name={"email"}
                    label={"Email"}
                    placeholder={"Ingrese la dirección de correo electrónico"}
                    required={true}
                    fullWidth
                    type={"email"}
                    rules={
                        {
                            required: "Por favor ingrese la dirección de correo electrónico",
                        }
                    }
                />
            </Grid>
            <Grid size={6} key={"tipoJuridico"}>
                <SelectElement
                    name={"tipoJuridico"}
                    label={"Tipo Jurídico"}
                    options={[
                        {label: "Gubernamental", id: TipoJuridica.Gubernamental},
                        {label: "ONG", id: TipoJuridica.Ong},
                        {label: "Empresa", id: TipoJuridica.Empresa},
                        {label: "Institucion", id: TipoJuridica.Institucion},
                    ]}
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor seleccione una opción"
                        }
                    }
                />
            </Grid>
            <Grid size={6} key={"rubro"}>
                <TextFieldElement
                    name={"rubro"}
                    label={"Rubro"}
                    placeholder={"Ingrese su rubro"}
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor ingrese su rubro"
                        }
                    }
                />
            </Grid>
            <Grid size={12} key={"razonSocial"}>
                <TextFieldElement
                    name={"razonSocial"}
                    label={"Razón Social"}
                    placeholder={"Ingrese su razón social"}
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor ingrese su razón social"
                        }
                    }
                />
            </Grid>
            <Grid size={6} key={"tipoDocumento"}>
                <SelectElement
                    name={"tipoDocumento"}
                    label={"Tipo de Documento"}
                    options={[
                        {label: "Cuit", id: TipoDocumento.CUIT},
                    ]}
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor seleccione una opción"
                        }
                    }
                />
            </Grid>
            <Grid size={6} key={"numeroDocumento"}>
                <TextFieldElement
                    name={"numeroDocumento"}
                    label={"Número de Documento"}
                    placeholder={"Ingrese su número de documento"}
                    required={true}
                    fullWidth
                    type="number"
                    rules={
                        {
                            pattern: {
                                value: /\d{8,11}/,
                                message: "Por favor ingrese un número de documento válido"
                            },
                            required: "Por favor ingrese su número de documento"
                        }
                    }
                />
            </Grid>
            <Grid size={6} key={"calle"}>
                <TextFieldElement
                    name={"calle"}
                    label={"Calle"}
                    placeholder={"Ingrese la calle"}
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor ingrese su calle",
                        }
                    }
                />
            </Grid>
            <Grid size={6} key={"numero"}>
                <TextFieldElement
                    name={"numero"}
                    label={"Número"}
                    placeholder={"Ingrese su número"}
                    required={true}
                    fullWidth
                    type="number"
                    rules={
                        {
                            required: "Por favor ingrese su número",
                        }
                    }
                />
            </Grid>
            <Grid size={4} key={"localidad"}>
                <TextFieldElement
                    name={"localidad"}
                    label={"Localidad"}
                    placeholder={"Ingrese la localidad"}
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor ingrese su localidad",
                        }
                    }
                />
            </Grid>
            <Grid size={2} key={"piso"}>
                <TextFieldElement
                    name={"piso"}
                    label={"Piso"}
                    placeholder={"Ingrese su piso"}
                    fullWidth
                />
            </Grid>
            <Grid size={4} key={"departamento"}>
                <TextFieldElement
                    name={"departamento"}
                    label={"Departamento"}
                    placeholder={"Ingrese su departamento"}
                    fullWidth
                />
            </Grid>
            <Grid size={2} key={"codigoPostal"}>
                <TextFieldElement
                    name={"codigoPostal"}
                    label={"Código Postal"}
                    placeholder={"Ingrese su código postal"}
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor ingrese su código postal"
                        }
                    }
                />
            </Grid>
        </Grid>
    );
}