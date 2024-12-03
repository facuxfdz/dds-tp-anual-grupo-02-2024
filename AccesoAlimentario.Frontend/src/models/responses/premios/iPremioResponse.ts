import {TipoRubro} from "@models/enums/tipoRubro";

export interface IPremioResponse {
    id: string;
    nombre: string;
    puntosNecesarios: number;
    imagen: string;
    fechaReclamo: string;
    rubro: TipoRubro;
}