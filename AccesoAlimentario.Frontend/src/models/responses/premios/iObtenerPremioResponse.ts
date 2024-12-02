export interface IObtenerPremioResponse{
    id: number, // ex String
    nombre: string,
    descripcion: string,
    puntosNecesarios: number,
    imagen: string,
    categoria: string // ex Categoria
}

export enum Categoria{
    Gastronomia,
    Electronica,
    ArticulosHogar,
    Otros,
    Null
}