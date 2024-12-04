import {
    accesosRoute,
    colaboradoresImportarRoute,
    colaboradoresRegistroRoute,
    contribucionesRoute,
    heladerasRoute,
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
        disabled: false
    },
    {
        id: "premios",
        title: "Premios",
        type: "item",
        url: premiosRoute(),
        icon: "fa-duotone fa-solid fa-gift",
        disabled: false
    },
    {
        id: "contribuciones",
        title: "Contribuciones",
        type: "item",
        url: contribucionesRoute(),
        icon: "fa-duotone fa-solid fa-hand-holding-usd",
        disabled: false
    },
    {
        id: "personas-vulnerables",
        title: "Personas vulnerables",
        type: "collapse",
        icon: "fa-duotone fa-solid fa-user-injured",
        children: [
            {
                id: "personas-vulnerables-registro",
                title: "Registro",
                type: "item",
                url: personaVulnerableRegistroRoute(),
                icon: "fa-duotone fa-solid fa-user-plus",
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
        disabled: false
    },
    {
        id: "suscripciones",
        title: "Suscripciones",
        type: "item",
        url: suscripcionesRoute(),
        icon: "fa-duotone fa-solid fa-envelope",
        disabled: false
    },
    {
        id: "accesos",
        title: "Accesos",
        type: "item",
        url: accesosRoute(),
        icon: "fa-sharp-duotone fa-light fa-key",
        disabled: false
    },
    {
        id: "colaboradores",
        title: "Colaboradores",
        type: "collapse",
        icon: "fa-duotone fa-solid fa-users",
        children: [
            {
                id: "colaboradores-registro",
                title: "Registro",
                type: "item",
                url: colaboradoresRegistroRoute(),
                icon: "fa-duotone fa-solid fa-user-plus",
                disabled: false
            },
            {
                id: "colaboradores-importar",
                title: "Importar",
                type: "item",
                url: colaboradoresImportarRoute(),
                icon: "fa-duotone fa-solid fa-file-import",
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
        disabled: false
    },
    {
        id: "reportes",
        title: "Reportes",
        type: "item",
        url: reportesRoute(),
        icon: "fa-duotone fa-solid fa-chart-bar",
        disabled: false
    },
    {
        id: "tecnicos",
        title: "Tecnicos",
        type: "collapse",
        icon: "fa-duotone fa-solid fa-user-cog",
        children: [
            {
                id: "tecnicos-registro",
                title: "Registro",
                type: "item",
                url: tecnicoRegistroRoute(),
                icon: "fa-duotone fa-solid fa-user-plus",
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
}

export function GetMenuItems() {
    const menuItem: IMenuItem = {
        id: "menu-id",
        title: "Menu",
        type: "group",
        children: items
    }

    return [menuItem];
}