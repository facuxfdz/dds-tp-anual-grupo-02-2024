import {IAccesoHeladeraResponse} from "@models/responses/autorizaciones/iAccesoHeladeraResponse";

export interface ITarjetaResponse {
    id: string;
    codigo: string;
    acceso: IAccesoHeladeraResponse[];
    tipo: 'Consumo' | 'Colaboracion';
}