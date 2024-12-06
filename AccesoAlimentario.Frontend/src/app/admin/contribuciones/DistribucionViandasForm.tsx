"use client";
import React from "react";
import Grid from "@mui/material/Grid2";
import {SelectElement, TextFieldElement} from "react-hook-form-mui";
import {useGetHeladerasQuery} from "@redux/services/heladerasApi";
import {CircularProgress} from "@mui/material";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {DatePickerElement} from "react-hook-form-mui/date-pickers";
import {useTheme} from "@mui/material/styles";

export const DistribucionViandasForm = ({
                                            onlyView = false,
                                        }: {
    onlyView?: boolean
}) => {
    const theme = useTheme();
    const {data, isLoading} = useGetHeladerasQuery();

    if (isLoading) {
        return (
            <Grid container spacing={3} alignItems="center" textAlign={"center"}>
                <Grid size={12}>
                    <CircularProgress/>
                </Grid>
            </Grid>
        );
    }

    return (
        <Grid container spacing={3} alignItems="center">
            <Grid size={12} key={"fechaContribucion"}>
                <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <DatePickerElement
                        label={"Fecha de la donación"}
                        name={"fechaContribucion"}
                        required={true}
                        rules={
                            {
                                required: "Por favor ingrese una fecha"
                            }
                        }
                        disabled={onlyView}
                        sx={{
                            width: '100%',
                            "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: theme.palette.text.primary,
                            },
                            "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                                color: theme.palette.text.secondary,
                            },
                        }}
                    />
                </LocalizationProvider>
            </Grid>
            <Grid size={6} key={"motivo"}>
                <SelectElement
                    name={"motivo"}
                    label={"Motivo"}
                    options={
                        ([
                            {
                                id: 1,
                                nombre: "Desperfecto"
                            },
                            {
                                id: 2,
                                nombre: "FaltaDeViandas"
                            }
                        ]).map(motivo => {
                            return {
                                label: motivo.nombre,
                                id: motivo.id
                            }
                        })
                    }
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor seleccione una opción"
                        }
                    }
                    disabled={onlyView}
                    sx={{
                        "& .MuiInputBase-input.Mui-disabled": {
                            WebkitTextFillColor: theme.palette.text.primary,
                        },
                        "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                            color: theme.palette.text.secondary,
                        },
                    }}
                />
            </Grid>
            <Grid size={6} key={"cantidadDeViandas"}>
                <TextFieldElement
                    name={"cantidadDeViandas"}
                    label={"Cantidad de viandas"}
                    placeholder={"Ingrese una cantidad"}
                    required={true}
                    fullWidth
                    type="number"
                    rules={
                        {
                            required: "Por favor ingrese una cantidad"
                        }
                    }
                    disabled={onlyView}
                    sx={{
                        "& .MuiInputBase-input.Mui-disabled": {
                            WebkitTextFillColor: theme.palette.text.primary,
                        },
                        "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                            color: theme.palette.text.secondary,
                        },
                    }}
                />
            </Grid>
            <Grid size={12} key={"heladeraOrigenId"}>
                <SelectElement
                    name={"heladeraOrigenId"}
                    label={"Heladera Origen"}
                    options={
                        (data ?? []).map(heladera => {
                            return {
                                label: heladera.puntoEstrategico.nombre,
                                id: heladera.id
                            }
                        })
                    }
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor seleccione una opción"
                        }
                    }
                    disabled={onlyView}
                    sx={{
                        "& .MuiInputBase-input.Mui-disabled": {
                            WebkitTextFillColor: theme.palette.text.primary,
                        },
                        "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                            color: theme.palette.text.secondary,
                        },
                    }}
                />
            </Grid>
            <Grid size={12} key={"heladeraDestinoId"}>
                <SelectElement
                    name={"heladeraDestinoId"}
                    label={"Heladera Destino"}
                    options={
                        (data ?? []).map(heladera => {
                            return {
                                label: heladera.puntoEstrategico.nombre,
                                id: heladera.id
                            }
                        })
                    }
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor seleccione una opción"
                        }
                    }
                    disabled={onlyView}
                    sx={{
                        "& .MuiInputBase-input.Mui-disabled": {
                            WebkitTextFillColor: theme.palette.text.primary,
                        },
                        "& .MuiFormLabel-root.MuiInputLabel-root.Mui-disabled": {
                            color: theme.palette.text.secondary,
                        },
                    }}
                />
            </Grid>
        </Grid>
    );
}