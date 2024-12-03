import {ITarjetaResponse} from "@models/responses/tarjetas/iTarjetaResponse";
import {
    IAutorizacionManipulacionHeladeraResponse
} from "@models/responses/autorizaciones/iAutorizacionesManipulacionHeladeraResponse";

export interface ITarjetaColaboracionesResponse extends ITarjetaResponse {
    autorizaciones: IAutorizacionManipulacionHeladeraResponse[];
}