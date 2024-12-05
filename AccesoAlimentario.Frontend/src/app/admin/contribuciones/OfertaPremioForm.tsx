"use client";
import React from "react";
import Grid from "@mui/material/Grid2";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {DatePickerElement} from "react-hook-form-mui/date-pickers";
import {Controller, SelectElement, TextFieldElement} from "react-hook-form-mui";
import {MuiFileInput} from "mui-file-input";
import {useFormContext} from "react-hook-form";
import {useTheme} from "@mui/material/styles";

export const OfertaPremioForm = ({
                                     onlyView = false,
                                 }: {
    onlyView?: boolean
}) => {
    const theme = useTheme();
    const context = useFormContext();
    const handleFileChange = (newFile: File | null) => {
        const reader = new FileReader();
        reader.onloadend = () => {
            context.setValue("imagen", reader.result);
        };
        reader.readAsDataURL(newFile as Blob);
    };

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
            <Grid size={6} key={"nombre"}>
                <TextFieldElement
                    name={"nombre"}
                    label={"Nombre"}
                    placeholder={"Ingrese el nombre de su premio"}
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor ingrese el nombre de su premio"
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
            <Grid size={6} key={"puntos"}>
                <TextFieldElement
                    name={"puntos"}
                    label={"Puntos Necesarios"}
                    placeholder={"Ingrese la cantidad de puntos necesarios"}
                    required={true}
                    fullWidth
                    type="number"
                    rules={
                        {
                            required: "Por favor ingrese la cantidad de puntos necesarios"
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
            <Grid size={12} key={"rubro"}>
                <SelectElement
                    name={"rubro"}
                    label={"Rubro"}
                    options={
                        [
                            {id: "Gastronomia", label: "Gastronomia"},
                            {id: "Electronica", label: "Electronica"},
                            {id: "ArticulosHogar", label: "Articulos del Hogar"},
                            {id: "Otros", label: "Otros"}
                        ]
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
            <Grid size={12} key={"imagen"}>
                {
                    !onlyView ? (
                        <Controller
                            name="imagen"
                            rules={{required: "Por favor cargue una imagen"}}
                            render={({field, fieldState}) => (
                                <MuiFileInput
                                    {...field}
                                    onChange={(newFile) => {
                                        handleFileChange(newFile);
                                    }}
                                    label="Imagen"
                                    placeholder="Seleccione una imagen"
                                    inputProps={{accept: "image/*"}}
                                    error={!!fieldState.error}
                                    helperText={fieldState.error?.message}
                                    getInputText={(value) => value ? 'Imagen seleccionada' : 'Seleccione una imagen'}
                                    fullWidth
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
                            )}
                        />
                    ) : (
                        <img src={context.watch("imagen")} alt="imagen" style={{maxWidth: '100%'}}/>
                    )
                }
            </Grid>
        </Grid>
    );
}