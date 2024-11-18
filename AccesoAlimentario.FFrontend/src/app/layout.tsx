import type {Metadata} from "next";
import React from "react";
import {Providers} from "@redux/providers";
import ThemeClient from "@components/themes/themeClient/themeClient";
import "@styles/fonts/fa.all.css";

import {Inter, Poppins, Roboto, Public_Sans} from 'next/font/google';
import {NotificationProvider} from "@components/Notifications/NotificationContext";

const inter = Inter({
    subsets: ['latin'],
    weight: ['400', '500', '600', '700'],
    display: 'swap',
    variable: '--font-inter',
});

const poppins = Poppins({
    subsets: ['latin'],
    weight: ['400', '500', '600', '700'],
    display: 'swap',
    variable: '--font-poppins',
});

const roboto = Roboto({
    subsets: ['latin'],
    weight: ['400', '500', '700'],
    display: 'swap',
    variable: '--font-roboto',
});

const publicSans = Public_Sans({
    subsets: ['latin'],
    weight: ['400', '500', '600', '700'],
    display: 'swap',
    variable: '--font-public-sans',
});

export const metadata: Metadata = {
    title: "Acceso Alimentario",
    description: "Sistema para la Mejora del Acceso Alimentario en Contextos de Vulnerabilidad Socioeconómica ",
};

export default function RootLayout({
                                       children,
                                   }: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <html lang="en" className={`${inter.variable} ${poppins.variable} ${roboto.variable} ${publicSans.variable}`}>
        <body>
        <Providers>
            <NotificationProvider>
                <ThemeClient>
                    {children}
                </ThemeClient>
            </NotificationProvider>
        </Providers>
        </body>
        </html>
    );
}
