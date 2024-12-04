import React from "react";
import { Grid, Card, CardContent, Typography, Box, CardActionArea } from "@mui/material";
import { contribucionesRoute, heladerasRoute, personaVulnerableRegistroRoute, premiosRoute, reportarIncidenciaRoute, reportesRoute } from "@/routes/router";
import NextLink from "next/link";

const Home: React.FC = () => {
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
      <Grid container spacing={3} sx={{ maxWidth: 800 }}>
        {/* Opciones para colaboradores */}
        {menuItems.map((item) => (
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

// Configuración de las opciones del menú
const menuItems = [
  { title: "Premios", icon: "fa-duotone fa-solid fa-gift", href:premiosRoute()},
  { title: "Contribuir", icon: "fa-duotone fa-solid fa-hand-holding-usd", href: contribucionesRoute() },
  { title: "Registrar persona vulnerable", icon: "fa-duotone fa-solid fa-user-injured", href:personaVulnerableRegistroRoute()},
  { title: "Heladeras", icon: "fa-duotone fa-solid fa-ice-cream", href:heladerasRoute()},
  { title: "Reportar incidencias", icon: "fa-duotone fa-solid fa-exclamation-triangle", href: reportarIncidenciaRoute() },
  { title: "Ver reportes", icon: "fa-duotone fa-solid fa-chart-bar", href:reportesRoute() }
  
];

export default Home;
