"use client";
import Toolbar from '@mui/material/Toolbar';
import Box from '@mui/material/Box';

// project import
import Drawer from './Drawer';
import Header from './Header';
import {Container} from "@mui/material";
import React from "react";
import Breadcrumbs from "@components/Layouts/MainLayout/Breadcrumbs";


export default function MainLayout({children}: {
    children: React.ReactNode
}) {
    return (
        <Box style={{display: "flex", width: "100%"}}>
            <Header/>
            <Drawer/>
            <Box
                component="main"
                sx={{
                    width: 'calc(100% - 260px)',
                    flexGrow: 1,
                    p: {xs: 2, sm: 3},
                }}
            >
                <Toolbar/>
                <Container
                    maxWidth='xl'
                    sx={{
                        px: {xs: 0, sm: 2},
                        position: 'relative',
                        minHeight: 'calc(100vh - 110px)',
                        display: 'flex',
                        flexDirection: 'column'
                    }}
                >
                    <Breadcrumbs />
                    {children}
                </Container>
            </Box>
        </Box>
    )
}