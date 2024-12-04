"use client";
import React from "react";
import Grid from "@mui/material/Grid2";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {DatePickerElement} from "react-hook-form-mui/date-pickers";
import {SelectElement, TextFieldElement} from "react-hook-form-mui";
import {useGetHeladerasQuery} from "@redux/services/heladerasApi";
import {CircularProgress} from "@mui/material";

export const DonacionViandasForm = () => {
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
                        defaultValue={new Date()}
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
                />
            </Grid>
            <Grid size={4} key={"peso"}>
                <TextFieldElement
                    name={"peso"}
                    label={"Peso"}
                    placeholder={"Ingrese el peso de la comida"}
                    required={true}
                    fullWidth
                    type="number"
                    rules={
                        {
                            required: "Por favor ingrese el peso de la comida"
                        }
                    }
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
                        sx={{width: '100%'}}
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
                />
            </Grid>
        </Grid>
    );
}