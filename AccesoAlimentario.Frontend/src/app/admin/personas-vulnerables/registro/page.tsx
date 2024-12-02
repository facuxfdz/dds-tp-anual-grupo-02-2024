"use client";
import React from "react";
import MainCard from "@components/Cards/MainCard";
import {useTheme} from "@mui/material/styles";
import {Box, Button, CardActions, Divider, Stack} from "@mui/material";
import Typography from "@mui/material/Typography";
import CardContent from "@mui/material/CardContent";
import {FormFieldValue} from "@components/Forms/Form";
import {FormContainer, SelectElement, TextFieldElement, useForm} from "react-hook-form-mui";
import Grid from "@mui/material/Grid2";
import {SexoDocumento} from "@models/enums/sexoDocumento";
import {TipoDocumento} from "@models/enums/tipoDocumento";
import {usePostRegistroPersonaVulnerableMutation} from "@redux/services/contribucionesApi";
import {IRegistroPersonaVulnerableRequest} from "@models/requests/contribuciones/iRegistroPersonaVulnerableRequest";
import {useAppSelector} from "@redux/hook";
import {LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import {DatePickerElement} from "react-hook-form-mui/date-pickers";
import {useNotification} from "@components/Notifications/NotificationContext";

export default function PersonasVulnerablesRegistroPage() {
    const theme = useTheme();
    const formContext = useForm();
    const user = useAppSelector(state => state.user);
    const [
        postRegistroPersonaVulnerable,
        {isLoading}
    ] = usePostRegistroPersonaVulnerableMutation();
    const {addNotification} = useNotification();

    const handleSave = async (data: FormFieldValue) => {
        const request: IRegistroPersonaVulnerableRequest = {
            colaboradorId: user.id,
            tarjeta: {
                codigo: data.codigoTarjeta,
                tipo: 'Consumo',
                responsableId: user.id,
            },
            persona: {
                nombre: data.nombre,
                tipo: "Humana",
                apellido: data.apellido,
                sexo: data.sexo,
            },
            direccion: {
                calle: data.calle,
                numero: data.numero,
                localidad: data.localidad,
                piso: data.piso,
                departamento: data.departamento,
                codigoPostal: data.codigoPostal,
            },
            documento: {
                tipoDocumento: data.tipoDocumento,
                nroDocumento: data.numeroDocumento,
                fechaNacimiento: data.fechaNacimiento,
            },
            cantidadMenores: data.cantidadHijos,
        };
        try {
            await postRegistroPersonaVulnerable(request).unwrap();
            addNotification("Persona registrada correctamente", "success");
        } catch {
            addNotification("Error al registrar la persona", "error");
        }
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
                        <Stack direction="row" spacing={1}>
                            <Button color="error" variant="contained" disabled={isLoading} onClick={formContext.reset}>
                                Reiniciar
                            </Button>
                        </Stack>
                    </Stack>
                </CardActions>
                <CardContent>
                    <Grid container spacing={3} alignItems="center">
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
                            />
                        </Grid>
                        <Grid size={2} key={"piso"}>
                            <TextFieldElement
                                name={"piso"}
                                label={"Piso"}
                                placeholder={"Ingrese su piso"}
                                fullWidth
                            />
                        </Grid>
                        <Grid size={4} key={"departamento"}>
                            <TextFieldElement
                                name={"departamento"}
                                label={"Departamento"}
                                placeholder={"Ingrese su departamento"}
                                fullWidth
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