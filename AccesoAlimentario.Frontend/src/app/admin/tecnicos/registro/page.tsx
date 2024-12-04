"use client";
import {useTheme} from "@mui/material/styles";
import {FormContainer, SelectElement, TextFieldElement, useForm} from "react-hook-form-mui";
import {useAppSelector} from "@redux/hook";
import {useNotification} from "@components/Notifications/NotificationContext";
import {FormFieldValue} from "@components/Forms/Form";
import {IAltaTecnicoRequest} from "@models/requests/tecnicos/iAltaTecnicoRequest";
import {SexoDocumento} from "@models/enums/sexoDocumento";
import {IPersonaHumanaRequest} from "@models/requests/colaboradores/iAltaColaboradorRequest";
import {TipoDocumento} from "@models/enums/tipoDocumento";
import {usePostTecnicoMutation} from "@redux/services/tecnicosApi";
import {Box, Button, CardActions, Divider, Stack} from "@mui/material";
import Typography from "@mui/material/Typography";
import CardContent from "@mui/material/CardContent";
import MainCard from "@components/Cards/MainCard";
import React from "react";
import Grid from "@mui/material/Grid2";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {DatePickerElement} from "react-hook-form-mui/date-pickers";

export default function TecnicoRegistroPage() {
    const theme = useTheme();
    const formContext = useForm();
    const user = useAppSelector(state => state.user);
    const [
        postTecnico,
        {isLoading}
    ] = usePostTecnicoMutation();
    const {addNotification} = useNotification();

    const handleSave = async (data: FormFieldValue) => {
        const request: IAltaTecnicoRequest = {
            persona: {
                nombre: data.nombre,
                apellido: data.apellido,
                sexo: data.sexo as unknown as SexoDocumento,
                tipoPersona: "Humana"
            } as IPersonaHumanaRequest,
            mediosDeContacto: [
                {
                    preferida: true,
                    tipo: "Email",
                    direccion: data.email
                }
            ],
            documento: {
                tipoDocumento: data.tipoDocumento as unknown as TipoDocumento,
                nroDocumento: Number(data.numeroDocumento),
                fechaNacimiento: data.fechaNacimiento
            },
            areaCoberturaLatitud: Number(data.areaCoberturaLatitud),
            areaCoberturaLongitud: Number(data.areaCoberturaLongitud),
            areaCoberturaRadio: Number(data.areaCoberturaRadio),
        }
        try {
            await postTecnico(request).unwrap();
            addNotification("Tecnico registrado correctamente", "success");
            formContext.reset();
        } catch {
            addNotification("Error al registrar el tecnico", "error");
        }
    }

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
                                Registro de Tecnicos
                            </Typography>
                            <Typography variant="subtitle1" sx={{flexGrow: 1}}>
                                Registrar un nuevo tecnico, completando los siguientes campos
                            </Typography>
                        </Box>
                    </Stack>
                </CardActions>
                <CardContent>
                    <Grid container spacing={3} alignItems="center">
                        <Grid size={6} key={"nombre"}>
                            <TextFieldElement
                                name={"nombre"}
                                label={"Nombre"}
                                placeholder={"Ingrese su nombre"}
                                required={true}
                                fullWidth
                                rules={
                                    {
                                        required: "Por favor ingrese su nombre"
                                    }
                                }
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
                            />
                        </Grid>
                        <Grid size={12} key={"email"}>
                            <TextFieldElement
                                name={"email"}
                                label={"Email"}
                                placeholder={"Ingrese la dirección de correo electrónico"}
                                required={true}
                                fullWidth
                                type={"email"}
                                rules={
                                    {
                                        required: "Por favor ingrese la dirección de correo electrónico",
                                    }
                                }
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
                                    {label: "Cuit", id: TipoDocumento.CUIT},
                                    {label: "Cuil", id: TipoDocumento.CUIL},
                                ]}
                                required={true}
                                fullWidth
                                rules={
                                    {
                                        required: "Por favor seleccione una opción"
                                    }
                                }
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
                            />
                        </Grid>
                        <Grid size={6} key={"sexo"}>
                            <SelectElement
                                name={"sexo"}
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
                                    sx={{width: '100%'}}
                                />
                            </LocalizationProvider>
                        </Grid>
                        <Grid size={4} key={"areaCoberturaLatitud"}>
                            <TextFieldElement
                                name={"areaCoberturaLatitud"}
                                label={"Area de Cobertura Latitud"}
                                placeholder={"Ingrese la latitud de la cobertura"}
                                required={true}
                                fullWidth
                                type="number"
                                rules={
                                    {
                                        required: "Por favor ingrese la latitud de la cobertura"
                                    }
                                }
                            />
                        </Grid>
                        <Grid size={4} key={"areaCoberturaLongitud"}>
                            <TextFieldElement
                                name={"areaCoberturaLongitud"}
                                label={"Area de Cobertura Longitud"}
                                placeholder={"Ingrese su longitud de la cobertura"}
                                required={true}
                                fullWidth
                                type="number"
                                rules={
                                    {
                                        required: "Por favor ingrese la longitud de la cobertura"
                                    }
                                }
                            />
                        </Grid>
                        <Grid size={4} key={"areaCoberturaRadio"}>
                            <TextFieldElement
                                name={"areaCoberturaRadio"}
                                label={"Area de Cobertura Radio"}
                                placeholder={"Ingrese su radio de la cobertura"}
                                required={true}
                                fullWidth
                                type="number"
                                rules={
                                    {
                                        required: "Por favor ingrese el radio de la cobertura"
                                    }
                                }
                            />
                        </Grid>
                    </Grid>
                </CardContent>
                <Divider/>
                <CardActions sx={{
                    bgcolor: theme.palette.background.default,
                }}>
                    <Stack direction="row" spacing={1} justifyContent="center"
                           sx={{width: 1, px: 1.5, py: 0.75}}>
                        <Button color="primary" variant="contained" type={"submit"} disabled={isLoading}>
                            Registrar
                        </Button>
                    </Stack>
                </CardActions>
            </FormContainer>
        </MainCard>
    );
}