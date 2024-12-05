import {useTheme} from "@mui/material/styles";
import Grid from "@mui/material/Grid2";
import {SelectElement, TextFieldElement} from "react-hook-form-mui";
import {TipoDocumento} from "@models/enums/tipoDocumento";
import {SexoDocumento} from "@models/enums/sexoDocumento";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {DatePickerElement} from "react-hook-form-mui/date-pickers";
import React from "react";

export const RegistroPersonaVulnerableForm = ({
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
            <Grid size={6} key={"nombre"}>
                <TextFieldElement
                    name={"Nombre"}
                    label={"Nombre"}
                    placeholder={"Ingrese su nombre"}
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor ingrese su nombre"
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
            <Grid size={6} key={"sexo"}>
                <SelectElement
                    name={"Sexo"}
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
            <Grid size={12} key={"cantidadHijos"}>
                <TextFieldElement
                    name={"cantidadHijos"}
                    label={"Cantidad de Hijos"}
                    placeholder={"Ingrese la cantidad de hijos"}
                    required={true}
                    fullWidth
                    type="number"
                    rules={
                        {
                            required: "Por favor ingrese la cantidad de hijos",
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
            <Grid size={2} key={"piso"}>
                <TextFieldElement
                    name={"piso"}
                    label={"Piso"}
                    placeholder={"Ingrese su piso"}
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
            </Grid>
            <Grid size={4} key={"departamento"}>
                <TextFieldElement
                    name={"departamento"}
                    label={"Departamento"}
                    placeholder={"Ingrese su departamento"}
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
            <Grid size={12} key={"codigoTarjeta"}>
                <TextFieldElement
                    name={"codigoTarjeta"}
                    label={"Código Tarjeta"}
                    placeholder={"Ingrese el código de la tarjeta de consumo"}
                    required={true}
                    fullWidth
                    rules={
                        {
                            required: "Por favor ingrese el código de la tarjeta de consumo"
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
    )
}