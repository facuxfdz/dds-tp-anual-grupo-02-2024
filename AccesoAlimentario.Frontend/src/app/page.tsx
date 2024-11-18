import {Box, Button} from "@mui/material";
import NextLink from "next/link";
import {
    colaboradoresImportarRoute,
    colaboradoresRegistroRoute,
    contribucionesRoute, heladeraIncidentesRoute, heladerasRoute, heladeraViandasRoute,
    inicioRoute,
    loginRoute, perfilRoute,
    personaVulnerableRegistroRoute,
    premiosRoute, reportarIncidenciaRoute, reportesRoute, suscripcionesRoute
} from "@routes/router";

export default function IndexPage() {
    return (
        <Box>
            <h1>Index Page</h1>
            <Box>
                <Button LinkComponent={NextLink} href={inicioRoute()}>Inicio</Button>
                <Button LinkComponent={NextLink} href={loginRoute()}>Login</Button>
                <Button LinkComponent={NextLink} href={premiosRoute()}>Premios</Button>
                <Button LinkComponent={NextLink} href={contribucionesRoute()}>Contribuciones</Button>
                <Button LinkComponent={NextLink} href={personaVulnerableRegistroRoute()}>Persona Vulnerable
                    Registro</Button>
                <Button LinkComponent={NextLink} href={heladerasRoute()}>Heladeras</Button>
                <Button LinkComponent={NextLink} href={heladeraViandasRoute("1")}>Heladera Viandas</Button>
                <Button LinkComponent={NextLink} href={heladeraIncidentesRoute("1")}>Heladera Incidentes</Button>
                <Button LinkComponent={NextLink} href={suscripcionesRoute()}>Suscripciones</Button>
                <Button LinkComponent={NextLink} href={colaboradoresRegistroRoute()}>Colaboradores Registro</Button>
                <Button LinkComponent={NextLink} href={colaboradoresImportarRoute()}>Colaboradores Importar</Button>
                <Button LinkComponent={NextLink} href={reportarIncidenciaRoute()}>Reportar Incidencia</Button>
                <Button LinkComponent={NextLink} href={reportesRoute()}>Reportes</Button>
                <Button LinkComponent={NextLink} href={perfilRoute()}>Perfil</Button>
            </Box>
        </Box>
    );
}