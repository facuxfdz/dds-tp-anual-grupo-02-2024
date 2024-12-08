"use client";
import React from "react";
import Grid from "@mui/material/Grid2";
import {MultiSelectElement, SelectElement, TextFieldElement} from "react-hook-form-mui";
import {TipoDocumento} from "@models/enums/tipoDocumento";
import {SexoDocumento} from "@models/enums/sexoDocumento";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {DatePickerElement} from "react-hook-form-mui/date-pickers";
import {useFormContext} from "react-hook-form";
import {useAppSelector} from "@redux/hook";

export const RegistroPersonaFisica = (
    {
        hideEmail = false,
    }: {
        hideEmail: boolean
    }
) => {
    const formContext = useFormContext();
    const contribucionesPreferidas: number[] = formContext.watch("contribucionesPreferidas");
    const user = useAppSelector((state) => state.user);

    return (
        <Grid container spacing={3} alignItems="center">
            <Grid size={6} key={"nombre"}>
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
            <Grid size={6} key={"apellido"}>
                <TextFieldElement
                    name={"apellido"}
                    label={"Apellido"}
                    placeholder={"Ingrese la apellido"}
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor ingrese su apellido",
                        }
                    }
                />
            </Grid>
            {
                !hideEmail &&
                (
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
                )
            }
            <Grid size={6} key={"tipoDocumento"}>
                <SelectElement
                    name={"tipoDocumento"}
                    label={"Tipo de Documento"}
                    options={[
                        {label: "Dni", id: TipoDocumento.DNI},
                        {label: "LE", id: TipoDocumento.LE},
                        {label: "LC", id: TipoDocumento.LC},
                        {label: "Cuil", id: TipoDocumento.CUIL},
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
            <Grid size={6} key={"sexo"}>
                <SelectElement
                    name={"sexo"}
                    label={"Sexo"}
                    options={[
                        {label: "Masculino", id: SexoDocumento.Masculino},
                        {label: "Femenino", id: SexoDocumento.Femenino},
                        {label: "Otro", id: SexoDocumento.Otro}
                    ]}
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor seleccione una opción",
                        }
                    }
                />
            </Grid>
            <Grid size={6} key={"fechaNacimiento"}>
                <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <DatePickerElement
                        label={"Fecha de Nacimiento"}
                        name={"fechaNacimiento"}
                        required={true}
                        rules={
                            {
                                required: "Por favor ingrese su fecha de nacimiento"
                            }
                        }
                        sx={{width: '100%'}}
                    />
                </LocalizationProvider>
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
            {
                user.colaboradorId != "" && (
                    <Grid size={12} key={"contribucionesPreferidas"}>
                        <MultiSelectElement
                            name={"contribucionesPreferidas"}
                            label={"Contribuciones Preferidas"}
                            itemKey="id"
                            itemLabel="name"
                            options={[
                                {
                                    id: 1,
                                    name: 'Donacion Monetaria'
                                },
                                {
                                    id: 2,
                                    name: 'Donacion de Viandas'
                                },
                                {
                                    id: 3,
                                    name: 'Redistribucion de Viandas'
                                },
                                {
                                    id: 4,
                                    name: 'Entrega de Tarjetas de Consumo'
                                }
                            ]}
                            required={true}
                            fullWidth
                            showChips
                            rules={
                                {
                                    required: "Por favor seleccione una opción",
                                }
                            }
                        />
                    </Grid>
                )
            }
            {
                contribucionesPreferidas?.some((contribucion) => contribucion === 3) &&
                (
                    <Grid size={12} key={"codigoTarjeta"}>
                        <TextFieldElement
                            name={"codigoTarjeta"}
                            label={"Código Tarjeta"}
                            placeholder={"Ingrese el código de la tarjeta de contribución"}
                            required={contribucionesPreferidas?.some((contribucion) => contribucion === 3)}
                            fullWidth
                        />
                    </Grid>
                )
            }
        </Grid>
    );
}