import {IDireccionResponse} from "@models/responses/direcciones/iDireccionResponse";

export interface IPuntoEstrategicoResponse {
    id: string,
    nombre: string,
    longitud: number,
    latitud: number,
    direccion: IDireccionResponse
}