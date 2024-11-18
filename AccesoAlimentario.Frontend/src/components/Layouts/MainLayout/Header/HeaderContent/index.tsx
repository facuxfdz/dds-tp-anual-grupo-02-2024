// material-ui
import useMediaQuery from '@mui/material/useMediaQuery';
import Box from '@mui/material/Box';
// import MobileSection from "@/app/test/Header/HeaderContent/MobileSection";
import {useTheme} from "@mui/material";
import React from "react";
import Profile from "@components/Layouts/MainLayout/Header/HeaderContent/Profile";
/*import IconButton from "@mui/material/IconButton";
import Link from "next/link";*/

// ==============================|| HEADER - CONTENT ||============================== //

export default function HeaderContent() {
    const theme = useTheme();
    const downLG = useMediaQuery(theme.breakpoints.down('lg'));

    return (
        <>
            {/*{!downLG && <Search />}*/}
            {downLG && <Box sx={{ width: '100%', ml: 1 }} />}

            <Box sx={{ flexGrow: 1 }} />

            {/*<Notification />*/}
            <Profile />
            {/*{downLG && <MobileSection />}*/}
        </>
    );
}
