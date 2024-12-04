"use client";
import React from "react";
import Grid from "@mui/material/Grid2";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {DatePickerElement} from "react-hook-form-mui/date-pickers";
import {TextFieldElement} from "react-hook-form-mui";


export const DonacionMonetariaForm = () => {
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
                        sx={{width: '100%'}}
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
                />
            </Grid>
        </Grid>
    );
}