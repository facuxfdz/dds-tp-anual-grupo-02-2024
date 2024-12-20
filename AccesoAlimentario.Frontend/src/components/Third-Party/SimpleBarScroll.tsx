// material-ui
import {alpha, styled} from '@mui/material/styles';
import Box from '@mui/material/Box';

// third-party
import SimpleBar from 'simplebar-react';
import {BrowserView, MobileView} from 'react-device-detect';
import {SxProps, Theme} from '@mui/system';
import React from "react";


// root style
const RootStyle = styled('div')({
    flexGrow: 1,
    height: '100%',
    overflow: 'hidden'
});

// scroll bar wrapper
const SimpleBarStyle = styled(SimpleBar)(({theme}) => ({
    maxHeight: '100%',
    '& .simplebar-scrollbar': {
        '&:before': {
            background: alpha(theme.palette.grey[500], 0.48)
        },
        '&.simplebar-visible:before': {
            opacity: 1
        }
    },
    '& .simplebar-track.simplebar-vertical': {
        width: 10
    },
    '& .simplebar-track.simplebar-horizontal .simplebar-scrollbar': {
        height: 6
    },
    '& .simplebar-mask': {
        zIndex: 'inherit'
    }
}));

// ==============================|| SIMPLE SCROLL BAR ||============================== //

export default function SimpleBarScroll({children, sx, ...other}: {
    children: React.ReactNode;
    sx: SxProps<Theme>;
    other?: Record<string, unknown>;
}) {
    return (
        <>
            <RootStyle>
                <BrowserView>
                    <SimpleBarStyle clickOnTrack={false} sx={sx} {...other}>
                        {children}
                    </SimpleBarStyle>
                </BrowserView>
            </RootStyle>
            <MobileView>
                <Box sx={{overflowX: 'auto', ...sx}} {...other}>
                    {children}
                </Box>
            </MobileView>
        </>
    );
}
