"use client";
import {Box, Button, CardActions, Divider, Stack} from "@mui/material";
import {FormContainer, TextFieldElement, useForm} from "react-hook-form-mui";
import Typography from "@mui/material/Typography";
import CardContent from "@mui/material/CardContent";
import {Form, FormFieldType, FormFieldValue, IFormField} from "@components/Forms/Form";
import MainCard from "@components/Cards/MainCard";
import React, {useEffect} from "react";
import {useTheme} from "@mui/material/styles";
import {useActualizarPerfilMutation, useGetObtenerPerfilQuery} from "@redux/services/authApi";
import {RegistroPersonaFisica} from "@components/RegistroColaboradores/RegistroPersonaFisica";
import {RegistroPersonaJuridica} from "@components/RegistroColaboradores/RegistroPersonaJuridica";
import {useAppSelector} from "@redux/hook";
import {IPersonaResponse} from "@models/responses/personas/iPersonaResponse";
import {IPersonaHumanaReponse} from "@models/responses/personas/iPersonaHumanaReponse";
import {IPersonaJuridicaResponse} from "@models/responses/personas/iPersonaJuridicaResponse";
import {
    IPersonaHumanaRequest,
    IPersonaJuridicaRequest,
    IPersonaRequest
} from "@models/requests/colaboradores/iAltaColaboradorRequest";
import {SexoDocumento} from "@models/enums/sexoDocumento";
import {TipoJuridica} from "@models/enums/tipoJuridica";
import {TipoDocumento} from "@models/enums/tipoDocumento";
import {ContribucionesTipo} from "@models/enums/contribucionesTipo";
import {IActualizarPerfilRequest} from "@models/requests/auth/iActualizarPerfilRequest";
import {useNotification} from "@components/Notifications/NotificationContext";
import Grid from "@mui/material/Grid2";
import {useDispatch} from "react-redux";
import {setUserName, setUserTarjetaColaboracionId} from "@redux/features/userSlice";
import {IColaboradorResponseMinimo} from "@models/responses/roles/iColaboradorResponse";

export default function PerfilPage() {
    const theme = useTheme();
    const user = useAppSelector(state => state.user);
    const formContext = useForm();
    const [
        actualizarPerfil,
        {isLoading: isUpdating}
    ] = useActualizarPerfilMutation();
    const {addNotification} = useNotification();
    const dispatch = useDispatch();

    const handleSave = async (data: FormFieldValue) => {
        let personaRequest: IPersonaRequest;
        if (user.personaTipo === "Humana") {
            personaRequest = {
                nombre: data.nombre,
                apellido: data.apellido,
                sexo: data.sexo as unknown as SexoDocumento,
                tipoPersona: "Humana"
            } as IPersonaHumanaRequest;
        } else {
            personaRequest = {
                nombre: data.nombre,
                tipo: data.tipoJuridico as unknown as TipoJuridica,
                razonSocial: data.razonSocial,
                rubro: data.rubro,
                tipoPersona: "Juridica"
            } as IPersonaJuridicaRequest;
        }
        const request: IActualizarPerfilRequest = {
            persona: personaRequest,
            direccion: {
                calle: data.calle,
                numero: data.numero,
                localidad: data.localidad,
                piso: data.piso,
                departamento: data.departamento,
                codigoPostal: data.codigoPostal
            },
            documento: {
                tipoDocumento: data.tipoDocumento as unknown as TipoDocumento,
                nroDocumento: Number(data.numeroDocumento),
                fechaNacimiento: data.fechaNacimiento
            },
            contribucionesPreferidas: (user.colaboradorId != null) ? data.contribucionesPreferidas as unknown as ContribucionesTipo[] : undefined,
            tarjeta: (user.personaTipo === "Humana" &&
                data.codigoTarjeta != null && data.codigoTarjeta != "")
                ? {
                    codigo: data.codigoTarjeta,
                    tipo: 'Colaboracion'
                } : undefined
        };

        try {
            await actualizarPerfil(request).unwrap();
            addNotification("Perfil actualizado correctamente", "success");
            dispatch(setUserName(data.nombre));
            if (user.personaTipo === "Humana" && data.codigoTarjeta != null && data.codigoTarjeta != "") {
                dispatch(setUserTarjetaColaboracionId(data.codigoTarjeta));
            }
        } catch {
            addNotification("Error al actualizar el perfil", "error");
        }
    };
    const {data} = useGetObtenerPerfilQuery();

    useEffect(() => {
        if (data) {
            const colaborador = data.roles.find((rol) => rol.tipo === "Colaborador");
            const initialData: any = {
                nombre: data.persona.nombre,

                apellido: user.personaTipo === "Humana" ? (data.persona as IPersonaHumanaReponse).apellido : "",
                sexo: user.personaTipo === "Humana" ? (data.persona as IPersonaHumanaReponse).sexo : "",

                tipoJuridico: user.personaTipo === "Juridica" ? (data.persona as IPersonaJuridicaResponse).tipo : "",
                razonSocial: user.personaTipo === "Juridica" ? (data.persona as IPersonaJuridicaResponse).razonSocial : "",
                rubro: user.personaTipo === "Juridica" ? (data.persona as IPersonaJuridicaResponse).rubro : "",

                tipoDocumento: data.persona.documentoIdentidad!.tipoDocumento,
                numeroDocumento: data.persona.documentoIdentidad!.nroDocumento,
                fechaNacimiento: data.persona.documentoIdentidad!.fechaNacimiento,
                calle: data.persona.direccion!.calle,
                numero: data.persona.direccion!.numero,
                localidad: data.persona.direccion!.localidad,
                piso: data.persona.direccion!.piso,
                departamento: data.persona.direccion!.departamento,
                codigoPostal: data.persona.direccion!.codigoPostal,
                contribucionesPreferidas: (colaborador as IColaboradorResponseMinimo)?.contribucionesPreferidas,
                codigoTarjeta: (colaborador as IColaboradorResponseMinimo)?.tarjetaColaboracion?.codigo
            }

            formContext.reset(initialData);
        }
    }, [data, formContext, user.personaTipo]);

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
                                Mi Perfil
                            </Typography>
                            <Typography variant="subtitle1" sx={{flexGrow: 1}}>
                                Complete su perfil para poder realizar donaciones
                            </Typography>
                        </Box>
                    </Stack>
                </CardActions>
                <CardContent>
                    {
                        user.personaTipo === "Humana" ? (
                            <RegistroPersonaFisica hideEmail={true}/>
                        ) : (
                            <RegistroPersonaJuridica hideEmail={true}/>
                        )
                    }
                    {
                        user.tecnicoId != "" && (
                            <>
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
                            </>
                        )
                    }
                </CardContent>
                <Divider/>
                <CardActions sx={{
                    bgcolor: theme.palette.background.default,
                }}>
                    <Stack direction="row" spacing={1} justifyContent="center"
                           sx={{width: 1, px: 1.5, py: 0.75}}>
                        <Button color="primary" variant="contained" type={"submit"} disabled={isUpdating}>
                            Actualizar
                        </Button>
                    </Stack>
                </CardActions>
            </FormContainer>
        </MainCard>
    );
}