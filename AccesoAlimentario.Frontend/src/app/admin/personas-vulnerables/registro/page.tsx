"use client";
import React from "react";
import MainCard from "@components/Cards/MainCard";
import {useTheme} from "@mui/material/styles";
import {Box, Button, CardActions, Divider, Stack} from "@mui/material";
import Typography from "@mui/material/Typography";
import CardContent from "@mui/material/CardContent";
import {Form, FormFieldType, FormFieldValue, IFormField} from "@components/Forms/Form";
import {FormContainer, useForm} from "react-hook-form-mui";

const fields: IFormField[] = [
    {
        id: "nombre",
        label: "Nombre",
        type: FormFieldType.TEXT,
        width: 12,
        value: "",
        placeholder: "Ingrese su nombre",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese su nombre",
        options: []
    },
    {
        id: "apellido",
        label: "Apellido",
        type: FormFieldType.TEXT,
        width: 12,
        value: "",
        placeholder: "Ingrese su apellido",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese su apellido",
        options: []
    },
    {
        id: "sexo",
        label: "Sexo",
        type: FormFieldType.SELECT,
        width: 12,
        value: "",
        placeholder: "Seleccione una opción",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor seleccione una opción",
        options: ["Masculino", "Femenino", "Otro"]
    },
    {
        id: "cantidadHijos",
        label: "Cantidad de Hijos",
        type: FormFieldType.NUMBER,
        width: 12,
        value: "",
        placeholder: "Ingrese la cantidad de hijos",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese la cantidad de hijos",
        options: []
    },
    {
        id: "colaboradorResponsable",
        label: "Colaborador Responsable",
        type: FormFieldType.TEXT,
        width: 12,
        value: "",
        placeholder: "Ingrese el colaborador responsable",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor ingrese el colaborador responsable",
        options: []
    },
    {
        id: "calle",
        label: "Calle",
        type: FormFieldType.TEXT,
        width: 6,
        value: "",
        placeholder: "Ingrese su calle",
        isRequired: false,
        regex: "",
        errorMessage: "Por favor ingrese su calle",
        options: []
    },
    {
        id: "numero",
        label: "Número",
        type: FormFieldType.NUMBER,
        width: 6,
        value: "",
        placeholder: "Ingrese su número",
        isRequired: false,
        regex: "",
        errorMessage: "Por favor ingrese su número",
        options: []
    },
    {
        id: "localidad",
        label: "Localidad",
        type: FormFieldType.TEXT,
        width: 4,
        value: "",
        placeholder: "Ingrese su localidad",
        isRequired: false,
        regex: "",
        errorMessage: "Por favor ingrese su localidad",
        options: []
    },
    {
        id: "piso",
        label: "Piso",
        type: FormFieldType.NUMBER,
        width: 2,
        value: "",
        placeholder: "Ingrese su piso",
        isRequired: false,
        regex: "",
        errorMessage: "Por favor ingrese su piso",
        options: []
    },
    {
        id: "departamento",
        label: "Departamento",
        type: FormFieldType.TEXT,
        width: 4,
        value: "",
        placeholder: "Ingrese su departamento",
        isRequired: false,
        regex: "",
        errorMessage: "Por favor ingrese su departamento",
        options: []
    },
    {
        id: "codigoPostal",
        label: "Código Postal",
        type: FormFieldType.NUMBER,
        width: 2,
        value: "",
        placeholder: "Ingrese su código postal",
        isRequired: false,
        regex: "\\d{4,8}",
        errorMessage: "Por favor ingrese su código postal",
        options: []
    },
    {
        id: "tipoDocumento",
        label: "Tipo de Documento",
        type: FormFieldType.SELECT,
        width: 6,
        value: "DNI",
        placeholder: "Seleccione una opción",
        isRequired: false,
        regex: "",
        errorMessage: "Por favor seleccione una opción",
        options: ["DNI", "LE", "LC", "CUIT", "CUIL"]
    },
    {
        id: "numeroDocumento",
        label: "Número de Documento",
        type: FormFieldType.NUMBER,
        width: 6,
        value: "",
        placeholder: "Ingrese su número de documento",
        isRequired: false,
        regex: "\\d{8,11}",
        errorMessage: "Por favor ingrese su número de documento",
        options: []
    },
]

export default function PersonasVulnerablesRegistroPage() {
    const theme = useTheme();
    const formContext = useForm();
    const handleSave = async (data: FormFieldValue) => {
        console.log(data);
    };

    return (
        <MainCard content={false} sx={{overflow: 'visible'}}>
            <FormContainer
                formContext={formContext}
                onSuccess={handleSave}
            >
                <CardActions
                    sx={{
                        position: 'sticky',
                        top: '60px',
                        bgcolor: theme.palette.background.default,
                        zIndex: 1,
                        borderBottom: `1px solid ${theme.palette.divider}`
                    }}
                >
                    <Stack direction="row" alignItems="center" justifyContent="space-between"
                           sx={{width: 1}}>
                        <Box component="div" sx={{flexGrow: 1, m: 0, pl: 1.5}}>
                            <Typography variant="h5" sx={{flexGrow: 1}}>
                                Registro de Personas Vulnerables
                            </Typography>
                            <Typography variant="subtitle1" sx={{flexGrow: 1}}>
                                Registra una persona vulnerable en el sistema, completando el formulario
                            </Typography>
                        </Box>
                    </Stack>
                </CardActions>
                <CardContent>
                    <Form fields={fields}/>
                </CardContent>
                <Divider/>
                <CardActions sx={{
                    bgcolor: theme.palette.background.default,
                }}>
                    <Stack direction="row" spacing={1} justifyContent="center"
                           sx={{width: 1, px: 1.5, py: 0.75}}>
                        <Button color="primary" variant="contained" type={"submit"} disabled={false}>
                            Registrar
                        </Button>
                    </Stack>
                </CardActions>
            </FormContainer>
        </MainCard>
    );
}