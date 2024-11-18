"use client";
import React from 'react';

// material-ui
import {
    Button,
    Grid2 as Grid,
    InputAdornment,
    InputLabel,
    OutlinedInput,
    Stack,
} from '@mui/material';
import IconButton from "@mui/material/IconButton";
import Next from "next/link";
import {inicioRoute} from "@routes/router";


// ============================|| JWT - LOGIN ||============================ //

const AuthLogin = () => {
    const [password, setPassword] = React.useState('');
    const [username, setUsername] = React.useState('');
    const [showPassword, setShowPassword] = React.useState(false);

    const handleClickShowPassword = () => {
        setShowPassword(!showPassword);
    };

    return (
        <Grid container spacing={3}>
            <Grid size={12}>
                <Stack spacing={1}>
                    <InputLabel htmlFor="username-login">Usuario</InputLabel>
                    <OutlinedInput
                        id="username-login"
                        type="text"
                        value={username}
                        name="username"
                        onChange={(e) => setUsername(e.target.value)}
                        placeholder="Ingrese su usuario"
                        fullWidth
                    />
                </Stack>
            </Grid>
            <Grid size={12}>
                <Stack spacing={1}>
                    <InputLabel htmlFor="password-login">Contraseña</InputLabel>
                    <OutlinedInput
                        fullWidth
                        id="-password-login"
                        type={showPassword ? 'text' : 'password'}
                        value={password}
                        name="password"
                        onChange={(e) => setPassword(e.target.value)}
                        endAdornment={
                            <InputAdornment position="end">
                                <IconButton
                                    aria-label="Invertir visibilidad de la contraseña"
                                    onClick={handleClickShowPassword}
                                    edge="end"
                                    color="secondary"
                                >
                                    {showPassword ? <i className="fa-duotone fa-solid fa-eye-slash"/> :
                                        <i className="fa-duotone fa-solid fa-eye"/>}
                                </IconButton>
                            </InputAdornment>
                        }
                        placeholder="Ingrese su contraseña"
                    />
                </Stack>
            </Grid>
            <Grid size={12}>
                <Button disableElevation disabled={false} fullWidth size="large" type="submit" variant="contained"
                        color="primary" component={Next} href={inicioRoute()}>
                    Ingresar
                </Button>
            </Grid>
        </Grid>
    );
};

export default AuthLogin;
