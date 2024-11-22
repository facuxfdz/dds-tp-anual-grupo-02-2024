"use client";
import {Stack} from "@mui/material";
import Grid from "@mui/material/Grid2"
import AuthWrapper from "@components/Auth/AuthWrapper";
import Typography from "@mui/material/Typography";
import React from "react";
import AuthLogin from "@components/Auth/AuthLogin";
import { GoogleLogin } from "@react-oauth/google";
import { setUser } from '@redux/features/userSlice';
import { setSession } from '@redux/features/sessionSlice';
import { useDispatch } from 'react-redux';
import { config } from '@config/config';
import { useRouter } from 'next/navigation'
import { parseJwt } from "@utils/decode_jwt";
import { useGoogleLogin } from '@react-oauth/google';





export default function LoginPage() {

    const dispatch = useDispatch();
    const router = useRouter();
    const login = useGoogleLogin({
        onSuccess: (response) => handleSuccess(response),
        onError: () => handleFailure()
    })

    const handleSuccess = async (credentialResponse: any) => {
        try {
            const jwtToken = credentialResponse.credential;

            // Call the backend to authenticate the user and get the user information
            const response = await fetch(`${config.apiUrl}/auth/login`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ token: jwtToken }),
                credentials: 'include',  // To ensure cookies are included in the request
            });
    
            if (response.ok) {
                const data = await response.json();  // Destructure the data from the backend
                const jsonRes = parseJwt(data.token);
                const user = {
                    id: jsonRes.aud,
                    name: jsonRes.name,
                    email: jsonRes.email,
                    profile_picture: jsonRes.picture
                }

                // Dispatch the action to update the Redux state with the user info
                console.log(user);
                dispatch(setUser(user));  // Assuming you have a setUser action
                dispatch(setSession(data.token));  // Assuming you have a setSession action
                // Optionally, handle any other necessary state changes here
    
            } else {
                console.error('Login failed', await response.json());
            }
        } catch (error) {
            console.error('Error during login request:', error);
        }

        router.push("/admin/inicio");

    };
    
    const handleFailure = () => {
        console.log("Error");
    }

    const GoogleButton = ({ onClick }: any) => {
        return (
            <button onClick={onClick} style={{ width: '100%', backgroundColor: '#f45b39', color: 'white', padding: '15px 25px', borderRadius: '5px', border: 'none', cursor: 'pointer' }}>
                Sign in with Google
            </button>
        );
    }

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
                    <GoogleButton onClick={() => login()}/>
                </Grid>
            </Grid>
        </AuthWrapper>
    );
}