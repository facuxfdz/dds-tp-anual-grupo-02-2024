// material-ui
import {styled} from '@mui/material/styles';
import Box from '@mui/material/Box';

// Definir la interfaz para las props del componente estilizado
interface DrawerHeaderStyledProps {
    open: boolean;
}

const DrawerHeaderStyled = styled(Box, {shouldForwardProp: (prop) => prop !== 'open'})<DrawerHeaderStyledProps>(
    ({theme, open}) => ({
        ...theme.mixins.toolbar,
        display: 'flex',
        alignItems: 'center',
        justifyContent: open ? 'flex-start' : 'center',
        paddingLeft: theme.spacing(open ? 3 : 0)
    })
);


export default DrawerHeaderStyled;