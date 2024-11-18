// material-ui
import { styled, useTheme } from '@mui/material/styles';
import MuiAvatar from '@mui/material/Avatar';

// project import
import getColors from '@utils/getColors';
import React from "react";
import {Theme} from "@mui/system";

// ==============================|| AVATAR - COLOR STYLE ||============================== //

function getColorStyle({ theme, color, type }:{
    theme: Theme;
    color: string;
    type: string;
}) {
    const colors = getColors(theme, color);
    const { lighter, light, main, contrastText } = colors;

    switch (type) {
        case 'filled':
            return {
                color: contrastText,
                backgroundColor: main
            };
        case 'outlined':
            return {
                color: main,
                border: '1px solid',
                borderColor: main,
                backgroundColor: 'transparent'
            };
        case 'combined':
            return {
                color: main,
                border: '1px solid',
                borderColor: light,
                backgroundColor: lighter
            };
        default:
            return {
                color: main,
                backgroundColor: lighter
            };
    }
}

// ==============================|| AVATAR - SIZE STYLE ||============================== //

function getSizeStyle(size: string) {
    switch (size) {
        case 'badge':
            return {
                border: '2px solid',
                fontSize: '0.675rem',
                width: 20,
                height: 20
            };
        case 'xs':
            return {
                fontSize: '0.75rem',
                width: 24,
                height: 24
            };
        case 'sm':
            return {
                fontSize: '0.875rem',
                width: 32,
                height: 32
            };
        case 'lg':
            return {
                fontSize: '1.2rem',
                width: 52,
                height: 52
            };
        case 'xl':
            return {
                fontSize: '1.5rem',
                width: 64,
                height: 64
            };
        case 'md':
        default:
            return {
                fontSize: '1rem',
                width: 40,
                height: 40
            };
    }
}

// ==============================|| STYLED - AVATAR ||============================== //

const AvatarStyle = styled(MuiAvatar, { shouldForwardProp: (prop) => prop !== 'color' && prop !== 'type' && prop !== 'size' })(
    ({ theme, color, type, size }:{
        theme: Theme;
        color: string;
        type: string;
        size: string;
    }) => ({
        ...getSizeStyle(size),
        ...getColorStyle({ theme, color, type }),
        ...(size === 'badge' && {
            borderColor: theme.palette.background.default
        })
    })
);

// ==============================|| EXTENDED - AVATAR ||============================== //

export default function Avatar({children, color = 'primary', type, size = 'md', ...others }:{
    children: React.ReactNode;
    color: string;
    type: string;
    size: string;
    others: object;
}) {
    const theme = useTheme();

    return (
        <AvatarStyle theme={theme} color={color} type={type} size={size} {...others}>
            {children}
        </AvatarStyle>
    );
}