// material-ui
import List from '@mui/material/List';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';

// project import
import NavItem from './NavItem';
import {useAppSelector} from "@redux/hook";
import {useTheme} from "@mui/material";
import NavCollapse from "@components/Layouts/MainLayout/Drawer/DrawerContent/Navigation/NavCollapse";
import {IMenuItem} from "@utils/getMenuItems";

export default function NavGroup({item}: { item: IMenuItem }) {
    const theme = useTheme();
    const drawerOpen = useAppSelector((state) => state.theme.drawerOpen);
    const user = useAppSelector((state) => state.user);

    const filterMenuItemRol = (item : IMenuItem) => {
        if (item.tecnicos && user.tecnicoId != "") {
            return true;
        }
        if (item.colaboradores && user.colaboradorId != "") {
            if (!item.tarjetaColaborador)
            {
                return true;
            } else {
                if (user.tarjetaColaboracionId != "") {
                    return true;
                }
            }
        }
        return false;
    }

    const filterMenuItemTipoPersona = (item : IMenuItem) => {
        if (item.personaHumana && user.personaTipo == "Humana") {
            return true;
        }
        if (item.personaJuridica && user.personaTipo == "Juridica") {
            return true;
        }
        return false;
    }

    const navCollapse = item.children?.map((menuItem: IMenuItem) => {
        if (!filterMenuItemRol(menuItem)) {
            return null;
        }
        if (!filterMenuItemTipoPersona(menuItem)) {
            return null;
        }
        switch (menuItem.type) {
            case 'collapse':
                return (
                    <NavCollapse
                        key={menuItem.id}
                        menu={menuItem}
                        level={1}
                    />
                );
            case 'item':
                return <NavItem key={menuItem.id} item={menuItem} level={1} />;
            default:
                return null;
        }
    });

    return (
        <List
            subheader={
                item.title &&
                drawerOpen && (
                    <Box sx={{ pl: 3, mb: 1.5 }}>
                        <Typography variant="subtitle2" color={theme.palette.mode === 'dark' ? 'textSecondary' : 'text.secondary'}>
                            {item.title}
                        </Typography>
                        {item.caption && (
                            <Typography variant="caption" color="secondary">
                                {item.caption}
                            </Typography>
                        )}
                    </Box>
                )
            }
            sx={{ mt: drawerOpen && item.title ? 1.5 : 0, py: 0, zIndex: 0 }}
        >
            {navCollapse}
        </List>
    )
}
