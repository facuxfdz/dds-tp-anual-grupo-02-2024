'use client';
import {useSearchParams} from 'next/navigation'
import {Controller, FormContainer, TextFieldElement, useForm} from "react-hook-form-mui";
import {Box, Button, CardActions, Divider, MenuItem, Select, Stack} from "@mui/material";
import Typography from "@mui/material/Typography";
import CardContent from "@mui/material/CardContent";
import {RegistroPersonaFisica} from "@components/RegistroColaboradores/RegistroPersonaFisica";
import {RegistroPersonaJuridica} from "@components/RegistroColaboradores/RegistroPersonaJuridica";
import MainCard from "@components/Cards/MainCard";
import React, {useEffect} from "react";
import {FormFieldValue} from "@components/Forms/Form";
import {useNotification} from "@components/Notifications/NotificationContext";
import {useTheme} from "@mui/material/styles";
import AuthBackground from "@components/Auth/AuthBackground";
import Grid from "@mui/material/Grid2";
import Logo from "@components/Logo";
import {MuiFileInput} from "mui-file-input";
import {useRouter} from 'next/navigation';
import IconButton from "@mui/material/IconButton";
import {
    IPersonaHumanaRequest,
    IPersonaJuridicaRequest,
    IPersonaRequest
} from "@models/requests/colaboradores/iAltaColaboradorRequest";
import {SexoDocumento} from "@models/enums/sexoDocumento";
import {TipoJuridica} from "@models/enums/tipoJuridica";
import {usePostRegisterMutation, usePostValidarPasswordMutation} from "@redux/services/authApi";
import {IPostRegisterRequest, RegisterType} from "@models/requests/auth/iPostRegisterRequest";
import {TipoDocumento} from "@models/enums/tipoDocumento";
import {ContribucionesTipo} from "@models/enums/contribucionesTipo";

