import {ISuscripcionResponse} from "@models/responses/suscripcionesColaboradores/iSuscripcionResponse";

export interface ISuscripcionIncidenteHeladeraResponse extends ISuscripcionResponse {
    tipo: 'IncidenteHeladera';
}