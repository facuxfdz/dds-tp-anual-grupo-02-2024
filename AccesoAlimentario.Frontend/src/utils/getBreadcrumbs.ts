import {
    colaboradoresImportarRoute,
    colaboradoresRegistroRoute,
    contribucionesRoute, heladeraIncidentesRoute, heladerasRoute, heladeraViandasRoute, inicioRoute,
    perfilRoute, personaVulnerableRegistroRoute, premiosRoute,
    reportarIncidenciaRoute,
    reportesRoute,
    suscripcionesRoute
} from "@routes/router";

export interface IBreadcrumbItem {
    id: string;
    title: string;
    url: string;
    father: string | null;
}

const items : IBreadcrumbItem[] = [
    {
        id: "inicio",
        title: "Inicio",
        url: inicioRoute(),
        father: null
    },
    {
        id: "premios",
        title: "Premios",
        url: premiosRoute(),
        father: "inicio"
    },
    {
        id: "contribuciones",
        title: "Contribuciones",
        url: contribucionesRoute(),
        father: "inicio"
    },
    {
        id: "persona-vulnerable",
        title: "Personas vulnerables",
        url: "#",
        father: "inicio"
    },
    {
        id: "personas-vulnerables-registro",
        title: "Registro",
        url: personaVulnerableRegistroRoute(),
        father: "persona-vulnerable"
    },
    {
        id: "heladeras",
        title: "Heladeras",
        url: heladerasRoute(),
        father: "inicio"
    },
    {
        id: "heladera-viandas",
        title: "Viandas",
        url: heladeraViandasRoute("#"),
        father: "heladeras"
    },
    {
        id: "heladera-incidentes",
        title: "Incidentes",
        url: heladeraIncidentesRoute("#"),
        father: "heladeras"
    },
    {
        id: "suscripciones",
        title: "Suscripciones",
        url: suscripcionesRoute(),
        father: "inicio"
    },
    {
        id: "colaboradores",
        title: "Colaboradores",
        url: "#",
        father: "inicio"
    },
    {
        id: "colaboradores-registro",
        title: "Registro",
        url: colaboradoresRegistroRoute(),
        father: "colaboradores"
    },
    {
        id: "colaboradores-importar",
        title: "Importar",
        url: colaboradoresImportarRoute(),
        father: "colaboradores"
    },
    {
        id: "reportar-incidencia",
        title: "Reportar incidencia",
        url: reportarIncidenciaRoute(),
        father: "inicio"
    },
    {
        id: "reportes",
        title: "Reportes",
        url: reportesRoute(),
        father: "inicio"
    },
    {
        id: "perfil",
        title: "Perfil",
        url: perfilRoute(),
        father: "inicio"
    },
]

export function GetBreadcrumbs(path: string): IBreadcrumbItem[] {
    const breadcrumbs: IBreadcrumbItem[] = [];
    let normalizedPath = path.replace(/\/[0-9a-fA-F-]{36}(?=\/|$)/, "/#");
    normalizedPath = normalizedPath.replace(/\/\d+(?=\/|$)/, "/#");

    let current: IBreadcrumbItem | undefined = items.find(item => item.url === normalizedPath);
    while(current) {
        breadcrumbs.unshift(current);
        current = items.find(item => item.id === current?.father);
    }
    return breadcrumbs;
}

export function GetBreadcrumb(path: string): IBreadcrumbItem {
    const breadcrumbs = GetBreadcrumbs(path);
    return breadcrumbs[breadcrumbs.length - 1];
}