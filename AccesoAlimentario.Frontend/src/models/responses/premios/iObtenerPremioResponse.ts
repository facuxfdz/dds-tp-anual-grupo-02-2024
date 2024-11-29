export interface IObtenerPremioResponse {
    id: string,
    nombre: string,
    puntos: number
    imagen: string,
    reclamadoPor: string,
    fechaReclamo: string,
    categoria: CategoriaPremio
}

export enum CategoriaPremio
{
    Gastronomia,
    Electronica,
    ArticulosHogar,
    Otros
}