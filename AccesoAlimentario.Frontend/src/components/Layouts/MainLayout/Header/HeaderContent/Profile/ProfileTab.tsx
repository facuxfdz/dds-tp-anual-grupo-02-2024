// material-ui
import { List, ListItemButton, ListItemIcon, ListItemText } from '@mui/material';
import NextLink from "next/link";
import {loginRoute, perfilRoute} from "@routes/router";

// ==============================|| HEADER PROFILE - PROFILE TAB ||============================== //

const ProfileTab = () => {
    return (
        <List component="nav" sx={{p: 0, '& .MuiListItemIcon-root': {minWidth: 32}}}>
            <ListItemButton component={NextLink} href={perfilRoute()}>
                <ListItemIcon>
                    <i className="fa-duotone fa-solid fa-user"></i>
                </ListItemIcon>
                <ListItemText primary="Ver Perfil"/>
            </ListItemButton>
            <ListItemButton component={NextLink} href={loginRoute()}>
                <ListItemIcon>
                    <i className="fa-duotone fa-solid fa-right-from-bracket"></i>
                </ListItemIcon>
                <ListItemText primary="Cerrar Sesión"/>
            </ListItemButton>
        </List>
    );
};

export default ProfileTab;
