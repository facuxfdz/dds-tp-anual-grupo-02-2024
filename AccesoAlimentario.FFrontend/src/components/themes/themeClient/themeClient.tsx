"use client";
import {useAppSelector} from "@/redux/hook";
import {createTheme, ThemeProvider} from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";
import React, {useMemo} from "react";
import {alpha} from "@mui/material";
import ThemeOption from './defaultTheme';
import Typography from "@components/themes/themeClient/typography";

const ThemeClient: React.FC<{ children: React.ReactNode }> = ({children}) => {
    const themeMode = useAppSelector((state) => state.theme.mode);
    const fontFamily = useAppSelector((state) => state.theme.fontFamily);

    const font = useMemo(() => {
        switch (fontFamily) {
            case 'Inter':
                return "var(--font-inter)";
            case 'Poppins':
                return "var(--font-poppins)";
            case 'Public Sans':
                return "var(--font-public-sans)";
            case 'Roboto':
                return "var(--font-roboto)";
            default:
                return "var(--font-inter)";
        }
    }, [fontFamily]);

    const paletteColor = ThemeOption(themeMode);
    const themeTypography = useMemo(() => Typography(font), [font]);

    const theme = useMemo(
        () =>
            createTheme({
                palette: {
                    mode: themeMode,
                    common: {
                        black: '#000',
                        white: '#fff'
                    },
                    ...paletteColor,
                    text: {
                        primary: themeMode === 'dark' ? alpha(paletteColor.grey[900], 0.87) : paletteColor.grey[700],
                        secondary: themeMode === 'dark' ? alpha(paletteColor.grey[900], 0.45) : paletteColor.grey[500],
                        disabled: themeMode === 'dark' ? alpha(paletteColor.grey[900], 0.1) : paletteColor.grey[400]
                    },
                    action: {
                        disabled: paletteColor.grey[300]
                    },
                    divider: themeMode === 'dark' ? alpha(paletteColor.grey[900], 0.05) : paletteColor.grey[200],
                    background: {
                        paper: themeMode === 'dark' ? paletteColor.grey[100] : paletteColor.grey[0],
                        default: paletteColor.grey.A50
                    }
                },
                mixins: {
                    toolbar: {
                        minHeight: 60,
                        paddingTop: 8,
                        paddingBottom: 8
                    }
                },
                typography: themeTypography,
            }),
        [themeMode, paletteColor, themeTypography]
    );

    return (
        <ThemeProvider theme={theme}>
            <CssBaseline/>
            {children}
        </ThemeProvider>
    );
};

export default ThemeClient;
