"use client";
import React, {useMemo} from 'react';

// material-ui
import {styled, useTheme} from '@mui/material/styles';
import useMediaQuery from '@mui/material/useMediaQuery';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';

// project import
import HeaderContent from './HeaderContent';
import {changeDrawer} from "@redux/features/themeSlice";
import {useAppDispatch, useAppSelector} from "@redux/hook";
import {Box, Icon, Typography} from "@mui/material";
import {ThemeChanger} from "@components/themes/themeButton/themeButton";


// ==============================|| MAIN LAYOUT - HEADER ||============================== //

interface AppBarStyledProps {
    open?: boolean;
}

const AppBarStyled = styled(AppBar, {shouldForwardProp: (prop) => prop !== 'open'})<AppBarStyledProps>(({
                                                                                                            theme,
                                                                                                            open
                                                                                                        }) => ({
    zIndex: theme.zIndex.drawer + 1,
    transition: theme.transitions.create(['width', 'margin'], {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen
    }),
    ...(!open && {
        width: `calc(100% - ${theme.spacing(7.5)})`
    }),
    ...(open && {
        marginLeft: "260px",
        width: `calc(100% - 260px)`,
        transition: theme.transitions.create(['width', 'margin'], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.enteringScreen
        })
    })
}));

export default function Header(
    {
        useInfoHeader = false
    }:
        {
            useInfoHeader?: boolean
        }
) {
    const theme = useTheme();
    const downLG = useMediaQuery(theme.breakpoints.down('lg'));
    const dispatch = useAppDispatch();
    const drawerOpen = useAppSelector((state) => state.theme.drawerOpen);
    const customization = useAppSelector((state) => state.customization);

    // header content
    const headerContent = useMemo(() => <HeaderContent/>, []);

    // common header
    const mainHeader = (
        <Toolbar>
            <IconButton
                aria-label="open drawer"
                onClick={() => dispatch(changeDrawer())}
                edge="start"
                color="secondary"
                sx={{
                    color: 'text.primary',
                    ml: {xs: 0, lg: -2}
                }}
            >
                {!drawerOpen
                    ? <Icon className="fa-sharp-duotone fa-solid fa-angles-right"/>
                    : <Icon className="fa-sharp-duotone fa-solid fa-angles-left"/>}
            </IconButton>
            {headerContent}
        </Toolbar>
    );

    const informationHeader = (
        <Toolbar>
            <Box
                sx={{
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "center",
                    height: 45,
                    width: "auto",
                    mr: 4
                }}
                component={"img"}
                src={customization.logo}
                alt="logo"
            />
            <Typography variant="h6" noWrap component="div" sx={{flexGrow: 1}}>
                Acceso Alimentario
            </Typography>
            <Box sx={{mr: 1}}/>
            <ThemeChanger/>
        </Toolbar>
    );

    return (
        <>
            {!downLG ? (
                <AppBarStyled
                    open={drawerOpen}
                    position="fixed"
                    color="inherit"
                    elevation={0}
                    sx={{
                        borderBottom: `1px solid ${theme.palette.divider}`,
                        zIndex: 1200,
                        width: useInfoHeader ? '100%' :
                            drawerOpen ? 'calc(100% - 260px)' : {
                                xs: '100%',
                                lg: 'calc(100% - 60px)'
                            }
                    }}
                >
                    {useInfoHeader ? informationHeader : mainHeader}
                </AppBarStyled>
            ) : (
                <AppBar
                    position="fixed"
                    color="inherit"
                    elevation={0}
                    sx={{
                        borderBottom: `1px solid ${theme.palette.divider}`,
                        zIndex: 1200,
                        width: useInfoHeader ? '100%' :
                            drawerOpen ? 'calc(100% - 260px)' : {
                                xs: '100%',
                                lg: 'calc(100% - 60px)'
                            }
                    }}
                >
                    {useInfoHeader ? informationHeader : mainHeader}
                </AppBar>
            )}
        </>
    );
}
