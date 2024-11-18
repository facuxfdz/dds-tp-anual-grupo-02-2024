// material-ui
import {Box} from '@mui/material';
import Grid from '@mui/material/Grid2';
import React from "react";
import Logo from "@components/Logo";
import AuthCard from "@components/Auth/AuthCard";
import AuthBackground from "@components/Auth/AuthBackground";

// ==============================|| AUTHENTICATION - WRAPPER ||============================== //

const AuthWrapper = ({children}: {
    children: React.ReactNode
}) => (
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
                    sx={{minHeight: {xs: 'calc(100vh - 210px)', sm: 'calc(100vh - 134px)', md: 'calc(100vh - 112px)'}}}
                >
                    <Grid>
                        <AuthCard>{children}</AuthCard>
                    </Grid>
                </Grid>
            </Grid>
        </Grid>
    </Box>
);

export default AuthWrapper;