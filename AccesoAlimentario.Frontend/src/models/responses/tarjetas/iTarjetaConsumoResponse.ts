import {ITarjetaResponse} from "@models/responses/tarjetas/iTarjetaResponse";
import {IColaboradorResponse} from "@models/responses/roles/iColaboradorResponse";

export interface ITarjetaConsumoResponse extends ITarjetaResponse {
    responsable: IColaboradorResponse;
}