"use client";
// material-ui
import { Box } from '@mui/material';

// project import
import MainCard from '@components/Cards/MainCard';
import React from "react";

// ==============================|| AUTHENTICATION - CARD WRAPPER ||============================== //

const AuthCard = ({ children, ...other }:{
    children: React.ReactNode,
    other?: object
}) => {
    return (
        <MainCard
            sx={{
                maxWidth: { xs: 400, lg: 475 },
                margin: { xs: 2.5, md: 3 },
                '& > *': {
                    flexGrow: 1,
                    flexBasis: '50%'
                }
            }}
            content={false}
            {...other}
            border={false}
            boxShadow
            /*shadow={theme.palette.mode === 'light' ? 3 : 0}*/
        >
            <Box sx={{ p: { xs: 2, sm: 3, md: 4, xl: 5 } }}>{children}</Box>
        </MainCard>
    );
};

export default AuthCard;
