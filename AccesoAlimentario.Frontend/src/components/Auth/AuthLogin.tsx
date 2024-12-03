import React, {useState} from "react";
import {
    Button,
    Grid2 as Grid,
    InputAdornment,
    InputLabel,
    OutlinedInput,
    Stack,
} from "@mui/material";
import IconButton from "@mui/material/IconButton"
import {useNotification} from "@components/Notifications/NotificationContext";
import {usePostLoginMutation} from "@redux/services/authApi";

// ============================|| JWT - LOGIN ||============================ //

const AuthLogin = ({
                       isLoading,
                       handleLogin,
                   }: {
    isLoading: boolean;
    handleLogin: (response: { userExists: boolean; token: string }) => void;
}) => {
    const [password, setPassword] = useState("");
    const [username, setUsername] = useState("");
    const [showPassword, setShowPassword] = useState(false);
    const {addNotification} = useNotification();
    const [
        postLogin,
        {isLoading: loginIsLoading}
    ] = usePostLoginMutation();

    const handleClickShowPassword = () => {
        setShowPassword(!showPassword);
    };

    const handleSubmit = async () => {
        if (!username || !password) {
            addNotification("Por favor, ingrese un usuario y contraseña", "error");
            return;
        }
        try {
            const response = await postLogin({username, password}).unwrap();
            handleLogin(response);
        } catch {
            addNotification("Error al iniciar sesión", "error");
        }
    }

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
                                        <i className="fa-duotone fa-solid fa-eye-slash"/>
                                    ) : (
                                        <i className="fa-duotone fa-solid fa-eye"/>
                                    )}
                                </IconButton>
                            </InputAdornment>
                        }
                        placeholder="Ingrese su contraseña"
                    />
                </Stack>
            </Grid>
            <Grid size={12}>
                <Button
                    disableElevation
                    disabled={loginIsLoading || isLoading}
                    fullWidth
                    type="submit"
                    variant="contained"
                    color="primary"
                    onClick={handleSubmit}
                >
                    Ingresar
                </Button>
            </Grid>
        </Grid>
    );
};

export default AuthLogin;
