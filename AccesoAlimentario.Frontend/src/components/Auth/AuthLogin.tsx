"use client";
import React, { useState, useEffect } from "react";
import {
    Button,
    Grid2 as Grid,
    InputAdornment,
    InputLabel,
    OutlinedInput,
    Stack,
} from "@mui/material";
import IconButton from "@mui/material/IconButton";
import { useRouter } from "next/navigation";

// ============================|| JWT - LOGIN ||============================ //

const AuthLogin = () => {
    const [password, setPassword] = useState("");
    const [username, setUsername] = useState("");
    const [showPassword, setShowPassword] = useState(false);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState(null);

    const router = useRouter();

    const handleClickShowPassword = () => {
        setShowPassword(!showPassword);
    };

    useEffect(() => {
        if (isSubmitting) {
            const authenticateUser = async () => {
                try {
                    const response = await fetch("http://localhost:5000/api/auth/login", {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json",
                        },
                        mode: "no-cors",
                        body: JSON.stringify({ username, password }),
                    });

                    if (!response.ok) {
                        throw new Error("Error en la autenticación. Por favor, verifica tus credenciales.");
                    }

                    const data = await response.json();

                    // Guardar token en almacenamiento local o cookies si es necesario
                    if (data.token) {
                        localStorage.setItem("authToken", data.token);
                        router.push("/admin/inicio"); // Redirige a la ruta deseada
                    } else {
                        throw new Error("Autenticación fallida. Token no recibido.");
                    }
                } catch (err : any) {
                    setError(err.message);
                } finally {
                    setIsSubmitting(false);
                }
            };

            authenticateUser();
        }
    }, [isSubmitting, username, password, router]);

    const handleSubmit = (e : any) => {
        e.preventDefault();
        setError(null); // Resetear errores previos
        setIsSubmitting(true);
    };

    return (
        <Grid container spacing={3} marginBottom={2}>
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
                        type={showPassword ? "text" : "password"}
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
                                    {showPassword ? (
                                        <i className="fa-duotone fa-solid fa-eye-slash" />
                                    ) : (
                                        <i className="fa-duotone fa-solid fa-eye" />
                                    )}
                                </IconButton>
                            </InputAdornment>
                        }
                        placeholder="Ingrese su contraseña"
                    />
                </Stack>
            </Grid>
            {error && (
                <Grid size={12}>
                    <p style={{ color: "red" }}>{error}</p>
                </Grid>
            )}
            <Grid size={12}>
                <Button
                    disableElevation
                    disabled={isSubmitting}
                    fullWidth
                    size="large"
                    type="submit"
                    variant="contained"
                    color="primary"
                    onClick={handleSubmit}
                >
                    {isSubmitting ? "Ingresando..." : "Ingresar"}
                </Button>
            </Grid>
        </Grid>
    );
};

export default AuthLogin;
