import {ISuscripcionResponse} from "@models/responses/suscripcionesColaboradores/iSuscripcionResponse";

export interface ISuscripcionExcedenteViandasResponse extends ISuscripcionResponse {
    maximo: number;
}