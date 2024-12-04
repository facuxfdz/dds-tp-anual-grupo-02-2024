import {
    accesosRoute,
    colaboradoresImportarRoute,
    colaboradoresRegistroRoute,
    contribucionesRoute,
    heladerasRoute, incidentesRoute,
    inicioRoute, perfilRoute,
    personaVulnerableRegistroRoute,
    premiosRoute, reportarIncidenciaRoute, reportesRoute, suscripcionesRoute, tecnicoRegistroRoute
} from "@routes/router";

const items : IMenuItem[] = [
    {
        id: "inicio",
        title: "Inicio",
        type: "item",
        url: inicioRoute(),
        icon: "fa-duotone fa-solid fa-house",
        tecnicos: true,
        colaboradores: true,
        disabled: false
    },
    {
        id: "premios",
        title: "Premios",
        type: "item",
        url: premiosRoute(),
        icon: "fa-duotone fa-solid fa-gift",
        tecnicos: false,
        colaboradores: true,
        disabled: false
    },
    {
        id: "contribuciones",
        title: "Contribuciones",
        type: "item",
        url: contribucionesRoute(),
        icon: "fa-duotone fa-solid fa-hand-holding-usd",
        tecnicos: false,
        colaboradores: true,
        disabled: false
    },
    {
        id: "personas-vulnerables",
        title: "Personas vulnerables",
        type: "collapse",
        icon: "fa-duotone fa-solid fa-user-injured",
        tecnicos: false,
        colaboradores: true,
        children: [
            {
                id: "personas-vulnerables-registro",
                title: "Registro",
                type: "item",
                url: personaVulnerableRegistroRoute(),
                icon: "fa-duotone fa-solid fa-user-plus",
                tecnicos: false,
                colaboradores: true,
                disabled: false
            }
        ]
    },
    {
        id: "heladeras",
        title: "Heladeras",
        type: "item",
        url: heladerasRoute(),
        icon: "fa-duotone fa-solid fa-ice-cream",
        tecnicos: true,
        colaboradores: true,
        disabled: false
    },
    {
        id: "suscripciones",
        title: "Suscripciones",
        type: "item",
        url: suscripcionesRoute(),
        icon: "fa-duotone fa-solid fa-envelope",
        tecnicos: true,
        colaboradores: true,
        disabled: false
    },
    {
        id: "accesos",
        title: "Accesos",
        type: "item",
        url: accesosRoute(),
        icon: "fa-sharp-duotone fa-light fa-key",
        tecnicos: false,
        colaboradores: true,
        disabled: false
    },
    {
        id: "colaboradores",
        title: "Colaboradores",
        type: "collapse",
        icon: "fa-duotone fa-solid fa-users",
        tecnicos: false,
        colaboradores: true,
        children: [
            {
                id: "colaboradores-registro",
                title: "Registro",
                type: "item",
                url: colaboradoresRegistroRoute(),
                icon: "fa-duotone fa-solid fa-user-plus",
                tecnicos: false,
                colaboradores: true,
                disabled: false
            },
            {
                id: "colaboradores-importar",
                title: "Importar",
                type: "item",
                url: colaboradoresImportarRoute(),
                icon: "fa-duotone fa-solid fa-file-import",
                tecnicos: false,
                colaboradores: true,
                disabled: false
            }
        ]
    },
    {
        id: "reportar-incidencia",
        title: "Reportar incidencia",
        type: "item",
        url: reportarIncidenciaRoute(),
        icon: "fa-duotone fa-solid fa-exclamation-triangle",
        tecnicos: true,
        colaboradores: true,
        disabled: false
    },
    {
        id: "reportes",
        title: "Reportes",
        type: "item",
        url: reportesRoute(),
        icon: "fa-duotone fa-solid fa-chart-bar",
        tecnicos: true,
        colaboradores: true,
        disabled: false
    },
    {
        id: "incidentes",
        title: "Incidentes",
        type: "item",
        url: incidentesRoute(),
        icon: "fa-duotone fa-solid fa-exclamation-circle",
        tecnicos: true,
        colaboradores: false,
        disabled: false
    },
    {
        id: "tecnicos",
        title: "Tecnicos",
        type: "collapse",
        icon: "fa-duotone fa-solid fa-user-cog",
        tecnicos: true,
        colaboradores: false,
        children: [
            {
                id: "tecnicos-registro",
                title: "Registro",
                type: "item",
                url: tecnicoRegistroRoute(),
                icon: "fa-duotone fa-solid fa-user-plus",
                tecnicos: true,
                colaboradores: false,
                disabled: false
            }
        ]
    },
    {
        id: "perfil",
        title: "Perfil",
        type: "item",
        url: perfilRoute(),
        icon: "fa-duotone fa-solid fa-user",
        tecnicos: true,
        colaboradores: true,
        disabled: false
    }
]

export interface IMenuItem {
    id: string,
    title: string,
    type: string,
    url?: string,
    icon?: string,
    children?: IMenuItem[],
    caption?: string,
    disabled?: boolean,
    chip?: {
        color?: 'default' | 'primary' | 'secondary' | 'error' | 'info' | 'success' | 'warning',
        variant?: 'filled' | 'outlined',
        size?: 'small' | 'medium',
        label?: string,
    },
    tecnicos: boolean,
    colaboradores: boolean,
}

export function GetMenuItems() {
    const menuItem: IMenuItem = {
        id: "menu-id",
        title: "Menu",
        type: "group",
        children: items,
        tecnicos: true,
        colaboradores: true
    }

    return [menuItem];
}