export default function RegisterPage() {
    const theme = useTheme();
    const [tipoColaborador, setTipoColaborador] = React.useState<"fisica" | "juridico">("fisica");
    const formContext = useForm();
    const {addNotification} = useNotification();
    const query = useSearchParams();
    const router = useRouter();
    const [
        postRegister,
        {isLoading: registerIsLoading}
    ] = usePostRegisterMutation();
    const [
        postValidarPassword,
    ] = usePostValidarPasswordMutation();

    const handleSave = async (data: FormFieldValue) => {
        let personaRequest: IPersonaRequest;
        if (tipoColaborador === "fisica") {
            personaRequest = {
                nombre: data.nombre,
                apellido: data.apellido,
                sexo: data.sexo as unknown as SexoDocumento,
                tipoPersona: "Humana"
            } as IPersonaHumanaRequest;
        } else {
            personaRequest = {
                nombre: data.nombre,
                tipoJuridico: data.tipoJuridico as unknown as TipoJuridica,
                razonSocial: data.razonSocial,
                rubro: data.rubro,
                tipoPersona: "Juridica"
            } as IPersonaJuridicaRequest;
        }

        const request: IPostRegisterRequest = {
            email: data.email,
            password: data.password,
            profilePicture: query.get("profilePicture") || data.profilePicture,
            registerType: query.get("register") === "sso" ? RegisterType.Sso : RegisterType.Standard,

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
            contribucionesPreferidas: data.contribucionesPreferidas as unknown as ContribucionesTipo[],
            tarjeta: tipoColaborador === "fisica" ? {
                codigo: data.codigo,
                tipo: 'Colaboracion'
            } : undefined
        };

        try {
            await postRegister(request).unwrap();
            addNotification("Colaborador registrado correctamente", "success");
            router.replace("/login");
        } catch {
            addNotification("Ocurrió un error al registrar el colaborador", "error");
        }
    }

    const handleFileChange = (newFile: File | null) => {
        const reader = new FileReader();
        reader.onloadend = () => {
            formContext.setValue("profilePicture", reader.result);
        };
        reader.readAsDataURL(newFile as Blob);
    };

    useEffect(() => {
        formContext.setValue("email", query.get("email"));
        formContext.setValue("nombre", query.get("nombre"));
    }, [formContext, query]);


    return (
        <Box sx={{minHeight: '100vh'}}>
            <AuthBackground/>
            <Grid
                container
                direction="column"
                justifyContent="flex-end"
                sx={{
                    minHeight: '100vh'
                }}
            >
                <Grid sx={{ml: 3, mt: 3}}>
                    <Logo isIcon={false} to="/"/>
                </Grid>
                <Grid size={12}>
                    <Grid
                        size={12}
                        container
                        justifyContent="center"
                        alignItems="center"
                        sx={{
                            minHeight: {
                                xs: 'calc(100vh - 210px)',
                                sm: 'calc(100vh - 134px)',
                                md: 'calc(100vh - 112px)'
                            }
                        }}
                    >
                        <Grid size={{
                            xs: 12,
                            sm: 10,
                            md: 8,
                            lg: 6,
                        }}>
                            <MainCard content={false} sx={{overflow: 'visible'}}>
                                <FormContainer
                                    formContext={formContext}
                                    onSuccess={handleSave}
                                >
                                    <CardActions
                                        sx={{
                                            position: 'sticky',
                                            top: 0,
                                            bgcolor: theme.palette.background.default,
                                            zIndex: 2,
                                            borderBottom: `1px solid ${theme.palette.divider}`
                                        }}
                                    >
                                        <Stack direction="row" alignItems="center" justifyContent="space-between"
                                               sx={{width: 1}}>

                                            <IconButton
                                                onClick={() => {
                                                    router.replace("/login");
                                                }}
                                                sx={{mr: 0.5}}
                                            >
                                                <i className="fa-solid fa-arrow-turn-down-left"></i>
                                            </IconButton>
                                            <Box component="div" sx={{flexGrow: 1, m: 0, pl: 1.5}}>
                                                <Typography variant="h5" sx={{flexGrow: 1}}>
                                                    Registro de Colaboradores
                                                </Typography>
                                                <Typography variant="subtitle1" sx={{flexGrow: 1}}>
                                                    Registrese en el sistema, completando el formulario
                                                </Typography>
                                            </Box>
                                        </Stack>
                                        <Stack direction="row" spacing={1} sx={{px: 1.5, py: 0.75}}>
                                            <Select variant="outlined" value={tipoColaborador}
                                                    onChange={(e) => {
                                                        setTipoColaborador(e.target.value as "fisica" | "juridico");
                                                        formContext.reset();
                                                        formContext.setValue("email", query.get("email"));
                                                        formContext.setValue("nombre", query.get("nombre"));
                                                        console.log(formContext.getValues());
                                                    }}>
                                                <MenuItem value="fisica">Persona Física</MenuItem>
                                                <MenuItem value="juridico">Persona Jurídica</MenuItem>
                                            </Select>
                                        </Stack>
                                    </CardActions>
                                    <CardContent>
                                        {
                                            tipoColaborador === "fisica" ? (
                                                <RegistroPersonaFisica/>
                                            ) : (
                                                <RegistroPersonaJuridica/>
                                            )
                                        }
                                        <Grid container marginTop={3} spacing={3} alignItems="center">
                                            <Grid size={6} key={"email"}>
                                                <TextFieldElement
                                                    name={"email"}
                                                    label={"Correo Electrónico"}
                                                    placeholder={"Ingrese su correo electrónico"}
                                                    required={true}
                                                    fullWidth
                                                    rules={
                                                        {
                                                            required: "Por favor ingrese su correo electrónico"
                                                        }
                                                    }
                                                />
                                            </Grid>
                                            <Grid size={6} key={"password"}>
                                                <Controller
                                                    name="password"
                                                    rules={{
                                                        required: query.get("register") !== "sso" ? 'La contraseña es obligatoria' : undefined,
                                                        validate: async (value) => {
                                                            if (query.get("register") === "sso" && (!value || value === "")) {
                                                                return true;
                                                            }
                                                            const resp = await postValidarPassword(value).unwrap();
                                                            if (!resp) {
                                                                return "La contraseña debe tener al menos 8 caracteres, una letra mayúscula, una minúscula, un número y un caracter especial";
                                                            } else {
                                                                return true;
                                                            }
                                                        },
                                                    }}
                                                    render={({field, fieldState}) => (
                                                        <TextFieldElement
                                                            {...field}
                                                            label="Contraseña"
                                                            type="password"
                                                            error={!!fieldState.error}
                                                            helperText={fieldState.error?.message}
                                                            fullWidth
                                                        />
                                                    )}
                                                />
                                            </Grid>
                                            {
                                                !query.get("profilePicture") && (
                                                    <Grid size={12} key={"profilePicture"}>
                                                        <Controller
                                                            name="profilePicture"
                                                            rules={{required: "Por favor cargue una imagen"}}
                                                            render={({field, fieldState}) => (
                                                                <MuiFileInput
                                                                    {...field}
                                                                    label="Imagen de perfil"
                                                                    onChange={(newFile) => {
                                                                        handleFileChange(newFile);
                                                                    }}
                                                                    placeholder="Seleccione una imagen"
                                                                    inputProps={{accept: "image/*"}}
                                                                    error={!!fieldState.error}
                                                                    helperText={fieldState.error?.message}
                                                                    getInputText={(value) => value ? 'Imagen seleccionada' : 'Seleccione una imagen'}
                                                                    fullWidth
                                                                />
                                                            )}
                                                        />
                                                    </Grid>
                                                )
                                            }
                                        </Grid>
                                    </CardContent>
                                    <Divider/>
                                    <CardActions sx={{
                                        bgcolor: theme.palette.background.default,
                                    }}>
                                        <Stack direction="row" spacing={1} justifyContent="center"
                                               sx={{width: 1, px: 1.5, py: 0.75}}>
                                            <Button color="primary" variant="contained" type={"submit"}
                                                    disabled={registerIsLoading}>
                                                Registrar
                                            </Button>
                                        </Stack>
                                    </CardActions>
                                </FormContainer>
                            </MainCard>
                        </Grid>
                    </Grid>
                </Grid>
            </Grid>
        </Box>
    )
}
