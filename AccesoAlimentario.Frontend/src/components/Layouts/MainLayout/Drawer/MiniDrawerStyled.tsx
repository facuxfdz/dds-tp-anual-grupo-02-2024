// material-ui
import {styled} from '@mui/material/styles';
import Drawer from '@mui/material/Drawer';
import {alpha} from "@mui/material";

// Definir la interfaz para las props del Drawer estilizado
interface MiniDrawerStyledProps {
    open?: boolean;
}

// ==============================|| DRAWER - MINI STYLED ||============================== //

const MiniDrawerStyled = styled(Drawer, {shouldForwardProp: (prop) => prop !== 'open'})<MiniDrawerStyledProps>(({
                                                                                                                    theme,
                                                                                                                    open
                                                                                                                }) => ({
    width: "260px",
    flexShrink: 0,
    whiteSpace: 'nowrap',
    boxSizing: 'border-box',
    ...(open && {
        width: "260px",
        borderRight: `1px solid ${theme.palette.divider}`,
        transition: theme.transitions.create('width', {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.enteringScreen
        }),
        overflowX: 'hidden',
        boxShadow: theme.palette.mode === 'dark' ? `0px 1px 1px rgb(0 0 0 / 14%), 0px 2px 1px rgb(0 0 0 / 12%), 0px 1px 3px rgb(0 0 0 / 20%)` : 'none',
        '& .MuiDrawer-paper': {
            width: "260px",
            borderRight: `1px solid ${theme.palette.divider}`,
            transition: theme.transitions.create('width', {
                easing: theme.transitions.easing.sharp,
                duration: theme.transitions.duration.enteringScreen
            }),
            overflowX: 'hidden',
            boxShadow: theme.palette.mode === 'dark' ? `0px 1px 1px rgb(0 0 0 / 14%), 0px 2px 1px rgb(0 0 0 / 12%), 0px 1px 3px rgb(0 0 0 / 20%)` : 'none',
        },
    }),
    ...(!open && {
        transition: theme.transitions.create('width', {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen
        }),
        overflowX: 'hidden',
        width: theme.spacing(7.5),
        borderRight: 'none',
        boxShadow: theme.palette.mode === 'dark'
            ? `0px 1px 1px rgb(0 0 0 / 14%), 0px 2px 1px rgb(0 0 0 / 12%), 0px 1px 3px rgb(0 0 0 / 20%)`
            : `0px 1px 4px ${alpha(theme.palette.grey[900], 0.08)}`,
        '& .MuiDrawer-paper': {
            transition: theme.transitions.create('width', {
                easing: theme.transitions.easing.sharp,
                duration: theme.transitions.duration.leavingScreen
            }),
            overflowX: 'hidden',
            width: theme.spacing(7.5),
            borderRight: 'none',
            boxShadow: theme.palette.mode === 'dark'
                ? `0px 1px 1px rgb(0 0 0 / 14%), 0px 2px 1px rgb(0 0 0 / 12%), 0px 1px 3px rgb(0 0 0 / 20%)`
                : `0px 1px 4px ${alpha(theme.palette.grey[900], 0.08)}`,
        },
    }),
}));

export default MiniDrawerStyled;
