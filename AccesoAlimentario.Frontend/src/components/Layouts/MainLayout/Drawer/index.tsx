import {useMemo} from 'react';

import useMediaQuery from '@mui/material/useMediaQuery';
import Drawer from '@mui/material/Drawer';
import Box from '@mui/material/Box';

// project import
import DrawerHeader from './DrawerHeader';
import DrawerContent from './DrawerContent';
import MiniDrawerStyled from './MiniDrawerStyled';
import {useTheme} from "@mui/material";
import {useAppDispatch, useAppSelector} from "@redux/hook";
import {changeDrawer} from "@redux/features/themeSlice";

// ==============================|| MAIN LAYOUT - DRAWER ||============================== //

export default function MainDrawer() {
    const theme = useTheme();
    const matchDownMD = useMediaQuery(theme.breakpoints.down('lg'));
    const drawerOpen = useAppSelector((state) => state.theme.drawerOpen);
    const dispatch = useAppDispatch();

    // header content
    const drawerContent = useMemo(() => <DrawerContent/>, []);
    const drawerHeader = useMemo(() => <DrawerHeader open={drawerOpen}/>, [drawerOpen]);

    return (
        <Box component="nav" sx={{flexShrink: {md: 0}, zIndex: 1200}}>
            {!matchDownMD ? (
                <MiniDrawerStyled variant="permanent" open={drawerOpen}>
                    {drawerHeader}
                    {drawerContent}
                </MiniDrawerStyled>
            ) : (
                <Drawer
                    variant="temporary"
                    open={drawerOpen}
                    onClose={() => dispatch(changeDrawer())}
                    ModalProps={{keepMounted: true}}
                    sx={{
                        display: {xs: 'block', lg: 'none'},
                        '& .MuiDrawer-paper': {
                            boxSizing: 'border-box',
                            width: "260px",
                            borderRight: `1px solid ${theme.palette.divider}`,
                            backgroundImage: 'none',
                            boxShadow: 'inherit'
                        }
                    }}
                >
                    {drawerHeader}
                    {drawerContent}
                </Drawer>
            )}
        </Box>
    );
}
