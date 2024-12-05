"use client";
import React from "react";
import Grid from "@mui/material/Grid2";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {DatePickerElement} from "react-hook-form-mui/date-pickers";
import {TextFieldElement} from "react-hook-form-mui";
import {useTheme} from "@mui/material/styles";


export const DonacionMonetariaForm = ({
                                          onlyView = false,
                                      }: {
    onlyView?: boolean
}) => {
    const theme = useTheme();
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
            <Grid size={12} key={"monto"}>
                <TextFieldElement
                    name={"monto"}
                    label={"Monto de la donación"}
                    placeholder={"Ingrese un monto"}
                    required={true}
                    fullWidth
                    type="number"
                    rules={
                        {
                            required: "Por favor ingrese un monto"
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
            <Grid size={12} key={"frecuenciaDias"}>
                <TextFieldElement
                    name={"frecuenciaDias"}
                    label={"Frecuencia de la donación"}
                    placeholder={"Ingrese una frecuencia en dias"}
                    required={true}
                    fullWidth
                    type="number"
                    rules={
                        {
                            required: "Por favor ingrese una frecuencia"
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