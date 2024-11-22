"use client";
import React from "react";
import Grid from "@mui/material/Grid2";
import {SelectElement, TextFieldElement} from "react-hook-form-mui";
import {useGetHeladerasQuery} from "@redux/services/heladerasApi";
import {CircularProgress} from "@mui/material";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {DatePickerElement} from "react-hook-form-mui/date-pickers";

export const DistribucionViandasForm = () => {
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
                        sx={{width: '100%'}}
                    />
                </LocalizationProvider>
            </Grid>
            <Grid size={6} key={"motivo"}>
                <TextFieldElement
                    name={"motivo"}
                    label={"Motivo"}
                    placeholder={"Ingrese un motivo"}
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor ingrese un motivo"
                        }
                    }
                />
            </Grid>
            <Grid size={6} key={"cantidad"}>
                <TextFieldElement
                    name={"cantidad"}
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
                />
            </Grid>
            <Grid size={12} key={"heladeraOrigen"}>
                <SelectElement
                    name={"heladeraOrigen"}
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
                />
            </Grid>
            <Grid size={12} key={"heladeraDestino"}>
                <SelectElement
                    name={"heladeraDestino"}
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
                />
            </Grid>
        </Grid>
    );
}