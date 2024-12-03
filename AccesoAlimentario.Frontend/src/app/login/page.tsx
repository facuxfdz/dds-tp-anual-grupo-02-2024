"use client";
import {Stack} from "@mui/material";
import Grid from "@mui/material/Grid2"
import AuthWrapper from "@components/Auth/AuthWrapper";
import Typography from "@mui/material/Typography";
import React from "react";
import AuthLogin from "@components/Auth/AuthLogin";
import {setUser, User} from '@redux/features/userSlice';
import {setSession} from '@redux/features/sessionSlice';
import {useDispatch} from 'react-redux';
import {config} from '@config/config';
import {useRouter} from 'next/navigation'
import {parseJwt} from "@utils/decode_jwt";
import {CredentialResponse, GoogleLogin} from '@react-oauth/google';
import {useNotification} from "@components/Notifications/NotificationContext";
import {usePostLoginValidateMutation} from "@redux/services/authApi";
import {inicioRoute} from "@routes/router";
import {ContribucionesTipo} from "@models/enums/contribucionesTipo";
import { getCookie } from 'cookies-next';


export default function LoginPage() {

    const dispatch = useDispatch();
    const router = useRouter();
    const {addNotification} = useNotification();
    const [
        postLoginValidate,
        {isLoading: loginValidationIsLoading}
    ] = usePostLoginValidateMutation();

    const parseToEnumArray = (input: string[]): ContribucionesTipo[] => {
        return input
            .map(item => {
                if (Object.values(ContribucionesTipo).includes(item as unknown as ContribucionesTipo)) {
                    return item as unknown as ContribucionesTipo;
                }
                return [];
            })
            .filter((item): item is ContribucionesTipo => item !== null);
    };

    const handleSuccess = async (credentialResponse: CredentialResponse) => {
        try {
            if (!credentialResponse.credential) {
                addNotification("Error en la autenticación. Por favor, verifica tus credenciales.", "error");
                return;
            }
            const jwtToken = credentialResponse.credential;

            try {
                const response = await postLoginValidate(jwtToken).unwrap();
                const userExists: boolean = response.userExists;
                if (userExists) {
                    const tokenCookie = await getCookie('session');
                    const jsonRes = parseJwt(tokenCookie);
                    const user : User = {
                        colaboradorId: jsonRes.colaboradorId ?? '',
                        tecnicoId: jsonRes.tecnicoId ?? '',

                        name: jsonRes.name ?? '',
                        profile_picture: jsonRes.profile_picture ?? '',

                        contribucionesPreferidas: parseToEnumArray(jsonRes.contribucionesPreferidas ?? []),
                        personaTipo: jsonRes.personaTipo ? jsonRes.personaTipo as 'Humana' | 'Juridica' : 'Humana',
                    };
                    dispatch(setUser(user));
                    router.replace(inicioRoute());
                } else {
                    router.replace("/login/register");
                }

            } catch {
                addNotification("Error en la autenticación. Por favor, verifica tus credenciales.", "error");
            }

            const response = await fetch(`${config.apiUrl}/auth/validate`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({token: jwtToken}),
                credentials: 'include',  // To ensure cookies are included in the request
            });

            if (response.ok) {
                const data = await response.json();  // Destructure the data from the backend
                const userExists: boolean = data.userExists;  // Check if the user exists in the database
                const jsonRes = parseJwt(jwtToken);
                const user = {
                    name: jsonRes.name ?? '',
                    email: jsonRes.email ?? '',
                    profile_picture: jsonRes.picture ?? '',
                    register_type: 'sso' as const,
                }
                // Dispatch the action to update the Redux state with the user info
                dispatch(setSignedUser(user));  // Assuming you have a setUser action
                if (userExists) {
                    console.log("User exists");
                    dispatch(setSession(jwtToken));  // Assuming you have a setSession action
                    dispatch(setUser(user));  // Assuming you have a setUser action
                    router.replace("/admin/inicio");
                }
                if (!userExists) {
                    router.replace("/login/register");
                }
            }

        } catch {
            addNotification("Error en la autenticación. Por favor, verifica tus credenciales.", "error");
        }
    };

    return (
        <AuthWrapper>
            <Grid container spacing={3}>
                <Grid size={12}>
                    <Stack direction="row" justifyContent="space-between" alignItems="baseline"
                           sx={{mb: {xs: -0.5, sm: 0.5}}}>
                        <Typography variant="h3">Login</Typography>
                    </Stack>
                </Grid>
                <Grid size={12}>
                    <AuthLogin/>
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