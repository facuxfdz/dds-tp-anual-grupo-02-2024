import {ITarjetaConsumoResponse} from "@models/responses/tarjetas/iTarjetaConsumoResponse";
import {IRolResponse, IRolResponseMinimo} from "@models/responses/roles/iRolResponse";

export interface IPersonaVulnerableResponse extends IRolResponse {
    cantidadDeMenores: number;
    tarjeta: ITarjetaConsumoResponse;
    fechaRegistroSistema: string;
}

export interface IPersonaVulnerableResponseMinimo extends IRolResponseMinimo{
    cantidadDeMenores: number;
    tarjeta: ITarjetaConsumoResponse;
    fechaRegistroSistema: string;
}