import {ISuscripcionResponse} from "@models/responses/suscripcionesColaboradores/iSuscripcionResponse";

export interface ISuscripcionFaltanteViandasResponse extends ISuscripcionResponse {
    minimo: number;
}