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
        id: "heladera",
        label: "Heladera",
        type: FormFieldType.SELECT,
        width: 12,
        value: "",
        placeholder: "Seleccione una opción",
        isRequired: true,
        regex: "",
        errorMessage: "Por favor seleccione una opción",
        options: ["Heladera 1", "Heladera 2", "Heladera 3"]
    },
    {
        id: "descripcion",
        label: "Descripción",
        type: FormFieldType.TEXT,
        width: 12,
        value: "",
        placeholder: "Ingrese una descripción",
        isRequired: false,
        regex: "",
        errorMessage: "Por favor ingrese una descripción",
        options: [],
        icon: "fa-duotone fa-solid fa-subtitles"
    },
    {
        id: "imagen",
        label: "Imagen",
        type: FormFieldType.IMAGE,
        width: 12,
        value: "",
        placeholder: "Seleccione una imagen",
        isRequired: false,
        regex: "",
        errorMessage: "Por favor seleccione una imagen",
        options: [],
        icon: "fa-duotone fa-solid fa-image"
    }
]

export default function ReportarIncidenciaPage() {
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
                                Reportar Incidencia
                            </Typography>
                            <Typography variant="subtitle1" sx={{flexGrow: 1}}>
                                Reporta una incidencia en el sistema, completando el formulario
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
                            Reportar
                        </Button>
                    </Stack>
                </CardActions>
            </FormContainer>
        </MainCard>
    );
}