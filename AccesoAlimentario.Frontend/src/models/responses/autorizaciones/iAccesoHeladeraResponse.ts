import {IHeladeraResponseMinimo} from "@models/responses/heladeras/iHeladeraResponse";
import {TipoAcceso} from "@models/enums/tipoAcceso";
import {IViandaResponse} from "@models/responses/heladeras/iViandaResponse";
import {
    IAutorizacionManipulacionHeladeraResponse
} from "@models/responses/autorizaciones/iAutorizacionesManipulacionHeladeraResponse";

export interface IAccesoHeladeraResponse {
    id: string;
    viandas: IViandaResponse[];
    fechaAcceso: string;
    tipoAcceso: TipoAcceso;
    heladera: IHeladeraResponseMinimo;
    autorizacion?: IAutorizacionManipulacionHeladeraResponse;
}