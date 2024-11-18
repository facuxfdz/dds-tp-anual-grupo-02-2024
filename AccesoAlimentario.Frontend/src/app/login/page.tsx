import {Stack} from "@mui/material";
import Grid from "@mui/material/Grid2"
import AuthWrapper from "@components/Auth/AuthWrapper";
import Typography from "@mui/material/Typography";
import React from "react";
import AuthLogin from "@components/Auth/AuthLogin";
/*import Link from "next/link";*/

export default function LoginPage() {
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
                </Grid>
            </Grid>
        </AuthWrapper>
    );
}