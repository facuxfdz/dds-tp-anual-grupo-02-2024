export interface IOfertaPremioRequest {
    colaboradorId: string;
    fechaContribucion: string;
    nombre: string;
    puntosNecesarios: number;
    imagen: string;
    rubro: "Gastronomia" | "Electronica" | "ArticulosHogar" | "Otros";
}