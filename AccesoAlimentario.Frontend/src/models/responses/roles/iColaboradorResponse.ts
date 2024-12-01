import {IRolResponse} from "@models/responses/roles/iRolResponse";
import {ContribucionesTipo} from "@models/enums/contribucionesTipo";
import {IFormaContribucionResponse} from "@models/responses/contribuciones/iFormaContribucionResponse";
import {ISuscripcionResponse} from "@models/responses/suscripcionesColaboradores/iSuscripcionResponse";
import {ITarjetaColaboracionesResponse} from "@models/responses/tarjetas/iTarjetaColaboracionesResponse";

export interface IColaboradorResponse extends IRolResponse {
    contribucionesPreferidas: ContribucionesTipo[];
    contribucionesRealizadas: IFormaContribucionResponse[];
    suscripciones: ISuscripcionResponse[];
    puntos: number;
    tarjetaColaboracion?: ITarjetaColaboracionesResponse;
}