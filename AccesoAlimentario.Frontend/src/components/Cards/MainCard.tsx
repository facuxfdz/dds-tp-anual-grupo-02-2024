"use client";
// material-ui
import {useTheme} from '@mui/material/styles';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardHeader from '@mui/material/CardHeader';
import Typography from '@mui/material/Typography';
import {alpha, Divider} from "@mui/material";
import React, {forwardRef, Ref} from "react";

// header style
const headerSX = {
    p: 2.5,
    '& .MuiCardHeader-action': {m: '0px auto', alignSelf: 'center'}
};

const MainCard =forwardRef( ({
                                 border = true,
                                 boxShadow,
                                 children,
                                 subheader,
                                 content = true,
                                 contentSX = {},
                                 darkTitle,
                                 divider = true,
                                 elevation,
                                 secondary,
                                 shadow,
                                 sx = {},
                                 title,
                                 modal = false,
                                 ...others
                             }: {
    border?: boolean,
    boxShadow?: boolean,
    children?: React.ReactNode,
    subheader?: React.ReactNode,
    content?: boolean,
    contentSX?: object,
    darkTitle?: boolean,
    divider?: boolean,
    elevation?: number,
    secondary?: React.ReactNode,
    shadow?: string,
    sx?: object,
    title?: React.ReactNode,
    modal?: boolean
}, ref: Ref<HTMLDivElement>) => {
    const theme = useTheme();
    boxShadow = theme.palette.mode === 'dark' ? boxShadow || true : boxShadow;

    return (
        <Card
            elevation={elevation || 0}
            ref={ref}
            {...others}
            sx={{
                position: 'relative',
                border: border ? '1px solid' : 'none',
                borderRadius: 1,
                borderColor: theme.palette.mode === 'dark' ? theme.palette.divider : "#d3d8db",
                boxShadow: boxShadow && (!border || theme.palette.mode === 'dark') ? shadow ||
                    (
                        theme.palette.mode === 'dark'
                        ? `0px 1px 1px rgb(0 0 0 / 14%), 0px 2px 1px rgb(0 0 0 / 12%), 0px 1px 3px rgb(0 0 0 / 20%)`
                        : `0px 1px 4px ${alpha(theme.palette.grey[900], 0.08)}`
                    )
                    : 'inherit',
                ':hover': {
                    boxShadow: boxShadow ? shadow || theme.palette.mode === 'dark'
                        ? `0px 1px 1px rgb(0 0 0 / 14%), 0px 2px 1px rgb(0 0 0 / 12%), 0px 1px 3px rgb(0 0 0 / 20%)`
                        : `0px 1px 4px ${alpha(theme.palette.grey[900], 0.08)}` : 'inherit'
                },
                ...(modal && {
                    position: 'absolute',
                    top: '50%',
                    left: '50%',
                    transform: 'translate(-50%, -50%)',
                    width: {xs: `calc( 100% - 50px)`, sm: 'auto'},
                    '& .MuiCardContent-root': {
                        overflowY: 'auto',
                        minHeight: 'auto',
                        maxHeight: `calc(100vh - 200px)`
                    }
                }),
                ...sx
            }}
        >
            {/* card header and action */}
            {!darkTitle && title && (
                <CardHeader
                    sx={headerSX}
                    titleTypographyProps={{variant: 'subtitle1'}}
                    title={title}
                    action={secondary}
                    subheader={subheader}
                />
            )}
            {darkTitle && title &&
                <CardHeader sx={headerSX} title={<Typography variant="h4">{title}</Typography>} action={secondary}/>}

            {/* content & header divider */}
            {title && divider && <Divider/>}

            {/* card content */}
            {content && <CardContent sx={contentSX}>{children}</CardContent>}
            {!content && children}
        </Card>
    );
});

MainCard.displayName = 'MainCard';

export default MainCard;