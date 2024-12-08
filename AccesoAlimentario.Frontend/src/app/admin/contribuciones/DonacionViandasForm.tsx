"use client";
import React from "react";
import Grid from "@mui/material/Grid2";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {DatePickerElement} from "react-hook-form-mui/date-pickers";
import {SelectElement, TextFieldElement} from "react-hook-form-mui";
import {useGetHeladerasQuery} from "@redux/services/heladerasApi";
import {CircularProgress} from "@mui/material";
import {useTheme} from "@mui/material/styles";

export const DonacionViandasForm = ({
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
            <Grid size={12} key={"comida"}>
                <TextFieldElement
                    name={"comida"}
                    label={"Comida"}
                    placeholder={"Comida"}
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor ingrese el nombre de la comida"
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
            <Grid size={4} key={"calorias"}>
                <TextFieldElement
                    name={"calorias"}
                    label={"Calorias"}
                    placeholder={"Ingrese la cantidad de calorias"}
                    required={true}
                    fullWidth
                    type="number"
                    rules={
                        {
                            required: "Por favor ingrese la cantidad de calorias"
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
            <Grid size={4} key={"peso"}>
                <TextFieldElement
                    name={"peso"}
                    label={"Peso"}
                    placeholder={"Ingrese el peso de la comida en kg"}
                    required={true}
                    fullWidth
                    type="number"
                    rules={
                        {
                            required: "Por favor ingrese el peso de la comida"
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
            <Grid size={4} key={"fechaCaducidad"}>
                <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <DatePickerElement
                        label={"Fecha de caducidad"}
                        name={"fechaCaducidad"}
                        required={true}
                        rules={
                            {
                                required: "Por favor ingrese una fecha de caducidad"
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
            <Grid size={12} key={"heladera"}>
                <SelectElement
                    name={"heladera"}
                    label={"Heladera"}
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