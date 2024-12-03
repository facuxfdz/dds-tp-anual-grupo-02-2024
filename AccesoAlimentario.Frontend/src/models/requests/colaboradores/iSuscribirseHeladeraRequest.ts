import {TipoSuscripcion} from "@models/enums/tipoSuscripcion";

export interface ISuscribirseHeladeraRequest {
    colaboradorId: string;
    heladeraId: string;
    minimo: number;
    maximo: number;
    tipo: TipoSuscripcion;
}