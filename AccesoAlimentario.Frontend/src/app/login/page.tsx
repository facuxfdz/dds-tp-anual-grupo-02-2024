"use client";
import {Stack} from "@mui/material";
import Grid from "@mui/material/Grid2"
import AuthWrapper from "@components/Auth/AuthWrapper";
import Typography from "@mui/material/Typography";
import React from "react";
import AuthLogin from "@components/Auth/AuthLogin";
import { GoogleLogin } from "@react-oauth/google";
import { setUser } from '@redux/features/userSlice';
import { useDispatch } from 'react-redux';
import { config } from '@config/config';

/*import Link from "next/link";*/

export default function LoginPage() {

    const dispatch = useDispatch();

    const handleSuccess = async (credentialResponse: any) => {
        try {
            const jwtToken = credentialResponse.credential;
            console.log(jwtToken);
            console.log(credentialResponse)
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
                console.log(data);
    
                // Dispatch the action to update the Redux state with the user info
                dispatch(setUser(data.user));  // Assuming you have a setUser action
    
                // Optionally, handle any other necessary state changes here
    
            } else {
                console.error('Login failed', await response.json());
            }
        } catch (error) {
            console.error('Error during login request:', error);
        }
    };
    
    const handleFailure = () => {
        console.log("Error");
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
                    <GoogleLogin 
                        onSuccess={(credentialResponse) => {
                            handleSuccess(credentialResponse);
                        }}
                        onError={() => {
                            handleFailure();
                        }}
                    />
                </Grid>
            </Grid>
        </AuthWrapper>
    );
}