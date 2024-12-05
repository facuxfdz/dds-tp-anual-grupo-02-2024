"use client";
import {Button, Stack} from "@mui/material";
import Grid from "@mui/material/Grid2"
import AuthWrapper from "@components/Auth/AuthWrapper";
import Typography from "@mui/material/Typography";
import React from "react";
import AuthLogin from "@components/Auth/AuthLogin";
import {setUser, User} from '@redux/features/userSlice';
import {useDispatch} from 'react-redux';
import {useRouter} from 'next/navigation';
import {parseJwt, parseJwtGoogle} from "@utils/decode_jwt";
import {CredentialResponse, GoogleLogin} from '@react-oauth/google';
import {useNotification} from "@components/Notifications/NotificationContext";
import {usePostLoginValidarMutation} from "@redux/services/authApi";
import {inicioRoute} from "@routes/router";
import {ContribucionesTipo} from "@models/enums/contribucionesTipo";

export default function LoginPage() {
    const dispatch = useDispatch();
    const router = useRouter();
    const {addNotification} = useNotification();
    const [
        postLoginValidate,
        {isLoading: loginValidationIsLoading}
    ] = usePostLoginValidarMutation();

    const parseToEnumArray = (input: string): ContribucionesTipo[] => {
        return input
            .split(",") // Divide el string en un array de cadenas
            .map(item => Number(item.trim())) // Convierte cada elemento en número
            .filter(
                (item): item is ContribucionesTipo =>
                    Object.values(ContribucionesTipo).includes(item as ContribucionesTipo) // Valida si el número pertenece al enum
            );
    };

    const handleLogin = async (response: {
                                   userExists: boolean;
                                   token: string;
                               },
                               jwtToken?: string
    ) => {
        const userExists: boolean = response.userExists;
        if (userExists) {
            const tokenCookie = response.token;
            const jsonRes = parseJwt(tokenCookie);
            const user: User = {
                colaboradorId: jsonRes.colaboradorId ?? '',
                tecnicoId: jsonRes.tecnicoId ?? '',

                name: jsonRes.name ?? '',
                profile_picture: jsonRes.profile_picture ?? '',

                contribucionesPreferidas: parseToEnumArray(jsonRes.contribucionesPreferidas || ""),
                tarjetaColaboracionId: jsonRes.tarjetaColaboracionId ?? '',
                personaTipo: jsonRes.personaTipo ? jsonRes.personaTipo as 'Humana' | 'Juridica' : 'Humana',
                isAdmin: jsonRes.isAdmin ? jsonRes.isAdmin == "1" : false,
            };
            dispatch(setUser(user));
            router.push(inicioRoute());
        } else {
            if (jwtToken) {
                const jsonRes = parseJwtGoogle(jwtToken);
                router.replace("/login/registrar?nombre=" + jsonRes.name + "&email=" + jsonRes.email + "&profilePicture=" + jsonRes.picture + "&register=sso");
            }
        }
    }

    const handleSuccess = async (credentialResponse: CredentialResponse) => {
        try {
            if (!credentialResponse.credential) {
                addNotification("Error en la autenticación. Por favor, verifica tus credenciales.", "error");
                return;
            }
            const jwtToken = credentialResponse.credential;
            try {
                const response = await postLoginValidate(jwtToken).unwrap();
                await handleLogin(response, jwtToken);
            } catch {
                addNotification("Error en la autenticación. Por favor, verifica tus credenciales.", "error");
            }
        } catch {
            addNotification("Error en la autenticación. Por favor, verifica tus credenciales.", "error");
        }
    };

    return (
        <AuthWrapper>
            <Grid container spacing={3} direction={"column"}>
                <Grid size={12}>
                    <Stack direction="row" justifyContent="space-between" alignItems="baseline"
                           sx={{mb: {xs: -0.5, sm: 0.5}}}>
                        <Typography variant="h3">Login</Typography>
                    </Stack>
                </Grid>
                <Grid size={12}>
                    <AuthLogin isLoading={loginValidationIsLoading} handleLogin={handleLogin}/>
                </Grid>
                <Grid size={12} sx={{display: "flex", justifyContent: "center"}}>
                    <Button
                        variant="contained"
                        color="secondary"
                        onClick={() => {
                            router.push("/login/registrar");
                        }}
                        fullWidth
                    >
                        Registrarse
                    </Button>
                </Grid>
                <Grid size={12} sx={{display: "flex", justifyContent: "center"}}>
                    <GoogleLogin
                        onSuccess={credentialResponse => {
                            handleSuccess(credentialResponse);
                        }}
                        onError={() => {
                            addNotification("Error en la autenticación. Por favor, verifica tus credenciales.", "error");
                        }}
                    />
                </Grid>
            </Grid>
        </AuthWrapper>
    );
}