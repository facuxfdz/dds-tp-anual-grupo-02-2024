"use client";
import React from "react";
import {Grid, Card, CardContent, Typography, Box, CardActionArea} from "@mui/material";
import {
    accesosRoute,
    contribucionesRoute,
    heladerasRoute, incidentesRoute,
    personaVulnerableRegistroRoute,
    premiosRoute,
    reportarIncidenciaRoute,
    reportesRoute, suscripcionesRoute
} from "@/routes/router";
import NextLink from "next/link";
import {useAppSelector} from "@redux/hook";

interface MenuItem {
    title: string;
    icon: string;
    href: string;
}

const Home: React.FC = () => {
    const user = useAppSelector((state) => state.user);

    const getMenuItems = (): MenuItem[] => {
        const menuItems: MenuItem[] = [];

        menuItems.push({title: "Heladeras", icon: "fa-duotone fa-solid fa-ice-cream", href: heladerasRoute()});

        if (user.colaboradorId != "")
        {
            menuItems.push({title: "Contribuciones", icon: "fa-duotone fa-solid fa-hand-holding-usd", href: contribucionesRoute()});
            menuItems.push({title: "Registrar persona vulnerable", icon: "fa-duotone fa-solid fa-user-injured", href: personaVulnerableRegistroRoute()});
            menuItems.push({title: "Premios", icon: "fa-duotone fa-solid fa-gift", href: premiosRoute()});
            menuItems.push({title: "Suscripciones", icon: "fa-duotone fa-solid fa-envelope", href: suscripcionesRoute()});
        }

        if (user.tarjetaColaboracionId != "")
        {
            menuItems.push({title: "Accesos", icon: "fa-sharp-duotone fa-light fa-key", href: accesosRoute()});

        }

        if (user.tecnicoId != "") {
            menuItems.push({title: "Incidentes", icon: "fa-duotone fa-solid fa-exclamation-circle", href: incidentesRoute()});
        }

        menuItems.push({title: "Reportar incidencia", icon: "fa-duotone fa-solid fa-exclamation-triangle", href: reportarIncidenciaRoute()});

        menuItems.push({title: "Ver reportes", icon: "fa-duotone fa-solid fa-chart-bar", href: reportesRoute()});

        return menuItems;
    }

    return (
        <Box
            sx={{
                display: "flex",
                flexDirection: "column",
                alignItems: "center",
                justifyContent: "center",
            }}
        >
            <Typography variant="h2" component="h1" gutterBottom>
                Sistema para la Mejora del Acceso Alimentario
            </Typography>
            <Typography variant="subtitle1" color="textSecondary" gutterBottom>
                Nuestros servicios disponibles
            </Typography>
            <Grid container spacing={3} sx={{maxWidth: 800}}>
                {/* Opciones para colaboradores */}
                {getMenuItems().map((item) => (
                    <Grid item xs={12} sm={6} md={4} key={item.title}>
                        <DashboardCard title={item.title} icon={item.icon} href={item.href}/>
                    </Grid>
                ))}
            </Grid>
        </Box>
    );
};

const DashboardCard = ({
                           title,
                           icon,
                           href,
                       }: {
    title: string;
    icon: string;
    href: string;
}) => {
    return (

        <Card
            sx={{
                textAlign: "center",
                padding: 2,
                borderRadius: 2,
                boxShadow: 3,
                "&:hover": {
                    boxShadow: 6,
                },
                minHeight: 175,
                alignItems: "center",
                justifyContent: "center",
                display: "flex",
            }}

        >
            <CardActionArea
                LinkComponent={NextLink}
                href={href}
            >
                <CardContent>
                    <Box
                        sx={{
                            fontSize: "2.5rem",
                            color: "#3f51b5",
                            marginBottom: 2,
                        }}
                        className={icon}
                    />
                    <Typography variant="h6" component="div">
                        {title}
                    </Typography>
                </CardContent>
            </CardActionArea>
        </Card>
    );
};

export default Home;
